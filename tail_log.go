package main

import (
	"fmt"
	"log"
	"os"
	"strings"

	"golang.org/x/crypto/ssh"
)

func main() {
	b, _ := os.ReadFile(".env")
	lines := strings.Split(string(b), "\n")
	pwd := ""
	for _, l := range lines {
		l = strings.TrimSpace(l)
		if strings.HasPrefix(l, "TEST_SCREEN_SSH_PASSWORD=") {
			pwd = strings.TrimSpace(strings.Split(l, "=")[1])
			pwd = strings.Trim(pwd, `"'`)
		}
	}
	config := &ssh.ClientConfig{
		User:            "root",
		Auth:            []ssh.AuthMethod{ssh.Password(pwd)},
		HostKeyCallback: ssh.InsecureIgnoreHostKey(),
	}
	conn, err := ssh.Dial("tcp", "172.24.2.23:22", config)
	if err != nil {
		conn, err = ssh.Dial("tcp", "10.88.88.31:22", config)
		if err != nil {
			log.Fatal(err)
		}
	}
	defer conn.Close()
	session, _ := conn.NewSession()
	out, _ := session.CombinedOutput("tail -n 100 /opt/edge-collector/edge.log")
	fmt.Println(string(out))
}
