using System;
using System.IO;
using System.Runtime.InteropServices;

namespace IBrainChrom2018;

[Serializable]
[StructLayout(LayoutKind.Sequential)]
public class ChromInfo
{
	public bool addChrom;

	public float amount;

	public string asChrom = "";

	public string asDirectory = "";

	public ASMatchStyle asMatching = ASMatchStyle.NoChange;

	public string asShowName = "";

	public float ccColumnLength = 100f;

	public float ccColumnUT = 1f;

	public string cclAuthor = "作者";

	public CalcuStyle cclCalcu = CalcuStyle.ESTD;

	public string cclCalibration = "校正文件[峰表]";

	public DateTime cclCreateTime = DateTime.Now;

	public string cclDescription = "描述";

	public string cclDirectory = "";

	public DateTime cclModifiedTime;

	public string cclShowName = "";

	public ColumnCalcuStyle ccStyle = ColumnCalcuStyle.From50per;

	public float dilution = 1f;

	public float injVolumn;

	private IstdAmount[] istdAmount_0 = new IstdAmount[0];

	public bool lsoForAllDetectedPeaks;

	public int lsoFrom = 180;

	public int lsoTo = 910;

	public LSO_MatchCriteria lsoMatchCriteria = LSO_MatchCriteria.Correlation;

	public int lsoMatchFactorThreshold = 900;

	public int lsoMaxNumHits = 3;

	public bool lsoRestrictRT;

	public float lsoRestrictRTV = 0.01f;

	public bool lsoRestrictWaveLength;

	public bool lsoUseBackCorr;

	public PDARow[] pdaRows = new PDARow[0];

	public string msmColumn = "";

	public string msmDetection = "";

	public string msmFlowRate = "";

	public string msmMobilePhase = "";

	public string msmMtdDspt = "";

	public string msmNote = "";

	public string msmPressure = "";

	public string msmTemperature = "";

	public GPC_RangeRow[] gpc_RangeRow_0 = new GPC_RangeRow[0];

	public GPC_RangeRow[] percents = new GPC_RangeRow[0];

	public float ppoAbsorbanceThreshold = 0.05f;

	public int ppoFrom = 190;

	public int ppoTo = 900;

	public int ppoPurityThreshold;

	public bool ppoRestrictWaveLength;

	public bool ppoUseBackCorr;

	public PPO_UsedPoints ppoUsedPoints = PPO_UsedPoints.All;

	public float prsScaleFactor = 1f;

	public float prsUncalAmtRespF = -1f;

	public RespStyle prsUncalBase = RespStyle.Height;

	public string prsUnitAfterScale = "";

	public bool prsUseScaleFactor = true;

	public bool rtrHideISTDPeak;

	public RltReportPeaks rtrRltReportPeaks = RltReportPeaks.AllDetectedPeaks;

	public ChromInfo()
	{
		SetIstdAmount(-1f, 0f);
	}

	public float GetIstdAmount(float cmpdRT)
	{
		for (int i = 0; i < istdAmount_0.Length; i++)
		{
			if (istdAmount_0[i].cmpdRT == cmpdRT)
			{
				return istdAmount_0[i].amount;
			}
		}
		return -1f;
	}

	public float GetIstdAmount(int istdIndex)
	{
		if (istdIndex < istdAmount_0.Length)
		{
			return istdAmount_0[istdIndex].amount;
		}
		return -1f;
	}

	public void Init(Instrument instrument)
	{
		Array.Resize(ref pdaRows, 2);
		SetPdaRow(0, used: false, "1tr");
		SetPdaRow(1, used: true, "PAH");
	}

	public void RefreshAsInfo()
	{
		if (File.Exists(asChrom))
		{
			Class49.smethod_26(asChrom, out asDirectory, out asShowName, out var _);
		}
		else
		{
			asDirectory = (asShowName = "");
		}
	}

	public void SetIstdAmount(float cmpdRT, float amount)
	{
		for (int i = 0; i < istdAmount_0.Length; i++)
		{
			if (istdAmount_0[i].cmpdRT < 0f)
			{
				istdAmount_0[i].cmpdRT = cmpdRT;
				istdAmount_0[i].amount = amount;
				return;
			}
			if (istdAmount_0[i].cmpdRT == cmpdRT)
			{
				istdAmount_0[i].amount = amount;
				return;
			}
		}
		if (istdAmount_0.Length == 0 && cmpdRT < 0f)
		{
			int num = istdAmount_0.Length;
			Array.Resize(ref istdAmount_0, num + 1);
			istdAmount_0[num].cmpdRT = cmpdRT;
			istdAmount_0[num].amount = amount;
		}
	}

	public void SetIstdAmountIndex(int cmpdIndex, float amount)
	{
		if (istdAmount_0.Length != 0)
		{
			istdAmount_0[cmpdIndex].amount = amount;
		}
	}

	public void SetPdaRow(int index, bool used, string libName)
	{
		if (index >= 0 && index < pdaRows.Length)
		{
			pdaRows[index].used = used;
			pdaRows[index].name = libName;
		}
	}

	public void LoadFromFile()
	{
		if (cclCalibration == "\\//.cal")
		{
			cclCalibration = "\\.cal";
		}
		if (File.Exists(cclCalibration))
		{
			Class49.smethod_26(cclCalibration, out cclDirectory, out cclShowName, out var _);
			CaliGnl caliGnl = new CaliGnl();
			caliGnl.LoadFromFileOldV11(cclCalibration, out cclAuthor);
			cclDescription = caliGnl.caliOption.description;
			cclCreateTime = File.GetCreationTime(cclCalibration);
			cclModifiedTime = File.GetLastWriteTime(cclCalibration);
		}
		else
		{
			cclAuthor = (cclDescription = "");
			cclCreateTime = (cclModifiedTime = DateTime.FromBinary(0L));
		}
	}

	public void LoadFromFile(CaliGnl caliGnl)
	{
		if (cclCalibration == "\\//.cal")
		{
			cclCalibration = "\\.cal";
		}
		if (File.Exists(cclCalibration))
		{
			Class49.smethod_26(cclCalibration, out cclDirectory, out cclShowName, out var _);
			caliGnl.LoadFromFileOldV11(cclCalibration, out cclAuthor);
			cclDescription = caliGnl.caliOption.description;
			cclCreateTime = File.GetCreationTime(cclCalibration);
			cclModifiedTime = File.GetLastWriteTime(cclCalibration);
		}
		else
		{
			cclAuthor = (cclDescription = "");
			cclCreateTime = (cclModifiedTime = DateTime.FromBinary(0L));
		}
	}

	public void LoadFromInjAnalysis(Injection injection)
	{
		amount = injection.amount;
		istdAmount_0[0].amount = injection.ISTD_amount;
		injVolumn = injection.inj_volume;
		dilution = injection.dilution;
	}

	public ChromInfo Copy()
	{
		ChromInfo chromInfo = new ChromInfo();
		chromInfo.msmMtdDspt = msmMtdDspt;
		chromInfo.msmColumn = msmColumn;
		chromInfo.msmMobilePhase = msmMobilePhase;
		chromInfo.msmFlowRate = msmFlowRate;
		chromInfo.msmPressure = msmPressure;
		chromInfo.msmDetection = msmDetection;
		chromInfo.msmTemperature = msmTemperature;
		chromInfo.msmNote = msmNote;
		chromInfo.cclCalibration = cclCalibration;
		chromInfo.cclDirectory = cclDirectory;
		chromInfo.cclShowName = cclShowName;
		chromInfo.cclCalcu = cclCalcu;
		chromInfo.cclAuthor = cclAuthor;
		chromInfo.cclDescription = cclDescription;
		chromInfo.cclCreateTime = cclCreateTime;
		chromInfo.cclModifiedTime = cclModifiedTime;
		chromInfo.rtrHideISTDPeak = rtrHideISTDPeak;
		chromInfo.rtrRltReportPeaks = rtrRltReportPeaks;
		chromInfo.prsUseScaleFactor = prsUseScaleFactor;
		chromInfo.prsScaleFactor = prsScaleFactor;
		chromInfo.prsUnitAfterScale = prsUnitAfterScale;
		chromInfo.prsUncalBase = prsUncalBase;
		chromInfo.prsUncalAmtRespF = prsUncalAmtRespF;
		chromInfo.amount = amount;
		Array.Resize(ref chromInfo.istdAmount_0, istdAmount_0.Length);
		for (int i = 0; i < istdAmount_0.Length; i++)
		{
			chromInfo.istdAmount_0[i] = istdAmount_0[i];
		}
		chromInfo.injVolumn = injVolumn;
		chromInfo.dilution = dilution;
		chromInfo.asChrom = asChrom;
		chromInfo.asDirectory = asDirectory;
		chromInfo.asShowName = asShowName;
		chromInfo.addChrom = addChrom;
		chromInfo.asMatching = asMatching;
		chromInfo.ccColumnUT = ccColumnUT;
		chromInfo.ccColumnLength = ccColumnLength;
		chromInfo.ccStyle = ccStyle;
		chromInfo.ppoRestrictWaveLength = ppoRestrictWaveLength;
		chromInfo.ppoFrom = ppoFrom;
		chromInfo.ppoTo = ppoTo;
		chromInfo.ppoPurityThreshold = ppoPurityThreshold;
		chromInfo.ppoAbsorbanceThreshold = ppoAbsorbanceThreshold;
		chromInfo.ppoUsedPoints = ppoUsedPoints;
		chromInfo.ppoUseBackCorr = ppoUseBackCorr;
		chromInfo.lsoMatchCriteria = lsoMatchCriteria;
		chromInfo.lsoMatchFactorThreshold = lsoMatchFactorThreshold;
		chromInfo.lsoMaxNumHits = lsoMaxNumHits;
		chromInfo.lsoRestrictWaveLength = lsoRestrictWaveLength;
		chromInfo.lsoFrom = lsoFrom;
		chromInfo.lsoTo = lsoTo;
		chromInfo.lsoRestrictRT = lsoRestrictRT;
		chromInfo.lsoRestrictRTV = lsoRestrictRTV;
		chromInfo.lsoUseBackCorr = lsoUseBackCorr;
		chromInfo.lsoForAllDetectedPeaks = lsoForAllDetectedPeaks;
		Array.Resize(ref chromInfo.pdaRows, pdaRows.Length);
		for (int j = 0; j < chromInfo.pdaRows.Length; j++)
		{
			chromInfo.SetPdaRow(j, pdaRows[j].used, pdaRows[j].name);
		}
		Array.Resize(ref chromInfo.percents, percents.Length);
		for (int k = 0; k < chromInfo.percents.Length; k++)
		{
			chromInfo.percents[k] = percents[k].Copy();
		}
		Array.Resize(ref chromInfo.gpc_RangeRow_0, gpc_RangeRow_0.Length);
		for (int l = 0; l < chromInfo.gpc_RangeRow_0.Length; l++)
		{
			chromInfo.gpc_RangeRow_0[l] = gpc_RangeRow_0[l].Copy();
		}
		return chromInfo;
	}

	public void LoadFromObject(ChromInfo chromInfo)
	{
		msmMtdDspt = chromInfo.msmMtdDspt;
		msmColumn = chromInfo.msmColumn;
		msmMobilePhase = chromInfo.msmMobilePhase;
		msmFlowRate = chromInfo.msmFlowRate;
		msmPressure = chromInfo.msmPressure;
		msmDetection = chromInfo.msmDetection;
		msmTemperature = chromInfo.msmTemperature;
		msmNote = chromInfo.msmNote;
		cclCalibration = chromInfo.cclCalibration;
		cclDirectory = chromInfo.cclDirectory;
		cclShowName = chromInfo.cclShowName;
		cclCalcu = chromInfo.cclCalcu;
		cclAuthor = chromInfo.cclAuthor;
		cclDescription = chromInfo.cclDescription;
		cclCreateTime = chromInfo.cclCreateTime;
		cclModifiedTime = chromInfo.cclModifiedTime;
		rtrHideISTDPeak = chromInfo.rtrHideISTDPeak;
		rtrRltReportPeaks = chromInfo.rtrRltReportPeaks;
		prsUseScaleFactor = chromInfo.prsUseScaleFactor;
		prsScaleFactor = chromInfo.prsScaleFactor;
		prsUnitAfterScale = chromInfo.prsUnitAfterScale;
		prsUncalBase = chromInfo.prsUncalBase;
		prsUncalAmtRespF = chromInfo.prsUncalAmtRespF;
		amount = chromInfo.amount;
		Array.Resize(ref istdAmount_0, chromInfo.istdAmount_0.Length);
		for (int i = 0; i < istdAmount_0.Length; i++)
		{
			istdAmount_0[i] = chromInfo.istdAmount_0[i];
		}
		injVolumn = chromInfo.injVolumn;
		dilution = chromInfo.dilution;
		asChrom = chromInfo.asChrom;
		asDirectory = chromInfo.asDirectory;
		asShowName = chromInfo.asShowName;
		addChrom = chromInfo.addChrom;
		asMatching = chromInfo.asMatching;
		ccColumnUT = chromInfo.ccColumnUT;
		ccColumnLength = chromInfo.ccColumnLength;
		ccStyle = chromInfo.ccStyle;
		ppoRestrictWaveLength = chromInfo.ppoRestrictWaveLength;
		ppoFrom = chromInfo.ppoFrom;
		ppoTo = chromInfo.ppoTo;
		ppoPurityThreshold = chromInfo.ppoPurityThreshold;
		ppoAbsorbanceThreshold = chromInfo.ppoAbsorbanceThreshold;
		ppoUsedPoints = chromInfo.ppoUsedPoints;
		ppoUseBackCorr = chromInfo.ppoUseBackCorr;
		lsoMatchCriteria = chromInfo.lsoMatchCriteria;
		lsoMatchFactorThreshold = chromInfo.lsoMatchFactorThreshold;
		lsoMaxNumHits = chromInfo.lsoMaxNumHits;
		lsoRestrictWaveLength = chromInfo.lsoRestrictWaveLength;
		lsoFrom = chromInfo.lsoFrom;
		lsoTo = chromInfo.lsoTo;
		lsoRestrictRT = chromInfo.lsoRestrictRT;
		lsoRestrictRTV = chromInfo.lsoRestrictRTV;
		lsoUseBackCorr = chromInfo.lsoUseBackCorr;
		lsoForAllDetectedPeaks = chromInfo.lsoForAllDetectedPeaks;
		Array.Resize(ref pdaRows, chromInfo.pdaRows.Length);
		for (int j = 0; j < pdaRows.Length; j++)
		{
			SetPdaRow(j, chromInfo.pdaRows[j].used, chromInfo.pdaRows[j].name);
		}
		Array.Resize(ref percents, chromInfo.percents.Length);
		for (int k = 0; k < percents.Length; k++)
		{
			percents[k] = chromInfo.percents[k];
		}
		Array.Resize(ref gpc_RangeRow_0, chromInfo.gpc_RangeRow_0.Length);
		for (int l = 0; l < gpc_RangeRow_0.Length; l++)
		{
			gpc_RangeRow_0[l] = chromInfo.gpc_RangeRow_0[l];
		}
	}

	public void SaveToFile(BinaryWriter binaryWriter_0)
	{
		binaryWriter_0.Write(msmMtdDspt);
		binaryWriter_0.Write(msmColumn);
		binaryWriter_0.Write(msmMobilePhase);
		binaryWriter_0.Write(msmFlowRate);
		binaryWriter_0.Write(msmPressure);
		binaryWriter_0.Write(msmDetection);
		binaryWriter_0.Write(msmTemperature);
		binaryWriter_0.Write(msmNote);
		binaryWriter_0.Write(cclCalibration);
		binaryWriter_0.Write((byte)cclCalcu);
		binaryWriter_0.Write(rtrHideISTDPeak);
		binaryWriter_0.Write((byte)rtrRltReportPeaks);
		binaryWriter_0.Write(prsUseScaleFactor);
		binaryWriter_0.Write(prsScaleFactor);
		binaryWriter_0.Write(prsUnitAfterScale);
		binaryWriter_0.Write((byte)prsUncalBase);
		binaryWriter_0.Write(prsUncalAmtRespF);
		binaryWriter_0.Write(amount);
		binaryWriter_0.Write(istdAmount_0.Length);
		for (int i = 0; i < istdAmount_0.Length; i++)
		{
			binaryWriter_0.Write(istdAmount_0[i].cmpdRT);
			binaryWriter_0.Write(istdAmount_0[i].amount);
		}
		binaryWriter_0.Write(injVolumn);
		if (dilution == 0f)
		{
			dilution = 1f;
		}
		binaryWriter_0.Write(dilution);
		binaryWriter_0.Write(asChrom);
		binaryWriter_0.Write(addChrom);
		binaryWriter_0.Write((byte)asMatching);
		binaryWriter_0.Write(ccColumnUT);
		binaryWriter_0.Write(ccColumnLength);
		binaryWriter_0.Write((byte)ccStyle);
		binaryWriter_0.Write(ppoRestrictWaveLength);
		binaryWriter_0.Write(ppoFrom);
		binaryWriter_0.Write(ppoTo);
		binaryWriter_0.Write(ppoPurityThreshold);
		binaryWriter_0.Write(ppoAbsorbanceThreshold);
		binaryWriter_0.Write((byte)ppoUsedPoints);
		binaryWriter_0.Write(ppoUseBackCorr);
		binaryWriter_0.Write((byte)lsoMatchCriteria);
		binaryWriter_0.Write(lsoMatchFactorThreshold);
		binaryWriter_0.Write(lsoMaxNumHits);
		binaryWriter_0.Write(lsoRestrictWaveLength);
		binaryWriter_0.Write(lsoFrom);
		binaryWriter_0.Write(lsoTo);
		binaryWriter_0.Write(lsoRestrictRT);
		binaryWriter_0.Write(lsoRestrictRTV);
		binaryWriter_0.Write(lsoUseBackCorr);
		binaryWriter_0.Write(lsoForAllDetectedPeaks);
		binaryWriter_0.Write(pdaRows.Length);
		for (int j = 0; j < pdaRows.Length; j++)
		{
			pdaRows[j].SaveToFile(binaryWriter_0);
		}
		binaryWriter_0.Write(percents.Length);
		for (int k = 0; k < percents.Length; k++)
		{
			percents[k].SaveToFile(binaryWriter_0);
		}
		binaryWriter_0.Write(gpc_RangeRow_0.Length);
		for (int l = 0; l < gpc_RangeRow_0.Length; l++)
		{
			gpc_RangeRow_0[l].SaveToFile(binaryWriter_0);
		}
	}

	public void LoadFromFile(BinaryReader binaryReader_0)
	{
		msmMtdDspt = binaryReader_0.ReadString();
		msmColumn = binaryReader_0.ReadString();
		msmMobilePhase = binaryReader_0.ReadString();
		msmFlowRate = binaryReader_0.ReadString();
		msmPressure = binaryReader_0.ReadString();
		msmDetection = binaryReader_0.ReadString();
		msmTemperature = binaryReader_0.ReadString();
		msmNote = binaryReader_0.ReadString();
		cclCalibration = binaryReader_0.ReadString();
		cclCalcu = (CalcuStyle)binaryReader_0.ReadByte();
		rtrHideISTDPeak = binaryReader_0.ReadBoolean();
		rtrRltReportPeaks = (RltReportPeaks)binaryReader_0.ReadByte();
		prsUseScaleFactor = binaryReader_0.ReadBoolean();
		prsScaleFactor = binaryReader_0.ReadSingle();
		prsUnitAfterScale = binaryReader_0.ReadString();
		prsUncalBase = (RespStyle)binaryReader_0.ReadByte();
		prsUncalAmtRespF = binaryReader_0.ReadSingle();
		amount = binaryReader_0.ReadSingle();
		Array.Resize(ref istdAmount_0, binaryReader_0.ReadInt32());
		for (int i = 0; i < istdAmount_0.Length; i++)
		{
			istdAmount_0[i].cmpdRT = binaryReader_0.ReadSingle();
			istdAmount_0[i].amount = binaryReader_0.ReadSingle();
		}
		injVolumn = binaryReader_0.ReadSingle();
		dilution = binaryReader_0.ReadSingle();
		asChrom = binaryReader_0.ReadString();
		addChrom = binaryReader_0.ReadBoolean();
		asMatching = (ASMatchStyle)binaryReader_0.ReadByte();
		ccColumnUT = binaryReader_0.ReadSingle();
		ccColumnLength = binaryReader_0.ReadSingle();
		ccStyle = (ColumnCalcuStyle)binaryReader_0.ReadByte();
		ppoRestrictWaveLength = binaryReader_0.ReadBoolean();
		ppoFrom = binaryReader_0.ReadInt32();
		ppoTo = binaryReader_0.ReadInt32();
		ppoPurityThreshold = binaryReader_0.ReadInt32();
		ppoAbsorbanceThreshold = binaryReader_0.ReadSingle();
		ppoUsedPoints = (PPO_UsedPoints)binaryReader_0.ReadByte();
		ppoUseBackCorr = binaryReader_0.ReadBoolean();
		lsoMatchCriteria = (LSO_MatchCriteria)binaryReader_0.ReadByte();
		lsoMatchFactorThreshold = binaryReader_0.ReadInt32();
		lsoMaxNumHits = binaryReader_0.ReadInt32();
		lsoRestrictWaveLength = binaryReader_0.ReadBoolean();
		lsoFrom = binaryReader_0.ReadInt32();
		lsoTo = binaryReader_0.ReadInt32();
		lsoRestrictRT = binaryReader_0.ReadBoolean();
		lsoRestrictRTV = binaryReader_0.ReadSingle();
		lsoUseBackCorr = binaryReader_0.ReadBoolean();
		lsoForAllDetectedPeaks = binaryReader_0.ReadBoolean();
		Array.Resize(ref pdaRows, binaryReader_0.ReadInt32());
		for (int j = 0; j < pdaRows.Length; j++)
		{
			pdaRows[j].LoadFromFile(binaryReader_0);
		}
		Array.Resize(ref percents, binaryReader_0.ReadInt32());
		for (int k = 0; k < percents.Length; k++)
		{
			percents[k].LoadFromFile(binaryReader_0);
		}
		Array.Resize(ref gpc_RangeRow_0, binaryReader_0.ReadInt32());
		for (int l = 0; l < gpc_RangeRow_0.Length; l++)
		{
			gpc_RangeRow_0[l].LoadFromFile(binaryReader_0);
		}
		LoadFromFile();
		RefreshAsInfo();
	}
}
