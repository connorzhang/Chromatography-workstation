import re

filepath = r'i:\GIT\VS2022\Chromatography-workstation\src\edge\cmd\collector\auto_connect.go'

with open(filepath, 'r', encoding='utf-8') as f:
    content = f.read()

# Fix 1: After temperature telemetry publish, update LastTelemetry
old_temp = '''			hub.Publish(modDevID, te)

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
			pstore.SaveHardwareConfig(modDevID, hwCfg)'''

new_temp = '''			hub.Publish(modDevID, te)

			// Update LastTelemetry for audit snapshot
			st := getState(states, modDevID)
			st.mu.Lock()
			if st.LastTelemetry == nil {
				st.LastTelemetry = &telemetryEvent{Type: "telemetry", DeviceID: modDevID}
			}
			st.LastTelemetry.At = time.Now().UTC()
			if te.TempInj1 != nil {
				st.LastTelemetry.TempInj1 = te.TempInj1
				st.LastTelemetry.SetTempInj1 = te.SetTempInj1
			}
			if te.TempCol != nil {
				st.LastTelemetry.TempCol = te.TempCol
				st.LastTelemetry.SetTempCol = te.SetTempCol
			}
			if te.TempDet1 != nil {
				st.LastTelemetry.TempDet1 = te.TempDet1
				st.LastTelemetry.SetTempDet1 = te.SetTempDet1
			}
			st.mu.Unlock()

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
			pstore.SaveHardwareConfig(modDevID, hwCfg)'''

content = content.replace(old_temp, new_temp)

# Fix 2: After EPC telemetry publish, update LastTelemetry
old_epc = '''			te.CarrierPsi = &vPress
			te.CarrierSccm = &vFlow
			
			hub.Publish(modDevID, te)'''

new_epc = '''			te.CarrierPsi = &vPress
			te.CarrierSccm = &vFlow
			
			hub.Publish(modDevID, te)

			// Update LastTelemetry for audit snapshot
			st := getState(states, modDevID)
			st.mu.Lock()
			if st.LastTelemetry == nil {
				st.LastTelemetry = &telemetryEvent{Type: "telemetry", DeviceID: modDevID}
			}
			st.LastTelemetry.At = time.Now().UTC()
			st.LastTelemetry.CarrierPsi = &vPress
			st.LastTelemetry.CarrierSccm = &vFlow
			st.mu.Unlock()'''

content = content.replace(old_epc, new_epc)

with open(filepath, 'w', encoding='utf-8') as f:
    f.write(content)

print("auto_connect.go patched: LastTelemetry now updated from Modbus HAL data")