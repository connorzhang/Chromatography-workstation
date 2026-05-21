package main

import (
	"sync"
	"time"

	v1 "chromatography-workstation/edge/internal/contracts/v1"
	"chromatography-workstation/edge/internal/realtime"
)

func startEngineScheduler(hub *realtime.Hub, states *sync.Map, method v1.Method) {
	go func() {
		tk := time.NewTicker(1 * time.Second)
		defer tk.Stop()
		for range tk.C {
			schedulerTick(hub, states, method)
		}
	}()
}

func schedulerTick(hub *realtime.Hub, states *sync.Map, method v1.Method) {
	states.Range(func(key, value any) bool {
		deviceID := key.(string)
		if deviceID == "" {
			return true
		}
		st := value.(*deviceState)
		ui := getUIForDevice(deviceID)
		acqMin := ui.AcqMin
		if acqMin <= 0 {
			return true
		}
		acqDur := time.Duration(acqMin*60.0*1000.0) * time.Millisecond
		for ch := 0; ch < 8; ch++ {
			st.mu.Lock()
			s := st.sessions[ch]
			if s == nil || !s.active || s.snapshotDone {
				st.mu.Unlock()
				continue
			}
			started := s.startedAt
			st.mu.Unlock()
			if time.Since(started) < acqDur {
				continue
			}
			
			// AcqMin reached: perform snapshot and result calculation
			_, _ = publishSessionResultSnapshot(hub, st, deviceID, ch, method)
			
			// Enforce backend scheduling: Stop or Loop
			if ui.Loop {
				// Restart session for the next cycle
				resetSession(st, ch)
				// Send Start command to hardware
				_ = sendCmd(st, deviceID, 22, []byte{byte(ch)})
				// Send the secondary start/reset command that frontend used to send
				_ = sendCmd(st, deviceID, 25, nil)
			} else {
				// Finalize and stop
				finalizeSession(hub, st, deviceID, ch, method)
				// Send Stop command to hardware
				channelMask := byte(1 << uint(ch))
				_ = sendCmd(st, deviceID, 245, []byte{channelMask})
			}
		}
		return true
	})
}

func getUIForDevice(deviceID string) uiState {
	uiMu.Lock()
	st, ok := uiByDevice[deviceID]
	uiMu.Unlock()
	if ok {
		if st.DeviceID == "" {
			st.DeviceID = deviceID
		}
		if st.ActiveTab == "" {
			st.ActiveTab = "overview"
		}
		return st
	}
	if pstore != nil {
		if st2, ok2 := pstore.LoadUI(deviceID); ok2 {
			if st2.DeviceID == "" {
				st2.DeviceID = deviceID
			}
			if st2.ActiveTab == "" {
				st2.ActiveTab = "overview"
			}
			// Cache the loaded UI state to prevent repetitive disk reads
			uiMu.Lock()
			uiByDevice[deviceID] = st2
			uiMu.Unlock()
			return st2
		}
	}
	return defaultUIState(deviceID)
}

