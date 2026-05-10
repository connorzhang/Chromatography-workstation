namespace IBrainChrom2018;

public class ASC_LC2 : ASC_Sampler
{
	public ASC_LC2(SysCfgControl from)
		: base(from)
	{
		cmStyle = ControlModule.AutoSampler;
		base.Working = false;
	}
}
