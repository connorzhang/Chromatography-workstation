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

	fmt.Println("Compiling loopback tester for linux/arm64...")
	cmd := exec.Command("go", "build", "-o", "loopback_bin", "./cmd/loopback")
	cmd.Dir = "src/edge"
	cmd.Env = append(os.Environ(), "GOOS=linux", "GOARCH=arm64", "CGO_ENABLED=0")
	out, err := cmd.CombinedOutput()
	if err != nil {
		log.Fatalf("Build failed: %v\n%s", err, string(out))
	}
	defer os.Remove("src/edge/loopback_bin")

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

	fmt.Println("Uploading binary to edge...")
	client, err := sftp.NewClient(conn)
	if err != nil {
		log.Fatalf("SFTP failed: %v", err)
	}
	srcFile, _ := os.Open("src/edge/loopback_bin")
	dstFile, _ := client.Create("/tmp/loopback_bin")
	dstFile.ReadFrom(srcFile)
	srcFile.Close()
	dstFile.Close()
	client.Chmod("/tmp/loopback_bin", 0755)
	client.Close()

	fmt.Println("Executing loopback test on edge...")
	session, _ := conn.NewSession()
	defer session.Close()
	runOut, err := session.CombinedOutput("/tmp/loopback_bin")
	fmt.Printf("\n--- Edge Output ---\n%s\n-------------------\n", string(runOut))
}
