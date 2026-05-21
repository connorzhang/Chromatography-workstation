@echo off
setlocal
powershell -ExecutionPolicy Bypass -File "%~dp0check-test-screen.ps1" %*
