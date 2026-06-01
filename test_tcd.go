package main

import (
	"context"
	"fmt"
	"log"
	"time"

	"go.bug.st/serial"
)

func main() {
	mode := &serial.Mode{
		BaudRate: 38400,
		DataBits: 8,
		Parity:   serial.NoParity,
		StopBits: serial.OneStopBit,
	}

	portName := "COM6"
	fmt.Printf("Opening port %s at 38400 baud...\n", portName)
	port, err := serial.Open(portName, mode)
	if err != nil {
		log.Fatalf("Failed to open %s: %v", portName, err)
	}
	defer port.Close()
	fmt.Println("Port opened successfully. Waiting for TCD data (10 seconds max)...")

	port.SetReadTimeout(time.Millisecond * 500)

	ctx, cancel := context.WithTimeout(context.Background(), 10*time.Second)
	defer cancel()

	buf := make([]byte, 1024)
	frame := make([]byte, 0, 512)
	framesRead := 0

	done := make(chan struct{})

	go func() {
		for framesRead < 3 {
			n, err := port.Read(buf)
			if err != nil {
				continue
			}
			if n > 0 {
				frame = append(frame, buf[:n]...)

				// Wait for at least 87 bytes
				for len(frame) >= 87 {
					// Find "EE" (0x45 0x45)
					idx := -1
					for i := 0; i <= len(frame)-87; i++ {
						if frame[i] == 0x45 && frame[i+1] == 0x45 {
							// Optional: Check if it ends with 0x0D 0x0A
							if frame[i+85] == 0x0D && frame[i+86] == 0x0A {
								idx = i
								break
							}
						}
					}

					if idx == -1 {
						frame = frame[len(frame)-86:]
						break
					}

					validFrame := frame[idx : idx+87]
					parseFrame87(validFrame)
					framesRead++

					frame = frame[idx+87:]

					if framesRead >= 3 {
						break
					}
				}
			}
		}
		close(done)
	}()

	select {
	case <-ctx.Done():
		fmt.Println("\nTimeout reached (10s). Did not receive enough data from TCD.")
	case <-done:
		fmt.Println("\nSuccessfully read and parsed 3 frames. Test completed.")
	}
}

func parseFrame87(frame []byte) {
	fmt.Printf("\n--- TCD Frame Received (87 bytes) ---\n")
	fmt.Printf("Header: %X %X\n", frame[0], frame[1])
	fmt.Printf("Unknown 2 bytes: %X %X\n", frame[2], frame[3])

	fmt.Printf("Bridge Current Setting (Offset 84): %d\n", frame[84])

	dataOffset := 4
	fmt.Println("Channel Data (First 4 of 20):")
	for i := 0; i < 4; i++ {
		idx := dataOffset + (i * 4)
		rawValue := uint32(frame[idx])<<24 | uint32(frame[idx+1])<<16 | uint32(frame[idx+2])<<8 | uint32(frame[idx+3])

		sign := 1
		if (rawValue & 0x80000000) != 0 {
			sign = -1
		}

		absValue := rawValue & 0x7FFFFFFF
		finalValue := float64(sign) * float64(absValue)

		fmt.Printf("  CH%02d: %.0f\n", i+1, finalValue)
	}
}
