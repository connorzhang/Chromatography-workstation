using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;

namespace IBrainChrom2018.Unit;

[Serializable]
[StructLayout(LayoutKind.Sequential)]
public class ParamBase
{
	private const int iEvSingleOverTime = 5000;

	private static ParamBase pmBase = null;

	private DataSet dsConfig = null;

	private string strConfigVersion = "1.1";

	private string strOldConfigVersion = "1.0";

	private bool m_bConfigUpgrade = false;

	private int m_loadLoopTimes = 0;

	[NonSerialized]
	private AutoResetEvent m_mut = new AutoResetEvent(initialState: true);

	public bool ConfigFileExist { get; set; }

	public bool IsDirty { get; set; }

	public string ConfigVersion => strConfigVersion;

	public string ConfigVersionOld => strConfigVersion;

	public bool NeedConfigUpgrade => m_bConfigUpgrade;

	public string FileConfigVersion => GetKeyByConfig("ConfigVersion");

	public string ConfigFilePath => Application.ExecutablePath + ".xml";

	public string ConfigFilePathBackUp
	{
		get
		{
			string directoryName = Path.GetDirectoryName(Application.ExecutablePath);
			string fileName = Path.GetFileName(Application.ExecutablePath);
			return directoryName + "\\BackUp\\" + fileName + ".xml";
		}
	}

	public string ErrorMessage { get; internal set; }

	private ParamBase()
	{
		LoadConfig();
	}

	public static ParamBase Create()
	{
		if (pmBase == null)
		{
			pmBase = new ParamBase();
		}
		return pmBase;
	}

	private bool RestoryFromBackUpFile()
	{
		string configFilePath = ConfigFilePath;
		string configFilePathBackUp = ConfigFilePathBackUp;
		if (File.Exists(configFilePathBackUp))
		{
			File.Copy(configFilePathBackUp, configFilePath, overwrite: true);
			return true;
		}
		return false;
	}

	public void BackUpConfigFile()
	{
		string configFilePath = ConfigFilePath;
		string configFilePathBackUp = ConfigFilePathBackUp;
		string directoryName = Path.GetDirectoryName(configFilePathBackUp);
		if (!Directory.Exists(directoryName))
		{
			Directory.CreateDirectory(directoryName);
		}
		if (File.Exists(configFilePath))
		{
			File.Copy(configFilePath, configFilePathBackUp, overwrite: true);
		}
	}

	public void LoadConfig()
	{
		ConfigFileExist = true;
		string path = Application.ExecutablePath + ".xml";
		if (!File.Exists(path))
		{
			dsConfig = ConstructConfig();
			ConfigFileExist = false;
			return;
		}
		if (dsConfig == null)
		{
			dsConfig = new DataSet();
		}
		FileStream fileStream = new FileStream(path, FileMode.Open);
		try
		{
			if (fileStream.Length < 10)
			{
				fileStream.Close();
				fileStream = null;
				TryReloadConfig(null);
			}
			dsConfig.ReadXml(fileStream);
			fileStream.Close();
		}
		catch (Exception ex)
		{
			fileStream.Close();
			fileStream = null;
			TryReloadConfig(ex);
		}
		CheckConfigVersion();
	}

	private void TryReloadConfig(Exception ex)
	{
		m_loadLoopTimes++;
		if (m_loadLoopTimes > 2)
		{
			throw new Exception("配置文件已损坏，请检查或删除后重新打开软件!", ex);
		}
		ErrorMessage += "当前配置文件损坏，已从备份文件中加载!\r\n";
		RestoryFromBackUpFile();
		LoadConfig();
	}

	private void CheckConfigVersion()
	{
		strOldConfigVersion = GetKeyByConfig("ConfigVersion");
		if (strOldConfigVersion != strConfigVersion)
		{
			m_bConfigUpgrade = true;
			SetKeyByConfig("ConfigVersion", strConfigVersion);
		}
	}

	public void Save()
	{
		IsDirty = true;
	}

	public void SaveFinally()
	{
		if (m_mut.WaitOne(5000))
		{
			try
			{
				string path = Application.ExecutablePath + ".xml";
				MemoryStream memoryStream = new MemoryStream();
				dsConfig.WriteXml(memoryStream, XmlWriteMode.IgnoreSchema);
				byte[] array = new byte[memoryStream.Length];
				memoryStream.Position = 0L;
				memoryStream.Read(array, 0, (int)memoryStream.Length);
				memoryStream.Close();
				FileStream fileStream = new FileStream(path, FileMode.Create);
				fileStream.Write(array, 0, array.Length);
				fileStream.Close();
				IsDirty = false;
			}
			catch (Exception ex)
			{
				m_mut.Set();
				throw ex;
			}
			m_mut.Set();
		}
	}

	private DataSet ConstructConfig()
	{
		DataSet dataSet = new DataSet("configuration");
		DataTable dataTable = dataSet.Tables.Add("appSettings");
		dataTable.Columns.Add("name");
		dataTable.Columns.Add("value");
		DataRow dataRow = dataTable.NewRow();
		dataRow[0] = "ConfigVersion";
		dataRow[1] = ConfigVersion;
		dataRow.EndEdit();
		dataTable.Rows.Add(dataRow);
		return dataSet;
	}

	public string GetKeyByConfig(string key)
	{
		if (!m_mut.WaitOne(5000))
		{
			return "";
		}
		string name = "appSettings";
		string result = "";
		if (dsConfig.Tables[name] != null && dsConfig.Tables[name].Rows.Count > 0)
		{
			DataView dataView = new DataView(dsConfig.Tables[name]);
			dataView.RowFilter = "name='" + key + "'";
			if (dataView.Count > 0 && dataView[0][1] != null)
			{
				result = dataView[0][1].ToString();
			}
		}
		m_mut.Set();
		return result;
	}

	public string GetKeyByConfig(string key, string strdefault)
	{
		string text = GetKeyByConfig(key);
		if (text == "")
		{
			text = strdefault;
		}
		return text;
	}

	public void SetKeyByConfig(string key, string vals)
	{
		if (m_mut.WaitOne(5000))
		{
			string name = "appSettings";
			DataView dataView = new DataView(dsConfig.Tables[name]);
			dataView.RowFilter = "name='" + key + "'";
			if (dataView.Count > 0)
			{
				dataView[0][1] = vals;
			}
			else
			{
				DataRowView dataRowView = dataView.AddNew();
				dataRowView["name"] = key;
				dataRowView["value"] = vals;
				dataRowView.EndEdit();
			}
			m_mut.Set();
		}
	}

	public string GetValue(string SectionName, string Property, string strDefault)
	{
		string text = GetValue(SectionName, Property);
		if (text == "")
		{
			text = strDefault;
		}
		return text;
	}

	public bool GetValue(string SectionName, string Property, string strDefault, bool bvalue)
	{
		string text = GetValue(SectionName, Property);
		if (text == "")
		{
			text = strDefault;
		}
		return text == "1";
	}

	public int GetValue(string SectionName, string Property, string strDefault, int ivalue)
	{
		string text = GetValue(SectionName, Property);
		if (text == "")
		{
			text = strDefault;
		}
		int.TryParse(text, out ivalue);
		return ivalue;
	}

	public float GetValue(string SectionName, string Property, string strDefault, float fvalue)
	{
		string text = GetValue(SectionName, Property);
		if (text == "")
		{
			text = strDefault;
		}
		float.TryParse(text, out fvalue);
		return fvalue;
	}

	public double GetValue(string SectionName, string Property, string strDefault, double fvalue)
	{
		string text = GetValue(SectionName, Property);
		if (text == "")
		{
			text = strDefault;
		}
		double.TryParse(text, out fvalue);
		return fvalue;
	}

	public string GetValue(string SectionName, string Property, string strDefault, string strvalue)
	{
		string text = GetValue(SectionName, Property);
		if (text == "")
		{
			text = strDefault;
		}
		return text;
	}

	public float[] GetValue(string SectionName, string Property, string strDefault, float[] fvalueList)
	{
		string text = GetValue(SectionName, Property);
		if (text == "")
		{
			text = strDefault;
		}
		string[] array = text.Split(' ');
		float[] array2 = new float[array.Length];
		for (int i = 0; i < array.Length; i++)
		{
			float.TryParse(array[i], out array2[i]);
		}
		return array2;
	}

	public int[] GetValue(string SectionName, string Property, string strDefault, int[] fvalueList)
	{
		string text = GetValue(SectionName, Property);
		if (text == "")
		{
			text = strDefault;
		}
		string[] array = text.Split(' ');
		int[] array2 = new int[array.Length];
		for (int i = 0; i < array.Length; i++)
		{
			int.TryParse(array[i], out array2[i]);
		}
		return array2;
	}

	public string[] GetValue(string SectionName, string Property, string strDefault, string[] fvalueList)
	{
		string text = GetValue(SectionName, Property);
		if (text == "")
		{
			text = strDefault;
		}
		string[] array = text.Split('|');
		List<string> list = new List<string>();
		for (int i = 0; i < array.Length; i++)
		{
			if (array[i] != "")
			{
				list.Add(array[i]);
			}
		}
		return list.ToArray();
	}

	public RectangleF GetValue(string SectionName, string Property, string strDefault, RectangleF fvalueList)
	{
		string text = GetValue(SectionName, Property);
		if (text == "")
		{
			text = strDefault;
		}
		string[] array = text.Split(' ');
		float[] array2 = new float[array.Length];
		for (int i = 0; i < array.Length; i++)
		{
			float.TryParse(array[i], out array2[i]);
		}
		RectangleF result = default(RectangleF);
		if (array2.Length == 4)
		{
			result = new RectangleF(array2[0], array2[1], array2[2], array2[3]);
		}
		return result;
	}

	public string GetValue(string SectionName, string Property)
	{
		string result = "";
		if (!m_mut.WaitOne(5000))
		{
			return "";
		}
		if (dsConfig.Tables[SectionName] != null && dsConfig.Tables[SectionName].Rows.Count > 0 && dsConfig.Tables[SectionName].Columns.Contains(Property))
		{
			result = dsConfig.Tables[SectionName].Rows[0][Property].ToString();
		}
		m_mut.Set();
		return result;
	}

	public void SetValue(string SectionName, string Property, int value)
	{
		SetValue(SectionName, Property, value.ToString());
	}

	public void SetValue(string SectionName, string Property, float value)
	{
		SetValue(SectionName, Property, value.ToString());
	}

	public void SetValue(string SectionName, string Property, double value)
	{
		SetValue(SectionName, Property, value.ToString());
	}

	public void SetValue(string SectionName, string Property, bool value)
	{
		SetValue(SectionName, Property, value ? "1" : "0");
	}

	public void SetValue(string SectionName, string Property, Color value)
	{
		SetValue(SectionName, Property, value.ToArgb().ToString());
	}

	public void SetValue(string SectionName, string Property, float[] valueList)
	{
		string text = "";
		for (int i = 0; i < valueList.Length; i++)
		{
			text = text + valueList[i] + " ";
		}
		text = text.TrimEnd();
		SetValue(SectionName, Property, text);
	}

	public void SetValue(string SectionName, string Property, int[] valueList)
	{
		string text = "";
		for (int i = 0; i < valueList.Length; i++)
		{
			text = text + valueList[i] + " ";
		}
		text = text.TrimEnd();
		SetValue(SectionName, Property, text);
	}

	public void SetValue(string SectionName, string Property, string[] valueList)
	{
		string text = "";
		for (int i = 0; i < valueList.Length; i++)
		{
			if (valueList[i] != "")
			{
				text = text + valueList[i] + "|";
			}
		}
		text = text.TrimEnd();
		SetValue(SectionName, Property, text);
	}

	public void SetValue(string SectionName, string Property, RectangleF rect)
	{
		string value = rect.X + " " + rect.Y + " " + rect.Width + " " + rect.Height;
		SetValue(SectionName, Property, value);
	}

	public void SetValue(string SectionName, string Property, string value)
	{
		if (!m_mut.WaitOne(5000))
		{
			return;
		}
		DataTable dataTable = dsConfig.Tables[SectionName];
		if (dataTable == null)
		{
			dataTable = dsConfig.Tables.Add(SectionName);
			DataColumn dataColumn = dataTable.Columns.Add(Property);
			object[] values = new object[1] { value };
			dataTable.Rows.Add(values);
			m_mut.Set();
		}
		else if (!dsConfig.Tables[SectionName].Columns.Contains(Property))
		{
			DataColumn dataColumn2 = dataTable.Columns.Add(Property);
			if (dataTable.Rows.Count > 0)
			{
				dsConfig.Tables[SectionName].Rows[0][Property] = value;
			}
			else
			{
				DataView dataView = new DataView(dsConfig.Tables[SectionName]);
				DataRowView dataRowView = dataView.AddNew();
				dataRowView[Property] = value;
				dataRowView.EndEdit();
			}
			m_mut.Set();
		}
		else if (dsConfig.Tables[SectionName].Rows.Count == 0)
		{
			DataView dataView2 = new DataView(dsConfig.Tables[SectionName]);
			DataRowView dataRowView2 = dataView2.AddNew();
			dataRowView2[Property] = value;
			dataRowView2.EndEdit();
			m_mut.Set();
		}
		else
		{
			dsConfig.Tables[SectionName].Rows[0][Property] = value;
			m_mut.Set();
		}
	}

	public Dictionary<string, string> GetProperties(string SectionName)
	{
		if (!m_mut.WaitOne(5000))
		{
			return null;
		}
		Dictionary<string, string> dictionary = new Dictionary<string, string>();
		for (int i = 0; i < dsConfig.Tables[SectionName].Rows[0].ItemArray.Length; i++)
		{
			dictionary.Add(dsConfig.Tables[SectionName].Columns[i].Caption, dsConfig.Tables[SectionName].Rows[0].ItemArray[i].ToString());
		}
		m_mut.Set();
		return dictionary;
	}

	public void SetProperties(string SectionName, Dictionary<string, string> value)
	{
		if (!m_mut.WaitOne(5000))
		{
			return;
		}
		foreach (KeyValuePair<string, string> item in value)
		{
			SetValue(SectionName, item.Key, item.Value);
		}
		m_mut.Set();
	}

	public void ClearSection(string SectionName)
	{
		if (m_mut.WaitOne(5000))
		{
			if (dsConfig.Tables[SectionName] != null && dsConfig.Tables[SectionName].Rows.Count > 0)
			{
				dsConfig.Tables[SectionName].Clear();
			}
			m_mut.Set();
		}
	}

	public DataTable GetTable(string tableName)
	{
		return GetTable(tableName, -1);
	}

	public DataTable GetTable(string tableName, int ncol)
	{
		DataTable result = null;
		if (!m_mut.WaitOne(5000))
		{
			return result;
		}
		if (dsConfig.Tables[tableName] != null)
		{
			result = dsConfig.Tables[tableName];
		}
		else
		{
			result = dsConfig.Tables.Add(tableName);
			for (int i = 0; i < ncol; i++)
			{
				result.Columns.Add("col" + i);
			}
		}
		for (int j = result.Columns.Count; j < ncol; j++)
		{
			DataColumn dataColumn = result.Columns.Add("col" + j);
		}
		m_mut.Set();
		return result;
	}

	public string[] GetTableRow(string tableName, params string[] param)
	{
		int ncol = param.Length;
		string[] array = null;
		DataTable table = GetTable(tableName, ncol);
		if (table == null)
		{
			return array;
		}
		if (!m_mut.WaitOne(5000))
		{
			return array;
		}
		int count = table.Columns.Count;
		string text = param[0];
		DataView defaultView = table.DefaultView;
		defaultView.RowFilter = "col0='" + text + "'";
		if (defaultView.Count > 0)
		{
			array = new string[count];
			for (int i = 0; i < count; i++)
			{
				array[i] = defaultView[0][i].ToString();
			}
		}
		m_mut.Set();
		if (array == null)
		{
			array = param;
		}
		return array;
	}

	public void SetTableRow(string tableName, params string[] param)
	{
		int ncol = param.Length;
		DataTable table = GetTable(tableName, ncol);
		if (table == null || !m_mut.WaitOne(5000))
		{
			return;
		}
		int num = Math.Min(table.Columns.Count, param.Length);
		string text = param[0];
		DataView defaultView = table.DefaultView;
		defaultView.RowFilter = "col0='" + text + "'";
		if (defaultView.Count > 0)
		{
			for (int i = 0; i < num; i++)
			{
				defaultView[0][i] = param[i];
			}
			defaultView[0].EndEdit();
		}
		else
		{
			DataRowView dataRowView = defaultView.AddNew();
			for (int j = 0; j < num; j++)
			{
				dataRowView[j] = param[j];
			}
			dataRowView.EndEdit();
		}
		m_mut.Set();
	}
}
