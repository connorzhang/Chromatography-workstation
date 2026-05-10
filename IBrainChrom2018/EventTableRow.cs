using System;
using System.Runtime.InteropServices;

namespace IBrainChrom2018;

[Serializable]
[StructLayout(LayoutKind.Sequential)]
public class EventTableRow
{
	public float[] fRowList = new float[8];

	public float this[int index]
	{
		get
		{
			return fRowList[index];
		}
		set
		{
			fRowList[index] = value;
		}
	}
}
