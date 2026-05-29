package modbusslave

import (
	"encoding/binary"
	"fmt"
	"log"
	"math"
	"strconv"
	"sync"
	"time"

	contracts "chromatography-workstation/edge/internal/contracts/v1"

	"github.com/tbrandon/mbserver"
)

type Server struct {
	srv      *mbserver.Server
	mu       sync.RWMutex
	deviceID string
	unitID   uint8
	logQueue chan string
	stopChan chan struct{}
}

func (s *Server) checkUnitID(frame mbserver.Framer) *mbserver.Exception {
	var reqDevice uint8
	switch f := frame.(type) {
	case *mbserver.TCPFrame:
		reqDevice = f.Device
	case *mbserver.RTUFrame:
		reqDevice = f.Address
	default:
		return nil
	}
	// Broadcast(0) and Match
	if reqDevice != s.unitID && reqDevice != 0 && reqDevice != 255 {
		return &mbserver.GatewayTargetDeviceFailedtoRespond
	}
	return nil
}

func NewServer(port int, deviceID string, rtuPort string) (*Server, error) {
	uid, _ := strconv.Atoi(deviceID)
	if uid <= 0 || uid > 255 {
		uid = 1
	}

	s := &Server{
		deviceID: deviceID,
		unitID:   uint8(uid),
		srv:      mbserver.NewServer(),
		logQueue: make(chan string, 100),
		stopChan: make(chan struct{}),
	}

	go s.logWorker()

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

	// 注册符合标准 Modbus 协议长度校验的处理函数
	// 标准：读保持寄存器 0x03，一次最多读 125 个
	s.srv.RegisterFunctionHandler(3, func(srv *mbserver.Server, frame mbserver.Framer) ([]byte, *mbserver.Exception) {
		if exc := s.checkUnitID(frame); exc != nil {
			return []byte{}, exc
		}
		data := frame.GetData()
		if len(data) < 4 {
			return []byte{}, &mbserver.IllegalDataValue
		}
		register := binary.BigEndian.Uint16(data[0:2])
		numRegs := binary.BigEndian.Uint16(data[2:4])
		
		if numRegs < 1 || numRegs > 125 {
			return []byte{}, &mbserver.IllegalDataValue
		}
		endRegister := int(register) + int(numRegs)
		if endRegister > 65536 {
			return []byte{}, &mbserver.IllegalDataAddress
		}
		return append([]byte{byte(numRegs * 2)}, mbserver.Uint16ToBytes(srv.HoldingRegisters[register:endRegister])...), &mbserver.Success
	})

	// 读输入寄存器 0x04，一次最多读 125 个
	s.srv.RegisterFunctionHandler(4, func(srv *mbserver.Server, frame mbserver.Framer) ([]byte, *mbserver.Exception) {
		if exc := s.checkUnitID(frame); exc != nil {
			return []byte{}, exc
		}
		data := frame.GetData()
		if len(data) < 4 {
			return []byte{}, &mbserver.IllegalDataValue
		}
		register := binary.BigEndian.Uint16(data[0:2])
		numRegs := binary.BigEndian.Uint16(data[2:4])
		
		if numRegs < 1 || numRegs > 125 {
			return []byte{}, &mbserver.IllegalDataValue
		}
		// 我们把 HoldingRegisters 和 InputRegisters 当成同一个数据源
		endRegister := int(register) + int(numRegs)
		if endRegister > 65536 {
			return []byte{}, &mbserver.IllegalDataAddress
		}
		return append([]byte{byte(numRegs * 2)}, mbserver.Uint16ToBytes(srv.HoldingRegisters[register:endRegister])...), &mbserver.Success
	})

	// 写多个寄存器 0x10，一次最多写 123 个
	s.srv.RegisterFunctionHandler(16, func(srv *mbserver.Server, frame mbserver.Framer) ([]byte, *mbserver.Exception) {
		if exc := s.checkUnitID(frame); exc != nil {
			return []byte{}, exc
		}
		data := frame.GetData()
		if len(data) < 5 {
			return []byte{}, &mbserver.IllegalDataValue
		}
		register := binary.BigEndian.Uint16(data[0:2])
		numRegs := binary.BigEndian.Uint16(data[2:4])
		byteCount := int(data[4])
		
		if numRegs < 1 || numRegs > 123 || byteCount != int(numRegs*2) || len(data) < 5+byteCount {
			return []byte{}, &mbserver.IllegalDataValue
		}
		endRegister := int(register) + int(numRegs)
		if endRegister > 65536 {
			return []byte{}, &mbserver.IllegalDataAddress
		}
		
		// 写入到内部存储
		values := mbserver.BytesToUint16(data[5 : 5+byteCount])
		for i, v := range values {
			srv.HoldingRegisters[int(register)+i] = v
		}
		
		return data[0:4], &mbserver.Success
	})

	// 屏蔽不支持的或仅做 Unit ID 校验的常见功能码
	unsupportedFunc := func(srv *mbserver.Server, frame mbserver.Framer) ([]byte, *mbserver.Exception) {
		if exc := s.checkUnitID(frame); exc != nil {
			return []byte{}, exc
		}
		return []byte{}, &mbserver.IllegalFunction
	}
	for _, fc := range []uint8{1, 2, 5, 6, 15} {
		s.srv.RegisterFunctionHandler(fc, unsupportedFunc)
	}

	err := s.srv.ListenTCP(fmt.Sprintf("0.0.0.0:%d", port))
	if err != nil {
		return nil, err
	}

	if rtuPort != "" {
		log.Printf("Note: RTU server on %s is requested, but current modbus library only supports TCP server. Please use Modbus TCP (port %d).", rtuPort, port)
	}

	return s, nil
}

func (s *Server) Start() error {
	// mbserver.ListenTCP already started it in NewServer
	return nil
}

func (s *Server) Stop() error {
	close(s.stopChan)
	s.srv.Close()
	return nil
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
	
	// 保留旧版代码读取方式 (100,102,104) 供旧系统使用
	var thc, ch4, nmhc float64
	for _, p := range res.Pollutants {
		if p.Code == "THC" || p.Name == "总烃" {
			thc = p.Amount
		}
		if p.Code == "CH4" || p.Name == "甲烷" {
			ch4 = p.Amount
		}
	}
	for _, g := range res.Groups {
		if g.Code == "NMHC" || g.Name == "非甲烷总烃" {
			nmhc = g.Amount
		}
	}
	// 兼容旧接口的 BigEndian ABCD (如果有的话)，但为了统一，我们这里全部改用标准要求的 CDAB 交换字节序
	s.setFloat32CDAB(100, float32(thc))
	s.setFloat32CDAB(102, float32(ch4))
	s.setFloat32CDAB(104, float32(nmhc))

	// 检测结果时间 161~166 (年,月,日,时,分,秒)
	t, err := time.Parse(time.RFC3339, res.CreatedAt)
	if err != nil {
		t = time.Now()
	}
	s.srv.HoldingRegisters[161] = uint16(t.Year())
	s.srv.HoldingRegisters[162] = uint16(t.Month())
	s.srv.HoldingRegisters[163] = uint16(t.Day())
	s.srv.HoldingRegisters[164] = uint16(t.Hour())
	s.srv.HoldingRegisters[165] = uint16(t.Minute())
	s.srv.HoldingRegisters[166] = uint16(t.Second())

	// 采样时间 201~206 (由于暂无独立字段，同上)
	s.srv.HoldingRegisters[201] = uint16(t.Year())
	s.srv.HoldingRegisters[202] = uint16(t.Month())
	s.srv.HoldingRegisters[203] = uint16(t.Day())
	s.srv.HoldingRegisters[204] = uint16(t.Hour())
	s.srv.HoldingRegisters[205] = uint16(t.Minute())
	s.srv.HoldingRegisters[206] = uint16(t.Second())

	// 按规范严格映射组分
	for _, p := range res.Pollutants {
		idx := -1
		name := p.Name
		code := p.Code
		if code == "THC" || name == "总烃" {
			idx = 0
		} else if code == "CH4" || name == "甲烷" {
			idx = 1
		} else if code == "Benzene" || name == "苯" {
			idx = 3
		} else if code == "Toluene" || name == "甲苯" {
			idx = 4
		} else if code == "Ethylbenzene" || name == "乙苯" {
			idx = 5
		} else if code == "m-Xylene" || name == "间二甲苯" || name == "间,对-二甲苯" || code == "m,p-Xylene" {
			idx = 6
		} else if code == "o-Xylene" || name == "邻二甲苯" {
			idx = 7
		}

		if idx >= 0 {
			baseConcentration := uint16(211 + idx*2)
			baseArea := uint16(231 + idx*2)
			baseHeight := uint16(251 + idx*2)

			s.setFloat32CDAB(baseConcentration, float32(p.Amount))
			s.setFloat32CDAB(baseArea, float32(p.Area))
			s.setFloat32CDAB(baseHeight, float32(p.Height))

			// 文档要求：只有总烃(idx=0)和甲烷(idx=1)有测量保留时间(675, 677)
			// 苯系物没有测量保留时间，避免覆写 679/681 等校准保留时间
			if idx == 0 {
				s.setFloat32CDAB(675, float32(p.RtS/60.0))
			} else if idx == 1 {
				s.setFloat32CDAB(677, float32(p.RtS/60.0))
			}
		}
	}

	// 提取 NMHC (从 Groups 中)
	for _, g := range res.Groups {
		if g.Code == "NMHC" || g.Name == "非甲烷总烃" {
			s.setFloat32CDAB(215, float32(g.Amount)) // NMHC 是第3组分，基址 215
		}
	}
}

// SetDeviceNo writes device unique code to 801+ (24 bytes max)
func (s *Server) SetDeviceNo(deviceNo string) {
	s.mu.Lock()
	defer s.mu.Unlock()

	// Clear 801 to 812
	for i := uint16(801); i <= 812; i++ {
		s.srv.HoldingRegisters[i] = 0
	}

	if len(deviceNo) > 24 {
		deviceNo = deviceNo[:24]
	}
	s.setASCII(801, deviceNo)
}

// SetFloat32 暴露给外部调用，用于更新单个 float32 寄存器 (CDAB 顺序)
func (s *Server) SetFloat32(addr uint16, v float32) {
	s.mu.Lock()
	defer s.mu.Unlock()
	s.setFloat32CDAB(addr, v)
}

// SetUint16 暴露给外部调用，用于更新单个 uint16 寄存器
func (s *Server) SetUint16(addr uint16, v uint16) {
	s.mu.Lock()
	defer s.mu.Unlock()
	s.srv.HoldingRegisters[addr] = v
}

func (s *Server) setFloat32CDAB(addr uint16, v float32) {
	bits := math.Float32bits(v)
	// VOC 动态管控协议规范（文档示例：260.0 对应 00 00 43 82）
	// 表示 Modbus 传输为 CDAB 顺序（即低字在前，高字在后）
	s.srv.HoldingRegisters[addr] = uint16(bits & 0xFFFF)
	s.srv.HoldingRegisters[addr+1] = uint16(bits >> 16)
}

func (s *Server) setFloat32(addr uint16, v float32) {
	s.setFloat32CDAB(addr, v)
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
		s.srv.HoldingRegisters[addr+uint16(i/2)] = (uint16(high) << 8) | uint16(low)
	}
}

// PushLog pushes a log message into the queue to be written to Modbus registers (700-799)
func (s *Server) PushLog(msg string) {
	select {
	case s.logQueue <- msg:
	default:
		// Queue full, drop log to prevent blocking
		log.Printf("Modbus log queue full, dropped log: %s", msg)
	}
}

func (s *Server) logWorker() {
	ticker := time.NewTicker(15 * time.Second)
	defer ticker.Stop()

	for {
		select {
		case <-s.stopChan:
			return
		case <-ticker.C:
			// Drain all available logs or just process one?
			// The requirement says: "如果产生多条日志就间隔15秒覆盖一次"
			// Meaning we should write one log every 15 seconds if there are any.
			select {
			case msg := <-s.logQueue:
				s.writeLogToRegisters(msg)
			default:
				// Queue empty
			}
		}
	}
}

func (s *Server) writeLogToRegisters(msg string) {
	now := time.Now()
	// Time format: YYYYMMDDHHMMSS (14 chars)
	timestamp := now.Format("20060102150405")
	
	buf := make([]byte, 200)
	copy(buf[0:14], []byte(timestamp))
	
	msgBytes := []byte(msg)
	if len(msgBytes) > 186 {
		msgBytes = msgBytes[:186]
	}
	copy(buf[14:], msgBytes)
	
	s.mu.Lock()
	defer s.mu.Unlock()
	for i := 0; i < 100; i++ {
		high := buf[i*2]
		low := buf[i*2+1]
		s.srv.HoldingRegisters[700+uint16(i)] = (uint16(high) << 8) | uint16(low)
	}
}