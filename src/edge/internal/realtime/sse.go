package realtime

import (
	"context"
	"encoding/json"
	"net/http"
	"sync"
	"time"
)

type Hub struct {
	mu      sync.RWMutex
	clients map[*client]struct{}
}

type client struct {
	ctx    context.Context
	w      http.ResponseWriter
	filter string
	ch     chan []byte
}

func NewHub() *Hub {
	return &Hub{clients: map[*client]struct{}{}}
}

func (h *Hub) ServeHTTP(w http.ResponseWriter, r *http.Request) {
	flusher, ok := w.(http.Flusher)
	if !ok {
		http.Error(w, "streaming unsupported", http.StatusInternalServerError)
		return
	}
	w.Header().Set("Content-Type", "text/event-stream")
	w.Header().Set("Cache-Control", "no-cache")
	w.Header().Set("Connection", "keep-alive")

	ctx := r.Context()
	c := &client{
		ctx:    ctx,
		w:      w,
		filter: r.URL.Query().Get("deviceId"),
		ch:     make(chan []byte, 256), // Buffered channel to prevent blocking
	}
	h.add(c)
	defer h.remove(c)

	ticker := time.NewTicker(15 * time.Second)
	defer ticker.Stop()

	writeLine(w, ": connected\n\n")
	flusher.Flush()

	for {
		select {
		case <-ctx.Done():
			return
		case <-ticker.C:
			writeLine(w, ": ping\n\n")
			flusher.Flush()
		case msg := <-c.ch:
			writeLine(w, "data: ")
			w.Write(msg)
			writeLine(w, "\n\n")
			flusher.Flush()
		}
	}
}

func (h *Hub) Publish(deviceID string, v any) {
	b, err := json.Marshal(v)
	if err != nil {
		return
	}

	h.mu.RLock()
	defer h.mu.RUnlock()
	for c := range h.clients {
		if c.filter != "" && c.filter != deviceID {
			continue
		}
		select {
		case c.ch <- b:
		default:
			// Drop message if client is too slow to prevent global deadlock
		}
	}
}

func (h *Hub) add(c *client) {
	h.mu.Lock()
	defer h.mu.Unlock()
	h.clients[c] = struct{}{}
}

func (h *Hub) remove(c *client) {
	h.mu.Lock()
	defer h.mu.Unlock()
	delete(h.clients, c)
}

func writeLine(w http.ResponseWriter, s string) {
	_, _ = w.Write([]byte(s))
}
