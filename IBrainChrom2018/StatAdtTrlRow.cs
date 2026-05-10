using System;
using System.IO;

namespace IBrainChrom2018;

public class StatAdtTrlRow
{
	private const string string_0 = "版本 ";

	private const string string_1 = "Ver. ";

	public string analyst = "";

	public ATArea atArea;

	public ATResult atResult;

	public DateTime atTime = DateTime.Now;

	public ATType atType;

	public string descript = "";

	public string instruName = "";

	public int pgNo;

	public string sTag = "";

	public string version = sVersion + Class49.smethod_37();

	private static string sVersion => Class49.sysLanguage_0 switch
	{
		SysLanguage.CN => "版本 ", 
		SysLanguage.EN => "Ver. ", 
		_ => "", 
	};

	public void LoadFromFile(BinaryReader binaryReader_0)
	{
		pgNo = binaryReader_0.ReadInt32();
		atResult = (ATResult)binaryReader_0.ReadByte();
		atTime = DateTime.FromBinary(binaryReader_0.ReadInt64());
		atType = (ATType)binaryReader_0.ReadByte();
		analyst = binaryReader_0.ReadString();
		instruName = binaryReader_0.ReadString();
		atArea = (ATArea)binaryReader_0.ReadByte();
		descript = binaryReader_0.ReadString();
		version = binaryReader_0.ReadString();
		sTag = binaryReader_0.ReadString();
	}

	public void SaveToFile(BinaryWriter binaryWriter_0)
	{
		binaryWriter_0.Write(pgNo);
		binaryWriter_0.Write((byte)atResult);
		binaryWriter_0.Write(atTime.ToBinary());
		binaryWriter_0.Write((byte)atType);
		binaryWriter_0.Write(analyst);
		binaryWriter_0.Write(instruName);
		binaryWriter_0.Write((byte)atArea);
		binaryWriter_0.Write(descript);
		binaryWriter_0.Write(version);
		binaryWriter_0.Write(sTag);
	}
}
