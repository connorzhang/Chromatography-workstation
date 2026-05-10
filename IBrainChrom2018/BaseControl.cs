using System;
using System.IO;
using System.Runtime.CompilerServices;
using System.Threading;

namespace IBrainChrom2018;

public class BaseControl
{
	public delegate void GetDataLC2(int NO, LcCmd data);

	[CompilerGenerated]
	private bool bool_0;

	public byte channel;

	protected LcCmd lcCmd_0;

	public ControlModule cmStyle;

	public int equipedInstruNo = -1;

	public SysCfgControl from;

	public byte byte_0;

	public string name = "";

	public int NO;

	private GetDataLC2 getDataLC2_0;

	public bool HasHardWare => from.HardWare != null;

	public bool Working { get; set; }

	public event GetDataLC2 OnGetDataLC2
	{
		add
		{
			GetDataLC2 getDataLC = getDataLC2_0;
			GetDataLC2 getDataLC2;
			do
			{
				getDataLC2 = getDataLC;
				GetDataLC2 value2 = (GetDataLC2)Delegate.Combine(getDataLC2, value);
				getDataLC = Interlocked.CompareExchange(ref getDataLC2_0, value2, getDataLC2);
			}
			while (getDataLC != getDataLC2);
		}
		remove
		{
			GetDataLC2 getDataLC = getDataLC2_0;
			GetDataLC2 getDataLC2;
			do
			{
				getDataLC2 = getDataLC;
				GetDataLC2 value2 = (GetDataLC2)Delegate.Remove(getDataLC2, value);
				getDataLC = Interlocked.CompareExchange(ref getDataLC2_0, value2, getDataLC2);
			}
			while (getDataLC != getDataLC2);
		}
	}

	public BaseControl(SysCfgControl from)
	{
		this.from = from;
	}

	public virtual void LoadFromFile(BinaryReader binaryReader_0)
	{
		name = binaryReader_0.ReadString();
		channel = binaryReader_0.ReadByte();
		byte_0 = binaryReader_0.ReadByte();
		equipedInstruNo = binaryReader_0.ReadInt32();
	}

	public virtual void LoadFromObject(object control)
	{
		name = (control as BaseControl).name;
		channel = (control as BaseControl).channel;
		byte_0 = (control as BaseControl).byte_0;
		equipedInstruNo = (control as BaseControl).equipedInstruNo;
	}

	public virtual void Raise(ref LcCmd data)
	{
		if (getDataLC2_0 != null)
		{
			getDataLC2_0(NO, data);
		}
	}

	public virtual void SaveToFile(BinaryWriter binaryWriter_0)
	{
		binaryWriter_0.Write((byte)cmStyle);
		binaryWriter_0.Write(name);
		binaryWriter_0.Write(channel);
		binaryWriter_0.Write(byte_0);
		binaryWriter_0.Write(equipedInstruNo);
	}

	protected void SendCmd(byte command, bool write, int? value32)
	{
		lcCmd_0.device = byte_0;
		lcCmd_0.byte_0 = command;
		lcCmd_0.byte_1 = ((!write) ? ((byte)1) : ((byte)0));
		if (value32.HasValue)
		{
			lcCmd_0.Value32 = value32.Value;
		}
		if (from.HardWare is Class10)
		{
			(from.HardWare as Class10).method_4(lcCmd_0);
		}
	}

	public bool WriteCmd(byte byte_1, byte byte_2, byte byte_3)
	{
		return WriteCmd(0, byte_1, byte_2, 0, 0, 0, byte_3, byte_0);
	}

	public bool WriteCmd(byte byte_1, byte byte_2, byte byte_3, byte byte_4, byte byte_5, byte byte_6, byte byte_7, byte byte_8)
	{
		if (!HasHardWare || !(from.HardWare is UsbSZ))
		{
			return false;
		}
		if (from is SZ_Dt)
		{
			return false;
		}
		byte[] cmdBuf = new byte[8] { byte_1, byte_2, byte_3, byte_4, byte_5, byte_6, byte_7, byte_8 };
		return (from.HardWare as UsbSZ).WriteCmd(cmdBuf);
	}
}
