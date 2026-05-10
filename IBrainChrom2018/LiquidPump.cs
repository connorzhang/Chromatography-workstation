using System.IO;

namespace IBrainChrom2018;

public class LiquidPump : SysCfgControl
{
	public LiquidPump()
	{
		controlModule = ControlModule.Pump;
	}

	public override void SaveToFile(BinaryWriter binaryWriter_0)
	{
		base.SaveToFile(binaryWriter_0);
	}
}
