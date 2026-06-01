package main

import (
	"fmt"
	"log"
	"os"
	"os/exec"
	"time"

	"github.com/joho/godotenv"
	"github.com/pkg/sftp"
	"golang.org/x/crypto/ssh"
)

func main() {
	_ = godotenv.Load(".env")
	host := os.Getenv("TEST_SCREEN_SSH_HOST")
	if host == "" {
		host = "172.24.2.23"
	}
	user := os.Getenv("TEST_SCREEN_SSH_USER")
	if user == "" {
		user = "root"
	}
	pass := os.Getenv("TEST_SCREEN_SSH_PASSWORD")
	if pass == "" {
		pass = "123456"
	}

	fmt.Println("Compiling monitor for linux/arm64...")
	cmd := exec.Command("go", "build", "-o", "monitor_bin", "./cmd/monitor")
	cmd.Dir = "src/edge"
	cmd.Env = append(os.Environ(), "GOOS=linux", "GOARCH=arm64", "CGO_ENABLED=0")
	out, err := cmd.CombinedOutput()
	if err != nil {
		log.Fatalf("Build failed: %v\n%s", err, string(out))
	}
	defer os.Remove("src/edge/monitor_bin")

	config := &ssh.ClientConfig{
		User:            user,
		Auth:            []ssh.AuthMethod{ssh.Password(pass)},
		HostKeyCallback: ssh.InsecureIgnoreHostKey(),
		Timeout:         5 * time.Second,
	}

	addr := host + ":22"
	fmt.Println("Connecting to " + addr + "...")
	conn, err := ssh.Dial("tcp", addr, config)
	if err != nil {
		log.Printf("SSH connect failed: %v. Trying BAK host...", err)
		hostBak := os.Getenv("TEST_SCREEN_SSH_HOST_BAK")
		if hostBak != "" {
			addr = hostBak + ":22"
			conn, err = ssh.Dial("tcp", addr, config)
			if err != nil {
				log.Fatalf("BAK SSH connect failed: %v", err)
			}
		} else {
			log.Fatalf("SSH connect failed: %v", err)
		}
	}
	defer conn.Close()

	fmt.Println("Killing existing monitor processes on edge...")
	sessionKill, _ := conn.NewSession()
	sessionKill.Run("pkill -f monitor_bin")
	sessionKill.Close()

	fmt.Println("Uploading binary to edge...")
	client, err := sftp.NewClient(conn)
	if err != nil {
		log.Fatalf("SFTP failed: %v", err)
	}
	srcFile, _ := os.Open("src/edge/monitor_bin")
	dstFile, _ := client.Create("/tmp/monitor_bin")
	dstFile.ReadFrom(srcFile)
	srcFile.Close()
	dstFile.Close()
	client.Chmod("/tmp/monitor_bin", 0755)
	client.Close()

	fmt.Println("Executing monitor on edge... (Press Ctrl+C to stop)")
	session, _ := conn.NewSession()
	defer session.Close()

	session.Stdout = os.Stdout
	session.Stderr = os.Stderr

	err = session.Run("/tmp/monitor_bin")
	if err != nil {
		log.Printf("Session run finished: %v", err)
	}
}
