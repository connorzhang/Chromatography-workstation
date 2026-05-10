using System;
using System.Threading;

namespace IBrainChrom2018;

public class DtC_Channel : BaseControl
{
	public delegate void GetNewSignals(int NO, float[] values);

	public DetectorStyle detectorStyle;

	public bool IsGC08;

	public byte mark;

	public string unitStr = "mV";

	public ChromInfoR chromInfoR = new ChromInfoR();

	private GetNewSignals getNewSignals_0;

	public event GetNewSignals OnGetNewSignals
	{
		add
		{
			GetNewSignals getNewSignals = getNewSignals_0;
			GetNewSignals getNewSignals2;
			do
			{
				getNewSignals2 = getNewSignals;
				GetNewSignals value2 = (GetNewSignals)Delegate.Combine(getNewSignals2, value);
				getNewSignals = Interlocked.CompareExchange(ref getNewSignals_0, value2, getNewSignals2);
			}
			while (getNewSignals != getNewSignals2);
		}
		remove
		{
			GetNewSignals getNewSignals = getNewSignals_0;
			GetNewSignals getNewSignals2;
			do
			{
				getNewSignals2 = getNewSignals;
				GetNewSignals value2 = (GetNewSignals)Delegate.Remove(getNewSignals2, value);
				getNewSignals = Interlocked.CompareExchange(ref getNewSignals_0, value2, getNewSignals2);
			}
			while (getNewSignals != getNewSignals2);
		}
	}

	public DtC_Channel(SysCfgControl from)
		: base(from)
	{
		detectorStyle = DetectorStyle.General;
		IsGC08 = true;
		cmStyle = ControlModule.Detector;
	}

	public void Foo(int int_0)
	{
		NO = int_0;
		getNewSignals_0 = null;
	}

	public void Gc08Values(float[] values)
	{
		if (IsGC08 && getNewSignals_0 != null)
		{
			getNewSignals_0(NO, values);
		}
	}
}
