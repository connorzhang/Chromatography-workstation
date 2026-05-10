using System;
using System.Drawing;
using System.IO;

namespace IBrainChrom2018;

[Serializable]
public struct FuncPt
{
	public float responseF;

	public float amountF;

	public bool IsValid => responseF != 0f && amountF != 0f;

	public bool RespFValid => responseF != 0f;

	public PointF AsPointF => new PointF(amountF, responseF);

	public void SaveToFile(BinaryWriter binaryWriter_0)
	{
		binaryWriter_0.Write(responseF);
		binaryWriter_0.Write(amountF);
	}

	public void LoadFromFile(BinaryReader binaryReader_0)
	{
		responseF = binaryReader_0.ReadSingle();
		amountF = binaryReader_0.ReadSingle();
	}
}
