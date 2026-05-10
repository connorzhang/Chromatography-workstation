using System;
using System.IO;
using System.Windows.Forms;

namespace IBrainChrom2018;

public struct Instrus_ATDirs
{
	public string[] dirInstrus;

	public string dirAuditTrail;

	public void DefaultDirections()
	{
		if (dirInstrus == null)
		{
			dirInstrus = new string[SysCfgDlg.sysConfig.pageInstrus.Length];
		}
		string text = ResourceImageLoad.ExePath() + "Projects\\";
		for (int i = 0; i < dirInstrus.Length; i++)
		{
			dirInstrus[i] = text;
		}
		dirAuditTrail = ResourceImageLoad.ExePath() + "Logs\\";
	}

	public bool CreateDirectories()
	{
		bool result;
		try
		{
			DirectoryInfo directoryInfo;
			for (int i = 0; i < dirInstrus.Length; i++)
			{
				directoryInfo = new DirectoryInfo(dirInstrus[i]);
				if (!directoryInfo.Exists)
				{
					directoryInfo.Create();
				}
				dirInstrus[i] = directoryInfo.FullName;
			}
			directoryInfo = new DirectoryInfo(dirAuditTrail);
			if (!directoryInfo.Exists)
			{
				directoryInfo.Create();
			}
			dirAuditTrail = directoryInfo.FullName;
			return true;
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message);
			result = false;
		}
		return result;
	}

	public void LoadFromObject(Instrus_ATDirs instrus_ATDirs)
	{
		Array.Resize(ref dirInstrus, instrus_ATDirs.dirInstrus.Length);
		for (int i = 0; i < dirInstrus.Length; i++)
		{
			dirInstrus[i] = instrus_ATDirs.dirInstrus[i];
		}
		dirAuditTrail = instrus_ATDirs.dirAuditTrail;
	}

	public void SaveToFile(BinaryWriter binaryWriter_0)
	{
		binaryWriter_0.Write(Class49.smethod_36());
		binaryWriter_0.Write(dirInstrus.Length);
		for (int i = 0; i < dirInstrus.Length; i++)
		{
			binaryWriter_0.Write(dirInstrus[i]);
		}
		binaryWriter_0.Write(dirAuditTrail);
	}

	public void LoadFromFile(BinaryReader binaryReader_0)
	{
		byte b = binaryReader_0.ReadByte();
		if (b == 1)
		{
			Array.Resize(ref dirInstrus, binaryReader_0.ReadInt32());
			for (int i = 0; i < dirInstrus.Length; i++)
			{
				dirInstrus[i] = binaryReader_0.ReadString();
			}
			dirAuditTrail = binaryReader_0.ReadString();
		}
		else
		{
			Class49.smethod_33(b);
		}
	}
}
