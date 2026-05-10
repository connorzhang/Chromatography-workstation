using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Xml.Serialization;
using IBrainChrom2018.ChromFile;

namespace IBrainChrom2018;

[Serializable]
[StructLayout(LayoutKind.Sequential)]
[XmlInclude(typeof(Compound))]
[XmlInclude(typeof(CaliGnlOpt))]
public class CaliGnl : IBaseFileMgr
{
	[NonSerialized]
	[XmlIgnore]
	public const string fnExt = ".cal";

	[NonSerialized]
	[XmlIgnore]
	public const int MaxLevelsNum = 20;

	private int int_0 = -1;

	[NonSerialized]
	[XmlIgnore]
	private Compound[][] compound_0 = new Compound[0][];

	public CaliGnlOpt caliOption = new CaliGnlOpt();

	public Compound[] cmpds = new Compound[0];

	public int PKPeakIndex = -1;

	public int UpDataPeakIndex = -1;

	[NonSerialized]
	[XmlIgnore]
	private BinaryReader binaryReader_0;

	[NonSerialized]
	[XmlIgnore]
	private BinaryWriter binaryWriter_0;

	[NonSerialized]
	[XmlIgnore]
	private FileInfo fileInfo_0;

	[NonSerialized]
	[XmlIgnore]
	private FileStream fileStream_0;

	public bool IsNull => cmpds.Length == 0;

	public CaliGnl()
	{
		m_strExt = "cal";
		m_strFileTypeName = Lang.PS("组份表文件");
		ClearLinks();
	}

	public Compound add_splLevel(bool checkExists, bool canAddNew, int level, float retainTime, float responseA, float responseH)
	{
		return add_splLevel(checkExists, canAddNew, "", level, retainTime, responseA, responseH, 0f);
	}

	public Compound add_splLevel(bool checkExists, bool canAddNew, string strcmpdname, int level, float retainTime, float responseA, float responseH)
	{
		return add_splLevel(checkExists, canAddNew, strcmpdname, level, retainTime, responseA, responseH, 0f);
	}

	public Compound add_splLevel(bool checkExists, bool canAddNew, string strcmpdname, int level, float retainTime, float responseA, float responseH, float famount)
	{
		bool flag = true;
		Compound compound = null;
		if (checkExists)
		{
			float num = float.MaxValue;
			for (int i = 0; i < cmpds.Length; i++)
			{
				float num2 = Math.Abs(cmpds[i].cmpdInfo.retainTime - retainTime);
				if (num2 < num)
				{
					num = num2;
					compound = cmpds[i];
				}
			}
			if (compound != null)
			{
				float num3 = compound.cmpdInfo.retainTime - compound.cmpdInfo.leftWindow;
				float num4 = compound.cmpdInfo.retainTime + compound.cmpdInfo.rightWindow;
				if (num3 <= retainTime && retainTime <= num4)
				{
					flag = false;
					if (famount > 0f)
					{
						compound.Add_splLevel(level, retainTime, responseA, responseH, caliOption.updateRT, caliOption.recaliMode, famount);
					}
					else
					{
						compound.Add_splLevel(level, retainTime, responseA, responseH, caliOption.updateRT, caliOption.recaliMode);
					}
				}
			}
		}
		if (canAddNew && flag)
		{
			compound = new Compound();
			compound.used = true;
			compound.cmpdInfo.name = strcmpdname;
			compound.cmpdInfo.retainTime = retainTime;
			compound.cmpdInfo.leftWindow = caliOption.leftWindow;
			compound.cmpdInfo.rightWindow = caliOption.rightWindow;
			compound.cmpdInfo.color = Color.Transparent;
			compound.cmpdInfo.respStyle = caliOption.respStyle;
			compound.eFunc.original = (compound.iFunc.original = caliOption.original);
			compound.eFunc.curveFit = (compound.iFunc.curveFit = caliOption.curveFit);
			if (famount > 0f)
			{
				compound.Add_splLevel(level, retainTime, responseA, responseH, caliOption.updateRT, caliOption.recaliMode, famount);
			}
			else
			{
				compound.Add_splLevel(level, retainTime, responseA, responseH, caliOption.updateRT, caliOption.recaliMode);
			}
			Array.Resize(ref cmpds, cmpds.Length + 1);
			cmpds[cmpds.Length - 1] = compound;
			Sortcmds();
		}
		return compound;
	}

	public void Sortcmds()
	{
		CompoundComparer comparer = new CompoundComparer();
		Array.Sort(cmpds, comparer);
	}

	public void SortcmdsByRetainTime()
	{
		if (cmpds != null)
		{
			cmpds = (from x in cmpds.ToList()
				orderby x.cmpdInfo.retainTime
				select x).ToArray();
		}
	}

	public int GetCmpdNameCount(string strName)
	{
		if (cmpds == null)
		{
			return 0;
		}
		return cmpds.Where((Compound x) => x.cmpdInfo.name == strName).ToList().Count;
	}

	public Compound GetCmpd(string strName)
	{
		if (cmpds == null)
		{
			return null;
		}
		List<Compound> list = cmpds.Where((Compound x) => x.cmpdInfo.name == strName).ToList();
		if (list.Count > 0)
		{
			return list[0];
		}
		return null;
	}

	public void AppendLink()
	{
		Array.Resize(ref compound_0, int_0 + 2);
		compound_0[compound_0.Length - 1] = new Compound[cmpds.Length];
		for (int i = 0; i < cmpds.Length; i++)
		{
			compound_0[compound_0.Length - 1][i] = new Compound();
			compound_0[compound_0.Length - 1][i].LoadFromObject(cmpds[i]);
		}
		int_0++;
	}

	public void CalculateFunc(bool appendLink)
	{
		for (int i = 0; i < cmpds.Length && i <= 1000; i++)
		{
			if (cmpds[i] != null)
			{
				cmpds[i].Init();
			}
		}
		for (int j = 0; j < cmpds.Length && j <= 1000; j++)
		{
			if (cmpds[j] != null && cmpds[j].Fill_eFuncPts())
			{
				cmpds[j].eFunc.Calcu_coefs();
			}
		}
		for (int k = 0; k < cmpds.Length && k <= 1000; k++)
		{
			if (cmpds[k] == null || !(cmpds[k].cmpdInfo.istdCmpd != "") || cmpds[k].cmpdInfo.istdCmpd == null)
			{
				continue;
			}
			bool flag = false;
			for (int l = 0; l < cmpds.Length; l++)
			{
				if (l != k && cmpds[l].cmpdInfo.name == cmpds[k].cmpdInfo.istdCmpd)
				{
					if (1 == 0)
					{
						cmpds[k].cmpdInfo.istdCmpd = "";
					}
					break;
				}
			}
		}
		for (int m = 0; m < cmpds.Length && m <= 1000; m++)
		{
			if (cmpds[m] == null || cmpds[m].cmpdInfo.istdCmpd == "" || cmpds[m].cmpdInfo.istdCmpd == null)
			{
				continue;
			}
			for (int n = 0; n < cmpds[m].levels.Length; n++)
			{
				if (cmpds[m].levels[n].eFuncPt.IsValid)
				{
					Level level = cmpds[m].levels[n];
					string istdCmpd = cmpds[m].cmpdInfo.istdCmpd;
					Compound cmpd = GetCmpd(istdCmpd);
					if (cmpd != null)
					{
						if (cmpds[m].cmpdInfo.respStyle == RespStyle.Area)
						{
							level.iFuncPt.responseF = level.iFuncPt.responseF / cmpd.levels[n].responseA;
						}
						else if (cmpds[m].cmpdInfo.respStyle == RespStyle.Height)
						{
							level.iFuncPt.responseF = level.iFuncPt.responseF / cmpd.levels[n].responseH;
						}
						else if (cmpds[m].cmpdInfo.respStyle == RespStyle.AreaSquare)
						{
							level.iFuncPt.responseF = level.iFuncPt.responseF / (float)Math.Sqrt(cmpd.levels[n].responseA);
						}
						else if (cmpds[m].cmpdInfo.respStyle == RespStyle.PeakHeightSquare)
						{
							level.iFuncPt.responseF = level.iFuncPt.responseF / (float)Math.Sqrt(cmpd.levels[n].responseH);
						}
						level.iFuncPt.amountF = level.iFuncPt.amountF / cmpd.levels[n].eFuncPt.amountF;
					}
				}
				else
				{
					cmpds[m].levels[n].iFuncPt.responseF = 0f;
					cmpds[m].levels[n].iFuncPt.amountF = 0f;
				}
			}
		}
		for (int num = 0; num < cmpds.Length && num <= 1000; num++)
		{
			if (cmpds[num] != null && cmpds[num].Fill_iFuncPts())
			{
				cmpds[num].iFunc.Calcu_coefs();
			}
		}
		for (int num2 = 0; num2 < cmpds.Length && num2 <= 1000; num2++)
		{
			if (cmpds[num2] != null)
			{
				cmpds[num2].CalcuDisLg();
			}
		}
		if (appendLink)
		{
			AppendLink();
		}
	}

	public void ClearLevel(int L)
	{
		for (int i = 0; i < cmpds.Length; i++)
		{
			if (cmpds[i] != null)
			{
				cmpds[i].ClearLevel(L);
			}
		}
	}

	public void Clear()
	{
		Array.Resize(ref compound_0, 0);
		int_0 = -1;
		cmpds = new Compound[0];
	}

	public void ClearLevels()
	{
		for (int i = 0; i < cmpds.Length; i++)
		{
			cmpds[i].ClearLevels();
		}
	}

	public void ClearLinks()
	{
		Array.Resize(ref compound_0, 0);
		int_0 = -1;
		AppendLink();
	}

	private bool CompareCompound(Compound[] compound_1, Compound compound_2)
	{
		for (int i = 0; i < compound_1.Length; i++)
		{
			if (compound_1[i] == compound_2)
			{
				return true;
			}
		}
		return false;
	}

	public void DeleteCmpds(Compound[] delCmpds)
	{
		Compound[] array = new Compound[cmpds.Length - delCmpds.Length];
		int num = 0;
		for (int i = 0; i < cmpds.Length; i++)
		{
			if (!CompareCompound(delCmpds, cmpds[i]))
			{
				array[num++] = cmpds[i];
			}
		}
		cmpds = array;
	}

	public int SetCompoundTimeMini(Peak peak11, Peak[] peak_0, int NumIndex)
	{
		float num = float.MaxValue;
		int num2 = -1;
		for (int i = NumIndex + 1; i < cmpds.Length; i++)
		{
			num = float.MaxValue;
			for (int j = 0; j < peak_0.Length; j++)
			{
				float num3 = Math.Abs(peak_0[j].pkRT - cmpds[i].cmpdInfo.retainTime);
				if (num3 < num)
				{
					num = num3;
					num2 = j;
				}
			}
			if (num2 >= 0 && cmpds[i].Contains(peak_0[num2].pkRT))
			{
				peak_0[num2].compound = cmpds[i];
			}
			else if (peak_0[num2].compound == null)
			{
				peak_0[num2].compound = cmpds[i];
			}
			string text = ((peak_0[num2].compound != null) ? peak_0[num2].compound.cmpdInfo.name : "");
			if (!(text != ""))
			{
				continue;
			}
			for (int k = 0; k < peak_0.Length; k++)
			{
				if (peak_0[k].name == text)
				{
					peak_0[k].name = "";
				}
			}
			peak_0[num2].name = text;
			if (peak_0[num2].compound != null && peak_0[num2].compound.levels.Length != 0 && peak_0[num2].compound.levels[0].amount > 0f)
			{
				peak_0[num2].GasAmount = peak_0[num2].compound.levels[0].amount;
			}
		}
		return num2;
	}

	public int SetCompound(Peak peak, Peak[] peak_0, int NumIndex)
	{
		float num = float.MaxValue;
		int num2 = -1;
		for (int i = NumIndex + 1; i < cmpds.Length; i++)
		{
			if (cmpds[i] != null && cmpds[i].Contains(peak.pkRT))
			{
				num2 = i;
				break;
			}
		}
		if (num2 >= 0 && cmpds[num2].Contains(peak.pkRT))
		{
			peak.compound = cmpds[num2];
		}
		else
		{
			peak.compound = null;
			num2 = -1;
		}
		string text = ((peak.compound != null) ? peak.compound.cmpdInfo.name : "");
		if (text != "")
		{
			for (int j = 0; j < peak_0.Length; j++)
			{
				if (peak_0[j].name == text)
				{
					peak_0[j].name = "";
				}
			}
			peak.name = text;
			if (peak.compound != null && peak.compound.levels.Length != 0 && peak.compound.levels[0].amount > 0f)
			{
				peak.GasAmount = peak.compound.levels[0].amount;
			}
		}
		return num2;
	}

	public void SetCompoundNew(Peak[] peak_0)
	{
		float num = float.MaxValue;
		int num2 = -1;
		int num3 = 0;
		for (num3 = 0; num3 < cmpds.Length; num3++)
		{
			for (int i = 0; i < peak_0.Length; i++)
			{
				Peak peak = peak_0[i];
				float num4 = Math.Abs(peak.pkRT - cmpds[num3].cmpdInfo.retainTime);
				if (num4 < num)
				{
					num = num4;
					num2 = i;
				}
			}
			num = float.MaxValue;
			int num5 = -1;
			for (int j = 0; j < peak_0.Length; j++)
			{
				Peak peak2 = peak_0[j];
				float num6 = Math.Abs(peak2.area - cmpds[num3].levels[0].responseA);
				if (num6 < num)
				{
					num = num6;
					num5 = j;
				}
			}
			if (num2 != num5 && Math.Abs(num2 - num5) == 1)
			{
				num2 = num5;
			}
			Peak peak3 = peak_0[num2];
			if (num2 < 0)
			{
				peak3.compound = null;
			}
			else if (peak3.compound != null && Math.Abs(cmpds[num3].cmpdInfo.retainTime - peak3.pkRT) > Math.Abs(peak3.compound.cmpdInfo.retainTime - peak3.pkRT))
			{
				num = float.MaxValue;
				num2 = -1;
			}
			else
			{
				peak3.compound = cmpds[num3];
				string text = ((peak3.compound != null) ? peak3.compound.cmpdInfo.name : "");
				if (text != "")
				{
					for (int k = 0; k < peak_0.Length; k++)
					{
						if (peak_0[k].name == text)
						{
							peak_0[k].name = "";
							peak_0[k].compound = null;
						}
					}
					peak3.name = text;
				}
			}
			num = float.MaxValue;
			num2 = -1;
		}
	}

	public void SetRecaliMode(RecaliMode recaliMode)
	{
		for (int i = 0; i < cmpds.Length; i++)
		{
			cmpds[i].SetRecaliMode(recaliMode);
		}
		CalculateFunc(appendLink: false);
	}

	public CaliGnl Copy()
	{
		CaliGnl caliGnl = new CaliGnl();
		caliGnl.int_0 = int_0;
		caliGnl.PKPeakIndex = PKPeakIndex;
		caliGnl.UpDataPeakIndex = UpDataPeakIndex;
		caliGnl.caliOption = caliOption.Copy();
		if (cmpds != null)
		{
			caliGnl.cmpds = new Compound[cmpds.Length];
			for (int i = 0; i < cmpds.Length; i++)
			{
				caliGnl.cmpds[i] = cmpds[i].Copy();
			}
		}
		return caliGnl;
	}

	public bool Redo()
	{
		if (int_0 >= compound_0.Length - 1)
		{
			return false;
		}
		int_0++;
		Array.Resize(ref cmpds, compound_0[int_0].Length);
		for (int i = 0; i < cmpds.Length; i++)
		{
			if (cmpds[i] == null)
			{
				cmpds[i] = new Compound();
			}
			cmpds[i].LoadFromObject(compound_0[int_0][i]);
		}
		return true;
	}

	public bool Undo()
	{
		if (int_0 <= 0)
		{
			return false;
		}
		int_0--;
		Array.Resize(ref cmpds, compound_0[int_0].Length);
		for (int i = 0; i < cmpds.Length; i++)
		{
			if (cmpds[i] == null)
			{
				cmpds[i] = new Compound();
			}
			cmpds[i].LoadFromObject(compound_0[int_0][i]);
		}
		return true;
	}

	public static CaliGnl LoadFromFile(string fileName)
	{
		CaliGnl caliGnl = new CaliGnl();
		try
		{
			caliGnl.LoadFromFileOldV11(fileName);
		}
		catch
		{
			caliGnl = (CaliGnl)IBaseFileMgr.OpenFile(fileName);
		}
		if (caliGnl != null && caliGnl.cmpds != null)
		{
			caliGnl.cmpds = (from x in caliGnl.cmpds.ToList()
				orderby x.cmpdInfo.retainTime
				select x).ToArray();
			for (int num = 0; num < caliGnl.cmpds.Length; num++)
			{
			}
		}
		return caliGnl;
	}

	public void SaveFile()
	{
		m_strExt = "cal";
		m_strFileTypeName = Lang.PS("组份表文件");
		IBaseFileMgr.SaveFile(this);
	}

	public void SaveFile(string fileName)
	{
		m_strExt = "cal";
		m_strFileTypeName = Lang.PS("组份表文件");
		IBaseFileMgr.SaveFile(fileName, this);
	}

	public void LoadFromFileOldV11(string fileName)
	{
		LoadFromFileOldV11(fileName, out var _);
	}

	public void LoadFromFileOld(string fileName, out string userName)
	{
		if (!fileName.EndsWith(".cal"))
		{
			fileName += ".cal";
		}
		try
		{
			Class49.OpenBinaryReader(fileName, out fileInfo_0, out fileStream_0, out binaryReader_0);
			userName = binaryReader_0.ReadString();
			caliOption.LoadFromFile(binaryReader_0);
			Array.Resize(ref cmpds, binaryReader_0.ReadInt32());
			int i;
			for (i = 0; i < cmpds.Length; i++)
			{
				if (cmpds[i] == null)
				{
					cmpds[i] = new Compound();
				}
				cmpds[i].LoadFromFileOld(binaryReader_0);
			}
			i = 0;
			PKPeakIndex = binaryReader_0.ReadInt32();
			UpDataPeakIndex = binaryReader_0.ReadInt32();
		}
		finally
		{
			Class49.FileStreamClose(ref fileStream_0, ref binaryReader_0);
		}
	}

	public void LoadFromFileOldV11(string fileName, out string userName)
	{
		if (!fileName.EndsWith(".cal"))
		{
			fileName += ".cal";
		}
		try
		{
			Class49.OpenBinaryReader(fileName, out fileInfo_0, out fileStream_0, out binaryReader_0);
			string text = binaryReader_0.ReadString();
			if (text != "Version1.1")
			{
				Class49.FileStreamClose(ref fileStream_0, ref binaryReader_0);
				LoadFromFileOld(fileName, out userName);
				return;
			}
			userName = binaryReader_0.ReadString();
			caliOption.LoadFromFile(binaryReader_0);
			Array.Resize(ref cmpds, binaryReader_0.ReadInt32());
			int i;
			for (i = 0; i < cmpds.Length; i++)
			{
				if (cmpds[i] == null)
				{
					cmpds[i] = new Compound();
				}
				cmpds[i].LoadFromFile(binaryReader_0);
			}
			i = 0;
			for (i = 0; i < cmpds.Length; i++)
			{
				cmpds[i].cmpdInfo.istdCmpd = null;
				for (int j = 0; j < cmpds.Length; j++)
				{
					if (cmpds[i].cmpdInfo.sl_IstdCmpdNo == j && i != j)
					{
						cmpds[i].cmpdInfo.istdCmpd = cmpds[j].cmpdInfo.name;
					}
				}
			}
			PKPeakIndex = binaryReader_0.ReadInt32();
			UpDataPeakIndex = binaryReader_0.ReadInt32();
		}
		finally
		{
			Class49.FileStreamClose(ref fileStream_0, ref binaryReader_0);
		}
	}

	public void LoadFromFileFrombr(BinaryReader binaryReader_1)
	{
		binaryReader_1.ReadString();
		binaryReader_1.ReadString();
		caliOption.LoadFromFile(binaryReader_1);
		Array.Resize(ref cmpds, binaryReader_1.ReadInt32());
		int i;
		for (i = 0; i < cmpds.Length; i++)
		{
			if (cmpds[i] == null)
			{
				cmpds[i] = new Compound();
			}
			cmpds[i].LoadFromFile(binaryReader_1);
		}
		i = 0;
		PKPeakIndex = binaryReader_1.ReadInt32();
		UpDataPeakIndex = binaryReader_1.ReadInt32();
	}

	public void SaveToFileV11(string fileName, string userName)
	{
		if (!fileName.EndsWith(".cal"))
		{
			fileName += ".cal";
		}
		try
		{
			Class49.OpenBinaryWriter(fileName, out fileInfo_0, out fileStream_0, out binaryWriter_0);
			binaryWriter_0.Write("Version1.1");
			binaryWriter_0.Write(userName);
			caliOption.SaveToFile(binaryWriter_0);
			for (int i = 0; i < cmpds.Length; i++)
			{
				cmpds[i].cmpdInfo.sl_IstdCmpdNo = -1;
				for (int j = 0; j < cmpds.Length; j++)
				{
					if (j != i && cmpds[j].cmpdInfo.name == cmpds[i].cmpdInfo.istdCmpd)
					{
						cmpds[i].cmpdInfo.sl_IstdCmpdNo = j;
						break;
					}
				}
			}
			for (int i = 0; i < cmpds.Length; i++)
			{
				cmpds[i].cmpdInfo.isIstd = false;
			}
			for (int i = 0; i < cmpds.Length; i++)
			{
				if (cmpds[i].cmpdInfo.istdCmpd != null)
				{
					cmpds[i].cmpdInfo.isIstd = true;
				}
			}
			binaryWriter_0.Write(cmpds.Length);
			for (int i = 0; i < cmpds.Length; i++)
			{
				cmpds[i].SaveToFile(binaryWriter_0);
			}
			binaryWriter_0.Write(PKPeakIndex);
			binaryWriter_0.Write(UpDataPeakIndex);
		}
		finally
		{
			Class49.FileStreamClose(ref fileStream_0, ref binaryWriter_0);
		}
	}

	public void SaveToFileBybw(BinaryWriter binaryWriter_1)
	{
		binaryWriter_1.Write("Version1.1");
		binaryWriter_1.Write("IBrainChrom");
		caliOption.SaveToFile(binaryWriter_1);
		int num = 0;
		for (num = 0; num < cmpds.Length; num++)
		{
			cmpds[num].cmpdInfo.isIstd = false;
		}
		binaryWriter_1.Write(cmpds.Length);
		for (num = 0; num < cmpds.Length; num++)
		{
			cmpds[num].SaveToFile(binaryWriter_1);
		}
		binaryWriter_1.Write(PKPeakIndex);
		binaryWriter_1.Write(UpDataPeakIndex);
	}
}
