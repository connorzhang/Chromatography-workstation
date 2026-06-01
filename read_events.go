package main

import (
	"bufio"
	"fmt"
	"net/http"
	"os"
)

func main() {
	resp, err := http.Get("http://10.88.88.31:8080/events")
	if err != nil {
		fmt.Println(err)
		os.Exit(1)
	}
	defer resp.Body.Close()

	scanner := bufio.NewScanner(resp.Body)
	count := 0
	for scanner.Scan() {
		line := scanner.Text()
		if line != "" {
			fmt.Println(line)
			count++
			if count >= 10 {
				break
			}
		}
	}
}
