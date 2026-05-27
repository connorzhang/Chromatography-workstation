package main

import (
	"database/sql"
	"fmt"
	"log"

	_ "modernc.org/sqlite"
)

func main() {
	db, err := sql.Open("sqlite", ".run/db/history.sqlite")
	if err != nil {
		log.Fatal(err)
	}
	defer db.Close()

	rows, err := db.Query("SELECT id, created_at, device_id FROM results")
	if err != nil {
		log.Fatal(err)
	}
	defer rows.Close()

	count := 0
	for rows.Next() {
		var id int
		var t, d string
		if err := rows.Scan(&id, &t, &d); err != nil {
			log.Fatal(err)
		}
		fmt.Printf("id=%d, created_at=%s, device_id=%s\n", id, t, d)
		count++
	}
	fmt.Printf("Total records: %d\n", count)
}
