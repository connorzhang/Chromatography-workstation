using System;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.IO;
using System.Net;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;
using IBrainChrom2018.Unit;

namespace IBrainChrom2018;

internal static class Common
{
	public const int bigEdge = 15;

	public const string comDir = "Common\\";

	public const string dftTimeUnit = "min";

	public const string dllName = "Station\\SysConfig\\Station.dll";

	public const int edge = 10;

	public const int itvCtrl = 3;

	public const int MaxSignalsNum = 12;

	public const int off_TbBtn = -1;

	public const int off_TbLb = 4;

	public const string pdaDlgMark = "PDA";

	public const string pdaLibsExt = ".lib";

	public const int smallEdge = 5;

	public const string strPID = "DF874EDF";

	public const string sysDir = "Station\\";

	public const string usersDir = "Users\\";

	private static Color[] clrInstrus = new Color[4]
	{
		Color.DarkGray,
		Color.DarkBlue,
		Color.DarkGreen,
		Color.White
	};

	public static bool IsQYVerSion = false;

	public static bool IsDebug = false;

	public static bool grpShowGrid = false;

	public static bool BrushTempUpgrate = false;

	public static int PointCount = 4;

	public static string LoginUserName = "";

	public static string ComPort = "COM1";

	public static string dftAreaUnit = yUnit + ".s";

	public static string dftSignalUnit = yUnit;

	private static byte major = 0;

	public static byte Minor = 0;

	public static Random random = new Random((int)DateTime.Now.Ticks);

	public static bool useAu = true;

	public static int PrintExe = 0;

	public static byte Major
	{
		get
		{
			if (major == 0)
			{
				string text = Assembly.GetExecutingAssembly().FullName.Split(',')[1];
				string[] array = text.Remove(0, text.IndexOf('=') + 1).Split('.');
				major = byte.Parse(array[0]);
				Minor = byte.Parse(array[1]);
			}
			return major;
		}
	}

	public static string VersionStr => Major + "." + Minor;

	public static string WorkDir
	{
		get
		{
			FileInfo fileInfo = new FileInfo(Application.ExecutablePath);
			return fileInfo.Directory.ToString() + "\\";
		}
	}

	public static string yUnit
	{
		get
		{
			if (useAu)
			{
				return "mV";
			}
			return "mV";
		}
	}

	public static void AddArrVal(ref int[] array, int value)
	{
		Array.Resize(ref array, array.Length + 1);
		array[array.Length - 1] = value;
	}

	public static void AddArrVal(ref float[] array, float value)
	{
		Array.Resize(ref array, array.Length + 1);
		array[array.Length - 1] = value;
	}

	public static void AddArrVal(ref string[] array, string value, bool unique)
	{
		bool flag = false;
		for (int i = 0; i < array.Length; i++)
		{
			if (array[i] == value)
			{
				flag = true;
				break;
			}
		}
		if (!unique || !flag)
		{
			int num = array.Length;
			Array.Resize(ref array, num + 1);
			array[num] = value;
		}
	}

	public static string ByteArrayToString(byte[] arrInput)
	{
		if (arrInput == null)
		{
			return "null";
		}
		StringBuilder stringBuilder = new StringBuilder(arrInput.Length);
		for (int i = 0; i < arrInput.Length; i++)
		{
			stringBuilder.Append(" " + arrInput[i].ToString("X2"));
		}
		return stringBuilder.ToString();
	}

	public static bool ChkDog()
	{
		return true;
	}

	public static void CloseFile(ref FileStream fs, ref BinaryReader br)
	{
		if (br != null)
		{
			br.Close();
		}
		if (fs != null)
		{
			fs.Close();
		}
	}

	public static void CloseFile(ref FileStream fs, ref BinaryWriter bw)
	{
		if (bw != null)
		{
			bw.Flush();
		}
		if (fs != null)
		{
			fs.Flush();
		}
		if (bw != null)
		{
			bw.Close();
		}
		if (fs != null)
		{
			fs.Close();
		}
	}

	public static void Confine(ref double val, double minVal, double maxVal)
	{
		if (val < minVal)
		{
			val = minVal;
		}
		if (val > maxVal)
		{
			val = maxVal;
		}
	}

	public static void Confine(ref int val, int minVal, int maxVal)
	{
		if (val < minVal)
		{
			val = minVal;
		}
		if (val > maxVal)
		{
			val = maxVal;
		}
	}

	public static void Confine(ref float val, float minVal, float maxVal)
	{
		if (val < minVal)
		{
			val = minVal;
		}
		if (val > maxVal)
		{
			val = maxVal;
		}
	}

	public static bool Contains(int[] ii, int i)
	{
		for (int j = 0; j < ii.Length; j++)
		{
			if (ii[j] == i)
			{
				return true;
			}
		}
		return false;
	}

	public static bool Contains(string[] strs, string str)
	{
		for (int i = 0; i < strs.Length; i++)
		{
			if (strs[i] == str)
			{
				return true;
			}
		}
		return false;
	}

	public static void CopySubfoldersAndFiles(string sourceFolder, string targetFolder)
	{
		try
		{
			if (!Directory.Exists(targetFolder))
			{
				Directory.CreateDirectory(targetFolder);
			}
			DirectoryInfo directoryInfo = new DirectoryInfo(sourceFolder);
			FileSystemInfo[] fileSystemInfos = directoryInfo.GetFileSystemInfos();
			FileSystemInfo[] array = fileSystemInfos;
			foreach (FileSystemInfo fileSystemInfo in array)
			{
				string text = Path.Combine(targetFolder, fileSystemInfo.Name);
				if (fileSystemInfo is FileInfo)
				{
					File.Copy(fileSystemInfo.FullName, text);
					continue;
				}
				Directory.CreateDirectory(text);
				CopySubfoldersAndFiles(fileSystemInfo.FullName, text);
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message);
		}
	}

	public static float FloatParse(object _object, float default_value)
	{
		if (!(_object is float result))
		{
			if (_object != null)
			{
				string s = _object.ToString().Trim();
				float result2 = 0f;
				if (float.TryParse(s, out result2))
				{
					return result2;
				}
			}
			return default_value;
		}
		return result;
	}

	public static string GetDlgFilter(string fileExt)
	{
		return "(*" + fileExt + ")|*" + fileExt;
	}

	public static byte[] HS(string fileName)
	{
		FileStream fileStream = new FileInfo(fileName).Open(FileMode.Open);
		byte[] result = new MD5CryptoServiceProvider().ComputeHash(fileStream);
		fileStream.Close();
		return result;
	}

	public static int IntParse(object _object, int default_value)
	{
		if (!(_object is int result))
		{
			if (_object != null)
			{
				string s = _object.ToString().Trim();
				int result2 = 0;
				if (int.TryParse(s, out result2))
				{
					return result2;
				}
			}
			return default_value;
		}
		return result;
	}

	public static bool IsEqual(byte[] bytes1, byte[] bytes2)
	{
		if (bytes1.Length != bytes2.Length)
		{
			return false;
		}
		int i;
		for (i = 0; i < bytes1.Length && bytes1[i] == bytes2[i]; i++)
		{
		}
		return i == bytes1.Length;
	}

	public static void LoadFromFile(string fileName, out FileInfo fi, out FileStream fs, out BinaryReader br)
	{
		fi = new FileInfo(fileName);
		fs = fi.Open(FileMode.Open);
		br = new BinaryReader(fs);
	}

	public static float Max(float[] fs)
	{
		if (fs == null || fs.Length == 0)
		{
			throw new Exception("数组空!");
		}
		float num = float.MinValue;
		for (int i = 0; i < fs.Length; i++)
		{
			num = Math.Max(num, fs[i]);
		}
		return num;
	}

	public static float Min(float[] fs)
	{
		if (fs == null || fs.Length == 0)
		{
			throw new Exception("数组空!");
		}
		float num = float.MaxValue;
		for (int i = 0; i < fs.Length; i++)
		{
			num = Math.Min(num, fs[i]);
		}
		return num;
	}

	public static bool ParseFileName(string fullName, out string directoy, out string name, out string ext)
	{
		directoy = (name = (ext = ""));
		try
		{
			FileInfo fileInfo = new FileInfo(fullName);
			if (fileInfo.Exists)
			{
				directoy = fileInfo.DirectoryName;
				name = fileInfo.Name.Remove(fileInfo.Name.Length - fileInfo.Extension.Length);
				ext = fileInfo.Extension;
			}
			return fileInfo.Exists;
		}
		catch
		{
			return false;
		}
	}

	public static byte ReadMajor(BinaryReader br)
	{
		return br.ReadByte();
	}

	public static Color SetInstruColor(int pageNo, Color C)
	{
		if (0 <= pageNo && pageNo < clrInstrus.Length)
		{
			clrInstrus[pageNo] = C;
		}
		return C;
	}

	public static Color retInstruColor(int pageNo)
	{
		if (0 <= pageNo && pageNo < clrInstrus.Length)
		{
			return clrInstrus[pageNo];
		}
		return Color.Black;
	}

	public static void setInstruColor(int pageNo, Color C)
	{
		if (0 <= pageNo && pageNo < clrInstrus.Length)
		{
			clrInstrus[pageNo] = C;
		}
	}

	public static void SaveToFile(string fileName, out FileInfo fi, out FileStream fs, out BinaryWriter bw)
	{
		fi = new FileInfo(fileName);
		if (!fi.Directory.Exists)
		{
			fi.Directory.Create();
		}
		fs = fi.Open(FileMode.OpenOrCreate);
		bw = new BinaryWriter(fs);
	}

	[DllImport("user32.dll", CallingConvention = CallingConvention.StdCall, SetLastError = true)]
	public static extern int SendMessage(IntPtr hwnd, uint Msg, int wParam, int lParam);

	public static void ShowHelp(string key)
	{
	}

	public static void WriteMajor(BinaryWriter bw)
	{
		bw.Write(Major);
	}

	public static bool CreateDb()
	{
		string text = Application.StartupPath + "\\ngmpol.dll";
		try
		{
			if (!File.Exists(text))
			{
				SQLiteConnection.CreateFile("MyDatabase.sqlite");
				SQLiteConnection sQLiteConnection = new SQLiteConnection("Data Source=" + text);
				sQLiteConnection.Open();
				string text2 = " ";
				text2 += " CREATE TABLE [OLog] ( ";
				text2 += "[DateTime] DATETIME NOT NULL ON CONFLICT FAIL,  ";
				text2 += "[Type] CHAR NOT NULL ON CONFLICT FAIL,  ";
				text2 += "[NetChromUserName] CHAR NOT NULL ON CONFLICT FAIL,  ";
				text2 += "[IntrumentName] CHAR NOT NULL ON CONFLICT FAIL,  ";
				text2 += "[Moudle] CHAR NOT NULL ON CONFLICT FAIL,  ";
				text2 += "[Describe] TEXT NOT NULL ON CONFLICT FAIL,  ";
				text2 += " [ComputerUsername] CHAR NOT NULL ON CONFLICT FAIL,  ";
				text2 += "[ComputerName] CHAR NOT NULL ON CONFLICT FAIL,  ";
				text2 += " [VerInfo] TEXT NOT NULL ON CONFLICT FAIL); ";
				SQLiteCommand sQLiteCommand = new SQLiteCommand(text2, sQLiteConnection);
				sQLiteCommand.ExecuteNonQuery();
				sQLiteConnection.Close();
			}
		}
		catch (Exception)
		{
			return false;
		}
		return true;
	}

	public static bool AddLog2Db(string Type, string NetChromUserName, string IntrumentName, string Moudle, string Describe)
	{
		string text = string.Format("Version {0} ", "VER.16.10.22.01");
		string hostName = Dns.GetHostName();
		string userName = Environment.UserName;
		string text2 = Application.StartupPath + "\\ngmpol.dll";
		try
		{
			if (File.Exists(text2))
			{
				SQLiteConnection sQLiteConnection = new SQLiteConnection("Data Source=" + text2);
				sQLiteConnection.Open();
				string text3 = " ";
				text3 += " INSERT INTO [OLog] ( [DateTime] ,[Type]  ,[NetChromUserName]  ,[IntrumentName] ,[Moudle]  ,     ";
				text3 += "[Describe] ,[ComputerUsername]  ,[ComputerName]  ,[VerInfo] )   ";
				text3 = text3 + " values( datetime('now','localtime'),'" + Type;
				string text4 = text3;
				text3 = text4 + "','" + NetChromUserName + "','" + IntrumentName;
				string text5 = text3;
				text3 = text5 + "','" + Moudle + "','" + Describe;
				string text6 = text3;
				text3 = text6 + "','" + userName + "','" + hostName + "','" + text;
				text3 += " ') ";
				SQLiteCommand sQLiteCommand = new SQLiteCommand(text3, sQLiteConnection);
				sQLiteCommand.ExecuteNonQuery();
				sQLiteConnection.Close();
			}
			try
			{
				SystemParam systemParam = SystemParam.Create();
				if (systemParam.bMqttEnable)
				{
					MqttTelemetryService.Instance.EnqueueAudit(NetChromUserName, IntrumentName, Moudle, Describe);
				}
			}
			catch
			{
			}
		}
		catch (Exception)
		{
			return false;
		}
		return true;
	}

	public static DataTable GetDataFromDb(string StrSQL)
	{
		DataTable dataTable = new DataTable();
		string text = Application.StartupPath + "\\ngmpol.dll";
		try
		{
			if (!File.Exists(text))
			{
				return dataTable;
			}
			SQLiteConnection sQLiteConnection = new SQLiteConnection("Data Source=" + text);
			sQLiteConnection.Open();
			SQLiteCommand cmd = new SQLiteCommand(StrSQL, sQLiteConnection);
			SQLiteDataAdapter sQLiteDataAdapter = new SQLiteDataAdapter(cmd);
			sQLiteDataAdapter.Fill(dataTable);
			sQLiteConnection.Close();
			return dataTable;
		}
		catch (Exception)
		{
			return null;
		}
	}
}
