const { NodeSSH } = require('node-ssh');
const fs = require('fs');

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

        console.log('Stopping simulator service...');
        await ssh.execCommand('systemctl stop chroma-simulator.service');

        console.log('Uploading chroma-sim...');
        await ssh.putFile(
            'src/salvo-backend/target/aarch64-unknown-linux-gnu/release/chroma-sim',
            '/opt/chromatography-workstation/chroma-sim'
        );
        console.log('Upload complete!');

        console.log('Updating systemd service...');
        const serviceFile = `
[Unit]
Description=Chromatography Hardware Simulator (Rust SCPI TCP 8081)
After=network.target

[Service]
Type=simple
WorkingDirectory=/opt/chromatography-workstation
ExecStart=/opt/chromatography-workstation/chroma-sim
Restart=always
RestartSec=3

[Install]
WantedBy=multi-user.target
`;
        await ssh.execCommand(`cat << 'EOF' > /etc/systemd/system/chroma-simulator.service
${serviceFile.trim()}
EOF`);

        console.log('Setting permissions and restarting service...');
        await ssh.execCommand('chmod +x /opt/chromatography-workstation/chroma-sim');
        await ssh.execCommand('systemctl daemon-reload');
        await ssh.execCommand('systemctl start chroma-simulator.service');
        await ssh.execCommand('systemctl restart chroma-edge.service'); // Restart edge to reconnect
        
        console.log('Waiting for service to start...');
        await new Promise(r => setTimeout(r, 2000));

        console.log('Checking service status...');
        const simStatus = await ssh.execCommand('systemctl status chroma-simulator.service | grep Active');
        console.log('Simulator:', simStatus.stdout);
        const edgeStatus = await ssh.execCommand('systemctl status chroma-edge.service | grep Active');
        console.log('Edge:', edgeStatus.stdout);

        console.log('Checking TCP port 8081...');
        const netstat = await ssh.execCommand('netstat -tlnp | grep 8081');
        console.log(netstat.stdout);

    } catch (e) {
        console.error('Error:', e);
    } finally {
        ssh.dispose();
    }
}

main();