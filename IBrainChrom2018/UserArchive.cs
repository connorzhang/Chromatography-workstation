using System;
using System.IO;
using System.Runtime.InteropServices;

namespace IBrainChrom2018;

[Serializable]
[StructLayout(LayoutKind.Sequential)]
public class UserArchive
{
	public ChromInfo chromInfo = new ChromInfo();

	public Integration integ = new Integration();

	public LbLine[] lbLines = new LbLine[0];

	public LbText[] lbTexts = new LbText[0];

	public DateTime openTime = DateTime.Now;

	public string remark = "";

	public DateTime saveTime = DateTime.Now;

	public string userName = "";

	public void LoadFromFile(BinaryReader binaryReader_0)
	{
		integ.Reset();
		userName = binaryReader_0.ReadString();
		openTime = DateTime.FromBinary(binaryReader_0.ReadInt64());
		saveTime = DateTime.FromBinary(binaryReader_0.ReadInt64());
		chromInfo.LoadFromFile(binaryReader_0);
		integ.LoadFromFile(binaryReader_0);
		Array.Resize(ref lbTexts, binaryReader_0.ReadInt32());
		for (int i = 0; i < lbTexts.Length; i++)
		{
			lbTexts[i] = new LbText();
			lbTexts[i].LoadFromFile(binaryReader_0);
		}
		Array.Resize(ref lbLines, binaryReader_0.ReadInt32());
		for (int j = 0; j < lbLines.Length; j++)
		{
			lbLines[j] = new LbLine();
			lbLines[j].LoadFromFile(binaryReader_0);
		}
		remark = binaryReader_0.ReadString();
	}

	public void SaveToFile(BinaryWriter binaryWriter_0)
	{
		binaryWriter_0.Write(userName);
		binaryWriter_0.Write(openTime.ToBinary());
		binaryWriter_0.Write(saveTime.ToBinary());
		chromInfo.SaveToFile(binaryWriter_0);
		integ.SaveToFile(binaryWriter_0);
		binaryWriter_0.Write(lbTexts.Length);
		for (int i = 0; i < lbTexts.Length; i++)
		{
			lbTexts[i].SaveToFile(binaryWriter_0);
		}
		binaryWriter_0.Write(lbLines.Length);
		for (int j = 0; j < lbLines.Length; j++)
		{
			lbLines[j].SaveToFile(binaryWriter_0);
		}
		binaryWriter_0.Write(remark);
	}

	public void SL_lbLines(bool load, ref LbLine[] lines)
	{
		if (load)
		{
			Array.Resize(ref lbLines, lines.Length);
			for (int i = 0; i < lbLines.Length; i++)
			{
				if (lbLines[i] == null)
				{
					lbLines[i] = new LbLine();
				}
				lbLines[i].LoadFromObject(lines[i]);
			}
			return;
		}
		Array.Resize(ref lines, lbLines.Length);
		for (int j = 0; j < lines.Length; j++)
		{
			if (lines[j] == null)
			{
				lines[j] = new LbLine();
			}
			lines[j].LoadFromObject(lbLines[j]);
		}
	}

	public void SL_lbTexts(bool load, ref LbText[] texts)
	{
		if (load)
		{
			Array.Resize(ref lbTexts, texts.Length);
			for (int i = 0; i < lbTexts.Length; i++)
			{
				if (lbTexts[i] == null)
				{
					lbTexts[i] = new LbText();
				}
				lbTexts[i].LoadFromObject(texts[i]);
			}
			return;
		}
		Array.Resize(ref texts, lbTexts.Length);
		for (int j = 0; j < texts.Length; j++)
		{
			if (texts[j] == null)
			{
				texts[j] = new LbText();
			}
			texts[j].LoadFromObject(lbTexts[j]);
		}
	}
}
