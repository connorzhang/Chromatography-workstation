using System;

namespace IBrainChrom2018.Unit;

public class OnLineCtrlParam
{
	private static OnLineCtrlParam myparam = null;

	private ParamBase dbBase = ParamBase.Create();

	public DateTime dataTimeStart;

	public DateTime dataTimeEnd;

	public float[] fStepTime = new float[10];

	public float fBriage;

	public bool bAutoModeHe;

	public int iTimes1;

	public int iTimes2;

	public int iTimes3;

	public int iTimes4;

	public int iTimes5;

	public int iTimes6;

	public int iTimes7;

	public int iTimes8;

	public int iTimes9;

	public int iTimes10;

	public int iTimes11;

	public int iTimes12;

	public int iCoerce;

	public float fStartTime;

	public float fCycleTime;

	public int iCycles;

	public static OnLineCtrlParam Create()
	{
		if (myparam == null)
		{
			myparam = new OnLineCtrlParam();
		}
		return myparam;
	}

	private OnLineCtrlParam()
	{
		LoadParam();
		if (!dbBase.ConfigFileExist)
		{
			SaveParam();
		}
	}

	public void ResetParam()
	{
		dbBase.ClearSection("OnLineCtrlParam");
		LoadParam();
	}

	public void LoadParam()
	{
		for (int i = 0; i < 10; i++)
		{
			fStepTime[i] = float.Parse(dbBase.GetValue("OnLineCtrlParam", "fStepTime" + i, "0"));
		}
		dataTimeStart = DateTime.Parse(dbBase.GetValue("OnLineCtrlParam", "dataTimeStart", DateTime.Now.ToString("yyyy-MM-dd HH:mm")));
		dataTimeEnd = DateTime.Parse(dbBase.GetValue("OnLineCtrlParam", "dataTimeEnd", DateTime.Now.ToString("yyyy-MM-dd HH:mm")));
		bAutoModeHe = dbBase.GetValue("OnLineCtrlParam", "bAutoModeHe", "0") == "1";
		iTimes1 = int.Parse(dbBase.GetValue("OnLineCtrlParam", "iTimes1", "0"));
		iTimes2 = int.Parse(dbBase.GetValue("OnLineCtrlParam", "iTimes2", "0"));
		iTimes3 = int.Parse(dbBase.GetValue("OnLineCtrlParam", "iTimes3", "0"));
		iTimes4 = int.Parse(dbBase.GetValue("OnLineCtrlParam", "iTimes4", "0"));
		iTimes5 = int.Parse(dbBase.GetValue("OnLineCtrlParam", "iTimes5", "0"));
		iTimes6 = int.Parse(dbBase.GetValue("OnLineCtrlParam", "iTimes6", "0"));
		iTimes7 = int.Parse(dbBase.GetValue("OnLineCtrlParam", "iTimes7", "0"));
		iTimes8 = int.Parse(dbBase.GetValue("OnLineCtrlParam", "iTimes8", "0"));
		iTimes9 = int.Parse(dbBase.GetValue("OnLineCtrlParam", "iTimes9", "0"));
		iTimes10 = int.Parse(dbBase.GetValue("OnLineCtrlParam", "iTimes10", "0"));
		iTimes11 = int.Parse(dbBase.GetValue("OnLineCtrlParam", "iTimes11", "0"));
		iTimes12 = int.Parse(dbBase.GetValue("OnLineCtrlParam", "iTimes12", "0"));
		iCoerce = int.Parse(dbBase.GetValue("OnLineCtrlParam", "iCoerce", "0"));
		fStartTime = float.Parse(dbBase.GetValue("OnLineCtrlParam", "fStartTime", "0"));
		fCycleTime = float.Parse(dbBase.GetValue("OnLineCtrlParam", "fCycleTime", "0"));
		iCycles = int.Parse(dbBase.GetValue("OnLineCtrlParam", "iCycles", "0"));
	}

	public void SaveParam()
	{
		for (int i = 0; i < fStepTime.Length; i++)
		{
			dbBase.SetValue("OnLineCtrlParam", "fStepTime" + i, fStepTime[i].ToString());
		}
		dbBase.SetValue("OnLineCtrlParam", "dataTimeStart", dataTimeStart.ToString("yyyy-MM-dd HH:mm"));
		dbBase.SetValue("OnLineCtrlParam", "dataTimeEnd", dataTimeEnd.ToString("yyyy-MM-dd HH:mm"));
		dbBase.SetValue("OnLineCtrlParam", "bAutoModeHe", bAutoModeHe ? "1" : "0");
		dbBase.SetValue("OnLineCtrlParam", "iTimes1", iTimes1.ToString());
		dbBase.SetValue("OnLineCtrlParam", "iTimes2", iTimes2.ToString());
		dbBase.SetValue("OnLineCtrlParam", "iTimes3", iTimes3.ToString());
		dbBase.SetValue("OnLineCtrlParam", "iTimes4", iTimes4.ToString());
		dbBase.SetValue("OnLineCtrlParam", "iTimes5", iTimes5.ToString());
		dbBase.SetValue("OnLineCtrlParam", "iTimes6", iTimes6.ToString());
		dbBase.SetValue("OnLineCtrlParam", "iTimes7", iTimes7.ToString());
		dbBase.SetValue("OnLineCtrlParam", "iTimes8", iTimes8.ToString());
		dbBase.SetValue("OnLineCtrlParam", "iTimes9", iTimes9.ToString());
		dbBase.SetValue("OnLineCtrlParam", "iTimes10", iTimes10.ToString());
		dbBase.SetValue("OnLineCtrlParam", "iTimes11", iTimes11.ToString());
		dbBase.SetValue("OnLineCtrlParam", "iTimes12", iTimes12.ToString());
		dbBase.SetValue("OnLineCtrlParam", "iCoerce", iCoerce.ToString());
		dbBase.SetValue("OnLineCtrlParam", "fStartTime", fStartTime.ToString());
		dbBase.SetValue("OnLineCtrlParam", "fCycleTime", fCycleTime.ToString());
		dbBase.SetValue("OnLineCtrlParam", "iCycles", iCycles.ToString());
		dbBase.Save();
	}
}
