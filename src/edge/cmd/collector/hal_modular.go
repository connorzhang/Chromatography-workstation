package main

import (
	"errors"
	"log"
)

// ModularDriver implements the InstrumentDriver interface for the new modular architecture.
type ModularDriver struct {
	st       *deviceState
	deviceID string
}

func NewModularDriver(st *deviceState, deviceID string) *ModularDriver {
	return &ModularDriver{
		st:       st,
		deviceID: deviceID,
	}
}

// -- TempDriver Implementation --

func (d *ModularDriver) StartTempControl() error {
	log.Println("[ModularDriver] StartTempControl: Temp control is automatically handled by the Modbus module.")
	return nil
}

func (d *ModularDriver) StopTempControl() error {
	log.Println("[ModularDriver] StopTempControl: Setting all setpoints to 0.")
	modbusTempCtrlMu.Lock()
	ctrl := globalModbusTempCtrl
	modbusTempCtrlMu.Unlock()

	if ctrl == nil {
		return errors.New("modbus temp controller not connected")
	}

	for i := 1; i <= 8; i++ {
		_ = ctrl.SetTemperature(i, 0)
	}
	return nil
}

func (d *ModularDriver) QueryTempSetpoints() error {
	// Modbus controller state is polled by the frontend or read directly. No-op here.
	return nil
}

func (d *ModularDriver) SetTempSetpoints(setpoints []float64, protects []float64, enables []bool) error {
	modbusTempCtrlMu.Lock()
	ctrl := globalModbusTempCtrl
	modbusTempCtrlMu.Unlock()

	if ctrl == nil {
		return errors.New("modbus temp controller not connected")
	}

	// In the original legacy layout, there are 6 channels:
	// 0: Inj1, 1: Col, 2: Det1, 3: Inj2, 4: Det2, 5: Det3
	// We map them sequentially to Modbus CH1 - CH6.
	for i, sp := range setpoints {
		if i >= 6 {
			break
		}
		ch := i + 1
		target := int16(sp)
		if i < len(enables) && !enables[i] {
			target = 0 // Disable by setting to 0
		}
		err := ctrl.SetTemperature(ch, target)
		if err != nil {
			log.Printf("[ModularDriver] Failed to set temp for CH%d: %v", ch, err)
			return err
		}
	}
	return nil
}

// -- EventDriver Implementation --

func (d *ModularDriver) QueryEvents() error {
	// Modular 模式下事件配置存储在本地 HardwareConfig，无需查询硬件
	return nil
}

func (d *ModularDriver) SetEvents(matrix [8][8]float64) error {
	// 将事件矩阵转换为 EventRow 列表并保存到 HardwareConfig
	events := matrixToEvents(matrix)
	hw, _ := pstore.LoadHardwareConfig(d.deviceID)
	hw.Events = events
	pstore.SaveHardwareConfig(d.deviceID, hw)
	log.Printf("[ModularDriver] SetEvents: saved %d event rows to HardwareConfig", len(events))
	return nil
}

// -- EPCDriver Implementation --

func (d *ModularDriver) SetEPC(epcs map[string]float64) error {
	log.Println("[ModularDriver] SetEPC: Not yet implemented for modular hardware")
	return ErrNotSupported
}

// -- CycleDriver Implementation --

func (d *ModularDriver) QueryCycleParams() error {
	log.Println("[ModularDriver] QueryCycleParams: Cycle handled locally, no hardware query needed")
	return nil
}

func (d *ModularDriver) SetCycleParams(count int, intervalMin float64) error {
	log.Printf("[ModularDriver] SetCycleParams: count=%d, interval=%.1f (handled by engine scheduler)", count, intervalMin)
	return nil
}

// -- IgniteDriver Implementation --

func (d *ModularDriver) QueryIgniteParams() error {
	log.Println("[ModularDriver] QueryIgniteParams: Not yet implemented for modular hardware")
	return ErrNotSupported
}

func (d *ModularDriver) SetIgniteParams(threshold1, threshold2 byte, durationByte byte) error {
	log.Println("[ModularDriver] SetIgniteParams: Not yet implemented for modular hardware")
	return ErrNotSupported
}

func (d *ModularDriver) Ignite(detector string, start bool) error {
	log.Printf("[ModularDriver] Ignite: detector=%s, start=%v. Not yet implemented.", detector, start)
	return ErrNotSupported
}

// -- AnalysisDriver Implementation --

func (d *ModularDriver) StartAnalysis(channel byte) error {
	log.Println("[ModularDriver] StartAnalysis: Triggering local start for modular hardware")
	if channel == 0xFF {
		for ch := 0; ch < 8; ch++ {
			resetSession(d.st, ch)
		}
	} else {
		resetSession(d.st, int(channel))
	}
	return nil
}

func (d *ModularDriver) StopAnalysis() error {
	log.Println("[ModularDriver] StopAnalysis: Triggering local stop for modular hardware")
	for ch := 0; ch < 8; ch++ {
		d.st.mu.Lock()
		if d.st.sessions != nil && d.st.sessions[ch] != nil {
			d.st.sessions[ch].active = false
		}
		d.st.mu.Unlock()
	}
	return nil
}

func (d *ModularDriver) StopAnalysisChannel(channel byte) error {
	log.Printf("[ModularDriver] StopAnalysisChannel: Triggering local stop for channel %d", channel)
	ch := int(channel)
	if ch >= 0 && ch < 8 {
		d.st.mu.Lock()
		if d.st.sessions != nil && d.st.sessions[ch] != nil {
			d.st.sessions[ch].active = false
		}
		d.st.mu.Unlock()
	}
	return nil
}

func (d *ModularDriver) RequestStop(channelMask byte) error {
	log.Println("[ModularDriver] RequestStop: Triggering local stop for modular hardware")
	for ch := 0; ch < 8; ch++ {
		if (channelMask & (1 << ch)) != 0 {
			d.st.mu.Lock()
			if d.st.sessions != nil && d.st.sessions[ch] != nil {
				d.st.sessions[ch].active = false
			}
			d.st.mu.Unlock()
		}
	}
	return nil
}

func (d *ModularDriver) SendRawCmd(cmd byte, payload []byte) error {
	return errors.New("ModularDriver does not support SendRawCmd (legacy binary protocol)")
}

func (d *ModularDriver) Capabilities() Capabilities {
	return Capabilities{
		HasIgnition: false,
		HasCycles:   true, // TCD 也可以支持前端下发循环次数和间隔
		HasEPC:      true,
		HasEvents:   true, // 通过温控模块 IO CH5-8 实现开关量事件控制
		Detectors:   []string{"TCD1"},
	}
}
