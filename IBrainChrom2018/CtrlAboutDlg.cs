using System.ComponentModel;
using System.Drawing;

namespace IBrainChrom2018;

public class CtrlAboutDlg : LclDialog
{
	private IContainer icontainer_1;

	protected string scnTitle;

	protected string senTitle;

	public CtrlAboutDlg()
	{
		scnTitle = "";
		senTitle = "";
		icontainer_1 = null;
		InitializeComponent();
	}

	public CtrlAboutDlg(string scnControlName, string senControlName)
	{
		scnTitle = "";
		senTitle = "";
		icontainer_1 = null;
		InitializeComponent();
		scnTitle = "关于" + scnControlName;
		senTitle = "About " + senControlName;
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
		base.SuspendLayout();
		base.btnOK.Location = new System.Drawing.Point(241, 149);
		base.btnCancel.Location = new System.Drawing.Point(179, 12);
		base.btnCancel.Visible = false;
		base.btnHelp.Location = new System.Drawing.Point(260, 12);
		base.btnHelp.Visible = false;
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
		base.ClientSize = new System.Drawing.Size(347, 184);
		base.Name = "CtrlAboutDlg";
		base.ResumeLayout(false);
	}

	public override void LoadLanguage()
	{
		base.LoadLanguage();
		switch (Class49.sysLanguage_0)
		{
		case SysLanguage.CN:
			Text = scnTitle;
			break;
		case SysLanguage.EN:
			Text = senTitle;
			break;
		}
	}

	public new void ShowDialog()
	{
		LoadLanguage();
		base.ShowDialog();
	}
}
