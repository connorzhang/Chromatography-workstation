namespace IBrainChrom2018.Unit;

public class VocParam
{
	private static VocParam myparam = null;

	private ParamBase dbBase = ParamBase.Create();

	public float fShuanjian;

	public float fShuanjian2;

	public bool bAutoheight = true;

	public string strCompanyNameVOC = "VOC在线检测系统";

	public static VocParam Create()
	{
		if (myparam == null)
		{
			myparam = new VocParam();
		}
		return myparam;
	}

	private VocParam()
	{
		LoadParam();
		if (!dbBase.ConfigFileExist)
		{
			SaveParam();
		}
	}

	public void ResetParam()
	{
		dbBase.ClearSection("VocParam");
		LoadParam();
	}

	public void LoadParam()
	{
		fShuanjian = float.Parse(dbBase.GetValue("VocParam", "fShuanjian", "1"));
		fShuanjian2 = float.Parse(dbBase.GetValue("VocParam", "fShuanjian2", "1"));
		bAutoheight = dbBase.GetValue("VocParam", "bAutoheight", "1") == "1";
	}

	public void SaveParam()
	{
		dbBase.SetValue("VocParam", "fShuanjian", fShuanjian.ToString());
		dbBase.SetValue("VocParam", "fShuanjian2", fShuanjian2.ToString());
		dbBase.SetValue("VocParam", "bAutoheight", bAutoheight.ToString());
		dbBase.Save();
	}
}
