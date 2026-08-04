package main

import (
	"encoding/json"
	"fmt"
	"io/ioutil"
	"log"
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
	var te *telemetryEvent

	states.Range(func(key, value interface{}) bool {
		st := value.(*deviceState)
		st.mu.Lock()
		if st.LastTelemetry != nil {
			te = st.LastTelemetry
		}
		st.mu.Unlock()
		return te == nil // if found one, stop ranging
	})

	if te == nil {
		return
	}

	snap := AuditSnapshot{
		Timestamp:   time.Now(),
		TempCol:     te.TempCol,
		TempInj1:    te.TempInj1,
		TempInj2:    te.TempInj2,
		TempDet1:    te.TempDet1,
		TempDet2:    te.TempDet2,
		TempDet3:    te.TempDet3,
		CarrierPsi:  te.CarrierPsi,
		CarrierSccm: te.CarrierSccm,
		H2Psi:       te.H2Psi,
		H2Sccm:      te.H2Sccm,
		AirPsi:      te.AirPsi,
		AirSccm:     te.AirSccm,
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

	log.Println("[Audit] Snapshot taken at", snap.Timestamp)
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
