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

)

const auditConfigFile = "audit_config.json"
const auditHistoryFile = "audit_history.json"

type AuditConfig struct {
	Enabled     bool `json:"enabled"`
	IntervalMins int  `json:"intervalMins"` // snapshot interval in minutes
}

type AuditSnapshot struct {
Timestamp     time.Time `json:"timestamp"`
TempBox       float64   `json:"tempBox"`
TempInj1      float64   `json:"tempInj1,omitempty"` // For backward compatibility
CarrierPsi    float64   `json:"carrierPsi"`
CarrierSccm   float64   `json:"carrierSccm"`
BridgeCurrent uint8     `json:"bridgeCurrent"`
BaselineMax   float64   `json:"baselineMax"`
BaselineMin   float64   `json:"baselineMin"`
BaselineDrift float64   `json:"baselineDrift"`
BaselineNoise float64   `json:"baselineNoise"`
}

func round4(v float64) float64 {
return math.Round(v*10000) / 10000
}

func roundPtr(v *float64) float64 {
if v == nil {
return 0.0
}
return round4(*v)
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
for i := range auditHistory {
if auditHistory[i].TempBox == 0 && auditHistory[i].TempInj1 != 0 {
auditHistory[i].TempBox = auditHistory[i].TempInj1
}
auditHistory[i].TempInj1 = 0
}
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
snap := AuditSnapshot{ Timestamp: time.Now() }
devCount := 0
nonNilCount := 0

states.Range(func(key, value interface{}) bool {
devCount++
st := value.(*deviceState)
st.mu.Lock()
if st.LastTelemetry != nil {
nonNilCount++
if st.LastTelemetry.TempInj1 != nil { snap.TempBox = roundPtr(st.LastTelemetry.TempInj1) }
if st.LastTelemetry.CarrierPsi != nil { snap.CarrierPsi = roundPtr(st.LastTelemetry.CarrierPsi) }
if st.LastTelemetry.CarrierSccm != nil { snap.CarrierSccm = roundPtr(st.LastTelemetry.CarrierSccm) }

if st.sessions != nil {
if sess, ok := st.sessions[1]; ok {
auditConfigMutex.Lock()
intervalMins := auditConfig.IntervalMins
auditConfigMutex.Unlock()

intervalSecs := float64(intervalMins) * 60.0
if sess.dtS > 0 && len(sess.values) > 0 {
pointsToConsider := int(intervalSecs / sess.dtS)
if pointsToConsider > len(sess.values) { pointsToConsider = len(sess.values) }
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
for _, v := range subVals { sum += v; sumSq += v * v }
mean := sum / float64(len(subVals))
variance := (sumSq / float64(len(subVals))) - (mean * mean)
noise := 0.0
if variance > 0 { noise = math.Sqrt(variance) }
snap.BaselineMax = round4(maxVal)
snap.BaselineMin = round4(minVal)
snap.BaselineDrift = round4(drift)
snap.BaselineNoise = round4(noise)
}
}
}
}
}
st.mu.Unlock()
return true // continue ranging
})

log.Printf("[Audit] Evaluated %d devices, %d had non-nil LastTelemetry\n", devCount, nonNilCount)
if nonNilCount == 0 { return }

if globalTCDCtrl != nil {
snap.BridgeCurrent = globalTCDCtrl.GetState().BridgeCurrent
}

auditHistoryMutex.Lock()
auditHistory = append(auditHistory, snap)
if len(auditHistory) > 10000 { auditHistory = auditHistory[len(auditHistory)-10000:] }
saveAuditHistory()
auditHistoryMutex.Unlock()

var devID string = "SYSTEM"
states.Range(func(key, value interface{}) bool {
st := value.(*deviceState)
st.mu.Lock()
if st.sessions != nil { devID = fmt.Sprintf("%v", key) }
st.mu.Unlock()
return devID == "SYSTEM"
})

if mqttClient != nil {
mqttClient.PublishAudit(devID, map[string]any{
"event": "audit_snapshot",
"time": snap.Timestamp.Unix(),
"snapshot": snap,
})
}
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
			fmt.Fprintf(w, "{\"status\":\"ok\"}")
			return
		}

		http.Error(w, "Method not allowed", http.StatusMethodNotAllowed)
	}
}
