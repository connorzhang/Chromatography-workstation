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
	log.Println("[ModularDriver] QueryEvents: Not yet implemented for modular hardware")
	return nil
}

func (d *ModularDriver) SetEvents(matrix [8][8]float64) error {
	log.Println("[ModularDriver] SetEvents: Not yet implemented for modular hardware")
	return nil
}

// -- EPCDriver Implementation --

func (d *ModularDriver) SetEPC(payload []byte) error {
	log.Println("[ModularDriver] SetEPC: Not yet implemented for modular hardware")
	return nil
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

func (d *ModularDriver) Ignite(detector string, start bool) error {
	log.Printf("[ModularDriver] Ignite: detector=%s, start=%v. Not yet implemented.", detector, start)
	return nil
}

func (d *ModularDriver) SendRawCmd(cmd byte, payload []byte) error {
	return errors.New("ModularDriver does not support SendRawCmd (legacy binary protocol)")
}
