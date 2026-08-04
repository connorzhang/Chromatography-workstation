import re

filepath = 'src/edge/cmd/collector/auto_connect.go'
with open(filepath, 'r', encoding='utf-8') as f:
    content = f.read()

content = re.sub(
    r'tok, _ := appendSessionSamplesLocked\(st, 1, dtS, t0, pts\)\s*st\.mu\.Unlock\(\)\s*// Publish to realtime hub for UI plotting\s*hub\.Publish\(currentDevID, event\{',
    r'''tok, active := appendSessionSamplesLocked(st, 1, dtS, t0, pts)
\t\t\t\t\t\t\t\tst.mu.Unlock()
\t\t\t\t\t\t\t\tif active {
\t\t\t\t\t\t\t\t\thub.Publish(currentDevID, event{''',
    content
)

content = re.sub(
    r'(SessionToken: tok,\s*DTs:\s*dtS,\s*T0s:\s*t0,\s*Values:\s*pts\}\)\s*)\}',
    r'\1\t\t\t\t\t\t\t\t}\n\t\t\t\t\t\t\t}',
    content
)

with open(filepath, 'w', encoding='utf-8') as f:
    f.write(content)
print('Replaced auto_connect.go TCD')
