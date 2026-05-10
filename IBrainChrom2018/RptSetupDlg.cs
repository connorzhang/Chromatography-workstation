using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Windows.Forms;

namespace IBrainChrom2018;

public class RptSetupDlg : LclDialog
{
	private int int_2;

	private float float_0;

	private SolidBrush solidBrush_0 = new SolidBrush(Color.Black);

	private Bitmap bitmap_0;

	private Bitmap bitmap_1;

	private Bitmap bitmap_2;

	private bool bool_1;

	private bool bool_2;

	private CmpdDisplay class8_0 = new CmpdDisplay(WinStyle.CaliGnl, null);

	private GvPrtInfos gvPrtInfos_0 = new GvPrtInfos();

	private int int_3;

	private int int_4;

	private int int_5;

	private int[] int_6 = new int[0];

	private ChromDisplay chromDisplay_0 = new ChromDisplay(WinStyle.Chromatogram, null);

	private int int_7;

	private int[] int_8 = new int[0];

	private int int_9;

	private int[] int_10 = new int[0];

	private int int_11;

	private int[] int_12 = new int[0];

	private Chromatogram[] chromatogram_0;

	private GvPrtInfos gvPrtInfos_1 = new GvPrtInfos();

	private int int_13;

	private int int_14;

	private GvPrtInfos gvPrtInfos_2 = new GvPrtInfos();

	private int int_15;

	private int int_16;

	private int int_17;

	private int int_18 = -1;

	private int int_19;

	private Chromatogram[] chromatogram_1 = new Chromatogram[0];

	private Chromatogram[] chromatogram_2 = new Chromatogram[0];

	private int int_20;

	private Chromatogram[] chromatogram_3 = new Chromatogram[0];

	private int int_21;

	private GvPrtInfos gvPrtInfos_3 = new GvPrtInfos();

	private int int_22;

	private int int_23;

	private GvPrtInfos gvPrtInfos_4 = new GvPrtInfos();

	private GradientDisplay gradientDisplay_0 = new GradientDisplay(WinStyle.Method, null);

	private GvPrtInfos gvPrtInfos_5 = new GvPrtInfos();

	private int int_24;

	private int int_25;

	private GvInfos gvInfos_0 = new GvInfos();

	private int int_26;

	private int int_27;

	private bool bool_3;

	private Image image_0;

	private Image image_1;

	private bool bool_4;

	private int int_28;

	private float float_1;

	private float float_2;

	private int int_29;

	private int int_30;

	private int int_31;

	private int int_32;

	private int int_33;

	private int int_34;

	private int[] int_35 = new int[0];

	private int int_36;

	private int int_37;

	private int int_38;

	private int int_39;

	private Pen pen_0 = new Pen(Color.Black, 1f);

	private GvPrtInfos gvPrtInfos_6 = new GvPrtInfos();

	private int int_40;

	private int int_41;

	private float float_3;

	private float float_4;

	private int int_42;

	private GradientDisplay gradientDisplay_1 = new GradientDisplay(WinStyle.Method, null);

	private RectangleF rectangleF_0;

	private Rectangle rectangle_0;

	private Rectangle[] rectangle_1 = new Rectangle[0];

	private Rectangle rectangle_2;

	private Rectangle rectangle_3;

	private static Rectangle rectangle_4;

	private Rectangle rectangle_5;

	private GvPrtInfos gvPrtInfos_7 = new GvPrtInfos();

	private int int_43;

	private int int_44;

	private int int_45;

	private int[] int_46 = new int[0];

	private RptSetup rptSetup_0 = new RptSetup();

	private int int_47;

	private int[] int_48 = new int[0];

	private StringFormat stringFormat_0 = new StringFormat();

	private int int_49;

	private int int_50;

	private GvPrtInfos gvPrtInfos_8 = new GvPrtInfos();

	private int int_51;

	private int int_52;

	private Color[] color_0;

	private GvPrtInfos gvPrtInfos_9 = new GvPrtInfos();

	private int int_53;

	private int int_54;

	private int int_55;

	private bool bool_5;

	private bool bool_6;

	private bool bool_7;

	private bool bool_8;

	private bool bool_9;

	private bool bool_10;

	private bool bool_11;

	private string string_158 = "";

	private GvPrtInfos gvPrtInfos_10 = new GvPrtInfos();

	private SizeF sizeF_0 = new SizeF(350f, 330f);

	private SizeF sizeF_1 = new SizeF(550f, 230f);

	private SizeF sizeF_2 = new SizeF(450f, 190f);

	private float float_5;

	private int int_56;

	private int int_57;

	private bool bool_12;

	private LclButton btnlhFont;

	private LclButton btnlhLeftImage;

	private LclButton btnlhRightImage;

	private LclButton btnNew;

	private LclButton btnOpen;

	private LclButton btnPreview;

	private LclButton btnPrint;

	private LclButton btnSave;

	private LclButton btnSaveAs;

	private LclButton btnSaveToPdf;

	private LclCheckBox cbcgOnNewPage;

	private LclCheckBox cbcgUse;

	private LclCheckBox cbciciAdvance;

	private LclCheckBox cbciciCalculation;

	private LclCheckBox cbciciGpcRanges;

	private LclCheckBox cbciciIntegration;

	private LclCheckBox cbciciMeasurement;

	private LclCheckBox cbciciPDA;

	private LclCheckBox cbciOnNewPage;

	private LclCheckBox cbciUse;

	private LclCheckBox cbclcdGnlGraph;

	private LclCheckBox cbclcdGnlLevels;

	private LclCheckBox cbclCmpds;

	private LclCheckBox cbclOnNewPage;

	private LclCheckBox cbclOptions;

	private LclCheckBox cbclUse;

	private LclCheckBox cbcrGpcRanges;

	private LclCheckBox cbcrGpcSlices;

	private LclCheckBox cbcrOnNewPage;

	private LclCheckBox cbcrPerformance;

	private LclCheckBox cbcrResult;

	private LclCheckBox cbcrRltCombine;

	private LclCheckBox cbcrSST;

	private LclCheckBox cbcrSummary;

	private LclCheckBox cbcrUse;

	private LclCheckBox cblhBorder;

	private LclCheckBox cblhGrayBkgnd;

	private LclCheckBox cblhJstFstPage;

	private LclCheckBox cblhUse;

	private LclCheckBox cblhUseLeftImage;

	private LclCheckBox cblhUseRightImage;

	private LclCheckBox cbmtdAcquisition;

	private LclCheckBox cbmtdAS;

	private LclCheckBox cbmtdciAdvance;

	private LclCheckBox cbmtdciCalculation;

	private LclCheckBox cbmtdciGpcRanges;

	private LclCheckBox cbmtdciMeasurement;

	private LclCheckBox cbmtdciPDA;

	private LclCheckBox cbmtdGcProgTemp;

	private LclCheckBox cbmtdGcPTGraph;

	private LclCheckBox cbmtdIntegration;

	private LclCheckBox cbmtdLcGradient;

	private LclCheckBox cbmtdLcGrdtGraph;

	private LclCheckBox cbmtdLcGrdtItems;

	private LclCheckBox cbmtdOnNewPage;

	public LclCheckBox cbmtdUse;

	private LclCheckBox cbpsColors;

	private LclCheckBox cbpsDrawGraphsBkgnd;

	private LclCheckBox cbpsSign;

	private LclCheckBox cbrhDateTime;

	private LclCheckBox cbrhOnNewPage;

	private LclCheckBox cbrhSystemInfo;

	private LclCheckBox cbrhUse;

	private LclCheckBox cbsqInjList;

	private LclCheckBox cbsqOnNewPage;

	private LclCheckBox cbsqOptions;

	private LclCheckBox cbsqUse;

	private DataGridViewTextBoxColumn Column1;

	private IContainer icontainer_1;

	private DisLg disLg_0;

	private LclFontBtn fbtnpsItem;

	private LclFontBtn fbtnpsValue;

	private FontDialog fontDialog_0;

	private LclGroupBox gbcgChroms;

	private LclGroupBox gbcgDisStyle;

	private LclGroupBox gbcgShowStyle;

	private LclGroupBox gbciChromInfo;

	private LclGroupBox gbciChroms;

	private LclGroupBox gbclCmpd;

	private LclGroupBox gbcrChroms;

	private LclGroupBox gblhLines;

	private LclGroupBox gblhUseLeftImage;

	private LclGroupBox gblhUseRightImage;

	private LclGroupBox gbmtdChromInfo;

	private LclGroupBox gbpsMargins;

	private LclGroupBox gbrhPrintFullPath;

	private LclRptLabHeaderGV gvlhLines;

	private LclLabel lblhLinesNum;

	private LclLabel lbpsmgBottom;

	private LclLabel lbpsmgInterval;

	private LclLabel lbpsmgLeft;

	private LclLabel lbpsmgRight;

	private LclLabel lbpsmgTop;

	private LclLabel lclLabel1;

	private LclLabel lclLabel2;

	private LclLabel lclLabel3;

	private LclNumericUpDown nudlhLinesNum;

	private LclNumericUpDown nudpsmgBottom;

	private LclNumericUpDown nudpsmgInterval;

	private LclNumericUpDown nudpsmgLeft;

	private LclNumericUpDown nudpsmgRight;

	private LclNumericUpDown nudpsmgTop;

	private OpenFileDialog openFileDialog_0;

	private OpenFileDialog openFileDialog_1;

	private PrintDialog printDialog_0;

	private PrintDocument printDocument_0;

	private PrintPreviewDialog prtPrvDlg;

	private LclRadioButton rbcgchmActive;

	private LclRadioButton rbcgchmAll;

	private LclRadioButton rbcgdsCurrent;

	private LclRadioButton rbcgdsWhole;

	private LclRadioButton rbcgssCombine;

	private LclRadioButton rbcgssSeparate;

	private LclRadioButton rbcichmActive;

	private LclRadioButton rbcichmAll;

	private LclRadioButton rbcrchmActive;

	private LclRadioButton rbcrchmAll;

	private LclRadioButton rblhL;

	private LclRadioButton rblhlFixed;

	private LclRadioButton rblhlHeader;

	private LclRadioButton rblhlOriginal;

	private LclRadioButton rblhM;

	private LclRadioButton rblhR;

	private LclRadioButton rblhrFixed;

	private LclRadioButton rblhrHeader;

	private LclRadioButton rblhrOriginal;

	private LclRadioButton rbrhnsAlways;

	private LclRadioButton rbrhnsOthersOnly;

	private SaveFileDialog saveFileDialog_0;

	private SmyHdrPara smyHdrPara_0;

	private SST sst_0;

	private LclTextBox tblhLeftImage;

	private LclTextBox tblhlWidth;

	private LclTextBox tblhRightImage;

	private LclTextBox tblhrWidth;

	private LclTabControl tcRptSetup;

	private TabPage tpCali;

	private TabPage tpChromGraph;

	private TabPage tpChromInfo;

	private TabPage tpChromRlts;

	private TabPage tpLabHeader;

	private TabPage tpMethod;

	private TabPage tpPageSetup;

	private TabPage tpRptHeader;

	private TabPage tpSeq;

	private void method_0(int int_58, int int_59, ChromInfo chromInfo_0, ref string string_159, ref string string_160, ref string string_161, ref string string_162)
	{
		switch (int_58 - int_59)
		{
		case 0:
			string_159 = Lang.PS("加减谱图", "Add Subtraction") + "." + Lang.PS("谱图", "Chromatogram");
			string_160 = chromInfo_0.asChrom + "  [ ";
			switch (chromInfo_0.asMatching)
			{
			case ASMatchStyle.NoChange:
				string_160 = string_160 + Lang.PS("无变化", "No Change") + " ]";
				break;
			case ASMatchStyle.OffsetChrom:
				string_160 = string_160 + Lang.PS("偏移谱图", "Offset Chrom") + " ]";
				break;
			case ASMatchStyle.ScaleChrom:
				string_160 = string_160 + Lang.PS("缩放谱图", "Scale Chrom") + " ]";
				break;
			}
			break;
		case 1:
			string_159 = Lang.PS("柱效计算", "Column Caculation") + ":";
			switch (chromInfo_0.ccStyle)
			{
			case ColumnCalcuStyle.Statistical:
				string_160 = Lang.PS("静态时间", "Statistical Moments");
				break;
			case ColumnCalcuStyle.From50per:
				string_160 = Lang.PS("50%宽起始", "From Width at 50%");
				break;
			}
			break;
		case 2:
			string_159 = Lang.PS("非保留峰时间", "Unretained Peak") + ":";
			string_160 = chromInfo_0.ccColumnUT + " " + instrument.form.dlgMethodSetup.lbccUnretainedPeakU.Text;
			string_161 = Lang.PS("柱长", "Column Length") + ":";
			string_162 = chromInfo_0.ccColumnLength + " " + instrument.form.dlgMethodSetup.lbccColumnLengthU.Text;
			break;
		default:
			string_159 = (string_160 = "*");
			break;
		}
	}

	private void method_1(int int_58, int int_59, ChromInfo chromInfo_0, ref string string_159, ref string string_160, ref string string_161, ref string string_162)
	{
		switch (int_58 - int_59)
		{
		case 0:
			string_159 = Lang.PS("校正文件:", "Calib. File:");
			string_160 = chromInfo_0.cclCalibration;
			string_161 = Lang.PS("计算", "Calculation") + ":";
			switch (chromInfo_0.cclCalcu)
			{
			case CalcuStyle.Uncal:
				string_162 = Lang.PS("无校正", "Uncal");
				break;
			case CalcuStyle.ESTD:
				string_162 = Lang.PS("外标法", "ESTD");
				break;
			case CalcuStyle.ISTD:
				string_162 = Lang.PS("内标法", "ISTD");
				break;
			}
			break;
		case 1:
			string_159 = Lang.PS("作者", "Author") + ":";
			string_160 = chromInfo_0.cclAuthor;
			break;
		case 2:
			string_159 = Lang.PS("描述", "Description") + ":";
			string_160 = chromInfo_0.cclDescription;
			break;
		case 3:
			string_159 = Lang.PS("创建时间", "Create Time") + ":";
			string_160 = chromInfo_0.cclCreateTime.ToLongDateString() + "  " + chromInfo_0.cclCreateTime.ToShortTimeString();
			string_161 = Lang.PS("修改时间", "Modified Time") + ":";
			string_162 = chromInfo_0.cclModifiedTime.ToLongDateString() + "  " + chromInfo_0.cclModifiedTime.ToShortTimeString();
			break;
		case 4:
			string_159 = Lang.PS("参数", "Parameters") + ":";
			string_160 = (chromInfo_0.prsUseScaleFactor ? (Lang.PS("使用缩放因子", "Use Scale Factor") + " [ " + chromInfo_0.prsScaleFactor + " ]  ") : "");
			if (string_160 != "")
			{
				string_161 = Lang.PS("缩放后单位", "Use Unit") + ":";
				string_162 = chromInfo_0.prsUnitAfterScale;
			}
			break;
		case 5:
			string_159 = Lang.PS("未识别响应", "Uncal. Base") + ":";
			switch (chromInfo_0.prsUncalBase)
			{
			case RespStyle.Area:
				string_160 = Lang.PS("面积", "Area");
				break;
			case RespStyle.Height:
				string_160 = Lang.PS("高度", "Height");
				break;
			case RespStyle.AreaSquare:
				string_160 = Lang.PS("面积平方根", "AreaSquare");
				break;
			case RespStyle.PeakHeightSquare:
				string_160 = Lang.PS("高度平方根", "PeakHeightSquare");
				break;
			}
			string_161 = Lang.PS("未识别因子", "Uncal. Factor") + ":";
			string_162 = chromInfo_0.prsUncalAmtRespF + " " + instrument.form.dlgMethodSetup.lbprsUncalAmtRespFU.Text;
			break;
		case 6:
			string_159 = Lang.PS("结果表报告", "Report in Result Table") + ":";
			string_160 = (chromInfo_0.rtrHideISTDPeak ? (Lang.PS("隐藏内标峰", "Hide ISTD Peak") + ", ") : "");
			switch (chromInfo_0.rtrRltReportPeaks)
			{
			case RltReportPeaks.AllDetectedPeaks:
				string_160 += Lang.PS("所有检测峰", "All Detected Peaks");
				break;
			case RltReportPeaks.IdentifiedPeaks:
				string_160 += Lang.PS("所有识别峰", "All Identified Peaks");
				break;
			case RltReportPeaks.CaliPeaks:
				string_160 += Lang.PS("所有校正峰", "All Peaks in Calibration");
				break;
			}
			break;
		default:
			string_159 = (string_160 = "*");
			break;
		}
	}

	public static Color _clr(bool psColors, Color color)
	{
		if (psColors)
		{
			return color;
		}
		int num = (color.R + color.G + color.B) / 3;
		return Color.FromArgb(num, num, num);
	}

	private void method_2(int int_58, int int_59, ChromInfo chromInfo_0, ChromInfoR chromInfoR_0, ref string string_159, ref string string_160, ref string string_161, ref string string_162)
	{
		switch (int_58 - int_59)
		{
		case 0:
			string_159 = Lang.PS("方法描述", "Method Description") + ":";
			string_160 = chromInfo_0.msmMtdDspt;
			break;
		case 1:
			string_159 = ((instrument.instruStyle != InstruStyle.GC) ? Lang.PS("色谱柱", "Column") : "色谱柱") + ":";
			string_160 = chromInfo_0.msmColumn;
			string_161 = ((instrument.instruStyle != InstruStyle.GC) ? Lang.PS("流动相", "Mobile Phase") : "柱温") + ":";
			string_162 = chromInfo_0.msmMobilePhase;
			break;
		case 2:
			string_159 = ((instrument.instruStyle != InstruStyle.GC) ? Lang.PS("流速", "Flow Rate") : "载气") + ":";
			string_160 = chromInfo_0.msmFlowRate;
			string_161 = ((instrument.instruStyle != InstruStyle.GC) ? Lang.PS("压力", "Pressure") : "气体1") + ":";
			string_162 = chromInfo_0.msmPressure;
			break;
		case 3:
			string_159 = ((instrument.instruStyle != InstruStyle.GC) ? Lang.PS("检测", "Detection") : "气体2") + ":";
			string_160 = chromInfo_0.msmDetection;
			string_161 = ((instrument.instruStyle != InstruStyle.GC) ? Lang.PS("温度", "Temperature") : "检测器") + ":";
			string_162 = chromInfo_0.msmTemperature;
			break;
		case 4:
			string_159 = Lang.PS("备注", "Note") + ":";
			string_160 = chromInfo_0.msmNote;
			break;
		case 5:
			string_159 = Lang.PS("采集", "Acquisition") + ":";
			string_160 = (chromInfoR_0.AcqAutoStop ? (Lang.PS("自动结束", "Enable Autostop") + " [ " + chromInfoR_0.AcqRunTime + " min ]") : "");
			break;
		case 6:
		{
			string_159 = Lang.PS("外部控制", "External Control") + ":";
			string text = (chromInfoR_0.EcExternalControl ? (Lang.PS("外部开始/结束", "External Start/Stop") + ", ") : "");
			switch (chromInfoR_0.ExtCtrlStart)
			{
			case ExtCtrlStart.StartOnly:
				text = text + Lang.PS("仅开始", "Start Only") + ", ";
				break;
			case ExtCtrlStart.StartRestart:
				text = text + Lang.PS("开始-开始", "Start - Restart") + ", ";
				break;
			case ExtCtrlStart.StartStop:
				text = text + Lang.PS("开始-结束", "Start - Stop") + ", ";
				break;
			}
			switch (chromInfoR_0.ExtCtrlSignal)
			{
			case ExtCtrlSignal.Up:
				text += Lang.PS("上升沿", "Up");
				break;
			case ExtCtrlSignal.Down:
				text += Lang.PS("下降沿", "Down");
				break;
			}
			string_160 = text;
			break;
		}
		default:
			string_159 = (string_160 = "*");
			break;
		}
	}

	private void method_3(int int_58, int int_59, ChromInfo chromInfo_0, ChromInfoR chromInfoR_0, ref string string_159, ref string string_160, ref string string_161, ref string string_162, Chromatogram chromatogram_4)
	{
		method_2(int_58, int_59, chromInfo_0, chromInfoR_0, ref string_159, ref string_160, ref string_161, ref string_162);
		int num = int_58 - int_59;
		if (num == 7)
		{
			string_159 = Lang.PS("进样时间:", "Injection Time:");
			DateTime dtAcquire = chromatogram_4.injAnalysis.dtAcquire;
			string_160 = dtAcquire.ToLongDateString() + " " + dtAcquire.ToLongTimeString();
			string_161 = Lang.PS("采集时间:", "Sample Time:");
			string_162 = chromatogram_4.injAnalysis.tsAcquire.TotalMinutes.ToString("0.000min");
		}
	}

	private void method_4(int int_58, int int_59, ChromInfo chromInfo_0, ref string string_159, ref string string_160, ref string string_161, ref string string_162)
	{
		switch (int_58 - int_59)
		{
		case 0:
			string_159 = "[ " + Lang.PS("峰纯度选项", "Peak Purity Options") + " ]";
			break;
		case 1:
			string_159 = Lang.PS("限制波长范围", "Restrict Wavelength Range") + ":";
			string_160 = (chromInfo_0.ppoRestrictWaveLength ? (chromInfo_0.ppoFrom + " - " + chromInfo_0.ppoTo + " nm") : Lang.PS("非", "False"));
			break;
		case 2:
			string_159 = Lang.PS("纯度极限", "Purity Threshold") + ":";
			string_160 = chromInfo_0.ppoPurityThreshold.ToString();
			string_161 = Lang.PS("吸收极限", "Absorbance Threshold") + ":";
			string_162 = chromInfo_0.ppoAbsorbanceThreshold + "%";
			break;
		case 3:
			string_159 = Lang.PS("使用点数", "Used Points") + ":";
			switch (chromInfo_0.ppoUsedPoints)
			{
			case PPO_UsedPoints.All:
				string_160 = Lang.PS("全部", "All");
				break;
			case PPO_UsedPoints.Five:
				string_160 = Lang.PS("五点", "Five");
				break;
			}
			string_161 = Lang.PS("使用背景修正", "Use Background Correction") + ":";
			string_162 = (chromInfo_0.ppoUseBackCorr ? Lang.PS("是", "True") : Lang.PS("非", "False"));
			break;
		case 4:
			string_159 = "[ " + Lang.PS("库分析选项", "Library Search Options") + " ]";
			break;
		case 5:
			string_159 = Lang.PS("匹配规则", "Match Criteria") + ":";
			switch (chromInfo_0.lsoMatchCriteria)
			{
			case LSO_MatchCriteria.LeastSquare:
				string_160 = Lang.PS("最小平方", "Least Square");
				break;
			case LSO_MatchCriteria.WeightedLeastSquare:
				string_160 = Lang.PS("重量最小平方", "Weighted Least Square");
				break;
			case LSO_MatchCriteria.Correlation:
				string_160 = Lang.PS("修正", "Correlation");
				break;
			}
			break;
		case 6:
			string_159 = Lang.PS("匹配因子极限", "Match Factor Threshold") + ":";
			string_160 = chromInfo_0.lsoMatchFactorThreshold.ToString();
			string_161 = Lang.PS("最大显示波数", "Max. Number of Hits") + ":";
			string_162 = chromInfo_0.lsoMaxNumHits.ToString();
			break;
		case 7:
			string_159 = Lang.PS("限制波长范围", "Restrict Wavelength Range") + ":";
			string_160 = (chromInfo_0.lsoRestrictWaveLength ? (chromInfo_0.lsoFrom + " - " + chromInfo_0.lsoTo + " nm") : Lang.PS("非", "False"));
			string_161 = Lang.PS("限制保留时间", "Restrict Reten. Time") + ":";
			string_162 = (chromInfo_0.lsoRestrictRT ? (chromInfo_0.lsoRestrictRTV + "%") : Lang.PS("非", "False"));
			break;
		case 8:
			string_159 = Lang.PS("使用背景修正", "Use Background Correction") + ":";
			string_160 = (chromInfo_0.lsoUseBackCorr ? Lang.PS("是", "True") : Lang.PS("非", "False"));
			string_161 = Lang.PS("所有检测峰", "For All Detected Peaks") + ":";
			string_162 = (chromInfo_0.lsoForAllDetectedPeaks ? Lang.PS("是", "True") : Lang.PS("非", "False"));
			break;
		case 9:
			string_159 = "[ " + Lang.PS("匹配库", "Match Library") + " ]";
			string_160 = "";
			break;
		case 10:
		{
			string_159 = "PDA" + Lang.PS("匹配库", "Match Library") + ":";
			string_160 = "";
			for (int i = 0; i < chromInfo_0.pdaRows.Length; i++)
			{
				PDARow pDARow = chromInfo_0.pdaRows[i];
				string text = ((i != 0) ? ", " : "");
				string text2 = (pDARow.used ? "√" : "×");
				string text3 = string_160;
				string_160 = text3 + text + "[" + text2 + "]" + pDARow.name;
			}
			break;
		}
		default:
			string_159 = (string_160 = "*");
			break;
		}
	}

	private void method_5(int int_58, int int_59, ChromInfo chromInfo_0, ref string string_159, ref string string_160, ref string string_161, ref string string_162)
	{
		switch (int_58 - int_59)
		{
		case 0:
		{
			string_159 = Lang.PS("百分比类型GPC表", "Percent Type GPC Ranges Table") + ":";
			int num2 = chromInfo_0.percents.Length;
			if (num2 != 0)
			{
				for (int j = 0; j < num2; j++)
				{
					if (j != 0)
					{
						string_160 += ", ";
					}
					GPC_RangeRow gPC_RangeRow2 = chromInfo_0.percents[j];
					string text2 = string_160;
					string_160 = text2 + "[ " + gPC_RangeRow2.float_0 + " - " + gPC_RangeRow2.high + " ]";
				}
			}
			else
			{
				string_160 = Lang.PS("[ 空 ]", "[ Null ]");
			}
			break;
		}
		case 1:
		{
			string_159 = Lang.PS("分子量类型GPC表", "Mw Type GPC Ranges Table") + ":";
			int num = chromInfo_0.gpc_RangeRow_0.Length;
			if (num != 0)
			{
				for (int i = 0; i < num; i++)
				{
					if (i != 0)
					{
						string_160 += ", ";
					}
					GPC_RangeRow gPC_RangeRow = chromInfo_0.gpc_RangeRow_0[i];
					string text = string_160;
					string_160 = text + "[ " + gPC_RangeRow.high + " - " + gPC_RangeRow.float_0 + " ]";
				}
			}
			else
			{
				string_160 = Lang.PS("[ 空 ]", "[ Null ]");
			}
			break;
		}
		default:
			string_159 = (string_160 = "*");
			break;
		}
	}

	private int method_6(int int_58)
	{
		return PrinterUnitConvert.Convert(int_58, PrinterUnit.Display, PrinterUnit.TenthsOfAMillimeter);
	}

	private void method_7(bool bool_13, ref int[] int_58, int int_59, int int_60)
	{
		if (bool_13 && int_60 > 0)
		{
			int num = int_58.Length;
			Array.Resize(ref int_58, num + int_60);
			int num2 = int_59;
			while (num < int_58.Length)
			{
				int_58[num++] = num2++;
			}
		}
	}

	private void method_8(int int_58, ref float float_6)
	{
		if (float_6 == (float)(rectangle_4.Top + int_28))
		{
			float_6 -= int_28;
		}
		else if (float_6 == float_0 + (float)int_28)
		{
			float_6 -= int_28;
			float_6 += 10f;
		}
		else
		{
			float_6 += ((int_58 % 100 == 0) ? 10f : 0f);
		}
	}

	private void btnlhFont_Click(object sender, EventArgs e)
	{
		if (int_18 >= 0)
		{
			LabHdrTag labHdrTag = (LabHdrTag)gvlhLines.Rows[int_18].Tag;
			if (fontDialog_0 == null)
			{
				fontDialog_0 = new FontDialog();
				fontDialog_0.ShowColor = true;
			}
			fontDialog_0.Font = labHdrTag.font;
			fontDialog_0.Color = labHdrTag.color;
			if (fontDialog_0.ShowDialog() == DialogResult.OK)
			{
				labHdrTag.font = fontDialog_0.Font;
				labHdrTag.color = fontDialog_0.Color;
				gvlhLines.Rows[int_18].Tag = labHdrTag;
				gvlhLines.InvalidateRow(int_18);
			}
		}
	}

	private void btnlhLeftImage_Click(object sender, EventArgs e)
	{
		if (openFileDialog_0 == null)
		{
			openFileDialog_0 = new OpenFileDialog();
			openFileDialog_0.Filter = "(*.*)|*.*";
		}
		if (openFileDialog_0.ShowDialog() == DialogResult.OK)
		{
			FileInfo fileInfo = new FileInfo(openFileDialog_0.FileName);
			if (sender == btnlhLeftImage)
			{
				tblhLeftImage.Tag = openFileDialog_0.FileName;
				tblhLeftImage.Text = fileInfo.Name;
			}
			if (sender == btnlhRightImage)
			{
				tblhRightImage.Tag = openFileDialog_0.FileName;
				tblhRightImage.Text = fileInfo.Name;
			}
		}
	}

	private void btnNew_Click(object sender, EventArgs e)
	{
		rptSetup_0.Init();
		method_69(AccStyle.Read, rptSetup_0);
		string_158 = "";
		method_70();
	}

	private void btnOpen_Click(object sender, EventArgs e)
	{
		if (openFileDialog_1 == null)
		{
			openFileDialog_1 = new OpenFileDialog();
			openFileDialog_1.InitialDirectory = ((instrument == null || instrument.pjtDir == null) ? "" : instrument.pjtDir.PjtFullName);
			openFileDialog_1.Filter = Class49.MakeFileFilter(".sty");
		}
		if (openFileDialog_1.ShowDialog() == DialogResult.OK)
		{
			rptSetup_0.LoadFromFile(string_158 = openFileDialog_1.FileName);
			method_69(AccStyle.Read, rptSetup_0);
			method_70();
		}
	}

	private void btnPreview_Click(object sender, EventArgs e)
	{
		method_69(AccStyle.Write, rptSetup_0);
		Preview();
	}

	private void btnPrint_Click(object sender, EventArgs e)
	{
		print(refresh: true);
	}

	private void btnSave_Click(object sender, EventArgs e)
	{
		if (string_158 == "")
		{
			btnSaveAs_Click(null, null);
			return;
		}
		method_69(AccStyle.Write, rptSetup_0);
		rptSetup_0.SaveToFile(string_158);
	}

	private void btnSaveAs_Click(object sender, EventArgs e)
	{
		if (saveFileDialog_0 == null)
		{
			saveFileDialog_0 = new SaveFileDialog();
			saveFileDialog_0.InitialDirectory = ((instrument == null || instrument.pjtDir == null) ? "" : instrument.pjtDir.PjtFullName);
			saveFileDialog_0.Filter = "(*.sty)|*.sty";
		}
		if (saveFileDialog_0.ShowDialog() == DialogResult.OK)
		{
			string_158 = saveFileDialog_0.FileName;
			method_69(AccStyle.Write, rptSetup_0);
			rptSetup_0.SaveToFile(string_158);
			method_70();
		}
	}

	private void btnSaveToPdf_Click(object sender, EventArgs e)
	{
		saveToPdf(refresh: true);
	}

	private bool method_9(Graphics graphics_0, string string_159, bool bool_13, float float_6, out float float_7)
	{
		Font font = (bool_13 ? rptSetup_0.psItemFont : rptSetup_0.psValueFont);
		int num = (bool_13 ? int_33 : int_56);
		SizeF sizeF = graphics_0.MeasureString(string_159, font, num);
		if (bool_13)
		{
			int_29 = Convert.ToInt32(sizeF.Width) + 1;
			int_29 = Math.Max(int_29, int_30);
			int_56 = rectangle_4.Width - int_29;
		}
		float_7 = sizeF.Height;
		return float_6 + float_7 < (float)rectangle_4.Bottom;
	}

	private bool method_10(Graphics graphics_0, string string_159, bool bool_13, float float_6, out float float_7)
	{
		Font font = (bool_13 ? rptSetup_0.psItemFont : rptSetup_0.psValueFont);
		int num = (bool_13 ? int_30 : int_57);
		float_7 = graphics_0.MeasureString(string_159, font, num).Height;
		return float_6 + float_7 < (float)rectangle_4.Bottom;
	}

	private void cblhGrayBkgnd_CheckedChanged(object sender, EventArgs e)
	{
		gvlhLines.grayBkgnd = cblhGrayBkgnd.Checked;
		gvlhLines.Refresh();
	}

	private void cblhUseLeftImage_CheckedChanged(object sender, EventArgs e)
	{
		LclCheckBox lclCheckBox = sender as LclCheckBox;
		LclGroupBox lclGroupBox = lclCheckBox.Tag as LclGroupBox;
		lclGroupBox.Enabled = lclCheckBox.Checked;
	}

	private void cbpsColors_CheckedChanged(object sender, EventArgs e)
	{
		cbpsDrawGraphsBkgnd.Enabled = cbpsColors.Checked;
	}

	private void cbsqUse_CheckedChanged(object sender, EventArgs e)
	{
		LclCheckBox lclCheckBox = sender as LclCheckBox;
		int imageIndex = (lclCheckBox.Checked ? SystemImageListResource1.int_1 : SystemImageListResource1.int_0);
		if (lclCheckBox == cblhUse)
		{
			tpLabHeader.ImageIndex = imageIndex;
		}
		else if (lclCheckBox == cbrhUse)
		{
			tpRptHeader.ImageIndex = imageIndex;
		}
		else if (lclCheckBox == cbmtdUse)
		{
			tpMethod.ImageIndex = imageIndex;
		}
		else if (lclCheckBox == cbciUse)
		{
			tpChromInfo.ImageIndex = imageIndex;
		}
		else if (lclCheckBox == cbsqUse)
		{
			tpSeq.ImageIndex = imageIndex;
		}
		else if (lclCheckBox == cbcgUse)
		{
			tpChromGraph.ImageIndex = imageIndex;
		}
		else if (lclCheckBox == cbcrUse)
		{
			tpChromRlts.ImageIndex = imageIndex;
		}
		else if (lclCheckBox == cbclUse)
		{
			tpCali.ImageIndex = imageIndex;
		}
	}

	public void ChromFormLoadStyFile(string styFile)
	{
		if (File.Exists(styFile))
		{
			RptSetup rptSetup = rptSetup_0;
			string_158 = styFile;
			rptSetup.LoadFromFile(styFile);
			method_69(AccStyle.Read, rptSetup_0);
		}
	}

	private string method_11(SSTResult sstresult_0)
	{
		return sstresult_0 switch
		{
			SSTResult.Success => "√", 
			SSTResult.Fail => "X", 
			SSTResult.Unknown => "?", 
			_ => "", 
		};
	}

	private bool method_12(Graphics graphics_0, ref float float_6)
	{
		if (!rptSetup_0.clUse)
		{
			return true;
		}
		if (!bool_2 && !bool_1)
		{
			return true;
		}
		if (!bool_5)
		{
			bool_5 = true;
			int_5 = 0;
			int_16 = 0;
			int_15 = 0;
			int_14 = 0;
			int_13 = 0;
			if (float_6 != float_0 && rptSetup_0.clOnNewPage)
			{
				return false;
			}
		}
		return method_17(graphics_0, ref float_6);
	}

	private bool method_13(Graphics graphics_0, ref float float_6)
	{
		if (rptSetup_0.cgUse && chromatogram_1.Length != 0 && int_8.Length != 0)
		{
			if (!bool_6)
			{
				bool_6 = true;
				int_7 = 0;
				int_19 = 0;
				if (float_6 != float_0 && rptSetup_0.cgOnNewPage)
				{
					return false;
				}
			}
			while (int_7 < int_8.Length)
			{
				float_6 += int_28;
				string text = null;
				int num = chromatogram_1.Length;
				string string_ = ">>>> " + Lang.PS("谱图", "Chrom.") + " <<<<";
				int num2 = int_8[int_7];
				method_8(num2, ref float_6);
				float num3 = 0f;
				float num4 = 0f;
				float num5 = float.MaxValue;
				float num6 = float.MinValue;
				Chromatogram[] chroms = null;
				if (rptSetup_0.cgGraphShowStyle == GraphShowStyle.Combine)
				{
					for (int i = 0; i < chromatogram_1.Length; i++)
					{
						Chromatogram chromatogram = chromatogram_1[i];
						if (rptSetup_0.cgGraphDisStyle == GraphDisStyle.Whole)
						{
							num3 = Math.Min(num3, chromatogram.signal.xMinTime);
							num4 = Math.Max(num4, chromatogram.signal.xMaxTime);
							num5 = Math.Min(num5, chromatogram.signal.yMinValue);
							num6 = Math.Max(num6, chromatogram.signal.yMaxValue);
						}
						text = text + ((i != 0) ? "\n" : "") + method_43(num, i, chromatogram.fullName, chromatogram.fName);
					}
					chroms = chromatogram_1;
				}
				else if (rptSetup_0.cgGraphShowStyle == GraphShowStyle.Separate)
				{
					Chromatogram chromatogram2 = chromatogram_1[num2];
					if (num != 1)
					{
						string[] array = new string[7]
						{
							">>>> [ ",
							(num2 + 1).ToString(),
							" / ",
							num.ToString(),
							" ] ",
							Lang.PS("谱图", "Chrom."),
							" <<<<"
						};
						string_ = string.Concat(array);
					}
					text = method_43(num, num2, chromatogram2.fullName, chromatogram2.fName);
					if (rptSetup_0.cgGraphDisStyle == GraphDisStyle.Whole)
					{
						num3 = chromatogram2.signal.xMinTime;
						num4 = chromatogram2.signal.xMaxTime;
						num5 = chromatogram2.signal.yMinValue;
						num6 = chromatogram2.signal.yMaxValue;
					}
					chroms = new Chromatogram[1] { chromatogram2 };
				}
				method_9(graphics_0, string_, bool_13: true, float_6, out var float_7);
				method_9(graphics_0, text, bool_13: false, float_6, out var float_8);
				float num7 = Math.Max(float_7, float_8);
				if (float_6 + num7 + (float)int_28 + sizeF_1.Height > (float)rectangle_4.Bottom)
				{
					return false;
				}
				if (bool_12)
				{
					float_6 += num7 + (float)int_28 + sizeF_1.Height;
				}
				else
				{
					method_30(graphics_0, string_, text, ref float_6);
					float_6 += int_28;
					if (rptSetup_0.cgGraphDisStyle == GraphDisStyle.Whole)
					{
						chromDisplay_0.CalcuFullDisLg(ref disLg_0, num3, num4, num5, num6);
					}
					else if (rptSetup_0.cgGraphDisStyle == GraphDisStyle.Current)
					{
						disLg_0 = instrument.form.chromForm.CurDisLg;
					}
					chromDisplay_0.stDisChain.AppendFrameLg(disLg_0);
					chromDisplay_0.dskRC.Y = float_6;
					bool erase = rptSetup_0.psColors && rptSetup_0.psDrawGraphsBkgnd;
					int setChromNo = int_2;
					chromDisplay_0.LinkDisChroms(chroms, ref setChromNo);
					chromDisplay_0.Draw(graphics_0, erase);
					float_6 += sizeF_1.Height;
				}
				int_7++;
			}
		}
		return true;
	}

	private bool method_14(Graphics graphics_0, ref float float_6)
	{
		if (rptSetup_0.ciUse && chromatogram_2.Length != 0 && int_10.Length != 0)
		{
			if (!bool_7)
			{
				bool_7 = true;
				int_9 = 0;
				int_20 = 0;
				bool_3 = false;
				int_4 = 0;
				int_3 = 0;
				if (float_6 != float_0 && rptSetup_0.ciOnNewPage)
				{
					return false;
				}
			}
			while (int_20 < chromatogram_2.Length)
			{
				Chromatogram chromatogram = chromatogram_2[int_20];
				while (int_9 < int_10.Length)
				{
					float_6 += int_28;
					string string_ = null;
					string string_2 = null;
					string string_3 = null;
					string string_4 = null;
					while (true)
					{
						int num = int_10[int_9];
						method_8(num, ref float_6);
						int num2 = num;
						if (num2 <= 206)
						{
							switch (num2)
							{
							case 100:
								goto IL_0219;
							case 200:
							case 201:
							case 202:
							case 203:
							case 204:
							case 205:
							case 206:
								goto IL_0482;
							case 0:
							case 1:
							case 2:
							case 3:
							case 4:
							case 5:
							case 6:
							case 7:
								goto IL_04a5;
							}
							goto IL_0202;
						}
						switch (num2)
						{
						default:
						{
							int num3 = num2;
							if (num3 != 500 && num3 != 501)
							{
								goto IL_0202;
							}
							method_5(num, 500, chromatogram.chromInfo, ref string_, ref string_2, ref string_3, ref string_4);
							break;
						}
						case 400:
						case 401:
						case 402:
						case 403:
						case 404:
						case 405:
						case 406:
						case 407:
						case 408:
						case 409:
						case 410:
							method_4(num, 400, chromatogram.chromInfo, ref string_, ref string_2, ref string_3, ref string_4);
							break;
						case 300:
						case 301:
						case 302:
							method_0(num, 300, chromatogram.chromInfo, ref string_, ref string_2, ref string_3, ref string_4);
							break;
						}
						goto IL_03cf;
						IL_0202:
						int_9++;
						continue;
						IL_04a5:
						method_3(num, 0, chromatogram.chromInfo, chromatogram.chromInfoR, ref string_, ref string_2, ref string_3, ref string_4, chromatogram);
						goto IL_03cf;
						IL_0482:
						method_1(num, 200, chromatogram.chromInfo, ref string_, ref string_2, ref string_3, ref string_4);
						goto IL_03cf;
						IL_0219:
						string_ = Lang.PS("积分", "Integration") + ":";
						Integration integ = chromatogram.integ;
						if (gvPrtInfos_0.DimCount != 0 && integ.Count != 0)
						{
							int count = integ.Count;
							while (int_3 < gvPrtInfos_0.PartsNum && int_4 < count)
							{
								string[] array = gvPrtInfos_0.colNames[int_3];
								string[] array2 = new string[array.Length];
								for (int i = 0; i < array.Length; i++)
								{
									if (array[i] == "")
									{
										array2[i] = (int_4 + 1).ToString();
									}
								}
								if (int_3 == 0 && int_4 == 0 && !bool_3 && !method_26(graphics_0, chromatogram_2.Length, int_20, chromatogram, ref float_6, string_, string_2, string_3, string_4, ref bool_3))
								{
									return false;
								}
								if (!method_19(graphics_0, string_, "", gvPrtInfos_0, array2, -1, count, ref int_3, ref int_4, ref float_6))
								{
									return false;
								}
							}
							int_4 = 0;
							int_3 = 0;
							int_9++;
							break;
						}
						string_2 = Lang.PS("[ 空 ]", "[ Null ]");
						goto IL_03cf;
						IL_03cf:
						if (num % 100 == 0 && !bool_3 && !method_26(graphics_0, chromatogram_2.Length, int_20, chromatogram, ref float_6, string_, string_2, string_3, string_4, ref bool_3))
						{
							return false;
						}
						bool flag = false;
						if (string_ != null && string_3 != null)
						{
							flag = method_31(graphics_0, string_, string_2, string_3, string_4, ref float_6);
						}
						else if (string_ != null)
						{
							flag = method_30(graphics_0, string_, string_2, ref float_6);
						}
						if (flag)
						{
							int_9++;
							break;
						}
						return false;
					}
				}
				int_20++;
				bool_3 = false;
				int_9 = 0;
				if (int_20 < chromatogram_2.Length && float_6 != float_0 && rptSetup_0.ciOnNewPage)
				{
					return false;
				}
			}
		}
		return true;
	}

	private bool method_15(Graphics graphics_0, ref float float_6)
	{
		if (rptSetup_0.crUse && chromatogram_3.Length != 0 && int_12.Length != 0)
		{
			if (!bool_8)
			{
				bool_8 = true;
				int_11 = 0;
				int_21 = 0;
				bool_3 = false;
				int_50 = 0;
				int_49 = 0;
				int_55 = 0;
				int_54 = 0;
				int_53 = 0;
				int_44 = 0;
				int_43 = 0;
				int_41 = 0;
				int_40 = 0;
				if (float_6 != float_0 && rptSetup_0.crOnNewPage)
				{
					return false;
				}
			}
			while (int_21 < chromatogram_3.Length)
			{
				Chromatogram chromatogram = chromatogram_3[int_21];
				while (int_11 < int_12.Length)
				{
					float_6 += int_28;
					string string_ = null;
					string text = null;
					string string_2 = null;
					int num = int_12[int_11];
					method_8(num, ref float_6);
					int num2 = num;
					string text2;
					if (num2 <= 100)
					{
						if (num2 != 0)
						{
							if (num2 != 100)
							{
								goto IL_0f76;
							}
							if (int_21 != 0)
							{
								int_11++;
								continue;
							}
							text2 = Lang.PS("组分验证:", "SST:");
							SSTCmpd[] chkCmpds = sst_0.ChkCmpds;
							if (chkCmpds.Length != 0 && gvPrtInfos_9.DimCount != 0)
							{
								int num3 = 6 + chromatogram_3.Length;
								while (int_53 < chkCmpds.Length)
								{
									string_ = "[ " + method_11(chkCmpds[int_53].sstResult) + " ]" + chkCmpds[int_53].RT.ToString("[ 0.000min ]: ") + chkCmpds[int_53].name;
									while (int_54 < gvPrtInfos_9.PartsNum && int_55 < num3)
									{
										string[] array = gvPrtInfos_9.colNames[int_54];
										string[] array2 = new string[array.Length];
										method_71(array.Length);
										for (int i = 0; i < array.Length; i++)
										{
											int num4 = int_55 - 6;
											Chromatogram chromatogram2 = ((num4 >= 0) ? chromatogram_3[num4] : null);
											if (int_55 >= 6 && array[i] == "")
											{
												array2[i] = (num4 + 1).ToString();
											}
											string text3 = array[i];
											SstItem item = chkCmpds[int_53].GetItem(text3);
											if (text3 == "Chrom")
											{
												if (int_55 == 0)
												{
													array2[i] = Lang.PS("上限", "Upper Limit");
												}
												else if (int_55 == 1)
												{
													array2[i] = Lang.PS("下限", "Lower Limit");
												}
												else if (int_55 == 2)
												{
													array2[i] = Lang.PS("RSD% 要求", "RSD% Limit");
												}
												else if (int_55 == 3)
												{
													array2[i] = Lang.PS("均值", "Average");
												}
												else if (int_55 == 4)
												{
													array2[i] = "RSD[%]";
												}
												else if (int_55 == 5)
												{
													array2[i] = Lang.PS("参数结果", "Parameter Result");
												}
												else
												{
													array2[i] = chromatogram2.fName;
												}
											}
											else if (int_55 <= 5 && item != null)
											{
												array2[i] = instrument.form.chromForm.RetSstItem(item, int_55);
												if (int_55 == 3)
												{
													color_0[i] = ChromForm.RetSstMeanClr(item);
												}
												if (int_55 == 4)
												{
													color_0[i] = ChromForm.RetSstRsdPerClr(item);
												}
												if (int_55 == 5)
												{
													array2[i] = method_11(item.result);
												}
											}
											else
											{
												if (int_55 <= 5)
												{
													continue;
												}
												if (array[i] == "X")
												{
													SSTResult sstresult_ = chkCmpds[int_53].extResult(chromatogram2, sst_0.sstParas.criterion);
													array2[i] = method_11(sstresult_);
												}
												else
												{
													if (!(text3 != ""))
													{
														continue;
													}
													for (int j = 0; j < chromatogram2.RltPeaks.Length; j++)
													{
														if (chromatogram2.RltPeaks[j].name == chkCmpds[int_53].name)
														{
															Peak peak = chromatogram2.RltPeaks[j];
															array2[i] = instrument.form.chromForm.RetSstItem(peak, text3);
															float value = SstItem.getValue(peak, text3, sst_0.sstParas.criterion);
															if (!float.IsNaN(value) && item != null)
															{
																color_0[i] = ((item.extResult(value) == SSTResult.Success) ? Color.Black : Color.Red);
															}
															break;
														}
													}
												}
											}
										}
										if (int_55 == 6)
										{
											method_27(graphics_0, bool_13: false, gvPrtInfos_9.float_0[int_54], gvPrtInfos_9.float_1[int_54], ref float_6);
										}
										if (!method_19(graphics_0, text2, string_, gvPrtInfos_9, array2, -1, num3, ref int_54, ref int_55, ref float_6))
										{
											return false;
										}
									}
									int_55 = 0;
									int_54 = 0;
									int_53++;
								}
								int_11++;
								continue;
							}
							string_ = Lang.PS("[ 空 ]", "[ Null ]");
						}
						else
						{
							if (int_21 != 0)
							{
								int_11++;
								continue;
							}
							text2 = Lang.PS("总结表:", "Summary:");
							if (gvPrtInfos_10.DimCount != 0)
							{
								int num5 = chromatogram_3.Length;
								while (int_49 < gvPrtInfos_10.PartsNum && int_50 < num5)
								{
									string[] array3 = gvPrtInfos_10.colNames[int_49];
									string[] array4 = new string[array3.Length];
									Chromatogram chrom = chromatogram_3[int_50];
									for (int k = 0; k < array3.Length; k++)
									{
										if (array3[k] == "")
										{
											array4[k] = (int_50 + 1).ToString();
										}
										else if (array3[k].Contains("\0"))
										{
											string[] array5 = array3[k].Split(default(char));
											array4[k] = instrument.form.chromForm.chromDataGrid.gvSummarySmyValue(chrom, array5[1], array5[0]);
										}
										else
										{
											array4[k] = instrument.form.chromForm.chromDataGrid.gvSummaryComValue(chrom, array3[k]);
										}
									}
									if (!method_19(graphics_0, text2, "", gvPrtInfos_10, array4, -1, num5, ref int_49, ref int_50, ref float_6))
									{
										return false;
									}
								}
								int_11++;
								continue;
							}
							string_ = Lang.PS("[ 空 ]", "[ Null ]");
						}
					}
					else if (num2 != 200)
					{
						if (num2 != 300)
						{
							goto IL_0f76;
						}
						text2 = cbcrPerformance.Text + ":";
						if (gvPrtInfos_6.DimCount != 0)
						{
							Peak[] rltPeaks = chromatogram.RltPeaks;
							int num6 = rltPeaks.Length;
							while (int_40 < gvPrtInfos_6.PartsNum && int_41 < num6)
							{
								string[] array6 = gvPrtInfos_6.colNames[int_40];
								string[] array7 = new string[array6.Length];
								Peak peak2 = rltPeaks[int_41];
								for (int l = 0; l < array6.Length; l++)
								{
									if (array6[l] == "")
									{
										array7[l] = (int_41 + 1).ToString();
									}
									else
									{
										array7[l] = instrument.form.chromForm.chromDataGrid.gvPerformFrom50Value(peak2, array6[l]);
									}
								}
								if (int_40 == 0 && int_41 == 0 && !bool_3 && !method_26(graphics_0, chromatogram_3.Length, int_21, chromatogram, ref float_6, text2, string_, text, string_2, ref bool_3))
								{
									return false;
								}
								if (!method_19(graphics_0, text2, "", gvPrtInfos_6, array7, -1, num6, ref int_40, ref int_41, ref float_6))
								{
									return false;
								}
							}
							int_41 = 0;
							int_40 = 0;
							int_11++;
							continue;
						}
						string_ = Lang.PS("[ 空 ]", "[ Null ]");
					}
					else
					{
						text2 = cbcrResult.Text + ":";
						if (gvPrtInfos_7.DimCount == 0)
						{
							string_ = Lang.PS("[ 空 ]", "[ Null ]");
						}
						else
						{
							if (rptSetup_0.crRltCombine)
							{
								if (int_21 != 0)
								{
									int_11++;
									continue;
								}
								string_ = Lang.PS("整合: ", "Combine: ");
								for (int m = 0; m < chromatogram_3.Length; m++)
								{
									Chromatogram chromatogram3 = chromatogram_3[m];
									if (chromatogram3 != chromatogram)
									{
										string_ = string_ + chromatogram3.fName + ", ";
									}
								}
								if (string_.EndsWith(", "))
								{
									string_ = string_.Remove(string_.Length - 2);
								}
							}
							else
							{
								string_ = "";
							}
							bool crRltCombine = rptSetup_0.crRltCombine;
							float whlArea;
							float whlHeight;
							float whlAmount;
							float whlAreaPer;
							float whlHeightPer;
							float whlAmountPer;
							Peak[] array8 = Chromatogram.GetRltPeaks(chromatogram_3, chromatogram, crRltCombine, out whlArea, out whlHeight, out whlAmount, out whlAreaPer, out whlHeightPer, out whlAmountPer);
							int num7 = array8.Length;
							if (num7 != 0)
							{
								Array.Resize(ref array8, num7 + 1);
								Peak peak3 = (array8[num7] = new Peak());
								peak3.area = whlArea;
								peak3.areaPer = (peak3._areaPer = whlAreaPer);
								peak3.height = whlHeight;
								peak3.heightPer = (peak3._heightPer = whlHeightPer);
								peak3.amount = whlAmount;
								peak3.amountPer = (peak3._amountPer = whlAmountPer);
								int num8 = array8.Length;
								while (int_43 < gvPrtInfos_7.PartsNum && int_44 < num8)
								{
									string[] array9 = gvPrtInfos_7.colNames[int_43];
									string[] array10 = new string[array9.Length];
									peak3 = array8[int_44];
									for (int n = 0; n < array9.Length; n++)
									{
										if (array9[n] == "")
										{
											array10[n] = ((int_44 < num8 - 1) ? (int_44 + 1).ToString() : Lang.PS("总计", "Total"));
										}
										else
										{
											array10[n] = instrument.form.chromForm.chromDataGrid.gvRltsValue(peak3, array9[n], "", crRltCombine);
										}
									}
									if (int_43 == 0 && int_44 == 0 && !bool_3 && !method_26(graphics_0, chromatogram_3.Length, int_21, chromatogram, ref float_6, text2, string_, text, string_2, ref bool_3))
									{
										return false;
									}
									if (!method_19(graphics_0, text2, (int_43 == 0) ? string_ : "", gvPrtInfos_7, array10, -1, num8, ref int_43, ref int_44, ref float_6))
									{
										return false;
									}
								}
								int_44 = 0;
								int_43 = 0;
								int_11++;
								continue;
							}
							string_ = Lang.PS("[ 空 ]", "[ Null ]");
						}
					}
					goto IL_0ec6;
					IL_0f76:
					string_ = (text2 = "*");
					goto IL_0ec6;
					IL_0ec6:
					if (num % 100 == 0 && !bool_3 && !method_26(graphics_0, chromatogram_3.Length, int_21, chromatogram, ref float_6, text2, string_, text, string_2, ref bool_3))
					{
						return false;
					}
					bool flag = false;
					if (text2 != null && text != null)
					{
						flag = method_31(graphics_0, text2, string_, text, string_2, ref float_6);
					}
					else if (text2 != null)
					{
						flag = method_30(graphics_0, text2, string_, ref float_6);
					}
					if (flag)
					{
						int_11++;
						continue;
					}
					return false;
				}
				int_21++;
				bool_3 = false;
				int_11 = 0;
				if (int_21 < chromatogram_3.Length && float_6 != float_0 && rptSetup_0.crOnNewPage)
				{
					return false;
				}
			}
		}
		return true;
	}

	private bool method_16(Graphics graphics_0, ref float float_6)
	{
		return true;
	}

	private bool method_17(Graphics graphics_0, ref float float_6)
	{
		while (int_5 < int_6.Length)
		{
			float_6 += int_28;
			string text = null;
			string string_ = null;
			CaliGnl caliGnl = null;
			CaliGnlOpt caliGnlOpt = null;
			while (true)
			{
				int num = int_6[int_5];
				method_8(num, ref float_6);
				string text2;
				string string_2;
				bool flag;
				switch (num)
				{
				default:
				{
					int num2;
					Compound compound;
					if (1000 > num || num >= 2000)
					{
						if (2000 <= num && num < 3000)
						{
							num2 = num - 2000;
							compound = caliGnl.cmpds[num2];
							string string_3 = "[ " + (num2 + 1) + " / " + caliGnl.cmpds.Length + " ]." + compound.cmpdInfo.name + " " + Lang.PS("方程", "Function") + ":";
							CmpdFunc cmpdFunc = ((caliGnlOpt.caliDisMode == CaliDisMode.Estd) ? compound.eFunc : compound.iFunc);
							string equationStr = cmpdFunc.GetEquationStr();
							method_9(graphics_0, string_3, bool_13: true, 0f, out var float_7);
							method_9(graphics_0, equationStr, bool_13: false, 0f, out var float_8);
							float num3 = Math.Max(float_7, float_8);
							string string_4 = Class49.MesureUnit() + ".s";
							if (compound.cmpdInfo.respStyle == RespStyle.Height)
							{
								string_4 = Class49.MesureUnit();
							}
							class8_0.SetCompound(compound, caliGnlOpt.caliDisMode == CaliDisMode.Istd, caliGnlOpt.cmpdUnit, ref string_4);
							text2 = Lang.PS("相关系数:", "Co. Factor:") + ":";
							string_2 = cmpdFunc.GetCorrFactorTxt();
							text = Lang.PS("残余", "Residuum") + ":";
							string_ = cmpdFunc.GetResiduumTxt(string_4);
							method_10(graphics_0, text2, bool_13: true, 0f, out var float_9);
							method_10(graphics_0, string_2, bool_13: false, 0f, out var float_10);
							method_10(graphics_0, text, bool_13: true, 0f, out var float_11);
							method_10(graphics_0, string_, bool_13: false, 0f, out var float_12);
							float val = Math.Max(float_9, float_10);
							float val2 = Math.Max(float_11, float_12);
							float num4 = Math.Max(val, val2);
							if (float_6 + num3 + (float)int_28 + num4 + (float)int_28 + sizeF_0.Height <= (float)rectangle_4.Bottom)
							{
								if (bool_12)
								{
									float_6 += num3 + (float)int_28 + num4 + (float)int_28 + sizeF_0.Height;
								}
								else
								{
									method_30(graphics_0, string_3, equationStr, ref float_6);
									float_6 += int_28;
									method_31(graphics_0, text2, string_2, text, string_, ref float_6);
									float_6 += int_28;
									chromDisplay_0.dskRC.Y = float_6;
									bool erase = rptSetup_0.psColors && rptSetup_0.psDrawGraphsBkgnd;
									class8_0.dskRC.Y = float_6;
									class8_0.Draw(graphics_0, erase);
									float_6 += sizeF_0.Height;
								}
								int_5++;
								break;
							}
							return false;
						}
						goto IL_00b1;
					}
					num2 = num - 1000;
					compound = caliGnl.cmpds[num2];
					text2 = "[ " + (num2 + 1) + " / " + caliGnl.cmpds.Length + " ]." + compound.cmpdInfo.name + " " + cbclcdGnlLevels.Text + ":";
					if (gvPrtInfos_1.DimCount != 0 && compound.LastLevelNo >= 0)
					{
						int num5 = compound.LastLevelNo + 1;
						while (int_13 < gvPrtInfos_1.PartsNum && int_14 < num5)
						{
							string[] array = gvPrtInfos_1.colNames[int_13];
							for (int i = 0; i < array.Length; i++)
							{
								if (!(array[i] == "Resp"))
								{
									continue;
								}
								string string_4 = Class49.MesureUnit() + ".s";
								if (compound.cmpdInfo.respStyle == RespStyle.Height)
								{
									string_4 = Class49.MesureUnit();
								}
								gvPrtInfos_1.colHdrTxts[int_13][i] = Lang.PS("响应", "Response") + "\n[" + string_4 + "]";
								string[] array2 = new string[array.Length];
								Level level = compound.levels[int_14];
								for (i = 0; i < array.Length; i++)
								{
									if (array[i] == "")
									{
										array2[i] = (int_14 + 1).ToString();
									}
									else
									{
										array2[i] = instrument.form.caliGnlForm.gvCmpdValue(gvUse: false, level, array[i]).ToString();
									}
								}
								if (method_19(graphics_0, text2, "", gvPrtInfos_1, array2, -1, num5, ref int_13, ref int_14, ref float_6))
								{
									break;
								}
								return false;
							}
						}
						int_5++;
						int_14 = 0;
						int_13 = 0;
						break;
					}
					string_2 = Lang.PS("[ 空 ]", "[ Null ]");
					goto IL_08b9;
				}
				case 100:
					text2 = cbclCmpds.Text + ":";
					if (gvPrtInfos_2.DimCount != 0 && caliGnl.cmpds.Length != 0)
					{
						int num6 = caliGnl.cmpds.Length;
						while (int_15 < gvPrtInfos_2.PartsNum && int_16 < num6)
						{
							string[] array3 = gvPrtInfos_2.colNames[int_15];
							string[] array4 = new string[array3.Length];
							Compound cmpd = caliGnl.cmpds[int_16];
							int int_ = -1;
							for (int j = 0; j < array3.Length; j++)
							{
								if (array3[j] == "")
								{
									array4[j] = (int_16 + 1).ToString();
									continue;
								}
								string text3 = array3[j];
								if (text3 != null && text3 == "PeakColor")
								{
									array4[j] = ((Color)instrument.form.caliGnlForm.gvCmpdsValue(gvUse: false, cmpd, array3[j])).ToArgb().ToString();
									int_ = j;
								}
								else
								{
									array4[j] = instrument.form.caliGnlForm.gvCmpdsValue(gvUse: false, cmpd, array3[j]).ToString();
								}
							}
							if (!method_19(graphics_0, text2, "", gvPrtInfos_2, array4, int_, num6, ref int_15, ref int_16, ref float_6))
							{
								return false;
							}
						}
						int_5++;
						break;
					}
					string_2 = Lang.PS("[ 空 ]", "[ Null ]");
					goto IL_08b9;
				case 0:
					text2 = CaliGnlOptDlg.sDescription + ":";
					string_2 = caliGnlOpt.description;
					goto IL_08b9;
				case 1:
					text2 = CaliGnlOptDlg.sDisplayMode + ":";
					string_2 = ((caliGnlOpt.caliDisMode == CaliDisMode.Estd) ? Lang.PS("外标模式", "ESTD Style") : Lang.PS("内标模式", "ISTD Style"));
					text = CaliGnlOptDlg.sCmpdUnits + ":";
					string_ = caliGnlOpt.cmpdUnit;
					goto IL_08b9;
				case 2:
					{
						text2 = Lang.PS("再校正", "Recalibration") + ":";
						string_2 = ((caliGnlOpt.recaliMode == RecaliMode.Average) ? Lang.PS("平均", "Average") : Lang.PS("替换", "Replace"));
						text = Lang.PS("刷新保留时间", "Update Retain Time") + ":";
						string_ = (caliGnlOpt.updateRT ? Lang.PS("是", "True") : Lang.PS("非", "False"));
						goto IL_08b9;
					}
					IL_08b9:
					flag = false;
					if (text2 != null && text != null)
					{
						flag = method_31(graphics_0, text2, string_2, text, string_, ref float_6);
					}
					else if (text2 != null)
					{
						flag = method_30(graphics_0, text2, string_2, ref float_6);
					}
					if (flag)
					{
						int_5++;
						break;
					}
					return false;
				}
				break;
				IL_00b1:
				int_5++;
				if (int_5 >= int_6.Length)
				{
					return true;
				}
			}
		}
		return true;
	}

	private bool method_18(Graphics graphics_0, ref float float_6)
	{
		return true;
	}

	private bool method_19(Graphics graphics_0, string string_159, string string_160, GvPrtInfos gvPrtInfos_11, string[] string_161, int int_58, int int_59, ref int int_60, ref int int_61, ref float float_6)
	{
		int partsNum = gvPrtInfos_11.PartsNum;
		bool flag = int_61 == int_59 - 1;
		string[] array = gvPrtInfos_11.colHdrTxts[int_60];
		bool flag2 = false;
		int num = 0;
		while (num < array.Length)
		{
			float num2;
			float[] array2;
			float num3;
			float float_7;
			string[] array3;
			string text;
			float num4;
			float num5;
			float num7;
			if (array[num].Contains("\n"))
			{
				flag2 = true;
				num2 = (flag2 ? float_2 : float_1);
				array2 = gvPrtInfos_11.colWidths[int_60];
				num3 = gvPrtInfos_11.float_0[int_60];
				float_7 = gvPrtInfos_11.float_1[int_60];
				array3 = gvPrtInfos_11.colNames[int_60];
				if (int_61 == 0)
				{
					text = "";
					if (partsNum != 1)
					{
						string[] array4 = new string[5]
						{
							" <",
							(int_60 + 1).ToString(),
							" / ",
							partsNum.ToString(),
							">"
						};
						text = string.Concat(array4);
					}
					float float_8 = 0f;
					float float_9 = 0f;
					method_9(graphics_0, string_159 + text, bool_13: true, float_6, out float_8);
					method_9(graphics_0, string_160, bool_13: false, float_6, out float_9);
					num4 = Math.Max(float_8, float_9);
					num5 = 0f;
					num = 0;
					while (num < array3.Length)
					{
						if (!array3[num].Contains("\0"))
						{
							num++;
							continue;
						}
						goto IL_0168;
					}
				}
				if (float_6 == (float)rectangle_4.Top || float_6 == float_0 + 10f)
				{
					method_27(graphics_0, bool_13: true, num3, float_7, ref float_6);
					float num6 = num3;
					for (num = 0; num < array.Length; num++)
					{
						method_28(graphics_0, array[num], num6, float_6, array2[num], num2);
						num6 += array2[num];
					}
					float_6 += num2;
					method_27(graphics_0, bool_13: false, num3, float_7, ref float_6);
				}
				num7 = float_6 + float_5;
				if (flag)
				{
					num7 += 4f;
				}
				if (num7 > (float)rectangle_4.Bottom)
				{
					return false;
				}
				method_35(graphics_0, gvPrtInfos_11, string_161, int_58, int_60, int_61, num3, ref float_6);
				goto IL_0604;
			}
			num++;
			continue;
			IL_0604:
			if (flag)
			{
				method_27(graphics_0, bool_13: true, num3, float_7, ref float_6);
				if (int_60 < partsNum - 1)
				{
					float_6 += int_28;
				}
				int_60++;
				int_61 = 0;
			}
			else
			{
				int_61++;
			}
			return true;
			IL_0168:
			bool flag3 = false;
			for (int i = 0; i < array3.Length; i++)
			{
				if (!array3[i].Contains("\0"))
				{
					continue;
				}
				string[] array5 = array3[i].Split(default(char));
				if (!instrument.form.chromForm.chromDataGrid.getSmyHeaderText(array5[0]).Contains("\n"))
				{
					continue;
				}
				flag3 = true;
				if (flag2)
				{
					if (flag3)
					{
						num5 = float_1;
					}
				}
				else
				{
					num5 = (flag3 ? float_2 : float_1);
				}
				num5 += 3f;
				break;
			}
			num7 = float_6 + num4 + (float)int_28 + 4f + num2 + num5 + 4f + float_5;
			if (flag)
			{
				num7 += 2f;
			}
			if (num7 > (float)rectangle_4.Bottom)
			{
				return false;
			}
			method_30(graphics_0, string_159 + text, string_160, ref float_6);
			method_27(graphics_0, bool_13: true, num3, float_7, ref float_6);
			float num8 = num3;
			string text2 = "";
			float num9 = 0f;
			for (num = 0; num < array.Length; num++)
			{
				if (num5 == 0f)
				{
					method_28(graphics_0, array[num], num8, float_6, array2[num], num2);
				}
				else
				{
					string text3 = array3[num];
					if (!text3.Contains("\0"))
					{
						method_28(graphics_0, array[num], num8, float_6, array2[num], num2 + num5);
					}
					else
					{
						float num10 = 0f;
						float num11 = float_6;
						float float_10 = 0f;
						string text4 = "";
						if (smyHdrPara_0 == SmyHdrPara.Cmpd_Para)
						{
							text4 = text3.Split(default(char))[1];
							num10 = float_1;
							num11 += num10;
							float_10 = num2 + num5 - float_1 - 3f;
						}
						if (smyHdrPara_0 == SmyHdrPara.Para_Cmpd)
						{
							text4 = text3.Split(default(char))[0];
							text4 = instrument.form.chromForm.chromDataGrid.getSmyHeaderText(text4);
							num10 = num2 + num5 - float_1 - 3f;
							num11 += num10;
							float_10 = float_1;
						}
						if (text2 == "")
						{
							text2 = text4;
							num9 = num8;
						}
						if (text4 != text2)
						{
							method_24(graphics_0, text2, num9, float_6, num8 - num9, num10);
							text2 = text4;
							num9 = num8;
						}
						if (num == array.Length - 1)
						{
							method_24(graphics_0, text2, num9, float_6, num8 - num9 + array2[num], num10);
						}
						method_33(graphics_0, num8, num11 + 1f, gvPrtInfos_11.colWidths[int_60][num], 1);
						method_28(graphics_0, array[num], num8, num11 + 3f, array2[num], float_10);
					}
				}
				num8 += array2[num];
			}
			float_6 += num2;
			float_6 += num5;
			method_27(graphics_0, bool_13: false, num3, float_7, ref float_6);
			method_35(graphics_0, gvPrtInfos_11, string_161, int_58, int_60, int_61, num3, ref float_6);
			goto IL_0604;
		}
		return false;
	}

	private void method_20(Graphics graphics_0)
	{
		method_37(graphics_0, "@@@", bool_13: false, rectangle_4.Left, float_4);
		string string_ = int_17 + " / " + int_42;
		SizeF sizeF = graphics_0.MeasureString(string_, rptSetup_0.psValueFont);
		method_37(graphics_0, string_, bool_13: false, (float)rectangle_4.Right - sizeF.Width, float_4);
		if (!rptSetup_0.psSign || instrument == null)
		{
			return;
		}
		User user = instrument.user;
		method_37(graphics_0, user.personInfo, bool_13: false, rectangle_4.Left, float_3);
		if (!(user.signGraph != "") || !File.Exists(user.signGraph))
		{
			return;
		}
		rectangle_5.Size = UserAccountsDlg.defaultSignGraph;
		rectangle_5.X = rectangle_4.Right - rectangle_5.Width;
		rectangle_5.Y = Convert.ToInt32(float_3);
		Image image = Image.FromFile(user.signGraph);
		switch (user.sgSizeMode)
		{
		case PictureBoxSizeMode.Normal:
			rectangle_5.Size = image.Size;
			break;
		case PictureBoxSizeMode.AutoSize:
			rectangle_5.Size = image.Size;
			rectangle_5.X = rectangle_4.Right - rectangle_5.Width;
			break;
		case PictureBoxSizeMode.CenterImage:
			rectangle_5.X += (UserAccountsDlg.defaultSignGraph.Width - image.Width) / 2;
			rectangle_5.Y += (UserAccountsDlg.defaultSignGraph.Height - image.Height) / 2;
			rectangle_5.Size = image.Size;
			break;
		case PictureBoxSizeMode.Zoom:
		{
			float num = rectangle_5.Width;
			num /= (float)rectangle_5.Height;
			float num2 = image.Width;
			num2 /= (float)image.Height;
			if (num2 > num)
			{
				rectangle_5.Height = Convert.ToInt32((float)rectangle_5.Width / num2);
			}
			else
			{
				rectangle_5.Width = Convert.ToInt32(num2 * (float)rectangle_5.Height);
			}
			break;
		}
		}
		method_29(graphics_0, image, ref bitmap_2, rectangle_5);
	}

	private void method_21(Graphics graphics_0, ref float float_6)
	{
		if (rptSetup_0.lhUse && (!rptSetup_0.lhJstFstPage || (rptSetup_0.lhJstFstPage && ((bool_12 && int_42 == 0) || (!bool_12 && int_17 == 1)))) && int_31 <= int_32)
		{
			rectangle_0.X = int_22;
			rectangle_0.Width = int_23 - int_22;
			if (rptSetup_0.lhGrayBkgnd)
			{
				method_42(graphics_0, Color.LightGray, rectangle_0);
			}
			if (rptSetup_0.lhBorder)
			{
				method_34(graphics_0, Color.Black, rectangle_0);
			}
			for (int i = 0; i < rptSetup_0.LinesNum; i++)
			{
				rectangle_1[i].X = rectangle_0.Left + 5;
				rectangle_1[i].Width = rectangle_0.Width - 10;
				method_38(graphics_0, rptSetup_0.lhLines[i], rptSetup_0.lhLinesFt[i], _clr(rptSetup_0.psColors, rptSetup_0.lhLinesClr[i]), rectangle_1[i], rptSetup_0.lhLinesAlign[i]);
			}
			method_29(graphics_0, image_0, ref bitmap_0, rectangle_2);
			method_29(graphics_0, image_1, ref bitmap_1, rectangle_3);
			float_6 += int_31;
		}
	}

	private bool method_22(Graphics graphics_0, ref float float_6)
	{
		if (rptSetup_0.mtdUse && int_35.Length != 0)
		{
			if (!bool_9)
			{
				bool_9 = true;
				int_34 = 0;
				int_27 = 0;
				int_26 = 0;
				int_25 = 0;
				int_24 = 0;
				int_38 = 0;
				int_37 = 0;
				if (float_6 != float_0 && rptSetup_0.mtdOnNewPage)
				{
					return false;
				}
			}
			while (int_34 < int_35.Length)
			{
				float_6 += int_28;
				string string_ = null;
				string string_2 = null;
				string string_3 = null;
				string string_4 = null;
				while (true)
				{
					int num = int_35[int_34];
					method_8(num, ref float_6);
					int num2 = num;
					if (num2 <= 302)
					{
						if (num2 <= 60)
						{
							int num3 = num2;
							if (num3 != 0)
							{
								if (num3 != 1)
								{
									if (num2 != 50)
									{
										if (num2 != 60)
										{
											goto IL_0338;
										}
										string_ = Lang.PS("程序升温图像:", "Program Temp. Graph");
										if (gvPrtInfos_4.DimCount != 0 && instrument.methodSetup.chromInfoR.GcProgTemp.progTempRows.Length != 0)
										{
											if (float_6 + float_1 + (float)int_28 + sizeF_2.Height <= (float)rectangle_4.Bottom)
											{
												if (bool_12)
												{
													float_6 += float_1 + (float)int_28 + sizeF_2.Height;
												}
												else
												{
													method_37(graphics_0, string_, bool_13: true, rectangle_4.Left, float_6);
													float_6 += float_1;
													gradientDisplay_1.dskRC.Y = float_6 + (float)int_28;
													bool erase = rptSetup_0.psColors && rptSetup_0.psDrawGraphsBkgnd;
													gradientDisplay_1.Draw(graphics_0, erase);
													float_6 += (float)int_28 + sizeF_2.Height;
												}
												int_34++;
												break;
											}
											return false;
										}
										string_2 = Lang.PS("[ 空 ]", "[ Null ]");
									}
									else
									{
										string_ = instrument.form.dlgMethodSetup.Text + Lang.PS(".程序升温:", ".Prog. Temp.");
										if (gvPrtInfos_4.DimCount != 0 && instrument.methodSetup.chromInfoR.GcProgTemp.progTempRows.Length != 0)
										{
											int num4 = instrument.methodSetup.chromInfoR.GcProgTemp.progTempRows.Length;
											while (int_26 < gvPrtInfos_4.PartsNum && int_27 < num4)
											{
												string[] array = gvPrtInfos_4.colNames[int_26];
												string[] array2 = new string[array.Length];
												ProgTRow progTempRow = instrument.methodSetup.chromInfoR.GcProgTemp.progTempRows[int_27];
												for (int i = 0; i < array.Length; i++)
												{
													if (array[i] == "")
													{
														array2[i] = (int_27 + 1).ToString();
													}
													else
													{
														array2[i] = instrument.form.dlgMethodSetup.gvProgTempValue(progTempRow, array[i]).ToString();
													}
												}
												if (!method_19(graphics_0, string_, "", gvPrtInfos_4, array2, -1, num4, ref int_26, ref int_27, ref float_6))
												{
													return false;
												}
											}
											int_34++;
											break;
										}
										string_2 = Lang.PS("[ 空 ]", "[ Null ]");
									}
								}
								else
								{
									string_ = "mtdAS 2";
									string_2 = "222";
									string_3 = "mtdAS 2 *";
									string_4 = "***222***";
								}
							}
							else
							{
								string_ = "mtdAS 1";
								string_2 = "111";
								string_3 = "mtdAS 1 *";
								string_4 = "***111***";
							}
						}
						else if (num2 != 100)
						{
							if (num2 != 200)
							{
								if (num2 != 302)
								{
									goto IL_0338;
								}
								string_ = Lang.PS("空闲状态", "Idle State") + ":";
								string_2 = Lang.PS("监控设置", "Monitor Set");
								if (instrument.methodSetup.chromInfoR.LcGradient.idleStateProc == IdleStateProc.Initial)
								{
									string_2 = Lang.PS("初始", "Initial");
								}
								if (instrument.methodSetup.chromInfoR.LcGradient.idleStateProc == IdleStateProc.PumpOff)
								{
									string_2 = Lang.PS("关泵", "Pump Off");
								}
							}
							else
							{
								string_ = cbmtdLcGrdtGraph.Text + ":";
								if (gvPrtInfos_5.DimCount != 0 && instrument.methodSetup.chromInfoR.LcGradient.gradientRows.Length != 0)
								{
									if (float_6 + float_1 + (float)int_28 + sizeF_2.Height <= (float)rectangle_4.Bottom)
									{
										if (bool_12)
										{
											float_6 += float_1 + (float)int_28 + sizeF_2.Height;
										}
										else
										{
											method_37(graphics_0, string_, bool_13: true, rectangle_4.Left, float_6);
											float_6 += float_1;
											gradientDisplay_0.dskRC.Y = float_6 + (float)int_28;
											bool erase2 = rptSetup_0.psColors && rptSetup_0.psDrawGraphsBkgnd;
											gradientDisplay_0.Draw(graphics_0, erase2);
											float_6 += (float)int_28 + sizeF_2.Height;
										}
										int_34++;
										break;
									}
									return false;
								}
								string_2 = Lang.PS("[ 空 ]", "[ Null ]");
							}
						}
						else
						{
							string_ = instrument.form.dlgMethodSetup.Text + "." + Lang.PS("梯度表", "Gradient Table") + ":";
							if (gvPrtInfos_5.DimCount != 0 && instrument.methodSetup.chromInfoR.LcGradient.gradientRows.Length != 0)
							{
								int num5 = instrument.methodSetup.chromInfoR.LcGradient.gradientRows.Length;
								while (int_24 < gvPrtInfos_5.PartsNum && int_25 < num5)
								{
									string[] array3 = gvPrtInfos_5.colNames[int_24];
									string[] array4 = new string[array3.Length];
									GradientRow gradientRow_ = instrument.methodSetup.chromInfoR.LcGradient.gradientRows[int_25];
									for (int j = 0; j < array3.Length; j++)
									{
										if (array3[j] == "")
										{
											array4[j] = (int_25 + 1).ToString();
										}
										else
										{
											array4[j] = instrument.form.dlgMethodSetup.gvGrdtValue(gradientRow_, array3[j]).ToString();
										}
									}
									if (!method_19(graphics_0, string_, "", gvPrtInfos_5, array4, -1, num5, ref int_24, ref int_25, ref float_6))
									{
										return false;
									}
								}
								int_34++;
								break;
							}
							string_2 = Lang.PS("[ 空 ]", "[ Null ]");
						}
					}
					else
					{
						if (num2 <= 606)
						{
							switch (num2)
							{
							case 500:
							case 501:
							case 502:
							case 503:
							case 504:
							case 505:
							case 506:
							case 507:
							case 508:
							case 509:
							case 510:
							case 511:
								goto IL_0a1b;
							case 600:
							case 601:
							case 602:
							case 603:
							case 604:
							case 605:
							case 606:
								goto IL_0dc5;
							case 400:
							case 401:
							case 402:
							case 403:
							case 404:
							case 405:
							case 406:
							case 407:
							case 408:
							case 409:
							case 410:
							case 411:
								goto IL_0e01;
							}
							goto IL_0338;
						}
						if (num2 <= 802)
						{
							switch (num2)
							{
							default:
								goto IL_0338;
							case 800:
							case 801:
							case 802:
								method_0(num, 800, instrument.methodSetup.chromInfo, ref string_, ref string_2, ref string_3, ref string_4);
								break;
							case 700:
							case 701:
							case 702:
							case 703:
							case 704:
							case 705:
							case 706:
								method_1(num, 700, instrument.methodSetup.chromInfo, ref string_, ref string_2, ref string_3, ref string_4);
								break;
							}
						}
						else
						{
							switch (num2)
							{
							default:
							{
								int num6 = num2;
								if (num6 != 1000 && num6 != 1001)
								{
									goto IL_0338;
								}
								method_5(num, 1000, instrument.methodSetup.chromInfo, ref string_, ref string_2, ref string_3, ref string_4);
								break;
							}
							case 900:
							case 901:
							case 902:
							case 903:
							case 904:
							case 905:
							case 906:
							case 907:
							case 908:
							case 909:
							case 910:
								method_4(num, 900, instrument.methodSetup.chromInfo, ref string_, ref string_2, ref string_3, ref string_4);
								break;
							}
						}
					}
					goto IL_0c55;
					IL_0c55:
					bool flag = false;
					if (string_ != null && string_3 != null)
					{
						flag = method_31(graphics_0, string_, string_2, string_3, string_4, ref float_6);
					}
					else if (string_ != null)
					{
						flag = method_30(graphics_0, string_, string_2, ref float_6);
					}
					if (flag)
					{
						int_34++;
						break;
					}
					return false;
					IL_0e01:
					int num7 = num - 400;
					string text = "[ " + (num7 + 1) + " / " + int_39 + " ] ";
					Acquisition acquisition = instrument.methodSetup.dtcAcquisitions[num7];
					string_ = text + Lang.PS("采集", "Acquisition") + "." + instrument.dtc_Channels[num7].name + ":";
					string_2 = acquisition.AcqRange + " " + Class49.MesureUnit() + ",  " + acquisition.AcqRate + " Hz";
					goto IL_0c55;
					IL_0338:
					int_34++;
					if (int_34 >= int_35.Length)
					{
						return true;
					}
					continue;
					IL_0dc5:
					method_2(num, 600, instrument.methodSetup.chromInfo, instrument.methodSetup.chromInfoR, ref string_, ref string_2, ref string_3, ref string_4);
					goto IL_0c55;
					IL_0a1b:
					int num8 = num - 500;
					text = "[ " + (num8 + 1) + " / " + int_39 + " ] ";
					string_ = text + Lang.PS("[方法] 积分", "[Method] Integration") + "." + instrument.dtc_Channels[num8].name + ":";
					Integration integration = instrument.methodSetup.sigIntegrations[num8];
					if (gvPrtInfos_3.DimCount != 0 && integration.Count != 0)
					{
						int count = integration.Count;
						while (int_37 < gvPrtInfos_3.PartsNum && int_38 < count)
						{
							string[] array5 = gvPrtInfos_3.colNames[int_37];
							string[] array6 = new string[array5.Length];
							IntegRow integRow = integration.IntegRows[int_38];
							for (int k = 0; k < array5.Length; k++)
							{
								if (array5[k] == "")
								{
									array6[k] = (int_38 + 1).ToString();
								}
								else
								{
									array6[k] = instrument.form.dlgMethodSetup.gvInteg.gvValue(gvUse: false, integRow, array5[k]).ToString();
								}
							}
							if (!method_19(graphics_0, string_, "", gvPrtInfos_3, array6, -1, count, ref int_37, ref int_38, ref float_6))
							{
								return false;
							}
						}
						num8++;
						int_38 = 0;
						int_37 = 0;
						int_34++;
						break;
					}
					string_2 = Lang.PS("[ 空 ]", "[ Null ]");
					goto IL_0c55;
				}
			}
		}
		return true;
	}

	private bool method_23(Graphics graphics_0, ref float float_6)
	{
		if (rptSetup_0.rhUse && int_46.Length != 0)
		{
			if (!bool_10)
			{
				bool_10 = true;
				int_45 = 0;
				if (float_6 != float_0 && rptSetup_0.rhOnNewPage)
				{
					return false;
				}
			}
			while (int_45 < int_46.Length)
			{
				float_6 += int_28;
				string text = null;
				string string_ = null;
				string text2;
				string string_2;
				while (true)
				{
					int num = int_46[int_45];
					method_8(num, ref float_6);
					switch (num)
					{
					default:
						goto IL_00d9;
					case 100:
						text2 = cbrhDateTime.Text + ":";
						string_2 = DateTime.Now.ToLongDateString() + "   " + DateTime.Now.ToShortTimeString();
						break;
					case 0:
						text2 = Lang.PS("操作系统:", "Comp. OS:");
						string_2 = Environment.OSVersion.VersionString;
						break;
					case 1:
						text2 = Lang.PS("计算机名:", "Comp. Name:");
						string_2 = Environment.MachineName;
						text = Lang.PS("计算机用户:", "Comp. User:");
						string_ = Environment.UserName;
						break;
					case 2:
						text2 = Lang.PS("工作站版本:", "CS Version:");
						string_2 = Class49.smethod_13() + " " + Class49.smethod_37();
						text = Lang.PS("工作站用户:", "CS User:");
						string_ = instrument.user.u_name;
						break;
					}
					break;
					IL_00d9:
					int_45++;
					if (int_45 >= int_46.Length)
					{
						return true;
					}
				}
				bool flag = false;
				if (text2 != null && text != null)
				{
					flag = method_31(graphics_0, text2, string_2, text, string_, ref float_6);
				}
				else if (text2 != null)
				{
					flag = method_30(graphics_0, text2, string_2, ref float_6);
				}
				if (flag)
				{
					int_45++;
					continue;
				}
				return false;
			}
		}
		return true;
	}

	private void method_24(Graphics graphics_0, string string_159, float float_6, float float_7, float float_8, float float_9)
	{
		method_32(graphics_0, float_6, float_7, float_9);
		method_32(graphics_0, float_6 + float_8, float_7, float_9);
		method_28(graphics_0, string_159, float_6, float_7, float_8, float_9);
	}

	private bool method_25(Graphics graphics_0, ref float float_6)
	{
		if (rptSetup_0.sqUse && int_48.Length != 0)
		{
			if (!bool_11)
			{
				bool_11 = true;
				int_47 = 0;
				int_52 = 0;
				int_51 = 0;
				if (float_6 != float_0 && rptSetup_0.sqOnNewPage)
				{
					return false;
				}
			}
			while (int_47 < int_48.Length)
			{
				float_6 += int_28;
				string string_ = null;
				string text = null;
				string string_2 = null;
				while (true)
				{
					int num = int_48[int_47];
					method_8(num, ref float_6);
					SeqAly curSeqAly = instrument.form.seqAlyForm.CurSeqAly;
					SeqAlyOpt seqAlyOpt = curSeqAly.seqAlyOpt;
					string text2;
					bool flag;
					switch (num)
					{
					default:
						goto IL_0119;
					case 100:
						text2 = cbsqInjList.Text + ":";
						if (gvPrtInfos_8.DimCount != 0 && curSeqAly.seqInjs.Length != 0)
						{
							int num2 = curSeqAly.seqInjs.Length;
							while (int_51 < gvPrtInfos_8.PartsNum && int_52 < num2)
							{
								string[] array = gvPrtInfos_8.colNames[int_51];
								string[] array2 = new string[array.Length];
								Injection injection_ = curSeqAly.seqInjs[int_52];
								for (int i = 0; i < array.Length; i++)
								{
									if (array[i] == "")
									{
										array2[i] = (int_52 + 1).ToString();
									}
									else
									{
										array2[i] = instrument.form.seqAlyForm.gvValue(gvUse: false, injection_, array[i]).ToString();
									}
								}
								if (!method_19(graphics_0, text2, "", gvPrtInfos_8, array2, -1, num2, ref int_51, ref int_52, ref float_6))
								{
									return false;
								}
							}
							int_47++;
							break;
						}
						string_ = Lang.PS("[ 空 ]", "[ Null ]");
						goto IL_02d1;
					case 0:
					{
						text2 = Lang.PS("序列文件", "Seq. File") + ":";
						string curFullName = instrument.form.seqAlyForm.CurFullName;
						string_ = ((curFullName != null) ? curFullName : Lang.PS("[ 空 ]", "[ Null ]"));
						goto IL_02d1;
					}
					case 1:
						text2 = SeqAlyOptDlg.sIdleBeforeFirstInj + ":";
						string_ = (seqAlyOpt.idleBeforeFirstInj ? (seqAlyOpt.idleTime + " " + instrument.form.seqAlyForm.dlgSequAlyOptions.lbIdleTimeU.Text) : Lang.PS("非", "False"));
						goto IL_02d1;
					case 2:
					{
						text2 = SeqAlyOptDlg.sCounter + "." + SeqAlyOptDlg.sStart + ":";
						string_ = seqAlyOpt.counter_start.ToString();
						text = SeqAlyOptDlg.sCounter + "." + SeqAlyOptDlg.sReset + ":";
						CounterResetStyle counter_resetStyle = seqAlyOpt.counter_resetStyle;
						if (counter_resetStyle != CounterResetStyle.OpenInstrument)
						{
							if (counter_resetStyle == CounterResetStyle.Never)
							{
								string_2 = SeqAlyOptDlg.sNever;
							}
						}
						else
						{
							string_2 = SeqAlyOptDlg.sOpenInstrument;
						}
						goto IL_02d1;
					}
					case 3:
					{
						text2 = SeqAlyOptDlg.sFormat + ":";
						switch (seqAlyOpt.formatStyle)
						{
						case FormatStyle.Automatically:
							string_ = SeqAlyOptDlg.sAutomatically;
							break;
						case FormatStyle.Manually:
							string_ = SeqAlyOptDlg.sManually;
							break;
						}
						text = SeqAlyOptDlg.sInjVolumnUnit + ":";
						VolumnUnits injVolumnUnit = seqAlyOpt.injVolumnUnit;
						if (injVolumnUnit != VolumnUnits.const_0)
						{
							if (injVolumnUnit == VolumnUnits.const_1)
							{
								string_2 = "ml";
							}
						}
						else
						{
							string_2 = "μl";
						}
						goto IL_02d1;
					}
					case 4:
						{
							text2 = SeqAlyOptDlg.sDescription + ":";
							string_ = seqAlyOpt.description;
							goto IL_02d1;
						}
						IL_02d1:
						flag = false;
						if (text2 != null && text != null)
						{
							flag = method_31(graphics_0, text2, string_, text, string_2, ref float_6);
						}
						else if (text2 != null)
						{
							flag = method_30(graphics_0, text2, string_, ref float_6);
						}
						if (flag)
						{
							int_47++;
							break;
						}
						return false;
					}
					break;
					IL_0119:
					int_47++;
					if (int_47 >= int_48.Length)
					{
						return true;
					}
				}
			}
		}
		return true;
	}

	private bool method_26(Graphics graphics_0, int int_58, int int_59, Chromatogram chromatogram_4, ref float float_6, string string_159, string string_160, string string_161, string string_162, ref bool bool_13)
	{
		string text = "";
		if (int_58 != 1)
		{
			string[] array = new string[5]
			{
				">>>> [ ",
				(int_59 + 1).ToString(),
				" / ",
				int_58.ToString(),
				" ] "
			};
			text = string.Concat(array);
		}
		string string_163 = ((text != "") ? (text + Lang.PS("谱图", "Chrom.") + " <<<<") : (">>>> " + Lang.PS("谱图", "Chrom.") + " <<<<"));
		string text2 = method_43(int_58, int_59, chromatogram_4.fullName, chromatogram_4.fName);
		float float_7 = 0f;
		float float_8 = 0f;
		bool flag;
		if (flag = method_9(graphics_0, string_163, bool_13: true, float_6, out float_7) && method_9(graphics_0, text2, bool_13: false, float_6, out float_8))
		{
			float num = Math.Max(float_7, float_8);
			float num2 = float_6 + num + (float)int_28;
			float float_9 = 0f;
			float float_10 = 0f;
			if (string_159 != null && string_161 != null)
			{
				flag = method_10(graphics_0, string_159, bool_13: true, num2, out float_7) && method_10(graphics_0, string_160, bool_13: false, num2, out float_8) && method_10(graphics_0, string_161, bool_13: true, num2, out float_9) && method_10(graphics_0, string_162, bool_13: false, num2, out float_10);
			}
			else if (string_159 != null)
			{
				flag = method_9(graphics_0, string_159, bool_13: true, num2, out float_7) && method_9(graphics_0, string_160, bool_13: false, num2, out float_8);
			}
			if (!flag)
			{
				return false;
			}
			num = Math.Max(Math.Max(float_7, float_8), Math.Max(float_9, float_10));
			if (num2 + num < (float)rectangle_4.Bottom)
			{
				method_30(graphics_0, string_163, text2, ref float_6);
				bool_13 = true;
				float_6 += int_28;
				return true;
			}
		}
		return false;
	}

	private void method_27(Graphics graphics_0, bool bool_13, float float_6, float float_7, ref float float_8)
	{
		float_8 += 2f;
		int int_ = ((!bool_13) ? 1 : 2);
		method_33(graphics_0, float_6, float_8, float_7, int_);
		float_8 += 2f;
	}

	private void method_28(Graphics graphics_0, string string_159, float float_6, float float_7, float float_8, float float_9)
	{
		if (!bool_12)
		{
			rectangleF_0.X = float_6;
			rectangleF_0.Y = float_7;
			rectangleF_0.Width = float_8;
			rectangleF_0.Height = float_9;
			solidBrush_0.Color = (rptSetup_0.psColors ? rptSetup_0.psItemColor : Color.Black);
			stringFormat_0.Alignment = StringAlignment.Center;
			graphics_0.DrawString(string_159, rptSetup_0.psItemFont, solidBrush_0, rectangleF_0, stringFormat_0);
		}
	}

	private void method_29(Graphics graphics_0, Image image_2, ref Bitmap bitmap_3, Rectangle rectangle_6)
	{
		if (bool_12 || image_2 == null)
		{
			return;
		}
		int num = image_2.Width;
		int num2 = image_2.Height;
		if (rptSetup_0.psColors)
		{
			graphics_0.DrawImage(image_2, rectangle_6, 0, 0, num, num2, GraphicsUnit.Pixel);
			return;
		}
		if (bitmap_3 == null)
		{
			bitmap_3 = new Bitmap(image_2);
			for (int i = 0; i < num; i++)
			{
				for (int j = 0; j < num2; j++)
				{
					Color pixel = bitmap_3.GetPixel(i, j);
					int num3 = (pixel.R + pixel.G + pixel.B) / 3;
					bitmap_3.SetPixel(i, j, Color.FromArgb(num3, num3, num3));
				}
			}
		}
		graphics_0.DrawImage(bitmap_3, rectangle_6, 0, 0, num, num2, GraphicsUnit.Pixel);
	}

	private bool method_30(Graphics graphics_0, string string_159, string string_160, ref float float_6)
	{
		float float_7 = 0f;
		float float_8 = 0f;
		if (method_9(graphics_0, string_159, bool_13: true, float_6, out float_7) && method_9(graphics_0, string_160, bool_13: false, float_6, out float_8))
		{
			float num = Math.Max(float_7, float_8);
			method_39(graphics_0, string_159, bool_13: true, bool_14: true, rectangle_4.Left, float_6, num);
			method_39(graphics_0, string_160, bool_13: true, bool_14: false, rectangle_4.Left + int_29, float_6, num);
			float_6 += num;
			return true;
		}
		return false;
	}

	private bool method_31(Graphics graphics_0, string string_159, string string_160, string string_161, string string_162, ref float float_6)
	{
		float float_7 = 0f;
		float float_8 = 0f;
		float float_9 = 0f;
		float float_10 = 0f;
		if (method_10(graphics_0, string_159, bool_13: true, float_6, out float_7) && method_10(graphics_0, string_160, bool_13: false, float_6, out float_8) && method_10(graphics_0, string_161, bool_13: true, float_6, out float_9) && method_10(graphics_0, string_162, bool_13: false, float_6, out float_10))
		{
			float num = Math.Max(Math.Max(float_7, float_8), Math.Max(float_9, float_10));
			method_39(graphics_0, string_159, bool_13: false, bool_14: true, rectangle_4.Left, float_6, num);
			method_39(graphics_0, string_160, bool_13: false, bool_14: false, rectangle_4.Left + int_30, float_6, num);
			method_39(graphics_0, string_161, bool_13: false, bool_14: true, int_36, float_6, num);
			method_39(graphics_0, string_162, bool_13: false, bool_14: false, int_36 + int_30, float_6, num);
			float_6 += num;
			return true;
		}
		return false;
	}

	private void method_32(Graphics graphics_0, float float_6, float float_7, float float_8)
	{
		if (!bool_12)
		{
			pen_0.Color = Color.Black;
			graphics_0.DrawLine(pen_0, float_6, float_7, float_6, float_7 + float_8);
		}
	}

	private void method_33(Graphics graphics_0, float float_6, float float_7, float float_8, int int_58)
	{
		if (!bool_12)
		{
			pen_0.Color = Color.Black;
			pen_0.Width = int_58;
			graphics_0.DrawLine(pen_0, float_6, float_7, float_6 + float_8, float_7);
			pen_0.Width = 1f;
		}
	}

	private void method_34(Graphics graphics_0, Color color_1, Rectangle rectangle_6)
	{
		if (!bool_12)
		{
			pen_0.Color = color_1;
			graphics_0.DrawRectangle(pen_0, rectangle_6);
		}
	}

	private void method_35(Graphics graphics_0, GvPrtInfos gvPrtInfos_11, string[] string_159, int int_58, int int_59, int int_60, float float_6, ref float float_7)
	{
		string[] array = gvPrtInfos_11.colNames[int_59];
		StringAlignment[] array2 = gvPrtInfos_11.colAligns[int_59];
		float[] array3 = gvPrtInfos_11.colWidths[int_59];
		float num = float_6;
		for (int i = 0; i < array.Length; i++)
		{
			StringAlignment stringAlignment_ = array2[i];
			if (gvPrtInfos_11 == gvPrtInfos_9)
			{
				method_36(graphics_0, array[i], int_60, string_159[i], i, num, float_7, array3[i], stringAlignment_);
			}
			else if (i == int_58)
			{
				method_40(graphics_0, string_159[i], num, float_7, array3[i], stringAlignment_);
			}
			else
			{
				method_41(graphics_0, string_159[i], num, float_7, array3[i], stringAlignment_);
			}
			num += array3[i];
		}
		float_7 += float_5;
	}

	private void method_36(Graphics graphics_0, string string_159, int int_58, string string_160, int int_59, float float_6, float float_7, float float_8, StringAlignment stringAlignment_0)
	{
		if (string_159 == "Chrom" && int_58 < 6)
		{
			stringAlignment_0 = StringAlignment.Far;
		}
		else if (int_58 == 5)
		{
			stringAlignment_0 = StringAlignment.Center;
		}
		if (!bool_12)
		{
			rectangleF_0.X = float_6;
			rectangleF_0.Y = float_7;
			rectangleF_0.Width = float_8;
			rectangleF_0.Height = float_5;
			solidBrush_0.Color = _clr(rptSetup_0.psColors, color_0[int_59]);
			stringFormat_0.Alignment = stringAlignment_0;
			graphics_0.DrawString(string_160, rptSetup_0.psValueFont, solidBrush_0, rectangleF_0, stringFormat_0);
		}
	}

	private void method_37(Graphics graphics_0, string string_159, bool bool_13, float float_6, float float_7)
	{
		if (!bool_12)
		{
			Font font = (bool_13 ? rptSetup_0.psItemFont : rptSetup_0.psValueFont);
			if (bool_13)
			{
				solidBrush_0.Color = (rptSetup_0.psColors ? rptSetup_0.psItemColor : Color.Black);
			}
			else
			{
				solidBrush_0.Color = (rptSetup_0.psColors ? rptSetup_0.psValueColor : Color.Black);
			}
			graphics_0.DrawString(string_159, font, solidBrush_0, float_6, float_7);
		}
	}

	private void method_38(Graphics graphics_0, string string_159, Font font_0, Color color_1, Rectangle rectangle_6, StringAlignment stringAlignment_0)
	{
		if (!bool_12)
		{
			solidBrush_0.Color = color_1;
			stringFormat_0.Alignment = stringAlignment_0;
			graphics_0.DrawString(string_159, font_0, solidBrush_0, rectangle_6, stringFormat_0);
		}
	}

	private void method_39(Graphics graphics_0, string string_159, bool bool_13, bool bool_14, float float_6, float float_7, float float_8)
	{
		if (!bool_12)
		{
			Font font = (bool_14 ? rptSetup_0.psItemFont : rptSetup_0.psValueFont);
			if (bool_14)
			{
				solidBrush_0.Color = (rptSetup_0.psColors ? rptSetup_0.psItemColor : Color.Black);
			}
			else
			{
				solidBrush_0.Color = (rptSetup_0.psColors ? rptSetup_0.psValueColor : Color.Black);
			}
			rectangleF_0.X = float_6;
			rectangleF_0.Y = float_7;
			if (bool_13)
			{
				rectangleF_0.Width = (bool_14 ? ((float)int_29) : ((float)int_56));
			}
			else
			{
				rectangleF_0.Width = (bool_14 ? ((float)int_30) : ((float)int_57));
			}
			rectangleF_0.Height = float_8;
			graphics_0.DrawString(string_159, font, solidBrush_0, rectangleF_0);
		}
	}

	private void method_40(Graphics graphics_0, string string_159, float float_6, float float_7, float float_8, StringAlignment stringAlignment_0)
	{
		if (!bool_12)
		{
			rectangleF_0.X = float_6;
			rectangleF_0.Y = float_7;
			rectangleF_0.Width = float_8;
			rectangleF_0.Height = float_5;
			Color color = Color.FromArgb(int.Parse(string_159));
			solidBrush_0.Color = _clr(rptSetup_0.psColors, color);
			graphics_0.FillRectangle(solidBrush_0, rectangleF_0);
		}
	}

	private void method_41(Graphics graphics_0, string string_159, float float_6, float float_7, float float_8, StringAlignment stringAlignment_0)
	{
		if (!bool_12)
		{
			rectangleF_0.X = float_6;
			rectangleF_0.Y = float_7;
			rectangleF_0.Width = float_8;
			rectangleF_0.Height = float_5;
			solidBrush_0.Color = (rptSetup_0.psColors ? rptSetup_0.psValueColor : Color.Black);
			stringFormat_0.Alignment = stringAlignment_0;
			graphics_0.DrawString(string_159, rptSetup_0.psValueFont, solidBrush_0, rectangleF_0, stringFormat_0);
		}
	}

	private void method_42(Graphics graphics_0, Color color_1, Rectangle rectangle_6)
	{
		if (!bool_12)
		{
			solidBrush_0.Color = color_1;
			graphics_0.FillRectangle(solidBrush_0, rectangle_6);
		}
	}

	private string method_43(int int_58, int int_59, string string_159, string string_160)
	{
		if (rptSetup_0.rhNamePathStyle == NamePathStyle.Always)
		{
			return string_159;
		}
		if (int_58 == 1)
		{
			return string_160;
		}
		if (int_59 != int_2)
		{
			return string_159;
		}
		return string_160;
	}

	private void gvlhLines_SelectionChanged(object sender, EventArgs e)
	{
		bool flag = gvlhLines.CurrentCell == null;
		LclRadioButton lclRadioButton = rblhL;
		LclRadioButton lclRadioButton2 = rblhM;
		LclRadioButton lclRadioButton3 = rblhR;
		bool flag2 = (btnlhFont.Enabled = !flag);
		bool flag4 = (lclRadioButton3.Enabled = flag2);
		bool enabled = (lclRadioButton2.Enabled = flag4);
		lclRadioButton.Enabled = enabled;
		if (rblhL.Enabled)
		{
			int_18 = gvlhLines.CurrentCell.RowIndex;
			LabHdrTag labHdrTag = (LabHdrTag)gvlhLines.Rows[int_18].Tag;
			rblhL.Checked = labHdrTag.align == StringAlignment.Near;
			rblhM.Checked = labHdrTag.align == StringAlignment.Center;
			rblhR.Checked = labHdrTag.align == StringAlignment.Far;
		}
		else
		{
			RadioButton radioButton = rblhL;
			RadioButton radioButton2 = rblhM;
			rblhR.Checked = false;
			radioButton2.Checked = false;
			radioButton.Checked = false;
		}
	}

	private void method_44(bool bool_13)
	{
		if (rptSetup_0.clUse)
		{
			bool_5 = false;
			bool_2 = rptSetup_0.clOptions || rptSetup_0.clCmpds;
			bool_1 = rptSetup_0.clcdGnlLevels || rptSetup_0.clcdGnlGraph;
			if (bool_13)
			{
				Array.Resize(ref int_6, 0);
				method_7(rptSetup_0.clOptions, ref int_6, 0, 10);
				method_7(rptSetup_0.clCmpds, ref int_6, 100, 1);
				method_52();
				int int_ = instrument.form.caliGnlForm.CurCaliGnl.cmpds.Length;
				method_7(rptSetup_0.clcdGnlLevels, ref int_6, 1000, int_);
				method_51();
				method_7(rptSetup_0.clcdGnlGraph, ref int_6, 2000, int_);
				method_45();
			}
		}
	}

	private void method_45()
	{
		class8_0.LinkOptions(instrument.user.options);
		class8_0.rcPage = printDocument_0.DefaultPageSettings.Bounds;
		class8_0.dskRC.Size = sizeF_0;
		class8_0.dskRC.X = (float)rectangle_4.Left + ((float)rectangle_4.Width - sizeF_0.Width) / 2f;
		class8_0.psColors = rptSetup_0.psColors;
	}

	private void method_46()
	{
		if (rptSetup_0.ciciIntegration)
		{
			instrument.form.chromForm.chromDataGrid.GetItgDisColumns(ref gvInfos_0);
			method_56(ref gvPrtInfos_0);
		}
	}

	private void method_47()
	{
		chromDisplay_0.LinkOptions(instrument.user.options);
		chromDisplay_0.showFlowLine = false;
		chromDisplay_0.showGrdtBelt = false;
		chromDisplay_0.showProgTemp = false;
		chromDisplay_0.rcPage = printDocument_0.DefaultPageSettings.Bounds;
		chromDisplay_0.dskRC.Size = sizeF_1;
		chromDisplay_0.dskRC.X = (float)rectangle_4.Left + ((float)rectangle_4.Width - sizeF_1.Width) / 2f;
		chromDisplay_0.psColors = rptSetup_0.psColors;
	}

	private void method_48(bool bool_13)
	{
		if (!rptSetup_0.cgUse)
		{
			Array.Resize(ref chromatogram_1, 0);
			return;
		}
		bool_6 = false;
		if (!bool_13)
		{
			return;
		}
		if (rptSetup_0.cgChooseStyle == ChooseStyle.All)
		{
			Array.Resize(ref chromatogram_1, chromatogram_0.Length);
			for (int i = 0; i < chromatogram_1.Length; i++)
			{
				chromatogram_1[i] = chromatogram_0[i];
			}
		}
		else if (rptSetup_0.cgChooseStyle == ChooseStyle.Active)
		{
			Array.Resize(ref chromatogram_1, 1);
			Class49.SafeValueCheck(ref int_2, 0, chromatogram_0.Length - 1);
			chromatogram_1[0] = chromatogram_0[int_2];
		}
		Array.Resize(ref int_8, 0);
		if (rptSetup_0.cgGraphShowStyle == GraphShowStyle.Combine)
		{
			method_7(bool_13: true, ref int_8, 0, (chromatogram_1.Length != 0) ? 1 : 0);
			for (int j = 0; j < chromatogram_1.Length; j++)
			{
				if (j < 12)
				{
					chromatogram_1[j].signal.disColor = (rptSetup_0.psColors ? instrument.user.options.sgColors[j] : ((chromatogram_1.Length == 1) ? Color.Black : _clr(psColors: false, instrument.user.options.sgColors[j])));
				}
			}
		}
		else if (rptSetup_0.cgGraphShowStyle == GraphShowStyle.Separate)
		{
			method_7(bool_13: true, ref int_8, 0, chromatogram_1.Length);
			for (int k = 0; k < chromatogram_1.Length; k++)
			{
				if (k < 12)
				{
					chromatogram_1[k].signal.disColor = (rptSetup_0.psColors ? instrument.user.options.sgColors[k] : Color.Black);
				}
			}
		}
		method_47();
	}

	private void method_49(bool bool_13)
	{
		if (rptSetup_0.ciUse && chromatogram_0.Length != 0)
		{
			bool_7 = false;
			if (!bool_13)
			{
				return;
			}
			if (rptSetup_0.ciChooseStyle == ChooseStyle.All)
			{
				Array.Resize(ref chromatogram_2, chromatogram_0.Length);
				for (int i = 0; i < chromatogram_2.Length; i++)
				{
					chromatogram_2[i] = chromatogram_0[i];
				}
			}
			else if (rptSetup_0.ciChooseStyle == ChooseStyle.Active)
			{
				Array.Resize(ref chromatogram_2, 1);
				Class49.SafeValueCheck(ref int_2, 0, chromatogram_0.Length - 1);
				chromatogram_2[0] = chromatogram_0[int_2];
			}
			Array.Resize(ref int_10, 0);
			method_7(rptSetup_0.ciciMeasurement, ref int_10, 0, 8);
			method_7(rptSetup_0.ciciIntegration, ref int_10, 100, 1);
			method_46();
			method_7(rptSetup_0.ciciCalculation, ref int_10, 200, 7);
			method_7(rptSetup_0.ciciAdvance, ref int_10, 300, 3);
			method_7(rptSetup_0.ciciPDA, ref int_10, 400, 11);
			method_7(rptSetup_0.ciciGpcRanges, ref int_10, 500, 2);
		}
		else
		{
			Array.Resize(ref chromatogram_2, 0);
		}
	}

	private void method_50(bool bool_13)
	{
		if (rptSetup_0.crUse && chromatogram_0.Length != 0)
		{
			bool_8 = false;
			if (!bool_13)
			{
				return;
			}
			if (rptSetup_0.crChooseStyle == ChooseStyle.All)
			{
				Array.Resize(ref chromatogram_3, chromatogram_0.Length);
				for (int i = 0; i < chromatogram_3.Length; i++)
				{
					chromatogram_3[i] = chromatogram_0[i];
				}
			}
			else if (rptSetup_0.crChooseStyle == ChooseStyle.Active)
			{
				Array.Resize(ref chromatogram_3, 1);
				Class49.SafeValueCheck(ref int_2, 0, chromatogram_0.Length - 1);
				chromatogram_3[0] = chromatogram_0[int_2];
			}
			Array.Resize(ref int_12, 0);
			method_7(rptSetup_0.crSummary, ref int_12, 0, 1);
			method_68();
			method_7(rptSetup_0.crSST, ref int_12, 100, 1);
			method_67();
			method_7(rptSetup_0.crResult, ref int_12, 200, 1);
			method_63();
			method_7(rptSetup_0.crPerformance, ref int_12, 300, 1);
			method_60();
			method_7(rptSetup_0.crGpcSlices, ref int_12, 400, 1);
			method_7(rptSetup_0.crGpcRanges, ref int_12, 500, 2);
		}
		else
		{
			Array.Resize(ref chromatogram_3, 0);
		}
	}

	private void method_51()
	{
		if (rptSetup_0.clcdGnlLevels)
		{
			instrument.form.caliGnlForm.GetCmpdDisColumns(ref gvInfos_0);
			method_56(ref gvPrtInfos_1);
		}
	}

	private void method_52()
	{
		if (rptSetup_0.clCmpds)
		{
			instrument.form.caliGnlForm.GetCmpdsDisColumns(ref gvInfos_0);
			method_56(ref gvPrtInfos_2);
		}
	}

	private void method_53()
	{
		if (rptSetup_0.mtdIntegration)
		{
			instrument.form.dlgMethodSetup.GetItgDisColumns(ref gvInfos_0);
			method_56(ref gvPrtInfos_3);
		}
	}

	private void method_54()
	{
		gradientDisplay_0.instruStyle = InstruStyle.LC;
		gradientDisplay_0.txtY = "Flow";
		gradientDisplay_0.unitY = "mL/min";
		gradientDisplay_0.fmtY = "0.0";
		gradientDisplay_0.txtY_ = "Gradient";
		gradientDisplay_0.unitY_ = "%";
		gradientDisplay_0.refScaleY_Num = 4;
		gradientDisplay_0.LinkOptions(instrument.user.options);
		gradientDisplay_0.PrepareInfo(instrument.methodSetup.chromInfoR.LcGradient);
		gradientDisplay_0.rcPage = printDocument_0.DefaultPageSettings.Bounds;
		gradientDisplay_0.dskRC.Size = sizeF_2;
		gradientDisplay_0.dskRC.X = (float)rectangle_4.Left + ((float)rectangle_4.Width - sizeF_2.Width) / 2f;
		gradientDisplay_0.psColors = rptSetup_0.psColors;
	}

	private void method_55()
	{
		if (rptSetup_0.mtdLcGradient)
		{
			instrument.form.dlgMethodSetup.GetGrdtDisColumns(ref gvInfos_0, instrument.methodSetup);
			method_56(ref gvPrtInfos_5);
		}
	}

	private void method_56(ref GvPrtInfos gvPrtInfos_11)
	{
		if (gvInfos_0.ColCount == 0)
		{
			Array.Resize(ref gvPrtInfos_11.colNames, 0);
			Array.Resize(ref gvPrtInfos_11.colHdrTxts, 0);
			Array.Resize(ref gvPrtInfos_11.colAligns, 0);
			Array.Resize(ref gvPrtInfos_11.colWidths, 0);
			Array.Resize(ref gvPrtInfos_11.float_0, 0);
			Array.Resize(ref gvPrtInfos_11.float_1, 0);
			return;
		}
		float[] array = new float[gvInfos_0.ColCount];
		float num = 0f;
		for (int i = 0; i < gvInfos_0.ColCount; i++)
		{
			array[i] = gvInfos_0.colWidths[i];
			num += array[i];
		}
		float num2 = num / (float)(rectangle_4.Width - 45);
		int num3 = (int)Math.Floor(num2) + 1;
		float num4 = num / (float)num3;
		float num5 = ((float)(rectangle_4.Width - 45) - num4) / 2f;
		num4 += num5;
		int num6 = 0;
		int num7 = -1;
		for (int j = 0; j < gvInfos_0.ColCount; j++)
		{
			float num8 = array[j];
			if (num7 == -1 || num + num8 > 45f + num4)
			{
				num7++;
				if (num7 != 0)
				{
					gvPrtInfos_11.float_0[num7 - 1] = (float)rectangle_4.Left + ((float)rectangle_4.Width - num) / 2f;
					gvPrtInfos_11.float_1[num7 - 1] = num;
				}
				Array.Resize(ref gvPrtInfos_11.colNames, num7 + 1);
				Array.Resize(ref gvPrtInfos_11.colHdrTxts, num7 + 1);
				Array.Resize(ref gvPrtInfos_11.colAligns, num7 + 1);
				Array.Resize(ref gvPrtInfos_11.colWidths, num7 + 1);
				Array.Resize(ref gvPrtInfos_11.float_0, num7 + 1);
				Array.Resize(ref gvPrtInfos_11.float_1, num7 + 1);
				Array.Resize(ref gvPrtInfos_11.colNames[num7], 1);
				gvPrtInfos_11.colNames[num7][0] = "";
				Array.Resize(ref gvPrtInfos_11.colHdrTxts[num7], 1);
				gvPrtInfos_11.colHdrTxts[num7][0] = "";
				Array.Resize(ref gvPrtInfos_11.colAligns[num7], 1);
				gvPrtInfos_11.colAligns[num7][0] = StringAlignment.Center;
				Array.Resize(ref gvPrtInfos_11.colWidths[num7], 1);
				float[] array2 = gvPrtInfos_11.colWidths[num7];
				int num9 = 0;
				num6 = num9 + 1;
				num = (array2[num9] = 45f);
			}
			Array.Resize(ref gvPrtInfos_11.colNames[num7], num6 + 1);
			gvPrtInfos_11.colNames[num7][num6] = gvInfos_0.colNames[j];
			Array.Resize(ref gvPrtInfos_11.colHdrTxts[num7], num6 + 1);
			gvPrtInfos_11.colHdrTxts[num7][num6] = gvInfos_0.colHdrTxts[j];
			Array.Resize(ref gvPrtInfos_11.colAligns[num7], num6 + 1);
			gvPrtInfos_11.colAligns[num7][num6] = gvInfos_0.colAligns[j];
			Array.Resize(ref gvPrtInfos_11.colWidths[num7], num6 + 1);
			gvPrtInfos_11.colWidths[num7][num6++] = num8;
			num += num8;
		}
		gvPrtInfos_11.float_0[num7] = (float)rectangle_4.Left + ((float)rectangle_4.Width - num) / 2f;
		gvPrtInfos_11.float_1[num7] = num;
	}

	private void method_57(Graphics graphics_0)
	{
		SizeF sizeF = graphics_0.MeasureString("中国", rptSetup_0.psItemFont);
		SizeF sizeF2 = graphics_0.MeasureString("中国", rptSetup_0.psValueFont);
		float_1 = sizeF.Height;
		float_5 = sizeF2.Height;
		float_2 = graphics_0.MeasureString("中国\n[..]", rptSetup_0.psItemFont).Height;
		float_4 = (float)rectangle_4.Top - float_5 - 10f - 10f;
		float_3 = rectangle_4.Bottom + 10 + 10;
	}

	private void method_58(Graphics graphics_0)
	{
		Array.Resize(ref rectangle_1, rptSetup_0.LinesNum);
		for (int i = 0; i < rectangle_1.Length; i++)
		{
			SizeF sizeF = graphics_0.MeasureString(rptSetup_0.lhLines[i], rptSetup_0.lhLinesFt[i]);
			rectangle_1[i].X = rectangle_4.Left;
			if (i == 0)
			{
				rectangle_1[i].Y = rectangle_4.Top;
			}
			else
			{
				rectangle_1[i].Y = rectangle_1[i - 1].Bottom;
			}
			rectangle_1[i].Width = rectangle_4.Width;
			rectangle_1[i].Height = Convert.ToInt32(sizeF.Height);
			if (i == 0)
			{
				Rectangle[] array = rectangle_1;
				int num = i;
				array[num].Height = array[num].Height + 5;
			}
		}
		rectangle_0 = rectangle_1[0];
		for (int j = 1; j < rectangle_1.Length; j++)
		{
			rectangle_0.Height += rectangle_1[j].Height;
		}
		int_31 = rectangle_0.Height;
		int_32 = rectangle_4.Height / 2;
		int_22 = rectangle_4.Left;
		int_23 = rectangle_4.Right;
		image_0 = null;
		if (rptSetup_0.lhUseImgLeft && File.Exists(rptSetup_0.lhImgLeftName))
		{
			image_0 = Image.FromFile(rptSetup_0.lhImgLeftName);
			float num2 = image_0.Width;
			float num3 = image_0.Height;
			int num4 = -1;
			int num5 = -1;
			if (rptSetup_0.lhImgLeftStyle == ImageLctStyle.Original)
			{
				num5 = image_0.Width;
				num4 = image_0.Height;
			}
			else if (rptSetup_0.lhImgLeftStyle == ImageLctStyle.Header)
			{
				num4 = rectangle_0.Height - 1;
				num5 = Convert.ToInt32(num2 / num3 * (float)num4);
			}
			else if (rptSetup_0.lhImgLeftStyle == ImageLctStyle.Fixed)
			{
				num5 = rptSetup_0.lhImgLeftWidth;
				if (num5 > 0)
				{
					num4 = Convert.ToInt32(num3 / num2 * (float)num5);
				}
			}
			if (num5 > 0 && num4 > 0)
			{
				int_31 = Math.Max(int_31, num4);
				int_22 += num5;
				rectangle_2.X = rectangle_4.Left;
				rectangle_2.Y = rectangle_4.Top;
				rectangle_2.Width = num5;
				rectangle_2.Height = num4;
			}
		}
		image_1 = null;
		if (rptSetup_0.lhUseImgRight && File.Exists(rptSetup_0.lhImgRightName))
		{
			image_1 = Image.FromFile(rptSetup_0.lhImgRightName);
			float num6 = image_1.Width;
			float num7 = image_1.Height;
			int num8 = -1;
			int num9 = -1;
			if (rptSetup_0.lhImgRightStyle == ImageLctStyle.Original)
			{
				num9 = image_1.Width;
				num8 = image_1.Height;
			}
			else if (rptSetup_0.lhImgRightStyle == ImageLctStyle.Header)
			{
				num8 = rectangle_0.Height - 1;
				num9 = Convert.ToInt32(num6 / num7 * (float)num8);
			}
			else if (rptSetup_0.lhImgRightStyle == ImageLctStyle.Fixed)
			{
				num9 = rptSetup_0.lhImgRightWidth;
				if (num9 > 0)
				{
					num8 = Convert.ToInt32(num7 / num6 * (float)num9);
				}
			}
			if (num9 > 0 && num8 > 0)
			{
				int_31 = Math.Max(int_31, num8);
				int_23 -= num9;
				rectangle_3.X = rectangle_4.Right - num9;
				rectangle_3.Y = rectangle_4.Top;
				rectangle_3.Width = num9;
				rectangle_3.Height = num8;
			}
		}
		int_22 += 5;
		int_23 -= 5;
	}

	private void method_59(bool bool_13)
	{
		if (rptSetup_0.mtdUse)
		{
			bool_9 = false;
			if (bool_13)
			{
				Array.Resize(ref int_35, 0);
				method_7(rptSetup_0.mtdAS, ref int_35, 0, 5);
				method_7(rptSetup_0.mtdGcProgTemp, ref int_35, 50, 1);
				method_62();
				method_7(rptSetup_0.mtdGcPTGraph, ref int_35, 60, 1);
				method_61();
				method_7(rptSetup_0.mtdLcGradient, ref int_35, 100, 1);
				method_55();
				method_7(rptSetup_0.mtdLcGrdtGraph, ref int_35, 200, 1);
				method_54();
				method_7(rptSetup_0.mtdLcGrdtItems, ref int_35, 300, 4);
				int_39 = instrument.methodSetup.dtcAcquisitions.Count;
				method_7(rptSetup_0.mtdAcquisition, ref int_35, 400, int_39);
				method_7(rptSetup_0.mtdIntegration, ref int_35, 500, int_39);
				method_53();
				method_7(rptSetup_0.mtdciMeasurement, ref int_35, 600, 7);
				method_7(rptSetup_0.mtdciCalculation, ref int_35, 700, 7);
				method_7(rptSetup_0.mtdciAdvance, ref int_35, 800, 3);
				method_7(rptSetup_0.mtdciPDA, ref int_35, 900, 11);
				method_7(rptSetup_0.mtdciGpcRanges, ref int_35, 1000, 2);
			}
		}
	}

	private void method_60()
	{
		if (rptSetup_0.crPerformance)
		{
			instrument.form.chromForm.chromDataGrid.GetPfmDisColumns(ref gvInfos_0);
			method_56(ref gvPrtInfos_6);
		}
	}

	private void method_61()
	{
		gradientDisplay_1.instruStyle = InstruStyle.GC;
		gradientDisplay_1.txtY = "Temp.";
		gradientDisplay_1.unitY = "℃";
		gradientDisplay_1.fmtY = "0.0";
		gradientDisplay_1.LinkOptions(instrument.user.options);
		gradientDisplay_1.PrepareInfo(instrument.methodSetup.chromInfoR.GcProgTemp);
		gradientDisplay_1.rcPage = printDocument_0.DefaultPageSettings.Bounds;
		gradientDisplay_1.dskRC.Size = sizeF_2;
		gradientDisplay_1.dskRC.X = (float)rectangle_4.Left + ((float)rectangle_4.Width - sizeF_2.Width) / 2f;
		gradientDisplay_1.psColors = rptSetup_0.psColors;
	}

	private void method_62()
	{
		if (rptSetup_0.mtdGcProgTemp)
		{
			instrument.form.dlgMethodSetup.GetProgTempDisColumns(ref gvInfos_0, instrument.methodSetup);
			method_56(ref gvPrtInfos_4);
		}
	}

	private void method_63()
	{
		if (rptSetup_0.crResult)
		{
			instrument.form.chromForm.chromDataGrid.GetRltDisColumns(ref gvInfos_0);
			method_56(ref gvPrtInfos_7);
		}
	}

	private void method_64(bool bool_13)
	{
		if (rptSetup_0.rhUse)
		{
			bool_10 = false;
			if (bool_13)
			{
				Array.Resize(ref int_46, 0);
				method_7(rptSetup_0.rhSystemInfo, ref int_46, 0, 3);
				method_7(rptSetup_0.rhDateTime, ref int_46, 100, 1);
			}
		}
	}

	private void method_65(bool bool_13)
	{
		if (rptSetup_0.sqUse)
		{
			bool_11 = false;
			if (bool_13)
			{
				Array.Resize(ref int_48, 0);
				method_7(rptSetup_0.sqOptions, ref int_48, 0, 5);
				method_7(rptSetup_0.sqInjList, ref int_48, 100, 1);
				method_66();
			}
		}
	}

	private void method_66()
	{
		if (rptSetup_0.sqInjList)
		{
			instrument.form.seqAlyForm.GetDisColumns(ref gvInfos_0);
			method_56(ref gvPrtInfos_8);
		}
	}

	private void method_67()
	{
		if (rptSetup_0.crSST)
		{
			instrument.form.chromForm.chromDataGrid.GetSstDisColumns(ref gvInfos_0);
			method_56(ref gvPrtInfos_9);
		}
	}

	private void method_68()
	{
		if (rptSetup_0.crSummary)
		{
			instrument.form.chromForm.chromDataGrid.GetSmyColumns(chromatogram_3, ref gvInfos_0, ref smyHdrPara_0);
			method_56(ref gvPrtInfos_10);
		}
	}

	public DialogResult JustShow(RptSetup rptSetup)
	{
		method_69(AccStyle.Read, rptSetup);
		Control control = btnNew;
		Control control2 = btnOpen;
		Control control3 = btnSave;
		btnSaveAs.Visible = false;
		control3.Visible = false;
		control2.Visible = false;
		control.Visible = false;
		Control control4 = btnPreview;
		btnPrint.Visible = false;
		control4.Visible = false;
		DialogResult dialogResult = ShowDialog();
		if (dialogResult == DialogResult.OK)
		{
			method_69(AccStyle.Write, rptSetup);
		}
		return dialogResult;
	}

	public DialogResult JustShow2(RptSetup rptSetup)
	{
		method_69(AccStyle.Read, rptSetup);
		Control control = btnNew;
		Control control2 = btnOpen;
		Control control3 = btnSave;
		btnSaveAs.Visible = true;
		control3.Visible = true;
		control2.Visible = true;
		control.Visible = true;
		Control control4 = btnPreview;
		btnPrint.Visible = false;
		control4.Visible = false;
		DialogResult dialogResult = ShowDialog();
		if (dialogResult == DialogResult.OK)
		{
			method_69(AccStyle.Write, rptSetup);
		}
		return dialogResult;
	}

	public void Link()
	{
		if (instrument != null)
		{
			instrument.form.chromForm.SetChromsLink(ref chromatogram_0, ref int_2, ref sst_0);
		}
	}

	public override void LoadLanguage()
	{
		base.LoadLanguage();
		btnNew.Text = Lang.PS("新建", "New");
		btnOpen.Text = Lang.PS("打开...", "Open...");
		btnSave.Text = Lang.PS("保存", "Save");
		btnSaveAs.Text = Lang.PS("另存...", "Save as...");
		btnPreview.Text = Lang.PS("预览", "Preview");
		btnPrint.Text = Lang.PS("打印", "Print");
		tpPageSetup.Text = Lang.PS("页面设置", "Page Setup");
		tpLabHeader.Text = Lang.PS("实验标头", "Lab. Header");
		tpRptHeader.Text = Lang.PS("报告标头", "Report Header");
		tpMethod.Text = Lang.PS("方法", "Method");
		tpChromInfo.Text = Lang.PS("谱图信息", "Chrom. Info.");
		tpChromGraph.Text = Lang.PS("谱图", "Chrom. Graph");
		tpChromRlts.Text = Lang.PS("处理结果", "Results");
		tpCali.Text = Lang.PS("校正", "Cali.");
		tpSeq.Text = Lang.PS("序列进样", "Sequence");
		cbpsColors.Text = Lang.PS("彩色", "Colors");
		cbpsDrawGraphsBkgnd.Text = Lang.PS("绘制谱图背景", "Draw Graphs' Background Color");
		fbtnpsItem.Text = Lang.PS("项目字体", "Item Font");
		fbtnpsValue.Text = Lang.PS("值 字体", "Value Font");
		gbpsMargins.Text = Lang.PS("边界", "Margins");
		lbpsmgLeft.Text = Lang.PS("左", "Left");
		lbpsmgRight.Text = Lang.PS("右", "Right");
		lbpsmgTop.Text = Lang.PS("顶", "Top");
		lbpsmgBottom.Text = Lang.PS("底", "Bottom");
		lbpsmgInterval.Text = Lang.PS("间距", "Interval");
		cblhUse.Text = Lang.PS("使用", "Use");
		cblhJstFstPage.Text = Lang.PS("仅首页", "Just Fst page");
		cblhBorder.Text = Lang.PS("绘制边框", "Borger");
		cblhGrayBkgnd.Text = Lang.PS("灰色背景", "Gray Bkgnd");
		lblhLinesNum.Text = Lang.PS("行数", "Lines");
		cblhUseLeftImage.Text = Lang.PS("左方图像", "Left Image");
		cblhUseRightImage.Text = Lang.PS("右方图像", "Right Image");
		rblhlOriginal.Text = Lang.PS("原始", "Original");
		rblhlHeader.Text = Lang.PS("自动", "Header");
		rblhlFixed.Text = Lang.PS("固定", "Fixed");
		rblhrOriginal.Text = Lang.PS("原始", "Original");
		rblhrHeader.Text = Lang.PS("自动", "Header");
		rblhrFixed.Text = Lang.PS("固定", "Fixed");
		rblhL.Text = Lang.PS("左", "L");
		rblhM.Text = Lang.PS("中", "M");
		rblhR.Text = Lang.PS("右", "R");
		cbrhUse.Text = Lang.PS("使用", "Use");
		cbrhOnNewPage.Text = Lang.PS("使用新页", "On New Page");
		gbrhPrintFullPath.Text = Lang.PS("打印全路径文件名", "Print Full Path FileName");
		rbrhnsAlways.Text = Lang.PS("总是打印", "Always");
		rbrhnsOthersOnly.Text = Lang.PS("仅对别的谱图", "Others Only [Outside Current]");
		cbrhSystemInfo.Text = Lang.PS("系统信息", "System Info.");
		cbrhDateTime.Text = Lang.PS("打印日期", "Print Date");
		cbmtdUse.Text = Lang.PS("使用", "Use");
		cbmtdOnNewPage.Text = Lang.PS("使用新页", "On New Page");
		cbmtdAS.Text = Lang.PS("自动进样器", "auto sampler");
		cbmtdLcGradient.Text = Lang.PS("液相梯度", "Lc Gradient");
		cbmtdLcGrdtGraph.Text = Lang.PS("梯度图像", "Gradient Graph");
		cbmtdLcGrdtItems.Text = Lang.PS("梯度项目", "Gradient Items");
		cbmtdAcquisition.Text = Lang.PS("采集", "Acquisition");
		cbmtdIntegration.Text = Lang.PS("[方法] 积分", "[Method] Integration");
		gbmtdChromInfo.Text = Lang.PS("谱图信息", "Chrom. Info.");
		cbmtdciMeasurement.Text = Lang.PS("测量信息", "Measurement");
		cbmtdciCalculation.Text = Lang.PS("计算", "Calculation");
		cbmtdciAdvance.Text = Lang.PS("高级", "Advance");
		cbmtdciPDA.Text = Lang.PS("PDA", "PDA");
		cbmtdciGpcRanges.Text = Lang.PS("分段积分", "Ranges Gpc");
		cbciUse.Text = Lang.PS("使用", "Use");
		cbciOnNewPage.Text = Lang.PS("使用新页", "On New Page");
		gbciChroms.Text = Lang.PS("对像谱图", "Choose Chroms");
		rbcichmAll.Text = Lang.PS("所有谱图", "all chroms");
		rbcichmActive.Text = Lang.PS("当前谱图", "the active chrom");
		gbciChromInfo.Text = Lang.PS("谱图信息", "Chrom. Info.");
		cbciciMeasurement.Text = Lang.PS("测量信息", "Measurement");
		cbciciIntegration.Text = Lang.PS("积分", "Integration");
		cbciciCalculation.Text = Lang.PS("计算", "Calculation");
		cbciciAdvance.Text = Lang.PS("高级", "Advance");
		cbciciPDA.Text = Lang.PS("PDA", "PDA");
		cbciciGpcRanges.Text = Lang.PS("分段积分", "Ranges Gpc");
		cbcgUse.Text = Lang.PS("使用", "Use");
		cbcgOnNewPage.Text = Lang.PS("使用新页", "On New Page");
		gbcgChroms.Text = Lang.PS("对像谱图", "Choose Chroms");
		rbcgchmAll.Text = Lang.PS("所有谱图", "all chroms");
		rbcgchmActive.Text = Lang.PS("当前谱图", "the active chrom");
		gbcgShowStyle.Text = Lang.PS("显示方式", "Show Style");
		rbcgssCombine.Text = Lang.PS("整合显示", "Combine");
		rbcgssSeparate.Text = Lang.PS("分立显示", "Separate");
		gbcgDisStyle.Text = Lang.PS("显示逻辑", "Dis. Style");
		rbcgdsWhole.Text = Lang.PS("完全显示", "Whole");
		rbcgdsCurrent.Text = Lang.PS("当前逻辑", "Current");
		cbcrUse.Text = Lang.PS("使用", "Use");
		cbcrOnNewPage.Text = Lang.PS("使用新页", "On New Page");
		gbcrChroms.Text = Lang.PS("对像谱图", "Choose Chroms");
		rbcrchmAll.Text = Lang.PS("所有谱图", "all chroms");
		rbcrchmActive.Text = Lang.PS("当前谱图", "the active chrom");
		cbcrResult.Text = Lang.PS("结果", "Results");
		cbclUse.Text = Lang.PS("使用", "Use");
		cbclOnNewPage.Text = Lang.PS("使用新页", "On New Page");
		cbclOptions.Text = Lang.PS("校正选项", "Cali. Options");
		cbclCmpds.Text = Lang.PS("组分表", "Cmpds List");
		gbclCmpd.Text = Lang.PS("分类", "Assort");
		cbclcdGnlLevels.Text = Lang.PS("Levels", "Levells");
		cbclcdGnlGraph.Text = Lang.PS("图像", "Graph");
		cbsqUse.Text = Lang.PS("使用", "Use");
		cbsqOnNewPage.Text = Lang.PS("使用新页", "On New Page");
		cbsqOptions.Text = Lang.PS("序列选项", "Seq Options");
		cbsqInjList.Text = Lang.PS("进样列表", "Inj. List");
		gblhLines.Text = tpLabHeader.Text;
	}

	private void nudlhLinesNum_ValueChanged(object sender, EventArgs e)
	{
		gvlhLines.RowCount = (int)nudlhLinesNum.Value;
		for (int i = 0; i < gvlhLines.RowCount; i++)
		{
			if (gvlhLines.Rows[i].Tag == null)
			{
				LabHdrTag labHdrTag = new LabHdrTag
				{
					font = new Font(FontFamily.GenericSansSerif, 13f),
					color = Color.Black,
					align = StringAlignment.Center
				};
				gvlhLines.Rows[i].Tag = labHdrTag;
				gvlhLines.Rows[i].Cells[0].Value = "[ ]";
			}
		}
		gvlhLines.Refresh();
	}

	public void Preview()
	{
		try
		{
			prtPrvDlg.ShowDialog();
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message);
			prtPrvDlg.Close();
		}
	}

	public void print(bool refresh)
	{
		if (printDialog_0.ShowDialog() != DialogResult.OK || chromatogram_0.Length == 0)
		{
			return;
		}
		if (!printDocument_0.PrinterSettings.IsValid)
		{
			MessageBox.Show(Lang.PS("打印机无效！", "Printer is not valid!"));
			return;
		}
		string descript = Lang.PS("打印报告", "Print Report");
		try
		{
			if (refresh)
			{
				method_69(AccStyle.Write, rptSetup_0);
			}
			printDocument_0.Print();
			if (instrument != null)
			{
				StatAdtTrlRow statAdtTrlRow = MainForm.stationAdtTrlForm.AddTail(instrument.pageNo, ATResult.Ok, ATType.Print, instrument.user.u_name, instrument.name, ATArea.Instru, descript);
				for (int i = 0; i < chromatogram_0.Length; i++)
				{
					statAdtTrlRow.sTag = statAdtTrlRow.sTag + ((i == 0) ? "" : "*") + chromatogram_0[i].fullName;
				}
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message);
			if (instrument != null)
			{
				MainForm.stationAdtTrlForm.AddTail(instrument.pageNo, ATResult.Fail, ATType.Print, instrument.user.u_name, instrument.name, ATArea.Instru, descript);
			}
		}
	}

	public void Print(string[] fileNames)
	{
		Print(fileNames, instrument.instruStyle);
	}

	public void Print(string[] fileNames, InstruStyle style)
	{
		Array.Resize(ref chromatogram_0, 0);
		for (int i = 0; i < fileNames.Length; i++)
		{
			Chromatogram chromatogram = Chromatogram.LoadFromFile2(fileNames[i], DetectorStyle.General);
			if (chromatogram != null)
			{
				chromatogram.Process(style);
				int num = chromatogram_0.Length;
				Array.Resize(ref chromatogram_0, num + 1);
				chromatogram_0[num] = chromatogram;
			}
		}
		int_2 = 0;
		print(refresh: false);
	}

	private void printDocument_0_BeginPrint(object sender, PrintEventArgs e)
	{
		rectangle_4.X = method_6(rptSetup_0.psmgLeft);
		rectangle_4.Y = method_6(rptSetup_0.psmgTop);
		rectangle_4.Width = printDocument_0.DefaultPageSettings.Bounds.Width - method_6(rptSetup_0.psmgRight) - rectangle_4.Left;
		rectangle_4.Height = printDocument_0.DefaultPageSettings.Bounds.Height - method_6(rptSetup_0.psmgBottom) - rectangle_4.Top;
		bool_4 = false;
		int_28 = method_6(rptSetup_0.psmgInterval);
		int_33 = rectangle_4.Width / 4;
		float num = (float)rectangle_4.Width / 8f;
		int_36 = rectangle_4.Left + rectangle_4.Width / 2;
		int_30 = Convert.ToInt32(num);
		int_57 = Convert.ToInt32(num + num + num - 10f);
		bitmap_0 = (bitmap_1 = (bitmap_2 = null));
		rectangle_5.Size = UserAccountsDlg.defaultSignGraph;
		int_42 = 0;
		int_17 = 1;
		bool_12 = true;
		stringFormat_0.LineAlignment = StringAlignment.Center;
	}

	private void printDocument_0_PrintPage(object sender, PrintPageEventArgs e)
	{
		while (true)
		{
			Graphics graphics = e.Graphics;
			if (!bool_4)
			{
				method_57(graphics);
				method_58(graphics);
				method_64(bool_13: true);
				method_59(bool_13: true);
				method_49(bool_13: true);
				method_48(bool_13: true);
				method_50(bool_13: true);
				method_44(bool_13: true);
				method_65(bool_13: true);
				bool_4 = true;
			}
			bool flag = false;
			float float_ = rectangle_4.Top;
			method_20(graphics);
			method_21(graphics, ref float_);
			float_0 = float_;
			if (method_23(graphics, ref float_) && method_22(graphics, ref float_) && method_14(graphics, ref float_) && method_13(graphics, ref float_) && method_15(graphics, ref float_) && method_12(graphics, ref float_) && method_25(graphics, ref float_))
			{
				flag = true;
			}
			if (!bool_12)
			{
				break;
			}
			int_42++;
			if (flag)
			{
				bool_12 = false;
				int_17 = 1;
				method_64(bool_13: false);
				method_59(bool_13: false);
				method_49(bool_13: false);
				method_48(bool_13: false);
				method_50(bool_13: false);
				method_44(bool_13: false);
				method_65(bool_13: false);
			}
		}
		e.HasMorePages = int_17++ < int_42;
	}

	private void rblhL_Click(object sender, EventArgs e)
	{
		if (int_18 >= 0)
		{
			LabHdrTag labHdrTag = (LabHdrTag)gvlhLines.Rows[int_18].Tag;
			if (rblhL.Checked)
			{
				labHdrTag.align = StringAlignment.Near;
			}
			else if (rblhM.Checked)
			{
				labHdrTag.align = StringAlignment.Center;
			}
			else if (rblhR.Checked)
			{
				labHdrTag.align = StringAlignment.Far;
			}
			gvlhLines.Rows[int_18].Tag = labHdrTag;
			gvlhLines.InvalidateRow(int_18);
		}
	}

	private void method_69(AccStyle accStyle_0, RptSetup rptSetup_1)
	{
		switch (accStyle_0)
		{
		case AccStyle.Read:
		{
			cbpsColors.Checked = rptSetup_1.psColors;
			cbpsDrawGraphsBkgnd.Checked = rptSetup_1.psDrawGraphsBkgnd;
			fbtnpsItem.SetFontColor(rptSetup_1.psItemFont, rptSetup_1.psItemColor);
			fbtnpsValue.SetFontColor(rptSetup_1.psValueFont, rptSetup_1.psValueColor);
			nudpsmgLeft.Value = Convert.ToDecimal(rptSetup_1.psmgLeft);
			nudpsmgRight.Value = Convert.ToDecimal(rptSetup_1.psmgRight);
			nudpsmgTop.Value = Convert.ToDecimal(rptSetup_1.psmgTop);
			nudpsmgBottom.Value = Convert.ToDecimal(rptSetup_1.psmgBottom);
			nudpsmgInterval.Value = Convert.ToDecimal(rptSetup_1.psmgInterval);
			cbpsSign.Checked = rptSetup_1.psSign;
			cblhUse.Checked = rptSetup_1.lhUse;
			cblhJstFstPage.Checked = rptSetup_1.lhJstFstPage;
			cblhBorder.Checked = rptSetup_1.lhBorder;
			cblhGrayBkgnd.Checked = (gvlhLines.grayBkgnd = rptSetup_1.lhGrayBkgnd);
			LclNumericUpDown lclNumericUpDown = nudlhLinesNum;
			int num = (gvlhLines.RowCount = rptSetup_1.lhLinesNum);
			lclNumericUpDown.Value = num;
			for (int j = 0; j < gvlhLines.RowCount; j++)
			{
				gvlhLines.Rows[j].Cells[0].Value = rptSetup_1.lhLines[j];
				if (gvlhLines.Rows[j].Tag == null)
				{
					gvlhLines.Rows[j].Tag = default(LabHdrTag);
				}
				LabHdrTag labHdrTag2 = (LabHdrTag)gvlhLines.Rows[j].Tag;
				labHdrTag2.font = rptSetup_1.lhLinesFt[j];
				labHdrTag2.color = rptSetup_1.lhLinesClr[j];
				labHdrTag2.align = rptSetup_1.lhLinesAlign[j];
				gvlhLines.Rows[j].Tag = labHdrTag2;
			}
			gvlhLines_SelectionChanged(null, null);
			cblhUseLeftImage.Checked = rptSetup_1.lhUseImgLeft;
			tblhLeftImage.Tag = rptSetup_1.lhImgLeftName;
			if (rptSetup_1.lhImgLeftName != "" && File.Exists(rptSetup_1.lhImgLeftName))
			{
				FileInfo fileInfo = new FileInfo(rptSetup_1.lhImgLeftName);
				tblhLeftImage.Text = fileInfo.Name;
			}
			else
			{
				tblhLeftImage.Text = "";
			}
			rblhlOriginal.Checked = rptSetup_1.lhImgLeftStyle == ImageLctStyle.Original;
			rblhlHeader.Checked = rptSetup_1.lhImgLeftStyle == ImageLctStyle.Header;
			rblhlFixed.Checked = rptSetup_1.lhImgLeftStyle == ImageLctStyle.Fixed;
			tblhlWidth.Text = rptSetup_1.lhImgLeftWidth.ToString();
			cblhUseRightImage.Checked = rptSetup_1.lhUseImgRight;
			tblhRightImage.Tag = rptSetup_1.lhImgRightName;
			if (rptSetup_1.lhImgRightName != "" && File.Exists(rptSetup_1.lhImgRightName))
			{
				FileInfo fileInfo2 = new FileInfo(rptSetup_1.lhImgRightName);
				tblhRightImage.Text = fileInfo2.Name;
			}
			else
			{
				tblhRightImage.Text = "";
			}
			rblhrOriginal.Checked = rptSetup_1.lhImgRightStyle == ImageLctStyle.Original;
			rblhrHeader.Checked = rptSetup_1.lhImgRightStyle == ImageLctStyle.Header;
			rblhrFixed.Checked = rptSetup_1.lhImgRightStyle == ImageLctStyle.Fixed;
			tblhrWidth.Text = rptSetup_1.lhImgRightWidth.ToString();
			cblhUseLeftImage_CheckedChanged(cblhUseLeftImage, null);
			cblhUseLeftImage_CheckedChanged(cblhUseRightImage, null);
			cbrhUse.Checked = rptSetup_1.rhUse;
			cbrhOnNewPage.Checked = rptSetup_1.rhOnNewPage;
			rbrhnsAlways.Checked = rptSetup_1.rhNamePathStyle == NamePathStyle.Always;
			rbrhnsOthersOnly.Checked = rptSetup_1.rhNamePathStyle == NamePathStyle.OthersOnly;
			cbrhSystemInfo.Checked = rptSetup_1.rhSystemInfo;
			cbrhDateTime.Checked = rptSetup_1.rhDateTime;
			if (cbmtdUse.Enabled)
			{
				cbmtdUse.Checked = rptSetup_1.mtdUse;
			}
			cbmtdOnNewPage.Checked = rptSetup_1.mtdOnNewPage;
			cbmtdAS.Checked = rptSetup_1.mtdAS;
			cbmtdGcProgTemp.Checked = rptSetup_1.mtdGcProgTemp;
			cbmtdGcPTGraph.Checked = rptSetup_1.mtdGcPTGraph;
			cbmtdLcGradient.Checked = rptSetup_1.mtdLcGradient;
			cbmtdLcGrdtGraph.Checked = rptSetup_1.mtdLcGrdtGraph;
			cbmtdLcGrdtItems.Checked = rptSetup_1.mtdLcGrdtItems;
			cbmtdAcquisition.Checked = rptSetup_1.mtdAcquisition;
			cbmtdIntegration.Checked = rptSetup_1.mtdIntegration;
			cbmtdciMeasurement.Checked = rptSetup_1.mtdciMeasurement;
			cbmtdciCalculation.Checked = rptSetup_1.mtdciCalculation;
			cbmtdciAdvance.Checked = rptSetup_1.mtdciAdvance;
			cbmtdciPDA.Checked = rptSetup_1.mtdciPDA;
			cbmtdciGpcRanges.Checked = rptSetup_1.mtdciGpcRanges;
			cbciUse.Checked = rptSetup_1.ciUse;
			cbciOnNewPage.Checked = rptSetup_1.ciOnNewPage;
			rbcichmAll.Checked = rptSetup_1.ciChooseStyle == ChooseStyle.All;
			rbcichmActive.Checked = rptSetup_1.ciChooseStyle == ChooseStyle.Active;
			cbciciMeasurement.Checked = rptSetup_1.ciciMeasurement;
			cbciciIntegration.Checked = rptSetup_1.ciciIntegration;
			cbciciCalculation.Checked = rptSetup_1.ciciCalculation;
			cbciciAdvance.Checked = rptSetup_1.ciciAdvance;
			cbciciPDA.Checked = rptSetup_1.ciciPDA;
			cbciciGpcRanges.Checked = rptSetup_1.ciciGpcRanges;
			cbcgUse.Checked = rptSetup_1.cgUse;
			cbcgOnNewPage.Checked = rptSetup_1.cgOnNewPage;
			rbcgchmAll.Checked = rptSetup_1.cgChooseStyle == ChooseStyle.All;
			rbcgchmActive.Checked = rptSetup_1.cgChooseStyle == ChooseStyle.Active;
			rbcgssCombine.Checked = rptSetup_1.cgGraphShowStyle == GraphShowStyle.Combine;
			rbcgssSeparate.Checked = rptSetup_1.cgGraphShowStyle == GraphShowStyle.Separate;
			rbcgdsWhole.Checked = rptSetup_1.cgGraphDisStyle == GraphDisStyle.Whole;
			rbcgdsCurrent.Checked = rptSetup_1.cgGraphDisStyle == GraphDisStyle.Current;
			cbcrUse.Checked = rptSetup_1.crUse;
			cbcrOnNewPage.Checked = rptSetup_1.crOnNewPage;
			rbcrchmAll.Checked = rptSetup_1.crChooseStyle == ChooseStyle.All;
			rbcrchmActive.Checked = rptSetup_1.crChooseStyle == ChooseStyle.Active;
			cbcrResult.Checked = rptSetup_1.crResult;
			cbcrRltCombine.Checked = rptSetup_1.crRltCombine;
			cbcrSummary.Checked = rptSetup_1.crSummary;
			cbcrPerformance.Checked = rptSetup_1.crPerformance;
			cbcrSST.Checked = rptSetup_1.crSST;
			cbcrGpcSlices.Checked = rptSetup_1.crGpcSlices;
			cbcrGpcRanges.Checked = rptSetup_1.crGpcRanges;
			cbclUse.Checked = rptSetup_1.clUse;
			cbclOnNewPage.Checked = rptSetup_1.clOnNewPage;
			cbclOptions.Checked = rptSetup_1.clOptions;
			cbclCmpds.Checked = rptSetup_1.clCmpds;
			cbclcdGnlLevels.Checked = rptSetup_1.clcdGnlLevels;
			cbclcdGnlGraph.Checked = rptSetup_1.clcdGnlGraph;
			cbsqUse.Checked = rptSetup_1.sqUse;
			cbsqOnNewPage.Checked = rptSetup_1.sqOnNewPage;
			cbsqOptions.Checked = rptSetup_1.sqOptions;
			cbsqInjList.Checked = rptSetup_1.sqInjList;
			cbsqUse_CheckedChanged(cblhUse, null);
			cbsqUse_CheckedChanged(cbrhUse, null);
			cbsqUse_CheckedChanged(cbmtdUse, null);
			cbsqUse_CheckedChanged(cbciUse, null);
			cbsqUse_CheckedChanged(cbcgUse, null);
			cbsqUse_CheckedChanged(cbcrUse, null);
			cbsqUse_CheckedChanged(cbclUse, null);
			cbsqUse_CheckedChanged(cbsqUse, null);
			break;
		}
		case AccStyle.Write:
		{
			rptSetup_1.psColors = cbpsColors.Checked;
			rptSetup_1.psDrawGraphsBkgnd = cbpsDrawGraphsBkgnd.Checked;
			rptSetup_1.psItemFont = (Font)fbtnpsItem.Font.Clone();
			rptSetup_1.psItemColor = fbtnpsItem.ForeColor;
			rptSetup_1.psValueFont = (Font)fbtnpsValue.Font.Clone();
			rptSetup_1.psValueColor = fbtnpsValue.ForeColor;
			rptSetup_1.psmgLeft = Convert.ToInt32(nudpsmgLeft.Value);
			rptSetup_1.psmgRight = Convert.ToInt32(nudpsmgRight.Value);
			rptSetup_1.psmgTop = Convert.ToInt32(nudpsmgTop.Value);
			rptSetup_1.psmgBottom = Convert.ToInt32(nudpsmgBottom.Value);
			rptSetup_1.psmgInterval = Convert.ToInt32(nudpsmgInterval.Value);
			rptSetup_1.psSign = cbpsSign.Checked;
			rptSetup_1.lhUse = cblhUse.Checked;
			rptSetup_1.lhJstFstPage = cblhJstFstPage.Checked;
			rptSetup_1.lhBorder = cblhBorder.Checked;
			rptSetup_1.lhGrayBkgnd = cblhGrayBkgnd.Checked;
			rptSetup_1.LinesNum = (int)nudlhLinesNum.Value;
			for (int i = 0; i < gvlhLines.RowCount; i++)
			{
				rptSetup_1.lhLines[i] = gvlhLines.Rows[i].Cells[0].Value.ToString();
				LabHdrTag labHdrTag = (LabHdrTag)gvlhLines.Rows[i].Tag;
				rptSetup_1.lhLinesFt[i] = labHdrTag.font;
				rptSetup_1.lhLinesClr[i] = labHdrTag.color;
				rptSetup_1.lhLinesAlign[i] = labHdrTag.align;
			}
			rptSetup_1.lhUseImgLeft = cblhUseLeftImage.Checked;
			rptSetup_1.lhImgLeftName = (string)tblhLeftImage.Tag;
			if (rblhlOriginal.Checked)
			{
				rptSetup_1.lhImgLeftStyle = ImageLctStyle.Original;
			}
			if (rblhlHeader.Checked)
			{
				rptSetup_1.lhImgLeftStyle = ImageLctStyle.Header;
			}
			if (rblhlFixed.Checked)
			{
				rptSetup_1.lhImgLeftStyle = ImageLctStyle.Fixed;
			}
			rptSetup_1.lhImgLeftWidth = Class49.Object2Int(tblhlWidth.Text, rptSetup_1.lhImgLeftWidth);
			rptSetup_1.lhUseImgRight = cblhUseRightImage.Checked;
			rptSetup_1.lhImgRightName = (string)tblhRightImage.Tag;
			if (rblhrOriginal.Checked)
			{
				rptSetup_1.lhImgRightStyle = ImageLctStyle.Original;
			}
			if (rblhrHeader.Checked)
			{
				rptSetup_1.lhImgRightStyle = ImageLctStyle.Header;
			}
			if (rblhrFixed.Checked)
			{
				rptSetup_1.lhImgRightStyle = ImageLctStyle.Fixed;
			}
			rptSetup_1.lhImgRightWidth = Class49.Object2Int(tblhrWidth.Text, rptSetup_1.lhImgRightWidth);
			rptSetup_1.rhUse = cbrhUse.Checked;
			rptSetup_1.rhOnNewPage = cbrhOnNewPage.Checked;
			rptSetup_1.rhNamePathStyle = ((!rbrhnsAlways.Checked) ? NamePathStyle.OthersOnly : NamePathStyle.Always);
			rptSetup_1.rhNamePathStyle = (rbrhnsOthersOnly.Checked ? NamePathStyle.OthersOnly : NamePathStyle.Always);
			rptSetup_1.rhSystemInfo = cbrhSystemInfo.Checked;
			rptSetup_1.rhDateTime = cbrhDateTime.Checked;
			rptSetup_1.mtdUse = cbmtdUse.Checked;
			rptSetup_1.mtdOnNewPage = cbmtdOnNewPage.Checked;
			rptSetup_1.mtdAS = cbmtdAS.Checked;
			rptSetup_1.mtdGcProgTemp = cbmtdGcProgTemp.Checked;
			rptSetup_1.mtdGcPTGraph = cbmtdGcPTGraph.Checked;
			rptSetup_1.mtdLcGradient = cbmtdLcGradient.Checked;
			rptSetup_1.mtdLcGrdtGraph = cbmtdLcGrdtGraph.Checked;
			rptSetup_1.mtdLcGrdtItems = cbmtdLcGrdtItems.Checked;
			rptSetup_1.mtdAcquisition = cbmtdAcquisition.Checked;
			rptSetup_1.mtdIntegration = cbmtdIntegration.Checked;
			rptSetup_1.mtdciMeasurement = cbmtdciMeasurement.Checked;
			rptSetup_1.mtdciCalculation = cbmtdciCalculation.Checked;
			rptSetup_1.mtdciAdvance = cbmtdciAdvance.Checked;
			rptSetup_1.mtdciPDA = cbmtdciPDA.Checked;
			rptSetup_1.mtdciGpcRanges = cbmtdciGpcRanges.Checked;
			rptSetup_1.ciUse = cbciUse.Checked;
			rptSetup_1.ciOnNewPage = cbciOnNewPage.Checked;
			rptSetup_1.ciChooseStyle = ((!rbcichmAll.Checked) ? ChooseStyle.Active : ChooseStyle.All);
			rptSetup_1.ciciMeasurement = cbciciMeasurement.Checked;
			rptSetup_1.ciciIntegration = cbciciIntegration.Checked;
			rptSetup_1.ciciCalculation = cbciciCalculation.Checked;
			rptSetup_1.ciciAdvance = cbciciAdvance.Checked;
			rptSetup_1.ciciPDA = cbciciPDA.Checked;
			rptSetup_1.ciciGpcRanges = cbciciGpcRanges.Checked;
			rptSetup_1.cgUse = cbcgUse.Checked;
			rptSetup_1.cgOnNewPage = cbcgOnNewPage.Checked;
			rptSetup_1.cgChooseStyle = ((!rbcgchmAll.Checked) ? ChooseStyle.Active : ChooseStyle.All);
			rptSetup_1.cgGraphShowStyle = ((!rbcgssCombine.Checked) ? GraphShowStyle.Separate : GraphShowStyle.Combine);
			rptSetup_1.cgGraphDisStyle = ((!rbcgdsWhole.Checked) ? GraphDisStyle.Current : GraphDisStyle.Whole);
			rptSetup_1.crUse = cbcrUse.Checked;
			rptSetup_1.crOnNewPage = cbcrOnNewPage.Checked;
			rptSetup_1.crChooseStyle = ((!rbcrchmAll.Checked) ? ChooseStyle.Active : ChooseStyle.All);
			rptSetup_1.crResult = cbcrResult.Checked;
			rptSetup_1.crRltCombine = cbcrRltCombine.Checked;
			rptSetup_1.crSummary = cbcrSummary.Checked;
			rptSetup_1.crPerformance = cbcrPerformance.Checked;
			rptSetup_1.crSST = cbcrSST.Checked;
			rptSetup_1.crGpcSlices = cbcrGpcSlices.Checked;
			rptSetup_1.crGpcRanges = cbcrGpcRanges.Checked;
			rptSetup_1.clUse = cbclUse.Checked;
			rptSetup_1.clOnNewPage = cbclOnNewPage.Checked;
			rptSetup_1.clOptions = cbclOptions.Checked;
			rptSetup_1.clCmpds = cbclCmpds.Checked;
			rptSetup_1.clcdGnlLevels = cbclcdGnlLevels.Checked;
			rptSetup_1.clcdGnlGraph = cbclcdGnlGraph.Checked;
			rptSetup_1.sqUse = cbsqUse.Checked;
			rptSetup_1.sqOnNewPage = cbsqOnNewPage.Checked;
			rptSetup_1.sqOptions = cbsqOptions.Checked;
			rptSetup_1.sqInjList = cbsqInjList.Checked;
			break;
		}
		}
	}

	private void method_70()
	{
		Text = Lang.PS("报告设置", "Report Setup") + " [" + string_158 + "]";
	}

	private void method_71(int int_58)
	{
		Array.Resize(ref color_0, int_58);
		for (int i = 0; i < color_0.Length; i++)
		{
			color_0[i] = Color.Black;
		}
	}

	private void RptSetupDlg_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode == Keys.F1)
		{
			Class49.smethod_32("样式");
		}
	}

	private void RptSetupDlg_Load(object sender, EventArgs e)
	{
		Link();
		string_158 = "";
		method_70();
	}

	public void saveToPdf(bool refresh)
	{
	}

	public void SaveToPdf(string[] fileNames)
	{
		saveToPdf(refresh: false);
	}

	public DialogResult ShowDialog(RptSetup rptSetup)
	{
		method_69(AccStyle.Read, rptSetup);
		Control control = btnNew;
		Control control2 = btnOpen;
		Control control3 = btnSave;
		btnSaveAs.Visible = true;
		control3.Visible = true;
		control2.Visible = true;
		control.Visible = true;
		Control control4 = btnPreview;
		btnPrint.Visible = true;
		control4.Visible = true;
		DialogResult dialogResult = ShowDialog();
		if (dialogResult == DialogResult.OK)
		{
			method_69(AccStyle.Write, rptSetup);
			method_69(AccStyle.Write, rptSetup_0);
		}
		return dialogResult;
	}

	private bool method_72()
	{
		return false;
	}

	public RptSetupDlg(Instrument instrument)
	{
		InitializeComponent();
		cbpsSign.Text = Lang.PS("电子签名", "Electronic Sign");
		cbmtdGcProgTemp.Text = Lang.PS("程序升温", "Prog. Temp.");
		cbmtdGcPTGraph.Text = Lang.PS("升温图像", "P.T. Graph");
		cbcrSummary.Text = Lang.PS("总结表", "Summary");
		cbcrSST.Text = Lang.PS("组分验证", "Cmpd. Varify (SST)");
		cbcrRltCombine.Text = Lang.PS("整合输出结果", "Combine Results");
		cbcrPerformance.Text = Lang.PS("柱效", "Performance");
		lclLabel1.Text = Lang.PS("常规", "General");
		lclLabel2.Text = Lang.PS("凝胶", "GPC");
		lclLabel3.Text = Lang.PS("阵列", "DAD");
		btnSaveToPdf.Text = Lang.PS("存为Pdf", "Saveto Pdf");
		cblhUseLeftImage.Tag = gblhUseLeftImage;
		cblhUseRightImage.Tag = gblhUseRightImage;
		gblhUseLeftImage.Left = cblhUseLeftImage.Left - 10;
		gblhUseRightImage.Left = cblhUseRightImage.Left - 10;
		gblhUseLeftImage.Top = cblhUseLeftImage.Top;
		gblhUseRightImage.Top = cblhUseRightImage.Top;
		base.instrument = instrument;
		gvlhLines.Columns[0].Width = gvlhLines.Width - 2;
		tcRptSetup.ImageList = SystemImageListResource1.smethod_0();
		prtPrvDlg.Width = 660;
		prtPrvDlg.Height = 700;
		LoadLanguage();
	}

	public RptSetupDlg()
	{
		InitializeComponent();
		cbpsSign.Text = Lang.PS("电子签名", "Electronic Sign");
		cbmtdGcProgTemp.Text = Lang.PS("程序升温", "Prog. Temp.");
		cbmtdGcPTGraph.Text = Lang.PS("升温图像", "P.T. Graph");
		cbcrSummary.Text = Lang.PS("总结表", "Summary");
		cbcrSST.Text = Lang.PS("组分验证", "Cmpd. Varify (SST)");
		cbcrRltCombine.Text = Lang.PS("整合输出结果", "Combine Results");
		cbcrPerformance.Text = Lang.PS("柱效", "Performance");
		lclLabel1.Text = Lang.PS("常规", "General");
		lclLabel2.Text = Lang.PS("凝胶", "GPC");
		lclLabel3.Text = Lang.PS("阵列", "DAD");
		btnSaveToPdf.Text = Lang.PS("存为Pdf", "Saveto Pdf");
		cblhUseLeftImage.Tag = gblhUseLeftImage;
		cblhUseRightImage.Tag = gblhUseRightImage;
		gblhUseLeftImage.Left = cblhUseLeftImage.Left - 10;
		gblhUseRightImage.Left = cblhUseRightImage.Left - 10;
		gblhUseLeftImage.Top = cblhUseLeftImage.Top;
		gblhUseRightImage.Top = cblhUseRightImage.Top;
		gvlhLines.Columns[0].Width = gvlhLines.Width - 2;
		tcRptSetup.ImageList = SystemImageListResource1.smethod_0();
		prtPrvDlg.Width = 660;
		prtPrvDlg.Height = 700;
		LoadLanguage();
	}

	protected override void Dispose(bool disposing)
	{
		if (disposing && icontainer_1 != null)
		{
			icontainer_1.Dispose();
		}
		base.Dispose(disposing);
	}

	private void InitializeComponent()
	{
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(IBrainChrom2018.RptSetupDlg));
		this.tcRptSetup = new IBrainChrom2018.LclTabControl();
		this.tpPageSetup = new System.Windows.Forms.TabPage();
		this.fbtnpsValue = new IBrainChrom2018.LclFontBtn();
		this.fbtnpsItem = new IBrainChrom2018.LclFontBtn();
		this.gbpsMargins = new IBrainChrom2018.LclGroupBox();
		this.nudpsmgInterval = new IBrainChrom2018.LclNumericUpDown();
		this.lbpsmgInterval = new IBrainChrom2018.LclLabel();
		this.nudpsmgRight = new IBrainChrom2018.LclNumericUpDown();
		this.lbpsmgRight = new IBrainChrom2018.LclLabel();
		this.nudpsmgBottom = new IBrainChrom2018.LclNumericUpDown();
		this.lbpsmgBottom = new IBrainChrom2018.LclLabel();
		this.nudpsmgTop = new IBrainChrom2018.LclNumericUpDown();
		this.lbpsmgTop = new IBrainChrom2018.LclLabel();
		this.nudpsmgLeft = new IBrainChrom2018.LclNumericUpDown();
		this.lbpsmgLeft = new IBrainChrom2018.LclLabel();
		this.cbpsSign = new IBrainChrom2018.LclCheckBox();
		this.cbpsDrawGraphsBkgnd = new IBrainChrom2018.LclCheckBox();
		this.cbpsColors = new IBrainChrom2018.LclCheckBox();
		this.tpLabHeader = new System.Windows.Forms.TabPage();
		this.cblhUseRightImage = new IBrainChrom2018.LclCheckBox();
		this.gblhLines = new IBrainChrom2018.LclGroupBox();
		this.rblhR = new IBrainChrom2018.LclRadioButton();
		this.rblhM = new IBrainChrom2018.LclRadioButton();
		this.rblhL = new IBrainChrom2018.LclRadioButton();
		this.gvlhLines = new IBrainChrom2018.LclRptLabHeaderGV();
		this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.btnlhFont = new IBrainChrom2018.LclButton();
		this.lblhLinesNum = new IBrainChrom2018.LclLabel();
		this.nudlhLinesNum = new IBrainChrom2018.LclNumericUpDown();
		this.cblhUseLeftImage = new IBrainChrom2018.LclCheckBox();
		this.gblhUseRightImage = new IBrainChrom2018.LclGroupBox();
		this.tblhrWidth = new IBrainChrom2018.LclTextBox();
		this.rblhrFixed = new IBrainChrom2018.LclRadioButton();
		this.tblhRightImage = new IBrainChrom2018.LclTextBox();
		this.rblhrHeader = new IBrainChrom2018.LclRadioButton();
		this.rblhrOriginal = new IBrainChrom2018.LclRadioButton();
		this.btnlhRightImage = new IBrainChrom2018.LclButton();
		this.cblhGrayBkgnd = new IBrainChrom2018.LclCheckBox();
		this.cblhBorder = new IBrainChrom2018.LclCheckBox();
		this.cblhJstFstPage = new IBrainChrom2018.LclCheckBox();
		this.cblhUse = new IBrainChrom2018.LclCheckBox();
		this.gblhUseLeftImage = new IBrainChrom2018.LclGroupBox();
		this.tblhlWidth = new IBrainChrom2018.LclTextBox();
		this.rblhlFixed = new IBrainChrom2018.LclRadioButton();
		this.rblhlHeader = new IBrainChrom2018.LclRadioButton();
		this.tblhLeftImage = new IBrainChrom2018.LclTextBox();
		this.rblhlOriginal = new IBrainChrom2018.LclRadioButton();
		this.btnlhLeftImage = new IBrainChrom2018.LclButton();
		this.tpRptHeader = new System.Windows.Forms.TabPage();
		this.gbrhPrintFullPath = new IBrainChrom2018.LclGroupBox();
		this.rbrhnsOthersOnly = new IBrainChrom2018.LclRadioButton();
		this.rbrhnsAlways = new IBrainChrom2018.LclRadioButton();
		this.cbrhDateTime = new IBrainChrom2018.LclCheckBox();
		this.cbrhSystemInfo = new IBrainChrom2018.LclCheckBox();
		this.cbrhUse = new IBrainChrom2018.LclCheckBox();
		this.cbrhOnNewPage = new IBrainChrom2018.LclCheckBox();
		this.tpMethod = new System.Windows.Forms.TabPage();
		this.gbmtdChromInfo = new IBrainChrom2018.LclGroupBox();
		this.cbmtdciMeasurement = new IBrainChrom2018.LclCheckBox();
		this.cbmtdciCalculation = new IBrainChrom2018.LclCheckBox();
		this.cbmtdciAdvance = new IBrainChrom2018.LclCheckBox();
		this.cbmtdciGpcRanges = new IBrainChrom2018.LclCheckBox();
		this.cbmtdciPDA = new IBrainChrom2018.LclCheckBox();
		this.cbmtdAS = new IBrainChrom2018.LclCheckBox();
		this.cbmtdUse = new IBrainChrom2018.LclCheckBox();
		this.cbmtdIntegration = new IBrainChrom2018.LclCheckBox();
		this.cbmtdAcquisition = new IBrainChrom2018.LclCheckBox();
		this.cbmtdLcGrdtItems = new IBrainChrom2018.LclCheckBox();
		this.cbmtdGcPTGraph = new IBrainChrom2018.LclCheckBox();
		this.cbmtdLcGrdtGraph = new IBrainChrom2018.LclCheckBox();
		this.cbmtdGcProgTemp = new IBrainChrom2018.LclCheckBox();
		this.cbmtdLcGradient = new IBrainChrom2018.LclCheckBox();
		this.cbmtdOnNewPage = new IBrainChrom2018.LclCheckBox();
		this.tpChromInfo = new System.Windows.Forms.TabPage();
		this.cbciUse = new IBrainChrom2018.LclCheckBox();
		this.cbciOnNewPage = new IBrainChrom2018.LclCheckBox();
		this.gbciChromInfo = new IBrainChrom2018.LclGroupBox();
		this.cbciciIntegration = new IBrainChrom2018.LclCheckBox();
		this.cbciciMeasurement = new IBrainChrom2018.LclCheckBox();
		this.cbciciCalculation = new IBrainChrom2018.LclCheckBox();
		this.cbciciAdvance = new IBrainChrom2018.LclCheckBox();
		this.cbciciGpcRanges = new IBrainChrom2018.LclCheckBox();
		this.cbciciPDA = new IBrainChrom2018.LclCheckBox();
		this.gbciChroms = new IBrainChrom2018.LclGroupBox();
		this.rbcichmActive = new IBrainChrom2018.LclRadioButton();
		this.rbcichmAll = new IBrainChrom2018.LclRadioButton();
		this.tpChromGraph = new System.Windows.Forms.TabPage();
		this.gbcgDisStyle = new IBrainChrom2018.LclGroupBox();
		this.rbcgdsCurrent = new IBrainChrom2018.LclRadioButton();
		this.rbcgdsWhole = new IBrainChrom2018.LclRadioButton();
		this.gbcgShowStyle = new IBrainChrom2018.LclGroupBox();
		this.rbcgssSeparate = new IBrainChrom2018.LclRadioButton();
		this.rbcgssCombine = new IBrainChrom2018.LclRadioButton();
		this.gbcgChroms = new IBrainChrom2018.LclGroupBox();
		this.rbcgchmActive = new IBrainChrom2018.LclRadioButton();
		this.rbcgchmAll = new IBrainChrom2018.LclRadioButton();
		this.cbcgUse = new IBrainChrom2018.LclCheckBox();
		this.cbcgOnNewPage = new IBrainChrom2018.LclCheckBox();
		this.tpChromRlts = new System.Windows.Forms.TabPage();
		this.gbcrChroms = new IBrainChrom2018.LclGroupBox();
		this.rbcrchmActive = new IBrainChrom2018.LclRadioButton();
		this.rbcrchmAll = new IBrainChrom2018.LclRadioButton();
		this.cbcrRltCombine = new IBrainChrom2018.LclCheckBox();
		this.cbcrResult = new IBrainChrom2018.LclCheckBox();
		this.cbcrUse = new IBrainChrom2018.LclCheckBox();
		this.cbcrGpcRanges = new IBrainChrom2018.LclCheckBox();
		this.cbcrGpcSlices = new IBrainChrom2018.LclCheckBox();
		this.cbcrSST = new IBrainChrom2018.LclCheckBox();
		this.cbcrPerformance = new IBrainChrom2018.LclCheckBox();
		this.cbcrSummary = new IBrainChrom2018.LclCheckBox();
		this.cbcrOnNewPage = new IBrainChrom2018.LclCheckBox();
		this.tpCali = new System.Windows.Forms.TabPage();
		this.gbclCmpd = new IBrainChrom2018.LclGroupBox();
		this.lclLabel3 = new IBrainChrom2018.LclLabel();
		this.lclLabel2 = new IBrainChrom2018.LclLabel();
		this.lclLabel1 = new IBrainChrom2018.LclLabel();
		this.cbclcdGnlGraph = new IBrainChrom2018.LclCheckBox();
		this.cbclcdGnlLevels = new IBrainChrom2018.LclCheckBox();
		this.cbclUse = new IBrainChrom2018.LclCheckBox();
		this.cbclCmpds = new IBrainChrom2018.LclCheckBox();
		this.cbclOptions = new IBrainChrom2018.LclCheckBox();
		this.cbclOnNewPage = new IBrainChrom2018.LclCheckBox();
		this.tpSeq = new System.Windows.Forms.TabPage();
		this.cbsqOptions = new IBrainChrom2018.LclCheckBox();
		this.cbsqUse = new IBrainChrom2018.LclCheckBox();
		this.cbsqInjList = new IBrainChrom2018.LclCheckBox();
		this.cbsqOnNewPage = new IBrainChrom2018.LclCheckBox();
		this.btnNew = new IBrainChrom2018.LclButton();
		this.btnOpen = new IBrainChrom2018.LclButton();
		this.btnSave = new IBrainChrom2018.LclButton();
		this.btnSaveAs = new IBrainChrom2018.LclButton();
		this.btnPrint = new IBrainChrom2018.LclButton();
		this.btnPreview = new IBrainChrom2018.LclButton();
		this.printDocument_0 = new System.Drawing.Printing.PrintDocument();
		this.printDialog_0 = new System.Windows.Forms.PrintDialog();
		this.prtPrvDlg = new System.Windows.Forms.PrintPreviewDialog();
		this.btnSaveToPdf = new IBrainChrom2018.LclButton();
		this.tcRptSetup.SuspendLayout();
		this.tpPageSetup.SuspendLayout();
		this.gbpsMargins.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.nudpsmgInterval).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.nudpsmgRight).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.nudpsmgBottom).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.nudpsmgTop).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.nudpsmgLeft).BeginInit();
		this.tpLabHeader.SuspendLayout();
		this.gblhLines.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.gvlhLines).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.nudlhLinesNum).BeginInit();
		this.gblhUseRightImage.SuspendLayout();
		this.gblhUseLeftImage.SuspendLayout();
		this.tpRptHeader.SuspendLayout();
		this.gbrhPrintFullPath.SuspendLayout();
		this.tpMethod.SuspendLayout();
		this.gbmtdChromInfo.SuspendLayout();
		this.tpChromInfo.SuspendLayout();
		this.gbciChromInfo.SuspendLayout();
		this.gbciChroms.SuspendLayout();
		this.tpChromGraph.SuspendLayout();
		this.gbcgDisStyle.SuspendLayout();
		this.gbcgShowStyle.SuspendLayout();
		this.gbcgChroms.SuspendLayout();
		this.tpChromRlts.SuspendLayout();
		this.gbcrChroms.SuspendLayout();
		this.tpCali.SuspendLayout();
		this.gbclCmpd.SuspendLayout();
		this.tpSeq.SuspendLayout();
		base.SuspendLayout();
		base.btnCancel.Location = new System.Drawing.Point(428, 31);
		base.btnCancel.Text = "取消";
		base.btnHelp.Location = new System.Drawing.Point(428, 56);
		base.btnHelp.Text = "帮助";
		base.btnOK.Location = new System.Drawing.Point(428, 6);
		base.btnOK.Text = "确认";
		this.tcRptSetup.Alignment = System.Windows.Forms.TabAlignment.Left;
		this.tcRptSetup.Controls.Add(this.tpPageSetup);
		this.tcRptSetup.Controls.Add(this.tpLabHeader);
		this.tcRptSetup.Controls.Add(this.tpRptHeader);
		this.tcRptSetup.Controls.Add(this.tpMethod);
		this.tcRptSetup.Controls.Add(this.tpChromInfo);
		this.tcRptSetup.Controls.Add(this.tpChromGraph);
		this.tcRptSetup.Controls.Add(this.tpChromRlts);
		this.tcRptSetup.Controls.Add(this.tpCali);
		this.tcRptSetup.Controls.Add(this.tpSeq);
		this.tcRptSetup.ItemSize = new System.Drawing.Size(23, 85);
		this.tcRptSetup.Location = new System.Drawing.Point(4, 6);
		this.tcRptSetup.Multiline = true;
		this.tcRptSetup.Name = "tcRptSetup";
		this.tcRptSetup.SelectedIndex = 0;
		this.tcRptSetup.Size = new System.Drawing.Size(421, 262);
		this.tcRptSetup.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
		this.tcRptSetup.TabIndex = 1;
		this.tpPageSetup.Controls.Add(this.fbtnpsValue);
		this.tpPageSetup.Controls.Add(this.fbtnpsItem);
		this.tpPageSetup.Controls.Add(this.gbpsMargins);
		this.tpPageSetup.Controls.Add(this.cbpsSign);
		this.tpPageSetup.Controls.Add(this.cbpsDrawGraphsBkgnd);
		this.tpPageSetup.Controls.Add(this.cbpsColors);
		this.tpPageSetup.Location = new System.Drawing.Point(89, 4);
		this.tpPageSetup.Name = "tpPageSetup";
		this.tpPageSetup.Size = new System.Drawing.Size(328, 254);
		this.tpPageSetup.TabIndex = 0;
		this.tpPageSetup.Text = "页面设置";
		this.tpPageSetup.UseVisualStyleBackColor = true;
		this.fbtnpsValue.Location = new System.Drawing.Point(130, 73);
		this.fbtnpsValue.Name = "fbtnpsValue";
		this.fbtnpsValue.Size = new System.Drawing.Size(99, 32);
		this.fbtnpsValue.TabIndex = 4;
		this.fbtnpsValue.Text = "值 字体";
		this.fbtnpsValue.UseVisualStyleBackColor = true;
		this.fbtnpsItem.Location = new System.Drawing.Point(12, 73);
		this.fbtnpsItem.Name = "fbtnpsItem";
		this.fbtnpsItem.Size = new System.Drawing.Size(99, 32);
		this.fbtnpsItem.TabIndex = 4;
		this.fbtnpsItem.Text = "项目字体";
		this.fbtnpsItem.UseVisualStyleBackColor = true;
		this.gbpsMargins.Controls.Add(this.nudpsmgInterval);
		this.gbpsMargins.Controls.Add(this.lbpsmgInterval);
		this.gbpsMargins.Controls.Add(this.nudpsmgRight);
		this.gbpsMargins.Controls.Add(this.lbpsmgRight);
		this.gbpsMargins.Controls.Add(this.nudpsmgBottom);
		this.gbpsMargins.Controls.Add(this.lbpsmgBottom);
		this.gbpsMargins.Controls.Add(this.nudpsmgTop);
		this.gbpsMargins.Controls.Add(this.lbpsmgTop);
		this.gbpsMargins.Controls.Add(this.nudpsmgLeft);
		this.gbpsMargins.Controls.Add(this.lbpsmgLeft);
		this.gbpsMargins.Location = new System.Drawing.Point(12, 111);
		this.gbpsMargins.Name = "gbpsMargins";
		this.gbpsMargins.Size = new System.Drawing.Size(306, 100);
		this.gbpsMargins.TabIndex = 3;
		this.gbpsMargins.TabStop = false;
		this.gbpsMargins.Text = "边界";
		this.nudpsmgInterval.Location = new System.Drawing.Point(143, 45);
		this.nudpsmgInterval.Maximum = new decimal(new int[4] { 1, 0, 0, 0 });
		this.nudpsmgInterval.Name = "nudpsmgInterval";
		this.nudpsmgInterval.Size = new System.Drawing.Size(55, 21);
		this.nudpsmgInterval.TabIndex = 1;
		this.lbpsmgInterval.AutoSize = true;
		this.lbpsmgInterval.Location = new System.Drawing.Point(105, 50);
		this.lbpsmgInterval.Name = "lbpsmgInterval";
		this.lbpsmgInterval.Size = new System.Drawing.Size(29, 12);
		this.lbpsmgInterval.TabIndex = 0;
		this.lbpsmgInterval.Text = "间距";
		this.nudpsmgRight.Location = new System.Drawing.Point(244, 45);
		this.nudpsmgRight.Name = "nudpsmgRight";
		this.nudpsmgRight.Size = new System.Drawing.Size(55, 21);
		this.nudpsmgRight.TabIndex = 1;
		this.lbpsmgRight.AutoSize = true;
		this.lbpsmgRight.Location = new System.Drawing.Point(205, 50);
		this.lbpsmgRight.Name = "lbpsmgRight";
		this.lbpsmgRight.Size = new System.Drawing.Size(17, 12);
		this.lbpsmgRight.TabIndex = 0;
		this.lbpsmgRight.Text = "右";
		this.nudpsmgBottom.Location = new System.Drawing.Point(143, 72);
		this.nudpsmgBottom.Name = "nudpsmgBottom";
		this.nudpsmgBottom.Size = new System.Drawing.Size(55, 21);
		this.nudpsmgBottom.TabIndex = 1;
		this.lbpsmgBottom.AutoSize = true;
		this.lbpsmgBottom.Location = new System.Drawing.Point(105, 77);
		this.lbpsmgBottom.Name = "lbpsmgBottom";
		this.lbpsmgBottom.Size = new System.Drawing.Size(17, 12);
		this.lbpsmgBottom.TabIndex = 0;
		this.lbpsmgBottom.Text = "底";
		this.nudpsmgTop.Location = new System.Drawing.Point(143, 18);
		this.nudpsmgTop.Name = "nudpsmgTop";
		this.nudpsmgTop.Size = new System.Drawing.Size(55, 21);
		this.nudpsmgTop.TabIndex = 1;
		this.lbpsmgTop.AutoSize = true;
		this.lbpsmgTop.Location = new System.Drawing.Point(105, 23);
		this.lbpsmgTop.Name = "lbpsmgTop";
		this.lbpsmgTop.Size = new System.Drawing.Size(17, 12);
		this.lbpsmgTop.TabIndex = 0;
		this.lbpsmgTop.Text = "顶";
		this.nudpsmgLeft.Location = new System.Drawing.Point(44, 45);
		this.nudpsmgLeft.Name = "nudpsmgLeft";
		this.nudpsmgLeft.Size = new System.Drawing.Size(55, 21);
		this.nudpsmgLeft.TabIndex = 1;
		this.lbpsmgLeft.AutoSize = true;
		this.lbpsmgLeft.Location = new System.Drawing.Point(6, 50);
		this.lbpsmgLeft.Name = "lbpsmgLeft";
		this.lbpsmgLeft.Size = new System.Drawing.Size(17, 12);
		this.lbpsmgLeft.TabIndex = 0;
		this.lbpsmgLeft.Text = "左";
		this.cbpsSign.AutoSize = true;
		this.cbpsSign.Location = new System.Drawing.Point(12, 216);
		this.cbpsSign.Name = "cbpsSign";
		this.cbpsSign.Size = new System.Drawing.Size(72, 16);
		this.cbpsSign.TabIndex = 0;
		this.cbpsSign.Text = "电子签名";
		this.cbpsSign.UseVisualStyleBackColor = true;
		this.cbpsDrawGraphsBkgnd.AutoSize = true;
		this.cbpsDrawGraphsBkgnd.Location = new System.Drawing.Point(12, 28);
		this.cbpsDrawGraphsBkgnd.Name = "cbpsDrawGraphsBkgnd";
		this.cbpsDrawGraphsBkgnd.Size = new System.Drawing.Size(96, 16);
		this.cbpsDrawGraphsBkgnd.TabIndex = 0;
		this.cbpsDrawGraphsBkgnd.Text = "绘制谱图背景";
		this.cbpsDrawGraphsBkgnd.UseVisualStyleBackColor = true;
		this.cbpsColors.AutoSize = true;
		this.cbpsColors.Location = new System.Drawing.Point(12, 6);
		this.cbpsColors.Name = "cbpsColors";
		this.cbpsColors.Size = new System.Drawing.Size(48, 16);
		this.cbpsColors.TabIndex = 0;
		this.cbpsColors.Text = "彩色";
		this.cbpsColors.UseVisualStyleBackColor = true;
		this.cbpsColors.CheckedChanged += new System.EventHandler(cbpsColors_CheckedChanged);
		this.tpLabHeader.Controls.Add(this.cblhUseRightImage);
		this.tpLabHeader.Controls.Add(this.gblhLines);
		this.tpLabHeader.Controls.Add(this.cblhUseLeftImage);
		this.tpLabHeader.Controls.Add(this.gblhUseRightImage);
		this.tpLabHeader.Controls.Add(this.cblhGrayBkgnd);
		this.tpLabHeader.Controls.Add(this.cblhBorder);
		this.tpLabHeader.Controls.Add(this.cblhJstFstPage);
		this.tpLabHeader.Controls.Add(this.cblhUse);
		this.tpLabHeader.Controls.Add(this.gblhUseLeftImage);
		this.tpLabHeader.Location = new System.Drawing.Point(89, 4);
		this.tpLabHeader.Name = "tpLabHeader";
		this.tpLabHeader.Size = new System.Drawing.Size(328, 254);
		this.tpLabHeader.TabIndex = 1;
		this.tpLabHeader.Text = "实验标头";
		this.tpLabHeader.UseVisualStyleBackColor = true;
		this.cblhUseRightImage.AutoSize = true;
		this.cblhUseRightImage.Location = new System.Drawing.Point(115, 84);
		this.cblhUseRightImage.Name = "cblhUseRightImage";
		this.cblhUseRightImage.Size = new System.Drawing.Size(72, 16);
		this.cblhUseRightImage.TabIndex = 1;
		this.cblhUseRightImage.Text = "右方图像";
		this.cblhUseRightImage.UseVisualStyleBackColor = true;
		this.cblhUseRightImage.CheckedChanged += new System.EventHandler(cblhUseLeftImage_CheckedChanged);
		this.gblhLines.Controls.Add(this.rblhR);
		this.gblhLines.Controls.Add(this.rblhM);
		this.gblhLines.Controls.Add(this.rblhL);
		this.gblhLines.Controls.Add(this.gvlhLines);
		this.gblhLines.Controls.Add(this.btnlhFont);
		this.gblhLines.Controls.Add(this.lblhLinesNum);
		this.gblhLines.Controls.Add(this.nudlhLinesNum);
		this.gblhLines.Location = new System.Drawing.Point(4, 136);
		this.gblhLines.Name = "gblhLines";
		this.gblhLines.Size = new System.Drawing.Size(322, 117);
		this.gblhLines.TabIndex = 12;
		this.gblhLines.TabStop = false;
		this.gblhLines.Text = "lclGroupBox1";
		this.rblhR.AutoSize = true;
		this.rblhR.Location = new System.Drawing.Point(210, 17);
		this.rblhR.Name = "rblhR";
		this.rblhR.Size = new System.Drawing.Size(35, 16);
		this.rblhR.TabIndex = 4;
		this.rblhR.TabStop = true;
		this.rblhR.Text = "右";
		this.rblhR.UseVisualStyleBackColor = true;
		this.rblhR.Click += new System.EventHandler(rblhL_Click);
		this.rblhM.AutoSize = true;
		this.rblhM.Location = new System.Drawing.Point(162, 17);
		this.rblhM.Name = "rblhM";
		this.rblhM.Size = new System.Drawing.Size(35, 16);
		this.rblhM.TabIndex = 4;
		this.rblhM.TabStop = true;
		this.rblhM.Text = "中";
		this.rblhM.UseVisualStyleBackColor = true;
		this.rblhM.Click += new System.EventHandler(rblhL_Click);
		this.rblhL.AutoSize = true;
		this.rblhL.Location = new System.Drawing.Point(114, 17);
		this.rblhL.Name = "rblhL";
		this.rblhL.Size = new System.Drawing.Size(35, 16);
		this.rblhL.TabIndex = 4;
		this.rblhL.TabStop = true;
		this.rblhL.Text = "左";
		this.rblhL.UseVisualStyleBackColor = true;
		this.rblhL.Click += new System.EventHandler(rblhL_Click);
		this.gvlhLines.AllowUserToAddRows = false;
		this.gvlhLines.AllowUserToResizeRows = false;
		this.gvlhLines.BackgroundColor = System.Drawing.Color.White;
		this.gvlhLines.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
		this.gvlhLines.ColumnHeadersVisible = false;
		this.gvlhLines.Columns.AddRange(this.Column1);
		this.gvlhLines.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.gvlhLines.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
		this.gvlhLines.Location = new System.Drawing.Point(3, 39);
		this.gvlhLines.Name = "gvlhLines";
		this.gvlhLines.RowHeadersVisible = false;
		this.gvlhLines.RowTemplate.Height = 23;
		this.gvlhLines.Size = new System.Drawing.Size(316, 75);
		this.gvlhLines.TabIndex = 7;
		this.gvlhLines.SelectionChanged += new System.EventHandler(gvlhLines_SelectionChanged);
		this.Column1.Frozen = true;
		this.Column1.HeaderText = "Column1";
		this.Column1.Name = "Column1";
		this.Column1.Width = 21;
		this.btnlhFont.Location = new System.Drawing.Point(273, 14);
		this.btnlhFont.Name = "btnlhFont";
		this.btnlhFont.Size = new System.Drawing.Size(38, 23);
		this.btnlhFont.TabIndex = 3;
		this.btnlhFont.Text = "A";
		this.btnlhFont.UseVisualStyleBackColor = true;
		this.btnlhFont.Click += new System.EventHandler(btnlhFont_Click);
		this.lblhLinesNum.AutoSize = true;
		this.lblhLinesNum.Location = new System.Drawing.Point(10, 21);
		this.lblhLinesNum.Name = "lblhLinesNum";
		this.lblhLinesNum.Size = new System.Drawing.Size(29, 12);
		this.lblhLinesNum.TabIndex = 5;
		this.lblhLinesNum.Text = "行数";
		this.nudlhLinesNum.Location = new System.Drawing.Point(58, 15);
		this.nudlhLinesNum.Maximum = new decimal(new int[4] { 1, 0, 0, 0 });
		this.nudlhLinesNum.Minimum = new decimal(new int[4] { 1, 0, 0, 0 });
		this.nudlhLinesNum.Name = "nudlhLinesNum";
		this.nudlhLinesNum.Size = new System.Drawing.Size(41, 21);
		this.nudlhLinesNum.TabIndex = 6;
		this.nudlhLinesNum.Value = new decimal(new int[4] { 1, 0, 0, 0 });
		this.nudlhLinesNum.ValueChanged += new System.EventHandler(nudlhLinesNum_ValueChanged);
		this.cblhUseLeftImage.AutoSize = true;
		this.cblhUseLeftImage.Location = new System.Drawing.Point(115, 6);
		this.cblhUseLeftImage.Name = "cblhUseLeftImage";
		this.cblhUseLeftImage.Size = new System.Drawing.Size(72, 16);
		this.cblhUseLeftImage.TabIndex = 1;
		this.cblhUseLeftImage.Text = "左方图像";
		this.cblhUseLeftImage.UseVisualStyleBackColor = true;
		this.cblhUseLeftImage.CheckedChanged += new System.EventHandler(cblhUseLeftImage_CheckedChanged);
		this.gblhUseRightImage.Controls.Add(this.tblhrWidth);
		this.gblhUseRightImage.Controls.Add(this.rblhrFixed);
		this.gblhUseRightImage.Controls.Add(this.tblhRightImage);
		this.gblhUseRightImage.Controls.Add(this.rblhrHeader);
		this.gblhUseRightImage.Controls.Add(this.rblhrOriginal);
		this.gblhUseRightImage.Controls.Add(this.btnlhRightImage);
		this.gblhUseRightImage.Location = new System.Drawing.Point(126, 92);
		this.gblhUseRightImage.Name = "gblhUseRightImage";
		this.gblhUseRightImage.Size = new System.Drawing.Size(217, 61);
		this.gblhUseRightImage.TabIndex = 11;
		this.gblhUseRightImage.TabStop = false;
		this.gblhUseRightImage.Text = "              ";
		this.tblhrWidth.Location = new System.Drawing.Point(177, 40);
		this.tblhrWidth.Name = "tblhrWidth";
		this.tblhrWidth.Size = new System.Drawing.Size(35, 21);
		this.tblhrWidth.TabIndex = 2;
		this.rblhrFixed.AutoSize = true;
		this.rblhrFixed.Location = new System.Drawing.Point(119, 41);
		this.rblhrFixed.Name = "rblhrFixed";
		this.rblhrFixed.Size = new System.Drawing.Size(47, 16);
		this.rblhrFixed.TabIndex = 4;
		this.rblhrFixed.TabStop = true;
		this.rblhrFixed.Text = "固定";
		this.rblhrFixed.UseVisualStyleBackColor = true;
		this.tblhRightImage.Location = new System.Drawing.Point(12, 17);
		this.tblhRightImage.Name = "tblhRightImage";
		this.tblhRightImage.ReadOnly = true;
		this.tblhRightImage.Size = new System.Drawing.Size(154, 21);
		this.tblhRightImage.TabIndex = 2;
		this.rblhrHeader.AutoSize = true;
		this.rblhrHeader.Location = new System.Drawing.Point(65, 41);
		this.rblhrHeader.Name = "rblhrHeader";
		this.rblhrHeader.Size = new System.Drawing.Size(47, 16);
		this.rblhrHeader.TabIndex = 4;
		this.rblhrHeader.TabStop = true;
		this.rblhrHeader.Text = "自动";
		this.rblhrHeader.UseVisualStyleBackColor = true;
		this.rblhrOriginal.AutoSize = true;
		this.rblhrOriginal.Location = new System.Drawing.Point(12, 41);
		this.rblhrOriginal.Name = "rblhrOriginal";
		this.rblhrOriginal.Size = new System.Drawing.Size(47, 16);
		this.rblhrOriginal.TabIndex = 4;
		this.rblhrOriginal.TabStop = true;
		this.rblhrOriginal.Text = "原始";
		this.rblhrOriginal.UseVisualStyleBackColor = true;
		this.btnlhRightImage.Location = new System.Drawing.Point(171, 16);
		this.btnlhRightImage.Name = "btnlhRightImage";
		this.btnlhRightImage.Size = new System.Drawing.Size(38, 22);
		this.btnlhRightImage.TabIndex = 3;
		this.btnlhRightImage.Text = "...";
		this.btnlhRightImage.UseVisualStyleBackColor = true;
		this.btnlhRightImage.Click += new System.EventHandler(btnlhLeftImage_Click);
		this.cblhGrayBkgnd.AutoSize = true;
		this.cblhGrayBkgnd.Location = new System.Drawing.Point(12, 70);
		this.cblhGrayBkgnd.Name = "cblhGrayBkgnd";
		this.cblhGrayBkgnd.Size = new System.Drawing.Size(72, 16);
		this.cblhGrayBkgnd.TabIndex = 1;
		this.cblhGrayBkgnd.Text = "灰色背景";
		this.cblhGrayBkgnd.UseVisualStyleBackColor = true;
		this.cblhGrayBkgnd.CheckedChanged += new System.EventHandler(cblhGrayBkgnd_CheckedChanged);
		this.cblhBorder.AutoSize = true;
		this.cblhBorder.Location = new System.Drawing.Point(12, 48);
		this.cblhBorder.Name = "cblhBorder";
		this.cblhBorder.Size = new System.Drawing.Size(72, 16);
		this.cblhBorder.TabIndex = 1;
		this.cblhBorder.Text = "绘制边框";
		this.cblhBorder.UseVisualStyleBackColor = true;
		this.cblhJstFstPage.AutoSize = true;
		this.cblhJstFstPage.Location = new System.Drawing.Point(12, 27);
		this.cblhJstFstPage.Name = "cblhJstFstPage";
		this.cblhJstFstPage.Size = new System.Drawing.Size(60, 16);
		this.cblhJstFstPage.TabIndex = 1;
		this.cblhJstFstPage.Text = "仅首页";
		this.cblhJstFstPage.UseVisualStyleBackColor = true;
		this.cblhUse.AutoSize = true;
		this.cblhUse.Location = new System.Drawing.Point(12, 6);
		this.cblhUse.Name = "cblhUse";
		this.cblhUse.Size = new System.Drawing.Size(48, 16);
		this.cblhUse.TabIndex = 1;
		this.cblhUse.Text = "使用";
		this.cblhUse.UseVisualStyleBackColor = true;
		this.cblhUse.CheckedChanged += new System.EventHandler(cbsqUse_CheckedChanged);
		this.gblhUseLeftImage.Controls.Add(this.tblhlWidth);
		this.gblhUseLeftImage.Controls.Add(this.rblhlFixed);
		this.gblhUseLeftImage.Controls.Add(this.rblhlHeader);
		this.gblhUseLeftImage.Controls.Add(this.tblhLeftImage);
		this.gblhUseLeftImage.Controls.Add(this.rblhlOriginal);
		this.gblhUseLeftImage.Controls.Add(this.btnlhLeftImage);
		this.gblhUseLeftImage.Location = new System.Drawing.Point(126, 27);
		this.gblhUseLeftImage.Name = "gblhUseLeftImage";
		this.gblhUseLeftImage.Size = new System.Drawing.Size(217, 61);
		this.gblhUseLeftImage.TabIndex = 10;
		this.gblhUseLeftImage.TabStop = false;
		this.gblhUseLeftImage.Text = "              ";
		this.tblhlWidth.Location = new System.Drawing.Point(177, 40);
		this.tblhlWidth.Name = "tblhlWidth";
		this.tblhlWidth.Size = new System.Drawing.Size(35, 21);
		this.tblhlWidth.TabIndex = 2;
		this.rblhlFixed.AutoSize = true;
		this.rblhlFixed.Location = new System.Drawing.Point(119, 41);
		this.rblhlFixed.Name = "rblhlFixed";
		this.rblhlFixed.Size = new System.Drawing.Size(47, 16);
		this.rblhlFixed.TabIndex = 4;
		this.rblhlFixed.TabStop = true;
		this.rblhlFixed.Text = "固定";
		this.rblhlFixed.UseVisualStyleBackColor = true;
		this.rblhlHeader.AutoSize = true;
		this.rblhlHeader.Location = new System.Drawing.Point(65, 41);
		this.rblhlHeader.Name = "rblhlHeader";
		this.rblhlHeader.Size = new System.Drawing.Size(47, 16);
		this.rblhlHeader.TabIndex = 4;
		this.rblhlHeader.TabStop = true;
		this.rblhlHeader.Text = "自动";
		this.rblhlHeader.UseVisualStyleBackColor = true;
		this.tblhLeftImage.Location = new System.Drawing.Point(12, 17);
		this.tblhLeftImage.Name = "tblhLeftImage";
		this.tblhLeftImage.ReadOnly = true;
		this.tblhLeftImage.Size = new System.Drawing.Size(154, 21);
		this.tblhLeftImage.TabIndex = 2;
		this.rblhlOriginal.AutoSize = true;
		this.rblhlOriginal.Location = new System.Drawing.Point(12, 41);
		this.rblhlOriginal.Name = "rblhlOriginal";
		this.rblhlOriginal.Size = new System.Drawing.Size(47, 16);
		this.rblhlOriginal.TabIndex = 4;
		this.rblhlOriginal.TabStop = true;
		this.rblhlOriginal.Text = "原始";
		this.rblhlOriginal.UseVisualStyleBackColor = true;
		this.btnlhLeftImage.Location = new System.Drawing.Point(171, 15);
		this.btnlhLeftImage.Name = "btnlhLeftImage";
		this.btnlhLeftImage.Size = new System.Drawing.Size(38, 22);
		this.btnlhLeftImage.TabIndex = 3;
		this.btnlhLeftImage.Text = "...";
		this.btnlhLeftImage.UseVisualStyleBackColor = true;
		this.btnlhLeftImage.Click += new System.EventHandler(btnlhLeftImage_Click);
		this.tpRptHeader.Controls.Add(this.gbrhPrintFullPath);
		this.tpRptHeader.Controls.Add(this.cbrhDateTime);
		this.tpRptHeader.Controls.Add(this.cbrhSystemInfo);
		this.tpRptHeader.Controls.Add(this.cbrhUse);
		this.tpRptHeader.Controls.Add(this.cbrhOnNewPage);
		this.tpRptHeader.Location = new System.Drawing.Point(89, 4);
		this.tpRptHeader.Name = "tpRptHeader";
		this.tpRptHeader.Size = new System.Drawing.Size(328, 254);
		this.tpRptHeader.TabIndex = 2;
		this.tpRptHeader.Text = "报告标头";
		this.tpRptHeader.UseVisualStyleBackColor = true;
		this.gbrhPrintFullPath.Controls.Add(this.rbrhnsOthersOnly);
		this.gbrhPrintFullPath.Controls.Add(this.rbrhnsAlways);
		this.gbrhPrintFullPath.Location = new System.Drawing.Point(12, 75);
		this.gbrhPrintFullPath.Name = "gbrhPrintFullPath";
		this.gbrhPrintFullPath.Size = new System.Drawing.Size(205, 65);
		this.gbrhPrintFullPath.TabIndex = 5;
		this.gbrhPrintFullPath.TabStop = false;
		this.gbrhPrintFullPath.Text = "打印全路径文件名";
		this.rbrhnsOthersOnly.AutoSize = true;
		this.rbrhnsOthersOnly.Location = new System.Drawing.Point(10, 42);
		this.rbrhnsOthersOnly.Name = "rbrhnsOthersOnly";
		this.rbrhnsOthersOnly.Size = new System.Drawing.Size(95, 16);
		this.rbrhnsOthersOnly.TabIndex = 0;
		this.rbrhnsOthersOnly.TabStop = true;
		this.rbrhnsOthersOnly.Text = "仅对别的谱图";
		this.rbrhnsOthersOnly.UseVisualStyleBackColor = true;
		this.rbrhnsAlways.AutoSize = true;
		this.rbrhnsAlways.Location = new System.Drawing.Point(10, 20);
		this.rbrhnsAlways.Name = "rbrhnsAlways";
		this.rbrhnsAlways.Size = new System.Drawing.Size(71, 16);
		this.rbrhnsAlways.TabIndex = 0;
		this.rbrhnsAlways.TabStop = true;
		this.rbrhnsAlways.Text = "总是打印";
		this.rbrhnsAlways.UseVisualStyleBackColor = true;
		this.cbrhDateTime.AutoSize = true;
		this.cbrhDateTime.Location = new System.Drawing.Point(12, 168);
		this.cbrhDateTime.Name = "cbrhDateTime";
		this.cbrhDateTime.Size = new System.Drawing.Size(72, 16);
		this.cbrhDateTime.TabIndex = 4;
		this.cbrhDateTime.Text = "打印日期";
		this.cbrhDateTime.UseVisualStyleBackColor = true;
		this.cbrhSystemInfo.AutoSize = true;
		this.cbrhSystemInfo.Location = new System.Drawing.Point(12, 146);
		this.cbrhSystemInfo.Name = "cbrhSystemInfo";
		this.cbrhSystemInfo.Size = new System.Drawing.Size(72, 16);
		this.cbrhSystemInfo.TabIndex = 4;
		this.cbrhSystemInfo.Text = "系统信息";
		this.cbrhSystemInfo.UseVisualStyleBackColor = true;
		this.cbrhUse.AutoSize = true;
		this.cbrhUse.Location = new System.Drawing.Point(12, 6);
		this.cbrhUse.Name = "cbrhUse";
		this.cbrhUse.Size = new System.Drawing.Size(48, 16);
		this.cbrhUse.TabIndex = 3;
		this.cbrhUse.Text = "使用";
		this.cbrhUse.UseVisualStyleBackColor = true;
		this.cbrhUse.CheckedChanged += new System.EventHandler(cbsqUse_CheckedChanged);
		this.cbrhOnNewPage.AutoSize = true;
		this.cbrhOnNewPage.Location = new System.Drawing.Point(12, 28);
		this.cbrhOnNewPage.Name = "cbrhOnNewPage";
		this.cbrhOnNewPage.Size = new System.Drawing.Size(72, 16);
		this.cbrhOnNewPage.TabIndex = 3;
		this.cbrhOnNewPage.Text = "使用新页";
		this.cbrhOnNewPage.UseVisualStyleBackColor = true;
		this.tpMethod.Controls.Add(this.gbmtdChromInfo);
		this.tpMethod.Controls.Add(this.cbmtdAS);
		this.tpMethod.Controls.Add(this.cbmtdUse);
		this.tpMethod.Controls.Add(this.cbmtdIntegration);
		this.tpMethod.Controls.Add(this.cbmtdAcquisition);
		this.tpMethod.Controls.Add(this.cbmtdLcGrdtItems);
		this.tpMethod.Controls.Add(this.cbmtdGcPTGraph);
		this.tpMethod.Controls.Add(this.cbmtdLcGrdtGraph);
		this.tpMethod.Controls.Add(this.cbmtdGcProgTemp);
		this.tpMethod.Controls.Add(this.cbmtdLcGradient);
		this.tpMethod.Controls.Add(this.cbmtdOnNewPage);
		this.tpMethod.Location = new System.Drawing.Point(89, 4);
		this.tpMethod.Name = "tpMethod";
		this.tpMethod.Size = new System.Drawing.Size(328, 254);
		this.tpMethod.TabIndex = 3;
		this.tpMethod.Text = "方法";
		this.tpMethod.UseVisualStyleBackColor = true;
		this.gbmtdChromInfo.Controls.Add(this.cbmtdciMeasurement);
		this.gbmtdChromInfo.Controls.Add(this.cbmtdciCalculation);
		this.gbmtdChromInfo.Controls.Add(this.cbmtdciAdvance);
		this.gbmtdChromInfo.Controls.Add(this.cbmtdciGpcRanges);
		this.gbmtdChromInfo.Controls.Add(this.cbmtdciPDA);
		this.gbmtdChromInfo.Location = new System.Drawing.Point(12, 163);
		this.gbmtdChromInfo.Name = "gbmtdChromInfo";
		this.gbmtdChromInfo.Size = new System.Drawing.Size(148, 87);
		this.gbmtdChromInfo.TabIndex = 8;
		this.gbmtdChromInfo.TabStop = false;
		this.gbmtdChromInfo.Text = "谱图信息";
		this.cbmtdciMeasurement.AutoSize = true;
		this.cbmtdciMeasurement.Location = new System.Drawing.Point(15, 20);
		this.cbmtdciMeasurement.Name = "cbmtdciMeasurement";
		this.cbmtdciMeasurement.Size = new System.Drawing.Size(72, 16);
		this.cbmtdciMeasurement.TabIndex = 6;
		this.cbmtdciMeasurement.Text = "测量信息";
		this.cbmtdciMeasurement.UseVisualStyleBackColor = true;
		this.cbmtdciCalculation.AutoSize = true;
		this.cbmtdciCalculation.Location = new System.Drawing.Point(15, 42);
		this.cbmtdciCalculation.Name = "cbmtdciCalculation";
		this.cbmtdciCalculation.Size = new System.Drawing.Size(48, 16);
		this.cbmtdciCalculation.TabIndex = 6;
		this.cbmtdciCalculation.Text = "计算";
		this.cbmtdciCalculation.UseVisualStyleBackColor = true;
		this.cbmtdciAdvance.AutoSize = true;
		this.cbmtdciAdvance.Location = new System.Drawing.Point(15, 64);
		this.cbmtdciAdvance.Name = "cbmtdciAdvance";
		this.cbmtdciAdvance.Size = new System.Drawing.Size(48, 16);
		this.cbmtdciAdvance.TabIndex = 6;
		this.cbmtdciAdvance.Text = "高级";
		this.cbmtdciAdvance.UseVisualStyleBackColor = true;
		this.cbmtdciGpcRanges.AutoSize = true;
		this.cbmtdciGpcRanges.Enabled = false;
		this.cbmtdciGpcRanges.Location = new System.Drawing.Point(15, 108);
		this.cbmtdciGpcRanges.Name = "cbmtdciGpcRanges";
		this.cbmtdciGpcRanges.Size = new System.Drawing.Size(72, 16);
		this.cbmtdciGpcRanges.TabIndex = 6;
		this.cbmtdciGpcRanges.Text = "分段积分";
		this.cbmtdciGpcRanges.UseVisualStyleBackColor = true;
		this.cbmtdciGpcRanges.Visible = false;
		this.cbmtdciPDA.AutoSize = true;
		this.cbmtdciPDA.Enabled = false;
		this.cbmtdciPDA.Location = new System.Drawing.Point(15, 86);
		this.cbmtdciPDA.Name = "cbmtdciPDA";
		this.cbmtdciPDA.Size = new System.Drawing.Size(96, 16);
		this.cbmtdciPDA.TabIndex = 6;
		this.cbmtdciPDA.Text = "lclCheckBox1";
		this.cbmtdciPDA.UseVisualStyleBackColor = true;
		this.cbmtdciPDA.Visible = false;
		this.cbmtdAS.AutoSize = true;
		this.cbmtdAS.Enabled = false;
		this.cbmtdAS.Location = new System.Drawing.Point(174, 38);
		this.cbmtdAS.Name = "cbmtdAS";
		this.cbmtdAS.Size = new System.Drawing.Size(84, 16);
		this.cbmtdAS.TabIndex = 7;
		this.cbmtdAS.Text = "自动进样器";
		this.cbmtdAS.UseVisualStyleBackColor = true;
		this.cbmtdAS.Visible = false;
		this.cbmtdUse.AutoSize = true;
		this.cbmtdUse.Location = new System.Drawing.Point(12, 6);
		this.cbmtdUse.Name = "cbmtdUse";
		this.cbmtdUse.Size = new System.Drawing.Size(48, 16);
		this.cbmtdUse.TabIndex = 7;
		this.cbmtdUse.Text = "使用";
		this.cbmtdUse.UseVisualStyleBackColor = true;
		this.cbmtdUse.CheckedChanged += new System.EventHandler(cbsqUse_CheckedChanged);
		this.cbmtdIntegration.AutoSize = true;
		this.cbmtdIntegration.Location = new System.Drawing.Point(12, 141);
		this.cbmtdIntegration.Name = "cbmtdIntegration";
		this.cbmtdIntegration.Size = new System.Drawing.Size(90, 16);
		this.cbmtdIntegration.TabIndex = 6;
		this.cbmtdIntegration.Text = "[方法] 积分";
		this.cbmtdIntegration.UseVisualStyleBackColor = true;
		this.cbmtdAcquisition.AutoSize = true;
		this.cbmtdAcquisition.Location = new System.Drawing.Point(12, 119);
		this.cbmtdAcquisition.Name = "cbmtdAcquisition";
		this.cbmtdAcquisition.Size = new System.Drawing.Size(48, 16);
		this.cbmtdAcquisition.TabIndex = 6;
		this.cbmtdAcquisition.Text = "采集";
		this.cbmtdAcquisition.UseVisualStyleBackColor = true;
		this.cbmtdLcGrdtItems.AutoSize = true;
		this.cbmtdLcGrdtItems.Location = new System.Drawing.Point(216, 97);
		this.cbmtdLcGrdtItems.Name = "cbmtdLcGrdtItems";
		this.cbmtdLcGrdtItems.Size = new System.Drawing.Size(72, 16);
		this.cbmtdLcGrdtItems.TabIndex = 6;
		this.cbmtdLcGrdtItems.Text = "梯度项目";
		this.cbmtdLcGrdtItems.UseVisualStyleBackColor = true;
		this.cbmtdGcPTGraph.AutoSize = true;
		this.cbmtdGcPTGraph.Location = new System.Drawing.Point(114, 75);
		this.cbmtdGcPTGraph.Name = "cbmtdGcPTGraph";
		this.cbmtdGcPTGraph.Size = new System.Drawing.Size(72, 16);
		this.cbmtdGcPTGraph.TabIndex = 6;
		this.cbmtdGcPTGraph.Text = "升温图像";
		this.cbmtdGcPTGraph.UseVisualStyleBackColor = true;
		this.cbmtdLcGrdtGraph.AutoSize = true;
		this.cbmtdLcGrdtGraph.Location = new System.Drawing.Point(114, 97);
		this.cbmtdLcGrdtGraph.Name = "cbmtdLcGrdtGraph";
		this.cbmtdLcGrdtGraph.Size = new System.Drawing.Size(72, 16);
		this.cbmtdLcGrdtGraph.TabIndex = 6;
		this.cbmtdLcGrdtGraph.Text = "梯度图像";
		this.cbmtdLcGrdtGraph.UseVisualStyleBackColor = true;
		this.cbmtdGcProgTemp.AutoSize = true;
		this.cbmtdGcProgTemp.Location = new System.Drawing.Point(12, 75);
		this.cbmtdGcProgTemp.Name = "cbmtdGcProgTemp";
		this.cbmtdGcProgTemp.Size = new System.Drawing.Size(72, 16);
		this.cbmtdGcProgTemp.TabIndex = 6;
		this.cbmtdGcProgTemp.Text = "程序升温";
		this.cbmtdGcProgTemp.UseVisualStyleBackColor = true;
		this.cbmtdLcGradient.AutoSize = true;
		this.cbmtdLcGradient.Location = new System.Drawing.Point(12, 97);
		this.cbmtdLcGradient.Name = "cbmtdLcGradient";
		this.cbmtdLcGradient.Size = new System.Drawing.Size(72, 16);
		this.cbmtdLcGradient.TabIndex = 6;
		this.cbmtdLcGradient.Text = "液相梯度";
		this.cbmtdLcGradient.UseVisualStyleBackColor = true;
		this.cbmtdOnNewPage.AutoSize = true;
		this.cbmtdOnNewPage.Location = new System.Drawing.Point(12, 28);
		this.cbmtdOnNewPage.Name = "cbmtdOnNewPage";
		this.cbmtdOnNewPage.Size = new System.Drawing.Size(72, 16);
		this.cbmtdOnNewPage.TabIndex = 6;
		this.cbmtdOnNewPage.Text = "使用新页";
		this.cbmtdOnNewPage.UseVisualStyleBackColor = true;
		this.tpChromInfo.Controls.Add(this.cbciUse);
		this.tpChromInfo.Controls.Add(this.cbciOnNewPage);
		this.tpChromInfo.Controls.Add(this.gbciChromInfo);
		this.tpChromInfo.Controls.Add(this.gbciChroms);
		this.tpChromInfo.Location = new System.Drawing.Point(89, 4);
		this.tpChromInfo.Name = "tpChromInfo";
		this.tpChromInfo.Size = new System.Drawing.Size(328, 254);
		this.tpChromInfo.TabIndex = 4;
		this.tpChromInfo.Text = "谱图信息";
		this.tpChromInfo.UseVisualStyleBackColor = true;
		this.cbciUse.AutoSize = true;
		this.cbciUse.Location = new System.Drawing.Point(12, 6);
		this.cbciUse.Name = "cbciUse";
		this.cbciUse.Size = new System.Drawing.Size(48, 16);
		this.cbciUse.TabIndex = 12;
		this.cbciUse.Text = "使用";
		this.cbciUse.UseVisualStyleBackColor = true;
		this.cbciUse.CheckedChanged += new System.EventHandler(cbsqUse_CheckedChanged);
		this.cbciOnNewPage.AutoSize = true;
		this.cbciOnNewPage.Location = new System.Drawing.Point(12, 28);
		this.cbciOnNewPage.Name = "cbciOnNewPage";
		this.cbciOnNewPage.Size = new System.Drawing.Size(72, 16);
		this.cbciOnNewPage.TabIndex = 11;
		this.cbciOnNewPage.Text = "使用新页";
		this.cbciOnNewPage.UseVisualStyleBackColor = true;
		this.gbciChromInfo.Controls.Add(this.cbciciIntegration);
		this.gbciChromInfo.Controls.Add(this.cbciciMeasurement);
		this.gbciChromInfo.Controls.Add(this.cbciciCalculation);
		this.gbciChromInfo.Controls.Add(this.cbciciAdvance);
		this.gbciChromInfo.Controls.Add(this.cbciciGpcRanges);
		this.gbciChromInfo.Controls.Add(this.cbciciPDA);
		this.gbciChromInfo.Location = new System.Drawing.Point(12, 146);
		this.gbciChromInfo.Name = "gbciChromInfo";
		this.gbciChromInfo.Size = new System.Drawing.Size(159, 104);
		this.gbciChromInfo.TabIndex = 10;
		this.gbciChromInfo.TabStop = false;
		this.gbciChromInfo.Text = "谱图信息";
		this.cbciciIntegration.AutoSize = true;
		this.cbciciIntegration.Location = new System.Drawing.Point(15, 41);
		this.cbciciIntegration.Name = "cbciciIntegration";
		this.cbciciIntegration.Size = new System.Drawing.Size(48, 16);
		this.cbciciIntegration.TabIndex = 6;
		this.cbciciIntegration.Text = "积分";
		this.cbciciIntegration.UseVisualStyleBackColor = true;
		this.cbciciMeasurement.AutoSize = true;
		this.cbciciMeasurement.Location = new System.Drawing.Point(15, 20);
		this.cbciciMeasurement.Name = "cbciciMeasurement";
		this.cbciciMeasurement.Size = new System.Drawing.Size(72, 16);
		this.cbciciMeasurement.TabIndex = 6;
		this.cbciciMeasurement.Text = "测量信息";
		this.cbciciMeasurement.UseVisualStyleBackColor = true;
		this.cbciciCalculation.AutoSize = true;
		this.cbciciCalculation.Location = new System.Drawing.Point(15, 62);
		this.cbciciCalculation.Name = "cbciciCalculation";
		this.cbciciCalculation.Size = new System.Drawing.Size(48, 16);
		this.cbciciCalculation.TabIndex = 6;
		this.cbciciCalculation.Text = "计算";
		this.cbciciCalculation.UseVisualStyleBackColor = true;
		this.cbciciAdvance.AutoSize = true;
		this.cbciciAdvance.Location = new System.Drawing.Point(15, 83);
		this.cbciciAdvance.Name = "cbciciAdvance";
		this.cbciciAdvance.Size = new System.Drawing.Size(48, 16);
		this.cbciciAdvance.TabIndex = 6;
		this.cbciciAdvance.Text = "高级";
		this.cbciciAdvance.UseVisualStyleBackColor = true;
		this.cbciciGpcRanges.AutoSize = true;
		this.cbciciGpcRanges.Enabled = false;
		this.cbciciGpcRanges.Location = new System.Drawing.Point(15, 131);
		this.cbciciGpcRanges.Name = "cbciciGpcRanges";
		this.cbciciGpcRanges.Size = new System.Drawing.Size(72, 16);
		this.cbciciGpcRanges.TabIndex = 6;
		this.cbciciGpcRanges.Text = "分段积分";
		this.cbciciGpcRanges.UseVisualStyleBackColor = true;
		this.cbciciGpcRanges.Visible = false;
		this.cbciciPDA.AutoSize = true;
		this.cbciciPDA.Enabled = false;
		this.cbciciPDA.Location = new System.Drawing.Point(15, 109);
		this.cbciciPDA.Name = "cbciciPDA";
		this.cbciciPDA.Size = new System.Drawing.Size(96, 16);
		this.cbciciPDA.TabIndex = 6;
		this.cbciciPDA.Text = "lclCheckBox1";
		this.cbciciPDA.UseVisualStyleBackColor = true;
		this.cbciciPDA.Visible = false;
		this.gbciChroms.Controls.Add(this.rbcichmActive);
		this.gbciChroms.Controls.Add(this.rbcichmAll);
		this.gbciChroms.Location = new System.Drawing.Point(12, 75);
		this.gbciChroms.Name = "gbciChroms";
		this.gbciChroms.Size = new System.Drawing.Size(159, 65);
		this.gbciChroms.TabIndex = 9;
		this.gbciChroms.TabStop = false;
		this.gbciChroms.Text = "对像谱图";
		this.rbcichmActive.AutoSize = true;
		this.rbcichmActive.Location = new System.Drawing.Point(10, 42);
		this.rbcichmActive.Name = "rbcichmActive";
		this.rbcichmActive.Size = new System.Drawing.Size(71, 16);
		this.rbcichmActive.TabIndex = 0;
		this.rbcichmActive.TabStop = true;
		this.rbcichmActive.Text = "当前谱图";
		this.rbcichmActive.UseVisualStyleBackColor = true;
		this.rbcichmAll.AutoSize = true;
		this.rbcichmAll.Location = new System.Drawing.Point(10, 20);
		this.rbcichmAll.Name = "rbcichmAll";
		this.rbcichmAll.Size = new System.Drawing.Size(71, 16);
		this.rbcichmAll.TabIndex = 0;
		this.rbcichmAll.TabStop = true;
		this.rbcichmAll.Text = "所有谱图";
		this.rbcichmAll.UseVisualStyleBackColor = true;
		this.tpChromGraph.Controls.Add(this.gbcgDisStyle);
		this.tpChromGraph.Controls.Add(this.gbcgShowStyle);
		this.tpChromGraph.Controls.Add(this.gbcgChroms);
		this.tpChromGraph.Controls.Add(this.cbcgUse);
		this.tpChromGraph.Controls.Add(this.cbcgOnNewPage);
		this.tpChromGraph.Location = new System.Drawing.Point(89, 4);
		this.tpChromGraph.Name = "tpChromGraph";
		this.tpChromGraph.Size = new System.Drawing.Size(328, 254);
		this.tpChromGraph.TabIndex = 5;
		this.tpChromGraph.Text = "谱图";
		this.tpChromGraph.UseVisualStyleBackColor = true;
		this.gbcgDisStyle.Controls.Add(this.rbcgdsCurrent);
		this.gbcgDisStyle.Controls.Add(this.rbcgdsWhole);
		this.gbcgDisStyle.Location = new System.Drawing.Point(155, 146);
		this.gbcgDisStyle.Name = "gbcgDisStyle";
		this.gbcgDisStyle.Size = new System.Drawing.Size(137, 65);
		this.gbcgDisStyle.TabIndex = 15;
		this.gbcgDisStyle.TabStop = false;
		this.gbcgDisStyle.Text = "显示逻辑";
		this.rbcgdsCurrent.AutoSize = true;
		this.rbcgdsCurrent.Location = new System.Drawing.Point(10, 42);
		this.rbcgdsCurrent.Name = "rbcgdsCurrent";
		this.rbcgdsCurrent.Size = new System.Drawing.Size(71, 16);
		this.rbcgdsCurrent.TabIndex = 0;
		this.rbcgdsCurrent.TabStop = true;
		this.rbcgdsCurrent.Text = "当前逻辑";
		this.rbcgdsCurrent.UseVisualStyleBackColor = true;
		this.rbcgdsWhole.AutoSize = true;
		this.rbcgdsWhole.Location = new System.Drawing.Point(10, 20);
		this.rbcgdsWhole.Name = "rbcgdsWhole";
		this.rbcgdsWhole.Size = new System.Drawing.Size(71, 16);
		this.rbcgdsWhole.TabIndex = 0;
		this.rbcgdsWhole.TabStop = true;
		this.rbcgdsWhole.Text = "完全显示";
		this.rbcgdsWhole.UseVisualStyleBackColor = true;
		this.gbcgShowStyle.Controls.Add(this.rbcgssSeparate);
		this.gbcgShowStyle.Controls.Add(this.rbcgssCombine);
		this.gbcgShowStyle.Location = new System.Drawing.Point(12, 146);
		this.gbcgShowStyle.Name = "gbcgShowStyle";
		this.gbcgShowStyle.Size = new System.Drawing.Size(137, 65);
		this.gbcgShowStyle.TabIndex = 15;
		this.gbcgShowStyle.TabStop = false;
		this.gbcgShowStyle.Text = "显示方式";
		this.rbcgssSeparate.AutoSize = true;
		this.rbcgssSeparate.Location = new System.Drawing.Point(10, 42);
		this.rbcgssSeparate.Name = "rbcgssSeparate";
		this.rbcgssSeparate.Size = new System.Drawing.Size(71, 16);
		this.rbcgssSeparate.TabIndex = 0;
		this.rbcgssSeparate.TabStop = true;
		this.rbcgssSeparate.Text = "分立显示";
		this.rbcgssSeparate.UseVisualStyleBackColor = true;
		this.rbcgssCombine.AutoSize = true;
		this.rbcgssCombine.Location = new System.Drawing.Point(10, 20);
		this.rbcgssCombine.Name = "rbcgssCombine";
		this.rbcgssCombine.Size = new System.Drawing.Size(71, 16);
		this.rbcgssCombine.TabIndex = 0;
		this.rbcgssCombine.TabStop = true;
		this.rbcgssCombine.Text = "整合显示";
		this.rbcgssCombine.UseVisualStyleBackColor = true;
		this.gbcgChroms.Controls.Add(this.rbcgchmActive);
		this.gbcgChroms.Controls.Add(this.rbcgchmAll);
		this.gbcgChroms.Location = new System.Drawing.Point(12, 75);
		this.gbcgChroms.Name = "gbcgChroms";
		this.gbcgChroms.Size = new System.Drawing.Size(159, 65);
		this.gbcgChroms.TabIndex = 15;
		this.gbcgChroms.TabStop = false;
		this.gbcgChroms.Text = "对像谱图";
		this.rbcgchmActive.AutoSize = true;
		this.rbcgchmActive.Location = new System.Drawing.Point(10, 42);
		this.rbcgchmActive.Name = "rbcgchmActive";
		this.rbcgchmActive.Size = new System.Drawing.Size(71, 16);
		this.rbcgchmActive.TabIndex = 0;
		this.rbcgchmActive.TabStop = true;
		this.rbcgchmActive.Text = "当前谱图";
		this.rbcgchmActive.UseVisualStyleBackColor = true;
		this.rbcgchmAll.AutoSize = true;
		this.rbcgchmAll.Location = new System.Drawing.Point(10, 20);
		this.rbcgchmAll.Name = "rbcgchmAll";
		this.rbcgchmAll.Size = new System.Drawing.Size(71, 16);
		this.rbcgchmAll.TabIndex = 0;
		this.rbcgchmAll.TabStop = true;
		this.rbcgchmAll.Text = "所有谱图";
		this.rbcgchmAll.UseVisualStyleBackColor = true;
		this.cbcgUse.AutoSize = true;
		this.cbcgUse.Location = new System.Drawing.Point(12, 6);
		this.cbcgUse.Name = "cbcgUse";
		this.cbcgUse.Size = new System.Drawing.Size(48, 16);
		this.cbcgUse.TabIndex = 14;
		this.cbcgUse.Text = "使用";
		this.cbcgUse.UseVisualStyleBackColor = true;
		this.cbcgUse.CheckedChanged += new System.EventHandler(cbsqUse_CheckedChanged);
		this.cbcgOnNewPage.AutoSize = true;
		this.cbcgOnNewPage.Location = new System.Drawing.Point(12, 28);
		this.cbcgOnNewPage.Name = "cbcgOnNewPage";
		this.cbcgOnNewPage.Size = new System.Drawing.Size(72, 16);
		this.cbcgOnNewPage.TabIndex = 13;
		this.cbcgOnNewPage.Text = "使用新页";
		this.cbcgOnNewPage.UseVisualStyleBackColor = true;
		this.tpChromRlts.Controls.Add(this.gbcrChroms);
		this.tpChromRlts.Controls.Add(this.cbcrRltCombine);
		this.tpChromRlts.Controls.Add(this.cbcrResult);
		this.tpChromRlts.Controls.Add(this.cbcrUse);
		this.tpChromRlts.Controls.Add(this.cbcrGpcRanges);
		this.tpChromRlts.Controls.Add(this.cbcrGpcSlices);
		this.tpChromRlts.Controls.Add(this.cbcrSST);
		this.tpChromRlts.Controls.Add(this.cbcrPerformance);
		this.tpChromRlts.Controls.Add(this.cbcrSummary);
		this.tpChromRlts.Controls.Add(this.cbcrOnNewPage);
		this.tpChromRlts.Location = new System.Drawing.Point(89, 4);
		this.tpChromRlts.Name = "tpChromRlts";
		this.tpChromRlts.Size = new System.Drawing.Size(328, 254);
		this.tpChromRlts.TabIndex = 6;
		this.tpChromRlts.Text = "处理结果";
		this.tpChromRlts.UseVisualStyleBackColor = true;
		this.gbcrChroms.Controls.Add(this.rbcrchmActive);
		this.gbcrChroms.Controls.Add(this.rbcrchmAll);
		this.gbcrChroms.Location = new System.Drawing.Point(12, 75);
		this.gbcrChroms.Name = "gbcrChroms";
		this.gbcrChroms.Size = new System.Drawing.Size(159, 65);
		this.gbcrChroms.TabIndex = 16;
		this.gbcrChroms.TabStop = false;
		this.gbcrChroms.Text = "对像谱图";
		this.rbcrchmActive.AutoSize = true;
		this.rbcrchmActive.Location = new System.Drawing.Point(10, 42);
		this.rbcrchmActive.Name = "rbcrchmActive";
		this.rbcrchmActive.Size = new System.Drawing.Size(71, 16);
		this.rbcrchmActive.TabIndex = 0;
		this.rbcrchmActive.TabStop = true;
		this.rbcrchmActive.Text = "当前谱图";
		this.rbcrchmActive.UseVisualStyleBackColor = true;
		this.rbcrchmAll.AutoSize = true;
		this.rbcrchmAll.Location = new System.Drawing.Point(10, 20);
		this.rbcrchmAll.Name = "rbcrchmAll";
		this.rbcrchmAll.Size = new System.Drawing.Size(71, 16);
		this.rbcrchmAll.TabIndex = 0;
		this.rbcrchmAll.TabStop = true;
		this.rbcrchmAll.Text = "所有谱图";
		this.rbcrchmAll.UseVisualStyleBackColor = true;
		this.cbcrRltCombine.AutoSize = true;
		this.cbcrRltCombine.Location = new System.Drawing.Point(131, 190);
		this.cbcrRltCombine.Name = "cbcrRltCombine";
		this.cbcrRltCombine.Size = new System.Drawing.Size(96, 16);
		this.cbcrRltCombine.TabIndex = 14;
		this.cbcrRltCombine.Text = "整合输出结果";
		this.cbcrRltCombine.UseVisualStyleBackColor = true;
		this.cbcrResult.AutoSize = true;
		this.cbcrResult.Location = new System.Drawing.Point(12, 190);
		this.cbcrResult.Name = "cbcrResult";
		this.cbcrResult.Size = new System.Drawing.Size(48, 16);
		this.cbcrResult.TabIndex = 14;
		this.cbcrResult.Text = "结果";
		this.cbcrResult.UseVisualStyleBackColor = true;
		this.cbcrUse.AutoSize = true;
		this.cbcrUse.Location = new System.Drawing.Point(12, 6);
		this.cbcrUse.Name = "cbcrUse";
		this.cbcrUse.Size = new System.Drawing.Size(48, 16);
		this.cbcrUse.TabIndex = 14;
		this.cbcrUse.Text = "使用";
		this.cbcrUse.UseVisualStyleBackColor = true;
		this.cbcrUse.CheckedChanged += new System.EventHandler(cbsqUse_CheckedChanged);
		this.cbcrGpcRanges.AutoSize = true;
		this.cbcrGpcRanges.Location = new System.Drawing.Point(12, 256);
		this.cbcrGpcRanges.Name = "cbcrGpcRanges";
		this.cbcrGpcRanges.Size = new System.Drawing.Size(102, 16);
		this.cbcrGpcRanges.TabIndex = 13;
		this.cbcrGpcRanges.Text = "cbcrGpcRanges";
		this.cbcrGpcRanges.UseVisualStyleBackColor = true;
		this.cbcrGpcRanges.Visible = false;
		this.cbcrGpcSlices.AutoSize = true;
		this.cbcrGpcSlices.Location = new System.Drawing.Point(12, 234);
		this.cbcrGpcSlices.Name = "cbcrGpcSlices";
		this.cbcrGpcSlices.Size = new System.Drawing.Size(102, 16);
		this.cbcrGpcSlices.TabIndex = 13;
		this.cbcrGpcSlices.Text = "cbcrGpcSlices";
		this.cbcrGpcSlices.UseVisualStyleBackColor = true;
		this.cbcrGpcSlices.Visible = false;
		this.cbcrSST.AutoSize = true;
		this.cbcrSST.Location = new System.Drawing.Point(12, 168);
		this.cbcrSST.Name = "cbcrSST";
		this.cbcrSST.Size = new System.Drawing.Size(72, 16);
		this.cbcrSST.TabIndex = 13;
		this.cbcrSST.Text = "组分验证";
		this.cbcrSST.UseVisualStyleBackColor = true;
		this.cbcrPerformance.AutoSize = true;
		this.cbcrPerformance.Location = new System.Drawing.Point(12, 212);
		this.cbcrPerformance.Name = "cbcrPerformance";
		this.cbcrPerformance.Size = new System.Drawing.Size(48, 16);
		this.cbcrPerformance.TabIndex = 13;
		this.cbcrPerformance.Text = "柱效";
		this.cbcrPerformance.UseVisualStyleBackColor = true;
		this.cbcrSummary.AutoSize = true;
		this.cbcrSummary.Location = new System.Drawing.Point(12, 146);
		this.cbcrSummary.Name = "cbcrSummary";
		this.cbcrSummary.Size = new System.Drawing.Size(60, 16);
		this.cbcrSummary.TabIndex = 13;
		this.cbcrSummary.Text = "总结表";
		this.cbcrSummary.UseVisualStyleBackColor = true;
		this.cbcrOnNewPage.AutoSize = true;
		this.cbcrOnNewPage.Location = new System.Drawing.Point(12, 28);
		this.cbcrOnNewPage.Name = "cbcrOnNewPage";
		this.cbcrOnNewPage.Size = new System.Drawing.Size(72, 16);
		this.cbcrOnNewPage.TabIndex = 13;
		this.cbcrOnNewPage.Text = "使用新页";
		this.cbcrOnNewPage.UseVisualStyleBackColor = true;
		this.tpCali.Controls.Add(this.gbclCmpd);
		this.tpCali.Controls.Add(this.cbclUse);
		this.tpCali.Controls.Add(this.cbclCmpds);
		this.tpCali.Controls.Add(this.cbclOptions);
		this.tpCali.Controls.Add(this.cbclOnNewPage);
		this.tpCali.Location = new System.Drawing.Point(89, 4);
		this.tpCali.Name = "tpCali";
		this.tpCali.Size = new System.Drawing.Size(328, 254);
		this.tpCali.TabIndex = 7;
		this.tpCali.Text = "校正";
		this.tpCali.UseVisualStyleBackColor = true;
		this.gbclCmpd.Controls.Add(this.lclLabel3);
		this.gbclCmpd.Controls.Add(this.lclLabel2);
		this.gbclCmpd.Controls.Add(this.lclLabel1);
		this.gbclCmpd.Controls.Add(this.cbclcdGnlGraph);
		this.gbclCmpd.Controls.Add(this.cbclcdGnlLevels);
		this.gbclCmpd.Location = new System.Drawing.Point(12, 119);
		this.gbclCmpd.Name = "gbclCmpd";
		this.gbclCmpd.Size = new System.Drawing.Size(207, 42);
		this.gbclCmpd.TabIndex = 17;
		this.gbclCmpd.TabStop = false;
		this.gbclCmpd.Text = "分类";
		this.lclLabel3.AutoSize = true;
		this.lclLabel3.Location = new System.Drawing.Point(6, 73);
		this.lclLabel3.Name = "lclLabel3";
		this.lclLabel3.Size = new System.Drawing.Size(23, 12);
		this.lclLabel3.TabIndex = 0;
		this.lclLabel3.Text = "Dad";
		this.lclLabel3.Visible = false;
		this.lclLabel2.AutoSize = true;
		this.lclLabel2.Location = new System.Drawing.Point(6, 46);
		this.lclLabel2.Name = "lclLabel2";
		this.lclLabel2.Size = new System.Drawing.Size(23, 12);
		this.lclLabel2.TabIndex = 0;
		this.lclLabel2.Text = "Gpc";
		this.lclLabel2.Visible = false;
		this.lclLabel1.AutoSize = true;
		this.lclLabel1.Location = new System.Drawing.Point(6, 21);
		this.lclLabel1.Name = "lclLabel1";
		this.lclLabel1.Size = new System.Drawing.Size(23, 12);
		this.lclLabel1.TabIndex = 0;
		this.lclLabel1.Text = "Gnl";
		this.cbclcdGnlGraph.AutoSize = true;
		this.cbclcdGnlGraph.Location = new System.Drawing.Point(127, 20);
		this.cbclcdGnlGraph.Name = "cbclcdGnlGraph";
		this.cbclcdGnlGraph.Size = new System.Drawing.Size(48, 16);
		this.cbclcdGnlGraph.TabIndex = 15;
		this.cbclcdGnlGraph.Text = "图像";
		this.cbclcdGnlGraph.UseVisualStyleBackColor = true;
		this.cbclcdGnlLevels.AutoSize = true;
		this.cbclcdGnlLevels.Location = new System.Drawing.Point(45, 20);
		this.cbclcdGnlLevels.Name = "cbclcdGnlLevels";
		this.cbclcdGnlLevels.Size = new System.Drawing.Size(96, 16);
		this.cbclcdGnlLevels.TabIndex = 15;
		this.cbclcdGnlLevels.Text = "lclCheckBox1";
		this.cbclcdGnlLevels.UseVisualStyleBackColor = true;
		this.cbclUse.AutoSize = true;
		this.cbclUse.Location = new System.Drawing.Point(12, 6);
		this.cbclUse.Name = "cbclUse";
		this.cbclUse.Size = new System.Drawing.Size(48, 16);
		this.cbclUse.TabIndex = 16;
		this.cbclUse.Text = "使用";
		this.cbclUse.UseVisualStyleBackColor = true;
		this.cbclUse.CheckedChanged += new System.EventHandler(cbsqUse_CheckedChanged);
		this.cbclCmpds.AutoSize = true;
		this.cbclCmpds.Location = new System.Drawing.Point(12, 97);
		this.cbclCmpds.Name = "cbclCmpds";
		this.cbclCmpds.Size = new System.Drawing.Size(60, 16);
		this.cbclCmpds.TabIndex = 15;
		this.cbclCmpds.Text = "组分表";
		this.cbclCmpds.UseVisualStyleBackColor = true;
		this.cbclOptions.AutoSize = true;
		this.cbclOptions.Location = new System.Drawing.Point(12, 75);
		this.cbclOptions.Name = "cbclOptions";
		this.cbclOptions.Size = new System.Drawing.Size(72, 16);
		this.cbclOptions.TabIndex = 15;
		this.cbclOptions.Text = "校正选项";
		this.cbclOptions.UseVisualStyleBackColor = true;
		this.cbclOnNewPage.AutoSize = true;
		this.cbclOnNewPage.Location = new System.Drawing.Point(12, 28);
		this.cbclOnNewPage.Name = "cbclOnNewPage";
		this.cbclOnNewPage.Size = new System.Drawing.Size(72, 16);
		this.cbclOnNewPage.TabIndex = 15;
		this.cbclOnNewPage.Text = "使用新页";
		this.cbclOnNewPage.UseVisualStyleBackColor = true;
		this.tpSeq.Controls.Add(this.cbsqOptions);
		this.tpSeq.Controls.Add(this.cbsqUse);
		this.tpSeq.Controls.Add(this.cbsqInjList);
		this.tpSeq.Controls.Add(this.cbsqOnNewPage);
		this.tpSeq.Location = new System.Drawing.Point(89, 4);
		this.tpSeq.Name = "tpSeq";
		this.tpSeq.Size = new System.Drawing.Size(328, 254);
		this.tpSeq.TabIndex = 8;
		this.tpSeq.Text = "序列进样";
		this.tpSeq.UseVisualStyleBackColor = true;
		this.cbsqOptions.AutoSize = true;
		this.cbsqOptions.Location = new System.Drawing.Point(12, 75);
		this.cbsqOptions.Name = "cbsqOptions";
		this.cbsqOptions.Size = new System.Drawing.Size(72, 16);
		this.cbsqOptions.TabIndex = 14;
		this.cbsqOptions.Text = "序列选项";
		this.cbsqOptions.UseVisualStyleBackColor = true;
		this.cbsqUse.AutoSize = true;
		this.cbsqUse.Location = new System.Drawing.Point(12, 6);
		this.cbsqUse.Name = "cbsqUse";
		this.cbsqUse.Size = new System.Drawing.Size(48, 16);
		this.cbsqUse.TabIndex = 14;
		this.cbsqUse.Text = "使用";
		this.cbsqUse.UseVisualStyleBackColor = true;
		this.cbsqUse.CheckedChanged += new System.EventHandler(cbsqUse_CheckedChanged);
		this.cbsqInjList.AutoSize = true;
		this.cbsqInjList.Location = new System.Drawing.Point(12, 97);
		this.cbsqInjList.Name = "cbsqInjList";
		this.cbsqInjList.Size = new System.Drawing.Size(72, 16);
		this.cbsqInjList.TabIndex = 13;
		this.cbsqInjList.Text = "进样列表";
		this.cbsqInjList.UseVisualStyleBackColor = true;
		this.cbsqOnNewPage.AutoSize = true;
		this.cbsqOnNewPage.Location = new System.Drawing.Point(12, 28);
		this.cbsqOnNewPage.Name = "cbsqOnNewPage";
		this.cbsqOnNewPage.Size = new System.Drawing.Size(72, 16);
		this.cbsqOnNewPage.TabIndex = 13;
		this.cbsqOnNewPage.Text = "使用新页";
		this.cbsqOnNewPage.UseVisualStyleBackColor = true;
		this.btnNew.Location = new System.Drawing.Point(428, 87);
		this.btnNew.Name = "btnNew";
		this.btnNew.Size = new System.Drawing.Size(75, 23);
		this.btnNew.TabIndex = 2;
		this.btnNew.Text = "新建";
		this.btnNew.UseVisualStyleBackColor = true;
		this.btnNew.Click += new System.EventHandler(btnNew_Click);
		this.btnOpen.Location = new System.Drawing.Point(428, 112);
		this.btnOpen.Name = "btnOpen";
		this.btnOpen.Size = new System.Drawing.Size(75, 23);
		this.btnOpen.TabIndex = 2;
		this.btnOpen.Text = "打开...";
		this.btnOpen.UseVisualStyleBackColor = true;
		this.btnOpen.Click += new System.EventHandler(btnOpen_Click);
		this.btnSave.Location = new System.Drawing.Point(428, 137);
		this.btnSave.Name = "btnSave";
		this.btnSave.Size = new System.Drawing.Size(75, 23);
		this.btnSave.TabIndex = 2;
		this.btnSave.Text = "保存";
		this.btnSave.UseVisualStyleBackColor = true;
		this.btnSave.Click += new System.EventHandler(btnSave_Click);
		this.btnSaveAs.Location = new System.Drawing.Point(428, 162);
		this.btnSaveAs.Name = "btnSaveAs";
		this.btnSaveAs.Size = new System.Drawing.Size(75, 23);
		this.btnSaveAs.TabIndex = 2;
		this.btnSaveAs.Text = "另存...";
		this.btnSaveAs.UseVisualStyleBackColor = true;
		this.btnSaveAs.Click += new System.EventHandler(btnSaveAs_Click);
		this.btnPrint.Location = new System.Drawing.Point(428, 234);
		this.btnPrint.Name = "btnPrint";
		this.btnPrint.Size = new System.Drawing.Size(75, 23);
		this.btnPrint.TabIndex = 2;
		this.btnPrint.Text = "打印";
		this.btnPrint.UseVisualStyleBackColor = true;
		this.btnPrint.Click += new System.EventHandler(btnPrint_Click);
		this.btnPreview.Location = new System.Drawing.Point(428, 209);
		this.btnPreview.Name = "btnPreview";
		this.btnPreview.Size = new System.Drawing.Size(75, 23);
		this.btnPreview.TabIndex = 2;
		this.btnPreview.Text = "预览";
		this.btnPreview.UseVisualStyleBackColor = true;
		this.btnPreview.Click += new System.EventHandler(btnPreview_Click);
		this.printDocument_0.BeginPrint += new System.Drawing.Printing.PrintEventHandler(printDocument_0_BeginPrint);
		this.printDocument_0.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(printDocument_0_PrintPage);
		this.printDialog_0.Document = this.printDocument_0;
		this.printDialog_0.UseEXDialog = true;
		this.prtPrvDlg.AutoScrollMargin = new System.Drawing.Size(0, 0);
		this.prtPrvDlg.AutoScrollMinSize = new System.Drawing.Size(0, 0);
		this.prtPrvDlg.ClientSize = new System.Drawing.Size(400, 300);
		this.prtPrvDlg.Document = this.printDocument_0;
		this.prtPrvDlg.Enabled = true;
		this.prtPrvDlg.Icon = (System.Drawing.Icon)resources.GetObject("prtPrvDlg.Icon");
		this.prtPrvDlg.Name = "prtPrvDlg";
		this.prtPrvDlg.Visible = false;
		this.btnSaveToPdf.Location = new System.Drawing.Point(431, 262);
		this.btnSaveToPdf.Name = "btnSaveToPdf";
		this.btnSaveToPdf.Size = new System.Drawing.Size(75, 23);
		this.btnSaveToPdf.TabIndex = 3;
		this.btnSaveToPdf.Text = "lclButton1";
		this.btnSaveToPdf.UseVisualStyleBackColor = true;
		this.btnSaveToPdf.Visible = false;
		this.btnSaveToPdf.Click += new System.EventHandler(btnSaveToPdf_Click);
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
		base.ClientSize = new System.Drawing.Size(508, 287);
		base.Controls.Add(this.btnSaveToPdf);
		base.Controls.Add(this.tcRptSetup);
		base.Controls.Add(this.btnNew);
		base.Controls.Add(this.btnPreview);
		base.Controls.Add(this.btnPrint);
		base.Controls.Add(this.btnSaveAs);
		base.Controls.Add(this.btnOpen);
		base.Controls.Add(this.btnSave);
		base.Name = "RptSetupDlg";
		this.Text = "报告设置";
		base.Load += new System.EventHandler(RptSetupDlg_Load);
		base.KeyDown += new System.Windows.Forms.KeyEventHandler(RptSetupDlg_KeyDown);
		base.Controls.SetChildIndex(this.btnSave, 0);
		base.Controls.SetChildIndex(this.btnOpen, 0);
		base.Controls.SetChildIndex(this.btnSaveAs, 0);
		base.Controls.SetChildIndex(this.btnPrint, 0);
		base.Controls.SetChildIndex(base.btnCancel, 0);
		base.Controls.SetChildIndex(base.btnHelp, 0);
		base.Controls.SetChildIndex(base.btnOK, 0);
		base.Controls.SetChildIndex(this.btnPreview, 0);
		base.Controls.SetChildIndex(this.btnNew, 0);
		base.Controls.SetChildIndex(this.tcRptSetup, 0);
		base.Controls.SetChildIndex(this.btnSaveToPdf, 0);
		this.tcRptSetup.ResumeLayout(false);
		this.tpPageSetup.ResumeLayout(false);
		this.tpPageSetup.PerformLayout();
		this.gbpsMargins.ResumeLayout(false);
		this.gbpsMargins.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.nudpsmgInterval).EndInit();
		((System.ComponentModel.ISupportInitialize)this.nudpsmgRight).EndInit();
		((System.ComponentModel.ISupportInitialize)this.nudpsmgBottom).EndInit();
		((System.ComponentModel.ISupportInitialize)this.nudpsmgTop).EndInit();
		((System.ComponentModel.ISupportInitialize)this.nudpsmgLeft).EndInit();
		this.tpLabHeader.ResumeLayout(false);
		this.tpLabHeader.PerformLayout();
		this.gblhLines.ResumeLayout(false);
		this.gblhLines.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.gvlhLines).EndInit();
		((System.ComponentModel.ISupportInitialize)this.nudlhLinesNum).EndInit();
		this.gblhUseRightImage.ResumeLayout(false);
		this.gblhUseRightImage.PerformLayout();
		this.gblhUseLeftImage.ResumeLayout(false);
		this.gblhUseLeftImage.PerformLayout();
		this.tpRptHeader.ResumeLayout(false);
		this.tpRptHeader.PerformLayout();
		this.gbrhPrintFullPath.ResumeLayout(false);
		this.gbrhPrintFullPath.PerformLayout();
		this.tpMethod.ResumeLayout(false);
		this.tpMethod.PerformLayout();
		this.gbmtdChromInfo.ResumeLayout(false);
		this.gbmtdChromInfo.PerformLayout();
		this.tpChromInfo.ResumeLayout(false);
		this.tpChromInfo.PerformLayout();
		this.gbciChromInfo.ResumeLayout(false);
		this.gbciChromInfo.PerformLayout();
		this.gbciChroms.ResumeLayout(false);
		this.gbciChroms.PerformLayout();
		this.tpChromGraph.ResumeLayout(false);
		this.tpChromGraph.PerformLayout();
		this.gbcgDisStyle.ResumeLayout(false);
		this.gbcgDisStyle.PerformLayout();
		this.gbcgShowStyle.ResumeLayout(false);
		this.gbcgShowStyle.PerformLayout();
		this.gbcgChroms.ResumeLayout(false);
		this.gbcgChroms.PerformLayout();
		this.tpChromRlts.ResumeLayout(false);
		this.tpChromRlts.PerformLayout();
		this.gbcrChroms.ResumeLayout(false);
		this.gbcrChroms.PerformLayout();
		this.tpCali.ResumeLayout(false);
		this.tpCali.PerformLayout();
		this.gbclCmpd.ResumeLayout(false);
		this.gbclCmpd.PerformLayout();
		this.tpSeq.ResumeLayout(false);
		this.tpSeq.PerformLayout();
		base.ResumeLayout(false);
	}
}
