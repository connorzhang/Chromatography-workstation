using System;
using System.Runtime.InteropServices;

namespace IBrainChrom2018;

[Serializable]
[StructLayout(LayoutKind.Sequential)]
public class ChromComponentRow : IArrayBase
{
	public string name = "";

	public bool JuseTimeCheck;

	public double JStdandPeakTime;

	public double JTimePara;

	public double JPeakAdjustPara;

	public int JModBusAddr;
}
