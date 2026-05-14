package gckc_test

import (
	"testing"

	"chromatography-workstation/edge/internal/protocol/gckc"
)

func TestEncodeDecodeRoundTrip(t *testing.T) {
	in := gckc.Frame{DeviceID: "DEV0000000000001", Seq: 42, Cmd: 143, Payload: []byte{1, 2, 3, 4, 5}}
	b, err := gckc.Encode(in)
	if err != nil {
		t.Fatal(err)
	}
	out, err := gckc.Decode(b)
	if err != nil {
		t.Fatal(err)
	}
	if out.DeviceID != in.DeviceID || out.Seq != in.Seq || out.Cmd != in.Cmd {
		t.Fatalf("mismatch: %+v vs %+v", out, in)
	}
	if len(out.Payload) != len(in.Payload) {
		t.Fatalf("payload len mismatch")
	}
	for i := range out.Payload {
		if out.Payload[i] != in.Payload[i] {
			t.Fatalf("payload mismatch at %d", i)
		}
	}
}

func TestStreamDecoderSplitChunks(t *testing.T) {
	f1, _ := gckc.Encode(gckc.Frame{DeviceID: "DEV0000000000001", Seq: 1, Cmd: 143, Payload: []byte{9, 9, 9}})
	f2, _ := gckc.Encode(gckc.Frame{DeviceID: "DEV0000000000001", Seq: 2, Cmd: 143, Payload: []byte{8, 8}})
	all := append(append([]byte{}, f1...), f2...)

	dec := &gckc.StreamDecoder{}
	dec.Push(all[:7])
	if _, ok, _ := dec.Next(); ok {
		t.Fatalf("expected no frame")
	}
	dec.Push(all[7 : len(f1)-2])
	if _, ok, _ := dec.Next(); ok {
		t.Fatalf("expected no frame")
	}
	dec.Push(all[len(f1)-2:])
	if fr, ok, _ := dec.Next(); !ok || fr.Seq != 1 {
		t.Fatalf("expected first frame")
	}
	if fr, ok, _ := dec.Next(); !ok || fr.Seq != 2 {
		t.Fatalf("expected second frame")
	}
}
