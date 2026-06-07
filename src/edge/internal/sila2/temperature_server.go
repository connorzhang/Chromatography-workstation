package sila2

import (
        "context"
        "fmt"

		"chromatography-workstation/edge/internal/components"
        pb "chromatography-workstation/edge/internal/sila2/pb"
        "chromatography-workstation/edge/internal/models"
)

type TemperatureServer struct {
        pb.UnimplementedTemperatureControllerServiceServer
        twin *models.DigitalTwin
}

func NewTemperatureServer(twin *models.DigitalTwin) *TemperatureServer {
        return &TemperatureServer{
                twin: twin,
        }
}

func (s *TemperatureServer) SetTargetTemperature(ctx context.Context, req *pb.SetTargetTemperatureRequest) (*pb.SetTargetTemperatureResponse, error) {
        // Implement logic to dispatch to HAL via DigitalTwin Components
        comp, exists := s.twin.GetComponent(req.ZoneId)
        if !exists {
                return &pb.SetTargetTemperatureResponse{Success: false, Message: "Zone not found"}, nil
        }
        
        if tComp, ok := comp.(components.TemperatureComponent); ok {
		err := tComp.SetSV(req.TargetTemperature)
		if err != nil {
			return &pb.SetTargetTemperatureResponse{Success: false, Message: err.Error()}, nil
		}
		
		s.twin.AppendAuditLog("SetTargetTemperature", "gRPC_Client", fmt.Sprintf("Set Zone %s target temp to %.2f", req.ZoneId, req.TargetTemperature))
		
		return &pb.SetTargetTemperatureResponse{
			Success: true,
			Message: fmt.Sprintf("Temperature set for %s to %.2f", req.ZoneId, req.TargetTemperature),
		}, nil
	}

	return &pb.SetTargetTemperatureResponse{Success: false, Message: "Component is not a temperature controller"}, nil
}

func (s *TemperatureServer) GetTemperature(ctx context.Context, req *pb.GetTemperatureRequest) (*pb.GetTemperatureResponse, error) {
	comp, exists := s.twin.GetComponent(req.ZoneId)
	if !exists {
		return &pb.GetTemperatureResponse{}, fmt.Errorf("Zone not found")
	}

	if tComp, ok := comp.(components.TemperatureComponent); ok {
		return &pb.GetTemperatureResponse{
			ActualTemperature:  tComp.GetPV(),
			TargetTemperature:  tComp.GetSV(),
			ProtectTemperature: 400.0, // Hardcoded for now
			HeatingEnabled:     tComp.GetState() == components.CompStateHeating || tComp.GetState() == components.CompStateReady,
		}, nil
	}

	return &pb.GetTemperatureResponse{}, fmt.Errorf("Component is not a temperature controller")
}

func (s *TemperatureServer) SetHeatingState(ctx context.Context, req *pb.SetHeatingStateRequest) (*pb.SetHeatingStateResponse, error) {
        _, exists := s.twin.GetComponent(req.ZoneId)
        if !exists {
                return &pb.SetHeatingStateResponse{Success: false, Message: "Zone not found"}, nil
        }

        s.twin.AppendAuditLog("SetHeatingState", "gRPC_Client", fmt.Sprintf("Set Zone %s heating state to %v", req.ZoneId, req.Enable))

        return &pb.SetHeatingStateResponse{
                Success: true,
                Message: fmt.Sprintf("Heating state set to %v for %s", req.Enable, req.ZoneId),
        }, nil
}
