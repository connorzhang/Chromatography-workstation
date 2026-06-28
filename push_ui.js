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

        console.log('Uploading UI dist folder...');
        const localDist = path.join(__dirname, 'src/ui/apps/workstation/dist');
        const remoteDist = '/opt/chromatography-workstation/dist';
        
        await ssh.execCommand(`rm -rf ${remoteDist}/*`);
        await ssh.putDirectory(localDist, remoteDist, {
            recursive: true,
            concurrency: 10
        });
        
        console.log('Upload complete!');

    } catch (e) {
        console.error('Error:', e);
    } finally {
        ssh.dispose();
    }
}

main();