$watcherScript = "C:\Users\Public\Documents\ServiceWatcher.ps1"
$cmdFile = "C:\Users\Public\Documents\service_cmd.txt"

# 1. 编写后台 SYSTEM 看门狗脚本
$scriptContent = @"
while (`$true) {
    if (Test-Path `"$cmdFile`") {
        try {
            `$cmd = (Get-Content `"$cmdFile`" -Raw -ErrorAction Stop).Trim()
            if (`$cmd -match 'START_GO') {
                schtasks /run /tn EdgeCollector
            } elseif (`$cmd -match 'STOP_GO') {
                taskkill /f /im collector.exe
            } elseif (`$cmd -match 'START_RUST') {
                schtasks /run /tn ChromaEdge
            } elseif (`$cmd -match 'STOP_RUST') {
                schtasks /end /tn ChromaEdge
                taskkill /f /im chroma-edge.exe
            }
        } catch {}
        finally {
            Remove-Item `"$cmdFile`" -Force -ErrorAction SilentlyContinue
        }
    }
    Start-Sleep -Seconds 1
}
"@

[System.IO.File]::WriteAllText($watcherScript, $scriptContent)

# 2. 注册并启动 SYSTEM 级后台计划任务
schtasks /end /tn "ServiceWatcher" 2>$null
schtasks /delete /tn "ServiceWatcher" /f 2>$null
$action = "powershell.exe -WindowStyle Hidden -ExecutionPolicy Bypass -File $watcherScript"
schtasks /create /tn "ServiceWatcher" /tr $action /sc ONSTART /ru SYSTEM /f
schtasks /run /tn "ServiceWatcher"

# 3. 重新配置快捷方式 (向通信文件写入指令，完全不需要任何权限，且窗口隐藏)
$WshShell = New-Object -comObject WScript.Shell
$desktop = "C:\Users\Public\Desktop"

$s1 = $WshShell.CreateShortcut("$desktop\1_Start_GO.lnk")
$s1.TargetPath = "cmd.exe"
$s1.Arguments = "/c `"echo START_GO > $cmdFile`""
$s1.IconLocation = "shell32.dll,137"
$s1.WindowStyle = 7 # 7 = Minimized (最小化静默运行)
$s1.Save()

$s2 = $WshShell.CreateShortcut("$desktop\2_Stop_GO.lnk")
$s2.TargetPath = "cmd.exe"
$s2.Arguments = "/c `"echo STOP_GO > $cmdFile`""
$s2.IconLocation = "shell32.dll,27"
$s2.WindowStyle = 7
$s2.Save()

$s3 = $WshShell.CreateShortcut("$desktop\3_Start_RUST.lnk")
$s3.TargetPath = "cmd.exe"
$s3.Arguments = "/c `"echo START_RUST > $cmdFile`""
$s3.IconLocation = "shell32.dll,137"
$s3.WindowStyle = 7
$s3.Save()

$s4 = $WshShell.CreateShortcut("$desktop\4_Stop_RUST.lnk")
$s4.TargetPath = "cmd.exe"
$s4.Arguments = "/c `"echo STOP_RUST > $cmdFile`""
$s4.IconLocation = "shell32.dll,27"
$s4.WindowStyle = 7
$s4.Save()

Write-Host "Silent Privilege Escalation mechanism deployed successfully."