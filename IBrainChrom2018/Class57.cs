using System;
using System.Runtime.InteropServices;
using System.Text;

namespace IBrainChrom2018;

internal class Class57
{
	private const ushort ushort_0 = 13961;

	private const ushort ushort_1 = 34658;

	private const ushort ushort_2 = 8224;

	private const ushort ushort_3 = 13961;

	private const ushort ushort_4 = 8224;

	private const ushort ushort_5 = 8224;

	private const short short_0 = 2;

	private const short short_1 = 16;

	private const short short_2 = -1;

	private const short short_3 = 259;

	private const uint uint_0 = 2147483648u;

	private const int int_0 = 1073741824;

	private const uint uint_1 = 1u;

	private const uint uint_2 = 2u;

	private const uint uint_3 = 3u;

	private const uint uint_4 = 128u;

	private const uint uint_5 = 65535u;

	private const short short_4 = 495;

	public const int int_1 = -21;

	public const int int_2 = -22;

	public const int int_3 = -23;

	public const int int_4 = -24;

	public const int int_5 = -50;

	public const int int_6 = 97;

	public const int int_7 = 128;

	public const int int_8 = 225;

	public const int int_9 = 80;

	public const int int_10 = 32;

	public const int int_11 = 16;

	private const byte byte_0 = 1;

	private const byte byte_1 = 2;

	private const byte byte_2 = 5;

	private const byte byte_3 = 8;

	private const byte byte_4 = 9;

	private const byte byte_5 = 16;

	private const byte byte_6 = 17;

	private const byte byte_7 = 18;

	private const byte byte_8 = 19;

	private const byte byte_9 = 32;

	private const byte byte_10 = 36;

	private const byte byte_11 = 48;

	private const byte byte_12 = 49;

	private const byte byte_13 = 50;

	private const byte byte_14 = 51;

	private const byte byte_15 = 52;

	private const byte byte_16 = 53;

	private const byte byte_17 = 54;

	private const byte byte_18 = 55;

	private const byte byte_19 = 81;

	private const byte byte_20 = 82;

	private const byte byte_21 = 83;

	private const byte byte_22 = 83;

	private bool bool_0;

	[DllImport("kernel32.dll")]
	public static extern int lstrlenA(string string_0);

	[DllImport("kernel32.dll")]
	public static extern void RtlMoveMemory(byte[] byte_23, string string_0, int int_12);

	[DllImport("kernel32.dll", EntryPoint = "RtlMoveMemory")]
	public static extern void RtlMoveMemory_1(StringBuilder stringBuilder_0, byte[] byte_23, int int_12);

	[DllImport("HID.dll")]
	private static extern bool HidD_GetAttributes(int int_12, ref HIDD_ATTRIBUTES hidd_ATTRIBUTES_0);

	[DllImport("HID.dll")]
	private static extern int HidD_GetHidGuid(ref GUID guid_0);

	[DllImport("HID.dll")]
	private static extern bool HidD_GetPreparsedData(int int_12, ref IntPtr intptr_0);

	[DllImport("HID.dll")]
	private static extern int HidP_GetCaps(IntPtr intptr_0, ref HIDP_CAPS hidp_CAPS_0);

	[DllImport("HID.dll")]
	private static extern bool HidD_FreePreparsedData(IntPtr intptr_0);

	[DllImport("HID.dll")]
	private static extern bool HidD_SetFeature(int int_12, byte[] byte_23, int int_13);

	[DllImport("HID.dll")]
	private static extern bool HidD_GetFeature(int int_12, byte[] byte_23, int int_13);

	[DllImport("SetupApi.dll")]
	private static extern IntPtr SetupDiGetClassDevsA(ref GUID guid_0, int int_12, int int_13, int int_14);

	[DllImport("SetupApi.dll")]
	private static extern bool SetupDiDestroyDeviceInfoList(IntPtr intptr_0);

	[DllImport("SetupApi.dll")]
	private static extern bool SetupDiGetDeviceInterfaceDetailA(IntPtr intptr_0, ref SP_INTERFACE_DEVICE_DATA sp_INTERFACE_DEVICE_DATA_0, ref SP_DEVICE_INTERFACE_DETAIL_DATA sp_DEVICE_INTERFACE_DETAIL_DATA_0, int int_12, ref int int_13, int int_14);

	[DllImport("SetupApi.dll", EntryPoint = "SetupDiGetDeviceInterfaceDetailA")]
	private static extern bool SetupDiGetDeviceInterfaceDetailA_1(IntPtr intptr_0, ref SP_INTERFACE_DEVICE_DATA_64 sp_INTERFACE_DEVICE_DATA_64_0, ref SP_DEVICE_INTERFACE_DETAIL_DATA sp_DEVICE_INTERFACE_DETAIL_DATA_0, int int_12, ref int int_13, int int_14);

	[DllImport("SetupApi.dll")]
	private static extern bool SetupDiEnumDeviceInterfaces(IntPtr intptr_0, int int_12, ref GUID guid_0, int int_13, ref SP_INTERFACE_DEVICE_DATA sp_INTERFACE_DEVICE_DATA_0);

	[DllImport("SetupApi.dll", EntryPoint = "SetupDiEnumDeviceInterfaces")]
	private static extern bool SetupDiEnumDeviceInterfaces_1(IntPtr intptr_0, ulong ulong_0, ref GUID guid_0, int int_12, ref SP_INTERFACE_DEVICE_DATA_64 sp_INTERFACE_DEVICE_DATA_64_0);

	[DllImport("kernel32.dll")]
	private static extern int CreateFileA(string string_0, uint uint_6, uint uint_7, uint uint_8, uint uint_9, uint uint_10, uint uint_11);

	[DllImport("kernel32.dll")]
	private static extern int CloseHandle(int int_12);

	[DllImport("kernel32.dll")]
	private static extern int GetLastError();

	[DllImport("kernel32.dll")]
	private static extern int CreateSemaphoreA(int int_12, int int_13, int int_14, string string_0);

	[DllImport("kernel32.dll")]
	private static extern int WaitForSingleObject(int int_12, uint uint_6);

	[DllImport("kernel32.dll")]
	private static extern int ReleaseSemaphore(int int_12, int int_13, int int_14);

	public Class57()
	{
		bool_0 = IntPtr.Size == 4;
	}

	private static string smethod_0(byte[] byte_23)
	{
		char[] array = new char[2];
		char[] trimChars = array;
		Encoding encoding = Encoding.Default;
		return encoding.GetString(byte_23).TrimEnd(trimChars);
	}

	private uint method_0(string string_0)
	{
		string[] array = new string[16]
		{
			"0", "1", "2", "3", "4", "5", "6", "7", "8", "9",
			"A", "B", "C", "D", "E", "F"
		};
		string_0 = string_0.ToUpper();
		int num = 1;
		int num2 = 0;
		for (int num3 = string_0.Length; num3 > 0; num3--)
		{
			string text = string_0.Substring(num3 - 1, 1);
			int num4 = 0;
			for (int i = 0; i < 16; i++)
			{
				if (text == array[i])
				{
					num4 = i;
				}
			}
			num2 += num4 * num;
			num *= 16;
		}
		return (uint)num2;
	}

	private int method_1(string string_0, ref byte[] byte_23)
	{
		int length = string_0.Length;
		if (length < 16)
		{
		}
		int num = length / 2;
		byte_23 = new byte[num];
		int num2 = 0;
		for (int i = 0; i < length; i += 2)
		{
			string string_1 = string_0.Substring(i, 2);
			byte_23[num2] = (byte)method_0(string_1);
			num2++;
		}
		return num;
	}

	public void method_2(byte[] byte_23, byte[] byte_24, string string_0)
	{
		uint[] array = new uint[16];
		uint num = 2654435769u;
		uint num2 = 0u;
		int length = string_0.Length;
		int num3 = 0;
		for (int i = 1; i <= length; i += 2)
		{
			string string_1 = string_0.Substring(i - 1, 2);
			array[num3] = method_0(string_1);
			num3++;
		}
		uint num4 = 0u;
		uint num5 = 0u;
		uint num6 = 0u;
		uint num7 = 0u;
		for (int j = 0; j <= 3; j++)
		{
			num4 = (array[j] << j * 8) | num4;
			num5 = (array[j + 4] << j * 8) | num5;
			num6 = (array[j + 4 + 4] << j * 8) | num6;
			num7 = (array[j + 4 + 4 + 4] << j * 8) | num7;
		}
		uint num8 = 0u;
		uint num9 = 0u;
		for (int k = 0; k <= 3; k++)
		{
			uint num10 = byte_23[k];
			num8 = (num10 << k * 8) | num8;
			num10 = byte_23[k + 4];
			num9 = (num10 << k * 8) | num9;
		}
		for (int num11 = 32; num11 > 0; num11--)
		{
			num2 = num + num2;
			num8 += ((num9 << 4) + num4) ^ (num9 + num2) ^ ((num9 >> 5) + num5);
			num9 += ((num8 << 4) + num6) ^ (num8 + num2) ^ ((num8 >> 5) + num7);
		}
		for (int l = 0; l <= 3; l++)
		{
			byte_24[l] = Convert.ToByte((num8 >> l * 8) & 0xFF);
			byte_24[l + 4] = Convert.ToByte((num9 >> l * 8) & 0xFF);
		}
	}

	public void method_3(byte[] byte_23, byte[] byte_24, string string_0)
	{
		uint[] array = new uint[16];
		uint num = 2654435769u;
		uint num2 = 3337565984u;
		int length = string_0.Length;
		int num3 = 0;
		int i;
		for (i = 1; i <= length; i += 2)
		{
			string string_1 = string_0.Substring(i - 1, 2);
			array[num3] = method_0(string_1);
			num3++;
		}
		uint num4 = 0u;
		uint num5 = 0u;
		uint num6 = 0u;
		uint num7 = 0u;
		for (i = 0; i <= 3; i++)
		{
			num4 = (array[i] << i * 8) | num4;
			num5 = (array[i + 4] << i * 8) | num5;
			num6 = (array[i + 4 + 4] << i * 8) | num6;
			num7 = (array[i + 4 + 4 + 4] << i * 8) | num7;
		}
		uint num8 = 0u;
		uint num9 = 0u;
		for (i = 0; i <= 3; i++)
		{
			uint num10 = byte_23[i];
			num8 = (num10 << i * 8) | num8;
			num10 = byte_23[i + 4];
			num9 = (num10 << i * 8) | num9;
		}
		i = 32;
		while (i-- > 0)
		{
			num9 -= ((num8 << 4) + num6) ^ (num8 + num2) ^ ((num8 >> 5) + num7);
			num8 -= ((num9 << 4) + num4) ^ (num9 + num2) ^ ((num9 >> 5) + num5);
			num2 -= num;
		}
		for (i = 0; i <= 3; i++)
		{
			byte_24[i] = Convert.ToByte((num8 >> i * 8) & 0xFF);
			byte_24[i + 4] = Convert.ToByte((num9 >> i * 8) & 0xFF);
		}
	}

	public string method_4(string string_0, string string_1)
	{
		byte[] array = new byte[8];
		byte[] array2 = new byte[8];
		int num = lstrlenA(string_0) + 1;
		int num2 = ((num >= 8) ? num : 8);
		byte[] array3 = new byte[num2];
		byte[] array4 = new byte[num2];
		RtlMoveMemory(array3, string_0, num);
		array3.CopyTo(array4, 0);
		for (int i = 0; i <= num2 - 8; i += 8)
		{
			for (int j = 0; j < 8; j++)
			{
				array[j] = array3[j + i];
			}
			method_2(array, array2, string_1);
			for (int k = 0; k < 8; k++)
			{
				array4[k + i] = array2[k];
			}
		}
		string text = "";
		for (int l = 0; l <= num2 - 1; l++)
		{
			text += array4[l].ToString("X2");
		}
		return text;
	}

	public string method_5(string string_0, string string_1)
	{
		byte[] array = new byte[8];
		byte[] array2 = new byte[8];
		int length = string_0.Length;
		if (length < 16)
		{
		}
		int num = length / 2;
		byte[] array3 = new byte[num];
		byte[] array4 = new byte[num];
		int num2 = 0;
		for (int i = 1; i <= length; i += 2)
		{
			string string_2 = string_0.Substring(i - 1, 2);
			array3[num2] = Convert.ToByte(method_0(string_2));
			num2++;
		}
		array3.CopyTo(array4, 0);
		for (int j = 0; j <= num - 8; j += 8)
		{
			for (num2 = 0; num2 < 8; num2++)
			{
				array[num2] = array3[num2 + j];
			}
			method_3(array, array2, string_1);
			for (num2 = 0; num2 < 8; num2++)
			{
				array4[num2 + j] = array2[num2];
			}
		}
		StringBuilder stringBuilder = new StringBuilder("", num);
		RtlMoveMemory_1(stringBuilder, array4, num);
		return stringBuilder.ToString();
	}

	private bool method_6(int int_12, ref int int_13, ref string string_0)
	{
		if (bool_0)
		{
			return method_8(int_12, ref int_13, ref string_0);
		}
		return method_7(int_12, ref int_13, ref string_0);
	}

	private bool method_7(int int_12, ref int int_13, ref string string_0)
	{
		SP_INTERFACE_DEVICE_DATA_64 sp_INTERFACE_DEVICE_DATA_64_ = default(SP_INTERFACE_DEVICE_DATA_64);
		GUID guid_ = default(GUID);
		SP_DEVICE_INTERFACE_DETAIL_DATA sp_DEVICE_INTERFACE_DETAIL_DATA_ = default(SP_DEVICE_INTERFACE_DETAIL_DATA);
		HIDD_ATTRIBUTES hidd_ATTRIBUTES_ = default(HIDD_ATTRIBUTES);
		int i = 0;
		int_13 = 0;
		HidD_GetHidGuid(ref guid_);
		IntPtr intPtr = SetupDiGetClassDevsA(ref guid_, 0, 0, 18);
		if (intPtr == (IntPtr)(-1))
		{
			return false;
		}
		sp_INTERFACE_DEVICE_DATA_64_.cbSize = Marshal.SizeOf((object)sp_INTERFACE_DEVICE_DATA_64_);
		for (; SetupDiEnumDeviceInterfaces_1(intPtr, 0uL, ref guid_, i, ref sp_INTERFACE_DEVICE_DATA_64_); i++)
		{
			if (GetLastError() == 259)
			{
				SetupDiDestroyDeviceInfoList(intPtr);
				return false;
			}
			sp_DEVICE_INTERFACE_DETAIL_DATA_.cbSize = 8;
			int int_14 = 0;
			if (!SetupDiGetDeviceInterfaceDetailA_1(intPtr, ref sp_INTERFACE_DEVICE_DATA_64_, ref sp_DEVICE_INTERFACE_DETAIL_DATA_, 300, ref int_14, 0))
			{
				SetupDiDestroyDeviceInfoList(intPtr);
				return false;
			}
			string_0 = smethod_0(sp_DEVICE_INTERFACE_DETAIL_DATA_.DevicePath);
			int num = CreateFileA(string_0, 3221225472u, 3u, 0u, 3u, 0u, 0u);
			if (-1 == num)
			{
				continue;
			}
			if (HidD_GetAttributes(num, ref hidd_ATTRIBUTES_) && ((hidd_ATTRIBUTES_.ProductID == 34658 && hidd_ATTRIBUTES_.VendorID == 13961) || (hidd_ATTRIBUTES_.ProductID == 8224 && hidd_ATTRIBUTES_.VendorID == 13961) || (hidd_ATTRIBUTES_.ProductID == 8224 && hidd_ATTRIBUTES_.VendorID == 8224)))
			{
				if (int_12 == int_13)
				{
					SetupDiDestroyDeviceInfoList(intPtr);
					CloseHandle(num);
					return true;
				}
				int_13++;
			}
			CloseHandle(num);
		}
		return false;
	}

	private bool method_8(int int_12, ref int int_13, ref string string_0)
	{
		SP_INTERFACE_DEVICE_DATA sp_INTERFACE_DEVICE_DATA_ = default(SP_INTERFACE_DEVICE_DATA);
		GUID guid_ = default(GUID);
		SP_DEVICE_INTERFACE_DETAIL_DATA sp_DEVICE_INTERFACE_DETAIL_DATA_ = default(SP_DEVICE_INTERFACE_DETAIL_DATA);
		HIDD_ATTRIBUTES hidd_ATTRIBUTES_ = default(HIDD_ATTRIBUTES);
		int i = 0;
		int_13 = 0;
		HidD_GetHidGuid(ref guid_);
		IntPtr intPtr = SetupDiGetClassDevsA(ref guid_, 0, 0, 18);
		if (intPtr == (IntPtr)(-1))
		{
			return false;
		}
		sp_INTERFACE_DEVICE_DATA_.cbSize = Marshal.SizeOf((object)sp_INTERFACE_DEVICE_DATA_);
		for (; SetupDiEnumDeviceInterfaces(intPtr, 0, ref guid_, i, ref sp_INTERFACE_DEVICE_DATA_); i++)
		{
			if (GetLastError() == 259)
			{
				SetupDiDestroyDeviceInfoList(intPtr);
				return false;
			}
			sp_DEVICE_INTERFACE_DETAIL_DATA_.cbSize = Marshal.SizeOf((object)sp_DEVICE_INTERFACE_DETAIL_DATA_) - 255;
			int int_14 = 0;
			if (!SetupDiGetDeviceInterfaceDetailA(intPtr, ref sp_INTERFACE_DEVICE_DATA_, ref sp_DEVICE_INTERFACE_DETAIL_DATA_, 300, ref int_14, 0))
			{
				SetupDiDestroyDeviceInfoList(intPtr);
				return false;
			}
			string_0 = smethod_0(sp_DEVICE_INTERFACE_DETAIL_DATA_.DevicePath);
			int num = CreateFileA(string_0, 3221225472u, 3u, 0u, 3u, 0u, 0u);
			if (-1 == num)
			{
				continue;
			}
			if (HidD_GetAttributes(num, ref hidd_ATTRIBUTES_) && ((hidd_ATTRIBUTES_.ProductID == 34658 && hidd_ATTRIBUTES_.VendorID == 13961) || (hidd_ATTRIBUTES_.ProductID == 8224 && hidd_ATTRIBUTES_.VendorID == 13961) || (hidd_ATTRIBUTES_.ProductID == 8224 && hidd_ATTRIBUTES_.VendorID == 8224)))
			{
				if (int_12 == int_13)
				{
					SetupDiDestroyDeviceInfoList(intPtr);
					CloseHandle(num);
					return true;
				}
				int_13++;
			}
			CloseHandle(num);
		}
		return false;
	}

	private bool method_9(int int_12, byte[] byte_23, int int_13)
	{
		byte[] array = new byte[512];
		IntPtr intptr_ = IntPtr.Zero;
		HIDP_CAPS hidp_CAPS_ = default(HIDP_CAPS);
		if (!HidD_GetPreparsedData(int_12, ref intptr_))
		{
			return false;
		}
		if (HidP_GetCaps(intptr_, ref hidp_CAPS_) <= 0)
		{
			HidD_FreePreparsedData(intptr_);
			return false;
		}
		bool flag = true;
		array[0] = 1;
		bool flag2;
		if (flag2 = HidD_GetFeature(int_12, array, hidp_CAPS_.FeatureReportByteLength))
		{
			for (int i = 0; i < int_13; i++)
			{
				byte_23[i] = array[i];
			}
		}
		flag = flag && flag2;
		HidD_FreePreparsedData(intptr_);
		return flag;
	}

	private bool method_10(int int_12, byte[] byte_23, int int_13)
	{
		byte[] array = new byte[512];
		IntPtr intptr_ = IntPtr.Zero;
		HIDP_CAPS hidp_CAPS_ = default(HIDP_CAPS);
		if (!HidD_GetPreparsedData(int_12, ref intptr_))
		{
			return false;
		}
		if (HidP_GetCaps(intptr_, ref hidp_CAPS_) <= 0)
		{
			HidD_FreePreparsedData(intptr_);
			return false;
		}
		bool flag = true;
		array[0] = 2;
		for (int i = 0; i < int_13; i++)
		{
			array[i + 1] = byte_23[i + 1];
		}
		bool flag2 = HidD_SetFeature(int_12, array, hidp_CAPS_.FeatureReportByteLength);
		flag = flag && flag2;
		HidD_FreePreparsedData(intptr_);
		return flag;
	}

	private int method_11(int int_12, ref string string_0)
	{
		int int_13 = 0;
		if (!method_6(int_12, ref int_13, ref string_0))
		{
			return -92;
		}
		return 0;
	}

	private int method_12(ref int int_12, string string_0)
	{
		int int_13 = 0;
		if (string_0.Length < 1)
		{
			string string_1 = "";
			if (!method_6(0, ref int_13, ref string_1))
			{
				return -92;
			}
			int_12 = CreateFileA(string_1, 3221225472u, 3u, 0u, 3u, 128u, 0u);
			if (int_12 == -1)
			{
				return -92;
			}
		}
		else
		{
			int_12 = CreateFileA(string_0, 3221225472u, 3u, 0u, 3u, 128u, 0u);
			if (int_12 == -1)
			{
				return -92;
			}
		}
		return 0;
	}

	private int method_13(ref short short_5, string string_0)
	{
		byte[] array = new byte[25];
		byte[] array2 = new byte[25];
		int int_ = 0;
		if (method_12(ref int_, string_0) != 0)
		{
			return -92;
		}
		array[1] = 1;
		if (!method_10(int_, array, 1))
		{
			CloseHandle(int_);
			return -93;
		}
		if (!method_9(int_, array2, 1))
		{
			CloseHandle(int_);
			return -93;
		}
		CloseHandle(int_);
		short_5 = array2[0];
		return 0;
	}

	private int method_14(ref int int_12, ref int int_13, string string_0)
	{
		int[] array = new int[8];
		byte[] array2 = new byte[25];
		byte[] array3 = new byte[25];
		int int_14 = 0;
		if (method_12(ref int_14, string_0) != 0)
		{
			return -92;
		}
		array2[1] = 2;
		if (!method_10(int_14, array2, 1))
		{
			CloseHandle(int_14);
			return -93;
		}
		if (!method_9(int_14, array3, 8))
		{
			CloseHandle(int_14);
			return -93;
		}
		CloseHandle(int_14);
		array[0] = array3[0];
		array[1] = array3[1];
		array[2] = array3[2];
		array[3] = array3[3];
		array[4] = array3[4];
		array[5] = array3[5];
		array[6] = array3[6];
		array[7] = array3[7];
		int_12 = array[3] | (array[2] << 8) | (array[1] << 16) | (array[0] << 24);
		int_13 = array[7] | (array[6] << 8) | (array[5] << 16) | (array[4] << 24);
		return 0;
	}

	private int method_15(byte[] byte_23, int int_12, int int_13, byte[] byte_24, string string_0, int int_14)
	{
		byte[] array = new byte[512];
		byte[] array2 = new byte[512];
		if (int_12 > 495 || int_12 < 0)
		{
			return -81;
		}
		if (int_13 > 255)
		{
			return -87;
		}
		if (int_13 + int_12 > 495)
		{
			return -88;
		}
		int num = (int_12 >> 8) * 2;
		int num2 = int_12 & 0xFF;
		int int_15 = 0;
		if (method_12(ref int_15, string_0) != 0)
		{
			return -92;
		}
		array[1] = 18;
		array[2] = (byte)num;
		array[3] = (byte)num2;
		array[4] = (byte)int_13;
		for (int i = 0; i <= 7; i++)
		{
			array[5 + i] = byte_24[i];
		}
		if (!method_10(int_15, array, 13))
		{
			CloseHandle(int_15);
			return -93;
		}
		if (!method_9(int_15, array2, int_13 + 1))
		{
			CloseHandle(int_15);
			return -94;
		}
		CloseHandle(int_15);
		if (array2[0] != 0)
		{
			return -83;
		}
		for (int j = 0; j < int_13; j++)
		{
			byte_23[j + int_14] = array2[j + 1];
		}
		return 0;
	}

	private int method_16(byte[] byte_23, int int_12, int int_13, byte[] byte_24, string string_0, int int_14)
	{
		byte[] array = new byte[512];
		byte[] array2 = new byte[512];
		if (int_13 > 255)
		{
			return -87;
		}
		if (int_12 + int_13 - 1 > 512 || int_12 < 0)
		{
			return -81;
		}
		int num = (int_12 >> 8) * 2;
		int num2 = int_12 & 0xFF;
		int int_15 = 0;
		if (method_12(ref int_15, string_0) != 0)
		{
			return -92;
		}
		array[1] = 19;
		array[2] = (byte)num;
		array[3] = (byte)num2;
		array[4] = (byte)int_13;
		for (int i = 0; i <= 7; i++)
		{
			array[5 + i] = byte_24[i];
		}
		for (int j = 0; j < int_13; j++)
		{
			array[13 + j] = byte_23[j + int_14];
		}
		if (!method_10(int_15, array, 13 + int_13))
		{
			CloseHandle(int_15);
			return -93;
		}
		if (!method_9(int_15, array2, 2))
		{
			CloseHandle(int_15);
			return -94;
		}
		CloseHandle(int_15);
		if (array2[0] != 0)
		{
			return -82;
		}
		return 0;
	}

	private int method_17(byte[] byte_23, byte[] byte_24, string string_0, int int_12)
	{
		byte[] array = new byte[25];
		byte[] array2 = new byte[25];
		int int_13 = 0;
		if (method_12(ref int_13, string_0) != 0)
		{
			return -92;
		}
		array[1] = 8;
		for (int i = 2; i <= 9; i++)
		{
			array[i] = byte_23[i - 2 + int_12];
		}
		if (!method_10(int_13, array, 9))
		{
			CloseHandle(int_13);
			return -93;
		}
		if (!method_9(int_13, array2, 9))
		{
			CloseHandle(int_13);
			return -93;
		}
		CloseHandle(int_13);
		for (int j = 0; j < 8; j++)
		{
			byte_24[j + int_12] = array2[j];
		}
		if (array2[8] != 85)
		{
			return -20;
		}
		return 0;
	}

	private int method_18(byte[] byte_23, byte byte_24, string string_0, short short_5)
	{
		byte[] array = new byte[25];
		byte[] array2 = new byte[25];
		int int_ = 0;
		if (method_12(ref int_, string_0) != 0)
		{
			return -92;
		}
		array[1] = 9;
		array[2] = byte_24;
		for (int i = 0; i < 8; i++)
		{
			array[3 + i] = byte_23[i + short_5];
		}
		if (!method_10(int_, array, 11))
		{
			CloseHandle(int_);
			return -93;
		}
		if (!method_9(int_, array2, 2))
		{
			CloseHandle(int_);
			return -94;
		}
		CloseHandle(int_);
		if (array2[0] != 0)
		{
			return -82;
		}
		return 0;
	}

	public int method_19(ref short short_5, string string_0)
	{
		int int_ = CreateSemaphoreA(0, 1, 1, "ex_sim");
		WaitForSingleObject(int_, 65535u);
		int result = method_13(ref short_5, string_0);
		ReleaseSemaphore(int_, 1, 0);
		CloseHandle(int_);
		return result;
	}

	public int method_20(ref int int_12, ref int int_13, string string_0)
	{
		int int_14 = CreateSemaphoreA(0, 1, 1, "ex_sim");
		WaitForSingleObject(int_14, 65535u);
		int result = method_14(ref int_12, ref int_13, string_0);
		ReleaseSemaphore(int_14, 1, 0);
		CloseHandle(int_14);
		return result;
	}

	public int method_21(byte[] byte_23, int int_12, int int_13, string string_0, string string_1, string string_2)
	{
		byte[] array = new byte[8];
		int int_14 = 0;
		if (int_12 + int_13 - 1 > 495 || int_12 < 0)
		{
			return -81;
		}
		int num = method_50(string_2, ref int_14);
		if (int_14 < 100)
		{
			int_14 = 16;
		}
		int_14 -= 8;
		if (num != 0)
		{
			return num;
		}
		method_25(string_0, string_1, array);
		int int_15 = CreateSemaphoreA(0, 1, 1, "ex_sim");
		WaitForSingleObject(int_15, 65535u);
		int num2 = int_12 % int_14;
		int num3 = int_14 - num2;
		if (num3 > int_13)
		{
			num3 = int_13;
		}
		if (num3 > 0)
		{
			int i;
			for (i = 0; i < num3 / int_14; i++)
			{
				num = method_16(byte_23, int_12 + i * int_14, int_14, array, string_2, int_14 * i);
				if (num != 0)
				{
					ReleaseSemaphore(int_15, 1, 0);
					CloseHandle(int_15);
					return method_44(byte_23, int_12, int_13, string_0, string_1, string_2);
				}
			}
			if (num3 - int_14 * i > 0)
			{
				num = method_16(byte_23, int_12 + i * int_14, num3 - i * int_14, array, string_2, int_14 * i);
				if (num != 0)
				{
					ReleaseSemaphore(int_15, 1, 0);
					CloseHandle(int_15);
					return method_44(byte_23, int_12, int_13, string_0, string_1, string_2);
				}
			}
		}
		int_13 -= num3;
		int_12 += num3;
		if (int_13 > 0)
		{
			int j;
			for (j = 0; j < int_13 / int_14; j++)
			{
				num = method_16(byte_23, int_12 + j * int_14, int_14, array, string_2, num3 + int_14 * j);
				if (num != 0)
				{
					ReleaseSemaphore(int_15, 1, 0);
					CloseHandle(int_15);
					return method_44(byte_23, int_12, int_13, string_0, string_1, string_2);
				}
			}
			if (int_13 - int_14 * j > 0)
			{
				num = method_16(byte_23, int_12 + j * int_14, int_13 - j * int_14, array, string_2, num3 + int_14 * j);
				if (num != 0)
				{
					ReleaseSemaphore(int_15, 1, 0);
					CloseHandle(int_15);
					return method_44(byte_23, int_12, int_13, string_0, string_1, string_2);
				}
			}
		}
		ReleaseSemaphore(int_15, 1, 0);
		CloseHandle(int_15);
		return num;
	}

	public int method_22(byte[] byte_23, short short_5, short short_6, string string_0, string string_1, string string_2)
	{
		byte[] array = new byte[8];
		int int_ = 0;
		if (short_5 + short_6 - 1 > 495 || short_5 < 0)
		{
			return -81;
		}
		int num = method_50(string_2, ref int_);
		if (int_ < 100)
		{
			int_ = 16;
		}
		if (num != 0)
		{
			return num;
		}
		method_25(string_0, string_1, array);
		int int_2 = CreateSemaphoreA(0, 1, 1, "ex_sim");
		WaitForSingleObject(int_2, 65535u);
		int i;
		for (i = 0; i < short_6 / int_; i++)
		{
			num = method_15(byte_23, short_5 + i * int_, int_, array, string_2, i * int_);
			if (num != 0)
			{
				ReleaseSemaphore(int_2, 1, 0);
				CloseHandle(int_2);
				return method_43(byte_23, short_5, short_6, string_0, string_1, string_2);
			}
		}
		if (short_6 - int_ * i > 0)
		{
			num = method_15(byte_23, short_5 + i * int_, short_6 - int_ * i, array, string_2, int_ * i);
			if (num != 0)
			{
				ReleaseSemaphore(int_2, 1, 0);
				CloseHandle(int_2);
				return method_43(byte_23, short_5, short_6, string_0, string_1, string_2);
			}
		}
		ReleaseSemaphore(int_2, 1, 0);
		CloseHandle(int_2);
		return num;
	}

	public int method_23(int int_12, ref string string_0)
	{
		int int_13 = CreateSemaphoreA(0, 1, 1, "ex_sim");
		WaitForSingleObject(int_13, 65535u);
		int result = method_11(int_12, ref string_0);
		ReleaseSemaphore(int_13, 1, 0);
		CloseHandle(int_13);
		return result;
	}

	private string method_24(string string_0)
	{
		int length = string_0.Length;
		for (int i = length; i <= 7; i++)
		{
			string_0 = "0" + string_0;
		}
		return string_0;
	}

	private void method_25(string string_0, string string_1, byte[] byte_23)
	{
		string_0 = method_24(string_0);
		string_1 = method_24(string_1);
		for (int i = 0; i <= 3; i++)
		{
			byte_23[i] = (byte)method_0(string_0.Substring(i * 2, 2));
		}
		for (int j = 0; j <= 3; j++)
		{
			byte_23[j + 4] = (byte)method_0(string_1.Substring(j * 2, 2));
		}
	}

	public int method_26(ref byte byte_23, int int_12, string string_0, string string_1, string string_2)
	{
		byte[] array = new byte[8];
		if (int_12 <= 495 && int_12 >= 0)
		{
			method_25(string_0, string_1, array);
			int int_13 = CreateSemaphoreA(0, 1, 1, "ex_sim");
			WaitForSingleObject(int_13, 65535u);
			int result = method_27(ref byte_23, int_12, array, string_2);
			ReleaseSemaphore(int_13, 1, 0);
			CloseHandle(int_13);
			return result;
		}
		return -81;
	}

	private int method_27(ref byte byte_23, int int_12, byte[] byte_24, string string_0)
	{
		byte[] array = new byte[25];
		byte[] array2 = new byte[25];
		int int_13 = 0;
		if (int_12 > 495 || int_12 < 0)
		{
			return -81;
		}
		byte b = 128;
		if (int_12 > 255)
		{
			b = 160;
			int_12 -= 256;
		}
		if (method_12(ref int_13, string_0) != 0)
		{
			return -92;
		}
		array[1] = 16;
		array[2] = b;
		array[3] = (byte)int_12;
		array[4] = (byte)int_12;
		for (int i = 0; i < 8; i++)
		{
			array[5 + i] = byte_24[i];
		}
		if (!method_10(int_13, array, 13))
		{
			CloseHandle(int_13);
			return -93;
		}
		if (!method_9(int_13, array2, 2))
		{
			CloseHandle(int_13);
			return -94;
		}
		CloseHandle(int_13);
		if (array2[0] != 83)
		{
			return -83;
		}
		byte_23 = array2[1];
		return 0;
	}

	public int method_28(byte byte_23, int int_12, string string_0, string string_1, string string_2)
	{
		byte[] array = new byte[8];
		if (int_12 <= 495 && int_12 >= 0)
		{
			method_25(string_0, string_1, array);
			int int_13 = CreateSemaphoreA(0, 1, 1, "ex_sim");
			WaitForSingleObject(int_13, 65535u);
			int result = method_29(byte_23, int_12, array, string_2);
			ReleaseSemaphore(int_13, 1, 0);
			CloseHandle(int_13);
			return result;
		}
		return -81;
	}

	private int method_29(byte byte_23, int int_12, byte[] byte_24, string string_0)
	{
		byte[] array = new byte[25];
		byte[] array2 = new byte[25];
		int int_13 = 0;
		if (int_12 > 511 || int_12 < 0)
		{
			return -81;
		}
		byte b = 64;
		if (int_12 > 255)
		{
			b = 96;
			int_12 -= 256;
		}
		if (method_12(ref int_13, string_0) != 0)
		{
			return -92;
		}
		array[1] = 17;
		array[2] = b;
		array[3] = (byte)int_12;
		array[4] = byte_23;
		for (int i = 0; i < 8; i++)
		{
			array[5 + i] = byte_24[i];
		}
		if (!method_10(int_13, array, 13))
		{
			CloseHandle(int_13);
			return -93;
		}
		if (!method_9(int_13, array2, 2))
		{
			CloseHandle(int_13);
			return -94;
		}
		CloseHandle(int_13);
		if (array2[1] != 1)
		{
			return -82;
		}
		return 0;
	}

	public int method_30(string string_0, string string_1, string string_2, string string_3, string string_4)
	{
		byte[] array = new byte[8];
		byte[] byte_ = new byte[8];
		method_25(string_0, string_1, array);
		method_25(string_2, string_3, byte_);
		int int_ = CreateSemaphoreA(0, 1, 1, "ex_sim");
		WaitForSingleObject(int_, 65535u);
		int result = method_16(byte_, 496, 8, array, string_4, 0);
		ReleaseSemaphore(int_, 1, 0);
		CloseHandle(int_);
		return result;
	}

	public int method_31(string string_0, string string_1, string string_2, string string_3, string string_4)
	{
		byte[] array = new byte[8];
		byte[] byte_ = new byte[8];
		method_25(string_0, string_1, array);
		method_25(string_2, string_3, byte_);
		int int_ = CreateSemaphoreA(0, 1, 1, "ex_sim");
		WaitForSingleObject(int_, 65535u);
		int result = method_16(byte_, 504, 8, array, string_4, 0);
		ReleaseSemaphore(int_, 1, 0);
		CloseHandle(int_);
		return result;
	}

	public int method_32(string string_0, int int_12, string string_1, string string_2, string string_3)
	{
		byte[] array = new byte[8];
		int int_13 = 0;
		if (int_12 < 0)
		{
			return -81;
		}
		int num = method_50(string_3, ref int_13);
		if (int_13 < 100)
		{
			int_13 = 16;
		}
		int_13 -= 8;
		if (num != 0)
		{
			return num;
		}
		method_25(string_1, string_2, array);
		int num2 = lstrlenA(string_0);
		byte[] byte_ = new byte[num2];
		RtlMoveMemory(byte_, string_0, num2);
		int num3 = int_12 + num2;
		if (num3 > 495)
		{
			return -47;
		}
		int int_14 = CreateSemaphoreA(0, 1, 1, "ex_sim");
		WaitForSingleObject(int_14, 65535u);
		int num4 = int_12 % int_13;
		int num5 = int_13 - num4;
		if (num5 > num2)
		{
			num5 = num2;
		}
		if (num5 > 0)
		{
			int i;
			for (i = 0; i < num5 / int_13; i++)
			{
				num = method_16(byte_, int_12 + i * int_13, int_13, array, string_3, i * int_13);
				if (num != 0)
				{
					ReleaseSemaphore(int_14, 1, 0);
					CloseHandle(int_14);
					return method_42(string_0, int_12, string_1, string_2, string_3);
				}
			}
			if (num5 - int_13 * i > 0)
			{
				num = method_16(byte_, int_12 + i * int_13, num5 - i * int_13, array, string_3, int_13 * i);
				if (num != 0)
				{
					ReleaseSemaphore(int_14, 1, 0);
					CloseHandle(int_14);
					return method_42(string_0, int_12, string_1, string_2, string_3);
				}
			}
		}
		num2 -= num5;
		int_12 += num5;
		if (num2 > 0)
		{
			int j;
			for (j = 0; j < num2 / int_13; j++)
			{
				num = method_16(byte_, int_12 + j * int_13, int_13, array, string_3, num5 + j * int_13);
				if (num != 0)
				{
					ReleaseSemaphore(int_14, 1, 0);
					CloseHandle(int_14);
					return num;
				}
			}
			if (num2 - int_13 * j > 0)
			{
				num = method_16(byte_, int_12 + j * int_13, num2 - j * int_13, array, string_3, num5 + int_13 * j);
				if (num != 0)
				{
					ReleaseSemaphore(int_14, 1, 0);
					CloseHandle(int_14);
					return num;
				}
			}
		}
		ReleaseSemaphore(int_14, 1, 0);
		CloseHandle(int_14);
		return num;
	}

	public int method_33(ref string string_0, int int_12, int int_13, string string_1, string string_2, string string_3)
	{
		byte[] array = new byte[8];
		int int_14 = 0;
		byte[] byte_ = new byte[int_13];
		method_25(string_1, string_2, array);
		if (int_12 < 0)
		{
			return -81;
		}
		int num = method_50(string_3, ref int_14);
		if (int_14 < 100)
		{
			int_14 = 16;
		}
		if (num != 0)
		{
			return num;
		}
		int num2 = int_12 + int_13;
		if (num2 > 495)
		{
			return -47;
		}
		int int_15 = CreateSemaphoreA(0, 1, 1, "ex_sim");
		WaitForSingleObject(int_15, 65535u);
		int i;
		for (i = 0; i < int_13 / int_14; i++)
		{
			num = method_15(byte_, int_12 + i * int_14, int_14, array, string_3, i * int_14);
			if (num != 0)
			{
				ReleaseSemaphore(int_15, 1, 0);
				CloseHandle(int_15);
				return method_41(ref string_0, int_12, int_13, string_1, string_2, string_3);
			}
		}
		if (int_13 - int_14 * i > 0)
		{
			num = method_15(byte_, int_12 + i * int_14, int_13 - int_14 * i, array, string_3, int_14 * i);
			if (num != 0)
			{
				ReleaseSemaphore(int_15, 1, 0);
				CloseHandle(int_15);
				return method_41(ref string_0, int_12, int_13, string_1, string_2, string_3);
			}
		}
		ReleaseSemaphore(int_15, 1, 0);
		CloseHandle(int_15);
		StringBuilder stringBuilder = new StringBuilder("", int_13);
		for (i = 0; i < int_13; i++)
		{
			stringBuilder.Append(0);
		}
		RtlMoveMemory_1(stringBuilder, byte_, int_13);
		string_0 = stringBuilder.ToString();
		return num;
	}

	public int method_34(string string_0, string string_1)
	{
		byte[] byte_ = new byte[16];
		method_1(string_0, ref byte_);
		int int_ = CreateSemaphoreA(0, 1, 1, "ex_sim");
		WaitForSingleObject(int_, 65535u);
		int num = method_18(byte_, 0, string_1, 8);
		if (num == 0)
		{
			num = method_18(byte_, 1, string_1, 0);
		}
		ReleaseSemaphore(int_, 1, 0);
		CloseHandle(int_);
		return num;
	}

	public int method_35(byte[] byte_23, byte[] byte_24, string string_0)
	{
		int int_ = CreateSemaphoreA(0, 1, 1, "ex_sim");
		WaitForSingleObject(int_, 65535u);
		int result = method_17(byte_23, byte_24, string_0, 0);
		ReleaseSemaphore(int_, 1, 0);
		CloseHandle(int_);
		return result;
	}

	public int method_36(string string_0, ref string string_1, string string_2)
	{
		int num = 0;
		int num2 = lstrlenA(string_0) + 1;
		int int_ = num2;
		if (num2 < 8)
		{
			num2 = 8;
		}
		byte[] array = new byte[num2];
		byte[] array2 = new byte[num2];
		RtlMoveMemory(array, string_0, int_);
		array.CopyTo(array2, 0);
		int int_2 = CreateSemaphoreA(0, 1, 1, "ex_sim");
		WaitForSingleObject(int_2, 65535u);
		for (int i = 0; i <= num2 - 8; i += 8)
		{
			num = method_17(array, array2, string_2, i);
			if (num != 0)
			{
				break;
			}
		}
		ReleaseSemaphore(int_2, 1, 0);
		CloseHandle(int_2);
		string_1 = "";
		for (int j = 0; j < num2; j++)
		{
			string_1 += array2[j].ToString("X2");
		}
		return num;
	}

	public int method_37(string string_0)
	{
		int int_ = CreateSemaphoreA(0, 1, 1, "ex_sim");
		WaitForSingleObject(int_, 65535u);
		int result = method_38(string_0);
		ReleaseSemaphore(int_, 1, 0);
		CloseHandle(int_);
		return result;
	}

	private int method_38(string string_0)
	{
		byte[] array = new byte[25];
		byte[] array2 = new byte[25];
		int int_ = 0;
		if (method_12(ref int_, string_0) != 0)
		{
			return -92;
		}
		array[1] = 32;
		if (!method_10(int_, array, 2))
		{
			CloseHandle(int_);
			return -93;
		}
		if (!method_9(int_, array2, 2))
		{
			CloseHandle(int_);
			return -93;
		}
		CloseHandle(int_);
		if (array2[0] != 0)
		{
			return -82;
		}
		return 0;
	}

	public int method_39(ref short short_5, string string_0)
	{
		int int_ = CreateSemaphoreA(0, 1, 1, "ex_sim");
		WaitForSingleObject(int_, 65535u);
		int result = method_40(ref short_5, string_0);
		ReleaseSemaphore(int_, 1, 0);
		CloseHandle(int_);
		return result;
	}

	private int method_40(ref short short_5, string string_0)
	{
		byte[] array = new byte[25];
		byte[] array2 = new byte[25];
		int int_ = 0;
		if (method_12(ref int_, string_0) != 0)
		{
			return -92;
		}
		array[1] = 5;
		if (!method_10(int_, array, 1))
		{
			CloseHandle(int_);
			return -93;
		}
		if (!method_9(int_, array2, 1))
		{
			CloseHandle(int_);
			return -93;
		}
		CloseHandle(int_);
		short_5 = array2[0];
		return 0;
	}

	private int method_41(ref string string_0, int int_12, int int_13, string string_1, string string_2, string string_3)
	{
		int num = 0;
		byte[] array = new byte[int_13];
		for (int i = 0; i < int_13; i++)
		{
			num = method_26(ref array[i], int_12 + i, string_1, string_2, string_3);
			if (num != 0)
			{
				return num;
			}
		}
		StringBuilder stringBuilder = new StringBuilder("", int_13);
		for (int j = 0; j < int_13; j++)
		{
			stringBuilder.Append(0);
		}
		RtlMoveMemory_1(stringBuilder, array, int_13);
		string_0 = stringBuilder.ToString();
		return num;
	}

	private int method_42(string string_0, int int_12, string string_1, string string_2, string string_3)
	{
		int num = 0;
		int num2 = lstrlenA(string_0);
		byte[] array = new byte[num2];
		RtlMoveMemory(array, string_0, num2);
		for (int i = 0; i < num2; i++)
		{
			num = method_28(array[i], int_12 + i, string_1, string_2, string_3);
			if (num != 0)
			{
				return num;
			}
		}
		return num;
	}

	private int method_43(byte[] byte_23, int int_12, int int_13, string string_0, string string_1, string string_2)
	{
		int num = 0;
		for (int i = 0; i < int_13; i++)
		{
			num = method_26(ref byte_23[i], int_12 + i, string_0, string_1, string_2);
			if (num != 0)
			{
				return num;
			}
		}
		return num;
	}

	private int method_44(byte[] byte_23, int int_12, int int_13, string string_0, string string_1, string string_2)
	{
		int num = 0;
		for (int i = 0; i < int_13; i++)
		{
			num = method_28(byte_23[i], int_12 + i, string_0, string_1, string_2);
			if (num != 0)
			{
				return num;
			}
		}
		return num;
	}

	public int method_45(bool bool_1, string string_0)
	{
		int int_ = CreateSemaphoreA(0, 1, 1, "ex_sim");
		WaitForSingleObject(int_, 65535u);
		int result = method_46(bool_1, string_0);
		ReleaseSemaphore(int_, 1, 0);
		CloseHandle(int_);
		return result;
	}

	private int method_46(bool bool_1, string string_0)
	{
		byte[] array = new byte[25];
		byte[] array2 = new byte[25];
		int int_ = 0;
		if (method_12(ref int_, string_0) != 0)
		{
			return -92;
		}
		array[1] = 85;
		if (bool_1)
		{
			array[2] = 0;
		}
		else
		{
			array[2] = byte.MaxValue;
		}
		if (!method_10(int_, array, 3))
		{
			CloseHandle(int_);
			return -93;
		}
		if (!method_9(int_, array2, 1))
		{
			CloseHandle(int_);
			return -93;
		}
		CloseHandle(int_);
		if (array2[0] != 0)
		{
			return -82;
		}
		return 0;
	}

	public int method_47(string string_0)
	{
		int int_ = CreateSemaphoreA(0, 1, 1, "ex_sim");
		WaitForSingleObject(int_, 65535u);
		int result = method_48(string_0);
		ReleaseSemaphore(int_, 1, 0);
		CloseHandle(int_);
		return result;
	}

	private int method_48(string string_0)
	{
		byte[] array = new byte[25];
		byte[] array2 = new byte[25];
		int int_ = 0;
		if (method_12(ref int_, string_0) != 0)
		{
			return -92;
		}
		array[1] = 86;
		if (!method_10(int_, array, 3))
		{
			CloseHandle(int_);
			return -93;
		}
		if (!method_9(int_, array2, 1))
		{
			CloseHandle(int_);
			return -93;
		}
		CloseHandle(int_);
		if (array2[0] != 0)
		{
			return -82;
		}
		return 0;
	}

	private int method_49(int int_12, ref int int_13)
	{
		IntPtr intptr_ = IntPtr.Zero;
		HIDP_CAPS hidp_CAPS_ = default(HIDP_CAPS);
		if (!HidD_GetPreparsedData(int_12, ref intptr_))
		{
			return -93;
		}
		if (HidP_GetCaps(intptr_, ref hidp_CAPS_) <= 0)
		{
			HidD_FreePreparsedData(intptr_);
			return -93;
		}
		HidD_FreePreparsedData(intptr_);
		int_13 = hidp_CAPS_.FeatureReportByteLength - 5;
		return 0;
	}

	private int method_50(string string_0, ref int int_12)
	{
		int int_13 = 0;
		if (method_12(ref int_13, string_0) != 0)
		{
			return -92;
		}
		int result = method_49(int_13, ref int_12);
		CloseHandle(int_13);
		return result;
	}

	private int method_51(byte[] byte_23, byte[] byte_24, byte[] byte_25, byte[] byte_26, string string_0)
	{
		byte[] array = new byte[256];
		byte[] array2 = new byte[25];
		int int_ = 0;
		if (method_12(ref int_, string_0) != 0)
		{
			return -92;
		}
		array[1] = 50;
		for (int i = 0; i < 32; i++)
		{
			array[2 + i] = byte_23[i];
			array[2 + i + 32] = byte_24[i];
			array[2 + i + 64] = byte_25[i];
		}
		for (int j = 0; j < 80; j++)
		{
			array[2 + j + 96] = byte_26[j];
		}
		if (!method_10(int_, array, 178))
		{
			CloseHandle(int_);
			return -93;
		}
		if (!method_9(int_, array2, 2))
		{
			CloseHandle(int_);
			return -94;
		}
		CloseHandle(int_);
		if (array2[0] != 32)
		{
			return -50;
		}
		return 0;
	}

	private int method_52(byte[] byte_23, byte[] byte_24, string string_0)
	{
		byte[] array = new byte[256];
		byte[] array2 = new byte[256];
		int int_ = 0;
		array2[0] = 251;
		if (method_12(ref int_, string_0) != 0)
		{
			return -92;
		}
		array[1] = 55;
		if (!method_10(int_, array, 2))
		{
			CloseHandle(int_);
			return -93;
		}
		if (!method_9(int_, array2, 98))
		{
			CloseHandle(int_);
			return -94;
		}
		CloseHandle(int_);
		if (array2[0] != 32)
		{
			return -21;
		}
		for (int i = 0; i < 32; i++)
		{
			byte_23[i] = array2[1 + i];
		}
		for (int j = 0; j < 65; j++)
		{
			byte_24[j] = array2[33 + j];
		}
		return 0;
	}

	private int method_53(byte[] byte_23, string string_0)
	{
		byte[] array = new byte[25];
		byte[] array2 = new byte[25];
		int int_ = 0;
		if (method_12(ref int_, string_0) != 0)
		{
			return -92;
		}
		array[1] = 83;
		if (!method_10(int_, array, 1))
		{
			CloseHandle(int_);
			return -93;
		}
		if (!method_9(int_, array2, 17))
		{
			CloseHandle(int_);
			return -93;
		}
		CloseHandle(int_);
		if (array2[0] != 32)
		{
			return -50;
		}
		for (int i = 0; i < 16; i++)
		{
			byte_23[i] = array2[1 + i];
		}
		return 0;
	}

	private int method_54(byte[] byte_23, byte[] byte_24, byte[] byte_25, string string_0)
	{
		byte[] array = new byte[256];
		byte[] array2 = new byte[256];
		int int_ = 0;
		if (method_12(ref int_, string_0) != 0)
		{
			return -92;
		}
		array[1] = 51;
		if (!method_10(int_, array, 2))
		{
			CloseHandle(int_);
			return -93;
		}
		if (!method_9(int_, array2, 146))
		{
			CloseHandle(int_);
			return -94;
		}
		CloseHandle(int_);
		if (array2[0] != 32)
		{
			return -50;
		}
		for (int i = 0; i < 32; i++)
		{
			byte_23[i] = array2[1 + i];
			byte_24[i] = array2[33 + i];
		}
		for (int j = 0; j < 80; j++)
		{
			byte_25[j] = array2[65 + j];
		}
		return 0;
	}

	private int method_55(string string_0, string string_1, string string_2)
	{
		byte[] array = new byte[256];
		byte[] array2 = new byte[256];
		int int_ = 0;
		if (method_12(ref int_, string_2) != 0)
		{
			return -92;
		}
		array[1] = 54;
		byte[] array3 = new byte[16];
		RtlMoveMemory(array3, string_0, 16);
		byte[] array4 = new byte[16];
		RtlMoveMemory(array4, string_1, 16);
		for (int i = 0; i < 16; i++)
		{
			array[2 + i] = array3[i];
			array[18 + i] = array4[i];
		}
		if (!method_10(int_, array, 34))
		{
			CloseHandle(int_);
			return -93;
		}
		if (!method_9(int_, array2, 2))
		{
			CloseHandle(int_);
			return -94;
		}
		CloseHandle(int_);
		if (array2[0] != 32)
		{
			return -50;
		}
		if (array2[1] != 32)
		{
			return -24;
		}
		return 0;
	}

	private int method_56(byte[] byte_23, byte[] byte_24, byte byte_25, string string_0)
	{
		byte[] array = new byte[256];
		byte[] array2 = new byte[256];
		int int_ = 0;
		if (method_12(ref int_, string_0) != 0)
		{
			return -92;
		}
		array[1] = 52;
		array[2] = byte_25;
		for (int i = 0; i < byte_25; i++)
		{
			array[3 + i] = byte_23[i];
		}
		if (!method_10(int_, array, byte_25 + 1 + 2))
		{
			CloseHandle(int_);
			return -93;
		}
		if (!method_9(int_, array2, byte_25 + 97 + 3))
		{
			CloseHandle(int_);
			return -94;
		}
		CloseHandle(int_);
		if (array2[0] != 32)
		{
			return -50;
		}
		if (array2[1] == 0)
		{
			return -22;
		}
		for (int j = 0; j < byte_25 + 97; j++)
		{
			byte_24[j] = array2[2 + j];
		}
		return 0;
	}

	private int method_57(byte[] byte_23, byte[] byte_24, byte byte_25, string string_0, string string_1)
	{
		byte[] array = new byte[256];
		byte[] array2 = new byte[256];
		int int_ = 0;
		if (method_12(ref int_, string_1) != 0)
		{
			return -92;
		}
		array[1] = 53;
		byte[] array3 = new byte[16];
		RtlMoveMemory(array3, string_0, 16);
		for (int i = 0; i < 16; i++)
		{
			array[2 + i] = array3[i];
		}
		array[18] = byte_25;
		for (int j = 0; j < byte_25; j++)
		{
			array[19 + j] = byte_23[j];
		}
		if (!method_10(int_, array, byte_25 + 1 + 2 + 16))
		{
			CloseHandle(int_);
			return -93;
		}
		if (!method_9(int_, array2, byte_25 - 97 + 4))
		{
			CloseHandle(int_);
			return -94;
		}
		CloseHandle(int_);
		if (array2[2] != 32)
		{
			return -24;
		}
		if (array2[1] == 0)
		{
			return -22;
		}
		if (array2[0] != 32)
		{
			return -50;
		}
		for (int k = 0; k < byte_25 - 97; k++)
		{
			byte_24[k] = array2[3 + k];
		}
		return 0;
	}

	private int method_58(byte[] byte_23, byte[] byte_24, string string_0, string string_1)
	{
		byte[] array = new byte[256];
		byte[] array2 = new byte[256];
		int int_ = 0;
		if (method_12(ref int_, string_1) != 0)
		{
			return -92;
		}
		array[1] = 81;
		byte[] array3 = new byte[16];
		RtlMoveMemory(array3, string_0, 16);
		for (int i = 0; i < 16; i++)
		{
			array[2 + i] = array3[i];
		}
		for (int j = 0; j < 32; j++)
		{
			array[18 + j] = byte_23[j];
		}
		if (!method_10(int_, array, 50))
		{
			CloseHandle(int_);
			return -93;
		}
		if (!method_9(int_, array2, 67))
		{
			CloseHandle(int_);
			return -94;
		}
		CloseHandle(int_);
		if (array2[1] != 32)
		{
			return -24;
		}
		if (array2[0] != 32)
		{
			return -50;
		}
		for (int k = 0; k < 64; k++)
		{
			byte_24[k] = array2[2 + k];
		}
		return 0;
	}

	private int method_59(byte[] byte_23, byte[] byte_24, string string_0, string string_1)
	{
		byte[] array = new byte[256];
		byte[] array2 = new byte[256];
		int int_ = 0;
		if (method_12(ref int_, string_1) != 0)
		{
			return -92;
		}
		array[1] = 83;
		byte[] array3 = new byte[16];
		RtlMoveMemory(array3, string_0, 16);
		for (int i = 0; i < 16; i++)
		{
			array[2 + i] = array3[i];
		}
		for (int j = 0; j < 32; j++)
		{
			array[18 + j] = byte_23[j];
		}
		if (!method_10(int_, array, 50))
		{
			CloseHandle(int_);
			return -93;
		}
		if (!method_9(int_, array2, 67))
		{
			CloseHandle(int_);
			return -94;
		}
		CloseHandle(int_);
		if (array2[1] != 32)
		{
			return -24;
		}
		if (array2[0] != 32)
		{
			return -50;
		}
		for (int k = 0; k < 64; k++)
		{
			byte_24[k] = array2[2 + k];
		}
		return 0;
	}

	private int method_60(byte[] byte_23, byte[] byte_24, ref bool bool_1, string string_0)
	{
		byte[] array = new byte[256];
		byte[] array2 = new byte[256];
		int int_ = 0;
		if (method_12(ref int_, string_0) != 0)
		{
			return -92;
		}
		array[1] = 82;
		for (int i = 0; i < 32; i++)
		{
			array[2 + i] = byte_23[i];
		}
		for (int j = 0; j < 64; j++)
		{
			array[34 + j] = byte_24[j];
		}
		if (!method_10(int_, array, 98))
		{
			CloseHandle(int_);
			return -93;
		}
		if (!method_9(int_, array2, 3))
		{
			CloseHandle(int_);
			return -94;
		}
		CloseHandle(int_);
		bool_1 = array2[1] != 0;
		if (array2[0] != 32)
		{
			return -50;
		}
		return 0;
	}

	private string method_61(byte[] byte_23, int int_12)
	{
		string text = "";
		for (int i = 0; i < int_12; i++)
		{
			text += byte_23[i].ToString("X2");
		}
		return text;
	}

	public int method_62(ref string string_0, ref string string_1, ref string string_2, string string_3)
	{
		byte[] byte_ = new byte[32];
		byte[] array = new byte[65];
		int int_ = CreateSemaphoreA(0, 1, 1, "ex_sim");
		WaitForSingleObject(int_, 65535u);
		int result = method_52(byte_, array, string_3);
		ReleaseSemaphore(int_, 1, 0);
		CloseHandle(int_);
		string_0 = method_61(byte_, 32);
		string_1 = "";
		string_2 = "";
		for (int i = 0; i < 32; i++)
		{
			string_1 += array[i + 1].ToString("X2");
			string_2 += array[i + 1 + 32].ToString("X2");
		}
		return result;
	}

	public int method_63(string string_0, string string_1, string string_2, string string_3, string string_4)
	{
		byte[] byte_ = new byte[32];
		byte[] byte_2 = new byte[32];
		byte[] byte_3 = new byte[32];
		byte[] array = new byte[80];
		method_1(string_0, ref byte_);
		method_1(string_1, ref byte_2);
		method_1(string_2, ref byte_3);
		RtlMoveMemory(array, string_3, 80);
		int int_ = CreateSemaphoreA(0, 1, 1, "ex_sim");
		WaitForSingleObject(int_, 65535u);
		int result = method_51(byte_, byte_2, byte_3, array, string_4);
		ReleaseSemaphore(int_, 1, 0);
		CloseHandle(int_);
		return result;
	}

	public int method_64(ref string string_0, ref string string_1, ref string string_2, string string_3)
	{
		byte[] byte_ = new byte[32];
		byte[] array = new byte[32];
		byte[] array2 = new byte[80];
		int int_ = CreateSemaphoreA(0, 1, 1, "ex_sim");
		WaitForSingleObject(int_, 65535u);
		int result = method_54(byte_, array, array2, string_3);
		ReleaseSemaphore(int_, 1, 0);
		CloseHandle(int_);
		string_0 = method_61(byte_, 32);
		string_1 = method_61(array, 32);
		StringBuilder stringBuilder = new StringBuilder("", 80);
		RtlMoveMemory_1(stringBuilder, array2, 80);
		string_2 = stringBuilder.ToString();
		return result;
	}

	public int method_65(ref string string_0, string string_1)
	{
		byte[] byte_ = new byte[16];
		int int_ = CreateSemaphoreA(0, 1, 1, "ex_sim");
		WaitForSingleObject(int_, 65535u);
		int result = method_53(byte_, string_1);
		ReleaseSemaphore(int_, 1, 0);
		CloseHandle(int_);
		string_0 = method_61(byte_, 16);
		return result;
	}

	public int method_66(byte[] byte_23, byte[] byte_24, int int_12, string string_0)
	{
		int num = 0;
		int num2 = 0;
		int num3 = 0;
		byte[] array = new byte[225];
		byte[] array2 = new byte[225];
		int int_13 = CreateSemaphoreA(0, 1, 1, "ex_sim");
		WaitForSingleObject(int_13, 65535u);
		while (int_12 > 0)
		{
			int num4 = ((int_12 <= 128) ? int_12 : 128);
			for (int i = 0; i < num4; i++)
			{
				array[i] = byte_23[num2 + i];
			}
			num = method_56(array, array2, (byte)num4, string_0);
			for (int j = 0; j < num4 + 97; j++)
			{
				byte_24[num3 + j] = array2[j];
			}
			if (num != 0)
			{
				break;
			}
			int_12 -= 128;
			num2 += 128;
			num3 += 225;
		}
		ReleaseSemaphore(int_13, 1, 0);
		CloseHandle(int_13);
		return num;
	}

	public int method_67(byte[] byte_23, byte[] byte_24, int int_12, string string_0, string string_1)
	{
		int num = 0;
		int num2 = 0;
		int num3 = 0;
		byte[] array = new byte[225];
		byte[] array2 = new byte[225];
		int int_13 = CreateSemaphoreA(0, 1, 1, "ex_sim");
		WaitForSingleObject(int_13, 65535u);
		while (int_12 > 0)
		{
			int num4 = ((int_12 <= 225) ? int_12 : 225);
			for (int i = 0; i < num4; i++)
			{
				array[i] = byte_23[num2 + i];
			}
			num = method_57(byte_23, byte_24, (byte)num4, string_0, string_1);
			for (int j = 0; j < num4 - 97; j++)
			{
				byte_24[num3 + j] = array2[j];
			}
			if (num != 0)
			{
				break;
			}
			int_12 -= 225;
			num2 += 225;
			num3 += 128;
		}
		ReleaseSemaphore(int_13, 1, 0);
		CloseHandle(int_13);
		return num;
	}

	public int method_68(string string_0, ref string string_1, string string_2)
	{
		int num = 0;
		int num2 = 0;
		byte[] array = new byte[225];
		byte[] array2 = new byte[225];
		int num3 = lstrlenA(string_0) + 1;
		int num4 = (num3 / 128 + 1) * 97 + num3;
		byte[] array3 = new byte[num4];
		byte[] array4 = new byte[num3];
		RtlMoveMemory(array4, string_0, num3);
		int num5 = 0;
		int int_ = CreateSemaphoreA(0, 1, 1, "ex_sim");
		WaitForSingleObject(int_, 65535u);
		while (num3 > 0)
		{
			int num6 = ((num3 <= 128) ? num3 : 128);
			for (int i = 0; i < num6; i++)
			{
				array[i] = array4[num + i];
			}
			num5 = method_56(array, array2, (byte)num6, string_2);
			for (int j = 0; j < num6 + 97; j++)
			{
				array3[num2 + j] = array2[j];
			}
			if (num5 != 0)
			{
				break;
			}
			num3 -= 128;
			num += 128;
			num2 += 225;
		}
		string_1 = method_61(array3, num4);
		ReleaseSemaphore(int_, 1, 0);
		CloseHandle(int_);
		return num5;
	}

	public int method_69(string string_0, ref string string_1, string string_2, string string_3)
	{
		int num = 0;
		int num2 = 0;
		byte[] array = new byte[225];
		byte[] array2 = new byte[225];
		int num3 = lstrlenA(string_0) / 2;
		int num4 = num3 - (num3 / 225 + 1) * 97;
		byte[] byte_ = new byte[num3];
		byte[] array3 = new byte[num4];
		int num5 = 0;
		method_1(string_0, ref byte_);
		int int_ = CreateSemaphoreA(0, 1, 1, "ex_sim");
		WaitForSingleObject(int_, 65535u);
		while (num3 > 0)
		{
			int num6 = ((num3 <= 225) ? num3 : 225);
			for (int i = 0; i < num6; i++)
			{
				array[i] = byte_[num + i];
			}
			num5 = method_57(array, array2, (byte)num6, string_2, string_3);
			for (int j = 0; j < num6 - 97; j++)
			{
				array3[num2 + j] = array2[j];
			}
			if (num5 != 0)
			{
				break;
			}
			num3 -= 225;
			num += 225;
			num2 += 128;
		}
		ReleaseSemaphore(int_, 1, 0);
		CloseHandle(int_);
		StringBuilder stringBuilder = new StringBuilder("", num4);
		RtlMoveMemory_1(stringBuilder, array3, num4);
		string_1 = stringBuilder.ToString();
		return num5;
	}

	public int method_70(string string_0, string string_1, string string_2)
	{
		int int_ = CreateSemaphoreA(0, 1, 1, "ex_sim");
		WaitForSingleObject(int_, 65535u);
		int result = method_55(string_0, string_1, string_2);
		ReleaseSemaphore(int_, 1, 0);
		CloseHandle(int_);
		return result;
	}
}
