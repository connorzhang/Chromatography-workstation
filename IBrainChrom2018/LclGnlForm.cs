using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace IBrainChrom2018;

public class LclGnlForm : LclForm
{
	public int formNo;

	private IContainer icontainer_1;

	public Instrument instrument;

	protected ToolStripMenuItem miHelp;

	protected ToolStripMenuItem miHpHelp;

	protected ToolStripMenuItem miView;

	protected ToolStripMenuItem miVwAlwaysOnTop;

	public ToolStripMenuItem miWinCaliGnl;

	public ToolStripMenuItem miWinCaliGpc;

	public ToolStripMenuItem miWinChromatogram;

	public ToolStripMenuItem miWinDataAcq;

	public ToolStripMenuItem miWinDevMonitor;

	protected ToolStripMenuItem miWindow;

	public ToolStripMenuItem miWinInstrument;

	public ToolStripMenuItem miWinMain;

	public ToolStripMenuItem miWinSeqAly;

	public ToolStripMenuItem miWinSglAly;

	public ToolStripMenuItem miWinStationAuditTrail;

	protected ToolStripButton mubtnCaliGnl;

	protected ToolStripButton mubtnCaliGpc;

	protected ToolStripButton mubtnChromatogram;

	protected ToolStripButton mubtnDataAcq;

	protected ToolStripButton mubtnDevMonitor;

	protected ToolStripButton mubtnInstrument;

	protected ToolStripButton mubtnMainForm;

	protected ToolStripButton mubtnSeqAly;

	protected ToolStripButton mubtnSglAly;

	public LclGnlForm()
	{
		InitializeComponent();
	}

	private void LclGnlForm_Load(object sender, EventArgs e)
	{
		LoadLanguage();
	}

	public virtual void LoadLanguage()
	{
		miView.Text = Lang.PS("查看", "View");
		miVwAlwaysOnTop.Text = Lang.PS("顶层显示", "Always On Top");
		miWindow.Text = Lang.PS("窗口", "Window");
		miWinMain.Text = Lang.PS("主窗口", "Main");
		miWinInstrument.Text = Lang.PS("仪器", "Instrument");
		miWinDataAcq.Text = Lang.PS("数据采集", "Data Acquisition");
		miWinChromatogram.Text = Lang.PS("谱图", "Chromatogram");
		miWinCaliGnl.Text = Lang.PS("校正", "Calibration");
		miWinCaliGpc.Text = Lang.PS("GPC 校正", "GPC Calibration");
		miWinSglAly.Text = Lang.PS("单针分析", "Single Analysis");
		miWinSeqAly.Text = Lang.PS("序列分析", "Sequence Analysis");
		miWinDevMonitor.Text = Lang.PS("设备监视", "Device Monitor");
		miWinStationAuditTrail.Text = Lang.PS("工作站日志", "Station Audit Trail");
		ToolStripMenuItem toolStripMenuItem = miHelp;
		string text = (miHpHelp.Text = Lang.PS("帮助", "Help"));
		toolStripMenuItem.Text = text;
		mubtnMainForm.Text = miWinMain.Text;
		mubtnInstrument.Text = miWinInstrument.Text;
		mubtnDataAcq.Text = miWinDataAcq.Text;
		mubtnChromatogram.Text = miWinChromatogram.Text;
		mubtnCaliGnl.Text = miWinCaliGnl.Text;
		mubtnCaliGpc.Text = miWinCaliGpc.Text;
		mubtnSglAly.Text = miWinSglAly.Text;
		mubtnDevMonitor.Text = miWinDevMonitor.Text;
	}

	protected void miFiExit_Click(object sender, EventArgs e)
	{
		Close();
	}

	protected virtual void miHpHelp_Click(object sender, EventArgs e)
	{
	}

	private void miVwAlwaysOnTop_Click(object sender, EventArgs e)
	{
		miVwAlwaysOnTop.Checked = !miVwAlwaysOnTop.Checked;
		base.TopMost = miVwAlwaysOnTop.Checked;
	}

	public void miWindowsHandler(EventHandler dropDownOpeningHandler, EventHandler clickHandler)
	{
		miWindow.DropDownOpening += dropDownOpeningHandler;
		miWinInstrument.Click += clickHandler;
		miWinDataAcq.Click += clickHandler;
		miWinChromatogram.Click += clickHandler;
		miWinCaliGnl.Click += clickHandler;
		miWinCaliGpc.Click += clickHandler;
		miWinSglAly.Click += clickHandler;
		miWinSeqAly.Click += clickHandler;
		miWinDevMonitor.Click += clickHandler;
		miWinStationAuditTrail.Click += clickHandler;
	}

	public void mubtnClickHandler(EventHandler mainFormHandler, EventHandler instruHandler, EventHandler dataAcqHandler, EventHandler chromHandler, EventHandler caliHandler, EventHandler singleAnalysisHandler, EventHandler sequenceAnalysisHandler, EventHandler deviceMonitorHandler)
	{
		miWinMain.Click += mainFormHandler;
		mubtnMainForm.Click += mainFormHandler;
		mubtnInstrument.Click += instruHandler;
		mubtnDataAcq.Click += dataAcqHandler;
		mubtnChromatogram.Click += chromHandler;
		mubtnCaliGnl.Click += caliHandler;
		mubtnCaliGpc.Click += caliHandler;
		mubtnSglAly.Click += singleAnalysisHandler;
		mubtnSeqAly.Click += sequenceAnalysisHandler;
		mubtnDevMonitor.Click += deviceMonitorHandler;
	}

	protected override void OnKeyDown(KeyEventArgs keyEventArgs_0)
	{
		if (keyEventArgs_0.KeyCode == Keys.F1)
		{
			miHpHelp_Click(null, null);
		}
		base.OnKeyDown(keyEventArgs_0);
	}

	public virtual void ReadWinInfo(WinInfo winInfo)
	{
		base.WindowState = winInfo.windowState;
		base.Location = new Point(winInfo.left, winInfo.int_0);
		if ((base.FormBorderStyle == FormBorderStyle.Sizable || base.FormBorderStyle == FormBorderStyle.SizableToolWindow) && winInfo.width > 0 && winInfo.height > 0)
		{
			base.Size = new Size(winInfo.width, winInfo.height);
		}
	}

	public virtual void refresh_once()
	{
	}

	public virtual void SetProjectDir(string projectDir)
	{
	}

	public virtual void WriteWinInfo(WinInfo winInfo)
	{
		winInfo.visible = base.Visible;
		winInfo.windowState = base.WindowState;
		if (base.WindowState != FormWindowState.Minimized)
		{
			winInfo.left = base.Left;
			winInfo.int_0 = base.Top;
			winInfo.width = base.Width;
			winInfo.height = base.Height;
		}
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
		this.miHelp = new System.Windows.Forms.ToolStripMenuItem();
		this.miHpHelp = new System.Windows.Forms.ToolStripMenuItem();
		this.miView = new System.Windows.Forms.ToolStripMenuItem();
		this.miVwAlwaysOnTop = new System.Windows.Forms.ToolStripMenuItem();
		this.miWinCaliGnl = new System.Windows.Forms.ToolStripMenuItem();
		this.miWinCaliGpc = new System.Windows.Forms.ToolStripMenuItem();
		this.miWinChromatogram = new System.Windows.Forms.ToolStripMenuItem();
		this.miWinDataAcq = new System.Windows.Forms.ToolStripMenuItem();
		this.miWinDevMonitor = new System.Windows.Forms.ToolStripMenuItem();
		this.miWindow = new System.Windows.Forms.ToolStripMenuItem();
		this.miWinInstrument = new System.Windows.Forms.ToolStripMenuItem();
		this.miWinMain = new System.Windows.Forms.ToolStripMenuItem();
		this.miWinSeqAly = new System.Windows.Forms.ToolStripMenuItem();
		this.miWinSglAly = new System.Windows.Forms.ToolStripMenuItem();
		this.miWinStationAuditTrail = new System.Windows.Forms.ToolStripMenuItem();
		this.mubtnCaliGnl = new System.Windows.Forms.ToolStripButton();
		this.mubtnCaliGpc = new System.Windows.Forms.ToolStripButton();
		this.mubtnChromatogram = new System.Windows.Forms.ToolStripButton();
		this.mubtnDataAcq = new System.Windows.Forms.ToolStripButton();
		this.mubtnDevMonitor = new System.Windows.Forms.ToolStripButton();
		this.mubtnInstrument = new System.Windows.Forms.ToolStripButton();
		this.mubtnMainForm = new System.Windows.Forms.ToolStripButton();
		this.mubtnSeqAly = new System.Windows.Forms.ToolStripButton();
		this.mubtnSglAly = new System.Windows.Forms.ToolStripButton();
		base.SuspendLayout();
		this.mubtnSeqAly.Visible = false;
		this.miWinSeqAly.Visible = false;
		this.miView.DropDownItems.Add(this.miVwAlwaysOnTop);
		this.miVwAlwaysOnTop.Click += new System.EventHandler(miVwAlwaysOnTop_Click);
		this.miWinMain.Checked = true;
		this.miWinInstrument.Checked = true;
		this.miWindow.DropDownItems.Add(this.miWinMain);
		this.miWindow.DropDownItems.Add(this.miWinInstrument);
		this.miWindow.DropDownItems.Add(new System.Windows.Forms.ToolStripSeparator());
		this.miWindow.DropDownItems.Add(this.miWinDataAcq);
		this.miWindow.DropDownItems.Add(this.miWinChromatogram);
		this.miWindow.DropDownItems.Add(this.miWinCaliGnl);
		this.miWindow.DropDownItems.Add(this.miWinCaliGpc);
		this.miWindow.DropDownItems.Add(this.miWinSglAly);
		this.miWindow.DropDownItems.Add(this.miWinSeqAly);
		this.miWindow.DropDownItems.Add(this.miWinDevMonitor);
		this.miWindow.DropDownItems.Add(this.miWinStationAuditTrail);
		this.miWinInstrument.Tag = IBrainChrom2018.WinStyle.Instrument;
		this.miWinDataAcq.Tag = IBrainChrom2018.WinStyle.DataAcq;
		this.miWinChromatogram.Tag = IBrainChrom2018.WinStyle.Chromatogram;
		this.miWinCaliGnl.Tag = IBrainChrom2018.WinStyle.CaliGnl;
		this.miWinCaliGpc.Tag = IBrainChrom2018.WinStyle.CaliGpc;
		this.miWinSglAly.Tag = IBrainChrom2018.WinStyle.SglAly;
		this.miWinSeqAly.Tag = IBrainChrom2018.WinStyle.SeqAly;
		this.miWinDevMonitor.Tag = IBrainChrom2018.WinStyle.DevMonitor;
		this.miWinStationAuditTrail.Tag = IBrainChrom2018.WinStyle.StationAdtTrl;
		this.miHelp.DropDownItems.Add(this.miHpHelp);
		this.miHpHelp.Click += new System.EventHandler(miHpHelp_Click);
		this.mubtnDevMonitor.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
		this.mubtnSeqAly.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
		this.mubtnSglAly.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
		this.mubtnCaliGpc.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
		this.mubtnCaliGnl.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
		this.mubtnChromatogram.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
		this.mubtnDataAcq.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
		this.mubtnInstrument.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
		this.mubtnMainForm.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
		this.mubtnDevMonitor.AutoSize = false;
		this.mubtnSeqAly.AutoSize = false;
		this.mubtnSglAly.AutoSize = false;
		this.mubtnCaliGpc.AutoSize = false;
		this.mubtnCaliGnl.AutoSize = false;
		this.mubtnChromatogram.AutoSize = false;
		this.mubtnDataAcq.AutoSize = false;
		this.mubtnInstrument.AutoSize = false;
		this.mubtnMainForm.AutoSize = false;
		this.mubtnDevMonitor.Size = new System.Drawing.Size(20, 20);
		this.mubtnSeqAly.Size = new System.Drawing.Size(20, 20);
		this.mubtnSglAly.Size = new System.Drawing.Size(20, 20);
		this.mubtnCaliGpc.Size = new System.Drawing.Size(20, 20);
		this.mubtnCaliGnl.Size = new System.Drawing.Size(20, 20);
		this.mubtnChromatogram.Size = new System.Drawing.Size(20, 20);
		this.mubtnDataAcq.Size = new System.Drawing.Size(20, 20);
		this.mubtnInstrument.Size = new System.Drawing.Size(20, 20);
		this.mubtnMainForm.Size = new System.Drawing.Size(20, 20);
		this.mubtnDevMonitor.Margin = new System.Windows.Forms.Padding(5, 1, 1, 1);
		this.mubtnSeqAly.Margin = new System.Windows.Forms.Padding(5, 1, 1, 1);
		this.mubtnSglAly.Margin = new System.Windows.Forms.Padding(5, 1, 1, 1);
		this.mubtnCaliGpc.Margin = new System.Windows.Forms.Padding(5, 1, 1, 1);
		this.mubtnCaliGnl.Margin = new System.Windows.Forms.Padding(5, 1, 1, 1);
		this.mubtnChromatogram.Margin = new System.Windows.Forms.Padding(5, 1, 1, 1);
		this.mubtnDataAcq.Margin = new System.Windows.Forms.Padding(5, 1, 1, 1);
		this.mubtnInstrument.Margin = new System.Windows.Forms.Padding(5, 1, 1, 1);
		this.mubtnMainForm.Margin = new System.Windows.Forms.Padding(5, 1, 1, 1);
		IBrainChrom2018.ResourceImageLoad.SetCtrlBitmap(this.mubtnMainForm, IBrainChrom2018.SystemIconResource.smethod_55());
		IBrainChrom2018.ResourceImageLoad.SetCtrlBitmap(this.mubtnDataAcq, IBrainChrom2018.SystemIconResource.smethod_49());
		IBrainChrom2018.ResourceImageLoad.SetCtrlBitmap(this.mubtnChromatogram, IBrainChrom2018.SystemIconResource.smethod_48());
		IBrainChrom2018.ResourceImageLoad.SetCtrlBitmap(this.mubtnCaliGnl, IBrainChrom2018.SystemIconResource.smethod_46());
		IBrainChrom2018.ResourceImageLoad.SetCtrlBitmap(this.mubtnCaliGpc, IBrainChrom2018.SystemIconResource.smethod_47());
		IBrainChrom2018.ResourceImageLoad.SetCtrlBitmap(this.mubtnSglAly, IBrainChrom2018.SystemIconResource.smethod_62());
		IBrainChrom2018.ResourceImageLoad.SetCtrlBitmap(this.mubtnSeqAly, IBrainChrom2018.SystemIconResource.smethod_61());
		IBrainChrom2018.ResourceImageLoad.SetCtrlBitmap(this.mubtnDevMonitor, IBrainChrom2018.SystemIconResource.smethod_50());
		base.TopMost = this.miVwAlwaysOnTop.Checked;
		this.instrument = new IBrainChrom2018.Instrument();
		this.instrument.user = new IBrainChrom2018.User();
		this.instrument.user.options = new IBrainChrom2018.Options();
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
		base.ClientSize = new System.Drawing.Size(401, 198);
		base.Name = "LclGnlForm";
		base.Load += new System.EventHandler(LclGnlForm_Load);
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
