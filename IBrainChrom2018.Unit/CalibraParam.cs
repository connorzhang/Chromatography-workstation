namespace IBrainChrom2018.Unit;

public class CalibraParam
{
	private static CalibraParam myparam = null;

	private ParamBase dbBase = ParamBase.Create();

	public bool bAutoCalibra;

	public int iCollectTimes;

	public float fIntervalTime;

	public int iSampleDelay;

	public double fRSDLimit;

	public int iLevel = 0;

	public string strUnit = "";

	public string strLastTimeCalibra;

	public int iCurveFitSelect = 0;

	public int iOriginalSelect = 0;

	public ClassAutoCalibraComp[] autoCalibraComp = new ClassAutoCalibraComp[30];

	public ClassAutoCalibraComp[] autoCalibraComp2 = new ClassAutoCalibraComp[30];

	public ClassAutoCalibraComp[] autoCalibraComp3 = new ClassAutoCalibraComp[30];

	public ClassAutoCalibraComp[] autoCalibraComp4 = new ClassAutoCalibraComp[30];

	public static CalibraParam Create()
	{
		if (myparam == null)
		{
			myparam = new CalibraParam();
		}
		return myparam;
	}

	private CalibraParam()
	{
		LoadParam();
		if (!dbBase.ConfigFileExist)
		{
			SaveParam();
		}
	}

	public void ResetParam()
	{
		dbBase.ClearSection("CalibraParam");
		LoadParam();
	}

	public void LoadParam()
	{
		for (int i = 0; i < autoCalibraComp.Length; i++)
		{
			if (autoCalibraComp[i] == null)
			{
				autoCalibraComp[i] = new ClassAutoCalibraComp();
			}
			autoCalibraComp[i].strName = dbBase.GetValue("CalibraParam", "name" + i, i.ToString());
			autoCalibraComp[i].fCompAmountLevel = dbBase.GetValue("CalibraParam", "autoCalibraCompAmount" + i, "0", autoCalibraComp[i].fCompAmountLevel);
		}
		for (int j = 0; j < autoCalibraComp2.Length; j++)
		{
			if (autoCalibraComp2[j] == null)
			{
				autoCalibraComp2[j] = new ClassAutoCalibraComp();
			}
			autoCalibraComp2[j].strName = dbBase.GetValue("CalibraParam", "nameB" + j, j.ToString());
			autoCalibraComp2[j].fCompAmountLevel = dbBase.GetValue("CalibraParam", "autoCalibraCompAmountB" + j, "0", autoCalibraComp2[j].fCompAmountLevel);
		}
		for (int k = 0; k < autoCalibraComp3.Length; k++)
		{
			if (autoCalibraComp3[k] == null)
			{
				autoCalibraComp3[k] = new ClassAutoCalibraComp();
			}
			autoCalibraComp3[k].strName = dbBase.GetValue("CalibraParam", "nameC" + k, k.ToString());
			autoCalibraComp3[k].fCompAmountLevel = dbBase.GetValue("CalibraParam", "autoCalibraCompAmountC" + k, "0", autoCalibraComp3[k].fCompAmountLevel);
		}
		for (int l = 0; l < autoCalibraComp4.Length; l++)
		{
			if (autoCalibraComp4[l] == null)
			{
				autoCalibraComp4[l] = new ClassAutoCalibraComp();
			}
			autoCalibraComp4[l].strName = dbBase.GetValue("CalibraParam", "nameD" + l, l.ToString());
			autoCalibraComp4[l].fCompAmountLevel = dbBase.GetValue("CalibraParam", "autoCalibraCompAmountD" + l, "0", autoCalibraComp4[l].fCompAmountLevel);
		}
		bAutoCalibra = dbBase.GetValue("CalibraParam", "bAutoCalibra", "0") == "1";
		iCollectTimes = int.Parse(dbBase.GetValue("CalibraParam", "iCollectTimes", "0"));
		fIntervalTime = float.Parse(dbBase.GetValue("CalibraParam", "fIntervalTime", "0"));
		iSampleDelay = int.Parse(dbBase.GetValue("CalibraParam", "iSampleDelay", "0"));
		fRSDLimit = double.Parse(dbBase.GetValue("CalibraParam", "fRSDLimit", "0"));
		iLevel = int.Parse(dbBase.GetValue("CalibraParam", "iLevel", "0"));
		strUnit = dbBase.GetValue("CalibraParam", "strUnit", " ");
		strLastTimeCalibra = dbBase.GetValue("CalibraParam", "strLastTimeCalibra", " ");
		iOriginalSelect = int.Parse(dbBase.GetValue("CalibraParam", "iOriginalSelect", "0"));
		iCurveFitSelect = int.Parse(dbBase.GetValue("CalibraParam", "iCurveFitSelect", "0"));
	}

	public void SaveParam()
	{
		for (int i = 0; i < autoCalibraComp.Length; i++)
		{
			dbBase.SetValue("CalibraParam", "name" + i, autoCalibraComp[i].strName);
			dbBase.SetValue("CalibraParam", "autoCalibraCompAmount" + i, autoCalibraComp[i].fCompAmountLevel);
		}
		for (int j = 0; j < autoCalibraComp2.Length; j++)
		{
			dbBase.SetValue("CalibraParam", "nameB" + j, autoCalibraComp2[j].strName);
			dbBase.SetValue("CalibraParam", "autoCalibraCompAmountB" + j, autoCalibraComp2[j].fCompAmountLevel);
		}
		for (int k = 0; k < autoCalibraComp3.Length; k++)
		{
			dbBase.SetValue("CalibraParam", "nameC" + k, autoCalibraComp3[k].strName);
			dbBase.SetValue("CalibraParam", "autoCalibraCompAmountC" + k, autoCalibraComp3[k].fCompAmountLevel);
		}
		for (int l = 0; l < autoCalibraComp4.Length; l++)
		{
			dbBase.SetValue("CalibraParam", "nameD" + l, autoCalibraComp4[l].strName);
			dbBase.SetValue("CalibraParam", "autoCalibraCompAmountD" + l, autoCalibraComp4[l].fCompAmountLevel);
		}
		dbBase.SetValue("CalibraParam", "bAutoCalibra", bAutoCalibra ? "1" : "0");
		dbBase.SetValue("CalibraParam", "iCollectTimes", iCollectTimes.ToString());
		dbBase.SetValue("CalibraParam", "fIntervalTime", fIntervalTime.ToString("0.0"));
		dbBase.SetValue("CalibraParam", "iSampleDelay", iSampleDelay.ToString());
		dbBase.SetValue("CalibraParam", "fRSDLimit", fRSDLimit.ToString("0.0"));
		dbBase.SetValue("CalibraParam", "iLevel", iLevel.ToString());
		dbBase.SetValue("CalibraParam", "strUnit", strUnit);
		dbBase.SetValue("CalibraParam", "strLastTimeCalibra", strLastTimeCalibra);
		dbBase.SetValue("CalibraParam", "iOriginalSelect", iOriginalSelect.ToString());
		dbBase.SetValue("CalibraParam", "iCurveFitSelect", iCurveFitSelect.ToString());
		dbBase.Save();
	}
}
