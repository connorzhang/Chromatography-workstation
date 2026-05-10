using System;
using System.IO;
using System.Runtime.InteropServices;
using IBrainChrom2018.Unit;

namespace IBrainChrom2018;

[Serializable]
[StructLayout(LayoutKind.Sequential)]
public class CaliGnlOpt
{
	private FormMainParam frmParam = FormMainParam.Create();

	public CaliDisMode caliDisMode;

	public string cmpdUnit = "g/L";

	public CurveFit curveFit = CurveFit.Linear;

	public string description = "1111";

	public float leftWindow = 0.1f;

	public Original original = Original.With;

	public RecaliMode recaliMode = RecaliMode.Average;

	public RespStyle respStyle;

	public float rightWindow = 0.1f;

	public bool updateRT = true;

	public CaliGnlOpt()
	{
		SystemParam systemParam = SystemParam.Create();
		cmpdUnit = "g/L";
		recaliMode = (RecaliMode)systemParam.iCaliGnlOptReCali;
	}

	public CaliGnlOpt Copy()
	{
		CaliGnlOpt caliGnlOpt = new CaliGnlOpt();
		caliGnlOpt.caliDisMode = caliDisMode;
		caliGnlOpt.cmpdUnit = cmpdUnit;
		caliGnlOpt.curveFit = curveFit;
		caliGnlOpt.leftWindow = leftWindow;
		caliGnlOpt.original = original;
		caliGnlOpt.recaliMode = recaliMode;
		caliGnlOpt.respStyle = respStyle;
		caliGnlOpt.rightWindow = rightWindow;
		caliGnlOpt.updateRT = updateRT;
		return caliGnlOpt;
	}

	public void LoadFromFile(BinaryReader binaryReader_0)
	{
		description = binaryReader_0.ReadString();
		caliDisMode = (CaliDisMode)binaryReader_0.ReadByte();
		cmpdUnit = binaryReader_0.ReadString();
		updateRT = binaryReader_0.ReadBoolean();
		recaliMode = (RecaliMode)binaryReader_0.ReadByte();
		respStyle = (RespStyle)binaryReader_0.ReadByte();
		original = (Original)binaryReader_0.ReadByte();
		curveFit = (CurveFit)binaryReader_0.ReadByte();
		leftWindow = binaryReader_0.ReadSingle();
		rightWindow = binaryReader_0.ReadSingle();
	}

	public void SaveToFile(BinaryWriter binaryWriter_0)
	{
		binaryWriter_0.Write(description);
		binaryWriter_0.Write((byte)caliDisMode);
		binaryWriter_0.Write(cmpdUnit);
		binaryWriter_0.Write(updateRT);
		binaryWriter_0.Write((byte)recaliMode);
		binaryWriter_0.Write((byte)respStyle);
		binaryWriter_0.Write((byte)original);
		binaryWriter_0.Write((byte)curveFit);
		binaryWriter_0.Write(leftWindow);
		binaryWriter_0.Write(rightWindow);
	}
}
