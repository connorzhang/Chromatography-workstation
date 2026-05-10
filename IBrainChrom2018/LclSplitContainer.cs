using System;
using System.Drawing;
using System.Windows.Forms;

namespace IBrainChrom2018;

public class LclSplitContainer : SplitContainer
{
	private Pen pen_0 = new Pen(Color.Black);

	protected override void OnMouseUp(MouseEventArgs mouseEventArgs_0)
	{
		base.OnMouseUp(mouseEventArgs_0);
		base.Panel1.Focus();
	}

	protected override void OnPaint(PaintEventArgs pevent)
	{
		base.OnPaint(pevent);
		Rectangle splitterRectangle = base.SplitterRectangle;
		if (base.Orientation == Orientation.Horizontal)
		{
			splitterRectangle.Height--;
		}
		else if (base.Orientation == Orientation.Vertical)
		{
			splitterRectangle.Width--;
		}
		pevent.Graphics.DrawRectangle(pen_0, splitterRectangle);
	}

	protected override void OnResize(EventArgs eventArgs_0)
	{
		base.OnResize(eventArgs_0);
		Refresh();
	}
}
