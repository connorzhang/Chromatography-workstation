using System;
using System.Management;
using System.Security.Cryptography;
using System.Text;
using Microsoft.Win32;

namespace IBrainChrom2018;

public static class RegisterClass
{
	public static int[] intCode = new int[127];

	public static int[] intNumber = new int[25];

	public static char[] Charcode = new char[25];

	public static string getCpu()
	{
		string result = null;
		ManagementClass managementClass = new ManagementClass("win32_Processor");
		ManagementObjectCollection instances = managementClass.GetInstances();
		using (ManagementObjectCollection.ManagementObjectEnumerator managementObjectEnumerator = instances.GetEnumerator())
		{
			if (managementObjectEnumerator.MoveNext())
			{
				ManagementObject managementObject = (ManagementObject)managementObjectEnumerator.Current;
				result = managementObject.Properties["Processorid"].Value.ToString();
			}
		}
		return result;
	}

	public static string GetDiskVolumeSerialNumber()
	{
		ManagementClass managementClass = new ManagementClass("Win32_NetworkAdapterConfiguration");
		ManagementObject managementObject = new ManagementObject("win32_logicaldisk.deviceid=\"c:\"");
		managementObject.Get();
		return managementObject.GetPropertyValue("VolumeSerialNumber").ToString();
	}

	public static string CreateCode()
	{
		string text = getCpu() + GetDiskVolumeSerialNumber();
		string[] array = new string[24];
		for (int i = 0; i < 24; i++)
		{
			array[i] = text.Substring(i, 1);
		}
		text = "";
		for (int j = 0; j < 24; j++)
		{
			text += array[(j + 3 < 24) ? (j + 3) : 0];
		}
		return GetMd5(text);
	}

	public static void setIntCode()
	{
	}

	public static void setIntCodeF()
	{
		for (int i = 1; i < intCode.Length; i++)
		{
			intCode[i] = i % 9;
		}
	}

	public static string GetCode(string code)
	{
		if (code != "")
		{
			setIntCode();
			for (int i = 1; i < Charcode.Length; i++)
			{
				Charcode[i] = Convert.ToChar(code.Substring(i - 1, 1));
			}
			for (int j = 1; j < intNumber.Length; j++)
			{
				intNumber[j] = intCode[Convert.ToInt32(Charcode[j])] + Convert.ToInt32(Charcode[j]);
			}
			string text = null;
			for (int k = 1; k < intNumber.Length; k++)
			{
				text = ((intNumber[k] >= 48 && intNumber[k] <= 57) ? (text + Convert.ToChar(intNumber[k])) : ((intNumber[k] >= 65 && intNumber[k] <= 90) ? (text + Convert.ToChar(intNumber[k])) : ((intNumber[k] >= 97 && intNumber[k] <= 122) ? (text + Convert.ToChar(intNumber[k])) : ((intNumber[k] <= 122) ? (text + Convert.ToChar(intNumber[k] - 9)) : (text + Convert.ToChar(intNumber[k] - 10))))));
			}
			return text;
		}
		return "";
	}

	public static bool RegistIt(string currentCode, string realCode)
	{
		if (realCode != "")
		{
			if (currentCode.TrimEnd().Equals(realCode.TrimEnd()))
			{
				RegistryKey registryKey = Registry.CurrentUser.OpenSubKey("software", writable: true).CreateSubKey("IBrainChrom").CreateSubKey("IBrainChrom.ini")
					.CreateSubKey("active");
				registryKey.SetValue("StupidsCat", "BBC6D58D0953F027760A046D58D52786");
				registryKey = Registry.LocalMachine.OpenSubKey("software", writable: true).CreateSubKey("IBrainChrom").CreateSubKey("IBrainChrom.ini")
					.CreateSubKey("active");
				registryKey.SetValue("StupidsCat", "BBC6D58D0953F027760A046D58D52786");
				return true;
			}
			return false;
		}
		return false;
	}

	public static bool DeRegistIt()
	{
		RegistryKey registryKey = Registry.CurrentUser.OpenSubKey("software", writable: true).CreateSubKey("IBrainChrom").CreateSubKey("IBrainChrom.ini")
			.CreateSubKey("active");
		registryKey.SetValue("StupidsCat", "");
		registryKey = Registry.LocalMachine.OpenSubKey("software", writable: true).CreateSubKey("IBrainChrom").CreateSubKey("IBrainChrom.ini")
			.CreateSubKey("active");
		registryKey.SetValue("StupidsCat", "");
		return true;
	}

	public static bool BoolRegist(string sn)
	{
		bool flag = false;
		RegistryKey localMachine = Registry.LocalMachine;
		RegistryKey currentUser = Registry.CurrentUser;
		try
		{
			string[] valueNames = currentUser.OpenSubKey("software\\IBrainChrom\\IBrainChrom.ini\\active").GetValueNames();
			string[] array = valueNames;
			foreach (string text in array)
			{
				if (text == "StupidsCat" && currentUser.OpenSubKey("software\\IBrainChrom\\IBrainChrom.ini\\active").GetValue("StupidsCat").ToString() == "BBC6D58D0953F027760A046D58D52786")
				{
					return true;
				}
			}
			return false;
		}
		catch (Exception)
		{
			return false;
		}
		finally
		{
			localMachine.Close();
			currentUser.Close();
		}
	}

	public static string GetMd5(object text)
	{
		string text2 = text.ToString();
		MD5CryptoServiceProvider mD5CryptoServiceProvider = new MD5CryptoServiceProvider();
		byte[] bytes = Encoding.GetEncoding("utf-8").GetBytes(text.ToString());
		byte[] array = mD5CryptoServiceProvider.ComputeHash(bytes);
		return BitConverter.ToString(array).Replace("-", "");
	}
}
