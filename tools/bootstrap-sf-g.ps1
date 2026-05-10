$ErrorActionPreference = 'Stop'

$repoRoot = Split-Path -Parent $PSScriptRoot
$src = Join-Path $repoRoot 'bin\Debug\net48'
$dst = Join-Path $repoRoot 'SF-G'

if (-not (Test-Path $src)) {
  throw "Source directory not found: $src"
}

New-Item -ItemType Directory -Force -Path $dst | Out-Null

$files = @(
  'System.Data.SQLite.dll',
  'SQLite.Interop.dll',
  'NPOI.dll',
  'NPOI.OOXML.dll',
  'NPOI.OpenXml4Net.dll',
  'NPOI.OpenXmlFormats.dll',
  'HZH_Controls.dll',
  'Microsoft.Office.Interop.Word.dll',
  'NPlot.dll',
  'DevComponents.DotNetBar2.dll',
  'dog_net_windows.dll',
  'log4net.dll'
)

$missing = New-Object System.Collections.Generic.List[string]
foreach ($f in $files) {
  $from = Join-Path $src $f
  if (Test-Path $from) {
    Copy-Item -Force $from (Join-Path $dst $f)
  } else {
    $missing.Add($f)
  }
}

if ($missing.Count -gt 0) {
  $missingText = ($missing | Sort-Object) -join ', '
  throw "Missing files in $src: $missingText"
}

Get-ChildItem -LiteralPath $dst -File | Sort-Object Name | Select-Object Name, Length

