using System;
using System.Drawing;
using System.Windows.Forms;

namespace IBrainChrom2018;

public class LclColorBtn : LclButton
{
	private ColorDialog colorDialog_0 = new ColorDialog();

	private Color color_0 = Color.Green;

	private Rectangle rectangle_0;

	private Rectangle rectangle_1;

	private SolidBrush solidBrush_0 = new SolidBrush(Color.Black);

	public Color Color
	{
		get
		{
			return color_0;
		}
		set
		{
			color_0 = value;
			Refresh();
		}
	}

	public LclColorBtn()
	{
		base.Width = 100;
		TextAlign = ContentAlignment.MiddleLeft;
		rectangle_1 = new Rectangle(70, 4, 20, 14);
		rectangle_0 = rectangle_1;
		rectangle_0.Offset(2, 2);
		rectangle_0.Width -= 3;
		rectangle_0.Height -= 3;
		base.Click += LclColorBtn_Click;
	}

	private void LclColorBtn_Click(object sender, EventArgs e)
	{
		if (colorDialog_0.ShowDialog() == DialogResult.OK)
		{
			color_0 = colorDialog_0.Color;
			Refresh();
		}
	}

	protected override void OnPaint(PaintEventArgs pevent)
	{
		base.OnPaint(pevent);
		pevent.Graphics.DrawRectangle(Pens.Gray, rectangle_1);
		solidBrush_0.Color = color_0;
		pevent.Graphics.FillRectangle(solidBrush_0, rectangle_0);
	}
}
