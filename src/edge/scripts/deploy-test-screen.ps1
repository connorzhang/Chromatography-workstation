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
  $tmp = New-TemporaryFile
  $scan = & ssh-keyscan -p $port -t rsa $sshHost 2>$null
  if ([string]::IsNullOrWhiteSpace($scan)) { throw "ssh-keyscan failed" }
  Set-Content -LiteralPath $tmp -Value $scan
  $fp = & ssh-keygen -lf $tmp -E sha256 2>$null
  Remove-Item -LiteralPath $tmp -Force -ErrorAction SilentlyContinue
  $m = [regex]::Match($fp, '(\d+)\s+SHA256:([A-Za-z0-9+/=]+)')
  if ($m.Success) { return "ssh-rsa $($m.Groups[1].Value) SHA256:$($m.Groups[2].Value)" }

  $tmp2 = New-TemporaryFile
  Set-Content -LiteralPath $tmp2 -Value $scan
  $fp2 = & ssh-keygen -lf $tmp2 -E md5 2>$null
  Remove-Item -LiteralPath $tmp2 -Force -ErrorAction SilentlyContinue
  $m2 = [regex]::Match($fp2, '(\d+)\s+MD5:([0-9a-f:]+)')
  if ($m2.Success) { return "ssh-rsa $($m2.Groups[1].Value) $($m2.Groups[2].Value)" }

  throw "cannot parse host key fingerprint"
}

$localBin = Join-Path $root "publish/edge-collector/collector-linux-arm64"
if (-not (Test-Path $localBin)) { throw "local binary not found: $localBin" }

$remote = "$user@$sshHost"
$putty = Ensure-Putty (Join-Path $root "src/edge/.run/tools/putty")
$hostkey = Get-HostKey $sshHost $port

function Run-Remote([string]$cmd) {
  & $putty.Plink -batch -ssh -P $port -pw $pass -hostkey $hostkey $remote $cmd
  if ($LASTEXITCODE -ne 0) { throw "plink failed ($LASTEXITCODE)" }
}

function Copy-Remote([string]$src, [string]$dst) {
  & $putty.Pscp -batch -P $port -pw $pass -hostkey $hostkey $src "${remote}:$dst"
  if ($LASTEXITCODE -ne 0) { throw "pscp failed ($LASTEXITCODE)" }
}

function Copy-RemoteDir([string]$srcDir, [string]$dstDir) {
  & $putty.Pscp -batch -r -P $port -pw $pass -hostkey $hostkey $srcDir "${remote}:$dstDir"
  if ($LASTEXITCODE -ne 0) { throw "pscp failed ($LASTEXITCODE)" }
}

$stopCmd = "set -e; mkdir -p `"$RemoteDir`"; cd `"$RemoteDir`"; if [ -f collector.pid ]; then kill `$(cat collector.pid) 2>/dev/null || true; rm -f collector.pid; fi"
Run-Remote $stopCmd

Copy-Remote $localBin "$RemoteDir/collector"

if ($WithData) {
  $localRun = Join-Path $root "publish/edge-collector/.run"
  if (-not (Test-Path $localRun)) { throw "local .run not found: $localRun" }
  Copy-RemoteDir $localRun "$RemoteDir/.run"
}

$startCmd = "set -e; cd `"$RemoteDir`"; chmod +x ./collector; EDGE_HTTP_BIND=0.0.0.0 nohup ./collector > collector.log 2>&1 & echo `$! > collector.pid; sleep 1; tail -n 80 collector.log || true"
Run-Remote $startCmd
