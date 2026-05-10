using System;
using System.IO;

namespace IBrainChrom2018;

public class SSAly : LclFileRW
{
	public Injection[] ssInjs = new Injection[0];

	public SSOpt ssOpt = new SSOpt();

	protected override void loadFromFile(BinaryReader binaryReader_1)
	{
		byte b = binaryReader_1.ReadByte();
		if (b == 1)
		{
			int seqInjsNum = binaryReader_1.ReadInt32();
			SetSeqInjsNum(seqInjsNum);
			for (int i = 0; i < ssInjs.Length; i++)
			{
				ssInjs[i].LoadFromFile(binaryReader_1);
			}
			ssOpt.LoadFromFile(binaryReader_1);
		}
		else
		{
			Class49.smethod_33(b);
		}
	}

	protected override void saveToFile(BinaryWriter binaryWriter_1)
	{
		binaryWriter_1.Write(Class49.smethod_36());
		binaryWriter_1.Write(ssInjs.Length);
		for (int i = 0; i < ssInjs.Length; i++)
		{
			ssInjs[i].SaveToFile(binaryWriter_1);
		}
		ssOpt.SaveToFile(binaryWriter_1);
	}

	public void SetSeqInjsNum(int int_0)
	{
		Array.Resize(ref ssInjs, int_0);
		for (int i = 0; i < ssInjs.Length; i++)
		{
			if (ssInjs[i] == null)
			{
				ssInjs[i] = new Injection();
			}
		}
	}
}
