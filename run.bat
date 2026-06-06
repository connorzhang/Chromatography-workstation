@echo off
cd src\edge\cmd\collector
collector.exe > log.txt 2>&1
echo Done running collector
