package main

import (
	"fmt"
	"os"
	"path/filepath"
	"sync"
	"time"

	v1 "chromatography-workstation/edge/internal/contracts/v1"
	"chromatography-workstation/edge/internal/protocol/chromsend143"
	"chromatography-workstation/edge/internal/realtime"
)

func alertf(format string, args ...any) {
	msg := fmt.Sprintf(format, args...)
	LogWarnf(msg)
	_ = os.MkdirAll(filepath.Join(".run"), 0o755)
	f, err := os.OpenFile(filepath.Join(".run", "alerts.log"), os.O_CREATE|os.O_APPEND|os.O_WRONLY, 0o644)
	if err != nil {
		return
	}
	_, _ = f.WriteString(time.Now().Format(time.RFC3339) + " " + msg + "\n")
	_ = f.Close()
}

func runTCPForever(port int, hub *realtime.Hub, states *sync.Map, cfg chromsend143.Config, method v1.Method) {
	backoff := 1 * time.Second
	for {
		err := serveTCP(port, hub, states, cfg, method)
		if err == nil {
			backoff = 1 * time.Second
			continue
		}
		alertf("collector tcp listener stopped: %v", err)
		time.Sleep(backoff)
		if backoff < 30*time.Second {
			backoff *= 2
		}
	}
}

func runHTTPForever(port int, hub *realtime.Hub, states *sync.Map, allowControl bool, method v1.Method) {
	backoff := 1 * time.Second
	for {
		err := serveHTTP(port, hub, states, allowControl, method)
		if err == nil {
			backoff = 1 * time.Second
			continue
		}
		alertf("collector http stopped: %v", err)
		time.Sleep(backoff)
		if backoff < 30*time.Second {
			backoff *= 2
		}
	}
}
