package main

import (
	"context"
	"fmt"
	"log"

	"github.com/gopcua/opcua"
	"github.com/gopcua/opcua/ua"
)

func main() {
	ctx := context.Background()
	c, err := opcua.NewClient("opc.tcp://10.8.5.50:4840", opcua.SecurityMode(ua.MessageSecurityModeNone))
	if err != nil {
		log.Fatal(err)
	}
	if err := c.Connect(ctx); err != nil {
		log.Fatal(err)
	}
	defer c.Close(ctx)

	node := c.Node(ua.NewNumericNodeID(1, 85))
	attrs, err := node.Attributes(ctx, ua.AttributeIDBrowseName, ua.AttributeIDDisplayName, ua.AttributeIDNodeClass)
	if err != nil {
		log.Fatal(err)
	}
	fmt.Printf("Node ns=1;i=85:\n")
	fmt.Printf(" BrowseName: %v\n", attrs[0].Value.Value())
	fmt.Printf(" DisplayName: %v\n", attrs[1].Value.Value())
	fmt.Printf(" NodeClass: %v\n", attrs[2].Value.Value())
}
