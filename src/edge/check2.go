package main

import (
	"database/sql"
	"log"
	"time"

	_ "modernc.org/sqlite"
)

func main() {
	db, err := sql.Open("sqlite", ".run/db/history.sqlite")
	if err != nil {
		log.Fatal(err)
	}
	defer db.Close()

	from := time.Now().Add(-24 * time.Hour).UTC()
	to := time.Now().UTC()

	rows, err := db.Query("SELECT id, created_at, device_id FROM results WHERE created_at >= ? AND created_at <= ?", from, to)
	if err != nil {
		log.Fatal(err)
	}
	defer rows.Close()

	for rows.Next() {
		var id int
		var t time.Time
		var d string
		if err := rows.Scan(&id, &t, &d); err != nil {
			log.Printf("Scan error: %v", err)
		} else {
            log.Printf("Scanned successfully")
        }
        break
	}
}
