using System;
using System.Runtime.InteropServices;

namespace IBrainChrom2018;

[Serializable]
[StructLayout(LayoutKind.Sequential)]
public class even : IArrayBase
{
	public double TimeStart;

	public double TimeEnd;
}
