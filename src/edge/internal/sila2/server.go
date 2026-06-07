package sila2

import (
        "context"
        "fmt"
        "net"

        pb "chromatography-workstation/edge/internal/sila2/pb"
        "chromatography-workstation/edge/internal/models"

        "google.golang.org/grpc"
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

func (s *SilaServer) StartRun(ctx context.Context, req *pb.StartRunRequest) (*pb.StartRunResponse, error) {
        if s.twin.GetState() != models.StateReady && s.twin.GetState() != models.StateIdle {
                return &pb.StartRunResponse{
                        Success: false,
                        Message: fmt.Sprintf("Cannot start run. Current state is %s", s.twin.GetState()),
                }, nil
        }

        s.twin.UpdateState(models.StateRunning)
        
        // Update cycle count as required by the standard model
        s.twin.Mu.Lock()
        s.twin.CurrentCycleCount++
        s.twin.Mu.Unlock()

        s.twin.AppendAuditLog("StartRun", "gRPC_Client", "Initiated analysis run")

        return &pb.StartRunResponse{
                Success: true,
                Message: "Run started successfully",
        }, nil
}

func (s *SilaServer) StopRun(ctx context.Context, req *pb.StopRunRequest) (*pb.StopRunResponse, error) {
        s.twin.UpdateState(models.StateIdle)
        s.twin.AppendAuditLog("StopRun", "gRPC_Client", "Terminated analysis run manually")
        return &pb.StopRunResponse{
                Success: true,
                Message: "Run stopped successfully",
        }, nil
}

func (s *SilaServer) GetState(ctx context.Context, req *pb.GetStateRequest) (*pb.GetStateResponse, error) {
        s.twin.Mu.RLock()
        count := s.twin.CurrentCycleCount
        target := s.twin.TargetCycleCount
        s.twin.Mu.RUnlock()

        return &pb.GetStateResponse{
                CurrentState: string(s.twin.GetState()),
                CycleCount:   int32(count),
                TargetCycleCount: int32(target),
        }, nil
}

func (s *SilaServer) SetCycleParameters(ctx context.Context, req *pb.SetCycleRequest) (*pb.SetCycleResponse, error) {
        s.twin.Mu.Lock()
        s.twin.TargetCycleCount = int(req.CycleCount)
        s.twin.CycleInterval = req.CycleInterval
        s.twin.Mu.Unlock()
        
        s.twin.AppendAuditLog("SetCycleParameters", "gRPC_Client", fmt.Sprintf("Updated cycle target to %d, interval to %.1f", req.CycleCount, req.CycleInterval))

        return &pb.SetCycleResponse{
                Success: true,
                Message: "Cycle parameters updated successfully",
        }, nil
}

func (s *SilaServer) Subscribe_AnalyticalResults(req *pb.SubscribeAnalyticalResultsRequest, stream pb.ChromatographService_Subscribe_AnalyticalResultsServer) error {
	ch := make(chan map[string]float64, 10)
	
	// Hook into the twin's results change callback
	// Note: In a real production scenario with multiple subscribers,
	// you'd want a registry of channels. For now, we'll replace the global callback.
	originalCb := s.twin.OnResultsChange
	s.twin.Mu.Lock()
	s.twin.OnResultsChange = func(devID string, results map[string]float64) {
		if originalCb != nil {
			originalCb(devID, results)
		}
		select {
		case ch <- results:
		default:
		}
	}
	s.twin.Mu.Unlock()

	// Send initial value immediately
	s.twin.Mu.RLock()
	initialResults := make(map[string]float64)
	for k, v := range s.twin.LatestResults {
		initialResults[k] = v
	}
	s.twin.Mu.RUnlock()
	
	if err := stream.Send(&pb.SubscribeAnalyticalResultsResponse{
		Results: initialResults,
	}); err != nil {
		return err
	}

	defer func() {
		s.twin.Mu.Lock()
		s.twin.OnResultsChange = originalCb
		s.twin.Mu.Unlock()
		close(ch)
	}()

	for {
		select {
		case <-stream.Context().Done():
			return nil
		case results := <-ch:
			err := stream.Send(&pb.SubscribeAnalyticalResultsResponse{
				Results: results,
			})
			if err != nil {
				return err
			}
		}
	}
}

func StartServer(twin *models.DigitalTwin, port int) error {
        lis, err := net.Listen("tcp", fmt.Sprintf(":%d", port))
        if err != nil {
                return fmt.Errorf("failed to listen: %v", err)
        }

        s := grpc.NewServer()
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
