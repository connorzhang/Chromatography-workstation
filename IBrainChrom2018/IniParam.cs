using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;

namespace IBrainChrom2018;

public class IniParam
{
	private static IniParam myparam = null;

	private string strFilePath;

	private string strSec = "";

	public string[] strGasNAME = new string[24];

	public bool bSul = true;

	[DllImport("kernel32")]
	private static extern long WritePrivateProfileString(string section, string key, string val, string filepath);

	[DllImport("kernel32")]
	private static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retval, int size, string filePath);

	public IniParam(string iniFilePath)
	{
		strFilePath = iniFilePath;
	}

	public void LoadParam()
	{
		if (File.Exists(strFilePath))
		{
			strSec = Path.GetFileNameWithoutExtension(strFilePath);
			StringBuilder stringBuilder = new StringBuilder(256);
			GetPrivateProfileString("Sul", "method", "true", stringBuilder, 256, strFilePath);
			bSul = Convert.ToBoolean(stringBuilder.ToString());
			for (int i = 0; i < 24; i++)
			{
				GetPrivateProfileString("Sul", "strGasNAME" + i, " ", stringBuilder, 256, strFilePath);
				strGasNAME[i] = stringBuilder.ToString();
			}
		}
	}

	public void SaveParam()
	{
		try
		{
			WritePrivateProfileString("Sul", "method", bSul.ToString(), strFilePath);
			for (int i = 0; i < 24; i++)
			{
				WritePrivateProfileString("Sul", "strGasNAME" + i, strGasNAME[i], strFilePath);
			}
		}
		catch (Exception)
		{
		}
	}
}
