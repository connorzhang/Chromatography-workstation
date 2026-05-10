using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace IBrainChrom2018;

public class FormMineMethod : Form
{
	private IContainer components = null;

	public ToolStrip tsCali;

	private ToolStripButton btnNewCali;

	private ToolStripButton btnOpenCali;

	private ToolStripButton btnSaveCali;

	private ToolStripSeparator toolStripSeparator7;

	private ToolStripButton btnOpenChrom;

	private ToolStripButton btnCloseChrom;

	private GroupBox groupBox1;

	private GroupBox groupBox2;

	private GroupBox groupBox3;

	private TabControl tabControl1;

	private TabPage tabPage1;

	private TabPage tabPage2;

	private TabPage tabPage3;

	private GroupBox groupBox4;

	private RadioButton radioButton4;

	private RadioButton radioButton3;

	private RadioButton radioButton2;

	private RadioButton radioButton1;

	private TextBox textBox3;

	private Label label3;

	private TextBox textBox2;

	private Label label2;

	private TextBox textBox1;

	private Label label1;

	private TextBox textBox4;

	private Label label4;

	private TextBox textBox5;

	private Label label5;

	private TextBox textBox6;

	private Label label6;

	private TextBox textBox7;

	private Label label7;

	private TextBox textBox8;

	private Label label8;

	private TextBox textBox9;

	private Label label9;

	private GroupBox groupBox5;

	private DataGridView dataGridView1;

	private DataGridViewTextBoxColumn 组份号;

	private DataGridViewTextBoxColumn 组分名;

	private DataGridViewTextBoxColumn 保留时间;

	private DataGridViewTextBoxColumn 带宽;

	private DataGridViewTextBoxColumn 通道号;

	private DataGridViewTextBoxColumn 标样浓度;

	private DataGridViewTextBoxColumn 单位;

	private DataGridViewTextBoxColumn 校正因子;

	private DataGridViewTextBoxColumn 含量;

	private DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;

	private DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;

	private DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;

	private DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;

	private DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;

	private DataGridViewTextBoxColumn dataGridViewTextBoxColumn6;

	private DataGridViewTextBoxColumn dataGridViewTextBoxColumn7;

	private DataGridViewTextBoxColumn dataGridViewTextBoxColumn8;

	private DataGridViewTextBoxColumn dataGridViewTextBoxColumn9;

	private MineChromCtrl mineChromCtrl1;

	private SplitContainer splitContainer1;

	private SplitContainer splitContainer2;

	private MineChromCtrl mineChromCtrl2;

	private MineChromCtrl mineChromCtrl3;

	public FormMineMethod()
	{
		InitializeComponent();
	}

	private void btnSaveCali_Click(object sender, EventArgs e)
	{
	}

	private void btnOpenChrom_Click(object sender, EventArgs e)
	{
		OpenFileDialog openFileDialog = new OpenFileDialog();
		if (openFileDialog.ShowDialog() == DialogResult.OK)
		{
			mineChromCtrl1.OpenChrom(openFileDialog.FileName, sampling: false, useCurrent: true);
			mineChromCtrl2.OpenChrom(openFileDialog.FileName, sampling: false, useCurrent: true);
			mineChromCtrl3.OpenChrom(openFileDialog.FileName, sampling: false, useCurrent: true);
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(IBrainChrom2018.FormMineMethod));
		this.tsCali = new System.Windows.Forms.ToolStrip();
		this.btnNewCali = new System.Windows.Forms.ToolStripButton();
		this.btnOpenCali = new System.Windows.Forms.ToolStripButton();
		this.btnSaveCali = new System.Windows.Forms.ToolStripButton();
		this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
		this.btnOpenChrom = new System.Windows.Forms.ToolStripButton();
		this.btnCloseChrom = new System.Windows.Forms.ToolStripButton();
		this.groupBox1 = new System.Windows.Forms.GroupBox();
		this.groupBox4 = new System.Windows.Forms.GroupBox();
		this.radioButton4 = new System.Windows.Forms.RadioButton();
		this.radioButton3 = new System.Windows.Forms.RadioButton();
		this.groupBox3 = new System.Windows.Forms.GroupBox();
		this.radioButton2 = new System.Windows.Forms.RadioButton();
		this.radioButton1 = new System.Windows.Forms.RadioButton();
		this.groupBox2 = new System.Windows.Forms.GroupBox();
		this.tabControl1 = new System.Windows.Forms.TabControl();
		this.tabPage1 = new System.Windows.Forms.TabPage();
		this.textBox3 = new System.Windows.Forms.TextBox();
		this.label3 = new System.Windows.Forms.Label();
		this.textBox2 = new System.Windows.Forms.TextBox();
		this.label2 = new System.Windows.Forms.Label();
		this.textBox1 = new System.Windows.Forms.TextBox();
		this.label1 = new System.Windows.Forms.Label();
		this.tabPage2 = new System.Windows.Forms.TabPage();
		this.textBox4 = new System.Windows.Forms.TextBox();
		this.label4 = new System.Windows.Forms.Label();
		this.textBox5 = new System.Windows.Forms.TextBox();
		this.label5 = new System.Windows.Forms.Label();
		this.textBox6 = new System.Windows.Forms.TextBox();
		this.label6 = new System.Windows.Forms.Label();
		this.tabPage3 = new System.Windows.Forms.TabPage();
		this.textBox7 = new System.Windows.Forms.TextBox();
		this.label7 = new System.Windows.Forms.Label();
		this.textBox8 = new System.Windows.Forms.TextBox();
		this.label8 = new System.Windows.Forms.Label();
		this.textBox9 = new System.Windows.Forms.TextBox();
		this.label9 = new System.Windows.Forms.Label();
		this.groupBox5 = new System.Windows.Forms.GroupBox();
		this.dataGridView1 = new System.Windows.Forms.DataGridView();
		this.组份号 = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.组分名 = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.保留时间 = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.带宽 = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.通道号 = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.标样浓度 = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.单位 = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.校正因子 = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.含量 = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.dataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.dataGridViewTextBoxColumn6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.dataGridViewTextBoxColumn7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.dataGridViewTextBoxColumn8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.dataGridViewTextBoxColumn9 = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.mineChromCtrl1 = new IBrainChrom2018.MineChromCtrl();
		this.splitContainer1 = new System.Windows.Forms.SplitContainer();
		this.splitContainer2 = new System.Windows.Forms.SplitContainer();
		this.mineChromCtrl2 = new IBrainChrom2018.MineChromCtrl();
		this.mineChromCtrl3 = new IBrainChrom2018.MineChromCtrl();
		this.tsCali.SuspendLayout();
		this.groupBox1.SuspendLayout();
		this.groupBox4.SuspendLayout();
		this.groupBox3.SuspendLayout();
		this.groupBox2.SuspendLayout();
		this.tabControl1.SuspendLayout();
		this.tabPage1.SuspendLayout();
		this.tabPage2.SuspendLayout();
		this.tabPage3.SuspendLayout();
		this.groupBox5.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.dataGridView1).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.splitContainer1).BeginInit();
		this.splitContainer1.Panel1.SuspendLayout();
		this.splitContainer1.Panel2.SuspendLayout();
		this.splitContainer1.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.splitContainer2).BeginInit();
		this.splitContainer2.Panel1.SuspendLayout();
		this.splitContainer2.Panel2.SuspendLayout();
		this.splitContainer2.SuspendLayout();
		base.SuspendLayout();
		this.tsCali.ImageScalingSize = new System.Drawing.Size(32, 32);
		this.tsCali.Items.AddRange(new System.Windows.Forms.ToolStripItem[6] { this.btnNewCali, this.btnOpenCali, this.btnSaveCali, this.toolStripSeparator7, this.btnOpenChrom, this.btnCloseChrom });
		this.tsCali.Location = new System.Drawing.Point(0, 0);
		this.tsCali.Name = "tsCali";
		this.tsCali.Size = new System.Drawing.Size(1257, 39);
		this.tsCali.TabIndex = 7;
		this.tsCali.Text = "toolStrip1";
		this.btnNewCali.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
		this.btnNewCali.Image = (System.Drawing.Image)resources.GetObject("btnNewCali.Image");
		this.btnNewCali.ImageTransparentColor = System.Drawing.Color.Magenta;
		this.btnNewCali.Name = "btnNewCali";
		this.btnNewCali.Size = new System.Drawing.Size(36, 36);
		this.btnNewCali.Text = "新建组份表";
		this.btnNewCali.ToolTipText = "新建组份表";
		this.btnOpenCali.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
		this.btnOpenCali.Image = (System.Drawing.Image)resources.GetObject("btnOpenCali.Image");
		this.btnOpenCali.ImageTransparentColor = System.Drawing.Color.Magenta;
		this.btnOpenCali.Name = "btnOpenCali";
		this.btnOpenCali.Size = new System.Drawing.Size(36, 36);
		this.btnOpenCali.Text = "打开组份表";
		this.btnOpenCali.ToolTipText = "打开组份表";
		this.btnSaveCali.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
		this.btnSaveCali.Image = (System.Drawing.Image)resources.GetObject("btnSaveCali.Image");
		this.btnSaveCali.ImageTransparentColor = System.Drawing.Color.Magenta;
		this.btnSaveCali.Name = "btnSaveCali";
		this.btnSaveCali.Size = new System.Drawing.Size(36, 36);
		this.btnSaveCali.Text = "保存组份表";
		this.btnSaveCali.ToolTipText = "保存组份表";
		this.btnSaveCali.Click += new System.EventHandler(btnSaveCali_Click);
		this.toolStripSeparator7.Name = "toolStripSeparator7";
		this.toolStripSeparator7.Size = new System.Drawing.Size(6, 39);
		this.btnOpenChrom.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
		this.btnOpenChrom.Image = (System.Drawing.Image)resources.GetObject("btnOpenChrom.Image");
		this.btnOpenChrom.ImageTransparentColor = System.Drawing.Color.Magenta;
		this.btnOpenChrom.Name = "btnOpenChrom";
		this.btnOpenChrom.Size = new System.Drawing.Size(36, 36);
		this.btnOpenChrom.Text = "打开标样";
		this.btnOpenChrom.ToolTipText = "打开标样";
		this.btnOpenChrom.Click += new System.EventHandler(btnOpenChrom_Click);
		this.btnCloseChrom.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
		this.btnCloseChrom.Image = (System.Drawing.Image)resources.GetObject("btnCloseChrom.Image");
		this.btnCloseChrom.ImageTransparentColor = System.Drawing.Color.Magenta;
		this.btnCloseChrom.Name = "btnCloseChrom";
		this.btnCloseChrom.Size = new System.Drawing.Size(36, 36);
		this.btnCloseChrom.Text = "关闭标样";
		this.btnCloseChrom.ToolTipText = "关闭标样";
		this.groupBox1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.groupBox1.Controls.Add(this.groupBox4);
		this.groupBox1.Controls.Add(this.groupBox3);
		this.groupBox1.Location = new System.Drawing.Point(906, 12);
		this.groupBox1.Name = "groupBox1";
		this.groupBox1.Size = new System.Drawing.Size(321, 175);
		this.groupBox1.TabIndex = 8;
		this.groupBox1.TabStop = false;
		this.groupBox1.Text = "定量参数";
		this.groupBox4.Controls.Add(this.radioButton4);
		this.groupBox4.Controls.Add(this.radioButton3);
		this.groupBox4.Location = new System.Drawing.Point(169, 38);
		this.groupBox4.Name = "groupBox4";
		this.groupBox4.Size = new System.Drawing.Size(111, 101);
		this.groupBox4.TabIndex = 11;
		this.groupBox4.TabStop = false;
		this.groupBox4.Text = "定量方法";
		this.radioButton4.AutoSize = true;
		this.radioButton4.Location = new System.Drawing.Point(10, 54);
		this.radioButton4.Name = "radioButton4";
		this.radioButton4.Size = new System.Drawing.Size(71, 16);
		this.radioButton4.TabIndex = 12;
		this.radioButton4.TabStop = true;
		this.radioButton4.Text = "外标归一";
		this.radioButton4.UseVisualStyleBackColor = true;
		this.radioButton3.AutoSize = true;
		this.radioButton3.Location = new System.Drawing.Point(10, 21);
		this.radioButton3.Name = "radioButton3";
		this.radioButton3.Size = new System.Drawing.Size(47, 16);
		this.radioButton3.TabIndex = 11;
		this.radioButton3.TabStop = true;
		this.radioButton3.Text = "外标";
		this.radioButton3.UseVisualStyleBackColor = true;
		this.groupBox3.Controls.Add(this.radioButton2);
		this.groupBox3.Controls.Add(this.radioButton1);
		this.groupBox3.Location = new System.Drawing.Point(6, 39);
		this.groupBox3.Name = "groupBox3";
		this.groupBox3.Size = new System.Drawing.Size(111, 100);
		this.groupBox3.TabIndex = 10;
		this.groupBox3.TabStop = false;
		this.groupBox3.Text = "定量基准";
		this.radioButton2.AutoSize = true;
		this.radioButton2.Location = new System.Drawing.Point(6, 53);
		this.radioButton2.Name = "radioButton2";
		this.radioButton2.Size = new System.Drawing.Size(47, 16);
		this.radioButton2.TabIndex = 11;
		this.radioButton2.TabStop = true;
		this.radioButton2.Text = "峰高";
		this.radioButton2.UseVisualStyleBackColor = true;
		this.radioButton1.AutoSize = true;
		this.radioButton1.Location = new System.Drawing.Point(10, 20);
		this.radioButton1.Name = "radioButton1";
		this.radioButton1.Size = new System.Drawing.Size(59, 16);
		this.radioButton1.TabIndex = 10;
		this.radioButton1.TabStop = true;
		this.radioButton1.Text = "峰面积";
		this.radioButton1.UseVisualStyleBackColor = true;
		this.groupBox2.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.groupBox2.Controls.Add(this.tabControl1);
		this.groupBox2.Location = new System.Drawing.Point(906, 193);
		this.groupBox2.Name = "groupBox2";
		this.groupBox2.Size = new System.Drawing.Size(322, 175);
		this.groupBox2.TabIndex = 9;
		this.groupBox2.TabStop = false;
		this.groupBox2.Text = "积分参数";
		this.tabControl1.Controls.Add(this.tabPage1);
		this.tabControl1.Controls.Add(this.tabPage2);
		this.tabControl1.Controls.Add(this.tabPage3);
		this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
		this.tabControl1.Location = new System.Drawing.Point(3, 17);
		this.tabControl1.Name = "tabControl1";
		this.tabControl1.SelectedIndex = 0;
		this.tabControl1.Size = new System.Drawing.Size(316, 155);
		this.tabControl1.TabIndex = 0;
		this.tabPage1.Controls.Add(this.textBox3);
		this.tabPage1.Controls.Add(this.label3);
		this.tabPage1.Controls.Add(this.textBox2);
		this.tabPage1.Controls.Add(this.label2);
		this.tabPage1.Controls.Add(this.textBox1);
		this.tabPage1.Controls.Add(this.label1);
		this.tabPage1.Location = new System.Drawing.Point(4, 22);
		this.tabPage1.Name = "tabPage1";
		this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
		this.tabPage1.Size = new System.Drawing.Size(308, 129);
		this.tabPage1.TabIndex = 0;
		this.tabPage1.Text = "通道1";
		this.tabPage1.UseVisualStyleBackColor = true;
		this.textBox3.Location = new System.Drawing.Point(71, 87);
		this.textBox3.Name = "textBox3";
		this.textBox3.Size = new System.Drawing.Size(100, 21);
		this.textBox3.TabIndex = 11;
		this.label3.AutoSize = true;
		this.label3.Location = new System.Drawing.Point(24, 91);
		this.label3.Name = "label3";
		this.label3.Size = new System.Drawing.Size(41, 12);
		this.label3.TabIndex = 10;
		this.label3.Text = "峰斜率";
		this.textBox2.Location = new System.Drawing.Point(71, 51);
		this.textBox2.Name = "textBox2";
		this.textBox2.Size = new System.Drawing.Size(100, 21);
		this.textBox2.TabIndex = 9;
		this.label2.AutoSize = true;
		this.label2.Location = new System.Drawing.Point(24, 55);
		this.label2.Name = "label2";
		this.label2.Size = new System.Drawing.Size(29, 12);
		this.label2.TabIndex = 8;
		this.label2.Text = "峰高";
		this.textBox1.Location = new System.Drawing.Point(71, 20);
		this.textBox1.Name = "textBox1";
		this.textBox1.Size = new System.Drawing.Size(100, 21);
		this.textBox1.TabIndex = 7;
		this.label1.AutoSize = true;
		this.label1.Location = new System.Drawing.Point(24, 24);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(29, 12);
		this.label1.TabIndex = 6;
		this.label1.Text = "峰宽";
		this.tabPage2.Controls.Add(this.textBox4);
		this.tabPage2.Controls.Add(this.label4);
		this.tabPage2.Controls.Add(this.textBox5);
		this.tabPage2.Controls.Add(this.label5);
		this.tabPage2.Controls.Add(this.textBox6);
		this.tabPage2.Controls.Add(this.label6);
		this.tabPage2.Location = new System.Drawing.Point(4, 22);
		this.tabPage2.Name = "tabPage2";
		this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
		this.tabPage2.Size = new System.Drawing.Size(308, 129);
		this.tabPage2.TabIndex = 1;
		this.tabPage2.Text = "通道2";
		this.tabPage2.UseVisualStyleBackColor = true;
		this.textBox4.Location = new System.Drawing.Point(69, 87);
		this.textBox4.Name = "textBox4";
		this.textBox4.Size = new System.Drawing.Size(100, 21);
		this.textBox4.TabIndex = 11;
		this.label4.AutoSize = true;
		this.label4.Location = new System.Drawing.Point(22, 91);
		this.label4.Name = "label4";
		this.label4.Size = new System.Drawing.Size(41, 12);
		this.label4.TabIndex = 10;
		this.label4.Text = "峰斜率";
		this.textBox5.Location = new System.Drawing.Point(69, 51);
		this.textBox5.Name = "textBox5";
		this.textBox5.Size = new System.Drawing.Size(100, 21);
		this.textBox5.TabIndex = 9;
		this.label5.AutoSize = true;
		this.label5.Location = new System.Drawing.Point(22, 55);
		this.label5.Name = "label5";
		this.label5.Size = new System.Drawing.Size(29, 12);
		this.label5.TabIndex = 8;
		this.label5.Text = "峰高";
		this.textBox6.Location = new System.Drawing.Point(69, 20);
		this.textBox6.Name = "textBox6";
		this.textBox6.Size = new System.Drawing.Size(100, 21);
		this.textBox6.TabIndex = 7;
		this.label6.AutoSize = true;
		this.label6.Location = new System.Drawing.Point(22, 24);
		this.label6.Name = "label6";
		this.label6.Size = new System.Drawing.Size(29, 12);
		this.label6.TabIndex = 6;
		this.label6.Text = "峰宽";
		this.tabPage3.Controls.Add(this.textBox7);
		this.tabPage3.Controls.Add(this.label7);
		this.tabPage3.Controls.Add(this.textBox8);
		this.tabPage3.Controls.Add(this.label8);
		this.tabPage3.Controls.Add(this.textBox9);
		this.tabPage3.Controls.Add(this.label9);
		this.tabPage3.Location = new System.Drawing.Point(4, 22);
		this.tabPage3.Name = "tabPage3";
		this.tabPage3.Size = new System.Drawing.Size(308, 129);
		this.tabPage3.TabIndex = 2;
		this.tabPage3.Text = "通道3";
		this.tabPage3.UseVisualStyleBackColor = true;
		this.textBox7.Location = new System.Drawing.Point(75, 85);
		this.textBox7.Name = "textBox7";
		this.textBox7.Size = new System.Drawing.Size(100, 21);
		this.textBox7.TabIndex = 11;
		this.label7.AutoSize = true;
		this.label7.Location = new System.Drawing.Point(28, 89);
		this.label7.Name = "label7";
		this.label7.Size = new System.Drawing.Size(41, 12);
		this.label7.TabIndex = 10;
		this.label7.Text = "峰斜率";
		this.textBox8.Location = new System.Drawing.Point(75, 49);
		this.textBox8.Name = "textBox8";
		this.textBox8.Size = new System.Drawing.Size(100, 21);
		this.textBox8.TabIndex = 9;
		this.label8.AutoSize = true;
		this.label8.Location = new System.Drawing.Point(28, 53);
		this.label8.Name = "label8";
		this.label8.Size = new System.Drawing.Size(29, 12);
		this.label8.TabIndex = 8;
		this.label8.Text = "峰高";
		this.textBox9.Location = new System.Drawing.Point(75, 18);
		this.textBox9.Name = "textBox9";
		this.textBox9.Size = new System.Drawing.Size(100, 21);
		this.textBox9.TabIndex = 7;
		this.label9.AutoSize = true;
		this.label9.Location = new System.Drawing.Point(28, 22);
		this.label9.Name = "label9";
		this.label9.Size = new System.Drawing.Size(29, 12);
		this.label9.TabIndex = 6;
		this.label9.Text = "峰宽";
		this.groupBox5.Controls.Add(this.dataGridView1);
		this.groupBox5.Location = new System.Drawing.Point(879, 390);
		this.groupBox5.Name = "groupBox5";
		this.groupBox5.Size = new System.Drawing.Size(378, 184);
		this.groupBox5.TabIndex = 10;
		this.groupBox5.TabStop = false;
		this.groupBox5.Text = "组份表";
		this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
		this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
		this.dataGridView1.Columns.AddRange(this.组份号, this.组分名, this.保留时间, this.带宽, this.通道号, this.标样浓度, this.单位, this.校正因子, this.含量);
		this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
		this.dataGridView1.Location = new System.Drawing.Point(3, 17);
		this.dataGridView1.Name = "dataGridView1";
		this.dataGridView1.RowTemplate.Height = 23;
		this.dataGridView1.Size = new System.Drawing.Size(372, 164);
		this.dataGridView1.TabIndex = 0;
		this.组份号.HeaderText = "组份号";
		this.组份号.Name = "组份号";
		this.组分名.HeaderText = "组分名";
		this.组分名.Name = "组分名";
		this.保留时间.HeaderText = "保留时间";
		this.保留时间.Name = "保留时间";
		this.带宽.HeaderText = "带宽";
		this.带宽.Name = "带宽";
		this.通道号.HeaderText = "通道号";
		this.通道号.Name = "通道号";
		this.标样浓度.HeaderText = "标样浓度";
		this.标样浓度.Name = "标样浓度";
		this.单位.HeaderText = "单位";
		this.单位.Name = "单位";
		this.校正因子.HeaderText = "校正因子";
		this.校正因子.Name = "校正因子";
		this.含量.HeaderText = "含量";
		this.含量.Name = "含量";
		this.dataGridViewTextBoxColumn1.HeaderText = "组份号";
		this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
		this.dataGridViewTextBoxColumn1.Width = 73;
		this.dataGridViewTextBoxColumn2.HeaderText = "组分名";
		this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
		this.dataGridViewTextBoxColumn2.Width = 73;
		this.dataGridViewTextBoxColumn3.HeaderText = "保留时间";
		this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
		this.dataGridViewTextBoxColumn3.Width = 73;
		this.dataGridViewTextBoxColumn4.HeaderText = "带宽";
		this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
		this.dataGridViewTextBoxColumn4.Width = 73;
		this.dataGridViewTextBoxColumn5.HeaderText = "通道号";
		this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
		this.dataGridViewTextBoxColumn5.Width = 74;
		this.dataGridViewTextBoxColumn6.HeaderText = "标样浓度";
		this.dataGridViewTextBoxColumn6.Name = "dataGridViewTextBoxColumn6";
		this.dataGridViewTextBoxColumn6.Width = 73;
		this.dataGridViewTextBoxColumn7.HeaderText = "单位";
		this.dataGridViewTextBoxColumn7.Name = "dataGridViewTextBoxColumn7";
		this.dataGridViewTextBoxColumn7.Width = 73;
		this.dataGridViewTextBoxColumn8.HeaderText = "校正因子";
		this.dataGridViewTextBoxColumn8.Name = "dataGridViewTextBoxColumn8";
		this.dataGridViewTextBoxColumn8.Width = 73;
		this.dataGridViewTextBoxColumn9.HeaderText = "含量";
		this.dataGridViewTextBoxColumn9.Name = "dataGridViewTextBoxColumn9";
		this.dataGridViewTextBoxColumn9.Width = 73;
		this.mineChromCtrl1.Dock = System.Windows.Forms.DockStyle.Fill;
		this.mineChromCtrl1.Location = new System.Drawing.Point(0, 0);
		this.mineChromCtrl1.Name = "mineChromCtrl1";
		this.mineChromCtrl1.ShowManuAndStateBar = true;
		this.mineChromCtrl1.ShowOnlineMethod = false;
		this.mineChromCtrl1.Size = new System.Drawing.Size(714, 285);
		this.mineChromCtrl1.TabIndex = 11;
		this.splitContainer1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.splitContainer1.Location = new System.Drawing.Point(0, 42);
		this.splitContainer1.Name = "splitContainer1";
		this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
		this.splitContainer1.Panel1.Controls.Add(this.mineChromCtrl1);
		this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
		this.splitContainer1.Size = new System.Drawing.Size(714, 573);
		this.splitContainer1.SplitterDistance = 285;
		this.splitContainer1.TabIndex = 12;
		this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
		this.splitContainer2.Location = new System.Drawing.Point(0, 0);
		this.splitContainer2.Name = "splitContainer2";
		this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
		this.splitContainer2.Panel1.Controls.Add(this.mineChromCtrl2);
		this.splitContainer2.Panel2.Controls.Add(this.mineChromCtrl3);
		this.splitContainer2.Size = new System.Drawing.Size(714, 284);
		this.splitContainer2.SplitterDistance = 238;
		this.splitContainer2.TabIndex = 0;
		this.mineChromCtrl2.Dock = System.Windows.Forms.DockStyle.Fill;
		this.mineChromCtrl2.Location = new System.Drawing.Point(0, 0);
		this.mineChromCtrl2.Name = "mineChromCtrl2";
		this.mineChromCtrl2.ShowManuAndStateBar = true;
		this.mineChromCtrl2.ShowOnlineMethod = false;
		this.mineChromCtrl2.Size = new System.Drawing.Size(714, 238);
		this.mineChromCtrl2.TabIndex = 12;
		this.mineChromCtrl3.Dock = System.Windows.Forms.DockStyle.Fill;
		this.mineChromCtrl3.Location = new System.Drawing.Point(0, 0);
		this.mineChromCtrl3.Name = "mineChromCtrl3";
		this.mineChromCtrl3.ShowManuAndStateBar = true;
		this.mineChromCtrl3.ShowOnlineMethod = false;
		this.mineChromCtrl3.Size = new System.Drawing.Size(714, 42);
		this.mineChromCtrl3.TabIndex = 12;
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(1257, 611);
		base.Controls.Add(this.splitContainer1);
		base.Controls.Add(this.groupBox5);
		base.Controls.Add(this.groupBox2);
		base.Controls.Add(this.groupBox1);
		base.Controls.Add(this.tsCali);
		base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
		base.Name = "FormMineMethod";
		this.Text = "FormMineMethod";
		this.tsCali.ResumeLayout(false);
		this.tsCali.PerformLayout();
		this.groupBox1.ResumeLayout(false);
		this.groupBox4.ResumeLayout(false);
		this.groupBox4.PerformLayout();
		this.groupBox3.ResumeLayout(false);
		this.groupBox3.PerformLayout();
		this.groupBox2.ResumeLayout(false);
		this.tabControl1.ResumeLayout(false);
		this.tabPage1.ResumeLayout(false);
		this.tabPage1.PerformLayout();
		this.tabPage2.ResumeLayout(false);
		this.tabPage2.PerformLayout();
		this.tabPage3.ResumeLayout(false);
		this.tabPage3.PerformLayout();
		this.groupBox5.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.dataGridView1).EndInit();
		this.splitContainer1.Panel1.ResumeLayout(false);
		this.splitContainer1.Panel2.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.splitContainer1).EndInit();
		this.splitContainer1.ResumeLayout(false);
		this.splitContainer2.Panel1.ResumeLayout(false);
		this.splitContainer2.Panel2.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.splitContainer2).EndInit();
		this.splitContainer2.ResumeLayout(false);
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
