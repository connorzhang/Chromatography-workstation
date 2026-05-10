using System;
using System.IO;
using System.Security.Cryptography;

namespace IBrainChrom2018.Unit;

public class KasuAes
{
	public static void Main2()
	{
		try
		{
			string text = "Here is some data to encrypt!";
			using Aes aes = Aes.Create();
			byte[] cipherText = EncryptStringToBytes_Aes(text, aes.Key, aes.IV);
			string arg = DecryptStringFromBytes_Aes(cipherText, aes.Key, aes.IV);
			Console.WriteLine("Original:   {0}", text);
			Console.WriteLine("Round Trip: {0}", arg);
		}
		catch (Exception ex)
		{
			Console.WriteLine("Error: {0}", ex.Message);
		}
	}

	public static byte[] EncryptStringToBytes_Aes(string plainText, byte[] Key, byte[] IV)
	{
		if (plainText == null || plainText.Length <= 0)
		{
			throw new ArgumentNullException("plainText");
		}
		if (Key == null || Key.Length == 0)
		{
			throw new ArgumentNullException("Key");
		}
		if (IV == null || IV.Length == 0)
		{
			throw new ArgumentNullException("Key");
		}
		using Aes aes = Aes.Create();
		aes.KeySize = 128;
		aes.Key = Key;
		aes.IV = IV;
		ICryptoTransform transform = aes.CreateEncryptor(aes.Key, aes.IV);
		using MemoryStream memoryStream = new MemoryStream();
		using CryptoStream stream = new CryptoStream(memoryStream, transform, CryptoStreamMode.Write);
		using (StreamWriter streamWriter = new StreamWriter(stream))
		{
			streamWriter.Write(plainText);
		}
		return memoryStream.ToArray();
	}

	public static string DecryptStringFromBytes_Aes(byte[] cipherText, byte[] Key, byte[] IV)
	{
		if (cipherText == null || cipherText.Length == 0)
		{
			throw new ArgumentNullException("cipherText");
		}
		if (Key == null || Key.Length == 0)
		{
			throw new ArgumentNullException("Key");
		}
		if (IV == null || IV.Length == 0)
		{
			throw new ArgumentNullException("Key");
		}
		string result = null;
		using (Aes aes = Aes.Create())
		{
			aes.KeySize = 128;
			aes.Key = Key;
			aes.IV = IV;
			ICryptoTransform transform = aes.CreateDecryptor(aes.Key, aes.IV);
			using MemoryStream stream = new MemoryStream(cipherText);
			using CryptoStream stream2 = new CryptoStream(stream, transform, CryptoStreamMode.Read);
			using StreamReader streamReader = new StreamReader(stream2);
			result = streamReader.ReadToEnd();
		}
		return result;
	}
}
