using System;
using System.Runtime.InteropServices;

namespace IBrainChrom2018;

[Serializable]
[StructLayout(LayoutKind.Sequential)]
public class TempCtrlAreaTable
{
	public TempCtrlAreaRow[] tempList = new TempCtrlAreaRow[8];

	public TempCtrlAreaTable()
	{
		for (int i = 0; i < tempList.Length; i++)
		{
			tempList[i] = new TempCtrlAreaRow();
		}
	}

	public void Byte2Name(byte[] byte_0)
	{
		if (byte_0.Length == 72)
		{
			int num = 0;
			for (int i = 0; i < 6; i++)
			{
				tempList[i].Byte2Name(byte_0, num, bool_0: true);
				num += 6;
			}
			for (int j = 0; j < 6; j++)
			{
				tempList[j].Byte2Name(byte_0, num, bool_0: false);
				num += 6;
			}
		}
		else if (byte_0.Length == 96)
		{
			int num2 = 0;
			for (int k = 0; k < tempList.Length; k++)
			{
				tempList[k].Byte2Name(byte_0, num2, bool_0: true);
				num2 += 6;
			}
			for (int l = 0; l < tempList.Length; l++)
			{
				tempList[l].Byte2Name(byte_0, num2, bool_0: false);
				num2 += 6;
			}
		}
	}

	public byte[] Name2Byte(int rowCount)
	{
		byte[] byte_ = new byte[0];
		for (int i = 0; i < rowCount; i++)
		{
			IBrainConvert.ArrayCopy(ref byte_, tempList[i].CnName2Byte());
		}
		for (int j = 0; j < rowCount; j++)
		{
			IBrainConvert.ArrayCopy(ref byte_, tempList[j].EnName2Byte());
		}
		return byte_;
	}
}
