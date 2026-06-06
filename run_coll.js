const { execSync } = require('child_process');
try {
  const output = execSync('.\\collector8081.exe', { cwd: 'src/edge/cmd/collector' });
  console.log('SUCCESS:', output.toString());
} catch (e) {
  console.log('ERROR:', e.message);
  if (e.stdout) console.log('STDOUT:', e.stdout.toString());
  if (e.stderr) console.log('STDERR:', e.stderr.toString());
}
