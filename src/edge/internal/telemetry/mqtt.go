package telemetry

import (
	"encoding/json"
	"fmt"
	"log"
	"os"
	"time"

	mqtt "github.com/eclipse/paho.mqtt.golang"
)

type MqttClient struct {
	client mqtt.Client
	topic  string
}

func NewMqttClient() *MqttClient {
	broker := os.Getenv("MQTT_BROKER")
	if broker == "" {
		broker = "tcp://127.0.0.1:1883" // Default fallback
	}
	topic := os.Getenv("MQTT_TOPIC")
	if topic == "" {
		topic = "vocs/telemetry/results"
	}
	clientID := os.Getenv("MQTT_CLIENT_ID")
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
		
	if user := os.Getenv("MQTT_USER"); user != "" {
		opts.SetUsername(user)
	}
	if pass := os.Getenv("MQTT_PASS"); pass != "" {
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
