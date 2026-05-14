package analyzer

import (
	"errors"
	"math"
	"time"

	contracts "chromatography-workstation/edge/internal/contracts/v1"
)

const EngineName = "edge-analyzer"
const EngineVersion = "0.1.0"

func Analyze(trace contracts.Trace, method contracts.Method, gitSHA string, now time.Time) (contracts.Result, error) {
	if trace.DtS <= 0 {
		return contracts.Result{}, errors.New("dtS must be > 0")
	}
	if len(trace.Values) < 2 {
		return contracts.Result{}, errors.New("values must have at least 2 points")
	}
	if method.Version < 1 {
		return contracts.Result{}, errors.New("method version must be >= 1")
	}
	if len(method.Pollutants) == 0 {
		return contracts.Result{}, errors.New("pollutants must not be empty")
	}

	out := contracts.Result{
		Schema:        "voc-result.v1",
		TraceID:       trace.TraceID,
		DeviceID:      trace.DeviceID,
		StationID:     trace.StationID,
		MethodID:      method.MethodID,
		MethodVersion: method.Version,
		CreatedAt:     now.UTC().Format(time.RFC3339),
		Engine: contracts.Engine{
			Name:    EngineName,
			Version: EngineVersion,
			GitSHA:  gitSHA,
		},
	}

	for _, p := range method.Pollutants {
		r, err := analyzeOne(trace, p)
		if err != nil {
			return contracts.Result{}, err
		}
		out.Pollutants = append(out.Pollutants, r)
	}

	return out, nil
}

func analyzeOne(trace contracts.Trace, p contracts.PollutantSpec) (contracts.PollutantResult, error) {
	if p.StartS < 0 || p.EndS < 0 || p.EndS < p.StartS {
		return contracts.PollutantResult{}, errors.New("invalid startS/endS")
	}
	if p.PaddingS < 0 {
		return contracts.PollutantResult{}, errors.New("paddingS must be >= 0")
	}
	startS := math.Max(0, p.StartS-p.PaddingS)
	endS := math.Min(trace.TimeSpanS, p.EndS+p.PaddingS)
	if endS <= startS {
		return contracts.PollutantResult{}, errors.New("window is empty")
	}

	i0 := clampIndex(int(math.Floor(startS/trace.DtS)), len(trace.Values))
	i1 := clampIndex(int(math.Ceil(endS/trace.DtS)), len(trace.Values))
	if i1 <= i0 {
		i1 = minInt(i0+1, len(trace.Values)-1)
	}

	y0 := trace.Values[i0]
	y1 := trace.Values[i1]
	t0 := float64(i0) * trace.DtS
	t1 := float64(i1) * trace.DtS
	denom := t1 - t0
	if denom == 0 {
		denom = trace.DtS
	}

	peakI := i0
	peakY := trace.Values[i0]
	for i := i0; i <= i1; i++ {
		if trace.Values[i] > peakY {
			peakY = trace.Values[i]
			peakI = i
		}
	}

	baselineAt := func(t float64) float64 {
		f := (t - t0) / denom
		return y0 + (y1-y0)*f
	}

	area := 0.0
	lastT := float64(i0) * trace.DtS
	lastB := baselineAt(lastT)
	lastV := math.Max(0, trace.Values[i0]-lastB)
	for i := i0 + 1; i <= i1; i++ {
		t := float64(i) * trace.DtS
		b := baselineAt(t)
		v := math.Max(0, trace.Values[i]-b)
		dt := t - lastT
		area += (lastV + v) * 0.5 * dt
		lastT = t
		lastB = b
		_ = lastB
		lastV = v
	}

	peakT := float64(peakI) * trace.DtS
	peakB := baselineAt(peakT)
	height := math.Max(0, peakY-peakB)
	status := "detected"
	if height < p.Threshold {
		status = "not_detected"
		area = 0
		height = 0
	}

	return contracts.PollutantResult{
		Code:   p.Code,
		Name:   p.Name,
		Status: status,
		RtS:    round6(peakT),
		Area:   round6(area),
		Height: round6(height),
	}, nil
}

func clampIndex(i int, n int) int {
	if i < 0 {
		return 0
	}
	if i >= n {
		return n - 1
	}
	return i
}

func minInt(a, b int) int {
	if a < b {
		return a
	}
	return b
}

func round6(v float64) float64 {
	return math.Round(v*1e6) / 1e6
}
