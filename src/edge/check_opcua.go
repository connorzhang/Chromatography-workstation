package main

import (
	"context"
	"fmt"
	"log"

	"github.com/gopcua/opcua"
	"github.com/gopcua/opcua/id"
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

	node := c.Node(ua.NewNumericNodeID(0, id.ObjectsFolder))
	browseRecursive(ctx, c, node, "")
}

func browseRecursive(ctx context.Context, client *opcua.Client, n *opcua.Node, indent string) {
	resp, err := client.Browse(ctx, &ua.BrowseRequest{
		NodesToBrowse: []*ua.BrowseDescription{
			{
				NodeID:          n.ID,
				BrowseDirection: ua.BrowseDirectionForward,
				IncludeSubtypes: true,
				ReferenceTypeID: ua.NewNumericNodeID(0, id.HierarchicalReferences),
				ResultMask:      uint32(ua.BrowseResultMaskAll),
			},
		},
	})
	if err != nil || len(resp.Results) == 0 || resp.Results[0].StatusCode != ua.StatusOK {
		fmt.Printf("%s[Error browsing %v]\n", indent, n.ID)
		return
	}

	for _, ref := range resp.Results[0].References {
		fmt.Printf("%s- %s (%s)", indent, ref.BrowseName.Name, ref.NodeClass)
		childNode := client.Node(ref.NodeID.NodeID)
		if ref.NodeClass == ua.NodeClassVariable {
			if val, err := childNode.Value(ctx); err == nil && val != nil {
				fmt.Printf(" = %v", val.Value())
			}
		}
		fmt.Println()
		browseRecursive(ctx, client, childNode, indent+"  ")
	}
}