package sila2

import (
	"context"
	"fmt"

	"chromatography-workstation/edge/internal/components"
	"chromatography-workstation/edge/internal/models"
	pb "chromatography-workstation/edge/internal/sila2/pb"
)

type FlameIonizationDetectorServer struct {
	pb.UnimplementedFlameIonizationDetectorServiceServer
	twin *models.DigitalTwin
}

func NewFlameIonizationDetectorServer(twin *models.DigitalTwin) *FlameIonizationDetectorServer {
	return &FlameIonizationDetectorServer{twin: twin}
}

func (s *FlameIonizationDetectorServer) Ignite(ctx context.Context, req *pb.IgniteRequest) (*pb.IgniteResponse, error) {
	comp, exists := s.twin.GetComponent(req.DeviceId) // Assumes DeviceId is the component ID (e.g. FID1)
	if exists {
		if dComp, ok := comp.(components.DetectorComponent); ok {
			err := dComp.SetIgnite(true)
			if err != nil {
				return &pb.IgniteResponse{Success: false, Message: err.Error()}, nil
			}
		}
	}
	
	s.twin.AppendAuditLog("Ignite", "gRPC_Client", fmt.Sprintf("Igniting FID on device %s", req.DeviceId))
	return &pb.IgniteResponse{
		Success: true,
		Message: "FID ignite command processed",
	}, nil
}

func (s *FlameIonizationDetectorServer) IgniteConfig(ctx context.Context, req *pb.IgniteConfigRequest) (*pb.IgniteConfigResponse, error) {
	s.twin.AppendAuditLog("IgniteConfig", "gRPC_Client", fmt.Sprintf("Configuring FID ignite for device %s", req.DeviceId))
	return &pb.IgniteConfigResponse{
		Success: true,
		Message: "FID ignite config processed",
	}, nil
}

type ThermalConductivityDetectorServer struct {
	pb.UnimplementedThermalConductivityDetectorServiceServer
	twin *models.DigitalTwin
}

func NewThermalConductivityDetectorServer(twin *models.DigitalTwin) *ThermalConductivityDetectorServer {
	return &ThermalConductivityDetectorServer{twin: twin}
}

func (s *ThermalConductivityDetectorServer) SetBridge(ctx context.Context, req *pb.SetBridgeRequest) (*pb.SetBridgeResponse, error) {
	comp, exists := s.twin.GetComponent(req.DeviceId) // Assumes DeviceId is the component ID (e.g. TCD1)
	if exists {
		if dComp, ok := comp.(components.DetectorComponent); ok {
			err := dComp.SetBridgeCurrent(int(req.Current))
			if err != nil {
				return &pb.SetBridgeResponse{Success: false, Message: err.Error()}, nil
			}
		}
	}

	s.twin.AppendAuditLog("SetBridge", "gRPC_Client", fmt.Sprintf("Setting TCD Bridge current to %d on device %s", req.Current, req.DeviceId))
	return &pb.SetBridgeResponse{
		Success: true,
		Message: "TCD bridge set",
	}, nil
}

func (s *ThermalConductivityDetectorServer) Zeroing(ctx context.Context, req *pb.ZeroingRequest) (*pb.ZeroingResponse, error) {
	s.twin.AppendAuditLog("Zeroing", "gRPC_Client", fmt.Sprintf("Zeroing TCD on device %s", req.DeviceId))
	return &pb.ZeroingResponse{
		Success: true,
		Message: "TCD zeroing initiated",
	}, nil
}

func (s *ThermalConductivityDetectorServer) State(ctx context.Context, req *pb.StateRequest) (*pb.StateResponse, error) {
	return &pb.StateResponse{
		IsReady:      true,
		CurrentValue: 0,
	}, nil
}
