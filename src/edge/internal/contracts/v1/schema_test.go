package v1_test

import (
	"encoding/json"
	"errors"
	"os"
	"path/filepath"
	"testing"
	"time"

	"chromatography-workstation/edge/internal/analyzer"
	v1 "chromatography-workstation/edge/internal/contracts/v1"
)

func TestSchemasValidateExamples(t *testing.T) {
	root, err := repoRootFromEdgeModule()
	if err != nil {
		t.Fatal(err)
	}

	mustBeJSON(t, filepath.Join(root, "docs", "schemas", "voc-trace.v1.schema.json"))
	mustBeJSON(t, filepath.Join(root, "docs", "schemas", "voc-method.v1.schema.json"))
	mustBeJSON(t, filepath.Join(root, "docs", "schemas", "voc-result.v1.schema.json"))

	var trace v1.Trace
	var method v1.Method
	mustDecode(t, filepath.Join(root, "docs", "schemas", "examples", "trace.gaussian.json"), &trace)
	mustDecode(t, filepath.Join(root, "docs", "schemas", "examples", "method.basic.json"), &method)

	if err := validateTraceExample(trace); err != nil {
		t.Fatal(err)
	}
	if err := validateMethodExample(method); err != nil {
		t.Fatal(err)
	}

	res, err := analyzer.Analyze(trace, method, "test", time.Date(2026, 5, 14, 0, 0, 0, 0, time.UTC))
	if err != nil {
		t.Fatal(err)
	}

	if err := validateResultExample(res); err != nil {
		t.Fatal(err)
	}
}

func mustDecode(t *testing.T, path string, dst any) {
	t.Helper()
	b := readFile(t, path)
	if err := json.Unmarshal([]byte(b), dst); err != nil {
		t.Fatalf("decode %s: %v", path, err)
	}
}

func readFile(t *testing.T, path string) string {
	t.Helper()
	b, err := os.ReadFile(path)
	if err != nil {
		t.Fatalf("read %s: %v", path, err)
	}
	return string(b)
}

func mustBeJSON(t *testing.T, path string) {
	t.Helper()
	var v any
	mustDecode(t, path, &v)
}

func validateTraceExample(t v1.Trace) error {
	if t.Schema != "voc-trace.v1" {
		return errors.New("trace.schema must be voc-trace.v1")
	}
	if len(t.StationID) != 24 {
		return errors.New("trace.stationId must be 24 chars")
	}
	if t.TimeSpanS <= 0 {
		return errors.New("trace.timeSpanS must be > 0")
	}
	if t.DtS <= 0 {
		return errors.New("trace.dtS must be > 0")
	}
	if len(t.Values) < 2 {
		return errors.New("trace.values must have at least 2 points")
	}
	if t.Unit == "" {
		return errors.New("trace.unit must not be empty")
	}
	if _, err := time.Parse(time.RFC3339, t.DataTime); err != nil {
		return errors.New("trace.dataTime must be RFC3339")
	}
	return nil
}

func validateMethodExample(m v1.Method) error {
	if m.Schema != "voc-method.v1" {
		return errors.New("method.schema must be voc-method.v1")
	}
	if m.Version < 1 {
		return errors.New("method.version must be >= 1")
	}
	if len(m.Pollutants) == 0 {
		return errors.New("method.pollutants must not be empty")
	}
	for _, p := range m.Pollutants {
		if p.Code == "" || p.Name == "" {
			return errors.New("pollutant code/name must not be empty")
		}
		if p.StartS < 0 || p.EndS < 0 || p.EndS < p.StartS {
			return errors.New("pollutant startS/endS invalid")
		}
		if p.PaddingS < 0 {
			return errors.New("pollutant paddingS must be >= 0")
		}
	}
	return nil
}

func validateResultExample(r v1.Result) error {
	if r.Schema != "voc-result.v1" {
		return errors.New("result.schema must be voc-result.v1")
	}
	if len(r.StationID) != 24 {
		return errors.New("result.stationId must be 24 chars")
	}
	if _, err := time.Parse(time.RFC3339, r.CreatedAt); err != nil {
		return errors.New("result.createdAt must be RFC3339")
	}
	if r.Engine.Name == "" || r.Engine.Version == "" || r.Engine.GitSHA == "" {
		return errors.New("result.engine fields must not be empty")
	}
	if len(r.Pollutants) == 0 {
		return errors.New("result.pollutants must not be empty")
	}
	for _, p := range r.Pollutants {
		if p.Code == "" || p.Name == "" {
			return errors.New("result pollutant code/name must not be empty")
		}
		if p.Status != "detected" && p.Status != "not_detected" {
			return errors.New("result pollutant status invalid")
		}
		if p.RtS < 0 || p.Area < 0 || p.Height < 0 {
			return errors.New("result numeric fields must be >= 0")
		}
	}
	return nil
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
