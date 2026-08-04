import os

filepath = r'i:\GIT\VS2022\Chromatography-workstation\src\edge\cmd\collector\audit_snapshot.go'
with open(filepath, 'r', encoding='utf-8') as f:
    content = f.read()

content = content.replace('LastTelemetry\n", devCount', r'LastTelemetry\n", devCount')

with open(filepath, 'w', encoding='utf-8') as f:
    f.write(content)
