using System;
using System.Runtime.InteropServices;

namespace IBrainChrom2018;

[Serializable]
[StructLayout(LayoutKind.Sequential)]
public class ChromDeviceInfo
{
	public string ID = "";

	public string Name = "";

	public string DepartMent = "";

	public string Other = "";

	public int ModBusDeviceID;

	public ChromDeviceInfo()
	{
	}

	public ChromDeviceInfo(string id, string name, string departMent, string other, int modbusDeviceID)
	{
		ID = id;
		Name = name;
		DepartMent = departMent;
		Other = other;
		ModBusDeviceID = modbusDeviceID;
	}
}
