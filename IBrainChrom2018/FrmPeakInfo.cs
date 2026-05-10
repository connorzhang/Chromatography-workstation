using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace IBrainChrom2018;

public class FrmPeakInfo : Form
{
	private IContainer icontainer_0;

	public Label LabelPeakInfo;

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
		this.LabelPeakInfo = new System.Windows.Forms.Label();
		base.SuspendLayout();
		this.LabelPeakInfo.AutoSize = true;
		this.LabelPeakInfo.Location = new System.Drawing.Point(24, 38);
		this.LabelPeakInfo.Name = "LabelPeakInfo";
		this.LabelPeakInfo.Size = new System.Drawing.Size(83, 12);
		this.LabelPeakInfo.TabIndex = 0;
		this.LabelPeakInfo.Text = "LabelPeakInfo";
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(244, 318);
		base.Controls.Add(this.LabelPeakInfo);
		base.MaximizeBox = false;
		base.MinimizeBox = false;
		base.Name = "FrmPeakInfo";
		base.ShowIcon = false;
		base.ShowInTaskbar = false;
		this.Text = "PeakInfo";
		base.Load += new System.EventHandler(FrmPeakInfo_Load);
		base.ResumeLayout(false);
		base.PerformLayout();
	}

	public FrmPeakInfo()
	{
		InitializeComponent();
	}

	private void FrmPeakInfo_Load(object sender, EventArgs e)
	{
	}
}
