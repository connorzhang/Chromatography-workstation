const fs = require('fs');
const stat = fs.statSync('src/edge/cmd/collector/collector.exe');
console.log("Collector mtime:", stat.mtime);
