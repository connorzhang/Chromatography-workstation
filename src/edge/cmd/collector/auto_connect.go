package main

import (
	"fmt"
	"os"
	"strconv"
	"sync"
	"time"

	"chromatography-workstation/edge/internal/realtime"
)

func startAutoConnect(states *sync.Map, hub *realtime.Hub) {
	go func() {
		for {
			cfg := pstore.LoadSysConfig()

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
			if globalModbusTempCtrl == nil {
				globalModbusTempCtrl = NewModbusTempController(modbusPort, modbusSlaveID)
			}
			mCtrl := globalModbusTempCtrl
			modbusTempCtrlMu.Unlock()

			state, err := mCtrl.ReadState()
			if err != nil {
				// Re-init client if it fails
				mCtrl.Close()
				if err := mCtrl.Connect(); err == nil {
					fmt.Printf("[AutoConnect] Modbus Temp re-connected on %s (Slave: %d).\n", modbusPort, modbusSlaveID)
				}
			} else {
				// Push Modbus Temp data to GC-MODULAR
				te := telemetryEvent{
					Type:     "telemetry",
					DeviceID: "GC-MODULAR",
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
				hub.Publish("GC-MODULAR", te)

				// Update hardware config for UI to query
				hwCfg, _ := pstore.LoadHardwareConfig("GC-MODULAR")
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
				pstore.SaveHardwareConfig("GC-MODULAR", hwCfg)
			}

			// --- Handle TCD Auto-Connect ---
			tcdCtrlMu.Lock()
			if globalTCDCtrl == nil {
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

				// Push data to GC-MODULAR twin
				tCtrl.OnData = func(pts []float64) {
					st := getState(states, "GC-MODULAR")
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
					hub.Publish("GC-MODULAR", event{
						Type:         "samples",
						DeviceID:     "GC-MODULAR",
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
				}
			}

			time.Sleep(3 * time.Second)
		}
	}()
}
