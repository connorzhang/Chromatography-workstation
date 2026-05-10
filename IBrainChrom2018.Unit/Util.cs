using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;
using log4net;

namespace IBrainChrom2018.Unit;

public class Util
{
	private const string KEY_64 = "jzw7tz8g";

	private const string IV_64 = "tvbzn9i7";

	private static ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

	public static string SysCofigName { get; set; }

	public static string Encode(string data)
	{
		byte[] bytes = Encoding.ASCII.GetBytes("jzw7tz8g");
		byte[] bytes2 = Encoding.ASCII.GetBytes("tvbzn9i7");
		DESCryptoServiceProvider dESCryptoServiceProvider = new DESCryptoServiceProvider();
		int keySize = dESCryptoServiceProvider.KeySize;
		MemoryStream memoryStream = new MemoryStream();
		CryptoStream cryptoStream = new CryptoStream(memoryStream, dESCryptoServiceProvider.CreateEncryptor(bytes, bytes2), CryptoStreamMode.Write);
		StreamWriter streamWriter = new StreamWriter(cryptoStream);
		streamWriter.Write(data);
		streamWriter.Flush();
		cryptoStream.FlushFinalBlock();
		streamWriter.Flush();
		return Convert.ToBase64String(memoryStream.GetBuffer(), 0, (int)memoryStream.Length);
	}

	public static string Decode(string data)
	{
		byte[] bytes = Encoding.ASCII.GetBytes("jzw7tz8g");
		byte[] bytes2 = Encoding.ASCII.GetBytes("tvbzn9i7");
		byte[] buffer;
		try
		{
			buffer = Convert.FromBase64String(data);
		}
		catch
		{
			return null;
		}
		DESCryptoServiceProvider dESCryptoServiceProvider = new DESCryptoServiceProvider();
		MemoryStream stream = new MemoryStream(buffer);
		CryptoStream stream2 = new CryptoStream(stream, dESCryptoServiceProvider.CreateDecryptor(bytes, bytes2), CryptoStreamMode.Read);
		StreamReader streamReader = new StreamReader(stream2);
		return streamReader.ReadToEnd();
	}

	public static float ConvertPressUnit(float value, PressUnit unit, PressUnit toUnit)
	{
		return toUnit switch
		{
			PressUnit.Bar => unit switch
			{
				PressUnit.Bar => value, 
				PressUnit.Mpa => float.Parse((value * 10f).ToString("0.00")), 
				PressUnit.Psi => float.Parse((value * 0.98f / 14.2f).ToString("0.00")), 
				PressUnit.kgf => float.Parse((value * 0.98f).ToString("0.00")), 
				_ => -1f, 
			}, 
			PressUnit.Psi => unit switch
			{
				PressUnit.Bar => float.Parse((value * 14.2f / 0.98f).ToString("0.00")), 
				PressUnit.Mpa => float.Parse((value * 14.2f / 0.098f).ToString("0.00")), 
				PressUnit.Psi => value, 
				PressUnit.kgf => float.Parse((value * 14.2f).ToString("0.00")), 
				_ => -1f, 
			}, 
			PressUnit.Mpa => unit switch
			{
				PressUnit.Bar => float.Parse((value * 0.1f).ToString("0.00")), 
				PressUnit.Mpa => value, 
				PressUnit.Psi => float.Parse((value * 0.098f / 14.2f).ToString("0.00")), 
				PressUnit.kgf => float.Parse((value * 0.098f).ToString("0.00")), 
				_ => -1f, 
			}, 
			PressUnit.kgf => unit switch
			{
				PressUnit.Bar => float.Parse((value / 0.98f).ToString("0.00")), 
				PressUnit.Mpa => float.Parse((value / 0.098f).ToString("0.00")), 
				PressUnit.Psi => float.Parse((value / 14.2f).ToString("0.00")), 
				PressUnit.kgf => value, 
				_ => -1f, 
			}, 
			_ => -1f, 
		};
	}

	public static string GetFileMD5(string path)
	{
		FileStream inputStream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read);
		MD5CryptoServiceProvider mD5CryptoServiceProvider = new MD5CryptoServiceProvider();
		byte[] array = mD5CryptoServiceProvider.ComputeHash(inputStream);
		string text = BitConverter.ToString(array);
		return text.Replace("-", "");
	}

	public static byte[] Sub(byte[] array, int startIndex, int length)
	{
		byte[] array2 = new byte[length];
		Array.Copy(array, startIndex, array2, 0, length);
		return array2;
	}

	public static byte[] Append(byte[] array, byte[] add)
	{
		int destinationIndex = array.Length;
		Array.Resize(ref array, array.Length + add.Length);
		Array.Copy(add, 0, array, destinationIndex, add.Length);
		return array;
	}

	public static string GetBytesToX2Str(byte[] validByte, bool space)
	{
		string text = "";
		string text2 = "";
		if (space)
		{
			text2 = " ";
		}
		foreach (byte b in validByte)
		{
			text = text + b.ToString("X2") + text2;
		}
		return text;
	}

	public static object ConvertCellValue(DataGridViewCell cell, Type type)
	{
		string text = "";
		if (cell.Value != null)
		{
			text = cell.Value.ToString();
		}
		try
		{
			if (type == typeof(int))
			{
				return int.Parse(text);
			}
			if (type == typeof(float))
			{
				return float.Parse(text);
			}
			if (type == typeof(double))
			{
				return double.Parse(text);
			}
			if (type == typeof(bool))
			{
				return text.ToUpper().Equals("TRUE");
			}
			return text;
		}
		catch
		{
			throw new Exception(Lang.PS("格式错误") + ":" + text);
		}
	}

	public static object ConvertParseValue(string str, object def, Type type)
	{
		string text = "";
		if (str != null)
		{
			text = str;
		}
		try
		{
			if (type == typeof(int))
			{
				return int.Parse(text);
			}
			if (type == typeof(long))
			{
				return long.Parse(text);
			}
			if (type == typeof(float))
			{
				return float.Parse(text);
			}
			if (type == typeof(double))
			{
				return double.Parse(text);
			}
			if (type == typeof(short))
			{
				return short.Parse(text);
			}
			if (type == typeof(byte))
			{
				return byte.Parse(text);
			}
			if (type == typeof(bool))
			{
				return text.ToUpper().Equals("TRUE");
			}
			return text;
		}
		catch
		{
			return def;
		}
	}

	public static float? GetYByX(List<PointF> list, float x)
	{
		if (list == null)
		{
			return null;
		}
		float? result = null;
		for (int i = 0; i < list.Count - 1; i++)
		{
			if (x >= list[i].X && x < list[i + 1].X)
			{
				float x2 = list[i].X;
				float x3 = list[i + 1].X;
				float y = list[i].Y;
				float y2 = list[i + 1].Y;
				result = (x - x2) * (y2 - y) / (x3 - x2) + y;
				break;
			}
		}
		return result;
	}

	public static byte[] GetByteImage(Image img)
	{
		byte[] array = new byte[0];
		if (img != null && !img.Equals(null))
		{
			using MemoryStream memoryStream = new MemoryStream();
			Bitmap bitmap = new Bitmap(img);
			bitmap.Save(memoryStream, ImageFormat.Jpeg);
			array = new byte[memoryStream.Length];
			memoryStream.Position = 0L;
			memoryStream.Read(array, 0, Convert.ToInt32(array.Length));
			memoryStream.Close();
		}
		return array;
	}
}
