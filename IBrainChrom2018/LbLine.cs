using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Runtime.InteropServices;
using System.Runtime.Serialization.Formatters.Binary;

namespace IBrainChrom2018;

[Serializable]
[StructLayout(LayoutKind.Sequential)]
public class LbLine : DisLabel
{
	public PointF disPt2;

	public PointF pointF_2;

	public DashStyle style = DashStyle.DashDotDot;

	public override void LoadFromFile(BinaryReader binaryReader_0)
	{
		new BinaryFormatter();
		base.LoadFromFile(binaryReader_0);
		style = (DashStyle)binaryReader_0.ReadByte();
		pointF_2.X = binaryReader_0.ReadSingle();
		pointF_2.Y = binaryReader_0.ReadSingle();
	}

	public override void LoadFromObject(object object_0)
	{
		base.LoadFromObject(object_0);
		if (object_0 is LbLine)
		{
			LbLine lbLine = object_0 as LbLine;
			style = lbLine.style;
			pointF_2 = lbLine.pointF_2;
		}
	}

	public override void SaveToFile(BinaryWriter binaryWriter_0)
	{
		new BinaryFormatter();
		base.SaveToFile(binaryWriter_0);
		binaryWriter_0.Write((byte)style);
		binaryWriter_0.Write(pointF_2.X);
		binaryWriter_0.Write(pointF_2.Y);
	}
}
