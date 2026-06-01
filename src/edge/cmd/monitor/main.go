package main

import (
	"fmt"
	"time"

	"go.bug.st/serial"
)

func main() {
	portName := "/dev/ttyUSB5"
	baud := 38400

	mode := &serial.Mode{
		BaudRate: baud,
		DataBits: 8,
		Parity:   serial.NoParity,
		StopBits: serial.OneStopBit,
	}

	port, err := serial.Open(portName, mode)
	if err != nil {
		fmt.Printf("Failed to open %s: %v\n", portName, err)
		return
	}
	defer port.Close()

	fmt.Printf("Monitoring %s at %d baud...\n", portName, baud)

	buf := make([]byte, 1024)
	for {
		n, err := port.Read(buf)
		if err != nil {
			fmt.Printf("Read error: %v\n", err)
			time.Sleep(1 * time.Second)
			continue
		}
		if n > 0 {
			fmt.Printf("Received %d bytes: %X\n", n, buf[:n])
			// 尝试打印字符串
			str := string(buf[:n])
			// 简单过滤一下不可见字符，或者直接打印
			fmt.Printf("String format: %q\n", str)
		}
	}
}
