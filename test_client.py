import asyncio, logging
from asyncua import Client
logging.basicConfig(level=logging.DEBUG)
async def main():
    c = Client('opc.tcp://10.8.5.50:4840')
    await c.connect()
    print('Connected!')
    await asyncio.sleep(5)
    await c.disconnect()
asyncio.run(main())
