using System;
using System.Collections;
using System.Drawing;
using System.IO;
using System.Media;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Xml.Serialization;
using IBrainChrom2018.Unit;

namespace IBrainChrom2018;

[Serializable]
[StructLayout(LayoutKind.Sequential)]
[XmlInclude(typeof(LbLine))]
[XmlInclude(typeof(LbText))]
[XmlInclude(typeof(GcProgTemp))]
[XmlInclude(typeof(LcGradient))]
[XmlInclude(typeof(Peak))]
[XmlInclude(typeof(VV))]
[XmlInclude(typeof(Injection))]
[XmlInclude(typeof(Integration))]
public class Signal : AiaShell
{
	[NonSerialized]
	[XmlIgnore]
	public ApplyIntegs applyIntegs_0 = new ApplyIntegs();

	private int int_2;

	private bool bool_0;

	private float float_0 = float.MinValue;

	public int detectorMark;

	public int disBeg;

	public Color disColor;

	public int disEnd;

	public PointF[] oriDots = new PointF[0];

	public PointF[] svDots;

	public PointF[] disPts = new PointF[0];

	public PointF[] dots = new PointF[0];

	public int DotsNum;

	public int[] endNs = new int[0];

	public static float fgTime = 0.03f;

	private VV vv_0 = new VV();

	public static float fgValue = 0.03f;

	[NonSerialized]
	[XmlIgnore]
	private FrameDis frameDis_0;

	public static int hwN = 3;

	public int instruMark;

	public LbLine[] lbLines = new LbLine[0];

	public LbText[] lbTexts = new LbText[0];

	public GcProgTemp linkGcProgTemp;

	public LcGradient linkLcGradient;

	private int int_3;

	private float float_1;

	public bool needReCalcuDis;

	private string string_2 = "";

	public Peak[] peaks = new Peak[0];

	public bool SampleV = true;

	private int int_4 = 16;

	public string sname = "";

	[NonSerialized]
	[XmlIgnore]
	public SoundPlayer soundPlayer_0 = new SoundPlayer();

	[NonSerialized]
	[XmlIgnore]
	public SoundPlayer soundPlayer_1 = new SoundPlayer();

	public int[] startNs = new int[0];

	private int int_5;

	private float float_2;

	private int int_6;

	private uint uint_2;

	private VV vv_1 = new VV();

	public float xMaxTime;

	public float xMinTime;

	public float xOffset;

	public float xScale = 1f;

	public float yMaxValue;

	public float yMaxValueTime;

	public float yMinValue;

	public float yOffset;

	public float yScale = 1f;

	public float ySrcMaxValueTime;

	public float MaxBlowV = -10f;

	public float MaxUpV = 500f;

	public float HoleT = 30f;

	public Chromatogram baseLine;

	public bool baseLinededuct;

	public bool ZeroUYCan;

	public float ZeroUY;

	public float EvenUY;

	public float EvenTimeIndex = -1f;

	public float deltaEvenUY;

	public DisLg disLg = default(DisLg);

	public bool simple;

	public bool StopAutoAlalyse;

	public bool StopAutoPrint;

	public bool StopAutoPutright;

	public Injection runningInjInfo = new Injection();

	public Integration RunningInteg = new Integration();

	public int SaveIndex;

	private DateTime dateTime_0 = DateTime.Now;

	public float _actual_sampling_interval => Math.Max(1E-05f, actual_sampling_interval.floats[0]);

	public float _detector_maximum_value => Math.Max(float.MinValue, detector_maximum_value.floats[0]);

	public float _detector_minimum_value => Math.Max(float.MinValue, detector_minimum_value.floats[0]);

	public float DefaultDisYBeg
	{
		get
		{
			if (float_0 != float.MinValue)
			{
				return float_0;
			}
			float num = 0f;
			int i;
			for (i = 0; i < 100 && i < DotsNum; i++)
			{
				num += dots[i].Y;
			}
			return float_0 = num / (float)i;
		}
	}

	public int FgState { get; set; }

	public int PeaksNum => peaks.Length;

	public float SecondY
	{
		get
		{
			float[] array = new float[PeaksNum];
			for (int i = 0; i < PeaksNum; i++)
			{
				array[i] = peaks[i].height;
			}
			if (array.Length != 0)
			{
				if (array.Length == 1)
				{
					if (peaks[0].pkN != uint.MaxValue)
					{
						return 0f;
					}
					return dots[peaks[0].pkN].Y;
				}
				Array.Sort(array);
				Array.Reverse(array);
				for (int j = 0; j < PeaksNum; j++)
				{
					if (peaks[j].height == array[1])
					{
						if (peaks[j].pkN < 0)
						{
							peaks[j].pkN = 0;
						}
						return dots[peaks[j].pkN].Y;
					}
				}
			}
			return yMaxValue;
		}
	}

	public Signal()
	{
		RunningInteg.Reset();
		applyIntegs_0.NewApplyIntegs();
	}

	public bool AddDot(float value, out PointF newDot)
	{
		if (DotsNum == 0)
		{
			int_2 = Environment.TickCount;
			disEnd = -1;
			disBeg = -1;
		}
		if (DotsNum > oriDots.Length)
		{
			Array.Resize(ref oriDots, oriDots.Length + 1000);
			Array.Resize(ref dots, oriDots.Length);
		}
		oriDots[DotsNum].X = Environment.TickCount - int_2;
		oriDots[DotsNum].Y = value;
		int dotsNum = DotsNum;
		float num = 0f;
		int num2 = 0;
		for (int i = 0; i < int_4; i++)
		{
			if (dotsNum < 0)
			{
				break;
			}
			num += oriDots[dotsNum--].Y;
			num2++;
		}
		if (DotsNum == 0)
		{
			int_6 = (int)oriDots[0].X;
			ref PointF reference = ref dots[0];
			float x = (oriDots[0].X = oriDots[0].X / 1000f / 60f);
			reference.X = x;
			float_2 = 0.0001f;
			int_5 = 0;
		}
		else if ((int)oriDots[DotsNum].X == int_6)
		{
			ref PointF reference2 = ref dots[DotsNum];
			float x = (oriDots[DotsNum].X = oriDots[int_5].X + float_2 * (float)(DotsNum - int_5));
			reference2.X = x;
		}
		else
		{
			int_6 = (int)oriDots[DotsNum].X;
			float x = (oriDots[DotsNum].X = oriDots[DotsNum].X / 1000f / 60f);
			float num6 = x;
			x = (dots[DotsNum].X = num6);
			float num8 = x;
			if (int_5 != DotsNum - 1)
			{
				float_2 = (num8 - oriDots[int_5].X) / (float)(DotsNum - int_5);
				for (int j = int_5 + 1; j < DotsNum; j++)
				{
					ref PointF reference3 = ref dots[j];
					x = (oriDots[j].X = oriDots[int_5].X + float_2 * (float)(j - int_5));
					reference3.X = x;
				}
			}
			int_5 = DotsNum;
		}
		dots[DotsNum].Y = num / (float)num2;
		newDot = dots[DotsNum];
		DotsNum++;
		return true;
	}

	public ArrayList AddDots(float[] values, int freq, out PointF newDot, bool bAnlyse, byte dataMark)
	{
		ArrayList arrayList = new ArrayList();
		if (dataMark == 80 || dataMark == 81 || dataMark == 82)
		{
			freq *= 5;
		}
		else
		{
			freq *= 10;
		}
		float num = 1f / (float)freq;
		if (ZeroUYCan)
		{
			for (int i = 0; i < values.Length; i++)
			{
				values[i] -= ZeroUY;
			}
		}
		if (DotsNum == 0)
		{
			int_2 = Environment.TickCount;
			disEnd = -1;
			disBeg = -1;
			int_3 = 0;
			Array.Resize(ref oriDots, 1000);
			Array.Resize(ref dots, 1000);
			ref PointF reference = ref oriDots[0];
			float x = (dots[0].X = 0f);
			reference.X = x;
			ref PointF reference2 = ref oriDots[0];
			x = (dots[0].Y = values[0]);
			reference2.Y = x;
			DotsNum++;
			newDot = dots[0];
			return null;
		}
		if (DotsNum + values.Length >= oriDots.Length)
		{
			Array.Resize(ref oriDots, oriDots.Length + 1000);
			Array.Resize(ref dots, oriDots.Length);
		}
		int num4 = Environment.TickCount - int_2;
		if (num4 == int_3)
		{
			newDot = dots[0];
			return null;
		}
		for (int j = 0; j < values.Length; j++)
		{
			int num5 = DotsNum + j;
			ref PointF reference3 = ref oriDots[num5];
			float x = (dots[num5].X = dots[num5 - 1].X + num / 60f);
			reference3.X = x;
			oriDots[num5].Y = values[j];
		}
		for (int k = 0; k < values.Length; k++)
		{
			dots[DotsNum + k].Y = values[k];
		}
		newDot = dots[DotsNum];
		DotsNum += values.Length;
		int_3 = num4;
		if (dots.Length > 1 && dots[0].Y == 0f)
		{
			dots[0].X = dots[1].X;
			dots[0].Y = dots[1].Y;
		}
		if (oriDots.Length > 1 && oriDots[0].Y == 0f)
		{
			oriDots[0].X = oriDots[1].X;
			oriDots[0].Y = oriDots[1].Y;
		}
		if (simple && bAnlyse && DateTime.Now.Subtract(dateTime_0).TotalSeconds > 30.0)
		{
			if (dots.Length != oriDots.Length)
			{
				Array.Resize(ref dots, DotsNum = oriDots.Length);
			}
			svDots = (PointF[])dots.Clone();
			IntegRow integRow = new IntegRow
			{
				oprtStyle = IntegOprtStyle.DtecDelay,
				value = 0f
			};
			RunningInteg.AppendRow(integRow);
			ApplyIntegs(RunningInteg, dots, bool_1: false);
			CalcuAreaPer();
			dateTime_0 = DateTime.Now;
		}
		return Smooth2(dataMark);
	}

	public bool AddDots(float[] values, int freq, out PointF newDot, bool bAnlyse, byte dataMark = 0)
	{
		try
		{
			if (dataMark == 80 || dataMark == 81 || dataMark == 82)
			{
				freq *= 5;
			}
			else
			{
				freq *= 10;
			}
			float num = 1f / (float)freq;
			if (ZeroUYCan)
			{
				for (int i = 0; i < values.Length; i++)
				{
					values[i] -= ZeroUY;
				}
			}
			if (DotsNum == 0)
			{
				int_2 = Environment.TickCount;
				disEnd = -1;
				disBeg = -1;
				int_3 = 0;
				Array.Resize(ref oriDots, 1000);
				Array.Resize(ref dots, 1000);
				ref PointF reference = ref oriDots[0];
				float x = (dots[0].X = 0f);
				reference.X = x;
				ref PointF reference2 = ref oriDots[0];
				x = (dots[0].Y = values[0]);
				reference2.Y = x;
				DotsNum++;
				newDot = dots[0];
				return false;
			}
			int num4 = Environment.TickCount - int_2;
			if (num4 == int_3)
			{
			}
			if (DotsNum + values.Length >= oriDots.Length)
			{
				Array.Resize(ref oriDots, oriDots.Length + 1000 + (DotsNum + values.Length - oriDots.Length));
				Array.Resize(ref dots, oriDots.Length);
			}
			if (dots.Length != oriDots.Length)
			{
				Array.Resize(ref dots, oriDots.Length);
			}
			for (int j = 0; j < values.Length; j++)
			{
				float num5 = dots[DotsNum + j - 1].X + 0.00083333335f;
				if (num5 < (float)(DotsNum + j) * (num / 60f))
				{
					num5 = (float)(DotsNum + j) * (num / 60f);
				}
				ref PointF reference3 = ref oriDots[DotsNum + j];
				float x = (dots[DotsNum + j].X = num5);
				reference3.X = x;
				oriDots[DotsNum + j].Y = values[j];
			}
			for (int k = 0; k < values.Length; k++)
			{
				dots[DotsNum + k].Y = values[k];
			}
			newDot = dots[DotsNum];
			DotsNum += values.Length;
			int_3 = num4;
			if (dots.Length > 1 && dots[0].Y == 0f)
			{
				dots[0].X = dots[1].X;
				dots[0].Y = dots[1].Y;
			}
			if (oriDots.Length > 1 && oriDots[0].Y == 0f)
			{
				oriDots[0].X = oriDots[1].X;
				oriDots[0].Y = oriDots[1].Y;
			}
			if (simple && bAnlyse && DateTime.Now.Subtract(dateTime_0).TotalSeconds > 30.0)
			{
				if (dots.Length != oriDots.Length)
				{
					Array.Resize(ref dots, DotsNum = oriDots.Length);
				}
				svDots = (PointF[])dots.Clone();
				IntegRow integRow = new IntegRow
				{
					oprtStyle = IntegOprtStyle.DtecDelay,
					value = 0f
				};
				RunningInteg.AppendRow(integRow);
				ApplyIntegs(RunningInteg, dots, bool_1: false);
				CalcuAreaPer();
				dateTime_0 = DateTime.Now;
			}
			Smooth(16);
		}
		catch (Exception ex)
		{
			LogMgr.Instance.LogError($"AddDots {ex.Message}");
			LogMgr.Instance.LogError($"AddDots values.Length{values.Length}");
			LogMgr.Instance.LogError($"AddDots dots.Length{dots.Length}");
			LogMgr.Instance.LogError($"AddDots oriDots.Length{oriDots.Length}");
			LogMgr.Instance.LogError($"AddDots DotsNum{DotsNum}");
			LogMgr.Instance.LogError($"AddDots {ex.StackTrace}");
			newDot = dots[0];
		}
		return true;
	}

	public bool AddDots(float[] values, int freq, out PointF newDot, bool bAnlyse, int iSmooth, byte dataMark = 0)
	{
		try
		{
			if (dataMark == 80 || dataMark == 81 || dataMark == 82)
			{
				freq *= 5;
			}
			else
			{
				freq *= 10;
			}
			float num = 1f / (float)freq;
			if (ZeroUYCan)
			{
				for (int i = 0; i < values.Length; i++)
				{
					values[i] -= ZeroUY;
				}
			}
			if (DotsNum == 0)
			{
				int_2 = Environment.TickCount;
				disEnd = -1;
				disBeg = -1;
				int_3 = 0;
				Array.Resize(ref oriDots, 1000);
				Array.Resize(ref dots, 1000);
				ref PointF reference = ref oriDots[0];
				float x = (dots[0].X = 0f);
				reference.X = x;
				ref PointF reference2 = ref oriDots[0];
				x = (dots[0].Y = values[0]);
				reference2.Y = x;
				DotsNum++;
				newDot = dots[0];
				return false;
			}
			int num4 = Environment.TickCount - int_2;
			if (num4 == int_3)
			{
			}
			if (DotsNum + values.Length >= oriDots.Length)
			{
				Array.Resize(ref oriDots, oriDots.Length + 1000 + (DotsNum + values.Length - oriDots.Length));
				Array.Resize(ref dots, oriDots.Length);
			}
			if (dots.Length != oriDots.Length)
			{
				Array.Resize(ref dots, oriDots.Length);
			}
			for (int j = 0; j < values.Length; j++)
			{
				float num5 = dots[DotsNum + j - 1].X + 0.00083333335f;
				if (num5 < (float)(DotsNum + j) * (num / 60f))
				{
					num5 = (float)(DotsNum + j) * (num / 60f);
				}
				ref PointF reference3 = ref oriDots[DotsNum + j];
				float x = (dots[DotsNum + j].X = num5);
				reference3.X = x;
				oriDots[DotsNum + j].Y = values[j];
			}
			for (int k = 0; k < values.Length; k++)
			{
				dots[DotsNum + k].Y = values[k];
			}
			newDot = dots[DotsNum];
			DotsNum += values.Length;
			int_3 = num4;
			if (dots.Length > 1 && dots[0].Y == 0f)
			{
				dots[0].X = dots[1].X;
				dots[0].Y = dots[1].Y;
			}
			if (oriDots.Length > 1 && oriDots[0].Y == 0f)
			{
				oriDots[0].X = oriDots[1].X;
				oriDots[0].Y = oriDots[1].Y;
			}
			if (simple && bAnlyse && DateTime.Now.Subtract(dateTime_0).TotalSeconds > 30.0)
			{
				if (dots.Length != oriDots.Length)
				{
					Array.Resize(ref dots, DotsNum = oriDots.Length);
				}
				svDots = (PointF[])dots.Clone();
				IntegRow integRow = new IntegRow
				{
					oprtStyle = IntegOprtStyle.DtecDelay,
					value = 0f
				};
				RunningInteg.AppendRow(integRow);
				ApplyIntegs(RunningInteg, dots, bool_1: false);
				CalcuAreaPer();
				dateTime_0 = DateTime.Now;
			}
			Smooth(iSmooth);
		}
		catch (Exception ex)
		{
			LogMgr.Instance.LogError($"AddDots {ex.Message}");
			LogMgr.Instance.LogError($"AddDots values.Length{values.Length}");
			LogMgr.Instance.LogError($"AddDots dots.Length{dots.Length}");
			LogMgr.Instance.LogError($"AddDots oriDots.Length{oriDots.Length}");
			LogMgr.Instance.LogError($"AddDots DotsNum{DotsNum}");
			LogMgr.Instance.LogError($"AddDots {ex.StackTrace}");
			newDot = dots[0];
		}
		return true;
	}

	public void ClearPeak()
	{
		Array.Resize(ref peaks, 0);
	}

	public void AddLbLine(LbLine lbLine)
	{
		int num = lbLines.Length;
		Array.Resize(ref lbLines, num + 1);
		lbLines[num] = lbLine;
	}

	public void AddLbText(LbText lbText)
	{
		int num = lbTexts.Length;
		Array.Resize(ref lbTexts, num + 1);
		lbTexts[num] = lbText;
	}

	public bool ApplyIntegs(Integration integs, PointF[] subDots, bool bool_1)
	{
		applyIntegs_0.Integs = integs;
		if (svDots != null && svDots.Length != dots.Length)
		{
			svDots = (PointF[])dots.Clone();
		}
		bool result = applyIntegs_0.Apply(dots, svDots, DefaultDisYBeg, subDots, bool_1);
		peaks = applyIntegs_0.Peaks;
		refresh_TimeValue();
		return result;
	}

	public void CalcuAreaPer()
	{
		float num = 0f;
		for (int i = 0; i < peaks.Length; i++)
		{
			num += peaks[i].area;
		}
		for (int j = 0; j < peaks.Length; j++)
		{
			peaks[j].areaPer = peaks[j].area / num;
		}
	}

	public void ClickLb(Point ptMouse)
	{
		for (int i = 0; i < lbTexts.Length; i++)
		{
			RectangleF rectangleF = default(RectangleF);
			rectangleF.Offset(lbTexts[i].disPt);
			float width = (rectangleF.Height = 30f);
			rectangleF.Width = width;
			lbTexts[i].selected = rectangleF.Contains(ptMouse);
			if (lbTexts[i].selected)
			{
				break;
			}
		}
	}

	public void CutAllLbs()
	{
		Array.Resize(ref lbTexts, 0);
		Array.Resize(ref lbLines, 0);
	}

	public void CutSelectedLbs()
	{
		for (int i = 0; i < lbTexts.Length; i++)
		{
			if (lbTexts[i].selected)
			{
				int num = lbTexts.Length - 1;
				lbTexts[i] = lbTexts[num];
				Array.Resize(ref lbTexts, num);
				i--;
			}
		}
		for (int j = 0; j < lbLines.Length; j++)
		{
			if (lbLines[j].selected)
			{
				int num2 = lbLines.Length - 1;
				lbLines[j] = lbLines[num2];
				Array.Resize(ref lbLines, num2);
				j--;
			}
		}
	}

	public void data_AIA(AccStyle accStyle, Injection injAnalysis)
	{
		switch (accStyle)
		{
		case AccStyle.Read:
			data_AIA(accStyle);
			injAnalysis.sampleID = sample_id;
			injAnalysis.sample = sample_name;
			injAnalysis.amount = Class49.String2Float(sample_amount, 0f);
			injAnalysis.ISTD_amount = Class49.String2Float(sample_istd_amount, 0f);
			injAnalysis.dilution = Class49.String2Float(sample_dilution, 1f);
			injAnalysis.inj_volume = Class49.String2Float(sample_injection_volume, 1f);
			if (sample_cali_stand.Equals("Y"))
			{
				injAnalysis.cali_stand = true;
			}
			else
			{
				injAnalysis.cali_stand = false;
			}
			injAnalysis.gpc_k = Class49.String2Float(sample_gpc_k, 0f);
			injAnalysis.gpc_alpha = Class49.String2Float(sample_gpc_alpha, 0f);
			injAnalysis.fileName = file_name;
			if (detectorStyle == DetectorStyle.General)
			{
				float[] array3 = ordinate_times.floats;
				float[] floats = ordinate_values.floats;
				if (array3.Length != floats.Length)
				{
					Array.Resize(ref array3, floats.Length);
					float num = _actual_sampling_interval;
					float num2 = 0f;
					for (int j = 0; j < array3.Length; j++)
					{
						array3[j] = num2 / 60f;
						num2 += num;
					}
					ordinate_times.floats = array3;
				}
				Array.Resize(ref oriDots, floats.Length);
				for (int k = 0; k < oriDots.Length; k++)
				{
					oriDots[k].X = array3[k];
					oriDots[k].Y = floats[k];
				}
				Smooth(16);
			}
			else if (detectorStyle != DetectorStyle.DAD)
			{
			}
			break;
		case AccStyle.Write:
			sample_id = injAnalysis.sampleID;
			sample_name = injAnalysis.sample;
			sample_amount = injAnalysis.amount.ToString();
			sample_istd_amount = injAnalysis.ISTD_amount.ToString();
			sample_dilution = injAnalysis.dilution.ToString();
			sample_injection_volume = injAnalysis.inj_volume.ToString();
			sample_cali_stand = (injAnalysis.cali_stand ? "Y" : "N");
			sample_gpc_k = injAnalysis.gpc_k.ToString();
			sample_gpc_alpha = injAnalysis.gpc_alpha.ToString();
			file_name = injAnalysis.fileName;
			if (detectorStyle == DetectorStyle.General)
			{
				float[] array = new float[oriDots.Length];
				float[] array2 = new float[oriDots.Length];
				for (int i = 0; i < array2.Length; i++)
				{
					array[i] = oriDots[i].X;
					array2[i] = oriDots[i].Y;
				}
				ordinate_times.floats = array;
				ordinate_values.floats = array2;
			}
			data_AIA(accStyle);
			break;
		}
	}

	private void method_0()
	{
		uint_2 = GetTickCount();
	}

	private int[] method_1(int[] int_7, int[] int_8)
	{
		int[] array = new int[0];
		for (int i = 0; i < int_7.Length; i++)
		{
			if (!Class49.ValueInArray(int_8, int_7[i]))
			{
				Array.Resize(ref array, array.Length + 1);
				array[array.Length - 1] = int_7[i];
			}
		}
		return array;
	}

	private void method_2(int[] int_7)
	{
		if (int_7.Length != 0)
		{
			string text = "";
			for (int i = 0; i < int_7.Length; i++)
			{
				text = text + "\n" + int_7[i];
			}
			MessageBox.Show(text);
		}
	}

	private void method_3()
	{
		MessageBox.Show("耗时 " + (GetTickCount() - uint_2));
	}

	public bool Equals(Signal signal)
	{
		if (int_4 == signal.int_4 && dots.Length == signal.dots.Length)
		{
			for (int i = 0; i < dots.Length; i++)
			{
				if (dots[i] != signal.dots[i])
				{
					return false;
				}
			}
			return true;
		}
		return false;
	}

	public float getAy(float timeA, float timeB)
	{
		return applyIntegs_0.getAy(timeA, timeB);
	}

	public int getDotNo(float minute)
	{
		if (float.IsNaN(minute))
		{
			minute = 0f;
		}
		return applyIntegs_0.getDotNo(minute);
	}

	public float getDotY(float minute)
	{
		return applyIntegs_0.getDotY(minute);
	}

	public float getHy(float timeA, float timeB)
	{
		return applyIntegs_0.getHy(timeA, timeB);
	}

	public void GetRunFileName()
	{
		string_2 = ResourceImageLoad.ExePath() + "Station\\TMP\\" + instruMark + "_" + detectorMark + ".raw";
	}

	[DllImport("kernel32.dll")]
	private static extern uint GetTickCount();

	public float getWx(float timeA, float timeB)
	{
		return applyIntegs_0.getWx(timeA, timeB);
	}

	public bool JudgeBegEnd(FrameDis frameDis)
	{
		frameDis_0 = frameDis;
		disBeg = -1;
		if (DotsNum > dots.Length)
		{
			if (DotsNum > oriDots.Length)
			{
				Array.Resize(ref oriDots, oriDots.Length + 1000);
			}
			Array.Resize(ref dots, oriDots.Length);
		}
		for (int i = 0; i < DotsNum; i++)
		{
			if (dots[i].X >= frameDis.disLg.lgXBeg)
			{
				disBeg = i;
				break;
			}
		}
		disEnd = -1;
		for (int num = DotsNum - 1; num >= 0; num--)
		{
			if (dots[num].X <= frameDis.disLg.lgXBeg + frameDis.disLg.lgX)
			{
				disEnd = num;
				break;
			}
		}
		if (disBeg >= 0 && disEnd >= 0)
		{
			Array.Resize(ref disPts, disEnd - disBeg + 1);
			return true;
		}
		Array.Resize(ref disPts, 0);
		return false;
	}

	public void JudgeFG(bool needGather)
	{
		int num = DotsNum - 1 - hwN;
		if (num < 0)
		{
			return;
		}
		applyIntegs_0.getVV(ref vv_1, dots, DotsNum, num, float.NaN, hwN);
		if (FgState == 0)
		{
			if ((vv_1.vs_0 == VS.PL || vv_1.vs_0 == VS.V) && (vv_0.index < 0 || vv_1.Y < vv_0.Y))
			{
				vv_0.LoadFromObject(vv_1);
				float_1 = vv_0.Y;
			}
			if (vv_0.index >= 0)
			{
				PointF pointF = dots[DotsNum - 1];
				float_1 = Math.Max(float_1, pointF.Y);
				if (pointF.Y - vv_0.Y > fgValue || (pointF.Y > vv_0.Y && pointF.X - vv_0.X > fgTime))
				{
					int num2 = startNs.Length;
					Array.Resize(ref startNs, num2 + 1);
					startNs[num2] = vv_0.index;
					FgState = 1;
					vv_0.index = -1;
				}
			}
		}
		else
		{
			if (FgState != 1)
			{
				return;
			}
			PointF pointF2 = dots[DotsNum - 1];
			float_1 = Math.Max(float_1, pointF2.Y);
			if ((vv_1.vs_0 == VS.PR || vv_1.vs_0 == VS.V) && float_1 > pointF2.Y && (vv_0.index < 0 || vv_1.Y < vv_0.Y))
			{
				vv_0.LoadFromObject(vv_1);
			}
			if (vv_0.index < 0)
			{
				return;
			}
			if (pointF2.Y - vv_0.Y > fgValue)
			{
				int num3 = endNs.Length;
				Array.Resize(ref endNs, num3 + 1);
				endNs[num3] = vv_0.index;
				if (needGather)
				{
					num3 = startNs.Length;
					Array.Resize(ref startNs, num3 + 1);
					startNs[num3] = vv_0.index;
					float_1 = pointF2.Y;
				}
				else
				{
					FgState = 0;
					vv_0.index = -1;
				}
			}
			if (pointF2.X - vv_0.X > fgTime)
			{
				int num4 = endNs.Length;
				Array.Resize(ref endNs, num4 + 1);
				endNs[num4] = vv_0.index;
				FgState = 0;
				vv_0.index = -1;
			}
		}
	}

	public bool ReCalcuDis()
	{
		if (needReCalcuDis)
		{
			needReCalcuDis = false;
			return true;
		}
		for (int i = 0; i < lbTexts.Length; i++)
		{
			if (float.IsNaN(lbTexts[i].disPt.X))
			{
				return true;
			}
		}
		for (int j = 0; j < lbLines.Length; j++)
		{
			if (float.IsNaN(lbLines[j].disPt.X))
			{
				return true;
			}
		}
		return false;
	}

	public void refresh_TimeValue()
	{
		yMinValue = float.MaxValue;
		yMaxValue = float.MinValue;
		for (int i = 1; i < DotsNum && dots.Length > i; i++)
		{
			yMinValue = Math.Min(yMinValue, dots[i].Y);
			if (dots[i].Y > yMaxValue)
			{
				yMaxValue = dots[i].Y;
				yMaxValueTime = dots[i].X;
			}
		}
		if (DotsNum > 0 && dots.Length >= DotsNum)
		{
			xMinTime = dots[0].X;
			xMaxTime = dots[DotsNum - 1].X;
		}
		if (yMinValue == float.MaxValue)
		{
			yMinValue = 0f;
		}
		if (yMaxValue == float.MinValue)
		{
			yMaxValue = 20f;
		}
	}

	public void Reset_xyOffsetScale()
	{
		xOffset = (yOffset = 0f);
		xScale = (yScale = 1f);
	}

	public void ResetOriDots(bool createDiskFile)
	{
		bool_0 = createDiskFile;
		Array.Resize(ref oriDots, 0);
		Array.Resize(ref dots, 0);
		DotsNum = 0;
		Array.Resize(ref disPts, 0);
		if (createDiskFile)
		{
			int_4 = 16;
		}
		else
		{
			int_4 = 16;
		}
		FgState = 0;
		vv_0.index = -1;
		Array.Resize(ref startNs, 0);
		Array.Resize(ref endNs, 0);
	}

	public void ResetSelectLbs()
	{
		for (int i = 0; i < lbTexts.Length; i++)
		{
			lbTexts[i].selected = false;
		}
		for (int j = 0; j < lbLines.Length; j++)
		{
			lbLines[j].selected = false;
		}
	}

	public ArrayList Smooth2(int int_7)
	{
		ArrayList arrayList = new ArrayList();
		ArrayList arrayList2 = new ArrayList();
		ArrayList arrayList3 = new ArrayList();
		if (int_7 <= 0)
		{
			return null;
		}
		int_7 = ((int_7 != 64 && int_7 != 65 && int_7 != 66) ? ((int_7 == 80 || int_7 == 81 || int_7 == 82) ? 1 : 16) : 16);
		int_4 = int_7;
		double num = 0.0;
		if (dots.Length != oriDots.Length)
		{
			DotsNum = oriDots.Length;
			Array.Resize(ref dots, DotsNum);
		}
		for (int i = 0; i < oriDots.Length; i++)
		{
			dots[i].X = oriDots[i].X;
			dots[i].Y = oriDots[i].Y;
			num += (double)oriDots[i].Y;
			if (i >= int_7)
			{
				num -= (double)oriDots[i - int_7].Y;
				dots[i].Y = Convert.ToSingle(num / Math.Max(int_7, 1.0));
			}
			arrayList3.Add(dots[i].Y);
			arrayList2.Add(dots[i].X);
		}
		if (dots.Length > int_4)
		{
			for (int j = 0; j < int_4; j++)
			{
				dots[j].Y = dots[int_4].Y;
				arrayList3[j] = dots[j].Y;
			}
		}
		svDots = (PointF[])dots.Clone();
		float_0 = float.MinValue;
		refresh_TimeValue();
		ySrcMaxValueTime = yMaxValueTime;
		arrayList.Add(arrayList2);
		arrayList.Add(arrayList3);
		return arrayList;
	}

	public void Smooth(int int_7)
	{
		int i = 0;
		try
		{
			if (int_7 <= 0)
			{
				return;
			}
			if (int_7 == 0)
			{
				int_7 = 1;
			}
			int_4 = int_7;
			double num = 0.0;
			if (dots.Length != oriDots.Length)
			{
				Array.Resize(ref dots, oriDots.Length);
			}
			try
			{
				for (i = 0; i < DotsNum; i++)
				{
					if (oriDots[i].X == 0f && i > 0)
					{
						oriDots[i].X = oriDots[i - 1].X + 0.00083333335f;
						oriDots[i].Y = oriDots[i - 1].Y;
					}
					dots[i].X = oriDots[i].X;
					dots[i].Y = oriDots[i].Y;
					num += (double)oriDots[i].Y;
					if (i >= int_7)
					{
						num -= (double)oriDots[i - int_7].Y;
						dots[i].Y = (int)(Convert.ToSingle(num / Math.Max(int_7, 1.0)) * 1000f);
						dots[i].Y /= 1000f;
					}
				}
			}
			catch (Exception ex)
			{
				LogMgr.Instance.LogError($"Smooth {ex.Message}");
				LogMgr.Instance.LogError($"Smooth dots.Length{dots.Length}");
				LogMgr.Instance.LogError($"Smooth oriDots.Length{oriDots.Length}");
				LogMgr.Instance.LogError($"Smooth i= {i}");
			}
			if (dots.Length > int_4)
			{
				for (int j = 0; j < int_4; j++)
				{
					dots[j].Y = dots[int_4].Y;
				}
			}
			svDots = (PointF[])dots.Clone();
			float_0 = float.MinValue;
			refresh_TimeValue();
			ySrcMaxValueTime = yMaxValueTime;
		}
		catch (Exception ex2)
		{
			LogMgr.Instance.LogError($"Smooth {ex2.Message}");
			LogMgr.Instance.LogError($"Smooth dots.Length{dots.Length}");
			LogMgr.Instance.LogError($"Smooth oriDots.Length{oriDots.Length}");
			LogMgr.Instance.LogError($"Smooth {ex2.StackTrace}");
		}
	}

	public void Test()
	{
	}

	public void TransToLast()
	{
		GetRunFileName();
		string destFileName = "_" + string_2;
		File.Move(string_2, destFileName);
	}

	public void WriteDisplay(int dotNo)
	{
		int num = dotNo - disBeg;
		if (dotNo > disBeg)
		{
			disPts[num].X = frameDis_0.frmRC.Left + frameDis_0.fX * (dots[dotNo].X - frameDis_0.disLg.lgXBeg);
			disPts[num].Y = frameDis_0.frmRC.Bottom - frameDis_0.fY * (dots[dotNo].Y - frameDis_0.disLg.lgYBeg);
			if (dotNo == 1)
			{
				disPts[0].X = disPts[1].X;
				disPts[0].Y = disPts[1].Y;
			}
		}
	}

	public void WriteDisplay(int dotNo, float YPara, float float_3)
	{
		if (dotNo > disBeg)
		{
			int num = dotNo - disBeg;
			disPts[num].X = frameDis_0.frmRC.Left + frameDis_0.fX * (dots[dotNo].X - frameDis_0.disLg.lgXBeg);
			disPts[num].Y = frameDis_0.frmRC.Bottom - frameDis_0.fY * (dots[dotNo].Y * YPara - float_3 - frameDis_0.disLg.lgYBeg);
			if (dotNo == 1)
			{
				disPts[0].X = disPts[1].X;
				disPts[0].Y = disPts[1].Y;
			}
		}
	}

	public void LoadFromFile(BinaryReader binaryReader_0)
	{
		detectorStyle = (DetectorStyle)binaryReader_0.ReadInt32();
		detectorMark = binaryReader_0.ReadInt32();
		DotsNum = binaryReader_0.ReadInt32();
		Array.Resize(ref oriDots, DotsNum);
		for (int i = 0; i < oriDots.Length; i++)
		{
			oriDots[i].X = binaryReader_0.ReadSingle();
			oriDots[i].Y = binaryReader_0.ReadSingle();
		}
		Array.Resize(ref dots, DotsNum);
		Smooth(binaryReader_0.ReadInt32());
	}

	public void LoadFromFile_N2k(BinaryReader binaryReader_0, FileStream fileStream_0)
	{
		sample_name = "";
		DotsNum = Convert.ToInt32(fileStream_0.Length / 8);
		if (DotsNum <= 0)
		{
			throw new Exception("N2000文件异常！");
		}
		Array.Resize(ref oriDots, DotsNum);
		Array.Resize(ref dots, DotsNum);
		float num = 0f;
		for (int i = 0; i < oriDots.Length; i++)
		{
			ref PointF reference = ref oriDots[i];
			float x = (dots[i].X = num);
			reference.X = x;
			oriDots[i].Y = (float)binaryReader_0.ReadInt32() / 1000f;
			fileStream_0.Seek(4L, SeekOrigin.Current);
			num += 0.001666667f;
		}
		Smooth(16);
	}

	public void LoadFromFile_Org(BinaryReader binaryReader_0, FileStream fileStream_0)
	{
		sample_name = "";
		DotsNum = binaryReader_0.ReadInt32();
		if (DotsNum <= 0)
		{
			throw new Exception("N2000文件异常！");
		}
		Array.Resize(ref oriDots, DotsNum);
		Array.Resize(ref dots, DotsNum);
		float num = 0f;
		for (int i = 0; i < oriDots.Length; i++)
		{
			ref PointF reference = ref oriDots[i];
			float x = (dots[i].X = num);
			reference.X = x;
			oriDots[i].Y = (float)binaryReader_0.ReadInt32() / 1000f;
			num += 0.001666667f;
		}
		Smooth(16);
	}

	public void LoadFromFile_hw(BinaryReader binaryReader_0, FileStream fileStream_0)
	{
		binaryReader_0.ReadInt32();
		binaryReader_0.ReadInt32();
		binaryReader_0.ReadInt32();
		binaryReader_0.ReadByte();
		sample_name = "";
		DotsNum = binaryReader_0.ReadInt32();
		if (DotsNum <= 0)
		{
			throw new Exception("千谱文件异常！");
		}
		Array.Resize(ref oriDots, DotsNum);
		Array.Resize(ref dots, DotsNum);
		float num = 0f;
		for (int i = 0; i < oriDots.Length; i++)
		{
			ref PointF reference = ref oriDots[i];
			float x = (dots[i].X = num);
			reference.X = x;
			oriDots[i].Y = (float)binaryReader_0.ReadInt32() / 1000f;
			num += 0.0008333335f;
		}
		Smooth(16);
	}

	public void SaveToFile(BinaryWriter binaryWriter_0)
	{
		binaryWriter_0.Write((int)detectorStyle);
		binaryWriter_0.Write(detectorMark);
		binaryWriter_0.Write(DotsNum);
		for (int i = 0; i < DotsNum; i++)
		{
			binaryWriter_0.Write(oriDots[i].X);
			binaryWriter_0.Write(oriDots[i].Y);
		}
		binaryWriter_0.Write(int_4);
	}
}
