using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing.Printing;
using System.Drawing.Text;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using IBrainChrom2018.Properties;
using IBrainChrom2018.Unit;
using Microsoft.Office.Interop.Word;
using NPOI.HSSF.UserModel;
using NPOI.OpenXmlFormats.Wordprocessing;
using NPOI.SS.UserModel;
using NPOI.XWPF.UserModel;

namespace IBrainChrom2018;

public class ChromFormCtrl : UserControl
{
	public string strDetec = "";

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

	public static ChromFormCtrl form = null;

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

	private CmpdDisplay gCmpdDisplay = new CmpdDisplay(WinStyle.CaliGnl, null);

	private RectangleF rectangleF_1 = default(RectangleF);

	private Pen pen_0 = new Pen(Color.Black, 1f);

	private SizeF sizeF_0 = new SizeF(350f, 330f);

	private SizeF sizeF_1 = new SizeF(550f, 230f);

	private SizeF sizeF_2 = new SizeF(450f, 190f);

	private SolidBrush solidBrush_0 = new SolidBrush(Color.Black);

	public Chromatogram chromatogram_1;

	private Chromatogram[] chromatogram_0 = new Chromatogram[0];

	public ChromDisplay chromDisplay_0;

	private RectangleF rectangleF_0;

	private IntegRow integRow_0;

	private IntegRow integRow_1;

	private IntegRow integRow_2;

	private SmyTabOpt smyTabOpt_0 = new SmyTabOpt();

	private Options options_0 = new Options();

	private Bitmap bitmap_0;

	private Bitmap bitmap_1;

	private Bitmap bitmap_2;

	private int int_7;

	private bool bool_6;

	private bool bool_7;

	private bool bool_8;

	private bool bool_9;

	private int int_4;

	private int int_9;

	private int int_10;

	private int int_11;

	private int int_12;

	private int int_13;

	private int int_14;

	private int int_0;

	private int int_1;

	private int int_3;

	private int int_2;

	private int iIndexCmpd;

	private float float_2;

	private System.Drawing.Rectangle rectangle_2;

	private static System.Drawing.Rectangle rectangle_1;

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

	private LclSplitContainer splitContainer;

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

	private System.Windows.Forms.CheckBox cChangePeak;

	private SplitContainer splitContainer1;

	private ImageList imageList_1;

	private ToolStripSeparator toolStripSeparator7;

	private ToolStripButton toolStripButton6;

	private SplitContainer splitContainer2;

	private ToolStripStatusLabel HeatValue;

	private SplitContainer splitContainer4;

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

	private ToolStripButton toolStripButton8;

	private Button bExportXY;

	private Button btUpdataByXX;

	public RptSetupDlg dlgReportSetup;

	private IContainer components;

	public System.Windows.Forms.CheckBox cbNMHC;

	public ChromFormDataGrid chromDataGrid;

	private ChromFormFileSearchCtrl chromFormFileSearchCtrl1;

	public MstSet mstSetChromForm;

	private Button btnGnl;

	private Button btnGnl2;

	private Button btnConvert;

	private Button btnUploading;

	private System.Windows.Forms.CheckBox chbTVOC;

	private PrintDialog printDialog_0;

	private PrintDocument printDocument_0;

	private PrintPreviewDialog prtPrvDlg;

	private Button btnSaveDb;

	private ToolStripButton btnOutputExcel;

	private ToolStripMenuItem 导出谱点文件ToolStripMenuItem;

	private ToolStripButton btnPrint;

	private Label labmouseLgvalue;

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
				ssChrom.Visible = true;
			}
			else
			{
				msChrom.Visible = false;
				ssChrom.Visible = false;
			}
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

	public void Reload()
	{
		chromDataGrid.reLoad();
	}

	public ChromFormCtrl()
	{
		InitializeComponent();
		form = this;
		if (!IsDesignMode())
		{
			InitChromFormDataGrid();
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
		if (strSdaDataFileDir == "")
		{
			strSdaDataFileDir = System.Windows.Forms.Application.StartupPath;
		}
		splitContainer2.Panel1Collapsed = true;
		lbyUnit.Text = Class49.MesureUnit();
		toolStripMenuItem_1.Text = Lang.PS("标准按钮", "Stand. Buttons");
		toolStripMenuItem_1.Click += toolStripMenuItem_1_Click;
		toolStripMenuItem_1_Click(null, null);
		toolStripMenuItem_0.Text = Lang.PS("手动积分按钮", "Manual itg. Buttons");
		toolStripMenuItem_0.Click += toolStripMenuItem_0_Click;
		ofdChrom.Title = Lang.PS("打开谱图", "Open Chromatogram");
		ofdChrom.Filter = "(*.sda)|*.sda|(所有文件)|*.*";
		ofdChrom.FilterIndex = 1;
		ofdChrom.Multiselect = true;
		openFileDialog_3.Title = Lang.PS("打开SST文件", "Open SST File");
		openFileDialog_3.Filter = Class49.MakeFileFilter(".sst");
		openFileDialog_3.Multiselect = false;
		saveFileDialog_1.Title = Lang.PS("保存SST文件", "Save SST File");
		saveFileDialog_1.Filter = Class49.MakeFileFilter(".sst");
		btnOutputExcel.ToolTipText = Lang.PS("导出Excel", "Export Excel");
		导出谱点文件ToolStripMenuItem.Text = Lang.PS("导出谱点文件", "Export the spectral point file");
		chromDisplay_0 = new ChromDisplay(WinStyle.Chromatogram, dpgnlChrom);
		chromDisplay_0.OnSignalClick += method_3;
		chromDisplay_0.OnSignalDoubleClick += method_4;
		chromDisplay_0.showMouseLgValue = false;
		chromDisplay_0.showProgTemp = false;
		chromDisplay_0.ExtDraw_begin();
		LoadOptions();
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
		chbTVOC.Checked = frmParam.bTVOC;
		chbTVOC.Visible = false;
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
		btnUploading.Text = Lang.PS("上传", "Up DCS");
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
		toolStripButton8.ToolTipText = Lang.PS("查找打开", "Looking for open");
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
		btnbsBsBackHorz.ToolTipText = Lang.PS("向后水平", "BsBackHorz");
		btnbsBsFrontTgnt.ToolTipText = Lang.PS("前切", "BsFrontTgnt");
		btnbsBsForwHorz.ToolTipText = Lang.PS("向前水平", "BsForwHorz");
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
		btUpdataByXX.Text = Lang.PS("上传到DCS", "UpLoad To DCS");
		btnXYFull.Text = Lang.PS("满屏", "YFull");
		label32.Text = Lang.PS("满屏", "TimeFull");
		btnTimeFull.Text = Lang.PS("满屏", "XFull");
		bExportXY.Text = Lang.PS("导出谱图点", "ExportXY");
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
		chromDataGrid.lbExpress.Text = Lang.PS("取值", "Suggest Value");
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
			CurChrom.integ.AppendRow(integRow_2);
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
				method_27(chromatogram_0, 0);
				SetSignalsColor();
				chromDataGrid.tcChrom_SelectedIndexChanged(null, null);
				break;
			}
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
		ofdChrom.InitialDirectory = strSdaDataFileDir;
		try
		{
			if (ofdChrom.ShowDialog(this) != DialogResult.OK)
			{
				return;
			}
		}
		catch
		{
			ofdChrom.InitialDirectory = System.Windows.Forms.Application.StartupPath;
			if (ofdChrom.ShowDialog(this) != DialogResult.OK)
			{
				return;
			}
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

	public void setOverlayMode()
	{
		ToolStripButton toolStripButton = btnOverlayMode;
		bool flag = (miFiOverlayMode.Checked = true);
		toolStripButton.Checked = flag;
		if (btnOverlayMode.Checked)
		{
			btnOverlayMode.Image = imageList_1.Images[3];
		}
		else
		{
			btnOverlayMode.Image = imageList_1.Images[2];
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
			else
			{
				if (mstSetChromForm.radioButton4.Checked)
				{
					PrintToWord(CurChrom, m_strChormFileName);
					return;
				}
				frmParam.bTVOC = false;
				if (frmParam.bTVOC)
				{
					prtPrvDlg = new PrintPreviewDialog();
					prtPrvDlg.Document = printDocument_0;
					prtPrvDlg.ShowIcon = false;
					prtPrvDlg.TopMost = true;
					prtPrvDlg.Show();
					prtPrvDlg.Activate();
				}
				else
				{
					prtPrvDlg = new PrintPreviewDialog();
					prtPrvDlg.Document = printDocument_0;
					prtPrvDlg.ShowIcon = false;
					prtPrvDlg.TopMost = true;
					prtPrvDlg.Show();
					prtPrvDlg.Activate();
				}
			}
			Class49.InsertIntoTable(Class49.string_9[3], Class49.user_0.u_name, "", "谱图打印", "谱图打印:" + CurChrom.fullName);
		}
		catch (Exception)
		{
		}
	}

	public void printToExcel()
	{
		SaveFileDialog saveFileDialog = new SaveFileDialog();
		saveFileDialog.FileName = System.Windows.Forms.Application.StartupPath + "\\" + DateTime.Now.ToString("yyyyMMddHHmm") + ".xls";
		saveFileDialog.Filter = " csv files(*.csv)|*.csv|All files(*.*)|*.*";
		saveFileDialog.FilterIndex = 2;
		saveFileDialog.RestoreDirectory = true;
		if (saveFileDialog.ShowDialog() == DialogResult.OK)
		{
			int num = 0;
			if (num < 1)
			{
				num = 1;
			}
			FileStream fileStream = new FileStream(System.Windows.Forms.Application.StartupPath + "\\quanfenxibaogao.xls", FileMode.Open, FileAccess.Read);
			HSSFWorkbook hSSFWorkbook = new HSSFWorkbook(fileStream);
			ISheet sheetAt = hSSFWorkbook.GetSheetAt(0);
			sheetAt.ForceFormulaRecalculation = true;
			FileStream fileStream2 = new FileStream(saveFileDialog.FileName, FileMode.Create);
			hSSFWorkbook.Write(fileStream2);
			fileStream.Close();
			fileStream2.Close();
			dataToExcel(saveFileDialog.FileName);
			if (File.Exists(saveFileDialog.FileName))
			{
				Process.Start(saveFileDialog.FileName);
			}
		}
	}

	public bool dataToExcel(string Outpath)
	{
		bool result = false;
		IWorkbook workbook = null;
		FileStream fileStream = null;
		IRow row = null;
		IRow row2 = null;
		ISheet sheet = null;
		NPOI.SS.UserModel.ICell cell = null;
		NPOI.SS.UserModel.ICell cell2 = null;
		bool flag = false;
		double num = 0.0;
		FileStream fileStream2 = new FileStream(Outpath, FileMode.Open, FileAccess.ReadWrite);
		try
		{
			if (CurChrom != null)
			{
				Bitmap bitmap = new Bitmap(864, 190);
				Graphics graphics = Graphics.FromImage(bitmap);
				if (CurChrom.PPara.bPicBound)
				{
					graphics.SmoothingMode = SmoothingMode.HighQuality;
					graphics.CompositingQuality = CompositingQuality.HighQuality;
					graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
					graphics.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
					graphics.DrawRectangle(new Pen(Color.Black, 2f), 0, 0, bitmap.Width - 15, bitmap.Height - 15);
				}
				int setChromNo = chromatogram_0.Length;
				RectangleF rectangleF = new RectangleF(0f, 0f, 849f, 175f);
				ChromDisplay chromDisplay = new ChromDisplay(WinStyle.Chromatogram, null);
				chromDisplay.LinkDisChroms(chromatogram_0, ref setChromNo);
				chromDisplay.showMouseLgValue = false;
				chromDisplay.showProgTemp = false;
				chromDisplay.ShowBgChrom = true;
				chromDisplay.setShowGrid = true;
				chromDisplay.showPeakArea = false;
				chromDisplay.rcPage = rectangleF;
				chromDisplay.dskRC = rectangleF;
				chromDisplay.FrmPen.Width = 1f;
				chromDisplay.DisPen.Width = 1f;
				System.Drawing.Font peakFont = chromDisplay.options.peakFont;
				chromDisplay.options.peakFont = new System.Drawing.Font(peakFont.FontFamily, peakFont.Size * 1f, peakFont.Style);
				chromDisplay.Draw(graphics, erase: true);
				bitmap.Save(System.Windows.Forms.Application.StartupPath + "\\a1.Emf");
				Peak[] peakAllCompound = CurChrom.GetPeakAllCompound();
				workbook = new HSSFWorkbook(fileStream2);
				sheet = workbook.GetSheetAt(0);
				int num2 = peakAllCompound.Length;
				int num3 = 6;
				byte[] pictureData = File.ReadAllBytes(System.Windows.Forms.Application.StartupPath + "\\a1.Emf");
				int pictureIndex = workbook.AddPicture(pictureData, NPOI.SS.UserModel.PictureType.EMF);
				IDrawing drawing = sheet.CreateDrawingPatriarch();
				row2 = sheet.GetRow(0);
				cell2 = row2.GetCell(0);
				row2 = sheet.GetRow(1);
				cell2 = row2.GetCell(3);
				cell2.SetCellValue(DateTime.Now.ToString());
				row2 = sheet.GetRow(4);
				cell2 = row2.GetCell(1);
				cell2.SetCellValue(chromatogram_1.injAnalysis.dtAcquire.ToString());
				row = sheet.GetRow(5);
				for (int i = 8; i < num2 + 8; i++)
				{
					if (i > 7)
					{
						IRow row3 = sheet.GetRow(8);
						MyInsertRow(sheet, i + 1, 1, row3);
					}
					row = sheet.GetRow(i);
					if (row == null)
					{
						row = sheet.CreateRow(i);
					}
					for (int j = 0; j <= num3; j++)
					{
						cell = row.GetCell(j);
						if (cell == null)
						{
							cell = row.CreateCell(j);
						}
						switch (j)
						{
						case 0:
							cell.SetCellValue(peakAllCompound[i - 8].name);
							break;
						case 1:
							cell.SetCellValue(peakAllCompound[i - 8].area.ToString("F" + Class49.int_8));
							break;
						case 2:
							cell.SetCellValue(peakAllCompound[i - 8].amount.ToString("F" + Class49.int_8));
							break;
						case 3:
							cell.SetCellValue(Math.Abs(peakAllCompound[i - 8].GasAmount - peakAllCompound[i - 8].amount).ToString("F" + Class49.int_8));
							break;
						case 4:
							cell.SetCellValue(peakAllCompound[i - 8].compound.cmpdInfo.CriticalAmount.ToString("F" + Class49.int_8));
							break;
						case 5:
							cell.SetCellValue(peakAllCompound[i - 8].StrResult);
							break;
						}
					}
				}
				HSSFClientAnchor anchor = new HSSFClientAnchor(0, 0, 1023, 255, 1, num2 + 10, 5, num2 + 10);
				IPicture picture = drawing.CreatePicture(anchor, pictureIndex);
				using (fileStream = File.OpenWrite(Outpath))
				{
					workbook.Write(fileStream);
					result = true;
				}
				fileStream2.Close();
			}
			return result;
		}
		catch (Exception)
		{
			using (fileStream = File.OpenWrite(Outpath))
			{
				workbook.Write(fileStream);
				result = true;
			}
			fileStream.Close();
			return false;
		}
	}

	public static void MyInsertRow(ISheet sheet, int 插入行, int 插入行总数, IRow 源格式行)
	{
		sheet.ShiftRows(插入行, sheet.LastRowNum, 插入行总数, copyRowHeight: true, resetOriginalRowHeight: false);
		for (int i = 插入行; i < 插入行 + 插入行总数 - 1; i++)
		{
			IRow row = null;
			NPOI.SS.UserModel.ICell cell = null;
			NPOI.SS.UserModel.ICell cell2 = null;
			row = sheet.CreateRow(i + 1);
			for (int j = 源格式行.FirstCellNum; j < 源格式行.LastCellNum; j++)
			{
				cell = 源格式行.GetCell(j);
				if (cell != null)
				{
					cell2 = row.CreateCell(j);
					cell2.CellStyle = cell.CellStyle;
					cell2.SetCellType(cell.CellType);
				}
			}
		}
		IRow row2 = sheet.GetRow(插入行);
		NPOI.SS.UserModel.ICell cell3 = null;
		NPOI.SS.UserModel.ICell cell4 = null;
		for (int k = 源格式行.FirstCellNum; k < 源格式行.LastCellNum; k++)
		{
			cell3 = 源格式行.GetCell(k);
			if (cell3 != null)
			{
				cell4 = row2.CreateCell(k);
				cell4.CellStyle = cell3.CellStyle;
				cell4.SetCellType(cell3.CellType);
			}
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
				array[i + 1].GetRow(j + 2).GetCell(3).SetText(array4[j].ToString("F" + Class49.int_8));
				array[i + 1].GetRow(j + 2).GetCell(4).SetText((array5[j] * 100f).ToString("F" + Class49.int_8));
				array[i + 1].GetRow(j + 2).GetCell(5).SetText(array6[j].ToString("0.000"));
				array[i + 1].GetRow(j + 2).GetCell(6).SetText((array7[j] * 100f).ToString("F" + Class49.int_8));
				array[i + 1].GetRow(j + 2).GetCell(7).SetText(array8[j].ToString("F" + Class49.int_8));
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
					dataTable.Rows[k][num2] = rltPeaks[k].pkRT.ToString("F" + Class49.int_8);
				}
				num2++;
			}
			if (CurChrom.PPara.bPeakArea)
			{
				dataTable.Columns.Add("峰面积");
				for (int l = 0; l < num; l++)
				{
					dataTable.Rows[l][num2] = rltPeaks[l].area.ToString("F" + Class49.int_8);
				}
				dataTable.Rows[num][num2] = CurChrom.whlArea.ToString("F" + Class49.int_8);
				num2++;
			}
			if (CurChrom.PPara.bPeakheight)
			{
				dataTable.Columns.Add("峰高");
				for (int m = 0; m < num; m++)
				{
					dataTable.Rows[m][num2] = rltPeaks[m].height.ToString("F" + Class49.int_8);
				}
				dataTable.Rows[num][num2] = CurChrom.whlHeight.ToString("F" + Class49.int_8);
				num2++;
			}
			if (CurChrom.PPara.bPeakHalfheight)
			{
				dataTable.Columns.Add("半峰宽");
				for (int n = 0; n < num; n++)
				{
					dataTable.Rows[n][num2] = rltPeaks[n].WO5.ToString("F" + Class49.int_8);
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
						dataTable.Rows[num3][num2] = rltPeaks[num3].amount.ToString("F" + Class49.int_8);
					}
					else
					{
						dataTable.Rows[num3][num2] = "";
					}
				}
				dataTable.Rows[num][num2] = CurChrom.whlAmount.ToString("F" + Class49.int_8);
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
					dataTable.Rows[num5][num2] = rltPeaks[num5].Capacity.ToString("F" + Class49.int_8);
				}
				num2++;
			}
			if (CurChrom.PPara.bPeakLV)
			{
				dataTable.Columns.Add("峰分离度");
				for (int num6 = 0; num6 < num; num6++)
				{
					dataTable.Rows[num6][num2] = rltPeaks[num6].Resolution_EP.ToString("F" + Class49.int_8);
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
						dataTable.Rows[num8][num2] = rltPeaks[num8].compound.cmpdInfo.freeRespFactor.ToString("F" + Class49.int_8);
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
					dataTable.Rows[num9][num2] = rltPeaks[num9].SymmetryTailing.ToString("F" + Class49.int_8);
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
					dataTable.Rows[num13][num2] = (rltPeaks[num13].heightPer * 100f).ToString("F" + Class49.int_8) + "%";
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
					dataTable.Rows[num14][num2] = (rltPeaks[num14].areaPer * 100f).ToString("F" + Class49.int_8);
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
						dataTable.Rows[num15][num2] = (rltPeaks[num15].amountPer * 100f).ToString("F" + Class49.int_8);
					}
					else
					{
						dataTable.Rows[num15][num2] = "";
					}
				}
				if (CurChrom.whlAmountPer != -1f)
				{
					dataTable.Rows[num][num2] = CurChrom.whlAmountPer.ToString("F" + Class49.int_8);
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

	public void PrintToWordTVOC(Chromatogram chromatogram, string strFileName)
	{
		int num = 0;
		int num2 = 0;
		float num3 = 0f;
		PrintPara pPara = chromatogram_0[0].PPara;
		int setChromNo = chromatogram_0.Length;
		Bitmap bitmap = new Bitmap(864, 340);
		Graphics graphics = Graphics.FromImage(bitmap);
		RectangleF rectangleF = new RectangleF(0f, 0f, 849f, 325f);
		ChromDisplay chromDisplay = new ChromDisplay(WinStyle.Chromatogram, null);
		chromDisplay.LinkDisChroms(chromatogram_0, ref setChromNo);
		chromDisplay.showMouseLgValue = false;
		chromDisplay.showProgTemp = false;
		chromDisplay.ShowBgChrom = true;
		chromDisplay.setShowGrid = true;
		chromDisplay.showPeakArea = false;
		chromDisplay.rcPage = rectangleF;
		chromDisplay.dskRC = rectangleF;
		chromDisplay.FrmPen.Width = 1f;
		chromDisplay.DisPen.Width = 1f;
		System.Drawing.Font peakFont = chromDisplay.options.peakFont;
		chromDisplay.options.peakFont = new System.Drawing.Font(peakFont.FontFamily, peakFont.Size * 1f, peakFont.Style);
		chromDisplay.Draw(graphics, erase: true);
		bitmap.Save(System.Windows.Forms.Application.StartupPath + "\\a1.Emf");
		string text = pPara.Title + DateTime.Now.ToString("yyyyMMdd-HHmmss") + ".docx";
		FileStream fileStream = File.OpenWrite(text);
		fileStream.Position = 0L;
		XWPFDocument xWPFDocument = new XWPFDocument();
		xWPFDocument.Document.body.sectPr = new CT_SectPr();
		CT_SectPr sectPr = xWPFDocument.Document.body.sectPr;
		XWPFParagraph xWPFParagraph = xWPFDocument.CreateParagraph();
		CT_Ftr cT_Ftr = new CT_Ftr();
		cT_Ftr.Items = new ArrayList();
		CT_SdtBlock cT_SdtBlock = new CT_SdtBlock();
		CT_SdtPr cT_SdtPr = cT_SdtBlock.AddNewSdtPr();
		CT_SdtDocPart cT_SdtDocPart = cT_SdtPr.AddNewDocPartObj();
		cT_SdtDocPart.AddNewDocPartGallery().val = "PageNumbers (Bottom of Page)";
		cT_SdtDocPart.docPartUnique = new CT_OnOff();
		CT_SdtContentBlock cT_SdtContentBlock = cT_SdtBlock.AddNewSdtContent();
		CT_P cT_P = cT_SdtContentBlock.AddNewP();
		CT_PPr cT_PPr = cT_P.AddNewPPr();
		cT_PPr.AddNewJc().val = ST_Jc.center;
		cT_P.Items = new ArrayList();
		CT_SimpleField cT_SimpleField = new CT_SimpleField();
		cT_SimpleField.instr = " PAGE   \\*MERGEFORMAT ";
		cT_P.Items.Add(cT_SimpleField);
		cT_Ftr.Items.Add(cT_SdtBlock);
		XWPFRelation fOOTER = XWPFRelation.FOOTER;
		XWPFFooter xWPFFooter = (XWPFFooter)xWPFDocument.CreateRelationship(fOOTER, XWPFFactory.GetInstance(), xWPFDocument.FooterList.Count + 1);
		xWPFFooter.SetHeaderFooter(cT_Ftr);
		CT_HdrFtrRef cT_HdrFtrRef = sectPr.AddNewFooterReference();
		cT_HdrFtrRef.type = ST_HdrFtr.@default;
		cT_HdrFtrRef.id = xWPFFooter.GetPackageRelationship().Id;
		XWPFParagraph xWPFParagraph2 = xWPFDocument.CreateParagraph();
		xWPFParagraph2.Alignment = ParagraphAlignment.CENTER;
		XWPFRun xWPFRun = xWPFParagraph2.CreateRun();
		xWPFRun.FontSize = 16;
		xWPFRun.IsBold = true;
		xWPFRun.AppendText(pPara.Title);
		XWPFParagraph xWPFParagraph3 = xWPFDocument.CreateParagraph();
		xWPFParagraph3.Alignment = ParagraphAlignment.LEFT;
		XWPFRun xWPFRun2 = xWPFParagraph3.CreateRun();
		xWPFRun2.AddCarriageReturn();
		xWPFRun2.SetText(pPara.PrintTitleTop);
		xWPFRun2.AddCarriageReturn();
		if (CurChrom.PPara.bPTime)
		{
			xWPFRun2.AppendText("打印时间:" + DateTime.Now.ToString());
			xWPFRun2.AddCarriageReturn();
		}
		if (CurChrom.PPara.bJtime)
		{
			xWPFRun2.AppendText("进样时间:" + chromatogram_1.injAnalysis.dtAcquire.ToString());
			xWPFRun2.AddCarriageReturn();
		}
		if (CurChrom.PPara.bfname)
		{
			xWPFRun2.AppendText("打开的谱图文件:" + m_strChormFileName);
			xWPFRun2.AddCarriageReturn();
		}
		xWPFRun2.AppendText("采样点温度:" + frmParam.fTemp + "℃    ");
		xWPFRun2.AppendText("采样点大气压:" + frmParam.fAtm + "Kpa    ");
		xWPFRun2.AddCarriageReturn();
		xWPFRun2.AppendText("采样体积:" + frmParam.fInjectionVolume + "L");
		int num4 = 5715000;
		int num5 = 2857500;
		string text2 = System.Windows.Forms.Application.StartupPath + "\\a1.Emf";
		FileStream fileStream2 = new FileStream(text2, FileMode.Open, FileAccess.Read);
		xWPFRun2.AddPicture(fileStream2, 2, text2, num4, num5);
		fileStream2.Close();
		xWPFRun2.AddCarriageReturn();
		Peak[] rltPeaks = chromatogram_0[0].RltPeaks;
		XWPFTable[] array = new XWPFTable[rltPeaks.Length];
		num = 0;
		if (pPara.bIndex)
		{
			num++;
		}
		if (pPara.bPeakName)
		{
			num++;
		}
		if (pPara.bPeakMaxTime)
		{
			num++;
		}
		if (pPara.bPeakArea)
		{
			num++;
		}
		if (pPara.bPeakheight)
		{
			num++;
		}
		if (pPara.bPeakHalfheight)
		{
			num++;
		}
		if (pPara.bPeakAmont)
		{
			num++;
		}
		if (frmParam.bTVOC)
		{
			num++;
		}
		if (pPara.bPeakFx)
		{
			num++;
		}
		if (pPara.bPeakLPara)
		{
			num++;
		}
		if (pPara.bPeakLV)
		{
			num++;
		}
		if (pPara.bPeakOtherPara)
		{
			num++;
		}
		if (pPara.bPeakPara)
		{
			num++;
		}
		if (pPara.bPeaktailPara)
		{
			num++;
		}
		if (pPara.bPeakTBPara)
		{
			num++;
		}
		if (pPara.bPeakUTBPara)
		{
			num++;
		}
		if (pPara.bPeakV)
		{
			num++;
		}
		if (pPara.bPeakheightPer)
		{
			num++;
		}
		if (pPara.bPeakAreaPer)
		{
			num++;
		}
		if (pPara.bPeakAmontPer)
		{
			num++;
		}
		array[0] = xWPFDocument.CreateTable(1, num);
		num2 = 0;
		if (pPara.bIndex)
		{
			num2++;
			array[0].GetRow(0).GetCell(num2 - 1).SetText(" 序号 ");
		}
		if (pPara.bPeakName)
		{
			num2++;
			array[0].GetRow(0).GetCell(num2 - 1).SetText(" 名称 ");
		}
		if (pPara.bPeakMaxTime)
		{
			num2++;
			array[0].GetRow(0).GetCell(num2 - 1).SetText(" 保留时间(min) ");
		}
		if (pPara.bPeakArea)
		{
			num2++;
			array[0].GetRow(0).GetCell(num2 - 1).SetText(" 峰面积 ");
		}
		if (pPara.bPeakheight)
		{
			num2++;
			array[0].GetRow(0).GetCell(num2 - 1).SetText(" 峰高 ");
		}
		if (pPara.bPeakHalfheight)
		{
			num2++;
			array[0].GetRow(0).GetCell(num2 - 1).SetText(" 半峰宽 ");
		}
		if (pPara.bPeakAmont)
		{
			num2++;
			array[0].GetRow(0).GetCell(num2 - 1).SetText(" 浓度 ");
		}
		if (frmParam.bTVOC)
		{
			num2++;
			array[0].GetRow(0).GetCell(num2 - 1).SetText(" 标准状态(mg/m³)");
		}
		if (pPara.bPeakFx)
		{
			num2++;
			array[0].GetRow(0).GetCell(num2 - 1).SetText(" 工作曲线方程 ");
		}
		if (pPara.bPeakLPara)
		{
			num2++;
			array[0].GetRow(0).GetCell(num2 - 1).SetText(" 容量因子 ");
		}
		if (pPara.bPeakLV)
		{
			num2++;
			array[0].GetRow(0).GetCell(num2 - 1).SetText(" 峰分离度 ");
		}
		if (pPara.bPeakOtherPara)
		{
			num2++;
			array[0].GetRow(0).GetCell(num2 - 1).SetText(" 相关系数 ");
		}
		if (pPara.bPeakPara)
		{
			num2++;
			array[0].GetRow(0).GetCell(num2 - 1).SetText(" 校正因子 ");
		}
		if (pPara.bPeaktailPara)
		{
			num2++;
			array[0].GetRow(0).GetCell(num2 - 1).SetText(" 拖尾因子 ");
		}
		if (pPara.bPeakTBPara)
		{
			num2++;
			array[0].GetRow(0).GetCell(num2 - 1).SetText(" 理论塔板数 ");
		}
		if (pPara.bPeakUTBPara)
		{
			num2++;
			array[0].GetRow(0).GetCell(num2 - 1).SetText(" 有效塔板数 ");
		}
		if (pPara.bPeakV)
		{
			num2++;
			array[0].GetRow(0).GetCell(num2 - 1).SetText(" 峰标志 ");
		}
		if (pPara.bPeakheightPer)
		{
			num2++;
			array[0].GetRow(0).GetCell(num2 - 1).SetText(" 峰高百分比 ");
		}
		if (pPara.bPeakAreaPer)
		{
			num2++;
			array[0].GetRow(0).GetCell(num2 - 1).SetText(" 面积百分比 ");
		}
		if (pPara.bPeakAmontPer)
		{
			num2++;
			array[0].GetRow(0).GetCell(num2 - 1).SetText(" 浓度百分比 ");
		}
		for (int i = 0; i < num; i++)
		{
			CT_Tc cTTc = array[0].GetRow(0).GetCell(i).GetCTTc();
			CT_TcPr cT_TcPr = cTTc.AddNewTcPr();
			cT_TcPr.tcW = new CT_TblWidth();
			cT_TcPr.tcW.w = "6000";
			cT_TcPr.tcW.type = ST_TblWidth.dxa;
		}
		XWPFParagraph xWPFParagraph4 = xWPFDocument.CreateParagraph();
		XWPFRun xWPFRun3 = xWPFParagraph4.CreateRun();
		xWPFRun3.AddCarriageReturn();
		int num6 = 0;
		float num7 = 0f;
		float num8 = 0f;
		double num9 = 0.0;
		for (int j = 0; j < rltPeaks.Length; j++)
		{
			if (rltPeaks[j].compound == null)
			{
				num7 += rltPeaks[j].area;
				continue;
			}
			array[0].CreateRow();
			num2 = 0;
			if (pPara.bIndex)
			{
				num2++;
				array[0].GetRow(num6 + 1).GetCell(num2 - 1).SetText((num6 + 1).ToString());
			}
			if (pPara.bPeakName)
			{
				num2++;
				array[0].GetRow(num6 + 1).GetCell(num2 - 1).SetText(rltPeaks[j].name);
			}
			if (pPara.bPeakMaxTime)
			{
				num2++;
				array[0].GetRow(num6 + 1).GetCell(num2 - 1).SetText(rltPeaks[j].pkRT.ToString("F" + Class49.int_8));
			}
			if (pPara.bPeakArea)
			{
				num2++;
				array[0].GetRow(num6 + 1).GetCell(num2 - 1).SetText(rltPeaks[j].area.ToString("F" + Class49.int_8));
			}
			if (pPara.bPeakheight)
			{
				num2++;
				array[0].GetRow(num6 + 1).GetCell(num2 - 1).SetText(rltPeaks[j].height.ToString("F" + Class49.int_8));
			}
			if (pPara.bPeakHalfheight)
			{
				num2++;
				array[0].GetRow(num6 + 1).GetCell(num2 - 1).SetText(rltPeaks[j].WO5.ToString("F" + Class49.int_8));
			}
			if (pPara.bPeakAmont)
			{
				num2++;
				if (rltPeaks[j].amount < 0f)
				{
					rltPeaks[j].amount = 0f;
				}
				array[0].GetRow(num6 + 1).GetCell(num2 - 1).SetText(rltPeaks[j].amount.ToString("F" + Class49.int_8));
			}
			if (frmParam.bTVOC)
			{
				num2++;
				if (rltPeaks[j].amount < 0f)
				{
					rltPeaks[j].amount = 0f;
				}
				double num10 = rltPeaks[j].amount / frmParam.fInjectionVolume * (101.3f * (frmParam.fTemp + 273f) / (frmParam.fAtm * 273f));
				num9 += num10;
				array[0].GetRow(num6 + 1).GetCell(num2 - 1).SetText(num10.ToString("F" + Class49.int_8));
			}
			if (pPara.bPeakFx)
			{
				num2++;
				if (chromatogram_0[0].chromInfo.cclCalcu == CalcuStyle.ESTD)
				{
					if (rltPeaks[j].compound != null)
					{
						array[0].GetRow(num6 + 1).GetCell(num2 - 1).SetText(rltPeaks[j].compound.eFunc.GetEquationStr());
					}
					else
					{
						array[0].GetRow(num6 + 1).GetCell(num2 - 1).SetText("无");
					}
				}
				else if (chromatogram_0[0].chromInfo.cclCalcu == CalcuStyle.ISTD)
				{
					if (rltPeaks[j].compound != null)
					{
						array[0].GetRow(num6 + 1).GetCell(num2 - 1).SetText(rltPeaks[j].compound.iFunc.GetEquationStr());
					}
					else
					{
						array[0].GetRow(num6 + 1).GetCell(num2 - 1).SetText("无");
					}
				}
				else
				{
					array[0].GetRow(num6 + 1).GetCell(num2 - 1).SetText("无");
				}
			}
			if (pPara.bPeakLPara)
			{
				num2++;
				array[0].GetRow(num6 + 1).GetCell(num2 - 1).SetText(rltPeaks[j].Capacity.ToString("F" + Class49.int_8));
			}
			if (pPara.bPeakLV)
			{
				num2++;
				array[0].GetRow(num6 + 1).GetCell(num2 - 1).SetText(rltPeaks[j].Resolution_EP.ToString("F" + Class49.int_8));
			}
			if (pPara.bPeakOtherPara)
			{
				num2++;
				if (chromatogram_0[0].chromInfo.cclCalcu == CalcuStyle.ESTD)
				{
					if (rltPeaks[j].compound != null)
					{
						array[0].GetRow(num6 + 1).GetCell(num2 - 1).SetText(rltPeaks[j].compound.eFunc.corrFactor.ToString("0.00000"));
					}
					else
					{
						array[0].GetRow(num6 + 1).GetCell(num2 - 1).SetText("无");
					}
				}
				else if (chromatogram_0[0].chromInfo.cclCalcu == CalcuStyle.ISTD)
				{
					if (rltPeaks[j].compound != null)
					{
						array[0].GetRow(num6 + 1).GetCell(num2 - 1).SetText(rltPeaks[j].compound.iFunc.corrFactor.ToString("0.00000"));
					}
					else
					{
						array[0].GetRow(num6 + 1).GetCell(num2 - 1).SetText("无");
					}
				}
				else
				{
					array[0].GetRow(num6 + 1).GetCell(num2 - 1).SetText("无");
				}
			}
			if (pPara.bPeakPara)
			{
				num2++;
				if (rltPeaks[j].compound != null)
				{
					array[0].GetRow(num6 + 1).GetCell(num2 - 1).SetText(rltPeaks[j].compound.cmpdInfo.freeRespFactor.ToString("F" + Class49.int_8));
				}
				else
				{
					array[0].GetRow(num6 + 1).GetCell(num2 - 1).SetText("无");
				}
			}
			if (pPara.bPeaktailPara)
			{
				num2++;
				array[0].GetRow(num6 + 1).GetCell(num2 - 1).SetText(rltPeaks[j].SymmetryTailing.ToString("F" + Class49.int_8));
			}
			if (pPara.bPeakTBPara)
			{
				num2++;
				array[0].GetRow(num6 + 1).GetCell(num2 - 1).SetText(rltPeaks[j].Efficiency_EP.ToString("F" + Class49.int_8));
			}
			if (pPara.bPeakUTBPara)
			{
				num2++;
				array[0].GetRow(num6 + 1).GetCell(num2 - 1).SetText(rltPeaks[j].Eff_Column_EP.ToString("F" + Class49.int_8));
			}
			if (pPara.bPeakV)
			{
				num2++;
				array[0].GetRow(num6 + 1).GetCell(num2 - 1).SetText(rltPeaks[j].pkStyle.ToString());
			}
			if (pPara.bPeakheightPer)
			{
				num2++;
				array[0].GetRow(num6 + 1).GetCell(num2 - 1).SetText((rltPeaks[j].heightPer * 100f).ToString("F" + Class49.int_8));
			}
			if (pPara.bPeakAreaPer)
			{
				num2++;
				array[0].GetRow(num6 + 1).GetCell(num2 - 1).SetText((rltPeaks[j].areaPer * 100f).ToString("F" + Class49.int_8));
			}
			if (pPara.bPeakAmontPer)
			{
				num2++;
				if (rltPeaks[j].amountPer < 0f)
				{
					rltPeaks[j].amountPer = 0f;
				}
				array[0].GetRow(num6 + 1).GetCell(num2 - 1).SetText((rltPeaks[j].amountPer * 100f).ToString("F" + Class49.int_8));
			}
			num6++;
		}
		Compound compound = null;
		for (int k = 0; k < CurChrom.caliGnl.cmpds.Length; k++)
		{
			if (CurChrom.caliGnl.cmpds[k].cmpdInfo.name.Trim() == "甲苯")
			{
				compound = CurChrom.caliGnl.cmpds[k];
			}
		}
		if (CurChrom.caliGnl == null || CurChrom.caliGnl.cmpds.Length == 0)
		{
			MessageBox.Show("请正确加载组份表!");
			return;
		}
		if (compound == null)
		{
			compound = CurChrom.caliGnl.cmpds[0];
		}
		float[] array2 = compound.eFunc.Calcu_amountF(num7);
		num8 = ((array2.Length != 0) ? array2[0] : 0f);
		array[0].CreateRow();
		num2 = 0;
		if (pPara.bIndex)
		{
			num2++;
			array[0].GetRow(num6 + 1).GetCell(num2 - 1).SetText((num6 + 1).ToString());
		}
		if (pPara.bPeakName)
		{
			num2++;
			array[0].GetRow(num6 + 1).GetCell(num2 - 1).SetText("以甲苯计算的和峰");
		}
		if (pPara.bPeakMaxTime)
		{
			num2++;
			array[0].GetRow(num6 + 1).GetCell(num2 - 1).SetText("");
		}
		if (pPara.bPeakArea)
		{
			num2++;
			array[0].GetRow(num6 + 1).GetCell(num2 - 1).SetText(num7.ToString("F" + Class49.int_8));
		}
		if (pPara.bPeakheight)
		{
			num2++;
			array[0].GetRow(num6 + 1).GetCell(num2 - 1).SetText("");
		}
		if (pPara.bPeakHalfheight)
		{
			num2++;
			array[0].GetRow(num6 + 1).GetCell(num2 - 1).SetText("");
		}
		if (pPara.bPeakAmont)
		{
			num2++;
			array[0].GetRow(num6 + 1).GetCell(num2 - 1).SetText(num8.ToString("F" + Class49.int_8));
		}
		if (frmParam.bTVOC)
		{
			num2++;
			double num11 = num8 / frmParam.fInjectionVolume * (101.3f * (frmParam.fTemp + 273f) / (frmParam.fAtm * 273f));
			num9 += num11;
			array[0].GetRow(num6 + 1).GetCell(num2 - 1).SetText(num11.ToString("F" + Class49.int_8));
		}
		if (pPara.bPeakFx)
		{
			num2++;
			if (compound != null)
			{
				array[0].GetRow(num6 + 1).GetCell(num2 - 1).SetText(compound.eFunc.GetEquationStr());
			}
			else
			{
				array[0].GetRow(num6 + 1).GetCell(num2 - 1).SetText("无");
			}
		}
		if (pPara.bPeakLPara)
		{
			num2++;
		}
		if (pPara.bPeakLV)
		{
			num2++;
		}
		if (pPara.bPeakOtherPara)
		{
			num2++;
			if (compound != null)
			{
				array[0].GetRow(num6 + 1).GetCell(num2 - 1).SetText(compound.eFunc.corrFactor.ToString("0.00000"));
			}
			else
			{
				array[0].GetRow(num6 + 1).GetCell(num2 - 1).SetText("无");
			}
		}
		if (pPara.bPeakPara)
		{
			num2++;
			if (compound != null)
			{
				array[0].GetRow(num6 + 1).GetCell(num2 - 1).SetText(compound.cmpdInfo.freeRespFactor.ToString("F" + Class49.int_8));
			}
			else
			{
				array[0].GetRow(num6 + 1).GetCell(num2 - 1).SetText("无");
			}
		}
		if (pPara.bPeaktailPara)
		{
			num2++;
		}
		if (pPara.bPeakTBPara)
		{
			num2++;
		}
		if (pPara.bPeakUTBPara)
		{
			num2++;
		}
		if (pPara.bPeakV)
		{
			num2++;
		}
		if (pPara.bPeakheightPer)
		{
			num2++;
		}
		if (pPara.bPeakAreaPer)
		{
			num2++;
		}
		if (pPara.bPeakAmontPer)
		{
			num2++;
		}
		array[0].CreateRow();
		num2 = 0;
		if (pPara.bIndex)
		{
			num2++;
			array[0].GetRow(num6 + 2).GetCell(num2 - 1).SetText((num6 + 2).ToString());
		}
		if (pPara.bPeakName)
		{
			num2++;
			array[0].GetRow(num6 + 2).GetCell(num2 - 1).SetText("总计");
		}
		if (pPara.bPeakMaxTime)
		{
			num2++;
			array[0].GetRow(num6 + 2).GetCell(num2 - 1).SetText("");
		}
		if (pPara.bPeakArea)
		{
			num2++;
			array[0].GetRow(num6 + 2).GetCell(num2 - 1).SetText(CurChrom.whlArea.ToString("F" + Class49.int_8));
		}
		if (pPara.bPeakheight)
		{
			num2++;
			array[0].GetRow(num6 + 2).GetCell(num2 - 1).SetText(CurChrom.whlHeight.ToString("F" + Class49.int_8));
		}
		if (pPara.bPeakHalfheight)
		{
			num2++;
			array[0].GetRow(num6 + 2).GetCell(num2 - 1).SetText("");
		}
		if (pPara.bPeakAmont)
		{
			num2++;
			array[0].GetRow(num6 + 2).GetCell(num2 - 1).SetText((CurChrom.whlAmount + num8).ToString("F" + Class49.int_8));
		}
		if (frmParam.bTVOC)
		{
			num2++;
			array[0].GetRow(num6 + 2).GetCell(num2 - 1).SetText(num9.ToString("F" + Class49.int_8));
		}
		if (pPara.bPeakFx)
		{
			num2++;
			if (compound != null)
			{
				array[0].GetRow(num6 + 2).GetCell(num2 - 1).SetText("无");
			}
			else
			{
				array[0].GetRow(num6 + 2).GetCell(num2 - 1).SetText("无");
			}
		}
		if (pPara.bPeakLPara)
		{
			num2++;
		}
		if (pPara.bPeakLV)
		{
			num2++;
		}
		if (pPara.bPeakOtherPara)
		{
			num2++;
			if (compound != null)
			{
				array[0].GetRow(num6 + 2).GetCell(num2 - 1).SetText(compound.eFunc.corrFactor.ToString("0.00000"));
			}
			else
			{
				array[0].GetRow(num6 + 2).GetCell(num2 - 1).SetText("无");
			}
		}
		if (pPara.bPeakPara)
		{
			num2++;
			if (compound != null)
			{
				array[0].GetRow(num6 + 2).GetCell(num2 - 1).SetText(compound.cmpdInfo.freeRespFactor.ToString("F" + Class49.int_8));
			}
			else
			{
				array[0].GetRow(num6 + 2).GetCell(num2 - 1).SetText("无");
			}
		}
		if (pPara.bPeaktailPara)
		{
			num2++;
		}
		if (pPara.bPeakTBPara)
		{
			num2++;
		}
		if (pPara.bPeakUTBPara)
		{
			num2++;
		}
		if (pPara.bPeakV)
		{
			num2++;
		}
		if (pPara.bPeakheightPer)
		{
			num2++;
		}
		if (pPara.bPeakAreaPer)
		{
			num2++;
		}
		if (pPara.bPeakAmontPer)
		{
			num2++;
		}
		if (CurChrom.PPara.bPeakFx && CurChrom.caliGnl != null)
		{
			XWPFTable[] array3 = new XWPFTable[rltPeaks.Length];
			for (int l = 0; l < rltPeaks.Length; l++)
			{
				CaliGnl caliGnl = CurChrom.caliGnl;
				CmpdDisplay cmpdDisplay = new CmpdDisplay(WinStyle.CaliGnl, null);
				Size size = new Size(1540, 500);
				RectangleF rectangleF2 = new RectangleF(0f, 0f, size.Width, size.Height);
				Compound compound2 = rltPeaks[l].compound;
				if (compound2 != null && compound2.levels != null && compound2.levels.Length != 0)
				{
					Bitmap image = new Bitmap(base.Size.Width, base.Size.Height);
					string text3 = System.Windows.Forms.Application.StartupPath + "\\cmpd.Emf";
					Graphics graphics2 = Graphics.FromImage(image);
					Metafile metafile = new Metafile(text3, graphics2.GetHdc());
					Graphics graphics3 = Graphics.FromImage(metafile);
					string string_ = Class49.MesureUnit() + ".s";
					if (compound2.cmpdInfo.respStyle == RespStyle.Height)
					{
						string_ = Class49.MesureUnit();
					}
					cmpdDisplay.rcPage = rectangleF2;
					cmpdDisplay.dskRC = rectangleF2;
					cmpdDisplay.SetCompound2(compound2, bool_0: false, caliGnl.caliOption.cmpdUnit, ref string_);
					cmpdDisplay.Draw(graphics3, erase: true);
					graphics3.Dispose();
					metafile.Dispose();
					FileStream fileStream3 = new FileStream(text3, FileMode.Open, FileAccess.Read);
					XWPFParagraph xWPFParagraph5 = xWPFDocument.CreateParagraph();
					XWPFRun xWPFRun4 = xWPFParagraph5.CreateRun();
					xWPFRun4.AddPicture(fileStream3, 2, text3, num4, num5);
					fileStream3.Close();
					array3[l] = xWPFDocument.CreateTable(rltPeaks[l].compound.eFunc.funcPts.Length + 1, 5);
					array3[l].GetRow(0).GetCell(0).SetText("       响应      ");
					array3[l].GetRow(0).GetCell(1).SetText("       浓度      ");
					array3[l].GetRow(0).GetCell(2).SetText("       因子      ");
					array3[l].GetRow(0).GetCell(3).SetText("      相关系数       ");
					array3[l].GetRow(0).GetCell(4).SetText("           方程          ");
					for (int m = 0; m < rltPeaks[l].compound.eFunc.funcPts.Length; m++)
					{
						array3[l].GetRow(m + 1).GetCell(0).SetText(rltPeaks[l].compound.eFunc.funcPts[m].responseF.ToString("F" + Class49.int_8));
						array3[l].GetRow(m + 1).GetCell(1).SetText(rltPeaks[l].compound.eFunc.funcPts[m].amountF.ToString("F" + Class49.int_8));
					}
					array3[l].GetRow(1).GetCell(2).SetText(rltPeaks[l].compound.cmpdInfo.freeRespFactor.ToString("F" + Class49.int_8));
					if (chromatogram_0[0].chromInfo.cclCalcu == CalcuStyle.ESTD)
					{
						array3[l].GetRow(1).GetCell(3).SetText(rltPeaks[l].compound.eFunc.corrFactor.ToString("0.00000"));
						array3[l].GetRow(1).GetCell(4).SetText(rltPeaks[l].compound.eFunc.GetEquationStr());
					}
					else if (chromatogram_0[0].chromInfo.cclCalcu == CalcuStyle.ISTD)
					{
						array3[l].GetRow(1).GetCell(3).SetText(rltPeaks[l].compound.iFunc.corrFactor.ToString("0.00000"));
						array3[l].GetRow(1).GetCell(4).SetText(rltPeaks[l].compound.iFunc.GetEquationStr());
					}
					array3[l].SetColumnWidth(0, 76800uL);
					array3[l].SetColumnWidth(1, 76800uL);
					array3[l].SetColumnWidth(2, 76800uL);
					array3[l].SetColumnWidth(3, 153600uL);
					array3[l].SetColumnWidth(4, 1536000uL);
					xWPFRun4.AddCarriageReturn();
				}
			}
		}
		XWPFParagraph xWPFParagraph6 = xWPFDocument.CreateParagraph();
		XWPFRun xWPFRun5 = xWPFParagraph6.CreateRun();
		xWPFRun5.SetText(pPara.printTitleBotom);
		xWPFDocument.Write(fileStream);
		Process.Start(text);
	}

	public void PrintToWord(Chromatogram chromatogram, string strFileName)
	{
		int num = 0;
		int num2 = 0;
		PrintPara pPara = chromatogram_0[0].PPara;
		int setChromNo = chromatogram_0.Length;
		Bitmap bitmap = new Bitmap(864, 340);
		Graphics graphics = Graphics.FromImage(bitmap);
		if (CurChrom.PPara.bPicBound)
		{
			graphics.SmoothingMode = SmoothingMode.HighQuality;
			graphics.CompositingQuality = CompositingQuality.HighQuality;
			graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
			graphics.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
			graphics.DrawRectangle(new Pen(Color.Black, 2f), 0, 0, bitmap.Width - 15, bitmap.Height - 15);
		}
		RectangleF rectangleF = new RectangleF(0f, 0f, 849f, 325f);
		ChromDisplay chromDisplay = new ChromDisplay(WinStyle.Chromatogram, null);
		chromDisplay.LinkDisChroms(chromatogram_0, ref setChromNo);
		chromDisplay.showMouseLgValue = false;
		chromDisplay.showProgTemp = false;
		chromDisplay.ShowBgChrom = true;
		chromDisplay.setShowGrid = true;
		chromDisplay.showPeakArea = false;
		chromDisplay.rcPage = rectangleF;
		chromDisplay.dskRC = rectangleF;
		chromDisplay.FrmPen.Width = 1f;
		chromDisplay.DisPen.Width = 1f;
		System.Drawing.Font peakFont = chromDisplay.options.peakFont;
		chromDisplay.options.peakFont = new System.Drawing.Font(peakFont.FontFamily, peakFont.Size * 1f, peakFont.Style);
		chromDisplay.Draw(graphics, erase: true);
		bitmap.Save(System.Windows.Forms.Application.StartupPath + "\\a1.Emf");
		string text = pPara.Title + DateTime.Now.ToString("yyyyMMdd-HHmmss") + ".docx";
		FileStream fileStream = File.OpenWrite(text);
		fileStream.Position = 0L;
		XWPFDocument xWPFDocument = new XWPFDocument();
		xWPFDocument.Document.body.sectPr = new CT_SectPr();
		CT_SectPr sectPr = xWPFDocument.Document.body.sectPr;
		XWPFParagraph xWPFParagraph = xWPFDocument.CreateParagraph();
		CT_Ftr cT_Ftr = new CT_Ftr();
		cT_Ftr.Items = new ArrayList();
		CT_SdtBlock cT_SdtBlock = new CT_SdtBlock();
		CT_SdtPr cT_SdtPr = cT_SdtBlock.AddNewSdtPr();
		CT_SdtDocPart cT_SdtDocPart = cT_SdtPr.AddNewDocPartObj();
		cT_SdtDocPart.AddNewDocPartGallery().val = "PageNumbers (Bottom of Page)";
		cT_SdtDocPart.docPartUnique = new CT_OnOff();
		CT_SdtContentBlock cT_SdtContentBlock = cT_SdtBlock.AddNewSdtContent();
		CT_P cT_P = cT_SdtContentBlock.AddNewP();
		CT_PPr cT_PPr = cT_P.AddNewPPr();
		cT_PPr.AddNewJc().val = ST_Jc.center;
		cT_P.Items = new ArrayList();
		CT_SimpleField cT_SimpleField = new CT_SimpleField();
		cT_SimpleField.instr = " PAGE   \\*MERGEFORMAT ";
		cT_P.Items.Add(cT_SimpleField);
		cT_Ftr.Items.Add(cT_SdtBlock);
		XWPFRelation fOOTER = XWPFRelation.FOOTER;
		XWPFFooter xWPFFooter = (XWPFFooter)xWPFDocument.CreateRelationship(fOOTER, XWPFFactory.GetInstance(), xWPFDocument.FooterList.Count + 1);
		xWPFFooter.SetHeaderFooter(cT_Ftr);
		CT_HdrFtrRef cT_HdrFtrRef = sectPr.AddNewFooterReference();
		cT_HdrFtrRef.type = ST_HdrFtr.@default;
		cT_HdrFtrRef.id = xWPFFooter.GetPackageRelationship().Id;
		XWPFParagraph xWPFParagraph2 = xWPFDocument.CreateParagraph();
		xWPFParagraph2.Alignment = ParagraphAlignment.CENTER;
		XWPFRun xWPFRun = xWPFParagraph2.CreateRun();
		xWPFRun.FontSize = 16;
		xWPFRun.IsBold = true;
		xWPFRun.AppendText(pPara.Title);
		XWPFParagraph xWPFParagraph3 = xWPFDocument.CreateParagraph();
		xWPFParagraph3.Alignment = ParagraphAlignment.LEFT;
		XWPFRun xWPFRun2 = xWPFParagraph3.CreateRun();
		xWPFRun2.AddCarriageReturn();
		xWPFRun2.SetText(pPara.PrintTitleTop);
		xWPFRun2.AddCarriageReturn();
		if (CurChrom.PPara.bPTime)
		{
			xWPFRun2.AppendText("打印时间:" + DateTime.Now.ToString());
			xWPFRun2.AddCarriageReturn();
		}
		if (CurChrom.PPara.bJtime)
		{
			xWPFRun2.AppendText("进样时间:" + chromatogram_1.injAnalysis.dtAcquire.ToString());
			xWPFRun2.AddCarriageReturn();
		}
		if (CurChrom.PPara.bfname)
		{
			xWPFRun2.AppendText("打开的谱图文件:" + m_strChormFileName);
			xWPFRun2.AddCarriageReturn();
		}
		int num3 = 5238750;
		int num4 = 2857500;
		string text2 = System.Windows.Forms.Application.StartupPath + "\\a1.Emf";
		FileStream fileStream2 = new FileStream(text2, FileMode.Open, FileAccess.Read);
		xWPFRun2.AddPicture(fileStream2, 2, text2, num3, num4);
		fileStream2.Close();
		xWPFRun2.AddCarriageReturn();
		Peak[] rltPeaks = chromatogram_0[0].RltPeaks;
		int num5 = rltPeaks.Length;
		if (num5 == 0)
		{
			num5 = 1;
		}
		XWPFTable[] array = new XWPFTable[num5];
		num = 0;
		if (pPara.bIndex)
		{
			num++;
		}
		if (pPara.bPeakName)
		{
			num++;
		}
		if (pPara.bPeakMaxTime)
		{
			num++;
		}
		if (pPara.bPeakArea)
		{
			num++;
		}
		if (pPara.bPeakheight)
		{
			num++;
		}
		if (pPara.bPeakHalfheight)
		{
			num++;
		}
		if (pPara.bPeakAmont)
		{
			num++;
		}
		if (pPara.bPeakFx)
		{
			num++;
		}
		if (pPara.bPeakLPara)
		{
			num++;
		}
		if (pPara.bPeakLV)
		{
			num++;
		}
		if (pPara.bPeakOtherPara)
		{
			num++;
		}
		if (pPara.bPeakPara)
		{
			num++;
		}
		if (pPara.bPeaktailPara)
		{
			num++;
		}
		if (pPara.bPeakTBPara)
		{
			num++;
		}
		if (pPara.bPeakUTBPara)
		{
			num++;
		}
		if (pPara.bPeakV)
		{
			num++;
		}
		if (pPara.bPeakheightPer)
		{
			num++;
		}
		if (pPara.bPeakAreaPer)
		{
			num++;
		}
		if (pPara.bPeakAmontPer)
		{
			num++;
		}
		array[0] = xWPFDocument.CreateTable(1, num);
		num2 = 0;
		if (pPara.bIndex)
		{
			num2++;
			array[0].GetRow(0).GetCell(num2 - 1).SetText("序号");
		}
		if (pPara.bPeakName)
		{
			num2++;
			array[0].GetRow(0).GetCell(num2 - 1).SetText("名称");
		}
		if (pPara.bPeakMaxTime)
		{
			num2++;
			array[0].GetRow(0).GetCell(num2 - 1).SetText("保留时间(min)");
		}
		if (pPara.bPeakArea)
		{
			num2++;
			array[0].GetRow(0).GetCell(num2 - 1).SetText("峰面积");
		}
		if (pPara.bPeakheight)
		{
			num2++;
			array[0].GetRow(0).GetCell(num2 - 1).SetText("峰高");
		}
		if (pPara.bPeakHalfheight)
		{
			num2++;
			array[0].GetRow(0).GetCell(num2 - 1).SetText("半峰宽");
		}
		if (pPara.bPeakAmont)
		{
			num2++;
			array[0].GetRow(0).GetCell(num2 - 1).SetText("浓度(" + chromatogram_0[0].AmountUnit + ")");
		}
		if (pPara.bPeakFx)
		{
			num2++;
			array[0].GetRow(0).GetCell(num2 - 1).SetText("工作曲线方程");
		}
		if (pPara.bPeakLPara)
		{
			num2++;
			array[0].GetRow(0).GetCell(num2 - 1).SetText("容量因子");
		}
		if (pPara.bPeakLV)
		{
			num2++;
			array[0].GetRow(0).GetCell(num2 - 1).SetText("峰分离度");
		}
		if (pPara.bPeakOtherPara)
		{
			num2++;
			array[0].GetRow(0).GetCell(num2 - 1).SetText("相关系数");
		}
		if (pPara.bPeakPara)
		{
			num2++;
			array[0].GetRow(0).GetCell(num2 - 1).SetText("校正因子");
		}
		if (pPara.bPeaktailPara)
		{
			num2++;
			array[0].GetRow(0).GetCell(num2 - 1).SetText("拖尾因子");
		}
		if (pPara.bPeakTBPara)
		{
			num2++;
			array[0].GetRow(0).GetCell(num2 - 1).SetText("理论塔板数");
		}
		if (pPara.bPeakUTBPara)
		{
			num2++;
			array[0].GetRow(0).GetCell(num2 - 1).SetText("有效塔板数");
		}
		if (pPara.bPeakV)
		{
			num2++;
			array[0].GetRow(0).GetCell(num2 - 1).SetText("峰标志");
		}
		if (pPara.bPeakheightPer)
		{
			num2++;
			array[0].GetRow(0).GetCell(num2 - 1).SetText("峰高百分比");
		}
		if (pPara.bPeakAreaPer)
		{
			num2++;
			array[0].GetRow(0).GetCell(num2 - 1).SetText("面积百分比");
		}
		if (pPara.bPeakAmontPer)
		{
			num2++;
			array[0].GetRow(0).GetCell(num2 - 1).SetText("浓度百分比");
		}
		for (int i = 0; i < num; i++)
		{
			CT_Tc cTTc = array[0].GetRow(0).GetCell(i).GetCTTc();
			CT_TcPr cT_TcPr = cTTc.AddNewTcPr();
			cT_TcPr.tcW = new CT_TblWidth();
			cT_TcPr.tcW.w = "6000";
			cT_TcPr.tcW.type = ST_TblWidth.dxa;
		}
		XWPFParagraph xWPFParagraph4 = xWPFDocument.CreateParagraph();
		XWPFRun xWPFRun3 = xWPFParagraph4.CreateRun();
		xWPFRun3.AddCarriageReturn();
		for (int j = 0; j < rltPeaks.Length; j++)
		{
			array[0].CreateRow();
			num2 = 0;
			if (pPara.bIndex)
			{
				num2++;
				array[0].GetRow(j + 1).GetCell(num2 - 1).SetText((j + 1).ToString());
			}
			if (pPara.bPeakName)
			{
				num2++;
				array[0].GetRow(j + 1).GetCell(num2 - 1).SetText(rltPeaks[j].name);
			}
			if (pPara.bPeakMaxTime)
			{
				num2++;
				array[0].GetRow(j + 1).GetCell(num2 - 1).SetText(rltPeaks[j].pkRT.ToString("F" + Class49.int_8));
			}
			if (pPara.bPeakArea)
			{
				num2++;
				array[0].GetRow(j + 1).GetCell(num2 - 1).SetText(rltPeaks[j].area.ToString("F" + Class49.int_8));
			}
			if (pPara.bPeakheight)
			{
				num2++;
				array[0].GetRow(j + 1).GetCell(num2 - 1).SetText(rltPeaks[j].height.ToString("F" + Class49.int_8));
			}
			if (pPara.bPeakHalfheight)
			{
				num2++;
				array[0].GetRow(j + 1).GetCell(num2 - 1).SetText(rltPeaks[j].WO5.ToString("F" + Class49.int_8));
			}
			if (pPara.bPeakAmont)
			{
				num2++;
				if (rltPeaks[j].amount < 0f)
				{
					rltPeaks[j].amount = 0f;
				}
				array[0].GetRow(j + 1).GetCell(num2 - 1).SetText(rltPeaks[j].amount.ToString("F" + Class49.int_8));
			}
			if (pPara.bPeakFx)
			{
				num2++;
				if (chromatogram_0[0].chromInfo.cclCalcu == CalcuStyle.ESTD)
				{
					if (rltPeaks[j].compound != null)
					{
						array[0].GetRow(j + 1).GetCell(num2 - 1).SetText(rltPeaks[j].compound.eFunc.GetEquationStr());
					}
					else
					{
						array[0].GetRow(j + 1).GetCell(num2 - 1).SetText("无");
					}
				}
				else if (chromatogram_0[0].chromInfo.cclCalcu == CalcuStyle.ISTD)
				{
					if (rltPeaks[j].compound != null)
					{
						array[0].GetRow(j + 1).GetCell(num2 - 1).SetText(rltPeaks[j].compound.iFunc.GetEquationStr());
					}
					else
					{
						array[0].GetRow(j + 1).GetCell(num2 - 1).SetText("无");
					}
				}
				else
				{
					array[0].GetRow(j + 1).GetCell(num2 - 1).SetText("无");
				}
			}
			if (pPara.bPeakLPara)
			{
				num2++;
				array[0].GetRow(j + 1).GetCell(num2 - 1).SetText(rltPeaks[j].Capacity.ToString("F" + Class49.int_8));
			}
			if (pPara.bPeakLV)
			{
				num2++;
				array[0].GetRow(j + 1).GetCell(num2 - 1).SetText(rltPeaks[j].Resolution_EP.ToString("F" + Class49.int_8));
			}
			if (pPara.bPeakOtherPara)
			{
				num2++;
				if (chromatogram_0[0].chromInfo.cclCalcu == CalcuStyle.ESTD)
				{
					if (rltPeaks[j].compound != null)
					{
						array[0].GetRow(j + 1).GetCell(num2 - 1).SetText(rltPeaks[j].compound.eFunc.corrFactor.ToString("0.00000"));
					}
					else
					{
						array[0].GetRow(j + 1).GetCell(num2 - 1).SetText("无");
					}
				}
				else if (chromatogram_0[0].chromInfo.cclCalcu == CalcuStyle.ISTD)
				{
					if (rltPeaks[j].compound != null)
					{
						array[0].GetRow(j + 1).GetCell(num2 - 1).SetText(rltPeaks[j].compound.iFunc.corrFactor.ToString("0.00000"));
					}
					else
					{
						array[0].GetRow(j + 1).GetCell(num2 - 1).SetText("无");
					}
				}
				else
				{
					array[0].GetRow(j + 1).GetCell(num2 - 1).SetText("无");
				}
			}
			if (pPara.bPeakPara)
			{
				num2++;
				if (rltPeaks[j].compound != null)
				{
					array[0].GetRow(j + 1).GetCell(num2 - 1).SetText(rltPeaks[j].compound.cmpdInfo.freeRespFactor.ToString("F" + Class49.int_8));
				}
				else
				{
					array[0].GetRow(j + 1).GetCell(num2 - 1).SetText("无");
				}
			}
			if (pPara.bPeaktailPara)
			{
				num2++;
				array[0].GetRow(j + 1).GetCell(num2 - 1).SetText(rltPeaks[j].SymmetryTailing.ToString("F" + Class49.int_8));
			}
			if (pPara.bPeakTBPara)
			{
				num2++;
				array[0].GetRow(j + 1).GetCell(num2 - 1).SetText(rltPeaks[j].Efficiency_EP.ToString("F" + Class49.int_8));
			}
			if (pPara.bPeakUTBPara)
			{
				num2++;
				array[0].GetRow(j + 1).GetCell(num2 - 1).SetText(rltPeaks[j].Eff_Column_EP.ToString("F" + Class49.int_8));
			}
			if (pPara.bPeakV)
			{
				num2++;
				array[0].GetRow(j + 1).GetCell(num2 - 1).SetText(rltPeaks[j].pkStyle.ToString());
			}
			if (pPara.bPeakheightPer)
			{
				num2++;
				array[0].GetRow(j + 1).GetCell(num2 - 1).SetText((rltPeaks[j].heightPer * 100f).ToString("F" + Class49.int_8));
			}
			if (pPara.bPeakAreaPer)
			{
				num2++;
				array[0].GetRow(j + 1).GetCell(num2 - 1).SetText((rltPeaks[j].areaPer * 100f).ToString("F" + Class49.int_8));
			}
			if (pPara.bPeakAmontPer)
			{
				num2++;
				if (rltPeaks[j].amountPer < 0f)
				{
					rltPeaks[j].amountPer = 0f;
				}
				array[0].GetRow(j + 1).GetCell(num2 - 1).SetText((rltPeaks[j].amountPer * 100f).ToString("F" + Class49.int_8));
			}
		}
		if (CurChrom.PPara.bPeakFx && CurChrom.caliGnl != null)
		{
			XWPFTable[] array2 = new XWPFTable[rltPeaks.Length];
			for (int k = 0; k < rltPeaks.Length; k++)
			{
				CaliGnl caliGnl = CurChrom.caliGnl;
				CmpdDisplay cmpdDisplay = new CmpdDisplay(WinStyle.CaliGnl, null);
				Size size = new Size(1184, 384);
				RectangleF rectangleF2 = new RectangleF(0f, 0f, size.Width, size.Height);
				Compound compound = rltPeaks[k].compound;
				if (compound == null || compound.levels == null || compound.levels.Length == 0)
				{
					continue;
				}
				Bitmap image = new Bitmap(base.Size.Width, base.Size.Height);
				string text3 = System.Windows.Forms.Application.StartupPath + "\\cmpd.Emf";
				Graphics graphics2 = Graphics.FromImage(image);
				Metafile metafile = new Metafile(text3, graphics2.GetHdc());
				Graphics graphics3 = Graphics.FromImage(metafile);
				System.Drawing.Font font = FrameDis.font;
				FrameDis.font = new System.Drawing.Font(font.FontFamily, font.Size * 2.2f, font.Style);
				string string_ = Class49.MesureUnit() + ".s";
				if (compound.cmpdInfo.respStyle == RespStyle.Height)
				{
					string_ = Class49.MesureUnit();
				}
				cmpdDisplay.rcPage = rectangleF2;
				cmpdDisplay.dskRC = rectangleF2;
				System.Drawing.Font unitFont = cmpdDisplay.options.unitFont;
				cmpdDisplay.options.titleFont = new System.Drawing.Font(unitFont.FontFamily, unitFont.Size * 2.2f, unitFont.Style);
				cmpdDisplay.options.unitFont = new System.Drawing.Font(unitFont.FontFamily, unitFont.Size * 2.2f, unitFont.Style);
				cmpdDisplay.SetCompound2(compound, bool_0: false, caliGnl.caliOption.cmpdUnit, ref string_);
				cmpdDisplay.Draw(graphics3, erase: true);
				graphics3.Dispose();
				metafile.Dispose();
				FrameDis.font = font;
				FileStream fileStream3 = new FileStream(text3, FileMode.Open, FileAccess.Read);
				XWPFParagraph xWPFParagraph5 = xWPFDocument.CreateParagraph();
				XWPFRun xWPFRun4 = xWPFParagraph5.CreateRun();
				xWPFRun4.AddPicture(fileStream3, 2, text3, 5238750, 1905000);
				fileStream3.Close();
				array2[k] = xWPFDocument.CreateTable(rltPeaks[k].compound.eFunc.funcPts.Length + 1, 5);
				array2[k].GetRow(0).GetCell(0).SetText("响应");
				array2[k].GetRow(0).GetCell(1).SetText("浓度(" + chromatogram_0[0].AmountUnit + ")");
				array2[k].GetRow(0).GetCell(2).SetText("因子");
				array2[k].GetRow(0).GetCell(3).SetText("相关系数");
				array2[k].GetRow(0).GetCell(4).SetText("方程");
				for (int l = 0; l < rltPeaks[k].compound.levels.Length; l++)
				{
					if (rltPeaks[k].compound.levels[l].used)
					{
						array2[k].GetRow(l + 1).GetCell(0).SetText(rltPeaks[k].compound.levels[l].response.ToString("F" + Class49.int_8));
						array2[k].GetRow(l + 1).GetCell(1).SetText(rltPeaks[k].compound.levels[l].amount.ToString("F" + Class49.int_8));
						array2[k].GetRow(l + 1).GetCell(2).SetText(rltPeaks[k].compound.levels[l].respFactor.ToString("F" + Class49.int_8));
						if (chromatogram_0[0].chromInfo.cclCalcu == CalcuStyle.ESTD)
						{
							double num6 = Class49.takeDouble(rltPeaks[k].compound.eFunc.corrFactor, Class49.int_8);
							array2[k].GetRow(l + 1).GetCell(3).SetText(rltPeaks[k].compound.eFunc.corrFactor.ToString("F" + Class49.int_8));
							array2[k].GetRow(l + 1).GetCell(4).SetText(rltPeaks[k].compound.eFunc.GetEquationStr());
						}
						else
						{
							double num7 = Class49.takeDouble(rltPeaks[k].compound.iFunc.corrFactor, Class49.int_8);
							array2[k].GetRow(l + 1).GetCell(3).SetText(rltPeaks[k].compound.iFunc.corrFactor.ToString("F" + Class49.int_8));
							array2[k].GetRow(l + 1).GetCell(4).SetText(rltPeaks[k].compound.iFunc.GetEquationStr());
						}
					}
				}
				for (int m = 0; m < 4; m++)
				{
					CT_Tc cTTc2 = array2[k].GetRow(0).GetCell(m).GetCTTc();
					CT_TcPr cT_TcPr2 = cTTc2.AddNewTcPr();
					cT_TcPr2.tcW = new CT_TblWidth();
					cT_TcPr2.tcW.w = "1500";
					cT_TcPr2.tcW.type = ST_TblWidth.dxa;
				}
				xWPFRun4.AddCarriageReturn();
			}
		}
		XWPFParagraph xWPFParagraph6 = xWPFDocument.CreateParagraph();
		XWPFRun xWPFRun5 = xWPFParagraph6.CreateRun();
		xWPFRun5.SetText(pPara.printTitleBotom);
		xWPFDocument.Write(fileStream);
		Process.Start(text);
	}

	public void PrintToWordAndPrint(Chromatogram chromatogram, string strFileName)
	{
		int num = 0;
		int num2 = 0;
		Chromatogram[] chroms = new Chromatogram[1] { chromatogram };
		PrintPara pPara = chromatogram.PPara;
		int setChromNo = 1;
		Bitmap bitmap = new Bitmap(864, 340);
		Graphics graphics = Graphics.FromImage(bitmap);
		if (CurChrom.PPara.bPicBound)
		{
			graphics.SmoothingMode = SmoothingMode.HighQuality;
			graphics.CompositingQuality = CompositingQuality.HighQuality;
			graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
			graphics.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
			graphics.DrawRectangle(new Pen(Color.Black, 2f), 0, 0, bitmap.Width - 15, bitmap.Height - 15);
		}
		RectangleF rectangleF = new RectangleF(0f, 0f, 849f, 325f);
		ChromDisplay chromDisplay = new ChromDisplay(WinStyle.Chromatogram, null);
		chromDisplay.LinkDisChroms(chroms, ref setChromNo);
		chromDisplay.showMouseLgValue = false;
		chromDisplay.showProgTemp = false;
		chromDisplay.ShowBgChrom = true;
		chromDisplay.setShowGrid = true;
		chromDisplay.showPeakArea = false;
		chromDisplay.rcPage = rectangleF;
		chromDisplay.dskRC = rectangleF;
		chromDisplay.FrmPen.Width = 1f;
		chromDisplay.DisPen.Width = 1f;
		System.Drawing.Font peakFont = chromDisplay.options.peakFont;
		chromDisplay.options.peakFont = new System.Drawing.Font(peakFont.FontFamily, peakFont.Size * 1f, peakFont.Style);
		chromDisplay.Draw(graphics, erase: true);
		bitmap.Save(System.Windows.Forms.Application.StartupPath + "\\a1.Emf");
		string text = pPara.Title + DateTime.Now.ToString("yyyyMMdd-HHmmss") + ".docx";
		FileStream fileStream = File.OpenWrite(text);
		fileStream.Position = 0L;
		XWPFDocument xWPFDocument = new XWPFDocument();
		xWPFDocument.Document.body.sectPr = new CT_SectPr();
		CT_SectPr sectPr = xWPFDocument.Document.body.sectPr;
		XWPFParagraph xWPFParagraph = xWPFDocument.CreateParagraph();
		CT_Ftr cT_Ftr = new CT_Ftr();
		cT_Ftr.Items = new ArrayList();
		CT_SdtBlock cT_SdtBlock = new CT_SdtBlock();
		CT_SdtPr cT_SdtPr = cT_SdtBlock.AddNewSdtPr();
		CT_SdtDocPart cT_SdtDocPart = cT_SdtPr.AddNewDocPartObj();
		cT_SdtDocPart.AddNewDocPartGallery().val = "PageNumbers (Bottom of Page)";
		cT_SdtDocPart.docPartUnique = new CT_OnOff();
		CT_SdtContentBlock cT_SdtContentBlock = cT_SdtBlock.AddNewSdtContent();
		CT_P cT_P = cT_SdtContentBlock.AddNewP();
		CT_PPr cT_PPr = cT_P.AddNewPPr();
		cT_PPr.AddNewJc().val = ST_Jc.center;
		cT_P.Items = new ArrayList();
		CT_SimpleField cT_SimpleField = new CT_SimpleField();
		cT_SimpleField.instr = " PAGE   \\*MERGEFORMAT ";
		cT_P.Items.Add(cT_SimpleField);
		cT_Ftr.Items.Add(cT_SdtBlock);
		XWPFRelation fOOTER = XWPFRelation.FOOTER;
		XWPFFooter xWPFFooter = (XWPFFooter)xWPFDocument.CreateRelationship(fOOTER, XWPFFactory.GetInstance(), xWPFDocument.FooterList.Count + 1);
		xWPFFooter.SetHeaderFooter(cT_Ftr);
		CT_HdrFtrRef cT_HdrFtrRef = sectPr.AddNewFooterReference();
		cT_HdrFtrRef.type = ST_HdrFtr.@default;
		cT_HdrFtrRef.id = xWPFFooter.GetPackageRelationship().Id;
		XWPFParagraph xWPFParagraph2 = xWPFDocument.CreateParagraph();
		xWPFParagraph2.Alignment = ParagraphAlignment.CENTER;
		XWPFRun xWPFRun = xWPFParagraph2.CreateRun();
		xWPFRun.FontSize = 16;
		xWPFRun.IsBold = true;
		xWPFRun.AppendText(pPara.Title);
		XWPFParagraph xWPFParagraph3 = xWPFDocument.CreateParagraph();
		xWPFParagraph3.Alignment = ParagraphAlignment.LEFT;
		XWPFRun xWPFRun2 = xWPFParagraph3.CreateRun();
		xWPFRun2.AddCarriageReturn();
		xWPFRun2.SetText(pPara.PrintTitleTop);
		xWPFRun2.AddCarriageReturn();
		if (chromatogram.PPara.bPTime)
		{
			xWPFRun2.AppendText("打印时间:" + DateTime.Now.ToString());
			xWPFRun2.AddCarriageReturn();
		}
		if (chromatogram.PPara.bJtime)
		{
			xWPFRun2.AppendText("进样时间:" + chromatogram_1.injAnalysis.dtAcquire.ToString());
			xWPFRun2.AddCarriageReturn();
		}
		if (chromatogram.PPara.bfname)
		{
			xWPFRun2.AppendText("打开的谱图文件:" + m_strChormFileName);
			xWPFRun2.AddCarriageReturn();
		}
		int num3 = 5238750;
		int num4 = 2857500;
		string text2 = System.Windows.Forms.Application.StartupPath + "\\a1.Emf";
		FileStream fileStream2 = new FileStream(text2, FileMode.Open, FileAccess.Read);
		xWPFRun2.AddPicture(fileStream2, 2, text2, num3, num4);
		fileStream2.Close();
		xWPFRun2.AddCarriageReturn();
		Peak[] rltPeaks = chromatogram.RltPeaks;
		int num5 = rltPeaks.Length;
		if (num5 == 0)
		{
			num5 = 1;
		}
		XWPFTable[] array = new XWPFTable[num5];
		num = 0;
		if (pPara.bIndex)
		{
			num++;
		}
		if (pPara.bPeakName)
		{
			num++;
		}
		if (pPara.bPeakMaxTime)
		{
			num++;
		}
		if (pPara.bPeakArea)
		{
			num++;
		}
		if (pPara.bPeakheight)
		{
			num++;
		}
		if (pPara.bPeakHalfheight)
		{
			num++;
		}
		if (pPara.bPeakAmont)
		{
			num++;
		}
		if (pPara.bPeakFx)
		{
			num++;
		}
		if (pPara.bPeakLPara)
		{
			num++;
		}
		if (pPara.bPeakLV)
		{
			num++;
		}
		if (pPara.bPeakOtherPara)
		{
			num++;
		}
		if (pPara.bPeakPara)
		{
			num++;
		}
		if (pPara.bPeaktailPara)
		{
			num++;
		}
		if (pPara.bPeakTBPara)
		{
			num++;
		}
		if (pPara.bPeakUTBPara)
		{
			num++;
		}
		if (pPara.bPeakV)
		{
			num++;
		}
		if (pPara.bPeakheightPer)
		{
			num++;
		}
		if (pPara.bPeakAreaPer)
		{
			num++;
		}
		if (pPara.bPeakAmontPer)
		{
			num++;
		}
		array[0] = xWPFDocument.CreateTable(1, num);
		num2 = 0;
		if (pPara.bIndex)
		{
			num2++;
			array[0].GetRow(0).GetCell(num2 - 1).SetText("序号");
		}
		if (pPara.bPeakName)
		{
			num2++;
			array[0].GetRow(0).GetCell(num2 - 1).SetText("名称");
		}
		if (pPara.bPeakMaxTime)
		{
			num2++;
			array[0].GetRow(0).GetCell(num2 - 1).SetText("保留时间(min)");
		}
		if (pPara.bPeakArea)
		{
			num2++;
			array[0].GetRow(0).GetCell(num2 - 1).SetText("峰面积");
		}
		if (pPara.bPeakheight)
		{
			num2++;
			array[0].GetRow(0).GetCell(num2 - 1).SetText("峰高");
		}
		if (pPara.bPeakHalfheight)
		{
			num2++;
			array[0].GetRow(0).GetCell(num2 - 1).SetText("半峰宽");
		}
		if (pPara.bPeakAmont)
		{
			num2++;
			array[0].GetRow(0).GetCell(num2 - 1).SetText("浓度(" + chromatogram_0[0].AmountUnit + ")");
		}
		if (pPara.bPeakFx)
		{
			num2++;
			array[0].GetRow(0).GetCell(num2 - 1).SetText("工作曲线方程");
		}
		if (pPara.bPeakLPara)
		{
			num2++;
			array[0].GetRow(0).GetCell(num2 - 1).SetText("容量因子");
		}
		if (pPara.bPeakLV)
		{
			num2++;
			array[0].GetRow(0).GetCell(num2 - 1).SetText("峰分离度");
		}
		if (pPara.bPeakOtherPara)
		{
			num2++;
			array[0].GetRow(0).GetCell(num2 - 1).SetText("相关系数");
		}
		if (pPara.bPeakPara)
		{
			num2++;
			array[0].GetRow(0).GetCell(num2 - 1).SetText("校正因子");
		}
		if (pPara.bPeaktailPara)
		{
			num2++;
			array[0].GetRow(0).GetCell(num2 - 1).SetText("拖尾因子");
		}
		if (pPara.bPeakTBPara)
		{
			num2++;
			array[0].GetRow(0).GetCell(num2 - 1).SetText("理论塔板数");
		}
		if (pPara.bPeakUTBPara)
		{
			num2++;
			array[0].GetRow(0).GetCell(num2 - 1).SetText("有效塔板数");
		}
		if (pPara.bPeakV)
		{
			num2++;
			array[0].GetRow(0).GetCell(num2 - 1).SetText("峰标志");
		}
		if (pPara.bPeakheightPer)
		{
			num2++;
			array[0].GetRow(0).GetCell(num2 - 1).SetText("峰高百分比");
		}
		if (pPara.bPeakAreaPer)
		{
			num2++;
			array[0].GetRow(0).GetCell(num2 - 1).SetText("面积百分比");
		}
		if (pPara.bPeakAmontPer)
		{
			num2++;
			array[0].GetRow(0).GetCell(num2 - 1).SetText("浓度百分比");
		}
		for (int i = 0; i < num; i++)
		{
			CT_Tc cTTc = array[0].GetRow(0).GetCell(i).GetCTTc();
			CT_TcPr cT_TcPr = cTTc.AddNewTcPr();
			cT_TcPr.tcW = new CT_TblWidth();
			cT_TcPr.tcW.w = "6000";
			cT_TcPr.tcW.type = ST_TblWidth.dxa;
		}
		XWPFParagraph xWPFParagraph4 = xWPFDocument.CreateParagraph();
		XWPFRun xWPFRun3 = xWPFParagraph4.CreateRun();
		xWPFRun3.AddCarriageReturn();
		for (int j = 0; j < rltPeaks.Length; j++)
		{
			array[0].CreateRow();
			num2 = 0;
			if (pPara.bIndex)
			{
				num2++;
				array[0].GetRow(j + 1).GetCell(num2 - 1).SetText((j + 1).ToString());
			}
			if (pPara.bPeakName)
			{
				num2++;
				array[0].GetRow(j + 1).GetCell(num2 - 1).SetText(rltPeaks[j].name);
			}
			if (pPara.bPeakMaxTime)
			{
				num2++;
				array[0].GetRow(j + 1).GetCell(num2 - 1).SetText(rltPeaks[j].pkRT.ToString("F" + Class49.int_8));
			}
			if (pPara.bPeakArea)
			{
				num2++;
				array[0].GetRow(j + 1).GetCell(num2 - 1).SetText(rltPeaks[j].area.ToString("F" + Class49.int_8));
			}
			if (pPara.bPeakheight)
			{
				num2++;
				array[0].GetRow(j + 1).GetCell(num2 - 1).SetText(rltPeaks[j].height.ToString("F" + Class49.int_8));
			}
			if (pPara.bPeakHalfheight)
			{
				num2++;
				array[0].GetRow(j + 1).GetCell(num2 - 1).SetText(rltPeaks[j].WO5.ToString("F" + Class49.int_8));
			}
			if (pPara.bPeakAmont)
			{
				num2++;
				if (rltPeaks[j].amount < 0f)
				{
					rltPeaks[j].amount = 0f;
				}
				array[0].GetRow(j + 1).GetCell(num2 - 1).SetText(rltPeaks[j].amount.ToString("F" + Class49.int_8));
			}
			if (pPara.bPeakFx)
			{
				num2++;
				if (chromatogram_0[0].chromInfo.cclCalcu == CalcuStyle.ESTD)
				{
					if (rltPeaks[j].compound != null)
					{
						array[0].GetRow(j + 1).GetCell(num2 - 1).SetText(rltPeaks[j].compound.eFunc.GetEquationStr());
					}
					else
					{
						array[0].GetRow(j + 1).GetCell(num2 - 1).SetText("无");
					}
				}
				else if (chromatogram_0[0].chromInfo.cclCalcu == CalcuStyle.ISTD)
				{
					if (rltPeaks[j].compound != null)
					{
						array[0].GetRow(j + 1).GetCell(num2 - 1).SetText(rltPeaks[j].compound.iFunc.GetEquationStr());
					}
					else
					{
						array[0].GetRow(j + 1).GetCell(num2 - 1).SetText("无");
					}
				}
				else
				{
					array[0].GetRow(j + 1).GetCell(num2 - 1).SetText("无");
				}
			}
			if (pPara.bPeakLPara)
			{
				num2++;
				array[0].GetRow(j + 1).GetCell(num2 - 1).SetText(rltPeaks[j].Capacity.ToString("F" + Class49.int_8));
			}
			if (pPara.bPeakLV)
			{
				num2++;
				array[0].GetRow(j + 1).GetCell(num2 - 1).SetText(rltPeaks[j].Resolution_EP.ToString("F" + Class49.int_8));
			}
			if (pPara.bPeakOtherPara)
			{
				num2++;
				if (chromatogram_0[0].chromInfo.cclCalcu == CalcuStyle.ESTD)
				{
					if (rltPeaks[j].compound != null)
					{
						array[0].GetRow(j + 1).GetCell(num2 - 1).SetText(rltPeaks[j].compound.eFunc.corrFactor.ToString("0.00000"));
					}
					else
					{
						array[0].GetRow(j + 1).GetCell(num2 - 1).SetText("无");
					}
				}
				else if (chromatogram_0[0].chromInfo.cclCalcu == CalcuStyle.ISTD)
				{
					if (rltPeaks[j].compound != null)
					{
						array[0].GetRow(j + 1).GetCell(num2 - 1).SetText(rltPeaks[j].compound.iFunc.corrFactor.ToString("0.00000"));
					}
					else
					{
						array[0].GetRow(j + 1).GetCell(num2 - 1).SetText("无");
					}
				}
				else
				{
					array[0].GetRow(j + 1).GetCell(num2 - 1).SetText("无");
				}
			}
			if (pPara.bPeakPara)
			{
				num2++;
				if (rltPeaks[j].compound != null)
				{
					array[0].GetRow(j + 1).GetCell(num2 - 1).SetText(rltPeaks[j].compound.cmpdInfo.freeRespFactor.ToString("F" + Class49.int_8));
				}
				else
				{
					array[0].GetRow(j + 1).GetCell(num2 - 1).SetText("无");
				}
			}
			if (pPara.bPeaktailPara)
			{
				num2++;
				array[0].GetRow(j + 1).GetCell(num2 - 1).SetText(rltPeaks[j].SymmetryTailing.ToString("F" + Class49.int_8));
			}
			if (pPara.bPeakTBPara)
			{
				num2++;
				array[0].GetRow(j + 1).GetCell(num2 - 1).SetText(rltPeaks[j].Efficiency_EP.ToString("F" + Class49.int_8));
			}
			if (pPara.bPeakUTBPara)
			{
				num2++;
				array[0].GetRow(j + 1).GetCell(num2 - 1).SetText(rltPeaks[j].Eff_Column_EP.ToString("F" + Class49.int_8));
			}
			if (pPara.bPeakV)
			{
				num2++;
				array[0].GetRow(j + 1).GetCell(num2 - 1).SetText(rltPeaks[j].pkStyle.ToString());
			}
			if (pPara.bPeakheightPer)
			{
				num2++;
				array[0].GetRow(j + 1).GetCell(num2 - 1).SetText((rltPeaks[j].heightPer * 100f).ToString("F" + Class49.int_8));
			}
			if (pPara.bPeakAreaPer)
			{
				num2++;
				array[0].GetRow(j + 1).GetCell(num2 - 1).SetText((rltPeaks[j].areaPer * 100f).ToString("F" + Class49.int_8));
			}
			if (pPara.bPeakAmontPer)
			{
				num2++;
				if (rltPeaks[j].amountPer < 0f)
				{
					rltPeaks[j].amountPer = 0f;
				}
				array[0].GetRow(j + 1).GetCell(num2 - 1).SetText((rltPeaks[j].amountPer * 100f).ToString("F" + Class49.int_8));
			}
		}
		if (chromatogram.PPara.bPeakFx && chromatogram.caliGnl != null)
		{
			XWPFTable[] array2 = new XWPFTable[rltPeaks.Length];
			for (int k = 0; k < rltPeaks.Length; k++)
			{
				CaliGnl caliGnl = chromatogram.caliGnl;
				CmpdDisplay cmpdDisplay = new CmpdDisplay(WinStyle.CaliGnl, null);
				Size size = new Size(1184, 384);
				RectangleF rectangleF2 = new RectangleF(0f, 0f, size.Width, size.Height);
				Compound compound = rltPeaks[k].compound;
				if (compound == null || compound.levels == null || compound.levels.Length == 0)
				{
					continue;
				}
				Bitmap image = new Bitmap(base.Size.Width, base.Size.Height);
				string text3 = System.Windows.Forms.Application.StartupPath + "\\cmpd.Emf";
				Graphics graphics2 = Graphics.FromImage(image);
				Metafile metafile = new Metafile(text3, graphics2.GetHdc());
				Graphics graphics3 = Graphics.FromImage(metafile);
				System.Drawing.Font font = FrameDis.font;
				FrameDis.font = new System.Drawing.Font(font.FontFamily, font.Size * 2.2f, font.Style);
				string string_ = Class49.MesureUnit() + ".s";
				if (compound.cmpdInfo.respStyle == RespStyle.Height)
				{
					string_ = Class49.MesureUnit();
				}
				cmpdDisplay.rcPage = rectangleF2;
				cmpdDisplay.dskRC = rectangleF2;
				System.Drawing.Font unitFont = cmpdDisplay.options.unitFont;
				cmpdDisplay.options.titleFont = new System.Drawing.Font(unitFont.FontFamily, unitFont.Size * 2.2f, unitFont.Style);
				cmpdDisplay.options.unitFont = new System.Drawing.Font(unitFont.FontFamily, unitFont.Size * 2.2f, unitFont.Style);
				cmpdDisplay.SetCompound2(compound, bool_0: false, caliGnl.caliOption.cmpdUnit, ref string_);
				cmpdDisplay.Draw(graphics3, erase: true);
				graphics3.Dispose();
				metafile.Dispose();
				FrameDis.font = font;
				FileStream fileStream3 = new FileStream(text3, FileMode.Open, FileAccess.Read);
				XWPFParagraph xWPFParagraph5 = xWPFDocument.CreateParagraph();
				XWPFRun xWPFRun4 = xWPFParagraph5.CreateRun();
				xWPFRun4.AddPicture(fileStream3, 2, text3, 5238750, 1905000);
				fileStream3.Close();
				array2[k] = xWPFDocument.CreateTable(rltPeaks[k].compound.eFunc.funcPts.Length + 1, 5);
				array2[k].GetRow(0).GetCell(0).SetText("响应");
				array2[k].GetRow(0).GetCell(1).SetText("浓度(" + chromatogram_0[0].AmountUnit + ")");
				array2[k].GetRow(0).GetCell(2).SetText("因子");
				array2[k].GetRow(0).GetCell(3).SetText("相关系数");
				array2[k].GetRow(0).GetCell(4).SetText("方程");
				for (int l = 0; l < rltPeaks[k].compound.levels.Length; l++)
				{
					if (rltPeaks[k].compound.levels[l].used)
					{
						array2[k].GetRow(l + 1).GetCell(0).SetText(rltPeaks[k].compound.levels[l].response.ToString("F" + Class49.int_8));
						array2[k].GetRow(l + 1).GetCell(1).SetText(rltPeaks[k].compound.levels[l].amount.ToString("F" + Class49.int_8));
						array2[k].GetRow(l + 1).GetCell(2).SetText(rltPeaks[k].compound.levels[l].respFactor.ToString("F" + Class49.int_8));
						if (chromatogram_0[0].chromInfo.cclCalcu == CalcuStyle.ESTD)
						{
							double num6 = Class49.takeDouble(rltPeaks[k].compound.eFunc.corrFactor, Class49.int_8);
							array2[k].GetRow(l + 1).GetCell(3).SetText(rltPeaks[k].compound.eFunc.corrFactor.ToString("F" + Class49.int_8));
							array2[k].GetRow(l + 1).GetCell(4).SetText(rltPeaks[k].compound.eFunc.GetEquationStr());
						}
						else
						{
							double num7 = Class49.takeDouble(rltPeaks[k].compound.iFunc.corrFactor, Class49.int_8);
							array2[k].GetRow(l + 1).GetCell(3).SetText(rltPeaks[k].compound.iFunc.corrFactor.ToString("F" + Class49.int_8));
							array2[k].GetRow(l + 1).GetCell(4).SetText(rltPeaks[k].compound.iFunc.GetEquationStr());
						}
					}
				}
				for (int m = 0; m < 4; m++)
				{
					CT_Tc cTTc2 = array2[k].GetRow(0).GetCell(m).GetCTTc();
					CT_TcPr cT_TcPr2 = cTTc2.AddNewTcPr();
					cT_TcPr2.tcW = new CT_TblWidth();
					cT_TcPr2.tcW.w = "1500";
					cT_TcPr2.tcW.type = ST_TblWidth.dxa;
				}
				xWPFRun4.AddCarriageReturn();
			}
		}
		XWPFParagraph xWPFParagraph6 = xWPFDocument.CreateParagraph();
		XWPFRun xWPFRun5 = xWPFParagraph6.CreateRun();
		xWPFRun5.SetText(pPara.printTitleBotom);
		xWPFDocument.Write(fileStream);
		Process.Start(text);
	}

	public void PrintToWordCH4(Chromatogram chromatogram, string strFileName)
	{
		int num = 0;
		int num2 = 0;
		PrintPara pPara = chromatogram_0[0].PPara;
		int setChromNo = chromatogram_0.Length;
		Bitmap bitmap = new Bitmap(864, 340);
		Graphics graphics = Graphics.FromImage(bitmap);
		if (CurChrom.PPara.bPicBound)
		{
			graphics.SmoothingMode = SmoothingMode.HighQuality;
			graphics.CompositingQuality = CompositingQuality.HighQuality;
			graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
			graphics.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
			graphics.DrawRectangle(new Pen(Color.Black, 2f), 0, 0, bitmap.Width - 15, bitmap.Height - 15);
		}
		RectangleF rectangleF = new RectangleF(0f, 0f, 849f, 325f);
		ChromDisplay chromDisplay = new ChromDisplay(WinStyle.Chromatogram, null);
		chromDisplay.LinkDisChroms(chromatogram_0, ref setChromNo);
		chromDisplay.showMouseLgValue = false;
		chromDisplay.showProgTemp = false;
		chromDisplay.ShowBgChrom = true;
		chromDisplay.setShowGrid = true;
		chromDisplay.showPeakArea = false;
		chromDisplay.rcPage = rectangleF;
		chromDisplay.dskRC = rectangleF;
		chromDisplay.FrmPen.Width = 1f;
		chromDisplay.DisPen.Width = 1f;
		System.Drawing.Font peakFont = chromDisplay.options.peakFont;
		chromDisplay.options.peakFont = new System.Drawing.Font(peakFont.FontFamily, peakFont.Size * 1f, peakFont.Style);
		chromDisplay.Draw(graphics, erase: true);
		bitmap.Save(System.Windows.Forms.Application.StartupPath + "\\a1.Emf");
		string text = pPara.Title + DateTime.Now.ToString("yyyyMMdd-HHmmss") + ".docx";
		FileStream fileStream = File.OpenWrite(text);
		fileStream.Position = 0L;
		XWPFDocument xWPFDocument = new XWPFDocument();
		xWPFDocument.Document.body.sectPr = new CT_SectPr();
		CT_SectPr sectPr = xWPFDocument.Document.body.sectPr;
		XWPFParagraph xWPFParagraph = xWPFDocument.CreateParagraph();
		CT_Ftr cT_Ftr = new CT_Ftr();
		cT_Ftr.Items = new ArrayList();
		CT_SdtBlock cT_SdtBlock = new CT_SdtBlock();
		CT_SdtPr cT_SdtPr = cT_SdtBlock.AddNewSdtPr();
		CT_SdtDocPart cT_SdtDocPart = cT_SdtPr.AddNewDocPartObj();
		cT_SdtDocPart.AddNewDocPartGallery().val = "PageNumbers (Bottom of Page)";
		cT_SdtDocPart.docPartUnique = new CT_OnOff();
		CT_SdtContentBlock cT_SdtContentBlock = cT_SdtBlock.AddNewSdtContent();
		CT_P cT_P = cT_SdtContentBlock.AddNewP();
		CT_PPr cT_PPr = cT_P.AddNewPPr();
		cT_PPr.AddNewJc().val = ST_Jc.center;
		cT_P.Items = new ArrayList();
		CT_SimpleField cT_SimpleField = new CT_SimpleField();
		cT_SimpleField.instr = " PAGE   \\*MERGEFORMAT ";
		cT_P.Items.Add(cT_SimpleField);
		cT_Ftr.Items.Add(cT_SdtBlock);
		XWPFRelation fOOTER = XWPFRelation.FOOTER;
		XWPFFooter xWPFFooter = (XWPFFooter)xWPFDocument.CreateRelationship(fOOTER, XWPFFactory.GetInstance(), xWPFDocument.FooterList.Count + 1);
		xWPFFooter.SetHeaderFooter(cT_Ftr);
		CT_HdrFtrRef cT_HdrFtrRef = sectPr.AddNewFooterReference();
		cT_HdrFtrRef.type = ST_HdrFtr.@default;
		cT_HdrFtrRef.id = xWPFFooter.GetPackageRelationship().Id;
		XWPFParagraph xWPFParagraph2 = xWPFDocument.CreateParagraph();
		xWPFParagraph2.Alignment = ParagraphAlignment.CENTER;
		XWPFRun xWPFRun = xWPFParagraph2.CreateRun();
		xWPFRun.FontSize = 16;
		xWPFRun.IsBold = true;
		xWPFRun.AppendText(pPara.Title);
		XWPFParagraph xWPFParagraph3 = xWPFDocument.CreateParagraph();
		xWPFParagraph3.Alignment = ParagraphAlignment.LEFT;
		XWPFRun xWPFRun2 = xWPFParagraph3.CreateRun();
		xWPFRun2.AddCarriageReturn();
		xWPFRun2.SetText(pPara.PrintTitleTop);
		xWPFRun2.AddCarriageReturn();
		if (CurChrom.PPara.bPTime)
		{
			xWPFRun2.AppendText("打印时间:" + DateTime.Now.ToString());
			xWPFRun2.AddCarriageReturn();
		}
		if (CurChrom.PPara.bJtime)
		{
			xWPFRun2.AppendText("进样时间:" + chromatogram_1.injAnalysis.dtAcquire.ToString());
			xWPFRun2.AddCarriageReturn();
		}
		if (CurChrom.PPara.bfname)
		{
			xWPFRun2.AppendText("打开的谱图文件:" + m_strChormFileName);
			xWPFRun2.AddCarriageReturn();
		}
		int num3 = 5238750;
		int num4 = 2857500;
		string text2 = System.Windows.Forms.Application.StartupPath + "\\a1.Emf";
		FileStream fileStream2 = new FileStream(text2, FileMode.Open, FileAccess.Read);
		xWPFRun2.AddPicture(fileStream2, 2, text2, num3, num4);
		fileStream2.Close();
		xWPFRun2.AddCarriageReturn();
		Peak[] rltPeaks = chromatogram_0[0].RltPeaks;
		float[] array = new float[1];
		try
		{
			array = rltPeaks[0].compound.eFunc.Calcu_amountF(CurChrom.mtdSetup.chromInfoR.UvwsStartT);
		}
		catch
		{
			array[0] = 0f;
		}
		float num5 = 0f;
		if (rltPeaks.Length != 0)
		{
			num5 = float.Parse(rltPeaks[0].amount.ToString("F" + Class49.int_8));
		}
		float num6 = 0f;
		if (rltPeaks.Length > 1)
		{
			num6 = float.Parse(rltPeaks[1].amount.ToString("F" + Class49.int_8));
		}
		float num7 = float.Parse(array[0].ToString("F" + Class49.int_8));
		float num8 = 0f;
		XWPFTable[] array2 = new XWPFTable[rltPeaks.Length];
		if (array2.Length < 1)
		{
			Array.Resize(ref array2, 1);
		}
		num = 0;
		if (pPara.bIndex)
		{
			num++;
		}
		if (pPara.bPeakName)
		{
			num++;
		}
		if (pPara.bPeakMaxTime)
		{
			num++;
		}
		if (pPara.bPeakArea)
		{
			num++;
		}
		if (pPara.bPeakheight)
		{
			num++;
		}
		if (pPara.bPeakAmont)
		{
			num++;
		}
		array2[0] = xWPFDocument.CreateTable(1, num);
		num2 = 0;
		if (pPara.bIndex)
		{
			num2++;
			array2[0].GetRow(0).GetCell(num2 - 1).SetText("序号");
		}
		if (pPara.bPeakName)
		{
			num2++;
			array2[0].GetRow(0).GetCell(num2 - 1).SetText("名称");
		}
		if (pPara.bPeakMaxTime)
		{
			num2++;
			array2[0].GetRow(0).GetCell(num2 - 1).SetText("保留时间(min)");
		}
		if (pPara.bPeakArea)
		{
			num2++;
			array2[0].GetRow(0).GetCell(num2 - 1).SetText("峰面积");
		}
		if (pPara.bPeakheight)
		{
			num2++;
			array2[0].GetRow(0).GetCell(num2 - 1).SetText("峰高");
		}
		if (pPara.bPeakAmont)
		{
			num2++;
			array2[0].GetRow(0).GetCell(num2 - 1).SetText("浓度(" + chromatogram_0[0].AmountUnit + ")");
		}
		for (int i = 0; i < num; i++)
		{
			CT_Tc cTTc = array2[0].GetRow(0).GetCell(i).GetCTTc();
			CT_TcPr cT_TcPr = cTTc.AddNewTcPr();
			cT_TcPr.tcW = new CT_TblWidth();
			cT_TcPr.tcW.w = "6000";
			cT_TcPr.tcW.type = ST_TblWidth.dxa;
		}
		XWPFParagraph xWPFParagraph4 = xWPFDocument.CreateParagraph();
		XWPFRun xWPFRun3 = xWPFParagraph4.CreateRun();
		xWPFRun3.AddCarriageReturn();
		try
		{
			for (int j = 0; j < 6; j++)
			{
				array2[0].CreateRow();
				num2 = 0;
				switch (j)
				{
				case 0:
					if (pPara.bIndex)
					{
						num2++;
						array2[0].GetRow(j + 1).GetCell(num2 - 1).SetText((j + 1).ToString());
					}
					if (pPara.bPeakName)
					{
						num2++;
						array2[0].GetRow(j + 1).GetCell(num2 - 1).SetText("总烃");
					}
					if (pPara.bPeakMaxTime)
					{
						num2++;
						if (rltPeaks.Length > j)
						{
							array2[0].GetRow(j + 1).GetCell(num2 - 1).SetText(rltPeaks[j].pkRT.ToString("F" + Class49.int_8));
						}
					}
					if (pPara.bPeakArea)
					{
						num2++;
						if (rltPeaks.Length > j)
						{
							array2[0].GetRow(j + 1).GetCell(num2 - 1).SetText(rltPeaks[j].area.ToString("F" + Class49.int_8));
						}
					}
					if (pPara.bPeakheight)
					{
						num2++;
						if (rltPeaks.Length > j)
						{
							array2[0].GetRow(j + 1).GetCell(num2 - 1).SetText(rltPeaks[j].height.ToString("F" + Class49.int_8));
						}
					}
					if (!pPara.bPeakAmont)
					{
						break;
					}
					num2++;
					if (rltPeaks.Length > j)
					{
						if (rltPeaks[j].amount < 0f)
						{
							rltPeaks[j].amount = 0f;
						}
						array2[0].GetRow(j + 1).GetCell(num2 - 1).SetText(rltPeaks[j].amount.ToString("F" + Class49.int_8));
					}
					break;
				case 1:
					if (pPara.bIndex)
					{
						num2++;
						array2[0].GetRow(j + 1).GetCell(num2 - 1).SetText((j + 1).ToString());
					}
					if (pPara.bPeakName)
					{
						num2++;
						array2[0].GetRow(j + 1).GetCell(num2 - 1).SetText("甲烷");
					}
					if (pPara.bPeakMaxTime)
					{
						num2++;
						if (rltPeaks.Length > j)
						{
							array2[0].GetRow(j + 1).GetCell(num2 - 1).SetText(rltPeaks[j].pkRT.ToString("F" + Class49.int_8));
						}
					}
					if (pPara.bPeakArea)
					{
						num2++;
						if (rltPeaks.Length > j)
						{
							array2[0].GetRow(j + 1).GetCell(num2 - 1).SetText(rltPeaks[j].area.ToString("F" + Class49.int_8));
						}
					}
					if (pPara.bPeakheight)
					{
						num2++;
						if (rltPeaks.Length > j)
						{
							array2[0].GetRow(j + 1).GetCell(num2 - 1).SetText(rltPeaks[j].height.ToString("F" + Class49.int_8));
						}
					}
					if (!pPara.bPeakAmont)
					{
						break;
					}
					num2++;
					if (rltPeaks.Length > j)
					{
						if (rltPeaks[j].amount < 0f)
						{
							rltPeaks[j].amount = 0f;
						}
						array2[0].GetRow(j + 1).GetCell(num2 - 1).SetText(rltPeaks[j].amount.ToString("F" + Class49.int_8));
					}
					break;
				case 2:
					if (pPara.bIndex)
					{
						num2++;
						array2[0].GetRow(j + 1).GetCell(num2 - 1).SetText((j + 1).ToString());
					}
					if (pPara.bPeakName)
					{
						num2++;
						array2[0].GetRow(j + 1).GetCell(num2 - 1).SetText("氧气");
					}
					if (pPara.bPeakMaxTime)
					{
						num2++;
						array2[0].GetRow(j + 1).GetCell(num2 - 1).SetText("--");
					}
					if (pPara.bPeakArea)
					{
						num2++;
						array2[0].GetRow(j + 1).GetCell(num2 - 1).SetText(CurChrom.mtdSetup.chromInfoR.UvwsStartT.ToString("F" + Class49.int_8));
					}
					if (pPara.bPeakheight)
					{
						num2++;
						array2[0].GetRow(j + 1).GetCell(num2 - 1).SetText("--");
					}
					if (pPara.bPeakAmont)
					{
						num2++;
						array2[0].GetRow(j + 1).GetCell(num2 - 1).SetText(array[0].ToString("F" + Class49.int_8));
					}
					break;
				case 3:
					if (pPara.bIndex)
					{
						num2++;
						array2[0].GetRow(j + 1).GetCell(num2 - 1).SetText((j + 1).ToString());
					}
					if (pPara.bPeakName)
					{
						num2++;
						array2[0].GetRow(j + 1).GetCell(num2 - 1).SetText("总烃去氧");
					}
					if (pPara.bPeakMaxTime)
					{
						num2++;
						array2[0].GetRow(j + 1).GetCell(num2 - 1).SetText("--");
					}
					if (pPara.bPeakArea)
					{
						num2++;
						array2[0].GetRow(j + 1).GetCell(num2 - 1).SetText("--");
					}
					if (pPara.bPeakheight)
					{
						num2++;
						array2[0].GetRow(j + 1).GetCell(num2 - 1).SetText("--");
					}
					if (pPara.bPeakAmont)
					{
						num2++;
						array2[0].GetRow(j + 1).GetCell(num2 - 1).SetText((num5 - num7).ToString("F" + Class49.int_8));
					}
					break;
				case 4:
					if (pPara.bIndex)
					{
						num2++;
						array2[0].GetRow(j + 1).GetCell(num2 - 1).SetText((j + 1).ToString());
					}
					if (pPara.bPeakName)
					{
						num2++;
						array2[0].GetRow(j + 1).GetCell(num2 - 1).SetText("非甲烷总烃(以碳计)");
					}
					if (pPara.bPeakMaxTime)
					{
						num2++;
						array2[0].GetRow(j + 1).GetCell(num2 - 1).SetText("--");
					}
					if (pPara.bPeakArea)
					{
						num2++;
						array2[0].GetRow(j + 1).GetCell(num2 - 1).SetText("--");
					}
					if (pPara.bPeakheight)
					{
						num2++;
						array2[0].GetRow(j + 1).GetCell(num2 - 1).SetText("--");
					}
					if (pPara.bPeakAmont)
					{
						num2++;
						num8 = num5 - num6 - num7;
						if (num8 < 0f)
						{
							num8 = 0f;
						}
						array2[0].GetRow(j + 1).GetCell(num2 - 1).SetText((num8 * 0.75f).ToString("F" + Class49.int_8));
					}
					break;
				case 5:
					if (pPara.bIndex)
					{
						num2++;
						array2[0].GetRow(j + 1).GetCell(num2 - 1).SetText((j + 1).ToString());
					}
					if (pPara.bPeakName)
					{
						num2++;
						array2[0].GetRow(j + 1).GetCell(num2 - 1).SetText("非甲烷总烃(以甲烷计)");
					}
					if (pPara.bPeakMaxTime)
					{
						num2++;
						array2[0].GetRow(j + 1).GetCell(num2 - 1).SetText("--");
					}
					if (pPara.bPeakArea)
					{
						num2++;
						array2[0].GetRow(j + 1).GetCell(num2 - 1).SetText("--");
					}
					if (pPara.bPeakheight)
					{
						num2++;
						array2[0].GetRow(j + 1).GetCell(num2 - 1).SetText("--");
					}
					if (pPara.bPeakAmont)
					{
						num2++;
						array2[0].GetRow(j + 1).GetCell(num2 - 1).SetText(num8.ToString("F" + Class49.int_8));
					}
					break;
				}
			}
		}
		catch
		{
		}
		if (CurChrom.PPara.bPeakFx && CurChrom.caliGnl != null)
		{
			XWPFTable[] array3 = new XWPFTable[rltPeaks.Length];
			for (int k = 0; k < rltPeaks.Length; k++)
			{
				CaliGnl caliGnl = CurChrom.caliGnl;
				CmpdDisplay cmpdDisplay = new CmpdDisplay(WinStyle.CaliGnl, null);
				Size size = new Size(1184, 384);
				RectangleF rectangleF2 = new RectangleF(0f, 0f, size.Width, size.Height);
				Compound compound = rltPeaks[k].compound;
				if (compound == null || compound.levels == null || compound.levels.Length == 0)
				{
					continue;
				}
				Bitmap image = new Bitmap(base.Size.Width, base.Size.Height);
				string text3 = System.Windows.Forms.Application.StartupPath + "\\cmpd.Emf";
				Graphics graphics2 = Graphics.FromImage(image);
				Metafile metafile = new Metafile(text3, graphics2.GetHdc());
				Graphics graphics3 = Graphics.FromImage(metafile);
				System.Drawing.Font font = FrameDis.font;
				FrameDis.font = new System.Drawing.Font(font.FontFamily, font.Size * 2.2f, font.Style);
				string string_ = Class49.MesureUnit() + ".s";
				if (compound.cmpdInfo.respStyle == RespStyle.Height)
				{
					string_ = Class49.MesureUnit();
				}
				cmpdDisplay.rcPage = rectangleF2;
				cmpdDisplay.dskRC = rectangleF2;
				System.Drawing.Font unitFont = cmpdDisplay.options.unitFont;
				cmpdDisplay.options.titleFont = new System.Drawing.Font(unitFont.FontFamily, unitFont.Size * 2.2f, unitFont.Style);
				cmpdDisplay.options.unitFont = new System.Drawing.Font(unitFont.FontFamily, unitFont.Size * 2.2f, unitFont.Style);
				cmpdDisplay.SetCompound2(compound, bool_0: false, caliGnl.caliOption.cmpdUnit, ref string_);
				cmpdDisplay.Draw(graphics3, erase: true);
				graphics3.Dispose();
				metafile.Dispose();
				FrameDis.font = font;
				FileStream fileStream3 = new FileStream(text3, FileMode.Open, FileAccess.Read);
				XWPFParagraph xWPFParagraph5 = xWPFDocument.CreateParagraph();
				XWPFRun xWPFRun4 = xWPFParagraph5.CreateRun();
				xWPFRun4.AddPicture(fileStream3, 2, text3, 5238750, 1905000);
				fileStream3.Close();
				array3[k] = xWPFDocument.CreateTable(rltPeaks[k].compound.eFunc.funcPts.Length + 1, 5);
				array3[k].GetRow(0).GetCell(0).SetText("响应");
				array3[k].GetRow(0).GetCell(1).SetText("浓度(" + chromatogram_0[0].AmountUnit + ")");
				array3[k].GetRow(0).GetCell(2).SetText("因子");
				array3[k].GetRow(0).GetCell(3).SetText("相关系数");
				array3[k].GetRow(0).GetCell(4).SetText("方程");
				for (int l = 0; l < rltPeaks[k].compound.levels.Length; l++)
				{
					if (rltPeaks[k].compound.levels[l].used)
					{
						array3[k].GetRow(l + 1).GetCell(0).SetText(rltPeaks[k].compound.levels[l].response.ToString("F" + Class49.int_8));
						array3[k].GetRow(l + 1).GetCell(1).SetText(rltPeaks[k].compound.levels[l].amount.ToString("F" + Class49.int_8));
						array3[k].GetRow(l + 1).GetCell(2).SetText(rltPeaks[k].compound.levels[l].respFactor.ToString("F" + Class49.int_8));
						if (chromatogram_0[0].chromInfo.cclCalcu == CalcuStyle.ESTD)
						{
							double num9 = Class49.takeDouble(rltPeaks[k].compound.eFunc.corrFactor, Class49.int_8);
							array3[k].GetRow(l + 1).GetCell(3).SetText(num9.ToString("F" + Class49.int_8));
							array3[k].GetRow(l + 1).GetCell(4).SetText(rltPeaks[k].compound.eFunc.GetEquationStr());
						}
						else
						{
							double num10 = Class49.takeDouble(rltPeaks[k].compound.iFunc.corrFactor, Class49.int_8);
							array3[k].GetRow(l + 1).GetCell(3).SetText(num10.ToString("F" + Class49.int_8));
							array3[k].GetRow(l + 1).GetCell(4).SetText(rltPeaks[k].compound.iFunc.GetEquationStr());
						}
					}
				}
				for (int m = 0; m < 4; m++)
				{
					CT_Tc cTTc2 = array3[k].GetRow(0).GetCell(m).GetCTTc();
					CT_TcPr cT_TcPr2 = cTTc2.AddNewTcPr();
					cT_TcPr2.tcW = new CT_TblWidth();
					cT_TcPr2.tcW.w = "1500";
					cT_TcPr2.tcW.type = ST_TblWidth.dxa;
				}
				xWPFRun4.AddCarriageReturn();
			}
		}
		XWPFParagraph xWPFParagraph6 = xWPFDocument.CreateParagraph();
		XWPFRun xWPFRun5 = xWPFParagraph6.CreateRun();
		xWPFRun5.SetText(pPara.printTitleBotom);
		xWPFDocument.Write(fileStream);
		Process.Start(text);
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
			Console.WriteLine("");
		}
		catch (Exception ex)
		{
			Console.WriteLine(ex.Message);
		}
		document = document2;
		Range range2 = document.Paragraphs.Last.Range;
		range2.Text = range2.Text ?? "";
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
				Clipboard.SetText(array8[j] + "" + array2[j].ToString("0.000") + "    " + array3[j].ToString("F" + Class49.int_8) + " " + array5[j].ToString("F" + Class49.int_8) + "       " + array7[j].ToString("0.000") + "\r\n\r\n");
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
		float num19 = 0f;
		float num20 = 0f;
		float num21 = 0f;
		float num22 = 0f;
		float num23 = 0f;
		float num24 = 0f;
		float num25 = 0f;
		float num26 = 0f;
		float num27 = 0f;
		float num28 = 0f;
		float num29 = 0f;
		float num30 = 0f;
		float num31 = 216.89f;
		float num32 = 4.713f;
		float num33 = 0f;
		float num34 = 0f;
		float num35 = 0f;
		float num36 = 0f;
		float num37 = 0f;
		float num38 = 0f;
		float num39 = 0f;
		float num40 = 0f;
		float num41 = 0f;
		float num42 = 0f;
		float num43 = 0f;
		float num44 = 0f;
		float num45 = 0f;
		float num46 = 0f;
		float num47 = 0f;
		float num48 = 0f;
		float num49 = 216.89f;
		float num50 = 4.713f;
		float num51 = 0f;
		float num52 = 0f;
		float num53 = 0f;
		float num54 = 0f;
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
		Clipboard.SetText("序号   保留时间    名称             浓度       浓度百分比       峰面积        峰高\r\n\r\n");
		rtprtb.Paste();
		Clipboard.Clear();
		Clipboard.SetText("────────────────────────────────────────────\r\n\r\n");
		rtprtb.Paste();
		Peak[] array = chromatogram_0[0].GetPeakFromCompound();
		double num55 = 0.0;
		double num56 = 0.0;
		double num57 = 0.0;
		frmParam.bMinus = false;
		if (frmParam.bMinus)
		{
			Array.Resize(ref array, array.Length + 1);
			array[array.Length - 1] = new Peak();
			array[array.Length - 1].compound = array[array.Length - 2].compound;
			array[array.Length - 1].name = "H2";
			array[array.Length - 1].amount = 100f - CurChrom.whlAmount;
			for (int i = 0; i < array.Length; i++)
			{
				array[i].amountPer = array[i].amount / 100f;
			}
		}
		for (int j = 0; j < array.Length; j++)
		{
			Peak peak = array[j];
			Clipboard.Clear();
			string text = ((j <= 9) ? (" " + j) : j.ToString());
			string text2 = peak.pkRT.ToString("000.0000");
			for (int k = 0; k < text2.Length && text2[k] == '0'; k++)
			{
				if (k + 1 < text2.Length && text2[k + 1] != '.')
				{
					char[] array2 = text2.ToCharArray();
					array2[k] = ' ';
					text2 = new string(array2);
				}
			}
			string text3 = peak.name;
			int num58 = 12 - Encoding.Default.GetBytes(text3.Trim()).Length;
			for (int l = 0; l < num58; l++)
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
				for (int m = 0; m < text4.Length && text4[m] == '0'; m++)
				{
					if (m + 1 < text4.Length && text4[m + 1] != '.')
					{
						char[] array3 = text4.ToCharArray();
						array3[m] = ' ';
						text4 = new string(array3);
					}
				}
			}
			string text5;
			if (peak.amountPer != -1f)
			{
				text5 = (peak.amountPer * 100f).ToString("00.000");
				for (int n = 0; n < text5.Length && text5[n] == '0'; n++)
				{
					if (n + 1 < text5.Length && text5[n + 1] != '.')
					{
						char[] array4 = text5.ToCharArray();
						array4[n] = ' ';
						text5 = new string(array4);
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
				for (int num59 = 0; num59 < text6.Length && text6[num59] == '0'; num59++)
				{
					if (num59 + 1 < text6.Length && text6[num59 + 1] != '.')
					{
						char[] array5 = text6.ToCharArray();
						array5[num59] = ' ';
						text6 = new string(array5);
					}
				}
			}
			else
			{
				text6 = "   0.0   ";
			}
			if (peak.areaPer != -1f)
			{
				string text7 = (peak.areaPer * 100f).ToString("00.000");
				for (int num60 = 0; num60 < text7.Length && text7[num60] == '0'; num60++)
				{
					if (num60 + 1 < text7.Length && text7[num60 + 1] != '.')
					{
						char[] array6 = text7.ToCharArray();
						array6[num60] = ' ';
						text7 = new string(array6);
					}
				}
			}
			else
			{
				string text7 = " 0.0  ";
			}
			string text8;
			if (peak.height != -1f)
			{
				text8 = peak.height.ToString("0000.0000");
				for (int num61 = 0; num61 < text8.Length && text8[num61] == '0'; num61++)
				{
					if (num61 + 1 < text8.Length && text8[num61 + 1] != '.')
					{
						char[] array7 = text8.ToCharArray();
						array7[num61] = ' ';
						text8 = new string(array7);
					}
				}
			}
			else
			{
				text8 = "   0.0   ";
			}
			if (peak.heightPer != -1f)
			{
				string text9 = (peak.heightPer * 100f).ToString("00.000");
				for (int num62 = 0; num62 < text9.Length && text9[num62] == '0'; num62++)
				{
					if (num62 + 1 < text9.Length && text9[num62 + 1] != '.')
					{
						char[] array8 = text9.ToCharArray();
						array8[num62] = ' ';
						text9 = new string(array8);
					}
				}
			}
			else
			{
				string text9 = " 0.0  ";
			}
			Clipboard.SetText(text + "      " + text2 + "    " + text3 + " " + text4 + "       " + text5 + "%       " + text6 + "     " + text8 + "\r\n\r\n");
			rtprtb.Paste();
			if (peak.amount != -1f)
			{
				num55 += (double)peak.amount;
			}
			num56 += (double)peak.area;
			num57 += (double)peak.height;
		}
		Clipboard.Clear();
		Clipboard.SetText("────────────────────────────────────────────\r\n\r\n");
		rtprtb.Paste();
		Clipboard.Clear();
		if (mstSetChromForm.cbprsUncalBase.SelectedIndex == 0)
		{
			Clipboard.SetText("总计                             " + num55.ToString("F" + Class49.int_8) + "             100%       " + num56.ToString("F" + Class49.int_8) + "        100%\r\n\r\n\r\n");
		}
		else
		{
			Clipboard.SetText("总计                             " + num55.ToString("F" + Class49.int_8) + "             100%       " + num57.ToString("F" + Class49.int_8) + "        100%\r\n\r\n\r\n");
		}
		rtprtb.Paste();
		Clipboard.Clear();
		int num63 = 0;
		while (1 <= array.Length && num63 < array.Length)
		{
			if (array[num63].compound != null)
			{
				if (array[num63].compound.eFunc.curveFit == CurveFit.Free)
				{
					num += Program.getCharacteristic(array[num63].name, array[num63].amountPer, 1, 0f);
					num4 += Program.getCharacteristic(array[num63].name, array[num63].amountPer, 2, 0f);
					num12 += Program.getCharacteristic(array[num63].name, array[num63].amountPer, 3, 0f);
					num11 += Program.getCharacteristic(array[num63].name, array[num63].amountPer, 4, 0f);
					num2 += Program.getCharacteristic(array[num63].name, array[num63].amountPer, 5, 0f);
					num5 += Program.getCharacteristic(array[num63].name, array[num63].amountPer, 6, 0f);
					num3 += Program.getCharacteristic(array[num63].name, array[num63].amountPer, 7, 0f);
					num19 += Program.getCharacteristic(array[num63].name, array[num63].amountPer, 1, 15f);
					num22 += Program.getCharacteristic(array[num63].name, array[num63].amountPer, 2, 15f);
					num30 += Program.getCharacteristic(array[num63].name, array[num63].amountPer, 3, 15f);
					num29 += Program.getCharacteristic(array[num63].name, array[num63].amountPer, 4, 15f);
					num20 += Program.getCharacteristic(array[num63].name, array[num63].amountPer, 5, 15f);
					num23 += Program.getCharacteristic(array[num63].name, array[num63].amountPer, 6, 15f);
					num21 += Program.getCharacteristic(array[num63].name, array[num63].amountPer, 7, 15f);
					num37 += Program.getCharacteristic(array[num63].name, array[num63].amountPer, 1, 20f);
					num40 += Program.getCharacteristic(array[num63].name, array[num63].amountPer, 2, 20f);
					num48 += Program.getCharacteristic(array[num63].name, array[num63].amountPer, 3, 20f);
					num47 += Program.getCharacteristic(array[num63].name, array[num63].amountPer, 4, 20f);
					num38 += Program.getCharacteristic(array[num63].name, array[num63].amountPer, 5, 20f);
					num41 += Program.getCharacteristic(array[num63].name, array[num63].amountPer, 6, 20f);
					num39 += Program.getCharacteristic(array[num63].name, array[num63].amountPer, 7, 20f);
				}
				else
				{
					num += Program.getCharacteristic(array[num63].name, array[num63].amountPer, 1, 0f);
					num4 += Program.getCharacteristic(array[num63].name, array[num63].amountPer, 2, 0f);
					num12 += Program.getCharacteristic(array[num63].name, array[num63].amountPer, 3, 0f);
					num11 += Program.getCharacteristic(array[num63].name, array[num63].amountPer, 4, 0f);
					num2 += Program.getCharacteristic(array[num63].name, array[num63].amountPer, 5, 0f);
					num5 += Program.getCharacteristic(array[num63].name, array[num63].amountPer, 6, 0f);
					num3 += Program.getCharacteristic(array[num63].name, array[num63].amountPer, 7, 0f);
					num19 += Program.getCharacteristic(array[num63].name, array[num63].amountPer, 1, 15f);
					num22 += Program.getCharacteristic(array[num63].name, array[num63].amountPer, 2, 15f);
					num30 += Program.getCharacteristic(array[num63].name, array[num63].amountPer, 3, 15f);
					num29 += Program.getCharacteristic(array[num63].name, array[num63].amountPer, 4, 15f);
					num20 += Program.getCharacteristic(array[num63].name, array[num63].amountPer, 5, 15f);
					num23 += Program.getCharacteristic(array[num63].name, array[num63].amountPer, 6, 15f);
					num21 += Program.getCharacteristic(array[num63].name, array[num63].amountPer, 7, 15f);
					num37 += Program.getCharacteristic(array[num63].name, array[num63].amountPer, 1, 20f);
					num40 += Program.getCharacteristic(array[num63].name, array[num63].amountPer, 2, 20f);
					num48 += Program.getCharacteristic(array[num63].name, array[num63].amountPer, 3, 20f);
					num47 += Program.getCharacteristic(array[num63].name, array[num63].amountPer, 4, 20f);
					num38 += Program.getCharacteristic(array[num63].name, array[num63].amountPer, 5, 20f);
					num41 += Program.getCharacteristic(array[num63].name, array[num63].amountPer, 6, 20f);
					num39 += Program.getCharacteristic(array[num63].name, array[num63].amountPer, 7, 20f);
				}
			}
			num63++;
		}
		double d = num12;
		float num64 = (float)Math.Sqrt(d);
		num2 = num / 0.0041858517f;
		num5 = num4 / 0.0041858517f;
		num6 = num / num64;
		num7 = num2 / num64;
		num8 = num4 / num64;
		num9 = num5 / num64;
		double num65 = num15;
		float num66 = (float)Math.Pow(num65, 2.0);
		num10 = (1f + 0.0054f * num66) * (num16 + 0.3f * num17 + 0.6f * num18) / num64;
		string text10 = Lang.PS("在标准状态（0℃ 273.15K、101325Pa）", "In standard condition（273.15K、101325Pa）") + "\n" + Lang.PS("平均分子量 =", "average mean molecular weight =") + num3.ToString("0.000") + "\n" + Lang.PS("高热值 =", "high heating value =") + num.ToString("0.000") + "(MJ / Nm3) = " + num2.ToString("0.000") + "(KCal / Nm3)\n" + Lang.PS("低热值 =", "low heating value =") + num4.ToString("0.000") + "(MJ / Nm3) = " + num5.ToString("0.000") + "(KCal / Nm3)\n" + Lang.PS("高热值华白数 =", "High calorific value White number =") + num6.ToString("0.000") + "(MJ / Nm3) = " + num7.ToString("0.000") + "(KCal / Nm3)\n" + Lang.PS("低热值华白数 =", "Low calorific value White number =") + num8.ToString("0.000") + "(MJ / Nm3) = " + num9.ToString("0.000") + "(KCal / Nm3)\n" + Lang.PS("密度 =", "density =") + num11.ToString("0.000") + "(kg / m3)\n" + Lang.PS("相对密度 =", "relative density =") + num12.ToString("0.000") + "\n" + Lang.PS("气化率 =", "rate of gasification =") + (1000f / num11).ToString("0.000") + "\n" + Lang.PS("临界温度 =", "critical temperature =") + num13.ToString("0.000") + "(K)\n" + Lang.PS("临界压力 =", "critical pressure =") + num14.ToString("0.000") + "(MPa)\n";
		double d2 = num30;
		num20 = num19 / 0.0041858517f;
		num23 = num22 / 0.0041858517f;
		float num67 = (float)Math.Sqrt(d2);
		num24 = num19 / num67;
		num25 = num20 / num67;
		num26 = num22 / num67;
		num27 = num23 / num67;
		double num68 = num33;
		float num69 = (float)Math.Pow(num68, 2.0);
		num28 = (1f + 0.0054f * num69) * (num34 + 0.3f * num35 + 0.6f * num36) / num67;
		string text11 = Lang.PS("在（15℃ 288.15K、101325Pa）下", "In = condition（288.15K、101325Pa）") + "\n" + Lang.PS("平均分子量 =", "average mean molecular weight =") + num21.ToString("0.000") + "\n" + Lang.PS("高热值 =", "high heating value =") + num19.ToString("0.000") + "(MJ / Nm3) = " + num20.ToString("0.000") + "(KCal / Nm3)\n" + Lang.PS("低热值 =", "low heating value =") + num22.ToString("0.000") + "(MJ / Nm3) = " + num23.ToString("0.000") + "(KCal / Nm3)\n" + Lang.PS("高热值华白数 =", "High calorific value White number =") + num24.ToString("0.000") + "(MJ / Nm3) = " + num25.ToString("0.000") + "(KCal / Nm3)\n" + Lang.PS("低热值华白数 =", "Low calorific value White number =") + num26.ToString("0.000") + "(MJ / Nm3) = " + num27.ToString("0.000") + "(KCal / Nm3)\n" + Lang.PS("密度 =", "density =") + num29.ToString("0.000") + "(kg / m3)\n" + Lang.PS("相对密度 =", "relative density =") + num30.ToString("0.000") + "\n" + Lang.PS("气化率 =", "rate of gasification =") + (1000f / num29).ToString("0.000") + "\n" + Lang.PS("临界温度 =", "critical temperature =") + num31.ToString("0.000") + "(K)\n" + Lang.PS("临界压力 =", "critical pressure =") + num32.ToString("0.000") + "(MPa)\n";
		double d3 = num48;
		float num70 = (float)Math.Sqrt(d3);
		num38 = num37 / 0.0041858517f;
		num41 = num40 / 0.0041858517f;
		num42 = num37 / num70;
		num43 = num38 / num70;
		num44 = num40 / num70;
		num45 = num41 / num70;
		double num71 = num51;
		float num72 = (float)Math.Pow(num71, 2.0);
		num46 = (1f + 0.0054f * num72) * (num52 + 0.3f * num53 + 0.6f * num54) / num70;
		string text12 = Lang.PS("在（20℃ 293.15K、101325Pa）", "In standard condition（293.15K、101325Pa）") + "\n" + Lang.PS("平均分子量 =", "average mean molecular weight =") + num39.ToString("0.000") + "\n" + Lang.PS("高热值 =", "high heating value =") + num37.ToString("0.000") + "(MJ / Nm3) = " + num38.ToString("0.000") + "(KCal / Nm3)\n" + Lang.PS("低热值 =", "low heating value =") + num40.ToString("0.000") + "(MJ / Nm3) = " + num41.ToString("0.000") + "(KCal / Nm3)\n" + Lang.PS("高热值华白数 =", "High calorific value White number =") + num42.ToString("0.000") + "(MJ / Nm3) = " + num43.ToString("0.000") + "(KCal / Nm3)\n" + Lang.PS("低热值华白数 =", "Low calorific value White number =") + num44.ToString("0.000") + "(MJ / Nm3) = " + num45.ToString("0.000") + "(KCal / Nm3)\n" + Lang.PS("密度 =", "density =") + num47.ToString("0.000") + "(kg / m3)\n" + Lang.PS("相对密度 =", "relative density =") + num48.ToString("0.000") + "\n" + Lang.PS("气化率 =", "rate of gasification =") + (1000f / num47).ToString("0.000") + "\n" + Lang.PS("临界温度 =", "critical temperature =") + num49.ToString("0.000") + "(K)\n" + Lang.PS("临界压力 =", "critical pressure =") + num50.ToString("0.000") + "(MPa)\n";
		Clipboard.SetText("────────────────────────────────────────────\r\n\r\n");
		rtprtb.Paste();
		Clipboard.Clear();
		Clipboard.SetText(text10 + "\r\n\r\n");
		rtprtb.Paste();
		Clipboard.Clear();
		Clipboard.SetText(text11 + "\r\n\r\n");
		rtprtb.Paste();
		Clipboard.Clear();
		Clipboard.SetText(text12 + "\r\n\r\n");
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
			Clipboard.SetText("总计                             " + num.ToString("F" + Class49.int_8) + "             100%       " + num2.ToString("F" + Class49.int_8) + "        100%\r\n\r\n\r\n");
		}
		else
		{
			Clipboard.SetText("总计                             " + num.ToString("F" + Class49.int_8) + "             100%       " + num3.ToString("F" + Class49.int_8) + "        100%\r\n\r\n\r\n");
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
			mstSetChromForm.bUseSet_Click(null, null);
		}
	}

	public void OpenChrom(Chromatogram chromatogram, string chromName, bool sampling, bool useCurrent)
	{
		if (chromatogram != null && chromatogram.signal != null)
		{
			chromatogram.signal.Smooth(frmParam.iSmooths);
		}
		m_strChormFileName = chromName;
		cbNMHC.Checked = chromatogram.bEnNMHC;
		if (chromName.Contains("fid") || chromName.Contains("FID") || chromName.Contains("pdd") || chromName.Contains("PDD"))
		{
			Class49.bool_4 = true;
			string_277 = "面积\n[" + Class49.MesureUnit() + ".s]";
			string_278 = "高度\n[" + Class49.MesureUnit() + "]";
			lbyUnit.Text = "pA";
			label29.Text = "pA";
			chromDisplay_0.unitY = "pA";
			chromDisplay_0.txtY = Lang.PS("信号", "Signal");
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
			chromDisplay_0.txtY = Lang.PS("信号", "Signal");
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
		try
		{
			if (File.Exists(chromatogram.chromInfo.cclCalibration))
			{
				chromatogram.caliGnl = CaliGnl.LoadFromFile(chromatogram.chromInfo.cclCalibration);
			}
		}
		catch
		{
		}
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
		if (Ch4Ctrl.selfCtrl != null)
		{
		}
		for (int k = 0; k < chromatogram.RltPeaks.Length; k++)
		{
			if (chromatogram.AdjustRltPeaksPara != null && k < chromatogram.AdjustRltPeaksPara.Length)
			{
				chromatogram.RltPeaks[k].name = chromatogram.AdjustRltPeaksPara[k].name;
			}
		}
		mstSetChromForm.Clear();
		mstSetChromForm.CurMtdMgr.chromInfo.LoadFromObject(chromatogram_1.chromInfo);
		mstSetChromForm.CurMtdMgr.chromInfoR.LoadFromObject(chromatogram_1.chromInfoR);
		if (chromatogram_1.userArchives.Length == 0)
		{
			Array.Resize(ref chromatogram_1.userArchives, 1);
			chromatogram_1.userArchives[0] = new UserArchive();
		}
		if (mstSetChromForm.CurMtdMgr.sigIntegrations.Count == 0)
		{
			Integration item = new Integration();
			mstSetChromForm.CurMtdMgr.sigIntegrations.Add(item);
		}
		mstSetChromForm.CurMtdMgr.sigIntegrations[0].LoadFromObject(chromatogram_1.userArchives[0].integ);
		mstSetChromForm.CurMtdMgr.strMtdShowName = chromatogram_1.chromInfoR.MtdFileName;
		mstSetChromForm.printParaOld = chromatogram_1.PPara;
		mstSetChromForm.CurMtdMgr.printPara = chromatogram_1.PPara;
		mstSetChromForm.devManager = chromatogram_1.devManager;
		mstSetChromForm.RefreshPara();
		if (!chromatogram.mtdSetup.IsNull)
		{
			mstSetChromForm.ReadFromMtdMgr(chromatogram.mtdSetup);
		}
		chromDataGrid.tcChrom_SelectedIndexChanged(null, null);
		float istdAmount = CurChrom.chromInfo.GetIstdAmount(0);
		bool flag = CurChrom.chromInfo.cclCalcu != CalcuStyle.ISTD || (CurChrom.chromInfo.cclCalcu == CalcuStyle.ISTD && CurChrom.IstdNum <= 1);
		mstSetChromForm.tbcuIstdAmount.Enabled = flag;
		mstSetChromForm.tbcuIstdAmount.Text = (flag ? istdAmount.ToString() : "列表行输入");
		if (chromatogram.SDAVER == "VER2.0")
		{
			mstSetChromForm.SetCaliGnl(chromatogram.caliGnl);
		}
		Class49.InsertIntoTable(Class49.string_9[2], Class49.user_0.u_name, "", "打开谱图", "打开谱图:" + CurChrom.fullName);
		chromDisplay_0.showProgTemp = false;
		dpgnlChrom.Refresh();
		if (frmParam.bEnNMHC)
		{
			chromDataGrid.InitFmPeak();
		}
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
		if (base.Parent != null)
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
			string asChrom = mstSetChromForm.CurMtdMgr.chromInfo.asChrom;
			userArchive.integ.LoadFromObject(CurChrom.integ);
			userArchive.SL_lbTexts(load: true, ref CurChrom.signal.lbTexts);
			userArchive.SL_lbLines(load: true, ref CurChrom.signal.lbLines);
			userArchive.userName = "";
			userArchive.chromInfo.LoadFromObject(CurChrom.chromInfo);
			userArchive.chromInfo.asChrom = asChrom;
			userArchive.chromInfo.asShowName = mstSetChromForm.CurMtdMgr.chromInfo.asShowName;
			CurChrom.AdjustRltPeaksPara = CurChrom.RltPeaks;
			CurChrom.mtdSetup = mstSetChromForm.CurMtdMgr;
			CurChrom.ChromPPara = mstSetChromForm.PrintMethod;
			CurChrom.AdjustRltPeaksPara = CurChrom.RltPeaks;
			CurChrom.chromInfoR.MtdFileName = mstSetChromForm.CurMtdMgr.strMtdShowName;
			userArchive.saveTime = DateTime.Now;
			CurChrom.SaveToFileOld(strFilePath);
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
		if (dpgnlChrom.Visible)
		{
			chromDataGrid.lbExpress.Top = splitContainer.Panel2.Height - chromDataGrid.lbExpress.Height;
		}
		else
		{
			chromDataGrid.lbExpress.Top = splitContainer.Panel2.Height - chromDataGrid.lbExpress.Height - 3;
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

	private void method_80(Graphics graphics_0, string string_289, float fX, float fY, float fWidth, StringAlignment stringAlignment_0)
	{
		rectangleF_1.X = fX;
		rectangleF_1.Y = fY;
		rectangleF_1.Width = fWidth;
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
		frmopenfile_0.ChromFrm = this;
		frmopenfile_0.Show();
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
		if (splitContainer2.Panel1Collapsed)
		{
			splitContainer2.SplitterDistance = 600;
			splitContainer2.Panel1Collapsed = false;
		}
		else
		{
			splitContainer2.Panel1Collapsed = true;
		}
	}

	private void btnSave_Click(object sender, EventArgs e)
	{
		if (Class49.user_0.ULevel != User.Level.访问员)
		{
			for (int i = 0; i < chromatogram_0.Length; i++)
			{
				tbsiSample_TextChanged(null, new KeyPressEventArgs(Convert.ToChar(13)));
				chromatogram_1 = chromatogram_0[i];
				SaveChromFile(chromatogram_0[i].fullName);
				Class49.InsertIntoTable(Class49.string_9[2], Class49.user_0.u_name, "", "保存谱图", "保存谱图:" + chromatogram_0[i].fullName);
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
					PointF pointF = new PointF
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
						goto IL_010b;
					}
				}
				byte_0++;
			}
			goto IL_0166;
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
		IL_0166:
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
			if (frmParam.bEnNMHC)
			{
				chromDataGrid.InitFmPeak();
			}
		}
		return;
		IL_010b:
		num = num2;
		if (num == -1)
		{
			EndManualDrawLine(bool_10: true);
			return;
		}
		EndManualDrawLine(bool_10: true);
		method_30();
		goto IL_0166;
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
		PointF pointF = chromDisplay_0.scrToLg(e.Location, bool_0: true);
		labmouseLgvalue.Text = "X:" + pointF.X + " Y:" + pointF.Y;
		chromDisplay_0.DrawMouseLgValue();
		DisDpRefresh();
		if (chromDisplay_0.curSignal != null)
		{
			for (int i = 0; i < chromDisplay_0.curSignal.peaks.Length; i++)
			{
				Peak peak = chromDisplay_0.curSignal.peaks[i];
				if (peak.disNo >= 0)
				{
					PointF pointF2 = chromDisplay_0.scrToLg(e.Location, bool_0: true);
					if ((Math.Abs(pointF2.X - peak.startT) < disLg_0.LgXEnd / 100f) & (Math.Abs(pointF2.Y - peak.startV) < disLg_0.LgYEnd / 100f))
					{
						chromDisplay_0.displayPanel.Cursor = Cursors.Hand;
						float_0 = pointF2.X;
						break;
					}
					if ((Math.Abs(pointF2.X - peak.endT) < disLg_0.LgXEnd / 100f) & (Math.Abs(pointF2.Y - peak.endV) < disLg_0.LgYEnd / 100f))
					{
						chromDisplay_0.displayPanel.Cursor = Cursors.Hand;
						float_0 = pointF2.X;
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

	private void chbTVOC_CheckedChanged(object sender, EventArgs e)
	{
		frmParam.bTVOC = chbTVOC.Checked;
		frmParam.SaveParam();
	}

	private void printDocument_0_BeginPrint(object sender, PrintEventArgs e)
	{
		try
		{
			font_1 = new System.Drawing.Font(System.Drawing.FontFamily.GenericSansSerif, 7f, FontStyle.Bold);
			if ((float)CurChrom.PPara.BPicFontSize == 0f)
			{
				CurChrom.PPara.BPicFontSize = 9;
			}
			font_1 = new System.Drawing.Font("宋体", CurChrom.PPara.BPicFontSize, FontStyle.Bold);
			font_0 = new System.Drawing.Font(System.Drawing.FontFamily.GenericSansSerif, 8f);
			font_0 = new System.Drawing.Font("宋体", 8f, FontStyle.Regular);
			psmgRight = 60;
			psmgLeft = 60;
			psmgBottom = 40;
			psmgTop = 40;
			rectangle_1.X = unitConvert(15);
			rectangle_1.Y = unitConvert(40);
			rectangle_1.Width = printDocument_0.DefaultPageSettings.Bounds.Width - unitConvert(15) - rectangle_1.Left;
			rectangle_1.Height = printDocument_0.DefaultPageSettings.Bounds.Height - unitConvert(40) - rectangle_1.Top;
			bool_8 = false;
			int_7 = unitConvert(7);
			int_10 = rectangle_1.Width / 4;
			float num = (float)rectangle_1.Width / 8f;
			int_11 = rectangle_1.Left + rectangle_1.Width / 2;
			int_9 = Convert.ToInt32(num);
			int_12 = Convert.ToInt32(num + num + num - 10f);
			bitmap_0 = (bitmap_1 = (bitmap_2 = null));
			rectangle_2.Size = UserAccountsDlg.defaultSignGraph;
			int_13 = 0;
			int_14 = 1;
			bool_9 = true;
			stringFormat_0.LineAlignment = StringAlignment.Center;
			bool_6 = true;
			gCmpdDisplay.LinkOptions(new Options());
			gCmpdDisplay.rcPage = printDocument_0.DefaultPageSettings.Bounds;
			gCmpdDisplay.dskRC.Size = sizeF_0;
			gCmpdDisplay.dskRC.X = (float)rectangle_1.Left + ((float)rectangle_1.Width - sizeF_0.Width) / 2f;
			int_4 = 0;
			iIndexCmpd = 0;
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message);
		}
	}

	private System.Data.DataTable method_72(Compound compound_0)
	{
		System.Data.DataTable dataTable = new System.Data.DataTable();
		dataTable.Columns.Add("响应");
		dataTable.Columns.Add("浓度");
		dataTable.Columns.Add("因子");
		dataTable.Columns.Add("相关系数");
		float fdata = 0f;
		if (chromatogram_0[0].chromInfo.cclCalcu == CalcuStyle.ESTD)
		{
			fdata = (float)compound_0.eFunc.corrFactor;
		}
		else if (chromatogram_0[0].chromInfo.cclCalcu == CalcuStyle.ISTD)
		{
			fdata = (float)compound_0.iFunc.corrFactor;
		}
		fdata = Class49.takeDecimal(fdata, Class49.int_8);
		for (int i = 0; i < compound_0.levels.Length; i++)
		{
			if (compound_0.levels[i].used)
			{
				dataTable.Rows.Add(compound_0.levels[i].response, compound_0.levels[i].amount, compound_0.levels[i].respFactor, fdata);
			}
		}
		return dataTable;
	}

	private bool method_73(PrintPageEventArgs printPageEventArgs_0, ref float fY)
	{
		float num = 80f;
		float num2 = fY;
		Peak[] peakAllCompound = CurChrom.GetPeakAllCompound();
		for (int i = int_4; i < peakAllCompound.Length; i++)
		{
			Compound compound = peakAllCompound[i].compound;
			if (compound == null)
			{
				int_4++;
				continue;
			}
			if (sizeF_0.Height + fY >= (float)rectangle_1.Height)
			{
				if (int_2 == 1)
				{
					chromDisplay_0.Draw(printPageEventArgs_0.Graphics, erase: false);
					chromDisplay_0.displayPanel = dpgnlChrom;
				}
				printPageEventArgs_0.HasMorePages = true;
				fY = psmgTop;
				int_2++;
				iIndexCmpd = 0;
				return false;
			}
			int_4++;
			System.Data.DataTable dataTable = method_72(compound);
			try
			{
				method_84(printPageEventArgs_0.Graphics, compound.cmpdInfo.name, bool_10: true, psmgLeft, fY, font_1);
			}
			catch
			{
			}
			fY += (float)int_7 * 2f;
			float fX;
			for (int j = 0; j < dataTable.Columns.Count; j++)
			{
				fX = 15f + num * (float)j;
				method_80(printPageEventArgs_0.Graphics, dataTable.Columns[j].ColumnName, fX, fY, num, StringAlignment.Far);
			}
			fX = 15f + num * (float)dataTable.Columns.Count;
			fY += int_7;
			printPageEventArgs_0.Graphics.DrawLine(pen_0, psmgLeft, fY, fX, fY);
			string string_ = "";
			gCmpdDisplay.method_15(compound, CurChrom.caliGnl.caliOption.caliDisMode == CaliDisMode.Istd, CurChrom.caliGnl.caliOption.cmpdUnit, ref string_);
			gCmpdDisplay.dskRC.X = fX + 100f;
			num2 = fY;
			gCmpdDisplay.dskRC.Y = num2;
			gCmpdDisplay.Draw(printPageEventArgs_0.Graphics, erase: false);
			fY += int_7;
			for (int k = 0; k < dataTable.Rows.Count; k++)
			{
				for (int l = 0; l < dataTable.Columns.Count; l++)
				{
					method_80(printPageEventArgs_0.Graphics, float.Parse(dataTable.Rows[k][l].ToString()).ToString("F" + Class49.int_8), 15f + num * (float)l, fY, num, StringAlignment.Far);
				}
				fY += int_7;
				if (k == dataTable.Rows.Count - 1)
				{
					printPageEventArgs_0.Graphics.DrawLine(pen_0, psmgLeft, fY, fX, fY);
					fY += int_7;
				}
			}
			try
			{
				method_84(printPageEventArgs_0.Graphics, Lang.PS("工作曲线:", " PeakFx:") + compound.eFunc.GetEquationStr(), bool_10: true, psmgLeft, fY, font_0);
			}
			catch
			{
			}
			try
			{
				fY += int_7;
				method_84(printPageEventArgs_0.Graphics, Lang.PS("校正时间:", "correction time:") + compound.cmpdInfo.BLString[0], bool_10: true, psmgLeft, fY, font_0);
			}
			catch
			{
			}
			fY += sizeF_0.Height + (float)(2 * int_7);
			iIndexCmpd++;
		}
		return true;
	}

	private System.Data.DataTable method_74()
	{
		float num = 0f;
		float num2 = 0f;
		System.Data.DataTable dataTable = new System.Data.DataTable();
		if (frmParam.bEnNMHC)
		{
			if (CurChrom.PPara.bRdata)
			{
				CH4Param cH4Param = CH4Param.Create();
				Peak[] peakAllCompound = CurChrom.GetPeakAllCompound();
				float num3 = 0f;
				float num4 = 0f;
				float num5 = 0f;
				float[] array;
				if (peakAllCompound.Length == 0)
				{
					array = new float[1] { 0f };
				}
				else
				{
					num3 = float.Parse(peakAllCompound[0].amount.ToString("F" + Class49.int_8));
					array = peakAllCompound[0].compound.eFunc.Calcu_amountF(CurChrom.mtdSetup.chromInfoR.UvwsStartT);
				}
				if (peakAllCompound.Length > 1)
				{
					num4 = float.Parse(peakAllCompound[1].amount.ToString("F" + Class49.int_8));
				}
				num5 = float.Parse(array[0].ToString("F" + Class49.int_8));
				int num6 = 3;
				int num7 = 0;
				bool flag = true;
				switch (Class49.sysLanguage_0)
				{
				case SysLanguage.CN:
					dataTable.Columns.Add("组份名称");
					break;
				case SysLanguage.EN:
					dataTable.Columns.Add("Name");
					break;
				}
				for (int i = 0; i < 6; i++)
				{
					dataTable.Rows.Add(i + 1);
				}
				dataTable.Rows[0][num7] = Lang.PS("总烃", "NMHC");
				dataTable.Rows[1][num7] = Lang.PS("甲烷", "CH4");
				dataTable.Rows[2][num7] = Lang.PS("氧气", "O2");
				dataTable.Rows[3][num7] = Lang.PS("总烃去氧", "THC");
				dataTable.Rows[4][num7] = Lang.PS("非甲烷总烃(以碳计)", "NMHC");
				dataTable.Rows[5][num7] = Lang.PS("非甲烷总烃(以甲烷计)", "NMHC");
				switch (Class49.sysLanguage_0)
				{
				case SysLanguage.CN:
					dataTable.Rows.Add("");
					break;
				case SysLanguage.EN:
					dataTable.Rows.Add("");
					break;
				}
				num7++;
				if (CurChrom.PPara.bPeakMaxTime)
				{
					switch (Class49.sysLanguage_0)
					{
					case SysLanguage.CN:
						dataTable.Columns.Add("保留时间(min)");
						break;
					case SysLanguage.EN:
						dataTable.Columns.Add("PeakRT");
						break;
					}
					if (peakAllCompound.Length == 1)
					{
						dataTable.Rows[0][num7] = peakAllCompound[0].pkRT.ToString("F" + Class49.int_8);
						dataTable.Rows[1][num7] = 0.ToString("F" + Class49.int_8);
					}
					else if (peakAllCompound.Length >= 2)
					{
						dataTable.Rows[0][num7] = peakAllCompound[0].pkRT.ToString("F" + Class49.int_8);
						dataTable.Rows[1][num7] = peakAllCompound[1].pkRT.ToString("F" + Class49.int_8);
					}
					else if (peakAllCompound.Length == 0)
					{
						dataTable.Rows[0][num7] = 0.ToString("F" + Class49.int_8);
						dataTable.Rows[1][num7] = 0.ToString("F" + Class49.int_8);
					}
					num7++;
				}
				bool flag2 = false;
				bool flag3 = false;
				if (CurChrom.PPara.bPeakheight)
				{
					switch (Class49.sysLanguage_0)
					{
					case SysLanguage.CN:
						dataTable.Columns.Add("峰高(pA)");
						break;
					case SysLanguage.EN:
						dataTable.Columns.Add("PeakHeight");
						break;
					}
					if (peakAllCompound.Length == 1)
					{
						dataTable.Rows[0][num7] = peakAllCompound[0].height.ToString("F" + Class49.int_8);
						dataTable.Rows[1][num7] = 0.ToString("F" + Class49.int_8);
					}
					else if (peakAllCompound.Length >= 2)
					{
						dataTable.Rows[0][num7] = peakAllCompound[0].height.ToString("F" + Class49.int_8);
						dataTable.Rows[1][num7] = peakAllCompound[1].height.ToString("F" + Class49.int_8);
					}
					else if (peakAllCompound.Length == 0)
					{
						dataTable.Rows[0][num7] = 0.ToString("F" + Class49.int_8);
						dataTable.Rows[1][num7] = 0.ToString("F" + Class49.int_8);
					}
					num7++;
				}
				if (CurChrom.PPara.bPeakArea)
				{
					switch (Class49.sysLanguage_0)
					{
					case SysLanguage.CN:
						dataTable.Columns.Add("峰面积(pA*S)");
						break;
					case SysLanguage.EN:
						dataTable.Columns.Add("PeakArea");
						break;
					}
					if (peakAllCompound.Length == 1)
					{
						dataTable.Rows[0][num7] = peakAllCompound[0].area.ToString("F" + Class49.int_8);
						dataTable.Rows[1][num7] = 0.ToString("F" + Class49.int_8);
					}
					else if (peakAllCompound.Length >= 2)
					{
						dataTable.Rows[0][num7] = peakAllCompound[0].area.ToString("F" + Class49.int_8);
						dataTable.Rows[1][num7] = peakAllCompound[1].area.ToString("F" + Class49.int_8);
					}
					else if (peakAllCompound.Length == 0)
					{
						dataTable.Rows[0][num7] = 0.ToString("F" + Class49.int_8);
						dataTable.Rows[1][num7] = 0.ToString("F" + Class49.int_8);
					}
					dataTable.Rows[2][num7] = CurChrom.mtdSetup.chromInfoR.UvwsStartT.ToString("F" + Class49.int_8);
					if (peakAllCompound.Length == 1)
					{
						dataTable.Rows[3][num7] = (peakAllCompound[0].area - CurChrom.mtdSetup.chromInfoR.UvwsStartT).ToString("F" + Class49.int_8);
					}
					num7++;
				}
				if (CurChrom.PPara.bPeakAmont)
				{
					switch (Class49.sysLanguage_0)
					{
					case SysLanguage.CN:
						dataTable.Columns.Add("浓度(" + CurChrom.chromInfo.prsUnitAfterScale + ")");
						break;
					case SysLanguage.EN:
						dataTable.Columns.Add("Amount");
						break;
					}
					if (peakAllCompound.Length == 1)
					{
						if (peakAllCompound[0].amount != -1f)
						{
							dataTable.Rows[0][num7] = peakAllCompound[0].amount.ToString("F" + Class49.int_8);
						}
						else
						{
							dataTable.Rows[0][num7] = "";
						}
						dataTable.Rows[1][num7] = 0.ToString("F" + Class49.int_8);
					}
					else if (peakAllCompound.Length >= 2)
					{
						if (peakAllCompound[0].amount != -1f)
						{
							dataTable.Rows[0][num7] = peakAllCompound[0].amount.ToString("F" + Class49.int_8);
						}
						else
						{
							dataTable.Rows[0][num7] = "";
						}
						if (peakAllCompound[1].amount != -1f)
						{
							dataTable.Rows[1][num7] = peakAllCompound[1].amount.ToString("F" + Class49.int_8);
						}
						else
						{
							dataTable.Rows[1][num7] = "";
						}
					}
					else if (peakAllCompound.Length == 0)
					{
						dataTable.Rows[0][num7] = 0.ToString("F" + Class49.int_8);
						dataTable.Rows[1][num7] = 0.ToString("F" + Class49.int_8);
					}
					dataTable.Rows[2][num7] = array[0].ToString("F" + Class49.int_8);
					dataTable.Rows[3][num7] = (num3 - array[0]).ToString("F" + Class49.int_8);
					float num8 = 0f;
					if (peakAllCompound.Length == 1)
					{
						num8 = num3 - num5;
					}
					else if (peakAllCompound.Length >= 2)
					{
						num8 = num3 - num4 - num5;
					}
					else if (peakAllCompound.Length == 0)
					{
						num8 = 0f;
					}
					if (num8 < 0f)
					{
						num8 = 0f;
					}
					dataTable.Rows[4][num7] = (num8 * 0.75f).ToString("F" + Class49.int_8);
					dataTable.Rows[5][num7] = num8.ToString("F" + Class49.int_8);
					num7++;
				}
			}
			return dataTable;
		}
		if (CurChrom.PPara.bRdata)
		{
			float num9 = 0f;
			float num10 = 0f;
			Peak[] rltPeaks = CurChrom.RltPeaks;
			int num11 = rltPeaks.Length;
			int num12 = 0;
			switch (Class49.sysLanguage_0)
			{
			case SysLanguage.CN:
				dataTable.Columns.Add("序号");
				break;
			case SysLanguage.EN:
				dataTable.Columns.Add("Index");
				break;
			}
			if (frmParam.bTVOC)
			{
				for (int j = 0; j < num11 + 1; j++)
				{
					dataTable.Rows.Add(j + 1);
				}
			}
			else
			{
				for (int k = 0; k < num11; k++)
				{
					dataTable.Rows.Add(k + 1);
				}
			}
			switch (Class49.sysLanguage_0)
			{
			case SysLanguage.CN:
				if (mstSetChromForm.CurMtdMgr.chromInfo.asMatching == ASMatchStyle.PeakDeduct)
				{
					dataTable.Rows.Add("谱峰扣除");
					if (File.Exists(mstSetChromForm.CurMtdMgr.chromInfo.asChrom))
					{
						Chromatogram chromatogram = Chromatogram.LoadFromFile2(mstSetChromForm.CurMtdMgr.chromInfo.asChrom, detectorStyle_0);
						chromatogram.mtdSetup = mstSetChromForm.CurMtdMgr.Copy();
						chromatogram.Process(InstruStyle.LC);
						Peak[] rltPeaks2 = chromatogram.RltPeaks;
						Peak[] peakFromCompound = chromatogram.GetPeakFromCompound();
						for (int l = 0; l < rltPeaks2.Count(); l++)
						{
							num9 += rltPeaks2[l].area;
						}
						for (int m = 0; m < peakFromCompound.Count(); m++)
						{
							if (peakFromCompound[m].amount > 0f)
							{
								num10 += peakFromCompound[m].amount;
							}
						}
					}
				}
				dataTable.Rows.Add("总计");
				break;
			case SysLanguage.EN:
				dataTable.Rows.Add("total");
				break;
			}
			num12++;
			if (CurChrom.PPara.bPeakName)
			{
				switch (Class49.sysLanguage_0)
				{
				case SysLanguage.CN:
					dataTable.Columns.Add("组份名称");
					break;
				case SysLanguage.EN:
					dataTable.Columns.Add("Name");
					break;
				}
				for (int n = 0; n < num11; n++)
				{
					dataTable.Rows[n][num12] = rltPeaks[n].name;
				}
				if (frmParam.bTVOC)
				{
					dataTable.Rows[num11][num12] = "以甲苯计算的和峰";
				}
				num12++;
			}
			if (CurChrom.PPara.bPeakMaxTime)
			{
				switch (Class49.sysLanguage_0)
				{
				case SysLanguage.CN:
					dataTable.Columns.Add("保留时间(min)");
					break;
				case SysLanguage.EN:
					dataTable.Columns.Add("PeakRT");
					break;
				}
				for (int num13 = 0; num13 < num11; num13++)
				{
					dataTable.Rows[num13][num12] = rltPeaks[num13].pkRT.ToString("F" + Class49.int_8);
				}
				num12++;
			}
			if (CurChrom.PPara.bPeakArea)
			{
				switch (Class49.sysLanguage_0)
				{
				case SysLanguage.CN:
					dataTable.Columns.Add("峰面积");
					break;
				case SysLanguage.EN:
					dataTable.Columns.Add("PeakArea");
					break;
				}
				for (int num14 = 0; num14 < num11; num14++)
				{
					dataTable.Rows[num14][num12] = rltPeaks[num14].area.ToString("F" + Class49.int_8);
					if (rltPeaks[num14].compound == null)
					{
						num += rltPeaks[num14].area;
					}
				}
				if (frmParam.bTVOC)
				{
					dataTable.Rows[num11][num12] = num.ToString("F" + Class49.int_8);
					dataTable.Rows[num11 + 1][num12] = CurChrom.whlArea.ToString("F" + Class49.int_8);
				}
				else if (mstSetChromForm.CurMtdMgr.chromInfo.asMatching == ASMatchStyle.PeakDeduct)
				{
					dataTable.Rows[num11][num12] = num9.ToString("F" + Class49.int_8);
					dataTable.Rows[num11 + 1][num12] = (CurChrom.whlArea - num9).ToString("F" + Class49.int_8);
				}
				else
				{
					dataTable.Rows[num11][num12] = CurChrom.whlArea.ToString("F" + Class49.int_8);
				}
				num12++;
			}
			if (CurChrom.PPara.bPeakheight)
			{
				switch (Class49.sysLanguage_0)
				{
				case SysLanguage.CN:
					dataTable.Columns.Add("峰高");
					break;
				case SysLanguage.EN:
					dataTable.Columns.Add("PeakHeight");
					break;
				}
				for (int num15 = 0; num15 < num11; num15++)
				{
					dataTable.Rows[num15][num12] = rltPeaks[num15].height.ToString("F" + Class49.int_8);
				}
				if (frmParam.bTVOC)
				{
					dataTable.Rows[num11 + 1][num12] = CurChrom.whlHeight.ToString("F" + Class49.int_8);
				}
				else if (mstSetChromForm.CurMtdMgr.chromInfo.asMatching == ASMatchStyle.PeakDeduct)
				{
					dataTable.Rows[num11 + 1][num12] = CurChrom.whlHeight.ToString("F" + Class49.int_8);
				}
				else
				{
					dataTable.Rows[num11][num12] = CurChrom.whlHeight.ToString("F" + Class49.int_8);
				}
				num12++;
			}
			if (CurChrom.PPara.bPeakHalfheight)
			{
				switch (Class49.sysLanguage_0)
				{
				case SysLanguage.CN:
					dataTable.Columns.Add("半峰宽");
					break;
				case SysLanguage.EN:
					dataTable.Columns.Add("HalfWidth");
					break;
				}
				for (int num16 = 0; num16 < num11; num16++)
				{
					dataTable.Rows[num16][num12] = rltPeaks[num16].WO5.ToString("F" + Class49.int_8);
				}
				num12++;
			}
			if (CurChrom.PPara.bPeakAmont)
			{
				switch (Class49.sysLanguage_0)
				{
				case SysLanguage.CN:
					dataTable.Columns.Add("浓度(" + CurChrom.AmountUnit + ")");
					break;
				case SysLanguage.EN:
					dataTable.Columns.Add("Amount");
					break;
				}
				for (int num17 = 0; num17 < num11; num17++)
				{
					if (rltPeaks[num17].amount != -1f)
					{
						dataTable.Rows[num17][num12] = rltPeaks[num17].amount.ToString("F" + Class49.int_8);
					}
					else
					{
						dataTable.Rows[num17][num12] = "";
					}
				}
				if (frmParam.bTVOC)
				{
					Compound compound = null;
					if (CurChrom.caliGnl != null && CurChrom.caliGnl.cmpds.Length != 0)
					{
						for (int num18 = 0; num18 < CurChrom.caliGnl.cmpds.Length; num18++)
						{
							if (CurChrom.caliGnl.cmpds[num18].cmpdInfo.name.Trim() == "甲苯")
							{
								compound = CurChrom.caliGnl.cmpds[num18];
							}
						}
					}
					if (compound == null)
					{
						compound = ((CurChrom.caliGnl == null || CurChrom.caliGnl.cmpds.Length == 0) ? new Compound() : CurChrom.caliGnl.cmpds[0]);
					}
					float[] array2 = compound.eFunc.Calcu_amountF(num);
					num2 = ((array2.Length != 0) ? array2[0] : 0f);
					dataTable.Rows[num11][num12] = num2.ToString("F" + Class49.int_8);
					dataTable.Rows[num11 + 1][num12] = (CurChrom.whlAmount + num2).ToString("F" + Class49.int_8);
					num12++;
					switch (Class49.sysLanguage_0)
					{
					case SysLanguage.CN:
						dataTable.Columns.Add("标准状态(mg/m³)");
						break;
					case SysLanguage.EN:
						dataTable.Columns.Add("Standard state content(mg/m³)");
						break;
					}
					double num19 = 0.0;
					for (int num20 = 0; num20 < num11; num20++)
					{
						if (rltPeaks[num20].amount != -1f)
						{
							if (rltPeaks[num20].amount < 0f)
							{
								rltPeaks[num20].amount = 0f;
							}
							dataTable.Rows[num20][num12] = (rltPeaks[num20].amount / frmParam.fInjectionVolume * (101.3f * (frmParam.fTemp + 273f) / (frmParam.fAtm * 273f))).ToString("F" + Class49.int_8);
							num19 += (double)(rltPeaks[num20].amount / frmParam.fInjectionVolume * (101.3f * (frmParam.fTemp + 273f) / (frmParam.fAtm * 273f)));
						}
						else
						{
							dataTable.Rows[num20][num12] = "0";
						}
					}
					dataTable.Rows[num11][num12] = (num2 / frmParam.fInjectionVolume * (101.3f * (frmParam.fTemp + 273f) / (frmParam.fAtm * 273f))).ToString("F" + Class49.int_8);
					num19 += (double)(num2 / frmParam.fInjectionVolume * (101.3f * (frmParam.fTemp + 273f) / (frmParam.fAtm * 237f)));
					dataTable.Rows[num11 + 1][num12] = num19.ToString("F" + Class49.int_8);
				}
				else if (mstSetChromForm.CurMtdMgr.chromInfo.asMatching == ASMatchStyle.PeakDeduct)
				{
					dataTable.Rows[num11][num12] = num10.ToString("F" + Class49.int_8);
					dataTable.Rows[num11 + 1][num12] = (CurChrom.whlAmount - num10).ToString("F" + Class49.int_8);
				}
				else
				{
					dataTable.Rows[num11][num12] = CurChrom.whlAmount.ToString("F" + Class49.int_8);
				}
				num12++;
			}
			bool flag4 = false;
			if (CurChrom.PPara.bPeakLPara)
			{
				dataTable.Columns.Add(Lang.PS("容量因子", "capacityFactor"));
				for (int num21 = 0; num21 < num11; num21++)
				{
					dataTable.Rows[num21][num12] = rltPeaks[num21].Capacity.ToString("F" + Class49.int_8);
				}
				num12++;
			}
			if (CurChrom.PPara.bPeakLV)
			{
				dataTable.Columns.Add(Lang.PS("峰分离度", "Resolution"));
				for (int num22 = 0; num22 < num11; num22++)
				{
					dataTable.Rows[num22][num12] = rltPeaks[num22].Resolution_EP.ToString("F" + Class49.int_8);
				}
				num12++;
			}
			if (CurChrom.PPara.bPeakOtherPara)
			{
				dataTable.Columns.Add(Lang.PS("相关系数", "Asymmetry"));
				for (int num23 = 0; num23 < num11; num23++)
				{
					if (CurChrom.chromInfo.cclCalcu == CalcuStyle.ESTD)
					{
						if (rltPeaks[num23].compound != null)
						{
							dataTable.Rows[num23][num12] = rltPeaks[num23].compound.eFunc.corrFactor.ToString("0.00000");
						}
						else
						{
							dataTable.Rows[num23][num12] = "";
						}
					}
					else if (CurChrom.chromInfo.cclCalcu == CalcuStyle.ISTD)
					{
						if (rltPeaks[num23].compound != null)
						{
							dataTable.Rows[num23][num12] = rltPeaks[num23].compound.iFunc.corrFactor.ToString("0.00000");
						}
						else
						{
							dataTable.Rows[num23][num12] = "";
						}
					}
				}
				num12++;
			}
			if (CurChrom.PPara.bPeakPara)
			{
				dataTable.Columns.Add(Lang.PS("校正因子", "freeRespFactor"));
				for (int num24 = 0; num24 < num11; num24++)
				{
					if (rltPeaks[num24].compound != null)
					{
						dataTable.Rows[num24][num12] = rltPeaks[num24].compound.cmpdInfo.freeRespFactor.ToString("F" + Class49.int_8);
					}
					else
					{
						dataTable.Rows[num24][num12] = "F" + Class49.int_8;
					}
				}
				num12++;
			}
			if (CurChrom.PPara.bPeaktailPara)
			{
				dataTable.Columns.Add(Lang.PS("拖尾因子", "SymmetryTailing"));
				for (int num25 = 0; num25 < num11; num25++)
				{
					dataTable.Rows[num25][num12] = rltPeaks[num25].SymmetryTailing.ToString("F" + Class49.int_8);
				}
				num12++;
			}
			if (CurChrom.PPara.bPeakTBPara)
			{
				dataTable.Columns.Add(Lang.PS("理论塔板数", "Efficiency"));
				for (int num26 = 0; num26 < num11; num26++)
				{
					dataTable.Rows[num26][num12] = ((int)rltPeaks[num26].Efficiency_EP).ToString();
				}
				num12++;
			}
			if (CurChrom.PPara.bPeakUTBPara)
			{
				dataTable.Columns.Add(Lang.PS("有效塔板数", "Eff_Column"));
				for (int num27 = 0; num27 < num11; num27++)
				{
					dataTable.Rows[num27][num12] = ((int)rltPeaks[num27].Eff_Column_EP).ToString();
				}
				num12++;
			}
			if (CurChrom.PPara.bPeakV)
			{
				dataTable.Columns.Add(Lang.PS("峰标志", "pkStyle"));
				for (int num28 = 0; num28 < num11; num28++)
				{
					dataTable.Rows[num28][num12] = rltPeaks[num28].pkStyle.ToString();
				}
				num12++;
			}
			if (CurChrom.PPara.bPeakheightPer)
			{
				dataTable.Columns.Add(Lang.PS("峰高百分比", "HeightPer"));
				for (int num29 = 0; num29 < num11; num29++)
				{
					dataTable.Rows[num29][num12] = (rltPeaks[num29].heightPer * 100f).ToString("F" + Class49.int_8) + "%";
				}
				if (CurChrom.whlHeightPer != -1f)
				{
					if (frmParam.bTVOC)
					{
						dataTable.Rows[num11 + 1][num12] = "100%";
					}
					else
					{
						dataTable.Rows[num11][num12] = "100%";
					}
				}
				num12++;
			}
			if (CurChrom.PPara.bPeakAreaPer)
			{
				dataTable.Columns.Add(Lang.PS("面积百分比", "AreaPer"));
				for (int num30 = 0; num30 < num11; num30++)
				{
					if (rltPeaks[num30].areaPer < 0f)
					{
						rltPeaks[num30].areaPer = 0f;
					}
					dataTable.Rows[num30][num12] = (rltPeaks[num30].areaPer * 100f).ToString("F" + Class49.int_8);
				}
				if (CurChrom.whlAreaPer != -1f)
				{
					if (frmParam.bTVOC)
					{
						dataTable.Rows[num11 + 1][num12] = "100";
					}
					else if (mstSetChromForm.CurMtdMgr.chromInfo.asMatching == ASMatchStyle.PeakDeduct)
					{
						dataTable.Rows[num11 + 1][num12] = "100";
					}
					else
					{
						dataTable.Rows[num11][num12] = "100";
					}
				}
				num12++;
			}
			if (CurChrom.PPara.bPeakAmontPer)
			{
				dataTable.Columns.Add(Lang.PS("浓度百分比", "AmountPer"));
				for (int num31 = 0; num31 < num11; num31++)
				{
					if (rltPeaks[num31].amountPer != -1f)
					{
						dataTable.Rows[num31][num12] = (rltPeaks[num31].amountPer * 100f).ToString("F" + Class49.int_8);
					}
					else
					{
						dataTable.Rows[num31][num12] = "";
					}
				}
				if (CurChrom.whlAmountPer != -1f)
				{
					if (frmParam.bTVOC)
					{
						dataTable.Rows[num11 + 1][num12] = CurChrom.whlAmountPer.ToString("F" + Class49.int_8);
					}
					else if (mstSetChromForm.CurMtdMgr.chromInfo.asMatching == ASMatchStyle.PeakDeduct)
					{
						dataTable.Rows[num11 + 1][num12] = CurChrom.whlAmountPer.ToString("F" + Class49.int_8);
					}
					else
					{
						dataTable.Rows[num11][num12] = CurChrom.whlAmountPer.ToString("F" + Class49.int_8);
					}
				}
				num12++;
			}
		}
		return dataTable;
	}

	private void printDocument_0_PrintPage(object sender, PrintPageEventArgs e)
	{
		Color disColor = chromDisplay_0.curSignal.disColor;
		chromDisplay_0.curSignal.disColor = Color.Black;
		Class49.bool_3 = true;
		frmParam.bTVOC = false;
		try
		{
			System.Data.DataTable dataTable = method_74();
			if (bool_6)
			{
				method_85(e.Graphics);
				bool_6 = false;
				int_0 = 0;
				int_1 = 0;
				int_2 = 1;
				bool_7 = false;
				int_3 = 0;
				string text = CurChrom.PPara.title;
				SizeF sizeF = e.Graphics.MeasureString(text, font_1);
				method_84(e.Graphics, text, bool_10: true, (float)int_11 - sizeF.Width / 2f, psmgTop, font_1);
				float_2 = psmgTop + int_7;
				if (CurChrom.PPara.bPTime)
				{
					switch (Class49.sysLanguage_0)
					{
					case SysLanguage.CN:
						text = "打印时间:" + DateTime.Now.ToString();
						break;
					case SysLanguage.EN:
						text = "printTime:" + DateTime.Now.ToString();
						break;
					}
					method_84(e.Graphics, text, bool_10: true, psmgLeft, float_2 + (float)int_7, font_0);
					float_2 += int_7;
				}
				if (CurChrom.PPara.bJtime)
				{
					switch (Class49.sysLanguage_0)
					{
					case SysLanguage.CN:
						text = "进样时间:" + chromatogram_1.injAnalysis.dtAcquire.ToString();
						break;
					case SysLanguage.EN:
						text = "Sampling time:" + chromatogram_1.injAnalysis.dtAcquire.ToString();
						break;
					}
					method_84(e.Graphics, text, bool_10: true, psmgLeft, float_2 + (float)int_7, font_0);
					float_2 += int_7;
				}
				if (CurChrom.PPara.bfname)
				{
					switch (Class49.sysLanguage_0)
					{
					case SysLanguage.CN:
						text = "打开的谱图文件:" + chromatogram_1.fullName.ToUpper();
						break;
					case SysLanguage.EN:
						text = "filePath:" + chromatogram_1.fullName;
						break;
					}
					SizeF sizeF2 = e.Graphics.MeasureString(text, font_0);
					if (sizeF2.Width > (float)rectangle_1.Width)
					{
						int num = int.Parse(Math.Ceiling(sizeF2.Width / (float)rectangle_1.Width).ToString());
						for (int i = 0; i < num; i++)
						{
							int num2 = 80;
							if (i != num - 1)
							{
								method_84(e.Graphics, text.Substring(i * num2, num2), bool_10: true, psmgLeft, float_2 + (float)int_7, font_0);
							}
							else
							{
								method_84(e.Graphics, text.Substring(i * num2), bool_10: true, psmgLeft, float_2 + (float)int_7, font_0);
							}
							float_2 += int_7;
						}
					}
					else
					{
						method_84(e.Graphics, text, bool_10: true, psmgLeft, float_2 + (float)int_7, font_0);
						float_2 += int_7;
					}
				}
				if (!frmParam.bTVOC)
				{
					text = CurChrom.PPara.PrintTitleTop;
					if (text == "" || text == null)
					{
						text = " ";
					}
					method_84(e.Graphics, text, bool_10: true, psmgLeft, float_2 + (float)int_7, font_0);
				}
				SizeF sizeF3 = e.Graphics.MeasureString(text, font_1);
				float_2 = float_2 + sizeF3.Height + (float)int_7;
				sizeF_1 = new SizeF(rectangle_1.Width, rectangle_1.Height / 3);
				chromDisplay_0.showFlowLine = false;
				chromDisplay_0.showGrdtBelt = false;
				chromDisplay_0.showProgTemp = false;
				chromDisplay_0.rcPage = rectangle_1;
				chromDisplay_0.dskRC.Size = sizeF_1;
				chromDisplay_0.dskRC.X = psmgLeft;
				chromDisplay_0.dskRC.Y = float_2 + (float)int_7;
				chromDisplay_0.displayPanel = null;
				float_2 += sizeF_1.Height + (float)(3 * int_7);
				float_2 = 569f;
				for (int j = 0; j < dataTable.Columns.Count; j++)
				{
					if (j < 7)
					{
						method_80(e.Graphics, dataTable.Columns[j].ColumnName, 15 + 100 * j, float_2, 100f, StringAlignment.Far);
					}
				}
				float_2 += int_7;
				pen_0.Color = Color.Black;
				pen_0.Width = 1f;
				e.Graphics.DrawLine(pen_0, psmgLeft, float_2, rectangle_1.Width - 10, float_2);
				float_2 += int_7;
			}
			for (int k = int_1; k < dataTable.Rows.Count; k++)
			{
				for (int l = 0; l < dataTable.Columns.Count; l++)
				{
					if (l < 7)
					{
						method_80(e.Graphics, dataTable.Rows[k][l].ToString(), 15 + 100 * l, float_2, 100f, StringAlignment.Far);
					}
				}
				float_2 += int_7;
				if (float_2 > (float)rectangle_1.Height)
				{
					if (int_2 == 1)
					{
						chromDisplay_0.Draw(e.Graphics, erase: false);
						chromDisplay_0.displayPanel = dpgnlChrom;
					}
					e.HasMorePages = true;
					int_1 = k + 1;
					float_2 = psmgTop;
					int_2++;
					return;
				}
				if (k == dataTable.Rows.Count - 2)
				{
					e.Graphics.DrawLine(pen_0, psmgLeft, float_2, rectangle_1.Width - 10, float_2);
					float_2 += int_7;
				}
			}
			int_1 = dataTable.Rows.Count + 1;
			if (CurChrom.PPara.bPeakFx && !method_73(e, ref float_2))
			{
				return;
			}
			int count = dataTable.Rows.Count;
			if (e.Graphics.MeasureString(CurChrom.PPara.PrintTitleBotom, font_1).Height + float_2 >= (float)rectangle_1.Height)
			{
				if (int_2 == 1)
				{
					chromDisplay_0.Draw(e.Graphics, erase: false);
					chromDisplay_0.displayPanel = dpgnlChrom;
				}
				e.HasMorePages = true;
				float_2 = psmgTop;
				int_2++;
				return;
			}
			method_84(e.Graphics, CurChrom.PPara.PrintTitleBotom, bool_10: true, psmgLeft, float_2 + (float)int_7 + 160f, font_0);
			if (int_2 == 1)
			{
				chromDisplay_0.Draw(e.Graphics, erase: false);
				chromDisplay_0.displayPanel = dpgnlChrom;
			}
			e.HasMorePages = false;
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message);
		}
		chromDisplay_0.curSignal.disColor = disColor;
		Class49.bool_3 = false;
	}

	private int unitConvert(int int_16)
	{
		return PrinterUnitConvert.Convert(int_16, PrinterUnit.Display, PrinterUnit.TenthsOfAMillimeter);
	}

	private void btnSaveDb_Click(object sender, EventArgs e)
	{
		Peak[] peakAllCompound = chromatogram_0[0].GetPeakAllCompound();
		if (peakAllCompound != null)
		{
			string[] array = new string[peakAllCompound.Length];
			string[] array2 = new string[peakAllCompound.Length];
			for (int i = 0; i < peakAllCompound.Length; i++)
			{
				array[i] = peakAllCompound[i].name;
				array2[i] = peakAllCompound[i].amount.ToString("F" + Class49.int_8);
			}
			Class49.updateIntoMineSqlLite2(0, strDetec, chromatogram_0[0].fullName, array, array2);
			if (FormCoalHistory.selfCtrl != null)
			{
				FormCoalHistory.selfCtrl.loadData();
			}
		}
	}

	private void btnOutputExcel_Click(object sender, EventArgs e)
	{
		peakToExcel();
	}

	public void peakToExcel()
	{
		bool flag = true;
		int num = 0;
		if (num < 1)
		{
			num = 1;
		}
		FileStream fileStream = new FileStream(System.Windows.Forms.Application.StartupPath + "\\peakToExcel.xls", FileMode.Open, FileAccess.Read);
		HSSFWorkbook hSSFWorkbook = new HSSFWorkbook(fileStream);
		ISheet sheetAt = hSSFWorkbook.GetSheetAt(0);
		sheetAt.ForceFormulaRecalculation = true;
		string text = System.Windows.Forms.Application.StartupPath + "\\峰信息.xls";
		FileStream fileStream2 = new FileStream(text, FileMode.Create);
		hSSFWorkbook.Write(fileStream2);
		fileStream.Close();
		fileStream2.Close();
		peakToExcel(text);
		if (File.Exists(text))
		{
			Process.Start(text);
		}
	}

	public bool peakToExcel(string Outpath)
	{
		bool result = false;
		IWorkbook workbook = null;
		FileStream fileStream = null;
		IRow row = null;
		IRow row2 = null;
		ISheet sheet = null;
		NPOI.SS.UserModel.ICell cell = null;
		NPOI.SS.UserModel.ICell cell2 = null;
		bool flag = false;
		double num = 0.0;
		FileStream fileStream2 = new FileStream(Outpath, FileMode.Open, FileAccess.ReadWrite);
		try
		{
			if (CurChrom != null)
			{
				PrintPara pPara = chromatogram_0[0].PPara;
				int setChromNo = chromatogram_0.Length;
				Bitmap bitmap = new Bitmap(864, 340);
				Graphics graphics = Graphics.FromImage(bitmap);
				if (CurChrom.PPara.bPicBound)
				{
					graphics.SmoothingMode = SmoothingMode.HighQuality;
					graphics.CompositingQuality = CompositingQuality.HighQuality;
					graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
					graphics.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
					graphics.DrawRectangle(new Pen(Color.Black, 2f), 0, 0, bitmap.Width - 15, bitmap.Height - 15);
				}
				RectangleF rectangleF = new RectangleF(0f, 0f, 849f, 325f);
				ChromDisplay chromDisplay = new ChromDisplay(WinStyle.Chromatogram, null);
				chromDisplay.LinkDisChroms(chromatogram_0, ref setChromNo);
				chromDisplay.showMouseLgValue = false;
				chromDisplay.showProgTemp = false;
				chromDisplay.ShowBgChrom = true;
				chromDisplay.setShowGrid = true;
				chromDisplay.showPeakArea = false;
				chromDisplay.rcPage = rectangleF;
				chromDisplay.dskRC = rectangleF;
				chromDisplay.FrmPen.Width = 1f;
				chromDisplay.DisPen.Width = 1f;
				System.Drawing.Font peakFont = chromDisplay.options.peakFont;
				chromDisplay.options.peakFont = new System.Drawing.Font(peakFont.FontFamily, peakFont.Size * 1f, peakFont.Style);
				chromDisplay.Draw(graphics, erase: true);
				bitmap.Save(System.Windows.Forms.Application.StartupPath + "\\a1.Emf");
				Peak[] rltPeaks = CurChrom.RltPeaks;
				workbook = new HSSFWorkbook(fileStream2);
				sheet = workbook.GetSheetAt(0);
				int num2 = rltPeaks.Length;
				int num3 = 6;
				byte[] pictureData = File.ReadAllBytes(System.Windows.Forms.Application.StartupPath + "\\a1.Emf");
				int pictureIndex = workbook.AddPicture(pictureData, NPOI.SS.UserModel.PictureType.EMF);
				IDrawing drawing = sheet.CreateDrawingPatriarch();
				HSSFClientAnchor anchor = new HSSFClientAnchor(0, 0, 1023, 255, 0, 5, 3, 5);
				IPicture picture = drawing.CreatePicture(anchor, pictureIndex);
				row2 = sheet.GetRow(0);
				cell2 = row2.GetCell(0);
				row2 = sheet.GetRow(3);
				cell2 = row2.GetCell(0);
				cell2.SetCellValue(DateTime.Now.ToString());
				row2 = sheet.GetRow(3);
				cell2 = row2.GetCell(2);
				cell2.SetCellValue("分析时间:" + chromatogram_1.injAnalysis.dtAcquire.ToString());
				row2 = sheet.GetRow(4);
				cell2 = row2.GetCell(0);
				cell2.SetCellValue("谱图名称:" + chromatogram_1.fullName);
				row = sheet.GetRow(5);
				for (int i = 7; i < num2 + 7; i++)
				{
					if (i > 7)
					{
						IRow row3 = sheet.GetRow(8);
						MyInsertRow(sheet, i + 1, 1, row3);
					}
					row = sheet.GetRow(i);
					if (row == null)
					{
						row = sheet.CreateRow(i);
					}
					for (int j = 0; j <= num3; j++)
					{
						cell = row.GetCell(j);
						if (cell == null)
						{
							cell = row.CreateCell(j);
						}
						switch (j)
						{
						case 0:
							cell.SetCellValue(rltPeaks[i - 7].name);
							break;
						case 1:
							cell.SetCellValue(rltPeaks[i - 7].pkRT);
							break;
						case 2:
							cell.SetCellValue(rltPeaks[i - 7].area);
							break;
						case 3:
							if (rltPeaks[i - 7].amount < 0f)
							{
								rltPeaks[i - 7].areaPer = 0f;
							}
							cell.SetCellValue(rltPeaks[i - 7].areaPer * 100f);
							break;
						case 4:
							if (rltPeaks[i - 7].amount < 0f)
							{
								rltPeaks[i - 7].amount = 0f;
							}
							cell.SetCellValue(rltPeaks[i - 7].amount);
							break;
						}
					}
				}
				using (fileStream = File.OpenWrite(Outpath))
				{
					workbook.Write(fileStream);
					result = true;
				}
				fileStream2.Close();
			}
			return result;
		}
		catch (Exception)
		{
			using (fileStream = File.OpenWrite(Outpath))
			{
				workbook.Write(fileStream);
				result = true;
			}
			fileStream.Close();
			return false;
		}
	}

	private void miFiPrint_Click(object sender, EventArgs e)
	{
		if (printDialog_0.ShowDialog() != DialogResult.OK || !HasChrom)
		{
			return;
		}
		if (!printDocument_0.PrinterSettings.IsValid)
		{
			switch (Class49.sysLanguage_0)
			{
			case SysLanguage.CN:
				MessageBox.Show("打印机无效！");
				break;
			case SysLanguage.EN:
				MessageBox.Show("Invalid printer!");
				break;
			}
			return;
		}
		try
		{
			printDocument_0.Print();
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message);
		}
	}

	private void 导出谱点文件ToolStripMenuItem_Click(object sender, EventArgs e)
	{
		if (!HasChrom || chromatogram_0.Length == 0)
		{
			return;
		}
		for (int i = 0; i < chromatogram_0.Length; i++)
		{
			int num = chromatogram_0[i].signal.dots.Length;
			string text = Path.GetDirectoryName(chromatogram_0[i].fullName) + "\\" + Path.GetFileNameWithoutExtension(chromatogram_0[i].fName) + ".txt";
			FileStream fileStream = new FileStream(text, FileMode.OpenOrCreate);
			StreamWriter streamWriter = new StreamWriter(fileStream, Encoding.Default);
			for (int j = 0; j < num; j++)
			{
				string text2 = chromatogram_0[i].signal.dots[j].X.ToString("0.000000");
				string text3 = chromatogram_0[i].signal.dots[j].Y.ToString("0.000000");
				streamWriter.Write(text2 + "," + text3 + "\r\n");
			}
			streamWriter.Close();
			fileStream.Close();
			Process.Start(text);
		}
	}

	public void printDirect()
	{
		try
		{
			if (HasChrom)
			{
				printDocument_0.Print();
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message);
		}
	}

	private void btnPrint_Click(object sender, EventArgs e)
	{
		try
		{
			if (HasChrom)
			{
				printDocument_0.Print();
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message);
		}
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
		if (CurChrom != null)
		{
			FormRltReminder formRltReminder = new FormRltReminder(CurChrom.RltPeaks, 0);
			formRltReminder.StartPosition = FormStartPosition.CenterScreen;
			formRltReminder.TopMost = true;
			formRltReminder.Show();
		}
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(IBrainChrom2018.ChromFormCtrl));
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
		this.导出谱点文件ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
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
		this.toolStrip1 = new System.Windows.Forms.ToolStrip();
		this.toolStripButton8 = new System.Windows.Forms.ToolStripButton();
		this.btnOpen = new System.Windows.Forms.ToolStripButton();
		this.btnOverlayMode = new System.Windows.Forms.ToolStripButton();
		this.btnSave = new System.Windows.Forms.ToolStripButton();
		this.btnClose = new System.Windows.Forms.ToolStripButton();
		this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
		this.toolStripButton6 = new System.Windows.Forms.ToolStripButton();
		this.btnPrint = new System.Windows.Forms.ToolStripButton();
		this.btnPreview = new System.Windows.Forms.ToolStripButton();
		this.btnPreviousZoom = new System.Windows.Forms.ToolStripButton();
		this.btnNextZoom = new System.Windows.Forms.ToolStripButton();
		this.btnUnzoom = new System.Windows.Forms.ToolStripButton();
		this.btnOutputExcel = new System.Windows.Forms.ToolStripButton();
		this.toolStrip2 = new System.Windows.Forms.ToolStrip();
		this.btnipPkAddPosi = new System.Windows.Forms.ToolStripButton();
		this.btnipPkCut = new System.Windows.Forms.ToolStripButton();
		this.btnbsBsTailTgnt = new System.Windows.Forms.ToolStripButton();
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
		this.toolStripSeparator31 = new System.Windows.Forms.ToolStripSeparator();
		this.btngblDtecDelay = new System.Windows.Forms.ToolStripButton();
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
		this.cChangePeak = new System.Windows.Forms.CheckBox();
		this.imageList_1 = new System.Windows.Forms.ImageList(this.components);
		this.splitContainer1 = new System.Windows.Forms.SplitContainer();
		this.splitContainer4 = new System.Windows.Forms.SplitContainer();
		this.labmouseLgvalue = new System.Windows.Forms.Label();
		this.btnSaveDb = new System.Windows.Forms.Button();
		this.chbTVOC = new System.Windows.Forms.CheckBox();
		this.btnUploading = new System.Windows.Forms.Button();
		this.btnConvert = new System.Windows.Forms.Button();
		this.btnGnl2 = new System.Windows.Forms.Button();
		this.btnGnl = new System.Windows.Forms.Button();
		this.cbNMHC = new System.Windows.Forms.CheckBox();
		this.btUpdataByXX = new System.Windows.Forms.Button();
		this.bExportXY = new System.Windows.Forms.Button();
		this.btnTimeFull = new System.Windows.Forms.Button();
		this.btnXYFull = new System.Windows.Forms.Button();
		this.mtbTime = new System.Windows.Forms.MaskedTextBox();
		this.label32 = new System.Windows.Forms.Label();
		this.mtbSigYEnd = new System.Windows.Forms.MaskedTextBox();
		this.label31 = new System.Windows.Forms.Label();
		this.mtbSigYBeg = new System.Windows.Forms.MaskedTextBox();
		this.label29 = new System.Windows.Forms.Label();
		this.label119 = new System.Windows.Forms.Label();
		this.label1 = new System.Windows.Forms.Label();
		this.splitContainer = new IBrainChrom2018.LclSplitContainer();
		this.dpgnlChrom = new IBrainChrom2018.LclDisplayPanel();
		this.chromDataGrid = new IBrainChrom2018.ChromFormDataGrid();
		this.mstSetChromForm = new IBrainChrom2018.MstSet();
		this.splitContainer2 = new System.Windows.Forms.SplitContainer();
		this.chromFormFileSearchCtrl1 = new IBrainChrom2018.ChromFormFileSearchCtrl();
		this.folderBrowserDialog_0 = new System.Windows.Forms.FolderBrowserDialog();
		this.printDialog_0 = new System.Windows.Forms.PrintDialog();
		this.printDocument_0 = new System.Drawing.Printing.PrintDocument();
		this.prtPrvDlg = new System.Windows.Forms.PrintPreviewDialog();
		this.msChrom.SuspendLayout();
		this.flpChrom.SuspendLayout();
		this.toolStrip1.SuspendLayout();
		this.toolStrip2.SuspendLayout();
		this.tsDatAcq.SuspendLayout();
		this.ssChrom.SuspendLayout();
		this.cmsLibs.SuspendLayout();
		this.cmsSSTCmpds.SuspendLayout();
		this.cmsSlices.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.dataGridView2).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.dataGridView3).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.splitContainer1).BeginInit();
		this.splitContainer1.Panel1.SuspendLayout();
		this.splitContainer1.Panel2.SuspendLayout();
		this.splitContainer1.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.splitContainer4).BeginInit();
		this.splitContainer4.Panel1.SuspendLayout();
		this.splitContainer4.Panel2.SuspendLayout();
		this.splitContainer4.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.splitContainer).BeginInit();
		this.splitContainer.Panel1.SuspendLayout();
		this.splitContainer.Panel2.SuspendLayout();
		this.splitContainer.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.splitContainer2).BeginInit();
		this.splitContainer2.Panel1.SuspendLayout();
		this.splitContainer2.Panel2.SuspendLayout();
		this.splitContainer2.SuspendLayout();
		base.SuspendLayout();
		this.msChrom.ImageScalingSize = new System.Drawing.Size(20, 20);
		this.msChrom.Items.AddRange(new System.Windows.Forms.ToolStripItem[4] { this.miFile, this.miDisplay, this.miChromatogram, this.miMethod });
		this.msChrom.Location = new System.Drawing.Point(0, 0);
		this.msChrom.Name = "msChrom";
		this.msChrom.Size = new System.Drawing.Size(1388, 25);
		this.msChrom.TabIndex = 0;
		this.msChrom.Text = "menuStrip1";
		this.miFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[14]
		{
			this.miFiOverlayMode, this.miFiOpen, this.miFiClose, this.miFiCloseAll, this.miFiSave, this.miFiSaveAs, this.toolStripSeparator2, this.toolStripMenuItem2, this.toolStripMenuItem1, this.toolStripSeparator6,
			this.miFiPreview, this.miFiPrint, this.导出谱点文件ToolStripMenuItem, this.miFiExit
		});
		this.miFile.Name = "miFile";
		this.miFile.Size = new System.Drawing.Size(60, 21);
		this.miFile.Text = "  文件  ";
		this.miFiOverlayMode.Name = "miFiOverlayMode";
		this.miFiOverlayMode.Size = new System.Drawing.Size(152, 22);
		this.miFiOverlayMode.Text = "重叠模式";
		this.miFiOverlayMode.Click += new System.EventHandler(btnOverlayMode_Click);
		this.miFiOpen.Name = "miFiOpen";
		this.miFiOpen.Size = new System.Drawing.Size(152, 22);
		this.miFiOpen.Text = "打开&O";
		this.miFiOpen.Click += new System.EventHandler(btnOpen_Click);
		this.miFiClose.Name = "miFiClose";
		this.miFiClose.Size = new System.Drawing.Size(152, 22);
		this.miFiClose.Text = "关闭&C";
		this.miFiClose.Click += new System.EventHandler(btnClose_Click);
		this.miFiCloseAll.Name = "miFiCloseAll";
		this.miFiCloseAll.Size = new System.Drawing.Size(152, 22);
		this.miFiCloseAll.Text = "关闭所有";
		this.miFiCloseAll.Click += new System.EventHandler(miFiCloseAll_Click);
		this.miFiSave.Name = "miFiSave";
		this.miFiSave.Size = new System.Drawing.Size(152, 22);
		this.miFiSave.Text = "保存&S";
		this.miFiSave.Click += new System.EventHandler(btnSave_Click);
		this.miFiSaveAs.Name = "miFiSaveAs";
		this.miFiSaveAs.Size = new System.Drawing.Size(152, 22);
		this.miFiSaveAs.Text = "另存为...";
		this.miFiSaveAs.Click += new System.EventHandler(miFiSaveAs_Click);
		this.toolStripSeparator2.Name = "toolStripSeparator2";
		this.toolStripSeparator2.Size = new System.Drawing.Size(149, 6);
		this.toolStripMenuItem2.Name = "toolStripMenuItem2";
		this.toolStripMenuItem2.Size = new System.Drawing.Size(152, 22);
		this.toolStripMenuItem2.Text = "编辑组份表...";
		this.toolStripMenuItem2.Click += new System.EventHandler(toolStripMenuItem2_Click);
		this.toolStripMenuItem1.Name = "toolStripMenuItem1";
		this.toolStripMenuItem1.Size = new System.Drawing.Size(152, 22);
		this.toolStripMenuItem1.Text = "另存为模板...";
		this.toolStripMenuItem1.Visible = false;
		this.toolStripMenuItem1.Click += new System.EventHandler(toolStripMenuItem1_Click);
		this.toolStripSeparator6.Name = "toolStripSeparator6";
		this.toolStripSeparator6.Size = new System.Drawing.Size(149, 6);
		this.miFiPreview.Name = "miFiPreview";
		this.miFiPreview.Size = new System.Drawing.Size(152, 22);
		this.miFiPreview.Text = "打印预览&P";
		this.miFiPreview.Click += new System.EventHandler(btnPreview_Click);
		this.miFiPrint.Name = "miFiPrint";
		this.miFiPrint.Size = new System.Drawing.Size(152, 22);
		this.miFiPrint.Text = "打印";
		this.miFiPrint.Click += new System.EventHandler(miFiPrint_Click);
		this.导出谱点文件ToolStripMenuItem.Name = "导出谱点文件ToolStripMenuItem";
		this.导出谱点文件ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
		this.导出谱点文件ToolStripMenuItem.Text = "导出谱点文件";
		this.导出谱点文件ToolStripMenuItem.Click += new System.EventHandler(导出谱点文件ToolStripMenuItem_Click);
		this.miFiExit.Name = "miFiExit";
		this.miFiExit.Size = new System.Drawing.Size(152, 22);
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
		this.toolStripSeparator4.Name = "toolStripSeparator4";
		this.toolStripSeparator4.Size = new System.Drawing.Size(6, 39);
		this.flpChrom.AutoSize = true;
		this.flpChrom.Controls.Add(this.toolStrip1);
		this.flpChrom.Controls.Add(this.toolStrip2);
		this.flpChrom.Dock = System.Windows.Forms.DockStyle.Top;
		this.flpChrom.Location = new System.Drawing.Point(0, 25);
		this.flpChrom.Margin = new System.Windows.Forms.Padding(0);
		this.flpChrom.Name = "flpChrom";
		this.flpChrom.Size = new System.Drawing.Size(1388, 39);
		this.flpChrom.TabIndex = 6;
		this.toolStrip1.Dock = System.Windows.Forms.DockStyle.None;
		this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
		this.toolStrip1.ImageScalingSize = new System.Drawing.Size(32, 32);
		this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[16]
		{
			this.toolStripButton8, this.btnOpen, this.btnOverlayMode, this.btnSave, this.btnClose, this.toolStripSeparator7, this.toolStripButton6, this.toolStripSeparator1, this.btnPrint, this.btnPreview,
			this.toolStripSeparator3, this.btnPreviousZoom, this.btnNextZoom, this.btnUnzoom, this.toolStripSeparator4, this.btnOutputExcel
		});
		this.toolStrip1.Location = new System.Drawing.Point(0, 0);
		this.toolStrip1.Name = "toolStrip1";
		this.toolStrip1.Size = new System.Drawing.Size(314, 39);
		this.toolStrip1.TabIndex = 4;
		this.toolStrip1.Text = "toolStrip1";
		this.toolStripButton8.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
		this.toolStripButton8.Image = (System.Drawing.Image)resources.GetObject("toolStripButton8.Image");
		this.toolStripButton8.ImageTransparentColor = System.Drawing.Color.Magenta;
		this.toolStripButton8.Name = "toolStripButton8";
		this.toolStripButton8.Size = new System.Drawing.Size(36, 36);
		this.toolStripButton8.Text = "toolStripButton8";
		this.toolStripButton8.ToolTipText = "查找打开";
		this.toolStripButton8.Visible = false;
		this.toolStripButton8.Click += new System.EventHandler(toolStripButton8_Click);
		this.btnOpen.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
		this.btnOpen.Image = (System.Drawing.Image)resources.GetObject("btnOpen.Image");
		this.btnOpen.ImageTransparentColor = System.Drawing.Color.Magenta;
		this.btnOpen.Margin = new System.Windows.Forms.Padding(5, 1, 0, 2);
		this.btnOpen.Name = "btnOpen";
		this.btnOpen.Size = new System.Drawing.Size(36, 36);
		this.btnOpen.Text = "toolStripButton1";
		this.btnOpen.ToolTipText = "打开谱图文件";
		this.btnOpen.Click += new System.EventHandler(btnOpen_Click);
		this.btnOverlayMode.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
		this.btnOverlayMode.Image = (System.Drawing.Image)resources.GetObject("btnOverlayMode.Image");
		this.btnOverlayMode.ImageTransparentColor = System.Drawing.Color.Magenta;
		this.btnOverlayMode.Name = "btnOverlayMode";
		this.btnOverlayMode.Size = new System.Drawing.Size(36, 36);
		this.btnOverlayMode.Text = "toolStripButton12";
		this.btnOverlayMode.ToolTipText = "谱图叠加打开";
		this.btnOverlayMode.Click += new System.EventHandler(btnOverlayMode_Click);
		this.btnSave.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
		this.btnSave.Image = (System.Drawing.Image)resources.GetObject("btnSave.Image");
		this.btnSave.ImageTransparentColor = System.Drawing.Color.Magenta;
		this.btnSave.Name = "btnSave";
		this.btnSave.Size = new System.Drawing.Size(36, 36);
		this.btnSave.Text = "toolStripButton2";
		this.btnSave.ToolTipText = "保存当前谱图文件";
		this.btnSave.Click += new System.EventHandler(btnSave_Click);
		this.btnClose.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
		this.btnClose.Image = (System.Drawing.Image)resources.GetObject("btnClose.Image");
		this.btnClose.ImageTransparentColor = System.Drawing.Color.Magenta;
		this.btnClose.Name = "btnClose";
		this.btnClose.Size = new System.Drawing.Size(36, 36);
		this.btnClose.Text = "toolStripButton3";
		this.btnClose.ToolTipText = "关闭";
		this.btnClose.Click += new System.EventHandler(btnClose_Click);
		this.toolStripSeparator7.Name = "toolStripSeparator7";
		this.toolStripSeparator7.Size = new System.Drawing.Size(6, 39);
		this.toolStripButton6.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
		this.toolStripButton6.Image = (System.Drawing.Image)resources.GetObject("toolStripButton6.Image");
		this.toolStripButton6.ImageTransparentColor = System.Drawing.Color.Magenta;
		this.toolStripButton6.Name = "toolStripButton6";
		this.toolStripButton6.Size = new System.Drawing.Size(36, 36);
		this.toolStripButton6.Text = "toolStripButton6";
		this.toolStripButton6.Click += new System.EventHandler(toolStripButton6_Click);
		this.btnPrint.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
		this.btnPrint.Image = IBrainChrom2018.Properties.Resources.print_80px;
		this.btnPrint.ImageTransparentColor = System.Drawing.Color.Magenta;
		this.btnPrint.Name = "btnPrint";
		this.btnPrint.Size = new System.Drawing.Size(36, 36);
		this.btnPrint.Text = "打印";
		this.btnPrint.Click += new System.EventHandler(btnPrint_Click);
		this.btnPreview.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
		this.btnPreview.Image = (System.Drawing.Image)resources.GetObject("btnPreview.Image");
		this.btnPreview.ImageTransparentColor = System.Drawing.Color.Magenta;
		this.btnPreview.Name = "btnPreview";
		this.btnPreview.Size = new System.Drawing.Size(36, 36);
		this.btnPreview.Text = "toolStripButton5";
		this.btnPreview.ToolTipText = "打印预览";
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
		this.btnOutputExcel.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
		this.btnOutputExcel.Image = (System.Drawing.Image)resources.GetObject("btnOutputExcel.Image");
		this.btnOutputExcel.ImageTransparentColor = System.Drawing.Color.Magenta;
		this.btnOutputExcel.Name = "btnOutputExcel";
		this.btnOutputExcel.Size = new System.Drawing.Size(36, 36);
		this.btnOutputExcel.Text = "toolStripButton7";
		this.btnOutputExcel.ToolTipText = "导出Excel";
		this.btnOutputExcel.Click += new System.EventHandler(btnOutputExcel_Click);
		this.toolStrip2.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
		this.toolStrip2.ImageScalingSize = new System.Drawing.Size(32, 32);
		this.toolStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[34]
		{
			this.btnipPkAddPosi, this.btnipPkCut, this.btnbsBsTailTgnt, this.btnExpress, this.toolStripSeparator20, this.btngblPeakWidth, this.btngblThreshold, this.btngblPkSlope, this.toolStripSeparator21, this.btnipResetDtecNeg,
			this.btnipClampNeg, this.toolStripSeparator5, this.btnipPkWidth, this.btnipPkThreshold, this.btnipPkAddNeg, this.btnipPkHalfWidth, this.btnipPkArea, this.toolStripSeparator23, this.btnipPkVale, this.toolStripSeparator28,
			this.btnipSolventPeak, this.btnipFlowMarker, this.btnipGroups, this.toolStripSeparator22, this.btnbsBsTgnt, this.btnbsBsVtV, this.toolStripSeparator14, this.btnbsBsValley, this.btnbsBsTogether, this.btnbsBsForwHorz,
			this.btnbsBsBackHorz, this.btnbsBsFrontTgnt, this.toolStripSeparator31, this.btngblDtecDelay
		});
		this.toolStrip2.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
		this.toolStrip2.Location = new System.Drawing.Point(314, 0);
		this.toolStrip2.Name = "toolStrip2";
		this.toolStrip2.Size = new System.Drawing.Size(914, 39);
		this.toolStrip2.TabIndex = 6;
		this.toolStrip2.Text = "toolStrip2";
		this.btnipPkAddPosi.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
		this.btnipPkAddPosi.Image = (System.Drawing.Image)resources.GetObject("btnipPkAddPosi.Image");
		this.btnipPkAddPosi.ImageTransparentColor = System.Drawing.Color.Magenta;
		this.btnipPkAddPosi.Name = "btnipPkAddPosi";
		this.btnipPkAddPosi.Size = new System.Drawing.Size(36, 36);
		this.btnipPkAddPosi.Text = "toolStripButton38";
		this.btnipPkAddPosi.ToolTipText = "添加正峰";
		this.btnipPkAddPosi.Click += new System.EventHandler(btngblDtecDelay_Click);
		this.btnipPkCut.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
		this.btnipPkCut.Image = (System.Drawing.Image)resources.GetObject("btnipPkCut.Image");
		this.btnipPkCut.ImageTransparentColor = System.Drawing.Color.Magenta;
		this.btnipPkCut.Name = "btnipPkCut";
		this.btnipPkCut.Size = new System.Drawing.Size(36, 36);
		this.btnipPkCut.Text = "toolStripButton51";
		this.btnipPkCut.ToolTipText = "删除峰";
		this.btnipPkCut.Click += new System.EventHandler(btngblDtecDelay_Click);
		this.btnbsBsTailTgnt.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
		this.btnbsBsTailTgnt.Image = (System.Drawing.Image)resources.GetObject("btnbsBsTailTgnt.Image");
		this.btnbsBsTailTgnt.ImageTransparentColor = System.Drawing.Color.Magenta;
		this.btnbsBsTailTgnt.Name = "btnbsBsTailTgnt";
		this.btnbsBsTailTgnt.Size = new System.Drawing.Size(36, 36);
		this.btnbsBsTailTgnt.Text = "toolStripButton49";
		this.btnbsBsTailTgnt.ToolTipText = "添加尾切峰";
		this.btnbsBsTailTgnt.Click += new System.EventHandler(btngblDtecDelay_Click);
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
		this.btnbsBsForwHorz.ToolTipText = "手动积分";
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
		this.tsDatAcq.Location = new System.Drawing.Point(1292, 0);
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
		this.ssChrom.Location = new System.Drawing.Point(0, 788);
		this.ssChrom.Name = "ssChrom";
		this.ssChrom.Size = new System.Drawing.Size(1388, 22);
		this.ssChrom.TabIndex = 7;
		this.ssChrom.Text = "statusStrip1";
		this.slbExplain.Name = "slbExplain";
		this.slbExplain.Size = new System.Drawing.Size(80, 17);
		this.slbExplain.Text = "谱图处理窗口";
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
		this.cChangePeak.AutoSize = true;
		this.cChangePeak.Location = new System.Drawing.Point(589, 6);
		this.cChangePeak.Name = "cChangePeak";
		this.cChangePeak.Size = new System.Drawing.Size(60, 16);
		this.cChangePeak.TabIndex = 14;
		this.cChangePeak.Text = "调整峰";
		this.cChangePeak.UseVisualStyleBackColor = true;
		this.cChangePeak.Visible = false;
		this.cChangePeak.KeyPress += new System.Windows.Forms.KeyPressEventHandler(cChangePeak_KeyPress);
		this.imageList_1.ImageStream = (System.Windows.Forms.ImageListStreamer)resources.GetObject("imageList_1.ImageStream");
		this.imageList_1.TransparentColor = System.Drawing.Color.Transparent;
		this.imageList_1.Images.SetKeyName(0, "02.png");
		this.imageList_1.Images.SetKeyName(1, "在线帮助选中.png");
		this.imageList_1.Images.SetKeyName(2, "01.png");
		this.imageList_1.Images.SetKeyName(3, "重叠打开选中.png");
		this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
		this.splitContainer1.Location = new System.Drawing.Point(0, 0);
		this.splitContainer1.Name = "splitContainer1";
		this.splitContainer1.Panel1.Controls.Add(this.splitContainer4);
		this.splitContainer1.Panel2.Controls.Add(this.mstSetChromForm);
		this.splitContainer1.Panel2MinSize = 0;
		this.splitContainer1.Size = new System.Drawing.Size(1179, 724);
		this.splitContainer1.SplitterDistance = 864;
		this.splitContainer1.TabIndex = 15;
		this.splitContainer4.Dock = System.Windows.Forms.DockStyle.Fill;
		this.splitContainer4.Location = new System.Drawing.Point(0, 0);
		this.splitContainer4.Name = "splitContainer4";
		this.splitContainer4.Orientation = System.Windows.Forms.Orientation.Horizontal;
		this.splitContainer4.Panel1.Controls.Add(this.labmouseLgvalue);
		this.splitContainer4.Panel1.Controls.Add(this.btnSaveDb);
		this.splitContainer4.Panel1.Controls.Add(this.chbTVOC);
		this.splitContainer4.Panel1.Controls.Add(this.btnUploading);
		this.splitContainer4.Panel1.Controls.Add(this.btnConvert);
		this.splitContainer4.Panel1.Controls.Add(this.btnGnl2);
		this.splitContainer4.Panel1.Controls.Add(this.btnGnl);
		this.splitContainer4.Panel1.Controls.Add(this.cbNMHC);
		this.splitContainer4.Panel1.Controls.Add(this.btUpdataByXX);
		this.splitContainer4.Panel1.Controls.Add(this.bExportXY);
		this.splitContainer4.Panel1.Controls.Add(this.btnTimeFull);
		this.splitContainer4.Panel1.Controls.Add(this.btnXYFull);
		this.splitContainer4.Panel1.Controls.Add(this.mtbTime);
		this.splitContainer4.Panel1.Controls.Add(this.label32);
		this.splitContainer4.Panel1.Controls.Add(this.mtbSigYEnd);
		this.splitContainer4.Panel1.Controls.Add(this.label31);
		this.splitContainer4.Panel1.Controls.Add(this.mtbSigYBeg);
		this.splitContainer4.Panel1.Controls.Add(this.label29);
		this.splitContainer4.Panel1.Controls.Add(this.label119);
		this.splitContainer4.Panel1.Controls.Add(this.label1);
		this.splitContainer4.Panel2.Controls.Add(this.splitContainer);
		this.splitContainer4.Size = new System.Drawing.Size(864, 724);
		this.splitContainer4.SplitterDistance = 26;
		this.splitContainer4.TabIndex = 14;
		this.labmouseLgvalue.AutoSize = true;
		this.labmouseLgvalue.Location = new System.Drawing.Point(453, 8);
		this.labmouseLgvalue.Name = "labmouseLgvalue";
		this.labmouseLgvalue.Size = new System.Drawing.Size(35, 12);
		this.labmouseLgvalue.TabIndex = 27;
		this.labmouseLgvalue.Text = "X: Y:";
		this.btnSaveDb.BackColor = System.Drawing.Color.Lime;
		this.btnSaveDb.Location = new System.Drawing.Point(683, 0);
		this.btnSaveDb.Name = "btnSaveDb";
		this.btnSaveDb.Size = new System.Drawing.Size(95, 23);
		this.btnSaveDb.TabIndex = 26;
		this.btnSaveDb.Text = "更新数据库";
		this.btnSaveDb.UseVisualStyleBackColor = false;
		this.btnSaveDb.Visible = false;
		this.btnSaveDb.Click += new System.EventHandler(btnSaveDb_Click);
		this.chbTVOC.AutoSize = true;
		this.chbTVOC.Location = new System.Drawing.Point(756, 5);
		this.chbTVOC.Name = "chbTVOC";
		this.chbTVOC.Size = new System.Drawing.Size(48, 16);
		this.chbTVOC.TabIndex = 25;
		this.chbTVOC.Text = "TVOC";
		this.chbTVOC.UseVisualStyleBackColor = true;
		this.chbTVOC.CheckedChanged += new System.EventHandler(chbTVOC_CheckedChanged);
		this.btnUploading.Location = new System.Drawing.Point(525, 0);
		this.btnUploading.Name = "btnUploading";
		this.btnUploading.Size = new System.Drawing.Size(75, 23);
		this.btnUploading.TabIndex = 24;
		this.btnUploading.Text = "上传";
		this.btnUploading.UseVisualStyleBackColor = true;
		this.btnUploading.Click += new System.EventHandler(btnUploading_Click);
		this.btnConvert.Location = new System.Drawing.Point(606, 0);
		this.btnConvert.Name = "btnConvert";
		this.btnConvert.Size = new System.Drawing.Size(83, 23);
		this.btnConvert.TabIndex = 23;
		this.btnConvert.Text = "单位转换";
		this.btnConvert.UseVisualStyleBackColor = true;
		this.btnConvert.Visible = false;
		this.btnConvert.Click += new System.EventHandler(btnConvert_Click);
		this.btnGnl2.Location = new System.Drawing.Point(517, 1);
		this.btnGnl2.Name = "btnGnl2";
		this.btnGnl2.Size = new System.Drawing.Size(83, 23);
		this.btnGnl2.TabIndex = 22;
		this.btnGnl2.Text = "更新组分表2";
		this.btnGnl2.UseVisualStyleBackColor = true;
		this.btnGnl2.Visible = false;
		this.btnGnl2.Click += new System.EventHandler(BtnGnl2_Click);
		this.btnGnl.Location = new System.Drawing.Point(633, -3);
		this.btnGnl.Name = "btnGnl";
		this.btnGnl.Size = new System.Drawing.Size(74, 23);
		this.btnGnl.TabIndex = 21;
		this.btnGnl.Text = "更新组分表";
		this.btnGnl.UseVisualStyleBackColor = true;
		this.btnGnl.Visible = false;
		this.btnGnl.Click += new System.EventHandler(BtnGnl_Click);
		this.cbNMHC.AutoSize = true;
		this.cbNMHC.Location = new System.Drawing.Point(593, 5);
		this.cbNMHC.Name = "cbNMHC";
		this.cbNMHC.Size = new System.Drawing.Size(84, 16);
		this.cbNMHC.TabIndex = 20;
		this.cbNMHC.Text = "非甲烷总烃";
		this.cbNMHC.UseVisualStyleBackColor = true;
		this.cbNMHC.Visible = false;
		this.cbNMHC.CheckedChanged += new System.EventHandler(cbNMHC_CheckedChanged);
		this.btUpdataByXX.Location = new System.Drawing.Point(578, 2);
		this.btUpdataByXX.Name = "btUpdataByXX";
		this.btUpdataByXX.Size = new System.Drawing.Size(99, 23);
		this.btUpdataByXX.TabIndex = 19;
		this.btUpdataByXX.Text = "上传到DCS";
		this.btUpdataByXX.UseVisualStyleBackColor = true;
		this.btUpdataByXX.Visible = false;
		this.btUpdataByXX.Click += new System.EventHandler(btUpdataByXX_Click);
		this.bExportXY.Location = new System.Drawing.Point(810, 5);
		this.bExportXY.Name = "bExportXY";
		this.bExportXY.Size = new System.Drawing.Size(89, 23);
		this.bExportXY.TabIndex = 17;
		this.bExportXY.Text = "导出谱图点";
		this.bExportXY.UseVisualStyleBackColor = true;
		this.bExportXY.Visible = false;
		this.bExportXY.Click += new System.EventHandler(bExportXY_Click);
		this.btnTimeFull.Location = new System.Drawing.Point(395, 3);
		this.btnTimeFull.Name = "btnTimeFull";
		this.btnTimeFull.Size = new System.Drawing.Size(47, 23);
		this.btnTimeFull.TabIndex = 17;
		this.btnTimeFull.Text = "满屏";
		this.btnTimeFull.UseVisualStyleBackColor = true;
		this.btnTimeFull.Click += new System.EventHandler(btnTimeFull_Click);
		this.btnXYFull.Location = new System.Drawing.Point(212, 2);
		this.btnXYFull.Name = "btnXYFull";
		this.btnXYFull.Size = new System.Drawing.Size(47, 23);
		this.btnXYFull.TabIndex = 18;
		this.btnXYFull.Text = "满屏";
		this.btnXYFull.UseVisualStyleBackColor = true;
		this.btnXYFull.Click += new System.EventHandler(btnXYFull_Click);
		this.mtbTime.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.mtbTime.Font = new System.Drawing.Font("宋体", 9f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 134);
		this.mtbTime.ForeColor = System.Drawing.Color.Lime;
		this.mtbTime.Location = new System.Drawing.Point(330, 4);
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
		this.label32.AutoSize = true;
		this.label32.Font = new System.Drawing.Font("宋体", 9f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 134);
		this.label32.ForeColor = System.Drawing.Color.Black;
		this.label32.Location = new System.Drawing.Point(265, 9);
		this.label32.Name = "label32";
		this.label32.Size = new System.Drawing.Size(64, 12);
		this.label32.TabIndex = 9;
		this.label32.Text = "满屏时间:";
		this.mtbSigYEnd.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.mtbSigYEnd.Font = new System.Drawing.Font("宋体", 9f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 134);
		this.mtbSigYEnd.ForeColor = System.Drawing.Color.Lime;
		this.mtbSigYEnd.Location = new System.Drawing.Point(135, 4);
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
		this.label31.AutoSize = true;
		this.label31.Location = new System.Drawing.Point(373, 9);
		this.label31.Name = "label31";
		this.label31.Size = new System.Drawing.Size(23, 12);
		this.label31.TabIndex = 16;
		this.label31.Text = "min";
		this.mtbSigYBeg.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.mtbSigYBeg.Font = new System.Drawing.Font("宋体", 9f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 134);
		this.mtbSigYBeg.ForeColor = System.Drawing.Color.Lime;
		this.mtbSigYBeg.Location = new System.Drawing.Point(42, 4);
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
		this.label29.AutoSize = true;
		this.label29.Location = new System.Drawing.Point(192, 9);
		this.label29.Name = "label29";
		this.label29.Size = new System.Drawing.Size(17, 12);
		this.label29.TabIndex = 15;
		this.label29.Text = "mV";
		this.label119.AutoSize = true;
		this.label119.Font = new System.Drawing.Font("宋体", 9f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 134);
		this.label119.ForeColor = System.Drawing.Color.Black;
		this.label119.Location = new System.Drawing.Point(4, 8);
		this.label119.Name = "label119";
		this.label119.Size = new System.Drawing.Size(38, 12);
		this.label119.TabIndex = 10;
		this.label119.Text = "下限:";
		this.label1.AutoSize = true;
		this.label1.Font = new System.Drawing.Font("宋体", 9f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 134);
		this.label1.ForeColor = System.Drawing.Color.Black;
		this.label1.Location = new System.Drawing.Point(96, 9);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(38, 12);
		this.label1.TabIndex = 11;
		this.label1.Text = "上限:";
		this.splitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
		this.splitContainer.Location = new System.Drawing.Point(0, 0);
		this.splitContainer.Name = "splitContainer";
		this.splitContainer.Orientation = System.Windows.Forms.Orientation.Horizontal;
		this.splitContainer.Panel1.Controls.Add(this.dpgnlChrom);
		this.splitContainer.Panel2.Controls.Add(this.chromDataGrid);
		this.splitContainer.Size = new System.Drawing.Size(864, 694);
		this.splitContainer.SplitterDistance = 329;
		this.splitContainer.SplitterWidth = 6;
		this.splitContainer.TabIndex = 13;
		this.splitContainer.SplitterMoved += new System.Windows.Forms.SplitterEventHandler(splitContainer_SplitterMoved);
		this.dpgnlChrom.BackColor = System.Drawing.Color.BlanchedAlmond;
		this.dpgnlChrom.Cursor = System.Windows.Forms.Cursors.Default;
		this.dpgnlChrom.Dock = System.Windows.Forms.DockStyle.Fill;
		this.dpgnlChrom.Location = new System.Drawing.Point(0, 0);
		this.dpgnlChrom.Name = "dpgnlChrom";
		this.dpgnlChrom.Size = new System.Drawing.Size(864, 329);
		this.dpgnlChrom.TabIndex = 9;
		this.dpgnlChrom.Click += new System.EventHandler(dpgpcChrom_Click);
		this.dpgnlChrom.Paint += new System.Windows.Forms.PaintEventHandler(dpgpcChrom_Paint);
		this.dpgnlChrom.DoubleClick += new System.EventHandler(dpgpcChrom_DoubleClick);
		this.dpgnlChrom.MouseDown += new System.Windows.Forms.MouseEventHandler(dpgpcChrom_MouseDown);
		this.dpgnlChrom.MouseLeave += new System.EventHandler(dpgpcChrom_MouseLeave);
		this.dpgnlChrom.MouseMove += new System.Windows.Forms.MouseEventHandler(dpgpcChrom_MouseMove);
		this.dpgnlChrom.MouseUp += new System.Windows.Forms.MouseEventHandler(dpgpcChrom_MouseUp);
		this.dpgnlChrom.Resize += new System.EventHandler(dpgnlChrom_Resize);
		this.chromDataGrid.Dock = System.Windows.Forms.DockStyle.Fill;
		this.chromDataGrid.GetcbNMHCChecked = null;
		this.chromDataGrid.GetChromatogram = null;
		this.chromDataGrid.GetChromatogramList = null;
		this.chromDataGrid.GetChromDisplay = null;
		this.chromDataGrid.GetHasChrom = null;
		this.chromDataGrid.GetSmyTabOpt = null;
		this.chromDataGrid.Location = new System.Drawing.Point(0, 0);
		this.chromDataGrid.Margin = new System.Windows.Forms.Padding(4);
		this.chromDataGrid.mySetslbExplainText = null;
		this.chromDataGrid.Name = "chromDataGrid";
		this.chromDataGrid.Size = new System.Drawing.Size(864, 359);
		this.chromDataGrid.TabIndex = 0;
		this.mstSetChromForm.AutoScroll = true;
		this.mstSetChromForm.devManager = (IBrainChrom2018.InsDeviceManager)resources.GetObject("mstSetChromForm.devManager");
		this.mstSetChromForm.Dock = System.Windows.Forms.DockStyle.Fill;
		this.mstSetChromForm.Location = new System.Drawing.Point(0, 0);
		this.mstSetChromForm.Margin = new System.Windows.Forms.Padding(4);
		this.mstSetChromForm.Name = "mstSetChromForm";
		this.mstSetChromForm.PrintMethod = (IBrainChrom2018.PrintPara)resources.GetObject("mstSetChromForm.PrintMethod");
		this.mstSetChromForm.ShowComponentTable = false;
		this.mstSetChromForm.ShowMethodNew = true;
		this.mstSetChromForm.ShowOnlineMethod = false;
		this.mstSetChromForm.ShowOnlineMethod2 = false;
		this.mstSetChromForm.Size = new System.Drawing.Size(311, 724);
		this.mstSetChromForm.TabIndex = 0;
		this.mstSetChromForm.OnMethodSaveEvent += new System.EventHandler(mstSetChromForm_OnMethodSaveEvent);
		this.mstSetChromForm.OnUseSet += new System.EventHandler(mstSetChromForm_OnUseSet);
		this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
		this.splitContainer2.Location = new System.Drawing.Point(0, 64);
		this.splitContainer2.Name = "splitContainer2";
		this.splitContainer2.Panel1.Controls.Add(this.chromFormFileSearchCtrl1);
		this.splitContainer2.Panel2.Controls.Add(this.splitContainer1);
		this.splitContainer2.Size = new System.Drawing.Size(1388, 724);
		this.splitContainer2.SplitterDistance = 205;
		this.splitContainer2.TabIndex = 16;
		this.chromFormFileSearchCtrl1.Dock = System.Windows.Forms.DockStyle.Fill;
		this.chromFormFileSearchCtrl1.Location = new System.Drawing.Point(0, 0);
		this.chromFormFileSearchCtrl1.Margin = new System.Windows.Forms.Padding(4);
		this.chromFormFileSearchCtrl1.Name = "chromFormFileSearchCtrl1";
		this.chromFormFileSearchCtrl1.Size = new System.Drawing.Size(205, 724);
		this.chromFormFileSearchCtrl1.TabIndex = 0;
		this.printDialog_0.Document = this.printDocument_0;
		this.printDialog_0.UseEXDialog = true;
		this.printDocument_0.BeginPrint += new System.Drawing.Printing.PrintEventHandler(printDocument_0_BeginPrint);
		this.printDocument_0.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(printDocument_0_PrintPage);
		this.prtPrvDlg.AutoScrollMargin = new System.Drawing.Size(0, 0);
		this.prtPrvDlg.AutoScrollMinSize = new System.Drawing.Size(0, 0);
		this.prtPrvDlg.ClientSize = new System.Drawing.Size(400, 300);
		this.prtPrvDlg.Document = this.printDocument_0;
		this.prtPrvDlg.Enabled = true;
		this.prtPrvDlg.Icon = (System.Drawing.Icon)resources.GetObject("prtPrvDlg.Icon");
		this.prtPrvDlg.Name = "prtPrvDlg";
		this.prtPrvDlg.Visible = false;
		base.Controls.Add(this.splitContainer2);
		base.Controls.Add(this.cChangePeak);
		base.Controls.Add(this.tsDatAcq);
		base.Controls.Add(this.flpChrom);
		base.Controls.Add(this.ssChrom);
		base.Controls.Add(this.msChrom);
		base.Name = "ChromFormCtrl";
		base.Size = new System.Drawing.Size(1388, 810);
		base.Load += new System.EventHandler(ChromForm_Load);
		base.KeyDown += new System.Windows.Forms.KeyEventHandler(ChromForm_KeyDown);
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
		this.cmsLibs.ResumeLayout(false);
		this.cmsSSTCmpds.ResumeLayout(false);
		this.cmsSlices.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.dataGridView2).EndInit();
		((System.ComponentModel.ISupportInitialize)this.dataGridView3).EndInit();
		this.splitContainer1.Panel1.ResumeLayout(false);
		this.splitContainer1.Panel2.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.splitContainer1).EndInit();
		this.splitContainer1.ResumeLayout(false);
		this.splitContainer4.Panel1.ResumeLayout(false);
		this.splitContainer4.Panel1.PerformLayout();
		this.splitContainer4.Panel2.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.splitContainer4).EndInit();
		this.splitContainer4.ResumeLayout(false);
		this.splitContainer.Panel1.ResumeLayout(false);
		this.splitContainer.Panel2.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.splitContainer).EndInit();
		this.splitContainer.ResumeLayout(false);
		this.splitContainer2.Panel1.ResumeLayout(false);
		this.splitContainer2.Panel2.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.splitContainer2).EndInit();
		this.splitContainer2.ResumeLayout(false);
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
