package sila2

import (
	"context"
	"fmt"

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
