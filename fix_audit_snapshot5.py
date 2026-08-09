import io
import re

path = r'i:\GIT\VS2022\Chromatography-workstation\src\edge\cmd\collector\audit_snapshot.go'
with io.open(path, 'r', encoding='utf-8') as f:
    content = f.read()

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
content = re.sub(struct_pattern, new_struct, content, flags=re.DOTALL)

# Update variables in takeAuditSnapshot
content = content.replace('var baselineMax, baselineMin, baselineDrift, baselineNoise *float64', 'var baselineMax, baselineMin, baselineDrift, baselineNoise float64')

# Update assignments
content = content.replace('baselineMax = &maxVal', 'baselineMax = maxVal')
content = content.replace('baselineMin = &minVal', 'baselineMin = minVal')
content = content.replace('baselineDrift = &drift', 'baselineDrift = drift')
content = content.replace('baselineNoise = &noise', 'baselineNoise = noise')

with io.open(path, 'w', encoding='utf-8', newline='') as f:
    f.write(content)

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
