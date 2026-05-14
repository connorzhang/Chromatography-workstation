package main

import (
	"net/http"
	"os"
	"time"

	"chromatography-workstation/edge/internal/analyzer"
	contracts "chromatography-workstation/edge/internal/contracts/v1"
	"chromatography-workstation/edge/internal/httpjson"
)

type analyzeRequest struct {
	Trace  contracts.Trace  `json:"trace"`
	Method contracts.Method `json:"method"`
}

func main() {
	port := os.Getenv("EDGE_ANALYZER_PORT")
	if port == "" {
		port = "8081"
	}
	gitSHA := os.Getenv("EDGE_GIT_SHA")
	if gitSHA == "" {
		gitSHA = "dev"
	}

	mux := http.NewServeMux()
	mux.HandleFunc("/healthz", func(w http.ResponseWriter, r *http.Request) {
		httpjson.WriteJSON(w, http.StatusOK, map[string]any{"ok": true})
	})
	mux.HandleFunc("/analyzer/v1/analyze", func(w http.ResponseWriter, r *http.Request) {
		if r.Method != http.MethodPost {
			httpjson.WriteError(w, http.StatusMethodNotAllowed, "method not allowed")
			return
		}
		var req analyzeRequest
		if err := httpjson.ReadJSON(r, &req); err != nil {
			httpjson.WriteError(w, http.StatusBadRequest, err.Error())
			return
		}
		res, err := analyzer.Analyze(req.Trace, req.Method, gitSHA, time.Now())
		if err != nil {
			httpjson.WriteError(w, http.StatusBadRequest, err.Error())
			return
		}
		httpjson.WriteJSON(w, http.StatusOK, res)
	})

	_ = http.ListenAndServe(":"+port, mux)
}
