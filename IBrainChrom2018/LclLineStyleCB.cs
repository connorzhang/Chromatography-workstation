using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace IBrainChrom2018;

public class LclLineStyleCB : ComboBox
{
	private const int int_0 = 5;

	private Pen pen_0 = new Pen(Color.Black);

	private SolidBrush solidBrush_0 = new SolidBrush(Color.White);

	public LclLineStyleCB()
	{
		base.DrawMode = DrawMode.OwnerDrawFixed;
	}

	public void AddItems()
	{
		base.Items.Clear();
		for (int i = 0; i < 5; i++)
		{
			base.Items.Add(new object());
		}
	}

	protected override void OnDrawItem(DrawItemEventArgs drawItemEventArgs_0)
	{
		solidBrush_0.Color = Color.White;
		pen_0.Color = Color.Black;
		if ((drawItemEventArgs_0.State & DrawItemState.Selected) == DrawItemState.Selected || (drawItemEventArgs_0.State & DrawItemState.HotLight) == DrawItemState.HotLight)
		{
			solidBrush_0.Color = Color.Blue;
			pen_0.Color = Color.White;
		}
		drawItemEventArgs_0.Graphics.FillRectangle(solidBrush_0, drawItemEventArgs_0.Bounds);
		Point location = drawItemEventArgs_0.Bounds.Location;
		location.X += 5;
		location.Y += drawItemEventArgs_0.Bounds.Height / 2;
		Point pt = location;
		pt.X = drawItemEventArgs_0.Bounds.Width - 15;
		pen_0.DashStyle = retStyle(drawItemEventArgs_0.Index);
		drawItemEventArgs_0.Graphics.DrawLine(pen_0, location, pt);
	}

	public int retIndex(DashStyle dashStyle)
	{
		return dashStyle switch
		{
			DashStyle.Solid => 0, 
			DashStyle.Dash => 1, 
			DashStyle.Dot => 2, 
			DashStyle.DashDot => 3, 
			DashStyle.DashDotDot => 4, 
			_ => -1, 
		};
	}

	public DashStyle retStyle(int index)
	{
		return index switch
		{
			0 => DashStyle.Solid, 
			1 => DashStyle.Dash, 
			2 => DashStyle.Dot, 
			3 => DashStyle.DashDot, 
			4 => DashStyle.DashDotDot, 
			_ => DashStyle.Dot, 
		};
	}
}
