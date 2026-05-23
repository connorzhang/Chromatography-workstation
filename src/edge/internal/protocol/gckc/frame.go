package gckc

import (
	"bytes"
	"encoding/binary"
	"errors"
)

var headerBytes = []byte{'G', 'C', 'K', 'C'}

type Frame struct {
	DeviceID string
	Seq      uint16
	Cmd      byte
	Payload  []byte
}

func Encode(f Frame) ([]byte, error) {
	dev := make([]byte, 16)
	copy(dev, []byte(f.DeviceID))
	body := make([]byte, 0, 16+2+1+len(f.Payload))
	body = append(body, dev...)
	seq := make([]byte, 2)
	binary.BigEndian.PutUint16(seq, f.Seq)
	body = append(body, seq...)
	body = append(body, f.Cmd)
	body = append(body, f.Payload...)

	out := make([]byte, 0, 4+2+len(body)+1)
	out = append(out, headerBytes...)
	lenBE := make([]byte, 2)
	binary.BigEndian.PutUint16(lenBE, uint16(len(body)))
	out = append(out, lenBE...)
	out = append(out, body...)
	out = append(out, CRC(body))
	return out, nil
}

func Decode(frame []byte) (Frame, error) {
	if len(frame) < 7 {
		return Frame{}, errors.New("frame too short")
	}
	if !bytes.Equal(frame[:4], headerBytes) {
		return Frame{}, errors.New("bad header")
	}
	blen := int(binary.BigEndian.Uint16(frame[4:6]))
	if blen < 19 {
		return Frame{}, errors.New("body too short")
	}
	if len(frame) != blen+7 {
		return Frame{}, errors.New("length mismatch")
	}
	body := frame[6 : 6+blen]
	crc := frame[len(frame)-1]
	if CRC(body) != crc {
		return Frame{}, errors.New("crc mismatch")
	}
	devRaw := body[:16]
	// 兼容真实硬件可能填充 \x00 或者 空格 的情况
	devRaw = bytes.TrimRight(devRaw, "\x00 ")
	seq := binary.BigEndian.Uint16(body[16:18])
	cmd := body[18]
	payload := body[19:]
	return Frame{DeviceID: string(devRaw), Seq: seq, Cmd: cmd, Payload: append([]byte(nil), payload...)}, nil
}
