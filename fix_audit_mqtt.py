import re

with open(r'i:\GIT\VS2022\Chromatography-workstation\src\edge\cmd\collector\audit_snapshot.go', 'r', encoding='utf-8') as f:
    content = f.read()

# Add import
if '\"chromatography-workstation/edge/internal/publisher\"' not in content:
    content = content.replace('\"time\"\\n)', '\"time\"\\n\\t\"chromatography-workstation/edge/internal/publisher\"\\n)')

# Find the log statement at the end of takeAuditSnapshot
old_log = 'log.Println(\"[Audit] Snapshot taken successfully at\", snap.Timestamp)'
new_log = '''log.Println(\"[Audit] Snapshot taken successfully at\", snap.Timestamp)

// Send to MQTT via publisher
var devID string = \"SYSTEM\"
states.Range(func(key, value interface{}) bool {
devID = fmt.Sprintf(\"%v\", key)
return false
})

publisher.GlobalPublisher.PublishResult(publisher.ResultPayload{
DeviceID: devID,
DeviceNo: devID,
Time:     snap.Timestamp.Unix(),
Result: map[string]interface{}{
\"event\":    \"audit_snapshot\",
\"snapshot\": snap,
},
})'''

if 'publisher.GlobalPublisher.PublishResult' not in content:
    content = content.replace(old_log, new_log)

with open(r'i:\GIT\VS2022\Chromatography-workstation\src\edge\cmd\collector\audit_snapshot.go', 'w', encoding='utf-8') as f:
    f.write(content)
