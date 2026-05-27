package telemetry

import (
	"encoding/json"
	"fmt"
	"log"
	"time"

	"chromatography-workstation/edge/internal/models"
	mqtt "github.com/eclipse/paho.mqtt.golang"
)

type MqttClient struct {
	client mqtt.Client
	topic  string
}

func NewMqttClient(cfg models.SysConfig) *MqttClient {
	if !cfg.MqttEnabled {
		return nil
	}
	broker := cfg.MqttBroker
	if broker == "" {
		broker = "tcp://127.0.0.1:1883"
	}
	topic := cfg.MqttTopic
	if topic == "" {
		topic = "vocs/telemetry/results"
	}
	clientID := cfg.MqttClientID
	if clientID == "" {
		clientID = fmt.Sprintf("edge_collector_%d", time.Now().UnixNano())
	}

	opts := mqtt.NewClientOptions().
		AddBroker(broker).
		SetClientID(clientID).
		SetKeepAlive(60 * time.Second).
		SetPingTimeout(15 * time.Second).
		SetAutoReconnect(true).
		SetMaxReconnectInterval(10 * time.Second)
		
	if user := cfg.MqttUser; user != "" {
		opts.SetUsername(user)
	}
	if pass := cfg.MqttPass; pass != "" {
		opts.SetPassword(pass)
	}

	opts.OnConnect = func(c mqtt.Client) {
		log.Printf("MQTT Connected to %s", broker)
	}
	opts.OnConnectionLost = func(c mqtt.Client, err error) {
		log.Printf("MQTT Connection lost: %v", err)
	}

	c := mqtt.NewClient(opts)
	if token := c.Connect(); token.Wait() && token.Error() != nil {
		log.Printf("MQTT Initial connect failed: %v (will keep trying)", token.Error())
	}

	return &MqttClient{
		client: c,
		topic:  topic,
	}
}

func (m *MqttClient) IsConnected() bool {
	if m == nil || m.client == nil {
		return false
	}
	return m.client.IsConnected()
}

func (m *MqttClient) Disconnect() {
	if m != nil && m.client != nil {
		m.client.Disconnect(250)
	}
}

func (m *MqttClient) TestPublish() error {
	if m == nil {
		return fmt.Errorf("MQTT 客户端未初始化 (可能未启用)")
	}
	if !m.client.IsConnected() {
		return fmt.Errorf("MQTT 尚未连接到 Broker")
	}
	payload := map[string]any{
		"event": "test_connection",
		"time":  time.Now().Format(time.RFC3339),
	}
	b, _ := json.Marshal(payload)
	token := m.client.Publish(m.topic, 1, false, b)
	if !token.WaitTimeout(3 * time.Second) {
		return fmt.Errorf("发布超时")
	}
	return token.Error()
}

// PublishResult 上报精简增量的结果到 MQTT 以供 Elasticsearch 溯源 (要求：轻量、不固定组份)
func (m *MqttClient) PublishResult(deviceID string, at time.Time, traceID string, pollutants map[string]float64) {
	if m == nil || !m.client.IsConnected() {
		return
	}
	
	payload := map[string]any{
		"@timestamp": at.UTC().Format(time.RFC3339),
		"device_id":  deviceID,
		"trace_id":   traceID,
		"results":    pollutants,
	}
	
	b, err := json.Marshal(payload)
	if err != nil {
		return
	}
	
	token := m.client.Publish(m.topic, 1, false, b)
	go func() {
		_ = token.Wait()
		if token.Error() != nil {
			log.Printf("MQTT publish failed: %v", token.Error())
		}
	}()
}
