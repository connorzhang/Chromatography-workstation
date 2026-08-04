import re

file_path = r'i:\GIT\VS2022\Chromatography-workstation\src\edge\cmd\collector\engine_scheduler.go'
with open(file_path, 'r', encoding='utf-8') as f:
    content = f.read()

content = content.replace('if pstore != nil && pstore.LoadSysConfig().DriverMode == "modular" && isLooping2 {', 'isModularDevice := strings.HasPrefix(deviceID, "GC-MODULAR") || (pstore != nil && pstore.LoadSysConfig().DriverMode == "modular")\n\t\t\t\t\t\tif isModularDevice && isLooping2 {')

with open(file_path, 'w', encoding='utf-8') as f:
    f.write(content)
print('Done')
