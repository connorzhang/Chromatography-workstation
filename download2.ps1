$ErrorActionPreference = "Stop"
$url = "http://10.8.5.7:8080/deploy_chroma_edge.zip"
$outFile = "C:\Users\trae\Desktop\chroma-edge\deploy_chroma_edge.zip"
if (Test-Path $outFile) { Remove-Item $outFile -Force }
$out = [System.IO.File]::Create($outFile)
Write-Host "Downloading full file..."
$req = [System.Net.HttpWebRequest]::Create($url)
$resp = $req.GetResponse()
$stream = $resp.GetResponseStream()
$stream.CopyTo($out)
$resp.Close()
$out.Close()
Write-Host "Download complete."
