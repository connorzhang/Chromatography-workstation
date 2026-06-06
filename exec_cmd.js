const { execSync } = require('child_process');
const fs = require('fs');

try {
  let stdout = execSync('go build -o collector.exe', { cwd: 'src/edge/cmd/collector' });
  fs.writeFileSync('build.log', stdout);
  console.log('Build OK');
} catch (err) {
  fs.writeFileSync('build.log', err.stdout + '\n' + err.stderr);
  console.log('Build Failed');
}

try {
  let stdout = execSync('collector.exe', { cwd: 'src/edge/cmd/collector', timeout: 5000 });
  fs.writeFileSync('run.log', stdout);
  console.log('Run OK');
} catch (err) {
  fs.writeFileSync('run.log', err.stdout + '\n' + err.stderr);
  console.log('Run Failed/Timeout');
}
