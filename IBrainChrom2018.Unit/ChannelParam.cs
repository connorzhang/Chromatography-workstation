namespace IBrainChrom2018.Unit;

public class ChannelParam
{
	private static ChannelParam myparam = null;

	private ParamBase dbBase = ParamBase.Create();

	public float fShuaijian;

	public float fShuaijian2;

	public float fShuaijian3;

	public bool bAutoheight = true;

	public string strCompanyNameVOC = "VOC在线检测系统";

	public static ChannelParam Create()
	{
		if (myparam == null)
		{
			myparam = new ChannelParam();
		}
		return myparam;
	}

	private ChannelParam()
	{
		LoadParam();
		if (!dbBase.ConfigFileExist)
		{
			SaveParam();
		}
	}

	public void ResetParam()
	{
		dbBase.ClearSection("ChannelParam");
		LoadParam();
	}

	public void LoadParam()
	{
		fShuaijian = float.Parse(dbBase.GetValue("ChannelParam", "fShuanjian", "1"));
		fShuaijian2 = float.Parse(dbBase.GetValue("ChannelParam", "fShuanjian2", "1"));
		fShuaijian3 = float.Parse(dbBase.GetValue("ChannelParam", "fShuanjian3", "1"));
	}

	public void SaveParam()
	{
		dbBase.SetValue("ChannelParam", "fShuanjian", fShuaijian.ToString());
		dbBase.SetValue("ChannelParam", "fShuanjian2", fShuaijian2.ToString());
		dbBase.SetValue("ChannelParam", "fShuanjian3", fShuaijian3.ToString());
		dbBase.Save();
	}
}
