using System;
using System.Runtime.InteropServices;

namespace IBrainChrom2018;

[Serializable]
[StructLayout(LayoutKind.Sequential)]
public class NetWorkDeviceRow : IArrayBase
{
	public byte idx;

	public int iDetector;

	public byte[] version = new byte[15];

	public void SetNetDeivceVersion(byte[] byte_2, int idx)
	{
		this.idx = byte_2[idx];
		if (idx + 1 + 15 <= byte_2.Length)
		{
			Array.Copy(byte_2, idx + 1, version, 0, 15);
		}
	}

	public override string ToString()
	{
		iDetector = 0;
		string text;
		if (idx == 0)
		{
			text = "显示屏";
		}
		if (iDetector == 0)
		{
			if (idx == 64)
			{
				text = "FID1";
			}
			else if (idx == 65)
			{
				text = "FID2";
			}
		}
		else if (iDetector == 1)
		{
			if (idx == 64)
			{
				text = "FID1";
			}
			else if (idx == 65)
			{
				text = "PDD2";
			}
		}
		else if (iDetector == 2)
		{
			if (idx == 64)
			{
				text = "PDD1";
			}
			else if (idx == 65)
			{
				text = "PDD2";
			}
		}
		else if (idx == 64)
		{
			text = "FID1";
		}
		else if (idx == 65)
		{
			text = "FID2";
		}
		if (idx == 80)
		{
			text = "TCD1";
		}
		else if (idx == 81)
		{
			text = "TCD2";
		}
		else if (idx == 96)
		{
			text = "FPD1";
		}
		else if (idx == 97)
		{
			text = "FPD2";
		}
		else if (idx == 112)
		{
			text = "ECD1";
		}
		else if (idx == 113)
		{
			text = "ECD2";
		}
		else if (idx == 128)
		{
			text = "NPD1";
		}
		else if (idx == 129)
		{
			text = "NPD2";
		}
		else if (idx == 144)
		{
			text = "ZrO";
		}
		else if (idx == 145)
		{
			text = "ZrO";
		}
		else if (idx == 160)
		{
			text = "PDD1";
		}
		else if (idx == 161)
		{
			text = "PDD2";
		}
		else
		{
			if (idx != 16)
			{
				Console.WriteLine("未知硬件");
			}
			text = "控温板";
		}
		text += " ";
		for (int i = 0; i < version.Length; i++)
		{
			string text2 = text;
			char c = (char)version[i];
			text = text2 + c;
		}
		return text;
	}
}
