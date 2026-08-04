import re

files = [
    r'i:\GIT\VS2022\Chromatography-workstation\src\edge\cmd\collector\static\js\app.js',
    r'i:\GIT\VS2022\Chromatography-workstation\src\edge\cmd\collector\static\index.html'
]

for file_path in files:
    with open(file_path, 'r', encoding='utf-8') as f:
        content = f.read()
    
    content = re.sub(r'\?v=0\.3\.\d+', '?v=0.3.137', content)
    
    with open(file_path, 'w', encoding='utf-8') as f:
        f.write(content)
    print(f'{file_path} cache buster updated to v0.3.137')
