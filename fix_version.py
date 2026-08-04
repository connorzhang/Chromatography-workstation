import os
filepath = r'i:\GIT\VS2022\Chromatography-workstation\src\edge\cmd\collector\main.go'
with open(filepath, 'r', encoding='utf-8') as f:
    content = f.read()

if 'v0.3.141' in content:
    content = content.replace('v0.3.141', 'v0.3.142')
    with open(filepath, 'w', encoding='utf-8') as f:
        f.write(content)
    print("Version in main.go updated to v0.3.142.")
else:
    print("v0.3.141 not found in main.go.")
