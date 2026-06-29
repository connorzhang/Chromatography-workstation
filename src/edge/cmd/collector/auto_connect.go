package main

import (
	"fmt"
	"os"
	"strconv"
	"time"
)

func startAutoConnect() {
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

			_, err := mCtrl.ReadState()
			if err != nil {
				// Re-init client if it fails
				mCtrl.Close()
				if err := mCtrl.Connect(); err == nil {
					fmt.Printf("[AutoConnect] Modbus Temp re-connected on %s (Slave: %d).\n", modbusPort, modbusSlaveID)
				}
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
				tcdCtrlMu.Unlock()

				if err := tCtrl.Connect(); err == nil {
					fmt.Printf("[AutoConnect] TCD re-connected on %s.\n", tcdPort)
				}
			}

			time.Sleep(3 * time.Second)
		}
	}()
}
