package main

	import (
	"bytes"
	"encoding/json"
	"encoding/xml"
	"fmt"
	"io"
	"math"
	"mime/multipart"
	"net/http"
	"time"

	v1 "chromatography-workstation/edge/internal/contracts/v1"
	"chromatography-workstation/edge/internal/models"
)

type VocData struct {
	XMLName    xml.Name      `xml:"Voc"`
	DataTime   string        `xml:"DataTime"`
	TimeSpan   int           `xml:"TimeSpan"`
	Datas      VocDatas      `xml:"Datas"`
	Pollutants VocPollutants `xml:"Pollutants"`
}

type VocDatas struct {
	Count  int       `xml:"Count,attr"`
	Width  int       `xml:"Width,attr"`
	Height int       `xml:"Height,attr"`
	Data   []float64 `xml:"Data"`
}

type VocPollutants struct {
	Data []VocPollutantData `xml:"Data"`
}

type VocPollutantData struct {
	PollCode  string `xml:"PollCode,attr"`
	StartTime int    `xml:"StartTime,attr"`
	EndTime   int    `xml:"EndTime,attr"`
}

func uploadSpectrum(trace v1.Trace, res v1.Result, at time.Time, cfg models.UploadConfig) {
	if !cfg.EnableUpload || cfg.UploadIP == "" || cfg.UploadPort == 0 {
		return
	}

	roundedValues := make([]float64, len(trace.Values))
	for i, v := range trace.Values {
		roundedValues[i] = math.Round(v*1000) / 1000
	}

	voc := VocData{
		DataTime: at.Local().Format("20060102150405"),
		TimeSpan: int(trace.TimeSpanS),
		Datas: VocDatas{
			Count:  len(roundedValues),
			Width:  int(trace.TimeSpanS),
			Height: 600,
			Data:   roundedValues,
		},
		Pollutants: VocPollutants{},
	}

	for _, p := range res.Pollutants {
		if p.Code == "" {
			continue
		}
		voc.Pollutants.Data = append(voc.Pollutants.Data, VocPollutantData{
			PollCode:  p.Code,
			StartTime: int(p.StartS),
			EndTime:   int(p.EndS),
		})
	}

	xmlBytes, err := xml.MarshalIndent(voc, "", "  ")
	if err != nil {
		LogErrorf("生成谱图XML失败: %v", err)
		return
	}
	xmlHeader := []byte("<?xml version=\"1.0\" encoding=\"utf-8\"?>\n")
	xmlBytes = append(xmlHeader, xmlBytes...)

	body := &bytes.Buffer{}
	writer := multipart.NewWriter(body)
	
	_ = writer.WriteField("deviceCode", cfg.DeviceNo)
	_ = writer.WriteField("mediaType", "4")
	_ = writer.WriteField("mediaFormat", "8")
	_ = writer.WriteField("mediaTime", at.Local().Format("2006-01-02 15:04:05"))

	part, err := writer.CreateFormFile("mediaFile", fmt.Sprintf("%s.xml", at.Local().Format("20060102150405")))
	if err != nil {
		LogErrorf("构建谱图上传请求失败: %v", err)
		return
	}
	part.Write(xmlBytes)
	writer.Close()

	url := fmt.Sprintf("http://%s:%d/bin/mediafileupload/file", cfg.UploadIP, cfg.UploadPort)
	req, err := http.NewRequest("POST", url, body)
	if err != nil {
		LogErrorf("构建谱图上传请求失败: %v", err)
		return
	}
	req.Header.Set("Content-Type", writer.FormDataContentType())

	// 在后台协程上传
	go func() {
		client := &http.Client{Timeout: 60 * time.Second}
		resp, err := client.Do(req)
		if err != nil {
			LogErrorf("谱图上传失败: %v", err)
			return
		}
		defer resp.Body.Close()

		respBody, _ := io.ReadAll(resp.Body)
		
		var resJson struct {
			Code    string `json:"code"`
			Success bool   `json:"success"`
			Msg     string `json:"msg"`
		}
		if err := json.Unmarshal(respBody, &resJson); err != nil {
			LogErrorf("谱图上传解析响应失败: %s", string(respBody))
			return
		}

		// 适配 code=0 或 success=true
		if resJson.Code == "0" || resJson.Success {
			LogInfof("谱图上传成功: %s", url)
		} else {
			LogErrorf("谱图上传返回错误: %s", resJson.Msg)
		}
	}()
}
