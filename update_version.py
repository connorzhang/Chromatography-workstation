import os

def replace_in_file(filepath, old_str, new_str):
    with open(filepath, 'rb') as f:
        content = f.read().decode('utf-8')
    content = content.replace(old_str, new_str)
    with open(filepath, 'wb') as f:
        f.write(content.encode('utf-8'))
    print(f'Updated {filepath}')

main_go = r'i:\GIT\VS2022\Chromatography-workstation\src\edge\cmd\collector\main.go'
tcd_js = r'i:\GIT\VS2022\Chromatography-workstation\src\edge\cmd\collector\static\js\views\tcd.js'
live_js = r'i:\GIT\VS2022\Chromatography-workstation\src\edge\cmd\collector\static\js\views\live.js'
method_run = r'i:\GIT\VS2022\Chromatography-workstation\src\ui\apps\workstation\src\pages\MethodRun.tsx'

replace_in_file(main_go, 'const AppVersion = "v0.3.128"', 'const AppVersion = "v0.3.129"')
replace_in_file(tcd_js, '500000', '5000000')
replace_in_file(live_js, '500000', '5000000')
replace_in_file(method_run, '500000', '5000000')
