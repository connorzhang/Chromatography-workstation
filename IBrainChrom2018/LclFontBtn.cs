using System;
using System.Drawing;
using System.Windows.Forms;

namespace IBrainChrom2018;

public class LclFontBtn : LclButton
{
	private FontDialog fontDialog_0 = new FontDialog();

	public LclFontBtn()
	{
		fontDialog_0.ShowColor = true;
		base.Click += LclFontBtn_Click;
	}

	private void LclFontBtn_Click(object sender, EventArgs e)
	{
		fontDialog_0.Font = Font;
		fontDialog_0.Color = ForeColor;
		if (fontDialog_0.ShowDialog() == DialogResult.OK)
		{
			ForeColor = fontDialog_0.Color;
			Font = fontDialog_0.Font;
		}
	}

	public void SetFontColor(Font font, Color color)
	{
		Font = (Font)font.Clone();
		ForeColor = color;
	}
}
