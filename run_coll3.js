const { spawn } = require('child_process');
const fs = require('fs');

const child = spawn('.\\collector8081.exe', [], { cwd: 'src/edge/cmd/collector' });

const logStream = fs.createWriteStream('D:\\GIT\\VS2022\\Chromatography-workstation\\collector_log.txt');

child.stdout.on('data', (data) => {
  logStream.write(data);
});

child.stderr.on('data', (data) => {
  logStream.write(data);
});

child.on('close', (code) => {
  logStream.write('\nEXITED WITH CODE ' + code + '\n');
  logStream.end();
});
