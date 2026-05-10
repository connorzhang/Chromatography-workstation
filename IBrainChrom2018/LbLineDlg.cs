using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace IBrainChrom2018;

public class LbLineDlg : LclDialog
{
	private LclColorBtn btnColor;

	private IContainer icontainer_1;

	public LbLine lbLine;

	private LclLabel lclLabel1;

	private LclLabel lclLabel2;

	private LclLineStyleCB lscbStyle;

	private LclNumericUpDown nudWidth;

	public PointF pointF_0;

	public LbLineDlg()
	{
		InitializeComponent();
		lscbStyle.AddItems();
	}

	private void method_0(object sender, EventArgs e)
	{
		lbLine = new LbLine();
		lbLine.pointF_0 = pointF_0;
		lbLine.style = lscbStyle.retStyle(lscbStyle.SelectedIndex);
		lbLine.int_0 = (int)nudWidth.Value;
		lbLine.color_0 = btnColor.Color;
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
		this.nudWidth = new IBrainChrom2018.LclNumericUpDown();
		this.lclLabel2 = new IBrainChrom2018.LclLabel();
		this.lclLabel1 = new IBrainChrom2018.LclLabel();
		this.lscbStyle = new IBrainChrom2018.LclLineStyleCB();
		this.btnColor = new IBrainChrom2018.LclColorBtn();
		((System.ComponentModel.ISupportInitialize)this.nudWidth).BeginInit();
		base.SuspendLayout();
		base.btnCancel.Location = new System.Drawing.Point(260, 43);
		base.btnCancel.Text = "取消";
		base.btnCancel.Click += new System.EventHandler(method_1);
		base.btnHelp.Location = new System.Drawing.Point(260, 74);
		base.btnHelp.Text = "帮助";
		base.btnOK.Location = new System.Drawing.Point(260, 12);
		base.btnOK.Text = "确认";
		base.btnOK.Click += new System.EventHandler(method_0);
		this.nudWidth.Location = new System.Drawing.Point(15, 99);
		int[] bits = new int[4] { 1, 0, 0, 0 };
		this.nudWidth.Minimum = new decimal(bits);
		this.nudWidth.Name = "nudWidth";
		this.nudWidth.Size = new System.Drawing.Size(54, 21);
		this.nudWidth.TabIndex = 10;
		int[] bits2 = new int[4] { 1, 0, 0, 0 };
		this.nudWidth.Value = new decimal(bits2);
		this.lclLabel2.AutoSize = true;
		this.lclLabel2.Location = new System.Drawing.Point(12, 83);
		this.lclLabel2.Name = "lclLabel2";
		this.lclLabel2.Size = new System.Drawing.Size(29, 12);
		this.lclLabel2.TabIndex = 6;
		this.lclLabel2.Text = "线宽";
		this.lclLabel1.AutoSize = true;
		this.lclLabel1.Location = new System.Drawing.Point(12, 18);
		this.lclLabel1.Name = "lclLabel1";
		this.lclLabel1.Size = new System.Drawing.Size(29, 12);
		this.lclLabel1.TabIndex = 7;
		this.lclLabel1.Text = "线型";
		this.lscbStyle.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
		this.lscbStyle.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.lscbStyle.FormattingEnabled = true;
		this.lscbStyle.Location = new System.Drawing.Point(15, 34);
		this.lscbStyle.Name = "lscbStyle";
		this.lscbStyle.Size = new System.Drawing.Size(107, 22);
		this.lscbStyle.TabIndex = 13;
		this.btnColor.Color = System.Drawing.Color.Green;
		this.btnColor.Location = new System.Drawing.Point(128, 96);
		this.btnColor.Name = "btnColor";
		this.btnColor.Size = new System.Drawing.Size(100, 23);
		this.btnColor.TabIndex = 14;
		this.btnColor.Text = "颜色";
		this.btnColor.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.btnColor.UseVisualStyleBackColor = true;
		base.ClientSize = new System.Drawing.Size(347, 138);
		base.Controls.Add(this.btnColor);
		base.Controls.Add(this.lscbStyle);
		base.Controls.Add(this.nudWidth);
		base.Controls.Add(this.lclLabel2);
		base.Controls.Add(this.lclLabel1);
		base.Name = "LbLineDlg";
		this.Text = "直线标签";
		base.Controls.SetChildIndex(base.btnHelp, 0);
		base.Controls.SetChildIndex(base.btnCancel, 0);
		base.Controls.SetChildIndex(base.btnOK, 0);
		base.Controls.SetChildIndex(this.lclLabel1, 0);
		base.Controls.SetChildIndex(this.lclLabel2, 0);
		base.Controls.SetChildIndex(this.nudWidth, 0);
		base.Controls.SetChildIndex(this.lscbStyle, 0);
		base.Controls.SetChildIndex(this.btnColor, 0);
		((System.ComponentModel.ISupportInitialize)this.nudWidth).EndInit();
		base.ResumeLayout(false);
		base.PerformLayout();
	}

	private void method_1(object sender, EventArgs e)
	{
	}
}
