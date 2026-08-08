package main

import (

	"log"
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
			if s == nil {
				st.mu.Unlock()
				continue
			}
			started := s.startedAt
			snapshotDone := s.snapshotDone
			isActive := s.active
			st.mu.Unlock()

			timeSinceStart := time.Since(started)

			// 0. 外部事件时间程序调度 (Modular 模式下通过温控模块 IO CH5-8 下发开关量)
			// 事件1-4 对应 IO CH5-8
			if pstore != nil && pstore.LoadSysConfig().DriverMode == "modular" {
				hw, _ := pstore.LoadHardwareConfig(deviceID)
				if len(hw.Events) > 0 {
					elapsedMin := timeSinceStart.Minutes()
					// 计算当前应该的 event_mask 状态
					currentMask := 0
					for _, evt := range hw.Events {
						if elapsedMin >= evt.Time {
							currentMask = evt.EventMask
						} else {
							break
						}
					}
					// 读取 lastEventMask 需要持锁
					st.mu.Lock()
					lastMask := s.lastEventMask
					st.mu.Unlock()
					// 与上次下发的 mask 对比，仅变化时下发
					if lastMask != currentMask {
						log.Printf("[Scheduler] ch=%d elapsedMin=%.3f lastMask=%d -> currentMask=%d events=%v", ch, elapsedMin, lastMask, currentMask, hw.Events)
						for bit := 0; bit < 4; bit++ {
							oldOn := (lastMask & (1 << bit)) != 0
							newOn := (currentMask & (1 << bit)) != 0
							if oldOn != newOn {
								// 事件1-4 映射到 IO CH5-8
								ioChannel := bit + 5
								modbusTempCtrlMu.Lock()
								ctrl := globalModbusTempCtrl
								modbusTempCtrlMu.Unlock()
								if ctrl != nil {
									if err := ctrl.SetIO(ioChannel, newOn); err != nil {
										log.Printf("[Scheduler] SetIO CH%d=%v failed: %v", ioChannel, newOn, err)
									} else {
										log.Printf("[Scheduler] Event CH%d (IO CH%d) -> %v at %.4f min", bit+1, ioChannel, newOn, elapsedMin)
									}
								} else {
									log.Printf("[Scheduler] globalModbusTempCtrl is nil, cannot SetIO CH%d", ioChannel)
								}
							}
						}
						st.mu.Lock()
						s.lastEventMask = currentMask
						st.mu.Unlock()
					}
				}
			} else {
				log.Printf("[Scheduler] SKIP event dispatch: pstore=%v driverMode=%v", pstore != nil, func() string {
					if pstore != nil { return pstore.LoadSysConfig().DriverMode }
					return "nil"
				}())
			}

			// 1. Check if we need to stop acquisition / generate results
			if isActive && !snapshotDone && timeSinceStart >= acqDur {
				// AcqMin reached: perform snapshot and result calculation
				finalizeSession(hub, st, deviceID, ch, method)
				// hw, _ := pstore.LoadHardwareConfig(deviceID)
				// isLooping := ui.Loop || hw.CycleCount > 1
				// if !isLooping {
				// 	// Send Stop command to hardware ONLY if not looping
				// 	// _ = sendCmd(st, deviceID, 23, []byte{byte(ch)})
				// }
			}

			// 2. We removed the local Start (Cmd 22) for the next cycle
			// because the hardware mainboard handles the CycleInterval itself.
			// When the hardware starts the next cycle, it will send Cmd 150 (Start Ack),
			// which will trigger resetSession() in main.go.

			// HOWEVER, if we are in Modular Driver mode OR this is a modular device, there is NO MAINBOARD!
			// We MUST handle the loop/cycle interval locally.
			hw2, _ := pstore.LoadHardwareConfig(deviceID)
			isLooping2 := ui.Loop || hw2.CycleCount > 1
						if isLooping2 {
				hw, _ := pstore.LoadHardwareConfig(deviceID)
				cycleInterval := hw.CycleInterval
				if cycleInterval <= 0 {
					cycleInterval = acqMin // Fallback to acqMin if not set
				}
				if cycleInterval > 0 {
					cycleDur := time.Duration(cycleInterval*60.0*1000.0) * time.Millisecond
					if timeSinceStart >= cycleDur {
						LogInfof("Modular mode auto-cycle triggered: timeSinceStart=%v >= cycleDur=%v", timeSinceStart, cycleDur)
						// 循环重置时，复位事件掩码，下一轮重新触发事件
						st.mu.Lock()
						s.lastEventMask = 0
						st.mu.Unlock()
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
