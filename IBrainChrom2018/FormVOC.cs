using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using IBrainChrom2018.Unit;

namespace IBrainChrom2018;

public class FormVOC : Form
{
	public static FormVOC fromVoc = null;

	private SystemParam sysParam = SystemParam.Create();

	private VocParam vocParam = VocParam.Create();

	private FormMainParam frmParam = FormMainParam.Create();

	private IContainer components = null;

	public Label lbBTEX9;

	public Label lbBTEX9T;

	public Label lbBTEX8;

	public Label lbBTEX8T;

	public Label lbNMHC;

	public Label lbBTEX;

	public Label lbNMHCT;

	public Label lbBTEXt;

	public Label lbBTEX7;

	public Label lbBTEX7T;

	public Label lbBTEX5;

	public Label lbBTEX6;

	public Label lbBTEX4;

	public Label lbBTEX5T;

	public Label lbBTEX6T;

	public Label lbBTEX4T;

	public Label lbBTEX2;

	public Label lbBTEX3;

	public Label lbBTEX1;

	public Label lbBTEX2T;

	public Label lbBTEX3T;

	public Label lbBTEX1T;

	public Label lbCH4;

	public Label label79;

	public Label lbTHC;

	public Label label77;

	public Label label1;

	public Label labAnaTimes;

	public Button Fire1;

	private Label labGSM;

	public Button Fire2;

	public ImageList imageList1;

	public TextBox txsignal1;

	public TextBox txsignal2;

	public Label labFireState;

	private PictureBox pictureBox1;

	private SplitContainer sctForm;

	private SplitContainer sctMain;

	public GroupBox gbBenXW;

	public GroupBox gbNHMC1;

	public SplitContainer sctSetting;

	public Label label3;

	public Label lbBXWDanWei;

	public Label label2;

	public Label lbNMHCDanWei;

	private Label label19;

	private Label label18;

	private Label label17;

	private Label label16;

	private Label label15;

	public PictureBox pB5;

	public PictureBox pB4;

	public PictureBox pB3;

	public PictureBox pB2;

	public PictureBox pB1;

	public TextBox tbTime;

	public Button btnData;

	public Button button1;

	public Button btnDes;

	private GroupBox gbSignal2;

	public TextBox tbTime2;

	private GroupBox groupBox1;

	public FormVOC()
	{
		InitializeComponent();
		fromVoc = this;
		LoadLanguage();
		labGSM.Text = AssemblyInfoCfg.VOCTitle;
		int num = Screen.PrimaryScreen.Bounds.Height;
		int num2 = Screen.PrimaryScreen.Bounds.Width;
		if (num2 < 1024)
		{
			labGSM.Font = new Font("宋体", 24f);
			label77.Font = (label79.Font = (lbNMHCT.Font = new Font("宋体", 12f)));
			lbBTEX1T.Font = (lbBTEX2T.Font = (lbBTEX3T.Font = (lbBTEX4T.Font = (lbBTEX5T.Font = (lbBTEX6T.Font = (lbBTEX7T.Font = (lbBTEX8T.Font = (lbBTEX9T.Font = (lbBTEXt.Font = new Font("宋体", 12f))))))))));
			lbBTEX1.Location = new Point(100, lbBTEX1.Location.Y);
			lbBTEX3.Location = new Point(100, lbBTEX3.Location.Y);
			lbBTEX5.Location = new Point(100, lbBTEX5.Location.Y);
			lbBTEX7.Location = new Point(100, lbBTEX7.Location.Y);
			lbBTEX9.Location = new Point(100, lbBTEX9.Location.Y);
			lbBTEX2T.Location = new Point(250, lbBTEX1.Location.Y);
			lbBTEX4T.Location = new Point(250, lbBTEX3.Location.Y);
			lbBTEX6T.Location = new Point(250, lbBTEX5.Location.Y);
			lbBTEX8T.Location = new Point(250, lbBTEX7.Location.Y);
			lbBTEX2.Location = new Point(350, lbBTEX1.Location.Y);
			lbBTEX4.Location = new Point(350, lbBTEX3.Location.Y);
			lbBTEX6.Location = new Point(350, lbBTEX5.Location.Y);
			lbBTEX8.Location = new Point(350, lbBTEX7.Location.Y);
			int num3 = sctSetting.Panel1.Width;
			Fire2.Location = new Point(300, Fire2.Location.Y);
			txsignal2.Location = new Point(Fire2.Location.X + 100, txsignal2.Location.Y);
		}
		if (frmParam.kindMachine != 4)
		{
			Fire2.Visible = true;
			txsignal2.Visible = true;
		}
		else
		{
			gbSignal2.Visible = false;
		}
		CtrlLangPS.Instance.UpdateLanguageForAllControl(this);
	}

	private void LoadLanguage()
	{
		label1.Text = Lang.PS("运行次数：", "Analysis of the number:");
		label2.Text = Lang.PS("单位", "Unit");
		label3.Text = Lang.PS("单位", "Unit");
		label77.Text = Lang.PS("总烃", "THC");
		label79.Text = Lang.PS("甲烷", "CH4");
		lbNMHCT.Text = Lang.PS("非甲烷总烃", "NMHC");
		label16.Text = Lang.PS("反吹", "backflush");
		btnData.Text = Lang.PS("数据查询", "dataQuery");
		groupBox1.Text = Lang.PS("通道1", "Channel 1");
		gbSignal2.Text = Lang.PS("通道2", "Channel 2");
	}

	private void button1_Click(object sender, EventArgs e)
	{
		FormMain.fromMain.Show();
		FormMain.fromMain.Activate();
		FormMain.fromMain.WindowState = FormWindowState.Maximized;
		Hide();
		if (FormMain.fromMain.WindowState == FormWindowState.Minimized)
		{
			FormMain.fromMain.WindowState = FormWindowState.Normal;
		}
	}

	private void lbBTEX4_Click(object sender, EventArgs e)
	{
		FormAreaPlot formAreaPlot = new FormAreaPlot(14);
		formAreaPlot.Show();
		formAreaPlot.loadData();
	}

	private void lbBTEX5T_Click(object sender, EventArgs e)
	{
	}

	private void lbBTEX6_Click(object sender, EventArgs e)
	{
		FormAreaPlot formAreaPlot = new FormAreaPlot(16);
		formAreaPlot.Show();
		formAreaPlot.loadData();
	}

	private void lbBTEX_Click(object sender, EventArgs e)
	{
		FormAreaPlot formAreaPlot = new FormAreaPlot(20);
		formAreaPlot.Show();
		formAreaPlot.loadData();
	}

	private void lbTHC_Click(object sender, EventArgs e)
	{
		FormAreaPlot formAreaPlot = new FormAreaPlot(1);
		formAreaPlot.Show();
		formAreaPlot.loadData();
	}

	private void lbCH4_Click(object sender, EventArgs e)
	{
		FormAreaPlot formAreaPlot = new FormAreaPlot(2);
		formAreaPlot.Show();
		formAreaPlot.loadData();
	}

	private void lbNMHC_Click(object sender, EventArgs e)
	{
		FormAreaPlot formAreaPlot = new FormAreaPlot(3);
		formAreaPlot.Show();
		formAreaPlot.loadData();
	}

	private void lbBTEX1_Click(object sender, EventArgs e)
	{
		FormAreaPlot formAreaPlot = new FormAreaPlot(11);
		formAreaPlot.Show();
		formAreaPlot.loadData();
	}

	private void lbBTEX2_Click(object sender, EventArgs e)
	{
		FormAreaPlot formAreaPlot = new FormAreaPlot(12);
		formAreaPlot.Show();
		formAreaPlot.loadData();
	}

	private void lbBTEX3_Click(object sender, EventArgs e)
	{
		FormAreaPlot formAreaPlot = new FormAreaPlot(13);
		formAreaPlot.Show();
		formAreaPlot.loadData();
	}

	private void lbBTEX5_Click(object sender, EventArgs e)
	{
		FormAreaPlot formAreaPlot = new FormAreaPlot(15);
		formAreaPlot.Show();
		formAreaPlot.loadData();
	}

	private void lbBTEX7_Click(object sender, EventArgs e)
	{
		FormAreaPlot formAreaPlot = new FormAreaPlot(17);
		formAreaPlot.Show();
		formAreaPlot.loadData();
	}

	private void lbBTEX8_Click(object sender, EventArgs e)
	{
		FormAreaPlot formAreaPlot = new FormAreaPlot(18);
		formAreaPlot.Show();
		formAreaPlot.loadData();
	}

	private void lbBTEX9_Click(object sender, EventArgs e)
	{
		FormAreaPlot formAreaPlot = new FormAreaPlot(19);
		formAreaPlot.Show();
		formAreaPlot.loadData();
	}

	private void btnData_Click(object sender, EventArgs e)
	{
		FormHistory formHistory = new FormHistory();
		formHistory.StartPosition = FormStartPosition.CenterScreen;
		formHistory.Show();
		formHistory.loadData();
	}

	private void btnDes_Click(object sender, EventArgs e)
	{
		if (fromVoc != null)
		{
			fromVoc.WindowState = FormWindowState.Minimized;
		}
	}

	protected override void Dispose(bool disposing)
	{
		if (disposing && components != null)
		{
			components.Dispose();
		}
		base.Dispose(disposing);
	}

	private void InitializeComponent()
	{
		this.components = new System.ComponentModel.Container();
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(IBrainChrom2018.FormVOC));
		this.button1 = new System.Windows.Forms.Button();
		this.lbBTEX9 = new System.Windows.Forms.Label();
		this.lbBTEX9T = new System.Windows.Forms.Label();
		this.lbBTEX8 = new System.Windows.Forms.Label();
		this.lbBTEX8T = new System.Windows.Forms.Label();
		this.lbNMHC = new System.Windows.Forms.Label();
		this.lbBTEX = new System.Windows.Forms.Label();
		this.lbNMHCT = new System.Windows.Forms.Label();
		this.lbBTEXt = new System.Windows.Forms.Label();
		this.lbBTEX7 = new System.Windows.Forms.Label();
		this.lbBTEX7T = new System.Windows.Forms.Label();
		this.lbBTEX5 = new System.Windows.Forms.Label();
		this.lbBTEX6 = new System.Windows.Forms.Label();
		this.lbBTEX4 = new System.Windows.Forms.Label();
		this.lbBTEX5T = new System.Windows.Forms.Label();
		this.lbBTEX6T = new System.Windows.Forms.Label();
		this.lbBTEX4T = new System.Windows.Forms.Label();
		this.lbBTEX2 = new System.Windows.Forms.Label();
		this.lbBTEX3 = new System.Windows.Forms.Label();
		this.lbBTEX1 = new System.Windows.Forms.Label();
		this.lbBTEX2T = new System.Windows.Forms.Label();
		this.lbBTEX3T = new System.Windows.Forms.Label();
		this.lbBTEX1T = new System.Windows.Forms.Label();
		this.lbCH4 = new System.Windows.Forms.Label();
		this.label79 = new System.Windows.Forms.Label();
		this.lbTHC = new System.Windows.Forms.Label();
		this.label77 = new System.Windows.Forms.Label();
		this.label1 = new System.Windows.Forms.Label();
		this.labAnaTimes = new System.Windows.Forms.Label();
		this.Fire1 = new System.Windows.Forms.Button();
		this.Fire2 = new System.Windows.Forms.Button();
		this.labGSM = new System.Windows.Forms.Label();
		this.gbBenXW = new System.Windows.Forms.GroupBox();
		this.gbSignal2 = new System.Windows.Forms.GroupBox();
		this.tbTime2 = new System.Windows.Forms.TextBox();
		this.txsignal2 = new System.Windows.Forms.TextBox();
		this.btnDes = new System.Windows.Forms.Button();
		this.btnData = new System.Windows.Forms.Button();
		this.label3 = new System.Windows.Forms.Label();
		this.lbBXWDanWei = new System.Windows.Forms.Label();
		this.gbNHMC1 = new System.Windows.Forms.GroupBox();
		this.groupBox1 = new System.Windows.Forms.GroupBox();
		this.tbTime = new System.Windows.Forms.TextBox();
		this.txsignal1 = new System.Windows.Forms.TextBox();
		this.label2 = new System.Windows.Forms.Label();
		this.lbNMHCDanWei = new System.Windows.Forms.Label();
		this.imageList1 = new System.Windows.Forms.ImageList(this.components);
		this.labFireState = new System.Windows.Forms.Label();
		this.pictureBox1 = new System.Windows.Forms.PictureBox();
		this.sctForm = new System.Windows.Forms.SplitContainer();
		this.sctMain = new System.Windows.Forms.SplitContainer();
		this.sctSetting = new System.Windows.Forms.SplitContainer();
		this.label19 = new System.Windows.Forms.Label();
		this.label18 = new System.Windows.Forms.Label();
		this.label17 = new System.Windows.Forms.Label();
		this.label16 = new System.Windows.Forms.Label();
		this.label15 = new System.Windows.Forms.Label();
		this.pB5 = new System.Windows.Forms.PictureBox();
		this.pB4 = new System.Windows.Forms.PictureBox();
		this.pB3 = new System.Windows.Forms.PictureBox();
		this.pB2 = new System.Windows.Forms.PictureBox();
		this.pB1 = new System.Windows.Forms.PictureBox();
		this.gbBenXW.SuspendLayout();
		this.gbSignal2.SuspendLayout();
		this.gbNHMC1.SuspendLayout();
		this.groupBox1.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.pictureBox1).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.sctForm).BeginInit();
		this.sctForm.Panel1.SuspendLayout();
		this.sctForm.Panel2.SuspendLayout();
		this.sctForm.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.sctMain).BeginInit();
		this.sctMain.Panel1.SuspendLayout();
		this.sctMain.Panel2.SuspendLayout();
		this.sctMain.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.sctSetting).BeginInit();
		this.sctSetting.Panel1.SuspendLayout();
		this.sctSetting.Panel2.SuspendLayout();
		this.sctSetting.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.pB5).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.pB4).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.pB3).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.pB2).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.pB1).BeginInit();
		base.SuspendLayout();
		this.button1.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.button1.Font = new System.Drawing.Font("宋体", 18f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		this.button1.Location = new System.Drawing.Point(984, 6);
		this.button1.Name = "button1";
		this.button1.Size = new System.Drawing.Size(187, 57);
		this.button1.TabIndex = 0;
		this.button1.Text = "参数设置";
		this.button1.UseVisualStyleBackColor = true;
		this.button1.Click += new System.EventHandler(button1_Click);
		this.lbBTEX9.AutoSize = true;
		this.lbBTEX9.BackColor = System.Drawing.Color.Cyan;
		this.lbBTEX9.Cursor = System.Windows.Forms.Cursors.Hand;
		this.lbBTEX9.Font = new System.Drawing.Font("宋体", 24f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		this.lbBTEX9.ForeColor = System.Drawing.Color.Blue;
		this.lbBTEX9.Location = new System.Drawing.Point(207, 284);
		this.lbBTEX9.Name = "lbBTEX9";
		this.lbBTEX9.Size = new System.Drawing.Size(95, 33);
		this.lbBTEX9.TabIndex = 53;
		this.lbBTEX9.Text = "0.000";
		this.lbBTEX9.TextAlign = System.Drawing.ContentAlignment.TopRight;
		this.lbBTEX9.Click += new System.EventHandler(lbBTEX9_Click);
		this.lbBTEX9T.AutoSize = true;
		this.lbBTEX9T.Font = new System.Drawing.Font("宋体", 24f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		this.lbBTEX9T.ForeColor = System.Drawing.Color.White;
		this.lbBTEX9T.Location = new System.Drawing.Point(12, 284);
		this.lbBTEX9T.Name = "lbBTEX9T";
		this.lbBTEX9T.Size = new System.Drawing.Size(127, 33);
		this.lbBTEX9T.TabIndex = 52;
		this.lbBTEX9T.Text = "组份9：";
		this.lbBTEX8.AutoSize = true;
		this.lbBTEX8.BackColor = System.Drawing.Color.Cyan;
		this.lbBTEX8.Cursor = System.Windows.Forms.Cursors.Hand;
		this.lbBTEX8.Font = new System.Drawing.Font("宋体", 24f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		this.lbBTEX8.ForeColor = System.Drawing.Color.Blue;
		this.lbBTEX8.Location = new System.Drawing.Point(590, 223);
		this.lbBTEX8.Name = "lbBTEX8";
		this.lbBTEX8.Size = new System.Drawing.Size(95, 33);
		this.lbBTEX8.TabIndex = 51;
		this.lbBTEX8.Text = "0.000";
		this.lbBTEX8.TextAlign = System.Drawing.ContentAlignment.TopRight;
		this.lbBTEX8.Click += new System.EventHandler(lbBTEX8_Click);
		this.lbBTEX8T.AutoSize = true;
		this.lbBTEX8T.Font = new System.Drawing.Font("宋体", 24f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		this.lbBTEX8T.ForeColor = System.Drawing.Color.White;
		this.lbBTEX8T.Location = new System.Drawing.Point(400, 223);
		this.lbBTEX8T.Name = "lbBTEX8T";
		this.lbBTEX8T.Size = new System.Drawing.Size(127, 33);
		this.lbBTEX8T.TabIndex = 50;
		this.lbBTEX8T.Text = "组份8：";
		this.lbNMHC.AutoSize = true;
		this.lbNMHC.BackColor = System.Drawing.Color.Cyan;
		this.lbNMHC.Cursor = System.Windows.Forms.Cursors.Hand;
		this.lbNMHC.Font = new System.Drawing.Font("宋体", 26.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		this.lbNMHC.ForeColor = System.Drawing.Color.Blue;
		this.lbNMHC.Location = new System.Drawing.Point(248, 340);
		this.lbNMHC.Name = "lbNMHC";
		this.lbNMHC.Size = new System.Drawing.Size(87, 35);
		this.lbNMHC.TabIndex = 49;
		this.lbNMHC.Text = "0.00";
		this.lbNMHC.Click += new System.EventHandler(lbNMHC_Click);
		this.lbBTEX.AutoSize = true;
		this.lbBTEX.BackColor = System.Drawing.Color.Cyan;
		this.lbBTEX.Cursor = System.Windows.Forms.Cursors.Hand;
		this.lbBTEX.Font = new System.Drawing.Font("宋体", 26.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		this.lbBTEX.ForeColor = System.Drawing.Color.Blue;
		this.lbBTEX.Location = new System.Drawing.Point(207, 340);
		this.lbBTEX.Name = "lbBTEX";
		this.lbBTEX.Size = new System.Drawing.Size(87, 35);
		this.lbBTEX.TabIndex = 48;
		this.lbBTEX.Text = "0.00";
		this.lbBTEX.Click += new System.EventHandler(lbBTEX_Click);
		this.lbNMHCT.AutoSize = true;
		this.lbNMHCT.Font = new System.Drawing.Font("宋体", 26.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		this.lbNMHCT.ForeColor = System.Drawing.Color.White;
		this.lbNMHCT.Location = new System.Drawing.Point(7, 340);
		this.lbNMHCT.Name = "lbNMHCT";
		this.lbNMHCT.Size = new System.Drawing.Size(225, 35);
		this.lbNMHCT.TabIndex = 47;
		this.lbNMHCT.Text = "非甲烷总烃：";
		this.lbBTEXt.AutoSize = true;
		this.lbBTEXt.Font = new System.Drawing.Font("宋体", 26.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		this.lbBTEXt.ForeColor = System.Drawing.Color.White;
		this.lbBTEXt.Location = new System.Drawing.Point(6, 340);
		this.lbBTEXt.Name = "lbBTEXt";
		this.lbBTEXt.Size = new System.Drawing.Size(155, 35);
		this.lbBTEXt.TabIndex = 46;
		this.lbBTEXt.Text = "苯系物：";
		this.lbBTEX7.AutoSize = true;
		this.lbBTEX7.BackColor = System.Drawing.Color.Cyan;
		this.lbBTEX7.Cursor = System.Windows.Forms.Cursors.Hand;
		this.lbBTEX7.Font = new System.Drawing.Font("宋体", 24f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		this.lbBTEX7.ForeColor = System.Drawing.Color.Blue;
		this.lbBTEX7.Location = new System.Drawing.Point(207, 223);
		this.lbBTEX7.Name = "lbBTEX7";
		this.lbBTEX7.Size = new System.Drawing.Size(95, 33);
		this.lbBTEX7.TabIndex = 45;
		this.lbBTEX7.Text = "0.000";
		this.lbBTEX7.TextAlign = System.Drawing.ContentAlignment.TopRight;
		this.lbBTEX7.Click += new System.EventHandler(lbBTEX7_Click);
		this.lbBTEX7T.AutoSize = true;
		this.lbBTEX7T.Font = new System.Drawing.Font("宋体", 24f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		this.lbBTEX7T.ForeColor = System.Drawing.Color.White;
		this.lbBTEX7T.Location = new System.Drawing.Point(12, 223);
		this.lbBTEX7T.Name = "lbBTEX7T";
		this.lbBTEX7T.Size = new System.Drawing.Size(127, 33);
		this.lbBTEX7T.TabIndex = 44;
		this.lbBTEX7T.Text = "组份7：";
		this.lbBTEX5.AutoSize = true;
		this.lbBTEX5.BackColor = System.Drawing.Color.Cyan;
		this.lbBTEX5.Cursor = System.Windows.Forms.Cursors.Hand;
		this.lbBTEX5.Font = new System.Drawing.Font("宋体", 24f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		this.lbBTEX5.ForeColor = System.Drawing.Color.Blue;
		this.lbBTEX5.Location = new System.Drawing.Point(207, 159);
		this.lbBTEX5.Name = "lbBTEX5";
		this.lbBTEX5.Size = new System.Drawing.Size(95, 33);
		this.lbBTEX5.TabIndex = 43;
		this.lbBTEX5.Text = "0.000";
		this.lbBTEX5.TextAlign = System.Drawing.ContentAlignment.TopRight;
		this.lbBTEX5.Click += new System.EventHandler(lbBTEX5_Click);
		this.lbBTEX6.AutoSize = true;
		this.lbBTEX6.BackColor = System.Drawing.Color.Cyan;
		this.lbBTEX6.Cursor = System.Windows.Forms.Cursors.Hand;
		this.lbBTEX6.Font = new System.Drawing.Font("宋体", 24f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		this.lbBTEX6.ForeColor = System.Drawing.Color.Blue;
		this.lbBTEX6.Location = new System.Drawing.Point(590, 159);
		this.lbBTEX6.Name = "lbBTEX6";
		this.lbBTEX6.Size = new System.Drawing.Size(95, 33);
		this.lbBTEX6.TabIndex = 42;
		this.lbBTEX6.Text = "0.000";
		this.lbBTEX6.TextAlign = System.Drawing.ContentAlignment.TopRight;
		this.lbBTEX6.Click += new System.EventHandler(lbBTEX6_Click);
		this.lbBTEX4.AutoSize = true;
		this.lbBTEX4.BackColor = System.Drawing.Color.Cyan;
		this.lbBTEX4.Cursor = System.Windows.Forms.Cursors.Hand;
		this.lbBTEX4.Font = new System.Drawing.Font("宋体", 24f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		this.lbBTEX4.ForeColor = System.Drawing.Color.Blue;
		this.lbBTEX4.Location = new System.Drawing.Point(590, 99);
		this.lbBTEX4.Name = "lbBTEX4";
		this.lbBTEX4.Size = new System.Drawing.Size(95, 33);
		this.lbBTEX4.TabIndex = 41;
		this.lbBTEX4.Text = "0.000";
		this.lbBTEX4.TextAlign = System.Drawing.ContentAlignment.TopRight;
		this.lbBTEX4.Click += new System.EventHandler(lbBTEX4_Click);
		this.lbBTEX5T.AutoSize = true;
		this.lbBTEX5T.Font = new System.Drawing.Font("宋体", 24f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		this.lbBTEX5T.ForeColor = System.Drawing.Color.White;
		this.lbBTEX5T.Location = new System.Drawing.Point(12, 159);
		this.lbBTEX5T.Name = "lbBTEX5T";
		this.lbBTEX5T.Size = new System.Drawing.Size(127, 33);
		this.lbBTEX5T.TabIndex = 40;
		this.lbBTEX5T.Text = "组份5：";
		this.lbBTEX5T.Click += new System.EventHandler(lbBTEX5T_Click);
		this.lbBTEX6T.AutoSize = true;
		this.lbBTEX6T.Font = new System.Drawing.Font("宋体", 24f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		this.lbBTEX6T.ForeColor = System.Drawing.Color.White;
		this.lbBTEX6T.Location = new System.Drawing.Point(400, 159);
		this.lbBTEX6T.Name = "lbBTEX6T";
		this.lbBTEX6T.Size = new System.Drawing.Size(127, 33);
		this.lbBTEX6T.TabIndex = 39;
		this.lbBTEX6T.Text = "组份6：";
		this.lbBTEX4T.AutoSize = true;
		this.lbBTEX4T.Font = new System.Drawing.Font("宋体", 24f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		this.lbBTEX4T.ForeColor = System.Drawing.Color.White;
		this.lbBTEX4T.Location = new System.Drawing.Point(400, 99);
		this.lbBTEX4T.Name = "lbBTEX4T";
		this.lbBTEX4T.Size = new System.Drawing.Size(207, 33);
		this.lbBTEX4T.TabIndex = 38;
		this.lbBTEX4T.Text = "间对二甲苯：";
		this.lbBTEX2.AutoSize = true;
		this.lbBTEX2.BackColor = System.Drawing.Color.Cyan;
		this.lbBTEX2.Cursor = System.Windows.Forms.Cursors.Hand;
		this.lbBTEX2.Font = new System.Drawing.Font("宋体", 24f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		this.lbBTEX2.ForeColor = System.Drawing.Color.Blue;
		this.lbBTEX2.Location = new System.Drawing.Point(590, 34);
		this.lbBTEX2.Name = "lbBTEX2";
		this.lbBTEX2.Size = new System.Drawing.Size(95, 33);
		this.lbBTEX2.TabIndex = 37;
		this.lbBTEX2.Text = "0.000";
		this.lbBTEX2.TextAlign = System.Drawing.ContentAlignment.TopRight;
		this.lbBTEX2.Click += new System.EventHandler(lbBTEX2_Click);
		this.lbBTEX3.AutoSize = true;
		this.lbBTEX3.BackColor = System.Drawing.Color.Cyan;
		this.lbBTEX3.Cursor = System.Windows.Forms.Cursors.Hand;
		this.lbBTEX3.Font = new System.Drawing.Font("宋体", 24f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		this.lbBTEX3.ForeColor = System.Drawing.Color.Blue;
		this.lbBTEX3.Location = new System.Drawing.Point(207, 99);
		this.lbBTEX3.Name = "lbBTEX3";
		this.lbBTEX3.Size = new System.Drawing.Size(95, 33);
		this.lbBTEX3.TabIndex = 36;
		this.lbBTEX3.Text = "0.000";
		this.lbBTEX3.TextAlign = System.Drawing.ContentAlignment.TopRight;
		this.lbBTEX3.Click += new System.EventHandler(lbBTEX3_Click);
		this.lbBTEX1.AutoSize = true;
		this.lbBTEX1.BackColor = System.Drawing.Color.Cyan;
		this.lbBTEX1.Cursor = System.Windows.Forms.Cursors.Hand;
		this.lbBTEX1.Font = new System.Drawing.Font("宋体", 24f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		this.lbBTEX1.ForeColor = System.Drawing.Color.Blue;
		this.lbBTEX1.Location = new System.Drawing.Point(207, 34);
		this.lbBTEX1.Name = "lbBTEX1";
		this.lbBTEX1.Size = new System.Drawing.Size(95, 33);
		this.lbBTEX1.TabIndex = 35;
		this.lbBTEX1.Text = "0.000";
		this.lbBTEX1.TextAlign = System.Drawing.ContentAlignment.TopRight;
		this.lbBTEX1.Click += new System.EventHandler(lbBTEX1_Click);
		this.lbBTEX2T.AutoSize = true;
		this.lbBTEX2T.Font = new System.Drawing.Font("宋体", 24f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		this.lbBTEX2T.ForeColor = System.Drawing.Color.White;
		this.lbBTEX2T.Location = new System.Drawing.Point(400, 34);
		this.lbBTEX2T.Name = "lbBTEX2T";
		this.lbBTEX2T.Size = new System.Drawing.Size(127, 33);
		this.lbBTEX2T.TabIndex = 34;
		this.lbBTEX2T.Text = "组份2：";
		this.lbBTEX3T.AutoSize = true;
		this.lbBTEX3T.Font = new System.Drawing.Font("宋体", 24f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		this.lbBTEX3T.ForeColor = System.Drawing.Color.White;
		this.lbBTEX3T.Location = new System.Drawing.Point(12, 99);
		this.lbBTEX3T.Name = "lbBTEX3T";
		this.lbBTEX3T.Size = new System.Drawing.Size(191, 33);
		this.lbBTEX3T.TabIndex = 33;
		this.lbBTEX3T.Text = "间对二甲苯:";
		this.lbBTEX1T.AutoSize = true;
		this.lbBTEX1T.Font = new System.Drawing.Font("宋体", 24f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		this.lbBTEX1T.ForeColor = System.Drawing.Color.White;
		this.lbBTEX1T.Location = new System.Drawing.Point(12, 34);
		this.lbBTEX1T.Name = "lbBTEX1T";
		this.lbBTEX1T.Size = new System.Drawing.Size(127, 33);
		this.lbBTEX1T.TabIndex = 32;
		this.lbBTEX1T.Text = "组份1：";
		this.lbCH4.BackColor = System.Drawing.Color.Cyan;
		this.lbCH4.Cursor = System.Windows.Forms.Cursors.Hand;
		this.lbCH4.Font = new System.Drawing.Font("宋体", 24f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		this.lbCH4.ForeColor = System.Drawing.Color.Blue;
		this.lbCH4.Location = new System.Drawing.Point(248, 113);
		this.lbCH4.Name = "lbCH4";
		this.lbCH4.Size = new System.Drawing.Size(143, 33);
		this.lbCH4.TabIndex = 31;
		this.lbCH4.Text = "0.000";
		this.lbCH4.TextAlign = System.Drawing.ContentAlignment.TopRight;
		this.lbCH4.Click += new System.EventHandler(lbCH4_Click);
		this.label79.AutoSize = true;
		this.label79.Font = new System.Drawing.Font("宋体", 24f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		this.label79.ForeColor = System.Drawing.Color.White;
		this.label79.Location = new System.Drawing.Point(4, 113);
		this.label79.Name = "label79";
		this.label79.Size = new System.Drawing.Size(207, 33);
		this.label79.TabIndex = 30;
		this.label79.Text = "甲      烷：";
		this.lbTHC.BackColor = System.Drawing.Color.Cyan;
		this.lbTHC.Cursor = System.Windows.Forms.Cursors.Hand;
		this.lbTHC.Font = new System.Drawing.Font("宋体", 24f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		this.lbTHC.ForeColor = System.Drawing.Color.Blue;
		this.lbTHC.Location = new System.Drawing.Point(248, 41);
		this.lbTHC.Name = "lbTHC";
		this.lbTHC.Size = new System.Drawing.Size(143, 33);
		this.lbTHC.TabIndex = 29;
		this.lbTHC.Text = "0.000";
		this.lbTHC.TextAlign = System.Drawing.ContentAlignment.TopRight;
		this.lbTHC.Click += new System.EventHandler(lbTHC_Click);
		this.label77.AutoSize = true;
		this.label77.Font = new System.Drawing.Font("宋体", 24f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		this.label77.ForeColor = System.Drawing.Color.White;
		this.label77.Location = new System.Drawing.Point(4, 41);
		this.label77.Name = "label77";
		this.label77.Size = new System.Drawing.Size(207, 33);
		this.label77.TabIndex = 28;
		this.label77.Text = "总      烃：";
		this.label1.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.label1.AutoSize = true;
		this.label1.Font = new System.Drawing.Font("宋体", 18f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		this.label1.ForeColor = System.Drawing.Color.White;
		this.label1.Location = new System.Drawing.Point(7, 36);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(130, 24);
		this.label1.TabIndex = 54;
		this.label1.Text = "运行次数：";
		this.labAnaTimes.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.labAnaTimes.AutoSize = true;
		this.labAnaTimes.Font = new System.Drawing.Font("宋体", 26.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		this.labAnaTimes.ForeColor = System.Drawing.Color.Cyan;
		this.labAnaTimes.Location = new System.Drawing.Point(156, 28);
		this.labAnaTimes.Name = "labAnaTimes";
		this.labAnaTimes.Size = new System.Drawing.Size(33, 35);
		this.labAnaTimes.TabIndex = 55;
		this.labAnaTimes.Text = "0";
		this.Fire1.Image = (System.Drawing.Image)resources.GetObject("Fire1.Image");
		this.Fire1.Location = new System.Drawing.Point(1, 13);
		this.Fire1.Name = "Fire1";
		this.Fire1.Size = new System.Drawing.Size(44, 37);
		this.Fire1.TabIndex = 56;
		this.Fire1.UseVisualStyleBackColor = true;
		this.Fire2.Image = (System.Drawing.Image)resources.GetObject("Fire2.Image");
		this.Fire2.Location = new System.Drawing.Point(6, 18);
		this.Fire2.Name = "Fire2";
		this.Fire2.Size = new System.Drawing.Size(43, 37);
		this.Fire2.TabIndex = 57;
		this.Fire2.UseVisualStyleBackColor = true;
		this.Fire2.Visible = false;
		this.labGSM.Dock = System.Windows.Forms.DockStyle.Fill;
		this.labGSM.Font = new System.Drawing.Font("宋体", 42f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 134);
		this.labGSM.Location = new System.Drawing.Point(101, 0);
		this.labGSM.Name = "labGSM";
		this.labGSM.Size = new System.Drawing.Size(1091, 88);
		this.labGSM.TabIndex = 59;
		this.labGSM.Text = "VOC在线监测系统";
		this.labGSM.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
		this.gbBenXW.BackColor = System.Drawing.Color.FromArgb(0, 64, 64);
		this.gbBenXW.Controls.Add(this.gbSignal2);
		this.gbBenXW.Controls.Add(this.btnDes);
		this.gbBenXW.Controls.Add(this.btnData);
		this.gbBenXW.Controls.Add(this.label3);
		this.gbBenXW.Controls.Add(this.lbBXWDanWei);
		this.gbBenXW.Controls.Add(this.lbBTEX9T);
		this.gbBenXW.Controls.Add(this.lbBTEX1T);
		this.gbBenXW.Controls.Add(this.lbBTEX3T);
		this.gbBenXW.Controls.Add(this.lbBTEX1);
		this.gbBenXW.Controls.Add(this.lbBTEX3);
		this.gbBenXW.Controls.Add(this.lbBTEX5T);
		this.gbBenXW.Controls.Add(this.lbBTEX8);
		this.gbBenXW.Controls.Add(this.lbBTEX);
		this.gbBenXW.Controls.Add(this.lbBTEX9);
		this.gbBenXW.Controls.Add(this.lbBTEXt);
		this.gbBenXW.Controls.Add(this.lbBTEX8T);
		this.gbBenXW.Controls.Add(this.lbBTEX5);
		this.gbBenXW.Controls.Add(this.lbBTEX7T);
		this.gbBenXW.Controls.Add(this.lbBTEX7);
		this.gbBenXW.Controls.Add(this.lbBTEX2T);
		this.gbBenXW.Controls.Add(this.lbBTEX6);
		this.gbBenXW.Controls.Add(this.lbBTEX2);
		this.gbBenXW.Controls.Add(this.lbBTEX4);
		this.gbBenXW.Controls.Add(this.lbBTEX4T);
		this.gbBenXW.Controls.Add(this.lbBTEX6T);
		this.gbBenXW.Dock = System.Windows.Forms.DockStyle.Fill;
		this.gbBenXW.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.gbBenXW.Location = new System.Drawing.Point(0, 0);
		this.gbBenXW.Name = "gbBenXW";
		this.gbBenXW.Size = new System.Drawing.Size(783, 565);
		this.gbBenXW.TabIndex = 60;
		this.gbBenXW.TabStop = false;
		this.gbSignal2.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.gbSignal2.Controls.Add(this.tbTime2);
		this.gbSignal2.Controls.Add(this.txsignal2);
		this.gbSignal2.Controls.Add(this.Fire2);
		this.gbSignal2.ForeColor = System.Drawing.Color.White;
		this.gbSignal2.Location = new System.Drawing.Point(12, 496);
		this.gbSignal2.Name = "gbSignal2";
		this.gbSignal2.Size = new System.Drawing.Size(268, 66);
		this.gbSignal2.TabIndex = 69;
		this.gbSignal2.TabStop = false;
		this.gbSignal2.Text = "通道2";
		this.tbTime2.Font = new System.Drawing.Font("宋体", 18f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		this.tbTime2.Location = new System.Drawing.Point(161, 20);
		this.tbTime2.Name = "tbTime2";
		this.tbTime2.Size = new System.Drawing.Size(100, 35);
		this.tbTime2.TabIndex = 76;
		this.txsignal2.Font = new System.Drawing.Font("宋体", 18f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		this.txsignal2.Location = new System.Drawing.Point(55, 20);
		this.txsignal2.Name = "txsignal2";
		this.txsignal2.Size = new System.Drawing.Size(100, 35);
		this.txsignal2.TabIndex = 63;
		this.txsignal2.Visible = false;
		this.btnDes.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.btnDes.Font = new System.Drawing.Font("宋体", 18f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		this.btnDes.Location = new System.Drawing.Point(372, 508);
		this.btnDes.Name = "btnDes";
		this.btnDes.Size = new System.Drawing.Size(187, 51);
		this.btnDes.TabIndex = 68;
		this.btnDes.Text = "显示桌面";
		this.btnDes.UseVisualStyleBackColor = true;
		this.btnDes.Click += new System.EventHandler(btnDes_Click);
		this.btnData.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.btnData.Font = new System.Drawing.Font("宋体", 18f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		this.btnData.Location = new System.Drawing.Point(575, 508);
		this.btnData.Name = "btnData";
		this.btnData.Size = new System.Drawing.Size(187, 51);
		this.btnData.TabIndex = 67;
		this.btnData.Text = "数据查询";
		this.btnData.UseVisualStyleBackColor = true;
		this.btnData.Click += new System.EventHandler(btnData_Click);
		this.label3.AutoSize = true;
		this.label3.Font = new System.Drawing.Font("宋体", 14.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		this.label3.ForeColor = System.Drawing.Color.White;
		this.label3.Location = new System.Drawing.Point(505, 449);
		this.label3.Name = "label3";
		this.label3.Size = new System.Drawing.Size(66, 19);
		this.label3.TabIndex = 65;
		this.label3.Text = "单位：";
		this.label3.Visible = false;
		this.lbBXWDanWei.AutoSize = true;
		this.lbBXWDanWei.BackColor = System.Drawing.Color.Cyan;
		this.lbBXWDanWei.Cursor = System.Windows.Forms.Cursors.Hand;
		this.lbBXWDanWei.Font = new System.Drawing.Font("宋体", 14.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		this.lbBXWDanWei.ForeColor = System.Drawing.Color.Blue;
		this.lbBXWDanWei.Location = new System.Drawing.Point(571, 449);
		this.lbBXWDanWei.Name = "lbBXWDanWei";
		this.lbBXWDanWei.Size = new System.Drawing.Size(39, 19);
		this.lbBXWDanWei.TabIndex = 66;
		this.lbBXWDanWei.Text = "ppm";
		this.lbBXWDanWei.Visible = false;
		this.gbNHMC1.BackColor = System.Drawing.Color.FromArgb(0, 64, 64);
		this.gbNHMC1.Controls.Add(this.groupBox1);
		this.gbNHMC1.Controls.Add(this.label79);
		this.gbNHMC1.Controls.Add(this.label77);
		this.gbNHMC1.Controls.Add(this.lbTHC);
		this.gbNHMC1.Controls.Add(this.lbCH4);
		this.gbNHMC1.Controls.Add(this.lbNMHCT);
		this.gbNHMC1.Controls.Add(this.lbNMHC);
		this.gbNHMC1.Dock = System.Windows.Forms.DockStyle.Fill;
		this.gbNHMC1.Location = new System.Drawing.Point(0, 0);
		this.gbNHMC1.Name = "gbNHMC1";
		this.gbNHMC1.Size = new System.Drawing.Size(399, 565);
		this.gbNHMC1.TabIndex = 61;
		this.gbNHMC1.TabStop = false;
		this.groupBox1.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.groupBox1.Controls.Add(this.tbTime);
		this.groupBox1.Controls.Add(this.txsignal1);
		this.groupBox1.Controls.Add(this.Fire1);
		this.groupBox1.ForeColor = System.Drawing.Color.White;
		this.groupBox1.Location = new System.Drawing.Point(13, 501);
		this.groupBox1.Name = "groupBox1";
		this.groupBox1.Size = new System.Drawing.Size(263, 58);
		this.groupBox1.TabIndex = 70;
		this.groupBox1.TabStop = false;
		this.groupBox1.Text = "通道1";
		this.tbTime.Font = new System.Drawing.Font("宋体", 18f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		this.tbTime.Location = new System.Drawing.Point(157, 15);
		this.tbTime.Name = "tbTime";
		this.tbTime.Size = new System.Drawing.Size(100, 35);
		this.tbTime.TabIndex = 75;
		this.txsignal1.Font = new System.Drawing.Font("宋体", 18f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		this.txsignal1.Location = new System.Drawing.Point(51, 15);
		this.txsignal1.Name = "txsignal1";
		this.txsignal1.Size = new System.Drawing.Size(100, 35);
		this.txsignal1.TabIndex = 62;
		this.label2.AutoSize = true;
		this.label2.Font = new System.Drawing.Font("宋体", 14.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		this.label2.ForeColor = System.Drawing.Color.White;
		this.label2.Location = new System.Drawing.Point(863, 46);
		this.label2.Name = "label2";
		this.label2.Size = new System.Drawing.Size(66, 19);
		this.label2.TabIndex = 63;
		this.label2.Text = "单位：";
		this.lbNMHCDanWei.AutoSize = true;
		this.lbNMHCDanWei.BackColor = System.Drawing.Color.Cyan;
		this.lbNMHCDanWei.Cursor = System.Windows.Forms.Cursors.Hand;
		this.lbNMHCDanWei.Font = new System.Drawing.Font("宋体", 14.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		this.lbNMHCDanWei.ForeColor = System.Drawing.Color.Blue;
		this.lbNMHCDanWei.Location = new System.Drawing.Point(929, 46);
		this.lbNMHCDanWei.Name = "lbNMHCDanWei";
		this.lbNMHCDanWei.Size = new System.Drawing.Size(39, 19);
		this.lbNMHCDanWei.TabIndex = 64;
		this.lbNMHCDanWei.Text = "ppm";
		this.imageList1.ImageStream = (System.Windows.Forms.ImageListStreamer)resources.GetObject("imageList1.ImageStream");
		this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
		this.imageList1.Images.SetKeyName(0, "点火1.png");
		this.imageList1.Images.SetKeyName(1, "点火2.png");
		this.labFireState.AutoSize = true;
		this.labFireState.Font = new System.Drawing.Font("宋体", 24f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		this.labFireState.Location = new System.Drawing.Point(335, 1);
		this.labFireState.Name = "labFireState";
		this.labFireState.Size = new System.Drawing.Size(31, 33);
		this.labFireState.TabIndex = 64;
		this.labFireState.Text = "0";
		this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
		this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Left;
		this.pictureBox1.Location = new System.Drawing.Point(0, 0);
		this.pictureBox1.Name = "pictureBox1";
		this.pictureBox1.Size = new System.Drawing.Size(101, 88);
		this.pictureBox1.TabIndex = 65;
		this.pictureBox1.TabStop = false;
		this.sctForm.Dock = System.Windows.Forms.DockStyle.Fill;
		this.sctForm.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
		this.sctForm.Location = new System.Drawing.Point(0, 0);
		this.sctForm.Name = "sctForm";
		this.sctForm.Orientation = System.Windows.Forms.Orientation.Horizontal;
		this.sctForm.Panel1.Controls.Add(this.labGSM);
		this.sctForm.Panel1.Controls.Add(this.pictureBox1);
		this.sctForm.Panel2.Controls.Add(this.sctMain);
		this.sctForm.Size = new System.Drawing.Size(1192, 726);
		this.sctForm.SplitterDistance = 88;
		this.sctForm.SplitterWidth = 2;
		this.sctForm.TabIndex = 66;
		this.sctMain.Dock = System.Windows.Forms.DockStyle.Fill;
		this.sctMain.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
		this.sctMain.Location = new System.Drawing.Point(0, 0);
		this.sctMain.Name = "sctMain";
		this.sctMain.Orientation = System.Windows.Forms.Orientation.Horizontal;
		this.sctMain.Panel1.Controls.Add(this.sctSetting);
		this.sctMain.Panel2.Controls.Add(this.label19);
		this.sctMain.Panel2.Controls.Add(this.label18);
		this.sctMain.Panel2.Controls.Add(this.label17);
		this.sctMain.Panel2.Controls.Add(this.label16);
		this.sctMain.Panel2.Controls.Add(this.label15);
		this.sctMain.Panel2.Controls.Add(this.pB5);
		this.sctMain.Panel2.Controls.Add(this.pB4);
		this.sctMain.Panel2.Controls.Add(this.pB3);
		this.sctMain.Panel2.Controls.Add(this.pB2);
		this.sctMain.Panel2.Controls.Add(this.pB1);
		this.sctMain.Panel2.Controls.Add(this.label2);
		this.sctMain.Panel2.Controls.Add(this.lbNMHCDanWei);
		this.sctMain.Panel2.Controls.Add(this.labFireState);
		this.sctMain.Panel2.Controls.Add(this.label1);
		this.sctMain.Panel2.Controls.Add(this.button1);
		this.sctMain.Panel2.Controls.Add(this.labAnaTimes);
		this.sctMain.Size = new System.Drawing.Size(1192, 636);
		this.sctMain.SplitterDistance = 565;
		this.sctMain.SplitterWidth = 2;
		this.sctMain.TabIndex = 0;
		this.sctSetting.Dock = System.Windows.Forms.DockStyle.Fill;
		this.sctSetting.IsSplitterFixed = true;
		this.sctSetting.Location = new System.Drawing.Point(0, 0);
		this.sctSetting.Name = "sctSetting";
		this.sctSetting.Panel1.Controls.Add(this.gbNHMC1);
		this.sctSetting.Panel2.Controls.Add(this.gbBenXW);
		this.sctSetting.Size = new System.Drawing.Size(1192, 565);
		this.sctSetting.SplitterDistance = 399;
		this.sctSetting.SplitterWidth = 10;
		this.sctSetting.TabIndex = 0;
		this.label19.AutoSize = true;
		this.label19.ForeColor = System.Drawing.Color.White;
		this.label19.Location = new System.Drawing.Point(536, 55);
		this.label19.Name = "label19";
		this.label19.Size = new System.Drawing.Size(29, 12);
		this.label19.TabIndex = 70;
		this.label19.Text = "反吹";
		this.label18.AutoSize = true;
		this.label18.ForeColor = System.Drawing.Color.White;
		this.label18.Location = new System.Drawing.Point(491, 55);
		this.label18.Name = "label18";
		this.label18.Size = new System.Drawing.Size(29, 12);
		this.label18.TabIndex = 71;
		this.label18.Text = "进样";
		this.label17.AutoSize = true;
		this.label17.ForeColor = System.Drawing.Color.White;
		this.label17.Location = new System.Drawing.Point(440, 55);
		this.label17.Name = "label17";
		this.label17.Size = new System.Drawing.Size(29, 12);
		this.label17.TabIndex = 72;
		this.label17.Text = "备用";
		this.label16.AutoSize = true;
		this.label16.ForeColor = System.Drawing.Color.White;
		this.label16.Location = new System.Drawing.Point(389, 55);
		this.label16.Name = "label16";
		this.label16.Size = new System.Drawing.Size(29, 12);
		this.label16.TabIndex = 73;
		this.label16.Text = "反吹";
		this.label15.AutoSize = true;
		this.label15.ForeColor = System.Drawing.Color.White;
		this.label15.Location = new System.Drawing.Point(337, 55);
		this.label15.Name = "label15";
		this.label15.Size = new System.Drawing.Size(29, 12);
		this.label15.TabIndex = 74;
		this.label15.Text = "进样";
		this.pB5.Image = (System.Drawing.Image)resources.GetObject("pB5.Image");
		this.pB5.Location = new System.Drawing.Point(542, 30);
		this.pB5.Name = "pB5";
		this.pB5.Size = new System.Drawing.Size(20, 20);
		this.pB5.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
		this.pB5.TabIndex = 65;
		this.pB5.TabStop = false;
		this.pB4.Image = (System.Drawing.Image)resources.GetObject("pB4.Image");
		this.pB4.Location = new System.Drawing.Point(493, 30);
		this.pB4.Name = "pB4";
		this.pB4.Size = new System.Drawing.Size(20, 20);
		this.pB4.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
		this.pB4.TabIndex = 66;
		this.pB4.TabStop = false;
		this.pB3.Image = (System.Drawing.Image)resources.GetObject("pB3.Image");
		this.pB3.Location = new System.Drawing.Point(443, 30);
		this.pB3.Name = "pB3";
		this.pB3.Size = new System.Drawing.Size(20, 20);
		this.pB3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
		this.pB3.TabIndex = 67;
		this.pB3.TabStop = false;
		this.pB2.Image = (System.Drawing.Image)resources.GetObject("pB2.Image");
		this.pB2.Location = new System.Drawing.Point(391, 30);
		this.pB2.Name = "pB2";
		this.pB2.Size = new System.Drawing.Size(20, 20);
		this.pB2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
		this.pB2.TabIndex = 68;
		this.pB2.TabStop = false;
		this.pB1.Image = (System.Drawing.Image)resources.GetObject("pB1.Image");
		this.pB1.Location = new System.Drawing.Point(341, 31);
		this.pB1.Name = "pB1";
		this.pB1.Size = new System.Drawing.Size(20, 20);
		this.pB1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
		this.pB1.TabIndex = 69;
		this.pB1.TabStop = false;
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		this.BackColor = System.Drawing.Color.Teal;
		base.ClientSize = new System.Drawing.Size(1192, 726);
		base.Controls.Add(this.sctForm);
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
		base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
		base.Name = "FormVOC";
		this.Text = "FormVOC";
		base.WindowState = System.Windows.Forms.FormWindowState.Maximized;
		this.gbBenXW.ResumeLayout(false);
		this.gbBenXW.PerformLayout();
		this.gbSignal2.ResumeLayout(false);
		this.gbSignal2.PerformLayout();
		this.gbNHMC1.ResumeLayout(false);
		this.gbNHMC1.PerformLayout();
		this.groupBox1.ResumeLayout(false);
		this.groupBox1.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.pictureBox1).EndInit();
		this.sctForm.Panel1.ResumeLayout(false);
		this.sctForm.Panel2.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.sctForm).EndInit();
		this.sctForm.ResumeLayout(false);
		this.sctMain.Panel1.ResumeLayout(false);
		this.sctMain.Panel2.ResumeLayout(false);
		this.sctMain.Panel2.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.sctMain).EndInit();
		this.sctMain.ResumeLayout(false);
		this.sctSetting.Panel1.ResumeLayout(false);
		this.sctSetting.Panel2.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.sctSetting).EndInit();
		this.sctSetting.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.pB5).EndInit();
		((System.ComponentModel.ISupportInitialize)this.pB4).EndInit();
		((System.ComponentModel.ISupportInitialize)this.pB3).EndInit();
		((System.ComponentModel.ISupportInitialize)this.pB2).EndInit();
		((System.ComponentModel.ISupportInitialize)this.pB1).EndInit();
		base.ResumeLayout(false);
	}
}
