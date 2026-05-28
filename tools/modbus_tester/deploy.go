package main

import (
	"bytes"
	"fmt"
	"io"
	"log"
	"os"
	"strings"

	"github.com/pkg/sftp"
	"golang.org/x/crypto/ssh"
)

func main() {
	host := "172.24.2.23:22"
	user := "root"
	password := "123456"

	config := &ssh.ClientConfig{
		User: user,
		Auth: []ssh.AuthMethod{
			ssh.Password(password),
		},
		HostKeyCallback: ssh.InsecureIgnoreHostKey(),
	}

	fmt.Println("Connecting to", host)
	client, err := ssh.Dial("tcp", host, config)
	if err != nil {
		host = "10.88.88.31:22"
		fmt.Println("Connecting to backup IP", host)
		client, err = ssh.Dial("tcp", host, config)
		if err != nil {
			log.Fatalf("Failed to dial: %s", err)
		}
	}
	defer client.Close()

	// 建立 SFTP 客户端并上传文件
	sftpClient, err := sftp.NewClient(client)
	if err != nil {
		log.Fatalf("Failed to create SFTP client: %s", err)
	}
	defer sftpClient.Close()

	// 获取架构
	arch := strings.TrimSpace(runCmdStr(client, "uname -m"))
	fmt.Println("Edge Device Architecture:", arch)

	localFile := "modbus_tester_amd64"
	if strings.Contains(arch, "aarch64") || strings.Contains(arch, "arm") {
		localFile = "modbus_tester_arm64"
	}

	remotePath := "/root/modbus_tester"
	uploadFile(sftpClient, localFile, remotePath)

	// 给执行权限并运行
	runCmd(client, "chmod +x /root/modbus_tester")

	// 查找 ttyUSB 端口
	fmt.Println("\n--- Checking /dev/ttyUSB* ---")
	runCmd(client, "ls -l /dev/ttyUSB*")

	fmt.Println("\n--- Testing /dev/ttyUSB3 ---")
	runCmd(client, "/root/modbus_tester /dev/ttyUSB3")

	fmt.Println("\n--- Testing /dev/ttyUSB4 ---")
	runCmd(client, "/root/modbus_tester /dev/ttyUSB4")
}

func uploadFile(sftpClient *sftp.Client, localPath, remotePath string) {
	localFile, err := os.Open(localPath)
	if err != nil {
		log.Fatalf("Failed to open local file: %s", err)
	}
	defer localFile.Close()

	remoteFile, err := sftpClient.Create(remotePath)
	if err != nil {
		log.Fatalf("Failed to create remote file: %s", err)
	}
	defer remoteFile.Close()

	bytes, err := io.Copy(remoteFile, localFile)
	if err != nil {
		log.Fatalf("Failed to copy file: %s", err)
	}
	fmt.Printf("Uploaded %d bytes (%s) to %s\n", bytes, localPath, remotePath)
}

func runCmd(client *ssh.Client, cmd string) {
	fmt.Print(runCmdStr(client, cmd))
}

func runCmdStr(client *ssh.Client, cmd string) string {
	session, err := client.NewSession()
	if err != nil {
		log.Fatalf("Failed to create session: %s", err)
	}
	defer session.Close()

	var stdoutBuf bytes.Buffer
	var stderrBuf bytes.Buffer
	session.Stdout = &stdoutBuf
	session.Stderr = &stderrBuf

	err = session.Run(cmd)
	if err != nil {
		// return both err and output
		return stdoutBuf.String() + stderrBuf.String() + fmt.Sprintf("\nCommand failed: %v\n", err)
	}
	return stdoutBuf.String() + stderrBuf.String()
}
