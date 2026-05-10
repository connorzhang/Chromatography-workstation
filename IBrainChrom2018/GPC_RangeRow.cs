using System;
using System.IO;

namespace IBrainChrom2018;

[Serializable]
public struct GPC_RangeRow
{
	public float float_0;

	public float high;

	public bool Equals(GPC_RangeRow gpc_RangeRow)
	{
		return float_0 == gpc_RangeRow.float_0 && high == gpc_RangeRow.high;
	}

	public GPC_RangeRow Copy()
	{
		return new GPC_RangeRow
		{
			float_0 = float_0,
			high = high
		};
	}

	public void SaveToFile(BinaryWriter binaryWriter_0)
	{
		binaryWriter_0.Write(float_0);
		binaryWriter_0.Write(high);
	}

	public void LoadFromFile(BinaryReader binaryReader_0)
	{
		float_0 = binaryReader_0.ReadSingle();
		high = binaryReader_0.ReadSingle();
	}
}
