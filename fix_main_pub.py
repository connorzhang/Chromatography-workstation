import re

filepath = 'src/edge/cmd/collector/main.go'
with open(filepath, 'r', encoding='utf-8') as f:
    content = f.read()

content = re.sub(
    r'tok, _ := appendSessionSamplesLocked\(st, parsed\.Channel, dtS, t0, parsed\.Values\)\s*st\.mu\.Unlock\(\)\s*hub\.Publish\(f\.DeviceID, event\{Type: "samples"',
    r'''tok, active := appendSessionSamplesLocked(st, parsed.Channel, dtS, t0, parsed.Values)
\t\tst.mu.Unlock()

\t\tif active {
\t\t\thub.Publish(f.DeviceID, event{Type: "samples"''',
    content
)

content = re.sub(
    r'(Values: parsed\.Values\}\)\s*)\}',
    r'\1\t\t}\n\t}',
    content
)

with open(filepath, 'w', encoding='utf-8') as f:
    f.write(content)
print('Replaced main.go FID')
