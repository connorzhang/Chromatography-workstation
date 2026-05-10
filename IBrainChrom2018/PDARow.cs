using System;
using System.IO;

namespace IBrainChrom2018;

[Serializable]
public struct PDARow
{
	public bool used;

	public string name;

	public bool Equals(PDARow pdaRow)
	{
		return used == pdaRow.used && name == pdaRow.name;
	}

	public void SaveToFile(BinaryWriter binaryWriter_0)
	{
		binaryWriter_0.Write(used);
		binaryWriter_0.Write(name);
	}

	public void LoadFromFile(BinaryReader binaryReader_0)
	{
		used = binaryReader_0.ReadBoolean();
		name = binaryReader_0.ReadString();
	}
}
