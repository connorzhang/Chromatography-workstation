package main

import (
	"fmt"
	"os"
	"strconv"
	"sync"
	"time"

	"chromatography-workstation/edge/internal/realtime"
)

var voltageHighFreqStarted = false

// startVoltageHighFreqPoll 独立的高频电压采集循环（500ms 一次 = 1秒2次）
// 通过 SharedPortLock 自动与温控/EPC 排队，不会冲突
func startVoltageHighFreqPoll(ctrl *VoltageController) {
	ticker := time.NewTicker(500 * time.Millisecond)
	defer ticker.Stop()
	for range ticker.C {
		ctrl.ReadVoltageOnce()
	}
}

func startAutoConnect(states *sync.Map, hub *realtime.Hub) {
	go func() {
		for {
			cfg := pstore.LoadSysConfig()

			modDevID := cfg.ModularDeviceID
			if modDevID == "" {
				modDevID = "GC-MODULAR"
			}

			tcdPort := os.Getenv("TCD_PORT")
			if tcdPort == "" {
				tcdPort = cfg.ModularTCDPort
				if tcdPort == "" {
					tcdPort = "/dev/ttyUSB5" // Default for edge device
				}
			}

			modbusPort := os.Getenv("MODBUS_TEMP_PORT")
			if modbusPort == "" {
				modbusPort = cfg.ModularTempPort
				if modbusPort == "" {
					modbusPort = "/dev/ttyUSB3" // Default for edge device
				}
			}

			modbusSlaveIDStr := os.Getenv("MODBUS_TEMP_SLAVE_ID")
			modbusSlaveID := byte(20)
			if modbusSlaveIDStr != "" {
				if val, err := strconv.ParseUint(modbusSlaveIDStr, 10, 8); err == nil {
					modbusSlaveID = byte(val)
				}
			} else if cfg.ModularTempSlaveID > 0 {
				modbusSlaveID = byte(cfg.ModularTempSlaveID)
			}

			// --- Handle Modbus Temp Auto-Connect ---
			modbusTempCtrlMu.Lock()
			if globalModbusTempCtrl == nil || globalModbusTempCtrl.port != modbusPort || globalModbusTempCtrl.address != modbusSlaveID {
				if globalModbusTempCtrl != nil {
					globalModbusTempCtrl.Close()
				}
				globalModbusTempCtrl = NewModbusTempController(modbusPort, modbusSlaveID)
			}
			mCtrl := globalModbusTempCtrl
			modbusTempCtrlMu.Unlock()

			state, err := mCtrl.ReadState()
			if err != nil {
				// We don't close the port on timeout, because it might be shared with EPC.
				// Just log and continue.
				fmt.Printf("[AutoConnect] Modbus Temp read failed: %v\n", err)
			} else {
				// Push Modbus Temp data to modular device
				te := telemetryEvent{
					Type:     "telemetry",
					DeviceID: modDevID,
					At:       time.Now().UTC(),
				}
				if !state.Disconnected[0] {
					v := state.RealTimeTemps[0]
					te.TempInj1 = &v
					s := float64(state.SetTemps[0])
					te.SetTempInj1 = &s
				}
				if !state.Disconnected[1] {
					v := state.RealTimeTemps[1]
					te.TempCol = &v
					s := float64(state.SetTemps[1])
					te.SetTempCol = &s
				}
				if !state.Disconnected[2] {
					v := state.RealTimeTemps[2]
					te.TempDet1 = &v
					s := float64(state.SetTemps[2])
					te.SetTempDet1 = &s
				}
				hub.Publish(modDevID, te)

				// Update hardware config for UI to query
				hwCfg, _ := pstore.LoadHardwareConfig(modDevID)
				if hwCfg.Temperatures == nil {
					hwCfg.Temperatures = make(map[string]float64)
				}
				if te.SetTempInj1 != nil {
					hwCfg.Temperatures["Inj1"] = *te.SetTempInj1
				}
				if te.SetTempCol != nil {
					hwCfg.Temperatures["Col"] = *te.SetTempCol
				}
				if te.SetTempDet1 != nil {
					hwCfg.Temperatures["Det1"] = *te.SetTempDet1
				}
				pstore.SaveHardwareConfig(modDevID, hwCfg)
			}

			// Add a short delay to ensure RS-485 bus is idle before switching slave ID
		time.Sleep(100 * time.Millisecond)

		// --- Handle Modbus Voltage Auto-Connect (地址1, 共享COM7) ---
		voltagePort := cfg.ModularTempPort // 电压模块与温控共享同一485总线
		voltageSlaveID := byte(1)         // 电压采集模块地址为1

		voltageCtrlMu.Lock()
		if globalVoltageCtrl == nil || globalVoltageCtrl.port != voltagePort || globalVoltageCtrl.address != voltageSlaveID {
			globalVoltageCtrl = NewVoltageController(voltagePort, voltageSlaveID)
		}
		vCtrl := globalVoltageCtrl
		voltageCtrlMu.Unlock()

		// 在此统一读取电压并缓存，前端 API 只读缓存值，避免与温控/EPC 争抢串口锁
		vCtrl.ReadVoltageOnce()

		// 启动独立的高频电压采集 goroutine（500ms 一次 = 1秒2次）
		// 通过 SharedPortLock 自动与温控/EPC 排队，不会冲突
		if !voltageHighFreqStarted {
			voltageHighFreqStarted = true
			go startVoltageHighFreqPoll(vCtrl)
		}

		// Add a short delay to ensure RS-485 bus is idle before switching slave ID
		time.Sleep(100 * time.Millisecond)

		// --- Handle Modbus EPC Auto-Connect ---
			epcPort := cfg.ModularEPCPort
			if epcPort == "" {
				epcPort = cfg.ModularTempPort // EPC shares the same 485 port with Temp Controller
			}
			epcSlaveID := byte(21) // Default EPC Modbus Slave ID
			
			modbusEPCCtrlMu.Lock()
			if globalModbusEPCCtrl == nil || globalModbusEPCCtrl.port != epcPort || globalModbusEPCCtrl.address != epcSlaveID {
				if globalModbusEPCCtrl != nil {
					globalModbusEPCCtrl.Close()
				}
				globalModbusEPCCtrl = NewModbusEPCController(epcPort, epcSlaveID)
			}
			eCtrl := globalModbusEPCCtrl
			modbusEPCCtrlMu.Unlock()

			epcState, err := eCtrl.ReadState()
			if err != nil {
				fmt.Printf("[AutoConnect] Modbus EPC read failed: %v\n", err)
			} else {
				// We can push EPC real-time telemetry here
				te := telemetryEvent{
					Type:     "telemetry",
					DeviceID: modDevID,
					At:       time.Now().UTC(),
				}
				
				vPress := float64(epcState.RealPressure)
				vFlow := float64(epcState.RealFlow)
				
				te.CarrierPsi = &vPress
				te.CarrierSccm = &vFlow
				
				hub.Publish(modDevID, te)
			}

			// --- Handle TCD Auto-Connect ---
			tcdCtrlMu.Lock()
			if globalTCDCtrl == nil || globalTCDCtrl.portName != tcdPort {
				if globalTCDCtrl != nil {
					globalTCDCtrl.Close()
				}
				globalTCDCtrl = NewTCDController(tcdPort)
			}
			tCtrl := globalTCDCtrl
			tcdCtrlMu.Unlock()

			tState := tCtrl.GetState()
			if !tState.Connected || time.Since(tState.LastUpdate) > 3*time.Second {
				tCtrl.Close() // Ensure old resources are freed

				tcdCtrlMu.Lock()
				globalTCDCtrl = NewTCDController(tcdPort)
				tCtrl = globalTCDCtrl

				// Push data to configured Modular Device ID
			modDevID := cfg.ModularDeviceID
			if modDevID == "" {
				modDevID = "GC-MODULAR"
			}
			
			tCtrl.OnData = func(pts []float64) {
				// Always use the latest device ID for data routing
				currentCfg := pstore.LoadSysConfig()
				currentDevID := currentCfg.ModularDeviceID
				if currentDevID == "" {
					currentDevID = "GC-MODULAR"
				}

				st := getState(states, currentDevID)
				st.mu.Lock()
				st.synced = true
				st.lastSeen = time.Now()
				dtS := 0.02 // Default to 50Hz (0.02s interval)
				if st.lastTS == nil {
					st.lastTS = map[int]float64{}
				}
				t0 := st.lastTS[0]
				st.lastTS[0] = t0 + float64(len(pts))*dtS
				st.last143 = time.Now()
				tok, _ := appendSessionSamplesLocked(st, 0, dtS, t0, pts)
				st.mu.Unlock()

				// Publish to realtime hub for UI plotting
				hub.Publish(currentDevID, event{
					Type:         "samples",
					DeviceID:     currentDevID,
					At:           time.Now(),
					Channel:      0,
					SessionToken: tok,
					DTs:          dtS,
					T0s:          t0,
					Values:       pts,
				})
			}
				tcdCtrlMu.Unlock()

				if err := tCtrl.Connect(); err == nil {
					fmt.Printf("[AutoConnect] TCD re-connected on %s.\n", tcdPort)
					
					// Apply persisted TCD Bridge Current
					hwCfg, _ := pstore.LoadHardwareConfig(modDevID)
					if hwCfg.TCDBridgeCurrent > 0 {
						// Sleep a bit to ensure port is ready
						go func(val uint8) {
							time.Sleep(1 * time.Second)
							_ = tCtrl.SetBridgeCurrent(val)
							fmt.Printf("[AutoConnect] Applied persisted TCD Bridge Current: %d\n", val)
						}(hwCfg.TCDBridgeCurrent)
					}
				}
			}

			time.Sleep(3 * time.Second)
		}
	}()
}
