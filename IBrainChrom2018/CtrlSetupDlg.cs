using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace IBrainChrom2018;

public class CtrlSetupDlg : LclDialog
{
	private const string string_0 = "设置";

	private const string string_1 = "Setup";

	private IContainer icontainer_1;

	protected string scnTitle;

	protected string senTitle;

	public CtrlSetupDlg()
	{
		scnTitle = "";
		senTitle = "";
		icontainer_1 = null;
		InitializeComponent();
	}

	public CtrlSetupDlg(string scnControlName, string senControlName)
	{
		scnTitle = "";
		senTitle = "";
		icontainer_1 = null;
		InitializeComponent();
		scnTitle = scnControlName + "设置";
		senTitle = senControlName + " Setup";
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
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
		base.ClientSize = new System.Drawing.Size(347, 184);
		base.Name = "CtrlSetupDlg";
		base.ResumeLayout(false);
	}

	public virtual void LoadControl(SysCfgControl sysCfgControl)
	{
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

	public new DialogResult ShowDialog()
	{
		LoadLanguage();
		return base.ShowDialog();
	}

	public virtual void WriteControl(SysCfgControl sysCfgControl)
	{
	}
}
