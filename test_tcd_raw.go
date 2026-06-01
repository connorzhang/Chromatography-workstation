package main

import (
	"fmt"
	"io"
	"time"

	"go.bug.st/serial"
	"go.bug.st/serial/enumerator"
)

func main() {
	ports, err := enumerator.GetDetailedPortsList()
	if err != nil {
		fmt.Println("Error enumerating ports:", err)
		return
	}

	var validPort string
	mode := &serial.Mode{BaudRate: 38400}

	for _, p := range ports {
		port, err := serial.Open(p.Name, mode)
		if err != nil {
			continue
		}
		port.SetReadTimeout(2 * time.Second)
		buf := make([]byte, 100)
		n, _ := port.Read(buf)
		port.Close()

		if n > 0 {
			validPort = p.Name
			break
		}
	}

	if validPort == "" {
		// Default to COM6 if auto-detect fails
		validPort = "COM6"
	}

	fmt.Printf("--- Found TCD on %s. Starting Data Dump ---\n", validPort)
	port, err := serial.Open(validPort, mode)
	if err != nil {
		fmt.Println("Error opening port:", err)
		return
	}
	defer port.Close()

	testPoints := []int{0, 10, 20, 30, 40, 50, 60, 70, 80, 90, 100, 110, 120, 130, 140, 150, 160, 170, 180, 190, 200, 210, 220, 230, 240, 249, 250, 251, 252, 253, 254, 255}

	for _, current := range testPoints {
		cmd := []byte{0x47, 0x45, 0x45, 0x02, 0x0E, byte(current)}
		port.Write(cmd)

		fmt.Printf("\n>>> Set Bridge to %d, waiting 5 seconds...\n", current)
		time.Sleep(5 * time.Second)
		port.ResetInputBuffer()

		// Read 2 frames
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

						// Try to decode based on current understanding
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
