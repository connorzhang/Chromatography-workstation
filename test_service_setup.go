package main

import (
	"fmt"
	"log"
	"os"
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

	fmt.Println("Uploading systemd service file...")
	client, err := sftp.NewClient(conn)
	if err != nil {
		log.Fatalf("SFTP failed: %v", err)
	}
	srcFile, _ := os.Open("src/edge/scripts/edge-collector.service")
	dstFile, _ := client.Create("/etc/systemd/system/edge-collector.service")
	dstFile.ReadFrom(srcFile)
	srcFile.Close()
	dstFile.Close()
	client.Close()

	fmt.Println("Configuring and starting systemd service...")
	session, _ := conn.NewSession()
	defer session.Close()

	// Kill the old nohup process first
	session.Run("fuser -k 8080/tcp; fuser -k 25001/tcp; pkill -f collector-linux-arm64")

	session2, _ := conn.NewSession()
	defer session2.Close()

	// Reload daemon, enable start on boot, and start the service
	cmd := "systemctl daemon-reload && systemctl enable edge-collector.service && systemctl restart edge-collector.service && systemctl status edge-collector.service --no-pager"
	out, err := session2.CombinedOutput(cmd)
	fmt.Printf("\n--- Service Status ---\n%s\n-------------------\n", string(out))
	if err != nil {
		log.Printf("Service config failed: %v", err)
	} else {
		fmt.Println("\n[SUCCESS] Edge Collector is now running as a background service and will start automatically on reboot!")
	}
}
