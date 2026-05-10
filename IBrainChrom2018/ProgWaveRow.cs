using System;
using System.IO;
using System.Runtime.InteropServices;

namespace IBrainChrom2018;

[Serializable]
[StructLayout(LayoutKind.Sequential)]
public class ProgWaveRow : IComparable
{
	private float time;

	private int wave;

	public float Time
	{
		get
		{
			return time;
		}
		set
		{
			time = value;
		}
	}

	public int Wave
	{
		get
		{
			return wave;
		}
		set
		{
			wave = value;
		}
	}

	public ProgWaveRow()
	{
	}

	public ProgWaveRow(float time, int wave)
	{
		Time = time;
		Wave = wave;
	}

	public ProgWaveRow Copy()
	{
		ProgWaveRow progWaveRow = new ProgWaveRow();
		progWaveRow.time = time;
		progWaveRow.wave = wave;
		return progWaveRow;
	}

	public void SaveToFile(BinaryWriter binaryWriter_0)
	{
		binaryWriter_0.Write(Time);
		binaryWriter_0.Write(Wave);
	}

	public void LoadFromFile(BinaryReader binaryReader_0)
	{
		Time = binaryReader_0.ReadSingle();
		Wave = binaryReader_0.ReadInt32();
	}

	public int CompareTo(object target)
	{
		if (target is ProgWaveRow)
		{
			float value = ((ProgWaveRow)target).Time;
			return Time.CompareTo(value);
		}
		return 0;
	}

	public static ProgWaveRow[] NewArray(int count)
	{
		ProgWaveRow[] array = new ProgWaveRow[count];
		for (int i = 0; i < count; i++)
		{
			array[i] = new ProgWaveRow();
		}
		return array;
	}

	public static void NewArray(ref ProgWaveRow[] list, int count)
	{
		list = new ProgWaveRow[count];
		for (int i = 0; i < count; i++)
		{
			list[i] = new ProgWaveRow();
		}
	}
}
