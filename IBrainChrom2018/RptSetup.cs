using System;
using System.Drawing;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace IBrainChrom2018;

public class RptSetup
{
	public const string fnExt = ".sty";

	private BinaryFormatter binaryFormatter_0 = new BinaryFormatter();

	private BinaryReader binaryReader_0;

	private BinaryWriter binaryWriter_0;

	public ChooseStyle cgChooseStyle;

	public GraphDisStyle cgGraphDisStyle;

	public GraphShowStyle cgGraphShowStyle;

	public bool cgOnNewPage;

	public bool cgUse;

	public ChooseStyle ciChooseStyle;

	public bool ciciAdvance;

	public bool ciciCalculation;

	public bool ciciGpcRanges;

	public bool ciciIntegration;

	public bool ciciMeasurement;

	public bool ciciPDA;

	public bool ciOnNewPage;

	public bool ciUse;

	public bool clcdGnlGraph;

	public bool clcdGnlLevels;

	public bool clCmpds;

	public bool clOnNewPage;

	public bool clOptions;

	public bool clUse;

	public ChooseStyle crChooseStyle;

	public bool crGpcRanges;

	public bool crGpcSlices;

	public bool crOnNewPage;

	public bool crPerformance;

	public bool crResult;

	public bool crRltCombine;

	public bool crSST;

	public bool crSummary;

	public bool crUse;

	private FileInfo fileInfo_0;

	private FileStream fileStream_0;

	public bool lhBorder;

	public bool lhGrayBkgnd;

	public string lhImgLeftName;

	public ImageLctStyle lhImgLeftStyle;

	public int lhImgLeftWidth;

	public string lhImgRightName;

	public ImageLctStyle lhImgRightStyle;

	public int lhImgRightWidth;

	public bool lhJstFstPage;

	public string[] lhLines;

	public StringAlignment[] lhLinesAlign;

	public Color[] lhLinesClr;

	public Font[] lhLinesFt;

	public int lhLinesNum;

	public bool lhUse;

	public bool lhUseImgLeft;

	public bool lhUseImgRight;

	public bool mtdAcquisition;

	public bool mtdAS;

	public bool mtdciAdvance;

	public bool mtdciCalculation;

	public bool mtdciGpcRanges;

	public bool mtdciMeasurement;

	public bool mtdciPDA;

	public bool mtdGcProgTemp;

	public bool mtdGcPTGraph;

	public bool mtdIntegration;

	public bool mtdLcGradient;

	public bool mtdLcGrdtGraph;

	public bool mtdLcGrdtItems;

	public bool mtdOnNewPage;

	public bool mtdUse;

	public bool psColors;

	public bool psDrawGraphsBkgnd;

	public Color psItemColor;

	public Font psItemFont;

	public int psmgBottom;

	public int psmgInterval;

	public int psmgLeft;

	public int psmgRight;

	public int psmgTop;

	public bool psSign;

	public Color psValueColor;

	public Font psValueFont;

	public bool rhDateTime;

	public NamePathStyle rhNamePathStyle;

	public bool rhOnNewPage;

	public bool rhSystemInfo;

	public bool rhUse;

	public bool sqInjList;

	public bool sqOnNewPage;

	public bool sqOptions;

	public bool sqUse;

	public int LinesNum
	{
		get
		{
			return lhLinesNum;
		}
		set
		{
			Array.Resize(ref lhLines, value);
			Array.Resize(ref lhLinesAlign, value);
			Array.Resize(ref lhLinesFt, value);
			Array.Resize(ref lhLinesClr, value);
			for (int i = lhLinesNum; i < value; i++)
			{
				lhLines[i] = "";
				lhLinesAlign[i] = StringAlignment.Center;
				lhLinesFt[i] = new Font(FontFamily.GenericSansSerif, 13f, FontStyle.Italic);
				lhLinesClr[i] = Color.Black;
			}
			lhLinesNum = value;
		}
	}

	public RptSetup()
	{
		Init();
	}

	public void Init()
	{
		psColors = true;
		psDrawGraphsBkgnd = true;
		psItemFont = new Font(FontFamily.GenericSansSerif, 8f);
		psValueFont = new Font(FontFamily.GenericSansSerif, 8f);
		psItemColor = (psValueColor = Color.Black);
		psmgRight = 15;
		psmgLeft = 15;
		psmgBottom = 40;
		psmgTop = 40;
		psmgInterval = 0;
		psSign = true;
		lhUse = true;
		lhJstFstPage = false;
		lhBorder = true;
		lhGrayBkgnd = false;
		LinesNum = 3;
		lhLines[0] = "多糖实验";
		lhLines[1] = "赛尔泰";
		lhLines[2] = "www.54pc.com";
		lhLinesClr[0] = Color.Red;
		lhLinesAlign[2] = StringAlignment.Near;
		lhUseImgLeft = true;
		lhImgLeftName = "";
		lhImgLeftStyle = ImageLctStyle.Header;
		lhImgLeftWidth = 30;
		lhUseImgRight = false;
		lhImgRightName = "";
		lhImgRightStyle = ImageLctStyle.Header;
		lhImgRightWidth = 30;
		rhUse = false;
		rhOnNewPage = false;
		rhNamePathStyle = NamePathStyle.OthersOnly;
		rhSystemInfo = true;
		rhDateTime = true;
		mtdUse = false;
		mtdOnNewPage = false;
		mtdAS = false;
		mtdGcProgTemp = true;
		mtdGcPTGraph = true;
		mtdLcGradient = true;
		mtdLcGrdtGraph = true;
		mtdLcGrdtItems = true;
		mtdAcquisition = true;
		mtdIntegration = true;
		mtdciMeasurement = true;
		mtdciCalculation = true;
		mtdciAdvance = true;
		mtdciPDA = false;
		mtdciGpcRanges = false;
		ciUse = false;
		ciOnNewPage = true;
		ciChooseStyle = ChooseStyle.All;
		ciciMeasurement = true;
		ciciIntegration = true;
		ciciCalculation = true;
		ciciAdvance = true;
		ciciPDA = false;
		ciciGpcRanges = false;
		cgUse = false;
		cgOnNewPage = false;
		cgChooseStyle = ChooseStyle.All;
		cgGraphShowStyle = GraphShowStyle.Combine;
		cgGraphDisStyle = GraphDisStyle.Whole;
		crUse = false;
		crOnNewPage = false;
		crChooseStyle = ChooseStyle.All;
		crSummary = true;
		crSST = true;
		crResult = false;
		crRltCombine = false;
		crPerformance = false;
		crGpcSlices = false;
		crGpcRanges = false;
		clUse = false;
		clOnNewPage = false;
		clOptions = true;
		clCmpds = true;
		clcdGnlLevels = true;
		clcdGnlGraph = true;
		sqUse = false;
		sqOnNewPage = false;
		sqOptions = true;
		sqInjList = true;
	}

	public void LoadFromFile(BinaryReader binaryReader_1)
	{
		psColors = binaryReader_1.ReadBoolean();
		psDrawGraphsBkgnd = binaryReader_1.ReadBoolean();
		psItemFont = (Font)binaryFormatter_0.Deserialize(binaryReader_1.BaseStream);
		psItemColor = Color.FromArgb(binaryReader_1.ReadInt32());
		psValueFont = (Font)binaryFormatter_0.Deserialize(binaryReader_1.BaseStream);
		psValueColor = Color.FromArgb(binaryReader_1.ReadInt32());
		psmgLeft = binaryReader_1.ReadInt32();
		psmgRight = binaryReader_1.ReadInt32();
		psmgTop = binaryReader_1.ReadInt32();
		psmgBottom = binaryReader_1.ReadInt32();
		psmgInterval = binaryReader_1.ReadInt32();
		psSign = binaryReader_1.ReadBoolean();
		lhUse = binaryReader_1.ReadBoolean();
		lhJstFstPage = binaryReader_1.ReadBoolean();
		lhBorder = binaryReader_1.ReadBoolean();
		lhGrayBkgnd = binaryReader_1.ReadBoolean();
		lhLinesNum = binaryReader_1.ReadInt32();
		Array.Resize(ref lhLines, lhLinesNum);
		Array.Resize(ref lhLinesAlign, lhLinesNum);
		Array.Resize(ref lhLinesFt, lhLinesNum);
		Array.Resize(ref lhLinesClr, lhLinesNum);
		for (int i = 0; i < lhLinesNum; i++)
		{
			lhLines[i] = binaryReader_1.ReadString();
			lhLinesAlign[i] = (StringAlignment)binaryReader_1.ReadByte();
			lhLinesFt[i] = (Font)binaryFormatter_0.Deserialize(binaryReader_1.BaseStream);
			lhLinesClr[i] = Color.FromArgb(binaryReader_1.ReadInt32());
		}
		lhUseImgLeft = binaryReader_1.ReadBoolean();
		lhImgLeftName = binaryReader_1.ReadString();
		lhImgLeftStyle = (ImageLctStyle)binaryReader_1.ReadByte();
		lhImgLeftWidth = binaryReader_1.ReadInt32();
		lhUseImgRight = binaryReader_1.ReadBoolean();
		lhImgRightName = binaryReader_1.ReadString();
		lhImgRightStyle = (ImageLctStyle)binaryReader_1.ReadByte();
		lhImgRightWidth = binaryReader_1.ReadInt32();
		rhUse = binaryReader_1.ReadBoolean();
		rhOnNewPage = binaryReader_1.ReadBoolean();
		rhNamePathStyle = (NamePathStyle)binaryReader_1.ReadByte();
		rhSystemInfo = binaryReader_1.ReadBoolean();
		rhDateTime = binaryReader_1.ReadBoolean();
		mtdUse = binaryReader_1.ReadBoolean();
		mtdOnNewPage = binaryReader_1.ReadBoolean();
		mtdAS = binaryReader_1.ReadBoolean();
		mtdGcProgTemp = binaryReader_1.ReadBoolean();
		mtdGcPTGraph = binaryReader_1.ReadBoolean();
		mtdLcGradient = binaryReader_1.ReadBoolean();
		mtdLcGrdtGraph = binaryReader_1.ReadBoolean();
		mtdLcGrdtItems = binaryReader_1.ReadBoolean();
		mtdAcquisition = binaryReader_1.ReadBoolean();
		mtdIntegration = binaryReader_1.ReadBoolean();
		mtdciMeasurement = binaryReader_1.ReadBoolean();
		mtdciCalculation = binaryReader_1.ReadBoolean();
		mtdciAdvance = binaryReader_1.ReadBoolean();
		mtdciPDA = binaryReader_1.ReadBoolean();
		mtdciGpcRanges = binaryReader_1.ReadBoolean();
		ciUse = binaryReader_1.ReadBoolean();
		ciOnNewPage = binaryReader_1.ReadBoolean();
		ciChooseStyle = (ChooseStyle)binaryReader_1.ReadByte();
		ciciMeasurement = binaryReader_1.ReadBoolean();
		ciciIntegration = binaryReader_1.ReadBoolean();
		ciciCalculation = binaryReader_1.ReadBoolean();
		ciciAdvance = binaryReader_1.ReadBoolean();
		ciciPDA = binaryReader_1.ReadBoolean();
		ciciGpcRanges = binaryReader_1.ReadBoolean();
		cgUse = binaryReader_1.ReadBoolean();
		cgOnNewPage = binaryReader_1.ReadBoolean();
		cgChooseStyle = (ChooseStyle)binaryReader_1.ReadByte();
		cgGraphShowStyle = (GraphShowStyle)binaryReader_1.ReadByte();
		cgGraphDisStyle = (GraphDisStyle)binaryReader_1.ReadByte();
		crUse = binaryReader_1.ReadBoolean();
		crOnNewPage = binaryReader_1.ReadBoolean();
		crChooseStyle = (ChooseStyle)binaryReader_1.ReadByte();
		crSummary = binaryReader_1.ReadBoolean();
		crSST = binaryReader_1.ReadBoolean();
		crResult = binaryReader_1.ReadBoolean();
		crRltCombine = binaryReader_1.ReadBoolean();
		crPerformance = binaryReader_1.ReadBoolean();
		crGpcSlices = binaryReader_1.ReadBoolean();
		crGpcRanges = binaryReader_1.ReadBoolean();
		clUse = binaryReader_1.ReadBoolean();
		clOnNewPage = binaryReader_1.ReadBoolean();
		clOptions = binaryReader_1.ReadBoolean();
		clCmpds = binaryReader_1.ReadBoolean();
		clcdGnlLevels = binaryReader_1.ReadBoolean();
		clcdGnlGraph = binaryReader_1.ReadBoolean();
		sqUse = binaryReader_1.ReadBoolean();
		sqOnNewPage = binaryReader_1.ReadBoolean();
		sqOptions = binaryReader_1.ReadBoolean();
		sqInjList = binaryReader_1.ReadBoolean();
	}

	public void LoadFromFile(string fileName)
	{
		if (!fileName.EndsWith(".sty"))
		{
			fileName += ".sty";
		}
		try
		{
			Class49.OpenBinaryReader(fileName, out fileInfo_0, out fileStream_0, out binaryReader_0);
			LoadFromFile(binaryReader_0);
		}
		finally
		{
			Class49.FileStreamClose(ref fileStream_0, ref binaryReader_0);
		}
	}

	public void SaveToFile(BinaryWriter binaryWriter_1)
	{
		binaryWriter_1.Write(psColors);
		binaryWriter_1.Write(psDrawGraphsBkgnd);
		binaryFormatter_0.Serialize(binaryWriter_1.BaseStream, psItemFont);
		binaryWriter_1.Write(psItemColor.ToArgb());
		binaryFormatter_0.Serialize(binaryWriter_1.BaseStream, psValueFont);
		binaryWriter_1.Write(psValueColor.ToArgb());
		binaryWriter_1.Write(psmgLeft);
		binaryWriter_1.Write(psmgRight);
		binaryWriter_1.Write(psmgTop);
		binaryWriter_1.Write(psmgBottom);
		binaryWriter_1.Write(psmgInterval);
		binaryWriter_1.Write(psSign);
		binaryWriter_1.Write(lhUse);
		binaryWriter_1.Write(lhJstFstPage);
		binaryWriter_1.Write(lhBorder);
		binaryWriter_1.Write(lhGrayBkgnd);
		binaryWriter_1.Write(lhLinesNum);
		for (int i = 0; i < lhLinesNum; i++)
		{
			binaryWriter_1.Write(lhLines[i]);
			binaryWriter_1.Write((byte)lhLinesAlign[i]);
			binaryFormatter_0.Serialize(binaryWriter_1.BaseStream, lhLinesFt[i]);
			binaryWriter_1.Write(lhLinesClr[i].ToArgb());
		}
		binaryWriter_1.Write(lhUseImgLeft);
		binaryWriter_1.Write(lhImgLeftName);
		binaryWriter_1.Write((byte)lhImgLeftStyle);
		binaryWriter_1.Write(lhImgLeftWidth);
		binaryWriter_1.Write(lhUseImgRight);
		binaryWriter_1.Write(lhImgRightName);
		binaryWriter_1.Write((byte)lhImgRightStyle);
		binaryWriter_1.Write(lhImgRightWidth);
		binaryWriter_1.Write(rhUse);
		binaryWriter_1.Write(rhOnNewPage);
		binaryWriter_1.Write((byte)rhNamePathStyle);
		binaryWriter_1.Write(rhSystemInfo);
		binaryWriter_1.Write(rhDateTime);
		binaryWriter_1.Write(mtdUse);
		binaryWriter_1.Write(mtdOnNewPage);
		binaryWriter_1.Write(mtdAS);
		binaryWriter_1.Write(mtdGcProgTemp);
		binaryWriter_1.Write(mtdGcPTGraph);
		binaryWriter_1.Write(mtdLcGradient);
		binaryWriter_1.Write(mtdLcGrdtGraph);
		binaryWriter_1.Write(mtdLcGrdtItems);
		binaryWriter_1.Write(mtdAcquisition);
		binaryWriter_1.Write(mtdIntegration);
		binaryWriter_1.Write(mtdciMeasurement);
		binaryWriter_1.Write(mtdciCalculation);
		binaryWriter_1.Write(mtdciAdvance);
		binaryWriter_1.Write(mtdciPDA);
		binaryWriter_1.Write(mtdciGpcRanges);
		binaryWriter_1.Write(ciUse);
		binaryWriter_1.Write(ciOnNewPage);
		binaryWriter_1.Write((byte)ciChooseStyle);
		binaryWriter_1.Write(ciciMeasurement);
		binaryWriter_1.Write(ciciIntegration);
		binaryWriter_1.Write(ciciCalculation);
		binaryWriter_1.Write(ciciAdvance);
		binaryWriter_1.Write(ciciPDA);
		binaryWriter_1.Write(ciciGpcRanges);
		binaryWriter_1.Write(cgUse);
		binaryWriter_1.Write(cgOnNewPage);
		binaryWriter_1.Write((byte)cgChooseStyle);
		binaryWriter_1.Write((byte)cgGraphShowStyle);
		binaryWriter_1.Write((byte)cgGraphDisStyle);
		binaryWriter_1.Write(crUse);
		binaryWriter_1.Write(crOnNewPage);
		binaryWriter_1.Write((byte)crChooseStyle);
		binaryWriter_1.Write(crSummary);
		binaryWriter_1.Write(crSST);
		binaryWriter_1.Write(crResult);
		binaryWriter_1.Write(crRltCombine);
		binaryWriter_1.Write(crPerformance);
		binaryWriter_1.Write(crGpcSlices);
		binaryWriter_1.Write(crGpcRanges);
		binaryWriter_1.Write(clUse);
		binaryWriter_1.Write(clOnNewPage);
		binaryWriter_1.Write(clOptions);
		binaryWriter_1.Write(clCmpds);
		binaryWriter_1.Write(clcdGnlLevels);
		binaryWriter_1.Write(clcdGnlGraph);
		binaryWriter_1.Write(sqUse);
		binaryWriter_1.Write(sqOnNewPage);
		binaryWriter_1.Write(sqOptions);
		binaryWriter_1.Write(sqInjList);
	}

	public void SaveToFile(string fileName)
	{
		if (!fileName.EndsWith(".sty"))
		{
			fileName += ".sty";
		}
		try
		{
			Class49.OpenBinaryWriter(fileName, out fileInfo_0, out fileStream_0, out binaryWriter_0);
			SaveToFile(binaryWriter_0);
		}
		finally
		{
			Class49.FileStreamClose(ref fileStream_0, ref binaryWriter_0);
		}
	}
}
