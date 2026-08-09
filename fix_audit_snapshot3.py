import io
import re

# 1. Update audit_snapshot.go
go_path = r'i:\GIT\VS2022\Chromatography-workstation\src\edge\cmd\collector\audit_snapshot.go'
with io.open(go_path, 'r', encoding='utf-8') as f:
    go_content = f.read()

# Replace AuditSnapshot struct
old_struct = '''type AuditSnapshot struct {
Timestamp     time.Time json:"timestamp"
TempCol       *float64  json:"tempCol,omitempty"
TempInj1      *float64  json:"tempInj1,omitempty"
TempInj2      *float64  json:"tempInj2,omitempty"
TempDet1      *float64  json:"tempDet1,omitempty"
TempDet2      *float64  json:"tempDet2,omitempty"
TempDet3      *float64  json:"tempDet3,omitempty"
CarrierPsi    *float64  json:"carrierPsi,omitempty"
CarrierSccm   *float64  json:"carrierSccm,omitempty"
H2Psi         *float64  json:"h2Psi,omitempty"
H2Sccm        *float64  json:"h2Sccm,omitempty"
AirPsi        *float64  json:"airPsi,omitempty"
AirSccm       *float64  json:"airSccm,omitempty"
BridgeCurrent uint8     json:"bridgeCurrent"
BaselineMax   *float64  json:"baselineMax,omitempty"
BaselineMin   *float64  json:"baselineMin,omitempty"
BaselineDrift *float64  json:"baselineDrift,omitempty"
BaselineNoise *float64  json:"baselineNoise,omitempty"
}'''
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
}

func round4(v float64) float64 {
return math.Round(v*10000) / 10000
}

func roundPtr(v *float64) float64 {
if v == nil {
return 0.0
}
return round4(*v)
}'''
go_content = go_content.replace(old_struct, new_struct)

# Update variables in takeAuditSnapshot
go_content = go_content.replace('var baselineMax, baselineMin, baselineDrift, baselineNoise *float64', 'var baselineMax, baselineMin, baselineDrift, baselineNoise float64')

# Update assignments
go_content = go_content.replace('baselineMax = &maxVal', 'baselineMax = maxVal')
go_content = go_content.replace('baselineMin = &minVal', 'baselineMin = minVal')
go_content = go_content.replace('baselineDrift = &drift', 'baselineDrift = drift')
go_content = go_content.replace('baselineNoise = &noise', 'baselineNoise = noise')

# Replace snapshot instantiation
old_snap = '''snap := AuditSnapshot{
Timestamp:     time.Now(),
TempCol:       te.TempCol,
TempInj1:      te.TempInj1,
TempInj2:      te.TempInj2,
TempDet1:      te.TempDet1,
TempDet2:      te.TempDet2,
TempDet3:      te.TempDet3,
CarrierPsi:    te.CarrierPsi,
CarrierSccm:   te.CarrierSccm,
H2Psi:         te.H2Psi,
H2Sccm:        te.H2Sccm,
AirPsi:        te.AirPsi,
AirSccm:       te.AirSccm,
BaselineMax:   baselineMax,
BaselineMin:   baselineMin,
BaselineDrift: baselineDrift,
BaselineNoise: baselineNoise,
}'''
new_snap = '''snap := AuditSnapshot{
Timestamp:     time.Now(),
TempBox:       roundPtr(te.TempInj1),
CarrierPsi:    roundPtr(te.CarrierPsi),
CarrierSccm:   roundPtr(te.CarrierSccm),
BaselineMax:   round4(baselineMax),
BaselineMin:   round4(baselineMin),
BaselineDrift: round4(baselineDrift),
BaselineNoise: round4(baselineNoise),
}'''
go_content = go_content.replace(old_snap, new_snap)

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
