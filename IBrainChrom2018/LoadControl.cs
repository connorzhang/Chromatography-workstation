using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace IBrainChrom2018;

public class LoadControl : UserControl
{
	private Color beginColor = Color.Blue;

	private Color endColor = Color.Red;

	private int wid = 10;

	private int curindex = 0;

	private Timer timer;

	private int instervel = 200;

	private string loadStr = "loading....";

	private IContainer components = null;

	public LoadControl()
	{
		InitializeComponent();
		SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, value: true);
		MinimumSize = new Size(40, 80);
		if (!base.DesignMode)
		{
			Start();
		}
	}

	public void Start()
	{
		if (timer == null)
		{
			timer = new Timer();
			timer.Interval = instervel;
			timer.Tick += Timer_Tick;
		}
		timer.Enabled = true;
	}

	public void Stop()
	{
		if (timer != null)
		{
			timer.Enabled = false;
		}
	}

	private void Timer_Tick(object sender, EventArgs e)
	{
		curindex++;
		curindex = ((curindex < wid) ? curindex : 0);
		Refresh();
	}

	private Point getPoint(double d, double r, Point center)
	{
		int num = (int)(r * Math.Cos(d * Math.PI / 180.0));
		int num2 = (int)(r * Math.Sin(d * Math.PI / 180.0));
		return new Point(center.X + num, center.Y - num2);
	}

	private GraphicsPath getPath(Point a, Point b)
	{
		int num = 2;
		Vertical(a, b, num, out var pointc, out var pointd);
		Vertical(b, a, num, out var pointc2, out var pointd2);
		GraphicsPath graphicsPath = new GraphicsPath();
		graphicsPath.AddPolygon(new Point[4] { pointc, pointd, pointc2, pointd2 });
		graphicsPath.CloseAllFigures();
		return graphicsPath;
	}

	private bool Vertical(Point pointa, Point pointb, double R, out Point pointc, out Point pointd)
	{
		pointc = default(Point);
		pointd = default(Point);
		try
		{
			double num = (double)pointa.X - (double)(pointb.Y - pointa.Y) * R / Distance(pointa, pointb);
			double num2 = (double)pointa.Y + (double)(pointb.X - pointa.X) * R / Distance(pointa, pointb);
			double num3 = (double)pointa.X + (double)(pointb.Y - pointa.Y) * R / Distance(pointa, pointb);
			double num4 = (double)pointa.Y - (double)(pointb.X - pointa.X) * R / Distance(pointa, pointb);
			pointc = new Point((int)num, (int)num2);
			pointd = new Point((int)num3, (int)num4);
			return true;
		}
		catch
		{
			return false;
		}
	}

	private double Distance(double xa, double ya, double xb, double yb)
	{
		return Math.Sqrt(Math.Pow(xa - xb, 2.0) + Math.Pow(ya - yb, 2.0));
	}

	private double Distance(Point pa, Point pb)
	{
		return Distance(pa.X, pa.Y, pb.X, pb.Y);
	}

	private GraphicsPath getPath(double d, double r, Point c)
	{
		Point point = getPoint(d, r / 2.0, c);
		Point point2 = getPoint(d, r, c);
		return getPath(point, point2);
	}

	private Color[] getColors()
	{
		int num = (int)((double)(endColor.R - beginColor.R) / (double)wid);
		int num2 = (int)((double)(endColor.G - beginColor.G) / (double)wid);
		int num3 = (int)((double)(endColor.B - beginColor.B) / (double)wid);
		List<Color> list = new List<Color>();
		for (int i = 0; i < wid; i++)
		{
			list.Add(Color.FromArgb(beginColor.R + num * i, beginColor.G + num2 * i, beginColor.B + num3 * i));
		}
		return list.ToArray();
	}

	private void drawRect(Graphics g)
	{
		int num = (int)((double)base.Size.Height / 2.0);
		Point c = new Point(num, num);
		Color[] colors = getColors();
		int num2 = curindex;
		for (int i = 0; i < wid; i++)
		{
			double d = 360.0 / (double)wid * (double)i;
			GraphicsPath path = getPath(d, num, c);
			int num3 = num2 + i;
			num3 = ((num3 >= wid) ? (num3 - wid) : num3);
			g.FillPath(new SolidBrush(colors[num3]), path);
		}
	}

	private void drawString(Graphics g)
	{
		if (base.Size.Height < base.Size.Width)
		{
			Rectangle rectangle = new Rectangle(new Point(base.Size.Height, 0), new Size(base.Size.Width - base.Size.Height, base.Size.Height));
			StringFormat stringFormat = new StringFormat();
			stringFormat.Alignment = StringAlignment.Center;
			stringFormat.LineAlignment = StringAlignment.Center;
			g.DrawString(loadStr, Font, Brushes.Black, rectangle, stringFormat);
		}
	}

	protected override void OnPaint(PaintEventArgs pe)
	{
		base.OnPaint(pe);
		Graphics graphics = pe.Graphics;
		graphics.SmoothingMode = SmoothingMode.HighQuality;
		graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
		drawRect(graphics);
		drawString(graphics);
	}

	protected override void OnSizeChanged(EventArgs e)
	{
		base.OnSizeChanged(e);
		if (base.Size.Height > base.Size.Width)
		{
			base.Size = new Size(base.Size.Height, base.Size.Height);
		}
	}

	protected override void Dispose(bool disposing)
	{
		if (disposing && components != null)
		{
			components.Dispose();
		}
		base.Dispose(disposing);
	}

	private void InitializeComponent()
	{
		this.components = new System.ComponentModel.Container();
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
	}
}
