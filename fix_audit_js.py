import io
import re

js_path = r'i:\GIT\VS2022\Chromatography-workstation\src\edge\cmd\collector\static\js\views\audit.js'
with io.open(js_path, 'r', encoding='utf-8') as f:
    js_content = f.read()

# Replace the specific TH lines
js_content = re.sub(r'<th>时间</th>\s*<th>柱温\(\)</th>\s*<th>进样1\(\)</th>', '<th>时间</th>\n                            <th>保温箱()</th>', js_content)

with io.open(js_path, 'w', encoding='utf-8', newline='') as f:
    f.write(js_content)
