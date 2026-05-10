using System;
using System.Globalization;
using System.IO;
using System.Text;

namespace IBrainChrom2018;

internal static class IBrainConvert
{
	public enum Enum6
	{
		const_0 = 0,
		const_1 = 108,
		const_2 = 1,
		const_3 = 2,
		const_4 = 3,
		const_5 = 4,
		const_6 = 5,
		const_7 = 6,
		const_8 = 7,
		const_9 = 8,
		const_10 = 106,
		const_11 = 9,
		const_12 = 10,
		const_13 = 11,
		const_14 = 104,
		const_15 = 12,
		const_16 = 13,
		const_17 = 14,
		const_18 = 16,
		const_19 = 17,
		const_20 = 18,
		const_21 = 19,
		const_22 = 20,
		const_23 = 21,
		const_24 = 22,
		const_25 = 23,
		const_26 = 33,
		const_27 = 34,
		const_28 = 35,
		const_29 = 36,
		const_30 = 37,
		const_31 = 38,
		const_32 = 39,
		const_33 = 40,
		const_34 = 41,
		const_35 = 42,
		const_36 = 40,
		const_37 = 41,
		const_38 = 45,
		const_39 = 47,
		const_40 = 48,
		const_41 = 49,
		const_42 = 50,
		const_43 = 53,
		const_44 = 60,
		const_45 = 61,
		const_46 = 62,
		const_47 = 63,
		const_48 = 64,
		const_49 = 65,
		const_50 = 66,
		const_51 = 67,
		const_52 = 69,
		const_53 = 80,
		const_54 = 83,
		const_55 = 84,
		const_56 = 88,
		const_57 = 89,
		const_58 = 90,
		const_59 = 91,
		const_60 = 96,
		const_61 = 97,
		const_62 = 98,
		const_63 = 99,
		const_64 = 100,
		const_65 = 101,
		const_66 = 110,
		const_67 = 111
	}

	private static byte[] byte_0 = new byte[256]
	{
		0, 94, 188, 226, 97, 63, 221, 131, 194, 156,
		126, 32, 163, 253, 31, 65, 157, 195, 33, 127,
		252, 162, 64, 30, 95, 1, 227, 189, 62, 96,
		130, 220, 35, 125, 159, 193, 66, 28, 254, 160,
		225, 191, 93, 3, 128, 222, 60, 98, 190, 224,
		2, 92, 223, 129, 99, 61, 124, 34, 192, 158,
		29, 67, 161, 255, 70, 24, 250, 164, 39, 121,
		155, 197, 132, 218, 56, 102, 229, 187, 89, 7,
		219, 133, 103, 57, 186, 228, 6, 88, 25, 71,
		165, 251, 120, 38, 196, 154, 101, 59, 217, 135,
		4, 90, 184, 230, 167, 249, 27, 69, 198, 152,
		122, 36, 248, 166, 68, 26, 153, 199, 37, 123,
		58, 100, 134, 216, 91, 5, 231, 185, 140, 210,
		48, 110, 237, 179, 81, 15, 78, 16, 242, 172,
		47, 113, 147, 205, 17, 79, 173, 243, 112, 46,
		204, 146, 211, 141, 111, 49, 178, 236, 14, 80,
		175, 241, 19, 77, 206, 144, 114, 44, 109, 51,
		209, 143, 12, 82, 176, 238, 50, 108, 142, 208,
		83, 13, 239, 177, 240, 174, 76, 18, 145, 207,
		45, 115, 202, 148, 118, 40, 171, 245, 23, 73,
		8, 86, 180, 234, 105, 55, 213, 139, 87, 9,
		235, 181, 54, 104, 138, 212, 149, 203, 41, 119,
		244, 170, 72, 22, 233, 183, 85, 11, 136, 214,
		52, 106, 43, 117, 151, 201, 74, 20, 246, 168,
		116, 42, 200, 150, 21, 75, 169, 247, 182, 232,
		10, 84, 215, 137, 107, 53
	};

	public static void ArrayAdd(ref byte[] byte_1, byte byte_2)
	{
		int num = byte_1.Length;
		Array.Resize(ref byte_1, num + 1);
		byte_1[num] = byte_2;
	}

	public static void ArrayCopy(ref byte[] byte_1, byte[] byte_2)
	{
		if (byte_2 != null)
		{
			int num = byte_1.Length;
			Array.Resize(ref byte_1, num + byte_2.Length);
			Array.Copy(byte_2, 0, byte_1, num, byte_2.Length);
		}
	}

	public static byte[] String2ByteArray(string string_0)
	{
		if (string_0 == null)
		{
			string_0 = "设备编号";
		}
		byte[] array = Encoding.Default.GetBytes(string_0);
		int num = array.Length;
		Array.Resize(ref array, 16);
		for (int i = num; i < array.Length; i++)
		{
			array[i] = 0;
		}
		return array;
	}

	public static byte ByteReverse(byte byte_1, int int_0)
	{
		byte_1 &= (byte)(255 - (1 << int_0));
		return byte_1;
	}

	public static byte[] Short2ByteArray(short short_0)
	{
		MemoryStream memoryStream = new MemoryStream(2);
		new BinaryWriter(memoryStream).Write(short_0);
		memoryStream.Position = 0L;
		byte[] array = new BinaryReader(memoryStream).ReadBytes(2);
		byte b = array[0];
		array[0] = array[1];
		array[1] = b;
		return array;
	}

	public static byte[] Short2ByteArray2(short short_0)
	{
		return Short2ByteArray(short_0);
	}

	public static float FromBcd_2B(byte[] value, int index, int dot)
	{
		byte b = (byte)(value[index] >> 4);
		byte b2 = (byte)((byte)(value[index] << 4) >> 4);
		byte b3 = (byte)(value[index + 1] >> 4);
		byte b4 = (byte)((byte)(value[index + 1] << 4) >> 4);
		return dot switch
		{
			1 => (float)(b * 100 + b2 * 10 + b3) + (float)(int)b4 * 0.1f, 
			2 => (float)(b * 10 + b2) + (float)(int)b3 * 0.1f + (float)(int)b4 * 0.01f, 
			_ => throw new Exception("不支持的小数位数 FromBcd_2B"), 
		};
	}

	public static float FromBcd_3B(byte[] value, int index, int dot)
	{
		byte b = (byte)(value[index] >> 4);
		byte b2 = (byte)((byte)(value[index] << 4) >> 4);
		byte b3 = (byte)(value[index + 1] >> 4);
		byte b4 = (byte)((byte)(value[index + 1] << 4) >> 4);
		byte b5 = (byte)(value[index + 2] >> 4);
		byte b6 = (byte)((byte)(value[index + 2] << 4) >> 4);
		return dot switch
		{
			1 => (float)(b * 10000 + b2 * 1000 + b3 * 100 + b4 * 10 + b5) + (float)(int)b6 * 0.1f, 
			2 => (float)(b * 1000 + b2 * 100 + b3 * 10 + b4) + (float)(int)b5 * 0.1f + (float)(int)b6 * 0.01f, 
			_ => throw new Exception("不支持的小数位数 FromBcd_3B"), 
		};
	}

	public static float ByteArray2Float(byte[] value, int index, int dot)
	{
		byte b = (byte)(value[index] >> 4);
		byte b2 = (byte)((byte)(value[index] << 4) >> 4);
		byte b3 = (byte)(value[index + 1] >> 4);
		byte b4 = (byte)((byte)(value[index + 1] << 4) >> 4);
		return dot switch
		{
			1 => (float)(b * 100 + b2 * 10 + b3) + (float)(int)b4 * 0.1f, 
			2 => (float)(b * 10 + b2) + (float)(int)b3 * 0.1f + (float)(int)b4 * 0.01f, 
			_ => throw new Exception("不支持的小数位数 FromBcd_2B"), 
		};
	}

	public static float Byte2ToFloat(byte[] byte_1, int int_0)
	{
		int num = byte_1[int_0];
		int num2 = byte_1[int_0 + 1];
		return (float)(num * 256 + num2 - 10000) / 100f;
	}

	public static float Byte3ToFloat(byte[] byte_1, int int_0)
	{
		byte b = (byte)(byte_1[int_0] >> 4);
		byte b2 = (byte)((byte)(byte_1[int_0] << 4) >> 4);
		byte b3 = (byte)(byte_1[int_0 + 1] >> 4);
		byte b4 = (byte)((byte)(byte_1[int_0 + 1] << 4) >> 4);
		byte b5 = (byte)(byte_1[int_0 + 2] >> 4);
		byte b6 = (byte)((byte)(byte_1[int_0 + 2] << 4) >> 4);
		return (float)(b * 100 + b2 * 10 + b3) + (float)(int)b4 * 0.1f + (float)(int)b5 * 0.01f + (float)(int)b6 * 0.001f;
	}

	public static int Byte2ToInt(byte[] byte_1, int int_0)
	{
		byte b = (byte)(byte_1[int_0] >> 4);
		byte b2 = (byte)((byte)(byte_1[int_0] << 4) >> 4);
		byte b3 = (byte)(byte_1[int_0 + 1] >> 4);
		byte b4 = (byte)((byte)(byte_1[int_0 + 1] << 4) >> 4);
		return b * 1000 + b2 * 100 + b3 * 10 + b4;
	}

	public static float Byte2Float(byte byte_1)
	{
		byte b = (byte)(byte_1 >> 4);
		byte b2 = (byte)((byte)(byte_1 << 4) >> 4);
		return (float)(int)b * 0.1f + (float)(int)b2 * 0.01f;
	}

	public static bool Byte2Bool(byte byte_1, int int_0)
	{
		return (byte_1 & (1 << int_0)) > 0;
	}

	public static byte BitByBitNo(byte[] byte_1, int idxSt, int length)
	{
		byte b = 0;
		for (int i = 0; i < length; i++)
		{
			byte b2 = (byte)(byte_1[idxSt + i] ^ b);
			b = byte_0[b2];
		}
		return b;
	}

	public static byte BitByBitNo(byte byte_1, int int_0)
	{
		byte_1 ^= (byte)(1 << int_0);
		return byte_1;
	}

	public static byte BitByBitOr(byte byte_1, int int_0)
	{
		byte_1 |= (byte)(1 << int_0);
		return byte_1;
	}

	public static string smethod_15(byte[] byte_1, byte[] byte_2, out string string_0)
	{
		return smethod_16(byte_1) + smethod_17(byte_1, byte_2, out string_0) + BitConverter.ToString(byte_1, byte_1.Length - 1, 1) + " [校验]";
	}

	private static string smethod_16(byte[] byte_1)
	{
		object obj = Encoding.ASCII.GetString(byte_1, 0, 4) + " ";
		obj = string.Concat(string.Concat(obj, BitConverter.ToString(byte_1, 4, 2), " [长度:", Byte2Short(byte_1, 4), "]\r\n"), BitConverter.ToString(byte_1, 6, 16), " ", BitConverter.ToString(byte_1, 22, 2), " [\"");
		return string.Concat(obj, Byte2String(byte_1, 6, 16), "\", 序列:", Byte2Short(byte_1, 22), "]\r\n");
	}

	private static string smethod_17(byte[] byte_1, byte[] byte_2, out string string_0)
	{
		string_0 = "?";
		string text = BitConverter.ToString(byte_1, 24, 1);
		byte b = byte_1[24];
		for (int i = 0; i < GC08_GCs.lsItems.Count; i++)
		{
			GC08_GCs.CmdItem cmdItem = GC08_GCs.lsItems[i];
			if (cmdItem.byte_0 == b)
			{
				string_0 = cmdItem.express;
				text = text + " [命令:" + cmdItem.express + "]\r\n";
				if (byte_2 != null)
				{
					text += BitConverter.ToString(byte_2);
				}
				return text + "\r\n";
			}
		}
		text += " [命令:?应答]\r\n";
		if (byte_2 != null)
		{
			text += BitConverter.ToString(byte_2);
		}
		return text + "\r\n";
	}

	public static byte[] ArrayCopy(byte[] byte_1, int int_0, int int_1)
	{
		byte[] array = new byte[int_1];
		int num = int_0 + int_1;
		if (num > byte_1.Length)
		{
			return array;
		}
		Array.Copy(byte_1, int_0, array, 0, int_1);
		return array;
	}

	public static byte[] Float2Byte2(float float_0, int int_0)
	{
		if (float_0 > 1000f && int_0 == 1)
		{
			throw new Exception(float_0 + ",    2字节不足[1位小数]");
		}
		if (float_0 > 100f && int_0 == 2)
		{
			throw new Exception(float_0 + ",    2字节不足[2位小数]");
		}
		string text = "0.0";
		if (int_0 == 2)
		{
			text = "0.00";
		}
		if (int_0 == 3)
		{
			text = "0.000";
		}
		if (int_0 == 4)
		{
			text = "0.0000";
		}
		if (int_0 == 5)
		{
			text = "0.00000";
		}
		string text2 = float_0.ToString(text);
		text2 = text2.Remove(text2.IndexOf('.'), 1);
		if (text2.Length > 6)
		{
			throw new Exception("ToBcd_2B1");
		}
		while (text2.Length < 6)
		{
			text2 = "0" + text2;
		}
		byte[] array = new byte[3]
		{
			(byte)(byte.Parse(text2[0].ToString()) << 4),
			0,
			0
		};
		char c = text2[1];
		array[0] = (byte)(array[0] + byte.Parse(c.ToString()));
		array[1] = (byte)(byte.Parse(text2[2].ToString()) << 4);
		c = text2[3];
		array[1] = (byte)(array[1] + byte.Parse(c.ToString()));
		array[2] = (byte)(byte.Parse(text2[4].ToString()) << 4);
		c = text2[5];
		array[2] = (byte)(array[2] + byte.Parse(c.ToString()));
		return array;
	}

	public static byte[] Float2Byte(float value, int dot)
	{
		if (dot == 3)
		{
			if (value > 1000f && dot == 1)
			{
				value = 1000f;
			}
			if (value > 100f && dot == 2)
			{
				value = 100f;
			}
			string text = "0.0";
			if (dot == 3)
			{
				text = "0.000";
			}
			string text2 = value.ToString(text);
			text2 = text2.Remove(text2.IndexOf('.'), 1);
			if (text2.Length > 4)
			{
			}
			while (text2.Length < 6)
			{
				text2 = "0" + text2;
			}
			byte[] array = new byte[3]
			{
				(byte)(byte.Parse(text2[0].ToString()) << 4),
				0,
				0
			};
			char c = text2[1];
			array[0] = (byte)(array[0] + byte.Parse(c.ToString()));
			array[1] = (byte)(byte.Parse(text2[2].ToString()) << 4);
			c = text2[3];
			array[1] = (byte)(array[1] + byte.Parse(c.ToString()));
			array[2] = (byte)(byte.Parse(text2[4].ToString()) << 4);
			c = text2[5];
			array[2] = (byte)(array[2] + byte.Parse(c.ToString()));
			return array;
		}
		if (value > 1000f && dot == 1)
		{
			value = 1000f;
		}
		if (value > 100f && dot == 2)
		{
			value = 100f;
		}
		string text3 = "0.0";
		if (dot == 2)
		{
			text3 = "0.00";
		}
		string text4 = value.ToString(text3);
		text4 = text4.Remove(text4.IndexOf('.'), 1);
		if (text4.Length > 4)
		{
		}
		while (text4.Length < 4)
		{
			text4 = "0" + text4;
		}
		byte[] array2 = new byte[2]
		{
			(byte)(byte.Parse(text4[0].ToString()) << 4),
			0
		};
		char c2 = text4[1];
		array2[0] = (byte)(array2[0] + byte.Parse(c2.ToString()));
		array2[1] = (byte)(byte.Parse(text4[2].ToString()) << 4);
		c2 = text4[3];
		array2[1] = (byte)(array2[1] + byte.Parse(c2.ToString()));
		return array2;
	}

	public static byte[] ToBcd_2B(float value, int dot)
	{
		if (value > 1000f && dot == 1)
		{
			throw new Exception(value + ",    2字节不足[1位小数]");
		}
		if (value > 100f && dot == 2)
		{
			throw new Exception(value + ",    2字节不足[2位小数]");
		}
		string text = "0.0";
		if (dot == 2)
		{
			text = "0.00";
		}
		string text2 = value.ToString(text);
		text2 = text2.Remove(text2.IndexOf('.'), 1);
		if (text2.Length > 4)
		{
			throw new Exception("ToBcd_2B1");
		}
		while (text2.Length < 4)
		{
			text2 = "0" + text2;
		}
		byte[] array = new byte[2]
		{
			(byte)(byte.Parse(text2[0].ToString()) << 4),
			0
		};
		char c = text2[1];
		array[0] = (byte)(array[0] + byte.Parse(c.ToString()));
		array[1] = (byte)(byte.Parse(text2[2].ToString()) << 4);
		c = text2[3];
		array[1] = (byte)(array[1] + byte.Parse(c.ToString()));
		return array;
	}

	public static byte[] ToBcd_3B_new(float value, int dot)
	{
		if ((double)value > 166666.5 && dot == 1)
		{
			throw new Exception(value + ",    3字节不足[1位小数]");
		}
		if (dot == 1)
		{
			value *= 10f;
		}
		if ((double)value > 16666.65 && dot == 2)
		{
			throw new Exception(value + ",    3字节不足[2位小数]");
		}
		if (dot == 2)
		{
			value *= 100f;
		}
		if (dot == 3)
		{
			value *= 1000f;
		}
		int[] array = new int[6];
		to3Byte((int)value, array);
		byte[] array2 = new byte[3];
		array2[0] = (byte)(array[0] << 4);
		array2[0] = (byte)(array2[0] + array[1]);
		array2[1] = (byte)(array[2] << 4);
		array2[1] = (byte)(array2[1] + array[3]);
		array2[2] = (byte)(array[4] << 4);
		array2[2] = (byte)(array2[2] + array[5]);
		return array2;
	}

	private static void to3Byte(int value, int[] num)
	{
		for (int i = 0; i < 5; i++)
		{
			int num2 = (int)((double)value / Math.Pow(10.0, 5 - i));
			if (num2 <= 15)
			{
				num[i] = num2;
			}
			else
			{
				num[i] = 15;
			}
			value -= (int)((double)num[i] * Math.Pow(10.0, 5 - i));
		}
		num[5] = value;
	}

	public static byte Float2Byte(float float_0)
	{
		string text = "0.00";
		string text2 = float_0.ToString(text);
		text2 = text2.Remove(text2.IndexOf('.'), 1);
		text2 = text2.Remove(text2.IndexOf('0'), 1);
		if (text2.Length > 2)
		{
			throw new Exception("ToBcd_2B1");
		}
		while (text2.Length < 2)
		{
			text2 = "0" + text2;
		}
		byte b = (byte)(byte.Parse(text2[0].ToString()) << 4);
		return (byte)(b + byte.Parse(text2[1].ToString()));
	}

	public static byte[] Int2Byte2(int int_0)
	{
		byte[] array = new byte[2];
		if (int_0 > 100)
		{
			array[0] = byte.Parse(((int)Math.Floor((double)int_0 / 100.0)).ToString(), NumberStyles.HexNumber);
			array[1] = byte.Parse(((int)Math.Floor((double)int_0 % 100.0)).ToString(), NumberStyles.HexNumber);
			return array;
		}
		array[0] = 0;
		array[1] = byte.Parse(int_0.ToString(), NumberStyles.HexNumber);
		return array;
	}

	public static byte[] Int2Byte3(int int_0)
	{
		return new byte[2]
		{
			(byte)(int_0 >> 8),
			(byte)int_0
		};
	}

	public static byte Int2Byte(int int_0)
	{
		while (int_0 >= 100)
		{
			int_0 -= 100;
		}
		string text = int_0.ToString();
		while (text.Length < 2)
		{
			text = "0" + text;
		}
		byte b = (byte)(byte.Parse(text[0].ToString()) << 4);
		return (byte)(b + byte.Parse(text[1].ToString()));
	}

	private static short Byte2Short(byte[] byte_1, int int_0)
	{
		return BitConverter.ToInt16(new byte[2]
		{
			byte_1[int_0 + 1],
			byte_1[int_0]
		}, 0);
	}

	private static string Byte2String(byte[] byte_1, int int_0, int int_1)
	{
		string text = Encoding.Default.GetString(byte_1, int_0, int_1);
		int num = text.IndexOf('\0');
		if (num >= 0)
		{
			return text.Remove(num);
		}
		return text;
	}
}
