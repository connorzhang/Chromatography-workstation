const fs = require('fs');
const path = require('path');

function walk(dir) {
  const files = fs.readdirSync(dir);
  for (const file of files) {
    const fullPath = path.join(dir, file);
    const stat = fs.statSync(fullPath);
    if (stat.isDirectory()) {
      walk(fullPath);
    } else if (file.endsWith('.tsx') || file.endsWith('.ts')) {
      let content = fs.readFileSync(fullPath, 'utf8');
      let changed = false;
      if (content.includes(':8080/api')) { content = content.replace(/:8080\/api/g, ':8082/api'); changed = true; }
      if (content.includes(':8080/ws')) { content = content.replace(/:8080\/ws/g, ':8082/ws'); changed = true; }
      if (changed) {
        fs.writeFileSync(fullPath, content, 'utf8');
        console.log('Updated ' + fullPath);
      }
    }
  }
}

walk('D:/GIT/VS2022/Chromatography-workstation/src/ui/apps/workstation/src');
