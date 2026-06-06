const { execSync } = require('child_process');
const ports = [8080, 8000, 25001, 1502, 50051, 4840];
for (const port of ports) {
  try {
    const output = execSync(`netstat -ano | findstr :${port}`).toString();
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
}
