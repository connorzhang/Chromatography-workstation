$replacements = @{
    "'/api/control/temp" = "'/api/sila2/v1/TemperatureControllerService/SetTargetTemperature"
    "'/api/control/epc" = "'/api/sila2/v1/PneumaticControllerService/SetTargetPressure"
    "'/api/control/events" = "'/api/sila2/v1/ValveControllerService/SwitchValve"
    "'/api/control/cycle" = "'/api/sila2/v1/ChromatographyService/SetCycle"
    "'/api/control/ignite" = "'/api/sila2/v1/FlameIonizationDetectorService/Ignite"
    "'/api/control/ignite_config" = "'/api/sila2/v1/FlameIonizationDetectorService/IgniteConfig"
    
    "'/api/v1/hardware" = "'/api/sila2/v1/HardwareService/Config"
    "'/api/v1/devices'" = "'/api/sila2/v1/SystemDiscoveryService/Devices'"
    "'/api/v1/sys/drivers/active" = "'/api/sila2/v1/DriverRegistryService/ActiveDriver"
    "'/api/sysconfig" = "'/api/sila2/v1/SystemConfigService/Config"
    "'/api/v1/uploadconfig" = "'/api/sila2/v1/DataExportService/Config"
    
    "'/api/method" = "'/api/animl/v1/MethodService"
    "'/api/process/detect_all" = "'/api/animl/v1/ProcessService/DetectAll"
    "'/api/process/detect_window" = "'/api/animl/v1/ProcessService/DetectWindow"
    "'/api/history/results" = "'/api/animl/v1/HistoryService/Results"
    "'/api/history/run/" = "'/api/animl/v1/HistoryService/Run/"
    "'/api/v1/session/active" = "'/api/animl/v1/SessionService/Active"
    
    # special backtick replacement for /api/v1/devices/${deviceId}/cmd
    "`/api/v1/devices/`$(`{deviceId`})/cmd" = "`/api/sila2/v1/SystemDiscoveryService/DeviceCmd/`$(`{deviceId`})/cmd"
}

$files = Get-ChildItem -Path "src/edge/cmd/collector/static/js" -Recurse -Filter "*.js"

foreach ($file in $files) {
    $content = Get-Content $file.FullName -Raw -Encoding UTF8
    $modified = $false
    foreach ($key in $replacements.Keys) {
        if ($content -match [regex]::Escape($key)) {
            $content = $content -replace [regex]::Escape($key), $replacements[$key]
            $modified = $true
        }
    }
    if ($modified) {
        Set-Content -Path $file.FullName -Value $content -NoNewline -Encoding UTF8
        Write-Host "Updated $($file.Name)"
    }
}
