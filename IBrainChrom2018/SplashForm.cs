using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace IBrainChrom2018;

public class SplashForm : Form
{
	private IContainer icontainer_0;

	private LclLabel lbStr1;

	public string str1 = "*** RST ***";

	private Timer timer_0;

	public SplashForm()
	{
		InitializeComponent();
		BackgroundImage = ResourceImageLoad.LoadBitmap("Station\\splash");
		if (BackgroundImage != null)
		{
			base.ClientSize = BackgroundImage.Size;
		}
	}

	protected override void Dispose(bool disposing)
	{
		if (disposing && icontainer_0 != null)
		{
			icontainer_0.Dispose();
		}
		base.Dispose(disposing);
	}

	public new void Hide()
	{
		timer_0.Stop();
		base.Hide();
	}

	private void InitializeComponent()
	{
		this.icontainer_0 = new System.ComponentModel.Container();
		this.timer_0 = new System.Windows.Forms.Timer(this.icontainer_0);
		this.lbStr1 = new IBrainChrom2018.LclLabel();
		base.SuspendLayout();
		this.timer_0.Tick += new System.EventHandler(timer_0_Tick);
		this.lbStr1.AutoSize = true;
		this.lbStr1.BackColor = System.Drawing.Color.Transparent;
		this.lbStr1.Location = new System.Drawing.Point(131, 137);
		this.lbStr1.Name = "lbStr1";
		this.lbStr1.Size = new System.Drawing.Size(23, 12);
		this.lbStr1.TabIndex = 0;
		this.lbStr1.Text = "rst";
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(284, 262);
		base.Controls.Add(this.lbStr1);
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
		base.Name = "SplashForm";
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.Text = "SplashForm";
		base.Paint += new System.Windows.Forms.PaintEventHandler(SplashForm_Paint);
		base.ResumeLayout(false);
		base.PerformLayout();
	}

	public new void Show()
	{
		base.Show();
		timer_0.Start();
		Refresh();
	}

	private void SplashForm_Paint(object sender, PaintEventArgs e)
	{
		lbStr1.Text = str1;
	}

	private void timer_0_Tick(object sender, EventArgs e)
	{
		Refresh();
	}
}
