import os, re
audit_file = r'i:\GIT\VS2022\Chromatography-workstation\src\edge\cmd\collector\audit_snapshot.go'
with open(audit_file, 'r', encoding='utf-8') as f:
    content = f.read()

correct_struct = """type AuditSnapshot struct {
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
}"""

content = re.sub(r'type AuditSnapshot struct \{[\s\S]*?\}', correct_struct, content)
with open(audit_file, 'w', encoding='utf-8') as f:
    f.write(content)
print('Regex replace done.')
