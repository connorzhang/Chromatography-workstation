package opcua

import (
	"context"
	"log"
	"sync"
	"time"

	"chromatography-workstation/edge/internal/components"
	"chromatography-workstation/edge/internal/models"

	"github.com/gopcua/opcua/id"
	"github.com/gopcua/opcua/server"
	"github.com/gopcua/opcua/ua"
)

type Server struct {
	srv           *server.Server
	ns            *server.NodeNameSpace
	devicesFolder *server.Node
	deviceRoots   map[string]*server.Node
	deviceNodes   map[string]map[string]*server.Node
	twins         *sync.Map // string -> *models.DigitalTwin
	mu            sync.Mutex
}

func NewServer(twins *sync.Map, port int) (*Server, error) {
	opts := []server.Option{
		server.EndPoint("0.0.0.0", port),
		server.EndPoint("127.0.0.1", port),
		server.EndPoint("localhost", port),
		server.EndPoint("10.8.5.50", port),
		server.EnableSecurity("None", ua.MessageSecurityModeNone),
		server.EnableAuthMode(ua.UserTokenTypeAnonymous),
	}

	srv := server.New(opts...)

	// Create a namespace for LADS
	ns := server.NewNodeNameSpace(srv, "http://opcfoundation.org/UA/LADS/")

	// Create a standard folder structure in Root Objects
	rootNs, _ := srv.Namespace(0)
	rootObjNode := rootNs.Objects()

	devicesFolder := server.NewFolderNode(ua.NewNumericNodeID(ns.ID(), ns.GetNextNodeID()), "AnalyticalDevices")
	ns.AddNode(devicesFolder)
	rootObjNode.AddRef(devicesFolder, id.Organizes, true)

	s := &Server{
		srv:           srv,
		ns:            ns,
		devicesFolder: devicesFolder,
		deviceRoots:   make(map[string]*server.Node),
		deviceNodes:   make(map[string]map[string]*server.Node),
		twins:         twins,
	}

	go s.updateLoop()

	return s, nil
}

func (s *Server) createLinkedVar(root *server.Node, name string, value interface{}) *server.Node {
	node := s.ns.AddNewVariableNode(name, value)
	root.AddRef(node, id.HasComponent, true)
	return node
}

func (s *Server) updateLoop() {
	for {
		time.Sleep(1 * time.Second)

		s.twins.Range(func(key, value interface{}) bool {
			deviceID := key.(string)
			twin, ok := value.(*models.DigitalTwin)
			if !ok || twin == nil {
				return true
			}

			s.mu.Lock()
			ladsRoot, exists := s.deviceRoots[deviceID]
			if !exists {
				// Initialize new device folder and base nodes
				ladsRoot = server.NewFolderNode(ua.NewNumericNodeID(s.ns.ID(), s.ns.GetNextNodeID()), deviceID)
				s.ns.AddNode(ladsRoot)
				s.devicesFolder.AddRef(ladsRoot, id.Organizes, true)
				s.deviceRoots[deviceID] = ladsRoot
				s.deviceNodes[deviceID] = make(map[string]*server.Node)
				
				// LADS Basic Information
				s.createLinkedVar(ladsRoot, "DeviceID", twin.DeviceID)
			}
			nodesMap := s.deviceNodes[deviceID]
			s.mu.Unlock()

			// Read current Twin state safely
			twin.Mu.RLock()
			currentState := string(twin.CurrentState)
			currentCycleCount := int32(twin.CurrentCycleCount)
			targetCycleCount := int32(twin.TargetCycleCount)
			cycleInterval := float64(twin.CycleInterval)
			samplingInterval := float64(twin.SamplingInterval)

			var activeAlarms []string
			if twin.ActiveAlarms != nil {
				activeAlarms = make([]string, len(twin.ActiveAlarms))
				copy(activeAlarms, twin.ActiveAlarms)
			} else {
				activeAlarms = []string{}
			}
			lastAuditLog := twin.LastAuditLog

			type compData struct {
				ID    string
				State string
				Type  string
				PV    float64
				SV    float64
			}
			var currentComps []compData
			for _, c := range twin.Components {
				cd := compData{
					ID:    c.GetID(),
					State: string(c.GetState()),
					Type:  string(c.GetType()),
				}
				if tComp, ok := c.(components.TemperatureComponent); ok {
					cd.PV = tComp.GetPV()
					cd.SV = tComp.GetSV()
				} else if dComp, ok := c.(components.DetectorComponent); ok {
					cd.PV = dComp.GetSignal()
				}
				currentComps = append(currentComps, cd)
			}

			currentResults := make(map[string]float64)
			for k, v := range twin.LatestResults {
				currentResults[k] = v
			}
			twin.Mu.RUnlock()

			// Helper to get or create node
			getNode := func(name string, initVal interface{}) *server.Node {
				s.mu.Lock()
				defer s.mu.Unlock()
				node, ok := nodesMap[name]
				if !ok {
					node = s.createLinkedVar(ladsRoot, name, initVal)
					nodesMap[name] = node
				}
				return node
			}

			// Update values
			s.ns.SetAttribute(getNode("State", currentState).ID(), ua.AttributeIDValue, &ua.DataValue{
				EncodingMask: 1, Value: ua.MustVariant(currentState), Status: ua.StatusOK, SourceTimestamp: time.Now(),
			})
			s.ns.SetAttribute(getNode("CurrentCycleCount", currentCycleCount).ID(), ua.AttributeIDValue, &ua.DataValue{
				EncodingMask: 1, Value: ua.MustVariant(currentCycleCount), Status: ua.StatusOK, SourceTimestamp: time.Now(),
			})
			s.ns.SetAttribute(getNode("TargetCycleCount", targetCycleCount).ID(), ua.AttributeIDValue, &ua.DataValue{
				EncodingMask: 1, Value: ua.MustVariant(targetCycleCount), Status: ua.StatusOK, SourceTimestamp: time.Now(),
			})
			s.ns.SetAttribute(getNode("CycleInterval", cycleInterval).ID(), ua.AttributeIDValue, &ua.DataValue{
				EncodingMask: 1, Value: ua.MustVariant(cycleInterval), Status: ua.StatusOK, SourceTimestamp: time.Now(),
			})
			s.ns.SetAttribute(getNode("SamplingInterval", samplingInterval).ID(), ua.AttributeIDValue, &ua.DataValue{
				EncodingMask: 1, Value: ua.MustVariant(samplingInterval), Status: ua.StatusOK, SourceTimestamp: time.Now(),
			})
			s.ns.SetAttribute(getNode("ActiveAlarms", activeAlarms).ID(), ua.AttributeIDValue, &ua.DataValue{
				EncodingMask: 1, Value: ua.MustVariant(activeAlarms), Status: ua.StatusOK, SourceTimestamp: time.Now(),
			})
			s.ns.SetAttribute(getNode("LastAuditLog", lastAuditLog).ID(), ua.AttributeIDValue, &ua.DataValue{
				EncodingMask: 1, Value: ua.MustVariant(lastAuditLog), Status: ua.StatusOK, SourceTimestamp: time.Now(),
			})

			for k, v := range currentResults {
				s.ns.SetAttribute(getNode("Result_"+k, v).ID(), ua.AttributeIDValue, &ua.DataValue{
					EncodingMask: 1, Value: ua.MustVariant(v), Status: ua.StatusOK, SourceTimestamp: time.Now(),
				})
			}

			for _, c := range currentComps {
				s.ns.SetAttribute(getNode("Component_"+c.ID+"_State", c.State).ID(), ua.AttributeIDValue, &ua.DataValue{
					EncodingMask: 1, Value: ua.MustVariant(c.State), Status: ua.StatusOK, SourceTimestamp: time.Now(),
				})
				if c.Type == "TemperatureZone" {
					s.ns.SetAttribute(getNode("Component_"+c.ID+"_PV", c.PV).ID(), ua.AttributeIDValue, &ua.DataValue{
						EncodingMask: 1, Value: ua.MustVariant(c.PV), Status: ua.StatusOK, SourceTimestamp: time.Now(),
					})
					s.ns.SetAttribute(getNode("Component_"+c.ID+"_SV", c.SV).ID(), ua.AttributeIDValue, &ua.DataValue{
						EncodingMask: 1, Value: ua.MustVariant(c.SV), Status: ua.StatusOK, SourceTimestamp: time.Now(),
					})
				} else if c.Type == "Detector" {
					s.ns.SetAttribute(getNode("Component_"+c.ID+"_Signal", c.PV).ID(), ua.AttributeIDValue, &ua.DataValue{
						EncodingMask: 1, Value: ua.MustVariant(c.PV), Status: ua.StatusOK, SourceTimestamp: time.Now(),
					})
				}
			}

			// Clear old alarms
			s.mu.Lock()
			for name, node := range nodesMap {
				if len(name) > 6 && name[:6] == "Alarm_" {
					s.ns.SetAttribute(node.ID(), ua.AttributeIDValue, &ua.DataValue{
						EncodingMask: 1, Value: ua.MustVariant(false), Status: ua.StatusOK, SourceTimestamp: time.Now(),
					})
				}
			}
			s.mu.Unlock()

			// Set active alarms
			for _, alarmName := range activeAlarms {
				s.ns.SetAttribute(getNode("Alarm_"+alarmName, true).ID(), ua.AttributeIDValue, &ua.DataValue{
					EncodingMask: 1, Value: ua.MustVariant(true), Status: ua.StatusOK, SourceTimestamp: time.Now(),
				})
			}

			return true
		})
	}
}

func (s *Server) Start(ctx context.Context) error {
	log.Printf("Starting OPC-UA Server...")
	return s.srv.Start(ctx)
}

func (s *Server) Stop() {
	s.srv.Close()
}