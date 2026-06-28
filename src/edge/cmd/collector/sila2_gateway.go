package main

import (
	"context"
	"encoding/json"
	"net/http"
	"strings"
	"sync"

	"chromatography-workstation/edge/internal/sila2"
	pb "chromatography-workstation/edge/internal/sila2/pb"
)

// RegisterSiLA2HTTPGateway maps HTTP REST calls from frontend directly to the true SiLA 2 gRPC methods.
func RegisterSiLA2HTTPGateway(mux *http.ServeMux, states *sync.Map, allowControl bool) {
	mux.HandleFunc("/api/sila2/v1/ChromatographService/StartRun", func(w http.ResponseWriter, r *http.Request) {
		handleSiLA2GatewayCommand(w, r, states, allowControl, func(server *sila2.SilaServer) (any, error) {
			return server.StartRun(context.Background(), &pb.StartRun_Parameters{})
		}, 0xFF) // 0xFF denotes StartAll for hardware
	})

	mux.HandleFunc("/api/sila2/v1/ChromatographService/StopRun", func(w http.ResponseWriter, r *http.Request) {
		handleSiLA2GatewayCommand(w, r, states, allowControl, func(server *sila2.SilaServer) (any, error) {
			return server.StopRun(context.Background(), &pb.StopRun_Parameters{})
		}, 0x00) // 0x00 denotes Stop for hardware
	})

	mux.HandleFunc("/api/sila2/v1/ChromatographService/PauseRun", func(w http.ResponseWriter, r *http.Request) {
		handleSiLA2GatewayCommand(w, r, states, allowControl, func(server *sila2.SilaServer) (any, error) {
			return server.PauseRun(context.Background(), &pb.PauseRun_Parameters{})
		}, 0x01) // Pause doesn't typically map to a legacy hardware command in our current setup
	})

	mux.HandleFunc("/api/sila2/v1/ChromatographService/ResumeRun", func(w http.ResponseWriter, r *http.Request) {
		handleSiLA2GatewayCommand(w, r, states, allowControl, func(server *sila2.SilaServer) (any, error) {
			return server.ResumeRun(context.Background(), &pb.ResumeRun_Parameters{})
		}, 0x01)
	})

	mux.HandleFunc("/api/sila2/v1/ChromatographService/AbortRun", func(w http.ResponseWriter, r *http.Request) {
		handleSiLA2GatewayCommand(w, r, states, allowControl, func(server *sila2.SilaServer) (any, error) {
			return server.AbortRun(context.Background(), &pb.AbortRun_Parameters{})
		}, 0x00)
	})

	mux.HandleFunc("/api/sila2/v1/ChromatographService/GetState", func(w http.ResponseWriter, r *http.Request) {
		if r.Method != http.MethodGet {
			http.Error(w, "Method not allowed", http.StatusMethodNotAllowed)
			return
		}
		deviceID := strings.TrimSpace(r.URL.Query().Get("deviceId"))
		if deviceID == "" {
			deviceID = uiLastDevice
		}
		stAny, ok := states.Load(deviceID)
		if !ok {
			http.Error(w, "Device state not initialized", http.StatusInternalServerError)
			return
		}
		st := stAny.(*deviceState)
		if st.Twin == nil {
			http.Error(w, "Digital Twin not available", http.StatusInternalServerError)
			return
		}

		server := sila2.NewSilaServer(st.Twin)
		res, err := server.GetState(context.Background(), &pb.GetState_Parameters{})
		if err != nil {
			http.Error(w, err.Error(), http.StatusInternalServerError)
			return
		}
		w.Header().Set("Content-Type", "application/json")
		json.NewEncoder(w).Encode(res)
	})
}

func handleSiLA2GatewayCommand(w http.ResponseWriter, r *http.Request, states *sync.Map, allowControl bool, grpcCall func(*sila2.SilaServer) (any, error), hwAction byte) {
	if r.Method != http.MethodPost {
		http.Error(w, "Method not allowed", http.StatusMethodNotAllowed)
		return
	}
	if !allowControl {
		writeJSON(w, http.StatusForbidden, map[string]any{"error": "control disabled: set EDGE_ALLOW_CONTROL=1"})
		return
	}

	deviceID := strings.TrimSpace(r.URL.Query().Get("deviceId"))
	if deviceID == "" {
		deviceID = uiLastDevice
	}

	stAny, ok := states.Load(deviceID)
	if !ok {
		writeJSON(w, http.StatusInternalServerError, map[string]any{"error": "Device state not initialized"})
		return
	}
	st := stAny.(*deviceState)
	if st.Twin == nil {
		writeJSON(w, http.StatusInternalServerError, map[string]any{"error": "Digital Twin not available"})
		return
	}

	// 1. Call standard gRPC method on the Twin via our server instance
	server := sila2.NewSilaServer(st.Twin)
	res, err := grpcCall(server)
	if err != nil {
		writeJSON(w, http.StatusInternalServerError, map[string]any{"error": err.Error()})
		return
	}

	// 2. Hardware mapping (since gRPC currently only changes state in memory)
	driver := getDriver(st, deviceID)
	if hwAction == 0xFF { // Start All
		_ = driver.StartAnalysis(0xFF)
		resetAllSessions(st)
	} else if hwAction == 0x00 && (strings.Contains(r.URL.Path, "StopRun") || strings.Contains(r.URL.Path, "AbortRun")) {
		_ = driver.StopAnalysis()
	}

	w.Header().Set("Content-Type", "application/json")
	json.NewEncoder(w).Encode(res)
}
