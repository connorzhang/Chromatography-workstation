$ErrorActionPreference = "Stop"
Set-Location "D:\GIT\VS2022\Chromatography-workstation\src\edge\cmd\collector"
try {
    go build -o c8081.exe 2>&1 | Out-File "D:\GIT\VS2022\Chromatography-workstation\build_output.txt" -Encoding utf8
} catch {
    $_.Exception.Message | Out-File "D:\GIT\VS2022\Chromatography-workstation\build_output.txt" -Encoding utf8
}
