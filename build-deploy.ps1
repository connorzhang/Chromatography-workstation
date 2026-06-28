$ErrorActionPreference = "Stop"

Write-Host "1. Clean and create deploy directory..."
$DeployDir = "$PSScriptRoot\deploy"
if (Test-Path $DeployDir) { Remove-Item -Recurse -Force $DeployDir }
New-Item -ItemType Directory -Path $DeployDir | Out-Null

Write-Host "2. Build React Frontend (Vite)..."
Set-Location "$PSScriptRoot\src\ui\apps\workstation"
npm run build
Copy-Item -Recurse -Force "dist" "$DeployDir\dist"

Write-Host "3. Build Rust Backend (Salvo)..."
Set-Location "$PSScriptRoot\src\salvo-backend\chroma-edge"
cargo build --release
Copy-Item "target\release\chroma-edge.exe" "$DeployDir\chroma-edge.exe"

Write-Host "4. Copy Simulator..."
Set-Location $PSScriptRoot
Copy-Item "src\edge\simulator_tcd.exe" "$DeployDir\simulator_tcd.exe"

Write-Host "5. Create autostart installation script..."
$InstallScript = @"
`$ErrorActionPreference = "Stop"

`$TargetDir = `"C:\ChromatographyWorkstation`"
if (!(Test-Path `$TargetDir)) {
    New-Item -ItemType Directory -Path `$TargetDir | Out-Null
}

Write-Host `"Copying files to `$TargetDir...`"
Copy-Item -Recurse -Force `"$PSScriptRoot\*`" `"$TargetDir\`"

`$StartupFolder = [Environment]::GetFolderPath('Startup')

Write-Host `"Creating background service startup script...`"
`$VbsPath = `"`$TargetDir\start-services.vbs`"
`$VbsContent = `"Set WshShell = CreateObject(`"WScript.Shell`")`n`"
`$VbsContent += `"WshShell.CurrentDirectory = `"`$TargetDir`"`n`"
`$VbsContent += `"WshShell.Run chr(34) & `"`$TargetDir\chroma-edge.exe`" & chr(34), 0, False`n`"
`$VbsContent += `"WshShell.Run chr(34) & `"`$TargetDir\simulator_tcd.exe`" & chr(34), 0, False`n`"
Set-Content -Path `$VbsPath -Value `$VbsContent -Encoding UTF8

Write-Host `"Creating autostart shortcuts...`"
`$WshShell = New-Object -ComObject WScript.Shell
`$ServiceShortcut = `$WshShell.CreateShortcut(`"`$StartupFolder\StartChromaServices.lnk`")
`$ServiceShortcut.TargetPath = `"wscript.exe`"
`$ServiceShortcut.Arguments = `"`"`$VbsPath`"`"
`$ServiceShortcut.WorkingDirectory = `$TargetDir
`$ServiceShortcut.Save()

Write-Host `"Creating browser Kiosk shortcut...`"
`$BrowserPath = `"C:\Program Files (x86)\Microsoft\Edge\Application\msedge.exe`"
if (!(Test-Path `$BrowserPath)) {
    `$BrowserPath = `"C:\Program Files\Google\Chrome\Application\chrome.exe`"
}

if (Test-Path `$BrowserPath) {
    `$KioskShortcut = `$WshShell.CreateShortcut(`"`$StartupFolder\StartChromaUI.lnk`")
    `$KioskShortcut.TargetPath = `$BrowserPath
    `$KioskShortcut.Arguments = `"--kiosk http://127.0.0.1:8082`"
    `$KioskShortcut.Save()
    Write-Host `"Success: Kiosk mode setup.`"
} else {
    Write-Host `"Warning: Edge or Chrome not found.`" -ForegroundColor Yellow
}

Write-Host `"Deployment and Autostart setup complete!`" -ForegroundColor Green
"@

Set-Content -Path "$DeployDir\install-autostart.ps1" -Value $InstallScript -Encoding UTF8

Write-Host "Build and packaging complete!" -ForegroundColor Green
