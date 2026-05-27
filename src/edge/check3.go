package main

import (
	"database/sql"
	"fmt"
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

	deviceID := "69000000001ABCDE"

	rows, err := db.Query("SELECT id, created_at, device_id FROM results WHERE device_id = ? AND created_at >= ? AND created_at <= ?", deviceID, from, to)
	if err != nil {
		log.Fatal(err)
	}
	defer rows.Close()

	count := 0
	for rows.Next() {
		var id int
		var t time.Time
		var d string
		if err := rows.Scan(&id, &t, &d); err != nil {
			log.Printf("Scan error: %v", err)
		}
		count++
	}
	fmt.Printf("Total records for %s: %d\n", deviceID, count)
}
