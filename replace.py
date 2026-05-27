import re

with open('src/edge/cmd/collector/main.go', 'r', encoding='utf-8') as f:
    content = f.read()

pattern = re.compile(r'mux\.HandleFunc\("/", func\(w http\.ResponseWriter, r \*http\.Request\) \{.*?w\.Write\(\[\]byte\(html\)\)\n\t\}\)', re.DOTALL)

replacement = """mux.Handle("/static/", http.StripPrefix("/static/", http.FileServer(http.FS(staticFS))))
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
})"""

new_content = pattern.sub(replacement, content)

with open('src/edge/cmd/collector/main.go', 'w', encoding='utf-8') as f:
    f.write(new_content)
