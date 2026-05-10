using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using IBrainChrom2018.Unit;

namespace IBrainChrom2018;

public class FrmGasNameSet : Form
{
	private FormMainParam frmParam = FormMainParam.Create();

	private IContainer icontainer_0;

	private Button button4;

	private Button button1;

	public DataGridView dgtempControl;

	private DataGridViewTextBoxColumn 名称;

	private DataGridViewTextBoxColumn 中文;

	private DataGridViewTextBoxColumn 英文;

	private DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;

	private DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;

	private DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;

	private ChromFormInterface formMain_0;

	private IniParam iniParam = new IniParam(Application.StartupPath + "\\iniParam.dll");

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
		this.button4 = new System.Windows.Forms.Button();
		this.button1 = new System.Windows.Forms.Button();
		this.dgtempControl = new System.Windows.Forms.DataGridView();
		this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.名称 = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.中文 = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.英文 = new System.Windows.Forms.DataGridViewTextBoxColumn();
		((System.ComponentModel.ISupportInitialize)this.dgtempControl).BeginInit();
		base.SuspendLayout();
		this.button4.ForeColor = System.Drawing.Color.Blue;
		this.button4.Location = new System.Drawing.Point(200, 477);
		this.button4.Name = "button4";
		this.button4.Size = new System.Drawing.Size(75, 23);
		this.button4.TabIndex = 4;
		this.button4.Text = "设定";
		this.button4.UseVisualStyleBackColor = true;
		this.button4.Click += new System.EventHandler(button4_Click);
		this.button1.ForeColor = System.Drawing.Color.Blue;
		this.button1.Location = new System.Drawing.Point(2, 477);
		this.button1.Name = "button1";
		this.button1.Size = new System.Drawing.Size(75, 23);
		this.button1.TabIndex = 5;
		this.button1.Text = "查询";
		this.button1.UseVisualStyleBackColor = true;
		this.button1.Click += new System.EventHandler(button1_Click);
		this.dgtempControl.AllowUserToAddRows = false;
		this.dgtempControl.AllowUserToDeleteRows = false;
		this.dgtempControl.AllowUserToOrderColumns = true;
		this.dgtempControl.AllowUserToResizeColumns = false;
		this.dgtempControl.AllowUserToResizeRows = false;
		this.dgtempControl.BackgroundColor = System.Drawing.SystemColors.ActiveCaptionText;
		dataGridViewCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
		dataGridViewCellStyle.BackColor = System.Drawing.Color.FromArgb(128, 128, 255);
		dataGridViewCellStyle.Font = new System.Drawing.Font("宋体", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		dataGridViewCellStyle.ForeColor = System.Drawing.Color.FromArgb(128, 128, 255);
		dataGridViewCellStyle.SelectionBackColor = System.Drawing.SystemColors.Highlight;
		dataGridViewCellStyle.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
		dataGridViewCellStyle.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
		this.dgtempControl.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle;
		this.dgtempControl.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
		this.dgtempControl.Columns.AddRange(this.名称, this.中文, this.英文);
		this.dgtempControl.Dock = System.Windows.Forms.DockStyle.Top;
		this.dgtempControl.Location = new System.Drawing.Point(0, 0);
		this.dgtempControl.MultiSelect = false;
		this.dgtempControl.Name = "dgtempControl";
		this.dgtempControl.RowHeadersVisible = false;
		this.dgtempControl.RowTemplate.Height = 23;
		this.dgtempControl.Size = new System.Drawing.Size(287, 471);
		this.dgtempControl.TabIndex = 3;
		dataGridViewCellStyle2.BackColor = System.Drawing.Color.Blue;
		dataGridViewCellStyle2.Font = new System.Drawing.Font("宋体", 9f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 134);
		dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
		this.dataGridViewTextBoxColumn1.DefaultCellStyle = dataGridViewCellStyle2;
		this.dataGridViewTextBoxColumn1.HeaderText = "名称";
		this.dataGridViewTextBoxColumn1.MinimumWidth = 20;
		this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
		this.dataGridViewTextBoxColumn1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.dataGridViewTextBoxColumn1.Width = 80;
		dataGridViewCellStyle3.BackColor = System.Drawing.Color.Black;
		dataGridViewCellStyle3.Font = new System.Drawing.Font("宋体", 9f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 134);
		dataGridViewCellStyle3.ForeColor = System.Drawing.Color.Lime;
		this.dataGridViewTextBoxColumn2.DefaultCellStyle = dataGridViewCellStyle3;
		this.dataGridViewTextBoxColumn2.HeaderText = "中文";
		this.dataGridViewTextBoxColumn2.MaxInputLength = 6;
		this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
		this.dataGridViewTextBoxColumn2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		dataGridViewCellStyle4.BackColor = System.Drawing.Color.Black;
		dataGridViewCellStyle4.Font = new System.Drawing.Font("宋体", 9f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 134);
		dataGridViewCellStyle4.ForeColor = System.Drawing.Color.White;
		this.dataGridViewTextBoxColumn3.DefaultCellStyle = dataGridViewCellStyle4;
		this.dataGridViewTextBoxColumn3.HeaderText = "英文";
		this.dataGridViewTextBoxColumn3.MaxInputLength = 6;
		this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
		this.dataGridViewTextBoxColumn3.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		dataGridViewCellStyle5.BackColor = System.Drawing.Color.Blue;
		dataGridViewCellStyle5.Font = new System.Drawing.Font("宋体", 9f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 134);
		dataGridViewCellStyle5.ForeColor = System.Drawing.Color.White;
		this.名称.DefaultCellStyle = dataGridViewCellStyle5;
		this.名称.HeaderText = "名称";
		this.名称.MinimumWidth = 20;
		this.名称.Name = "名称";
		this.名称.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.名称.Width = 80;
		dataGridViewCellStyle6.BackColor = System.Drawing.Color.Black;
		dataGridViewCellStyle6.Font = new System.Drawing.Font("宋体", 9f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 134);
		dataGridViewCellStyle6.ForeColor = System.Drawing.Color.Lime;
		this.中文.DefaultCellStyle = dataGridViewCellStyle6;
		this.中文.HeaderText = "中文";
		this.中文.MaxInputLength = 6;
		this.中文.Name = "中文";
		this.中文.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		dataGridViewCellStyle7.BackColor = System.Drawing.Color.Black;
		dataGridViewCellStyle7.Font = new System.Drawing.Font("宋体", 9f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 134);
		dataGridViewCellStyle7.ForeColor = System.Drawing.Color.White;
		this.英文.DefaultCellStyle = dataGridViewCellStyle7;
		this.英文.HeaderText = "英文";
		this.英文.MaxInputLength = 6;
		this.英文.Name = "英文";
		this.英文.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(287, 512);
		base.Controls.Add(this.button4);
		base.Controls.Add(this.button1);
		base.Controls.Add(this.dgtempControl);
		base.MaximizeBox = false;
		base.MinimizeBox = false;
		base.Name = "FrmGasNameSet";
		base.ShowIcon = false;
		base.ShowInTaskbar = false;
		this.Text = "气路名称配置";
		base.TopMost = true;
		base.Activated += new System.EventHandler(FrmGasNameSet_Activated);
		base.FormClosing += new System.Windows.Forms.FormClosingEventHandler(FrmGasNameSet_FormClosing);
		base.Load += new System.EventHandler(FrmGasNameSet_Load);
		((System.ComponentModel.ISupportInitialize)this.dgtempControl).EndInit();
		base.ResumeLayout(false);
	}

	public FrmGasNameSet()
	{
		InitializeComponent();
		iniParam.LoadParam();
		CtrlLangPS.Instance.UpdateLanguageForAllControl(this);
	}

	public void Init(ChromFormInterface Fm)
	{
		iniParam.LoadParam();
		for (int i = 0; i < 24; i++)
		{
			string key = "第 " + (i + 1) + "  路";
			dgtempControl.Rows.Add(Lang.PS(key, "  8  "), iniParam.strGasNAME[i], " ");
		}
		formMain_0 = Fm;
	}

	private void FrmGasNameSet_FormClosing(object sender, FormClosingEventArgs e)
	{
		e.Cancel = true;
		Hide();
	}

	private void FrmGasNameSet_Load(object sender, EventArgs e)
	{
		for (int i = 0; i < 24; i++)
		{
			dgtempControl.Rows[i].Cells[1].Value = iniParam.strGasNAME[i];
		}
	}

	private void FrmGasNameSet_Activated(object sender, EventArgs e)
	{
	}

	private void button1_Click(object sender, EventArgs e)
	{
	}

	private void button4_Click(object sender, EventArgs e)
	{
		for (int i = 0; i < 24; i++)
		{
			if (dgtempControl.Rows[i].Cells[1].Value == null)
			{
				dgtempControl.Rows[i].Cells[1].Value = " ";
			}
			iniParam.strGasNAME[i] = dgtempControl.Rows[i].Cells[1].Value.ToString();
		}
		iniParam.SaveParam();
		Close();
	}
}
