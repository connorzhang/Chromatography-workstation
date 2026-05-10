using System;
using Microsoft.Win32;

namespace IBrainChrom2018;

public class RegistryHelper
{
	private string string_0 = string.Empty;

	private RegistryKey registryKey_0 = Registry.CurrentUser;

	public RegistryHelper()
	{
	}

	public RegistryHelper(string softwareKey)
		: this(softwareKey, Registry.CurrentUser)
	{
	}

	public RegistryHelper(string softwareKey, RegistryKey rootRegistry)
	{
		string_0 = softwareKey;
		registryKey_0 = rootRegistry;
	}

	public bool FileTypeRegistered(string string_1)
	{
		if (string_1 == null)
		{
			throw new ArgumentNullException("key");
		}
		string text = string.Empty;
		try
		{
			RegistryKey registryKey = registryKey_0.OpenSubKey(string_0);
			text = registryKey.GetValue(string_1).ToString();
		}
		catch
		{
		}
		return !(text == string.Empty);
	}

	public string GetValue(string string_1)
	{
		if (string_1 == null)
		{
			throw new ArgumentNullException("key");
		}
		string result = string.Empty;
		try
		{
			RegistryKey registryKey = registryKey_0.OpenSubKey(string_0);
			result = registryKey.GetValue(string_1).ToString();
		}
		catch
		{
		}
		return result;
	}

	public bool SaveValue(string string_1, string value)
	{
		if (string_1 == null)
		{
			throw new ArgumentNullException("key");
		}
		if (value == null)
		{
			throw new ArgumentNullException("value");
		}
		RegistryKey registryKey = registryKey_0.OpenSubKey(string_0, writable: true);
		if (registryKey == null)
		{
			registryKey = registryKey_0.CreateSubKey(string_0);
		}
		registryKey.SetValue(string_1, value);
		return true;
	}
}
