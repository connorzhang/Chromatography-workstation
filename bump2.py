import os

def replace_in_file(filepath, old, new):
    if not os.path.exists(filepath): return
    with open(filepath, 'r', encoding='utf-8') as f:
        content = f.read()
    if old in content:
        with open(filepath, 'w', encoding='utf-8') as f:
            f.write(content.replace(old, new))
        print(f'Replaced in {filepath}')

replace_in_file('src/edge/cmd/collector/static/index.html', 'v=0.3.139', 'v=0.3.140')
replace_in_file('src/edge/cmd/collector/main.go', 'v0.3.139', 'v0.3.140')
replace_in_file('src/edge/cmd/collector/static/js/app.js', 'v=0.3.139', 'v=0.3.140')
replace_in_file('src/edge/cmd/collector/buildver.txt', '0.3.139', '0.3.140')
replace_in_file('src/edge/cmd/collector/Makefile', '0.3.139', '0.3.140')
