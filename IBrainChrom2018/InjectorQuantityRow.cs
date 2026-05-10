using System;
using System.Runtime.InteropServices;

namespace IBrainChrom2018;

[Serializable]
[StructLayout(LayoutKind.Sequential)]
public class InjectorQuantityRow : IArrayBase
{
	public int startBotNo;

	public int endBotNo;

	public float fQuantity;

	public int iTime;

	public int iInterval;

	public byte[] byte_0 = new byte[6];
}
