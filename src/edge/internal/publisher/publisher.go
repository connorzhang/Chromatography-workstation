package publisher

import (
	"chromatography-workstation/edge/internal/models"
)

// ResultPayload wraps the analytical result along with metadata for external systems
type ResultPayload struct {
	DeviceID string
	DeviceNo string // External unique code (e.g. MN or DeviceNo from advanced settings)
	TraceID  string
	Time     int64
	Result   interface{}
}

// DataPublisher defines the unified interface for external data transmission.
type DataPublisher interface {
	// PublishState broadcasts the current standard instrument state.
	PublishState(deviceID string, deviceNo string, state models.TwinState) error

	// PublishResult broadcasts the finalized analytical results.
	PublishResult(payload ResultPayload) error

	// PublishAlarm broadcasts real-time alarms and conditions.
	PublishAlarm(deviceID string, deviceNo string, activeAlarms []string) error

	// PublishLog broadcasts internal logs for external auditing or troubleshooting.
	PublishLog(deviceID string, deviceNo string, level string, message string) error

	// PublishAudit broadcasts LADS security audit trail records.
	PublishAudit(deviceID string, deviceNo string, user string, action string, details string) error

	// Stop gracefully shuts down the publisher connection/server.
	Stop()
}

// MultiPublisher allows sending data to multiple publishers (e.g. MQTT + Modbus) simultaneously.
type MultiPublisher struct {
	publishers []DataPublisher
}

func NewMultiPublisher(publishers ...DataPublisher) *MultiPublisher {
	return &MultiPublisher{
		publishers: publishers,
	}
}

func (m *MultiPublisher) AddPublisher(p DataPublisher) {
	if p != nil {
		m.publishers = append(m.publishers, p)
	}
}

func (m *MultiPublisher) Stop() {
	for _, p := range m.publishers {
		p.Stop()
	}
}

func (m *MultiPublisher) PublishState(deviceID string, deviceNo string, state models.TwinState) error {
	for _, p := range m.publishers {
		_ = p.PublishState(deviceID, deviceNo, state)
	}
	return nil
}

func (m *MultiPublisher) PublishResult(payload ResultPayload) error {
	for _, p := range m.publishers {
		_ = p.PublishResult(payload)
	}
	return nil
}

func (m *MultiPublisher) PublishAlarm(deviceID string, deviceNo string, activeAlarms []string) error {
	for _, p := range m.publishers {
		_ = p.PublishAlarm(deviceID, deviceNo, activeAlarms)
	}
	return nil
}

func (m *MultiPublisher) PublishLog(deviceID string, deviceNo string, level string, message string) error {
	for _, p := range m.publishers {
		_ = p.PublishLog(deviceID, deviceNo, level, message)
	}
	return nil
}

func (m *MultiPublisher) PublishAudit(deviceID string, deviceNo string, user string, action string, details string) error {
	for _, p := range m.publishers {
		_ = p.PublishAudit(deviceID, deviceNo, user, action, details)
	}
	return nil
}

// GlobalPublisher is the singleton instance used by the application
var GlobalPublisher = NewMultiPublisher()
