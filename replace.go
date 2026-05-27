package main
import (
    "io/ioutil"
    "regexp"
    "fmt"
)
func main() {
    content, err := ioutil.ReadFile("src/edge/cmd/collector/main.go")
    if err != nil { panic(err) }
    
    pattern := regexp.MustCompile("(?s)mux\\.HandleFunc\\(\"/\", func\\(w http\\.ResponseWriter, r \\*http\\.Request\\) \\{.*?w\\.Write\\(\\[\\]byte\\(html\\)\\)\\n\\t\\}\\)")
    
    replacement := mux.Handle("/static/", http.StripPrefix("/static/", http.FileServer(http.FS(staticFS))))
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
})
    
    newContent := pattern.ReplaceAllString(string(content), replacement)
    
    err = ioutil.WriteFile("src/edge/cmd/collector/main.go", []byte(newContent), 0644)
    if err != nil { panic(err) }
    fmt.Println("Replace OK")
}
