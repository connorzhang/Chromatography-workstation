package main

import (
	"encoding/json"
	"log"
	"os"

	"github.com/xuri/excelize/v2"
)

func main() {
	f, err := excelize.OpenFile("../../docs/VOC动态管控上传协议MODBUS地址260108(1).xlsx")
	if err != nil {
		log.Fatal(err)
	}
	defer f.Close()

	sheets := f.GetSheetList()
	if len(sheets) == 0 {
		return
	}
	
	sheet := sheets[0]
	rows, err := f.GetRows(sheet)
	if err != nil {
		log.Fatal(err)
	}
	
	data, _ := json.MarshalIndent(rows, "", "  ")
	os.WriteFile("dump.json", data, 0644)
}