const { execSync } = require('child_process');
try {
  const output = execSync('netstat -ano | findstr :8080').toString();
  const lines = output.trim().split('\n');
  for (const line of lines) {
    if (line.includes('LISTENING')) {
      const parts = line.trim().split(/\s+/);
      const pid = parts[parts.length - 1];
      console.log('PID:', pid);
      const wmic = execSync(`wmic process where processid=${pid} get ExecutablePath`).toString();
      console.log(wmic);
    }
  }
} catch (e) {
  console.error(e.message);
}
