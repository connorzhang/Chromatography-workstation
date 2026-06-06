const { execSync } = require('child_process');
try {
  const output = execSync('netstat -ano | findstr :8080').toString();
  const lines = output.trim().split('\n');
  for (const line of lines) {
    if (line.trim()) {
      const parts = line.trim().split(/\s+/);
      const pid = parts[parts.length - 1];
      if (pid && pid !== '0') {
        execSync(`taskkill /F /PID ${pid}`);
      }
    }
  }
} catch (e) {}
