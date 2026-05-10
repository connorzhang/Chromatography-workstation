using System;
using System.Drawing;
using System.Windows.Forms;

namespace IBrainChrom2018;

public class LclInstruButton : Button
{
	private bool bool_0;

	public int offset = 1;

	private TextureBrush textureBrush_0;

	private TextureBrush textureBrush_1;

	protected override void OnMouseEnter(EventArgs eventArgs_0)
	{
		bool_0 = true;
		base.OnMouseEnter(eventArgs_0);
	}

	protected override void OnMouseLeave(EventArgs eventArgs_0)
	{
		base.OnMouseLeave(eventArgs_0);
		bool_0 = false;
	}

	protected override void OnPaint(PaintEventArgs pevent)
	{
		try
		{
			if (bool_0)
			{
				pevent.Graphics.FillRectangle(textureBrush_0, base.ClientRectangle);
			}
			else
			{
				pevent.Graphics.FillRectangle(textureBrush_1, base.ClientRectangle);
			}
		}
		catch
		{
		}
	}

	protected override void OnPaintBackground(PaintEventArgs pevent)
	{
		base.OnPaintBackground(pevent);
	}

	public void SetStillImage(Image imgStill)
	{
		if (imgStill != null)
		{
			base.Image = imgStill;
			textureBrush_1 = new TextureBrush(imgStill);
			textureBrush_0 = new TextureBrush(imgStill);
			textureBrush_0.TranslateTransform(offset, offset);
		}
	}
}
