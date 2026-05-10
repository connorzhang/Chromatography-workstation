using System;
using System.IO;
using System.Runtime.InteropServices;

namespace IBrainChrom2018;

[Serializable]
[StructLayout(LayoutKind.Sequential)]
public class GrdtOpt
{
	public bool hasSolvent1 = true;

	public bool hasSolvent2;

	public bool hasSolvent3;

	public bool hasSolvent4;

	public string solvent1Name = "A1";

	public string solvent2Name = "B";

	public string solvent3Name = "C";

	public string solvent4Name = "D";

	public int SolventNum
	{
		get
		{
			int num = 0;
			if (hasSolvent1)
			{
				num++;
			}
			if (hasSolvent2)
			{
				num++;
			}
			if (hasSolvent3)
			{
				num++;
			}
			if (hasSolvent4)
			{
				num++;
			}
			return num;
		}
	}

	public void LoadFromObject(GrdtOpt gradientOption)
	{
		hasSolvent1 = gradientOption.hasSolvent1;
		hasSolvent2 = gradientOption.hasSolvent2;
		hasSolvent3 = gradientOption.hasSolvent3;
		hasSolvent4 = gradientOption.hasSolvent4;
		solvent1Name = gradientOption.solvent1Name;
		solvent2Name = gradientOption.solvent2Name;
		solvent3Name = gradientOption.solvent3Name;
		solvent4Name = gradientOption.solvent4Name;
	}

	public GrdtOpt Copy()
	{
		GrdtOpt grdtOpt = new GrdtOpt();
		grdtOpt.hasSolvent1 = hasSolvent1;
		grdtOpt.hasSolvent2 = hasSolvent2;
		grdtOpt.hasSolvent3 = hasSolvent3;
		grdtOpt.hasSolvent4 = hasSolvent4;
		grdtOpt.solvent1Name = solvent1Name;
		grdtOpt.solvent2Name = solvent2Name;
		grdtOpt.solvent3Name = solvent3Name;
		grdtOpt.solvent4Name = solvent4Name;
		return grdtOpt;
	}

	public void LoadFromFile(BinaryReader binaryReader_0)
	{
		hasSolvent1 = binaryReader_0.ReadBoolean();
		hasSolvent2 = binaryReader_0.ReadBoolean();
		hasSolvent3 = binaryReader_0.ReadBoolean();
		hasSolvent4 = binaryReader_0.ReadBoolean();
		solvent1Name = binaryReader_0.ReadString();
		solvent2Name = binaryReader_0.ReadString();
		solvent3Name = binaryReader_0.ReadString();
		solvent4Name = binaryReader_0.ReadString();
	}

	public void SaveToFile(BinaryWriter binaryWriter_0)
	{
		binaryWriter_0.Write(hasSolvent1);
		binaryWriter_0.Write(hasSolvent2);
		binaryWriter_0.Write(hasSolvent3);
		binaryWriter_0.Write(hasSolvent4);
		binaryWriter_0.Write(solvent1Name);
		binaryWriter_0.Write(solvent2Name);
		binaryWriter_0.Write(solvent3Name);
		binaryWriter_0.Write(solvent4Name);
	}
}
