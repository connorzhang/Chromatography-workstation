$ErrorActionPreference = 'Stop'

$root = Resolve-Path (Join-Path $PSScriptRoot '..')
$pidPath = Join-Path $root '.run\collector.pid'

if (Test-Path $pidPath) {
  $pidText = (Get-Content -Raw $pidPath).Trim()
  if ($pidText -match '^\d+$') {
    $collectorPid = [int]$pidText
    try {
      Stop-Process -Id $collectorPid -Force -ErrorAction SilentlyContinue
    } catch {}
  }
}

$ports = @(8080, 8000, 25001)
foreach ($p in $ports) {
  $lines = netstat -ano -p tcp | Select-String -Pattern (":" + $p + "\s+.*LISTENING\s+(\d+)$")
  foreach ($m in $lines.Matches) {
    $owningPid = [int]$m.Groups[1].Value
    try { Stop-Process -Id $owningPid -Force -ErrorAction SilentlyContinue } catch {}
  }
}

Start-Sleep -Milliseconds 300

Push-Location $root
try {
  $env:EDGE_ALLOW_CONTROL = '1'
  if (-not $env:EDGE_HTTP_BIND -or $env:EDGE_HTTP_BIND.Trim().Length -eq 0) {
    $env:EDGE_HTTP_BIND = '0.0.0.0'
  }
  & "C:\Program Files\Go\bin\go.exe" run .\cmd\collector
} finally {
  Pop-Location
}

