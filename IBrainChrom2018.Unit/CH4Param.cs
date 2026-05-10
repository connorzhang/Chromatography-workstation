using System;

namespace IBrainChrom2018.Unit;

public class CH4Param
{
	private static CH4Param myparam = null;

	private ParamBase dbBase = ParamBase.Create();

	public float fShuaijian;

	public float fShuaijian2;

	public float fShuaijian3;

	public float ContentO2;

	public bool enNMHC;

	public string strContentO2;

	public float fShuanjian2;

	public string[] strSeqName1 = new string[1];

	public string[] strSeqName2 = new string[1];

	public string[] strSeqName3 = new string[1];

	public string[] strSeqName4 = new string[1];

	public int countSeq1;

	public int countSeq2;

	public int countSeq3;

	public int countSeq4;

	public bool bSeq;

	public static CH4Param Create()
	{
		if (myparam == null)
		{
			myparam = new CH4Param();
		}
		return myparam;
	}

	private CH4Param()
	{
		LoadParam();
		if (!dbBase.ConfigFileExist)
		{
			SaveParam();
		}
	}

	public void ResetParam()
	{
		dbBase.ClearSection("CH4Param");
		LoadParam();
	}

	public void LoadParam()
	{
		ContentO2 = float.Parse(dbBase.GetValue("CH4Param", "ContentO2", "1"));
		enNMHC = dbBase.GetValue("CH4Param", "enNMHC", "1") == "1";
		fShuaijian = float.Parse(dbBase.GetValue("CH4Param", "shuaijian1", "1"));
		fShuaijian2 = float.Parse(dbBase.GetValue("CH4Param", "shuaijian2", "1"));
		fShuaijian3 = float.Parse(dbBase.GetValue("CH4Param", "shuaijian3", "1"));
		countSeq1 = int.Parse(dbBase.GetValue("CH4Param", "countSeq1", "1"));
		countSeq2 = int.Parse(dbBase.GetValue("CH4Param", "countSeq2", "1"));
		countSeq3 = int.Parse(dbBase.GetValue("CH4Param", "countSeq3", "1"));
		countSeq4 = int.Parse(dbBase.GetValue("CH4Param", "countSeq4", "1"));
		bSeq = dbBase.GetValue("CH4Param", "bSeq", "1") == "1";
		Array.Resize(ref strSeqName1, countSeq1);
		Array.Resize(ref strSeqName2, countSeq2);
		Array.Resize(ref strSeqName3, countSeq3);
		Array.Resize(ref strSeqName4, countSeq4);
		for (int i = 0; i < countSeq1; i++)
		{
			strSeqName1[i] = dbBase.GetValue("CH4Param", "strSeqName1_" + i, i.ToString());
		}
		for (int j = 0; j < countSeq2; j++)
		{
			strSeqName2[j] = dbBase.GetValue("CH4Param", "strSeqName2_" + j, j.ToString());
		}
		for (int k = 0; k < countSeq3; k++)
		{
			strSeqName3[k] = dbBase.GetValue("CH4Param", "strSeqName3_" + k, k.ToString());
		}
		for (int l = 0; l < countSeq4; l++)
		{
			strSeqName4[l] = dbBase.GetValue("CH4Param", "strSeqName4_" + l, l.ToString());
		}
	}

	public void SaveParam()
	{
		dbBase.SetValue("CH4Param", "ContentO2", strContentO2);
		dbBase.SetValue("CH4Param", "enNMHC", enNMHC ? "1" : "0");
		dbBase.SetValue("CH4Param", "bSeq", bSeq ? "1" : "0");
		dbBase.SetValue("CH4Param", "shuaijian1", fShuaijian.ToString());
		dbBase.SetValue("CH4Param", "shuaijian2", fShuaijian2.ToString());
		dbBase.SetValue("CH4Param", "shuaijian3", fShuaijian3.ToString());
		dbBase.SetValue("CH4Param", "countSeq1", countSeq1.ToString());
		dbBase.SetValue("CH4Param", "countSeq2", countSeq2.ToString());
		dbBase.SetValue("CH4Param", "countSeq3", countSeq3.ToString());
		dbBase.SetValue("CH4Param", "countSeq4", countSeq4.ToString());
		for (int i = 0; i < strSeqName1.Length; i++)
		{
			dbBase.SetValue("CH4Param", "strSeqName1_" + i, strSeqName1[i]);
		}
		for (int j = 0; j < strSeqName2.Length; j++)
		{
			dbBase.SetValue("CH4Param", "strSeqName2_" + j, strSeqName2[j]);
		}
		for (int k = 0; k < strSeqName3.Length; k++)
		{
			dbBase.SetValue("CH4Param", "strSeqName3_" + k, strSeqName3[k]);
		}
		for (int l = 0; l < strSeqName4.Length; l++)
		{
			dbBase.SetValue("CH4Param", "strSeqName4_" + l, strSeqName4[l]);
		}
		dbBase.Save();
	}
}
