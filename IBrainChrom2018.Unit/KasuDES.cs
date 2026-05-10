using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace IBrainChrom2018.Unit;

public class KasuDES
{
	public static string Encrypt(string strData, string strKey)
	{
		string text = "";
		DES dES = new DESCryptoServiceProvider();
		byte[] bytes = Encoding.Default.GetBytes(strData);
		string text2 = strKey;
		int length = text2.Length;
		if (length < 8)
		{
			for (int i = 1; i <= 8 - length; i++)
			{
				text2 += " ";
			}
		}
		else
		{
			text2 = text2.Substring(0, 8);
		}
		dES.Key = Encoding.ASCII.GetBytes(text2);
		dES.IV = Encoding.ASCII.GetBytes(text2);
		MemoryStream memoryStream = new MemoryStream();
		CryptoStream cryptoStream = new CryptoStream(memoryStream, dES.CreateEncryptor(), CryptoStreamMode.Write);
		cryptoStream.Write(bytes, 0, bytes.Length);
		cryptoStream.FlushFinalBlock();
		cryptoStream.Close();
		memoryStream.Close();
		StringBuilder stringBuilder = new StringBuilder();
		byte[] array = memoryStream.ToArray();
		foreach (byte b in array)
		{
			stringBuilder.AppendFormat("{0:X2}", b);
		}
		return stringBuilder.ToString();
	}

	public static string Decrypt(string strData, string strKey)
	{
		string text = "";
		try
		{
			DESCryptoServiceProvider dESCryptoServiceProvider = new DESCryptoServiceProvider();
			byte[] array = new byte[strData.Length / 2];
			for (int i = 0; i < strData.Length / 2; i++)
			{
				int num = Convert.ToInt32(strData.Substring(i * 2, 2), 16);
				array[i] = (byte)num;
			}
			string text2 = strKey;
			int length = text2.Length;
			if (length < 8)
			{
				for (int j = 1; j <= 8 - length; j++)
				{
					text2 += " ";
				}
			}
			else
			{
				text2 = text2.Substring(0, 8);
			}
			dESCryptoServiceProvider.Key = Encoding.ASCII.GetBytes(text2);
			dESCryptoServiceProvider.IV = Encoding.ASCII.GetBytes(text2);
			MemoryStream memoryStream = new MemoryStream();
			CryptoStream cryptoStream = new CryptoStream(memoryStream, dESCryptoServiceProvider.CreateDecryptor(), CryptoStreamMode.Write);
			cryptoStream.Write(array, 0, array.Length);
			cryptoStream.FlushFinalBlock();
			text = Encoding.Default.GetString(memoryStream.ToArray());
			cryptoStream.Close();
			memoryStream.Close();
			return text;
		}
		catch (Exception)
		{
			return "";
		}
	}

	public static string Encrypt2(string strData, string strKey)
	{
		string text = "";
		DES dES = new DESCryptoServiceProvider();
		byte[] bytes = Encoding.Default.GetBytes(strData);
		string text2 = strKey;
		int length = text2.Length;
		if (length < 3)
		{
			for (int i = 1; i <= 3 - length; i++)
			{
				text2 += " ";
			}
		}
		else
		{
			text2 = text2.Substring(0, 3);
		}
		dES.Key = Encoding.ASCII.GetBytes(text2);
		dES.IV = Encoding.ASCII.GetBytes(text2);
		MemoryStream memoryStream = new MemoryStream();
		CryptoStream cryptoStream = new CryptoStream(memoryStream, dES.CreateEncryptor(), CryptoStreamMode.Write);
		cryptoStream.Write(bytes, 0, bytes.Length);
		cryptoStream.FlushFinalBlock();
		cryptoStream.Close();
		memoryStream.Close();
		StringBuilder stringBuilder = new StringBuilder();
		byte[] array = memoryStream.ToArray();
		foreach (byte b in array)
		{
			stringBuilder.AppendFormat("{0:X2}", b);
		}
		return stringBuilder.ToString();
	}
}
