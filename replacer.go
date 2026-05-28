package main
import ("os"; "strings"; "log")
func main() {
	b, err := os.ReadFile("src/edge/cmd/collector/main.go")
	if err != nil { log.Fatal(err) }
	s := strings.ReplaceAll(string(b), "NewLegacyGCKCDriver(st, deviceID)", "getDriver(st, deviceID)")
	os.WriteFile("src/edge/cmd/collector/main.go", []byte(s), 0644)
}