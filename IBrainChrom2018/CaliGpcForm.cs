using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace IBrainChrom2018;

public class CaliGpcForm : LclGnlForm
{
	private ToolStripButton btnAddAll;

	private ToolStripButton btnAddPeak;

	private ToolStripButton btnCloseStand;

	private ToolStripButton btnNewCali;

	private ToolStripButton btnNextZoom;

	private ToolStripButton btnOpenCali;

	private ToolStripButton btnOpenStand;

	private ToolStripButton btnOptions;

	private ToolStripButton btnPreviousZoom;

	private ToolStripButton btnSaveCali;

	private ToolStripButton btnUnzoom;

	private ContextMenuStrip cmsCali;

	private IContainer icontainer_2;

	private ColumnsSetupDlg columnsSetupDlg_0;

	private CaliGpcOptDlg caliGpcOptDlg_0;

	private LclDisplayPanel dpDisplay;

	private LclGridView gvCmpds;

	private LclExpressLabel lclExpressLabel1;

	private ToolStripMenuItem miCaliAddAll;

	private ToolStripMenuItem miCaliAddPeak;

	private ToolStripMenuItem miCalibration;

	private ToolStripMenuItem miCaliOptions;

	private ToolStripMenuItem miColumnsSetup;

	private ToolStripMenuItem miDisNextZoom;

	private ToolStripMenuItem miDisplay;

	private ToolStripMenuItem miDisPreviousZoom;

	private ToolStripMenuItem miDisProperties;

	private ToolStripMenuItem miDisUnzoom;

	private ToolStripMenuItem miFiCloseStand;

	private ToolStripMenuItem miFiExit;

	private ToolStripMenuItem miFile;

	private ToolStripMenuItem miFiNewCali;

	private ToolStripMenuItem miFiOpenCali;

	private ToolStripMenuItem miFiOpenStand;

	private ToolStripMenuItem miFiPreview;

	private ToolStripMenuItem miFiPrint;

	private ToolStripMenuItem miFiReportSetup;

	private ToolStripMenuItem miFiSaveAsCali;

	private ToolStripMenuItem miFiSaveCali;

	private ToolStripMenuItem miRestoreDftColumns;

	private MenuStrip msCali;

	private LclSplitter splt;

	private StatusStrip ssCali;

	private ToolStripSeparator toolStripSeparator1;

	private ToolStripSeparator toolStripSeparator10;

	private ToolStripSeparator toolStripSeparator2;

	private ToolStripSeparator toolStripSeparator3;

	private ToolStripSeparator toolStripSeparator5;

	private ToolStripSeparator toolStripSeparator6;

	private ToolStripSeparator toolStripSeparator7;

	private ToolStripSeparator toolStripSeparator8;

	private ToolStripSeparator toolStripSeparator9;

	private ToolStripStatusLabel toolStripStatusLabel1;

	private ToolStrip tsCali;

	public static string Filter => "(*.gal)|*.gal";

	private string sTitle => Class49.sysLanguage_0 switch
	{
		SysLanguage.CN => "凝胶校正", 
		SysLanguage.EN => "GPC Calibration", 
		_ => "", 
	};

	public CaliGpcForm()
	{
		icontainer_2 = null;
		columnsSetupDlg_0 = new ColumnsSetupDlg("GPC校正列设置", "GPC Calibrate Columns Setup");
		InitializeComponent_2();
	}

	public CaliGpcForm(Instrument instrument)
	{
		icontainer_2 = null;
		columnsSetupDlg_0 = new ColumnsSetupDlg("GPC校正列设置", "GPC Calibrate Columns Setup");
		InitializeComponent_2();
		base.instrument = instrument;
	}

	private void CaliGpcForm_Load(object sender, EventArgs e)
	{
		msCali.Items.Add(miView);
		miFiExit.Click += base.miFiExit_Click;
		msCali.Items.Add(miWindow);
		ToolStripItem toolStripItem = miWinCaliGnl;
		miWinCaliGpc.Visible = false;
		toolStripItem.Visible = false;
		msCali.Items.Add(miHelp);
		msCali.Items.Add(new ToolStripSeparator());
		msCali.Items.Add(mubtnMainForm);
		msCali.Items.Add(mubtnInstrument);
		msCali.Items.Add(new ToolStripSeparator());
		msCali.Items.Add(mubtnChromatogram);
		msCali.Items.Add(mubtnDataAcq);
		msCali.Items.Add(mubtnSglAly);
		msCali.Items.Add(mubtnSeqAly);
		base.Icon = SystemIconResource.smethod_7();
		ResourceImageLoad.SetCtrlBitmap(btnNewCali, SystemIconResource.smethod_27());
		ResourceImageLoad.SetCtrlBitmap(btnOpenCali, SystemIconResource.smethod_32());
		ResourceImageLoad.SetCtrlBitmap(btnSaveCali, SystemIconResource.smethod_39());
		ResourceImageLoad.SetCtrlBitmap(btnOpenStand, SystemIconResource.smethod_31());
		ResourceImageLoad.SetCtrlBitmap(btnCloseStand, SystemIconResource.smethod_18());
		ResourceImageLoad.SetCtrlBitmap(btnPreviousZoom, SystemIconResource.smethod_58());
		ResourceImageLoad.SetCtrlBitmap(btnNextZoom, SystemIconResource.smethod_56());
		ResourceImageLoad.SetCtrlBitmap(btnUnzoom, SystemIconResource.smethod_63());
		ResourceImageLoad.SetCtrlBitmap(btnAddAll, SystemBitmapResource5.smethod_0());
		ResourceImageLoad.SetCtrlBitmap(btnAddPeak, SystemBitmapResource5.smethod_3());
		ResourceImageLoad.SetCtrlBitmap(btnOptions, SystemBitmapResource5.smethod_5());
		gvCmpds.BorderStyle = BorderStyle.None;
		gvCmpds.Dock = DockStyle.Fill;
		gvCmpds.CharacterHeaderColor = Color.Red;
		method_1();
		method_0();
		if (!gvCmpds.LoadFromManager())
		{
			miRestoreDftColumns_Click(miRestoreDftColumns, null);
		}
		InstruWinsInfo instruWinsInfo = instrument.user.instrusWinsInfo[instrument.pageNo];
		if (instruWinsInfo.valid)
		{
			ReadWinInfo(instruWinsInfo.winInfos[6]);
		}
	}

	private void miRestoreDftColumns_Click(object sender, EventArgs e)
	{
		if (sender == miColumnsSetup)
		{
			columnsSetupDlg_0.ShowDialog(gvCmpds);
		}
		else if (sender == miRestoreDftColumns)
		{
			gvCmpds.ini_SetFirstVisibleColumn("StdNum");
			gvCmpds.ini_SetNextVisibleColumn("Used");
			gvCmpds.ini_SetNextVisibleColumn("Mn");
			gvCmpds.ini_SetNextVisibleColumn("Mw");
			gvCmpds.ini_SetNextVisibleColumn("LogM");
			gvCmpds.ini_SetNextVisibleColumn("PeakRT");
			gvCmpds.ini_SetNextVisibleColumn("SourceChrom");
			gvCmpds.ini_FinishVisibleColumn();
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

	private void method_0()
	{
		if (gvCmpds.ColumnCount == 0)
		{
			return;
		}
		switch (Class49.sysLanguage_0)
		{
		case SysLanguage.CN:
		{
			for (int j = 0; j < gvCmpds.ColumnCount; j++)
			{
				string name2;
				switch (name2 = gvCmpds.Columns[j].Name)
				{
				case "StdNum":
					gvCmpds.Columns[j].HeaderText = "标样\n数";
					break;
				case "Used":
					gvCmpds.Columns[j].HeaderText = "使用";
					break;
				case "PeakRT":
					gvCmpds.Columns[j].HeaderText = "峰位RT\n[min]";
					break;
				case "AverageRT":
					gvCmpds.Columns[j].HeaderText = "平均RT\n[min]";
					break;
				case "FRFactor":
					gvCmpds.Columns[j].HeaderText = "FR\n因子";
					break;
				case "SourceChrom":
					gvCmpds.Columns[j].HeaderText = "源谱图";
					break;
				case "Residual":
					gvCmpds.Columns[j].HeaderText = "残差";
					break;
				}
			}
			break;
		}
		case SysLanguage.EN:
		{
			for (int i = 0; i < gvCmpds.ColumnCount; i++)
			{
				string name;
				switch (name = gvCmpds.Columns[i].Name)
				{
				case "StdNum":
					gvCmpds.Columns[i].HeaderText = "Std.\nNum";
					break;
				case "Used":
					gvCmpds.Columns[i].HeaderText = "Used";
					break;
				case "PeakRT":
					gvCmpds.Columns[i].HeaderText = "Peak RT\n[min]";
					break;
				case "AverageRT":
					gvCmpds.Columns[i].HeaderText = "Avr. RT\n[min]";
					break;
				case "FRFactor":
					gvCmpds.Columns[i].HeaderText = "FR\nFactor";
					break;
				case "SourceChrom":
					gvCmpds.Columns[i].HeaderText = "Source Chromatogram";
					break;
				case "Residual":
					gvCmpds.Columns[i].HeaderText = "Residual";
					break;
				}
			}
			break;
		}
		}
	}

	private void method_1()
	{
		gvCmpds.AddLclCheckBoxColumn("StdNum", 35);
		gvCmpds.AddLclCheckBoxColumn("Used", 30);
		gvCmpds.AddLclTextBoxColumn("Mn", 60);
		gvCmpds.AddLclTextBoxColumn("Mw", 60);
		gvCmpds.AddLclTextBoxColumn("LogM", 60).HeaderText = "LogM\nadjusted";
		gvCmpds.AddLclComboBoxColumn("PeakRT", 60);
		gvCmpds.AddLclComboBoxColumn("AverageRT", 60);
		gvCmpds.AddLclComboBoxColumn("FRFactor", 60);
		gvCmpds.AddLclTextBoxColumn("K", 75).HeaderText = "K\n[dL/g*10^3]";
		gvCmpds.AddLclTextBoxColumn("Alpha", 60).HeaderText = "alpha";
		gvCmpds.AddLclTextBoxColumn("SourceChrom", 130);
		gvCmpds.AddLclTextBoxColumn("Residual", 60);
	}

	private void InitializeComponent_2()
	{
		icontainer_2 = new Container();
		new ComponentResourceManager(typeof(CaliGpcForm));
		msCali = new MenuStrip();
		miFile = new ToolStripMenuItem();
		miFiNewCali = new ToolStripMenuItem();
		miFiOpenCali = new ToolStripMenuItem();
		miFiSaveCali = new ToolStripMenuItem();
		miFiSaveAsCali = new ToolStripMenuItem();
		toolStripSeparator1 = new ToolStripSeparator();
		miFiOpenStand = new ToolStripMenuItem();
		miFiCloseStand = new ToolStripMenuItem();
		toolStripSeparator2 = new ToolStripSeparator();
		miFiReportSetup = new ToolStripMenuItem();
		miFiPreview = new ToolStripMenuItem();
		miFiPrint = new ToolStripMenuItem();
		toolStripSeparator3 = new ToolStripSeparator();
		miFiExit = new ToolStripMenuItem();
		miDisplay = new ToolStripMenuItem();
		miDisPreviousZoom = new ToolStripMenuItem();
		miDisNextZoom = new ToolStripMenuItem();
		miDisUnzoom = new ToolStripMenuItem();
		toolStripSeparator5 = new ToolStripSeparator();
		miDisProperties = new ToolStripMenuItem();
		miCalibration = new ToolStripMenuItem();
		miCaliAddAll = new ToolStripMenuItem();
		miCaliAddPeak = new ToolStripMenuItem();
		toolStripSeparator6 = new ToolStripSeparator();
		miCaliOptions = new ToolStripMenuItem();
		tsCali = new ToolStrip();
		btnNewCali = new ToolStripButton();
		btnOpenCali = new ToolStripButton();
		btnSaveCali = new ToolStripButton();
		toolStripSeparator7 = new ToolStripSeparator();
		btnOpenStand = new ToolStripButton();
		btnCloseStand = new ToolStripButton();
		toolStripSeparator8 = new ToolStripSeparator();
		btnPreviousZoom = new ToolStripButton();
		btnNextZoom = new ToolStripButton();
		btnUnzoom = new ToolStripButton();
		toolStripSeparator9 = new ToolStripSeparator();
		btnAddAll = new ToolStripButton();
		btnAddPeak = new ToolStripButton();
		btnOptions = new ToolStripButton();
		toolStripSeparator10 = new ToolStripSeparator();
		ssCali = new StatusStrip();
		toolStripStatusLabel1 = new ToolStripStatusLabel();
		lclExpressLabel1 = new LclExpressLabel();
		splt = new LclSplitter();
		dpDisplay = new LclDisplayPanel();
		gvCmpds = new LclGridView();
		cmsCali = new ContextMenuStrip(icontainer_2);
		miColumnsSetup = new ToolStripMenuItem();
		miRestoreDftColumns = new ToolStripMenuItem();
		msCali.SuspendLayout();
		tsCali.SuspendLayout();
		ssCali.SuspendLayout();
		((ISupportInitialize)gvCmpds).BeginInit();
		cmsCali.SuspendLayout();
		SuspendLayout();
		msCali.Items.AddRange(new ToolStripItem[3] { miFile, miDisplay, miCalibration });
		msCali.Location = new Point(0, 0);
		msCali.Name = "msCali";
		msCali.Size = new Size(841, 24);
		msCali.TabIndex = 0;
		msCali.Text = "menuStrip1";
		miFile.DropDownItems.AddRange(new ToolStripItem[13]
		{
			miFiNewCali, miFiOpenCali, miFiSaveCali, miFiSaveAsCali, toolStripSeparator1, miFiOpenStand, miFiCloseStand, toolStripSeparator2, miFiReportSetup, miFiPreview,
			miFiPrint, toolStripSeparator3, miFiExit
		});
		miFile.Name = "miFile";
		miFile.Size = new Size(53, 20);
		miFile.Text = "miFile";
		miFiNewCali.Name = "miFiNewCali";
		miFiNewCali.Size = new Size(160, 22);
		miFiNewCali.Text = "miFiNewCali";
		miFiNewCali.Click += btnNewCali_Click;
		miFiOpenCali.Name = "miFiOpenCali";
		miFiOpenCali.Size = new Size(160, 22);
		miFiOpenCali.Text = "miFiOpenCali";
		miFiOpenCali.Click += btnOpenCali_Click;
		miFiSaveCali.Name = "miFiSaveCali";
		miFiSaveCali.Size = new Size(160, 22);
		miFiSaveCali.Text = "miFiSaveCali";
		miFiSaveCali.Click += btnSaveCali_Click;
		miFiSaveAsCali.Name = "miFiSaveAsCali";
		miFiSaveAsCali.Size = new Size(160, 22);
		miFiSaveAsCali.Text = "miFiSaveAsCali";
		miFiSaveAsCali.Click += miFiSaveAsCali_Click;
		toolStripSeparator1.Name = "toolStripSeparator1";
		toolStripSeparator1.Size = new Size(157, 6);
		miFiOpenStand.Name = "miFiOpenStand";
		miFiOpenStand.Size = new Size(160, 22);
		miFiOpenStand.Text = "miFiOpenStand";
		miFiOpenStand.Click += btnOpenStand_Click;
		miFiCloseStand.Name = "miFiCloseStand";
		miFiCloseStand.Size = new Size(160, 22);
		miFiCloseStand.Text = "miFiCloseStand";
		miFiCloseStand.Click += btnCloseStand_Click;
		toolStripSeparator2.Name = "toolStripSeparator2";
		toolStripSeparator2.Size = new Size(157, 6);
		miFiReportSetup.Name = "miFiReportSetup";
		miFiReportSetup.Size = new Size(160, 22);
		miFiReportSetup.Text = "miFiReportSetup";
		miFiReportSetup.Click += miFiReportSetup_Click;
		miFiPreview.Name = "miFiPreview";
		miFiPreview.Size = new Size(160, 22);
		miFiPreview.Text = "miFiPreview";
		miFiPreview.Click += miFiPreview_Click;
		miFiPrint.Name = "miFiPrint";
		miFiPrint.Size = new Size(160, 22);
		miFiPrint.Text = "miFiPrint";
		miFiPrint.Click += miFiPrint_Click;
		toolStripSeparator3.Name = "toolStripSeparator3";
		toolStripSeparator3.Size = new Size(157, 6);
		miFiExit.Name = "miFiExit";
		miFiExit.Size = new Size(160, 22);
		miFiExit.Text = "miFiExit";
		miDisplay.DropDownItems.AddRange(new ToolStripItem[5] { miDisPreviousZoom, miDisNextZoom, miDisUnzoom, toolStripSeparator5, miDisProperties });
		miDisplay.Name = "miDisplay";
		miDisplay.Size = new Size(71, 20);
		miDisplay.Text = "miDisplay";
		miDisPreviousZoom.Name = "miDisPreviousZoom";
		miDisPreviousZoom.Size = new Size(172, 22);
		miDisPreviousZoom.Text = "miDisPreviousZoom";
		miDisPreviousZoom.Click += btnPreviousZoom_Click;
		miDisNextZoom.Name = "miDisNextZoom";
		miDisNextZoom.Size = new Size(172, 22);
		miDisNextZoom.Text = "miDisNextZoom";
		miDisNextZoom.Click += btnNextZoom_Click;
		miDisUnzoom.Name = "miDisUnzoom";
		miDisUnzoom.Size = new Size(172, 22);
		miDisUnzoom.Text = "miDisUnzoom";
		miDisUnzoom.Click += btnUnzoom_Click;
		toolStripSeparator5.Name = "toolStripSeparator5";
		toolStripSeparator5.Size = new Size(169, 6);
		miDisProperties.Name = "miDisProperties";
		miDisProperties.Size = new Size(172, 22);
		miDisProperties.Text = "miDisProperties";
		miDisProperties.Click += miDisProperties_Click;
		miCalibration.DropDownItems.AddRange(new ToolStripItem[4] { miCaliAddAll, miCaliAddPeak, toolStripSeparator6, miCaliOptions });
		miCalibration.Name = "miCalibration";
		miCalibration.Size = new Size(95, 20);
		miCalibration.Text = "miCalibration";
		miCaliAddAll.Name = "miCaliAddAll";
		miCaliAddAll.Size = new Size(148, 22);
		miCaliAddAll.Text = "miCaliAddAll";
		miCaliAddAll.Click += btnAddAll_Click;
		miCaliAddPeak.Name = "miCaliAddPeak";
		miCaliAddPeak.Size = new Size(148, 22);
		miCaliAddPeak.Text = "miCaliAddPeak";
		miCaliAddPeak.Click += btnAddPeak_Click;
		toolStripSeparator6.Name = "toolStripSeparator6";
		toolStripSeparator6.Size = new Size(145, 6);
		miCaliOptions.Name = "miCaliOptions";
		miCaliOptions.Size = new Size(148, 22);
		miCaliOptions.Text = "miCaliOptions";
		miCaliOptions.Click += btnOptions_Click;
		tsCali.Items.AddRange(new ToolStripItem[15]
		{
			btnNewCali, btnOpenCali, btnSaveCali, toolStripSeparator7, btnOpenStand, btnCloseStand, toolStripSeparator8, btnPreviousZoom, btnNextZoom, btnUnzoom,
			toolStripSeparator9, btnAddAll, btnAddPeak, btnOptions, toolStripSeparator10
		});
		tsCali.Location = new Point(0, 24);
		tsCali.Name = "tsCali";
		tsCali.Size = new Size(841, 25);
		tsCali.TabIndex = 1;
		tsCali.Text = "toolStrip1";
		btnNewCali.DisplayStyle = ToolStripItemDisplayStyle.Image;
		btnNewCali.ImageTransparentColor = Color.Magenta;
		btnNewCali.Name = "btnNewCali";
		btnNewCali.Size = new Size(23, 22);
		btnNewCali.Text = "toolStripButton1";
		btnNewCali.Click += btnNewCali_Click;
		btnOpenCali.DisplayStyle = ToolStripItemDisplayStyle.Image;
		btnOpenCali.ImageTransparentColor = Color.Magenta;
		btnOpenCali.Name = "btnOpenCali";
		btnOpenCali.Size = new Size(23, 22);
		btnOpenCali.Text = "toolStripButton2";
		btnOpenCali.Click += btnOpenCali_Click;
		btnSaveCali.DisplayStyle = ToolStripItemDisplayStyle.Image;
		btnSaveCali.ImageTransparentColor = Color.Magenta;
		btnSaveCali.Name = "btnSaveCali";
		btnSaveCali.Size = new Size(23, 22);
		btnSaveCali.Text = "toolStripButton3";
		btnSaveCali.Click += btnSaveCali_Click;
		toolStripSeparator7.Name = "toolStripSeparator7";
		toolStripSeparator7.Size = new Size(6, 25);
		btnOpenStand.DisplayStyle = ToolStripItemDisplayStyle.Image;
		btnOpenStand.ImageTransparentColor = Color.Magenta;
		btnOpenStand.Name = "btnOpenStand";
		btnOpenStand.Size = new Size(23, 22);
		btnOpenStand.Text = "toolStripButton4";
		btnOpenStand.Click += btnOpenStand_Click;
		btnCloseStand.DisplayStyle = ToolStripItemDisplayStyle.Image;
		btnCloseStand.ImageTransparentColor = Color.Magenta;
		btnCloseStand.Name = "btnCloseStand";
		btnCloseStand.Size = new Size(23, 22);
		btnCloseStand.Text = "toolStripButton5";
		btnCloseStand.Click += btnCloseStand_Click;
		toolStripSeparator8.Name = "toolStripSeparator8";
		toolStripSeparator8.Size = new Size(6, 25);
		btnPreviousZoom.DisplayStyle = ToolStripItemDisplayStyle.Image;
		btnPreviousZoom.ImageTransparentColor = Color.Magenta;
		btnPreviousZoom.Name = "btnPreviousZoom";
		btnPreviousZoom.Size = new Size(23, 22);
		btnPreviousZoom.Text = "toolStripButton6";
		btnPreviousZoom.Click += btnPreviousZoom_Click;
		btnNextZoom.DisplayStyle = ToolStripItemDisplayStyle.Image;
		btnNextZoom.ImageTransparentColor = Color.Magenta;
		btnNextZoom.Name = "btnNextZoom";
		btnNextZoom.Size = new Size(23, 22);
		btnNextZoom.Text = "toolStripButton7";
		btnNextZoom.Click += btnNextZoom_Click;
		btnUnzoom.DisplayStyle = ToolStripItemDisplayStyle.Image;
		btnUnzoom.ImageTransparentColor = Color.Magenta;
		btnUnzoom.Name = "btnUnzoom";
		btnUnzoom.Size = new Size(23, 22);
		btnUnzoom.Text = "toolStripButton8";
		btnUnzoom.Click += btnUnzoom_Click;
		toolStripSeparator9.Name = "toolStripSeparator9";
		toolStripSeparator9.Size = new Size(6, 25);
		btnAddAll.DisplayStyle = ToolStripItemDisplayStyle.Image;
		btnAddAll.ImageTransparentColor = Color.Magenta;
		btnAddAll.Name = "btnAddAll";
		btnAddAll.Size = new Size(23, 22);
		btnAddAll.Text = "toolStripButton9";
		btnAddAll.Click += btnAddAll_Click;
		btnAddPeak.DisplayStyle = ToolStripItemDisplayStyle.Image;
		btnAddPeak.ImageTransparentColor = Color.Magenta;
		btnAddPeak.Name = "btnAddPeak";
		btnAddPeak.Size = new Size(23, 22);
		btnAddPeak.Text = "toolStripButton10";
		btnAddPeak.Click += btnAddPeak_Click;
		btnOptions.DisplayStyle = ToolStripItemDisplayStyle.Image;
		btnOptions.ImageTransparentColor = Color.Magenta;
		btnOptions.Name = "btnOptions";
		btnOptions.Size = new Size(23, 22);
		btnOptions.Text = "toolStripButton11";
		btnOptions.Click += btnOptions_Click;
		toolStripSeparator10.Name = "toolStripSeparator10";
		toolStripSeparator10.Size = new Size(6, 25);
		ssCali.Items.AddRange(new ToolStripItem[1] { toolStripStatusLabel1 });
		ssCali.Location = new Point(0, 515);
		ssCali.Name = "ssCali";
		ssCali.Size = new Size(841, 22);
		ssCali.TabIndex = 2;
		ssCali.Text = "statusStrip1";
		toolStripStatusLabel1.Name = "toolStripStatusLabel1";
		toolStripStatusLabel1.Size = new Size(131, 17);
		toolStripStatusLabel1.Text = "toolStripStatusLabel1";
		lclExpressLabel1.Dock = DockStyle.Top;
		lclExpressLabel1.Location = new Point(0, 261);
		lclExpressLabel1.Name = "lclExpressLabel1";
		lclExpressLabel1.Size = new Size(841, 23);
		lclExpressLabel1.TabIndex = 5;
		lclExpressLabel1.Text = "lclExpressLabel1";
		lclExpressLabel1.TextAlign = ContentAlignment.MiddleCenter;
		splt.BorderStyle = BorderStyle.FixedSingle;
		splt.Dock = DockStyle.Top;
		splt.Location = new Point(0, 256);
		splt.Name = "splt";
		splt.Size = new Size(841, 5);
		splt.TabIndex = 4;
		splt.TabStop = false;
		dpDisplay.BackColor = Color.BlanchedAlmond;
		dpDisplay.Dock = DockStyle.Top;
		dpDisplay.Location = new Point(0, 49);
		dpDisplay.Name = "dpDisplay";
		dpDisplay.Size = new Size(841, 207);
		dpDisplay.TabIndex = 3;
		gvCmpds.AllowUserToAddRows = false;
		gvCmpds.AllowUserToDeleteRows = false;
		gvCmpds.AllowUserToResizeRows = false;
		gvCmpds.BackgroundColor = Color.AliceBlue;
		gvCmpds.CharacterHeaderColor = Color.Black;
		gvCmpds.ColumnHeadersHeight = 32;
		gvCmpds.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
		gvCmpds.ContextMenuStrip = cmsCali;
		gvCmpds.EditMode = DataGridViewEditMode.EditProgrammatically;
		gvCmpds.Location = new Point(43, 309);
		gvCmpds.Name = "gvCmpds";
		gvCmpds.RowHeadersWidth = 25;
		gvCmpds.RowTemplate.Height = 16;
		gvCmpds.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
		gvCmpds.ShowCellToolTips = false;
		gvCmpds.Size = new Size(205, 93);
		gvCmpds.TabIndex = 6;
		cmsCali.Items.AddRange(new ToolStripItem[2] { miColumnsSetup, miRestoreDftColumns });
		cmsCali.Name = "cmsCali";
		cmsCali.Size = new Size(209, 48);
		miColumnsSetup.Name = "miColumnsSetup";
		miColumnsSetup.Size = new Size(208, 22);
		miColumnsSetup.Text = "miColumnsSetup";
		miColumnsSetup.Click += miRestoreDftColumns_Click;
		miRestoreDftColumns.Name = "miRestoreDftColumns";
		miRestoreDftColumns.Size = new Size(208, 22);
		miRestoreDftColumns.Text = "miRestoreDftColumns";
		miRestoreDftColumns.Click += miRestoreDftColumns_Click;
		base.AutoScaleDimensions = new SizeF(6f, 12f);
		base.ClientSize = new Size(841, 537);
		base.Controls.Add(gvCmpds);
		base.Controls.Add(lclExpressLabel1);
		base.Controls.Add(splt);
		base.Controls.Add(dpDisplay);
		base.Controls.Add(ssCali);
		base.Controls.Add(tsCali);
		base.Controls.Add(msCali);
		base.MainMenuStrip = msCali;
		base.Name = "CaliGpcForm";
		base.Load += CaliGpcForm_Load;
		msCali.ResumeLayout(performLayout: false);
		msCali.PerformLayout();
		tsCali.ResumeLayout(performLayout: false);
		tsCali.PerformLayout();
		ssCali.ResumeLayout(performLayout: false);
		ssCali.PerformLayout();
		((ISupportInitialize)gvCmpds).EndInit();
		cmsCali.ResumeLayout(performLayout: false);
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
			miFiNewCali.Text = "新建";
			miFiOpenCali.Text = "打开...";
			miFiSaveCali.Text = "保存";
			miFiSaveAsCali.Text = "另存...";
			miFiOpenStand.Text = "打开标样";
			miFiCloseStand.Text = "关闭标样";
			miFiReportSetup.Text = "样式文件...";
			miFiPreview.Text = "预览";
			miFiPrint.Text = "打印";
			miFiExit.Text = "退出";
			miDisplay.Text = "显示";
			miDisPreviousZoom.Text = "后退";
			miDisNextZoom.Text = "前进";
			miDisUnzoom.Text = "复位";
			miDisProperties.Text = "属性...";
			miCalibration.Text = "校正";
			miCaliAddAll.Text = "添加所有峰";
			miCaliAddPeak.Text = "添加峰";
			miCaliOptions.Text = "选项...";
			miColumnsSetup.Text = "列设置...";
			miRestoreDftColumns.Text = "恢复默认列设置";
			break;
		case SysLanguage.EN:
			miFile.Text = "File";
			miFiNewCali.Text = "New";
			miFiOpenCali.Text = "Open...";
			miFiSaveCali.Text = "Save";
			miFiSaveAsCali.Text = "Save as...";
			miFiOpenStand.Text = "Open Standard...";
			miFiCloseStand.Text = "Close Standard";
			miFiReportSetup.Text = "Style Set...";
			miFiPreview.Text = "Preview";
			miFiPrint.Text = "Print";
			miFiExit.Text = "Exit";
			miDisplay.Text = "Display";
			miDisPreviousZoom.Text = "Previous Zoom";
			miDisNextZoom.Text = "Next Zoom";
			miDisUnzoom.Text = "Unzoom";
			miDisProperties.Text = "Properties...";
			miCalibration.Text = "Calibration";
			miCaliAddAll.Text = "Add All";
			miCaliAddPeak.Text = "Add Peak";
			miCaliOptions.Text = "Options...";
			miColumnsSetup.Text = "Columns Setup...";
			miRestoreDftColumns.Text = "Restore Default Columns";
			break;
		}
		btnOpenCali.Text = miFiOpenCali.Text;
		btnNewCali.Text = miFiNewCali.Text;
		btnSaveCali.Text = miFiSaveCali.Text;
		btnOpenStand.Text = miFiOpenStand.Text;
		btnCloseStand.Text = miFiCloseStand.Text;
		btnPreviousZoom.Text = miDisPreviousZoom.Text;
		btnNextZoom.Text = miDisNextZoom.Text;
		btnUnzoom.Text = miDisUnzoom.Text;
		btnAddAll.Text = miCaliAddAll.Text;
		btnAddPeak.Text = miCaliAddPeak.Text;
		btnOptions.Text = miCaliOptions.Text;
	}

	private void btnAddAll_Click(object sender, EventArgs e)
	{
	}

	private void btnAddPeak_Click(object sender, EventArgs e)
	{
	}

	private void btnOptions_Click(object sender, EventArgs e)
	{
		if (caliGpcOptDlg_0 == null)
		{
			caliGpcOptDlg_0 = new CaliGpcOptDlg();
		}
		caliGpcOptDlg_0.ShowDialog();
	}

	private void btnNextZoom_Click(object sender, EventArgs e)
	{
	}

	private void btnPreviousZoom_Click(object sender, EventArgs e)
	{
	}

	private void miDisProperties_Click(object sender, EventArgs e)
	{
		Class49.optionsDialog_0.ShowDialog(instrument, WinStyle.CaliGpc, instrument.user.options);
	}

	private void btnUnzoom_Click(object sender, EventArgs e)
	{
	}

	private void btnCloseStand_Click(object sender, EventArgs e)
	{
	}

	private void btnNewCali_Click(object sender, EventArgs e)
	{
	}

	private void btnOpenCali_Click(object sender, EventArgs e)
	{
	}

	private void btnOpenStand_Click(object sender, EventArgs e)
	{
	}

	private void miFiPreview_Click(object sender, EventArgs e)
	{
	}

	private void miFiPrint_Click(object sender, EventArgs e)
	{
	}

	private void miFiReportSetup_Click(object sender, EventArgs e)
	{
	}

	private void miFiSaveAsCali_Click(object sender, EventArgs e)
	{
	}

	private void btnSaveCali_Click(object sender, EventArgs e)
	{
	}

	public override void ReadWinInfo(WinInfo winInfo)
	{
		base.ReadWinInfo(winInfo);
		if (winInfo.para1 > 0)
		{
			dpDisplay.Height = winInfo.para1;
		}
		int gvNo = 0;
		winInfo.gvCF_w(gvCmpds, ref gvNo);
	}

	private void method_2()
	{
	}

	public override void refresh_once()
	{
		base.refresh_once();
		method_3();
	}

	private void method_3()
	{
		Text = sTitle + "[" + instrument.name + "]";
	}

	public override void WriteWinInfo(WinInfo winInfo)
	{
		base.WriteWinInfo(winInfo);
		winInfo.para1 = dpDisplay.Height;
		winInfo.gvCF_r(gvCmpds);
	}
}
