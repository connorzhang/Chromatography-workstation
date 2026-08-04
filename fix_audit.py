import re

filepath = r'i:\GIT\VS2022\Chromatography-workstation\src\edge\cmd\collector\audit_snapshot.go'

with open(filepath, 'r', encoding='utf-8') as f:
    content = f.read()

content = re.sub(r'// We proceed even if te is nil, just to record a snapshot with missing data\s*if te == nil \{\s*te = &telemetryEvent\{\}\s*\}', '''if te == nil {
log.Println("[Audit] te is nil, no snapshot taken")
return
}''', content)

with open(filepath, 'w', encoding='utf-8') as f:
    f.write(content)

print("audit_snapshot.go patched")
