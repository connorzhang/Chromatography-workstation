$path = 'src/edge/cmd/collector/static/js/views/settings.js'
$c = [System.IO.File]::ReadAllText((Resolve-Path $path).Path)
$c = $c -replace '        let isHwLoaded = false;\r?\n', ''
$c = $c -replace '(let deviceId = [^;]+;)', "`$1`r`n    let isHwLoaded = false;"
[System.IO.File]::WriteAllText((Resolve-Path $path).Path, $c)
