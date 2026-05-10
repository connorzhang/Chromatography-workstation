using System;
using System.Runtime.InteropServices;

namespace IBrainChrom2018;

[Serializable]
[StructLayout(LayoutKind.Sequential)]
public class timeProgram : IArrayBase
{
	public double TimeValue;

	public int TestCard;

	public timeProgram()
	{
		TimeValue = 0.0;
		TestCard = 0;
	}
}
