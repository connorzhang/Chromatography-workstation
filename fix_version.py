import re

file_path = r'i:\GIT\VS2022\Chromatography-workstation\src\edge\cmd\collector\main.go'
with open(file_path, 'r', encoding='utf-8') as f:
    content = f.read()

content = re.sub(r'AppVersion = "v0\.3\.\d+"', 'AppVersion = "v0.3.137"', content)

with open(file_path, 'w', encoding='utf-8') as f:
    f.write(content)
print('main.go version updated to v0.3.137')
