using System;
using System.Drawing;

namespace IBrainChrom2018;

public class PeakGroupMaker
{
	private ApplyIntegs applyIntegs_0;

	public PeakGroup[] peakGroupList;

	private int iPeakGroup;

	private LR lr_0;

	private Peak peak_0;

	private Peak[] peak_1 = new Peak[0];

	public bool MakePeakGroupList(LR lr_1, PointF[] pointF_0, ref PeakGroup class39_1)
	{
		if (peakGroupList.Length == 0)
		{
			Array.Resize(ref peakGroupList, peak_1.Length);
			for (int i = 0; i < peakGroupList.Length; i++)
			{
				peakGroupList[i] = new PeakGroup();
				int num = ((lr_1 == LR.Left) ? (peak_1.Length - i) : (i + 1));
				peakGroupList[i].peak_0 = new Peak[num];
				for (int j = 0; j < num; j++)
				{
					peakGroupList[i].peak_0[j] = ((lr_1 == LR.Left) ? peak_1[i + j] : peak_1[i - j]);
				}
				Array.Sort(peakGroupList[i].peak_0);
				int num2 = (peakGroupList[i].LfDotNo = peakGroupList[i].FirstPeak().bsLfV.dotNo);
				int num3 = (peakGroupList[i].RtDotNo = peakGroupList[i].LastPeak().bsRtV.dotNo);
				if ((lr_1 == LR.Left && pointF_0[num2].Y >= pointF_0[num3].Y) || (lr_1 == LR.Right && pointF_0[num2].Y <= pointF_0[num3].Y))
				{
					peakGroupList[i].tanValue = -1f;
				}
				else if (num == 1)
				{
					Peak peak = peakGroupList[i].peak_0[0];
					peakGroupList[i].LeftPt = peak.bsLfV.N;
					peakGroupList[i].RightPt = peak.bsRtV.N;
					peakGroupList[i].tanValue = Math.Abs(Convert.ToSingle(peak.tanValue));
				}
				else
				{
					int num4 = -1;
					float num5 = float.MinValue;
					for (int j = 0; j < num; j++)
					{
						int pkN = peakGroupList[i].peak_0[j].pkN;
						float y = pointF_0[pkN].Y;
						if (y > num5)
						{
							num5 = y;
							num4 = pkN;
						}
					}
					peakGroupList[i].LeftPt = num4 - 1;
					peakGroupList[i].RightPt = num4 + 1;
					GetApplyIntegs().FindVale_MaxSlopPoint(isPositive: true, ref peakGroupList[i].LeftPt, ref peakGroupList[i].RightPt, num2, num3);
					if (peakGroupList.Length <= i)
					{
						break;
					}
					if (peakGroupList[i].LeftPt < peakGroupList[i].FirstPeak().pkN && peakGroupList[i].RightPt > peakGroupList[i].LastPeak().pkN)
					{
						double num6 = pointF_0[peakGroupList[i].RightPt].Y - pointF_0[peakGroupList[i].LeftPt].Y;
						double num7 = pointF_0[peakGroupList[i].RightPt].X - pointF_0[peakGroupList[i].LeftPt].X;
						peakGroupList[i].tanValue = Math.Abs(Convert.ToSingle(num6 / num7));
					}
					else
					{
						peakGroupList[i].tanValue = -1f;
					}
				}
				peakGroupList[i].AllPeakArea = -1f;
			}
			Array.Sort(peakGroupList);
			Array.Reverse(peakGroupList);
			iPeakGroup = 0;
		}
		bool flag;
		if (flag = iPeakGroup < peakGroupList.Length)
		{
			class39_1 = peakGroupList[iPeakGroup++];
		}
		return flag && class39_1.tanValue > 0f;
	}

	public void Init(LR lr_1, Peak[] peak_2, Peak peak_3)
	{
		lr_0 = lr_1;
		Array.Resize(ref peak_1, peak_2.Length);
		for (int i = 0; i < peak_1.Length; i++)
		{
			peak_1[i] = peak_2[i];
		}
		Array.Sort(peak_1);
		Array.Resize(ref peakGroupList, 0);
		SetCurrentPeak(peak_3);
	}

	public ApplyIntegs GetApplyIntegs()
	{
		return applyIntegs_0;
	}

	public void SetApplyIntegs(ApplyIntegs applyIntegs_1)
	{
		applyIntegs_0 = applyIntegs_1;
	}

	public Peak GetCurrentPeak()
	{
		return peak_0;
	}

	public void SetCurrentPeak(Peak peak_2)
	{
		peak_0 = peak_2;
	}

	public Peak FirstPeak()
	{
		if (peak_1.Length == 0)
		{
			return null;
		}
		return peak_1[0];
	}

	public Peak LastPeak()
	{
		if (peak_1.Length == 0)
		{
			return null;
		}
		return peak_1[peak_1.Length - 1];
	}
}
