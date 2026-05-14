@echo off
setlocal

set ROOT=%~dp0..
for %%I in ("%ROOT%") do set ROOT=%%~fI

if exist "%ROOT%\.run\collector.pid" (
  for /f "usebackq delims=" %%p in ("%ROOT%\.run\collector.pid") do (
    if not "%%p"=="" taskkill /F /PID %%p >NUL 2>&1
  )
)

for %%P in (8080 8000 25001) do (
  for /f "tokens=5" %%a in ('netstat -ano ^| findstr /R /C:":%%P .*LISTENING"') do (
    taskkill /F /PID %%a >NUL 2>&1
  )
)

cd /d "%ROOT%"
set EDGE_ALLOW_CONTROL=1

if exist "C:\Program Files\Go\bin\go.exe" (
  start "Collector" cmd /k "\"C:\Program Files\Go\bin\go.exe\" run .\cmd\collector"
) else (
  start "Collector" cmd /k "go run .\cmd\collector"
)

endlocal

