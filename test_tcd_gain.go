package main

import (
	"fmt"
	"io"
	"time"

	"go.bug.st/serial"
)

func main() {
	portName := "COM3"
	mode := &serial.Mode{BaudRate: 38400}

	port, err := serial.Open(portName, mode)
	if err != nil {
		fmt.Println("Error opening port:", err)
		return
	}
	defer port.Close()

	// 我们定义 4 种高位状态（放大倍数/极性猜测位）
	highBits := []struct {
		name string
		val  byte
	}{
		{"Base (High bits = 000)", 0x00},
		{"Bit 5 Set (001)", 0x20},
		{"Bit 6 Set (010)", 0x40},
		{"Bit 7 Set (100)", 0x80},
	}

	// 对于每种高位状态，我们线性改变真正的桥流值（低 5 位）
	// 取 3 个等间距的点：5, 10, 15。
	// 通过计算 (Value_15 - Value_10)/5 和 (Value_10 - Value_5)/5，
	// 我们能得出在这条曲线上的“斜率”。
	// 如果高位真的是放大倍数（如 x1, x10, x100），那么斜率也一定会严格成 1倍、10倍、100倍的关系！
	lowBits := []byte{5, 10, 15}

	for _, hb := range highBits {
		fmt.Printf("\n--- Testing Multiplier State: %s ---\n", hb.name)
		var values []float64

		for _, lb := range lowBits {
			cmdVal := hb.val | lb
			cmd := []byte{0x47, 0x45, 0x45, 0x02, 0x0E, cmdVal}
			port.Write(cmd)

			// 给足 10 秒时间等待基线平稳
			time.Sleep(10 * time.Second)
			port.ResetInputBuffer()

			var decoded float64
			var stableCount int

			// 连续读取，直到连续 3 帧数据波动小于 100，或者最多读 15 帧
			var lastVal float64
			for f := 1; f <= 15; f++ {
				syncBuf := make([]byte, 1)
				state := 0
				var frame []byte
				timeout := time.After(3 * time.Second)
			loop:
				for {
					select {
					case <-timeout:
						break loop
					default:
						n, _ := port.Read(syncBuf)
						if n == 0 {
							continue
						}
						b := syncBuf[0]

						if state == 0 && b == 0x45 {
							state = 1
						} else if state == 1 && b == 0x45 {
							state = 2
						} else if state == 2 && b == 0xFF {
							state = 3
						} else if state == 3 && b == 0x01 {
							frame = make([]byte, 83)
							io.ReadFull(port, frame)
							raw := frame[0:4]
							signByte := raw[0]
							sign := 1.0
							if (signByte & 0xF0) == 0x10 {
								sign = -1.0
							}
							absValue := uint32(raw[0]&0x0F)<<24 | uint32(raw[1])<<16 | uint32(raw[2])<<8 | uint32(raw[3])
							decoded = sign * float64(absValue)
							break loop
						} else {
							state = 0
							if b == 0x45 {
								state = 1
							}
						}
					}
				}

				if f > 1 {
					diff := decoded - lastVal
					if diff < 0 {
						diff = -diff
					}
					if diff < 100 {
						stableCount++
					} else {
						stableCount = 0
					}
				}
				lastVal = decoded

				if stableCount >= 3 {
					break // 数据已经稳定
				}
			}
			fmt.Printf("  LowBit %2d (Send 0x%02X) -> Stable Value: %8.0f\n", lb, cmdVal, decoded)
			values = append(values, decoded)
		}

		if len(values) == 3 {
			slope1 := (values[1] - values[0]) / 5.0
			slope2 := (values[2] - values[1]) / 5.0
			fmt.Printf("  >> Slope (5->10):  %8.2f per step\n", slope1)
			fmt.Printf("  >> Slope (10->15): %8.2f per step\n", slope2)
		}
	}
}
