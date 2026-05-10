using System;
using System.IO;
using System.Runtime.InteropServices;

namespace IBrainChrom2018;

[Serializable]
[StructLayout(LayoutKind.Sequential)]
public class GcProgTemp
{
	public float initHoldTime;

	public ProgTRow[] progTempRows = new ProgTRow[16];

	public float[] SetT6 = new float[6];

	public void Init()
	{
		initHoldTime = 15f;
		progTempRows[0].upRate = 2f;
		progTempRows[0].endTemp = 120f;
		progTempRows[0].holdTime = 20f;
		progTempRows[1].upRate = 3f;
		progTempRows[1].endTemp = 140f;
		progTempRows[1].holdTime = 20f;
	}

	public void LoadFromObject(GcProgTemp gcProgTemp)
	{
		SetT6 = (float[])gcProgTemp.SetT6.Clone();
		initHoldTime = gcProgTemp.initHoldTime;
		Array.Resize(ref progTempRows, gcProgTemp.progTempRows.Length);
		for (int i = 0; i < progTempRows.Length; i++)
		{
			progTempRows[i] = gcProgTemp.progTempRows[i];
		}
	}

	public GcProgTemp Copy()
	{
		GcProgTemp gcProgTemp = new GcProgTemp();
		gcProgTemp.SetT6 = (float[])SetT6.Clone();
		gcProgTemp.initHoldTime = initHoldTime;
		Array.Resize(ref gcProgTemp.progTempRows, progTempRows.Length);
		for (int i = 0; i < gcProgTemp.progTempRows.Length; i++)
		{
			gcProgTemp.progTempRows[i] = progTempRows[i];
		}
		return gcProgTemp;
	}

	public void LoadFromFile(BinaryReader binaryReader_0)
	{
		for (int i = 0; i < SetT6.Length; i++)
		{
			SetT6[i] = binaryReader_0.ReadSingle();
		}
		initHoldTime = binaryReader_0.ReadSingle();
		Array.Resize(ref progTempRows, binaryReader_0.ReadInt32());
		for (int j = 0; j < progTempRows.Length; j++)
		{
			progTempRows[j].LoadFromFile(binaryReader_0);
		}
	}

	public void SaveToFile(BinaryWriter binaryWriter_0)
	{
		for (int i = 0; i < SetT6.Length; i++)
		{
			binaryWriter_0.Write(SetT6[i]);
		}
		binaryWriter_0.Write(initHoldTime);
		binaryWriter_0.Write(progTempRows.Length);
		for (int j = 0; j < progTempRows.Length; j++)
		{
			progTempRows[j].SaveToFile(binaryWriter_0);
		}
	}
}
