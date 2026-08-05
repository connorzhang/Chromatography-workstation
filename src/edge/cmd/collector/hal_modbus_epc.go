package main

import (
	"encoding/binary"
	"encoding/json"
	"fmt"
	"math"
	"net/http"
	"strings"
	"sync"
	"time"

	"github.com/goburrow/modbus"
)

var (
	globalModbusEPCCtrl *ModbusEPCController
	modbusEPCCtrlMu     sync.Mutex
)

type ModbusEPCController struct {
	client  modbus.Client
	handler modbus.ClientHandler
	mu      sync.Mutex
	portMu  *SharedPortLock // Mutex for sharing COM port across slaves
	port    string
	address byte

	// 缓存字段
	cachedMu    sync.RWMutex
	cachedState EPCState
}

type EPCState struct {
	RealPressure float32 `json:"real_pressure"` // 0x0000
	RealFlow     float32 `json:"real_flow"`     // 0x0002
	ValveOpen    uint16  `json:"valve_open"`    // 0x0004
	Status       uint16  `json:"status"`        // 0x0005
	Temp         int16   `json:"temp"`          // 0x0006
	Connected    bool    `json:"connected"`
}

func NewModbusEPCController(port string, slaveID byte) *ModbusEPCController {
	var handler modbus.ClientHandler
	var portMu *SharedPortLock

	if strings.Contains(port, ":") {
		tcpHandler := modbus.NewTCPClientHandler(port)
		tcpHandler.SlaveId = slaveID
		tcpHandler.Timeout = 2 * time.Second
		handler = tcpHandler
	} else {
		rtuHandler, mu := getSharedRTUHandler(port)
		handler = rtuHandler
		portMu = mu
	}

	return &ModbusEPCController{
		handler: handler,
		portMu:  portMu,
		port:    port,
		address: slaveID,
	}
}

func (m *ModbusEPCController) Connect() error {
	m.mu.Lock()
	defer m.mu.Unlock()

	if m.client != nil {
		return nil
	}

	if h, ok := m.handler.(*modbus.RTUClientHandler); ok {
		if err := h.Connect(); err != nil {
			return fmt.Errorf("failed to connect modbus RTU for EPC on %s: %v", m.port, err)
		}
	} else if h, ok := m.handler.(*modbus.TCPClientHandler); ok {
		if err := h.Connect(); err != nil {
			return fmt.Errorf("failed to connect modbus TCP for EPC on %s: %v", m.port, err)
		}
	}

	m.client = modbus.NewClient(m.handler)
	return nil
}

func (m *ModbusEPCController) Close() {
	m.mu.Lock()
	defer m.mu.Unlock()

	if h, ok := m.handler.(*modbus.RTUClientHandler); ok {
		h.Close()
	} else if h, ok := m.handler.(*modbus.TCPClientHandler); ok {
		h.Close()
	}

	m.client = nil
}

func (m *ModbusEPCController) ReadStateOnce() {
	m.mu.Lock()
	defer m.mu.Unlock()

	var state EPCState
	if m.client == nil {
		if h, ok := m.handler.(*modbus.RTUClientHandler); ok {
			if err := h.Connect(); err != nil {
				m.cachedMu.Lock()
				m.cachedState.Connected = false
				m.cachedMu.Unlock()
				return
			}
		}
		m.client = modbus.NewClient(m.handler)
	}

	if m.portMu != nil {
		m.portMu.Lock()
		defer m.portMu.Unlock()
		if rtu, ok := m.handler.(*modbus.RTUClientHandler); ok {
			rtu.SlaveId = m.address
		}
	}

	// 0x03 read holding registers. Address 0x0000, 7 registers.
	results, err := m.client.ReadHoldingRegisters(0, 7)
	if err != nil {
		m.cachedMu.Lock()
		m.cachedState.Connected = false
		m.cachedMu.Unlock()
		return
	}

	if len(results) >= 14 {
		// Pressure: 0x0000 - 0x0001
		reg1 := binary.BigEndian.Uint16(results[0:2])
		reg2 := binary.BigEndian.Uint16(results[2:4])
		packedPress := uint32(reg1)<<16 | uint32(reg2)
		state.RealPressure = math.Float32frombits(packedPress)

		// Flow: 0x0002 - 0x0003
		reg3 := binary.BigEndian.Uint16(results[4:6])
		reg4 := binary.BigEndian.Uint16(results[6:8])
		packedFlow := uint32(reg3)<<16 | uint32(reg4)
		state.RealFlow = math.Float32frombits(packedFlow)

		// Valve: 0x0004
		state.ValveOpen = binary.BigEndian.Uint16(results[8:10])

		// Status: 0x0005
		state.Status = binary.BigEndian.Uint16(results[10:12])

		// Temp: 0x0006
		state.Temp = int16(binary.BigEndian.Uint16(results[12:14]))
	}

	state.Connected = true

	m.cachedMu.Lock()
	m.cachedState = state
	m.cachedMu.Unlock()
}

func (m *ModbusEPCController) GetCachedState() EPCState {
	m.cachedMu.RLock()
	defer m.cachedMu.RUnlock()
	return m.cachedState
}

func (m *ModbusEPCController) ensureClient() error {
	if m.client == nil {
		if h, ok := m.handler.(*modbus.RTUClientHandler); ok {
			if err := h.Connect(); err != nil {
				return fmt.Errorf("failed to connect: %v", err)
			}
		}
		m.client = modbus.NewClient(m.handler)
	}
	return nil
}

func (m *ModbusEPCController) lockPort() {
	if m.portMu != nil {
		m.portMu.Lock()
		if rtu, ok := m.handler.(*modbus.RTUClientHandler); ok {
			rtu.SlaveId = m.address
		}
	}
}

func (m *ModbusEPCController) unlockPort() {
	if m.portMu != nil {
		m.portMu.Unlock()
	}
}

func (m *ModbusEPCController) WriteSingleRegister(addr uint16, val uint16) error {
	m.mu.Lock()
	defer m.mu.Unlock()
	if err := m.ensureClient(); err != nil { return err }

	m.lockPort()
	defer m.unlockPort()

	_, err := m.client.WriteSingleRegister(addr, val)
	return err
}

func (m *ModbusEPCController) WriteFloat32(addr uint16, val float32) error {
	m.mu.Lock()
	defer m.mu.Unlock()
	if err := m.ensureClient(); err != nil { return err }

	m.lockPort()
	defer m.unlockPort()

	bits := math.Float32bits(val)
	data := make([]byte, 4)
	binary.BigEndian.PutUint32(data, bits)

	_, err := m.client.WriteMultipleRegisters(addr, 2, data)
	return err
}

// WriteAllConfig writes mode, pressure, flow, gasType, units in a single locked session
// to avoid being interleaved by the 500ms background poll, which was causing ~10s delays.
func (m *ModbusEPCController) WriteAllConfig(mode *uint16, pressure *float32, flow *float32, gasType *uint16, units *uint16) error {
	m.mu.Lock()
	defer m.mu.Unlock()
	if err := m.ensureClient(); err != nil { return err }

	m.lockPort()
	defer m.unlockPort()

	// All writes share the same port lock session so the 500ms poll cannot interleave
	if mode != nil {
		if _, err := m.client.WriteSingleRegister(0x0014, *mode); err != nil {
			return fmt.Errorf("set mode failed: %w", err)
		}
	}
	if pressure != nil {
		bits := math.Float32bits(*pressure)
		data := make([]byte, 4)
		binary.BigEndian.PutUint32(data, bits)
		if _, err := m.client.WriteMultipleRegisters(0x0015, 2, data); err != nil {
			return fmt.Errorf("set pressure failed: %w", err)
		}
	}
	if flow != nil {
		bits := math.Float32bits(*flow)
		data := make([]byte, 4)
		binary.BigEndian.PutUint32(data, bits)
		if _, err := m.client.WriteMultipleRegisters(0x0017, 2, data); err != nil {
			return fmt.Errorf("set flow failed: %w", err)
		}
	}
	if gasType != nil {
		if _, err := m.client.WriteSingleRegister(0x0019, *gasType); err != nil {
			return fmt.Errorf("set gas type failed: %w", err)
		}
	}
	if units != nil {
		if _, err := m.client.WriteSingleRegister(0x001A, *units); err != nil {
			return fmt.Errorf("set units failed: %w", err)
		}
	}
	return nil
}

// WriteControlMode 0x0014
func (m *ModbusEPCController) WriteControlMode(mode uint16) error {
	return m.WriteSingleRegister(0x0014, mode)
}

// WriteTargetPressure 0x0015
func (m *ModbusEPCController) WriteTargetPressure(val float32) error {
	return m.WriteFloat32(0x0015, val)
}

// WriteTargetFlow 0x0017
func (m *ModbusEPCController) WriteTargetFlow(val float32) error {
	return m.WriteFloat32(0x0017, val)
}

// WriteGasType 0x0019
func (m *ModbusEPCController) WriteGasType(gt uint16) error {
	return m.WriteSingleRegister(0x0019, gt)
}

// WriteUnits 0x001A
func (m *ModbusEPCController) WriteUnits(u uint16) error {
	return m.WriteSingleRegister(0x001A, u)
}

func handleEPCState(w http.ResponseWriter, r *http.Request) {
	modbusEPCCtrlMu.Lock()
	ctrl := globalModbusEPCCtrl
	modbusEPCCtrlMu.Unlock()

	if ctrl == nil {
		writeJSON(w, http.StatusOK, EPCState{Connected: false})
		return
	}

	state := ctrl.GetCachedState()
	writeJSON(w, http.StatusOK, state)
}

func handleEPCConfig(w http.ResponseWriter, r *http.Request) {
	if r.Method != http.MethodPost {
		http.Error(w, "Method not allowed", http.StatusMethodNotAllowed)
		return
	}

	modbusEPCCtrlMu.Lock()
	ctrl := globalModbusEPCCtrl
	modbusEPCCtrlMu.Unlock()

	if ctrl == nil {
		writeJSON(w, http.StatusBadRequest, map[string]any{"error": "EPC未连接"})
		return
	}

	var req struct {
		Mode     *uint16  `json:"mode"`
		Pressure *float32 `json:"pressure"`
		Flow     *float32 `json:"flow"`
		GasType  *uint16  `json:"gasType"`
		Units    *uint16  `json:"units"`
	}

	if err := json.NewDecoder(r.Body).Decode(&req); err != nil {
		writeJSON(w, http.StatusBadRequest, map[string]any{"error": "invalid json"})
		return
	}

	if err := ctrl.WriteAllConfig(req.Mode, req.Pressure, req.Flow, req.GasType, req.Units); err != nil {
		writeJSON(w, http.StatusInternalServerError, map[string]any{"error": err.Error()})
		return
	}

	writeJSON(w, http.StatusOK, map[string]any{"success": true})
}

