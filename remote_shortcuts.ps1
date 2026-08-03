$WshShell = New-Object -comObject WScript.Shell
$desktop = "C:\Users\Public\Desktop"

# 1. 尝试清理之前可能因为编码问题产生的乱码快捷方式 (通过匹配目标路径来安全删除)
Get-ChildItem -Path $desktop -Filter "*.lnk" | ForEach-Object {
    $link = $WshShell.CreateShortcut($_.FullName)
    if ($link.TargetPath -match "schtasks.exe|taskkill.exe|powershell.exe" -and ($link.Arguments -match "EdgeCollector|ChromaEdge|collector.exe|KillChroma")) {
        Remove-Item $_.FullName -Force -ErrorAction SilentlyContinue
    }
}

# 2. 创建纯英文名称的快捷方式，彻底杜绝中文在远程机器上变成乱码
# 并且强制使用 cmd.exe /c "... & pause" ，这样运行后黑框会停留，您能清楚看到是成功了还是报了“拒绝访问”等错误

$s1 = $WshShell.CreateShortcut("$desktop\1_Start_GO.lnk")
$s1.TargetPath = "cmd.exe"
$s1.Arguments = "/c `"schtasks /run /tn EdgeCollector & echo. & pause`""
$s1.IconLocation = "shell32.dll,137"
$s1.Save()

$s2 = $WshShell.CreateShortcut("$desktop\2_Stop_GO.lnk")
$s2.TargetPath = "cmd.exe"
$s2.Arguments = "/c `"taskkill /f /im collector.exe & echo. & pause`""
$s2.IconLocation = "shell32.dll,27"
$s2.Save()

$s3 = $WshShell.CreateShortcut("$desktop\3_Start_RUST.lnk")
$s3.TargetPath = "cmd.exe"
$s3.Arguments = "/c `"schtasks /run /tn ChromaEdge & echo. & pause`""
$s3.IconLocation = "shell32.dll,137"
$s3.Save()

$s4 = $WshShell.CreateShortcut("$desktop\4_Stop_RUST.lnk")
$s4.TargetPath = "cmd.exe"
$s4.Arguments = "/c `"schtasks /end /tn ChromaEdge 2>nul & schtasks /run /tn KillChroma & echo. & pause`""
$s4.IconLocation = "shell32.dll,27"
$s4.Save()

Write-Host "Clean English shortcuts created with PAUSE for debugging."