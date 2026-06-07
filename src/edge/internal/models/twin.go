package models

import (
	"chromatography-workstation/edge/internal/components"
	"fmt"
	"sync"
	"time"
)

// TwinState defines the standard state machine for a chromatography device,
// aligning with the OPC-UA LADS (Laboratory Analytical Device Standard)
// and SiLA 2 standard device control state behaviors.
type TwinState string

const (
	// StateIdle indicates the device is idle and not actively controlling.
	StateIdle TwinState = "Idle"

	// StatePreProcessing indicates the device is preparing for a run
	// (e.g., heating up the column oven, waiting for baseline stability).
	StatePreProcessing TwinState = "PreProcessing"

	// StateReady indicates the device is ready to accept a Start command.
	StateReady TwinState = "Ready"

	// StateRunning indicates the device is currently executing a method/acquiring data.
	StateRunning TwinState = "Running"

	// StatePostProcessing indicates the device has finished data acquisition
	// and is performing post-run activities (e.g., purging, cooling down).
	StatePostProcessing TwinState = "PostProcessing"

	// StateError indicates a hardware or software fault.
	StateError TwinState = "Error"
)

// DigitalTwin represents the standardized abstraction of the chromatography instrument.
// This structure serves as the single source of truth (Middleware/Digital Twin)
// connecting the Southbound hardware drivers (HAL) with the Northbound standard protocols (OPC-UA, SiLA 2).
type DigitalTwin struct {
	Mu sync.RWMutex

	// DeviceID is the unique identifier for the instrument.
	DeviceID string

	// CurrentState reflects the standard state machine status.
	CurrentState TwinState

	// Cycle properties
	CurrentCycleCount int
	TargetCycleCount  int
	CycleInterval     float64
	CycleIntervalMin  float64
	MaxCycleCount     int
	SamplingInterval  float64 // dtS

	// LADS Alarms & Audit
	ActiveAlarms []string
	LastAuditLog string
	AuditTrail   []string // Keep a short history of standard audit logs

	// Analytical Results
	LatestResults map[string]float64

	// Components registry holding all standard hardware components (LADS Component)
	Components map[string]components.LadsComponent

	// Method properties
	ActiveMethodID string

	UpdatedAt time.Time

	// OnStateChange is a callback triggered whenever the CurrentState changes.
	OnStateChange func(deviceID string, newState TwinState)

	// OnAlarmsChange is a callback triggered whenever the ActiveAlarms list changes.
	OnAlarmsChange func(deviceID string, activeAlarms []string)

	// OnAuditLogChange is a callback triggered whenever a new audit log is appended.
	OnAuditLogChange func(deviceID string, user string, action string, details string)

	// OnResultsChange is a callback triggered whenever new analytical results are available.
	OnResultsChange func(deviceID string, results map[string]float64)
}

// NewDigitalTwin creates a new instance of a standardized digital twin.
func NewDigitalTwin(deviceID string) *DigitalTwin {
	return &DigitalTwin{
		DeviceID:      deviceID,
		CurrentState:  StateIdle,
		Components:    make(map[string]components.LadsComponent),
		LatestResults: make(map[string]float64),
		UpdatedAt:     time.Now(),
	}
}

// RegisterComponent adds a new hardware component to the digital twin's registry.
func (dt *DigitalTwin) RegisterComponent(comp components.LadsComponent) {
	dt.Mu.Lock()
	defer dt.Mu.Unlock()
	dt.Components[comp.GetID()] = comp
}

// GetComponent retrieves a registered hardware component by its ID.
func (dt *DigitalTwin) GetComponent(id string) (components.LadsComponent, bool) {
	dt.Mu.RLock()
	defer dt.Mu.RUnlock()
	comp, exists := dt.Components[id]
	return comp, exists
}

// SetAlarms updates the active alarms and triggers the callback if changed.
func (dt *DigitalTwin) SetAlarms(alarms []string) {
	dt.Mu.Lock()
	dt.ActiveAlarms = alarms
	dt.UpdatedAt = time.Now()
	cb := dt.OnAlarmsChange
	dt.Mu.Unlock()

	if cb != nil {
		cb(dt.DeviceID, alarms)
	}
}

// TriggerAlarm adds a new alarm to the active alarms list if it doesn't exist.
func (dt *DigitalTwin) TriggerAlarm(alarm string) {
	dt.Mu.Lock()
	exists := false
	for _, a := range dt.ActiveAlarms {
		if a == alarm {
			exists = true
			break
		}
	}
	var currentAlarms []string
	if !exists {
		dt.ActiveAlarms = append(dt.ActiveAlarms, alarm)
		dt.UpdatedAt = time.Now()
	}
	currentAlarms = make([]string, len(dt.ActiveAlarms))
	copy(currentAlarms, dt.ActiveAlarms)
	cb := dt.OnAlarmsChange
	dt.Mu.Unlock()

	if !exists && cb != nil {
		cb(dt.DeviceID, currentAlarms)
	}
}

// ClearAlarm removes an alarm from the active alarms list.
func (dt *DigitalTwin) ClearAlarm(alarm string) {
	dt.Mu.Lock()
	idx := -1
	for i, a := range dt.ActiveAlarms {
		if a == alarm {
			idx = i
			break
		}
	}
	var currentAlarms []string
	if idx != -1 {
		dt.ActiveAlarms = append(dt.ActiveAlarms[:idx], dt.ActiveAlarms[idx+1:]...)
		dt.UpdatedAt = time.Now()
	}
	currentAlarms = make([]string, len(dt.ActiveAlarms))
	copy(currentAlarms, dt.ActiveAlarms)
	cb := dt.OnAlarmsChange
	dt.Mu.Unlock()

	if idx != -1 && cb != nil {
		cb(dt.DeviceID, currentAlarms)
	}
}

// UpdateState transitions the digital twin to a new standard state.
func (dt *DigitalTwin) UpdateState(newState TwinState) {
	dt.Mu.Lock()
	changed := dt.CurrentState != newState
	dt.CurrentState = newState
	dt.UpdatedAt = time.Now()
	cb := dt.OnStateChange
	dt.Mu.Unlock()

	if changed && cb != nil {
		cb(dt.DeviceID, newState)
	}
}

// AppendAuditLog adds a new standardized audit trail record.
func (dt *DigitalTwin) AppendAuditLog(action, user, details string) {
	dt.Mu.Lock()
	entry := fmt.Sprintf(`{"time":"%s","user":"%s","action":"%s","details":"%s"}`,
		time.Now().Format(time.RFC3339), user, action, details)
	dt.LastAuditLog = entry
	dt.AuditTrail = append(dt.AuditTrail, entry)
	if len(dt.AuditTrail) > 50 {
		dt.AuditTrail = dt.AuditTrail[1:]
	}
	dt.UpdatedAt = time.Now()
	cb := dt.OnAuditLogChange
	dt.Mu.Unlock()

	if cb != nil {
		cb(dt.DeviceID, user, action, details)
	}
}

// UpdateResults updates the analytical results cache.
func (dt *DigitalTwin) UpdateResults(results map[string]float64) {
	dt.Mu.Lock()
	if dt.LatestResults == nil {
		dt.LatestResults = make(map[string]float64)
	}
	for k, v := range results {
		dt.LatestResults[k] = v
	}
	dt.UpdatedAt = time.Now()
	// Copy to pass to callback without holding lock
	copied := make(map[string]float64)
	for k, v := range dt.LatestResults {
		copied[k] = v
	}
	cb := dt.OnResultsChange
	dt.Mu.Unlock()

	if cb != nil {
		cb(dt.DeviceID, copied)
	}
}

// GetState returns the current standard state.
func (dt *DigitalTwin) GetState() TwinState {
	dt.Mu.RLock()
	defer dt.Mu.RUnlock()
	return dt.CurrentState
}
