import io
import re

path = r'i:\GIT\VS2022\Chromatography-workstation\src\edge\cmd\collector\audit_snapshot.go'
with io.open(path, 'r', encoding='utf-8') as f:
    content = f.read()

# Fix the backticks around JSON tags
content = re.sub(r'type AuditSnapshot struct \{.*?\}', r'''type AuditSnapshot struct {
Timestamp     time.Time json:"timestamp"
TempBox       float64   json:"tempBox"
TempInj1      float64   json:"tempInj1,omitempty" // For backward compatibility
CarrierPsi    float64   json:"carrierPsi"
CarrierSccm   float64   json:"carrierSccm"
BridgeCurrent uint8     json:"bridgeCurrent"
BaselineMax   float64   json:"baselineMax"
BaselineMin   float64   json:"baselineMin"
BaselineDrift float64   json:"baselineDrift"
BaselineNoise float64   json:"baselineNoise"
}''', content, flags=re.DOTALL)

with io.open(path, 'w', encoding='utf-8', newline='') as f:
    f.write(content)
