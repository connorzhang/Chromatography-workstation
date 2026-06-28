const fs = require('fs');
const path = require('path');

const pagesDir = path.join(__dirname, 'src/ui/apps/workstation/src/pages');

function getBaseApiStr() {
    return "const apiBase = window.location.port === '5173' ? `http://${window.location.hostname}:8082` : '';\n";
}

function processFile(filePath) {
    let content = fs.readFileSync(filePath, 'utf8');
    let changed = false;

    // Replace wsUrl
    if (content.includes('ws://${hostname}:8082')) {
        content = content.replace(
            /const wsUrl = `ws:\/\/\$\{hostname\}:8082(.*?)`;/g,
            "const wsProtocol = window.location.protocol === 'https:' ? 'wss:' : 'ws:';\n      const wsHost = window.location.port === '5173' ? `${window.location.hostname}:8082` : window.location.host;\n      const wsUrl = `${wsProtocol}//${wsHost}$1`;"
        );
        changed = true;
    }

    // Replace fetch HTTP
    if (content.includes('http://${hostname}:8082') || content.includes('http://${window.location.hostname}:8082')) {
        // Add apiBase definition at the beginning of the component
        // But doing it via regex might be tricky, so let's just inline it
        content = content.replace(
            /`http:\/\/\$\{hostname\}:8082(\/api\/.*?)`/g,
            "(window.location.port === '5173' ? `http://${window.location.hostname}:8082` : '') + `$1`"
        );
        content = content.replace(
            /`http:\/\/\$\{window\.location\.hostname\}:8082(\/api\/.*?)`/g,
            "(window.location.port === '5173' ? `http://${window.location.hostname}:8082` : '') + `$1`"
        );
        changed = true;
    }

    if (changed) {
        fs.writeFileSync(filePath, content);
        console.log('Updated:', filePath);
    }
}

function walkDir(dir) {
    const files = fs.readdirSync(dir);
    for (const file of files) {
        const fullPath = path.join(dir, file);
        if (fs.statSync(fullPath).isDirectory()) {
            walkDir(fullPath);
        } else if (fullPath.endsWith('.tsx') || fullPath.endsWith('.ts')) {
            processFile(fullPath);
        }
    }
}

walkDir(pagesDir);
