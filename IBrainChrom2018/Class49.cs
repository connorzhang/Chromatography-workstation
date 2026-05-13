using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using IBrainChrom2018.Unit;

namespace IBrainChrom2018;

public static class Class49
{
	public static string strSaveFilePath;

	public static int ChannelIndex;

	public static bool bUpdateChrAcqCtrlState = false;

	public static bool bOpenChromForm = false;

	public static bool bUpdateFormMainParam = false;

	public static bool bUpdateDataCtrl = false;

	public static Color[] color_0 = new Color[5]
	{
		Color.DarkGray,
		Color.DarkBlue,
		Color.DarkGreen,
		Color.DarkRed,
		Color.Black
	};

	public static float fTemp;

	public static float fAtm;

	public static float fInjectionVolume;

	public static string strDB = "";

	public static string string_8 = "VER2.0";

	public static string[] string_9 = new string[5] { "系统", "方法", "谱图", "打印", "反控" };

	public static User user_0 = new User();

	public static bool bool_1 = false;

	public static bool bool_2 = false;

	public static int int_8 = 4;

	public static bool bool_3 = false;

	public static string string_10 = "COM1";

	public static string string_11 = "COM2";

	public static int int_9 = 0;

	public static int int_10 = 100;

	public static int int_11 = 1;

	public static string string_12 = Application.StartupPath;

	public static string string_13 = Application.StartupPath;

	public static string strSdaDataFileDir = Application.StartupPath;

	public static string string_15 = MesureUnit() + ".s";

	public static string string_16 = MesureUnit();

	public static KAlphaDlg kalphaDlg_0 = new KAlphaDlg();

	public static OptionsDialog optionsDialog_0 = new OptionsDialog();

	public static Edition edition_0 = Edition.VI2010G;

	public static LoginDlg loginDlg_0 = new LoginDlg();

	private static byte byte_0 = 0;

	public static byte byte_1 = 0;

	public static Random random_0 = new Random((int)DateTime.Now.Ticks);

	public static SysLanguage sysLanguage_0 = SysLanguage.CN;

	public static bool bool_4 = false;

	public static int int_12 = 0;

	public unsafe static float Byte2Float(byte[] byte_2)
	{
		float result = 0f;
		byte* ptr = null;
		if (byte_2 != null && byte_2.Length != 0)
		{
			fixed (byte* ptr2 = &byte_2[0])
			{
			}
		}
		else
		{
			ptr = null;
		}
		void* ptr3 = &result;
		for (byte b = 0; b < byte_2.Length; b++)
		{
			((sbyte*)ptr3)[(int)b] = (sbyte)ptr[(int)b];
		}
		ptr = null;
		return result;
	}

	public unsafe static byte[] Float2Byte(float float_0)
	{
		byte* ptr = (byte*)(&float_0);
		byte[] array = new byte[4];
		for (int i = 0; i < 4; i++)
		{
			array[i] = *(ptr++);
		}
		return new byte[4]
		{
			array[1],
			array[0],
			array[3],
			array[2]
		};
	}

	public static void Append2Array(ref int[] int_13, int int_14)
	{
		Array.Resize(ref int_13, int_13.Length + 1);
		int_13[int_13.Length - 1] = int_14;
	}

	public static void Append2Array(ref float[] float_0, float float_1)
	{
		Array.Resize(ref float_0, float_0.Length + 1);
		float_0[float_0.Length - 1] = float_1;
	}

	public static void Append2Array(ref string[] string_17, string string_18, bool bool_5)
	{
		bool flag = false;
		for (int i = 0; i < string_17.Length; i++)
		{
			if (string_17[i] == string_18)
			{
				flag = true;
				break;
			}
		}
		if (!bool_5 || !flag)
		{
			int num = string_17.Length;
			Array.Resize(ref string_17, num + 1);
			string_17[num] = string_18;
		}
	}

	public static string Byte2String(byte[] byte_2)
	{
		if (byte_2 == null)
		{
			return "null";
		}
		StringBuilder stringBuilder = new StringBuilder(byte_2.Length);
		for (int i = 0; i < byte_2.Length; i++)
		{
			stringBuilder.Append(" " + byte_2[i].ToString("X2"));
		}
		return stringBuilder.ToString();
	}

	public static float takeDecimal(float fdata, int iDigits)
	{
		float num = (float)Math.Pow(10.0, iDigits);
		return (float)(int)(fdata * num) / num;
	}

	public static double takeDouble(double fdata, int iDigits)
	{
		double num = 10.0 * (double)iDigits;
		return (double)(int)(fdata * num) / num;
	}

	public static bool smethod_6()
	{
		return true;
	}

	public static void FileStreamClose(ref FileStream fileStream_0, ref BinaryReader binaryReader_0)
	{
		if (binaryReader_0 != null)
		{
			binaryReader_0.Close();
		}
		if (fileStream_0 != null)
		{
			fileStream_0.Close();
		}
	}

	public static void FileStreamClose(ref FileStream fileStream_0, ref BinaryWriter binaryWriter_0)
	{
		if (binaryWriter_0 != null)
		{
			binaryWriter_0.Flush();
		}
		if (fileStream_0 != null)
		{
			fileStream_0.Flush();
		}
		if (binaryWriter_0 != null)
		{
			binaryWriter_0.Close();
		}
		if (fileStream_0 != null)
		{
			fileStream_0.Close();
		}
	}

	public static void SafeValueCheck(ref double double_0, double double_1, double double_2)
	{
		if (double_0 < double_1)
		{
			double_0 = double_1;
		}
		if (double_0 > double_2)
		{
			double_0 = double_2;
		}
	}

	public static void SafeValueCheck(ref int int_13, int int_14, int int_15)
	{
		if (int_13 < int_14)
		{
			int_13 = int_14;
		}
		if (int_13 > int_15)
		{
			int_13 = int_15;
		}
	}

	public static void SafeValueCheck(ref float float_0, float float_1, float float_2)
	{
		if (float_0 < float_1)
		{
			float_0 = float_1;
		}
		if (float_0 > float_2)
		{
			float_0 = float_2;
		}
	}

	public static bool ValueInArray(int[] int_13, int int_14)
	{
		for (int i = 0; i < int_13.Length; i++)
		{
			if (int_13[i] == int_14)
			{
				return true;
			}
		}
		return false;
	}

	public static bool ValueInArray(string[] string_17, string string_18)
	{
		for (int i = 0; i < string_17.Length; i++)
		{
			if (string_17[i] == string_18)
			{
				return true;
			}
		}
		return false;
	}

	public static void smethod_14(string string_17, string string_18)
	{
		try
		{
			if (!Directory.Exists(string_18))
			{
				Directory.CreateDirectory(string_18);
			}
			DirectoryInfo directoryInfo = new DirectoryInfo(string_17);
			FileSystemInfo[] fileSystemInfos = directoryInfo.GetFileSystemInfos();
			foreach (FileSystemInfo fileSystemInfo in fileSystemInfos)
			{
				string text = Path.Combine(string_18, fileSystemInfo.Name);
				if (fileSystemInfo is FileInfo)
				{
					File.Copy(fileSystemInfo.FullName, text);
					continue;
				}
				Directory.CreateDirectory(text);
				smethod_14(fileSystemInfo.FullName, text);
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message);
		}
	}

	public static float String2Float(object object_0, float float_0)
	{
		if (!(object_0 is float result))
		{
			if (object_0 != null)
			{
				string str = object_0.ToString();
				str = str.Replace("_", "").Replace(" ", "").Trim();
				float result2 = 0f;
				if (float.TryParse(str, out result2))
				{
					return result2;
				}
			}
			return float_0;
		}
		return result;
	}

	public static string MakeFileFilter(string string_17)
	{
		return "(*" + string_17 + ")|*" + string_17;
	}

	private static bool IsContainColumn(LclGridView lclGridView_0, ref int int_13, ref DataGridViewColumn dataGridViewColumn_0)
	{
		for (int i = 0; i < lclGridView_0.ColumnCount; i++)
		{
			if (lclGridView_0.Columns[i].DisplayIndex == int_13 && lclGridView_0.Columns[i].Visible)
			{
				dataGridViewColumn_0 = lclGridView_0.Columns[i];
				int_13++;
				return true;
			}
		}
		return false;
	}

	public static byte[] smethod_18(string string_17)
	{
		FileStream fileStream = new FileInfo(string_17).Open(FileMode.Open);
		byte[] result = new MD5CryptoServiceProvider().ComputeHash(fileStream);
		fileStream.Close();
		return result;
	}

	public static void SetGridViewInfo(LclGridView lclGridView_0, ref GvInfos gvInfos_0, string[] string_17)
	{
		int int_ = 0;
		gvInfos_0.SetLength(lclGridView_0.ColumnCount);
		DataGridViewColumn dataGridViewColumn_ = null;
		int num = 0;
		while (IsContainColumn(lclGridView_0, ref int_, ref dataGridViewColumn_))
		{
			if (string_17 == null || !ValueInArray(string_17, dataGridViewColumn_.Name))
			{
				gvInfos_0.colNames[num] = dataGridViewColumn_.Name;
				gvInfos_0.colHdrTxts[num] = dataGridViewColumn_.HeaderText;
				gvInfos_0.colAligns[num++] = lclGridView_0.ConvertColAlign(dataGridViewColumn_.Index);
			}
		}
		gvInfos_0.SetLength(num);
	}

	public static int Object2Int(object object_0, int int_13)
	{
		if (!(object_0 is int result))
		{
			if (object_0 != null)
			{
				string s = object_0.ToString().Trim();
				int result2 = 0;
				if (int.TryParse(s, out result2))
				{
					return result2;
				}
			}
			return int_13;
		}
		return result;
	}

	public static bool IsByteArrayEqual(byte[] byte_2, byte[] byte_3)
	{
		if (byte_2.Length != byte_3.Length)
		{
			return false;
		}
		int i;
		for (i = 0; i < byte_2.Length && byte_2[i] == byte_3[i]; i++)
		{
		}
		return i == byte_2.Length;
	}

	public static float MaxValue(float[] float_0)
	{
		if (float_0 != null && float_0.Length != 0)
		{
			float num = float.MinValue;
			for (int i = 0; i < float_0.Length; i++)
			{
				num = Math.Max(num, float_0[i]);
			}
			return num;
		}
		throw new Exception("数组空!");
	}

	public static float MinValue(float[] float_0)
	{
		if (float_0 != null && float_0.Length != 0)
		{
			float num = float.MaxValue;
			for (int i = 0; i < float_0.Length; i++)
			{
				num = Math.Min(num, float_0[i]);
			}
			return num;
		}
		throw new Exception("数组空!");
	}

	public static void MessageBoxCheckInput()
	{
		MessageBox.Show(Lang.PS("请检查输入！", "Please check input!"));
	}

	public static bool smethod_26(string string_17, out string string_18, out string string_19, out string string_20)
	{
		string_18 = (string_19 = (string_20 = ""));
		try
		{
			FileInfo fileInfo = new FileInfo(string_17);
			if (fileInfo.Exists)
			{
				string_18 = fileInfo.DirectoryName;
				string_19 = fileInfo.Name.Remove(fileInfo.Name.Length - fileInfo.Extension.Length);
				string_20 = fileInfo.Extension;
			}
			return fileInfo.Exists;
		}
		catch
		{
			return false;
		}
	}

	public static Color SetColor(int int_13, Color color_1)
	{
		if (0 <= int_13 && int_13 < color_0.Length)
		{
			color_0[int_13] = color_1;
		}
		return color_1;
	}

	public static Color GetColor(int int_13)
	{
		if (0 <= int_13 && int_13 < color_0.Length)
		{
			return color_0[int_13];
		}
		return Color.Black;
	}

	public static void SetColor2(int int_13, Color color_1)
	{
		if (0 <= int_13 && int_13 < color_0.Length)
		{
			color_0[int_13] = color_1;
		}
	}

	public static void OpenBinaryReader(string filePath, out FileInfo fileInfo_0, out FileStream fileStream_0, out BinaryReader binaryReader_0)
	{
		fileInfo_0 = new FileInfo(filePath);
		fileStream_0 = fileInfo_0.Open(FileMode.Open);
		binaryReader_0 = new BinaryReader(fileStream_0);
	}

	public static void OpenBinaryWriter(string filePath, out FileInfo fileInfo_0, out FileStream fileStream_0, out BinaryWriter binaryWriter_0)
	{
		fileInfo_0 = new FileInfo(filePath);
		if (!fileInfo_0.Directory.Exists)
		{
			fileInfo_0.Directory.Create();
		}
		fileStream_0 = fileInfo_0.Open(FileMode.OpenOrCreate);
		binaryWriter_0 = new BinaryWriter(fileStream_0);
	}

	[DllImport("user32.dll", CallingConvention = CallingConvention.StdCall, SetLastError = true)]
	public static extern int SendMessage(IntPtr intptr_0, uint uint_0, int int_13, int int_14);

	public static void smethod_32(string string_17)
	{
	}

	public static void smethod_33(byte byte_2)
	{
		MessageBox.Show(Lang.PS("该版本不支持", "This Edition cannot supply") + byte_2 + Lang.PS(".x 文件", ".x files"));
	}

	public static bool smethod_35()
	{
		return sysLanguage_0 == SysLanguage.CN;
	}

	public static byte smethod_36()
	{
		if (byte_0 == 0)
		{
			string text = Assembly.GetExecutingAssembly().FullName.Split(',')[1];
			string[] array = text.Remove(0, text.IndexOf('=') + 1).Split('.');
			byte_0 = byte.Parse(array[0]);
			byte_1 = byte.Parse(array[1]);
		}
		return byte_0;
	}

	public static float SulfurTransformation(string strName, float amount)
	{
		float num = 0f;
		switch (strName)
		{
		case "硫化氢":
			num = amount / 34f * 32f;
			break;
		case "羰基硫":
			num = amount / 60f * 32f;
			break;
		case "甲硫醇":
			num = amount / 48f * 32f;
			break;
		case "乙硫醇":
			num = amount / 62f * 32f;
			break;
		case "甲硫醚":
			num = amount / 62f * 32f;
			break;
		case "二硫化碳":
			num = amount / 76f * 64f;
			break;
		case "乙硫醚":
			num = amount / 90f * 32f;
			break;
		case "噻吩":
			num = amount / 84f * 32f;
			break;
		case "二甲基二硫":
			num = amount / 94f * 64f;
			break;
		case "二氧化硫":
			num = amount / 64f * 32f;
			break;
		}
		if (num < 0f)
		{
			num = 0f;
		}
		return num;
	}

	public static string smethod_37()
	{
		return smethod_36() + "." + byte_1;
	}

	public static string GetStartPath()
	{
		FileInfo fileInfo = new FileInfo(Application.ExecutablePath);
		return fileInfo.Directory.ToString() + "\\";
	}

	public static string MesureUnit()
	{
		if (bool_4)
		{
			return "pA";
		}
		return "mV";
	}

	[DllImport("imm32.dll")]
	public static extern IntPtr ImmGetContext(IntPtr intptr_0);

	[DllImport("imm32.dll")]
	public static extern bool ImmGetOpenStatus(IntPtr intptr_0);

	[DllImport("imm32.dll")]
	public static extern bool ImmGetConversionStatus(IntPtr intptr_0, ref int int_13, ref int int_14);

	[DllImport("imm32.dll")]
	public static extern bool ImmReleaseContext(IntPtr intptr_0, IntPtr intptr_1);

	[DllImport("imm32.dll")]
	public static extern bool ImmSetConversionStatus(IntPtr intptr_0, int int_13, int int_14);

	public static void smethod_40(IntPtr intptr_0)
	{
		IntPtr intPtr = ImmGetContext(intptr_0);
		if (ImmGetOpenStatus(intPtr))
		{
			int int_ = 0;
			int int_2 = 0;
			if (ImmGetConversionStatus(intPtr, ref int_, ref int_2) && (int_ & 8) > 0)
			{
				int_ &= -9;
				ImmSetConversionStatus(intPtr, int_, int_2);
			}
		}
		ImmReleaseContext(intptr_0, intPtr);
	}

	public static string ConfigFilePath()
	{
		return Application.StartupPath + "\\Config.xml";
	}

	public static bool ConfigFileExist()
	{
		return File.Exists(ConfigFilePath());
	}

	public static string ReadConfigSection(string string_17)
	{
		if (!ConfigFileExist())
		{
			return null;
		}
		XmlDocument xmlDocument = new XmlDocument();
		string result;
		try
		{
			xmlDocument.Load(ConfigFilePath());
			result = ((xmlDocument["Sample"] == null || xmlDocument["Sample"][string_17] == null) ? null : xmlDocument["Sample"][string_17].InnerText);
		}
		catch (Exception ex)
		{
			result = "ERR:" + ex.Message;
		}
		return result;
	}

	public static bool Write2ConfigFile(string string_17)
	{
		XmlDocument xmlDocument = new XmlDocument();
		xmlDocument.Load(ConfigFilePath());
		if (xmlDocument["Sample"] != null)
		{
			if (xmlDocument["Sample"]["SampleName"] == null)
			{
				XmlNode xmlNode = xmlDocument.CreateNode(XmlNodeType.Element, "SampleName", "");
				xmlNode.InnerText = string_17;
				xmlDocument["Sample"].AppendChild(xmlNode);
			}
			else
			{
				xmlDocument["Sample"]["SampleName"].InnerText = string_17;
			}
		}
		xmlDocument.Save(ConfigFilePath());
		return true;
	}

	public static bool CreateDbBase()
	{
		DBBase dBBase = DBBase.Create();
		SystemParam systemParam = SystemParam.Create();
		if (systemParam.iDbConnectType == 0 || systemParam.iDbConnectType == 2)
		{
			return dBBase.CreateDbBaseAccess();
		}
		return dBBase.CreateDbBaseSqlLite();
	}

	public static bool InsertIntoTable(string strType, string strUserName, string strIntrumentName, string strMoudle, string strDesc)
	{
		if (DogFeturlMgr.LicencedGMP())
		{
			DBBase dBBase = DBBase.Create();
			SystemParam systemParam = SystemParam.Create();
			if (systemParam.iDbConnectType == 0 || systemParam.iDbConnectType == 2)
			{
				bool result = dBBase.InsertIntoTableAccess(strType, strUserName, strIntrumentName, strMoudle, strDesc);
				TryPublishAudit(strUserName, strIntrumentName, strMoudle, strDesc);
				return result;
			}
			bool result2 = dBBase.InsertIntoTableSqlLite(strType, strUserName, strIntrumentName, strMoudle, strDesc);
			TryPublishAudit(strUserName, strIntrumentName, strMoudle, strDesc);
			return result2;
		}
		if (DogFeturlMgr.LicencedGMP())
		{
			DBBase dBBase2 = DBBase.Create();
			SystemParam systemParam2 = SystemParam.Create();
			if (systemParam2.iDbConnectType == 0 || systemParam2.iDbConnectType == 2)
			{
				bool result3 = dBBase2.InsertIntoTableAccess(strType, strUserName, strIntrumentName, strMoudle, strDesc);
				TryPublishAudit(strUserName, strIntrumentName, strMoudle, strDesc);
				return result3;
			}
			bool result4 = dBBase2.InsertIntoTableSqlLite(strType, strUserName, strIntrumentName, strMoudle, strDesc);
			TryPublishAudit(strUserName, strIntrumentName, strMoudle, strDesc);
			return result4;
		}
		TryPublishAudit(strUserName, strIntrumentName, strMoudle, strDesc);
		return false;
	}

	private static void TryPublishAudit(string userName, string instrumentName, string module, string desc)
	{
		try
		{
			SystemParam sysParam = SystemParam.Create();
			if (!sysParam.bMqttEnable)
			{
				return;
			}
			MqttTelemetryService.Instance.EnqueueAudit(userName, instrumentName, module, desc);
		}
		catch
		{
		}
	}

	public static bool InsertIntoHistory(int ComponentID, int dexcotID, string strFileName, float fTHC, float fCh4)
	{
		DBBase dBBase = DBBase.Create();
		SystemParam systemParam = SystemParam.Create();
		if (systemParam.iDbConnectType == 0 || systemParam.iDbConnectType == 2)
		{
			return false;
		}
		return dBBase.InsertIntoHistorySqlLite(ComponentID, dexcotID, strFileName, fTHC, fCh4);
	}

	public static bool InsertIntoRLTHistory(int ComponentID, string site, string componentName, float data)
	{
		DBBase dBBase = DBBase.Create();
		SystemParam systemParam = SystemParam.Create();
		if (systemParam.iDbConnectType == 0 || systemParam.iDbConnectType == 2)
		{
			return false;
		}
		return dBBase.InsertIntoRLTHistorySqlLite(ComponentID, site, componentName, data);
	}

	public static DataTable GetDataTableRLTHistory(int ComponentID, string componentName, DateTime startTime, DateTime endtTime)
	{
		DBBase dBBase = DBBase.Create();
		SystemParam systemParam = SystemParam.Create();
		if (systemParam.iDbConnectType == 0 || systemParam.iDbConnectType == 2)
		{
			return dBBase.GetDataTableVocAccess(ComponentID, startTime, endtTime);
		}
		return dBBase.GetDataTableRLTHistorySqlLite(ComponentID, componentName, startTime, endtTime);
	}

	public static bool InsertIntoRZHistory(int ComponentID, int dexcotID, string strFileName, float[] amountComponent)
	{
		DBBase dBBase = DBBase.Create();
		SystemParam systemParam = SystemParam.Create();
		if (systemParam.iDbConnectType == 0 || systemParam.iDbConnectType == 2)
		{
			return false;
		}
		return dBBase.InsertIntoRZHistorySqlLite(ComponentID, dexcotID, strFileName, amountComponent);
	}

	public static DataTable GetDataTableRZHistory(int ComponentID, DateTime startTime, DateTime endtTime)
	{
		DBBase dBBase = DBBase.Create();
		SystemParam systemParam = SystemParam.Create();
		if (systemParam.iDbConnectType == 0 || systemParam.iDbConnectType == 2)
		{
			return dBBase.GetDataTableVocAccess(ComponentID, startTime, endtTime);
		}
		return dBBase.GetDataTableRZHistorySqlLite(ComponentID, startTime, endtTime);
	}

	public static int DeleteDataTableRZHistory(int ComponentID, DateTime startTime, DateTime endtTime)
	{
		DBBase dBBase = DBBase.Create();
		SystemParam systemParam = SystemParam.Create();
		if (systemParam.iDbConnectType == 0 || systemParam.iDbConnectType == 2)
		{
			return dBBase.DeleteDataTableVocAccess(ComponentID, startTime, endtTime);
		}
		return dBBase.DeleteDataTableRZHistorySqlLite(ComponentID, startTime, endtTime);
	}

	public static bool InsertIntoVoc(int ComponentID, int dexcotID, string strComponentName, string strFileName, float fAreaPer, float fArea = 0f, float floatRev0 = 0f, float floatRev1 = 0f, string strRev0 = "", string strRev1 = "")
	{
		DBBase dBBase = DBBase.Create();
		SystemParam systemParam = SystemParam.Create();
		if (systemParam.iDbConnectType == 0 || systemParam.iDbConnectType == 2)
		{
			return dBBase.InsertIntoVocAccess(ComponentID, dexcotID, strComponentName, strFileName, fAreaPer, fArea, floatRev0, floatRev1, strRev0, strRev1);
		}
		return dBBase.InsertIntoVocSqlLite(ComponentID, dexcotID, strComponentName, strFileName, fAreaPer, fArea, floatRev0, floatRev1, strRev0, strRev1);
	}

	public static bool InsertIntoVocTable(int ComponentID, int dexcotID, string strFileName, string[] zufenAmount)
	{
		DBBase dBBase = DBBase.Create();
		SystemParam systemParam = SystemParam.Create();
		if (systemParam.iDbConnectType == 0 || systemParam.iDbConnectType == 2)
		{
			return false;
		}
		return dBBase.InsertIntoVocTableSqlLite(ComponentID, dexcotID, strFileName, zufenAmount);
	}

	public static bool InsertIntoCoalTable(int ComponentID, int dexcotID, string[] zufenAmount, string strFileName, string strFileName2, string strFileName3)
	{
		DBBase dBBase = DBBase.Create();
		SystemParam systemParam = SystemParam.Create();
		if (systemParam.iDbConnectType == 0 || systemParam.iDbConnectType == 2)
		{
			return false;
		}
		return dBBase.InsertIntoCoalTableSqlLite(ComponentID, dexcotID, zufenAmount, strFileName, strFileName2, strFileName3);
	}

	public static bool InsertIntoRNVocTable(int ComponentID, int dexcotID, string[] zufenAmount)
	{
		DBBase dBBase = DBBase.Create();
		SystemParam systemParam = SystemParam.Create();
		if (systemParam.iDbConnectType == 0 || systemParam.iDbConnectType == 2)
		{
			return false;
		}
		return dBBase.InsertIntoRNVocTableSqlLite(ComponentID, dexcotID, zufenAmount);
	}

	public static bool InsertIntoRNNMHC(int ComponentID, int dexcotID, string[] zufenAmount)
	{
		DBBase dBBase = DBBase.Create();
		SystemParam systemParam = SystemParam.Create();
		if (systemParam.iDbConnectType == 0 || systemParam.iDbConnectType == 2)
		{
			return false;
		}
		return dBBase.InsertIntoRNNMHCTableSqlLite(ComponentID, dexcotID, zufenAmount);
	}

	public static bool InsertIntoRNBTEX(int ComponentID, int dexcotID, string[] zufenAmount)
	{
		DBBase dBBase = DBBase.Create();
		SystemParam systemParam = SystemParam.Create();
		if (systemParam.iDbConnectType == 0 || systemParam.iDbConnectType == 2)
		{
			return false;
		}
		return dBBase.InsertIntoRNBTEXTableSqlLite(ComponentID, dexcotID, zufenAmount);
	}

	public static DataTable GetDataTableVoc(int ComponentID, DateTime startTime, DateTime endtTime)
	{
		DBBase dBBase = DBBase.Create();
		SystemParam systemParam = SystemParam.Create();
		if (systemParam.iDbConnectType == 0 || systemParam.iDbConnectType == 2)
		{
			return dBBase.GetDataTableVocAccess(ComponentID, startTime, endtTime);
		}
		return dBBase.GetDataTableVocSqlLite(ComponentID, startTime, endtTime);
	}

	public static int DeleteDataTableVoc(int ComponentID, DateTime startTime, DateTime endtTime)
	{
		DBBase dBBase = DBBase.Create();
		SystemParam systemParam = SystemParam.Create();
		if (systemParam.iDbConnectType == 0 || systemParam.iDbConnectType == 2)
		{
			return dBBase.DeleteDataTableVocAccess(ComponentID, startTime, endtTime);
		}
		return dBBase.DeleteDataTableVocSqlLite(ComponentID, startTime, endtTime);
	}

	public static int DeleteDataTableVoc(int ComponentID, string fileName)
	{
		DBBase dBBase = DBBase.Create();
		SystemParam systemParam = SystemParam.Create();
		if (systemParam.iDbConnectType == 0 || systemParam.iDbConnectType == 2)
		{
			return dBBase.DeleteDataTableVocAccess(ComponentID, fileName);
		}
		return 0;
	}

	public static DataTable GetDataTable(string strSql)
	{
		DBBase dBBase = DBBase.Create();
		SystemParam systemParam = SystemParam.Create();
		if (systemParam.iDbConnectType == 0 || systemParam.iDbConnectType == 2)
		{
			return dBBase.GetDataTableAccess(strSql);
		}
		return dBBase.GetDataTableSqlLite(strSql);
	}

	public static DataTable GetDataTable(string strSql, string strDb)
	{
		DBBase dBBase = DBBase.Create();
		SystemParam systemParam = SystemParam.Create();
		if (systemParam.iDbConnectType == 0 || systemParam.iDbConnectType == 2)
		{
			return dBBase.GetDataTableAccess(strSql);
		}
		return dBBase.GetDataTableSqlLite(strSql, strDb);
	}

	public static bool InsertIntoLYTHC(int ComponentID, int dexcotID, string samplingSite, string strFileName, float thcAmount, float ch4Amount, float nmhcAmount = 0f, float floatRev1 = 0f, string strRev0 = "", string strRev1 = "")
	{
		DBBase dBBase = DBBase.Create();
		SystemParam systemParam = SystemParam.Create();
		if (systemParam.iDbConnectType == 0 || systemParam.iDbConnectType == 2)
		{
			return false;
		}
		return dBBase.InsertIntoLYThcSqlLite(ComponentID, dexcotID, samplingSite, strFileName, thcAmount, ch4Amount, nmhcAmount, floatRev1, strRev0, strRev1);
	}

	public static bool InsertIntoLYBTEX(int ComponentID, int dexcotID, string samplingSite, string strFileName, float[] btexAmount, float floatRev1 = 0f, string strRev0 = "", string strRev1 = "")
	{
		DBBase dBBase = DBBase.Create();
		SystemParam systemParam = SystemParam.Create();
		if (systemParam.iDbConnectType == 0 || systemParam.iDbConnectType == 2)
		{
			return false;
		}
		return dBBase.InsertIntoLYBtexSqlLite(ComponentID, dexcotID, samplingSite, strFileName, btexAmount, floatRev1, strRev0, strRev1);
	}

	public static bool InsertIntoLYTHCRLT(int ComponentID, int dexcotID, float thcAmount, float ch4Amount, float nmhcAmount = 0f)
	{
		DBBase dBBase = DBBase.Create();
		SystemParam systemParam = SystemParam.Create();
		if (systemParam.iDbConnectType == 0 || systemParam.iDbConnectType == 2)
		{
			return false;
		}
		return dBBase.InsertIntoLYThcRLTSqlLite(ComponentID, dexcotID, thcAmount, ch4Amount, nmhcAmount);
	}

	public static bool InsertIntoLYTHCRLT(int ComponentID, int dexcotID, string strDB, float thcAmount, float ch4Amount, float nmhcAmount = 0f)
	{
		try
		{
			DBBase dBBase = DBBase.Create();
			SystemParam systemParam = SystemParam.Create();
			if (systemParam.iDbConnectType == 0 || systemParam.iDbConnectType == 2)
			{
				return false;
			}
			return dBBase.InsertIntoLYThcRLTSqlLite(ComponentID, dexcotID, strDB, thcAmount, ch4Amount, nmhcAmount);
		}
		catch
		{
			return false;
		}
	}

	public static bool InsertIntoLYBTENRLT(int ComponentID, int dexcotID, float[] fBtexAmount)
	{
		DBBase dBBase = DBBase.Create();
		SystemParam systemParam = SystemParam.Create();
		if (systemParam.iDbConnectType == 0 || systemParam.iDbConnectType == 2)
		{
			return false;
		}
		return dBBase.InsertIntoLYBTEXRLTSqlLite(ComponentID, dexcotID, fBtexAmount);
	}

	public static DataTable GetDataTableLYThc(int ComponentID, DateTime startTime, DateTime endtTime)
	{
		DBBase dBBase = DBBase.Create();
		SystemParam systemParam = SystemParam.Create();
		if (systemParam.iDbConnectType == 0 || systemParam.iDbConnectType == 2)
		{
			return null;
		}
		return dBBase.GetDataTablelythcSqlLite(ComponentID, startTime, endtTime);
	}

	public static int DeleteDataTableLYThc(int ComponentID, DateTime startTime, DateTime endtTime)
	{
		DBBase dBBase = DBBase.Create();
		SystemParam systemParam = SystemParam.Create();
		if (systemParam.iDbConnectType == 0 || systemParam.iDbConnectType == 2)
		{
			return 0;
		}
		return dBBase.DeleteDataTableLYThcSqlLite(ComponentID, startTime, endtTime);
	}

	public static int DeleteDataTableLYThcRLTAll()
	{
		DBBase dBBase = DBBase.Create();
		SystemParam systemParam = SystemParam.Create();
		if (systemParam.iDbConnectType == 0 || systemParam.iDbConnectType == 2)
		{
			return 0;
		}
		return dBBase.DeleteDataTableLYThcRLTSqlLiteAll();
	}

	public static bool updateIntoMineSqlLite2(int ComponentID, string strName, string fileName1, string[] peakName, string[] zufenAmount)
	{
		DBBase dBBase = DBBase.Create();
		SystemParam systemParam = SystemParam.Create();
		if (systemParam.iDbConnectType == 0 || systemParam.iDbConnectType == 2)
		{
			return false;
		}
		return dBBase.updateIntoMineSqlLite2(ComponentID, strName, fileName1, peakName, zufenAmount);
	}

	public static int DeleteDataTable(string name, DateTime startTime, DateTime endtTime)
	{
		DBBase dBBase = DBBase.Create();
		SystemParam systemParam = SystemParam.Create();
		if (systemParam.iDbConnectType == 0 || systemParam.iDbConnectType == 2)
		{
			return 0;
		}
		return dBBase.DeleteDataTableSqlLite(name, startTime, endtTime);
	}

	public static bool DeleteDataTable(string name)
	{
		DBBase dBBase = DBBase.Create();
		SystemParam systemParam = SystemParam.Create();
		if (systemParam.iDbConnectType == 0 || systemParam.iDbConnectType == 2)
		{
			return false;
		}
		return dBBase.DeleteDataTableSqlLite(name);
	}

	public static bool DeleteDataTable(int id, string name)
	{
		DBBase dBBase = DBBase.Create();
		SystemParam systemParam = SystemParam.Create();
		if (systemParam.iDbConnectType == 0 || systemParam.iDbConnectType == 2)
		{
			return false;
		}
		return dBBase.DeleteDataTableSqlLite(id, name);
	}

	public static bool InsertIntoMine(int ComponentID, int dexcotID, string[] zufenName, string[] zufenAmount, string strJiedian)
	{
		DBBase dBBase = DBBase.Create();
		SystemParam systemParam = SystemParam.Create();
		if (systemParam.iDbConnectType == 0 || systemParam.iDbConnectType == 2)
		{
			return false;
		}
		return dBBase.InsertIntoMineSqlLite(ComponentID, dexcotID, zufenName, zufenAmount, strJiedian);
	}

	public static bool InsertIntoHISDATASql(int ComponentID, string strTime, string strSite, string method, string interval, string fileName, string[] zufenAmount)
	{
		DBBase dBBase = DBBase.Create();
		SystemParam systemParam = SystemParam.Create();
		if (systemParam.iDbConnectType == 0 || systemParam.iDbConnectType == 2)
		{
			return false;
		}
		return dBBase.InsertIntoHISDATASql(ComponentID, strTime, strSite, method, interval, fileName, zufenAmount);
	}

	public static bool InsertIntoPortableSql(int ComponentID, string strTime, string strSite, string fileName, string[] zufenAmount)
	{
		DBBase dBBase = DBBase.Create();
		SystemParam systemParam = SystemParam.Create();
		if (systemParam.iDbConnectType == 0 || systemParam.iDbConnectType == 2)
		{
			return false;
		}
		return dBBase.InsertIntoPortableSql(ComponentID, strTime, strSite, fileName, zufenAmount);
	}

	public static bool InsertIntoMine(int ComponentID, string strName, int dexcotID, string[] zufenName, string[] zufenAmount, string strJiedian)
	{
		DBBase dBBase = DBBase.Create();
		SystemParam systemParam = SystemParam.Create();
		if (systemParam.iDbConnectType == 0 || systemParam.iDbConnectType == 2)
		{
			return false;
		}
		return dBBase.InsertIntoMineSqlLite(ComponentID, strName, dexcotID, zufenName, zufenAmount, strJiedian);
	}

	public static bool InsertIntoMine(int ComponentID, string strSite, string[] zufenAmountn)
	{
		DBBase dBBase = DBBase.Create();
		SystemParam systemParam = SystemParam.Create();
		if (systemParam.iDbConnectType == 0 || systemParam.iDbConnectType == 2)
		{
			return false;
		}
		return dBBase.InsertIntoMineSqlLite(ComponentID, strSite, zufenAmountn);
	}

	public static DataTable GetDataTableMINE(int ComponentID, string strName, DateTime startTime, DateTime endtTime)
	{
		DBBase dBBase = DBBase.Create();
		SystemParam systemParam = SystemParam.Create();
		if (systemParam.iDbConnectType == 0 || systemParam.iDbConnectType == 2)
		{
			return null;
		}
		return dBBase.GetDataTableMineSqlLite(ComponentID, strName, startTime, endtTime);
	}

	public static DataTable GetDataTableRow(int ComponentID, string strName, DateTime startTime, string strTime)
	{
		DBBase dBBase = DBBase.Create();
		SystemParam systemParam = SystemParam.Create();
		if (systemParam.iDbConnectType == 0 || systemParam.iDbConnectType == 2)
		{
			return null;
		}
		return dBBase.GetDataTableRowSqlLite(ComponentID, strName, startTime, strTime);
	}

	public static string smethod_13()
	{
		string text = ((edition_0 == Edition.Clarify) ? Lang.PS("色谱数据系统", "Chromatography Data System") : Lang.PS("色谱工作站", "Chrom. Station"));
		return edition_0.ToString() + " " + text;
	}
}
