import time
from sila2.client import SilaClient

print("Connecting to SiLA 2 Server at 172.24.2.23:50051...")
try:
    client = SilaClient("172.24.2.23", 50051, insecure=True)
    print(f"✅ Successfully connected!")
    print("ServerName:", client.SiLAService.ServerName.get())
    print("ImplementedFeatures:", client.SiLAService.ImplementedFeatures.get())
    
    print("\n--- Testing ChromatographService ---")
    state = client.ChromatographService.GetState()
    print("State:", state)

except Exception as e:
    print(f"❌ Failed to connect to SiLA 2 Server: {e}")
