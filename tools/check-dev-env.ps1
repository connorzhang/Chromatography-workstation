$ErrorActionPreference = 'Stop'

function Test-PathReport([string]$path) {
  if (Test-Path $path) {
    "OK  $path"
  } else {
    "MISS $path"
  }
}

"Machine: $env:COMPUTERNAME"
"User:    $env:USERNAME"
""

$vsWhere = "$env:ProgramFiles(x86)\Microsoft Visual Studio\Installer\vswhere.exe"
if (Test-Path $vsWhere) {
  "vswhere: OK"
  & $vsWhere -latest -products * -format json | Out-String
} else {
  "vswhere: MISS ($vsWhere)"
}

""
"DevExpress 22.2 (csproj default path):"
Test-PathReport "C:\Program Files\DevExpress 22.2\Components\Bin\Framework\DevExpress.Utils.v22.2.dll"

""
".NET Framework 4.8 reference assemblies (targeting pack):"
Test-PathReport "C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.8"

""
"Repo SF-G files:"
$repoRoot = Split-Path -Parent $PSScriptRoot
$sfG = Join-Path $repoRoot 'SF-G'
$expect = @(
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

if (-not (Test-Path $sfG)) {
  "MISS $sfG"
} else {
  foreach ($f in $expect) {
    Test-PathReport (Join-Path $sfG $f)
  }
}

