const { execSync } = require('child_process');
const fs = require('fs');
try {
  const output = execSync('.\\collector8081.exe', { cwd: 'src/edge/cmd/collector' });
  fs.writeFileSync('out_run_coll2.txt', 'SUCCESS:\n' + output.toString());
} catch (e) {
  let result = 'ERROR: ' + e.message + '\n';
  if (e.stdout) result += 'STDOUT:\n' + e.stdout.toString() + '\n';
  if (e.stderr) result += 'STDERR:\n' + e.stderr.toString() + '\n';
  fs.writeFileSync('out_run_coll2.txt', result);
}
