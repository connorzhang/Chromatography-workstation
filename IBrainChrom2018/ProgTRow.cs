using System;
using System.IO;

namespace IBrainChrom2018;

[Serializable]
public struct ProgTRow
{
	public float upRate;

	public float endTemp;

	public float holdTime;

	public bool Valid => upRate > 0f && endTemp > 0f && holdTime > 0f;

	public void SaveToFile(BinaryWriter binaryWriter_0)
	{
		binaryWriter_0.Write(upRate);
		binaryWriter_0.Write(endTemp);
		binaryWriter_0.Write(holdTime);
	}

	public void LoadFromFile(BinaryReader binaryReader_0)
	{
		upRate = binaryReader_0.ReadSingle();
		endTemp = binaryReader_0.ReadSingle();
		holdTime = binaryReader_0.ReadSingle();
	}
}
