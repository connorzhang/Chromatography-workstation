package main

import (
	"encoding/hex"
	"fmt"
	"sort"
)

type EventRow struct {
	Time      float64 `json:"time"`
	EventMask int     `json:"event_mask"`
}

func bcd3BToFloat(b []byte) float64 {
	if len(b) < 3 {
		return 0
	}
	v := int(b[0]>>4)*100000 + int(b[0]&0x0F)*10000 +
		int(b[1]>>4)*1000 + int(b[1]&0x0F)*100 +
		int(b[2]>>4)*10 + int(b[2]&0x0F)
	return float64(v) / 100.0
}

func parseEventTable(payload []byte) *[4][8]float64 {
	if len(payload) < 96 {
		return nil
	}
	var m [4][8]float64
	idx := 0
	for ch := 0; ch < 4; ch++ {
		for act := 0; act < 8; act++ {
			m[ch][act] = bcd3BToFloat(payload[idx : idx+3])
			idx += 3
		}
	}
	return &m
}

func matrixToEvents(m [8][8]float64) []EventRow {
	timeSet := make(map[float64]bool)
	for ch := 0; ch < 8; ch++ {
		for act := 0; act < 8; act++ {
			if t := m[ch][act]; t > 0 {
				timeSet[t] = true
			}
		}
	}
	var times []float64
	for t := range timeSet {
		times = append(times, t)
	}
	sort.Float64s(times)

	var events []EventRow
	var currentMask int
	for _, t := range times {
		for ch := 0; ch < 8; ch++ {
			if m[ch][0] == t || m[ch][2] == t || m[ch][4] == t || m[ch][6] == t {
				currentMask |= (1 << ch)
			}
			if m[ch][1] == t || m[ch][3] == t || m[ch][5] == t || m[ch][7] == t {
				currentMask &^= (1 << ch)
			}
		}
		events = append(events, EventRow{Time: t, EventMask: currentMask})
	}
	return events
}

func eventsToMatrix(events []EventRow) [8][8]float64 {
	var m [8][8]float64
	var prevMask int
	for _, evt := range events {
		mask := evt.EventMask
		t := evt.Time
		for ch := 0; ch < 8; ch++ {
			wasOn := (prevMask & (1 << ch)) != 0
			isOn := (mask & (1 << ch)) != 0
			if !wasOn && isOn {
				if m[ch][0] == 0 { m[ch][0] = t } else if m[ch][2] == 0 { m[ch][2] = t } else if m[ch][4] == 0 { m[ch][4] = t } else if m[ch][6] == 0 { m[ch][6] = t }
			}
			if wasOn && !isOn {
				if m[ch][1] == 0 { m[ch][1] = t } else if m[ch][3] == 0 { m[ch][3] = t } else if m[ch][5] == 0 { m[ch][5] = t } else if m[ch][7] == 0 { m[ch][7] = t }
			}
		}
		prevMask = mask
	}
	return m
}

func main() {
	payloadStr := "000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000001000120000000000000000000000000000000000000"
	payload, _ := hex.DecodeString(payloadStr)
	
	m := parseEventTable(payload)
	
	var matrix [8][8]float64
	for ch := 0; ch < 4; ch++ {
		matrix[ch+4] = m[ch]
	}
	
	events := matrixToEvents(matrix)
	fmt.Printf("Events: %+v\n", events)
}