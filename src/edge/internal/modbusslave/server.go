package modbusslave

import (
	"encoding/binary"
	"log"
	"math"
	"sync"
	"time"

	"github.com/simonvetter/modbus"
)

type Server struct {
	srv      *modbus.ModbusServer
	mu       sync.RWMutex
	regs     [65536]uint16
	deviceID string
}

func NewServer(port int, deviceID string) (*Server, error) {
	s := &Server{
		deviceID: deviceID,
	}

	// 填入 24 字节 ASCII 标识 (寄存器 800 - 811)
	// 默认值: "69000000001ABCDEFG123456" (对应长度24)
	defaultID := "69000000001ABCDEFG123456"
	if len(deviceID) >= 24 {
		defaultID = deviceID[:24]
	} else {
		// 补齐空格
		defaultID = deviceID
		for len(defaultID) < 24 {
			defaultID += " "
		}
	}
	s.setASCII(800, defaultID)

	srv, err := modbus.NewServer(&modbus.ServerConfiguration{
		URL:        "tcp://0.0.0.0:1502", // 固定使用 1502
		Timeout:    30 * time.Second,
		MaxClients: 5,
	}, s)

	if err != nil {
		return nil, err
	}

	s.srv = srv
	return s, nil
}

func (s *Server) Start() error {
	return s.srv.Start()
}

func (s *Server) Stop() error {
	return s.srv.Stop()
}

// 供主程序实时更新浓度等数据 (寄存器 100 起)
func (s *Server) UpdateResults(thc, ch4, nmhc float64) {
	s.mu.Lock()
	defer s.mu.Unlock()
	s.setFloat32(100, float32(thc))
	s.setFloat32(102, float32(ch4))
	s.setFloat32(104, float32(nmhc))
}

func (s *Server) setFloat32(addr uint16, v float32) {
	bits := math.Float32bits(v)
	s.regs[addr] = uint16(bits >> 16)   // 高位
	s.regs[addr+1] = uint16(bits & 0xFFFF) // 低位
}

func (s *Server) setASCII(addr uint16, str string) {
	b := []byte(str)
	for i := 0; i < len(b); i += 2 {
		var high, low byte = 0, 0
		if i < len(b) {
			high = b[i]
		}
		if i+1 < len(b) {
			low = b[i+1]
		}
		s.regs[addr+uint16(i/2)] = (uint16(high) << 8) | uint16(low)
	}
}

// === modbus.RequestHandler Interface Implementation ===

func (s *Server) HandleCoils(req *modbus.CoilsRequest) (res []bool, err error) {
	return nil, modbus.ErrIllegalFunction
}

func (s *Server) HandleDiscreteInputs(req *modbus.DiscreteInputsRequest) (res []bool, err error) {
	return nil, modbus.ErrIllegalFunction
}

func (s *Server) HandleHoldingRegisters(req *modbus.HoldingRegistersRequest) (res []uint16, err error) {
	s.mu.RLock()
	defer s.mu.RUnlock()
	
	if req.IsWrite {
		// 处理反控逻辑 (暂略)
		log.Printf("Modbus Write HR: addr=%d, vals=%v", req.Addr, req.Args)
		for i, v := range req.Args {
			if int(req.Addr)+i < len(s.regs) {
				s.regs[req.Addr+uint16(i)] = v
			}
		}
		return nil, nil
	}
	
	// Read
	if int(req.Addr)+int(req.Quantity) > len(s.regs) {
		return nil, modbus.ErrIllegalDataAddress
	}
	
	res = make([]uint16, req.Quantity)
	for i := 0; i < int(req.Quantity); i++ {
		res[i] = s.regs[req.Addr+uint16(i)]
	}
	return res, nil
}

func (s *Server) HandleInputRegisters(req *modbus.InputRegistersRequest) (res []uint16, err error) {
	return s.HandleHoldingRegisters(&modbus.HoldingRegistersRequest{
		Addr:     req.Addr,
		Quantity: req.Quantity,
		IsWrite:  false,
	})
}