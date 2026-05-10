using System.ComponentModel;

namespace IBrainChrom2018;

public class DetectorControl1AboutForm : CtrlAboutDlg
{
	private IContainer icontainer_2;

	public DetectorControl1AboutForm()
	{
		icontainer_2 = null;
		method_0();
	}

	public DetectorControl1AboutForm(string scnControlName, string senControlName)
		: base(scnControlName, senControlName)
	{
		icontainer_2 = null;
		method_0();
	}

	protected override void Dispose(bool disposing)
	{
		if (disposing && icontainer_2 != null)
		{
			icontainer_2.Dispose();
		}
		base.Dispose(disposing);
	}

	private void method_0()
	{
		icontainer_2 = new Container();
	}

	public override void LoadLanguage()
	{
		base.LoadLanguage();
	}
}
