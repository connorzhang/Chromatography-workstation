package main

import (
	"fmt"
	"io"
	"time"

	"go.bug.st/serial"
)

func main() {
	// 使用之前确认的 COM3
	portName := "COM3"
	mode := &serial.Mode{BaudRate: 38400}

	fmt.Printf("--- Found TCD on %s. Starting Bitfield Test ---\n", portName)
	port, err := serial.Open(portName, mode)
	if err != nil {
		fmt.Println("Error opening port:", err)
		return
	}
	defer port.Close()

	// 我们固定桥流的低 5 位（即 0~31 的范围）为一个安全值，比如 10 (0x0A, 0000 1010)
	// 然后我们分别改变高 3 位（Bit 5, Bit 6, Bit 7）
	// 如果高位是放大倍数或极性，那么在基础桥流均为 10 的情况下，
	// 输出的基线绝对值会成倍数关系，或者符号会反转。

	baseCurrent := byte(10) // 0000 1010

	testCases := []struct {
		name string
		val  byte
	}{
		{"Base (No high bits)", baseCurrent},                                // 0000 1010 (10)
		{"Bit 5 Set", baseCurrent | (1 << 5)},                               // 0010 1010 (42)
		{"Bit 6 Set", baseCurrent | (1 << 6)},                               // 0100 1010 (74)
		{"Bit 7 Set", baseCurrent | (1 << 7)},                               // 1000 1010 (138)
		{"Bit 5+6 Set", baseCurrent | (1 << 5) | (1 << 6)},                  // 0110 1010 (106)
		{"Bit 6+7 Set", baseCurrent | (1 << 6) | (1 << 7)},                  // 1100 1010 (202)
		{"All High Bits Set", baseCurrent | (1 << 5) | (1 << 6) | (1 << 7)}, // 1110 1010 (234)
	}

	for _, tc := range testCases {
		cmd := []byte{0x47, 0x45, 0x45, 0x02, 0x0E, tc.val}
		port.Write(cmd)

		fmt.Printf("\n>>> Testing %s (Send: 0x%02X), waiting 5 seconds...\n", tc.name, tc.val)
		time.Sleep(5 * time.Second)
		port.ResetInputBuffer()

		// Read 2 frames to confirm stability
		for f := 1; f <= 2; f++ {
			syncBuf := make([]byte, 1)
			state := 0
			var frame []byte
			timeout := time.After(3 * time.Second)
		loop:
			for {
				select {
				case <-timeout:
					fmt.Printf("  Frame %d: Timeout\n", f)
					break loop
				default:
					n, err := port.Read(syncBuf)
					if err != nil || n == 0 {
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

						// Decode logic
						signByte := raw[0]
						sign := 1.0
						if (signByte & 0xF0) == 0x10 {
							sign = -1.0
						}
						absValue := uint32(raw[0]&0x0F)<<24 | uint32(raw[1])<<16 | uint32(raw[2])<<8 | uint32(raw[3])
						decoded := sign * float64(absValue)

						fmt.Printf("  Frame %d -> Raw Hex: %02X %02X %02X %02X | Decoded: %v\n", f, raw[0], raw[1], raw[2], raw[3], decoded)
						break loop
					} else {
						state = 0
						if b == 0x45 {
							state = 1
						}
					}
				}
			}
		}
	}
}
