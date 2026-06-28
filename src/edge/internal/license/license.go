package license

import (
	"crypto/ecdsa"
	"crypto/sha256"
	"crypto/x509"
	"encoding/base64"
	"encoding/binary"
	"encoding/hex"
	"encoding/pem"
	"fmt"
	"math/big"
	"os"
	"path/filepath"
	"strings"
	"time"
)

const PublicKeyPEM = `-----BEGIN PUBLIC KEY-----
MFkwEwYHKoZIzj0CAQYIKoZIzj0DAQcDQgAEApzApF7XkOB4Gcfv5VDnbNb94QjZ
OCq+YCeGq3XPe72j5vV2ztWpQQTbbiGQKilcHjcBVWkrT3L4fKYKoFuYzw==
-----END PUBLIC KEY-----`

type LicensePayload struct {
	MachineID string `json:"machine_id"`
	Exp       int64  `json:"exp"`
	Tier      string `json:"tier"`
}

// VerifyCode checks the short activation code
func VerifyCode(code string) (*LicensePayload, error) {
	data, err := base64.RawURLEncoding.DecodeString(strings.TrimSpace(code))
	if err != nil {
		return nil, fmt.Errorf("无效的授权码格式")
	}
	if len(data) != 77 {
		return nil, fmt.Errorf("授权码长度或类型不匹配")
	}

	payloadBytes := data[:13]
	rBytes := data[13:45]
	sBytes := data[45:77]

	// Verify ECC Signature
	hash := sha256.Sum256(payloadBytes)

	block, _ := pem.Decode([]byte(PublicKeyPEM))
	if block == nil {
		return nil, fmt.Errorf("内置公钥无效")
	}

	pub, err := x509.ParsePKIXPublicKey(block.Bytes)
	if err != nil {
		return nil, fmt.Errorf("公钥解析失败: %v", err)
	}

	ecdsaPub, ok := pub.(*ecdsa.PublicKey)
	if !ok {
		return nil, fmt.Errorf("非ECC公钥")
	}

	r := new(big.Int).SetBytes(rBytes)
	s := new(big.Int).SetBytes(sBytes)

	if !ecdsa.Verify(ecdsaPub, hash[:], r, s) {
		return nil, fmt.Errorf("授权签名校验失败，授权码伪造或损坏")
	}

	// Parse payload
	macIDBytes := payloadBytes[0:8]
	macIDHex := strings.ToUpper(hex.EncodeToString(macIDBytes))
	macIDStr := macIDHex[0:4] + "-" + macIDHex[4:8] + "-" + macIDHex[8:12] + "-" + macIDHex[12:16]

	exp := int64(binary.BigEndian.Uint32(payloadBytes[8:12]))
	tierByte := payloadBytes[12]
	tier := "standard"
	if tierByte == 1 {
		tier = "advanced"
	}

	// Check Machine ID
	if macIDStr != GetMachineID() {
		return nil, fmt.Errorf("授权码与本机不匹配 (当前: %s, 授权: %s)", GetMachineID(), macIDStr)
	}

	// Check Expiration
	if exp > 0 && time.Now().Unix() > exp {
		return nil, fmt.Errorf("授权已过期 (过期时间: %s)", time.Unix(exp, 0).Format("2006-01-02"))
	}

	return &LicensePayload{MachineID: macIDStr, Exp: exp, Tier: tier}, nil
}

func SaveCode(dataDir string, code string) error {
	return os.WriteFile(filepath.Join(dataDir, "activation.key"), []byte(strings.TrimSpace(code)), 0644)
}

func LoadAndVerify(dataDir string) (*LicensePayload, error) {
	code, err := os.ReadFile(filepath.Join(dataDir, "activation.key"))
	if err != nil {
		return nil, fmt.Errorf("设备未激活，找不到授权记录")
	}
	return VerifyCode(string(code))
}
