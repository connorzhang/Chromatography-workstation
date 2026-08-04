import re

file_path = r'i:\GIT\VS2022\Chromatography-workstation\src\edge\cmd\collector\static\js\views\live.js'
with open(file_path, 'r', encoding='utf-8') as f:
    content = f.read()

old_str = 'if (baseT === 0 || dataPoints.length > 50000000000) {'
new_str = '''let sessionChanged = false;
                if (parsed.sessionToken) {
                    if (window.currentSessionToken !== undefined && window.currentSessionToken !== parsed.sessionToken) {
                        sessionChanged = true;
                    }
                    window.currentSessionToken = parsed.sessionToken;
                }

                if (baseT === 0 || sessionChanged || dataPoints.length > 50000000000) {'''

if old_str in content:
    content = content.replace(old_str, new_str)
    with open(file_path, 'w', encoding='utf-8') as f:
        f.write(content)
    print('live.js successfully updated')
else:
    print('old_str not found in live.js')
