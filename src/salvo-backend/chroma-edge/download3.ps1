$ErrorActionPreference = "Stop"
$url = "http://10.8.5.7:8080/chroma-edge.exe"
$outFile = "C:\Users\trae\Desktop\chroma-edge\chroma-edge.exe"
if (Test-Path $outFile) { Remove-Item $outFile -Force }
$out = [System.IO.File]::Create($outFile)
Write-Host "Downloading chroma-edge.exe..."
$req = [System.Net.HttpWebRequest]::Create($url)
$resp = $req.GetResponse()
$stream = $resp.GetResponseStream()
$stream.CopyTo($out)
$resp.Close()
$out.Close()
Write-Host "Download complete."
