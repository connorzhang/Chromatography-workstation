import io

go_path = r'i:\GIT\VS2022\Chromatography-workstation\src\edge\cmd\collector\audit_snapshot.go'
with io.open(go_path, 'r', encoding='utf-8') as f:
    go_content = f.read()

import re

# Struct replacement
struct_pattern = r'type AuditSnapshot struct \{.*?\}'
new_struct = '''type AuditSnapshot struct {
Timestamp     time.Time json:"timestamp"
TempBox       float64   json:"tempBox"
CarrierPsi    float64   json:"carrierPsi"
CarrierSccm   float64   json:"carrierSccm"
BridgeCurrent uint8     json:"bridgeCurrent"
BaselineMax   float64   json:"baselineMax"
BaselineMin   float64   json:"baselineMin"
BaselineDrift float64   json:"baselineDrift"
BaselineNoise float64   json:"baselineNoise"
}'''
go_content = re.sub(struct_pattern, new_struct, go_content, flags=re.DOTALL)

# round4 function replacement
round4_pattern = r'func round4\(v \*float64\) \*float64 \{.*?\}'
new_round4 = '''func round4(v float64) float64 {
return math.Round(v*10000) / 10000
}

func roundPtr(v *float64) float64 {
if v == nil {
return 0.0
}
return round4(*v)
}'''
go_content = re.sub(round4_pattern, new_round4, go_content, flags=re.DOTALL)

# takeAuditSnapshot logic replacement
take_audit_pattern = r'func takeAuditSnapshot\(states \*sync\.Map\) \{.*?\n\s+if globalTCDCtrl != nil \{'
new_take_audit = '''func takeAuditSnapshot(states *sync.Map) {
log.Println("[Audit] takeAuditSnapshot triggered")
var te *telemetryEvent
var baselineMax, baselineMin, baselineDrift, baselineNoise float64
devCount := 0
nonNilCount := 0

states.Range(func(key, value interface{}) bool {
devCount++
st := value.(*deviceState)
st.mu.Lock()
if st.LastTelemetry != nil {
nonNilCount++
te = st.LastTelemetry

// Calculate baseline drift & noise from channel 1 session
if st.sessions != nil {
if sess, ok := st.sessions[1]; ok {
auditConfigMutex.Lock()
intervalMins := auditConfig.IntervalMins
auditConfigMutex.Unlock()

intervalSecs := float64(intervalMins) * 60.0
if sess.dtS > 0 && len(sess.values) > 0 {
pointsToConsider := int(intervalSecs / sess.dtS)
if pointsToConsider > len(sess.values) {
pointsToConsider = len(sess.values)
}
if pointsToConsider > 0 {
startIdx := len(sess.values) - pointsToConsider
subVals := sess.values[startIdx:]

maxVal := subVals[0]
minVal := subVals[0]
for _, v := range subVals {
if v > maxVal { maxVal = v }
if v < minVal { minVal = v }
}
drift := maxVal - minVal

var sum, sumSq float64
for _, v := range subVals {
sum += v
sumSq += v * v
}
mean := sum / float64(len(subVals))
variance := (sumSq / float64(len(subVals))) - (mean * mean)
noise := 0.0
if variance > 0 {
noise = math.Sqrt(variance)
}

baselineMax = maxVal
baselineMin = minVal
baselineDrift = drift
baselineNoise = noise
}
}
}
}
}
log.Printf("[Audit] Evaluated device %v, LastTelemetry is nil? %v\\n", key, st.LastTelemetry == nil)
st.mu.Unlock()
return te == nil // if found one, stop ranging
})

log.Printf("[Audit] Evaluated %d devices, %d had non-nil LastTelemetry\\n", devCount, nonNilCount)

snap := AuditSnapshot{
Timestamp: time.Now(),
}

if te != nil {
snap.TempBox = roundPtr(te.TempInj1)
snap.CarrierPsi = roundPtr(te.CarrierPsi)
snap.CarrierSccm = roundPtr(te.CarrierSccm)
snap.BaselineMax = round4(baselineMax)
snap.BaselineMin = round4(baselineMin)
snap.BaselineDrift = round4(baselineDrift)
snap.BaselineNoise = round4(baselineNoise)
}

if globalTCDCtrl != nil {'''
go_content = re.sub(take_audit_pattern, new_take_audit, go_content, flags=re.DOTALL)

with io.open(go_path, 'w', encoding='utf-8', newline='') as f:
    f.write(go_content)

# 2. Update audit.js
js_path = r'i:\GIT\VS2022\Chromatography-workstation\src\edge\cmd\collector\static\js\views\audit.js'
with io.open(js_path, 'r', encoding='utf-8') as f:
    js_content = f.read()

# Replace table headers
old_th = '''<th>时间</th>
                            <th>柱温()</th>
                            <th>进样1()</th>
                            <th>载气压力(psi)</th>
                            <th>载气流量(sccm)</th>'''
new_th = '''<th>时间</th>
                            <th>保温箱()</th>
                            <th>载气压力(psi)</th>
                            <th>载气流量(sccm)</th>'''
js_content = js_content.replace(old_th, new_th)

# Replace table rows
old_tr = '''<td></td>
                <td></td>
                <td></td>
                <td></td>
                <td></td>'''
new_tr = '''<td></td>
                <td></td>
                <td></td>
                <td></td>'''
js_content = js_content.replace(old_tr, new_tr)

# Fix null/undefined display to show 0 if they are 0
old_val = '''const val = (v) => v !== null && v !== undefined ? parseFloat(v).toFixed(2) : '-';
            const intVal = (v) => v !== null && v !== undefined ? v : '-';'''
new_val = '''const val = (v) => v !== null && v !== undefined ? parseFloat(v).toFixed(4) : '0.0000';
            const intVal = (v) => v !== null && v !== undefined ? v : '0';'''
js_content = js_content.replace(old_val, new_val)

with io.open(js_path, 'w', encoding='utf-8', newline='') as f:
    f.write(js_content)
