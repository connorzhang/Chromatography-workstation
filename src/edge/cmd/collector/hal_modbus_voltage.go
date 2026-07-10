package main

import (
	"encoding/binary"
	"math"
	"net/http"
	"sync"
	"time"

	"github.com/goburrow/modbus"
)

var (
	globalVoltageCtrl *VoltageController
	voltageCtrlMu     sync.Mutex
)

// VoltageController 电压采集模块驱动 (Modbus RTU, 地址1, CDAB字节序)
type VoltageController struct {
	handler modbus.ClientHandler
	client  modbus.Client
	portMu  *SharedPortLock
	port    string
	address byte
	mu      sync.Mutex

	// 缓存字段：后台循环写入，API 只读
	cachedMu     sync.RWMutex
	cachedVolt   float32
	cachedTime   time.Time
	cachedOK     bool
}

// VoltageState 电压采集状态
type VoltageState struct {
	Connected bool    `json:"connected"`
	Voltage   float32 `json:"voltage"` // 原始电压值 (V)
}

func NewVoltageController(port string, slaveID byte) *VoltageController {
	handler, mu := getSharedRTUHandler(port)
	return &VoltageController{
		handler: handler,
		portMu:  mu,
		port:    port,
		address: slaveID,
		client:  modbus.NewClient(handler),
	}
}

// ReadVoltageOnce 在 auto_connect 循环中调用，读取并缓存电压值
func (v *VoltageController) ReadVoltageOnce() {
	v.mu.Lock()
	defer v.mu.Unlock()

	v.portMu.Lock()
	defer v.portMu.Unlock()

	if rtu, ok := v.handler.(*modbus.RTUClientHandler); ok {
		rtu.SlaveId = v.address
	}

	// 读取保持寄存器 0x0020, 2个寄存器
	results, err := v.client.ReadHoldingRegisters(0x0020, 2)
	if err != nil {
		v.cachedMu.Lock()
		v.cachedOK = false
		v.cachedMu.Unlock()
		return
	}

	if len(results) < 4 {
		v.cachedMu.Lock()
		v.cachedOK = false
		v.cachedMu.Unlock()
		return
	}

	// CDAB字节序: 交换两个寄存器
	// 原始字节: results[0]=9D, results[1]=B2, results[2]=3F, results[3]=8F
	// CDAB排列: 3F 8F 9D B2 → 0x3F8F9DB2 = 1.121
	reg1 := binary.BigEndian.Uint16(results[0:2]) // 0x9DB2
	reg2 := binary.BigEndian.Uint16(results[2:4]) // 0x3F8F
	packed := uint32(reg2)<<16 | uint32(reg1)
	voltage := math.Float32frombits(packed)

	v.cachedMu.Lock()
	v.cachedVolt = voltage
	v.cachedTime = time.Now()
	v.cachedOK = true
	v.cachedMu.Unlock()
}

// GetCachedVoltage 返回缓存的电压值，不访问串口
func (v *VoltageController) GetCachedVoltage() (float32, bool) {
	v.cachedMu.RLock()
	defer v.cachedMu.RUnlock()
	return v.cachedVolt, v.cachedOK
}

func handleVoltageState(w http.ResponseWriter, r *http.Request) {
	voltageCtrlMu.Lock()
	ctrl := globalVoltageCtrl
	voltageCtrlMu.Unlock()

	if ctrl == nil {
		writeJSON(w, http.StatusOK, VoltageState{Connected: false})
		return
	}

	volt, ok := ctrl.GetCachedVoltage()
	if !ok {
		writeJSON(w, http.StatusOK, VoltageState{Connected: false})
		return
	}

	writeJSON(w, http.StatusOK, VoltageState{
		Connected: true,
		Voltage:   volt,
	})
}