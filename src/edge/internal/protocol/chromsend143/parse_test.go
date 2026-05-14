package chromsend143_test

import (
	"testing"

	"chromatography-workstation/edge/internal/protocol/chromsend143"
)

func TestParseBasic(t *testing.T) {
	p := make([]byte, 0, 128)
	for i := 0; i < 12; i++ {
		p = append(p, 0)
	}
	for i := 0; i < 6; i++ {
		p = append(p, 0)
	}
	p = append(p, 1)
	p = append(p, 64)
	p = append(p, 0)
	p = append(p, 0)
	p = append(p, 1)
	for i := 0; i < 10; i++ {
		p = append(p, 0, 0, 0, 1)
	}

	out, has, err := chromsend143.Parse(p, chromsend143.Config{ShuaiJian1: 1, ShuaiJian2: 1, ShuaiJian3: 1})
	if err != nil {
		t.Fatal(err)
	}
	if !has {
		t.Fatalf("expected has=true")
	}
	if out.Channel != 0 {
		t.Fatalf("expected channel 0")
	}
	if out.Freq10 != 10 {
		t.Fatalf("expected freq10 10")
	}
	if len(out.Values) != 10 {
		t.Fatalf("expected 10 values")
	}
}
