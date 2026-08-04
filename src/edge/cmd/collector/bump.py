import os

def replace_in_file(filepath, old, new):
    if not os.path.exists(filepath): return
    with open(filepath, 'r', encoding='utf-8') as f:
        content = f.read()
    if old in content:
        with open(filepath, 'w', encoding='utf-8') as f:
            f.write(content.replace(old, new))
        print(f'Replaced in {filepath}')

replace_in_file('../../../../buildver.txt', '0.3.140', '0.3.142')
replace_in_file('../../../../Makefile', '0.3.140', '0.3.142')
