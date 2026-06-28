package main

import (
	"context"
	"fmt"
	"log"
	"math"
	"time"

	"go.bug.st/serial"
)

type DataPoint struct {
	Timestamp time.Time
	Value     float64
}

var baselineHistory []DataPoint

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
	fmt.Println("Port opened successfully. Waiting for TCD data...")

	port.SetReadTimeout(time.Millisecond * 500)

	// Run for a long time to allow 2 minutes of data collection
	ctx, cancel := context.WithTimeout(context.Background(), 10*time.Minute)
	defer cancel()

	buf := make([]byte, 1024)
	frame := make([]byte, 0, 512)

	done := make(chan struct{})

	go func() {
		for {
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
							// Check if it ends with 0x0D 0x0A
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

					frame = frame[idx+87:]
				}
			}
		}
	}()

	select {
	case <-ctx.Done():
		fmt.Println("\nTimeout reached (10m). Test completed.")
	case <-done:
		fmt.Println("\nTest completed.")
	}
}

func parseFrame87(frame []byte) {
	fmt.Printf("\n--- TCD Frame Received (87 bytes) ---\n")
	fmt.Printf("Bridge Current Setting (Offset 84): %d\n", frame[84])

	dataOffset := 4
	fmt.Println("20组实时数据:")

	now := time.Now()
	for i := 0; i < 20; i++ {
		idx := dataOffset + (i * 4)
		rawValue := uint32(frame[idx])<<24 | uint32(frame[idx+1])<<16 | uint32(frame[idx+2])<<8 | uint32(frame[idx+3])

		sign := 1
		if (rawValue & 0x80000000) != 0 {
			sign = -1
		}

		absValue := rawValue & 0x7FFFFFFF
		finalValue := float64(sign) * float64(absValue)

		// Record data for drift calculation (assuming all points are from the same detector channel)
		baselineHistory = append(baselineHistory, DataPoint{Timestamp: now, Value: finalValue})

		if i < 4 {
			fmt.Printf("  Data[%02d]: %.0f\n", i+1, finalValue)
		} else if i == 4 {
			fmt.Println("  ... (remaining 16 groups omitted for brevity)")
		}
	}

	// Remove data older than 2 minutes
	cutoff := now.Add(-2 * time.Minute)
	validIdx := 0
	for i, p := range baselineHistory {
		if p.Timestamp.After(cutoff) {
			validIdx = i
			break
		}
	}
	if validIdx > 0 {
		baselineHistory = baselineHistory[validIdx:]
	}

	// Calculate baseline drift if we have data
	if len(baselineHistory) > 0 {
		minVal := math.MaxFloat64
		maxVal := -math.MaxFloat64
		for _, p := range baselineHistory {
			if p.Value < minVal {
				minVal = p.Value
			}
			if p.Value > maxVal {
				maxVal = p.Value
			}
		}
		drift := maxVal - minVal
		fmt.Printf("=> 2分钟基线漂移数据 (Max - Min): %.3f\n", drift)
	}
}
