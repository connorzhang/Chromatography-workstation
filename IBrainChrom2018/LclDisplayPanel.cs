using System;
using System.Drawing;
using System.Windows.Forms;

namespace IBrainChrom2018;

public class LclDisplayPanel : Panel
{
	public LclDisplayPanel()
	{
		SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, value: true);
		BackColor = Color.BlanchedAlmond;
	}

	protected override void OnResize(EventArgs eventargs)
	{
		base.OnResize(eventargs);
		Refresh();
	}
}
