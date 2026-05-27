package main

import (
	"encoding/binary"
	"fmt"
	"time"

	"github.com/goburrow/modbus"
)

func main() {
	comPort := "\\\\.\\COM6"
	baud := 9600
	parity := "O"
	slaveID := byte(20)

	fmt.Println("=====================================================")
	fmt.Printf(" 开始深度采集 WINPARK 温控板 (站号 %d, %d, 8-%s-1)\n", slaveID, baud, parity)
	fmt.Println("=====================================================")

	handler := modbus.NewRTUClientHandler(comPort)
	handler.BaudRate = baud
	handler.DataBits = 8
	handler.Parity = parity
	handler.StopBits = 1
	handler.SlaveId = slaveID
	handler.Timeout = 1 * time.Second

	err := handler.Connect()
	if err != nil {
		fmt.Printf("串口打开失败: %v\n", err)
		return
	}
	defer handler.Close()
	client := modbus.NewClient(handler)

	// 1. 读取前 20 个输入寄存器 (04指令)
	fmt.Println("\n>>> [1] 读取地址 0000 开始的 20 个输入寄存器 (04指令) <<<")
	res, err := client.ReadInputRegisters(0, 20)
	if err != nil {
		fmt.Printf("  读取失败: %v\n", err)
	} else {
		for i := 0; i < 20; i++ {
			val := int16(binary.BigEndian.Uint16(res[i*2 : i*2+2]))
			fmt.Printf("  地址 %04d (0x%04X): %d\n", i, i, val)
		}
	}

	// 2. 读取前 20 个保持寄存器 (03指令)
	fmt.Println("\n>>> [2] 读取地址 0000 开始的 20 个保持寄存器 (03指令) <<<")
	res2, err2 := client.ReadHoldingRegisters(0, 20)
	if err2 != nil {
		fmt.Printf("  读取失败: %v\n", err2)
	} else {
		for i := 0; i < 20; i++ {
			val := int16(binary.BigEndian.Uint16(res2[i*2 : i*2+2]))
			fmt.Printf("  地址 %04d (0x%04X): %d\n", i, i, val)
		}
	}

	// 既然找到了规律，地址 100 开始很明显是 PID 设定或者通道设定
	// 240, 60, 100，三个数字一组循环，很像是: [比例带P=240, 积分时间I=60, 微分时间D=100] 或者 [设定温度SV=240, 上限报警=60, 下限报警=100] 等等
	// 既然是 8 路，我们往后多读一些
	
	fmt.Println("\n>>> [4] 连续读取地址 0100 开始的 30 个保持寄存器 (推测为 8 路 PID 或设定参数) <<<")
	res4, err4 := client.ReadHoldingRegisters(100, 30)
	if err4 != nil {
		fmt.Printf("  读取失败: %v\n", err4)
	} else {
		for ch := 1; ch <= 8; ch++ {
			offset := (ch - 1) * 3
			val1 := int16(binary.BigEndian.Uint16(res4[offset*2 : offset*2+2]))
			val2 := int16(binary.BigEndian.Uint16(res4[(offset+1)*2 : (offset+1)*2+2]))
			val3 := int16(binary.BigEndian.Uint16(res4[(offset+2)*2 : (offset+2)*2+2]))
			
			fmt.Printf("  [通道 %d] (地址 0x%04X~0x%04X): 参数1 = %d, 参数2 = %d, 参数3 = %d\n", ch, 100+offset, 100+offset+2, val1, val2, val3)
		}
	}

	// 探寻 8 路实时测量值 (PV) 究竟藏在哪里
	// 根据国产品牌温控表的习惯，如果0x00 存的是总状态，那很可能通道测量值在 0x01, 0x02 或者 0x10, 0x11 或者 0x20
	// 刚才读取 0x00~0x19，发现地址 0x00=8888, 0x01=3，后面基本是0或者常数。不像8个温度
	// 试试读取地址 0x1000 开始的高级地址，或者 0x0200
	fmt.Println("\n>>> [5] 寻找 8 路实时温度 PV (尝试读取 0x0200~0x021F) <<<")
	res5, err5 := client.ReadHoldingRegisters(0x0200, 20)
	if err5 == nil {
		for i := 0; i < 20; i++ {
			val := int16(binary.BigEndian.Uint16(res5[i*2 : i*2+2]))
			fmt.Printf("  地址 %04d (0x%04X): %d\n", 0x0200+i, 0x0200+i, val)
		}
	} else {
		fmt.Println("  0x0200 读取失败", err5)
	}
}