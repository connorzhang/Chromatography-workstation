package license

import (
	"crypto/hmac"
	"crypto/sha256"
	"encoding/hex"
	"encoding/json"
	"fmt"
	"os"
	"path/filepath"
	"time"
)

// tamperSecret should be unique for this application. 
// In production, you might want to obfuscate this.
const tamperSecret = "chromatography_edge_secret_v1_anti_tamper"

type timeState struct {
	LastRun   int64  `json:"last_run"`
	Signature string `json:"signature"`
}

// generateTimeSignature creates an HMAC-SHA256 signature for the timestamp.
func generateTimeSignature(ts int64) string {
	mac := hmac.New(sha256.New, []byte(tamperSecret))
	mac.Write([]byte(fmt.Sprintf("%d", ts)))
	return hex.EncodeToString(mac.Sum(nil))
}

// CheckAndUpdateTime verifies the last run time to prevent time rollback.
// It reads from a hidden file, verifies the HMAC signature, and updates the timestamp.
func CheckAndUpdateTime(dataDir string) error {
	stateFile := filepath.Join(dataDir, ".edge_time.dat")
	now := time.Now().Unix()

	data, err := os.ReadFile(stateFile)
	if err == nil {
		var state timeState
		if err := json.Unmarshal(data, &state); err == nil {
			expectedSig := generateTimeSignature(state.LastRun)
			if state.Signature != expectedSig {
				return fmt.Errorf("系统运行时间记录已被篡改 (HMAC校验失败)")
			}
			
			// Allow a small drift of 1 minute just in case of minor NTP adjustments
			if now < state.LastRun-60 {
				return fmt.Errorf("检测到系统时间被回拨！当前时间: %d, 记录时间: %d", now, state.LastRun)
			}
		}
	}

	// Update the time state
	newState := timeState{
		LastRun:   now,
		Signature: generateTimeSignature(now),
	}
	b, _ := json.Marshal(newState)
	return os.WriteFile(stateFile, b, 0644)
}
