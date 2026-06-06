const { execSync } = require('child_process');
const fs = require('fs');

try {
  let stdout = execSync('netstat -ano | findstr :8080');
  fs.writeFileSync('netstat_8080.log', stdout);
} catch (err) {
  fs.writeFileSync('netstat_8080.log', err.message);
}
