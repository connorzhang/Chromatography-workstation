import os

files_to_replace = [
    r'i:\GIT\VS2022\Chromatography-workstation\src\edge\cmd\collector\static\index.html',
    r'i:\GIT\VS2022\Chromatography-workstation\src\edge\cmd\collector\static\js\app.js',
    r'i:\GIT\VS2022\Chromatography-workstation\src\edge\cmd\collector\bump.py',
    r'i:\GIT\VS2022\Chromatography-workstation\Makefile'
]

for filepath in files_to_replace:
    if not os.path.exists(filepath):
        print(f"Not found: {filepath}")
        continue
    with open(filepath, 'r', encoding='utf-8') as f:
        content = f.read()
    
    if '0.3.141' in content:
        content = content.replace('0.3.141', '0.3.142')
        with open(filepath, 'w', encoding='utf-8') as f:
            f.write(content)
        print(f"Updated {filepath} to 0.3.142")
    elif '0.3.140' in content:
        content = content.replace('0.3.140', '0.3.142')
        with open(filepath, 'w', encoding='utf-8') as f:
            f.write(content)
        print(f"Updated {filepath} to 0.3.142")

