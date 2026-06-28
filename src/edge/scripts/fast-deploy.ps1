param(
  [string]$RemoteDir = "/opt/edge-collector"
)

$ErrorActionPreference = "Stop"

function Get-RepoRoot {
  $p = $PSScriptRoot
  while ($true) {
    if (Test-Path (Join-Path $p ".git")) { return $p }
    $parent = Split-Path -Parent $p
    if ($parent -eq $p -or [string]::IsNullOrWhiteSpace($parent)) { throw "repo root not found" }
    $p = $parent
  }
}

function Read-DotEnv([string]$path) {
  $m = @{}
  if (-not (Test-Path $path)) { return $m }
  $lines = Get-Content -LiteralPath $path
  foreach ($line in $lines) {
    $t = $line.Trim()
    if ($t.Length -eq 0 -or $t.StartsWith("#")) { continue }
    $i = $t.IndexOf("=")
    if ($i -lt 1) { continue }
    $k = $t.Substring(0, $i).Trim()
    $v = $t.Substring($i + 1).Trim()
    $m[$k] = $v
  }
  return $m
}

$root = Get-RepoRoot
$envPath = Join-Path $root ".env"
$kv = Read-DotEnv $envPath

$sshHost = $kv["TEST_SCREEN_SSH_HOST"]
$hostsToTry = @()
if (-not [string]::IsNullOrWhiteSpace($sshHost)) { $hostsToTry += $sshHost }
if (-not [string]::IsNullOrWhiteSpace($kv["TEST_SCREEN_SSH_HOST_BAK1"])) { $hostsToTry += $kv["TEST_SCREEN_SSH_HOST_BAK1"] }
if (-not [string]::IsNullOrWhiteSpace($kv["TEST_SCREEN_SSH_HOST_BAK2"])) { $hostsToTry += $kv["TEST_SCREEN_SSH_HOST_BAK2"] }
if (-not [string]::IsNullOrWhiteSpace($kv["TEST_SCREEN_SSH_HOST_BAK"])) { $hostsToTry += $kv["TEST_SCREEN_SSH_HOST_BAK"] }

$port = $kv["TEST_SCREEN_SSH_PORT"]
$user = $kv["TEST_SCREEN_SSH_USER"]
$pass = $kv["TEST_SCREEN_SSH_PASSWORD"]

if ($hostsToTry.Count -eq 0) { throw "TEST_SCREEN_SSH_HOST is missing in .env" }
if ([string]::IsNullOrWhiteSpace($port)) { $port = "22" }
if ([string]::IsNullOrWhiteSpace($user)) { $user = "root" }
if ([string]::IsNullOrWhiteSpace($pass)) { throw "TEST_SCREEN_SSH_PASSWORD is missing in .env" }

$plink = (Get-Command plink.exe -ErrorAction SilentlyContinue).Source
$pscp = (Get-Command pscp.exe -ErrorAction SilentlyContinue).Source
if ([string]::IsNullOrWhiteSpace($plink) -or [string]::IsNullOrWhiteSpace($pscp)) {
  throw "PuTTY plink.exe and pscp.exe are required for Windows native deployment."
}

# Build
Write-Host "Building linux-arm64 binary..."
$oldGoos = $env:GOOS
$oldGoarch = $env:GOARCH
$env:GOOS = "linux"
$env:GOARCH = "arm64"
$srcDir = Join-Path $root "src/edge"
$outBin = Join-Path $srcDir "collector-linux-arm64"
Set-Location $srcDir
& go build -o $outBin .\cmd\collector
if ($LASTEXITCODE -ne 0) { $env:GOOS = $oldGoos; $env:GOARCH = $oldGoarch; throw "go build failed" }
$env:GOOS = $oldGoos
$env:GOARCH = $oldGoarch

$ErrorActionPreference = "Continue"

# Test SSH Connection
$connected = $false
foreach ($h in $hostsToTry) {
    Write-Host "Testing SSH connection to $h..."
    & $plink -ssh -P $port -l $user -pw $pass -batch $h "echo ok" 2>$null | Out-Null
    if ($LASTEXITCODE -eq 0) {
        $sshHost = $h
        $connected = $true
        Write-Host "Successfully connected to $h"
        break
    } else {
        Write-Host "Failed to connect to $h."
    }
}

if (-not $connected) {
    throw "Failed to connect to any SSH hosts."
}
$ErrorActionPreference = "Stop"

# 1. Stop remote service
$stopCmd = "mkdir -p `"$RemoteDir`"; systemctl stop edge-collector 2>/dev/null || true; cd `"$RemoteDir`"; if [ -f collector.pid ]; then kill -9 `$(cat collector.pid) 2>/dev/null || true; rm -f collector.pid; fi; killall -9 collector collector-linux-arm64 2>/dev/null || true; fuser -k 8080/tcp 2>/dev/null || true; fuser -k 50051/tcp 2>/dev/null || true; fuser -k 4840/tcp 2>/dev/null || true; fuser -k 8000/tcp 2>/dev/null || true; fuser -k 25001/tcp 2>/dev/null || true; sleep 1"
Write-Host "Stopping remote services..."
$ErrorActionPreference = "Continue"
& $plink -ssh -P $port -l $user -pw $pass -batch $sshHost $stopCmd
if ($LASTEXITCODE -ne 0) { Write-Warning "Stop command returned non-zero exit code" }

# 2. Upload binary
Write-Host "Uploading binary using pscp..."
& $pscp -P $port -l $user -pw $pass -batch $outBin "${sshHost}:${RemoteDir}/collector-linux-arm64"
if ($LASTEXITCODE -ne 0) { throw "pscp upload failed" }

# 3. Start remote service
$startCmd = "cd `"$RemoteDir`"; chmod +x ./collector-linux-arm64; if systemctl list-unit-files | grep -q edge-collector; then systemctl start edge-collector; else EDGE_HTTP_BIND=0.0.0.0 EDGE_ALLOW_CONTROL=1 nohup ./collector-linux-arm64 > collector.log 2>&1 & echo `$! > collector.pid; fi"
Write-Host "Starting remote services..."
& $plink -ssh -P $port -l $user -pw $pass -batch $sshHost $startCmd
if ($LASTEXITCODE -ne 0) { throw "Start command failed" }
$ErrorActionPreference = "Stop"

Write-Host "Deployment completed successfully using Windows native plink/pscp!"
