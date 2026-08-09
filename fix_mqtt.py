import re

with open(r'i:\GIT\VS2022\Chromatography-workstation\src\edge\internal\telemetry\mqtt.go', 'r', encoding='utf-8') as f:
    c = f.read()

new_getTopic = '''func (m *MqttClient) getTopic(mn string, flow string, eventType string) string {
\tbase := strings.TrimSpace(m.cfg.MqttTopic)
\tif base == "" {
\t\tbase = "chromatograph/tcd"
\t} else {
\t\tbase = strings.TrimSuffix(base, "/")
\t}
\treturn fmt.Sprintf("%s/%s/%s/%s", base, mn, flow, eventType)
}'''

c = re.sub(
    r'func \(m \*MqttClient\) getTopic\(mn string, sub string\) string \{.*?\n\}',
    new_getTopic,
    c,
    flags=re.DOTALL
)

c = c.replace('topic := m.getTopic(extNo, "test")', 'topic := m.getTopic(extNo, "telemetry", "test")')
c = c.replace('topic := m.getTopic(mn, "info")', 'topic := m.getTopic(mn, "telemetry", "info")')
c = c.replace('topic := m.getTopic(mn, "status")', 'topic := m.getTopic(mn, "telemetry", "status")')
c = c.replace('topic := m.getTopic(mn, "result")', 'topic := m.getTopic(mn, "telemetry", "result")')
c = c.replace('topic := m.getTopic(mn, "log/"+level)', 'topic := m.getTopic(mn, "telemetry", "log/"+level)')
c = c.replace('topic := m.getTopic(mn, "audit")', 'topic := m.getTopic(mn, "telemetry", "audit_snapshot")')

with open(r'i:\GIT\VS2022\Chromatography-workstation\src\edge\internal\telemetry\mqtt.go', 'w', encoding='utf-8') as f:
    f.write(c)