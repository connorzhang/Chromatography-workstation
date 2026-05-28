package main

import (
	"fmt"
	"log"
	"sync"
	"time"
)

type LogEntry struct {
	Time  int64  `json:"time"`
	Level string `json:"level"`
	Msg   string `json:"msg"`
}

type LogBatch struct {
	Timestamp int64      `json:"timestamp"`
	Logs      []LogEntry `json:"logs"`
}

var (
	logMu       sync.Mutex
	logQueue    []LogEntry
	allLogs     []LogEntry // For UI display
	logHubChan  chan LogBatch
	uiLogChan   chan LogEntry
)

func init() {
	logHubChan = make(chan LogBatch, 10)
	uiLogChan = make(chan LogEntry, 100)
	go logWorker()
}

// PushLog is called by any component to add a log
func PushLog(level, msg string) {
	log.Printf("[%s] %s", level, msg)
	logMu.Lock()
	defer logMu.Unlock()
	entry := LogEntry{
		Time:  time.Now().Unix(),
		Level: level,
		Msg:   msg,
	}
	logQueue = append(logQueue, entry)
	allLogs = append(allLogs, entry)
	// Keep last 1000 logs in memory for UI
	if len(allLogs) > 1000 {
		allLogs = allLogs[len(allLogs)-1000:]
	}
	
	// Real-time push to UI
	select {
	case uiLogChan <- entry:
	default:
	}
}

func LogInfof(format string, args ...interface{}) {
	PushLog("INFO", fmt.Sprintf(format, args...))
}

func LogDebugf(format string, args ...interface{}) {
	PushLog("DEBUG", fmt.Sprintf(format, args...))
}

func LogWarnf(format string, args ...interface{}) {
	PushLog("WARN", fmt.Sprintf(format, args...))
}

func LogErrorf(format string, args ...interface{}) {
	PushLog("ERROR", fmt.Sprintf(format, args...))
}

// GetRecentLogs returns all logs in memory for UI initial load
func GetRecentLogs() []LogEntry {
	logMu.Lock()
	defer logMu.Unlock()
	res := make([]LogEntry, len(allLogs))
	copy(res, allLogs)
	return res
}

// logWorker flushes the queue every 15 seconds (core memory requirement)
func logWorker() {
	ticker := time.NewTicker(15 * time.Second)
	for range ticker.C {
		logMu.Lock()
		if len(logQueue) > 0 {
			// Copy and clear the queue
			batch := make([]LogEntry, len(logQueue))
			copy(batch, logQueue)
			logQueue = logQueue[:0]
			logMu.Unlock()

			// Create a batch
			lb := LogBatch{
				Timestamp: time.Now().Unix(),
				Logs:      batch,
			}

			// In the future, this batch will be sent via MQTT (vocs/device/{MN}/log)
			if mqttClient != nil {
				uiMu.Lock()
				deviceID := uiLastDevice
				uiMu.Unlock()
				if deviceID != "" {
					payload := map[string]any{
						"timestamp": lb.Timestamp,
						"logs":      lb.Logs,
					}
					mqttClient.PublishLog(deviceID, payload)
				}
			}

			// For now, we just broadcast it to the UI (if needed) or keep it ready
			select {
			case logHubChan <- lb:
			default:
			}
		} else {
			logMu.Unlock()
		}
	}
}
