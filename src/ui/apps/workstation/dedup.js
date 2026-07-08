const fs = require('fs');
const path = './src/i18n.ts';
const content = fs.readFileSync(path, 'utf8');

let newContent = content;

// Replace for en
const enMatch = newContent.match(/en: \{\s*translation: (\{[\s\S]*?\})\s*\}/);
if (enMatch) {
  let enObj = enMatch[1];
  let lines = enObj.split('\n');
  let seen = new Set();
  let newLines = [];
  for (let i = lines.length - 1; i >= 0; i--) {
    let line = lines[i];
    let kvMatch = line.match(/^\s*"([^"]+)"\s*:/);
    if (kvMatch) {
      if (!seen.has(kvMatch[1])) {
        seen.add(kvMatch[1]);
        newLines.unshift(line);
      }
    } else {
      newLines.unshift(line);
    }
  }
  newContent = newContent.replace(enObj, newLines.join('\n'));
}

// Replace for zh
const zhMatch = newContent.match(/zh: \{\s*translation: (\{[\s\S]*?\})\s*\}/);
if (zhMatch) {
  let zhObj = zhMatch[1];
  let lines = zhObj.split('\n');
  let seen = new Set();
  let newLines = [];
  for (let i = lines.length - 1; i >= 0; i--) {
    let line = lines[i];
    let kvMatch = line.match(/^\s*"([^"]+)"\s*:/);
    if (kvMatch) {
      if (!seen.has(kvMatch[1])) {
        seen.add(kvMatch[1]);
        newLines.unshift(line);
      }
    } else {
      newLines.unshift(line);
    }
  }
  newContent = newContent.replace(zhObj, newLines.join('\n'));
}

fs.writeFileSync(path, newContent);
console.log('Deduplicated i18n.ts');