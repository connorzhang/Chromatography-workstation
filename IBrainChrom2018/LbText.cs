using System;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Runtime.Serialization.Formatters.Binary;
using System.Web.Script.Serialization;
using System.Xml.Serialization;

namespace IBrainChrom2018;

[Serializable]
[StructLayout(LayoutKind.Sequential)]
public class LbText : DisLabel
{
	[NonSerialized]
	[XmlIgnore]
	[ScriptIgnore]
	public Font font = new Font(FontFamily.GenericSansSerif, 8f);

	public SizeF szText;

	public string text = "";

	public override void LoadFromFile(BinaryReader binaryReader_0)
	{
		BinaryFormatter binaryFormatter = new BinaryFormatter();
		base.LoadFromFile(binaryReader_0);
		font = (Font)binaryFormatter.Deserialize(binaryReader_0.BaseStream);
		text = binaryReader_0.ReadString();
	}

	public override void LoadFromObject(object object_0)
	{
		base.LoadFromObject(object_0);
		if (object_0 is LbText)
		{
			LbText lbText = object_0 as LbText;
			font = (Font)lbText.font.Clone();
			text = lbText.text;
		}
	}

	public override void SaveToFile(BinaryWriter binaryWriter_0)
	{
		BinaryFormatter binaryFormatter = new BinaryFormatter();
		base.SaveToFile(binaryWriter_0);
		binaryFormatter.Serialize(binaryWriter_0.BaseStream, font);
		binaryWriter_0.Write(text);
	}
}
