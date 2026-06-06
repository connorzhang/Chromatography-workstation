const http = require('http');
http.get('http://127.0.0.1:8080/', (res) => {
  console.log('8080 Status:', res.statusCode);
}).on('error', (e) => {
  console.log('8080 Error:', e.message);
});
