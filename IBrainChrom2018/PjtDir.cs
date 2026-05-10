using System;
using System.IO;
using System.Windows.Forms;

namespace IBrainChrom2018;

public class PjtDir
{
	public const string analysis = "Data";

	public const string calibration = "Calib";

	private const string string_0 = "请检查工程名！";

	private const string string_1 = "Please check Project Name!";

	private string string_2 = "";

	private string string_3 = "";

	public string instruDir = "";

	public string projectName = "";

	public string AlyDirectory => string_2;

	public string CalibDirectory => string_3;

	public string PjtFullName => instruDir + projectName;

	private string sError => Class49.sysLanguage_0 switch
	{
		SysLanguage.CN => "请检查工程名！", 
		SysLanguage.EN => "Please check Project Name!", 
		_ => "", 
	};

	public PjtDir(string instruDir, string projectName)
	{
		this.instruDir = instruDir;
		this.projectName = projectName;
	}

	public void CreateDirectories()
	{
		try
		{
			if (projectName == "")
			{
				throw new Exception(sError);
			}
			string text = instruDir + projectName + "\\";
			DirectoryInfo directoryInfo = new DirectoryInfo(text + "Calib");
			if (!directoryInfo.Exists)
			{
				directoryInfo.Create();
			}
			string_3 = directoryInfo.FullName;
			directoryInfo = new DirectoryInfo(text + "Data");
			if (!directoryInfo.Exists)
			{
				directoryInfo.Create();
			}
			string_2 = directoryInfo.FullName;
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message);
		}
	}
}
