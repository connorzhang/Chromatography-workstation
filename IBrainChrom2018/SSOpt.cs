using System.IO;

namespace IBrainChrom2018;

public class SSOpt
{
	public string description = "";

	public VolumnUnits injVolumnUnit;

	public void LoadFromFile(BinaryReader binaryReader_0)
	{
		byte b = binaryReader_0.ReadByte();
		if (b == 1)
		{
			injVolumnUnit = (VolumnUnits)binaryReader_0.ReadByte();
			description = binaryReader_0.ReadString();
		}
		else
		{
			Class49.smethod_33(b);
		}
	}

	public void LoadFromObject(SSOpt ssOpt)
	{
		injVolumnUnit = ssOpt.injVolumnUnit;
		description = ssOpt.description;
	}

	public void SaveToFile(BinaryWriter binaryWriter_0)
	{
		binaryWriter_0.Write(Class49.smethod_36());
		binaryWriter_0.Write((byte)injVolumnUnit);
		binaryWriter_0.Write(description);
	}
}
