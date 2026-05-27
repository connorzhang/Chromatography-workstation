param(
  [switch]$WithData,
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
    if ($t.Length -eq 0) { continue }
    if ($t.StartsWith("#")) { continue }
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
$port = $kv["TEST_SCREEN_SSH_PORT"]
$user = $kv["TEST_SCREEN_SSH_USER"]
$pass = $kv["TEST_SCREEN_SSH_PASSWORD"]

if ([string]::IsNullOrWhiteSpace($sshHost)) { throw "TEST_SCREEN_SSH_HOST is missing in .env" }
if ([string]::IsNullOrWhiteSpace($port)) { $port = "22" }
if ([string]::IsNullOrWhiteSpace($user)) { $user = "root" }
if ([string]::IsNullOrWhiteSpace($pass)) { throw "TEST_SCREEN_SSH_PASSWORD is missing in .env" }

function Ensure-Putty([string]$dir) {
  New-Item -ItemType Directory -Force -Path $dir | Out-Null
  $plink = Join-Path $dir "plink.exe"
  $pscp = Join-Path $dir "pscp.exe"
  if (-not (Test-Path $plink)) {
    Invoke-WebRequest -UseBasicParsing -Uri "https://the.earth.li/~sgtatham/putty/latest/w64/plink.exe" -OutFile $plink
  }
  if (-not (Test-Path $pscp)) {
    Invoke-WebRequest -UseBasicParsing -Uri "https://the.earth.li/~sgtatham/putty/latest/w64/pscp.exe" -OutFile $pscp
  }
  return @{ Plink = $plink; Pscp = $pscp }
}

function Get-HostKey([string]$sshHost, [string]$port) {
  $oldPref = $ErrorActionPreference
  $ErrorActionPreference = "Continue"
  $algos = @("ed25519", "rsa")
  foreach ($algo in $algos) {
    $scan = & ssh-keyscan -p $port -t $algo $sshHost 2>$null
    if ([string]::IsNullOrWhiteSpace($scan)) { continue }
    $tmp = New-TemporaryFile
    Set-Content -LiteralPath $tmp -Value $scan
    $fp = & ssh-keygen -lf $tmp -E sha256 2>$null
    Remove-Item -LiteralPath $tmp -Force -ErrorAction SilentlyContinue
    $m = [regex]::Match($fp, '(\d+)\s+SHA256:([A-Za-z0-9+/=]+)')
    if (-not $m.Success) { continue }

    $ErrorActionPreference = $oldPref
    if ($algo -eq "ed25519") { return "ssh-ed25519 $($m.Groups[1].Value) SHA256:$($m.Groups[2].Value)" }
    if ($algo -eq "rsa") { return "ssh-rsa $($m.Groups[1].Value) SHA256:$($m.Groups[2].Value)" }
  }
  $ErrorActionPreference = $oldPref
  throw "ssh-keyscan failed"
}

$localBin = Join-Path $root "publish/edge-collector/collector-linux-arm64"
if (-not (Test-Path $localBin)) { throw "local binary not found: $localBin" }

$remote = "$user@$sshHost"
$putty = Ensure-Putty (Join-Path $root "src/edge/.run/tools/putty")
$hostkey = Get-HostKey $sshHost $port

function Run-Remote([string]$cmd) {
  Write-Host "Running: $cmd"
  & $putty.Plink -batch -hostkey $hostkey -ssh -P $port -pw $pass $remote $cmd
  if ($LASTEXITCODE -ne 0) { throw "plink failed ($LASTEXITCODE)" }
}

function Copy-Remote([string]$src, [string]$dst) {
  "" | & $putty.Pscp -batch -hostkey $hostkey -P $port -pw $pass $src "${remote}:$dst"
  if ($LASTEXITCODE -ne 0) { throw "pscp failed ($LASTEXITCODE)" }
}

function Copy-RemoteDir([string]$srcDir, [string]$dstDir) {
  "" | & $putty.Pscp -batch -hostkey $hostkey -r -P $port -pw $pass $srcDir "${remote}:$dstDir"
  if ($LASTEXITCODE -ne 0) { throw "pscp failed ($LASTEXITCODE)" }
}

$stopCmd = "set -e; mkdir -p `"$RemoteDir`"; cd `"$RemoteDir`"; if [ -f collector.pid ]; then kill -9 `$(cat collector.pid) 2>/dev/null || true; rm -f collector.pid; fi; killall -9 collector 2>/dev/null || true; fuser -k 8080/tcp 2>/dev/null || true; sleep 1; rm -f collector"
Run-Remote $stopCmd

Copy-Remote $localBin "$RemoteDir/collector"

if ($WithData) {
  $localRun = Join-Path $root "publish/edge-collector/.run"
  if (-not (Test-Path $localRun)) { throw "local .run not found: $localRun" }
  Copy-RemoteDir $localRun "$RemoteDir/.run"
}

$startCmd = "set -e; cd `"$RemoteDir`"; chmod +x ./collector; EDGE_HTTP_BIND=0.0.0.0 EDGE_ALLOW_CONTROL=1 nohup ./collector > collector.log 2>&1 & echo `$! > collector.pid; sleep 1; tail -n 80 collector.log || true"
Run-Remote $startCmd
