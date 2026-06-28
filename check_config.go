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
		"cat /opt/edge-collector/.run/db/kv.json",
		"grep -i 'channel\\|activeCh\\|driver_mode' /opt/edge-collector/edge.log | tail -20",
	}

	for _, cmd := range commands {
		fmt.Printf("--- Running: %s ---\n", cmd)
		session, err := client.NewSession()
		if err != nil {
			log.Printf("Failed to create session: %v", err)
			continue
		}

		var stdoutBuf, stderrBuf bytes.Buffer
		session.Stdout = &stdoutBuf
		session.Stderr = &stderrBuf
		err = session.Run(cmd)
		fmt.Println("STDOUT:", stdoutBuf.String())
		if err != nil {
			fmt.Println("STDERR:", stderrBuf.String())
			fmt.Println("Error:", err)
		}
		session.Close()
	}
}