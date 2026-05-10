using System.ComponentModel;

namespace IBrainChrom2018;

public class SZ_DtAboutForm : CtrlAboutDlg
{
	private IContainer icontainer_2;

	public SZ_DtAboutForm(string scnControlName, string senControlName)
		: base(scnControlName, senControlName)
	{
		icontainer_2 = null;
		InitializeComponent();
	}

	protected override void Dispose(bool disposing)
	{
		if (disposing && icontainer_2 != null)
		{
			icontainer_2.Dispose();
		}
		base.Dispose(disposing);
	}

	private void InitializeComponent()
	{
		this.icontainer_2 = new System.ComponentModel.Container();
	}

	public override void LoadLanguage()
	{
		base.LoadLanguage();
	}
}
