package main

import (
	"crypto"
	"crypto/rand"
	"crypto/rsa"
	"crypto/sha256"
	"crypto/x509"
	"encoding/base64"
	"encoding/json"
	"encoding/pem"
	"flag"
	"fmt"
	"os"
	"time"
)

type LicensePayload struct {
	MachineID string `json:"machine_id"`
	Exp       int64  `json:"exp"`
	IssuedAt  int64  `json:"issued_at"`
	Tier      string `json:"tier"`
	Signature string `json:"signature"`
}

func main() {
	machineID := flag.String("m", "", "Machine ID of the client (required)")
	days := flag.Int("d", 365, "Valid days (0 for permanent)")
	tier := flag.String("t", "advanced", "License tier (e.g. standard, advanced)")
	keyPath := flag.String("k", "../../docs/keys/license_private.pem", "Path to RSA private key")
	outPath := flag.String("o", "license.lic", "Output license file path")
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

	privKey, err := x509.ParsePKCS1PrivateKey(block.Bytes)
	if err != nil {
		fmt.Printf("Failed to parse private key: %v\n", err)
		os.Exit(1)
	}

	// 2. Prepare payload
	issuedAt := time.Now().Unix()
	var exp int64 = 0
	if *days > 0 {
		exp = time.Now().AddDate(0, 0, *days).Unix()
	}

	raw := fmt.Sprintf("%s|%d|%d|%s", *machineID, exp, issuedAt, *tier)
	hash := sha256.Sum256([]byte(raw))

	// 3. Sign
	sig, err := rsa.SignPKCS1v15(rand.Reader, privKey, crypto.SHA256, hash[:])
	if err != nil {
		fmt.Printf("Failed to sign: %v\n", err)
		os.Exit(1)
	}

	payload := LicensePayload{
		MachineID: *machineID,
		Exp:       exp,
		IssuedAt:  issuedAt,
		Tier:      *tier,
		Signature: base64.StdEncoding.EncodeToString(sig),
	}

	// 4. Save
	outData, _ := json.MarshalIndent(payload, "", "  ")
	if err := os.WriteFile(*outPath, outData, 0644); err != nil {
		fmt.Printf("Failed to write license file: %v\n", err)
		os.Exit(1)
	}

	fmt.Printf("License generated successfully at %s\n", *outPath)
	fmt.Printf("Machine ID: %s\n", *machineID)
	if exp == 0 {
		fmt.Println("Expiration: Permanent")
	} else {
		fmt.Printf("Expiration: %s\n", time.Unix(exp, 0).Format(time.RFC3339))
	}
}
