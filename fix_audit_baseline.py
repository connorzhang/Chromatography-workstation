import re

with open(r'i:\GIT\VS2022\Chromatography-workstation\src\edge\cmd\collector\audit_snapshot.go', 'r', encoding='utf-8') as f:
    content = f.read()

new_takeAuditSnapshot = """func takeAuditSnapshot(states *sync.Map) {
	log.Println("[Audit] takeAuditSnapshot triggered")
	var te *telemetryEvent
	var baselineMax, baselineMin, baselineDrift, baselineNoise *float64
	devCount := 0
	nonNilCount := 0

	states.Range(func(key, value interface{}) bool {
		devCount++
		st := value.(*deviceState)
		st.mu.Lock()
		if st.LastTelemetry != nil {
			nonNilCount++
			te = st.LastTelemetry
			
			// Calculate baseline drift & noise from channel 1 session
			if st.sessions != nil {
				if sess, ok := st.sessions[1]; ok {
					auditConfigMutex.Lock()
					intervalMins := auditConfig.IntervalMins
					auditConfigMutex.Unlock()
					
					intervalSecs := float64(intervalMins) * 60.0
					if sess.dtS > 0 && len(sess.values) > 0 {
						pointsToConsider := int(intervalSecs / sess.dtS)
						if pointsToConsider > len(sess.values) {
							pointsToConsider = len(sess.values)
						}
						if pointsToConsider > 0 {
							startIdx := len(sess.values) - pointsToConsider
							subVals := sess.values[startIdx:]
							
							maxVal := subVals[0]
							minVal := subVals[0]
							for _, v := range subVals {
								if v > maxVal { maxVal = v }
								if v < minVal { minVal = v }
							}
							drift := maxVal - minVal
							
							var sum, sumSq float64
							for _, v := range subVals {
								sum += v
								sumSq += v * v
							}
							mean := sum / float64(len(subVals))
							variance := (sumSq / float64(len(subVals))) - (mean * mean)
							noise := 0.0
							if variance > 0 {
								noise = math.Sqrt(variance)
							}
							
							baselineMax = &maxVal
							baselineMin = &minVal
							baselineDrift = &drift
							baselineNoise = &noise
						}
					}
				}
			}
		}
		log.Printf("[Audit] Evaluated device %v, LastTelemetry is nil? %v\\n", key, st.LastTelemetry == nil)
		st.mu.Unlock()
		return te == nil // if found one, stop ranging
	})

	log.Printf("[Audit] Evaluated %d devices, %d had non-nil LastTelemetry\\n", devCount, nonNilCount)

	if te == nil {
		log.Println("[Audit] te is nil, no snapshot taken")
		return
	}

	snap := AuditSnapshot{
		Timestamp:     time.Now(),
		TempCol:       te.TempCol,
		TempInj1:      te.TempInj1,
		TempInj2:      te.TempInj2,
		TempDet1:      te.TempDet1,
		TempDet2:      te.TempDet2,
		TempDet3:      te.TempDet3,
		CarrierPsi:    te.CarrierPsi,
		CarrierSccm:   te.CarrierSccm,
		H2Psi:         te.H2Psi,
		H2Sccm:        te.H2Sccm,
		AirPsi:        te.AirPsi,
		AirSccm:       te.AirSccm,
		BaselineMax:   baselineMax,
		BaselineMin:   baselineMin,
		BaselineDrift: baselineDrift,
		BaselineNoise: baselineNoise,
	}

	if globalTCDCtrl != nil {
		tcdState := globalTCDCtrl.GetState()
		snap.BridgeCurrent = tcdState.BridgeCurrent
	}

	auditHistoryMutex.Lock()
	auditHistory = append(auditHistory, snap)
	if len(auditHistory) > 10000 {
		auditHistory = auditHistory[len(auditHistory)-10000:]
	}
	saveAuditHistory()
	auditHistoryMutex.Unlock()

	log.Println("[Audit] Snapshot taken successfully at", snap.Timestamp)
}"""

# Replace the old function
content = re.sub(r'func takeAuditSnapshot\(states \*sync\.Map\) \{.*?\nfunc handleAuditAPI', new_takeAuditSnapshot + '\n\nfunc handleAuditAPI', content, flags=re.DOTALL)

with open(r'i:\GIT\VS2022\Chromatography-workstation\src\edge\cmd\collector\audit_snapshot.go', 'w', encoding='utf-8') as f:
    f.write(content)
