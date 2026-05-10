using System;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using IBrainChrom2018.Unit;

namespace IBrainChrom2018;

public class ApplyIntegs
{
	private class PeakDetectResult
	{
		private EnumPeakState state;

		private PointF peakPot;

		private int index;

		public PeakDetectResult(EnumPeakState state, PointF peakPot, int index)
		{
			this.state = state;
			this.peakPot = peakPot;
			this.index = index;
		}

		public override string ToString()
		{
			return index.ToString() + " " + peakPot.X + " " + peakPot.Y + " " + GetPeakStateString(EnumPeakState.Peak);
		}

		private string GetPeakStateString(EnumPeakState peakstate)
		{
			return peakstate switch
			{
				EnumPeakState.Peak => "Peak", 
				EnumPeakState.Vale => "Vale", 
				EnumPeakState.Clear => "Clear", 
				EnumPeakState.None => "None", 
				_ => "None", 
			};
		}
	}

	private const float float_0 = 0.35f;

	private const float float_1 = 1.15f;

	private const int m_nMaxPeakPointCount = 4000;

	private const float float_3 = 0.0001f;

	private FormMainParam frmParam = FormMainParam.Create();

	private PeakGroupMaker peakGroupMaker1 = new PeakGroupMaker();

	private PeakGroupMaker peakGroupMaker2 = new PeakGroupMaker();

	private EnumPeakState[] enumPeakStateList = new EnumPeakState[0];

	private float persentOfLength;

	private float myfThreshold;

	private float myfPeakWidth;

	private float myfMaxThreshold;

	private float myfMaxPeakWidth;

	private int[] iSelectPeakList = new int[0];

	private uint tickCount;

	private PointF[] dots;

	private float[] fDistanceList = new float[0];

	private Peak[] peak_0 = new Peak[0];

	private Integration integration_0;

	private int dotLength => (dots != null) ? dots.Length : 0;

	public Peak[] Peaks => peak_0;

	private int PeaksNum => peak_0.Length;

	public Integration Integs
	{
		get
		{
			return integration_0;
		}
		set
		{
			integration_0 = value;
		}
	}

	[DllImport("kernel32.dll")]
	private static extern uint GetTickCount();

	public void NewApplyIntegs()
	{
		persentOfLength = 0f;
		myfThreshold = 0f;
		myfPeakWidth = 0f;
		tickCount = 0u;
		myfMaxThreshold = 0f;
		myfMaxPeakWidth = 0f;
	}

	public bool IsInit()
	{
		return dots == null;
	}

	public bool Apply(PointF[] dots, PointF[] svDots, float virBsY, PointF[] asDots, bool bool_0)
	{
		GetPeak_pkN_List();
		ApplyIntegs2(dots, svDots, virBsY, asDots, bool_0, out var bool_1);
		if (!bool_1)
		{
			ResetDtecNeg(1, dotLength - 1, EnumDetectPeakMethod.OnlyPeak);
			ProcessAllInteg();
			SetPeakListData();
			SetPeaksSelectState();
		}
		return true;
	}

	public float getAy(float timeA, float timeB)
	{
		int dotNo = getDotNo(timeA);
		int dotNo2 = getDotNo(timeB);
		return GetYDistance_2Point(dotNo, dotNo2);
	}

	public int getDotNo(float minute)
	{
		int int_ = Convert.ToInt32(minute * persentOfLength);
		Class49.SafeValueCheck(ref int_, 0, dotLength - 1);
		float num = Math.Abs(dots[int_].X - minute);
		int result = int_;
		for (int num2 = int_ - 1; num2 >= 0; num2--)
		{
			float num3 = Math.Abs(dots[num2].X - minute);
			if (num3 >= num)
			{
				break;
			}
			num = num3;
			result = num2;
		}
		for (int num2 = int_ + 1; num2 < dotLength; num2++)
		{
			float num3 = Math.Abs(dots[num2].X - minute);
			if (num3 >= num)
			{
				return result;
			}
			num = num3;
			result = num2;
		}
		return result;
	}

	public float getDotY(float minute)
	{
		return dots[getDotNo(minute)].Y;
	}

	public float getHy(float timeA, float timeB)
	{
		int dotNo = getDotNo(timeA);
		int dotNo2 = getDotNo(timeB);
		return GetYDistance_2Point2(dotNo, dotNo2);
	}

	public void getVV(ref VV vv_0, PointF[] dots, int dotNo, float pnY, int hwN)
	{
		getVV(ref vv_0, dots, dots.Length, dotNo, pnY, hwN);
	}

	public void getVVSolv(ref VV vv_0, PointF[] dots, int dotNo, float pnY, int hwN, int dotLength, int startIndex, int endIndex)
	{
		getVVSolv(ref vv_0, dots, dotLength, dotNo + startIndex, pnY, hwN, startIndex, endIndex);
	}

	public void getVVSolv(ref VV vv_0, PointF[] dots, int dotsLength, int dotNo, float pnY, int hwN, int startIndex, int endIndex)
	{
		vv_0.vs_0 = VS.None;
		vv_0.X = dots[dotNo].X;
		float num = (vv_0.Y = dots[dotNo].Y);
		if (!float.IsNaN(pnY) && !(Math.Abs(num - pnY) >= myfThreshold))
		{
			return;
		}
		vv_0.index = dotNo;
		int num2 = 0;
		float num3 = 0f;
		int num4 = dotNo - 1;
		while (num4 >= 0 && num2 < hwN)
		{
			num3 += dots[num4].Y;
			if (enumPeakStateList.Length != 0 && enumPeakStateList[num4] != EnumPeakState.None)
			{
				break;
			}
			num4--;
			num2++;
		}
		if (num2 != 0)
		{
			num3 /= (float)num2;
		}
		float num5 = ((num2 != 0) ? (num3 - num) : 0f);
		float num6 = 0f;
		num2 = 0;
		num4 = dotNo + 1;
		while (num4 < endIndex && num2 < hwN)
		{
			num6 += dots[num4].Y;
			if (enumPeakStateList.Length != 0 && enumPeakStateList[num4] != EnumPeakState.None)
			{
				break;
			}
			num4++;
			num2++;
		}
		if (num2 != 0)
		{
			num6 /= (float)num2;
		}
		float num7 = ((num2 != 0) ? (num6 - num) : 0f);
		if (num5 > 0.0001f)
		{
			if (num7 > 0.0001f)
			{
				vv_0.vs_0 = VS.V;
			}
			else if (-0.0001f <= num7 && num7 <= 0.0001f)
			{
				vv_0.vs_0 = VS.PR;
			}
			else
			{
				vv_0.vs_0 = VS.None;
			}
		}
		else if (-0.0001f <= num5 && num5 <= 0.0001f)
		{
			if (num7 > 0.0001f)
			{
				vv_0.vs_0 = VS.PL;
			}
			else if (-0.0001f <= num7 && num7 <= 0.0001f)
			{
				vv_0.vs_0 = VS.None;
			}
			else
			{
				vv_0.vs_0 = VS.NL;
			}
		}
		else if (num7 > 0.0001f)
		{
			vv_0.vs_0 = VS.None;
		}
		else if (-0.0001f <= num7 && num7 <= 0.0001f)
		{
			vv_0.vs_0 = VS.NR;
		}
		else
		{
			vv_0.vs_0 = VS.A;
		}
		vv_0.value = Math.Abs(num5 + num7);
	}

	public void getVV(ref VV vv_0, PointF[] dots, int dotsLength, int dotNo, float pnY, int hwN)
	{
		vv_0.vs_0 = VS.None;
		vv_0.X = dots[dotNo].X;
		float num = (vv_0.Y = dots[dotNo].Y);
		if (!float.IsNaN(pnY) && !(Math.Abs(num - pnY) >= myfThreshold))
		{
			return;
		}
		vv_0.index = dotNo;
		int num2 = 0;
		float num3 = 0f;
		int num4 = dotNo - 1;
		while (num4 >= 0 && num2 < hwN)
		{
			num3 += dots[num4].Y;
			if (enumPeakStateList.Length != 0 && enumPeakStateList[num4] != EnumPeakState.None)
			{
				break;
			}
			num4--;
			num2++;
		}
		if (num2 != 0)
		{
			num3 /= (float)num2;
		}
		float num5 = ((num2 != 0) ? (num3 - num) : 0f);
		float num6 = 0f;
		num2 = 0;
		num4 = dotNo + 1;
		while (num4 < dotsLength && num2 < hwN)
		{
			num6 += dots[num4].Y;
			if (enumPeakStateList.Length != 0 && enumPeakStateList[num4] != EnumPeakState.None)
			{
				break;
			}
			num4++;
			num2++;
		}
		if (num2 != 0)
		{
			num6 /= (float)num2;
		}
		float num7 = ((num2 != 0) ? (num6 - num) : 0f);
		if (num5 > 0.0001f)
		{
			if (num7 > 0.0001f)
			{
				vv_0.vs_0 = VS.V;
			}
			else if (-0.0001f <= num7 && num7 <= 0.0001f)
			{
				vv_0.vs_0 = VS.PR;
			}
			else
			{
				vv_0.vs_0 = VS.None;
			}
		}
		else if (-0.0001f <= num5 && num5 <= 0.0001f)
		{
			if (num7 > 0.0001f)
			{
				vv_0.vs_0 = VS.PL;
			}
			else if (-0.0001f <= num7 && num7 <= 0.0001f)
			{
				vv_0.vs_0 = VS.None;
			}
			else
			{
				vv_0.vs_0 = VS.NL;
			}
		}
		else if (num7 > 0.0001f)
		{
			vv_0.vs_0 = VS.None;
		}
		else if (-0.0001f <= num7 && num7 <= 0.0001f)
		{
			vv_0.vs_0 = VS.NR;
		}
		else
		{
			vv_0.vs_0 = VS.A;
		}
		vv_0.value = Math.Abs(num5 + num7);
	}

	public float getWx(float timeA, float timeB)
	{
		int dotNo = getDotNo(timeA);
		int dotNo2 = getDotNo(timeB);
		return GetXDistance_2Point2(dotNo, dotNo2);
	}

	public bool IsolatedVale(int dotNo, Peak curPeak)
	{
		if (dotNo != -1)
		{
			if (enumPeakStateList[dotNo] == EnumPeakState.Clear)
			{
				for (int i = 0; i < PeaksNum; i++)
				{
					if (peak_0[i] != curPeak && peak_0[i].VsContain(dotNo))
					{
						return false;
					}
				}
			}
			return true;
		}
		return false;
	}

	private void method_0(Peak[] peak_1, float float_10, float float_11, float float_12, float float_13)
	{
		if (peak_1.Length <= 1 || !peak_1[0].positive)
		{
			return;
		}
		Array.Sort(peak_1);
		float num = -1f;
		Peak peak = null;
		for (int i = 0; i < peak_1.Length; i++)
		{
			if (peak_1[i].area > num)
			{
				num = peak_1[i].area;
				peak = peak_1[i];
			}
		}
		Peak peak_2 = peak;
		Peak peak_3 = peak;
		bool flag = true;
		bool flag2 = true;
		while (flag || flag2)
		{
			flag = method_53(LR.Left, peak_1, peak, ref peak_2, float_10, float_11, float_12, float_13);
			flag2 = method_53(LR.Right, peak_1, peak, ref peak_3, float_10, float_11, float_12, float_13);
		}
		Peak currentPeak;
		peakGroupMaker2.SetCurrentPeak(currentPeak = null);
		peakGroupMaker1.SetCurrentPeak(currentPeak);
		if (peak_2 != peak_1[0])
		{
			Peak[] array = new Peak[0];
			for (int i = 0; i < peak_1.Length && peak_1[i] != peak_2; i++)
			{
				int num2 = array.Length;
				Array.Resize(ref array, num2 + 1);
				array[num2] = peak_1[i];
			}
			method_0(array, float_10, float_11, float_12, float_13);
		}
		if (peak_3 != peak_1[peak_1.Length - 1])
		{
			Peak[] array2 = new Peak[0];
			int i = peak_1.Length - 1;
			while (i >= 0 && peak_1[i] != peak_3)
			{
				int num2 = array2.Length;
				Array.Resize(ref array2, num2 + 1);
				array2[num2] = peak_1[i];
				i--;
			}
		}
	}

	private bool method_1(LR lr_0, Peak peak_1, ref PeakGroup class39_0, ref Peak peak_2, float float_10)
	{
		if (class39_0.AllPeakArea < 0f)
		{
			if (class39_0.PeakListLength() == 1)
			{
				class39_0.AllPeakArea = class39_0.peak_0[0].area;
			}
			else
			{
				class39_0.AllPeakArea = 0f;
				for (int i = 0; i < class39_0.PeakListLength(); i++)
				{
					Peak peak = new Peak();
					peak.CopyBaseInfo(class39_0.peak_0[i], cloneBsline: false);
					peak.bsLfV.dotNo = class39_0.LfDotNo;
					peak.bsLfV.N = class39_0.LeftPt;
					peak.bsRtV.dotNo = class39_0.RtDotNo;
					peak.bsRtV.N = class39_0.RightPt;
					needProcPeak(peak);
					class39_0.AllPeakArea += peak.area;
					class39_0.peak_0[i] = peak;
				}
			}
		}
		if (peak_1.area / class39_0.AllPeakArea < float_10)
		{
			return false;
		}
		if (class39_0.PeakListLength() == 1)
		{
			class39_0.peak_0[0].CloneBsline(FindPeakByPeakIndex(class39_0.peak_0[0]));
		}
		int num = class39_0.RtDotNo - class39_0.LfDotNo;
		float num2 = 0f;
		if (lr_0 == LR.Left)
		{
			int num3 = peak_1.lfTgntYs.Length;
			Array.Resize(ref peak_1.lfTgntYs, num3 + num);
			for (int j = 0; j < num; j++)
			{
				int num4 = num3 + j;
				int num5 = class39_0.RtDotNo - 1 - j;
				num2 = dots[num5].Y;
				if (class39_0.LeftPt < num5 && num5 < class39_0.RightPt)
				{
					for (int i = 0; i < class39_0.PeakListLength() && !class39_0.peak_0[i].BsY(num5, ref num2); i++)
					{
					}
				}
				peak_1.lfTgntYs[num4] = num2;
			}
			peak_1.bsLfV.dotNo = class39_0.LfDotNo;
			peak_2 = FindPeakByPeakIndex(class39_0.FirstPeak());
		}
		if (lr_0 == LR.Right)
		{
			int num3 = peak_1.rtTgntYs.Length;
			Array.Resize(ref peak_1.rtTgntYs, num3 + num);
			for (int j = 0; j < num; j++)
			{
				int num4 = num3 + j;
				int num5 = class39_0.LfDotNo + 1 + j;
				num2 = dots[num5].Y;
				if (class39_0.LeftPt < num5 && num5 < class39_0.RightPt)
				{
					for (int i = 0; i < class39_0.PeakListLength() && !class39_0.peak_0[i].BsY(num5, ref num2); i++)
					{
					}
				}
				peak_1.rtTgntYs[num4] = num2;
			}
			peak_1.bsRtV.dotNo = class39_0.RtDotNo;
			peak_2 = FindPeakByPeakIndex(class39_0.LastPeak());
		}
		needProcPeak(peak_1);
		if (class39_0.PeakListLength() != 1)
		{
			for (int i = 0; i < class39_0.PeakListLength(); i++)
			{
				Peak peak = class39_0.peak_0[i];
				FindPeakByPeakIndex(peak).CopyBaseInfo(peak, cloneBsline: true);
			}
		}
		return true;
	}

	private bool method_11(int startIndex, int endIndex)
	{
		if (endIndex != startIndex)
		{
			int num = -1;
			int num2 = -1;
			int num3 = -1;
			for (int num4 = startIndex; num4 >= 0; num4--)
			{
				if (enumPeakStateList[num4] == EnumPeakState.Clear)
				{
					num2 = num4;
					break;
				}
			}
			for (int num4 = startIndex; num4 < dotLength; num4++)
			{
				if (enumPeakStateList[num4] == EnumPeakState.Clear)
				{
					num3 = num4;
					break;
				}
			}
			num = ((Math.Abs(num2 - startIndex) <= Math.Abs(num3 - startIndex)) ? num2 : num3);
			if (num2 != num3)
			{
				if (num > 0)
				{
					enumPeakStateList[num] = EnumPeakState.None;
				}
			}
			else if (num > 0)
			{
				enumPeakStateList[num] = EnumPeakState.Clear;
			}
			if (endIndex < dotLength)
			{
				enumPeakStateList[endIndex] = EnumPeakState.Clear;
			}
		}
		return true;
	}

	private void AddPointIndex2Array(ref int[] int_2, int int_3, Peak peak_1)
	{
		if (!Class49.ValueInArray(int_2, int_3))
		{
			int num = int_2.Length;
			Array.Resize(ref int_2, num + 1);
			int_2[num] = int_3;
			float num2 = dots[int_3].Y;
			if (peak_1.FromNo <= int_3 && int_3 <= peak_1.ToNo)
			{
				num2 = peak_1.bsYs[int_3 - peak_1.FromNo];
			}
			dots[int_3].Y = num2 + num2 - dots[int_3].Y;
		}
	}

	private bool method_13(ref Peak[] peak_1, ref Peak peak_2)
	{
		int num = peak_1.Length - 1;
		Peak onTanPeak;
		while (num >= 0 && ((onTanPeak = GetOnTanPeak(peak_1[num])) == null || onTanPeak.pkN >= peak_1[num].pkN))
		{
			num--;
		}
		for (int i = 0; i <= num; i++)
		{
			peak_1[i] = peak_1[peak_1.Length - 1 - i];
		}
		Array.Resize(ref peak_1, peak_1.Length - 1 - num);
		if (peak_1.Length == 0)
		{
			return false;
		}
		Array.Sort(peak_1);
		int lfDotNo = peak_1[0].LfDotNo;
		int rtDotNo = peak_1[peak_1.Length - 1].RtDotNo;
		if (dots[lfDotNo].Y >= dots[rtDotNo].Y)
		{
			return false;
		}
		peak_2 = FindPeakByDir(rtDotNo, LR.Right, bool_0: true);
		return peak_2 != null && IsPeakOnTan(peak_2);
	}

	private bool method_14(ref Peak[] peak_1, ref Peak peak_2)
	{
		int i;
		for (i = 0; i < peak_1.Length; i++)
		{
			Peak onTanPeak;
			if ((onTanPeak = GetOnTanPeak(peak_1[i])) != null && onTanPeak.pkN > peak_1[i].pkN)
			{
				break;
			}
		}
		Array.Resize(ref peak_1, i);
		if (peak_1.Length == 0)
		{
			return false;
		}
		int lfDotNo = peak_1[0].LfDotNo;
		int rtDotNo = peak_1[peak_1.Length - 1].RtDotNo;
		if (dots[lfDotNo].Y <= dots[rtDotNo].Y)
		{
			return false;
		}
		peak_2 = FindPeakByDir(lfDotNo, LR.Left, bool_0: true);
		return peak_2 != null && IsPeakOnTan(peak_2);
	}

	private void MarkValeList(string string_4)
	{
		int[] int_ = new int[0];
		for (int i = 0; i < PeaksNum; i++)
		{
			int lfDotNo;
			if (!Class49.ValueInArray(int_, lfDotNo = peak_0[i].lfDotNo))
			{
				Class49.Append2Array(ref int_, lfDotNo);
			}
			if (!Class49.ValueInArray(int_, lfDotNo = peak_0[i].rtDotNo))
			{
				Class49.Append2Array(ref int_, lfDotNo);
			}
			if (!Class49.ValueInArray(int_, lfDotNo = peak_0[i].bsLfV.dotNo))
			{
				Class49.Append2Array(ref int_, lfDotNo);
			}
			if (!Class49.ValueInArray(int_, lfDotNo = peak_0[i].bsRtV.dotNo))
			{
				Class49.Append2Array(ref int_, lfDotNo);
			}
		}
		string text = "清除Vale点:";
		int length = text.Length;
		for (int j = 0; j < dotLength; j++)
		{
			if (enumPeakStateList[j] == EnumPeakState.Clear && !Class49.ValueInArray(int_, j))
			{
				text = text + j + ", ";
				enumPeakStateList[j] = EnumPeakState.None;
			}
		}
		int length2 = text.Length;
	}

	private bool IsContainPeak(Peak[] peak_1, Peak peak_2)
	{
		for (int i = 0; i < peak_1.Length; i++)
		{
			if (peak_1[i] == peak_2)
			{
				return true;
			}
		}
		return false;
	}

	private void SetMyTickCount()
	{
		tickCount = GetTickCount();
	}

	private int[] GetPointIndexList_ByState1_2()
	{
		List<int> list = new List<int>();
		for (int i = 0; i < dotLength; i++)
		{
			if (enumPeakStateList[i] == EnumPeakState.Peak || enumPeakStateList[i] == EnumPeakState.Vale)
			{
				list.Add(i);
			}
		}
		return list.ToArray();
	}

	private bool method_2(ref int startIndex, ref int endIndex)
	{
		if (endIndex - startIndex < 5)
		{
			return false;
		}
		int num = (startIndex + endIndex) / 2;
		if (enumPeakStateList[num] != EnumPeakState.None)
		{
			return false;
		}
		for (int num2 = num - 1; num2 >= startIndex; num2--)
		{
			if (enumPeakStateList[num2] != EnumPeakState.None)
			{
				startIndex = num2;
				break;
			}
		}
		for (int num2 = num + 1; num2 <= endIndex; num2++)
		{
			if (enumPeakStateList[num2] != EnumPeakState.None)
			{
				endIndex = num2;
				break;
			}
		}
		return true;
	}

	private int[] MergeIntArray(int[] int_2, int[] int_3)
	{
		int[] array = new int[0];
		for (int i = 0; i < int_2.Length; i++)
		{
			if (!Class49.ValueInArray(int_3, int_2[i]))
			{
				Array.Resize(ref array, array.Length + 1);
				array[array.Length - 1] = int_2[i];
			}
		}
		return array;
	}

	private int GetMinAvgDisYIndex_2Point(int startIndex, int endIndex, bool bool_0)
	{
		if (startIndex > endIndex)
		{
			int num = startIndex;
			startIndex = endIndex;
			endIndex = num;
		}
		int num2 = startIndex;
		int num3 = endIndex;
		if (!bool_0)
		{
			num2++;
			num3--;
		}
		float num4 = float.MaxValue;
		int result = -1;
		for (int i = num2; i <= num3; i++)
		{
			float num5 = Math.Abs(dots[i].Y - fDistanceList[i]);
			if (num5 < num4)
			{
				num4 = num5;
				result = i;
			}
		}
		return result;
	}

	private int GetMinYIndex_2Point(int startIndex, int endIndex, bool bool_0)
	{
		if (startIndex > endIndex)
		{
			int num = startIndex;
			startIndex = endIndex;
			endIndex = num;
		}
		float num2 = float.MinValue;
		int result = -1;
		int num3 = startIndex;
		int num4 = endIndex;
		if (!bool_0)
		{
			num3++;
			num4--;
		}
		for (int i = num3; i <= num4; i++)
		{
			if (dots[i].Y > num2)
			{
				num2 = dots[i].Y;
				result = i;
			}
		}
		return result;
	}

	private int GetMaxYIndex_2Point(int startIndex, int endIndex, bool bool_0)
	{
		if (startIndex > endIndex)
		{
			int num = startIndex;
			startIndex = endIndex;
			endIndex = num;
		}
		float num2 = float.MaxValue;
		int result = -1;
		int num3 = startIndex;
		int num4 = endIndex;
		if (!bool_0)
		{
			num3++;
			num4--;
		}
		for (int i = num3; i <= num4; i++)
		{
			if (dots[i].Y < num2)
			{
				num2 = dots[i].Y;
				result = i;
			}
		}
		return result;
	}

	private bool IsValidPeak2(int startIndex, int endIndex)
	{
		float num = Math.Abs(dots[startIndex].X - dots[endIndex].X);
		if (num < myfPeakWidth + myfPeakWidth)
		{
			return true;
		}
		float num2 = Math.Abs(dots[startIndex].Y - dots[endIndex].Y) / num;
		return num2 > Integs.PkSlope;
	}

	private void SetPeakListData()
	{
		for (int i = 0; i < PeaksNum; i++)
		{
			Peak peak = peak_0[i];
			if (peak.area > 0f)
			{
				peak.startT = dots[peak.FromNo].X;
				peak.endT = dots[peak.ToNo].X;
				peak.startV = dots[peak.FromNo].Y;
				peak.endV = dots[peak.ToNo].Y;
			}
		}
		for (int i = 0; i < PeaksNum; i++)
		{
			peak_0[i].pkStyle = PeakStyle.None;
		}
		for (int i = 0; i < PeaksNum; i++)
		{
			Peak peak = peak_0[i];
			if (peak.pkStyle != PeakStyle.None)
			{
				continue;
			}
			if (SimpleIsolatedPeak(peak))
			{
				peak.pkStyle = PeakStyle.Single;
				continue;
			}
			Peak onTanPeak = GetOnTanPeak(peak);
			if (onTanPeak != null)
			{
				onTanPeak.pkStyle = PeakStyle.SO;
				peak.pkStyle = PeakStyle.Shoulder;
			}
			else if (peak.BsLfTogether || peak.BsRtTogether)
			{
				peak.pkStyle = PeakStyle.Overlap;
			}
		}
		for (int i = 0; i < PeaksNum; i++)
		{
			if (peak_0[i].pkStyle == PeakStyle.None)
			{
				peak_0[i].pkStyle = PeakStyle.Single;
			}
		}
		for (int i = 0; i < PeaksNum; i++)
		{
			Peak peak = peak_0[i];
			peak.Asymmetry = peak.float_1 / peak.float_0;
			peak.SymmetryTailing = (peak.a05i + peak.b05i) / (peak.a05i + peak.a05i);
			peak.Efficiency_EP = Convert.ToSingle(5.545 * Math.Pow(peak.pkRT / peak.WO5, 2.0));
			peak.Efficiency_USP = Convert.ToSingle(16.0 * Math.Pow(peak.pkRT / peak.width, 2.0));
			peak.Efficiency_JP = Convert.ToSingle(5.55 * Math.Pow(peak.pkRT / peak.WO5, 2.0));
			Peak peak2 = ((i == 0) ? null : peak_0[i - 1]);
			peak.Resolution_EP = ((i == 0) ? 0f : (1.18f * (peak.pkRT - peak2.pkRT) / (peak2.WO5 + peak.WO5)));
			peak.Resolution_USP = ((i == 0) ? 0f : (2f * (peak.pkRT - peak2.pkRT) / (peak2.width + peak.width)));
		}
	}

	private float GetYDistance_2Point(int startIndex, int endIndex)
	{
		if (startIndex > endIndex)
		{
			int num = startIndex;
			startIndex = endIndex;
			endIndex = num;
		}
		float num2 = float.MinValue;
		float num3 = float.MaxValue;
		for (int i = startIndex; i <= endIndex; i++)
		{
			num2 = Math.Max(num2, dots[i].Y);
			num3 = Math.Min(num3, dots[i].Y);
		}
		return num2 - num3;
	}

	private bool FindNegativePeak_MaxSlopPoint(LR lrDirect, ref int startIndex, ref int valeIndex, int endIndex)
	{
		float num = float.MinValue;
		int num2 = -1;
		if (lrDirect == LR.Left)
		{
			for (int num3 = startIndex; num3 >= endIndex; num3--)
			{
				PointF pointF = dots[num3];
				float num4 = (pointF.Y - dots[valeIndex].Y) / (dots[valeIndex].X - pointF.X);
				if (num4 > num)
				{
					num = num4;
					num2 = num3;
				}
			}
		}
		if (lrDirect == LR.Right)
		{
			for (int i = valeIndex; i <= endIndex; i++)
			{
				PointF pointF = dots[i];
				float num5 = (pointF.Y - dots[startIndex].Y) / (pointF.X - dots[startIndex].X);
				if (num5 > num)
				{
					num = num5;
					num2 = i;
				}
			}
		}
		if (num2 == -1)
		{
			return false;
		}
		bool result;
		if (lrDirect == LR.Left)
		{
			result = startIndex != num2;
			startIndex = num2;
			return result;
		}
		result = valeIndex != num2;
		valeIndex = num2;
		return result;
	}

	private bool FindPossitivePeak_MaxSlopPoint(LR lrDirect, ref int startIndex, ref int valeIndex, int endIndex)
	{
		float num = float.MaxValue;
		int num2 = -1;
		if (lrDirect == LR.Left)
		{
			for (int num3 = startIndex; num3 >= endIndex; num3--)
			{
				PointF pointF = dots[num3];
				float num4 = (pointF.Y - dots[valeIndex].Y) / (dots[valeIndex].X - pointF.X);
				if (num4 < num)
				{
					num = num4;
					num2 = num3;
				}
			}
		}
		if (lrDirect == LR.Right)
		{
			for (int i = valeIndex; i <= endIndex; i++)
			{
				PointF pointF = dots[i];
				float num5 = (pointF.Y - dots[startIndex].Y) / (pointF.X - dots[startIndex].X);
				if (num5 < num)
				{
					num = num5;
					num2 = i;
				}
			}
		}
		if (num2 == -1)
		{
			return false;
		}
		bool result;
		if (lrDirect == LR.Left)
		{
			result = startIndex != num2;
			startIndex = num2;
			return result;
		}
		result = valeIndex != num2;
		valeIndex = num2;
		return result;
	}

	private bool method_53(LR lr_0, Peak[] peak_1, Peak peak_2, ref Peak peak_3, float float_10, float float_11, float float_12, float float_13)
	{
		PeakGroupMaker peakGroupMaker = null;
		if (lr_0 == LR.Left)
		{
			if (dots[peak_2.LfDotNo].Y < dots[peak_2.RtDotNo].Y)
			{
				return false;
			}
			if (peakGroupMaker1.FirstPeak() != peak_1[0] || peakGroupMaker1.GetCurrentPeak() != peak_3)
			{
				Peak[] array = new Peak[0];
				for (int i = 0; i < peak_1.Length && peak_1[i] != peak_3; i++)
				{
					int num = array.Length;
					Array.Resize(ref array, num + 1);
					array[num] = peak_1[i];
				}
				peakGroupMaker1.Init(lr_0, array, peak_3);
			}
			peakGroupMaker = peakGroupMaker1;
		}
		if (lr_0 == LR.Right)
		{
			if (dots[peak_2.LfDotNo].Y > dots[peak_2.RtDotNo].Y)
			{
				return false;
			}
			if (peakGroupMaker2.GetCurrentPeak() != peak_3 || peakGroupMaker2.LastPeak() != peak_1[peak_1.Length - 1])
			{
				Peak[] array2 = new Peak[0];
				int i = peak_1.Length - 1;
				while (i >= 0 && peak_1[i] != peak_3)
				{
					int num = array2.Length;
					Array.Resize(ref array2, num + 1);
					array2[num] = peak_1[i];
					i--;
				}
				peakGroupMaker2.Init(lr_0, array2, peak_3);
			}
			peakGroupMaker = peakGroupMaker2;
		}
		float num2 = Math.Abs(Convert.ToSingle(peak_2.tanValue));
		peakGroupMaker.SetApplyIntegs(this);
		PeakGroup class39_ = null;
		while (peakGroupMaker.MakePeakGroupList(lr_0, dots, ref class39_))
		{
			if (((lr_0 == LR.Left) ? (class39_.tanValue < float_12 * Integs.PkSlope) : (class39_.tanValue < float_13 * Integs.PkSlope)) || class39_.tanValue < float_11 * num2)
			{
				return false;
			}
			if (method_1(lr_0, peak_2, ref class39_, ref peak_3, float_10))
			{
				return true;
			}
		}
		return false;
	}

	public bool FindVale_MaxSlopPoint(bool isPositive, ref int leftValeIndex, ref int rightValeIndex, int leftBoundIndex, int rightBoundIndex)
	{
		bool flag = true;
		bool flag2 = true;
		if (!isPositive)
		{
			while (flag || flag2)
			{
				flag = FindNegativePeak_MaxSlopPoint(LR.Left, ref leftValeIndex, ref rightValeIndex, leftBoundIndex);
				flag2 = FindNegativePeak_MaxSlopPoint(LR.Right, ref leftValeIndex, ref rightValeIndex, rightBoundIndex);
			}
		}
		else
		{
			while (flag || flag2)
			{
				flag = FindPossitivePeak_MaxSlopPoint(LR.Left, ref leftValeIndex, ref rightValeIndex, leftBoundIndex);
				flag2 = FindPossitivePeak_MaxSlopPoint(LR.Right, ref leftValeIndex, ref rightValeIndex, rightBoundIndex);
			}
		}
		return true;
	}

	private float GetSlope2(PointF[] pointF_1, float float_10)
	{
		if (float_10 < pointF_1[0].X || float_10 > pointF_1[pointF_1.Length - 1].X)
		{
			return 0f;
		}
		int int_ = Convert.ToInt32(float_10 * persentOfLength);
		Class49.SafeValueCheck(ref int_, 0, pointF_1.Length - 1);
		float num = Math.Abs(pointF_1[int_].X - float_10);
		int num2 = int_;
		for (int num3 = int_ - 1; num3 >= 0; num3--)
		{
			float num4 = Math.Abs(pointF_1[num3].X - float_10);
			if (num4 >= num)
			{
				break;
			}
			num = num4;
			num2 = num3;
		}
		for (int num3 = int_ + 1; num3 < pointF_1.Length; num3++)
		{
			float num4 = Math.Abs(pointF_1[num3].X - float_10);
			if (num4 >= num)
			{
				break;
			}
			num = num4;
			num2 = num3;
		}
		if (float_10 == pointF_1[num2].X)
		{
			return pointF_1[num2].Y;
		}
		if (float_10 < pointF_1[num2].X)
		{
			return GetSlope2(pointF_1, num2 - 1, num2, float_10);
		}
		return GetSlope2(pointF_1, num2, num2 + 1, float_10);
	}

	private float GetYDistance_2Point2(int startIndex, int endIndex)
	{
		return Math.Abs(dots[startIndex].Y - dots[endIndex].Y);
	}

	private float GetDistance_3Point(int leftIdx, int conerIdx, int rightIdx)
	{
		float num = (dots[conerIdx].Y - dots[leftIdx].Y) / Math.Max(dots[conerIdx].X - dots[leftIdx].X, 1E-10f);
		float num2 = (dots[rightIdx].X - dots[leftIdx].X) * num;
		return dots[leftIdx].Y + num2;
	}

	private float GetSlope_2Point(int startIndex, int endIndex)
	{
		if (startIndex > endIndex)
		{
			int num = startIndex;
			startIndex = endIndex;
			endIndex = num;
		}
		return (dots[endIndex].Y - dots[startIndex].Y) / GetXDistance_2Point2(startIndex, endIndex);
	}

	private float GetXDistance_2Point2(int startIndex, int endIndex)
	{
		return Math.Abs(dots[endIndex].X - dots[startIndex].X);
	}

	private void ExcludePointForPeak(int startIndex, int endIndex)
	{
		int num = 0;
		int[] array = new int[0];
		for (int i = startIndex; i <= endIndex; i++)
		{
			EnumPeakState enumPeakState = EnumPeakState.None;
			if (enumPeakStateList[i] != EnumPeakState.Peak && enumPeakStateList[i] != EnumPeakState.Vale)
			{
				continue;
			}
			enumPeakState = enumPeakStateList[i];
			EnumPeakState enumPeakState2 = ((enumPeakState != EnumPeakState.Peak) ? EnumPeakState.Peak : EnumPeakState.Vale);
			float num3;
			float y;
			float num2 = (num3 = (y = dots[i].Y));
			Array.Resize(ref array, 1);
			array[0] = i;
			float num4 = 0.5f * Math.Max(0.0001f, myfThreshold);
			for (int j = i + 1; j < endIndex; j++)
			{
				if (dots[j].Y > num3)
				{
					num3 = dots[j].Y;
				}
				if (dots[j].Y < y)
				{
					y = dots[j].Y;
				}
				if (num3 >= num2 + num4 || y <= num2 - num4 || enumPeakStateList[j] == enumPeakState2 || j == endIndex - 1)
				{
					if (array.Length != 1)
					{
						num3 = (y = dots[i].Y);
						int num6;
						int num5 = (num6 = i);
						foreach (int num7 in array)
						{
							if (dots[num7].Y > num3)
							{
								num3 = dots[num7].Y;
								num5 = num7;
							}
							if (dots[num7].Y < y)
							{
								y = dots[num7].Y;
								num6 = num7;
							}
							enumPeakStateList[num7] = EnumPeakState.None;
						}
						int num8 = -1;
						if (num5 == num6)
						{
							num8 = (array[0] + array[array.Length - 1]) / 2;
						}
						else
						{
							switch (enumPeakState)
							{
							case EnumPeakState.Peak:
								num8 = num5;
								break;
							case EnumPeakState.Vale:
								num8 = num6;
								break;
							}
						}
						enumPeakStateList[num8] = enumPeakState;
						i = Math.Max(0, num8 - 1);
					}
					else
					{
						i = j - 1;
					}
					break;
				}
				if (enumPeakStateList[j] == enumPeakState)
				{
					Array.Resize(ref array, array.Length + 1);
					array[array.Length - 1] = j;
				}
			}
		}
		for (int k = startIndex; k <= endIndex; k++)
		{
			if (enumPeakStateList[k] != EnumPeakState.Peak || !(dots[k].Y >= 800f))
			{
				continue;
			}
			int[] array2 = new int[0];
			int num9 = k + 1;
			if (num9 >= endIndex)
			{
				continue;
			}
			bool flag = true;
			if (array2.Length != 0)
			{
				int num10 = k;
				for (num = 0; num < array2.Length; num++)
				{
					if (dots[array2[num]].Y > dots[num10].Y)
					{
						num10 = array2[num];
					}
				}
				enumPeakStateList[k] = EnumPeakState.None;
				for (num = 0; num < array2.Length; num++)
				{
					enumPeakStateList[array2[num]] = EnumPeakState.None;
				}
				enumPeakStateList[num10] = EnumPeakState.Peak;
			}
			k = num9 - 1;
		}
	}

	private void ApplyIntegs2(PointF[] dots, PointF[] svDots, float virBsY, PointF[] asDots, bool bool_0, out bool bool_1)
	{
		float dtecDelay = Integs.DtecDelay;
		for (int i = 0; i < dots.Length; i++)
		{
			dots[i].Y = svDots[i].Y;
			dots[i].X = svDots[i].X - dtecDelay;
		}
		if (asDots != null && bool_0)
		{
			for (int i = 0; i < dots.Length; i++)
			{
				dots[i].Y += GetSlope2(asDots, svDots[i].X);
			}
		}
		if (asDots != null && !bool_0)
		{
			for (int i = 0; i < dots.Length; i++)
			{
				dots[i].Y -= GetSlope2(asDots, svDots[i].X);
			}
		}
		this.dots = dots;
		bool_1 = true;
		for (int i = 0; i < dots.Length; i++)
		{
			if (dots[i].Y != 0f)
			{
				bool_1 = false;
				break;
			}
		}
		if (dotLength != 0)
		{
			findTheLastXValue(out var iLastIndex, out var fLastValue);
			persentOfLength = (float)iLastIndex / Math.Max(1E-07f, fLastValue);
		}
		myfPeakWidth = Integs.PeakWidth;
		myfThreshold = Integs.Threshold;
		Array.Resize(ref enumPeakStateList, dotLength);
		Array.Resize(ref fDistanceList, dotLength);
		for (int i = 0; i < dotLength; i++)
		{
			enumPeakStateList[i] = EnumPeakState.None;
			fDistanceList[i] = virBsY;
		}
		Array.Resize(ref peak_0, 0);
	}

	private void findTheLastXValue(out int iLastIndex, out float fLastValue)
	{
		int theLastOne = dots.Length - 1;
		findTheLastXValue(theLastOne, out iLastIndex, out fLastValue);
	}

	private void findTheLastXValue(int theLastOne, out int iLastIndex, out float fLastValue)
	{
		int num = (iLastIndex = theLastOne);
		fLastValue = dots[num].X;
		for (int num2 = num; num2 >= 0; num2--)
		{
			if (dots[num2].X > 0f)
			{
				iLastIndex = num2;
				fLastValue = dots[num2].X;
				break;
			}
		}
	}

	private bool IsPeakOnTan(Peak peak_1)
	{
		if (peak_1.lfTgntYs.Length == 1 && peak_1.rtTgntYs.Length == 1)
		{
			for (int i = 0; i < PeaksNum; i++)
			{
				if (peak_0[i] != peak_1 && (peak_0[i].OnLfTgnt(peak_1.pkN) || peak_0[i].OnRtTgnt(peak_1.pkN)))
				{
					return false;
				}
			}
		}
		return true;
	}

	private bool IsHaveNeedClearPeak(bool bool_0, int int_2)
	{
		if (bool_0)
		{
			for (int num = int_2 - 1; num >= 0; num--)
			{
				if (enumPeakStateList[num] == EnumPeakState.Peak || enumPeakStateList[num] == EnumPeakState.Vale)
				{
					return false;
				}
				if (enumPeakStateList[num] == EnumPeakState.Clear)
				{
					return true;
				}
			}
		}
		if (!bool_0)
		{
			for (int num = int_2 + 1; num < dotLength; num++)
			{
				if (enumPeakStateList[num] == EnumPeakState.Peak || enumPeakStateList[num] == EnumPeakState.Vale)
				{
					return false;
				}
				if (enumPeakStateList[num] == EnumPeakState.Clear)
				{
					return true;
				}
			}
		}
		return true;
	}

	private bool method_4(int int_2)
	{
		bool flag = false;
		Peak peak = null;
		Peak peak2 = null;
		int num = -1;
		int num2 = -1;
		for (int i = 0; i < PeaksNum; i++)
		{
			if (peak_0[i].RtDotNo == int_2)
			{
				peak = peak_0[i];
				num = i;
				break;
			}
		}
		for (int i = PeaksNum - 1; i >= 0; i--)
		{
			if (peak_0[i].LfDotNo == int_2)
			{
				peak2 = peak_0[i];
				num2 = i;
				break;
			}
		}
		if (num < 0 || num2 < 0)
		{
			return false;
		}
		for (int i = 0; i <= num; i++)
		{
			if (peak_0[i].bsLfV.dotNo == peak.bsLfV.dotNo && peak_0[i].bsRtV.dotNo == peak.bsRtV.dotNo)
			{
				int dotNo = peak_0[i].bsRtV.dotNo;
				peak_0[i].bsRtV.dotNo = peak2.bsRtV.dotNo;
				needProcPeak(peak_0[i]);
				if (peak_0[i].bsRtV.dotNo != dotNo)
				{
					flag = true;
				}
			}
		}
		for (int i = PeaksNum - 1; i >= num2; i--)
		{
			if (peak_0[i].bsLfV.dotNo == peak2.bsLfV.dotNo && peak_0[i].bsRtV.dotNo == peak2.bsRtV.dotNo)
			{
				int dotNo = peak_0[i].bsLfV.dotNo;
				peak_0[i].bsLfV.dotNo = peak.bsLfV.dotNo;
				needProcPeak(peak_0[i]);
				if (peak_0[i].bsLfV.dotNo != dotNo)
				{
					flag = true;
				}
			}
		}
		if (flag)
		{
			method_62(int_2);
		}
		return flag;
	}

	private bool IsValePoint(int int_2)
	{
		float y = dots[int_2].Y;
		float num = Math.Max(0.0001f, myfThreshold);
		bool flag = false;
		bool flag2 = false;
		for (int i = 1; i < dotLength; i++)
		{
			int num2 = int_2 - i;
			int num3 = int_2 + i;
			if (!flag && 0 <= num2 && num2 < dotLength)
			{
				if (dots[num2].Y < y)
				{
					return false;
				}
				if (dots[num2].Y > y + num)
				{
					flag = true;
				}
			}
			if (!flag2 && 0 <= num3 && num3 < dotLength)
			{
				if (dots[num3].Y < y)
				{
					return false;
				}
				if (dots[num3].Y > y + num)
				{
					flag2 = true;
				}
			}
			if (flag && flag2)
			{
				return true;
			}
		}
		return false;
	}

	private bool IsPeakPoint(int indexPeak)
	{
		float y = dots[indexPeak].Y;
		float num = Math.Max(0.0001f, myfThreshold);
		bool flag = false;
		bool flag2 = false;
		int num2 = Math.Min(4000, dotLength);
		for (int i = 1; i < num2; i++)
		{
			int num3 = indexPeak - i;
			int num4 = indexPeak + i;
			if (!flag && 0 <= num3 && num3 < dotLength)
			{
				if (dots[num3].Y > y)
				{
					return false;
				}
				if (dots[num3].Y < y - num)
				{
					flag = true;
				}
			}
			if (!flag2 && 0 <= num4 && num4 < dotLength)
			{
				if (dots[num4].Y > y)
				{
					return false;
				}
				if (dots[num4].Y < y - num)
				{
					flag2 = true;
				}
			}
			if (flag && flag2)
			{
				return true;
			}
		}
		return false;
	}

	private bool IsPeakPointSov(int indexPeak, int startIndex, int endIndex)
	{
		int num = endIndex - startIndex;
		indexPeak -= startIndex;
		float y = dots[indexPeak + startIndex].Y;
		float num2 = Math.Max(0.0001f, myfThreshold);
		bool flag = false;
		bool flag2 = false;
		int num3 = Math.Min(4000, num);
		for (int i = 1; i < num3; i++)
		{
			int num4 = indexPeak - i;
			int num5 = indexPeak + i;
			if (!flag && 0 <= num4 && num4 < num)
			{
				if (dots[num4 + startIndex].Y > y)
				{
					return false;
				}
				if (dots[num4 + startIndex].Y < y - num2)
				{
					flag = true;
				}
			}
			if (!flag2 && 0 <= num5 && num5 < num)
			{
				if (dots[num5 + startIndex].Y > y)
				{
					return false;
				}
				if (dots[num5 + startIndex].Y < y - num2)
				{
					flag2 = true;
				}
			}
			if (flag && flag2)
			{
				return true;
			}
		}
		return false;
	}

	private bool method_42(VS vs_0, int int_2, float float_10)
	{
		if (int_2 == 0)
		{
			switch (vs_0)
			{
			case VS.PL:
				return dots[0].Y < dots[1].Y && dots[int_2].Y < float_10;
			case VS.PR:
				return false;
			case VS.NL:
				return dots[0].Y > dots[1].Y && dots[int_2].Y > float_10;
			case VS.NR:
				return false;
			}
		}
		if (int_2 == dotLength - 1)
		{
			switch (vs_0)
			{
			case VS.PL:
				return false;
			case VS.PR:
				return dots[int_2 - 1].Y > dots[int_2].Y && dots[int_2].Y < float_10;
			case VS.NL:
				return false;
			case VS.NR:
				return dots[int_2 - 1].Y < dots[int_2].Y && dots[int_2].Y > float_10;
			}
		}
		return vs_0 switch
		{
			VS.PL => dots[int_2 - 1].Y >= dots[int_2].Y && dots[int_2].Y < dots[int_2 + 1].Y && dots[int_2].Y < float_10, 
			VS.PR => dots[int_2 - 1].Y > dots[int_2].Y && dots[int_2].Y <= dots[int_2 + 1].Y && dots[int_2].Y < float_10, 
			VS.NL => dots[int_2 - 1].Y <= dots[int_2].Y && dots[int_2].Y > dots[int_2 + 1].Y && dots[int_2].Y > float_10, 
			VS.NR => dots[int_2 - 1].Y < dots[int_2].Y && dots[int_2].Y >= dots[int_2 + 1].Y && dots[int_2].Y > float_10, 
			_ => false, 
		};
	}

	private void MarkPeakAndValuePoint(int index, EnumDetectPeakMethod peakMethod)
	{
		enumPeakStateList[index] = EnumPeakState.None;
		switch (peakMethod)
		{
		case EnumDetectPeakMethod.OnlyPeak:
			if (IsPeakPoint(index))
			{
				enumPeakStateList[index] = EnumPeakState.Peak;
			}
			break;
		case EnumDetectPeakMethod.PeakAndVale:
			if (IsPeakPoint(index) && dots[index].Y > fDistanceList[index] + myfThreshold)
			{
				enumPeakStateList[index] = EnumPeakState.Peak;
			}
			if (IsValePoint(index) && dots[index].Y < fDistanceList[index] - myfThreshold)
			{
				enumPeakStateList[index] = EnumPeakState.Vale;
			}
			break;
		}
	}

	private void MarkPeakAndValuePointSolv(int index, int startIndex, int endIndex, EnumDetectPeakMethod peakMethod)
	{
		enumPeakStateList[index] = EnumPeakState.None;
		switch (peakMethod)
		{
		case EnumDetectPeakMethod.OnlyPeak:
			if (IsPeakPointSov(index, startIndex, endIndex))
			{
				enumPeakStateList[index] = EnumPeakState.Peak;
			}
			break;
		case EnumDetectPeakMethod.PeakAndVale:
			if (IsPeakPointSov(index, startIndex, endIndex) && dots[index].Y > fDistanceList[index] + myfThreshold)
			{
				enumPeakStateList[index] = EnumPeakState.Peak;
			}
			if (IsValePoint(index) && dots[index].Y < fDistanceList[index] - myfThreshold)
			{
				enumPeakStateList[index] = EnumPeakState.Vale;
			}
			break;
		}
	}

	private List<PeakDetectResult> findPeakStateList()
	{
		List<PeakDetectResult> list = new List<PeakDetectResult>();
		for (int i = 0; i < enumPeakStateList.Length; i++)
		{
			if (enumPeakStateList[i] == EnumPeakState.Peak || enumPeakStateList[i] == EnumPeakState.Vale)
			{
				list.Add(new PeakDetectResult(enumPeakStateList[i], dots[i], i));
			}
		}
		return list;
	}

	private bool MarkPeakAndValuePoint(int startIndex, int endIndex, EnumDetectPeakMethod peakMethod)
	{
		for (int num = startIndex - 1; num >= 0; num--)
		{
			if (enumPeakStateList[num] == EnumPeakState.Peak || enumPeakStateList[num] == EnumPeakState.Vale)
			{
				return false;
			}
			if (enumPeakStateList[num] == EnumPeakState.Clear)
			{
				int num2 = num - 1;
				while (num2 >= 0 && enumPeakStateList[num2] != EnumPeakState.Peak && enumPeakStateList[num2] != EnumPeakState.Vale)
				{
					if (enumPeakStateList[num2] == EnumPeakState.Clear)
					{
						enumPeakStateList[num] = EnumPeakState.None;
					}
					num2--;
				}
				break;
			}
		}
		for (int i = endIndex + 1; i < dotLength; i++)
		{
			if (enumPeakStateList[i] == EnumPeakState.Peak || enumPeakStateList[i] == EnumPeakState.Vale)
			{
				return false;
			}
			if (enumPeakStateList[i] != EnumPeakState.Clear)
			{
				continue;
			}
			for (int j = i + 1; j < dotLength && enumPeakStateList[j] != EnumPeakState.Peak && enumPeakStateList[j] != EnumPeakState.Vale; j++)
			{
				if (enumPeakStateList[j] == EnumPeakState.Clear)
				{
					enumPeakStateList[i] = EnumPeakState.None;
				}
			}
			break;
		}
		if (peakMethod == EnumDetectPeakMethod.PeakAndVale)
		{
			for (int k = startIndex; k <= endIndex; k++)
			{
				fDistanceList[k] = GetDistance_3Point(startIndex, endIndex, k);
			}
		}
		for (int l = startIndex; l <= endIndex; l++)
		{
			MarkPeakAndValuePoint(l, peakMethod);
		}
		enumPeakStateList[endIndex] = EnumPeakState.None;
		enumPeakStateList[startIndex] = EnumPeakState.None;
		ExcludePointForPeak(startIndex, endIndex);
		if (!FindVvByPeak(startIndex, endIndex))
		{
			return false;
		}
		return true;
	}

	private bool MarkPeakAndValuePointSolv(int startIndex, int endIndex, EnumDetectPeakMethod peakMethod)
	{
		int num = endIndex + 1;
		if (peakMethod == EnumDetectPeakMethod.PeakAndVale)
		{
			for (int i = startIndex; i <= endIndex; i++)
			{
				fDistanceList[i] = GetDistance_3Point(startIndex, endIndex, i);
			}
		}
		for (int j = startIndex; j <= endIndex; j++)
		{
			MarkPeakAndValuePointSolv(j, startIndex, endIndex, peakMethod);
		}
		enumPeakStateList[endIndex] = EnumPeakState.None;
		enumPeakStateList[startIndex] = EnumPeakState.None;
		ExcludePointForPeak(startIndex, endIndex);
		if (!FindVvByPeakSolv(startIndex, endIndex))
		{
			return false;
		}
		return true;
	}

	private bool method_45(int int_2, int int_3)
	{
		for (int i = int_2; i <= int_3; i++)
		{
			if (0 < i && i < dotLength && enumPeakStateList[i] != EnumPeakState.None && enumPeakStateList[i - 1] != EnumPeakState.None)
			{
				return false;
			}
		}
		enumPeakStateList[int_3] = EnumPeakState.None;
		enumPeakStateList[int_2] = EnumPeakState.None;
		float num = -1f;
		myfMaxThreshold = -1f;
		myfMaxPeakWidth = num;
		float num2 = 0f;
		float num3 = 0f;
		int[] array = new int[0];
		for (int i = int_2; i <= int_3; i++)
		{
			if (enumPeakStateList[i] != EnumPeakState.Peak && enumPeakStateList[i] != EnumPeakState.Vale)
			{
				continue;
			}
			int num4;
			int num10;
			float y;
			int num8;
			float num6;
			float num5;
			if (enumPeakStateList[i] == EnumPeakState.Peak)
			{
				num4 = -1;
				num5 = -1f;
				num6 = -1f;
				y = dots[i].Y;
				float num7 = float.MinValue;
				num8 = i - 1;
				while (num8 >= int_2)
				{
					num7 = Math.Max(num7, dots[num8].Y);
					float num9;
					if ((method_42(VS.PL, num8, y) || enumPeakStateList[num8] == EnumPeakState.Clear) && (num4 == -1 || GetXDistance_2Point2(num8, num4) < num2 || ((num9 = dots[num4].Y - dots[num8].Y) > num3 && (num7 - dots[num8].Y) / num9 < 1.45f)))
					{
						y = dots[num8].Y;
						num4 = num8;
						num6 = GetXDistance_2Point2(num8, i);
						num5 = GetYDistance_2Point2(num8, i);
						num2 = num6 * 0.21f;
						num3 = num5 * 0.12f;
						num7 = dots[num8].Y;
					}
					if (enumPeakStateList[num8] != EnumPeakState.Clear)
					{
						if (enumPeakStateList[num8] == EnumPeakState.Peak || enumPeakStateList[num8] == EnumPeakState.Vale)
						{
							throw new Exception("P前无V点:P " + i);
						}
						num8--;
						continue;
					}
					if (num4 == -1)
					{
						num4 = num8;
					}
					if (num4 != num8 && IsValidePeak(num8, num4))
					{
						enumPeakStateList[num8] = EnumPeakState.None;
						num4 = (num8 + num4) / 2;
					}
					break;
				}
				if (num4 == -1)
				{
					enumPeakStateList[i] = EnumPeakState.None;
				}
				else
				{
					enumPeakStateList[num4] = EnumPeakState.Clear;
					num10 = -1;
					num5 = -1f;
					num6 = -1f;
					y = dots[i].Y;
					Array.Resize(ref array, 0);
					num7 = float.MinValue;
					for (num8 = i + 1; num8 <= int_3; num8++)
					{
						if (enumPeakStateList[num8] != EnumPeakState.Peak)
						{
							if (enumPeakStateList[num8] != EnumPeakState.Vale)
							{
								num7 = Math.Max(num7, dots[num8].Y);
								float num9;
								if (method_42(VS.PR, num8, y) && (num10 == -1 || GetXDistance_2Point2(num10, num8) < num2 || ((num9 = dots[num10].Y - dots[num8].Y) > num3 && (num7 - dots[num8].Y) / num9 < 1.45f)))
								{
									Array.Resize(ref array, array.Length + 1);
									array[array.Length - 1] = num8;
									y = dots[num8].Y;
									num10 = num8;
									num6 = GetXDistance_2Point2(i, num8);
									num5 = GetYDistance_2Point2(i, num8);
									num2 = num6 * 0.21f;
									num3 = num5 * 0.12f;
									num7 = dots[num8].Y;
								}
								continue;
							}
							int minAvgDisYIndex_2Point = GetMinAvgDisYIndex_2Point(i, num8, bool_0: false);
							float y2 = dots[minAvgDisYIndex_2Point].Y;
							if (num10 == -1)
							{
								num10 = minAvgDisYIndex_2Point;
							}
							else if (dots[num8].Y < y2 && y2 < dots[i].Y)
							{
								num10 = -1;
								float num11 = y2 - (y2 - dots[num8].Y) * 0.21f;
								float num12 = y2 + (dots[i].Y - y2) * 0.21f;
								float num13 = 0f;
								for (int j = 0; j < array.Length; j++)
								{
									if (!(num11 <= dots[array[j]].Y) || !(dots[array[j]].Y <= num12))
									{
										continue;
									}
									if (num10 == -1)
									{
										num10 = array[j];
										num13 = GetYDistance_2Point2(num10, minAvgDisYIndex_2Point);
										continue;
									}
									float yDistance_2Point = GetYDistance_2Point2(array[j], minAvgDisYIndex_2Point);
									if (yDistance_2Point < num13)
									{
										num13 = yDistance_2Point;
										num10 = array[j];
									}
								}
								if (num10 == -1)
								{
									num10 = minAvgDisYIndex_2Point;
								}
							}
							else
							{
								num10 = minAvgDisYIndex_2Point;
							}
							break;
						}
						if (num10 == -1)
						{
							num10 = GetMaxYIndex_2Point(i, num8, bool_0: false);
						}
						break;
					}
					if (num10 == -1)
					{
						enumPeakStateList[i] = EnumPeakState.None;
						if (IsHaveNeedClearPeak(bool_0: true, num4))
						{
							enumPeakStateList[num4] = EnumPeakState.None;
						}
					}
					else
					{
						enumPeakStateList[num10] = EnumPeakState.Clear;
					}
				}
			}
			if (enumPeakStateList[i] != EnumPeakState.Vale)
			{
				continue;
			}
			num4 = -1;
			num5 = -1f;
			num6 = -1f;
			y = dots[i].Y;
			float num14 = float.MaxValue;
			num8 = i - 1;
			while (num8 >= int_2)
			{
				num14 = Math.Min(num14, dots[num8].Y);
				float num9;
				if ((method_42(VS.NL, num8, y) || enumPeakStateList[num8] == EnumPeakState.Clear) && (num4 == -1 || GetXDistance_2Point2(num8, num4) < num2 || ((num9 = dots[num8].Y - dots[num4].Y) > num3 && (dots[num8].Y - num14) / num9 < 1.45f)))
				{
					y = dots[num8].Y;
					num4 = num8;
					num6 = GetXDistance_2Point2(num8, i);
					num5 = GetYDistance_2Point2(num8, i);
					num2 = num6 * 0.21f;
					num3 = num5 * 0.12f;
					num14 = dots[num8].Y;
				}
				if (enumPeakStateList[num8] != EnumPeakState.Clear)
				{
					if (enumPeakStateList[num8] == EnumPeakState.Peak || enumPeakStateList[num8] == EnumPeakState.Vale)
					{
						throw new Exception("N前无V点:N " + i);
					}
					num8--;
					continue;
				}
				if (num4 == -1)
				{
					num4 = num8;
				}
				if (num4 != num8 && IsValidePeak(num8, num4))
				{
					enumPeakStateList[num8] = EnumPeakState.None;
					num4 = (num8 + num4) / 2;
				}
				break;
			}
			if (num4 == -1)
			{
				enumPeakStateList[i] = EnumPeakState.None;
				continue;
			}
			enumPeakStateList[num4] = EnumPeakState.Clear;
			num10 = -1;
			num5 = -1f;
			num6 = -1f;
			y = dots[i].Y;
			Array.Resize(ref array, 0);
			num14 = float.MaxValue;
			for (num8 = i + 1; num8 <= int_3; num8++)
			{
				if (enumPeakStateList[num8] != EnumPeakState.Peak)
				{
					if (enumPeakStateList[num8] != EnumPeakState.Vale)
					{
						num14 = Math.Min(num14, dots[num8].Y);
						float num9;
						if (method_42(VS.NR, num8, y) && (num10 == -1 || GetXDistance_2Point2(num10, num8) < num2 || ((num9 = dots[num8].Y - dots[num10].Y) > num3 && (dots[num8].Y - num14) / num9 < 1.45f)))
						{
							Array.Resize(ref array, array.Length + 1);
							array[array.Length - 1] = num8;
							y = dots[num8].Y;
							num10 = num8;
							num6 = GetXDistance_2Point2(i, num8);
							num5 = GetYDistance_2Point2(i, num8);
							num2 = num6 * 0.21f;
							num3 = num5 * 0.12f;
							num14 = dots[num8].Y;
						}
						continue;
					}
					if (num10 == -1)
					{
						num10 = GetMinYIndex_2Point(i, num8, bool_0: false);
					}
					break;
				}
				int minAvgDisYIndex_2Point = GetMinAvgDisYIndex_2Point(i, num8, bool_0: false);
				float y2 = dots[minAvgDisYIndex_2Point].Y;
				if (num10 == -1)
				{
					num10 = minAvgDisYIndex_2Point;
				}
				else if (dots[i].Y < y2 && y2 < dots[num8].Y)
				{
					num10 = -1;
					float num11 = y2 - (y2 - dots[i].Y) * 0.21f;
					float num12 = y2 + (dots[num8].Y - y2) * 0.21f;
					float num13 = 0f;
					for (int j = 0; j < array.Length; j++)
					{
						if (!(num11 <= dots[array[j]].Y) || !(dots[array[j]].Y <= num12))
						{
							continue;
						}
						if (num10 == -1)
						{
							num10 = array[j];
							num13 = GetYDistance_2Point2(num10, minAvgDisYIndex_2Point);
							continue;
						}
						float yDistance_2Point = GetYDistance_2Point2(array[j], minAvgDisYIndex_2Point);
						if (yDistance_2Point < num13)
						{
							num13 = yDistance_2Point;
							num10 = array[j];
						}
					}
					if (num10 == -1)
					{
						num10 = minAvgDisYIndex_2Point;
					}
				}
				else
				{
					num10 = minAvgDisYIndex_2Point;
				}
				break;
			}
			if (num10 == -1)
			{
				enumPeakStateList[i] = EnumPeakState.None;
				if (IsHaveNeedClearPeak(bool_0: true, num4))
				{
					enumPeakStateList[num4] = EnumPeakState.None;
				}
			}
			else
			{
				enumPeakStateList[num10] = EnumPeakState.Clear;
			}
		}
		return true;
	}

	private void FindVvByPeak_ProcessPositivePeak(int startIndex, int endIndex, int i, int hwN, ref VV leftVV, ref VV rightVV, ref VV lastVV)
	{
		VV vv_ = new VV();
		float y = dots[i].Y;
		rightVV.index = -1;
		leftVV.index = -1;
		float num = y;
		for (int num2 = i - 1; num2 >= startIndex; num2--)
		{
			if (enumPeakStateList[num2] == EnumPeakState.None)
			{
				getVV(ref vv_, dots, num2, y, hwN);
				bool flag = true;
				num = Math.Min(num, vv_.Y);
				if (vv_.Y > num + myfThreshold)
				{
					break;
				}
				if (vv_.vs_0 == VS.PL || vv_.vs_0 == VS.V)
				{
					if (leftVV.index < 0)
					{
						leftVV = vv_.Copy();
					}
					else if (vv_.Y < leftVV.Y && IsValidPeak2(vv_.index, leftVV.index))
					{
						leftVV = vv_.Copy();
					}
				}
			}
			else
			{
				if (enumPeakStateList[num2] == EnumPeakState.Peak || enumPeakStateList[num2] == EnumPeakState.Vale)
				{
					break;
				}
				if (enumPeakStateList[num2] == EnumPeakState.Clear)
				{
					if (leftVV.index == -1)
					{
						if (dots[num2].Y < y - myfThreshold)
						{
							leftVV = lastVV.Copy();
						}
					}
					else if (dots[num2].Y < leftVV.Y && IsValidPeak2(num2, leftVV.index))
					{
						leftVV = lastVV.Copy();
					}
					break;
				}
			}
		}
		num = y;
		if (leftVV.index < 0)
		{
			return;
		}
		for (int num2 = i + 1; num2 <= endIndex; num2++)
		{
			if (enumPeakStateList[num2] == EnumPeakState.None)
			{
				getVV(ref vv_, dots, num2, y, hwN);
				bool flag2 = true;
				num = Math.Min(num, vv_.Y);
				if (vv_.Y > num + myfThreshold)
				{
					break;
				}
				if (vv_.vs_0 == VS.PR || vv_.vs_0 == VS.V)
				{
					if (rightVV.index < 0)
					{
						rightVV = vv_.Copy();
					}
					else if (vv_.Y < rightVV.Y && IsValidPeak2(vv_.index, rightVV.index))
					{
						rightVV = vv_.Copy();
					}
				}
			}
			else
			{
				if (enumPeakStateList[num2] == EnumPeakState.Peak)
				{
					getVV(ref rightVV, dots, GetMaxYIndex_2Point(i, num2, bool_0: false), y, hwN);
					break;
				}
				if (enumPeakStateList[num2] == EnumPeakState.Vale)
				{
					getVV(ref rightVV, dots, GetMinAvgDisYIndex_2Point(i, num2, bool_0: false), y, hwN);
					break;
				}
				if (enumPeakStateList[num2] == EnumPeakState.Clear)
				{
					break;
				}
			}
		}
	}

	private void FindVvByPeak_ProcessPositivePeakSolv(int startIndex, int endIndex, int i, int hwN, ref VV leftVV, ref VV rightVV, ref VV lastVV)
	{
		VV vv_ = new VV();
		i -= startIndex;
		float y = dots[i + startIndex].Y;
		rightVV.index = -1;
		leftVV.index = -1;
		float num = y;
		for (int num2 = i - 1; num2 >= 1; num2--)
		{
			if (enumPeakStateList[num2 + startIndex] == EnumPeakState.None)
			{
				getVVSolv(ref vv_, dots, num2, y, hwN, endIndex - startIndex, startIndex, endIndex);
				bool flag = true;
				num = Math.Min(num, vv_.Y);
				if (vv_.Y > num + myfThreshold)
				{
					break;
				}
				if (vv_.vs_0 == VS.PL || vv_.vs_0 == VS.V)
				{
					if (leftVV.index < 0)
					{
						leftVV = vv_.Copy();
					}
					else if (vv_.Y < leftVV.Y && IsValidPeak2(vv_.index, leftVV.index))
					{
						leftVV = vv_.Copy();
					}
				}
			}
			else
			{
				if (enumPeakStateList[num2 + startIndex] == EnumPeakState.Peak || enumPeakStateList[num2 + startIndex] == EnumPeakState.Vale)
				{
					break;
				}
				if (enumPeakStateList[num2 + startIndex] == EnumPeakState.Clear)
				{
					if (leftVV.index == -1)
					{
						if (dots[num2 + startIndex].Y < y - myfThreshold)
						{
							leftVV = lastVV.Copy();
						}
					}
					else if (dots[num2 + startIndex].Y < leftVV.Y && IsValidPeak2(num2 + startIndex, leftVV.index))
					{
						leftVV = lastVV.Copy();
					}
					break;
				}
			}
		}
		num = y;
		if (leftVV.index < 0)
		{
			return;
		}
		for (int num2 = i + 1; num2 <= endIndex; num2++)
		{
			if (enumPeakStateList[num2] == EnumPeakState.None)
			{
				getVVSolv(ref vv_, dots, num2, y, hwN, endIndex - startIndex, startIndex, endIndex);
				bool flag2 = true;
				num = Math.Min(num, vv_.Y);
				if (vv_.Y > num + myfThreshold)
				{
					break;
				}
				if (vv_.vs_0 == VS.PR || vv_.vs_0 == VS.V)
				{
					if (rightVV.index < 0)
					{
						rightVV = vv_.Copy();
					}
					else if (vv_.Y < rightVV.Y && IsValidPeak2(vv_.index, rightVV.index))
					{
						rightVV = vv_.Copy();
					}
				}
			}
			else
			{
				if (enumPeakStateList[num2] == EnumPeakState.Peak)
				{
					getVVSolv(ref rightVV, dots, GetMaxYIndex_2Point(i, num2, bool_0: false), y, hwN, endIndex - startIndex, startIndex, endIndex);
					break;
				}
				if (enumPeakStateList[num2] == EnumPeakState.Vale)
				{
					getVVSolv(ref rightVV, dots, GetMinAvgDisYIndex_2Point(i, num2, bool_0: false), y, hwN, endIndex - startIndex, startIndex, endIndex);
					break;
				}
				if (enumPeakStateList[num2] == EnumPeakState.Clear)
				{
					break;
				}
			}
		}
	}

	private void FindVvByPeak_ProcessNegativePeak(int startIndex, int endIndex, int i, int hwN, ref VV leftVV, ref VV rightVV, ref VV lastVV)
	{
		VV vv_ = new VV();
		float y = dots[i].Y;
		rightVV.index = -1;
		leftVV.index = -1;
		float num = y;
		for (int num2 = i - 1; num2 >= startIndex; num2--)
		{
			if (enumPeakStateList[num2] == EnumPeakState.None)
			{
				getVV(ref vv_, dots, num2, y, hwN);
				num = Math.Max(num, vv_.Y);
				if (vv_.Y < num - myfThreshold)
				{
					break;
				}
				if (vv_.vs_0 == VS.NL || vv_.vs_0 == VS.A)
				{
					if (leftVV.index < 0)
					{
						leftVV = vv_.Copy();
					}
					else if (vv_.Y > leftVV.Y && IsValidPeak2(vv_.index, leftVV.index))
					{
						leftVV = vv_.Copy();
					}
				}
			}
			else
			{
				if (enumPeakStateList[num2] == EnumPeakState.Peak || enumPeakStateList[num2] == EnumPeakState.Vale)
				{
					break;
				}
				if (enumPeakStateList[num2] == EnumPeakState.Clear)
				{
					if (leftVV.index == -1)
					{
						if (dots[num2].Y > y + myfThreshold)
						{
							leftVV = lastVV.Copy();
						}
					}
					else if (dots[num2].Y > leftVV.Y && IsValidPeak2(num2, leftVV.index))
					{
						leftVV = lastVV.Copy();
					}
					break;
				}
			}
		}
		num = y;
		if (leftVV.index < 0)
		{
			return;
		}
		for (int num2 = i + 1; num2 <= endIndex; num2++)
		{
			if (enumPeakStateList[num2] == EnumPeakState.None)
			{
				getVV(ref vv_, dots, num2, y, hwN);
				num = Math.Max(num, vv_.Y);
				if (vv_.Y < num - myfThreshold)
				{
					break;
				}
				if (vv_.vs_0 == VS.NR || vv_.vs_0 == VS.A)
				{
					if (rightVV.index < 0)
					{
						rightVV = vv_.Copy();
					}
					else if (vv_.Y > rightVV.Y && IsValidPeak2(vv_.index, rightVV.index))
					{
						rightVV = vv_.Copy();
					}
				}
			}
			else
			{
				if (enumPeakStateList[num2] == EnumPeakState.Peak)
				{
					getVV(ref rightVV, dots, GetMinAvgDisYIndex_2Point(i, num2, bool_0: false), y, hwN);
					break;
				}
				if (enumPeakStateList[num2] == EnumPeakState.Vale)
				{
					getVV(ref rightVV, dots, GetMinYIndex_2Point(i, num2, bool_0: false), y, hwN);
					break;
				}
				if (enumPeakStateList[num2] == EnumPeakState.Clear)
				{
					break;
				}
			}
		}
	}

	private bool FindVvByPeak(int startIndex, int endIndex)
	{
		for (int i = startIndex; i < endIndex; i++)
		{
			if (0 < i && i < dotLength && enumPeakStateList[i] != EnumPeakState.None && enumPeakStateList[i - 1] != EnumPeakState.None)
			{
				return false;
			}
		}
		enumPeakStateList[endIndex] = EnumPeakState.None;
		enumPeakStateList[startIndex] = EnumPeakState.None;
		myfMaxThreshold = -1f;
		myfMaxPeakWidth = -1f;
		int val = Convert.ToInt32((double)(persentOfLength * myfPeakWidth) * 0.5);
		int hwN = Math.Max(1, val);
		VV leftVV = new VV();
		VV rightVV = new VV();
		VV lastVV = new VV();
		bool flag = false;
		Integs.PkSlope = Math.Max(0.0001f, Integs.PkSlope);
		for (int j = startIndex; j <= endIndex; j++)
		{
			bool flag2 = enumPeakStateList[j] == EnumPeakState.Peak;
			bool flag3 = enumPeakStateList[j] == EnumPeakState.Vale;
			if (flag2)
			{
				FindVvByPeak_ProcessPositivePeak(startIndex, endIndex, j, hwN, ref leftVV, ref rightVV, ref lastVV);
			}
			else if (flag3)
			{
				FindVvByPeak_ProcessNegativePeak(startIndex, endIndex, j, hwN, ref leftVV, ref rightVV, ref lastVV);
			}
			if (!(flag2 || flag3))
			{
				continue;
			}
			if (leftVV.index >= 0 && rightVV.index >= 0)
			{
				if (lastVV.index >= 0 && IsValidePeak(lastVV.index, leftVV.index))
				{
					enumPeakStateList[lastVV.index] = EnumPeakState.None;
					if (flag)
					{
						if (flag2)
						{
							int maxYIndex_2Point = GetMaxYIndex_2Point(lastVV.index, leftVV.index, bool_0: true);
							getVV(ref leftVV, dots, maxYIndex_2Point, float.NaN, hwN);
						}
						else
						{
							int maxYIndex_2Point = (lastVV.index + leftVV.index) / 2;
							getVV(ref leftVV, dots, maxYIndex_2Point, float.NaN, hwN);
						}
					}
					else if (flag2)
					{
						int maxYIndex_2Point = (lastVV.index + leftVV.index) / 2;
						getVV(ref leftVV, dots, maxYIndex_2Point, float.NaN, hwN);
					}
					else
					{
						int maxYIndex_2Point = GetMinYIndex_2Point(lastVV.index, leftVV.index, bool_0: true);
						getVV(ref leftVV, dots, maxYIndex_2Point, float.NaN, hwN);
					}
				}
				enumPeakStateList[rightVV.index] = EnumPeakState.Clear;
				enumPeakStateList[leftVV.index] = EnumPeakState.Clear;
				lastVV = rightVV.Copy();
				flag = flag2;
			}
			else
			{
				enumPeakStateList[j] = EnumPeakState.None;
			}
		}
		return true;
	}

	private bool FindVvByPeakSolv(int startIndex, int endIndex)
	{
		for (int i = startIndex; i < endIndex; i++)
		{
			if (0 < i && i < dotLength && enumPeakStateList[i] != EnumPeakState.None && enumPeakStateList[i - 1] != EnumPeakState.None)
			{
				return false;
			}
		}
		enumPeakStateList[endIndex] = EnumPeakState.None;
		enumPeakStateList[startIndex] = EnumPeakState.None;
		myfMaxThreshold = -1f;
		myfMaxPeakWidth = -1f;
		int val = Convert.ToInt32((double)(persentOfLength * myfPeakWidth) * 0.5);
		int hwN = Math.Max(1, val);
		VV leftVV = new VV();
		VV rightVV = new VV();
		VV lastVV = new VV();
		bool flag = false;
		Integs.PkSlope = Math.Max(0.0001f, Integs.PkSlope);
		for (int j = startIndex; j <= endIndex; j++)
		{
			bool flag2 = enumPeakStateList[j] == EnumPeakState.Peak;
			bool flag3 = enumPeakStateList[j] == EnumPeakState.Vale;
			if (flag2)
			{
				FindVvByPeak_ProcessPositivePeakSolv(startIndex, endIndex, j, hwN, ref leftVV, ref rightVV, ref lastVV);
			}
			else if (flag3)
			{
				FindVvByPeak_ProcessNegativePeak(startIndex, endIndex, j, hwN, ref leftVV, ref rightVV, ref lastVV);
			}
			if (!(flag2 || flag3))
			{
				continue;
			}
			if (leftVV.index >= 0 && rightVV.index >= 0)
			{
				if (lastVV.index >= 0 && IsValidePeak(lastVV.index, leftVV.index))
				{
					enumPeakStateList[lastVV.index] = EnumPeakState.None;
					if (flag)
					{
						if (flag2)
						{
							int maxYIndex_2Point = GetMaxYIndex_2Point(lastVV.index, leftVV.index, bool_0: true);
							getVV(ref leftVV, dots, maxYIndex_2Point, float.NaN, hwN);
						}
						else
						{
							int maxYIndex_2Point = (lastVV.index + leftVV.index) / 2;
							getVV(ref leftVV, dots, maxYIndex_2Point, float.NaN, hwN);
						}
					}
					else if (flag2)
					{
						int maxYIndex_2Point = (lastVV.index + leftVV.index) / 2;
						getVV(ref leftVV, dots, maxYIndex_2Point, float.NaN, hwN);
					}
					else
					{
						int maxYIndex_2Point = GetMinYIndex_2Point(lastVV.index, leftVV.index, bool_0: true);
						getVV(ref leftVV, dots, maxYIndex_2Point, float.NaN, hwN);
					}
				}
				enumPeakStateList[rightVV.index] = EnumPeakState.Clear;
				enumPeakStateList[leftVV.index] = EnumPeakState.Clear;
				lastVV = rightVV.Copy();
				flag = flag2;
			}
			else
			{
				enumPeakStateList[j] = EnumPeakState.None;
			}
		}
		return true;
	}

	private void SetPeakLeftAndRightValeDotNo(Peak peak_1, LR lr_0)
	{
		if (lr_0 == LR.Left)
		{
			for (int num = peak_1.LfDotNo; num >= 0; num--)
			{
				if (enumPeakStateList[num] == EnumPeakState.Clear)
				{
					peak_1.bsLfV.dotNo = num;
					break;
				}
			}
		}
		if (lr_0 != LR.Right)
		{
			return;
		}
		for (int num = peak_1.RtDotNo; num < dotLength; num++)
		{
			if (enumPeakStateList[num] == EnumPeakState.Clear)
			{
				peak_1.bsRtV.dotNo = num;
				break;
			}
		}
	}

	private Peak[] DetectPeaks(int startIdx, int endIdx)
	{
		int num = 0;
		int num2 = 0;
		for (int i = 0; i < endIdx - startIdx; i++)
		{
			if (enumPeakStateList[i] == EnumPeakState.Peak || enumPeakStateList[i] == EnumPeakState.Vale)
			{
				num++;
				if (startIdx <= i && i <= endIdx)
				{
					num2++;
				}
			}
		}
		if (num == 0)
		{
			num = PeaksNum;
		}
		Peak[] array = new Peak[num];
		num = 0;
		for (int j = 0; j < PeaksNum; j++)
		{
			if (peak_0[j].pkN < startIdx || peak_0[j].pkN > endIdx)
			{
				if (num + 1 > array.Length)
				{
					Array.Resize(ref array, array.Length + 1);
				}
				array[num++] = peak_0[j];
			}
		}
		Peak[] array2 = new Peak[num2];
		num2 = 0;
		for (int k = startIdx; k <= endIdx; k++)
		{
			if (enumPeakStateList[k] != EnumPeakState.Peak && enumPeakStateList[k] != EnumPeakState.Vale)
			{
				continue;
			}
			if (num + 1 > array.Length)
			{
				Array.Resize(ref array, array.Length + 1);
			}
			Peak peak = new Peak();
			peak.positive = enumPeakStateList[k] == EnumPeakState.Peak;
			peak.pkN = k;
			int num3 = -1;
			for (int num4 = k - 1; num4 >= 0; num4--)
			{
				if (enumPeakStateList[num4] == EnumPeakState.Clear)
				{
					num3 = num4;
					break;
				}
			}
			if (num3 == -1)
			{
				enumPeakStateList[k] = EnumPeakState.None;
				return new Peak[0];
			}
			peak.lfDotNo = (peak.bsLfV.dotNo = num3);
			peak.lfTgntYs[0] = dots[num3].Y;
			int num5 = -1;
			for (int num4 = k + 1; num4 < dotLength; num4++)
			{
				if (enumPeakStateList[num4] == EnumPeakState.Clear)
				{
					num5 = num4;
					break;
				}
			}
			if (num5 == -1)
			{
				enumPeakStateList[k] = EnumPeakState.None;
				return new Peak[0];
			}
			peak.rtDotNo = (peak.bsRtV.dotNo = num5);
			peak.rtTgntYs[0] = dots[num5].Y;
			peak.bsRtV.N = -1;
			peak.bsLfV.N = -1;
			peak.width = dots[num5].X - dots[num3].X;
			if (peak.width < 0f)
			{
				if (num3 > 0 && num5 > 0)
				{
					int iLastIndex = num5 - 1;
					float fLastValue = 0f;
					findTheLastXValue(out iLastIndex, out fLastValue);
					peak.width = fLastValue - dots[num3].X;
					if (peak.width > 0f)
					{
						peak.rtDotNo = (peak.bsRtV.dotNo = iLastIndex);
					}
					else
					{
						peak.width = myfPeakWidth + 1E-05f;
					}
				}
				if (peak.width < 0f)
				{
					peak.width = myfPeakWidth + 1E-05f;
				}
			}
			array[num++] = peak;
			array2[num2++] = peak;
		}
		Array.Sort(array);
		peak_0 = array;
		return array2;
	}

	private Peak[] DetectPeaksSolv(int startIdx, int endIdx)
	{
		int num = 0;
		int num2 = 0;
		for (int i = startIdx; i < endIdx; i++)
		{
			if (enumPeakStateList[i] == EnumPeakState.Peak || enumPeakStateList[i] == EnumPeakState.Vale)
			{
				num++;
				if (startIdx <= i && i <= endIdx)
				{
					num2++;
				}
			}
		}
		if (num == 0)
		{
			num = PeaksNum;
		}
		num = 0;
		Peak[] array = new Peak[num2];
		num2 = 0;
		for (int j = startIdx; j <= endIdx; j++)
		{
			if (enumPeakStateList[j] != EnumPeakState.Peak && enumPeakStateList[j] != EnumPeakState.Vale)
			{
				continue;
			}
			Peak peak = new Peak();
			peak.positive = enumPeakStateList[j] == EnumPeakState.Peak;
			peak.pkN = j;
			int num3 = -1;
			for (int num4 = j - 1; num4 >= 0; num4--)
			{
				if (enumPeakStateList[num4] == EnumPeakState.Clear)
				{
					num3 = num4;
					break;
				}
			}
			if (num3 == -1)
			{
				enumPeakStateList[j] = EnumPeakState.None;
				return new Peak[0];
			}
			peak.lfDotNo = (peak.bsLfV.dotNo = num3);
			peak.lfTgntYs[0] = dots[num3].Y;
			int num5 = -1;
			for (int num4 = j + 1; num4 < dotLength; num4++)
			{
				if (enumPeakStateList[num4] == EnumPeakState.Clear)
				{
					num5 = num4;
					break;
				}
			}
			if (num5 == -1)
			{
				enumPeakStateList[j] = EnumPeakState.None;
				return new Peak[0];
			}
			peak.rtDotNo = (peak.bsRtV.dotNo = num5);
			peak.rtTgntYs[0] = dots[num5].Y;
			peak.bsRtV.N = -1;
			peak.bsLfV.N = -1;
			peak.width = dots[num5].X - dots[num3].X;
			if (peak.width < 0f)
			{
				if (num3 > 0 && num5 > 0)
				{
					int iLastIndex = num5 - 1;
					float fLastValue = 0f;
					findTheLastXValue(out iLastIndex, out fLastValue);
					peak.width = fLastValue - dots[num3].X;
					if (peak.width > 0f)
					{
						peak.rtDotNo = (peak.bsRtV.dotNo = iLastIndex);
					}
					else
					{
						peak.width = myfPeakWidth + 1E-05f;
					}
				}
				if (peak.width < 0f)
				{
					peak.width = myfPeakWidth + 1E-05f;
				}
			}
			array[num2++] = peak;
		}
		return array;
	}

	private bool needProcPeak_ForwHorz(ref Peak peak_1)
	{
		float num = float.MaxValue;
		if (!peak_1.positive)
		{
			peak_1.bsStyle = BsStyle.General;
			return false;
		}
		int para = peak_1.para1;
		Peak peak = peak_1;
		Peak peak2;
		while ((peak2 = FindPeakByDir(peak.LfDotNo, LR.Left, bool_0: false)) != null && peak2.positive && IsPeakOnTan(peak2) && peak2.bsStyle == BsStyle.ForwHorz)
		{
			peak = peak2;
		}
		para = Math.Max(para, peak.bsLfV.dotNo);
		float num2 = 0f;
		float num3 = float.MaxValue;
		int n = -1;
		for (int num4 = peak_1.pkN; num4 >= para; num4--)
		{
			num2 = dots[num4].Y;
			if (num2 < num)
			{
				num = num2;
				n = num4;
			}
		}
		for (int i = peak_1.pkN; i <= peak_1.RtDotNo; i++)
		{
			num2 = dots[i].Y;
			if (num2 < num3)
			{
				num3 = num2;
			}
		}
		if (num > num3)
		{
			peak_1.bsStyle = BsStyle.General;
			return false;
		}
		peak.bsLfV.dotNo = para;
		SetPeakLeftAndRightValeDotNo(peak_1, LR.Right);
		peak_1.bsLfV.N = n;
		peak_1.bsRtV.N = -1;
		peak_1.bsHorzY = num;
		peak_1.tanValue = 0.0;
		return true;
	}

	private bool needProcPeak_BackHorz(ref Peak peak_1)
	{
		float num = float.MaxValue;
		if (!peak_1.positive)
		{
			peak_1.bsStyle = BsStyle.General;
			return false;
		}
		int para = peak_1.para2;
		Peak peak = peak_1;
		Peak peak2;
		while ((peak2 = FindPeakByDir(peak.RtDotNo, LR.Right, bool_0: false)) != null && peak2.positive && IsPeakOnTan(peak2) && peak2.bsStyle == BsStyle.BackHorz)
		{
			peak = peak2;
		}
		para = Math.Min(para, peak.bsRtV.dotNo);
		float num2 = 0f;
		num = float.MaxValue;
		float num3 = float.MaxValue;
		int n = -1;
		for (int num4 = peak_1.pkN; num4 >= peak_1.LfDotNo; num4--)
		{
			num2 = dots[num4].Y;
			if (num2 < num)
			{
				num = num2;
			}
		}
		for (int i = peak_1.pkN; i <= para; i++)
		{
			num2 = dots[i].Y;
			if (num2 < num3)
			{
				num3 = num2;
				n = i;
			}
		}
		if (num < num3)
		{
			peak_1.bsStyle = BsStyle.General;
			return false;
		}
		SetPeakLeftAndRightValeDotNo(peak_1, LR.Left);
		peak.bsRtV.dotNo = para;
		peak_1.bsLfV.N = -1;
		peak_1.bsRtV.N = n;
		peak_1.bsHorzY = num3;
		peak_1.tanValue = 0.0;
		return true;
	}

	private bool needProcPeak_General(ref Peak peak_1)
	{
		if (peak_1.bsLfV.N < 0 || peak_1.bsLfV.N < peak_1.bsLfV.dotNo || peak_1.bsRtV.N < 0 || peak_1.bsRtV.N > peak_1.bsRtV.dotNo)
		{
			peak_1.bsLfV.N = peak_1.pkN - 1;
			peak_1.bsRtV.N = peak_1.pkN + 1;
		}
		FindVale_MaxSlopPoint(peak_1.positive, ref peak_1.bsLfV.N, ref peak_1.bsRtV.N, peak_1.bsLfV.dotNo, peak_1.bsRtV.dotNo);
		double num = dots[peak_1.bsRtV.N].Y - dots[peak_1.bsLfV.N].Y;
		double num2 = dots[peak_1.bsRtV.N].X - dots[peak_1.bsLfV.N].X;
		peak_1.tanValue = num / num2;
		if (double.IsNaN(peak_1.tanValue))
		{
			LogMgr.Instance.Write2RunLog("ApplyIntegs.method_54 Error: peak_1.double_0 is NaN。num19:" + num + " num20:" + num2 + " pointF_0.X:" + dots[peak_1.bsRtV.N].X + " pointF_0.Y:" + dots[peak_1.bsRtV.N].Y + " pointF_1.X:" + dots[peak_1.bsLfV.N].X + " pointF_1.Y:" + dots[peak_1.bsLfV.N].Y);
			peak_1.tanValue = 0.0;
		}
		return true;
	}

	private void needProcPeak(Peak peak_1)
	{
		try
		{
			if (peak_1.bsStyle == BsStyle.ForwHorz)
			{
				if (!needProcPeak_ForwHorz(ref peak_1))
				{
					return;
				}
			}
			else if (peak_1.bsStyle == BsStyle.BackHorz)
			{
				if (!needProcPeak_BackHorz(ref peak_1))
				{
					return;
				}
			}
			else if (peak_1.bsStyle == BsStyle.General && !needProcPeak_General(ref peak_1))
			{
				return;
			}
			if (peak_1.bsLfV.N >= 0)
			{
				for (int num = peak_1.bsLfV.N; num > peak_1.bsLfV.dotNo; num--)
				{
					if (enumPeakStateList[num] == EnumPeakState.Clear)
					{
						peak_1.bsLfV.dotNo = num;
						if (peak_1.LfDotNo >= num)
						{
							Array.Resize(ref peak_1.lfTgntYs, peak_1.lfDotNo - num + 1);
						}
						break;
					}
				}
			}
			if (peak_1.bsRtV.N >= 0)
			{
				for (int i = peak_1.bsRtV.N; i < peak_1.bsRtV.dotNo; i++)
				{
					if (enumPeakStateList[i] == EnumPeakState.Clear)
					{
						peak_1.bsRtV.dotNo = i;
						if (peak_1.RtDotNo > i && i - peak_1.rtDotNo + 1 > 0)
						{
							Array.Resize(ref peak_1.rtTgntYs, i - peak_1.rtDotNo + 1);
						}
						break;
					}
				}
			}
			int fromNo = peak_1.FromNo;
			int num2 = peak_1.ToNo;
			if (peak_1.ToNo == 0)
			{
				num2 = peak_1.bsRtV.N;
				peak_1.rtDotNo = num2 - peak_1.FromNo;
			}
			int num3 = num2 - fromNo + 1;
			Array.Resize(ref peak_1.bsYs, num3);
			if (peak_1.bsStyle == BsStyle.ForwHorz || peak_1.bsStyle == BsStyle.BackHorz)
			{
				for (int j = 0; j < num3; j++)
				{
					peak_1.bsYs[j] = peak_1.bsHorzY;
				}
			}
			if (peak_1.bsStyle == BsStyle.General)
			{
				float x = dots[peak_1.bsLfV.N].X;
				float y = dots[peak_1.bsLfV.N].Y;
				double tanValue = peak_1.tanValue;
				for (int k = 0; k < num3; k++)
				{
					peak_1.bsYs[k] = Convert.ToSingle((double)y + tanValue * (double)(dots[fromNo + k].X - x));
				}
			}
			float[] array = new float[num3];
			if (peak_1.positive)
			{
				int num4 = fromNo;
				int num5 = 0;
				while (num4 < num2)
				{
					float fTgntY = 0f;
					if (peak_1.TgntY(num4, ref fTgntY))
					{
						array[num5] = Math.Max(0f, fTgntY - peak_1.bsYs[num5]);
					}
					else
					{
						array[num5] = Math.Max(0f, dots[num4].Y - peak_1.bsYs[num5]);
					}
					num4++;
					num5++;
				}
			}
			else
			{
				int num6 = fromNo;
				int num7 = 0;
				while (num6 < num2)
				{
					array[num7] = Math.Max(0f, peak_1.bsYs[num7] - dots[num6].Y);
					num6++;
					num7++;
				}
			}
			float num8 = 0f;
			int num9 = fromNo;
			int num10 = 0;
			while (num9 < num2)
			{
				num8 += (array[num10] + array[num10 + 1]) * (dots[num9 + 1].X - dots[num9].X) * 30f;
				num9++;
				num10++;
			}
			peak_1.area = Convert.ToSingle(num8);
			peak_1.height = array[peak_1.pkN - fromNo];
			peak_1.pkRT = dots[peak_1.pkN].X;
			peak_1.width = dots[num2].X - dots[fromNo].X;
			if (peak_1.width < 0f)
			{
				int iLastIndex = num2 - 1;
				if (iLastIndex > 0 && fromNo > 0)
				{
					float fLastValue = 0f;
					findTheLastXValue(out iLastIndex, out fLastValue);
					peak_1.width = fLastValue - dots[fromNo].X;
					if (peak_1.width > 0f)
					{
						peak_1.rtDotNo = (peak_1.bsRtV.dotNo = iLastIndex);
					}
					else
					{
						peak_1.width = myfPeakWidth + 1E-05f;
					}
				}
				else
				{
					peak_1.width = myfPeakWidth + 1E-05f;
				}
			}
			float leftDist = 0f;
			float rightDist = 0f;
			CalcLeftAndRightDistance(peak_1, array, 0.5f, ref leftDist, ref rightDist);
			peak_1.WO5 = leftDist + rightDist;
			CalcLeftAndRightDistance(peak_1, array, 0.1f, ref peak_1.float_0, ref peak_1.float_1);
			CalcLeftAndRightDistance(peak_1, array, 0.05f, ref peak_1.a05i, ref peak_1.b05i);
			peak_1.needProc = false;
		}
		catch (Exception ex)
		{
			LogMgr.Instance.LogError("ApplyIntegs.needProcPeak" + ex.Message);
		}
	}

	private void needProcPeak(Peak peak_1, int int1, int int2, float time1, float time2)
	{
		if (peak_1.bsStyle == BsStyle.ForwHorz)
		{
			if (!needProcPeak_ForwHorz(ref peak_1))
			{
				return;
			}
		}
		else if (peak_1.bsStyle == BsStyle.BackHorz)
		{
			if (!needProcPeak_BackHorz(ref peak_1))
			{
				return;
			}
		}
		else if (peak_1.bsStyle == BsStyle.General && !needProcPeak_General(ref peak_1))
		{
			return;
		}
		peak_1.bsLfV.N = int1;
		peak_1.bsRtV.N = int2;
		if (peak_1.bsLfV.N >= 0)
		{
			for (int num = peak_1.bsLfV.N; num > peak_1.bsLfV.dotNo; num--)
			{
				if (enumPeakStateList[num] == EnumPeakState.Clear)
				{
					peak_1.bsLfV.dotNo = num;
					if (peak_1.LfDotNo >= num)
					{
						Array.Resize(ref peak_1.lfTgntYs, peak_1.lfDotNo - num + 1);
					}
					break;
				}
			}
		}
		if (peak_1.bsRtV.N >= 0)
		{
			for (int i = peak_1.bsRtV.N; i < peak_1.bsRtV.dotNo; i++)
			{
				if (enumPeakStateList[i] == EnumPeakState.Clear)
				{
					peak_1.bsRtV.dotNo = i;
					if (peak_1.RtDotNo > i && i - peak_1.rtDotNo + 1 > 0)
					{
						Array.Resize(ref peak_1.rtTgntYs, i - peak_1.rtDotNo + 1);
					}
					break;
				}
			}
		}
		int fromNo = peak_1.FromNo;
		int toNo = peak_1.ToNo;
		fromNo = int1;
		toNo = int2;
		if (peak_1.ToNo == 0)
		{
			toNo = peak_1.bsRtV.N;
			peak_1.rtDotNo = toNo - peak_1.FromNo;
		}
		int num2 = toNo - fromNo + 1;
		Array.Resize(ref peak_1.bsYs, num2);
		if (peak_1.bsStyle == BsStyle.ForwHorz || peak_1.bsStyle == BsStyle.BackHorz)
		{
			for (int j = 0; j < num2; j++)
			{
				peak_1.bsYs[j] = peak_1.bsHorzY;
			}
		}
		if (peak_1.bsStyle == BsStyle.General)
		{
			float x = dots[peak_1.bsLfV.N].X;
			float y = dots[peak_1.bsLfV.N].Y;
			double tanValue = peak_1.tanValue;
			for (int k = 0; k < num2; k++)
			{
				peak_1.bsYs[k] = Convert.ToSingle((double)y + tanValue * (double)(dots[fromNo + k].X - x));
			}
		}
		float[] array = new float[num2];
		if (peak_1.positive)
		{
			int num3 = fromNo;
			int num4 = 0;
			while (num3 < toNo)
			{
				float fTgntY = 0f;
				if (peak_1.TgntY(num3, ref fTgntY))
				{
					array[num4] = Math.Max(0f, fTgntY - peak_1.bsYs[num4]);
				}
				else
				{
					array[num4] = Math.Max(0f, dots[num3].Y - peak_1.bsYs[num4]);
				}
				num3++;
				num4++;
			}
		}
		else
		{
			int num5 = fromNo;
			int num6 = 0;
			while (num5 < toNo)
			{
				array[num6] = Math.Max(0f, peak_1.bsYs[num6] - dots[num5].Y);
				num5++;
				num6++;
			}
		}
		float num7 = 0f;
		int num8 = fromNo;
		int num9 = 0;
		while (num8 < toNo)
		{
			num7 += (array[num9] + array[num9 + 1]) * (dots[num8 + 1].X - dots[num8].X) * 30f;
			num8++;
			num9++;
		}
		num7 = (time2 - time1) * array[peak_1.pkN - fromNo] * 30f;
		peak_1.area = Convert.ToSingle(num7);
		peak_1.height = array[peak_1.pkN - fromNo];
		peak_1.pkRT = dots[peak_1.pkN].X;
		peak_1.width = dots[toNo].X - dots[fromNo].X;
		if (peak_1.width < 0f)
		{
			int iLastIndex = toNo - 1;
			if (iLastIndex > 0 && fromNo > 0)
			{
				float fLastValue = 0f;
				findTheLastXValue(out iLastIndex, out fLastValue);
				peak_1.width = fLastValue - dots[fromNo].X;
				if (peak_1.width > 0f)
				{
					peak_1.rtDotNo = (peak_1.bsRtV.dotNo = iLastIndex);
				}
				else
				{
					peak_1.width = myfPeakWidth + 1E-05f;
				}
			}
			else
			{
				peak_1.width = myfPeakWidth + 1E-05f;
			}
		}
		float leftDist = 0f;
		float rightDist = 0f;
		CalcLeftAndRightDistance(peak_1, array, 0.5f, ref leftDist, ref rightDist);
		peak_1.WO5 = leftDist + rightDist;
		CalcLeftAndRightDistance(peak_1, array, 0.1f, ref peak_1.float_0, ref peak_1.float_1);
		CalcLeftAndRightDistance(peak_1, array, 0.05f, ref peak_1.a05i, ref peak_1.b05i);
		peak_1.needProc = false;
	}

	private bool method_6(int int_2, bool bool_0)
	{
		if (int_2 < 0)
		{
			return false;
		}
		bool flag = false;
		Peak peak = null;
		Peak peak2 = null;
		int num = -1;
		int num2 = -1;
		for (int i = 0; i < PeaksNum; i++)
		{
			if (peak_0[i].RtDotNo == int_2)
			{
				peak = peak_0[i];
				num = i;
				break;
			}
		}
		for (int i = PeaksNum - 1; i >= 0; i--)
		{
			if (peak_0[i].LfDotNo == int_2)
			{
				peak2 = peak_0[i];
				num2 = i;
				break;
			}
		}
		if (num >= 0)
		{
			for (int i = 0; i <= num; i++)
			{
				if ((!bool_0 || (bool_0 && peak_0[i].bsLfV.dotNo == peak.bsLfV.dotNo && peak_0[i].bsRtV.dotNo == peak.bsRtV.dotNo)) && peak_0[i].RtDotNo <= int_2 && peak_0[i].bsRtV.dotNo > int_2)
				{
					int dotNo = peak_0[i].bsRtV.dotNo;
					peak_0[i].bsRtV.dotNo = int_2;
					needProcPeak(peak_0[i]);
					if (peak_0[i].bsRtV.dotNo != dotNo)
					{
						flag = true;
					}
				}
			}
		}
		if (num2 >= 0)
		{
			for (int i = PeaksNum - 1; i >= num2; i--)
			{
				if ((!bool_0 || (bool_0 && peak_0[i].bsLfV.dotNo == peak2.bsLfV.dotNo && peak_0[i].bsRtV.dotNo == peak2.bsRtV.dotNo)) && peak_0[i].LfDotNo >= int_2 && peak_0[i].bsLfV.dotNo < int_2)
				{
					int dotNo = peak_0[i].bsLfV.dotNo;
					peak_0[i].bsLfV.dotNo = int_2;
					needProcPeak(peak_0[i]);
					if (peak_0[i].bsLfV.dotNo != dotNo)
					{
						flag = true;
					}
				}
			}
		}
		if (flag)
		{
			method_62(int_2);
		}
		return flag;
	}

	private void ProcessAllInteg()
	{
		if (Integs.IntegRows.Length < 3)
		{
			return;
		}
		for (int i = 0; i < 3; i++)
		{
			Integs.IntegRows[i].success = true;
		}
		bool success = false;
		for (int i = 3; i < Integs.Count; i++)
		{
			IntegOprtStyle oprtStyle = Integs.IntegRows[i].oprtStyle;
			char c = Integs.IntegRows[i].group;
			float timeA = Integs.IntegRows[i].timeA;
			float timeB = Integs.IntegRows[i].timeB;
			float value = Integs.IntegRows[i].value;
			int dotNo = getDotNo(Math.Min(timeA, timeB));
			int dotNo2 = getDotNo(Math.Max(timeA, timeB));
			Integs.IntegRows[i].success = false;
			if (dotNo != dotNo2)
			{
				switch (oprtStyle)
				{
				case IntegOprtStyle.DtecDelay:
					success = true;
					break;
				case IntegOprtStyle.ResetDtecNeg:
					success = ResetDtecNeg(dotNo, dotNo2, EnumDetectPeakMethod.PeakAndVale);
					break;
				case IntegOprtStyle.ClampNeg:
					success = ClampNeg(dotNo, dotNo2);
					break;
				case IntegOprtStyle.PkWidth:
					success = MarkValeList_ByWidth(dotNo, dotNo2, value);
					break;
				case IntegOprtStyle.PkThreshold:
					success = MarkValeList_ByThreshold(dotNo, dotNo2, value);
					break;
				case IntegOprtStyle.PkAddPosi:
					success = PkAddPosi(dotNo, dotNo2);
					break;
				case IntegOprtStyle.PkAddNeg:
					success = PkAddNeg(dotNo, dotNo2);
					break;
				case IntegOprtStyle.PkCut:
					success = MarkValeList_ByCutPks(dotNo, dotNo2);
					break;
				case IntegOprtStyle.PkHalfWidth:
					success = MarkValeList_ByW05(dotNo, dotNo2, value);
					break;
				case IntegOprtStyle.PkArea:
					success = MarkValeList_ByArea(dotNo, dotNo2, value);
					break;
				case IntegOprtStyle.PkVale:
					dotNo = getDotNo(timeA);
					success = PkVale(dotNo);
					break;
				case IntegOprtStyle.GroupAdd:
					success = GroupAdd(dotNo, dotNo2);
					break;
				case IntegOprtStyle.BsTgnt:
					success = BsTgnt(dotNo, dotNo2, value, Integs.IntegRows[i].value2, Integs.IntegRows[i].value3, Integs.IntegRows[i].value4);
					break;
				case IntegOprtStyle.BsVtV:
					success = BsVtV(dotNo, dotNo2, value);
					break;
				case IntegOprtStyle.BsValley:
					success = BsValley(dotNo, dotNo2);
					break;
				case IntegOprtStyle.BsForwHorz:
					success = BsForwHorz(dotNo, dotNo2, LR.Left);
					break;
				case IntegOprtStyle.BsBackHorz:
					success = BsForwHorz(dotNo, dotNo2, LR.Right);
					break;
				case IntegOprtStyle.BsFrontTgnt:
					success = BsFrontTgnt(dotNo, dotNo2, LR.Left);
					break;
				case IntegOprtStyle.BsTailTgnt:
					success = BsFrontTgnt(dotNo, dotNo2, LR.Right);
					break;
				case IntegOprtStyle.Noise:
					Integs.IntegRows[i].timeA = Math.Min(timeA, timeB);
					Integs.IntegRows[i].timeB = Math.Max(timeA, timeB);
					Integs.IntegRows[i].value = GetYDistance_2Point(dotNo, dotNo2);
					success = true;
					break;
				case IntegOprtStyle.Drift:
				{
					float num = (Integs.IntegRows[i].timeA = Math.Min(timeA, timeB));
					float num2 = (Integs.IntegRows[i].timeB = Math.Max(timeA, timeB));
					float num3 = dots[dotNo2].Y - dots[dotNo].Y;
					Integs.IntegRows[i].value = 60f * num3 / (num2 - num);
					success = true;
					break;
				}
				case IntegOprtStyle.BsTogether:
					success = BsTogether(dotNo, dotNo2);
					break;
				case IntegOprtStyle.SolventPeak:
					dotNo = getDotNo(timeA);
					success = PkAddSolventPeak(dotNo, dotNo2);
					break;
				}
				Integs.IntegRows[i].success = success;
			}
		}
	}

	private bool PkAddPosiHand(int startIdx, int endIdx)
	{
		if (!method_2(ref startIdx, ref endIdx))
		{
			return false;
		}
		float num = float.MinValue;
		int pkN = -1;
		for (int num2 = (startIdx + endIdx) / 2; num2 >= startIdx; num2--)
		{
			if (dots[num2].Y > num)
			{
				num = dots[num2].Y;
				pkN = num2;
			}
		}
		for (int num2 = (startIdx + endIdx) / 2; num2 <= endIdx; num2++)
		{
			if (dots[num2].Y > num)
			{
				num = dots[num2].Y;
				pkN = num2;
			}
		}
		Peak peak = new Peak
		{
			positive = true,
			rtDotNo = -1,
			lfDotNo = -1
		};
		peak.lfDotNo = (peak.bsLfV.dotNo = (peak.bsLfV.N = startIdx));
		peak.rtDotNo = (peak.bsRtV.dotNo = (peak.bsRtV.N = endIdx));
		peak.pkN = pkN;
		qzneedProcPeakHand(peak);
		enumPeakStateList[peak.bsRtV.dotNo] = EnumPeakState.Clear;
		enumPeakStateList[peak.bsLfV.dotNo] = EnumPeakState.Clear;
		int peaksNum = PeaksNum;
		if (peak.area > 0f)
		{
			Array.Resize(ref peak_0, peaksNum + 1);
			peak_0[peaksNum] = peak;
			Array.Sort(peak_0);
			return true;
		}
		return false;
	}

	private bool PkAddPosi4(int startIdx, int endIdx)
	{
		if (!method_2(ref startIdx, ref endIdx))
		{
			return false;
		}
		float num = float.MinValue;
		int num2 = -1;
		for (int num3 = (startIdx + endIdx) / 2; num3 >= startIdx; num3--)
		{
			if (dots[num3].Y > num)
			{
				num = dots[num3].Y;
				num2 = num3;
			}
		}
		for (int num3 = (startIdx + endIdx) / 2; num3 <= endIdx; num3++)
		{
			if (dots[num3].Y > num)
			{
				num = dots[num3].Y;
				num2 = num3;
			}
		}
		for (int i = 0; i < PeaksNum; i++)
		{
			if (peak_0[i].pkN == num2)
			{
				return false;
			}
		}
		Peak peak = new Peak
		{
			positive = true,
			rtDotNo = -1,
			lfDotNo = -1
		};
		if (num2 == startIdx)
		{
			num2 = startIdx + 1;
			peak.lfDotNo = (peak.bsLfV.dotNo = startIdx);
		}
		if (num2 == endIdx)
		{
			num2 = endIdx - 1;
			peak.rtDotNo = (peak.bsRtV.dotNo = endIdx);
		}
		peak.pkN = num2;
		float num4 = float.MaxValue;
		if (peak.lfDotNo == -1)
		{
			for (int num3 = num2 - 1; num3 >= startIdx; num3--)
			{
				if (dots[num3].Y < num4)
				{
					num4 = dots[num3].Y;
					peak.lfDotNo = (peak.bsLfV.dotNo = num3);
				}
				if (enumPeakStateList[num3] != EnumPeakState.None)
				{
					break;
				}
			}
		}
		num4 = float.MaxValue;
		if (peak.rtDotNo == -1)
		{
			for (int num3 = num2 + 1; num3 < endIdx; num3++)
			{
				if (dots[num3].Y < num4)
				{
					num4 = dots[num3].Y;
					peak.rtDotNo = (peak.bsRtV.dotNo = num3);
				}
				if (enumPeakStateList[num3] != EnumPeakState.None)
				{
					break;
				}
			}
		}
		if (peak.lfDotNo < 0 || peak.rtDotNo < 0)
		{
			return false;
		}
		peak.lfDotNo = (peak.bsLfV.dotNo = peak.lfDotNo + 50);
		if (peak.pkN < peak.lfDotNo)
		{
			peak.pkN += 50;
		}
		qzneedProcPeak(peak);
		enumPeakStateList[peak.bsRtV.dotNo] = EnumPeakState.Clear;
		enumPeakStateList[peak.bsLfV.dotNo] = EnumPeakState.Clear;
		int peaksNum = PeaksNum;
		if (peak.area > 0f)
		{
			Array.Resize(ref peak_0, peaksNum + 1);
			peak_0[peaksNum] = peak;
			Array.Sort(peak_0);
			return true;
		}
		return false;
	}

	private void qzneedProcPeak(Peak peak_1)
	{
		if (peak_1.bsStyle == BsStyle.ForwHorz)
		{
			if (!needProcPeak_ForwHorz(ref peak_1))
			{
				return;
			}
		}
		else if (peak_1.bsStyle == BsStyle.BackHorz)
		{
			if (!needProcPeak_BackHorz(ref peak_1))
			{
				return;
			}
		}
		else if (peak_1.bsStyle == BsStyle.General && !qzneedProcPeak_General(ref peak_1))
		{
			return;
		}
		if (peak_1.bsLfV.N >= 0)
		{
			for (int num = peak_1.bsLfV.N; num > peak_1.bsLfV.dotNo; num--)
			{
				if (enumPeakStateList[num] == EnumPeakState.Clear)
				{
					peak_1.bsLfV.dotNo = num;
					if (peak_1.LfDotNo >= num)
					{
						Array.Resize(ref peak_1.lfTgntYs, peak_1.lfDotNo - num + 1);
					}
					break;
				}
			}
		}
		if (peak_1.bsRtV.N >= 0)
		{
			for (int i = peak_1.bsRtV.N; i < peak_1.bsRtV.dotNo; i++)
			{
				if (enumPeakStateList[i] == EnumPeakState.Clear)
				{
					peak_1.bsRtV.dotNo = i;
					if (peak_1.RtDotNo > i && i - peak_1.rtDotNo + 1 > 0)
					{
						Array.Resize(ref peak_1.rtTgntYs, i - peak_1.rtDotNo + 1);
					}
					break;
				}
			}
		}
		int fromNo = peak_1.FromNo;
		if (peak_1.ToNo < peak_1.pkN)
		{
			peak_1.rtDotNo = (peak_1.bsRtV.N = peak_1.pkN + 1);
		}
		int num2 = peak_1.ToNo;
		if (peak_1.ToNo == 0)
		{
			num2 = peak_1.bsRtV.N;
			peak_1.rtDotNo = num2 - peak_1.FromNo;
		}
		int num3 = num2 - fromNo + 1;
		if (num3 < 1)
		{
			num3 = 1;
		}
		Array.Resize(ref peak_1.bsYs, num3);
		if (peak_1.bsStyle == BsStyle.ForwHorz || peak_1.bsStyle == BsStyle.BackHorz)
		{
			for (int j = 0; j < num3; j++)
			{
				peak_1.bsYs[j] = peak_1.bsHorzY;
			}
		}
		if (peak_1.bsStyle == BsStyle.General)
		{
			float x = dots[peak_1.bsLfV.N].X;
			float y = dots[peak_1.bsLfV.N].Y;
			double tanValue = peak_1.tanValue;
			for (int k = 0; k < num3; k++)
			{
				peak_1.bsYs[k] = Convert.ToSingle((double)y + tanValue * (double)(dots[fromNo + k].X - x));
			}
		}
		float[] array = new float[num3];
		if (peak_1.positive)
		{
			int num4 = fromNo;
			int num5 = 0;
			while (num4 < num2)
			{
				float fTgntY = 0f;
				if (peak_1.TgntY(num4, ref fTgntY))
				{
					array[num5] = Math.Max(0f, fTgntY - peak_1.bsYs[num5]);
				}
				else
				{
					array[num5] = Math.Max(0f, dots[num4].Y - peak_1.bsYs[num5]);
				}
				num4++;
				num5++;
			}
		}
		else
		{
			int num6 = fromNo;
			int num7 = 0;
			while (num6 < num2)
			{
				array[num7] = Math.Max(0f, peak_1.bsYs[num7] - dots[num6].Y);
				num6++;
				num7++;
			}
		}
		float num8 = 0f;
		int num9 = fromNo;
		int num10 = 0;
		while (num9 < num2)
		{
			num8 += (array[num10] + array[num10 + 1]) * (dots[num9 + 1].X - dots[num9].X) * 30f;
			num9++;
			num10++;
		}
		peak_1.area = Convert.ToSingle(num8);
		peak_1.height = array[peak_1.pkN - fromNo];
		peak_1.pkRT = dots[peak_1.pkN].X;
		peak_1.width = dots[num2].X - dots[fromNo].X;
		if (peak_1.width < 0f)
		{
			int iLastIndex = num2 - 1;
			if (iLastIndex > 0 && fromNo > 0)
			{
				float fLastValue = 0f;
				findTheLastXValue(out iLastIndex, out fLastValue);
				peak_1.width = fLastValue - dots[fromNo].X;
				if (peak_1.width > 0f)
				{
					peak_1.rtDotNo = (peak_1.bsRtV.dotNo = iLastIndex);
				}
				else
				{
					peak_1.width = myfPeakWidth + 1E-05f;
				}
			}
			else
			{
				peak_1.width = myfPeakWidth + 1E-05f;
			}
		}
		float leftDist = 0f;
		float rightDist = 0f;
		CalcLeftAndRightDistance(peak_1, array, 0.5f, ref leftDist, ref rightDist);
		peak_1.WO5 = leftDist + rightDist;
		CalcLeftAndRightDistance(peak_1, array, 0.1f, ref peak_1.float_0, ref peak_1.float_1);
		CalcLeftAndRightDistance(peak_1, array, 0.05f, ref peak_1.a05i, ref peak_1.b05i);
		peak_1.needProc = false;
	}

	private void qzneedProcPeakHand(Peak peak_1)
	{
		qzneedProcPeak_GeneralHand(ref peak_1);
		if (peak_1.bsLfV.N >= 0)
		{
			for (int num = peak_1.bsLfV.N; num > peak_1.bsLfV.dotNo; num--)
			{
				if (enumPeakStateList[num] == EnumPeakState.Clear)
				{
					peak_1.bsLfV.dotNo = num;
					if (peak_1.LfDotNo >= num)
					{
						Array.Resize(ref peak_1.lfTgntYs, peak_1.lfDotNo - num + 1);
					}
					break;
				}
			}
		}
		if (peak_1.bsRtV.N >= 0)
		{
			for (int i = peak_1.bsRtV.N; i < peak_1.bsRtV.dotNo; i++)
			{
				if (enumPeakStateList[i] == EnumPeakState.Clear)
				{
					peak_1.bsRtV.dotNo = i;
					if (peak_1.RtDotNo > i && i - peak_1.rtDotNo + 1 > 0)
					{
						Array.Resize(ref peak_1.rtTgntYs, i - peak_1.rtDotNo + 1);
					}
					break;
				}
			}
		}
		int fromNo = peak_1.FromNo;
		if (peak_1.ToNo < peak_1.pkN)
		{
			peak_1.rtDotNo = (peak_1.bsRtV.N = peak_1.pkN + 1);
		}
		int num2 = peak_1.ToNo;
		if (peak_1.ToNo == 0)
		{
			num2 = peak_1.bsRtV.N;
			peak_1.rtDotNo = num2 - peak_1.FromNo;
		}
		int num3 = num2 - fromNo + 1;
		if (num3 < 1)
		{
			num3 = 1;
		}
		Array.Resize(ref peak_1.bsYs, num3);
		if (peak_1.bsStyle == BsStyle.ForwHorz || peak_1.bsStyle == BsStyle.BackHorz)
		{
			for (int j = 0; j < num3; j++)
			{
				peak_1.bsYs[j] = peak_1.bsHorzY;
			}
		}
		if (peak_1.bsStyle == BsStyle.General)
		{
			float x = dots[peak_1.bsLfV.N].X;
			float y = dots[peak_1.bsLfV.N].Y;
			double tanValue = peak_1.tanValue;
			for (int k = 0; k < num3; k++)
			{
				peak_1.bsYs[k] = Convert.ToSingle((double)y + tanValue * (double)(dots[fromNo + k].X - x));
			}
		}
		float[] array = new float[num3];
		if (peak_1.positive)
		{
			int num4 = fromNo;
			int num5 = 0;
			while (num4 < num2)
			{
				float num6 = 0f;
				array[num5] = Math.Max(0f, dots[num4].Y - peak_1.bsYs[num5]);
				num4++;
				num5++;
			}
		}
		else
		{
			int num7 = fromNo;
			int num8 = 0;
			while (num7 < num2)
			{
				array[num8] = Math.Max(0f, peak_1.bsYs[num8] - dots[num7].Y);
				num7++;
				num8++;
			}
		}
		float num9 = 0f;
		int num10 = fromNo;
		int num11 = 0;
		while (num10 < num2)
		{
			num9 += (array[num11] + array[num11 + 1]) * (dots[num10 + 1].X - dots[num10].X) * 30f;
			num10++;
			num11++;
		}
		peak_1.area = Convert.ToSingle(num9);
		peak_1.height = array[peak_1.pkN - fromNo];
		peak_1.pkRT = dots[peak_1.pkN].X;
		peak_1.width = dots[num2].X - dots[fromNo].X;
		if (peak_1.width < 0f)
		{
			int iLastIndex = num2 - 1;
			if (iLastIndex > 0 && fromNo > 0)
			{
				float fLastValue = 0f;
				findTheLastXValue(out iLastIndex, out fLastValue);
				peak_1.width = fLastValue - dots[fromNo].X;
				if (peak_1.width > 0f)
				{
					peak_1.rtDotNo = (peak_1.bsRtV.dotNo = iLastIndex);
				}
				else
				{
					peak_1.width = myfPeakWidth + 1E-05f;
				}
			}
			else
			{
				peak_1.width = myfPeakWidth + 1E-05f;
			}
		}
		float leftDist = 0f;
		float rightDist = 0f;
		CalcLeftAndRightDistance(peak_1, array, 0.5f, ref leftDist, ref rightDist);
		peak_1.WO5 = leftDist + rightDist;
		CalcLeftAndRightDistance(peak_1, array, 0.1f, ref peak_1.float_0, ref peak_1.float_1);
		CalcLeftAndRightDistance(peak_1, array, 0.05f, ref peak_1.a05i, ref peak_1.b05i);
		peak_1.needProc = false;
	}

	private bool qzneedProcPeak_General(ref Peak peak_1)
	{
		if (peak_1.bsLfV.N < 0 || peak_1.bsLfV.N < peak_1.bsLfV.dotNo || peak_1.bsRtV.N < 0 || peak_1.bsRtV.N > peak_1.bsRtV.dotNo)
		{
			peak_1.bsLfV.N = peak_1.pkN - 1;
			peak_1.bsRtV.N = peak_1.pkN + 1;
		}
		FindVale_MaxSlopPoint(peak_1.positive, ref peak_1.bsLfV.N, ref peak_1.bsRtV.N, peak_1.bsLfV.dotNo, peak_1.bsRtV.dotNo);
		peak_1.bsLfV.N = peak_1.bsLfV.dotNo;
		peak_1.bsRtV.N = peak_1.bsRtV.dotNo;
		double num = dots[peak_1.bsRtV.N].Y - dots[peak_1.bsLfV.N].Y;
		double num2 = dots[peak_1.bsRtV.N].X - dots[peak_1.bsLfV.N].X;
		double num3 = (peak_1.tanValue = num / num2);
		float x = dots[peak_1.bsLfV.N].X;
		float y = dots[peak_1.bsLfV.N].Y;
		int num4 = peak_1.bsRtV.N - peak_1.bsLfV.N;
		List<int> list = new List<int>(0);
		List<float> list2 = new List<float>(0);
		List<float> list3 = new List<float>(0);
		for (int i = 0; i < num4; i++)
		{
			float num5 = Convert.ToSingle((double)y + num3 * (double)(dots[peak_1.bsLfV.N + i].X - x));
			if (num5 > dots[peak_1.bsLfV.N + i].Y)
			{
				list3.Add(num5);
				list.Add(peak_1.bsLfV.N + i);
				list2.Add(dots[peak_1.bsLfV.N + i].Y);
			}
		}
		if (list3.Count > 0)
		{
			int num6 = peak_1.bsRtV.N - 1;
			int num7 = list3.Count - 1;
			int num8 = 0;
			int num9 = 0;
			num8 = (num9 = list[num7 - 1]);
			while (num7 >= 0)
			{
				num9 = list[num7];
				if (num8 - num9 > 1)
				{
					peak_1.lfDotNo = (peak_1.bsLfV.dotNo = num9);
					peak_1.bsLfV.N = num9;
					peak_1.rtDotNo = (peak_1.bsRtV.dotNo = num8);
					peak_1.bsRtV.N = num8;
					peak_1.pkN = (num9 + num8) / 2;
					break;
				}
				num8 = num9;
				if (num7 == 0)
				{
					peak_1.rtDotNo = (peak_1.bsRtV.dotNo = num8);
					peak_1.bsRtV.N = num8;
					break;
				}
				num7--;
			}
		}
		if (double.IsNaN(peak_1.tanValue))
		{
			LogMgr.Instance.Write2RunLog("ApplyIntegs.method_54 Error: peak_1.double_0 is NaN。num19:" + num + " num20:" + num2 + " pointF_0.X:" + dots[peak_1.bsRtV.N].X + " pointF_0.Y:" + dots[peak_1.bsRtV.N].Y + " pointF_1.X:" + dots[peak_1.bsLfV.N].X + " pointF_1.Y:" + dots[peak_1.bsLfV.N].Y);
			peak_1.tanValue = 0.0;
		}
		return true;
	}

	private bool qzneedProcPeak_GeneralHand(ref Peak peak_1)
	{
		if (peak_1.bsLfV.N < 0 || peak_1.bsLfV.N < peak_1.bsLfV.dotNo || peak_1.bsRtV.N < 0 || peak_1.bsRtV.N > peak_1.bsRtV.dotNo)
		{
			peak_1.bsLfV.N = peak_1.pkN - 1;
			peak_1.bsRtV.N = peak_1.pkN + 1;
		}
		peak_1.bsLfV.N = peak_1.bsLfV.dotNo;
		peak_1.bsRtV.N = peak_1.bsRtV.dotNo;
		double num = dots[peak_1.bsRtV.N].Y - dots[peak_1.bsLfV.N].Y;
		double num2 = dots[peak_1.bsRtV.N].X - dots[peak_1.bsLfV.N].X;
		double num3 = (peak_1.tanValue = num / num2);
		if (double.IsNaN(peak_1.tanValue))
		{
			LogMgr.Instance.Write2RunLog("ApplyIntegs.method_54 Error: peak_1.double_0 is NaN。num19:" + num + " num20:" + num2 + " pointF_0.X:" + dots[peak_1.bsRtV.N].X + " pointF_0.Y:" + dots[peak_1.bsRtV.N].Y + " pointF_1.X:" + dots[peak_1.bsLfV.N].X + " pointF_1.Y:" + dots[peak_1.bsLfV.N].Y);
			peak_1.tanValue = 0.0;
		}
		return true;
	}

	public bool qzPkAddPosi3(int startIdx, int endIdx)
	{
		return qzResetDtecNeg(startIdx, endIdx, EnumDetectPeakMethod.OnlyPeak);
	}

	private bool qzResetDtecNeg(int startIdx, int endIdx, EnumDetectPeakMethod peakMethod)
	{
		float num = 0.001f;
		SetMyTickCount();
		if (endIdx - startIdx < 20)
		{
			return false;
		}
		if (!qzMarkPeakAndValuePoint(startIdx, endIdx, peakMethod))
		{
			return false;
		}
		for (int i = 3; i < Integs.Count; i++)
		{
			IntegOprtStyle oprtStyle = Integs.IntegRows[i].oprtStyle;
			float timeA = Integs.IntegRows[i].timeA;
			float timeB = Integs.IntegRows[i].timeB;
			float value = Integs.IntegRows[i].value;
			int dotNo = getDotNo(Math.Min(timeA, timeB));
			int dotNo2 = getDotNo(Math.Max(timeA, timeB));
			if (dotNo != dotNo2)
			{
				IntegOprtStyle integOprtStyle = oprtStyle;
				if (integOprtStyle == IntegOprtStyle.PkVale)
				{
					dotNo = getDotNo(timeA);
					dotNo2 = getDotNo(timeB);
					method_11(dotNo, dotNo2);
				}
			}
		}
		Peak[] array = qzDetectPeaks(startIdx, endIdx);
		if (array == null || array.Length < 1)
		{
			return false;
		}
		for (int i = 0; i < array.Length; i++)
		{
			if (array[i].width < myfPeakWidth && SimpleIsolatedPeak(array[i]))
			{
				DeletePeak(array[i]);
				array[i--] = array[array.Length - 1];
				Array.Resize(ref array, array.Length - 1);
			}
		}
		for (int i = 0; i < array.Length; i++)
		{
			array[i].startT = dots[array[i].FromNo].X;
			array[i].endT = dots[array[i].ToNo].X;
			array[i].startV = dots[array[i].FromNo].Y;
			array[i].endV = dots[array[i].ToNo].Y;
		}
		for (int i = 0; i < array.Length; i++)
		{
			needProcPeak(array[i]);
		}
		SetPeakTgntYsList(array, Integs.IniTgntAreaF, Integs.IniTgntSlopeF, Integs.IniTgntLfF, Integs.IniTgntRtF);
		valeToVale(array, Integs.IniVtVSlope);
		for (int i = 0; i < array.Length; i++)
		{
			if (!array[i].BsTogether && (array[i].width < myfPeakWidth || array[i].height < num))
			{
				DeletePeak(array[i]);
			}
		}
		Array.Sort(peak_0);
		if (peak_0.Length < 1)
		{
			return false;
		}
		if (peak_0.Length > 1)
		{
			int num2 = array.Length;
			for (int j = 1; j < array.Length; j++)
			{
				if (array[j - 1].area > array[j].area)
				{
					Peak peak = array[j - 1];
					array[j - 1] = array[j];
					array[j] = peak;
				}
			}
			for (int k = 0; k < array.Length - 1; k++)
			{
				DeletePeak(array[k]);
			}
			Array.Sort(peak_0);
		}
		return true;
	}

	private Peak[] qzDetectPeaks(int startIdx, int endIdx)
	{
		int num = 0;
		int num2 = 0;
		for (int i = startIdx; i < dotLength; i++)
		{
			if (enumPeakStateList[i] == EnumPeakState.Peak || enumPeakStateList[i] == EnumPeakState.Vale)
			{
				num++;
				if (startIdx <= i && i <= endIdx)
				{
					num2++;
				}
			}
		}
		if (num == 0)
		{
			num = PeaksNum;
		}
		Peak[] array = new Peak[num];
		num = 0;
		for (int j = 0; j < PeaksNum; j++)
		{
			if (peak_0[j].pkN < startIdx || peak_0[j].pkN > endIdx)
			{
				if (num + 1 > array.Length)
				{
					Array.Resize(ref array, array.Length + 1);
				}
				array[num++] = peak_0[j];
			}
		}
		Peak[] array2 = new Peak[num2];
		num2 = 0;
		for (int k = startIdx; k <= endIdx; k++)
		{
			if (enumPeakStateList[k] != EnumPeakState.Peak && enumPeakStateList[k] != EnumPeakState.Vale)
			{
				continue;
			}
			if (num + 1 > array.Length)
			{
				Array.Resize(ref array, array.Length + 1);
			}
			Peak peak = new Peak();
			peak.positive = enumPeakStateList[k] == EnumPeakState.Peak;
			peak.pkN = k;
			int num3 = -1;
			for (int num4 = k - 1; num4 >= 0; num4--)
			{
				if (enumPeakStateList[num4] == EnumPeakState.Clear)
				{
					num3 = num4;
					break;
				}
			}
			if (num3 == -1)
			{
				num3 = startIdx + 1;
			}
			peak.lfDotNo = (peak.bsLfV.dotNo = num3);
			peak.lfTgntYs[0] = dots[num3].Y;
			int num5 = -1;
			for (int num4 = k + 1; num4 < dotLength; num4++)
			{
				if (enumPeakStateList[num4] == EnumPeakState.Clear)
				{
					num5 = num4;
					break;
				}
			}
			if (num5 == -1)
			{
				num5 = endIdx - 1;
			}
			peak.rtDotNo = (peak.bsRtV.dotNo = num5);
			peak.rtTgntYs[0] = dots[num5].Y;
			peak.bsRtV.N = -1;
			peak.bsLfV.N = -1;
			peak.width = dots[num5].X - dots[num3].X;
			if (peak.width < 0f)
			{
				if (num3 > 0 && num5 > 0)
				{
					int iLastIndex = num5 - 1;
					float fLastValue = 0f;
					findTheLastXValue(out iLastIndex, out fLastValue);
					peak.width = fLastValue - dots[num3].X;
					if (peak.width > 0f)
					{
						peak.rtDotNo = (peak.bsRtV.dotNo = iLastIndex);
					}
					else
					{
						peak.width = myfPeakWidth + 1E-05f;
					}
				}
				if (peak.width < 0f)
				{
					peak.width = myfPeakWidth + 1E-05f;
				}
			}
			array[num++] = peak;
			array2[num2++] = peak;
		}
		Array.Sort(array);
		peak_0 = array;
		return array2;
	}

	private bool qzMarkPeakAndValuePoint(int startIndex, int endIndex, EnumDetectPeakMethod peakMethod)
	{
		for (int num = endIndex - 1; num >= startIndex; num--)
		{
			if (enumPeakStateList[num] == EnumPeakState.Peak || enumPeakStateList[num] == EnumPeakState.Vale)
			{
				return false;
			}
			if (enumPeakStateList[num] == EnumPeakState.Clear)
			{
				int num2 = num - 1;
				while (num2 >= 0 && enumPeakStateList[num2] != EnumPeakState.Peak && enumPeakStateList[num2] != EnumPeakState.Vale)
				{
					if (enumPeakStateList[num2] == EnumPeakState.Clear)
					{
						enumPeakStateList[num] = EnumPeakState.None;
					}
					num2--;
				}
				break;
			}
		}
		for (int i = startIndex + 1; i < endIndex; i++)
		{
			if (enumPeakStateList[i] == EnumPeakState.Peak || enumPeakStateList[i] == EnumPeakState.Vale)
			{
				return false;
			}
			if (enumPeakStateList[i] != EnumPeakState.Clear)
			{
				continue;
			}
			for (int j = i + 1; j < dotLength && enumPeakStateList[j] != EnumPeakState.Peak && enumPeakStateList[j] != EnumPeakState.Vale; j++)
			{
				if (enumPeakStateList[j] == EnumPeakState.Clear)
				{
					enumPeakStateList[i] = EnumPeakState.None;
				}
			}
			break;
		}
		if (peakMethod == EnumDetectPeakMethod.PeakAndVale)
		{
			for (int k = startIndex; k <= endIndex; k++)
			{
				fDistanceList[k] = GetDistance_3Point(startIndex, endIndex, k);
			}
		}
		for (int l = startIndex; l <= endIndex; l++)
		{
			qzMarkPeakAndValuePoint(l, peakMethod);
		}
		enumPeakStateList[endIndex] = EnumPeakState.None;
		enumPeakStateList[startIndex] = EnumPeakState.None;
		ExcludePointForPeak(startIndex, endIndex);
		if (!qzFindVvByPeak(startIndex, endIndex))
		{
			return false;
		}
		return true;
	}

	private bool qzIsPeakPoint(int indexPeak)
	{
		float y = dots[indexPeak].Y;
		float num = Math.Max(0.0001f, myfThreshold);
		num = 0.001f;
		bool flag = false;
		bool flag2 = false;
		int num2 = Math.Min(4000, dotLength);
		for (int i = 1; i < num2; i++)
		{
			int num3 = indexPeak - i;
			int num4 = indexPeak + i;
			if (!flag && 0 <= num3 && num3 < dotLength)
			{
				if (dots[num3].Y > y)
				{
					return false;
				}
				if (dots[num3].Y < y - num)
				{
					flag = true;
				}
			}
			if (!flag2 && 0 <= num4 && num4 < dotLength)
			{
				if (dots[num4].Y > y)
				{
					return false;
				}
				if (dots[num4].Y < y - num)
				{
					flag2 = true;
				}
			}
			if (flag && flag2)
			{
				return true;
			}
		}
		return false;
	}

	private void qzMarkPeakAndValuePoint(int index, EnumDetectPeakMethod peakMethod)
	{
		float num = 0.001f;
		enumPeakStateList[index] = EnumPeakState.None;
		switch (peakMethod)
		{
		case EnumDetectPeakMethod.OnlyPeak:
			if (qzIsPeakPoint(index))
			{
				enumPeakStateList[index] = EnumPeakState.Peak;
			}
			break;
		case EnumDetectPeakMethod.PeakAndVale:
			if (qzIsPeakPoint(index) && dots[index].Y > fDistanceList[index] + myfThreshold)
			{
				enumPeakStateList[index] = EnumPeakState.Peak;
			}
			if (IsValePoint(index) && dots[index].Y < fDistanceList[index] - myfThreshold)
			{
				enumPeakStateList[index] = EnumPeakState.Vale;
			}
			break;
		}
	}

	private bool qzFindVvByPeak(int startIndex, int endIndex)
	{
		for (int i = startIndex; i <= endIndex; i++)
		{
			if (0 < i && i < dotLength && enumPeakStateList[i] != EnumPeakState.None && enumPeakStateList[i - 1] != EnumPeakState.None)
			{
				return false;
			}
		}
		enumPeakStateList[endIndex] = EnumPeakState.None;
		enumPeakStateList[startIndex] = EnumPeakState.None;
		myfMaxThreshold = -1f;
		myfMaxPeakWidth = -1f;
		int val = Convert.ToInt32((double)(persentOfLength * myfPeakWidth) * 0.5);
		int hwN = Math.Max(1, val);
		VV leftVV = new VV();
		VV rightVV = new VV();
		VV lastVV = new VV();
		bool flag = false;
		Integs.PkSlope = Math.Max(0.0001f, Integs.PkSlope);
		for (int j = startIndex; j <= endIndex; j++)
		{
			bool flag2 = enumPeakStateList[j] == EnumPeakState.Peak;
			bool flag3 = enumPeakStateList[j] == EnumPeakState.Vale;
			if (flag2)
			{
				qzFindVvByPeak_ProcessPositivePeak(startIndex, endIndex, j, hwN, ref leftVV, ref rightVV, ref lastVV);
				if (leftVV.index < 0)
				{
					leftVV.vs_0 = VS.V;
					leftVV.index = startIndex + 1;
				}
				if (rightVV.index < 0)
				{
					rightVV.vs_0 = VS.V;
					rightVV.index = endIndex;
				}
				while (true)
				{
					IL_0162:
					rightVV.vs_0 = VS.V;
					rightVV.index--;
					double num = (dots[rightVV.index].Y - dots[leftVV.index].Y) / (dots[rightVV.index].X - dots[leftVV.index].X);
					float x = dots[leftVV.index].X;
					float y = dots[leftVV.index].Y;
					int num2 = rightVV.index - leftVV.index;
					List<int> list = new List<int>(0);
					List<float> list2 = new List<float>(0);
					List<float> list3 = new List<float>(0);
					for (int k = 0; k < num2; k++)
					{
						float num3 = Convert.ToSingle((double)y + num * (double)(dots[leftVV.index + k].X - x));
						if (num3 >= dots[leftVV.index + k].Y)
						{
							list3.Add(num3);
							list.Add(leftVV.index + k);
							list2.Add(dots[leftVV.index + k].Y);
						}
					}
					int num4 = endIndex - 1;
					int num5 = list3.Count - 1;
					int num6 = 0;
					int num7 = 0;
					if (num5 <= 0)
					{
						break;
					}
					num6 = (num7 = list[num5 - 1]);
					while (num5 > 0)
					{
						num7 = list[num5];
						if (num6 - num7 > 1)
						{
							goto IL_0162;
						}
						num5--;
					}
					break;
				}
			}
			else if (flag3)
			{
				FindVvByPeak_ProcessNegativePeak(startIndex, endIndex, j, hwN, ref leftVV, ref rightVV, ref lastVV);
			}
			if (!(flag2 || flag3))
			{
				continue;
			}
			if (leftVV.index >= 0 && rightVV.index >= 0)
			{
				if (lastVV.index >= 0 && IsValidePeak(lastVV.index, leftVV.index))
				{
					enumPeakStateList[lastVV.index] = EnumPeakState.None;
					if (flag)
					{
						if (flag2)
						{
							int maxYIndex_2Point = GetMaxYIndex_2Point(lastVV.index, leftVV.index, bool_0: true);
							getVV(ref leftVV, dots, maxYIndex_2Point, float.NaN, hwN);
						}
						else
						{
							int maxYIndex_2Point = (lastVV.index + leftVV.index) / 2;
							getVV(ref leftVV, dots, maxYIndex_2Point, float.NaN, hwN);
						}
					}
					else if (flag2)
					{
						int maxYIndex_2Point = (lastVV.index + leftVV.index) / 2;
						getVV(ref leftVV, dots, maxYIndex_2Point, float.NaN, hwN);
					}
					else
					{
						int maxYIndex_2Point = GetMinYIndex_2Point(lastVV.index, leftVV.index, bool_0: true);
						getVV(ref leftVV, dots, maxYIndex_2Point, float.NaN, hwN);
					}
				}
				enumPeakStateList[rightVV.index] = EnumPeakState.Clear;
				enumPeakStateList[leftVV.index] = EnumPeakState.Clear;
				lastVV = rightVV.Copy();
				flag = flag2;
			}
			else
			{
				enumPeakStateList[j] = EnumPeakState.None;
			}
		}
		return true;
	}

	private void qzFindVvByPeak_ProcessPositivePeak(int startIndex, int endIndex, int i, int hwN, ref VV leftVV, ref VV rightVV, ref VV lastVV)
	{
		float num = 0.001f;
		VV vv_ = new VV();
		float y = dots[i].Y;
		rightVV.index = -1;
		leftVV.index = -1;
		float num2 = y;
		for (int num3 = i - 1; num3 >= startIndex; num3--)
		{
			if (enumPeakStateList[num3] == EnumPeakState.None)
			{
				qzgetVV(ref vv_, dots, num3, y, hwN);
				bool flag = true;
				num2 = Math.Min(num2, vv_.Y);
				if (vv_.Y > num2 + num)
				{
					break;
				}
				if (vv_.vs_0 == VS.PL || vv_.vs_0 == VS.V)
				{
					if (leftVV.index < 0)
					{
						leftVV = vv_.Copy();
					}
					else if (vv_.Y < leftVV.Y && IsValidPeak2(vv_.index, leftVV.index))
					{
						leftVV = vv_.Copy();
					}
				}
			}
			else
			{
				if (enumPeakStateList[num3] == EnumPeakState.Peak || enumPeakStateList[num3] == EnumPeakState.Vale)
				{
					break;
				}
				if (enumPeakStateList[num3] == EnumPeakState.Clear)
				{
					if (leftVV.index == -1)
					{
						if (dots[num3].Y < y - num)
						{
							leftVV = lastVV.Copy();
						}
					}
					else if (dots[num3].Y < leftVV.Y)
					{
						leftVV = lastVV.Copy();
					}
					break;
				}
			}
		}
		num2 = y;
		if (leftVV.index < 0)
		{
			return;
		}
		for (int num3 = i + 1; num3 <= endIndex; num3++)
		{
			if (enumPeakStateList[num3] == EnumPeakState.None)
			{
				qzgetVV(ref vv_, dots, num3, y, hwN);
				bool flag2 = true;
				num2 = Math.Min(num2, vv_.Y);
				if (vv_.Y > num2 + num)
				{
					break;
				}
				if (vv_.vs_0 == VS.PR || vv_.vs_0 == VS.V)
				{
					if (rightVV.index < 0)
					{
						rightVV = vv_.Copy();
					}
					else if (vv_.Y < rightVV.Y && IsValidPeak2(vv_.index, rightVV.index))
					{
						rightVV = vv_.Copy();
					}
				}
			}
			else
			{
				if (enumPeakStateList[num3] == EnumPeakState.Peak)
				{
					qzgetVV(ref rightVV, dots, GetMaxYIndex_2Point(i, num3, bool_0: false), y, hwN);
					break;
				}
				if (enumPeakStateList[num3] == EnumPeakState.Vale)
				{
					qzgetVV(ref rightVV, dots, GetMinAvgDisYIndex_2Point(i, num3, bool_0: false), y, hwN);
					break;
				}
				if (enumPeakStateList[num3] == EnumPeakState.Clear)
				{
					break;
				}
			}
		}
	}

	public void qzgetVV(ref VV vv_0, PointF[] dots, int dotNo, float pnY, int hwN)
	{
		qzgetVV(ref vv_0, dots, dots.Length, dotNo, pnY, hwN);
	}

	public void qzgetVV(ref VV vv_0, PointF[] dots, int dotsLength, int dotNo, float pnY, int hwN)
	{
		float num = 0.001f;
		vv_0.vs_0 = VS.None;
		vv_0.X = dots[dotNo].X;
		float num2 = (vv_0.Y = dots[dotNo].Y);
		if (!float.IsNaN(pnY) && !(Math.Abs(num2 - pnY) >= num))
		{
			return;
		}
		vv_0.index = dotNo;
		int num3 = 0;
		float num4 = 0f;
		int num5 = dotNo - 1;
		while (num5 >= 0 && num3 < hwN)
		{
			num4 += dots[num5].Y;
			if (enumPeakStateList.Length != 0 && enumPeakStateList[num5] != EnumPeakState.None)
			{
				break;
			}
			num5--;
			num3++;
		}
		if (num3 != 0)
		{
			num4 /= (float)num3;
		}
		float num6 = ((num3 != 0) ? (num4 - num2) : 0f);
		float num7 = 0f;
		num3 = 0;
		num5 = dotNo + 1;
		while (num5 < dotsLength && num3 < hwN)
		{
			num7 += dots[num5].Y;
			if (enumPeakStateList.Length != 0 && enumPeakStateList[num5] != EnumPeakState.None)
			{
				break;
			}
			num5++;
			num3++;
		}
		if (num3 != 0)
		{
			num7 /= (float)num3;
		}
		float num8 = ((num3 != 0) ? (num7 - num2) : 0f);
		if (num6 > 0.0001f)
		{
			if (num8 > 0.0001f)
			{
				vv_0.vs_0 = VS.V;
			}
			else if (-0.0001f <= num8 && num8 <= 0.0001f)
			{
				vv_0.vs_0 = VS.PR;
			}
			else
			{
				vv_0.vs_0 = VS.None;
			}
		}
		else if (-0.0001f <= num6 && num6 <= 0.0001f)
		{
			if (num8 > 0.0001f)
			{
				vv_0.vs_0 = VS.PL;
			}
			else if (-0.0001f <= num8 && num8 <= 0.0001f)
			{
				vv_0.vs_0 = VS.None;
			}
			else
			{
				vv_0.vs_0 = VS.NL;
			}
		}
		else if (num8 > 0.0001f)
		{
			vv_0.vs_0 = VS.None;
		}
		else if (-0.0001f <= num8 && num8 <= 0.0001f)
		{
			vv_0.vs_0 = VS.NR;
		}
		else
		{
			vv_0.vs_0 = VS.A;
		}
		vv_0.value = Math.Abs(num6 + num8);
	}

	private bool GroupAdd(int startIdx, int endIdx)
	{
		Peak[] peaks_2Point = GetPeaks_2Point(startIdx, endIdx);
		if (peaks_2Point.Length == 0)
		{
			return false;
		}
		peaks_2Point[0].rtDotNo = peaks_2Point[peaks_2Point.Length - 1].rtDotNo;
		peaks_2Point[0].bsRtV = peaks_2Point[peaks_2Point.Length - 1].bsRtV;
		for (int i = 1; i < peaks_2Point.Length; i++)
		{
			peaks_2Point[0].area += peaks_2Point[i].area;
			peaks_2Point[0].height += peaks_2Point[i].height;
		}
		for (int j = 0; j < peaks_2Point.Length; j++)
		{
			Peak peak = peaks_2Point[j];
			if (j != 0)
			{
				peak.area += peaks_2Point[j].area;
				enumPeakStateList[peak.pkN] = EnumPeakState.None;
			}
			int lfDotNo;
			if ((j != 0) & IsolatedVale(lfDotNo = peak.lfDotNo, peak))
			{
				enumPeakStateList[lfDotNo] = EnumPeakState.None;
			}
			if ((j != peaks_2Point.Length - 1) & IsolatedVale(lfDotNo = peak.rtDotNo, peak))
			{
				enumPeakStateList[lfDotNo] = EnumPeakState.None;
			}
			if ((j != 0) & (peak.bsLfV.dotNo != peak.lfDotNo && IsolatedVale(lfDotNo = peak.bsLfV.dotNo, peak)))
			{
				enumPeakStateList[lfDotNo] = EnumPeakState.None;
			}
			if ((j != peaks_2Point.Length - 1) & (peak.bsRtV.dotNo != peak.rtDotNo && IsolatedVale(lfDotNo = peak.bsRtV.dotNo, peak)))
			{
				enumPeakStateList[lfDotNo] = EnumPeakState.None;
			}
			if (j == 0)
			{
				continue;
			}
			for (int k = 0; k < PeaksNum; k++)
			{
				if (peak_0[k] == peak)
				{
					peak_0[k] = peak_0[PeaksNum - 1];
					Array.Resize(ref peak_0, PeaksNum - 1);
					break;
				}
			}
		}
		Array.Sort(peak_0);
		MarkValeList("pkCutPks");
		return true;
	}

	private bool PkVale(int int_2)
	{
		bool flag = false;
		int num = -1;
		for (int i = 0; i < peak_0.Length; i++)
		{
			if (int_2 > peak_0[i].lfDotNo && int_2 < peak_0[i].rtDotNo)
			{
				flag = true;
				num = i;
				break;
			}
		}
		if (!flag)
		{
			return false;
		}
		enumPeakStateList[int_2] = EnumPeakState.Clear;
		enumPeakStateList[int_2 + 1] = EnumPeakState.Clear;
		float num2 = float.MinValue;
		int num3 = -1;
		int dotNo = peak_0[num].bsLfV.dotNo;
		int dotNo2 = peak_0[num].bsRtV.dotNo;
		for (int num4 = (dotNo + int_2) / 2; num4 >= dotNo; num4--)
		{
			if (dots[num4].Y > num2)
			{
				num2 = dots[num4].Y;
				num3 = num4;
			}
		}
		for (int num4 = (dotNo + int_2) / 2; num4 <= int_2; num4++)
		{
			if (dots[num4].Y > num2)
			{
				num2 = dots[num4].Y;
				num3 = num4;
			}
		}
		Peak peak = new Peak
		{
			positive = true,
			rtDotNo = -1,
			lfDotNo = -1
		};
		if (num3 == dotNo)
		{
			num3 = dotNo + 1;
			peak.lfDotNo = (peak.bsLfV.dotNo = dotNo);
		}
		if (num3 == int_2)
		{
			num3 = int_2 - 1;
			peak.rtDotNo = (peak.bsRtV.dotNo = int_2);
		}
		peak.pkN = num3;
		float num5 = float.MaxValue;
		if (peak.lfDotNo == -1)
		{
			for (int num4 = num3 - 1; num4 >= dotNo; num4--)
			{
				if (dots[num4].Y < num5)
				{
					num5 = dots[num4].Y;
					peak.lfDotNo = (peak.bsLfV.dotNo = num4);
				}
				if (enumPeakStateList[num4] != EnumPeakState.None)
				{
					break;
				}
			}
		}
		num5 = float.MaxValue;
		if (peak.rtDotNo == -1)
		{
			for (int num4 = num3 + 1; num4 < int_2; num4++)
			{
				if (dots[num4].Y < num5)
				{
					num5 = dots[num4].Y;
					peak.rtDotNo = (peak.bsRtV.dotNo = num4);
				}
				if (enumPeakStateList[num4] != EnumPeakState.None)
				{
					break;
				}
			}
		}
		if (peak.lfDotNo < 0 || peak.rtDotNo < 0)
		{
			return false;
		}
		peak.bsStyle = BsStyle.BackHorz;
		needProcPeak(peak);
		enumPeakStateList[peak.bsRtV.dotNo] = EnumPeakState.Clear;
		enumPeakStateList[peak.bsLfV.dotNo] = EnumPeakState.Clear;
		peak_0[num] = peak;
		num3 = -1;
		num2 = float.MinValue;
		for (int num4 = (int_2 + dotNo2) / 2; num4 >= int_2; num4--)
		{
			if (dots[num4].Y > num2)
			{
				num2 = dots[num4].Y;
				num3 = num4;
			}
		}
		for (int num4 = (dotNo2 + int_2) / 2; num4 <= dotNo2; num4++)
		{
			if (dots[num4].Y > num2)
			{
				num2 = dots[num4].Y;
				num3 = num4;
			}
		}
		Peak peak2 = new Peak
		{
			positive = true
		};
		peak.rtDotNo = -1;
		peak2.lfDotNo = -1;
		if (num3 == int_2)
		{
			num3 = int_2 + 1;
			peak2.lfDotNo = (peak2.bsLfV.dotNo = int_2);
		}
		if (num3 == dotNo2)
		{
			num3 = int_2 - 1;
			peak2.rtDotNo = (peak2.bsRtV.dotNo = dotNo2);
		}
		peak2.pkN = num3;
		num5 = float.MaxValue;
		if (peak2.lfDotNo == -1)
		{
			for (int num4 = num3 - 1; num4 >= int_2; num4--)
			{
				if (dots[num4].Y < num5)
				{
					num5 = dots[num4].Y;
					peak2.lfDotNo = (peak2.bsLfV.dotNo = num4);
				}
				if (enumPeakStateList[num4] != EnumPeakState.None)
				{
					break;
				}
			}
		}
		num5 = float.MaxValue;
		if (peak2.rtDotNo == -1)
		{
			for (int num4 = num3 + 1; num4 < dotNo2; num4++)
			{
				if (dots[num4].Y < num5)
				{
					num5 = dots[num4].Y;
					peak2.rtDotNo = (peak2.bsRtV.dotNo = num4);
				}
				if (enumPeakStateList[num4] != EnumPeakState.None)
				{
					break;
				}
			}
		}
		if (peak2.lfDotNo < 0 || peak2.rtDotNo < 0)
		{
			return false;
		}
		peak.bsStyle = BsStyle.ForwHorz;
		needProcPeak(peak2);
		enumPeakStateList[peak2.bsRtV.dotNo] = EnumPeakState.Clear;
		enumPeakStateList[peak2.bsLfV.dotNo] = EnumPeakState.Clear;
		if (peak2.area > 0f)
		{
			Array.Resize(ref peak_0, peak_0.Length + 1);
			peak_0[peak_0.Length - 1] = peak2;
			Array.Sort(peak_0);
		}
		return true;
	}

	private bool ResetDtecSolv(int startIdx, int endIdx, EnumDetectPeakMethod peakMethod)
	{
		SetMyTickCount();
		if (endIdx - startIdx < 20)
		{
			return false;
		}
		if (!MarkPeakAndValuePointSolv(startIdx, endIdx, peakMethod))
		{
			return false;
		}
		for (int i = 3; i < Integs.Count; i++)
		{
			IntegOprtStyle oprtStyle = Integs.IntegRows[i].oprtStyle;
			float timeA = Integs.IntegRows[i].timeA;
			float timeB = Integs.IntegRows[i].timeB;
			float value = Integs.IntegRows[i].value;
			int dotNo = getDotNo(Math.Min(timeA, timeB));
			int dotNo2 = getDotNo(Math.Max(timeA, timeB));
			if (dotNo != dotNo2)
			{
				IntegOprtStyle integOprtStyle = oprtStyle;
				if (integOprtStyle == IntegOprtStyle.PkVale)
				{
					dotNo = getDotNo(timeA);
					dotNo2 = getDotNo(timeB);
					method_11(dotNo, dotNo2);
				}
			}
		}
		Peak[] array = DetectPeaksSolv(startIdx, endIdx);
		for (int i = 0; i < array.Length; i++)
		{
			if (array[i].width < myfPeakWidth && SimpleIsolatedPeak(array[i]))
			{
				DeletePeak(array[i]);
				array[i--] = array[array.Length - 1];
				Array.Resize(ref array, array.Length - 1);
			}
		}
		for (int i = 0; i < array.Length; i++)
		{
			array[i].startT = dots[array[i].FromNo].X;
			array[i].endT = dots[array[i].ToNo].X;
			array[i].startV = dots[array[i].FromNo].Y;
			array[i].endV = dots[array[i].ToNo].Y;
		}
		for (int i = 0; i < array.Length; i++)
		{
			needProcPeak(array[i]);
		}
		SetPeakTgntYsList(array, Integs.IniTgntAreaF, Integs.IniTgntSlopeF, Integs.IniTgntLfF, Integs.IniTgntRtF);
		valeToVale(array, Integs.IniVtVSlope);
		int num = peak_0.Length;
		Array.Resize(ref peak_0, num + array.Length);
		for (int j = 0; j < array.Length; j++)
		{
			peak_0[num + j] = array[j];
		}
		for (int i = 0; i < array.Length; i++)
		{
			if (!array[i].BsTogether && (array[i].width < myfPeakWidth || array[i].height < myfThreshold))
			{
				DeletePeak(array[i]);
			}
		}
		Array.Sort(peak_0);
		return true;
	}

	private bool ResetDtecNeg(int startIdx, int endIdx, EnumDetectPeakMethod peakMethod)
	{
		SetMyTickCount();
		if (endIdx - startIdx < 20)
		{
			return false;
		}
		if (!MarkPeakAndValuePoint(startIdx, endIdx, peakMethod))
		{
			return false;
		}
		for (int i = 3; i < Integs.Count; i++)
		{
			IntegOprtStyle oprtStyle = Integs.IntegRows[i].oprtStyle;
			float timeA = Integs.IntegRows[i].timeA;
			float timeB = Integs.IntegRows[i].timeB;
			float value = Integs.IntegRows[i].value;
			int dotNo = getDotNo(Math.Min(timeA, timeB));
			int dotNo2 = getDotNo(Math.Max(timeA, timeB));
			if (dotNo != dotNo2)
			{
				IntegOprtStyle integOprtStyle = oprtStyle;
				if (integOprtStyle == IntegOprtStyle.PkVale)
				{
					dotNo = getDotNo(timeA);
					dotNo2 = getDotNo(timeB);
					method_11(dotNo, dotNo2);
				}
			}
		}
		Peak[] array = DetectPeaks(startIdx, endIdx);
		for (int i = 0; i < array.Length; i++)
		{
			if (array[i].width < myfPeakWidth && SimpleIsolatedPeak(array[i]))
			{
				DeletePeak(array[i]);
				array[i--] = array[array.Length - 1];
				Array.Resize(ref array, array.Length - 1);
			}
		}
		for (int i = 0; i < array.Length; i++)
		{
			array[i].startT = dots[array[i].FromNo].X;
			array[i].endT = dots[array[i].ToNo].X;
			array[i].startV = dots[array[i].FromNo].Y;
			array[i].endV = dots[array[i].ToNo].Y;
		}
		for (int i = 0; i < array.Length; i++)
		{
			needProcPeak(array[i]);
		}
		SetPeakTgntYsList(array, Integs.IniTgntAreaF, Integs.IniTgntSlopeF, Integs.IniTgntLfF, Integs.IniTgntRtF);
		valeToVale(array, Integs.IniVtVSlope);
		for (int i = 0; i < array.Length; i++)
		{
			if (!array[i].BsTogether && (array[i].width < myfPeakWidth || array[i].height < myfThreshold))
			{
				DeletePeak(array[i]);
			}
		}
		Array.Sort(peak_0);
		return true;
	}

	private bool ClampNeg(int startIdx, int endIdx)
	{
		Peak[] array = GetPeaks_2Point(startIdx, endIdx);
		for (int i = 0; i < array.Length; i++)
		{
			if (array[i].positive)
			{
				int num = array.Length - 1;
				array[i--] = array[num];
				Array.Resize(ref array, num);
			}
		}
		if (array.Length == 0)
		{
			return false;
		}
		int[] int_ = new int[0];
		foreach (Peak peak in array)
		{
			AddPointIndex2Array(ref int_, peak.LfDotNo, peak);
			AddPointIndex2Array(ref int_, peak.RtDotNo, peak);
			for (int j = peak.LfDotNo + 1; j < peak.RtDotNo; j++)
			{
				float num2 = dots[j].Y;
				if (peak.FromNo <= j && j <= peak.ToNo)
				{
					num2 = peak.bsYs[j - peak.FromNo];
				}
				dots[j].Y = num2 + num2 - dots[j].Y;
			}
			peak.positive = true;
			needProcPeak(peak);
		}
		return true;
	}

	private bool PkAddSolventPeak(int int_2, int endIdx)
	{
		bool flag = false;
		int num = -1;
		Peak[] array = new Peak[2];
		for (int i = 0; i < peak_0.Length; i++)
		{
			if (int_2 > peak_0[i].lfDotNo && int_2 < peak_0[i].rtDotNo)
			{
				flag = true;
				num = i;
				break;
			}
		}
		if (!flag)
		{
			return false;
		}
		int lfDotNo = peak_0[num].lfDotNo;
		int rtDotNo = peak_0[num].rtDotNo;
		enumPeakStateList[int_2] = EnumPeakState.Clear;
		float num2 = float.MinValue;
		int num3 = -1;
		int num4 = (peak_0[num].bsLfV.dotNo = (peak_0[num].lfDotNo = int_2));
		int num5 = (peak_0[num].bsRtV.dotNo = peak_0[num].rtDotNo);
		for (int j = lfDotNo; j <= num4; j++)
		{
			if (dots[j].Y > num2)
			{
				num2 = dots[j].Y;
				num3 = j;
			}
		}
		peak_0[num].lfDotNo = (peak_0[num].bsLfV.dotNo = int_2);
		Peak peak = new Peak
		{
			positive = true,
			rtDotNo = int_2,
			lfDotNo = lfDotNo
		};
		peak.bsLfV.dotNo = peak.lfDotNo;
		peak.bsRtV.dotNo = peak.rtDotNo;
		if (num3 == int_2)
		{
			num3 = int_2 - 1;
		}
		if (num3 == peak.lfDotNo)
		{
			num3 = peak.lfDotNo + 1;
		}
		peak.pkN = num3;
		float num6 = float.MaxValue;
		if (peak.lfDotNo == -1)
		{
			for (int j = num3 - 1; j >= num4; j--)
			{
				if (dots[j].Y < num6)
				{
					num6 = dots[j].Y;
					peak.lfDotNo = (peak.bsLfV.dotNo = j);
				}
				if (enumPeakStateList[j] != EnumPeakState.None)
				{
					break;
				}
			}
		}
		num6 = float.MaxValue;
		if (peak.rtDotNo == -1)
		{
			for (int j = num3 + 1; j < int_2; j++)
			{
				if (dots[j].Y < num6)
				{
					num6 = dots[j].Y;
					peak.rtDotNo = (peak.bsRtV.dotNo = j);
				}
				if (enumPeakStateList[j] != EnumPeakState.None)
				{
					break;
				}
			}
		}
		if (peak.lfDotNo < 0 || peak.rtDotNo < 0)
		{
			return false;
		}
		num2 = float.MinValue;
		for (int j = int_2; j <= num5; j++)
		{
			if (dots[j].Y > num2)
			{
				num2 = dots[j].Y;
				num3 = j;
			}
		}
		peak_0[num].pkN = num3;
		peak.bsStyle = BsStyle.General;
		needProcPeak(peak);
		enumPeakStateList[peak.bsRtV.dotNo] = EnumPeakState.Clear;
		enumPeakStateList[peak.bsLfV.dotNo] = EnumPeakState.Clear;
		needProcPeak(peak_0[num]);
		Array.Resize(ref peak_0, peak_0.Length + 1);
		peak_0[peak_0.Length - 1] = peak;
		Array.Sort(peak_0);
		valeToVale(peak_0, Integs.IniVtVSlope);
		return true;
	}

	private bool PkAddPosi(int startIdx, int endIdx)
	{
		if (!method_2(ref startIdx, ref endIdx))
		{
			return false;
		}
		float num = float.MinValue;
		int num2 = -1;
		for (int num3 = (startIdx + endIdx) / 2; num3 >= startIdx; num3--)
		{
			if (dots[num3].Y > num)
			{
				num = dots[num3].Y;
				num2 = num3;
			}
		}
		for (int num3 = (startIdx + endIdx) / 2; num3 <= endIdx; num3++)
		{
			if (dots[num3].Y > num)
			{
				num = dots[num3].Y;
				num2 = num3;
			}
		}
		for (int i = 0; i < PeaksNum; i++)
		{
			if (peak_0[i].pkN == num2)
			{
				return false;
			}
		}
		Peak peak = new Peak
		{
			positive = true,
			rtDotNo = -1,
			lfDotNo = -1
		};
		if (num2 == startIdx)
		{
			num2 = startIdx + 1;
			peak.lfDotNo = (peak.bsLfV.dotNo = startIdx);
		}
		if (num2 == endIdx)
		{
			num2 = endIdx - 1;
			peak.rtDotNo = (peak.bsRtV.dotNo = endIdx);
		}
		peak.pkN = num2;
		float num4 = float.MaxValue;
		if (peak.lfDotNo == -1)
		{
			for (int num3 = num2 - 1; num3 >= startIdx; num3--)
			{
				if (dots[num3].Y < num4)
				{
					num4 = dots[num3].Y;
					peak.lfDotNo = (peak.bsLfV.dotNo = num3);
				}
				if (enumPeakStateList[num3] != EnumPeakState.None)
				{
					break;
				}
			}
		}
		num4 = float.MaxValue;
		if (peak.rtDotNo == -1)
		{
			for (int num3 = num2 + 1; num3 < endIdx; num3++)
			{
				if (dots[num3].Y < num4)
				{
					num4 = dots[num3].Y;
					peak.rtDotNo = (peak.bsRtV.dotNo = num3);
				}
				if (enumPeakStateList[num3] != EnumPeakState.None)
				{
					break;
				}
			}
		}
		if (peak.lfDotNo < 0 || peak.rtDotNo < 0)
		{
			return false;
		}
		needProcPeak(peak);
		enumPeakStateList[peak.bsRtV.dotNo] = EnumPeakState.Clear;
		enumPeakStateList[peak.bsLfV.dotNo] = EnumPeakState.Clear;
		int peaksNum = PeaksNum;
		if (peak.area > 0f)
		{
			Array.Resize(ref peak_0, peaksNum + 1);
			peak_0[peaksNum] = peak;
			Array.Sort(peak_0);
		}
		return true;
	}

	private bool qzPkAddPosi(float timeA, float timeB)
	{
		qzPkAddPosi2(timeA, timeB);
		return true;
	}

	private bool qzPkAddPosi2(float time1, float time2)
	{
		int dotNo = getDotNo(Math.Min(time1, time2));
		int dotNo2 = getDotNo(Math.Max(time1, time2));
		float num = float.MinValue;
		int num2 = -1;
		for (int num3 = (dotNo + dotNo2) / 2; num3 >= dotNo; num3--)
		{
			if (dots[num3].Y > num)
			{
				num = dots[num3].Y;
				num2 = num3;
			}
		}
		for (int num3 = (dotNo + dotNo2) / 2; num3 <= dotNo2; num3++)
		{
			if (dots[num3].Y > num)
			{
				num = dots[num3].Y;
				num2 = num3;
			}
		}
		for (int i = 0; i < PeaksNum; i++)
		{
			if (peak_0[i].pkN == num2)
			{
				return false;
			}
		}
		Peak peak = new Peak
		{
			positive = true,
			rtDotNo = -1,
			lfDotNo = -1
		};
		if (num2 == dotNo)
		{
			num2 = dotNo + 1;
			peak.lfDotNo = (peak.bsLfV.dotNo = dotNo);
		}
		if (num2 == dotNo2)
		{
			num2 = dotNo2 - 1;
			peak.rtDotNo = (peak.bsRtV.dotNo = dotNo2);
		}
		peak.pkN = num2;
		float num4 = float.MaxValue;
		if (peak.lfDotNo == -1)
		{
			for (int num3 = num2 - 1; num3 >= dotNo; num3--)
			{
				if (dots[num3].Y < num4)
				{
					num4 = dots[num3].Y;
					peak.lfDotNo = (peak.bsLfV.dotNo = num3);
				}
				if (enumPeakStateList[num3] != EnumPeakState.None)
				{
					break;
				}
			}
		}
		num4 = float.MaxValue;
		if (peak.rtDotNo == -1)
		{
			for (int num3 = num2 + 1; num3 < dotNo2; num3++)
			{
				if (dots[num3].Y < num4)
				{
					num4 = dots[num3].Y;
					peak.rtDotNo = (peak.bsRtV.dotNo = num3);
				}
				if (enumPeakStateList[num3] != EnumPeakState.None)
				{
					break;
				}
			}
		}
		if (peak.lfDotNo < 0 || peak.rtDotNo < 0)
		{
			return false;
		}
		needProcPeak(peak, dotNo, dotNo2, time1, time2);
		enumPeakStateList[peak.bsRtV.dotNo] = EnumPeakState.Clear;
		enumPeakStateList[peak.bsLfV.dotNo] = EnumPeakState.Clear;
		int peaksNum = PeaksNum;
		if (peak.area > 0f)
		{
			Array.Resize(ref peak_0, peaksNum + 1);
			peak_0[peaksNum] = peak;
			Array.Sort(peak_0);
		}
		return true;
	}

	private bool PkAddNeg(int startIdx, int endIdx)
	{
		if (!method_2(ref startIdx, ref endIdx))
		{
			return false;
		}
		float num = float.MaxValue;
		int num2 = -1;
		for (int num3 = (startIdx + endIdx) / 2; num3 >= startIdx; num3--)
		{
			if (dots[num3].Y < num)
			{
				num = dots[num3].Y;
				num2 = num3;
			}
		}
		for (int num3 = (startIdx + endIdx) / 2; num3 <= endIdx; num3++)
		{
			if (dots[num3].Y < num)
			{
				num = dots[num3].Y;
				num2 = num3;
			}
		}
		for (int i = 0; i < PeaksNum; i++)
		{
			if (peak_0[i].pkN == num2)
			{
				return false;
			}
		}
		Peak peak = new Peak
		{
			positive = false,
			rtDotNo = -1,
			lfDotNo = -1
		};
		if (num2 == startIdx)
		{
			num2 = startIdx + 1;
			peak.lfDotNo = (peak.bsLfV.dotNo = startIdx);
		}
		if (num2 == endIdx)
		{
			num2 = endIdx - 1;
			peak.rtDotNo = (peak.bsRtV.dotNo = endIdx);
		}
		peak.pkN = num2;
		float num4 = float.MinValue;
		if (peak.lfDotNo == -1)
		{
			for (int num3 = num2 - 1; num3 >= startIdx; num3--)
			{
				if (dots[num3].Y > num4)
				{
					num4 = dots[num3].Y;
					peak.lfDotNo = (peak.bsLfV.dotNo = num3);
				}
				if (enumPeakStateList[num3] != EnumPeakState.None)
				{
					break;
				}
			}
		}
		num4 = float.MinValue;
		if (peak.rtDotNo == -1)
		{
			for (int num3 = num2 + 1; num3 < endIdx; num3++)
			{
				if (dots[num3].Y > num4)
				{
					num4 = dots[num3].Y;
					peak.rtDotNo = (peak.bsRtV.dotNo = num3);
				}
				if (enumPeakStateList[num3] != EnumPeakState.None)
				{
					break;
				}
			}
		}
		if (peak.lfDotNo < 0 || peak.rtDotNo < 0)
		{
			return false;
		}
		needProcPeak(peak);
		enumPeakStateList[peak.bsRtV.dotNo] = EnumPeakState.Clear;
		enumPeakStateList[peak.bsLfV.dotNo] = EnumPeakState.Clear;
		int peaksNum = PeaksNum;
		if (peak.area > 0f)
		{
			Array.Resize(ref peak_0, peaksNum + 1);
			peak_0[peaksNum] = peak;
			Array.Sort(peak_0);
		}
		return true;
	}

	private bool MarkValeList_ByCutPks(int startIdx, int endIdx)
	{
		Peak[] peaks_2Point = GetPeaks_2Point(startIdx, endIdx);
		if (peaks_2Point.Length == 0)
		{
			return false;
		}
		for (int i = 0; i < peaks_2Point.Length; i++)
		{
			DeletePeak(peaks_2Point[i]);
		}
		int lfDotNo = peaks_2Point[0].lfDotNo;
		int rtDotNo = peaks_2Point[peaks_2Point.Length - 1].rtDotNo;
		method_6(lfDotNo, bool_0: false);
		method_6(rtDotNo, bool_0: false);
		Array.Sort(peak_0);
		MarkValeList("pkCutPks");
		return true;
	}

	private bool MarkValeList_ByIncise(int startIdx, int endIdx)
	{
		Peak[] peaks_2Point = GetPeaks_2Point(startIdx, endIdx);
		if (peaks_2Point.Length == 0)
		{
		}
		for (int i = 0; i < peaks_2Point.Length; i++)
		{
			DeletePeak(peaks_2Point[i]);
		}
		for (int j = startIdx; j < endIdx; j++)
		{
			enumPeakStateList[j] = EnumPeakState.Clear;
		}
		method_6(startIdx, bool_0: false);
		method_6(endIdx, bool_0: false);
		Array.Sort(peak_0);
		MarkValeList("pkCutPks");
		return true;
	}

	private bool MarkValeList_ByArea(int startIdx, int endIdx, float float_10)
	{
		Peak[] peaks_2Point = GetPeaks_2Point(startIdx, endIdx);
		if (peaks_2Point.Length == 0)
		{
			return false;
		}
		bool flag = false;
		for (int i = 0; i < peaks_2Point.Length; i++)
		{
			if (peaks_2Point[i].area < float_10)
			{
				DeletePeak(peaks_2Point[i]);
				flag = true;
			}
		}
		if (flag)
		{
			Array.Sort(peak_0);
			MarkValeList("pkArea");
		}
		return flag;
	}

	private bool MarkValeList_ByW05(int startIdx, int endIdx, float float_10)
	{
		Peak[] peaks_2Point = GetPeaks_2Point(startIdx, endIdx);
		if (peaks_2Point.Length == 0)
		{
			return false;
		}
		bool flag = false;
		for (int i = 0; i < peaks_2Point.Length; i++)
		{
			if (peaks_2Point[i].WO5 < float_10)
			{
				DeletePeak(peaks_2Point[i]);
				flag = true;
			}
		}
		if (flag)
		{
			Array.Sort(peak_0);
			MarkValeList("pkHalfWidth");
		}
		return flag;
	}

	private bool MarkValeList_ByThreshold(int startIdx, int endIdx, float float_10)
	{
		Peak[] peaks_2Point = GetPeaks_2Point(startIdx, endIdx);
		if (peaks_2Point.Length == 0)
		{
			return false;
		}
		bool flag = false;
		for (int i = 0; i < peaks_2Point.Length; i++)
		{
			if (peaks_2Point[i].height < float_10)
			{
				DeletePeak(peaks_2Point[i]);
				flag = true;
			}
		}
		if (flag)
		{
			Array.Sort(peak_0);
			MarkValeList("pkThreshold");
		}
		return flag;
	}

	private bool MarkValeList_ByWidth(int startIdx, int endIdx, float float_10)
	{
		Peak[] peaks_2Point = GetPeaks_2Point(startIdx, endIdx);
		if (peaks_2Point.Length == 0)
		{
			return false;
		}
		bool flag = false;
		for (int i = 0; i < peaks_2Point.Length; i++)
		{
			if (peaks_2Point[i].width < float_10)
			{
				DeletePeak(peaks_2Point[i]);
				flag = true;
			}
		}
		if (flag)
		{
			Array.Sort(peak_0);
			MarkValeList("pkWidth");
		}
		return flag;
	}

	private bool BsTgnt(int startIdx, int endIdx, float float_10, float float_11, float float_12, float float_13)
	{
		Peak[] peaks_2Point = GetPeaks_2Point(startIdx, endIdx);
		if (peaks_2Point.Length == 0)
		{
			return false;
		}
		for (int i = 0; i < peaks_2Point.Length; i++)
		{
			Peak onTanPeak = GetOnTanPeak(peaks_2Point[i]);
			if (onTanPeak != null && !IsContainPeak(peaks_2Point, onTanPeak))
			{
				MessageBox.Show("请选择相对独立的范围！");
				return false;
			}
		}
		SetPeakTgntYsList(peaks_2Point, float_10, float_11, float_12, float_13);
		valeToVale(peaks_2Point, Integs.IniVtVSlope);
		return true;
	}

	private bool BsValley(int startIdx, int int_3)
	{
		bool flag = false;
		for (int i = startIdx; i <= int_3; i++)
		{
			if (enumPeakStateList[i] == EnumPeakState.Clear)
			{
				bool flag2 = method_6(i, bool_0: true);
				if (!flag)
				{
					flag = flag2;
				}
			}
		}
		return flag;
	}

	private bool BsVtV(int startIdx, int endIdx, float float_10)
	{
		Peak[] peaks_2Point = GetPeaks_2Point(startIdx, endIdx);
		if (peaks_2Point.Length == 0)
		{
			return false;
		}
		foreach (Peak peak in peaks_2Point)
		{
			if (peak.bsLfV.dotNo != peak.LfDotNo || peak.bsRtV.dotNo != peak.RtDotNo)
			{
				peak.bsLfV.dotNo = peak.LfDotNo;
				peak.bsRtV.dotNo = peak.RtDotNo;
				enumPeakStateList[peak.RtDotNo] = EnumPeakState.Clear;
				enumPeakStateList[peak.LfDotNo] = EnumPeakState.Clear;
				needProcPeak(peak);
			}
		}
		valeToVale(peaks_2Point, float_10);
		int dotNo = peaks_2Point[0].bsLfV.dotNo;
		int dotNo2 = peaks_2Point[peaks_2Point.Length - 1].bsRtV.dotNo;
		method_6(dotNo, bool_0: true);
		method_6(dotNo2, bool_0: true);
		return true;
	}

	private bool BsTogether(int startIdx, int endIdx)
	{
		bool flag = false;
		for (int i = startIdx; i <= endIdx; i++)
		{
			if (enumPeakStateList[i] == EnumPeakState.Clear)
			{
				bool flag2 = method_4(i);
				if (!flag)
				{
					flag = flag2;
				}
			}
		}
		return flag;
	}

	private bool BsForwHorz(int startIdx, int endIdx, LR lr_0)
	{
		Peak[] array = GetPeaks_2Point(startIdx, endIdx);
		for (int i = 0; i < array.Length; i++)
		{
			if (IsPeakOnTan(array[i]) && array[i].positive)
			{
				array[i].bsStyle = ((lr_0 == LR.Left) ? BsStyle.ForwHorz : BsStyle.BackHorz);
				continue;
			}
			array[i--] = array[array.Length - 1];
			Array.Resize(ref array, array.Length - 1);
		}
		if (array.Length == 0)
		{
			return false;
		}
		Array.Sort(array);
		int num = array[0].LfDotNo;
		if (enumPeakStateList[num] != EnumPeakState.Clear)
		{
			num = array[0].bsLfV.dotNo;
		}
		int num2 = array[array.Length - 1].RtDotNo;
		if (enumPeakStateList[num2] != EnumPeakState.Clear)
		{
			num2 = array[array.Length - 1].bsRtV.dotNo;
		}
		if (lr_0 == LR.Left)
		{
			for (int i = 0; i < array.Length; i++)
			{
				array[i].para1 = num;
				array[i].para2 = num2;
				needProcPeak(array[i]);
			}
		}
		if (lr_0 == LR.Right)
		{
			for (int i = array.Length - 1; i >= 0; i--)
			{
				array[i].para1 = num;
				array[i].para2 = num2;
				needProcPeak(array[i]);
			}
		}
		int num3 = -1;
		int num4 = -1;
		for (int i = 0; i < array.Length; i++)
		{
			if (array[i].bsStyle != BsStyle.General)
			{
				int dotNo = array[i].bsLfV.dotNo;
				int dotNo2 = array[i].bsRtV.dotNo;
				num3 = ((num3 < 0) ? dotNo : Math.Min(num3, dotNo));
				num4 = ((num4 < 0) ? dotNo2 : Math.Max(num4, dotNo2));
			}
		}
		method_6(num3, bool_0: true);
		method_6(num4, bool_0: true);
		return true;
	}

	private bool BsFrontTgnt(int startIdx, int endIdx, LR lr_0)
	{
		Peak[] array = GetPeaks_2Point(startIdx, endIdx);
		for (int i = 0; i < array.Length; i++)
		{
			if (!array[i].positive || array[i].bsStyle != BsStyle.General || array[i].lfTgntYs.Length != 1 || array[i].rtTgntYs.Length != 1)
			{
				array[i--] = array[array.Length - 1];
				Array.Resize(ref array, array.Length - 1);
			}
		}
		if (array.Length == 0)
		{
			return false;
		}
		Array.Sort(array);
		for (int i = 1; i < array.Length; i++)
		{
			if (array[i].LfDotNo != array[i - 1].RtDotNo)
			{
				return false;
			}
		}
		Peak peak_ = null;
		if (!((lr_0 == LR.Left) ? method_13(ref array, ref peak_) : method_14(ref array, ref peak_)))
		{
			return false;
		}
		for (int i = 0; i < array.Length; i++)
		{
			if (array[i].bsLfV.dotNo != array[i].LfDotNo || array[i].bsRtV.dotNo != array[i].RtDotNo)
			{
				array[i].bsLfV.dotNo = array[i].LfDotNo;
				array[i].bsRtV.dotNo = array[i].RtDotNo;
				needProcPeak(array[i]);
			}
		}
		needProcPeakList(array, null);
		int lfDotNo = array[0].LfDotNo;
		int rtDotNo = array[array.Length - 1].RtDotNo;
		if (lr_0 == LR.Left)
		{
			peak_.bsLfV.dotNo = lfDotNo;
			if (enumPeakStateList[peak_.RtDotNo] == EnumPeakState.Clear)
			{
				peak_.bsRtV.dotNo = peak_.RtDotNo;
			}
			CalcLeftAndRightTgnYs(peak_, LR.Left, lfDotNo);
		}
		if (lr_0 == LR.Right)
		{
			if (enumPeakStateList[peak_.LfDotNo] == EnumPeakState.Clear)
			{
				peak_.bsLfV.dotNo = peak_.LfDotNo;
			}
			peak_.bsRtV.dotNo = rtDotNo;
			CalcLeftAndRightTgnYs(peak_, LR.Right, rtDotNo);
		}
		needProcPeak(peak_);
		method_6((lr_0 == LR.Left) ? lfDotNo : rtDotNo, bool_0: false);
		method_6((lr_0 == LR.Left) ? peak_.bsRtV.dotNo : peak_.bsLfV.dotNo, bool_0: false);
		return true;
	}

	private void method_62(int int_2)
	{
		for (int i = 0; i < PeaksNum; i++)
		{
			bool flag;
			if (flag = peak_0[i].OnLfTgnt(int_2))
			{
				CalcLeftAndRightTgnYs(peak_0[i], LR.Left, -1);
			}
			bool flag2;
			if (flag2 = peak_0[i].OnRtTgnt(int_2))
			{
				CalcLeftAndRightTgnYs(peak_0[i], LR.Right, -1);
			}
			if (flag || flag2)
			{
				needProcPeak(peak_0[i]);
				break;
			}
		}
	}

	private void SetPeaksSelectState()
	{
		for (int i = 0; i < PeaksNum; i++)
		{
			if (peak_0[i] != null)
			{
				peak_0[i].selected = Class49.ValueInArray(iSelectPeakList, peak_0[i].pkN);
			}
		}
	}

	private void CalcLeftAndRightTgnYs(Peak peak_1, LR lr_0, int int_2)
	{
		if (lr_0 == LR.Left)
		{
			int num = ((int_2 >= 0) ? (peak_1.lfDotNo - int_2 + 1) : peak_1.lfTgntYs.Length);
			Array.Resize(ref peak_1.lfTgntYs, num);
			for (int i = 0; i < num; i++)
			{
				GetPeakBsYValue_ByIndex(peak_1, ref peak_1.lfTgntYs[i], peak_1.lfDotNo - i);
			}
		}
		if (lr_0 == LR.Right)
		{
			int num = ((int_2 >= 0) ? (int_2 - peak_1.rtDotNo + 1) : peak_1.rtTgntYs.Length);
			Array.Resize(ref peak_1.rtTgntYs, num);
			for (int i = 0; i < num; i++)
			{
				GetPeakBsYValue_ByIndex(peak_1, ref peak_1.rtTgntYs[i], peak_1.rtDotNo + i);
			}
		}
	}

	private int[] GetPeak_pkN_List()
	{
		List<int> list = new List<int>();
		for (int i = 0; i < PeaksNum; i++)
		{
			if (peak_0[i] != null && peak_0[i].selected)
			{
				list.Add(peak_0[i].pkN);
			}
		}
		return list.ToArray();
	}

	private Peak GetOnTanPeak(Peak peak_1)
	{
		if (!IsPeakOnTan(peak_1))
		{
			for (int i = 0; i < PeaksNum; i++)
			{
				if (peak_0[i] != peak_1 && (peak_0[i].OnLfTgnt(peak_1.pkN) || peak_0[i].OnRtTgnt(peak_1.pkN)))
				{
					return peak_0[i];
				}
			}
		}
		return null;
	}

	private float GetSlope2(PointF[] pointF_1, int startIdx, int endIdx, float float_10)
	{
		float num = (float_10 - pointF_1[startIdx].X) / (pointF_1[endIdx].X - pointF_1[startIdx].X);
		return pointF_1[startIdx].Y + num * (pointF_1[endIdx].Y - pointF_1[startIdx].Y);
	}

	private Peak FindPeakByDir(int int_2, LR lr_0, bool bool_0)
	{
		if (bool_0)
		{
			if (lr_0 == LR.Left)
			{
				for (int i = 0; i < PeaksNum; i++)
				{
					if (peak_0[i].rtDotNo == int_2)
					{
						return peak_0[i];
					}
				}
			}
			if (lr_0 == LR.Right)
			{
				for (int i = 0; i < PeaksNum; i++)
				{
					if (peak_0[i].lfDotNo == int_2)
					{
						return peak_0[i];
					}
				}
			}
		}
		else
		{
			if (lr_0 == LR.Left)
			{
				for (int i = 0; i < PeaksNum; i++)
				{
					if (peak_0[i].RtDotNo == int_2)
					{
						return peak_0[i];
					}
				}
			}
			if (lr_0 == LR.Right)
			{
				for (int i = PeaksNum - 1; i >= 0; i--)
				{
					if (peak_0[i].LfDotNo == int_2)
					{
						return peak_0[i];
					}
				}
			}
		}
		return null;
	}

	private Peak FindPeakByPeakIndex(Peak peak_1)
	{
		for (int i = 0; i < PeaksNum; i++)
		{
			if (peak_0[i].pkN == peak_1.pkN)
			{
				return peak_0[i];
			}
		}
		return null;
	}

	private void GetPeakBsYValue_ByIndex(Peak peak_1, ref float float_10, int int_2)
	{
		Peak peak = null;
		for (int i = 0; i < PeaksNum; i++)
		{
			if ((peak = peak_0[i]) != peak_1 && peak.BsY(int_2, ref float_10))
			{
				return;
			}
		}
		float_10 = dots[int_2].Y;
	}

	private void DeletePeak(Peak peak_1)
	{
		enumPeakStateList[peak_1.pkN] = EnumPeakState.None;
		int lfDotNo;
		if (IsolatedVale(lfDotNo = peak_1.lfDotNo, peak_1))
		{
			enumPeakStateList[lfDotNo] = EnumPeakState.None;
		}
		if (IsolatedVale(lfDotNo = peak_1.rtDotNo, peak_1))
		{
			enumPeakStateList[lfDotNo] = EnumPeakState.None;
		}
		if (peak_1.bsLfV.dotNo != peak_1.lfDotNo && IsolatedVale(lfDotNo = peak_1.bsLfV.dotNo, peak_1))
		{
			enumPeakStateList[lfDotNo] = EnumPeakState.None;
		}
		if (peak_1.bsRtV.dotNo != peak_1.rtDotNo && IsolatedVale(lfDotNo = peak_1.bsRtV.dotNo, peak_1))
		{
			enumPeakStateList[lfDotNo] = EnumPeakState.None;
		}
		for (int i = 0; i < PeaksNum; i++)
		{
			if (peak_0[i] == peak_1)
			{
				peak_0[i] = peak_0[PeaksNum - 1];
				Array.Resize(ref peak_0, PeaksNum - 1);
				break;
			}
		}
		Array.Sort(peak_0);
	}

	private void SetPeakTgntYs(Peak peak_1)
	{
		if (peak_1.bsStyle != BsStyle.General || peak_1.lfTgntYs.Length != 1 || peak_1.rtTgntYs.Length != 1 || peak_1.BsLfTogether || peak_1.BsRtTogether)
		{
			Array.Resize(ref peak_1.lfTgntYs, 1);
			Array.Resize(ref peak_1.rtTgntYs, 1);
			peak_1.bsLfV.dotNo = peak_1.lfDotNo;
			peak_1.bsRtV.dotNo = peak_1.rtDotNo;
			peak_1.bsRtV.N = -1;
			peak_1.bsLfV.N = -1;
			peak_1.bsStyle = BsStyle.General;
			needProcPeak(peak_1);
		}
	}

	private Peak[] GetPeaks_2Point(int startIdx, int endIdx)
	{
		Peak[] array = new Peak[0];
		for (int i = 0; i < PeaksNum; i++)
		{
			if (startIdx <= peak_0[i].pkN && peak_0[i].pkN <= endIdx)
			{
				int num = array.Length;
				Array.Resize(ref array, num + 1);
				array[num] = peak_0[i];
			}
		}
		Array.Sort(array);
		return array;
	}

	private void SetPeakTgntYsList(Peak[] peak_1, float float_10, float float_11, float float_12, float float_13)
	{
		if (float_10 <= 0f)
		{
			for (int i = 0; i < peak_1.Length; i++)
			{
				SetPeakTgntYs(peak_1[i]);
			}
			return;
		}
		Array.Sort(peak_1);
		Peak[] array = new Peak[0];
		int num = -1;
		bool flag = true;
		foreach (Peak peak in peak_1)
		{
			if (peak.lfDotNo == num && peak.positive == flag)
			{
				int num2 = array.Length;
				Array.Resize(ref array, num2 + 1);
				array[num2] = peak;
			}
			else
			{
				for (int i = 0; i < array.Length; i++)
				{
					SetPeakTgntYs(array[i]);
				}
				method_0(array, float_10, float_11, float_12, float_13);
				Array.Resize(ref array, 1);
				array[0] = peak;
			}
			num = peak.rtDotNo;
			flag = peak.positive;
		}
		for (int i = 0; i < array.Length; i++)
		{
			SetPeakTgntYs(array[i]);
		}
		method_0(array, float_10, float_11, float_12, float_13);
	}

	private void needProcPeakList(Peak[] peak_1, float[] vtvs)
	{
		if (peak_1 == null || peak_1.Length == 0)
		{
			return;
		}
		if (vtvs != null)
		{
			if (peak_1.Length != vtvs.Length)
			{
				throw new Exception("together: pks.Length != vtvs.Length");
			}
			float num = float.MaxValue;
			int num2 = -1;
			for (int i = 0; i < vtvs.Length; i++)
			{
				if (vtvs[i] < num)
				{
					num = vtvs[i];
					num2 = i;
				}
			}
			int lfDotNo = peak_1[0].LfDotNo;
			int rtDotNo = peak_1[num2].RtDotNo;
			enumPeakStateList[rtDotNo] = EnumPeakState.Clear;
			enumPeakStateList[lfDotNo] = EnumPeakState.Clear;
			for (int i = 0; i <= num2; i++)
			{
				peak_1[i].bsLfV.dotNo = lfDotNo;
				peak_1[i].bsRtV.dotNo = rtDotNo;
				peak_1[i].bsRtV.N = -1;
				peak_1[i].bsLfV.N = -1;
				needProcPeak(peak_1[i]);
			}
			if (num2 != vtvs.Length - 1)
			{
				int num3 = 0;
				Peak[] array = new Peak[vtvs.Length - 1 - num2];
				for (int i = num2 + 1; i < peak_1.Length; i++)
				{
					array[num3++] = peak_1[i];
				}
				needProcPeakList(array, null);
			}
		}
		else
		{
			float[] array2 = new float[peak_1.Length];
			array2[0] = Convert.ToSingle(peak_1[0].tanValue);
			for (int i = 1; i < peak_1.Length; i++)
			{
				array2[i] = GetSlope_2Point(peak_1[0].LfDotNo, peak_1[i].RtDotNo);
			}
			needProcPeakList(peak_1, array2);
		}
	}

	private void valeToVale(Peak[] peak_1, float myIniVtVSlope)
	{
		if (peak_1.Length == 0)
		{
			return;
		}
		Peak[] array = new Peak[0];
		foreach (Peak peak in peak_1)
		{
			if (!SimpleIsolatedPeak(peak) && IsPeakOnTan(peak))
			{
				peak.bsStyle = BsStyle.General;
				int num = array.Length;
				Array.Resize(ref array, num + 1);
				array[num] = peak;
			}
		}
		Array.Sort(array);
		Peak[] array2 = new Peak[0];
		float[] array3 = new float[0];
		int num2 = -1;
		bool flag = true;
		int startIndex = -1;
		foreach (Peak peak in array)
		{
			if (array2.Length != 0 && peak.LfDotNo == num2 && peak.positive == flag)
			{
				float num3 = GetSlope_2Point(startIndex, peak.RtDotNo);
				if (!peak.positive)
				{
					num3 = 0f - num3;
				}
				int num = array2.Length;
				Array.Resize(ref array2, num + 1);
				array2[num] = peak;
				Array.Resize(ref array3, num + 1);
				array3[num] = num3;
				if (num3 <= myIniVtVSlope)
				{
					needProcPeakList(array2, array3);
					Array.Resize(ref array2, 0);
				}
			}
			else
			{
				needProcPeakList(array2, array3);
				Array.Resize(ref array2, 0);
				double num4 = (peak.positive ? peak.tanValue : (0.0 - peak.tanValue));
				if (num4 > (double)myIniVtVSlope)
				{
					startIndex = peak.LfDotNo;
					Array.Resize(ref array2, 1);
					array2[0] = peak;
					Array.Resize(ref array3, 1);
					array3[0] = Convert.ToSingle(num4);
				}
			}
			num2 = peak.RtDotNo;
			flag = peak.positive;
		}
		needProcPeakList(array2, array3);
		MarkValeList("valeToVale");
	}

	private void CalcLeftAndRightDistance(Peak peak_1, float[] numArray, float fHRate, ref float leftDist, ref float rightDist)
	{
		float num = peak_1.height * fHRate;
		int num2 = -1;
		int num3 = -1;
		float num4 = float.MaxValue;
		int fromNo = peak_1.FromNo;
		int num5 = peak_1.pkN - fromNo;
		for (int i = 0; i < num5; i++)
		{
			float num6 = Math.Abs(numArray[i] - num);
			if (num6 < num4)
			{
				num4 = num6;
				num2 = i;
			}
		}
		num4 = float.MaxValue;
		for (int j = num5; j < numArray.Length; j++)
		{
			float num6 = Math.Abs(numArray[j] - num);
			if (num6 < num4)
			{
				num4 = num6;
				num3 = j;
			}
		}
		num2 += fromNo;
		num3 += fromNo;
		leftDist = peak_1.pkRT - dots[num2].X;
		rightDist = dots[num3].X - peak_1.pkRT;
	}

	private bool IsValidePeak(int startIdx, int endIdx)
	{
		if (startIdx == endIdx)
		{
			return false;
		}
		if (myfMaxPeakWidth < 0f || myfMaxThreshold < 0f)
		{
			myfMaxPeakWidth = Math.Max(0.001f, 1.15f * myfPeakWidth);
			myfMaxThreshold = Math.Max(0.001f, 0.35f * myfThreshold);
		}
		float xDistance_2Point = GetXDistance_2Point2(startIdx, endIdx);
		float yDistance_2Point = GetYDistance_2Point(startIdx, endIdx);
		return xDistance_2Point < myfMaxPeakWidth && yDistance_2Point < myfMaxThreshold;
	}

	private bool ProcPeakList(int startIdx, int endIdx)
	{
		if (endIdx != startIdx)
		{
			int num = -1;
			int num2 = -1;
			int num3 = -1;
			int num4 = -1;
			int num5 = -1;
			for (int num6 = startIdx; num6 >= 0; num6--)
			{
				if (enumPeakStateList[num6] == EnumPeakState.Clear && num4 == -1)
				{
					num4 = num6;
				}
				if (enumPeakStateList[num6] == EnumPeakState.Peak || enumPeakStateList[num6] == EnumPeakState.Vale)
				{
					num2 = num6;
					break;
				}
			}
			for (int num6 = startIdx + 1; num6 < dotLength; num6++)
			{
				if (enumPeakStateList[num6] == EnumPeakState.Clear && num5 == -1)
				{
					num5 = num6;
				}
				if (enumPeakStateList[num6] == EnumPeakState.Peak || enumPeakStateList[num6] == EnumPeakState.Vale)
				{
					num3 = num6;
					break;
				}
			}
			if (num4 >= 0 && num5 == -1)
			{
				num = num4;
			}
			if (num4 == -1 && num5 >= 0)
			{
				num = num5;
			}
			if (num4 >= 0 && num5 >= 0)
			{
				num = ((GetXDistance_2Point2(startIdx, num4) < GetXDistance_2Point2(startIdx, num5)) ? num4 : num5);
			}
			if (num == -1)
			{
				return false;
			}
			if (num == endIdx)
			{
				return true;
			}
			bool flag = num2 < 0 || num2 < endIdx;
			bool flag2 = num3 < 0 || endIdx < num3;
			if (!flag || !flag2)
			{
				return false;
			}
			enumPeakStateList[num] = EnumPeakState.None;
			for (int num6 = num - 1; num6 >= endIdx; num6--)
			{
				if (enumPeakStateList[num6] == EnumPeakState.Clear)
				{
					endIdx = num6;
					break;
				}
			}
			for (int num6 = num + 1; num6 <= endIdx; num6++)
			{
				if (enumPeakStateList[num6] == EnumPeakState.Clear)
				{
					endIdx = num6;
					break;
				}
			}
			enumPeakStateList[endIdx] = EnumPeakState.Clear;
			for (int i = 0; i < PeaksNum; i++)
			{
				Peak peak = peak_0[i];
				peak.needProc = false;
				if (peak.lfTgntYs.Length == 1)
				{
					if (peak.lfDotNo == num)
					{
						peak.lfDotNo = endIdx;
						peak.needProc = true;
					}
					if (peak.bsLfV.dotNo == num)
					{
						peak.bsLfV.dotNo = endIdx;
						peak.needProc = true;
					}
				}
				if (peak.rtTgntYs.Length == 1)
				{
					if (peak.rtDotNo == num)
					{
						peak.rtDotNo = endIdx;
						peak.needProc = true;
					}
					if (peak.bsRtV.dotNo == num)
					{
						peak.bsRtV.dotNo = endIdx;
						peak.needProc = true;
					}
				}
				if (peak.needProc)
				{
					needProcPeak(peak);
				}
			}
			for (int i = 0; i < PeaksNum; i++)
			{
				Peak peak = peak_0[i];
				if (peak.lfTgntYs.Length != 1)
				{
					int int_ = peak.LfDotNo;
					if (peak.OnLfTgnt(num))
					{
						peak.needProc = true;
					}
					if (peak.lfDotNo == num)
					{
						peak.lfDotNo = endIdx;
						peak.needProc = true;
					}
					if (peak.bsLfV.dotNo == num)
					{
						peak.bsLfV.dotNo = endIdx;
						peak.needProc = true;
					}
					if (peak.LfDotNo == num)
					{
						int_ = endIdx;
						peak.needProc = true;
					}
					if (peak.needProc)
					{
						CalcLeftAndRightTgnYs(peak, LR.Left, int_);
					}
				}
				if (peak.rtTgntYs.Length != 1)
				{
					int int_2 = peak.RtDotNo;
					if (peak.OnRtTgnt(num))
					{
						peak.needProc = true;
					}
					if (peak.rtDotNo == num)
					{
						peak.rtDotNo = endIdx;
						peak.needProc = true;
					}
					if (peak.bsRtV.dotNo == num)
					{
						peak.bsRtV.dotNo = endIdx;
						peak.needProc = true;
					}
					if (peak.RtDotNo == num)
					{
						int_2 = endIdx;
						peak.needProc = true;
					}
					if (peak.needProc)
					{
						CalcLeftAndRightTgnYs(peak, LR.Right, int_2);
					}
				}
				if (peak.needProc)
				{
					needProcPeak(peak);
				}
			}
			MarkValeList("chgPeakV");
		}
		return true;
	}

	public bool SimpleIsolatedPeak(Peak peak)
	{
		Peak peak2 = null;
		for (int i = 0; i < PeaksNum; i++)
		{
			peak2 = peak_0[i];
			if (peak2 != peak && (peak2.VsContain(peak.lfDotNo) || peak2.VsContain(peak.rtDotNo)))
			{
				return false;
			}
		}
		return true;
	}
}
