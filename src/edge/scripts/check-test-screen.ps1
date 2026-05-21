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

function Ensure-Putty([string]$dir) {
  New-Item -ItemType Directory -Force -Path $dir | Out-Null
  $plink = Join-Path $dir "plink.exe"
  if (-not (Test-Path $plink)) {
    Invoke-WebRequest -UseBasicParsing -Uri "https://the.earth.li/~sgtatham/putty/latest/w64/plink.exe" -OutFile $plink
  }
  return $plink
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
  throw "cannot parse host key fingerprint"
}

$root = Get-RepoRoot
$kv = Read-DotEnv (Join-Path $root ".env")

$sshHost = $kv["TEST_SCREEN_SSH_HOST"]
$port = $kv["TEST_SCREEN_SSH_PORT"]
$user = $kv["TEST_SCREEN_SSH_USER"]
$pass = $kv["TEST_SCREEN_SSH_PASSWORD"]

if ([string]::IsNullOrWhiteSpace($sshHost)) { throw "TEST_SCREEN_SSH_HOST is missing in .env" }
if ([string]::IsNullOrWhiteSpace($port)) { $port = "22" }
if ([string]::IsNullOrWhiteSpace($user)) { $user = "root" }
if ([string]::IsNullOrWhiteSpace($pass)) { throw "TEST_SCREEN_SSH_PASSWORD is missing in .env" }

$remote = "$user@$sshHost"
$plink = Ensure-Putty (Join-Path $root "src/edge/.run/tools/putty")
$hostkey = Get-HostKey $sshHost $port

$cmd = "set -e; cd `"$RemoteDir`"; echo '== ls =='; ls -lah; echo '== pid =='; cat collector.pid 2>/dev/null || true; if [ -f collector.pid ]; then echo '== ps =='; ps -p `$(cat collector.pid) -o pid,cmd || true; fi; echo '== ports =='; (ss -lntp 2>/dev/null || netstat -lntp 2>/dev/null || true) | grep -E '(:8080|:8000|:25001)' || true; echo '== log =='; tail -n 120 collector.log 2>/dev/null || true"

& $plink -batch -ssh -P $port -pw $pass -hostkey $hostkey $remote $cmd
if ($LASTEXITCODE -ne 0) { throw "plink failed ($LASTEXITCODE)" }
