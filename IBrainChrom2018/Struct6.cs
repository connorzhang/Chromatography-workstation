using System;

namespace IBrainChrom2018;

internal struct Struct6
{
	public byte byte_0;

	public float float_0;

	public float float_1;

	public float float_2;

	public byte byte_1;

	public bool bool_0;

	public bool bool_1;

	public bool bool_2;

	public bool bool_3;

	public bool bool_4;

	public byte byte_2;

	public void method_0(byte[] byte_3, int int_0)
	{
		byte_0 = byte_3[int_0++];
		if (byte_0 < 30 || byte_0 > 37)
		{
			throw new Exception("EPC数据:ECP类型");
		}
		float_0 = IBrainConvert.ByteArray2Float(byte_3, int_0, 1);
		int_0 += 2;
		float_1 = IBrainConvert.ByteArray2Float(byte_3, int_0, 1);
		int_0 += 2;
		float_2 = IBrainConvert.ByteArray2Float(byte_3, int_0, 1);
		int_0 += 2;
		byte_1 = byte_3[int_0++];
		if (byte_1 >= 100)
		{
			throw new Exception("EPC数据:分流比");
		}
		byte b = byte_3[int_0];
		bool_0 = IBrainConvert.Byte2Bool(b, 7);
		bool_1 = IBrainConvert.Byte2Bool(b, 6);
		bool_2 = IBrainConvert.Byte2Bool(b, 5);
		bool_3 = IBrainConvert.Byte2Bool(b, 4);
		bool_4 = IBrainConvert.Byte2Bool(b, 3);
		byte_2 = (byte)(b & 7);
	}

	public override string ToString()
	{
		return "类型:" + BitConverter.ToString(new byte[1] { byte_0 }) + ", " + $"进口:{float_0:0.0} psi, 出口:{float_1:0.0} psi" + ", " + $"流量:{float_2:0.0} ml/min" + ", " + $"分流比:{byte_1:0}%" + string.Format("\r\n      状态:[气源:{0}", bool_0 ? "开" : "关") + ", " + string.Format("初始:{0}", bool_1 ? "√" : "×") + ", " + string.Format("上升:{0}", bool_2 ? "√" : "×") + ", " + string.Format("保持:{0}", bool_3 ? "√" : "×") + ", " + string.Format("故障:{0}", bool_4 ? "√" : "×") + "]" + $" 程升阶数:{byte_2}";
	}
}
