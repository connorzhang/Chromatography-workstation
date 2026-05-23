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
	// 用户要求最小峰高默认 0.1。为了防止极大峰导致全局 1% 阈值过高而漏掉小峰，我们将动态比例调小，或者直接主要依赖绝对峰高 0.1
	threshold := math.Max((maxY - minY) * 0.002, 0.1) 

	var peaks []contracts.PollutantResult

	for i := 1; i < len(smoothed)-1; i++ {
		// 寻找局部极大值
		if smoothed[i] > smoothed[i-1] && smoothed[i] >= smoothed[i+1] {
			// 向左找谷底：直到不再下降
			left := i
			for left > 0 {
				if smoothed[left-1] < smoothed[left] {
					left--
				} else {
					// 可能是平坦基线或微小噪声起伏，往左展望 15 个点看是否还会继续下降
					foundLower := false
					for k := 1; k <= 15 && left-k >= 0; k++ {
						if smoothed[left-k] < smoothed[left] {
							left = left - k
							foundLower = true
							break
						}
						// 如果往左展望时发现上升超过了阈值的一小部分，说明遇到了另一个峰，立即停止
						if smoothed[left-k]-smoothed[left] > threshold*0.2 {
							break
						}
					}
					if !foundLower {
						break
					}
				}
			}

			// 向右找谷底：直到不再下降
			right := i
			for right < len(smoothed)-1 {
				if smoothed[right+1] < smoothed[right] {
					right++
				} else {
					// 往右展望 15 个点
					foundLower := false
					for k := 1; k <= 15 && right+k < len(smoothed); k++ {
						if smoothed[right+k] < smoothed[right] {
							right = right + k
							foundLower = true
							break
						}
						if smoothed[right+k]-smoothed[right] > threshold*0.2 {
							break
						}
					}
					if !foundLower {
						break
					}
				}
			}

			// 如果两边谷底太不平衡，很可能是跨了其他峰的宽基线
			if smoothed[i]-smoothed[left] < threshold || smoothed[i]-smoothed[right] < threshold {
				continue
			}

			y0 := smoothed[left]
			y1 := smoothed[right]

			peakT := float64(i) * trace.DtS
			t0 := float64(left) * trace.DtS
			t1 := float64(right) * trace.DtS

			// 过滤掉积分区间太窄的假峰 (宽度小于 1.5 秒)
			if t1-t0 < 1.5 {
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
		windowLeft := p.StartS - 6.0
		windowRight := p.EndS + 6.0
		
		bestIdx := -1
		bestResp := -1.0

		for i, pk := range peaks {
			// 如果该峰已经被匹配给其他组分了，则跳过
			if pk.Code != fmt.Sprintf("Unk_%d", i) {
				continue
			}
			
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
				RtS:    (p.StartS + p.EndS) / 2.0,
				StartS: p.StartS,
				EndS:   p.EndS,
				Area:   0,
				Height: 0,
				Amount: 0,
			})
		}
	}

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
