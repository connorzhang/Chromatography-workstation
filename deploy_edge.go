package main

import (
	"fmt"
	"log"
	"os"
	"time"

	"github.com/pkg/sftp"
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

	conn, err := ssh.Dial("tcp", "172.24.2.23:22", config)
	if err != nil {
		log.Fatalf("Failed to dial: %v", err)
	}
	defer conn.Close()

	fmt.Println("Killing existing process...")
	session1, _ := conn.NewSession()
	session1.Run("killall collector-linux-arm64")
	session1.Close()

	time.Sleep(1 * time.Second)

	client, err := sftp.NewClient(conn)
	if err != nil {
		log.Fatalf("Failed to create sftp client: %v", err)
	}
	defer client.Close()

	localFile := "src/edge/build/collector-linux-arm64"
	remoteFile := "/opt/edge-collector/collector-linux-arm64"

	srcFile, err := os.Open(localFile)
	if err != nil {
		log.Fatalf("Failed to open local file: %v", err)
	}
	defer srcFile.Close()

	dstFile, err := client.Create(remoteFile)
	if err != nil {
		log.Fatalf("Failed to create remote file: %v", err)
	}

	fmt.Println("Uploading new binary...")
	bytes, err := dstFile.ReadFrom(srcFile)
	if err != nil {
		log.Fatalf("Failed to upload: %v", err)
	}
	dstFile.Close()
	fmt.Printf("Uploaded %d bytes successfully.\n", bytes)

	client.Chmod(remoteFile, 0755)

	fmt.Println("Starting new process...")
	session2, _ := conn.NewSession()
	err = session2.Start("bash -lc 'cd /opt/edge-collector && nohup ./collector-linux-arm64 > edge.log 2>&1 < /dev/null &'")
	if err != nil {
		log.Printf("Start command returned: %v", err)
	}
	// Give it a brief moment to detach before closing session
	time.Sleep(1 * time.Second)
	session2.Close()
	fmt.Println("Upgrade to test screen finished!")
}