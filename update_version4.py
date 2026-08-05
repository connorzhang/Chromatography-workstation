filepath = r'i:\GIT\VS2022\Chromatography-workstation\src\edge\cmd\collector\main.go'

with open(filepath, 'r', encoding='gbk', errors='ignore') as f:
    content = f.read()

content = content.replace('const AppVersion = "v0.3.145"', 'const AppVersion = "v0.3.146"')

with open(filepath, 'w', encoding='gbk') as f:
    f.write(content)

print("Version updated to v0.3.146")