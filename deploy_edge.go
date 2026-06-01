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
	err := godotenv.Load(".env")
	if err != nil {
		log.Println("No .env file found, using defaults")
	}

	sshUser := os.Getenv("TEST_SCREEN_SSH_USER")
	if sshUser == "" {
		sshUser = "root"
	}
	sshPass := os.Getenv("TEST_SCREEN_SSH_PASSWORD")
	if sshPass == "" {
		sshPass = "123456"
	}
	sshHost := os.Getenv("TEST_SCREEN_SSH_HOST")
	if sshHost == "" {
		sshHost = "172.24.2.23"
	}
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
	fmt.Printf("Connecting to %s...\n", addr)
	conn, err := ssh.Dial("tcp", addr, config)
	if err != nil {
		log.Printf("Failed to dial main host: %v. Trying BAK host...", err)
		sshHostBak := os.Getenv("TEST_SCREEN_SSH_HOST_BAK")
		if sshHostBak != "" {
			addr = fmt.Sprintf("%s:%s", sshHostBak, sshPort)
			fmt.Printf("Connecting to %s...\n", addr)
			conn, err = ssh.Dial("tcp", addr, config)
			if err != nil {
				log.Fatalf("Failed to dial BAK host: %v", err)
			}
		} else {
			log.Fatalf("Failed to dial: %v", err)
		}
	}
	defer conn.Close()

	fmt.Println("Stopping systemd service...")
	session1, _ := conn.NewSession()
	session1.Run("systemctl stop edge-collector.service; fuser -k 8080/tcp; fuser -k 25001/tcp; pkill -f collector")
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

	sessionMd5, _ := conn.NewSession()
	out, _ := sessionMd5.CombinedOutput("tail -n 20 /opt/edge-collector/edge.log")
	fmt.Printf("Remote Log:\n%s\n", string(out))
	sessionMd5.Close()

	fmt.Println("Restarting systemd service...")
	session2, _ := conn.NewSession()
	err = session2.Run("systemctl restart edge-collector.service")
	if err != nil {
		log.Printf("Restart service command returned: %v", err)
	}
	session2.Close()
	fmt.Println("Upgrade to test screen finished!")
}
