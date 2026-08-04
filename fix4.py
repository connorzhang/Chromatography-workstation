import os, re
audit_file = r'i:\GIT\VS2022\Chromatography-workstation\src\edge\cmd\collector\audit_snapshot.go'
with open(audit_file, 'r', encoding='utf-8') as f:
    content = f.read()

correct_struct = """type AuditSnapshot struct {
Timestamp     time.Time {B}json:"timestamp"{B}
TempCol       *float64  {B}json:"tempCol,omitempty"{B}
TempInj1      *float64  {B}json:"tempInj1,omitempty"{B}
TempInj2      *float64  {B}json:"tempInj2,omitempty"{B}
TempDet1      *float64  {B}json:"tempDet1,omitempty"{B}
TempDet2      *float64  {B}json:"tempDet2,omitempty"{B}
TempDet3      *float64  {B}json:"tempDet3,omitempty"{B}
CarrierPsi    *float64  {B}json:"carrierPsi,omitempty"{B}
CarrierSccm   *float64  {B}json:"carrierSccm,omitempty"{B}
H2Psi         *float64  {B}json:"h2Psi,omitempty"{B}
H2Sccm        *float64  {B}json:"h2Sccm,omitempty"{B}
AirPsi        *float64  {B}json:"airPsi,omitempty"{B}
AirSccm       *float64  {B}json:"airSccm,omitempty"{B}
BridgeCurrent uint8     {B}json:"bridgeCurrent"{B}
}""".replace('{B}', chr(96))

content = re.sub(r'type AuditSnapshot struct \{[\s\S]*?\}', correct_struct, content)
with open(audit_file, 'w', encoding='utf-8') as f:
    f.write(content)
print('Regex replace done.')
