BIN_NAME        ?= collector
PKG             ?= ./cmd/collector
RELEASE_DIR     ?= ./release
LDFLAGS         ?= -s -w
BUILD_FLAGS     ?= -trimpath -ldflags "$(LDFLAGS)"

.DEFAULT_GOAL := build

clean:
	rm -f $(BIN_NAME)
	rm -rf $(RELEASE_DIR)

build:
	CGO_ENABLED=0 go build $(BUILD_FLAGS) -o $(BIN_NAME) $(PKG)

build-linux:
	mkdir -p $(RELEASE_DIR)
	CGO_ENABLED=0 GOOS=linux GOARCH=amd64 go build $(BUILD_FLAGS) -o $(RELEASE_DIR)/$(BIN_NAME)-linux-amd64 $(PKG)

build-arm64:
	mkdir -p $(RELEASE_DIR)
	CGO_ENABLED=0 GOOS=linux GOARCH=arm64 go build $(BUILD_FLAGS) -o $(RELEASE_DIR)/$(BIN_NAME)-linux-arm64 $(PKG)

build-armv7:
	mkdir -p $(RELEASE_DIR)
	CGO_ENABLED=0 GOOS=linux GOARCH=arm GOARM=7 go build $(BUILD_FLAGS) -o $(RELEASE_DIR)/$(BIN_NAME)-linux-armv7 $(PKG)

build-windows:
	mkdir -p $(RELEASE_DIR)
	CGO_ENABLED=0 GOOS=windows GOARCH=amd64 go build $(BUILD_FLAGS) -o $(RELEASE_DIR)/$(BIN_NAME)-windows-amd64.exe $(PKG)

test:
	go test ./...

.PHONY: clean build build-linux build-arm64 build-armv7 build-windows test
