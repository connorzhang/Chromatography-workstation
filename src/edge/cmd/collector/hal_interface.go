package main

import "errors"

// ErrNotSupported indicates that the requested operation is not supported by the hardware driver.
var ErrNotSupported = errors.New("not supported by this hardware driver")

// TempDriver defines the standard interface for temperature control modules.
type TempDriver interface {
	StartTempControl() error
	StopTempControl() error
	QueryTempSetpoints() error
	SetTempSetpoints(setpoints []float64, protects []float64, enables []bool) error
}

// EventDriver defines the standard interface for event/relay control modules.
type EventDriver interface {
	QueryEvents() error
	SetEvents(matrix [8][8]float64) error
}

// EPCDriver defines the standard interface for electronic pneumatic control modules.
type EPCDriver interface {
	SetEPC(epcs map[string]float64) error
}

// CycleDriver defines the standard interface for time cycle control.
type CycleDriver interface {
	QueryCycleParams() error
	SetCycleParams(count int, intervalMin float64) error
}

// IgniteDriver defines the standard interface for ignition control.
type IgniteDriver interface {
	QueryIgniteParams() error
	SetIgniteParams(threshold1, threshold2 byte, durationByte byte) error
	Ignite(detector string, start bool) error
}

// AnalysisDriver defines the standard interface for analysis lifecycle control.
type AnalysisDriver interface {
	StartAnalysis(channel byte) error
	StopAnalysis() error
	StopAnalysisChannel(channel byte) error
	RequestStop(channelMask byte) error
}

// Capabilities represents the hardware capabilities of an instrument driver.
type Capabilities struct {
	HasIgnition bool     `json:"has_ignition"`
	HasCycles   bool     `json:"has_cycles"`
	HasEPC      bool     `json:"has_epc"`
	HasEvents   bool     `json:"has_events"`
	Detectors   []string `json:"detectors"`
}

// InstrumentDriver aggregates all sub-drivers to represent a complete chromatograph.
type InstrumentDriver interface {
	TempDriver
	EventDriver
	EPCDriver
	CycleDriver
	IgniteDriver
	AnalysisDriver
	Capabilities() Capabilities
	SendRawCmd(cmd byte, payload []byte) error // Temporary escape hatch for unmigrated cmds
}
