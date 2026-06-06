package main
import (
	"os"
	"os/exec"
)
func main() {
	cmd := exec.Command("go", "build", "-o", "c8081.exe", "./src/edge/cmd/collector")
	cmd.Dir = "D:\\GIT\\VS2022\\Chromatography-workstation"
	out, _ := cmd.CombinedOutput()
	os.WriteFile("D:\\GIT\\VS2022\\Chromatography-workstation\\go_build_err.log", out, 0644)
}
