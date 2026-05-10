using Microsoft.Win32;

namespace IBrainChrom2018;

public class FileTypeRegister
{
	private static string GetFileTypeKeyName(string extendName)
	{
		return extendName.Substring(1, extendName.Length - 1).ToUpper() + "_FileType";
	}

	private static RegistryKey OpenSubKeyPreferCurrentUser(string subKey, bool writable)
	{
		RegistryKey registryKey = Registry.CurrentUser.OpenSubKey("Software\\Classes\\" + subKey, writable);
		return registryKey ?? Registry.ClassesRoot.OpenSubKey(subKey, writable);
	}

	private static bool TryRegisterFileType(RegistryKey root, string prefix, FileTypeRegInfo regInfo, string fileTypeKeyName)
	{
		try
		{
			string subKey = (prefix == null) ? regInfo.ExtendName : (prefix + "\\" + regInfo.ExtendName);
			using RegistryKey registryKey = root.CreateSubKey(subKey);
			registryKey.SetValue("", fileTypeKeyName);

			string subKey2 = (prefix == null) ? fileTypeKeyName : (prefix + "\\" + fileTypeKeyName);
			using RegistryKey registryKey2 = root.CreateSubKey(subKey2);
			registryKey2.SetValue("", regInfo.Description);

			using RegistryKey registryKey3 = registryKey2.CreateSubKey("DefaultIcon");
			registryKey3.SetValue("", regInfo.IcoPath);

			using RegistryKey registryKey4 = registryKey2.CreateSubKey("Shell");
			using RegistryKey registryKey5 = registryKey4.CreateSubKey("Open");
			using RegistryKey registryKey6 = registryKey5.CreateSubKey("Command");
			registryKey6.SetValue("", regInfo.ExePath + " %1");
			return true;
		}
		catch
		{
			return false;
		}
	}

	public void RegisterFileType(FileTypeRegInfo regInfo)
	{
		if (regInfo == null)
		{
			return;
		}
		if (FileTypeRegistered(regInfo.ExtendName))
		{
			return;
		}
		string fileTypeKeyName = GetFileTypeKeyName(regInfo.ExtendName);
		if (!TryRegisterFileType(Registry.CurrentUser, "Software\\Classes", regInfo, fileTypeKeyName))
		{
			TryRegisterFileType(Registry.ClassesRoot, null, regInfo, fileTypeKeyName);
		}
	}

	public FileTypeRegInfo GetFileTypeRegInfo(string extendName)
	{
		if (!FileTypeRegistered(extendName))
		{
			return null;
		}
		FileTypeRegInfo fileTypeRegInfo = new FileTypeRegInfo(extendName);
		string fileTypeKeyName = GetFileTypeKeyName(extendName);
		using RegistryKey registryKey = OpenSubKeyPreferCurrentUser(fileTypeKeyName, writable: false);
		fileTypeRegInfo.Description = registryKey.GetValue("").ToString();
		using RegistryKey registryKey2 = registryKey.OpenSubKey("DefaultIcon");
		fileTypeRegInfo.IcoPath = registryKey2.GetValue("").ToString();
		using RegistryKey registryKey3 = registryKey.OpenSubKey("Shell");
		using RegistryKey registryKey4 = registryKey3.OpenSubKey("Open");
		using RegistryKey registryKey5 = registryKey4.OpenSubKey("Command");
		string text = registryKey5.GetValue("").ToString();
		fileTypeRegInfo.ExePath = text.Substring(0, text.Length - 3);
		return fileTypeRegInfo;
	}

	public bool UpdateFileTypeRegInfo(FileTypeRegInfo regInfo)
	{
		string extendName = regInfo.ExtendName;
		string fileTypeKeyName = GetFileTypeKeyName(extendName);
		using RegistryKey registryKey = OpenSubKeyPreferCurrentUser(fileTypeKeyName, writable: true);
		if (registryKey == null)
		{
			return false;
		}
		registryKey.SetValue("", regInfo.Description);
		using RegistryKey registryKey2 = registryKey.OpenSubKey("DefaultIcon", writable: true);
		registryKey2.SetValue("", regInfo.IcoPath);
		using RegistryKey registryKey3 = registryKey.OpenSubKey("Shell");
		using RegistryKey registryKey4 = registryKey3.OpenSubKey("Open");
		using RegistryKey registryKey5 = registryKey4.OpenSubKey("Command", writable: true);
		registryKey5.SetValue("", regInfo.ExePath + " %1");
		return true;
	}

	public bool FileTypeRegistered(string extendName)
	{
		if (Registry.CurrentUser.OpenSubKey("Software\\Classes\\" + extendName) != null)
		{
			return true;
		}
		return Registry.ClassesRoot.OpenSubKey(extendName) != null;
	}
}
