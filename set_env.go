package main

import (
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

	cmds := []string{
		"grep -q 'EDGE_HTTP_BIND' /etc/environment || echo 'EDGE_HTTP_BIND=0.0.0.0' >> /etc/environment",
		"grep -q 'EDGE_ALLOW_CONTROL' /etc/environment || echo 'EDGE_ALLOW_CONTROL=true' >> /etc/environment",
		"grep -q 'EDGE_HTTP_BIND' /etc/profile || echo 'export EDGE_HTTP_BIND=0.0.0.0' >> /etc/profile",
		"grep -q 'EDGE_ALLOW_CONTROL' /etc/profile || echo 'export EDGE_ALLOW_CONTROL=true' >> /etc/profile",
	}

	for _, cmd := range cmds {
		session, _ := client.NewSession()
		err := session.Run(cmd)
		if err != nil {
			log.Printf("Failed to run '%s': %v", cmd, err)
		}
		session.Close()
	}
	fmt.Println("Global environment variables configured successfully.")
}
