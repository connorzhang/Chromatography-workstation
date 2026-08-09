import re

with open(r'i:\GIT\VS2022\Chromatography-workstation\src\edge\internal\telemetry\mqtt.go', 'r', encoding='utf-8') as f:
    content = f.read()

# Remove the check for MqttUploadInfo so that audit snapshots (which use PublishInfo) always go through
old_func = '''func (m *MqttClient) PublishInfo(mn string, payload map[string]any) {
if m == nil || !m.client.IsConnected() || !m.cfg.MqttUploadInfo {
return
}'''

new_func = '''func (m *MqttClient) PublishInfo(mn string, payload map[string]any) {
if m == nil || !m.client.IsConnected() {
return
}'''

content = content.replace(old_func, new_func)

with open(r'i:\GIT\VS2022\Chromatography-workstation\src\edge\internal\telemetry\mqtt.go', 'w', encoding='utf-8') as f:
    f.write(content)
