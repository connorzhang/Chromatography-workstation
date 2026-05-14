package main

import (
	"fmt"
	"math"
	"net"
	"os"
	"strconv"
	"time"

	"chromatography-workstation/edge/internal/protocol/gckc"
)

func main() {
	host := getenv("EDGE_SIM_HOST", "127.0.0.1")
	port := envInt("EDGE_SIM_PORT", 25001)
	deviceID := getenv("EDGE_SIM_DEVICE_ID", "DEV0000000000001")
	chnType := envInt("EDGE_SIM_DETECTOR_TYPE", 64)
	freqByte := envInt("EDGE_SIM_FREQ_BYTE", 1)
	periodMs := envInt("EDGE_SIM_PERIOD_MS", 1000)

	addr := fmt.Sprintf("%s:%d", host, port)
	c, err := net.Dial("tcp", addr)
	if err != nil {
		return
	}
	defer c.Close()

	seq := uint16(1)
	ticker := time.NewTicker(time.Duration(periodMs) * time.Millisecond)
	defer ticker.Stop()

	phase := 0.0
	for range ticker.C {
		payload := build143Payload(byte(chnType), byte(freqByte), phase)
		phase += 0.25
		frame, _ := gckc.Encode(gckc.Frame{DeviceID: deviceID, Seq: seq, Cmd: 143, Payload: payload})
		seq++
		_, _ = c.Write(frame)
	}
}

func build143Payload(detType byte, freqByte byte, phase float64) []byte {
	payload := make([]byte, 0, 64)
	for i := 0; i < 12; i++ {
		payload = append(payload, 0)
	}
	for i := 0; i < 6; i++ {
		payload = append(payload, 0)
	}
	payload = append(payload, 1)

	payload = append(payload, detType)
	payload = append(payload, 0)
	payload = append(payload, 0)
	payload = append(payload, freqByte)

	points := int(freqByte) * 10
	for i := 0; i < points; i++ {
		t := float64(i) / float64(points)
		v := 0.003873 * (0.15 + 0.85*math.Exp(-math.Pow((t-0.5)*3, 2)))
		v = v * (1 + 0.05*math.Sin(phase))
		payload = append(payload, encode7Digits(v)...)
	}

	return payload
}

func encode7Digits(v float64) []byte {
	if v < 0 {
		v = -v
	}
	if v > 9.999999 {
		v = 9.999999
	}
	x := int(math.Round(v * 1_000_000))
	if x > 9_999_999 {
		x = 9_999_999
	}
	d0 := (x / 1_000_000) % 10
	d1 := (x / 100_000) % 10
	d2 := (x / 10_000) % 10
	d3 := (x / 1_000) % 10
	d4 := (x / 100) % 10
	d5 := (x / 10) % 10
	d6 := x % 10

	b0 := byte(d0)
	b1 := byte((d1 << 4) | d2)
	b2 := byte((d3 << 4) | d4)
	b3 := byte((d5 << 4) | d6)
	return []byte{b0, b1, b2, b3}
}

func envInt(name string, def int) int {
	v := os.Getenv(name)
	if v == "" {
		return def
	}
	n, err := strconv.Atoi(v)
	if err != nil {
		return def
	}
	return n
}

func getenv(name string, def string) string {
	v := os.Getenv(name)
	if v == "" {
		return def
	}
	return v
}
