package publisher

import (
	v1 "chromatography-workstation/edge/internal/contracts/v1"
	"chromatography-workstation/edge/internal/modbusslave"
	"chromatography-workstation/edge/internal/models"
	"fmt"
)

type ModbusAdapter struct {
	Server *modbusslave.Server
}

func NewModbusAdapter(port int, deviceID string, rtuPort string) (*ModbusAdapter, error) {
	srv, err := modbusslave.NewServer(port, deviceID, rtuPort)
	if err != nil {
		return nil, err
	}
	if err := srv.Start(); err != nil {
		return nil, err
	}
	return &ModbusAdapter{Server: srv}, nil
}

func (m *ModbusAdapter) Stop() {
	if m.Server != nil {
		m.Server.Stop()
	}
}

func (m *ModbusAdapter) PublishState(deviceID string, deviceNo string, state models.TwinState) error {
	if m.Server == nil {
		return nil
	}
	// Mapping standard TwinState to Modbus register 101 (from legacy)
	var st uint16 = 0 // Idle
	switch state {
	case models.StateIdle:
		st = 0
	case models.StateStarting:
		st = 2
	case models.StateRunning:
		st = 1 // Measuring
	case models.StatePaused:
		st = 3
	case models.StateError, models.StateAborted:
		st = 4
	}
	m.Server.SetUint16(101, st)
	return nil
}

func (m *ModbusAdapter) PublishResult(payload ResultPayload) error {
	if m.Server == nil {
		return nil
	}
	// We expect result to be either a v1.Result directly or a map containing it.
	// Since main.go previously called UpdateFullResult(res), let's type assert.
	if res, ok := payload.Result.(v1.Result); ok {
		m.Server.UpdateFullResult(res)
	} else if p, ok := payload.Result.(map[string]interface{}); ok {
		// Fallback
		_ = p
	}
	return nil
}

func (m *ModbusAdapter) PublishAlarm(deviceID string, deviceNo string, activeAlarms []string) error {
	if m.Server == nil {
		return nil
	}
	// e.g., Set register 102 to number of active alarms
	m.Server.SetUint16(102, uint16(len(activeAlarms)))
	return nil
}

func (m *ModbusAdapter) PublishLog(deviceID string, deviceNo string, level string, message string) error {
	if m.Server == nil {
		return nil
	}
	m.Server.PushLog(fmt.Sprintf("[%s] %s", level, message))
	return nil
}

func (m *ModbusAdapter) PublishAudit(deviceID string, deviceNo string, user string, action string, details string) error {
	if m.Server == nil {
		return nil
	}
	// Audit logs can be pushed as a special log entry to the Modbus holding registers
	m.Server.PushLog(fmt.Sprintf("[AUDIT] %s: %s - %s", user, action, details))
	return nil
}

// GetUnderlyingServer returns the raw modbusslave.Server to maintain backward compatibility
// for specific register writes in main.go (e.g. SetFloat32(111, ...))
func (m *ModbusAdapter) GetUnderlyingServer() *modbusslave.Server {
	return m.Server
}
