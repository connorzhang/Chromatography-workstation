using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace IBrainChrom2018;

public class LbTextDlg : LclDialog
{
	private LclFontBtn btnFont;

	private IContainer icontainer_1;

	public LbText lbText;

	private LclLabel lclLabel1;

	private LclLabel lclLabel2;

	private LclLabel lclLabel3;

	private LclNumericUpDown nudDegree;

	public LclTextBox tbText;

	public LbTextDlg()
	{
		InitializeComponent();
	}

	private void method_0(object sender, EventArgs e)
	{
		lbText = null;
		string text = tbText.Text.Trim();
		if (!(text == ""))
		{
			lbText = new LbText();
			lbText.text = text;
			lbText.int_0 = (int)nudDegree.Value;
			lbText.font = btnFont.Font;
			lbText.color_0 = btnFont.ForeColor;
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
		this.lclLabel1 = new IBrainChrom2018.LclLabel();
		this.tbText = new IBrainChrom2018.LclTextBox();
		this.btnFont = new IBrainChrom2018.LclFontBtn();
		this.lclLabel2 = new IBrainChrom2018.LclLabel();
		this.nudDegree = new IBrainChrom2018.LclNumericUpDown();
		this.lclLabel3 = new IBrainChrom2018.LclLabel();
		((System.ComponentModel.ISupportInitialize)this.nudDegree).BeginInit();
		base.SuspendLayout();
		base.btnCancel.Location = new System.Drawing.Point(260, 43);
		base.btnCancel.Text = "取消";
		base.btnCancel.Click += new System.EventHandler(method_1);
		base.btnHelp.Location = new System.Drawing.Point(260, 74);
		base.btnHelp.Text = "帮助";
		base.btnHelp.Click += new System.EventHandler(method_2);
		base.btnOK.Location = new System.Drawing.Point(260, 12);
		base.btnOK.Text = "确认";
		base.btnOK.Click += new System.EventHandler(method_0);
		this.lclLabel1.AutoSize = true;
		this.lclLabel1.Location = new System.Drawing.Point(12, 9);
		this.lclLabel1.Name = "lclLabel1";
		this.lclLabel1.Size = new System.Drawing.Size(29, 12);
		this.lclLabel1.TabIndex = 1;
		this.lclLabel1.Text = "文本";
		this.tbText.Location = new System.Drawing.Point(15, 25);
		this.tbText.Name = "tbText";
		this.tbText.Size = new System.Drawing.Size(220, 21);
		this.tbText.TabIndex = 2;
		this.btnFont.Location = new System.Drawing.Point(15, 108);
		this.btnFont.Name = "btnFont";
		this.btnFont.Size = new System.Drawing.Size(75, 23);
		this.btnFont.TabIndex = 3;
		this.btnFont.Text = "字体";
		this.btnFont.UseVisualStyleBackColor = true;
		this.lclLabel2.AutoSize = true;
		this.lclLabel2.Location = new System.Drawing.Point(140, 95);
		this.lclLabel2.Name = "lclLabel2";
		this.lclLabel2.Size = new System.Drawing.Size(29, 12);
		this.lclLabel2.TabIndex = 1;
		this.lclLabel2.Text = "倾斜";
		this.nudDegree.Location = new System.Drawing.Point(143, 111);
		System.Windows.Forms.NumericUpDown numericUpDown = this.nudDegree;
		numericUpDown.Maximum = new decimal(new int[4] { 360, 0, 0, 0 });
		this.nudDegree.Name = "nudDegree";
		this.nudDegree.Size = new System.Drawing.Size(54, 21);
		this.nudDegree.TabIndex = 4;
		this.lclLabel3.AutoSize = true;
		this.lclLabel3.Location = new System.Drawing.Point(203, 113);
		this.lclLabel3.Name = "lclLabel3";
		this.lclLabel3.Size = new System.Drawing.Size(17, 12);
		this.lclLabel3.TabIndex = 1;
		this.lclLabel3.Text = "度";
		base.ClientSize = new System.Drawing.Size(347, 145);
		base.Controls.Add(this.nudDegree);
		base.Controls.Add(this.btnFont);
		base.Controls.Add(this.tbText);
		base.Controls.Add(this.lclLabel3);
		base.Controls.Add(this.lclLabel2);
		base.Controls.Add(this.lclLabel1);
		base.Name = "LbTextDlg";
		this.Text = "文本标签";
		base.Controls.SetChildIndex(base.btnCancel, 0);
		base.Controls.SetChildIndex(base.btnOK, 0);
		base.Controls.SetChildIndex(base.btnHelp, 0);
		base.Controls.SetChildIndex(this.lclLabel1, 0);
		base.Controls.SetChildIndex(this.lclLabel2, 0);
		base.Controls.SetChildIndex(this.lclLabel3, 0);
		base.Controls.SetChildIndex(this.tbText, 0);
		base.Controls.SetChildIndex(this.btnFont, 0);
		base.Controls.SetChildIndex(this.nudDegree, 0);
		((System.ComponentModel.ISupportInitialize)this.nudDegree).EndInit();
		base.ResumeLayout(false);
		base.PerformLayout();
	}

	private void method_1(object sender, EventArgs e)
	{
		tbText.Text = "";
		Hide();
	}

	private void method_2(object sender, EventArgs e)
	{
	}
}
