package main

import (
	"bufio"
	"encoding/json"
	"errors"
	"fmt"
	"io"
	"log"
	"math"
	"net"
	"net/http"
	"os"
	"path/filepath"
	"sort"
	"strconv"
	"strings"
	"sync"
	"sync/atomic"
	"time"

	"chromatography-workstation/edge/internal/analyzer"
	v1 "chromatography-workstation/edge/internal/contracts/v1"
	"chromatography-workstation/edge/internal/protocol/chromsend143"
	"chromatography-workstation/edge/internal/protocol/gckc"
	"chromatography-workstation/edge/internal/realtime"
)

var startedAt = time.Now().UTC()

var runSessionSeq uint64

type deviceState struct {
	mu       sync.Mutex
	lastTS   map[int]float64
	lastSeen time.Time
	lastCmd  byte
	cmdCnt   map[byte]uint64
	conn     net.Conn
	seq      uint32
	last143  time.Time
	sessions map[int]*runSession
	lastResultByCh map[int]lastResult
}

type lastResult struct {
	token string
	at    time.Time
	res   v1.Result
}

type runSession struct {
	token      string
	active     bool
	startedAt  time.Time
	snapshotDone bool
	dtS        float64
	values     []float64
	lastSample float64
}

func newRunSession() *runSession {
	n := atomic.AddUint64(&runSessionSeq, 1)
	return &runSession{token: fmt.Sprintf("%d-%d", time.Now().UnixNano(), n), active: true, startedAt: time.Now()}
}

type event struct {
	Type     string    `json:"type"`
	DeviceID string    `json:"deviceId"`
	At       time.Time `json:"at"`

	Channel       int       `json:"channel"`
	SessionToken  string    `json:"sessionToken"`
	DTs           float64   `json:"dtS"`
	T0s           float64   `json:"t0S"`
	Values        []float64 `json:"values"`
}

type sessionSnapshot struct {
	DtS    float64
	Values []float64
}

type nmhcRecord struct {
	TimeRFC3339 string  `json:"time"`
	DeviceID    string  `json:"deviceId"`
	TraceID     string  `json:"traceId"`
	THC         float64 `json:"thc"`
	CH4         float64 `json:"ch4"`
	NMHC        float64 `json:"nmhc"`
}

type nmhcHistoryStore struct {
	mu       sync.Mutex
	byDevice map[string][]nmhcRecord
	path     string
}

func newNMHCHistoryStore(path string) *nmhcHistoryStore {
	return &nmhcHistoryStore{byDevice: map[string][]nmhcRecord{}, path: path}
}

func (s *nmhcHistoryStore) Load() {
	s.mu.Lock()
	defer s.mu.Unlock()
	s.byDevice = map[string][]nmhcRecord{}
	f, err := os.Open(s.path)
	if err != nil {
		return
	}
	defer f.Close()
	sc := bufio.NewScanner(f)
	for sc.Scan() {
		line := strings.TrimSpace(sc.Text())
		if line == "" {
			continue
		}
		var r nmhcRecord
		if json.Unmarshal([]byte(line), &r) != nil {
			continue
		}
		if r.DeviceID == "" {
			continue
		}
		s.byDevice[r.DeviceID] = append(s.byDevice[r.DeviceID], r)
	}
}

func (s *nmhcHistoryStore) Add(r nmhcRecord) {
	if r.DeviceID == "" || r.TimeRFC3339 == "" {
		return
	}
	s.mu.Lock()
	defer s.mu.Unlock()
	s.byDevice[r.DeviceID] = append(s.byDevice[r.DeviceID], r)
	_ = os.MkdirAll(filepath.Dir(s.path), 0o755)
	f, err := os.OpenFile(s.path, os.O_CREATE|os.O_APPEND|os.O_WRONLY, 0o644)
	if err != nil {
		return
	}
	_, _ = f.Write(append(mustJSONLine(r), '\n'))
	_ = f.Close()
	if pstore != nil {
		pstore.AddNMHC(r)
	}
}

func mustJSONLine(v any) []byte {
	b, err := json.Marshal(v)
	if err != nil {
		return []byte("{}")
	}
	return b
}

func (s *nmhcHistoryStore) Query(deviceID string, from, to *time.Time, limit int) []nmhcRecord {
	s.mu.Lock()
	defer s.mu.Unlock()
	src := s.byDevice[deviceID]
	out := make([]nmhcRecord, 0, len(src))
	for i := 0; i < len(src); i++ {
		r := src[i]
		t, err := time.Parse(time.RFC3339, r.TimeRFC3339)
		if err != nil {
			continue
		}
		if from != nil && t.Before(*from) {
			continue
		}
		if to != nil && t.After(*to) {
			continue
		}
		out = append(out, r)
	}
	sort.Slice(out, func(i, j int) bool {
		ti, e1 := time.Parse(time.RFC3339, out[i].TimeRFC3339)
		tj, e2 := time.Parse(time.RFC3339, out[j].TimeRFC3339)
		if e1 != nil || e2 != nil {
			return false
		}
		return tj.After(ti)
	})
	if limit > 0 && len(out) > limit {
		out = out[:limit]
	}
	return out
}

func (s *nmhcHistoryStore) DeleteRange(deviceID string, from, to time.Time) int {
	s.mu.Lock()
	defer s.mu.Unlock()
	src := s.byDevice[deviceID]
	if len(src) == 0 {
		return 0
	}
	out := make([]nmhcRecord, 0, len(src))
	deleted := 0
	for i := 0; i < len(src); i++ {
		r := src[i]
		t, err := time.Parse(time.RFC3339, r.TimeRFC3339)
		if err != nil {
			out = append(out, r)
			continue
		}
		if !t.Before(from) && !t.After(to) {
			deleted++
			continue
		}
		out = append(out, r)
	}
	s.byDevice[deviceID] = out
	s.rewriteLocked()
	return deleted
}

func (s *nmhcHistoryStore) rewriteLocked() {
	_ = os.MkdirAll(filepath.Dir(s.path), 0o755)
	tmp := s.path + ".tmp"
	f, err := os.OpenFile(tmp, os.O_CREATE|os.O_TRUNC|os.O_WRONLY, 0o644)
	if err != nil {
		return
	}
	for _, rs := range s.byDevice {
		for i := 0; i < len(rs); i++ {
			_, _ = f.Write(append(mustJSONLine(rs[i]), '\n'))
		}
	}
	_ = f.Close()
	_ = os.Rename(tmp, s.path)
}

func parseTimeAny(s string) (*time.Time, error) {
	s = strings.TrimSpace(s)
	if s == "" {
		return nil, nil
	}
	if t, err := time.Parse(time.RFC3339, s); err == nil {
		return &t, nil
	}
	if t, err := time.ParseInLocation("2006-01-02 15:04:05", s, time.Local); err == nil {
		tt := t.UTC()
		return &tt, nil
	}
	return nil, errors.New("invalid time")
}

func extractNMHC(res v1.Result) (thc, ch4, nmhc float64, ok bool) {
	var thcOK, ch4OK bool
	for i := 0; i < len(res.Pollutants); i++ {
		p := res.Pollutants[i]
		switch p.Code {
		case "THC":
			thc = p.Height
			thcOK = true
		case "CH4":
			ch4 = p.Height
			ch4OK = true
		}
	}
	if !thcOK || !ch4OK {
		return 0, 0, 0, false
	}
	return thc, ch4, thc - ch4, true
}

var nmhcStore = newNMHCHistoryStore(filepath.Join(".run", "results_nmch.jsonl"))

var pstore *persistStore

type uiState struct {
	DeviceID        string  `json:"deviceId"`
	ActiveTab       string  `json:"activeTab"`
	SelectedChannel int     `json:"selectedChannel"`
	FullMin         float64 `json:"fullMin"`
	YLow            float64 `json:"yLow"`
	YHigh           float64 `json:"yHigh"`
	AutoY           bool    `json:"autoY"`
	AcqMin          float64 `json:"acqMin"`
	Loop            bool    `json:"loop"`
	CycleMin        float64 `json:"cycleMin"`
	CycleMax        int     `json:"cycleMax"`
	EpcCarrier      int     `json:"epcCarrier"`
	EpcH2           int     `json:"epcH2"`
	EpcAir          int     `json:"epcAir"`
	UpdatedAt       string  `json:"updatedAt"`
}

var uiMu sync.Mutex
var uiByDevice = map[string]uiState{}
var uiLastDevice string

func defaultUIState(deviceID string) uiState {
	return uiState{DeviceID: deviceID, ActiveTab: "overview", SelectedChannel: 0, FullMin: 2, YLow: 0, YHigh: 40, AutoY: true, AcqMin: 2, Loop: true, CycleMin: 2, CycleMax: 9999, EpcCarrier: 0, EpcH2: 1, EpcAir: 2}
}

type resultEvent struct {
	Type     string    `json:"type"`
	DeviceID string    `json:"deviceId"`
	Channel  int       `json:"channel"`
	SessionToken string `json:"sessionToken"`
	At       time.Time `json:"at"`
	Result   v1.Result `json:"result"`
	Trace    v1.Trace  `json:"trace"`
	Method   v1.Method `json:"method"`
	Error    string    `json:"error,omitempty"`
}

type telemetryEvent struct {
	Type     string    `json:"type"`
	DeviceID string    `json:"deviceId"`
	At       time.Time `json:"at"`

	TempInj1 *float64 `json:"tempInj1,omitempty"`
	TempCol  *float64 `json:"tempCol,omitempty"`
	TempDet1 *float64 `json:"tempDet1,omitempty"`
	TempInj2 *float64 `json:"tempInj2,omitempty"`

	Epc []telemetryEpc `json:"epc,omitempty"`

	CarrierPsi  *float64 `json:"carrierPsi,omitempty"`
	CarrierSccm *float64 `json:"carrierSccm,omitempty"`
	H2Psi       *float64 `json:"h2Psi,omitempty"`
	H2Sccm      *float64 `json:"h2Sccm,omitempty"`
	AirPsi      *float64 `json:"airPsi,omitempty"`
	AirSccm     *float64 `json:"airSccm,omitempty"`
}

type telemetryEpc struct {
	Psi  float64 `json:"psi"`
	Sccm float64 `json:"sccm"`
}

func f64p(v float64) *float64 {
	return &v
}

func bcd2Temp1(data []byte, off int) (float64, bool) {
	if off < 0 || off+1 >= len(data) {
		return 0, false
	}
	b0 := data[off]
	neg := (b0 & 0xD0) == 0xD0
	if neg {
		b0 -= 0xD0
	}
	d1 := int((b0 >> 4) & 0x0F)
	d2 := int(b0 & 0x0F)
	d3 := int((data[off+1] >> 4) & 0x0F)
	d4 := int(data[off+1] & 0x0F)
	if d1 > 9 || d2 > 9 || d3 > 9 || d4 > 9 {
		return 0, false
	}
	v := float64(d1*100+d2*10+d3) + float64(d4)*0.1
	if neg {
		v = -v
	}
	return v, true
}

func u16BE(data []byte, off int) (uint16, bool) {
	if off < 0 || off+1 >= len(data) {
		return 0, false
	}
	return uint16(data[off])<<8 | uint16(data[off+1]), true
}

func parseTemps143(payload []byte) (telemetryEvent, bool) {
	if len(payload) < 12 {
		return telemetryEvent{}, false
	}
	inj1, ok0 := bcd2Temp1(payload, 0)
	col, ok1 := bcd2Temp1(payload, 2)
	det1, ok2 := bcd2Temp1(payload, 4)
	inj2, ok4 := bcd2Temp1(payload, 8)
	if !ok0 && !ok1 && !ok2 && !ok4 {
		return telemetryEvent{}, false
	}
	te := telemetryEvent{Type: "telemetry", At: time.Now().UTC()}
	if ok0 {
		te.TempInj1 = f64p(inj1)
	}
	if ok1 {
		te.TempCol = f64p(col)
	}
	if ok2 {
		te.TempDet1 = f64p(det1)
	}
	if ok4 {
		te.TempInj2 = f64p(inj2)
	}
	return te, true
}

type epcItem struct {
	ActualPsi  float64
	ActualSccm float64
}

func parseEpc159(payload []byte) ([]epcItem, bool) {
	if len(payload) < 1 {
		return nil, false
	}
	n := int(payload[0])
	idx := 1
	items := make([]epcItem, 0, n)
	for i := 0; i < n; i++ {
		if idx >= len(payload) {
			break
		}
		idx++
		u0, ok0 := u16BE(payload, idx)
		u1, ok1 := u16BE(payload, idx+2)
		u2, ok2 := u16BE(payload, idx+4)
		if !ok0 || !ok1 || !ok2 {
			break
		}
		_ = u0
		items = append(items, epcItem{ActualPsi: float64(u1) / 100.0, ActualSccm: float64(u2) / 100.0})
		idx += 6
		if idx >= len(payload) {
			break
		}
		idx++
		if idx >= len(payload) {
			break
		}
		idx++
	}
	if len(items) == 0 {
		return nil, false
	}
	return items, true
}

func main() {
	tcpPort := 25001
	tcpPort8000 := 8000
	httpPort := 8080
	allowControl := envBool("EDGE_ALLOW_CONTROL", false)

	hub := realtime.NewHub()
	states := &sync.Map{}
	cfg := chromsend143.Config{ShuaiJian1: 1, ShuaiJian2: 1, ShuaiJian3: 1}
	method := loadMethod()
	nmhcStore.Load()
	if ps, err := openPersistStore(filepath.Join(".run", "db")); err == nil {
		pstore = ps
		if v, ok := ps.LoadLastDeviceID(); ok {
			uiMu.Lock()
			uiLastDevice = v
			uiMu.Unlock()
		}
		startPersistence(states)
	} else {
		log.Printf("persist disabled: %v", err)
	}
	startEngineScheduler(hub, states, method)

	go runTCPForever(tcpPort, hub, states, cfg, method)
	go runTCPForever(tcpPort8000, hub, states, cfg, method)

	writePID()

	runHTTPForever(httpPort, hub, states, allowControl, method)
}

func writePID() {
	_ = os.MkdirAll(filepath.Join(".run"), 0o755)
	pidPath := filepath.Join(".run", "collector.pid")
	_ = os.WriteFile(pidPath, []byte(strconv.Itoa(os.Getpid())), 0o644)
}

func serveHTTP(port int, hub *realtime.Hub, states *sync.Map, allowControl bool, method v1.Method) error {
	mux := http.NewServeMux()
	mux.Handle("/events", hub)
	mux.HandleFunc("/api/v1/server", func(w http.ResponseWriter, r *http.Request) {
		if r.Method != http.MethodGet {
			http.Error(w, "method not allowed", http.StatusMethodNotAllowed)
			return
		}
		writeJSON(w, http.StatusOK, map[string]any{
			"pid":       os.Getpid(),
			"startedAt": startedAt.Format(time.RFC3339),
			"httpPort":  port,
			"tcpPorts":  []int{25001, 8000},
			"pidFile":   filepath.Join(".run", "collector.pid"),
		})
	})
	mux.HandleFunc("/api/v1/health", func(w http.ResponseWriter, r *http.Request) {
		if r.Method != http.MethodGet {
			http.Error(w, "method not allowed", http.StatusMethodNotAllowed)
			return
		}
		writeJSON(w, http.StatusOK, map[string]any{"ok": true, "startedAt": startedAt.Format(time.RFC3339)})
	})
	mux.HandleFunc("/api/v1/method", func(w http.ResponseWriter, r *http.Request) {
		if r.Method != http.MethodGet {
			http.Error(w, "method not allowed", http.StatusMethodNotAllowed)
			return
		}
		writeJSON(w, http.StatusOK, method)
	})
	mux.HandleFunc("/api/v1/ui", func(w http.ResponseWriter, r *http.Request) {
		switch r.Method {
		case http.MethodGet:
			deviceID := strings.TrimSpace(r.URL.Query().Get("deviceId"))
			if deviceID == "" {
				uiMu.Lock()
				last := uiLastDevice
				uiMu.Unlock()
				if last == "" && pstore != nil {
					if v, ok := pstore.LoadLastDeviceID(); ok {
						last = v
					}
				}
				writeJSON(w, http.StatusOK, map[string]any{"lastDeviceId": last})
				return
			}
			uiMu.Lock()
			st, ok := uiByDevice[deviceID]
			uiMu.Unlock()
			if !ok && pstore != nil {
				if v, ok2 := pstore.LoadUI(deviceID); ok2 {
					st = v
					ok = true
				}
			}
			if !ok {
				st = defaultUIState(deviceID)
			}
			writeJSON(w, http.StatusOK, st)
			return
		case http.MethodPost:
			var in uiState
			if json.NewDecoder(r.Body).Decode(&in) != nil {
				writeJSON(w, http.StatusBadRequest, map[string]any{"error": "invalid json"})
				return
			}
			in.DeviceID = strings.TrimSpace(in.DeviceID)
			if in.DeviceID == "" {
				writeJSON(w, http.StatusBadRequest, map[string]any{"error": "deviceId required"})
				return
			}
			in.ActiveTab = strings.TrimSpace(in.ActiveTab)
			if in.ActiveTab == "" {
				in.ActiveTab = "overview"
			}
			switch in.ActiveTab {
			case "overview", "curve", "result", "events", "logs", "settings":
			default:
				in.ActiveTab = "overview"
			}
			if in.SelectedChannel < 0 {
				in.SelectedChannel = 0
			}
			if in.SelectedChannel > 7 {
				in.SelectedChannel = 7
			}
			if in.FullMin <= 0 || !isFinite(in.FullMin) {
				in.FullMin = 2
			}
			if !isFinite(in.YLow) {
				in.YLow = 0
			}
			if !isFinite(in.YHigh) || in.YHigh <= in.YLow {
				in.YHigh = in.YLow + 1
			}
			if in.AcqMin < 0 || !isFinite(in.AcqMin) {
				in.AcqMin = 0
			}
			if in.CycleMin < 0 || !isFinite(in.CycleMin) {
				in.CycleMin = in.AcqMin
			}
			if in.CycleMax < 0 {
				in.CycleMax = 9999
			}
			in.UpdatedAt = time.Now().UTC().Format(time.RFC3339)
			uiMu.Lock()
			uiByDevice[in.DeviceID] = in
			uiLastDevice = in.DeviceID
			uiMu.Unlock()
			if pstore != nil {
				pstore.SaveUI(in)
			}
			writeJSON(w, http.StatusOK, map[string]any{"ok": true})
			return
		default:
			http.Error(w, "method not allowed", http.StatusMethodNotAllowed)
			return
		}
	})
	mux.HandleFunc("/api/v1/session", func(w http.ResponseWriter, r *http.Request) {
		if r.Method != http.MethodGet {
			http.Error(w, "method not allowed", http.StatusMethodNotAllowed)
			return
		}
		deviceID := strings.TrimSpace(r.URL.Query().Get("deviceId"))
		if deviceID == "" {
			writeJSON(w, http.StatusBadRequest, map[string]any{"error": "deviceId required"})
			return
		}
		ch := envIntFromQuery(r, "channel", 0)
		if ch < 0 || ch > 7 {
			writeJSON(w, http.StatusBadRequest, map[string]any{"error": "invalid channel"})
			return
		}
		stAny, ok := states.Load(deviceID)
		if !ok {
			if pstore != nil {
				if out, ok2 := pstore.LoadSession(deviceID, ch); ok2 {
					writeJSON(w, http.StatusOK, out)
					return
				}
			}
			writeJSON(w, http.StatusNotFound, map[string]any{"error": "device not found"})
			return
		}
		st := stAny.(*deviceState)
		st.mu.Lock()
		s := st.sessions[ch]
		if s == nil {
			st.mu.Unlock()
			if pstore != nil {
				if out, ok2 := pstore.LoadSession(deviceID, ch); ok2 {
					writeJSON(w, http.StatusOK, out)
					return
				}
			}
			writeJSON(w, http.StatusNotFound, map[string]any{"error": "session not found"})
			return
		}
		vals := append([]float64(nil), s.values...)
		if len(vals) > 200000 {
			vals = vals[len(vals)-200000:]
		}
		out := map[string]any{
			"deviceId":      deviceID,
			"channel":       ch,
			"sessionToken":  s.token,
			"active":        s.active,
			"startedAt":     s.startedAt.UTC().Format(time.RFC3339),
			"dtS":           s.dtS,
			"timeSpanS":     float64(len(vals)-1) * s.dtS,
			"values":        vals,
			"lastSample":    s.lastSample,
			"valuesCount":   len(vals),
			"totalCount":    len(s.values),
		}
		if st.lastResultByCh != nil {
			if lr, ok := st.lastResultByCh[ch]; ok && lr.token == s.token && lr.at.Unix() > 0 {
				out["resultAt"] = lr.at.UTC().Format(time.RFC3339)
				out["result"] = lr.res
			} else if pstore != nil {
				if rr, ok2 := pstore.LoadResult(deviceID, ch); ok2 {
					if tok, _ := rr["sessionToken"].(string); tok == s.token {
						out["resultAt"] = rr["at"]
						out["result"] = rr["result"]
					}
				}
			}
		} else if pstore != nil {
			if rr, ok2 := pstore.LoadResult(deviceID, ch); ok2 {
				if tok, _ := rr["sessionToken"].(string); tok == s.token {
					out["resultAt"] = rr["at"]
					out["result"] = rr["result"]
				}
			}
		}
		st.mu.Unlock()
		writeJSON(w, http.StatusOK, out)
	})
	mux.HandleFunc("/api/v1/session/active", func(w http.ResponseWriter, r *http.Request) {
		if r.Method != http.MethodGet {
			http.Error(w, "method not allowed", http.StatusMethodNotAllowed)
			return
		}
		deviceID := strings.TrimSpace(r.URL.Query().Get("deviceId"))
		if deviceID == "" {
			writeJSON(w, http.StatusBadRequest, map[string]any{"error": "deviceId required"})
			return
		}
		preferCh := envIntFromQuery(r, "channel", 0)
		if preferCh < 0 {
			preferCh = 0
		}
		if preferCh > 7 {
			preferCh = 7
		}
		stAny, ok := states.Load(deviceID)
		if !ok {
			if pstore != nil {
				if out, ok2 := pstore.LoadSession(deviceID, preferCh); ok2 {
					writeJSON(w, http.StatusOK, out)
					return
				}
			}
			writeJSON(w, http.StatusNotFound, map[string]any{"error": "device not found"})
			return
		}
		st := stAny.(*deviceState)
		pick := func(ch int) (map[string]any, bool) {
			s := st.sessions[ch]
			if s == nil || s.dtS <= 0 || len(s.values) < 2 {
				return nil, false
			}
			vals := append([]float64(nil), s.values...)
			if len(vals) > 200000 {
				vals = vals[len(vals)-200000:]
			}
			out := map[string]any{
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
			if st.lastResultByCh != nil {
				if lr, ok := st.lastResultByCh[ch]; ok && lr.token == s.token && lr.at.Unix() > 0 {
					out["resultAt"] = lr.at.UTC().Format(time.RFC3339)
					out["result"] = lr.res
				} else if pstore != nil {
					if rr, ok2 := pstore.LoadResult(deviceID, ch); ok2 {
						if tok, _ := rr["sessionToken"].(string); tok == s.token {
							out["resultAt"] = rr["at"]
							out["result"] = rr["result"]
						}
					}
				}
			} else if pstore != nil {
				if rr, ok2 := pstore.LoadResult(deviceID, ch); ok2 {
					if tok, _ := rr["sessionToken"].(string); tok == s.token {
						out["resultAt"] = rr["at"]
						out["result"] = rr["result"]
					}
				}
			}
			return out, true
		}
		st.mu.Lock()
		if out, ok := pick(preferCh); ok {
			st.mu.Unlock()
			writeJSON(w, http.StatusOK, out)
			return
		}
		for ch := 0; ch < 8; ch++ {
			if out, ok := pick(ch); ok {
				st.mu.Unlock()
				writeJSON(w, http.StatusOK, out)
				return
			}
		}
		st.mu.Unlock()
		if pstore != nil {
			if out, ok2 := pstore.LoadSession(deviceID, preferCh); ok2 {
				writeJSON(w, http.StatusOK, out)
				return
			}
		}
		writeJSON(w, http.StatusNotFound, map[string]any{"error": "session not found"})
	})
	mux.HandleFunc("/api/v1/results/nmhc", func(w http.ResponseWriter, r *http.Request) {
		deviceID := strings.TrimSpace(r.URL.Query().Get("deviceId"))
		if deviceID == "" {
			writeJSON(w, http.StatusBadRequest, map[string]any{"error": "deviceId required"})
			return
		}
		from, err := parseTimeAny(r.URL.Query().Get("from"))
		if err != nil {
			writeJSON(w, http.StatusBadRequest, map[string]any{"error": "invalid from"})
			return
		}
		to, err := parseTimeAny(r.URL.Query().Get("to"))
		if err != nil {
			writeJSON(w, http.StatusBadRequest, map[string]any{"error": "invalid to"})
			return
		}
		limit := envIntFromQuery(r, "limit", 2000)
		if limit < 0 {
			limit = 0
		}
		if limit > 5000 {
			limit = 5000
		}

		switch r.Method {
		case http.MethodGet:
			out := nmhcStore.Query(deviceID, from, to, limit)
			writeJSON(w, http.StatusOK, out)
			return
		case http.MethodDelete:
			if from == nil || to == nil {
				writeJSON(w, http.StatusBadRequest, map[string]any{"error": "from/to required"})
				return
			}
			deleted := nmhcStore.DeleteRange(deviceID, *from, *to)
			writeJSON(w, http.StatusOK, map[string]any{"ok": true, "deleted": deleted})
			return
		default:
			http.Error(w, "method not allowed", http.StatusMethodNotAllowed)
			return
		}
	})
	mux.HandleFunc("/api/v1/results/nmhc/export.csv", func(w http.ResponseWriter, r *http.Request) {
		if r.Method != http.MethodGet {
			http.Error(w, "method not allowed", http.StatusMethodNotAllowed)
			return
		}
		deviceID := strings.TrimSpace(r.URL.Query().Get("deviceId"))
		if deviceID == "" {
			http.Error(w, "deviceId required", http.StatusBadRequest)
			return
		}
		from, err := parseTimeAny(r.URL.Query().Get("from"))
		if err != nil {
			http.Error(w, "invalid from", http.StatusBadRequest)
			return
		}
		to, err := parseTimeAny(r.URL.Query().Get("to"))
		if err != nil {
			http.Error(w, "invalid to", http.StatusBadRequest)
			return
		}
		rs := nmhcStore.Query(deviceID, from, to, 5000)
		w.Header().Set("Content-Type", "text/csv; charset=utf-8")
		w.Header().Set("Content-Disposition", "attachment; filename=nmhc_"+deviceID+".csv")
		_, _ = io.WriteString(w, "time,THC,CH4,NMHC\n")
		for i := 0; i < len(rs); i++ {
			line := fmt.Sprintf("%s,%.6f,%.6f,%.6f\n", rs[i].TimeRFC3339, rs[i].THC, rs[i].CH4, rs[i].NMHC)
			_, _ = io.WriteString(w, line)
		}
	})
	mux.HandleFunc("/api/v1/devices", func(w http.ResponseWriter, r *http.Request) {
		if r.Method != http.MethodGet {
			http.Error(w, "method not allowed", http.StatusMethodNotAllowed)
			return
		}
		type dev struct {
			DeviceID   string            `json:"deviceId"`
			LastSeen   time.Time         `json:"lastSeen"`
			LastCmd    int               `json:"lastCmd"`
			CmdCounts  map[string]uint64 `json:"cmdCounts"`
			Last143    time.Time         `json:"last143"`
			Connected  bool              `json:"connected"`
			AllowCtrl  bool              `json:"allowControl"`
			CanStart22 bool              `json:"canStart22"`
		}
		out := make([]dev, 0)
		states.Range(func(key, value any) bool {
			id := key.(string)
			if strings.HasPrefix(id, "DEV") {
				return true
			}
			st := value.(*deviceState)
			st.mu.Lock()
			cc := map[string]uint64{}
			for k, v := range st.cmdCnt {
				cc[strconv.Itoa(int(k))] = v
			}
			connected := st.conn != nil
			lastSeen := st.lastSeen
			lastCmd := st.lastCmd
			last143 := st.last143
			st.mu.Unlock()
			out = append(out, dev{DeviceID: id, LastSeen: lastSeen, LastCmd: int(lastCmd), CmdCounts: cc, Last143: last143, Connected: connected, AllowCtrl: allowControl, CanStart22: allowControl && connected})
			return true
		})
		writeJSON(w, http.StatusOK, out)
	})
	mux.HandleFunc("/api/v1/devices/", func(w http.ResponseWriter, r *http.Request) {
		path := strings.TrimPrefix(r.URL.Path, "/api/v1/devices/")
		parts := strings.Split(path, "/")
		if len(parts) < 2 {
			http.NotFound(w, r)
			return
		}
		deviceID := parts[0]
		action := parts[1]
		if action != "cmd" && action != "localStart" && action != "localStop" && action != "localResult" {
			http.NotFound(w, r)
			return
		}
		if r.Method != http.MethodPost {
			http.Error(w, "method not allowed", http.StatusMethodNotAllowed)
			return
		}
		stAny, ok := states.Load(deviceID)
		if !ok {
			writeJSON(w, http.StatusNotFound, map[string]any{"error": "device not found"})
			return
		}
		if strings.HasPrefix(deviceID, "DEV") {
			writeJSON(w, http.StatusNotFound, map[string]any{"error": "device not found"})
			return
		}
		st := stAny.(*deviceState)
		switch action {
		case "cmd":
			if !allowControl {
				writeJSON(w, http.StatusForbidden, map[string]any{"error": "control disabled: set EDGE_ALLOW_CONTROL=1"})
				return
			}
			sub := r.URL.Query().Get("name")
			ch := envIntFromQuery(r, "channel", 0)
			cmd, payload, err := buildCmd(sub, ch)
			if err != nil {
				writeJSON(w, http.StatusBadRequest, map[string]any{"error": err.Error()})
				return
			}
			if err := sendCmd(st, deviceID, cmd, payload); err != nil {
				writeJSON(w, http.StatusBadRequest, map[string]any{"error": err.Error()})
				return
			}
			writeJSON(w, http.StatusOK, map[string]any{"ok": true, "cmd": cmd})
		case "localStart":
			ch := envIntFromQuery(r, "channel", 0)
			if ch < 0 || ch > 7 {
				writeJSON(w, http.StatusBadRequest, map[string]any{"error": "invalid channel"})
				return
			}
			force := strings.TrimSpace(strings.ToLower(r.URL.Query().Get("force")))
			if force != "1" && force != "true" && force != "yes" {
				st.mu.Lock()
				s := st.sessions[ch]
				active := s != nil && s.active
				st.mu.Unlock()
				if active {
					writeJSON(w, http.StatusOK, map[string]any{"ok": true, "skipped": true})
					return
				}
			}
			resetSession(st, ch)
			writeJSON(w, http.StatusOK, map[string]any{"ok": true})
		case "localStop":
			ch := envIntFromQuery(r, "channel", 0)
			if ch < 0 || ch > 7 {
				writeJSON(w, http.StatusBadRequest, map[string]any{"error": "invalid channel"})
				return
			}
			st.mu.Lock()
			s := st.sessions[ch]
			active := s != nil && s.active
			st.mu.Unlock()
			if !active {
				writeJSON(w, http.StatusOK, map[string]any{"ok": true, "skipped": true})
				return
			}
			if allowControl {
				channelMask := byte(1 << uint(ch))
				_ = sendCmd(st, deviceID, 245, []byte{channelMask})
				time.Sleep(100 * time.Millisecond)
			}
			ok, msg := finalizeSession(hub, st, deviceID, ch, method)
			if !ok {
				writeJSON(w, http.StatusConflict, map[string]any{"error": msg})
				return
			}
			writeJSON(w, http.StatusOK, map[string]any{"ok": true})
		case "localResult":
			ch := envIntFromQuery(r, "channel", 0)
			if ch < 0 || ch > 7 {
				writeJSON(w, http.StatusBadRequest, map[string]any{"error": "invalid channel"})
				return
			}
			st.mu.Lock()
			s := st.sessions[ch]
			active := s != nil && s.active
			st.mu.Unlock()
			if !active {
				writeJSON(w, http.StatusOK, map[string]any{"ok": true, "skipped": true})
				return
			}
			ok, msg := publishSessionResultSnapshot(hub, st, deviceID, ch, method)
			if !ok {
				writeJSON(w, http.StatusConflict, map[string]any{"error": msg})
				return
			}
			writeJSON(w, http.StatusOK, map[string]any{"ok": true})
		}
	})
	mux.HandleFunc("/", func(w http.ResponseWriter, r *http.Request) {
		w.Header().Set("Content-Type", "text/html; charset=utf-8")
		_, _ = w.Write([]byte(indexHTML))
	})
	host := strings.TrimSpace(os.Getenv("EDGE_HTTP_BIND"))
	if host == "" {
		host = "127.0.0.1"
	}
	addr := host + ":" + strconv.Itoa(port)
	log.Printf("collector http listening on %s", addr)
	return http.ListenAndServe(addr, mux)
}

func serveTCP(port int, hub *realtime.Hub, states *sync.Map, cfg chromsend143.Config, method v1.Method) error {
	ln, err := net.Listen("tcp", fmt.Sprintf("0.0.0.0:%d", port))
	if err != nil {
		return fmt.Errorf("tcp listen %d failed: %w", port, err)
	}
	log.Printf("collector tcp listening on 0.0.0.0:%d", port)
	for {
		c, err := ln.Accept()
		if err != nil {
			continue
		}
		go handleConn(c, hub, states, cfg, method)
	}
}

func handleConn(c net.Conn, hub *realtime.Hub, states *sync.Map, cfg chromsend143.Config, method v1.Method) {
	defer c.Close()
	dec := &gckc.StreamDecoder{}
	buf := make([]byte, 64*1024)

	for {
		n, err := c.Read(buf)
		if n > 0 {
			dec.Push(buf[:n])
			for {
				f, ok, derr := dec.Next()
				if derr != nil {
					break
				}
				if !ok {
					break
				}
				processFrame(c, f, hub, states, cfg, method)
			}
		}
		if err != nil {
			if err == io.EOF {
				return
			}
			return
		}
	}
}

func processFrame(c net.Conn, f gckc.Frame, hub *realtime.Hub, states *sync.Map, cfg chromsend143.Config, method v1.Method) {
	if strings.HasPrefix(f.DeviceID, "DEV") {
		return
	}
	st := getState(states, f.DeviceID)
	st.mu.Lock()
	st.lastSeen = time.Now()
	st.lastCmd = f.Cmd
	if st.cmdCnt == nil {
		st.cmdCnt = map[byte]uint64{}
	}
	st.cmdCnt[f.Cmd]++
	st.conn = c
	if st.sessions == nil {
		st.sessions = map[int]*runSession{}
	}
	st.mu.Unlock()

	hub.Publish(f.DeviceID, event{Type: "device", DeviceID: f.DeviceID, At: time.Now()})

	switch f.Cmd {
	case 146:
		resetAllSessions(st)
	case 150:
		// stop/complete ack: do not reset session here; the next start ack (146) defines a new session
	case 147:
			finalizeAllSessions(hub, st, f.DeviceID, method)
	case 151:
		if len(f.Payload) > 0 {
			ch := int(f.Payload[0])
			finalizeSession(hub, st, f.DeviceID, ch, method)
		}
	case 159:
		if items, ok := parseEpc159(f.Payload); ok {
			e := telemetryEvent{Type: "telemetry", DeviceID: f.DeviceID, At: time.Now().UTC()}
			epc := make([]telemetryEpc, 0, len(items))
			for i := 0; i < len(items) && i < 32; i++ {
				epc = append(epc, telemetryEpc{Psi: items[i].ActualPsi, Sccm: items[i].ActualSccm})
			}
			e.Epc = epc
			if len(items) > 0 {
				e.CarrierPsi = f64p(items[0].ActualPsi)
				e.CarrierSccm = f64p(items[0].ActualSccm)
			}
			if len(items) > 1 {
				e.H2Psi = f64p(items[1].ActualPsi)
				e.H2Sccm = f64p(items[1].ActualSccm)
			}
			if len(items) > 2 {
				e.AirPsi = f64p(items[2].ActualPsi)
				e.AirSccm = f64p(items[2].ActualSccm)
			}
			hub.Publish(f.DeviceID, e)
		}
	}

	if f.Cmd != 143 {
		return
	}
	if te, ok := parseTemps143(f.Payload); ok {
		te.DeviceID = f.DeviceID
		hub.Publish(f.DeviceID, te)
	}
	parsedAll, has, err := chromsend143.ParseAll(f.Payload, cfg)
	if err != nil || !has || len(parsedAll) == 0 {
		return
	}

	for _, parsed := range parsedAll {
		dtS := 1.0 / float64(parsed.Freq10)
		st.mu.Lock()
		if st.lastTS == nil {
			st.lastTS = map[int]float64{}
		}
		t0 := st.lastTS[parsed.Channel]
		st.lastTS[parsed.Channel] = t0 + float64(len(parsed.Values))*dtS
		st.last143 = time.Now()
		tok, _ := appendSessionSamplesLocked(st, parsed.Channel, dtS, t0, parsed.Values)
		st.mu.Unlock()
		hub.Publish(f.DeviceID, event{Type: "samples", DeviceID: f.DeviceID, At: time.Now(), Channel: parsed.Channel, SessionToken: tok, DTs: dtS, T0s: t0, Values: parsed.Values})
	}
}

func resetAllSessions(st *deviceState) {
	st.mu.Lock()
	defer st.mu.Unlock()
	st.lastTS = map[int]float64{}
	if st.sessions == nil {
		st.sessions = map[int]*runSession{}
	}
	for ch := range st.sessions {
		st.sessions[ch] = newRunSession()
	}
}

func resetSession(st *deviceState, ch int) {
	st.mu.Lock()
	defer st.mu.Unlock()
	if st.lastTS == nil {
		st.lastTS = map[int]float64{}
	}
	st.lastTS[ch] = 0
	if st.sessions == nil {
		st.sessions = map[int]*runSession{}
	}
	st.sessions[ch] = newRunSession()
}

func appendSessionSamplesLocked(st *deviceState, ch int, dtS float64, t0 float64, vals []float64) (string, bool) {
	s, ok := st.sessions[ch]
	if !ok || s == nil {
		s = newRunSession()
		st.sessions[ch] = s
	}
	if !s.active {
		return s.token, false
	}
	if s.dtS == 0 {
		s.dtS = dtS
	} else if mathAbs(s.dtS-dtS) > 1e-6 {
		s.dtS = dtS
		s.values = nil
		s.snapshotDone = false
	}
	idx0 := int(t0 / s.dtS)
	if idx0 < 0 {
		idx0 = 0
	}
	need := idx0 + len(vals)
	if len(s.values) < need {
		last := s.lastSample
		if len(s.values) > 0 {
			last = s.values[len(s.values)-1]
		}
		for len(s.values) < need {
			s.values = append(s.values, last)
		}
	}
	for i := 0; i < len(vals); i++ {
		s.values[idx0+i] = vals[i]
		s.lastSample = vals[i]
	}
	return s.token, true
}

func finalizeAllSessions(hub *realtime.Hub, st *deviceState, deviceID string, method v1.Method) {
	st.mu.Lock()
	chs := make([]int, 0, len(st.sessions))
	for ch := range st.sessions {
		chs = append(chs, ch)
	}
	st.mu.Unlock()
	for _, ch := range chs {
		finalizeSession(hub, st, deviceID, ch, method)
	}
}

func finalizeSession(hub *realtime.Hub, st *deviceState, deviceID string, ch int, method v1.Method) (bool, string) {
	st.mu.Lock()
	s, ok := st.sessions[ch]
	if !ok || s == nil || !s.active || s.dtS <= 0 || len(s.values) < 2 {
		if ok && s != nil {
			s.active = false
		}
		st.mu.Unlock()
		return false, "no active session"
	}
	trace := v1.Trace{
		Schema:    "voc-trace.v1",
		TraceID:   fmt.Sprintf("%s-%d-%d", deviceID, ch, time.Now().UnixNano()),
		DeviceID:  deviceID,
		StationID: deviceID,
		DataTime:  time.Now().UTC().Format(time.RFC3339),
		DtS:       s.dtS,
		TimeSpanS: float64(len(s.values)-1) * s.dtS,
		Unit:      "pA",
		Values:    append([]float64(nil), s.values...),
	}
	tok := s.token
	s.active = false
	st.mu.Unlock()

	res, err := analyzer.Analyze(trace, method, "dev", time.Now())
	e := resultEvent{Type: "result", DeviceID: deviceID, Channel: ch, SessionToken: tok, At: time.Now(), Trace: trace, Method: method}
	if err != nil {
		e.Error = err.Error()
	} else {
		e.Result = res
		st.mu.Lock()
		if st.lastResultByCh == nil {
			st.lastResultByCh = map[int]lastResult{}
		}
		st.lastResultByCh[ch] = lastResult{token: tok, at: e.At.UTC(), res: res}
		st.mu.Unlock()
		if pstore != nil {
			pstore.SaveResult(deviceID, ch, map[string]any{"deviceId": deviceID, "channel": ch, "sessionToken": tok, "at": e.At.UTC().Format(time.RFC3339), "result": res})
		}
		if thc, ch4, nmhc, ok := extractNMHC(res); ok {
			nmhcStore.Add(nmhcRecord{
				TimeRFC3339: e.At.UTC().Format(time.RFC3339),
				DeviceID:    deviceID,
				TraceID:     trace.TraceID,
				THC:         thc,
				CH4:         ch4,
				NMHC:        nmhc,
			})
		}
	}
	hub.Publish(deviceID, e)
	return true, e.Error
}

func publishSessionResultSnapshot(hub *realtime.Hub, st *deviceState, deviceID string, ch int, method v1.Method) (bool, string) {
	st.mu.Lock()
	s, ok := st.sessions[ch]
	if !ok || s == nil || !s.active || s.dtS <= 0 || len(s.values) < 2 {
		st.mu.Unlock()
		return false, "no active session"
	}
	snap := sessionSnapshot{DtS: s.dtS, Values: append([]float64(nil), s.values...)}
	tok := s.token
	s.snapshotDone = true
	st.mu.Unlock()

	trace := v1.Trace{
		Schema:    "voc-trace.v1",
		TraceID:   fmt.Sprintf("%s-%d-snap-%d", deviceID, ch, time.Now().UnixNano()),
		DeviceID:  deviceID,
		StationID: deviceID,
		DataTime:  time.Now().UTC().Format(time.RFC3339),
		DtS:       snap.DtS,
		TimeSpanS: float64(len(snap.Values)-1) * snap.DtS,
		Unit:      "pA",
		Values:    snap.Values,
	}

	e := resultEvent{Type: "result", DeviceID: deviceID, Channel: ch, SessionToken: tok, At: time.Now().UTC(), Trace: trace, Method: method}
	res, err := analyzer.Analyze(trace, method, deviceID, time.Now())
	if err != nil {
		e.Error = err.Error()
	} else {
		e.Result = res
		st.mu.Lock()
		if st.lastResultByCh == nil {
			st.lastResultByCh = map[int]lastResult{}
		}
		st.lastResultByCh[ch] = lastResult{token: tok, at: e.At.UTC(), res: res}
		st.mu.Unlock()
		if pstore != nil {
			pstore.SaveResult(deviceID, ch, map[string]any{"deviceId": deviceID, "channel": ch, "sessionToken": tok, "at": e.At.UTC().Format(time.RFC3339), "result": res})
		}
		if thc, ch4, nmhc, ok := extractNMHC(res); ok {
			nmhcStore.Add(nmhcRecord{
				TimeRFC3339: e.At.UTC().Format(time.RFC3339),
				DeviceID:    deviceID,
				TraceID:     trace.TraceID,
				THC:         thc,
				CH4:         ch4,
				NMHC:        nmhc,
			})
		}
	}
	hub.Publish(deviceID, e)
	return true, e.Error
}

func mathAbs(v float64) float64 {
	if v < 0 {
		return -v
	}
	return v
}

func isFinite(v float64) bool {
	return !math.IsNaN(v) && !math.IsInf(v, 0)
}

func loadMethod() v1.Method {
	path := filepath.Join(".run", "method.json")
	b, err := os.ReadFile(path)
	if err == nil {
		var m v1.Method
		if json.Unmarshal(b, &m) == nil && m.Schema != "" {
			return m
		}
	}
	return v1.Method{
		Schema:   "voc-method.v1",
		MethodID: "default",
		Version:  1,
		Pollutants: []v1.PollutantSpec{
			{Code: "THC", Name: "总烃", StartS: 0, EndS: 20, PaddingS: 2, Threshold: 0},
			{Code: "CH4", Name: "甲烷", StartS: 20, EndS: 80, PaddingS: 2, Threshold: 0},
		},
	}
}

func getState(states *sync.Map, deviceID string) *deviceState {
	v, ok := states.Load(deviceID)
	if ok {
		return v.(*deviceState)
	}
	st := &deviceState{lastTS: map[int]float64{}, lastResultByCh: map[int]lastResult{}}
	states.Store(deviceID, st)
	return st
}

func envInt(name string, def int) int {
	v := os.Getenv(name)
	if v == "" {
		return def
	}
	n, err := strconv.Atoi(v)
	if err != nil {
		return def
	}
	return n
}

func envBool(name string, def bool) bool {
	v := os.Getenv(name)
	if v == "" {
		return def
	}
	v = strings.TrimSpace(strings.ToLower(v))
	return v == "1" || v == "true" || v == "yes" || v == "on"
}

func writeJSON(w http.ResponseWriter, status int, v any) {
	w.Header().Set("Content-Type", "application/json")
	w.WriteHeader(status)
	_ = json.NewEncoder(w).Encode(v)
}

func envIntFromQuery(r *http.Request, key string, def int) int {
	v := r.URL.Query().Get(key)
	if v == "" {
		return def
	}
	n, err := strconv.Atoi(v)
	if err != nil {
		return def
	}
	return n
}

func buildCmd(name string, channel int) (byte, []byte, error) {
	switch name {
	case "start":
		return 22, []byte{byte(channel)}, nil
	case "stop":
		return 23, []byte{byte(channel)}, nil
	case "startAll":
		return 18, nil, nil
	case "stopAll":
		return 19, nil, nil
	case "tempOn":
		return 16, nil, nil
	case "tempOff":
		return 17, nil, nil
	default:
		return 0, nil, fmt.Errorf("unknown cmd name: %s", name)
	}
}

func sendCmd(st *deviceState, deviceID string, cmd byte, payload []byte) error {
	st.mu.Lock()
	conn := st.conn
	st.mu.Unlock()
	if conn == nil {
		return fmt.Errorf("device %s not connected", deviceID)
	}
	seq := uint16(atomic.AddUint32(&st.seq, 1) & 0xFFFF)
	frame, err := gckc.Encode(gckc.Frame{DeviceID: deviceID, Seq: seq, Cmd: cmd, Payload: payload})
	if err != nil {
		return err
	}
	_, err = conn.Write(frame)
	return err
}

var indexHTML = `<!doctype html>
<html lang="zh-CN">
<head>
  <meta charset="utf-8" />
  <meta name="viewport" content="width=device-width,initial-scale=1" />
  <title>Edge Collector</title>
  <style>
    :root{--bg:#F3F6FB;--card:#FFFFFF;--stroke:#D9E2EF;--grid:#E6EEF8;--shadow:0 2px 10px rgba(15,39,71,0.08);--text:#1F2A44;--muted:#5B6B84;--primary:#2B6DFF;--dark:#3B3B3B;--dark2:#2F2F2F;--ok:#3AC268;--warn:#FF4D4F;--blueCard:#6FB6FF;--blueCard2:#56A6FF}
    *{box-sizing:border-box}
    html,body{height:100%}
    body{font-family:system-ui,"Segoe UI",Arial;margin:0;background:var(--bg);color:var(--text)}
    .mono{font-variant-numeric:tabular-nums}
    .shell{min-height:100%;display:flex;flex-direction:column}
    .topbar{background:linear-gradient(180deg,var(--dark),var(--dark2));color:#fff;display:flex;align-items:center;gap:18px;padding:10px 16px}
    .brand{font-size:26px;letter-spacing:1px;white-space:nowrap;margin-right:8px}
    .tabs{display:flex;gap:10px;align-items:center;flex:1}
    .tab{appearance:none;border:none;background:transparent;color:#fff;display:flex;align-items:center;gap:8px;padding:8px 12px;border-radius:10px;cursor:pointer;opacity:0.9}
    .tab:hover{background:rgba(255,255,255,0.08);opacity:1}
    .tab.active{background:rgba(255,255,255,0.14);opacity:1}
    .tabIcon{width:28px;height:28px;border-radius:999px;background:rgba(255,255,255,0.15);display:flex;align-items:center;justify-content:center;font-size:14px;position:relative;flex:none}
    .tab.active .tabIcon::after{content:"";position:absolute;bottom:-6px;left:50%;transform:translateX(-50%);width:10px;height:10px;border-radius:999px;background:rgba(144,238,144,0.9);box-shadow:0 0 0 2px rgba(0,0,0,0.25)}
    .tabText{font-size:13px;white-space:nowrap}
    .flame{width:46px;height:46px;border-radius:14px;background:#fff;display:flex;align-items:center;justify-content:center;box-shadow:var(--shadow);border:1px solid var(--stroke)}
    .flameInner{width:22px;height:22px;background:var(--warn);border-radius:14px 14px 14px 0;transform:rotate(45deg)}
    .main{padding:14px 16px;flex:1}
    .view{display:none}
    .view.active{display:block}
    .card{background:var(--card);border:1px solid var(--stroke);border-radius:10px;box-shadow:var(--shadow)}
    .cardPad{padding:12px}
    .row{display:flex;gap:12px;align-items:center;flex-wrap:wrap}
    .spacer{flex:1}
    .btn{appearance:none;border:1px solid var(--stroke);background:#fff;color:var(--text);border-radius:8px;padding:8px 12px;cursor:pointer}
    .btn:hover{background:rgba(43,109,255,0.06);border-color:rgba(43,109,255,0.35)}
    .btn.dark{background:#444;color:#fff;border-color:#444}
    .btn.primary{background:var(--primary);border-color:var(--primary);color:#fff}
    .btn:disabled{opacity:0.45;cursor:not-allowed}
    .input,.select{border:1px solid var(--stroke);border-radius:8px;padding:8px 10px;background:#fff;color:var(--text);outline:none}
    .label{font-size:12px;color:var(--muted)}
    .dot{width:10px;height:10px;border-radius:999px;display:inline-block}
    .modeItem{display:flex;gap:6px;align-items:center;color:var(--text);font-size:13px}
    .modeItem input{accent-color:var(--primary)}

    #row{display:flex;gap:16px;align-items:center;flex-wrap:wrap}
    #status{padding:6px 10px;border:1px solid var(--stroke);border-radius:6px;background:#fff}
    #panel{display:grid;grid-template-columns:1fr 380px;gap:12px;align-items:start}
    #chartWrap{min-width:720px}
    @media (max-width:1200px){#panel{grid-template-columns:1fr}#chartWrap{min-width:unset}}
    canvas{border:1px solid var(--stroke);border-radius:8px;background:#fff;width:100%;height:440px;display:block}
    #right{border:1px solid var(--stroke);border-radius:10px;overflow:hidden;background:#fff;box-shadow:var(--shadow)}
    #tblTitle{background:rgba(31,42,68,0.92);color:#fff;padding:10px 12px;font-size:12px}
    table{width:100%;border-collapse:collapse}
    th{background:rgba(31,42,68,0.92);color:#fff;font-weight:600;text-align:left;padding:10px 12px;font-size:12px}
    td{padding:10px 12px;border-top:1px solid var(--grid);font-size:12px}

    .homeGrid{display:grid;grid-template-columns:1fr 1fr;gap:12px;max-width:560px}
    .blueCard{background:linear-gradient(180deg,var(--blueCard),var(--blueCard2));border:1px solid rgba(255,255,255,0.65);border-radius:6px;min-height:118px;display:flex;align-items:center;justify-content:center;flex-direction:column;gap:10px;color:#0b1b2f}
    .blueTitle{font-size:18px;opacity:0.92}
    .blueValue{font-size:28px;font-weight:700;letter-spacing:0.5px}
    .bottomBar{margin-top:12px;display:grid;grid-template-columns:1fr 60px 300px;gap:12px;align-items:end;max-width:980px}
    .statusStrip{background:linear-gradient(180deg,#87C2FF,#74B9FF);border-radius:6px;padding:8px 12px;color:#0b1b2f}
    .ctrlStrip{display:flex;gap:10px;align-items:center;flex-wrap:wrap}
    .ctrlBtn{background:#E9EEF5;border:1px solid #D0D7E2;border-radius:6px;padding:8px 12px;color:#20314f}
    .ctrlVal{background:#87C2FF;border:1px solid rgba(255,255,255,0.6);border-radius:6px;padding:8px 12px;color:#0b1b2f;min-width:100px;text-align:center}
    .ctrlAction{background:#2B6DFF;border:1px solid rgba(0,0,0,0.05);border-radius:6px;padding:8px 16px;color:#fff}
    .clock{font-family:ui-monospace, SFMono-Regular, Menlo, Consolas, "Liberation Mono", monospace;background:#fff;border:1px solid #000;border-radius:2px;padding:10px 12px;font-size:28px;letter-spacing:1px;text-align:center}
    @media (max-width:1200px){.bottomBar{grid-template-columns:1fr}}
  </style>
</head>
<body>
  <div class="shell">
    <header class="topbar">
      <div class="brand">在线监测</div>
      <nav class="tabs" id="tabs">
        <button class="tab active" data-tab="overview"><span class="tabIcon">概</span><span class="tabText">概览</span></button>
        <button class="tab" data-tab="curve"><span class="tabIcon">曲</span><span class="tabText">曲线</span></button>
        <button class="tab" data-tab="result"><span class="tabIcon">果</span><span class="tabText">结果</span></button>
        <button class="tab" data-tab="events"><span class="tabIcon">事</span><span class="tabText">事件</span></button>
        <button class="tab" data-tab="logs"><span class="tabIcon">志</span><span class="tabText">日志</span></button>
        <button class="tab" data-tab="settings"><span class="tabIcon">设</span><span class="tabText">设置</span></button>
      </nav>
      <div class="flame" title="告警"><div class="flameInner"></div></div>
    </header>

    <main class="main">
      <section id="view-overview" class="view active">
        <div class="card cardPad" style="max-width:980px">
          <div class="homeGrid">
            <div class="blueCard"><div class="blueTitle">总烃</div><div class="blueValue mono" id="kpi-thc">-</div></div>
            <div class="blueCard"><div class="blueTitle" style="opacity:0.0">占位</div><div class="blueValue mono" id="kpi-thc2"> </div></div>
            <div class="blueCard"><div class="blueTitle">甲烷</div><div class="blueValue mono" id="kpi-ch4">-</div></div>
            <div class="blueCard"><div class="blueTitle" style="opacity:0.0">占位</div><div class="blueValue mono" id="kpi-ch4b"> </div></div>
            <div class="blueCard"><div class="blueTitle">非甲烷总烃</div><div class="blueValue mono" id="kpi-nmhc">-</div></div>
            <div class="blueCard"><div class="blueTitle" style="opacity:0.0">占位</div><div class="blueValue mono" id="kpi-nmhc2"> </div></div>
          </div>

          <div class="bottomBar">
            <div>
              <div class="statusStrip mono" id="home-status">时间: 0.000 min   信号: 0.000 pA</div>
              <div style="margin-top:10px" class="ctrlStrip">
                <button class="ctrlBtn">运行次数</button>
                <div class="ctrlVal mono" id="home-runCountVal">1720</div>
                <button class="ctrlBtn">单位</button>
                <div class="ctrlVal mono" id="home-unitVal">mg/m³</div>
                <button class="ctrlAction" id="home-inject">进样</button>
              </div>
            </div>
            <div class="flame" title="状态"><div class="flameInner"></div></div>
            <div class="clock mono" id="home-clock">0000-00-00 00:00:00</div>
          </div>
        </div>
        <div class="card cardPad" style="max-width:980px;margin-top:12px">
          <div id="tblTitle">设备列表</div>
          <table>
            <thead><tr><th>设备</th><th>在线</th><th>lastSeen</th><th>143</th><th>last143</th></tr></thead>
            <tbody id="overview-devices"><tr><td class="mono" colspan="5" style="color:var(--muted)">等待 GC...</td></tr></tbody>
          </table>
        </div>
      </section>

      <section id="view-curve" class="view">
        <div class="card cardPad" style="max-width:1240px">
          <div class="row" style="margin-bottom:10px">
            <button class="btn dark">通道1结束</button>
            <label class="modeItem"><span class="dot" style="background:var(--ok)"></span><input type="radio" name="mode" checked /> 正常进样</label>
            <label class="modeItem"><span class="dot" style="background:#B7C0CF"></span><input type="radio" name="mode" /> 零气反标</label>
            <label class="modeItem"><span class="dot" style="background:#B7C0CF"></span><input type="radio" name="mode" /> 标气反标</label>
            <div class="spacer"></div>
            <span class="label">下限:</span><input id="ylow" class="input mono" style="width:90px" value="0" />
            <span class="label">上限:</span><input id="yhigh" class="input mono" style="width:90px" value="40" />
            <span class="label">采集时间:</span><input id="acqmin" class="input mono" style="width:70px" value="2" />
            <input class="input" style="width:48px" value="0" />
            <span class="label">满屏时间</span><input id="fullmin" class="input mono" style="width:70px" value="2" />
          </div>

          <div class="row" style="margin-bottom:10px">
            <div id="stat" class="mono">通道1: 0.000 min  0.000 pA  信号1:</div>
            <label class="modeItem"><input id="autoy" type="checkbox" checked /> 峰高自适应</label>
            <label class="modeItem"><input id="loop" type="checkbox" checked /> 自动出数</label>
            <input id="name" class="input" placeholder="谱图名称" style="width:260px" />
            <div class="spacer"></div>
            <div class="kpi"><div class="label">在线</div><div id="status" class="mono">未连接</div></div>
            <div class="kpi"><div class="label">设备</div><select id="device" class="select mono"><option value="">等待 GC...</option></select></div>
            <div class="kpi"><div class="label">Channel</div><select id="chn" class="select mono"><option value="0">0</option><option value="1">1</option><option value="2">2</option><option value="3">3</option></select></div>
            <button class="btn primary" id="start">开始</button>
            <button class="btn" id="stop">停止</button>
            <button class="btn" id="clear">清屏</button>
          </div>

          <div id="panel">
            <div id="chartWrap" class="card" style="padding:10px">
              <canvas id="cv" width="1200" height="440"></canvas>
            </div>
            <div>
              <div id="right">
                <div id="tblTitle">名称 | 含量(mg/m³)</div>
                <table>
                  <thead><tr><th>名称</th><th>含量(mg/m³)</th></tr></thead>
                  <tbody id="tbody">
                    <tr><td>总烃</td><td class="mono">-</td></tr>
                    <tr><td>甲烷</td><td class="mono">-</td></tr>
                    <tr><td>非甲烷总烃</td><td class="mono">-</td></tr>
                  </tbody>
                </table>
              </div>
              <div style="display:grid;grid-template-columns:1fr 1fr;gap:12px;margin-top:12px">
                <div class="card" style="border-radius:10px;overflow:hidden">
                  <div id="tblTitle">实测</div>
                  <table>
                    <tbody>
                      <tr><td>载气</td><td class="mono" id="gas-carrier">-</td></tr>
                      <tr><td>氢气</td><td class="mono" id="gas-h2">-</td></tr>
                      <tr><td>空气</td><td class="mono" id="gas-air">-</td></tr>
                    </tbody>
                  </table>
                </div>
                <div class="card" style="border-radius:10px;overflow:hidden">
                  <div id="tblTitle">实测℃</div>
                  <table>
                    <tbody>
                      <tr><td>柱箱</td><td class="mono" id="temp-col">-</td></tr>
                      <tr><td>阀温</td><td class="mono" id="temp-inj1">-</td></tr>
                      <tr><td>检测1</td><td class="mono" id="temp-det1">-</td></tr>
                      <tr><td>进样2</td><td class="mono" id="temp-inj2">-</td></tr>
                    </tbody>
                  </table>
                </div>
              </div>
              <div class="flame" style="margin-top:12px" title="状态"><div class="flameInner"></div></div>
              <div id="dbg" class="mono" style="margin-top:10px;color:var(--muted)"></div>
            </div>
          </div>
        </div>
      </section>

      <section id="view-result" class="view">
        <div class="card cardPad" style="max-width:1240px">
          <div class="row" style="margin-bottom:10px">
            <div class="label">NMHC 结果历史（总烃/甲烷/非甲烷总烃）</div>
            <div class="spacer"></div>
            <span class="label">开始</span><input id="res-from" class="input mono" style="width:220px" placeholder="YYYY-MM-DD HH:mm:ss" />
            <span class="label">结束</span><input id="res-to" class="input mono" style="width:220px" placeholder="YYYY-MM-DD HH:mm:ss" />
            <button class="btn dark" id="res-export">导出CSV</button>
            <button class="btn dark" id="res-delete">删除时间段</button>
          </div>
          <div class="card" style="border-radius:10px;overflow:hidden">
            <div id="tblTitle">记录报表</div>
            <table>
              <thead><tr><th>时间</th><th>总烃</th><th>甲烷</th><th>非甲烷总烃</th></tr></thead>
              <tbody id="res-tbody"><tr><td class="mono" colspan="4" style="color:var(--muted)">暂无数据</td></tr></tbody>
            </table>
          </div>
        </div>
      </section>

      <section id="view-events" class="view">
        <div class="card cardPad" style="max-width:1240px">
          <div class="row" style="margin-bottom:10px">
            <label class="modeItem"><input id="evt-only-selected" type="checkbox" checked /> 仅当前设备</label>
            <div class="spacer"></div>
            <button class="btn dark" id="evt-clear">清空</button>
          </div>
          <div class="card" style="border-radius:10px;overflow:hidden">
            <div id="tblTitle">事件流</div>
            <table>
              <thead><tr><th>时间</th><th>设备</th><th>类型</th><th>摘要</th></tr></thead>
              <tbody id="evt-tbody"><tr><td class="mono" colspan="4" style="color:var(--muted)">暂无数据</td></tr></tbody>
            </table>
          </div>
        </div>
      </section>

      <section id="view-logs" class="view">
        <div class="card cardPad" style="max-width:1240px">
          <div id="tblTitle">调试日志</div>
          <pre id="logs-pre" class="mono" style="margin:0;padding:12px;white-space:pre-wrap"></pre>
        </div>
      </section>

      <section id="view-settings" class="view">
        <div class="card cardPad" style="max-width:980px">
          <div id="tblTitle">设置</div>
          <div class="row" style="margin-top:12px">
            <div><div class="label">默认满屏时间(min)</div><input id="set-fullmin" class="input mono" style="width:120px" value="2" /></div>
            <div><div class="label">默认下限</div><input id="set-ylow" class="input mono" style="width:120px" value="0" /></div>
            <div><div class="label">默认上限</div><input id="set-yhigh" class="input mono" style="width:120px" value="40" /></div>
            <div><div class="label">默认峰高自适应</div><label class="modeItem"><input id="set-autoy" type="checkbox" checked /> 启用</label></div>
            <div><div class="label">默认采集时间(min)</div><input id="set-acqmin" class="input mono" style="width:120px" value="2" /></div>
            <div class="spacer"></div>
            <button class="btn primary" id="set-save">保存</button>
          </div>
          <div class="row" style="margin-top:12px">
            <div><div class="label">载气 EPC idx</div><select id="set-epc-carrier" class="select mono" style="width:120px"></select></div>
            <div><div class="label">氢气 EPC idx</div><select id="set-epc-h2" class="select mono" style="width:120px"></select></div>
            <div><div class="label">空气 EPC idx</div><select id="set-epc-air" class="select mono" style="width:120px"></select></div>
            <div class="spacer"></div>
            <div class="label">提示：idx 来自 Cmd=159 EPC 上报的条目序号（从 0 开始）</div>
          </div>
          <div class="row" style="margin-top:12px">
            <button class="btn dark" id="set-open-method">方法</button>
            <button class="btn dark" id="set-open-processing">谱图处理</button>
            <button class="btn dark" id="set-open-reports">高级报表</button>
            <div class="spacer"></div>
            <div class="label" style="color:var(--muted)">二级入口占位：不占用顶栏标签</div>
          </div>
        </div>
      </section>
    </main>
  </div>

  <script>
    const tabsEl = document.getElementById('tabs');
    const views = {
      overview: document.getElementById('view-overview'),
      curve: document.getElementById('view-curve'),
      result: document.getElementById('view-result'),
      events: document.getElementById('view-events'),
      logs: document.getElementById('view-logs'),
      settings: document.getElementById('view-settings'),
    };

    function setActiveTab(tab){
      currentTab = tab || 'overview';
      for(const b of tabsEl.querySelectorAll('.tab')){
        b.classList.toggle('active', b.dataset.tab === tab);
      }
      for(const k in views){
        views[k].classList.toggle('active', k === tab);
      }
      const sel = selectedDevice();
      if(sel) saveUiToBackend(sel);
      if(tab === 'curve'){
        if(sel && !suppressUiSave){
          const ch = Number(chnEl.value || '0');
          const s = streams.get(streamKey(sel, ch));
          if(!s || !s.pts || s.pts.length === 0){
            restoreSessionOnly(sel).finally(()=>{});
          }
        }
        draw();
      }
      if(tab === 'overview') renderOverview();
      if(tab === 'result') renderResults();
      if(tab === 'events') renderEvents();
      if(tab === 'logs') renderLogs();
    }

    tabsEl.addEventListener('click', (e)=>{
      const btn = e.target.closest('.tab');
      if(!btn) return;
      setActiveTab(btn.dataset.tab);
    });

    const statusEl = document.getElementById('status');
    const deviceEl = document.getElementById('device');
    const chnEl = document.getElementById('chn');
    const ylowEl = document.getElementById('ylow');
    const yhighEl = document.getElementById('yhigh');
    const acqminEl = document.getElementById('acqmin');
    const fullminEl = document.getElementById('fullmin');
    const autoyEl = document.getElementById('autoy');
    const loopEl = document.getElementById('loop');
    const statEl = document.getElementById('stat');
    const cv = document.getElementById('cv');
    const ctx = cv.getContext('2d');
    const streams = new Map();
    const seenDevices = new Set();
    let lastActiveDevice = '';
    let deviceInfo = new Map();
    let serverInfo = null;
    const results = new Map();

    const gasCarrierEl = document.getElementById('gas-carrier');
    const gasH2El = document.getElementById('gas-h2');
    const gasAirEl = document.getElementById('gas-air');
    const tempColEl = document.getElementById('temp-col');
    const tempInj1El = document.getElementById('temp-inj1');
    const tempDet1El = document.getElementById('temp-det1');
    const tempInj2El = document.getElementById('temp-inj2');

    const overviewDevicesEl = document.getElementById('overview-devices');

    const resFromEl = document.getElementById('res-from');
    const resToEl = document.getElementById('res-to');
    const resTbodyEl = document.getElementById('res-tbody');
    const resExportEl = document.getElementById('res-export');
    const resDeleteEl = document.getElementById('res-delete');

    const evtOnlySelectedEl = document.getElementById('evt-only-selected');
    const evtClearEl = document.getElementById('evt-clear');
    const evtTbodyEl = document.getElementById('evt-tbody');

    const logsPreEl = document.getElementById('logs-pre');

    const setAcqMinEl = document.getElementById('set-acqmin');
    const setEpcCarrierEl = document.getElementById('set-epc-carrier');
    const setEpcH2El = document.getElementById('set-epc-h2');
    const setEpcAirEl = document.getElementById('set-epc-air');
    const setOpenMethodEl = document.getElementById('set-open-method');
    const setOpenProcessingEl = document.getElementById('set-open-processing');
    const setOpenReportsEl = document.getElementById('set-open-reports');

    const acqMinStorageKey = 'chrom.acqmin';
    try {
      const v = localStorage.getItem(acqMinStorageKey);
      if(v !== null && v !== undefined && acqminEl) {
        acqminEl.value = v;
      }
    } catch {}
    if(acqminEl){
      const saveAcqMin = ()=>{
        try { localStorage.setItem(acqMinStorageKey, String(acqminEl.value || '')); } catch {}
      };
      acqminEl.addEventListener('input', saveAcqMin);
      acqminEl.addEventListener('change', ()=>{
        saveAcqMin();
        saveUiToBackend(selectedDevice());
      });
    }

    const loopStorageKey = 'chrom.loop';
    try {
      const v = localStorage.getItem(loopStorageKey);
      if(v !== null && v !== undefined && loopEl) {
        loopEl.checked = (v === '1' || v === 'true');
      }
    } catch {}
    if(loopEl){
      const saveLoop = ()=>{
        try { localStorage.setItem(loopStorageKey, loopEl.checked ? '1' : '0'); } catch {}
        saveUiToBackend(selectedDevice());
      };
      loopEl.addEventListener('change', saveLoop);
    }
    
    if(cycleminEl){
      cycleminEl.addEventListener('change', () => {
        saveUiToBackend(selectedDevice());
      });
    }
    if(cyclemaxEl){
      cyclemaxEl.addEventListener('change', () => {
        saveUiToBackend(selectedDevice());
      });
    }

    const homeStatusEl = document.getElementById('home-status');
    const homeClockEl = document.getElementById('home-clock');
    const homeInjectEl = document.getElementById('home-inject');

    const nmhcHistPrefix = 'nmhc_history.';
    const nmhcHistByDevice = new Map();
    const nmhcFetchByDevice = new Map();
    const evtBuf = [];
    const evtMax = 400;

    function nowStr(){
      const d = new Date();
      const yyyy = d.getFullYear();
      const mm = String(d.getMonth()+1).padStart(2,'0');
      const dd = String(d.getDate()).padStart(2,'0');
      const hh = String(d.getHours()).padStart(2,'0');
      const mi = String(d.getMinutes()).padStart(2,'0');
      const ss = String(d.getSeconds()).padStart(2,'0');
      return yyyy + '-' + mm + '-' + dd + ' ' + hh + ':' + mi + ':' + ss;
    }

    function parseTimeText(s){
      const t = String(s || '').trim();
      if(!t) return null;
      const t2 = t.replace('T',' ').replace(/\//g,'-');
      const parts = t2.split(' ');
      if(parts.length < 2) return null;
      const d = new Date(parts[0] + 'T' + parts[1]);
      if(!isFinite(d.getTime())) return null;
      return d;
    }

    function nmhcEntryFromResult(deviceId, msg){
      const at = (msg && msg.at) ? new Date(msg.at) : new Date();
      const res = msg && msg.result ? msg.result : null;
      if(!res || !Array.isArray(res.pollutants)) return null;
      const by = new Map();
      for(const p of res.pollutants){
        if(p && (p.code || p.name)) by.set(p.code || p.name, p);
      }
      const thc = by.get('THC');
      const ch4 = by.get('CH4');
      const thcV = thc && isFinite(thc.height) ? Number(thc.height) : null;
      const ch4V = ch4 && isFinite(ch4.height) ? Number(ch4.height) : null;
      const nmhcV = (thcV !== null && ch4V !== null) ? (thcV - ch4V) : null;
      return { t: at.toISOString(), deviceId, traceId: (res.traceId || ''), thc: thcV, ch4: ch4V, nmhc: nmhcV };
    }

    function loadNmchHistory(deviceId){
      if(nmhcHistByDevice.has(deviceId)) return nmhcHistByDevice.get(deviceId);
      let arr = [];
      try{
        const raw = localStorage.getItem(nmhcHistPrefix + deviceId);
        if(raw){
          const v = JSON.parse(raw);
          if(Array.isArray(v)) arr = v;
        }
      }catch{}
      nmhcHistByDevice.set(deviceId, arr);
      return arr;
    }

    function saveNmchHistory(deviceId){
      const arr = nmhcHistByDevice.get(deviceId) || [];
      try{ localStorage.setItem(nmhcHistPrefix + deviceId, JSON.stringify(arr.slice(-5000))); } catch {}
    }

    function getNmhcFetchState(deviceId){
      let st = nmhcFetchByDevice.get(deviceId);
      if(!st){
        st = { inFlight: false, lastKey: '', lastOkAtMs: 0 };
        nmhcFetchByDevice.set(deviceId, st);
      }
      return st;
    }

    function nmhcRangeKey(fromD, toD){
      return (fromD ? fromD.toISOString() : '') + '|' + (toD ? toD.toISOString() : '');
    }

    async function fetchNmhcHistory(deviceId, fromD, toD){
      const qs = new URLSearchParams();
      qs.set('deviceId', deviceId);
      if(fromD) qs.set('from', fromD.toISOString());
      if(toD) qs.set('to', toD.toISOString());
      qs.set('limit', '5000');
      const res = await fetch('/api/v1/results/nmhc?' + qs.toString(), {method:'GET'});
      const j = await res.json().catch(()=>null);
      if(!res.ok){
        const msg = j && j.error ? String(j.error) : 'request failed';
        throw new Error(msg);
      }
      if(!Array.isArray(j)) return [];
      const out = [];
      for(const r of j){
        if(!r || !r.time) continue;
        out.push({
          t: String(r.time),
          deviceId: r.deviceId ? String(r.deviceId) : deviceId,
          traceId: r.traceId ? String(r.traceId) : '',
          thc: (r.thc === null || r.thc === undefined) ? null : Number(r.thc),
          ch4: (r.ch4 === null || r.ch4 === undefined) ? null : Number(r.ch4),
          nmhc: (r.nmhc === null || r.nmhc === undefined) ? null : Number(r.nmhc),
        });
      }
      return out;
    }

    function kickFetchNmhcHistory(deviceId, fromD, toD, force){
      if(!deviceId) return;
      const st = getNmhcFetchState(deviceId);
      const key = nmhcRangeKey(fromD, toD);
      const now = Date.now();
      if(!force){
        if(st.inFlight && st.lastKey === key) return;
        if(st.lastKey === key && (now - st.lastOkAtMs) < 1500) return;
      }
      st.inFlight = true;
      st.lastKey = key;
      fetchNmhcHistory(deviceId, fromD, toD).then(arr=>{
        nmhcHistByDevice.set(deviceId, arr);
        saveNmchHistory(deviceId);
        st.inFlight = false;
        st.lastOkAtMs = Date.now();
        applyKpiFromLatestNmhc(deviceId);
        const sel = selectedDevice();
        if(sel === deviceId){
          const run = document.getElementById('home-runCountVal');
          if(run) run.textContent = String(arr.length);
        }
        if(views.result && views.result.classList.contains('active') && selectedDevice() === deviceId){
          renderResults();
        }
      }).catch(()=>{
        st.inFlight = false;
      });
    }

    function addNmchHistory(deviceId, entry){
      if(!entry) return;
      const arr = loadNmchHistory(deviceId);
      arr.push(entry);
      if(arr.length > 5000) arr.splice(0, arr.length-5000);
      saveNmchHistory(deviceId);
      const sel = selectedDevice();
      if(sel === deviceId){
        const run = document.getElementById('home-runCountVal');
        if(run) run.textContent = String(arr.length);
      }
    }

    function applyKpiFromLatestNmhc(deviceId){
      const arr = loadNmchHistory(deviceId);
      let latest = null;
      for(const it of arr){
        if(!it || !it.t) continue;
        if(!latest){ latest = it; continue; }
        if(new Date(it.t).getTime() > new Date(latest.t).getTime()) latest = it;
      }
      const k1 = document.getElementById('kpi-thc');
      const k2 = document.getElementById('kpi-ch4');
      const k3 = document.getElementById('kpi-nmhc');
      const f4 = (v)=> (v === null || v === undefined || !isFinite(Number(v))) ? '-' : Number(v).toFixed(4);
      const thc = latest ? latest.thc : null;
      const ch4 = latest ? latest.ch4 : null;
      if(k1) k1.textContent = f4(thc);
      if(k2) k2.textContent = f4(ch4);
      if(k3){
        if(isFinite(Number(thc)) && isFinite(Number(ch4))){
          k3.textContent = (Number(thc) - Number(ch4)).toFixed(4);
        } else {
          k3.textContent = '-';
        }
      }

      const table = document.getElementById('tbody');
      if(table){
        const rows = table.querySelectorAll('tr');
        for(const tr of rows){
          const tds = tr.querySelectorAll('td');
          if(tds.length < 2) continue;
          const name = (tds[0].textContent || '').trim();
          if(name === '总烃') tds[1].textContent = f4(thc);
          if(name === '甲烷') tds[1].textContent = f4(ch4);
          if(name === '非甲烷总烃'){
            if(isFinite(Number(thc)) && isFinite(Number(ch4))){
              tds[1].textContent = (Number(thc) - Number(ch4)).toFixed(4);
            } else {
              tds[1].textContent = '-';
            }
          }
        }
      }
    }

    function pushEvtRow(deviceId, type, summary){
      evtBuf.push({ t: nowStr(), deviceId: deviceId || '', type, summary: summary || '' });
      if(evtBuf.length > evtMax) evtBuf.splice(0, evtBuf.length-evtMax);
    }

    function renderOverview(){
      if(!overviewDevicesEl) return;
      const rows = [];
      for(const [id, d] of deviceInfo.entries()){
        if(!String(id).startsWith('GC')) continue;
        const c143 = d && d.cmdCounts ? (d.cmdCounts['143'] || d.cmdCounts[143] || 0) : 0;
        rows.push({id, connected: !!d.connected, lastSeen: d.lastSeen || '', c143, last143: d.last143 || ''});
      }
      rows.sort((a,b)=> String(a.id).localeCompare(String(b.id)));
      if(rows.length === 0){
        overviewDevicesEl.innerHTML = '<tr><td class="mono" colspan="5" style="color:var(--muted)">等待 GC...</td></tr>';
        return;
      }
      overviewDevicesEl.innerHTML = rows.map(r=>{
        return '<tr>' +
          '<td class="mono">' + r.id + '</td>' +
          '<td class="mono">' + (r.connected ? 'Y' : 'N') + '</td>' +
          '<td class="mono">' + (r.lastSeen ? String(r.lastSeen).replace('T',' ').replace('Z','') : '-') + '</td>' +
          '<td class="mono">' + String(r.c143) + '</td>' +
          '<td class="mono">' + (r.last143 ? String(r.last143).replace('T',' ').replace('Z','') : '-') + '</td>' +
        '</tr>';
      }).join('');
    }

    function renderResults(){
      if(!resTbodyEl) return;
      const sel = selectedDevice();
      if(!sel){
        resTbodyEl.innerHTML = '<tr><td class="mono" colspan="4" style="color:var(--muted)">未选择设备</td></tr>';
        return;
      }
      const fromD = parseTimeText(resFromEl && resFromEl.value);
      const toD = parseTimeText(resToEl && resToEl.value);
      kickFetchNmhcHistory(sel, fromD, toD, false);
      const arr = loadNmchHistory(sel);
      const fetchSt = getNmhcFetchState(sel);
      if(arr.length === 0 && fetchSt.inFlight){
        resTbodyEl.innerHTML = '<tr><td class="mono" colspan="4" style="color:var(--muted)">加载中...</td></tr>';
        return;
      }
      const fromT = fromD ? fromD.getTime() : null;
      const toT = toD ? toD.getTime() : null;
      const f4 = (v)=> (v === null || v === undefined || !isFinite(Number(v))) ? '-' : Number(v).toFixed(4);
      const items = [];
      for(const it of arr){
        const t = new Date(it.t);
        const tt = t.getTime();
        if(fromT !== null && tt < fromT) continue;
        if(toT !== null && tt > toT) continue;
        items.push({t, it});
      }
      items.sort((a,b)=> b.t.getTime() - a.t.getTime());
      if(items.length === 0){
        resTbodyEl.innerHTML = '<tr><td class="mono" colspan="4" style="color:var(--muted)">暂无数据</td></tr>';
        return;
      }
      resTbodyEl.innerHTML = items.slice(0, 2000).map(x=>{
        const t = x.t;
        const ts = String(t.getFullYear()) + '-' + String(t.getMonth()+1).padStart(2,'0') + '-' + String(t.getDate()).padStart(2,'0') + ' ' + String(t.getHours()).padStart(2,'0') + ':' + String(t.getMinutes()).padStart(2,'0') + ':' + String(t.getSeconds()).padStart(2,'0');
        return '<tr>' +
          '<td class="mono">' + ts + '</td>' +
          '<td class="mono">' + f4(x.it.thc) + '</td>' +
          '<td class="mono">' + f4(x.it.ch4) + '</td>' +
          '<td class="mono">' + f4(x.it.nmhc) + '</td>' +
        '</tr>';
      }).join('');
    }

    function renderEvents(){
      if(!evtTbodyEl) return;
      const onlySel = !!(evtOnlySelectedEl && evtOnlySelectedEl.checked);
      const sel = selectedDevice();
      const items = onlySel && sel ? evtBuf.filter(e=> e.deviceId === sel) : evtBuf.slice();
      if(items.length === 0){
        evtTbodyEl.innerHTML = '<tr><td class="mono" colspan="4" style="color:var(--muted)">暂无数据</td></tr>';
        return;
      }
      evtTbodyEl.innerHTML = items.slice(-300).reverse().map(e=>{
        return '<tr>' +
          '<td class="mono">' + e.t + '</td>' +
          '<td class="mono">' + (e.deviceId || '-') + '</td>' +
          '<td class="mono">' + e.type + '</td>' +
          '<td class="mono">' + String(e.summary || '') + '</td>' +
        '</tr>';
      }).join('');
    }

    function renderLogs(){
      if(!logsPreEl) return;
      const sel = selectedDevice();
      const lines = [];
      if(serverInfo){
        lines.push('server.pid=' + (serverInfo.pid || ''));
        lines.push('server.startedAt=' + (serverInfo.startedAt || ''));
        lines.push('server.httpPort=' + (serverInfo.httpPort || ''));
        lines.push('server.tcpPorts=' + (serverInfo.tcpPorts ? JSON.stringify(serverInfo.tcpPorts) : ''));
      }
      if(sel){
        const d = deviceInfo.get(sel);
        if(d){
          const c143 = d && d.cmdCounts ? (d.cmdCounts['143'] || d.cmdCounts[143] || 0) : 0;
          lines.push('device=' + sel + ' connected=' + (!!d.connected) + ' lastCmd=' + d.lastCmd + ' 143=' + c143);
          lines.push('lastSeen=' + (d.lastSeen || '') + ' last143=' + (d.last143 || ''));
        }
        const s = streams.get(streamKey(sel, Number(chnEl.value||'0')));
        if(s && s.cycleStartedAtMs !== null){
          lines.push('elapsed=' + (s.lastElapsedS/60).toFixed(3) + 'min fullWindow=' + (fullWindowS()/60).toFixed(3) + 'min');
        }
      }
      const dbgEl = document.getElementById('dbg');
      if(dbgEl && dbgEl.textContent) lines.push('dbg=' + dbgEl.textContent);
      logsPreEl.textContent = lines.join('\n');
    }

    const epcMapKey = 'online_monitor_epc_map';
    function loadEpcMap(){
      try{
        const raw = localStorage.getItem(epcMapKey);
        if(raw){
          const v = JSON.parse(raw);
          const c = Number(v.carrier);
          const h = Number(v.h2);
          const a = Number(v.air);
          return { carrier: isFinite(c) && c >= 0 ? c : 0, h2: isFinite(h) && h >= 0 ? h : 1, air: isFinite(a) && a >= 0 ? a : 2 };
        }
      }catch{}
      return { carrier: 0, h2: 1, air: 2 };
    }

    function saveEpcMap(map){
      try{ localStorage.setItem(epcMapKey, JSON.stringify(map)); }catch{}
    }

    function fillEpcSelects(maxIdx){
      const n = Math.max(3, Math.min(64, Number(maxIdx || 12)));
      const opts = [];
      for(let i=0;i<n;i++){
        opts.push('<option value=\"' + i + '\">' + i + '</option>');
      }
      if(setEpcCarrierEl) setEpcCarrierEl.innerHTML = opts.join('');
      if(setEpcH2El) setEpcH2El.innerHTML = opts.join('');
      if(setEpcAirEl) setEpcAirEl.innerHTML = opts.join('');
      const m = loadEpcMap();
      if(setEpcCarrierEl) setEpcCarrierEl.value = String(m.carrier);
      if(setEpcH2El) setEpcH2El.value = String(m.h2);
      if(setEpcAirEl) setEpcAirEl.value = String(m.air);
    }

    function exportResultsCsv(){
      const sel = selectedDevice();
      if(!sel) return;
      const fromD = parseTimeText(resFromEl && resFromEl.value);
      const toD = parseTimeText(resToEl && resToEl.value);
      const a = document.createElement('a');
      const qs = new URLSearchParams();
      qs.set('deviceId', sel);
      if(fromD) qs.set('from', fromD.toISOString());
      if(toD) qs.set('to', toD.toISOString());
      a.href = '/api/v1/results/nmhc/export.csv?' + qs.toString();
      a.rel = 'noopener';
      document.body.appendChild(a);
      a.click();
      a.remove();
    }

    async function deleteResultsRange(){
      const sel = selectedDevice();
      if(!sel) return;
      const fromD = parseTimeText(resFromEl && resFromEl.value);
      const toD = parseTimeText(resToEl && resToEl.value);
      if(!fromD || !toD){
        alert('请填写开始与结束时间');
        return;
      }
      const fromT = fromD.getTime();
      const toT = toD.getTime();
      if(!(toT >= fromT)){
        alert('结束时间必须大于开始时间');
        return;
      }
      if(!confirm('确认删除该时间段内的记录？')) return;
      const qs = new URLSearchParams();
      qs.set('deviceId', sel);
      qs.set('from', fromD.toISOString());
      qs.set('to', toD.toISOString());
      const res = await fetch('/api/v1/results/nmhc?' + qs.toString(), {method:'DELETE'});
      const j = await res.json().catch(()=>({}));
      if(!res.ok){
        alert(j && j.error ? String(j.error) : '删除失败');
        return;
      }
      kickFetchNmhcHistory(sel, null, null, true);
    }


    function fullWindowS(){
      const v = Number(fullminEl.value || '2');
      if(!isFinite(v) || v <= 0) return 2*60;
      return v*60;
    }

    function tickClock(){
      const d = new Date();
      const yyyy = d.getFullYear();
      const mm = String(d.getMonth()+1).padStart(2,'0');
      const dd = String(d.getDate()).padStart(2,'0');
      const hh = String(d.getHours()).padStart(2,'0');
      const mi = String(d.getMinutes()).padStart(2,'0');
      const ss = String(d.getSeconds()).padStart(2,'0');
      homeClockEl.textContent = yyyy + '-' + mm + '-' + dd + ' ' + hh + ':' + mi + ':' + ss;
    }

    function drawPlaceholder(canvas){
      if(!canvas) return;
      const ctx = canvas.getContext('2d');
      if(!ctx) return;
      const w = canvas.width;
      const h = canvas.height;
      ctx.clearRect(0,0,w,h);
      ctx.fillStyle = '#fff';
      ctx.fillRect(0,0,w,h);
      ctx.strokeStyle = '#E6EEF8';
      ctx.lineWidth = 1;
      for(let i=0;i<=10;i++){
        const x = 60 + (w-80)*(i/10);
        ctx.beginPath();
        ctx.moveTo(x, 16);
        ctx.lineTo(x, h-44);
        ctx.stroke();
      }
      for(let i=0;i<=7;i++){
        const y = 16 + (h-60)*(i/7);
        ctx.beginPath();
        ctx.moveTo(60, y);
        ctx.lineTo(w-18, y);
        ctx.stroke();
      }
      ctx.strokeStyle = '#000';
      ctx.lineWidth = 2;
      ctx.beginPath();
      ctx.moveTo(60, 16);
      ctx.lineTo(60, h-44);
      ctx.lineTo(w-18, h-44);
      ctx.stroke();
      ctx.save();
      ctx.translate(22, (h-44+16)/2);
      ctx.rotate(-Math.PI/2);
      ctx.fillStyle = '#2B6DFF';
      ctx.font = '14px system-ui';
      ctx.fillText('信号(pA)', -28, 0);
      ctx.restore();
    }

    function streamKey(deviceId, channel){
      return deviceId + '|' + String(channel);
    }

    function getStream(deviceId, channel){
      const k = streamKey(deviceId, channel);
      let s = streams.get(k);
      if(!s){
        s = { deviceId, channel, sessionToken: '', cycleStartS: null, dtS: null, winS: null, pts: [], lastMin: null, lastMax: null, lastElapsedS: 0, lastValue: null, stopped: false, targetStopS: null, stopRequested: false, resultRequested: false, loopActive: false, autoTimer: null, cycleStartedAtMs: null };
        streams.set(k, s);
      }
      s.deviceId = deviceId;
      s.channel = channel;
      const win = fullWindowS();
      if(s.winS !== win){
        s.winS = win;
      }
      return s;
    }

    function trimPointsToWindow(s){
      const win = s.winS || fullWindowS();
      let i = 0;
      while(i < s.pts.length && s.pts[i][0] < -0.5) i++;
      if(i > 0) s.pts = s.pts.slice(i);
      let j = s.pts.length - 1;
      while(j >= 0 && s.pts[j][0] > win+0.5) j--;
      if(j < s.pts.length - 1) s.pts = s.pts.slice(0, j+1);
      if(s.pts.length > 200000) s.pts = s.pts.slice(s.pts.length - 200000);
    }

    function resetStream(deviceId, channel){
      const s = getStream(deviceId, channel);
      if(s.autoTimer){
        try { clearTimeout(s.autoTimer); } catch {}
      }
      s.autoTimer = null;
      s.cycleStartedAtMs = null;
      s.sessionToken = '';
      s.cycleStartS = null;
      s.dtS = null;
      s.pts = [];
      s.lastMin = null;
      s.lastMax = null;
      s.lastElapsedS = 0;
      s.lastValue = null;
      s.stopped = false;
      s.targetStopS = null;
      s.stopRequested = false;
      s.resultRequested = false;
      s.loopActive = false;
    }

    function resetStreamForNewCycle(deviceId, channel){
      const s = getStream(deviceId, channel);
      if(s.autoTimer){
        try { clearTimeout(s.autoTimer); } catch {}
      }
      s.autoTimer = null;
      s.cycleStartedAtMs = null;
      s.sessionToken = '';
      s.cycleStartS = null;
      s.dtS = null;
      s.pts = [];
      s.lastMin = null;
      s.lastMax = null;
      s.lastElapsedS = 0;
      s.lastValue = null;
      s.stopped = false;
      s.stopRequested = false;
      s.resultRequested = false;
      return s;
    }

    async function localActionFor(deviceId, channel, action){
      if(!deviceId) return {ok:false, error:'no device'};
      const url = '/api/v1/devices/' + encodeURIComponent(deviceId) + '/' + action + '?channel=' + Number(channel || 0);
      const res = await fetch(url, {method:'POST'});
      const j = await res.json().catch(()=>({}));
      if(!res.ok){
        return {ok:false, error: j.error || 'request failed'};
      }
      return {ok:true};
    }

    async function localAction(action){
      const sel = selectedDevice();
      const channel = Number(chnEl.value || '0');
      return localActionFor(sel, channel, action);
    }

    function draw(){
      ctx.clearRect(0,0,cv.width,cv.height);
      const sel = selectedDevice();
      const ch = Number(chnEl.value || '0');
      if(!sel){
        ctx.fillStyle = '#777';
        ctx.font = '14px system-ui';
        ctx.fillText('等待选择设备', 12, 22);
        return;
      }

      const s = getStream(sel, ch);
      if(!s.pts || s.pts.length < 2){
        ctx.fillStyle = '#777';
        ctx.font = '14px system-ui';
        ctx.fillText('暂无实时数据（等待主板发送 143 数据流）', 12, 22);
        return;
      }

      const win = s.winS || fullWindowS();
      const viewStartS = 0;
      const viewEndS = win;

      let yBeg = Number(ylowEl.value || '0');
      let yEnd = Number(yhighEl.value || '40');
      if(!isFinite(yBeg)) yBeg = 0;
      if(!isFinite(yEnd)) yEnd = 40;
      if(yEnd <= yBeg) yEnd = yBeg + 1;

      if(autoyEl.checked){
        let yMin = Infinity, yMax = -Infinity;
        for(const p of s.pts){
          const t = p[0];
          if(t < viewStartS || t > viewEndS) continue;
          const y = p[1];
          if(!isFinite(y)) continue;
          if(y < yMin) yMin = y;
          if(y > yMax) yMax = y;
        }
        if(!isFinite(yMin) || !isFinite(yMax)){
          yMin = 0;
          yMax = 1;
        }
        const span0 = yMax - yMin;
        const minSpan = 0.5;
        if(span0 < minSpan){
          const c0 = (yMin + yMax) * 0.5;
          yMin = c0 - minSpan/2;
          yMax = c0 + minSpan/2;
        }
        const c = (yMin + yMax) * 0.5;
        const half = (yMax - yMin) * 0.5;
        const padHalf = half * 1.02;
        yBeg = c - padHalf;
        yEnd = c + padHalf;
      }

      if(s.lastMin !== null && s.lastMax !== null){
        const a = 0.2;
        yBeg = s.lastMin + (yBeg - s.lastMin) * a;
        yEnd = s.lastMax + (yEnd - s.lastMax) * a;
      }
      s.lastMin = yBeg;
      s.lastMax = yEnd;

      const padL = 60;
      const padR = 18;
      const padT = 16;
      const padB = 44;
      const w = cv.width - padL - padR;
      const h = cv.height - padT - padB;

      const curveTopReserve = h * 0.40;
      const curveBottomReserve = h * 0.05;
      const curveH = Math.max(1, h - curveTopReserve - curveBottomReserve);

      const xBegMin = 0;
      const xEndMin = viewEndS / 60;
      const xSpanMin = xEndMin - xBegMin;

      function niceStep(range, targetTicks){
        const raw = range / targetTicks;
        const pow = Math.pow(10, Math.floor(Math.log10(raw)));
        const n = raw / pow;
        let step;
        if(n <= 1) step = 1;
        else if(n <= 2) step = 2;
        else if(n <= 3) step = 3;
        else if(n <= 5) step = 5;
        else step = 10;
        return step * pow;
      }

      const xStep = niceStep(xSpanMin, 7);
      const yStep = niceStep(yEnd - yBeg, 5);

      ctx.strokeStyle = '#E6EEF8';
      ctx.lineWidth = 1;
      for(let x = Math.ceil(xBegMin / xStep) * xStep; x <= xEndMin + 1e-9; x += xStep){
        const sx = padL + ((x - xBegMin) / xSpanMin) * w;
        ctx.beginPath();
        ctx.moveTo(sx, padT);
        ctx.lineTo(sx, padT + h);
        ctx.stroke();
      }
      for(let y = Math.ceil(yBeg / yStep) * yStep; y <= yEnd + 1e-9; y += yStep){
        const sy = padT + (1 - (y - yBeg) / (yEnd - yBeg)) * h;
        ctx.beginPath();
        ctx.moveTo(padL, sy);
        ctx.lineTo(padL + w, sy);
        ctx.stroke();
      }

      ctx.strokeStyle = '#000';
      ctx.lineWidth = 2;
      ctx.beginPath();
      ctx.moveTo(padL, padT);
      ctx.lineTo(padL, padT + h);
      ctx.lineTo(padL + w, padT + h);
      ctx.stroke();

      ctx.fillStyle = '#000';
      ctx.font = '12px system-ui';
      for(let x = Math.ceil(xBegMin / xStep) * xStep; x <= xEndMin + 1e-9; x += xStep){
        const sx = padL + ((x - xBegMin) / xSpanMin) * w;
        const label = (Math.round(x * 1000) / 1000).toString();
        ctx.fillText(label, sx - 6, padT + h + 18);
      }

      ctx.fillStyle = '#1F5CFF';
      ctx.font = '700 14px system-ui';
      for(let y = Math.ceil(yBeg / yStep) * yStep; y <= yEnd + 1e-9; y += yStep){
        const sy = padT + (1 - (y - yBeg) / (yEnd - yBeg)) * h;
        ctx.fillText(y.toFixed(0), 10, sy + 5);
      }

      ctx.save();
      ctx.translate(26, padT + h/2);
      ctx.rotate(-Math.PI/2);
      ctx.fillStyle = '#1F5CFF';
      ctx.font = '700 14px system-ui';
      ctx.fillText('信号(pA)', -32, 0);
      ctx.restore();

      ctx.strokeStyle = '#1F5CFF';
      ctx.lineWidth = 1;
      ctx.beginPath();
      let started = false;
      const maxDraw = Math.max(2000, Math.floor(w*3));
      const stride = Math.max(1, Math.floor(s.pts.length / maxDraw));
      for(let i=0;i<s.pts.length;i+=stride){
        const tS = s.pts[i][0];
        if(tS < viewStartS || tS > viewEndS) continue;
        const v = s.pts[i][1];
        if(!isFinite(v)){
          started = false;
          continue;
        }
        const tMin = tS / 60;
        const x = padL + ((tMin - xBegMin) / xSpanMin) * w;
        const yn = (v-yBeg)/(yEnd-yBeg);
        const y = padT + curveTopReserve + (1-yn)*curveH;
        if(!started){
          ctx.moveTo(x,y);
          started = true;
        } else {
          ctx.lineTo(x,y);
        }
      }
      ctx.stroke();

      const rk = streamKey(sel, ch);
      const rr = results.get(rk);
      const r = rr && rr.result ? rr.result : null;
      const rTok = rr && rr.sessionToken ? rr.sessionToken : '';
      if(r && r.pollutants && Array.isArray(r.pollutants) && rTok && s.sessionToken && rTok === s.sessionToken){
        ctx.save();
        ctx.fillStyle = '#1F5CFF';
        ctx.font = '700 13px system-ui';
        for(const p of r.pollutants){
          if(!p || p.status !== 'detected') continue;
          const rtS = Number(p.rtS);
          if(!isFinite(rtS)) continue;
          const xMin = rtS/60;
          if(xMin < xBegMin || xMin > xEndMin) continue;
          const x = padL + ((xMin - xBegMin) / xSpanMin) * w;
          const y = padT + curveTopReserve * 0.9;
          ctx.save();
          ctx.translate(x, y);
          ctx.rotate(-Math.PI/2);
          const t = (p.name || p.code || '') + '  ' + (xMin.toFixed(4));
          ctx.fillText(t, 0, 0);
          ctx.restore();
        }
        ctx.restore();
      }
    }

    document.getElementById('clear').addEventListener('click', ()=>{
      const sel = selectedDevice();
      if(!sel) return;
      resetStream(sel, Number(chnEl.value || '0'));
      draw();
    });

    function setButtonsEnabled(enabled){
      document.getElementById('start').disabled = !enabled;
      document.getElementById('stop').disabled = !enabled;
    }

    async function refreshDevices(){
      try{
        const res = await fetch('/api/v1/devices');
        if(!res.ok) return;
        const arr = await res.json();
        deviceInfo = new Map();
        for(const d of arr){
          deviceInfo.set(d.deviceId, d);
          ensureDeviceOption(d.deviceId);
        }

        if(selectedDevice() === ''){
          let prefer = '';
          if(backendLastDeviceId && deviceInfo.has(backendLastDeviceId)){
            prefer = backendLastDeviceId;
          }
          for(const d of arr){
            if(!prefer && String(d.deviceId || '').startsWith('GC')){ prefer = d.deviceId; break; }
          }
          if(prefer){
            deviceEl.value = prefer;
            statusEl.textContent = '在线: ' + prefer;
            const run = document.getElementById('home-runCountVal');
            if(run) run.textContent = String(loadNmchHistory(prefer).length);
          }
        }

        if(!didInitialRestore){
          const sel0 = selectedDevice();
          if(sel0){
            didInitialRestore = true;
            restoreFromBackend(sel0);
          }
        }
        renderDebug();
        if(views.overview && views.overview.classList.contains('active')) renderOverview();
        if(views.logs && views.logs.classList.contains('active')) renderLogs();
      }catch{}
    }

    async function refreshServer(){
      try{
        const res = await fetch('/api/v1/server');
        if(!res.ok) return;
        serverInfo = await res.json();
        renderDebug();
      }catch{}
    }

    function renderDebug(){
      const dbg = document.getElementById('dbg');
      const sel = selectedDevice();
      const cur = sel || lastActiveDevice;
      if(!cur){
        dbg.textContent = '';
        setButtonsEnabled(false);
        return;
      }
      const d = deviceInfo.get(cur);
      if(!d){
        dbg.textContent = '设备: ' + cur + '（未获取到统计信息）';
        setButtonsEnabled(false);
        return;
      }
      const c143 = (d.cmdCounts && d.cmdCounts['143']) ? d.cmdCounts['143'] : 0;
      const lastSeen = d.lastSeen ? new Date(d.lastSeen) : null;
      const last143 = d.last143 ? new Date(d.last143) : null;
      const now = new Date();
      const seenAgo = lastSeen ? Math.max(0, Math.round((now - lastSeen)/1000)) : -1;
      const d143Ago = last143 && last143.getTime() > 0 ? Math.max(0, Math.round((now - last143)/1000)) : -1;
      let extra = '';
      const s = streams.get(streamKey(cur, Number(chnEl.value||'0')));
      if(s && s.cycleStartS !== null){
        extra = ' | elapsed=' + (s.lastElapsedS/60).toFixed(2) + 'min/' + (fullWindowS()/60).toFixed(2) + 'min';
      }
      let sinfo = '';
      if(serverInfo && serverInfo.pid){
        sinfo = ' | pid=' + serverInfo.pid;
      }
      dbg.textContent = '设备: ' + cur + ' | lastCmd=' + d.lastCmd + ' | 143=' + c143 + ' | lastSeen=' + (seenAgo>=0 ? (seenAgo+'s') : '-') + ' | last143=' + (d143Ago>=0 ? (d143Ago+'s') : '-') + ' | control=' + (d.allowControl ? 'on' : 'off') + extra + sinfo;
	  setButtonsEnabled(!!d.connected);
    }

    async function sendCmd(name){
      const sel = selectedDevice();
      if(!sel){
        alert('请选择设备');
        return;
      }
      const channel = Number(chnEl.value || '0');
      const url = '/api/v1/devices/' + encodeURIComponent(sel) + '/cmd?name=' + encodeURIComponent(name) + '&channel=' + channel;
      const res = await fetch(url, {method:'POST'});
      const j = await res.json().catch(()=>({}));
      if(!res.ok){
        alert(j.error || '发送失败');
        return;
      }
      await refreshDevices();
    }

    document.getElementById('start').addEventListener('click', ()=>{
      const sel = selectedDevice();
      saveUiToBackend(sel);
      if(sel){
        resetStream(sel, Number(chnEl.value || '0'));
        const s = getStream(sel, Number(chnEl.value || '0'));
        s.loopActive = true;
        draw();
      }
	  localAction('localStart').finally(()=>{});
	  const di = deviceInfo.get(sel);
	  if(di && di.canStart22){
	    sendCmd('start');
	  }
    });
	  document.getElementById('stop').addEventListener('click', ()=>{
	    const sel = selectedDevice();
	    if(sel){
	      const s = getStream(sel, Number(chnEl.value || '0'));
	      s.loopActive = false;
	      if(s.autoTimer){
	        try { clearTimeout(s.autoTimer); } catch {}
	      }
	      s.autoTimer = null;
	      s.stopRequested = true;
	      s.stopped = true;
	    }
	    localAction('localStop').finally(()=>{});
	  });

    homeInjectEl.addEventListener('click', ()=>{
      setActiveTab('chrom');
      const sel = selectedDevice();
      if(sel){
        resetStream(sel, Number(chnEl.value || '0'));
	    const s = getStream(sel, Number(chnEl.value || '0'));
	    s.loopActive = true;
        draw();
      }
	  localAction('localStart').finally(()=>{});
	  const di = deviceInfo.get(sel);
	  if(di && di.canStart22){
	    sendCmd('start');
	  }
    });

    function ensureDeviceOption(id){
      if(!String(id).startsWith('GC')) return;
      if(seenDevices.has(id)) return;
      seenDevices.add(id);
      const opt = document.createElement('option');
      opt.value = id;
      opt.textContent = id;
      if(deviceEl.options.length === 1 && deviceEl.options[0].value === ''){
        deviceEl.remove(0);
      }
      deviceEl.appendChild(opt);
    }

    function selectedDevice(){
      return deviceEl.value || '';
    }

    const es = new EventSource('/events');
    es.onmessage = (e)=>{
      let msg;
      try{ msg = JSON.parse(e.data); }catch{ return; }
	  if(msg.type === 'telemetry'){
	    if(!String(msg.deviceId).startsWith('GC')) return;
	    const sel = selectedDevice();
	    if(sel && msg.deviceId !== sel) return;
	    pushEvtRow(msg.deviceId, 'telemetry', 'temps=' + [msg.tempCol,msg.tempInj1,msg.tempDet1,msg.tempInj2].filter(v=>v!==undefined).length + ' epc=' + (msg.epc ? msg.epc.length : 0));
	    const f2 = (v)=> {
	      if(v === undefined || v === null) return '-';
	      const n = Number(v);
	      if(!isFinite(n)) return '-';
	      if(n >= 655.35 - 1e-9) return '-';
	      return n.toFixed(2);
	    };
	    const f1 = (v)=> (v === undefined || v === null || !isFinite(Number(v))) ? '-' : Number(v).toFixed(1);
	    const gasText = (psi, sccm)=>{
	      const p = f2(psi);
	      const f = f2(sccm);
	      if(p === '-' && f === '-') return '-';
	      if(p === '-') return f + ' sccm';
	      if(f === '-') return p + ' psi';
	      return p + ' psi / ' + f + ' sccm';
	    };
	    const epcMap = loadEpcMap();
	    if(msg.epc && Array.isArray(msg.epc)){
	      const g0 = msg.epc[epcMap.carrier] || null;
	      const g1 = msg.epc[epcMap.h2] || null;
	      const g2 = msg.epc[epcMap.air] || null;
	      if(gasCarrierEl) gasCarrierEl.textContent = gasText(g0 && g0.psi, g0 && g0.sccm);
	      if(gasH2El) gasH2El.textContent = gasText(g1 && g1.psi, g1 && g1.sccm);
	      if(gasAirEl) gasAirEl.textContent = gasText(g2 && g2.psi, g2 && g2.sccm);
	    }
	    if(tempColEl && msg.tempCol !== undefined) tempColEl.textContent = f1(msg.tempCol);
	    if(tempInj1El && msg.tempInj1 !== undefined) tempInj1El.textContent = f1(msg.tempInj1);
	    if(tempDet1El && msg.tempDet1 !== undefined) tempDet1El.textContent = f1(msg.tempDet1);
	    if(tempInj2El && msg.tempInj2 !== undefined) tempInj2El.textContent = f1(msg.tempInj2);
	    if(views.events && views.events.classList.contains('active')) renderEvents();
	    return;
	  }
      if(msg.type === 'result'){
        if(!String(msg.deviceId).startsWith('GC')) return;
        const ch = (msg.channel === undefined || msg.channel === null) ? 0 : msg.channel;
        const sel = selectedDevice();
        if(sel && msg.deviceId !== sel) return;
        const rk = streamKey(msg.deviceId, ch);
        if(msg.result && msg.result.pollutants){
          const entry = nmhcEntryFromResult(msg.deviceId, msg);
          addNmchHistory(msg.deviceId, entry);
          pushEvtRow(msg.deviceId, 'result', 'pollutants=' + msg.result.pollutants.length);
          const s = getStream(msg.deviceId, ch);
          const tok = (msg.sessionToken !== undefined && msg.sessionToken !== null) ? String(msg.sessionToken) : (s.sessionToken || '');
          results.set(rk, { result: msg.result, sessionToken: tok });
          const table = document.getElementById('tbody');
          if(table){
            const rows = table.querySelectorAll('tr');
            const byName = new Map();
            for(const p of msg.result.pollutants){
              if(p && (p.name || p.code)) byName.set(p.code || p.name, p);
            }
            let thc = byName.get('THC');
            let ch4 = byName.get('CH4');
			const k1 = document.getElementById('kpi-thc');
			const k2 = document.getElementById('kpi-ch4');
			const k3 = document.getElementById('kpi-nmhc');
			if(k1) k1.textContent = thc && isFinite(thc.height) ? Number(thc.height).toFixed(4) : '-';
			if(k2) k2.textContent = ch4 && isFinite(ch4.height) ? Number(ch4.height).toFixed(4) : '-';
			if(k3) {
				if(thc && ch4 && isFinite(thc.height) && isFinite(ch4.height)){
					k3.textContent = (Number(thc.height) - Number(ch4.height)).toFixed(4);
				} else {
					k3.textContent = '-';
				}
			}
            for(const tr of rows){
              const tds = tr.querySelectorAll('td');
              if(tds.length < 2) continue;
              const name = (tds[0].textContent || '').trim();
              if(name === '总烃'){
                tds[1].textContent = thc && isFinite(thc.height) ? Number(thc.height).toFixed(4) : '-';
              }
              if(name === '甲烷'){
                tds[1].textContent = ch4 && isFinite(ch4.height) ? Number(ch4.height).toFixed(4) : '-';
              }
              if(name === '非甲烷总烃'){
                if(thc && ch4 && isFinite(thc.height) && isFinite(ch4.height)){
                  tds[1].textContent = (Number(thc.height) - Number(ch4.height)).toFixed(4);
                } else {
                  tds[1].textContent = '-';
                }
              }
            }
          }
        }
        draw();
        if(views.result && views.result.classList.contains('active')) renderResults();
        if(views.overview && views.overview.classList.contains('active')) renderOverview();
        if(views.events && views.events.classList.contains('active')) renderEvents();
        if(views.logs && views.logs.classList.contains('active')) renderLogs();
        return;
      }

      if(msg.type === 'device'){
        if(String(msg.deviceId).startsWith('GC')){
          ensureDeviceOption(msg.deviceId);
          lastActiveDevice = msg.deviceId;
        }
        pushEvtRow(msg.deviceId, 'device', 'online');
        const sel = selectedDevice();
        if(sel === ''){
          if(String(msg.deviceId).startsWith('GC')){
            statusEl.textContent = '在线: ' + msg.deviceId + '（自动）';
          }
        } else if(sel === msg.deviceId){
          statusEl.textContent = '在线: ' + msg.deviceId;
        }
        if(views.events && views.events.classList.contains('active')) renderEvents();
        return;
      }

      if(msg.type !== 'samples') return;

      if(!String(msg.deviceId).startsWith('GC')) return;
      ensureDeviceOption(msg.deviceId);
      const sel = selectedDevice();
      if(sel && msg.deviceId !== sel) return;

      const msgChannel = (msg.channel === undefined || msg.channel === null) ? 0 : msg.channel;
      if(String(msgChannel) !== chnEl.value) return;

      if(!sel){
        deviceEl.value = msg.deviceId;
        statusEl.textContent = '在线: ' + msg.deviceId;
      }

      let s = getStream(msg.deviceId, msgChannel);
      if(s.stopped) return;
      const msgTok = (msg.sessionToken !== undefined && msg.sessionToken !== null) ? String(msg.sessionToken) : '';
      if(msgTok && s.sessionToken && msgTok !== s.sessionToken){
        resetStreamForNewCycle(msg.deviceId, msgChannel);
        s = getStream(msg.deviceId, msgChannel);
      }
      if(msgTok) s.sessionToken = msgTok;
      const dt = Number(msg.dtS);
      if(!isFinite(dt) || dt <= 0) return;
      pushEvtRow(msg.deviceId, 'samples', 'ch=' + msgChannel + ' n=' + (msg.values ? msg.values.length : 0) + ' dt=' + dt.toFixed(4));
      if(s.dtS !== dt){
        s.dtS = dt;
      }
      if(s.cycleStartS === null){
        s.cycleStartS = Number(msg.t0S) || 0;
      }
      const base = (Number(msg.t0S) || 0) - s.cycleStartS;

      if(base < -1 || (s.lastElapsedS > 0 && base+msg.values.length*dt < s.lastElapsedS-5)){
        resetStreamForNewCycle(msg.deviceId, msgChannel);
        s = getStream(msg.deviceId, msgChannel);
        s.dtS = dt;
        s.cycleStartS = Number(msg.t0S) || 0;
        processSamples(s, 0, dt, msg.values);
      } else {
        processSamples(s, base, dt, msg.values);
      }

      const minText = (s.lastElapsedS/60).toFixed(3);
      const vText = (s.lastValue === null ? '0.000' : Number(s.lastValue).toFixed(3));
      statEl.textContent = '通道' + (Number(chnEl.value||'0')+1) + ': ' + minText + ' min   ' + vText + ' pA';
      homeStatusEl.textContent = '时间: ' + minText + ' min   信号: ' + vText + ' pA';
      draw();
      renderDebug();
    };

    function processSamples(s, base, dt, values){
      const win = s.winS || fullWindowS();
      for(let i=0;i<values.length;i++){
        const t = base + i*dt;
        if(t < -0.5) continue;
        if(t > win+0.5) continue;
        const vv = Number(values[i]);
        s.pts.push([t, vv]);
        s.lastValue = vv;
      }
      const end = base + values.length*dt;
      if(end > s.lastElapsedS) s.lastElapsedS = end;
      trimPointsToWindow(s);
    }
    es.onerror = ()=>{
      const sel = selectedDevice();
      if(sel){
        statusEl.textContent = '连接断开: ' + sel;
      } else if(lastActiveDevice){
        statusEl.textContent = '连接断开: ' + lastActiveDevice;
      } else {
        statusEl.textContent = '连接断开';
      }
    };

    deviceEl.addEventListener('change', ()=>{
      const sel = selectedDevice();
      if(sel){
        statusEl.textContent = '在线: ' + sel;
        const run = document.getElementById('home-runCountVal');
        if(run) run.textContent = String(loadNmchHistory(sel).length);
        kickFetchNmhcHistory(sel, null, null, true);
        restoreFromBackend(sel);
      } else {
        statusEl.textContent = '未选择设备（自动）';
      }
      draw();
      renderDebug();
      if(views.overview && views.overview.classList.contains('active')) renderOverview();
      if(views.result && views.result.classList.contains('active')) renderResults();
      if(views.logs && views.logs.classList.contains('active')) renderLogs();
    });

    chnEl.addEventListener('change', ()=>{
      const sel = selectedDevice();
      saveUiToBackend(sel);
      if(sel){
        restoreSessionOnly(sel);
      }
      draw();
      renderDebug();
    });

    fullminEl.addEventListener('change', ()=>{
      const sel = selectedDevice();
      saveUiToBackend(sel);
      if(sel){
        resetStream(sel, Number(chnEl.value || '0'));
      }
      draw();
      renderDebug();
    });

    ylowEl.addEventListener('change', ()=>{ saveUiToBackend(selectedDevice()); draw(); });
    yhighEl.addEventListener('change', ()=>{ saveUiToBackend(selectedDevice()); draw(); });
    autoyEl.addEventListener('change', ()=>{ saveUiToBackend(selectedDevice()); draw(); });

    const cycleminEl = document.getElementById('cyclemin');
    const cyclemaxEl = document.getElementById('cyclemax');

    let backendLastDeviceId = '';
    let didInitialRestore = false;
    let suppressUiSave = false;
    let currentTab = 'overview';

    function uiPayloadFor(deviceId){
      const ch = Number(chnEl.value || '0');
      const fullMin = Number(fullminEl.value || '2');
      const yLow = Number(ylowEl.value || '0');
      const yHigh = Number(yhighEl.value || '40');
      const autoY = !!autoyEl.checked;
      const acqMin = Number(acqminEl.value || '0');
      const loop = !!(loopEl && loopEl.checked);
      const cycleMin = Number(cycleminEl ? cycleminEl.value : '2');
      const cycleMax = Number(cyclemaxEl ? cyclemaxEl.value : '9999');
      const epcMap = loadEpcMap();
      return {
        deviceId,
        activeTab: currentTab,
        selectedChannel: isFinite(ch) ? ch : 0,
        fullMin: isFinite(fullMin) ? fullMin : 2,
        yLow: isFinite(yLow) ? yLow : 0,
        yHigh: isFinite(yHigh) ? yHigh : 40,
        autoY,
        acqMin: isFinite(acqMin) ? acqMin : 0,
        loop,
        cycleMin: isFinite(cycleMin) ? cycleMin : 2,
        cycleMax: isFinite(cycleMax) ? cycleMax : 9999,
        epcCarrier: Number(epcMap.carrier || 0),
        epcH2: Number(epcMap.h2 || 1),
        epcAir: Number(epcMap.air || 2),
      };
    }

    async function saveUiToBackend(deviceId){
      if(suppressUiSave) return;
      if(!deviceId) return;
      const payload = uiPayloadFor(deviceId);
      try{
        await fetch('/api/v1/ui', {method:'POST', headers:{'Content-Type':'application/json'}, body: JSON.stringify(payload)});
      }catch{}
    }

    function applyUiState(u){
      if(!u) return;
      if(u.activeTab){
        currentTab = String(u.activeTab);
      }
      if(u.fullMin !== undefined) fullminEl.value = String(u.fullMin);
      if(u.yLow !== undefined) ylowEl.value = String(u.yLow);
      if(u.yHigh !== undefined) yhighEl.value = String(u.yHigh);
      if(u.autoY !== undefined) autoyEl.checked = !!u.autoY;
      if(u.acqMin !== undefined) acqminEl.value = String(u.acqMin);
      if(u.loop !== undefined && loopEl) loopEl.checked = !!u.loop;
      if(u.cycleMin !== undefined && cycleminEl) cycleminEl.value = String(u.cycleMin);
      if(u.cycleMax !== undefined && cyclemaxEl) cyclemaxEl.value = String(u.cycleMax);
      if(u.selectedChannel !== undefined) chnEl.value = String(u.selectedChannel);

      document.getElementById('set-fullmin').value = fullminEl.value;
      document.getElementById('set-ylow').value = ylowEl.value;
      document.getElementById('set-yhigh').value = yhighEl.value;
      document.getElementById('set-autoy').checked = autoyEl.checked;
      if(setAcqMinEl) setAcqMinEl.value = acqminEl.value;

      const epcMap = { carrier: Number(u.epcCarrier || 0), h2: Number(u.epcH2 || 1), air: Number(u.epcAir || 2) };
      saveEpcMap(epcMap);
      if(setEpcCarrierEl) setEpcCarrierEl.value = String(epcMap.carrier);
      if(setEpcH2El) setEpcH2El.value = String(epcMap.h2);
      if(setEpcAirEl) setEpcAirEl.value = String(epcMap.air);
    }

    function hydrateStreamFromSession(deviceId, channel, sess){
      if(!sess) return;
      const dt = Number(sess.dtS);
      const values = sess.values;
      if(!isFinite(dt) || dt <= 0) return;
      if(!Array.isArray(values) || values.length < 2) return;
      resetStream(deviceId, channel);
      const s = getStream(deviceId, channel);
      s.dtS = dt;
      s.sessionToken = sess.sessionToken ? String(sess.sessionToken) : '';
      s.cycleStartS = 0;
      const win = fullWindowS();
      s.winS = win;
      const maxPts = 200000;
      const maxSpanPts = Math.max(2, Math.floor((win + 2) / dt));
      const keep = Math.min(values.length, Math.max(maxSpanPts, Math.min(maxPts, values.length)));
      const valuesCount = values.length;
      const totalCount = (sess.totalCount !== undefined && sess.totalCount !== null) ? Number(sess.totalCount) : valuesCount;
      const baseIdx = Math.max(0, totalCount - valuesCount);
      const startLocalIdx = Math.max(0, valuesCount - keep);
      for(let i=startLocalIdx;i<valuesCount;i++){
        const t = (baseIdx + i) * dt;
        const v = Number(values[i]);
        s.pts.push([t, v]);
        s.lastValue = v;
      }
      s.lastElapsedS = (totalCount - 1) * dt;
      trimPointsToWindow(s);
      const minText = (s.lastElapsedS/60).toFixed(3);
      const vText = (s.lastValue === null ? '0.000' : Number(s.lastValue).toFixed(3));
      statEl.textContent = '通道' + (Number(chnEl.value||'0')+1) + ': ' + minText + ' min   ' + vText + ' pA';
      homeStatusEl.textContent = '时间: ' + minText + ' min   信号: ' + vText + ' pA';
    }

    async function restoreFromBackend(deviceId){
      if(!deviceId) return;
      suppressUiSave = true;
      try{
        const uRes = await fetch('/api/v1/ui?deviceId=' + encodeURIComponent(deviceId));
        if(uRes.ok){
          const u = await uRes.json();
          applyUiState(u);
          if(u && u.activeTab){
            setActiveTab(String(u.activeTab));
          }
        }
      }catch{}
      suppressUiSave = false;
      kickFetchNmhcHistory(deviceId, null, null, true);
      applyKpiFromLatestNmhc(deviceId);
      try{
        const ch = Number(chnEl.value || '0');
        const sRes = await fetch('/api/v1/session/active?deviceId=' + encodeURIComponent(deviceId) + '&channel=' + ch);
        if(sRes.ok){
          const sess = await sRes.json();
          if(sess && sess.channel !== undefined) chnEl.value = String(sess.channel);
          hydrateStreamFromSession(deviceId, Number(chnEl.value||'0'), sess);
          if(sess && sess.result && sess.sessionToken){
            const rk = streamKey(deviceId, Number(chnEl.value||'0'));
            results.set(rk, { result: sess.result, sessionToken: String(sess.sessionToken) });
          }
        }
      }catch{}
      draw();
      renderDebug();
    }

    async function restoreSessionOnly(deviceId){
      if(!deviceId) return;
      try{
        const ch = Number(chnEl.value || '0');
        const sRes = await fetch('/api/v1/session/active?deviceId=' + encodeURIComponent(deviceId) + '&channel=' + ch);
        if(sRes.ok){
          const sess = await sRes.json();
          if(sess && sess.channel !== undefined) chnEl.value = String(sess.channel);
          hydrateStreamFromSession(deviceId, Number(chnEl.value||'0'), sess);
          if(sess && sess.result && sess.sessionToken){
            const rk = streamKey(deviceId, Number(chnEl.value||'0'));
            results.set(rk, { result: sess.result, sessionToken: String(sess.sessionToken) });
          }
        }
      }catch{}
      draw();
      renderDebug();
    }

    async function fetchLastDeviceId(){
      try{
        const r = await fetch('/api/v1/ui');
        if(!r.ok) return;
        const j = await r.json();
        if(j && j.lastDeviceId) backendLastDeviceId = String(j.lastDeviceId);
      }catch{}
    }

    function loadSettings(){
      try{
        const raw = localStorage.getItem('online_monitor_settings');
        if(!raw) return;
        const v = JSON.parse(raw);
        if(v.fullmin !== undefined) fullminEl.value = String(v.fullmin);
        if(v.ylow !== undefined) ylowEl.value = String(v.ylow);
        if(v.yhigh !== undefined) yhighEl.value = String(v.yhigh);
        if(v.autoy !== undefined) autoyEl.checked = !!v.autoy;
        if(v.acqmin !== undefined) acqminEl.value = String(v.acqmin);
        document.getElementById('set-fullmin').value = fullminEl.value;
        document.getElementById('set-ylow').value = ylowEl.value;
        document.getElementById('set-yhigh').value = yhighEl.value;
        document.getElementById('set-autoy').checked = autoyEl.checked;
        if(setAcqMinEl) setAcqMinEl.value = acqminEl.value;
      }catch{}
    }

    document.getElementById('set-save').addEventListener('click', ()=>{
      const fullmin = Number(document.getElementById('set-fullmin').value || '2');
      const ylow = Number(document.getElementById('set-ylow').value || '0');
      const yhigh = Number(document.getElementById('set-yhigh').value || '40');
      const autoy = !!document.getElementById('set-autoy').checked;
      const acqmin = Number((setAcqMinEl && setAcqMinEl.value) ? setAcqMinEl.value : (acqminEl.value || '2'));
      localStorage.setItem('online_monitor_settings', JSON.stringify({fullmin, ylow, yhigh, autoy, acqmin}));
      fullminEl.value = String(isFinite(fullmin) ? fullmin : 2);
      ylowEl.value = String(isFinite(ylow) ? ylow : 0);
      yhighEl.value = String(isFinite(yhigh) ? yhigh : 40);
      autoyEl.checked = autoy;
      if(isFinite(acqmin) && acqmin > 0){
        acqminEl.value = String(acqmin);
        try{ localStorage.setItem(acqMinStorageKey, String(acqmin)); }catch{}
      }
      const m = { carrier: Number(setEpcCarrierEl && setEpcCarrierEl.value || '0'), h2: Number(setEpcH2El && setEpcH2El.value || '1'), air: Number(setEpcAirEl && setEpcAirEl.value || '2') };
      saveEpcMap(m);
      const sel = selectedDevice();
      saveUiToBackend(sel);
      draw();
    });

    setButtonsEnabled(false);
    loadSettings();
    fillEpcSelects(12);
    const initActiveBtn = tabsEl.querySelector('.tab.active');
    if(initActiveBtn && initActiveBtn.dataset && initActiveBtn.dataset.tab){
      currentTab = initActiveBtn.dataset.tab;
    }
    fetchLastDeviceId().finally(()=>{});
    tickClock();
    setInterval(tickClock, 250);
    refreshDevices();
    setInterval(refreshDevices, 1000);
    refreshServer();
    setInterval(refreshServer, 2000);

    if(resExportEl) resExportEl.addEventListener('click', exportResultsCsv);
    if(resDeleteEl) resDeleteEl.addEventListener('click', deleteResultsRange);
    if(resFromEl) resFromEl.addEventListener('change', renderResults);
    if(resToEl) resToEl.addEventListener('change', renderResults);

    if(evtClearEl) evtClearEl.addEventListener('click', ()=>{ evtBuf.splice(0, evtBuf.length); renderEvents(); });
    if(evtOnlySelectedEl) evtOnlySelectedEl.addEventListener('change', renderEvents);

    if(setEpcCarrierEl) setEpcCarrierEl.addEventListener('change', ()=>{ saveEpcMap({ carrier: Number(setEpcCarrierEl.value||'0'), h2: Number(setEpcH2El && setEpcH2El.value || '1'), air: Number(setEpcAirEl && setEpcAirEl.value || '2') }); const sel = selectedDevice(); saveUiToBackend(sel); });
    if(setEpcH2El) setEpcH2El.addEventListener('change', ()=>{ saveEpcMap({ carrier: Number(setEpcCarrierEl && setEpcCarrierEl.value || '0'), h2: Number(setEpcH2El.value||'1'), air: Number(setEpcAirEl && setEpcAirEl.value || '2') }); const sel = selectedDevice(); saveUiToBackend(sel); });
    if(setEpcAirEl) setEpcAirEl.addEventListener('change', ()=>{ saveEpcMap({ carrier: Number(setEpcCarrierEl && setEpcCarrierEl.value || '0'), h2: Number(setEpcH2El && setEpcH2El.value || '1'), air: Number(setEpcAirEl.value||'2') }); const sel = selectedDevice(); saveUiToBackend(sel); });

    const openPlaceholder = (title)=>{
      alert(title + '：待实现');
    };
    if(setOpenMethodEl) setOpenMethodEl.addEventListener('click', ()=>openPlaceholder('方法'));
    if(setOpenProcessingEl) setOpenProcessingEl.addEventListener('click', ()=>openPlaceholder('谱图处理'));
    if(setOpenReportsEl) setOpenReportsEl.addEventListener('click', ()=>openPlaceholder('高级报表'));
  </script>
</body>
</html>`
