package analyzer

import (
	"errors"
	"math"
	"time"

	contracts "chromatography-workstation/edge/internal/contracts/v1"
)

const EngineName = "edge-analyzer"
const EngineVersion = "0.3.5"

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

	// 统一使用全局盲寻峰逻辑
	out.Pollutants = DetectAllPeaks(trace, method)

	// 计算峰分组聚合逻辑
	for _, g := range method.Groups {
		amount := 0.0
		// 累加 IncludeCodes
		for _, inc := range g.IncludeCodes {
			for _, pr := range out.Pollutants {
				if pr.Code == inc && pr.Status == "detected" {
					amount += pr.Amount
				}
			}
		}
		// 扣减 ExcludeCodes (如非甲 = 总烃 - 甲烷)
		for _, exc := range g.ExcludeCodes {
			for _, pr := range out.Pollutants {
				if pr.Code == exc && pr.Status == "detected" {
					amount -= pr.Amount
				}
			}
		}
		if amount < 0 {
			amount = 0 // 防止负值
		}
		out.Groups = append(out.Groups, contracts.GroupResult{
			Code:   g.Code,
			Name:   g.Name,
			Amount: round6(amount),
		})
	}

	return out, nil
}

// calcConcentration 基于多点标定曲线将响应值（面积或高度）转换为浓度
func calcConcentration(response float64, levels []contracts.Level, curveFunc int) float64 {
	if len(levels) == 0 {
		return response // 如果没有校准点，默认返回响应值本身
	}
	
	// 单点校准或所有点都在原点
	if len(levels) == 1 {
		l := levels[0]
		if l.Response <= 0 {
			return 0
		}
		// 默认过原点的单点线性校准
		return response * (l.Amount / l.Response)
	}

	// 线性插值/外推 (遗留系统中 CurveFunc = 0 为线性)
	// 为了简便，目前先实现两点/多点的线性分段插值
	// TODO: 后续可加入最小二乘法进行多项式拟合
	for i := 0; i < len(levels)-1; i++ {
		l1, l2 := levels[i], levels[i+1]
		// 确保 l1 < l2
		if l1.Response > l2.Response {
			l1, l2 = l2, l1
		}
		
		if response >= l1.Response && response <= l2.Response {
			if l2.Response == l1.Response {
				return l1.Amount
			}
			f := (response - l1.Response) / (l2.Response - l1.Response)
			return l1.Amount + f*(l2.Amount-l1.Amount)
		}
	}
	
	// 如果超出了最大点或小于最小点，采用最近的一段进行线性外推
	if response < levels[0].Response {
		l1, l2 := levels[0], levels[1]
		if l2.Response == l1.Response { return 0 }
		f := (response - l1.Response) / (l2.Response - l1.Response)
		res := l1.Amount + f*(l2.Amount-l1.Amount)
		if res < 0 { return 0 }
		return res
	}
	
	l1, l2 := levels[len(levels)-2], levels[len(levels)-1]
	if l2.Response == l1.Response { return l2.Amount }
	f := (response - l1.Response) / (l2.Response - l1.Response)
	return l1.Amount + f*(l2.Amount-l1.Amount)
}

// smooth 采用与遗留系统等价的滑动平均 (Moving Average) 进行数据平滑，以降低高频噪声
func smooth(values []float64, windowSize int) []float64 {
	n := len(values)
	if n == 0 || windowSize <= 1 {
		return values
	}
	if windowSize > n {
		windowSize = n
	}

	smoothed := make([]float64, n)
	sum := 0.0

	// 初始窗口
	for i := 0; i < windowSize; i++ {
		sum += values[i]
	}

	half := windowSize / 2
	for i := 0; i < n; i++ {
		if i > half && i+windowSize-half-1 < n {
			sum = sum - values[i-half-1] + values[i+windowSize-half-1]
		} else if i <= half {
			// 前端边缘保持不变或使用已有的sum平均
			// 严格对齐老系统的话，此处可以不减不加，直接算当前有效窗口内的平均
		}
		
		// 为了严谨，计算实际的窗口大小 (防止边缘越界)
		start := i - half
		end := i + (windowSize - half)
		if start < 0 { start = 0 }
		if end > n { end = n }
		
		localSum := 0.0
		for j := start; j < end; j++ {
			localSum += values[j]
		}
		smoothed[i] = localSum / float64(end-start)
	}
	return smoothed
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

	// 1. 数据平滑：使用 16 个采样点的窗口进行平滑 (对齐老系统的默认平滑)
	smoothedVals := smooth(trace.Values, 16)

	// 2. 基线计算：实现多种基线模式
	// 默认使用线性基线 (General) 连接峰起点和终点
	y0 := smoothedVals[i0]
	y1 := smoothedVals[i1]
	
	// 如果配置了水平前延或水平后延基线 (Horizontal)
	if p.BaselineMode == "ForwHorz" {
		y1 = y0 // 水平向后
	} else if p.BaselineMode == "BackHorz" {
		y0 = y1 // 水平向前
	}
	// TODO: 谷对谷 (Valley-to-Valley) 和切线 (Tangent) 处理需在全图识别后处理，这里先按窗口局部处理

	t0 := float64(i0) * trace.DtS
	t1 := float64(i1) * trace.DtS
	denom := t1 - t0
	if denom == 0 {
		denom = trace.DtS
	}

	// 3. 寻找峰顶
	peakI := i0
	peakY := smoothedVals[i0]
	for i := i0; i <= i1; i++ {
		if smoothedVals[i] > peakY {
			peakY = smoothedVals[i]
			peakI = i
		}
	}

	baselineAt := func(t float64) float64 {
		f := (t - t0) / denom
		return y0 + (y1-y0)*f
	}

	// 4. 梯形积分计算面积
	area := 0.0
	lastT := float64(i0) * trace.DtS
	lastB := baselineAt(lastT)
	lastV := math.Max(0, smoothedVals[i0]-lastB)
	for i := i0 + 1; i <= i1; i++ {
		t := float64(i) * trace.DtS
		b := baselineAt(t)
		v := math.Max(0, smoothedVals[i]-b)
		dt := t - lastT
		
		// 遗留系统在积分时会过滤掉负峰(除非开启 Add Negative)
		// 这里默认 v 是 math.Max(0, y-b)，即只积分正峰部分
		area += (lastV + v) * 0.5 * dt
		
		lastT = t
		lastB = b
		lastV = v
	}
	
	// C# 代码中面积乘以 60 将单位转化为微伏·秒 (保留时间是分钟)
	// 这里假设我们的 DtS 已经是秒，因此无需乘 60，但需与遗留单位对齐时，按需求放大。
	// 这里暂时保留原始秒级积分值。

	peakT := float64(peakI) * trace.DtS
	peakB := baselineAt(peakT)
	height := math.Max(0, peakY-peakB)
	status := "detected"
	
	// 5. 检峰条件判断 (阈值过滤)
	if height < p.Threshold {
		status = "not_detected"
		area = 0
		height = 0
	}

	res := contracts.PollutantResult{
		Code:   p.Code,
		Name:   p.Name,
		Status: status,
		RtS:    round6(peakT),
		Area:   round6(area),
		Height: round6(height),
	}

	// 根据 RespStyle (面积或高度) 计算浓度
	resp := res.Area
	if p.RespStyle == 1 {
		resp = res.Height
	}
	res.Amount = calcConcentration(resp, p.Levels, p.CurveFunc)

	return res, nil
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
