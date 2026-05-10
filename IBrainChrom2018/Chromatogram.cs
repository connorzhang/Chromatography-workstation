using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Media;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Xml.Serialization;
using IBrainChrom2018.ChromFile;
using IBrainChrom2018.Unit;

namespace IBrainChrom2018;

[Serializable]
[StructLayout(LayoutKind.Sequential)]
[XmlInclude(typeof(CaliGnl))]
[XmlInclude(typeof(ChromInfo))]
[XmlInclude(typeof(ChromInfoR))]
[XmlInclude(typeof(Injection))]
[XmlInclude(typeof(Integration))]
[XmlInclude(typeof(Peak))]
[XmlInclude(typeof(Signal))]
[XmlInclude(typeof(UserArchive))]
public class Chromatogram : IBaseFileMgr
{
	[NonSerialized]
	[XmlIgnore]
	public const string cdfFileExt = ".cdf";

	[NonSerialized]
	[XmlIgnore]
	public const string chromFileExt = ".sda";

	[NonSerialized]
	[XmlIgnore]
	public const string n2kFileExt = ".dat";

	private PointF[] pointF_0;

	private float float_0;

	private PointF[] pointF_1;

	public bool bEnNMHC = false;

	public bool canSetRs = true;

	public string cus1_formula = "";

	public string cus1_name = Lang.PS("自定义1", "Custom1");

	public string cus2_formula = "";

	public string cus2_name = Lang.PS("自定义2", "Custom2");

	public string directory = "";

	public string fName = "g";

	public string fullName = "g";

	public string SDAVER = "";

	private byte[] byte_1;

	private byte[] byte_2;

	public InsDeviceManager devManager = new InsDeviceManager();

	public int idxUserArchive = -1;

	public Signal signal = new Signal();

	public Peak[] RltPeaks;

	public Peak[] AdjustRltPeaksPara = new Peak[0];

	public Injection injAnalysis = new Injection();

	public string[] nrUserNames = new string[0];

	public DateTime openTime;

	public DisLg disLg = default(DisLg);

	public MtdSetup mtdSetup = new MtdSetup();

	public PrintPara ChromPPara = new PrintPara();

	public UserArchive[] userArchives = new UserArchive[0];

	public float whlHheatVaue;

	public float whlLheatVaue;

	public float whlAmount;

	public float whlAmountPer;

	public float whlArea;

	public float whlAreaPer;

	public float whlHeight;

	public float whlHeightPer;

	public string[] wUserNames = new string[0];

	public CaliGnl caliGnl
	{
		get
		{
			return mtdSetup.caliGnl;
		}
		set
		{
			mtdSetup.caliGnl = value;
		}
	}

	public PrintPara PPara
	{
		get
		{
			return mtdSetup.printPara;
		}
		set
		{
			mtdSetup.printPara = value;
		}
	}

	public ChromInfo chromInfo => mtdSetup.chromInfo;

	public ChromInfoR chromInfoR => mtdSetup.chromInfoR;

	public Integration integ => mtdSetup.sigIntegrations[0];

	public string AmountUnit
	{
		get
		{
			string result = "";
			if (chromInfo.cclCalcu != CalcuStyle.Uncal && caliGnl != null)
			{
				result = caliGnl.caliOption.cmpdUnit;
			}
			return result;
		}
	}

	public int IstdNum { get; set; }

	public int PeaksNum
	{
		get
		{
			if (RltPeaks == null)
			{
				return 0;
			}
			return RltPeaks.Length;
		}
	}

	public Chromatogram()
	{
		m_strExt = "sda";
		m_strFileTypeName = Lang.PS("谱图文件");
	}

	public void CalcuCus()
	{
		for (int i = 0; i < PeaksNum; i++)
		{
			Peak peak = RltPeaks[i];
			peak.cus1 = (peak.cus2 = float.NaN);
		}
		for (int j = 0; j < PeaksNum; j++)
		{
			RltPeaks[j].CalcuCus(cus1_formula, cus2_formula);
		}
	}

	public void CalcuPerformanceAndCus()
	{
		for (int i = 0; i < PeaksNum; i++)
		{
			Peak peak = RltPeaks[i];
			peak.Capacity = (peak.pkRT - chromInfo.ccColumnUT) / chromInfo.ccColumnUT;
			peak.Eff_Column_EP = peak.Efficiency_EP * 1000f / chromInfo.ccColumnLength;
			peak.HETP_EP = chromInfo.ccColumnLength / peak.Efficiency_EP;
			peak.Eff_Column_USP = peak.Efficiency_USP * 1000f / chromInfo.ccColumnLength;
			peak.HETP_USP = chromInfo.ccColumnLength / peak.Efficiency_USP;
			peak.Eff_Column_JP = peak.Efficiency_JP * 1000f / chromInfo.ccColumnLength;
			peak.HETP_JP = chromInfo.ccColumnLength / peak.Efficiency_JP;
		}
		CalcuCus();
	}

	private void CalcuResults_LC()
	{
		int i = 0;
		int numIndex = -1;
		FormMainParam formMainParam = FormMainParam.Create();
		if (formMainParam.bChanne2)
		{
			for (; i < signal.PeaksNum; i++)
			{
				caliGnl.SetCompoundTimeMini(signal.peaks[i], signal.peaks, numIndex);
			}
		}
		else
		{
			for (; i < signal.PeaksNum; i++)
			{
				numIndex = caliGnl.SetCompound(signal.peaks[i], signal.peaks, numIndex);
			}
		}
		if (chromInfo.cclCalcu != CalcuStyle.Uncal)
		{
			if (chromInfo.cclCalcu == CalcuStyle.ISTD)
			{
				IstdNum = 0;
				for (int j = 0; j < signal.PeaksNum; j++)
				{
					if (signal.peaks[j].IsIstd)
					{
						IstdNum++;
					}
				}
				IstdNum = 1;
				if (IstdNum == 1)
				{
					for (int k = 0; k < signal.PeaksNum; k++)
					{
						if (signal.peaks[k].IsIstd)
						{
							float istdAmount = chromInfo.GetIstdAmount(0);
							if (istdAmount > 0f)
							{
								signal.peaks[k].amount = istdAmount;
							}
							else
							{
								signal.peaks[k].CalcuResults(signal, CalcuStyle.ESTD);
							}
						}
					}
				}
				if (IstdNum > 1)
				{
					for (int l = 0; l < signal.PeaksNum; l++)
					{
						if (signal.peaks[l].IsIstd)
						{
							float istdAmount2 = chromInfo.GetIstdAmount(signal.peaks[l].pkRT);
							if (istdAmount2 > 0f)
							{
								signal.peaks[l].amount = istdAmount2;
							}
							else
							{
								signal.peaks[l].CalcuResults(signal, CalcuStyle.ESTD);
							}
						}
					}
				}
			}
			for (int m = 0; m < signal.PeaksNum; m++)
			{
				if (signal.peaks[m].compound != null)
				{
					if (signal.peaks[m].compound.cmpdInfo.respStyle == RespStyle.Area)
					{
						signal.peaks[m].respStyle = 0;
					}
					else if (signal.peaks[m].compound.cmpdInfo.respStyle == RespStyle.Height)
					{
						signal.peaks[m].respStyle = 1;
					}
					else if (signal.peaks[m].compound.cmpdInfo.respStyle == RespStyle.AreaSquare)
					{
						signal.peaks[m].respStyle = 2;
					}
					else if (signal.peaks[m].compound.cmpdInfo.respStyle == RespStyle.PeakHeightSquare)
					{
						signal.peaks[m].respStyle = 3;
					}
					signal.peaks[m].CalcuResults(signal, chromInfo.cclCalcu);
					continue;
				}
				if (chromInfo.prsUncalBase == RespStyle.Area)
				{
					signal.peaks[m].respStyle = 0;
				}
				else if (chromInfo.prsUncalBase == RespStyle.Height)
				{
					signal.peaks[m].respStyle = 1;
				}
				else if (chromInfo.prsUncalBase == RespStyle.AreaSquare)
				{
					signal.peaks[m].respStyle = 2;
				}
				else if (chromInfo.prsUncalBase == RespStyle.PeakHeightSquare)
				{
					signal.peaks[m].respStyle = 3;
				}
				if (chromInfo.prsUncalBase == RespStyle.Area)
				{
					signal.peaks[m].amount = 0f;
				}
				if (chromInfo.prsUncalBase == RespStyle.Height)
				{
					signal.peaks[m].amount = 0f;
				}
				if (chromInfo.prsUncalBase == RespStyle.AreaSquare)
				{
					signal.peaks[m].amount = 0f;
				}
				if (chromInfo.prsUncalBase == RespStyle.PeakHeightSquare)
				{
					signal.peaks[m].amount = 0f;
				}
			}
			if (chromInfo.prsUseScaleFactor)
			{
				for (int n = 0; n < signal.PeaksNum; n++)
				{
					if (signal.peaks[n].amount > 0f)
					{
						Peak peak = signal.peaks[n];
						if (chromInfo.prsScaleFactor != 0f)
						{
							peak.amount /= chromInfo.prsScaleFactor;
						}
					}
				}
			}
			if (chromInfo.dilution != 1f)
			{
				for (int num = 0; num < signal.PeaksNum; num++)
				{
					if (signal.peaks[num].amount > 0f)
					{
						Peak peak2 = signal.peaks[num];
						peak2.amount *= chromInfo.dilution;
					}
				}
			}
			if (chromInfo.rtrRltReportPeaks == RltReportPeaks.AllDetectedPeaks)
			{
				for (int num2 = 0; num2 < signal.PeaksNum; num2++)
				{
					if (chromInfo.cclCalcu != CalcuStyle.ISTD || !chromInfo.rtrHideISTDPeak || !signal.peaks[num2].IsIstd)
					{
						int num3 = RltPeaks.Length;
						Array.Resize(ref RltPeaks, num3 + 1);
						RltPeaks[num3] = signal.peaks[num2];
						RltPeaks[num3].disNo = RltPeaks.Length;
					}
				}
			}
			else if (chromInfo.rtrRltReportPeaks == RltReportPeaks.IdentifiedPeaks)
			{
				for (int num4 = 0; num4 < signal.PeaksNum; num4++)
				{
					if ((chromInfo.cclCalcu != CalcuStyle.ISTD || !chromInfo.rtrHideISTDPeak || !signal.peaks[num4].IsIstd) && signal.peaks[num4].IsIdentified)
					{
						int num5 = RltPeaks.Length;
						Array.Resize(ref RltPeaks, num5 + 1);
						RltPeaks[num5] = signal.peaks[num4];
						RltPeaks[num5].disNo = RltPeaks.Length;
					}
				}
			}
			else
			{
				if (chromInfo.rtrRltReportPeaks != RltReportPeaks.CaliPeaks)
				{
					return;
				}
				for (int num6 = 0; num6 < signal.PeaksNum; num6++)
				{
					if ((chromInfo.cclCalcu != CalcuStyle.ISTD || !chromInfo.rtrHideISTDPeak || !signal.peaks[num6].IsIstd) && signal.peaks[num6].compound != null)
					{
						int num7 = RltPeaks.Length;
						Array.Resize(ref RltPeaks, num7 + 1);
						RltPeaks[num7] = signal.peaks[num6];
					}
				}
				Peak[] array = RecognisePeakFromCompound();
				int peaksNum = PeaksNum;
				Array.Resize(ref RltPeaks, peaksNum + array.Length);
				for (int num8 = 0; num8 < array.Length; num8++)
				{
					RltPeaks[peaksNum + num8] = array[num8];
				}
				Array.Sort(RltPeaks);
				for (int num9 = 0; num9 < PeaksNum; num9++)
				{
					if (RltPeaks[num9].pkN >= 0)
					{
						RltPeaks[num9].disNo = num9 + 1;
					}
				}
			}
		}
		else
		{
			Array.Resize(ref RltPeaks, signal.peaks.Length);
			for (int num10 = 0; num10 < PeaksNum; num10++)
			{
				Peak peak3 = signal.peaks[num10];
				peak3.amount = (peak3.amountPer = -1f);
				peak3.disNo = num10 + 1;
				RltPeaks[num10] = peak3;
			}
		}
	}

	public Peak[] GetPeakAllCompound()
	{
		int i = 0;
		int numIndex = -1;
		for (; i < signal.PeaksNum; i++)
		{
			if (caliGnl != null)
			{
				numIndex = caliGnl.SetCompound(signal.peaks[i], signal.peaks, numIndex);
			}
		}
		FormMainParam formMainParam = FormMainParam.Create();
		if (formMainParam.bChanne2)
		{
			for (; i < signal.PeaksNum; i++)
			{
				if (caliGnl != null)
				{
					caliGnl.SetCompoundTimeMini(signal.peaks[i], signal.peaks, numIndex);
				}
			}
		}
		else
		{
			for (; i < signal.PeaksNum; i++)
			{
				if (caliGnl != null)
				{
					numIndex = caliGnl.SetCompound(signal.peaks[i], signal.peaks, numIndex);
				}
			}
		}
		Peak[] array = new Peak[0];
		for (int j = 0; j < signal.PeaksNum; j++)
		{
			if ((chromInfo.cclCalcu != CalcuStyle.ISTD || !chromInfo.rtrHideISTDPeak || !signal.peaks[j].IsIstd) && signal.peaks[j].compound != null && !(signal.peaks[j].name == ""))
			{
				int num = array.Length;
				Array.Resize(ref array, num + 1);
				array[num] = signal.peaks[j];
			}
		}
		Peak[] array2 = RecognisePeakFromCompound(array);
		int num2 = array.Length;
		Array.Resize(ref array, num2 + array2.Length);
		for (int k = 0; k < array2.Length; k++)
		{
			array[num2 + k] = array2[k];
		}
		Array.Sort(array);
		for (int l = 0; l < num2; l++)
		{
			if (array[l].pkN >= 0)
			{
				array[l].disNo = l + 1;
			}
		}
		return array;
	}

	public Peak[] GetPeakFromCompound()
	{
		Peak[] array = new Peak[0];
		for (int i = 0; i < signal.PeaksNum; i++)
		{
			if ((chromInfo.cclCalcu != CalcuStyle.ISTD || !chromInfo.rtrHideISTDPeak || !signal.peaks[i].IsIstd) && signal.peaks[i].compound != null)
			{
				int num = array.Length;
				Array.Resize(ref array, num + 1);
				array[num] = signal.peaks[i];
			}
		}
		return array;
	}

	public void CalcuResults(InstruStyle instruStyle)
	{
		if (caliGnl == null)
		{
			return;
		}
		caliGnl.CalculateFunc(appendLink: false);
		for (int i = 0; i < signal.PeaksNum; i++)
		{
			signal.peaks[i].respStyle = -1;
			signal.peaks[i].amount = -1f;
			signal.peaks[i].compound = null;
			signal.peaks[i].disNo = -1;
		}
		Array.Resize(ref RltPeaks, 0);
		if (caliGnl.PKPeakIndex > 0 && caliGnl.cmpds.Length != 0)
		{
			float retainTime = caliGnl.cmpds[caliGnl.PKPeakIndex].cmpdInfo.retainTime;
			float responseA = caliGnl.cmpds[caliGnl.PKPeakIndex].levels[0].responseA;
			float leftWindow = caliGnl.cmpds[caliGnl.PKPeakIndex].cmpdInfo.leftWindow;
			float rightWindow = caliGnl.cmpds[caliGnl.PKPeakIndex].cmpdInfo.rightWindow;
			leftWindow = 3f;
			rightWindow = 3f;
			float num = float.MaxValue;
			int num2 = -1;
			for (int j = 0; j < signal.peaks.Length; j++)
			{
				if (signal.peaks[j].pkRT > retainTime - leftWindow && signal.peaks[j].pkRT < retainTime + rightWindow && Math.Abs(signal.peaks[j].area - responseA) < num)
				{
					num = Math.Abs(signal.peaks[j].area - responseA);
					num2 = j;
				}
			}
			if (num2 > -1)
			{
				float num3 = signal.peaks[num2].pkRT / retainTime;
				if (num3 == 0f)
				{
					num3 = 1f;
				}
				for (int k = 0; k < signal.peaks.Length; k++)
				{
					signal.peaks[k].pkRT = signal.peaks[k].pkRT / num3;
				}
			}
		}
		if (instruStyle == InstruStyle.GC || instruStyle == InstruStyle.LC)
		{
			CalcuResults_LC();
		}
		whlHheatVaue = (whlLheatVaue = (whlArea = (whlHeight = (whlAmount = 0f))));
		for (int l = 0; l < PeaksNum; l++)
		{
			if (RltPeaks[l].area > 0f)
			{
				whlArea += RltPeaks[l].area;
			}
			if (RltPeaks[l].height > 0f)
			{
				whlHeight += RltPeaks[l].height;
			}
			if (RltPeaks[l].amount > 0f)
			{
				whlAmount += RltPeaks[l].amount;
				if (RltPeaks[l].compound != null)
				{
					whlHheatVaue += RltPeaks[l].amount * RltPeaks[l].compound.cmpdInfo.HheatValue;
					whlLheatVaue += RltPeaks[l].amount * RltPeaks[l].compound.cmpdInfo.LheatValue;
				}
			}
		}
		if (chromInfo.amount > 0f)
		{
			whlAmount = chromInfo.amount;
		}
		whlAreaPer = (whlHeightPer = (whlAmountPer = 0f));
		for (int m = 0; m < PeaksNum; m++)
		{
			if (RltPeaks[m].area > 0f)
			{
				RltPeaks[m].areaPer = RltPeaks[m].area / whlArea;
				whlAreaPer += RltPeaks[m].areaPer;
			}
			else
			{
				RltPeaks[m].areaPer = -1f;
			}
			if (RltPeaks[m].height > 0f)
			{
				RltPeaks[m].heightPer = RltPeaks[m].height / whlHeight;
				whlHeightPer += RltPeaks[m].heightPer;
			}
			else
			{
				RltPeaks[m].heightPer = -1f;
			}
			if (RltPeaks[m].amount > 0f)
			{
				RltPeaks[m].amountPer = RltPeaks[m].amount / whlAmount;
				whlAmountPer += RltPeaks[m].amountPer;
			}
			else
			{
				RltPeaks[m].amountPer = -1f;
			}
		}
	}

	public bool CanWrite(string userName)
	{
		for (int i = 0; i < wUserNames.Length; i++)
		{
			if (wUserNames[i].Equals(userName))
			{
				return true;
			}
		}
		MessageBox.Show(Lang.PS("您没有修改本谱图的权利！", "No right to modify!"));
		return false;
	}

	private void AddOrSubtractChrom()
	{
		chromInfo.asDirectory = "";
		chromInfo.asShowName = "[无]";
		if (chromInfo.asChrom != "")
		{
			try
			{
				FileInfo fileInfo = new FileInfo(chromInfo.asChrom);
				if (fileInfo.Exists)
				{
					byte[] byte_ = Class49.smethod_18(fileInfo.FullName);
					if (pointF_1 == null || byte_2 == null || !Class49.IsByteArrayEqual(byte_2, byte_))
					{
						byte_2 = byte_;
						Chromatogram chromatogram = new Chromatogram();
						if (!chromatogram.LoadFromFile(chromInfo.asChrom, DetectorStyle.General))
						{
							chromInfo.asDirectory = "";
							chromInfo.asShowName = "[文件异常]";
							throw new Exception();
						}
						pointF_1 = (PointF[])chromatogram.signal.svDots.Clone();
						float_0 = chromatogram.signal.ySrcMaxValueTime;
					}
					chromInfo.asDirectory = fileInfo.DirectoryName;
					chromInfo.asShowName = fileInfo.Name.Remove(fileInfo.Name.Length - fileInfo.Extension.Length);
				}
				else
				{
					chromInfo.asChrom = "";
				}
			}
			catch
			{
				chromInfo.asChrom = "";
			}
		}
		if (chromInfo.asChrom == "")
		{
			pointF_1 = null;
		}
		pointF_0 = null;
		if (pointF_1 == null)
		{
			return;
		}
		if (chromInfo.asMatching == ASMatchStyle.NoChange)
		{
			pointF_0 = pointF_1;
			return;
		}
		pointF_0 = (PointF[])pointF_1.Clone();
		if (chromInfo.asMatching == ASMatchStyle.OffsetChrom)
		{
			float num = signal.ySrcMaxValueTime - float_0;
			for (int i = 0; i < pointF_0.Length; i++)
			{
				pointF_0[i].X = pointF_0[i].X + num;
			}
		}
		else if (chromInfo.asMatching == ASMatchStyle.ScaleChrom)
		{
			float num2 = signal.ySrcMaxValueTime / float_0;
			for (int j = 0; j < pointF_0.Length; j++)
			{
				pointF_0[j].X = pointF_0[j].X * num2;
			}
		}
	}

	public Peak[] RecognisePeakFromCompound()
	{
		Peak[] array = new Peak[0];
		if (caliGnl != null)
		{
			Compound[] array2 = new Compound[0];
			for (int i = 0; i < caliGnl.cmpds.Length; i++)
			{
				bool flag = false;
				for (int j = 0; j < PeaksNum; j++)
				{
					if (RltPeaks[j].compound == caliGnl.cmpds[i])
					{
						flag = true;
						break;
					}
				}
				if (!flag && (chromInfo.cclCalcu != CalcuStyle.ISTD || !chromInfo.rtrHideISTDPeak || !caliGnl.cmpds[i].cmpdInfo.isIstd))
				{
					int num = array2.Length;
					Array.Resize(ref array2, num + 1);
					array2[num] = caliGnl.cmpds[i];
				}
			}
			Array.Resize(ref array, array2.Length);
			for (int k = 0; k < array.Length; k++)
			{
				if (array[k] == null)
				{
					array[k] = new Peak();
				}
				CmpdInfo cmpdInfo = array2[k].cmpdInfo;
				array[k].pkN = -1;
				array[k].pkRT = cmpdInfo.retainTime;
				array[k].name = cmpdInfo.name;
				array[k].compound = array2[k];
			}
		}
		return array;
	}

	public Peak[] RecognisePeakFromCompound(Peak[] arrayRlt)
	{
		Peak[] array = new Peak[0];
		if (caliGnl != null)
		{
			Compound[] array2 = new Compound[0];
			for (int i = 0; i < caliGnl.cmpds.Length; i++)
			{
				bool flag = false;
				for (int j = 0; j < arrayRlt.Length; j++)
				{
					if (arrayRlt[j].compound == caliGnl.cmpds[i])
					{
						flag = true;
						break;
					}
				}
				if (!flag && (chromInfo.cclCalcu != CalcuStyle.ISTD || !chromInfo.rtrHideISTDPeak || !caliGnl.cmpds[i].cmpdInfo.isIstd))
				{
					int num = array2.Length;
					Array.Resize(ref array2, num + 1);
					array2[num] = caliGnl.cmpds[i];
				}
			}
			Array.Resize(ref array, array2.Length);
			for (int k = 0; k < array.Length; k++)
			{
				if (array[k] == null)
				{
					array[k] = new Peak();
				}
				CmpdInfo cmpdInfo = array2[k].cmpdInfo;
				array[k].pkN = -1;
				array[k].pkRT = cmpdInfo.retainTime;
				array[k].name = cmpdInfo.name;
				array[k].compound = array2[k];
			}
		}
		return array;
	}

	public void data_AIA(AccStyle accStyle)
	{
		signal.data_AIA(accStyle, injAnalysis);
	}

	public bool Equals(Chromatogram chromatogram)
	{
		return injAnalysis.Equals(chromatogram.injAnalysis) && signal.Equals(chromatogram.signal);
	}

	public void Process(InstruStyle instruStyle)
	{
		AddOrSubtractChrom();
		signal.ApplyIntegs(integ, pointF_0, chromInfo.addChrom);
		integ.RecordSnap();
		CalcuResults(instruStyle);
		CalcuPerformanceAndCus();
	}

	public void ProcessNoReCalc(InstruStyle instruStyle)
	{
		AddOrSubtractChrom();
		signal.ApplyIntegs(integ, pointF_0, chromInfo.addChrom);
		integ.RecordSnap();
		LoadSinglePeakFromRltPeaks();
	}

	private void LoadSinglePeakFromRltPeaks()
	{
		if (RltPeaks != null)
		{
			int num = RltPeaks.Length;
			signal.peaks = new Peak[num];
			for (int i = 0; i < num; i++)
			{
				signal.peaks[i] = RltPeaks[i];
			}
		}
	}

	public void ResetOriDots(bool createDiskFile)
	{
		signal.ResetOriDots(createDiskFile);
	}

	public MtdSetup TransOutMethod()
	{
		return mtdSetup.Copy();
	}

	public static Peak[] GetRltPeaks(Chromatogram[] chroms, Chromatogram curChrom, bool combine, out float whlArea, out float whlHeight, out float whlAmount, out float whlAreaPer, out float whlHeightPer, out float whlAmountPer)
	{
		Peak[] array = new Peak[0];
		whlArea = (whlHeight = (whlAmount = (whlAreaPer = (whlHeightPer = (whlAmountPer = 0f)))));
		if (!combine)
		{
			if (curChrom.RltPeaks != null)
			{
				array = curChrom.RltPeaks;
			}
			whlArea = curChrom.whlArea;
			whlAreaPer = curChrom.whlAreaPer;
			whlHeight = curChrom.whlHeight;
			whlHeightPer = curChrom.whlHeightPer;
			whlAmount = curChrom.whlAmount;
			whlAmountPer = curChrom.whlAmountPer;
			foreach (Peak peak in array)
			{
				if (peak.name.Contains("."))
				{
					peak.name = peak.name.Remove(0, peak.name.IndexOf(".") + 1);
				}
				peak._backColor = ((curChrom.chromInfo.cclCalcu != CalcuStyle.ISTD || !peak.IsIstd) ? Color.White : ChromFormCtrl.istdBkColor);
			}
			return array;
		}
		foreach (Chromatogram chromatogram in chroms)
		{
			int num = array.Length;
			Array.Resize(ref array, num + chromatogram.PeaksNum);
			whlArea += chromatogram.whlArea;
			whlHeight += chromatogram.whlHeight;
			whlAmount += chromatogram.whlAmount;
			for (int k = num; k < array.Length; k++)
			{
				Peak peak2 = (array[k] = chromatogram.RltPeaks[k - num]);
				if (!peak2.name.StartsWith(chromatogram.fName))
				{
					peak2.name = chromatogram.fName + "." + peak2.name;
				}
				peak2._backColor = ((chromatogram.chromInfo.cclCalcu != CalcuStyle.ISTD || !peak2.IsIstd) ? Color.White : ChromFormCtrl.istdBkColor);
			}
		}
		foreach (Peak peak3 in array)
		{
			peak3._areaPer = (peak3._heightPer = (peak3._amountPer = -1f));
			if (peak3.area > 0f)
			{
				peak3._areaPer = peak3.area / whlArea;
				whlAreaPer += peak3._areaPer;
			}
			else
			{
				peak3._areaPer = -1f;
			}
			if (peak3.height > 0f)
			{
				peak3._heightPer = peak3.height / whlHeight;
				whlHeightPer += peak3._heightPer;
			}
			else
			{
				peak3._heightPer = -1f;
			}
			if (peak3.amount > 0f)
			{
				peak3._amountPer = peak3.amount / whlAmount;
				whlAmountPer += peak3._amountPer;
			}
			else
			{
				peak3._amountPer = -1f;
			}
		}
		return array;
	}

	public static Peak[] GetRltPeaks(Chromatogram[] chroms, Chromatogram curChrom, bool combine)
	{
		float num;
		float num2;
		float num3;
		float num4;
		float num5;
		float num6;
		return GetRltPeaks(chroms, curChrom, combine, out num, out num2, out num3, out num4, out num5, out num6);
	}

	public Peak[] GetRltPeaks(bool combine)
	{
		Chromatogram[] chroms = new Chromatogram[0];
		return GetRltPeaks(chroms, this, combine, out whlArea, out whlHeight, out whlAmount, out whlAmountPer, out whlHeightPer, out whlAmountPer);
	}

	public static Chromatogram LoadFromFile2(string fileName, DetectorStyle detectorStyle)
	{
		Chromatogram chromatogram = new Chromatogram();
		try
		{
			chromatogram.LoadFromFile(fileName, detectorStyle);
		}
		catch (Exception)
		{
			chromatogram = (Chromatogram)IBaseFileMgr.OpenFile(fileName);
		}
		if (chromatogram.signal.oriDots.Length == 0)
		{
			chromatogram = (Chromatogram)IBaseFileMgr.OpenFile(fileName);
		}
		if (chromatogram != null)
		{
			chromatogram.fullName = fileName;
			chromatogram.fName = Path.GetFileName(fileName);
			if (chromatogram.signal.applyIntegs_0 == null)
			{
				chromatogram.signal.applyIntegs_0 = new ApplyIntegs();
			}
			if (chromatogram.signal.soundPlayer_0 == null)
			{
				chromatogram.signal.soundPlayer_0 = new SoundPlayer();
			}
			if (chromatogram.signal.soundPlayer_1 == null)
			{
				chromatogram.signal.soundPlayer_1 = new SoundPlayer();
			}
			if (chromatogram.integ.IntegRows.Length >= 3)
			{
				if (chromatogram.integ.IntegRows[0].oprtStyle != IntegOprtStyle.PeakWidth)
				{
					chromatogram.integ.IntegRows[0].oprtStyle = IntegOprtStyle.PeakWidth;
				}
				if (chromatogram.integ.IntegRows[1].oprtStyle != IntegOprtStyle.Threshold)
				{
					chromatogram.integ.IntegRows[1].oprtStyle = IntegOprtStyle.Threshold;
				}
				if (chromatogram.integ.IntegRows[2].oprtStyle != IntegOprtStyle.VtVSlope)
				{
					chromatogram.integ.IntegRows[2].oprtStyle = IntegOprtStyle.VtVSlope;
				}
			}
			int iLastIndex = 0;
			float fLastValue = 0f;
			findTheLastXValue(ref chromatogram.signal.dots, out iLastIndex, out fLastValue);
			if (iLastIndex < chromatogram.signal.dots.Length)
			{
				iLastIndex++;
				for (int i = iLastIndex; i < chromatogram.signal.dots.Length; i++)
				{
					if (chromatogram.signal.dots[i].Y >= 0f)
					{
						chromatogram.signal.dots[i].X = 0f;
						chromatogram.signal.dots[i].Y = 0f;
					}
				}
				for (int j = iLastIndex; j < chromatogram.signal.oriDots.Length; j++)
				{
					if (chromatogram.signal.oriDots[j].Y >= 0f)
					{
						chromatogram.signal.oriDots[j].X = 0f;
						chromatogram.signal.oriDots[j].Y = 0f;
					}
				}
				for (int k = iLastIndex; k < chromatogram.signal.svDots.Length; k++)
				{
					if (chromatogram.signal.svDots[k].Y >= 0f)
					{
						chromatogram.signal.svDots[k].X = 0f;
						chromatogram.signal.svDots[k].Y = 0f;
					}
				}
			}
			try
			{
				if (chromatogram.caliGnl != null && chromatogram.caliGnl.cmpds != null)
				{
					chromatogram.caliGnl.cmpds = (from x in chromatogram.caliGnl.cmpds.ToList()
						orderby x.cmpdInfo.retainTime
						select x).ToArray();
				}
			}
			catch (Exception)
			{
			}
			if (chromatogram.RltPeaks == null)
			{
				chromatogram.Process(InstruStyle.GC);
			}
			else
			{
				chromatogram.ProcessNoReCalc(InstruStyle.GC);
			}
		}
		return chromatogram;
	}

	public void SaveToFile(string fileName)
	{
		fullName = fileName;
		IBaseFileMgr.SaveFile(fileName, this);
	}

	private static void findTheLastXValue(ref PointF[] dots, out int iLastIndex, out float fLastValue)
	{
		LogMgr.Instance.Write2RunLog("findTheLastXValue(ref PointF[] dots, out int iLastIndex, out float fLastValue)" + dots.Length);
		if (dots.Length != 0)
		{
			int num = (iLastIndex = dots.Length - 1);
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
		else
		{
			iLastIndex = 0;
			fLastValue = 0f;
		}
	}

	public bool LoadFromFile(string fileName, DetectorStyle detectorStyle)
	{
		if (!File.Exists(fileName))
		{
			return false;
		}
		fileName = fileName.ToLower();
		FileInfo fileInfo_ = null;
		FileStream fileStream_ = null;
		BinaryReader binaryReader_ = null;
		bool result;
		try
		{
			Class49.OpenBinaryReader(fileName, out fileInfo_, out fileStream_, out binaryReader_);
			fullName = fileInfo_.FullName;
			directory = fileInfo_.DirectoryName;
			fName = fileInfo_.Name.Replace(fileInfo_.Extension, "");
			string text = fileInfo_.Extension.ToLower();
			switch (text)
			{
			case ".sda":
			case ".tab":
				if ((DetectorStyle)binaryReader_.ReadByte() != detectorStyle)
				{
					MessageBox.Show(Lang.PS("文件类型不匹配！"));
					return false;
				}
				method_2(binaryReader_);
				idxUserArchive = userArchives.Length - 1;
				if (idxUserArchive >= 0)
				{
					chromInfo.LoadFromObject(userArchives[idxUserArchive].chromInfo);
					integ.LoadFromObject(userArchives[idxUserArchive].integ);
					userArchives[idxUserArchive].SL_lbTexts(load: false, ref signal.lbTexts);
					userArchives[idxUserArchive].SL_lbLines(load: false, ref signal.lbLines);
				}
				break;
			case ".dat":
				signal.LoadFromFile_N2k(binaryReader_, fileStream_);
				injAnalysis.dtAcquire = DateTime.Now;
				injAnalysis.tsAcquire = TimeSpan.FromMinutes(signal.xMaxTime);
				injAnalysis.analyst = "[N2000.dat]";
				break;
			case ".org":
				signal.LoadFromFile_Org(binaryReader_, fileStream_);
				injAnalysis.dtAcquire = DateTime.Now;
				injAnalysis.tsAcquire = TimeSpan.FromMinutes(signal.xMaxTime);
				injAnalysis.analyst = "[N2000.dat]";
				break;
			default:
				if (text == ".cdf")
				{
					signal.Read(fileStream_, binaryReader_);
					data_AIA(AccStyle.Read);
					injAnalysis.dtAcquire = DateTime.Now;
					injAnalysis.tsAcquire = TimeSpan.FromMinutes(signal.xMaxTime);
					injAnalysis.analyst = "[戴安.cdf]";
				}
				else
				{
					if (!(text == ".hw"))
					{
						break;
					}
					try
					{
						signal.LoadFromFile_hw(binaryReader_, fileStream_);
						injAnalysis.dtAcquire = DateTime.Now;
						injAnalysis.tsAcquire = TimeSpan.FromMinutes(signal.xMaxTime);
						injAnalysis.analyst = "[千谱.hw]";
					}
					catch
					{
						fileStream_.Seek(0L, SeekOrigin.Begin);
						if ((DetectorStyle)binaryReader_.ReadByte() != detectorStyle)
						{
							MessageBox.Show(Lang.PS("文件类型不匹配！"));
							return false;
						}
						method_2(binaryReader_);
						idxUserArchive = userArchives.Length - 1;
						if (idxUserArchive >= 0)
						{
							chromInfo.LoadFromObject(userArchives[idxUserArchive].chromInfo);
							integ.LoadFromObject(userArchives[idxUserArchive].integ);
							userArchives[idxUserArchive].SL_lbTexts(load: false, ref signal.lbTexts);
							userArchives[idxUserArchive].SL_lbLines(load: false, ref signal.lbLines);
						}
					}
				}
				break;
			case null:
				break;
			}
			signal.linkLcGradient = chromInfoR.LcGradient;
			signal.linkGcProgTemp = chromInfoR.GcProgTemp;
			signal.sample_name = fName;
			openTime = DateTime.Now;
			result = true;
			if (text == ".sda" || text == ".tab")
			{
				Array.Resize(ref AdjustRltPeaksPara, binaryReader_.ReadInt32());
				for (int i = 0; i < AdjustRltPeaksPara.Length; i++)
				{
					if (AdjustRltPeaksPara[i] == null)
					{
						AdjustRltPeaksPara[i] = new Peak();
					}
					AdjustRltPeaksPara[i].JuseTimeCheck = binaryReader_.ReadBoolean();
					AdjustRltPeaksPara[i].JStdandPeakTime = binaryReader_.ReadDouble();
					AdjustRltPeaksPara[i].JTimePara = binaryReader_.ReadDouble();
					AdjustRltPeaksPara[i].name = binaryReader_.ReadString();
					AdjustRltPeaksPara[i].JPeakAdjustPara = binaryReader_.ReadDouble();
					AdjustRltPeaksPara[i].JModBusAddr = binaryReader_.ReadInt32();
				}
				EnumChannelDetectionMethod enumChannelDetectionMethod = (EnumChannelDetectionMethod)binaryReader_.ReadInt32();
				EnumChannelBasisQuantity enumChannelBasisQuantity = (EnumChannelBasisQuantity)binaryReader_.ReadInt32();
				PrintPara printPara = new PrintPara();
				printPara.LoadFromBr(binaryReader_);
				PPara = printPara;
				devManager.ReadFromFile(binaryReader_, SDAVER);
				devManager.printPara_0 = printPara;
				disLg.LoadFromFile(binaryReader_);
				if (SDAVER == "VER2.0")
				{
					if (caliGnl == null)
					{
						caliGnl = new CaliGnl();
					}
					caliGnl.LoadFromFileFrombr(binaryReader_);
				}
				try
				{
					readOthers(binaryReader_);
				}
				catch
				{
				}
			}
			if (caliGnl != null && caliGnl.cmpds != null)
			{
				caliGnl.cmpds = (from x in caliGnl.cmpds.ToList()
					orderby x.cmpdInfo.retainTime
					select x).ToArray();
			}
		}
		catch (Exception)
		{
			result = false;
		}
		finally
		{
			Class49.FileStreamClose(ref fileStream_, ref binaryReader_);
		}
		return result;
	}

	private void method_2(BinaryReader binaryReader_0)
	{
		string text = binaryReader_0.ReadString();
		if (text == "VER2.0")
		{
			SDAVER = text;
		}
		else
		{
			binaryReader_0.BaseStream.Position = 1L;
		}
		injAnalysis.LoadFromFile(binaryReader_0);
		chromInfoR.LoadFromFile(binaryReader_0);
		signal.LoadFromFile(binaryReader_0);
		Array.Resize(ref userArchives, binaryReader_0.ReadInt32());
		for (int i = 0; i < userArchives.Length; i++)
		{
			userArchives[i] = new UserArchive();
			userArchives[i].LoadFromFile(binaryReader_0);
		}
		canSetRs = binaryReader_0.ReadBoolean();
		Array.Resize(ref nrUserNames, binaryReader_0.ReadInt32());
		for (int j = 0; j < nrUserNames.Length; j++)
		{
			nrUserNames[j] = binaryReader_0.ReadString();
		}
		Array.Resize(ref wUserNames, binaryReader_0.ReadInt32());
		for (int k = 0; k < wUserNames.Length; k++)
		{
			wUserNames[k] = binaryReader_0.ReadString();
		}
		cus1_name = binaryReader_0.ReadString();
		cus1_formula = binaryReader_0.ReadString();
		cus2_name = binaryReader_0.ReadString();
		cus2_formula = binaryReader_0.ReadString();
	}

	private void method_3(BinaryWriter binaryWriter_0)
	{
		if (caliGnl != null)
		{
			caliGnl.SaveToFileBybw(binaryWriter_0);
		}
	}

	private void saveSdaData(BinaryWriter binaryWriter_0)
	{
		binaryWriter_0.Write(Class49.string_8);
		injAnalysis.SaveToFile(binaryWriter_0);
		chromInfoR.SaveToFile(binaryWriter_0);
		signal.SaveToFile(binaryWriter_0);
		binaryWriter_0.Write(userArchives.Length);
		for (int i = 0; i < userArchives.Length; i++)
		{
			userArchives[i].SaveToFile(binaryWriter_0);
		}
		binaryWriter_0.Write(canSetRs);
		binaryWriter_0.Write(nrUserNames.Length);
		for (int j = 0; j < nrUserNames.Length; j++)
		{
			binaryWriter_0.Write(nrUserNames[j]);
		}
		binaryWriter_0.Write(wUserNames.Length);
		for (int k = 0; k < wUserNames.Length; k++)
		{
			binaryWriter_0.Write(wUserNames[k]);
		}
		binaryWriter_0.Write(cus1_name);
		binaryWriter_0.Write(cus1_formula);
		binaryWriter_0.Write(cus2_name);
		binaryWriter_0.Write(cus2_formula);
		binaryWriter_0.Write(AdjustRltPeaksPara.Length);
		for (int l = 0; l < AdjustRltPeaksPara.Length; l++)
		{
			binaryWriter_0.Write(AdjustRltPeaksPara[l].JuseTimeCheck);
			binaryWriter_0.Write(AdjustRltPeaksPara[l].JStdandPeakTime);
			binaryWriter_0.Write(AdjustRltPeaksPara[l].JTimePara);
			binaryWriter_0.Write(AdjustRltPeaksPara[l].name);
			binaryWriter_0.Write(AdjustRltPeaksPara[l].JPeakAdjustPara);
			binaryWriter_0.Write(AdjustRltPeaksPara[l].JModBusAddr);
		}
		EnumChannelDetectionMethod value = EnumChannelDetectionMethod.CalibrationNormal;
		EnumChannelBasisQuantity value2 = EnumChannelBasisQuantity.AreaSquare;
		binaryWriter_0.Write((int)value);
		binaryWriter_0.Write((int)value2);
		if (ChromPPara == null)
		{
			ChromPPara = new PrintPara();
		}
		ChromPPara.WriteToFile(binaryWriter_0);
		InsDeviceManager insDeviceManager = new InsDeviceManager();
		insDeviceManager.printPara_0 = ChromPPara;
		insDeviceManager.SaveToFile(binaryWriter_0, Class49.string_8);
		disLg.SaveToFile(binaryWriter_0);
	}

	public void writeOthers(BinaryWriter binaryWriter_0)
	{
	}

	public void readOthers(BinaryReader binaryReader_0)
	{
		string text = binaryReader_0.ReadString();
		if (text == "--TVOC--")
		{
			float num = binaryReader_0.ReadSingle();
			float num2 = binaryReader_0.ReadSingle();
			float num3 = binaryReader_0.ReadSingle();
		}
	}

	public void SaveToFileOld(string fileName)
	{
		FileInfo fileInfo_ = null;
		FileStream fileStream_ = null;
		BinaryWriter binaryWriter_ = null;
		try
		{
			Class49.OpenBinaryWriter(fileName, out fileInfo_, out fileStream_, out binaryWriter_);
			fullName = fileInfo_.FullName;
			directory = fileInfo_.DirectoryName;
			fName = fileInfo_.Name.Replace(fileInfo_.Extension, "");
			binaryWriter_.Write((byte)signal.detectorStyle);
			saveSdaData(binaryWriter_);
			idxUserArchive = userArchives.Length - 1;
			method_3(binaryWriter_);
			try
			{
				writeOthers(binaryWriter_);
			}
			catch
			{
			}
		}
		finally
		{
			Class49.FileStreamClose(ref fileStream_, ref binaryWriter_);
		}
	}

	public void TransToAIA(string fileName)
	{
		data_AIA(AccStyle.Write);
		FileStream fileStream = new FileInfo(fileName + ".cdf").Open(FileMode.Create);
		BinaryWriter binaryWriter = new BinaryWriter(fileStream);
		signal.Write(fileStream, binaryWriter);
		binaryWriter.Close();
		fileStream.Close();
	}
}
