using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Xml.Serialization;
using IBrainChrom2018.ChromFile;

namespace IBrainChrom2018;

[Serializable]
[StructLayout(LayoutKind.Sequential)]
[XmlInclude(typeof(MtdSetup))]
[XmlInclude(typeof(ChromComponentRow))]
[XmlInclude(typeof(timeProgram))]
[XmlInclude(typeof(even))]
public class ChartParaOpera : IBaseFileMgr
{
	public MtdSetup mtdMgr;

	public string TemplatePath;

	public List<ChromComponentRow> componentList;

	public Integration Integ;

	public EnumChannelDetectionMethod cnlDetectMethod;

	public EnumChannelBasisQuantity cnlBasisQuantity;

	public List<timeProgram> tProgram;

	public List<even> evenPara;

	public bool FileNameAquipName;

	public bool FileNameChannelName;

	public bool FileNameDateTime;

	public string FileUserSet;

	public bool FileNameAutoInject;

	public bool InjectIndex;

	public bool UseUserZeroTime;

	public double ZeroTime;

	public double ZeroTimeLeft;

	public double ZeroTimeRight;

	public ChartParaOpera()
	{
		m_strExt = "mtd";
		m_strFileTypeName = Lang.PS("方法参数文件");
		mtdMgr = new MtdSetup();
		TemplatePath = "";
		FileNameAquipName = true;
		FileNameChannelName = true;
		FileNameDateTime = true;
		FileUserSet = "";
		FileNameAutoInject = false;
		InjectIndex = true;
		UseUserZeroTime = false;
		ZeroTime = 0.0;
		ZeroTimeLeft = 0.1;
		ZeroTimeRight = 0.1;
		tProgram = IArrayBase.NewArray<timeProgram>(8);
		evenPara = IArrayBase.NewArray<even>(4);
		componentList = IArrayBase.NewArray<ChromComponentRow>(4);
	}

	public void LoadMtdFile(string fileName)
	{
		if (File.Exists(fileName))
		{
			if (mtdMgr == null)
			{
				mtdMgr = new MtdSetup();
			}
			mtdMgr.LoadFromFile(fileName);
			ChromDeviceListMgr chromDeviceListMgr = ChromDeviceListMgr.Create();
			chromDeviceListMgr.formMain.MainmstSet.ReadFromMtdMgr(mtdMgr);
		}
	}

	public void LoadCalFile(string fileName)
	{
		if (File.Exists(fileName))
		{
			if (mtdMgr == null)
			{
				mtdMgr = new MtdSetup();
			}
			ChromDeviceListMgr chromDeviceListMgr = ChromDeviceListMgr.Create();
			chromDeviceListMgr.formMain.MainmstSet.OpenCaliGnlFile(fileName);
		}
	}
}
