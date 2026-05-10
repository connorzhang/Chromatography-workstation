using System;
using System.Runtime.InteropServices;

namespace IBrainChrom2018;

public struct COPYDATASTRUCT
{
	public IntPtr dwData;

	public int cbData;

	[MarshalAs(UnmanagedType.LPStr)]
	public string lpData;
}
