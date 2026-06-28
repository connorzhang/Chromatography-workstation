import os
path = r'd:\GIT\VS2022\Chromatography-workstation\src\edge\cmd\collector\static\js\views\settings.js'
with open(path, 'r', encoding='utf-8') as f:
    s = f.read()

s = s.replace('\\`', '`').replace('\\${', '${')

with open(path, 'w', encoding='utf-8') as f:
    f.write(s)
print("Done")