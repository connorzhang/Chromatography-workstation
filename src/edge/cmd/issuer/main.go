package main

import (
	"crypto/ecdsa"
	"crypto/rand"
	"crypto/sha256"
	"crypto/x509"
	"encoding/base64"
	"encoding/binary"
	"encoding/hex"
	"encoding/pem"
	"flag"
	"fmt"
	"os"
	"strings"
	"time"
)

func main() {
	machineID := flag.String("m", "", "Machine ID of the client (required)")
	days := flag.Int("d", 365, "Valid days (0 for permanent)")
	tier := flag.String("t", "advanced", "License tier (e.g. standard, advanced)")
	keyPath := flag.String("k", "docs/keys/license_private.pem", "Path to ECC private key")
	flag.Parse()

	if *machineID == "" {
		fmt.Println("Error: Machine ID is required. Use -m <id>")
		os.Exit(1)
	}

	// 1. Load private key
	keyData, err := os.ReadFile(*keyPath)
	if err != nil {
		fmt.Printf("Failed to read private key from %s: %v\n", *keyPath, err)
		os.Exit(1)
	}

	block, _ := pem.Decode(keyData)
	if block == nil {
		fmt.Println("Failed to parse PEM block")
		os.Exit(1)
	}

	privKey, err := x509.ParseECPrivateKey(block.Bytes)
	if err != nil {
		fmt.Printf("Failed to parse private key: %v\n", err)
		os.Exit(1)
	}

	// 2. Prepare payload
	macStr := strings.ReplaceAll(*machineID, "-", "")
	macBytes, err := hex.DecodeString(macStr)
	if err != nil || len(macBytes) != 8 {
		fmt.Println("Error: Invalid Machine ID format. Must be XXXX-XXXX-XXXX-XXXX")
		os.Exit(1)
	}

	var exp uint32 = 0
	if *days > 0 {
		exp = uint32(time.Now().AddDate(0, 0, *days).Unix())
	}

	payload := make([]byte, 13)
	copy(payload[0:8], macBytes)
	binary.BigEndian.PutUint32(payload[8:12], exp)
	if *tier == "advanced" {
		payload[12] = 1
	} else {
		payload[12] = 0
	}

	// 3. Sign
	hash := sha256.Sum256(payload)
	r, s, err := ecdsa.Sign(rand.Reader, privKey, hash[:])
	if err != nil {
		fmt.Printf("Failed to sign: %v\n", err)
		os.Exit(1)
	}

	// Enforce 32 bytes for P-256 r and s
	rBytes := make([]byte, 32)
	r.FillBytes(rBytes)
	sBytes := make([]byte, 32)
	s.FillBytes(sBytes)

	// 4. Construct final 77-byte data
	finalData := append(payload, rBytes...)
	finalData = append(finalData, sBytes...)

	// 5. Encode
	code := base64.RawURLEncoding.EncodeToString(finalData)

	fmt.Println("============================================")
	fmt.Printf("Machine ID: %s\n", *machineID)
	if exp == 0 {
		fmt.Println("Expiration: Permanent")
	} else {
		fmt.Printf("Expiration: %s\n", time.Unix(int64(exp), 0).Format("2006-01-02 15:04:05"))
	}
	fmt.Printf("Tier      : %s\n", *tier)
	fmt.Println("============================================")
	fmt.Println("ACTIVATION CODE (Copy and paste this into the software):")
	fmt.Println()
	fmt.Println(code)
	fmt.Println()
	fmt.Println("============================================")
}
