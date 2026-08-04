import os
import re

filepath = r'i:\GIT\VS2022\Chromatography-workstation\src\edge\cmd\collector\audit_snapshot.go'
with open(filepath, 'r', encoding='utf-8') as f:
    content = f.read()

new_take = '''func takeAuditSnapshot(states *sync.Map) {
log.Println("[Audit] takeAuditSnapshot triggered")
var te *telemetryEvent
devCount := 0
nonNilCount := 0

states.Range(func(key, value interface{}) bool {
devCount++
st := value.(*deviceState)
st.mu.Lock()
if st.LastTelemetry != nil {
nonNilCount++
te = st.LastTelemetry
}
log.Printf("[Audit] Evaluated device %v, LastTelemetry is nil? %v\n", key, st.LastTelemetry == nil)
st.mu.Unlock()
return te == nil // if found one, stop ranging
})

log.Printf("[Audit] Evaluated %d devices, %d had non-nil LastTelemetry\n", devCount, nonNilCount)

if te == nil {
log.Println("[Audit] te is nil, no snapshot taken")
return
}'''

content = re.sub(r'func takeAuditSnapshot\(states \*sync\.Map\) \{[\s\S]*?log\.Println\("\[Audit\] te is nil, no snapshot taken"\)\s+return\s+\}', new_take, content)

with open(filepath, 'w', encoding='utf-8') as f:
    f.write(content)
print("audit_snapshot.go updated with debug4.")
