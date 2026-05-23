package main

import (
	"fmt"
	"math"
	"math/rand"
	"net"
	"os"
	"strconv"
	"sync"
	"time"

	"chromatography-workstation/edge/internal/protocol/gckc"
)

var (
	mu      sync.Mutex
	targets = map[string]float64{
		"Inj1": 120, "Col": 80, "Det1": 150,
		"Carrier1": 20, "H2": 30, "Air": 300,
	}
	actuals = map[string]float64{
		"Inj1": 25, "Col": 25, "Det1": 25,
		"Carrier1": 0, "H2": 0, "Air": 0,
	}
)

func main() {
	host := getenv("EDGE_SIM_HOST", "127.0.0.1")
	port := envInt("EDGE_SIM_PORT", 8000)
	deviceID := getenv("EDGE_SIM_DEVICE_ID", "69000000001ABCDEFG123456")
	chnType := envInt("EDGE_SIM_DETECTOR_TYPE", 64)
	freqByte := envInt("EDGE_SIM_FREQ_BYTE", 2) // 20 points per sec
	periodMs := envInt("EDGE_SIM_PERIOD_MS", 1000)

	addr := fmt.Sprintf("%s:%d", host, port)
	c, err := net.Dial("tcp", addr)
	if err != nil {
		fmt.Printf("Dial error: %v\n", err)
		return
	}
	defer c.Close()
	fmt.Printf("Connected to %s as %s\n", addr, deviceID)

	go func() {
		buf := make([]byte, 4096)
		decoder := &gckc.StreamDecoder{}
		for {
			n, err := c.Read(buf)
			if err != nil {
				return
			}
			decoder.Push(buf[:n])
			for {
				f, ok, _ := decoder.Next()
				if !ok {
					break
				}
				handleCmd(f)
			}
		}
	}()

	seq := uint16(1)
	ticker := time.NewTicker(time.Duration(periodMs) * time.Millisecond)
	defer ticker.Stop()

	t_sec := 0.0 // 整个循环周期内的时间
	dt := float64(periodMs) / 1000.0

	rand.Seed(time.Now().UnixNano())
	thcMod := 1.0
	ch4Mod := 1.0

	// 模拟器参数
	cycleS := 120.0 // 整个循环周期 2分钟
	acqS := 60.0    // 采集谱图时间 1分钟
	isAcquiring := false

	for range ticker.C {
		updateSimulation(dt)

		if t_sec == 0.0 {
			thcMod = 1.0 + (rand.Float64()*0.04 - 0.02)
			ch4Mod = 1.0 + (rand.Float64()*0.04 - 0.02)

			// Start of cycle: Send Answer 146 (Start)
			frame, _ := gckc.Encode(gckc.Frame{DeviceID: deviceID, Seq: seq, Cmd: 146, Payload: []byte{0}})
			seq++
			c.Write(frame)
			fmt.Printf("Cycle started: sent Cmd 146 (THC mod: %.3f, CH4 mod: %.3f)\n", thcMod, ch4Mod)
			isAcquiring = true
		}

		// 无论是否在采集期内，都要继续发送 Telemetry(143) 和 EPC(159) 保持状态同步和长连接
		// 在采集期结束后，发出的信号数据保持为基线或0即可
		payload := build143Payload(byte(chnType), byte(freqByte), t_sec, dt, thcMod, ch4Mod, isAcquiring)
		frame, _ := gckc.Encode(gckc.Frame{DeviceID: deviceID, Seq: seq, Cmd: 143, Payload: payload})
		seq++
		c.Write(frame)

		// Send EPC 159
		payload159 := build159Payload()
		frame159, _ := gckc.Encode(gckc.Frame{DeviceID: deviceID, Seq: seq, Cmd: 159, Payload: payload159})
		seq++
		c.Write(frame159)

		t_sec += dt

		// 达到采集结束时间，发送 Cmd 147 触发后端出数和快照，但循环继续
		if isAcquiring && t_sec >= acqS {
			frame, _ := gckc.Encode(gckc.Frame{DeviceID: deviceID, Seq: seq, Cmd: 147, Payload: []byte{0}})
			seq++
			c.Write(frame)
			fmt.Printf("Acquisition ended at %.1fs: sent Cmd 147. Cycle continues...\n", t_sec)
			isAcquiring = false
		}

		// 达到整个循环周期时间，重置 t_sec，准备下一轮
		if t_sec >= cycleS {
			fmt.Printf("Cycle finished at %.1fs. Preparing for next cycle...\n", t_sec)
			t_sec = 0.0
		}
	}
}

func bcd2Temp(data []byte) float64 {
	if len(data) < 2 {
		return 0
	}
	b0 := data[0]
	neg := (b0 & 0xD0) == 0xD0
	if neg {
		b0 -= 0xD0
	}
	d1 := float64((b0 >> 4) & 0x0F)
	d2 := float64(b0 & 0x0F)
	d3 := float64((data[1] >> 4) & 0x0F)
	d4 := float64(data[1] & 0x0F)
	v := d1*100 + d2*10 + d3 + d4*0.1
	if neg {
		v = -v
	}
	return v
}

func handleCmd(f gckc.Frame) {
	mu.Lock()
	defer mu.Unlock()
	if f.Cmd == 8 && len(f.Payload) >= 12 {
		targets["Inj1"] = bcd2Temp(f.Payload[0:2])
		targets["Col"] = bcd2Temp(f.Payload[2:4])
		targets["Det1"] = bcd2Temp(f.Payload[4:6])
		fmt.Printf("Simulator received Cmd 8: Targets Inj1=%.1f, Col=%.1f, Det1=%.1f\n", targets["Inj1"], targets["Col"], targets["Det1"])
	}
	if f.Cmd == 34 && len(f.Payload) >= 24 {
		targets["Carrier1"] = float64(uint16(f.Payload[0])<<8|uint16(f.Payload[1])) / 100.0
		targets["H2"] = float64(uint16(f.Payload[8])<<8|uint16(f.Payload[9])) / 100.0
		targets["Air"] = float64(uint16(f.Payload[16])<<8|uint16(f.Payload[17])) / 100.0
		fmt.Printf("Simulator received Cmd 34: Targets Carrier1=%.1f, H2=%.1f, Air=%.1f\n", targets["Carrier1"], targets["H2"], targets["Air"])
	}
}

func updateSimulation(dt float64) {
	mu.Lock()
	defer mu.Unlock()
	for k, t := range targets {
		a := actuals[k]
		diff := t - a
		step := 5.0 * dt // 5 degree/psi per second
		if k == "Carrier1" || k == "H2" || k == "Air" {
			step = 20.0 * dt
		}
		if diff > step {
			actuals[k] += step
		} else if diff < -step {
			actuals[k] -= step
		} else {
			actuals[k] = t
		}
	}
}

func tempToBCD2(temp float64) []byte {
	out := make([]byte, 2)
	neg := false
	if temp < 0 {
		neg = true
		temp = -temp
	}
	v := int(math.Round(temp * 10))
	if v > 9999 {
		v = 9999
	}
	d1 := (v / 1000) % 10
	d2 := (v / 100) % 10
	d3 := (v / 10) % 10
	d4 := v % 10

	out[0] = byte((d1 << 4) | d2)
	if neg {
		out[0] |= 0xD0
	}
	out[1] = byte((d3 << 4) | d4)
	return out
}

func build159Payload() []byte {
	mu.Lock()
	defer mu.Unlock()
	payload := make([]byte, 1+3*8)
	payload[0] = 3
	cPsi := uint16(actuals["Carrier1"] * 100)
	hPsi := uint16(actuals["H2"] * 100)
	aPsi := uint16(actuals["Air"] * 100)
	// format: set_psi(2), actual_psi(2), actual_sccm(2), 0(2)
	payload[1] = byte(cPsi >> 8)
	payload[2] = byte(cPsi)
	payload[3] = byte(cPsi >> 8)
	payload[4] = byte(cPsi)

	payload[9] = byte(hPsi >> 8)
	payload[10] = byte(hPsi)
	payload[11] = byte(hPsi >> 8)
	payload[12] = byte(hPsi)

	payload[17] = byte(aPsi >> 8)
	payload[18] = byte(aPsi)
	payload[19] = byte(aPsi >> 8)
	payload[20] = byte(aPsi)

	return payload
}

func getSignal(t_sec float64, thcMod float64, ch4Mod float64) float64 {
	// Y in pA
	base := 4.0 // 4 pA baseline

	// THC peak at 0.2 min (12s)
	// 缩小10倍: 13.5 -> 1.35，并应用 1-2% 周期随机偏差
	thc := 1.35 * thcMod * math.Exp(-math.Pow(t_sec-12.0, 2)/2.0)

	// Oxygen W-shape at 0.5 min (30s)
	// 缩小10倍: -0.15 -> -0.015
	oxy := -0.015 * math.Cos((t_sec-30.0)*math.Pi/2.0) * math.Exp(-math.Pow(t_sec-30.0, 2)/2.0)

	// CH4 peak at 0.6 min (36s)
	// 缩小10倍: 2.5 -> 0.25，并应用 1-2% 周期随机偏差
	ch4 := 0.25 * ch4Mod * math.Exp(-math.Pow(t_sec-36.0, 2)/1.0)

	Y := base + thc + ch4 + oxy
	if Y < 0 {
		Y = 0
	}

	// convert Y (pA) to raw BCD float format used by hardware
	// Y = raw^2 * 1000000 / 1.5 => raw = sqrt(Y * 1.5) / 1000
	raw := math.Sqrt(Y*1.5) / 1000.0
	return raw
}

func build143Payload(detType byte, freqByte byte, startT float64, dt float64, thcMod float64, ch4Mod float64, isAcquiring bool) []byte {
	mu.Lock()
	inj1 := tempToBCD2(actuals["Inj1"])
	col := tempToBCD2(actuals["Col"])
	det1 := tempToBCD2(actuals["Det1"])
	mu.Unlock()

	payload := make([]byte, 0, 64)
	payload = append(payload, inj1...)
	payload = append(payload, col...)
	payload = append(payload, det1...)

	for i := 0; i < 6; i++ {
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
	step := dt / float64(points)

	t := startT
	for i := 0; i < points; i++ {
		raw := 0.0
		if isAcquiring {
			raw = getSignal(t, thcMod, ch4Mod)
		} else {
			raw = getSignal(t, 0, 0) // 或者固定基线
		}
		payload = append(payload, encode7Digits(raw)...)
		t += step
	}

	return payload
}

func encode7Digits(v float64) []byte {
	neg := false
	if v < 0 {
		neg = true
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
	if neg {
		b0 |= (1 << 4)
	}
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
