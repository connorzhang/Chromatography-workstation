using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace IBrainChrom2018;

public class LclTabControl : TabControl
{
	private Pen pen_0 = new Pen(Color.Black);

	private SolidBrush solidBrush_0 = new SolidBrush(Color.Black);

	private Pen pen_1 = new Pen(Color.LightGray);

	private Pen pen_2 = new Pen(SystemColors.Control);

	private Point point_0;

	private Point point_1;

	private Rectangle rectangle_0;

	private SolidBrush solidBrush_1 = new SolidBrush(SystemColors.Control);

	private SolidBrush solidBrush_2 = new SolidBrush(Color.FromArgb(160, 220, 220, 220));

	private SolidBrush solidBrush_3 = new SolidBrush(SystemColors.Control);

	private StringFormat stringFormat_0 = new StringFormat();

	public TabStyle tabStyle;

	public new TabAlignment Alignment
	{
		get
		{
			return base.Alignment;
		}
		set
		{
			base.Alignment = value;
			switch (base.Alignment)
			{
			case TabAlignment.Top:
			case TabAlignment.Bottom:
				if (tabStyle != TabStyle.Normal)
				{
					base.ItemSize = new Size(90, 16);
				}
				else
				{
					base.ItemSize = new Size(90, 19);
				}
				base.SizeMode = TabSizeMode.Normal;
				break;
			case TabAlignment.Left:
			case TabAlignment.Right:
				if (tabStyle != TabStyle.Normal)
				{
					base.ItemSize = new Size(16, 90);
				}
				else
				{
					base.ItemSize = new Size(19, 90);
				}
				base.SizeMode = TabSizeMode.Fixed;
				break;
			}
		}
	}

	public override Rectangle DisplayRectangle
	{
		get
		{
			Rectangle displayRectangle = base.DisplayRectangle;
			if (tabStyle == TabStyle.Special)
			{
				switch (Alignment)
				{
				case TabAlignment.Top:
				case TabAlignment.Left:
				case TabAlignment.Right:
					return displayRectangle;
				case TabAlignment.Bottom:
				{
					int num = displayRectangle.Left - 1;
					int num2 = displayRectangle.Top - 1;
					displayRectangle.X -= num;
					displayRectangle.Width += num + num;
					displayRectangle.Y -= num2;
					displayRectangle.Height += num2;
					return displayRectangle;
				}
				}
			}
			return displayRectangle;
		}
	}

	public LclTabControl()
	{
		SetStyle(ControlStyles.UserPaint, value: true);
		SetStyle(ControlStyles.OptimizedDoubleBuffer, value: true);
		base.ItemSize = new Size(90, 19);
		base.ResizeRedraw = true;
		stringFormat_0.Alignment = StringAlignment.Center;
		stringFormat_0.LineAlignment = StringAlignment.Center;
		stringFormat_0.Trimming = StringTrimming.EllipsisCharacter;
		stringFormat_0.FormatFlags = StringFormatFlags.NoWrap;
	}

	private GraphicsPath method_0(int int_0)
	{
		GraphicsPath graphicsPath = new GraphicsPath();
		graphicsPath.Reset();
		Rectangle tabRect = GetTabRect(int_0);
		Point[] array = new Point[4];
		if (tabStyle == TabStyle.Normal)
		{
			switch (Alignment)
			{
			case TabAlignment.Bottom:
				array[0] = new Point(tabRect.Left + 1, tabRect.Top);
				array[1] = new Point(tabRect.Left + 1, tabRect.Bottom);
				array[2] = new Point(tabRect.Right - 1, tabRect.Bottom);
				array[3] = new Point(tabRect.Right - 1, tabRect.Top);
				break;
			case TabAlignment.Left:
				array[0] = new Point(tabRect.Right, tabRect.Top + 1);
				array[1] = new Point(tabRect.Left, tabRect.Top + 1);
				array[2] = new Point(tabRect.Left, tabRect.Bottom - 1);
				array[3] = new Point(tabRect.Right, tabRect.Bottom - 1);
				break;
			}
		}
		else if (tabStyle == TabStyle.Special)
		{
			TabAlignment tabAlignment = Alignment;
			if (tabAlignment == TabAlignment.Bottom)
			{
				array[0] = new Point(tabRect.Left + 1, tabRect.Top);
				array[1] = new Point(tabRect.Left + 5 + 1, tabRect.Bottom + 1);
				array[2] = new Point(tabRect.Right - 5 - 1, tabRect.Bottom + 1);
				array[3] = new Point(tabRect.Right - 1, tabRect.Top);
			}
		}
		graphicsPath.AddLines(array);
		return graphicsPath;
	}

	private GraphicsPath method_1(int int_0)
	{
		GraphicsPath graphicsPath = new GraphicsPath();
		graphicsPath.Reset();
		Rectangle tabRect = GetTabRect(int_0);
		Point[] array = new Point[4];
		if (tabStyle != TabStyle.Normal)
		{
			if (tabStyle == TabStyle.Special)
			{
				TabAlignment tabAlignment = Alignment;
				if (tabAlignment == TabAlignment.Bottom)
				{
					array[0] = new Point(tabRect.Left, tabRect.Top);
					array[1] = new Point(tabRect.Left + 5, tabRect.Bottom + 1);
					array[2] = new Point(tabRect.Right - 5, tabRect.Bottom + 1);
					array[3] = new Point(tabRect.Right, tabRect.Top);
				}
			}
		}
		else
		{
			switch (Alignment)
			{
			case TabAlignment.Top:
				if (int_0 == base.SelectedIndex)
				{
					array[0] = new Point(tabRect.Left, tabRect.Bottom);
					array[1] = new Point(tabRect.Left, tabRect.Top - 1);
					array[2] = new Point(tabRect.Right, tabRect.Top - 1);
					array[3] = new Point(tabRect.Right, tabRect.Bottom);
				}
				else
				{
					array[0] = new Point(tabRect.Left, tabRect.Bottom);
					array[1] = new Point(tabRect.Left, tabRect.Top);
					array[2] = new Point(tabRect.Right, tabRect.Top);
					array[3] = new Point(tabRect.Right, tabRect.Bottom);
				}
				break;
			case TabAlignment.Bottom:
				if (int_0 == base.SelectedIndex)
				{
					array[0] = new Point(tabRect.Left, tabRect.Top - 1);
					array[1] = new Point(tabRect.Left, tabRect.Bottom + 1);
					array[2] = new Point(tabRect.Right, tabRect.Bottom + 1);
					array[3] = new Point(tabRect.Right, tabRect.Top - 1);
				}
				else
				{
					array[0] = new Point(tabRect.Left, tabRect.Top);
					array[1] = new Point(tabRect.Left, tabRect.Bottom);
					array[2] = new Point(tabRect.Right, tabRect.Bottom);
					array[3] = new Point(tabRect.Right, tabRect.Top);
				}
				break;
			case TabAlignment.Left:
				if (int_0 == base.SelectedIndex)
				{
					array[0] = new Point(tabRect.Right, tabRect.Top);
					array[1] = new Point(tabRect.Left - 1, tabRect.Top);
					array[2] = new Point(tabRect.Left - 1, tabRect.Bottom);
					array[3] = new Point(tabRect.Right, tabRect.Bottom);
				}
				else
				{
					array[0] = new Point(tabRect.Right, tabRect.Top);
					array[1] = new Point(tabRect.Left, tabRect.Top);
					array[2] = new Point(tabRect.Left, tabRect.Bottom);
					array[3] = new Point(tabRect.Right, tabRect.Bottom);
				}
				break;
			}
		}
		graphicsPath.AddLines(array);
		return graphicsPath;
	}

	protected override void OnPaint(PaintEventArgs pevent)
	{
		if (rectangle_0.Width != 0 && rectangle_0.Height != 0)
		{
			pevent.Graphics.FillRectangle(solidBrush_3, rectangle_0);
			method_2(pevent);
			method_8(pevent);
			method_7(pevent);
		}
	}

	protected override void OnPaintBackground(PaintEventArgs pevent)
	{
		InvokePaintBackground(base.Parent, pevent);
		InvokePaint(base.Parent, pevent);
	}

	protected override void OnResize(EventArgs eventArgs_0)
	{
		switch (Alignment)
		{
		case TabAlignment.Top:
			rectangle_0 = new Rectangle(0, 1, base.Width - 4, base.ItemSize.Height);
			break;
		case TabAlignment.Bottom:
			rectangle_0 = new Rectangle(0, base.Height - base.ItemSize.Height, base.Width - 4, base.ItemSize.Height);
			break;
		case TabAlignment.Left:
			rectangle_0 = new Rectangle(1, 2, base.ItemSize.Height, base.Height - 4);
			break;
		}
		for (int i = 0; i < base.TabCount; i++)
		{
			base.TabPages[i].Padding = new Padding(0);
		}
		base.OnResize(eventArgs_0);
		Invalidate();
	}

	protected override void OnSelecting(TabControlCancelEventArgs tabControlCancelEventArgs_0)
	{
		base.OnSelecting(tabControlCancelEventArgs_0);
		Refresh();
	}

	private void method_2(PaintEventArgs paintEventArgs_0)
	{
		if (base.TabCount > 0)
		{
			for (int i = 0; i < base.TabCount; i++)
			{
				method_3(paintEventArgs_0, i);
			}
		}
	}

	private void method_3(PaintEventArgs paintEventArgs_0, int int_0)
	{
		GraphicsPath graphicsPath_ = method_1(int_0);
		GraphicsPath graphicsPath_2 = method_0(int_0);
		method_4(paintEventArgs_0.Graphics, int_0, graphicsPath_);
		method_5(paintEventArgs_0.Graphics, int_0, graphicsPath_, graphicsPath_2);
		method_6(paintEventArgs_0.Graphics, int_0);
	}

	private void method_4(Graphics graphics_0, int int_0, GraphicsPath graphicsPath_0)
	{
		Rectangle tabRect = GetTabRect(int_0);
		if (tabRect.Width != 0 && tabRect.Height != 0)
		{
			if (int_0 == base.SelectedIndex)
			{
				graphics_0.FillPath(solidBrush_1, graphicsPath_0);
			}
			else
			{
				graphics_0.FillPath(solidBrush_2, graphicsPath_0);
			}
		}
	}

	private void method_5(Graphics graphics_0, int int_0, GraphicsPath graphicsPath_0, GraphicsPath graphicsPath_1)
	{
		if (int_0 == base.SelectedIndex)
		{
			graphics_0.DrawPath(pen_0, graphicsPath_0);
			graphics_0.DrawPath(pen_1, graphicsPath_1);
			return;
		}
		graphics_0.DrawPath(pen_0, graphicsPath_0);
		if (tabStyle == TabStyle.Special && graphicsPath_0.PointCount >= 3)
		{
			graphics_0.DrawLine(pen_1, graphicsPath_0.PathPoints[1], graphicsPath_0.PathPoints[2]);
		}
	}

	private void method_6(Graphics graphics_0, int int_0)
	{
		Rectangle tabRect = GetTabRect(int_0);
		int num = 0;
		int num2 = 1;
		switch (Alignment)
		{
		case TabAlignment.Top:
			num2 = 2;
			break;
		case TabAlignment.Bottom:
			num2 = ((tabStyle == TabStyle.Normal) ? 1 : 2);
			break;
		case TabAlignment.Left:
		{
			num2 = 2;
			num = 3;
			stringFormat_0.Alignment = StringAlignment.Near;
			stringFormat_0.LineAlignment = StringAlignment.Center;
			int imageIndex = base.TabPages[int_0].ImageIndex;
			if (imageIndex >= 0)
			{
				Image image = base.ImageList.Images[imageIndex];
				graphics_0.DrawImage(image, tabRect.Left + num, (float)tabRect.Top + (float)(tabRect.Height - image.Height) / 2f);
				num += image.Width + 2;
			}
			break;
		}
		}
		graphics_0.DrawString(layoutRectangle: new Rectangle(tabRect.Left + num, tabRect.Top + num2, tabRect.Width, tabRect.Height), s: base.TabPages[int_0].Text, font: Font, brush: solidBrush_0, format: stringFormat_0);
	}

	private void method_7(PaintEventArgs paintEventArgs_0)
	{
		if (base.SelectedIndex >= 0)
		{
			Rectangle tabRect = GetTabRect(base.SelectedIndex);
			switch (Alignment)
			{
			case TabAlignment.Top:
				point_0 = new Point(tabRect.Left + 1, tabRect.Bottom + 1);
				point_1 = new Point(tabRect.Right - 1, tabRect.Bottom + 1);
				break;
			case TabAlignment.Bottom:
				point_0 = new Point(tabRect.Left, tabRect.Top - 1);
				point_1 = new Point(tabRect.Right, tabRect.Top - 1);
				break;
			case TabAlignment.Left:
				point_0 = new Point(tabRect.Right + 1, tabRect.Top);
				point_1 = new Point(tabRect.Right + 1, tabRect.Bottom);
				break;
			}
			paintEventArgs_0.Graphics.DrawLine(pen_2, point_0, point_1);
		}
	}

	private void method_8(PaintEventArgs paintEventArgs_0)
	{
		if (base.TabCount <= 0 || base.SelectedIndex < 0)
		{
			return;
		}
		if (tabStyle == TabStyle.Normal)
		{
			Rectangle bounds = base.TabPages[base.SelectedIndex].Bounds;
			bounds.Inflate(1, 1);
			paintEventArgs_0.Graphics.DrawRectangle(pen_0, bounds);
			return;
		}
		TabAlignment tabAlignment = Alignment;
		if (tabAlignment == TabAlignment.Bottom)
		{
			Point point = new Point(0, base.Height - base.ItemSize.Height - 3);
			Point pt = point;
			pt.X = base.Width;
			paintEventArgs_0.Graphics.DrawLine(pen_0, point, pt);
		}
	}
}
