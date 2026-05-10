using System;
using System.Runtime.InteropServices;

namespace IBrainChrom2018.ChromFile;

[Serializable]
[StructLayout(LayoutKind.Sequential)]
public class ZhuLuSetting
{
	public bool bColEnable;

	public float fColSetTemp;

	public float fColProTemp;
}
