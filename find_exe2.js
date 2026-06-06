const { execSync } = require('child_process');
const fs = require('fs');
try {
  let result = '';
  const output = execSync('netstat -ano | findstr :8080').toString();
  const lines = output.trim().split('\n');
  for (const line of lines) {
    if (line.includes('LISTENING')) {
      const parts = line.trim().split(/\s+/);
      const pid = parts[parts.length - 1];
      result += 'PID: ' + pid + '\n';
      const wmic = execSync(`wmic process where processid=${pid} get ExecutablePath`).toString();
      result += wmic + '\n';
      
      // KILL IT!
      execSync(`taskkill /F /PID ${pid}`);
    }
  }
  fs.writeFileSync('D:\\GIT\\VS2022\\Chromatography-workstation\\exe_result.txt', result);
} catch (e) {
  fs.writeFileSync('D:\\GIT\\VS2022\\Chromatography-workstation\\exe_result.txt', e.message);
}
