netstat -ano | findstr :8080 > ns.txt
for /f "tokens=5" %%a in ('netstat -aon ^| findstr :8080 ^| findstr LISTENING') do taskkill /F /PID %%a > tk.txt 2>&1
