const http = require('http');
const fs = require('fs');

let out = "Checking ports...\n";
let active = 2;
function done() { 
  active--; 
  if(active===0) {
    out += "Done\n";
    fs.writeFileSync('ports_result.txt', out);
  }
}

http.get('http://127.0.0.1:8081/', (res) => {
  let data = '';
  res.on('data', (chunk) => data += chunk);
  res.on('end', () => {
    out += '8081 response end. Has 43: ' + data.includes('v0.3.43') + ' Has TCD: ' + data.includes('view-tcd') + '\n';
    let match = data.match(/v0\.3\.\d+/);
    out += '8081 Version match: ' + (match ? match[0] : 'None') + '\n';
    done();
  });
}).on('error', (err) => {
  out += '8081 error: ' + err.message + '\n';
  done();
});

http.get('http://127.0.0.1:8080/', (res) => {
  let data = '';
  res.on('data', (chunk) => data += chunk);
  res.on('end', () => {
    let match = data.match(/v0\.3\.\d+/);
    out += '8080 Version match: ' + (match ? match[0] : 'None') + '\n';
    done();
  });
}).on('error', (err) => {
  out += '8080 error: ' + err.message + '\n';
  done();
});
