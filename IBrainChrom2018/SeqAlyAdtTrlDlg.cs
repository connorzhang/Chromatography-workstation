using System.ComponentModel;

namespace IBrainChrom2018;

public class SeqAlyAdtTrlDlg : LclDialog
{
	private IContainer icontainer_1;

	public SeqAlyAdtTrlDlg()
	{
		icontainer_1 = null;
		InitializeComponent();
	}

	public SeqAlyAdtTrlDlg(Instrument instrument)
	{
		icontainer_1 = null;
		InitializeComponent();
		base.instrument = instrument;
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
		this.icontainer_1 = new System.ComponentModel.Container();
	}
}
