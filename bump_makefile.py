import os
filepath = r'i:\GIT\VS2022\Chromatography-workstation\src\edge\Makefile'
if os.path.exists(filepath):
    with open(filepath, 'r', encoding='utf-8') as f:
        content = f.read()
    if '0.3.141' in content:
        content = content.replace('0.3.141', '0.3.142')
    elif '0.3.140' in content:
        content = content.replace('0.3.140', '0.3.142')
    with open(filepath, 'w', encoding='utf-8') as f:
        f.write(content)
    print("Makefile updated.")
