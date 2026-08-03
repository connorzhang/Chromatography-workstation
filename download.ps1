$ErrorActionPreference = "Stop"
$url = "http://10.8.5.7:8080/deploy_chroma_edge.zip"
$outFile = "C:\Users\trae\Desktop\chroma-edge\deploy_chroma_edge.zip"
$chunkSize = 10240
$fileSize = 2660621

if (Test-Path $outFile) { Remove-Item $outFile -Force }
$out = [System.IO.File]::Create($outFile)
for ($i = 0; $i -lt $fileSize; $i += $chunkSize) {
    $end = [Math]::Min($i + $chunkSize - 1, $fileSize - 1)
    Write-Host "Downloading bytes $i-$end..."
    $req = [System.Net.HttpWebRequest]::Create($url)
    $req.AddRange($i, $end)
    $resp = $req.GetResponse()
    $stream = $resp.GetResponseStream()
    $stream.CopyTo($out)
    $resp.Close()
}
$out.Close()
Write-Host "Download complete."
