import os, re
index_file = r'i:\GIT\VS2022\Chromatography-workstation\src\edge\cmd\collector\static\index.html'
with open(index_file, 'r', encoding='utf-8') as f:
    content = f.read()

content = content.replace(
    '<div class=\"nav-item\" data-target=\"view-epc\">\n            <div class=\"nav-icon\"></div><div>EPC</div>\n        </div>',
    '<div class=\"nav-item\" data-target=\"view-epc\">\n            <div class=\"nav-icon\"></div><div>EPC</div>\n        </div>\n        <div class=\"nav-item\" data-target=\"view-audit\">\n            <div class=\"nav-icon\"></div><div>…Ûº∆</div>\n        </div>'
)

content = content.replace(
    '<div id=\"view-epc\" class=\"view-panel\"></div>',
    '<div id=\"view-epc\" class=\"view-panel\"></div>\n        <div id=\"view-audit\" class=\"view-panel\"></div>'
)

content = content.replace('v=0.3.140', 'v=0.3.141')

with open(index_file, 'w', encoding='utf-8') as f:
    f.write(content)
print('index.html modified.')
