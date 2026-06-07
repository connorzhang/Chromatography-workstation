import asyncio
from asyncua import Client

async def browse_recursive(node, indent=""):
    try:
        children = await node.get_children()
        for child in children:
            name = (await child.read_browse_name()).Name
            node_class = await child.read_node_class()
            val = ""
            if str(node_class) == "NodeClass.Variable":
                try:
                    val = f" = {await child.read_value()}"
                except:
                    val = " = [Unreadable]"
            print(f"{indent}- {name} ({node_class}){val}")
            await browse_recursive(child, indent + "  ")
    except Exception as e:
        print(f"{indent}[Error browsing {node}: {e}]")

async def main():
    url = "opc.tcp://127.0.0.1:4840"
    print(f"Connecting to {url} ...")
    try:
        async with Client(url=url) as client:
            print("Connected!")
            objects = client.nodes.objects
            print("Root -> Objects:")
            await browse_recursive(objects, "  ")
    except Exception as e:
        print(f"Failed to connect: {e}")

if __name__ == "__main__":
    asyncio.run(main())