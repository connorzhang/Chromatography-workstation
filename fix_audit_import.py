import io
path = r'i:\GIT\VS2022\Chromatography-workstation\src\edge\cmd\collector\audit_snapshot.go'
with io.open(path, 'r', encoding='utf-8') as f:
    content = f.read()

content = content.replace('\t"chromatography-workstation/edge/internal/publisher"\n', '')

with io.open(path, 'w', encoding='utf-8', newline='') as f:
    f.write(content)
