package main

import (
	"sync"
	"time"

	"github.com/goburrow/modbus"
)

var (
	sharedRTUHandlers   = make(map[string]*modbus.RTUClientHandler)
	sharedRTUHandlersMu sync.Mutex
	sharedPortMutexes   = make(map[string]*SharedPortLock)
)

type SharedPortLock struct {
	mu         sync.Mutex
	lastUnlock time.Time
}

func (s *SharedPortLock) Lock() {
	s.mu.Lock()
	// Ensure at least 50ms gap between consecutive RS485 transactions on the same port
	elapsed := time.Since(s.lastUnlock)
	if elapsed < 50*time.Millisecond {
		time.Sleep(50*time.Millisecond - elapsed)
	}
}

func (s *SharedPortLock) Unlock() {
	s.lastUnlock = time.Now()
	s.mu.Unlock()
}

// getSharedRTUHandler returns a singleton RTUClientHandler and its associated SharedPortLock for a given COM port.
// This allows multiple logical Modbus slaves (like TempController and EPC) to share the same physical COM port.
func getSharedRTUHandler(port string) (*modbus.RTUClientHandler, *SharedPortLock) {
	sharedRTUHandlersMu.Lock()
	defer sharedRTUHandlersMu.Unlock()

	h, ok := sharedRTUHandlers[port]
	if !ok {
		h = modbus.NewRTUClientHandler(port)
		h.BaudRate = 9600
		h.DataBits = 8
		h.Parity = "N"
		h.StopBits = 1
		h.Timeout = 1 * time.Second
		sharedRTUHandlers[port] = h
		sharedPortMutexes[port] = &SharedPortLock{}
	}
	return h, sharedPortMutexes[port]
}
