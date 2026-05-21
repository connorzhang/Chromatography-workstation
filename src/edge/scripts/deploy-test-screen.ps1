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

$host = $kv["TEST_SCREEN_SSH_HOST"]
$port = $kv["TEST_SCREEN_SSH_PORT"]
$user = $kv["TEST_SCREEN_SSH_USER"]

if ([string]::IsNullOrWhiteSpace($host)) { throw "TEST_SCREEN_SSH_HOST is missing in .env" }
if ([string]::IsNullOrWhiteSpace($port)) { $port = "22" }
if ([string]::IsNullOrWhiteSpace($user)) { $user = "root" }

$localBin = Join-Path $root "publish/edge-collector/collector-linux-arm64"
if (-not (Test-Path $localBin)) { throw "local binary not found: $localBin" }

$remote = "$user@$host"

$stopCmd = "set -e; mkdir -p `"$RemoteDir`"; cd `"$RemoteDir`"; if [ -f collector.pid ]; then kill `$(cat collector.pid) 2>/dev/null || true; rm -f collector.pid; fi"
& ssh -p $port $remote $stopCmd

& scp -P $port $localBin "$remote:$RemoteDir/collector"

if ($WithData) {
  $localRun = Join-Path $root "publish/edge-collector/.run"
  if (-not (Test-Path $localRun)) { throw "local .run not found: $localRun" }
  & scp -P $port -r $localRun "$remote:$RemoteDir/.run"
}

$startCmd = "set -e; cd `"$RemoteDir`"; chmod +x ./collector; nohup ./collector > collector.log 2>&1 & echo `$! > collector.pid; sleep 1; tail -n 80 collector.log || true"
& ssh -p $port $remote $startCmd
