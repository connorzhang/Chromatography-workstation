using System;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing.Text;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using IBrainChrom2018.Report;
using IBrainChrom2018.Unit;
using Microsoft.Office.Interop.Word;
using NPOI.OpenXmlFormats.Wordprocessing;
using NPOI.XWPF.UserModel;

namespace IBrainChrom2018;

public class MineChromCtrl : UserControl
{
	private const int WM_PRINT = 791;

	private const int PRF_CHECKVISIBLE = 0;

	private const int PRF_NONCLIENT = 2;

	private const int PRF_CLIENT = 4;

	private const int PRF_ERASEBKGND = 8;

	private const int PRF_CHILDREN = 16;

	private SystemParam sysParam = SystemParam.Create();

	private FormMainParam frmParam = FormMainParam.Create();

	private ChromDeviceListMgr cdlMgr = ChromDeviceListMgr.Create();

	private bool bShowOnlineMethod;

	private TreeNode treeNode_0;

	private frmopenfile frmopenfile_0 = new frmopenfile();

	public static MineChromCtrl form = null;

	private RichTextBox rtprtb = new RichTextBox();

	private bool m_ShowManuAndStateBar = true;

	private SSTCmpd sstcmpd_0;

	private DetectorStyle detectorStyle_0;

	private DisLg disLg_0 = default(DisLg);

	private CusDlg cusDlg_0;

	public int psmgBottom;

	public int psmgInterval;

	public int psmgLeft;

	public int psmgRight;

	public int psmgTop;

	private float float_3;

	private float float_4;

	private StringFormat stringFormat_0 = new StringFormat();

	private float float_5;

	private float float_6;

	private System.Drawing.Font font_0;

	private System.Drawing.Font font_1;

	private float float_7;

	private float float_0 = -1f;

	private float float_1 = -1f;

	private string strSdaDataFileDir = "";

	private string strDirOptionInitDir = "";

	private string m_strChormFileName = "";

	public static Color istdBkColor = Color.LightGray;

	private bool bool_1;

	private System.Drawing.Point point_0;

	private System.Drawing.Point point_1;

	private PointF pointF_0;

	private object object_0;

	private byte byte_0;

	private System.Drawing.Rectangle rectangle_0 = new System.Drawing.Rectangle(5, 5, 0, 50);

	private System.Drawing.Point point_2;

	private string string_277 = "面积\n[" + Class49.MesureUnit() + ".s]";

	private string string_278 = "高度\n[" + Class49.MesureUnit() + "]";

	private bool bool_5;

	private CmpdDisplay class8_0 = new CmpdDisplay(WinStyle.CaliGnl, null);

	private RectangleF rectangleF_1 = default(RectangleF);

	private Pen pen_0 = new Pen(Color.Black, 1f);

	private SizeF sizeF_0 = new SizeF(350f, 330f);

	private SizeF sizeF_1 = new SizeF(550f, 230f);

	private SizeF sizeF_2 = new SizeF(450f, 190f);

	private SolidBrush solidBrush_0 = new SolidBrush(Color.Black);

	private static System.Drawing.Rectangle rectangle_1;

	public Chromatogram chromatogram_1;

	public Chromatogram[] chromatogram_0 = new Chromatogram[0];

	public ChromDisplay chromDisplay_0;

	private RectangleF rectangleF_0;

	private IntegRow integRow_0;

	private IntegRow integRow_1;

	private IntegRow integRow_2;

	private SmyTabOpt smyTabOpt_0 = new SmyTabOpt();

	private Options options_0 = new Options();

	public MyOfdChrom ofdChrom = new MyOfdChrom();

	private SmyTabOptDlg smyTabOptDlg_0 = new SmyTabOptDlg();

	private OpenFileDialog openFileDialog_2 = new OpenFileDialog();

	private OpenFileDialog openFileDialog_3 = new OpenFileDialog();

	private SaveFileDialog saveFileDialog_0;

	private SaveFileDialog saveFileDialog_1 = new SaveFileDialog();

	private ToolStripMenuItem toolStripMenuItem2;

	private ToolStripMenuItem toolStripMenuItem1;

	private ToolStripSeparator toolStripSeparator2;

	private ToolStripSeparator toolStripSeparator6;

	private ToolStripButton btnbsBsBackHorz;

	private ToolStripButton btnbsBsForwHorz;

	private ToolStripButton btnbsBsFrontTgnt;

	private ToolStripButton btnbsBsTailTgnt;

	private ToolStripButton btnbsBsTgnt;

	private ToolStripButton btnbsBsTogether;

	private ToolStripButton btnbsBsValley;

	private ToolStripButton btnbsBsVtV;

	private ToolStripButton btnClose;

	private ToolStripButton btnExpress;

	private ToolStripButton btngblDtecDelay;

	private ToolStripButton btngblPeakWidth;

	private ToolStripButton btngblPkSlope;

	private ToolStripButton btngblThreshold;

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

	private ToolStripButton btnOverlayMode;

	private ToolStripButton btnPreview;

	private ToolStripButton btnPreviousZoom;

	private ToolStripButton btnProperties;

	private ToolStripButton btnPrtLink;

	private ToolStripButton btnReportSetup;

	private ToolStripButton btnSave;

	private ToolStripButton btnUnzoom;

	private ContextMenuStrip cmsLibs;

	private ContextMenuStrip cmsSlices;

	private ContextMenuStrip cmsSSTCmpds;

	private IContainer icontainer_0;

	private LbLineDlg lbLineDlg_0 = new LbLineDlg();

	private LbTextDlg lbTextDlg_0 = new LbTextDlg();

	private ManuDlg manuDlg_0;

	private SSTParasDlg sstparasDlg_0;

	private LclDisplayPanel dpgnlChrom;

	private FlowLayoutPanel flpChrom;

	private ToolStripLabel lbSignal;

	private ToolStripLabel lbSignalU;

	private ToolStripLabel lbTime;

	private ToolStripLabel lbTimeU;

	private ToolStripLabel lbyUnit;

	private SST sst_0 = new SST();

	private ToolStripMenuItem miAddRow;

	private ToolStripMenuItem miChmCreateLabel;

	private ToolStripMenuItem miChmRemoveLabels;

	private ToolStripMenuItem miChromatogram;

	private ToolStripMenuItem miclLine;

	private ToolStripMenuItem miclText;

	private ToolStripMenuItem miDeleteRow;

	private ToolStripMenuItem miDisNextZoom;

	private ToolStripMenuItem miDisplay;

	private ToolStripMenuItem miDisPreviousZoom;

	private ToolStripMenuItem miDisUnzoom;

	private ToolStripMenuItem miFiClose;

	private ToolStripMenuItem miFiCloseAll;

	private ToolStripMenuItem miFiExit;

	private ToolStripMenuItem miFile;

	private ToolStripMenuItem miFiOpen;

	private ToolStripMenuItem miFiOverlayMode;

	private ToolStripMenuItem miFiPreview;

	private ToolStripMenuItem miFiPrint;

	private ToolStripMenuItem miFiSave;

	private ToolStripMenuItem miFiSaveAs;

	private ToolStripMenuItem toolStripMenuItem_0 = new ToolStripMenuItem();

	private ToolStripMenuItem miMethod;

	private ToolStripMenuItem miMtdSaveTplt;

	private ToolStripMenuItem miMtdTplt;

	private ToolStripMenuItem mirlActiveChrom;

	private ToolStripMenuItem mirlAllChroms;

	private ToolStripMenuItem mirlSelected;

	private ToolStripMenuItem mislcColumnsSetup;

	private ToolStripMenuItem mislcRestoreDftColumns;

	private ToolStripMenuItem misstcClearParas;

	private ToolStripMenuItem misstcNew;

	private ToolStripMenuItem misstColumnsSetup;

	private ToolStripMenuItem misstcOpen;

	private ToolStripMenuItem misstcSave;

	private ToolStripMenuItem misstcSaveas;

	private ToolStripMenuItem misstcSet;

	private ToolStripMenuItem misstcUpdateFromCalib;

	private ToolStripMenuItem misstRestoreDftColumns;

	private ToolStripMenuItem toolStripMenuItem_1 = new ToolStripMenuItem();

	private MenuStrip msChrom;

	private ToolStripStatusLabel slbExplain;

	private StatusStrip ssChrom;

	private ToolStripTextBox tbSigYBeg;

	private ToolStripTextBox tbSigYEnd;

	private ToolStripTextBox tbTime;

	private ToolStrip toolStrip1;

	private ToolStrip toolStrip2;

	private ToolStripSeparator toolStripSeparator1;

	private ToolStripSeparator toolStripSeparator14;

	private ToolStripSeparator toolStripSeparator16;

	private ToolStripSeparator toolStripSeparator17;

	private ToolStripSeparator toolStripSeparator18;

	private ToolStripSeparator toolStripSeparator19;

	private ToolStripSeparator toolStripSeparator20;

	private ToolStripSeparator toolStripSeparator21;

	private ToolStripSeparator toolStripSeparator22;

	private ToolStripSeparator toolStripSeparator23;

	private ToolStripSeparator toolStripSeparator28;

	private ToolStripSeparator toolStripSeparator3;

	private ToolStripSeparator toolStripSeparator31;

	private ToolStripSeparator toolStripSeparator34;

	private ToolStripSeparator toolStripSeparator4;

	private ToolStripSeparator toolStripSeparator5;

	private ToolStripSeparator toolStripSeparator8;

	private ToolStrip tsDatAcq;

	private ToolStripSeparator tss1;

	private ToolStripLabel toolStripLabel1;

	private ToolStripSeparator toolStripSeparator24;

	private ToolStripSeparator toolStripSeparator35;

	private ToolStripButton toolStripButton1;

	private ToolStripButton toolStripButton2;

	private ToolStripButton toolStripButton3;

	private ToolStripButton toolStripButton4;

	private ToolStripButton toolStripButton5;

	public DataGridView dataGridView2;

	private DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;

	private DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;

	private DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;

	private DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;

	private DataGridViewTextBoxColumn dataGridViewTextBoxColumn9;

	private Button button8;

	private Button button7;

	private Button button6;

	private Button button5;

	public DataGridView dataGridView3;

	private DataGridViewTextBoxColumn dataGridViewTextBoxColumn10;

	private DataGridViewTextBoxColumn dataGridViewTextBoxColumn11;

	private DataGridViewTextBoxColumn dataGridViewTextBoxColumn12;

	private DataGridViewTextBoxColumn dataGridViewTextBoxColumn13;

	private DataGridViewTextBoxColumn dataGridViewTextBoxColumn14;

	private Button button3;

	private Button button4;

	private Button button9;

	private Button button10;

	private ImageList imageList_0;

	private ToolStripSeparator tss2;

	private ImageList imageList_1;

	private ToolStripSeparator toolStripSeparator7;

	private ToolStripButton toolStripButton6;

	private ToolStripStatusLabel HeatValue;

	private ToolStripSeparator toolStripSeparator9;

	private Button btnTimeFull;

	private Button btnXYFull;

	private MaskedTextBox mtbTime;

	private Label label32;

	private MaskedTextBox mtbSigYEnd;

	private Label label31;

	private MaskedTextBox mtbSigYBeg;

	private Label label29;

	private Label label119;

	private Label label1;

	private FolderBrowserDialog folderBrowserDialog_0;

	private ToolStripButton btnOpen;

	public RptSetupDlg dlgReportSetup;

	private IContainer components;

	public MstSet mstSetChromForm;

	public ChromFormDataGrid chromDataGrid;

	public bool ShowOnlineMethod
	{
		get
		{
			return bShowOnlineMethod;
		}
		set
		{
			bShowOnlineMethod = value;
			if (bShowOnlineMethod)
			{
				mstSetChromForm.ShowOnlineMethod = true;
				toolStrip2.Width = 129;
			}
			else
			{
				mstSetChromForm.ShowOnlineMethod = false;
				toolStrip2.Width = 800;
			}
		}
	}

	public Chromatogram CurChrom => chromatogram_1;

	public DisLg CurDisLg => chromDisplay_0.disLg;

	private Signal CurSignal => chromDisplay_0.curSignal;

	public bool HasChrom => chromatogram_0.Length != 0;

	private ChromFormInterface formMain => cdlMgr.formMain;

	private LclGridView lclGvRltsGnl => chromDataGrid.gvRltsGnl;

	public bool ShowManuAndStateBar
	{
		get
		{
			return m_ShowManuAndStateBar;
		}
		set
		{
			m_ShowManuAndStateBar = value;
			if (m_ShowManuAndStateBar)
			{
				msChrom.Visible = true;
				return;
			}
			msChrom.Visible = false;
			ssChrom.Visible = false;
		}
	}

	[DllImport("gdi32.dll", CharSet = CharSet.Auto, ExactSpelling = true, SetLastError = true)]
	private static extern bool BitBlt(HandleRef hDC, int x, int y, int nWidth, int nHeight, HandleRef hSrcDC, int xSrc, int ySrc, int dwRop);

	[DllImport("user32.dll", CharSet = CharSet.Auto)]
	private static extern IntPtr SendMessage(HandleRef hWnd, int msg, int wParam, int lParam);

	[DllImport("user32.dll", CharSet = CharSet.Auto)]
	private static extern IntPtr SendMessage(HandleRef hWnd, int msg, int wParam, int[] lParam);

	[DllImport("user32.dll")]
	private static extern bool OpenClipboard(IntPtr hWndNewOwner);

	[DllImport("user32.dll")]
	private static extern bool EmptyClipboard();

	[DllImport("user32.dll")]
	private static extern IntPtr SetClipboardData(uint uFormat, IntPtr hMem);

	[DllImport("user32.dll")]
	private static extern bool CloseClipboard();

	[DllImport("gdi32.dll")]
	private static extern IntPtr CopyEnhMetaFile(IntPtr hemfSrc, IntPtr hNULL);

	[DllImport("gdi32.dll")]
	private static extern bool DeleteEnhMetaFile(IntPtr hemf);

	public static bool PutEnhMetafileOnClipboard(IntPtr hWnd, Metafile mf)
	{
		bool result = false;
		IntPtr henhmetafile = mf.GetHenhmetafile();
		if (!henhmetafile.Equals(new IntPtr(0)))
		{
			IntPtr intPtr = CopyEnhMetaFile(henhmetafile, new IntPtr(0));
			if (!intPtr.Equals(new IntPtr(0)) && OpenClipboard(hWnd) && EmptyClipboard())
			{
				result = SetClipboardData(14u, intPtr).Equals(intPtr);
				CloseClipboard();
			}
			DeleteEnhMetaFile(henhmetafile);
		}
		return result;
	}

	public static void CopyControlToClipboard(Control control)
	{
		Graphics graphics = control.CreateGraphics();
		IntPtr hdc = graphics.GetHdc();
		Metafile metafile = new Metafile(hdc, new System.Drawing.Rectangle(0, 0, control.Width, control.Height), MetafileFrameUnit.Pixel, EmfType.EmfPlusDual);
		graphics.ReleaseHdc(hdc);
		graphics.Dispose();
		Graphics graphics2 = Graphics.FromImage(metafile);
		DrawControl(control, graphics2);
		graphics2.Dispose();
		PutEnhMetafileOnClipboard(control.Handle, metafile);
	}

	public static void DrawControl(Control control, Graphics g)
	{
		if (!control.Created)
		{
			control.CreateControl();
		}
		IntPtr hdc = g.GetHdc();
		SendMessage(new HandleRef(control, control.Handle), 791, (int)hdc, 30);
		g.ReleaseHdc(hdc);
	}

	private Chromatogram[] GetChromatogramList()
	{
		return chromatogram_0;
	}

	private Chromatogram GetChromatogram()
	{
		return CurChrom;
	}

	private ChromDisplay GetChromDisplay()
	{
		return chromDisplay_0;
	}

	private SmyTabOpt GetSmyTabOpt()
	{
		return smyTabOpt_0;
	}

	public bool GetHasChrom()
	{
		return HasChrom;
	}

	public void InitChromFormDataGrid()
	{
		chromDataGrid.GetChromatogramList = GetChromatogramList;
		chromDataGrid.GetChromatogram = GetChromatogram;
		chromDataGrid.GetChromDisplay = GetChromDisplay;
		chromDataGrid.GetSmyTabOpt = GetSmyTabOpt;
		chromDataGrid.GetHasChrom = GetHasChrom;
		chromDataGrid.OnDisDpRefresh += ChromDataGrid_OnDisDpRefresh;
	}

	private void ChromDataGrid_OnDisDpRefresh(object sender, EventArgs e)
	{
		DisDpRefresh();
	}

	public void InitFm()
	{
		chromDataGrid.InitFm();
	}

	public MineChromCtrl()
	{
		InitializeComponent();
		form = this;
		if (!IsDesignMode())
		{
			InitChromFormDataGrid();
			chromatogram_1 = new Chromatogram();
			chromatogram_0 = new Chromatogram[1];
			chromatogram_0[0] = new Chromatogram();
			if (chromatogram_1.signal.applyIntegs_0 == null)
			{
				chromatogram_1.signal.applyIntegs_0 = new ApplyIntegs();
			}
			Loading();
		}
	}

	private void Loading()
	{
		strDirOptionInitDir = sysParam.strDirOptionInitDir;
		strSdaDataFileDir = sysParam.strSdaDataFileDir;
		if (strDirOptionInitDir == "")
		{
			strDirOptionInitDir = System.Windows.Forms.Application.StartupPath;
		}
		lbyUnit.Text = Class49.MesureUnit();
		toolStripMenuItem_1.Text = Lang.PS("标准按钮", "Stand. Buttons");
		toolStripMenuItem_1.Click += toolStripMenuItem_1_Click;
		toolStripMenuItem_1_Click(null, null);
		toolStripMenuItem_0.Text = Lang.PS("手动积分按钮", "Manual itg. Buttons");
		toolStripMenuItem_0.Click += toolStripMenuItem_0_Click;
		openFileDialog_3.Title = Lang.PS("打开SST文件", "Open SST File");
		openFileDialog_3.Filter = Class49.MakeFileFilter(".sst");
		openFileDialog_3.Multiselect = false;
		saveFileDialog_1.Title = Lang.PS("保存SST文件", "Save SST File");
		saveFileDialog_1.Filter = Class49.MakeFileFilter(".sst");
		chromDisplay_0 = new ChromDisplay(WinStyle.Chromatogram, dpgnlChrom);
		chromDisplay_0.OnSignalClick += method_3;
		chromDisplay_0.OnSignalDoubleClick += method_4;
		chromDisplay_0.showMouseLgValue = false;
		chromDisplay_0.showProgTemp = false;
		chromDisplay_0.curSignal = new Signal();
		chromDisplay_0.ExtDraw_begin();
		LoadOptions();
		dpgnlChrom.Refresh();
		manuDlg_0 = new ManuDlg();
		manuDlg_0.TopMost = true;
		manuDlg_0.OnSuggestClick += method_8;
		manuDlg_0.OnOKClick += method_7;
		btngblDtecDelay.Tag = IntegOprtStyle.DtecDelay;
		btngblPeakWidth.Tag = IntegOprtStyle.PeakWidth;
		btngblThreshold.Tag = IntegOprtStyle.Threshold;
		btngblPkSlope.Tag = IntegOprtStyle.VtVSlope;
		btnipResetDtecNeg.Tag = IntegOprtStyle.ResetDtecNeg;
		btnipClampNeg.Tag = IntegOprtStyle.ClampNeg;
		btnipPkWidth.Tag = IntegOprtStyle.PkWidth;
		btnipPkThreshold.Tag = IntegOprtStyle.PkThreshold;
		btnipPkAddPosi.Tag = IntegOprtStyle.PkAddPosi;
		btnipPkAddNeg.Tag = IntegOprtStyle.PkAddNeg;
		btnipPkCut.Tag = IntegOprtStyle.PkCut;
		btnipPkHalfWidth.Tag = IntegOprtStyle.PkHalfWidth;
		btnipPkArea.Tag = IntegOprtStyle.PkArea;
		btnipPkVale.Tag = IntegOprtStyle.PkVale;
		btnipSolventPeak.Tag = IntegOprtStyle.SolventPeak;
		btnipFlowMarker.Tag = IntegOprtStyle.FlowMarker;
		btnipGroups.Tag = IntegOprtStyle.GroupAdd;
		btnbsBsTgnt.Tag = IntegOprtStyle.BsTgnt;
		btnbsBsVtV.Tag = IntegOprtStyle.BsVtV;
		btnbsBsValley.Tag = IntegOprtStyle.BsValley;
		btnbsBsTogether.Tag = IntegOprtStyle.BsTogether;
		btnbsBsForwHorz.Tag = IntegOprtStyle.BsForwHorz;
		btnbsBsBackHorz.Tag = IntegOprtStyle.BsBackHorz;
		btnbsBsFrontTgnt.Tag = IntegOprtStyle.BsFrontTgnt;
		btnbsBsTailTgnt.Tag = IntegOprtStyle.BsTailTgnt;
	}

	public static bool IsDesignMode()
	{
		return false;
	}

	private void ChromForm_Load(object sender, EventArgs e)
	{
		if (IsDesignMode())
		{
			return;
		}
		try
		{
			if (FrameDis.font == null)
			{
				FrameDis.font = new System.Drawing.Font("Tahoma", 8f);
				User user = new User();
				user.options.InitGradientColors();
				user.LoadUserOptions();
				chromDisplay_0.LinkOptions(user.options);
				chromDisplay_0.ShowBgChrom = true;
				chromDisplay_0.setShowGrid = true;
				Class49.SetColor(0, Color.White);
			}
		}
		catch
		{
			FrameDis.font = (System.Drawing.Font)Font.Clone();
		}
		btnExpress_Click(null, null);
		integRow_1.oprtStyle = IntegOprtStyle.Noise;
		integRow_0.oprtStyle = IntegOprtStyle.Drift;
		OpenFileDialog openFileDialog = new OpenFileDialog();
		openFileDialog.Tag = "PDA";
		openFileDialog.Filter = Class49.MakeFileFilter(".lib");
		sstparasDlg_0 = new SSTParasDlg();
		mstSetChromForm.lclButton1.Visible = false;
		mstSetChromForm.lclButton2.Visible = false;
		mstSetChromForm.UsePlace = 1;
		mstSetChromForm.checkBox4.Enabled = false;
		mstSetChromForm.checkBox5.Enabled = false;
		mstSetChromForm.checkBox6.Enabled = false;
		mstSetChromForm.checkBox7.Enabled = false;
		mstSetChromForm.tbMethNameP.Text = "";
		chromDataGrid.OnAddAllCompnent += mstSetChromForm_OnAddAllCompnent;
		mstSetChromForm.OnAddAllCompnent += mstSetChromForm_OnAddAllCompnent;
		mstSetChromForm.OnDisDpRefresh += ChromDataGrid_OnDisDpRefresh;
		LoadLanguage();
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

	public void LoadLanguage()
	{
		lbTime.Text = Lang.PS("时轴", "[min]");
		lbSignal.Text = Lang.PS("信号", "Signal");
		miFile.Text = Lang.PS("文件", "File");
		miFiOverlayMode.Text = Lang.PS("重叠模式", "Overlay Mode");
		miFiOpen.Text = Lang.PS("打开...", "Open...");
		miFiClose.Text = Lang.PS("关闭", "Close");
		miFiCloseAll.Text = Lang.PS("关闭全部", "Close All");
		miFiSave.Text = Lang.PS("保存", "Save");
		miFiSaveAs.Text = Lang.PS("另存...", "Save as...");
		toolStripMenuItem2.Text = Lang.PS("编辑组份表", "Edit ComponentTable");
		miFiPreview.Text = Lang.PS("预览", "Preview");
		miFiPrint.Text = Lang.PS("打印", "Print");
		miFiExit.Text = Lang.PS("退出", "Exit");
		miDisplay.Text = Lang.PS("显示", "Display");
		miDisPreviousZoom.Text = Lang.PS("后退", "Previous Zoom");
		miDisNextZoom.Text = Lang.PS("前进", "Next Zoom");
		miDisUnzoom.Text = Lang.PS("复位", "Unzoom");
		miChromatogram.Text = Lang.PS("谱图", "Chromatogram Chart");
		miChmCreateLabel.Text = Lang.PS("创建标识", "Create Label");
		miclText.Text = Lang.PS("文本...", "Text...");
		miclLine.Text = Lang.PS("直线...", "Line...");
		miChmRemoveLabels.Text = Lang.PS("移除标识", "Remove Label(s)");
		miMtdTplt.Text = Lang.PS("帮助", "Help");
		miMtdSaveTplt.Text = Lang.PS("关于", "About");
		mirlSelected.Text = Lang.PS("选择的", "Selected");
		mirlActiveChrom.Text = Lang.PS("当前谱图", "ActiveChrom");
		mirlAllChroms.Text = Lang.PS("所有谱图", "All Chroms");
		miMethod.Text = Lang.PS("帮助", "Help");
		btnExpress.Text = Lang.PS("动态帮助", "Baloon help");
		miAddRow.Text = Lang.PS("添加库", "Add Library");
		miDeleteRow.Text = Lang.PS("删除库", "Delete Library");
		misstColumnsSetup.Text = Lang.PS("列设置...", "Columns Setup...");
		misstRestoreDftColumns.Text = Lang.PS("恢复默认列设置", "Restore Default Columns");
		mislcColumnsSetup.Text = Lang.PS("列设置...", "Columns Setup...");
		mislcRestoreDftColumns.Text = Lang.PS("恢复默认列设置", "Restore Default Columns");
		btnExpress.ToolTipText = Lang.PS("显示操作描述", "Baloon help");
		btngblPeakWidth.ToolTipText = Lang.PS("峰宽参数", "PeakWidth");
		btngblThreshold.ToolTipText = Lang.PS("峰高参数", "PkHeight");
		btngblPkSlope.ToolTipText = Lang.PS("峰斜率", "PkSlope");
		btnipResetDtecNeg.ToolTipText = Lang.PS("检测负峰", "ResetDtecNeg");
		btnipClampNeg.ToolTipText = Lang.PS("负峰翻转", "ClampNeg");
		btnipPkWidth.ToolTipText = Lang.PS("最小峰宽", "miipPkWidth");
		btnipPkThreshold.ToolTipText = Lang.PS("最小峰高", "miipPkHeight");
		btnipPkAddPosi.ToolTipText = Lang.PS("添加正峰", "AddPeak");
		btnipPkAddNeg.ToolTipText = Lang.PS("添加负峰", "AddNeg");
		btnipPkCut.ToolTipText = Lang.PS("删除峰", "PkCut");
		btnipPkHalfWidth.ToolTipText = Lang.PS("最小半峰宽", "miPkHalfWidth");
		btnipPkArea.ToolTipText = Lang.PS("最小峰面积", "miPkArea");
		btnipPkVale.ToolTipText = Lang.PS("谷点", "PkVale");
		btnipSolventPeak.ToolTipText = Lang.PS("峰分离", "SolventPeak");
		btnipFlowMarker.ToolTipText = Lang.PS("流速标识", "FlowMarker");
		btnipGroups.ToolTipText = Lang.PS("添加组", "AddGroups");
		btnbsBsTgnt.ToolTipText = Lang.PS("切肩参数", "bsBsTgnt");
		btnbsBsVtV.ToolTipText = Lang.PS("谷.谷斜率", "bsBsVtV");
		btnbsBsValley.ToolTipText = Lang.PS("经过谷点", "BsValley");
		btnbsBsTogether.ToolTipText = Lang.PS("整合基线", "BsTogether");
		btnbsBsForwHorz.ToolTipText = Lang.PS("向前水平", "BsForwHorz");
		btnbsBsBackHorz.ToolTipText = Lang.PS("向后水平", "BsBackHorz");
		btnbsBsFrontTgnt.ToolTipText = Lang.PS("前切", "BsFrontTgnt");
		btnbsBsTailTgnt.ToolTipText = Lang.PS("尾切", "BsTailTgnt");
		btngblDtecDelay.ToolTipText = Lang.PS("信号延迟", "DtecDelay");
		toolStripLabel1.Text = Lang.PS("谱图参数", "ChartPara");
		lbTime.Text = Lang.PS("时间", "Time");
		lbTimeU.Text = Lang.PS("分", "m");
		lbSignal.Text = Lang.PS("信号", "signal");
		lbSignalU.Text = Lang.PS("到", "~");
		btnOpen.ToolTipText = Lang.PS("打开谱图文件", "OpenFile");
		btnSave.ToolTipText = Lang.PS("保存当前谱图文件", "Save");
		btnClose.ToolTipText = Lang.PS("关闭", "Close");
		toolStripButton6.ToolTipText = Lang.PS("文件列表", "FileList");
		btnPreview.ToolTipText = Lang.PS("打印预览", "Preview");
		btnPreviousZoom.ToolTipText = Lang.PS("上一视图", "LastView");
		btnNextZoom.ToolTipText = Lang.PS("下一视图", "NextView");
		btnUnzoom.ToolTipText = Lang.PS("原始视图", "NomalView");
		btnOverlayMode.ToolTipText = Lang.PS("谱图叠加打开", "MultiOpenMode");
		btnXYFull.Text = Lang.PS("满屏", "YFull");
		label32.Text = Lang.PS("满屏", "TimeFull");
		btnTimeFull.Text = Lang.PS("满屏", "XFull");
		btnOpen.Text = miFiOpen.Text;
		btnSave.Text = miFiSave.Text;
		btnClose.Text = miFiClose.Text;
		btnPreview.Text = miFiPreview.Text;
		btnPrtLink.Text = Lang.PS("样式设置", "Style Set");
		btnPreviousZoom.Text = miDisPreviousZoom.Text;
		btnNextZoom.Text = miDisNextZoom.Text;
		btnUnzoom.Text = miDisUnzoom.Text;
	}

	private void misstRestoreDftColumns_Click(object sender, EventArgs e)
	{
		method_49();
	}

	private void mislcRestoreDftColumns_Click(object sender, EventArgs e)
	{
	}

	private void btnExpress_Click(object sender, EventArgs e)
	{
		btnExpress.Checked = !btnExpress.Checked;
		if (btnExpress.Checked)
		{
			btnExpress.Image = imageList_1.Images[1];
		}
		else
		{
			btnExpress.Image = imageList_1.Images[0];
		}
		chromDataGrid.lbExpress.Visible = btnExpress.Checked && byte_0 != 0;
	}

	private void btngcuKAlpha_Click(object sender, EventArgs e)
	{
	}

	private void btnPrtLink_Click(object sender, EventArgs e)
	{
	}

	private void btnasNoneChrom_Click(object sender, EventArgs e)
	{
		if (HasChrom)
		{
			CurChrom.Process(InstruStyle.GC);
			chromDataGrid.tcChrom_SelectedIndexChanged(null, null);
		}
	}

	public void ChromForm_KeyDown(object sender, KeyEventArgs e)
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
			UpdateIntegRow();
		}
		if (e.KeyCode == Keys.Up)
		{
			dpgpcChrom_DoubleClick(null, null);
		}
		if (e.KeyCode == Keys.Down)
		{
			btnNextZoom_Click(null, null);
		}
		if (e.KeyCode == Keys.Space)
		{
			btnUnzoom_Click(null, null);
		}
	}

	private void UpdateIntegRow()
	{
		IntegRow integRow = new IntegRow
		{
			oprtStyle = IntegOprtStyle.DtecDelay,
			value = 0f
		};
		CurChrom.Process(InstruStyle.GC);
		if (CurChrom.PPara.UseUserZeroTime)
		{
			integRow = new IntegRow
			{
				oprtStyle = IntegOprtStyle.DtecDelay,
				value = 0f
			};
			double num = 10000.0;
			for (int i = 0; i < CurChrom.RltPeaks.Length; i++)
			{
				if ((double)CurChrom.RltPeaks[i].pkRT > CurChrom.PPara.ZeroTime - CurChrom.PPara.ZeroTimeLeft && (double)CurChrom.RltPeaks[i].pkRT < CurChrom.PPara.ZeroTime + CurChrom.PPara.ZeroTimeRight && num > Math.Abs((double)CurChrom.RltPeaks[i].pkRT - CurChrom.PPara.ZeroTime))
				{
					integRow.value = (float)((double)CurChrom.RltPeaks[i].pkRT - CurChrom.PPara.ZeroTime);
					CurChrom.integ.AppendRow(integRow);
					num = Math.Abs((double)CurChrom.RltPeaks[i].pkRT - CurChrom.PPara.ZeroTime);
				}
			}
		}
		chromDataGrid.tcChrom_SelectedIndexChanged(null, null);
		if (HasChrom)
		{
			mstSetChromForm.gvInteg.Refresh(AccStyle.Read, CurChrom.integ);
		}
		else
		{
			mstSetChromForm.gvInteg.Refresh(AccStyle.Clear, null);
		}
	}

	public void ApplyMethod()
	{
	}

	private void method_0(bool bool_10)
	{
		byte_0 = 1;
		chromDataGrid.lbExpress.Visible = btnExpress.Checked;
		splitContainer_SplitterMoved(null, null);
		chromDisplay_0.DrawL_begin();
		bool_5 = bool_10;
		chromDisplay_0.drawDynamicL = integRow_2.oprtStyle != IntegOprtStyle.PkVale;
		DisDpRefresh();
		chromDisplay_0.DrawL(PointToClient(Cursor.Position).X, 1);
	}

	private void method_1()
	{
		if (HasChrom)
		{
			CurChrom.CalcuResults(InstruStyle.GC);
			chromDataGrid.tcChrom_SelectedIndexChanged(null, null);
		}
	}

	private void cbrtaCanSetRs_Click(object sender, EventArgs e)
	{
	}

	private void cbasMatching_SelectionChangeCommitted(object sender, EventArgs e)
	{
		if (HasChrom && CurChrom.chromInfo.asChrom != "")
		{
			CurChrom.Process(InstruStyle.GC);
			chromDataGrid.tcChrom_SelectedIndexChanged(null, null);
		}
	}

	public void ChkOverlayMode()
	{
		if (!miFiOverlayMode.Checked)
		{
			miFiCloseAll_Click(null, null);
		}
	}

	private void method_3(int int_16, Signal signal_0)
	{
		chromatogram_1 = null;
		for (int i = 0; i < chromatogram_0.Length; i++)
		{
			if (chromatogram_0[i].signal == signal_0)
			{
				chromatogram_1 = chromatogram_0[i];
				SetExplainText();
				chromDataGrid.tcChrom_SelectedIndexChanged(null, null);
				break;
			}
		}
	}

	private void method_4(Signal signal_0)
	{
		DisDpRefresh();
		SetDisZoomButtonEnableState();
	}

	public void DisDpRefresh()
	{
		chromDisplay_0.displayPanel.Refresh();
		if (chromDisplay_0.stDisChain.Count != 0)
		{
			disLg_0 = chromDisplay_0.stDisChain.CurDisLg;
		}
	}

	private void method_7()
	{
		CurChrom.integ.AppendRow(manuDlg_0.GetNewIntegRow());
		method_28();
	}

	private void method_8()
	{
		chromDataGrid.lbExpress.Text = Lang.PS("感应取值", "Suggest Value");
		method_0(bool_10: true);
	}

	private void method_27(Chromatogram[] chromatogram_2, int curSignalNo)
	{
		chromatogram_1 = chromDisplay_0.LinkDisChroms(chromatogram_2, ref curSignalNo);
		chromatogram_1.signal.needReCalcuDis = true;
		SetExplainText();
	}

	public void LoadOptions()
	{
		chromDisplay_0.LinkOptions(options_0);
		chromDisplay_0.setShowGrid = options_0.grpShowGrid;
		ApplyMethod();
		SetDisZoomButtonEnableState();
	}

	private void method_28()
	{
		UpdateButtonCheckState(bool_10: false);
		UpdateIntegRow();
		CurChrom.integ.ResetUndoIndex();
	}

	private void EndManualDrawLine(bool bool_10)
	{
		if (bool_10)
		{
			byte_0 = 0;
		}
		chromDisplay_0.DrawL_end();
		chromDataGrid.lbExpress.Text = "";
		chromDataGrid.lbExpress.Visible = false;
		UpdateButtonCheckState(bool_10: false);
	}

	private void method_30()
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
				manuDlg_0.SetTitleValueU(mstSetChromForm.gvInteg, integRow_2, disMouseLgFmtX);
				manuDlg_0.Show(showSuggest: true);
				Class49.InsertIntoTable(Class49.string_9[2], Class49.user_0.u_name, "", "谱图处理:" + CurChrom.fullName, "谱图处理:" + integRow_2.oprtStyle.ToString() + " " + integRow_2.value);
				return;
			case IntegOprtStyle.PkThreshold:
				integRow_2.value = CurChrom.integ.Threshold;
				manuDlg_0.SetTitleValueU(mstSetChromForm.gvInteg, integRow_2, disMouseLgFmtX);
				manuDlg_0.Show(showSuggest: true);
				Class49.InsertIntoTable(Class49.string_9[2], Class49.user_0.u_name, "", "谱图处理:" + CurChrom.fullName, "谱图处理:" + integRow_2.oprtStyle.ToString() + " " + integRow_2.value);
				return;
			case IntegOprtStyle.PkHalfWidth:
				integRow_2.value = CurChrom.integ.PeakWidth;
				manuDlg_0.SetTitleValueU(mstSetChromForm.gvInteg, integRow_2, disMouseLgFmtX);
				manuDlg_0.Show(showSuggest: true);
				Class49.InsertIntoTable(Class49.string_9[2], Class49.user_0.u_name, "", "谱图处理:" + CurChrom.fullName, "谱图处理:" + integRow_2.oprtStyle.ToString() + " " + integRow_2.value);
				return;
			case IntegOprtStyle.PkArea:
				integRow_2.value = 0f;
				manuDlg_0.SetTitleValueU(mstSetChromForm.gvInteg, integRow_2, disMouseLgFmtX);
				manuDlg_0.Show(showSuggest: false);
				Class49.InsertIntoTable(Class49.string_9[2], Class49.user_0.u_name, "", "谱图处理:" + CurChrom.fullName, "谱图处理:" + integRow_2.oprtStyle.ToString() + " " + disMouseLgFmtX);
				return;
			default:
				switch (oprtStyle)
				{
				case IntegOprtStyle.BsTgnt:
					integRow_2.value = CurChrom.integ.IniTgntAreaF;
					integRow_2.value2 = CurChrom.integ.IniTgntSlopeF;
					integRow_2.value3 = CurChrom.integ.IniTgntLfF;
					integRow_2.value4 = CurChrom.integ.IniTgntRtF;
					manuDlg_0.SetTitleValueU(mstSetChromForm.gvInteg, integRow_2, disMouseLgFmtX);
					manuDlg_0.Show(showSuggest: false);
					Class49.InsertIntoTable(Class49.string_9[2], Class49.user_0.u_name, "", "谱图处理:" + CurChrom.fullName, "谱图处理:" + integRow_2.oprtStyle.ToString() + " " + integRow_2.value);
					return;
				case IntegOprtStyle.BsVtV:
					integRow_2.value = CurChrom.integ.IniVtVSlope;
					manuDlg_0.SetTitleValueU(mstSetChromForm.gvInteg, integRow_2, disMouseLgFmtX);
					manuDlg_0.Show(showSuggest: true);
					Class49.InsertIntoTable(Class49.string_9[2], Class49.user_0.u_name, "", "谱图处理:" + CurChrom.fullName, "谱图处理:" + integRow_2.oprtStyle.ToString() + " " + integRow_2.value);
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
			if (integRow_2.oprtStyle != IntegOprtStyle.SolventPeak)
			{
				CurChrom.integ.AppendRow(integRow_2);
			}
			Class49.InsertIntoTable(Class49.string_9[2], Class49.user_0.u_name, "", "谱图处理:" + CurChrom.fullName, "谱图处理:" + integRow_2.oprtStyle.ToString() + "开始时间:" + integRow_2.timeA + "结束时间:" + integRow_2.timeB);
			break;
		}
		method_28();
	}

	private void miclLine_Click(object sender, EventArgs e)
	{
		UpdateButtonCheckState(bool_10: false);
		if (HasChrom && !CheckAuthority(bool_10: true))
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
		UpdateButtonCheckState(bool_10: false);
		if (!HasChrom || CheckAuthority(bool_10: true))
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
		SetDisZoomButtonEnableState();
	}

	private void dpgpcChrom_DoubleClick(object sender, EventArgs e)
	{
		chromDisplay_0.stDisChain.DynNo--;
		DisDpRefresh();
		SetDisZoomButtonEnableState();
	}

	private void btnUnzoom_Click(object sender, EventArgs e)
	{
		if (chromDisplay_0.SetFullDisLg(ref disLg_0, CurSignal, second: true))
		{
			DisDpRefresh();
			SetDisZoomButtonEnableState();
		}
	}

	private void btnClose_Click(object sender, EventArgs e)
	{
		lclGvRltsGnl.EndEdit();
		mstSetChromForm.Clear();
		switch (chromatogram_0.Length)
		{
		case 0:
			break;
		case 1:
			miFiCloseAll_Click(null, null);
			break;
		default:
			miFiCloseAll_Click(null, null);
			break;
		}
	}

	public void miFiCloseAll_Click(object sender, EventArgs e)
	{
		lclGvRltsGnl.EndEdit();
		Array.Resize(ref chromatogram_0, 0);
		chromDisplay_0.ClearDisSignals();
		chromatogram_1 = null;
		SetExplainText();
		SetDisZoomButtonEnableState();
		chromDataGrid.tcChrom_SelectedIndexChanged(null, null);
	}

	private void btnOpen_Click(object sender, EventArgs e)
	{
		if (chromatogram_0.Length >= 12)
		{
			MessageBox.Show(Lang.PS("最多同时显示", "At most ") + 12 + Lang.PS("张谱图！", "chromatograms"));
			return;
		}
		ofdChrom.InitialDirectory = strSdaDataFileDir;
		if (ofdChrom.ShowDialog(this) != DialogResult.OK)
		{
			return;
		}
		if (chromatogram_0.Length == 0)
		{
			chromDisplay_0.ClearDisSignals();
		}
		string[] fileNames = ofdChrom.FileNames;
		for (int i = 0; i < fileNames.Length; i++)
		{
			string text = fileNames[i].ToLower();
			string directoryName = Path.GetDirectoryName(text);
			sysParam.strSdaDataFileDir = Path.GetDirectoryName(text);
			sysParam.SaveParam();
			bool flag = false;
			for (int j = 0; j < chromatogram_0.Length; j++)
			{
				if (chromatogram_0[j].fullName == text)
				{
					MessageBox.Show(Lang.PS("已打开谱图!", "Chrom. opened!") + "\n" + Path.GetFileName(text));
					flag = true;
					break;
				}
			}
			if (!flag)
			{
				SetExplainText();
				ofdChrom.InitialDirectory = Path.GetDirectoryName(strSdaDataFileDir);
				bool flag2 = false;
				OpenChrom(text, sampling: false, ofdChrom.Checked);
				if (cdlMgr.formMain != null)
				{
					cdlMgr.formMain.StrAlarmFile = text;
				}
			}
		}
		if (fileNames.Length != 0)
		{
			string path = fileNames[0];
			string directoryName2 = Path.GetDirectoryName(path);
			strSdaDataFileDir = directoryName2;
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
			if (btnOverlayMode.Checked)
			{
				btnOverlayMode.Image = imageList_1.Images[3];
			}
			else
			{
				btnOverlayMode.Image = imageList_1.Images[2];
			}
		}
	}

	private void btnPreview_Click(object sender, EventArgs e)
	{
		try
		{
			if (CurChrom == null)
			{
				return;
			}
			CurChrom.disLg = chromDisplay_0.stDisChain.CurDisLg;
			if (mstSetChromForm.radioButton7.Checked)
			{
				PrintToFile("", Open: true);
			}
			else if (mstSetChromForm.radioButton4.Checked)
			{
				string startupPath = System.Windows.Forms.Application.StartupPath;
				Control control = dpgnlChrom;
				Bitmap bitmap = new Bitmap(control.Width, control.Height);
				Graphics graphics = Graphics.FromImage(bitmap);
				System.Drawing.Rectangle bounds = control.Bounds;
				bounds.X = 0;
				bounds.Y = 0;
				control.DrawToBitmap(bitmap, bounds);
				bitmap.Save(System.Windows.Forms.Application.StartupPath + "\\a2.Emf", ImageFormat.Emf);
				Chromatogram[] array = chromatogram_0;
				if (array.Length != 0)
				{
					array[0].signal.yMaxValue = 1000f;
				}
				Size size = new Size(600, 300);
				int setChromNo = 0;
				Bitmap image = new Bitmap(size.Width, size.Height);
				RectangleF rectangleF = new RectangleF(0f, 0f, size.Width, size.Height);
				string fileName = System.Windows.Forms.Application.StartupPath + "\\a1.Emf";
				Graphics graphics2 = Graphics.FromImage(image);
				Metafile metafile = new Metafile(fileName, graphics2.GetHdc());
				Graphics graphics3 = Graphics.FromImage(metafile);
				ChromDisplay chromDisplay = new ChromDisplay(WinStyle.Chromatogram, null);
				chromDisplay.LinkDisChroms(array, ref setChromNo);
				chromDisplay.showMouseLgValue = false;
				chromDisplay.showProgTemp = false;
				chromDisplay.ShowBgChrom = true;
				chromDisplay.setShowGrid = false;
				chromDisplay.rcPage = rectangleF;
				chromDisplay.dskRC = rectangleF;
				chromDisplay_0.DrawBmp(graphics3, erase: false);
				DrawControl(control, graphics3);
				graphics3.Dispose();
				metafile.Dispose();
			}
			else
			{
				XrAnysReport.OpenReportForm(CurChrom, m_strChormFileName);
			}
			Class49.InsertIntoTable(Class49.string_9[3], Class49.user_0.u_name, "", "谱图打印", "谱图打印:" + CurChrom.fullName);
		}
		catch (Exception)
		{
		}
	}

	public void setForm(XWPFRun run, int changeLine, string content, bool isUnderline, string color)
	{
		if (isUnderline)
		{
			run.SetUnderline(UnderlinePatterns.Words);
		}
		run.SetText(content);
		if (changeLine != 0)
		{
			for (int i = 0; i < changeLine; i++)
			{
				run.AddBreak();
			}
		}
		run.SetColor(color);
		run.SetTextPosition(5);
		run.FontSize = 10;
		run.FontFamily = "宋体";
	}

	public XWPFParagraph SetCellText(XWPFDocument doc, XWPFTable table, string setText)
	{
		CT_P prgrph = new CT_P();
		XWPFParagraph xWPFParagraph = new XWPFParagraph(prgrph, table.Body);
		xWPFParagraph.Alignment = ParagraphAlignment.CENTER;
		xWPFParagraph.VerticalAlignment = TextAlignment.CENTER;
		XWPFRun xWPFRun = xWPFParagraph.CreateRun();
		xWPFRun.SetText(setText);
		xWPFRun.FontSize = 12;
		xWPFRun.FontFamily = "华文楷体";
		return xWPFParagraph;
	}

	public void CreateDocStatistics(string docTemplatePath)
	{
		bool flag = true;
		PrintPara pPara = chromatogram_0[0].PPara;
		Control control = dpgnlChrom;
		Bitmap bitmap = new Bitmap(control.Width, control.Height);
		Graphics graphics = Graphics.FromImage(bitmap);
		System.Drawing.Rectangle bounds = control.Bounds;
		bounds.X = 0;
		bounds.Y = 0;
		control.DrawToBitmap(bitmap, bounds);
		bitmap.Save(System.Windows.Forms.Application.StartupPath + "\\a1.Emf", ImageFormat.Emf);
		string text = pPara.Title + DateTime.Now.ToString("yyyyMMdd-HHmmss") + ".docx";
		FileStream fileStream = File.OpenWrite(text);
		fileStream.Position = 0L;
		XWPFDocument xWPFDocument = new XWPFDocument();
		XWPFParagraph xWPFParagraph = xWPFDocument.CreateParagraph();
		xWPFParagraph.Alignment = ParagraphAlignment.CENTER;
		XWPFRun xWPFRun = xWPFParagraph.CreateRun();
		xWPFRun.FontSize = 16;
		xWPFRun.IsBold = true;
		xWPFRun.AppendText(pPara.Title);
		XWPFParagraph xWPFParagraph2 = xWPFDocument.CreateParagraph();
		xWPFParagraph2.Alignment = ParagraphAlignment.LEFT;
		XWPFRun xWPFRun2 = xWPFParagraph2.CreateRun();
		xWPFRun2.AddCarriageReturn();
		xWPFRun2.SetText(pPara.PrintTitleTop);
		xWPFRun2.AddCarriageReturn();
		if (CurChrom.PPara.bPTime)
		{
			xWPFRun2.AppendText("打印时间:" + DateTime.Now.ToString());
		}
		if (CurChrom.PPara.bJtime)
		{
			xWPFRun2.AppendText("进样时间:" + chromatogram_1.injAnalysis.dtAcquire.ToString());
		}
		if (CurChrom.PPara.bfname)
		{
			xWPFRun2.AppendText("打开的谱图文件:" + m_strChormFileName);
			xWPFRun2.AddCarriageReturn();
		}
		int num = 5715000;
		int num2 = 2857500;
		string text2 = System.Windows.Forms.Application.StartupPath + "\\a1.Emf";
		FileStream fileStream2 = new FileStream(text2, FileMode.Open, FileAccess.Read);
		xWPFRun2.AddPicture(fileStream2, 2, text2, num, num2);
		fileStream2.Close();
		xWPFRun2.AddCarriageReturn();
		XWPFTable[] array = new XWPFTable[chromatogram_0[0].caliGnl.cmpds.Length + 1];
		XWPFParagraph xWPFParagraph3 = xWPFDocument.CreateParagraph();
		xWPFParagraph3.CreateRun().AddCarriageReturn();
		xWPFParagraph3.CreateRun().AddCarriageReturn();
		ClassAutoCalibraPeak[] array2 = new ClassAutoCalibraPeak[chromatogram_0.Length];
		float[] array3 = new float[chromatogram_0.Length];
		float[] array4 = new float[chromatogram_0.Length];
		float[] array5 = new float[chromatogram_0.Length];
		float[] array6 = new float[chromatogram_0.Length];
		float[] array7 = new float[chromatogram_0.Length];
		float[] array8 = new float[chromatogram_0.Length];
		string[] array9 = new string[chromatogram_0.Length];
		for (int i = 0; i < chromatogram_0[0].caliGnl.cmpds.Length; i++)
		{
			array[i + 1] = xWPFDocument.CreateTable(2, 8);
			array[i + 1].GetRow(1).GetCell(0).SetText("序号");
			array[i + 1].GetRow(1).GetCell(1).SetText("名称");
			array[i + 1].GetRow(1).GetCell(2).SetText("保留时间(min)");
			array[i + 1].GetRow(1).GetCell(3).SetText("峰面积(" + chromatogram_0[0].signal.detecter_unit + "s)");
			array[i + 1].GetRow(1).GetCell(4).SetText("相对峰面积(%)");
			array[i + 1].GetRow(1).GetCell(5).SetText("峰高(" + chromatogram_0[0].signal.detecter_unit + ")");
			array[i + 1].GetRow(1).GetCell(6).SetText("相对峰高(%)");
			array[i + 1].GetRow(1).GetCell(7).SetText("样品量(" + chromatogram_0[0].caliGnl.caliOption.cmpdUnit + ")");
			array[i + 1].SetColumnWidth(0, 76800uL);
			array[i + 1].SetColumnWidth(1, 7680uL);
			array[i + 1].SetColumnWidth(2, 7680uL);
			XWPFParagraph xWPFParagraph4 = xWPFDocument.CreateParagraph();
			XWPFRun xWPFRun3 = xWPFParagraph4.CreateRun();
			xWPFRun3.AddCarriageReturn();
			for (int j = 0; j < chromatogram_0.Length; j++)
			{
				array[i + 1].CreateRow();
				array2[j] = new ClassAutoCalibraPeak();
				array2[j].peak = new Peak[0];
				array2[j].peak = chromatogram_0[j].GetPeakAllCompound();
				array3[j] = array2[j].peak[i].pkRT;
				array4[j] = array2[j].peak[i].area;
				array5[j] = array2[j].peak[i].areaPer;
				array6[j] = array2[j].peak[i].height;
				array7[j] = array2[j].peak[i].heightPer;
				if (array2[j].peak[i].compound.eFunc.curveFit == CurveFit.Free)
				{
					array8[j] = array2[j].peak[i].amountPer * 100f;
				}
				else
				{
					array8[j] = array2[j].peak[i].amount;
				}
				array9[j] = chromatogram_0[j].fName;
				array[i + 1].GetRow(j + 2).GetCell(0).SetText((j + 1).ToString());
				array[i + 1].GetRow(j + 2).GetCell(1).SetText(array9[j]);
				array[i + 1].GetRow(j + 2).GetCell(2).SetText(array3[j].ToString("0.000"));
				array[i + 1].GetRow(j + 2).GetCell(3).SetText(array4[j].ToString("0.0000"));
				array[i + 1].GetRow(j + 2).GetCell(4).SetText((array5[j] * 100f).ToString("0.0000"));
				array[i + 1].GetRow(j + 2).GetCell(5).SetText(array6[j].ToString("0.000"));
				array[i + 1].GetRow(j + 2).GetCell(6).SetText((array7[j] * 100f).ToString("0.0000"));
				array[i + 1].GetRow(j + 2).GetCell(7).SetText(array8[j].ToString("0.0000"));
			}
			array[i + 1].CreateRow();
			array[i + 1].CreateRow();
			array[i + 1].CreateRow();
			float num3 = 0f;
			array[i + 1].GetRow(chromatogram_0.Length + 2).GetCell(1).SetText("总和");
			array[i + 1].GetRow(chromatogram_0.Length + 3).GetCell(1).SetText("平均值");
			array[i + 1].GetRow(chromatogram_0.Length + 4).GetCell(1).SetText("相对标准偏差");
			for (int k = 0; k < array3.Length; k++)
			{
				num3 += array3[k];
			}
			array[i + 1].GetRow(chromatogram_0.Length + 2).GetCell(2).SetText(num3.ToString("0.000"));
			array[i + 1].GetRow(chromatogram_0.Length + 3).GetCell(2).SetText((num3 / (float)array3.Length).ToString("0.000"));
			array[i + 1].GetRow(chromatogram_0.Length + 4).GetCell(2).SetText(Program.RSDCalculate(num3 / (float)array3.Length, array3, array3.Length).ToString("0.000"));
			num3 = 0f;
			for (int l = 0; l < array4.Length; l++)
			{
				num3 += array4[l];
			}
			array[i + 1].GetRow(chromatogram_0.Length + 2).GetCell(3).SetText(num3.ToString("0.000"));
			array[i + 1].GetRow(chromatogram_0.Length + 3).GetCell(3).SetText((num3 / (float)array4.Length).ToString("0.000"));
			array[i + 1].GetRow(chromatogram_0.Length + 4).GetCell(3).SetText(Program.RSDCalculate(num3 / (float)array4.Length, array4, array4.Length).ToString("0.000"));
			num3 = 0f;
			for (int m = 0; m < array5.Length; m++)
			{
				num3 += array5[m];
			}
			array[i + 1].GetRow(chromatogram_0.Length + 2).GetCell(4).SetText(num3.ToString("0.000"));
			array[i + 1].GetRow(chromatogram_0.Length + 3).GetCell(4).SetText((num3 / (float)array5.Length).ToString("0.000"));
			array[i + 1].GetRow(chromatogram_0.Length + 4).GetCell(4).SetText(Program.RSDCalculate(num3 / (float)array5.Length, array5, array5.Length).ToString("0.000"));
			num3 = 0f;
			for (int n = 0; n < array6.Length; n++)
			{
				num3 += array6[n];
			}
			array[i + 1].GetRow(chromatogram_0.Length + 2).GetCell(5).SetText(num3.ToString("0.000"));
			array[i + 1].GetRow(chromatogram_0.Length + 3).GetCell(5).SetText((num3 / (float)array6.Length).ToString("0.000"));
			array[i + 1].GetRow(chromatogram_0.Length + 4).GetCell(5).SetText(Program.RSDCalculate(num3 / (float)array6.Length, array6, array6.Length).ToString("0.000"));
			num3 = 0f;
			for (int num4 = 0; num4 < array7.Length; num4++)
			{
				num3 += array7[num4];
			}
			array[i + 1].GetRow(chromatogram_0.Length + 2).GetCell(6).SetText(num3.ToString("0.000"));
			array[i + 1].GetRow(chromatogram_0.Length + 3).GetCell(6).SetText((num3 / (float)array7.Length).ToString("0.000"));
			array[i + 1].GetRow(chromatogram_0.Length + 4).GetCell(6).SetText(Program.RSDCalculate(num3 / (float)array7.Length, array7, array7.Length).ToString("0.000"));
			num3 = 0f;
			for (int num5 = 0; num5 < array8.Length; num5++)
			{
				num3 += array8[num5];
			}
			array[i + 1].GetRow(chromatogram_0.Length + 2).GetCell(7).SetText(num3.ToString("0.000"));
			array[i + 1].GetRow(chromatogram_0.Length + 3).GetCell(7).SetText((num3 / (float)array8.Length).ToString("0.000"));
			array[i + 1].GetRow(chromatogram_0.Length + 4).GetCell(7).SetText(Program.RSDCalculate(num3 / (float)array8.Length, array8, array8.Length).ToString("0.000"));
			array[i + 1].GetRow(0).GetCell(0).SetText("组份:" + chromatogram_0[0].caliGnl.cmpds[i].cmpdInfo.name);
			array[i + 1].GetRow(0).MergeCells(0, 7);
			XWPFParagraph xWPFParagraph5 = array[i + 1].GetRow(1).GetCell(5).AddParagraph();
			XWPFRun xWPFRun4 = xWPFParagraph5.CreateRun();
		}
		CT_TcPr cT_TcPr = array[1].GetRow(1).GetCell(0).GetCTTc()
			.AddNewTcPr();
		cT_TcPr.tcW = new CT_TblWidth();
		for (int num6 = 1; num6 < array.Length; num6++)
		{
			for (int num7 = 0; num7 < 8; num7++)
			{
				cT_TcPr = array[num6].GetRow(1).GetCell(num7).GetCTTc()
					.AddNewTcPr();
				cT_TcPr.tcW = new CT_TblWidth();
				if (num7 == 1)
				{
					cT_TcPr.tcW.w = "1700";
				}
				else
				{
					cT_TcPr.tcW.w = "950";
				}
				cT_TcPr.tcW.type = ST_TblWidth.dxa;
			}
		}
		xWPFDocument.Write(fileStream);
		Process.Start(text);
		if (CurChrom.PPara.bPeakFx)
		{
			Thread.Sleep(1000);
			if (CaliGnlUserCtrl.caliGnlUserCtrl == null)
			{
				CaliGnlUserCtrl.caliGnlUserCtrl = new CaliGnlUserCtrl();
			}
			CaliGnlUserCtrl.caliGnlUserCtrl.LoadFile(CurChrom.caliGnl);
			CaliGnlUserCtrl.caliGnlUserCtrl.CreateDocStatistics("");
		}
	}

	public void CreateDoc(string docTemplatePath)
	{
		if (!File.Exists(docTemplatePath))
		{
			return;
		}
		string text = "D:\\UploadFiles\\PdfFiles\\";
		string text2 = Guid.NewGuid().ToString() + ".docx";
		FileStream fileStream = File.OpenWrite(text2);
		fileStream.Position = 0L;
		XWPFDocument xWPFDocument = new XWPFDocument();
		PrintPara pPara = chromatogram_0[0].PPara;
		XWPFParagraph xWPFParagraph = xWPFDocument.CreateParagraph();
		xWPFParagraph.Alignment = ParagraphAlignment.CENTER;
		XWPFRun xWPFRun = xWPFParagraph.CreateRun();
		xWPFRun.FontSize = 16;
		xWPFRun.IsBold = true;
		xWPFRun.AppendText(pPara.Title);
		XWPFParagraph xWPFParagraph2 = xWPFDocument.CreateParagraph();
		xWPFParagraph2.Alignment = ParagraphAlignment.LEFT;
		XWPFRun xWPFRun2 = xWPFParagraph2.CreateRun();
		xWPFRun2.AddCarriageReturn();
		xWPFRun2.SetText(pPara.PrintTitleTop);
		xWPFRun2.AddCarriageReturn();
		if (CurChrom.PPara.bPTime)
		{
			xWPFRun2.AppendText("打印时间:" + DateTime.Now.ToString());
		}
		if (CurChrom.PPara.bJtime)
		{
			xWPFRun2.AppendText("进样时间:" + chromatogram_1.injAnalysis.dtAcquire.ToString());
		}
		if (CurChrom.PPara.bfname)
		{
			xWPFRun2.AppendText("打开的谱图文件:" + m_strChormFileName);
			xWPFRun2.AddCarriageReturn();
		}
		int num = 5715000;
		int num2 = 2857500;
		string text3 = System.Windows.Forms.Application.StartupPath + "\\a1.Emf";
		FileStream pictureData = new FileStream(text3, FileMode.Open, FileAccess.Read);
		xWPFRun2.AddPicture(pictureData, 2, text3, num, num2);
		foreach (XWPFParagraph paragraph in xWPFDocument.Paragraphs)
		{
		}
		foreach (XWPFTable table in xWPFDocument.Tables)
		{
			foreach (XWPFTableRow row in table.Rows)
			{
				foreach (XWPFTableCell tableCell in row.GetTableCells())
				{
					foreach (XWPFParagraph paragraph2 in tableCell.Paragraphs)
					{
					}
				}
			}
		}
		xWPFDocument.Write(fileStream);
		Process.Start(text2);
	}

	private static void ReplaceKey<T>(T etity, XWPFParagraph para)
	{
	}

	public void PrintByDot(string FilePath)
	{
		if (!HasChrom)
		{
			return;
		}
		string startupPath = System.Windows.Forms.Application.StartupPath;
		WordHelper wordHelper = new WordHelper();
		wordHelper.CreateNewWordDocument(startupPath + "//PrintDoc.dot");
		wordHelper.Replace("报告标题", mstSetChromForm.PrintMethod.Title);
		wordHelper.Replace("报告头", mstSetChromForm.PrintMethod.PrintTitleTop);
		wordHelper.Replace("报告尾", mstSetChromForm.PrintMethod.PrintTitleBotom);
		if (CurChrom.PPara.bPTime)
		{
			wordHelper.Replace("打印时间", "打印时间:" + DateTime.Now.ToString());
		}
		if (CurChrom.PPara.bJtime)
		{
			wordHelper.Replace("进样时间", "进样时间:" + chromatogram_1.injAnalysis.dtAcquire.ToString());
		}
		if (CurChrom.PPara.bfname)
		{
		}
		dpgnlChrom.Dock = DockStyle.None;
		dpgnlChrom.Refresh();
		Control control = dpgnlChrom;
		Bitmap bitmap = new Bitmap(control.Width, control.Height);
		Graphics graphics = Graphics.FromImage(bitmap);
		System.Drawing.Rectangle bounds = control.Bounds;
		bounds.X = 0;
		bounds.Y = 0;
		control.DrawToBitmap(bitmap, bounds);
		dpgnlChrom.Dock = DockStyle.Fill;
		if (CurChrom.PPara.bPicBound)
		{
			graphics.SmoothingMode = SmoothingMode.HighQuality;
			graphics.CompositingQuality = CompositingQuality.HighQuality;
			graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
			graphics.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
			graphics.DrawRectangle(new Pen(Color.Black, 2f), 0, 0, bitmap.Width - 2, bitmap.Height - 2);
		}
		if (CurChrom.PPara.bpic)
		{
			wordHelper.ReplacePic("谱图", bitmap);
		}
		chromDisplay_0.Draw(graphics, erase: false);
		Peak[] rltPeaks = chromatogram_0[0].RltPeaks;
		if (CurChrom.PPara.bRdata)
		{
			System.Data.DataTable dataTable = new System.Data.DataTable();
			int num = rltPeaks.Length;
			int num2 = 0;
			if (CurChrom.PPara.bIndex)
			{
				dataTable.Columns.Add("序号");
				for (int i = 0; i < num; i++)
				{
					dataTable.Rows.Add(i + 1);
				}
				dataTable.Rows.Add("合计");
				num2++;
			}
			if (CurChrom.PPara.bPeakName)
			{
				dataTable.Columns.Add("组份名称");
				for (int j = 0; j < num; j++)
				{
					dataTable.Rows[j][num2] = rltPeaks[j].name;
				}
				num2++;
			}
			if (CurChrom.PPara.bPeakMaxTime)
			{
				dataTable.Columns.Add("保留时间");
				for (int k = 0; k < num; k++)
				{
					dataTable.Rows[k][num2] = rltPeaks[k].pkRT.ToString("0.0000");
				}
				num2++;
			}
			if (CurChrom.PPara.bPeakArea)
			{
				dataTable.Columns.Add("峰面积");
				for (int l = 0; l < num; l++)
				{
					dataTable.Rows[l][num2] = rltPeaks[l].area.ToString("0.0000");
				}
				dataTable.Rows[num][num2] = CurChrom.whlArea.ToString("0.0000");
				num2++;
			}
			if (CurChrom.PPara.bPeakheight)
			{
				dataTable.Columns.Add("峰高");
				for (int m = 0; m < num; m++)
				{
					dataTable.Rows[m][num2] = rltPeaks[m].height.ToString("0.0000");
				}
				dataTable.Rows[num][num2] = CurChrom.whlHeight.ToString("0.0000");
				num2++;
			}
			if (CurChrom.PPara.bPeakHalfheight)
			{
				dataTable.Columns.Add("半峰宽");
				for (int n = 0; n < num; n++)
				{
					dataTable.Rows[n][num2] = rltPeaks[n].WO5.ToString("0.0000");
				}
				num2++;
			}
			if (CurChrom.PPara.bPeakAmont)
			{
				dataTable.Columns.Add("浓度");
				for (int num3 = 0; num3 < num; num3++)
				{
					if (rltPeaks[num3].amount != -1f)
					{
						dataTable.Rows[num3][num2] = rltPeaks[num3].amount.ToString("0.0000");
					}
					else
					{
						dataTable.Rows[num3][num2] = "";
					}
				}
				dataTable.Rows[num][num2] = CurChrom.whlAmount.ToString("0.0000");
				num2++;
			}
			if (CurChrom.PPara.bPeakFx)
			{
				dataTable.Columns.Add("工作曲线方程");
				for (int num4 = 0; num4 < num; num4++)
				{
					if (CurChrom.chromInfo.cclCalcu == CalcuStyle.ESTD)
					{
						if (rltPeaks[num4].compound != null)
						{
							dataTable.Rows[num4][num2] = rltPeaks[num4].compound.eFunc.GetEquationStr();
						}
						else
						{
							dataTable.Rows[num4][num2] = "";
						}
					}
					else if (CurChrom.chromInfo.cclCalcu == CalcuStyle.ISTD)
					{
						if (rltPeaks[num4].compound != null)
						{
							dataTable.Rows[num4][num2] = rltPeaks[num4].compound.iFunc.GetEquationStr();
						}
						else
						{
							dataTable.Rows[num4][num2] = "";
						}
					}
				}
				num2++;
			}
			if (CurChrom.PPara.bPeakLPara)
			{
				dataTable.Columns.Add("容量因子");
				for (int num5 = 0; num5 < num; num5++)
				{
					dataTable.Rows[num5][num2] = rltPeaks[num5].Capacity.ToString("0.0000");
				}
				num2++;
			}
			if (CurChrom.PPara.bPeakLV)
			{
				dataTable.Columns.Add("峰分离度");
				for (int num6 = 0; num6 < num; num6++)
				{
					dataTable.Rows[num6][num2] = rltPeaks[num6].Resolution_EP.ToString("0.0000");
				}
				num2++;
			}
			if (CurChrom.PPara.bPeakOtherPara)
			{
				dataTable.Columns.Add("相关系数");
				for (int num7 = 0; num7 < num; num7++)
				{
					if (CurChrom.chromInfo.cclCalcu == CalcuStyle.ESTD)
					{
						if (rltPeaks[num7].compound != null)
						{
							dataTable.Rows[num7][num2] = rltPeaks[num7].compound.eFunc.corrFactor.ToString("0.00000");
						}
						else
						{
							dataTable.Rows[num7][num2] = "";
						}
					}
					else if (CurChrom.chromInfo.cclCalcu == CalcuStyle.ISTD)
					{
						if (rltPeaks[num7].compound != null)
						{
							dataTable.Rows[num7][num2] = rltPeaks[num7].compound.iFunc.corrFactor.ToString("0.00000");
						}
						else
						{
							dataTable.Rows[num7][num2] = "";
						}
					}
				}
				num2++;
			}
			if (CurChrom.PPara.bPeakPara)
			{
				dataTable.Columns.Add("校正因子");
				for (int num8 = 0; num8 < num; num8++)
				{
					if (rltPeaks[num8].compound != null)
					{
						dataTable.Rows[num8][num2] = rltPeaks[num8].compound.cmpdInfo.freeRespFactor.ToString("0.0000");
					}
					else
					{
						dataTable.Rows[num8][num2] = "1";
					}
				}
				num2++;
			}
			if (CurChrom.PPara.bPeaktailPara)
			{
				dataTable.Columns.Add("拖尾因子");
				for (int num9 = 0; num9 < num; num9++)
				{
					dataTable.Rows[num9][num2] = rltPeaks[num9].SymmetryTailing.ToString("0.0000");
				}
				num2++;
			}
			if (CurChrom.PPara.bPeakTBPara)
			{
				dataTable.Columns.Add("理论塔板数");
				for (int num10 = 0; num10 < num; num10++)
				{
					dataTable.Rows[num10][num2] = ((int)rltPeaks[num10].Efficiency_EP).ToString();
				}
				num2++;
			}
			if (CurChrom.PPara.bPeakUTBPara)
			{
				dataTable.Columns.Add("有效塔板数");
				for (int num11 = 0; num11 < num; num11++)
				{
					dataTable.Rows[num11][num2] = ((int)rltPeaks[num11].Eff_Column_EP).ToString();
				}
				num2++;
			}
			if (CurChrom.PPara.bPeakV)
			{
				dataTable.Columns.Add("峰标志");
				for (int num12 = 0; num12 < num; num12++)
				{
					dataTable.Rows[num12][num2] = rltPeaks[num12].pkStyle.ToString();
				}
				num2++;
			}
			if (CurChrom.PPara.bPeakheightPer)
			{
				dataTable.Columns.Add("峰高百分比");
				for (int num13 = 0; num13 < num; num13++)
				{
					dataTable.Rows[num13][num2] = (rltPeaks[num13].heightPer * 100f).ToString("0.0000") + "%";
				}
				if (CurChrom.whlHeightPer != -1f)
				{
					dataTable.Rows[num][num2] = "100%";
				}
				num2++;
			}
			if (CurChrom.PPara.bPeakAreaPer)
			{
				dataTable.Columns.Add("面积百分比");
				for (int num14 = 0; num14 < num; num14++)
				{
					dataTable.Rows[num14][num2] = (rltPeaks[num14].areaPer * 100f).ToString("0.0000");
				}
				if (CurChrom.whlAreaPer != -1f)
				{
					dataTable.Rows[num][num2] = "100";
				}
				num2++;
			}
			if (CurChrom.PPara.bPeakAmontPer)
			{
				dataTable.Columns.Add("浓度百分比");
				for (int num15 = 0; num15 < num; num15++)
				{
					if (rltPeaks[num15].amountPer != -1f)
					{
						dataTable.Rows[num15][num2] = (rltPeaks[num15].amountPer * 100f).ToString("0.0000");
					}
					else
					{
						dataTable.Rows[num15][num2] = "";
					}
				}
				if (CurChrom.whlAmountPer != -1f)
				{
					dataTable.Rows[num][num2] = CurChrom.whlAmountPer.ToString("0.0000");
				}
				num2++;
			}
			wordHelper.CreateTable("表格", dataTable);
		}
		if (CurChrom.PPara.bPeakFx)
		{
			CaliGnlForm caliGnlForm = new CaliGnlForm();
			caliGnlForm.Visible = false;
			caliGnlForm.Show();
			caliGnlForm.Close();
		}
		string text = startupPath + "//PrintFlie.doc";
		string text2 = "1111";
		string[] array = text2.Split('.');
		text = text2.Replace("." + array[array.Length - 1], ".doc");
		File.Delete(text);
		wordHelper.SaveAs(text);
		wordHelper.Close();
		new Process();
		switch (mstSetChromForm.PrintMethod.PPreView)
		{
		case PrintPreview.写字板:
		case PrintPreview.Word:
		case PrintPreview.程序自带:
			Process.Start(text);
			break;
		}
	}

	public void PrintToWordSprieStatic(string FilePath, bool Open)
	{
	}

	public void PrintToWordComStatic(string FilePath, bool Open)
	{
		object FileName = Environment.CurrentDirectory + "\\MyWord_Print.doc";
		Microsoft.Office.Interop.Word.Application application = new ApplicationClass();
		application.Visible = true;
		if (File.Exists((string)FileName))
		{
			File.Delete((string)FileName);
		}
		object Template = Missing.Value;
		Microsoft.Office.Interop.Word.Document document = application.Documents.Add(ref Template, ref Template, ref Template, ref Template);
		document.PageSetup.PaperSize = WdPaperSize.wdPaperA4;
		document.PageSetup.Orientation = WdOrientation.wdOrientPortrait;
		document.PageSetup.TopMargin = 57f;
		document.PageSetup.BottomMargin = 57f;
		document.PageSetup.LeftMargin = 57f;
		document.PageSetup.RightMargin = 57f;
		document.PageSetup.HeaderDistance = 30f;
		application.ActiveWindow.View.Type = WdViewType.wdNormalView;
		application.ActiveWindow.View.SeekView = WdSeekView.wdSeekPrimaryHeader;
		application.Selection.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphRight;
		application.Selection.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;
		string fileName = "C:\\Users\\xiahui\\Desktop\\OficeProgram\\3.jpg";
		InlineShape inlineShape = application.ActiveWindow.ActivePane.Selection.InlineShapes.AddPicture(fileName, ref Template, ref Template, ref Template);
		inlineShape.Height = 5f;
		inlineShape.Width = 20f;
		application.ActiveWindow.ActivePane.Selection.InsertAfter("  文档页眉");
		application.ActiveWindow.ActivePane.Selection.ParagraphFormat.Borders[WdBorderType.wdBorderBottom].LineStyle = WdLineStyle.wdLineStyleNone;
		application.ActiveWindow.ActivePane.Selection.Borders[WdBorderType.wdBorderBottom].Visible = false;
		application.ActiveWindow.ActivePane.View.SeekView = WdSeekView.wdSeekMainDocument;
		PageNumbers pageNumbers = application.Selection.Sections[1].Headers[WdHeaderFooterIndex.wdHeaderFooterEvenPages].PageNumbers;
		pageNumbers.NumberStyle = WdPageNumberStyle.wdPageNumberStyleNumberInDash;
		pageNumbers.HeadingLevelForChapter = 0;
		pageNumbers.IncludeChapterNumber = false;
		pageNumbers.RestartNumberingAtSection = false;
		pageNumbers.StartingNumber = 0;
		object PageNumberAlignment = WdPageNumberAlignment.wdAlignPageNumberCenter;
		object FirstPage = true;
		application.Selection.Sections[1].Footers[WdHeaderFooterIndex.wdHeaderFooterEvenPages].PageNumbers.Add(ref PageNumberAlignment, ref FirstPage);
		application.Selection.ParagraphFormat.LineSpacing = 16f;
		application.Selection.ParagraphFormat.FirstLineIndent = 30f;
		string text = "我是普通文本\n";
		document.Paragraphs.Last.Range.Text = text;
		document.Paragraphs.Last.Range.Text = "我再加一行试试，这里不加'\\n'";
		document.Paragraphs.Last.Range.Text += "不会覆盖的,";
		document.Paragraphs.Last.Range.InsertAfter("这是后面的内容\n");
		object Start = 0;
		object End = 4;
		Range range = document.Range(ref Start, ref End);
		range.Font.Color = WdColor.wdColorRed;
		range.Text = "哥是替换文字";
		document.Range(ref Start, ref End);
		object Unit = WdUnits.wdStory;
		application.Selection.EndKey(ref Unit, ref Template);
		application.Selection.ParagraphFormat.FirstLineIndent = 0f;
		text = "这是黑体文本\n";
		document.Paragraphs.Last.Range.Font.Name = "黑体";
		document.Paragraphs.Last.Range.Text = text;
		text = "这是粗体文本\n";
		application.Selection.EndKey(ref Unit, ref Template);
		document.Paragraphs.Last.Range.Font.Bold = 1;
		document.Paragraphs.Last.Range.Text = text;
		text = "我这个文本的字号是15号，而且是宋体\n";
		application.Selection.EndKey(ref Unit, ref Template);
		document.Paragraphs.Last.Range.Font.Size = 15f;
		document.Paragraphs.Last.Range.Font.Name = "宋体";
		document.Paragraphs.Last.Range.Text = text;
		text = "我是斜体字文本\n";
		application.Selection.EndKey(ref Unit, ref Template);
		document.Paragraphs.Last.Range.Font.Italic = 1;
		document.Paragraphs.Last.Range.Text = text;
		text = "我是蓝色的文本\n";
		application.Selection.EndKey(ref Unit, ref Template);
		document.Paragraphs.Last.Range.Font.Color = WdColor.wdColorBlue;
		document.Paragraphs.Last.Range.Text = text;
		text = "我是下划线文本\n";
		application.Selection.EndKey(ref Unit, ref Template);
		document.Paragraphs.Last.Range.Font.Underline = WdUnderline.wdUnderlineThick;
		document.Paragraphs.Last.Range.Text = text;
		text = "我是点线下划线，并且下划线是红色的\n";
		application.Selection.EndKey(ref Unit, ref Template);
		document.Paragraphs.Last.Range.Font.Underline = WdUnderline.wdUnderlineDottedHeavy;
		document.Paragraphs.Last.Range.Font.UnderlineColor = WdColor.wdColorRed;
		document.Paragraphs.Last.Range.Text = text;
		text = "我他妈不要下划线了，并且设置字号为12号，黑色不要斜体\n";
		application.Selection.EndKey(ref Unit, ref Template);
		document.Paragraphs.Last.Range.Font.Size = 12f;
		document.Paragraphs.Last.Range.Font.Underline = WdUnderline.wdUnderlineNone;
		document.Paragraphs.Last.Range.Font.Color = WdColor.wdColorBlack;
		document.Paragraphs.Last.Range.Font.Italic = 0;
		document.Paragraphs.Last.Range.Text = text;
		application.Selection.EndKey(ref Unit, ref Template);
		string fileName2 = Environment.CurrentDirectory + "\\6.jpg";
		object Range = document.Paragraphs.Last.Range;
		object LinkToFile = false;
		object SaveWithDocument = true;
		document.InlineShapes.AddPicture(fileName2, ref LinkToFile, ref SaveWithDocument, ref Range);
		application.Selection.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;
		document.InlineShapes[1].ScaleWidth = 20f;
		document.InlineShapes[1].ScaleHeight = 20f;
		document.Content.InsertAfter("\n");
		application.Selection.EndKey(ref Unit, ref Template);
		application.Selection.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;
		application.Selection.Font.Size = 10f;
		application.Selection.TypeText("图1 测试图片\n");
		document.Content.InsertAfter("\n");
		application.Selection.EndKey(ref Unit, ref Template);
		application.Selection.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphLeft;
		int num = 6;
		int num2 = 6;
		Table table = document.Tables.Add(application.Selection.Range, num, num2, ref Template, ref Template);
		table.Borders.Enable = 1;
		document.Tables[1].Cell(1, 1).Range.Text = "列\n行";
		for (int i = 1; i < num; i++)
		{
			for (int j = 1; j < num2; j++)
			{
				if (i == 1)
				{
					table.Cell(i, j + 1).Range.Text = "Column " + j;
				}
				if (j == 1)
				{
					table.Cell(i + 1, j).Range.Text = "Row " + i;
				}
				table.Cell(i + 1, j + 1).Range.Text = i + "行 " + j + "列";
			}
		}
		table.Rows.Add(ref Template);
		table.Rows[num + 1].Height = 35f;
		string fileName3 = Environment.CurrentDirectory + "\\6.jpg";
		object LinkToFile2 = false;
		object SaveWithDocument2 = true;
		object Range2 = table.Cell(num + 1, num2).Range;
		document.Application.ActiveDocument.InlineShapes.AddPicture(fileName3, ref LinkToFile2, ref SaveWithDocument2, ref Range2);
		document.Application.ActiveDocument.InlineShapes[2].Width = 50f;
		document.Application.ActiveDocument.InlineShapes[2].Height = 35f;
		Shape shape = document.Application.ActiveDocument.InlineShapes[2].ConvertToShape();
		shape.WrapFormat.Type = WdWrapType.wdWrapSquare;
		table.Rows.HeightRule = WdRowHeightRule.wdRowHeightAtLeast;
		table.Rows.Height = application.CentimetersToPoints(float.Parse("0.8"));
		table.Range.Font.Size = 10.5f;
		table.Range.Font.Bold = 0;
		table.Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;
		table.Range.Cells.VerticalAlignment = WdCellVerticalAlignment.wdCellAlignVerticalBottom;
		table.Borders.OutsideLineStyle = WdLineStyle.wdLineStyleDouble;
		table.Borders.InsideLineStyle = WdLineStyle.wdLineStyleSingle;
		table.Rows[1].Range.Font.Bold = 1;
		table.Rows[1].Range.Font.Size = 12f;
		table.Cell(1, 1).Range.Font.Size = 10.5f;
		application.Selection.Cells.Height = 30f;
		for (int k = 2; k <= num; k++)
		{
			table.Rows[k].Height = 20f;
		}
		table.Cell(1, 1).Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphRight;
		table.Cell(1, 1).Range.Paragraphs[2].Format.Alignment = WdParagraphAlignment.wdAlignParagraphLeft;
		table.Columns[1].Width = 50f;
		for (int l = 2; l <= num2; l++)
		{
			table.Columns[l].Width = 75f;
		}
		table.Cell(1, 1).Borders[WdBorderType.wdBorderDiagonalDown].Visible = true;
		table.Cell(1, 1).Borders[WdBorderType.wdBorderDiagonalDown].Color = WdColor.wdColorRed;
		table.Cell(1, 1).Borders[WdBorderType.wdBorderDiagonalDown].LineWidth = WdLineWidth.wdLineWidth150pt;
		table.Cell(4, 4).Merge(table.Cell(4, 5));
		table.Cell(2, 3).Merge(table.Cell(4, 3));
		application.Selection.EndKey(ref Unit, ref Template);
		document.Content.InsertAfter("\n");
		document.Content.InsertAfter("就写这么多，算了吧！2016.09.27");
		object FileFormat = WdSaveFormat.wdFormatDocument;
		document.SaveAs(ref FileName, ref FileFormat, ref Template, ref Template, ref Template, ref Template, ref Template, ref Template, ref Template, ref Template, ref Template, ref Template, ref Template, ref Template, ref Template, ref Template);
		document.Close(ref Template, ref Template, ref Template);
		application.Quit(ref Template, ref Template, ref Template);
		Console.WriteLine(string.Concat(FileName, " 创建完毕！"));
		Console.ReadKey();
		Microsoft.Office.Interop.Word.Application application2 = new ApplicationClass();
		Microsoft.Office.Interop.Word.Document document2 = null;
		try
		{
			object ConfirmConversions = Type.Missing;
			application2.Visible = true;
			string text2 = Environment.CurrentDirectory + "\\MyWord_Print.doc";
			object FileName2 = text2;
			document2 = application2.Documents.Open(ref FileName2, ref ConfirmConversions, ref ConfirmConversions, ref ConfirmConversions, ref ConfirmConversions, ref ConfirmConversions, ref ConfirmConversions, ref ConfirmConversions, ref ConfirmConversions, ref ConfirmConversions, ref ConfirmConversions, ref ConfirmConversions, ref ConfirmConversions, ref ConfirmConversions, ref ConfirmConversions, ref ConfirmConversions);
			string text3 = document2.Paragraphs[1].Range.Text.Trim();
			Console.WriteLine("你他妈输出temp干嘛？");
		}
		catch (Exception ex)
		{
			Console.WriteLine(ex.Message);
		}
		document = document2;
		document.Paragraphs.Last.Range.Text += "我真的不打算再写了,就写这么多吧";
		Console.ReadKey();
	}

	public void PrintToRtfStatic(string FilePath, bool Open)
	{
		if (!HasChrom)
		{
			return;
		}
		rtprtb.Text = "";
		Clipboard.Clear();
		Clipboard.SetText("                   ");
		rtprtb.Paste();
		Clipboard.Clear();
		Clipboard.SetText("                   ");
		mstSetChromForm.ReportTitle.SelectAll();
		mstSetChromForm.ReportTitle.Copy();
		rtprtb.Paste();
		Clipboard.Clear();
		Clipboard.SetText(Environment.NewLine + Environment.NewLine + Environment.NewLine);
		rtprtb.Paste();
		Clipboard.Clear();
		Clipboard.SetText("打印时间：" + DateTime.Now.ToLongDateString() + "\r\n\r\n");
		rtprtb.Paste();
		Clipboard.Clear();
		Clipboard.SetText("\r\n\r\n进样时间：" + chromatogram_1.userArchives[0].saveTime.ToString() + "\r\n\r\n");
		rtprtb.Paste();
		Clipboard.Clear();
		if (mstSetChromForm.PrintMethod.PrintTitleTop == "")
		{
			Clipboard.SetText(" ");
		}
		else
		{
			Clipboard.SetText(mstSetChromForm.PrintMethod.PrintTitleTop);
		}
		rtprtb.Paste();
		Clipboard.Clear();
		rtprtb.Paste();
		dpgnlChrom.Dock = DockStyle.None;
		Control control = dpgnlChrom;
		Clipboard.Clear();
		rtprtb.Paste();
		control.Width = 600;
		Bitmap bitmap = new Bitmap(control.Width, control.Height);
		Clipboard.Clear();
		Graphics.FromImage(bitmap);
		System.Drawing.Rectangle bounds = control.Bounds;
		bounds.X = 0;
		bounds.Y = 0;
		control.DrawToBitmap(bitmap, bounds);
		dpgnlChrom.Dock = DockStyle.Fill;
		Clipboard.Clear();
		Clipboard.SetImage(bitmap);
		rtprtb.Paste();
		Clipboard.Clear();
		Clipboard.SetText("\r\n\r\n");
		rtprtb.Paste();
		Clipboard.Clear();
		ClassAutoCalibraPeak[] array = new ClassAutoCalibraPeak[chromatogram_0.Length];
		float[] array2 = new float[chromatogram_0.Length];
		float[] array3 = new float[chromatogram_0.Length];
		float[] array4 = new float[chromatogram_0.Length];
		float[] array5 = new float[chromatogram_0.Length];
		float[] array6 = new float[chromatogram_0.Length];
		float[] array7 = new float[chromatogram_0.Length];
		string[] array8 = new string[chromatogram_0.Length];
		for (int i = 0; i < chromatogram_0[0].caliGnl.cmpds.Length; i++)
		{
			Clipboard.SetText("──────────────────────────\r\n\r\n");
			rtprtb.Paste();
			Clipboard.Clear();
			Clipboard.SetText("进样名称             保留时间    峰面积    峰高   样品量\r\n\r\n");
			rtprtb.Paste();
			Clipboard.Clear();
			Clipboard.SetText(" ".PadRightWhileDouble(21, ' ') + "" + chromatogram_0[0].caliGnl.cmpds[i].cmpdInfo.name.PadRightWhileDouble(7, ' ') + "    " + chromatogram_0[0].caliGnl.cmpds[i].cmpdInfo.name.PadRightWhileDouble(7, ' ') + " " + chromatogram_0[0].caliGnl.cmpds[i].cmpdInfo.name.PadRightWhileDouble(7, ' ') + "       " + chromatogram_0[0].caliGnl.cmpds[i].cmpdInfo.name.PadRightWhileDouble(7, ' ') + "\r\n\r\n");
			rtprtb.Paste();
			Clipboard.Clear();
			for (int j = 0; j < chromatogram_0.Length; j++)
			{
				array[j] = new ClassAutoCalibraPeak();
				array[j].peak = new Peak[0];
				array[j].peak = chromatogram_0[j].GetPeakAllCompound();
				array2[j] = array[j].peak[i].pkRT;
				array3[j] = array[j].peak[i].area;
				array4[j] = array[j].peak[i].areaPer;
				array5[j] = array[j].peak[i].height;
				array6[j] = array[j].peak[i].heightPer;
				array7[j] = chromatogram_0[j].chromInfo.injVolumn;
				array8[j] = chromatogram_0[j].fName.PadRightWhileDouble(21, ' ');
				Clipboard.SetText(array8[j] + "" + array2[j].ToString("0.000") + "    " + array3[j].ToString("0.0000") + " " + array5[j].ToString("0.0000") + "       " + array7[j].ToString("0.000") + "\r\n\r\n");
				rtprtb.Paste();
				Clipboard.Clear();
			}
			Clipboard.SetText("──────────────────────────\r\n\r\n");
			rtprtb.Paste();
			Clipboard.Clear();
		}
		CaliGnl caliGnl = chromatogram_0[0].caliGnl;
		Compound[] cmpds = caliGnl.cmpds;
		CmpdDisplay cmpdDisplay = new CmpdDisplay(WinStyle.CaliGnl, null);
		Size size = new Size(200, 150);
		RectangleF rectangleF = new RectangleF(0f, 0f, size.Width, size.Height);
		for (int k = 0; k < 1; k++)
		{
			Compound compound = cmpds[k];
			if (compound.levels == null || compound.levels.Length == 0)
			{
				break;
			}
			Bitmap image = new Bitmap(200, 150);
			Graphics graphics = Graphics.FromImage(image);
			string string_ = Class49.MesureUnit() + ".s";
			if (compound.cmpdInfo.respStyle == RespStyle.Height)
			{
				string_ = Class49.MesureUnit();
			}
			cmpdDisplay.rcPage = rectangleF;
			cmpdDisplay.dskRC = rectangleF;
			cmpdDisplay.SetCompound2(compound, bool_0: false, caliGnl.caliOption.cmpdUnit, ref string_);
			cmpdDisplay.Draw(graphics, erase: true);
			graphics.Dispose();
		}
		Clipboard.SetText("──────────────────────────\r\n\r\n");
		rtprtb.Paste();
		Peak[] rltPeaks = chromatogram_0[0].RltPeaks;
		Clipboard.Clear();
		Clipboard.SetText("──────────────────────────\r\n\r\n");
		rtprtb.Paste();
		Clipboard.Clear();
		rtprtb.Paste();
		Clipboard.Clear();
		Clipboard.Clear();
		if (mstSetChromForm.PrintMethod.PrintTitleBotom != "")
		{
			Clipboard.SetText(mstSetChromForm.PrintMethod.PrintTitleBotom);
		}
		else
		{
			Clipboard.SetText(" ");
		}
		rtprtb.Paste();
		string path = "PrintDoc.rtf";
		if (FilePath == "")
		{
			if (Class49.int_12 == 0)
			{
				path = System.Windows.Forms.Application.StartupPath + "\\PrintDoc1.rtf";
			}
			else if (Class49.int_12 == 1)
			{
				path = System.Windows.Forms.Application.StartupPath + "\\PrintDoc.doc";
			}
		}
		else
		{
			path = FilePath;
		}
		try
		{
			File.Delete(path);
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message);
		}
		path = "D:\\PrintDoc1.rtf";
		rtprtb.SaveFile(path, RichTextBoxStreamType.RichText);
		Process process = new Process();
		string fileName = mstSetChromForm.PrintMethod.PPreView switch
		{
			PrintPreview.写字板 => "wordpad.exe", 
			PrintPreview.Word => "winword.exe", 
			PrintPreview.程序自带 => "wordpad.exe", 
			_ => "wordpad.exe", 
		};
		string arguments = path;
		if (Open)
		{
			ProcessStartInfo startInfo = new ProcessStartInfo(fileName, arguments);
			process.StartInfo = startInfo;
			process.Start();
		}
	}

	public void PrintToRtfRZ(string FilePath, bool Open)
	{
		if (!HasChrom)
		{
			return;
		}
		float num = 0f;
		float num2 = 0f;
		float num3 = 0f;
		float num4 = 0f;
		float num5 = 0f;
		float num6 = 0f;
		float num7 = 0f;
		float num8 = 0f;
		float num9 = 0f;
		float num10 = 0f;
		float num11 = 0f;
		float num12 = 0f;
		float num13 = 216.89f;
		float num14 = 4.713f;
		float num15 = 0f;
		float num16 = 0f;
		float num17 = 0f;
		float num18 = 0f;
		rtprtb.Text = "";
		Clipboard.Clear();
		Clipboard.SetText("                   ");
		rtprtb.Paste();
		Clipboard.Clear();
		Clipboard.SetText("                   ");
		mstSetChromForm.ReportTitle.SelectAll();
		mstSetChromForm.ReportTitle.Copy();
		rtprtb.Paste();
		Clipboard.Clear();
		Clipboard.SetText(Environment.NewLine + Environment.NewLine + Environment.NewLine);
		rtprtb.Paste();
		Clipboard.Clear();
		Clipboard.SetText("打印时间：" + DateTime.Now.ToLongDateString() + "\r\n\r\n");
		rtprtb.Paste();
		Clipboard.Clear();
		Clipboard.SetText("\r\n\r\n进样时间：" + chromatogram_1.userArchives[0].saveTime.ToString() + "\r\n\r\n");
		rtprtb.Paste();
		Clipboard.Clear();
		if (mstSetChromForm.PrintMethod.PrintTitleTop == "")
		{
			Clipboard.SetText(" ");
		}
		else
		{
			Clipboard.SetText(mstSetChromForm.PrintMethod.PrintTitleTop);
		}
		rtprtb.Paste();
		Clipboard.Clear();
		rtprtb.Paste();
		dpgnlChrom.Dock = DockStyle.None;
		Control control = dpgnlChrom;
		Clipboard.Clear();
		rtprtb.Paste();
		control.Width = 600;
		Bitmap bitmap = new Bitmap(control.Width, control.Height);
		Clipboard.Clear();
		Graphics.FromImage(bitmap);
		System.Drawing.Rectangle bounds = control.Bounds;
		bounds.X = 0;
		bounds.Y = 0;
		control.DrawToBitmap(bitmap, bounds);
		dpgnlChrom.Dock = DockStyle.Fill;
		Clipboard.Clear();
		Clipboard.SetImage(bitmap);
		rtprtb.Paste();
		Clipboard.Clear();
		Clipboard.SetText("\r\n\r\n");
		rtprtb.Paste();
		Clipboard.Clear();
		Clipboard.SetText("────────────────────────────────────────────\r\n\r\n");
		rtprtb.Paste();
		Clipboard.Clear();
		if (mstSetChromForm.cbprsUncalBase.SelectedIndex == 0)
		{
			Clipboard.SetText("序号   保留时间    名称             浓度       浓度百分比       峰面积        面积百分比\r\n\r\n");
		}
		else
		{
			Clipboard.SetText("序号   保留时间    名称             浓度       浓度百分比       峰  高        峰高百分比\r\n\r\n");
		}
		rtprtb.Paste();
		Clipboard.Clear();
		Clipboard.SetText("────────────────────────────────────────────\r\n\r\n");
		rtprtb.Paste();
		Peak[] rltPeaks = chromatogram_0[0].RltPeaks;
		double num19 = 0.0;
		double num20 = 0.0;
		double num21 = 0.0;
		for (int i = 0; i < rltPeaks.Length; i++)
		{
			Peak peak = rltPeaks[i];
			Clipboard.Clear();
			string text = ((i <= 9) ? (" " + i) : i.ToString());
			string text2 = peak.pkRT.ToString("000.0000");
			for (int j = 0; j < text2.Length && text2[j] == '0'; j++)
			{
				if (j + 1 < text2.Length && text2[j + 1] != '.')
				{
					char[] array = text2.ToCharArray();
					array[j] = ' ';
					text2 = new string(array);
				}
			}
			string text3 = peak.name;
			int num22 = 12 - Encoding.Default.GetBytes(text3.Trim()).Length;
			for (int k = 0; k < num22; k++)
			{
				text3 += " ";
			}
			string text4;
			if (peak.amount == -1f)
			{
				text4 = "   0.0   ";
			}
			else
			{
				text4 = peak.amount.ToString("0000.0000");
				for (int l = 0; l < text4.Length && text4[l] == '0'; l++)
				{
					if (l + 1 < text4.Length && text4[l + 1] != '.')
					{
						char[] array2 = text4.ToCharArray();
						array2[l] = ' ';
						text4 = new string(array2);
					}
				}
			}
			string text5;
			if (peak.amountPer != -1f)
			{
				text5 = (peak.amountPer * 100f).ToString("00.000");
				for (int m = 0; m < text5.Length && text5[m] == '0'; m++)
				{
					if (m + 1 < text5.Length && text5[m + 1] != '.')
					{
						char[] array3 = text5.ToCharArray();
						array3[m] = ' ';
						text5 = new string(array3);
					}
				}
			}
			else
			{
				text5 = " 0.0  ";
			}
			string text6;
			if (peak.area != -1f)
			{
				text6 = peak.area.ToString("0000.0000");
				for (int n = 0; n < text6.Length && text6[n] == '0'; n++)
				{
					if (n + 1 < text6.Length && text6[n + 1] != '.')
					{
						char[] array4 = text6.ToCharArray();
						array4[n] = ' ';
						text6 = new string(array4);
					}
				}
			}
			else
			{
				text6 = "   0.0   ";
			}
			string text7;
			if (peak.areaPer != -1f)
			{
				text7 = (peak.areaPer * 100f).ToString("00.000");
				for (int num23 = 0; num23 < text7.Length && text7[num23] == '0'; num23++)
				{
					if (num23 + 1 < text7.Length && text7[num23 + 1] != '.')
					{
						char[] array5 = text7.ToCharArray();
						array5[num23] = ' ';
						text7 = new string(array5);
					}
				}
			}
			else
			{
				text7 = " 0.0  ";
			}
			string text8;
			if (peak.height != -1f)
			{
				text8 = peak.height.ToString("0000.0000");
				for (int num24 = 0; num24 < text8.Length && text8[num24] == '0'; num24++)
				{
					if (num24 + 1 < text8.Length && text8[num24 + 1] != '.')
					{
						char[] array6 = text8.ToCharArray();
						array6[num24] = ' ';
						text8 = new string(array6);
					}
				}
			}
			else
			{
				text8 = "   0.0   ";
			}
			string text9;
			if (peak.heightPer != -1f)
			{
				text9 = (peak.heightPer * 100f).ToString("00.000");
				for (int num25 = 0; num25 < text9.Length && text9[num25] == '0'; num25++)
				{
					if (num25 + 1 < text9.Length && text9[num25 + 1] != '.')
					{
						char[] array7 = text9.ToCharArray();
						array7[num25] = ' ';
						text9 = new string(array7);
					}
				}
			}
			else
			{
				text9 = " 0.0  ";
			}
			if (mstSetChromForm.cbprsUncalBase.SelectedIndex == 0)
			{
				Clipboard.SetText(text + "      " + text2 + "    " + text3 + " " + text4 + "       " + text5 + "%       " + text6 + "     " + text7 + "%\r\n\r\n");
			}
			else
			{
				Clipboard.SetText(text + "      " + text2 + "    " + text3 + " " + text4 + "       " + text5 + "%       " + text8 + "      " + text9 + "%\r\n\r\n");
			}
			rtprtb.Paste();
			if (peak.amount != -1f)
			{
				num19 += (double)peak.amount;
			}
			num20 += (double)peak.area;
			num21 += (double)peak.height;
		}
		Clipboard.Clear();
		Clipboard.SetText("────────────────────────────────────────────\r\n\r\n");
		rtprtb.Paste();
		Clipboard.Clear();
		if (mstSetChromForm.cbprsUncalBase.SelectedIndex == 0)
		{
			Clipboard.SetText("总计                             " + num19.ToString("0.0000") + "             100%       " + num20.ToString("0.0000") + "        100%\r\n\r\n\r\n");
		}
		else
		{
			Clipboard.SetText("总计                             " + num19.ToString("0.0000") + "             100%       " + num21.ToString("0.0000") + "        100%\r\n\r\n\r\n");
		}
		rtprtb.Paste();
		Clipboard.Clear();
		Peak[] peakFromCompound = chromatogram_0[0].GetPeakFromCompound();
		int num26 = 0;
		while (1 <= peakFromCompound.Length && num26 < peakFromCompound.Length)
		{
			if (peakFromCompound[num26].compound.eFunc.curveFit == CurveFit.Free)
			{
				num += Program.getCharacteristic(peakFromCompound[num26].name, peakFromCompound[num26].amountPer, 1);
				num4 += Program.getCharacteristic(peakFromCompound[num26].name, peakFromCompound[num26].amountPer, 2);
				num12 += Program.getCharacteristic(peakFromCompound[num26].name, peakFromCompound[num26].amountPer, 3);
				num11 += Program.getCharacteristic(peakFromCompound[num26].name, peakFromCompound[num26].amountPer, 4);
				num2 += Program.getCharacteristic(peakFromCompound[num26].name, peakFromCompound[num26].amountPer, 5);
				num5 += Program.getCharacteristic(peakFromCompound[num26].name, peakFromCompound[num26].amountPer, 6);
				num3 += Program.getCharacteristic(peakFromCompound[num26].name, peakFromCompound[num26].amountPer, 7);
			}
			else
			{
				num += Program.getCharacteristic(peakFromCompound[num26].name, peakFromCompound[num26].amountPer, 1);
				num4 += Program.getCharacteristic(peakFromCompound[num26].name, peakFromCompound[num26].amountPer, 2);
				num12 += Program.getCharacteristic(peakFromCompound[num26].name, peakFromCompound[num26].amountPer, 3);
				num11 += Program.getCharacteristic(peakFromCompound[num26].name, peakFromCompound[num26].amountPer, 4);
				num2 += Program.getCharacteristic(peakFromCompound[num26].name, peakFromCompound[num26].amountPer, 5);
				num5 += Program.getCharacteristic(peakFromCompound[num26].name, peakFromCompound[num26].amountPer, 6);
				num3 += Program.getCharacteristic(peakFromCompound[num26].name, peakFromCompound[num26].amountPer, 7);
			}
			num26++;
		}
		double d = num12;
		float num27 = (float)Math.Sqrt(d);
		num6 = num / num27;
		num7 = num2 / num27;
		num8 = num4 / num27;
		num9 = num5 / num27;
		double num28 = num15;
		float num29 = (float)Math.Pow(num28, 2.0);
		num10 = (1f + 0.0054f * num29) * (num16 + 0.3f * num17 + 0.6f * num18) / num27;
		string text10 = Lang.PS("在标准状态（273.15K、101325Pa）", "In standard condition（273.15K、101325Pa）") + "\n" + Lang.PS("平均分子量 =", "average mean molecular weight =") + num3.ToString("0.000") + "\n" + Lang.PS("高热值 =", "high heating value =") + num.ToString("0.000") + "(MJ / Nm3) = " + num2.ToString("0.000") + "(KCal / Nm3)\n" + Lang.PS("低热值 =", "low heating value =") + num4.ToString("0.000") + "(MJ / Nm3) = " + num5.ToString("0.000") + "(KCal / Nm3)\n" + Lang.PS("高热值华白数 =", "High calorific value White number =") + num6.ToString("0.000") + "(MJ / Nm3) = " + num7.ToString("0.000") + "(KCal / Nm3)\n" + Lang.PS("低热值华白数 =", "Low calorific value White number =") + num8.ToString("0.000") + "(MJ / Nm3) = " + num9.ToString("0.000") + "(KCal / Nm3)\n" + Lang.PS("密度 =", "density =") + num11.ToString("0.000") + "(kg / m3)\n" + Lang.PS("相对密度 =", "relative density =") + num12.ToString("0.000") + "\n" + Lang.PS("临界温度 =", "critical temperature =") + num13.ToString("0.000") + "(K)\n" + Lang.PS("临界压力 =", "critical pressure =") + num14.ToString("0.000") + "(MPa)\n";
		Clipboard.SetText("────────────────────────────────────────────\r\n\r\n");
		rtprtb.Paste();
		Clipboard.Clear();
		Clipboard.SetText(text10 + "\r\n\r\n");
		rtprtb.Paste();
		Clipboard.Clear();
		Clipboard.SetText("────────────────────────────────────────────\r\n\r\n");
		rtprtb.Paste();
		Clipboard.Clear();
		if (mstSetChromForm.PrintMethod.PrintTitleBotom != "")
		{
			Clipboard.SetText(mstSetChromForm.PrintMethod.PrintTitleBotom);
		}
		else
		{
			Clipboard.SetText(" ");
		}
		rtprtb.Paste();
		string path = "PrintDoc.rtf";
		if (FilePath == "")
		{
			if (Class49.int_12 == 0)
			{
				path = System.Windows.Forms.Application.StartupPath + "\\PrintDoc1.rtf";
			}
			else if (Class49.int_12 == 1)
			{
				path = System.Windows.Forms.Application.StartupPath + "\\PrintDoc.doc";
			}
		}
		else
		{
			path = FilePath;
		}
		try
		{
			File.Delete(path);
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message);
		}
		path = "D:\\PrintDoc1.rtf";
		rtprtb.SaveFile(path, RichTextBoxStreamType.RichText);
		Process process = new Process();
		string fileName = mstSetChromForm.PrintMethod.PPreView switch
		{
			PrintPreview.写字板 => "wordpad.exe", 
			PrintPreview.Word => "winword.exe", 
			PrintPreview.程序自带 => "wordpad.exe", 
			_ => "wordpad.exe", 
		};
		string arguments = path;
		if (Open)
		{
			ProcessStartInfo startInfo = new ProcessStartInfo(fileName, arguments);
			process.StartInfo = startInfo;
			process.Start();
		}
	}

	public void PrintToFile(string FilePath, bool Open)
	{
		if (!HasChrom)
		{
			return;
		}
		rtprtb.Text = "";
		Clipboard.Clear();
		Clipboard.SetText("                   ");
		rtprtb.Paste();
		Clipboard.Clear();
		Clipboard.SetText("                   ");
		mstSetChromForm.ReportTitle.SelectAll();
		mstSetChromForm.ReportTitle.Copy();
		rtprtb.Paste();
		Clipboard.Clear();
		Clipboard.SetText(Environment.NewLine + Environment.NewLine + Environment.NewLine);
		rtprtb.Paste();
		Clipboard.Clear();
		Clipboard.SetText("打印时间：" + DateTime.Now.ToLongDateString() + "\r\n\r\n");
		rtprtb.Paste();
		Clipboard.Clear();
		Clipboard.SetText("\r\n\r\n进样时间：" + chromatogram_1.userArchives[0].saveTime.ToString() + "\r\n\r\n");
		rtprtb.Paste();
		Clipboard.Clear();
		if (mstSetChromForm.PrintMethod.PrintTitleTop == "")
		{
			Clipboard.SetText(" ");
		}
		else
		{
			Clipboard.SetText(mstSetChromForm.PrintMethod.PrintTitleTop);
		}
		rtprtb.Paste();
		Clipboard.Clear();
		rtprtb.Paste();
		dpgnlChrom.Dock = DockStyle.None;
		Control control = dpgnlChrom;
		Clipboard.Clear();
		rtprtb.Paste();
		control.Width = 600;
		Bitmap bitmap = new Bitmap(control.Width, control.Height);
		Clipboard.Clear();
		Graphics.FromImage(bitmap);
		System.Drawing.Rectangle bounds = control.Bounds;
		bounds.X = 0;
		bounds.Y = 0;
		control.DrawToBitmap(bitmap, bounds);
		dpgnlChrom.Dock = DockStyle.Fill;
		Clipboard.Clear();
		Clipboard.SetImage(bitmap);
		rtprtb.Paste();
		Clipboard.Clear();
		Clipboard.SetText("\r\n\r\n");
		rtprtb.Paste();
		Clipboard.Clear();
		Clipboard.SetText("────────────────────────────────────────────\r\n\r\n");
		rtprtb.Paste();
		Clipboard.Clear();
		if (mstSetChromForm.cbprsUncalBase.SelectedIndex == 0)
		{
			Clipboard.SetText("序号   保留时间    名称             浓度       浓度百分比       峰面积        面积百分比\r\n\r\n");
		}
		else
		{
			Clipboard.SetText("序号   保留时间    名称             浓度       浓度百分比       峰  高        峰高百分比\r\n\r\n");
		}
		rtprtb.Paste();
		Clipboard.Clear();
		Clipboard.SetText("────────────────────────────────────────────\r\n\r\n");
		rtprtb.Paste();
		Peak[] rltPeaks = chromatogram_0[0].RltPeaks;
		double num = 0.0;
		double num2 = 0.0;
		double num3 = 0.0;
		for (int i = 0; i < rltPeaks.Length; i++)
		{
			Peak peak = rltPeaks[i];
			Clipboard.Clear();
			string text = ((i <= 9) ? (" " + i) : i.ToString());
			string text2 = peak.pkRT.ToString("000.0000");
			for (int j = 0; j < text2.Length && text2[j] == '0'; j++)
			{
				if (j + 1 < text2.Length && text2[j + 1] != '.')
				{
					char[] array = text2.ToCharArray();
					array[j] = ' ';
					text2 = new string(array);
				}
			}
			string text3 = peak.name;
			int num4 = 12 - Encoding.Default.GetBytes(text3.Trim()).Length;
			for (int k = 0; k < num4; k++)
			{
				text3 += " ";
			}
			string text4;
			if (peak.amount == -1f)
			{
				text4 = "   0.0   ";
			}
			else
			{
				text4 = peak.amount.ToString("0000.0000");
				for (int l = 0; l < text4.Length && text4[l] == '0'; l++)
				{
					if (l + 1 < text4.Length && text4[l + 1] != '.')
					{
						char[] array2 = text4.ToCharArray();
						array2[l] = ' ';
						text4 = new string(array2);
					}
				}
			}
			string text5;
			if (peak.amountPer != -1f)
			{
				text5 = (peak.amountPer * 100f).ToString("00.000");
				for (int m = 0; m < text5.Length && text5[m] == '0'; m++)
				{
					if (m + 1 < text5.Length && text5[m + 1] != '.')
					{
						char[] array3 = text5.ToCharArray();
						array3[m] = ' ';
						text5 = new string(array3);
					}
				}
			}
			else
			{
				text5 = " 0.0  ";
			}
			string text6;
			if (peak.area != -1f)
			{
				text6 = peak.area.ToString("0000.0000");
				for (int n = 0; n < text6.Length && text6[n] == '0'; n++)
				{
					if (n + 1 < text6.Length && text6[n + 1] != '.')
					{
						char[] array4 = text6.ToCharArray();
						array4[n] = ' ';
						text6 = new string(array4);
					}
				}
			}
			else
			{
				text6 = "   0.0   ";
			}
			string text7;
			if (peak.areaPer != -1f)
			{
				text7 = (peak.areaPer * 100f).ToString("00.000");
				for (int num5 = 0; num5 < text7.Length && text7[num5] == '0'; num5++)
				{
					if (num5 + 1 < text7.Length && text7[num5 + 1] != '.')
					{
						char[] array5 = text7.ToCharArray();
						array5[num5] = ' ';
						text7 = new string(array5);
					}
				}
			}
			else
			{
				text7 = " 0.0  ";
			}
			string text8;
			if (peak.height != -1f)
			{
				text8 = peak.height.ToString("0000.0000");
				for (int num6 = 0; num6 < text8.Length && text8[num6] == '0'; num6++)
				{
					if (num6 + 1 < text8.Length && text8[num6 + 1] != '.')
					{
						char[] array6 = text8.ToCharArray();
						array6[num6] = ' ';
						text8 = new string(array6);
					}
				}
			}
			else
			{
				text8 = "   0.0   ";
			}
			string text9;
			if (peak.heightPer != -1f)
			{
				text9 = (peak.heightPer * 100f).ToString("00.000");
				for (int num7 = 0; num7 < text9.Length && text9[num7] == '0'; num7++)
				{
					if (num7 + 1 < text9.Length && text9[num7 + 1] != '.')
					{
						char[] array7 = text9.ToCharArray();
						array7[num7] = ' ';
						text9 = new string(array7);
					}
				}
			}
			else
			{
				text9 = " 0.0  ";
			}
			if (mstSetChromForm.cbprsUncalBase.SelectedIndex == 0)
			{
				Clipboard.SetText(text + "      " + text2 + "    " + text3 + " " + text4 + "       " + text5 + "%       " + text6 + "     " + text7 + "%\r\n\r\n");
			}
			else
			{
				Clipboard.SetText(text + "      " + text2 + "    " + text3 + " " + text4 + "       " + text5 + "%       " + text8 + "      " + text9 + "%\r\n\r\n");
			}
			rtprtb.Paste();
			if (peak.amount != -1f)
			{
				num += (double)peak.amount;
			}
			num2 += (double)peak.area;
			num3 += (double)peak.height;
		}
		Clipboard.Clear();
		Clipboard.SetText("────────────────────────────────────────────\r\n\r\n");
		rtprtb.Paste();
		Clipboard.Clear();
		if (mstSetChromForm.cbprsUncalBase.SelectedIndex == 0)
		{
			Clipboard.SetText("总计                             " + num.ToString("0.0000") + "             100%       " + num2.ToString("0.0000") + "        100%\r\n\r\n\r\n");
		}
		else
		{
			Clipboard.SetText("总计                             " + num.ToString("0.0000") + "             100%       " + num3.ToString("0.0000") + "        100%\r\n\r\n\r\n");
		}
		rtprtb.Paste();
		Clipboard.Clear();
		Clipboard.SetText(" \r\n");
		rtprtb.Paste();
		Clipboard.Clear();
		if (mstSetChromForm.PrintMethod.PrintTitleBotom != "")
		{
			Clipboard.SetText(mstSetChromForm.PrintMethod.PrintTitleBotom);
		}
		else
		{
			Clipboard.SetText(" ");
		}
		rtprtb.Paste();
		string text10 = "PrintDoc.rtf";
		if (FilePath == "")
		{
			if (Class49.int_12 == 0)
			{
				text10 = System.Windows.Forms.Application.StartupPath + "\\PrintDoc.rtf";
			}
			else if (Class49.int_12 == 1)
			{
				text10 = System.Windows.Forms.Application.StartupPath + "\\PrintDoc.doc";
			}
		}
		else
		{
			text10 = FilePath;
		}
		try
		{
			File.Delete(text10);
		}
		catch (Exception)
		{
		}
		rtprtb.SaveFile(text10, RichTextBoxStreamType.RichText);
		Process process = new Process();
		string fileName = mstSetChromForm.PrintMethod.PPreView switch
		{
			PrintPreview.写字板 => "wordpad.exe", 
			PrintPreview.Word => "winword.exe", 
			PrintPreview.程序自带 => "wordpad.exe", 
			_ => "wordpad.exe", 
		};
		string arguments = text10;
		if (Open)
		{
			ProcessStartInfo startInfo = new ProcessStartInfo(fileName, arguments);
			process.StartInfo = startInfo;
			process.Start();
		}
	}

	private void btnReportSetup_Click(object sender, EventArgs e)
	{
	}

	private void toolStripMenuItem_0_Click(object sender, EventArgs e)
	{
		ToolStrip toolStrip = toolStrip2;
		bool visible = (toolStripMenuItem_0.Checked = !toolStripMenuItem_0.Checked);
		toolStrip.Visible = visible;
	}

	private void btngblDtecDelay_Click(object sender, EventArgs e)
	{
		UpdateButtonCheckState(bool_10: false);
		if (HasChrom && !CheckAuthority(bool_10: true) && !(dpgnlChrom.Cursor != Cursors.Default))
		{
			integRow_2.oprtStyle = (IntegOprtStyle)(sender as ToolStripItem).Tag;
			chromDataGrid.lbExpress.Text = integRow_2.ExpString(1);
			object_0 = sender;
			UpdateButtonCheckState(bool_10: true);
			method_0(bool_10: false);
		}
	}

	protected void miHpHelp_Click(object sender, EventArgs e)
	{
		if (chromDataGrid.tcChrom.SelectedTab == chromDataGrid.tpResults)
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
		else if (chromDataGrid.tcChrom.SelectedTab == chromDataGrid.tpPerformance)
		{
			Class49.smethod_32("柱效");
		}
		else if (chromDataGrid.tcChrom.SelectedTab == chromDataGrid.tpSummary)
		{
			Class49.smethod_32("总结表");
		}
		else
		{
			Class49.smethod_32("谱图处理");
		}
	}

	private void misstcClearParas_Click(object sender, EventArgs e)
	{
		if (sender != misstcNew && sender != misstcNew)
		{
			if (sender != misstcOpen && sender != misstcOpen)
			{
				if (sender == misstcSave || sender == misstcSave)
				{
					if (sst_0.sstCmpds.Length != 0)
					{
						if (sst_0.fullName == "")
						{
							misstcClearParas_Click(misstcSaveas, null);
						}
						else
						{
							sst_0.SaveToFile(sst_0.fullName);
						}
					}
					return;
				}
				if (sender == misstcSaveas || sender == misstcSaveas)
				{
					if (sst_0.sstCmpds.Length != 0 && saveFileDialog_1.ShowDialog() == DialogResult.OK)
					{
						sst_0.SaveToFile(saveFileDialog_1.FileName);
					}
					return;
				}
				if (sender == misstcUpdateFromCalib)
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
				else if (sender != misstcSet && sender != misstcSet)
				{
					if (sender == misstcClearParas || sender == misstcClearParas)
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
				if (openFileDialog_3.ShowDialog() != DialogResult.OK)
				{
					return;
				}
				sst_0.LoadFromFile(openFileDialog_3.FileName);
			}
		}
		else
		{
			sst_0.fullName = (sst_0.fName = "");
			sst_0.ResetRecs();
		}
		method_49();
	}

	private void toolStripMenuItem_1_Click(object sender, EventArgs e)
	{
	}

	private void method_31(object sender, EventArgs e)
	{
	}

	public void Opensda(string filePath)
	{
		if (chromatogram_0.Length == 0)
		{
			chromDisplay_0.ClearDisSignals();
		}
		OpenChrom(filePath, sampling: false, ofdChrom.Checked);
	}

	private static string smethod_0()
	{
		long num = 1L;
		byte[] array = Guid.NewGuid().ToByteArray();
		foreach (byte b in array)
		{
			num *= b + 1;
		}
		return $"{num - DateTime.Now.Ticks:x}";
	}

	public void OpenChrom(string chromName, bool sampling, bool useCurrent)
	{
		m_strChormFileName = chromName;
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
		if (chromatogram != null)
		{
			OpenChrom(chromatogram, chromName, sampling, useCurrent);
		}
	}

	public void OpenChrom(Chromatogram chromatogram, string chromName, bool sampling, bool useCurrent)
	{
		if (chromatogram != null && chromatogram.signal != null)
		{
			chromatogram.signal.Smooth(frmParam.iSmooths);
		}
		m_strChormFileName = chromName;
		if (chromName.Contains("fid") || chromName.Contains("FID") || chromName.Contains("pdd") || chromName.Contains("PDD"))
		{
			Class49.bool_4 = true;
			string_277 = "面积\n[" + Class49.MesureUnit() + ".s]";
			string_278 = "高度\n[" + Class49.MesureUnit() + "]";
			lbyUnit.Text = "pA";
			label29.Text = "pA";
			chromDisplay_0.unitY = "pA";
			chromDisplay_0.txtY = "信号";
			chromDataGrid.SetGvRltsGnlHeaderText(chromDataGrid.gvRltsGnl);
			chromDataGrid.SetGvRltsGnlHeaderText(chromDataGrid.gvRltsGpc);
			chromDataGrid.SetGvRltsGnlHeaderText(chromDataGrid.gvRltsDad);
		}
		else
		{
			Class49.bool_4 = false;
			string_277 = "面积\n[" + Class49.MesureUnit() + ".s]";
			string_278 = "高度\n[" + Class49.MesureUnit() + "]";
			lbyUnit.Text = "mV";
			label29.Text = "mV";
			chromDisplay_0.unitY = "mV";
			chromDisplay_0.txtY = "电压";
			chromDataGrid.SetGvRltsGnlHeaderText(chromDataGrid.gvRltsGnl);
			chromDataGrid.SetGvRltsGnlHeaderText(chromDataGrid.gvRltsGpc);
			chromDataGrid.SetGvRltsGnlHeaderText(chromDataGrid.gvRltsDad);
		}
		for (int i = 0; i < chromatogram.nrUserNames.Length; i++)
		{
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
				chromInfoR.DtcAcquisition.AcqRange = 10000f;
				chromInfoR.DtcAcquisition.AcqRate = 10f;
				chromInfoR.MtdFileName = Lang.PS("默认", "Default");
				chromatogram.canSetRs = true;
			}
			if (chromatogram.fullName.EndsWith(".cdf"))
			{
				ChromInfoR chromInfoR2 = chromatogram.chromInfoR;
				float val = chromatogram.signal._detector_maximum_value - chromatogram.signal._detector_minimum_value;
				val = Math.Max(10f, val);
				chromInfoR2.DtcAcquisition.AcqRange = val;
				chromInfoR2.DtcAcquisition.AcqRate = 1f / chromatogram.signal._actual_sampling_interval;
				chromInfoR2.MtdFileName = Lang.PS("默认", "Default");
				chromatogram.canSetRs = true;
			}
			ChkOverlayMode();
		}
		if (chromatogram.RltPeaks == null)
		{
			chromatogram.RltPeaks = chromatogram.GetRltPeaks(combine: false);
		}
		if (miFiOverlayMode.Checked)
		{
			Array.Resize(ref chromatogram_0, chromatogram_0.Length + 1);
		}
		else
		{
			Array.Resize(ref chromatogram_0, 1);
		}
		chromatogram_0[chromatogram_0.Length - 1] = chromatogram;
		chromatogram_1 = chromatogram;
		if (sampling)
		{
			float num = 0f;
			float num2 = 0f;
			float num3 = float.MaxValue;
			float num4 = float.MinValue;
			for (int j = 0; j < chromatogram_0.Length; j++)
			{
				Chromatogram chromatogram2 = chromatogram_0[j];
				num = Math.Min(num, chromatogram2.signal.xMinTime);
				num2 = Math.Max(num2, chromatogram2.signal.xMaxTime);
				num3 = Math.Min(num3, chromatogram2.signal.yMinValue);
				num4 = Math.Max(num4, chromatogram2.signal.yMaxValue);
			}
			chromDisplay_0.CalcuFullDisLg(ref disLg_0, num, num2, num3, num4);
			chromDisplay_0.stDisChain.AppendFrameLg(disLg_0);
			if (chromatogram.disLg.lgX > 0f)
			{
				chromDisplay_0.stDisChain.AppendFrameLg(chromatogram.disLg);
				mtbSigYBeg.Text = chromatogram.disLg.lgYBeg.ToString();
				mtbSigYEnd.Text = chromatogram.disLg.LgYEnd.ToString();
				mtbTime.Text = chromatogram.disLg.LgXEnd.ToString();
			}
		}
		else if (chromatogram_0.Length == 1)
		{
			chromDisplay_0.stDisChain.Clear();
			chromDisplay_0.SetFullDisLg(ref disLg_0, chromatogram.signal, second: true);
			float lgX = chromatogram.disLg.lgX;
			int num5 = (int)lgX;
			if (!float.IsNaN(lgX) && !float.IsInfinity(lgX) && (float)num5 > 0f)
			{
				chromDisplay_0.stDisChain.AppendFrameLg(chromatogram.disLg);
				mtbSigYBeg.Text = chromatogram.disLg.lgYBeg.ToString();
				mtbSigYEnd.Text = chromatogram.disLg.LgYEnd.ToString();
				mtbTime.Text = chromatogram.disLg.LgXEnd.ToString();
			}
			SetDisZoomButtonEnableState();
		}
		method_27(chromatogram_0, 0);
		SetSignalsColor();
		for (int k = 0; k < chromatogram.RltPeaks.Length; k++)
		{
			if (chromatogram.AdjustRltPeaksPara != null && k < chromatogram.AdjustRltPeaksPara.Length)
			{
				chromatogram.RltPeaks[k].name = chromatogram.AdjustRltPeaksPara[k].name;
			}
		}
		if (!chromatogram.mtdSetup.IsNull)
		{
			mstSetChromForm.ReadFromMtdMgr(chromatogram.mtdSetup);
		}
		chromDataGrid.tcChrom_SelectedIndexChanged(null, null);
		float istdAmount = CurChrom.chromInfo.GetIstdAmount(0);
		bool flag = CurChrom.chromInfo.cclCalcu != CalcuStyle.ISTD || (CurChrom.chromInfo.cclCalcu == CalcuStyle.ISTD && CurChrom.IstdNum <= 1);
		mstSetChromForm.tbcuIstdAmount.Enabled = flag;
		mstSetChromForm.tbcuIstdAmount.Text = (flag ? istdAmount.ToString() : "列表行输入");
		chromDisplay_0.showProgTemp = false;
		dpgnlChrom.Refresh();
	}

	public void addSignal(Signal signal)
	{
		chromatogram_1.signal = signal;
		chromatogram_0[0] = chromatogram_1;
		chromDisplay_0.curSignal = signal;
		chromDisplay_0.curSignal.disColor = Color.Red;
		float num = 0f;
		float num2 = 0f;
		float num3 = float.MaxValue;
		float num4 = float.MinValue;
		for (int i = 0; i < chromatogram_0.Length; i++)
		{
			Chromatogram chromatogram = chromatogram_0[i];
			num = Math.Min(num, chromatogram.signal.xMinTime);
			num2 = Math.Max(num2, chromatogram.signal.xMaxTime);
			num3 = Math.Min(num3, chromatogram.signal.yMinValue);
			num4 = Math.Max(num4, chromatogram.signal.yMaxValue);
		}
		chromDisplay_0.CalcuFullDisLg(ref disLg_0, num, num2, num3, num4);
		chromDisplay_0.stDisChain.AppendFrameLg(disLg_0);
		if (chromatogram_1.disLg.lgX > 0f)
		{
			chromDisplay_0.stDisChain.AppendFrameLg(chromatogram_1.disLg);
			mtbSigYBeg.Text = chromatogram_1.disLg.lgYBeg.ToString();
			mtbSigYEnd.Text = chromatogram_1.disLg.LgYEnd.ToString();
			mtbTime.Text = chromatogram_1.disLg.LgXEnd.ToString();
		}
		method_27(chromatogram_0, 0);
		SetSignalsColor();
		chromDataGrid.tcChrom_SelectedIndexChanged(null, null);
		form.Invoke((MethodInvoker)delegate
		{
			dpgnlChrom.Refresh();
		});
	}

	private void method_37(object sender, EventArgs e)
	{
	}

	private void UpdateButtonCheckState(bool bool_10)
	{
		if (object_0 != null)
		{
			if (object_0 is ToolStripButton)
			{
				(object_0 as ToolStripButton).Checked = bool_10;
			}
			if (object_0 is ToolStripMenuItem)
			{
				(object_0 as ToolStripMenuItem).Checked = bool_10;
			}
		}
	}

	private void rbasAdd_Click(object sender, EventArgs e)
	{
		if (HasChrom)
		{
			CurChrom.Process(InstruStyle.GC);
			chromDataGrid.tcChrom_SelectedIndexChanged(null, null);
		}
	}

	private bool CheckAuthority(bool bool_10)
	{
		bool flag = false;
		if (bool_10 && flag)
		{
			MessageBox.Show(Lang.PS("受限！", "No Right！"));
		}
		return flag;
	}

	public void ReadWinInfo(WinInfo winInfo)
	{
	}

	private void method_40(float float_8, float float_9, float float_10, float float_11)
	{
		if (float_9 >= 0.001f && float_11 >= 0.001f)
		{
			disLg_0.lgXBeg = float_8;
			disLg_0.lgX = float_9;
			disLg_0.lgYBeg = float_10;
			disLg_0.lgY = float_11;
			chromDisplay_0.stDisChain.AppendFrameLg(disLg_0);
		}
	}

	public void refresh_once()
	{
		chromDisplay_0.instruStyle = InstruStyle.GC;
		SuspendLayout();
		detectorStyle_0 = DetectorStyle.General;
		SetExplainText();
		chromDisplay_0.displayPanel = dpgnlChrom;
		openFileDialog_2.Filter = CaliGnlUserCtrl.Filter;
		chromDataGrid.refresh_once();
		ResumeLayout();
		splitContainer_SplitterMoved(null, null);
		if (lclGvRltsGnl.Columns.Contains("RetenTime"))
		{
			chromDisplay_0.fmtPeakRT = lclGvRltsGnl.ConvertValFmt("RetenTime");
		}
		chromDisplay_0.ExtDraw_begin();
		for (int i = 0; i < chromatogram_0.Length; i++)
		{
			chromatogram_0[i].CalcuResults(InstruStyle.GC);
		}
		if (chromatogram_0.Length != 0)
		{
			chromDataGrid.tcChrom_SelectedIndexChanged(null, null);
		}
	}

	private void method_49()
	{
		sst_0.Calcu(chromatogram_0);
	}

	private void SetDisZoomButtonEnableState()
	{
		ToolStripMenuItem toolStripMenuItem = miDisPreviousZoom;
		bool enabled = (btnPreviousZoom.Enabled = chromDisplay_0.stDisChain.HasPrevious);
		toolStripMenuItem.Enabled = enabled;
		ToolStripMenuItem toolStripMenuItem2 = miDisNextZoom;
		enabled = (btnNextZoom.Enabled = chromDisplay_0.stDisChain.HasNext);
		toolStripMenuItem2.Enabled = enabled;
	}

	private void SetExplainText()
	{
		if (base.Parent.GetType() == typeof(ChromForm))
		{
			base.Parent.Text = Lang.PS("谱图处理", "Chrom. Process") + ((chromatogram_1 != null) ? (": " + chromatogram_1.fullName) : "");
		}
	}

	public string RetSstItem(Peak peak, string item)
	{
		float value = SstItem.getValue(peak, item, sst_0.sstParas.criterion);
		return "";
	}

	public string RetSstItem(SstItem sstItem, int rowIndex)
	{
		return "";
	}

	private void SaveChromFile(string strFilePath)
	{
		if (HasChrom)
		{
			UserArchive userArchive;
			if (CurChrom.userArchives.Length == 0)
			{
				Array.Resize(ref CurChrom.userArchives, 1);
				userArchive = (CurChrom.userArchives[0] = new UserArchive());
			}
			else
			{
				userArchive = CurChrom.userArchives[0];
			}
			string asChrom = CurChrom.userArchives[0].chromInfo.asChrom;
			userArchive.integ.LoadFromObject(CurChrom.integ);
			userArchive.SL_lbTexts(load: true, ref CurChrom.signal.lbTexts);
			userArchive.SL_lbLines(load: true, ref CurChrom.signal.lbLines);
			userArchive.userName = "";
			userArchive.chromInfo.LoadFromObject(CurChrom.chromInfo);
			userArchive.chromInfo.asChrom = asChrom;
			CurChrom.AdjustRltPeaksPara = CurChrom.RltPeaks;
			CurChrom.mtdSetup = mstSetChromForm.CurMtdMgr;
			CurChrom.SaveToFile(strFilePath);
			SetExplainText();
		}
	}

	public void SetChromsLink(ref Chromatogram[] chroms, ref int activeNo, ref SST sst_1)
	{
		if (dlgReportSetup == null)
		{
			dlgReportSetup = new RptSetupDlg();
		}
		chroms = chromatogram_0;
		sst_1 = sst_0;
	}

	public void SetProjectDir(string projectDir)
	{
	}

	public void SetSignalsColor()
	{
		for (int i = 0; i < chromatogram_0.Length; i++)
		{
			if (i < 12)
			{
				chromatogram_0[i].signal.disColor = options_0.sgColors[i];
			}
		}
	}

	private void method_56(LclGridView lclGridView_2, GcProgTemp gcProgTemp_0)
	{
		for (int i = 0; i < gcProgTemp_0.progTempRows.Length; i++)
		{
			lclGridView_2.Rows[i].Cells[0].Value = gcProgTemp_0.progTempRows[i].upRate.ToString();
			lclGridView_2.Rows[i].Cells[1].Value = gcProgTemp_0.progTempRows[i].endTemp.ToString();
			lclGridView_2.Rows[i].Cells[2].Value = gcProgTemp_0.progTempRows[i].holdTime.ToString();
		}
	}

	private void splitContainer_SplitterMoved(object sender, SplitterEventArgs e)
	{
		if (chromDataGrid.lbExpress.Height == 1)
		{
			Graphics graphics = Graphics.FromHwnd(chromDataGrid.lbExpress.Handle);
			SizeF sizeF = graphics.MeasureString("检测", chromDataGrid.lbExpress.Font);
			chromDataGrid.lbExpress.Height = Convert.ToInt32(sizeF.Height) + 1;
			graphics.Dispose();
		}
		if (!dpgnlChrom.Visible)
		{
		}
	}

	private void method_59(bool bool_10, int int_16)
	{
		if (bool_10)
		{
			method_27(chromatogram_0, int_16);
		}
		else
		{
			SetSignalsColor();
			chromDisplay_0.RefreshSignalLabels = true;
		}
		chromDataGrid.tcChrom_SelectedIndexChanged(null, null);
	}

	private void method_60(object sender, KeyEventArgs e)
	{
		if ((Keys.D0 <= e.KeyCode && e.KeyCode <= Keys.D9) || (Keys.NumPad0 <= e.KeyCode && e.KeyCode <= Keys.NumPad9) || e.KeyCode == Keys.Back || e.KeyCode == Keys.Delete)
		{
			bool_1 = true;
		}
	}

	private void method_61(object sender, KeyPressEventArgs e)
	{
		if (!bool_1)
		{
			e.Handled = true;
		}
		bool_1 = false;
	}

	private void mtbSigYBeg_DoubleClick(object sender, EventArgs e)
	{
		if (sender != null)
		{
			if (((MaskedTextBox)sender).Text.Trim() == "" || (sender == mtbSigYBeg && mtbSigYBeg.Text == "-") || (sender == mtbSigYEnd && mtbSigYEnd.Text == "-"))
			{
				return;
			}
			if (sender == mtbSigYBeg)
			{
				string text = mtbSigYBeg.Text.Trim();
				if (text.Substring(text.Length - 1, 1) == ".")
				{
					return;
				}
			}
			if (sender == mtbSigYEnd)
			{
				string text2 = mtbSigYEnd.Text.Trim();
				if (text2.Substring(text2.Length - 1, 1) == ".")
				{
					return;
				}
			}
		}
		float num = Class49.String2Float(mtbTime.Text, disLg_0.lgX);
		float num2 = Class49.String2Float(mtbSigYBeg.Text, disLg_0.lgYBeg);
		float num3 = Class49.String2Float(mtbSigYEnd.Text, disLg_0.lgYBeg + disLg_0.lgY);
		if (sender == mtbTime)
		{
			if (num == disLg_0.lgX)
			{
				mtbTime.Text = disLg_0.lgX.ToString("0.0");
			}
			if (num <= 0f)
			{
				mtbTime.Text = disLg_0.lgX.ToString("0.0");
			}
		}
		if (sender == mtbSigYBeg && num2 == disLg_0.lgYBeg)
		{
			mtbSigYBeg.Text = disLg_0.lgYBeg.ToString("0.0");
		}
		if (sender == mtbSigYEnd && num3 == disLg_0.lgYBeg + disLg_0.lgY)
		{
			mtbSigYEnd.Text = (disLg_0.lgYBeg + disLg_0.lgY).ToString("0.0");
		}
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

	private void mtbSigYBeg_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode == Keys.Return)
		{
			mtbSigYBeg_DoubleClick(null, null);
		}
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
					System.Drawing.Rectangle bounds = toolStripTextBox.Bounds;
					bounds.Offset(-1, -1);
					bounds.Width += 3;
					e.Graphics.DrawRectangle(Pens.Gray, bounds);
				}
			}
		}
	}

	public void WriteWinInfo(WinInfo winInfo)
	{
	}

	private void method_63(Control control_0)
	{
		Bitmap bitmap = new Bitmap(control_0.Width, control_0.Height);
		Graphics graphics = Graphics.FromImage(bitmap);
		control_0.DrawToBitmap(bitmap, control_0.ClientRectangle);
		graphics.DrawImage(bitmap, control_0.Location);
		Clipboard.SetImage(bitmap);
		bitmap.Dispose();
	}

	private void method_64(object sender, EventArgs e)
	{
		FileInfo fileInfo_ = null;
		FileStream fileStream_ = null;
		BinaryReader binaryReader_ = null;
		try
		{
			string filePath = "AAA.rst";
			Class49.OpenBinaryReader(filePath, out fileInfo_, out fileStream_, out binaryReader_);
			Program.WriteLine(binaryReader_.ReadString());
			Program.WriteLine(binaryReader_.ReadString());
		}
		finally
		{
			Class49.FileStreamClose(ref fileStream_, ref binaryReader_);
		}
	}

	private void toolStripButton1_Click(object sender, EventArgs e)
	{
	}

	private void btnProperties_Click(object sender, EventArgs e)
	{
	}

	private void miFiExit_Click(object sender, EventArgs e)
	{
	}

	private void toolStripMenuItem1_Click(object sender, EventArgs e)
	{
		if (HasChrom && !CheckAuthority(bool_10: true))
		{
			if (saveFileDialog_0 == null)
			{
				saveFileDialog_0 = new SaveFileDialog();
				saveFileDialog_0.Filter = "(*.tab)|*.tab";
			}
			if (saveFileDialog_0.ShowDialog() != DialogResult.Cancel)
			{
				SaveChromFile(saveFileDialog_0.FileName);
			}
		}
	}

	private void toolStripMenuItem2_Click(object sender, EventArgs e)
	{
		CaliGnlForm caliGnlForm = new CaliGnlForm();
		caliGnlForm.Show();
	}

	private void tbsiSample_TextChanged(object sender, EventArgs e)
	{
	}

	private void cChangePeak_KeyPress(object sender, KeyPressEventArgs e)
	{
	}

	private void mstSetChromForm_OnMethodSaveEvent(object sender, EventArgs e)
	{
		if (CurChrom != null)
		{
			if (mstSetChromForm.CurMtdMgr != null)
			{
				CurChrom.mtdSetup = mstSetChromForm.CurMtdMgr.Copy();
			}
			CurChrom.Process(InstruStyle.LC);
			chromDataGrid.peakArray = chromatogram_0[0].GetPeakFromCompound();
			chromDataGrid.tcChrom_SelectedIndexChanged(null, null);
		}
	}

	private void mstSetChromForm_OnUseSet(object sender, EventArgs e)
	{
	}

	private void mstSetChromForm_OnAddAllCompnent(object sender, EventArgs e)
	{
		mstSetChromForm.CurMtdMgr.caliGnl.Clear();
		for (int i = 0; i < CurChrom.signal.PeaksNum; i++)
		{
			Peak peak = CurChrom.signal.peaks[i];
			if (peak.GasAmount == 0f)
			{
				MessageBox.Show("请检查标气浓度");
				return;
			}
			mstSetChromForm.CurMtdMgr.caliGnl.add_splLevel(checkExists: false, canAddNew: true, peak.name, 0, peak.pkRT, peak.area, peak.height, peak.GasAmount);
		}
		if (mstSetChromForm.CurMtdMgr != null)
		{
			if (File.Exists(mstSetChromForm.CurMtdMgr.strMtdFilePath))
			{
				mstSetChromForm.CurMtdMgr.SaveToFile();
			}
			mstSetChromForm.ReadComponentData();
		}
	}

	private void method_77(Graphics graphics_0, GvPrtInfos gvPrtInfos_1, string[] string_289, int int_16, int int_17, int int_18, float float_8, ref float float_9)
	{
		string[] array = gvPrtInfos_1.colNames[int_17];
		float[] array2 = gvPrtInfos_1.colWidths[int_17];
		float num = float_8;
		StringAlignment stringAlignment_ = StringAlignment.Far;
		for (int i = 0; i < array.Length; i++)
		{
			method_80(graphics_0, string_289[i], num, float_9, array2[i], stringAlignment_);
			num += array2[i];
		}
		float_9 += float_7;
	}

	private void method_78(Graphics graphics_0, string string_289, float float_8, float float_9, float float_10, float float_11)
	{
		method_82(graphics_0, float_8, float_9, float_11);
		method_82(graphics_0, float_8 + float_10, float_9, float_11);
		method_79(graphics_0, string_289, float_8, float_9, float_10, float_11);
	}

	private void method_79(Graphics graphics_0, string string_289, float float_8, float float_9, float float_10, float float_11)
	{
		rectangleF_1.X = float_8;
		rectangleF_1.Y = float_9;
		rectangleF_1.Width = float_10;
		rectangleF_1.Height = float_11;
		solidBrush_0.Color = Color.Black;
		stringFormat_0.Alignment = StringAlignment.Center;
		graphics_0.DrawString(string_289, font_0, solidBrush_0, rectangleF_1, stringFormat_0);
	}

	private void method_80(Graphics graphics_0, string string_289, float float_8, float float_9, float float_10, StringAlignment stringAlignment_0)
	{
		rectangleF_1.X = float_8;
		rectangleF_1.Y = float_9;
		rectangleF_1.Width = float_10;
		rectangleF_1.Height = float_7;
		solidBrush_0.Color = Color.Black;
		stringFormat_0.Alignment = stringAlignment_0;
		graphics_0.DrawString(string_289, font_0, solidBrush_0, rectangleF_1, stringFormat_0);
	}

	private void method_81(Graphics graphics_0, bool bool_10, float float_8, float float_9, ref float float_10)
	{
		float_10 += 2f;
		int int_ = ((!bool_10) ? 1 : 2);
		method_83(graphics_0, float_8, float_10, float_9, int_);
		float_10 += 2f;
	}

	private void method_82(Graphics graphics_0, float float_8, float float_9, float float_10)
	{
		pen_0.Color = Color.Black;
		graphics_0.DrawLine(pen_0, float_8, float_9, float_8, float_9 + float_10);
	}

	private void method_83(Graphics graphics_0, float float_8, float float_9, float float_10, int int_16)
	{
		pen_0.Color = Color.Black;
		pen_0.Width = int_16;
		graphics_0.DrawLine(pen_0, float_8, float_9, float_8 + float_10, float_9);
		pen_0.Width = 1f;
	}

	private void method_84(Graphics graphics_0, string string_289, bool bool_10, float float_8, float float_9, System.Drawing.Font font_2)
	{
		graphics_0.DrawString(string_289, font_2, solidBrush_0, float_8, float_9);
	}

	private void method_85(Graphics graphics_0)
	{
		SizeF sizeF = graphics_0.MeasureString("中国", font_0);
		SizeF sizeF2 = graphics_0.MeasureString("中国", font_0);
		float_3 = sizeF.Height;
		float_7 = sizeF2.Height;
		float_4 = graphics_0.MeasureString("中国\n[..]", font_0).Height;
		float_6 = (float)rectangle_1.Top - float_7 - 10f - 10f;
		float_5 = rectangle_1.Bottom + 10 + 10;
	}

	private void btnXYFull_Click(object sender, EventArgs e)
	{
		if (HasChrom)
		{
			chromatogram_1.signal.refresh_TimeValue();
			float num = chromatogram_1.signal.yMaxValue - chromatogram_1.signal.yMinValue;
			num *= 0.02f;
			mtbSigYBeg.Text = (chromatogram_1.signal.yMinValue - num).ToString("0.00");
			mtbSigYEnd.Text = (chromatogram_1.signal.yMaxValue + num).ToString("0.00");
			mtbSigYBeg_DoubleClick(null, null);
		}
	}

	private void btnTimeFull_Click(object sender, EventArgs e)
	{
		if (HasChrom)
		{
			chromatogram_1.signal.refresh_TimeValue();
			mtbTime.Text = chromatogram_1.signal.xMaxTime.ToString("0.0");
			mtbSigYBeg_DoubleClick(null, null);
		}
	}

	private void toolStripButton8_Click(object sender, EventArgs e)
	{
	}

	private void bExportXY_Click(object sender, EventArgs e)
	{
		if (HasChrom)
		{
			int num = CurChrom.signal.dots.Length;
			string text = System.Windows.Forms.Application.StartupPath + "\\CChromData.txt";
			FileStream fileStream = new FileStream(text, FileMode.OpenOrCreate);
			StreamWriter streamWriter = new StreamWriter(fileStream, Encoding.Default);
			for (int i = 0; i < num; i++)
			{
				string text2 = CurChrom.signal.dots[i].X.ToString("0.000000");
				string text3 = CurChrom.signal.dots[i].Y.ToString("0.000000");
				streamWriter.Write(text2 + "," + text3 + "\r\n");
			}
			streamWriter.Close();
			fileStream.Close();
			Process.Start(text);
		}
	}

	private void btUpdataByXX_Click(object sender, EventArgs e)
	{
		for (int i = 0; i < CurChrom.RltPeaks.Length; i++)
		{
			if (i == CurChrom.caliGnl.UpDataPeakIndex)
			{
				float areaPer = CurChrom.RltPeaks[i].areaPer;
				float num = 0.16f * (areaPer * 100f) + 4f;
				if (num < 4f)
				{
					num = 4f;
				}
				if (num > 20f)
				{
					num = 20f;
				}
				string s = "#010+" + num.ToString("00.000") + ".";
				byte[] bytes = Encoding.Default.GetBytes(s);
				bytes[bytes.Length - 1] = 13;
				if (formMain != null)
				{
					formMain.ModbusComSendData(bytes);
				}
				areaPer = CurChrom.RltPeaks[i].amount;
				num = 16f / (sysParam.fDcsMaxValue - sysParam.fDcsMinValue) * (areaPer - sysParam.fDcsMinValue) + 4f;
				if (num < 4f)
				{
					num = 4f;
				}
				if (num > 20f)
				{
					num = 20f;
				}
				s = "#011+" + num.ToString("00.000") + ".";
				byte[] bytes2 = Encoding.Default.GetBytes(s);
				bytes2[bytes.Length - 1] = 13;
				if (formMain != null)
				{
					formMain.ModbusComSendData(bytes2);
				}
			}
		}
	}

	private void cbNMHC_CheckedChanged(object sender, EventArgs e)
	{
	}

	private void ChromForm_FormClosing(object sender, FormClosingEventArgs e)
	{
		form = null;
	}

	private void ChromForm_FormClosed(object sender, FormClosedEventArgs e)
	{
		GC.Collect();
	}

	private void toolStripButton6_Click(object sender, EventArgs e)
	{
	}

	private void btnSave_Click(object sender, EventArgs e)
	{
		if (Class49.user_0.ULevel != User.Level.访问员)
		{
			if (HasChrom)
			{
				tbsiSample_TextChanged(null, new KeyPressEventArgs(Convert.ToChar(13)));
				if (CurChrom.fullName.EndsWith(".sda"))
				{
					SaveChromFile(CurChrom.fullName);
					Class49.InsertIntoTable(Class49.string_9[2], Class49.user_0.u_name, "", "保存谱图", "保存谱图:" + CurChrom.fullName);
				}
				else
				{
					miFiSaveAs_Click(null, null);
				}
			}
		}
		else
		{
			MessageBox.Show(Lang.PS("没有保存谱图权限！", "Do not save spectrum access!"));
		}
	}

	public void saveFile()
	{
		if (Class49.user_0.ULevel != User.Level.访问员 && HasChrom && CurChrom.fullName.EndsWith(".sda"))
		{
			SaveChromFile(CurChrom.fullName);
			Class49.InsertIntoTable(Class49.string_9[2], Class49.user_0.u_name, "", "保存谱图", "保存谱图:" + CurChrom.fullName);
		}
	}

	private void miFiSaveAs_Click(object sender, EventArgs e)
	{
		if (Class49.user_0.ULevel != User.Level.访问员)
		{
			if (HasChrom && !CheckAuthority(bool_10: true))
			{
				if (saveFileDialog_0 == null)
				{
					saveFileDialog_0 = new SaveFileDialog();
					saveFileDialog_0.Filter = "(*.sda)|*.sda";
				}
				if (saveFileDialog_0.ShowDialog() != DialogResult.Cancel)
				{
					SaveChromFile(saveFileDialog_0.FileName);
					Class49.InsertIntoTable(Class49.string_9[2], Class49.user_0.u_name, "", "另存谱图", "另存谱图:" + saveFileDialog_0.FileName);
				}
			}
		}
		else
		{
			MessageBox.Show(Lang.PS("没有保存谱图权限！", "Do not save spectrum access!"));
		}
	}

	private void miMtdSaveTplt_Click(object sender, EventArgs e)
	{
		try
		{
			Process.Start(System.Windows.Forms.Application.StartupPath.ToString() + "\\Help.chm");
		}
		catch
		{
		}
	}

	private void gvRltsGnl_Enter(object sender, EventArgs e)
	{
		Class49.smethod_40(((Control)sender).Handle);
	}

	private void dpgpcChrom_Click(object sender, EventArgs e)
	{
		if (!HasChrom)
		{
			return;
		}
		if (dpgnlChrom.Cursor == Cursors.IBeam)
		{
			lbTextDlg_0.ShowDialog();
			if (lbTextDlg_0.lbText != null)
			{
				lbTextDlg_0.lbText.pointF_0 = chromDisplay_0.ClickLgV();
				CurChrom.signal.AddLbText(lbTextDlg_0.lbText);
			}
		}
		CurChrom.signal.ResetSelectLbs();
		CurChrom.signal.ClickLb(point_1);
		int num = chromDisplay_0.GraphClick();
		for (int i = 0; i < CurChrom.PeaksNum; i++)
		{
			CurChrom.RltPeaks[i].selected = CurChrom.RltPeaks[i].pkN == num;
		}
		chromDataGrid.tcChrom_SelectedIndexChanged(null, null);
	}

	private void dpgpcChrom_MouseDown(object sender, MouseEventArgs e)
	{
		if (!HasChrom)
		{
			return;
		}
		PointF pointF;
		int num2;
		int num;
		if (e.Button == MouseButtons.Left)
		{
			lbLineDlg_0.pointF_0 = chromDisplay_0.scrToLg(e.Location, bool_0: true);
			chromDisplay_0.ptScaleBegin = e.Location;
			if (byte_0 != 0)
			{
				if (integRow_2.oprtStyle == IntegOprtStyle.SolventPeak)
				{
					pointF = new PointF
					{
						X = integRow_2.timeA,
						Y = chromatogram_1.signal.getDotY(integRow_2.timeA)
					};
					num = -1;
					num2 = 0;
					while (num2 < chromatogram_1.RltPeaks.Length)
					{
						Peak peak = chromatogram_1.RltPeaks[num2];
						if (pointF.X <= peak.startT || pointF.X >= peak.endT)
						{
							num2++;
							continue;
						}
						goto IL_0111;
					}
				}
				byte_0++;
			}
			goto IL_02c6;
		}
		if (e.Button == MouseButtons.Right)
		{
			chromDisplay_0.mouseLocation = (point_2 = e.Location);
			if (byte_0 != 0)
			{
				EndManualDrawLine(bool_10: false);
			}
		}
		return;
		IL_02c6:
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
				chromDataGrid.lbExpress.Text = integRow_2.ExpString(byte_0);
			}
			Cursor.Position = new System.Drawing.Point(Cursor.Position.X + 10, Cursor.Position.Y);
		}
		if (byte_0 != 3)
		{
			return;
		}
		if (integRow_2.oprtStyle != IntegOprtStyle.PkVale)
		{
			integRow_2.ArrTime();
		}
		EndManualDrawLine(bool_10: true);
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
			manuDlg_0.RefreshValue(mstSetChromForm.gvInteg, integRow_2);
			manuDlg_0.Visible = true;
		}
		else
		{
			method_30();
		}
		return;
		IL_0111:
		num = num2;
		if (num == -1)
		{
			EndManualDrawLine(bool_10: true);
			return;
		}
		IntegRow integRow = new IntegRow
		{
			oprtStyle = IntegOprtStyle.PkCut,
			timeA = chromatogram_1.RltPeaks[num].startT,
			timeB = chromatogram_1.RltPeaks[num].endT
		};
		CurChrom.integ.AppendRow(integRow);
		IntegRow integRow2 = new IntegRow
		{
			oprtStyle = IntegOprtStyle.PkAddPosi,
			timeA = chromatogram_1.RltPeaks[num].startT,
			timeB = pointF.X
		};
		CurChrom.integ.AppendRow(integRow2);
		IntegRow integRow3 = new IntegRow
		{
			oprtStyle = IntegOprtStyle.PkAddPosi,
			timeA = pointF.X - 0.05f,
			timeB = chromatogram_1.RltPeaks[num].endT
		};
		CurChrom.integ.AppendRow(integRow3);
		IntegRow integRow4 = new IntegRow
		{
			oprtStyle = IntegOprtStyle.BsTogether,
			timeA = chromatogram_1.RltPeaks[num].startT - 0.01f,
			timeB = chromatogram_1.RltPeaks[num].endT + 0.01f
		};
		CurChrom.integ.AppendRow(integRow4);
		EndManualDrawLine(bool_10: true);
		method_30();
		goto IL_02c6;
	}

	private void dpgpcChrom_MouseLeave(object sender, EventArgs e)
	{
		if (byte_0 != 0)
		{
			rectangle_0.Width = dpgnlChrom.Width - 10;
			if (!rectangle_0.Contains(point_0))
			{
				EndManualDrawLine(bool_10: true);
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
				if (dpgnlChrom.Cursor == Cursors.Hand && float_0 > 0f)
				{
					float_1 = chromDisplay_0.scrToLg(e.Location, bool_0: true).X;
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
		DisDpRefresh();
		if (chromDisplay_0.curSignal != null)
		{
			for (int i = 0; i < chromDisplay_0.curSignal.peaks.Length; i++)
			{
				Peak peak = chromDisplay_0.curSignal.peaks[i];
				if (peak.disNo >= 0)
				{
					PointF pointF = chromDisplay_0.scrToLg(e.Location, bool_0: true);
					if ((Math.Abs(pointF.X - peak.startT) < disLg_0.LgXEnd / 100f) & (Math.Abs(pointF.Y - peak.startV) < disLg_0.LgYEnd / 100f))
					{
						chromDisplay_0.displayPanel.Cursor = Cursors.Hand;
						float_0 = pointF.X;
						break;
					}
					if ((Math.Abs(pointF.X - peak.endT) < disLg_0.LgXEnd / 100f) & (Math.Abs(pointF.Y - peak.endV) < disLg_0.LgYEnd / 100f))
					{
						chromDisplay_0.displayPanel.Cursor = Cursors.Hand;
						float_0 = pointF.X;
						break;
					}
					chromDisplay_0.displayPanel.Cursor = Cursors.Default;
					if (float_0 > 0f && float_1 > 0f)
					{
						IntegRow integRow = new IntegRow
						{
							oprtStyle = IntegOprtStyle.PkVale,
							timeA = float_0,
							timeB = float_1
						};
						CurChrom.integ.AppendRow(integRow);
						float_0 = -1f;
						float_1 = -1f;
						method_28();
					}
				}
			}
		}
		if (byte_0 == 1)
		{
			integRow_2.timeA = chromDisplay_0.DrawL(e.X, 1);
		}
		if (byte_0 == 2)
		{
			chromDisplay_0.DrawL_add();
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
			lbLineDlg_0.ShowDialog();
			lbLineDlg_0.lbLine.pointF_2 = chromDisplay_0.scrToLg(e.Location, bool_0: true);
			CurChrom.signal.AddLbLine(lbLineDlg_0.lbLine);
			DisDpRefresh();
		}
		chromDisplay_0.displayPanel.Cursor = Cursors.Default;
		if (chromDisplay_0.moving)
		{
			DisDpRefresh();
			SetDisZoomButtonEnableState();
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
					method_40(rectangleF_0.X, rectangleF_0.Width, rectangleF_0.Y, rectangleF_0.Height);
				}
				chromDisplay_0.scaling = false;
				DisDpRefresh();
				SetDisZoomButtonEnableState();
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
		if (chromDisplay_0 != null)
		{
			chromDisplay_0.Draw(e.Graphics, erase: true);
		}
	}

	private void dpgnlChrom_Resize(object sender, EventArgs e)
	{
		chromDataGrid.lbExpress.Width = dpgnlChrom.Width - 2;
	}

	public string gvSummaryComValue(Chromatogram chrom, string columnName)
	{
		return chromDataGrid.gvSummaryComValue(chrom, columnName);
	}

	public string gvSummarySmyValue(Chromatogram chrom, string cmpdName, string columnName)
	{
		return chromDataGrid.gvSummarySmyValue(chrom, cmpdName, columnName);
	}

	public string gvPerformFrom50Value(Peak peak, string columnName)
	{
		return chromDataGrid.gvPerformFrom50Value(peak, columnName);
	}

	public string gvRltsValue(Peak peak, string columnName, string string_289, bool combine)
	{
		return chromDataGrid.gvRltsValue(peak, columnName, string_289, combine);
	}

	public string getSmyHeaderText(string name)
	{
		return chromDataGrid.getSmyHeaderText(name);
	}

	public void GetRltDisColumns(ref GvInfos gvInfos)
	{
		chromDataGrid.GetRltDisColumns(ref gvInfos);
	}

	public void GetSmyColumns(Chromatogram[] chroms, ref GvInfos gvInfos, ref SmyHdrPara smyHdrPara)
	{
		chromDataGrid.GetSmyColumns(chroms, ref gvInfos, ref smyHdrPara);
	}

	public void GetItgDisColumns(ref GvInfos gvInfos)
	{
		chromDataGrid.GetItgDisColumns(ref gvInfos);
	}

	public void GetPfmDisColumns(ref GvInfos gvInfos)
	{
		chromDataGrid.GetPfmDisColumns(ref gvInfos);
	}

	public void GetSstDisColumns(ref GvInfos gvInfos)
	{
		chromDataGrid.GetSstDisColumns(ref gvInfos);
	}

	private void BtnGnl_Click(object sender, EventArgs e)
	{
		if (HasChrom)
		{
			if (cdlMgr.ChartParaOperaList == null)
			{
				return;
			}
			Peak[] rltPeaks = chromatogram_1.RltPeaks;
			CaliGnl caliGnl = cdlMgr.ChartParaOperaList[0].mtdMgr.caliGnl;
			int num = 0;
			int num2 = 0;
			for (num = 0; num < caliGnl.cmpds.Count(); num++)
			{
				num2 = 0;
				while (1 <= rltPeaks.Count() && num2 < rltPeaks.Count())
				{
					if (rltPeaks[num2].pkRT >= caliGnl.cmpds[num].cmpdInfo.retainTime - caliGnl.cmpds[num].cmpdInfo.leftWindow && rltPeaks[num2].pkRT <= caliGnl.cmpds[num].cmpdInfo.retainTime + caliGnl.cmpds[num].cmpdInfo.rightWindow)
					{
						caliGnl.cmpds[num].levels[0].responseA = rltPeaks[num2].area;
						caliGnl.cmpds[num].levels[0].LastAddresponseA = caliGnl.cmpds[num].levels[0].responseA;
						caliGnl.CalculateFunc(appendLink: false);
						caliGnl.cmpds[num].levels[0].respFactor = caliGnl.cmpds[num].levels[0].amount / caliGnl.cmpds[num].levels[0].responseA;
						caliGnl.cmpds[num].cmpdInfo.freeRespFactor = caliGnl.cmpds[num].levels[0].respFactor;
					}
					num2++;
				}
			}
			caliGnl.SaveFile(cdlMgr.ChartParaOperaList[0].mtdMgr.chromInfo.cclCalibration);
			cdlMgr.ChartParaOperaList[0].mtdMgr.SaveToFile();
			cdlMgr.formMain.MainmstSet.ReadFromMtdMgr();
			cdlMgr.formMain.MainmstSet.UsePara();
			cdlMgr.formMain.MainmstSet.ReadFromMtdMgr();
			cdlMgr.formMain.MainmstSet.bUseSet_Click(null, null);
			MessageBox.Show("重新标定完成！");
		}
		else
		{
			MessageBox.Show("请先加载谱图！");
		}
	}

	private void BtnGnl2_Click(object sender, EventArgs e)
	{
		if (HasChrom)
		{
			if (cdlMgr.ChartParaOperaList == null)
			{
				return;
			}
			Peak[] rltPeaks = chromatogram_1.RltPeaks;
			CaliGnl caliGnl = cdlMgr.ChartParaOperaList[1].mtdMgr.caliGnl;
			int num = 0;
			int num2 = 0;
			for (num = 0; num < caliGnl.cmpds.Count(); num++)
			{
				num2 = 0;
				while (1 <= rltPeaks.Count() && num2 < rltPeaks.Count())
				{
					if (rltPeaks[num2].pkRT >= caliGnl.cmpds[num].cmpdInfo.retainTime - caliGnl.cmpds[num].cmpdInfo.leftWindow && rltPeaks[num2].pkRT <= caliGnl.cmpds[num].cmpdInfo.retainTime + caliGnl.cmpds[num].cmpdInfo.rightWindow)
					{
						caliGnl.cmpds[num].levels[0].responseA = rltPeaks[num2].area;
						caliGnl.cmpds[num].levels[0].LastAddresponseA = caliGnl.cmpds[num].levels[0].responseA;
						caliGnl.CalculateFunc(appendLink: false);
						caliGnl.cmpds[num].levels[0].respFactor = caliGnl.cmpds[num].levels[0].amount / caliGnl.cmpds[num].levels[0].responseA;
						caliGnl.cmpds[num].cmpdInfo.freeRespFactor = caliGnl.cmpds[num].levels[0].respFactor;
					}
					num2++;
				}
			}
			caliGnl.SaveFile(cdlMgr.ChartParaOperaList[0].mtdMgr.chromInfo.cclCalibration);
			cdlMgr.ChartParaOperaList[0].mtdMgr.SaveToFile();
			cdlMgr.formMain.MainmstSet.ReadFromMtdMgr();
			cdlMgr.formMain.MainmstSet.UsePara();
			cdlMgr.formMain.MainmstSet.ReadFromMtdMgr();
			cdlMgr.formMain.MainmstSet.bUseSet_Click(null, null);
			MessageBox.Show("重新标定完成！");
		}
		else
		{
			MessageBox.Show("请先加载谱图！");
		}
	}

	private void btnConvert_Click(object sender, EventArgs e)
	{
		FormAllHydr formAllHydr = new FormAllHydr();
		formAllHydr.TopMost = true;
		formAllHydr.StartPosition = FormStartPosition.CenterScreen;
		formAllHydr.Show();
	}

	private void btnUploading_Click(object sender, EventArgs e)
	{
		byte b = 0;
		int num = 0;
		float[] array = new float[1];
		ushort[] dst = new ushort[2];
		array = new float[1];
		Buffer.BlockCopy(array, 0, dst, 0, 4);
		FormRltReminder formRltReminder = new FormRltReminder(CurChrom.RltPeaks, OnlineCtrl.selfCtrl.indexChannelStart);
		formRltReminder.StartPosition = FormStartPosition.CenterScreen;
		formRltReminder.TopMost = true;
		formRltReminder.Show();
	}

	protected override void Dispose(bool disposing)
	{
		if (disposing && icontainer_0 != null)
		{
			icontainer_0.Dispose();
		}
		base.Dispose(disposing);
	}

	private void InitializeComponent()
	{
		this.components = new System.ComponentModel.Container();
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(IBrainChrom2018.MineChromCtrl));
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
		this.msChrom = new System.Windows.Forms.MenuStrip();
		this.miFile = new System.Windows.Forms.ToolStripMenuItem();
		this.miFiOverlayMode = new System.Windows.Forms.ToolStripMenuItem();
		this.miFiOpen = new System.Windows.Forms.ToolStripMenuItem();
		this.miFiClose = new System.Windows.Forms.ToolStripMenuItem();
		this.miFiCloseAll = new System.Windows.Forms.ToolStripMenuItem();
		this.miFiSave = new System.Windows.Forms.ToolStripMenuItem();
		this.miFiSaveAs = new System.Windows.Forms.ToolStripMenuItem();
		this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
		this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
		this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
		this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
		this.miFiPreview = new System.Windows.Forms.ToolStripMenuItem();
		this.miFiPrint = new System.Windows.Forms.ToolStripMenuItem();
		this.miFiExit = new System.Windows.Forms.ToolStripMenuItem();
		this.miDisplay = new System.Windows.Forms.ToolStripMenuItem();
		this.miDisPreviousZoom = new System.Windows.Forms.ToolStripMenuItem();
		this.miDisNextZoom = new System.Windows.Forms.ToolStripMenuItem();
		this.miDisUnzoom = new System.Windows.Forms.ToolStripMenuItem();
		this.miChromatogram = new System.Windows.Forms.ToolStripMenuItem();
		this.miChmCreateLabel = new System.Windows.Forms.ToolStripMenuItem();
		this.miclText = new System.Windows.Forms.ToolStripMenuItem();
		this.miclLine = new System.Windows.Forms.ToolStripMenuItem();
		this.miChmRemoveLabels = new System.Windows.Forms.ToolStripMenuItem();
		this.mirlSelected = new System.Windows.Forms.ToolStripMenuItem();
		this.mirlActiveChrom = new System.Windows.Forms.ToolStripMenuItem();
		this.toolStripSeparator8 = new System.Windows.Forms.ToolStripSeparator();
		this.mirlAllChroms = new System.Windows.Forms.ToolStripMenuItem();
		this.miMethod = new System.Windows.Forms.ToolStripMenuItem();
		this.miMtdTplt = new System.Windows.Forms.ToolStripMenuItem();
		this.miMtdSaveTplt = new System.Windows.Forms.ToolStripMenuItem();
		this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
		this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
		this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
		this.flpChrom = new System.Windows.Forms.FlowLayoutPanel();
		this.toolStrip2 = new System.Windows.Forms.ToolStrip();
		this.btnipPkAddPosi = new System.Windows.Forms.ToolStripButton();
		this.btnipPkCut = new System.Windows.Forms.ToolStripButton();
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
		this.btnipPkAddNeg = new System.Windows.Forms.ToolStripButton();
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
		this.label119 = new System.Windows.Forms.Label();
		this.mtbSigYBeg = new System.Windows.Forms.MaskedTextBox();
		this.label1 = new System.Windows.Forms.Label();
		this.mtbSigYEnd = new System.Windows.Forms.MaskedTextBox();
		this.label29 = new System.Windows.Forms.Label();
		this.label31 = new System.Windows.Forms.Label();
		this.btnXYFull = new System.Windows.Forms.Button();
		this.label32 = new System.Windows.Forms.Label();
		this.mtbTime = new System.Windows.Forms.MaskedTextBox();
		this.btnTimeFull = new System.Windows.Forms.Button();
		this.toolStrip1 = new System.Windows.Forms.ToolStrip();
		this.btnOpen = new System.Windows.Forms.ToolStripButton();
		this.btnOverlayMode = new System.Windows.Forms.ToolStripButton();
		this.btnSave = new System.Windows.Forms.ToolStripButton();
		this.btnClose = new System.Windows.Forms.ToolStripButton();
		this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
		this.toolStripButton6 = new System.Windows.Forms.ToolStripButton();
		this.btnPreview = new System.Windows.Forms.ToolStripButton();
		this.btnPreviousZoom = new System.Windows.Forms.ToolStripButton();
		this.btnNextZoom = new System.Windows.Forms.ToolStripButton();
		this.btnUnzoom = new System.Windows.Forms.ToolStripButton();
		this.toolStripSeparator9 = new System.Windows.Forms.ToolStripSeparator();
		this.toolStripSeparator16 = new System.Windows.Forms.ToolStripSeparator();
		this.toolStripSeparator17 = new System.Windows.Forms.ToolStripSeparator();
		this.toolStripSeparator18 = new System.Windows.Forms.ToolStripSeparator();
		this.btnProperties = new System.Windows.Forms.ToolStripButton();
		this.toolStripSeparator19 = new System.Windows.Forms.ToolStripSeparator();
		this.tsDatAcq = new System.Windows.Forms.ToolStrip();
		this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
		this.toolStripSeparator24 = new System.Windows.Forms.ToolStripSeparator();
		this.lbTime = new System.Windows.Forms.ToolStripLabel();
		this.tbTime = new System.Windows.Forms.ToolStripTextBox();
		this.lbTimeU = new System.Windows.Forms.ToolStripLabel();
		this.toolStripSeparator35 = new System.Windows.Forms.ToolStripSeparator();
		this.lbSignal = new System.Windows.Forms.ToolStripLabel();
		this.tbSigYBeg = new System.Windows.Forms.ToolStripTextBox();
		this.lbSignalU = new System.Windows.Forms.ToolStripLabel();
		this.tbSigYEnd = new System.Windows.Forms.ToolStripTextBox();
		this.lbyUnit = new System.Windows.Forms.ToolStripLabel();
		this.ssChrom = new System.Windows.Forms.StatusStrip();
		this.slbExplain = new System.Windows.Forms.ToolStripStatusLabel();
		this.HeatValue = new System.Windows.Forms.ToolStripStatusLabel();
		this.cmsLibs = new System.Windows.Forms.ContextMenuStrip(this.components);
		this.miAddRow = new System.Windows.Forms.ToolStripMenuItem();
		this.miDeleteRow = new System.Windows.Forms.ToolStripMenuItem();
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
		this.cmsSlices = new System.Windows.Forms.ContextMenuStrip(this.components);
		this.mislcColumnsSetup = new System.Windows.Forms.ToolStripMenuItem();
		this.mislcRestoreDftColumns = new System.Windows.Forms.ToolStripMenuItem();
		this.btnReportSetup = new System.Windows.Forms.ToolStripButton();
		this.btnPrtLink = new System.Windows.Forms.ToolStripButton();
		this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
		this.toolStripButton2 = new System.Windows.Forms.ToolStripButton();
		this.toolStripButton3 = new System.Windows.Forms.ToolStripButton();
		this.toolStripButton4 = new System.Windows.Forms.ToolStripButton();
		this.toolStripButton5 = new System.Windows.Forms.ToolStripButton();
		this.dataGridView2 = new System.Windows.Forms.DataGridView();
		this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.dataGridViewTextBoxColumn9 = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.button8 = new System.Windows.Forms.Button();
		this.button7 = new System.Windows.Forms.Button();
		this.button6 = new System.Windows.Forms.Button();
		this.button5 = new System.Windows.Forms.Button();
		this.dataGridView3 = new System.Windows.Forms.DataGridView();
		this.dataGridViewTextBoxColumn10 = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.dataGridViewTextBoxColumn11 = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.dataGridViewTextBoxColumn12 = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.dataGridViewTextBoxColumn13 = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.dataGridViewTextBoxColumn14 = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.button3 = new System.Windows.Forms.Button();
		this.button4 = new System.Windows.Forms.Button();
		this.button9 = new System.Windows.Forms.Button();
		this.button10 = new System.Windows.Forms.Button();
		this.imageList_0 = new System.Windows.Forms.ImageList(this.components);
		this.imageList_1 = new System.Windows.Forms.ImageList(this.components);
		this.folderBrowserDialog_0 = new System.Windows.Forms.FolderBrowserDialog();
		this.dpgnlChrom = new IBrainChrom2018.LclDisplayPanel();
		this.chromDataGrid = new IBrainChrom2018.ChromFormDataGrid();
		this.mstSetChromForm = new IBrainChrom2018.MstSet();
		this.msChrom.SuspendLayout();
		this.flpChrom.SuspendLayout();
		this.toolStrip2.SuspendLayout();
		this.toolStrip1.SuspendLayout();
		this.tsDatAcq.SuspendLayout();
		this.ssChrom.SuspendLayout();
		this.cmsLibs.SuspendLayout();
		this.cmsSSTCmpds.SuspendLayout();
		this.cmsSlices.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.dataGridView2).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.dataGridView3).BeginInit();
		base.SuspendLayout();
		this.msChrom.ImageScalingSize = new System.Drawing.Size(20, 20);
		this.msChrom.Items.AddRange(new System.Windows.Forms.ToolStripItem[4] { this.miFile, this.miDisplay, this.miChromatogram, this.miMethod });
		this.msChrom.Location = new System.Drawing.Point(0, 0);
		this.msChrom.Name = "msChrom";
		this.msChrom.Size = new System.Drawing.Size(1152, 25);
		this.msChrom.TabIndex = 0;
		this.msChrom.Text = "menuStrip1";
		this.msChrom.Visible = false;
		this.miFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[13]
		{
			this.miFiOverlayMode, this.miFiOpen, this.miFiClose, this.miFiCloseAll, this.miFiSave, this.miFiSaveAs, this.toolStripSeparator2, this.toolStripMenuItem2, this.toolStripMenuItem1, this.toolStripSeparator6,
			this.miFiPreview, this.miFiPrint, this.miFiExit
		});
		this.miFile.Name = "miFile";
		this.miFile.Size = new System.Drawing.Size(60, 21);
		this.miFile.Text = "  文件  ";
		this.miFiOverlayMode.Name = "miFiOverlayMode";
		this.miFiOverlayMode.Size = new System.Drawing.Size(145, 22);
		this.miFiOverlayMode.Text = "重叠模式";
		this.miFiOverlayMode.Click += new System.EventHandler(btnOverlayMode_Click);
		this.miFiOpen.Name = "miFiOpen";
		this.miFiOpen.Size = new System.Drawing.Size(145, 22);
		this.miFiOpen.Text = "打开&O";
		this.miFiOpen.Click += new System.EventHandler(btnOpen_Click);
		this.miFiClose.Name = "miFiClose";
		this.miFiClose.Size = new System.Drawing.Size(145, 22);
		this.miFiClose.Text = "关闭&C";
		this.miFiClose.Click += new System.EventHandler(btnClose_Click);
		this.miFiCloseAll.Name = "miFiCloseAll";
		this.miFiCloseAll.Size = new System.Drawing.Size(145, 22);
		this.miFiCloseAll.Text = "关闭所有";
		this.miFiCloseAll.Click += new System.EventHandler(miFiCloseAll_Click);
		this.miFiSave.Name = "miFiSave";
		this.miFiSave.Size = new System.Drawing.Size(145, 22);
		this.miFiSave.Text = "保存&S";
		this.miFiSave.Click += new System.EventHandler(btnSave_Click);
		this.miFiSaveAs.Name = "miFiSaveAs";
		this.miFiSaveAs.Size = new System.Drawing.Size(145, 22);
		this.miFiSaveAs.Text = "另存为...";
		this.miFiSaveAs.Click += new System.EventHandler(miFiSaveAs_Click);
		this.toolStripSeparator2.Name = "toolStripSeparator2";
		this.toolStripSeparator2.Size = new System.Drawing.Size(142, 6);
		this.toolStripMenuItem2.Name = "toolStripMenuItem2";
		this.toolStripMenuItem2.Size = new System.Drawing.Size(145, 22);
		this.toolStripMenuItem2.Text = "编辑组份表...";
		this.toolStripMenuItem2.Click += new System.EventHandler(toolStripMenuItem2_Click);
		this.toolStripMenuItem1.Name = "toolStripMenuItem1";
		this.toolStripMenuItem1.Size = new System.Drawing.Size(145, 22);
		this.toolStripMenuItem1.Text = "另存为模板...";
		this.toolStripMenuItem1.Visible = false;
		this.toolStripMenuItem1.Click += new System.EventHandler(toolStripMenuItem1_Click);
		this.toolStripSeparator6.Name = "toolStripSeparator6";
		this.toolStripSeparator6.Size = new System.Drawing.Size(142, 6);
		this.miFiPreview.Name = "miFiPreview";
		this.miFiPreview.Size = new System.Drawing.Size(145, 22);
		this.miFiPreview.Text = "打印预览&P";
		this.miFiPreview.Click += new System.EventHandler(btnPreview_Click);
		this.miFiPrint.Name = "miFiPrint";
		this.miFiPrint.Size = new System.Drawing.Size(145, 22);
		this.miFiPrint.Text = "打印";
		this.miFiExit.Name = "miFiExit";
		this.miFiExit.Size = new System.Drawing.Size(145, 22);
		this.miFiExit.Text = "退出&Q";
		this.miFiExit.Click += new System.EventHandler(miFiExit_Click);
		this.miDisplay.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[3] { this.miDisPreviousZoom, this.miDisNextZoom, this.miDisUnzoom });
		this.miDisplay.Name = "miDisplay";
		this.miDisplay.Size = new System.Drawing.Size(60, 21);
		this.miDisplay.Text = "  视图  ";
		this.miDisPreviousZoom.Name = "miDisPreviousZoom";
		this.miDisPreviousZoom.Size = new System.Drawing.Size(124, 22);
		this.miDisPreviousZoom.Text = "前一视图";
		this.miDisPreviousZoom.Click += new System.EventHandler(dpgpcChrom_DoubleClick);
		this.miDisNextZoom.Name = "miDisNextZoom";
		this.miDisNextZoom.Size = new System.Drawing.Size(124, 22);
		this.miDisNextZoom.Text = "后一视图";
		this.miDisNextZoom.Click += new System.EventHandler(btnNextZoom_Click);
		this.miDisUnzoom.Name = "miDisUnzoom";
		this.miDisUnzoom.Size = new System.Drawing.Size(124, 22);
		this.miDisUnzoom.Text = "原始视图";
		this.miDisUnzoom.Click += new System.EventHandler(btnUnzoom_Click);
		this.miChromatogram.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[2] { this.miChmCreateLabel, this.miChmRemoveLabels });
		this.miChromatogram.Name = "miChromatogram";
		this.miChromatogram.Size = new System.Drawing.Size(60, 21);
		this.miChromatogram.Text = "  谱图  ";
		this.miChmCreateLabel.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[2] { this.miclText, this.miclLine });
		this.miChmCreateLabel.Name = "miChmCreateLabel";
		this.miChmCreateLabel.Size = new System.Drawing.Size(124, 22);
		this.miChmCreateLabel.Text = "创建标签";
		this.miclText.Name = "miclText";
		this.miclText.Size = new System.Drawing.Size(100, 22);
		this.miclText.Text = "文本";
		this.miclText.Click += new System.EventHandler(miclLine_Click);
		this.miclLine.Name = "miclLine";
		this.miclLine.Size = new System.Drawing.Size(100, 22);
		this.miclLine.Text = "画线";
		this.miclLine.Click += new System.EventHandler(miclLine_Click);
		this.miChmRemoveLabels.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[4] { this.mirlSelected, this.mirlActiveChrom, this.toolStripSeparator8, this.mirlAllChroms });
		this.miChmRemoveLabels.Name = "miChmRemoveLabels";
		this.miChmRemoveLabels.Size = new System.Drawing.Size(124, 22);
		this.miChmRemoveLabels.Text = "删除标签";
		this.mirlSelected.Name = "mirlSelected";
		this.mirlSelected.Size = new System.Drawing.Size(136, 22);
		this.mirlSelected.Text = "删除选择项";
		this.mirlSelected.Visible = false;
		this.mirlSelected.Click += new System.EventHandler(mirlAllChroms_Click);
		this.mirlActiveChrom.Name = "mirlActiveChrom";
		this.mirlActiveChrom.Size = new System.Drawing.Size(136, 22);
		this.mirlActiveChrom.Text = "删除活动项";
		this.mirlActiveChrom.Click += new System.EventHandler(mirlAllChroms_Click);
		this.toolStripSeparator8.Name = "toolStripSeparator8";
		this.toolStripSeparator8.Size = new System.Drawing.Size(133, 6);
		this.mirlAllChroms.Name = "mirlAllChroms";
		this.mirlAllChroms.Size = new System.Drawing.Size(136, 22);
		this.mirlAllChroms.Text = "删除所有";
		this.mirlAllChroms.Click += new System.EventHandler(mirlAllChroms_Click);
		this.miMethod.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[2] { this.miMtdTplt, this.miMtdSaveTplt });
		this.miMethod.Name = "miMethod";
		this.miMethod.Size = new System.Drawing.Size(60, 21);
		this.miMethod.Text = "  帮助  ";
		this.miMtdTplt.Name = "miMtdTplt";
		this.miMtdTplt.Size = new System.Drawing.Size(109, 22);
		this.miMtdTplt.Text = "帮助&H";
		this.miMtdTplt.Click += new System.EventHandler(miMtdSaveTplt_Click);
		this.miMtdSaveTplt.Name = "miMtdSaveTplt";
		this.miMtdSaveTplt.Size = new System.Drawing.Size(109, 22);
		this.miMtdSaveTplt.Text = "关于&A";
		this.miMtdSaveTplt.Click += new System.EventHandler(miMtdSaveTplt_Click);
		this.toolStripSeparator1.Name = "toolStripSeparator1";
		this.toolStripSeparator1.Size = new System.Drawing.Size(6, 39);
		this.toolStripSeparator1.Visible = false;
		this.toolStripSeparator3.Name = "toolStripSeparator3";
		this.toolStripSeparator3.Size = new System.Drawing.Size(6, 39);
		this.toolStripSeparator3.Visible = false;
		this.toolStripSeparator4.Name = "toolStripSeparator4";
		this.toolStripSeparator4.Size = new System.Drawing.Size(6, 39);
		this.toolStripSeparator4.Visible = false;
		this.flpChrom.AutoSize = true;
		this.flpChrom.Controls.Add(this.toolStrip2);
		this.flpChrom.Controls.Add(this.label119);
		this.flpChrom.Controls.Add(this.mtbSigYBeg);
		this.flpChrom.Controls.Add(this.label1);
		this.flpChrom.Controls.Add(this.mtbSigYEnd);
		this.flpChrom.Controls.Add(this.label29);
		this.flpChrom.Controls.Add(this.label31);
		this.flpChrom.Controls.Add(this.btnXYFull);
		this.flpChrom.Controls.Add(this.label32);
		this.flpChrom.Controls.Add(this.mtbTime);
		this.flpChrom.Controls.Add(this.btnTimeFull);
		this.flpChrom.Controls.Add(this.toolStrip1);
		this.flpChrom.Dock = System.Windows.Forms.DockStyle.Top;
		this.flpChrom.Location = new System.Drawing.Point(0, 0);
		this.flpChrom.Margin = new System.Windows.Forms.Padding(0);
		this.flpChrom.Name = "flpChrom";
		this.flpChrom.Size = new System.Drawing.Size(1152, 37);
		this.flpChrom.TabIndex = 6;
		this.toolStrip2.AutoSize = false;
		this.toolStrip2.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
		this.toolStrip2.ImageScalingSize = new System.Drawing.Size(32, 32);
		this.toolStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[34]
		{
			this.btnipPkAddPosi, this.btnipPkCut, this.btnExpress, this.toolStripSeparator20, this.btngblPeakWidth, this.btngblThreshold, this.btngblPkSlope, this.toolStripSeparator21, this.btnipResetDtecNeg, this.btnipClampNeg,
			this.toolStripSeparator5, this.btnipPkWidth, this.btnipPkThreshold, this.btnipPkAddNeg, this.btnipPkHalfWidth, this.btnipPkArea, this.toolStripSeparator23, this.btnipPkVale, this.toolStripSeparator28, this.btnipSolventPeak,
			this.btnipFlowMarker, this.btnipGroups, this.toolStripSeparator22, this.btnbsBsTgnt, this.btnbsBsVtV, this.toolStripSeparator14, this.btnbsBsValley, this.btnbsBsTogether, this.btnbsBsForwHorz, this.btnbsBsBackHorz,
			this.btnbsBsFrontTgnt, this.btnbsBsTailTgnt, this.toolStripSeparator31, this.btngblDtecDelay
		});
		this.toolStrip2.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
		this.toolStrip2.Location = new System.Drawing.Point(0, 0);
		this.toolStrip2.Name = "toolStrip2";
		this.toolStrip2.Size = new System.Drawing.Size(129, 37);
		this.toolStrip2.TabIndex = 6;
		this.toolStrip2.Text = "toolStrip2";
		this.btnipPkAddPosi.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
		this.btnipPkAddPosi.Image = (System.Drawing.Image)resources.GetObject("btnipPkAddPosi.Image");
		this.btnipPkAddPosi.ImageTransparentColor = System.Drawing.Color.Magenta;
		this.btnipPkAddPosi.Name = "btnipPkAddPosi";
		this.btnipPkAddPosi.Size = new System.Drawing.Size(36, 34);
		this.btnipPkAddPosi.Text = "toolStripButton38";
		this.btnipPkAddPosi.ToolTipText = "添加正峰";
		this.btnipPkAddPosi.Click += new System.EventHandler(btngblDtecDelay_Click);
		this.btnipPkCut.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
		this.btnipPkCut.Image = (System.Drawing.Image)resources.GetObject("btnipPkCut.Image");
		this.btnipPkCut.ImageTransparentColor = System.Drawing.Color.Magenta;
		this.btnipPkCut.Name = "btnipPkCut";
		this.btnipPkCut.Size = new System.Drawing.Size(36, 34);
		this.btnipPkCut.Text = "toolStripButton51";
		this.btnipPkCut.ToolTipText = "删除峰";
		this.btnipPkCut.Click += new System.EventHandler(btngblDtecDelay_Click);
		this.btnExpress.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
		this.btnExpress.Image = (System.Drawing.Image)resources.GetObject("btnExpress.Image");
		this.btnExpress.ImageTransparentColor = System.Drawing.Color.Magenta;
		this.btnExpress.Margin = new System.Windows.Forms.Padding(5, 1, 0, 2);
		this.btnExpress.Name = "btnExpress";
		this.btnExpress.Size = new System.Drawing.Size(36, 36);
		this.btnExpress.Text = "显示操作描述";
		this.btnExpress.Click += new System.EventHandler(btnExpress_Click);
		this.toolStripSeparator20.Name = "toolStripSeparator20";
		this.toolStripSeparator20.Size = new System.Drawing.Size(6, 39);
		this.btngblPeakWidth.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
		this.btngblPeakWidth.Image = (System.Drawing.Image)resources.GetObject("btngblPeakWidth.Image");
		this.btngblPeakWidth.ImageTransparentColor = System.Drawing.Color.Magenta;
		this.btngblPeakWidth.Name = "btngblPeakWidth";
		this.btngblPeakWidth.Size = new System.Drawing.Size(36, 36);
		this.btngblPeakWidth.Text = "toolStripButton33";
		this.btngblPeakWidth.ToolTipText = "峰宽参数";
		this.btngblPeakWidth.Click += new System.EventHandler(btngblDtecDelay_Click);
		this.btngblThreshold.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
		this.btngblThreshold.Image = (System.Drawing.Image)resources.GetObject("btngblThreshold.Image");
		this.btngblThreshold.ImageTransparentColor = System.Drawing.Color.Magenta;
		this.btngblThreshold.Name = "btngblThreshold";
		this.btngblThreshold.Size = new System.Drawing.Size(36, 36);
		this.btngblThreshold.Text = "toolStripButton34";
		this.btngblThreshold.ToolTipText = "峰高参数";
		this.btngblThreshold.Click += new System.EventHandler(btngblDtecDelay_Click);
		this.btngblPkSlope.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
		this.btngblPkSlope.Image = (System.Drawing.Image)resources.GetObject("btngblPkSlope.Image");
		this.btngblPkSlope.ImageTransparentColor = System.Drawing.Color.Magenta;
		this.btngblPkSlope.Name = "btngblPkSlope";
		this.btngblPkSlope.Size = new System.Drawing.Size(36, 36);
		this.btngblPkSlope.Text = "toolStripButton49";
		this.btngblPkSlope.ToolTipText = "峰斜率";
		this.btngblPkSlope.Visible = false;
		this.btngblPkSlope.Click += new System.EventHandler(btngblDtecDelay_Click);
		this.toolStripSeparator21.Name = "toolStripSeparator21";
		this.toolStripSeparator21.Size = new System.Drawing.Size(6, 39);
		this.btnipResetDtecNeg.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
		this.btnipResetDtecNeg.Image = (System.Drawing.Image)resources.GetObject("btnipResetDtecNeg.Image");
		this.btnipResetDtecNeg.ImageTransparentColor = System.Drawing.Color.Magenta;
		this.btnipResetDtecNeg.Name = "btnipResetDtecNeg";
		this.btnipResetDtecNeg.Size = new System.Drawing.Size(36, 36);
		this.btnipResetDtecNeg.Text = "toolStripButton56";
		this.btnipResetDtecNeg.ToolTipText = "检测负峰";
		this.btnipResetDtecNeg.Click += new System.EventHandler(btngblDtecDelay_Click);
		this.btnipClampNeg.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
		this.btnipClampNeg.Image = (System.Drawing.Image)resources.GetObject("btnipClampNeg.Image");
		this.btnipClampNeg.ImageTransparentColor = System.Drawing.Color.Magenta;
		this.btnipClampNeg.Name = "btnipClampNeg";
		this.btnipClampNeg.Size = new System.Drawing.Size(36, 36);
		this.btnipClampNeg.Text = "toolStripButton50";
		this.btnipClampNeg.ToolTipText = "翻转负峰";
		this.btnipClampNeg.Click += new System.EventHandler(btngblDtecDelay_Click);
		this.toolStripSeparator5.Name = "toolStripSeparator5";
		this.toolStripSeparator5.Size = new System.Drawing.Size(6, 39);
		this.btnipPkWidth.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
		this.btnipPkWidth.Image = (System.Drawing.Image)resources.GetObject("btnipPkWidth.Image");
		this.btnipPkWidth.ImageTransparentColor = System.Drawing.Color.Magenta;
		this.btnipPkWidth.Name = "btnipPkWidth";
		this.btnipPkWidth.Size = new System.Drawing.Size(36, 36);
		this.btnipPkWidth.Text = "最小峰宽";
		this.btnipPkWidth.Click += new System.EventHandler(btngblDtecDelay_Click);
		this.btnipPkThreshold.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
		this.btnipPkThreshold.Image = (System.Drawing.Image)resources.GetObject("btnipPkThreshold.Image");
		this.btnipPkThreshold.ImageTransparentColor = System.Drawing.Color.Magenta;
		this.btnipPkThreshold.Name = "btnipPkThreshold";
		this.btnipPkThreshold.Size = new System.Drawing.Size(36, 36);
		this.btnipPkThreshold.Text = "toolStripButton49";
		this.btnipPkThreshold.ToolTipText = "最小峰高";
		this.btnipPkThreshold.Click += new System.EventHandler(btngblDtecDelay_Click);
		this.btnipPkAddNeg.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
		this.btnipPkAddNeg.Image = (System.Drawing.Image)resources.GetObject("btnipPkAddNeg.Image");
		this.btnipPkAddNeg.ImageTransparentColor = System.Drawing.Color.Magenta;
		this.btnipPkAddNeg.Name = "btnipPkAddNeg";
		this.btnipPkAddNeg.Size = new System.Drawing.Size(36, 36);
		this.btnipPkAddNeg.Text = "toolStripButton39";
		this.btnipPkAddNeg.ToolTipText = "添加负峰";
		this.btnipPkAddNeg.Click += new System.EventHandler(btngblDtecDelay_Click);
		this.btnipPkHalfWidth.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
		this.btnipPkHalfWidth.Image = (System.Drawing.Image)resources.GetObject("btnipPkHalfWidth.Image");
		this.btnipPkHalfWidth.ImageTransparentColor = System.Drawing.Color.Magenta;
		this.btnipPkHalfWidth.Name = "btnipPkHalfWidth";
		this.btnipPkHalfWidth.Size = new System.Drawing.Size(36, 36);
		this.btnipPkHalfWidth.Text = "toolStripButton59";
		this.btnipPkHalfWidth.ToolTipText = "最小半峰宽";
		this.btnipPkHalfWidth.Click += new System.EventHandler(btngblDtecDelay_Click);
		this.btnipPkArea.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
		this.btnipPkArea.Image = (System.Drawing.Image)resources.GetObject("btnipPkArea.Image");
		this.btnipPkArea.ImageTransparentColor = System.Drawing.Color.Magenta;
		this.btnipPkArea.Name = "btnipPkArea";
		this.btnipPkArea.Size = new System.Drawing.Size(36, 36);
		this.btnipPkArea.Text = "toolStripButton57";
		this.btnipPkArea.ToolTipText = "最小峰面积";
		this.btnipPkArea.Click += new System.EventHandler(btngblDtecDelay_Click);
		this.toolStripSeparator23.Name = "toolStripSeparator23";
		this.toolStripSeparator23.Size = new System.Drawing.Size(6, 39);
		this.btnipPkVale.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
		this.btnipPkVale.Image = (System.Drawing.Image)resources.GetObject("btnipPkVale.Image");
		this.btnipPkVale.ImageTransparentColor = System.Drawing.Color.Magenta;
		this.btnipPkVale.Name = "btnipPkVale";
		this.btnipPkVale.Size = new System.Drawing.Size(36, 36);
		this.btnipPkVale.Text = "toolStripButton35";
		this.btnipPkVale.ToolTipText = "谷点";
		this.btnipPkVale.Click += new System.EventHandler(btngblDtecDelay_Click);
		this.toolStripSeparator28.Name = "toolStripSeparator28";
		this.toolStripSeparator28.Size = new System.Drawing.Size(6, 39);
		this.toolStripSeparator28.Visible = false;
		this.btnipSolventPeak.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
		this.btnipSolventPeak.Image = (System.Drawing.Image)resources.GetObject("btnipSolventPeak.Image");
		this.btnipSolventPeak.ImageTransparentColor = System.Drawing.Color.Magenta;
		this.btnipSolventPeak.Name = "btnipSolventPeak";
		this.btnipSolventPeak.Size = new System.Drawing.Size(36, 36);
		this.btnipSolventPeak.Text = "toolStripButton40";
		this.btnipSolventPeak.ToolTipText = "峰切分";
		this.btnipSolventPeak.Click += new System.EventHandler(btngblDtecDelay_Click);
		this.btnipFlowMarker.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
		this.btnipFlowMarker.Image = (System.Drawing.Image)resources.GetObject("btnipFlowMarker.Image");
		this.btnipFlowMarker.ImageTransparentColor = System.Drawing.Color.Magenta;
		this.btnipFlowMarker.Name = "btnipFlowMarker";
		this.btnipFlowMarker.Size = new System.Drawing.Size(36, 36);
		this.btnipFlowMarker.Text = "toolStripButton41";
		this.btnipFlowMarker.ToolTipText = "流速标识";
		this.btnipFlowMarker.Visible = false;
		this.btnipFlowMarker.Click += new System.EventHandler(btngblDtecDelay_Click);
		this.btnipGroups.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
		this.btnipGroups.Image = (System.Drawing.Image)resources.GetObject("btnipGroups.Image");
		this.btnipGroups.ImageTransparentColor = System.Drawing.Color.Magenta;
		this.btnipGroups.Name = "btnipGroups";
		this.btnipGroups.Size = new System.Drawing.Size(36, 36);
		this.btnipGroups.Text = "toolStripButton42";
		this.btnipGroups.ToolTipText = "添加组";
		this.btnipGroups.Click += new System.EventHandler(btngblDtecDelay_Click);
		this.toolStripSeparator22.Name = "toolStripSeparator22";
		this.toolStripSeparator22.Size = new System.Drawing.Size(6, 39);
		this.btnbsBsTgnt.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
		this.btnbsBsTgnt.Image = (System.Drawing.Image)resources.GetObject("btnbsBsTgnt.Image");
		this.btnbsBsTgnt.ImageTransparentColor = System.Drawing.Color.Magenta;
		this.btnbsBsTgnt.Name = "btnbsBsTgnt";
		this.btnbsBsTgnt.Size = new System.Drawing.Size(36, 36);
		this.btnbsBsTgnt.Text = "toolStripButton1";
		this.btnbsBsTgnt.ToolTipText = "切肩参数";
		this.btnbsBsTgnt.Click += new System.EventHandler(btngblDtecDelay_Click);
		this.btnbsBsVtV.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
		this.btnbsBsVtV.Image = (System.Drawing.Image)resources.GetObject("btnbsBsVtV.Image");
		this.btnbsBsVtV.ImageTransparentColor = System.Drawing.Color.Magenta;
		this.btnbsBsVtV.Name = "btnbsBsVtV";
		this.btnbsBsVtV.Size = new System.Drawing.Size(36, 36);
		this.btnbsBsVtV.Text = "toolStripButton2";
		this.btnbsBsVtV.ToolTipText = "谷.谷斜率";
		this.btnbsBsVtV.Click += new System.EventHandler(btngblDtecDelay_Click);
		this.toolStripSeparator14.Name = "toolStripSeparator14";
		this.toolStripSeparator14.Size = new System.Drawing.Size(6, 39);
		this.btnbsBsValley.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
		this.btnbsBsValley.Image = (System.Drawing.Image)resources.GetObject("btnbsBsValley.Image");
		this.btnbsBsValley.ImageTransparentColor = System.Drawing.Color.Magenta;
		this.btnbsBsValley.Name = "btnbsBsValley";
		this.btnbsBsValley.Size = new System.Drawing.Size(36, 36);
		this.btnbsBsValley.Text = "toolStripButton44";
		this.btnbsBsValley.ToolTipText = "经过谷点";
		this.btnbsBsValley.Click += new System.EventHandler(btngblDtecDelay_Click);
		this.btnbsBsTogether.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
		this.btnbsBsTogether.Image = (System.Drawing.Image)resources.GetObject("btnbsBsTogether.Image");
		this.btnbsBsTogether.ImageTransparentColor = System.Drawing.Color.Magenta;
		this.btnbsBsTogether.Name = "btnbsBsTogether";
		this.btnbsBsTogether.Size = new System.Drawing.Size(36, 36);
		this.btnbsBsTogether.Text = "toolStripButton45";
		this.btnbsBsTogether.ToolTipText = "整合基线";
		this.btnbsBsTogether.Click += new System.EventHandler(btngblDtecDelay_Click);
		this.btnbsBsForwHorz.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
		this.btnbsBsForwHorz.Image = (System.Drawing.Image)resources.GetObject("btnbsBsForwHorz.Image");
		this.btnbsBsForwHorz.ImageTransparentColor = System.Drawing.Color.Magenta;
		this.btnbsBsForwHorz.Name = "btnbsBsForwHorz";
		this.btnbsBsForwHorz.Size = new System.Drawing.Size(36, 36);
		this.btnbsBsForwHorz.Text = "toolStripButton46";
		this.btnbsBsForwHorz.ToolTipText = "向前水平";
		this.btnbsBsForwHorz.Click += new System.EventHandler(btngblDtecDelay_Click);
		this.btnbsBsBackHorz.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
		this.btnbsBsBackHorz.Image = (System.Drawing.Image)resources.GetObject("btnbsBsBackHorz.Image");
		this.btnbsBsBackHorz.ImageTransparentColor = System.Drawing.Color.Magenta;
		this.btnbsBsBackHorz.Name = "btnbsBsBackHorz";
		this.btnbsBsBackHorz.Size = new System.Drawing.Size(36, 36);
		this.btnbsBsBackHorz.Text = "toolStripButton47";
		this.btnbsBsBackHorz.ToolTipText = "向后水平";
		this.btnbsBsBackHorz.Click += new System.EventHandler(btngblDtecDelay_Click);
		this.btnbsBsFrontTgnt.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
		this.btnbsBsFrontTgnt.Image = (System.Drawing.Image)resources.GetObject("btnbsBsFrontTgnt.Image");
		this.btnbsBsFrontTgnt.ImageTransparentColor = System.Drawing.Color.Magenta;
		this.btnbsBsFrontTgnt.Name = "btnbsBsFrontTgnt";
		this.btnbsBsFrontTgnt.Size = new System.Drawing.Size(36, 36);
		this.btnbsBsFrontTgnt.Text = "toolStripButton48";
		this.btnbsBsFrontTgnt.ToolTipText = "前切";
		this.btnbsBsFrontTgnt.Click += new System.EventHandler(btngblDtecDelay_Click);
		this.btnbsBsTailTgnt.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
		this.btnbsBsTailTgnt.Image = (System.Drawing.Image)resources.GetObject("btnbsBsTailTgnt.Image");
		this.btnbsBsTailTgnt.ImageTransparentColor = System.Drawing.Color.Magenta;
		this.btnbsBsTailTgnt.Name = "btnbsBsTailTgnt";
		this.btnbsBsTailTgnt.Size = new System.Drawing.Size(36, 36);
		this.btnbsBsTailTgnt.Text = "toolStripButton49";
		this.btnbsBsTailTgnt.ToolTipText = "尾切";
		this.btnbsBsTailTgnt.Click += new System.EventHandler(btngblDtecDelay_Click);
		this.toolStripSeparator31.Name = "toolStripSeparator31";
		this.toolStripSeparator31.Size = new System.Drawing.Size(6, 39);
		this.btngblDtecDelay.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
		this.btngblDtecDelay.Image = (System.Drawing.Image)resources.GetObject("btngblDtecDelay.Image");
		this.btngblDtecDelay.ImageTransparentColor = System.Drawing.Color.Magenta;
		this.btngblDtecDelay.Name = "btngblDtecDelay";
		this.btngblDtecDelay.Size = new System.Drawing.Size(36, 36);
		this.btngblDtecDelay.Text = "toolStripButton60";
		this.btngblDtecDelay.ToolTipText = "信号延迟";
		this.btngblDtecDelay.Click += new System.EventHandler(btngblDtecDelay_Click);
		this.label119.AutoSize = true;
		this.label119.Font = new System.Drawing.Font("宋体", 9f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 134);
		this.label119.ForeColor = System.Drawing.Color.Black;
		this.label119.Location = new System.Drawing.Point(132, 0);
		this.label119.Name = "label119";
		this.label119.Size = new System.Drawing.Size(38, 12);
		this.label119.TabIndex = 10;
		this.label119.Text = "下限:";
		this.mtbSigYBeg.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.mtbSigYBeg.Font = new System.Drawing.Font("宋体", 9f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 134);
		this.mtbSigYBeg.ForeColor = System.Drawing.Color.Lime;
		this.mtbSigYBeg.Location = new System.Drawing.Point(176, 3);
		this.mtbSigYBeg.Name = "mtbSigYBeg";
		this.mtbSigYBeg.PromptChar = ' ';
		this.mtbSigYBeg.Size = new System.Drawing.Size(52, 21);
		this.mtbSigYBeg.TabIndex = 12;
		this.mtbSigYBeg.Text = "-10";
		this.mtbSigYBeg.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
		this.mtbSigYBeg.TextChanged += new System.EventHandler(mtbSigYBeg_DoubleClick);
		this.mtbSigYBeg.DoubleClick += new System.EventHandler(mtbSigYBeg_DoubleClick);
		this.mtbSigYBeg.Enter += new System.EventHandler(gvRltsGnl_Enter);
		this.mtbSigYBeg.KeyDown += new System.Windows.Forms.KeyEventHandler(mtbSigYBeg_KeyDown);
		this.label1.AutoSize = true;
		this.label1.Font = new System.Drawing.Font("宋体", 9f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 134);
		this.label1.ForeColor = System.Drawing.Color.Black;
		this.label1.Location = new System.Drawing.Point(234, 0);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(38, 12);
		this.label1.TabIndex = 11;
		this.label1.Text = "上限:";
		this.mtbSigYEnd.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.mtbSigYEnd.Font = new System.Drawing.Font("宋体", 9f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 134);
		this.mtbSigYEnd.ForeColor = System.Drawing.Color.Lime;
		this.mtbSigYEnd.Location = new System.Drawing.Point(278, 3);
		this.mtbSigYEnd.Name = "mtbSigYEnd";
		this.mtbSigYEnd.PromptChar = ' ';
		this.mtbSigYEnd.Size = new System.Drawing.Size(52, 21);
		this.mtbSigYEnd.TabIndex = 13;
		this.mtbSigYEnd.Text = "500";
		this.mtbSigYEnd.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
		this.mtbSigYEnd.TextChanged += new System.EventHandler(mtbSigYBeg_DoubleClick);
		this.mtbSigYEnd.DoubleClick += new System.EventHandler(mtbSigYBeg_DoubleClick);
		this.mtbSigYEnd.Enter += new System.EventHandler(gvRltsGnl_Enter);
		this.mtbSigYEnd.KeyDown += new System.Windows.Forms.KeyEventHandler(mtbSigYBeg_KeyDown);
		this.label29.AutoSize = true;
		this.label29.Location = new System.Drawing.Point(336, 0);
		this.label29.Name = "label29";
		this.label29.Size = new System.Drawing.Size(17, 12);
		this.label29.TabIndex = 15;
		this.label29.Text = "mV";
		this.label31.AutoSize = true;
		this.label31.Location = new System.Drawing.Point(359, 0);
		this.label31.Name = "label31";
		this.label31.Size = new System.Drawing.Size(23, 12);
		this.label31.TabIndex = 16;
		this.label31.Text = "min";
		this.btnXYFull.Location = new System.Drawing.Point(388, 3);
		this.btnXYFull.Name = "btnXYFull";
		this.btnXYFull.Size = new System.Drawing.Size(47, 23);
		this.btnXYFull.TabIndex = 18;
		this.btnXYFull.Text = "满屏";
		this.btnXYFull.UseVisualStyleBackColor = true;
		this.btnXYFull.Click += new System.EventHandler(btnXYFull_Click);
		this.label32.AutoSize = true;
		this.label32.Font = new System.Drawing.Font("宋体", 9f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 134);
		this.label32.ForeColor = System.Drawing.Color.Black;
		this.label32.Location = new System.Drawing.Point(441, 0);
		this.label32.Name = "label32";
		this.label32.Size = new System.Drawing.Size(64, 12);
		this.label32.TabIndex = 9;
		this.label32.Text = "满屏时间:";
		this.mtbTime.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.mtbTime.Font = new System.Drawing.Font("宋体", 9f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 134);
		this.mtbTime.ForeColor = System.Drawing.Color.Lime;
		this.mtbTime.Location = new System.Drawing.Point(511, 3);
		this.mtbTime.Name = "mtbTime";
		this.mtbTime.PromptChar = ' ';
		this.mtbTime.Size = new System.Drawing.Size(40, 21);
		this.mtbTime.TabIndex = 14;
		this.mtbTime.Text = "30";
		this.mtbTime.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
		this.mtbTime.TextChanged += new System.EventHandler(mtbSigYBeg_DoubleClick);
		this.mtbTime.DoubleClick += new System.EventHandler(mtbSigYBeg_DoubleClick);
		this.mtbTime.Enter += new System.EventHandler(gvRltsGnl_Enter);
		this.mtbTime.KeyDown += new System.Windows.Forms.KeyEventHandler(mtbSigYBeg_KeyDown);
		this.btnTimeFull.Location = new System.Drawing.Point(557, 3);
		this.btnTimeFull.Name = "btnTimeFull";
		this.btnTimeFull.Size = new System.Drawing.Size(47, 23);
		this.btnTimeFull.TabIndex = 17;
		this.btnTimeFull.Text = "满屏";
		this.btnTimeFull.UseVisualStyleBackColor = true;
		this.btnTimeFull.Click += new System.EventHandler(btnTimeFull_Click);
		this.toolStrip1.Dock = System.Windows.Forms.DockStyle.None;
		this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
		this.toolStrip1.ImageScalingSize = new System.Drawing.Size(32, 32);
		this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[14]
		{
			this.btnOpen, this.btnOverlayMode, this.btnSave, this.btnClose, this.toolStripSeparator7, this.toolStripButton6, this.toolStripSeparator1, this.btnPreview, this.toolStripSeparator3, this.btnPreviousZoom,
			this.btnNextZoom, this.btnUnzoom, this.toolStripSeparator4, this.toolStripSeparator9
		});
		this.toolStrip1.Location = new System.Drawing.Point(607, 0);
		this.toolStrip1.Name = "toolStrip1";
		this.toolStrip1.Size = new System.Drawing.Size(393, 39);
		this.toolStrip1.TabIndex = 4;
		this.toolStrip1.Text = "toolStrip1";
		this.toolStrip1.Visible = false;
		this.btnOpen.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
		this.btnOpen.Image = (System.Drawing.Image)resources.GetObject("btnOpen.Image");
		this.btnOpen.ImageTransparentColor = System.Drawing.Color.Magenta;
		this.btnOpen.Margin = new System.Windows.Forms.Padding(5, 1, 0, 2);
		this.btnOpen.Name = "btnOpen";
		this.btnOpen.Size = new System.Drawing.Size(36, 36);
		this.btnOpen.Text = "toolStripButton1";
		this.btnOpen.ToolTipText = "打开谱图文件";
		this.btnOpen.Visible = false;
		this.btnOpen.Click += new System.EventHandler(btnOpen_Click);
		this.btnOverlayMode.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
		this.btnOverlayMode.Image = (System.Drawing.Image)resources.GetObject("btnOverlayMode.Image");
		this.btnOverlayMode.ImageTransparentColor = System.Drawing.Color.Magenta;
		this.btnOverlayMode.Name = "btnOverlayMode";
		this.btnOverlayMode.Size = new System.Drawing.Size(36, 36);
		this.btnOverlayMode.Text = "toolStripButton12";
		this.btnOverlayMode.ToolTipText = "谱图叠加打开";
		this.btnOverlayMode.Visible = false;
		this.btnOverlayMode.Click += new System.EventHandler(btnOverlayMode_Click);
		this.btnSave.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
		this.btnSave.Image = (System.Drawing.Image)resources.GetObject("btnSave.Image");
		this.btnSave.ImageTransparentColor = System.Drawing.Color.Magenta;
		this.btnSave.Name = "btnSave";
		this.btnSave.Size = new System.Drawing.Size(36, 36);
		this.btnSave.Text = "toolStripButton2";
		this.btnSave.ToolTipText = "保存当前谱图文件";
		this.btnSave.Visible = false;
		this.btnSave.Click += new System.EventHandler(btnSave_Click);
		this.btnClose.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
		this.btnClose.Image = (System.Drawing.Image)resources.GetObject("btnClose.Image");
		this.btnClose.ImageTransparentColor = System.Drawing.Color.Magenta;
		this.btnClose.Name = "btnClose";
		this.btnClose.Size = new System.Drawing.Size(36, 36);
		this.btnClose.Text = "toolStripButton3";
		this.btnClose.ToolTipText = "关闭";
		this.btnClose.Visible = false;
		this.btnClose.Click += new System.EventHandler(btnClose_Click);
		this.toolStripSeparator7.Name = "toolStripSeparator7";
		this.toolStripSeparator7.Size = new System.Drawing.Size(6, 39);
		this.toolStripSeparator7.Visible = false;
		this.toolStripButton6.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
		this.toolStripButton6.Image = (System.Drawing.Image)resources.GetObject("toolStripButton6.Image");
		this.toolStripButton6.ImageTransparentColor = System.Drawing.Color.Magenta;
		this.toolStripButton6.Name = "toolStripButton6";
		this.toolStripButton6.Size = new System.Drawing.Size(36, 36);
		this.toolStripButton6.Text = "toolStripButton6";
		this.toolStripButton6.Visible = false;
		this.toolStripButton6.Click += new System.EventHandler(toolStripButton6_Click);
		this.btnPreview.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
		this.btnPreview.Image = (System.Drawing.Image)resources.GetObject("btnPreview.Image");
		this.btnPreview.ImageTransparentColor = System.Drawing.Color.Magenta;
		this.btnPreview.Name = "btnPreview";
		this.btnPreview.Size = new System.Drawing.Size(36, 36);
		this.btnPreview.Text = "toolStripButton5";
		this.btnPreview.ToolTipText = "打印预览";
		this.btnPreview.Visible = false;
		this.btnPreview.Click += new System.EventHandler(btnPreview_Click);
		this.btnPreviousZoom.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
		this.btnPreviousZoom.Image = (System.Drawing.Image)resources.GetObject("btnPreviousZoom.Image");
		this.btnPreviousZoom.ImageTransparentColor = System.Drawing.Color.Magenta;
		this.btnPreviousZoom.Name = "btnPreviousZoom";
		this.btnPreviousZoom.Size = new System.Drawing.Size(36, 36);
		this.btnPreviousZoom.Text = "toolStripButton7";
		this.btnPreviousZoom.ToolTipText = "前一视图";
		this.btnPreviousZoom.Visible = false;
		this.btnPreviousZoom.Click += new System.EventHandler(dpgpcChrom_DoubleClick);
		this.btnNextZoom.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
		this.btnNextZoom.Image = (System.Drawing.Image)resources.GetObject("btnNextZoom.Image");
		this.btnNextZoom.ImageTransparentColor = System.Drawing.Color.Magenta;
		this.btnNextZoom.Name = "btnNextZoom";
		this.btnNextZoom.Size = new System.Drawing.Size(36, 36);
		this.btnNextZoom.Text = "toolStripButton8";
		this.btnNextZoom.ToolTipText = "后一视图";
		this.btnNextZoom.Visible = false;
		this.btnNextZoom.Click += new System.EventHandler(btnNextZoom_Click);
		this.btnUnzoom.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
		this.btnUnzoom.Image = (System.Drawing.Image)resources.GetObject("btnUnzoom.Image");
		this.btnUnzoom.ImageTransparentColor = System.Drawing.Color.Magenta;
		this.btnUnzoom.Name = "btnUnzoom";
		this.btnUnzoom.Size = new System.Drawing.Size(36, 36);
		this.btnUnzoom.Text = "toolStripButton9";
		this.btnUnzoom.ToolTipText = "原始视图";
		this.btnUnzoom.Visible = false;
		this.btnUnzoom.Click += new System.EventHandler(btnUnzoom_Click);
		this.toolStripSeparator9.Name = "toolStripSeparator9";
		this.toolStripSeparator9.Size = new System.Drawing.Size(6, 39);
		this.toolStripSeparator9.Visible = false;
		this.toolStripSeparator16.Name = "toolStripSeparator16";
		this.toolStripSeparator16.Size = new System.Drawing.Size(6, 39);
		this.toolStripSeparator17.Name = "toolStripSeparator17";
		this.toolStripSeparator17.Size = new System.Drawing.Size(6, 39);
		this.toolStripSeparator18.Name = "toolStripSeparator18";
		this.toolStripSeparator18.Size = new System.Drawing.Size(6, 39);
		this.btnProperties.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
		this.btnProperties.ImageTransparentColor = System.Drawing.Color.Magenta;
		this.btnProperties.Name = "btnProperties";
		this.btnProperties.Size = new System.Drawing.Size(23, 36);
		this.btnProperties.Text = "toolStripButton10";
		this.btnProperties.Click += new System.EventHandler(btnProperties_Click);
		this.toolStripSeparator19.Name = "toolStripSeparator19";
		this.toolStripSeparator19.Size = new System.Drawing.Size(6, 39);
		this.tsDatAcq.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.tsDatAcq.Dock = System.Windows.Forms.DockStyle.None;
		this.tsDatAcq.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
		this.tsDatAcq.ImageScalingSize = new System.Drawing.Size(20, 20);
		this.tsDatAcq.Items.AddRange(new System.Windows.Forms.ToolStripItem[11]
		{
			this.toolStripLabel1, this.toolStripSeparator24, this.lbTime, this.tbTime, this.lbTimeU, this.toolStripSeparator35, this.lbSignal, this.tbSigYBeg, this.lbSignalU, this.tbSigYEnd,
			this.lbyUnit
		});
		this.tsDatAcq.Location = new System.Drawing.Point(1056, 0);
		this.tsDatAcq.Name = "tsDatAcq";
		this.tsDatAcq.Size = new System.Drawing.Size(102, 25);
		this.tsDatAcq.TabIndex = 8;
		this.tsDatAcq.Text = "toolStrip1";
		this.tsDatAcq.Paint += new System.Windows.Forms.PaintEventHandler(tsDatAcq_Paint);
		this.toolStripLabel1.Font = new System.Drawing.Font("微软雅黑", 9f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 134);
		this.toolStripLabel1.ForeColor = System.Drawing.Color.Blue;
		this.toolStripLabel1.Name = "toolStripLabel1";
		this.toolStripLabel1.Size = new System.Drawing.Size(68, 22);
		this.toolStripLabel1.Text = "谱图参数：";
		this.toolStripLabel1.ToolTipText = "设置谱图显示参数，修改后回车生效";
		this.toolStripLabel1.Visible = false;
		this.toolStripSeparator24.Name = "toolStripSeparator24";
		this.toolStripSeparator24.Size = new System.Drawing.Size(6, 25);
		this.toolStripSeparator24.Visible = false;
		this.lbTime.Margin = new System.Windows.Forms.Padding(5, 1, 0, 2);
		this.lbTime.Name = "lbTime";
		this.lbTime.Size = new System.Drawing.Size(32, 22);
		this.lbTime.Text = "时间";
		this.lbTime.Visible = false;
		this.tbTime.BorderStyle = System.Windows.Forms.BorderStyle.None;
		this.tbTime.Font = new System.Drawing.Font("Tahoma", 8.25f);
		this.tbTime.Name = "tbTime";
		this.tbTime.Size = new System.Drawing.Size(50, 25);
		this.tbTime.Text = "30";
		this.tbTime.TextBoxTextAlign = System.Windows.Forms.HorizontalAlignment.Center;
		this.tbTime.Visible = false;
		this.tbTime.Enter += new System.EventHandler(gvRltsGnl_Enter);
		this.tbTime.KeyDown += new System.Windows.Forms.KeyEventHandler(mtbSigYBeg_KeyDown);
		this.tbTime.DoubleClick += new System.EventHandler(mtbSigYBeg_DoubleClick);
		this.tbTime.TextChanged += new System.EventHandler(mtbSigYBeg_DoubleClick);
		this.lbTimeU.Margin = new System.Windows.Forms.Padding(2, 1, 0, 2);
		this.lbTimeU.Name = "lbTimeU";
		this.lbTimeU.Size = new System.Drawing.Size(20, 22);
		this.lbTimeU.Text = "分";
		this.lbTimeU.Visible = false;
		this.toolStripSeparator35.Name = "toolStripSeparator35";
		this.toolStripSeparator35.Size = new System.Drawing.Size(6, 25);
		this.toolStripSeparator35.Visible = false;
		this.lbSignal.Name = "lbSignal";
		this.lbSignal.Size = new System.Drawing.Size(32, 22);
		this.lbSignal.Text = "信号";
		this.lbSignal.Visible = false;
		this.tbSigYBeg.BorderStyle = System.Windows.Forms.BorderStyle.None;
		this.tbSigYBeg.Font = new System.Drawing.Font("Tahoma", 8.25f);
		this.tbSigYBeg.Name = "tbSigYBeg";
		this.tbSigYBeg.Size = new System.Drawing.Size(50, 25);
		this.tbSigYBeg.Text = "-10";
		this.tbSigYBeg.TextBoxTextAlign = System.Windows.Forms.HorizontalAlignment.Center;
		this.tbSigYBeg.Visible = false;
		this.tbSigYBeg.Enter += new System.EventHandler(gvRltsGnl_Enter);
		this.tbSigYBeg.KeyDown += new System.Windows.Forms.KeyEventHandler(mtbSigYBeg_KeyDown);
		this.tbSigYBeg.DoubleClick += new System.EventHandler(mtbSigYBeg_DoubleClick);
		this.tbSigYBeg.TextChanged += new System.EventHandler(mtbSigYBeg_DoubleClick);
		this.lbSignalU.Margin = new System.Windows.Forms.Padding(3, 1, 0, 2);
		this.lbSignalU.Name = "lbSignalU";
		this.lbSignalU.Size = new System.Drawing.Size(20, 22);
		this.lbSignalU.Text = "到";
		this.lbSignalU.Visible = false;
		this.tbSigYEnd.BorderStyle = System.Windows.Forms.BorderStyle.None;
		this.tbSigYEnd.Font = new System.Drawing.Font("Tahoma", 8.25f);
		this.tbSigYEnd.Name = "tbSigYEnd";
		this.tbSigYEnd.Size = new System.Drawing.Size(50, 25);
		this.tbSigYEnd.Text = "500";
		this.tbSigYEnd.TextBoxTextAlign = System.Windows.Forms.HorizontalAlignment.Center;
		this.tbSigYEnd.Visible = false;
		this.tbSigYEnd.Enter += new System.EventHandler(gvRltsGnl_Enter);
		this.tbSigYEnd.KeyDown += new System.Windows.Forms.KeyEventHandler(mtbSigYBeg_KeyDown);
		this.tbSigYEnd.DoubleClick += new System.EventHandler(mtbSigYBeg_DoubleClick);
		this.tbSigYEnd.TextChanged += new System.EventHandler(mtbSigYBeg_DoubleClick);
		this.lbyUnit.Margin = new System.Windows.Forms.Padding(3, 1, 0, 2);
		this.lbyUnit.Name = "lbyUnit";
		this.lbyUnit.Size = new System.Drawing.Size(27, 22);
		this.lbyUnit.Text = "mV";
		this.lbyUnit.Visible = false;
		this.ssChrom.ImageScalingSize = new System.Drawing.Size(20, 20);
		this.ssChrom.Items.AddRange(new System.Windows.Forms.ToolStripItem[2] { this.slbExplain, this.HeatValue });
		this.ssChrom.Location = new System.Drawing.Point(0, 462);
		this.ssChrom.Name = "ssChrom";
		this.ssChrom.Size = new System.Drawing.Size(1152, 22);
		this.ssChrom.TabIndex = 7;
		this.ssChrom.Text = "statusStrip1";
		this.ssChrom.Visible = false;
		this.slbExplain.Name = "slbExplain";
		this.slbExplain.Size = new System.Drawing.Size(80, 17);
		this.slbExplain.Text = "谱图处理窗口";
		this.slbExplain.Visible = false;
		this.HeatValue.Name = "HeatValue";
		this.HeatValue.Size = new System.Drawing.Size(32, 17);
		this.HeatValue.Text = "热值";
		this.cmsLibs.ImageScalingSize = new System.Drawing.Size(20, 20);
		this.cmsLibs.Items.AddRange(new System.Windows.Forms.ToolStripItem[2] { this.miAddRow, this.miDeleteRow });
		this.cmsLibs.Name = "cmsLibs";
		this.cmsLibs.ShowImageMargin = false;
		this.cmsLibs.Size = new System.Drawing.Size(88, 48);
		this.miAddRow.Name = "miAddRow";
		this.miAddRow.Size = new System.Drawing.Size(87, 22);
		this.miAddRow.Text = "添加行";
		this.miDeleteRow.Name = "miDeleteRow";
		this.miDeleteRow.Size = new System.Drawing.Size(87, 22);
		this.miDeleteRow.Text = "删除行";
		this.cmsSSTCmpds.ImageScalingSize = new System.Drawing.Size(20, 20);
		this.cmsSSTCmpds.Items.AddRange(new System.Windows.Forms.ToolStripItem[12]
		{
			this.misstcNew, this.misstcOpen, this.misstcSave, this.misstcSaveas, this.misstcUpdateFromCalib, this.toolStripSeparator34, this.misstcSet, this.tss1, this.misstColumnsSetup, this.misstRestoreDftColumns,
			this.tss2, this.misstcClearParas
		});
		this.cmsSSTCmpds.Name = "cmsSSTCmpds";
		this.cmsSSTCmpds.ShowImageMargin = false;
		this.cmsSSTCmpds.Size = new System.Drawing.Size(136, 220);
		this.misstcNew.Name = "misstcNew";
		this.misstcNew.Size = new System.Drawing.Size(135, 22);
		this.misstcNew.Text = "新建";
		this.misstcNew.Click += new System.EventHandler(misstcClearParas_Click);
		this.misstcOpen.Name = "misstcOpen";
		this.misstcOpen.Size = new System.Drawing.Size(135, 22);
		this.misstcOpen.Text = "打开";
		this.misstcOpen.Click += new System.EventHandler(misstcClearParas_Click);
		this.misstcSave.Name = "misstcSave";
		this.misstcSave.Size = new System.Drawing.Size(135, 22);
		this.misstcSave.Text = "保存";
		this.misstcSave.Click += new System.EventHandler(misstcClearParas_Click);
		this.misstcSaveas.Name = "misstcSaveas";
		this.misstcSaveas.Size = new System.Drawing.Size(135, 22);
		this.misstcSaveas.Text = "另存为";
		this.misstcSaveas.Click += new System.EventHandler(misstcClearParas_Click);
		this.misstcUpdateFromCalib.Name = "misstcUpdateFromCalib";
		this.misstcUpdateFromCalib.Size = new System.Drawing.Size(135, 22);
		this.misstcUpdateFromCalib.Text = "从分析结果提取";
		this.misstcUpdateFromCalib.Click += new System.EventHandler(misstcClearParas_Click);
		this.toolStripSeparator34.Name = "toolStripSeparator34";
		this.toolStripSeparator34.Size = new System.Drawing.Size(132, 6);
		this.misstcSet.Name = "misstcSet";
		this.misstcSet.Size = new System.Drawing.Size(135, 22);
		this.misstcSet.Text = "定量组份设置";
		this.misstcSet.Click += new System.EventHandler(misstcClearParas_Click);
		this.tss1.Name = "tss1";
		this.tss1.Size = new System.Drawing.Size(132, 6);
		this.misstColumnsSetup.Name = "misstColumnsSetup";
		this.misstColumnsSetup.Size = new System.Drawing.Size(135, 22);
		this.misstColumnsSetup.Text = "列设置";
		this.misstColumnsSetup.Click += new System.EventHandler(misstRestoreDftColumns_Click);
		this.misstRestoreDftColumns.Name = "misstRestoreDftColumns";
		this.misstRestoreDftColumns.Size = new System.Drawing.Size(135, 22);
		this.misstRestoreDftColumns.Text = "恢复列设置";
		this.misstRestoreDftColumns.Click += new System.EventHandler(misstRestoreDftColumns_Click);
		this.tss2.Name = "tss2";
		this.tss2.Size = new System.Drawing.Size(132, 6);
		this.misstcClearParas.Name = "misstcClearParas";
		this.misstcClearParas.Size = new System.Drawing.Size(135, 22);
		this.misstcClearParas.Text = "清楚参数";
		this.misstcClearParas.Click += new System.EventHandler(misstcClearParas_Click);
		this.cmsSlices.ImageScalingSize = new System.Drawing.Size(20, 20);
		this.cmsSlices.Items.AddRange(new System.Windows.Forms.ToolStripItem[2] { this.mislcColumnsSetup, this.mislcRestoreDftColumns });
		this.cmsSlices.Name = "cmsSlices";
		this.cmsSlices.ShowImageMargin = false;
		this.cmsSlices.Size = new System.Drawing.Size(112, 48);
		this.mislcColumnsSetup.Name = "mislcColumnsSetup";
		this.mislcColumnsSetup.Size = new System.Drawing.Size(111, 22);
		this.mislcColumnsSetup.Text = "列设置";
		this.mislcColumnsSetup.Click += new System.EventHandler(mislcRestoreDftColumns_Click);
		this.mislcRestoreDftColumns.Name = "mislcRestoreDftColumns";
		this.mislcRestoreDftColumns.Size = new System.Drawing.Size(111, 22);
		this.mislcRestoreDftColumns.Text = "恢复列设置";
		this.mislcRestoreDftColumns.Click += new System.EventHandler(mislcRestoreDftColumns_Click);
		this.btnReportSetup.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
		this.btnReportSetup.Image = (System.Drawing.Image)resources.GetObject("btnReportSetup.Image");
		this.btnReportSetup.ImageTransparentColor = System.Drawing.Color.Magenta;
		this.btnReportSetup.Name = "btnReportSetup";
		this.btnReportSetup.Size = new System.Drawing.Size(36, 36);
		this.btnReportSetup.Text = "toolStripButton4";
		this.btnReportSetup.ToolTipText = "设置";
		this.btnReportSetup.Click += new System.EventHandler(btnReportSetup_Click);
		this.btnPrtLink.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
		this.btnPrtLink.Image = (System.Drawing.Image)resources.GetObject("btnPrtLink.Image");
		this.btnPrtLink.ImageTransparentColor = System.Drawing.Color.Magenta;
		this.btnPrtLink.Name = "btnPrtLink";
		this.btnPrtLink.Size = new System.Drawing.Size(36, 36);
		this.btnPrtLink.Text = "toolStripButton1";
		this.btnPrtLink.Click += new System.EventHandler(btnPrtLink_Click);
		this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
		this.toolStripButton1.Image = (System.Drawing.Image)resources.GetObject("toolStripButton1.Image");
		this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
		this.toolStripButton1.Name = "toolStripButton1";
		this.toolStripButton1.Size = new System.Drawing.Size(36, 36);
		this.toolStripButton1.Text = "toolStripButton1";
		this.toolStripButton1.Click += new System.EventHandler(toolStripButton1_Click);
		this.toolStripButton2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
		this.toolStripButton2.Image = (System.Drawing.Image)resources.GetObject("toolStripButton2.Image");
		this.toolStripButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
		this.toolStripButton2.Name = "toolStripButton2";
		this.toolStripButton2.Size = new System.Drawing.Size(36, 36);
		this.toolStripButton2.Text = "toolStripButton2";
		this.toolStripButton3.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
		this.toolStripButton3.Image = (System.Drawing.Image)resources.GetObject("toolStripButton3.Image");
		this.toolStripButton3.ImageTransparentColor = System.Drawing.Color.Magenta;
		this.toolStripButton3.Name = "toolStripButton3";
		this.toolStripButton3.Size = new System.Drawing.Size(36, 36);
		this.toolStripButton3.Text = "toolStripButton3";
		this.toolStripButton4.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
		this.toolStripButton4.Image = (System.Drawing.Image)resources.GetObject("toolStripButton4.Image");
		this.toolStripButton4.ImageTransparentColor = System.Drawing.Color.Magenta;
		this.toolStripButton4.Name = "toolStripButton4";
		this.toolStripButton4.Size = new System.Drawing.Size(36, 36);
		this.toolStripButton4.Text = "toolStripButton4";
		this.toolStripButton5.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
		this.toolStripButton5.Image = (System.Drawing.Image)resources.GetObject("toolStripButton5.Image");
		this.toolStripButton5.ImageTransparentColor = System.Drawing.Color.Magenta;
		this.toolStripButton5.Name = "toolStripButton5";
		this.toolStripButton5.Size = new System.Drawing.Size(36, 36);
		this.toolStripButton5.Text = "toolStripButton5";
		this.dataGridView2.AllowUserToOrderColumns = true;
		this.dataGridView2.BackgroundColor = System.Drawing.SystemColors.Control;
		dataGridViewCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
		dataGridViewCellStyle.BackColor = System.Drawing.Color.FromArgb(128, 128, 255);
		dataGridViewCellStyle.Font = new System.Drawing.Font("宋体", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		dataGridViewCellStyle.ForeColor = System.Drawing.Color.FromArgb(128, 128, 255);
		dataGridViewCellStyle.SelectionBackColor = System.Drawing.SystemColors.Highlight;
		dataGridViewCellStyle.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
		dataGridViewCellStyle.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
		this.dataGridView2.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle;
		this.dataGridView2.ColumnHeadersHeight = 30;
		this.dataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
		this.dataGridView2.Columns.AddRange(this.dataGridViewTextBoxColumn1, this.dataGridViewTextBoxColumn2, this.dataGridViewTextBoxColumn3, this.dataGridViewTextBoxColumn4, this.dataGridViewTextBoxColumn9);
		dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
		dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
		dataGridViewCellStyle2.Font = new System.Drawing.Font("宋体", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
		dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
		dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
		dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
		this.dataGridView2.DefaultCellStyle = dataGridViewCellStyle2;
		this.dataGridView2.Location = new System.Drawing.Point(0, 0);
		this.dataGridView2.MultiSelect = false;
		this.dataGridView2.Name = "dataGridView2";
		dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
		dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
		dataGridViewCellStyle3.Font = new System.Drawing.Font("宋体", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
		dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
		dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
		dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
		this.dataGridView2.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
		this.dataGridView2.RowHeadersVisible = false;
		this.dataGridView2.RowTemplate.Height = 23;
		this.dataGridView2.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
		this.dataGridView2.Size = new System.Drawing.Size(494, 273);
		this.dataGridView2.TabIndex = 3;
		dataGridViewCellStyle4.BackColor = System.Drawing.Color.White;
		dataGridViewCellStyle4.Font = new System.Drawing.Font("宋体", 9f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 134);
		dataGridViewCellStyle4.ForeColor = System.Drawing.Color.Black;
		this.dataGridViewTextBoxColumn1.DefaultCellStyle = dataGridViewCellStyle4;
		this.dataGridViewTextBoxColumn1.HeaderText = "套峰时间";
		this.dataGridViewTextBoxColumn1.MinimumWidth = 20;
		this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
		this.dataGridViewTextBoxColumn1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.dataGridViewTextBoxColumn1.Width = 80;
		dataGridViewCellStyle5.BackColor = System.Drawing.Color.White;
		dataGridViewCellStyle5.Font = new System.Drawing.Font("宋体", 9f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 134);
		dataGridViewCellStyle5.ForeColor = System.Drawing.Color.Black;
		this.dataGridViewTextBoxColumn2.DefaultCellStyle = dataGridViewCellStyle5;
		this.dataGridViewTextBoxColumn2.HeaderText = "时间校正";
		this.dataGridViewTextBoxColumn2.MaxInputLength = 10;
		this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
		this.dataGridViewTextBoxColumn2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.dataGridViewTextBoxColumn2.Width = 80;
		dataGridViewCellStyle6.BackColor = System.Drawing.Color.White;
		dataGridViewCellStyle6.Font = new System.Drawing.Font("宋体", 9f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 134);
		dataGridViewCellStyle6.ForeColor = System.Drawing.Color.Lime;
		this.dataGridViewTextBoxColumn3.DefaultCellStyle = dataGridViewCellStyle6;
		this.dataGridViewTextBoxColumn3.HeaderText = "组份名称";
		this.dataGridViewTextBoxColumn3.MaxInputLength = 10;
		this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
		this.dataGridViewTextBoxColumn3.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.dataGridViewTextBoxColumn3.Width = 150;
		dataGridViewCellStyle7.BackColor = System.Drawing.Color.White;
		dataGridViewCellStyle7.Font = new System.Drawing.Font("宋体", 9f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 134);
		dataGridViewCellStyle7.ForeColor = System.Drawing.Color.Yellow;
		this.dataGridViewTextBoxColumn4.DefaultCellStyle = dataGridViewCellStyle7;
		this.dataGridViewTextBoxColumn4.HeaderText = "校正因子";
		this.dataGridViewTextBoxColumn4.MaxInputLength = 10;
		this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
		this.dataGridViewTextBoxColumn4.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.dataGridViewTextBoxColumn4.Width = 80;
		this.dataGridViewTextBoxColumn9.HeaderText = "ModBus地址";
		this.dataGridViewTextBoxColumn9.Name = "dataGridViewTextBoxColumn9";
		this.button8.Location = new System.Drawing.Point(499, 152);
		this.button8.Name = "button8";
		this.button8.Size = new System.Drawing.Size(75, 23);
		this.button8.TabIndex = 0;
		this.button8.Text = "删除";
		this.button8.UseVisualStyleBackColor = true;
		this.button7.Location = new System.Drawing.Point(499, 204);
		this.button7.Name = "button7";
		this.button7.Size = new System.Drawing.Size(75, 23);
		this.button7.TabIndex = 0;
		this.button7.Text = "清表";
		this.button7.UseVisualStyleBackColor = true;
		this.button6.Location = new System.Drawing.Point(499, 98);
		this.button6.Name = "button6";
		this.button6.Size = new System.Drawing.Size(75, 23);
		this.button6.TabIndex = 0;
		this.button6.Text = "取校正因子";
		this.button6.UseVisualStyleBackColor = true;
		this.button5.Location = new System.Drawing.Point(499, 37);
		this.button5.Name = "button5";
		this.button5.Size = new System.Drawing.Size(75, 23);
		this.button5.TabIndex = 0;
		this.button5.Text = "取保留时间";
		this.button5.UseVisualStyleBackColor = true;
		this.dataGridView3.AllowUserToOrderColumns = true;
		this.dataGridView3.BackgroundColor = System.Drawing.SystemColors.Control;
		dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
		dataGridViewCellStyle8.BackColor = System.Drawing.Color.FromArgb(128, 128, 255);
		dataGridViewCellStyle8.Font = new System.Drawing.Font("宋体", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		dataGridViewCellStyle8.ForeColor = System.Drawing.Color.FromArgb(128, 128, 255);
		dataGridViewCellStyle8.SelectionBackColor = System.Drawing.SystemColors.Highlight;
		dataGridViewCellStyle8.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
		dataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
		this.dataGridView3.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle8;
		this.dataGridView3.ColumnHeadersHeight = 30;
		this.dataGridView3.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
		this.dataGridView3.Columns.AddRange(this.dataGridViewTextBoxColumn10, this.dataGridViewTextBoxColumn11, this.dataGridViewTextBoxColumn12, this.dataGridViewTextBoxColumn13, this.dataGridViewTextBoxColumn14);
		dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
		dataGridViewCellStyle9.BackColor = System.Drawing.SystemColors.Window;
		dataGridViewCellStyle9.Font = new System.Drawing.Font("宋体", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		dataGridViewCellStyle9.ForeColor = System.Drawing.SystemColors.ControlText;
		dataGridViewCellStyle9.SelectionBackColor = System.Drawing.SystemColors.Highlight;
		dataGridViewCellStyle9.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
		dataGridViewCellStyle9.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
		this.dataGridView3.DefaultCellStyle = dataGridViewCellStyle9;
		this.dataGridView3.Location = new System.Drawing.Point(0, 0);
		this.dataGridView3.MultiSelect = false;
		this.dataGridView3.Name = "dataGridView3";
		dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
		dataGridViewCellStyle10.BackColor = System.Drawing.SystemColors.Control;
		dataGridViewCellStyle10.Font = new System.Drawing.Font("宋体", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		dataGridViewCellStyle10.ForeColor = System.Drawing.SystemColors.WindowText;
		dataGridViewCellStyle10.SelectionBackColor = System.Drawing.SystemColors.Highlight;
		dataGridViewCellStyle10.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
		dataGridViewCellStyle10.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
		this.dataGridView3.RowHeadersDefaultCellStyle = dataGridViewCellStyle10;
		this.dataGridView3.RowHeadersVisible = false;
		this.dataGridView3.RowTemplate.Height = 23;
		this.dataGridView3.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
		this.dataGridView3.Size = new System.Drawing.Size(494, 273);
		this.dataGridView3.TabIndex = 3;
		dataGridViewCellStyle11.BackColor = System.Drawing.Color.White;
		dataGridViewCellStyle11.Font = new System.Drawing.Font("宋体", 9f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 134);
		dataGridViewCellStyle11.ForeColor = System.Drawing.Color.Black;
		this.dataGridViewTextBoxColumn10.DefaultCellStyle = dataGridViewCellStyle11;
		this.dataGridViewTextBoxColumn10.HeaderText = "套峰时间";
		this.dataGridViewTextBoxColumn10.MinimumWidth = 20;
		this.dataGridViewTextBoxColumn10.Name = "dataGridViewTextBoxColumn10";
		this.dataGridViewTextBoxColumn10.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.dataGridViewTextBoxColumn10.Width = 80;
		dataGridViewCellStyle12.BackColor = System.Drawing.Color.White;
		dataGridViewCellStyle12.Font = new System.Drawing.Font("宋体", 9f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 134);
		dataGridViewCellStyle12.ForeColor = System.Drawing.Color.Black;
		this.dataGridViewTextBoxColumn11.DefaultCellStyle = dataGridViewCellStyle12;
		this.dataGridViewTextBoxColumn11.HeaderText = "时间校正";
		this.dataGridViewTextBoxColumn11.MaxInputLength = 10;
		this.dataGridViewTextBoxColumn11.Name = "dataGridViewTextBoxColumn11";
		this.dataGridViewTextBoxColumn11.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.dataGridViewTextBoxColumn11.Width = 80;
		dataGridViewCellStyle13.BackColor = System.Drawing.Color.White;
		dataGridViewCellStyle13.Font = new System.Drawing.Font("宋体", 9f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 134);
		dataGridViewCellStyle13.ForeColor = System.Drawing.Color.Lime;
		this.dataGridViewTextBoxColumn12.DefaultCellStyle = dataGridViewCellStyle13;
		this.dataGridViewTextBoxColumn12.HeaderText = "组份名称";
		this.dataGridViewTextBoxColumn12.MaxInputLength = 10;
		this.dataGridViewTextBoxColumn12.Name = "dataGridViewTextBoxColumn12";
		this.dataGridViewTextBoxColumn12.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.dataGridViewTextBoxColumn12.Width = 150;
		dataGridViewCellStyle14.BackColor = System.Drawing.Color.White;
		dataGridViewCellStyle14.Font = new System.Drawing.Font("宋体", 9f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 134);
		dataGridViewCellStyle14.ForeColor = System.Drawing.Color.Yellow;
		this.dataGridViewTextBoxColumn13.DefaultCellStyle = dataGridViewCellStyle14;
		this.dataGridViewTextBoxColumn13.HeaderText = "校正因子";
		this.dataGridViewTextBoxColumn13.MaxInputLength = 10;
		this.dataGridViewTextBoxColumn13.Name = "dataGridViewTextBoxColumn13";
		this.dataGridViewTextBoxColumn13.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.dataGridViewTextBoxColumn13.Width = 80;
		this.dataGridViewTextBoxColumn14.HeaderText = "ModBus地址";
		this.dataGridViewTextBoxColumn14.Name = "dataGridViewTextBoxColumn14";
		this.button3.Location = new System.Drawing.Point(499, 152);
		this.button3.Name = "button3";
		this.button3.Size = new System.Drawing.Size(75, 23);
		this.button3.TabIndex = 0;
		this.button3.Text = "删除";
		this.button3.UseVisualStyleBackColor = true;
		this.button4.Location = new System.Drawing.Point(499, 204);
		this.button4.Name = "button4";
		this.button4.Size = new System.Drawing.Size(75, 23);
		this.button4.TabIndex = 0;
		this.button4.Text = "清表";
		this.button4.UseVisualStyleBackColor = true;
		this.button9.Location = new System.Drawing.Point(499, 98);
		this.button9.Name = "button9";
		this.button9.Size = new System.Drawing.Size(75, 23);
		this.button9.TabIndex = 0;
		this.button9.Text = "取校正因子";
		this.button9.UseVisualStyleBackColor = true;
		this.button10.Location = new System.Drawing.Point(499, 37);
		this.button10.Name = "button10";
		this.button10.Size = new System.Drawing.Size(75, 23);
		this.button10.TabIndex = 0;
		this.button10.Text = "取保留时间";
		this.button10.UseVisualStyleBackColor = true;
		this.imageList_0.ImageStream = (System.Windows.Forms.ImageListStreamer)resources.GetObject("imageList_0.ImageStream");
		this.imageList_0.TransparentColor = System.Drawing.Color.Transparent;
		this.imageList_0.Images.SetKeyName(0, "下载.gif");
		this.imageList_0.Images.SetKeyName(1, "gif_47_091.gif");
		this.imageList_0.Images.SetKeyName(2, "02.png");
		this.imageList_0.Images.SetKeyName(3, "在线帮助选中.png");
		this.imageList_0.Images.SetKeyName(4, "01.png");
		this.imageList_0.Images.SetKeyName(5, "重叠打开选中.png");
		this.imageList_1.ImageStream = (System.Windows.Forms.ImageListStreamer)resources.GetObject("imageList_1.ImageStream");
		this.imageList_1.TransparentColor = System.Drawing.Color.Transparent;
		this.imageList_1.Images.SetKeyName(0, "02.png");
		this.imageList_1.Images.SetKeyName(1, "在线帮助选中.png");
		this.imageList_1.Images.SetKeyName(2, "01.png");
		this.imageList_1.Images.SetKeyName(3, "重叠打开选中.png");
		this.dpgnlChrom.BackColor = System.Drawing.Color.BlanchedAlmond;
		this.dpgnlChrom.Cursor = System.Windows.Forms.Cursors.Default;
		this.dpgnlChrom.Dock = System.Windows.Forms.DockStyle.Fill;
		this.dpgnlChrom.Location = new System.Drawing.Point(0, 37);
		this.dpgnlChrom.Name = "dpgnlChrom";
		this.dpgnlChrom.Size = new System.Drawing.Size(1152, 447);
		this.dpgnlChrom.TabIndex = 9;
		this.dpgnlChrom.Click += new System.EventHandler(dpgpcChrom_Click);
		this.dpgnlChrom.Paint += new System.Windows.Forms.PaintEventHandler(dpgpcChrom_Paint);
		this.dpgnlChrom.DoubleClick += new System.EventHandler(dpgpcChrom_DoubleClick);
		this.dpgnlChrom.MouseDown += new System.Windows.Forms.MouseEventHandler(dpgpcChrom_MouseDown);
		this.dpgnlChrom.MouseLeave += new System.EventHandler(dpgpcChrom_MouseLeave);
		this.dpgnlChrom.MouseMove += new System.Windows.Forms.MouseEventHandler(dpgpcChrom_MouseMove);
		this.dpgnlChrom.MouseUp += new System.Windows.Forms.MouseEventHandler(dpgpcChrom_MouseUp);
		this.dpgnlChrom.Resize += new System.EventHandler(dpgnlChrom_Resize);
		this.chromDataGrid.GetcbNMHCChecked = null;
		this.chromDataGrid.GetChromatogram = null;
		this.chromDataGrid.GetChromatogramList = null;
		this.chromDataGrid.GetChromDisplay = null;
		this.chromDataGrid.GetHasChrom = null;
		this.chromDataGrid.GetSmyTabOpt = null;
		this.chromDataGrid.Location = new System.Drawing.Point(150, 640);
		this.chromDataGrid.Margin = new System.Windows.Forms.Padding(4);
		this.chromDataGrid.mySetslbExplainText = null;
		this.chromDataGrid.Name = "chromDataGrid";
		this.chromDataGrid.Size = new System.Drawing.Size(104, 17);
		this.chromDataGrid.TabIndex = 0;
		this.mstSetChromForm.devManager = (IBrainChrom2018.InsDeviceManager)resources.GetObject("mstSetChromForm.devManager");
		this.mstSetChromForm.Location = new System.Drawing.Point(902, 78);
		this.mstSetChromForm.Margin = new System.Windows.Forms.Padding(4);
		this.mstSetChromForm.Name = "mstSetChromForm";
		this.mstSetChromForm.PrintMethod = null;
		this.mstSetChromForm.ShowComponentTable = false;
		this.mstSetChromForm.ShowMethodNew = true;
		this.mstSetChromForm.ShowOnlineMethod = false;
		this.mstSetChromForm.ShowOnlineMethod2 = false;
		this.mstSetChromForm.Size = new System.Drawing.Size(55, 70);
		this.mstSetChromForm.TabIndex = 15;
		this.mstSetChromForm.Visible = false;
		this.mstSetChromForm.OnMethodSaveEvent += new System.EventHandler(mstSetChromForm_OnMethodSaveEvent);
		this.mstSetChromForm.OnUseSet += new System.EventHandler(mstSetChromForm_OnUseSet);
		base.Controls.Add(this.dpgnlChrom);
		base.Controls.Add(this.chromDataGrid);
		base.Controls.Add(this.mstSetChromForm);
		base.Controls.Add(this.tsDatAcq);
		base.Controls.Add(this.flpChrom);
		base.Controls.Add(this.ssChrom);
		base.Controls.Add(this.msChrom);
		base.Name = "MineChromCtrl";
		base.Size = new System.Drawing.Size(1152, 484);
		base.Load += new System.EventHandler(ChromForm_Load);
		base.KeyDown += new System.Windows.Forms.KeyEventHandler(ChromForm_KeyDown);
		this.msChrom.ResumeLayout(false);
		this.msChrom.PerformLayout();
		this.flpChrom.ResumeLayout(false);
		this.flpChrom.PerformLayout();
		this.toolStrip2.ResumeLayout(false);
		this.toolStrip2.PerformLayout();
		this.toolStrip1.ResumeLayout(false);
		this.toolStrip1.PerformLayout();
		this.tsDatAcq.ResumeLayout(false);
		this.tsDatAcq.PerformLayout();
		this.ssChrom.ResumeLayout(false);
		this.ssChrom.PerformLayout();
		this.cmsLibs.ResumeLayout(false);
		this.cmsSSTCmpds.ResumeLayout(false);
		this.cmsSlices.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.dataGridView2).EndInit();
		((System.ComponentModel.ISupportInitialize)this.dataGridView3).EndInit();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
