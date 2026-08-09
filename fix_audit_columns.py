import re

with open(r'i:\GIT\VS2022\Chromatography-workstation\src\edge\cmd\collector\static\js\views\audit.js', 'r', encoding='utf-8') as f:
    content = f.read()

# Update table headers
new_th = """                            <th>时间</th>
                            <th>柱温(℃)</th>
                            <th>进样1(℃)</th>
                            <th>载气压力(psi)</th>
                            <th>载气流量(sccm)</th>
                            <th>桥流(mA)</th>
                            <th>基线最大值(mV)</th>
                            <th>基线最小值(mV)</th>
                            <th>基线漂移(mV)</th>
                            <th>基线噪声(mV)</th>"""
content = re.sub(r'<th>时间</th>.*?<th>基线噪声\(mV\)</th>', new_th, content, flags=re.DOTALL)

# Update table body cells
new_td = """                <td>${timeStr}</td>
                <td>${val(snap.tempCol)}</td>
                <td>${val(snap.tempInj1)}</td>
                <td>${val(snap.carrierPsi)}</td>
                <td>${val(snap.carrierSccm)}</td>
                <td>${intVal(snap.bridgeCurrent)}</td>
                <td>${val(snap.baselineMax)}</td>
                <td>${val(snap.baselineMin)}</td>
                <td>${val(snap.baselineDrift)}</td>
                <td>${val(snap.baselineNoise)}</td>"""
content = re.sub(r'<td>\$\{timeStr\}</td>.*?<td>\$\{val\(snap\.baselineNoise\)\}</td>', new_td, content, flags=re.DOTALL)

with open(r'i:\GIT\VS2022\Chromatography-workstation\src\edge\cmd\collector\static\js\views\audit.js', 'w', encoding='utf-8') as f:
    f.write(content)
