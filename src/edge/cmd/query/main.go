package main
import (
"database/sql"
"fmt"
_ "modernc.org/sqlite"
)
func main() {
db, err := sql.Open("sqlite", ".run/db/history.sqlite")
if err != nil { panic(err) }
rows, err := db.Query("SELECT trace_id, device_id, created_at, method_id FROM results")
if err != nil { panic(err) }
defer rows.Close()
count := 0
for rows.Next() {
var t, d, c, m string
rows.Scan(&t, &d, &c, &m)
fmt.Printf("%s | %s | %s | %s\n", t, d, c, m)
count++
}
fmt.Printf("Total: %d\n", count)
}
