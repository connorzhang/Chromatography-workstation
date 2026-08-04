import os
import re

filepath = r'i:\GIT\VS2022\Chromatography-workstation\src\edge\cmd\collector\main.go'
with open(filepath, 'r', encoding='utf-8') as f:
    content = f.read()

content = content.replace('if te, ok := parseTemps143(f.Payload); ok {', 'if te, ok := parseTemps143(f.Payload); ok {\n\t\t\tlog.Println("[Debug] Received 143 frame and parsed successfully")')

with open(filepath, 'w', encoding='utf-8') as f:
    f.write(content)
