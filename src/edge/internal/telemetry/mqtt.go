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
	cfg    models.SysConfig
}

func NewMqttClient(cfg models.SysConfig) *MqttClient {
	if !cfg.MqttEnabled {
		return nil
	}
	broker := cfg.MqttBroker
	if broker == "" {
		broker = "tcp://127.0.0.1:1883"
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
		cfg:    cfg,
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
		"time":  time.Now().Unix(),
	}
	b, _ := json.Marshal(payload)
	topic := "vocs/device/test/info"
	token := m.client.Publish(topic, 1, false, b)
	if !token.WaitTimeout(3 * time.Second) {
		return fmt.Errorf("发布超时")
	}
	return token.Error()
}

func (m *MqttClient) PublishInfo(mn string, payload map[string]any) {
	if m == nil || !m.client.IsConnected() || !m.cfg.MqttUploadInfo {
		return
	}
	topic := fmt.Sprintf("vocs/device/%s/info", mn)
	b, _ := json.Marshal(payload)
	m.client.Publish(topic, 1, false, b)
}

func (m *MqttClient) PublishStatus(mn string, payload map[string]any) {
	if m == nil || !m.client.IsConnected() || !m.cfg.MqttUploadStatus {
		return
	}
	topic := fmt.Sprintf("vocs/device/%s/status", mn)
	b, _ := json.Marshal(payload)
	m.client.Publish(topic, 1, false, b)
}

func (m *MqttClient) PublishResult(mn string, payload map[string]any) {
	if m == nil || !m.client.IsConnected() || !m.cfg.MqttUploadResult {
		return
	}
	topic := fmt.Sprintf("vocs/device/%s/result", mn)
	b, _ := json.Marshal(payload)
	m.client.Publish(topic, 1, false, b)
}

func (m *MqttClient) PublishLog(mn string, payload map[string]any) {
	if m == nil || !m.client.IsConnected() || !m.cfg.MqttUploadLog {
		return
	}
	topic := fmt.Sprintf("vocs/device/%s/log", mn)
	b, _ := json.Marshal(payload)
	m.client.Publish(topic, 1, false, b)
}
