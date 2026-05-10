using System.Windows.Forms;

namespace IBrainChrom2018;

public class LCC_LC2 : LCC_Pump
{
	private byte byte_1;

	public LCC_LC2(SysCfgControl from)
		: base(from)
	{
		byte_1 = 0;
		cmStyle = ControlModule.Pump;
		base.Working = false;
	}

	public void Edition(int edition)
	{
		SendCmd(55, write: false, null);
	}

	public override void Flow(bool write, float fFlow)
	{
		if (0.001 <= (double)fFlow && (double)fFlow <= 9.999)
		{
			SendCmd(48, write, (ushort)(fFlow * 1000f));
			return;
		}
		MessageBox.Show("输入0.001——9.999ml流量值");
		hasShowMsg = true;
	}

	public override void MaxPress(bool write, float press)
	{
		SendCmd(50, write, method_0(press));
	}

	public override void MinPress(bool write, float press)
	{
		SendCmd(51, write, method_0(press));
	}

	public void Press()
	{
		SendCmd(49, write: false, null);
	}

	public void PressUnit(bool write, byte unit)
	{
		SendCmd(53, write, unit);
	}

	public override void Raise(ref LcCmd data)
	{
		base.Raise(ref data);
		if (data.byte_0 == 53)
		{
			byte_1 = data.Value8;
		}
	}

	public void Serial(int serial)
	{
		SendCmd(54, write: false, null);
	}

	public override void Solvent(bool write, byte idxItem)
	{
		SendCmd(56, write, idxItem);
	}

	public override void StartStop(byte startStop)
	{
		if (startStop == 0)
		{
			base.Working = false;
		}
		if (startStop == 1 || startStop == 2)
		{
			base.Working = true;
		}
		SendCmd(52, write: true, startStop);
	}

	private ushort method_0(float float_0)
	{
		if (byte_1 == 0)
		{
			return (ushort)((double)float_0 * 144.897959);
		}
		if (byte_1 == 1)
		{
			return (ushort)((double)float_0 * 14.2);
		}
		if (byte_1 == 2)
		{
			return (ushort)((double)float_0 * 14.489796);
		}
		if (byte_1 == 3)
		{
			return (ushort)float_0;
		}
		return 0;
	}
}
