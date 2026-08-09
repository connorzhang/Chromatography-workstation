package main

import (
	"encoding/json"
	"fmt"
	"io/ioutil"
	"log"
	"math"
	"net/http"
	"sync"
	"time"

	"chromatography-workstation/edge/internal/publisher"
)

const auditConfigFile = "audit_config.json"
const auditHistoryFile = "audit_history.json"

type AuditConfig struct {
	Enabled     bool `json:"enabled"`
	IntervalMins int  `json:"intervalMins"` // snapshot interval in minutes
}

type AuditSnapshot struct {
	Timestamp     time.Time `json:"timestamp"`
	TempCol       *float64  `json:"tempCol,omitempty"`
	TempInj1      *float64  `json:"tempInj1,omitempty"`
	TempInj2      *float64  `json:"tempInj2,omitempty"`
	TempDet1      *float64  `json:"tempDet1,omitempty"`
	TempDet2      *float64  `json:"tempDet2,omitempty"`
	TempDet3      *float64  `json:"tempDet3,omitempty"`
	CarrierPsi    *float64  `json:"carrierPsi,omitempty"`
	CarrierSccm   *float64  `json:"carrierSccm,omitempty"`
	H2Psi         *float64  `json:"h2Psi,omitempty"`
	H2Sccm        *float64  `json:"h2Sccm,omitempty"`
	AirPsi        *float64  `json:"airPsi,omitempty"`
	AirSccm       *float64  `json:"airSccm,omitempty"`
	BridgeCurrent uint8     `json:"bridgeCurrent"`
	BaselineMax   *float64  `json:"baselineMax,omitempty"`
	BaselineMin   *float64  `json:"baselineMin,omitempty"`
	BaselineDrift *float64  `json:"baselineDrift,omitempty"`
	BaselineNoise *float64  `json:"baselineNoise,omitempty"`
}

var (
	auditConfigMutex   sync.Mutex
	auditConfig        AuditConfig
	auditHistoryMutex  sync.Mutex
	auditHistory       []AuditSnapshot
	auditRoutineTicker *time.Ticker
	auditRoutineDone   chan bool
)

func initAuditSnapshot(states *sync.Map) {
	loadAuditConfig()
	loadAuditHistory()
	restartAuditRoutine(states)
}

func loadAuditConfig() {
	auditConfigMutex.Lock()
	defer auditConfigMutex.Unlock()

	auditConfig = AuditConfig{
		Enabled:      true,
		IntervalMins: 5, // default
	}

	data, err := ioutil.ReadFile(auditConfigFile)
	if err == nil {
		json.Unmarshal(data, &auditConfig)
	}
}

func saveAuditConfig() {
	data, _ := json.MarshalIndent(auditConfig, "", "  ")
	ioutil.WriteFile(auditConfigFile, data, 0644)
}

func loadAuditHistory() {
	auditHistoryMutex.Lock()
	defer auditHistoryMutex.Unlock()

	auditHistory = []AuditSnapshot{}
	data, err := ioutil.ReadFile(auditHistoryFile)
	if err == nil {
		json.Unmarshal(data, &auditHistory)
	}
}

func saveAuditHistory() {
	data, _ := json.MarshalIndent(auditHistory, "", "  ")
	ioutil.WriteFile(auditHistoryFile, data, 0644)
}

func restartAuditRoutine(states *sync.Map) {
	auditConfigMutex.Lock()
	enabled := auditConfig.Enabled
	interval := auditConfig.IntervalMins
	auditConfigMutex.Unlock()

	if auditRoutineDone != nil {
		close(auditRoutineDone)
		auditRoutineDone = nil
	}

	if auditRoutineTicker != nil {
		auditRoutineTicker.Stop()
		auditRoutineTicker = nil
	}

	if !enabled || interval <= 0 {
		return
	}

	auditRoutineTicker = time.NewTicker(time.Duration(interval) * time.Minute)
	auditRoutineDone = make(chan bool)

	go func() {
takeAuditSnapshot(states)
for {
select {
case <-auditRoutineDone:
return
case <-auditRoutineTicker.C:
takeAuditSnapshot(states)
}
}
}()
}

func takeAuditSnapshot(states *sync.Map) {
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
		log.Printf("[Audit] Evaluated device %v, LastTelemetry is nil? %v\n", key, st.LastTelemetry == nil)
		st.mu.Unlock()
		return te == nil // if found one, stop ranging
	})

	log.Printf("[Audit] Evaluated %d devices, %d had non-nil LastTelemetry\n", devCount, nonNilCount)

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

// Send to MQTT via publisher
var devID string = "SYSTEM"
states.Range(func(key, value interface{}) bool {
devID = fmt.Sprintf("%v", key)
return false
})

publisher.GlobalPublisher.PublishInfo(devID, devID, map[string]interface{}{
"event":    "audit_snapshot",
"time":     snap.Timestamp.Unix(),
"snapshot": snap,
})
}

func handleAuditAPI(states *sync.Map) http.HandlerFunc {
	return func(w http.ResponseWriter, r *http.Request) {
		if r.Method == http.MethodGet {
			auditHistoryMutex.Lock()
			historyCopy := make([]AuditSnapshot, len(auditHistory))
			copy(historyCopy, auditHistory)
			auditHistoryMutex.Unlock()

			auditConfigMutex.Lock()
			configCopy := auditConfig
			auditConfigMutex.Unlock()

			resp := map[string]interface{}{
				"config":  configCopy,
				"history": historyCopy,
			}

			w.Header().Set("Content-Type", "application/json")
			json.NewEncoder(w).Encode(resp)
			return
		}

		if r.Method == http.MethodPost {
			var reqConfig AuditConfig
			if err := json.NewDecoder(r.Body).Decode(&reqConfig); err != nil {
				http.Error(w, err.Error(), http.StatusBadRequest)
				return
			}

			auditConfigMutex.Lock()
			auditConfig.Enabled = reqConfig.Enabled
			if reqConfig.IntervalMins >= 1 {
				auditConfig.IntervalMins = reqConfig.IntervalMins
			}
			saveAuditConfig()
			auditConfigMutex.Unlock()

			restartAuditRoutine(states)

			w.Header().Set("Content-Type", "application/json")
			fmt.Fprintf(w, "{\"status\":\"ek\"}")
			return
		}

		http.Error(w, "Method not allowed", http.StatusMethodNotAllowed)
	}
}
