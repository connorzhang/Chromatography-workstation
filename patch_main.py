import re

with open(r'i:\GIT\VS2022\Chromatography-workstation\src\edge\cmd\collector\main.go', 'r', encoding='utf-8') as f:
    content = f.read()

old_str = '''	if f.Cmd != 143 && f.Cmd != 159 && f.Cmd != 128 {

		LogDebugf("Received Cmd %d, Payload len: %d, Payload: %X", f.Cmd, len(f.Payload), f.Payload)

	}'''

new_str = '''	if f.Cmd != 143 && f.Cmd != 159 && f.Cmd != 128 {
		LogDebugf("Received Cmd %d, Payload len: %d, Payload: %X", f.Cmd, len(f.Payload), f.Payload)
	} else {
		LogInfof("[Debug] Received Cmd %d from %s", f.Cmd, f.DeviceID)
	}'''

content = content.replace(old_str, new_str)

with open(r'i:\GIT\VS2022\Chromatography-workstation\src\edge\cmd\collector\main.go', 'w', encoding='utf-8') as f:
    f.write(content)

print("Patched main.go")
