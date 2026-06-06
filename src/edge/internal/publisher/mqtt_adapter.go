package publisher

import (
	"chromatography-workstation/edge/internal/contracts/v1"
	"chromatography-workstation/edge/internal/models"
	"chromatography-workstation/edge/internal/telemetry"
	"math"
	"time"
)

type MqttAdapter struct {
	Client *telemetry.MqttClient
}

func NewMqttAdapter(cfg models.SysConfig) *MqttAdapter {
	c := telemetry.NewMqttClient(cfg)
	if c == nil {
		return nil
	}
	return &MqttAdapter{Client: c}
}

func (m *MqttAdapter) Stop() {
	if m.Client != nil {
		m.Client.Disconnect()
	}
}

func (m *MqttAdapter) PublishState(deviceID string, deviceNo string, state models.TwinState) error {
	if m.Client == nil {
		return nil
	}
	targetID := deviceID
	if deviceNo != "" {
		targetID = deviceNo
	}
	m.Client.PublishStatus(targetID, map[string]interface{}{
		"time":      time.Now().Unix(),
		"device_id": targetID,
		"state":     state,
	})
	return nil
}

func (m *MqttAdapter) PublishResult(payload ResultPayload) error {
	if m.Client == nil {
		return nil
	}
	
	// Default payload extraction
	var pubPayload map[string]interface{}

	targetID := payload.DeviceID
	if payload.DeviceNo != "" {
		targetID = payload.DeviceNo
	}

	if res, ok := payload.Result.(v1.Result); ok {
		// Specific mapping for MQTT Result 
		polls := make(map[string]float64)
		for _, p := range res.Pollutants {
			polls[p.Code] = math.Round(p.Amount*1000) / 1000
		}
		for _, g := range res.Groups {
			polls[g.Code] = math.Round(g.Amount*1000) / 1000
		}
		pubPayload = map[string]interface{}{
			"time":      payload.Time,
			"trace_id":  payload.TraceID,
			"device_id": targetID,
			"results":   polls,
		}
	} else if p, ok := payload.Result.(map[string]interface{}); ok {
		pubPayload = p
	} else {
		pubPayload = map[string]interface{}{
			"time":      payload.Time,
			"device_id": targetID,
			"data":      payload.Result,
		}
	}

	m.Client.PublishResult(targetID, pubPayload)
	return nil
}

func (m *MqttAdapter) PublishAlarm(deviceID string, deviceNo string, activeAlarms []string) error {
	if m.Client == nil {
		return nil
	}
	targetID := deviceID
	if deviceNo != "" {
		targetID = deviceNo
	}
	m.Client.PublishStatus(targetID, map[string]interface{}{
		"time":      time.Now().Unix(),
		"device_id": targetID,
		"alarms":    activeAlarms,
	})
	return nil
}

func (m *MqttAdapter) PublishLog(deviceID string, deviceNo string, level string, message string) error {
	if m.Client == nil {
		return nil
	}
	targetID := deviceID
	if deviceNo != "" {
		targetID = deviceNo
	}
	m.Client.PublishLog(targetID, map[string]interface{}{
		"time":      time.Now().Unix(),
		"device_id": targetID,
		"level":     level,
		"message":   message,
	})
	return nil
}

func (m *MqttAdapter) PublishAudit(deviceID string, deviceNo string, user string, action string, details string) error {
	if m.Client == nil {
		return nil
	}
	targetID := deviceID
	if deviceNo != "" {
		targetID = deviceNo
	}
	m.Client.PublishAudit(targetID, map[string]interface{}{
		"time":      time.Now().Unix(),
		"device_id": targetID,
		"user":      user,
		"action":    action,
		"details":   details,
	})
	return nil
}
