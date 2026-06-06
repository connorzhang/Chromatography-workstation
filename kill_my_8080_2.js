const { execSync } = require('child_process');
const fs = require('fs');
let log = '';
try {
  let out = execSync('netstat -ano | findstr :8080').toString();
  log += 'netstat:\n' + out + '\n';
  let lines = out.split('\n');
  for (let line of lines) {
    if (line.includes('LISTENING')) {
      let parts = line.trim().split(/\s+/);
      let pid = parts[parts.length - 1];
      log += 'Killing PID ' + pid + '\n';
      try {
        let killOut = execSync('taskkill /F /PID ' + pid).toString();
        log += killOut + '\n';
      } catch (e) {
        log += 'Kill failed: ' + e.message + '\n' + e.stdout + '\n' + e.stderr + '\n';
      }
    }
  }
} catch (err) {
  log += 'Error or no process found: ' + err.message + '\n';
}
fs.writeFileSync('kill_log.txt', log);
