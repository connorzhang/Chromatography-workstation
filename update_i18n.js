const fs = require('fs');
const filePath = 'src/ui/apps/workstation/src/i18n.ts';
let content = fs.readFileSync(filePath, 'utf8');

const newEnKeys = {
  'EMF Limits': 'EMF Limits',
  'EMF Limits...': 'EMF Limits...',
  'EMF Limits Configuration': 'EMF Limits Configuration',
  'Configure Early Maintenance Feedback thresholds. System will generate warnings when limits are reached.': 'Configure Early Maintenance Feedback thresholds. System will generate warnings when limits are reached.',
  'Component': 'Component',
  'Current Value': 'Current Value',
  'Warning Limit': 'Warning Limit',
  'Unit': 'Unit',
  'Apply Limits': 'Apply Limits',
  'Spectra(S)': 'Spectra(S)',
  'Peak Purity': 'Peak Purity',
  'Peak Purity...': 'Peak Purity...',
  'Peak Purity Analysis': 'Peak Purity Analysis',
  'Assess peak homogeneity by comparing spectra across the peak using DAD data.': 'Assess peak homogeneity by comparing spectra across the peak using DAD data.',
  'Purity Parameters': 'Purity Parameters',
  'Calculate Purity Factor': 'Calculate Purity Factor',
  'Threshold Limit': 'Threshold Limit',
  'Reference Spectrum': 'Reference Spectrum',
  'Peak Apex': 'Peak Apex',
  'Peak Start': 'Peak Start',
  'User Defined': 'User Defined',
  'Wavelength Range': 'Wavelength Range',
  'Start': 'Start',
  'End': 'End',
  'Noise Threshold Correction': 'Noise Threshold Correction',
  'Calculate Purity': 'Calculate Purity',
  'Library Search': 'Library Search',
  'Library Search...': 'Library Search...',
  'Spectral Library Search': 'Spectral Library Search',
  'Identify unknown peaks by searching DAD or MS spectra against local/cloud libraries.': 'Identify unknown peaks by searching DAD or MS spectra against local/cloud libraries.',
  'Target Libraries': 'Target Libraries',
  'Add Library...': 'Add Library...',
  'Search Threshold': 'Search Threshold',
  'Max Hits': 'Max Hits',
  'Search Selected Peak': 'Search Selected Peak',
  '3D Plot': '3D Plot',
  '3D Plot...': '3D Plot...',
  '3D Spectral Plot & Isoabsorbance': '3D Spectral Plot & Isoabsorbance',
  'Isoabsorbance Plot': 'Isoabsorbance Plot',
  'Isoabsorbance Plot...': 'Isoabsorbance Plot...',
  'View': 'View',
  '3D Surface Plot': '3D Surface Plot',
  'Isoabsorbance (Contour)': 'Isoabsorbance (Contour)',
  'Time': 'Time',
  'WL': 'WL',
  'Extract Spectrum': 'Extract Spectrum',
  'Extract Chromatogram': 'Extract Chromatogram',
  'Wavelength': 'Wavelength',
  'Compare': 'Compare',
  'Version Comparison...': 'Version Comparison...',
  'Version Comparison (Audit Trail)': 'Version Comparison (Audit Trail)',
  'Compare two versions of a method or dataset to highlight modified parameters, aligning with data integrity guidelines.': 'Compare two versions of a method or dataset to highlight modified parameters, aligning with data integrity guidelines.',
  'Source Version (Older)': 'Source Version (Older)',
  'Target Version (Newer)': 'Target Version (Newer)',
  'Parameter / Setting': 'Parameter / Setting',
  'Source Value': 'Source Value',
  'Target Value': 'Target Value',
  'Print Diff Report': 'Print Diff Report'
};

const newZhKeys = {
  'EMF Limits': 'EMF 预警限值',
  'EMF Limits...': 'EMF 预警限值...',
  'EMF Limits Configuration': 'EMF 预警限值配置',
  'Configure Early Maintenance Feedback thresholds. System will generate warnings when limits are reached.': '配置早期维护反馈(EMF)阈值。达到限值时系统将生成警告。',
  'Component': '组件',
  'Current Value': '当前值',
  'Warning Limit': '警告限值',
  'Unit': '单位',
  'Apply Limits': '应用限值',
  'Spectra(S)': '光谱(S)',
  'Peak Purity': '峰纯度',
  'Peak Purity...': '峰纯度...',
  'Peak Purity Analysis': '峰纯度分析',
  'Assess peak homogeneity by comparing spectra across the peak using DAD data.': '使用DAD数据比较色谱峰不同位置的光谱来评估峰均匀性。',
  'Purity Parameters': '纯度参数',
  'Calculate Purity Factor': '计算纯度因子',
  'Threshold Limit': '阈值限制',
  'Reference Spectrum': '参考光谱',
  'Peak Apex': '峰顶点',
  'Peak Start': '峰起点',
  'User Defined': '用户定义',
  'Wavelength Range': '波长范围',
  'Start': '起始',
  'End': '结束',
  'Noise Threshold Correction': '噪声阈值校正',
  'Calculate Purity': '计算纯度',
  'Library Search': '谱库检索',
  'Library Search...': '谱库检索...',
  'Spectral Library Search': '光谱库检索',
  'Identify unknown peaks by searching DAD or MS spectra against local/cloud libraries.': '通过在本地/云端库中检索DAD或MS光谱来鉴定未知峰。',
  'Target Libraries': '目标谱库',
  'Add Library...': '添加谱库...',
  'Search Threshold': '检索阈值',
  'Max Hits': '最大命中数',
  'Search Selected Peak': '检索选定峰',
  '3D Plot': '3D 绘图',
  '3D Plot...': '3D 绘图...',
  '3D Spectral Plot & Isoabsorbance': '3D 光谱图与等吸收图',
  'Isoabsorbance Plot': '等吸收图',
  'Isoabsorbance Plot...': '等吸收图...',
  'View': '视图',
  '3D Surface Plot': '3D 表面图',
  'Isoabsorbance (Contour)': '等吸收图(等高线)',
  'Time': '时间',
  'WL': '波长',
  'Extract Spectrum': '提取光谱',
  'Extract Chromatogram': '提取色谱图',
  'Wavelength': '波长',
  'Compare': '对比',
  'Version Comparison...': '版本对比...',
  'Version Comparison (Audit Trail)': '版本对比(审计追踪)',
  'Compare two versions of a method or dataset to highlight modified parameters, aligning with data integrity guidelines.': '比较方法或数据集的两个版本以高亮显示修改的参数，符合数据完整性准则。',
  'Source Version (Older)': '源版本(较旧)',
  'Target Version (Newer)': '目标版本(较新)',
  'Parameter / Setting': '参数 / 设置',
  'Source Value': '源值',
  'Target Value': '目标值',
  'Print Diff Report': '打印差异报告'
};

function insertKeys(langContent, keys) {
  let entries = Object.entries(keys).map(([k, v]) => `      "${k}": "${v}",`).join('\n');
  return langContent.replace(/translation:\s*\{/, `translation: {\n${entries}`);
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
  console.log("Successfully injected new translations.");
} else {
  console.log("Could not find translation blocks.");
}
