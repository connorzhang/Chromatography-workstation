const { execSync } = require('child_process');
const fs = require('fs');

try {
  let stdout = execSync('go build -o ../../../../c8081.exe', { cwd: __dirname + '/src/edge/cmd/collector' });
  fs.writeFileSync(__dirname + '/build.log', 'OK\n' + stdout.toString());
} catch (err) {
  fs.writeFileSync(__dirname + '/build.log', 'ERROR\n' + err.message + '\n' + (err.stdout ? err.stdout.toString() : '') + '\n' + (err.stderr ? err.stderr.toString() : ''));
}
