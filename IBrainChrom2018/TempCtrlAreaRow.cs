using System;
using System.Runtime.InteropServices;
using System.Text;

namespace IBrainChrom2018;

[Serializable]
[StructLayout(LayoutKind.Sequential)]
public class TempCtrlAreaRow
{
	public string strNameCn = "";

	public string strNameEn = "";

	public void Byte2Name(byte[] byte_0, int int_0, bool bool_0)
	{
		if (bool_0)
		{
			strNameCn = Encoding.Default.GetString(byte_0, int_0, 6);
		}
		else
		{
			strNameEn = Encoding.ASCII.GetString(byte_0, int_0, 6);
		}
	}

	public byte[] CnName2Byte()
	{
		byte[] array = Encoding.Default.GetBytes(strNameCn);
		Array.Resize(ref array, 6);
		for (int i = 0; i < array.Length; i++)
		{
			if (array[i] == 0)
			{
				array[i] = 32;
			}
		}
		return array;
	}

	public byte[] EnName2Byte()
	{
		byte[] array = Encoding.ASCII.GetBytes(strNameEn);
		Array.Resize(ref array, 6);
		for (int i = 0; i < array.Length; i++)
		{
			if (array[i] == 0)
			{
				array[i] = 32;
			}
		}
		return array;
	}
}
