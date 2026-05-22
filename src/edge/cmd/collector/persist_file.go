package main

import (
	"bufio"
	"database/sql"
	"encoding/json"
	"log"
	"os"
	"path/filepath"
	"strconv"
	"sync"
	"time"

	_ "modernc.org/sqlite"

	"chromatography-workstation/edge/internal/models"
)

type persistStore struct {
	mu   sync.Mutex
	root string
	kv   map[string]string
	db   *sql.DB
}

func openPersistStore(root string) (*persistStore, error) {
	if root == "" {
		root = filepath.Join(".run", "db")
	}
	_ = os.MkdirAll(root, 0o755)
	
	// 初始化 SQLite 数据库
	dbPath := filepath.Join(root, "history.sqlite")
	db, err := sql.Open("sqlite", dbPath)
	if err != nil {
		return nil, err
	}
	
	// 创建历史结果表
	createTableSQL := `
	CREATE TABLE IF NOT EXISTS results (
		id INTEGER PRIMARY KEY AUTOINCREMENT,
		trace_id TEXT NOT NULL,
		device_id TEXT NOT NULL,
		created_at DATETIME NOT NULL,
		method_id TEXT,
		result_json TEXT NOT NULL
	);
	CREATE INDEX IF NOT EXISTS idx_results_device_time ON results(device_id, created_at);
	`
	if _, err := db.Exec(createTableSQL); err != nil {
		log.Printf("create table failed: %v", err)
	}

	st := &persistStore{root: root, kv: map[string]string{}, db: db}
	st.loadKVLocked()
	return st, nil
}

func (s *persistStore) Close() {
	if s.db != nil {
		s.db.Close()
	}
}

func (s *persistStore) kvPath() string {
	return filepath.Join(s.root, "kv.json")
}

func (s *persistStore) loadKVLocked() {
	b, err := os.ReadFile(s.kvPath())
	if err != nil {
		return
	}
	var m map[string]string
	if json.Unmarshal(b, &m) != nil {
		return
	}
	for k, v := range m {
		s.kv[k] = v
	}
}

func (s *persistStore) flushKVLocked() {
	b, err := json.Marshal(s.kv)
	if err != nil {
		return
	}
	_ = os.WriteFile(s.kvPath(), b, 0o644)
}

func (s *persistStore) SaveKV(key, value string) {
	if key == "" {
		return
	}
	s.mu.Lock()
	defer s.mu.Unlock()
	s.kv[key] = value
	s.flushKVLocked()
}

func (s *persistStore) LoadKV(key string) (string, bool) {
	s.mu.Lock()
	defer s.mu.Unlock()
	v, ok := s.kv[key]
	return v, ok
}

func (s *persistStore) LoadLastDeviceID() (string, bool) {
	return s.LoadKV("ui.lastDeviceId")
}

func (s *persistStore) SaveUI(st uiState) {
	if st.DeviceID == "" {
		return
	}
	st.UpdatedAt = time.Now().UTC().Format(time.RFC3339)
	b, err := json.Marshal(st)
	if err != nil {
		return
	}
	path := filepath.Join(s.root, "ui")
	_ = os.MkdirAll(path, 0o755)
	_ = os.WriteFile(filepath.Join(path, st.DeviceID+".json"), b, 0o644)
	s.SaveKV("ui.lastDeviceId", st.DeviceID)
}

func (s *persistStore) LoadUI(deviceID string) (uiState, bool) {
	if deviceID == "" {
		return uiState{}, false
	}
	b, err := os.ReadFile(filepath.Join(s.root, "ui", deviceID+".json"))
	if err != nil {
		return uiState{}, false
	}
	var out uiState
	if json.Unmarshal(b, &out) != nil {
		return uiState{}, false
	}
	if out.DeviceID == "" {
		out.DeviceID = deviceID
	}
	return out, true
}

func (s *persistStore) SaveSession(deviceID string, channel int, payload any) {
	if deviceID == "" {
		return
	}
	b, err := json.Marshal(payload)
	if err != nil {
		return
	}
	path := filepath.Join(s.root, "session", deviceID)
	_ = os.MkdirAll(path, 0o755)
	_ = os.WriteFile(filepath.Join(path, "ch"+itoa(channel)+".json"), b, 0o644)
}

func (s *persistStore) LoadSession(deviceID string, channel int) (map[string]any, bool) {
	if deviceID == "" {
		return nil, false
	}
	b, err := os.ReadFile(filepath.Join(s.root, "session", deviceID, "ch"+itoa(channel)+".json"))
	if err != nil {
		return nil, false
	}
	var out map[string]any
	if json.Unmarshal(b, &out) != nil {
		return nil, false
	}
	return out, true
}

func (s *persistStore) SaveResult(deviceID string, channel int, payload any) {
	if deviceID == "" {
		return
	}
	b, err := json.Marshal(payload)
	if err != nil {
		return
	}
	path := filepath.Join(s.root, "result", deviceID)
	_ = os.MkdirAll(path, 0o755)
	_ = os.WriteFile(filepath.Join(path, "ch"+itoa(channel)+".json"), b, 0o644)
}

func (s *persistStore) SaveResultToDB(deviceID string, traceID string, createdAt time.Time, methodID string, resultJSON string) {
	if s.db == nil {
		return
	}
	query := `INSERT INTO results (trace_id, device_id, created_at, method_id, result_json) VALUES (?, ?, ?, ?, ?)`
	_, err := s.db.Exec(query, traceID, deviceID, createdAt.UTC(), methodID, resultJSON)
	if err != nil {
		log.Printf("SaveResultToDB error: %v", err)
	}
}

func (s *persistStore) LoadResultsFromDB(deviceID string, from time.Time, to time.Time, limit int) []string {
	if s.db == nil {
		return nil
	}
	query := `SELECT result_json FROM results WHERE device_id = ? AND created_at >= ? AND created_at <= ? ORDER BY created_at DESC LIMIT ?`
	rows, err := s.db.Query(query, deviceID, from.UTC(), to.UTC(), limit)
	if err != nil {
		log.Printf("LoadResultsFromDB query error: %v", err)
		return nil
	}
	defer rows.Close()

	var results []string
	for rows.Next() {
		var r string
		if err := rows.Scan(&r); err == nil {
			results = append(results, r)
		}
	}
	return results
}

func (s *persistStore) LoadResult(deviceID string, ch int) (map[string]any, bool) {
	if deviceID == "" {
		return nil, false
	}
	b, err := os.ReadFile(filepath.Join(s.root, "result", deviceID, "ch"+strconv.Itoa(ch)+".json"))
	if err != nil {
		return nil, false
	}
	var out map[string]any
	if json.Unmarshal(b, &out) != nil {
		return nil, false
	}
	return out, true
}

func (s *persistStore) AddNMHC(r nmhcRecord) {
	if r.DeviceID == "" || r.TimeRFC3339 == "" {
		return
	}
	path := filepath.Join(s.root, "nmhc.jsonl")
	_ = os.MkdirAll(filepath.Dir(path), 0o755)
	f, err := os.OpenFile(path, os.O_CREATE|os.O_APPEND|os.O_WRONLY, 0o644)
	if err != nil {
		return
	}
	w := bufio.NewWriter(f)
	_, _ = w.Write(append(mustJSONLine(r), '\n'))
	_ = w.Flush()
	_ = f.Close()
}

func (s *persistStore) SaveSnapshot(stateJSON string) {
	if stateJSON == "" {
		return
	}
	path := filepath.Join(s.root, "snapshots.jsonl")
	f, err := os.OpenFile(path, os.O_CREATE|os.O_APPEND|os.O_WRONLY, 0o644)
	if err != nil {
		return
	}
	_, _ = f.Write(append([]byte(stateJSON), '\n'))
	_ = f.Close()
}

func (s *persistStore) SaveMethod(methodID string, method models.Method) {
	if methodID == "" {
		methodID = "default"
	}
	b, err := json.Marshal(method)
	if err != nil {
		return
	}
	path := filepath.Join(s.root, "methods")
	_ = os.MkdirAll(path, 0o755)
	_ = os.WriteFile(filepath.Join(path, methodID+".json"), b, 0o644)
}

func (s *persistStore) LoadMethod(methodID string) (models.Method, bool) {
	if methodID == "" {
		methodID = "default"
	}
	b, err := os.ReadFile(filepath.Join(s.root, "methods", methodID+".json"))
	if err != nil {
		return models.Method{}, false
	}
	var out models.Method
	if json.Unmarshal(b, &out) != nil {
		return models.Method{}, false
	}
	return out, true
}

func (s *persistStore) SaveHardwareConfig(deviceID string, cfg models.HardwareConfig) {
	if deviceID == "" {
		return
	}
	b, err := json.Marshal(cfg)
	if err != nil {
		return
	}
	path := filepath.Join(s.root, "hwconfig")
	_ = os.MkdirAll(path, 0o755)
	_ = os.WriteFile(filepath.Join(path, deviceID+".json"), b, 0o644)
}

func (s *persistStore) LoadHardwareConfig(deviceID string) (models.HardwareConfig, bool) {
	if deviceID == "" {
		return models.HardwareConfig{}, false
	}
	b, err := os.ReadFile(filepath.Join(s.root, "hwconfig", deviceID+".json"))
	if err != nil {
		return models.HardwareConfig{}, false
	}
	var out models.HardwareConfig
	if json.Unmarshal(b, &out) != nil {
		return models.HardwareConfig{}, false
	}
	return out, true
}

func itoa(n int) string {
	if n == 0 {
		return "0"
	}
	neg := n < 0
	if neg {
		n = -n
	}
	buf := [32]byte{}
	i := len(buf)
	for n > 0 {
		i--
		buf[i] = byte('0' + (n % 10))
		n /= 10
	}
	if neg {
		i--
		buf[i] = '-'
	}
	return string(buf[i:])
}

