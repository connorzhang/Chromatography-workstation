package v1

type Trace struct {
	Schema    string    `json:"schema"`
	TraceID   string    `json:"traceId"`
	DeviceID  string    `json:"deviceId"`
	StationID string    `json:"stationId"`
	DataTime  string    `json:"dataTime"`
	TimeSpanS float64   `json:"timeSpanS"`
	DtS       float64   `json:"dtS"`
	Unit      string    `json:"unit"`
	Values    []float64 `json:"values"`
}

type Method struct {
	MethodID   string          `json:"methodId"`
	Version    int             `json:"version"`
	Pollutants []PollutantSpec `json:"pollutants"`
	Groups     []PeakGroupSpec `json:"groups"` // 峰分组配置
}

type PeakGroupSpec struct {
	Code         string   `json:"code"`         // 如 "NMHC"
	Name         string   `json:"name"`         // 如 "非甲烷总烃"
	IncludeCodes []string `json:"includeCodes"` // 包含的单峰 Code 列表
	ExcludeCodes []string `json:"excludeCodes"` // 需要排除的单峰 Code (用于扣减计算，如总烃减去甲烷)
}

type PollutantSpec struct {
	Code         string  `json:"code"`
	Name         string  `json:"name"`
	StartS       float64 `json:"startS"`
	EndS         float64 `json:"endS"`
	PaddingS     float64 `json:"paddingS"`
	AlignMode    string  `json:"alignMode"`
	BaselineMode string  `json:"baselineMode"`
	Threshold    float64 `json:"threshold"`
	RespStyle    int     `json:"respStyle"` // 0: 面积, 1: 峰高
	CurveFunc    int     `json:"curveFunc"` // 0: 线性分段
	Levels       []Level `json:"levels"`    // 校准点
}

type Level struct {
	LevelIndex int     `json:"levelIndex"`
	Amount     float64 `json:"amount"`
	Response   float64 `json:"response"`
}

type Result struct {
	Schema        string            `json:"schema"`
	TraceID       string            `json:"traceId"`
	DeviceID      string            `json:"deviceId"`
	StationID     string            `json:"stationId"`
	MethodID      string            `json:"methodId"`
	MethodVersion int               `json:"methodVersion"`
	CreatedAt     string            `json:"createdAt"`
	Engine        Engine            `json:"engine"`
	Pollutants    []PollutantResult `json:"pollutants"`
	Groups        []GroupResult     `json:"groups"` // 聚合结果
}

type GroupResult struct {
	Code   string  `json:"code"`
	Name   string  `json:"name"`
	Amount float64 `json:"amount"` // 聚合计算出的总浓度
}

type Engine struct {
	Name    string `json:"name"`
	Version string `json:"version"`
	GitSHA  string `json:"gitSha"`
}

type PollutantResult struct {
	Code   string  `json:"code"`
	Name   string  `json:"name"`
	Status string  `json:"status"`
	RtS    float64 `json:"rtS"`
	StartS float64 `json:"startS"` // 峰起点时间 (秒)
	EndS   float64 `json:"endS"`   // 峰终点时间 (秒)
	Area   float64 `json:"area"`
	Height float64 `json:"height"`
	Amount float64 `json:"amount"` // 计算出的浓度
}
