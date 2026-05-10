using System;
using System.Runtime.InteropServices;

namespace IBrainChrom2018;

[Serializable]
[StructLayout(LayoutKind.Sequential)]
public class TemperSettingRow : IArrayBase
{
	public float tempStart;

	public float tempEnd;

	public float tempKeep;

	public byte[] GetByte()
	{
		byte[] byte_ = new byte[0];
		IBrainConvert.ArrayCopy(ref byte_, IBrainConvert.Float2Byte(tempStart, 1));
		IBrainConvert.ArrayCopy(ref byte_, IBrainConvert.Float2Byte(tempEnd, 1));
		IBrainConvert.ArrayCopy(ref byte_, IBrainConvert.Float2Byte(tempKeep, 1));
		return byte_;
	}

	public void ReadByte(byte[] byte_0)
	{
		if (byte_0.Length != 6)
		{
			throw new Exception("ProgTemp.Bytes");
		}
		tempStart = IBrainConvert.ByteArray2Float(byte_0, 0, 1);
		tempEnd = IBrainConvert.ByteArray2Float(byte_0, 2, 1);
		tempKeep = IBrainConvert.ByteArray2Float(byte_0, 4, 1);
	}
}
