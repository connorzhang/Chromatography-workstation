import re

filepath = r'i:\GIT\VS2022\Chromatography-workstation\src\edge\cmd\collector\auto_connect.go'

with open(filepath, 'r', encoding='utf-8') as f:
    content = f.read()

# Fix 1: After temperature telemetry publish, update LastTelemetry
content = re.sub(
    r'(hub\.Publish\(modDevID, te\)\s*\n\s*// Update hardware config for UI to query)',
    '''hub.Publish(modDevID, te)

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

			// Update hardware config for UI to query''',
    content,
    count=1
)

# Fix 2: After EPC telemetry publish, update LastTelemetry
content = re.sub(
    r'(te\.CarrierSccm = &vFlow\s*\n\s*hub\.Publish\(modDevID, te\))',
    '''te.CarrierSccm = &vFlow
			
			hub.Publish(modDevID, te)

			// Update LastTelemetry for audit snapshot
			est := getState(states, modDevID)
			est.mu.Lock()
			if est.LastTelemetry == nil {
				est.LastTelemetry = &telemetryEvent{Type: "telemetry", DeviceID: modDevID}
			}
			est.LastTelemetry.At = time.Now().UTC()
			est.LastTelemetry.CarrierPsi = &vPress
			est.LastTelemetry.CarrierSccm = &vFlow
			est.mu.Unlock()''',
    content,
    count=1
)

with open(filepath, 'w', encoding='utf-8') as f:
    f.write(content)

print("auto_connect.go patched with regex")