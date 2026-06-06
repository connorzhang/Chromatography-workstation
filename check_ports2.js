const http = require('http');
console.log("Checking ports...");
let active = 2;
function done() { active--; if(active===0) console.log("Done"); }

http.get('http://127.0.0.1:8081/', (res) => {
  let data = '';
  res.on('data', (chunk) => data += chunk);
  res.on('end', () => {
    console.log('8081 response end. Has 43:', data.includes('v0.3.43'), 'Has TCD:', data.includes('view-tcd'));
    let match = data.match(/v0\.3\.\d+/);
    console.log('8081 Version match:', match ? match[0] : 'None');
    done();
  });
}).on('error', (err) => {
  console.log('8081 error:', err.message);
  done();
});

http.get('http://127.0.0.1:8080/', (res) => {
  let data = '';
  res.on('data', (chunk) => data += chunk);
  res.on('end', () => {
    let match = data.match(/v0\.3\.\d+/);
    console.log('8080 Version match:', match ? match[0] : 'None');
    done();
  });
}).on('error', (err) => {
  console.log('8080 error:', err.message);
  done();
});
