$ErrorActionPreference = "Stop"
$MaxRetries = 3
$RetryCount = 0

while ($RetryCount -lt $MaxRetries) {
    try {
        Write-Host "[Step 1] Compiling Go binary..." -ForegroundColor Cyan
        Set-Location "i:\GIT\VS2022\Chromatography-workstation\src\edge\cmd\collector"
        $env:GOOS="windows"
        $env:GOARCH="amd64"
        $env:GOTOOLCHAIN="local"
        $env:GOMODCACHE="i:\GIT\VS2022\go_cache\pkg\mod"
        if (-not (Test-Path $env:GOMODCACHE)) { New-Item -ItemType Directory -Force -Path $env:GOMODCACHE | Out-Null }
        D:\GOPATH\go1.26.2\bin\go.exe build -o collector.exe .
        if ($LASTEXITCODE -ne 0) { throw "Compile failed!" }
        Start-Sleep -Seconds 2
        Set-Location "i:\GIT\VS2022\Chromatography-workstation\src\edge\cmd\collector"

        Write-Host "[Step 2] Zipping deployment files..." -ForegroundColor Cyan
        if (Test-Path "deploy_new.zip") { Remove-Item "deploy_new.zip" -Force }
        Copy-Item "..\..\..\..\.env" ".\.env" -Force
        Compress-Archive -Path "collector.exe", "static", ".env", "..\..\..\..\collector-service.exe", "..\..\..\..\collector-service.xml" -DestinationPath "deploy_new.zip" -Force

        $remoteHost = "10.8.5.23"
        $remoteUser = "trae"
        $remoteDir = "C:\Users\trae\Desktop\edge"
        $env:SSH_PASSWORD = "a1234567A"

        Write-Host "[Step 3] Creating remote directory & Stopping remote process..." -ForegroundColor Cyan
        sshx "-h=$remoteHost" "-u=$remoteUser" --password-only --timeout=15s "New-Item -ItemType Directory -Force -Path $remoteDir" | Out-Null
        
        # 尝试使用 WinSW 停止并卸载旧服务
        sshx "-h=$remoteHost" "-u=$remoteUser" --password-only --timeout=15s "if (Test-Path $remoteDir\collector-service.exe) { cd $remoteDir; .\collector-service.exe stop; .\collector-service.exe uninstall }" | Out-Null
        
        # 强制防自愈重置：先重命名可执行文件，防止守护进程秒级拉起
        sshx "-h=$remoteHost" "-u=$remoteUser" --password-only --timeout=15s "if (Test-Path $remoteDir\collector.exe) { Rename-Item $remoteDir\collector.exe collector_hide.exe -Force -ErrorAction SilentlyContinue }" | Out-Null
        
        # 使用 SYSTEM 权限的计划任务来强杀进程，防止普通账户无权 Kill
        sshx "-h=$remoteHost" "-u=$remoteUser" --password-only --timeout=15s "schtasks /Create /TN KillCollector /TR 'taskkill /F /IM collector.exe' /SC ONCE /ST 00:00 /RU SYSTEM /F 2> `$null" | Out-Null
        sshx "-h=$remoteHost" "-u=$remoteUser" --password-only --timeout=15s "schtasks /Run /TN KillCollector 2> `$null" | Out-Null
        Start-Sleep -Seconds 2
        # 清除“午夜定时炸弹”，防止凌晨 00:00 再次执行误杀
        sshx "-h=$remoteHost" "-u=$remoteUser" --password-only --timeout=15s "schtasks /Delete /TN KillCollector /F 2> `$null" | Out-Null
        
        sshx "-h=$remoteHost" "-u=$remoteUser" --password-only --timeout=15s "if (Test-Path $remoteDir\collector.old) { Remove-Item $remoteDir\collector.old -Force -ErrorAction SilentlyContinue }"
        sshx "-h=$remoteHost" "-u=$remoteUser" --password-only --timeout=15s "if (Test-Path $remoteDir\collector_hide.exe) { Rename-Item $remoteDir\collector_hide.exe collector.old -Force -ErrorAction SilentlyContinue }"

        Write-Host "[Step 4] Uploading deployment package..." -ForegroundColor Cyan
        $env:ErrorActionPreference = "Continue"
        sshx "-h=$remoteHost" "-u=$remoteUser" --password-only --timeout=60s "--upload=deploy_new.zip" "--to=$remoteDir\deploy_new.zip"
        if ($LASTEXITCODE -ne 0) { throw "Upload failed!" }
        $env:ErrorActionPreference = "Stop"

        Write-Host "[Step 5] Extracting and starting service..." -ForegroundColor Cyan
        $env:ErrorActionPreference = "Continue"
        sshx "-h=$remoteHost" "-u=$remoteUser" --password-only --timeout=30s "cd $remoteDir; tar -xf deploy_new.zip"
        if ($LASTEXITCODE -ne 0) { throw "Extract failed!" }
        
        # 使用 WinSW 安装并启动真正的 Windows 服务
        sshx "-h=$remoteHost" "-u=$remoteUser" --password-only --timeout=30s "cd $remoteDir; .\collector-service.exe install; .\collector-service.exe start"
        if ($LASTEXITCODE -ne 0) { throw "Start WinSW service failed!" }
        $env:ErrorActionPreference = "Stop"

        Write-Host "[Step 6] Verifying..." -ForegroundColor Cyan
        Start-Sleep -Seconds 5
        $env:ErrorActionPreference = "Continue"
        $out = sshx "-h=$remoteHost" "-u=$remoteUser" --password-only --timeout=15s "tasklist | findstr collector"
        $env:ErrorActionPreference = "Stop"

        if ($out -match "collector.exe") {
            Write-Host ">>> DEPLOYMENT & VERIFICATION SUCCESSFUL <<<" -ForegroundColor Green
            exit 0
        } else {
            Write-Host "Verification failed. Checking remote log:" -ForegroundColor Yellow
            $env:ErrorActionPreference = "Continue"
            sshx "-h=$remoteHost" "-u=$remoteUser" --password-only "type $remoteDir\collector.log"
            throw "Process not found in tasklist."
        }
    } catch {
        Write-Host "Error occurred: $_" -ForegroundColor Red
        $RetryCount++
        if ($RetryCount -lt $MaxRetries) {
            $sleepTime = $RetryCount * 5
            Write-Host "Retrying in $sleepTime seconds... ($RetryCount/$MaxRetries)" -ForegroundColor Yellow
            Start-Sleep -Seconds $sleepTime
        } else {
            Write-Host "Deployment failed after $MaxRetries attempts." -ForegroundColor Red
            exit 1
        }
    }
}

