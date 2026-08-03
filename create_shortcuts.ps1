$WshShell = New-Object -comObject WScript.Shell

$desktop = "C:\Users\connor\Desktop"

# 1. GO版 启动
$Shortcut1 = $WshShell.CreateShortcut("$desktop\GO版本-启动服务.lnk")
$Shortcut1.TargetPath = "powershell.exe"
$Shortcut1.Arguments = "-NoProfile -ExecutionPolicy Bypass -Command `"& { `$env:SSH_PASSWORD='a1234567A'; Write-Host 'Starting GO Service...'; sshx -h=test-win --password-only 'schtasks /run /tn EdgeCollector'; Start-Sleep -Seconds 2 }`""
$Shortcut1.IconLocation = "shell32.dll,137" # 运行图标
$Shortcut1.Save()

# 2. GO版 停止
$Shortcut2 = $WshShell.CreateShortcut("$desktop\GO版本-停止服务.lnk")
$Shortcut2.TargetPath = "powershell.exe"
$Shortcut2.Arguments = "-NoProfile -ExecutionPolicy Bypass -Command `"& { `$env:SSH_PASSWORD='a1234567A'; Write-Host 'Stopping GO Service...'; sshx -h=test-win --password-only 'taskkill /f /im collector.exe'; Start-Sleep -Seconds 2 }`""
$Shortcut2.IconLocation = "shell32.dll,27" # 停止图标
$Shortcut2.Save()

# 3. RUST版 启动
$Shortcut3 = $WshShell.CreateShortcut("$desktop\RUST版本-启动服务.lnk")
$Shortcut3.TargetPath = "powershell.exe"
$Shortcut3.Arguments = "-NoProfile -ExecutionPolicy Bypass -Command `"& { `$env:SSH_PASSWORD='a1234567A'; Write-Host 'Starting RUST Service...'; sshx -h=test-win --password-only 'schtasks /run /tn ChromaEdge'; Start-Sleep -Seconds 2 }`""
$Shortcut3.IconLocation = "shell32.dll,137"
$Shortcut3.Save()

# 4. RUST版 停止
$Shortcut4 = $WshShell.CreateShortcut("$desktop\RUST版本-停止服务.lnk")
$Shortcut4.TargetPath = "powershell.exe"
$Shortcut4.Arguments = "-NoProfile -ExecutionPolicy Bypass -Command `"& { `$env:SSH_PASSWORD='a1234567A'; Write-Host 'Stopping RUST Service...'; sshx -h=test-win --password-only 'schtasks /end /tn ChromaEdge 2> `$null; schtasks /create /tn KillChroma /tr `'taskkill /F /IM chroma-edge.exe`' /sc ONCE /st 00:00 /ru SYSTEM /f > `$null 2>&1; schtasks /run /tn KillChroma'; Start-Sleep -Seconds 2 }`""
$Shortcut4.IconLocation = "shell32.dll,27"
$Shortcut4.Save()

Write-Host "Shortcuts created successfully."