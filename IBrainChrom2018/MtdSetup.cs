using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Xml.Serialization;
using IBrainChrom2018.ChromFile;
using IBrainChrom2018.Unit;

namespace IBrainChrom2018;

[Serializable]
[StructLayout(LayoutKind.Sequential)]
[XmlInclude(typeof(ChromInfo))]
[XmlInclude(typeof(ChromInfoR))]
[XmlInclude(typeof(Acquisition))]
[XmlInclude(typeof(Integration))]
[XmlInclude(typeof(PrintPara))]
public class MtdSetup : IBaseFileMgr
{
	public bool insDevEnable0 = true;

	public bool insDevEnable1 = false;

	public bool insDevEnable2 = true;

	public bool insDevEnable3 = false;

	public ChromInfo chromInfo = new ChromInfo();

	public CaliGnl caliGnl = new CaliGnl();

	public ChromInfoR chromInfoR = new ChromInfoR();

	public List<Acquisition> dtcAcquisitions = IArrayBase.NewArray<Acquisition>(2);

	public List<Integration> sigIntegrations = IArrayBase.NewArray<Integration>(1);

	public PrintPara printPara = new PrintPara();

	public string strMtdShowName = Lang.PS("[默认]", "[Default]");

	public string strMtdFilePath = "";

	public string strPaFilePath = "";

	[NonSerialized]
	[XmlIgnore]
	private FileInfo fileInfo_0;

	[NonSerialized]
	[XmlIgnore]
	private FileStream fileStream_0;

	[NonSerialized]
	[XmlIgnore]
	private BinaryReader binaryReader_0;

	[NonSerialized]
	[XmlIgnore]
	private BinaryWriter binaryWriter_0;

	[XmlIgnore]
	private int sigIntegsNum
	{
		get
		{
			return sigIntegrations.Count;
		}
		set
		{
			IArrayBase.NewArray(ref sigIntegrations, value);
		}
	}

	public bool IsNull => strMtdShowName == "" || strMtdShowName == Lang.PS("[默认]", "[Default]");

	public MtdSetup()
	{
		m_strExt = "mtd";
		m_strFileTypeName = Lang.PS("方法参数文件");
	}

	public void Copy(MtdSetup mtd)
	{
		insDevEnable0 = mtd.insDevEnable0;
		insDevEnable1 = mtd.insDevEnable1;
		insDevEnable2 = mtd.insDevEnable2;
		insDevEnable3 = mtd.insDevEnable3;
		chromInfo = mtd.chromInfo;
		chromInfoR = mtd.chromInfoR;
		dtcAcquisitions = mtd.dtcAcquisitions;
		sigIntegrations = mtd.sigIntegrations;
		printPara = mtd.printPara;
		strMtdShowName = mtd.strMtdShowName;
		strMtdFilePath = mtd.strMtdFilePath;
		caliGnl = mtd.caliGnl;
	}

	public MtdSetup Copy()
	{
		MtdSetup mtdSetup = new MtdSetup();
		mtdSetup.insDevEnable0 = insDevEnable0;
		mtdSetup.insDevEnable1 = insDevEnable1;
		mtdSetup.insDevEnable2 = insDevEnable2;
		mtdSetup.insDevEnable3 = insDevEnable3;
		mtdSetup.chromInfo = chromInfo.Copy();
		mtdSetup.chromInfoR = chromInfoR.Copy();
		mtdSetup.dtcAcquisitions = new List<Acquisition>(dtcAcquisitions.Count);
		for (int i = 0; i < dtcAcquisitions.Count; i++)
		{
			mtdSetup.dtcAcquisitions.Add(dtcAcquisitions[i].Copy());
		}
		mtdSetup.sigIntegrations = new List<Integration>(sigIntegrations.Count);
		for (int j = 0; j < sigIntegrations.Count; j++)
		{
			mtdSetup.sigIntegrations.Add(sigIntegrations[j].Copy());
		}
		mtdSetup.printPara = printPara.Copy();
		mtdSetup.strMtdShowName = strMtdShowName;
		mtdSetup.strMtdFilePath = strMtdFilePath;
		if (caliGnl != null)
		{
			mtdSetup.caliGnl = caliGnl.Copy();
		}
		return mtdSetup;
	}

	public void Clear()
	{
		sigIntegrations[0].Reset();
		printPara = new PrintPara();
		chromInfo = new ChromInfo();
		chromInfoR = new ChromInfoR();
		caliGnl.Clear();
	}

	public void ResetIntegrations()
	{
		sigIntegrations = IArrayBase.NewArray<Integration>(1);
		sigIntegrations[0].Reset();
	}

	public bool LoadFromFile()
	{
		if (strMtdFilePath == "" || !File.Exists(strMtdFilePath))
		{
			return false;
		}
		return LoadFromFile(strMtdFilePath);
	}

	public bool SaveToFile()
	{
		if (strMtdFilePath == "")
		{
			return SaveToFileAs();
		}
		SaveToFile(strMtdFilePath);
		return true;
	}

	public bool LoadFromFile(string fileName)
	{
		MtdSetup mtdSetup = (MtdSetup)IBaseFileMgr.OpenFile(fileName);
		if (mtdSetup == null)
		{
			bool result = LoadFromFileOld2(fileName);
			LogMgr.Instance.LogWarning("MtdSetup.LoadFromFileOld2");
			return result;
		}
		List<Integration> list = new List<Integration>();
		for (int i = 0; i < mtdSetup.sigIntegrations.Count; i++)
		{
			if ((mtdSetup.sigIntegrations[i].IntegRows.Length != 3 || mtdSetup.sigIntegrations[i].IntegRows[0].oprtStyle != IntegOprtStyle.PeakWidth) && mtdSetup.sigIntegrations[i].IntegRows.Length != 0)
			{
				list.Add(mtdSetup.sigIntegrations[i]);
			}
		}
		if (mtdSetup != null)
		{
			Copy(mtdSetup);
		}
		sigIntegrations = list;
		strMtdFilePath = fileName;
		strMtdShowName = Path.GetFileNameWithoutExtension(fileName);
		return true;
	}

	public bool LoadFromFile(string fileName, InsDeviceManager insDeviceManager)
	{
		return LoadFromFileOld2(fileName, insDeviceManager);
	}

	public void SaveToFile(string fileName)
	{
		strMtdShowName = Path.GetFileNameWithoutExtension(fileName);
		IBaseFileMgr.SaveFile(fileName, this);
		strMtdFilePath = fileName;
		strMtdShowName = Path.GetFileNameWithoutExtension(fileName);
	}

	public bool SaveToFileAs()
	{
		if (IBaseFileMgr.SaveFile(this))
		{
			strMtdFilePath = IBaseFileMgr.m_strFilePath;
			strMtdShowName = Path.GetFileNameWithoutExtension(IBaseFileMgr.m_strFilePath);
			return true;
		}
		return false;
	}

	public void SetDtcAcquisitionRange(int index, float range)
	{
		if (index >= 0 && index < dtcAcquisitions.Count)
		{
			dtcAcquisitions[index].AcqRange = range;
		}
	}

	public void SetDtcAcquisitionRate(int index, float rate)
	{
		if (index >= 0 && index < dtcAcquisitions.Count)
		{
			dtcAcquisitions[index].AcqRate = rate;
		}
	}

	public void Init(Instrument instrument)
	{
		int count = instrument.dtc_Channels.Length;
		dtcAcquisitions = null;
		dtcAcquisitions = IArrayBase.NewArray<Acquisition>(count);
		sigIntegsNum = count;
		SetDtcAcquisitionRange(0, 2500f);
		SetDtcAcquisitionRate(0, 30f);
		SetDtcAcquisitionRange(1, 2500f);
		SetDtcAcquisitionRate(1, 60f);
		if (sigIntegrations.Count != 0)
		{
			sigIntegrations[0].Reset();
		}
		chromInfo.Init(instrument);
		chromInfoR.LcGradient.Init();
		chromInfoR.GcProgTemp.Init();
	}

	internal void SaveToFile(string strMtdFilePath, InsDeviceManager insDevMer)
	{
		if (!strMtdFilePath.EndsWith(".mtd"))
		{
			strMtdFilePath += ".mtd";
		}
		try
		{
			Class49.OpenBinaryWriter(strMtdFilePath, out fileInfo_0, out fileStream_0, out binaryWriter_0);
			this.strMtdFilePath = strMtdFilePath.ToLower();
			strMtdShowName = fileInfo_0.Name.Remove(fileInfo_0.Name.Length - fileInfo_0.Extension.Length);
			SaveToFile(binaryWriter_0);
			insDevMer.SaveToFile(binaryWriter_0, Class49.string_8);
			binaryWriter_0.Write(strPaFilePath);
		}
		finally
		{
			Class49.FileStreamClose(ref fileStream_0, ref binaryWriter_0);
		}
	}

	public void SaveToFile(BinaryWriter binaryWriter_1)
	{
		binaryWriter_1.Write(Class49.string_8);
		binaryWriter_1.Write(strMtdShowName);
		binaryWriter_1.Write(strMtdFilePath);
		if (dtcAcquisitions.Count() > 10)
		{
			binaryWriter_1.Write(10);
			for (int i = 0; i < 10; i++)
			{
				dtcAcquisitions[i].SaveToFile(binaryWriter_1);
			}
		}
		else
		{
			binaryWriter_1.Write(dtcAcquisitions.Count());
			for (int j = 0; j < dtcAcquisitions.Count(); j++)
			{
				dtcAcquisitions[j].SaveToFile(binaryWriter_1);
			}
		}
		binaryWriter_1.Write(sigIntegrations.Count());
		for (int k = 0; k < sigIntegrations.Count(); k++)
		{
			sigIntegrations[k].SaveToFile(binaryWriter_1);
		}
		chromInfoR.SaveToFile(binaryWriter_1);
		chromInfo.SaveToFile(binaryWriter_1);
	}

	public void loadFromFileOld(BinaryReader binaryReader_1)
	{
		strMtdShowName = binaryReader_1.ReadString();
		strMtdFilePath = binaryReader_1.ReadString();
		IArrayBase.NewArray(ref dtcAcquisitions, binaryReader_1.ReadInt32());
		for (int i = 0; i < dtcAcquisitions.Count; i++)
		{
			dtcAcquisitions[i].LoadFromFile(binaryReader_1);
		}
		IArrayBase.NewArray(ref sigIntegrations, binaryReader_1.ReadInt32());
		for (int j = 0; j < sigIntegrations.Count; j++)
		{
			if (sigIntegrations[j] == null)
			{
				sigIntegrations[j] = new Integration();
				sigIntegrations[j].Reset();
			}
			sigIntegrations[j].LoadFromFile(binaryReader_1);
		}
		chromInfoR.LoadFromFile(binaryReader_1);
	}

	public bool LoadFromFileOld2(string fileName, InsDeviceManager insDeviceManager)
	{
		if (!fileName.EndsWith(".mtd"))
		{
			fileName += ".mtd";
		}
		try
		{
			Class49.OpenBinaryReader(fileName, out fileInfo_0, out fileStream_0, out binaryReader_0);
			strMtdFilePath = fileName.ToLower();
			strMtdShowName = fileInfo_0.Name.Remove(fileInfo_0.Name.Length - fileInfo_0.Extension.Length);
			int num = (int)binaryReader_0.BaseStream.Position;
			string svaddr = "";
			loadFromFile(binaryReader_0, ref svaddr);
			if (insDeviceManager == null)
			{
				insDeviceManager = new InsDeviceManager();
			}
			insDeviceManager.ReadFromFile(binaryReader_0, svaddr);
			try
			{
				strPaFilePath = (insDeviceManager.string_0 = binaryReader_0.ReadString());
			}
			catch
			{
			}
			printPara = insDeviceManager.printPara_0;
		}
		catch
		{
			if (fileStream_0 != null)
			{
				Class49.FileStreamClose(ref fileStream_0, ref binaryReader_0);
			}
			return false;
		}
		finally
		{
			if (fileStream_0 != null)
			{
				Class49.FileStreamClose(ref fileStream_0, ref binaryReader_0);
			}
		}
		return true;
	}

	public bool LoadFromFileOld2(string fileName)
	{
		if (!fileName.EndsWith(".mtd"))
		{
			fileName += ".mtd";
		}
		try
		{
			Class49.OpenBinaryReader(fileName, out fileInfo_0, out fileStream_0, out binaryReader_0);
			strMtdFilePath = fileName.ToLower();
			strMtdShowName = fileInfo_0.Name.Remove(fileInfo_0.Name.Length - fileInfo_0.Extension.Length);
			string svaddr = "";
			loadFromFile(binaryReader_0, ref svaddr);
		}
		catch
		{
			if (fileStream_0 != null)
			{
				Class49.FileStreamClose(ref fileStream_0, ref binaryReader_0);
			}
			return false;
		}
		finally
		{
			if (fileStream_0 != null)
			{
				Class49.FileStreamClose(ref fileStream_0, ref binaryReader_0);
			}
		}
		return true;
	}

	public void loadFromFile(BinaryReader binaryReader_1, ref string svaddr)
	{
		svaddr = binaryReader_1.ReadString();
		strMtdShowName = binaryReader_1.ReadString();
		strMtdFilePath = binaryReader_1.ReadString();
		IArrayBase.NewArray(ref dtcAcquisitions, binaryReader_1.ReadInt32());
		for (int i = 0; i < dtcAcquisitions.Count; i++)
		{
			dtcAcquisitions[i].LoadFromFile(binaryReader_1);
		}
		IArrayBase.NewArray(ref sigIntegrations, binaryReader_1.ReadInt32());
		for (int j = 0; j < sigIntegrations.Count; j++)
		{
			if (sigIntegrations[j] == null)
			{
				sigIntegrations[j] = new Integration();
				sigIntegrations[j].Reset();
			}
			sigIntegrations[j].LoadFromFile(binaryReader_1);
		}
		chromInfoR.LoadFromFile(binaryReader_1);
		chromInfo.LoadFromFile(binaryReader_1);
		try
		{
			if (!File.Exists(strMtdFilePath))
			{
			}
		}
		catch
		{
		}
	}
}
