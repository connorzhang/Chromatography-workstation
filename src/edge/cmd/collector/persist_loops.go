package main

import (
	"encoding/json"
	"os"
	"path/filepath"
	"sync"
	"time"
)

func startPersistence(states *sync.Map) {
	if pstore == nil {
		return
	}
	go func() {
		tk := time.NewTicker(2 * time.Second)
		defer tk.Stop()
		for range tk.C {
			persistSessions(states)
		}
	}()
	go func() {
		tk := time.NewTicker(60 * time.Second)
		defer tk.Stop()
		for range tk.C {
			persistSnapshot(states)
		}
	}()
}

func persistSessions(states *sync.Map) {
	if pstore == nil {
		return
	}
	states.Range(func(key, value any) bool {
		deviceID := key.(string)
		if deviceID == "" || len(deviceID) >= 3 && deviceID[:3] == "DEV" {
			return true
		}
		st := value.(*deviceState)
		st.mu.Lock()
		chs := make([]int, 0, len(st.sessions))
		for ch := range st.sessions {
			chs = append(chs, ch)
		}
		st.mu.Unlock()
		for i := 0; i < len(chs); i++ {
			ch := chs[i]
			st.mu.Lock()
			s := st.sessions[ch]
			if s == nil || s.dtS <= 0 || len(s.values) < 2 {
				st.mu.Unlock()
				continue
			}
			vals := append([]float64(nil), s.values...)
			if len(vals) > 200000 {
				vals = vals[len(vals)-200000:]
			}
			payload := map[string]any{
				"deviceId":     deviceID,
				"channel":      ch,
				"sessionToken": s.token,
				"active":       s.active,
				"startedAt":    s.startedAt.UTC().Format(time.RFC3339),
				"dtS":          s.dtS,
				"timeSpanS":    float64(len(vals)-1) * s.dtS,
				"values":       vals,
				"lastSample":   s.lastSample,
				"valuesCount":  len(vals),
				"totalCount":   len(s.values),
			}
			st.mu.Unlock()
			pstore.SaveSession(deviceID, ch, payload)
		}
		return true
	})
}

func persistSnapshot(states *sync.Map) {
	if pstore == nil {
		return
	}
	uiMu.Lock()
	uiCopy := map[string]uiState{}
	for k, v := range uiByDevice {
		uiCopy[k] = v
	}
	last := uiLastDevice
	uiMu.Unlock()

	snap := map[string]any{
		"at":           time.Now().UTC().Format(time.RFC3339),
		"lastDeviceId": last,
		"ui":           uiCopy,
	}
	b, err := json.Marshal(snap)
	if err != nil {
		return
	}
	stateJSON := string(b)
	pstore.SaveSnapshot(stateJSON)
	_ = os.MkdirAll(filepath.Join(".run", "snapshots"), 0o755)
	_ = os.WriteFile(filepath.Join(".run", "snapshots", "state-"+time.Now().UTC().Format("20060102-150405")+".json"), []byte(stateJSON), 0o644)
}

