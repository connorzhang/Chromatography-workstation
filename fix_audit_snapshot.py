import os
import re

filepath = r'i:\GIT\VS2022\Chromatography-workstation\src\edge\cmd\collector\audit_snapshot.go'
with open(filepath, 'r', encoding='utf-8') as f:
    content = f.read()

content = re.sub(r'go func\(\) \{\s+for \{\s+select \{\s+case <-auditRoutineDone:\s+return\s+case <-auditRoutineTicker\.C:\s+takeAuditSnapshot\(states\)\s+\}\s+\}\s+\}\(\)', 
r'''go func() {
takeAuditSnapshot(states)
for {
select {
case <-auditRoutineDone:
return
case <-auditRoutineTicker.C:
takeAuditSnapshot(states)
}
}
}()''', content)

with open(filepath, 'w', encoding='utf-8') as f:
    f.write(content)
print("audit_snapshot.go updated successfully.")
