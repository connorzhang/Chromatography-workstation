import io
import re

path = r'i:\GIT\VS2022\Chromatography-workstation\src\edge\cmd\collector\audit_snapshot.go'
with io.open(path, 'r', encoding='utf-8') as f:
    content = f.read()

# Replace snapshot instantiation using regex
snap_pattern = r'snap := AuditSnapshot\{.*?\n\t\}'
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
content = re.sub(snap_pattern, new_snap, content, flags=re.DOTALL)

with io.open(path, 'w', encoding='utf-8', newline='') as f:
    f.write(content)
