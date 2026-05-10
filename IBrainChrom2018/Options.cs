using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace IBrainChrom2018;

public class Options
{
	public bool axisClrAsActive = true;

	public decimal axisLineWidth = 1m;

	public Color axisColor = Color.Black;

	public Color backClrBorder = Color.BurlyWood;

	public Color backClrChart = Color.BlanchedAlmond;

	public Color baselineColor = Color.Black;

	public Color caliCurveColor = Color.Black;

	private Color color_0 = Color.WhiteSmoke;

	private Color color_1 = Color.White;

	public Color[] dtColors = new Color[12];

	public Color gradSolvClrA = Color.AliceBlue;

	public Color gradSolvClrB = Color.LightGray;

	public Color gradSolvClrC = Color.LightYellow;

	public Color gradSolvClrD = Color.LightGreen;

	public bool backClrDefaultBorder = true;

	public bool backClrDefaultChart = true;

	public bool baselineColorAsActive = true;

	public Color[] sgColors = new Color[12];

	public bool baselineMarks = true;

	public DashStyle baselineStyle = DashStyle.Dot;

	public bool baselineVisible = true;

	public bool caliCurveClrAsActive = true;

	public bool dt1cAsInstru = true;

	public GcDisAuxYStyle gcDisAuxYStyle = GcDisAuxYStyle.Temperature;

	public bool gnlPlayEventsSounds;

	public bool gnlRequestOldFormatsConfirm;

	public bool gnlSendUnsuccReports;

	public bool gnlShowWinsOnTaskbar;

	public bool gnlWarnMaxZoom = true;

	public bool grpShowEvents;

	public bool grpShowGrid = true;

	public bool grpShowLegend;

	public bool grpShowWorkplaceLabels;

	public LcDisAuxYStyle lcDisAuxYStyle = LcDisAuxYStyle.Gradient;

	public bool peakAreaClrByTags = true;

	public bool peakAreaClrSetByCalib = true;

	public Font peakFont = new Font("Tahoma", 8f);

	public bool peakFtClrAsActiveSignal = true;

	public bool peakGroupID = true;

	public bool peakName = true;

	public bool peakNumber = true;

	public bool peakRetenTime = true;

	public ScaleToStyle scaleToStyle;

	public string sigAxisDisUnit = Class49.MesureUnit();

	public float sigAxisFrom;

	public float sigAxisOffset;

	public bool sigAxisRangeFixed;

	public float sigAxisScale = 1f;

	public string sigAxisTitle = "Voltage";

	public float sigAxisTo = 10f;

	public bool sigAxisVisible = true;

	public decimal sigLineWidth = 1m;

	public bool sigScalePsrAll = true;

	public bool sigScaleYModePreserveRelation = true;

	public TimeAxisData timeAxisData = TimeAxisData.Minute;

	public string timeAxisDisUnit = "min";

	public float timeAxisFrom;

	public float timeAxisOffset;

	public bool timeAxisRangeFixed;

	public float timeAxisScale = 1f;

	public string timeAxisTitle = "Time";

	public float timeAxisTo = 10f;

	public bool timeAxisVisible = true;

	public Font titleFont = new Font("Tahoma", 9f);

	public Font unitFont = new Font("Tahoma", 8f);

	public Font valueFont = new Font("Tahoma", 8f);

	public Color BackClrBorder
	{
		get
		{
			if (backClrDefaultBorder)
			{
				return color_0;
			}
			return backClrBorder;
		}
	}

	public Color BackClrChart
	{
		get
		{
			if (backClrDefaultChart)
			{
				return color_1;
			}
			return backClrChart;
		}
	}

	public Options()
	{
		InitDtSigColors();
	}

	public void InitDtSigColors()
	{
		dtColors[0] = Color.Black;
		dtColors[1] = Color.Blue;
		dtColors[2] = Color.Red;
		dtColors[3] = Color.Green;
		dtColors[4] = Color.LightBlue;
		dtColors[5] = Color.Brown;
		dtColors[6] = Color.Yellow;
		dtColors[7] = Color.LightGreen;
		dtColors[8] = Color.Teal;
		dtColors[9] = Color.Thistle;
		dtColors[10] = Color.Tan;
		dtColors[11] = Color.Black;
		sgColors[0] = Color.Black;
		sgColors[1] = Color.Blue;
		sgColors[2] = Color.Green;
		sgColors[3] = Color.Pink;
		sgColors[4] = Color.LightBlue;
		sgColors[5] = Color.Brown;
		sgColors[6] = Color.Yellow;
		sgColors[7] = Color.LightGreen;
		sgColors[8] = Color.Teal;
		sgColors[9] = Color.Thistle;
		sgColors[10] = Color.Tan;
		sgColors[11] = Color.Red;
	}

	public void InitGradientColors()
	{
		gradSolvClrA = Color.LightBlue;
		gradSolvClrB = Color.LightGray;
		gradSolvClrC = Color.LightYellow;
		gradSolvClrD = Color.LightGreen;
	}

	public void LoadFromFile(FileStream fileStream_0, BinaryReader binaryReader_0)
	{
		BinaryFormatter binaryFormatter = new BinaryFormatter();
		gnlShowWinsOnTaskbar = binaryReader_0.ReadBoolean();
		gnlPlayEventsSounds = binaryReader_0.ReadBoolean();
		gnlSendUnsuccReports = binaryReader_0.ReadBoolean();
		gnlRequestOldFormatsConfirm = binaryReader_0.ReadBoolean();
		gnlWarnMaxZoom = binaryReader_0.ReadBoolean();
		grpShowWorkplaceLabels = binaryReader_0.ReadBoolean();
		grpShowGrid = binaryReader_0.ReadBoolean();
		grpShowLegend = binaryReader_0.ReadBoolean();
		grpShowEvents = binaryReader_0.ReadBoolean();
		baselineVisible = binaryReader_0.ReadBoolean();
		baselineStyle = (DashStyle)binaryReader_0.ReadInt32();
		baselineMarks = binaryReader_0.ReadBoolean();
		baselineColorAsActive = binaryReader_0.ReadBoolean();
		baselineColor = (Color)binaryFormatter.Deserialize(fileStream_0);
		peakRetenTime = binaryReader_0.ReadBoolean();
		peakName = binaryReader_0.ReadBoolean();
		peakNumber = binaryReader_0.ReadBoolean();
		peakGroupID = binaryReader_0.ReadBoolean();
		peakFont = (Font)binaryFormatter.Deserialize(fileStream_0);
		peakFtClrAsActiveSignal = binaryReader_0.ReadBoolean();
		peakAreaClrSetByCalib = binaryReader_0.ReadBoolean();
		peakAreaClrByTags = binaryReader_0.ReadBoolean();
		backClrDefaultChart = binaryReader_0.ReadBoolean();
		backClrChart = (Color)binaryFormatter.Deserialize(fileStream_0);
		backClrDefaultBorder = binaryReader_0.ReadBoolean();
		backClrBorder = (Color)binaryFormatter.Deserialize(fileStream_0);
		axisLineWidth = binaryReader_0.ReadDecimal();
		axisClrAsActive = binaryReader_0.ReadBoolean();
		axisColor = (Color)binaryFormatter.Deserialize(fileStream_0);
		titleFont = (Font)binaryFormatter.Deserialize(fileStream_0);
		valueFont = (Font)binaryFormatter.Deserialize(fileStream_0);
		unitFont = (Font)binaryFormatter.Deserialize(fileStream_0);
		sigLineWidth = binaryReader_0.ReadDecimal();
		caliCurveClrAsActive = binaryReader_0.ReadBoolean();
		caliCurveColor = (Color)binaryFormatter.Deserialize(fileStream_0);
		dt1cAsInstru = binaryReader_0.ReadBoolean();
		for (int i = 0; i < 12; i++)
		{
			dtColors[i] = (Color)binaryFormatter.Deserialize(fileStream_0);
			sgColors[i] = (Color)binaryFormatter.Deserialize(fileStream_0);
		}
		timeAxisVisible = binaryReader_0.ReadBoolean();
		timeAxisTitle = binaryReader_0.ReadString();
		timeAxisData = (TimeAxisData)binaryReader_0.ReadInt32();
		timeAxisDisUnit = binaryReader_0.ReadString();
		timeAxisOffset = binaryReader_0.ReadSingle();
		timeAxisScale = binaryReader_0.ReadSingle();
		timeAxisRangeFixed = binaryReader_0.ReadBoolean();
		timeAxisFrom = binaryReader_0.ReadSingle();
		timeAxisTo = binaryReader_0.ReadSingle();
		sigAxisVisible = binaryReader_0.ReadBoolean();
		sigAxisTitle = binaryReader_0.ReadString();
		sigAxisDisUnit = binaryReader_0.ReadString();
		sigAxisOffset = binaryReader_0.ReadSingle();
		sigAxisScale = binaryReader_0.ReadSingle();
		sigAxisRangeFixed = binaryReader_0.ReadBoolean();
		sigAxisFrom = binaryReader_0.ReadSingle();
		sigAxisTo = binaryReader_0.ReadSingle();
		sigScaleYModePreserveRelation = binaryReader_0.ReadBoolean();
		sigScalePsrAll = binaryReader_0.ReadBoolean();
		scaleToStyle = (ScaleToStyle)binaryReader_0.ReadInt32();
		lcDisAuxYStyle = (LcDisAuxYStyle)binaryReader_0.ReadByte();
		gcDisAuxYStyle = (GcDisAuxYStyle)binaryReader_0.ReadByte();
		gradSolvClrA = (Color)binaryFormatter.Deserialize(fileStream_0);
		gradSolvClrB = (Color)binaryFormatter.Deserialize(fileStream_0);
		gradSolvClrC = (Color)binaryFormatter.Deserialize(fileStream_0);
		gradSolvClrD = (Color)binaryFormatter.Deserialize(fileStream_0);
	}

	public void LoadFromObject(Options options)
	{
		gnlShowWinsOnTaskbar = options.gnlShowWinsOnTaskbar;
		gnlPlayEventsSounds = options.gnlPlayEventsSounds;
		gnlSendUnsuccReports = options.gnlSendUnsuccReports;
		gnlRequestOldFormatsConfirm = options.gnlRequestOldFormatsConfirm;
		gnlWarnMaxZoom = options.gnlWarnMaxZoom;
		grpShowWorkplaceLabels = options.grpShowWorkplaceLabels;
		grpShowGrid = options.grpShowGrid;
		grpShowLegend = options.grpShowLegend;
		grpShowEvents = options.grpShowEvents;
		baselineVisible = options.baselineVisible;
		baselineStyle = options.baselineStyle;
		baselineMarks = options.baselineMarks;
		baselineColorAsActive = options.baselineColorAsActive;
		baselineColor = options.baselineColor;
		peakRetenTime = options.peakRetenTime;
		peakName = options.peakName;
		peakNumber = options.peakNumber;
		peakGroupID = options.peakGroupID;
		peakFont = options.peakFont;
		peakFtClrAsActiveSignal = options.peakFtClrAsActiveSignal;
		peakAreaClrSetByCalib = options.peakAreaClrSetByCalib;
		peakAreaClrByTags = options.peakAreaClrByTags;
		backClrDefaultChart = options.backClrDefaultChart;
		backClrChart = options.backClrChart;
		backClrDefaultBorder = options.backClrDefaultBorder;
		backClrBorder = options.backClrBorder;
		axisLineWidth = options.axisLineWidth;
		axisClrAsActive = options.axisClrAsActive;
		axisColor = options.axisColor;
		titleFont = options.titleFont;
		valueFont = options.valueFont;
		unitFont = options.unitFont;
		sigLineWidth = options.sigLineWidth;
		caliCurveClrAsActive = options.caliCurveClrAsActive;
		caliCurveColor = options.caliCurveColor;
		dt1cAsInstru = options.dt1cAsInstru;
		dtColors = (Color[])options.dtColors.Clone();
		sgColors = (Color[])options.sgColors.Clone();
		timeAxisVisible = options.timeAxisVisible;
		timeAxisTitle = options.timeAxisTitle;
		timeAxisData = options.timeAxisData;
		timeAxisDisUnit = options.timeAxisDisUnit;
		timeAxisOffset = options.timeAxisOffset;
		timeAxisScale = options.timeAxisScale;
		timeAxisRangeFixed = options.timeAxisRangeFixed;
		timeAxisFrom = options.timeAxisFrom;
		timeAxisTo = options.timeAxisTo;
		sigAxisVisible = options.sigAxisVisible;
		sigAxisTitle = options.sigAxisTitle;
		sigAxisDisUnit = options.sigAxisDisUnit;
		sigAxisOffset = options.sigAxisOffset;
		sigAxisScale = options.sigAxisScale;
		sigAxisRangeFixed = options.sigAxisRangeFixed;
		sigAxisFrom = options.sigAxisFrom;
		sigAxisTo = options.sigAxisTo;
		sigScaleYModePreserveRelation = options.sigScaleYModePreserveRelation;
		sigScalePsrAll = options.sigScalePsrAll;
		scaleToStyle = options.scaleToStyle;
		lcDisAuxYStyle = options.lcDisAuxYStyle;
		gcDisAuxYStyle = options.gcDisAuxYStyle;
		gradSolvClrA = options.gradSolvClrA;
		gradSolvClrB = options.gradSolvClrB;
		gradSolvClrC = options.gradSolvClrC;
		gradSolvClrD = options.gradSolvClrD;
	}

	public void SaveToFile(FileStream fileStream_0, BinaryWriter binaryWriter_0)
	{
		BinaryFormatter binaryFormatter = new BinaryFormatter();
		binaryWriter_0.Write(gnlShowWinsOnTaskbar);
		binaryWriter_0.Write(gnlPlayEventsSounds);
		binaryWriter_0.Write(gnlSendUnsuccReports);
		binaryWriter_0.Write(gnlRequestOldFormatsConfirm);
		binaryWriter_0.Write(gnlWarnMaxZoom);
		binaryWriter_0.Write(grpShowWorkplaceLabels);
		binaryWriter_0.Write(grpShowGrid);
		binaryWriter_0.Write(grpShowLegend);
		binaryWriter_0.Write(grpShowEvents);
		binaryWriter_0.Write(baselineVisible);
		binaryWriter_0.Write((int)baselineStyle);
		binaryWriter_0.Write(baselineMarks);
		binaryWriter_0.Write(baselineColorAsActive);
		binaryFormatter.Serialize(fileStream_0, baselineColor);
		binaryWriter_0.Write(peakRetenTime);
		binaryWriter_0.Write(peakName);
		binaryWriter_0.Write(peakNumber);
		binaryWriter_0.Write(peakGroupID);
		binaryFormatter.Serialize(fileStream_0, peakFont);
		binaryWriter_0.Write(peakFtClrAsActiveSignal);
		binaryWriter_0.Write(peakAreaClrSetByCalib);
		binaryWriter_0.Write(peakAreaClrByTags);
		binaryWriter_0.Write(backClrDefaultChart);
		binaryFormatter.Serialize(fileStream_0, backClrChart);
		binaryWriter_0.Write(backClrDefaultBorder);
		binaryFormatter.Serialize(fileStream_0, backClrBorder);
		binaryWriter_0.Write(axisLineWidth);
		binaryWriter_0.Write(axisClrAsActive);
		binaryFormatter.Serialize(fileStream_0, axisColor);
		binaryFormatter.Serialize(fileStream_0, titleFont);
		binaryFormatter.Serialize(fileStream_0, valueFont);
		binaryFormatter.Serialize(fileStream_0, unitFont);
		binaryWriter_0.Write(sigLineWidth);
		binaryWriter_0.Write(caliCurveClrAsActive);
		binaryFormatter.Serialize(fileStream_0, caliCurveColor);
		binaryWriter_0.Write(dt1cAsInstru);
		for (int i = 0; i < 12; i++)
		{
			binaryFormatter.Serialize(fileStream_0, dtColors[i]);
			binaryFormatter.Serialize(fileStream_0, sgColors[i]);
		}
		binaryWriter_0.Write(timeAxisVisible);
		binaryWriter_0.Write(timeAxisTitle);
		binaryWriter_0.Write((int)timeAxisData);
		binaryWriter_0.Write(timeAxisDisUnit);
		binaryWriter_0.Write(timeAxisOffset);
		binaryWriter_0.Write(timeAxisScale);
		binaryWriter_0.Write(timeAxisRangeFixed);
		binaryWriter_0.Write(timeAxisFrom);
		binaryWriter_0.Write(timeAxisTo);
		binaryWriter_0.Write(sigAxisVisible);
		binaryWriter_0.Write(sigAxisTitle);
		binaryWriter_0.Write(sigAxisDisUnit);
		binaryWriter_0.Write(sigAxisOffset);
		binaryWriter_0.Write(sigAxisScale);
		binaryWriter_0.Write(sigAxisRangeFixed);
		binaryWriter_0.Write(sigAxisFrom);
		binaryWriter_0.Write(sigAxisTo);
		binaryWriter_0.Write(sigScaleYModePreserveRelation);
		binaryWriter_0.Write(sigScalePsrAll);
		binaryWriter_0.Write((int)scaleToStyle);
		binaryWriter_0.Write((byte)lcDisAuxYStyle);
		binaryWriter_0.Write((byte)gcDisAuxYStyle);
		binaryFormatter.Serialize(fileStream_0, gradSolvClrA);
		binaryFormatter.Serialize(fileStream_0, gradSolvClrB);
		binaryFormatter.Serialize(fileStream_0, gradSolvClrC);
		binaryFormatter.Serialize(fileStream_0, gradSolvClrD);
	}
}
