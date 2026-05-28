package main

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
	SetEPC(payload []byte) error // TODO: abstract payload into a structured format
}

// AnalysisDriver defines the standard interface for analysis lifecycle control.
type AnalysisDriver interface {
	StartAnalysis(channel byte) error
	StopAnalysis() error
	RequestStop(channelMask byte) error
	Ignite(detector string, start bool) error
}

// InstrumentDriver aggregates all sub-drivers to represent a complete chromatograph.
type InstrumentDriver interface {
	TempDriver
	EventDriver
	EPCDriver
	AnalysisDriver
	SendRawCmd(cmd byte, payload []byte) error // Temporary escape hatch for unmigrated cmds
}
