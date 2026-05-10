using System;
using System.Windows.Forms;

namespace IBrainChrom2018;

public class DtC_LC2 : DtC_Detector
{
	private Acquisition acquisition_0;

	public Acquisition Acq
	{
		get
		{
			return acquisition_0;
		}
		set
		{
			acquisition_0 = value;
			auF = acquisition_0.AcqRange / 8388607f;
		}
	}

	public DtC_LC2(SysCfgControl from)
		: base(from)
	{
	}

	public override void BeginGather(bool sample)
	{
		if (IsGC08)
		{
			return;
		}
		if (base.HasHardWare)
		{
			if (from.HardWare is Class10)
			{
				(from.HardWare as Class10).method_5(bool_1: true);
			}
			return;
		}
		float acqRate = acquisition_0.AcqRate;
		acqRate = Math.Max(15f, acqRate);
		virSgl.virSglArg_0.sample = sample;
		int val = Convert.ToInt32(1000f / acqRate) - 1;
		virSgl.virSglArg_0.sample_interval = Math.Max(1, val);
		if (!sample)
		{
			virSgl.virSglArg_0.Init_sf();
		}
		virSgl.BeginThread();
	}

	public void DLightTime(int time)
	{
		SendCmd(32, write: false, null);
	}

	public void Edition(int edition)
	{
		SendCmd(35, write: false, null);
	}

	public override void OpenCloseLight(bool write, bool open)
	{
		base.Working = open;
		byte openClose = (byte)(open ? 3u : 0u);
		OpenCloseLight(write, openClose);
	}

	public virtual void OpenCloseLight(bool write, byte openClose)
	{
		SendCmd(4, write, openClose);
	}

	public override void Raise(ref LcCmd data)
	{
		base.Raise(ref data);
		if (data.OK)
		{
			byte b = data.byte_0;
			if (b == 2)
			{
			}
		}
	}

	public override void Range(bool write, float range)
	{
		float val = Math.Min(6f, range);
		val = Math.Max(0f, val);
		SendCmd(5, write, (ushort)(val * 10000f));
	}

	public override void RistTime(bool write, float ristTime)
	{
		byte val = (byte)Math.Min(10f, ristTime);
		val = Math.Max((byte)0, val);
		SendCmd(3, write, val);
	}

	public void ScanBeginW(bool write, ushort wave)
	{
		SendCmd(17, write, wave);
	}

	public void ScanEndW(bool write, ushort wave)
	{
		SendCmd(18, write, wave);
	}

	public void ScanFile(bool write, byte file)
	{
		SendCmd(16, write, file);
	}

	public void ScanProc(byte proc)
	{
		SendCmd(19, write: true, proc);
	}

	public void ScanRead(ushort wave)
	{
		SendCmd(20, write: false, wave);
	}

	public void Serial(int serial)
	{
		SendCmd(34, write: false, null);
	}

	public override void Stop()
	{
		if (IsGC08)
		{
			return;
		}
		if (base.HasHardWare)
		{
			if (from.HardWare is Class10)
			{
				(from.HardWare as Class10).method_5(bool_1: false);
			}
		}
		else if (virSgl != null)
		{
			virSgl.Stop();
		}
	}

	public override void Wave(bool write, ushort wave)
	{
		if ((190 > wave || wave > 720) && !hasShowMsg)
		{
			MessageBox.Show("要求190——720nm");
			hasShowMsg = true;
		}
		else
		{
			hasShowMsg = false;
			SendCmd(2, write, wave);
		}
	}

	public void WLightTime(int time)
	{
		SendCmd(33, write: false, null);
	}

	public override void Zero()
	{
		SendCmd(15, write: true, 1);
	}
}
