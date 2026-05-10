using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Xml.Serialization;

namespace IBrainChrom2018;

[Serializable]
[StructLayout(LayoutKind.Sequential)]
[XmlInclude(typeof(Vale))]
[XmlInclude(typeof(Compound))]
public class Peak : IComparable
{
	public Color _backColor;

	public float a05i;

	public float b05i;

	public float float_0;

	public float float_1;

	public float amount;

	public float amountPer;

	public float _amountPer;

	public float area;

	public float areaPer;

	public float _areaPer;

	public float Asymmetry;

	public float avRT;

	public BsStyle bsStyle;

	public Vale bsLfV = default(Vale);

	public Vale bsRtV = default(Vale);

	public double tanValue;

	public float bsHorzY;

	public float[] bsYs = new float[0];

	public float[] lfTgntYs = new float[1];

	public float[] rtTgntYs = new float[1];

	public float Capacity;

	public Compound compound;

	public float cus1;

	public float cus2;

	public float Eff_Column_EP = 0f;

	public float Eff_Column_JP;

	public float Eff_Column_USP;

	public float Efficiency_EP;

	public float Efficiency_JP;

	public float Efficiency_USP;

	public char groupID;

	public float height;

	public float heightPer;

	public float _heightPer;

	public float HETP_EP;

	public float HETP_JP;

	public float HETP_USP;

	public string name = "";

	public bool needProc;

	public int disNo;

	public int pkN;

	public int lfDotNo;

	public int rtDotNo;

	public int para1;

	public int para2;

	public PeakStyle pkStyle;

	public bool positive;

	public float Resolution_EP;

	public float Resolution_USP;

	public float response;

	public int respStyle = -1;

	public bool selected;

	public float startT;

	public float startV;

	public float pkRT;

	public float endT;

	public float endV;

	public float SymmetryTailing;

	public float width;

	public float WO5;

	public bool JuseTimeCheck = true;

	public double JStdandPeakTime;

	public double JTimePara;

	public double JPeakAdjustPara;

	public int JModBusAddr;

	public float GasAmount;

	public float CriticalAmount;

	public string StrResult;

	public bool BsLfTogether => bsLfV.dotNo < LfDotNo;

	public bool BsRtTogether => bsRtV.dotNo > RtDotNo;

	public bool BsTogether => BsLfTogether || BsRtTogether;

	public bool IsIdentified => name != "";

	public bool IsIstd => compound != null && (compound.cmpdInfo.istdCmpd == "" || compound.cmpdInfo.istdCmpd == null);

	public int LfDotNo => lfDotNo - lfTgntYs.Length + 1;

	public int RtDotNo => rtDotNo + rtTgntYs.Length - 1;

	public int FromNo
	{
		get
		{
			if (bsLfV.N < 0)
			{
				return LfDotNo;
			}
			return Math.Max(LfDotNo, bsLfV.N);
		}
	}

	public int ToNo
	{
		get
		{
			if (bsRtV.N < 0)
			{
				return RtDotNo;
			}
			return Math.Min(RtDotNo, bsRtV.N);
		}
	}

	public Peak()
	{
		bsRtV.N = -1;
		bsLfV.N = -1;
	}

	private float CalcFormula(string string_0)
	{
		string_0 = string_0.Trim();
		int num = string_0.LastIndexOf("+");
		if (num != -1)
		{
			return CalcFormula(string_0.Substring(0, num)) + CalcFormula(string_0.Substring(num + 1));
		}
		num = string_0.Length - 1;
		while (true)
		{
			int num2 = num - 1;
			if (num2 < 0)
			{
				break;
			}
			num = string_0.LastIndexOf('-', num2);
			int num3 = num;
			if (num3 == -1 || num3 == 0)
			{
				break;
			}
			if (!(string_0.Substring(num - 1, 1) != "#"))
			{
				continue;
			}
			return CalcFormula(string_0.Substring(0, num)) - CalcFormula(string_0.Substring(num + 1));
		}
		num = string_0.LastIndexOf('*');
		if (num != -1)
		{
			return CalcFormula(string_0.Substring(0, num)) * CalcFormula(string_0.Substring(num + 1));
		}
		num = string_0.LastIndexOf('/');
		if (num != -1)
		{
			return CalcFormula(string_0.Substring(0, num)) / CalcFormula(string_0.Substring(num + 1));
		}
		if (GetPeakParam_ByParamName(string_0, out var float_))
		{
			return float_;
		}
		if (string_0.StartsWith("#"))
		{
			return Convert.ToSingle(string_0.Substring(1));
		}
		if (string_0.StartsWith("-#"))
		{
			return 0f - Convert.ToSingle(string_0.Substring(2));
		}
		return Convert.ToSingle(string_0);
	}

	public bool BsY(int dotNo, ref float float_2)
	{
		int fromNo = FromNo;
		bool result;
		if (result = fromNo <= dotNo && dotNo <= ToNo)
		{
			float_2 = bsYs[dotNo - fromNo];
		}
		return result;
	}

	public void CalcuCus(string formula1, string formula2)
	{
		cus1 = CalcFormula2(formula1);
		cus2 = CalcFormula2(formula2);
	}

	private float CalcFormula2(string string_0)
	{
		if (string_0 == "")
		{
			return float.NaN;
		}
		string text = string_0;
		int num = 0;
		while (num != -1)
		{
			num = text.LastIndexOf('(');
			if (num != -1)
			{
				string text2 = text.Substring(num + 1);
				int num2 = text2.IndexOf(')');
				string string_1 = text2.Substring(0, num2);
				text = text.Substring(0, num) + "#" + CalcFormula(string_1).ToString("F") + text.Substring(num + num2 + 2);
			}
		}
		return CalcFormula(text);
	}

	public void CalcuResults(Signal signal, CalcuStyle calcuStyle)
	{
		if (compound.cmpdInfo.respStyle == RespStyle.Area)
		{
			response = area;
		}
		else if (compound.cmpdInfo.respStyle == RespStyle.Height)
		{
			response = height;
		}
		else if (compound.cmpdInfo.respStyle == RespStyle.AreaSquare)
		{
			response = (float)Math.Sqrt(area);
		}
		else if (compound.cmpdInfo.respStyle == RespStyle.PeakHeightSquare)
		{
			response = (float)Math.Sqrt(height);
		}
		if (calcuStyle == CalcuStyle.ESTD && !compound.eFunc.IsValideData)
		{
			calcuStyle = CalcuStyle.Uncal;
		}
		if (calcuStyle == CalcuStyle.ISTD && !compound.iFunc.IsValideData)
		{
			calcuStyle = CalcuStyle.Uncal;
		}
		switch (calcuStyle)
		{
		case CalcuStyle.Uncal:
			amount = -1f;
			break;
		case CalcuStyle.ESTD:
		{
			float[] array2 = new float[0];
			if (compound.eFunc.curveFit == CurveFit.Exponent)
			{
				array2 = compound.eFunc.Calcu_amountF(Convert.ToSingle(Math.Log(response * 1000f)));
				if (array2.Length != 0)
				{
					array2[0] = Convert.ToSingle(Math.Exp(array2[0]));
				}
			}
			else
			{
				array2 = compound.eFunc.Calcu_amountF(response);
			}
			if (array2.Length == 0)
			{
				amount = -1f;
			}
			else
			{
				amount = array2[0];
			}
			break;
		}
		case CalcuStyle.ISTD:
		{
			if (IsIstd)
			{
				break;
			}
			if (compound.cmpdInfo.istdCmpd == "" || compound.cmpdInfo.istdCmpd == null)
			{
				amount = -1f;
				break;
			}
			float num = -1f;
			float num2 = -1f;
			int num3 = 0;
			while (num3 < signal.PeaksNum)
			{
				if (signal.peaks[num3].name == compound.cmpdInfo.istdCmpd)
				{
					if (compound.cmpdInfo.respStyle == RespStyle.Area)
					{
						num = signal.peaks[num3].area;
					}
					else if (compound.cmpdInfo.respStyle == RespStyle.Height)
					{
						num = signal.peaks[num3].height;
					}
					else if (compound.cmpdInfo.respStyle == RespStyle.AreaSquare)
					{
						num = (float)Math.Sqrt(signal.peaks[num3].area);
					}
					else if (compound.cmpdInfo.respStyle == RespStyle.PeakHeightSquare)
					{
						num = (float)Math.Sqrt(signal.peaks[num3].height);
					}
					num2 = signal.peaks[num3].amount;
					if (num <= 0f || num2 <= 0f)
					{
						amount = -1f;
						break;
					}
					float responseF = response / num;
					float[] array = compound.iFunc.Calcu_amountF(responseF);
					if (array.Length >= 1)
					{
						amount = num2 * array[0];
						break;
					}
					amount = -1f;
				}
				else
				{
					num3++;
				}
			}
			break;
		}
		}
	}

	public void CloneBsline(Peak peak)
	{
		Array.Resize(ref bsYs, peak.bsYs.Length);
		for (int i = 0; i < bsYs.Length; i++)
		{
			bsYs[i] = peak.bsYs[i];
		}
	}

	public int CompareTo(object target)
	{
		if (target is Peak)
		{
			float value = (target as Peak).pkRT;
			return pkRT.CompareTo(value);
		}
		return 0;
	}

	public void CopyBaseInfo(Peak peak, bool cloneBsline)
	{
		positive = peak.positive;
		pkN = peak.pkN;
		lfDotNo = peak.lfDotNo;
		rtDotNo = peak.rtDotNo;
		Array.Resize(ref lfTgntYs, peak.lfTgntYs.Length);
		for (int i = 0; i < lfTgntYs.Length; i++)
		{
			lfTgntYs[i] = peak.lfTgntYs[i];
		}
		Array.Resize(ref rtTgntYs, peak.rtTgntYs.Length);
		for (int j = 0; j < rtTgntYs.Length; j++)
		{
			rtTgntYs[j] = peak.rtTgntYs[j];
		}
		bsStyle = peak.bsStyle;
		bsLfV = peak.bsLfV;
		bsRtV = peak.bsRtV;
		bsHorzY = peak.bsHorzY;
		tanValue = peak.tanValue;
		if (cloneBsline)
		{
			CloneBsline(peak);
		}
		pkRT = peak.pkRT;
		avRT = peak.avRT;
		area = peak.area;
		height = peak.height;
		width = peak.width;
		WO5 = peak.WO5;
		float_0 = peak.float_0;
		float_1 = peak.float_1;
	}

	public void DrawArea(PointF[] dots, FrameDis frameDis_0, Color disColor)
	{
		int fromNo = FromNo;
		int toNo = ToNo;
		if (toNo <= 0 || toNo < fromNo)
		{
			return;
		}
		PointF[] array = new PointF[toNo - fromNo + 3];
		int num = 0;
		float fTgntY = 0f;
		int num2 = fromNo;
		while (num2 <= toNo)
		{
			array[num] = dots[num2];
			if (TgntY(num2, ref fTgntY))
			{
				array[num].Y = fTgntY;
			}
			num2++;
			num++;
		}
		array[array.Length - 2] = GetBsRt(dots);
		array[array.Length - 1] = GetBsLf(dots);
		for (int i = 0; i < array.Length; i++)
		{
			array[i] = frameDis_0.lgToScr(array[i], bool_0: true);
		}
		if (frameDis_0.options.peakAreaClrSetByCalib && compound != null)
		{
			Color color = compound.cmpdInfo.color;
			if (color.A == Color.Transparent.A && color.R == Color.Transparent.R && color.G == Color.Transparent.G && color.B == Color.Transparent.B)
			{
			}
		}
		Color green = Color.Green;
		frameDis_0.FillPolygon(array, green);
	}

	public void DrawBaseLine(PointF[] dots, FrameDis frameDis_0)
	{
		PointF[] pointF_ = new PointF[3]
		{
			frameDis_0.lgToScr(dots[FromNo], bool_0: true),
			frameDis_0.lgToScr(GetBsLf(dots), bool_0: true),
			frameDis_0.lgToScr(GetBsRt(dots), bool_0: true)
		};
		frameDis_0.DrawLines(pointF_);
	}

	public float Get_lf(PointF[] dots)
	{
		return dots[lfDotNo].X;
	}

	public float Get_rt(PointF[] dots)
	{
		return dots[rtDotNo].X;
	}

	public PointF GetBsLf(PointF[] dots)
	{
		return new PointF(dots[FromNo].X, bsYs[0]);
	}

	public PointF GetBsRt(PointF[] dots)
	{
		return new PointF(dots[ToNo].X, bsYs[bsYs.Length - 1]);
	}

	public void LoadFromObject1(Peak peak, bool cloneBsline)
	{
		positive = peak.positive;
		pkN = peak.pkN;
		lfDotNo = peak.lfDotNo;
		rtDotNo = peak.rtDotNo;
		Array.Resize(ref lfTgntYs, peak.lfTgntYs.Length);
		for (int i = 0; i < lfTgntYs.Length; i++)
		{
			lfTgntYs[i] = peak.lfTgntYs[i];
		}
		Array.Resize(ref rtTgntYs, peak.rtTgntYs.Length);
		for (int j = 0; j < rtTgntYs.Length; j++)
		{
			rtTgntYs[j] = peak.rtTgntYs[j];
		}
		bsStyle = peak.bsStyle;
		bsLfV = peak.bsLfV;
		bsRtV = peak.bsRtV;
		bsHorzY = peak.bsHorzY;
		tanValue = peak.tanValue;
		if (cloneBsline)
		{
			CloneBsline(peak);
		}
		pkRT = peak.pkRT;
		avRT = peak.avRT;
		area = peak.area;
		height = peak.height;
		width = peak.width;
		WO5 = peak.WO5;
		float_0 = peak.float_0;
		float_1 = peak.float_1;
	}

	public bool OnLfTgnt(int dotNo)
	{
		return lfTgntYs.Length > 1 && LfDotNo < dotNo && dotNo < LfDotNo;
	}

	public bool OnRtTgnt(int dotNo)
	{
		return rtTgntYs.Length > 1 && rtDotNo < dotNo && dotNo < RtDotNo;
	}

	private bool GetPeakParam_ByParamName(string string_0, out float float_2)
	{
		if (string_0 != null && SystemDictionaryList.dictionary_0.TryGetValue(string_0, out var value))
		{
			switch (value)
			{
			case 0:
				float_2 = startT;
				goto IL_00ba;
			case 1:
				float_2 = endT;
				goto IL_00ba;
			case 2:
				float_2 = startV;
				goto IL_00ba;
			case 3:
				float_2 = endV;
				goto IL_00ba;
			case 4:
				float_2 = pkRT;
				goto IL_00ba;
			case 5:
				float_2 = area;
				goto IL_00ba;
			case 6:
				float_2 = height;
				goto IL_00ba;
			case 7:
				float_2 = width;
				goto IL_00ba;
			case 8:
				float_2 = WO5;
				goto IL_00ba;
			case 9:
				{
					float_2 = amount;
					goto IL_00ba;
				}
				IL_00ba:
				return true;
			}
		}
		float_2 = 0f;
		return false;
	}

	public bool TgntY(int dotNo, ref float fTgntY)
	{
		bool flag;
		if (flag = OnLfTgnt(dotNo))
		{
			fTgntY = lfTgntYs[lfDotNo - dotNo];
		}
		bool flag2;
		if (flag2 = OnRtTgnt(dotNo))
		{
			fTgntY = rtTgntYs[dotNo - rtDotNo];
		}
		return flag || flag2;
	}

	public bool VsContain(int dotNo)
	{
		return lfDotNo == dotNo || rtDotNo == dotNo || bsLfV.dotNo == dotNo || bsRtV.dotNo == dotNo;
	}
}
