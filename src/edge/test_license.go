package main

import (
"fmt"
"chromatography-workstation/edge/internal/license"
)

func main() {
code := "i1_QgXEUpN4AAAAAAaBgTsAnoJtH2pXmXCt9yWAgFa0CDn9wPkWXHOgmiJm4wGoSP8uk1sCYHjh3ApGrATF9Ao3AMLzLJ-Oqfg186j0"

payload, err := license.VerifyCode(code)
if err != nil {
fmt.Println("Error:", err)
} else {
fmt.Printf("Verified! MachineID: %s, Tier: %s\n", payload.MachineID, payload.Tier)
}
}
