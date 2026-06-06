package sila2

import (
        "context"
        "fmt"

        pb "chromatography-workstation/edge/internal/sila2/pb"
        "chromatography-workstation/edge/internal/models"
)

type ValveServer struct {
        pb.UnimplementedValveControllerServiceServer
        twin *models.DigitalTwin
}

func NewValveServer(twin *models.DigitalTwin) *ValveServer {
        return &ValveServer{
                twin: twin,
        }
}

func (s *ValveServer) SwitchValve(ctx context.Context, req *pb.SwitchValveRequest) (*pb.SwitchValveResponse, error) {
        _, exists := s.twin.GetComponent(req.ValveId)
        if !exists {
                return &pb.SwitchValveResponse{Success: false, Message: "Valve not found"}, nil
        }

        s.twin.AppendAuditLog("SwitchValve", "gRPC_Client", fmt.Sprintf("Switched Valve %s to position %d", req.ValveId, req.Position))

        return &pb.SwitchValveResponse{
                Success: true,
                Message: fmt.Sprintf("Valve %s switched to %d", req.ValveId, req.Position),
        }, nil
}

func (s *ValveServer) GetValveState(ctx context.Context, req *pb.GetValveStateRequest) (*pb.GetValveStateResponse, error) {
        _, exists := s.twin.GetComponent(req.ValveId)
        if !exists {
                return &pb.GetValveStateResponse{}, fmt.Errorf("Valve not found")
        }

        return &pb.GetValveStateResponse{
                CurrentPosition: 0,
        }, nil
}
