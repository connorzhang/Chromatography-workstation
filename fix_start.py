import re

filepath = 'src/edge/cmd/collector/main.go'
with open(filepath, 'r', encoding='utf-8') as f:
    content = f.read()

content = re.sub(r'(case "startAll":\s*err = driver\.StartAnalysis\(0xFF\).*?\s*mappedCmd = 18)', r'\1\n\t\t\t\tresetAllSessions(st)', content, flags=re.DOTALL)

content = re.sub(r'(case "start":\s*err = driver\.StartAnalysis\(byte\(ch\)\).*?\s*mappedCmd = 22)', r'\1\n\t\t\t\tresetSession(st, ch)', content, flags=re.DOTALL)

with open(filepath, 'w', encoding='utf-8') as f:
    f.write(content)
print('Regex replace done.')
