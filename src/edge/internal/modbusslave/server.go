package modbusslave

import (
	"log"
	"math"
	"sync"
	"time"

	"github.com/simonvetter/modbus"
	contracts "chromatography-workstation/edge/internal/contracts/v1"
)

type Server struct {
	srvTCP   *modbus.ModbusServer
	mu       sync.RWMutex
	regs     [65536]uint16
	deviceID string
}

func NewServer(port int, deviceID string, rtuPort string) (*Server, error) {
	s := &Server{
		deviceID: deviceID,
	}

	// 填入 24 字节 ASCII 标识 (寄存器 800 - 811)
	defaultID := "69000000001ABCDEFG123456"
	if len(deviceID) >= 24 {
		defaultID = deviceID[:24]
	} else {
		defaultID = deviceID
		for len(defaultID) < 24 {
			defaultID += " "
		}
	}
	s.setASCII(800, defaultID)

	srvTCP, err := modbus.NewServer(&modbus.ServerConfiguration{
		URL:        "tcp://0.0.0.0:1502", // 固定使用 1502
		Timeout:    30 * time.Second,
		MaxClients: 5,
	}, s)
	if err != nil {
		return nil, err
	}
	s.srvTCP = srvTCP

	if rtuPort != "" {
		log.Printf("Note: RTU server on %s is requested, but current modbus library only supports TCP server. Please use Modbus TCP (port 1502).", rtuPort)
	}

	return s, nil
}

func (s *Server) Start() error {
	return s.srvTCP.Start()
}

func (s *Server) Stop() error {
	return s.srvTCP.Stop()
}

// 供主程序实时更新浓度等数据 (寄存器 100 起)
func (s *Server) UpdateResults(thc, ch4, nmhc float64) {
	s.mu.Lock()
	defer s.mu.Unlock()
	s.setFloat32(100, float32(thc))
	s.setFloat32(102, float32(ch4))
	s.setFloat32(104, float32(nmhc))
}

// 供主程序更新完整的积分结果
func (s *Server) UpdateFullResult(res contracts.Result) {
	s.mu.Lock()
	defer s.mu.Unlock()
	
	// 首先保留原有的 100 102 104 逻辑
	var thc, ch4, nmhc float64
	for _, p := range res.Pollutants {
		if p.Code == "THC" {
			thc = p.Amount
		}
		if p.Code == "CH4" {
			ch4 = p.Amount
		}
	}
	for _, g := range res.Groups {
		if g.Code == "NMHC" {
			nmhc = g.Amount
		}
	}
	s.setFloat32(100, float32(thc))
	s.setFloat32(102, float32(ch4))
	s.setFloat32(104, float32(nmhc))

	// 寄存器 200 起，按顺序记录每个 Pollutant 的详细积分信息
	// 每个组分占用 10 个寄存器：
	// +0: 浓度 (Float32, 2 regs)
	// +2: 保留时间 (Float32, 2 regs)
	// +4: 面积 (Float32, 2 regs)
	// +6: 高度 (Float32, 2 regs)
	// +8: 预留
	baseAddr := uint16(200)
	for i, p := range res.Pollutants {
		if i >= 20 { // 最多支持 20 个组分
			break
		}
		addr := baseAddr + uint16(i*10)
		s.setFloat32(addr+0, float32(p.Amount))
		s.setFloat32(addr+2, float32(p.RtS/60.0)) // 保留时间转回分钟
		s.setFloat32(addr+4, float32(p.Area))
		s.setFloat32(addr+6, float32(p.Height))
	}
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