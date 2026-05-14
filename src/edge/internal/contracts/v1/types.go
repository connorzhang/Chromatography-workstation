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
	Schema     string          `json:"schema"`
	MethodID   string          `json:"methodId"`
	Version    int             `json:"version"`
	Pollutants []PollutantSpec `json:"pollutants"`
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
	Area   float64 `json:"area"`
	Height float64 `json:"height"`
}
