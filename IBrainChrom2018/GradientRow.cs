using System;
using System.IO;

namespace IBrainChrom2018;

[Serializable]
public struct GradientRow : IComparable
{
	public float time;

	public float float_0;

	public float float_1;

	public float float_2;

	public float float_3;

	public float flow;

	public int CompareTo(object target)
	{
		if (target is GradientRow)
		{
			float value = ((GradientRow)target).time;
			return time.CompareTo(value);
		}
		return 0;
	}

	public void SaveToFile(BinaryWriter binaryWriter_0)
	{
		binaryWriter_0.Write(time);
		binaryWriter_0.Write(float_0);
		binaryWriter_0.Write(float_1);
		binaryWriter_0.Write(float_2);
		binaryWriter_0.Write(float_3);
		binaryWriter_0.Write(flow);
	}

	public void LoadFromFile(BinaryReader binaryReader_0)
	{
		time = binaryReader_0.ReadSingle();
		float_0 = binaryReader_0.ReadSingle();
		float_1 = binaryReader_0.ReadSingle();
		float_2 = binaryReader_0.ReadSingle();
		float_3 = binaryReader_0.ReadSingle();
		flow = binaryReader_0.ReadSingle();
	}
}
