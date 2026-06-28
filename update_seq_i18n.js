const fs = require('fs');
const filePath = 'src/ui/apps/workstation/src/i18n.ts';
let content = fs.readFileSync(filePath, 'utf8');

const newEnKeys = {
  'Sequence loaded successfully': 'Sequence loaded successfully',
  'No saved sequence found': 'No saved sequence found',
  'Failed to load sequence': 'Failed to load sequence',
  'Sequence saved successfully': 'Sequence saved successfully',
  'Failed to save sequence': 'Failed to save sequence'
};

const newZhKeys = {
  'Sequence loaded successfully': '序列加载成功',
  'No saved sequence found': '未找到已保存的序列',
  'Failed to load sequence': '加载序列失败',
  'Sequence saved successfully': '序列保存成功',
  'Failed to save sequence': '保存序列失败'
};

function insertKeys(langContent, keys) {
  let entries = '';
  for (const [k, v] of Object.entries(keys)) {
    if (!langContent.includes(`"${k}":`)) {
      entries += `      "${k}": "${v}",\n`;
    }
  }
  if (entries) {
    return langContent.replace(/translation:\s*\{/, `translation: {\n${entries}`);
  }
  return langContent;
}

let enIndex = content.indexOf('en: {');
let zhIndex = content.indexOf('zh: {');

if (enIndex !== -1 && zhIndex !== -1) {
  if (enIndex < zhIndex) {
    let part1 = content.slice(0, zhIndex);
    let part2 = content.slice(zhIndex);
    part1 = insertKeys(part1, newEnKeys);
    part2 = insertKeys(part2, newZhKeys);
    content = part1 + part2;
  } else {
    let part1 = content.slice(0, enIndex);
    let part2 = content.slice(enIndex);
    part1 = insertKeys(part1, newZhKeys);
    part2 = insertKeys(part2, newEnKeys);
    content = part1 + part2;
  }
  fs.writeFileSync(filePath, content, 'utf8');
  console.log("Successfully injected new sequence translations.");
} else {
  console.log("Could not find translation blocks.");
}