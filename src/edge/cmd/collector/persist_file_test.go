package main

import (
	"path/filepath"
	"testing"
)

func TestPersistStore_RoundTrip(t *testing.T) {
	root := t.TempDir()
	ps, err := openPersistStore(root)
	if err != nil {
		t.Fatalf("openPersistStore: %v", err)
	}
	ps.SaveKV("k1", "v1")
	if v, ok := ps.LoadKV("k1"); !ok || v != "v1" {
		t.Fatalf("LoadKV got=%q ok=%v", v, ok)
	}

	ui := defaultUIState("GC1")
	ui.SelectedChannel = 2
	ui.FullMin = 5
	ps.SaveUI(ui)
	if got, ok := ps.LoadUI("GC1"); !ok || got.SelectedChannel != 2 || got.FullMin != 5 {
		t.Fatalf("LoadUI got=%+v ok=%v", got, ok)
	}
	if v, ok := ps.LoadLastDeviceID(); !ok || v != "GC1" {
		t.Fatalf("LoadLastDeviceID got=%q ok=%v", v, ok)
	}

	payload := map[string]any{"deviceId": "GC1", "channel": 2, "dtS": 0.1, "values": []float64{1, 2, 3}}
	ps.SaveSession("GC1", 2, payload)
	if got, ok := ps.LoadSession("GC1", 2); !ok || got["deviceId"].(string) != "GC1" {
		t.Fatalf("LoadSession got=%v ok=%v", got, ok)
	}

	ps.SaveSnapshot("{\"ok\":true}")
	if _, err := filepath.Glob(filepath.Join(root, "snapshots.jsonl")); err != nil {
		t.Fatalf("snapshot glob: %v", err)
	}
}

