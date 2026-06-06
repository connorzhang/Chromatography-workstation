const { execSync } = require('child_process');
const fs = require('fs');

try {
  let stdout = execSync('go build -o collector8081.exe', { cwd: 'D:/GIT/VS2022/Chromatography-workstation/src/edge/cmd/collector' });
  fs.writeFileSync('D:/GIT/VS2022/Chromatography-workstation/build2.log', 'OK\n' + stdout.toString());
} catch (err) {
  fs.writeFileSync('D:/GIT/VS2022/Chromatography-workstation/build2.log', 'ERROR\n' + err.message + '\n' + (err.stdout ? err.stdout.toString() : '') + '\n' + (err.stderr ? err.stderr.toString() : ''));
}
