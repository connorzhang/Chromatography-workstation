using System;
using System.IO;
using System.Threading;
using System.Windows.Forms;

namespace IBrainChrom2018;

public class DtC_Detector : DtC_Channel
{
	public delegate void GetNewSignal(int NO, float value, bool bool_0);

	protected float auF;

	protected bool hasShowMsg;

	public HwPara hwPara;

	public float range;

	public float ristTime;

	protected VirtualSignal virSgl;

	public int wave;

	private GetNewSignal getNewSignal_0;

	private event GetNewSignal OnGetNewSignal
	{
		add
		{
			GetNewSignal getNewSignal = getNewSignal_0;
			GetNewSignal getNewSignal2;
			do
			{
				getNewSignal2 = getNewSignal;
				GetNewSignal value2 = (GetNewSignal)Delegate.Combine(getNewSignal2, value);
				getNewSignal = Interlocked.CompareExchange(ref getNewSignal_0, value2, getNewSignal2);
			}
			while (getNewSignal != getNewSignal2);
		}
		remove
		{
			GetNewSignal getNewSignal = getNewSignal_0;
			GetNewSignal getNewSignal2;
			do
			{
				getNewSignal2 = getNewSignal;
				GetNewSignal value2 = (GetNewSignal)Delegate.Remove(getNewSignal2, value);
				getNewSignal = Interlocked.CompareExchange(ref getNewSignal_0, value2, getNewSignal2);
			}
			while (getNewSignal != getNewSignal2);
		}
	}

	public DtC_Detector(SysCfgControl from)
		: base(from)
	{
		cmStyle = ControlModule.Detector;
		base.Working = true;
	}

	public virtual void BeginGather(bool sample)
	{
		if (IsGC08)
		{
			return;
		}
		if (base.HasHardWare)
		{
			hwPara.working = true;
			if (from.HardWare is UsbSZ)
			{
				(from.HardWare as UsbSZ).ExecutePara(channel, hwPara);
			}
			return;
		}
		float acqRate = hwPara.acquisition_0.AcqRate;
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

	public virtual void Detector_Set(bool zero)
	{
		virSgl.Detector_Set(zero);
	}

	public override void LoadFromFile(BinaryReader binaryReader_0)
	{
		base.LoadFromFile(binaryReader_0);
		detectorStyle = (DetectorStyle)binaryReader_0.ReadByte();
	}

	public override void LoadFromObject(object control)
	{
		base.LoadFromObject(control);
		detectorStyle = (control as DtC_Detector).detectorStyle;
	}

	public virtual void LoadVirtualSignal(int dtNo, float bsChg, float scChg, GetNewSignal getNewSignal)
	{
		if (!IsGC08)
		{
			Stop();
			getNewSignal_0 = getNewSignal;
			virSgl = new VirtualSignal();
			virSgl.virSglArg_0.signal_bs = bsChg;
			virSgl.virSglArg_0.signal_scale = scChg;
			VirSglArg virSglArg_ = virSgl.virSglArg_0;
			NO = dtNo;
			virSglArg_.SetRandomSeed(dtNo);
			virSgl.LoadVirtualSignals(dtNo);
			virSgl.OnGetVrData += method_1;
		}
	}

	public virtual void OpenCloseLight(bool write, bool open)
	{
		base.Working = open;
		WriteCmd((byte)((!open) ? 1u : 3u), 0, 4);
	}

	public override void Raise(ref LcCmd data)
	{
		if (getNewSignal_0 != null)
		{
			getNewSignal_0(NO, (float)data.AU * auF, data.Key);
		}
	}

	public virtual void Range(bool write, float range)
	{
		if (range > 0f)
		{
			this.range = range;
			long num = Convert.ToUInt32(range * 100f);
			WriteCmd((byte)(num % 256), (byte)(num / 256), 3);
		}
	}

	public virtual void RistTime(bool write, float ristTime)
	{
		if (ristTime > 0f)
		{
			this.ristTime = ristTime;
			long num = Convert.ToUInt32(ristTime * 100f);
			WriteCmd((byte)(num % 256), (byte)(num / 256), 5);
		}
	}

	public override void SaveToFile(BinaryWriter binaryWriter_0)
	{
		base.SaveToFile(binaryWriter_0);
		binaryWriter_0.Write((byte)detectorStyle);
	}

	public virtual void Stop()
	{
		if (IsGC08)
		{
			return;
		}
		if (base.HasHardWare)
		{
			hwPara.working = false;
			if (from.HardWare is UsbSZ)
			{
				(from.HardWare as UsbSZ).ExecutePara(channel, hwPara);
			}
		}
		else if (virSgl != null)
		{
			virSgl.Stop();
		}
	}

	private void method_0(float float_0, bool bool_1)
	{
		if (getNewSignal_0 != null)
		{
			getNewSignal_0(NO, float_0, bool_1);
		}
	}

	private void method_1(float float_0)
	{
		if (getNewSignal_0 != null)
		{
			getNewSignal_0(NO, float_0, bool_0: false);
		}
	}

	public virtual void Wave(bool write, ushort wave)
	{
		if ((190 > wave || wave > 720) && !hasShowMsg)
		{
			MessageBox.Show("要求190——720nm");
			hasShowMsg = true;
		}
		else
		{
			this.wave = wave;
			hasShowMsg = false;
			WriteCmd((byte)(wave % 256), (byte)(wave / 256), 2);
		}
	}

	public virtual void Zero()
	{
		WriteCmd(1, 0, 6);
	}
}
