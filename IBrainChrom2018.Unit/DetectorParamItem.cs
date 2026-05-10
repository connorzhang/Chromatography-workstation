namespace IBrainChrom2018.Unit;

public class DetectorParamItem
{
	public int iID;

	public string strName;

	public string strFormula;

	public string strParamName;

	public string strParamUnit;

	public string strParamRemark;

	public float fParamValue;

	public float fTestValue;

	public bool bAllowAddtionParam;

	public string strAddtionParamName;

	public string strAddtionParamUnit;

	public DetectorParamItem()
	{
		iID = 64;
		strName = "FID1";
		strFormula = "";
		strParamName = Lang.PS("量程:", "Range:");
		strParamUnit = Lang.PS("次方:", "Power:");
		strParamRemark = Lang.PS("*输入范围7、8、9、10", "*input range7、8、9、10");
		fParamValue = 7f;
		fTestValue = 0.5f;
		bAllowAddtionParam = false;
		strAddtionParamName = Lang.PS("点火时长:", "IgnitionTime");
		strAddtionParamUnit = Lang.PS("秒:", "s");
	}

	public DetectorParamItem(int iID, string strName, string strFormula, string strParamName, string strParamUnit, string strParamRemark, float fParamValue, float fTestValue, bool bAllowAddtionParam, string strAddtionParamName, string strAddtionParamUnit)
	{
		this.iID = iID;
		this.strName = strName;
		this.strFormula = strFormula;
		this.strParamName = strParamName;
		this.strParamUnit = strParamUnit;
		this.strParamRemark = strParamRemark;
		this.fParamValue = fParamValue;
		this.fTestValue = fTestValue;
		this.bAllowAddtionParam = bAllowAddtionParam;
		this.strAddtionParamName = strAddtionParamName;
		this.strAddtionParamUnit = strAddtionParamUnit;
	}

	public DetectorParamItem(string[] strParams)
	{
		iID = GetInt(strParams, 0);
		strName = GetString(strParams, 1);
		strFormula = GetString(strParams, 2);
		strParamName = GetString(strParams, 3);
		strParamUnit = GetString(strParams, 4);
		strParamRemark = GetString(strParams, 5);
		fParamValue = GetFloat(strParams, 6);
		fTestValue = GetFloat(strParams, 7);
		bAllowAddtionParam = GetBool(strParams, 8);
		strAddtionParamName = GetString(strParams, 9);
		strAddtionParamUnit = GetString(strParams, 10);
	}

	public string[] ToStringList()
	{
		return new string[11]
		{
			iID.ToString(),
			strName,
			strFormula,
			strParamName,
			strParamUnit,
			strParamRemark,
			fParamValue.ToString(),
			fTestValue.ToString(),
			bAllowAddtionParam ? "1" : "0",
			strAddtionParamName,
			strAddtionParamUnit
		};
	}

	private int GetInt(string[] strParams, int id)
	{
		string s = GetString(strParams, id, "0");
		int result = 0;
		int.TryParse(s, out result);
		return result;
	}

	private float GetFloat(string[] strParams, int id)
	{
		string s = GetString(strParams, id, "0");
		float result = 0f;
		float.TryParse(s, out result);
		return result;
	}

	private bool GetBool(string[] strParams, int id)
	{
		string text = GetString(strParams, id, "0");
		return text == "1";
	}

	private string GetString(string[] strParams, int id)
	{
		return GetString(strParams, id, "");
	}

	private string GetString(string[] strParams, int id, string strdefault)
	{
		string result = strdefault;
		if (id < strParams.Length)
		{
			result = strParams[id];
		}
		return result;
	}
}
