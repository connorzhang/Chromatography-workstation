$connections = Get-NetTCPConnection -LocalPort 8080 -ErrorAction SilentlyContinue
if ($connections) {
    foreach ($conn in $connections) {
        if ($conn.State -eq 'Listen') {
            $pid = $conn.OwningProcess
            $proc = Get-Process -Id $pid -ErrorAction SilentlyContinue
            if ($proc) {
                Write-Host "Killing process $($proc.Name) with PID $pid"
                Stop-Process -Id $pid -Force
            }
        }
    }
} else {
    Write-Host "No process listening on 8080"
}
