namespace IBrainChrom2018.Unit;

public class LYTHCPara
{
	private static LYTHCPara myparam = null;

	private ParamBase dbBase = ParamBase.Create();

	public int detectorMode = 1;

	public float fShuaijian;

	public float fShuaijian2;

	public float fShuaijian3;

	public bool bAutoheight = true;

	public string strCompanyNameVOC = "";

	public float THCAmount = 0f;

	public float CH4Amount = 0f;

	public float fBTEXAmount1 = 0f;

	public float fBTEXAmount2 = 0f;

	public float fBTEXAmount3 = 0f;

	public float fBTEXAmount4 = 0f;

	public float fBTEXAmount5 = 0f;

	public float fBTEXAmount6 = 0f;

	public float fBTEXAmount7 = 0f;

	public float fBTEXAmount8 = 0f;

	public float fTHCAmountCali = 0f;

	public float fCH4AmountCali = 0f;

	public float fBTEX1 = 0f;

	public float fBTEX2 = 0f;

	public float fBTEX3 = 0f;

	public float fBTEX4 = 0f;

	public float fBTEX5 = 0f;

	public float fBTEX6 = 0f;

	public float fBTEX7 = 0f;

	public float fBTEX8 = 0f;

	public string strUnit = "";

	public int runMode;

	public int collectMode;

	public int collectTimes;

	public float collectTime;

	public float intervalTime;

	public string strCollectSite;

	public string strCollectP;

	public string strCollectMC;

	public string strCollectBH;

	public string strCollectSJDW;

	public string strCollectJYDW;

	public string strCollectJCXM;

	public string strCollectWenDu;

	public string strCollectShiDu;

	public string strCollectDQy;

	public string strCollectTime;

	public float fCatalytic;

	public float fSample;

	public float fSample2;

	public int iSample;

	public static LYTHCPara Create()
	{
		if (myparam == null)
		{
			myparam = new LYTHCPara();
		}
		return myparam;
	}

	private LYTHCPara()
	{
		LoadParam();
		if (!dbBase.ConfigFileExist)
		{
			SaveParam();
		}
	}

	public void ResetParam()
	{
		dbBase.ClearSection("LYTHCPara");
		LoadParam();
	}

	public void LoadParam()
	{
		fCatalytic = float.Parse(dbBase.GetValue("LYTHCPara", "fCatalytic", "100"));
		fSample = float.Parse(dbBase.GetValue("LYTHCPara", "fSample", "100"));
		fSample2 = float.Parse(dbBase.GetValue("LYTHCPara", "fSample2", "100"));
		fShuaijian = float.Parse(dbBase.GetValue("LYTHCPara", "fShuanjian", "1"));
		fShuaijian2 = float.Parse(dbBase.GetValue("LYTHCPara", "fShuanjian2", "1"));
		fShuaijian3 = float.Parse(dbBase.GetValue("LYTHCPara", "fShuanjian3", "1"));
		THCAmount = float.Parse(dbBase.GetValue("LYTHCPara", "THCAmount", "1"));
		CH4Amount = float.Parse(dbBase.GetValue("LYTHCPara", "CH4Amount", "1"));
		fTHCAmountCali = float.Parse(dbBase.GetValue("LYTHCPara", "fTHCAmountCali", "1"));
		fCH4AmountCali = float.Parse(dbBase.GetValue("LYTHCPara", "fCH4AmountCali", "1"));
		fBTEX1 = float.Parse(dbBase.GetValue("LYTHCPara", "fBTEX1", "1"));
		fBTEX2 = float.Parse(dbBase.GetValue("LYTHCPara", "fBTEX2", "1"));
		fBTEX3 = float.Parse(dbBase.GetValue("LYTHCPara", "fBTEX3", "1"));
		fBTEX4 = float.Parse(dbBase.GetValue("LYTHCPara", "fBTEX4", "1"));
		fBTEX5 = float.Parse(dbBase.GetValue("LYTHCPara", "fBTEX5", "1"));
		fBTEX6 = float.Parse(dbBase.GetValue("LYTHCPara", "fBTEX6", "1"));
		fBTEX7 = float.Parse(dbBase.GetValue("LYTHCPara", "fBTEX7", "1"));
		fBTEX8 = float.Parse(dbBase.GetValue("LYTHCPara", "fBTEX8", "1"));
		collectMode = int.Parse(dbBase.GetValue("LYTHCPara", "collectMode", "1"));
		collectTimes = int.Parse(dbBase.GetValue("LYTHCPara", "collectTimes", "5"));
		runMode = int.Parse(dbBase.GetValue("LYTHCPara", "runMode", "0"));
		iSample = int.Parse(dbBase.GetValue("LYTHCPara", "iSample", "1"));
		collectTime = float.Parse(dbBase.GetValue("LYTHCPara", "collectTime", "60"));
		intervalTime = float.Parse(dbBase.GetValue("LYTHCPara", "intervalTime", "0"));
		strCollectSite = dbBase.GetValue("LYTHCPara", "strCollectSite", "济南");
		strCollectP = dbBase.GetValue("LYTHCPara", "strCollectP", "负责人");
		detectorMode = int.Parse(dbBase.GetValue("LYTHCPara", "detectorMode", "0"));
		strCollectMC = dbBase.GetValue("LYTHCPara", "strCollectMC", "");
		strCollectBH = dbBase.GetValue("LYTHCPara", "strCollectBH", "");
		strCollectSJDW = dbBase.GetValue("LYTHCPara", "strCollectSJDW", "");
		strCollectJYDW = dbBase.GetValue("LYTHCPara", "strCollectJYDW", "");
		strCollectJCXM = dbBase.GetValue("LYTHCPara", "strCollectJCXM", "");
		strCollectWenDu = dbBase.GetValue("LYTHCPara", "strCollectWenDu", "");
		strCollectShiDu = dbBase.GetValue("LYTHCPara", "strCollectShiDu", "");
		strCollectDQy = dbBase.GetValue("LYTHCPara", "strCollectDQy", "");
		strCollectTime = dbBase.GetValue("LYTHCPara", "strCollectTime", "");
		strUnit = dbBase.GetValue("LYTHCPara", "strUnit", "mg/m³");
	}

	public void SaveParam()
	{
		dbBase.SetValue("LYTHCPara", "fCatalytic", fCatalytic.ToString());
		dbBase.SetValue("LYTHCPara", "fSample", fSample.ToString());
		dbBase.SetValue("LYTHCPara", "fSample2", fSample2.ToString());
		dbBase.SetValue("LYTHCPara", "fShuanjian", fShuaijian.ToString());
		dbBase.SetValue("LYTHCPara", "fShuanjian2", fShuaijian2.ToString());
		dbBase.SetValue("LYTHCPara", "fShuanjian3", fShuaijian3.ToString());
		dbBase.SetValue("LYTHCPara", "THCAmount", THCAmount.ToString());
		dbBase.SetValue("LYTHCPara", "CH4Amount", CH4Amount.ToString());
		dbBase.SetValue("LYTHCPara", "fTHCAmountCali", fTHCAmountCali.ToString());
		dbBase.SetValue("LYTHCPara", "fCH4AmountCali", fCH4AmountCali.ToString());
		dbBase.SetValue("LYTHCPara", "fBTEX1", fBTEX1.ToString());
		dbBase.SetValue("LYTHCPara", "fBTEX2", fBTEX2.ToString());
		dbBase.SetValue("LYTHCPara", "fBTEX3", fBTEX3.ToString());
		dbBase.SetValue("LYTHCPara", "fBTEX4", fBTEX4.ToString());
		dbBase.SetValue("LYTHCPara", "fBTEX5", fBTEX5.ToString());
		dbBase.SetValue("LYTHCPara", "fBTEX6", fBTEX6.ToString());
		dbBase.SetValue("LYTHCPara", "fBTEX7", fBTEX7.ToString());
		dbBase.SetValue("LYTHCPara", "fBTEX8", fBTEX8.ToString());
		dbBase.SetValue("LYTHCPara", "iSample", iSample.ToString());
		dbBase.SetValue("LYTHCPara", "runMode", runMode.ToString());
		dbBase.SetValue("LYTHCPara", "collectMode", collectMode.ToString());
		dbBase.SetValue("LYTHCPara", "collectTimes", collectTimes.ToString());
		dbBase.SetValue("LYTHCPara", "collectTime", collectTime.ToString());
		dbBase.SetValue("LYTHCPara", "intervalTime", intervalTime.ToString());
		dbBase.SetValue("LYTHCPara", "strCollectSite", strCollectSite);
		dbBase.SetValue("LYTHCPara", "strCollectP", strCollectP);
		dbBase.SetValue("LYTHCPara", "detectorMode", detectorMode.ToString());
		dbBase.SetValue("LYTHCPara", "strCollectMC", strCollectMC);
		dbBase.SetValue("LYTHCPara", "strCollectBH", strCollectBH);
		dbBase.SetValue("LYTHCPara", "strCollectSJDW", strCollectSJDW);
		dbBase.SetValue("LYTHCPara", "strCollectJYDW", strCollectJYDW);
		dbBase.SetValue("LYTHCPara", "strCollectJCXM", strCollectJCXM);
		dbBase.SetValue("LYTHCPara", "strCollectWenDu", strCollectWenDu);
		dbBase.SetValue("LYTHCPara", "strCollectShiDu", strCollectShiDu);
		dbBase.SetValue("LYTHCPara", "strCollectDQy", strCollectDQy);
		dbBase.SetValue("LYTHCPara", "strCollectTime", strCollectTime);
		dbBase.SetValue("LYTHCPara", "strUnit", strUnit);
		dbBase.Save();
	}
}
