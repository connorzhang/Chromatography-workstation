using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace IBrainChrom2018;

public class FrmTempNameSet : Form
{
	public int clmCT6CNIndex = 1;

	public int clmCT6ENIndex = 2;

	public int clmCT6CtrlTIndex = 3;

	private ChromFormInterface formMain_0;

	private IContainer icontainer_0;

	public DataGridView dgvCT6;

	private Button button1;

	private Button button4;

	private DataGridViewTextBoxColumn 名称;

	private DataGridViewTextBoxColumn 中文;

	private DataGridViewTextBoxColumn 英文;

	private DataGridViewCheckBoxColumn 使能;

	private DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;

	private DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;

	private DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;

	private DataGridViewCheckBoxColumn dataGridViewCheckBoxColumn1;

	public FrmTempNameSet()
	{
		InitializeComponent();
		CtrlLangPS.Instance.UpdateLanguageForAllControl(this);
	}

	public void Init(ChromFormInterface Fm)
	{
		dgvCT6.Rows.Add(Lang.PS("控区一", "Zone1"), " ", " ", false);
		dgvCT6.Rows.Add(Lang.PS("控区二", "Zone2"), " ", " ", false);
		dgvCT6.Rows.Add(Lang.PS("控区三", "Zone3"), " ", " ", false);
		dgvCT6.Rows.Add(Lang.PS("控区四", "Zone4"), " ", " ", false);
		dgvCT6.Rows.Add(Lang.PS("控区五", "Zone5"), " ", " ", false);
		dgvCT6.Rows.Add(Lang.PS("控区六", "Zone6"), " ", " ", false);
		dgvCT6.Rows.Add(Lang.PS("控区七", "Zone7"), " ", " ", false);
		dgvCT6.Rows.Add(Lang.PS("控区八", "Zone8"), " ", " ", false);
		formMain_0 = Fm;
	}

	private void FrmTempNameSet_FormClosing(object sender, FormClosingEventArgs e)
	{
		e.Cancel = true;
		Hide();
	}

	public void button1_Click(object sender, EventArgs e)
	{
		formMain_0.DtrTempNameSelect();
		formMain_0.DtrTempNameEnableSelect();
	}

	private void button4_Click(object sender, EventArgs e)
	{
		formMain_0.DtrTempNameSet();
		formMain_0.DtrTempNameEnableSt();
	}

	private void FrmTempNameSet_Load(object sender, EventArgs e)
	{
		method_0();
	}

	private void method_0()
	{
		Text = Lang.PS("控温区名称设置", "Temperature control zone set name ");
		button1.Text = Lang.PS("查询", "query");
		button4.Text = Lang.PS("设定", "set");
		dgvCT6.Columns[0].HeaderText = Lang.PS("名称", "Name");
		dgvCT6.Columns[1].HeaderText = Lang.PS("中文", "CN");
		dgvCT6.Columns[2].HeaderText = Lang.PS("英文", "EN");
		dgvCT6.Columns[3].HeaderText = Lang.PS("使能", "Use");
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
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
		this.dgvCT6 = new System.Windows.Forms.DataGridView();
		this.名称 = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.中文 = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.英文 = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.使能 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
		this.button1 = new System.Windows.Forms.Button();
		this.button4 = new System.Windows.Forms.Button();
		this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.dataGridViewCheckBoxColumn1 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
		((System.ComponentModel.ISupportInitialize)this.dgvCT6).BeginInit();
		base.SuspendLayout();
		this.dgvCT6.AllowUserToAddRows = false;
		this.dgvCT6.AllowUserToDeleteRows = false;
		this.dgvCT6.AllowUserToOrderColumns = true;
		this.dgvCT6.AllowUserToResizeColumns = false;
		this.dgvCT6.AllowUserToResizeRows = false;
		this.dgvCT6.BackgroundColor = System.Drawing.SystemColors.ActiveCaptionText;
		dataGridViewCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
		dataGridViewCellStyle.BackColor = System.Drawing.Color.FromArgb(128, 128, 255);
		dataGridViewCellStyle.Font = new System.Drawing.Font("宋体", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		dataGridViewCellStyle.ForeColor = System.Drawing.Color.FromArgb(128, 128, 255);
		dataGridViewCellStyle.SelectionBackColor = System.Drawing.SystemColors.Highlight;
		dataGridViewCellStyle.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
		dataGridViewCellStyle.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
		this.dgvCT6.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle;
		this.dgvCT6.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
		this.dgvCT6.Columns.AddRange(this.名称, this.中文, this.英文, this.使能);
		this.dgvCT6.Location = new System.Drawing.Point(-2, 1);
		this.dgvCT6.MultiSelect = false;
		this.dgvCT6.Name = "dgvCT6";
		this.dgvCT6.RowHeadersVisible = false;
		this.dgvCT6.RowTemplate.Height = 23;
		this.dgvCT6.Size = new System.Drawing.Size(304, 213);
		this.dgvCT6.TabIndex = 1;
		dataGridViewCellStyle2.BackColor = System.Drawing.Color.Blue;
		dataGridViewCellStyle2.Font = new System.Drawing.Font("宋体", 9f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 134);
		dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
		this.名称.DefaultCellStyle = dataGridViewCellStyle2;
		this.名称.HeaderText = "名称";
		this.名称.MinimumWidth = 20;
		this.名称.Name = "名称";
		this.名称.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.名称.Width = 60;
		dataGridViewCellStyle3.BackColor = System.Drawing.Color.Black;
		dataGridViewCellStyle3.Font = new System.Drawing.Font("宋体", 9f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 134);
		dataGridViewCellStyle3.ForeColor = System.Drawing.Color.Lime;
		this.中文.DefaultCellStyle = dataGridViewCellStyle3;
		this.中文.HeaderText = "中文";
		this.中文.MaxInputLength = 6;
		this.中文.Name = "中文";
		this.中文.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.中文.Width = 80;
		dataGridViewCellStyle4.BackColor = System.Drawing.Color.Black;
		dataGridViewCellStyle4.Font = new System.Drawing.Font("宋体", 9f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 134);
		dataGridViewCellStyle4.ForeColor = System.Drawing.Color.White;
		this.英文.DefaultCellStyle = dataGridViewCellStyle4;
		this.英文.HeaderText = "英文";
		this.英文.MaxInputLength = 6;
		this.英文.Name = "英文";
		this.英文.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.英文.Width = 80;
		dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
		dataGridViewCellStyle5.BackColor = System.Drawing.Color.Black;
		dataGridViewCellStyle5.Font = new System.Drawing.Font("宋体", 9f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 134);
		dataGridViewCellStyle5.ForeColor = System.Drawing.Color.Yellow;
		dataGridViewCellStyle5.NullValue = false;
		this.使能.DefaultCellStyle = dataGridViewCellStyle5;
		this.使能.HeaderText = "使能";
		this.使能.Name = "使能";
		this.使能.Resizable = System.Windows.Forms.DataGridViewTriState.True;
		this.使能.Width = 80;
		this.button1.ForeColor = System.Drawing.Color.Blue;
		this.button1.Location = new System.Drawing.Point(12, 220);
		this.button1.Name = "button1";
		this.button1.Size = new System.Drawing.Size(75, 23);
		this.button1.TabIndex = 2;
		this.button1.Text = "查询";
		this.button1.UseVisualStyleBackColor = true;
		this.button1.Click += new System.EventHandler(button1_Click);
		this.button4.ForeColor = System.Drawing.Color.Blue;
		this.button4.Location = new System.Drawing.Point(215, 220);
		this.button4.Name = "button4";
		this.button4.Size = new System.Drawing.Size(75, 23);
		this.button4.TabIndex = 2;
		this.button4.Text = "设定";
		this.button4.UseVisualStyleBackColor = true;
		this.button4.Click += new System.EventHandler(button4_Click);
		dataGridViewCellStyle6.BackColor = System.Drawing.Color.Blue;
		dataGridViewCellStyle6.Font = new System.Drawing.Font("宋体", 9f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 134);
		dataGridViewCellStyle6.ForeColor = System.Drawing.Color.White;
		this.dataGridViewTextBoxColumn1.DefaultCellStyle = dataGridViewCellStyle6;
		this.dataGridViewTextBoxColumn1.HeaderText = "名称";
		this.dataGridViewTextBoxColumn1.MinimumWidth = 20;
		this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
		this.dataGridViewTextBoxColumn1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.dataGridViewTextBoxColumn1.Width = 60;
		dataGridViewCellStyle7.BackColor = System.Drawing.Color.Black;
		dataGridViewCellStyle7.Font = new System.Drawing.Font("宋体", 9f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 134);
		dataGridViewCellStyle7.ForeColor = System.Drawing.Color.Lime;
		this.dataGridViewTextBoxColumn2.DefaultCellStyle = dataGridViewCellStyle7;
		this.dataGridViewTextBoxColumn2.HeaderText = "中文";
		this.dataGridViewTextBoxColumn2.MaxInputLength = 6;
		this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
		this.dataGridViewTextBoxColumn2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.dataGridViewTextBoxColumn2.Width = 80;
		dataGridViewCellStyle8.BackColor = System.Drawing.Color.Black;
		dataGridViewCellStyle8.Font = new System.Drawing.Font("宋体", 9f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 134);
		dataGridViewCellStyle8.ForeColor = System.Drawing.Color.White;
		this.dataGridViewTextBoxColumn3.DefaultCellStyle = dataGridViewCellStyle8;
		this.dataGridViewTextBoxColumn3.HeaderText = "英文";
		this.dataGridViewTextBoxColumn3.MaxInputLength = 6;
		this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
		this.dataGridViewTextBoxColumn3.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.dataGridViewTextBoxColumn3.Width = 80;
		dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
		dataGridViewCellStyle9.BackColor = System.Drawing.Color.Black;
		dataGridViewCellStyle9.Font = new System.Drawing.Font("宋体", 9f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 134);
		dataGridViewCellStyle9.ForeColor = System.Drawing.Color.Yellow;
		dataGridViewCellStyle9.NullValue = false;
		this.dataGridViewCheckBoxColumn1.DefaultCellStyle = dataGridViewCellStyle9;
		this.dataGridViewCheckBoxColumn1.HeaderText = "使能";
		this.dataGridViewCheckBoxColumn1.Name = "dataGridViewCheckBoxColumn1";
		this.dataGridViewCheckBoxColumn1.Resizable = System.Windows.Forms.DataGridViewTriState.True;
		this.dataGridViewCheckBoxColumn1.Width = 80;
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(302, 255);
		base.Controls.Add(this.button4);
		base.Controls.Add(this.button1);
		base.Controls.Add(this.dgvCT6);
		base.MaximizeBox = false;
		base.MinimizeBox = false;
		base.Name = "FrmTempNameSet";
		base.ShowIcon = false;
		base.ShowInTaskbar = false;
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.Text = "控温区名称配置";
		base.TopMost = true;
		base.FormClosing += new System.Windows.Forms.FormClosingEventHandler(FrmTempNameSet_FormClosing);
		base.Load += new System.EventHandler(FrmTempNameSet_Load);
		((System.ComponentModel.ISupportInitialize)this.dgvCT6).EndInit();
		base.ResumeLayout(false);
	}
}
