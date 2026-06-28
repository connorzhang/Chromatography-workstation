package sila2

import (
	"context"
	"crypto/tls"
	"embed"
	"fmt"
	"net"

	"chromatography-workstation/edge/internal/models"
	pb "chromatography-workstation/edge/internal/sila2/pb"

	"google.golang.org/grpc"
	"google.golang.org/grpc/credentials"
	"google.golang.org/grpc/reflection"
)

type SilaServer struct {
	pb.UnimplementedChromatographServiceServer
	twin *models.DigitalTwin
}

func NewSilaServer(twin *models.DigitalTwin) *SilaServer {
	return &SilaServer{
		twin: twin,
	}
}

func (s *SilaServer) StartRun(ctx context.Context, req *pb.StartRun_Parameters) (*pb.StartRun_Responses, error) {
	if s.twin.GetState() != models.StateIdle {
		return &pb.StartRun_Responses{}, fmt.Errorf("Cannot start run. Current state is %s", s.twin.GetState())
	}

	s.twin.UpdateState(models.StateRunning)

	// Update cycle count as required by the standard model
	s.twin.Mu.Lock()
	s.twin.CurrentCycleCount++
	s.twin.Mu.Unlock()

	s.twin.AppendAuditLog("StartRun", "gRPC_Client", "Initiated analysis run")

	return &pb.StartRun_Responses{}, nil
}

func (s *SilaServer) StopRun(ctx context.Context, req *pb.StopRun_Parameters) (*pb.StopRun_Responses, error) {
	s.twin.UpdateState(models.StateIdle)
	s.twin.AppendAuditLog("StopRun", "gRPC_Client", "Terminated analysis run manually (Graceful Stop)")
	return &pb.StopRun_Responses{}, nil
}

func (s *SilaServer) PauseRun(ctx context.Context, req *pb.PauseRun_Parameters) (*pb.PauseRun_Responses, error) {
	if s.twin.GetState() != models.StateRunning {
		return &pb.PauseRun_Responses{}, fmt.Errorf("Cannot pause. Current state is %s", s.twin.GetState())
	}
	s.twin.UpdateState(models.StatePaused)
	s.twin.AppendAuditLog("PauseRun", "gRPC_Client", "Paused analysis run")
	return &pb.PauseRun_Responses{}, nil
}

func (s *SilaServer) ResumeRun(ctx context.Context, req *pb.ResumeRun_Parameters) (*pb.ResumeRun_Responses, error) {
	if s.twin.GetState() != models.StatePaused {
		return &pb.ResumeRun_Responses{}, fmt.Errorf("Cannot resume. Current state is %s", s.twin.GetState())
	}
	s.twin.UpdateState(models.StateRunning)
	s.twin.AppendAuditLog("ResumeRun", "gRPC_Client", "Resumed analysis run")
	return &pb.ResumeRun_Responses{}, nil
}

func (s *SilaServer) AbortRun(ctx context.Context, req *pb.AbortRun_Parameters) (*pb.AbortRun_Responses, error) {
	s.twin.UpdateState(models.StateAborted)
	s.twin.AppendAuditLog("AbortRun", "gRPC_Client", "Aborted analysis run (Emergency Stop)")
	return &pb.AbortRun_Responses{}, nil
}

func (s *SilaServer) GetState(ctx context.Context, req *pb.GetState_Parameters) (*pb.GetState_Responses, error) {
	return &pb.GetState_Responses{
		CurrentState: &pb.String{Value: string(s.twin.GetState())},
	}, nil
}

//go:embed certs/*
var certsFS embed.FS

func StartServer(twin *models.DigitalTwin, port int) error {
	lis, err := net.Listen("tcp", fmt.Sprintf(":%d", port))
	if err != nil {
		return fmt.Errorf("failed to listen: %v", err)
	}

	certBytes, err := certsFS.ReadFile("certs/server.crt")
	if err != nil {
		return fmt.Errorf("failed to read cert: %v", err)
	}
	keyBytes, err := certsFS.ReadFile("certs/server.key")
	if err != nil {
		return fmt.Errorf("failed to read key: %v", err)
	}

	cert, err := tls.X509KeyPair(certBytes, keyBytes)
	if err != nil {
		return fmt.Errorf("failed to parse key pair: %v", err)
	}

	creds := credentials.NewServerTLSFromCert(&cert)
	s := grpc.NewServer(grpc.Creds(creds))
	pb.RegisterSiLAServiceServer(s, NewSiLAServiceServer(twin))
	pb.RegisterChromatographServiceServer(s, NewSilaServer(twin))
	pb.RegisterTemperatureControllerServiceServer(s, NewTemperatureServer(twin))
	pb.RegisterValveControllerServiceServer(s, NewValveServer(twin))
	pb.RegisterFlameIonizationDetectorServiceServer(s, NewFlameIonizationDetectorServer(twin))
	pb.RegisterThermalConductivityDetectorServiceServer(s, NewThermalConductivityDetectorServer(twin))
	pb.RegisterPneumaticControllerServiceServer(s, NewPneumaticServer(twin))

	// Register reflection service on gRPC server to allow tools like grpcurl to interact with it.
	reflection.Register(s)

	fmt.Printf("[SiLA 2] Starting standard gRPC server on port %d...\n", port)
	if err := s.Serve(lis); err != nil {
		return fmt.Errorf("failed to serve: %v", err)
	}
	return nil
}
