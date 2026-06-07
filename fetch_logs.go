package main

import (
	"fmt"
	"os"
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
		Timeout:         10 * time.Second,
	}

	client, err := ssh.Dial("tcp", "10.8.5.50:22", config)
	if err != nil {
		fmt.Printf("SSH connect error: %v\n", err)
		os.Exit(1)
	}
	defer client.Close()

	session, err := client.NewSession()
	if err != nil {
		fmt.Printf("Session error: %v\n", err)
		os.Exit(1)
	}
	defer session.Close()

	out, _ := session.CombinedOutput("cat /opt/edge-collector/collector.log | tail -n 100")
	fmt.Println(string(out))
}
