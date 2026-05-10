using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Printing;
using System.Windows.Forms;
using HZH_Controls.Controls;

namespace IBrainChrom2018;

public class FormPrintf : Form
{
	private PaperSize ps = new PaperSize("Custom Size 1", 189, 1169);

	private IContainer components = null;

	private PrintDocument printDocument1;

	private UCBtnExt ucBtnPrintf;

	public RichTextBox rtprtb;

	public FormPrintf()
	{
		InitializeComponent();
	}

	private void printDocument1_PrintPage(object sender, PrintPageEventArgs e)
	{
		Graphics graphics = e.Graphics;
		using Font font = new Font("Lucda Console", 4f);
		Font font2 = new Font("Lucda Console", 6f);
		graphics.DrawString("                  便携式微量硫分析仪数据报表", font2, Brushes.Black, 0f, 0f);
		graphics.DrawString(rtprtb.Text, font, Brushes.Black, 0f, 10f);
	}

	private void ucBtnPrintf_BtnClick(object sender, EventArgs e)
	{
		printDocument1.Print();
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(IBrainChrom2018.FormPrintf));
		this.printDocument1 = new System.Drawing.Printing.PrintDocument();
		this.rtprtb = new System.Windows.Forms.RichTextBox();
		this.ucBtnPrintf = new HZH_Controls.Controls.UCBtnExt();
		base.SuspendLayout();
		this.printDocument1.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(printDocument1_PrintPage);
		this.rtprtb.Dock = System.Windows.Forms.DockStyle.Top;
		this.rtprtb.Font = new System.Drawing.Font("宋体", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		this.rtprtb.Location = new System.Drawing.Point(0, 0);
		this.rtprtb.Name = "rtprtb";
		this.rtprtb.Size = new System.Drawing.Size(359, 502);
		this.rtprtb.TabIndex = 0;
		this.rtprtb.Text = "";
		this.ucBtnPrintf.BackColor = System.Drawing.Color.White;
		this.ucBtnPrintf.BtnBackColor = System.Drawing.Color.White;
		this.ucBtnPrintf.BtnFont = new System.Drawing.Font("微软雅黑", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		this.ucBtnPrintf.BtnForeColor = System.Drawing.Color.White;
		this.ucBtnPrintf.BtnText = "打印";
		this.ucBtnPrintf.ConerRadius = 30;
		this.ucBtnPrintf.Cursor = System.Windows.Forms.Cursors.Hand;
		this.ucBtnPrintf.EnabledMouseEffect = false;
		this.ucBtnPrintf.FillColor = System.Drawing.Color.FromArgb(255, 77, 59);
		this.ucBtnPrintf.Font = new System.Drawing.Font("微软雅黑", 15f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
		this.ucBtnPrintf.IsRadius = true;
		this.ucBtnPrintf.IsShowRect = true;
		this.ucBtnPrintf.IsShowTips = false;
		this.ucBtnPrintf.Location = new System.Drawing.Point(59, 519);
		this.ucBtnPrintf.Margin = new System.Windows.Forms.Padding(0);
		this.ucBtnPrintf.Name = "ucBtnPrintf";
		this.ucBtnPrintf.RectColor = System.Drawing.Color.FromArgb(255, 77, 58);
		this.ucBtnPrintf.RectWidth = 1;
		this.ucBtnPrintf.Size = new System.Drawing.Size(231, 60);
		this.ucBtnPrintf.TabIndex = 1;
		this.ucBtnPrintf.TabStop = false;
		this.ucBtnPrintf.TipsColor = System.Drawing.Color.FromArgb(232, 30, 99);
		this.ucBtnPrintf.TipsText = "";
		this.ucBtnPrintf.BtnClick += new System.EventHandler(ucBtnPrintf_BtnClick);
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(359, 588);
		base.Controls.Add(this.ucBtnPrintf);
		base.Controls.Add(this.rtprtb);
		base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
		base.Name = "FormPrintf";
		this.Text = "FormPrintf";
		base.ResumeLayout(false);
	}
}
