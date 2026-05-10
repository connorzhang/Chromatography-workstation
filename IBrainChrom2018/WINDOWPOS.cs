using System;

namespace IBrainChrom2018;

public struct WINDOWPOS
{
	public IntPtr hwnd;

	public IntPtr hwndAfter;

	public int x;

	public int y;

	public int cx;

	public int cy;

	public uint flags;

	public override string ToString()
	{
		object[] obj = new object[9] { x, ":", y, ":", cx, ":", cy, ":", null };
		SWP_Flags sWP_Flags = (SWP_Flags)flags;
		obj[8] = sWP_Flags.ToString();
		return string.Concat(obj);
	}
}
