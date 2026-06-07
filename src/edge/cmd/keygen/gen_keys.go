package main

import (
	"crypto/ecdsa"
	"crypto/elliptic"
	"crypto/rand"
	"crypto/x509"
	"encoding/pem"
	"fmt"
	"os"
)

func main() {
	// Generate ECDSA P-256 key
	privateKey, err := ecdsa.GenerateKey(elliptic.P256(), rand.Reader)
	if err != nil {
		panic(err)
	}

	// Export private key
	privateKeyBytes, err := x509.MarshalECPrivateKey(privateKey)
	if err != nil {
		panic(err)
	}
	privateKeyPEM := pem.EncodeToMemory(&pem.Block{
		Type:  "EC PRIVATE KEY",
		Bytes: privateKeyBytes,
	})
	os.WriteFile("../../docs/keys/license_private.pem", privateKeyPEM, 0600)

	// Export public key
	publicKeyBytes, err := x509.MarshalPKIXPublicKey(&privateKey.PublicKey)
	if err != nil {
		panic(err)
	}
	publicKeyPEM := pem.EncodeToMemory(&pem.Block{
		Type:  "PUBLIC KEY",
		Bytes: publicKeyBytes,
	})
	os.WriteFile("../../docs/keys/license_public.pem", publicKeyPEM, 0644)

	fmt.Println("ECC P-256 keys generated in docs/keys/")
}
