using System;
using System.IO;

namespace IBrainChrom2018;

[Serializable]
public struct SplLevel
{
	public float responseA;

	public float responseH;

	public void SaveToFile(BinaryWriter binaryWriter_0)
	{
		binaryWriter_0.Write(responseA);
		binaryWriter_0.Write(responseH);
	}

	public void LoadFromFile(BinaryReader binaryReader_0)
	{
		responseA = binaryReader_0.ReadSingle();
		responseH = binaryReader_0.ReadSingle();
	}
}
