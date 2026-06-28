package main

import (
	"encoding/json"
	"fmt"
	"net/http"
	"sync"
	"time"

	"go.bug.st/serial"
)

var (
	globalTCDCtrl *TCDController
	tcdCtrlMu     sync.Mutex
)

type TCDState struct {
	Connected     bool        `json:"connected"`
	BridgeCurrent uint8       `json:"bridge_current"`
	Values        [20]float64 `json:"values"`
	LastUpdate    time.Time   `json:"-"`
}

type TCDController struct {
	port     serial.Port
	portName string
	state    TCDState
	stateMu  sync.RWMutex
	stopChan chan struct{}
	wg       sync.WaitGroup
}

func NewTCDController(portName string) *TCDController {
	return &TCDController{
		portName: portName,
		stopChan: make(chan struct{}),
	}
}

func (c *TCDController) Connect() error {
	mode := &serial.Mode{
		BaudRate: 38400,
		DataBits: 8,
		Parity:   serial.NoParity,
		StopBits: serial.OneStopBit,
	}

	port, err := serial.Open(c.portName, mode)
	if err != nil {
		return err
	}

	port.SetReadTimeout(time.Millisecond * 500)
	c.port = port

	c.stateMu.Lock()
	c.state.Connected = true
	c.state.LastUpdate = time.Now()
	c.stateMu.Unlock()

	c.wg.Add(1)
	go c.readLoop()

	return nil
}

func (c *TCDController) Close() {
	if c.port != nil {
		close(c.stopChan)
		c.port.Close()
		c.wg.Wait()
		c.port = nil
	}
	c.stateMu.Lock()
	c.state.Connected = false
	c.stateMu.Unlock()
}

func (c *TCDController) readLoop() {
	defer c.wg.Done()

	buf := make([]byte, 1024)
	frame := make([]byte, 0, 512)

	for {
		select {
		case <-c.stopChan:
			return
		default:
		}

		n, err := c.port.Read(buf)
		if err != nil {
			time.Sleep(100 * time.Millisecond)
			continue
		}
		if n > 0 {
			// [DEBUG] 打印收到的原始数据，看看到底是什么！
			if len(frame) < 100 {
				fmt.Printf("[TCD DEBUG] Read %d bytes: %X\n", n, buf[:n])
			}

			frame = append(frame, buf[:n]...)

			for len(frame) >= 87 {
				idx := -1
				for i := 0; i <= len(frame)-87; i++ {
					if frame[i] == 0x45 && frame[i+1] == 0x45 && frame[i+2] == 0xFF && frame[i+3] == 0x01 {
						idx = i
						break
					}
				}

				if idx == -1 {
					// 找不到包头，丢弃一部分
					frame = frame[len(frame)-86:]
					break
				}

				validFrame := frame[idx : idx+87]
				// 检查结尾是不是 0x0D 0x0A
				if validFrame[85] == 0x0D && validFrame[86] == 0x0A {
					c.parseFrame(validFrame)
				} else {
					fmt.Printf("[TCD DEBUG] Header found, but ending is %02X %02X instead of 0D 0A!\n", validFrame[85], validFrame[86])
					// 容错处理：即使结尾不对，也尝试解析
					c.parseFrame(validFrame)
				}
				frame = frame[idx+87:]
			}
		}
	}
}

func (c *TCDController) parseFrame(frame []byte) {
	c.stateMu.Lock()
	defer c.stateMu.Unlock()

	c.state.BridgeCurrent = frame[84]
	dataOffset := 4
	for i := 0; i < 20; i++ {
		idx := dataOffset + (i * 4)

		// 惊天真相：TCD 的协议中，符号位并不是最高 bit（Bit 31），而是最高半字节（Nibble）！
		// 官方文档说的“最高位为1”其实是指：最高的一个十六进制字符为 1（即 0x10）。
		// - 0x00 00 00 25 表示正数 0x25 (37)
		// - 0x10 00 00 25 表示负数 -0x25 (-37)

		signByte := frame[idx]
		sign := 1.0
		if (signByte & 0xF0) == 0x10 {
			sign = -1.0
		}

		// 提取剩下的 28 bit 作为绝对值
		absValue := uint32(frame[idx]&0x0F)<<24 | uint32(frame[idx+1])<<16 | uint32(frame[idx+2])<<8 | uint32(frame[idx+3])

		c.state.Values[i] = sign * float64(absValue)
	}
	c.state.LastUpdate = time.Now()
}

func (c *TCDController) SendCommand(cmd []byte) error {
	if c.port == nil {
		return fmt.Errorf("port not connected")
	}
	_, err := c.port.Write(cmd)
	return err
}

func (c *TCDController) SetBridgeCurrent(val uint8) error {
	cmd := []byte{0x47, 0x45, 0x45, 0x02, 0x0E, val}
	return c.SendCommand(cmd)
}

func (c *TCDController) Zeroing() error {
	cmd := []byte{0x47, 0x45, 0x45, 0x02, 0x0B, 0x00}
	return c.SendCommand(cmd)
}

func (c *TCDController) ReadBridgeCurrent() error {
	cmd := []byte{0x47, 0x45, 0x45, 0x02, 0x08, 0x50}
	return c.SendCommand(cmd)
}

func (c *TCDController) GetState() TCDState {
	c.stateMu.RLock()
	defer c.stateMu.RUnlock()
	return c.state
}

// HTTP Handlers

func handleTCDConnect(w http.ResponseWriter, r *http.Request) {
	if r.Method != http.MethodPost {
		http.Error(w, "Method not allowed", http.StatusMethodNotAllowed)
		return
	}

	var req struct {
		Port string `json:"port"`
	}
	if err := json.NewDecoder(r.Body).Decode(&req); err != nil {
		http.Error(w, `{"error":"invalid request"}`, http.StatusBadRequest)
		return
	}

	tcdCtrlMu.Lock()
	defer tcdCtrlMu.Unlock()

	if globalTCDCtrl != nil {
		globalTCDCtrl.Close()
	}

	globalTCDCtrl = NewTCDController(req.Port)
	if err := globalTCDCtrl.Connect(); err != nil {
		globalTCDCtrl = nil
		http.Error(w, fmt.Sprintf(`{"error":"%v"}`, err), http.StatusInternalServerError)
		return
	}

	w.WriteHeader(http.StatusOK)
	w.Write([]byte(`{"status":"connected"}`))
}

func handleTCDDisconnect(w http.ResponseWriter, r *http.Request) {
	if r.Method != http.MethodPost {
		http.Error(w, "Method not allowed", http.StatusMethodNotAllowed)
		return
	}

	tcdCtrlMu.Lock()
	defer tcdCtrlMu.Unlock()

	if globalTCDCtrl != nil {
		globalTCDCtrl.Close()
		globalTCDCtrl = nil
	}

	w.WriteHeader(http.StatusOK)
	w.Write([]byte(`{"status":"disconnected"}`))
}

func handleTCDState(w http.ResponseWriter, r *http.Request) {
	tcdCtrlMu.Lock()
	ctrl := globalTCDCtrl
	tcdCtrlMu.Unlock()

	if ctrl == nil {
		w.Header().Set("Content-Type", "application/json")
		w.Write([]byte(`{"connected":false}`))
		return
	}

	state := ctrl.GetState()
	// Check timeout (if no data received for 3 seconds)
	if time.Since(state.LastUpdate) > 3*time.Second {
		state.Connected = false
	}

	w.Header().Set("Content-Type", "application/json")
	json.NewEncoder(w).Encode(state)
}

func handleTCDSetBridge(w http.ResponseWriter, r *http.Request) {
	if r.Method != http.MethodPost {
		http.Error(w, "Method not allowed", http.StatusMethodNotAllowed)
		return
	}

	var req struct {
		Value uint8 `json:"value"`
	}
	if err := json.NewDecoder(r.Body).Decode(&req); err != nil {
		http.Error(w, `{"error":"invalid request"}`, http.StatusBadRequest)
		return
	}

	tcdCtrlMu.Lock()
	ctrl := globalTCDCtrl
	tcdCtrlMu.Unlock()

	if ctrl == nil {
		http.Error(w, `{"error":"TCD not connected"}`, http.StatusBadRequest)
		return
	}

	if err := ctrl.SetBridgeCurrent(req.Value); err != nil {
		http.Error(w, fmt.Sprintf(`{"error":"%v"}`, err), http.StatusInternalServerError)
		return
	}

	w.WriteHeader(http.StatusOK)
	w.Write([]byte(`{"status":"ok"}`))
}

func handleTCDZeroing(w http.ResponseWriter, r *http.Request) {
	if r.Method != http.MethodPost {
		http.Error(w, "Method not allowed", http.StatusMethodNotAllowed)
		return
	}

	tcdCtrlMu.Lock()
	ctrl := globalTCDCtrl
	tcdCtrlMu.Unlock()

	if ctrl == nil {
		http.Error(w, `{"error":"TCD not connected"}`, http.StatusBadRequest)
		return
	}

	if err := ctrl.Zeroing(); err != nil {
		http.Error(w, fmt.Sprintf(`{"error":"%v"}`, err), http.StatusInternalServerError)
		return
	}

	w.WriteHeader(http.StatusOK)
	w.Write([]byte(`{"status":"ok"}`))
}

func handleTCDReadBridge(w http.ResponseWriter, r *http.Request) {
	if r.Method != http.MethodPost {
		http.Error(w, "Method not allowed", http.StatusMethodNotAllowed)
		return
	}

	tcdCtrlMu.Lock()
	ctrl := globalTCDCtrl
	tcdCtrlMu.Unlock()

	if ctrl == nil {
		http.Error(w, `{"error":"TCD not connected"}`, http.StatusBadRequest)
		return
	}

	if err := ctrl.ReadBridgeCurrent(); err != nil {
		http.Error(w, fmt.Sprintf(`{"error":"%v"}`, err), http.StatusInternalServerError)
		return
	}

	w.WriteHeader(http.StatusOK)
	w.Write([]byte(`{"status":"ok"}`))
}
