package main

import (
	"encoding/json"
	"log"
	"math"
	"net/http"
	"time"

	"chromatography-workstation/edge/internal/integration"

	"github.com/gorilla/websocket"
)

var upgrader = websocket.Upgrader{
	CheckOrigin: func(r *http.Request) bool {
		return true // Allow all for dev
	},
}

// corsMiddleware adds headers to allow cross-origin requests from the React dev server
func corsMiddleware(next http.Handler) http.Handler {
	return http.HandlerFunc(func(w http.ResponseWriter, r *http.Request) {
		w.Header().Set("Access-Control-Allow-Origin", "*")
		w.Header().Set("Access-Control-Allow-Methods", "POST, GET, OPTIONS, PUT, DELETE")
		w.Header().Set("Access-Control-Allow-Headers", "Accept, Content-Type, Content-Length, Accept-Encoding, X-CSRF-Token, Authorization")
		if r.Method == "OPTIONS" {
			return
		}
		next.ServeHTTP(w, r)
	})
}

func main() {
	mux := http.NewServeMux()

	mux.HandleFunc("/api/v1/method", handleMethod)
	mux.HandleFunc("/api/v1/sequence", handleSequence)
	mux.HandleFunc("/api/v1/analyze", handleAnalyze)
	mux.HandleFunc("/ws/v1/realtime", handleRealtimeWS)

	log.Println("Edge API (Agilent-Clone Gateway) starting on :8080...")
	log.Fatal(http.ListenAndServe(":8080", corsMiddleware(mux)))
}

func handleMethod(w http.ResponseWriter, r *http.Request) {
	w.Header().Set("Content-Type", "application/json")
	w.Write([]byte(`{"status": "ok", "message": "Method fetched/saved"}`))
}

func handleSequence(w http.ResponseWriter, r *http.Request) {
	w.Header().Set("Content-Type", "application/json")
	w.Write([]byte(`{"status": "ok", "message": "Sequence loaded"}`))
}

// handleAnalyze simulates the classic chromatogram processing
func handleAnalyze(w http.ResponseWriter, r *http.Request) {
	if r.Method != http.MethodPost {
		http.Error(w, "Method not allowed", http.StatusMethodNotAllowed)
		return
	}

	events := []integration.IntegrationEvent{
		{Time: 0, Type: integration.EventInitialAreaReject, Value: 5.0},
		{Time: 0, Type: integration.EventInitialPeakWidth, Value: 0.05},
		{Time: 8.0, Type: integration.EventIntegrationOff, Value: 0},
	}

	engine := integration.NewAnalyzerEngine(events)

	// Generate a mock trace
	var times []float64
	var values []float64

	for t := 0.0; t <= 10.0; t += 0.1 {
		v := 10.0 + math.Sin(t)*1.0 // baseline noise

		// Peak 1
		if t > 2.0 && t < 3.0 {
			v += 100 * math.Exp(-math.Pow(t-2.5, 2)/0.05)
		}
		// Peak 2
		if t > 5.0 && t < 6.5 {
			v += 200 * math.Exp(-math.Pow(t-5.75, 2)/0.1)
		}

		times = append(times, t)
		values = append(values, v)
	}

	peaks, err := engine.Process(times, values)
	if err != nil {
		http.Error(w, err.Error(), http.StatusInternalServerError)
		return
	}

	w.Header().Set("Content-Type", "application/json")
	json.NewEncoder(w).Encode(map[string]interface{}{
		"status": "success",
		"peaks":  peaks,
		"trace": map[string]interface{}{
			"times":  times,
			"values": values,
		},
	})
}

// handleRealtimeWS simulates the real-time data stream from Cmd=143
func handleRealtimeWS(w http.ResponseWriter, r *http.Request) {
	c, err := upgrader.Upgrade(w, r, nil)
	if err != nil {
		log.Printf("WS upgrade error: %v", err)
		return
	}
	defer c.Close()

	log.Println("Client connected to realtime plot stream")

	t := 0.0
	for {
		// Simulate baseline and peaks
		v := 10.0 + (math.Sin(t*10) * 0.5) // Noise

		// Simulated peaks at t=2.5 and t=5.75
		if math.Abs(t-2.5) < 0.5 {
			v += 100 * math.Exp(-math.Pow(t-2.5, 2)/0.05)
		}
		if math.Abs(t-5.75) < 0.75 {
			v += 200 * math.Exp(-math.Pow(t-5.75, 2)/0.1)
		}

		err := c.WriteJSON(map[string]float64{
			"time":  t,
			"value": v,
		})
		if err != nil {
			log.Printf("WS write error: %v", err)
			break
		}

		t += 0.1
		time.Sleep(100 * time.Millisecond) // 10Hz data rate
	}
}
