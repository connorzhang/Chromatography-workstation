using System;
using System.Collections.Generic;
using System.Threading;
using System.Windows.Forms;

namespace IBrainChrom2018;

internal class Class10 : SLUSBXpressDLL
{
	public delegate void Delegate2(ref LcCmd lcCmd_0);

	public const int int_0 = 10;

	public ControlModule[] controlModule_0 = new ControlModule[1] { ControlModule.Set };

	public LcCmd lcCmd_0;

	private byte[] byte_0 = new byte[10];

	private Queue<LcCmd> queue_0 = new Queue<LcCmd>();

	public uint uint_0;

	public readonly HwStyle hwStyle_0 = HwStyle.SZ;

	public bool bool_0;

	private byte[] byte_1 = new byte[10];

	public string string_0 = Lang.PS("赛智液相卡", "hzSZ LC Card");

	private LcCmd lcCmd_1;

	private System.Windows.Forms.Timer timer_0 = new System.Windows.Forms.Timer();

	private Delegate2 delegate2_0;

	public void method_0(Delegate2 delegate2_1)
	{
		Delegate2 @delegate = delegate2_0;
		Delegate2 delegate2;
		do
		{
			delegate2 = @delegate;
			Delegate2 value = (Delegate2)Delegate.Combine(delegate2, delegate2_1);
			@delegate = Interlocked.CompareExchange(ref delegate2_0, value, delegate2);
		}
		while (@delegate != delegate2);
	}

	public void method_1(Delegate2 delegate2_1)
	{
		Delegate2 @delegate = delegate2_0;
		Delegate2 delegate2;
		do
		{
			delegate2 = @delegate;
			Delegate2 value = (Delegate2)Delegate.Remove(delegate2, delegate2_1);
			@delegate = Interlocked.CompareExchange(ref delegate2_0, value, delegate2);
		}
		while (@delegate != delegate2);
	}

	public Class10()
	{
		timer_0.Interval = 220;
		method_5(bool_1: false);
		timer_0.Tick += timer_0_Tick;
		lcCmd_0.byte_0 = 1;
		lcCmd_0.byte_1 = 1;
	}

	public void method_2()
	{
		SLUSBXpressDLL.SI_Close(uint_0);
	}

	public bool method_3(ControlModule controlModule_1, bool bool_1)
	{
		bool result = false;
		for (int i = 0; i < controlModule_0.Length; i++)
		{
			if (controlModule_0[i] == controlModule_1)
			{
				if (1 == 0)
				{
					return false;
				}
				bool_0 = bool_1;
				return true;
			}
		}
		return result;
	}

	public void method_4(LcCmd lcCmd_2)
	{
		queue_0.Enqueue(lcCmd_2);
	}

	public void method_5(bool bool_1)
	{
		if (bool_1 && !timer_0.Enabled)
		{
			timer_0.Enabled = true;
		}
		if (!bool_1 && timer_0.Enabled)
		{
			timer_0.Enabled = false;
		}
	}

	public void method_6(Delegate2 delegate2_1)
	{
		delegate2_0 = delegate2_1;
	}

	private void timer_0_Tick(object sender, EventArgs e)
	{
		int lpdwBytesWritten = 0;
		int lpdwBytesReturned = 0;
		int num = 0;
		((queue_0.Count != 0) ? queue_0.Dequeue() : lcCmd_0).ToBytes(ref byte_1);
		if (SLUSBXpressDLL.SI_Write(uint_0, ref byte_1[0], 10, ref lpdwBytesWritten, 0) == 0 && lpdwBytesWritten == 10)
		{
			if (SLUSBXpressDLL.SI_Read(uint_0, ref byte_0[0], 10, ref lpdwBytesReturned, 0) == 0 && lpdwBytesReturned == 10)
			{
				lcCmd_1.FromBytes(ref byte_0);
				if (delegate2_0 != null)
				{
					delegate2_0(ref lcCmd_1);
				}
			}
		}
		else
		{
			num++;
			if (num > 50)
			{
				method_5(bool_1: false);
			}
		}
	}
}
