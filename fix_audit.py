import re

with open(r'i:\GIT\VS2022\Chromatography-workstation\src\edge\cmd\collector\audit_snapshot.go', 'r', encoding='utf-8') as f:
    c = f.read()

c = c.replace('mqttClient.PublishInfo(devID, map[string]any{', 'mqttClient.PublishAudit(devID, map[string]any{')

with open(r'i:\GIT\VS2022\Chromatography-workstation\src\edge\cmd\collector\audit_snapshot.go', 'w', encoding='utf-8') as f:
    f.write(c)