using System;
using System.Runtime.InteropServices;

namespace IBrainChrom2018.ChromFile;

[Serializable]
[StructLayout(LayoutKind.Sequential)]
public class ChengShengParam
{
	public float fTempSpeed;

	public float fTempKeeped;

	public float fTempKeededTime;
}
