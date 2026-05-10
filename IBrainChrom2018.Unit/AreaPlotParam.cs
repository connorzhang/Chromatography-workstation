using System;

namespace IBrainChrom2018.Unit;

public class AreaPlotParam
{
	private ParamBase dbBase = ParamBase.Create();

	private string strAreaPlotName = "AreaPlot";

	public int myIdx = 0;

	public DateTime dataTimeStart;

	public DateTime dataTimeEnd;

	public string UintName;

	public string PeakName;

	public float TowerNumber;

	public float LowerLimit;

	public float UpperLimit;

	public AreaPlotParam(int idx)
	{
		myIdx = idx;
		strAreaPlotName = "AreaPlot" + myIdx;
		LoadParam();
		if (!dbBase.ConfigFileExist)
		{
			SaveParam();
		}
	}

	public void ResetParam()
	{
		dbBase.ClearSection(strAreaPlotName);
		LoadParam();
	}

	public void LoadParam()
	{
		dataTimeStart = DateTime.Parse(dbBase.GetValue(strAreaPlotName, "dataTimeStart", DateTime.Now.ToString("yyyy/MM/dd 00:00:01")));
		dataTimeEnd = DateTime.Parse(dbBase.GetValue(strAreaPlotName, "dataTimeEnd", DateTime.Now.ToString("yyyy/MM/dd") + " 23:59:59"));
		UintName = dbBase.GetValue(strAreaPlotName, "UintName", "");
		PeakName = dbBase.GetValue(strAreaPlotName, "PeakName", "");
		TowerNumber = float.Parse(dbBase.GetValue(strAreaPlotName, "TowerNumber", "1"));
		LowerLimit = float.Parse(dbBase.GetValue(strAreaPlotName, "LowerLimit", "0"));
		UpperLimit = float.Parse(dbBase.GetValue(strAreaPlotName, "UpperLimit", "100"));
	}

	public void SaveParam()
	{
		dbBase.SetValue(strAreaPlotName, "dataTimeStart", dataTimeStart.ToString("yyyy/MM/dd") + " 00:00:01");
		dbBase.SetValue(strAreaPlotName, "dataTimeEnd", dataTimeEnd.ToString("yyyy/MM/dd") + " 23:59:59");
		dbBase.SetValue(strAreaPlotName, "UintName", UintName);
		dbBase.SetValue(strAreaPlotName, "PeakName", PeakName);
		dbBase.SetValue(strAreaPlotName, "TowerNumber", TowerNumber.ToString());
		dbBase.SetValue(strAreaPlotName, "LowerLimit", LowerLimit.ToString());
		dbBase.SetValue(strAreaPlotName, "UpperLimit", UpperLimit.ToString());
		dbBase.Save();
	}
}
