import io
import re

path = r'i:\GIT\VS2022\Chromatography-workstation\src\edge\cmd\collector\audit_snapshot.go'
with io.open(path, 'r', encoding='utf-8') as f:
    content = f.read()

# 1. Update struct
struct_pattern = r'type AuditSnapshot struct \{.*?\}'
new_struct = '''type AuditSnapshot struct {
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
}'''
content = re.sub(struct_pattern, new_struct, content, flags=re.DOTALL)

# 2. Update loadAuditHistory
load_pattern = r'func loadAuditHistory\(\) \{.*?\n\}'
new_load = '''func loadAuditHistory() {
auditHistoryMutex.Lock()
defer auditHistoryMutex.Unlock()

auditHistory = []AuditSnapshot{}
data, err := ioutil.ReadFile(auditHistoryFile)
if err == nil {
json.Unmarshal(data, &auditHistory)
for i := range auditHistory {
if auditHistory[i].TempBox == 0 && auditHistory[i].TempInj1 != 0 {
auditHistory[i].TempBox = auditHistory[i].TempInj1
}
// Clear TempInj1 so it doesn't stay in memory/future saves if we don't want it,
// but keeping it is harmless because of omitempty if it's 0.
auditHistory[i].TempInj1 = 0
}
}
}'''
content = re.sub(load_pattern, new_load, content, flags=re.DOTALL)

with io.open(path, 'w', encoding='utf-8', newline='') as f:
    f.write(content)
