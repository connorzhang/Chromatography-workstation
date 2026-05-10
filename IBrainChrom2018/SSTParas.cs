using System;
using System.IO;

namespace IBrainChrom2018;

public class SSTParas
{
	public SSTCriterion criterion;

	public string description = "";

	public DateTime dtCreate = DateTime.Now;

	public string userName = "";

	public void LoadFromFile(BinaryReader binaryReader_0)
	{
		dtCreate = DateTime.FromBinary(binaryReader_0.ReadInt64());
		userName = binaryReader_0.ReadString();
		description = binaryReader_0.ReadString();
		criterion = (SSTCriterion)binaryReader_0.ReadByte();
	}

	public void SaveToFile(BinaryWriter binaryWriter_0)
	{
		binaryWriter_0.Write(dtCreate.ToBinary());
		binaryWriter_0.Write(userName);
		binaryWriter_0.Write(description);
		binaryWriter_0.Write((byte)criterion);
	}
}
