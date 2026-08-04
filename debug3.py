import os
import re

filepath = r'i:\GIT\VS2022\Chromatography-workstation\src\edge\cmd\collector\main.go'
with open(filepath, 'r', encoding='utf-8') as f:
    content = f.read()

content = content.replace('st.LastTelemetry = &te\n\t\t\thub.Publish(f.DeviceID, te)', 'st.LastTelemetry = &te\n\t\t\tlog.Println("[Debug] LastTelemetry updated for 143")\n\t\t\thub.Publish(f.DeviceID, te)')
content = content.replace('st.LastTelemetry = &te\n\t\t\t\thub.Publish(f.DeviceID, te)', 'st.LastTelemetry = &te\n\t\t\t\tlog.Println("[Debug] LastTelemetry updated for 128")\n\t\t\t\thub.Publish(f.DeviceID, te)')

with open(filepath, 'w', encoding='utf-8') as f:
    f.write(content)
