$ErrorActionPreference = "Stop"
$MaxRetries = 3
$RetryCount = 0

# 引入 Mobius Hook 以接管后续 cargo 编译日志
if (Test-Path ".\.trae\mobius_hook.ps1") {
    . ".\.trae\mobius_hook.ps1"
}

while ($RetryCount -lt $MaxRetries) {
    try {
        Write-Host "[Step 1] Compiling Rust binary..." -ForegroundColor Cyan
        Set-Location "i:\GIT\VS2022\Chromatography-workstation\src\salvo-backend\chroma-edge"
        $env:PATH += ";C:\Users\connor\.cargo\bin"
        cargo build --release
        if ($LASTEXITCODE -ne 0) { throw "Compile failed!" }

        Write-Host "[Step 2] Zipping deployment files..." -ForegroundColor Cyan
        if (Test-Path "deploy_chroma_edge.zip") { Remove-Item "deploy_chroma_edge.zip" -Force }
        Copy-Item "..\..\..\.env" ".\.env" -Force
        
        # 编译前端 React 项目并打包
        Write-Host "[Step 1.5] Compiling React Frontend..." -ForegroundColor Cyan
        Set-Location "i:\GIT\VS2022\Chromatography-workstation\src\ui\apps\workstation"
        $env:PATH += ";C:\nvm4w\nodejs;C:\Users\connor\AppData\Local\nvm"
        npm.cmd install
        npm.cmd run build
        if ($LASTEXITCODE -ne 0) { throw "Frontend compile failed!" }
        Set-Location "i:\GIT\VS2022\Chromatography-workstation\src\salvo-backend\chroma-edge"
        
        if (Test-Path ".\static") { Remove-Item ".\static" -Recurse -Force }
        Copy-Item "i:\GIT\VS2022\Chromatography-workstation\src\ui\apps\workstation\dist" ".\static" -Recurse -Force
        
        # 因为是 workspace，编译产物在上一级的 target 目录中
        Copy-Item "..\target\release\chroma-edge.exe" ".\chroma-edge.exe" -Force
        Compress-Archive -Path ".\chroma-edge.exe", ".\static", ".\.env" -DestinationPath "deploy_chroma_edge.zip" -Force

        $remoteHost = "test-win"
        $remoteUser = "trae"
        $remoteDir = "C:\Users\trae\Desktop\chroma-edge"
        $env:SSH_PASSWORD = "a1234567A"

        Write-Host "[Step 3] Creating remote directory & Stopping remote process..." -ForegroundColor Cyan
        # 杀掉 Go 版本，避免占用 COM 口
        sshx "-h=$remoteHost" --password-only --timeout=15s "taskkill /F /IM collector.exe 2> `$null" | Out-Null
        
        sshx "-h=$remoteHost" --password-only --timeout=15s "New-Item -ItemType Directory -Force -Path $remoteDir" | Out-Null
        sshx "-h=$remoteHost" --password-only --timeout=15s "schtasks /end /tn ChromaEdge 2> `$null" | Out-Null
        sshx "-h=$remoteHost" --password-only --timeout=15s "schtasks /create /tn KillChroma /tr `'taskkill /F /IM chroma-edge.exe`' /sc ONCE /st 00:00 /ru SYSTEM /f > `$null 2>&1; schtasks /run /tn KillChroma > `$null 2>&1" | Out-Null
        Start-Sleep -Seconds 2
        sshx "-h=$remoteHost" --password-only --timeout=15s "if (Test-Path $remoteDir\chroma-edge.old) { Remove-Item $remoteDir\chroma-edge.old -Force -ErrorAction SilentlyContinue }"
        sshx "-h=$remoteHost" --password-only --timeout=15s "if (Test-Path $remoteDir\chroma-edge.exe) { Rename-Item $remoteDir\chroma-edge.exe chroma-edge.old -Force -ErrorAction SilentlyContinue }"

        Write-Host "[Step 4] Uploading deployment package via HTTP Pull..." -ForegroundColor Cyan
        $env:ErrorActionPreference = "Continue"
        
        $uploadOut = sshx "-h=$remoteHost" --password-only --timeout=60s "--upload=deploy_chroma_edge.zip" "--to=$remoteDir\deploy_chroma_edge.zip"
        if ($LASTEXITCODE -ne 0) { throw "Upload failed! Output: $uploadOut" }
        $env:ErrorActionPreference = "Stop"

        Write-Host "[Step 5] Extracting and starting service..." -ForegroundColor Cyan
        $env:ErrorActionPreference = "Continue"
        sshx "-h=$remoteHost" --password-only --timeout=30s "cd $remoteDir; tar -xf deploy_chroma_edge.zip"
        if ($LASTEXITCODE -ne 0) { throw "Extract failed!" }
        
        # 尝试通过 schtasks 启动。如果任务不存在，则创建并执行；如果创建失败，退退回 Start-Process 启动
        sshx "-h=$remoteHost" --password-only --timeout=30s "schtasks /query /tn ChromaEdge >`$null 2>&1; if (`$LASTEXITCODE -ne 0) { schtasks /create /tn ChromaEdge /tr `"$remoteDir\chroma-edge.exe`" /sc ONSTART /ru SYSTEM /f }"
        sshx "-h=$remoteHost" --password-only --timeout=30s "schtasks /run /tn ChromaEdge"
        if ($LASTEXITCODE -ne 0) { 
            Write-Host "Task run failed, trying Start-Process fallback..." -ForegroundColor Yellow
            sshx "-h=$remoteHost" --password-only --timeout=30s "cd $remoteDir; Start-Process -FilePath '.\chroma-edge.exe' -WindowStyle Hidden -RedirectStandardOutput 'chroma-edge.log' -RedirectStandardError 'chroma-edge.err'"
        }
        $env:ErrorActionPreference = "Stop"

        Write-Host "[Step 6] Verifying..." -ForegroundColor Cyan
        Start-Sleep -Seconds 5
        $env:ErrorActionPreference = "Continue"
        $out = sshx "-h=$remoteHost" --password-only --timeout=15s "tasklist | findstr chroma-edge"
        $env:ErrorActionPreference = "Stop"

        if ($out -match "chroma-edge.exe") {
            Write-Host ">>> DEPLOYMENT & VERIFICATION SUCCESSFUL <<<" -ForegroundColor Green
            exit 0
        } else {
            Write-Host "Verification failed. Checking remote log:" -ForegroundColor Yellow
            $env:ErrorActionPreference = "Continue"
            sshx "-h=$remoteHost" --password-only "type $remoteDir\chroma-edge.log"
            sshx "-h=$remoteHost" --password-only "type $remoteDir\chroma-edge.err"
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
