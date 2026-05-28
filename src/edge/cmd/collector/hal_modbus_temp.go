package main

import (
	"encoding/binary"
	"encoding/json"
	"fmt"
	"math"
	"net/http"
	"sync"
	"time"

	"github.com/goburrow/modbus"
)

var (
	globalModbusTempCtrl *ModbusTempController
	modbusTempCtrlMu     sync.Mutex
)

type ModbusTempController struct {
	client  modbus.Client
	handler *modbus.RTUClientHandler
	mu      sync.Mutex
	port    string
	address byte
}

type TempModuleState struct {
	RealTimeTemps [8]float64 `json:"realtime_temps"`
	SetTemps      [8]int16   `json:"set_temps"`
	Disconnected  [8]bool    `json:"disconnected"`
	Connected     bool       `json:"connected"`
}

func NewModbusTempController(port string, slaveID byte) *ModbusTempController {
	handler := modbus.NewRTUClientHandler(port)
	handler.BaudRate = 9600
	handler.DataBits = 8
	handler.Parity = "N"
	handler.StopBits = 1
	handler.SlaveId = slaveID
	handler.Timeout = 1 * time.Second

	return &ModbusTempController{
		handler: handler,
		port:    port,
		address: slaveID,
	}
}

func (m *ModbusTempController) Connect() error {
	m.mu.Lock()
	defer m.mu.Unlock()

	if m.client != nil {
		return nil
	}

	err := m.handler.Connect()
	if err != nil {
		return fmt.Errorf("failed to connect modbus temp controller on %s: %v", m.port, err)
	}

	m.client = modbus.NewClient(m.handler)
	return nil
}

func (m *ModbusTempController) Close() {
	m.mu.Lock()
	defer m.mu.Unlock()
	if m.handler != nil {
		m.handler.Close()
	}
	m.client = nil
}

func (m *ModbusTempController) ReadState() (TempModuleState, error) {
	m.mu.Lock()
	defer m.mu.Unlock()

	var state TempModuleState
	if m.client == nil {
		return state, fmt.Errorf("modbus client not initialized")
	}

	// Read Set Temps (Address 42, 8 registers)
	setResults, err := m.client.ReadHoldingRegisters(42, 8)
	if err != nil {
		return state, fmt.Errorf("read set temps failed: %v", err)
	}
	for i := 0; i < 8; i++ {
		state.SetTemps[i] = int16(binary.BigEndian.Uint16(setResults[i*2 : i*2+2]))
	}

	// Read RealTime Temps (Address 360, 16 registers)
	rtResults, err := m.client.ReadHoldingRegisters(360, 16)
	if err != nil {
		return state, fmt.Errorf("read real-time temps failed: %v", err)
	}
	for i := 0; i < 8; i++ {
		// CDAB Byte order
		reg1 := binary.BigEndian.Uint16(rtResults[i*4 : i*4+2])
		reg2 := binary.BigEndian.Uint16(rtResults[i*4+2 : i*4+4])
		packed := uint32(reg2)<<16 | uint32(reg1)
		val := math.Float32frombits(packed)

		if val >= 32767.0 {
			state.Disconnected[i] = true
			state.RealTimeTemps[i] = 32767.0
		} else {
			state.Disconnected[i] = false
			state.RealTimeTemps[i] = float64(val)
		}
	}

	state.Connected = true
	return state, nil
}

func (m *ModbusTempController) SetTemperature(channel int, targetTemp int16) error {
	m.mu.Lock()
	defer m.mu.Unlock()

	if m.client == nil {
		return fmt.Errorf("modbus client not initialized")
	}
	if channel < 1 || channel > 8 {
		return fmt.Errorf("invalid channel %d", channel)
	}

	address := uint16(42 + (channel - 1))
	_, err := m.client.WriteSingleRegister(address, uint16(targetTemp))
	return err
}

func (m *ModbusTempController) SetIO(channel int, state bool) error {
	m.mu.Lock()
	defer m.mu.Unlock()

	if m.client == nil {
		return fmt.Errorf("modbus client not initialized")
	}
	if channel < 1 || channel > 8 {
		return fmt.Errorf("invalid channel %d", channel)
	}

	// Address starts from 32 for channel 1.
	// channel 5 -> address 36.
	address := uint16(32 + (channel - 1))
	
	// Modbus WriteSingleCoil: 0xFF00 for ON, 0x0000 for OFF
	var val uint16 = 0x0000
	if state {
		val = 0xFF00
	}
	
	_, err := m.client.WriteSingleCoil(address, val)
	return err
}

// HTTP API Handlers

func handleModbusTempConnect(w http.ResponseWriter, r *http.Request) {
	if r.Method != http.MethodPost {
		http.Error(w, "method not allowed", http.StatusMethodNotAllowed)
		return
	}

	var req struct {
		Port    string `json:"port"`
		SlaveID byte   `json:"slave_id"`
	}
	if err := json.NewDecoder(r.Body).Decode(&req); err != nil {
		http.Error(w, err.Error(), http.StatusBadRequest)
		return
	}

	modbusTempCtrlMu.Lock()
	defer modbusTempCtrlMu.Unlock()

	if globalModbusTempCtrl != nil {
		globalModbusTempCtrl.Close()
	}

	globalModbusTempCtrl = NewModbusTempController(req.Port, req.SlaveID)
	err := globalModbusTempCtrl.Connect()
	if err != nil {
		writeJSON(w, http.StatusInternalServerError, map[string]interface{}{"error": err.Error()})
		return
	}

	writeJSON(w, http.StatusOK, map[string]interface{}{"message": "connected"})
}

func handleModbusTempDisconnect(w http.ResponseWriter, r *http.Request) {
	if r.Method != http.MethodPost {
		http.Error(w, "method not allowed", http.StatusMethodNotAllowed)
		return
	}

	modbusTempCtrlMu.Lock()
	defer modbusTempCtrlMu.Unlock()

	if globalModbusTempCtrl != nil {
		globalModbusTempCtrl.Close()
		globalModbusTempCtrl = nil
	}

	writeJSON(w, http.StatusOK, map[string]interface{}{"message": "disconnected"})
}

func handleModbusTempState(w http.ResponseWriter, r *http.Request) {
	modbusTempCtrlMu.Lock()
	ctrl := globalModbusTempCtrl
	modbusTempCtrlMu.Unlock()

	if ctrl == nil {
		writeJSON(w, http.StatusOK, TempModuleState{Connected: false})
		return
	}

	state, err := ctrl.ReadState()
	if err != nil {
		// Log error, but still return connected=false or something
		writeJSON(w, http.StatusInternalServerError, map[string]interface{}{"error": err.Error(), "connected": false})
		return
	}

	writeJSON(w, http.StatusOK, state)
}

func handleModbusTempSet(w http.ResponseWriter, r *http.Request) {
	if r.Method != http.MethodPost {
		http.Error(w, "method not allowed", http.StatusMethodNotAllowed)
		return
	}

	var req struct {
		Channel    int   `json:"channel"`
		TargetTemp int16 `json:"target_temp"`
	}
	if err := json.NewDecoder(r.Body).Decode(&req); err != nil {
		http.Error(w, err.Error(), http.StatusBadRequest)
		return
	}

	modbusTempCtrlMu.Lock()
	ctrl := globalModbusTempCtrl
	modbusTempCtrlMu.Unlock()

	if ctrl == nil {
		writeJSON(w, http.StatusBadRequest, map[string]interface{}{"error": "not connected"})
		return
	}

	err := ctrl.SetTemperature(req.Channel, req.TargetTemp)
	if err != nil {
		writeJSON(w, http.StatusInternalServerError, map[string]interface{}{"error": err.Error()})
		return
	}

	writeJSON(w, http.StatusOK, map[string]interface{}{"message": "success"})
}

func handleModbusTempSetIO(w http.ResponseWriter, r *http.Request) {
	if r.Method != http.MethodPost {
		http.Error(w, "method not allowed", http.StatusMethodNotAllowed)
		return
	}

	var req struct {
		Channel int  `json:"channel"`
		State   bool `json:"state"`
	}
	if err := json.NewDecoder(r.Body).Decode(&req); err != nil {
		http.Error(w, err.Error(), http.StatusBadRequest)
		return
	}

	modbusTempCtrlMu.Lock()
	ctrl := globalModbusTempCtrl
	modbusTempCtrlMu.Unlock()

	if ctrl == nil {
		writeJSON(w, http.StatusBadRequest, map[string]interface{}{"error": "not connected"})
		return
	}

	err := ctrl.SetIO(req.Channel, req.State)
	if err != nil {
		writeJSON(w, http.StatusInternalServerError, map[string]interface{}{"error": err.Error()})
		return
	}

	writeJSON(w, http.StatusOK, map[string]interface{}{"message": "success"})
}
