$ErrorActionPreference = 'Stop'

$root = Resolve-Path (Join-Path $PSScriptRoot '..')
$logDir = Join-Path $root '.run'
$logPath = Join-Path $logDir 'guardian.log'

New-Item -ItemType Directory -Force -Path $logDir | Out-Null

function Write-GuardianLog([string]$msg) {
  $line = (Get-Date).ToString('s') + ' ' + $msg
  Add-Content -Path $logPath -Value $line
}

Push-Location $root
try {
  while ($true) {
    Write-GuardianLog 'collector starting'
    try {
      if (-not $env:EDGE_HTTP_BIND -or $env:EDGE_HTTP_BIND.Trim().Length -eq 0) {
        $env:EDGE_HTTP_BIND = '0.0.0.0'
      }
      & "C:\Program Files\Go\bin\go.exe" run .\cmd\collector
      Write-GuardianLog 'collector exited (code=0)'
    } catch {
      Write-GuardianLog ('collector crashed: ' + $_.Exception.Message)
    }
    Start-Sleep -Seconds 1
  }
} finally {
  Pop-Location
}

