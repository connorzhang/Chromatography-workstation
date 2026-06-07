package license

import (
	"bytes"
	"crypto/sha256"
	"encoding/hex"
	"net"
	"os"
	"strings"
)

// GetMachineID extracts a unique hardware fingerprint.
// It combines the hostname and the MAC addresses of physical network interfaces.
func GetMachineID() string {
	var macs []string
	interfaces, err := net.Interfaces()
	if err == nil {
		for _, i := range interfaces {
			// Skip down interfaces and loopback interfaces
			if i.Flags&net.FlagUp != 0 && i.Flags&net.FlagLoopback == 0 && bytes.Compare(i.HardwareAddr, nil) != 0 {
				macs = append(macs, i.HardwareAddr.String())
			}
		}
	}
	
	hostname, _ := os.Hostname()
	
	// If no MACs found, fallback to just hostname
	raw := hostname + "|" + strings.Join(macs, ",")
	hash := sha256.Sum256([]byte(raw))
	
	// Format as XXXX-XXXX-XXXX-XXXX
	hexStr := strings.ToUpper(hex.EncodeToString(hash[:]))
	return hexStr[0:4] + "-" + hexStr[4:8] + "-" + hexStr[8:12] + "-" + hexStr[12:16]
}
