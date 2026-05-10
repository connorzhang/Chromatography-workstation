using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace IBrainChrom2018;

public class VIChromForm : LclGnlForm
{
	private bool bool_0;

	private Chromatogram[] chromatogram_0 = new Chromatogram[0];

	private CusDlg cusDlg_0;

	private LbLineDlg lbLineDlg_0 = new LbLineDlg();

	private LbTextDlg lbTextDlg_0 = new LbTextDlg();

	private ManuDlg manuDlg_0;

	private ColumnsSetupDlg columnsSetupDlg_0 = new ColumnsSetupDlg("柱效列表列设置", "Performance Columns Setup");

	private ColumnsSetupDlg columnsSetupDlg_1 = new ColumnsSetupDlg("结果列表列设置", "Result List ColumnsSetup");

	private ColumnsSetupDlg columnsSetupDlg_2 = new ColumnsSetupDlg("切片列表列设置", "Slices List ColumnsSetup");

	private ColumnsSetupDlg columnsSetupDlg_3 = new ColumnsSetupDlg("总结列表列设置", "Summary List ColumnsSetup");

	private SmyTabOptDlg smyTabOptDlg_0 = new SmyTabOptDlg();

	private ColumnsSetupDlg columnsSetupDlg_4 = new ColumnsSetupDlg("SST结果列表列设置", "SST Results Columns Setup");

	private SSTParasDlg sstparasDlg_0;

	public static Color istdBkColor = Color.LightGray;

	private bool bool_1;

	private SST sst_0 = new SST();

	private byte byte_0;

	private RectangleF rectangleF_0;

	private IntegRow integRow_1;

	private OpenFileDialog openFileDialog_0;

	private MyOfdChrom myOfdChrom_0 = new MyOfdChrom();

	private OpenFileDialog openFileDialog_1;

	private OpenFileDialog openFileDialog_2 = new OpenFileDialog();

	private OpenFileDialog openFileDialog_3 = new OpenFileDialog();

	private IntegRow integRow_2;

	private Point point_0;

	private Point point_1;

	private PointF pointF_0;

	private object object_0;

	private Rectangle rectangle_0 = new Rectangle(5, 5, 0, 50);

	private Point point_2;

	private string string_277 = "面积\n[" + Class49.MesureUnit() + ".s]";

	private string string_278 = "结束值\n[" + Class49.MesureUnit() + "]";

	private string string_279 = "高度\n[" + Class49.MesureUnit() + "]";

	private string string_280 = "开始值\n[" + Class49.MesureUnit() + "]";

	private string string_281 = "Area\n[" + Class49.MesureUnit() + ".s]";

	private string string_282 = "End Value\n[" + Class49.MesureUnit() + "]";

	private string string_283 = "Height\n[" + Class49.MesureUnit() + "]";

	private string string_284 = "StartValue\n[" + Class49.MesureUnit() + "]";

	private SaveFileDialog saveFileDialog_0;

	private SaveFileDialog saveFileDialog_1 = new SaveFileDialog();

	private bool bool_2;

	private bool bool_3;

	private bool bool_4;

	private bool bool_5;

	private SmyTabOpt smyTabOpt_0 = new SmyTabOpt();

	private LclButton btnasNoneChrom;

	private LclButton btnasSetChrom;

	private ToolStripButton btnbsBsBackHorz;

	private ToolStripButton btnbsBsForwHorz;

	private ToolStripButton btnbsBsFrontTgnt;

	private ToolStripButton btnbsBsTailTgnt;

	private ToolStripButton btnbsBsTgnt;

	private ToolStripButton btnbsBsTogether;

	private ToolStripButton btnbsBsValley;

	private ToolStripButton btnbsBsVtV;

	private LclButton btnclbNone;

	private LclButton btnclbSet;

	private LclButton btnclbView;

	private ToolStripButton btnClose;

	private ToolStripButton btnExpress;

	private ToolStripButton btngblDtecDelay;

	private ToolStripButton btngblPeakWidth;

	private ToolStripButton btngblPkSlope;

	private ToolStripButton btngblThreshold;

	private LclButton btngcuKAlpha;

	private ToolStripButton btnipClampNeg;

	private ToolStripButton btnipFlowMarker;

	private ToolStripButton btnipGroups;

	private ToolStripButton btnipPkAddNeg;

	private ToolStripButton btnipPkAddPosi;

	private ToolStripButton btnipPkArea;

	private ToolStripButton btnipPkCut;

	private ToolStripButton btnipPkHalfWidth;

	private ToolStripButton btnipPkThreshold;

	private ToolStripButton btnipPkVale;

	private ToolStripButton btnipPkWidth;

	private ToolStripButton btnipResetDtecNeg;

	private ToolStripButton btnipSolventPeak;

	private ToolStripButton btnNextZoom;

	private ToolStripButton btnOpen;

	private ToolStripButton btnOverlayMode;

	private ToolStripButton btnPreview;

	private ToolStripButton btnPreviousZoom;

	private ToolStripButton btnPrint;

	private ToolStripButton btnProperties;

	private ToolStripButton btnPrtLink;

	private ToolStripButton btnReportSetup;

	private ToolStripButton btnSave;

	private ToolStripButton btnUnzoom;

	private LclCusComboBox cbasMatching;

	private LclCusComboBox cbcuCalcu;

	private LclCheckBox cbcuHideISTDPeak;

	private LclCusComboBox cbcuUncalBase;

	private LclCheckBox cbcuUseScaleFactor;

	private LclCheckBox cblsoForAllDetectedPeaks;

	private LclCusComboBox cblsoMatchCriteria;

	private LclCheckBox cblsoRestrictRT;

	private LclCheckBox cblsoRestrictWaveLength;

	private LclCheckBox cblsoUseBackCorr;

	private LclCheckBox cbppoRestrictWaveLength;

	private LclCheckBox cbppoUseBackCorr;

	private LclCheckBox cbRltCombine;

	private LclCheckBox cbrtaCanSetRs;

	private ChromDisplay chromDisplay_0;

	private DataGridViewTextBoxColumn clmCT6CN;

	private DataGridViewTextBoxColumn clmCT6SetT;

	private ContextMenuStrip cmsInteg;

	private ContextMenuStrip cmsLibs;

	private ContextMenuStrip cmsPerformance;

	private ContextMenuStrip cmsRltGV;

	private ContextMenuStrip cmsSlices;

	private ContextMenuStrip cmsSSTCmpds;

	private ContextMenuStrip cmsSummary;

	private IContainer icontainer_2;

	private Chromatogram chromatogram_1;

	private SSTCmpd sstcmpd_0;

	private DetectorStyle detectorStyle_0;

	public DataGridView dgvCT6;

	private DisLg disLg_0 = default(DisLg);

	private LclDisplayPanel dpgnlChrom;

	private LclDisplayPanel dpgpcChrom;

	private LclDisplayPanel dpgpcCmlMw;

	private LclDisplayPanel dpgpcMwDistrib;

	private IntegRow integRow_0;

	private FlowLayoutPanel flpChrom;

	private LclGroupBox gbCalibration;

	private LclGroupBox gbcuRltTableReport;

	private LclGroupBox gbcuScale;

	private LclGroupBox gbcuUncalPeaks;

	private LclGroupBox gbinsAcqParas;

	private LclGroupBox gbinsAddSub;

	private LclGroupBox gbinsCdts;

	private LclGroupBox gbinsSampleIdt;

	private LclGroupBox gbpdaLibs;

	private LclGroupBox gbpdaLibSearchOptions;

	private LclGroupBox gbpdaPeakPurityOptions;

	private LclGroupBox gbpfmColumnCalcu;

	private LclGroupBox gbppoUsedPoints;

	private LclGridView gvArchives;

	private LclGridView gvgcProgTemp;

	private LclGridView gvgrMw;

	private LclGridView gvgrPercent;

	public LclIntegGridView gvInteg;

	private LclGridView gvlcGradient;

	private LclGridView gvlibPDA;

	private LclGridView lclGridView_0;

	private LclGridView gvPerformFrom50;

	private LclGridView gvPerformStatic;

	private LclGridView lclGridView_1;

	private LclGridView gvRltsDad;

	private LclGridView gvRltsGnl;

	private LclGridView gvRltsGpc;

	private LclGridView gvSetRights;

	private LclGridView gvSlices;

	private LclGridView gvSSTCmpds;

	private LclSSTGridView gvSSTResults;

	private LclSummaryGridView gvSummary;

	private LclLabel lbapAutoStop;

	private LclLabel lbapAutoStopV;

	private LclLabel lbapExtStart;

	private LclLabel lbapExtStartV;

	private LclLabel lbapMethod;

	private LclLabel lbapMethodV;

	private LclLabel lbapRange;

	private LclLabel lbapRangeV;

	private LclLabel lbapSampling;

	private LclLabel lbapSamplingV;

	private LclLabel lbasChrom;

	private LclLabel lbasMatching;

	private LclLabel lbcuAmount;

	private LclLabel lbcuCalcu;

	private LclLabel lbcuDilution;

	private LclLabel lbcuInjVolume;

	private LclLabel lbcuIstdAmount;

	private LclLabel lbcuScaleFactor;

	private LclLabel lbcuUncalAmtRespF;

	private LclLabel lbcuUncalAmtRespFU;

	private LclLabel lbcuUncalBase;

	private LclLabel lbcuUnitAfterScale;

	private LclLabel lbExpress;

	private LclLabel lbgcuAlpha;

	private LclLabel lbgcuK;

	private LclLabel lbgrMw;

	private LclLabel lbgrPercent;

	private LclLabel lblsoFrom;

	private LclLabel lblsoMatchCriteria;

	private LclLabel lblsoMatchFactorThreshold;

	private LclLabel lblsoMaxNumHits;

	private LclLabel lblsoTo;

	private LclLabel lbmsmColumn;

	private LclLabel lbmsmDetection;

	private LclLabel lbmsmFlowRate;

	private LclLabel lbmsmMobilePhase;

	private LclLabel lbmsmMtdDspt;

	private LclLabel lbmsmNote;

	private LclLabel lbmsmPressure;

	private LclLabel lbmsmTemperature;

	private LclLabel lbpfmColumnLength;

	private LclLabel lbpfmColumnLengthU;

	private LclLabel lbpfmColumnUT;

	private LclLabel lbpfmUnretainedPeakU;

	private LclLabel lbppoAbsorbanceThreshold;

	private LclLabel lbppoFrom;

	private LclLabel lbppoPurityThreshold;

	private LclLabel lbppoTo;

	private LclExpressLabel lbRltExpress;

	private LclLabel lbsiAcquiredTime;

	private LclLabel lbsiAcquiredTimeV;

	private LclLabel lbsiAnalyst;

	private LclLabel lbsiAnalystV;

	private ToolStripLabel lbSignal;

	private ToolStripLabel lbSignalU;

	private LclLabel lbsiSample;

	private LclLabel lbsiSampleID;

	private LclLabel lbslcAverNum;

	private LclExpressLabel lbSSTExpress;

	private LclExpressLabel lbSSTFile;

	private ToolStripLabel lbTime;

	private ToolStripLabel lbTimeU;

	private ToolStripLabel lbyUnit;

	private LclExpressLabel lclExpressLabel5;

	private LclLabel lclLabel10;

	private LclLabel lclLabel15;

	private LclLabel lclLabel2;

	private LclLabel lclLabel3;

	private LclLabel lclLabel4;

	private LclLabel lclLabel5;

	private LclLabel lclLabel6;

	private LclLabel lclLabel7;

	private LclLabel lclLabel9;

	private LclPanel lclPanel1;

	private LclPanel lclPanel2;

	private ToolStripMenuItem miAddRow;

	private ToolStripMenuItem mibsBsBackHorz;

	private ToolStripMenuItem mibsBsForwHorz;

	private ToolStripMenuItem mibsBsFrontTgnt;

	private ToolStripMenuItem mibsBsTailTgnt;

	private ToolStripMenuItem mibsBsTgnt;

	private ToolStripMenuItem mibsBsTogether;

	private ToolStripMenuItem mibsBsValley;

	private ToolStripMenuItem mibsBsVtV;

	private ToolStripMenuItem miChmBaseline;

	private ToolStripMenuItem miChmCreateLabel;

	private ToolStripMenuItem miChmGlobal;

	private ToolStripMenuItem miChmItgPeak;

	private ToolStripMenuItem miChmNoiseDrift;

	private ToolStripMenuItem miChmRemoveLabels;

	private ToolStripMenuItem miChromatogram;

	private ToolStripMenuItem miclLine;

	private ToolStripMenuItem miclText;

	private ToolStripMenuItem miDeleteRow;

	private ToolStripMenuItem miDisNextZoom;

	private ToolStripMenuItem miDisplay;

	private ToolStripMenuItem miDisPreviousZoom;

	private ToolStripMenuItem miDisProperties;

	private ToolStripMenuItem miDisUnzoom;

	private ToolStripMenuItem miFiClose;

	private ToolStripMenuItem miFiCloseAll;

	private ToolStripMenuItem miFiExit;

	private ToolStripMenuItem miFiImportChrom;

	private ToolStripMenuItem miFile;

	private ToolStripMenuItem miFiOpen;

	private ToolStripMenuItem miFiOverlayMode;

	private ToolStripMenuItem miFiPreview;

	private ToolStripMenuItem miFiPrint;

	private ToolStripMenuItem miFiReportSetup;

	private ToolStripMenuItem miFiSave;

	private ToolStripMenuItem miFiSaveAs;

	private ToolStripMenuItem migblDtecDelay;

	private ToolStripMenuItem migblPeakWidth;

	private ToolStripMenuItem migblPkSlope;

	private ToolStripMenuItem migblThreshold;

	private ToolStripMenuItem miipClampNeg;

	private ToolStripMenuItem miipFlowMarker;

	private ToolStripMenuItem miipGroups;

	private ToolStripMenuItem miipPkAddNeg;

	private ToolStripMenuItem miipPkAddPosi;

	private ToolStripMenuItem miipPkArea;

	private ToolStripMenuItem miipPkCut;

	private ToolStripMenuItem miipPkHalfWidth;

	private ToolStripMenuItem miipPkThreshold;

	private ToolStripMenuItem miipPkVale;

	private ToolStripMenuItem miipPkWidth;

	private ToolStripMenuItem miipResetDtecNeg;

	private ToolStripMenuItem miipSolventPeak;

	private ToolStripMenuItem miitgAppendRow;

	private ToolStripMenuItem miitgCopy;

	private ToolStripMenuItem miitgDelete;

	private ToolStripMenuItem miitgInsertRow;

	private ToolStripMenuItem miitgPaste;

	private ToolStripMenuItem miitgRedo;

	private ToolStripMenuItem miitgReset;

	private ToolStripMenuItem miitgUndo;

	private ToolStripMenuItem toolStripMenuItem_0 = new ToolStripMenuItem();

	private ToolStripMenuItem miMethod;

	private ToolStripMenuItem miMtdCaculation;

	private ToolStripMenuItem miMtdIntegration;

	private ToolStripMenuItem miMtdMeasurement;

	private ToolStripMenuItem miMtdSaveTplt;

	private ToolStripMenuItem miMtdTplt;

	private ToolStripMenuItem mindDrift;

	private ToolStripMenuItem mindNoise;

	private ToolStripMenuItem mipfmColumnsSetup;

	private ToolStripMenuItem mipfmRestoreDftColumns;

	private ToolStripMenuItem miResults;

	private ToolStripMenuItem mirlActiveChrom;

	private ToolStripMenuItem mirlAllChroms;

	private ToolStripMenuItem mirlSelected;

	private ToolStripMenuItem miRltPerformance;

	private ToolStripMenuItem miRltResult;

	private ToolStripMenuItem mirltsColumnsSetup;

	private ToolStripMenuItem miRltSmyOpt;

	private ToolStripMenuItem mirltsResetCmpdNames;

	private ToolStripMenuItem mirltsRestoreDftColumns;

	private ToolStripMenuItem miRltSummary;

	private ToolStripMenuItem mislcColumnsSetup;

	private ToolStripMenuItem mislcRestoreDftColumns;

	private ToolStripMenuItem mismyColumnsSetup;

	private ToolStripMenuItem mismyRestoreDftColumns;

	private ToolStripMenuItem mismySmyOpt;

	private ToolStripMenuItem miSST;

	private ToolStripMenuItem misstcClearParas;

	private ToolStripMenuItem miSstClearParas;

	private ToolStripMenuItem misstcNew;

	private ToolStripMenuItem misstColumnsSetup;

	private ToolStripMenuItem misstcOpen;

	private ToolStripMenuItem misstcSave;

	private ToolStripMenuItem misstcSaveas;

	private ToolStripMenuItem misstcSet;

	private ToolStripMenuItem misstcUpdateFromCalib;

	private ToolStripMenuItem miSstNew;

	private ToolStripMenuItem miSstOpen;

	private ToolStripMenuItem misstRestoreDftColumns;

	private ToolStripMenuItem miSstSave;

	private ToolStripMenuItem miSstSaveas;

	private ToolStripMenuItem miSstSet;

	private ToolStripMenuItem miSstUpdateFromCalib;

	private ToolStripMenuItem toolStripMenuItem_1 = new ToolStripMenuItem();

	private MenuStrip msChrom;

	private LclPictureBox pbapES;

	private LclPanel pnlControl;

	private LclPanel pnlcu;

	private LclPanel pnlgcu;

	private LclPanel pnlpfmControl;

	private LclPanel pnlRltsControl;

	private LclPanel pnlSetRights;

	private LclRadioButton rbasAdd;

	private LclRadioButton rbasSub;

	private LclRadioButton rbcuAllDetectedPeaks;

	private LclRadioButton rbcuCaliPeaks;

	private LclRadioButton rbcuIdentifiedPeaks;

	private LclRadioButton rbpfmFrom50per;

	private LclRadioButton rbpfmStatistical;

	private LclRadioButton rbupAll;

	private LclRadioButton rbupFive;

	private ToolStripStatusLabel slbExplain;

	private LclSplitContainer splitContainer;

	private LclSplitContainer spltcArchives;

	private Lcl_SignalPanel spSignals;

	private StatusStrip ssChrom;

	private LclTextBox tbasChrom;

	private LclTextBox tbclb;

	private LclTextBox tbcuAmount;

	private LclTextBox tbcuDilution;

	private LclTextBox tbcuInjVolume;

	private LclTextBox tbcuIstdAmount;

	private LclTextBox tbcuScaleFactor;

	private LclTextBox tbcuUncalAmtRespF;

	private LclTextBox tbcuUnitAfterScale;

	private LclTextBox tbgcuAlpha;

	private LclTextBox tbgcuK;

	private LclTextBox tblsoFrom;

	private LclTextBox tblsoMatchFactorThreshold;

	private LclTextBox tblsoMaxNumHits;

	private LclTextBox tblsoRestrictRT;

	private LclTextBox tblsoTo;

	private LclTextBox tbmsmColumn;

	private LclTextBox tbmsmDetection;

	private LclTextBox tbmsmFlowRate;

	private LclTextBox tbmsmMobilePhase;

	private LclTextBox tbmsmMtdDspt;

	private LclTextBox tbmsmNote;

	private LclTextBox tbmsmPressure;

	private LclTextBox tbmsmTemperature;

	private LclTextBox tbpfmColumnLength;

	private LclTextBox tbpfmColumnUT;

	private LclTextBox tbppoAbsorbanceThreshold;

	private LclTextBox tbppoFrom;

	private LclTextBox tbppoPurityThreshold;

	private LclTextBox tbppoTo;

	private LclTextBox tbrtaRemark;

	private ToolStripTextBox tbSigYBeg;

	private ToolStripTextBox tbSigYEnd;

	private LclTextBox tbsiSample;

	private LclTextBox tbsiSampleID;

	private LclTextBox tbslcAverNum;

	private ToolStripTextBox tbTime;

	private LclTabControl tcChrom;

	private LclTabControl tcGPC;

	private LclTabControl tcMsmCdts;

	private ToolStrip toolStrip1;

	private ToolStrip toolStrip2;

	private ToolStripSeparator toolStripSeparator1;

	private ToolStripSeparator toolStripSeparator10;

	private ToolStripSeparator toolStripSeparator11;

	private ToolStripSeparator toolStripSeparator12;

	private ToolStripSeparator toolStripSeparator13;

	private ToolStripSeparator toolStripSeparator14;

	private ToolStripSeparator toolStripSeparator15;

	private ToolStripSeparator toolStripSeparator16;

	private ToolStripSeparator toolStripSeparator17;

	private ToolStripSeparator toolStripSeparator18;

	private ToolStripSeparator toolStripSeparator19;

	private ToolStripSeparator toolStripSeparator2;

	private ToolStripSeparator toolStripSeparator20;

	private ToolStripSeparator toolStripSeparator21;

	private ToolStripSeparator toolStripSeparator22;

	private ToolStripSeparator toolStripSeparator23;

	private ToolStripSeparator toolStripSeparator25;

	private ToolStripSeparator toolStripSeparator26;

	private ToolStripSeparator toolStripSeparator27;

	private ToolStripSeparator toolStripSeparator28;

	private ToolStripSeparator toolStripSeparator29;

	private ToolStripSeparator toolStripSeparator3;

	private ToolStripSeparator toolStripSeparator30;

	private ToolStripSeparator toolStripSeparator31;

	private ToolStripSeparator toolStripSeparator32;

	private ToolStripSeparator toolStripSeparator33;

	private ToolStripSeparator toolStripSeparator34;

	private ToolStripSeparator toolStripSeparator4;

	private ToolStripSeparator toolStripSeparator5;

	private ToolStripSeparator toolStripSeparator6;

	private ToolStripSeparator toolStripSeparator7;

	private ToolStripSeparator toolStripSeparator8;

	private ToolStripSeparator toolStripSeparator9;

	private TabPage tpGC;

	private TabPage tpgpcChrom;

	private TabPage tpgpcCmlMw;

	private TabPage tpgpcMwDistrib;

	private TabPage tpInstrument;

	private TabPage tpIntegration;

	private TabPage tpLC;

	private TabPage tpMsmCdts;

	private TabPage tpPDAMethod;

	private TabPage tpPerformance;

	private TabPage tpRanges;

	private TabPage tpResults;

	private TabPage tpRightsArchives;

	private TabPage tpSlices;

	private TabPage tpSST;

	private TabPage tpSummary;

	private ToolStrip tsDatAcq;

	private ToolStripSeparator tss1;

	private ToolStripSeparator tss2;

	private IContainer components;

	public Chromatogram CurChrom => chromatogram_1;

	public DisLg CurDisLg => chromDisplay_0.disLg;

	private Signal CurSignal => chromDisplay_0.curSignal;

	private bool HasChrom => chromatogram_0.Length != 0;

	public void SetInstrument(Instrument instrument)
	{
		base.instrument = instrument;
	}

	public VIChromForm()
	{
		InitializeComponent();
		miView.DropDownItems.Add(new ToolStripSeparator());
		miView.DropDownItems.Add(toolStripMenuItem_1);
		toolStripMenuItem_1.Text = Lang.PS("标准按钮", "Stand. Buttons");
		toolStripMenuItem_1.Click += toolStripMenuItem_1_Click;
		toolStripMenuItem_1_Click(null, null);
		miView.DropDownItems.Add(toolStripMenuItem_0);
		toolStripMenuItem_0.Text = Lang.PS("手动积分按钮", "Manual itg. Buttons");
		toolStripMenuItem_0.Click += toolStripMenuItem_0_Click;
		toolStripMenuItem_0_Click(null, null);
		mirltsResetCmpdNames.Text = Lang.PS("清除组分名", "Clear Cmpds Name");
		cbRltCombine.Text = Lang.PS("整合显示谱图结果\n(文件名不能含'.')", "Combine Chroms Results\n(Filename shouldn't contain '.')");
		btnclbView.Text = Lang.PS("查看", "View");
		lbcuIstdAmount.Text = Lang.PS("内标数量", "Istd. Amount");
		gbinsAddSub.Text = Lang.PS("加减谱图", "Add/Sub Chromatogram");
		rbasAdd.Text = Lang.PS("加", "Add");
		rbasSub.Text = Lang.PS("减", "Subtract");
		tpLC.Text = Lang.PS("液相梯度", "Liquid Gradient");
		tpGC.Text = Lang.PS("程序升温", "Prog. Temp.");
		tpRightsArchives.Text = Lang.PS("权限.存档", "Rights & Archives");
		lclLabel2.Text = Lang.PS("用户权限设置：", "User Access Rights Set");
		cbrtaCanSetRs.Text = Lang.PS("可修改权限", "Can modify rights");
		lclLabel4.Text = Lang.PS("审计跟踪：", "Audit Trail:");
		lclLabel6.Text = Lang.PS("备注：", "Note:");
		lbTime.Text = Lang.PS("时轴", "[min]");
		lbSignal.Text = Lang.PS("信号", "Signal");
		splitContainer.Dock = DockStyle.Fill;
		tcGPC.Dock = DockStyle.Fill;
		dpgnlChrom.Dock = DockStyle.Fill;
		gvRltsGpc.shieldBeginEdit = true;
		gvRltsGnl.shieldBeginEdit = true;
		gvRltsDad.shieldBeginEdit = true;
		cbasMatching.InitItems(new object[3]
		{
			ASMatchStyle.NoChange,
			ASMatchStyle.OffsetChrom,
			ASMatchStyle.ScaleChrom
		});
		cbasMatching.InitShowText(new string[3]
		{
			Lang.PS("无变化", "No Change"),
			Lang.PS("偏移谱图", "Offset Chrom"),
			Lang.PS("缩放谱图", "Scale Chrom")
		});
		cbcuCalcu.InitItems(new object[3]
		{
			CalcuStyle.Uncal,
			CalcuStyle.ESTD,
			CalcuStyle.ISTD
		});
		cbcuCalcu.InitShowText(new string[3]
		{
			Lang.PS("无校正", "Uncal"),
			Lang.PS("外标法", "ESTD"),
			Lang.PS("内标法", "ISTD")
		});
		cbcuUncalBase.InitItems(new object[4]
		{
			RespStyle.Area,
			RespStyle.Height,
			RespStyle.AreaSquare,
			RespStyle.PeakHeightSquare
		});
		cbcuUncalBase.InitShowText(new string[4]
		{
			Lang.PS("面积", "Area"),
			Lang.PS("高度", "Height"),
			Lang.PS("面积平方根", "AreaSquare"),
			Lang.PS("高度平方根", "PeakHeightSquare")
		});
		cblsoMatchCriteria.InitItems(new object[3]
		{
			LSO_MatchCriteria.LeastSquare,
			LSO_MatchCriteria.WeightedLeastSquare,
			LSO_MatchCriteria.Correlation
		});
		cblsoMatchCriteria.InitShowText(new string[3]
		{
			Lang.PS("最小平方", "Least Square"),
			Lang.PS("重量最小平方", "Weighted Least Square"),
			Lang.PS("修正", "Correlation")
		});
		myOfdChrom_0.Title = Lang.PS("打开谱图", "Open Chromatogram");
		myOfdChrom_0.Filter = "(*.chm)|*.chm|(N2000.dat)|*.dat|(Dionex.cdf)|*.cdf";
		myOfdChrom_0.FilterIndex = 2;
		myOfdChrom_0.Multiselect = true;
		openFileDialog_3.Title = Lang.PS("打开SST文件", "Open SST File");
		openFileDialog_3.Filter = Class49.MakeFileFilter(".sst");
		openFileDialog_3.Multiselect = false;
		saveFileDialog_1.Title = Lang.PS("保存SST文件", "Save SST File");
		saveFileDialog_1.Filter = Class49.MakeFileFilter(".sst");
		chromDisplay_0 = new ChromDisplay(WinStyle.Chromatogram, dpgnlChrom);
		LoadOptions();
		spSignals.OnSignalButtonClick += method_46;
		chromDisplay_0.OnSignalClick += method_2;
		chromDisplay_0.OnSignalDoubleClick += method_3;
		gvSummary.Dock = DockStyle.Fill;
		gvSummary.BorderStyle = BorderStyle.None;
		method_22();
		method_16(gvSummary.commonColumns);
		method_16(gvSummary.smyGnlColumns);
		method_16(gvSummary.smyGpcColumns);
		method_16(gvSummary.smyDadColumns);
		method_45(InstruStyle.LC);
		method_45(InstruStyle.GPC);
		method_45(InstruStyle.PDA);
		gvInteg.Dock = DockStyle.Fill;
		gvInteg.BorderStyle = BorderStyle.None;
		gvInteg.InitColumns();
		gvInteg.LoadLanguage();
		gvInteg.OnAfterEdit += method_9;
		lbExpress.Left = 1;
		lbExpress.Height = 1;
		lbExpress.Visible = false;
		lbExpress.BringToFront();
		manuDlg_0 = new ManuDlg();
		manuDlg_0.TopMost = true;
		manuDlg_0.OnSuggestClick += method_7;
		manuDlg_0.OnOKClick += method_6;
		btngblDtecDelay.Tag = (migblDtecDelay.Tag = IntegOprtStyle.DtecDelay);
		btngblPeakWidth.Tag = (migblPeakWidth.Tag = IntegOprtStyle.PeakWidth);
		btngblThreshold.Tag = (migblThreshold.Tag = IntegOprtStyle.Threshold);
		btngblPkSlope.Tag = (migblPkSlope.Tag = IntegOprtStyle.VtVSlope);
		btnipResetDtecNeg.Tag = (miipResetDtecNeg.Tag = IntegOprtStyle.ResetDtecNeg);
		btnipClampNeg.Tag = (miipClampNeg.Tag = IntegOprtStyle.ClampNeg);
		btnipPkWidth.Tag = (miipPkWidth.Tag = IntegOprtStyle.PkWidth);
		btnipPkThreshold.Tag = (miipPkThreshold.Tag = IntegOprtStyle.PkThreshold);
		btnipPkAddPosi.Tag = (miipPkAddPosi.Tag = IntegOprtStyle.PkAddPosi);
		btnipPkAddNeg.Tag = (miipPkAddNeg.Tag = IntegOprtStyle.PkAddNeg);
		btnipPkCut.Tag = (miipPkCut.Tag = IntegOprtStyle.PkCut);
		btnipPkHalfWidth.Tag = (miipPkHalfWidth.Tag = IntegOprtStyle.PkHalfWidth);
		btnipPkArea.Tag = (miipPkArea.Tag = IntegOprtStyle.PkArea);
		btnipPkVale.Tag = (miipPkVale.Tag = IntegOprtStyle.PkVale);
		btnipSolventPeak.Tag = (miipSolventPeak.Tag = IntegOprtStyle.SolventPeak);
		btnipFlowMarker.Tag = (miipFlowMarker.Tag = IntegOprtStyle.FlowMarker);
		btnipGroups.Tag = (miipGroups.Tag = IntegOprtStyle.GroupAdd);
		btnbsBsTgnt.Tag = (mibsBsTgnt.Tag = IntegOprtStyle.BsTgnt);
		btnbsBsVtV.Tag = (mibsBsVtV.Tag = IntegOprtStyle.BsVtV);
		btnbsBsValley.Tag = (mibsBsValley.Tag = IntegOprtStyle.BsValley);
		btnbsBsTogether.Tag = (mibsBsTogether.Tag = IntegOprtStyle.BsTogether);
		btnbsBsForwHorz.Tag = (mibsBsForwHorz.Tag = IntegOprtStyle.BsForwHorz);
		btnbsBsBackHorz.Tag = (mibsBsBackHorz.Tag = IntegOprtStyle.BsBackHorz);
		btnbsBsFrontTgnt.Tag = (mibsBsFrontTgnt.Tag = IntegOprtStyle.BsFrontTgnt);
		btnbsBsTailTgnt.Tag = (mibsBsTailTgnt.Tag = IntegOprtStyle.BsTailTgnt);
		mindNoise.Tag = IntegOprtStyle.Noise;
		mindDrift.Tag = IntegOprtStyle.Drift;
	}

	public void ApplyMethod()
	{
	}

	private void method_0(bool bool_6)
	{
		byte_0 = 1;
		lbExpress.Visible = btnExpress.Checked;
		splitContainer_SplitterMoved(null, null);
		chromDisplay_0.DrawL_begin();
		bool_5 = bool_6;
		chromDisplay_0.drawDynamicL = integRow_2.oprtStyle != IntegOprtStyle.PkVale;
		DisDpRefresh();
		chromDisplay_0.DrawL(PointToClient(Cursor.Position).X, 1);
	}

	private void btnExpress_Click(object sender, EventArgs e)
	{
		btnExpress.Checked = !btnExpress.Checked;
		if (btnExpress.Checked)
		{
			ResourceImageLoad.SetCtrlBitmap(btnExpress, SystemBitmapResource.smethod_0());
		}
		else
		{
			ResourceImageLoad.SetCtrlBitmap(btnExpress, SystemBitmapResource.smethod_13());
		}
		lbExpress.Visible = btnExpress.Checked && byte_0 != 0;
	}

	private void btnclbSet_Click(object sender, EventArgs e)
	{
		if (!HasChrom)
		{
			return;
		}
		if (sender == btnclbSet)
		{
			openFileDialog_2.InitialDirectory = ((CurChrom.chromInfo.cclDirectory != "") ? CurChrom.chromInfo.cclDirectory : instrument.PrjPath);
			if (openFileDialog_2.ShowDialog() != DialogResult.OK)
			{
				return;
			}
			CurChrom.chromInfo.cclCalibration = openFileDialog_2.FileName;
		}
		if (sender == btnclbNone)
		{
			CurChrom.chromInfo.cclCalibration = "";
		}
		if (sender == btnclbView)
		{
			string cclCalibration = CurChrom.chromInfo.cclCalibration;
			if (cclCalibration != "")
			{
				if (File.Exists(cclCalibration))
				{
					instrument.form.btnCaliWindow_Click(null, null);
					instrument.form.LoadCaliFile(cclCalibration);
				}
				else
				{
					MessageBox.Show(Lang.PS("文件无效", "File Invalid"));
				}
			}
		}
		else
		{
			method_1();
		}
	}

	private void btngcuKAlpha_Click(object sender, EventArgs e)
	{
	}

	private void btnPrtLink_Click(object sender, EventArgs e)
	{
		instrument.form.btnReportSetup_Click(null, null);
	}

	private void btnasNoneChrom_Click(object sender, EventArgs e)
	{
		if (!HasChrom)
		{
			return;
		}
		if (sender == btnasSetChrom)
		{
			if (openFileDialog_0 == null)
			{
				openFileDialog_0 = new OpenFileDialog();
				openFileDialog_0.Title = "设置加/减谱图";
				openFileDialog_0.Filter = Class49.MakeFileFilter(".chm") + "|" + Class49.MakeFileFilter(".dat");
				openFileDialog_0.FilterIndex = 2;
			}
			openFileDialog_0.InitialDirectory = ((CurChrom.chromInfo.asDirectory != "") ? CurChrom.chromInfo.asDirectory : instrument.PrjPath);
			if (openFileDialog_0.ShowDialog() != DialogResult.OK)
			{
				return;
			}
			CurChrom.chromInfo.asChrom = openFileDialog_0.FileName;
		}
		if (sender == btnasNoneChrom)
		{
			if (CurChrom.chromInfo.asChrom == "")
			{
				return;
			}
			CurChrom.chromInfo.asChrom = "";
		}
		CurChrom.Process(instrument.instruStyle);
		tcChrom_SelectedIndexChanged(null, null);
	}

	private void method_1()
	{
		if (HasChrom)
		{
			CurChrom.CalcuResults(instrument.instruStyle);
			tcChrom_SelectedIndexChanged(null, null);
		}
	}

	private void cbcuCalcu_SelectionChangeCommitted(object sender, EventArgs e)
	{
		if (HasChrom)
		{
			CurChrom.chromInfo.cclCalcu = (CalcuStyle)cbcuCalcu.SelectedItem;
			CurChrom.chromInfo.prsUncalBase = (RespStyle)cbcuUncalBase.SelectedItem;
			method_1();
		}
	}

	private void cbRltCombine_Click(object sender, EventArgs e)
	{
		method_33();
		DisDpRefresh();
	}

	private void cbrtaCanSetRs_Click(object sender, EventArgs e)
	{
		if (!HasChrom)
		{
			return;
		}
		CurChrom.canSetRs = cbrtaCanSetRs.Checked;
		Array.Resize(ref CurChrom.nrUserNames, 0);
		Array.Resize(ref CurChrom.wUserNames, 0);
		for (int i = 0; i < gvSetRights.RowCount; i++)
		{
			if ((bool)gvSetRights.Rows[i].Cells[0].Value)
			{
				int num = CurChrom.nrUserNames.Length;
				Array.Resize(ref CurChrom.nrUserNames, num + 1);
				CurChrom.nrUserNames[num] = gvSetRights.Rows[i].Cells[2].Value.ToString();
			}
			else if ((bool)gvSetRights.Rows[i].Cells[1].Value)
			{
				int num2 = CurChrom.wUserNames.Length;
				Array.Resize(ref CurChrom.wUserNames, num2 + 1);
				CurChrom.wUserNames[num2] = gvSetRights.Rows[i].Cells[2].Value.ToString();
			}
		}
	}

	private void cbasMatching_SelectionChangeCommitted(object sender, EventArgs e)
	{
		if (HasChrom)
		{
			CurChrom.chromInfo.asMatching = (ASMatchStyle)cbasMatching.SelectedItem;
			if (CurChrom.chromInfo.asChrom != "")
			{
				CurChrom.Process(instrument.instruStyle);
				tcChrom_SelectedIndexChanged(null, null);
			}
		}
	}

	public void ChkOverlayMode()
	{
		if (!miFiOverlayMode.Checked)
		{
			miFiCloseAll_Click(null, null);
		}
	}

	private void method_2(int int_0, Signal signal_0)
	{
		spSignals.VirtualClick(int_0);
		chromatogram_1 = null;
		for (int i = 0; i < chromatogram_0.Length; i++)
		{
			if (chromatogram_0[i].signal == signal_0)
			{
				chromatogram_1 = chromatogram_0[i];
				method_39();
				tcChrom_SelectedIndexChanged(null, null);
				break;
			}
		}
	}

	private void method_3(Signal signal_0)
	{
		DisDpRefresh();
		method_38();
	}

	private void VIChromForm_KeyDown(object sender, KeyEventArgs e)
	{
		bool flag = false;
		if (e.Control && e.KeyCode == Keys.Z && HasChrom)
		{
			if (e.Shift)
			{
				if (CurChrom.integ.Redo())
				{
					flag = true;
				}
			}
			else if (CurChrom.integ.Undo())
			{
				flag = true;
			}
		}
		if (flag)
		{
			method_23();
		}
	}

	private void VIChromForm_Load(object sender, EventArgs e)
	{
		lbyUnit.Text = Class49.MesureUnit();
		msChrom.Items.Add(miView);
		miFiExit.Click += base.miFiExit_Click;
		msChrom.Items.Add(miWindow);
		miWinChromatogram.Visible = false;
		msChrom.Items.Add(miHelp);
		msChrom.Items.Add(new ToolStripSeparator());
		msChrom.Items.Add(mubtnMainForm);
		msChrom.Items.Add(mubtnInstrument);
		msChrom.Items.Add(new ToolStripSeparator());
		msChrom.Items.Add(mubtnCaliGnl);
		msChrom.Items.Add(mubtnCaliGpc);
		msChrom.Items.Add(mubtnDataAcq);
		msChrom.Items.Add(mubtnSglAly);
		msChrom.Items.Add(mubtnSeqAly);
		base.Icon = SystemIconResource.smethod_8();
		ResourceImageLoad.SetCtrlBitmap(btnOpen, SystemIconResource.smethod_31());
		ResourceImageLoad.SetCtrlBitmap(btnSave, SystemIconResource.smethod_37());
		ResourceImageLoad.SetCtrlBitmap(btnClose, SystemIconResource.smethod_18());
		ResourceImageLoad.SetCtrlBitmap(btnReportSetup, SystemIconResource.smethod_59());
		ResourceImageLoad.SetCtrlBitmap(btnPrtLink, SystemIconResource.smethod_60());
		ResourceImageLoad.SetCtrlBitmap(btnPreview, SystemIconResource.smethod_33());
		ResourceImageLoad.SetCtrlBitmap(btnPrint, SystemIconResource.smethod_35());
		ResourceImageLoad.SetCtrlBitmap(btnPreviousZoom, SystemIconResource.smethod_58());
		ResourceImageLoad.SetCtrlBitmap(btnNextZoom, SystemIconResource.smethod_56());
		ResourceImageLoad.SetCtrlBitmap(btnUnzoom, SystemIconResource.smethod_63());
		ResourceImageLoad.SetCtrlBitmap(btnProperties, SystemIconResource.smethod_57());
		ResourceImageLoad.SetCtrlBitmap(btnOverlayMode, SystemBitmapResource.smethod_14());
		ResourceImageLoad.SetCtrlBitmap(btngblDtecDelay, SystemBitmapResource.smethod_10());
		ResourceImageLoad.SetCtrlBitmap(btngblPeakWidth, SystemBitmapResource.smethod_15());
		ResourceImageLoad.SetCtrlBitmap(btngblThreshold, SystemBitmapResource.smethod_26());
		ResourceImageLoad.SetCtrlBitmap(btngblPkSlope, SystemBitmapResource.smethod_27());
		ResourceImageLoad.SetCtrlBitmap(btnipResetDtecNeg, SystemBitmapResource.smethod_24());
		ResourceImageLoad.SetCtrlBitmap(btnipClampNeg, SystemBitmapResource.smethod_9());
		ResourceImageLoad.SetCtrlBitmap(btnipPkWidth, SystemBitmapResource.smethod_23());
		ResourceImageLoad.SetCtrlBitmap(btnipPkThreshold, SystemBitmapResource.smethod_21());
		ResourceImageLoad.SetCtrlBitmap(btnipPkAddPosi, SystemBitmapResource.smethod_17());
		ResourceImageLoad.SetCtrlBitmap(btnipPkAddNeg, SystemBitmapResource.smethod_16());
		ResourceImageLoad.SetCtrlBitmap(btnipPkCut, SystemBitmapResource.smethod_19());
		ResourceImageLoad.SetCtrlBitmap(btnipPkHalfWidth, SystemBitmapResource.smethod_20());
		ResourceImageLoad.SetCtrlBitmap(btnipPkArea, SystemBitmapResource.smethod_18());
		ResourceImageLoad.SetCtrlBitmap(btnipPkVale, SystemBitmapResource.smethod_22());
		ResourceImageLoad.SetCtrlBitmap(btnipSolventPeak, SystemBitmapResource.smethod_25());
		ResourceImageLoad.SetCtrlBitmap(btnipFlowMarker, SystemBitmapResource.smethod_11());
		ResourceImageLoad.SetCtrlBitmap(btnipGroups, SystemBitmapResource.smethod_12());
		ResourceImageLoad.SetCtrlBitmap(btnbsBsTgnt, SystemBitmapResource.smethod_5());
		ResourceImageLoad.SetCtrlBitmap(btnbsBsVtV, SystemBitmapResource.smethod_8());
		ResourceImageLoad.SetCtrlBitmap(btnbsBsValley, SystemBitmapResource.smethod_7());
		ResourceImageLoad.SetCtrlBitmap(btnbsBsTogether, SystemBitmapResource.smethod_6());
		ResourceImageLoad.SetCtrlBitmap(btnbsBsForwHorz, SystemBitmapResource.smethod_2());
		ResourceImageLoad.SetCtrlBitmap(btnbsBsBackHorz, SystemBitmapResource.smethod_1());
		ResourceImageLoad.SetCtrlBitmap(btnbsBsFrontTgnt, SystemBitmapResource.smethod_3());
		ResourceImageLoad.SetCtrlBitmap(btnbsBsTailTgnt, SystemBitmapResource.smethod_4());
		ResourceImageLoad.SetCtrlBitmap(migblDtecDelay, SystemBitmapResource.smethod_10());
		ResourceImageLoad.SetCtrlBitmap(migblPeakWidth, SystemBitmapResource.smethod_15());
		ResourceImageLoad.SetCtrlBitmap(migblThreshold, SystemBitmapResource.smethod_26());
		ResourceImageLoad.SetCtrlBitmap(migblPkSlope, SystemBitmapResource.smethod_27());
		ResourceImageLoad.SetCtrlBitmap(miipResetDtecNeg, SystemBitmapResource.smethod_24());
		ResourceImageLoad.SetCtrlBitmap(miipClampNeg, SystemBitmapResource.smethod_9());
		ResourceImageLoad.SetCtrlBitmap(miipPkWidth, SystemBitmapResource.smethod_23());
		ResourceImageLoad.SetCtrlBitmap(miipPkThreshold, SystemBitmapResource.smethod_21());
		ResourceImageLoad.SetCtrlBitmap(miipPkAddPosi, SystemBitmapResource.smethod_17());
		ResourceImageLoad.SetCtrlBitmap(miipPkAddNeg, SystemBitmapResource.smethod_16());
		ResourceImageLoad.SetCtrlBitmap(miipPkCut, SystemBitmapResource.smethod_19());
		ResourceImageLoad.SetCtrlBitmap(miipPkHalfWidth, SystemBitmapResource.smethod_20());
		ResourceImageLoad.SetCtrlBitmap(miipPkArea, SystemBitmapResource.smethod_18());
		ResourceImageLoad.SetCtrlBitmap(miipPkVale, SystemBitmapResource.smethod_22());
		ResourceImageLoad.SetCtrlBitmap(miipSolventPeak, SystemBitmapResource.smethod_25());
		ResourceImageLoad.SetCtrlBitmap(miipFlowMarker, SystemBitmapResource.smethod_11());
		ResourceImageLoad.SetCtrlBitmap(miipGroups, SystemBitmapResource.smethod_12());
		ResourceImageLoad.SetCtrlBitmap(mibsBsTgnt, SystemBitmapResource.smethod_5());
		ResourceImageLoad.SetCtrlBitmap(mibsBsVtV, SystemBitmapResource.smethod_8());
		ResourceImageLoad.SetCtrlBitmap(mibsBsValley, SystemBitmapResource.smethod_7());
		ResourceImageLoad.SetCtrlBitmap(mibsBsTogether, SystemBitmapResource.smethod_6());
		ResourceImageLoad.SetCtrlBitmap(mibsBsForwHorz, SystemBitmapResource.smethod_2());
		ResourceImageLoad.SetCtrlBitmap(mibsBsBackHorz, SystemBitmapResource.smethod_1());
		ResourceImageLoad.SetCtrlBitmap(mibsBsFrontTgnt, SystemBitmapResource.smethod_3());
		ResourceImageLoad.SetCtrlBitmap(mibsBsTailTgnt, SystemBitmapResource.smethod_4());
		btnExpress_Click(null, null);
		spSignals.ButtonsNum = 12;
		SetPanelColors();
		tcGPC.tabStyle = TabStyle.Special;
		tcGPC.Alignment = TabAlignment.Bottom;
		Control control = dpgpcChrom;
		Control control2 = dpgpcMwDistrib;
		dpgpcCmlMw.Dock = DockStyle.Fill;
		control2.Dock = DockStyle.Fill;
		control.Dock = DockStyle.Fill;
		tcChrom.Dock = DockStyle.Fill;
		tcChrom.tabStyle = TabStyle.Special;
		tcChrom.Alignment = TabAlignment.Bottom;
		Control control3 = gvRltsGnl;
		Control control4 = gvRltsGpc;
		gvRltsDad.Dock = DockStyle.Fill;
		control4.Dock = DockStyle.Fill;
		control3.Dock = DockStyle.Fill;
		DataGridView dataGridView = gvRltsGnl;
		DataGridView dataGridView2 = gvRltsGpc;
		gvRltsDad.BorderStyle = BorderStyle.None;
		dataGridView2.BorderStyle = BorderStyle.None;
		dataGridView.BorderStyle = BorderStyle.None;
		integRow_1.oprtStyle = IntegOprtStyle.Noise;
		integRow_0.oprtStyle = IntegOprtStyle.Drift;
		LclPanel lclPanel = pnlcu;
		Control control5 = (pnlgcu.Parent = pnlRltsControl);
		lclPanel.Parent = control5;
		pnlcu.Location = new Point(0, 0);
		if (method_18())
		{
			method_13(gvRltsGnl);
			method_13(gvRltsGpc);
			method_13(gvRltsDad);
		}
		Control control7 = gvPerformStatic;
		gvPerformFrom50.Dock = DockStyle.Fill;
		control7.Dock = DockStyle.Fill;
		DataGridView dataGridView3 = gvPerformStatic;
		gvPerformFrom50.BorderStyle = BorderStyle.None;
		dataGridView3.BorderStyle = BorderStyle.None;
		method_17();
		method_11(gvPerformStatic);
		method_11(gvPerformFrom50);
		rbpfmFrom50per.Checked = true;
		rbpfmStatistical_Click(null, null);
		mipfmRestoreDftColumns_Click(mipfmRestoreDftColumns, null);
		tcMsmCdts.Dock = DockStyle.Fill;
		tcMsmCdts.tabStyle = TabStyle.Special;
		tcMsmCdts.Alignment = TabAlignment.Bottom;
		gbpdaLibs.Size = new Size(225, 100);
		gvlibPDA.Dock = DockStyle.Fill;
		gvlibPDA.BorderStyle = BorderStyle.None;
		OpenFileDialog openFileDialog = new OpenFileDialog();
		openFileDialog.Tag = "PDA";
		openFileDialog.Filter = Class49.MakeFileFilter(".lib");
		gvlibPDA.AddLclCheckBoxColumn("Used", 35);
		gvlibPDA.AddLclTextBoxCtxBtnColumn("Library", 140, StringAlignment.Near, openFileDialog).ReadOnly = true;
		method_10();
		gvlibPDA.RowCount = 13;
		gvlcGradient.BorderStyle = BorderStyle.None;
		MtdSetupDlg.Init_gvGradient(gvlcGradient);
		MtdSetupDlg.RefreshHeaders_gvGradient(gvlcGradient);
		gvgcProgTemp.BorderStyle = BorderStyle.None;
		MtdSetupDlg.Init_gvgcProgTemp(gvgcProgTemp);
		dgvCT6.RowCount = 6;
		dgvCT6.Rows[0].Cells[0].Value = Lang.PS("进样器1", "Inj.1");
		dgvCT6.Rows[1].Cells[0].Value = Lang.PS("柱炉", "Envolve");
		dgvCT6.Rows[2].Cells[0].Value = Lang.PS("检测器1", "Dtc1");
		dgvCT6.Rows[3].Cells[0].Value = Lang.PS("辅1/进2", "Aux.1/Inj.2");
		dgvCT6.Rows[4].Cells[0].Value = Lang.PS("辅2/检2", "Aux.2/Dtc.2");
		dgvCT6.Rows[5].Cells[0].Value = Lang.PS("热导", "Therm.");
		lbSSTFile.SetText(Lang.PS("文件： ", "File: ") + "[]");
		sstparasDlg_0 = new SSTParasDlg();
		gvSSTCmpds.Dock = DockStyle.Fill;
		gvSSTCmpds.BorderStyle = BorderStyle.None;
		gvSSTResults.Dock = DockStyle.Fill;
		gvSSTResults.BorderStyle = BorderStyle.None;
		gvSSTCmpds.AddLclCheckBoxColumn("Used", 35);
		gvSSTCmpds.AddLclgvIconColumn("OK", 20).ReadOnly = true;
		gvSSTCmpds.AddLclTextBoxColumn("CmpdName", 150, StringAlignment.Near).ReadOnly = true;
		gvSSTCmpds.AddLclTextBoxColumn("RetenTime", 60, StringAlignment.Center).ReadOnly = true;
		method_21();
		method_15();
		gvSSTResults.SetChromCount(0);
		gvSSTResults.Rows[0].Cells["Chrom"].Value = Lang.PS("上限", "Upper Limit");
		gvSSTResults.Rows[1].Cells["Chrom"].Value = Lang.PS("下限", "Lower Limit");
		gvSSTResults.Rows[2].Cells["Chrom"].Value = Lang.PS("RSD% 要求", "RSD% Limit");
		gvSSTResults.Rows[3].Cells["Chrom"].Value = Lang.PS("均值", "Average");
		gvSSTResults.Rows[4].Cells["Chrom"].Value = "RSD[%]";
		gvSSTResults.Rows[5].Cells["Chrom"].Value = Lang.PS("参数结果", "Parameter Result");
		misstRestoreDftColumns_Click(misstRestoreDftColumns, null);
		gvSlices.Dock = DockStyle.Fill;
		gvSlices.BorderStyle = BorderStyle.None;
		method_20();
		method_14();
		mislcRestoreDftColumns_Click(mislcRestoreDftColumns, null);
		gvSlices.RowCount = 3;
		gvgrPercent.BorderStyle = BorderStyle.None;
		gvgrMw.BorderStyle = BorderStyle.None;
		gvgrPercent.AddLclTextBoxColumn("LowPercent", 110, StringAlignment.Center);
		gvgrPercent.AddLclTextBoxColumn("HightPercent", 110, StringAlignment.Center);
		gvgrPercent.AddLclTextBoxColumn("ResultMw", 110, StringAlignment.Center);
		gvgrMw.AddLclTextBoxColumn("HighMw", 110, StringAlignment.Center);
		gvgrMw.AddLclTextBoxColumn("LowMw", 110, StringAlignment.Center);
		gvgrMw.AddLclTextBoxColumn("ResultPercent", 110, StringAlignment.Center);
		method_8();
		gvgrPercent.RowCount = 4;
		gvgrMw.RowCount = 3;
		spltcArchives.Dock = DockStyle.Fill;
		Control control8 = gvSetRights;
		gvArchives.Dock = DockStyle.Fill;
		control8.Dock = DockStyle.Fill;
		DataGridView dataGridView4 = gvSetRights;
		gvArchives.BorderStyle = BorderStyle.None;
		dataGridView4.BorderStyle = BorderStyle.None;
		method_19();
		method_12(gvSetRights);
		method_12(gvArchives);
		method_41(bool_6: false);
		InstruWinsInfo instruWinsInfo = instrument.user.instrusWinsInfo[instrument.pageNo];
		if (instruWinsInfo.valid)
		{
			ReadWinInfo(instruWinsInfo.winInfos[4]);
		}
		bool flag = gvRltsGnl.LoadFromManager();
		bool flag2 = gvRltsGpc.LoadFromManager();
		bool flag3 = gvRltsDad.LoadFromManager();
		if (!flag && !flag2 && !flag3)
		{
			mirltsResetCmpdNames_Click(mirltsRestoreDftColumns, null);
		}
	}

	private string[] method_4(Chromatogram[] chromatogram_2)
	{
		string[] string_ = new string[0];
		if (smyTabOpt_0.smyTabRpt == SmyTabRpt.AllIdentifiedPeaks)
		{
			foreach (Chromatogram chromatogram in chromatogram_2)
			{
				for (int j = 0; j < chromatogram.PeaksNum; j++)
				{
					if (chromatogram.RltPeaks[j].IsIdentified)
					{
						Class49.Append2Array(ref string_, chromatogram.RltPeaks[j].name, bool_5: true);
					}
				}
			}
		}
		if (smyTabOpt_0.smyTabRpt == SmyTabRpt.AllPeaksInCali)
		{
			foreach (Chromatogram chromatogram2 in chromatogram_2)
			{
				if (chromatogram2.caliGnl != null)
				{
					for (int l = 0; l < chromatogram2.caliGnl.cmpds.Length; l++)
					{
						Class49.Append2Array(ref string_, chromatogram2.caliGnl.cmpds[l].cmpdInfo.name, bool_5: true);
					}
				}
			}
		}
		return string_;
	}

	private void miitgPaste_Click(object sender, EventArgs e)
	{
		if (!HasChrom)
		{
			return;
		}
		if (sender == miitgUndo)
		{
			VIChromForm_KeyDown(null, new KeyEventArgs(Keys.Z | Keys.Control));
		}
		if (sender == miitgRedo)
		{
			VIChromForm_KeyDown(null, new KeyEventArgs(Keys.Z | Keys.Shift | Keys.Control));
		}
		if (sender == miitgReset)
		{
			CurChrom.integ.Reset();
			method_23();
		}
		if (sender == miitgAppendRow)
		{
			CurChrom.integ.AppendRow(default(IntegRow));
			method_23();
		}
		if (sender == miitgInsertRow)
		{
			int num = -1;
			for (int i = 0; i < gvInteg.RowCount; i++)
			{
				if (gvInteg.Rows[i].Selected)
				{
					num = i;
					if (num < 0)
					{
						return;
					}
					num = Math.Max(3, num);
					CurChrom.integ.InsertNullRow(num);
					method_23();
					break;
				}
			}
		}
		if (sender == miitgDelete)
		{
			if (gvInteg.SelectedRows.Count == 0)
			{
				return;
			}
			int[] array = new int[0];
			for (int j = 0; j < gvInteg.SelectedRows.Count; j++)
			{
				int index = gvInteg.SelectedRows[j].Index;
				if (index >= 3)
				{
					int num2 = array.Length;
					Array.Resize(ref array, num2 + 1);
					array[num2] = index;
				}
			}
			if (array.Length != 0)
			{
				for (int k = 0; k < array.Length; k++)
				{
					gvInteg.Rows[array[k]].Selected = false;
				}
				CurChrom.integ.DeleteRows(array);
				method_23();
			}
		}
		if (sender == miitgCopy)
		{
			try
			{
				if (gvInteg.SelectedRows.Count == 0)
				{
					return;
				}
				int[] array2 = new int[0];
				for (int l = 0; l < gvInteg.SelectedRows.Count; l++)
				{
					int num3 = array2.Length;
					Array.Resize(ref array2, num3 + 1);
					array2[num3] = gvInteg.SelectedRows[l].Index;
				}
				Array.Sort(array2);
				string text = "";
				foreach (int num4 in array2)
				{
					IntegRow integRow = CurChrom.integ.IntegRows[num4];
					string text2 = text;
					text = text2 + integRow.oprtStyle.ToString() + ":" + ((integRow.group != 0) ? integRow.group.ToString() : "*") + ":" + integRow.timeA + ":" + integRow.timeB + ":" + integRow.value + ((integRow.oprtStyle == IntegOprtStyle.BsTgnt) ? ("," + integRow.value2) : "") + "\n";
				}
				Clipboard.SetText(text);
			}
			catch
			{
				return;
			}
		}
		if (sender != miitgPaste)
		{
			return;
		}
		try
		{
			if (!Clipboard.ContainsText())
			{
				return;
			}
			string[] array3 = Clipboard.GetText().Split('\n');
			IntegRow[] array4 = new IntegRow[array3.Length];
			int num5 = 0;
			for (int n = 0; n < array3.Length; n++)
			{
				if (array3[n].Contains(":"))
				{
					string[] array5 = array3[n].Split(':');
					if (array5.Length != 5)
					{
						return;
					}
					array4[num5++].Parse(array5);
				}
			}
			if (num5 != 0)
			{
				Array.Resize(ref array4, num5);
				CurChrom.integ.AppendRows(array4);
				method_23();
			}
		}
		catch
		{
		}
	}

	private void mipfmRestoreDftColumns_Click(object sender, EventArgs e)
	{
		lclGridView_0 = gvPerformStatic;
		if (rbpfmFrom50per.Checked)
		{
			lclGridView_0 = gvPerformFrom50;
		}
		if (sender == mipfmColumnsSetup)
		{
			columnsSetupDlg_0.ShowDialog(lclGridView_0);
		}
		else if (sender == mipfmRestoreDftColumns)
		{
			if (rbpfmStatistical.Checked)
			{
				gvPerformStatic.ini_SetFirstVisibleColumn("RetenTime");
				gvPerformStatic.ini_SetNextVisibleColumn("Centroid");
				gvPerformStatic.ini_SetNextVisibleColumn("Variance");
				gvPerformStatic.ini_SetNextVisibleColumn("Skew");
				gvPerformStatic.ini_SetNextVisibleColumn("Excess");
				gvPerformStatic.ini_SetNextVisibleColumn("Efficiency");
				gvPerformStatic.ini_SetNextVisibleColumn("Eff_ColL");
				gvPerformStatic.ini_SetNextVisibleColumn("CmpdName");
				gvPerformStatic.ini_FinishVisibleColumn();
			}
			if (rbpfmFrom50per.Checked)
			{
				gvPerformFrom50.ini_SetFirstVisibleColumn("RetenTime");
				gvPerformFrom50.ini_SetNextVisibleColumn("WO5");
				gvPerformFrom50.ini_SetNextVisibleColumn("Asymmetry");
				gvPerformFrom50.ini_SetNextVisibleColumn("Capacity");
				gvPerformFrom50.ini_SetNextVisibleColumn("Efficiency");
				gvPerformFrom50.ini_SetNextVisibleColumn("Eff_ColL");
				gvPerformFrom50.ini_SetNextVisibleColumn("Resolution");
				gvPerformFrom50.ini_SetNextVisibleColumn("CmpdName");
				gvPerformFrom50.ini_FinishVisibleColumn();
			}
		}
	}

	private void mirltsResetCmpdNames_Click(object sender, EventArgs e)
	{
		if (sender == mirltsColumnsSetup)
		{
			if (columnsSetupDlg_1.ShowDialog(lclGridView_1) == DialogResult.OK)
			{
				method_33();
			}
		}
		else if (sender == mirltsRestoreDftColumns)
		{
			if (instrument.instruStyle == InstruStyle.LC || instrument.instruStyle == InstruStyle.GC)
			{
				gvRltsGnl.ini_SetFirstVisibleColumn("CmpdName");
				gvRltsGnl.ini_SetNextVisibleColumn("PeakStyle");
				gvRltsGnl.ini_SetNextVisibleColumn("RetenTime");
				gvRltsGnl.ini_SetNextVisibleColumn("Area");
				gvRltsGnl.ini_SetNextVisibleColumn("AreaPer");
				gvRltsGnl.ini_SetNextVisibleColumn("Height");
				gvRltsGnl.ini_SetNextVisibleColumn("HeightPer");
				gvRltsGnl.ini_SetNextVisibleColumn("Amount");
				gvRltsGnl.ini_SetNextVisibleColumn("AmountPer");
				gvRltsGnl.ini_FinishVisibleColumn();
			}
			if (instrument.instruStyle == InstruStyle.PDA)
			{
				gvRltsDad.ini_SetFirstVisibleColumn("CmpdName");
				gvRltsDad.ini_SetNextVisibleColumn("RetenTime");
				gvRltsDad.ini_SetNextVisibleColumn("PeakPurity");
				gvRltsDad.ini_SetNextVisibleColumn("NameMatch");
				gvRltsDad.ini_SetNextVisibleColumn("BestMatchName");
				gvRltsDad.ini_SetNextVisibleColumn("BestMatch");
				gvRltsDad.ini_SetNextVisibleColumn("Area");
				gvRltsDad.ini_SetNextVisibleColumn("Amount");
				gvRltsDad.ini_FinishVisibleColumn();
			}
			if (instrument.instruStyle == InstruStyle.GPC)
			{
				gvRltsGpc.ini_SetFirstVisibleColumn("CmpdName");
				gvRltsGpc.ini_SetNextVisibleColumn("MaxRT");
				gvRltsGpc.ini_SetNextVisibleColumn("StartRT");
				gvRltsGpc.ini_SetNextVisibleColumn("EndRT");
				gvRltsGpc.ini_SetNextVisibleColumn("Mn");
				gvRltsGpc.ini_SetNextVisibleColumn("Mw");
				gvRltsGpc.ini_SetNextVisibleColumn("Area");
				gvRltsGpc.ini_SetNextVisibleColumn("AreaPer");
				gvRltsGpc.ini_FinishVisibleColumn();
			}
			if (lclGridView_1 != null)
			{
				method_33();
			}
		}
		else if (sender == mirltsResetCmpdNames)
		{
			if (!HasChrom)
			{
				return;
			}
			for (int i = 0; i < CurChrom.PeaksNum; i++)
			{
				CurChrom.RltPeaks[i].name = "";
			}
			method_33();
		}
		if (lclGridView_1 != null)
		{
			chromDisplay_0.fmtPeakRT = lclGridView_1.ConvertValFmt("RetenTime");
		}
		DisDpRefresh();
	}

	private void mislcRestoreDftColumns_Click(object sender, EventArgs e)
	{
		if (sender == mislcColumnsSetup)
		{
			columnsSetupDlg_2.ShowDialog(gvSlices);
		}
		else if (sender == mislcRestoreDftColumns)
		{
			gvSlices.ini_SetFirstVisibleColumn("RetenTime");
			gvSlices.ini_SetNextVisibleColumn("Response");
			gvSlices.ini_SetNextVisibleColumn("NormHtPer");
			gvSlices.ini_SetNextVisibleColumn("CumHtPer");
			gvSlices.ini_SetNextVisibleColumn("M");
			gvSlices.ini_SetNextVisibleColumn("LogM");
			gvSlices.ini_SetNextVisibleColumn("dWdLogM");
			gvSlices.ini_SetNextVisibleColumn("W");
			gvSlices.ini_SetNextVisibleColumn("OutsideCalib");
			gvSlices.ini_FinishVisibleColumn();
		}
	}

	private void misstRestoreDftColumns_Click(object sender, EventArgs e)
	{
		if (sender == misstColumnsSetup)
		{
			columnsSetupDlg_4.ShowDialog(gvSSTResults);
		}
		else if (sender == misstRestoreDftColumns)
		{
			gvSSTResults.ini_SetFirstVisibleColumn("RetenTime");
			gvSSTResults.ini_SetNextVisibleColumn("Area");
			gvSSTResults.ini_SetNextVisibleColumn("Height");
			gvSSTResults.ini_SetNextVisibleColumn("Amount");
			gvSSTResults.ini_FinishVisibleColumn();
		}
		method_37();
	}

	private void mismySmyOpt_Click(object sender, EventArgs e)
	{
		if (sender != mismyColumnsSetup)
		{
			if (sender == mismyRestoreDftColumns)
			{
				gvSummary.ArrayComSHColumns(show: true, 3);
				gvSummary.AddComShowLink(0, "ChromName");
				gvSummary.AddComShowLink(1, "SampleAmount");
				gvSummary.AddComShowLink(2, "InjVol");
				gvSummary.FinishComHideLinks();
				method_45(instrument.instruStyle);
				smyTabOpt_0.smyHdrPara = SmyHdrPara.Cmpd_Para;
				method_36();
			}
			else if (sender == mismySmyOpt)
			{
				miRltSmyOpt_Click(null, null);
			}
			return;
		}
		DialogResult dialogResult = DialogResult.Cancel;
		switch (instrument.instruStyle)
		{
		case InstruStyle.GC:
		case InstruStyle.LC:
			dialogResult = columnsSetupDlg_3.ShowDialog(gvSummary, instrument.instruStyle, Lang.PS("一般列", "Common"), Lang.PS("总结列", "Summary"), gvSummary.commonColumns, gvSummary.showComColumns, gvSummary.hideComColumns, gvSummary.smyGnlColumns, gvSummary.showGnlColumns, gvSummary.hideGnlColumns);
			break;
		case InstruStyle.GPC:
			dialogResult = columnsSetupDlg_3.ShowDialog(gvSummary, instrument.instruStyle, Lang.PS("一般列", "Common"), Lang.PS("总结列", "Summary"), gvSummary.commonColumns, gvSummary.showComColumns, gvSummary.hideComColumns, gvSummary.smyGpcColumns, gvSummary.showGpcColumns, gvSummary.hideGpcColumns);
			break;
		case InstruStyle.PDA:
			dialogResult = columnsSetupDlg_3.ShowDialog(gvSummary, instrument.instruStyle, Lang.PS("一般列", "Common"), Lang.PS("总结列", "Summary"), gvSummary.commonColumns, gvSummary.showComColumns, gvSummary.hideComColumns, gvSummary.smyDadColumns, gvSummary.showDadColumns, gvSummary.hideDadColumns);
			break;
		}
		if (dialogResult == DialogResult.OK)
		{
			method_36();
		}
	}

	private string method_5(PeakStyle peakStyle_0)
	{
		return peakStyle_0 switch
		{
			PeakStyle.Single => Lang.PS("单峰", "Single"), 
			PeakStyle.Overlap => Lang.PS("重叠峰", "Overlap"), 
			PeakStyle.Shoulder => Lang.PS("肩峰", "Shoulder"), 
			PeakStyle.SO => Lang.PS("肩叠峰", "Sh.Over"), 
			_ => "", 
		};
	}

	public void DisDpRefresh()
	{
		chromDisplay_0.displayPanel.Refresh();
		if (chromDisplay_0.stDisChain.Count != 0)
		{
			disLg_0 = chromDisplay_0.stDisChain.CurDisLg;
		}
	}

	private void method_6()
	{
		CurChrom.integ.AppendRow(manuDlg_0.GetNewIntegRow());
		method_25();
	}

	private void method_7()
	{
		lbExpress.Text = Lang.PS("感应取值", "Suggest Value");
		method_0(bool_6: true);
	}

	private void dpgpcChrom_Click(object sender, EventArgs e)
	{
		if (HasChrom)
		{
			if (dpgnlChrom.Cursor == Cursors.IBeam && lbTextDlg_0.ShowDialog() == DialogResult.OK && lbTextDlg_0.lbText != null)
			{
				lbTextDlg_0.lbText.pointF_0 = chromDisplay_0.ClickLgV();
				CurChrom.signal.AddLbText(lbTextDlg_0.lbText);
			}
			CurChrom.signal.ResetSelectLbs();
			CurChrom.signal.ClickLb(point_1);
			int num = chromDisplay_0.GraphClick();
			for (int i = 0; i < CurChrom.PeaksNum; i++)
			{
				CurChrom.RltPeaks[i].selected = CurChrom.RltPeaks[i].pkN == num;
			}
			tcChrom_SelectedIndexChanged(null, null);
		}
	}

	private void dpgpcChrom_MouseDown(object sender, MouseEventArgs e)
	{
		if (!HasChrom)
		{
			return;
		}
		if (e.Button == MouseButtons.Left)
		{
			lbLineDlg_0.pointF_0 = chromDisplay_0.scrToLg(e.Location, bool_0: true);
			chromDisplay_0.ptScaleBegin = e.Location;
			if (byte_0 != 0)
			{
				byte_0++;
			}
			if (byte_0 == 2)
			{
				PointF pointF_ = new PointF
				{
					X = integRow_2.timeA,
					Y = chromatogram_1.signal.getDotY(integRow_2.timeA)
				};
				chromDisplay_0.DrawL_VtrBl(pointF_, 1);
				if (!bool_5)
				{
					lbExpress.Text = integRow_2.ExpString(byte_0);
				}
				Cursor.Position = new Point(Cursor.Position.X + 10, Cursor.Position.Y);
			}
			if (byte_0 != 3)
			{
				return;
			}
			if (integRow_2.oprtStyle != IntegOprtStyle.PkVale)
			{
				integRow_2.ArrTime();
			}
			method_26(bool_6: true);
			DisDpRefresh();
			if (bool_5)
			{
				if (integRow_2.oprtStyle == IntegOprtStyle.PkWidth || integRow_2.oprtStyle == IntegOprtStyle.PkHalfWidth)
				{
					integRow_2.value = CurSignal.getWx(integRow_2.timeA, integRow_2.timeB);
				}
				if (integRow_2.oprtStyle == IntegOprtStyle.PkThreshold)
				{
					integRow_2.value = CurSignal.getAy(integRow_2.timeA, integRow_2.timeB);
				}
				if (integRow_2.oprtStyle == IntegOprtStyle.BsVtV)
				{
					integRow_2.value = CurSignal.getHy(integRow_2.timeA, integRow_2.timeB) / CurSignal.getWx(integRow_2.timeA, integRow_2.timeB);
				}
				manuDlg_0.RefreshValue(gvInteg, integRow_2);
				manuDlg_0.Visible = true;
			}
			else
			{
				method_27();
			}
		}
		else if (e.Button == MouseButtons.Right)
		{
			chromDisplay_0.mouseLocation = (point_2 = e.Location);
			if (byte_0 != 0)
			{
				method_26(bool_6: false);
			}
		}
	}

	private void dpgpcChrom_MouseLeave(object sender, EventArgs e)
	{
		if (byte_0 != 0)
		{
			rectangle_0.Width = dpgnlChrom.Width - 10;
			if (!rectangle_0.Contains(point_0))
			{
				method_26(bool_6: true);
			}
		}
	}

	private void dpgpcChrom_MouseMove(object sender, MouseEventArgs e)
	{
		point_0 = e.Location;
		if (!HasChrom)
		{
			return;
		}
		if (e.Button == MouseButtons.Left)
		{
			if (byte_0 == 0)
			{
				chromDisplay_0.mouseLocation = e.Location;
				if (dpgnlChrom.Cursor == Cursors.Default)
				{
					chromDisplay_0.scaling = true;
					chromDisplay_0.DrawScale_moving();
				}
				if (dpgnlChrom.Cursor == Cursors.SizeNWSE)
				{
					chromDisplay_0.DrawLbLine_moving();
				}
				chromDisplay_0.DrawMouseLgValue();
			}
			return;
		}
		if (e.Button == MouseButtons.Right)
		{
			if (byte_0 == 0)
			{
				chromDisplay_0.displayPanel.Cursor = Cursors.SizeAll;
				if (!chromDisplay_0.moving)
				{
					chromDisplay_0.stDisChain.MustAppendFrameLg(disLg_0);
				}
				Size szScr = new Size(e.X - chromDisplay_0.mouseLocation.X, e.Y - chromDisplay_0.mouseLocation.Y);
				SizeF sizeF = chromDisplay_0.scrToLg(szScr, bool_0: true);
				disLg_0.lgXBeg -= sizeF.Width;
				disLg_0.lgYBeg += sizeF.Height;
				chromDisplay_0.stDisChain.ReplaceCurFrameLg(disLg_0);
				chromDisplay_0.moving = true;
				chromDisplay_0.mouseLocation = e.Location;
				DisDpRefresh();
			}
			return;
		}
		chromDisplay_0.mouseLocation = e.Location;
		chromDisplay_0.DrawMouseLgValue();
		if (byte_0 == 1)
		{
			integRow_2.timeA = chromDisplay_0.DrawL(e.X, 1);
			bool_0 = false;
		}
		if (byte_0 == 2)
		{
			if (!bool_0)
			{
				chromDisplay_0.DrawL_add();
				bool_0 = true;
			}
			pointF_0.X = (integRow_2.timeB = chromDisplay_0.DrawL(e.X, 2));
			pointF_0.Y = chromatogram_1.signal.getDotY(integRow_2.timeB);
			chromDisplay_0.DrawL_VtrBl(pointF_0, 2);
		}
	}

	private void dpgpcChrom_MouseUp(object sender, MouseEventArgs e)
	{
		point_1 = e.Location;
		if (dpgnlChrom.Cursor == Cursors.SizeNWSE)
		{
			chromDisplay_0.DrawLbLine_end();
			if (lbLineDlg_0.ShowDialog() == DialogResult.OK)
			{
				lbLineDlg_0.lbLine.pointF_2 = chromDisplay_0.scrToLg(e.Location, bool_0: true);
				CurChrom.signal.AddLbLine(lbLineDlg_0.lbLine);
			}
			DisDpRefresh();
		}
		chromDisplay_0.displayPanel.Cursor = Cursors.Default;
		if (chromDisplay_0.moving)
		{
			DisDpRefresh();
			method_38();
		}
		if (chromDisplay_0.scaling)
		{
			chromDisplay_0.DrawScale_end();
			if (Math.Abs(chromDisplay_0.ptScaleBegin.X - chromDisplay_0.mouseLocation.X) > 10 && Math.Abs(chromDisplay_0.ptScaleBegin.Y - chromDisplay_0.mouseLocation.Y) > 10)
			{
				if (chromDisplay_0.mouseLocation.X < chromDisplay_0.ptScaleBegin.X)
				{
					btnUnzoom_Click(null, null);
				}
				else
				{
					PointF pointF = chromDisplay_0.scrToLg(chromDisplay_0.ptScaleBegin, bool_0: true);
					PointF pointF2 = chromDisplay_0.scrToLg(chromDisplay_0.mouseLocation, bool_0: true);
					rectangleF_0.X = Math.Min(pointF.X, pointF2.X);
					rectangleF_0.Y = Math.Min(pointF.Y, pointF2.Y);
					rectangleF_0.Width = Math.Max(pointF.X, pointF2.X) - rectangleF_0.X;
					rectangleF_0.Height = Math.Max(pointF.Y, pointF2.Y) - rectangleF_0.Y;
					method_31(rectangleF_0.X, rectangleF_0.Width, rectangleF_0.Y, rectangleF_0.Height);
				}
				chromDisplay_0.scaling = false;
				DisDpRefresh();
				method_38();
			}
		}
		chromDisplay_0.moving = false;
		chromDisplay_0.scaling = false;
		if (e.Button == MouseButtons.Right)
		{
			if (byte_0 == 0)
			{
			}
			byte_0 = 0;
		}
	}

	private void dpgpcChrom_Paint(object sender, PaintEventArgs e)
	{
		chromDisplay_0.Draw(e.Graphics, erase: true);
	}

	private void dpgnlChrom_Resize(object sender, EventArgs e)
	{
		tcGPC.Height = dpgnlChrom.Height;
		lbExpress.Width = dpgnlChrom.Width - 2;
	}

	public void GetItgDisColumns(ref GvInfos gvInfos)
	{
		Class49.SetGridViewInfo(gvInteg, ref gvInfos, null);
		for (int i = 0; i < gvInfos.colHdrTxts.Length; i++)
		{
			int num = 80;
			string text = gvInfos.colNames[i];
			if (text != null)
			{
				switch (text)
				{
				case "Unit":
					num = 45;
					break;
				case "Group":
					num = 45;
					gvInfos.colAligns[i] = StringAlignment.Center;
					break;
				case "ChromOprt":
					num = 115;
					break;
				}
			}
			gvInfos.colWidths[i] = num;
		}
	}

	public void GetPfmDisColumns(ref GvInfos gvInfos)
	{
		if (method_17())
		{
			method_11(gvPerformFrom50);
			rbpfmFrom50per.Checked = true;
			rbpfmStatistical_Click(null, null);
			mipfmRestoreDftColumns_Click(mipfmRestoreDftColumns, null);
		}
		Class49.SetGridViewInfo(gvPerformFrom50, ref gvInfos, null);
		for (int i = 0; i < gvInfos.colHdrTxts.Length; i++)
		{
			int num = 80;
			string text = gvInfos.colNames[i];
			if (text != null && text == "CmpdName")
			{
				num = 115;
			}
			gvInfos.colWidths[i] = num;
		}
	}

	public void GetRltDisColumns(ref GvInfos gvInfos)
	{
		if (method_18())
		{
			method_13(gvRltsGnl);
			method_13(gvRltsGpc);
			method_13(gvRltsDad);
			InstruWinsInfo instruWinsInfo = instrument.user.instrusWinsInfo[instrument.pageNo];
			if (instruWinsInfo.valid)
			{
				ReadWinInfo(instruWinsInfo.winInfos[4]);
			}
			bool flag = gvRltsGnl.LoadFromManager();
			bool flag2 = gvRltsGpc.LoadFromManager();
			bool flag3 = gvRltsDad.LoadFromManager();
			if (!flag && !flag2 && !flag3)
			{
				mirltsResetCmpdNames_Click(mirltsRestoreDftColumns, null);
			}
		}
		Class49.SetGridViewInfo(lclGridView_1, ref gvInfos, null);
		for (int i = 0; i < gvInfos.colHdrTxts.Length; i++)
		{
			int num = 80;
			string text = gvInfos.colNames[i];
			if (text != null && (text == "CmpdName" || text == "BestMatchName"))
			{
				num = 115;
			}
			gvInfos.colWidths[i] = num;
		}
	}

	public void GetSmyColumns(Chromatogram[] chroms, ref GvInfos gvInfos, ref SmyHdrPara smyHdrPara)
	{
		method_36();
		Class49.SetGridViewInfo(gvSummary, ref gvInfos, null);
		for (int i = 0; i < gvInfos.colHdrTxts.Length; i++)
		{
			int num = 80;
			string text = gvInfos.colNames[i];
			if (text != null && (text == "ChromName" || text == "Sample"))
			{
				num = 115;
			}
			gvInfos.colWidths[i] = num;
		}
		smyHdrPara = smyTabOpt_0.smyHdrPara;
	}

	public string getSmyHeaderText(string name)
	{
		return name switch
		{
			"ChromName" => Lang.PS("谱图", "Chromatogram"), 
			"SampleID" => Lang.PS("样品ID", "Chromatogram"), 
			"Sample" => Lang.PS("样品", "Chromatogram"), 
			"SampleAmount" => Lang.PS("样品\n数量", "Chromatogram"), 
			"SampleDilution" => Lang.PS("稀释", "Chromatogram"), 
			"ISTDAmount" => Lang.PS("内标\n数量", "Chromatogram"), 
			"InjVol" => Lang.PS("体积\n[", "Chromatogram") + instrument.form.seqAlyForm.CurSeqAly.seqAlyOpt.injVolumnUnit.ToString() + "]", 
			"ColumnUT" => Lang.PS("非保留\n时间\n[min]", "Chromatogram"), 
			"ColumnLength" => Lang.PS("柱长\n[mm]", "Chromatogram"), 
			"Noise" => Lang.PS("噪音", "Chromatogram"), 
			"Drift" => Lang.PS("漂移", "Chromatogram"), 
			"StartTime" => Lang.PS("开始时间\n[min]", "Chromatogram"), 
			"EndTime" => Lang.PS("结束时间\n[min]", "Chromatogram"), 
			"StartValue" => string_280, 
			"EndValue" => string_278, 
			"WO5" => Lang.PS("半峰宽\n[min]", "Chromatogram"), 
			"RespBase" => Lang.PS("响应基础", "Chromatogram"), 
			"RetenIndex" => Lang.PS("保留索引\n[-]", "Chromatogram"), 
			"Area" => string_277, 
			"Height" => string_279, 
			"AreaPer" => Lang.PS("面积\n[%]", "Chromatogram"), 
			"HeightPer" => Lang.PS("高度\n[%]", "Chromatogram"), 
			"RetenTime" => Lang.PS("保留时间\n[min]", "Chromatogram"), 
			"Amount" => Lang.PS("数量", "Chromatogram"), 
			"AmountPer" => Lang.PS("数量\n[%]", "Chromatogram"), 
			"PeakType" => Lang.PS("峰类型", "Chromatogram"), 
			"CmpdName" => Lang.PS("组分名", "Chromatogram"), 
			"PeakPurity" => Lang.PS("峰纯度", "Chromatogram"), 
			"NameMatch" => Lang.PS("名匹配", "Chromatogram"), 
			"BestMatchName" => Lang.PS("最佳匹配名", "Chromatogram"), 
			"BestMatch" => Lang.PS("最佳匹配", "Chromatogram"), 
			"MaxRT" => Lang.PS("最大RT\n[min]", "Chromatogram"), 
			"StartRT" => Lang.PS("开始RT\n[min]", "Chromatogram"), 
			"EndRT" => Lang.PS("结束RT\n[min]", "Chromatogram"), 
			"FlowRateCorr" => Lang.PS("流速校正", "Chromatogram"), 
			"Mp" => "Mp", 
			"Mn" => "Mn", 
			"Mw" => "Mw", 
			"Mz" => "Mz", 
			"Mz1" => "Mz1", 
			"Mv" => "Mv", 
			"PD" => "PD", 
			_ => "", 
		};
	}

	public void GetSstDisColumns(ref GvInfos gvInfos)
	{
		if (method_21())
		{
			gvSSTResults.Columns["Chrom"].HeaderText = "谱图";
			method_11(gvSSTResults);
			method_13(gvSSTResults);
			misstRestoreDftColumns_Click(misstRestoreDftColumns, null);
		}
		Class49.SetGridViewInfo(gvSSTResults, ref gvInfos, null);
		for (int i = 0; i < gvInfos.colHdrTxts.Length; i++)
		{
			int num = 80;
			string text = gvInfos.colNames[i];
			if (text != null)
			{
				if (!(text == "X"))
				{
					if (text == "Chrom")
					{
						num = 130;
					}
				}
				else
				{
					num = 20;
				}
			}
			gvInfos.colWidths[i] = num;
		}
	}

	private void gvArchives_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
	{
		if (gvArchives.SelectedRows.Count == 1)
		{
			UserArchive userArchive = gvArchives.SelectedRows[0].Tag as UserArchive;
			CurChrom.idxUserArchive = gvArchives.SelectedRows[0].Index;
			CurChrom.chromInfo.LoadFromObject(userArchive.chromInfo);
			CurChrom.integ.LoadFromObject(userArchive.integ);
			userArchive.SL_lbTexts(load: false, ref CurChrom.signal.lbTexts);
			userArchive.SL_lbLines(load: false, ref CurChrom.signal.lbLines);
			method_23();
		}
	}

	private void gvArchives_SelectionChanged(object sender, EventArgs e)
	{
		tbrtaRemark.Text = "";
		if (gvArchives.SelectedRows.Count == 1 && gvArchives.SelectedRows[0].Tag != null)
		{
			UserArchive userArchive = gvArchives.SelectedRows[0].Tag as UserArchive;
			tbrtaRemark.Text = userArchive.remark;
		}
	}

	private void method_8()
	{
		gvgrPercent.Columns["LowPercent"].HeaderText = "低百分比";
		gvgrPercent.Columns["HightPercent"].HeaderText = "高百分比";
		gvgrPercent.Columns["ResultMw"].HeaderText = "Mw 结果";
		gvgrMw.Columns["HighMw"].HeaderText = "高分子量";
		gvgrMw.Columns["LowMw"].HeaderText = "低分子量";
		gvgrMw.Columns["ResultPercent"].HeaderText = "百分比结果";
	}

	private void method_9()
	{
		if (HasChrom)
		{
			gvInteg.Refresh(AccStyle.Write, CurChrom.integ);
			method_23();
			CurChrom.integ.ResetUndoIndex();
		}
	}

	private void method_10()
	{
		gvlibPDA.Columns["Used"].HeaderText = "使用";
		gvlibPDA.Columns["Library"].HeaderText = "匹配库";
	}

	private void method_11(LclGridView lclGridView_2)
	{
		if (lclGridView_2.ColumnCount == 0)
		{
			return;
		}
		for (int i = 0; i < lclGridView_2.ColumnCount; i++)
		{
			string name;
			switch (name = lclGridView_2.Columns[i].Name)
			{
			case "RetenTime":
				lclGridView_2.Columns[i].HeaderText = Lang.PS("保留时间\n[min]", "Reten Time\n[min]");
				break;
			case "Efficiency":
				lclGridView_2.Columns[i].HeaderText = Lang.PS("效率\n[th.pl]", "Reten Time\n[min]");
				break;
			case "Eff_ColL":
				lclGridView_2.Columns[i].HeaderText = Lang.PS("柱效\n[t.p./m]", "Reten Time\n[min]");
				break;
			case "HETP":
				lclGridView_2.Columns[i].HeaderText = "HETP\n[mm]";
				break;
			case "SymTail":
				lclGridView_2.Columns[i].HeaderText = Lang.PS("对称性/\n拖尾[-]", "Reten Time\n[min]");
				break;
			case "CmpdName":
				lclGridView_2.Columns[i].HeaderText = Lang.PS("组分名", "Reten Time\n[min]");
				break;
			case "Centroid":
				lclGridView_2.Columns[i].HeaderText = Lang.PS("重心\n[min]", "Reten Time\n[min]");
				break;
			case "Variance":
				lclGridView_2.Columns[i].HeaderText = "_Variance";
				break;
			case "Skew":
				lclGridView_2.Columns[i].HeaderText = "_Skew";
				break;
			case "Excess":
				lclGridView_2.Columns[i].HeaderText = "_Excess";
				break;
			case "WO5":
				lclGridView_2.Columns[i].HeaderText = Lang.PS("半峰宽\n[min]", "Reten Time\n[min]");
				break;
			case "Asymmetry":
				lclGridView_2.Columns[i].HeaderText = Lang.PS("对称性\n[-]", "Reten Time\n[min]");
				break;
			case "Capacity":
				lclGridView_2.Columns[i].HeaderText = Lang.PS("容量\n[-]", "Reten Time\n[min]");
				break;
			case "Resolution":
				lclGridView_2.Columns[i].HeaderText = Lang.PS("分辨率\n[-]", "Reten Time\n[min]");
				break;
			}
		}
	}

	private void gvPerformStatic_SelectionChanged(object sender, EventArgs e)
	{
		if (HasChrom && !bool_2)
		{
			LclGridView lclGridView = sender as LclGridView;
			for (int i = 0; i < lclGridView.RowCount; i++)
			{
				(lclGridView.Rows[i].Tag as Peak).selected = lclGridView.Rows[i].Selected;
			}
			DisDpRefresh();
		}
	}

	public string gvPerformFrom50Value(Peak peak, string columnName)
	{
		float num = -1f;
		switch (columnName)
		{
		case "RetenTime":
			num = peak.pkRT;
			break;
		case "WO5":
			num = peak.WO5;
			break;
		case "Asymmetry":
			num = peak.Asymmetry;
			break;
		case "SymTail":
			num = peak.SymmetryTailing;
			break;
		case "Capacity":
			num = peak.Capacity;
			break;
		case "Efficiency":
			num = peak.Efficiency_EP;
			break;
		case "Eff_ColL":
			num = peak.Eff_Column_EP;
			break;
		case "Resolution":
			num = peak.Resolution_EP;
			break;
		case "CmpdName":
			return peak.name;
		}
		if (!(columnName == "Capacity") && num <= 0f)
		{
			return "";
		}
		string text = gvPerformFrom50.ConvertValFmt(columnName);
		return num.ToString(text);
	}

	private void method_12(LclGridView lclGridView_2)
	{
		if (lclGridView_2.ColumnCount == 0)
		{
			return;
		}
		for (int i = 0; i < lclGridView_2.ColumnCount; i++)
		{
			string name = lclGridView_2.Columns[i].Name;
			if (name != null)
			{
				switch (name)
				{
				case "Write":
					lclGridView_2.Columns[i].HeaderText = Lang.PS("可写", "Write");
					break;
				case "UserName":
					lclGridView_2.Columns[i].HeaderText = Lang.PS("用户名", "Write");
					break;
				case "ArcUser":
					lclGridView_2.Columns[i].HeaderText = Lang.PS("存档用户", "Write");
					break;
				case "ModifyT":
					lclGridView_2.Columns[i].HeaderText = Lang.PS("修改时间", "Write");
					break;
				case "OpenT":
					lclGridView_2.Columns[i].HeaderText = Lang.PS("打开时间", "Write");
					break;
				case "NoRead":
					lclGridView_2.Columns[i].HeaderText = Lang.PS("禁读", "Write");
					break;
				}
			}
		}
	}

	private void gvRltsGnl_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
	{
		e.Cancel = method_30(bool_6: true);
	}

	private void gvRltsGnl_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
	{
		if (!HasChrom)
		{
			return;
		}
		if (e.RowIndex >= 0 && e.ColumnIndex == -1)
		{
			Peak peak = lclGridView_1.Rows[e.RowIndex].Tag as Peak;
			string text = "峰 " + (e.RowIndex + 1);
			text = text + "\nLfDotNo: " + peak.LfDotNo + "       RtDotNo: " + peak.RtDotNo;
			text = text + "\nbsLfV.DotNo: " + peak.bsLfV.dotNo + "  bsRtV.DotNo: " + peak.bsRtV.dotNo;
			text = text + "\nbsLfV.N: " + peak.bsLfV.N + "      bsRtV.N: " + peak.bsRtV.N;
			MessageBox.Show(text + "\n\nFrom: " + peak.FromNo + "\tTo: " + peak.ToNo + "\n面积 " + peak.area.ToString(""));
		}
		if (e.RowIndex == -1 && e.ColumnIndex >= 0)
		{
			if (cusDlg_0 == null)
			{
				cusDlg_0 = new CusDlg();
			}
			if ((lclGridView_1.Columns[e.ColumnIndex].Name == "Cus1" && cusDlg_0.ShowDialog(ref CurChrom.cus1_name, ref CurChrom.cus1_formula) == DialogResult.OK) || (lclGridView_1.Columns[e.ColumnIndex].Name == "Cus2" && cusDlg_0.ShowDialog(ref CurChrom.cus2_name, ref CurChrom.cus2_formula) == DialogResult.OK))
			{
				CurChrom.CalcuCus();
				method_33();
			}
		}
	}

	private void gvRltsGnl_CellEndEdit(object sender, DataGridViewCellEventArgs e)
	{
		int rowIndex = e.RowIndex;
		int columnIndex = e.ColumnIndex;
		if (lclGridView_1.Columns[columnIndex].Name == "CmpdName")
		{
			string text = lclGridView_1.Rows[rowIndex].Cells[columnIndex].Value.ToString().TrimEnd();
			if (text == "")
			{
				return;
			}
			bool flag = false;
			for (int i = 0; i < CurChrom.PeaksNum; i++)
			{
				if (CurChrom.RltPeaks[i] != (Peak)lclGridView_1.Rows[rowIndex].Tag && CurChrom.RltPeaks[i].name == text)
				{
					MessageBox.Show("组分名重复！");
					if (1 == 0)
					{
						CurChrom.RltPeaks[rowIndex].name = text;
					}
					break;
				}
			}
		}
		if (lclGridView_1.Columns[columnIndex].Name == "Amount" && lclGridView_1.Rows[rowIndex].Tag != null)
		{
			Peak peak = lclGridView_1.Rows[rowIndex].Tag as Peak;
			float num = Class49.String2Float(lclGridView_1.Rows[rowIndex].Cells[columnIndex].Value, CurChrom.chromInfo.GetIstdAmount(peak.pkRT));
			if (num > 0f)
			{
				if (CurChrom.IstdNum == rowIndex)
				{
					CurChrom.chromInfo.SetIstdAmount(-1f, num);
				}
				else
				{
					CurChrom.chromInfo.SetIstdAmount(peak.pkRT, num);
				}
				CurChrom.CalcuResults(instrument.instruStyle);
			}
		}
		tcChrom_SelectedIndexChanged(null, null);
	}

	private void gvRltsGnl_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
	{
		int rowIndex = e.RowIndex;
		int columnIndex = e.ColumnIndex;
		if (!HasChrom || rowIndex < 0 || columnIndex < 0)
		{
			return;
		}
		for (int i = 0; i < lclGridView_1.RowCount; i++)
		{
			lclGridView_1.Rows[i].Selected = false;
		}
		if (lclGridView_1.Rows[rowIndex].Tag != null)
		{
			Peak peak = lclGridView_1.Rows[rowIndex].Tag as Peak;
			if (lclGridView_1.CurrentCell != null && ((lclGridView_1.Columns[columnIndex].Name == "Amount" && peak.IsIstd) || (lclGridView_1.Columns[columnIndex].Name == "CmpdName" && peak.compound == null)))
			{
				lclGridView_1.Rows[e.RowIndex].Selected = true;
				lclGridView_1.BeginEdit(selectAll: true);
			}
		}
	}

	private void method_13(LclGridView lclGridView_2)
	{
		for (int i = 0; i < lclGridView_2.ColumnCount; i++)
		{
			string name;
			switch (name = lclGridView_2.Columns[i].Name)
			{
			case "RetenTime":
				lclGridView_2.Columns[i].HeaderText = Lang.PS("保留时间\n[min]", "Reten Time\n[min]");
				break;
			case "StartTime":
				lclGridView_2.Columns[i].HeaderText = Lang.PS("开始时间\n[min]", "Reten Time\n[min]");
				break;
			case "EndTime":
				lclGridView_2.Columns[i].HeaderText = Lang.PS("结束时间\n[min]", "Reten Time\n[min]");
				break;
			case "StartValue":
				lclGridView_2.Columns[i].HeaderText = string_280;
				break;
			case "EndValue":
				lclGridView_2.Columns[i].HeaderText = string_278;
				break;
			case "PeakStyle":
				lclGridView_2.Columns[i].HeaderText = Lang.PS("峰类别", "Reten Time\n[min]");
				break;
			case "Area":
				lclGridView_2.Columns[i].HeaderText = string_277;
				break;
			case "Height":
				lclGridView_2.Columns[i].HeaderText = string_279;
				break;
			case "AreaPer":
				lclGridView_2.Columns[i].HeaderText = Lang.PS("面积\n[%]", "Reten Time\n[min]");
				break;
			case "HeightPer":
				lclGridView_2.Columns[i].HeaderText = Lang.PS("高度\n[%]", "Reten Time\n[min]");
				break;
			case "WO5":
				lclGridView_2.Columns[i].HeaderText = Lang.PS("半峰宽\n[min]", "Reten Time\n[min]");
				break;
			case "RespBase":
				lclGridView_2.Columns[i].HeaderText = Lang.PS("响应基础", "Reten Time\n[min]");
				break;
			case "Amount":
				lclGridView_2.Columns[i].HeaderText = Lang.PS("数量", "Reten Time\n[min]");
				break;
			case "AmountPer":
				lclGridView_2.Columns[i].HeaderText = Lang.PS("数量\n[%]", "Reten Time\n[min]");
				break;
			case "PeakType":
				lclGridView_2.Columns[i].HeaderText = Lang.PS("峰类型", "Reten Time\n[min]");
				break;
			case "CmpdName":
				lclGridView_2.Columns[i].HeaderText = Lang.PS("组分名", "Reten Time\n[min]");
				break;
			case "RetenIndex":
				lclGridView_2.Columns[i].HeaderText = Lang.PS("保留索引\n[-]", "Reten Time\n[min]");
				break;
			case "PeakPurity":
				lclGridView_2.Columns[i].HeaderText = Lang.PS("峰纯度", "Reten Time\n[min]");
				break;
			case "NameMatch":
				lclGridView_2.Columns[i].HeaderText = Lang.PS("名匹配", "Reten Time\n[min]");
				break;
			case "BestMatchName":
				lclGridView_2.Columns[i].HeaderText = Lang.PS("最佳匹配名", "Reten Time\n[min]");
				break;
			case "BestMatch":
				lclGridView_2.Columns[i].HeaderText = Lang.PS("最佳匹配", "Reten Time\n[min]");
				break;
			case "MaxRT":
				lclGridView_2.Columns[i].HeaderText = Lang.PS("最大RT\n[min]", "Reten Time\n[min]");
				break;
			case "StartRT":
				lclGridView_2.Columns[i].HeaderText = Lang.PS("开始RT\n[min]", "Reten Time\n[min]");
				break;
			case "EndRT":
				lclGridView_2.Columns[i].HeaderText = Lang.PS("结束RT\n[min]", "Reten Time\n[min]");
				break;
			case "FlowRateCorr":
				lclGridView_2.Columns[i].HeaderText = Lang.PS("流速校正", "Reten Time\n[min]");
				break;
			}
		}
	}

	private void gvRltsGnl_SelectionChanged(object sender, EventArgs e)
	{
		if (!HasChrom || bool_3)
		{
			return;
		}
		for (int i = 0; i < lclGridView_1.RowCount; i++)
		{
			if (lclGridView_1.Rows[i].Tag != null)
			{
				(lclGridView_1.Rows[i].Tag as Peak).selected = lclGridView_1.Rows[i].Selected;
			}
		}
		DisDpRefresh();
	}

	public string gvRltsValue(Peak peak, string columnName, string string_285, bool combine)
	{
		float num = -1f;
		switch (columnName)
		{
		case "RetenTime":
			num = peak.pkRT;
			break;
		case "StartTime":
			num = peak.startT;
			break;
		case "EndTime":
			num = peak.endT;
			break;
		case "StartValue":
			num = peak.startV;
			break;
		case "EndValue":
			num = peak.endV;
			break;
		case "PeakStyle":
			return method_5(peak.pkStyle);
		case "Area":
			num = peak.area;
			break;
		case "AreaPer":
			num = 100f * (combine ? peak._areaPer : peak.areaPer);
			break;
		case "Height":
			num = peak.height;
			break;
		case "HeightPer":
			num = 100f * (combine ? peak._heightPer : peak.heightPer);
			break;
		case "WO5":
			num = peak.WO5;
			break;
		case "RespBase":
			if (peak.respStyle == 0)
			{
				return RespStyle.Area.ToString();
			}
			if (peak.respStyle == 1)
			{
				return RespStyle.Height.ToString();
			}
			if (peak.respStyle == 2)
			{
				return RespStyle.AreaSquare.ToString();
			}
			if (peak.respStyle == 3)
			{
				return RespStyle.PeakHeightSquare.ToString();
			}
			break;
		case "Amount":
			num = peak.amount;
			break;
		case "AmountPer":
			num = 100f * (combine ? peak._amountPer : peak.amountPer);
			break;
		case "PeakType":
			if (peak.compound != null && peak.compound.cmpdInfo.isIstd)
			{
				return "标样";
			}
			break;
		case "CmpdName":
			return peak.name;
		case "Cus1":
			if (!float.IsNaN(peak.cus1))
			{
				if (string_285 == "")
				{
					string_285 = lclGridView_1.ConvertValFmt(columnName);
				}
				return peak.cus1.ToString(string_285);
			}
			break;
		case "Cus2":
			if (!float.IsNaN(peak.cus2))
			{
				if (string_285 == "")
				{
					string_285 = lclGridView_1.ConvertValFmt(columnName);
				}
				return peak.cus2.ToString(string_285);
			}
			break;
		}
		if (num > 0f)
		{
			if (string_285 == "")
			{
				string_285 = lclGridView_1.ConvertValFmt(columnName);
			}
			return num.ToString(string_285);
		}
		return "";
	}

	private void gvSetRights_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
	{
		cbrtaCanSetRs_Click(null, null);
	}

	private void method_14()
	{
		if (gvSlices.ColumnCount == 0)
		{
			return;
		}
		for (int i = 0; i < gvSlices.ColumnCount; i++)
		{
			string name;
			switch (name = gvSlices.Columns[i].Name)
			{
			case "RetenTime":
				gvSlices.Columns[i].HeaderText = Lang.PS("保留时间", "Reten. Time");
				break;
			case "Response":
				gvSlices.Columns[i].HeaderText = Lang.PS("响应", "Reten. Time");
				break;
			case "NormHt":
				gvSlices.Columns[i].HeaderText = "_Norm. Ht";
				break;
			case "NormHtPer":
				gvSlices.Columns[i].HeaderText = "_Norm. Ht %";
				break;
			case "CumHt":
				gvSlices.Columns[i].HeaderText = "_Cum. Ht";
				break;
			case "CumHtPer":
				gvSlices.Columns[i].HeaderText = "_Cum. Ht %";
				break;
			case "CumHtPerGraph":
				gvSlices.Columns[i].HeaderText = "_Cum. Ht %\nGraph";
				break;
			case "OutsideCalib":
				gvSlices.Columns[i].HeaderText = Lang.PS("外部\n校正", "Reten. Time");
				break;
			}
		}
	}

	private void gvSSTCmpds_MouseDown(object sender, MouseEventArgs e)
	{
		ToolStripMenuItem toolStripMenuItem = misstcClearParas;
		ToolStripMenuItem toolStripMenuItem2 = misstColumnsSetup;
		ToolStripMenuItem toolStripMenuItem3 = misstRestoreDftColumns;
		ToolStripSeparator toolStripSeparator = tss1;
		bool flag = (tss2.Visible = sender == gvSSTResults);
		bool flag3 = (toolStripSeparator.Visible = flag);
		bool flag5 = (toolStripMenuItem3.Visible = flag3);
		bool visible = (toolStripMenuItem2.Visible = flag5);
		toolStripMenuItem.Visible = visible;
	}

	private void method_15()
	{
		gvSSTCmpds.Columns["Used"].HeaderText = "使用";
		gvSSTCmpds.Columns["OK"].HeaderText = "校";
		gvSSTResults.Columns["Chrom"].HeaderText = "谱图";
		method_11(gvSSTCmpds);
		method_11(gvSSTResults);
		method_13(gvSSTResults);
	}

	private void gvSSTCmpds_CellEndEdit(object sender, DataGridViewCellEventArgs e)
	{
		if (sst_0.sstCmpds.Length != 0)
		{
			int rowIndex = e.RowIndex;
			SSTCmpd sSTCmpd = gvSSTCmpds.Rows[rowIndex].Tag as SSTCmpd;
			sSTCmpd.used = (bool)gvSSTCmpds.Rows[rowIndex].Cells["Used"].Value;
			method_37();
		}
	}

	private void gvSSTCmpds_SelectionChanged(object sender, EventArgs e)
	{
		if (bool_4)
		{
			return;
		}
		gvSSTResults.EndEdit();
		if (gvSSTCmpds.SelectedRows != null && gvSSTCmpds.SelectedRows.Count == 1 && gvSSTCmpds.SelectedRows[0].Tag != null)
		{
			sstcmpd_0 = gvSSTCmpds.SelectedRows[0].Tag as SSTCmpd;
			lbSSTExpress.Text = string.Concat(Lang.PS(" 验证标准:", " verify stand:"), sst_0.sstParas.criterion, "    <", sstcmpd_0.fromCali, ">", Lang.PS("    组分名: ", "    compound: "), sstcmpd_0.name);
			for (int i = gvSSTResults.frozenNum; i < gvSSTResults.ColumnCount; i++)
			{
				if (!gvSSTResults.Columns[i].Visible)
				{
					continue;
				}
				string name = gvSSTResults.Columns[i].Name;
				SstItem item = sstcmpd_0.GetItem(name);
				for (int j = 0; j < 6; j++)
				{
					if (j != 5)
					{
						gvSSTResults.Rows[j].Cells[i].Value = RetSstItem(item, j);
						if (j == 3)
						{
							gvSSTResults.Rows[j].Cells[i].Style.ForeColor = RetSstMeanClr(item);
						}
						if (j == 4)
						{
							gvSSTResults.Rows[j].Cells[i].Style.ForeColor = RetSstRsdPerClr(item);
						}
					}
					else
					{
						SSTResult sSTResult = SSTResult.None;
						if (item != null)
						{
							sSTResult = item.result;
						}
						gvSSTResults.Rows[j].Cells[i].Value = sSTResult;
					}
				}
			}
			gvSSTResults.SetChromCount(chromatogram_0.Length);
			for (int k = 0; k < chromatogram_0.Length; k++)
			{
				int num = 6 + k;
				Chromatogram chromatogram = chromatogram_0[k];
				gvSSTResults.Rows[num].Tag = chromatogram;
				SSTResult sSTResult2 = sstcmpd_0.extResult(chromatogram, sst_0.sstParas.criterion);
				(gvSSTResults.Rows[num].Cells["X"] as LclgvIconCell).Img = sSTResult2 switch
				{
					SSTResult.Success => SystemIconResource.smethod_25(), 
					SSTResult.None => null, 
					_ => SystemIconResource.smethod_24(), 
				};
				gvSSTResults.InvalidateCell(gvSSTResults.Columns["X"].Index, num);
				gvSSTResults.Rows[num].Cells["Chrom"].Value = chromatogram.fName;
				bool flag = false;
				for (int l = 0; l < chromatogram.RltPeaks.Length; l++)
				{
					if (!(chromatogram.RltPeaks[l].name == sstcmpd_0.name))
					{
						continue;
					}
					Peak peak = chromatogram.RltPeaks[l];
					for (int m = gvSSTResults.frozenNum; m < gvSSTResults.ColumnCount; m++)
					{
						if (gvSSTResults.Columns[m].Visible)
						{
							string name2 = gvSSTResults.Columns[m].Name;
							gvSSTResults.Rows[num].Cells[m].Value = RetSstItem(peak, name2);
							float value = SstItem.getValue(peak, name2, sst_0.sstParas.criterion);
							if (!float.IsNaN(value))
							{
								SstItem item2 = sstcmpd_0.GetItem(name2);
								gvSSTResults.Rows[num].Cells[m].Style.ForeColor = ((item2 == null || item2.extResult(value) == SSTResult.Success) ? Color.Black : Color.Red);
							}
							else
							{
								gvSSTResults.Rows[num].Cells[m].Value = null;
							}
						}
					}
					flag = true;
				}
				if (!flag)
				{
					for (int n = gvSSTResults.frozenNum; n < gvSSTResults.ColumnCount; n++)
					{
						gvSSTResults.Rows[num].Cells[n].Value = null;
					}
				}
			}
			return;
		}
		sstcmpd_0 = null;
		lbSSTExpress.Text = "[]";
		gvSSTResults.SetChromCount(0);
		for (int num2 = gvSSTResults.frozenNum; num2 < gvSSTResults.ColumnCount; num2++)
		{
			if (gvSSTResults.Columns[num2].Visible)
			{
				gvSSTResults.Rows[0].Cells[num2].Value = null;
				gvSSTResults.Rows[1].Cells[num2].Value = null;
				gvSSTResults.Rows[2].Cells[num2].Value = null;
				gvSSTResults.Rows[3].Cells[num2].Value = null;
				gvSSTResults.Rows[4].Cells[num2].Value = null;
				gvSSTResults.Rows[5].Cells[num2].Value = null;
			}
		}
	}

	private void gvSSTResults_CellEndEdit(object sender, DataGridViewCellEventArgs e)
	{
		if (sstcmpd_0 == null)
		{
			return;
		}
		int rowIndex = e.RowIndex;
		int columnIndex = e.ColumnIndex;
		string name = gvSSTResults.Columns[columnIndex].Name;
		object value = gvSSTResults.Rows[rowIndex].Cells[columnIndex].Value;
		string text = ((value != null) ? value.ToString().Trim() : "");
		switch (rowIndex)
		{
		case 0:
		{
			float upperLimit = sstcmpd_0.GetUpperLimit(name);
			float num2 = ((text != "") ? Class49.String2Float(text, upperLimit) : float.NaN);
			if (name != "Asymmetry" && name != "SymTail" && num2 < 0f)
			{
				num2 = upperLimit;
			}
			sstcmpd_0.SetUpperLimit(name, num2);
			break;
		}
		case 1:
		{
			float lowerLimit = sstcmpd_0.GetLowerLimit(name);
			float num3 = ((text != "") ? Class49.String2Float(text, lowerLimit) : float.NaN);
			if (name != "Asymmetry" && name != "SymTail" && num3 < 0f)
			{
				num3 = lowerLimit;
			}
			sstcmpd_0.SetLowerLimit(name, num3);
			break;
		}
		case 2:
		{
			float rsdPerLimit = sstcmpd_0.GetRsdPerLimit(name);
			float num = ((text != "") ? Class49.String2Float(text, rsdPerLimit) : float.NaN);
			if (num < 0f)
			{
				num = rsdPerLimit;
			}
			sstcmpd_0.SetRsdPerLimit(name, num);
			break;
		}
		}
		method_37();
	}

	private void method_16(DataGridViewColumn[] dataGridViewColumn_0)
	{
		for (int i = 0; i < dataGridViewColumn_0.Length; i++)
		{
			dataGridViewColumn_0[i].HeaderText = getSmyHeaderText(dataGridViewColumn_0[i].Name);
		}
	}

	public string gvSummaryComValue(Chromatogram chrom, string columnName)
	{
		float num = -1f;
		switch (columnName)
		{
		case "ChromName":
			return chrom.fName;
		case "SampleID":
			return chrom.injAnalysis.sampleID;
		case "Sample":
			return chrom.injAnalysis.sample;
		case "SampleAmount":
			num = chrom.injAnalysis.amount;
			break;
		case "SampleDilution":
			num = chrom.injAnalysis.dilution;
			break;
		case "ISTDAmount":
			num = chrom.injAnalysis.ISTD_amount;
			break;
		case "InjVol":
			num = chrom.injAnalysis.inj_volume;
			break;
		case "ColumnUT":
			num = chrom.chromInfo.ccColumnUT;
			break;
		case "ColumnLength":
			num = chrom.chromInfo.ccColumnLength;
			break;
		case "Noise":
			if (chrom.integ.GetNDRow(ref integRow_1) && integRow_1.success)
			{
				num = integRow_1.value;
			}
			break;
		case "Drift":
			if (chrom.integ.GetNDRow(ref integRow_0) && integRow_0.success)
			{
				num = integRow_0.value;
			}
			break;
		}
		if (num > 0f)
		{
			string text = gvSummary.ConvertValFmt(columnName);
			return num.ToString(text);
		}
		return "";
	}

	public string gvSummarySmyValue(Chromatogram chrom, string cmpdName, string columnName)
	{
		Peak peak = null;
		for (int i = 0; i < chrom.PeaksNum; i++)
		{
			if (chrom.RltPeaks[i].name == cmpdName)
			{
				peak = chrom.RltPeaks[i];
				if (peak == null)
				{
					return "";
				}
				string string_ = gvSummary.ConvertValFmt(instrument.instruStyle, columnName);
				return gvRltsValue(peak, columnName, string_, cbRltCombine.Checked);
			}
		}
		return "";
	}

	private bool method_17()
	{
		if (gvPerformStatic.ColumnCount != 0)
		{
			return false;
		}
		gvPerformStatic.AddLclTextBoxColumn("RetenTime", 60);
		gvPerformStatic.AddLclTextBoxColumn("Centroid", 60);
		gvPerformStatic.AddLclTextBoxColumn("Variance", 60);
		gvPerformStatic.AddLclTextBoxColumn("Skew", 60);
		gvPerformStatic.AddLclTextBoxColumn("Excess", 60);
		gvPerformStatic.AddLclTextBoxColumn("Efficiency", 70);
		gvPerformStatic.AddLclTextBoxColumn("Eff_ColL", 75);
		gvPerformStatic.AddLclTextBoxColumn("SymTail", 60);
		gvPerformStatic.AddLclTextBoxColumn("CmpdName", 130);
		gvPerformFrom50.AddLclTextBoxColumn("RetenTime", 60);
		gvPerformFrom50.AddLclTextBoxColumn("WO5", 60);
		gvPerformFrom50.AddLclTextBoxColumn("Asymmetry", 60);
		gvPerformFrom50.AddLclTextBoxColumn("Capacity", 60);
		gvPerformFrom50.AddLclTextBoxColumn("Efficiency", 70);
		gvPerformFrom50.AddLclTextBoxColumn("Eff_ColL", 75);
		gvPerformFrom50.AddLclTextBoxColumn("SymTail", 60);
		gvPerformFrom50.AddLclTextBoxColumn("Resolution", 60);
		gvPerformFrom50.AddLclTextBoxColumn("CmpdName", 130, StringAlignment.Near);
		return true;
	}

	private bool method_18()
	{
		if (gvRltsGnl.ColumnCount != 0)
		{
			return false;
		}
		gvRltsGnl.textBox_dftReadOnly = true;
		gvRltsGnl.AddLclTextBoxColumn("RetenTime", 70);
		gvRltsGnl.AddLclTextBoxColumn("StartTime", 70);
		gvRltsGnl.AddLclTextBoxColumn("EndTime", 70);
		gvRltsGnl.AddLclTextBoxColumn("StartValue", 70);
		gvRltsGnl.AddLclTextBoxColumn("EndValue", 70);
		gvRltsGnl.AddLclTextBoxColumn("PeakStyle", 70, StringAlignment.Near);
		gvRltsGnl.AddLclTextBoxColumn("Area", 70);
		gvRltsGnl.AddLclTextBoxColumn("Height", 70);
		gvRltsGnl.AddLclTextBoxColumn("AreaPer", 70, 2);
		gvRltsGnl.AddLclTextBoxColumn("HeightPer", 70, 2);
		gvRltsGnl.AddLclTextBoxColumn("WO5", 70);
		gvRltsGnl.AddLclTextBoxColumn("RespBase", 70, StringAlignment.Center);
		gvRltsGnl.AddLclTextBoxColumn("Amount", 70, 3, readOnly: false);
		gvRltsGnl.AddLclTextBoxColumn("AmountPer", 70, 2);
		gvRltsGnl.AddLclTextBoxColumn("PeakType", 70);
		gvRltsGnl.AddLclTextBoxColumn("CmpdName", 110, 0, StringAlignment.Near, readOnly: false);
		gvRltsGnl.AddLclTextBoxColumn("RetenIndex", 70);
		gvRltsGnl.AddLclTextBoxColumn("Cus1", 70);
		gvRltsGnl.AddLclTextBoxColumn("Cus2", 70);
		gvRltsDad.textBox_dftReadOnly = true;
		gvRltsDad.AddLclTextBoxColumn("RetenTime", 70);
		gvRltsDad.AddLclTextBoxColumn("StartTime", 70);
		gvRltsDad.AddLclTextBoxColumn("EndTime", 70);
		gvRltsDad.AddLclTextBoxColumn("StartValue", 70);
		gvRltsDad.AddLclTextBoxColumn("EndValue", 70);
		gvRltsDad.AddLclTextBoxColumn("PeakPurity", 70);
		gvRltsDad.AddLclTextBoxColumn("NameMatch", 70);
		gvRltsDad.AddLclTextBoxColumn("BestMatchName", 110);
		gvRltsDad.AddLclTextBoxColumn("BestMatch", 70);
		gvRltsDad.AddLclTextBoxColumn("WO5", 70);
		gvRltsDad.AddLclTextBoxColumn("RespBase", 70);
		gvRltsDad.AddLclTextBoxColumn("Amount", 70);
		gvRltsDad.AddLclTextBoxColumn("AmountPer", 70, 2);
		gvRltsDad.AddLclTextBoxColumn("PeakType", 70);
		gvRltsDad.AddLclTextBoxColumn("CmpdName", 110);
		gvRltsDad.AddLclTextBoxColumn("RetenIndex", 70);
		gvRltsDad.AddLclTextBoxColumn("Area", 70);
		gvRltsDad.AddLclTextBoxColumn("Height", 70);
		gvRltsDad.AddLclTextBoxColumn("AreaPer", 70, 2);
		gvRltsDad.AddLclTextBoxColumn("HeightPer", 70, 2);
		gvRltsGpc.textBox_dftReadOnly = true;
		gvRltsGpc.AddLclTextBoxColumn("CmpdName", 110);
		gvRltsGpc.AddLclTextBoxColumn("MaxRT", 70);
		gvRltsGpc.AddLclTextBoxColumn("StartRT", 70);
		gvRltsGpc.AddLclTextBoxColumn("EndRT", 70);
		gvRltsGpc.AddLclTextBoxColumn("Mp", 70).HeaderText = "Mp";
		gvRltsGpc.AddLclTextBoxColumn("Mn", 70).HeaderText = "Mn";
		gvRltsGpc.AddLclTextBoxColumn("Mw", 70).HeaderText = "Mw";
		gvRltsGpc.AddLclTextBoxColumn("Mz", 70).HeaderText = "Mz";
		gvRltsGpc.AddLclTextBoxColumn("Mz1", 70).HeaderText = "Mz1";
		gvRltsGpc.AddLclTextBoxColumn("Mv", 70).HeaderText = "Mv";
		gvRltsGpc.AddLclTextBoxColumn("PD", 70).HeaderText = "PD";
		gvRltsGpc.AddLclTextBoxColumn("Area", 70);
		gvRltsGpc.AddLclTextBoxColumn("Height", 70);
		gvRltsGpc.AddLclTextBoxColumn("AreaPer", 70, 2);
		gvRltsGpc.AddLclTextBoxColumn("HeightPer", 70, 2);
		gvRltsGpc.AddLclTextBoxColumn("FlowRateCorr", 70);
		return true;
	}

	private void method_19()
	{
		gvSetRights.AddLclCheckBoxColumn("NoRead", 50);
		gvSetRights.AddLclCheckBoxColumn("Write", 40);
		gvSetRights.AddLclTextBoxColumn("UserName", 210, 0, StringAlignment.Near, readOnly: true);
		gvArchives.AddLclTextBoxColumn("ArcUser", 210, StringAlignment.Near);
		gvArchives.AddLclTextBoxColumn("ModifyT", 170, StringAlignment.Near);
		gvArchives.AddLclTextBoxColumn("OpenT", 170, StringAlignment.Near);
	}

	private void method_20()
	{
		gvSlices.AddLclTextBoxColumn("RetenTime", 70);
		gvSlices.AddLclTextBoxColumn("Response", 70);
		gvSlices.AddLclTextBoxColumn("NormHt", 70);
		gvSlices.AddLclTextBoxColumn("NormHtPer", 70);
		gvSlices.AddLclTextBoxColumn("CumHt", 70);
		gvSlices.AddLclTextBoxColumn("CumHtPer", 70);
		gvSlices.AddLclTextBoxColumn("CumHtPerGraph", 70);
		gvSlices.AddLclTextBoxColumn("M", 70);
		gvSlices.AddLclTextBoxColumn("LogM", 70);
		gvSlices.AddLclTextBoxColumn("dWdLogM", 70).HeaderText = "dW/dLogM";
		gvSlices.AddLclTextBoxColumn("W", 70);
		gvSlices.AddLclTextBoxColumn("OutsideCalib", 70);
	}

	private bool method_21()
	{
		if (gvSSTResults.ColumnCount != 0)
		{
			return false;
		}
		DataGridViewColumn dataGridViewColumn = gvSSTResults.AddLclgvIconColumn("X", 20);
		dataGridViewColumn.HeaderText = Lang.PS("检", "X");
		dataGridViewColumn.Frozen = true;
		gvSSTResults.AddLclTextBoxColumn("Chrom", 130, StringAlignment.Near).Frozen = true;
		gvSSTResults.AddLclTextBoxColumn("RetenTime", 60);
		gvSSTResults.AddLclTextBoxColumn("Area", 60);
		gvSSTResults.AddLclTextBoxColumn("Height", 60);
		gvSSTResults.AddLclTextBoxColumn("Amount", 60);
		gvSSTResults.AddLclTextBoxColumn("AmountPer", 60, 2);
		gvSSTResults.AddLclTextBoxColumn("WO5", 60);
		gvSSTResults.AddLclTextBoxColumn("Asymmetry", 60);
		gvSSTResults.AddLclTextBoxColumn("SymTail", 80);
		gvSSTResults.AddLclTextBoxColumn("Capacity", 60);
		gvSSTResults.AddLclTextBoxColumn("Efficiency", 80);
		gvSSTResults.AddLclTextBoxColumn("Eff_ColL", 80);
		gvSSTResults.AddLclTextBoxColumn("HETP", 60);
		gvSSTResults.AddLclTextBoxColumn("Resolution", 70);
		return true;
	}

	private bool method_22()
	{
		if (gvSummary.ColumnCount != 0)
		{
			return false;
		}
		gvSummary.ArrayComColumns(11);
		gvSummary.AddComTB(0, "ChromName", 140, 0, StringAlignment.Near);
		gvSummary.AddComTB(1, "SampleID", 50, 0, StringAlignment.Near);
		gvSummary.AddComTB(2, "Sample", 110, 0, StringAlignment.Near);
		gvSummary.AddComTB(3, "SampleAmount", 60, 3, StringAlignment.Far);
		gvSummary.AddComTB(4, "SampleDilution", 60, 3, StringAlignment.Far);
		gvSummary.AddComTB(5, "ISTDAmount", 60, 3, StringAlignment.Far);
		gvSummary.AddComTB(6, "InjVol", 60, 3, StringAlignment.Far);
		gvSummary.AddComTB(7, "ColumnUT", 60, 3, StringAlignment.Far);
		gvSummary.AddComTB(8, "ColumnLength", 60, 3, StringAlignment.Far);
		gvSummary.AddComTB(9, "Noise", 60, 3, StringAlignment.Far);
		gvSummary.AddComTB(10, "Drift", 60, 3, StringAlignment.Far);
		gvSummary.ArraySmyColumns(InstruStyle.LC, 16);
		gvSummary.AddSmyTB(InstruStyle.LC, 0, "StartTime", 70, 3, StringAlignment.Far);
		gvSummary.AddSmyTB(InstruStyle.LC, 1, "EndTime", 70, 3, StringAlignment.Far);
		gvSummary.AddSmyTB(InstruStyle.LC, 2, "StartValue", 70, 3, StringAlignment.Far);
		gvSummary.AddSmyTB(InstruStyle.LC, 3, "EndValue", 70, 3, StringAlignment.Far);
		gvSummary.AddSmyTB(InstruStyle.LC, 4, "WO5", 70, 3, StringAlignment.Far);
		gvSummary.AddSmyTB(InstruStyle.LC, 5, "RespBase", 70, 3, StringAlignment.Center);
		gvSummary.AddSmyTB(InstruStyle.LC, 6, "RetenIndex", 70, 3, StringAlignment.Far);
		gvSummary.AddSmyTB(InstruStyle.LC, 7, "Area", 70, 3, StringAlignment.Far);
		gvSummary.AddSmyTB(InstruStyle.LC, 8, "Height", 70, 3, StringAlignment.Far);
		gvSummary.AddSmyTB(InstruStyle.LC, 9, "AreaPer", 70, 3, StringAlignment.Far);
		gvSummary.AddSmyTB(InstruStyle.LC, 10, "HeightPer", 70, 3, StringAlignment.Far);
		gvSummary.AddSmyTB(InstruStyle.LC, 11, "RetenTime", 70, 3, StringAlignment.Far);
		gvSummary.AddSmyTB(InstruStyle.LC, 12, "Amount", 70, 3, StringAlignment.Far);
		gvSummary.AddSmyTB(InstruStyle.LC, 13, "AmountPer", 70, 3, StringAlignment.Far);
		gvSummary.AddSmyTB(InstruStyle.LC, 14, "PeakType", 70, 3, StringAlignment.Far);
		gvSummary.AddSmyTB(InstruStyle.LC, 15, "CmpdName", 110, 0, StringAlignment.Near);
		gvSummary.ArraySmyColumns(InstruStyle.PDA, 20);
		gvSummary.AddSmyTB(InstruStyle.PDA, 0, "StartTime", 70, 3, StringAlignment.Far);
		gvSummary.AddSmyTB(InstruStyle.PDA, 1, "EndTime", 70, 3, StringAlignment.Far);
		gvSummary.AddSmyTB(InstruStyle.PDA, 2, "StartValue", 70, 3, StringAlignment.Far);
		gvSummary.AddSmyTB(InstruStyle.PDA, 3, "EndValue", 70, 3, StringAlignment.Far);
		gvSummary.AddSmyTB(InstruStyle.PDA, 4, "WO5", 70, 3, StringAlignment.Far);
		gvSummary.AddSmyTB(InstruStyle.PDA, 5, "RespBase", 70, 3, StringAlignment.Center);
		gvSummary.AddSmyTB(InstruStyle.PDA, 6, "RetenIndex", 70, 3, StringAlignment.Far);
		gvSummary.AddSmyTB(InstruStyle.PDA, 7, "Area", 70, 3, StringAlignment.Far);
		gvSummary.AddSmyTB(InstruStyle.PDA, 8, "Height", 70, 3, StringAlignment.Far);
		gvSummary.AddSmyTB(InstruStyle.PDA, 9, "AreaPer", 70, 3, StringAlignment.Far);
		gvSummary.AddSmyTB(InstruStyle.PDA, 10, "HeightPer", 70, 3, StringAlignment.Far);
		gvSummary.AddSmyTB(InstruStyle.PDA, 11, "RetenTime", 70, 3, StringAlignment.Far);
		gvSummary.AddSmyTB(InstruStyle.PDA, 12, "Amount", 70, 3, StringAlignment.Far);
		gvSummary.AddSmyTB(InstruStyle.PDA, 13, "AmountPer", 70, 3, StringAlignment.Far);
		gvSummary.AddSmyTB(InstruStyle.PDA, 14, "PeakType", 70, 3, StringAlignment.Far);
		gvSummary.AddSmyTB(InstruStyle.PDA, 15, "CmpdName", 110, 0, StringAlignment.Near);
		gvSummary.AddSmyTB(InstruStyle.PDA, 16, "PeakPurity", 70, 3, StringAlignment.Far);
		gvSummary.AddSmyTB(InstruStyle.PDA, 17, "NameMatch", 70, 3, StringAlignment.Far);
		gvSummary.AddSmyTB(InstruStyle.PDA, 18, "BestMatchName", 110, 0, StringAlignment.Near);
		gvSummary.AddSmyTB(InstruStyle.PDA, 19, "BestMatch", 70, 3, StringAlignment.Far);
		gvSummary.ArraySmyColumns(InstruStyle.GPC, 15);
		gvSummary.AddSmyTB(InstruStyle.GPC, 0, "MaxRT", 70, 3, StringAlignment.Far);
		gvSummary.AddSmyTB(InstruStyle.GPC, 1, "StartRT", 70, 3, StringAlignment.Far);
		gvSummary.AddSmyTB(InstruStyle.GPC, 2, "EndRT", 70, 3, StringAlignment.Far);
		gvSummary.AddSmyTB(InstruStyle.GPC, 3, "FlowRateCorr", 70, 3, StringAlignment.Far);
		gvSummary.AddSmyTB(InstruStyle.GPC, 4, "Mp", 70, 3, StringAlignment.Far);
		gvSummary.AddSmyTB(InstruStyle.GPC, 5, "Mn", 70, 3, StringAlignment.Far);
		gvSummary.AddSmyTB(InstruStyle.GPC, 6, "Mw", 70, 3, StringAlignment.Far);
		gvSummary.AddSmyTB(InstruStyle.GPC, 7, "Mz", 70, 3, StringAlignment.Far);
		gvSummary.AddSmyTB(InstruStyle.GPC, 8, "Mz1", 70, 3, StringAlignment.Far);
		gvSummary.AddSmyTB(InstruStyle.GPC, 9, "Mv", 70, 3, StringAlignment.Far);
		gvSummary.AddSmyTB(InstruStyle.GPC, 10, "PD", 70, 3, StringAlignment.Far);
		gvSummary.AddSmyTB(InstruStyle.GPC, 11, "Area", 70, 3, StringAlignment.Far);
		gvSummary.AddSmyTB(InstruStyle.GPC, 12, "Height", 70, 3, StringAlignment.Far);
		gvSummary.AddSmyTB(InstruStyle.GPC, 13, "AreaPer", 70, 3, StringAlignment.Far);
		gvSummary.AddSmyTB(InstruStyle.GPC, 14, "HeightPer", 70, 3, StringAlignment.Far);
		return true;
	}

	private void method_23()
	{
		CurChrom.Process(instrument.instruStyle);
		tcChrom_SelectedIndexChanged(null, null);
	}

	private void method_24(Chromatogram[] chromatogram_2, int int_0)
	{
		chromatogram_1 = chromDisplay_0.LinkDisChroms(chromatogram_2, ref int_0);
		chromatogram_1.signal.needReCalcuDis = true;
		spSignals.SetSignals(chromatogram_2.Length, int_0);
		method_39();
	}

	public override void LoadLanguage()
	{
		miFile.Text = Lang.PS("文件", "File");
		miFiOverlayMode.Text = Lang.PS("重叠模式", "Overlay Mode");
		miFiOpen.Text = Lang.PS("打开...", "Open...");
		miFiClose.Text = Lang.PS("关闭", "Close");
		miFiCloseAll.Text = Lang.PS("关闭全部", "Close All");
		miFiSave.Text = Lang.PS("保存", "Save");
		miFiSaveAs.Text = Lang.PS("另存...", "Save as...");
		miFiImportChrom.Text = Lang.PS("导入谱图", "Import Chrom");
		miFiReportSetup.Text = Lang.PS("样式文件...", "Style Set...");
		miFiPreview.Text = Lang.PS("预览", "Preview");
		miFiPrint.Text = Lang.PS("打印", "Print");
		miFiExit.Text = Lang.PS("退出", "Exit");
		miDisplay.Text = Lang.PS("显示", "Display");
		miDisPreviousZoom.Text = Lang.PS("后退", "Previous Zoom");
		miDisNextZoom.Text = Lang.PS("前进", "Next Zoom");
		miDisUnzoom.Text = Lang.PS("复位", "Unzoom");
		miDisProperties.Text = Lang.PS("属性...", "Properties...");
		miChromatogram.Text = Lang.PS("谱图", "Chromatogram");
		miChmGlobal.Text = Lang.PS("全局参数", "Global paras");
		migblDtecDelay.Text = Lang.PS("检测器延迟", "Detector Delay");
		migblPeakWidth.Text = Lang.PS("峰宽参数", "Peak Width Para");
		migblThreshold.Text = Lang.PS("峰高参数", "Peak Height Para");
		migblPkSlope.Text = Lang.PS("峰斜率", "Peak Slope");
		miChmItgPeak.Text = Lang.PS("峰", "Peak");
		miipResetDtecNeg.Text = Lang.PS("重置.检测负峰", "Reset&Detect Negative");
		miipClampNeg.Text = Lang.PS("翻转负峰", "Clamp Neg");
		miipPkWidth.Text = Lang.PS("最小峰宽");
		miipPkThreshold.Text = Lang.PS("最小峰高");
		miipPkAddPosi.Text = Lang.PS("添加正峰", "Add Positive");
		miipPkAddNeg.Text = Lang.PS("添加负峰", "Add Negative");
		miipPkCut.Text = Lang.PS("剔除峰", "Cut Peaks");
		miipPkHalfWidth.Text = Lang.PS("最小半峰宽", "Half Width");
		miipPkArea.Text = Lang.PS("最小面积", "Min Area");
		miipPkVale.Text = Lang.PS("谷点", "Valley");
		miipSolventPeak.Text = Lang.PS("垂直切割", "Solvent Peak");
		miipFlowMarker.Text = Lang.PS("流速标识", "Flow Marker");
		miipGroups.Text = Lang.PS("组...", "Groups...");
		miChmBaseline.Text = Lang.PS("基线", "Baseline");
		mibsBsTgnt.Text = Lang.PS("肩切参数", "Tangent Paras");
		mibsBsVtV.Text = Lang.PS("谷.谷斜率", "VtV");
		mibsBsValley.Text = Lang.PS("经过谷点", "Pass Valley");
		mibsBsTogether.Text = Lang.PS("整合基线", "Valley Together");
		mibsBsForwHorz.Text = Lang.PS("向前水平", "Forw. Horzitonal");
		mibsBsBackHorz.Text = Lang.PS("向后水平", "Back. Horzitonal");
		mibsBsFrontTgnt.Text = Lang.PS("前切", "Front Tangent");
		mibsBsTailTgnt.Text = Lang.PS("尾切", "Tail Tangent");
		miChmNoiseDrift.Text = Lang.PS("噪声漂移", "Noise Drift");
		mindNoise.Text = Lang.PS("噪声评估", "Noise Evaluation");
		mindDrift.Text = Lang.PS("漂移评估", "Drift Evaluation");
		miChmCreateLabel.Text = Lang.PS("创建标识", "Create Label");
		miclText.Text = Lang.PS("文本...", "Text...");
		miclLine.Text = Lang.PS("直线...", "Line...");
		miChmRemoveLabels.Text = Lang.PS("移除标识", "Remove Label(s)");
		mirlSelected.Text = Lang.PS("选择的", "Selected");
		mirlActiveChrom.Text = Lang.PS("当前谱图", "ActiveChrom");
		mirlAllChroms.Text = Lang.PS("所有谱图", "All Chroms");
		miMethod.Text = Lang.PS("方法", "Method");
		miMtdCaculation.Text = Lang.PS("计算", "Caculation");
		miMtdIntegration.Text = Lang.PS("积分", "Integration");
		miMtdMeasurement.Text = Lang.PS("测量", "Measurement");
		miResults.Text = Lang.PS("结果", "Results");
		miRltResult.Text = Lang.PS("结果", "Result");
		miRltSummary.Text = Lang.PS("总结", "Summary");
		miRltPerformance.Text = Lang.PS("性能", "Performance");
		miRltSmyOpt.Text = Lang.PS("总结选项...", "Summary Options...");
		miSST.Text = Lang.PS("组分验证", "SST");
		miSstNew.Text = Lang.PS("新建", "New");
		miSstOpen.Text = Lang.PS("打开...", "Open...");
		miSstSave.Text = Lang.PS("保存", "Save");
		miSstSaveas.Text = Lang.PS("另存...", "Save as...");
		miSstUpdateFromCalib.Text = Lang.PS("从校正刷新", "Update From Calib");
		miSstSet.Text = Lang.PS("SST设置...", "SST Set...");
		miSstClearParas.Text = Lang.PS("清除参数", "Clear Paras");
		btnExpress.Text = Lang.PS("动态帮助", "Baloon help");
		tpgpcChrom.Text = Lang.PS("谱图", "Chromatogram");
		tpgpcMwDistrib.Text = Lang.PS("重均微分分布", "Mw Distribution");
		tpgpcCmlMw.Text = Lang.PS("重均积分分布", "Cumulative Mw Distribution");
		tpResults.Text = Lang.PS("结果", "Results");
		gbCalibration.Text = Lang.PS("校正文件[峰表]", "Calibration[Peak Table]");
		btnclbSet.Text = Lang.PS("设置...", "Set...");
		btnclbNone.Text = Lang.PS("置空", "None");
		lbcuCalcu.Text = Lang.PS("计算", "Calculation");
		gbcuRltTableReport.Text = Lang.PS("结果表报告", "Report in Result Table");
		cbcuHideISTDPeak.Text = Lang.PS("隐藏内标峰", "Hide ISTD Peak");
		rbcuAllDetectedPeaks.Text = Lang.PS("所有检测峰", "All Detected Peaks");
		rbcuIdentifiedPeaks.Text = Lang.PS("所有识别峰", "All Identified Peaks");
		rbcuCaliPeaks.Text = Lang.PS("所有校正峰", "All Peaks in Calibration");
		gbcuUncalPeaks.Text = Lang.PS("未识别峰", "Uncal. Peaks");
		lbcuUncalBase.Text = Lang.PS("未识别响应", "Uncal. Base");
		lbcuUncalAmtRespF.Text = Lang.PS("未识别因子", "Uncal. Factor");
		cbcuUseScaleFactor.Text = Lang.PS("使用缩放因子", "Use Scale Factor");
		lbcuScaleFactor.Text = Lang.PS("缩放因子", "Scale Factor");
		lbcuUnitAfterScale.Text = Lang.PS("缩放后单位", "Use Unit");
		lbcuAmount.Text = Lang.PS("数量", "Amount");
		lbcuInjVolume.Text = Lang.PS("进样体积", "Inj. Volume");
		lbcuDilution.Text = Lang.PS("稀释", "Dilution");
		btngcuKAlpha.Text = Lang.PS("载入K , Alpha", "Load K , Alpha");
		mirltsColumnsSetup.Text = Lang.PS("列设置...", "Columns Setup...");
		mirltsRestoreDftColumns.Text = Lang.PS("恢复默认列设置", "Restore Default Columns");
		tpSummary.Text = Lang.PS("总结", "Summary");
		mismyColumnsSetup.Text = Lang.PS("列设置...", "Columns Setup...");
		mismyRestoreDftColumns.Text = Lang.PS("恢复默认列设置", "Restore Default Columns");
		mismySmyOpt.Text = Lang.PS("总结选项...", "Summary Options...");
		tpPerformance.Text = Lang.PS("柱效", "Performance");
		gbpfmColumnCalcu.Text = Lang.PS("柱效计算", "Column Caculation");
		lbpfmColumnUT.Text = Lang.PS("非保留峰时间", "Unretained Peak");
		lbpfmColumnLength.Text = Lang.PS("柱长", "Column Length");
		rbpfmStatistical.Text = Lang.PS("静态时间", "Statistical Moments");
		rbpfmFrom50per.Text = Lang.PS("50%宽起始", "From Width at 50%");
		mipfmColumnsSetup.Text = Lang.PS("列设置...", "Columns Setup...");
		mipfmRestoreDftColumns.Text = Lang.PS("恢复默认列设置", "Restore Default Columns");
		tpIntegration.Text = Lang.PS("积分", "Integration");
		miitgUndo.Text = Lang.PS("撤销", "Undo");
		miitgRedo.Text = Lang.PS("恢复", "Redo");
		miitgAppendRow.Text = Lang.PS("添加行", "Append Row");
		miitgInsertRow.Text = Lang.PS("插入行", "Insert Row");
		miitgDelete.Text = Lang.PS("删除", "Delete");
		miitgReset.Text = Lang.PS("重置", "Reset");
		miitgCopy.Text = Lang.PS("复制", "Copy");
		miitgPaste.Text = Lang.PS("粘帖", "Paste");
		tpMsmCdts.Text = Lang.PS("测量条件", "Measurement Conditions");
		tpInstrument.Text = Lang.PS("仪器", "Instrument");
		tpPDAMethod.Text = Lang.PS("PDA 方法", "PDA Method");
		gbinsCdts.Text = Lang.PS("测量条件", "Measurement Conditions");
		lbmsmMtdDspt.Text = Lang.PS("描述", "Description");
		lbmsmColumn.Text = ((instrument.instruStyle != InstruStyle.GC) ? Lang.PS("色谱柱") : Lang.PS("色谱柱"));
		lbmsmMobilePhase.Text = ((instrument.instruStyle != InstruStyle.GC) ? Lang.PS("流动相", "Mobile Phase") : Lang.PS("柱温"));
		lbmsmFlowRate.Text = ((instrument.instruStyle != InstruStyle.GC) ? Lang.PS("流速", "Flow Rate") : Lang.PS("载气"));
		lbmsmPressure.Text = ((instrument.instruStyle != InstruStyle.GC) ? Lang.PS("压力", "Pressure") : Lang.PS("气体1"));
		lbmsmDetection.Text = ((instrument.instruStyle != InstruStyle.GC) ? Lang.PS("检测", "Detection") : Lang.PS("气体2"));
		lbmsmTemperature.Text = ((instrument.instruStyle != InstruStyle.GC) ? Lang.PS("温度", "Temperature") : Lang.PS("检测器"));
		lbmsmNote.Text = Lang.PS("备注", "Note");
		gbinsSampleIdt.Text = Lang.PS("样品核对", "Sample Identification");
		lbsiSampleID.Text = Lang.PS("样品 ID", "Sample ID");
		lbsiSample.Text = Lang.PS("样品", "Sample");
		lbsiAcquiredTime.Text = Lang.PS("采集时间  :", "Acq. Time   :");
		lbsiAnalyst.Text = Lang.PS("分析员    :", "Analyst     :");
		gbinsAcqParas.Text = Lang.PS("采集参数", "Acquisition Parameters");
		lbapAutoStop.Text = Lang.PS("自动停止  :", "AutoStop    :");
		lbapRange.Text = Lang.PS("范围      :", "Range      :");
		lbapExtStart.Text = Lang.PS("外部启动  :", "Ext. Start  :");
		lbapSampling.Text = Lang.PS("采样      :", "Sampling   :");
		lbapMethod.Text = Lang.PS("方法      :", "Method       :");
		lbasChrom.Text = Lang.PS("谱图", "Chromatogram");
		lbasMatching.Text = Lang.PS("匹配方式", "Matching");
		btnasSetChrom.Text = Lang.PS("设置...", "Set...");
		btnasNoneChrom.Text = Lang.PS("置空", "None");
		gbpdaPeakPurityOptions.Text = Lang.PS("峰纯度选项", "Peak Purity Options");
		cbppoRestrictWaveLength.Text = Lang.PS("限制波长范围", "Restrict Wavelength Range");
		LclLabel lclLabel = lbppoFrom;
		string text = (lblsoFrom.Text = Lang.PS("从:", "from:"));
		lclLabel.Text = text;
		LclLabel lclLabel2 = lbppoTo;
		text = (lblsoTo.Text = Lang.PS("到:", "to:"));
		lclLabel2.Text = text;
		lbppoPurityThreshold.Text = Lang.PS("纯度极限", "Purity Threshold");
		lbppoAbsorbanceThreshold.Text = Lang.PS("吸收极限", "Absorbance Threshold");
		gbppoUsedPoints.Text = Lang.PS("使用点数", "Used Points");
		rbupAll.Text = Lang.PS("全部", "All");
		rbupFive.Text = Lang.PS("五点", "Five");
		cbppoUseBackCorr.Text = Lang.PS("使用背景修正", "Use Background Correction");
		gbpdaLibSearchOptions.Text = Lang.PS("库分析选项", "Library Search Options");
		lblsoMatchCriteria.Text = Lang.PS("匹配规则", "Match Criteria");
		lblsoMatchFactorThreshold.Text = Lang.PS("匹配因子极限", "Match Factor Threshold");
		lblsoMaxNumHits.Text = Lang.PS("最大显示波数", "Max. Number of Hits");
		cblsoRestrictWaveLength.Text = Lang.PS("限制波长范围", "Restrict Wavelength Range");
		cblsoRestrictRT.Text = Lang.PS("限制保留时间", "Restrict Reten. Time");
		cblsoForAllDetectedPeaks.Text = Lang.PS("所有检测峰", "For All Detected Peaks");
		cblsoUseBackCorr.Text = Lang.PS("使用背景修正", "Use Background Correction");
		gbpdaLibs.Text = Lang.PS("匹配库", "Match Library");
		miAddRow.Text = Lang.PS("添加库", "Add Library");
		miDeleteRow.Text = Lang.PS("删除库", "Delete Library");
		tpSST.Text = Lang.PS("组分验证", "SST Results");
		misstColumnsSetup.Text = Lang.PS("列设置...", "Columns Setup...");
		misstRestoreDftColumns.Text = Lang.PS("恢复默认列设置", "Restore Default Columns");
		tpSlices.Text = Lang.PS("切片", "Slices");
		mislcColumnsSetup.Text = Lang.PS("列设置...", "Columns Setup...");
		mislcRestoreDftColumns.Text = Lang.PS("恢复默认列设置", "Restore Default Columns");
		lbslcAverNum.Text = Lang.PS("平均点数", "Aver. Num");
		tpRanges.Text = Lang.PS("分段", "Ranges");
		lbgrPercent.Text = Lang.PS("百分比类型GPC表", "Percent Type GPC Ranges Table");
		lbgrMw.Text = Lang.PS("分子量类型GPC表", "Mw Type GPC Ranges Table");
		btnOpen.Text = miFiOpen.Text;
		btnSave.Text = miFiSave.Text;
		btnClose.Text = miFiClose.Text;
		btnReportSetup.Text = miFiReportSetup.Text;
		btnPreview.Text = miFiPreview.Text;
		btnPrint.Text = miFiPrint.Text;
		btnPrtLink.Text = Lang.PS("样式设置", "Style Set");
		btnPreviousZoom.Text = miDisPreviousZoom.Text;
		btnNextZoom.Text = miDisNextZoom.Text;
		btnUnzoom.Text = miDisUnzoom.Text;
		btnProperties.Text = miDisProperties.Text;
		btnOverlayMode.Text = miFiOverlayMode.Text;
		btngblPeakWidth.Text = migblPeakWidth.Text;
		btngblThreshold.Text = migblThreshold.Text;
		btnipResetDtecNeg.Text = miipResetDtecNeg.Text;
		btnipClampNeg.Text = miipClampNeg.Text;
		btnipPkWidth.Text = miipPkWidth.Text;
		btnipPkThreshold.Text = miipPkThreshold.Text;
		btnipPkAddPosi.Text = miipPkAddPosi.Text;
		btnipPkAddNeg.Text = miipPkAddNeg.Text;
		btnipPkCut.Text = miipPkCut.Text;
		btnipPkHalfWidth.Text = miipPkHalfWidth.Text;
		btnipPkArea.Text = miipPkArea.Text;
		btnipPkVale.Text = miipPkVale.Text;
		btnipSolventPeak.Text = miipSolventPeak.Text;
		btnipFlowMarker.Text = miipFlowMarker.Text;
		btnipGroups.Text = miipGroups.Text;
		btnbsBsTgnt.Text = mibsBsTgnt.Text;
		btnbsBsVtV.Text = mibsBsVtV.Text;
		btnbsBsValley.Text = mibsBsValley.Text;
		btnbsBsTogether.Text = mibsBsTogether.Text;
		btnbsBsForwHorz.Text = mibsBsForwHorz.Text;
		btnbsBsBackHorz.Text = mibsBsBackHorz.Text;
		btnbsBsFrontTgnt.Text = mibsBsFrontTgnt.Text;
		btnbsBsTailTgnt.Text = mibsBsTailTgnt.Text;
		btngblDtecDelay.Text = migblDtecDelay.Text;
		openFileDialog_2.Title = btnclbSet.Text;
		misstcNew.Text = miSstNew.Text;
		misstcOpen.Text = miSstOpen.Text;
		misstcSave.Text = miSstSave.Text;
		misstcSaveas.Text = miSstSaveas.Text;
		misstcUpdateFromCalib.Text = miSstUpdateFromCalib.Text;
		misstcSet.Text = miSstSet.Text;
		misstcClearParas.Text = miSstClearParas.Text;
	}

	public void LoadOptions()
	{
		chromDisplay_0.LinkOptions(instrument.user.options);
		chromDisplay_0.setShowGrid = instrument.user.options.grpShowGrid;
		ApplyMethod();
		method_38();
	}

	private void method_25()
	{
		method_29(bool_6: false);
		method_23();
		CurChrom.integ.ResetUndoIndex();
	}

	private void method_26(bool bool_6)
	{
		if (bool_6)
		{
			byte_0 = 0;
		}
		chromDisplay_0.DrawL_end();
		lbExpress.Text = "";
		lbExpress.Visible = false;
		method_29(bool_6: false);
	}

	private void method_27()
	{
		string disMouseLgFmtX = chromDisplay_0.disMouseLgFmtX;
		IntegOprtStyle oprtStyle = integRow_2.oprtStyle;
		switch (oprtStyle)
		{
		case IntegOprtStyle.PeakWidth:
			CurChrom.integ.PeakWidth = CurSignal.getWx(integRow_2.timeA, integRow_2.timeB);
			break;
		case IntegOprtStyle.Threshold:
			CurChrom.integ.Threshold = CurSignal.getAy(integRow_2.timeA, integRow_2.timeB);
			break;
		default:
			switch (oprtStyle)
			{
			case IntegOprtStyle.PkWidth:
				integRow_2.value = CurChrom.integ.PeakWidth;
				manuDlg_0.SetTitleValueU(gvInteg, integRow_2, disMouseLgFmtX);
				manuDlg_0.Show(showSuggest: true);
				return;
			case IntegOprtStyle.PkThreshold:
				integRow_2.value = CurChrom.integ.Threshold;
				manuDlg_0.SetTitleValueU(gvInteg, integRow_2, disMouseLgFmtX);
				manuDlg_0.Show(showSuggest: true);
				return;
			case IntegOprtStyle.PkHalfWidth:
				integRow_2.value = CurChrom.integ.PeakWidth;
				manuDlg_0.SetTitleValueU(gvInteg, integRow_2, disMouseLgFmtX);
				manuDlg_0.Show(showSuggest: true);
				return;
			case IntegOprtStyle.PkArea:
				integRow_2.value = 0f;
				manuDlg_0.SetTitleValueU(gvInteg, integRow_2, disMouseLgFmtX);
				manuDlg_0.Show(showSuggest: false);
				return;
			default:
				switch (oprtStyle)
				{
				case IntegOprtStyle.BsTgnt:
					integRow_2.value = CurChrom.integ.IniTgntAreaF;
					integRow_2.value2 = CurChrom.integ.IniTgntSlopeF;
					integRow_2.value3 = CurChrom.integ.IniTgntLfF;
					integRow_2.value4 = CurChrom.integ.IniTgntRtF;
					manuDlg_0.SetTitleValueU(gvInteg, integRow_2, disMouseLgFmtX);
					manuDlg_0.Show(showSuggest: false);
					return;
				case IntegOprtStyle.BsVtV:
					integRow_2.value = CurChrom.integ.IniVtVSlope;
					manuDlg_0.SetTitleValueU(gvInteg, integRow_2, disMouseLgFmtX);
					manuDlg_0.Show(showSuggest: true);
					return;
				}
				break;
			case IntegOprtStyle.PkAddPosi:
			case IntegOprtStyle.PkAddNeg:
			case IntegOprtStyle.PkCut:
				break;
			}
			if (integRow_2.oprtStyle == IntegOprtStyle.DtecDelay)
			{
				integRow_2.value = CurSignal.getWx(integRow_2.timeA, integRow_2.timeB);
			}
			CurChrom.integ.AppendRow(integRow_2);
			break;
		}
		method_25();
	}

	private void miclLine_Click(object sender, EventArgs e)
	{
		method_29(bool_6: false);
		if (HasChrom && !method_30(bool_6: true))
		{
			if (sender == miclText)
			{
				dpgnlChrom.Cursor = Cursors.IBeam;
			}
			else if (sender == miclLine)
			{
				dpgnlChrom.Cursor = Cursors.SizeNWSE;
			}
		}
	}

	private void mirlAllChroms_Click(object sender, EventArgs e)
	{
		method_29(bool_6: false);
		if (!HasChrom || method_30(bool_6: true))
		{
			return;
		}
		if (sender == mirlSelected)
		{
			CurChrom.signal.CutSelectedLbs();
		}
		else if (sender == mirlActiveChrom)
		{
			CurChrom.signal.CutAllLbs();
		}
		else if (sender == mirlAllChroms)
		{
			for (int i = 0; i < chromatogram_0.Length; i++)
			{
				chromatogram_0[i].signal.CutAllLbs();
			}
		}
		DisDpRefresh();
	}

	private void btnNextZoom_Click(object sender, EventArgs e)
	{
		chromDisplay_0.stDisChain.DynNo++;
		DisDpRefresh();
		method_38();
	}

	private void dpgpcChrom_DoubleClick(object sender, EventArgs e)
	{
		chromDisplay_0.stDisChain.DynNo--;
		DisDpRefresh();
		method_38();
	}

	private void btnProperties_Click(object sender, EventArgs e)
	{
		Class49.optionsDialog_0.ShowDialog(instrument, WinStyle.Chromatogram, instrument.user.options);
	}

	private void btnUnzoom_Click(object sender, EventArgs e)
	{
		if (chromDisplay_0.SetFullDisLg(ref disLg_0, CurSignal, second: true))
		{
			DisDpRefresh();
			method_38();
		}
	}

	private void btnClose_Click(object sender, EventArgs e)
	{
		lclGridView_1.EndEdit();
		switch (chromatogram_0.Length)
		{
		case 0:
			return;
		case 1:
			miFiCloseAll_Click(null, null);
			return;
		}
		if (chromatogram_1 == null)
		{
			return;
		}
		for (int i = 0; i < chromatogram_0.Length; i++)
		{
			if (chromatogram_0[i] == chromatogram_1)
			{
				for (int j = i; j < chromatogram_0.Length - 1; j++)
				{
					chromatogram_0[j] = chromatogram_0[j + 1];
				}
				Array.Resize(ref chromatogram_0, chromatogram_0.Length - 1);
				method_24(chromatogram_0, spSignals.curSignalNo);
				SetSignalsColor();
				tcChrom_SelectedIndexChanged(null, null);
				break;
			}
		}
	}

	public void miFiCloseAll_Click(object sender, EventArgs e)
	{
		lclGridView_1.EndEdit();
		Array.Resize(ref chromatogram_0, 0);
		chromDisplay_0.ClearDisSignals();
		spSignals.SetSignals(0, -1);
		chromatogram_1 = null;
		method_39();
		method_38();
		tcChrom_SelectedIndexChanged(null, null);
	}

	private void miFiImportChrom_Click(object sender, EventArgs e)
	{
	}

	private void btnOpen_Click(object sender, EventArgs e)
	{
		if (chromatogram_0.Length >= 12)
		{
			int num = 12;
			MessageBox.Show(Lang.PS("最多同时显示", "At most ") + num + Lang.PS("张谱图！", "chromatograms"));
			return;
		}
		if (chromatogram_0.Length == 0)
		{
			chromDisplay_0.ClearDisSignals();
		}
		if (myOfdChrom_0.ShowDialog(this) != DialogResult.OK)
		{
			return;
		}
		string[] fileNames = myOfdChrom_0.FileNames;
		int num2 = 0;
		while (num2 < fileNames.Length)
		{
			string text = fileNames[num2].ToLower();
			bool flag = false;
			for (int i = 0; i < chromatogram_0.Length; i++)
			{
				if (!(chromatogram_0[i].fullName == text))
				{
					continue;
				}
				MessageBox.Show(Lang.PS("已打开谱图!", "Chrom. opened!") + "\n" + Path.GetFileName(text));
				if (1 == 0)
				{
					if (chromatogram_0.Length >= 12)
					{
						MessageBox.Show(Lang.PS("最多同时显示", "At most ") + 12 + Lang.PS("张谱图！", "chromatograms"));
						return;
					}
					OpenChrom(text, sampling: false, myOfdChrom_0.Checked);
				}
				num2++;
				break;
			}
		}
	}

	private void btnOverlayMode_Click(object sender, EventArgs e)
	{
		if (sender is ToolStripMenuItem)
		{
			ToolStripMenuItem toolStripMenuItem = sender as ToolStripMenuItem;
			ToolStripButton toolStripButton = btnOverlayMode;
			bool flag = (toolStripMenuItem.Checked = !toolStripMenuItem.Checked);
			toolStripButton.Checked = flag;
		}
		if (sender is ToolStripButton)
		{
			ToolStripButton toolStripButton2 = sender as ToolStripButton;
			ToolStripMenuItem toolStripMenuItem2 = miFiOverlayMode;
			bool flag = (toolStripButton2.Checked = !toolStripButton2.Checked);
			toolStripMenuItem2.Checked = flag;
		}
	}

	private void btnPreview_Click(object sender, EventArgs e)
	{
		instrument.form.dlgReportSetup.Link();
		instrument.form.dlgReportSetup.Preview();
	}

	private void btnPrint_Click(object sender, EventArgs e)
	{
		instrument.form.dlgReportSetup.Link();
		instrument.form.dlgReportSetup.print(refresh: false);
	}

	private void btnReportSetup_Click(object sender, EventArgs e)
	{
		if (openFileDialog_1 == null)
		{
			openFileDialog_1 = new OpenFileDialog();
			if (instrument.pjtDir != null)
			{
				openFileDialog_1.InitialDirectory = instrument.pjtDir.PjtFullName;
			}
			openFileDialog_1.Filter = Class49.MakeFileFilter(".sty");
		}
		if (openFileDialog_1.ShowDialog() == DialogResult.OK)
		{
			instrument.form.dlgReportSetup.ChromFormLoadStyFile(openFileDialog_1.FileName);
			instrument.form.rptSetup.LoadFromFile(openFileDialog_1.FileName);
		}
	}

	private void btnSave_Click(object sender, EventArgs e)
	{
		if (HasChrom)
		{
			method_40(CurChrom.fullName);
		}
	}

	private void miFiSaveAs_Click(object sender, EventArgs e)
	{
		if (HasChrom && !method_30(bool_6: true))
		{
			if (saveFileDialog_0 == null)
			{
				saveFileDialog_0 = new SaveFileDialog();
				saveFileDialog_0.Filter = "(*.chm)|*.chm";
			}
			if (saveFileDialog_0.ShowDialog() != DialogResult.Cancel)
			{
				method_40(saveFileDialog_0.FileName);
			}
		}
	}

	protected override void miHpHelp_Click(object sender, EventArgs e)
	{
		if (tcChrom.SelectedTab == tpResults)
		{
			if (chromatogram_0.Length != 0)
			{
				Class49.smethod_32("结果");
			}
			else
			{
				Class49.smethod_32("谱图处理");
			}
		}
		else if (tcChrom.SelectedTab == tpPerformance)
		{
			Class49.smethod_32("柱效");
		}
		else if (tcChrom.SelectedTab == tpIntegration)
		{
			Class49.smethod_32("积分");
		}
		else if (tcChrom.SelectedTab == tpRightsArchives)
		{
			Class49.smethod_32("权限");
		}
		else if (tcChrom.SelectedTab == tpSummary)
		{
			Class49.smethod_32("总结表");
		}
		else if (tcChrom.SelectedTab == tpSST)
		{
			Class49.smethod_32("组分验证");
		}
		else
		{
			Class49.smethod_32("谱图处理");
		}
	}

	private void toolStripMenuItem_0_Click(object sender, EventArgs e)
	{
		ToolStrip toolStrip = toolStrip2;
		bool visible = (toolStripMenuItem_0.Checked = !toolStripMenuItem_0.Checked);
		toolStrip.Visible = visible;
	}

	private void btngblDtecDelay_Click(object sender, EventArgs e)
	{
		method_29(bool_6: false);
		if (HasChrom && !method_30(bool_6: true) && !(dpgnlChrom.Cursor != Cursors.Default))
		{
			integRow_2.oprtStyle = (IntegOprtStyle)(sender as ToolStripItem).Tag;
			lbExpress.Text = integRow_2.ExpString(1);
			object_0 = sender;
			method_29(bool_6: true);
			method_0(bool_6: false);
		}
	}

	private void miMtdCaculation_Click(object sender, EventArgs e)
	{
		tcChrom.SelectedTab = tpResults;
	}

	private void miMtdIntegration_Click(object sender, EventArgs e)
	{
		tcChrom.SelectedTab = tpIntegration;
	}

	private void miMtdMeasurement_Click(object sender, EventArgs e)
	{
		tcChrom.SelectedTab = tpMsmCdts;
		tcMsmCdts.SelectedTab = tpInstrument;
	}

	private void miMtdSaveTplt_Click(object sender, EventArgs e)
	{
		if (sender == miMtdTplt)
		{
		}
	}

	private void miRltSmyOpt_Click(object sender, EventArgs e)
	{
		if (smyTabOptDlg_0.ShowDialog(smyTabOpt_0) == DialogResult.OK)
		{
			method_36();
		}
	}

	private void misstcClearParas_Click(object sender, EventArgs e)
	{
		if (sender != miSstNew && sender != misstcNew)
		{
			if (sender != miSstOpen && sender != misstcOpen)
			{
				if (sender == miSstSave || sender == misstcSave)
				{
					if (sst_0.sstCmpds.Length != 0)
					{
						if (sst_0.fullName == "")
						{
							misstcClearParas_Click(miSstSaveas, null);
						}
						else
						{
							sst_0.SaveToFile(sst_0.fullName);
						}
					}
					return;
				}
				if (sender == miSstSaveas || sender == misstcSaveas)
				{
					if (sst_0.sstCmpds.Length != 0)
					{
						if (saveFileDialog_1.ShowDialog() == DialogResult.OK)
						{
							sst_0.SaveToFile(saveFileDialog_1.FileName);
						}
						lbSSTFile.SetText(Lang.PS("文件： ", "File: ") + sst_0.fName);
					}
					return;
				}
				if (sender != miSstUpdateFromCalib && sender != misstcUpdateFromCalib)
				{
					if (sender != miSstSet && sender != misstcSet)
					{
						if (sender == miSstClearParas || sender == misstcClearParas)
						{
							sst_0.ClearParas();
						}
					}
					else if (sstparasDlg_0.ShowDialog(sst_0.sstParas) != DialogResult.OK)
					{
						return;
					}
				}
				else
				{
					SSTCmpd[] array = new SSTCmpd[0];
					for (int i = 0; i < chromatogram_0.Length; i++)
					{
						Chromatogram chromatogram = chromatogram_0[i];
						CaliGnl caliGnl = chromatogram.caliGnl;
						if (caliGnl == null || chromatogram.chromInfo.cclCalcu == CalcuStyle.Uncal)
						{
							continue;
						}
						int num = 0;
						while (num < caliGnl.cmpds.Length)
						{
							Compound compound = caliGnl.cmpds[num];
							bool flag = false;
							for (int j = 0; j < array.Length; j++)
							{
								if (array[j].RT != compound.cmpdInfo.retainTime)
								{
									continue;
								}
								if (1 == 0)
								{
									SSTCmpd sSTCmpd = sst_0.RetCmpds(compound.cmpdInfo.retainTime);
									int num2 = array.Length;
									Array.Resize(ref array, num2 + 1);
									if (sSTCmpd != null)
									{
										array[num2] = sSTCmpd;
									}
									else
									{
										SSTCmpd sSTCmpd2 = (array[num2] = new SSTCmpd());
										sSTCmpd2.name = compound.cmpdInfo.name;
										sSTCmpd2.RT = compound.cmpdInfo.retainTime;
										sSTCmpd2.fromCali = chromatogram.chromInfo.cclShowName;
									}
								}
								num++;
								break;
							}
						}
					}
					sst_0.sstCmpds = array;
				}
			}
			else
			{
				if (openFileDialog_3.ShowDialog() != DialogResult.OK)
				{
					return;
				}
				sst_0.LoadFromFile(openFileDialog_3.FileName);
				lbSSTFile.SetText(Lang.PS("文件： ", "File: ") + sst_0.fName);
			}
		}
		else
		{
			sst_0.fullName = (sst_0.fName = "");
			sst_0.ResetRecs();
			lbSSTFile.SetText(Lang.PS("文件： ", "File: ") + "[]");
		}
		method_37();
	}

	private void toolStripMenuItem_1_Click(object sender, EventArgs e)
	{
		ToolStrip toolStrip = toolStrip1;
		Lcl_SignalPanel lcl_SignalPanel = spSignals;
		bool flag = (toolStripMenuItem_1.Checked = !toolStripMenuItem_1.Checked);
		bool visible = (lcl_SignalPanel.Visible = flag);
		toolStrip.Visible = visible;
	}

	private string method_28()
	{
		string disMouseLgFmtY = chromDisplay_0.disMouseLgFmtY;
		float value = integRow_1.value;
		if ((double)value < 0.1)
		{
			return value * 1000f + " [μV]";
		}
		return integRow_1.value.ToString(disMouseLgFmtY) + " [" + integRow_1.ValueUnitStr + "]";
	}

	public void OpenChrom(string chromName, bool sampling, bool useCurrent)
	{
		if (sampling)
		{
			Chromatogram[] array = new Chromatogram[0];
			for (int i = 0; i < chromatogram_0.Length; i++)
			{
				if (chromatogram_0[i].fullName != chromName.ToLower())
				{
					int num = array.Length;
					Array.Resize(ref array, num + 1);
					array[num] = chromatogram_0[i];
				}
			}
			chromatogram_0 = array;
			if (chromatogram_0.Length >= 12)
			{
				return;
			}
		}
		Chromatogram chromatogram = Chromatogram.LoadFromFile2(chromName, detectorStyle_0);
		if (chromatogram == null)
		{
			return;
		}
		for (int j = 0; j < chromatogram.nrUserNames.Length; j++)
		{
			if (chromatogram.nrUserNames[j] == instrument.user.u_name)
			{
				MessageBox.Show(Lang.PS("您没有本谱图的读取权限！", "No right!"));
				return;
			}
		}
		if (!sampling)
		{
			if (HasChrom && useCurrent)
			{
				chromatogram.chromInfo.LoadFromObject(CurChrom.chromInfo);
				chromatogram.integ.LoadFromObject(CurChrom.integ);
				chromatogram.cus1_name = CurChrom.cus1_name;
				chromatogram.cus1_formula = CurChrom.cus1_formula;
				chromatogram.cus2_name = CurChrom.cus2_name;
				chromatogram.cus2_formula = CurChrom.cus2_formula;
			}
			if (chromatogram.fullName.EndsWith(".dat"))
			{
				ChromInfoR chromInfoR = chromatogram.chromInfoR;
				chromInfoR.LoadFromObject(instrument.methodSetup.chromInfoR);
				chromInfoR.DtcAcquisition.AcqRange = 10000f;
				chromInfoR.DtcAcquisition.AcqRate = 10f;
				chromInfoR.MtdFileName = Lang.PS("默认", "Default");
				chromatogram.canSetRs = true;
				if (!useCurrent)
				{
					chromatogram.chromInfo.LoadFromObject(instrument.methodSetup.chromInfo);
				}
			}
			if (chromatogram.fullName.EndsWith(".cdf"))
			{
				ChromInfoR chromInfoR2 = chromatogram.chromInfoR;
				chromInfoR2.LoadFromObject(instrument.methodSetup.chromInfoR);
				float val = chromatogram.signal._detector_maximum_value - chromatogram.signal._detector_minimum_value;
				val = Math.Max(10f, val);
				chromInfoR2.DtcAcquisition.AcqRange = val;
				chromInfoR2.DtcAcquisition.AcqRate = 1f / chromatogram.signal._actual_sampling_interval;
				chromInfoR2.MtdFileName = Lang.PS("默认", "Default");
				chromatogram.canSetRs = true;
				if (!useCurrent)
				{
					chromatogram.chromInfo.LoadFromObject(instrument.methodSetup.chromInfo);
				}
			}
			ChkOverlayMode();
		}
		int num2 = chromatogram_0.Length;
		Array.Resize(ref chromatogram_0, num2 + 1);
		chromatogram_0[num2] = chromatogram;
		chromatogram.Process(instrument.instruStyle);
		if (sampling)
		{
			float num3 = 0f;
			float num4 = 0f;
			float num5 = float.MaxValue;
			float num6 = float.MinValue;
			for (int k = 0; k < chromatogram_0.Length; k++)
			{
				Chromatogram chromatogram2 = chromatogram_0[k];
				num3 = Math.Min(num3, chromatogram2.signal.xMinTime);
				num4 = Math.Max(num4, chromatogram2.signal.xMaxTime);
				num5 = Math.Min(num5, chromatogram2.signal.yMinValue);
				num6 = Math.Max(num6, chromatogram2.signal.yMaxValue);
			}
			chromDisplay_0.CalcuFullDisLg(ref disLg_0, num3, num4, num5, num6);
			chromDisplay_0.stDisChain.AppendFrameLg(disLg_0);
		}
		else if (chromatogram_0.Length == 1)
		{
			chromDisplay_0.stDisChain.Clear();
			if (chromDisplay_0.SetFullDisLg(ref disLg_0, chromatogram.signal, second: true))
			{
				spSignals.curSignalNo = 0;
			}
			chromatogram_1 = chromatogram;
			method_38();
		}
		method_24(chromatogram_0, spSignals.curSignalNo);
		SetSignalsColor();
		LoadOptions();
		tcChrom_SelectedIndexChanged(null, null);
	}

	private void method_29(bool bool_6)
	{
		if (object_0 != null)
		{
			if (object_0 is ToolStripButton)
			{
				(object_0 as ToolStripButton).Checked = bool_6;
			}
			if (object_0 is ToolStripMenuItem)
			{
				(object_0 as ToolStripMenuItem).Checked = bool_6;
			}
		}
	}

	private void rbpfmStatistical_Click(object sender, EventArgs e)
	{
		if (rbpfmStatistical.Checked)
		{
			gvPerformStatic.Visible = true;
			gvPerformFrom50.Visible = false;
		}
		if (rbpfmFrom50per.Checked)
		{
			gvPerformStatic.Visible = false;
			gvPerformFrom50.Visible = true;
		}
	}

	private void rbasAdd_Click(object sender, EventArgs e)
	{
		if (HasChrom)
		{
			CurChrom.chromInfo.addChrom = rbasAdd.Checked;
			CurChrom.Process(instrument.instruStyle);
			tcChrom_SelectedIndexChanged(null, null);
		}
	}

	private void cbcuHideISTDPeak_Click(object sender, EventArgs e)
	{
		if (HasChrom)
		{
			CurChrom.chromInfo.rtrHideISTDPeak = cbcuHideISTDPeak.Checked;
			CurChrom.chromInfo.rtrRltReportPeaks = ((!rbcuAllDetectedPeaks.Checked) ? (rbcuIdentifiedPeaks.Checked ? RltReportPeaks.IdentifiedPeaks : RltReportPeaks.CaliPeaks) : RltReportPeaks.AllDetectedPeaks);
			CurChrom.chromInfo.prsUseScaleFactor = cbcuUseScaleFactor.Checked;
			method_1();
		}
	}

	private bool method_30(bool bool_6)
	{
		bool flag = !instrument.user.uar_EditChromatogram;
		if (bool_6 && flag)
		{
			MessageBox.Show(Lang.PS("受限！", "No Right！"));
		}
		return flag;
	}

	public override void ReadWinInfo(WinInfo winInfo)
	{
		base.ReadWinInfo(winInfo);
		try
		{
			if (winInfo.para1 > 0)
			{
				splitContainer.SplitterDistance = winInfo.para1;
			}
		}
		catch
		{
		}
		int gvNo = 0;
		winInfo.gvCF_w(gvRltsGnl, ref gvNo);
		winInfo.gvCF_w(gvRltsGpc, ref gvNo);
		winInfo.gvCF_w(gvRltsDad, ref gvNo);
		winInfo.gvCF_w(gvSummary, ref gvNo);
		winInfo.gvCF_w(gvPerformStatic, ref gvNo);
		winInfo.gvCF_w(gvPerformFrom50, ref gvNo);
		winInfo.gvCF_w(gvSSTCmpds, ref gvNo);
		winInfo.gvCF_w(gvSSTResults, ref gvNo);
		winInfo.gvCF_w(gvSlices, ref gvNo);
		winInfo.gvCF_w(gvgrPercent, ref gvNo);
		winInfo.gvCF_w(gvgrMw, ref gvNo);
	}

	private void method_31(float float_0, float float_1, float float_2, float float_3)
	{
		if (float_1 >= 0.001f && float_3 >= 0.001f)
		{
			disLg_0.lgXBeg = float_0;
			disLg_0.lgX = float_1;
			disLg_0.lgYBeg = float_2;
			disLg_0.lgY = float_3;
			chromDisplay_0.stDisChain.AppendFrameLg(disLg_0);
		}
	}

	private void method_32()
	{
		if (!HasChrom)
		{
			DataGridView dataGridView = gvPerformStatic;
			gvPerformFrom50.RowCount = 0;
			dataGridView.RowCount = 0;
			return;
		}
		tbpfmColumnUT.Text = CurChrom.chromInfo.ccColumnUT.ToString();
		tbpfmColumnLength.Text = CurChrom.chromInfo.ccColumnLength.ToString();
		gvPerformStatic.RowCount = 0;
		bool_2 = true;
		gvPerformFrom50.SuspendLayout();
		gvPerformFrom50.RowCount = CurChrom.PeaksNum;
		for (int i = 0; i < CurChrom.PeaksNum; i++)
		{
			Peak peak = CurChrom.RltPeaks[i];
			gvPerformFrom50.Rows[i].Tag = peak;
			gvPerformFrom50.Rows[i].Selected = peak.selected;
			for (int j = 0; j < gvPerformFrom50.ColumnCount; j++)
			{
				if (gvPerformFrom50.Columns[j].Visible)
				{
					gvPerformFrom50.Rows[i].Cells[j].Value = gvPerformFrom50Value(peak, gvPerformFrom50.Columns[j].Name);
				}
			}
		}
		gvPerformFrom50.ResumeLayout();
		bool_2 = false;
	}

	private void method_33()
	{
		if (!HasChrom)
		{
			lbRltExpress.Text = "[]";
			lclGridView_1.RowCount = 0;
			return;
		}
		if (cbRltCombine.Checked)
		{
			lbRltExpress.Text = "[整合]";
		}
		else
		{
			lbRltExpress.ForeColor = CurSignal.disColor;
			string disMouseLgFmtX = chromDisplay_0.disMouseLgFmtX;
			string disMouseLgFmtY = chromDisplay_0.disMouseLgFmtY;
			string text = CurChrom.chromInfo.cclCalcu.ToString();
			bool flag;
			if (flag = CurChrom.integ.GetNDRow(ref integRow_1) && integRow_1.success)
			{
				string text2 = text;
				text = text2 + "\n噪音 (" + integRow_1.timeA.ToString(disMouseLgFmtX) + " - " + integRow_1.timeB.ToString(disMouseLgFmtX) + "min): " + method_28();
			}
			if (CurChrom.integ.GetNDRow(ref integRow_0) && integRow_0.success)
			{
				string text3 = text;
				text = text3 + (flag ? "  " : "\n") + "飘移 (" + integRow_0.timeA.ToString(disMouseLgFmtX) + " - " + integRow_0.timeB.ToString(disMouseLgFmtX) + "min): " + integRow_0.value.ToString(disMouseLgFmtY) + " [" + integRow_0.ValueUnitStr + "]";
			}
			lbRltExpress.Text = text;
		}
		bool_3 = true;
		lclGridView_1.SuspendLayout();
		method_42(lclGridView_1, -1, "Amount", Lang.PS("数量", "Amount") + "\n[" + CurChrom.AmountUnit + "]");
		lclGridView_1.Columns["Cus1"].HeaderText = CurChrom.cus1_name;
		lclGridView_1.Columns["Cus2"].HeaderText = CurChrom.cus2_name;
		float whlArea;
		float whlHeight;
		float whlAmount;
		float whlAreaPer;
		float whlHeightPer;
		float whlAmountPer;
		Peak[] array = RltPeaks(chromatogram_0, CurChrom, cbRltCombine.Checked, out whlArea, out whlHeight, out whlAmount, out whlAreaPer, out whlHeightPer, out whlAmountPer);
		lclGridView_1.RowCount = array.Length + 1;
		lclGridView_1.hideRowIndex = array.Length;
		for (int i = 0; i < array.Length; i++)
		{
			Peak peak = array[i];
			lclGridView_1.Rows[i].Tag = peak;
			lclGridView_1.Rows[i].DefaultCellStyle.BackColor = peak._backColor;
			lclGridView_1.Rows[i].Selected = peak.selected;
			for (int j = 0; j < lclGridView_1.ColumnCount; j++)
			{
				if (lclGridView_1.Columns[j].Visible)
				{
					string name = lclGridView_1.Columns[j].Name;
					lclGridView_1.Rows[i].Cells[j].Value = gvRltsValue(peak, name, "", cbRltCombine.Checked);
				}
			}
		}
		int num = array.Length;
		lclGridView_1.Rows[num].Tag = null;
		lclGridView_1.Rows[num].DefaultCellStyle.BackColor = Color.White;
		for (int k = 0; k < lclGridView_1.ColumnCount; k++)
		{
			lclGridView_1.Rows[num].Cells[k].Value = "";
		}
		for (int l = 0; l < lclGridView_1.showColumns.Length; l++)
		{
			if (lclGridView_1.showColumns[l].DisplayIndex == 0)
			{
				method_42(lclGridView_1, num, lclGridView_1.showColumns[l].Name, Lang.PS("总计", "Total"));
				method_42(lclGridView_1, num, "Area", whlArea);
				method_42(lclGridView_1, num, "AreaPer", 100f * whlAreaPer);
				method_42(lclGridView_1, num, "Height", whlHeight);
				method_42(lclGridView_1, num, "HeightPer", 100f * whlHeightPer);
				method_42(lclGridView_1, num, "Amount", whlAmount);
				method_42(lclGridView_1, num, "AmountPer", 100f * whlAmountPer);
				lclGridView_1.ResumeLayout();
				bool_3 = false;
				break;
			}
		}
	}

	private void method_34()
	{
	}

	public override void refresh_once()
	{
		base.refresh_once();
		chromDisplay_0.instruStyle = instrument.instruStyle;
		SuspendLayout();
		detectorStyle_0 = DetectorStyle.General;
		method_39();
		gvInteg.Columns["Group"].Visible = true;
		tcChrom.TabPages.Clear();
		tcMsmCdts.TabPages.Clear();
		Control control = gvRltsGnl;
		Control control2 = gvRltsGpc;
		gvRltsDad.Visible = false;
		control2.Visible = false;
		control.Visible = false;
		gvInteg.Columns["Group"].Visible = true;
		ToolStripItem toolStripItem = miWinCaliGnl;
		mubtnCaliGnl.Visible = true;
		toolStripItem.Visible = true;
		ToolStripItem toolStripItem2 = miWinCaliGpc;
		mubtnCaliGpc.Visible = false;
		toolStripItem2.Visible = false;
		Control control3 = dpgnlChrom;
		pnlcu.Visible = true;
		control3.Visible = true;
		Control control4 = tcGPC;
		pnlgcu.Visible = false;
		control4.Visible = false;
		chromDisplay_0.displayPanel = dpgnlChrom;
		switch (instrument.instruStyle)
		{
		case InstruStyle.GC:
			tcChrom.TabPages.Add(tpResults);
			tcChrom.TabPages.Add(tpSummary);
			tcChrom.TabPages.Add(tpPerformance);
			tcChrom.TabPages.Add(tpIntegration);
			tcChrom.TabPages.Add(tpMsmCdts);
			tcChrom.TabPages.Add(tpSST);
			tcChrom.TabPages.Add(tpRightsArchives);
			tcMsmCdts.TabPages.Add(tpInstrument);
			tcMsmCdts.TabPages.Add(tpGC);
			gvRltsGnl.Visible = true;
			lclGridView_1 = gvRltsGnl;
			openFileDialog_2.Filter = CaliGnlUserCtrl.Filter;
			miRltPerformance.Visible = false;
			break;
		case InstruStyle.LC:
			tcChrom.TabPages.Add(tpResults);
			tcChrom.TabPages.Add(tpSummary);
			tcChrom.TabPages.Add(tpPerformance);
			tcChrom.TabPages.Add(tpIntegration);
			tcChrom.TabPages.Add(tpMsmCdts);
			tcChrom.TabPages.Add(tpSST);
			tcChrom.TabPages.Add(tpRightsArchives);
			tcMsmCdts.TabPages.Add(tpInstrument);
			tcMsmCdts.TabPages.Add(tpLC);
			gvRltsGnl.Visible = true;
			lclGridView_1 = gvRltsGnl;
			openFileDialog_2.Filter = CaliGnlUserCtrl.Filter;
			miRltPerformance.Visible = true;
			break;
		case InstruStyle.GPC:
		{
			tcChrom.TabPages.Add(tpResults);
			tcChrom.TabPages.Add(tpSummary);
			tcChrom.TabPages.Add(tpIntegration);
			tcChrom.TabPages.Add(tpMsmCdts);
			tcChrom.TabPages.Add(tpRanges);
			tcChrom.TabPages.Add(tpSlices);
			tcChrom.TabPages.Add(tpRightsArchives);
			tcMsmCdts.TabPages.Add(tpInstrument);
			gvRltsGpc.Visible = true;
			lclGridView_1 = gvRltsGpc;
			gvInteg.Columns["Group"].Visible = false;
			openFileDialog_2.Filter = CaliGpcForm.Filter;
			ToolStripItem toolStripItem3 = miWinCaliGnl;
			mubtnCaliGnl.Visible = false;
			toolStripItem3.Visible = false;
			ToolStripItem toolStripItem4 = miWinCaliGpc;
			mubtnCaliGpc.Visible = true;
			toolStripItem4.Visible = true;
			miRltPerformance.Visible = false;
			dpgnlChrom.Visible = false;
			tcGPC.Visible = true;
			chromDisplay_0.displayPanel = dpgpcChrom;
			pnlcu.Visible = false;
			pnlgcu.Visible = true;
			break;
		}
		case InstruStyle.PDA:
			tcChrom.TabPages.Add(tpResults);
			tcChrom.TabPages.Add(tpSummary);
			tcChrom.TabPages.Add(tpPerformance);
			tcChrom.TabPages.Add(tpIntegration);
			tcChrom.TabPages.Add(tpMsmCdts);
			tcChrom.TabPages.Add(tpSST);
			tcChrom.TabPages.Add(tpRightsArchives);
			tcMsmCdts.TabPages.Add(tpInstrument);
			tcMsmCdts.TabPages.Add(tpLC);
			tcMsmCdts.TabPages.Add(tpPDAMethod);
			gvRltsDad.Visible = true;
			lclGridView_1 = gvRltsDad;
			openFileDialog_2.Filter = CaliGnlUserCtrl.Filter;
			detectorStyle_0 = DetectorStyle.DAD;
			miRltPerformance.Visible = true;
			break;
		}
		if (!gvSummary.LoadFromManager())
		{
			mismySmyOpt_Click(mismyRestoreDftColumns, null);
		}
		method_36();
		ResumeLayout();
		splitContainer_SplitterMoved(null, null);
		if (lclGridView_1.Columns.Contains("RetenTime"))
		{
			chromDisplay_0.fmtPeakRT = lclGridView_1.ConvertValFmt("RetenTime");
		}
		chromDisplay_0.ExtDraw_begin();
		for (int i = 0; i < chromatogram_0.Length; i++)
		{
			chromatogram_0[i].CalcuResults(instrument.instruStyle);
		}
		if (chromatogram_0.Length != 0)
		{
			tcChrom_SelectedIndexChanged(null, null);
		}
	}

	private void method_35()
	{
		pnlSetRights.Enabled = false;
		cbrtaCanSetRs.Checked = false;
		if (!HasChrom)
		{
			for (int i = 0; i < gvSetRights.RowCount; i++)
			{
				DataGridViewCell dataGridViewCell = gvSetRights.Rows[i].Cells[0];
				object value = (gvSetRights.Rows[i].Cells[1].Value = false);
				dataGridViewCell.Value = value;
			}
			gvArchives.RowCount = 0;
			return;
		}
		LclPanel lclPanel = pnlSetRights;
		bool enabled = (cbrtaCanSetRs.Checked = CurChrom.canSetRs);
		lclPanel.Enabled = enabled;
		User[] users = UserAccountsDlg.userAccounts.users;
		gvSetRights.RowCount = users.Length;
		int num = 0;
		while (num < users.Length)
		{
			string u_name = users[num].u_name;
			gvSetRights.Rows[num].Cells[2].Value = u_name;
			bool flag = false;
			for (int j = 0; j < CurChrom.nrUserNames.Length; j++)
			{
				if (!(CurChrom.nrUserNames[j] == u_name))
				{
					continue;
				}
				flag = true;
				gvSetRights.Rows[num].Cells[0].Value = flag;
				flag = false;
				j = 0;
				while (j < CurChrom.wUserNames.Length)
				{
					if (!(CurChrom.wUserNames[j] == u_name))
					{
						j++;
						continue;
					}
					goto IL_01a0;
				}
				continue;
				IL_01a0:
				flag = true;
				gvSetRights.Rows[num].Cells[1].Value = flag;
				num++;
				break;
			}
		}
		gvArchives.RowCount = CurChrom.userArchives.Length;
		for (num = 0; num < gvArchives.RowCount; num++)
		{
			gvArchives.Rows[num].Cells["ArcUser"].Value = CurChrom.userArchives[num].userName;
			DateTime saveTime = CurChrom.userArchives[num].saveTime;
			gvArchives.Rows[num].Cells["ModifyT"].Value = saveTime.ToLongDateString() + " " + saveTime.ToLongTimeString();
			saveTime = CurChrom.userArchives[num].openTime;
			gvArchives.Rows[num].Cells["OpenT"].Value = ((num != 0) ? (saveTime.ToLongDateString() + " " + saveTime.ToLongTimeString()) : Lang.PS("创建", "Created"));
			gvArchives.Rows[num].Tag = CurChrom.userArchives[num];
			gvArchives.Rows[num].Selected = false;
		}
		if (CurChrom.idxUserArchive >= 0)
		{
			gvArchives.Rows[CurChrom.idxUserArchive].Selected = true;
		}
	}

	private void method_36()
	{
		string[] array = method_4(chromatogram_0);
		bool flag;
		if (!(flag = array.Length != 0))
		{
			Array.Resize(ref array, 2);
			array[0] = "组分1";
			array[1] = "组分2";
		}
		gvSummary.W_showComColumns();
		if (smyTabOpt_0.smyHdrPara == SmyHdrPara.Cmpd_Para)
		{
			gvSummary.combineH = 16;
			for (int i = 0; i < array.Length; i++)
			{
				gvSummary.W_cmpd(instrument.instruStyle, array[i]);
			}
		}
		else if (smyTabOpt_0.smyHdrPara == SmyHdrPara.Para_Cmpd)
		{
			gvSummary.combineH = 32;
			gvSummary.W_cmpds(instrument.instruStyle, array);
		}
		if (chromatogram_0.Length == 0)
		{
			gvSummary.RowCount = 0;
			return;
		}
		gvSummary.RowCount = chromatogram_0.Length;
		for (int j = 0; j < chromatogram_0.Length; j++)
		{
			for (int k = 0; k < gvSummary.showComColumns.Length; k++)
			{
				gvSummary.Rows[j].Cells[k].Value = gvSummaryComValue(chromatogram_0[j], gvSummary.showComColumns[k].Name);
			}
			if (flag)
			{
				for (int l = gvSummary.showComColumns.Length; l < gvSummary.ColumnCount; l++)
				{
					string[] array2 = gvSummary.Columns[l].Name.Split(default(char));
					gvSummary.Rows[j].Cells[l].Value = gvSummarySmyValue(chromatogram_0[j], array2[1], array2[0]);
				}
			}
		}
	}

	private void method_37()
	{
		sst_0.Calcu(chromatogram_0);
		bool_4 = true;
		gvSSTCmpds.RowCount = sst_0.sstCmpds.Length;
		bool_4 = false;
		for (int i = 0; i < gvSSTCmpds.RowCount; i++)
		{
			SSTCmpd sSTCmpd = sst_0.sstCmpds[i];
			gvSSTCmpds.Rows[i].Cells["Used"].Value = sSTCmpd.used;
			(gvSSTCmpds.Rows[i].Cells["OK"] as LclgvIconCell).Img = ((sSTCmpd.sstResult == SSTResult.None) ? null : ((sSTCmpd.sstResult == SSTResult.Success) ? SystemIconResource.smethod_25() : ((sSTCmpd.sstResult == SSTResult.Fail) ? SystemIconResource.smethod_24() : SystemIconResource.smethod_26())));
			gvSSTCmpds.InvalidateCell(gvSSTCmpds.Columns["OK"].Index, i);
			gvSSTCmpds.Rows[i].Cells["CmpdName"].Value = sSTCmpd.name;
			gvSSTCmpds.Rows[i].Cells["RetenTime"].Value = sSTCmpd.RT;
			gvSSTCmpds.Rows[i].Tag = sSTCmpd;
		}
		gvSSTCmpds_SelectionChanged(null, null);
	}

	private void method_38()
	{
		ToolStripMenuItem toolStripMenuItem = miDisPreviousZoom;
		bool enabled = (btnPreviousZoom.Enabled = chromDisplay_0.stDisChain.HasPrevious);
		toolStripMenuItem.Enabled = enabled;
		ToolStripMenuItem toolStripMenuItem2 = miDisNextZoom;
		enabled = (btnNextZoom.Enabled = chromDisplay_0.stDisChain.HasNext);
		toolStripMenuItem2.Enabled = enabled;
	}

	private void method_39()
	{
		Text = Lang.PS("谱图处理", "Chrom. Process") + ((chromatogram_1 != null) ? (": " + chromatogram_1.fullName) : "");
		slbExplain.Text = instrument.name;
	}

	public string RetSstItem(Peak peak, string item)
	{
		float value = SstItem.getValue(peak, item, sst_0.sstParas.criterion);
		if (!float.IsNaN(value) && (item == "Asymmetry" || item == "SymTail" || value > 0f))
		{
			return value.ToString(gvSSTResults.ConvertValFmt(item));
		}
		return "";
	}

	public string RetSstItem(SstItem sstItem, int rowIndex)
	{
		float f = float.NaN;
		if (sstItem != null)
		{
			switch (rowIndex)
			{
			case 0:
				f = sstItem.upperLimit;
				break;
			case 1:
				f = sstItem.lowerLimit;
				break;
			case 2:
				f = sstItem.rsdPerLimit;
				break;
			case 3:
				f = sstItem.mean;
				break;
			case 4:
				f = sstItem.rsdPer;
				break;
			}
		}
		if (float.IsNaN(f))
		{
			return "";
		}
		return f.ToString(gvSSTResults.ConvertValFmt(sstItem.item));
	}

	public static Color RetSstMeanClr(SstItem sstItem)
	{
		if (sstItem == null)
		{
			return Color.Black;
		}
		if (float.IsNaN(sstItem.upperLimit) && float.IsNaN(sstItem.lowerLimit))
		{
			return Color.Blue;
		}
		if (sstItem.rltUpper && sstItem.rltLower)
		{
			return Color.Black;
		}
		return Color.Red;
	}

	public static Color RetSstRsdPerClr(SstItem sstItem)
	{
		if (sstItem == null)
		{
			return Color.Black;
		}
		if (float.IsNaN(sstItem.rsdPerLimit))
		{
			return Color.Blue;
		}
		if (!sstItem.rltRsdPer)
		{
			return Color.Red;
		}
		return Color.Black;
	}

	public static Peak[] RltPeaks(Chromatogram[] chroms, Chromatogram curChrom, bool combine, out float whlArea, out float whlHeight, out float whlAmount, out float whlAreaPer, out float whlHeightPer, out float whlAmountPer)
	{
		Peak[] array = new Peak[0];
		whlArea = (whlHeight = (whlAmount = (whlAreaPer = (whlHeightPer = (whlAmountPer = 0f)))));
		if (!combine)
		{
			array = curChrom.RltPeaks;
			whlArea = curChrom.whlArea;
			whlAreaPer = curChrom.whlAreaPer;
			whlHeight = curChrom.whlHeight;
			whlHeightPer = curChrom.whlHeightPer;
			whlAmount = curChrom.whlAmount;
			whlAmountPer = curChrom.whlAmountPer;
			foreach (Peak peak in array)
			{
				if (peak.name.Contains("."))
				{
					peak.name = peak.name.Remove(0, peak.name.IndexOf(".") + 1);
				}
				peak._backColor = ((curChrom.chromInfo.cclCalcu != CalcuStyle.ISTD || !peak.IsIstd) ? Color.White : istdBkColor);
			}
			return array;
		}
		foreach (Chromatogram chromatogram in chroms)
		{
			int num = array.Length;
			Array.Resize(ref array, num + chromatogram.PeaksNum);
			whlArea += chromatogram.whlArea;
			whlHeight += chromatogram.whlHeight;
			whlAmount += chromatogram.whlAmount;
			for (int k = num; k < array.Length; k++)
			{
				Peak peak2 = (array[k] = chromatogram.RltPeaks[k - num]);
				if (!peak2.name.StartsWith(chromatogram.fName))
				{
					peak2.name = chromatogram.fName + "." + peak2.name;
				}
				peak2._backColor = ((chromatogram.chromInfo.cclCalcu != CalcuStyle.ISTD || !peak2.IsIstd) ? Color.White : istdBkColor);
			}
		}
		foreach (Peak peak3 in array)
		{
			peak3._areaPer = (peak3._heightPer = (peak3._amountPer = -1f));
			if (peak3.area > 0f)
			{
				peak3._areaPer = peak3.area / whlArea;
				whlAreaPer += peak3._areaPer;
			}
			else
			{
				peak3._areaPer = -1f;
			}
			if (peak3.height > 0f)
			{
				peak3._heightPer = peak3.height / whlHeight;
				whlHeightPer += peak3._heightPer;
			}
			else
			{
				peak3._heightPer = -1f;
			}
			if (peak3.amount > 0f)
			{
				peak3._amountPer = peak3.amount / whlAmount;
				whlAmountPer += peak3._amountPer;
			}
			else
			{
				peak3._amountPer = -1f;
			}
		}
		return array;
	}

	private void method_40(string string_285)
	{
		if (!HasChrom || method_30(bool_6: true))
		{
			return;
		}
		bool flag = false;
		if (CurChrom.userArchives.Length == 0)
		{
			if (CurChrom.wUserNames.Length == 0 && MessageBox.Show(Lang.PS("您的权限设置,将没有任何用户再能修改谱图，确定？", "Your rights set would let no people can modify this chromatogram, \nAre you sure?"), Lang.PS("提示", "Note"), MessageBoxButtons.OKCancel) != DialogResult.OK)
			{
				return;
			}
			Array.Resize(ref CurChrom.userArchives, 1);
			UserArchive userArchive = (CurChrom.userArchives[0] = new UserArchive());
			userArchive.chromInfo.LoadFromObject(CurChrom.chromInfo);
			userArchive.integ.LoadFromObject(CurChrom.integ);
			userArchive.SL_lbTexts(load: true, ref CurChrom.signal.lbTexts);
			userArchive.SL_lbLines(load: true, ref CurChrom.signal.lbLines);
			userArchive.userName = instrument.user.u_name;
			userArchive.saveTime = DateTime.Now;
			userArchive.remark = tbrtaRemark.Text;
			CurChrom.SaveToFile(string_285);
			flag = true;
		}
		else if (CurChrom.CanWrite(instrument.user.u_name))
		{
			int num = CurChrom.userArchives.Length;
			UserArchive userArchive2 = CurChrom.userArchives[num - 1];
			if (CurChrom.openTime != userArchive2.openTime)
			{
				Array.Resize(ref CurChrom.userArchives, num + 1);
				userArchive2 = (CurChrom.userArchives[num] = new UserArchive());
			}
			userArchive2.chromInfo.LoadFromObject(CurChrom.chromInfo);
			userArchive2.integ.LoadFromObject(CurChrom.integ);
			userArchive2.SL_lbTexts(load: true, ref CurChrom.signal.lbTexts);
			userArchive2.SL_lbLines(load: true, ref CurChrom.signal.lbLines);
			userArchive2.userName = instrument.user.u_name;
			userArchive2.openTime = CurChrom.openTime;
			userArchive2.saveTime = DateTime.Now;
			userArchive2.remark = tbrtaRemark.Text;
			CurChrom.SaveToFile(string_285);
			flag = true;
		}
		if (flag)
		{
			method_39();
			tcChrom_SelectedIndexChanged(null, null);
		}
	}

	private void method_41(bool bool_6)
	{
		Control control = toolStrip2;
		ToolStripItem toolStripItem = miChmGlobal;
		ToolStripItem toolStripItem2 = miChmItgPeak;
		ToolStripItem toolStripItem3 = miChmBaseline;
		ToolStripItem toolStripItem4 = miChmNoiseDrift;
		toolStripMenuItem_0.Checked = bool_6;
		toolStripItem4.Enabled = bool_6;
		toolStripItem3.Enabled = bool_6;
		toolStripItem2.Enabled = bool_6;
		toolStripItem.Enabled = bool_6;
		control.Visible = bool_6;
	}

	public void SetChromsLink(ref Chromatogram[] chroms, ref int activeNo, ref SST sst_1)
	{
		chroms = chromatogram_0;
		activeNo = spSignals.curSignalNo;
		sst_1 = sst_0;
	}

	private bool method_42(LclGridView lclGridView_2, int int_0, string string_285, object object_1)
	{
		if (lclGridView_2.Columns.Contains(string_285) && lclGridView_2.Columns[string_285].Visible)
		{
			if (int_0 >= 0)
			{
				lclGridView_2.Rows[int_0].Cells[string_285].Value = object_1;
			}
			else
			{
				lclGridView_2.Columns[string_285].HeaderText = object_1.ToString();
			}
			return true;
		}
		return false;
	}

	public void SetPanelColors()
	{
		spSignals.SetColors(instrument.user.options.sgColors);
	}

	public override void SetProjectDir(string projectDir)
	{
		base.SetProjectDir(projectDir);
		myOfdChrom_0.InitialDirectory = instrument.PrjPath;
		openFileDialog_3.InitialDirectory = instrument.PrjPath;
		saveFileDialog_1.InitialDirectory = instrument.PrjPath;
	}

	public void SetSignalsColor()
	{
		for (int i = 0; i < chromatogram_0.Length; i++)
		{
			if (i < 12)
			{
				chromatogram_0[i].signal.disColor = instrument.user.options.sgColors[i];
			}
		}
	}

	private void method_43()
	{
		if (!HasChrom)
		{
			return;
		}
		ChromInfo chromInfo = CurChrom.chromInfo;
		ChromInfoR chromInfoR = CurChrom.chromInfoR;
		tbmsmMtdDspt.Text = chromInfo.msmMtdDspt;
		tbmsmColumn.Text = chromInfo.msmColumn;
		tbmsmMobilePhase.Text = chromInfo.msmMobilePhase;
		tbmsmFlowRate.Text = chromInfo.msmFlowRate;
		tbmsmPressure.Text = chromInfo.msmPressure;
		tbmsmDetection.Text = chromInfo.msmDetection;
		tbmsmTemperature.Text = chromInfo.msmTemperature;
		tbmsmNote.Text = chromInfo.msmNote;
		tbsiSampleID.Text = CMS_InfoParasFMT.FmtStr(0, CurChrom.injAnalysis, instrument);
		tbsiSample.Text = CMS_InfoParasFMT.FmtStr(1, CurChrom.injAnalysis, instrument);
		DateTime dtAcquire = CurChrom.injAnalysis.dtAcquire;
		lbsiAcquiredTimeV.Text = dtAcquire.ToLongDateString() + "  " + dtAcquire.ToLongTimeString();
		lbsiAnalystV.Text = CurChrom.injAnalysis.analyst;
		lbapAutoStopV.Text = (chromInfoR.AcqAutoStop ? (chromInfoR.AcqRunTime + " min") : Lang.PS("否", "No"));
		lbapRangeV.Text = chromInfoR.DtcAcquisition.AcqRange + " " + Class49.string_16;
		lbapExtStartV.Text = (chromInfoR.EcExternalControl ? chromInfoR.ExtCtrlStart.ToString() : Lang.PS("否", "No"));
		pbapES.Image = ((!chromInfoR.EcExternalControl) ? null : ((chromInfoR.ExtCtrlSignal == ExtCtrlSignal.Up) ? SystemBitmapResource6.smethod_1() : SystemBitmapResource6.smethod_0()));
		lbapSamplingV.Text = chromInfoR.DtcAcquisition.AcqRate + Lang.PS(" 点/秒", " dots/sec.");
		lbapMethodV.Text = chromInfoR.MtdFileName;
		tbasChrom.Text = "[]";
		rbasAdd.Checked = chromInfo.addChrom;
		rbasSub.Checked = !chromInfo.addChrom;
		cbasMatching.SelectedItem = null;
		string asChrom = CurChrom.chromInfo.asChrom;
		if (asChrom != "")
		{
			FileInfo fileInfo = new FileInfo(asChrom);
			if (fileInfo.Exists)
			{
				tbasChrom.Text = fileInfo.Name.Remove(fileInfo.Name.Length - fileInfo.Extension.Length);
				cbasMatching.SelectedItem = CurChrom.chromInfo.asMatching;
			}
			else
			{
				tbasChrom.Text = "[文件丢失]";
			}
		}
		instrument.form.dlgMethodSetup.ReadLcGradient(gvlcGradient, chromInfoR.LcGradient);
		for (int i = 0; i < chromInfoR.GcProgTemp.SetT6.Length; i++)
		{
			dgvCT6.Rows[i].Cells[1].Value = chromInfoR.GcProgTemp.SetT6[i];
		}
		instrument.form.dlgMethodSetup.ReadGcProgTemp(gvgcProgTemp, chromInfoR.GcProgTemp);
	}

	private void method_44()
	{
		if (HasChrom)
		{
			tbclb.Text = CurChrom.chromInfo.cclShowName;
			cbcuCalcu.SelectedItem = CurChrom.chromInfo.cclCalcu;
			cbcuUncalBase.SelectedItem = CurChrom.chromInfo.prsUncalBase;
			tbcuUncalAmtRespF.Text = CurChrom.chromInfo.prsUncalAmtRespF.ToString();
			cbcuUseScaleFactor.Checked = CurChrom.chromInfo.prsUseScaleFactor;
			tbcuScaleFactor.Text = CurChrom.chromInfo.prsScaleFactor.ToString();
			tbcuUnitAfterScale.Text = CurChrom.chromInfo.prsUnitAfterScale;
			cbcuHideISTDPeak.Checked = CurChrom.chromInfo.rtrHideISTDPeak;
			switch (CurChrom.chromInfo.rtrRltReportPeaks)
			{
			case RltReportPeaks.AllDetectedPeaks:
				rbcuAllDetectedPeaks.Checked = true;
				break;
			case RltReportPeaks.IdentifiedPeaks:
				rbcuIdentifiedPeaks.Checked = true;
				break;
			case RltReportPeaks.CaliPeaks:
				rbcuCaliPeaks.Checked = true;
				break;
			}
			string amountUnit = CurChrom.AmountUnit;
			lbcuAmount.Text = Lang.PS("数量", "Amount") + ((amountUnit != "") ? ("[" + amountUnit + "]") : "");
			tbcuAmount.Text = CurChrom.chromInfo.amount.ToString();
			float istdAmount = CurChrom.chromInfo.GetIstdAmount(-1f);
			bool flag = CurChrom.chromInfo.cclCalcu != CalcuStyle.ISTD || (CurChrom.chromInfo.cclCalcu == CalcuStyle.ISTD && CurChrom.IstdNum <= 1);
			tbcuIstdAmount.Enabled = flag;
			tbcuIstdAmount.Text = (flag ? istdAmount.ToString() : "列表行输入");
			tbcuInjVolume.Text = CurChrom.chromInfo.injVolumn.ToString();
			tbcuDilution.Text = CurChrom.chromInfo.dilution.ToString();
		}
	}

	private void method_45(InstruStyle instruStyle_0)
	{
		switch (instruStyle_0)
		{
		case InstruStyle.GC:
		case InstruStyle.LC:
			gvSummary.ArraySmySHColumns(instruStyle_0, show: true, 3);
			gvSummary.AddSmyShowLink(instruStyle_0, 0, "RetenTime");
			gvSummary.AddSmyShowLink(instruStyle_0, 1, "WO5");
			gvSummary.AddSmyShowLink(instruStyle_0, 2, "Amount");
			break;
		case InstruStyle.GPC:
			gvSummary.ArraySmySHColumns(instruStyle_0, show: true, 4);
			gvSummary.AddSmyShowLink(instruStyle_0, 0, "Mn");
			gvSummary.AddSmyShowLink(instruStyle_0, 1, "Mw");
			gvSummary.AddSmyShowLink(instruStyle_0, 2, "Mz");
			gvSummary.AddSmyShowLink(instruStyle_0, 3, "Mv");
			break;
		case InstruStyle.PDA:
			gvSummary.ArraySmySHColumns(instruStyle_0, show: true, 3);
			gvSummary.AddSmyShowLink(instruStyle_0, 0, "PeakPurity");
			gvSummary.AddSmyShowLink(instruStyle_0, 1, "NameMatch");
			gvSummary.AddSmyShowLink(instruStyle_0, 2, "BestMatch");
			break;
		}
		gvSummary.FinishSmyHideLinks(instruStyle_0);
	}

	private void splitContainer_SplitterMoved(object sender, SplitterEventArgs e)
	{
		if (lbExpress.Height == 1)
		{
			Graphics graphics = Graphics.FromHwnd(lbExpress.Handle);
			SizeF sizeF = graphics.MeasureString("检测", lbExpress.Font);
			lbExpress.Height = Convert.ToInt32(sizeF.Height) + 1;
			graphics.Dispose();
		}
		if (dpgnlChrom.Visible)
		{
			lbExpress.Top = splitContainer.Panel1.Height - lbExpress.Height;
		}
		else
		{
			lbExpress.Top = splitContainer.Panel1.Height - tcGPC.ItemSize.Height - lbExpress.Height - 3;
		}
	}

	private void method_46(bool bool_6, int int_0)
	{
		if (bool_6)
		{
			if (int_0 == spSignals.curSignalNo)
			{
				return;
			}
			method_24(chromatogram_0, int_0);
		}
		else
		{
			spSignals.RefreshColors(ref instrument.user.options.sgColors);
			SetSignalsColor();
			chromDisplay_0.RefreshSignalLabels = true;
		}
		tcChrom_SelectedIndexChanged(null, null);
	}

	private void method_47(object sender, KeyEventArgs e)
	{
		if ((Keys.D0 <= e.KeyCode && e.KeyCode <= Keys.D9) || (Keys.NumPad0 <= e.KeyCode && e.KeyCode <= Keys.NumPad9) || e.KeyCode == Keys.Back || e.KeyCode == Keys.Delete)
		{
			bool_1 = true;
		}
	}

	private void method_48(object sender, KeyPressEventArgs e)
	{
		if (!bool_1)
		{
			e.Handled = true;
		}
		bool_1 = false;
	}

	private void tbcuScaleFactor_KeyPress(object sender, KeyPressEventArgs e)
	{
		if (!HasChrom || e.KeyChar != Convert.ToChar(13))
		{
			return;
		}
		LclTextBox lclTextBox = sender as LclTextBox;
		e.Handled = true;
		if (sender == tbcuUncalAmtRespF)
		{
			float num = Class49.String2Float(lclTextBox.Text, CurChrom.chromInfo.prsUncalAmtRespF);
			if (num >= 0f)
			{
				CurChrom.chromInfo.prsUncalAmtRespF = num;
			}
		}
		else if (sender == tbcuScaleFactor)
		{
			float num2 = Class49.String2Float(lclTextBox.Text, CurChrom.chromInfo.prsScaleFactor);
			if (num2 > 0f)
			{
				CurChrom.chromInfo.prsScaleFactor = num2;
			}
		}
		else
		{
			if (sender == tbcuUnitAfterScale)
			{
				CurChrom.chromInfo.prsUnitAfterScale = lclTextBox.Text;
				tcChrom_SelectedIndexChanged(null, null);
				return;
			}
			if (sender == tbcuAmount)
			{
				float num3 = Class49.String2Float(lclTextBox.Text, CurChrom.chromInfo.amount);
				if (num3 >= 0f)
				{
					CurChrom.chromInfo.amount = num3;
				}
			}
			else if (sender == tbcuIstdAmount)
			{
				float istdAmount = CurChrom.chromInfo.GetIstdAmount(-1f);
				float num4 = Class49.String2Float(lclTextBox.Text, istdAmount);
				if (num4 >= 0f)
				{
					CurChrom.chromInfo.SetIstdAmount(-1f, num4);
				}
			}
			else if (sender != tbcuInjVolume && sender == tbcuDilution)
			{
				float num5 = Class49.String2Float(lclTextBox.Text, CurChrom.chromInfo.dilution);
				if (num5 > 0f)
				{
					CurChrom.chromInfo.dilution = num5;
				}
			}
		}
		method_1();
	}

	private void tbgcuAlpha_KeyPress(object sender, KeyPressEventArgs e)
	{
	}

	private void tbsiSampleID_KeyPress(object sender, KeyPressEventArgs e)
	{
		if (HasChrom && e.KeyChar == Convert.ToChar(13))
		{
			if (sender != tbmsmNote)
			{
				e.Handled = true;
			}
			ChromInfo chromInfo = CurChrom.chromInfo;
			chromInfo.msmMtdDspt = tbmsmMtdDspt.Text;
			chromInfo.msmColumn = tbmsmColumn.Text;
			chromInfo.msmMobilePhase = tbmsmMobilePhase.Text;
			chromInfo.msmFlowRate = tbmsmFlowRate.Text;
			chromInfo.msmPressure = tbmsmPressure.Text;
			chromInfo.msmDetection = tbmsmDetection.Text;
			chromInfo.msmTemperature = tbmsmTemperature.Text;
			chromInfo.msmNote = tbmsmNote.Text;
			CurChrom.injAnalysis.sampleID = tbsiSampleID.Text;
			CurChrom.injAnalysis.sample = tbsiSample.Text;
		}
	}

	private void tbpfmColumnUT_KeyDown(object sender, KeyEventArgs e)
	{
		if (HasChrom && e.KeyCode == Keys.Return)
		{
			CurChrom.chromInfo.ccColumnUT = Class49.String2Float(tbpfmColumnUT.Text, CurChrom.chromInfo.ccColumnUT);
			CurChrom.chromInfo.ccColumnLength = Class49.String2Float(tbpfmColumnLength.Text, CurChrom.chromInfo.ccColumnLength);
			CurChrom.CalcuPerformanceAndCus();
			method_32();
		}
	}

	private void tbSigYEnd_DoubleClick(object sender, EventArgs e)
	{
		float num = Class49.String2Float(tbTime.Text, disLg_0.lgX);
		float num2 = Class49.String2Float(tbSigYBeg.Text, disLg_0.lgYBeg);
		float num3 = Class49.String2Float(tbSigYEnd.Text, disLg_0.lgYBeg + disLg_0.lgY);
		if (num < 0.1f || (num2 == 0f && num3 == 0f))
		{
			disLg_0.lgXBeg = 0f;
			num = 0.2f;
			num2 = -1f;
			num3 = 10f;
		}
		disLg_0.lgXBeg = 0f;
		disLg_0.lgX = num;
		disLg_0.lgYBeg = num2;
		disLg_0.lgY = num3 - num2;
		chromDisplay_0.stDisChain.AppendFrameLg(disLg_0);
		DisDpRefresh();
	}

	private void tbSigYEnd_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode == Keys.Return)
		{
			tbSigYEnd_DoubleClick(null, null);
		}
	}

	private void tcChrom_SelectedIndexChanged(object sender, EventArgs e)
	{
		if (tcChrom.SelectedTab == tpResults)
		{
			lclGridView_1.EndEdit();
			method_33();
			method_44();
		}
		else if (tcChrom.SelectedTab == tpSummary)
		{
			method_36();
		}
		else if (tcChrom.SelectedTab == tpPerformance)
		{
			method_32();
		}
		else if (tcChrom.SelectedTab == tpIntegration)
		{
			method_41(bool_6: true);
			if (HasChrom)
			{
				gvInteg.Refresh(AccStyle.Read, CurChrom.integ);
			}
			else
			{
				gvInteg.Refresh(AccStyle.Clear, null);
			}
		}
		else if (tcChrom.SelectedTab == tpMsmCdts)
		{
			method_43();
		}
		else if (tcChrom.SelectedTab == tpSST)
		{
			method_37();
		}
		else if (tcChrom.SelectedTab != tpSlices && tcChrom.SelectedTab != tpRanges && tcChrom.SelectedTab == tpRightsArchives)
		{
			method_35();
		}
		DisDpRefresh();
	}

	private void tcGPC_Resize(object sender, EventArgs e)
	{
		dpgnlChrom.Height = tcGPC.Height;
		lbExpress.Width = dpgnlChrom.Width - 2;
	}

	private void tsDatAcq_Paint(object sender, PaintEventArgs e)
	{
		ToolStrip toolStrip = sender as ToolStrip;
		for (int i = 0; i < toolStrip.Items.Count; i++)
		{
			if (toolStrip.Items[i] is ToolStripTextBox)
			{
				ToolStripTextBox toolStripTextBox = toolStrip.Items[i] as ToolStripTextBox;
				if (toolStripTextBox.Visible)
				{
					toolStripTextBox.Height = 17;
					toolStripTextBox.AutoSize = false;
					Rectangle bounds = toolStripTextBox.Bounds;
					bounds.Offset(-1, -1);
					bounds.Width += 3;
					e.Graphics.DrawRectangle(Pens.Gray, bounds);
				}
			}
		}
	}

	public override void WriteWinInfo(WinInfo winInfo)
	{
		base.WriteWinInfo(winInfo);
		winInfo.para1 = splitContainer.SplitterDistance;
		winInfo.gvCF_r(gvRltsGnl);
		winInfo.gvCF_r(gvRltsGpc);
		winInfo.gvCF_r(gvRltsDad);
		winInfo.gvCF_r(gvSummary);
		winInfo.gvCF_r(gvPerformStatic);
		winInfo.gvCF_r(gvPerformFrom50);
		winInfo.gvCF_r(gvSSTCmpds);
		winInfo.gvCF_r(gvSSTResults);
		winInfo.gvCF_r(gvSlices);
		winInfo.gvCF_r(gvgrPercent);
		winInfo.gvCF_r(gvgrMw);
	}

	protected override void Dispose(bool disposing)
	{
		if (disposing && icontainer_2 != null)
		{
			icontainer_2.Dispose();
		}
		base.Dispose(disposing);
	}

	private void InitializeComponent()
	{
		this.components = new System.ComponentModel.Container();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle12 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle13 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle14 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle15 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle16 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle17 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle18 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle19 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle20 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle21 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle22 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle23 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle24 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle25 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle26 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle27 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle28 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle29 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle30 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle31 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle32 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle33 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle34 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle35 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle36 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle37 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle38 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle39 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle40 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle41 = new System.Windows.Forms.DataGridViewCellStyle();
		this.msChrom = new System.Windows.Forms.MenuStrip();
		this.miFile = new System.Windows.Forms.ToolStripMenuItem();
		this.miFiOverlayMode = new System.Windows.Forms.ToolStripMenuItem();
		this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
		this.miFiOpen = new System.Windows.Forms.ToolStripMenuItem();
		this.miFiClose = new System.Windows.Forms.ToolStripMenuItem();
		this.miFiCloseAll = new System.Windows.Forms.ToolStripMenuItem();
		this.miFiSave = new System.Windows.Forms.ToolStripMenuItem();
		this.miFiSaveAs = new System.Windows.Forms.ToolStripMenuItem();
		this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
		this.miFiImportChrom = new System.Windows.Forms.ToolStripMenuItem();
		this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
		this.miFiReportSetup = new System.Windows.Forms.ToolStripMenuItem();
		this.miFiPreview = new System.Windows.Forms.ToolStripMenuItem();
		this.miFiPrint = new System.Windows.Forms.ToolStripMenuItem();
		this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
		this.miFiExit = new System.Windows.Forms.ToolStripMenuItem();
		this.miDisplay = new System.Windows.Forms.ToolStripMenuItem();
		this.miDisPreviousZoom = new System.Windows.Forms.ToolStripMenuItem();
		this.miDisNextZoom = new System.Windows.Forms.ToolStripMenuItem();
		this.miDisUnzoom = new System.Windows.Forms.ToolStripMenuItem();
		this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
		this.miDisProperties = new System.Windows.Forms.ToolStripMenuItem();
		this.miChromatogram = new System.Windows.Forms.ToolStripMenuItem();
		this.miChmGlobal = new System.Windows.Forms.ToolStripMenuItem();
		this.migblPeakWidth = new System.Windows.Forms.ToolStripMenuItem();
		this.migblThreshold = new System.Windows.Forms.ToolStripMenuItem();
		this.migblPkSlope = new System.Windows.Forms.ToolStripMenuItem();
		this.toolStripSeparator32 = new System.Windows.Forms.ToolStripSeparator();
		this.migblDtecDelay = new System.Windows.Forms.ToolStripMenuItem();
		this.miChmItgPeak = new System.Windows.Forms.ToolStripMenuItem();
		this.miipResetDtecNeg = new System.Windows.Forms.ToolStripMenuItem();
		this.miipClampNeg = new System.Windows.Forms.ToolStripMenuItem();
		this.toolStripSeparator25 = new System.Windows.Forms.ToolStripSeparator();
		this.miipPkWidth = new System.Windows.Forms.ToolStripMenuItem();
		this.miipPkThreshold = new System.Windows.Forms.ToolStripMenuItem();
		this.miipPkAddPosi = new System.Windows.Forms.ToolStripMenuItem();
		this.miipPkAddNeg = new System.Windows.Forms.ToolStripMenuItem();
		this.miipPkCut = new System.Windows.Forms.ToolStripMenuItem();
		this.miipPkHalfWidth = new System.Windows.Forms.ToolStripMenuItem();
		this.miipPkArea = new System.Windows.Forms.ToolStripMenuItem();
		this.toolStripSeparator27 = new System.Windows.Forms.ToolStripSeparator();
		this.miipPkVale = new System.Windows.Forms.ToolStripMenuItem();
		this.toolStripSeparator26 = new System.Windows.Forms.ToolStripSeparator();
		this.miipSolventPeak = new System.Windows.Forms.ToolStripMenuItem();
		this.miipFlowMarker = new System.Windows.Forms.ToolStripMenuItem();
		this.miipGroups = new System.Windows.Forms.ToolStripMenuItem();
		this.miChmBaseline = new System.Windows.Forms.ToolStripMenuItem();
		this.mibsBsTgnt = new System.Windows.Forms.ToolStripMenuItem();
		this.mibsBsVtV = new System.Windows.Forms.ToolStripMenuItem();
		this.toolStripSeparator9 = new System.Windows.Forms.ToolStripSeparator();
		this.mibsBsValley = new System.Windows.Forms.ToolStripMenuItem();
		this.mibsBsTogether = new System.Windows.Forms.ToolStripMenuItem();
		this.mibsBsForwHorz = new System.Windows.Forms.ToolStripMenuItem();
		this.mibsBsBackHorz = new System.Windows.Forms.ToolStripMenuItem();
		this.mibsBsFrontTgnt = new System.Windows.Forms.ToolStripMenuItem();
		this.mibsBsTailTgnt = new System.Windows.Forms.ToolStripMenuItem();
		this.miChmNoiseDrift = new System.Windows.Forms.ToolStripMenuItem();
		this.mindNoise = new System.Windows.Forms.ToolStripMenuItem();
		this.mindDrift = new System.Windows.Forms.ToolStripMenuItem();
		this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
		this.miChmCreateLabel = new System.Windows.Forms.ToolStripMenuItem();
		this.miclText = new System.Windows.Forms.ToolStripMenuItem();
		this.miclLine = new System.Windows.Forms.ToolStripMenuItem();
		this.miChmRemoveLabels = new System.Windows.Forms.ToolStripMenuItem();
		this.mirlSelected = new System.Windows.Forms.ToolStripMenuItem();
		this.mirlActiveChrom = new System.Windows.Forms.ToolStripMenuItem();
		this.toolStripSeparator8 = new System.Windows.Forms.ToolStripSeparator();
		this.mirlAllChroms = new System.Windows.Forms.ToolStripMenuItem();
		this.miMethod = new System.Windows.Forms.ToolStripMenuItem();
		this.miMtdCaculation = new System.Windows.Forms.ToolStripMenuItem();
		this.miMtdIntegration = new System.Windows.Forms.ToolStripMenuItem();
		this.miMtdMeasurement = new System.Windows.Forms.ToolStripMenuItem();
		this.toolStripSeparator10 = new System.Windows.Forms.ToolStripSeparator();
		this.miMtdTplt = new System.Windows.Forms.ToolStripMenuItem();
		this.miMtdSaveTplt = new System.Windows.Forms.ToolStripMenuItem();
		this.miResults = new System.Windows.Forms.ToolStripMenuItem();
		this.miRltResult = new System.Windows.Forms.ToolStripMenuItem();
		this.miRltSummary = new System.Windows.Forms.ToolStripMenuItem();
		this.miRltPerformance = new System.Windows.Forms.ToolStripMenuItem();
		this.toolStripSeparator11 = new System.Windows.Forms.ToolStripSeparator();
		this.miRltSmyOpt = new System.Windows.Forms.ToolStripMenuItem();
		this.miSST = new System.Windows.Forms.ToolStripMenuItem();
		this.miSstNew = new System.Windows.Forms.ToolStripMenuItem();
		this.miSstOpen = new System.Windows.Forms.ToolStripMenuItem();
		this.miSstSave = new System.Windows.Forms.ToolStripMenuItem();
		this.miSstSaveas = new System.Windows.Forms.ToolStripMenuItem();
		this.miSstUpdateFromCalib = new System.Windows.Forms.ToolStripMenuItem();
		this.toolStripSeparator13 = new System.Windows.Forms.ToolStripSeparator();
		this.miSstSet = new System.Windows.Forms.ToolStripMenuItem();
		this.toolStripSeparator12 = new System.Windows.Forms.ToolStripSeparator();
		this.miSstClearParas = new System.Windows.Forms.ToolStripMenuItem();
		this.flpChrom = new System.Windows.Forms.FlowLayoutPanel();
		this.toolStrip1 = new System.Windows.Forms.ToolStrip();
		this.btnOpen = new System.Windows.Forms.ToolStripButton();
		this.btnSave = new System.Windows.Forms.ToolStripButton();
		this.btnClose = new System.Windows.Forms.ToolStripButton();
		this.toolStripSeparator16 = new System.Windows.Forms.ToolStripSeparator();
		this.btnReportSetup = new System.Windows.Forms.ToolStripButton();
		this.btnPrtLink = new System.Windows.Forms.ToolStripButton();
		this.btnPreview = new System.Windows.Forms.ToolStripButton();
		this.btnPrint = new System.Windows.Forms.ToolStripButton();
		this.toolStripSeparator17 = new System.Windows.Forms.ToolStripSeparator();
		this.btnPreviousZoom = new System.Windows.Forms.ToolStripButton();
		this.btnNextZoom = new System.Windows.Forms.ToolStripButton();
		this.btnUnzoom = new System.Windows.Forms.ToolStripButton();
		this.toolStripSeparator18 = new System.Windows.Forms.ToolStripSeparator();
		this.btnProperties = new System.Windows.Forms.ToolStripButton();
		this.toolStripSeparator19 = new System.Windows.Forms.ToolStripSeparator();
		this.btnOverlayMode = new System.Windows.Forms.ToolStripButton();
		this.spSignals = new IBrainChrom2018.Lcl_SignalPanel();
		this.toolStrip2 = new System.Windows.Forms.ToolStrip();
		this.btnExpress = new System.Windows.Forms.ToolStripButton();
		this.toolStripSeparator20 = new System.Windows.Forms.ToolStripSeparator();
		this.btngblPeakWidth = new System.Windows.Forms.ToolStripButton();
		this.btngblThreshold = new System.Windows.Forms.ToolStripButton();
		this.btngblPkSlope = new System.Windows.Forms.ToolStripButton();
		this.toolStripSeparator21 = new System.Windows.Forms.ToolStripSeparator();
		this.btnipResetDtecNeg = new System.Windows.Forms.ToolStripButton();
		this.btnipClampNeg = new System.Windows.Forms.ToolStripButton();
		this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
		this.btnipPkWidth = new System.Windows.Forms.ToolStripButton();
		this.btnipPkThreshold = new System.Windows.Forms.ToolStripButton();
		this.btnipPkAddPosi = new System.Windows.Forms.ToolStripButton();
		this.btnipPkAddNeg = new System.Windows.Forms.ToolStripButton();
		this.btnipPkCut = new System.Windows.Forms.ToolStripButton();
		this.btnipPkHalfWidth = new System.Windows.Forms.ToolStripButton();
		this.btnipPkArea = new System.Windows.Forms.ToolStripButton();
		this.toolStripSeparator23 = new System.Windows.Forms.ToolStripSeparator();
		this.btnipPkVale = new System.Windows.Forms.ToolStripButton();
		this.toolStripSeparator28 = new System.Windows.Forms.ToolStripSeparator();
		this.btnipSolventPeak = new System.Windows.Forms.ToolStripButton();
		this.btnipFlowMarker = new System.Windows.Forms.ToolStripButton();
		this.btnipGroups = new System.Windows.Forms.ToolStripButton();
		this.toolStripSeparator22 = new System.Windows.Forms.ToolStripSeparator();
		this.btnbsBsTgnt = new System.Windows.Forms.ToolStripButton();
		this.btnbsBsVtV = new System.Windows.Forms.ToolStripButton();
		this.toolStripSeparator14 = new System.Windows.Forms.ToolStripSeparator();
		this.btnbsBsValley = new System.Windows.Forms.ToolStripButton();
		this.btnbsBsTogether = new System.Windows.Forms.ToolStripButton();
		this.btnbsBsForwHorz = new System.Windows.Forms.ToolStripButton();
		this.btnbsBsBackHorz = new System.Windows.Forms.ToolStripButton();
		this.btnbsBsFrontTgnt = new System.Windows.Forms.ToolStripButton();
		this.btnbsBsTailTgnt = new System.Windows.Forms.ToolStripButton();
		this.toolStripSeparator31 = new System.Windows.Forms.ToolStripSeparator();
		this.btngblDtecDelay = new System.Windows.Forms.ToolStripButton();
		this.tsDatAcq = new System.Windows.Forms.ToolStrip();
		this.lbTime = new System.Windows.Forms.ToolStripLabel();
		this.tbTime = new System.Windows.Forms.ToolStripTextBox();
		this.lbTimeU = new System.Windows.Forms.ToolStripLabel();
		this.lbSignal = new System.Windows.Forms.ToolStripLabel();
		this.tbSigYBeg = new System.Windows.Forms.ToolStripTextBox();
		this.lbSignalU = new System.Windows.Forms.ToolStripLabel();
		this.tbSigYEnd = new System.Windows.Forms.ToolStripTextBox();
		this.lbyUnit = new System.Windows.Forms.ToolStripLabel();
		this.ssChrom = new System.Windows.Forms.StatusStrip();
		this.slbExplain = new System.Windows.Forms.ToolStripStatusLabel();
		this.dpgnlChrom = new IBrainChrom2018.LclDisplayPanel();
		this.lbExpress = new IBrainChrom2018.LclLabel();
		this.tcGPC = new IBrainChrom2018.LclTabControl();
		this.tpgpcChrom = new System.Windows.Forms.TabPage();
		this.dpgpcChrom = new IBrainChrom2018.LclDisplayPanel();
		this.tpgpcMwDistrib = new System.Windows.Forms.TabPage();
		this.dpgpcMwDistrib = new IBrainChrom2018.LclDisplayPanel();
		this.tpgpcCmlMw = new System.Windows.Forms.TabPage();
		this.dpgpcCmlMw = new IBrainChrom2018.LclDisplayPanel();
		this.tcChrom = new IBrainChrom2018.LclTabControl();
		this.tpResults = new System.Windows.Forms.TabPage();
		this.pnlgcu = new IBrainChrom2018.LclPanel();
		this.lbgcuAlpha = new IBrainChrom2018.LclLabel();
		this.lbgcuK = new IBrainChrom2018.LclLabel();
		this.btngcuKAlpha = new IBrainChrom2018.LclButton();
		this.tbgcuAlpha = new IBrainChrom2018.LclTextBox();
		this.tbgcuK = new IBrainChrom2018.LclTextBox();
		this.gvRltsDad = new IBrainChrom2018.LclGridView();
		this.cmsRltGV = new System.Windows.Forms.ContextMenuStrip(this.components);
		this.mirltsColumnsSetup = new System.Windows.Forms.ToolStripMenuItem();
		this.mirltsRestoreDftColumns = new System.Windows.Forms.ToolStripMenuItem();
		this.toolStripSeparator33 = new System.Windows.Forms.ToolStripSeparator();
		this.mirltsResetCmpdNames = new System.Windows.Forms.ToolStripMenuItem();
		this.gvRltsGpc = new IBrainChrom2018.LclGridView();
		this.gvRltsGnl = new IBrainChrom2018.LclGridView();
		this.lbRltExpress = new IBrainChrom2018.LclExpressLabel();
		this.pnlRltsControl = new IBrainChrom2018.LclPanel();
		this.pnlcu = new IBrainChrom2018.LclPanel();
		this.gbcuUncalPeaks = new IBrainChrom2018.LclGroupBox();
		this.cbcuUncalBase = new IBrainChrom2018.LclCusComboBox();
		this.lbcuUncalAmtRespFU = new IBrainChrom2018.LclLabel();
		this.lbcuUncalAmtRespF = new IBrainChrom2018.LclLabel();
		this.lbcuUncalBase = new IBrainChrom2018.LclLabel();
		this.tbcuUncalAmtRespF = new IBrainChrom2018.LclTextBox();
		this.gbCalibration = new IBrainChrom2018.LclGroupBox();
		this.btnclbView = new IBrainChrom2018.LclButton();
		this.btnclbNone = new IBrainChrom2018.LclButton();
		this.btnclbSet = new IBrainChrom2018.LclButton();
		this.tbclb = new IBrainChrom2018.LclTextBox();
		this.cbcuCalcu = new IBrainChrom2018.LclCusComboBox();
		this.lbcuDilution = new IBrainChrom2018.LclLabel();
		this.cbRltCombine = new IBrainChrom2018.LclCheckBox();
		this.lbcuInjVolume = new IBrainChrom2018.LclLabel();
		this.tbcuDilution = new IBrainChrom2018.LclTextBox();
		this.tbcuInjVolume = new IBrainChrom2018.LclTextBox();
		this.tbcuIstdAmount = new IBrainChrom2018.LclTextBox();
		this.lbcuIstdAmount = new IBrainChrom2018.LclLabel();
		this.tbcuAmount = new IBrainChrom2018.LclTextBox();
		this.lbcuAmount = new IBrainChrom2018.LclLabel();
		this.gbcuScale = new IBrainChrom2018.LclGroupBox();
		this.cbcuUseScaleFactor = new IBrainChrom2018.LclCheckBox();
		this.lbcuUnitAfterScale = new IBrainChrom2018.LclLabel();
		this.lbcuScaleFactor = new IBrainChrom2018.LclLabel();
		this.tbcuUnitAfterScale = new IBrainChrom2018.LclTextBox();
		this.tbcuScaleFactor = new IBrainChrom2018.LclTextBox();
		this.gbcuRltTableReport = new IBrainChrom2018.LclGroupBox();
		this.rbcuCaliPeaks = new IBrainChrom2018.LclRadioButton();
		this.rbcuIdentifiedPeaks = new IBrainChrom2018.LclRadioButton();
		this.rbcuAllDetectedPeaks = new IBrainChrom2018.LclRadioButton();
		this.cbcuHideISTDPeak = new IBrainChrom2018.LclCheckBox();
		this.lbcuCalcu = new IBrainChrom2018.LclLabel();
		this.tpSummary = new System.Windows.Forms.TabPage();
		this.gvSummary = new IBrainChrom2018.LclSummaryGridView();
		this.cmsSummary = new System.Windows.Forms.ContextMenuStrip(this.components);
		this.mismyColumnsSetup = new System.Windows.Forms.ToolStripMenuItem();
		this.mismyRestoreDftColumns = new System.Windows.Forms.ToolStripMenuItem();
		this.toolStripSeparator15 = new System.Windows.Forms.ToolStripSeparator();
		this.mismySmyOpt = new System.Windows.Forms.ToolStripMenuItem();
		this.tpPerformance = new System.Windows.Forms.TabPage();
		this.gvPerformFrom50 = new IBrainChrom2018.LclGridView();
		this.cmsPerformance = new System.Windows.Forms.ContextMenuStrip(this.components);
		this.mipfmColumnsSetup = new System.Windows.Forms.ToolStripMenuItem();
		this.mipfmRestoreDftColumns = new System.Windows.Forms.ToolStripMenuItem();
		this.gvPerformStatic = new IBrainChrom2018.LclGridView();
		this.pnlpfmControl = new IBrainChrom2018.LclPanel();
		this.gbpfmColumnCalcu = new IBrainChrom2018.LclGroupBox();
		this.rbpfmFrom50per = new IBrainChrom2018.LclRadioButton();
		this.rbpfmStatistical = new IBrainChrom2018.LclRadioButton();
		this.tbpfmColumnLength = new IBrainChrom2018.LclTextBox();
		this.lbpfmColumnLengthU = new IBrainChrom2018.LclLabel();
		this.tbpfmColumnUT = new IBrainChrom2018.LclTextBox();
		this.lbpfmColumnLength = new IBrainChrom2018.LclLabel();
		this.lbpfmUnretainedPeakU = new IBrainChrom2018.LclLabel();
		this.lbpfmColumnUT = new IBrainChrom2018.LclLabel();
		this.tpIntegration = new System.Windows.Forms.TabPage();
		this.gvInteg = new IBrainChrom2018.LclIntegGridView();
		this.cmsInteg = new System.Windows.Forms.ContextMenuStrip(this.components);
		this.miitgUndo = new System.Windows.Forms.ToolStripMenuItem();
		this.miitgRedo = new System.Windows.Forms.ToolStripMenuItem();
		this.toolStripSeparator30 = new System.Windows.Forms.ToolStripSeparator();
		this.miitgAppendRow = new System.Windows.Forms.ToolStripMenuItem();
		this.miitgInsertRow = new System.Windows.Forms.ToolStripMenuItem();
		this.miitgDelete = new System.Windows.Forms.ToolStripMenuItem();
		this.miitgReset = new System.Windows.Forms.ToolStripMenuItem();
		this.toolStripSeparator29 = new System.Windows.Forms.ToolStripSeparator();
		this.miitgCopy = new System.Windows.Forms.ToolStripMenuItem();
		this.miitgPaste = new System.Windows.Forms.ToolStripMenuItem();
		this.tpMsmCdts = new System.Windows.Forms.TabPage();
		this.tcMsmCdts = new IBrainChrom2018.LclTabControl();
		this.tpInstrument = new System.Windows.Forms.TabPage();
		this.gbinsAddSub = new IBrainChrom2018.LclGroupBox();
		this.rbasSub = new IBrainChrom2018.LclRadioButton();
		this.rbasAdd = new IBrainChrom2018.LclRadioButton();
		this.cbasMatching = new IBrainChrom2018.LclCusComboBox();
		this.btnasSetChrom = new IBrainChrom2018.LclButton();
		this.btnasNoneChrom = new IBrainChrom2018.LclButton();
		this.lbasMatching = new IBrainChrom2018.LclLabel();
		this.tbasChrom = new IBrainChrom2018.LclTextBox();
		this.lbasChrom = new IBrainChrom2018.LclLabel();
		this.gbinsCdts = new IBrainChrom2018.LclGroupBox();
		this.tbmsmNote = new IBrainChrom2018.LclTextBox();
		this.lbmsmNote = new IBrainChrom2018.LclLabel();
		this.tbmsmTemperature = new IBrainChrom2018.LclTextBox();
		this.lbmsmTemperature = new IBrainChrom2018.LclLabel();
		this.tbmsmDetection = new IBrainChrom2018.LclTextBox();
		this.lbmsmDetection = new IBrainChrom2018.LclLabel();
		this.tbmsmPressure = new IBrainChrom2018.LclTextBox();
		this.lbmsmPressure = new IBrainChrom2018.LclLabel();
		this.tbmsmFlowRate = new IBrainChrom2018.LclTextBox();
		this.lbmsmFlowRate = new IBrainChrom2018.LclLabel();
		this.tbmsmMobilePhase = new IBrainChrom2018.LclTextBox();
		this.lbmsmMobilePhase = new IBrainChrom2018.LclLabel();
		this.tbmsmColumn = new IBrainChrom2018.LclTextBox();
		this.lbmsmColumn = new IBrainChrom2018.LclLabel();
		this.tbmsmMtdDspt = new IBrainChrom2018.LclTextBox();
		this.lbmsmMtdDspt = new IBrainChrom2018.LclLabel();
		this.gbinsSampleIdt = new IBrainChrom2018.LclGroupBox();
		this.tbsiSample = new IBrainChrom2018.LclTextBox();
		this.lbsiAcquiredTimeV = new IBrainChrom2018.LclLabel();
		this.lbsiAcquiredTime = new IBrainChrom2018.LclLabel();
		this.lbsiAnalystV = new IBrainChrom2018.LclLabel();
		this.lbsiAnalyst = new IBrainChrom2018.LclLabel();
		this.lbsiSample = new IBrainChrom2018.LclLabel();
		this.tbsiSampleID = new IBrainChrom2018.LclTextBox();
		this.lbsiSampleID = new IBrainChrom2018.LclLabel();
		this.gbinsAcqParas = new IBrainChrom2018.LclGroupBox();
		this.pbapES = new IBrainChrom2018.LclPictureBox();
		this.lbapMethodV = new IBrainChrom2018.LclLabel();
		this.lbapMethod = new IBrainChrom2018.LclLabel();
		this.lbapSamplingV = new IBrainChrom2018.LclLabel();
		this.lbapSampling = new IBrainChrom2018.LclLabel();
		this.lbapExtStartV = new IBrainChrom2018.LclLabel();
		this.lbapExtStart = new IBrainChrom2018.LclLabel();
		this.lbapRangeV = new IBrainChrom2018.LclLabel();
		this.lbapAutoStopV = new IBrainChrom2018.LclLabel();
		this.lbapRange = new IBrainChrom2018.LclLabel();
		this.lbapAutoStop = new IBrainChrom2018.LclLabel();
		this.tpPDAMethod = new System.Windows.Forms.TabPage();
		this.gbpdaLibs = new IBrainChrom2018.LclGroupBox();
		this.gvlibPDA = new IBrainChrom2018.LclGridView();
		this.cmsLibs = new System.Windows.Forms.ContextMenuStrip(this.components);
		this.miAddRow = new System.Windows.Forms.ToolStripMenuItem();
		this.miDeleteRow = new System.Windows.Forms.ToolStripMenuItem();
		this.gbpdaLibSearchOptions = new IBrainChrom2018.LclGroupBox();
		this.cblsoMatchCriteria = new IBrainChrom2018.LclCusComboBox();
		this.tblsoRestrictRT = new IBrainChrom2018.LclTextBox();
		this.tblsoMaxNumHits = new IBrainChrom2018.LclTextBox();
		this.tblsoMatchFactorThreshold = new IBrainChrom2018.LclTextBox();
		this.tblsoFrom = new IBrainChrom2018.LclTextBox();
		this.lclLabel15 = new IBrainChrom2018.LclLabel();
		this.tblsoTo = new IBrainChrom2018.LclTextBox();
		this.lclLabel9 = new IBrainChrom2018.LclLabel();
		this.lclLabel10 = new IBrainChrom2018.LclLabel();
		this.lblsoMaxNumHits = new IBrainChrom2018.LclLabel();
		this.lblsoTo = new IBrainChrom2018.LclLabel();
		this.lblsoMatchCriteria = new IBrainChrom2018.LclLabel();
		this.lblsoMatchFactorThreshold = new IBrainChrom2018.LclLabel();
		this.lblsoFrom = new IBrainChrom2018.LclLabel();
		this.cblsoForAllDetectedPeaks = new IBrainChrom2018.LclCheckBox();
		this.cblsoUseBackCorr = new IBrainChrom2018.LclCheckBox();
		this.cblsoRestrictRT = new IBrainChrom2018.LclCheckBox();
		this.cblsoRestrictWaveLength = new IBrainChrom2018.LclCheckBox();
		this.gbpdaPeakPurityOptions = new IBrainChrom2018.LclGroupBox();
		this.gbppoUsedPoints = new IBrainChrom2018.LclGroupBox();
		this.rbupFive = new IBrainChrom2018.LclRadioButton();
		this.rbupAll = new IBrainChrom2018.LclRadioButton();
		this.tbppoAbsorbanceThreshold = new IBrainChrom2018.LclTextBox();
		this.tbppoPurityThreshold = new IBrainChrom2018.LclTextBox();
		this.tbppoFrom = new IBrainChrom2018.LclTextBox();
		this.tbppoTo = new IBrainChrom2018.LclTextBox();
		this.lclLabel7 = new IBrainChrom2018.LclLabel();
		this.lclLabel3 = new IBrainChrom2018.LclLabel();
		this.lclLabel5 = new IBrainChrom2018.LclLabel();
		this.lbppoAbsorbanceThreshold = new IBrainChrom2018.LclLabel();
		this.lbppoTo = new IBrainChrom2018.LclLabel();
		this.lbppoPurityThreshold = new IBrainChrom2018.LclLabel();
		this.lbppoFrom = new IBrainChrom2018.LclLabel();
		this.cbppoUseBackCorr = new IBrainChrom2018.LclCheckBox();
		this.cbppoRestrictWaveLength = new IBrainChrom2018.LclCheckBox();
		this.tpLC = new System.Windows.Forms.TabPage();
		this.gvlcGradient = new IBrainChrom2018.LclGridView();
		this.tpGC = new System.Windows.Forms.TabPage();
		this.gvgcProgTemp = new IBrainChrom2018.LclGridView();
		this.dgvCT6 = new System.Windows.Forms.DataGridView();
		this.clmCT6CN = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.clmCT6SetT = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.tpSST = new System.Windows.Forms.TabPage();
		this.gvSSTResults = new IBrainChrom2018.LclSSTGridView();
		this.cmsSSTCmpds = new System.Windows.Forms.ContextMenuStrip(this.components);
		this.misstcNew = new System.Windows.Forms.ToolStripMenuItem();
		this.misstcOpen = new System.Windows.Forms.ToolStripMenuItem();
		this.misstcSave = new System.Windows.Forms.ToolStripMenuItem();
		this.misstcSaveas = new System.Windows.Forms.ToolStripMenuItem();
		this.misstcUpdateFromCalib = new System.Windows.Forms.ToolStripMenuItem();
		this.toolStripSeparator34 = new System.Windows.Forms.ToolStripSeparator();
		this.misstcSet = new System.Windows.Forms.ToolStripMenuItem();
		this.tss1 = new System.Windows.Forms.ToolStripSeparator();
		this.misstColumnsSetup = new System.Windows.Forms.ToolStripMenuItem();
		this.misstRestoreDftColumns = new System.Windows.Forms.ToolStripMenuItem();
		this.tss2 = new System.Windows.Forms.ToolStripSeparator();
		this.misstcClearParas = new System.Windows.Forms.ToolStripMenuItem();
		this.lbSSTExpress = new IBrainChrom2018.LclExpressLabel();
		this.pnlControl = new IBrainChrom2018.LclPanel();
		this.gvSSTCmpds = new IBrainChrom2018.LclGridView();
		this.lbSSTFile = new IBrainChrom2018.LclExpressLabel();
		this.tpSlices = new System.Windows.Forms.TabPage();
		this.gvSlices = new IBrainChrom2018.LclGridView();
		this.cmsSlices = new System.Windows.Forms.ContextMenuStrip(this.components);
		this.mislcColumnsSetup = new System.Windows.Forms.ToolStripMenuItem();
		this.mislcRestoreDftColumns = new System.Windows.Forms.ToolStripMenuItem();
		this.tbslcAverNum = new IBrainChrom2018.LclTextBox();
		this.lbslcAverNum = new IBrainChrom2018.LclLabel();
		this.lclExpressLabel5 = new IBrainChrom2018.LclExpressLabel();
		this.tpRanges = new System.Windows.Forms.TabPage();
		this.gvgrMw = new IBrainChrom2018.LclGridView();
		this.gvgrPercent = new IBrainChrom2018.LclGridView();
		this.lbgrMw = new IBrainChrom2018.LclLabel();
		this.lbgrPercent = new IBrainChrom2018.LclLabel();
		this.tpRightsArchives = new System.Windows.Forms.TabPage();
		this.spltcArchives = new IBrainChrom2018.LclSplitContainer();
		this.gvArchives = new IBrainChrom2018.LclGridView();
		this.lclLabel6 = new IBrainChrom2018.LclLabel();
		this.tbrtaRemark = new IBrainChrom2018.LclTextBox();
		this.lclPanel1 = new IBrainChrom2018.LclPanel();
		this.lclLabel4 = new IBrainChrom2018.LclLabel();
		this.pnlSetRights = new IBrainChrom2018.LclPanel();
		this.gvSetRights = new IBrainChrom2018.LclGridView();
		this.lclPanel2 = new IBrainChrom2018.LclPanel();
		this.lclLabel2 = new IBrainChrom2018.LclLabel();
		this.cbrtaCanSetRs = new IBrainChrom2018.LclCheckBox();
		this.splitContainer = new IBrainChrom2018.LclSplitContainer();
		this.msChrom.SuspendLayout();
		this.flpChrom.SuspendLayout();
		this.toolStrip1.SuspendLayout();
		this.toolStrip2.SuspendLayout();
		this.tsDatAcq.SuspendLayout();
		this.ssChrom.SuspendLayout();
		this.tcGPC.SuspendLayout();
		this.tpgpcChrom.SuspendLayout();
		this.tpgpcMwDistrib.SuspendLayout();
		this.tpgpcCmlMw.SuspendLayout();
		this.tcChrom.SuspendLayout();
		this.tpResults.SuspendLayout();
		this.pnlgcu.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.gvRltsDad).BeginInit();
		this.cmsRltGV.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.gvRltsGpc).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.gvRltsGnl).BeginInit();
		this.pnlRltsControl.SuspendLayout();
		this.pnlcu.SuspendLayout();
		this.gbcuUncalPeaks.SuspendLayout();
		this.gbCalibration.SuspendLayout();
		this.gbcuScale.SuspendLayout();
		this.gbcuRltTableReport.SuspendLayout();
		this.tpSummary.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.gvSummary).BeginInit();
		this.cmsSummary.SuspendLayout();
		this.tpPerformance.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.gvPerformFrom50).BeginInit();
		this.cmsPerformance.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.gvPerformStatic).BeginInit();
		this.pnlpfmControl.SuspendLayout();
		this.gbpfmColumnCalcu.SuspendLayout();
		this.tpIntegration.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.gvInteg).BeginInit();
		this.cmsInteg.SuspendLayout();
		this.tpMsmCdts.SuspendLayout();
		this.tcMsmCdts.SuspendLayout();
		this.tpInstrument.SuspendLayout();
		this.gbinsAddSub.SuspendLayout();
		this.gbinsCdts.SuspendLayout();
		this.gbinsSampleIdt.SuspendLayout();
		this.gbinsAcqParas.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.pbapES).BeginInit();
		this.tpPDAMethod.SuspendLayout();
		this.gbpdaLibs.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.gvlibPDA).BeginInit();
		this.cmsLibs.SuspendLayout();
		this.gbpdaLibSearchOptions.SuspendLayout();
		this.gbpdaPeakPurityOptions.SuspendLayout();
		this.gbppoUsedPoints.SuspendLayout();
		this.tpLC.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.gvlcGradient).BeginInit();
		this.tpGC.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.gvgcProgTemp).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.dgvCT6).BeginInit();
		this.tpSST.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.gvSSTResults).BeginInit();
		this.cmsSSTCmpds.SuspendLayout();
		this.pnlControl.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.gvSSTCmpds).BeginInit();
		this.tpSlices.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.gvSlices).BeginInit();
		this.cmsSlices.SuspendLayout();
		this.tpRanges.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.gvgrMw).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.gvgrPercent).BeginInit();
		this.tpRightsArchives.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.spltcArchives).BeginInit();
		this.spltcArchives.Panel1.SuspendLayout();
		this.spltcArchives.Panel2.SuspendLayout();
		this.spltcArchives.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.gvArchives).BeginInit();
		this.lclPanel1.SuspendLayout();
		this.pnlSetRights.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.gvSetRights).BeginInit();
		this.lclPanel2.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.splitContainer).BeginInit();
		this.splitContainer.Panel1.SuspendLayout();
		this.splitContainer.Panel2.SuspendLayout();
		this.splitContainer.SuspendLayout();
		base.SuspendLayout();
		this.msChrom.Items.AddRange(new System.Windows.Forms.ToolStripItem[6] { this.miFile, this.miDisplay, this.miChromatogram, this.miMethod, this.miResults, this.miSST });
		this.msChrom.Location = new System.Drawing.Point(0, 0);
		this.msChrom.Name = "msChrom";
		this.msChrom.Size = new System.Drawing.Size(1047, 25);
		this.msChrom.TabIndex = 0;
		this.msChrom.Text = "menuStrip1";
		this.miFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[15]
		{
			this.miFiOverlayMode, this.toolStripSeparator1, this.miFiOpen, this.miFiClose, this.miFiCloseAll, this.miFiSave, this.miFiSaveAs, this.toolStripSeparator2, this.miFiImportChrom, this.toolStripSeparator3,
			this.miFiReportSetup, this.miFiPreview, this.miFiPrint, this.toolStripSeparator4, this.miFiExit
		});
		this.miFile.Name = "miFile";
		this.miFile.Size = new System.Drawing.Size(44, 21);
		this.miFile.Text = "文件";
		this.miFiOverlayMode.Name = "miFiOverlayMode";
		this.miFiOverlayMode.Size = new System.Drawing.Size(133, 22);
		this.miFiOverlayMode.Text = "重叠模式";
		this.miFiOverlayMode.Click += new System.EventHandler(btnOverlayMode_Click);
		this.toolStripSeparator1.Name = "toolStripSeparator1";
		this.toolStripSeparator1.Size = new System.Drawing.Size(130, 6);
		this.miFiOpen.Name = "miFiOpen";
		this.miFiOpen.Size = new System.Drawing.Size(133, 22);
		this.miFiOpen.Text = "打开...";
		this.miFiOpen.Click += new System.EventHandler(btnOpen_Click);
		this.miFiClose.Name = "miFiClose";
		this.miFiClose.Size = new System.Drawing.Size(133, 22);
		this.miFiClose.Text = "关闭";
		this.miFiClose.Click += new System.EventHandler(btnClose_Click);
		this.miFiCloseAll.Name = "miFiCloseAll";
		this.miFiCloseAll.Size = new System.Drawing.Size(133, 22);
		this.miFiCloseAll.Text = "关闭全部";
		this.miFiCloseAll.Click += new System.EventHandler(miFiCloseAll_Click);
		this.miFiSave.Name = "miFiSave";
		this.miFiSave.Size = new System.Drawing.Size(133, 22);
		this.miFiSave.Text = "保存";
		this.miFiSave.Click += new System.EventHandler(btnSave_Click);
		this.miFiSaveAs.Name = "miFiSaveAs";
		this.miFiSaveAs.Size = new System.Drawing.Size(133, 22);
		this.miFiSaveAs.Text = "另存...";
		this.miFiSaveAs.Click += new System.EventHandler(miFiSaveAs_Click);
		this.toolStripSeparator2.Name = "toolStripSeparator2";
		this.toolStripSeparator2.Size = new System.Drawing.Size(130, 6);
		this.toolStripSeparator2.Visible = false;
		this.miFiImportChrom.Name = "miFiImportChrom";
		this.miFiImportChrom.Size = new System.Drawing.Size(133, 22);
		this.miFiImportChrom.Text = "导入谱图";
		this.miFiImportChrom.Visible = false;
		this.miFiImportChrom.Click += new System.EventHandler(miFiImportChrom_Click);
		this.toolStripSeparator3.Name = "toolStripSeparator3";
		this.toolStripSeparator3.Size = new System.Drawing.Size(130, 6);
		this.miFiReportSetup.Name = "miFiReportSetup";
		this.miFiReportSetup.Size = new System.Drawing.Size(133, 22);
		this.miFiReportSetup.Text = "样式文件...";
		this.miFiReportSetup.Click += new System.EventHandler(btnReportSetup_Click);
		this.miFiPreview.Name = "miFiPreview";
		this.miFiPreview.Size = new System.Drawing.Size(133, 22);
		this.miFiPreview.Text = "预览";
		this.miFiPreview.Click += new System.EventHandler(btnPreview_Click);
		this.miFiPrint.Name = "miFiPrint";
		this.miFiPrint.Size = new System.Drawing.Size(133, 22);
		this.miFiPrint.Text = "打印";
		this.miFiPrint.Click += new System.EventHandler(btnPrint_Click);
		this.toolStripSeparator4.Name = "toolStripSeparator4";
		this.toolStripSeparator4.Size = new System.Drawing.Size(130, 6);
		this.miFiExit.Name = "miFiExit";
		this.miFiExit.Size = new System.Drawing.Size(133, 22);
		this.miFiExit.Text = "退出";
		this.miDisplay.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[5] { this.miDisPreviousZoom, this.miDisNextZoom, this.miDisUnzoom, this.toolStripSeparator6, this.miDisProperties });
		this.miDisplay.Name = "miDisplay";
		this.miDisplay.Size = new System.Drawing.Size(44, 21);
		this.miDisplay.Text = "显示";
		this.miDisPreviousZoom.Name = "miDisPreviousZoom";
		this.miDisPreviousZoom.Size = new System.Drawing.Size(109, 22);
		this.miDisPreviousZoom.Text = "后退";
		this.miDisPreviousZoom.Click += new System.EventHandler(dpgpcChrom_DoubleClick);
		this.miDisNextZoom.Name = "miDisNextZoom";
		this.miDisNextZoom.Size = new System.Drawing.Size(109, 22);
		this.miDisNextZoom.Text = "前进";
		this.miDisNextZoom.Click += new System.EventHandler(btnNextZoom_Click);
		this.miDisUnzoom.Name = "miDisUnzoom";
		this.miDisUnzoom.Size = new System.Drawing.Size(109, 22);
		this.miDisUnzoom.Text = "复位";
		this.miDisUnzoom.Click += new System.EventHandler(btnUnzoom_Click);
		this.toolStripSeparator6.Name = "toolStripSeparator6";
		this.toolStripSeparator6.Size = new System.Drawing.Size(106, 6);
		this.miDisProperties.Name = "miDisProperties";
		this.miDisProperties.Size = new System.Drawing.Size(109, 22);
		this.miDisProperties.Text = "属性...";
		this.miDisProperties.Click += new System.EventHandler(btnProperties_Click);
		this.miChromatogram.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[7] { this.miChmGlobal, this.miChmItgPeak, this.miChmBaseline, this.miChmNoiseDrift, this.toolStripSeparator7, this.miChmCreateLabel, this.miChmRemoveLabels });
		this.miChromatogram.Name = "miChromatogram";
		this.miChromatogram.Size = new System.Drawing.Size(44, 21);
		this.miChromatogram.Text = "谱图";
		this.miChmGlobal.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[5] { this.migblPeakWidth, this.migblThreshold, this.migblPkSlope, this.toolStripSeparator32, this.migblDtecDelay });
		this.miChmGlobal.Name = "miChmGlobal";
		this.miChmGlobal.Size = new System.Drawing.Size(124, 22);
		this.miChmGlobal.Text = "全局参数";
		this.migblPeakWidth.Name = "migblPeakWidth";
		this.migblPeakWidth.Size = new System.Drawing.Size(136, 22);
		this.migblPeakWidth.Text = "峰宽参数";
		this.migblPeakWidth.Click += new System.EventHandler(btngblDtecDelay_Click);
		this.migblThreshold.Name = "migblThreshold";
		this.migblThreshold.Size = new System.Drawing.Size(136, 22);
		this.migblThreshold.Text = "峰高参数";
		this.migblThreshold.Click += new System.EventHandler(btngblDtecDelay_Click);
		this.migblPkSlope.Name = "migblPkSlope";
		this.migblPkSlope.Size = new System.Drawing.Size(136, 22);
		this.migblPkSlope.Text = "峰斜率";
		this.migblPkSlope.Visible = false;
		this.migblPkSlope.Click += new System.EventHandler(btngblDtecDelay_Click);
		this.toolStripSeparator32.Name = "toolStripSeparator32";
		this.toolStripSeparator32.Size = new System.Drawing.Size(133, 6);
		this.migblDtecDelay.Name = "migblDtecDelay";
		this.migblDtecDelay.Size = new System.Drawing.Size(136, 22);
		this.migblDtecDelay.Text = "检测器延迟";
		this.migblDtecDelay.Click += new System.EventHandler(btngblDtecDelay_Click);
		this.miChmItgPeak.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[16]
		{
			this.miipResetDtecNeg, this.miipClampNeg, this.toolStripSeparator25, this.miipPkWidth, this.miipPkThreshold, this.miipPkAddPosi, this.miipPkAddNeg, this.miipPkCut, this.miipPkHalfWidth, this.miipPkArea,
			this.toolStripSeparator27, this.miipPkVale, this.toolStripSeparator26, this.miipSolventPeak, this.miipFlowMarker, this.miipGroups
		});
		this.miChmItgPeak.Name = "miChmItgPeak";
		this.miChmItgPeak.Size = new System.Drawing.Size(124, 22);
		this.miChmItgPeak.Text = "峰";
		this.miipResetDtecNeg.Name = "miipResetDtecNeg";
		this.miipResetDtecNeg.Size = new System.Drawing.Size(151, 22);
		this.miipResetDtecNeg.Text = "重置.检测负峰";
		this.miipResetDtecNeg.Click += new System.EventHandler(btngblDtecDelay_Click);
		this.miipClampNeg.Name = "miipClampNeg";
		this.miipClampNeg.Size = new System.Drawing.Size(151, 22);
		this.miipClampNeg.Text = "翻转负峰";
		this.miipClampNeg.Click += new System.EventHandler(btngblDtecDelay_Click);
		this.toolStripSeparator25.Name = "toolStripSeparator25";
		this.toolStripSeparator25.Size = new System.Drawing.Size(148, 6);
		this.miipPkWidth.Name = "miipPkWidth";
		this.miipPkWidth.Size = new System.Drawing.Size(151, 22);
		this.miipPkWidth.Text = "最小峰宽";
		this.miipPkWidth.Click += new System.EventHandler(btngblDtecDelay_Click);
		this.miipPkThreshold.Name = "miipPkThreshold";
		this.miipPkThreshold.Size = new System.Drawing.Size(151, 22);
		this.miipPkThreshold.Text = "最小峰高";
		this.miipPkThreshold.Click += new System.EventHandler(btngblDtecDelay_Click);
		this.miipPkAddPosi.Name = "miipPkAddPosi";
		this.miipPkAddPosi.Size = new System.Drawing.Size(151, 22);
		this.miipPkAddPosi.Text = "添加正峰";
		this.miipPkAddPosi.Click += new System.EventHandler(btngblDtecDelay_Click);
		this.miipPkAddNeg.Name = "miipPkAddNeg";
		this.miipPkAddNeg.Size = new System.Drawing.Size(151, 22);
		this.miipPkAddNeg.Text = "添加负峰";
		this.miipPkAddNeg.Click += new System.EventHandler(btngblDtecDelay_Click);
		this.miipPkCut.Name = "miipPkCut";
		this.miipPkCut.Size = new System.Drawing.Size(151, 22);
		this.miipPkCut.Text = "剔除峰";
		this.miipPkCut.Click += new System.EventHandler(btngblDtecDelay_Click);
		this.miipPkHalfWidth.Name = "miipPkHalfWidth";
		this.miipPkHalfWidth.Size = new System.Drawing.Size(151, 22);
		this.miipPkHalfWidth.Text = "最小半峰宽";
		this.miipPkHalfWidth.Click += new System.EventHandler(btngblDtecDelay_Click);
		this.miipPkArea.Name = "miipPkArea";
		this.miipPkArea.Size = new System.Drawing.Size(151, 22);
		this.miipPkArea.Text = "最小面积";
		this.miipPkArea.Click += new System.EventHandler(btngblDtecDelay_Click);
		this.toolStripSeparator27.Name = "toolStripSeparator27";
		this.toolStripSeparator27.Size = new System.Drawing.Size(148, 6);
		this.miipPkVale.Name = "miipPkVale";
		this.miipPkVale.Size = new System.Drawing.Size(151, 22);
		this.miipPkVale.Text = "谷点";
		this.miipPkVale.Click += new System.EventHandler(btngblDtecDelay_Click);
		this.toolStripSeparator26.Name = "toolStripSeparator26";
		this.toolStripSeparator26.Size = new System.Drawing.Size(148, 6);
		this.toolStripSeparator26.Visible = false;
		this.miipSolventPeak.Name = "miipSolventPeak";
		this.miipSolventPeak.Size = new System.Drawing.Size(151, 22);
		this.miipSolventPeak.Text = "溶剂峰";
		this.miipSolventPeak.Visible = false;
		this.miipSolventPeak.Click += new System.EventHandler(btngblDtecDelay_Click);
		this.miipFlowMarker.Name = "miipFlowMarker";
		this.miipFlowMarker.Size = new System.Drawing.Size(151, 22);
		this.miipFlowMarker.Text = "流速标识";
		this.miipFlowMarker.Visible = false;
		this.miipFlowMarker.Click += new System.EventHandler(btngblDtecDelay_Click);
		this.miipGroups.Name = "miipGroups";
		this.miipGroups.Size = new System.Drawing.Size(151, 22);
		this.miipGroups.Text = "组...";
		this.miipGroups.Visible = false;
		this.miipGroups.Click += new System.EventHandler(btngblDtecDelay_Click);
		this.miChmBaseline.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[9] { this.mibsBsTgnt, this.mibsBsVtV, this.toolStripSeparator9, this.mibsBsValley, this.mibsBsTogether, this.mibsBsForwHorz, this.mibsBsBackHorz, this.mibsBsFrontTgnt, this.mibsBsTailTgnt });
		this.miChmBaseline.Name = "miChmBaseline";
		this.miChmBaseline.Size = new System.Drawing.Size(124, 22);
		this.miChmBaseline.Text = "基线";
		this.mibsBsTgnt.Name = "mibsBsTgnt";
		this.mibsBsTgnt.Size = new System.Drawing.Size(127, 22);
		this.mibsBsTgnt.Text = "肩切参数";
		this.mibsBsTgnt.Click += new System.EventHandler(btngblDtecDelay_Click);
		this.mibsBsVtV.Name = "mibsBsVtV";
		this.mibsBsVtV.Size = new System.Drawing.Size(127, 22);
		this.mibsBsVtV.Text = "谷.谷斜率";
		this.mibsBsVtV.Click += new System.EventHandler(btngblDtecDelay_Click);
		this.toolStripSeparator9.Name = "toolStripSeparator9";
		this.toolStripSeparator9.Size = new System.Drawing.Size(124, 6);
		this.mibsBsValley.Name = "mibsBsValley";
		this.mibsBsValley.Size = new System.Drawing.Size(127, 22);
		this.mibsBsValley.Text = "经过谷点";
		this.mibsBsValley.Click += new System.EventHandler(btngblDtecDelay_Click);
		this.mibsBsTogether.Name = "mibsBsTogether";
		this.mibsBsTogether.Size = new System.Drawing.Size(127, 22);
		this.mibsBsTogether.Text = "整合基线";
		this.mibsBsTogether.Click += new System.EventHandler(btngblDtecDelay_Click);
		this.mibsBsForwHorz.Name = "mibsBsForwHorz";
		this.mibsBsForwHorz.Size = new System.Drawing.Size(127, 22);
		this.mibsBsForwHorz.Text = "向前水平";
		this.mibsBsForwHorz.Click += new System.EventHandler(btngblDtecDelay_Click);
		this.mibsBsBackHorz.Name = "mibsBsBackHorz";
		this.mibsBsBackHorz.Size = new System.Drawing.Size(127, 22);
		this.mibsBsBackHorz.Text = "向后水平";
		this.mibsBsBackHorz.Click += new System.EventHandler(btngblDtecDelay_Click);
		this.mibsBsFrontTgnt.Name = "mibsBsFrontTgnt";
		this.mibsBsFrontTgnt.Size = new System.Drawing.Size(127, 22);
		this.mibsBsFrontTgnt.Text = "前切";
		this.mibsBsFrontTgnt.Click += new System.EventHandler(btngblDtecDelay_Click);
		this.mibsBsTailTgnt.Name = "mibsBsTailTgnt";
		this.mibsBsTailTgnt.Size = new System.Drawing.Size(127, 22);
		this.mibsBsTailTgnt.Text = "尾切";
		this.mibsBsTailTgnt.Click += new System.EventHandler(btngblDtecDelay_Click);
		this.miChmNoiseDrift.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[2] { this.mindNoise, this.mindDrift });
		this.miChmNoiseDrift.Name = "miChmNoiseDrift";
		this.miChmNoiseDrift.Size = new System.Drawing.Size(124, 22);
		this.miChmNoiseDrift.Text = "噪声漂移";
		this.mindNoise.Name = "mindNoise";
		this.mindNoise.Size = new System.Drawing.Size(124, 22);
		this.mindNoise.Text = "噪声评估";
		this.mindNoise.Click += new System.EventHandler(btngblDtecDelay_Click);
		this.mindDrift.Name = "mindDrift";
		this.mindDrift.Size = new System.Drawing.Size(124, 22);
		this.mindDrift.Text = "漂移评估";
		this.mindDrift.Click += new System.EventHandler(btngblDtecDelay_Click);
		this.toolStripSeparator7.Name = "toolStripSeparator7";
		this.toolStripSeparator7.Size = new System.Drawing.Size(121, 6);
		this.miChmCreateLabel.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[2] { this.miclText, this.miclLine });
		this.miChmCreateLabel.Name = "miChmCreateLabel";
		this.miChmCreateLabel.Size = new System.Drawing.Size(124, 22);
		this.miChmCreateLabel.Text = "创建标识";
		this.miclText.Name = "miclText";
		this.miclText.Size = new System.Drawing.Size(109, 22);
		this.miclText.Text = "文本...";
		this.miclText.Click += new System.EventHandler(miclLine_Click);
		this.miclLine.Name = "miclLine";
		this.miclLine.Size = new System.Drawing.Size(109, 22);
		this.miclLine.Text = "直线...";
		this.miclLine.Click += new System.EventHandler(miclLine_Click);
		this.miChmRemoveLabels.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[4] { this.mirlSelected, this.mirlActiveChrom, this.toolStripSeparator8, this.mirlAllChroms });
		this.miChmRemoveLabels.Name = "miChmRemoveLabels";
		this.miChmRemoveLabels.Size = new System.Drawing.Size(124, 22);
		this.miChmRemoveLabels.Text = "移除标识";
		this.mirlSelected.Name = "mirlSelected";
		this.mirlSelected.Size = new System.Drawing.Size(124, 22);
		this.mirlSelected.Text = "选择的";
		this.mirlSelected.Visible = false;
		this.mirlSelected.Click += new System.EventHandler(mirlAllChroms_Click);
		this.mirlActiveChrom.Name = "mirlActiveChrom";
		this.mirlActiveChrom.Size = new System.Drawing.Size(124, 22);
		this.mirlActiveChrom.Text = "当前谱图";
		this.mirlActiveChrom.Click += new System.EventHandler(mirlAllChroms_Click);
		this.toolStripSeparator8.Name = "toolStripSeparator8";
		this.toolStripSeparator8.Size = new System.Drawing.Size(121, 6);
		this.mirlAllChroms.Name = "mirlAllChroms";
		this.mirlAllChroms.Size = new System.Drawing.Size(124, 22);
		this.mirlAllChroms.Text = "所有谱图";
		this.mirlAllChroms.Click += new System.EventHandler(mirlAllChroms_Click);
		this.miMethod.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[6] { this.miMtdCaculation, this.miMtdIntegration, this.miMtdMeasurement, this.toolStripSeparator10, this.miMtdTplt, this.miMtdSaveTplt });
		this.miMethod.Name = "miMethod";
		this.miMethod.Size = new System.Drawing.Size(44, 21);
		this.miMethod.Text = "方法";
		this.miMethod.Visible = false;
		this.miMtdCaculation.Name = "miMtdCaculation";
		this.miMtdCaculation.Size = new System.Drawing.Size(100, 22);
		this.miMtdCaculation.Text = "计算";
		this.miMtdCaculation.Click += new System.EventHandler(miMtdCaculation_Click);
		this.miMtdIntegration.Name = "miMtdIntegration";
		this.miMtdIntegration.Size = new System.Drawing.Size(100, 22);
		this.miMtdIntegration.Text = "积分";
		this.miMtdIntegration.Click += new System.EventHandler(miMtdIntegration_Click);
		this.miMtdMeasurement.Name = "miMtdMeasurement";
		this.miMtdMeasurement.Size = new System.Drawing.Size(100, 22);
		this.miMtdMeasurement.Text = "测量";
		this.miMtdMeasurement.Click += new System.EventHandler(miMtdMeasurement_Click);
		this.toolStripSeparator10.Name = "toolStripSeparator10";
		this.toolStripSeparator10.Size = new System.Drawing.Size(97, 6);
		this.miMtdTplt.Name = "miMtdTplt";
		this.miMtdTplt.Size = new System.Drawing.Size(100, 22);
		this.miMtdTplt.Click += new System.EventHandler(miMtdSaveTplt_Click);
		this.miMtdSaveTplt.Name = "miMtdSaveTplt";
		this.miMtdSaveTplt.Size = new System.Drawing.Size(100, 22);
		this.miMtdSaveTplt.Click += new System.EventHandler(miMtdSaveTplt_Click);
		this.miResults.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[5] { this.miRltResult, this.miRltSummary, this.miRltPerformance, this.toolStripSeparator11, this.miRltSmyOpt });
		this.miResults.Name = "miResults";
		this.miResults.Size = new System.Drawing.Size(44, 21);
		this.miResults.Text = "结果";
		this.miResults.Visible = false;
		this.miRltResult.Name = "miRltResult";
		this.miRltResult.Size = new System.Drawing.Size(133, 22);
		this.miRltResult.Text = "结果";
		this.miRltSummary.Name = "miRltSummary";
		this.miRltSummary.Size = new System.Drawing.Size(133, 22);
		this.miRltSummary.Text = "总结";
		this.miRltPerformance.Name = "miRltPerformance";
		this.miRltPerformance.Size = new System.Drawing.Size(133, 22);
		this.miRltPerformance.Text = "性能";
		this.toolStripSeparator11.Name = "toolStripSeparator11";
		this.toolStripSeparator11.Size = new System.Drawing.Size(130, 6);
		this.miRltSmyOpt.Name = "miRltSmyOpt";
		this.miRltSmyOpt.Size = new System.Drawing.Size(133, 22);
		this.miRltSmyOpt.Text = "总结选项...";
		this.miRltSmyOpt.Click += new System.EventHandler(miRltSmyOpt_Click);
		this.miSST.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[9] { this.miSstNew, this.miSstOpen, this.miSstSave, this.miSstSaveas, this.miSstUpdateFromCalib, this.toolStripSeparator13, this.miSstSet, this.toolStripSeparator12, this.miSstClearParas });
		this.miSST.Name = "miSST";
		this.miSST.Size = new System.Drawing.Size(68, 21);
		this.miSST.Text = "组分验证";
		this.miSstNew.Name = "miSstNew";
		this.miSstNew.Size = new System.Drawing.Size(136, 22);
		this.miSstNew.Text = "新建";
		this.miSstNew.Click += new System.EventHandler(misstcClearParas_Click);
		this.miSstOpen.Name = "miSstOpen";
		this.miSstOpen.Size = new System.Drawing.Size(136, 22);
		this.miSstOpen.Text = "打开...";
		this.miSstOpen.Click += new System.EventHandler(misstcClearParas_Click);
		this.miSstSave.Name = "miSstSave";
		this.miSstSave.Size = new System.Drawing.Size(136, 22);
		this.miSstSave.Text = "保存";
		this.miSstSave.Click += new System.EventHandler(misstcClearParas_Click);
		this.miSstSaveas.Name = "miSstSaveas";
		this.miSstSaveas.Size = new System.Drawing.Size(136, 22);
		this.miSstSaveas.Text = "另存...";
		this.miSstSaveas.Click += new System.EventHandler(misstcClearParas_Click);
		this.miSstUpdateFromCalib.Name = "miSstUpdateFromCalib";
		this.miSstUpdateFromCalib.Size = new System.Drawing.Size(136, 22);
		this.miSstUpdateFromCalib.Text = "从校正刷新";
		this.miSstUpdateFromCalib.Click += new System.EventHandler(misstcClearParas_Click);
		this.toolStripSeparator13.Name = "toolStripSeparator13";
		this.toolStripSeparator13.Size = new System.Drawing.Size(133, 6);
		this.miSstSet.Name = "miSstSet";
		this.miSstSet.Size = new System.Drawing.Size(136, 22);
		this.miSstSet.Text = "SST设置...";
		this.miSstSet.Click += new System.EventHandler(misstcClearParas_Click);
		this.toolStripSeparator12.Name = "toolStripSeparator12";
		this.toolStripSeparator12.Size = new System.Drawing.Size(133, 6);
		this.miSstClearParas.Name = "miSstClearParas";
		this.miSstClearParas.Size = new System.Drawing.Size(136, 22);
		this.miSstClearParas.Text = "清除参数";
		this.miSstClearParas.Click += new System.EventHandler(misstcClearParas_Click);
		this.flpChrom.AutoSize = true;
		this.flpChrom.Controls.Add(this.toolStrip1);
		this.flpChrom.Controls.Add(this.spSignals);
		this.flpChrom.Controls.Add(this.toolStrip2);
		this.flpChrom.Controls.Add(this.tsDatAcq);
		this.flpChrom.Dock = System.Windows.Forms.DockStyle.Top;
		this.flpChrom.Location = new System.Drawing.Point(0, 25);
		this.flpChrom.Margin = new System.Windows.Forms.Padding(0);
		this.flpChrom.Name = "flpChrom";
		this.flpChrom.Size = new System.Drawing.Size(1047, 50);
		this.flpChrom.TabIndex = 6;
		this.toolStrip1.Dock = System.Windows.Forms.DockStyle.None;
		this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
		this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[16]
		{
			this.btnOpen, this.btnSave, this.btnClose, this.toolStripSeparator16, this.btnReportSetup, this.btnPrtLink, this.btnPreview, this.btnPrint, this.toolStripSeparator17, this.btnPreviousZoom,
			this.btnNextZoom, this.btnUnzoom, this.toolStripSeparator18, this.btnProperties, this.toolStripSeparator19, this.btnOverlayMode
		});
		this.toolStrip1.Location = new System.Drawing.Point(0, 0);
		this.toolStrip1.Name = "toolStrip1";
		this.toolStrip1.Size = new System.Drawing.Size(308, 25);
		this.toolStrip1.TabIndex = 4;
		this.toolStrip1.Text = "toolStrip1";
		this.btnOpen.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
		this.btnOpen.ImageTransparentColor = System.Drawing.Color.Magenta;
		this.btnOpen.Margin = new System.Windows.Forms.Padding(5, 1, 0, 2);
		this.btnOpen.Name = "btnOpen";
		this.btnOpen.Size = new System.Drawing.Size(23, 22);
		this.btnOpen.Text = "toolStripButton1";
		this.btnOpen.Click += new System.EventHandler(btnOpen_Click);
		this.btnSave.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
		this.btnSave.ImageTransparentColor = System.Drawing.Color.Magenta;
		this.btnSave.Name = "btnSave";
		this.btnSave.Size = new System.Drawing.Size(23, 22);
		this.btnSave.Text = "toolStripButton2";
		this.btnSave.Click += new System.EventHandler(btnSave_Click);
		this.btnClose.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
		this.btnClose.ImageTransparentColor = System.Drawing.Color.Magenta;
		this.btnClose.Name = "btnClose";
		this.btnClose.Size = new System.Drawing.Size(23, 22);
		this.btnClose.Text = "toolStripButton3";
		this.btnClose.Click += new System.EventHandler(btnClose_Click);
		this.toolStripSeparator16.Name = "toolStripSeparator16";
		this.toolStripSeparator16.Size = new System.Drawing.Size(6, 25);
		this.btnReportSetup.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
		this.btnReportSetup.ImageTransparentColor = System.Drawing.Color.Magenta;
		this.btnReportSetup.Name = "btnReportSetup";
		this.btnReportSetup.Size = new System.Drawing.Size(23, 22);
		this.btnReportSetup.Text = "toolStripButton4";
		this.btnReportSetup.Click += new System.EventHandler(btnReportSetup_Click);
		this.btnPrtLink.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
		this.btnPrtLink.ImageTransparentColor = System.Drawing.Color.Magenta;
		this.btnPrtLink.Name = "btnPrtLink";
		this.btnPrtLink.Size = new System.Drawing.Size(23, 22);
		this.btnPrtLink.Text = "样式设置";
		this.btnPrtLink.Click += new System.EventHandler(btnPrtLink_Click);
		this.btnPreview.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
		this.btnPreview.ImageTransparentColor = System.Drawing.Color.Magenta;
		this.btnPreview.Name = "btnPreview";
		this.btnPreview.Size = new System.Drawing.Size(23, 22);
		this.btnPreview.Text = "toolStripButton5";
		this.btnPreview.Click += new System.EventHandler(btnPreview_Click);
		this.btnPrint.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
		this.btnPrint.ImageTransparentColor = System.Drawing.Color.Magenta;
		this.btnPrint.Name = "btnPrint";
		this.btnPrint.Size = new System.Drawing.Size(23, 22);
		this.btnPrint.Text = "toolStripButton6";
		this.btnPrint.Click += new System.EventHandler(btnPrint_Click);
		this.toolStripSeparator17.Name = "toolStripSeparator17";
		this.toolStripSeparator17.Size = new System.Drawing.Size(6, 25);
		this.btnPreviousZoom.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
		this.btnPreviousZoom.ImageTransparentColor = System.Drawing.Color.Magenta;
		this.btnPreviousZoom.Name = "btnPreviousZoom";
		this.btnPreviousZoom.Size = new System.Drawing.Size(23, 22);
		this.btnPreviousZoom.Text = "toolStripButton7";
		this.btnPreviousZoom.Click += new System.EventHandler(dpgpcChrom_DoubleClick);
		this.btnNextZoom.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
		this.btnNextZoom.ImageTransparentColor = System.Drawing.Color.Magenta;
		this.btnNextZoom.Name = "btnNextZoom";
		this.btnNextZoom.Size = new System.Drawing.Size(23, 22);
		this.btnNextZoom.Text = "toolStripButton8";
		this.btnNextZoom.Click += new System.EventHandler(btnNextZoom_Click);
		this.btnUnzoom.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
		this.btnUnzoom.ImageTransparentColor = System.Drawing.Color.Magenta;
		this.btnUnzoom.Name = "btnUnzoom";
		this.btnUnzoom.Size = new System.Drawing.Size(23, 22);
		this.btnUnzoom.Text = "toolStripButton9";
		this.btnUnzoom.Click += new System.EventHandler(btnUnzoom_Click);
		this.toolStripSeparator18.Name = "toolStripSeparator18";
		this.toolStripSeparator18.Size = new System.Drawing.Size(6, 25);
		this.btnProperties.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
		this.btnProperties.ImageTransparentColor = System.Drawing.Color.Magenta;
		this.btnProperties.Name = "btnProperties";
		this.btnProperties.Size = new System.Drawing.Size(23, 22);
		this.btnProperties.Text = "toolStripButton10";
		this.btnProperties.Click += new System.EventHandler(btnProperties_Click);
		this.toolStripSeparator19.Name = "toolStripSeparator19";
		this.toolStripSeparator19.Size = new System.Drawing.Size(6, 25);
		this.btnOverlayMode.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
		this.btnOverlayMode.ImageTransparentColor = System.Drawing.Color.Magenta;
		this.btnOverlayMode.Name = "btnOverlayMode";
		this.btnOverlayMode.Size = new System.Drawing.Size(23, 22);
		this.btnOverlayMode.Text = "toolStripButton12";
		this.btnOverlayMode.Click += new System.EventHandler(btnOverlayMode_Click);
		this.spSignals.BackColor = System.Drawing.Color.Lime;
		this.spSignals.ButtonsNum = 0;
		this.spSignals.Location = new System.Drawing.Point(308, 0);
		this.spSignals.Margin = new System.Windows.Forms.Padding(0);
		this.spSignals.Name = "spSignals";
		this.spSignals.Size = new System.Drawing.Size(200, 25);
		this.spSignals.TabIndex = 7;
		this.toolStrip2.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
		this.toolStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[34]
		{
			this.btnExpress, this.toolStripSeparator20, this.btngblPeakWidth, this.btngblThreshold, this.btngblPkSlope, this.toolStripSeparator21, this.btnipResetDtecNeg, this.btnipClampNeg, this.toolStripSeparator5, this.btnipPkWidth,
			this.btnipPkThreshold, this.btnipPkAddPosi, this.btnipPkAddNeg, this.btnipPkCut, this.btnipPkHalfWidth, this.btnipPkArea, this.toolStripSeparator23, this.btnipPkVale, this.toolStripSeparator28, this.btnipSolventPeak,
			this.btnipFlowMarker, this.btnipGroups, this.toolStripSeparator22, this.btnbsBsTgnt, this.btnbsBsVtV, this.toolStripSeparator14, this.btnbsBsValley, this.btnbsBsTogether, this.btnbsBsForwHorz, this.btnbsBsBackHorz,
			this.btnbsBsFrontTgnt, this.btnbsBsTailTgnt, this.toolStripSeparator31, this.btngblDtecDelay
		});
		this.toolStrip2.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
		this.toolStrip2.Location = new System.Drawing.Point(0, 25);
		this.toolStrip2.Name = "toolStrip2";
		this.toolStrip2.Size = new System.Drawing.Size(556, 25);
		this.toolStrip2.TabIndex = 6;
		this.toolStrip2.Text = "toolStrip2";
		this.btnExpress.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
		this.btnExpress.ImageTransparentColor = System.Drawing.Color.Magenta;
		this.btnExpress.Margin = new System.Windows.Forms.Padding(5, 1, 0, 2);
		this.btnExpress.Name = "btnExpress";
		this.btnExpress.Size = new System.Drawing.Size(23, 22);
		this.btnExpress.Text = "动态帮助";
		this.btnExpress.Click += new System.EventHandler(btnExpress_Click);
		this.toolStripSeparator20.Name = "toolStripSeparator20";
		this.toolStripSeparator20.Size = new System.Drawing.Size(6, 25);
		this.btngblPeakWidth.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
		this.btngblPeakWidth.ImageTransparentColor = System.Drawing.Color.Magenta;
		this.btngblPeakWidth.Name = "btngblPeakWidth";
		this.btngblPeakWidth.Size = new System.Drawing.Size(23, 22);
		this.btngblPeakWidth.Text = "toolStripButton33";
		this.btngblPeakWidth.Click += new System.EventHandler(btngblDtecDelay_Click);
		this.btngblThreshold.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
		this.btngblThreshold.ImageTransparentColor = System.Drawing.Color.Magenta;
		this.btngblThreshold.Name = "btngblThreshold";
		this.btngblThreshold.Size = new System.Drawing.Size(23, 22);
		this.btngblThreshold.Text = "toolStripButton34";
		this.btngblThreshold.Click += new System.EventHandler(btngblDtecDelay_Click);
		this.btngblPkSlope.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
		this.btngblPkSlope.ImageTransparentColor = System.Drawing.Color.Magenta;
		this.btngblPkSlope.Name = "btngblPkSlope";
		this.btngblPkSlope.Size = new System.Drawing.Size(23, 22);
		this.btngblPkSlope.Text = "toolStripButton49";
		this.btngblPkSlope.Visible = false;
		this.btngblPkSlope.Click += new System.EventHandler(btngblDtecDelay_Click);
		this.toolStripSeparator21.Name = "toolStripSeparator21";
		this.toolStripSeparator21.Size = new System.Drawing.Size(6, 25);
		this.btnipResetDtecNeg.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
		this.btnipResetDtecNeg.ImageTransparentColor = System.Drawing.Color.Magenta;
		this.btnipResetDtecNeg.Name = "btnipResetDtecNeg";
		this.btnipResetDtecNeg.Size = new System.Drawing.Size(23, 22);
		this.btnipResetDtecNeg.Text = "toolStripButton56";
		this.btnipResetDtecNeg.Click += new System.EventHandler(btngblDtecDelay_Click);
		this.btnipClampNeg.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
		this.btnipClampNeg.ImageTransparentColor = System.Drawing.Color.Magenta;
		this.btnipClampNeg.Name = "btnipClampNeg";
		this.btnipClampNeg.Size = new System.Drawing.Size(23, 22);
		this.btnipClampNeg.Text = "toolStripButton50";
		this.btnipClampNeg.Click += new System.EventHandler(btngblDtecDelay_Click);
		this.toolStripSeparator5.Name = "toolStripSeparator5";
		this.toolStripSeparator5.Size = new System.Drawing.Size(6, 25);
		this.btnipPkWidth.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
		this.btnipPkWidth.ImageTransparentColor = System.Drawing.Color.Magenta;
		this.btnipPkWidth.Name = "btnipPkWidth";
		this.btnipPkWidth.Size = new System.Drawing.Size(23, 22);
		this.btnipPkWidth.Text = "toolStripButton49";
		this.btnipPkWidth.Click += new System.EventHandler(btngblDtecDelay_Click);
		this.btnipPkThreshold.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
		this.btnipPkThreshold.ImageTransparentColor = System.Drawing.Color.Magenta;
		this.btnipPkThreshold.Name = "btnipPkThreshold";
		this.btnipPkThreshold.Size = new System.Drawing.Size(23, 22);
		this.btnipPkThreshold.Text = "toolStripButton49";
		this.btnipPkThreshold.Click += new System.EventHandler(btngblDtecDelay_Click);
		this.btnipPkAddPosi.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
		this.btnipPkAddPosi.ImageTransparentColor = System.Drawing.Color.Magenta;
		this.btnipPkAddPosi.Name = "btnipPkAddPosi";
		this.btnipPkAddPosi.Size = new System.Drawing.Size(23, 22);
		this.btnipPkAddPosi.Text = "toolStripButton38";
		this.btnipPkAddPosi.Click += new System.EventHandler(btngblDtecDelay_Click);
		this.btnipPkAddNeg.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
		this.btnipPkAddNeg.ImageTransparentColor = System.Drawing.Color.Magenta;
		this.btnipPkAddNeg.Name = "btnipPkAddNeg";
		this.btnipPkAddNeg.Size = new System.Drawing.Size(23, 22);
		this.btnipPkAddNeg.Text = "toolStripButton39";
		this.btnipPkAddNeg.Click += new System.EventHandler(btngblDtecDelay_Click);
		this.btnipPkCut.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
		this.btnipPkCut.ImageTransparentColor = System.Drawing.Color.Magenta;
		this.btnipPkCut.Name = "btnipPkCut";
		this.btnipPkCut.Size = new System.Drawing.Size(23, 22);
		this.btnipPkCut.Text = "toolStripButton51";
		this.btnipPkCut.Click += new System.EventHandler(btngblDtecDelay_Click);
		this.btnipPkHalfWidth.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
		this.btnipPkHalfWidth.ImageTransparentColor = System.Drawing.Color.Magenta;
		this.btnipPkHalfWidth.Name = "btnipPkHalfWidth";
		this.btnipPkHalfWidth.Size = new System.Drawing.Size(23, 22);
		this.btnipPkHalfWidth.Text = "toolStripButton59";
		this.btnipPkHalfWidth.Click += new System.EventHandler(btngblDtecDelay_Click);
		this.btnipPkArea.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
		this.btnipPkArea.ImageTransparentColor = System.Drawing.Color.Magenta;
		this.btnipPkArea.Name = "btnipPkArea";
		this.btnipPkArea.Size = new System.Drawing.Size(23, 22);
		this.btnipPkArea.Text = "toolStripButton57";
		this.btnipPkArea.Click += new System.EventHandler(btngblDtecDelay_Click);
		this.toolStripSeparator23.Name = "toolStripSeparator23";
		this.toolStripSeparator23.Size = new System.Drawing.Size(6, 25);
		this.btnipPkVale.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
		this.btnipPkVale.ImageTransparentColor = System.Drawing.Color.Magenta;
		this.btnipPkVale.Name = "btnipPkVale";
		this.btnipPkVale.Size = new System.Drawing.Size(23, 22);
		this.btnipPkVale.Text = "toolStripButton35";
		this.btnipPkVale.Click += new System.EventHandler(btngblDtecDelay_Click);
		this.toolStripSeparator28.Name = "toolStripSeparator28";
		this.toolStripSeparator28.Size = new System.Drawing.Size(6, 25);
		this.toolStripSeparator28.Visible = false;
		this.btnipSolventPeak.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
		this.btnipSolventPeak.ImageTransparentColor = System.Drawing.Color.Magenta;
		this.btnipSolventPeak.Name = "btnipSolventPeak";
		this.btnipSolventPeak.Size = new System.Drawing.Size(23, 22);
		this.btnipSolventPeak.Text = "toolStripButton40";
		this.btnipSolventPeak.Visible = false;
		this.btnipSolventPeak.Click += new System.EventHandler(btngblDtecDelay_Click);
		this.btnipFlowMarker.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
		this.btnipFlowMarker.ImageTransparentColor = System.Drawing.Color.Magenta;
		this.btnipFlowMarker.Name = "btnipFlowMarker";
		this.btnipFlowMarker.Size = new System.Drawing.Size(23, 22);
		this.btnipFlowMarker.Text = "toolStripButton41";
		this.btnipFlowMarker.Visible = false;
		this.btnipFlowMarker.Click += new System.EventHandler(btngblDtecDelay_Click);
		this.btnipGroups.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
		this.btnipGroups.ImageTransparentColor = System.Drawing.Color.Magenta;
		this.btnipGroups.Name = "btnipGroups";
		this.btnipGroups.Size = new System.Drawing.Size(23, 22);
		this.btnipGroups.Text = "toolStripButton42";
		this.btnipGroups.Visible = false;
		this.btnipGroups.Click += new System.EventHandler(btngblDtecDelay_Click);
		this.toolStripSeparator22.Name = "toolStripSeparator22";
		this.toolStripSeparator22.Size = new System.Drawing.Size(6, 25);
		this.btnbsBsTgnt.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
		this.btnbsBsTgnt.ImageTransparentColor = System.Drawing.Color.Magenta;
		this.btnbsBsTgnt.Name = "btnbsBsTgnt";
		this.btnbsBsTgnt.Size = new System.Drawing.Size(23, 22);
		this.btnbsBsTgnt.Text = "toolStripButton1";
		this.btnbsBsTgnt.Click += new System.EventHandler(btngblDtecDelay_Click);
		this.btnbsBsVtV.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
		this.btnbsBsVtV.ImageTransparentColor = System.Drawing.Color.Magenta;
		this.btnbsBsVtV.Name = "btnbsBsVtV";
		this.btnbsBsVtV.Size = new System.Drawing.Size(23, 22);
		this.btnbsBsVtV.Text = "toolStripButton2";
		this.btnbsBsVtV.Click += new System.EventHandler(btngblDtecDelay_Click);
		this.toolStripSeparator14.Name = "toolStripSeparator14";
		this.toolStripSeparator14.Size = new System.Drawing.Size(6, 25);
		this.btnbsBsValley.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
		this.btnbsBsValley.ImageTransparentColor = System.Drawing.Color.Magenta;
		this.btnbsBsValley.Name = "btnbsBsValley";
		this.btnbsBsValley.Size = new System.Drawing.Size(23, 22);
		this.btnbsBsValley.Text = "toolStripButton44";
		this.btnbsBsValley.Click += new System.EventHandler(btngblDtecDelay_Click);
		this.btnbsBsTogether.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
		this.btnbsBsTogether.ImageTransparentColor = System.Drawing.Color.Magenta;
		this.btnbsBsTogether.Name = "btnbsBsTogether";
		this.btnbsBsTogether.Size = new System.Drawing.Size(23, 22);
		this.btnbsBsTogether.Text = "toolStripButton45";
		this.btnbsBsTogether.Click += new System.EventHandler(btngblDtecDelay_Click);
		this.btnbsBsForwHorz.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
		this.btnbsBsForwHorz.ImageTransparentColor = System.Drawing.Color.Magenta;
		this.btnbsBsForwHorz.Name = "btnbsBsForwHorz";
		this.btnbsBsForwHorz.Size = new System.Drawing.Size(23, 22);
		this.btnbsBsForwHorz.Text = "toolStripButton46";
		this.btnbsBsForwHorz.Click += new System.EventHandler(btngblDtecDelay_Click);
		this.btnbsBsBackHorz.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
		this.btnbsBsBackHorz.ImageTransparentColor = System.Drawing.Color.Magenta;
		this.btnbsBsBackHorz.Name = "btnbsBsBackHorz";
		this.btnbsBsBackHorz.Size = new System.Drawing.Size(23, 22);
		this.btnbsBsBackHorz.Text = "toolStripButton47";
		this.btnbsBsBackHorz.Click += new System.EventHandler(btngblDtecDelay_Click);
		this.btnbsBsFrontTgnt.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
		this.btnbsBsFrontTgnt.ImageTransparentColor = System.Drawing.Color.Magenta;
		this.btnbsBsFrontTgnt.Name = "btnbsBsFrontTgnt";
		this.btnbsBsFrontTgnt.Size = new System.Drawing.Size(23, 22);
		this.btnbsBsFrontTgnt.Text = "toolStripButton48";
		this.btnbsBsFrontTgnt.Click += new System.EventHandler(btngblDtecDelay_Click);
		this.btnbsBsTailTgnt.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
		this.btnbsBsTailTgnt.ImageTransparentColor = System.Drawing.Color.Magenta;
		this.btnbsBsTailTgnt.Name = "btnbsBsTailTgnt";
		this.btnbsBsTailTgnt.Size = new System.Drawing.Size(23, 22);
		this.btnbsBsTailTgnt.Text = "toolStripButton49";
		this.btnbsBsTailTgnt.Click += new System.EventHandler(btngblDtecDelay_Click);
		this.toolStripSeparator31.Name = "toolStripSeparator31";
		this.toolStripSeparator31.Size = new System.Drawing.Size(6, 25);
		this.btngblDtecDelay.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
		this.btngblDtecDelay.ImageTransparentColor = System.Drawing.Color.Magenta;
		this.btngblDtecDelay.Name = "btngblDtecDelay";
		this.btngblDtecDelay.Size = new System.Drawing.Size(23, 22);
		this.btngblDtecDelay.Text = "toolStripButton60";
		this.btngblDtecDelay.Click += new System.EventHandler(btngblDtecDelay_Click);
		this.tsDatAcq.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
		this.tsDatAcq.Items.AddRange(new System.Windows.Forms.ToolStripItem[8] { this.lbTime, this.tbTime, this.lbTimeU, this.lbSignal, this.tbSigYBeg, this.lbSignalU, this.tbSigYEnd, this.lbyUnit });
		this.tsDatAcq.Location = new System.Drawing.Point(556, 25);
		this.tsDatAcq.Name = "tsDatAcq";
		this.tsDatAcq.Size = new System.Drawing.Size(307, 25);
		this.tsDatAcq.TabIndex = 8;
		this.tsDatAcq.Text = "toolStrip1";
		this.tsDatAcq.Paint += new System.Windows.Forms.PaintEventHandler(tsDatAcq_Paint);
		this.lbTime.Margin = new System.Windows.Forms.Padding(5, 1, 0, 2);
		this.lbTime.Name = "lbTime";
		this.lbTime.Size = new System.Drawing.Size(32, 22);
		this.lbTime.Text = "时轴";
		this.tbTime.BorderStyle = System.Windows.Forms.BorderStyle.None;
		this.tbTime.Name = "tbTime";
		this.tbTime.Size = new System.Drawing.Size(50, 25);
		this.tbTime.Text = "30";
		this.tbTime.TextBoxTextAlign = System.Windows.Forms.HorizontalAlignment.Center;
		this.tbTime.KeyDown += new System.Windows.Forms.KeyEventHandler(tbSigYEnd_KeyDown);
		this.tbTime.DoubleClick += new System.EventHandler(tbSigYEnd_DoubleClick);
		this.lbTimeU.Margin = new System.Windows.Forms.Padding(2, 1, 0, 2);
		this.lbTimeU.Name = "lbTimeU";
		this.lbTimeU.Size = new System.Drawing.Size(37, 22);
		this.lbTimeU.Text = "[min]";
		this.lbSignal.Name = "lbSignal";
		this.lbSignal.Size = new System.Drawing.Size(32, 22);
		this.lbSignal.Text = "信号";
		this.tbSigYBeg.BorderStyle = System.Windows.Forms.BorderStyle.None;
		this.tbSigYBeg.Name = "tbSigYBeg";
		this.tbSigYBeg.Size = new System.Drawing.Size(50, 25);
		this.tbSigYBeg.Text = "-10";
		this.tbSigYBeg.TextBoxTextAlign = System.Windows.Forms.HorizontalAlignment.Center;
		this.tbSigYBeg.KeyDown += new System.Windows.Forms.KeyEventHandler(tbSigYEnd_KeyDown);
		this.tbSigYBeg.DoubleClick += new System.EventHandler(tbSigYEnd_DoubleClick);
		this.lbSignalU.Margin = new System.Windows.Forms.Padding(3, 1, 0, 2);
		this.lbSignalU.Name = "lbSignalU";
		this.lbSignalU.Size = new System.Drawing.Size(0, 22);
		this.tbSigYEnd.BorderStyle = System.Windows.Forms.BorderStyle.None;
		this.tbSigYEnd.Name = "tbSigYEnd";
		this.tbSigYEnd.Size = new System.Drawing.Size(50, 25);
		this.tbSigYEnd.Text = "500";
		this.tbSigYEnd.TextBoxTextAlign = System.Windows.Forms.HorizontalAlignment.Center;
		this.tbSigYEnd.KeyDown += new System.Windows.Forms.KeyEventHandler(tbSigYEnd_KeyDown);
		this.tbSigYEnd.DoubleClick += new System.EventHandler(tbSigYEnd_DoubleClick);
		this.lbyUnit.Margin = new System.Windows.Forms.Padding(3, 1, 0, 2);
		this.lbyUnit.Name = "lbyUnit";
		this.lbyUnit.Size = new System.Drawing.Size(34, 22);
		this.lbyUnit.Text = "mAu";
		this.ssChrom.Items.AddRange(new System.Windows.Forms.ToolStripItem[1] { this.slbExplain });
		this.ssChrom.Location = new System.Drawing.Point(0, 551);
		this.ssChrom.Name = "ssChrom";
		this.ssChrom.Size = new System.Drawing.Size(1047, 22);
		this.ssChrom.TabIndex = 7;
		this.ssChrom.Text = "statusStrip1";
		this.slbExplain.Name = "slbExplain";
		this.slbExplain.Size = new System.Drawing.Size(0, 17);
		this.dpgnlChrom.BackColor = System.Drawing.Color.BlanchedAlmond;
		this.dpgnlChrom.Location = new System.Drawing.Point(21, 23);
		this.dpgnlChrom.Name = "dpgnlChrom";
		this.dpgnlChrom.Size = new System.Drawing.Size(310, 79);
		this.dpgnlChrom.TabIndex = 9;
		this.dpgnlChrom.Click += new System.EventHandler(dpgpcChrom_Click);
		this.dpgnlChrom.Paint += new System.Windows.Forms.PaintEventHandler(dpgpcChrom_Paint);
		this.dpgnlChrom.DoubleClick += new System.EventHandler(dpgpcChrom_DoubleClick);
		this.dpgnlChrom.MouseDown += new System.Windows.Forms.MouseEventHandler(dpgpcChrom_MouseDown);
		this.dpgnlChrom.MouseLeave += new System.EventHandler(dpgpcChrom_MouseLeave);
		this.dpgnlChrom.MouseMove += new System.Windows.Forms.MouseEventHandler(dpgpcChrom_MouseMove);
		this.dpgnlChrom.MouseUp += new System.Windows.Forms.MouseEventHandler(dpgpcChrom_MouseUp);
		this.dpgnlChrom.Resize += new System.EventHandler(dpgnlChrom_Resize);
		this.lbExpress.AutoEllipsis = true;
		this.lbExpress.BackColor = System.Drawing.Color.PowderBlue;
		this.lbExpress.Location = new System.Drawing.Point(19, 126);
		this.lbExpress.Name = "lbExpress";
		this.lbExpress.Size = new System.Drawing.Size(99, 12);
		this.lbExpress.TabIndex = 1;
		this.lbExpress.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.tcGPC.Controls.Add(this.tpgpcChrom);
		this.tcGPC.Controls.Add(this.tpgpcMwDistrib);
		this.tcGPC.Controls.Add(this.tpgpcCmlMw);
		this.tcGPC.ItemSize = new System.Drawing.Size(90, 19);
		this.tcGPC.Location = new System.Drawing.Point(349, 9);
		this.tcGPC.Name = "tcGPC";
		this.tcGPC.SelectedIndex = 0;
		this.tcGPC.Size = new System.Drawing.Size(310, 102);
		this.tcGPC.TabIndex = 10;
		this.tcGPC.Resize += new System.EventHandler(tcGPC_Resize);
		this.tpgpcChrom.Controls.Add(this.dpgpcChrom);
		this.tpgpcChrom.Location = new System.Drawing.Point(4, 23);
		this.tpgpcChrom.Name = "tpgpcChrom";
		this.tpgpcChrom.Size = new System.Drawing.Size(302, 75);
		this.tpgpcChrom.TabIndex = 0;
		this.tpgpcChrom.Text = "谱图";
		this.tpgpcChrom.UseVisualStyleBackColor = true;
		this.dpgpcChrom.BackColor = System.Drawing.Color.BlanchedAlmond;
		this.dpgpcChrom.Location = new System.Drawing.Point(16, 9);
		this.dpgpcChrom.Name = "dpgpcChrom";
		this.dpgpcChrom.Size = new System.Drawing.Size(200, 61);
		this.dpgpcChrom.TabIndex = 1;
		this.dpgpcChrom.Click += new System.EventHandler(dpgpcChrom_Click);
		this.dpgpcChrom.Paint += new System.Windows.Forms.PaintEventHandler(dpgpcChrom_Paint);
		this.dpgpcChrom.DoubleClick += new System.EventHandler(dpgpcChrom_DoubleClick);
		this.dpgpcChrom.MouseDown += new System.Windows.Forms.MouseEventHandler(dpgpcChrom_MouseDown);
		this.dpgpcChrom.MouseLeave += new System.EventHandler(dpgpcChrom_MouseLeave);
		this.dpgpcChrom.MouseMove += new System.Windows.Forms.MouseEventHandler(dpgpcChrom_MouseMove);
		this.dpgpcChrom.MouseUp += new System.Windows.Forms.MouseEventHandler(dpgpcChrom_MouseUp);
		this.tpgpcMwDistrib.Controls.Add(this.dpgpcMwDistrib);
		this.tpgpcMwDistrib.Location = new System.Drawing.Point(4, 23);
		this.tpgpcMwDistrib.Name = "tpgpcMwDistrib";
		this.tpgpcMwDistrib.Size = new System.Drawing.Size(302, 75);
		this.tpgpcMwDistrib.TabIndex = 1;
		this.tpgpcMwDistrib.Text = "重均微分分布";
		this.tpgpcMwDistrib.UseVisualStyleBackColor = true;
		this.dpgpcMwDistrib.BackColor = System.Drawing.Color.BlanchedAlmond;
		this.dpgpcMwDistrib.Location = new System.Drawing.Point(305, 23);
		this.dpgpcMwDistrib.Name = "dpgpcMwDistrib";
		this.dpgpcMwDistrib.Size = new System.Drawing.Size(200, 37);
		this.dpgpcMwDistrib.TabIndex = 1;
		this.tpgpcCmlMw.Controls.Add(this.dpgpcCmlMw);
		this.tpgpcCmlMw.Location = new System.Drawing.Point(4, 23);
		this.tpgpcCmlMw.Name = "tpgpcCmlMw";
		this.tpgpcCmlMw.Size = new System.Drawing.Size(302, 75);
		this.tpgpcCmlMw.TabIndex = 2;
		this.tpgpcCmlMw.Text = "重均积分分布";
		this.tpgpcCmlMw.UseVisualStyleBackColor = true;
		this.dpgpcCmlMw.BackColor = System.Drawing.Color.BlanchedAlmond;
		this.dpgpcCmlMw.Location = new System.Drawing.Point(54, 16);
		this.dpgpcCmlMw.Name = "dpgpcCmlMw";
		this.dpgpcCmlMw.Size = new System.Drawing.Size(200, 37);
		this.dpgpcCmlMw.TabIndex = 0;
		this.tcChrom.Controls.Add(this.tpResults);
		this.tcChrom.Controls.Add(this.tpSummary);
		this.tcChrom.Controls.Add(this.tpPerformance);
		this.tcChrom.Controls.Add(this.tpIntegration);
		this.tcChrom.Controls.Add(this.tpMsmCdts);
		this.tcChrom.Controls.Add(this.tpSST);
		this.tcChrom.Controls.Add(this.tpSlices);
		this.tcChrom.Controls.Add(this.tpRanges);
		this.tcChrom.Controls.Add(this.tpRightsArchives);
		this.tcChrom.ItemSize = new System.Drawing.Size(90, 19);
		this.tcChrom.Location = new System.Drawing.Point(4, 6);
		this.tcChrom.Name = "tcChrom";
		this.tcChrom.Padding = new System.Drawing.Point(0, 0);
		this.tcChrom.SelectedIndex = 0;
		this.tcChrom.Size = new System.Drawing.Size(982, 318);
		this.tcChrom.TabIndex = 12;
		this.tcChrom.SelectedIndexChanged += new System.EventHandler(tcChrom_SelectedIndexChanged);
		this.tpResults.BackColor = System.Drawing.Color.Transparent;
		this.tpResults.Controls.Add(this.pnlgcu);
		this.tpResults.Controls.Add(this.gvRltsDad);
		this.tpResults.Controls.Add(this.gvRltsGpc);
		this.tpResults.Controls.Add(this.gvRltsGnl);
		this.tpResults.Controls.Add(this.lbRltExpress);
		this.tpResults.Controls.Add(this.pnlRltsControl);
		this.tpResults.Location = new System.Drawing.Point(4, 23);
		this.tpResults.Name = "tpResults";
		this.tpResults.Size = new System.Drawing.Size(974, 291);
		this.tpResults.TabIndex = 0;
		this.tpResults.Text = "结果";
		this.pnlgcu.Controls.Add(this.lbgcuAlpha);
		this.pnlgcu.Controls.Add(this.lbgcuK);
		this.pnlgcu.Controls.Add(this.btngcuKAlpha);
		this.pnlgcu.Controls.Add(this.tbgcuAlpha);
		this.pnlgcu.Controls.Add(this.tbgcuK);
		this.pnlgcu.Location = new System.Drawing.Point(345, 19);
		this.pnlgcu.Name = "pnlgcu";
		this.pnlgcu.Size = new System.Drawing.Size(200, 100);
		this.pnlgcu.TabIndex = 2;
		this.lbgcuAlpha.AutoSize = true;
		this.lbgcuAlpha.Location = new System.Drawing.Point(14, 44);
		this.lbgcuAlpha.Name = "lbgcuAlpha";
		this.lbgcuAlpha.Size = new System.Drawing.Size(35, 12);
		this.lbgcuAlpha.TabIndex = 1;
		this.lbgcuAlpha.Text = "Alpha";
		this.lbgcuK.AutoSize = true;
		this.lbgcuK.Location = new System.Drawing.Point(14, 19);
		this.lbgcuK.Name = "lbgcuK";
		this.lbgcuK.Size = new System.Drawing.Size(77, 12);
		this.lbgcuK.TabIndex = 1;
		this.lbgcuK.Text = "K[dL/g*10^3]";
		this.btngcuKAlpha.Location = new System.Drawing.Point(80, 68);
		this.btngcuKAlpha.Name = "btngcuKAlpha";
		this.btngcuKAlpha.Size = new System.Drawing.Size(100, 23);
		this.btngcuKAlpha.TabIndex = 1;
		this.btngcuKAlpha.Text = "载入K , Alpha";
		this.btngcuKAlpha.UseVisualStyleBackColor = true;
		this.btngcuKAlpha.Click += new System.EventHandler(btngcuKAlpha_Click);
		this.tbgcuAlpha.Location = new System.Drawing.Point(104, 41);
		this.tbgcuAlpha.Name = "tbgcuAlpha";
		this.tbgcuAlpha.Size = new System.Drawing.Size(76, 21);
		this.tbgcuAlpha.TabIndex = 0;
		this.tbgcuAlpha.KeyPress += new System.Windows.Forms.KeyPressEventHandler(tbgcuAlpha_KeyPress);
		this.tbgcuK.Location = new System.Drawing.Point(104, 16);
		this.tbgcuK.Name = "tbgcuK";
		this.tbgcuK.Size = new System.Drawing.Size(76, 21);
		this.tbgcuK.TabIndex = 0;
		this.gvRltsDad.AllowUserToAddRows = false;
		this.gvRltsDad.AllowUserToDeleteRows = false;
		this.gvRltsDad.AllowUserToResizeRows = false;
		this.gvRltsDad.BackgroundColor = System.Drawing.Color.AliceBlue;
		this.gvRltsDad.CharacterHeaderColor = System.Drawing.Color.Black;
		dataGridViewCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
		dataGridViewCellStyle.BackColor = System.Drawing.SystemColors.Control;
		dataGridViewCellStyle.Font = new System.Drawing.Font("宋体", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		dataGridViewCellStyle.ForeColor = System.Drawing.SystemColors.WindowText;
		dataGridViewCellStyle.SelectionBackColor = System.Drawing.SystemColors.Highlight;
		dataGridViewCellStyle.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
		dataGridViewCellStyle.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
		this.gvRltsDad.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle;
		this.gvRltsDad.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
		this.gvRltsDad.ContextMenuStrip = this.cmsRltGV;
		dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
		dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
		dataGridViewCellStyle2.Font = new System.Drawing.Font("宋体", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		dataGridViewCellStyle2.ForeColor = System.Drawing.Color.Black;
		dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
		dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
		dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
		this.gvRltsDad.DefaultCellStyle = dataGridViewCellStyle2;
		this.gvRltsDad.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
		this.gvRltsDad.Location = new System.Drawing.Point(30, 162);
		this.gvRltsDad.Name = "gvRltsDad";
		dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
		dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
		dataGridViewCellStyle3.Font = new System.Drawing.Font("宋体", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
		dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
		dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
		dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
		this.gvRltsDad.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
		this.gvRltsDad.RowHeadersWidth = 25;
		this.gvRltsDad.RowTemplate.Height = 16;
		this.gvRltsDad.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
		this.gvRltsDad.ShowCellToolTips = false;
		this.gvRltsDad.Size = new System.Drawing.Size(149, 32);
		this.gvRltsDad.TabIndex = 4;
		this.gvRltsDad.CellBeginEdit += new System.Windows.Forms.DataGridViewCellCancelEventHandler(gvRltsGnl_CellBeginEdit);
		this.gvRltsDad.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(gvRltsGnl_CellDoubleClick);
		this.gvRltsDad.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(gvRltsGnl_CellEndEdit);
		this.gvRltsDad.CellMouseDown += new System.Windows.Forms.DataGridViewCellMouseEventHandler(gvRltsGnl_CellMouseDown);
		this.gvRltsDad.SelectionChanged += new System.EventHandler(gvRltsGnl_SelectionChanged);
		this.cmsRltGV.Items.AddRange(new System.Windows.Forms.ToolStripItem[4] { this.mirltsColumnsSetup, this.mirltsRestoreDftColumns, this.toolStripSeparator33, this.mirltsResetCmpdNames });
		this.cmsRltGV.Name = "cmsRltGV";
		this.cmsRltGV.ShowImageMargin = false;
		this.cmsRltGV.Size = new System.Drawing.Size(136, 76);
		this.mirltsColumnsSetup.Name = "mirltsColumnsSetup";
		this.mirltsColumnsSetup.Size = new System.Drawing.Size(135, 22);
		this.mirltsColumnsSetup.Text = "列设置...";
		this.mirltsColumnsSetup.Click += new System.EventHandler(mirltsResetCmpdNames_Click);
		this.mirltsRestoreDftColumns.Name = "mirltsRestoreDftColumns";
		this.mirltsRestoreDftColumns.Size = new System.Drawing.Size(135, 22);
		this.mirltsRestoreDftColumns.Text = "恢复默认列设置";
		this.mirltsRestoreDftColumns.Click += new System.EventHandler(mirltsResetCmpdNames_Click);
		this.toolStripSeparator33.Name = "toolStripSeparator33";
		this.toolStripSeparator33.Size = new System.Drawing.Size(132, 6);
		this.mirltsResetCmpdNames.Name = "mirltsResetCmpdNames";
		this.mirltsResetCmpdNames.Size = new System.Drawing.Size(135, 22);
		this.mirltsResetCmpdNames.Click += new System.EventHandler(mirltsResetCmpdNames_Click);
		this.gvRltsGpc.AllowUserToAddRows = false;
		this.gvRltsGpc.AllowUserToDeleteRows = false;
		this.gvRltsGpc.AllowUserToResizeRows = false;
		this.gvRltsGpc.BackgroundColor = System.Drawing.Color.AliceBlue;
		this.gvRltsGpc.CharacterHeaderColor = System.Drawing.Color.Black;
		dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
		dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control;
		dataGridViewCellStyle4.Font = new System.Drawing.Font("宋体", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
		dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
		dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
		dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
		this.gvRltsGpc.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle4;
		this.gvRltsGpc.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
		this.gvRltsGpc.ContextMenuStrip = this.cmsRltGV;
		dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
		dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Window;
		dataGridViewCellStyle5.Font = new System.Drawing.Font("宋体", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		dataGridViewCellStyle5.ForeColor = System.Drawing.Color.Black;
		dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
		dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
		dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
		this.gvRltsGpc.DefaultCellStyle = dataGridViewCellStyle5;
		this.gvRltsGpc.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
		this.gvRltsGpc.Location = new System.Drawing.Point(30, 114);
		this.gvRltsGpc.Name = "gvRltsGpc";
		dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
		dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Control;
		dataGridViewCellStyle6.Font = new System.Drawing.Font("宋体", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.WindowText;
		dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
		dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
		dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
		this.gvRltsGpc.RowHeadersDefaultCellStyle = dataGridViewCellStyle6;
		this.gvRltsGpc.RowHeadersWidth = 25;
		this.gvRltsGpc.RowTemplate.Height = 16;
		this.gvRltsGpc.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
		this.gvRltsGpc.ShowCellToolTips = false;
		this.gvRltsGpc.Size = new System.Drawing.Size(149, 32);
		this.gvRltsGpc.TabIndex = 4;
		this.gvRltsGpc.CellBeginEdit += new System.Windows.Forms.DataGridViewCellCancelEventHandler(gvRltsGnl_CellBeginEdit);
		this.gvRltsGpc.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(gvRltsGnl_CellDoubleClick);
		this.gvRltsGpc.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(gvRltsGnl_CellEndEdit);
		this.gvRltsGpc.CellMouseDown += new System.Windows.Forms.DataGridViewCellMouseEventHandler(gvRltsGnl_CellMouseDown);
		this.gvRltsGpc.SelectionChanged += new System.EventHandler(gvRltsGnl_SelectionChanged);
		this.gvRltsGnl.AllowUserToAddRows = false;
		this.gvRltsGnl.AllowUserToDeleteRows = false;
		this.gvRltsGnl.AllowUserToResizeRows = false;
		this.gvRltsGnl.BackgroundColor = System.Drawing.Color.AliceBlue;
		this.gvRltsGnl.CharacterHeaderColor = System.Drawing.Color.Black;
		dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
		dataGridViewCellStyle7.BackColor = System.Drawing.SystemColors.Control;
		dataGridViewCellStyle7.Font = new System.Drawing.Font("宋体", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		dataGridViewCellStyle7.ForeColor = System.Drawing.SystemColors.WindowText;
		dataGridViewCellStyle7.SelectionBackColor = System.Drawing.SystemColors.Highlight;
		dataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
		dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
		this.gvRltsGnl.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle7;
		this.gvRltsGnl.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
		this.gvRltsGnl.ContextMenuStrip = this.cmsRltGV;
		dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
		dataGridViewCellStyle8.BackColor = System.Drawing.SystemColors.Window;
		dataGridViewCellStyle8.Font = new System.Drawing.Font("宋体", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		dataGridViewCellStyle8.ForeColor = System.Drawing.Color.Black;
		dataGridViewCellStyle8.SelectionBackColor = System.Drawing.SystemColors.Highlight;
		dataGridViewCellStyle8.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
		dataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
		this.gvRltsGnl.DefaultCellStyle = dataGridViewCellStyle8;
		this.gvRltsGnl.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
		this.gvRltsGnl.Location = new System.Drawing.Point(30, 65);
		this.gvRltsGnl.Name = "gvRltsGnl";
		dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
		dataGridViewCellStyle9.BackColor = System.Drawing.SystemColors.Control;
		dataGridViewCellStyle9.Font = new System.Drawing.Font("宋体", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		dataGridViewCellStyle9.ForeColor = System.Drawing.SystemColors.WindowText;
		dataGridViewCellStyle9.SelectionBackColor = System.Drawing.SystemColors.Highlight;
		dataGridViewCellStyle9.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
		dataGridViewCellStyle9.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
		this.gvRltsGnl.RowHeadersDefaultCellStyle = dataGridViewCellStyle9;
		this.gvRltsGnl.RowHeadersWidth = 25;
		this.gvRltsGnl.RowTemplate.Height = 16;
		this.gvRltsGnl.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
		this.gvRltsGnl.ShowCellToolTips = false;
		this.gvRltsGnl.Size = new System.Drawing.Size(149, 32);
		this.gvRltsGnl.TabIndex = 4;
		this.gvRltsGnl.CellBeginEdit += new System.Windows.Forms.DataGridViewCellCancelEventHandler(gvRltsGnl_CellBeginEdit);
		this.gvRltsGnl.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(gvRltsGnl_CellDoubleClick);
		this.gvRltsGnl.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(gvRltsGnl_CellEndEdit);
		this.gvRltsGnl.CellMouseDown += new System.Windows.Forms.DataGridViewCellMouseEventHandler(gvRltsGnl_CellMouseDown);
		this.gvRltsGnl.SelectionChanged += new System.EventHandler(gvRltsGnl_SelectionChanged);
		this.lbRltExpress.BackColor = System.Drawing.Color.Transparent;
		this.lbRltExpress.Dock = System.Windows.Forms.DockStyle.Top;
		this.lbRltExpress.Location = new System.Drawing.Point(0, 0);
		this.lbRltExpress.Name = "lbRltExpress";
		this.lbRltExpress.Size = new System.Drawing.Size(583, 0);
		this.lbRltExpress.TabIndex = 5;
		this.lbRltExpress.Text = "[]";
		this.lbRltExpress.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
		this.pnlRltsControl.AutoScroll = true;
		this.pnlRltsControl.BackColor = System.Drawing.Color.Transparent;
		this.pnlRltsControl.Controls.Add(this.pnlcu);
		this.pnlRltsControl.Dock = System.Windows.Forms.DockStyle.Right;
		this.pnlRltsControl.Location = new System.Drawing.Point(583, 0);
		this.pnlRltsControl.Name = "pnlRltsControl";
		this.pnlRltsControl.Size = new System.Drawing.Size(391, 291);
		this.pnlRltsControl.TabIndex = 1;
		this.pnlcu.Controls.Add(this.gbcuUncalPeaks);
		this.pnlcu.Controls.Add(this.gbCalibration);
		this.pnlcu.Controls.Add(this.cbcuCalcu);
		this.pnlcu.Controls.Add(this.lbcuDilution);
		this.pnlcu.Controls.Add(this.cbRltCombine);
		this.pnlcu.Controls.Add(this.lbcuInjVolume);
		this.pnlcu.Controls.Add(this.tbcuDilution);
		this.pnlcu.Controls.Add(this.tbcuInjVolume);
		this.pnlcu.Controls.Add(this.tbcuIstdAmount);
		this.pnlcu.Controls.Add(this.lbcuIstdAmount);
		this.pnlcu.Controls.Add(this.tbcuAmount);
		this.pnlcu.Controls.Add(this.lbcuAmount);
		this.pnlcu.Controls.Add(this.gbcuScale);
		this.pnlcu.Controls.Add(this.gbcuRltTableReport);
		this.pnlcu.Controls.Add(this.lbcuCalcu);
		this.pnlcu.Location = new System.Drawing.Point(3, 5);
		this.pnlcu.Name = "pnlcu";
		this.pnlcu.Size = new System.Drawing.Size(373, 266);
		this.pnlcu.TabIndex = 3;
		this.gbcuUncalPeaks.Controls.Add(this.cbcuUncalBase);
		this.gbcuUncalPeaks.Controls.Add(this.lbcuUncalAmtRespFU);
		this.gbcuUncalPeaks.Controls.Add(this.lbcuUncalAmtRespF);
		this.gbcuUncalPeaks.Controls.Add(this.lbcuUncalBase);
		this.gbcuUncalPeaks.Controls.Add(this.tbcuUncalAmtRespF);
		this.gbcuUncalPeaks.Location = new System.Drawing.Point(172, 86);
		this.gbcuUncalPeaks.Name = "gbcuUncalPeaks";
		this.gbcuUncalPeaks.Size = new System.Drawing.Size(198, 62);
		this.gbcuUncalPeaks.TabIndex = 3;
		this.gbcuUncalPeaks.TabStop = false;
		this.gbcuUncalPeaks.Text = "未识别峰";
		this.cbcuUncalBase.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.cbcuUncalBase.FormattingEnabled = true;
		this.cbcuUncalBase.ItemExtString = "";
		this.cbcuUncalBase.Location = new System.Drawing.Point(81, 13);
		this.cbcuUncalBase.Name = "cbcuUncalBase";
		this.cbcuUncalBase.Size = new System.Drawing.Size(82, 20);
		this.cbcuUncalBase.TabIndex = 2;
		this.cbcuUncalBase.SelectionChangeCommitted += new System.EventHandler(cbcuCalcu_SelectionChangeCommitted);
		this.lbcuUncalAmtRespFU.AutoSize = true;
		this.lbcuUncalAmtRespFU.Location = new System.Drawing.Point(131, 41);
		this.lbcuUncalAmtRespFU.Name = "lbcuUncalAmtRespFU";
		this.lbcuUncalAmtRespFU.Size = new System.Drawing.Size(65, 12);
		this.lbcuUncalAmtRespFU.TabIndex = 1;
		this.lbcuUncalAmtRespFU.Text = "[Amt/Resp]";
		this.lbcuUncalAmtRespF.AutoSize = true;
		this.lbcuUncalAmtRespF.Location = new System.Drawing.Point(6, 40);
		this.lbcuUncalAmtRespF.Name = "lbcuUncalAmtRespF";
		this.lbcuUncalAmtRespF.Size = new System.Drawing.Size(65, 12);
		this.lbcuUncalAmtRespF.TabIndex = 1;
		this.lbcuUncalAmtRespF.Text = "未识别因子";
		this.lbcuUncalBase.AutoSize = true;
		this.lbcuUncalBase.Location = new System.Drawing.Point(6, 16);
		this.lbcuUncalBase.Name = "lbcuUncalBase";
		this.lbcuUncalBase.Size = new System.Drawing.Size(65, 12);
		this.lbcuUncalBase.TabIndex = 1;
		this.lbcuUncalBase.Text = "未识别响应";
		this.tbcuUncalAmtRespF.Location = new System.Drawing.Point(81, 36);
		this.tbcuUncalAmtRespF.Name = "tbcuUncalAmtRespF";
		this.tbcuUncalAmtRespF.Size = new System.Drawing.Size(48, 21);
		this.tbcuUncalAmtRespF.TabIndex = 0;
		this.tbcuUncalAmtRespF.KeyPress += new System.Windows.Forms.KeyPressEventHandler(tbcuScaleFactor_KeyPress);
		this.gbCalibration.Controls.Add(this.btnclbView);
		this.gbCalibration.Controls.Add(this.btnclbNone);
		this.gbCalibration.Controls.Add(this.btnclbSet);
		this.gbCalibration.Controls.Add(this.tbclb);
		this.gbCalibration.Location = new System.Drawing.Point(6, 6);
		this.gbCalibration.Name = "gbCalibration";
		this.gbCalibration.Size = new System.Drawing.Size(206, 76);
		this.gbCalibration.TabIndex = 0;
		this.gbCalibration.TabStop = false;
		this.gbCalibration.Text = "校正文件[峰表]";
		this.btnclbView.Location = new System.Drawing.Point(140, 47);
		this.btnclbView.Name = "btnclbView";
		this.btnclbView.Size = new System.Drawing.Size(60, 23);
		this.btnclbView.TabIndex = 1;
		this.btnclbView.UseVisualStyleBackColor = true;
		this.btnclbView.Click += new System.EventHandler(btnclbSet_Click);
		this.btnclbNone.Location = new System.Drawing.Point(74, 47);
		this.btnclbNone.Name = "btnclbNone";
		this.btnclbNone.Size = new System.Drawing.Size(60, 23);
		this.btnclbNone.TabIndex = 1;
		this.btnclbNone.Text = "置空";
		this.btnclbNone.UseVisualStyleBackColor = true;
		this.btnclbNone.Click += new System.EventHandler(btnclbSet_Click);
		this.btnclbSet.Location = new System.Drawing.Point(8, 47);
		this.btnclbSet.Name = "btnclbSet";
		this.btnclbSet.Size = new System.Drawing.Size(60, 23);
		this.btnclbSet.TabIndex = 1;
		this.btnclbSet.Text = "设置...";
		this.btnclbSet.UseVisualStyleBackColor = true;
		this.btnclbSet.Click += new System.EventHandler(btnclbSet_Click);
		this.tbclb.Location = new System.Drawing.Point(6, 20);
		this.tbclb.Name = "tbclb";
		this.tbclb.ReadOnly = true;
		this.tbclb.Size = new System.Drawing.Size(193, 21);
		this.tbclb.TabIndex = 0;
		this.cbcuCalcu.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.cbcuCalcu.FormattingEnabled = true;
		this.cbcuCalcu.ItemExtString = "";
		this.cbcuCalcu.Location = new System.Drawing.Point(217, 21);
		this.cbcuCalcu.Name = "cbcuCalcu";
		this.cbcuCalcu.Size = new System.Drawing.Size(82, 20);
		this.cbcuCalcu.TabIndex = 4;
		this.cbcuCalcu.SelectionChangeCommitted += new System.EventHandler(cbcuCalcu_SelectionChangeCommitted);
		this.lbcuDilution.AutoSize = true;
		this.lbcuDilution.Location = new System.Drawing.Point(178, 230);
		this.lbcuDilution.Name = "lbcuDilution";
		this.lbcuDilution.Size = new System.Drawing.Size(29, 12);
		this.lbcuDilution.TabIndex = 1;
		this.lbcuDilution.Text = "稀释";
		this.cbRltCombine.AutoSize = true;
		this.cbRltCombine.Location = new System.Drawing.Point(217, 47);
		this.cbRltCombine.Name = "cbRltCombine";
		this.cbRltCombine.Size = new System.Drawing.Size(15, 14);
		this.cbRltCombine.TabIndex = 0;
		this.cbRltCombine.UseVisualStyleBackColor = true;
		this.cbRltCombine.Click += new System.EventHandler(cbRltCombine_Click);
		this.lbcuInjVolume.AutoSize = true;
		this.lbcuInjVolume.Location = new System.Drawing.Point(179, 206);
		this.lbcuInjVolume.Name = "lbcuInjVolume";
		this.lbcuInjVolume.Size = new System.Drawing.Size(53, 12);
		this.lbcuInjVolume.TabIndex = 1;
		this.lbcuInjVolume.Text = "进样体积";
		this.tbcuDilution.Location = new System.Drawing.Point(253, 226);
		this.tbcuDilution.Name = "tbcuDilution";
		this.tbcuDilution.Size = new System.Drawing.Size(76, 21);
		this.tbcuDilution.TabIndex = 0;
		this.tbcuDilution.KeyPress += new System.Windows.Forms.KeyPressEventHandler(tbcuScaleFactor_KeyPress);
		this.tbcuInjVolume.Location = new System.Drawing.Point(253, 202);
		this.tbcuInjVolume.Name = "tbcuInjVolume";
		this.tbcuInjVolume.Size = new System.Drawing.Size(76, 21);
		this.tbcuInjVolume.TabIndex = 0;
		this.tbcuInjVolume.KeyPress += new System.Windows.Forms.KeyPressEventHandler(tbcuScaleFactor_KeyPress);
		this.tbcuIstdAmount.Location = new System.Drawing.Point(253, 178);
		this.tbcuIstdAmount.Name = "tbcuIstdAmount";
		this.tbcuIstdAmount.Size = new System.Drawing.Size(76, 21);
		this.tbcuIstdAmount.TabIndex = 0;
		this.tbcuIstdAmount.KeyPress += new System.Windows.Forms.KeyPressEventHandler(tbcuScaleFactor_KeyPress);
		this.lbcuIstdAmount.AutoSize = true;
		this.lbcuIstdAmount.Location = new System.Drawing.Point(179, 182);
		this.lbcuIstdAmount.Name = "lbcuIstdAmount";
		this.lbcuIstdAmount.Size = new System.Drawing.Size(0, 12);
		this.lbcuIstdAmount.TabIndex = 1;
		this.tbcuAmount.Location = new System.Drawing.Point(253, 154);
		this.tbcuAmount.Name = "tbcuAmount";
		this.tbcuAmount.Size = new System.Drawing.Size(76, 21);
		this.tbcuAmount.TabIndex = 0;
		this.tbcuAmount.KeyPress += new System.Windows.Forms.KeyPressEventHandler(tbcuScaleFactor_KeyPress);
		this.lbcuAmount.AutoSize = true;
		this.lbcuAmount.Location = new System.Drawing.Point(179, 158);
		this.lbcuAmount.Name = "lbcuAmount";
		this.lbcuAmount.Size = new System.Drawing.Size(29, 12);
		this.lbcuAmount.TabIndex = 1;
		this.lbcuAmount.Text = "数量";
		this.gbcuScale.Controls.Add(this.cbcuUseScaleFactor);
		this.gbcuScale.Controls.Add(this.lbcuUnitAfterScale);
		this.gbcuScale.Controls.Add(this.lbcuScaleFactor);
		this.gbcuScale.Controls.Add(this.tbcuUnitAfterScale);
		this.gbcuScale.Controls.Add(this.tbcuScaleFactor);
		this.gbcuScale.Location = new System.Drawing.Point(6, 182);
		this.gbcuScale.Name = "gbcuScale";
		this.gbcuScale.Size = new System.Drawing.Size(160, 68);
		this.gbcuScale.TabIndex = 1;
		this.gbcuScale.TabStop = false;
		this.cbcuUseScaleFactor.AutoSize = true;
		this.cbcuUseScaleFactor.Location = new System.Drawing.Point(10, 2);
		this.cbcuUseScaleFactor.Name = "cbcuUseScaleFactor";
		this.cbcuUseScaleFactor.Size = new System.Drawing.Size(96, 16);
		this.cbcuUseScaleFactor.TabIndex = 0;
		this.cbcuUseScaleFactor.Text = "使用缩放因子";
		this.cbcuUseScaleFactor.UseVisualStyleBackColor = true;
		this.cbcuUseScaleFactor.Click += new System.EventHandler(cbcuHideISTDPeak_Click);
		this.lbcuUnitAfterScale.AutoSize = true;
		this.lbcuUnitAfterScale.Location = new System.Drawing.Point(8, 47);
		this.lbcuUnitAfterScale.Name = "lbcuUnitAfterScale";
		this.lbcuUnitAfterScale.Size = new System.Drawing.Size(65, 12);
		this.lbcuUnitAfterScale.TabIndex = 1;
		this.lbcuUnitAfterScale.Text = "缩放后单位";
		this.lbcuScaleFactor.AutoSize = true;
		this.lbcuScaleFactor.Location = new System.Drawing.Point(8, 24);
		this.lbcuScaleFactor.Name = "lbcuScaleFactor";
		this.lbcuScaleFactor.Size = new System.Drawing.Size(53, 12);
		this.lbcuScaleFactor.TabIndex = 1;
		this.lbcuScaleFactor.Text = "缩放因子";
		this.tbcuUnitAfterScale.Location = new System.Drawing.Point(84, 42);
		this.tbcuUnitAfterScale.Name = "tbcuUnitAfterScale";
		this.tbcuUnitAfterScale.Size = new System.Drawing.Size(70, 21);
		this.tbcuUnitAfterScale.TabIndex = 0;
		this.tbcuUnitAfterScale.Text = "ul";
		this.tbcuUnitAfterScale.KeyPress += new System.Windows.Forms.KeyPressEventHandler(tbcuScaleFactor_KeyPress);
		this.tbcuScaleFactor.Location = new System.Drawing.Point(84, 18);
		this.tbcuScaleFactor.Name = "tbcuScaleFactor";
		this.tbcuScaleFactor.Size = new System.Drawing.Size(70, 21);
		this.tbcuScaleFactor.TabIndex = 0;
		this.tbcuScaleFactor.KeyPress += new System.Windows.Forms.KeyPressEventHandler(tbcuScaleFactor_KeyPress);
		this.gbcuRltTableReport.Controls.Add(this.rbcuCaliPeaks);
		this.gbcuRltTableReport.Controls.Add(this.rbcuIdentifiedPeaks);
		this.gbcuRltTableReport.Controls.Add(this.rbcuAllDetectedPeaks);
		this.gbcuRltTableReport.Controls.Add(this.cbcuHideISTDPeak);
		this.gbcuRltTableReport.Location = new System.Drawing.Point(6, 86);
		this.gbcuRltTableReport.Name = "gbcuRltTableReport";
		this.gbcuRltTableReport.Size = new System.Drawing.Size(160, 92);
		this.gbcuRltTableReport.TabIndex = 0;
		this.gbcuRltTableReport.TabStop = false;
		this.gbcuRltTableReport.Text = "结果表报告";
		this.rbcuCaliPeaks.AutoSize = true;
		this.rbcuCaliPeaks.Location = new System.Drawing.Point(10, 72);
		this.rbcuCaliPeaks.Name = "rbcuCaliPeaks";
		this.rbcuCaliPeaks.Size = new System.Drawing.Size(83, 16);
		this.rbcuCaliPeaks.TabIndex = 1;
		this.rbcuCaliPeaks.Text = "所有校正峰";
		this.rbcuCaliPeaks.UseVisualStyleBackColor = true;
		this.rbcuCaliPeaks.Click += new System.EventHandler(cbcuHideISTDPeak_Click);
		this.rbcuIdentifiedPeaks.AutoSize = true;
		this.rbcuIdentifiedPeaks.Location = new System.Drawing.Point(10, 54);
		this.rbcuIdentifiedPeaks.Name = "rbcuIdentifiedPeaks";
		this.rbcuIdentifiedPeaks.Size = new System.Drawing.Size(83, 16);
		this.rbcuIdentifiedPeaks.TabIndex = 1;
		this.rbcuIdentifiedPeaks.Text = "所有识别峰";
		this.rbcuIdentifiedPeaks.UseVisualStyleBackColor = true;
		this.rbcuIdentifiedPeaks.Click += new System.EventHandler(cbcuHideISTDPeak_Click);
		this.rbcuAllDetectedPeaks.AutoSize = true;
		this.rbcuAllDetectedPeaks.Checked = true;
		this.rbcuAllDetectedPeaks.Location = new System.Drawing.Point(10, 36);
		this.rbcuAllDetectedPeaks.Name = "rbcuAllDetectedPeaks";
		this.rbcuAllDetectedPeaks.Size = new System.Drawing.Size(83, 16);
		this.rbcuAllDetectedPeaks.TabIndex = 1;
		this.rbcuAllDetectedPeaks.TabStop = true;
		this.rbcuAllDetectedPeaks.Text = "所有检测峰";
		this.rbcuAllDetectedPeaks.UseVisualStyleBackColor = true;
		this.rbcuAllDetectedPeaks.Click += new System.EventHandler(cbcuHideISTDPeak_Click);
		this.cbcuHideISTDPeak.AutoSize = true;
		this.cbcuHideISTDPeak.Location = new System.Drawing.Point(10, 18);
		this.cbcuHideISTDPeak.Name = "cbcuHideISTDPeak";
		this.cbcuHideISTDPeak.Size = new System.Drawing.Size(84, 16);
		this.cbcuHideISTDPeak.TabIndex = 0;
		this.cbcuHideISTDPeak.Text = "隐藏内标峰";
		this.cbcuHideISTDPeak.UseVisualStyleBackColor = true;
		this.cbcuHideISTDPeak.Click += new System.EventHandler(cbcuHideISTDPeak_Click);
		this.lbcuCalcu.AutoSize = true;
		this.lbcuCalcu.Location = new System.Drawing.Point(218, 6);
		this.lbcuCalcu.Name = "lbcuCalcu";
		this.lbcuCalcu.Size = new System.Drawing.Size(29, 12);
		this.lbcuCalcu.TabIndex = 1;
		this.lbcuCalcu.Text = "计算";
		this.tpSummary.Controls.Add(this.gvSummary);
		this.tpSummary.Location = new System.Drawing.Point(4, 23);
		this.tpSummary.Name = "tpSummary";
		this.tpSummary.Size = new System.Drawing.Size(974, 291);
		this.tpSummary.TabIndex = 1;
		this.tpSummary.Text = "总结";
		this.tpSummary.UseVisualStyleBackColor = true;
		this.gvSummary.AllowUserToAddRows = false;
		this.gvSummary.AllowUserToDeleteRows = false;
		this.gvSummary.AllowUserToResizeRows = false;
		this.gvSummary.BackgroundColor = System.Drawing.Color.AliceBlue;
		this.gvSummary.CharacterHeaderColor = System.Drawing.Color.Black;
		dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
		dataGridViewCellStyle10.BackColor = System.Drawing.SystemColors.Control;
		dataGridViewCellStyle10.Font = new System.Drawing.Font("宋体", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		dataGridViewCellStyle10.ForeColor = System.Drawing.SystemColors.WindowText;
		dataGridViewCellStyle10.SelectionBackColor = System.Drawing.SystemColors.Highlight;
		dataGridViewCellStyle10.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
		dataGridViewCellStyle10.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
		this.gvSummary.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle10;
		this.gvSummary.ColumnHeadersHeight = 48;
		this.gvSummary.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
		this.gvSummary.ContextMenuStrip = this.cmsSummary;
		dataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
		dataGridViewCellStyle11.BackColor = System.Drawing.SystemColors.Window;
		dataGridViewCellStyle11.Font = new System.Drawing.Font("宋体", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		dataGridViewCellStyle11.ForeColor = System.Drawing.SystemColors.ControlText;
		dataGridViewCellStyle11.SelectionBackColor = System.Drawing.SystemColors.Highlight;
		dataGridViewCellStyle11.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
		dataGridViewCellStyle11.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
		this.gvSummary.DefaultCellStyle = dataGridViewCellStyle11;
		this.gvSummary.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
		this.gvSummary.Location = new System.Drawing.Point(80, 49);
		this.gvSummary.Name = "gvSummary";
		this.gvSummary.ReadOnly = true;
		dataGridViewCellStyle12.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
		dataGridViewCellStyle12.BackColor = System.Drawing.SystemColors.Control;
		dataGridViewCellStyle12.Font = new System.Drawing.Font("宋体", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		dataGridViewCellStyle12.ForeColor = System.Drawing.SystemColors.WindowText;
		dataGridViewCellStyle12.SelectionBackColor = System.Drawing.SystemColors.Highlight;
		dataGridViewCellStyle12.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
		dataGridViewCellStyle12.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
		this.gvSummary.RowHeadersDefaultCellStyle = dataGridViewCellStyle12;
		this.gvSummary.RowHeadersWidth = 25;
		this.gvSummary.RowTemplate.Height = 16;
		this.gvSummary.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
		this.gvSummary.ShowCellToolTips = false;
		this.gvSummary.Size = new System.Drawing.Size(240, 150);
		this.gvSummary.TabIndex = 0;
		this.cmsSummary.Items.AddRange(new System.Windows.Forms.ToolStripItem[4] { this.mismyColumnsSetup, this.mismyRestoreDftColumns, this.toolStripSeparator15, this.mismySmyOpt });
		this.cmsSummary.Name = "cmsSummary";
		this.cmsSummary.ShowImageMargin = false;
		this.cmsSummary.Size = new System.Drawing.Size(136, 76);
		this.mismyColumnsSetup.Name = "mismyColumnsSetup";
		this.mismyColumnsSetup.Size = new System.Drawing.Size(135, 22);
		this.mismyColumnsSetup.Text = "列设置...";
		this.mismyColumnsSetup.Click += new System.EventHandler(mismySmyOpt_Click);
		this.mismyRestoreDftColumns.Name = "mismyRestoreDftColumns";
		this.mismyRestoreDftColumns.Size = new System.Drawing.Size(135, 22);
		this.mismyRestoreDftColumns.Text = "恢复默认列设置";
		this.mismyRestoreDftColumns.Click += new System.EventHandler(mismySmyOpt_Click);
		this.toolStripSeparator15.Name = "toolStripSeparator15";
		this.toolStripSeparator15.Size = new System.Drawing.Size(132, 6);
		this.mismySmyOpt.Name = "mismySmyOpt";
		this.mismySmyOpt.Size = new System.Drawing.Size(135, 22);
		this.mismySmyOpt.Text = "总结选项...";
		this.mismySmyOpt.Click += new System.EventHandler(mismySmyOpt_Click);
		this.tpPerformance.Controls.Add(this.gvPerformFrom50);
		this.tpPerformance.Controls.Add(this.gvPerformStatic);
		this.tpPerformance.Controls.Add(this.pnlpfmControl);
		this.tpPerformance.Location = new System.Drawing.Point(4, 23);
		this.tpPerformance.Name = "tpPerformance";
		this.tpPerformance.Size = new System.Drawing.Size(974, 291);
		this.tpPerformance.TabIndex = 2;
		this.tpPerformance.Text = "柱效";
		this.tpPerformance.UseVisualStyleBackColor = true;
		this.gvPerformFrom50.AllowUserToAddRows = false;
		this.gvPerformFrom50.AllowUserToDeleteRows = false;
		this.gvPerformFrom50.AllowUserToResizeRows = false;
		this.gvPerformFrom50.BackgroundColor = System.Drawing.Color.AliceBlue;
		this.gvPerformFrom50.CharacterHeaderColor = System.Drawing.Color.Black;
		dataGridViewCellStyle13.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
		dataGridViewCellStyle13.BackColor = System.Drawing.SystemColors.Control;
		dataGridViewCellStyle13.Font = new System.Drawing.Font("宋体", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		dataGridViewCellStyle13.ForeColor = System.Drawing.SystemColors.WindowText;
		dataGridViewCellStyle13.SelectionBackColor = System.Drawing.SystemColors.Highlight;
		dataGridViewCellStyle13.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
		dataGridViewCellStyle13.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
		this.gvPerformFrom50.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle13;
		this.gvPerformFrom50.ColumnHeadersHeight = 32;
		this.gvPerformFrom50.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
		this.gvPerformFrom50.ContextMenuStrip = this.cmsPerformance;
		dataGridViewCellStyle14.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
		dataGridViewCellStyle14.BackColor = System.Drawing.SystemColors.Window;
		dataGridViewCellStyle14.Font = new System.Drawing.Font("宋体", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		dataGridViewCellStyle14.ForeColor = System.Drawing.SystemColors.ControlText;
		dataGridViewCellStyle14.SelectionBackColor = System.Drawing.SystemColors.Highlight;
		dataGridViewCellStyle14.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
		dataGridViewCellStyle14.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
		this.gvPerformFrom50.DefaultCellStyle = dataGridViewCellStyle14;
		this.gvPerformFrom50.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
		this.gvPerformFrom50.Location = new System.Drawing.Point(251, 67);
		this.gvPerformFrom50.Name = "gvPerformFrom50";
		this.gvPerformFrom50.ReadOnly = true;
		dataGridViewCellStyle15.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
		dataGridViewCellStyle15.BackColor = System.Drawing.SystemColors.Control;
		dataGridViewCellStyle15.Font = new System.Drawing.Font("宋体", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		dataGridViewCellStyle15.ForeColor = System.Drawing.SystemColors.WindowText;
		dataGridViewCellStyle15.SelectionBackColor = System.Drawing.SystemColors.Highlight;
		dataGridViewCellStyle15.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
		dataGridViewCellStyle15.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
		this.gvPerformFrom50.RowHeadersDefaultCellStyle = dataGridViewCellStyle15;
		this.gvPerformFrom50.RowHeadersWidth = 25;
		this.gvPerformFrom50.RowTemplate.Height = 16;
		this.gvPerformFrom50.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
		this.gvPerformFrom50.ShowCellToolTips = false;
		this.gvPerformFrom50.Size = new System.Drawing.Size(160, 74);
		this.gvPerformFrom50.TabIndex = 2;
		this.gvPerformFrom50.SelectionChanged += new System.EventHandler(gvPerformStatic_SelectionChanged);
		this.cmsPerformance.Items.AddRange(new System.Windows.Forms.ToolStripItem[2] { this.mipfmColumnsSetup, this.mipfmRestoreDftColumns });
		this.cmsPerformance.Name = "cmsPerformance";
		this.cmsPerformance.ShowImageMargin = false;
		this.cmsPerformance.Size = new System.Drawing.Size(136, 48);
		this.mipfmColumnsSetup.Name = "mipfmColumnsSetup";
		this.mipfmColumnsSetup.Size = new System.Drawing.Size(135, 22);
		this.mipfmColumnsSetup.Text = "列设置...";
		this.mipfmColumnsSetup.Click += new System.EventHandler(mipfmRestoreDftColumns_Click);
		this.mipfmRestoreDftColumns.Name = "mipfmRestoreDftColumns";
		this.mipfmRestoreDftColumns.Size = new System.Drawing.Size(135, 22);
		this.mipfmRestoreDftColumns.Text = "恢复默认列设置";
		this.mipfmRestoreDftColumns.Click += new System.EventHandler(mipfmRestoreDftColumns_Click);
		this.gvPerformStatic.AllowUserToAddRows = false;
		this.gvPerformStatic.AllowUserToDeleteRows = false;
		this.gvPerformStatic.AllowUserToResizeRows = false;
		this.gvPerformStatic.BackgroundColor = System.Drawing.Color.AliceBlue;
		this.gvPerformStatic.CharacterHeaderColor = System.Drawing.Color.Black;
		dataGridViewCellStyle16.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
		dataGridViewCellStyle16.BackColor = System.Drawing.SystemColors.Control;
		dataGridViewCellStyle16.Font = new System.Drawing.Font("宋体", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		dataGridViewCellStyle16.ForeColor = System.Drawing.SystemColors.WindowText;
		dataGridViewCellStyle16.SelectionBackColor = System.Drawing.SystemColors.Highlight;
		dataGridViewCellStyle16.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
		dataGridViewCellStyle16.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
		this.gvPerformStatic.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle16;
		this.gvPerformStatic.ColumnHeadersHeight = 32;
		this.gvPerformStatic.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
		this.gvPerformStatic.ContextMenuStrip = this.cmsPerformance;
		dataGridViewCellStyle17.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
		dataGridViewCellStyle17.BackColor = System.Drawing.SystemColors.Window;
		dataGridViewCellStyle17.Font = new System.Drawing.Font("宋体", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		dataGridViewCellStyle17.ForeColor = System.Drawing.SystemColors.ControlText;
		dataGridViewCellStyle17.SelectionBackColor = System.Drawing.SystemColors.Highlight;
		dataGridViewCellStyle17.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
		dataGridViewCellStyle17.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
		this.gvPerformStatic.DefaultCellStyle = dataGridViewCellStyle17;
		this.gvPerformStatic.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
		this.gvPerformStatic.Location = new System.Drawing.Point(34, 67);
		this.gvPerformStatic.Name = "gvPerformStatic";
		this.gvPerformStatic.ReadOnly = true;
		dataGridViewCellStyle18.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
		dataGridViewCellStyle18.BackColor = System.Drawing.SystemColors.Control;
		dataGridViewCellStyle18.Font = new System.Drawing.Font("宋体", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		dataGridViewCellStyle18.ForeColor = System.Drawing.SystemColors.WindowText;
		dataGridViewCellStyle18.SelectionBackColor = System.Drawing.SystemColors.Highlight;
		dataGridViewCellStyle18.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
		dataGridViewCellStyle18.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
		this.gvPerformStatic.RowHeadersDefaultCellStyle = dataGridViewCellStyle18;
		this.gvPerformStatic.RowHeadersWidth = 25;
		this.gvPerformStatic.RowTemplate.Height = 16;
		this.gvPerformStatic.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
		this.gvPerformStatic.ShowCellToolTips = false;
		this.gvPerformStatic.Size = new System.Drawing.Size(160, 74);
		this.gvPerformStatic.TabIndex = 2;
		this.gvPerformStatic.SelectionChanged += new System.EventHandler(gvPerformStatic_SelectionChanged);
		this.pnlpfmControl.AutoScroll = true;
		this.pnlpfmControl.Controls.Add(this.gbpfmColumnCalcu);
		this.pnlpfmControl.Controls.Add(this.tbpfmColumnLength);
		this.pnlpfmControl.Controls.Add(this.lbpfmColumnLengthU);
		this.pnlpfmControl.Controls.Add(this.tbpfmColumnUT);
		this.pnlpfmControl.Controls.Add(this.lbpfmColumnLength);
		this.pnlpfmControl.Controls.Add(this.lbpfmUnretainedPeakU);
		this.pnlpfmControl.Controls.Add(this.lbpfmColumnUT);
		this.pnlpfmControl.Dock = System.Windows.Forms.DockStyle.Right;
		this.pnlpfmControl.Location = new System.Drawing.Point(758, 0);
		this.pnlpfmControl.Name = "pnlpfmControl";
		this.pnlpfmControl.Size = new System.Drawing.Size(216, 291);
		this.pnlpfmControl.TabIndex = 0;
		this.gbpfmColumnCalcu.Controls.Add(this.rbpfmFrom50per);
		this.gbpfmColumnCalcu.Controls.Add(this.rbpfmStatistical);
		this.gbpfmColumnCalcu.Location = new System.Drawing.Point(19, 83);
		this.gbpfmColumnCalcu.Name = "gbpfmColumnCalcu";
		this.gbpfmColumnCalcu.Size = new System.Drawing.Size(149, 66);
		this.gbpfmColumnCalcu.TabIndex = 2;
		this.gbpfmColumnCalcu.TabStop = false;
		this.gbpfmColumnCalcu.Text = "柱效计算";
		this.rbpfmFrom50per.AutoSize = true;
		this.rbpfmFrom50per.Location = new System.Drawing.Point(6, 42);
		this.rbpfmFrom50per.Name = "rbpfmFrom50per";
		this.rbpfmFrom50per.Size = new System.Drawing.Size(77, 16);
		this.rbpfmFrom50per.TabIndex = 0;
		this.rbpfmFrom50per.TabStop = true;
		this.rbpfmFrom50per.Text = "50%宽起始";
		this.rbpfmFrom50per.UseVisualStyleBackColor = true;
		this.rbpfmFrom50per.Click += new System.EventHandler(rbpfmStatistical_Click);
		this.rbpfmStatistical.AutoSize = true;
		this.rbpfmStatistical.Enabled = false;
		this.rbpfmStatistical.Location = new System.Drawing.Point(6, 20);
		this.rbpfmStatistical.Name = "rbpfmStatistical";
		this.rbpfmStatistical.Size = new System.Drawing.Size(71, 16);
		this.rbpfmStatistical.TabIndex = 0;
		this.rbpfmStatistical.TabStop = true;
		this.rbpfmStatistical.Text = "静态时间";
		this.rbpfmStatistical.UseVisualStyleBackColor = true;
		this.rbpfmStatistical.Click += new System.EventHandler(rbpfmStatistical_Click);
		this.tbpfmColumnLength.Location = new System.Drawing.Point(84, 45);
		this.tbpfmColumnLength.Name = "tbpfmColumnLength";
		this.tbpfmColumnLength.Size = new System.Drawing.Size(67, 21);
		this.tbpfmColumnLength.TabIndex = 1;
		this.tbpfmColumnLength.KeyDown += new System.Windows.Forms.KeyEventHandler(tbpfmColumnUT_KeyDown);
		this.lbpfmColumnLengthU.AutoSize = true;
		this.lbpfmColumnLengthU.Location = new System.Drawing.Point(155, 50);
		this.lbpfmColumnLengthU.Name = "lbpfmColumnLengthU";
		this.lbpfmColumnLengthU.Size = new System.Drawing.Size(29, 12);
		this.lbpfmColumnLengthU.TabIndex = 0;
		this.lbpfmColumnLengthU.Text = "[mm]";
		this.tbpfmColumnUT.Location = new System.Drawing.Point(84, 18);
		this.tbpfmColumnUT.Name = "tbpfmColumnUT";
		this.tbpfmColumnUT.Size = new System.Drawing.Size(67, 21);
		this.tbpfmColumnUT.TabIndex = 1;
		this.tbpfmColumnUT.KeyDown += new System.Windows.Forms.KeyEventHandler(tbpfmColumnUT_KeyDown);
		this.lbpfmColumnLength.AutoSize = true;
		this.lbpfmColumnLength.Location = new System.Drawing.Point(6, 50);
		this.lbpfmColumnLength.Name = "lbpfmColumnLength";
		this.lbpfmColumnLength.Size = new System.Drawing.Size(29, 12);
		this.lbpfmColumnLength.TabIndex = 0;
		this.lbpfmColumnLength.Text = "柱长";
		this.lbpfmUnretainedPeakU.AutoSize = true;
		this.lbpfmUnretainedPeakU.Location = new System.Drawing.Point(155, 23);
		this.lbpfmUnretainedPeakU.Name = "lbpfmUnretainedPeakU";
		this.lbpfmUnretainedPeakU.Size = new System.Drawing.Size(35, 12);
		this.lbpfmUnretainedPeakU.TabIndex = 0;
		this.lbpfmUnretainedPeakU.Text = "[min]";
		this.lbpfmColumnUT.AutoSize = true;
		this.lbpfmColumnUT.Location = new System.Drawing.Point(6, 23);
		this.lbpfmColumnUT.Name = "lbpfmColumnUT";
		this.lbpfmColumnUT.Size = new System.Drawing.Size(77, 12);
		this.lbpfmColumnUT.TabIndex = 0;
		this.lbpfmColumnUT.Text = "非保留峰时间";
		this.tpIntegration.Controls.Add(this.gvInteg);
		this.tpIntegration.Location = new System.Drawing.Point(4, 23);
		this.tpIntegration.Name = "tpIntegration";
		this.tpIntegration.Size = new System.Drawing.Size(974, 291);
		this.tpIntegration.TabIndex = 3;
		this.tpIntegration.Text = "积分";
		this.tpIntegration.UseVisualStyleBackColor = true;
		this.gvInteg.AllowUserToAddRows = false;
		this.gvInteg.AllowUserToDeleteRows = false;
		this.gvInteg.AllowUserToResizeRows = false;
		this.gvInteg.BackgroundColor = System.Drawing.Color.AliceBlue;
		this.gvInteg.CharacterHeaderColor = System.Drawing.Color.Black;
		dataGridViewCellStyle19.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
		dataGridViewCellStyle19.BackColor = System.Drawing.SystemColors.Control;
		dataGridViewCellStyle19.Font = new System.Drawing.Font("宋体", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		dataGridViewCellStyle19.ForeColor = System.Drawing.SystemColors.WindowText;
		dataGridViewCellStyle19.SelectionBackColor = System.Drawing.SystemColors.Highlight;
		dataGridViewCellStyle19.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
		dataGridViewCellStyle19.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
		this.gvInteg.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle19;
		this.gvInteg.ColumnHeadersHeight = 32;
		this.gvInteg.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
		this.gvInteg.ContextMenuStrip = this.cmsInteg;
		dataGridViewCellStyle20.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
		dataGridViewCellStyle20.BackColor = System.Drawing.SystemColors.Window;
		dataGridViewCellStyle20.Font = new System.Drawing.Font("宋体", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		dataGridViewCellStyle20.ForeColor = System.Drawing.SystemColors.ControlText;
		dataGridViewCellStyle20.SelectionBackColor = System.Drawing.SystemColors.Highlight;
		dataGridViewCellStyle20.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
		dataGridViewCellStyle20.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
		this.gvInteg.DefaultCellStyle = dataGridViewCellStyle20;
		this.gvInteg.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
		this.gvInteg.Location = new System.Drawing.Point(48, 80);
		this.gvInteg.Name = "gvInteg";
		dataGridViewCellStyle21.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
		dataGridViewCellStyle21.BackColor = System.Drawing.SystemColors.Control;
		dataGridViewCellStyle21.Font = new System.Drawing.Font("宋体", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		dataGridViewCellStyle21.ForeColor = System.Drawing.SystemColors.WindowText;
		dataGridViewCellStyle21.SelectionBackColor = System.Drawing.SystemColors.Highlight;
		dataGridViewCellStyle21.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
		dataGridViewCellStyle21.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
		this.gvInteg.RowHeadersDefaultCellStyle = dataGridViewCellStyle21;
		this.gvInteg.RowHeadersWidth = 25;
		this.gvInteg.RowTemplate.Height = 16;
		this.gvInteg.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
		this.gvInteg.ShowCellToolTips = false;
		this.gvInteg.Size = new System.Drawing.Size(240, 150);
		this.gvInteg.TabIndex = 1;
		this.cmsInteg.Items.AddRange(new System.Windows.Forms.ToolStripItem[10] { this.miitgUndo, this.miitgRedo, this.toolStripSeparator30, this.miitgAppendRow, this.miitgInsertRow, this.miitgDelete, this.miitgReset, this.toolStripSeparator29, this.miitgCopy, this.miitgPaste });
		this.cmsInteg.Name = "cmsInteg";
		this.cmsInteg.Size = new System.Drawing.Size(113, 192);
		this.miitgUndo.Name = "miitgUndo";
		this.miitgUndo.Size = new System.Drawing.Size(112, 22);
		this.miitgUndo.Text = "撤销";
		this.miitgUndo.Click += new System.EventHandler(miitgPaste_Click);
		this.miitgRedo.Name = "miitgRedo";
		this.miitgRedo.Size = new System.Drawing.Size(112, 22);
		this.miitgRedo.Text = "恢复";
		this.miitgRedo.Click += new System.EventHandler(miitgPaste_Click);
		this.toolStripSeparator30.Name = "toolStripSeparator30";
		this.toolStripSeparator30.Size = new System.Drawing.Size(109, 6);
		this.miitgAppendRow.Name = "miitgAppendRow";
		this.miitgAppendRow.Size = new System.Drawing.Size(112, 22);
		this.miitgAppendRow.Text = "添加行";
		this.miitgAppendRow.Click += new System.EventHandler(miitgPaste_Click);
		this.miitgInsertRow.Name = "miitgInsertRow";
		this.miitgInsertRow.Size = new System.Drawing.Size(112, 22);
		this.miitgInsertRow.Text = "插入行";
		this.miitgInsertRow.Click += new System.EventHandler(miitgPaste_Click);
		this.miitgDelete.Name = "miitgDelete";
		this.miitgDelete.Size = new System.Drawing.Size(112, 22);
		this.miitgDelete.Text = "删除";
		this.miitgDelete.Click += new System.EventHandler(miitgPaste_Click);
		this.miitgReset.Name = "miitgReset";
		this.miitgReset.Size = new System.Drawing.Size(112, 22);
		this.miitgReset.Text = "重置";
		this.miitgReset.Click += new System.EventHandler(miitgPaste_Click);
		this.toolStripSeparator29.Name = "toolStripSeparator29";
		this.toolStripSeparator29.Size = new System.Drawing.Size(109, 6);
		this.miitgCopy.Name = "miitgCopy";
		this.miitgCopy.Size = new System.Drawing.Size(112, 22);
		this.miitgCopy.Text = "复制";
		this.miitgCopy.Click += new System.EventHandler(miitgPaste_Click);
		this.miitgPaste.Name = "miitgPaste";
		this.miitgPaste.Size = new System.Drawing.Size(112, 22);
		this.miitgPaste.Text = "粘帖";
		this.miitgPaste.Click += new System.EventHandler(miitgPaste_Click);
		this.tpMsmCdts.Controls.Add(this.tcMsmCdts);
		this.tpMsmCdts.Location = new System.Drawing.Point(4, 23);
		this.tpMsmCdts.Name = "tpMsmCdts";
		this.tpMsmCdts.Size = new System.Drawing.Size(974, 291);
		this.tpMsmCdts.TabIndex = 4;
		this.tpMsmCdts.Text = "测量条件";
		this.tpMsmCdts.UseVisualStyleBackColor = true;
		this.tcMsmCdts.Controls.Add(this.tpInstrument);
		this.tcMsmCdts.Controls.Add(this.tpPDAMethod);
		this.tcMsmCdts.Controls.Add(this.tpLC);
		this.tcMsmCdts.Controls.Add(this.tpGC);
		this.tcMsmCdts.ItemSize = new System.Drawing.Size(90, 19);
		this.tcMsmCdts.Location = new System.Drawing.Point(4, 8);
		this.tcMsmCdts.Name = "tcMsmCdts";
		this.tcMsmCdts.SelectedIndex = 0;
		this.tcMsmCdts.Size = new System.Drawing.Size(966, 258);
		this.tcMsmCdts.TabIndex = 0;
		this.tpInstrument.AutoScroll = true;
		this.tpInstrument.Controls.Add(this.gbinsAddSub);
		this.tpInstrument.Controls.Add(this.gbinsCdts);
		this.tpInstrument.Controls.Add(this.gbinsSampleIdt);
		this.tpInstrument.Controls.Add(this.gbinsAcqParas);
		this.tpInstrument.Location = new System.Drawing.Point(4, 23);
		this.tpInstrument.Name = "tpInstrument";
		this.tpInstrument.Size = new System.Drawing.Size(958, 231);
		this.tpInstrument.TabIndex = 0;
		this.tpInstrument.Text = "仪器";
		this.tpInstrument.UseVisualStyleBackColor = true;
		this.gbinsAddSub.Controls.Add(this.rbasSub);
		this.gbinsAddSub.Controls.Add(this.rbasAdd);
		this.gbinsAddSub.Controls.Add(this.cbasMatching);
		this.gbinsAddSub.Controls.Add(this.btnasSetChrom);
		this.gbinsAddSub.Controls.Add(this.btnasNoneChrom);
		this.gbinsAddSub.Controls.Add(this.lbasMatching);
		this.gbinsAddSub.Controls.Add(this.tbasChrom);
		this.gbinsAddSub.Controls.Add(this.lbasChrom);
		this.gbinsAddSub.Location = new System.Drawing.Point(305, 121);
		this.gbinsAddSub.Name = "gbinsAddSub";
		this.gbinsAddSub.Size = new System.Drawing.Size(283, 107);
		this.gbinsAddSub.TabIndex = 2;
		this.gbinsAddSub.TabStop = false;
		this.rbasSub.AutoSize = true;
		this.rbasSub.Location = new System.Drawing.Point(137, 15);
		this.rbasSub.Name = "rbasSub";
		this.rbasSub.Size = new System.Drawing.Size(14, 13);
		this.rbasSub.TabIndex = 3;
		this.rbasSub.TabStop = true;
		this.rbasSub.UseVisualStyleBackColor = true;
		this.rbasSub.Click += new System.EventHandler(rbasAdd_Click);
		this.rbasAdd.AutoSize = true;
		this.rbasAdd.Location = new System.Drawing.Point(81, 15);
		this.rbasAdd.Name = "rbasAdd";
		this.rbasAdd.Size = new System.Drawing.Size(14, 13);
		this.rbasAdd.TabIndex = 3;
		this.rbasAdd.TabStop = true;
		this.rbasAdd.UseVisualStyleBackColor = true;
		this.rbasAdd.Click += new System.EventHandler(rbasAdd_Click);
		this.cbasMatching.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.cbasMatching.FormattingEnabled = true;
		this.cbasMatching.ItemExtString = "";
		this.cbasMatching.Location = new System.Drawing.Point(81, 57);
		this.cbasMatching.Name = "cbasMatching";
		this.cbasMatching.Size = new System.Drawing.Size(166, 20);
		this.cbasMatching.TabIndex = 3;
		this.cbasMatching.SelectionChangeCommitted += new System.EventHandler(cbasMatching_SelectionChangeCommitted);
		this.btnasSetChrom.Location = new System.Drawing.Point(81, 80);
		this.btnasSetChrom.Name = "btnasSetChrom";
		this.btnasSetChrom.Size = new System.Drawing.Size(50, 23);
		this.btnasSetChrom.TabIndex = 2;
		this.btnasSetChrom.Text = "设置...";
		this.btnasSetChrom.UseVisualStyleBackColor = true;
		this.btnasSetChrom.Click += new System.EventHandler(btnasNoneChrom_Click);
		this.btnasNoneChrom.Location = new System.Drawing.Point(137, 80);
		this.btnasNoneChrom.Name = "btnasNoneChrom";
		this.btnasNoneChrom.Size = new System.Drawing.Size(50, 23);
		this.btnasNoneChrom.TabIndex = 2;
		this.btnasNoneChrom.Text = "置空";
		this.btnasNoneChrom.UseVisualStyleBackColor = true;
		this.btnasNoneChrom.Click += new System.EventHandler(btnasNoneChrom_Click);
		this.lbasMatching.AutoSize = true;
		this.lbasMatching.Location = new System.Drawing.Point(10, 60);
		this.lbasMatching.Name = "lbasMatching";
		this.lbasMatching.Size = new System.Drawing.Size(53, 12);
		this.lbasMatching.TabIndex = 0;
		this.lbasMatching.Text = "匹配方式";
		this.tbasChrom.Location = new System.Drawing.Point(81, 33);
		this.tbasChrom.Name = "tbasChrom";
		this.tbasChrom.ReadOnly = true;
		this.tbasChrom.Size = new System.Drawing.Size(192, 21);
		this.tbasChrom.TabIndex = 1;
		this.lbasChrom.AutoSize = true;
		this.lbasChrom.Location = new System.Drawing.Point(9, 37);
		this.lbasChrom.Name = "lbasChrom";
		this.lbasChrom.Size = new System.Drawing.Size(29, 12);
		this.lbasChrom.TabIndex = 0;
		this.lbasChrom.Text = "谱图";
		this.gbinsCdts.Controls.Add(this.tbmsmNote);
		this.gbinsCdts.Controls.Add(this.lbmsmNote);
		this.gbinsCdts.Controls.Add(this.tbmsmTemperature);
		this.gbinsCdts.Controls.Add(this.lbmsmTemperature);
		this.gbinsCdts.Controls.Add(this.tbmsmDetection);
		this.gbinsCdts.Controls.Add(this.lbmsmDetection);
		this.gbinsCdts.Controls.Add(this.tbmsmPressure);
		this.gbinsCdts.Controls.Add(this.lbmsmPressure);
		this.gbinsCdts.Controls.Add(this.tbmsmFlowRate);
		this.gbinsCdts.Controls.Add(this.lbmsmFlowRate);
		this.gbinsCdts.Controls.Add(this.tbmsmMobilePhase);
		this.gbinsCdts.Controls.Add(this.lbmsmMobilePhase);
		this.gbinsCdts.Controls.Add(this.tbmsmColumn);
		this.gbinsCdts.Controls.Add(this.lbmsmColumn);
		this.gbinsCdts.Controls.Add(this.tbmsmMtdDspt);
		this.gbinsCdts.Controls.Add(this.lbmsmMtdDspt);
		this.gbinsCdts.Location = new System.Drawing.Point(6, 8);
		this.gbinsCdts.Name = "gbinsCdts";
		this.gbinsCdts.Size = new System.Drawing.Size(293, 220);
		this.gbinsCdts.TabIndex = 4;
		this.gbinsCdts.TabStop = false;
		this.gbinsCdts.Text = "测量条件";
		this.tbmsmNote.Location = new System.Drawing.Point(85, 178);
		this.tbmsmNote.Multiline = true;
		this.tbmsmNote.Name = "tbmsmNote";
		this.tbmsmNote.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
		this.tbmsmNote.Size = new System.Drawing.Size(202, 38);
		this.tbmsmNote.TabIndex = 1;
		this.tbmsmNote.KeyPress += new System.Windows.Forms.KeyPressEventHandler(tbsiSampleID_KeyPress);
		this.lbmsmNote.AutoSize = true;
		this.lbmsmNote.Location = new System.Drawing.Point(6, 181);
		this.lbmsmNote.Name = "lbmsmNote";
		this.lbmsmNote.Size = new System.Drawing.Size(29, 12);
		this.lbmsmNote.TabIndex = 0;
		this.lbmsmNote.Text = "备注";
		this.tbmsmTemperature.Location = new System.Drawing.Point(85, 155);
		this.tbmsmTemperature.Name = "tbmsmTemperature";
		this.tbmsmTemperature.Size = new System.Drawing.Size(202, 21);
		this.tbmsmTemperature.TabIndex = 1;
		this.tbmsmTemperature.KeyPress += new System.Windows.Forms.KeyPressEventHandler(tbsiSampleID_KeyPress);
		this.lbmsmTemperature.AutoSize = true;
		this.lbmsmTemperature.Location = new System.Drawing.Point(6, 158);
		this.lbmsmTemperature.Name = "lbmsmTemperature";
		this.lbmsmTemperature.Size = new System.Drawing.Size(29, 12);
		this.lbmsmTemperature.TabIndex = 0;
		this.lbmsmTemperature.Text = "温度";
		this.tbmsmDetection.Location = new System.Drawing.Point(85, 132);
		this.tbmsmDetection.Name = "tbmsmDetection";
		this.tbmsmDetection.Size = new System.Drawing.Size(202, 21);
		this.tbmsmDetection.TabIndex = 1;
		this.tbmsmDetection.KeyPress += new System.Windows.Forms.KeyPressEventHandler(tbsiSampleID_KeyPress);
		this.lbmsmDetection.AutoSize = true;
		this.lbmsmDetection.Location = new System.Drawing.Point(6, 135);
		this.lbmsmDetection.Name = "lbmsmDetection";
		this.lbmsmDetection.Size = new System.Drawing.Size(29, 12);
		this.lbmsmDetection.TabIndex = 0;
		this.lbmsmDetection.Text = "检测";
		this.tbmsmPressure.Location = new System.Drawing.Point(85, 109);
		this.tbmsmPressure.Name = "tbmsmPressure";
		this.tbmsmPressure.Size = new System.Drawing.Size(202, 21);
		this.tbmsmPressure.TabIndex = 1;
		this.tbmsmPressure.KeyPress += new System.Windows.Forms.KeyPressEventHandler(tbsiSampleID_KeyPress);
		this.lbmsmPressure.AutoSize = true;
		this.lbmsmPressure.Location = new System.Drawing.Point(6, 114);
		this.lbmsmPressure.Name = "lbmsmPressure";
		this.lbmsmPressure.Size = new System.Drawing.Size(29, 12);
		this.lbmsmPressure.TabIndex = 0;
		this.lbmsmPressure.Text = "压力";
		this.tbmsmFlowRate.Location = new System.Drawing.Point(85, 86);
		this.tbmsmFlowRate.Name = "tbmsmFlowRate";
		this.tbmsmFlowRate.Size = new System.Drawing.Size(202, 21);
		this.tbmsmFlowRate.TabIndex = 1;
		this.tbmsmFlowRate.KeyPress += new System.Windows.Forms.KeyPressEventHandler(tbsiSampleID_KeyPress);
		this.lbmsmFlowRate.AutoSize = true;
		this.lbmsmFlowRate.Location = new System.Drawing.Point(6, 91);
		this.lbmsmFlowRate.Name = "lbmsmFlowRate";
		this.lbmsmFlowRate.Size = new System.Drawing.Size(29, 12);
		this.lbmsmFlowRate.TabIndex = 0;
		this.lbmsmFlowRate.Text = "流速";
		this.tbmsmMobilePhase.Location = new System.Drawing.Point(85, 63);
		this.tbmsmMobilePhase.Name = "tbmsmMobilePhase";
		this.tbmsmMobilePhase.Size = new System.Drawing.Size(202, 21);
		this.tbmsmMobilePhase.TabIndex = 1;
		this.tbmsmMobilePhase.KeyPress += new System.Windows.Forms.KeyPressEventHandler(tbsiSampleID_KeyPress);
		this.lbmsmMobilePhase.AutoSize = true;
		this.lbmsmMobilePhase.Location = new System.Drawing.Point(6, 68);
		this.lbmsmMobilePhase.Name = "lbmsmMobilePhase";
		this.lbmsmMobilePhase.Size = new System.Drawing.Size(41, 12);
		this.lbmsmMobilePhase.TabIndex = 0;
		this.lbmsmMobilePhase.Text = "流动相";
		this.tbmsmColumn.Location = new System.Drawing.Point(85, 40);
		this.tbmsmColumn.Name = "tbmsmColumn";
		this.tbmsmColumn.Size = new System.Drawing.Size(202, 21);
		this.tbmsmColumn.TabIndex = 1;
		this.tbmsmColumn.KeyPress += new System.Windows.Forms.KeyPressEventHandler(tbsiSampleID_KeyPress);
		this.lbmsmColumn.AutoSize = true;
		this.lbmsmColumn.Location = new System.Drawing.Point(6, 44);
		this.lbmsmColumn.Name = "lbmsmColumn";
		this.lbmsmColumn.Size = new System.Drawing.Size(41, 12);
		this.lbmsmColumn.TabIndex = 0;
		this.lbmsmColumn.Text = "色谱柱";
		this.tbmsmMtdDspt.Location = new System.Drawing.Point(85, 17);
		this.tbmsmMtdDspt.Name = "tbmsmMtdDspt";
		this.tbmsmMtdDspt.Size = new System.Drawing.Size(202, 21);
		this.tbmsmMtdDspt.TabIndex = 1;
		this.tbmsmMtdDspt.KeyPress += new System.Windows.Forms.KeyPressEventHandler(tbsiSampleID_KeyPress);
		this.lbmsmMtdDspt.AutoSize = true;
		this.lbmsmMtdDspt.Location = new System.Drawing.Point(6, 22);
		this.lbmsmMtdDspt.Name = "lbmsmMtdDspt";
		this.lbmsmMtdDspt.Size = new System.Drawing.Size(29, 12);
		this.lbmsmMtdDspt.TabIndex = 0;
		this.lbmsmMtdDspt.Text = "描述";
		this.gbinsSampleIdt.Controls.Add(this.tbsiSample);
		this.gbinsSampleIdt.Controls.Add(this.lbsiAcquiredTimeV);
		this.gbinsSampleIdt.Controls.Add(this.lbsiAcquiredTime);
		this.gbinsSampleIdt.Controls.Add(this.lbsiAnalystV);
		this.gbinsSampleIdt.Controls.Add(this.lbsiAnalyst);
		this.gbinsSampleIdt.Controls.Add(this.lbsiSample);
		this.gbinsSampleIdt.Controls.Add(this.tbsiSampleID);
		this.gbinsSampleIdt.Controls.Add(this.lbsiSampleID);
		this.gbinsSampleIdt.Location = new System.Drawing.Point(305, 8);
		this.gbinsSampleIdt.Name = "gbinsSampleIdt";
		this.gbinsSampleIdt.Size = new System.Drawing.Size(283, 107);
		this.gbinsSampleIdt.TabIndex = 2;
		this.gbinsSampleIdt.TabStop = false;
		this.gbinsSampleIdt.Text = "样品核对";
		this.tbsiSample.Location = new System.Drawing.Point(81, 40);
		this.tbsiSample.Name = "tbsiSample";
		this.tbsiSample.Size = new System.Drawing.Size(192, 21);
		this.tbsiSample.TabIndex = 1;
		this.tbsiSample.KeyPress += new System.Windows.Forms.KeyPressEventHandler(tbsiSampleID_KeyPress);
		this.lbsiAcquiredTimeV.AutoSize = true;
		this.lbsiAcquiredTimeV.Location = new System.Drawing.Point(79, 66);
		this.lbsiAcquiredTimeV.Name = "lbsiAcquiredTimeV";
		this.lbsiAcquiredTimeV.Size = new System.Drawing.Size(95, 12);
		this.lbsiAcquiredTimeV.TabIndex = 0;
		this.lbsiAcquiredTimeV.Text = "2008/08/08 8:00";
		this.lbsiAcquiredTime.AutoSize = true;
		this.lbsiAcquiredTime.Location = new System.Drawing.Point(10, 66);
		this.lbsiAcquiredTime.Name = "lbsiAcquiredTime";
		this.lbsiAcquiredTime.Size = new System.Drawing.Size(71, 12);
		this.lbsiAcquiredTime.TabIndex = 0;
		this.lbsiAcquiredTime.Text = "采集时间  :";
		this.lbsiAnalystV.AutoSize = true;
		this.lbsiAnalystV.Location = new System.Drawing.Point(79, 86);
		this.lbsiAnalystV.Name = "lbsiAnalystV";
		this.lbsiAnalystV.Size = new System.Drawing.Size(23, 12);
		this.lbsiAnalystV.TabIndex = 0;
		this.lbsiAnalystV.Text = "Hzz";
		this.lbsiAnalyst.AutoSize = true;
		this.lbsiAnalyst.Location = new System.Drawing.Point(9, 86);
		this.lbsiAnalyst.Name = "lbsiAnalyst";
		this.lbsiAnalyst.Size = new System.Drawing.Size(71, 12);
		this.lbsiAnalyst.TabIndex = 0;
		this.lbsiAnalyst.Text = "分析员    :";
		this.lbsiSample.AutoSize = true;
		this.lbsiSample.Location = new System.Drawing.Point(9, 43);
		this.lbsiSample.Name = "lbsiSample";
		this.lbsiSample.Size = new System.Drawing.Size(29, 12);
		this.lbsiSample.TabIndex = 0;
		this.lbsiSample.Text = "样品";
		this.tbsiSampleID.Location = new System.Drawing.Point(81, 17);
		this.tbsiSampleID.Name = "tbsiSampleID";
		this.tbsiSampleID.Size = new System.Drawing.Size(192, 21);
		this.tbsiSampleID.TabIndex = 1;
		this.tbsiSampleID.KeyPress += new System.Windows.Forms.KeyPressEventHandler(tbsiSampleID_KeyPress);
		this.lbsiSampleID.AutoSize = true;
		this.lbsiSampleID.Location = new System.Drawing.Point(9, 21);
		this.lbsiSampleID.Name = "lbsiSampleID";
		this.lbsiSampleID.Size = new System.Drawing.Size(47, 12);
		this.lbsiSampleID.TabIndex = 0;
		this.lbsiSampleID.Text = "样品 ID";
		this.gbinsAcqParas.Controls.Add(this.pbapES);
		this.gbinsAcqParas.Controls.Add(this.lbapMethodV);
		this.gbinsAcqParas.Controls.Add(this.lbapMethod);
		this.gbinsAcqParas.Controls.Add(this.lbapSamplingV);
		this.gbinsAcqParas.Controls.Add(this.lbapSampling);
		this.gbinsAcqParas.Controls.Add(this.lbapExtStartV);
		this.gbinsAcqParas.Controls.Add(this.lbapExtStart);
		this.gbinsAcqParas.Controls.Add(this.lbapRangeV);
		this.gbinsAcqParas.Controls.Add(this.lbapAutoStopV);
		this.gbinsAcqParas.Controls.Add(this.lbapRange);
		this.gbinsAcqParas.Controls.Add(this.lbapAutoStop);
		this.gbinsAcqParas.Location = new System.Drawing.Point(594, 8);
		this.gbinsAcqParas.Name = "gbinsAcqParas";
		this.gbinsAcqParas.Size = new System.Drawing.Size(196, 167);
		this.gbinsAcqParas.TabIndex = 2;
		this.gbinsAcqParas.TabStop = false;
		this.gbinsAcqParas.Text = "采集参数";
		this.pbapES.Location = new System.Drawing.Point(152, 40);
		this.pbapES.Name = "pbapES";
		this.pbapES.Size = new System.Drawing.Size(20, 15);
		this.pbapES.TabIndex = 6;
		this.pbapES.TabStop = false;
		this.lbapMethodV.AutoSize = true;
		this.lbapMethodV.Location = new System.Drawing.Point(78, 66);
		this.lbapMethodV.Name = "lbapMethodV";
		this.lbapMethodV.Size = new System.Drawing.Size(71, 12);
		this.lbapMethodV.TabIndex = 0;
		this.lbapMethodV.Text = "lbapMethodV";
		this.lbapMethod.AutoSize = true;
		this.lbapMethod.Location = new System.Drawing.Point(9, 66);
		this.lbapMethod.Name = "lbapMethod";
		this.lbapMethod.Size = new System.Drawing.Size(71, 12);
		this.lbapMethod.TabIndex = 0;
		this.lbapMethod.Text = "方法      :";
		this.lbapSamplingV.AutoSize = true;
		this.lbapSamplingV.Location = new System.Drawing.Point(78, 113);
		this.lbapSamplingV.Name = "lbapSamplingV";
		this.lbapSamplingV.Size = new System.Drawing.Size(41, 12);
		this.lbapSamplingV.TabIndex = 0;
		this.lbapSamplingV.Text = " 点/秒";
		this.lbapSampling.AutoSize = true;
		this.lbapSampling.Location = new System.Drawing.Point(9, 113);
		this.lbapSampling.Name = "lbapSampling";
		this.lbapSampling.Size = new System.Drawing.Size(71, 12);
		this.lbapSampling.TabIndex = 0;
		this.lbapSampling.Text = "采样      :";
		this.lbapExtStartV.AutoSize = true;
		this.lbapExtStartV.Location = new System.Drawing.Point(78, 42);
		this.lbapExtStartV.Name = "lbapExtStartV";
		this.lbapExtStartV.Size = new System.Drawing.Size(17, 12);
		this.lbapExtStartV.TabIndex = 0;
		this.lbapExtStartV.Text = "否";
		this.lbapExtStart.AutoSize = true;
		this.lbapExtStart.Location = new System.Drawing.Point(9, 42);
		this.lbapExtStart.Name = "lbapExtStart";
		this.lbapExtStart.Size = new System.Drawing.Size(71, 12);
		this.lbapExtStart.TabIndex = 0;
		this.lbapExtStart.Text = "外部启动  :";
		this.lbapRangeV.AutoSize = true;
		this.lbapRangeV.Location = new System.Drawing.Point(78, 89);
		this.lbapRangeV.Name = "lbapRangeV";
		this.lbapRangeV.Size = new System.Drawing.Size(95, 12);
		this.lbapRangeV.TabIndex = 0;
		this.lbapRangeV.Text = "1250mV, bipolar";
		this.lbapAutoStopV.AutoSize = true;
		this.lbapAutoStopV.Location = new System.Drawing.Point(78, 21);
		this.lbapAutoStopV.Name = "lbapAutoStopV";
		this.lbapAutoStopV.Size = new System.Drawing.Size(47, 12);
		this.lbapAutoStopV.TabIndex = 0;
		this.lbapAutoStopV.Text = "0 min.,";
		this.lbapRange.AutoSize = true;
		this.lbapRange.Location = new System.Drawing.Point(9, 89);
		this.lbapRange.Name = "lbapRange";
		this.lbapRange.Size = new System.Drawing.Size(71, 12);
		this.lbapRange.TabIndex = 0;
		this.lbapRange.Text = "范围      :";
		this.lbapAutoStop.AutoSize = true;
		this.lbapAutoStop.Location = new System.Drawing.Point(9, 21);
		this.lbapAutoStop.Name = "lbapAutoStop";
		this.lbapAutoStop.Size = new System.Drawing.Size(71, 12);
		this.lbapAutoStop.TabIndex = 0;
		this.lbapAutoStop.Text = "自动停止  :";
		this.tpPDAMethod.AutoScroll = true;
		this.tpPDAMethod.Controls.Add(this.gbpdaLibs);
		this.tpPDAMethod.Controls.Add(this.gbpdaLibSearchOptions);
		this.tpPDAMethod.Controls.Add(this.gbpdaPeakPurityOptions);
		this.tpPDAMethod.Location = new System.Drawing.Point(4, 23);
		this.tpPDAMethod.Name = "tpPDAMethod";
		this.tpPDAMethod.Size = new System.Drawing.Size(958, 231);
		this.tpPDAMethod.TabIndex = 1;
		this.tpPDAMethod.Text = "PDA 方法";
		this.tpPDAMethod.UseVisualStyleBackColor = true;
		this.gbpdaLibs.Controls.Add(this.gvlibPDA);
		this.gbpdaLibs.Location = new System.Drawing.Point(551, 7);
		this.gbpdaLibs.Name = "gbpdaLibs";
		this.gbpdaLibs.Size = new System.Drawing.Size(214, 94);
		this.gbpdaLibs.TabIndex = 1;
		this.gbpdaLibs.TabStop = false;
		this.gbpdaLibs.Text = "匹配库";
		this.gvlibPDA.AllowUserToAddRows = false;
		this.gvlibPDA.AllowUserToDeleteRows = false;
		this.gvlibPDA.AllowUserToResizeRows = false;
		this.gvlibPDA.BackgroundColor = System.Drawing.Color.AliceBlue;
		this.gvlibPDA.CharacterHeaderColor = System.Drawing.Color.Black;
		dataGridViewCellStyle22.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
		dataGridViewCellStyle22.BackColor = System.Drawing.SystemColors.Control;
		dataGridViewCellStyle22.Font = new System.Drawing.Font("宋体", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		dataGridViewCellStyle22.ForeColor = System.Drawing.SystemColors.WindowText;
		dataGridViewCellStyle22.SelectionBackColor = System.Drawing.SystemColors.Highlight;
		dataGridViewCellStyle22.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
		dataGridViewCellStyle22.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
		this.gvlibPDA.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle22;
		this.gvlibPDA.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
		this.gvlibPDA.ContextMenuStrip = this.cmsLibs;
		dataGridViewCellStyle23.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
		dataGridViewCellStyle23.BackColor = System.Drawing.SystemColors.Window;
		dataGridViewCellStyle23.Font = new System.Drawing.Font("宋体", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		dataGridViewCellStyle23.ForeColor = System.Drawing.SystemColors.ControlText;
		dataGridViewCellStyle23.SelectionBackColor = System.Drawing.SystemColors.Highlight;
		dataGridViewCellStyle23.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
		dataGridViewCellStyle23.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
		this.gvlibPDA.DefaultCellStyle = dataGridViewCellStyle23;
		this.gvlibPDA.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
		this.gvlibPDA.Location = new System.Drawing.Point(10, 20);
		this.gvlibPDA.Name = "gvlibPDA";
		dataGridViewCellStyle24.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
		dataGridViewCellStyle24.BackColor = System.Drawing.SystemColors.Control;
		dataGridViewCellStyle24.Font = new System.Drawing.Font("宋体", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		dataGridViewCellStyle24.ForeColor = System.Drawing.SystemColors.WindowText;
		dataGridViewCellStyle24.SelectionBackColor = System.Drawing.SystemColors.Highlight;
		dataGridViewCellStyle24.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
		dataGridViewCellStyle24.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
		this.gvlibPDA.RowHeadersDefaultCellStyle = dataGridViewCellStyle24;
		this.gvlibPDA.RowHeadersWidth = 25;
		this.gvlibPDA.RowTemplate.Height = 16;
		this.gvlibPDA.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
		this.gvlibPDA.ShowCellToolTips = false;
		this.gvlibPDA.Size = new System.Drawing.Size(23, 22);
		this.gvlibPDA.TabIndex = 0;
		this.cmsLibs.Items.AddRange(new System.Windows.Forms.ToolStripItem[2] { this.miAddRow, this.miDeleteRow });
		this.cmsLibs.Name = "cmsLibs";
		this.cmsLibs.ShowImageMargin = false;
		this.cmsLibs.Size = new System.Drawing.Size(88, 48);
		this.miAddRow.Name = "miAddRow";
		this.miAddRow.Size = new System.Drawing.Size(87, 22);
		this.miAddRow.Text = "添加库";
		this.miDeleteRow.Name = "miDeleteRow";
		this.miDeleteRow.Size = new System.Drawing.Size(87, 22);
		this.miDeleteRow.Text = "删除库";
		this.gbpdaLibSearchOptions.Controls.Add(this.cblsoMatchCriteria);
		this.gbpdaLibSearchOptions.Controls.Add(this.tblsoRestrictRT);
		this.gbpdaLibSearchOptions.Controls.Add(this.tblsoMaxNumHits);
		this.gbpdaLibSearchOptions.Controls.Add(this.tblsoMatchFactorThreshold);
		this.gbpdaLibSearchOptions.Controls.Add(this.tblsoFrom);
		this.gbpdaLibSearchOptions.Controls.Add(this.lclLabel15);
		this.gbpdaLibSearchOptions.Controls.Add(this.tblsoTo);
		this.gbpdaLibSearchOptions.Controls.Add(this.lclLabel9);
		this.gbpdaLibSearchOptions.Controls.Add(this.lclLabel10);
		this.gbpdaLibSearchOptions.Controls.Add(this.lblsoMaxNumHits);
		this.gbpdaLibSearchOptions.Controls.Add(this.lblsoTo);
		this.gbpdaLibSearchOptions.Controls.Add(this.lblsoMatchCriteria);
		this.gbpdaLibSearchOptions.Controls.Add(this.lblsoMatchFactorThreshold);
		this.gbpdaLibSearchOptions.Controls.Add(this.lblsoFrom);
		this.gbpdaLibSearchOptions.Controls.Add(this.cblsoForAllDetectedPeaks);
		this.gbpdaLibSearchOptions.Controls.Add(this.cblsoUseBackCorr);
		this.gbpdaLibSearchOptions.Controls.Add(this.cblsoRestrictRT);
		this.gbpdaLibSearchOptions.Controls.Add(this.cblsoRestrictWaveLength);
		this.gbpdaLibSearchOptions.Location = new System.Drawing.Point(281, 7);
		this.gbpdaLibSearchOptions.Name = "gbpdaLibSearchOptions";
		this.gbpdaLibSearchOptions.Size = new System.Drawing.Size(264, 219);
		this.gbpdaLibSearchOptions.TabIndex = 0;
		this.gbpdaLibSearchOptions.TabStop = false;
		this.gbpdaLibSearchOptions.Text = "库分析选项";
		this.cblsoMatchCriteria.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.cblsoMatchCriteria.FormattingEnabled = true;
		this.cblsoMatchCriteria.ItemExtString = "";
		this.cblsoMatchCriteria.Location = new System.Drawing.Point(135, 18);
		this.cblsoMatchCriteria.Name = "cblsoMatchCriteria";
		this.cblsoMatchCriteria.Size = new System.Drawing.Size(121, 20);
		this.cblsoMatchCriteria.TabIndex = 3;
		this.tblsoRestrictRT.Location = new System.Drawing.Point(135, 143);
		this.tblsoRestrictRT.Name = "tblsoRestrictRT";
		this.tblsoRestrictRT.Size = new System.Drawing.Size(57, 21);
		this.tblsoRestrictRT.TabIndex = 2;
		this.tblsoMaxNumHits.Location = new System.Drawing.Point(135, 72);
		this.tblsoMaxNumHits.Name = "tblsoMaxNumHits";
		this.tblsoMaxNumHits.Size = new System.Drawing.Size(57, 21);
		this.tblsoMaxNumHits.TabIndex = 2;
		this.tblsoMatchFactorThreshold.Location = new System.Drawing.Point(135, 45);
		this.tblsoMatchFactorThreshold.Name = "tblsoMatchFactorThreshold";
		this.tblsoMatchFactorThreshold.Size = new System.Drawing.Size(57, 21);
		this.tblsoMatchFactorThreshold.TabIndex = 2;
		this.tblsoFrom.Location = new System.Drawing.Point(90, 118);
		this.tblsoFrom.Name = "tblsoFrom";
		this.tblsoFrom.Size = new System.Drawing.Size(52, 21);
		this.tblsoFrom.TabIndex = 2;
		this.lclLabel15.AutoSize = true;
		this.lclLabel15.Location = new System.Drawing.Point(198, 146);
		this.lclLabel15.Name = "lclLabel15";
		this.lclLabel15.Size = new System.Drawing.Size(11, 12);
		this.lclLabel15.TabIndex = 1;
		this.lclLabel15.Text = "%";
		this.tblsoTo.Location = new System.Drawing.Point(180, 118);
		this.tblsoTo.Name = "tblsoTo";
		this.tblsoTo.Size = new System.Drawing.Size(52, 21);
		this.tblsoTo.TabIndex = 2;
		this.lclLabel9.AutoSize = true;
		this.lclLabel9.Location = new System.Drawing.Point(238, 121);
		this.lclLabel9.Name = "lclLabel9";
		this.lclLabel9.Size = new System.Drawing.Size(17, 12);
		this.lclLabel9.TabIndex = 1;
		this.lclLabel9.Text = "nm";
		this.lclLabel10.AutoSize = true;
		this.lclLabel10.Location = new System.Drawing.Point(198, 48);
		this.lclLabel10.Name = "lclLabel10";
		this.lclLabel10.Size = new System.Drawing.Size(59, 12);
		this.lclLabel10.TabIndex = 1;
		this.lclLabel10.Text = "(0..1000)";
		this.lblsoMaxNumHits.AutoSize = true;
		this.lblsoMaxNumHits.Location = new System.Drawing.Point(6, 75);
		this.lblsoMaxNumHits.Name = "lblsoMaxNumHits";
		this.lblsoMaxNumHits.Size = new System.Drawing.Size(77, 12);
		this.lblsoMaxNumHits.TabIndex = 1;
		this.lblsoMaxNumHits.Text = "最大显示波数";
		this.lblsoTo.AutoSize = true;
		this.lblsoTo.Location = new System.Drawing.Point(148, 121);
		this.lblsoTo.Name = "lblsoTo";
		this.lblsoTo.Size = new System.Drawing.Size(23, 12);
		this.lblsoTo.TabIndex = 1;
		this.lblsoTo.Text = "到:";
		this.lblsoMatchCriteria.AutoSize = true;
		this.lblsoMatchCriteria.Location = new System.Drawing.Point(6, 21);
		this.lblsoMatchCriteria.Name = "lblsoMatchCriteria";
		this.lblsoMatchCriteria.Size = new System.Drawing.Size(53, 12);
		this.lblsoMatchCriteria.TabIndex = 1;
		this.lblsoMatchCriteria.Text = "匹配规则";
		this.lblsoMatchFactorThreshold.AutoSize = true;
		this.lblsoMatchFactorThreshold.Location = new System.Drawing.Point(6, 48);
		this.lblsoMatchFactorThreshold.Name = "lblsoMatchFactorThreshold";
		this.lblsoMatchFactorThreshold.Size = new System.Drawing.Size(77, 12);
		this.lblsoMatchFactorThreshold.TabIndex = 1;
		this.lblsoMatchFactorThreshold.Text = "匹配因子极限";
		this.lblsoFrom.AutoSize = true;
		this.lblsoFrom.Location = new System.Drawing.Point(50, 121);
		this.lblsoFrom.Name = "lblsoFrom";
		this.lblsoFrom.Size = new System.Drawing.Size(23, 12);
		this.lblsoFrom.TabIndex = 1;
		this.lblsoFrom.Text = "从:";
		this.cblsoForAllDetectedPeaks.AutoSize = true;
		this.cblsoForAllDetectedPeaks.Location = new System.Drawing.Point(6, 189);
		this.cblsoForAllDetectedPeaks.Name = "cblsoForAllDetectedPeaks";
		this.cblsoForAllDetectedPeaks.Size = new System.Drawing.Size(84, 16);
		this.cblsoForAllDetectedPeaks.TabIndex = 0;
		this.cblsoForAllDetectedPeaks.Text = "所有检测峰";
		this.cblsoForAllDetectedPeaks.UseVisualStyleBackColor = true;
		this.cblsoUseBackCorr.AutoSize = true;
		this.cblsoUseBackCorr.Location = new System.Drawing.Point(6, 167);
		this.cblsoUseBackCorr.Name = "cblsoUseBackCorr";
		this.cblsoUseBackCorr.Size = new System.Drawing.Size(96, 16);
		this.cblsoUseBackCorr.TabIndex = 0;
		this.cblsoUseBackCorr.Text = "使用背景修正";
		this.cblsoUseBackCorr.UseVisualStyleBackColor = true;
		this.cblsoRestrictRT.AutoSize = true;
		this.cblsoRestrictRT.Location = new System.Drawing.Point(6, 145);
		this.cblsoRestrictRT.Name = "cblsoRestrictRT";
		this.cblsoRestrictRT.Size = new System.Drawing.Size(96, 16);
		this.cblsoRestrictRT.TabIndex = 0;
		this.cblsoRestrictRT.Text = "限制保留时间";
		this.cblsoRestrictRT.UseVisualStyleBackColor = true;
		this.cblsoRestrictWaveLength.AutoSize = true;
		this.cblsoRestrictWaveLength.Location = new System.Drawing.Point(6, 99);
		this.cblsoRestrictWaveLength.Name = "cblsoRestrictWaveLength";
		this.cblsoRestrictWaveLength.Size = new System.Drawing.Size(96, 16);
		this.cblsoRestrictWaveLength.TabIndex = 0;
		this.cblsoRestrictWaveLength.Text = "限制波长范围";
		this.cblsoRestrictWaveLength.UseVisualStyleBackColor = true;
		this.gbpdaPeakPurityOptions.Controls.Add(this.gbppoUsedPoints);
		this.gbpdaPeakPurityOptions.Controls.Add(this.tbppoAbsorbanceThreshold);
		this.gbpdaPeakPurityOptions.Controls.Add(this.tbppoPurityThreshold);
		this.gbpdaPeakPurityOptions.Controls.Add(this.tbppoFrom);
		this.gbpdaPeakPurityOptions.Controls.Add(this.tbppoTo);
		this.gbpdaPeakPurityOptions.Controls.Add(this.lclLabel7);
		this.gbpdaPeakPurityOptions.Controls.Add(this.lclLabel3);
		this.gbpdaPeakPurityOptions.Controls.Add(this.lclLabel5);
		this.gbpdaPeakPurityOptions.Controls.Add(this.lbppoAbsorbanceThreshold);
		this.gbpdaPeakPurityOptions.Controls.Add(this.lbppoTo);
		this.gbpdaPeakPurityOptions.Controls.Add(this.lbppoPurityThreshold);
		this.gbpdaPeakPurityOptions.Controls.Add(this.lbppoFrom);
		this.gbpdaPeakPurityOptions.Controls.Add(this.cbppoUseBackCorr);
		this.gbpdaPeakPurityOptions.Controls.Add(this.cbppoRestrictWaveLength);
		this.gbpdaPeakPurityOptions.Location = new System.Drawing.Point(9, 7);
		this.gbpdaPeakPurityOptions.Name = "gbpdaPeakPurityOptions";
		this.gbpdaPeakPurityOptions.Size = new System.Drawing.Size(266, 219);
		this.gbpdaPeakPurityOptions.TabIndex = 0;
		this.gbpdaPeakPurityOptions.TabStop = false;
		this.gbpdaPeakPurityOptions.Text = "峰纯度选项";
		this.gbppoUsedPoints.Controls.Add(this.rbupFive);
		this.gbppoUsedPoints.Controls.Add(this.rbupAll);
		this.gbppoUsedPoints.Location = new System.Drawing.Point(6, 126);
		this.gbppoUsedPoints.Name = "gbppoUsedPoints";
		this.gbppoUsedPoints.Size = new System.Drawing.Size(103, 64);
		this.gbppoUsedPoints.TabIndex = 3;
		this.gbppoUsedPoints.TabStop = false;
		this.gbppoUsedPoints.Text = "使用点数";
		this.rbupFive.AutoSize = true;
		this.rbupFive.Location = new System.Drawing.Point(6, 42);
		this.rbupFive.Name = "rbupFive";
		this.rbupFive.Size = new System.Drawing.Size(47, 16);
		this.rbupFive.TabIndex = 4;
		this.rbupFive.TabStop = true;
		this.rbupFive.Text = "五点";
		this.rbupFive.UseVisualStyleBackColor = true;
		this.rbupAll.AutoSize = true;
		this.rbupAll.Location = new System.Drawing.Point(6, 20);
		this.rbupAll.Name = "rbupAll";
		this.rbupAll.Size = new System.Drawing.Size(47, 16);
		this.rbupAll.TabIndex = 4;
		this.rbupAll.TabStop = true;
		this.rbupAll.Text = "全部";
		this.rbupAll.UseVisualStyleBackColor = true;
		this.tbppoAbsorbanceThreshold.Location = new System.Drawing.Point(139, 99);
		this.tbppoAbsorbanceThreshold.Name = "tbppoAbsorbanceThreshold";
		this.tbppoAbsorbanceThreshold.Size = new System.Drawing.Size(57, 21);
		this.tbppoAbsorbanceThreshold.TabIndex = 2;
		this.tbppoPurityThreshold.Location = new System.Drawing.Point(139, 72);
		this.tbppoPurityThreshold.Name = "tbppoPurityThreshold";
		this.tbppoPurityThreshold.Size = new System.Drawing.Size(57, 21);
		this.tbppoPurityThreshold.TabIndex = 2;
		this.tbppoFrom.Location = new System.Drawing.Point(96, 45);
		this.tbppoFrom.Name = "tbppoFrom";
		this.tbppoFrom.Size = new System.Drawing.Size(52, 21);
		this.tbppoFrom.TabIndex = 2;
		this.tbppoTo.Location = new System.Drawing.Point(186, 45);
		this.tbppoTo.Name = "tbppoTo";
		this.tbppoTo.Size = new System.Drawing.Size(52, 21);
		this.tbppoTo.TabIndex = 2;
		this.lclLabel7.AutoSize = true;
		this.lclLabel7.Location = new System.Drawing.Point(202, 102);
		this.lclLabel7.Name = "lclLabel7";
		this.lclLabel7.Size = new System.Drawing.Size(11, 12);
		this.lclLabel7.TabIndex = 1;
		this.lclLabel7.Text = "%";
		this.lclLabel3.AutoSize = true;
		this.lclLabel3.Location = new System.Drawing.Point(244, 48);
		this.lclLabel3.Name = "lclLabel3";
		this.lclLabel3.Size = new System.Drawing.Size(17, 12);
		this.lclLabel3.TabIndex = 1;
		this.lclLabel3.Text = "nm";
		this.lclLabel5.AutoSize = true;
		this.lclLabel5.Location = new System.Drawing.Point(202, 75);
		this.lclLabel5.Name = "lclLabel5";
		this.lclLabel5.Size = new System.Drawing.Size(59, 12);
		this.lclLabel5.TabIndex = 1;
		this.lclLabel5.Text = "(0..1000)";
		this.lbppoAbsorbanceThreshold.AutoSize = true;
		this.lbppoAbsorbanceThreshold.Location = new System.Drawing.Point(8, 102);
		this.lbppoAbsorbanceThreshold.Name = "lbppoAbsorbanceThreshold";
		this.lbppoAbsorbanceThreshold.Size = new System.Drawing.Size(53, 12);
		this.lbppoAbsorbanceThreshold.TabIndex = 1;
		this.lbppoAbsorbanceThreshold.Text = "吸收极限";
		this.lbppoTo.AutoSize = true;
		this.lbppoTo.Location = new System.Drawing.Point(154, 48);
		this.lbppoTo.Name = "lbppoTo";
		this.lbppoTo.Size = new System.Drawing.Size(23, 12);
		this.lbppoTo.TabIndex = 1;
		this.lbppoTo.Text = "到:";
		this.lbppoPurityThreshold.AutoSize = true;
		this.lbppoPurityThreshold.Location = new System.Drawing.Point(8, 75);
		this.lbppoPurityThreshold.Name = "lbppoPurityThreshold";
		this.lbppoPurityThreshold.Size = new System.Drawing.Size(53, 12);
		this.lbppoPurityThreshold.TabIndex = 1;
		this.lbppoPurityThreshold.Text = "纯度极限";
		this.lbppoFrom.AutoSize = true;
		this.lbppoFrom.Location = new System.Drawing.Point(50, 48);
		this.lbppoFrom.Name = "lbppoFrom";
		this.lbppoFrom.Size = new System.Drawing.Size(23, 12);
		this.lbppoFrom.TabIndex = 1;
		this.lbppoFrom.Text = "从:";
		this.cbppoUseBackCorr.AutoSize = true;
		this.cbppoUseBackCorr.Location = new System.Drawing.Point(6, 196);
		this.cbppoUseBackCorr.Name = "cbppoUseBackCorr";
		this.cbppoUseBackCorr.Size = new System.Drawing.Size(96, 16);
		this.cbppoUseBackCorr.TabIndex = 0;
		this.cbppoUseBackCorr.Text = "使用背景修正";
		this.cbppoUseBackCorr.UseVisualStyleBackColor = true;
		this.cbppoRestrictWaveLength.AutoSize = true;
		this.cbppoRestrictWaveLength.Location = new System.Drawing.Point(10, 20);
		this.cbppoRestrictWaveLength.Name = "cbppoRestrictWaveLength";
		this.cbppoRestrictWaveLength.Size = new System.Drawing.Size(96, 16);
		this.cbppoRestrictWaveLength.TabIndex = 0;
		this.cbppoRestrictWaveLength.Text = "限制波长范围";
		this.cbppoRestrictWaveLength.UseVisualStyleBackColor = true;
		this.tpLC.Controls.Add(this.gvlcGradient);
		this.tpLC.Location = new System.Drawing.Point(4, 23);
		this.tpLC.Name = "tpLC";
		this.tpLC.Size = new System.Drawing.Size(958, 231);
		this.tpLC.TabIndex = 2;
		this.tpLC.UseVisualStyleBackColor = true;
		this.gvlcGradient.AllowUserToAddRows = false;
		this.gvlcGradient.AllowUserToDeleteRows = false;
		this.gvlcGradient.AllowUserToResizeRows = false;
		this.gvlcGradient.BackgroundColor = System.Drawing.Color.AliceBlue;
		this.gvlcGradient.CharacterHeaderColor = System.Drawing.Color.Black;
		this.gvlcGradient.ColumnHeadersHeight = 32;
		this.gvlcGradient.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
		this.gvlcGradient.Dock = System.Windows.Forms.DockStyle.Left;
		this.gvlcGradient.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
		this.gvlcGradient.Location = new System.Drawing.Point(0, 0);
		this.gvlcGradient.Name = "gvlcGradient";
		this.gvlcGradient.ReadOnly = true;
		this.gvlcGradient.RowHeadersWidth = 25;
		this.gvlcGradient.RowTemplate.Height = 16;
		this.gvlcGradient.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
		this.gvlcGradient.ShowCellToolTips = false;
		this.gvlcGradient.Size = new System.Drawing.Size(395, 231);
		this.gvlcGradient.TabIndex = 5;
		this.tpGC.Controls.Add(this.gvgcProgTemp);
		this.tpGC.Controls.Add(this.dgvCT6);
		this.tpGC.Location = new System.Drawing.Point(4, 23);
		this.tpGC.Name = "tpGC";
		this.tpGC.Size = new System.Drawing.Size(958, 231);
		this.tpGC.TabIndex = 3;
		this.tpGC.UseVisualStyleBackColor = true;
		this.gvgcProgTemp.AllowUserToAddRows = false;
		this.gvgcProgTemp.AllowUserToDeleteRows = false;
		this.gvgcProgTemp.AllowUserToResizeRows = false;
		this.gvgcProgTemp.BackgroundColor = System.Drawing.Color.AliceBlue;
		this.gvgcProgTemp.CharacterHeaderColor = System.Drawing.Color.Black;
		this.gvgcProgTemp.ColumnHeadersHeight = 32;
		this.gvgcProgTemp.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
		this.gvgcProgTemp.Dock = System.Windows.Forms.DockStyle.Left;
		this.gvgcProgTemp.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
		this.gvgcProgTemp.Location = new System.Drawing.Point(148, 0);
		this.gvgcProgTemp.Name = "gvgcProgTemp";
		this.gvgcProgTemp.ReadOnly = true;
		this.gvgcProgTemp.RowHeadersWidth = 25;
		this.gvgcProgTemp.RowTemplate.Height = 16;
		this.gvgcProgTemp.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
		this.gvgcProgTemp.ShowCellToolTips = false;
		this.gvgcProgTemp.Size = new System.Drawing.Size(238, 231);
		this.gvgcProgTemp.TabIndex = 6;
		this.dgvCT6.AllowUserToAddRows = false;
		this.dgvCT6.AllowUserToDeleteRows = false;
		this.dgvCT6.BackgroundColor = System.Drawing.Color.White;
		this.dgvCT6.BorderStyle = System.Windows.Forms.BorderStyle.None;
		dataGridViewCellStyle25.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
		dataGridViewCellStyle25.BackColor = System.Drawing.SystemColors.Control;
		dataGridViewCellStyle25.Font = new System.Drawing.Font("宋体", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		dataGridViewCellStyle25.ForeColor = System.Drawing.SystemColors.WindowText;
		dataGridViewCellStyle25.SelectionBackColor = System.Drawing.SystemColors.Highlight;
		dataGridViewCellStyle25.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
		dataGridViewCellStyle25.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
		this.dgvCT6.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle25;
		this.dgvCT6.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
		this.dgvCT6.Columns.AddRange(this.clmCT6CN, this.clmCT6SetT);
		this.dgvCT6.Dock = System.Windows.Forms.DockStyle.Left;
		this.dgvCT6.EnableHeadersVisualStyles = false;
		this.dgvCT6.Location = new System.Drawing.Point(0, 0);
		this.dgvCT6.MultiSelect = false;
		this.dgvCT6.Name = "dgvCT6";
		this.dgvCT6.ReadOnly = true;
		this.dgvCT6.RowHeadersVisible = false;
		this.dgvCT6.RowHeadersWidth = 80;
		this.dgvCT6.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
		this.dgvCT6.RowTemplate.Height = 18;
		this.dgvCT6.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.dgvCT6.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
		this.dgvCT6.ShowEditingIcon = false;
		this.dgvCT6.Size = new System.Drawing.Size(148, 231);
		this.dgvCT6.TabIndex = 7;
		this.clmCT6CN.HeaderText = "";
		this.clmCT6CN.Name = "clmCT6CN";
		this.clmCT6CN.ReadOnly = true;
		this.clmCT6CN.Width = 60;
		dataGridViewCellStyle26.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
		dataGridViewCellStyle26.BackColor = System.Drawing.Color.White;
		dataGridViewCellStyle26.Format = "0.0";
		this.clmCT6SetT.DefaultCellStyle = dataGridViewCellStyle26;
		this.clmCT6SetT.HeaderText = "设定[℃]";
		this.clmCT6SetT.Name = "clmCT6SetT";
		this.clmCT6SetT.ReadOnly = true;
		this.clmCT6SetT.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.clmCT6SetT.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.clmCT6SetT.Width = 60;
		this.tpSST.Controls.Add(this.gvSSTResults);
		this.tpSST.Controls.Add(this.lbSSTExpress);
		this.tpSST.Controls.Add(this.pnlControl);
		this.tpSST.Location = new System.Drawing.Point(4, 23);
		this.tpSST.Name = "tpSST";
		this.tpSST.Size = new System.Drawing.Size(974, 291);
		this.tpSST.TabIndex = 5;
		this.tpSST.Text = "组分验证";
		this.tpSST.UseVisualStyleBackColor = true;
		this.gvSSTResults.AllowUserToAddRows = false;
		this.gvSSTResults.AllowUserToDeleteRows = false;
		this.gvSSTResults.AllowUserToResizeRows = false;
		this.gvSSTResults.BackgroundColor = System.Drawing.Color.AliceBlue;
		this.gvSSTResults.CharacterHeaderColor = System.Drawing.Color.Black;
		dataGridViewCellStyle27.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
		dataGridViewCellStyle27.BackColor = System.Drawing.SystemColors.Control;
		dataGridViewCellStyle27.Font = new System.Drawing.Font("宋体", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		dataGridViewCellStyle27.ForeColor = System.Drawing.SystemColors.WindowText;
		dataGridViewCellStyle27.SelectionBackColor = System.Drawing.SystemColors.Highlight;
		dataGridViewCellStyle27.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
		dataGridViewCellStyle27.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
		this.gvSSTResults.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle27;
		this.gvSSTResults.ColumnHeadersHeight = 32;
		this.gvSSTResults.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
		this.gvSSTResults.ContextMenuStrip = this.cmsSSTCmpds;
		dataGridViewCellStyle28.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
		dataGridViewCellStyle28.BackColor = System.Drawing.SystemColors.Window;
		dataGridViewCellStyle28.Font = new System.Drawing.Font("宋体", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		dataGridViewCellStyle28.ForeColor = System.Drawing.SystemColors.ControlText;
		dataGridViewCellStyle28.SelectionBackColor = System.Drawing.SystemColors.Highlight;
		dataGridViewCellStyle28.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
		dataGridViewCellStyle28.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
		this.gvSSTResults.DefaultCellStyle = dataGridViewCellStyle28;
		this.gvSSTResults.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
		this.gvSSTResults.Location = new System.Drawing.Point(358, 81);
		this.gvSSTResults.MultiSelect = false;
		this.gvSSTResults.Name = "gvSSTResults";
		dataGridViewCellStyle29.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
		dataGridViewCellStyle29.BackColor = System.Drawing.SystemColors.Control;
		dataGridViewCellStyle29.Font = new System.Drawing.Font("宋体", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		dataGridViewCellStyle29.ForeColor = System.Drawing.SystemColors.WindowText;
		dataGridViewCellStyle29.SelectionBackColor = System.Drawing.SystemColors.Highlight;
		dataGridViewCellStyle29.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
		dataGridViewCellStyle29.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
		this.gvSSTResults.RowHeadersDefaultCellStyle = dataGridViewCellStyle29;
		this.gvSSTResults.RowHeadersWidth = 25;
		this.gvSSTResults.RowTemplate.Height = 16;
		this.gvSSTResults.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
		this.gvSSTResults.ShowCellToolTips = false;
		this.gvSSTResults.Size = new System.Drawing.Size(137, 68);
		this.gvSSTResults.TabIndex = 2;
		this.gvSSTResults.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(gvSSTResults_CellEndEdit);
		this.gvSSTResults.MouseDown += new System.Windows.Forms.MouseEventHandler(gvSSTCmpds_MouseDown);
		this.cmsSSTCmpds.Items.AddRange(new System.Windows.Forms.ToolStripItem[12]
		{
			this.misstcNew, this.misstcOpen, this.misstcSave, this.misstcSaveas, this.misstcUpdateFromCalib, this.toolStripSeparator34, this.misstcSet, this.tss1, this.misstColumnsSetup, this.misstRestoreDftColumns,
			this.tss2, this.misstcClearParas
		});
		this.cmsSSTCmpds.Name = "cmsSSTCmpds";
		this.cmsSSTCmpds.ShowImageMargin = false;
		this.cmsSSTCmpds.Size = new System.Drawing.Size(190, 220);
		this.misstcNew.Name = "misstcNew";
		this.misstcNew.Size = new System.Drawing.Size(189, 22);
		this.misstcNew.Text = "misstcNew";
		this.misstcNew.Click += new System.EventHandler(misstcClearParas_Click);
		this.misstcOpen.Name = "misstcOpen";
		this.misstcOpen.Size = new System.Drawing.Size(189, 22);
		this.misstcOpen.Text = "misstcOpen";
		this.misstcOpen.Click += new System.EventHandler(misstcClearParas_Click);
		this.misstcSave.Name = "misstcSave";
		this.misstcSave.Size = new System.Drawing.Size(189, 22);
		this.misstcSave.Text = "misstcSave";
		this.misstcSave.Click += new System.EventHandler(misstcClearParas_Click);
		this.misstcSaveas.Name = "misstcSaveas";
		this.misstcSaveas.Size = new System.Drawing.Size(189, 22);
		this.misstcSaveas.Text = "misstcSaveas";
		this.misstcSaveas.Click += new System.EventHandler(misstcClearParas_Click);
		this.misstcUpdateFromCalib.Name = "misstcUpdateFromCalib";
		this.misstcUpdateFromCalib.Size = new System.Drawing.Size(189, 22);
		this.misstcUpdateFromCalib.Text = "misstcUpdateFromCalib";
		this.misstcUpdateFromCalib.Click += new System.EventHandler(misstcClearParas_Click);
		this.toolStripSeparator34.Name = "toolStripSeparator34";
		this.toolStripSeparator34.Size = new System.Drawing.Size(186, 6);
		this.misstcSet.Name = "misstcSet";
		this.misstcSet.Size = new System.Drawing.Size(189, 22);
		this.misstcSet.Text = "misstcSet";
		this.misstcSet.Click += new System.EventHandler(misstcClearParas_Click);
		this.tss1.Name = "tss1";
		this.tss1.Size = new System.Drawing.Size(186, 6);
		this.misstColumnsSetup.Name = "misstColumnsSetup";
		this.misstColumnsSetup.Size = new System.Drawing.Size(189, 22);
		this.misstColumnsSetup.Text = "列设置...";
		this.misstColumnsSetup.Click += new System.EventHandler(misstRestoreDftColumns_Click);
		this.misstRestoreDftColumns.Name = "misstRestoreDftColumns";
		this.misstRestoreDftColumns.Size = new System.Drawing.Size(189, 22);
		this.misstRestoreDftColumns.Text = "恢复默认列设置";
		this.misstRestoreDftColumns.Click += new System.EventHandler(misstRestoreDftColumns_Click);
		this.tss2.Name = "tss2";
		this.tss2.Size = new System.Drawing.Size(186, 6);
		this.misstcClearParas.Name = "misstcClearParas";
		this.misstcClearParas.Size = new System.Drawing.Size(189, 22);
		this.misstcClearParas.Text = "misstcClearParas";
		this.misstcClearParas.Click += new System.EventHandler(misstcClearParas_Click);
		this.lbSSTExpress.Dock = System.Windows.Forms.DockStyle.Top;
		this.lbSSTExpress.Location = new System.Drawing.Point(313, 0);
		this.lbSSTExpress.Name = "lbSSTExpress";
		this.lbSSTExpress.Size = new System.Drawing.Size(661, 0);
		this.lbSSTExpress.TabIndex = 1;
		this.lbSSTExpress.Text = "[]";
		this.lbSSTExpress.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.pnlControl.Controls.Add(this.gvSSTCmpds);
		this.pnlControl.Controls.Add(this.lbSSTFile);
		this.pnlControl.Dock = System.Windows.Forms.DockStyle.Left;
		this.pnlControl.Location = new System.Drawing.Point(0, 0);
		this.pnlControl.Name = "pnlControl";
		this.pnlControl.Size = new System.Drawing.Size(313, 291);
		this.pnlControl.TabIndex = 0;
		this.gvSSTCmpds.AllowUserToAddRows = false;
		this.gvSSTCmpds.AllowUserToDeleteRows = false;
		this.gvSSTCmpds.AllowUserToResizeRows = false;
		this.gvSSTCmpds.BackgroundColor = System.Drawing.Color.AliceBlue;
		this.gvSSTCmpds.CharacterHeaderColor = System.Drawing.Color.Black;
		dataGridViewCellStyle30.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
		dataGridViewCellStyle30.BackColor = System.Drawing.SystemColors.Control;
		dataGridViewCellStyle30.Font = new System.Drawing.Font("宋体", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		dataGridViewCellStyle30.ForeColor = System.Drawing.SystemColors.WindowText;
		dataGridViewCellStyle30.SelectionBackColor = System.Drawing.SystemColors.Highlight;
		dataGridViewCellStyle30.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
		dataGridViewCellStyle30.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
		this.gvSSTCmpds.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle30;
		this.gvSSTCmpds.ColumnHeadersHeight = 32;
		this.gvSSTCmpds.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
		this.gvSSTCmpds.ContextMenuStrip = this.cmsSSTCmpds;
		dataGridViewCellStyle31.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
		dataGridViewCellStyle31.BackColor = System.Drawing.SystemColors.Window;
		dataGridViewCellStyle31.Font = new System.Drawing.Font("宋体", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		dataGridViewCellStyle31.ForeColor = System.Drawing.SystemColors.ControlText;
		dataGridViewCellStyle31.SelectionBackColor = System.Drawing.SystemColors.Highlight;
		dataGridViewCellStyle31.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
		dataGridViewCellStyle31.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
		this.gvSSTCmpds.DefaultCellStyle = dataGridViewCellStyle31;
		this.gvSSTCmpds.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
		this.gvSSTCmpds.Location = new System.Drawing.Point(46, 81);
		this.gvSSTCmpds.MultiSelect = false;
		this.gvSSTCmpds.Name = "gvSSTCmpds";
		dataGridViewCellStyle32.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
		dataGridViewCellStyle32.BackColor = System.Drawing.SystemColors.Control;
		dataGridViewCellStyle32.Font = new System.Drawing.Font("宋体", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		dataGridViewCellStyle32.ForeColor = System.Drawing.SystemColors.WindowText;
		dataGridViewCellStyle32.SelectionBackColor = System.Drawing.SystemColors.Highlight;
		dataGridViewCellStyle32.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
		dataGridViewCellStyle32.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
		this.gvSSTCmpds.RowHeadersDefaultCellStyle = dataGridViewCellStyle32;
		this.gvSSTCmpds.RowHeadersWidth = 25;
		this.gvSSTCmpds.RowTemplate.Height = 16;
		this.gvSSTCmpds.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
		this.gvSSTCmpds.ShowCellToolTips = false;
		this.gvSSTCmpds.Size = new System.Drawing.Size(145, 68);
		this.gvSSTCmpds.TabIndex = 1;
		this.gvSSTCmpds.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(gvSSTCmpds_CellEndEdit);
		this.gvSSTCmpds.SelectionChanged += new System.EventHandler(gvSSTCmpds_SelectionChanged);
		this.gvSSTCmpds.MouseDown += new System.Windows.Forms.MouseEventHandler(gvSSTCmpds_MouseDown);
		this.lbSSTFile.Dock = System.Windows.Forms.DockStyle.Top;
		this.lbSSTFile.Location = new System.Drawing.Point(0, 0);
		this.lbSSTFile.Name = "lbSSTFile";
		this.lbSSTFile.Size = new System.Drawing.Size(313, 0);
		this.lbSSTFile.TabIndex = 0;
		this.lbSSTFile.Text = "[]";
		this.lbSSTFile.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.tpSlices.Controls.Add(this.gvSlices);
		this.tpSlices.Controls.Add(this.tbslcAverNum);
		this.tpSlices.Controls.Add(this.lbslcAverNum);
		this.tpSlices.Controls.Add(this.lclExpressLabel5);
		this.tpSlices.Location = new System.Drawing.Point(4, 23);
		this.tpSlices.Name = "tpSlices";
		this.tpSlices.Size = new System.Drawing.Size(974, 291);
		this.tpSlices.TabIndex = 6;
		this.tpSlices.Text = "切片";
		this.tpSlices.UseVisualStyleBackColor = true;
		this.gvSlices.AllowUserToAddRows = false;
		this.gvSlices.AllowUserToDeleteRows = false;
		this.gvSlices.AllowUserToResizeRows = false;
		this.gvSlices.BackgroundColor = System.Drawing.Color.AliceBlue;
		this.gvSlices.CharacterHeaderColor = System.Drawing.Color.Black;
		dataGridViewCellStyle33.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
		dataGridViewCellStyle33.BackColor = System.Drawing.SystemColors.Control;
		dataGridViewCellStyle33.Font = new System.Drawing.Font("宋体", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		dataGridViewCellStyle33.ForeColor = System.Drawing.SystemColors.WindowText;
		dataGridViewCellStyle33.SelectionBackColor = System.Drawing.SystemColors.Highlight;
		dataGridViewCellStyle33.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
		dataGridViewCellStyle33.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
		this.gvSlices.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle33;
		this.gvSlices.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
		this.gvSlices.ContextMenuStrip = this.cmsSlices;
		dataGridViewCellStyle34.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
		dataGridViewCellStyle34.BackColor = System.Drawing.SystemColors.Window;
		dataGridViewCellStyle34.Font = new System.Drawing.Font("宋体", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		dataGridViewCellStyle34.ForeColor = System.Drawing.SystemColors.ControlText;
		dataGridViewCellStyle34.SelectionBackColor = System.Drawing.SystemColors.Highlight;
		dataGridViewCellStyle34.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
		dataGridViewCellStyle34.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
		this.gvSlices.DefaultCellStyle = dataGridViewCellStyle34;
		this.gvSlices.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
		this.gvSlices.Location = new System.Drawing.Point(68, 81);
		this.gvSlices.Name = "gvSlices";
		dataGridViewCellStyle35.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
		dataGridViewCellStyle35.BackColor = System.Drawing.SystemColors.Control;
		dataGridViewCellStyle35.Font = new System.Drawing.Font("宋体", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		dataGridViewCellStyle35.ForeColor = System.Drawing.SystemColors.WindowText;
		dataGridViewCellStyle35.SelectionBackColor = System.Drawing.SystemColors.Highlight;
		dataGridViewCellStyle35.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
		dataGridViewCellStyle35.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
		this.gvSlices.RowHeadersDefaultCellStyle = dataGridViewCellStyle35;
		this.gvSlices.RowHeadersWidth = 25;
		this.gvSlices.RowTemplate.Height = 16;
		this.gvSlices.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
		this.gvSlices.ShowCellToolTips = false;
		this.gvSlices.Size = new System.Drawing.Size(166, 85);
		this.gvSlices.TabIndex = 3;
		this.cmsSlices.Items.AddRange(new System.Windows.Forms.ToolStripItem[2] { this.mislcColumnsSetup, this.mislcRestoreDftColumns });
		this.cmsSlices.Name = "cmsSlices";
		this.cmsSlices.ShowImageMargin = false;
		this.cmsSlices.Size = new System.Drawing.Size(136, 48);
		this.mislcColumnsSetup.Name = "mislcColumnsSetup";
		this.mislcColumnsSetup.Size = new System.Drawing.Size(135, 22);
		this.mislcColumnsSetup.Text = "列设置...";
		this.mislcColumnsSetup.Click += new System.EventHandler(mislcRestoreDftColumns_Click);
		this.mislcRestoreDftColumns.Name = "mislcRestoreDftColumns";
		this.mislcRestoreDftColumns.Size = new System.Drawing.Size(135, 22);
		this.mislcRestoreDftColumns.Text = "恢复默认列设置";
		this.mislcRestoreDftColumns.Click += new System.EventHandler(mislcRestoreDftColumns_Click);
		this.tbslcAverNum.Location = new System.Drawing.Point(68, 2);
		this.tbslcAverNum.Name = "tbslcAverNum";
		this.tbslcAverNum.Size = new System.Drawing.Size(54, 21);
		this.tbslcAverNum.TabIndex = 2;
		this.lbslcAverNum.AutoSize = true;
		this.lbslcAverNum.Location = new System.Drawing.Point(3, 7);
		this.lbslcAverNum.Name = "lbslcAverNum";
		this.lbslcAverNum.Size = new System.Drawing.Size(53, 12);
		this.lbslcAverNum.TabIndex = 1;
		this.lbslcAverNum.Text = "平均点数";
		this.lclExpressLabel5.Dock = System.Windows.Forms.DockStyle.Top;
		this.lclExpressLabel5.Location = new System.Drawing.Point(0, 0);
		this.lclExpressLabel5.Name = "lclExpressLabel5";
		this.lclExpressLabel5.Size = new System.Drawing.Size(974, 0);
		this.lclExpressLabel5.TabIndex = 0;
		this.lclExpressLabel5.Text = "lclExpressLabel5";
		this.lclExpressLabel5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
		this.tpRanges.Controls.Add(this.gvgrMw);
		this.tpRanges.Controls.Add(this.gvgrPercent);
		this.tpRanges.Controls.Add(this.lbgrMw);
		this.tpRanges.Controls.Add(this.lbgrPercent);
		this.tpRanges.Location = new System.Drawing.Point(4, 23);
		this.tpRanges.Name = "tpRanges";
		this.tpRanges.Size = new System.Drawing.Size(974, 291);
		this.tpRanges.TabIndex = 7;
		this.tpRanges.Text = "分段";
		this.tpRanges.UseVisualStyleBackColor = true;
		this.gvgrMw.AllowUserToAddRows = false;
		this.gvgrMw.AllowUserToDeleteRows = false;
		this.gvgrMw.AllowUserToResizeRows = false;
		this.gvgrMw.BackgroundColor = System.Drawing.Color.AliceBlue;
		this.gvgrMw.CharacterHeaderColor = System.Drawing.Color.Black;
		this.gvgrMw.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
		this.gvgrMw.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
		this.gvgrMw.Location = new System.Drawing.Point(397, 20);
		this.gvgrMw.Name = "gvgrMw";
		this.gvgrMw.RowHeadersWidth = 25;
		this.gvgrMw.RowTemplate.Height = 16;
		this.gvgrMw.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
		this.gvgrMw.ShowCellToolTips = false;
		this.gvgrMw.Size = new System.Drawing.Size(384, 235);
		this.gvgrMw.TabIndex = 1;
		this.gvgrPercent.AllowUserToAddRows = false;
		this.gvgrPercent.AllowUserToDeleteRows = false;
		this.gvgrPercent.AllowUserToResizeRows = false;
		this.gvgrPercent.BackgroundColor = System.Drawing.Color.AliceBlue;
		this.gvgrPercent.CharacterHeaderColor = System.Drawing.Color.Black;
		this.gvgrPercent.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
		this.gvgrPercent.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
		this.gvgrPercent.Location = new System.Drawing.Point(7, 20);
		this.gvgrPercent.Name = "gvgrPercent";
		this.gvgrPercent.RowHeadersWidth = 25;
		this.gvgrPercent.RowTemplate.Height = 16;
		this.gvgrPercent.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
		this.gvgrPercent.ShowCellToolTips = false;
		this.gvgrPercent.Size = new System.Drawing.Size(273, 125);
		this.gvgrPercent.TabIndex = 1;
		this.lbgrMw.AutoSize = true;
		this.lbgrMw.Location = new System.Drawing.Point(395, 5);
		this.lbgrMw.Name = "lbgrMw";
		this.lbgrMw.Size = new System.Drawing.Size(95, 12);
		this.lbgrMw.TabIndex = 0;
		this.lbgrMw.Text = "分子量类型GPC表";
		this.lbgrPercent.AutoSize = true;
		this.lbgrPercent.Location = new System.Drawing.Point(5, 5);
		this.lbgrPercent.Name = "lbgrPercent";
		this.lbgrPercent.Size = new System.Drawing.Size(95, 12);
		this.lbgrPercent.TabIndex = 0;
		this.lbgrPercent.Text = "百分比类型GPC表";
		this.tpRightsArchives.Controls.Add(this.spltcArchives);
		this.tpRightsArchives.Controls.Add(this.lclPanel1);
		this.tpRightsArchives.Controls.Add(this.pnlSetRights);
		this.tpRightsArchives.Location = new System.Drawing.Point(4, 23);
		this.tpRightsArchives.Name = "tpRightsArchives";
		this.tpRightsArchives.Size = new System.Drawing.Size(974, 291);
		this.tpRightsArchives.TabIndex = 8;
		this.tpRightsArchives.UseVisualStyleBackColor = true;
		this.spltcArchives.Location = new System.Drawing.Point(365, 33);
		this.spltcArchives.Name = "spltcArchives";
		this.spltcArchives.Orientation = System.Windows.Forms.Orientation.Horizontal;
		this.spltcArchives.Panel1.Controls.Add(this.gvArchives);
		this.spltcArchives.Panel2.Controls.Add(this.lclLabel6);
		this.spltcArchives.Panel2.Controls.Add(this.tbrtaRemark);
		this.spltcArchives.Size = new System.Drawing.Size(331, 237);
		this.spltcArchives.SplitterDistance = 162;
		this.spltcArchives.SplitterWidth = 6;
		this.spltcArchives.TabIndex = 12;
		this.gvArchives.AllowUserToAddRows = false;
		this.gvArchives.AllowUserToDeleteRows = false;
		this.gvArchives.AllowUserToResizeRows = false;
		this.gvArchives.BackgroundColor = System.Drawing.Color.AliceBlue;
		this.gvArchives.CharacterHeaderColor = System.Drawing.Color.Black;
		dataGridViewCellStyle36.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
		dataGridViewCellStyle36.BackColor = System.Drawing.SystemColors.Control;
		dataGridViewCellStyle36.Font = new System.Drawing.Font("宋体", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		dataGridViewCellStyle36.ForeColor = System.Drawing.SystemColors.WindowText;
		dataGridViewCellStyle36.SelectionBackColor = System.Drawing.SystemColors.Highlight;
		dataGridViewCellStyle36.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
		dataGridViewCellStyle36.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
		this.gvArchives.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle36;
		this.gvArchives.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
		dataGridViewCellStyle37.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
		dataGridViewCellStyle37.BackColor = System.Drawing.SystemColors.Window;
		dataGridViewCellStyle37.Font = new System.Drawing.Font("宋体", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		dataGridViewCellStyle37.ForeColor = System.Drawing.Color.Black;
		dataGridViewCellStyle37.SelectionBackColor = System.Drawing.SystemColors.Highlight;
		dataGridViewCellStyle37.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
		dataGridViewCellStyle37.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
		this.gvArchives.DefaultCellStyle = dataGridViewCellStyle37;
		this.gvArchives.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
		this.gvArchives.Location = new System.Drawing.Point(20, 17);
		this.gvArchives.MultiSelect = false;
		this.gvArchives.Name = "gvArchives";
		this.gvArchives.ReadOnly = true;
		dataGridViewCellStyle38.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
		dataGridViewCellStyle38.BackColor = System.Drawing.SystemColors.Control;
		dataGridViewCellStyle38.Font = new System.Drawing.Font("宋体", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		dataGridViewCellStyle38.ForeColor = System.Drawing.SystemColors.WindowText;
		dataGridViewCellStyle38.SelectionBackColor = System.Drawing.SystemColors.Highlight;
		dataGridViewCellStyle38.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
		dataGridViewCellStyle38.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
		this.gvArchives.RowHeadersDefaultCellStyle = dataGridViewCellStyle38;
		this.gvArchives.RowHeadersWidth = 25;
		this.gvArchives.RowTemplate.Height = 16;
		this.gvArchives.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
		this.gvArchives.ShowCellToolTips = false;
		this.gvArchives.Size = new System.Drawing.Size(120, 86);
		this.gvArchives.TabIndex = 7;
		this.gvArchives.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(gvArchives_CellDoubleClick);
		this.gvArchives.CellMouseUp += new System.Windows.Forms.DataGridViewCellMouseEventHandler(gvSetRights_CellMouseUp);
		this.gvArchives.SelectionChanged += new System.EventHandler(gvArchives_SelectionChanged);
		this.lclLabel6.AutoSize = true;
		this.lclLabel6.Location = new System.Drawing.Point(3, 8);
		this.lclLabel6.Name = "lclLabel6";
		this.lclLabel6.Size = new System.Drawing.Size(0, 12);
		this.lclLabel6.TabIndex = 8;
		this.tbrtaRemark.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.tbrtaRemark.Location = new System.Drawing.Point(50, 5);
		this.tbrtaRemark.Multiline = true;
		this.tbrtaRemark.Name = "tbrtaRemark";
		this.tbrtaRemark.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
		this.tbrtaRemark.Size = new System.Drawing.Size(276, 57);
		this.tbrtaRemark.TabIndex = 10;
		this.lclPanel1.Controls.Add(this.lclLabel4);
		this.lclPanel1.Dock = System.Windows.Forms.DockStyle.Top;
		this.lclPanel1.Location = new System.Drawing.Point(333, 0);
		this.lclPanel1.Name = "lclPanel1";
		this.lclPanel1.Size = new System.Drawing.Size(641, 27);
		this.lclPanel1.TabIndex = 9;
		this.lclLabel4.AutoSize = true;
		this.lclLabel4.Location = new System.Drawing.Point(9, 8);
		this.lclLabel4.Name = "lclLabel4";
		this.lclLabel4.Size = new System.Drawing.Size(0, 12);
		this.lclLabel4.TabIndex = 8;
		this.pnlSetRights.Controls.Add(this.gvSetRights);
		this.pnlSetRights.Controls.Add(this.lclPanel2);
		this.pnlSetRights.Dock = System.Windows.Forms.DockStyle.Left;
		this.pnlSetRights.Location = new System.Drawing.Point(0, 0);
		this.pnlSetRights.Name = "pnlSetRights";
		this.pnlSetRights.Size = new System.Drawing.Size(333, 291);
		this.pnlSetRights.TabIndex = 1;
		this.gvSetRights.AllowUserToAddRows = false;
		this.gvSetRights.AllowUserToDeleteRows = false;
		this.gvSetRights.AllowUserToResizeRows = false;
		this.gvSetRights.BackgroundColor = System.Drawing.Color.AliceBlue;
		this.gvSetRights.CharacterHeaderColor = System.Drawing.Color.Black;
		dataGridViewCellStyle39.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
		dataGridViewCellStyle39.BackColor = System.Drawing.SystemColors.Control;
		dataGridViewCellStyle39.Font = new System.Drawing.Font("宋体", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		dataGridViewCellStyle39.ForeColor = System.Drawing.SystemColors.WindowText;
		dataGridViewCellStyle39.SelectionBackColor = System.Drawing.SystemColors.Highlight;
		dataGridViewCellStyle39.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
		dataGridViewCellStyle39.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
		this.gvSetRights.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle39;
		this.gvSetRights.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
		this.gvSetRights.ContextMenuStrip = this.cmsRltGV;
		dataGridViewCellStyle40.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
		dataGridViewCellStyle40.BackColor = System.Drawing.SystemColors.Window;
		dataGridViewCellStyle40.Font = new System.Drawing.Font("宋体", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		dataGridViewCellStyle40.ForeColor = System.Drawing.Color.Black;
		dataGridViewCellStyle40.SelectionBackColor = System.Drawing.SystemColors.Highlight;
		dataGridViewCellStyle40.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
		dataGridViewCellStyle40.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
		this.gvSetRights.DefaultCellStyle = dataGridViewCellStyle40;
		this.gvSetRights.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
		this.gvSetRights.Location = new System.Drawing.Point(12, 62);
		this.gvSetRights.Name = "gvSetRights";
		dataGridViewCellStyle41.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
		dataGridViewCellStyle41.BackColor = System.Drawing.SystemColors.Control;
		dataGridViewCellStyle41.Font = new System.Drawing.Font("宋体", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		dataGridViewCellStyle41.ForeColor = System.Drawing.SystemColors.WindowText;
		dataGridViewCellStyle41.SelectionBackColor = System.Drawing.SystemColors.Highlight;
		dataGridViewCellStyle41.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
		dataGridViewCellStyle41.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
		this.gvSetRights.RowHeadersDefaultCellStyle = dataGridViewCellStyle41;
		this.gvSetRights.RowHeadersWidth = 25;
		this.gvSetRights.RowTemplate.Height = 16;
		this.gvSetRights.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
		this.gvSetRights.ShowCellToolTips = false;
		this.gvSetRights.Size = new System.Drawing.Size(148, 90);
		this.gvSetRights.TabIndex = 7;
		this.gvSetRights.CellMouseUp += new System.Windows.Forms.DataGridViewCellMouseEventHandler(gvSetRights_CellMouseUp);
		this.lclPanel2.Controls.Add(this.lclLabel2);
		this.lclPanel2.Controls.Add(this.cbrtaCanSetRs);
		this.lclPanel2.Dock = System.Windows.Forms.DockStyle.Top;
		this.lclPanel2.Location = new System.Drawing.Point(0, 0);
		this.lclPanel2.Name = "lclPanel2";
		this.lclPanel2.Size = new System.Drawing.Size(333, 45);
		this.lclPanel2.TabIndex = 0;
		this.lclLabel2.AutoSize = true;
		this.lclLabel2.Location = new System.Drawing.Point(10, 8);
		this.lclLabel2.Name = "lclLabel2";
		this.lclLabel2.Size = new System.Drawing.Size(0, 12);
		this.lclLabel2.TabIndex = 8;
		this.cbrtaCanSetRs.AutoSize = true;
		this.cbrtaCanSetRs.Location = new System.Drawing.Point(12, 25);
		this.cbrtaCanSetRs.Name = "cbrtaCanSetRs";
		this.cbrtaCanSetRs.Size = new System.Drawing.Size(15, 14);
		this.cbrtaCanSetRs.TabIndex = 0;
		this.cbrtaCanSetRs.UseVisualStyleBackColor = true;
		this.cbrtaCanSetRs.Click += new System.EventHandler(cbrtaCanSetRs_Click);
		this.splitContainer.Location = new System.Drawing.Point(8, 103);
		this.splitContainer.Name = "splitContainer";
		this.splitContainer.Orientation = System.Windows.Forms.Orientation.Horizontal;
		this.splitContainer.Panel1.Controls.Add(this.lbExpress);
		this.splitContainer.Panel1.Controls.Add(this.dpgnlChrom);
		this.splitContainer.Panel1.Controls.Add(this.tcGPC);
		this.splitContainer.Panel2.Controls.Add(this.tcChrom);
		this.splitContainer.Size = new System.Drawing.Size(1014, 441);
		this.splitContainer.SplitterDistance = 114;
		this.splitContainer.SplitterWidth = 6;
		this.splitContainer.TabIndex = 13;
		this.splitContainer.SplitterMoved += new System.Windows.Forms.SplitterEventHandler(splitContainer_SplitterMoved);
		base.ClientSize = new System.Drawing.Size(1047, 573);
		base.Controls.Add(this.splitContainer);
		base.Controls.Add(this.flpChrom);
		base.Controls.Add(this.ssChrom);
		base.Controls.Add(this.msChrom);
		base.MainMenuStrip = this.msChrom;
		base.Name = "VIChromForm";
		this.Text = "谱图处理";
		base.Load += new System.EventHandler(VIChromForm_Load);
		base.KeyDown += new System.Windows.Forms.KeyEventHandler(VIChromForm_KeyDown);
		this.msChrom.ResumeLayout(false);
		this.msChrom.PerformLayout();
		this.flpChrom.ResumeLayout(false);
		this.flpChrom.PerformLayout();
		this.toolStrip1.ResumeLayout(false);
		this.toolStrip1.PerformLayout();
		this.toolStrip2.ResumeLayout(false);
		this.toolStrip2.PerformLayout();
		this.tsDatAcq.ResumeLayout(false);
		this.tsDatAcq.PerformLayout();
		this.ssChrom.ResumeLayout(false);
		this.ssChrom.PerformLayout();
		this.tcGPC.ResumeLayout(false);
		this.tpgpcChrom.ResumeLayout(false);
		this.tpgpcMwDistrib.ResumeLayout(false);
		this.tpgpcCmlMw.ResumeLayout(false);
		this.tcChrom.ResumeLayout(false);
		this.tpResults.ResumeLayout(false);
		this.pnlgcu.ResumeLayout(false);
		this.pnlgcu.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.gvRltsDad).EndInit();
		this.cmsRltGV.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.gvRltsGpc).EndInit();
		((System.ComponentModel.ISupportInitialize)this.gvRltsGnl).EndInit();
		this.pnlRltsControl.ResumeLayout(false);
		this.pnlcu.ResumeLayout(false);
		this.pnlcu.PerformLayout();
		this.gbcuUncalPeaks.ResumeLayout(false);
		this.gbcuUncalPeaks.PerformLayout();
		this.gbCalibration.ResumeLayout(false);
		this.gbCalibration.PerformLayout();
		this.gbcuScale.ResumeLayout(false);
		this.gbcuScale.PerformLayout();
		this.gbcuRltTableReport.ResumeLayout(false);
		this.gbcuRltTableReport.PerformLayout();
		this.tpSummary.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.gvSummary).EndInit();
		this.cmsSummary.ResumeLayout(false);
		this.tpPerformance.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.gvPerformFrom50).EndInit();
		this.cmsPerformance.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.gvPerformStatic).EndInit();
		this.pnlpfmControl.ResumeLayout(false);
		this.pnlpfmControl.PerformLayout();
		this.gbpfmColumnCalcu.ResumeLayout(false);
		this.gbpfmColumnCalcu.PerformLayout();
		this.tpIntegration.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.gvInteg).EndInit();
		this.cmsInteg.ResumeLayout(false);
		this.tpMsmCdts.ResumeLayout(false);
		this.tcMsmCdts.ResumeLayout(false);
		this.tpInstrument.ResumeLayout(false);
		this.gbinsAddSub.ResumeLayout(false);
		this.gbinsAddSub.PerformLayout();
		this.gbinsCdts.ResumeLayout(false);
		this.gbinsCdts.PerformLayout();
		this.gbinsSampleIdt.ResumeLayout(false);
		this.gbinsSampleIdt.PerformLayout();
		this.gbinsAcqParas.ResumeLayout(false);
		this.gbinsAcqParas.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.pbapES).EndInit();
		this.tpPDAMethod.ResumeLayout(false);
		this.gbpdaLibs.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.gvlibPDA).EndInit();
		this.cmsLibs.ResumeLayout(false);
		this.gbpdaLibSearchOptions.ResumeLayout(false);
		this.gbpdaLibSearchOptions.PerformLayout();
		this.gbpdaPeakPurityOptions.ResumeLayout(false);
		this.gbpdaPeakPurityOptions.PerformLayout();
		this.gbppoUsedPoints.ResumeLayout(false);
		this.gbppoUsedPoints.PerformLayout();
		this.tpLC.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.gvlcGradient).EndInit();
		this.tpGC.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.gvgcProgTemp).EndInit();
		((System.ComponentModel.ISupportInitialize)this.dgvCT6).EndInit();
		this.tpSST.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.gvSSTResults).EndInit();
		this.cmsSSTCmpds.ResumeLayout(false);
		this.pnlControl.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.gvSSTCmpds).EndInit();
		this.tpSlices.ResumeLayout(false);
		this.tpSlices.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.gvSlices).EndInit();
		this.cmsSlices.ResumeLayout(false);
		this.tpRanges.ResumeLayout(false);
		this.tpRanges.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.gvgrMw).EndInit();
		((System.ComponentModel.ISupportInitialize)this.gvgrPercent).EndInit();
		this.tpRightsArchives.ResumeLayout(false);
		this.spltcArchives.Panel1.ResumeLayout(false);
		this.spltcArchives.Panel2.ResumeLayout(false);
		this.spltcArchives.Panel2.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.spltcArchives).EndInit();
		this.spltcArchives.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.gvArchives).EndInit();
		this.lclPanel1.ResumeLayout(false);
		this.lclPanel1.PerformLayout();
		this.pnlSetRights.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.gvSetRights).EndInit();
		this.lclPanel2.ResumeLayout(false);
		this.lclPanel2.PerformLayout();
		this.splitContainer.Panel1.ResumeLayout(false);
		this.splitContainer.Panel2.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.splitContainer).EndInit();
		this.splitContainer.ResumeLayout(false);
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
