import re

file_path = r'i:\GIT\VS2022\Chromatography-workstation\src\edge\cmd\collector\engine_scheduler.go'
with open(file_path, 'r', encoding='utf-8') as f:
    content = f.read()

content = content.replace('import (\n\t"strings"\n', 'import (\n')

with open(file_path, 'w', encoding='utf-8') as f:
    f.write(content)
print('engine_scheduler.go unused import removed')
