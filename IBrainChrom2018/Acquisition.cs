using System;
using System.IO;
using System.Runtime.InteropServices;

namespace IBrainChrom2018;

[Serializable]
[StructLayout(LayoutKind.Sequential)]
public class Acquisition : IArrayBase
{
	public float acqRange;

	public float acqRate;

	public float AcqRange
	{
		get
		{
			return acqRange;
		}
		set
		{
			acqRange = value;
		}
	}

	public float AcqRate
	{
		get
		{
			return acqRate;
		}
		set
		{
			acqRate = value;
		}
	}

	public Acquisition()
	{
		acqRange = 2500f;
		acqRate = 30f;
	}

	public Acquisition Copy()
	{
		Acquisition acquisition = new Acquisition();
		acquisition.acqRange = acqRange;
		acquisition.acqRate = acqRate;
		return acquisition;
	}

	public void SaveToFile(BinaryWriter binaryWriter_0)
	{
		binaryWriter_0.Write(AcqRange);
		binaryWriter_0.Write(AcqRate);
	}

	public void LoadFromFile(BinaryReader binaryReader_0)
	{
		AcqRange = binaryReader_0.ReadSingle();
		AcqRate = binaryReader_0.ReadSingle();
	}
}
