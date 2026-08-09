$ErrorActionPreference = "Stop"
cd i:\GIT\VS2022\Chromatography-workstation\src\edge\cmd\collector
Write-Host "Zipping..."
Compress-Archive -Path "collector.exe", "static", ".env", "..\..\..\..\collector-service.exe", "..\..\..\..\collector-service.xml" -DestinationPath "deploy_new.zip" -Force
Write-Host "Done"
