import re

file_path = r'i:\GIT\VS2022\Chromatography-workstation\src\edge\cmd\collector\engine_scheduler.go'
with open(file_path, 'r', encoding='utf-8') as f:
    content = f.read()

old_str = '''// HOWEVER, if we are in Modular Driver mode, there is NO MAINBOARD!
// We MUST handle the loop/cycle interval locally.
hw2, _ := pstore.LoadHardwareConfig(deviceID)
isLooping2 := ui.Loop || hw2.CycleCount > 1
if pstore != nil && pstore.LoadSysConfig().DriverMode == "modular" && isLooping2 {
hw, _ := pstore.LoadHardwareConfig(deviceID)'''

new_str = '''// HOWEVER, if we are in Modular Driver mode OR this is a modular device, there is NO MAINBOARD!
// We MUST handle the loop/cycle interval locally.
hw2, _ := pstore.LoadHardwareConfig(deviceID)
isLooping2 := ui.Loop || hw2.CycleCount > 1
isModularDevice := strings.HasPrefix(deviceID, "GC-MODULAR") || (pstore != nil && pstore.LoadSysConfig().DriverMode == "modular")
if isModularDevice && isLooping2 {
hw, _ := pstore.LoadHardwareConfig(deviceID)'''

if old_str in content:
    content = content.replace(old_str, new_str)
    with open(file_path, 'w', encoding='utf-8') as f:
        f.write(content)
    print('engine_scheduler.go successfully updated')
else:
    print('old_str not found in engine_scheduler.go')
