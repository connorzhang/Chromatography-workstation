import re

file_path = r'i:\GIT\VS2022\Chromatography-workstation\src\edge\cmd\collector\static\js\views\settings.js'
with open(file_path, 'r', encoding='utf-8') as f:
    content = f.read()

content = content.replace("const h3 = eventsBlock.querySelector('h3');", "const eventsBlock = document.getElementById('settings-events-block');\n                            if (eventsBlock) {\n                                const h3 = eventsBlock.querySelector('h3');\n                                if (h3) h3.innerText = '外部事件 (CH5-8 开关量)';\n                            }\n")

content = content.replace("if (h3) h3.innerText = '外部事件 (CH5-8 开关量)';\n                        }", "}")

with open(file_path, 'w', encoding='utf-8') as f:
    f.write(content)
print('settings.js fixed')
