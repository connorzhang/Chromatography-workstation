package analyzer

import (
	"fmt"
	"math"

	contracts "chromatography-workstation/edge/internal/contracts/v1"
)

// DetectAllPeaks 盲寻所有的峰，基于局部极大值和谷底寻找，阈值为全局振幅的 0.5%
func DetectAllPeaks(trace contracts.Trace, method contracts.Method) []contracts.PollutantResult {
	if trace.DtS <= 0 || len(trace.Values) < 3 {
		return nil
	}
	smoothed := smooth(trace.Values, 5) // 轻度平滑

	// 计算全局振幅阈值
	minY, maxY := smoothed[0], smoothed[0]
	for _, v := range smoothed {
		if v < minY {
			minY = v
		}
		if v > maxY {
			maxY = v
		}
	}
	// 获取用户配置的积分参数，并设置兜底默认值
	minHeight := method.Integration.MinHeight
	if minHeight <= 0 {
		minHeight = 0.1 // 默认 0.1
	}
	minWidth := method.Integration.MinWidth
	if minWidth <= 0 {
		minWidth = 1.5 // 默认 1.5 秒
	}
	slope := method.Integration.Slope
	if slope <= 0 {
		slope = 0 // 目前作为容差储备
	}

	// 动态全局极差比例作为辅助阈值（防止单点极高导致所有峰消失，比例设为极小 0.2%）
	dynThreshold := math.Max((maxY - minY) * 0.002, minHeight)
	threshold := dynThreshold

	var peaks []contracts.PollutantResult

	for i := 1; i < len(smoothed)-1; i++ {
		// 寻找局部极大值
		if smoothed[i] > smoothed[i-1] && smoothed[i] >= smoothed[i+1] {
			// 1. 寻找原始的物理谷底（单调下降结束点）
			leftValley := i
			for j := i - 1; j > 0; j-- {
				if smoothed[j-1] > smoothed[j] { // 遇到上升，说明到达谷底
					break
				}
				leftValley = j
			}

			// 2. 寻找左侧的最大斜率（拐点）
			maxSlopeLeft := 0.0
			for j := i; j > leftValley; j-- {
				s := smoothed[j] - smoothed[j-1]
				if s > maxSlopeLeft {
					maxSlopeLeft = s
				}
			}

			// 3. 动态计算截断斜率阈值
			// 采用专业色谱软件的一阶导数阈值法：使用最大斜率的 3% 作为截止条件
			// 这样能自动适应大峰和小峰：大峰斜率大，截止点自然靠外；小峰斜率小，截止点自然紧凑
			cutoffLeft := maxSlopeLeft * 0.01
			if method.Integration.Slope > 0 {
				cutoffLeft = method.Integration.Slope * trace.DtS
			}
			if cutoffLeft < dynThreshold*0.01 {
				cutoffLeft = dynThreshold * 0.01
			}

			// 4. 从峰顶向左寻找截断点
			left := leftValley
			passedInfLeft := false
			for j := i - 1; j >= leftValley; j-- {
				s := smoothed[j+1] - smoothed[j]
				// 只要经过了最大斜率的 50%，就认为越过了拐点
				if s >= maxSlopeLeft*0.5 {
					passedInfLeft = true
				}
				// 越过拐点后，如果斜率降到了截止阈值以下，就截断
				if passedInfLeft && s <= cutoffLeft {
					left = j
					break
				}
			}
			// 兜底：如果没触发截断（比如峰很窄，很快就到底了），就用物理谷底
			if !passedInfLeft || left == leftValley {
				left = leftValley
			}

			// --- 右侧同理 ---
			rightValley := i
			for j := i + 1; j < len(smoothed)-1; j++ {
				if smoothed[j+1] > smoothed[j] {
					break
				}
				rightValley = j
			}

			maxSlopeRight := 0.0
			for j := i; j < rightValley; j++ {
				s := smoothed[j] - smoothed[j+1]
				if s > maxSlopeRight {
					maxSlopeRight = s
				}
			}

			cutoffRight := maxSlopeRight * 0.01
			if method.Integration.Slope > 0 {
				cutoffRight = method.Integration.Slope * trace.DtS
			}
			if cutoffRight < dynThreshold*0.01 {
				cutoffRight = dynThreshold * 0.01
			}

			right := rightValley
			passedInfRight := false
			for j := i + 1; j <= rightValley; j++ {
				s := smoothed[j-1] - smoothed[j]
				if s >= maxSlopeRight*0.5 {
					passedInfRight = true
				}
				if passedInfRight && s <= cutoffRight {
					right = j
					break
				}
			}
			if !passedInfRight || right == rightValley {
				right = rightValley
			}

			// 如果两边谷底太不平衡，很可能是跨了其他峰的宽基线
			// 但也不能直接 continue 丢弃，而是应该记录下来
			// if smoothed[i]-smoothed[left] < threshold || smoothed[i]-smoothed[right] < threshold {
			// 	continue
			// }

			y0 := smoothed[left]
			y1 := smoothed[right]

			peakT := float64(i) * trace.DtS
			t0 := float64(left) * trace.DtS
			t1 := float64(right) * trace.DtS

			// 过滤掉积分区间太窄的假峰
			if t1-t0 < minWidth {
				continue
			}

			denom := t1 - t0
			if denom == 0 {
				denom = trace.DtS
			}
			f := (peakT - t0) / denom
			peakB := y0 + (y1 - y0) * f

			height := smoothed[i] - peakB
			if height >= threshold {
				area := 0.0
				lastT := t0
				lastB := y0
				lastV := math.Max(0, smoothed[left]-lastB)
				for j := left + 1; j <= right; j++ {
					t := float64(j) * trace.DtS
					f := (t - t0) / denom
					b := y0 + (y1 - y0) * f
					v := math.Max(0, smoothed[j]-b)
					dt := t - lastT
					area += (lastV + v) * 0.5 * dt
					lastT = t
					lastB = b
					lastV = v
				}

				peaks = append(peaks, contracts.PollutantResult{
					Code:   "", // 暂空，稍后分配
					Name:   "", // 暂空
					Status: "detected",
					RtS:    round6(peakT),
					StartS: round6(t0),
					EndS:   round6(t1),
					Area:   round6(area),
					Height: round6(height),
					Amount: 0,
				})
			}
		}
	}

	// 初始化所有峰为未知
	for i := range peaks {
		peaks[i].Code = fmt.Sprintf("Unk_%d", i)
		peaks[i].Name = fmt.Sprintf("未知峰_%d", i)
	}

	// 如果传入的方法中没有组分，或者全是未知，则直接返回所有的未知峰
	if len(method.Pollutants) == 0 {
		return peaks
	}

	// 我们需要将配置的方法组分按保留时间从左到右排序，以保证峰匹配顺序是确定的
	// 但是由于 Go 不太好在这里直接给 slice 排序（不想引入额外逻辑），我们仍然保持按配置顺序匹配
	
	var finalPeaks []contracts.PollutantResult

	// 遍历方法中配置的每一个标定组分，在检测到的峰中寻找落在区间内且响应值最大的峰作为匹配峰
	for _, p := range method.Pollutants {
		// 这里必须用 p.StartS 和 p.EndS 来约束匹配窗口
		// 注意：如果之前标定的时候没有生成 StartS 和 EndS，我们要退回使用保留时间和 PaddingS
		windowLeft := p.StartS
		windowRight := p.EndS
		if windowLeft <= 0 || windowRight <= 0 || windowLeft == windowRight {
			windowLeft = p.RtS - (p.PaddingS / 2.0)
			windowRight = p.RtS + (p.PaddingS / 2.0)
		}
		
		bestIdx := -1
		bestResp := -1.0

		for i, pk := range peaks {
			// 如果该峰已经被匹配给其他组分了，则跳过
			if pk.Code != fmt.Sprintf("Unk_%d", i) {
				continue
			}
			
			// 匹配条件：该峰的顶点保留时间 (RtS) 必须落在该组分的窗宽内
			if pk.RtS >= windowLeft && pk.RtS <= windowRight {
				resp := pk.Area
				if p.RespStyle == 1 {
					resp = pk.Height
				}
				if resp > bestResp {
					bestResp = resp
					bestIdx = i
				}
			}
		}

		if bestIdx != -1 {
			peaks[bestIdx].Code = p.Code
			peaks[bestIdx].Name = p.Name
			peaks[bestIdx].Amount = calcConcentration(bestResp, p.Levels, p.CurveFunc)
			finalPeaks = append(finalPeaks, peaks[bestIdx])
		} else {
			// 如果没找到对应的峰，也要在结果中体现出来，状态为未检出
			finalPeaks = append(finalPeaks, contracts.PollutantResult{
				Code:   p.Code,
				Name:   p.Name,
				Status: "not_detected",
				RtS:    p.RtS,
				StartS: p.StartS,
				EndS:   p.EndS,
				Area:   0,
				Height: 0,
				Amount: 0,
			})
		}
	}

	// // 将未被匹配的未知峰也追加到最终结果中
	// for _, pk := range peaks {
	// 	if pk.Code != "" && len(pk.Code) > 4 && pk.Code[:4] == "Unk_" {
	// 		finalPeaks = append(finalPeaks, pk)
	// 	}
	// }

	return finalPeaks
}

// DetectPeakInWindow 在指定窗口内寻峰并积分
func DetectPeakInWindow(trace contracts.Trace, method contracts.Method, startS float64, endS float64, customName string) *contracts.PollutantResult {
	if trace.DtS <= 0 || len(trace.Values) < 2 {
		return nil
	}

	i0 := clampIndex(int(math.Floor(startS/trace.DtS)), len(trace.Values))
	i1 := clampIndex(int(math.Ceil(endS/trace.DtS)), len(trace.Values))
	if i1 <= i0 {
		i1 = minInt(i0+1, len(trace.Values)-1)
	}

	smoothed := smooth(trace.Values, 16)
	y0 := smoothed[i0]
	y1 := smoothed[i1]
	t0 := float64(i0) * trace.DtS
	t1 := float64(i1) * trace.DtS
	denom := t1 - t0
	if denom == 0 {
		denom = trace.DtS
	}

	peakI := i0
	peakY := smoothed[i0]
	for i := i0; i <= i1; i++ {
		if smoothed[i] > peakY {
			peakY = smoothed[i]
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
	lastV := math.Max(0, smoothed[i0]-lastB)
	for i := i0 + 1; i <= i1; i++ {
		t := float64(i) * trace.DtS
		b := baselineAt(t)
		v := math.Max(0, smoothed[i]-b)
		dt := t - lastT
		area += (lastV + v) * 0.5 * dt
		lastT = t
		lastB = b
		lastV = v
	}

	peakT := (t0 + t1) / 2.0 // 取左和右时间的中间作为保留时间
	peakB := baselineAt(float64(peakI) * trace.DtS)
	height := math.Max(0, peakY-peakB)

	// 匹配 Method
	name := customName
	code := customName
	var matchedSpec *contracts.PollutantSpec
	for _, p := range method.Pollutants {
		if p.Code == customName || p.Name == customName {
			name = p.Name
			code = p.Code
			tmp := p
			matchedSpec = &tmp
			break
		}
	}

	amount := 0.0
	if matchedSpec != nil {
		resp := area
		if matchedSpec.RespStyle == 1 {
			resp = height
		}
		amount = calcConcentration(resp, matchedSpec.Levels, matchedSpec.CurveFunc)
	}

	return &contracts.PollutantResult{
		Code:   code,
		Name:   name,
		Status: "detected",
		RtS:    round6(peakT),
		StartS: round6(t0),
		EndS:   round6(t1),
		Area:   round6(area),
		Height: round6(height),
		Amount: amount,
	}
}
