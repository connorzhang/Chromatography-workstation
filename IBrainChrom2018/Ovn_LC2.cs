namespace IBrainChrom2018;

public class Ovn_LC2 : BaseControl
{
	public Ovn_LC2(SysCfgControl from)
		: base(from)
	{
		cmStyle = ControlModule.Oven;
		base.Working = false;
	}
}
