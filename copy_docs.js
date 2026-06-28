const fs = require('fs');
const path = require('path');

const srcDir = path.join(__dirname, 'docs');
const destDir = path.join(__dirname, 'temp_docs_v2', 'docs', 'chromatography-workstation');

const whitelistExts = ['.md', '.mdx', '.json', '.png', '.jpg', '.jpeg', '.gif', '.webp', '.svg', '.ico', '.pdf', '.txt', '.csv', '.xlsx', '.xls', '.yaml', '.yml'];

// Optimization: Strictly exclude internal architecture, planning, and development docs
const blacklistNames = [
    '.env', 'node_modules', 'dist', 'build', '.git', '.vscode', '.idea',
    'architecture.md', 'architecture-salvo-react.md', 'development.md',
    'roadmap-salvo-react.md', 'agilent_full_replica_plan.md', 'refactor'
];

function isAllowed(fileName, filePath) {
    // Check blacklist
    for (const bl of blacklistNames) {
        if (fileName.includes(bl) || filePath.includes(bl)) return false;
    }
    
    // Check whitelist
    const ext = path.extname(fileName).toLowerCase();
    if (whitelistExts.includes(ext) || fileName === 'project.json' || fileName === '_meta.json') {
        return true;
    }
    
    return false;
}

function checkConfidentiality(content, ext) {
    if (['.md', '.mdx', '.json', '.yaml', '.yml', '.txt', '.csv'].includes(ext)) {
        let safeContent = content;
        if (safeContent.includes('-----BEGIN PRIVATE KEY-----') || safeContent.includes('BEGIN RSA PRIVATE KEY')) {
            throw new Error("Melt: Detected private key in content!");
        }
        // Desensitize possible internal IPs if needed
        safeContent = safeContent.replace(/10\.\d+\.\d+\.\d+/g, '192.168.1.x');
        return safeContent;
    }
    return content;
}

function copyDir(src, dest) {
    if (!fs.existsSync(dest)) {
        fs.mkdirSync(dest, { recursive: true });
    }
    
    const entries = fs.readdirSync(src, { withFileTypes: true });
    
    for (const entry of entries) {
        const srcPath = path.join(src, entry.name);
        const destPath = path.join(dest, entry.name);
        
        if (entry.isDirectory()) {
            if (!blacklistNames.includes(entry.name)) {
                copyDir(srcPath, destPath);
            }
        } else {
            if (isAllowed(entry.name, srcPath)) {
                const ext = path.extname(entry.name).toLowerCase();
                if (['.md', '.mdx', '.json', '.yaml', '.yml', '.txt', '.csv'].includes(ext)) {
                    const content = fs.readFileSync(srcPath, 'utf8');
                    try {
                        const safeContent = checkConfidentiality(content, ext);
                        fs.writeFileSync(destPath, safeContent, 'utf8');
                    } catch (e) {
                        console.error(`Skipping ${srcPath} due to security melt: ${e.message}`);
                    }
                } else {
                    fs.copyFileSync(srcPath, destPath);
                }
                console.log(`Copied: ${entry.name}`);
            } else {
                console.log(`Excluded (Blacklist/Optimization): ${entry.name}`);
            }
        }
    }
}

if (fs.existsSync(destDir)) {
    fs.rmSync(destDir, { recursive: true, force: true });
}

copyDir(srcDir, destDir);
console.log("Copy complete with optimization rules.");
