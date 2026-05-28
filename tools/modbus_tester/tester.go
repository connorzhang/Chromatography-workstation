package main

import (
	"encoding/binary"
	"fmt"
	"math"
	"os"
	"time"

	"github.com/goburrow/modbus"
)

func main() {
	if len(os.Args) < 2 {
		fmt.Println("Usage: modbus_test <serial_port>")
		os.Exit(1)
	}
	port := os.Args[1]

	// 尝试 NONE 和 EVEN 两种校验
	parities := []string{"N", "E"}

	for _, parity := range parities {
		fmt.Printf("Testing port %s with Parity: %s\n", port, parity)
		handler := modbus.NewRTUClientHandler(port)
		handler.BaudRate = 9600
		handler.DataBits = 8
		handler.Parity = parity
		handler.StopBits = 1
		handler.SlaveId = 20 // 0x14
		handler.Timeout = 2 * time.Second

		err := handler.Connect()
		if err != nil {
			fmt.Printf("Failed to connect %s (Parity %s): %v\n", port, parity, err)
			continue
		}
		defer handler.Close()

		client := modbus.NewClient(handler)

		// 1. 读取设定温度 (地址 42，数量 8)
		fmt.Println("Reading set temperatures (Address 42)...")
		results, err := client.ReadHoldingRegisters(42, 8)
		if err != nil {
			fmt.Printf("Error reading set temps (Parity %s): %v\n", parity, err)
		} else {
			fmt.Printf("Set temps raw: %v\n", results)
			for i := 0; i < 8; i++ {
				val := int16(binary.BigEndian.Uint16(results[i*2 : i*2+2]))
				fmt.Printf("  CH%d Set Temp: %d ℃\n", i+1, val)
			}
		}

		// 2. 读取实时温度 (地址 360，数量 16)
		fmt.Println("Reading real-time temperatures (Address 360)...")
		results2, err := client.ReadHoldingRegisters(360, 16)
		if err != nil {
			fmt.Printf("Error reading real-time temps (Parity %s): %v\n", parity, err)
		} else {
			fmt.Printf("Real-time temps raw: %v\n", results2)
			for i := 0; i < 8; i++ {
				// 文档中文字写的是 ABCD，但其 Python 示例代码中是 reg2, reg1（即高低字交换，实际为 CDAB 模式）
				// 且收到的原始数据如：[161 136 66 18] -> 寄存器1: 0xA188, 寄存器2: 0x4212 -> 拼接为 0x4212A188 才等于 36.65℃
				reg1 := binary.BigEndian.Uint16(results2[i*4 : i*4+2])
				reg2 := binary.BigEndian.Uint16(results2[i*4+2 : i*4+4])
				packed := uint32(reg2)<<16 | uint32(reg1)
				val := math.Float32frombits(packed)
				
				// 按照文档，当未连接时返回占位值 32767.00
				if val >= 32767.0 {
					fmt.Printf("  CH%d Real-time Temp: DISCONNECTED (%.2f ℃)\n", i+1, val)
				} else {
					fmt.Printf("  CH%d Real-time Temp: %.2f ℃\n", i+1, val)
				}
			}
		}

		// 如果成功读到任何一个，跳出循环
		if err == nil {
			fmt.Println("Communication successful!")
			break
		}
	}
}
