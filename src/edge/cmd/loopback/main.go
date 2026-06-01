package main

import (
	"fmt"
	"path/filepath"
	"strings"
	"time"

	"go.bug.st/serial"
)

func main() {
	ports, err := filepath.Glob("/dev/ttyS*")
	if err != nil {
		ports = []string{}
	}
	usbPorts, err := filepath.Glob("/dev/ttyUSB*")
	if err == nil {
		ports = append(ports, usbPorts...)
	}

	exclude := map[string]bool{
		"/dev/ttyS2":   true,
		"/dev/ttyUSB3": true,
		"/dev/ttyUSB4": true,
	}

	fmt.Println("Starting loopback test on available ports...")
	bauds := []int{9600, 38400, 115200}
	found := false

	for _, portName := range ports {
		if exclude[portName] {
			continue
		}

		for _, baud := range bauds {
			mode := &serial.Mode{
				BaudRate: baud,
				DataBits: 8,
				Parity:   serial.NoParity,
				StopBits: serial.OneStopBit,
			}

			port, err := serial.Open(portName, mode)
			if err != nil {
				continue
			}
			port.SetReadTimeout(200 * time.Millisecond)

			testStr := fmt.Sprintf("PING_%s_%d\n", portName, baud)

			// Clear buffer
			buf := make([]byte, 1024)
			port.Read(buf)

			_, err = port.Write([]byte(testStr))
			if err != nil {
				port.Close()
				continue
			}

			time.Sleep(50 * time.Millisecond)
			n, _ := port.Read(buf)
			port.Close()

			if n > 0 {
				recv := string(buf[:n])
				if strings.Contains(recv, testStr) || strings.Contains(recv, "PING_") {
					fmt.Printf("\n[SUCCESS] Loopback detected!\n")
					fmt.Printf("Port: %s\n", portName)
					fmt.Printf("BaudRate: %d\n", baud)
					fmt.Printf("Sent: %s", testStr)
					fmt.Printf("Received: %s\n", recv)
					found = true
					goto DONE
				}
			}
		}
	}
DONE:
	if !found {
		fmt.Println("\n[FAILED] No port passed the loopback test.")
	}
}
