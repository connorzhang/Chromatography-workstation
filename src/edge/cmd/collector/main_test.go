package main

import (
	"encoding/json"
	"strings"
	"testing"
	"time"
)

func TestSamplesEventIncludesChannelZero(t *testing.T) {
	e := event{Type: "samples", DeviceID: "GCX", At: time.Unix(0, 0).UTC(), Channel: 0, DTs: 0.1, T0s: 0, Values: []float64{1, 2}}
	b, err := json.Marshal(e)
	if err != nil {
		t.Fatal(err)
	}
	s := string(b)
	if !strings.Contains(s, "\"channel\":0") {
		t.Fatalf("expected channel field, got: %s", s)
	}
}
