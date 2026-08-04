package main

import (
	"fmt"
	"os"
	"strconv"
	"sync"
	"time"

	"chromatography-workstation/edge/internal/realtime"
)

var (
	voltageHighFreqStarted = false
	tempHighFreqStarted    = false
	epcHighFreqStarted     = false
)

func startVoltageHighFreqPoll(ctrl *VoltageController) {
	ticker := time.NewTicker(500 * time.Millisecond)
	defer ticker.Stop()
	for range ticker.C {
		ctrl.ReadVoltageOnce()
	}
}

func startTempHighFreqPoll(ctrl *ModbusTempController) {
	ticker := time.NewTicker(500 * time.Millisecond)
	defer ticker.Stop()
	for range ticker.C {
		ctrl.ReadStateOnce()
	}
}

func startEpcHighFreqPoll(ctrl *ModbusEPCController) {
	ticker := time.NewTicker(500 * time.Millisecond)
	defer ticker.Stop()
	for range ticker.C {
		ctrl.ReadStateOnce()
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
				tempHighFreqStarted = false
			}
			mCtrl := globalModbusTempCtrl
			modbusTempCtrlMu.Unlock()

			mCtrl.ReadStateOnce()
			if !tempHighFreqStarted {
				tempHighFreqStarted = true
				go startTempHighFreqPoll(mCtrl)
			}
			state := mCtrl.GetCachedState()

			if !state.Connected {
				// We don't close the port on timeout, because it might be shared with EPC.
				// Just log and continue.
				fmt.Printf("[AutoConnect] Modbus Temp read failed\n")
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
			voltageHighFreqStarted = false
		}
		vCtrl := globalVoltageCtrl
		voltageCtrlMu.Unlock()

		if !voltageHighFreqStarted {
			vCtrl.ReadVoltageOnce() // initial read
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
				epcHighFreqStarted = false
			}
			eCtrl := globalModbusEPCCtrl
			modbusEPCCtrlMu.Unlock()

			if !epcHighFreqStarted {
				eCtrl.ReadStateOnce()
				epcHighFreqStarted = true
				go startEpcHighFreqPoll(eCtrl)
			}
			epcState := eCtrl.GetCachedState()

			if !epcState.Connected {
				fmt.Printf("[AutoConnect] Modbus EPC read failed\n")
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
				dtS := 0.05 // Actual TCD hardware runs at 20Hz (0.05s interval)
				if st.lastTS == nil {
					st.lastTS = map[int]float64{}
				}
				t0 := st.lastTS[1]
				st.lastTS[1] = t0 + float64(len(pts))*dtS
				st.last143 = time.Now()
				tok, active := appendSessionSamplesLocked(st, 1, dtS, t0, pts)
								st.mu.Unlock()
								if active {
									hub.Publish(currentDevID, event{
										Type:         "samples",
										DeviceID:     currentDevID,
										At:           time.Now(),
										Channel:      1,
										SessionToken: tok,
										DTs:          dtS,
										T0s:          t0,
										Values:       pts,
									})
								}
			}
				tcdCtrlMu.Unlock()

				if err := tCtrl.Connect(); err == nil {
					fmt.Printf("[AutoConnect] TCD re-connected on %s.\n", tcdPort)
					
					// On TCD re-connect, safely set Bridge Current to 0 to prevent overheating.
					// Send it multiple times to ensure success.
					go func() {
						time.Sleep(1 * time.Second)
						for i := 0; i < 3; i++ {
							_ = tCtrl.SetBridgeCurrent(0)
							time.Sleep(500 * time.Millisecond)
						}
						fmt.Printf("[AutoConnect] Safely reset TCD Bridge Current to 0 upon reconnection\n")
						
						// Update persisted config so UI reflects the 0 state
						hwCfg, _ := pstore.LoadHardwareConfig(modDevID)
						hwCfg.TCDBridgeCurrent = 0
						pstore.SaveHardwareConfig(modDevID, hwCfg)
					}()
				}
			}

			time.Sleep(3 * time.Second)
		}
	}()
}
