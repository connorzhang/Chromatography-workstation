using System.IO;

namespace IBrainChrom2018;

public class DetectorControl : SysCfgControl
{
	public DetectorControl()
	{
		controlModule = ControlModule.Detector;
	}

	public override void SaveToFile(BinaryWriter binaryWriter_0)
	{
		base.SaveToFile(binaryWriter_0);
	}
}
