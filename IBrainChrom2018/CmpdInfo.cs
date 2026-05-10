using System;
using System.Drawing;
using System.IO;

namespace IBrainChrom2018;

[Serializable]
public struct CmpdInfo
{
	public string name;

	public float retainTime;

	public float leftWindow;

	public float rightWindow;

	public float HheatValue;

	public float LheatValue;

	public float[] BLfloat;

	public string[] BLString;

	public Color color;

	public bool isIstd;

	public string istdCmpd;

	public int sl_IstdCmpdNo;

	public RespStyle respStyle;

	public float freeRespFactor;

	public float CriticalAmount;

	public void LoadFromObject(CmpdInfo cmpdInfo)
	{
		name = cmpdInfo.name;
		retainTime = cmpdInfo.retainTime;
		leftWindow = cmpdInfo.leftWindow;
		rightWindow = cmpdInfo.rightWindow;
		color = cmpdInfo.color;
		istdCmpd = cmpdInfo.istdCmpd;
		sl_IstdCmpdNo = cmpdInfo.sl_IstdCmpdNo;
		respStyle = cmpdInfo.respStyle;
		freeRespFactor = cmpdInfo.freeRespFactor;
		isIstd = cmpdInfo.isIstd;
	}

	public void SaveToFile(BinaryWriter binaryWriter_0)
	{
		if (name == null)
		{
			name = "";
		}
		binaryWriter_0.Write(name);
		binaryWriter_0.Write(retainTime);
		binaryWriter_0.Write(leftWindow);
		binaryWriter_0.Write(rightWindow);
		binaryWriter_0.Write(HheatValue);
		binaryWriter_0.Write(LheatValue);
		for (int i = 0; i < BLfloat.Length; i++)
		{
			binaryWriter_0.Write(BLfloat[i]);
		}
		for (int j = 0; j < BLString.Length; j++)
		{
			binaryWriter_0.Write(BLString[j]);
		}
		binaryWriter_0.Write(color.ToArgb());
		binaryWriter_0.Write(sl_IstdCmpdNo);
		binaryWriter_0.Write((byte)respStyle);
		binaryWriter_0.Write(freeRespFactor);
		binaryWriter_0.Write(isIstd);
	}

	public void LoadFromFile(BinaryReader binaryReader_0)
	{
		name = binaryReader_0.ReadString();
		retainTime = binaryReader_0.ReadSingle();
		leftWindow = binaryReader_0.ReadSingle();
		rightWindow = binaryReader_0.ReadSingle();
		HheatValue = binaryReader_0.ReadSingle();
		LheatValue = binaryReader_0.ReadSingle();
		for (int i = 0; i < BLfloat.Length; i++)
		{
			BLfloat[i] = binaryReader_0.ReadSingle();
		}
		for (int j = 0; j < BLString.Length; j++)
		{
			BLString[j] = binaryReader_0.ReadString();
		}
		color = Color.FromArgb(binaryReader_0.ReadInt32());
		sl_IstdCmpdNo = binaryReader_0.ReadInt32();
		respStyle = (RespStyle)binaryReader_0.ReadByte();
		freeRespFactor = binaryReader_0.ReadSingle();
		isIstd = binaryReader_0.ReadBoolean();
	}

	public void LoadFromFileOld(BinaryReader binaryReader_0)
	{
		name = binaryReader_0.ReadString();
		retainTime = binaryReader_0.ReadSingle();
		leftWindow = binaryReader_0.ReadSingle();
		rightWindow = binaryReader_0.ReadSingle();
		color = Color.FromArgb(binaryReader_0.ReadInt32());
		if (color == Color.FromArgb(0))
		{
			binaryReader_0.ReadSingle();
			color = Color.FromArgb(binaryReader_0.ReadInt32());
		}
		sl_IstdCmpdNo = binaryReader_0.ReadInt32();
		respStyle = (RespStyle)binaryReader_0.ReadByte();
		freeRespFactor = binaryReader_0.ReadSingle();
		isIstd = binaryReader_0.ReadBoolean();
	}
}
