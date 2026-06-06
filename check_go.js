const { execSync } = require('child_process');
const fs = require('fs');

try {
  let stdout = execSync('go env', { cwd: __dirname + '/src/edge/cmd/collector' });
  fs.writeFileSync(__dirname + '/go_env.log', stdout);
} catch (err) {
  fs.writeFileSync(__dirname + '/go_env.log', err.message + '\n' + (err.stdout||'') + '\n' + (err.stderr||''));
}
