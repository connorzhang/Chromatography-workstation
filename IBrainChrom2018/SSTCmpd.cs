using System;
using System.IO;

namespace IBrainChrom2018;

public class SSTCmpd
{
	public string fromCali = "";

	public string name = "";

	public float RT;

	public SstItem[] sstItems = new SstItem[0];

	public SSTResult sstResult;

	public bool used;

	public void AddValue(Chromatogram[] chroms, SSTCriterion criterion)
	{
		method_1();
		if (used)
		{
			for (int i = 0; i < sstItems.Length; i++)
			{
				sstItems[i].AddValue(chroms, name, criterion);
			}
		}
	}

	public void Calcu()
	{
		if (!used)
		{
			sstResult = SSTResult.None;
			return;
		}
		for (int i = 0; i < sstItems.Length; i++)
		{
			sstItems[i].Calcu();
		}
		if (sstItems.Length == 0)
		{
			sstResult = SSTResult.Unknown;
			return;
		}
		for (int j = 0; j < sstItems.Length; j++)
		{
			if (sstItems[j].result == SSTResult.Fail)
			{
				sstResult = SSTResult.Fail;
				return;
			}
		}
		sstResult = SSTResult.Success;
	}

	private void method_0(string string_0)
	{
		for (int i = 0; i < sstItems.Length; i++)
		{
			if (!(sstItems[i].item == string_0))
			{
				continue;
			}
			if (!float.IsNaN(sstItems[i].upperLimit) && !float.IsNaN(sstItems[i].lowerLimit))
			{
				if (sstItems[i].upperLimit < sstItems[i].lowerLimit)
				{
					float upperLimit = sstItems[i].upperLimit;
					sstItems[i].upperLimit = sstItems[i].lowerLimit;
					sstItems[i].lowerLimit = upperLimit;
				}
				if (sstItems[i].upperLimit == sstItems[i].lowerLimit)
				{
					sstItems[i].lowerLimit = float.NaN;
				}
			}
			if (float.IsNaN(sstItems[i].upperLimit) && float.IsNaN(sstItems[i].lowerLimit) && float.IsNaN(sstItems[i].rsdPerLimit))
			{
				int num = sstItems.Length;
				sstItems[i] = sstItems[num - 1];
				Array.Resize(ref sstItems, num - 1);
			}
			break;
		}
	}

	private void method_1()
	{
		for (int i = 0; i < sstItems.Length; i++)
		{
			sstItems[i].ClearValues();
		}
	}

	public SSTResult extResult(Chromatogram chrom, SSTCriterion criterion)
	{
		if (sstItems.Length == 0)
		{
			return SSTResult.None;
		}
		bool flag = false;
		for (int i = 0; i < chrom.RltPeaks.Length; i++)
		{
			if (!(chrom.RltPeaks[i].name == name))
			{
				continue;
			}
			flag = true;
			for (int j = 0; j < sstItems.Length; j++)
			{
				float value = SstItem.getValue(chrom.RltPeaks[i], sstItems[j].item, criterion);
				if (sstItems[j].extResult(value) == SSTResult.Fail)
				{
					return SSTResult.Fail;
				}
			}
		}
		if (!flag)
		{
			return SSTResult.None;
		}
		return SSTResult.Success;
	}

	public SstItem GetItem(string item)
	{
		for (int i = 0; i < sstItems.Length; i++)
		{
			if (sstItems[i].item == item)
			{
				return sstItems[i];
			}
		}
		return null;
	}

	public float GetLowerLimit(string item)
	{
		return GetItem(item)?.lowerLimit ?? float.NaN;
	}

	public float GetRsdPerLimit(string item)
	{
		return GetItem(item)?.rsdPerLimit ?? float.NaN;
	}

	public float GetUpperLimit(string item)
	{
		return GetItem(item)?.upperLimit ?? float.NaN;
	}

	public void LoadFromFile(BinaryReader binaryReader_0)
	{
		used = binaryReader_0.ReadBoolean();
		name = binaryReader_0.ReadString();
		RT = binaryReader_0.ReadSingle();
		fromCali = binaryReader_0.ReadString();
		Array.Resize(ref sstItems, binaryReader_0.ReadInt32());
		for (int i = 0; i < sstItems.Length; i++)
		{
			if (sstItems[i] == null)
			{
				sstItems[i] = new SstItem();
			}
			sstItems[i].LoadFromFile(binaryReader_0);
		}
	}

	public void ResetParas()
	{
		Array.Resize(ref sstItems, 0);
	}

	private SstItem method_2(string string_0)
	{
		for (int i = 0; i < sstItems.Length; i++)
		{
			if (sstItems[i].item == string_0)
			{
				return sstItems[i];
			}
		}
		int num = sstItems.Length;
		Array.Resize(ref sstItems, num + 1);
		sstItems[num] = new SstItem();
		sstItems[num].item = string_0;
		return sstItems[num];
	}

	public void SaveToFile(BinaryWriter binaryWriter_0)
	{
		binaryWriter_0.Write(used);
		binaryWriter_0.Write(name);
		binaryWriter_0.Write(RT);
		binaryWriter_0.Write(fromCali);
		binaryWriter_0.Write(sstItems.Length);
		for (int i = 0; i < sstItems.Length; i++)
		{
			sstItems[i].SaveToFile(binaryWriter_0);
		}
	}

	public void SetLowerLimit(string item, float lowerLimit)
	{
		method_2(item).lowerLimit = lowerLimit;
		method_0(item);
	}

	public void SetRsdPerLimit(string item, float rsdPerLimit)
	{
		method_2(item).rsdPerLimit = rsdPerLimit;
		method_0(item);
	}

	public void SetUpperLimit(string item, float upperLimit)
	{
		method_2(item).upperLimit = upperLimit;
		method_0(item);
	}
}
