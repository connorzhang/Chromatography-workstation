using System;
using System.Collections;
using System.Data;
using System.Globalization;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using IBrainChrom2018.Unit;

namespace IBrainChrom2018;

public class Lang
{
	private static Lang myself = null;

	private Mutex m_mut = new Mutex(initiallyOwned: true);

	private DataSet dsConfig = null;

	private ParamBase sysParam = ParamBase.Create();

	private static string strLanguageLast = "key";

	private static string strLanguage = "zh-cn";

	private const string DataSetName = "LangDataSet";

	private const string DataTableName = "LangTable";

	private Hashtable hashlist = new Hashtable();

	public static LangID LangID
	{
		get
		{
			if (strLanguage == "zh-cn")
			{
				return LangID.CN;
			}
			if (strLanguage == "en")
			{
				return LangID.EN;
			}
			if (strLanguage == "ja")
			{
				return LangID.JA;
			}
			if (strLanguage == "vi")
			{
				return LangID.VI;
			}
			return LangID.CN;
		}
		set
		{
			strLanguage = LangIDToString(value);
			ParamBase paramBase = ParamBase.Create();
			paramBase.SetKeyByConfig("Language", strLanguage);
			paramBase.Save();
		}
	}

	private static int langID => (int)LangID;

	public static string LangStr => LangID switch
	{
		LangID.CN => "中文", 
		LangID.EN => "英文", 
		LangID.VI => "越南文", 
		LangID.JA => "日文", 
		_ => "中文", 
	};

	public static string LangCode => strLanguage;

	public bool ConfigFileExist { get; set; }

	public static Lang Create()
	{
		if (myself == null)
		{
			myself = new Lang();
		}
		return myself;
	}

	private Lang()
	{
		m_mut.ReleaseMutex();
		strLanguage = sysParam.GetKeyByConfig("Language", "zh-cn");
		strLanguageLast = strLanguage;
		if (dsConfig == null)
		{
			LoadConfig();
		}
		Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo(strLanguage);
	}

	public static LangID SetLangID(string strLang)
	{
		LangID langID = LangID.CN;
		return LangID = strLang switch
		{
			"zh-cn" => LangID.CN, 
			"en" => LangID.EN, 
			"ja" => LangID.VI, 
			"vi" => LangID.JA, 
			_ => LangID.CN, 
		};
	}

	public static LangID SetLastLangID(string strLang)
	{
		LangID langID = LangID.CN;
		langID = strLang switch
		{
			"zh-cn" => LangID.CN, 
			"en" => LangID.EN, 
			"ja" => LangID.VI, 
			"vi" => LangID.JA, 
			_ => LangID.CN, 
		};
		strLanguageLast = strLang;
		return langID;
	}

	public static string LangIDToString(LangID lang)
	{
		return lang switch
		{
			LangID.CN => "zh-cn", 
			LangID.EN => "en", 
			LangID.VI => "ja", 
			LangID.JA => "vi", 
			_ => "zh-cn", 
		};
	}

	public static string LangIDToString(int lang)
	{
		return lang switch
		{
			0 => "zh-cn", 
			1 => "en", 
			2 => "ja", 
			3 => "vi", 
			_ => "zh-cn", 
		};
	}

	private string ConfigFilePath()
	{
		return Application.StartupPath + "\\language.xml";
	}

	public void LoadConfig()
	{
		ConfigFileExist = true;
		m_mut.WaitOne();
		string path = ConfigFilePath();
		if (!File.Exists(path))
		{
			dsConfig = ConstructConfig();
			ConfigFileExist = false;
		}
		else
		{
			if (dsConfig == null)
			{
				dsConfig = new DataSet();
			}
			FileStream fileStream = new FileStream(path, FileMode.Open);
			if (fileStream.Length < 10)
			{
				fileStream.Close();
				fileStream = null;
				m_mut.ReleaseMutex();
				throw new Exception("语言文件已损坏，请检查或删除后重新打开软件！");
			}
			dsConfig.ReadXml(fileStream);
			if (dsConfig.Tables["LangTable"].Columns["zh-cn"] == null)
			{
				dsConfig.Tables["LangTable"].Columns.Add("zh-cn");
			}
			fileStream.Close();
			DataView defaultView = dsConfig.Tables["LangTable"].DefaultView;
			for (int i = 0; i < defaultView.Count; i++)
			{
				string key = (string)defaultView[i]["key"];
				if (defaultView[i][strLanguage] != DBNull.Value)
				{
					string value = (string)defaultView[i][strLanguage];
					DataRowView dataRowView = defaultView[i];
					hashlist[key] = value;
				}
			}
		}
		m_mut.ReleaseMutex();
	}

	public void Save()
	{
		m_mut.WaitOne();
		string path = ConfigFilePath();
		FileStream fileStream = new FileStream(path, FileMode.Create);
		dsConfig.WriteXml(fileStream, XmlWriteMode.IgnoreSchema);
		fileStream.Close();
		m_mut.ReleaseMutex();
	}

	private DataSet ConstructConfig()
	{
		DataSet dataSet = new DataSet("LangDataSet");
		DataTable dataTable = dataSet.Tables.Add("LangTable");
		dataTable.Columns.Add("key");
		dataTable.Columns.Add("zh-cn");
		dataTable.Columns.Add("en");
		dataTable.Columns.Add("vi");
		dataTable.Columns.Add("ja");
		DataRow dataRow = dataTable.NewRow();
		dataRow[0] = "中文";
		dataRow[1] = "中文";
		dataRow[2] = "en";
		dataRow[3] = "vi";
		dataRow[3] = "ja";
		dataRow.EndEdit();
		dataTable.Rows.Add(dataRow);
		return dataSet;
	}

	public string GetValueByKey(string key, int column)
	{
		string result = "";
		if (dsConfig.Tables["LangTable"] != null && dsConfig.Tables["LangTable"].Columns.Count > column && dsConfig.Tables["LangTable"].Rows.Count > 0)
		{
			DataView defaultView = dsConfig.Tables["LangTable"].DefaultView;
			try
			{
				defaultView.RowFilter = "key='" + key + "'";
				if (defaultView.Count > 0 && defaultView[0][column] != null)
				{
					result = defaultView[0][column].ToString();
				}
			}
			catch
			{
			}
		}
		return result;
	}

	public string GetValueByKey(string key, string strColumn)
	{
		string result = "";
		if (dsConfig.Tables["LangTable"] != null && dsConfig.Tables["LangTable"].Columns[strColumn] != null && dsConfig.Tables["LangTable"].Rows.Count > 0)
		{
			string text = "key='" + key + "'";
			string text2 = strLanguageLast + "='" + key + "'";
			DataView defaultView = dsConfig.Tables["LangTable"].DefaultView;
			try
			{
				defaultView.RowFilter = text;
				if (defaultView.Count > 0)
				{
					if (defaultView[0][strColumn] != null)
					{
						result = defaultView[0][strColumn].ToString();
					}
				}
				else if (text != text2)
				{
					defaultView.RowFilter = text2;
					if (defaultView.Count > 0 && defaultView[0][strColumn] != null)
					{
						result = defaultView[0][strColumn].ToString();
					}
				}
			}
			catch
			{
			}
		}
		return result;
	}

	public void SetValueByKey(string key, int column, string vals)
	{
		m_mut.WaitOne();
		if (dsConfig.Tables["LangTable"].Columns.Count < column)
		{
			m_mut.ReleaseMutex();
			throw new Exception("Lang.SetKeyByConfig 此语言ID在语言文件中不存在！");
		}
		DataView defaultView = dsConfig.Tables["LangTable"].DefaultView;
		try
		{
			defaultView.RowFilter = "key='" + key + "'";
			if (defaultView.Count > 0)
			{
				defaultView[0][column] = vals;
			}
			else
			{
				DataRowView dataRowView = defaultView.AddNew();
				dataRowView["key"] = key;
				dataRowView[column] = vals;
				dataRowView.EndEdit();
			}
		}
		catch
		{
		}
		m_mut.ReleaseMutex();
	}

	public void SetValueByKey(string key, string strcolumn, string vals)
	{
		m_mut.WaitOne();
		if (dsConfig.Tables["LangTable"].Columns[strcolumn] == null)
		{
			m_mut.ReleaseMutex();
			throw new Exception("Lang.SetKeyByConfig 此语言ID在语言文件中不存在！");
		}
		DataView defaultView = dsConfig.Tables["LangTable"].DefaultView;
		try
		{
			defaultView.RowFilter = "key='" + key + "'";
			if (defaultView.Count > 0)
			{
				defaultView[0][strcolumn] = vals;
			}
			else
			{
				DataRowView dataRowView = defaultView.AddNew();
				dataRowView["key"] = key;
				dataRowView[strcolumn] = vals;
				dataRowView.EndEdit();
			}
		}
		catch
		{
		}
		m_mut.ReleaseMutex();
	}

	public string GetKeyByValColumn(string values, string strColumn)
	{
		string result = "";
		if (dsConfig.Tables["LangTable"] != null && dsConfig.Tables["LangTable"].Columns[strColumn] != null && dsConfig.Tables["LangTable"].Rows.Count > 0)
		{
			DataView defaultView = dsConfig.Tables["LangTable"].DefaultView;
			defaultView.RowFilter = "[" + strColumn + "]='" + values + "'";
			if (defaultView.Count > 0 && defaultView[0]["key"] != null)
			{
				result = defaultView[0]["key"].ToString();
			}
		}
		return result;
	}

	public string GetValue(string key, params string[] param)
	{
		if (dsConfig == null)
		{
			LoadConfig();
		}
		string text = "";
		text = ((hashlist[key] != null) ? ((string)hashlist[key]) : key);
		if (text == "")
		{
			text = key;
		}
		return text;
	}

	public void SetValue(string key, params string[] param)
	{
		if (dsConfig == null)
		{
			LoadConfig();
		}
		if (param.Length != 0)
		{
			SetValueByKey(key, "zh-cn", key);
			for (int i = 0; i < param.Length; i++)
			{
				SetValueByKey(key, LangIDToString(i + 1), param[i]);
			}
			Save();
		}
		else
		{
			SetValueByKey(key, "zh-cn", key);
			Save();
		}
	}

	public static string PS(string key, params string[] param)
	{
		Lang lang = Create();
		return lang.GetValue(key, param);
	}

	public static void PV(string key, params string[] param)
	{
		Lang lang = Create();
		lang.SetValue(key, param);
	}
}
