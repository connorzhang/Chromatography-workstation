using System;
using System.IO;
using System.Runtime.InteropServices;

namespace IBrainChrom2018;

[Serializable]
[StructLayout(LayoutKind.Sequential)]
public class PrintPara
{
	public string title;

	public string printTitleTop;

	public string printTitleBotom;

	public PrintPreview pPreView;

	public bool bJtime;

	public bool bfname;

	public bool bRdata;

	public bool bSouredata;

	public bool bpic;

	public int bpicwidth;

	public int bpicheight;

	public bool bPicBound;

	public bool bPicLineB;

	public int bPicFontSize;

	public bool bPTime;

	public bool bIndex;

	public bool bPeakMaxTime;

	public bool bPeakName;

	public bool bPeakPara;

	public bool bPeakAmont;

	public bool bPeakAmontPer;

	public bool bPeakArea;

	public bool bPeakAreaPer;

	public bool bPeakheight;

	public bool bPeakheightPer;

	public bool bPeakHalfheight;

	public bool bPeakV;

	public bool bPeakFx;

	public bool bPeakOtherPara;

	public bool bPeakLV;

	public bool bPeakTBPara;

	public bool bPeakUTBPara;

	public bool bPeakLPara;

	public bool bPeaktailPara;

	private bool useUserZeroTime;

	private double zeroTime;

	private double zeroTimeLeft;

	private double zeroTimeRight;

	public string Title
	{
		get
		{
			return title;
		}
		set
		{
			title = value;
		}
	}

	public string PrintTitleTop
	{
		get
		{
			return printTitleTop;
		}
		set
		{
			printTitleTop = value;
		}
	}

	public string PrintTitleBotom
	{
		get
		{
			return printTitleBotom;
		}
		set
		{
			printTitleBotom = value;
		}
	}

	public PrintPreview PPreView
	{
		get
		{
			return pPreView;
		}
		set
		{
			pPreView = value;
		}
	}

	public bool BJtime
	{
		get
		{
			return bJtime;
		}
		set
		{
			bJtime = value;
		}
	}

	public bool Bfname
	{
		get
		{
			return bfname;
		}
		set
		{
			bfname = value;
		}
	}

	public bool BRdata
	{
		get
		{
			return bRdata;
		}
		set
		{
			bRdata = value;
		}
	}

	public bool BSouredata
	{
		get
		{
			return bSouredata;
		}
		set
		{
			bSouredata = value;
		}
	}

	public bool Bpic
	{
		get
		{
			return bpic;
		}
		set
		{
			bpic = value;
		}
	}

	public int Bpicwidth
	{
		get
		{
			return bpicwidth;
		}
		set
		{
			bpicwidth = value;
		}
	}

	public int Bpicheight
	{
		get
		{
			return bpicheight;
		}
		set
		{
			bpicheight = value;
		}
	}

	public bool BPicBound
	{
		get
		{
			return bPicBound;
		}
		set
		{
			bPicBound = value;
		}
	}

	public bool BPicLineB
	{
		get
		{
			return bPicLineB;
		}
		set
		{
			bPicLineB = value;
		}
	}

	public int BPicFontSize
	{
		get
		{
			return bPicFontSize;
		}
		set
		{
			bPicFontSize = value;
		}
	}

	public bool BPTime
	{
		get
		{
			return bPTime;
		}
		set
		{
			bPTime = value;
		}
	}

	public bool BIndex
	{
		get
		{
			return bIndex;
		}
		set
		{
			bIndex = value;
		}
	}

	public bool BPeakMaxTime
	{
		get
		{
			return bPeakMaxTime;
		}
		set
		{
			bPeakMaxTime = value;
		}
	}

	public bool BPeakName
	{
		get
		{
			return bPeakName;
		}
		set
		{
			bPeakName = value;
		}
	}

	public bool BPeakPara
	{
		get
		{
			return bPeakPara;
		}
		set
		{
			bPeakPara = value;
		}
	}

	public bool BPeakAmont
	{
		get
		{
			return bPeakAmont;
		}
		set
		{
			bPeakAmont = value;
		}
	}

	public bool BPeakAmontPer
	{
		get
		{
			return bPeakAmontPer;
		}
		set
		{
			bPeakAmontPer = value;
		}
	}

	public bool BPeakArea
	{
		get
		{
			return bPeakArea;
		}
		set
		{
			bPeakArea = value;
		}
	}

	public bool BPeakAreaPer
	{
		get
		{
			return bPeakAreaPer;
		}
		set
		{
			bPeakAreaPer = value;
		}
	}

	public bool BPeakheight
	{
		get
		{
			return bPeakheight;
		}
		set
		{
			bPeakheight = value;
		}
	}

	public bool BPeakheightPer
	{
		get
		{
			return bPeakheightPer;
		}
		set
		{
			bPeakheightPer = value;
		}
	}

	public bool BPeakHalfheight
	{
		get
		{
			return bPeakHalfheight;
		}
		set
		{
			bPeakHalfheight = value;
		}
	}

	public bool BPeakV
	{
		get
		{
			return bPeakV;
		}
		set
		{
			bPeakV = value;
		}
	}

	public bool BPeakFx
	{
		get
		{
			return bPeakFx;
		}
		set
		{
			bPeakFx = value;
		}
	}

	public bool BPeakOtherPara
	{
		get
		{
			return bPeakOtherPara;
		}
		set
		{
			bPeakOtherPara = value;
		}
	}

	public bool BPeakLV
	{
		get
		{
			return bPeakLV;
		}
		set
		{
			bPeakLV = value;
		}
	}

	public bool BPeakTBPara
	{
		get
		{
			return bPeakTBPara;
		}
		set
		{
			bPeakTBPara = value;
		}
	}

	public bool BPeakUTBPara
	{
		get
		{
			return bPeakUTBPara;
		}
		set
		{
			bPeakUTBPara = value;
		}
	}

	public bool BPeakLPara
	{
		get
		{
			return bPeakLPara;
		}
		set
		{
			bPeakLPara = value;
		}
	}

	public bool BPeaktailPara
	{
		get
		{
			return bPeaktailPara;
		}
		set
		{
			bPeaktailPara = value;
		}
	}

	public bool UseUserZeroTime
	{
		get
		{
			return useUserZeroTime;
		}
		set
		{
			useUserZeroTime = value;
		}
	}

	public double ZeroTime
	{
		get
		{
			return zeroTime;
		}
		set
		{
			zeroTime = value;
		}
	}

	public double ZeroTimeLeft
	{
		get
		{
			return zeroTimeLeft;
		}
		set
		{
			zeroTimeLeft = value;
		}
	}

	public double ZeroTimeRight
	{
		get
		{
			return zeroTimeRight;
		}
		set
		{
			zeroTimeRight = value;
		}
	}

	public PrintPara()
	{
		Init();
	}

	public void Init()
	{
		Title = Lang.PS("XXXX分析报告", "XXXXRePort");
		PrintTitleTop = Lang.PS("质检（E）字第（ \u3000）号\r\n送样单位：         \u3000\u3000        仪器型号:\r\n取样日期：   年  月  日       收样日期：     年  月  日\r\n样品批号：                       样品名称：固液\r\n样品罐号：                       仪器控制参数文件：", "Sample units：         \u3000\u3000        Instrument Type:\r\nDate:       datereceived：     \r\nSample number：                       Sample Names：  \r\nsample tank No：                       Instrument parameter：");
		PrintTitleBotom = Lang.PS("备注：按                 检验，浓度含量单位：g/l\r\n检测结果:                检验部门：\r\n检验员：                 审核员：", "Remark:");
		PPreView = PrintPreview.程序自带;
		BPTime = true;
		BJtime = true;
		Bfname = true;
		BRdata = true;
		BSouredata = false;
		Bpic = true;
		Bpicwidth = 1326;
		Bpicheight = 528;
		BPicBound = true;
		BPicLineB = false;
		BPicFontSize = 8;
		BIndex = true;
		BPeakMaxTime = true;
		BPeakName = true;
		BPeakPara = false;
		BPeakAmont = true;
		BPeakAmontPer = true;
		BPeakArea = true;
		BPeakAreaPer = true;
		BPeakheight = false;
		BPeakheightPer = false;
		BPeakHalfheight = false;
		BPeakV = false;
		BPeakFx = false;
		BPeakOtherPara = false;
		BPeakLV = false;
		BPeakTBPara = false;
		BPeakUTBPara = false;
		BPeakLPara = false;
		BPeaktailPara = false;
		UseUserZeroTime = false;
		ZeroTime = 0.0;
		ZeroTimeLeft = 0.3;
		ZeroTimeRight = 0.3;
	}

	public PrintPara Copy()
	{
		PrintPara printPara = new PrintPara();
		printPara.title = title;
		printPara.printTitleTop = printTitleTop;
		printPara.printTitleBotom = printTitleBotom;
		printPara.pPreView = pPreView;
		printPara.bJtime = bJtime;
		printPara.bfname = bfname;
		printPara.bRdata = bRdata;
		printPara.bSouredata = bSouredata;
		printPara.bpic = bpic;
		printPara.bpicwidth = bpicwidth;
		printPara.bpicheight = bpicheight;
		printPara.bPicBound = bPicBound;
		printPara.bPicFontSize = bPicFontSize;
		printPara.bPTime = bPTime;
		printPara.bIndex = bIndex;
		printPara.bPeakMaxTime = bPeakMaxTime;
		printPara.bPeakName = bPeakName;
		printPara.bPeakPara = bPeakPara;
		printPara.bPeakAmont = bPeakAmont;
		printPara.bPeakAmontPer = bPeakAmontPer;
		printPara.bPeakArea = bPeakArea;
		printPara.bPeakAreaPer = bPeakAreaPer;
		printPara.bPeakheight = bPeakheight;
		printPara.bPeakheightPer = bPeakheightPer;
		printPara.bPeakHalfheight = bPeakHalfheight;
		printPara.bPeakV = bPeakV;
		printPara.bPeakFx = bPeakFx;
		printPara.bPeakOtherPara = bPeakOtherPara;
		printPara.bPeakLV = bPeakLV;
		printPara.bPeakTBPara = bPeakTBPara;
		printPara.bPeakUTBPara = bPeakUTBPara;
		printPara.bPeakLPara = bPeakLPara;
		printPara.bPeaktailPara = bPeaktailPara;
		printPara.useUserZeroTime = useUserZeroTime;
		printPara.zeroTime = zeroTime;
		printPara.zeroTimeLeft = zeroTimeLeft;
		printPara.zeroTimeRight = zeroTimeRight;
		return printPara;
	}

	public void WriteToFile(BinaryWriter binaryWriter_0)
	{
		if (Title == null)
		{
			Title = "";
		}
		if (PrintTitleTop == null)
		{
			PrintTitleTop = "";
		}
		if (PrintTitleBotom == null)
		{
			PrintTitleBotom = "";
		}
		binaryWriter_0.Write(Title);
		binaryWriter_0.Write(PrintTitleTop);
		binaryWriter_0.Write(PrintTitleBotom);
		binaryWriter_0.Write((int)PPreView);
		binaryWriter_0.Write(BPTime);
		binaryWriter_0.Write(BJtime);
		binaryWriter_0.Write(Bfname);
		binaryWriter_0.Write(BRdata);
		binaryWriter_0.Write(BSouredata);
		binaryWriter_0.Write(Bpic);
		binaryWriter_0.Write(Bpicwidth);
		binaryWriter_0.Write(Bpicheight);
		binaryWriter_0.Write(BPicBound);
		binaryWriter_0.Write(BPicLineB);
		binaryWriter_0.Write(BPicFontSize);
		binaryWriter_0.Write(BIndex);
		binaryWriter_0.Write(BPeakMaxTime);
		binaryWriter_0.Write(BPeakName);
		binaryWriter_0.Write(BPeakPara);
		binaryWriter_0.Write(BPeakAmont);
		binaryWriter_0.Write(BPeakAmontPer);
		binaryWriter_0.Write(BPeakArea);
		binaryWriter_0.Write(BPeakAreaPer);
		binaryWriter_0.Write(BPeakheight);
		binaryWriter_0.Write(BPeakheightPer);
		binaryWriter_0.Write(BPeakHalfheight);
		binaryWriter_0.Write(BPeakV);
		binaryWriter_0.Write(BPeakFx);
		binaryWriter_0.Write(BPeakOtherPara);
		binaryWriter_0.Write(BPeakLV);
		binaryWriter_0.Write(BPeakTBPara);
		binaryWriter_0.Write(BPeakUTBPara);
		binaryWriter_0.Write(BPeakLPara);
		binaryWriter_0.Write(BPeaktailPara);
		binaryWriter_0.Write(UseUserZeroTime);
		binaryWriter_0.Write(ZeroTime);
		binaryWriter_0.Write(ZeroTimeLeft);
		binaryWriter_0.Write(ZeroTimeRight);
	}

	public void LoadFromBr(BinaryReader binaryReader_0)
	{
		try
		{
			Title = binaryReader_0.ReadString();
			PrintTitleTop = binaryReader_0.ReadString();
			PrintTitleBotom = binaryReader_0.ReadString();
			PPreView = (PrintPreview)binaryReader_0.ReadInt32();
			BPTime = binaryReader_0.ReadBoolean();
			BJtime = binaryReader_0.ReadBoolean();
			Bfname = binaryReader_0.ReadBoolean();
			BRdata = binaryReader_0.ReadBoolean();
			BSouredata = binaryReader_0.ReadBoolean();
			Bpic = binaryReader_0.ReadBoolean();
			Bpicwidth = binaryReader_0.ReadInt32();
			Bpicheight = binaryReader_0.ReadInt32();
			BPicBound = binaryReader_0.ReadBoolean();
			BPicLineB = binaryReader_0.ReadBoolean();
			BPicFontSize = binaryReader_0.ReadInt32();
			BIndex = binaryReader_0.ReadBoolean();
			BPeakMaxTime = binaryReader_0.ReadBoolean();
			BPeakName = binaryReader_0.ReadBoolean();
			BPeakPara = binaryReader_0.ReadBoolean();
			BPeakAmont = binaryReader_0.ReadBoolean();
			BPeakAmontPer = binaryReader_0.ReadBoolean();
			BPeakArea = binaryReader_0.ReadBoolean();
			BPeakAreaPer = binaryReader_0.ReadBoolean();
			BPeakheight = binaryReader_0.ReadBoolean();
			BPeakheightPer = binaryReader_0.ReadBoolean();
			BPeakHalfheight = binaryReader_0.ReadBoolean();
			BPeakV = binaryReader_0.ReadBoolean();
			BPeakFx = binaryReader_0.ReadBoolean();
			BPeakOtherPara = binaryReader_0.ReadBoolean();
			BPeakLV = binaryReader_0.ReadBoolean();
			BPeakTBPara = binaryReader_0.ReadBoolean();
			BPeakUTBPara = binaryReader_0.ReadBoolean();
			BPeakLPara = binaryReader_0.ReadBoolean();
			BPeaktailPara = binaryReader_0.ReadBoolean();
			try
			{
				UseUserZeroTime = binaryReader_0.ReadBoolean();
				ZeroTime = binaryReader_0.ReadDouble();
				ZeroTimeLeft = binaryReader_0.ReadDouble();
				ZeroTimeRight = binaryReader_0.ReadDouble();
			}
			catch
			{
			}
		}
		catch
		{
			Title = "";
			PrintTitleTop = "";
			PrintTitleBotom = "";
			PPreView = PrintPreview.写字板;
		}
	}
}
