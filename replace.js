const fs = require('fs');
const path = 'src/edge/cmd/collector/main.go';
let content = fs.readFileSync(path, 'utf8');

const startStr = 'mux.HandleFunc("/", func(w http.ResponseWriter, r *http.Request) {';
const endStr = 'w.Write([]byte(html))\n\t})';

const startIndex = content.lastIndexOf(startStr);
const endIndex = content.lastIndexOf(endStr) + endStr.length;

if (startIndex === -1 || endIndex === -1) {
    console.error("Pattern not found");
    process.exit(1);
}

const replacement = \mux.Handle("/static/", http.StripPrefix("/static/", http.FileServer(http.FS(staticFS))))
mux.HandleFunc("/", func(w http.ResponseWriter, r *http.Request) {
if r.URL.Path != "/" {
http.NotFound(w, r)
return
}
content, err := staticFS.ReadFile("static/index.html")
if err != nil {
http.Error(w, "index.html not found", http.StatusInternalServerError)
return
}
w.Header().Set("Content-Type", "text/html; charset=utf-8")
w.Write(content)
})\;

content = content.substring(0, startIndex) + replacement + content.substring(endIndex);
fs.writeFileSync(path, content, 'utf8');
console.log("Replace OK");
