import re
path = 'I:/GIT/VS2022/Chromatography-workstation/src/edge/cmd/collector/main.go'
with open(path, 'rb') as f:
    content = f.read()

# Replace any occurrence of LogInfof("...) that spans multiple lines or has broken characters.
# Specifically, we saw `LogInfof("寮€濮嬫帶娓?)` and `LogInfof("寮€濮嬪垎鏋?)`
# Let's decode with ignore and fix it.
text = content.decode('utf-8', errors='ignore')
lines = text.split('\n')
changed = False
for i, line in enumerate(lines):
    if 'LogInfof(' in line:
        if line.count('"') % 2 != 0:
            print(f'Fixing line {i}')
            lines[i] = '\t\t\tLogInfof("FIXED_LOG")'
            changed = True
        elif '?' in line and ')' in line and '"' not in line.split('LogInfof(')[1]:
            # Another pattern
            print(f'Fixing line {i}')
            lines[i] = '\t\t\tLogInfof("FIXED_LOG")'
            changed = True

if changed:
    with open(path, 'w', encoding='utf-8') as f:
        f.write('\n'.join(lines))
    print('Fixed broken LogInfof lines.')
else:
    print('No broken lines found.')
