using System;
using System.Windows.Forms;

namespace IBrainChrom2018;

public class LCC_Pump : BaseControl
{
	public float fFlow;

	protected bool hasShowMsg;

	public bool hasShowWarn;

	public int idxSolvent;

	public float maxFp;

	public float minFp;

	public double double_0;

	public LCC_Pump(SysCfgControl from)
		: base(from)
	{
		cmStyle = ControlModule.Pump;
		base.Working = false;
	}

	public virtual void Flow(bool write, float fFlow)
	{
		if ((0.001 > (double)fFlow || (double)fFlow > 9.999) && !hasShowMsg)
		{
			MessageBox.Show("输入0.001——9.999ml流量值");
			hasShowMsg = true;
			return;
		}
		this.fFlow = fFlow;
		hasShowMsg = false;
		long num = (long)(fFlow * 1000f);
		WriteCmd((byte)(num % 256), (byte)(num / 256), 8);
	}

	public virtual void MaxPress(bool write, float press)
	{
		if (0f <= press && press <= 42f)
		{
			if (press != maxFp)
			{
				maxFp = press;
				double value = (double)press * 144.897959;
				long num = Convert.ToInt32(value);
				WriteCmd((byte)(num % 256), (byte)(num / 256), 3);
			}
		}
		else
		{
			MessageBox.Show("要求0-42MPa");
		}
	}

	public virtual void MinPress(bool write, float press)
	{
		if (0f <= press && press <= 42f)
		{
			if (press != minFp)
			{
				minFp = press;
				double value = (double)press * 144.897959;
				long num = Convert.ToInt32(value);
				WriteCmd((byte)(num % 256), (byte)(num / 256), 4);
			}
		}
		else
		{
			MessageBox.Show("要求0-42MPa");
		}
	}

	public virtual void Solvent(bool write, byte idxItem)
	{
		if (idxItem >= 0)
		{
			idxSolvent = idxItem;
			WriteCmd(idxItem, 0, 2);
		}
	}

	public virtual void StartStop(byte startStop)
	{
		if (startStop == 0)
		{
			base.Working = false;
			WriteCmd(1, 0, 6);
		}
		if (startStop == 1)
		{
			base.Working = true;
			WriteCmd(1, 0, 7);
		}
		if (startStop == 2)
		{
			fFlow = 9.999f;
			WriteCmd(1, 0, 5);
		}
	}
}
