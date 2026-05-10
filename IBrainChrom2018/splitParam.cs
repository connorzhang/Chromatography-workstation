using IBrainChrom2018.Unit;

namespace IBrainChrom2018;

public class splitParam
{
	private static splitParam myparam = null;

	private ParamBase dbBase = ParamBase.Create();

	public float fTemp1;

	public float fTemp2;

	public float fHoldTime;

	public float fInjTime;

	public float fCleanTime;

	public float fWaitTime;

	public float fWeight;

	public static splitParam Create()
	{
		if (myparam == null)
		{
			myparam = new splitParam();
		}
		return myparam;
	}

	private splitParam()
	{
		LoadParam();
		if (!dbBase.ConfigFileExist)
		{
			SaveParam();
		}
	}

	public void ResetParam()
	{
		dbBase.ClearSection("splitParam");
		LoadParam();
	}

	public void LoadParam()
	{
		fTemp1 = float.Parse(dbBase.GetValue("splitParam", "fTemp1", "0"));
		fTemp2 = float.Parse(dbBase.GetValue("splitParam", "fTemp2", "0"));
		fHoldTime = float.Parse(dbBase.GetValue("splitParam", "fHoldTime", "0"));
		fInjTime = float.Parse(dbBase.GetValue("splitParam", "fInjTime", "0"));
		fCleanTime = float.Parse(dbBase.GetValue("splitParam", "fCleanTime", "0"));
		fWaitTime = float.Parse(dbBase.GetValue("splitParam", "fWaitTime", "0"));
		fWeight = float.Parse(dbBase.GetValue("splitParam", "fWeight", "0"));
	}

	public void SaveParam()
	{
		dbBase.SetValue("splitParam", "fTemp1", fTemp1.ToString());
		dbBase.SetValue("splitParam", "fTemp2", fTemp2.ToString());
		dbBase.SetValue("splitParam", "fHoldTime", fHoldTime.ToString());
		dbBase.SetValue("splitParam", "fInjTime", fInjTime.ToString());
		dbBase.SetValue("splitParam", "fCleanTime", fCleanTime.ToString());
		dbBase.SetValue("splitParam", "fWaitTime", fWaitTime.ToString());
		dbBase.SetValue("splitParam", "fWeight", fWeight.ToString());
		dbBase.Save();
	}
}
