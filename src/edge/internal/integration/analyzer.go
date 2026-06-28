package integration

import (
	"errors"
	"math"
)

// EventType defines the classic Agilent integration events
type EventType string

const (
	EventInitialAreaReject EventType = "InitialAreaReject"
	EventInitialPeakWidth  EventType = "InitialPeakWidth"
	EventTangentSkimMode   EventType = "TangentSkimMode"
	EventBaselineHold      EventType = "BaselineHold"
	EventIntegrationOff    EventType = "IntegrationOff"
	EventIntegrationOn     EventType = "IntegrationOn"
	EventValleyToValley    EventType = "ValleyToValley"
)

// IntegrationEvent maps to the user-facing Agilent event table
type IntegrationEvent struct {
	Time  float64   `json:"time"` // Minutes
	Type  EventType `json:"eventType"`
	Value float64   `json:"value"`
}

// PeakResult is the standardized output (AIA/NetCDF inspired)
type PeakResult struct {
	RetTime      float64 `json:"rtS"` // Seconds
	Area         float64 `json:"area"`
	Height       float64 `json:"height"`
	Width        float64 `json:"width"`
	BaselineType string  `json:"baselineType"` // e.g. "BB", "BV", "VB", "VV", "T"
}

// AnalyzerEngine is the core struct that processes traces based on events
type AnalyzerEngine struct {
	Events []IntegrationEvent
}

func NewAnalyzerEngine(events []IntegrationEvent) *AnalyzerEngine {
	return &AnalyzerEngine{
		Events: events,
	}
}

// Process mimics the classic `ApplyIntegs.Apply` logic, translated to standard Go
func (ae *AnalyzerEngine) Process(times []float64, values []float64) ([]PeakResult, error) {
	if len(times) != len(values) {
		return nil, errors.New("times and values length mismatch")
	}
	if len(times) == 0 {
		return nil, nil
	}

	// 1. Build a timeline of active parameters
	areaReject := 0.0
	peakWidth := 0.0

	// Apply initial values (Time = 0)
	for _, ev := range ae.Events {
		if ev.Time == 0 {
			switch ev.Type {
			case EventInitialAreaReject:
				areaReject = ev.Value
			case EventInitialPeakWidth:
				peakWidth = ev.Value
			}
		}
	}

	// Mocking a basic derivative peak picking algorithm
	var peaks []PeakResult
	isIntegrating := true // controlled by IntegrationOn/Off

	// Mock peak detection (In a real scenario, we use first/second derivatives)
	// Here we just find simple local maxima over a threshold for demonstration
	threshold := 10.0 // arbitrary noise threshold

	for i := 1; i < len(values)-1; i++ {
		// Update dynamic events based on current time
		currentTimeMin := times[i] / 60.0
		for _, ev := range ae.Events {
			if math.Abs(ev.Time-currentTimeMin) < 0.001 { // Time match
				switch ev.Type {
				case EventIntegrationOff:
					isIntegrating = false
				case EventIntegrationOn:
					isIntegrating = true
				case EventInitialAreaReject:
					areaReject = ev.Value
				}
			}
		}

		if !isIntegrating {
			continue
		}

		// Local max detection
		if values[i] > values[i-1] && values[i] > values[i+1] && values[i] > threshold {
			// Calculate mock area
			mockArea := values[i] * peakWidth // simplified
			if mockArea >= areaReject {
				peaks = append(peaks, PeakResult{
					RetTime:      times[i],
					Area:         mockArea,
					Height:       values[i],
					Width:        peakWidth,
					BaselineType: "BB", // Base-to-Base
				})
			}
		}
	}

	return peaks, nil
}
