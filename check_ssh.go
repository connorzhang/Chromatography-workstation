package main

import (
	"bytes"
	"fmt"
	"log"
	"time"

	"golang.org/x/crypto/ssh"
)

func main() {
	config := &ssh.ClientConfig{
		User: "root",
		Auth: []ssh.AuthMethod{
			ssh.Password("123456"),
		},
		HostKeyCallback: ssh.InsecureIgnoreHostKey(),
		Timeout:         5 * time.Second,
	}

	client, err := ssh.Dial("tcp", "172.24.2.23:22", config)
	if err != nil {
		log.Fatalf("Failed to dial: %v", err)
	}
	defer client.Close()

	commands := []string{
		"netstat -tulnp | grep 8080",
		"tail -n 50 /opt/edge-collector/edge.log",
	}

	for _, cmd := range commands {
		fmt.Printf("--- Running: %s ---\n", cmd)
		session, err := client.NewSession()
		if err != nil {
			log.Printf("Failed to create session: %v", err)
			continue
		}

		var stdoutBuf bytes.Buffer
		session.Stdout = &stdoutBuf
		session.Run(cmd)
		fmt.Println(stdoutBuf.String())
		session.Close()
	}
}
