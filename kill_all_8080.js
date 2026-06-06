const { execSync } = require('child_process');
try {
  const output = execSync('netstat -ano | findstr :8080').toString();
  const lines = output.trim().split('\n');
  for (const line of lines) {
    if (line.includes('LISTENING')) {
      const parts = line.trim().split(/\s+/);
      const pid = parts[parts.length - 1];
      console.log('Killing PID', pid);
      execSync(`taskkill /F /PID ${pid}`);
    }
  }
} catch (e) {
  console.error(e.message);
}
