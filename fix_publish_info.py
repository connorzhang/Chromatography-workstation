import re

# 1. Update publisher.go
with open(r'i:\GIT\VS2022\Chromatography-workstation\src\edge\internal\publisher\publisher.go', 'r', encoding='utf-8') as f:
    pub_content = f.read()

if 'PublishInfo(' not in pub_content:
    pub_content = pub_content.replace('PublishState(deviceID string, deviceNo string, state models.TwinState) error', 'PublishState(deviceID string, deviceNo string, state models.TwinState) error\n\tPublishInfo(deviceID string, deviceNo string, payload map[string]interface{}) error')
    
    multi_info = '''func (m *MultiPublisher) PublishInfo(deviceID string, deviceNo string, payload map[string]interface{}) error {
for _, p := range m.publishers {
_ = p.PublishInfo(deviceID, deviceNo, payload)
}
return nil
}

func (m *MultiPublisher) PublishState'''
    pub_content = pub_content.replace('func (m *MultiPublisher) PublishState', multi_info)
    
    with open(r'i:\GIT\VS2022\Chromatography-workstation\src\edge\internal\publisher\publisher.go', 'w', encoding='utf-8') as f:
        f.write(pub_content)

# 2. Update mqtt_adapter.go
with open(r'i:\GIT\VS2022\Chromatography-workstation\src\edge\internal\publisher\mqtt_adapter.go', 'r', encoding='utf-8') as f:
    mqtt_content = f.read()

if 'PublishInfo(' not in mqtt_content:
    mqtt_info = '''func (m *MqttAdapter) PublishInfo(deviceID string, deviceNo string, payload map[string]interface{}) error {
if m.Client == nil {
return nil
}
targetID := deviceID
if deviceNo != "" {
targetID = deviceNo
}
m.Client.PublishInfo(targetID, payload)
return nil
}

func (m *MqttAdapter) PublishState'''
    mqtt_content = mqtt_content.replace('func (m *MqttAdapter) PublishState', mqtt_info)
    
    with open(r'i:\GIT\VS2022\Chromatography-workstation\src\edge\internal\publisher\mqtt_adapter.go', 'w', encoding='utf-8') as f:
        f.write(mqtt_content)

# 3. Update audit_snapshot.go
with open(r'i:\GIT\VS2022\Chromatography-workstation\src\edge\cmd\collector\audit_snapshot.go', 'r', encoding='utf-8') as f:
    audit_content = f.read()

if 'GlobalPublisher.PublishResult' in audit_content:
    old_pub = '''publisher.GlobalPublisher.PublishResult(publisher.ResultPayload{
DeviceID: devID,
DeviceNo: devID,
Time:     snap.Timestamp.Unix(),
Result: map[string]interface{}{
"event":    "audit_snapshot",
"snapshot": snap,
},
})'''
    new_pub = '''publisher.GlobalPublisher.PublishInfo(devID, devID, map[string]interface{}{
"event":    "audit_snapshot",
"time":     snap.Timestamp.Unix(),
"snapshot": snap,
})'''
    audit_content = audit_content.replace(old_pub, new_pub)
    with open(r'i:\GIT\VS2022\Chromatography-workstation\src\edge\cmd\collector\audit_snapshot.go', 'w', encoding='utf-8') as f:
        f.write(audit_content)

