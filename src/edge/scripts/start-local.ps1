$ErrorActionPreference = "Stop"

$root = (Get-Item $PSScriptRoot).Parent.FullName
Set-Location $root

Write-Host "Stopping existing instances..."
Stop-Process -Name "collector" -Force -ErrorAction SilentlyContinue
Stop-Process -Name "simulator" -Force -ErrorAction SilentlyContinue
Stop-Process -Name "simulator_tcd" -Force -ErrorAction SilentlyContinue

$pids = Get-NetTCPConnection -LocalPort 8080 -ErrorAction SilentlyContinue | Select-Object -ExpandProperty OwningProcess
if ($pids) { Stop-Process -Id $pids -Force -ErrorAction SilentlyContinue }

Write-Host "Building Windows executables..."
$env:GOOS = "windows"
$env:GOARCH = "amd64"
& go build -o collector.exe .\cmd\collector
if ($LASTEXITCODE -ne 0) { throw "Build collector failed" }

& go build -o simulator.exe .\cmd\simulator
if ($LASTEXITCODE -ne 0) { throw "Build simulator failed" }

& go build -o simulator_tcd.exe .\cmd\simulator_tcd
if ($LASTEXITCODE -ne 0) { throw "Build simulator_tcd failed" }

Write-Host "Starting collector..."
$env:EDGE_ALLOW_CONTROL = "1"
$env:EDGE_DRIVER_MODE = "modular"
$env:TCD_PORT = "COM10"
$env:MODBUS_TEMP_PORT = "COM3"
# Start collector in background
Start-Process -FilePath ".\collector.exe" -WindowStyle Hidden

Start-Sleep -Seconds 2

Write-Host "Starting simulators..."
$env:EDGE_SIM_TCD_PORT = "COM11"
$env:EDGE_SIM_MODBUS_PORT = "COM4"
Start-Process -FilePath ".\simulator_tcd.exe" -WindowStyle Hidden
Start-Process -FilePath ".\simulator.exe" -WindowStyle Hidden

Write-Host "Local environment started successfully."
