with open(r'i:\GIT\VS2022\Chromatography-workstation\src\edge\cmd\collector\audit_snapshot.go', 'r', encoding='utf-8') as f:
    content = f.read()

content = content.replace('LastTelemetry is nil? %v\n"', 'LastTelemetry is nil? %v\\n"')
content = content.replace('non-nil LastTelemetry\n"', 'non-nil LastTelemetry\\n"')

with open(r'i:\GIT\VS2022\Chromatography-workstation\src\edge\cmd\collector\audit_snapshot.go', 'w', encoding='utf-8') as f:
    f.write(content)
