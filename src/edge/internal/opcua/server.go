package opcua

import (
	"context"
	"log"
	"time"

	"chromatography-workstation/edge/internal/models"

	"github.com/gopcua/opcua/server"
	"github.com/gopcua/opcua/ua"
)

type Server struct {
	srv  *server.Server
	twin *models.DigitalTwin
}

func NewServer(twin *models.DigitalTwin, port int) (*Server, error) {
	opts := []server.Option{
		server.EndPoint("0.0.0.0", port),
	}

	srv := server.New(opts...)

	// Create a namespace for LADS
	ns := server.NewNodeNameSpace(srv, "http://lads.chromatography.local/")
	
	// Add nodes mapping to Digital Twin
	// LADS Basic Information
	ns.AddNewVariableNode("DeviceID", twin.DeviceID)

	// LADS State Machine
	stateNode := ns.AddNewVariableNode("State", string(twin.GetState()))
	cycleCountNode := ns.AddNewVariableNode("CurrentCycleCount", int32(twin.CurrentCycleCount))
	targetCycleCountNode := ns.AddNewVariableNode("TargetCycleCount", int32(twin.TargetCycleCount))
	cycleIntervalNode := ns.AddNewVariableNode("CycleInterval", float64(twin.CycleInterval))

	// LADS Alarms & Conditions (Simplified placeholder array)
	alarmsNode := ns.AddNewVariableNode("ActiveAlarms", []string{})
	alarmNodes := make(map[string]*server.Node)
	
	// LADS Audit Trail (Simplified placeholder string)
	auditNode := ns.AddNewVariableNode("LastAuditLog", "")

	// Component Nodes Registry
	componentNodes := make(map[string]*server.Node)

	go func() {
		for {
			time.Sleep(1 * time.Second)
			twin.Mu.RLock()
			currentState := string(twin.CurrentState)
			currentCycleCount := int32(twin.CurrentCycleCount)
			targetCycleCount := int32(twin.TargetCycleCount)
			cycleInterval := float64(twin.CycleInterval)
			
			// Copy slices/strings to avoid race conditions
			var activeAlarms []string
			if twin.ActiveAlarms != nil {
				activeAlarms = make([]string, len(twin.ActiveAlarms))
				copy(activeAlarms, twin.ActiveAlarms)
			} else {
				activeAlarms = []string{}
			}
			lastAuditLog := twin.LastAuditLog

			// Copy components for mapping
			type compData struct {
				ID    string
				State string
			}
			var currentComps []compData
			for _, c := range twin.Components {
				currentComps = append(currentComps, compData{
					ID:    c.GetID(),
					State: string(c.GetState()),
				})
			}
			twin.Mu.RUnlock()

			// Update Component Nodes
			for _, c := range currentComps {
				nodeID := "Component_" + c.ID + "_State"
				node, exists := componentNodes[nodeID]
				if !exists {
					node = ns.AddNewVariableNode(nodeID, c.State)
					componentNodes[nodeID] = node
				}
				ns.SetAttribute(node.ID(), ua.AttributeIDValue, &ua.DataValue{
					EncodingMask:    1,
					Value:           ua.MustVariant(c.State),
					Status:          ua.StatusOK,
					SourceTimestamp: time.Now(),
				})
			}

			ns.SetAttribute(stateNode.ID(), ua.AttributeIDValue, &ua.DataValue{
				EncodingMask:    1,
				Value:           ua.MustVariant(currentState),
				Status:          ua.StatusOK,
				SourceTimestamp: time.Now(),
			})
			ns.SetAttribute(cycleCountNode.ID(), ua.AttributeIDValue, &ua.DataValue{
				EncodingMask:    1,
				Value:           ua.MustVariant(currentCycleCount),
				Status:          ua.StatusOK,
				SourceTimestamp: time.Now(),
			})
			ns.SetAttribute(targetCycleCountNode.ID(), ua.AttributeIDValue, &ua.DataValue{
				EncodingMask:    1,
				Value:           ua.MustVariant(targetCycleCount),
				Status:          ua.StatusOK,
				SourceTimestamp: time.Now(),
			})
			ns.SetAttribute(cycleIntervalNode.ID(), ua.AttributeIDValue, &ua.DataValue{
				EncodingMask:    1,
				Value:           ua.MustVariant(cycleInterval),
				Status:          ua.StatusOK,
				SourceTimestamp: time.Now(),
			})
			
			// Update Alarms and Audit nodes
			ns.SetAttribute(alarmsNode.ID(), ua.AttributeIDValue, &ua.DataValue{
				EncodingMask:    1,
				Value:           ua.MustVariant(activeAlarms),
				Status:          ua.StatusOK,
				SourceTimestamp: time.Now(),
			})

			// Dynamic LADS Alarm Condition Nodes
			// Reset all known alarms to false first, then set active ones to true
			for _, node := range alarmNodes {
				ns.SetAttribute(node.ID(), ua.AttributeIDValue, &ua.DataValue{
					EncodingMask:    1,
					Value:           ua.MustVariant(false),
					Status:          ua.StatusOK,
					SourceTimestamp: time.Now(),
				})
			}
			for _, alarmName := range activeAlarms {
				nodeID := "Alarm_" + alarmName
				node, exists := alarmNodes[nodeID]
				if !exists {
					node = ns.AddNewVariableNode(nodeID, true)
					alarmNodes[nodeID] = node
				}
				ns.SetAttribute(node.ID(), ua.AttributeIDValue, &ua.DataValue{
					EncodingMask:    1,
					Value:           ua.MustVariant(true),
					Status:          ua.StatusOK,
					SourceTimestamp: time.Now(),
				})
			}

			ns.SetAttribute(auditNode.ID(), ua.AttributeIDValue, &ua.DataValue{
				EncodingMask:    1,
				Value:           ua.MustVariant(lastAuditLog),
				Status:          ua.StatusOK,
				SourceTimestamp: time.Now(),
			})
		}
	}()

	return &Server{
		srv:  srv,
		twin: twin,
	}, nil
}

func (s *Server) Start(ctx context.Context) error {
        log.Printf("Starting OPC-UA Server...")
        return s.srv.Start(ctx)
}

func (s *Server) Stop() {
	s.srv.Close()
}
