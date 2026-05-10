using System;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;

namespace IBrainChrom2018;

[Serializable]
[StructLayout(LayoutKind.Sequential)]
public class DisLabel
{
	public Color color_0 = Color.Purple;

	public PointF disPt;

	public int int_0;

	public PointF pointF_0;

	public float float_0;

	public bool selected;

	public PointF[] pointF_1 = new PointF[5];

	public DisLabel()
	{
		disPt.X = float.NaN;
	}

	public virtual void LoadFromFile(BinaryReader binaryReader_0)
	{
		color_0 = Color.FromArgb(binaryReader_0.ReadInt32());
		pointF_0.X = binaryReader_0.ReadSingle();
		pointF_0.Y = binaryReader_0.ReadSingle();
		int_0 = binaryReader_0.ReadInt32();
	}

	public virtual void LoadFromObject(object object_0)
	{
		if (object_0 is DisLabel)
		{
			DisLabel disLabel = object_0 as DisLabel;
			color_0 = disLabel.color_0;
			pointF_0 = disLabel.pointF_0;
			int_0 = disLabel.int_0;
		}
	}

	public virtual void SaveToFile(BinaryWriter binaryWriter_0)
	{
		binaryWriter_0.Write(color_0.ToArgb());
		binaryWriter_0.Write(pointF_0.X);
		binaryWriter_0.Write(pointF_0.Y);
		binaryWriter_0.Write(int_0);
	}
}
