const { NodeSSH } = require('node-ssh');
const fs = require('fs');
const path = require('path');

async function main() {
    const ssh = new NodeSSH();
    try {
        console.log('Connecting to 10.8.5.50...');
        await ssh.connect({
            host: '10.8.5.50',
            username: 'root',
            password: '123456',
            readyTimeout: 10000
        });
        console.log('Connected!');

        console.log('Stopping service...');
        await ssh.execCommand('systemctl stop chroma-edge.service');

        console.log('Uploading chroma-edge...');
        await ssh.putFile(
            'src/salvo-backend/target/aarch64-unknown-linux-gnu/release/chroma-edge',
            '/opt/chromatography-workstation/chroma-edge'
        );
        console.log('Upload complete!');

        console.log('Setting permissions and restarting service...');
        await ssh.execCommand('chmod +x /opt/chromatography-workstation/chroma-edge');
        await ssh.execCommand('systemctl start chroma-edge.service');
        
        console.log('Waiting for service to start...');
        await new Promise(r => setTimeout(r, 2000));

        console.log('Listing assets...');
        const ls = await ssh.execCommand('ls -1 /opt/chromatography-workstation/dist/assets/ | grep .js | head -n 1');
        const jsFile = ls.stdout.trim();
        console.log(`Found JS file: ${jsFile}`);

        if (jsFile) {
            console.log(`Testing HTTP GET to /assets/${jsFile}...`);
            const curl = await ssh.execCommand(`curl -s -o /dev/null -w "%{http_code}" http://127.0.0.1:8082/assets/${jsFile}`);
            console.log(`HTTP Status: ${curl.stdout}`);
        }

        console.log('Checking service logs...');
        const logs = await ssh.execCommand('journalctl -u chroma-edge.service -n 20 --no-pager');
        console.log(logs.stdout);

    } catch (e) {
        console.error('Error:', e);
    } finally {
        ssh.dispose();
    }
}

main();