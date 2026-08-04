import os
import re

filepath = r'i:\GIT\VS2022\Chromatography-workstation\src\edge\cmd\collector\audit_snapshot.go'
with open(filepath, 'r', encoding='utf-8') as f:
    content = f.read()

new_take = '''func takeAuditSnapshot(states *sync.Map) {
log.Println("[Audit] takeAuditSnapshot triggered")
var te *telemetryEvent

states.Range(func(key, value interface{}) bool {
st := value.(*deviceState)
st.mu.Lock()
if st.LastTelemetry != nil {
te = st.LastTelemetry
}
st.mu.Unlock()
return te == nil // if found one, stop ranging
})

if te == nil {
log.Println("[Audit] te is nil, no snapshot taken")
return
}

snap := AuditSnapshot{
Timestamp:   time.Now(),
TempCol:     te.TempCol,
TempInj1:    te.TempInj1,
TempInj2:    te.TempInj2,
TempDet1:    te.TempDet1,
TempDet2:    te.TempDet2,
TempDet3:    te.TempDet3,
CarrierPsi:  te.CarrierPsi,
CarrierSccm: te.CarrierSccm,
H2Psi:       te.H2Psi,
H2Sccm:      te.H2Sccm,
AirPsi:      te.AirPsi,
AirSccm:     te.AirSccm,
}

if globalTCDCtrl != nil {
tcdState := globalTCDCtrl.GetState()
snap.BridgeCurrent = tcdState.BridgeCurrent
}

auditHistoryMutex.Lock()
auditHistory = append(auditHistory, snap)
if len(auditHistory) > 10000 {
auditHistory = auditHistory[len(auditHistory)-10000:]
}
saveAuditHistory()
auditHistoryMutex.Unlock()

log.Println("[Audit] Snapshot taken successfully at", snap.Timestamp)
}'''

content = re.sub(r'func takeAuditSnapshot\(states \*sync\.Map\) \{[\s\S]*?log\.Println\("\[Audit\] Snapshot taken at", snap\.Timestamp\)\s+\}', new_take, content)

with open(filepath, 'w', encoding='utf-8') as f:
    f.write(content)
print("audit_snapshot.go updated with logs.")
