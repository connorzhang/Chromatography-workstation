import re

with open(r'i:\GIT\VS2022\Chromatography-workstation\src\edge\internal\telemetry\mqtt.go', 'r', encoding='utf-8') as f:
    content = f.read()

new_getTopic = """func (m *MqttClient) getTopic(mn string, flow string, eventType string) string {
\tbase := strings.TrimSpace(m.cfg.MqttTopic)
\tif base == "" {
\t\tbase = "chromatograph/tcd"
\t} else {
\t\tbase = strings.TrimSuffix(base, "/")
\t}
\treturn fmt.Sprintf("%s/%s/%s/%s", base, mn, flow, eventType)
}"""

content = re.sub(
    r'func \(m \*MqttClient\) getTopic\(mn string, sub string\) string \{.*?\n\}',
    new_getTopic,
    content,
    flags=re.DOTALL
)

content = content.replace('topic := m.getTopic(extNo, "test")', 'topic := m.getTopic(extNo, "telemetry", "test")')
content = content.replace('topic := m.getTopic(mn, "info")', 'topic := m.getTopic(mn, "telemetry", "info")')
content = content.replace('topic := m.getTopic(mn, "status")', 'topic := m.getTopic(mn, "telemetry", "status")')
content = content.replace('topic := m.getTopic(mn, "result")', 'topic := m.getTopic(mn, "telemetry", "result")')
content = content.replace('topic := m.getTopic(mn, "log/"+level)', 'topic := m.getTopic(mn, "telemetry", "log/"+level)')
content = content.replace('topic := m.getTopic(mn, "audit")', 'topic := m.getTopic(mn, "telemetry", "audit_snapshot")')

with open(r'i:\GIT\VS2022\Chromatography-workstation\src\edge\internal\telemetry\mqtt.go', 'w', encoding='utf-8') as f:
    f.write(content)
