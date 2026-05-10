using System;
using System.IO;
using System.Runtime.InteropServices;

namespace IBrainChrom2018;

[Serializable]
[StructLayout(LayoutKind.Sequential)]
public class LcGradient
{
	public GrdtOpt gradientOption = new GrdtOpt();

	public GradientRow[] gradientRows = new GradientRow[0];

	public IdleStateProc idleStateProc = IdleStateProc.Initial;

	public bool lcUse;

	public float sbFlowRate = 1f;

	public float sbPersist = 0.5f;

	public float sbTimeTo = 2f;

	public void Init()
	{
		Array.Resize(ref gradientRows, 2);
		SetGradientRow(0, 0f, 1f, 0f, 0f, 0f, 1f);
		SetGradientRow(1, 95f, 1f, 0f, 0f, 0f, 1f);
	}

	public LcGradient Copy()
	{
		LcGradient lcGradient = new LcGradient();
		lcGradient.lcUse = lcUse;
		Array.Resize(ref lcGradient.gradientRows, gradientRows.Length);
		for (int i = 0; i < lcGradient.gradientRows.Length; i++)
		{
			lcGradient.gradientRows[i].time = gradientRows[i].time;
			lcGradient.gradientRows[i].float_0 = gradientRows[i].float_0;
			lcGradient.gradientRows[i].float_1 = gradientRows[i].float_1;
			lcGradient.gradientRows[i].float_2 = gradientRows[i].float_2;
			lcGradient.gradientRows[i].float_3 = gradientRows[i].float_3;
			lcGradient.gradientRows[i].flow = gradientRows[i].flow;
		}
		lcGradient.sbFlowRate = sbFlowRate;
		lcGradient.sbTimeTo = sbTimeTo;
		lcGradient.sbPersist = sbPersist;
		lcGradient.idleStateProc = idleStateProc;
		lcGradient.gradientOption = gradientOption.Copy();
		return lcGradient;
	}

	public void LoadFromObject(LcGradient lcGradient)
	{
		lcUse = lcGradient.lcUse;
		Array.Resize(ref gradientRows, lcGradient.gradientRows.Length);
		for (int i = 0; i < gradientRows.Length; i++)
		{
			gradientRows[i].time = lcGradient.gradientRows[i].time;
			gradientRows[i].float_0 = lcGradient.gradientRows[i].float_0;
			gradientRows[i].float_1 = lcGradient.gradientRows[i].float_1;
			gradientRows[i].float_2 = lcGradient.gradientRows[i].float_2;
			gradientRows[i].float_3 = lcGradient.gradientRows[i].float_3;
			gradientRows[i].flow = lcGradient.gradientRows[i].flow;
		}
		sbFlowRate = lcGradient.sbFlowRate;
		sbTimeTo = lcGradient.sbTimeTo;
		sbPersist = lcGradient.sbPersist;
		idleStateProc = lcGradient.idleStateProc;
		gradientOption.LoadFromObject(lcGradient.gradientOption);
	}

	public void SetGradientRow(int index, float time, float float_0, float float_1, float float_2, float float_3, float flow)
	{
		if (index >= 0 && index < gradientRows.Length)
		{
			gradientRows[index].time = time;
			gradientRows[index].float_0 = float_0;
			gradientRows[index].float_1 = float_1;
			gradientRows[index].float_2 = float_2;
			gradientRows[index].float_3 = float_3;
			gradientRows[index].flow = flow;
		}
	}

	public void LoadFromFile(BinaryReader binaryReader_0)
	{
		lcUse = binaryReader_0.ReadBoolean();
		Array.Resize(ref gradientRows, binaryReader_0.ReadInt32());
		for (int i = 0; i < gradientRows.Length; i++)
		{
			gradientRows[i].LoadFromFile(binaryReader_0);
		}
		sbFlowRate = binaryReader_0.ReadSingle();
		sbTimeTo = binaryReader_0.ReadSingle();
		sbPersist = binaryReader_0.ReadSingle();
		idleStateProc = (IdleStateProc)binaryReader_0.ReadByte();
		gradientOption.LoadFromFile(binaryReader_0);
	}

	public void SaveToFile(BinaryWriter binaryWriter_0)
	{
		binaryWriter_0.Write(lcUse);
		binaryWriter_0.Write(gradientRows.Length);
		for (int i = 0; i < gradientRows.Length; i++)
		{
			gradientRows[i].SaveToFile(binaryWriter_0);
		}
		binaryWriter_0.Write(sbFlowRate);
		binaryWriter_0.Write(sbTimeTo);
		binaryWriter_0.Write(sbPersist);
		binaryWriter_0.Write((byte)idleStateProc);
		gradientOption.SaveToFile(binaryWriter_0);
	}
}
