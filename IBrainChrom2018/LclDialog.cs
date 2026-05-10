using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace IBrainChrom2018;

public class LclDialog : Form
{
	protected LclButton btnCancel;

	protected LclButton btnHelp;

	protected LclButton btnOK;

	private IContainer icontainer_0;

	private bool bool_0;

	public Instrument instrument;

	public LclDialog()
	{
		InitializeComponent();
	}

	private void btnHelp_Click(object sender, EventArgs e)
	{
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
		this.btnHelp = new IBrainChrom2018.LclButton();
		this.btnCancel = new IBrainChrom2018.LclButton();
		this.btnOK = new IBrainChrom2018.LclButton();
		base.SuspendLayout();
		this.btnHelp.Location = new System.Drawing.Point(243, 151);
		this.btnHelp.Name = "btnHelp";
		this.btnHelp.Size = new System.Drawing.Size(75, 25);
		this.btnHelp.TabIndex = 0;
		this.btnHelp.Text = "Help";
		this.btnHelp.UseVisualStyleBackColor = true;
		this.btnHelp.Click += new System.EventHandler(btnHelp_Click);
		this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
		this.btnCancel.Location = new System.Drawing.Point(138, 151);
		this.btnCancel.Name = "btnCancel";
		this.btnCancel.Size = new System.Drawing.Size(75, 25);
		this.btnCancel.TabIndex = 0;
		this.btnCancel.Text = "Cancel";
		this.btnCancel.UseVisualStyleBackColor = true;
		this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
		this.btnOK.Location = new System.Drawing.Point(36, 151);
		this.btnOK.Name = "btnOK";
		this.btnOK.Size = new System.Drawing.Size(75, 25);
		this.btnOK.TabIndex = 0;
		this.btnOK.Text = "OK";
		this.btnOK.UseVisualStyleBackColor = true;
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(347, 199);
		base.Controls.Add(this.btnHelp);
		base.Controls.Add(this.btnCancel);
		base.Controls.Add(this.btnOK);
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
		base.KeyPreview = true;
		base.MaximizeBox = false;
		base.MinimizeBox = false;
		base.Name = "LclDialog";
		base.ShowInTaskbar = false;
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
		this.Text = "LclDialog";
		base.Load += new System.EventHandler(LclDialog_Load);
		base.KeyDown += new System.Windows.Forms.KeyEventHandler(LclDialog_KeyDown);
		base.ResumeLayout(false);
	}

	private void LclDialog_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode == Keys.Escape)
		{
			Close();
		}
	}

	private void LclDialog_Load(object sender, EventArgs e)
	{
		if (!bool_0)
		{
			LoadLanguage();
			bool_0 = true;
		}
	}

	public virtual void LoadLanguage()
	{
		switch (Class49.sysLanguage_0)
		{
		case SysLanguage.CN:
			btnOK.Text = "确认";
			btnCancel.Text = "取消";
			btnHelp.Text = "帮助";
			break;
		case SysLanguage.EN:
			btnOK.Text = "OK";
			btnCancel.Text = "Cancel";
			btnHelp.Text = "Help";
			break;
		}
	}
}
