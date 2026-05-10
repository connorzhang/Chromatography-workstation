using System;
using System.Runtime.InteropServices;

namespace IBrainChrom2018.ChromFile;

[Serializable]
[StructLayout(LayoutKind.Sequential)]
public class JinYangParam
{
	public int idx;

	public float fVersion;

	public float fCount;

	public int iTime;

	public int iInterval;
}
