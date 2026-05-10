using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace IBrainChrom2018;

public class DataAcqForm : LclGnlForm
{
	private const byte byte_0 = 10;

	private const float float_0 = 0.01f;

	private const string string_0 = "信号";

	private const string string_1 = "时轴";

	private const string string_2 = "馏分收集提示";

	private const string string_3 = "重置零";

	public const string scnmiAlyRunSingle = "运行单针";

	private const string string_4 = "置零";

	public const string scnmiAnalysis = "分析";

	private const string string_5 = "关闭背景谱图";

	private const string string_6 = "设置背景谱图...";

	private const string string_7 = "网格线";

	private const string string_8 = "数据采集";

	private const string string_9 = "Signal";

	private const string string_10 = "Time";

	private const string string_11 = "Frag Gather";

	private const string string_12 = "Reset Zero";

	public const string senmiAlyRunSingle = "Run Single";

	private const string string_13 = "Set Zero";

	public const string senmiAnalysis = "Analysis";

	private const string string_14 = "Close Background Chromatogram";

	private const string string_15 = "Set Background Chromatogram...";

	private const string string_16 = "Grid Lines";

	private const string string_17 = "Data Acquisition";

	public const int textboxHeight = 17;

	private ToolStripButton btnAbortAcquisition;

	private ToolStripButton btnAutoStop;

	private ToolStripButton btnNextZoom;

	public ToolStripButton btnPauseSequence;

	private ToolStripButton btnPreviousZoom;

	private ToolStripButton btnProperties;

	public ToolStripButton btnResumeSequence;

	public ToolStripButton btnRunSequence;

	private ToolStripButton btnRunSingle;

	private ToolStripButton btnSnapshot;

	private ToolStripButton btnStopAcquisition;

	public ToolStripButton btnStopSequence;

	private IContainer icontainer_2;

	private DisLg disLg_0 = default(DisLg);

	public FgDlg dlgFg = new FgDlg();

	private LclDisplayPanel dpDatAcq;

	private ToolStripLabel lbSignal;

	private ToolStripLabel lbSignalU;

	private ToolStripLabel lbTime;

	private ToolStripLabel lbTimeU;

	private ToolStripLabel lbyUnit;

	private ToolStripMenuItem miAlyAbortAcquisition;

	private ToolStripMenuItem miAlyFg;

	public ToolStripMenuItem miAlyPauseSequence;

	private ToolStripMenuItem miAlyResetZero;

	public ToolStripMenuItem miAlyResumeSequence;

	public ToolStripMenuItem miAlyRunSequence;

	private ToolStripMenuItem miAlyRunSingle;

	private ToolStripMenuItem miAlySetZero;

	private ToolStripMenuItem miAlySnapshot;

	private ToolStripMenuItem miAlyStopAcquisition;

	public ToolStripMenuItem miAlyStopSequence;

	private ToolStripMenuItem miAnalysis;

	private ToolStripMenuItem miDisNextZoom;

	private ToolStripMenuItem miDisplay;

	private ToolStripMenuItem miDisPreviousZoom;

	private ToolStripMenuItem miDisProperties;

	private ToolStripMenuItem miDisView;

	private ToolStripMenuItem miFiCloseBkChrom;

	private ToolStripMenuItem miFiExit;

	private ToolStripMenuItem miFile;

	private ToolStripMenuItem miFiSetBgChrom;

	private ToolStripMenuItem mivExtend;

	private ToolStripMenuItem mivFix;

	private ToolStripMenuItem toolStripMenuItem_0 = new ToolStripMenuItem();

	private MenuStrip msDatAcq;

	private RectangleF rectangleF_0;

	private OpenFileDialog openFileDialog_0;

	private SampleDisplay sampleDisplay_0;

	public ToolStripStatusLabel[] slbSignals;

	private ToolStripStatusLabel slbTime;

	private StatusStrip ssDatAcq;

	private ToolStripTextBox tbAutoStop;

	private ToolStripTextBox tbSigYBeg;

	private ToolStripTextBox tbSigYEnd;

	private ToolStripTextBox tbTime;

	private Timer timer_0;

	private ToolStripLabel toolStripLabel2;

	private ToolStripSeparator toolStripSeparator1;

	private ToolStripSeparator toolStripSeparator2;

	private ToolStripSeparator toolStripSeparator3;

	private ToolStripSeparator toolStripSeparator4;

	private ToolStripSeparator toolStripSeparator5;

	private ToolStripSeparator toolStripSeparator6;

	private ToolStripSeparator toolStripSeparator7;

	private ToolStripSeparator toolStripSeparator8;

	private ToolStripSeparator toolStripSeparator9;

	private ToolStrip tsDatAcq;

	public float DisRt => disLg_0.lgXBeg + disLg_0.lgX;

	private string sTitle => Lang.PS("数据采集", "Data Acquisition");

	public DataAcqForm(Instrument instrument)
	{
		InitializeComponent_2();
		lbSignalU.Text = Lang.PS("到", "to");
		miDisView.Text = Lang.PS("观察模式", "View Style");
		mivFix.Text = Lang.PS("固定横坐标宽度", "Fix X axis Width");
		mivExtend.Text = Lang.PS("延展横坐标宽度", "Extend X axis Width");
		base.instrument = instrument;
		sampleDisplay_0 = new SampleDisplay(WinStyle.DataAcq, dpDatAcq);
		sampleDisplay_0.IsDataAcq = true;
		sampleDisplay_0.OnSignalDoubleClick += method_5;
		LoadOptions();
		mivExtend_Click(mivFix, null);
	}

	public void ApplyMethod()
	{
		for (int i = 0; i < instrument.sglsSampling.Length; i++)
		{
			instrument.sglsSampling[i].linkLcGradient = instrument.methodSetup.chromInfoR.LcGradient;
			instrument.sglsSampling[i].linkGcProgTemp = instrument.methodSetup.chromInfoR.GcProgTemp;
		}
		GetAutoStopState();
	}

	private void btnAutoStop_Click(object sender, EventArgs e)
	{
		instrument.methodSetup.chromInfoR.AcqAutoStop = !instrument.methodSetup.chromInfoR.AcqAutoStop;
		GetAutoStopState();
	}

	public void ChangeDisLg()
	{
		float val = disLg_0.lgX / 10f;
		val = Math.Max(val, 0.01f);
		if (mivFix.Checked)
		{
			method_2(disLg_0.lgXBeg + val, disLg_0.lgX, disLg_0.lgYBeg, disLg_0.lgY);
		}
		if (mivExtend.Checked)
		{
			method_2(disLg_0.lgXBeg, disLg_0.lgX + val, disLg_0.lgYBeg, disLg_0.lgY);
		}
	}

	private void DataAcqForm_FormClosing(object sender, FormClosingEventArgs e)
	{
		timer_0.Enabled = false;
	}

	private void DataAcqForm_Load(object sender, EventArgs e)
	{
		dpDatAcq.Dock = DockStyle.Fill;
		lbyUnit.Text = Class49.MesureUnit();
		miFiExit.Click += base.miFiExit_Click;
		msDatAcq.Items.Add(miView);
		miView.DropDownItems.Add(new ToolStripSeparator());
		miView.DropDownItems.Add(toolStripMenuItem_0);
		miView.DropDownOpening += method_0;
		toolStripMenuItem_0.Click += toolStripMenuItem_0_Click;
		msDatAcq.Items.Add(miWindow);
		miWinDataAcq.Visible = false;
		msDatAcq.Items.Add(miHelp);
		msDatAcq.Items.Add(new ToolStripSeparator());
		msDatAcq.Items.Add(mubtnMainForm);
		msDatAcq.Items.Add(mubtnInstrument);
		msDatAcq.Items.Add(new ToolStripSeparator());
		msDatAcq.Items.Add(mubtnChromatogram);
		msDatAcq.Items.Add(mubtnCaliGnl);
		msDatAcq.Items.Add(mubtnCaliGpc);
		msDatAcq.Items.Add(mubtnSglAly);
		msDatAcq.Items.Add(mubtnSeqAly);
		msDatAcq.Items.Add(mubtnDevMonitor);
		base.Icon = SystemIconResource.smethod_9();
		ResourceImageLoad.SetCtrlBitmap(btnRunSingle, SystemBitmapResource4.smethod_2());
		ResourceImageLoad.SetCtrlBitmap(btnRunSequence, SystemBitmapResource9.smethod_14());
		ResourceImageLoad.SetCtrlBitmap(btnPauseSequence, SystemBitmapResource9.smethod_9());
		ResourceImageLoad.SetCtrlBitmap(btnResumeSequence, SystemBitmapResource9.smethod_11());
		ResourceImageLoad.SetCtrlBitmap(btnStopSequence, SystemBitmapResource9.smethod_17());
		ResourceImageLoad.SetCtrlBitmap(btnStopAcquisition, SystemBitmapResource9.smethod_17());
		ResourceImageLoad.SetCtrlBitmap(btnAbortAcquisition, SystemBitmapResource9.smethod_0());
		ResourceImageLoad.SetCtrlBitmap(btnSnapshot, SystemBitmapResource9.smethod_16());
		ResourceImageLoad.SetCtrlBitmap(btnProperties, SystemIconResource.smethod_57());
		ResourceImageLoad.SetCtrlBitmap(btnPreviousZoom, SystemIconResource.smethod_58());
		ResourceImageLoad.SetCtrlBitmap(btnNextZoom, SystemIconResource.smethod_56());
		ResourceImageLoad.SetCtrlBitmap(btnAutoStop, SystemBitmapResource4.smethod_0());
		LoadOptions();
		if (btnPauseSequence.Visible && btnResumeSequence.Visible)
		{
			btnStopSequence_Click(null, null);
		}
		InstruWinsInfo instruWinsInfo = instrument.user.instrusWinsInfo[0];
		if (instruWinsInfo.valid)
		{
			ReadWinInfo(instruWinsInfo.winInfos[3]);
		}
	}

	protected override void Dispose(bool disposing)
	{
		if (disposing && icontainer_2 != null)
		{
			icontainer_2.Dispose();
		}
		base.Dispose(disposing);
	}

	private void dpDatAcq_MouseDown(object sender, MouseEventArgs e)
	{
		if (e.Button == MouseButtons.Left)
		{
			sampleDisplay_0.ptScaleBegin = e.Location;
		}
	}

	private void dpDatAcq_MouseMove(object sender, MouseEventArgs e)
	{
		if (e.Button == MouseButtons.Left)
		{
			sampleDisplay_0.scaling = true;
			dpDatAcq.Refresh();
		}
		if (e.Button == MouseButtons.Right)
		{
			dpDatAcq.Cursor = Cursors.SizeAll;
			if (!sampleDisplay_0.moving)
			{
				sampleDisplay_0.stDisChain.MustAppendFrameLg(disLg_0);
			}
			Size szScr = new Size(e.X - sampleDisplay_0.mouseLocation.X, e.Y - sampleDisplay_0.mouseLocation.Y);
			SizeF sizeF = sampleDisplay_0.scrToLg(szScr, bool_0: true);
			disLg_0.lgXBeg -= sizeF.Width;
			disLg_0.lgYBeg += sizeF.Height;
			sampleDisplay_0.moving = true;
			sampleDisplay_0.stDisChain.ReplaceCurFrameLg(disLg_0);
			dpDatAcq.Refresh();
		}
		sampleDisplay_0.mouseLocation = e.Location;
	}

	private void dpDatAcq_MouseUp(object sender, MouseEventArgs e)
	{
		dpDatAcq.Cursor = Cursors.Default;
		if (sampleDisplay_0.moving)
		{
			method_3();
		}
		if (sampleDisplay_0.scaling && Math.Abs(sampleDisplay_0.ptScaleBegin.X - sampleDisplay_0.mouseLocation.X) > 10 && Math.Abs(sampleDisplay_0.ptScaleBegin.Y - sampleDisplay_0.mouseLocation.Y) > 10)
		{
			PointF pointF = sampleDisplay_0.scrToLg(sampleDisplay_0.ptScaleBegin, bool_0: true);
			PointF pointF2 = sampleDisplay_0.scrToLg(sampleDisplay_0.mouseLocation, bool_0: true);
			rectangleF_0.X = Math.Min(pointF.X, pointF2.X);
			rectangleF_0.Y = Math.Min(pointF.Y, pointF2.Y);
			rectangleF_0.Width = Math.Max(pointF.X, pointF2.X) - rectangleF_0.X;
			rectangleF_0.Height = Math.Max(pointF.Y, pointF2.Y) - rectangleF_0.Y;
			method_2(rectangleF_0.X, rectangleF_0.Width, rectangleF_0.Y, rectangleF_0.Height);
			method_3();
		}
		sampleDisplay_0.moving = false;
		sampleDisplay_0.scaling = false;
		dpDatAcq.Refresh();
	}

	private void dpDatAcq_Paint(object sender, PaintEventArgs e)
	{
		sampleDisplay_0.Draw(e.Graphics, erase: true);
		if (sampleDisplay_0.stDisChain.Count != 0)
		{
			disLg_0 = sampleDisplay_0.stDisChain.CurDisLg;
		}
	}

	public void GetAutoStopState()
	{
		btnAutoStop.Image = (instrument.methodSetup.chromInfoR.AcqAutoStop ? SystemBitmapResource4.smethod_0() : SystemBitmapResource4.smethod_1());
		btnAutoStop.Text = (instrument.methodSetup.chromInfoR.AcqAutoStop ? Lang.PS("自动停止", "Auto Stop") : Lang.PS("手动停止", "Manual Stop"));
		tbAutoStop.Text = instrument.methodSetup.chromInfoR.AcqRunTime.ToString();
	}

	private void InitializeComponent_2()
	{
		icontainer_2 = new Container();
		msDatAcq = new MenuStrip();
		miFile = new ToolStripMenuItem();
		miFiSetBgChrom = new ToolStripMenuItem();
		miFiCloseBkChrom = new ToolStripMenuItem();
		toolStripSeparator1 = new ToolStripSeparator();
		miFiExit = new ToolStripMenuItem();
		miAnalysis = new ToolStripMenuItem();
		miAlyRunSingle = new ToolStripMenuItem();
		miAlyRunSequence = new ToolStripMenuItem();
		miAlyPauseSequence = new ToolStripMenuItem();
		miAlyResumeSequence = new ToolStripMenuItem();
		miAlyStopSequence = new ToolStripMenuItem();
		miAlySnapshot = new ToolStripMenuItem();
		miAlyStopAcquisition = new ToolStripMenuItem();
		miAlyAbortAcquisition = new ToolStripMenuItem();
		toolStripSeparator2 = new ToolStripSeparator();
		miAlyFg = new ToolStripMenuItem();
		toolStripSeparator3 = new ToolStripSeparator();
		miAlySetZero = new ToolStripMenuItem();
		miAlyResetZero = new ToolStripMenuItem();
		miDisplay = new ToolStripMenuItem();
		miDisPreviousZoom = new ToolStripMenuItem();
		miDisNextZoom = new ToolStripMenuItem();
		miDisView = new ToolStripMenuItem();
		mivFix = new ToolStripMenuItem();
		mivExtend = new ToolStripMenuItem();
		toolStripSeparator9 = new ToolStripSeparator();
		miDisProperties = new ToolStripMenuItem();
		tsDatAcq = new ToolStrip();
		btnRunSingle = new ToolStripButton();
		btnRunSequence = new ToolStripButton();
		toolStripSeparator4 = new ToolStripSeparator();
		btnPauseSequence = new ToolStripButton();
		btnResumeSequence = new ToolStripButton();
		btnStopSequence = new ToolStripButton();
		toolStripSeparator5 = new ToolStripSeparator();
		btnSnapshot = new ToolStripButton();
		btnStopAcquisition = new ToolStripButton();
		btnAbortAcquisition = new ToolStripButton();
		toolStripSeparator6 = new ToolStripSeparator();
		btnPreviousZoom = new ToolStripButton();
		btnNextZoom = new ToolStripButton();
		toolStripSeparator7 = new ToolStripSeparator();
		btnProperties = new ToolStripButton();
		toolStripSeparator8 = new ToolStripSeparator();
		lbTime = new ToolStripLabel();
		tbTime = new ToolStripTextBox();
		lbTimeU = new ToolStripLabel();
		lbSignal = new ToolStripLabel();
		tbSigYBeg = new ToolStripTextBox();
		lbSignalU = new ToolStripLabel();
		tbSigYEnd = new ToolStripTextBox();
		lbyUnit = new ToolStripLabel();
		btnAutoStop = new ToolStripButton();
		tbAutoStop = new ToolStripTextBox();
		toolStripLabel2 = new ToolStripLabel();
		ssDatAcq = new StatusStrip();
		slbTime = new ToolStripStatusLabel();
		dpDatAcq = new LclDisplayPanel();
		timer_0 = new Timer(icontainer_2);
		msDatAcq.SuspendLayout();
		tsDatAcq.SuspendLayout();
		ssDatAcq.SuspendLayout();
		SuspendLayout();
		msDatAcq.Items.AddRange(new ToolStripItem[3] { miFile, miAnalysis, miDisplay });
		msDatAcq.Location = new Point(0, 0);
		msDatAcq.Name = "msDatAcq";
		msDatAcq.Size = new Size(947, 25);
		msDatAcq.TabIndex = 0;
		msDatAcq.Text = "menuStrip1";
		miFile.DropDownItems.AddRange(new ToolStripItem[4] { miFiSetBgChrom, miFiCloseBkChrom, toolStripSeparator1, miFiExit });
		miFile.Name = "miFile";
		miFile.Size = new Size(53, 21);
		miFile.Text = "miFile";
		miFiSetBgChrom.Name = "miFiSetBgChrom";
		miFiSetBgChrom.Size = new Size(185, 22);
		miFiSetBgChrom.Text = "miFiSetBkChrom";
		miFiSetBgChrom.Click += miFiSetBgChrom_Click;
		miFiCloseBkChrom.Name = "miFiCloseBkChrom";
		miFiCloseBkChrom.Size = new Size(185, 22);
		miFiCloseBkChrom.Text = "miFiCloseBkChrom";
		miFiCloseBkChrom.Click += miFiCloseBkChrom_Click;
		toolStripSeparator1.Name = "toolStripSeparator1";
		toolStripSeparator1.Size = new Size(182, 6);
		miFiExit.Name = "miFiExit";
		miFiExit.Size = new Size(185, 22);
		miFiExit.Text = "miFiExit";
		miAnalysis.DropDownItems.AddRange(new ToolStripItem[13]
		{
			miAlyRunSingle, miAlyRunSequence, miAlyPauseSequence, miAlyResumeSequence, miAlyStopSequence, miAlySnapshot, miAlyStopAcquisition, miAlyAbortAcquisition, toolStripSeparator2, miAlyFg,
			toolStripSeparator3, miAlySetZero, miAlyResetZero
		});
		miAnalysis.Name = "miAnalysis";
		miAnalysis.Size = new Size(80, 21);
		miAnalysis.Text = "miAnalysis";
		miAlyRunSingle.Name = "miAlyRunSingle";
		miAlyRunSingle.ShortcutKeys = Keys.F4;
		miAlyRunSingle.Size = new Size(224, 22);
		miAlyRunSingle.Text = "miAlyRunSingle";
		miAlyRunSingle.Click += miAlyRunSingle_Click;
		miAlyRunSequence.Name = "miAlyRunSequence";
		miAlyRunSequence.Size = new Size(224, 22);
		miAlyRunSequence.Text = "miAlyRunSequence";
		miAlyRunSequence.Visible = false;
		miAlyRunSequence.Click += btnRunSequence_Click;
		miAlyPauseSequence.Name = "miAlyPauseSequence";
		miAlyPauseSequence.Size = new Size(224, 22);
		miAlyPauseSequence.Text = "miAlyPauseSequence";
		miAlyPauseSequence.Visible = false;
		miAlyPauseSequence.Click += btnPauseSequence_Click;
		miAlyResumeSequence.Name = "miAlyResumeSequence";
		miAlyResumeSequence.Size = new Size(224, 22);
		miAlyResumeSequence.Text = "miAlyResumeSequence";
		miAlyResumeSequence.Visible = false;
		miAlyResumeSequence.Click += btnResumeSequence_Click;
		miAlyStopSequence.Name = "miAlyStopSequence";
		miAlyStopSequence.Size = new Size(224, 22);
		miAlyStopSequence.Text = "miAlyStopSequence";
		miAlyStopSequence.Visible = false;
		miAlyStopSequence.Click += btnStopSequence_Click;
		miAlySnapshot.Name = "miAlySnapshot";
		miAlySnapshot.ShortcutKeys = Keys.F5;
		miAlySnapshot.Size = new Size(224, 22);
		miAlySnapshot.Text = "miAlySnapshot";
		miAlySnapshot.Click += btnSnapshot_Click;
		miAlyStopAcquisition.Name = "miAlyStopAcquisition";
		miAlyStopAcquisition.ShortcutKeys = Keys.F8;
		miAlyStopAcquisition.Size = new Size(224, 22);
		miAlyStopAcquisition.Text = "miAlyStopAcquisition";
		miAlyStopAcquisition.Click += miAlyStopAcquisition_Click;
		miAlyAbortAcquisition.Name = "miAlyAbortAcquisition";
		miAlyAbortAcquisition.ShortcutKeys = Keys.F9;
		miAlyAbortAcquisition.Size = new Size(224, 22);
		miAlyAbortAcquisition.Text = "miAlyAbortAcquisition";
		miAlyAbortAcquisition.Click += btnAbortAcquisition_Click;
		toolStripSeparator2.Name = "toolStripSeparator2";
		toolStripSeparator2.Size = new Size(221, 6);
		miAlyFg.Name = "miAlyFg";
		miAlyFg.Size = new Size(224, 22);
		miAlyFg.Text = "馏分收集提示";
		miAlyFg.Click += miAlyFg_Click;
		toolStripSeparator3.Name = "toolStripSeparator3";
		toolStripSeparator3.Size = new Size(221, 6);
		toolStripSeparator3.Visible = false;
		miAlySetZero.Name = "miAlySetZero";
		miAlySetZero.Size = new Size(224, 22);
		miAlySetZero.Text = "miAlySetZero";
		miAlySetZero.Visible = false;
		miAlySetZero.Click += miAlySetZero_Click;
		miAlyResetZero.Name = "miAlyResetZero";
		miAlyResetZero.Size = new Size(224, 22);
		miAlyResetZero.Text = "miAlyResetZero";
		miAlyResetZero.Visible = false;
		miAlyResetZero.Click += miAlyResetZero_Click;
		miDisplay.DropDownItems.AddRange(new ToolStripItem[5] { miDisPreviousZoom, miDisNextZoom, miDisView, toolStripSeparator9, miDisProperties });
		miDisplay.Name = "miDisplay";
		miDisplay.Size = new Size(76, 21);
		miDisplay.Text = "miDisplay";
		miDisPreviousZoom.Name = "miDisPreviousZoom";
		miDisPreviousZoom.Size = new Size(191, 22);
		miDisPreviousZoom.Text = "miDisPreviousZoom";
		miDisPreviousZoom.Click += dpDatAcq_DoubleClick;
		miDisNextZoom.Name = "miDisNextZoom";
		miDisNextZoom.Size = new Size(191, 22);
		miDisNextZoom.Text = "miDisNextZoom";
		miDisNextZoom.Click += btnNextZoom_Click;
		miDisView.DropDownItems.AddRange(new ToolStripItem[2] { mivFix, mivExtend });
		miDisView.Name = "miDisView";
		miDisView.Size = new Size(191, 22);
		miDisView.Text = "miDisView";
		mivFix.Name = "mivFix";
		mivFix.Size = new Size(135, 22);
		mivFix.Text = "mivFix";
		mivFix.Click += mivExtend_Click;
		mivExtend.Name = "mivExtend";
		mivExtend.Size = new Size(135, 22);
		mivExtend.Text = "mivExtend";
		mivExtend.Click += mivExtend_Click;
		toolStripSeparator9.Name = "toolStripSeparator9";
		toolStripSeparator9.Size = new Size(188, 6);
		miDisProperties.Name = "miDisProperties";
		miDisProperties.Size = new Size(191, 22);
		miDisProperties.Text = "miDisProperties";
		miDisProperties.Click += btnProperties_Click;
		tsDatAcq.Items.AddRange(new ToolStripItem[27]
		{
			btnRunSingle, btnRunSequence, toolStripSeparator4, btnPauseSequence, btnResumeSequence, btnStopSequence, toolStripSeparator5, btnSnapshot, btnStopAcquisition, btnAbortAcquisition,
			toolStripSeparator6, btnPreviousZoom, btnNextZoom, toolStripSeparator7, btnProperties, toolStripSeparator8, lbTime, tbTime, lbTimeU, lbSignal,
			tbSigYBeg, lbSignalU, tbSigYEnd, lbyUnit, btnAutoStop, tbAutoStop, toolStripLabel2
		});
		tsDatAcq.Location = new Point(0, 25);
		tsDatAcq.Name = "tsDatAcq";
		tsDatAcq.Size = new Size(947, 25);
		tsDatAcq.TabIndex = 1;
		tsDatAcq.Text = "toolStrip1";
		tsDatAcq.Paint += tsDatAcq_Paint;
		btnRunSingle.DisplayStyle = ToolStripItemDisplayStyle.Image;
		btnRunSingle.ImageTransparentColor = Color.Magenta;
		btnRunSingle.Name = "btnRunSingle";
		btnRunSingle.Size = new Size(23, 22);
		btnRunSingle.Text = "toolStripButton1";
		btnRunSingle.Click += miAlyRunSingle_Click;
		btnRunSequence.DisplayStyle = ToolStripItemDisplayStyle.Image;
		btnRunSequence.ImageTransparentColor = Color.Magenta;
		btnRunSequence.Name = "btnRunSequence";
		btnRunSequence.Size = new Size(23, 22);
		btnRunSequence.Text = "toolStripButton2";
		btnRunSequence.Visible = false;
		btnRunSequence.Click += btnRunSequence_Click;
		toolStripSeparator4.Name = "toolStripSeparator4";
		toolStripSeparator4.Size = new Size(6, 25);
		toolStripSeparator4.Visible = false;
		btnPauseSequence.DisplayStyle = ToolStripItemDisplayStyle.Image;
		btnPauseSequence.ImageTransparentColor = Color.Magenta;
		btnPauseSequence.Name = "btnPauseSequence";
		btnPauseSequence.Size = new Size(23, 22);
		btnPauseSequence.Text = "toolStripButton3";
		btnPauseSequence.Visible = false;
		btnPauseSequence.Click += btnPauseSequence_Click;
		btnResumeSequence.DisplayStyle = ToolStripItemDisplayStyle.Image;
		btnResumeSequence.ImageTransparentColor = Color.Magenta;
		btnResumeSequence.Name = "btnResumeSequence";
		btnResumeSequence.Size = new Size(23, 22);
		btnResumeSequence.Text = "toolStripButton4";
		btnResumeSequence.Visible = false;
		btnResumeSequence.Click += btnResumeSequence_Click;
		btnStopSequence.DisplayStyle = ToolStripItemDisplayStyle.Image;
		btnStopSequence.ImageTransparentColor = Color.Magenta;
		btnStopSequence.Name = "btnStopSequence";
		btnStopSequence.Size = new Size(23, 22);
		btnStopSequence.Text = "toolStripButton1";
		btnStopSequence.Visible = false;
		btnStopSequence.Click += btnStopSequence_Click;
		toolStripSeparator5.Name = "toolStripSeparator5";
		toolStripSeparator5.Size = new Size(6, 25);
		btnSnapshot.DisplayStyle = ToolStripItemDisplayStyle.Image;
		btnSnapshot.ImageTransparentColor = Color.Magenta;
		btnSnapshot.Name = "btnSnapshot";
		btnSnapshot.Size = new Size(23, 22);
		btnSnapshot.Text = "toolStripButton3";
		btnSnapshot.Click += btnSnapshot_Click;
		btnStopAcquisition.DisplayStyle = ToolStripItemDisplayStyle.Image;
		btnStopAcquisition.ImageTransparentColor = Color.Magenta;
		btnStopAcquisition.Name = "btnStopAcquisition";
		btnStopAcquisition.Size = new Size(23, 22);
		btnStopAcquisition.Text = "toolStripButton4";
		btnStopAcquisition.Click += miAlyStopAcquisition_Click;
		btnAbortAcquisition.DisplayStyle = ToolStripItemDisplayStyle.Image;
		btnAbortAcquisition.ImageTransparentColor = Color.Magenta;
		btnAbortAcquisition.Name = "btnAbortAcquisition";
		btnAbortAcquisition.Size = new Size(23, 22);
		btnAbortAcquisition.Text = "toolStripButton5";
		btnAbortAcquisition.Click += btnAbortAcquisition_Click;
		toolStripSeparator6.Name = "toolStripSeparator6";
		toolStripSeparator6.Size = new Size(6, 25);
		btnPreviousZoom.DisplayStyle = ToolStripItemDisplayStyle.Image;
		btnPreviousZoom.ImageTransparentColor = Color.Magenta;
		btnPreviousZoom.Name = "btnPreviousZoom";
		btnPreviousZoom.Size = new Size(23, 22);
		btnPreviousZoom.Text = "toolStripButton7";
		btnPreviousZoom.Click += dpDatAcq_DoubleClick;
		btnNextZoom.DisplayStyle = ToolStripItemDisplayStyle.Image;
		btnNextZoom.ImageTransparentColor = Color.Magenta;
		btnNextZoom.Name = "btnNextZoom";
		btnNextZoom.Size = new Size(23, 22);
		btnNextZoom.Text = "toolStripButton8";
		btnNextZoom.Click += btnNextZoom_Click;
		toolStripSeparator7.Name = "toolStripSeparator7";
		toolStripSeparator7.Size = new Size(6, 25);
		btnProperties.DisplayStyle = ToolStripItemDisplayStyle.Image;
		btnProperties.ImageTransparentColor = Color.Magenta;
		btnProperties.Name = "btnProperties";
		btnProperties.Size = new Size(23, 22);
		btnProperties.Text = "toolStripButton6";
		btnProperties.Click += btnProperties_Click;
		toolStripSeparator8.Name = "toolStripSeparator8";
		toolStripSeparator8.Size = new Size(6, 25);
		lbTime.Name = "lbTime";
		lbTime.Size = new Size(36, 22);
		lbTime.Text = "Time";
		tbTime.BorderStyle = BorderStyle.None;
		tbTime.Name = "tbTime";
		tbTime.Size = new Size(50, 25);
		tbTime.Text = "30";
		tbTime.TextBoxTextAlign = HorizontalAlignment.Center;
		tbTime.KeyDown += tbSigYEnd_KeyDown;
		tbTime.DoubleClick += tbSigYEnd_DoubleClick;
		lbTimeU.Margin = new Padding(2, 1, 0, 2);
		lbTimeU.Name = "lbTimeU";
		lbTimeU.Size = new Size(37, 22);
		lbTimeU.Text = "[min]";
		lbSignal.Name = "lbSignal";
		lbSignal.Size = new Size(43, 22);
		lbSignal.Text = "Signal";
		tbSigYBeg.BorderStyle = BorderStyle.None;
		tbSigYBeg.Name = "tbSigYBeg";
		tbSigYBeg.Size = new Size(50, 25);
		tbSigYBeg.Text = "-10";
		tbSigYBeg.TextBoxTextAlign = HorizontalAlignment.Center;
		tbSigYBeg.KeyDown += tbSigYEnd_KeyDown;
		tbSigYBeg.DoubleClick += tbSigYEnd_DoubleClick;
		lbSignalU.Margin = new Padding(3, 1, 0, 2);
		lbSignalU.Name = "lbSignalU";
		lbSignalU.Size = new Size(20, 22);
		lbSignalU.Text = "到";
		tbSigYEnd.BorderStyle = BorderStyle.None;
		tbSigYEnd.Name = "tbSigYEnd";
		tbSigYEnd.Size = new Size(50, 25);
		tbSigYEnd.Text = "500";
		tbSigYEnd.TextBoxTextAlign = HorizontalAlignment.Center;
		tbSigYEnd.KeyDown += tbSigYEnd_KeyDown;
		tbSigYEnd.DoubleClick += tbSigYEnd_DoubleClick;
		lbyUnit.Margin = new Padding(3, 1, 0, 2);
		lbyUnit.Name = "lbyUnit";
		lbyUnit.Size = new Size(37, 22);
		lbyUnit.Text = "mAu,";
		btnAutoStop.DisplayStyle = ToolStripItemDisplayStyle.Image;
		btnAutoStop.ImageTransparentColor = Color.Magenta;
		btnAutoStop.Name = "btnAutoStop";
		btnAutoStop.Size = new Size(23, 22);
		btnAutoStop.Text = "自动结束";
		btnAutoStop.Click += btnAutoStop_Click;
		tbAutoStop.BorderStyle = BorderStyle.None;
		tbAutoStop.Margin = new Padding(4, 0, 1, 0);
		tbAutoStop.Name = "tbAutoStop";
		tbAutoStop.Size = new Size(40, 25);
		tbAutoStop.Text = "10";
		tbAutoStop.TextBoxTextAlign = HorizontalAlignment.Center;
		tbAutoStop.KeyDown += tbAutoStop_KeyDown;
		toolStripLabel2.Name = "toolStripLabel2";
		toolStripLabel2.Size = new Size(29, 22);
		toolStripLabel2.Text = "min";
		ssDatAcq.Items.AddRange(new ToolStripItem[1] { slbTime });
		ssDatAcq.Location = new Point(0, 268);
		ssDatAcq.Name = "ssDatAcq";
		ssDatAcq.Size = new Size(947, 22);
		ssDatAcq.TabIndex = 2;
		ssDatAcq.Text = "statusStrip1";
		slbTime.AutoSize = false;
		slbTime.BorderSides = ToolStripStatusLabelBorderSides.Left | ToolStripStatusLabelBorderSides.Right;
		slbTime.Margin = new Padding(0, 2, 0, 0);
		slbTime.Name = "slbTime";
		slbTime.Size = new Size(100, 20);
		slbTime.Text = "?";
		dpDatAcq.BackColor = Color.BlanchedAlmond;
		dpDatAcq.Location = new Point(39, 79);
		dpDatAcq.Name = "dpDatAcq";
		dpDatAcq.Size = new Size(103, 68);
		dpDatAcq.TabIndex = 3;
		dpDatAcq.DoubleClick += dpDatAcq_DoubleClick;
		dpDatAcq.Paint += dpDatAcq_Paint;
		dpDatAcq.MouseMove += dpDatAcq_MouseMove;
		dpDatAcq.MouseDown += dpDatAcq_MouseDown;
		dpDatAcq.MouseUp += dpDatAcq_MouseUp;
		timer_0.Interval = 500;
		timer_0.Tick += timer_0_Tick;
		base.ClientSize = new Size(947, 290);
		base.Controls.Add(dpDatAcq);
		base.Controls.Add(ssDatAcq);
		base.Controls.Add(tsDatAcq);
		base.Controls.Add(msDatAcq);
		base.MainMenuStrip = msDatAcq;
		base.Name = "DataAcqForm";
		base.Load += DataAcqForm_Load;
		base.FormClosing += DataAcqForm_FormClosing;
		msDatAcq.ResumeLayout(performLayout: false);
		msDatAcq.PerformLayout();
		tsDatAcq.ResumeLayout(performLayout: false);
		tsDatAcq.PerformLayout();
		ssDatAcq.ResumeLayout(performLayout: false);
		ssDatAcq.PerformLayout();
		ResumeLayout(performLayout: false);
		PerformLayout();
	}

	public override void LoadLanguage()
	{
		base.LoadLanguage();
		switch (Class49.sysLanguage_0)
		{
		case SysLanguage.CN:
			miFile.Text = "文件";
			miFiSetBgChrom.Text = "设置背景谱图...";
			miFiCloseBkChrom.Text = "关闭背景谱图";
			miFiExit.Text = "退出";
			miAnalysis.Text = "分析";
			miAlyRunSingle.Text = "运行单针";
			miAlyRunSequence.Text = "运行序列";
			miAlyPauseSequence.Text = "暂停序列";
			miAlyResumeSequence.Text = "继续序列";
			miAlyStopSequence.Text = "停止序列";
			miAlySnapshot.Text = "快照";
			miAlyStopAcquisition.Text = "停止采集";
			miAlyAbortAcquisition.Text = "放弃采集";
			miAlyFg.Text = "馏分收集提示";
			miAlySetZero.Text = "置零";
			miAlyResetZero.Text = "重置零";
			miDisplay.Text = "显示";
			miDisPreviousZoom.Text = "后退";
			miDisNextZoom.Text = "前进";
			miDisProperties.Text = "属性...";
			toolStripMenuItem_0.Text = "网格线";
			lbTime.Text = "时轴";
			lbSignal.Text = "信号";
			break;
		case SysLanguage.EN:
			miFile.Text = "File";
			miFiSetBgChrom.Text = "Set Background Chromatogram...";
			miFiCloseBkChrom.Text = "Close Background Chromatogram";
			miFiExit.Text = "Exit";
			miAnalysis.Text = "Analysis";
			miAlyRunSingle.Text = "Run Single";
			miAlyRunSequence.Text = "Run Sequence";
			miAlyPauseSequence.Text = "Pause Sequence";
			miAlyResumeSequence.Text = "Resume Sequence";
			miAlyStopSequence.Text = "Stop Sequence";
			miAlySnapshot.Text = "Snapshot";
			miAlyStopAcquisition.Text = "Stop Acquisition";
			miAlyAbortAcquisition.Text = "Abort Acquisition";
			miAlyFg.Text = "Frag Gather";
			miAlySetZero.Text = "Set Zero";
			miAlyResetZero.Text = "Reset Zero";
			miDisplay.Text = "Display";
			miDisPreviousZoom.Text = "Previous Zoom";
			miDisNextZoom.Text = "Next Zoom";
			miDisProperties.Text = "Properties...";
			toolStripMenuItem_0.Text = "Grid Lines";
			lbTime.Text = "Time";
			lbSignal.Text = "Signal";
			break;
		}
		btnRunSingle.Text = miAlyRunSingle.Text;
		btnRunSequence.Text = miAlyRunSequence.Text;
		btnPauseSequence.Text = miAlyPauseSequence.Text;
		btnResumeSequence.Text = miAlyResumeSequence.Text;
		btnStopSequence.Text = miAlyStopSequence.Text;
		btnSnapshot.Text = miAlySnapshot.Text;
		btnStopAcquisition.Text = miAlyStopAcquisition.Text;
		btnAbortAcquisition.Text = miAlyAbortAcquisition.Text;
		btnProperties.Text = miDisProperties.Text;
		btnPreviousZoom.Text = miDisPreviousZoom.Text;
		btnNextZoom.Text = miDisNextZoom.Text;
	}

	public void LoadOptions()
	{
		sampleDisplay_0.LinkOptions(instrument.user.options);
		ApplyMethod();
		method_3();
	}

	private void btnAbortAcquisition_Click(object sender, EventArgs e)
	{
		if (instrument.injectStyle == InjectStyle.Single)
		{
			instrument.form.ssAlyForm.miAlyAbortAcquisition_Click(null, null);
		}
		else if (instrument.injectStyle == InjectStyle.Sequence)
		{
			instrument.form.seqAlyForm.miSeqAbortAcquisition_Click(null, null);
		}
	}

	private void miAlyFg_Click(object sender, EventArgs e)
	{
		dlgFg.ShowDialog();
	}

	private void btnPauseSequence_Click(object sender, EventArgs e)
	{
		instrument.form.seqAlyForm.miSeqPauseSequence_Click(null, null);
	}

	private void miAlyResetZero_Click(object sender, EventArgs e)
	{
		instrument.Detector_Set(zero: false);
	}

	private void btnResumeSequence_Click(object sender, EventArgs e)
	{
		instrument.form.seqAlyForm.miSeqResumeSequence_Click(null, null);
	}

	private void btnRunSequence_Click(object sender, EventArgs e)
	{
		instrument.form.seqAlyForm.miSeqRunSequence_Click(null, null);
	}

	public void miAlyRunSingle_Click(object sender, EventArgs e)
	{
		instrument.form.ssAlyForm.miAlyRunSingle_Click(null, null);
	}

	private void miAlySetZero_Click(object sender, EventArgs e)
	{
		instrument.Detector_Set(zero: true);
	}

	private void btnSnapshot_Click(object sender, EventArgs e)
	{
		instrument.Save();
	}

	public void miAlyStopAcquisition_Click(object sender, EventArgs e)
	{
		if (instrument.injectStyle == InjectStyle.Single)
		{
			instrument.form.ssAlyForm.miAlyStopAcquisition_Click(null, null);
		}
		else if (instrument.injectStyle == InjectStyle.Sequence)
		{
			instrument.form.seqAlyForm.miSeqStopAcquisition_Click(null, null);
		}
	}

	private void btnStopSequence_Click(object sender, EventArgs e)
	{
		instrument.form.seqAlyForm.miSeqStopSequence_Click(null, null);
	}

	private void btnNextZoom_Click(object sender, EventArgs e)
	{
		sampleDisplay_0.stDisChain.DynNo++;
		disLg_0 = sampleDisplay_0.stDisChain.CurDisLg;
		method_3();
	}

	private void dpDatAcq_DoubleClick(object sender, EventArgs e)
	{
		sampleDisplay_0.stDisChain.DynNo--;
		disLg_0 = sampleDisplay_0.stDisChain.CurDisLg;
		method_3();
	}

	private void btnProperties_Click(object sender, EventArgs e)
	{
		if (Class49.optionsDialog_0.ShowDialog(instrument, WinStyle.DataAcq, instrument.user.options) == DialogResult.OK)
		{
			LoadOptions();
		}
	}

	private void miFiCloseBkChrom_Click(object sender, EventArgs e)
	{
		sampleDisplay_0.ShowBgChrom = false;
	}

	private void miFiSetBgChrom_Click(object sender, EventArgs e)
	{
		if (openFileDialog_0 == null)
		{
			openFileDialog_0 = new OpenFileDialog();
			openFileDialog_0.Filter = Class49.MakeFileFilter(".sda");
			openFileDialog_0.Multiselect = false;
		}
		sampleDisplay_0.ShowBgChrom = false;
		if (openFileDialog_0.ShowDialog() == DialogResult.OK)
		{
			Chromatogram chromatogram = Chromatogram.LoadFromFile2(openFileDialog_0.FileName, DetectorStyle.General);
			if (chromatogram != null)
			{
				sampleDisplay_0.bgChrom = chromatogram;
			}
			for (int i = 0; i < sampleDisplay_0.bgChrom.nrUserNames.Length; i++)
			{
				if (sampleDisplay_0.bgChrom.nrUserNames[i] == instrument.user.u_name)
				{
					MessageBox.Show("您没有本谱图的读取权限！");
					return;
				}
			}
		}
		sampleDisplay_0.ShowBgChrom = true;
	}

	protected override void miHpHelp_Click(object sender, EventArgs e)
	{
		Class49.smethod_32("数据采集");
	}

	private void mivExtend_Click(object sender, EventArgs e)
	{
		mivExtend.Checked = false;
		mivFix.Checked = false;
		(sender as ToolStripMenuItem).Checked = true;
	}

	private void method_0(object sender, EventArgs e)
	{
		toolStripMenuItem_0.Checked = instrument.user.options.grpShowGrid;
	}

	private void toolStripMenuItem_0_Click(object sender, EventArgs e)
	{
		toolStripMenuItem_0.Checked = !toolStripMenuItem_0.Checked;
		if (instrument.user.options.grpShowGrid = toolStripMenuItem_0.Checked)
		{
			sampleDisplay_0.setShowGrid = true;
		}
		LoadOptions();
	}

	public override void ReadWinInfo(WinInfo winInfo)
	{
		base.ReadWinInfo(winInfo);
		if (winInfo.string_0 == null)
		{
			tbTime.Text = "30";
			tbSigYBeg.Text = "-10";
			tbSigYEnd.Text = "500";
			mivExtend_Click(mivExtend, null);
		}
		else
		{
			tbTime.Text = winInfo.string_0;
			tbSigYBeg.Text = winInfo.string_1;
			string[] array = winInfo.string_2.Split(',');
			tbSigYEnd.Text = array[0];
			if (array.Length == 2 && array[1] == "mivFix")
			{
				mivExtend_Click(mivFix, null);
			}
			else
			{
				mivExtend_Click(mivExtend, null);
			}
		}
		method_1();
	}

	private void method_1()
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
		method_2(disLg_0.lgXBeg, num, num2, num3 - num2);
	}

	private void method_2(float float_1, float float_2, float float_3, float float_4)
	{
		float_2 = Math.Max(float_2, 0.01f);
		float_4 = Math.Max(float_4, 0.001f);
		disLg_0.lgXBeg = float_1;
		disLg_0.lgX = float_2;
		disLg_0.lgYBeg = float_3;
		disLg_0.lgY = float_4;
		sampleDisplay_0.stDisChain.AppendFrameLg(disLg_0);
	}

	public override void refresh_once()
	{
		base.refresh_once();
		sampleDisplay_0.instruStyle = instrument.instruStyle;
		method_4();
		switch (instrument.instruStyle)
		{
		case InstruStyle.GC:
		case InstruStyle.LC:
		case InstruStyle.PDA:
		{
			ToolStripItem toolStripItem3 = miWinCaliGnl;
			mubtnCaliGnl.Visible = true;
			toolStripItem3.Visible = true;
			ToolStripItem toolStripItem4 = miWinCaliGpc;
			mubtnCaliGpc.Visible = false;
			toolStripItem4.Visible = false;
			break;
		}
		case InstruStyle.GPC:
		{
			ToolStripItem toolStripItem = miWinCaliGnl;
			mubtnCaliGnl.Visible = false;
			toolStripItem.Visible = false;
			ToolStripItem toolStripItem2 = miWinCaliGpc;
			mubtnCaliGpc.Visible = true;
			toolStripItem2.Visible = true;
			break;
		}
		}
		if (!instrument.sampling)
		{
			sampleDisplay_0.ClearDisSignals();
			ApplyMethod();
			sampleDisplay_0.LinkDisSignals(instrument.sglsSampling, 0, out var _);
		}
		slbTime.Tag = 0f;
		if (slbSignals == null)
		{
			slbSignals = new ToolStripStatusLabel[12];
			for (int i = 0; i < slbSignals.Length; i++)
			{
				slbSignals[i] = new ToolStripStatusLabel();
				slbSignals[i].AutoSize = false;
				slbSignals[i].Width = 120;
				slbSignals[i].Margin = new Padding(0, 3, 0, 0);
				slbSignals[i].BorderSides = ToolStripStatusLabelBorderSides.Left | ToolStripStatusLabelBorderSides.Right | ToolStripStatusLabelBorderSides.Top;
				slbSignals[i].TextAlign = ContentAlignment.MiddleRight;
				slbSignals[i].Visible = false;
				slbSignals[i].Tag = 0f;
				ssDatAcq.Items.Add(slbSignals[i]);
			}
		}
		if (instrument.sampling)
		{
			return;
		}
		for (int j = 0; j < slbSignals.Length; j++)
		{
			if (j < instrument.dtc_Channels.Length)
			{
				slbSignals[j].Visible = true;
				slbSignals[j].Text = "-";
			}
			else
			{
				slbSignals[j].Visible = false;
			}
		}
	}

	private void method_3()
	{
		ToolStripMenuItem toolStripMenuItem = miDisPreviousZoom;
		bool enabled = (btnPreviousZoom.Enabled = sampleDisplay_0.stDisChain.HasPrevious);
		toolStripMenuItem.Enabled = enabled;
		ToolStripMenuItem toolStripMenuItem2 = miDisNextZoom;
		enabled = (btnNextZoom.Enabled = sampleDisplay_0.stDisChain.HasNext);
		toolStripMenuItem2.Enabled = enabled;
	}

	private void method_4()
	{
		Text = sTitle + "[" + instrument.name + "]";
	}

	private void method_5(Signal signal_0)
	{
		signal_0.refresh_TimeValue();
		sampleDisplay_0.SetFullDisLg(ref disLg_0, signal_0, second: false);
		method_2(disLg_0.lgXBeg, disLg_0.lgX, disLg_0.lgYBeg, disLg_0.lgY);
	}

	public void Set3Buttons(bool enabled)
	{
		ToolStripButton toolStripButton = btnRunSingle;
		bool enabled2 = (miAlyRunSingle.Enabled = !enabled);
		toolStripButton.Enabled = enabled2;
		ToolStripItem toolStripItem = btnSnapshot;
		ToolStripItem toolStripItem2 = btnStopAcquisition;
		btnAbortAcquisition.Enabled = enabled;
		toolStripItem2.Enabled = enabled;
		toolStripItem.Enabled = enabled;
		ToolStripItem toolStripItem3 = miAlySnapshot;
		ToolStripItem toolStripItem4 = miAlyStopAcquisition;
		miAlyAbortAcquisition.Enabled = enabled;
		toolStripItem4.Enabled = enabled;
		toolStripItem3.Enabled = enabled;
	}

	public void SetBgChrom(bool visible)
	{
		sampleDisplay_0.ShowBgChrom = visible;
	}

	public void SetDrawName(string drawName)
	{
		sampleDisplay_0.drawName = drawName;
	}

	public new void Show()
	{
		base.Show();
		sampleDisplay_0.RefreshSignalLabels = true;
		sampleDisplay_0.stDisChain.Clear();
		method_1();
		timer_0.Enabled = true;
	}

	private void tbAutoStop_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode == Keys.Return)
		{
			float acqRunTime = instrument.methodSetup.chromInfoR.AcqRunTime;
			acqRunTime = Class49.String2Float(tbAutoStop.Text, acqRunTime);
			instrument.methodSetup.chromInfoR.AcqRunTime = acqRunTime;
		}
	}

	private void tbSigYEnd_DoubleClick(object sender, EventArgs e)
	{
		disLg_0.lgXBeg = 0f;
		method_1();
		dpDatAcq.Refresh();
	}

	private void tbSigYEnd_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode == Keys.Return)
		{
			tbSigYEnd_DoubleClick(null, null);
		}
	}

	private void timer_0_Tick(object sender, EventArgs e)
	{
		if (!base.Visible)
		{
			return;
		}
		for (int i = 0; i < slbSignals.Length; i++)
		{
			if (slbSignals[i].Visible)
			{
				slbSignals[i].Text = ((float)slbSignals[i].Tag).ToString("0.000") + " " + Class49.MesureUnit() + " ";
				slbTime.Text = (instrument.sampling ? instrument.sample_time : instrument.idle_time).ToString("0.00 min");
			}
		}
		dpDatAcq.Refresh();
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
		winInfo.hasS = true;
		winInfo.string_0 = tbTime.Text;
		winInfo.string_1 = tbSigYBeg.Text;
		winInfo.string_2 = tbSigYEnd.Text;
		if (mivFix.Checked)
		{
			winInfo.string_2 += ",mivFix";
		}
		else
		{
			winInfo.string_2 += ",mivExtend";
		}
	}
}
