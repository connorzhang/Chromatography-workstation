using System.IO;

namespace IBrainChrom2018;

public class GasChromControl : SysCfgControl
{
	public GasChromControl()
	{
		controlModule = ControlModule.GasControl;
	}

	public override void SaveToFile(BinaryWriter binaryWriter_0)
	{
		base.SaveToFile(binaryWriter_0);
	}
}
