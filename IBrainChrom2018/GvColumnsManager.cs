using System;
using System.IO;

namespace IBrainChrom2018;

public class GvColumnsManager
{
	public string[] colFormats = new string[0];

	public string[] showCols = new string[0];

	public int ShowColsCount => showCols.Length;

	public void Load(BinaryReader binaryReader_0)
	{
		Array.Resize(ref colFormats, binaryReader_0.ReadInt32());
		for (int i = 0; i < colFormats.Length; i++)
		{
			colFormats[i] = binaryReader_0.ReadString();
		}
		Array.Resize(ref showCols, binaryReader_0.ReadInt32());
		for (int j = 0; j < showCols.Length; j++)
		{
			showCols[j] = binaryReader_0.ReadString();
		}
	}

	public void LoadFromObject(GvColumnsManager gvColumnsManager)
	{
		Array.Resize(ref colFormats, gvColumnsManager.colFormats.Length);
		for (int i = 0; i < colFormats.Length; i++)
		{
			colFormats[i] = gvColumnsManager.colFormats[i];
		}
		Array.Resize(ref showCols, gvColumnsManager.showCols.Length);
		for (int j = 0; j < showCols.Length; j++)
		{
			showCols[j] = gvColumnsManager.showCols[j];
		}
	}

	public void Save(BinaryWriter binaryWriter_0)
	{
		binaryWriter_0.Write(colFormats.Length);
		for (int i = 0; i < colFormats.Length; i++)
		{
			binaryWriter_0.Write(colFormats[i]);
		}
		binaryWriter_0.Write(showCols.Length);
		for (int j = 0; j < showCols.Length; j++)
		{
			binaryWriter_0.Write(showCols[j]);
		}
	}
}
