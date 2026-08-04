import os
main_file = r'i:\GIT\VS2022\Chromatography-workstation\src\edge\cmd\collector\main.go'
with open(main_file, 'r', encoding='utf-8') as f:
    content = f.read()
content = content.replace('st.LastTelemetry = te', 'st.LastTelemetry = &te')
with open(main_file, 'w', encoding='utf-8') as f:
    f.write(content)

audit_file = r'i:\GIT\VS2022\Chromatography-workstation\src\edge\cmd\collector\audit_snapshot.go'
with open(audit_file, 'r', encoding='utf-8') as f:
    content = f.read()
content = content.replace('BridgeCurrent uint8     json:\"bridgeCurrent\"c\n}', 'BridgeCurrent uint8     json:\"bridgeCurrent\"\n}')
with open(audit_file, 'w', encoding='utf-8') as f:
    f.write(content)
print('Python modification done.')
