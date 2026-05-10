using System;
using System.Runtime.InteropServices;

namespace IBrainChrom2018.ChromFile;

[Serializable]
[StructLayout(LayoutKind.Sequential)]
public class TempParam
{
	public float fMesureTemp;

	public float fSetTemp;

	public float fProTemp;
}
