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

