package license

import (
	"crypto/sha256"
	"encoding/hex"
	"os"
	"os/exec"
	"runtime"
	"sort"
	"strings"
)

func GetMachineID() string {
	parts := hardwareParts()
	if len(parts) == 0 {
		parts = fallbackParts()
	}
	sort.Strings(parts)
	raw := "fixed-hardware|" + strings.Join(parts, "|")
	hash := sha256.Sum256([]byte(raw))
	hexStr := strings.ToUpper(hex.EncodeToString(hash[:]))
	return hexStr[0:4] + "-" + hexStr[4:8] + "-" + hexStr[8:12] + "-" + hexStr[12:16]
}

func hardwareParts() []string {
	if runtime.GOOS == "windows" {
		return windowsHardwareParts()
	}
	return unixHardwareParts()
}

func unixHardwareParts() []string {
	paths := []string{
		"/sys/class/dmi/id/product_uuid",
		"/sys/class/dmi/id/product_serial",
		"/sys/class/dmi/id/board_serial",
		"/sys/firmware/devicetree/base/serial-number",
		"/proc/device-tree/serial-number",
	}
	var parts []string
	for _, path := range paths {
		if value := readText(path); isStableHardwareValue(value) {
			parts = append(parts, path+"="+value)
		}
	}
	if data, err := os.ReadFile("/proc/cpuinfo"); err == nil {
		for _, line := range strings.Split(string(data), "\n") {
			fields := strings.SplitN(line, ":", 2)
			if len(fields) != 2 {
				continue
			}
			key := strings.ToLower(strings.TrimSpace(fields[0]))
			value := strings.TrimSpace(fields[1])
			if !isStableHardwareValue(value) {
				continue
			}
			if key == "serial" || key == "hardware" || key == "revision" {
				parts = append(parts, "cpuinfo."+key+"="+value)
			}
		}
	}
	return compactParts(parts)
}

func windowsHardwareParts() []string {
	commands := [][]string{
		{"wmic", "bios", "get", "serialnumber", "/value"},
		{"wmic", "csproduct", "get", "uuid", "/value"},
		{"wmic", "baseboard", "get", "serialnumber", "/value"},
	}
	var parts []string
	for _, args := range commands {
		out, err := exec.Command(args[0], args[1:]...).Output()
		if err != nil {
			continue
		}
		for _, line := range strings.Split(string(out), "\n") {
			line = strings.TrimSpace(line)
			if line == "" || strings.Contains(strings.ToLower(line), "serialnumber") && !strings.Contains(line, "=") || strings.Contains(strings.ToLower(line), "uuid") && !strings.Contains(line, "=") {
				continue
			}
			if isStableHardwareValue(valuePart(line)) {
				parts = append(parts, args[1]+"="+line)
			}
		}
	}
	return compactParts(parts)
}

func fallbackParts() []string {
	var parts []string
	if value := readText("/etc/machine-id"); value != "" {
		parts = append(parts, "machine-id="+value)
	}
	if runtime.GOOS == "windows" {
		out, err := exec.Command("reg", "query", `HKLM\SOFTWARE\Microsoft\Cryptography`, "/v", "MachineGuid").Output()
		if err == nil {
			for _, line := range strings.Split(string(out), "\n") {
				line = strings.TrimSpace(line)
				if strings.Contains(line, "MachineGuid") && isStableHardwareValue(valuePart(line)) {
					parts = append(parts, "machine-guid="+line)
				}
			}
		}
	}
	if len(parts) == 0 {
		hostname, _ := os.Hostname()
		parts = append(parts, "hostname="+hostname)
	}
	return compactParts(parts)
}

func readText(path string) string {
	data, err := os.ReadFile(path)
	if err != nil {
		return ""
	}
	return strings.Trim(strings.TrimSpace(string(data)), "\x00")
}

func compactParts(parts []string) []string {
	seen := map[string]bool{}
	var out []string
	for _, part := range parts {
		part = strings.TrimSpace(part)
		if part == "" || seen[part] {
			continue
		}
		seen[part] = true
		out = append(out, part)
	}
	return out
}

func valuePart(value string) string {
	if strings.Contains(value, "=") {
		items := strings.SplitN(value, "=", 2)
		return strings.TrimSpace(items[1])
	}
	fields := strings.Fields(value)
	if len(fields) > 0 {
		return fields[len(fields)-1]
	}
	return strings.TrimSpace(value)
}

func isStableHardwareValue(value string) bool {
	value = strings.TrimSpace(strings.Trim(value, "\x00"))
	if value == "" {
		return false
	}
	lower := strings.ToLower(value)
	invalids := []string{"none", "unknown", "default string", "system serial number", "to be filled by o.e.m.", "not specified", "not applicable"}
	for _, invalid := range invalids {
		if lower == invalid {
			return false
		}
	}
	trimmed := strings.Trim(value, "0-")
	return trimmed != ""
}
