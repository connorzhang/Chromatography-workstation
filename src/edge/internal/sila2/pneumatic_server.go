package sila2

import (
	"context"
	"fmt"

	"chromatography-workstation/edge/internal/models"
	pb "chromatography-workstation/edge/internal/sila2/pb"
)

type PneumaticServer struct {
	pb.UnimplementedPneumaticControllerServiceServer
	twin *models.DigitalTwin
}

func NewPneumaticServer(twin *models.DigitalTwin) *PneumaticServer {
	return &PneumaticServer{twin: twin}
}

func (s *PneumaticServer) SetTargetPressure(ctx context.Context, req *pb.SetTargetPressureRequest) (*pb.SetTargetPressureResponse, error) {
	s.twin.AppendAuditLog("SetTargetPressure", "gRPC_Client", fmt.Sprintf("Set pressure for channel %s to %.2f on device %s", req.Channel, req.TargetPressure, req.DeviceId))
	return &pb.SetTargetPressureResponse{
		Success: true,
		Message: fmt.Sprintf("Pressure set for %s", req.Channel),
	}, nil
}
