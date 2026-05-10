using System;
using System.Drawing;
using System.Windows.Forms;

namespace IBrainChrom2018;

public class LclExpressLabel : Label
{
	public new bool AutoSize
	{
		get
		{
			base.AutoSize = false;
			return false;
		}
	}

	public LclExpressLabel()
	{
		TextAlign = ContentAlignment.MiddleCenter;
	}

	protected override void OnSizeChanged(EventArgs eventArgs_0)
	{
		base.OnSizeChanged(eventArgs_0);
		SetText(Text);
	}

	public void SetText(string text)
	{
		Graphics graphics = Graphics.FromHwnd(base.Handle);
		Text = text;
		base.Height = Convert.ToInt32(graphics.MeasureString(text, Font, base.Width).Height);
	}
}
