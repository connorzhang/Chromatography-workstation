using System;
using System.IO;

namespace IBrainChrom2018;

[Serializable]
public struct DisLg
{
	public float lgXBeg;

	public float lgX;

	public float lgYBeg;

	public float lgY;

	public float lgY_Beg;

	public float lgY_;

	public bool Valid_xy => lgX > 0f && lgY > 0f;

	public bool Valid_xy_ => lgX != 0f && lgY_ != 0f;

	public float LgXEnd
	{
		get
		{
			if (lgXBeg >= 0f)
			{
				return lgXBeg + lgX;
			}
			return lgX;
		}
		set
		{
		}
	}

	public float LgYEnd
	{
		get
		{
			if (lgYBeg >= 0f)
			{
				return lgYBeg + lgY;
			}
			return lgY;
		}
		set
		{
		}
	}

	public float LgY_End => lgY_Beg + lgY_;

	public bool Equals(DisLg disLg)
	{
		return lgXBeg == disLg.lgXBeg && lgYBeg == disLg.lgYBeg && lgY_Beg == disLg.lgY_Beg && lgX == disLg.lgX && lgY == disLg.lgY && lgY_ == disLg.lgY_;
	}

	public void LoadFromFile(BinaryReader binaryReader_0)
	{
		try
		{
			lgXBeg = binaryReader_0.ReadSingle();
			lgX = binaryReader_0.ReadSingle();
			lgYBeg = binaryReader_0.ReadSingle();
			lgY = binaryReader_0.ReadSingle();
		}
		catch
		{
		}
	}

	public void SaveToFile(BinaryWriter binaryWriter_0)
	{
		binaryWriter_0.Write(0f);
		binaryWriter_0.Write(lgX);
		binaryWriter_0.Write(lgYBeg);
		binaryWriter_0.Write(lgY);
	}
}
