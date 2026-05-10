using System;

namespace IBrainChrom2018;

public class PeakGroup : IComparable
{
	public float AllPeakArea;

	public float tanValue;

	public int LfDotNo;

	public int LeftPt;

	public int RtDotNo;

	public int RightPt;

	public Peak[] peak_0;

	public int CompareTo(object target)
	{
		if (target is PeakGroup)
		{
			float value = (target as PeakGroup).tanValue;
			return tanValue.CompareTo(value);
		}
		return 0;
	}

	public Peak FirstPeak()
	{
		if (peak_0.Length == 0)
		{
			return null;
		}
		return peak_0[0];
	}

	public int PeakListLength()
	{
		return peak_0.Length;
	}

	public Peak LastPeak()
	{
		if (peak_0.Length == 0)
		{
			return null;
		}
		return peak_0[peak_0.Length - 1];
	}
}
