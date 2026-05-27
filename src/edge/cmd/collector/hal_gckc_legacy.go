package main

import (
	"errors"
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
	return sendCmd(d.st, d.deviceID, 17, nil)
}

func (d *LegacyGCKCDriver) StopTempControl() error {
	return sendCmd(d.st, d.deviceID, 16, nil)
}

func (d *LegacyGCKCDriver) QueryTempSetpoints() error {
	return sendCmd(d.st, d.deviceID, 0, nil)
}

func (d *LegacyGCKCDriver) SetTempSetpoints(setpoints []float64, protects []float64) error {
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

	copy(payload[0:2], tempToBCD2(setpoints[0]))
	copy(payload[2:4], tempToBCD2(setpoints[1]))
	copy(payload[4:6], tempToBCD2(setpoints[2]))
	copy(payload[8:10], tempToBCD2(setpoints[3]))

	copy(payload[12:14], tempToBCD2(protects[0]))
	copy(payload[14:16], tempToBCD2(protects[1]))
	copy(payload[16:18], tempToBCD2(protects[2]))
	copy(payload[18:20], tempToBCD2(protects[3]))

	return sendCmd(d.st, d.deviceID, 8, payload)
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
