const { execSync } = require('child_process');
try {
  let out = execSync('netstat -ano | findstr :8080').toString();
  let lines = out.split('\n');
  for (let line of lines) {
    if (line.includes('LISTENING')) {
      let parts = line.trim().split(/\s+/);
      let pid = parts[parts.length - 1];
      console.log('Killing PID ' + pid);
      execSync('taskkill /F /PID ' + pid);
    }
  }
} catch (err) {
  console.log('Error or no process found');
}
