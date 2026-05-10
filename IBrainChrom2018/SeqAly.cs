using System;
using System.IO;

namespace IBrainChrom2018;

public class SeqAly : LclFileRW
{
	public SeqAlyOpt seqAlyOpt = new SeqAlyOpt();

	public Injection[] seqInjs = new Injection[0];

	protected override void loadFromFile(BinaryReader binaryReader_1)
	{
		byte b = binaryReader_1.ReadByte();
		if (b == 1)
		{
			int seqInjsNum = binaryReader_1.ReadInt32();
			SetSeqInjsNum(seqInjsNum);
			for (int i = 0; i < seqInjs.Length; i++)
			{
				seqInjs[i].LoadFromFile(binaryReader_1);
			}
			seqAlyOpt.LoadFromFile(binaryReader_1);
		}
		else
		{
			Class49.smethod_33(b);
		}
	}

	public void LoadFromObject(SeqAly seqAly)
	{
		Array.Resize(ref seqInjs, seqAly.seqInjs.Length);
		for (int i = 0; i < seqInjs.Length; i++)
		{
			if (seqInjs[i] == null)
			{
				seqInjs[i] = new Injection();
			}
			seqInjs[i].LoadFromObject(seqAly.seqInjs[i]);
		}
		seqAlyOpt.LoadFromObject(seqAly.seqAlyOpt);
	}

	protected override void saveToFile(BinaryWriter binaryWriter_1)
	{
		binaryWriter_1.Write(Class49.smethod_36());
		binaryWriter_1.Write(seqInjs.Length);
		for (int i = 0; i < seqInjs.Length; i++)
		{
			seqInjs[i].SaveToFile(binaryWriter_1);
		}
		seqAlyOpt.SaveToFile(binaryWriter_1);
	}

	public void SetSeqInjsNum(int int_0)
	{
		Array.Resize(ref seqInjs, int_0);
		for (int i = 0; i < seqInjs.Length; i++)
		{
			if (seqInjs[i] == null)
			{
				seqInjs[i] = new Injection();
			}
		}
	}
}
