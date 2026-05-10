using System.IO;

namespace IBrainChrom2018;

public class AutoSamplerControl : SysCfgControl
{
	public AutoSamplerControl()
	{
		controlModule = ControlModule.AutoSampler;
	}

	public override void SaveToFile(BinaryWriter binaryWriter_0)
	{
		base.SaveToFile(binaryWriter_0);
	}
}
