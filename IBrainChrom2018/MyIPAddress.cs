using System;
using System.Runtime.InteropServices;

namespace IBrainChrom2018;

[Serializable]
[StructLayout(LayoutKind.Sequential)]
public class MyIPAddress : IArrayBase
{
	public string strIP = "127.0.0.1";

	public MyIPAddress()
	{
	}

	public MyIPAddress(string strip)
	{
		strIP = strip;
	}

	public override string ToString()
	{
		return strIP;
	}
}
