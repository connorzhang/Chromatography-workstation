const fs = require('fs');
const path = './src/i18n.ts';
let content = fs.readFileSync(path, 'utf8');

const newEn = {
  'Action': 'Action',
  'Add Time Event': 'Add Time Event',
  'Integration': 'Integration',
  'Tangent Skim': 'Tangent Skim',
  'Drop Baseline': 'Drop Baseline',
  'Apply & Re-integrate': 'Apply & Re-integrate',
  'Close': 'Close'
};

const newZh = {
  'Action': '操作',
  'Add Time Event': '添加时间事件',
  'Integration': '积分开关',
  'Tangent Skim': '切线撇峰',
  'Drop Baseline': '基线保持',
  'Apply & Re-integrate': '应用并重新积分',
  'Close': '关闭'
};

function inject(str, lang, newDict) {
  const match = str.match(new RegExp(lang + ': \\{\\s*translation: (\\{[\\s\\S]*?\\})\\s*\\}'));
  if (!match) return str;
  const objStr = match[1];
  let lines = objStr.split('\n');
  const lastLine = lines.pop(); // the closing brace
  
  for (const [k, v] of Object.entries(newDict)) {
    if (!objStr.includes('"' + k + '"')) {
      // make sure previous line has a comma
      if (lines.length > 1 && !lines[lines.length - 1].endsWith(',')) {
        lines[lines.length - 1] += ',';
      }
      lines.push('      "' + k + '": "' + v + '",');
    }
  }
  lines.push(lastLine);
  return str.replace(objStr, lines.join('\n'));
}

content = inject(content, 'en', newEn);
content = inject(content, 'zh', newZh);

fs.writeFileSync(path, content);
console.log('Injected translations.');