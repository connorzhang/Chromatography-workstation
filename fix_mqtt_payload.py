import re

with open(r'i:\GIT\VS2022\Chromatography-workstation\src\edge\internal\telemetry\mqtt.go', 'r', encoding='utf-8') as f:
    content = f.read()

def add_device_id_to_publish(method_name):
    global content
    pattern = r'(func \(m \*MqttClient\) ' + method_name + r'\(mn string, payload map\[string\]any\) \{.*?)(\n\ttopic :=)'
    
    def repl(m):
        return m.group(1) + '\n\tdeviceIdentifier := mn\n\tif m.cfg.MqttClientID != "" {\n\t\tdeviceIdentifier = m.cfg.MqttClientID\n\t}\n\tif _, ok := payload["device_id"]; !ok {\n\t\tpayload["device_id"] = deviceIdentifier\n\t}' + m.group(2)
        
    content = re.sub(pattern, repl, content, flags=re.DOTALL)

add_device_id_to_publish("PublishInfo")
add_device_id_to_publish("PublishStatus")
add_device_id_to_publish("PublishResult")
add_device_id_to_publish("PublishLog")
add_device_id_to_publish("PublishAudit")

with open(r'i:\GIT\VS2022\Chromatography-workstation\src\edge\internal\telemetry\mqtt.go', 'w', encoding='utf-8') as f:
    f.write(content)