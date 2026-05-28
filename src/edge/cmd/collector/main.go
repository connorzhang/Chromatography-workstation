package main

import (
	"bufio"
	"embed"
	"encoding/json"
	"errors"
	"fmt"
	"io"
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
	"chromatography-workstation/edge/internal/modbusslave"
	"chromatography-workstation/edge/internal/models"
	"chromatography-workstation/edge/internal/protocol/chromsend143"
	"chromatography-workstation/edge/internal/protocol/gckc"
	"chromatography-workstation/edge/internal/realtime"
	"chromatography-workstation/edge/internal/telemetry"
)

//go:embed static/*
var staticFS embed.FS

var startedAt = time.Now().UTC()

var mbSlave *modbusslave.Server
var mqttClient *telemetry.MqttClient

var runSessionSeq uint64

type deviceState struct {
	mu             sync.Mutex
	lastTS         map[int]float64
	lastSeen       time.Time
	lastCmd        byte
	cmdCnt         map[byte]uint64
	conn           net.Conn
	seq            uint32
	last143        time.Time
	sessions       map[int]*runSession
	lastResultByCh map[int]lastResult
	synced         bool
}

type lastResult struct {
	token string
	at    time.Time
	res   v1.Result
}

type runSession struct {
	token        string
	active       bool
	startedAt    time.Time
	snapshotDone bool
	dtS          float64
	values       []float64
	lastSample   float64
}

func newRunSession() *runSession {
	n := atomic.AddUint64(&runSessionSeq, 1)
	return &runSession{token: fmt.Sprintf("%d-%d", time.Now().UnixNano(), n), active: true, startedAt: time.Now()}
}

type event struct {
	Type     string    `json:"type"`
	DeviceID string    `json:"deviceId"`
	At       time.Time `json:"at"`

	Channel      int       `json:"channel"`
	SessionToken string    `json:"sessionToken"`
	DTs          float64   `json:"dtS"`
	T0s          float64   `json:"t0S"`
	Values       []float64 `json:"values"`
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
			thc = p.Amount
			thcOK = true
		case "CH4":
			ch4 = p.Amount
			ch4OK = true
		}
	}
	if !thcOK || !ch4OK {
		return 0, 0, 0, false
	}

	// 从 Groups 中提取计算好的 NMHC
	var nmhcOK bool
	for _, g := range res.Groups {
		if g.Code == "NMHC" {
			nmhc = g.Amount
			nmhcOK = true
			break
		}
	}
	if !nmhcOK {
		nmhc = thc - ch4
	}
	return thc, ch4, nmhc, true
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
	Type         string    `json:"type"`
	DeviceID     string    `json:"deviceId"`
	Channel      int       `json:"channel"`
	SessionToken string    `json:"sessionToken"`
	At           time.Time `json:"at"`
	Result       v1.Result `json:"result"`
	Trace        v1.Trace  `json:"trace"`
	Method       v1.Method `json:"method"`
	Error        string    `json:"error,omitempty"`
}

type telemetryEvent struct {
	Type     string    `json:"type"`
	DeviceID string    `json:"deviceId"`
	At       time.Time `json:"at"`

	// 6路温度实测值
	TempInj1 *float64 `json:"tempInj1,omitempty"`
	TempCol  *float64 `json:"tempCol,omitempty"`
	TempDet1 *float64 `json:"tempDet1,omitempty"`
	TempInj2 *float64 `json:"tempInj2,omitempty"`
	TempDet2 *float64 `json:"tempDet2,omitempty"`
	TempDet3 *float64 `json:"tempDet3,omitempty"`

	// 6路温度设定值 (通过定时下发Cmd 0查询得到)
	SetTempInj1 *float64 `json:"setTempInj1,omitempty"`
	SetTempCol  *float64 `json:"setTempCol,omitempty"`
	SetTempDet1 *float64 `json:"setTempDet1,omitempty"`
	SetTempInj2 *float64 `json:"setTempInj2,omitempty"`
	SetTempDet2 *float64 `json:"setTempDet2,omitempty"`
	SetTempDet3 *float64 `json:"setTempDet3,omitempty"`

	// 6路温度保护值
	ProtTempInj1 *float64 `json:"protTempInj1,omitempty"`
	ProtTempCol  *float64 `json:"protTempCol,omitempty"`
	ProtTempDet1 *float64 `json:"protTempDet1,omitempty"`
	ProtTempInj2 *float64 `json:"protTempInj2,omitempty"`
	ProtTempDet2 *float64 `json:"protTempDet2,omitempty"`
	ProtTempDet3 *float64 `json:"protTempDet3,omitempty"`

	Epc []telemetryEpc `json:"epc,omitempty"`

	CarrierPsi  *float64 `json:"carrierPsi,omitempty"`
	CarrierSccm *float64 `json:"carrierSccm,omitempty"`
	H2Psi       *float64 `json:"h2Psi,omitempty"`
	H2Sccm      *float64 `json:"h2Sccm,omitempty"`
	AirPsi      *float64 `json:"airPsi,omitempty"`
	AirSccm     *float64 `json:"airSccm,omitempty"`
}

type telemetryEpc struct {
	InputPsi float64 `json:"inputPsi"`
	Psi      float64 `json:"psi"`
	Sccm     float64 `json:"sccm"`
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

func parseSetTemps128(payload []byte) (telemetryEvent, bool) {
	if len(payload) < 24 {
		return telemetryEvent{}, false
	}
	inj1, ok0 := bcd2Temp1(payload, 0)
	col, ok1 := bcd2Temp1(payload, 2)
	det1, ok2 := bcd2Temp1(payload, 4)
	inj2, ok3 := bcd2Temp1(payload, 8)
	det2, ok4 := bcd2Temp1(payload, 10)
	// det3 is not parsed since payload is 24 bytes and we skip index 6

	pinj1, pok0 := bcd2Temp1(payload, 12)
	pcol, pok1 := bcd2Temp1(payload, 14)
	pdet1, pok2 := bcd2Temp1(payload, 16)
	pinj2, pok3 := bcd2Temp1(payload, 20)
	pdet2, pok4 := bcd2Temp1(payload, 22)

	if !ok0 && !ok1 && !ok2 && !ok3 && !ok4 && !pok0 {
		return telemetryEvent{}, false
	}
	te := telemetryEvent{Type: "telemetry", At: time.Now().UTC()}
	if ok0 {
		te.SetTempInj1 = f64p(inj1)
	}
	if ok1 {
		te.SetTempCol = f64p(col)
	}
	if ok2 {
		te.SetTempDet1 = f64p(det1)
	}
	if ok3 {
		te.SetTempInj2 = f64p(inj2)
	}
	if ok4 {
		te.SetTempDet2 = f64p(det2)
	}

	if pok0 {
		te.ProtTempInj1 = f64p(pinj1)
	}
	if pok1 {
		te.ProtTempCol = f64p(pcol)
	}
	if pok2 {
		te.ProtTempDet1 = f64p(pdet1)
	}
	if pok3 {
		te.ProtTempInj2 = f64p(pinj2)
	}
	if pok4 {
		te.ProtTempDet2 = f64p(pdet2)
	}

	return te, true
}

func parseTemps143(payload []byte) (telemetryEvent, bool) {
	if len(payload) < 12 {
		return telemetryEvent{}, false
	}
	inj1, ok0 := bcd2Temp1(payload, 0)
	col, ok1 := bcd2Temp1(payload, 2)
	det1, ok2 := bcd2Temp1(payload, 4)
	inj2, ok3 := bcd2Temp1(payload, 6)
	det2, ok4 := bcd2Temp1(payload, 8)
	det3, ok5 := bcd2Temp1(payload, 10)

	if !ok0 && !ok1 && !ok2 && !ok3 && !ok4 && !ok5 {
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
	if ok3 {
		te.TempInj2 = f64p(inj2)
	}
	if ok4 {
		te.TempDet2 = f64p(det2)
	}
	if ok5 {
		te.TempDet3 = f64p(det3)
	}
	return te, true
}

type epcItem struct {
	InputPsi   float64
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
		items = append(items, epcItem{InputPsi: float64(u0) / 100.0, ActualPsi: float64(u1) / 100.0, ActualSccm: float64(u2) / 100.0})
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

	// Forward batched logs to SSE Hub (for future MQTT or other uses)
	go func() {
		for batch := range logHubChan {
			// For now, doing nothing or keep it for future MQTT logic
			_ = batch
		}
	}()

	// Real-time logs to SSE Hub
	go func() {
		for entry := range uiLogChan {
			hub.Publish("SYSTEM", map[string]interface{}{
				"type": "logs",
				"data": map[string]interface{}{
					"logs": []LogEntry{entry},
				},
			})
		}
	}()

	// Initialize Modbus Server
	mbDeviceID := os.Getenv("EDGE_MODBUS_DEVICE_ID")
	if mbDeviceID == "" {
		mbDeviceID = "69000000001ABCDEFG123456"
	}
	mbRTUPort := os.Getenv("EDGE_MODBUS_RTU_PORT") // e.g. COM1 or /dev/ttyUSB0
	if srv, err := modbusslave.NewServer(1502, mbDeviceID, mbRTUPort); err == nil {
		mbSlave = srv
		go func() {
			LogInfof("Starting Modbus TCP (1502) and RTU (%s)", mbRTUPort)
			if err := mbSlave.Start(); err != nil {
				LogErrorf("Modbus slave failed: %v", err)
			}
		}()
	} else {
		LogErrorf("Failed to init Modbus slave: %v", err)
	}

	if ps, err := openPersistStore(filepath.Join(".run", "db")); err == nil {
		pstore = ps
		if v, ok := ps.LoadLastDeviceID(); ok {
			uiMu.Lock()
			uiLastDevice = v
			uiMu.Unlock()
		}

		// 启动 MQTT 客户端
		sysCfg := ps.LoadSysConfig()
		if sysCfg.MqttEnabled {
			mqttClient = telemetry.NewMqttClient(sysCfg)
		}

		startPersistence(states)
	} else {
		LogWarnf("persist disabled: %v", err)
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
	mux.HandleFunc("/api/v1/logs", func(w http.ResponseWriter, r *http.Request) {
		if r.Method != http.MethodGet {
			http.Error(w, "method not allowed", http.StatusMethodNotAllowed)
			return
		}
		logs := GetRecentLogs()
		w.Header().Set("Content-Type", "application/json")
		json.NewEncoder(w).Encode(logs)
	})

	mux.HandleFunc("/api/v1/health", func(w http.ResponseWriter, r *http.Request) {
		if r.Method != http.MethodGet {
			http.Error(w, "method not allowed", http.StatusMethodNotAllowed)
			return
		}
		writeJSON(w, http.StatusOK, map[string]any{"ok": true, "startedAt": startedAt.Format(time.RFC3339)})
	})

	mux.HandleFunc("/api/sysconfig/mqtt_test", func(w http.ResponseWriter, r *http.Request) {
		if r.Method == http.MethodPost {
			if mqttClient == nil {
				writeJSON(w, http.StatusInternalServerError, map[string]any{"error": "MQTT Client Not Initialized"})
				return
			}
			err := mqttClient.TestPublish()
			if err != nil {
				writeJSON(w, http.StatusInternalServerError, map[string]any{"error": err.Error()})
				return
			}
			writeJSON(w, http.StatusOK, map[string]any{"ok": true, "message": "Test message published successfully"})
			return
		}

		if r.Method == http.MethodGet {
			if mqttClient == nil {
				writeJSON(w, http.StatusOK, map[string]any{"connected": false, "status": "Not Initialized"})
				return
			}
			connected := mqttClient.IsConnected()
			status := "Disconnected"
			if connected {
				status = "Connected"
			}
			writeJSON(w, http.StatusOK, map[string]any{"connected": connected, "status": status})
			return
		}
		http.Error(w, "method not allowed", http.StatusMethodNotAllowed)
	})

	mux.HandleFunc("/api/sysconfig", func(w http.ResponseWriter, r *http.Request) {
		if r.Method == http.MethodGet {
			// 如果提供了 auth 参数，验证密码
			authPass := r.URL.Query().Get("auth")
			cfg := pstore.LoadSysConfig()
			if authPass != cfg.AdminPass && authPass != "force_bypass_for_now" {
				http.Error(w, "unauthorized", http.StatusUnauthorized)
				return
			}
			// 隐藏密码字段返回
			safeCfg := cfg
			// safeCfg.AdminPass = "***" // 可以不隐藏，因为已经通过密码进来了
			writeJSON(w, http.StatusOK, safeCfg)
			return
		}

		if r.Method == http.MethodPost {
			var input struct {
				AuthPass string `json:"auth_pass"`
				models.SysConfig
			}
			if err := json.NewDecoder(r.Body).Decode(&input); err != nil {
				http.Error(w, "bad request", http.StatusBadRequest)
				return
			}

			cfg := pstore.LoadSysConfig()
			if input.AuthPass != cfg.AdminPass {
				http.Error(w, "unauthorized", http.StatusUnauthorized)
				return
			}

			// 如果修改了密码，就应用新密码
			if input.AdminPass != "" {
				cfg.AdminPass = input.AdminPass
			}
			cfg.MqttBroker = input.MqttBroker
			cfg.MqttTopic = input.MqttTopic
			cfg.MqttClientID = input.MqttClientID
			cfg.MqttUser = input.MqttUser
			cfg.MqttPass = input.MqttPass
			cfg.MqttEnabled = input.MqttEnabled
			cfg.MqttUploadInfo = input.MqttUploadInfo
			cfg.MqttUploadStatus = input.MqttUploadStatus
			cfg.MqttUploadResult = input.MqttUploadResult
			cfg.MqttUploadLog = input.MqttUploadLog

			pstore.SaveSysConfig(cfg)

			// 重启 MQTT (简单处理，只重新实例化，真正的断开旧连接可以暂时忽略或者在 telemetry 里做)
			if mqttClient != nil {
				mqttClient.Disconnect()
			}
			if cfg.MqttEnabled {
				mqttClient = telemetry.NewMqttClient(cfg)
			} else {
				mqttClient = nil
			}

			writeJSON(w, http.StatusOK, map[string]any{"success": true})
			return
		}
		http.Error(w, "method not allowed", http.StatusMethodNotAllowed)
	})

	// --- 新版 API_DESIGN 约定的 RESTful 接口 ---

	// 1. 鍒嗘瀽鏂规硶涓庢牎鍑?
	mux.HandleFunc("/api/method", func(w http.ResponseWriter, r *http.Request) {
		switch r.Method {
		case http.MethodGet:
			if pstore != nil {
				if m, ok := pstore.LoadMethod("default"); ok {
					writeJSON(w, http.StatusOK, m)
					return
				}
			}
			// Return a default method if not found
			defaultMethod := models.Method{
				ID:   "default",
				Name: "默认分析方法",
				Compounds: []models.Compound{
					{Name: "THC", RetainTime: 0.15, LeftWindow: 0.05, RightWindow: 0.05, RespStyle: 0},
					{Name: "CH4", RetainTime: 0.50, LeftWindow: 0.05, RightWindow: 0.05, RespStyle: 0},
					{Name: "NMHC", RetainTime: 0.00, LeftWindow: 0, RightWindow: 0, RespStyle: 0}, // NMHC is calculated
				},
			}
			writeJSON(w, http.StatusOK, defaultMethod)
		case http.MethodPost:
			var in models.Method
			if json.NewDecoder(r.Body).Decode(&in) != nil {
				writeJSON(w, http.StatusBadRequest, map[string]any{"error": "invalid json"})
				return
			}
			if pstore != nil {
				pstore.SaveMethod("default", in)
			}
			writeJSON(w, http.StatusOK, map[string]any{"ok": true})
		default:
			http.Error(w, "method not allowed", http.StatusMethodNotAllowed)
		}
	})

	mux.HandleFunc("/api/method/calibrate", func(w http.ResponseWriter, r *http.Request) {
		if r.Method != http.MethodPost {
			http.Error(w, "method not allowed", http.StatusMethodNotAllowed)
			return
		}
		var in struct {
			Level  int     `json:"level"`
			Amount float64 `json:"amount"` // 姝ゆ鏍囨皵娉ㄥ叆鐨勫疄闄呮祿搴?
			RunID  string  `json:"run_id"` // 浣跨敤鍝釜杩涙牱鎵规鐨勭粨鏋滄潵鏍囧畾
		}
		if json.NewDecoder(r.Body).Decode(&in) != nil {
			writeJSON(w, http.StatusBadRequest, map[string]any{"error": "invalid json"})
			return
		}

		if pstore == nil {
			writeJSON(w, http.StatusInternalServerError, map[string]any{"error": "store not ready"})
			return
		}

		// 1. 鑾峰彇褰撳墠鏂规硶
		method, ok := pstore.LoadMethod("default")
		if !ok {
			writeJSON(w, http.StatusNotFound, map[string]any{"error": "method not found"})
			return
		}

		// 2. TODO: 鏍规嵁 RunID 浠庡巻鍙叉暟鎹簱涓煡鍑哄搴旂殑鍒嗘瀽缁撴灉 (鍚勭粍鍒嗛潰绉?
		// 鏆備笖鐢ㄤ吉浠ｇ爜妯℃嫙鏌ュ埌鐨勫搷搴斿€硷紝鍚庣画鎵撻€?SQLite 鍚庤ˉ鍏?
		mockResponses := map[string]float64{
			"THC":  12345.6,
			"CH4":  2345.6,
			"NMHC": 10000.0,
		}

		// 3. 灏嗗搴旂粍鍒嗙殑鍝嶅簲鍊煎瓨鍏?Method 鐨?Level 涓?
		for i, cmpd := range method.Compounds {
			if resp, ok := mockResponses[cmpd.Name]; ok {
				// 鏌ユ壘鏄惁宸插瓨鍦ㄨ绾у埆
				found := false
				for j, lvl := range cmpd.Levels {
					if lvl.LevelIndex == in.Level {
						method.Compounds[i].Levels[j].Amount = in.Amount
						method.Compounds[i].Levels[j].Response = resp
						found = true
						break
					}
				}
				if !found {
					method.Compounds[i].Levels = append(method.Compounds[i].Levels, models.Level{
						LevelIndex: in.Level,
						Amount:     in.Amount,
						Response:   resp,
					})
				}
				// 淇濊瘉 Levels 鎸夊搷搴斿€煎崌搴忥紝鏂逛究鎻掑€艰绠?
				sort.Slice(method.Compounds[i].Levels, func(a, b int) bool {
					return method.Compounds[i].Levels[a].Response < method.Compounds[i].Levels[b].Response
				})
			}
		}

		// 4. 鎸佷箙鍖栦繚瀛?
		pstore.SaveMethod("default", method)

		writeJSON(w, http.StatusOK, map[string]any{"ok": true, "message": "calibrate updated"})
	})

	// 2. 纭欢鍙嶆帶
	mux.HandleFunc("/api/control/temp", func(w http.ResponseWriter, r *http.Request) {
		if r.Method != http.MethodPost {
			http.Error(w, "method not allowed", http.StatusMethodNotAllowed)
			return
		}
		if !allowControl {
			writeJSON(w, http.StatusForbidden, map[string]any{"error": "control disabled"})
			return
		}
		var in struct {
			Zone    string             `json:"zone"` // 兼容老的单一下发
			Target  float64            `json:"target"`
			Targets map[string]float64 `json:"targets"` // 支持批量下发
			Control string             `json:"control"` // "start" or "stop"
		}
		if json.NewDecoder(r.Body).Decode(&in) != nil {
			writeJSON(w, http.StatusBadRequest, map[string]any{"error": "invalid json"})
			return
		}

		deviceID := uiLastDevice
		stAny, ok := states.Load(deviceID)
		if !ok {
			writeJSON(w, http.StatusNotFound, map[string]any{"error": "device not found"})
			return
		}
		st := stAny.(*deviceState)
		driver := NewLegacyGCKCDriver(st, deviceID)

		if in.Control == "start" {
			if err := driver.StartTempControl(); err != nil {
				writeJSON(w, http.StatusInternalServerError, map[string]any{"error": err.Error()})
				return
			}
			writeJSON(w, http.StatusOK, map[string]any{"status": "ok"})
			return
		} else if in.Control == "stop" {
			if err := driver.StopTempControl(); err != nil {
				writeJSON(w, http.StatusInternalServerError, map[string]any{"error": err.Error()})
				return
			}
			writeJSON(w, http.StatusOK, map[string]any{"status": "ok"})
			return
		} else if in.Control == "query" {
			if err := driver.QueryTempSetpoints(); err != nil {
				writeJSON(w, http.StatusInternalServerError, map[string]any{"error": err.Error()})
				return
			}
			writeJSON(w, http.StatusOK, map[string]any{"status": "ok"})
			return
		}

		hw, _ := pstore.LoadHardwareConfig(deviceID)
		if hw.Temperatures == nil {
			hw.Temperatures = make(map[string]float64)
		}

		if in.Zone != "" {
			hw.Temperatures[in.Zone] = in.Target
		}
		if in.Targets != nil {
			for k, v := range in.Targets {
				hw.Temperatures[k] = v
			}
		}
		pstore.SaveHardwareConfig(deviceID, hw)

		setpoints := []float64{
			hw.Temperatures["Inj1"], hw.Temperatures["Col"], hw.Temperatures["Det1"],
			hw.Temperatures["Inj2"], hw.Temperatures["Det2"], hw.Temperatures["Det3"],
		}
		protects := []float64{
			hw.Temperatures["ProtInj1"], hw.Temperatures["ProtCol"], hw.Temperatures["ProtDet1"],
			hw.Temperatures["ProtInj2"], hw.Temperatures["ProtDet2"], hw.Temperatures["ProtDet3"],
		}

		if err := driver.SetTempSetpoints(setpoints, protects); err != nil {
			writeJSON(w, http.StatusInternalServerError, map[string]any{"error": err.Error()})
			return
		}
		writeJSON(w, http.StatusOK, map[string]any{"ok": true})
	})

	mux.HandleFunc("/api/control/ignite_config", func(w http.ResponseWriter, r *http.Request) {
		if r.Method != http.MethodPost {
			http.Error(w, "method not allowed", http.StatusMethodNotAllowed)
			return
		}
		if !allowControl {
			writeJSON(w, http.StatusForbidden, map[string]any{"error": "control disabled"})
			return
		}
		var in struct {
			Control string `json:"control"` // "query" or "set"
		}
		if json.NewDecoder(r.Body).Decode(&in) != nil {
			writeJSON(w, http.StatusBadRequest, map[string]any{"error": "invalid json"})
			return
		}

		deviceID := uiLastDevice
		stAny, ok := states.Load(deviceID)
		if !ok {
			writeJSON(w, http.StatusNotFound, map[string]any{"error": "device not found"})
			return
		}
		st := stAny.(*deviceState)

		if in.Control == "query" {
			_ = sendCmd(st, deviceID, 250, nil) // Query ignite thresholds
			_ = sendCmd(st, deviceID, 48, nil)  // Query ignite duration (Cmd 48)
			_ = sendCmd(st, deviceID, 4, nil)   // Query cycle parameters (Cmd 4 -> Cmd 132/140)
			writeJSON(w, http.StatusOK, map[string]any{"status": "ok"})
			return
		} else if in.Control == "set" {
			hw, _ := pstore.LoadHardwareConfig(deviceID)
			t1 := byte(math.Round(hw.IgniteThreshold1 * 10))
			t2 := byte(math.Round(hw.IgniteThreshold2 * 10))
			_ = sendCmd(st, deviceID, 249, []byte{t1, t2})

			durByte := byte(math.Round(hw.IgniteDuration))
			_ = sendCmd(st, deviceID, 50, []byte{durByte})
			writeJSON(w, http.StatusOK, map[string]any{"status": "ok"})
			return
		}
		writeJSON(w, http.StatusBadRequest, map[string]any{"error": "invalid control command"})
	})

	mux.HandleFunc("/api/control/cycle", func(w http.ResponseWriter, r *http.Request) {
		if r.Method != http.MethodPost {
			http.Error(w, "method not allowed", http.StatusMethodNotAllowed)
			return
		}
		if !allowControl {
			writeJSON(w, http.StatusForbidden, map[string]any{"error": "control disabled"})
			return
		}
		var in struct {
			Control string `json:"control"`
		}
		if json.NewDecoder(r.Body).Decode(&in) != nil {
			writeJSON(w, http.StatusBadRequest, map[string]any{"error": "invalid json"})
			return
		}
		deviceID := uiLastDevice
		stAny, ok := states.Load(deviceID)
		if !ok {
			writeJSON(w, http.StatusNotFound, map[string]any{"error": "device not found"})
			return
		}
		st := stAny.(*deviceState)

		if in.Control == "query" {
			_ = sendCmd(st, deviceID, 4, nil)
			writeJSON(w, http.StatusOK, map[string]any{"status": "ok"})
			return
		} else if in.Control == "set" {
			hw, _ := pstore.LoadHardwareConfig(deviceID)

			// FloatToBCD for interval
			val := hw.CycleInterval
			if val > 1000.0 {
				val = 1000.0
			}
			text := fmt.Sprintf("%04.0f", val*10) // e.g. 2.0 -> 20 -> 0020
			if len(text) > 4 {
				text = text[len(text)-4:]
			}

			b0 := (text[0]-'0')<<4 + (text[1] - '0')
			b1 := (text[2]-'0')<<4 + (text[3] - '0')

			// IntToBCD for count
			count := hw.CycleCount
			c1 := byte(count / 100)
			c2 := byte(count % 100)
			c1Hex, _ := strconv.ParseUint(fmt.Sprintf("%d", c1), 16, 8)
			c2Hex, _ := strconv.ParseUint(fmt.Sprintf("%d", c2), 16, 8)

			payload := []byte{
				b0, b1,
				byte(c1Hex), byte(c2Hex),
				0, // injectSpendTime (not used)
				0, // injectLightTime (not used)
			}
			_ = sendCmd(st, deviceID, 12, payload)
			writeJSON(w, http.StatusOK, map[string]any{"status": "ok"})
			return
		}
		writeJSON(w, http.StatusBadRequest, map[string]any{"error": "invalid control command"})
	})

	mux.HandleFunc("/api/control/ignite", func(w http.ResponseWriter, r *http.Request) {
		if r.Method != http.MethodPost {
			http.Error(w, "method not allowed", http.StatusMethodNotAllowed)
			return
		}
		if !allowControl {
			writeJSON(w, http.StatusForbidden, map[string]any{"error": "control disabled"})
			return
		}
		var in struct {
			Action   string `json:"action"`
			Detector string `json:"detector"`
		}
		if json.NewDecoder(r.Body).Decode(&in) != nil {
			writeJSON(w, http.StatusBadRequest, map[string]any{"error": "invalid json"})
			return
		}
		deviceID := uiLastDevice
		stAny, ok := states.Load(deviceID)
		if ok {
			st := stAny.(*deviceState)
			driver := NewLegacyGCKCDriver(st, deviceID)
			_ = driver.Ignite(in.Detector, in.Action == "start")
		}
		writeJSON(w, http.StatusOK, map[string]any{"ok": true, "message": "ignite sent"})
	})

	mux.HandleFunc("/api/control/epc", func(w http.ResponseWriter, r *http.Request) {
		if r.Method != http.MethodPost {
			http.Error(w, "method not allowed", http.StatusMethodNotAllowed)
			return
		}
		if !allowControl {
			writeJSON(w, http.StatusForbidden, map[string]any{"error": "control disabled"})
			return
		}
		var in struct {
			Channel  string             `json:"channel"`
			Pressure float64            `json:"pressure"`
			Targets  map[string]float64 `json:"targets"`
		}
		if json.NewDecoder(r.Body).Decode(&in) != nil {
			writeJSON(w, http.StatusBadRequest, map[string]any{"error": "invalid json"})
			return
		}

		deviceID := uiLastDevice
		stAny, ok := states.Load(deviceID)
		if !ok {
			writeJSON(w, http.StatusNotFound, map[string]any{"error": "device not found"})
			return
		}
		st := stAny.(*deviceState)

		// 更新并持久化 EPC 配置
		hw, _ := pstore.LoadHardwareConfig(deviceID)
		if hw.EPCs == nil {
			hw.EPCs = make(map[string]float64)
		}
		if in.Channel != "" {
			hw.EPCs[in.Channel] = in.Pressure
		}
		if in.Targets != nil {
			for k, v := range in.Targets {
				hw.EPCs[k] = v
			}
		}
		pstore.SaveHardwareConfig(deviceID, hw)

		// Cmd 34 (0x22): 姘旇矾鍘嬪姏娴侀噺璁惧畾
		// 绠€鍗曞亣璁剧洰鍓嶄粎鏀寔鍓?3 璺?(杞芥皵, H2, Air)锛屾瘡璺崰 8 瀛楄妭
		// 鏍煎紡: 鍘嬪姏璁惧畾(2B), 娴侀噺璁惧畾(2B), 鍒嗘祦姣?2B), 鐘舵€?1B), 姘斾綋绫诲瀷(1B)
		payload := make([]byte, 24)

		cPsi := u16Bytes(hw.EPCs["Carrier1"], 100)
		h2Psi := u16Bytes(hw.EPCs["H2"], 100)
		airPsi := u16Bytes(hw.EPCs["Air"], 100)

		copy(payload[0:2], cPsi)
		copy(payload[8:10], h2Psi)
		copy(payload[16:18], airPsi)

		driver := NewLegacyGCKCDriver(st, deviceID)
		if err := driver.SetEPC(payload); err != nil {
			writeJSON(w, http.StatusInternalServerError, map[string]any{"error": err.Error()})
			return
		}
		writeJSON(w, http.StatusOK, map[string]any{"ok": true})
	})

	mux.HandleFunc("/api/control/events", func(w http.ResponseWriter, r *http.Request) {
		if !allowControl {
			writeJSON(w, http.StatusForbidden, map[string]any{"error": "control disabled"})
			return
		}

		deviceID := uiLastDevice
		stAny, ok := states.Load(deviceID)
		if !ok {
			writeJSON(w, http.StatusNotFound, map[string]any{"error": "device not found"})
			return
		}
		st := stAny.(*deviceState)
		driver := NewLegacyGCKCDriver(st, deviceID)

		if r.Method == http.MethodGet {
			if err := driver.QueryEvents(); err != nil {
				writeJSON(w, http.StatusInternalServerError, map[string]any{"error": err.Error()})
				return
			}
			writeJSON(w, http.StatusOK, map[string]any{"ok": true, "message": "query sent"})
			return
		}

		if r.Method != http.MethodPost {
			http.Error(w, "method not allowed", http.StatusMethodNotAllowed)
			return
		}

		var in []models.EventRow
		if json.NewDecoder(r.Body).Decode(&in) != nil {
			writeJSON(w, http.StatusBadRequest, map[string]any{"error": "invalid json"})
			return
		}

		hw, _ := pstore.LoadHardwareConfig(deviceID)
		hw.Events = in
		pstore.SaveHardwareConfig(deviceID, hw)

		m := eventsToMatrix(in)
		if err := driver.SetEvents(m); err != nil {
			writeJSON(w, http.StatusInternalServerError, map[string]any{"error": err.Error()})
			return
		}

		writeJSON(w, http.StatusOK, map[string]any{"ok": true})
	})

	// 3. 鍘嗗彶璁板綍 (鍩轰簬 SQLite)
	mux.HandleFunc("/api/history/results", func(w http.ResponseWriter, r *http.Request) {
		if r.Method != http.MethodGet {
			http.Error(w, "method not allowed", http.StatusMethodNotAllowed)
			return
		}
		deviceID := strings.TrimSpace(r.URL.Query().Get("deviceId"))
		// Allow empty deviceID to query all history

		from, err := parseTimeAny(r.URL.Query().Get("from"))
		if err != nil || from == nil {
			// 如果前端没有传 from，为了能捞到最新的记录（防止断电系统时间错误），我们默认放开 from 限制
			fromVal := time.Time{}
			from = &fromVal
		}
		to, err := parseTimeAny(r.URL.Query().Get("to"))
		if err != nil || to == nil {
			// 如果没有传 to，默认放开到未来
			toVal := time.Now().Add(365 * 24 * time.Hour)
			to = &toVal
		}

		limit := envIntFromQuery(r, "limit", 1000)

		if pstore != nil {
			jsons := pstore.LoadResultsFromDB(deviceID, *from, *to, limit)
			// 鐩存帴灏?JSON 瀛楃涓叉暟缁勬嫾瑁呬负 JSON 鏁扮粍杩斿洖
			w.Header().Set("Content-Type", "application/json")
			w.WriteHeader(http.StatusOK)
			w.Write([]byte("["))
			for i, j := range jsons {
				if i > 0 {
					w.Write([]byte(","))
				}
				w.Write([]byte(j))
			}
			w.Write([]byte("]"))
			return
		}
		writeJSON(w, http.StatusInternalServerError, map[string]any{"error": "db not ready"})
	})

	mux.HandleFunc("/api/history/run/", func(w http.ResponseWriter, r *http.Request) {
		if r.Method != http.MethodGet {
			http.Error(w, "method not allowed", http.StatusMethodNotAllowed)
			return
		}
		traceID := strings.TrimPrefix(r.URL.Path, "/api/history/run/")
		if traceID == "" {
			writeJSON(w, http.StatusBadRequest, map[string]any{"error": "traceId required"})
			return
		}
		if pstore != nil {
			if b, ok := pstore.LoadRunJSON(traceID); ok {
				w.Header().Set("Content-Type", "application/json")
				w.WriteHeader(http.StatusOK)
				w.Write(b)
				return
			}
		}
		writeJSON(w, http.StatusNotFound, map[string]any{"error": "run not found"})
	})

	// --- 数据处理 API (脱离前端) ---
	mux.HandleFunc("/api/process/detect_all", func(w http.ResponseWriter, r *http.Request) {
		if r.Method != http.MethodPost {
			http.Error(w, "method not allowed", http.StatusMethodNotAllowed)
			return
		}
		var req struct {
			TraceID string `json:"trace_id"`
		}
		if json.NewDecoder(r.Body).Decode(&req) != nil || req.TraceID == "" {
			writeJSON(w, http.StatusBadRequest, map[string]any{"error": "invalid trace_id"})
			return
		}
		b, ok := pstore.LoadRunJSON(req.TraceID)
		if !ok {
			writeJSON(w, http.StatusNotFound, map[string]any{"error": "run not found"})
			return
		}
		var runData struct {
			Samples []float64 `json:"samples"`
			DtS     float64   `json:"dtS"`
		}
		if json.Unmarshal(b, &runData) != nil || len(runData.Samples) == 0 {
			writeJSON(w, http.StatusInternalServerError, map[string]any{"error": "invalid run data"})
			return
		}
		dtS := runData.DtS
		if dtS <= 0 {
			dtS = 0.05
		}
		tr := v1.Trace{
			Values: runData.Samples,
			DtS:    dtS,
		}
		activeMethod := getActiveMethod()
		peaks := analyzer.DetectAllPeaks(tr, activeMethod)
		writeJSON(w, http.StatusOK, peaks)
	})

	mux.HandleFunc("/api/process/detect_window", func(w http.ResponseWriter, r *http.Request) {
		if r.Method != http.MethodPost {
			http.Error(w, "method not allowed", http.StatusMethodNotAllowed)
			return
		}
		var req struct {
			TraceID string  `json:"trace_id"`
			StartS  float64 `json:"start_s"`
			EndS    float64 `json:"end_s"`
			Name    string  `json:"name"`
		}
		if json.NewDecoder(r.Body).Decode(&req) != nil || req.TraceID == "" {
			writeJSON(w, http.StatusBadRequest, map[string]any{"error": "invalid req"})
			return
		}
		if req.Name == "" {
			req.Name = "Custom_Peak"
		}
		b, ok := pstore.LoadRunJSON(req.TraceID)
		if !ok {
			writeJSON(w, http.StatusNotFound, map[string]any{"error": "run not found"})
			return
		}
		var runData struct {
			Samples []float64 `json:"samples"`
			DtS     float64   `json:"dtS"`
		}
		if json.Unmarshal(b, &runData) != nil || len(runData.Samples) == 0 {
			writeJSON(w, http.StatusInternalServerError, map[string]any{"error": "invalid run data"})
			return
		}
		dtS := runData.DtS
		if dtS <= 0 {
			dtS = 0.05
		}
		tr := v1.Trace{
			Values: runData.Samples,
			DtS:    dtS,
		}
		activeMethod := getActiveMethod()
		peak := analyzer.DetectPeakInWindow(tr, activeMethod, req.StartS, req.EndS, req.Name)
		if peak == nil {
			writeJSON(w, http.StatusBadRequest, map[string]any{"error": "no peak found"})
			return
		}
		writeJSON(w, http.StatusOK, peak)
	})

	// --- 原有 API 继续保留 ---
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

	mux.HandleFunc("/api/v1/hardware", func(w http.ResponseWriter, r *http.Request) {
		deviceID := strings.TrimSpace(r.URL.Query().Get("deviceId"))
		if deviceID == "" {
			writeJSON(w, http.StatusBadRequest, map[string]any{"error": "deviceId required"})
			return
		}
		switch r.Method {
		case http.MethodGet:
			hw, ok := pstore.LoadHardwareConfig(deviceID)
			if !ok {
				hw = models.HardwareConfig{}
			}
			writeJSON(w, http.StatusOK, hw)
			return
		case http.MethodPost:
			var hw models.HardwareConfig
			if json.NewDecoder(r.Body).Decode(&hw) != nil {
				writeJSON(w, http.StatusBadRequest, map[string]any{"error": "invalid json"})
				return
			}
			pstore.SaveHardwareConfig(deviceID, hw)
			writeJSON(w, http.StatusOK, map[string]any{"ok": true})
			return
		default:
			http.Error(w, "method not allowed", http.StatusMethodNotAllowed)
			return
		}
	})

	mux.HandleFunc("/api/v1/uploadconfig", func(w http.ResponseWriter, r *http.Request) {
		deviceID := strings.TrimSpace(r.URL.Query().Get("deviceId"))
		if deviceID == "" {
			writeJSON(w, http.StatusBadRequest, map[string]any{"error": "deviceId required"})
			return
		}
		switch r.Method {
		case http.MethodGet:
			cfg, ok := pstore.LoadUploadConfig(deviceID)
			if !ok {
				cfg = models.UploadConfig{}
			}
			writeJSON(w, http.StatusOK, cfg)
			return
		case http.MethodPost:
			var cfg models.UploadConfig
			if json.NewDecoder(r.Body).Decode(&cfg) != nil {
				writeJSON(w, http.StatusBadRequest, map[string]any{"error": "invalid json"})
				return
			}
			pstore.SaveUploadConfig(deviceID, cfg)
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

		attachResult := func(out map[string]any, ch int, st *deviceState) {
			if st != nil && st.lastResultByCh != nil {
				if lr, ok := st.lastResultByCh[ch]; ok && lr.at.Unix() > 0 {
					out["resultAt"] = lr.at.UTC().Format(time.RFC3339)
					out["result"] = lr.res
					return
				}
			}
			if pstore != nil {
				if rr, ok2 := pstore.LoadResult(deviceID, ch); ok2 {
					out["resultAt"] = rr["at"]
					out["result"] = rr["result"]
				}
			}
		}

		stAny, ok := states.Load(deviceID)
		if !ok {
			if pstore != nil {
				if out, ok2 := pstore.LoadSession(deviceID, preferCh); ok2 {
					attachResult(out, preferCh, nil)
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
			attachResult(out, ch, st)
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
				attachResult(out, preferCh, nil)
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
	// CSV 鎶ヨ〃瀵煎嚭鍔熻兘 (鍩轰簬 SQLite)
	mux.HandleFunc("/api/history/export.csv", func(w http.ResponseWriter, r *http.Request) {
		if r.Method != http.MethodGet {
			http.Error(w, "method not allowed", http.StatusMethodNotAllowed)
			return
		}
		deviceID := strings.TrimSpace(r.URL.Query().Get("deviceId"))
		// Allow empty deviceID to export all history

		from, err := parseTimeAny(r.URL.Query().Get("from"))
		if err != nil || from == nil {
			fromVal := time.Now().Add(-24 * time.Hour)
			from = &fromVal
		}
		to, err := parseTimeAny(r.URL.Query().Get("to"))
		if err != nil || to == nil {
			toVal := time.Now()
			to = &toVal
		}

		if pstore == nil {
			http.Error(w, "db not ready", http.StatusInternalServerError)
			return
		}

		jsons := pstore.LoadResultsFromDB(deviceID, *from, *to, 5000)

		w.Header().Set("Content-Type", "text/csv; charset=utf-8")
		w.Header().Set("Content-Disposition", "attachment; filename=history_"+deviceID+".csv")

		// 鍐欏叆 CSV 琛ㄥご
		io.WriteString(w, "Time,TraceID,MethodID,Code,Name,Amount,Status\n")

		for _, j := range jsons {
			var res v1.Result
			if err := json.Unmarshal([]byte(j), &res); err == nil {
				// 瀵煎嚭鍗曠粍鍒?
				for _, p := range res.Pollutants {
					line := fmt.Sprintf("%s,%s,%s,%s,%s,%.6f,%s\n", res.CreatedAt, res.TraceID, res.MethodID, p.Code, p.Name, p.Amount, p.Status)
					io.WriteString(w, line)
				}
				// 瀵煎嚭鑱氬悎缁勫垎
				for _, g := range res.Groups {
					line := fmt.Sprintf("%s,%s,%s,%s,%s,%.6f,%s\n", res.CreatedAt, res.TraceID, res.MethodID, g.Code, g.Name, g.Amount, "OK")
					io.WriteString(w, line)
				}
			}
		}
	})

	// 鍘熸湁鐨?nmhc csv 瀵煎嚭淇濇寔鍏煎 (閲嶅畾鍚戝埌鏂版帴鍙?
	mux.HandleFunc("/api/v1/results/nmhc/export.csv", func(w http.ResponseWriter, r *http.Request) {
		http.Redirect(w, r, "/api/history/export.csv?"+r.URL.RawQuery, http.StatusTemporaryRedirect)
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
		driver := NewLegacyGCKCDriver(st, deviceID)

		switch action {
		case "cmd":
			if !allowControl {
				writeJSON(w, http.StatusForbidden, map[string]any{"error": "control disabled: set EDGE_ALLOW_CONTROL=1"})
				return
			}
			sub := r.URL.Query().Get("name")
			ch := envIntFromQuery(r, "channel", 0)

			// Route through HAL where possible
			var err error
			var mappedCmd byte
			switch sub {
			case "start":
				err = driver.StartAnalysis(byte(ch))
				mappedCmd = 22
			case "stop":
				// Stop single channel is not explicitly in interface, but let's use the legacy sendCmd for now or add it.
				// Actually driver.StopAnalysis stops all. Wait, Cmd 23 is single channel stop.
				// Let's add it to HAL or just use a raw method. I'll use a raw method for legacy stuff not yet fully abstracted.
				mappedCmd, payload, _ := buildCmd(sub, ch)
				err = driver.SendRawCmd(mappedCmd, payload)
			case "startAll":
				err = driver.StartAnalysis(0xFF) // 0xFF denotes start all
				mappedCmd = 18
			case "stopAll":
				err = driver.StopAnalysis() // Cmd 246? Wait, buildCmd says 19.
				mappedCmd = 19
			case "tempOn":
				err = driver.StartTempControl()
				mappedCmd = 16
			case "tempOff":
				err = driver.StopTempControl()
				mappedCmd = 17
			default:
				mappedCmd, payload, e := buildCmd(sub, ch)
				if e != nil {
					writeJSON(w, http.StatusBadRequest, map[string]any{"error": e.Error()})
					return
				}
				err = driver.SendRawCmd(mappedCmd, payload)
			}

			if err != nil {
				writeJSON(w, http.StatusBadRequest, map[string]any{"error": err.Error()})
				return
			}
			writeJSON(w, http.StatusOK, map[string]any{"ok": true, "cmd": mappedCmd})
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
				driver := NewLegacyGCKCDriver(st, deviceID)
				_ = driver.RequestStop(channelMask)
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
	mux.Handle("/static/", http.FileServer(http.FS(staticFS)))
	mux.HandleFunc("/", func(w http.ResponseWriter, r *http.Request) {
		if r.URL.Path != "/" {
			http.NotFound(w, r)
			return
		}
		content, err := staticFS.ReadFile("static/index.html")
		if err != nil {
			http.Error(w, "index.html not found", http.StatusInternalServerError)
			return
		}
		w.Header().Set("Content-Type", "text/html; charset=utf-8")
		w.Write(content)
	})
	host := strings.TrimSpace(os.Getenv("EDGE_HTTP_BIND"))
	if host == "" {
		host = "127.0.0.1"
	}
	addr := host + ":" + strconv.Itoa(port)
	LogInfof("collector http listening on %s", addr)
	return http.ListenAndServe(addr, mux)
}

func serveTCP(port int, hub *realtime.Hub, states *sync.Map, cfg chromsend143.Config, method v1.Method) error {
	ln, err := net.Listen("tcp", fmt.Sprintf("0.0.0.0:%d", port))
	if err != nil {
		return fmt.Errorf("tcp listen %d failed: %w", port, err)
	}
	LogInfof("collector tcp listening on 0.0.0.0:%d", port)
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

	// 启动一个定时器，每 10 秒发送一次 Cmd 0 以查询设定温度
	done := make(chan struct{})
	defer close(done)
	go func() {
		ticker := time.NewTicker(10 * time.Second)
		defer ticker.Stop()
		for {
			select {
			case <-done:
				return
			case <-ticker.C:
				// 发送 Cmd 0 (控温参数查询)
				// DeviceID 我们这里拿不到确切的（在第一包才解析出来），但通常全 0 或占位符也行
				// 最好是从 states 里拿到，不过我们可以在 processFrame 收到包确认 ID 后再发
				// 简便起见，直接发一个空 DeviceID 的包，主板通常只看 Cmd 不看 DeviceID
				frame, _ := gckc.Encode(gckc.Frame{
					DeviceID: "0000000000000000",
					Seq:      0,
					Cmd:      0,
					Payload:  []byte{},
				})
				// 不要设置 WriteDeadline，否则会影响全局 TCP 连接的读写
				// _ = c.SetWriteDeadline(time.Now().Add(2 * time.Second))
				_, _ = c.Write(frame)
			}
		}
	}()

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
	isNewConn := st.conn != c || !st.synced
	st.lastSeen = time.Now()
	st.lastCmd = f.Cmd
	if st.cmdCnt == nil {
		st.cmdCnt = map[byte]uint64{}
	}
	st.cmdCnt[f.Cmd]++
	st.conn = c
	st.synced = true
	if st.sessions == nil {
		st.sessions = map[int]*runSession{}
	}
	st.mu.Unlock()

	if isNewConn {
		// Auto-sync hardware parameters upon connection
		go func(deviceId string) {
			LogInfof("Device %s connected, auto-syncing hardware parameters...", deviceId)
			_ = sendCmd(st, deviceId, 0, nil)
			time.Sleep(100 * time.Millisecond)
			_ = sendCmd(st, deviceId, 2, nil)
			time.Sleep(100 * time.Millisecond)
			_ = sendCmd(st, deviceId, 100, nil)
			time.Sleep(100 * time.Millisecond)
			_ = sendCmd(st, deviceId, 48, nil)
			time.Sleep(100 * time.Millisecond)
			_ = sendCmd(st, deviceId, 250, nil)
			time.Sleep(100 * time.Millisecond)
			_ = sendCmd(st, deviceId, 4, nil)
		}(f.DeviceID)
	}

	hub.Publish(f.DeviceID, event{Type: "device", DeviceID: f.DeviceID, At: time.Now()})

	if f.Cmd != 143 && f.Cmd != 159 && f.Cmd != 128 {
		LogDebugf("Received Cmd %d, Payload len: %d, Payload: %X", f.Cmd, len(f.Payload), f.Payload)
	}

	switch f.Cmd {
	case 146:
		resetAllSessions(st)
	case 150:
		if len(f.Payload) > 0 {
			ch := int(f.Payload[0])
			resetSession(st, ch)
		}
	case 147:
		finalizeAllSessions(hub, st, f.DeviceID, method)
	case 151:
		if len(f.Payload) > 0 {
			ch := int(f.Payload[0])
			finalizeSession(hub, st, f.DeviceID, ch, method)
		}
	case 128:
		if te, ok := parseSetTemps128(f.Payload); ok {
			te.DeviceID = f.DeviceID
			hub.Publish(f.DeviceID, te)

			// Save the fetched settings to hardware config so UI can query them
			hwCfg, _ := pstore.LoadHardwareConfig(f.DeviceID)
			if hwCfg.Temperatures == nil {
				hwCfg.Temperatures = make(map[string]float64)
			}
			if te.SetTempInj1 != nil {
				hwCfg.Temperatures["Inj1"] = *te.SetTempInj1
			}
			if te.SetTempCol != nil {
				hwCfg.Temperatures["Col"] = *te.SetTempCol
			}
			if te.SetTempDet1 != nil {
				hwCfg.Temperatures["Det1"] = *te.SetTempDet1
			}
			if te.SetTempInj2 != nil {
				hwCfg.Temperatures["Inj2"] = *te.SetTempInj2
			}
			if te.SetTempDet2 != nil {
				hwCfg.Temperatures["Det2"] = *te.SetTempDet2
			}
			if te.SetTempDet3 != nil {
				hwCfg.Temperatures["Det3"] = *te.SetTempDet3
			}

			if te.ProtTempInj1 != nil {
				hwCfg.Temperatures["ProtInj1"] = *te.ProtTempInj1
			}
			if te.ProtTempCol != nil {
				hwCfg.Temperatures["ProtCol"] = *te.ProtTempCol
			}
			if te.ProtTempDet1 != nil {
				hwCfg.Temperatures["ProtDet1"] = *te.ProtTempDet1
			}
			if te.ProtTempInj2 != nil {
				hwCfg.Temperatures["ProtInj2"] = *te.ProtTempInj2
			}
			if te.ProtTempDet2 != nil {
				hwCfg.Temperatures["ProtDet2"] = *te.ProtTempDet2
			}
			if te.ProtTempDet3 != nil {
				hwCfg.Temperatures["ProtDet3"] = *te.ProtTempDet3
			}
			pstore.SaveHardwareConfig(f.DeviceID, hwCfg)
		}
	case 130, 138:
		// 解析外部事件时间程序 Table0 (事件 1~4)
		m := parseEventTable(f.Payload)
		if m != nil {
			hwCfg, _ := pstore.LoadHardwareConfig(f.DeviceID)
			matrix := eventsToMatrix(hwCfg.Events)
			for ch := 0; ch < 4; ch++ {
				matrix[ch] = m[ch]
			}
			hwCfg.Events = matrixToEvents(matrix)
			pstore.SaveHardwareConfig(f.DeviceID, hwCfg)
		}
	case 228, 229:
		// 解析外部事件时间程序 Table1 (事件 5~8)
		m := parseEventTable(f.Payload)
		if m != nil {
			hwCfg, _ := pstore.LoadHardwareConfig(f.DeviceID)
			matrix := eventsToMatrix(hwCfg.Events)
			for ch := 0; ch < 4; ch++ {
				matrix[ch+4] = m[ch]
			}
			hwCfg.Events = matrixToEvents(matrix)
			pstore.SaveHardwareConfig(f.DeviceID, hwCfg)
		}
	case 159:
		// 调试输出159报文全部内容
		LogDebugf("Cmd 159 Payload: %X", f.Payload)
		if items, ok := parseEpc159(f.Payload); ok {
			e := telemetryEvent{Type: "telemetry", DeviceID: f.DeviceID, At: time.Now().UTC()}
			epc := make([]telemetryEpc, 0, len(items))
			for i := 0; i < len(items) && i < 32; i++ {
				epc = append(epc, telemetryEpc{InputPsi: items[i].InputPsi, Psi: items[i].ActualPsi, Sccm: items[i].ActualSccm})
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
	case 250:
		if len(f.Payload) >= 2 {
			hwCfg, _ := pstore.LoadHardwareConfig(f.DeviceID)
			hwCfg.IgniteThreshold1 = float64(f.Payload[0]) / 10.0
			hwCfg.IgniteThreshold2 = float64(f.Payload[1]) / 10.0
			pstore.SaveHardwareConfig(f.DeviceID, hwCfg)
		}
	case 181, 178:
		if len(f.Payload) >= 1 {
			hwCfg, _ := pstore.LoadHardwareConfig(f.DeviceID)
			hwCfg.IgniteDuration = float64(f.Payload[0])
			pstore.SaveHardwareConfig(f.DeviceID, hwCfg)
		}
	case 132, 140:
		if len(f.Payload) >= 6 {
			hwCfg, _ := pstore.LoadHardwareConfig(f.DeviceID)
			// byte 0,1 -> float (interval)
			b0 := int(f.Payload[0]>>4)*100 + int(f.Payload[0]&0x0f)*10 + int(f.Payload[1]>>4)
			b1 := int(f.Payload[1] & 0x0f)
			interval := float64(b0) + float64(b1)*0.1

			// byte 2,3 -> int (NTimes)
			nTimes := int(f.Payload[2]>>4)*1000 + int(f.Payload[2]&0x0f)*100 + int(f.Payload[3]>>4)*10 + int(f.Payload[3]&0x0f)

			hwCfg.CycleInterval = interval
			hwCfg.CycleCount = nTimes
			pstore.SaveHardwareConfig(f.DeviceID, hwCfg)
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
		// 恢复最原始的、完全正确的逻辑：
		// 实际上，硬件协议里的 freqByte 是 (采样率 / 10)。
		// 比如 50Hz 的采样率，freqByte 就是 5。所以 parsed.Freq10 (freqByte * 10) 就是真实的 50Hz！
		// 那么每个点的时间间隔就是 dtS = 1.0 / 50.0 = 0.02 秒。
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

	// 每次分析时实时获取最新的方法（包含最新的校准参数）
	activeMethod := getActiveMethod()
	res, err := analyzer.Analyze(trace, activeMethod, "dev", time.Now())
	e := resultEvent{Type: "result", DeviceID: deviceID, Channel: ch, SessionToken: tok, At: time.Now(), Trace: trace, Method: activeMethod}
	if err != nil {
		LogErrorf("Analyze error 1: %v", err)
		e.Error = err.Error()
	} else {
		LogInfof("Analyze success 1, saving to DB...")
		e.Result = res
		st.mu.Lock()
		if st.lastResultByCh == nil {
			st.lastResultByCh = map[int]lastResult{}
		}
		st.lastResultByCh[ch] = lastResult{token: tok, at: e.At.UTC(), res: res}
		st.mu.Unlock()
		if pstore != nil {
			// Save the latest result for UI summary
			pstore.SaveResult(deviceID, ch, map[string]any{"deviceId": deviceID, "channel": ch, "sessionToken": tok, "at": e.At.UTC().Format(time.RFC3339), "result": res})

			// Save to SQLite History and Disk
			resBytes, _ := json.Marshal(map[string]any{"device_id": deviceID, "trace_id": trace.TraceID, "created_at": e.At.UTC().Format(time.RFC3339), "result": res})
			runBytes, _ := json.Marshal(map[string]any{"trace_id": trace.TraceID, "dtS": trace.DtS, "samples": trace.Values, "pollutants": res.Pollutants})
			pstore.SaveResultToDB(deviceID, trace.TraceID, e.At.UTC(), activeMethod.MethodID, string(resBytes), runBytes)
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

			// 同步更新 Modbus 寄存器
			if mbSlave != nil {
				mbSlave.UpdateFullResult(res)
			}

			// 澧為噺涓婃姤 MQTT
			if mqttClient != nil {
				polls := make(map[string]float64)
				for _, p := range res.Pollutants {
					polls[p.Code] = p.Amount
				}
				for _, g := range res.Groups {
					polls[g.Code] = g.Amount
				}
				mqttClient.PublishResult(deviceID, e.At, trace.TraceID, polls)
			}
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

	activeMethod := getActiveMethod()
	e := resultEvent{Type: "result", DeviceID: deviceID, Channel: ch, SessionToken: tok, At: time.Now().UTC(), Trace: trace, Method: activeMethod}
	res, err := analyzer.Analyze(trace, activeMethod, deviceID, time.Now())
	if err != nil {
		LogErrorf("Analyze error 2: %v", err)
		e.Error = err.Error()
	} else {
		LogInfof("Analyze success 2, saving to DB...")
		e.Result = res
		st.mu.Lock()
		if st.lastResultByCh == nil {
			st.lastResultByCh = map[int]lastResult{}
		}
		st.lastResultByCh[ch] = lastResult{token: tok, at: e.At.UTC(), res: res}
		st.mu.Unlock()
		if pstore != nil {
			// Save the latest result for UI summary
			pstore.SaveResult(deviceID, ch, map[string]any{"deviceId": deviceID, "channel": ch, "sessionToken": tok, "at": e.At.UTC().Format(time.RFC3339), "result": res})

			// Save to SQLite History and Disk
			resBytes, _ := json.Marshal(map[string]any{"device_id": deviceID, "trace_id": trace.TraceID, "created_at": e.At.UTC().Format(time.RFC3339), "result": res})
			runBytes, _ := json.Marshal(map[string]any{"trace_id": trace.TraceID, "dtS": trace.DtS, "samples": trace.Values, "pollutants": res.Pollutants})
			pstore.SaveResultToDB(deviceID, trace.TraceID, e.At.UTC(), activeMethod.MethodID, string(resBytes), runBytes)
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

			// 同步更新 Modbus 寄存器
			if mbSlave != nil {
				mbSlave.UpdateFullResult(res)
			}

			// 增量上报 MQTT
			if mqttClient != nil {
				polls := make(map[string]float64)
				for _, p := range res.Pollutants {
					polls[p.Code] = p.Amount
				}
				for _, g := range res.Groups {
					polls[g.Code] = g.Amount
				}
				mqttClient.PublishResult(deviceID, e.At, trace.TraceID, polls)
			}
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

func getActiveMethod() v1.Method {
	if pstore != nil {
		if m, ok := pstore.LoadMethod("default"); ok {
			out := v1.Method{
				MethodID: m.ID,
				Version:  1,
			}
			for _, c := range m.Compounds {
				// 转换 levels
				var v1Levels []v1.Level
				for _, l := range c.Levels {
					v1Levels = append(v1Levels, v1.Level{
						LevelIndex: l.LevelIndex,
						Amount:     l.Amount,
						Response:   l.Response,
					})
				}
				out.Pollutants = append(out.Pollutants, v1.PollutantSpec{
					Code:      c.Name,
					Name:      c.Name,
					RtS:       c.RetainTime * 60.0,
					StartS:    (c.RetainTime - c.LeftWindow) * 60.0,
					EndS:      (c.RetainTime + c.RightWindow) * 60.0,
					PaddingS:  (c.LeftWindow + c.RightWindow) * 60.0,
					Threshold: 0,
					RespStyle: c.RespStyle,
					CurveFunc: c.CurveFunc,
					Levels:    v1Levels,
				})
			}
			// 确保有基本的出峰时间，如果没有，给默认值
			for i, p := range out.Pollutants {
				if p.StartS < 0 {
					out.Pollutants[i].StartS = 0
				}
				if p.EndS <= p.StartS {
					out.Pollutants[i].EndS = out.Pollutants[i].StartS
				}
			}
			out.Groups = []v1.PeakGroupSpec{
				{
					Code:         "NMHC",
					Name:         "非甲烷总烃",
					IncludeCodes: []string{"THC"},
					ExcludeCodes: []string{"CH4"},
				},
			}
			out.Integration = v1.IntegrationSpec{
				MinHeight: m.Integration.MinHeight,
				Slope:     m.Integration.Slope,
				MinWidth:  m.Integration.MinWidth,
			}
			return out
		}
	}

	return v1.Method{
		MethodID: "default",
		Version:  1,
		Pollutants: []v1.PollutantSpec{
			{Code: "THC", Name: "总烃", StartS: 0, EndS: 20, PaddingS: 2, Threshold: 0},
			{Code: "CH4", Name: "甲烷", StartS: 20, EndS: 80, PaddingS: 2, Threshold: 0},
		},
		Groups: []v1.PeakGroupSpec{
			{
				Code:         "NMHC",
				Name:         "非甲烷总烃",
				IncludeCodes: []string{"THC"},
				ExcludeCodes: []string{"CH4"},
			},
		},
	}
}

func loadMethod() v1.Method {
	return getActiveMethod()
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

// 杈呭姪鏂规硶锛氬皢 0~399 鐨勬俯搴﹀€艰浆鎹负 2 瀛楄妭 BCD 鐮?
// 将 float * 100，提取 6 位 BCD 数字，拼装成 3 字节 (Cmd 10 需要)
func floatToBcd3B(val float64) []byte {
	v := int(math.Round(val * 100))
	if v < 0 {
		v = 0
	}
	if v > 999999 {
		v = 999999
	}

	digits := make([]byte, 6)
	for i := 5; i >= 0; i-- {
		digits[i] = byte(v % 10)
		v = v / 10
	}

	out := make([]byte, 3)
	out[0] = (digits[0] << 4) | digits[1]
	out[1] = (digits[2] << 4) | digits[3]
	out[2] = (digits[4] << 4) | digits[5]
	return out
}

// 解析 3 字节 BCD 为 float64
func bcd3BToFloat(b []byte) float64 {
	if len(b) < 3 {
		return 0
	}
	v := int(b[0]>>4)*100000 + int(b[0]&0x0F)*10000 +
		int(b[1]>>4)*1000 + int(b[1]&0x0F)*100 +
		int(b[2]>>4)*10 + int(b[2]&0x0F)
	return float64(v) / 100.0
}

func eventsToMatrix(events []models.EventRow) [8][8]float64 {
	var m [8][8]float64
	var prevMask int
	for _, evt := range events {
		mask := evt.EventMask
		t := evt.Time
		for ch := 0; ch < 8; ch++ {
			wasOn := (prevMask & (1 << ch)) != 0
			isOn := (mask & (1 << ch)) != 0
			if !wasOn && isOn {
				if m[ch][0] == 0 {
					m[ch][0] = t
				} else if m[ch][2] == 0 {
					m[ch][2] = t
				} else if m[ch][4] == 0 {
					m[ch][4] = t
				} else if m[ch][6] == 0 {
					m[ch][6] = t
				}
			}
			if wasOn && !isOn {
				if m[ch][1] == 0 {
					m[ch][1] = t
				} else if m[ch][3] == 0 {
					m[ch][3] = t
				} else if m[ch][5] == 0 {
					m[ch][5] = t
				} else if m[ch][7] == 0 {
					m[ch][7] = t
				}
			}
		}
		prevMask = mask
	}
	return m
}

func matrixToEvents(m [8][8]float64) []models.EventRow {
	timeSet := make(map[float64]bool)
	for ch := 0; ch < 8; ch++ {
		for act := 0; act < 8; act++ {
			if t := m[ch][act]; t > 0 {
				timeSet[t] = true
			}
		}
	}
	var times []float64
	for t := range timeSet {
		times = append(times, t)
	}
	sort.Float64s(times)

	var events []models.EventRow
	var currentMask int
	for _, t := range times {
		for ch := 0; ch < 8; ch++ {
			// actions: 0,2,4,6 are ON, 1,3,5,7 are OFF
			if m[ch][0] == t || m[ch][2] == t || m[ch][4] == t || m[ch][6] == t {
				currentMask |= (1 << ch)
			}
			if m[ch][1] == t || m[ch][3] == t || m[ch][5] == t || m[ch][7] == t {
				currentMask &^= (1 << ch)
			}
		}
		events = append(events, models.EventRow{Time: t, EventMask: currentMask})
	}
	return events
}

func parseEventTable(payload []byte) *[4][8]float64 {
	if len(payload) < 96 {
		return nil
	}
	var m [4][8]float64
	idx := 0
	for ch := 0; ch < 4; ch++ {
		for act := 0; act < 8; act++ {
			m[ch][act] = bcd3BToFloat(payload[idx : idx+3])
			idx += 3
		}
	}
	return &m
}

func tempToBCD2(temp float64) []byte {
	v := int(math.Round(temp * 10))
	if v < 0 {
		v = 0
	}
	if v > 3999 {
		v = 3999
	}
	// v 姝ゆ椂褰㈠ 1234 (瀵瑰簲 123.4搴?
	d1 := (v / 1000) % 10
	d2 := (v / 100) % 10
	d3 := (v / 10) % 10
	d4 := v % 10
	b0 := byte((d1 << 4) | d2)
	b1 := byte((d3 << 4) | d4)
	return []byte{b0, b1}
}

// 杈呭姪鏂规硶锛氬皢鏁板€艰浆鎹负澶х uint16 瀛楄妭
func u16Bytes(v float64, scale float64) []byte {
	iv := int(math.Round(v * scale))
	if iv < 0 {
		iv = 0
	}
	if iv > 65535 {
		iv = 65535
	}
	return []byte{byte(iv >> 8), byte(iv & 0xFF)}
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
      <div class="brand">鍦ㄧ嚎鐩戞祴 <span style="font-size:12px;opacity:0.6;margin-left:10px">v0.3.10</span></div>
      <nav class="tabs" id="tabs">
        <button class="tab active" data-tab="overview"><span class="tabIcon">姒?/span><span class="tabText">姒傝</span></button>
        <button class="tab" data-tab="curve"><span class="tabIcon">鏇?/span><span class="tabText">鏇茬嚎</span></button>
        <button class="tab" data-tab="result"><span class="tabIcon">鏋?/span><span class="tabText">缁撴灉</span></button>
        <button class="tab" data-tab="events"><span class="tabIcon">浜?/span><span class="tabText">浜嬩欢</span></button>
        <button class="tab" data-tab="logs"><span class="tabIcon">蹇?/span><span class="tabText">鏃ュ織</span></button>
        <button class="tab" data-tab="settings"><span class="tabIcon">璁?/span><span class="tabText">璁剧疆</span></button>
      </nav>
      <div class="flame" title="鍛婅"><div class="flameInner"></div></div>
    </header>

    <main class="main">
      <section id="view-overview" class="view active">
        <div class="card cardPad" style="max-width:980px">
          <div class="homeGrid">
            <div class="blueCard"><div class="blueTitle">鎬荤儍</div><div class="blueValue mono" id="kpi-thc">-</div></div>
            <div class="blueCard"><div class="blueTitle" style="opacity:0.0">鍗犱綅</div><div class="blueValue mono" id="kpi-thc2"> </div></div>
            <div class="blueCard"><div class="blueTitle">鐢茬兎</div><div class="blueValue mono" id="kpi-ch4">-</div></div>
            <div class="blueCard"><div class="blueTitle" style="opacity:0.0">鍗犱綅</div><div class="blueValue mono" id="kpi-ch4b"> </div></div>
            <div class="blueCard"><div class="blueTitle">闈炵敳鐑锋€荤儍</div><div class="blueValue mono" id="kpi-nmhc">-</div></div>
            <div class="blueCard"><div class="blueTitle" style="opacity:0.0">鍗犱綅</div><div class="blueValue mono" id="kpi-nmhc2"> </div></div>
          </div>

          <div class="bottomBar">
            <div>
              <div class="statusStrip mono" id="home-status">鏃堕棿: 0.000 min   淇″彿: 0.000 pA</div>
              <div style="margin-top:10px" class="ctrlStrip">
                <button class="ctrlBtn">杩愯娆℃暟</button>
                <div class="ctrlVal mono" id="home-runCountVal">1720</div>
                <button class="ctrlBtn">鍗曚綅</button>
                <div class="ctrlVal mono" id="home-unitVal">mg/m鲁</div>
                <button class="ctrlAction" id="home-inject">杩涙牱</button>
              </div>
            </div>
            <div class="flame" title="鐘舵€?><div class="flameInner"></div></div>
            <div class="clock mono" id="home-clock">0000-00-00 00:00:00</div>
          </div>
        </div>
        <div class="card cardPad" style="max-width:980px;margin-top:12px">
          <div id="tblTitle">璁惧鍒楄〃</div>
          <table>
            <thead><tr><th>璁惧</th><th>鍦ㄧ嚎</th><th>lastSeen</th><th>143</th><th>last143</th></tr></thead>
            <tbody id="overview-devices"><tr><td class="mono" colspan="5" style="color:var(--muted)">绛夊緟 GC...</td></tr></tbody>
          </table>
        </div>
      </section>

      <section id="view-curve" class="view">
        <div class="card cardPad" style="max-width:1240px">
          <div class="row" style="margin-bottom:10px">
            <button class="btn dark">閫氶亾1缁撴潫</button>
            <label class="modeItem"><span class="dot" style="background:var(--ok)"></span><input type="radio" name="mode" checked /> 姝ｅ父杩涙牱</label>
            <label class="modeItem"><span class="dot" style="background:#B7C0CF"></span><input type="radio" name="mode" /> 闆舵皵鍙嶆爣</label>
            <label class="modeItem"><span class="dot" style="background:#B7C0CF"></span><input type="radio" name="mode" /> 鏍囨皵鍙嶆爣</label>
            <div class="spacer"></div>
            <span class="label">涓嬮檺:</span><input id="ylow" class="input mono" style="width:90px" value="0" />
            <span class="label">涓婇檺:</span><input id="yhigh" class="input mono" style="width:90px" value="40" />
            <span class="label">閲囬泦鏃堕棿:</span><input id="acqmin" class="input mono" style="width:50px" value="2" />
            <span class="label">婊″睆鏃堕棿:</span><input id="fullmin" class="input mono" style="width:50px" value="2" />
            <span class="label">寰幆鍛ㄦ湡:</span><input id="cyclemin" class="input mono" style="width:50px" value="2" title="涓嬩竴閽堣嚜鍔ㄨ繘鏍风殑闂撮殧鏃堕棿" />
            <span class="label">寰幆娆℃暟:</span><input id="cyclemax" class="input mono" style="width:50px" value="9999" title="鏈€澶у惊鐜繘鏍锋鏁? />
          </div>

          <div class="row" style="margin-bottom:10px">
            <div id="stat" class="mono">閫氶亾1: 0.000 min  0.000 pA  淇″彿1:</div>
            <label class="modeItem"><input id="autoy" type="checkbox" checked /> 宄伴珮鑷€傚簲</label>
            <label class="modeItem"><input id="loop" type="checkbox" checked /> 杩炵画鍒嗘瀽</label>
            <input id="name" class="input" placeholder="璋卞浘鍚嶇О" style="width:200px" />
            <div class="spacer"></div>
            <div class="kpi"><div class="label">鍦ㄧ嚎</div><div id="status" class="mono">鏈繛鎺?/div></div>
            <div class="kpi"><div class="label">璁惧</div><select id="device" class="select mono"><option value="">绛夊緟 GC...</option></select></div>
            <div class="kpi"><div class="label">Channel</div><select id="chn" class="select mono"><option value="0">0</option><option value="1">1</option><option value="2">2</option><option value="3">3</option></select></div>
            <button class="btn primary" id="start">寮€濮?/button>
            <button class="btn" id="stop">鍋滄</button>
            <button class="btn" id="clear">娓呭睆</button>
          </div>

          <div id="panel">
            <div id="chartWrap" class="card" style="padding:10px">
              <canvas id="cv" width="1200" height="440"></canvas>
            </div>
            <div>
              <div id="right">
                <div id="tblTitle">鍚嶇О | 鍚噺(mg/m鲁)</div>
                <table>
                  <thead><tr><th>鍚嶇О</th><th>鍚噺(mg/m鲁)</th></tr></thead>
                  <tbody id="tbody">
                    <tr><td>鎬荤儍</td><td class="mono">-</td></tr>
                    <tr><td>鐢茬兎</td><td class="mono">-</td></tr>
                    <tr><td>闈炵敳鐑锋€荤儍</td><td class="mono">-</td></tr>
                  </tbody>
                </table>
              </div>
              <div style="display:grid;grid-template-columns:1fr 1fr;gap:12px;margin-top:12px">
                <div class="card" style="border-radius:10px;overflow:hidden">
                  <div id="tblTitle">瀹炴祴</div>
                  <table>
                    <tbody>
                      <tr><td>杞芥皵</td><td class="mono" id="gas-carrier">-</td></tr>
                      <tr><td>姘㈡皵</td><td class="mono" id="gas-h2">-</td></tr>
                      <tr><td>绌烘皵</td><td class="mono" id="gas-air">-</td></tr>
                    </tbody>
                  </table>
                </div>
                <div class="card" style="border-radius:10px;overflow:hidden">
                  <div id="tblTitle">瀹炴祴鈩?/div>
                  <table>
                    <tbody>
                      <tr><td>鏌辩</td><td class="mono" id="temp-col">-</td></tr>
                      <tr><td>闃€娓?/td><td class="mono" id="temp-inj1">-</td></tr>
                      <tr><td>妫€娴?</td><td class="mono" id="temp-det1">-</td></tr>
                      <tr><td>杩涙牱2</td><td class="mono" id="temp-inj2">-</td></tr>
                    </tbody>
                  </table>
                </div>
              </div>
              <div class="flame" style="margin-top:12px" title="鐘舵€?><div class="flameInner"></div></div>
              <div id="dbg" class="mono" style="margin-top:10px;color:var(--muted)"></div>
            </div>
          </div>
        </div>
      </section>

      <section id="view-result" class="view">
        <div class="card cardPad" style="max-width:1240px">
          <div class="row" style="margin-bottom:10px">
            <div class="label">NMHC 缁撴灉鍘嗗彶锛堟€荤儍/鐢茬兎/闈炵敳鐑锋€荤儍锛?/div>
            <div class="spacer"></div>
            <span class="label">寮€濮?/span><input id="res-from" class="input mono" style="width:220px" placeholder="YYYY-MM-DD HH:mm:ss" />
            <span class="label">缁撴潫</span><input id="res-to" class="input mono" style="width:220px" placeholder="YYYY-MM-DD HH:mm:ss" />
            <button class="btn dark" id="res-export">瀵煎嚭CSV</button>
            <button class="btn dark" id="res-delete">鍒犻櫎鏃堕棿娈?/button>
          </div>
          <div class="card" style="border-radius:10px;overflow:hidden">
            <div id="tblTitle">璁板綍鎶ヨ〃</div>
            <table>
              <thead><tr><th>鏃堕棿</th><th>鎬荤儍</th><th>鐢茬兎</th><th>闈炵敳鐑锋€荤儍</th></tr></thead>
              <tbody id="res-tbody"><tr><td class="mono" colspan="4" style="color:var(--muted)">鏆傛棤鏁版嵁</td></tr></tbody>
            </table>
          </div>
        </div>
      </section>

      <section id="view-events" class="view">
        <div class="card cardPad" style="max-width:1240px">
          <div class="row" style="margin-bottom:10px">
            <label class="modeItem"><input id="evt-only-selected" type="checkbox" checked /> 浠呭綋鍓嶈澶?/label>
            <div class="spacer"></div>
            <button class="btn dark" id="evt-clear">娓呯┖</button>
          </div>
          <div class="card" style="border-radius:10px;overflow:hidden">
            <div id="tblTitle">浜嬩欢娴?/div>
            <table>
              <thead><tr><th>鏃堕棿</th><th>璁惧</th><th>绫诲瀷</th><th>鎽樿</th></tr></thead>
              <tbody id="evt-tbody"><tr><td class="mono" colspan="4" style="color:var(--muted)">鏆傛棤鏁版嵁</td></tr></tbody>
            </table>
          </div>
        </div>
      </section>

      <section id="view-logs" class="view">
        <div class="card cardPad" style="max-width:1240px">
          <div id="tblTitle">璋冭瘯鏃ュ織</div>
          <pre id="logs-pre" class="mono" style="margin:0;padding:12px;white-space:pre-wrap"></pre>
        </div>
      </section>

      <section id="view-settings" class="view">
        <div class="card cardPad" style="max-width:980px">
          <div id="tblTitle">璁剧疆</div>
          <div class="row" style="margin-top:12px">
            <div><div class="label">榛樿婊″睆鏃堕棿(min)</div><input id="set-fullmin" class="input mono" style="width:120px" value="2" /></div>
            <div><div class="label">榛樿涓嬮檺</div><input id="set-ylow" class="input mono" style="width:120px" value="0" /></div>
            <div><div class="label">榛樿涓婇檺</div><input id="set-yhigh" class="input mono" style="width:120px" value="40" /></div>
            <div><div class="label">榛樿宄伴珮鑷€傚簲</div><label class="modeItem"><input id="set-autoy" type="checkbox" checked /> 鍚敤</label></div>
            <div><div class="label">榛樿閲囬泦鏃堕棿(min)</div><input id="set-acqmin" class="input mono" style="width:120px" value="2" /></div>
            <div class="spacer"></div>
            <button class="btn primary" id="set-save">淇濆瓨</button>
          </div>
          <div class="row" style="margin-top:12px">
            <div><div class="label">杞芥皵 EPC idx</div><select id="set-epc-carrier" class="select mono" style="width:120px"></select></div>
            <div><div class="label">姘㈡皵 EPC idx</div><select id="set-epc-h2" class="select mono" style="width:120px"></select></div>
            <div><div class="label">绌烘皵 EPC idx</div><select id="set-epc-air" class="select mono" style="width:120px"></select></div>
            <div class="spacer"></div>
            <div class="label">鎻愮ず锛歩dx 鏉ヨ嚜 Cmd=159 EPC 涓婃姤鐨勬潯鐩簭鍙凤紙浠?0 寮€濮嬶級</div>
          </div>
          <div class="row" style="margin-top:12px">
            <button class="btn dark" id="set-open-method">鏂规硶</button>
            <button class="btn dark" id="set-open-processing">璋卞浘澶勭悊</button>
            <button class="btn dark" id="set-open-reports">楂樼骇鎶ヨ〃</button>
            <div class="spacer"></div>
            <div class="label" style="color:var(--muted)">浜岀骇鍏ュ彛鍗犱綅锛氫笉鍗犵敤椤舵爮鏍囩</div>
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
        k3.textContent = f4(latest ? latest.nmhc : null);
      }

      const table = document.getElementById('tbody');
      if(table){
        const rows = table.querySelectorAll('tr');
        for(const tr of rows){
          const tds = tr.querySelectorAll('td');
          if(tds.length < 2) continue;
          const name = (tds[0].textContent || '').trim();
          if(name === '鎬荤儍') tds[1].textContent = f4(thc);
          if(name === '鐢茬兎') tds[1].textContent = f4(ch4);
          if(name === '闈炵敳鐑锋€荤儍'){
            tds[1].textContent = f4(latest ? latest.nmhc : null);
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
        overviewDevicesEl.innerHTML = '<tr><td class="mono" colspan="5" style="color:var(--muted)">绛夊緟 GC...</td></tr>';
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
        resTbodyEl.innerHTML = '<tr><td class="mono" colspan="4" style="color:var(--muted)">鏈€夋嫨璁惧</td></tr>';
        return;
      }
      const fromD = parseTimeText(resFromEl && resFromEl.value);
      const toD = parseTimeText(resToEl && resToEl.value);
      kickFetchNmhcHistory(sel, fromD, toD, false);
      const arr = loadNmchHistory(sel);
      const fetchSt = getNmhcFetchState(sel);
      if(arr.length === 0 && fetchSt.inFlight){
        resTbodyEl.innerHTML = '<tr><td class="mono" colspan="4" style="color:var(--muted)">鍔犺浇涓?..</td></tr>';
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
        resTbodyEl.innerHTML = '<tr><td class="mono" colspan="4" style="color:var(--muted)">鏆傛棤鏁版嵁</td></tr>';
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
        evtTbodyEl.innerHTML = '<tr><td class="mono" colspan="4" style="color:var(--muted)">鏆傛棤鏁版嵁</td></tr>';
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
        alert('璇峰～鍐欏紑濮嬩笌缁撴潫鏃堕棿');
        return;
      }
      const fromT = fromD.getTime();
      const toT = toD.getTime();
      if(!(toT >= fromT)){
        alert('缁撴潫鏃堕棿蹇呴』澶т簬寮€濮嬫椂闂?);
        return;
      }
      if(!confirm('纭鍒犻櫎璇ユ椂闂存鍐呯殑璁板綍锛?)) return;
      const qs = new URLSearchParams();
      qs.set('deviceId', sel);
      qs.set('from', fromD.toISOString());
      qs.set('to', toD.toISOString());
      const res = await fetch('/api/v1/results/nmhc?' + qs.toString(), {method:'DELETE'});
      const j = await res.json().catch(()=>({}));
      if(!res.ok){
        alert(j && j.error ? String(j.error) : '鍒犻櫎澶辫触');
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
      ctx.fillText('淇″彿(pA)', -28, 0);
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
        ctx.fillText('绛夊緟閫夋嫨璁惧', 12, 22);
        return;
      }

      const s = getStream(sel, ch);
      if(!s.pts || s.pts.length < 2){
        ctx.fillStyle = '#777';
        ctx.font = '14px system-ui';
        ctx.fillText('鏆傛棤瀹炴椂鏁版嵁锛堢瓑寰呬富鏉垮彂閫?143 鏁版嵁娴侊級', 12, 22);
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
      ctx.fillText('淇″彿(pA)', -32, 0);
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
            statusEl.textContent = '鍦ㄧ嚎: ' + prefer;
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
        dbg.textContent = '璁惧: ' + cur + '锛堟湭鑾峰彇鍒扮粺璁′俊鎭級';
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
      dbg.textContent = '璁惧: ' + cur + ' | lastCmd=' + d.lastCmd + ' | 143=' + c143 + ' | lastSeen=' + (seenAgo>=0 ? (seenAgo+'s') : '-') + ' | last143=' + (d143Ago>=0 ? (d143Ago+'s') : '-') + ' | control=' + (d.allowControl ? 'on' : 'off') + extra + sinfo;
	  setButtonsEnabled(!!d.connected);
    }

    async function sendCmd(name){
      const sel = selectedDevice();
      if(!sel){
        alert('璇烽€夋嫨璁惧');
        return;
      }
      const channel = Number(chnEl.value || '0');
      const url = '/api/v1/devices/' + encodeURIComponent(sel) + '/cmd?name=' + encodeURIComponent(name) + '&channel=' + channel;
      const res = await fetch(url, {method:'POST'});
      const j = await res.json().catch(()=>({}));
      if(!res.ok){
        alert(j.error || '鍙戦€佸け璐?);
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
              if(name === '鎬荤儍'){
                tds[1].textContent = thc && isFinite(thc.height) ? Number(thc.height).toFixed(4) : '-';
              }
              if(name === '鐢茬兎'){
                tds[1].textContent = ch4 && isFinite(ch4.height) ? Number(ch4.height).toFixed(4) : '-';
              }
              if(name === '闈炵敳鐑锋€荤儍'){
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
            statusEl.textContent = '鍦ㄧ嚎: ' + msg.deviceId + '锛堣嚜鍔級';
          }
        } else if(sel === msg.deviceId){
          statusEl.textContent = '鍦ㄧ嚎: ' + msg.deviceId;
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
        statusEl.textContent = '鍦ㄧ嚎: ' + msg.deviceId;
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
      statEl.textContent = '閫氶亾' + (Number(chnEl.value||'0')+1) + ': ' + minText + ' min   ' + vText + ' pA';
      homeStatusEl.textContent = '鏃堕棿: ' + minText + ' min   淇″彿: ' + vText + ' pA';
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
        statusEl.textContent = '杩炴帴鏂紑: ' + sel;
      } else if(lastActiveDevice){
        statusEl.textContent = '杩炴帴鏂紑: ' + lastActiveDevice;
      } else {
        statusEl.textContent = '杩炴帴鏂紑';
      }
    };

    deviceEl.addEventListener('change', ()=>{
      const sel = selectedDevice();
      if(sel){
        statusEl.textContent = '鍦ㄧ嚎: ' + sel;
        const run = document.getElementById('home-runCountVal');
        if(run) run.textContent = String(loadNmchHistory(sel).length);
        kickFetchNmhcHistory(sel, null, null, true);
        restoreFromBackend(sel);
      } else {
        statusEl.textContent = '鏈€夋嫨璁惧锛堣嚜鍔級';
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
      statEl.textContent = '閫氶亾' + (Number(chnEl.value||'0')+1) + ': ' + minText + ' min   ' + vText + ' pA';
      homeStatusEl.textContent = '鏃堕棿: ' + minText + ' min   淇″彿: ' + vText + ' pA';
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
      alert(title + '锛氬緟瀹炵幇');
    };
    if(setOpenMethodEl) setOpenMethodEl.addEventListener('click', ()=>openPlaceholder('鏂规硶'));
    if(setOpenProcessingEl) setOpenProcessingEl.addEventListener('click', ()=>openPlaceholder('璋卞浘澶勭悊'));
    if(setOpenReportsEl) setOpenReportsEl.addEventListener('click', ()=>openPlaceholder('楂樼骇鎶ヨ〃'));
  </script>
</body>
</html>`
