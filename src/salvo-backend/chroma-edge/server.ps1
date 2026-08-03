$listener = New-Object System.Net.HttpListener
$listener.Prefixes.Add("http://*:9999/")
$listener.Start()
Write-Host "Listening on port 9999..."
$context = $listener.GetContext()
$response = $context.Response
$file = [System.IO.File]::ReadAllBytes("deploy_chroma_edge.zip")
$response.ContentLength64 = $file.Length
$response.OutputStream.Write($file, 0, $file.Length)
$response.Close()
$listener.Stop()
Write-Host "File sent!"