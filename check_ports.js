const http = require('http');
http.get('http://127.0.0.1:8081/', (res) => {
  let data = '';
  res.on('data', (chunk) => data += chunk);
  res.on('end', () => {
    console.log(data.includes('v0.3.43') ? 'Has 43' : 'No 43');
    console.log(data.includes('view-tcd') ? 'Has TCD' : 'No TCD');
    console.log('Version match:', data.match(/v0\.3\.\d+/)?.[0]);
  });
}).on('error', (err) => {
  console.log('8081 error:', err.message);
});

http.get('http://127.0.0.1:8080/', (res) => {
  let data = '';
  res.on('data', (chunk) => data += chunk);
  res.on('end', () => {
    console.log('8080 Version match:', data.match(/v0\.3\.\d+/)?.[0]);
  });
}).on('error', (err) => {
  console.log('8080 error:', err.message);
});
