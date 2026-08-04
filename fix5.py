import os
audit_file = r'i:\GIT\VS2022\Chromatography-workstation\src\edge\cmd\collector\audit_snapshot.go'
with open(audit_file, 'r', encoding='utf-8') as f:
    content = f.read()

content = content.replace('http.BadRequest', 'http.StatusBadRequest')
content = content.replace('\"status\":\"ek\"', '\"status\":\"ok\"')

with open(audit_file, 'w', encoding='utf-8') as f:
    f.write(content)
print('Regex replace done.')
