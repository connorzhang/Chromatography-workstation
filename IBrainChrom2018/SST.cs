using System;
using System.IO;

namespace IBrainChrom2018;

public class SST
{
	public const string fnExt = ".sst";

	public string fName = "";

	public string fullName = "";

	public SSTCmpd[] sstCmpds = new SSTCmpd[0];

	public SSTParas sstParas = new SSTParas();

	public SSTCmpd[] ChkCmpds
	{
		get
		{
			SSTCmpd[] array = new SSTCmpd[0];
			for (int i = 0; i < sstCmpds.Length; i++)
			{
				if (sstCmpds[i].used)
				{
					int num = array.Length;
					Array.Resize(ref array, num + 1);
					array[num] = sstCmpds[i];
				}
			}
			return array;
		}
	}

	public void Calcu(Chromatogram[] chroms)
	{
		for (int i = 0; i < sstCmpds.Length; i++)
		{
			sstCmpds[i].AddValue(chroms, sstParas.criterion);
			sstCmpds[i].Calcu();
		}
	}

	public void ClearParas()
	{
		for (int i = 0; i < sstCmpds.Length; i++)
		{
			sstCmpds[i].ResetParas();
		}
	}

	public bool LoadFromFile(string fileName)
	{
		if (!File.Exists(fileName))
		{
			return false;
		}
		FileInfo fileInfo_ = null;
		FileStream fileStream_ = null;
		BinaryReader binaryReader_ = null;
		try
		{
			Class49.OpenBinaryReader(fileName, out fileInfo_, out fileStream_, out binaryReader_);
			fullName = fileInfo_.FullName;
			fName = fileInfo_.Name.Replace(fileInfo_.Extension, "");
			sstParas.LoadFromFile(binaryReader_);
			Array.Resize(ref sstCmpds, binaryReader_.ReadInt32());
			for (int i = 0; i < sstCmpds.Length; i++)
			{
				if (sstCmpds[i] == null)
				{
					sstCmpds[i] = new SSTCmpd();
				}
				sstCmpds[i].LoadFromFile(binaryReader_);
			}
		}
		finally
		{
			Class49.FileStreamClose(ref fileStream_, ref binaryReader_);
		}
		return true;
	}

	public void ResetRecs()
	{
		Array.Resize(ref sstCmpds, 0);
	}

	public SSTCmpd RetCmpds(float RT)
	{
		for (int i = 0; i < sstCmpds.Length; i++)
		{
			if (sstCmpds[i].RT == RT)
			{
				return sstCmpds[i];
			}
		}
		return null;
	}

	public void SaveToFile(string fileName)
	{
		if (!fileName.EndsWith(".sst"))
		{
			fileName += ".sst";
		}
		FileInfo fileInfo_ = null;
		FileStream fileStream_ = null;
		BinaryWriter binaryWriter_ = null;
		try
		{
			Class49.OpenBinaryWriter(fileName, out fileInfo_, out fileStream_, out binaryWriter_);
			fullName = fileInfo_.FullName;
			fName = fileInfo_.Name.Replace(fileInfo_.Extension, "");
			sstParas.SaveToFile(binaryWriter_);
			binaryWriter_.Write(sstCmpds.Length);
			for (int i = 0; i < sstCmpds.Length; i++)
			{
				sstCmpds[i].SaveToFile(binaryWriter_);
			}
		}
		finally
		{
			Class49.FileStreamClose(ref fileStream_, ref binaryWriter_);
		}
	}
}
