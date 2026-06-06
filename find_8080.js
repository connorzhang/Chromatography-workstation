const { execSync } = require('child_process');
const fs = require('fs');
try {
  const output = execSync('netstat -ano | findstr :8080').toString();
  fs.writeFileSync('8080_pid.txt', output);
} catch (e) {
  fs.writeFileSync('8080_pid.txt', e.message);
}
