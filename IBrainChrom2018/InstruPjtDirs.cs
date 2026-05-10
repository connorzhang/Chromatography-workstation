using System;
using System.Windows.Forms;

namespace IBrainChrom2018;

public class InstruPjtDirs
{
	private const string string_0 = "工程已存在！";

	private const string string_1 = "Project has exists!";

	public PjtDir[] pjtDirs = new PjtDir[0];

	private string sPjtExists => Class49.sysLanguage_0 switch
	{
		SysLanguage.CN => "工程已存在！", 
		SysLanguage.EN => "Project has exists!", 
		_ => "", 
	};

	private bool method_0(string string_2, string string_3)
	{
		string text = string_2 + string_3;
		for (int i = 0; i < pjtDirs.Length; i++)
		{
			if (pjtDirs[i].PjtFullName.ToLower() == text.ToLower())
			{
				MessageBox.Show(sPjtExists);
				return true;
			}
		}
		return false;
	}

	public PjtDir NewPjtDir(string instruDir, string projectName)
	{
		if (!(projectName == "") && !method_0(instruDir, projectName))
		{
			Array.Resize(ref pjtDirs, pjtDirs.Length + 1);
			pjtDirs[pjtDirs.Length - 1] = new PjtDir(instruDir, projectName);
			pjtDirs[pjtDirs.Length - 1].CreateDirectories();
			return pjtDirs[pjtDirs.Length - 1];
		}
		return null;
	}
}
