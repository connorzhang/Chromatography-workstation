import os
import re

filepath = r'src/edge/cmd/collector/auto_connect.go'
with open(filepath, 'r', encoding='utf-8') as f:
    content = f.read()

old_pattern = r'// Apply persisted TCD Bridge Current.*?\}\(hwCfg\.TCDBridgeCurrent\)\s*\}'

new_str = '''// On TCD re-connect, safely set Bridge Current to 0 to prevent overheating
// Send it multiple times to ensure success
go func() {
time.Sleep(1 * time.Second)
for i := 0; i < 3; i++ {
_ = tCtrl.SetBridgeCurrent(0)
time.Sleep(500 * time.Millisecond)
}
fmt.Printf("[AutoConnect] Safely reset TCD Bridge Current to 0 upon reconnection\\n")

// Update persisted config so UI reflects the 0 state
hwCfg, _ := pstore.LoadHardwareConfig(modDevID)
hwCfg.TCDBridgeCurrent = 0
pstore.SaveHardwareConfig(modDevID, hwCfg)
}()'''

if re.search(old_pattern, content, re.DOTALL):
    content = re.sub(old_pattern, new_str, content, flags=re.DOTALL)
    with open(filepath, 'w', encoding='utf-8') as f:
        f.write(content)
    print('Replaced successfully')
else:
    print('Pattern not found. Debug:')
    idx = content.find('Apply persisted TCD Bridge Current')
    if idx != -1:
        print(repr(content[idx:idx+200]))
