package license

import (
	"crypto"
	"crypto/rsa"
	"crypto/sha256"
	"crypto/x509"
	"encoding/base64"
	"encoding/json"
	"encoding/pem"
	"fmt"
	"os"
	"time"
)

// In a real production environment, you should use //go:embed to embed the key file.
// For this setup, we'll embed the public key directly as a string constant.
const PublicKeyPEM = `-----BEGIN PUBLIC KEY-----
MIIBIjANBgkqhkiG9w0BAQEFAAOCAQ8AMIIBCgKCAQEA6NtVqXbt/MKPw6LHZ3u6
/eBQAf8x5SMMBhkWKVF+Y1DL84gSbpkt6HMExhCrYt2mpMI2uSFZ1W/Jt5ieXwU2
DuBN0gCxAPUWMg3zAE46BEz/ZR5gOySGJNa9X1cGkbWX9czo4gOKnD8wpLANgUOg
YwmcifxDTykxZGKcHor6mJDH5F0ikeQAyu1YvkmP6iawnhSeGAbkjDjwAUyDaqdr
BGH9SRkk8/LZ0DuFc3HnnGAdx+RuV2/xoehVGc/ua8UnQQyA73OiY0EXyzPMxNzc
hpaVCkttZbD6aNBTER99ynQDfG4cR4INudYao3NM/5NJfuXnpJUMlPAy2GrxAqdN
vwIDAQAB
-----END PUBLIC KEY-----`

type LicensePayload struct {
	MachineID string `json:"machine_id"`
	Exp       int64  `json:"exp"` // Unix timestamp, 0 means permanent
	IssuedAt  int64  `json:"issued_at"`
	Tier      string `json:"tier"` // e.g. "standard", "advanced"
	Signature string `json:"signature"` // base64 encoded RSA signature
}

// VerifyLicense checks the license file for authenticity and expiration.
func VerifyLicense(filePath string) (*LicensePayload, error) {
	data, err := os.ReadFile(filePath)
	if err != nil {
		return nil, fmt.Errorf("无法读取授权文件 (未找到 license.lic): %v", err)
	}

	var payload LicensePayload
	if err := json.Unmarshal(data, &payload); err != nil {
		return nil, fmt.Errorf("授权文件格式错误: %v", err)
	}

	// 1. Verify RSA Signature
	raw := fmt.Sprintf("%s|%d|%d|%s", payload.MachineID, payload.Exp, payload.IssuedAt, payload.Tier)
	hash := sha256.Sum256([]byte(raw))

	block, _ := pem.Decode([]byte(PublicKeyPEM))
	if block == nil {
		return nil, fmt.Errorf("内置公钥无效")
	}

	pub, err := x509.ParsePKIXPublicKey(block.Bytes)
	if err != nil {
		return nil, fmt.Errorf("公钥解析失败: %v", err)
	}

	rsaPub, ok := pub.(*rsa.PublicKey)
	if !ok {
		return nil, fmt.Errorf("非RSA公钥")
	}

	sig, err := base64.StdEncoding.DecodeString(payload.Signature)
	if err != nil {
		return nil, fmt.Errorf("签名解码失败")
	}

	if err := rsa.VerifyPKCS1v15(rsaPub, crypto.SHA256, hash[:], sig); err != nil {
		return nil, fmt.Errorf("授权签名校验失败，文件被篡改")
	}

	// 2. Check Machine ID
	if payload.MachineID != GetMachineID() {
		return nil, fmt.Errorf("授权文件不属于本机，机器码不匹配 (当前: %s, 授权: %s)", GetMachineID(), payload.MachineID)
	}

	// 3. Check Expiration
	if payload.Exp > 0 && time.Now().Unix() > payload.Exp {
		return nil, fmt.Errorf("授权已过期 (过期时间: %s)", time.Unix(payload.Exp, 0).Format(time.RFC3339))
	}

	return &payload, nil
}
