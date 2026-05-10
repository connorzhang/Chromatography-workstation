using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace IBrainChrom2018.Unit;

public class KasuMD5
{
	public static string MD5Encrypt(string strData, string strKey)
	{
		return MD5String(MD5String(strData) + strKey);
	}

	public static string MD5FileEncrypt(string strPath, string strKey)
	{
		FileStream fileStream = new FileStream(strPath, FileMode.Open, FileAccess.Read, FileShare.Read);
		byte[] array = new byte[fileStream.Length];
		fileStream.Read(array, 0, (int)fileStream.Length);
		fileStream.Close();
		string text = MD5Buffer(array, 0, array.Length);
		return MD5String(text + strKey);
	}

	private static string MD5Buffer(byte[] buffer, int index, int count)
	{
		string text = "";
		MD5CryptoServiceProvider mD5CryptoServiceProvider = new MD5CryptoServiceProvider();
		byte[] array = mD5CryptoServiceProvider.ComputeHash(buffer, index, count);
		mD5CryptoServiceProvider.Clear();
		text = BitConverter.ToString(array);
		return text.Replace("-", "");
	}

	private static string MD5String(string str)
	{
		byte[] bytes = Encoding.ASCII.GetBytes(str);
		return MD5Buffer(bytes, 0, bytes.Length);
	}
}
