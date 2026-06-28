const fs = require('fs');
const filePath = 'src/ui/apps/workstation/src/i18n.ts';
let content = fs.readFileSync(filePath, 'utf8');

const newEnKeys = {
  'Instrument Control (EPC & Valve)': 'Instrument Control (EPC & Valve)',
  'EPC Control': 'EPC Control',
  'Control Mode': 'Control Mode',
  'Constant Flow': 'Constant Flow',
  'Constant Pressure': 'Constant Pressure',
  'Ramped Flow': 'Ramped Flow',
  'Ramped Pressure': 'Ramped Pressure',
  'Initial Value': 'Initial Value',
  'Initial Time (min)': 'Initial Time (min)',
  'Ramp Table': 'Ramp Table',
  'Add Ramp': 'Add Ramp',
  'Rate': 'Rate',
  'Final Value': 'Final Value',
  'Hold Time (min)': 'Hold Time (min)',
  'No ramps configured. Operates isothermally/isobarically.': 'No ramps configured. Operates isothermally/isobarically.',
  'Valve Time Events': 'Valve Time Events',
  'No valve events configured.': 'No valve events configured.'
};

const newZhKeys = {
  'Instrument Control (EPC & Valve)': '仪器控制 (EPC 与 阀门)',
  'EPC Control': 'EPC 电子气路控制',
  'Control Mode': '控制模式',
  'Constant Flow': '恒流 (Constant Flow)',
  'Constant Pressure': '恒压 (Constant Pressure)',
  'Ramped Flow': '程序升流 (Ramped Flow)',
  'Ramped Pressure': '程序升压 (Ramped Pressure)',
  'Initial Value': '初始值',
  'Initial Time (min)': '初始保持时间 (min)',
  'Ramp Table': '程序升压/升流表',
  'Add Ramp': '添加阶阶',
  'Rate': '速率',
  'Final Value': '终值',
  'Hold Time (min)': '保持时间 (min)',
  'No ramps configured. Operates isothermally/isobarically.': '未配置程序升压/升流，系统将以恒温/恒压模式运行。',
  'Valve Time Events': '阀门时间事件 (Time Events)',
  'No valve events configured.': '未配置阀门事件。'
};

function insertKeys(langContent, keys) {
  // To avoid duplicate keys, check if key exists
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
  console.log("Successfully injected new EPC translations.");
} else {
  console.log("Could not find translation blocks.");
}