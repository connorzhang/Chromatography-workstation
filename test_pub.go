package main

import (
"crypto/x509"
"encoding/pem"
"fmt"
"os"
)

func main() {
keyData, _ := os.ReadFile("N:\\license_private.pem")
block, _ := pem.Decode(keyData)
privKey, _ := x509.ParseECPrivateKey(block.Bytes)
pubBytes, _ := x509.MarshalPKIXPublicKey(&privKey.PublicKey)
pubPEM := pem.EncodeToMemory(&pem.Block{
Type:  "PUBLIC KEY",
Bytes: pubBytes,
})
fmt.Println(string(pubPEM))
}
