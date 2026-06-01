package main

import (
	"fmt"
	"io"
	"os"
	"time"

	"golang.org/x/crypto/ssh"
)

type ProgressReader struct {
	io.Reader
	Total   int64
	Current int64
}

func (pr *ProgressReader) Read(p []byte) (int, error) {
	n, err := pr.Reader.Read(p)
	pr.Current += int64(n)
	fmt.Printf("\rUploading... %d / %d bytes (%.2f%%)", pr.Current, pr.Total, float64(pr.Current)/float64(pr.Total)*100)
	return n, err
}

func main() {
	config := &ssh.ClientConfig{
		User: "root",
		Auth: []ssh.AuthMethod{
			ssh.Password("123456"),
		},
		HostKeyCallback: ssh.InsecureIgnoreHostKey(),
		Timeout:         5 * time.Second,
	}

	fmt.Println("Connecting to 10.88.88.31...")
	client, err := ssh.Dial("tcp", "10.88.88.31:22", config)
	if err != nil {
		fmt.Println("Dial error:", err)
		os.Exit(1)
	}
	defer client.Close()

	fmt.Println("Connected. Killing old process...")
	s0, _ := client.NewSession()
	s0.Run("killall collector-linux-arm64")
	s0.Close()

	fmt.Println("Starting upload...")
	session, err := client.NewSession()
	if err != nil {
		fmt.Println("Session error:", err)
		os.Exit(1)
	}
	defer session.Close()

	f, err := os.Open("collector_arm64")
	if err != nil {
		fmt.Println("Open error:", err)
		os.Exit(1)
	}
	defer f.Close()

	stat, _ := f.Stat()
	w, err := session.StdinPipe()
	if err != nil {
		fmt.Println("StdinPipe error:", err)
		os.Exit(1)
	}

	err = session.Start("cat > /opt/edge-collector/collector-linux-arm64")
	if err != nil {
		fmt.Println("\nCat error:", err)
		os.Exit(1)
	}

	pr := &ProgressReader{Reader: f, Total: stat.Size()}
	io.Copy(w, pr)
	w.Close()

	session.Wait()
	fmt.Println("\nUpload complete.")

	fmt.Println("Restarting service...")
	session2, _ := client.NewSession()
	err = session2.Start("bash -lc 'cd /opt/edge-collector && chmod +x ./collector-linux-arm64 && nohup ./collector-linux-arm64 > edge.log 2>&1 < /dev/null &'")
	if err != nil {
		fmt.Println("Restart error:", err)
	}
	session2.Close()
	fmt.Println("Deploy finished.")

	fmt.Println("Fetching config...")
	session3, _ := client.NewSession()
	out, _ := session3.CombinedOutput("cat /opt/edge-collector/run/db/sysconfig.json")
	fmt.Println(string(out))
	session3.Close()
}
