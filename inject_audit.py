import re

with open('src/edge/cmd/collector/main.go', 'r', encoding='utf-8') as f:
    content = f.read()

# 1. /api/control/temp
content = content.replace(
    'LogInfof("开始控温")',
    'LogInfof("开始控温")\n\t\t\tdefaultState.Twin.AppendAuditLog("StartTempControl", "Admin", "Started temperature control")'
)
content = content.replace(
    'LogInfof("停止控温")',
    'LogInfof("停止控温")\n\t\t\tdefaultState.Twin.AppendAuditLog("StopTempControl", "Admin", "Stopped temperature control")'
)

# Replace target temperature set
target_temp_block_old = """\t\t\tif err := driver.SetTempTarget(k, v); err != nil {
\t\t\t\twriteJSON(w, http.StatusInternalServerError, map[string]any{"error": err.Error()})
\t\t\t\treturn
\t\t\t}
\t\t}
\t})"""
target_temp_block_new = """\t\t\tif err := driver.SetTempTarget(k, v); err != nil {
\t\t\t\twriteJSON(w, http.StatusInternalServerError, map[string]any{"error": err.Error()})
\t\t\t\treturn
\t\t\t}
\t\t\tdefaultState.Twin.AppendAuditLog("SetTemperature", "Admin", fmt.Sprintf("Set temp %s to %.2f", k, v))
\t\t}
\t})"""
content = content.replace(target_temp_block_old, target_temp_block_new)

# 2. /api/control/epc
epc_block_old = """\t\tif err := driver.SetEPCTarget(in.Zone, in.Target); err != nil {
\t\t\twriteJSON(w, http.StatusInternalServerError, map[string]any{"error": err.Error()})
\t\t\treturn
\t\t}
\t\twriteJSON(w, http.StatusOK, map[string]any{"status": "ok"})
\t})"""
epc_block_new = """\t\tif err := driver.SetEPCTarget(in.Zone, in.Target); err != nil {
\t\t\twriteJSON(w, http.StatusInternalServerError, map[string]any{"error": err.Error()})
\t\t\treturn
\t\t}
\t\tdefaultState.Twin.AppendAuditLog("SetPressure", "Admin", fmt.Sprintf("Set EPC %s to %.2f", in.Zone, in.Target))
\t\twriteJSON(w, http.StatusOK, map[string]any{"status": "ok"})
\t})"""
content = content.replace(epc_block_old, epc_block_new)

# 3. /api/control/events
events_block_old = """\t\tif in.Event == "start_analysis" {
\t\t\terr = driver.StartAnalysis()
\t\t\tLogInfof("StartAnalysis command executed, err: %v", err)
\t\t\t// Reset sessions for all devices to prevent waveform drift
\t\t\tresetAllSessions()
\t\t} else if in.Event == "stop_analysis" {
\t\t\terr = driver.StopAnalysis()
\t\t\tLogInfof("StopAnalysis command executed, err: %v", err)
\t\t}"""
events_block_new = """\t\tif in.Event == "start_analysis" {
\t\t\terr = driver.StartAnalysis()
\t\t\tLogInfof("StartAnalysis command executed, err: %v", err)
\t\t\tdefaultState.Twin.AppendAuditLog("StartAnalysis", "Admin", "Started analysis cycle")
\t\t\t// Reset sessions for all devices to prevent waveform drift
\t\t\tresetAllSessions()
\t\t} else if in.Event == "stop_analysis" {
\t\t\terr = driver.StopAnalysis()
\t\t\tLogInfof("StopAnalysis command executed, err: %v", err)
\t\t\tdefaultState.Twin.AppendAuditLog("StopAnalysis", "Admin", "Stopped analysis cycle")
\t\t}"""
content = content.replace(events_block_old, events_block_new)

# 4. /api/method
method_block_old = """\t\tif err := pstore.SaveMethod(in); err != nil {
\t\t\twriteJSON(w, http.StatusInternalServerError, map[string]any{"error": err.Error()})
\t\t\treturn
\t\t}
\t\twriteJSON(w, http.StatusOK, map[string]any{"ok": true, "message": "method saved"})
\t})"""
method_block_new = """\t\tif err := pstore.SaveMethod(in); err != nil {
\t\t\twriteJSON(w, http.StatusInternalServerError, map[string]any{"error": err.Error()})
\t\t\treturn
\t\t}
\t\tdefaultState.Twin.AppendAuditLog("SaveMethod", "Admin", fmt.Sprintf("Saved method %s", in.ID))
\t\twriteJSON(w, http.StatusOK, map[string]any{"ok": true, "message": "method saved"})
\t})"""
content = content.replace(method_block_old, method_block_new)

# 5. /api/sysconfig
sysconfig_block_old = """\t\tif err := pstore.SaveSysConfig(in); err != nil {
\t\t\twriteJSON(w, http.StatusInternalServerError, map[string]any{"error": err.Error()})
\t\t\treturn
\t\t}

\t\twriteJSON(w, http.StatusOK, map[string]any{"ok": true, "message": "sysconfig saved"})
\t})"""
sysconfig_block_new = """\t\tif err := pstore.SaveSysConfig(in); err != nil {
\t\t\twriteJSON(w, http.StatusInternalServerError, map[string]any{"error": err.Error()})
\t\t\treturn
\t\t}
\t\tdefaultState.Twin.AppendAuditLog("SaveSysConfig", "Admin", "Updated system configuration")

\t\twriteJSON(w, http.StatusOK, map[string]any{"ok": true, "message": "sysconfig saved"})
\t})"""
content = content.replace(sysconfig_block_old, sysconfig_block_new)

with open('src/edge/cmd/collector/main.go', 'w', encoding='utf-8') as f:
    f.write(content)
