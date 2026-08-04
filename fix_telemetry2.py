import re

filepath = r'i:\GIT\VS2022\Chromatography-workstation\src\edge\cmd\collector\main.go'

with open(filepath, 'r', encoding='utf-8') as f:
    content = f.read()

# Fix 159
content = re.sub(r'e\.AirSccm = f64p\(items\[10\]\.ActualSccm\)\s*\}\s*hub\.Publish\(f\.DeviceID, e\)', '''e.AirSccm = f64p(items[10].ActualSccm)
}

st.mu.Lock()
if st.LastTelemetry == nil {
st.LastTelemetry = &telemetryEvent{Type: "telemetry", DeviceID: f.DeviceID}
}
st.LastTelemetry.At = time.Now().UTC()
st.LastTelemetry.CarrierPsi = e.CarrierPsi
st.LastTelemetry.CarrierSccm = e.CarrierSccm
st.LastTelemetry.H2Psi = e.H2Psi
st.LastTelemetry.H2Sccm = e.H2Sccm
st.LastTelemetry.AirPsi = e.AirPsi
st.LastTelemetry.AirSccm = e.AirSccm
st.LastTelemetry.Epc = e.Epc
st.mu.Unlock()

hub.Publish(f.DeviceID, e)''', content)

# Fix 143
content = re.sub(r'if te, ok := parseTemps143\(f\.Payload\); ok \{\s*te\.DeviceID = f\.DeviceID\s*st\.LastTelemetry = &te\s*hub\.Publish\(f\.DeviceID, te\)', '''if te, ok := parseTemps143(f.Payload); ok {
te.DeviceID = f.DeviceID

st.mu.Lock()
if st.LastTelemetry == nil {
st.LastTelemetry = &telemetryEvent{Type: "telemetry", DeviceID: f.DeviceID}
}
st.LastTelemetry.At = time.Now().UTC()
st.LastTelemetry.TempCol = te.TempCol
st.LastTelemetry.TempInj1 = te.TempInj1
st.LastTelemetry.TempInj2 = te.TempInj2
st.LastTelemetry.TempDet1 = te.TempDet1
st.LastTelemetry.TempDet2 = te.TempDet2
st.LastTelemetry.TempDet3 = te.TempDet3
st.LastTelemetry.Heating = te.Heating
st.LastTelemetry.Ready = te.Ready

mergedTe := *st.LastTelemetry
st.mu.Unlock()

hub.Publish(f.DeviceID, mergedTe)''', content)

# Fix 128
content = re.sub(r'if te, ok := parseSetTemps128\(f\.Payload\); ok \{\s*te\.DeviceID = f\.DeviceID\s*st\.LastTelemetry = &te\s*hub\.Publish\(f\.DeviceID, te\)', '''if te, ok := parseSetTemps128(f.Payload); ok {
te.DeviceID = f.DeviceID

st.mu.Lock()
if st.LastTelemetry == nil {
st.LastTelemetry = &telemetryEvent{Type: "telemetry", DeviceID: f.DeviceID}
}
st.LastTelemetry.SetTempCol = te.SetTempCol
st.LastTelemetry.SetTempInj1 = te.SetTempInj1
st.LastTelemetry.SetTempInj2 = te.SetTempInj2
st.LastTelemetry.SetTempDet1 = te.SetTempDet1
st.LastTelemetry.SetTempDet2 = te.SetTempDet2
st.LastTelemetry.SetTempDet3 = te.SetTempDet3
st.mu.Unlock()

hub.Publish(f.DeviceID, te)''', content)

with open(filepath, 'w', encoding='utf-8') as f:
    f.write(content)

print("telemetry merge patched with regex")
