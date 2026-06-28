import os

d = r'D:\GIT\VS2022\Chromatography-workstation\src\ui\apps\workstation\src\pages'
for f in os.listdir(d):
    if f.endswith('.tsx'):
        p = os.path.join(d, f)
        with open(p, 'r', encoding='utf-8') as file:
            c = file.read()
        
        c = c.replace("\\'react-i18next\\'", "'react-i18next'")
        
        with open(p, 'w', encoding='utf-8') as file:
            file.write(c)
