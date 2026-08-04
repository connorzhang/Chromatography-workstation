import re
filepath = r'i:\GIT\VS2022\Chromatography-workstation\src\edge\cmd\collector\main.go'
with open(filepath, 'r', encoding='utf-8') as f:
    content = f.read()
content = content.replace('const AppVersion = "v0.3.143"', 'const AppVersion = "v0.3.144"')
with open(filepath, 'w', encoding='utf-8') as f:
    f.write(content)
print("Version updated to v0.3.144")
