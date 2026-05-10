using System;
using System.IO;
using System.Runtime.InteropServices;
using IBrainChrom2018.Unit;

namespace IBrainChrom2018;

[Serializable]
[StructLayout(LayoutKind.Sequential)]
public class Compound
{
	public CmpdInfo cmpdInfo;

	public CmpdFunc eFunc = new CmpdFunc();

	public CmpdFunc iFunc = new CmpdFunc();

	public Level[] levels = new Level[20];

	public bool used;

	public int LastLevelNo
	{
		get
		{
			for (int num = levels.Length - 1; num >= 0; num--)
			{
				if (levels[num].SecsNum != 0)
				{
					return num;
				}
			}
			return -1;
		}
	}

	public Compound()
	{
		for (int i = 0; i < levels.Length; i++)
		{
			levels[i] = new Level();
		}
		cmpdInfo.BLfloat = new float[10];
		for (int j = 0; j < cmpdInfo.BLfloat.Length; j++)
		{
			cmpdInfo.BLfloat[j] = 0f;
		}
		cmpdInfo.BLString = new string[10];
		for (int k = 0; k < cmpdInfo.BLString.Length; k++)
		{
			cmpdInfo.BLString[k] = "";
		}
	}

	public void Add_splLevel(int L, float retainTime, float responseA, float responseH, bool updateRT, RecaliMode recaliMode)
	{
		if (L >= 0 && L < levels.Length)
		{
			if (updateRT)
			{
				cmpdInfo.retainTime = retainTime;
			}
			levels[L].AddRec(responseA, responseH);
			levels[L].used = true;
			SetRecaliMode(recaliMode);
		}
	}

	public void Add_splLevel(int L, float retainTime, float responseA, float responseH, bool updateRT, RecaliMode recaliMode, float famount)
	{
		if (L >= 0 && L < levels.Length)
		{
			if (updateRT)
			{
				cmpdInfo.retainTime = retainTime;
			}
			levels[L].AddRec(responseA, responseH);
			levels[L].used = true;
			levels[L].amount = famount;
			SetRecaliMode(recaliMode);
		}
	}

	public void CalcuDisLg()
	{
		eFunc.disLg.lgXBeg = (eFunc.disLg.lgYBeg = 0f);
		eFunc.disLg.lgX = (eFunc.disLg.lgY = 0.01f);
		iFunc.disLg.lgXBeg = (iFunc.disLg.lgYBeg = 0f);
		iFunc.disLg.lgX = (iFunc.disLg.lgY = 0.01f);
		for (int i = 0; i < levels.Length; i++)
		{
			if (levels[i].eFuncPt.IsValid)
			{
				if (eFunc.curveFit == CurveFit.Exponent)
				{
					float val = Convert.ToSingle(Math.Log(levels[i].eFuncPt.AsPointF.X));
					float val2 = Convert.ToSingle(Math.Log(levels[i].eFuncPt.AsPointF.Y * 1000f));
					eFunc.disLg.lgX = Math.Max(eFunc.disLg.lgX, val);
					eFunc.disLg.lgY = Math.Max(eFunc.disLg.lgY, val2);
					eFunc.disLg.lgXBeg = Math.Min(eFunc.disLg.lgXBeg, Convert.ToSingle(Math.Log(levels[i].eFuncPt.AsPointF.X)));
					eFunc.disLg.lgYBeg = Math.Min(eFunc.disLg.lgYBeg, Convert.ToSingle(Math.Log(levels[i].eFuncPt.AsPointF.Y * 1000f)));
				}
				else
				{
					eFunc.disLg.lgX = Math.Max(eFunc.disLg.lgX, levels[i].eFuncPt.AsPointF.X);
					eFunc.disLg.lgY = Math.Max(eFunc.disLg.lgY, levels[i].eFuncPt.AsPointF.Y);
				}
			}
			if (levels[i].iFuncPt.IsValid)
			{
				iFunc.disLg.lgX = Math.Max(iFunc.disLg.lgX, levels[i].iFuncPt.AsPointF.X);
				iFunc.disLg.lgY = Math.Max(iFunc.disLg.lgY, levels[i].iFuncPt.AsPointF.Y);
			}
		}
		if (cmpdInfo.freeRespFactor > 0f && eFunc.curveFit == CurveFit.Free)
		{
			float val3 = eFunc.disLg.lgY * cmpdInfo.freeRespFactor;
			float val4 = eFunc.disLg.lgX / cmpdInfo.freeRespFactor;
			eFunc.disLg.lgX = Math.Max(eFunc.disLg.lgX, val3);
			eFunc.disLg.lgY = Math.Max(eFunc.disLg.lgY, val4);
		}
		if (cmpdInfo.freeRespFactor > 0f && iFunc.curveFit == CurveFit.Free)
		{
			float val5 = iFunc.disLg.lgY * cmpdInfo.freeRespFactor;
			float val6 = iFunc.disLg.lgX / cmpdInfo.freeRespFactor;
			iFunc.disLg.lgX = Math.Max(iFunc.disLg.lgX, val5);
			iFunc.disLg.lgY = Math.Max(iFunc.disLg.lgY, val6);
		}
		if (eFunc.curveFit == CurveFit.Exponent)
		{
			eFunc.disLg.lgXBeg = eFunc.disLg.lgXBeg - Math.Abs(eFunc.disLg.lgXBeg) / 15f;
			eFunc.disLg.lgYBeg = eFunc.disLg.lgYBeg - Math.Abs(eFunc.disLg.lgYBeg) / 15f;
		}
		eFunc.disLg.lgX = eFunc.disLg.lgX + Math.Abs(eFunc.disLg.lgX) / 15f;
		eFunc.disLg.lgY = eFunc.disLg.lgY + Math.Abs(eFunc.disLg.lgY) / 15f;
		iFunc.disLg.lgX = iFunc.disLg.lgX + Math.Abs(iFunc.disLg.lgX) / 15f;
		iFunc.disLg.lgY = iFunc.disLg.lgY + Math.Abs(iFunc.disLg.lgY) / 15f;
	}

	public void ClearLevel(int L)
	{
		if (L >= 0 && L < levels.Length)
		{
			levels[L].ClearRecs();
		}
	}

	public void ClearLevels()
	{
		for (int i = 0; i < levels.Length; i++)
		{
			levels[i].ClearRecs();
		}
	}

	public bool Contains(float retainTime)
	{
		FormMainParam formMainParam = FormMainParam.Create();
		if (formMainParam.bChannel)
		{
			return cmpdInfo.retainTime - cmpdInfo.retainTime * cmpdInfo.leftWindow / 100f <= retainTime && retainTime <= cmpdInfo.retainTime + cmpdInfo.retainTime * cmpdInfo.rightWindow / 100f;
		}
		return cmpdInfo.retainTime - cmpdInfo.leftWindow <= retainTime && retainTime <= cmpdInfo.retainTime + cmpdInfo.rightWindow;
	}

	public bool Fill_eFuncPts()
	{
		eFunc.FuncPtsNum = levels.Length;
		int funcPtsNum = 0;
		for (int i = 0; i < levels.Length; i++)
		{
			if (levels[i].used && levels[i].eFuncPt.IsValid)
			{
				eFunc.funcPts[funcPtsNum++] = levels[i].eFuncPt;
			}
		}
		eFunc.FuncPtsNum = funcPtsNum;
		return eFunc.Adjust_funcPts();
	}

	public bool Fill_iFuncPts()
	{
		iFunc.FuncPtsNum = levels.Length;
		int funcPtsNum = 0;
		for (int i = 0; i < levels.Length; i++)
		{
			if (levels[i].used && levels[i].iFuncPt.IsValid)
			{
				iFunc.funcPts[funcPtsNum++] = levels[i].iFuncPt;
			}
		}
		iFunc.FuncPtsNum = funcPtsNum;
		return iFunc.Adjust_funcPts();
	}

	public void Init()
	{
		for (int i = 0; i < levels.Length; i++)
		{
			if (cmpdInfo.respStyle == RespStyle.Area)
			{
				levels[i].response = levels[i].responseA;
			}
			else if (cmpdInfo.respStyle == RespStyle.Height)
			{
				levels[i].response = levels[i].responseH;
			}
			else if (cmpdInfo.respStyle == RespStyle.AreaSquare)
			{
				levels[i].response = (float)Math.Sqrt(levels[i].responseA);
			}
			else if (cmpdInfo.respStyle == RespStyle.PeakHeightSquare)
			{
				levels[i].response = (float)Math.Sqrt(levels[i].responseH);
			}
			if (levels[i].response != 0f)
			{
				levels[i].respFactor = levels[i].amount / levels[i].response;
			}
			else
			{
				levels[i].respFactor = 0f;
			}
			levels[i].eFuncPt.responseF = levels[i].response;
			levels[i].eFuncPt.amountF = levels[i].amount;
			levels[i].iFuncPt.responseF = levels[i].response;
			levels[i].iFuncPt.amountF = levels[i].amount;
		}
		eFunc.IsFinishCalcuAmountF = false;
		iFunc.IsFinishCalcuAmountF = false;
		eFunc.freeRespFactor = cmpdInfo.freeRespFactor;
		iFunc.freeRespFactor = cmpdInfo.freeRespFactor;
		eFunc.corrFactor = (eFunc.residuum = double.NaN);
		iFunc.corrFactor = (iFunc.residuum = double.NaN);
	}

	public void LoadFromObject(Compound compound)
	{
		used = compound.used;
		cmpdInfo = compound.cmpdInfo;
		for (int i = 0; i < levels.Length; i++)
		{
			levels[i].LoadFromObject(compound.levels[i]);
		}
		eFunc.LoadFromObject(compound.eFunc);
		iFunc.LoadFromObject(compound.iFunc);
	}

	public Compound Copy()
	{
		Compound compound = new Compound();
		compound.used = used;
		compound.cmpdInfo = cmpdInfo;
		compound.levels = new Level[levels.Length];
		for (int i = 0; i < levels.Length; i++)
		{
			compound.levels[i] = levels[i].Copy();
		}
		compound.eFunc = eFunc.Copy();
		compound.iFunc = iFunc.Copy();
		return compound;
	}

	public void SetRecaliMode(RecaliMode recaliMode)
	{
		for (int i = 0; i < levels.Length; i++)
		{
			levels[i].SetRecaliMode(recaliMode);
		}
	}

	public void LoadFromFile(BinaryReader binaryReader_0)
	{
		used = binaryReader_0.ReadBoolean();
		cmpdInfo.LoadFromFile(binaryReader_0);
		Array.Resize(ref levels, binaryReader_0.ReadInt32());
		for (int i = 0; i < levels.Length; i++)
		{
			if (levels[i] == null)
			{
				levels[i] = new Level();
			}
			levels[i].LoadFromFile(binaryReader_0);
		}
		eFunc.LoadFromFile(binaryReader_0);
		iFunc.LoadFromFile(binaryReader_0);
	}

	public void LoadFromFileOld(BinaryReader binaryReader_0)
	{
		used = binaryReader_0.ReadBoolean();
		cmpdInfo.LoadFromFileOld(binaryReader_0);
		Array.Resize(ref levels, binaryReader_0.ReadInt32());
		for (int i = 0; i < levels.Length; i++)
		{
			if (levels[i] == null)
			{
				levels[i] = new Level();
			}
			levels[i].LoadFromFile(binaryReader_0);
		}
		eFunc.LoadFromFile(binaryReader_0);
		iFunc.LoadFromFile(binaryReader_0);
	}

	public void SaveToFile(BinaryWriter binaryWriter_0)
	{
		binaryWriter_0.Write(used);
		cmpdInfo.SaveToFile(binaryWriter_0);
		binaryWriter_0.Write(levels.Length);
		for (int i = 0; i < levels.Length; i++)
		{
			levels[i].SaveToFile(binaryWriter_0);
		}
		eFunc.SaveToFile(binaryWriter_0);
		iFunc.SaveToFile(binaryWriter_0);
	}
}
