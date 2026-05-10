using System;
using System.IO;
using System.Runtime.InteropServices;

namespace IBrainChrom2018;

[Serializable]
[StructLayout(LayoutKind.Sequential)]
public class Injection
{
	public float amount;

	public string analyst = "";

	public bool cali_stand;

	public int counter = 1;

	public float dilution = 1f;

	public DateTime dtAcquire;

	public int endVial = 1;

	public string fileName = "";

	public string fileNameFMT = "%N_%3n %T";

	public float gpc_alpha = 0.7f;

	public float gpc_k = 14.1f;

	public float inj_volume;

	public int injNo;

	public InjStatusMeasure injStatus;

	public InjStatusCheck injStatusCheck = InjStatusCheck.NotCheck;

	public float ISTD_amount;

	public string methodFileName = "";

	public bool openCaliWin;

	public bool openChromWin;

	public bool openPrintWin;

	public string reportStyleFileName = "";

	public bool bool_0 = true;

	public string sample = "%R_%n";

	public string sampleID = Lang.PS("中国", "Zju") + "%R_%3n";

	public int startVial = 1;

	public TimeSpan tsAcquire;

	public int vialInjs = 1;

	public int vialNo;

	public bool IsValid => vialInjs > 0 && startVial > 0 && endVial > 0 && startVial <= endVial;

	public bool Equals(Injection injection_0)
	{
		if (!(sampleID != injection_0.sampleID) && !(sample != injection_0.sample) && amount == injection_0.amount && ISTD_amount == injection_0.ISTD_amount && dilution == injection_0.dilution && inj_volume == injection_0.inj_volume && cali_stand == injection_0.cali_stand && gpc_k == injection_0.gpc_k && gpc_alpha == injection_0.gpc_alpha && !(fileName != injection_0.fileName) && !(fileNameFMT != injection_0.fileNameFMT) && openChromWin == injection_0.openChromWin && openCaliWin == injection_0.openCaliWin && openPrintWin == injection_0.openPrintWin)
		{
			return true;
		}
		return false;
	}

	public void LoadFromObject(Injection injection_0)
	{
		LoadFromObject(injection_0, Chrom: true, Cali: true, Print: true);
	}

	public void LoadFromObject(Injection injection_0, bool Chrom, bool Cali, bool Print)
	{
		counter = injection_0.counter;
		sampleID = injection_0.sampleID;
		sample = injection_0.sample;
		amount = injection_0.amount;
		ISTD_amount = injection_0.ISTD_amount;
		dilution = injection_0.dilution;
		inj_volume = injection_0.inj_volume;
		dtAcquire = injection_0.dtAcquire;
		tsAcquire = injection_0.tsAcquire;
		analyst = injection_0.analyst;
		cali_stand = injection_0.cali_stand;
		gpc_k = injection_0.gpc_k;
		gpc_alpha = injection_0.gpc_alpha;
		fileNameFMT = injection_0.fileNameFMT;
		fileName = injection_0.fileName;
		injStatusCheck = injection_0.injStatusCheck;
		bool_0 = injection_0.bool_0;
		startVial = injection_0.startVial;
		endVial = injection_0.endVial;
		vialInjs = injection_0.vialInjs;
		methodFileName = injection_0.methodFileName;
		reportStyleFileName = injection_0.reportStyleFileName;
		if (Chrom)
		{
			openChromWin = injection_0.openChromWin;
		}
		if (Cali)
		{
			openCaliWin = injection_0.openCaliWin;
		}
		if (Print)
		{
			openPrintWin = injection_0.openPrintWin;
		}
	}

	public void Reset()
	{
		sampleID = (sample = "");
		amount = (ISTD_amount = 0f);
		dilution = 1f;
		inj_volume = 0f;
		cali_stand = false;
		gpc_k = (gpc_alpha = 0f);
		fileName = (fileNameFMT = "");
		vialInjs = 1;
		endVial = 1;
		startVial = 1;
		methodFileName = (reportStyleFileName = "");
		openPrintWin = false;
		openCaliWin = false;
		openChromWin = false;
	}

	public virtual void LoadFromFile(BinaryReader binaryReader_0)
	{
		byte b = binaryReader_0.ReadByte();
		bool flag = true;
		sampleID = binaryReader_0.ReadString();
		sample = binaryReader_0.ReadString();
		amount = binaryReader_0.ReadSingle();
		ISTD_amount = binaryReader_0.ReadSingle();
		dilution = binaryReader_0.ReadSingle();
		inj_volume = binaryReader_0.ReadSingle();
		dtAcquire = DateTime.FromBinary(binaryReader_0.ReadInt64());
		tsAcquire = TimeSpan.FromMinutes(binaryReader_0.ReadDouble());
		analyst = binaryReader_0.ReadString();
		cali_stand = binaryReader_0.ReadBoolean();
		gpc_k = binaryReader_0.ReadSingle();
		gpc_alpha = binaryReader_0.ReadSingle();
		fileNameFMT = binaryReader_0.ReadString();
		injStatus = (InjStatusMeasure)binaryReader_0.ReadByte();
		injStatusCheck = (InjStatusCheck)binaryReader_0.ReadByte();
		bool_0 = binaryReader_0.ReadBoolean();
		startVial = binaryReader_0.ReadInt32();
		endVial = binaryReader_0.ReadInt32();
		vialInjs = binaryReader_0.ReadInt32();
		injNo = 0;
		methodFileName = binaryReader_0.ReadString();
		reportStyleFileName = binaryReader_0.ReadString();
		openChromWin = binaryReader_0.ReadBoolean();
		openCaliWin = binaryReader_0.ReadBoolean();
		openPrintWin = binaryReader_0.ReadBoolean();
	}

	public virtual void SaveToFile(BinaryWriter binaryWriter_0)
	{
		binaryWriter_0.Write(Class49.smethod_36());
		binaryWriter_0.Write(sampleID);
		binaryWriter_0.Write(sample);
		binaryWriter_0.Write(amount);
		binaryWriter_0.Write(ISTD_amount);
		binaryWriter_0.Write(dilution);
		binaryWriter_0.Write(inj_volume);
		binaryWriter_0.Write(dtAcquire.ToBinary());
		binaryWriter_0.Write(tsAcquire.TotalMinutes);
		binaryWriter_0.Write(analyst);
		binaryWriter_0.Write(cali_stand);
		binaryWriter_0.Write(gpc_k);
		binaryWriter_0.Write(gpc_alpha);
		binaryWriter_0.Write(fileNameFMT);
		binaryWriter_0.Write((byte)injStatus);
		binaryWriter_0.Write((byte)injStatusCheck);
		binaryWriter_0.Write(bool_0);
		binaryWriter_0.Write(startVial);
		binaryWriter_0.Write(endVial);
		binaryWriter_0.Write(vialInjs);
		binaryWriter_0.Write(methodFileName);
		binaryWriter_0.Write(reportStyleFileName);
		binaryWriter_0.Write(openChromWin);
		binaryWriter_0.Write(openCaliWin);
		binaryWriter_0.Write(openPrintWin);
	}
}
