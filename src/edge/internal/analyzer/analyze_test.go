package analyzer_test

import (
	"encoding/json"
	"math"
	"os"
	"path/filepath"
	"testing"
	"time"

	"chromatography-workstation/edge/internal/analyzer"
	v1 "chromatography-workstation/edge/internal/contracts/v1"
)

func TestAnalyzeGaussianBasic(t *testing.T) {
	root, err := repoRootFromEdgeModule()
	if err != nil {
		t.Fatal(err)
	}
	trace := readTrace(t, filepath.Join(root, "docs", "schemas", "examples", "trace.gaussian.json"))
	method := readMethod(t, filepath.Join(root, "docs", "schemas", "examples", "method.basic.json"))

	res, err := analyzer.Analyze(trace, method, "test", time.Date(2026, 5, 14, 0, 0, 0, 0, time.UTC))
	if err != nil {
		t.Fatal(err)
	}
	if len(res.Pollutants) != 1 {
		t.Fatalf("expected 1 pollutant, got %d", len(res.Pollutants))
	}
	p := res.Pollutants[0]
	if p.Status != "detected" {
		t.Fatalf("expected detected, got %s", p.Status)
	}
	if !near(p.RtS, 5.4, 1e-6) {
		t.Fatalf("rtS expected 5.4 got %v", p.RtS)
	}
	if !near(p.Height, 10.0, 1e-6) {
		t.Fatalf("height expected 10.0 got %v", p.Height)
	}
	if p.Area <= 0 {
		t.Fatalf("area expected > 0 got %v", p.Area)
	}
}

func readTrace(t *testing.T, path string) v1.Trace {
	t.Helper()
	var tr v1.Trace
	mustDecode(t, path, &tr)
	return tr
}

func readMethod(t *testing.T, path string) v1.Method {
	t.Helper()
	var m v1.Method
	mustDecode(t, path, &m)
	return m
}

func mustDecode(t *testing.T, path string, dst any) {
	t.Helper()
	b, err := os.ReadFile(path)
	if err != nil {
		t.Fatalf("read %s: %v", path, err)
	}
	if err := json.Unmarshal(b, dst); err != nil {
		t.Fatalf("decode %s: %v", path, err)
	}
}

func near(v float64, want float64, eps float64) bool {
	return math.Abs(v-want) <= eps
}

func repoRootFromEdgeModule() (string, error) {
	wd, err := os.Getwd()
	if err != nil {
		return "", err
	}
	dir := wd
	for i := 0; i < 20; i++ {
		if filepath.Base(dir) == "edge" && filepath.Base(filepath.Dir(dir)) == "src" {
			return filepath.Dir(filepath.Dir(dir)), nil
		}
		dir = filepath.Dir(dir)
	}
	return "", os.ErrNotExist
}
