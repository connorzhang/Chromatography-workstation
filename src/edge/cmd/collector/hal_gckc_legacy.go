package main

import (
	"errors"
	"math"
)

// LegacyGCKCDriver implements the InstrumentDriver interface for the original GCKC motherboard.
type LegacyGCKCDriver struct {
	st       *deviceState
	deviceID string
}

func NewLegacyGCKCDriver(st *deviceState, deviceID string) *LegacyGCKCDriver {
	return &LegacyGCKCDriver{
		st:       st,
		deviceID: deviceID,
	}
}

// -- TempDriver Implementation --

func (d *LegacyGCKCDriver) StartTempControl() error {
	// Cmd 16 在旧版 GCKC 主板上实际上是一个 Toggle (反转) 指令
	return sendCmd(d.st, d.deviceID, 16, nil)
}

func (d *LegacyGCKCDriver) StopTempControl() error {
	// Cmd 17 实际上无效。由于 Cmd 16 是 Toggle，再次发送 Cmd 16 即可关闭控温
	return sendCmd(d.st, d.deviceID, 16, nil)
}

func (d *LegacyGCKCDriver) QueryTempSetpoints() error {
	return sendCmd(d.st, d.deviceID, 0, nil)
}

func tempToBCD2(temp float64) []byte {
	v := int(math.Round(temp * 10))
	if v < 0 {
		v = 0
	}
	if v > 3999 {
		v = 3999
	}
	// v 此时形如 1234 (对应 123.4度)
	d1 := (v / 1000) % 10
	d2 := (v / 100) % 10
	d3 := (v / 10) % 10
	d4 := v % 10
	b0 := byte((d1 << 4) | d2)
	b1 := byte((d3 << 4) | d4)
	return []byte{b0, b1}
}

func (d *LegacyGCKCDriver) SetTempSetpoints(setpoints []float64, protects []float64, enables []bool) error {
	if len(setpoints) != 6 || len(protects) != 6 {
		return errors.New("invalid temperature array length, expected 6")
	}

	payload := make([]byte, 24)

	// Original GCKC mapping logic:
	// setpoints[0]: Inj1
	// setpoints[1]: Col
	// setpoints[2]: Det1
	// setpoints[3]: Inj2
	// setpoints[4]: Det2
	// setpoints[5]: Det3
	// And similar for protects

	copy(payload[0:2], tempToBCD2(setpoints[0]))   // Inj1
	copy(payload[2:4], tempToBCD2(setpoints[1]))   // Col
	copy(payload[4:6], tempToBCD2(setpoints[2]))   // Det1
	copy(payload[6:8], tempToBCD2(setpoints[4]))   // Det2
	copy(payload[8:10], tempToBCD2(setpoints[3]))  // Inj2
	copy(payload[10:12], tempToBCD2(setpoints[5])) // Det3

	copy(payload[12:14], tempToBCD2(protects[0])) // Inj1
	copy(payload[14:16], tempToBCD2(protects[1])) // Col
	copy(payload[16:18], tempToBCD2(protects[2])) // Det1
	copy(payload[18:20], tempToBCD2(protects[4])) // Det2
	copy(payload[20:22], tempToBCD2(protects[3])) // Inj2
	copy(payload[22:24], tempToBCD2(protects[5])) // Det3

	err := sendCmd(d.st, d.deviceID, 8, payload)
	if err != nil {
		return err
	}

	// 补发 Cmd 67 (控温使能设置)
	var enableMask byte
	if len(enables) == 6 {
		if enables[0] {
			enableMask |= (1 << 5)
		} // Inj1
		if enables[1] {
			enableMask |= (1 << 4)
		} // Col
		if enables[2] {
			enableMask |= (1 << 3)
		} // Det1
		if enables[3] {
			enableMask |= (1 << 1)
		} // Inj2 (Row 3 -> Bit 1)
		if enables[4] {
			enableMask |= (1 << 2)
		} // Det2 (Row 4 -> Bit 2)
		if enables[5] {
			enableMask |= (1 << 0)
		} // Det3/Aux
	}
	return sendCmd(d.st, d.deviceID, 67, []byte{enableMask})
}

// -- EventDriver Implementation --

func (d *LegacyGCKCDriver) QueryEvents() error {
	if err := sendCmd(d.st, d.deviceID, 2, []byte{}); err != nil {
		return err
	}
	return sendCmd(d.st, d.deviceID, 100, []byte{})
}

func (d *LegacyGCKCDriver) SetEvents(matrix [8][8]float64) error {
	var m0, m1 [4][8]float64
	for ch := 0; ch < 4; ch++ {
		m0[ch] = matrix[ch]
		m1[ch] = matrix[ch+4]
	}

	payload0 := buildEventPayload(m0)
	payload1 := buildEventPayload(m1)

	if err := sendCmd(d.st, d.deviceID, 10, payload0); err != nil {
		return err
	}
	return sendCmd(d.st, d.deviceID, 101, payload1)
}

func buildEventPayload(m [4][8]float64) []byte {
	payload := make([]byte, 96)
	idx := 0
	for ch := 0; ch < 4; ch++ {
		for act := 0; act < 8; act++ {
			copy(payload[idx:idx+3], floatToBcd3B(m[ch][act]))
			idx += 3
		}
	}
	return payload
}

// -- EPCDriver Implementation --

func (d *LegacyGCKCDriver) SetEPC(payload []byte) error {
	return sendCmd(d.st, d.deviceID, 34, payload)
}

// -- AnalysisDriver Implementation --

func (d *LegacyGCKCDriver) StartAnalysis(channel byte) error {
	// If channel is 0xFF, it means start all (Cmd 18). Let's use that convention, or define separate.
	if channel == 0xFF {
		return sendCmd(d.st, d.deviceID, 18, nil)
	}
	return sendCmd(d.st, d.deviceID, 22, []byte{channel})
}

func (d *LegacyGCKCDriver) StopAnalysis() error {
	return sendCmd(d.st, d.deviceID, 19, nil)
}

func (d *LegacyGCKCDriver) RequestStop(channelMask byte) error {
	return sendCmd(d.st, d.deviceID, 245, []byte{channelMask})
}

func (d *LegacyGCKCDriver) Ignite(detector string, start bool) error {
	cmd := byte(20) // Default FID1
	if detector == "FID2" {
		cmd = byte(21)
	}
	if !start {
		cmd += 1
	}
	return sendCmd(d.st, d.deviceID, cmd, nil)
}

func (d *LegacyGCKCDriver) SendRawCmd(cmd byte, payload []byte) error {
	return sendCmd(d.st, d.deviceID, cmd, payload)
}
