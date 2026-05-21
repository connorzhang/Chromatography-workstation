@echo off
setlocal
powershell -ExecutionPolicy Bypass -File "%~dp0deploy-test-screen.ps1" %*
