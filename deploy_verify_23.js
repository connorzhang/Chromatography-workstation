const { NodeSSH } = require('node-ssh');
const ssh = new NodeSSH();
const path = require('path');

async function deployAndVerify() {
  try {
    await ssh.connect({
      host: '10.8.5.23',
      port: 22,
      username: 'trae',
      password: 'a1234567A'
    });
    console.log('Connected to 10.8.5.23');
    
    await ssh.execCommand('taskkill /F /IM chroma-collector.exe /T');
    
    const localExe = path.join(__dirname, 'chroma-collector.exe');
    const remoteExe = 'C:\\Users\\trae\\Desktop\\ChromaTest\\chroma-collector.exe';
    
    console.log('Uploading corrected executable...');
    await ssh.putFile(localExe, remoteExe);
    console.log('Upload complete.');
    
    console.log('Starting remote service via WMI...');
    await ssh.execCommand('powershell -Command "Invoke-WmiMethod -Class Win32_Process -Name Create -ArgumentList \'cmd.exe /c C:\\Users\\trae\\run_app.bat\'"');
    
    console.log('Waiting for service initialization (4 seconds)...');
    await new Promise(r=>setTimeout(r, 4000));
    
    console.log('--- Verifying the service on 10.8.5.23 ---');
    const apiTest = await ssh.execCommand('curl.exe -s -m 5 http://127.0.0.1:8080/api/license/status');
    console.log('Local API Response:', apiTest.stdout || apiTest.stderr);
    
    ssh.dispose();
  } catch(e) {
    console.error(e);
    ssh.dispose();
    process.exit(1);
  }
}
deployAndVerify();