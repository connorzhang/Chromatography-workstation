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
			if s == nil || !s.active {
				st.mu.Unlock()
				continue
			}
			started := s.startedAt
			snapshotDone := s.snapshotDone
			st.mu.Unlock()

			timeSinceStart := time.Since(started)

			// 0. 外部事件时间程序调度 (Cmd 10 / Cmd 101 / 多位阀)
			// TODO: 后续可以精确按秒级对比当前进样时间与事件配置表，进行继电器下发
			// 目前仅占位

			// 1. Check if we need to stop acquisition / generate results
			if !snapshotDone && timeSinceStart >= acqDur {
				// AcqMin reached: perform snapshot and result calculation
				finalizeSession(hub, st, deviceID, ch, method)
				if !ui.Loop {
					// Send Stop command to hardware ONLY if not looping
					_ = sendCmd(st, deviceID, 23, []byte{byte(ch)})
				}
			}

			// 2. We removed the local Start (Cmd 22) for the next cycle
			// because the hardware mainboard handles the CycleInterval itself.
			// When the hardware starts the next cycle, it will send Cmd 150 (Start Ack),
			// which will trigger resetSession() in main.go.

			// HOWEVER, if we are in Modular Driver mode, there is NO MAINBOARD!
			// We MUST handle the loop/cycle interval locally.
			if pstore != nil && pstore.LoadSysConfig().DriverMode == "modular" && ui.Loop {
				hw, _ := pstore.LoadHardwareConfig(deviceID)
				cycleInterval := hw.CycleInterval
				if cycleInterval <= 0 {
					cycleInterval = acqMin // Fallback to acqMin if not set
				}
				if cycleInterval > 0 {
					cycleDur := time.Duration(cycleInterval*60.0*1000.0) * time.Millisecond
					if timeSinceStart >= cycleDur {
						LogInfof("Modular mode auto-cycle triggered: timeSinceStart=%v >= cycleDur=%v", timeSinceStart, cycleDur)
						// Start next cycle automatically!
						resetSession(st, ch)
					}
				}
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
