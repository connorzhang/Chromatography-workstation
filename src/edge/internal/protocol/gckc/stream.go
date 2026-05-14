package gckc

import (
	"bytes"
	"encoding/binary"
)

type StreamDecoder struct {
	buf []byte
}

func (d *StreamDecoder) Push(p []byte) {
	d.buf = append(d.buf, p...)
}

func (d *StreamDecoder) Next() (Frame, bool, error) {
	for {
		if len(d.buf) < 6 {
			return Frame{}, false, nil
		}
		idx := bytes.Index(d.buf, headerBytes)
		if idx < 0 {
			if len(d.buf) > 3 {
				d.buf = d.buf[len(d.buf)-3:]
			}
			return Frame{}, false, nil
		}
		if idx > 0 {
			d.buf = d.buf[idx:]
			continue
		}
		if len(d.buf) < 7 {
			return Frame{}, false, nil
		}
		blen := int(binary.BigEndian.Uint16(d.buf[4:6]))
		total := blen + 7
		if total <= 0 {
			d.buf = d.buf[4:]
			continue
		}
		if len(d.buf) < total {
			return Frame{}, false, nil
		}
		raw := d.buf[:total]
		d.buf = d.buf[total:]
		f, err := Decode(raw)
		if err != nil {
			continue
		}
		return f, true, nil
	}
}
