package protocol

import (
	"bytes"
	"encoding/binary"
	"errors"
	"fmt"
)

// GCKCFrame represents the legacy IBrainChrom protocol frame
type GCKCFrame struct {
	Header   [4]byte
	Length   uint16
	DeviceID string // 16 bytes ASCII
	Seq      uint16
	Cmd      uint8
	Payload  []byte
	CRC      uint8
}

// DecodeFrame scans the buffer for the "GCKC" header and decodes the frame.
func DecodeFrame(data []byte) (*GCKCFrame, error) {
	if len(data) < 7 {
		return nil, errors.New("data too short")
	}

	idx := bytes.Index(data, []byte("GCKC"))
	if idx == -1 {
		return nil, errors.New("GCKC header not found")
	}

	frameStart := data[idx:]
	if len(frameStart) < 6 {
		return nil, errors.New("incomplete frame length")
	}

	length := binary.BigEndian.Uint16(frameStart[4:6])
	if len(frameStart) < int(length+7) {
		return nil, errors.New("incomplete frame body")
	}

	// Payload starts at offset 6. The actual payload includes DeviceID(16), Seq(2), Cmd(1), Data(N)
	// But according to the legacy docs, body is what comes after length.
	body := frameStart[6 : 6+length]
	crc := frameStart[6+length]

	// Verify CRC (Placeholder for IBrainConvert.BitByBitNo)
	calculatedCRC := calculateCRC(body)
	if crc != calculatedCRC {
		return nil, fmt.Errorf("CRC mismatch: expected %d, got %d", calculatedCRC, crc)
	}

	if len(body) < 19 {
		return nil, errors.New("body too short for header fields")
	}

	deviceID := string(bytes.TrimRight(body[0:16], "\x00"))
	seq := binary.BigEndian.Uint16(body[16:18])
	cmd := body[18]
	payload := body[19:]

	return &GCKCFrame{
		Header:   [4]byte{'G', 'C', 'K', 'C'},
		Length:   length,
		DeviceID: deviceID,
		Seq:      seq,
		Cmd:      cmd,
		Payload:  payload,
		CRC:      crc,
	}, nil
}

// calculateCRC mocks IBrainConvert.BitByBitNo
func calculateCRC(data []byte) uint8 {
	var crc uint8 = 0
	for _, b := range data {
		crc ^= b // simplified logic for placeholder
	}
	return crc
}

// EncodeFrame creates a byte slice for sending down to the instrument
func EncodeFrame(deviceID string, seq uint16, cmd uint8, payload []byte) []byte {
	bodyLen := 16 + 2 + 1 + len(payload)
	buf := make([]byte, 6+bodyLen+1)

	copy(buf[0:4], "GCKC")
	binary.BigEndian.PutUint16(buf[4:6], uint16(bodyLen))

	// Pad device ID to 16 bytes
	devIDBytes := make([]byte, 16)
	copy(devIDBytes, deviceID)
	copy(buf[6:22], devIDBytes)

	binary.BigEndian.PutUint16(buf[22:24], seq)
	buf[24] = cmd
	copy(buf[25:25+len(payload)], payload)

	buf[25+len(payload)] = calculateCRC(buf[6 : 6+bodyLen])
	return buf
}
