using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using IBrainChrom2018.Unit;

namespace IBrainChrom2018;

public class ChromForm : Form
{
	private SystemParam sysParam = SystemParam.Create();

	private ChromDeviceListMgr cdlMgr = ChromDeviceListMgr.Create();

	public static ChromForm form = null;

	public string strDetec = "";

	private ToolStripButton btnProperties;

	private ToolStripButton btnPrtLink;

	private ToolStripButton btnReportSetup;

	private IContainer icontainer_0;

	private LbLineDlg lbLineDlg_0 = new LbLineDlg();

	private LbTextDlg lbTextDlg_0 = new LbTextDlg();

	private ManuDlg manuDlg_0;

	private SSTParasDlg sstparasDlg_0;

	private SST sst_0 = new SST();

	private ToolStripMenuItem toolStripMenuItem_0 = new ToolStripMenuItem();

	private ToolStripMenuItem toolStripMenuItem_1 = new ToolStripMenuItem();

	private ToolStripSeparator toolStripSeparator16;

	private ToolStripSeparator toolStripSeparator17;

	private ToolStripSeparator toolStripSeparator18;

	private ToolStripSeparator toolStripSeparator19;

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

	public RptSetupDlg dlgReportSetup;

	private IContainer components;

	public ChromFormCtrl chromDataGrid;

	public Chromatogram CurChrom => chromDataGrid.CurChrom;

	public DisLg CurDisLg => chromDataGrid.CurDisLg;

	public bool HasChrom => chromDataGrid.HasChrom;

	public void InitChromFormDataGrid()
	{
	}

	private void ChromDataGrid_OnDisDpRefresh(object sender, EventArgs e)
	{
		DisDpRefresh();
	}

	public void InitFm()
	{
		chromDataGrid.InitFm();
	}

	public ChromForm()
	{
		try
		{
			InitializeComponent();
			form = this;
			CtrlLangPS.Instance.UpdateLanguageForAllControl(this);
		}
		catch (Exception)
		{
		}
	}

	private void ChromForm_Load(object sender, EventArgs e)
	{
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

	public void ChkOverlayMode()
	{
	}

	public void DisDpRefresh()
	{
	}

	public void LoadOptions()
	{
	}

	public void ReadWinInfo(WinInfo winInfo)
	{
	}

	public void refresh_once()
	{
	}

	public void WriteWinInfo(WinInfo winInfo)
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

	private void ChromForm_KeyDown(object sender, KeyEventArgs e)
	{
		chromDataGrid.ChromForm_KeyDown(sender, e);
	}

	public void miFiCloseAll_Click(object sender, EventArgs e)
	{
		chromDataGrid.miFiCloseAll_Click(sender, e);
	}

	public void OpenChrom(string chromName, bool sampling, bool useCurrent)
	{
		chromDataGrid.OpenChrom(chromName, sampling, useCurrent);
	}

	public void Opensda(string filePath)
	{
		chromDataGrid.Opensda(filePath);
	}

	public void SetProjectDir(string projectDir)
	{
		chromDataGrid.SetProjectDir(projectDir);
	}

	public void SetChromsLink(ref Chromatogram[] chroms, ref int activeNo, ref SST sst_1)
	{
		chromDataGrid.SetChromsLink(ref chroms, ref activeNo, ref sst_1);
	}

	public void SetSignalsColor()
	{
		chromDataGrid.SetSignalsColor();
	}

	public string RetSstItem(Peak peak, string item)
	{
		return chromDataGrid.RetSstItem(peak, item);
	}

	public string RetSstItem(SstItem sstItem, int rowIndex)
	{
		return chromDataGrid.RetSstItem(sstItem, rowIndex);
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(IBrainChrom2018.ChromForm));
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
		this.toolStripSeparator16 = new System.Windows.Forms.ToolStripSeparator();
		this.toolStripSeparator17 = new System.Windows.Forms.ToolStripSeparator();
		this.toolStripSeparator18 = new System.Windows.Forms.ToolStripSeparator();
		this.btnProperties = new System.Windows.Forms.ToolStripButton();
		this.toolStripSeparator19 = new System.Windows.Forms.ToolStripSeparator();
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
		this.chromDataGrid = new IBrainChrom2018.ChromFormCtrl();
		((System.ComponentModel.ISupportInitialize)this.dataGridView2).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.dataGridView3).BeginInit();
		base.SuspendLayout();
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
		this.toolStripSeparator19.Name = "toolStripSeparator19";
		this.toolStripSeparator19.Size = new System.Drawing.Size(6, 39);
		this.btnReportSetup.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
		this.btnReportSetup.Image = (System.Drawing.Image)resources.GetObject("btnReportSetup.Image");
		this.btnReportSetup.ImageTransparentColor = System.Drawing.Color.Magenta;
		this.btnReportSetup.Name = "btnReportSetup";
		this.btnReportSetup.Size = new System.Drawing.Size(36, 36);
		this.btnReportSetup.Text = "toolStripButton4";
		this.btnReportSetup.ToolTipText = "设置";
		this.btnPrtLink.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
		this.btnPrtLink.Image = (System.Drawing.Image)resources.GetObject("btnPrtLink.Image");
		this.btnPrtLink.ImageTransparentColor = System.Drawing.Color.Magenta;
		this.btnPrtLink.Name = "btnPrtLink";
		this.btnPrtLink.Size = new System.Drawing.Size(36, 36);
		this.btnPrtLink.Text = "toolStripButton1";
		this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
		this.toolStripButton1.Image = (System.Drawing.Image)resources.GetObject("toolStripButton1.Image");
		this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
		this.toolStripButton1.Name = "toolStripButton1";
		this.toolStripButton1.Size = new System.Drawing.Size(36, 36);
		this.toolStripButton1.Text = "toolStripButton1";
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
		this.chromDataGrid.Dock = System.Windows.Forms.DockStyle.Fill;
		this.chromDataGrid.Location = new System.Drawing.Point(0, 0);
		this.chromDataGrid.Name = "chromDataGrid";
		this.chromDataGrid.ShowManuAndStateBar = true;
		this.chromDataGrid.ShowOnlineMethod = false;
		this.chromDataGrid.Size = new System.Drawing.Size(1221, 657);
		this.chromDataGrid.TabIndex = 0;
		base.ClientSize = new System.Drawing.Size(1221, 657);
		base.Controls.Add(this.chromDataGrid);
		base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
		base.Name = "ChromForm";
		base.WindowState = System.Windows.Forms.FormWindowState.Maximized;
		base.FormClosing += new System.Windows.Forms.FormClosingEventHandler(ChromForm_FormClosing);
		base.FormClosed += new System.Windows.Forms.FormClosedEventHandler(ChromForm_FormClosed);
		base.Load += new System.EventHandler(ChromForm_Load);
		base.KeyDown += new System.Windows.Forms.KeyEventHandler(ChromForm_KeyDown);
		((System.ComponentModel.ISupportInitialize)this.dataGridView2).EndInit();
		((System.ComponentModel.ISupportInitialize)this.dataGridView3).EndInit();
		base.ResumeLayout(false);
	}
}
