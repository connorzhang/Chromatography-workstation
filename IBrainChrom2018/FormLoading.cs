using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace IBrainChrom2018;

public class FormLoading : Form
{
	public bool bClose = false;

	public int iCntClose = 0;

	private IContainer components = null;

	private Timer timer1;

	public FormLoading()
	{
		InitializeComponent();
		base.StartPosition = FormStartPosition.CenterScreen;
		timer1.Enabled = true;
	}

	private void timer1_Tick(object sender, EventArgs e)
	{
		iCntClose++;
		if (iCntClose > 1)
		{
			base.DialogResult = DialogResult.OK;
			Close();
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(IBrainChrom2018.FormLoading));
		this.timer1 = new System.Windows.Forms.Timer(this.components);
		base.SuspendLayout();
		this.timer1.Interval = 1000;
		this.timer1.Tick += new System.EventHandler(timer1_Tick);
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		this.BackgroundImage = (System.Drawing.Image)resources.GetObject("$this.BackgroundImage");
		base.ClientSize = new System.Drawing.Size(464, 441);
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
		base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
		base.Name = "FormLoading";
		this.Text = "FormLoading";
		base.ResumeLayout(false);
	}
}
