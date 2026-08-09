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
\t
\tdeviceIdentifier := mn
\tif m.cfg.MqttClientID != "" {
\t\tdeviceIdentifier = m.cfg.MqttClientID
\t}
\t
\treturn fmt.Sprintf("%s/%s/%s/%s", base, deviceIdentifier, flow, eventType)
}'''

c = re.sub(
    r'func \(m \*MqttClient\) getTopic\(mn string, flow string, eventType string\) string \{.*?\n\}',
    new_getTopic,
    c,
    flags=re.DOTALL
)

with open(r'i:\GIT\VS2022\Chromatography-workstation\src\edge\internal\telemetry\mqtt.go', 'w', encoding='utf-8') as f:
    f.write(c)