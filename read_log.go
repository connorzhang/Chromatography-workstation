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
		Timeout:         5 * time.Second,
	}

	client, err := ssh.Dial("tcp", "10.88.88.31:22", config)
	if err != nil {
		fmt.Println("Dial error:", err)
		os.Exit(1)
	}
	defer client.Close()

	session, err := client.NewSession()
	if err != nil {
		fmt.Println("Session error:", err)
		os.Exit(1)
	}
	defer session.Close()

	out, err := session.CombinedOutput("cat /opt/edge-collector/.run/db/hwconfig/GC97002020100110.json")
	if err != nil {
		fmt.Println("Run error:", err)
	}
	fmt.Println(string(out))
}
