const fs = require('fs');
const path = require('path');

const projectJsonPath = path.join(__dirname, 'docs', 'project.json');
let projectData = JSON.parse(fs.readFileSync(projectJsonPath, 'utf8'));

const now = new Date();
const offset = now.getTimezoneOffset();
const sign = offset > 0 ? '-' : '+';
const absOffset = Math.abs(offset);
const hours = String(Math.floor(absOffset / 60)).padStart(2, '0');
const minutes = String(absOffset % 60).padStart(2, '0');
const isoTime = now.getFullYear() + '-' +
    String(now.getMonth() + 1).padStart(2, '0') + '-' +
    String(now.getDate()).padStart(2, '0') + 'T' +
    String(now.getHours()).padStart(2, '0') + ':' +
    String(now.getMinutes()).padStart(2, '0') + ':' +
    String(now.getSeconds()).padStart(2, '0') + sign + hours + ':' + minutes;

projectData.doc_synced_at = isoTime;

// Execute git rev-parse HEAD to get the latest commit
const { execSync } = require('child_process');
try {
    const commit = execSync('git rev-parse HEAD').toString().trim();
    projectData.doc_source_commit = commit;
} catch (e) {
    console.error(e);
}

fs.writeFileSync(projectJsonPath, JSON.stringify(projectData, null, 2), 'utf8');

// Update _meta.json with optimized structure
const metaData = [
    "index",
    {
        "type": "dir",
        "name": "01-overview",
        "label": "📖 产品说明"
    },
    {
        "type": "dir",
        "name": "04-deployment",
        "label": "🚀 部署与快速开始"
    },
    {
        "type": "dir",
        "name": "03-api-design",
        "label": "💻 API 接口文档"
    },
    {
        "type": "dir",
        "name": "02-protocols",
        "label": "🔌 协议与硬件接入"
    },
    {
        "type": "dir",
        "name": "schemas",
        "label": "📄 数据结构 (Schemas)"
    },
    {
        "type": "file",
        "name": "api",
        "label": "REST API"
    },
    {
        "type": "file",
        "name": "deployment",
        "label": "部署指南"
    },
    {
        "type": "file",
        "name": "sila2",
        "label": "SiLA2 标准"
    },
    {
        "type": "file",
        "name": "troubleshooting",
        "label": "🛠️ 故障排除"
    }
];

const metaJsonPath = path.join(__dirname, 'docs', '_meta.json');
fs.writeFileSync(metaJsonPath, JSON.stringify(metaData, null, 2), 'utf8');

// Update index.md meta line
const indexMdPath = path.join(__dirname, 'docs', 'index.md');
let content = fs.readFileSync(indexMdPath, 'utf8');
content = content.replace(/^> 🏷️ 当前版本: .*?\n\n/m, '');
const metaLine = `> 🏷️ 当前版本: ${projectData.doc_version} | ⏱️ 最后同步: ${now.getFullYear()}-${String(now.getMonth()+1).padStart(2,'0')}-${String(now.getDate()).padStart(2,'0')} ${String(now.getHours()).padStart(2,'0')}:${String(now.getMinutes()).padStart(2,'0')}:${String(now.getSeconds()).padStart(2,'0')} | 🔗 构建 Commit: ${projectData.doc_source_commit.substring(0,7)}\n\n`;

if (content.startsWith('# ')) {
    const lines = content.split('\n');
    lines.splice(1, 0, '\n' + metaLine.trim());
    content = lines.join('\n');
} else {
    content = `# 色谱工作站\n\n${metaLine}` + content;
}
fs.writeFileSync(indexMdPath, content, 'utf8');

console.log("Optimized meta and project data.");
