package main

import (
	"bytes"
	"fmt"
	"log"
	"os"
	"time"

	"github.com/joho/godotenv"
	"golang.org/x/crypto/ssh"
)

func main() {
	_ = godotenv.Load()

	sshHost := os.Getenv("TEST_SCREEN_SSH_HOST")
	sshUser := os.Getenv("TEST_SCREEN_SSH_USER")
	sshPass := os.Getenv("TEST_SCREEN_SSH_PASSWORD")
	sshPort := os.Getenv("TEST_SCREEN_SSH_PORT")
	if sshPort == "" {
		sshPort = "22"
	}

	config := &ssh.ClientConfig{
		User: sshUser,
		Auth: []ssh.AuthMethod{
			ssh.Password(sshPass),
		},
		HostKeyCallback: ssh.InsecureIgnoreHostKey(),
		Timeout:         10 * time.Second,
	}

	addr := fmt.Sprintf("%s:%s", sshHost, sshPort)
	client, err := ssh.Dial("tcp", addr, config)
	if err != nil {
		log.Fatalf("Failed to dial: %v", err)
	}
	defer client.Close()

	session, err := client.NewSession()
	if err != nil {
		log.Fatalf("Failed to create session: %v", err)
	}
	defer session.Close()

	var b bytes.Buffer
	session.Stdout = &b
	session.Stderr = &b

	// Get the last 100 lines of the edge-collector service logs
	cmd := "journalctl -u edge-collector.service -n 100 --no-pager"
	if err := session.Run(cmd); err != nil {
		log.Printf("Failed to run command: %v", err)
	}
	fmt.Println(b.String())
}
