const { execSync } = require('child_process');
try {
  const output = execSync('tasklist').toString();
  const lines = output.trim().split('\n');
  for (const line of lines) {
    if (line.toLowerCase().includes('collector')) {
      const parts = line.trim().split(/\s+/);
      const pid = parts[1];
      if (pid && pid !== '0') {
        console.log('Killing PID', pid);
        execSync(`taskkill /F /PID ${pid}`);
      }
    }
  }
} catch (e) {
  console.error(e.message);
}
