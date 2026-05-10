using System.Drawing;
using System.Windows.Forms;

namespace IBrainChrom2018;

public class Lcl_SignalButton : Button
{
	private Rectangle rectangle_0;

	private Color color_0 = Color.Red;

	private Rectangle rectangle_1;

	private bool bool_0;

	private SolidBrush solidBrush_0 = new SolidBrush(Color.Gray);

	public Color Color
	{
		get
		{
			return color_0;
		}
		set
		{
			color_0 = value;
			Invalidate();
		}
	}

	public bool HasSignal
	{
		get
		{
			return bool_0;
		}
		set
		{
			bool_0 = value;
			Refresh();
		}
	}

	public int SignalNo => (int)base.Tag;

	public Lcl_SignalButton()
	{
		AutoSize = false;
		Cursor = Cursors.Hand;
		base.Size = new Size(15, 13);
		rectangle_0.Size = base.Size;
		rectangle_1 = new Rectangle(3, 3, 9, 7);
	}

	protected override void OnPaint(PaintEventArgs pevent)
	{
		solidBrush_0.Color = color_0;
		pevent.Graphics.FillRectangle(solidBrush_0, rectangle_0);
		if (!bool_0)
		{
			solidBrush_0.Color = SystemColors.Control;
			pevent.Graphics.FillRectangle(solidBrush_0, rectangle_1);
		}
	}
}
