import re

with open(r'i:\GIT\VS2022\Chromatography-workstation\src\edge\cmd\collector\static\js\views\audit.js', 'r', encoding='utf-8') as f:
    content = f.read()

# Add table headers
new_th = """                            <th>桥流(mA)</th>
                            <th>基线最大值(mV)</th>
                            <th>基线最小值(mV)</th>
                            <th>基线漂移(mV)</th>
                            <th>基线噪声(mV)</th>"""
content = re.sub(r'<th>桥流\(mA\)</th>', new_th, content)

# Add table cells
new_td = """                <td>${intVal(snap.bridgeCurrent)}</td>
                <td>${val(snap.baselineMax)}</td>
                <td>${val(snap.baselineMin)}</td>
                <td>${val(snap.baselineDrift)}</td>
                <td>${val(snap.baselineNoise)}</td>"""
content = re.sub(r'<td>\$\{intVal\(snap\.bridgeCurrent\)\}</td>', new_td, content)

with open(r'i:\GIT\VS2022\Chromatography-workstation\src\edge\cmd\collector\static\js\views\audit.js', 'w', encoding='utf-8') as f:
    f.write(content)
