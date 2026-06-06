package components

// ComponentType defines the standard types of hardware components in a chromatograph,
// aligning with OPC-UA LADS and SiLA 2 standard taxonomies.
type ComponentType string

const (
	TypeTemperatureZone ComponentType = "TemperatureZone" // e.g., Oven, Injector, Detector Body
	TypeDetector        ComponentType = "Detector"        // e.g., TCD, FID, ECD
	TypeFlowController  ComponentType = "FlowController"  // e.g., EPC, MFC (Carrier Gas, Air, H2)
	TypeValve           ComponentType = "Valve"           // e.g., 10-port valve, 6-port valve
	TypeAutosampler     ComponentType = "Autosampler"
)

// ComponentState represents the individual readiness state of a specific component.
type ComponentState string

const (
	CompStateOff     ComponentState = "Off"
	CompStateHeating ComponentState = "Heating/Adjusting"
	CompStateReady   ComponentState = "Ready"
	CompStateError   ComponentState = "Error"
)

// LadsComponent is the base interface that ALL hardware parts (散件) MUST implement.
// This is the core of the "Component-Based" architecture.
type LadsComponent interface {
	// Identity
	GetID() string             // Unique identifier (e.g., "Oven1", "TCD_Main")
	GetName() string           // Human-readable name (e.g., "柱箱", "热导池")
	GetType() ComponentType    // The standard category of this component

	// State
	GetState() ComponentState  // Current readiness of this specific part
	
	// Lifecycle
	Initialize() error         // Setup serial ports, Modbus connections, etc.
	Close() error              // Clean cleanup resources
}

// TemperatureComponent defines the standard interface for any heating element.
type TemperatureComponent interface {
	LadsComponent
	
	GetPV() float64            // Process Value (Current Temperature)
	GetSV() float64            // Set Value (Target Temperature)
	SetSV(target float64) error // Command the hardware to heat/cool
}

// DetectorComponent defines the standard interface for any signal acquisition unit.
type DetectorComponent interface {
	LadsComponent
	
	GetSignal() float64        // Current intensity (e.g., pA, mV)
	SetIgnite(on bool) error   // For FID-like detectors
	SetBridgeCurrent(ma int) error // For TCD-like detectors
}

// FlowComponent defines the standard interface for electronic pneumatic controls (EPC).
type FlowComponent interface {
	LadsComponent
	
	GetPressurePV() float64
	GetPressureSV() float64
	SetPressureSV(target float64) error
	
	GetFlowPV() float64
	GetFlowSV() float64
	SetFlowSV(target float64) error
}

// ValveComponent defines the standard interface for switching valves.
type ValveComponent interface {
	LadsComponent
	
	IsOn() bool
	Toggle(on bool) error
}
