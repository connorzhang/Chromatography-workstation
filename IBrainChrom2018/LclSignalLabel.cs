using System;
using System.Drawing;
using System.Windows.Forms;

namespace IBrainChrom2018;

public class LclSignalLabel : Label
{
	public int disNo;

	private static StringFormat stringFormat_0 = new StringFormat();

	public bool Bold
	{
		get
		{
			return Font.Bold;
		}
		set
		{
			FontStyle style = Font.Style;
			style = ((!value) ? (style & ~FontStyle.Bold) : (style | FontStyle.Bold));
			Font = new Font(Font, style);
		}
	}

	public LclSignalLabel()
	{
		BackColor = Color.Transparent;
		Cursor = Cursors.Hand;
		AutoSize = false;
		stringFormat_0.FormatFlags = StringFormatFlags.NoWrap;
		base.AutoEllipsis = true;
	}

	public void Set(string text, int width)
	{
		Text = text;
		SizeF sizeF = Graphics.FromHwnd(base.Handle).MeasureString(text, Font, width, stringFormat_0);
		base.Width = width;
		base.Height = Convert.ToInt32(sizeF.Height);
	}
}
