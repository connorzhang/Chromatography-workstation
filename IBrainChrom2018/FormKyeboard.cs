using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using HZH_Controls.Controls;

namespace IBrainChrom2018;

public class FormKyeboard : Form
{
	private IContainer components = null;

	private UCKeyBorderNum ucKeyBorderNum1;

	public FormKyeboard()
	{
		InitializeComponent();
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(IBrainChrom2018.FormKyeboard));
		this.ucKeyBorderNum1 = new HZH_Controls.Controls.UCKeyBorderNum();
		base.SuspendLayout();
		this.ucKeyBorderNum1.BackColor = System.Drawing.Color.White;
		this.ucKeyBorderNum1.Dock = System.Windows.Forms.DockStyle.Fill;
		this.ucKeyBorderNum1.Location = new System.Drawing.Point(0, 0);
		this.ucKeyBorderNum1.Name = "ucKeyBorderNum1";
		this.ucKeyBorderNum1.Size = new System.Drawing.Size(337, 205);
		this.ucKeyBorderNum1.TabIndex = 0;
		this.ucKeyBorderNum1.UseCustomEvent = false;
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(337, 205);
		base.Controls.Add(this.ucKeyBorderNum1);
		base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
		base.Name = "FormKyeboard";
		this.Text = "FormKyeboard";
		base.ResumeLayout(false);
	}
}
