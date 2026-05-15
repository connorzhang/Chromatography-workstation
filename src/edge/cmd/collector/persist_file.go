package main

import (
	"bufio"
	"encoding/json"
	"os"
	"path/filepath"
	"sync"
	"time"
)

type persistStore struct {
	mu   sync.Mutex
	root string
	kv   map[string]string
}

func openPersistStore(root string) (*persistStore, error) {
	if root == "" {
		root = filepath.Join(".run", "db")
	}
	_ = os.MkdirAll(root, 0o755)
	st := &persistStore{root: root, kv: map[string]string{}}
	st.loadKVLocked()
	return st, nil
}

func (s *persistStore) Close() {}

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

func (s *persistStore) LoadResult(deviceID string, channel int) (map[string]any, bool) {
	if deviceID == "" {
		return nil, false
	}
	b, err := os.ReadFile(filepath.Join(s.root, "result", deviceID, "ch"+itoa(channel)+".json"))
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

