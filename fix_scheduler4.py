import re

file_path = r'i:\GIT\VS2022\Chromatography-workstation\src\edge\cmd\collector\engine_scheduler.go'
with open(file_path, 'r', encoding='utf-8') as f:
    content = f.read()

if '"strings"' not in content:
    content = content.replace('import (', 'import (\n\t"strings"\n', 1)

with open(file_path, 'w', encoding='utf-8') as f:
    f.write(content)
print('Done')
