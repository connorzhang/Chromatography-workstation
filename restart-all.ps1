$ErrorActionPreference = "Continue"

Write-Host "========================================="
Write-Host " Cleaning up old processes and ports..."
Write-Host "========================================="

# Kill specific processes
$processes = @("node", "cargo", "chroma-edge", "chroma-sim", "collector", "simulator", "simulator_tcd", "main")
foreach ($p in $processes) {
    Stop-Process -Name $p -Force -ErrorAction SilentlyContinue
}

# Kill processes by port
$ports = @(8080, 8081, 8082, 5173)
foreach ($port in $ports) {
    $pids = Get-NetTCPConnection -LocalPort $port -ErrorAction SilentlyContinue | Select-Object -ExpandProperty OwningProcess
    if ($pids) {
        foreach ($pid_num in $pids) {
            Stop-Process -Id $pid_num -Force -ErrorAction SilentlyContinue
        }
    }
}

Write-Host "Cleanup finished. Starting services..."

# Start Go Online Version (runs in background, window closes immediately)
Write-Host "-> Starting Online Version (Go, Port 8080)..."
Start-Process powershell -ArgumentList "-Command `"cd src\edge\scripts; .\start-local.ps1`""

Start-Sleep -Seconds 2

# Start Rust Simulator
Write-Host "-> Starting Lab Simulator (Rust chroma-sim, Port 8081)..."
Start-Process powershell -ArgumentList "-Command `"cd src\salvo-backend; cargo run --bin chroma-sim`""

Start-Sleep -Seconds 2

# Start Rust Backend
Write-Host "-> Starting Lab Backend (Rust chroma-edge, Port 8082)..."
Start-Process powershell -ArgumentList "-Command `"cd src\salvo-backend; cargo run --bin chroma-edge`""

Start-Sleep -Seconds 2

# Start React Frontend
Write-Host "-> Starting Lab Frontend (React Vite, Port 5173)..."
Start-Process powershell -ArgumentList "-Command `"`$env:PATH += ';C:\Users\Administrator\AppData\Local\Temp\node-v22.12.0-win-x64'; cd src\ui\apps\workstation; npm run dev`""

Write-Host "========================================="
Write-Host " All services have been started!"
Write-Host " 1. Lab Version (Rust+React): http://localhost:5173"
Write-Host " 2. Online Version (Go):      http://localhost:8080"
Write-Host " Run this script (.\restart-all.ps1) anytime to restart everything."
Write-Host "========================================="
