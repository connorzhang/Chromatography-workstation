using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace IBrainChrom2018.Unit;

public class DetectorParam
{
	private ParamBase dbBase = ParamBase.Create();

	private static DetectorParam myself = null;

	private CalculateExpression exp = CalculateExpression.Create();

	public List<DetectorParamItem> approxLevelParam = new List<DetectorParamItem>();

	public static DetectorParam Create()
	{
		if (myself == null)
		{
			myself = new DetectorParam();
			myself.LoadParam();
		}
		return myself;
	}

	public void LoadParam()
	{
		string[] tableRow = dbBase.GetTableRow("DetectorParam", "64", "FID1", "(((XX)/1", Lang.PS("量程:", "Range"), Lang.PS("次方", "Power"), "7,8,9,10", "7", "0.5", "1", Lang.PS("点火时长:", "IgnitionTime"), "秒");
		string[] tableRow2 = dbBase.GetTableRow("DetectorParam", "65", "FID2", "(((XX)/1", Lang.PS("量程:", "Range"), Lang.PS("次方", "Power"), "7,8,9,10", "7", "0.5", "1", Lang.PS("点火时长:", "IgnitionTime"), "秒");
		string[] tableRow3 = dbBase.GetTableRow("DetectorParam", "80", "TCD1", "XX/1", Lang.PS("桥流1:", "Current"), Lang.PS("毫安", "mA"), Lang.PS("输入范围：0~200mA", "Range：0~200mA"), "100", "1", "0", "", "");
		string[] tableRow4 = dbBase.GetTableRow("DetectorParam", "81", "TCD2", "XX/1", Lang.PS("桥流1:", "Current"), Lang.PS("毫安", "mA"), Lang.PS("输入范围：0~200mA", "Range：0~200mA"), "100", "1", "0", "", "");
		string[] tableRow5 = dbBase.GetTableRow("DetectorParam", "112", "ECD1", "XX/1", Lang.PS("基流:", "Current"), Lang.PS("纳安", "nA"), "*0.05、0.1、0.2、0.5、1、2", "1", "0", "", "");
		string[] tableRow6 = dbBase.GetTableRow("DetectorParam", "113", "ECD2", "XX/1", Lang.PS("基流:", "Current"), Lang.PS("纳安", "nA"), "*0.05、0.1、0.2、0.5、1、2", "1", "0", "", "");
		string[] tableRow7 = dbBase.GetTableRow("DetectorParam", "128", "NPD1", "XX/1", Lang.PS("量程:", "Range"), Lang.PS("次方", "Power"), "*0.05、0.1、0.2、0.5、1、2", "1", "1", Lang.PS("铷珠电流", "Current"), Lang.PS("安", "A"));
		string[] tableRow8 = dbBase.GetTableRow("DetectorParam", "96", "FPD1", "XX/1", Lang.PS("量程:", "Range"), Lang.PS("次方", "Power"), "7,8,9,10", "7", "0.5", "1", Lang.PS("点火时长:", "IgnitionTime"), Lang.PS("秒", "s"));
		string[] tableRow9 = dbBase.GetTableRow("DetectorParam", "97", "FPD2", "XX/1", Lang.PS("量程:", "Range"), Lang.PS("次方", "Power"), "*0、1、2", "1", "0", "", "");
		string[] tableRow10 = dbBase.GetTableRow("DetectorParam", "117", "Custom1", "XX/1", Lang.PS("量程:", "Range"), Lang.PS("次方", "Power"), "*0、1、2", "1", "0", "", "");
		string[] tableRow11 = dbBase.GetTableRow("DetectorParam", "118", "Custom1", "XX/1", Lang.PS("量程:", "Range"), Lang.PS("次方", "Power"), "*0、1、2", "1", "0", "", "");
		string[] tableRow12 = dbBase.GetTableRow("DetectorParam", "119", "Custom1", "XX/1", Lang.PS("量程:", "Range"), Lang.PS("次方", "Power"), "*0、1、2", "1", "0", "", "");
		string[] tableRow13 = dbBase.GetTableRow("DetectorParam", "120", "Custom1", "XX/1", Lang.PS("量程:", "Range"), Lang.PS("次方", "Power"), "*0、1、2", "1", "0", "", "");
		string[] tableRow14 = dbBase.GetTableRow("DetectorParam", "121", "Custom1", "XX/1", Lang.PS("量程:", "Range"), Lang.PS("次方", "Power"), "*0、1、2", "1", "0", "", "");
		string[] tableRow15 = dbBase.GetTableRow("DetectorParam", "122", "Custom1", "XX/1", Lang.PS("量程:", "Range"), Lang.PS("次方", "Power"), "*0、1、2", "1", "0", "", "");
		string[] tableRow16 = dbBase.GetTableRow("DetectorParam", "123", "Custom1", "XX/1", Lang.PS("量程:", "Range"), Lang.PS("次方", "Power"), "*0、1、2", "1", "0", "", "");
		approxLevelParam.Clear();
		approxLevelParam.Add(new DetectorParamItem(tableRow));
		approxLevelParam.Add(new DetectorParamItem(tableRow2));
		approxLevelParam.Add(new DetectorParamItem(tableRow3));
		approxLevelParam.Add(new DetectorParamItem(tableRow4));
		approxLevelParam.Add(new DetectorParamItem(tableRow5));
		approxLevelParam.Add(new DetectorParamItem(tableRow6));
		approxLevelParam.Add(new DetectorParamItem(tableRow7));
		approxLevelParam.Add(new DetectorParamItem(tableRow8));
		approxLevelParam.Add(new DetectorParamItem(tableRow9));
		approxLevelParam.Add(new DetectorParamItem(tableRow10));
		approxLevelParam.Add(new DetectorParamItem(tableRow11));
		approxLevelParam.Add(new DetectorParamItem(tableRow12));
		approxLevelParam.Add(new DetectorParamItem(tableRow13));
		approxLevelParam.Add(new DetectorParamItem(tableRow14));
		approxLevelParam.Add(new DetectorParamItem(tableRow15));
		approxLevelParam.Add(new DetectorParamItem(tableRow16));
		string[] keyList = approxLevelParam.Select((DetectorParamItem x) => x.iID.ToString()).ToList().ToArray();
		string[] expList = approxLevelParam.Select((DetectorParamItem x) => x.strFormula).ToList().ToArray();
		exp.AddCalculate(keyList, expList, 1);
	}

	public void SaveParam()
	{
		for (int i = 0; i < approxLevelParam.Count; i++)
		{
			dbBase.SetTableRow("DetectorParam", approxLevelParam[i].ToStringList());
		}
		dbBase.Save();
	}

	public DataTable GetDataTable()
	{
		DataTable dataTable = new DataTable();
		dataTable.Columns.Add("col0", typeof(int));
		dataTable.Columns.Add("col1", typeof(string));
		dataTable.Columns.Add("col2", typeof(string));
		dataTable.Columns.Add("col3", typeof(string));
		dataTable.Columns.Add("col4", typeof(string));
		dataTable.Columns.Add("col5", typeof(string));
		dataTable.Columns.Add("col6", typeof(float));
		dataTable.Columns.Add("col7", typeof(float));
		dataTable.Columns.Add("col8", typeof(bool));
		dataTable.Columns.Add("col9", typeof(string));
		dataTable.Columns.Add("col10", typeof(string));
		dataTable.Columns.Add("col11", typeof(float));
		DataView defaultView = dataTable.DefaultView;
		for (int i = 0; i < approxLevelParam.Count; i++)
		{
			DataRowView dataRowView = defaultView.AddNew();
			dataRowView[0] = approxLevelParam[i].iID;
			dataRowView[1] = approxLevelParam[i].strName;
			dataRowView[2] = approxLevelParam[i].strFormula;
			dataRowView[3] = approxLevelParam[i].strParamName;
			dataRowView[4] = approxLevelParam[i].strParamUnit;
			dataRowView[5] = approxLevelParam[i].strParamRemark;
			dataRowView[6] = approxLevelParam[i].fParamValue;
			dataRowView[7] = approxLevelParam[i].fTestValue;
			dataRowView[8] = approxLevelParam[i].bAllowAddtionParam;
			dataRowView[9] = approxLevelParam[i].strAddtionParamName;
			dataRowView[10] = approxLevelParam[i].strAddtionParamUnit;
			dataRowView[11] = 0f;
			dataRowView.EndEdit();
		}
		defaultView = null;
		return dataTable;
	}

	public void SaveParam(DataTable dtApprox)
	{
		approxLevelParam.Clear();
		DataView defaultView = dtApprox.DefaultView;
		for (int i = 0; i < defaultView.Count; i++)
		{
			DataRowView dataRowView = defaultView[i];
			DetectorParamItem item = new DetectorParamItem((int)dataRowView[0], (string)dataRowView[1], (string)dataRowView[2], (string)dataRowView[3], (string)dataRowView[4], (string)dataRowView[5], (float)dataRowView[6], (float)dataRowView[7], (bool)dataRowView[8], (string)dataRowView[9], (string)dataRowView[10]);
			approxLevelParam.Add(item);
		}
		SaveParam();
	}

	public double TestFoumular(int id, double value)
	{
		return (double)exp.RunExpression(id.ToString(), value);
	}

	public double TestFoumular(string strFormula, double value)
	{
		strFormula = strFormula.Replace("XX", value.ToString());
		return (double)CalculateExpression.Calculate(strFormula);
	}

	public DetectorParamItem GetDetectorParamItem(int id)
	{
		List<DetectorParamItem> list = approxLevelParam.Where((DetectorParamItem x) => x.iID == id).ToList();
		if (list.Count == 0)
		{
			return null;
		}
		return list[0];
	}

	public DetectorParamItem GetDetectorParamItem(string strName)
	{
		List<DetectorParamItem> list = approxLevelParam.Where((DetectorParamItem x) => x.strName == strName).ToList();
		if (list.Count == 0)
		{
			return null;
		}
		return list[0];
	}
}
