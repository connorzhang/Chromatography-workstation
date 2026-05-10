using System;

namespace IBrainChrom2018;

public struct LcCmd
{
	public byte device;

	public byte byte_0;

	public byte byte_1;

	private byte byte_2;

	private byte byte_3;

	private byte byte_4;

	private byte byte_5;

	private byte byte_6;

	private byte byte_7;

	private byte byte_8;

	public bool OK => (byte_1 & 1) == 0;

	public bool Key => (byte_1 & 0x10) == 16;

	public byte Value8
	{
		get
		{
			return byte_2;
		}
		set
		{
			byte_2 = value;
		}
	}

	public ushort Value16
	{
		get
		{
			return (ushort)(byte_2 + (ushort)(byte_3 << 8));
		}
		set
		{
			byte[] bytes = BitConverter.GetBytes(value);
			byte_2 = bytes[0];
			byte_3 = bytes[1];
		}
	}

	public int Value24
	{
		get
		{
			int num = byte_2 + (byte_3 << 8);
			return num + (byte_4 << 16);
		}
	}

	public int Value32
	{
		get
		{
			int num = byte_2 + (ushort)(byte_3 << 8);
			num += (ushort)(byte_4 << 16);
			return num + (ushort)(byte_5 << 24);
		}
		set
		{
			byte[] bytes = BitConverter.GetBytes(value);
			byte_2 = bytes[0];
			byte_3 = bytes[1];
			byte_4 = bytes[2];
			byte_5 = bytes[3];
		}
	}

	public int AU
	{
		get
		{
			int num = byte_6 + (byte_7 << 8);
			return num + (byte_8 << 16);
		}
	}

	public void ToBytes(ref byte[] bytes)
	{
		bytes[0] = device;
		bytes[1] = byte_0;
		bytes[2] = byte_1;
		bytes[3] = byte_2;
		bytes[4] = byte_3;
		bytes[5] = byte_4;
		bytes[6] = byte_5;
		bytes[7] = byte_6;
		bytes[8] = byte_7;
		bytes[9] = byte_8;
	}

	public void FromBytes(ref byte[] bytes)
	{
		device = bytes[0];
		byte_0 = bytes[1];
		byte_1 = bytes[2];
		byte_2 = bytes[3];
		byte_3 = bytes[4];
		byte_4 = bytes[5];
		byte_5 = bytes[6];
		byte_6 = bytes[7];
		byte_7 = bytes[8];
		byte_8 = bytes[9];
	}
}
