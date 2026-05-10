using System;
using System.Runtime.InteropServices;
using System.Text;

namespace IBrainChrom2018;

public static class ET99_API
{
	internal const string string_0 = "ffffffff";

	internal const int int_0 = 1;

	internal const int int_1 = 2;

	internal const int int_2 = 6;

	internal const int int_3 = 3;

	internal const int int_4 = 4;

	internal const int int_5 = 240;

	internal const int int_6 = 15;

	internal const int int_7 = 255;

	internal const int int_8 = 0;

	internal const int int_9 = 5;

	internal const int int_10 = 7;

	internal const int int_11 = 1;

	internal const int int_12 = 0;

	internal const int int_13 = 1;

	internal const int int_14 = 0;

	[DllImport("GPC_API.dll")]
	public static extern uint et_ChangeUserPIN(IntPtr hHandle, byte[] pucOldPIN, byte[] pucNewPIN);

	[DllImport("GPC_API.dll")]
	public static extern uint et_CloseToken(IntPtr hHandle);

	[DllImport("GPC_API.dll")]
	public static extern uint et_FindToken(byte[] byte_0, out int count);

	[DllImport("GPC_API.dll")]
	public static extern uint et_GenPID(IntPtr hHandle, int seedlen, byte[] pucseed, StringBuilder stringBuilder_0);

	[DllImport("GPC_API.dll")]
	public static extern uint et_GenRandom(IntPtr hHandle, ref byte[] pucRandBuf);

	[DllImport("GPC_API.dll")]
	public static extern uint et_GenSOPIN(IntPtr hHandle, int seedlen, byte[] pucseed, StringBuilder pucNewSoPin);

	[DllImport("GPC_API.dll")]
	public static extern uint et_GetSN(IntPtr hHandle, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 32)] byte[] pucSN);

	[DllImport("GPC_API.dll")]
	public static extern uint et_HMAC_MD5(IntPtr hHandle, int Keyid, int textLen, byte[] pucText, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 16)] byte[] digest);

	[DllImport("GPC_API.dll")]
	public static extern uint et_OpenToken(ref IntPtr hHandle, byte[] byte_0, int index);

	[DllImport("GPC_API.dll")]
	public static extern uint et_Read(IntPtr hHandle, ushort offset, int int_15, byte[] pucReadBuf);

	[DllImport("GPC_API.dll")]
	public static extern uint et_ResetPIN(IntPtr hHandle, byte[] pucSoPin);

	[DllImport("GPC_API.dll")]
	public static extern uint et_ResetSecurityState(IntPtr hHandle);

	[DllImport("GPC_API.dll")]
	public static extern uint et_SetKey(IntPtr hHandle, int Keyid, byte[] pucKeyBuf);

	[DllImport("GPC_API.dll")]
	public static extern uint et_SetupToken(IntPtr hHandle, byte bSoPINRetries, byte bUserPINRetries, byte bUserReadOnly, byte bBack);

	[DllImport("GPC_API.dll")]
	public static extern uint et_TurnOffLED(IntPtr hHandle);

	[DllImport("GPC_API.dll")]
	public static extern uint et_TurnOnLED(IntPtr hHandle);

	[DllImport("GPC_API.dll")]
	public static extern uint et_Verify(IntPtr hHandle, int Flags, byte[] pucPIN);

	internal static void smethod_0()
	{
		throw new Exception("The method or operation is not implemented.");
	}

	[DllImport("GPC_API.dll")]
	public static extern uint et_Write(IntPtr hHandle, ushort offset, int int_15, byte[] pucWriteBuf);

	[DllImport("GPC_API.dll")]
	public static extern uint MD5_HMAC(byte[] pucText, byte ulText_Len, byte[] pucKey, byte ulKey_Len, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 32)] byte[] pucToenKey, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 16)] byte[] pucDigest);

	public static string ShowResultText(uint result)
	{
		switch (result)
		{
		case 0u:
			return "操作成功！";
		case 1u:
			return "访问被拒绝，权限不够！";
		case 2u:
			return "通讯错误，没有打开设备 ！";
		case 3u:
			return "无效的参数，参数出错 ！";
		case 4u:
			return "没有设置 PID ！";
		case 5u:
			return "打开指定的设备失败！";
		case 6u:
			return "硬件错误！";
		case 7u:
			return "未知错误！";
		default:
			if (result == 240)
			{
				return "PIN码错误！设备已经被锁死。";
			}
			if (result == 255)
			{
				return "PIN码错误！请核实。";
			}
			if (result > 240 && result < 255)
			{
				return "PIN码验证错误！剩余重试次数：" + (result - 240);
			}
			return "未知代码！";
		}
	}
}
