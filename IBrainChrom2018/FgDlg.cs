using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace IBrainChrom2018;

public class FgDlg : LclDialog
{
	private LclCheckBox cbFgUse;

	private IContainer icontainer_1;

	public FgRow[] fgRows = new FgRow[0];

	public bool fgUse;

	private LclGridView gvFg;

	private LclGroupBox lclGroupBox1;

	private LclLabel lclLabel1;

	private LclLabel lclLabel2;

	private LclLabel lclLabel3;

	private LclLabel lclLabel4;

	private LclLabel lclLabel5;

	private LclLabel lclLabel6;

	private LclTextBox tbFgNs;

	private LclTextBox tbFgTime;

	private LclTextBox tbFgValue;

	public FgDlg()
	{
		InitializeComponent_1();
		Text = Lang.PS("馏分收集", "Frag Gather");
		cbFgUse.Text = Lang.PS("进行馏分收集", "Use Frag Gather");
		lclGroupBox1.Text = Lang.PS("参数", "Parameters");
		lclLabel1.Text = Lang.PS("检测窗宽度", "Dtc. Win. width");
		lclLabel2.Text = Lang.PS("点", "dots");
		lclLabel3.Text = Lang.PS("确认时间", "Confirm Time");
		lclLabel5.Text = Lang.PS("确认信号", "Confirm Signal");
		gvFg.AllowUserToAddRows = true;
		gvFg.AllowUserToDeleteRows = true;
		gvFg.AddLclTextBoxColumn("colStartT", 100, 2).HeaderText = Lang.PS("起始时间\n[min]", "Start Time\n[min]");
		gvFg.AddLclTextBoxColumn("colEndT", 100, 2).HeaderText = Lang.PS("结束时间\n[min]", "End Time\n[min]");
	}

	private void method_0(object sender, EventArgs e)
	{
		fgUse = cbFgUse.Checked;
		Array.Resize(ref fgRows, 0);
		for (int i = 0; i < gvFg.RowCount; i++)
		{
			object value = gvFg.Rows[i].Cells[0].Value;
			object value2 = gvFg.Rows[i].Cells[1].Value;
			if (value != null && float.TryParse(value.ToString(), out var result) && value2 != null && float.TryParse(value2.ToString(), out var result2) && result != result2)
			{
				if (result > result2)
				{
					float num = result;
					result = result2;
					result2 = num;
				}
				int num2 = fgRows.Length;
				Array.Resize(ref fgRows, num2 + 1);
				fgRows[num2].startT = result;
				fgRows[num2].endT = result2;
			}
		}
		int val = Class49.Object2Int(tbFgNs.Text, 3);
		Signal.hwN = (Math.Max(3, val) - 1) / 2;
		Signal.fgTime = Math.Max(0.01f, Class49.String2Float(tbFgTime.Text, Signal.fgTime));
		Signal.fgValue = Math.Max(0.001f, Class49.String2Float(tbFgValue.Text, Signal.fgValue));
	}

	protected override void Dispose(bool disposing)
	{
		if (disposing && icontainer_1 != null)
		{
			icontainer_1.Dispose();
		}
		base.Dispose(disposing);
	}

	private void FgDlg_Load(object sender, EventArgs e)
	{
		cbFgUse.Checked = fgUse;
		gvFg.RowCount = fgRows.Length + 1;
		for (int i = 0; i < fgRows.Length; i++)
		{
			gvFg.Rows[i].Cells[0].Value = fgRows[i].startT;
			gvFg.Rows[i].Cells[1].Value = fgRows[i].endT;
		}
		tbFgNs.Text = (Signal.hwN + Signal.hwN + 1).ToString();
		tbFgTime.Text = Signal.fgTime.ToString();
		tbFgValue.Text = Signal.fgValue.ToString();
	}

	private void InitializeComponent_1()
	{
		cbFgUse = new LclCheckBox();
		gvFg = new LclGridView();
		lclGroupBox1 = new LclGroupBox();
		tbFgValue = new LclTextBox();
		lclLabel6 = new LclLabel();
		tbFgTime = new LclTextBox();
		lclLabel4 = new LclLabel();
		lclLabel5 = new LclLabel();
		tbFgNs = new LclTextBox();
		lclLabel3 = new LclLabel();
		lclLabel2 = new LclLabel();
		lclLabel1 = new LclLabel();
		((ISupportInitialize)gvFg).BeginInit();
		lclGroupBox1.SuspendLayout();
		SuspendLayout();
		btnOK.Location = new Point(10, 233);
		btnOK.Text = "确认";
		btnOK.Click += method_0;
		btnCancel.Location = new Point(102, 233);
		btnCancel.Text = "取消";
		btnHelp.Location = new Point(193, 233);
		btnHelp.Text = "帮助";
		cbFgUse.AutoSize = true;
		cbFgUse.Location = new Point(12, 12);
		cbFgUse.Name = "cbFgUse";
		cbFgUse.Size = new Size(96, 16);
		cbFgUse.TabIndex = 1;
		cbFgUse.Text = "进行馏分收集";
		cbFgUse.UseVisualStyleBackColor = true;
		gvFg.AllowUserToAddRows = false;
		gvFg.AllowUserToDeleteRows = false;
		gvFg.AllowUserToResizeRows = false;
		gvFg.BackgroundColor = Color.AliceBlue;
		gvFg.CharacterHeaderColor = Color.Black;
		gvFg.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
		gvFg.EditMode = DataGridViewEditMode.EditProgrammatically;
		gvFg.Location = new Point(12, 34);
		gvFg.Name = "gvFg";
		gvFg.RowHeadersWidth = 25;
		gvFg.RowTemplate.Height = 16;
		gvFg.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
		gvFg.ShowCellToolTips = false;
		gvFg.Size = new Size(270, 94);
		gvFg.TabIndex = 2;
		lclGroupBox1.Controls.Add(tbFgValue);
		lclGroupBox1.Controls.Add(lclLabel6);
		lclGroupBox1.Controls.Add(tbFgTime);
		lclGroupBox1.Controls.Add(lclLabel4);
		lclGroupBox1.Controls.Add(lclLabel5);
		lclGroupBox1.Controls.Add(tbFgNs);
		lclGroupBox1.Controls.Add(lclLabel3);
		lclGroupBox1.Controls.Add(lclLabel2);
		lclGroupBox1.Controls.Add(lclLabel1);
		lclGroupBox1.Location = new Point(12, 134);
		lclGroupBox1.Name = "lclGroupBox1";
		lclGroupBox1.Size = new Size(192, 93);
		lclGroupBox1.TabIndex = 3;
		lclGroupBox1.TabStop = false;
		lclGroupBox1.Text = "参数";
		tbFgValue.Location = new Point(90, 40);
		tbFgValue.Name = "tbFgValue";
		tbFgValue.Size = new Size(61, 21);
		tbFgValue.TabIndex = 1;
		lclLabel6.AutoSize = true;
		lclLabel6.Location = new Point(157, 43);
		lclLabel6.Name = "lclLabel6";
		lclLabel6.Size = new Size(17, 12);
		lclLabel6.TabIndex = 0;
		lclLabel6.Text = Class49.MesureUnit();
		tbFgTime.Location = new Point(90, 64);
		tbFgTime.Name = "tbFgTime";
		tbFgTime.Size = new Size(61, 21);
		tbFgTime.TabIndex = 1;
		lclLabel4.AutoSize = true;
		lclLabel4.Location = new Point(157, 67);
		lclLabel4.Name = "lclLabel4";
		lclLabel4.Size = new Size(23, 12);
		lclLabel4.TabIndex = 0;
		lclLabel4.Text = "min";
		lclLabel5.AutoSize = true;
		lclLabel5.Location = new Point(10, 43);
		lclLabel5.Name = "lclLabel5";
		lclLabel5.Size = new Size(53, 12);
		lclLabel5.TabIndex = 0;
		lclLabel5.Text = "确认信号";
		tbFgNs.Location = new Point(90, 16);
		tbFgNs.Name = "tbFgNs";
		tbFgNs.Size = new Size(61, 21);
		tbFgNs.TabIndex = 1;
		lclLabel3.AutoSize = true;
		lclLabel3.Location = new Point(10, 67);
		lclLabel3.Name = "lclLabel3";
		lclLabel3.Size = new Size(53, 12);
		lclLabel3.TabIndex = 0;
		lclLabel3.Text = "确认时间";
		lclLabel2.AutoSize = true;
		lclLabel2.Location = new Point(157, 19);
		lclLabel2.Name = "lclLabel2";
		lclLabel2.Size = new Size(17, 12);
		lclLabel2.TabIndex = 0;
		lclLabel2.Text = "点";
		lclLabel1.AutoSize = true;
		lclLabel1.Location = new Point(10, 19);
		lclLabel1.Name = "lclLabel1";
		lclLabel1.Size = new Size(65, 12);
		lclLabel1.TabIndex = 0;
		lclLabel1.Text = "检测窗宽度";
		base.AutoScaleDimensions = new SizeF(6f, 12f);
		base.ClientSize = new Size(295, 268);
		base.Controls.Add(lclGroupBox1);
		base.Controls.Add(cbFgUse);
		base.Controls.Add(gvFg);
		base.Name = "FgDlg";
		Text = "馏分收集提示";
		base.Load += FgDlg_Load;
		base.Controls.SetChildIndex(btnOK, 0);
		base.Controls.SetChildIndex(btnCancel, 0);
		base.Controls.SetChildIndex(gvFg, 0);
		base.Controls.SetChildIndex(btnHelp, 0);
		base.Controls.SetChildIndex(cbFgUse, 0);
		base.Controls.SetChildIndex(lclGroupBox1, 0);
		((ISupportInitialize)gvFg).EndInit();
		lclGroupBox1.ResumeLayout(performLayout: false);
		lclGroupBox1.PerformLayout();
		ResumeLayout(performLayout: false);
		PerformLayout();
	}

	public bool NeedGather(float float_0)
	{
		for (int i = 0; i < fgRows.Length; i++)
		{
			if (fgRows[i].startT <= float_0 && float_0 <= fgRows[i].endT)
			{
				return true;
			}
		}
		return false;
	}
}
