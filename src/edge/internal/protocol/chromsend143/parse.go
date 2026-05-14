package chromsend143

import (
	"errors"
	"math"
)

type Config struct {
	ShuaiJian1 float64
	ShuaiJian2 float64
	ShuaiJian3 float64
}

type Parsed struct {
	Channel int
	Freq10  int
	Values  []float64
}

func Parse(payload []byte, cfg Config) (Parsed, bool, error) {
	ps, has, err := ParseAll(payload, cfg)
	if err != nil || !has {
		return Parsed{}, has, err
	}
	if len(ps) == 0 {
		return Parsed{}, false, nil
	}
	return ps[0], true, nil
}

func ParseAll(payload []byte, cfg Config) ([]Parsed, bool, error) {
	if cfg.ShuaiJian1 <= 0 {
		cfg.ShuaiJian1 = 1
	}
	if cfg.ShuaiJian2 <= 0 {
		cfg.ShuaiJian2 = 1
	}
	if cfg.ShuaiJian3 <= 0 {
		cfg.ShuaiJian3 = 1
	}

	if len(payload) < 20 {
		return nil, false, errors.New("payload too short")
	}
	count := int(payload[18])
	if count == 0 {
		return nil, false, nil
	}
	idx := 19
	out := make([]Parsed, 0, count)
	for d := 0; d < count; d++ {
		if idx+4 > len(payload) {
			return out, true, errors.New("payload truncated")
		}
		detType := payload[idx]
		_ = payload[idx+1]
		_ = payload[idx+2]
		freqByte := payload[idx+3]
		idx += 4
		points := int(freqByte) * 10
		if points <= 0 {
			return out, true, errors.New("invalid frequency")
		}
		availablePoints := (len(payload) - idx) / 4
		if availablePoints < 0 {
			availablePoints = 0
		}
		decodePoints := points
		truncated := false
		if availablePoints < points {
			decodePoints = availablePoints
			truncated = true
		}
		chn := channelFromDetectorType(detType)
		values := make([]float64, 0, points)
		for i := 0; i < decodePoints; i++ {
			b0 := payload[idx]
			b1 := payload[idx+1]
			b2 := payload[idx+2]
			b3 := payload[idx+3]
			idx += 4

			flagNeg := (b0 & (1 << 4)) != 0
			b0 = b0 & 0x0F

			d0 := float64(b0)
			d1 := float64((b1 & 0xF0) >> 4)
			d2 := float64(b1 & 0x0F)
			d3 := float64((b2 & 0xF0) >> 4)
			d4 := float64(b2 & 0x0F)
			d5 := float64((b3 & 0xF0) >> 4)
			d6 := float64(b3 & 0x0F)
			raw := d0 + d1*0.1 + d2*0.01 + d3*0.001 + d4*0.0001 + d5*0.00001 + d6*0.000001
			if flagNeg {
				raw = -raw
			}
			v := transform(detType, raw, cfg)
			values = append(values, v)
		}
		if truncated {
			pad := 0.0
			if len(values) > 0 {
				pad = values[len(values)-1]
			}
			for i := len(values); i < points; i++ {
				values = append(values, pad)
			}
			out = append(out, Parsed{Channel: chn, Freq10: int(freqByte) * 10, Values: values})
			return out, true, nil
		}
		out = append(out, Parsed{Channel: chn, Freq10: int(freqByte) * 10, Values: values})
	}

	return out, true, nil
}

func channelFromDetectorType(detType byte) int {
	switch detType {
	case 64:
		return 0
	case 65:
		return 1
	case 80:
		return 2
	case 81:
		return 3
	default:
		return 0
	}
}

func transform(detType byte, raw float64, cfg Config) float64 {
	v := raw
	switch detType {
	case 64, 65, 66, 67, 160, 161, 162, 163:
		v = v * 1000
		v = (v * v) / 1500
	}
	switch detType {
	case 64, 160:
		v = v / cfg.ShuaiJian1
	case 65, 161:
		v = v / cfg.ShuaiJian2
	case 112:
		v = v / cfg.ShuaiJian3
	}
	v = v * 1000
	if math.IsNaN(v) || math.IsInf(v, 0) {
		return 0
	}
	return v
}
