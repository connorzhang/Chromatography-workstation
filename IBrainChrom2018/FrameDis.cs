using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Runtime.InteropServices;
using System.Text;
using IBrainChrom2018.Unit;

namespace IBrainChrom2018;

public class FrameDis
{
	private SystemParam sysParam = SystemParam.Create();

	protected RectangleF _frmRC;

	private int myX1 = -1;

	private int myX2 = -1;

	private int myY1 = -1;

	private int myY2 = -1;

	private float float_2;

	private float float_3;

	protected SolidBrush brush = new SolidBrush(Color.Aqua);

	public DisLg disLg = default(DisLg);

	public string disMouseLgFmtX = "0.000";

	public string disMouseLgFmtY = "0.000";

	protected Pen disPen = new Pen(Color.Red);

	public LclDisplayPanel displayPanel;

	public bool drawDynamicL = true;

	public RectangleF dskRC;

	protected bool evrmOK;

	public string fmtX = "";

	public string fmtY = "";

	public string fmtY_ = "";

	public static Font font;

	protected Color frmColor = Color.Black;

	protected Pen frmPen = new Pen(Color.LightGray);

	public RectangleF frmRC;

	public float fX;

	public float fY;

	public float fY_;

	protected Graphics graphics_0;

	private Color color_0 = Color.GreenYellow;

	private DashStyle dashStyle_0 = DashStyle.DashDot;

	private PointF[] pointF_0 = new PointF[4];

	private PointF[] pointF_1 = new PointF[4];

	private Pen pen_0 = new Pen(Color.Black);

	private PointF[] pointF_2 = new PointF[7];

	private PointF[] pointF_3 = new PointF[7];

	private IntPtr intptr_0 = IntPtr.Zero;

	private IntPtr intptr_1 = IntPtr.Zero;

	protected IntPtr hdc;

	protected IntPtr hdc2;

	public InstruStyle instruStyle;

	protected bool isPrint;

	private float fXScaleStep;

	private float fYScaleStep;

	private float fYScaleStep2;

	private DisLg disLg_0;

	private RectangleF rectangleF_0;

	private RectangleF rectangleF_1;

	private Point point_0;

	private Rectangle rectangle_0;

	private string string_0;

	private string string_1;

	private string string_2;

	private int int_7 = -1;

	private int int_8 = -1;

	private int int_9 = -1;

	private int int_10 = -1;

	private int int_11 = -1;

	private int int_12 = -1;

	protected LineBE[] lineBEs = new LineBE[0];

	public float manuTimeA;

	public float manuTimeB;

	public Point mouseLocation;

	public bool moving;

	public Options options;

	public bool psColors;

	private PointF pointF_4;

	private PointF pointF_5;

	private PointF pointF_6;

	private PointF pointF_7;

	private PointF pointF_8;

	private PointF pointF_9;

	public Point ptScaleBegin;

	public Point ptMouseRight;

	private Rectangle rectangle_1;

	protected RectangleF rcMouseLgValue = new RectangleF(0f, 0f, 150f, 15f);

	public RectangleF rcPage;

	public float refDisScaleX = 1f;

	public float refDisScaleY = 1f;

	public float refDisScaleY_ = 1f;

	public int refScaleXNum = 10;

	public int refScaleY_Num = 8;

	public int refScaleYNum = 8;

	protected SolidBrush solidBrush_0 = new SolidBrush(Color.Black);

	public bool scaling;

	public bool setShowGrid;

	public bool showMouseLgValue = true;

	public DisChain stDisChain = new DisChain();

	public string txtX = "";

	public string txtY = "";

	public string txtY_ = "";

	public string unitX = "";

	public string unitY = "";

	public string unitY_ = "";

	protected WinStyle winStyle;

	private float float_7;

	private float float_8;

	public bool ShowAll;

	public float[] signalfactors;

	public float[] signal下限;

	public float[] signal上限;

	public StringBuilder strDebugLog = new StringBuilder();

	public string DebugLog => strDebugLog.ToString();

	public string DebugLog2 { get; set; }

	public PointF MouseLgValue => scrToLg(mouseLocation, bool_0: true);

	protected bool Relayout_DisLg => !disLg.Equals(disLg_0);

	protected bool Relayout_DskFrm => !dskRC.Equals(rectangleF_0) || !frmRC.Equals(rectangleF_1);

	protected bool Relayout_Unit => !txtX.Equals(string_0) || !txtY.Equals(string_1) || !txtY_.Equals(string_2);

	public FrameDis(WinStyle winStyle, LclDisplayPanel displayPanel)
	{
		this.winStyle = winStyle;
		this.displayPanel = displayPanel;
		if (displayPanel == null)
		{
			isPrint = true;
		}
	}

	protected void DebugLogAppendLine(string strinfo)
	{
	}

	private void method_0(ref float float_9)
	{
		if (float_9 > 0f)
		{
			float num = 1f;
			while (float_9 >= 10f)
			{
				float_9 /= 10f;
				num *= 10f;
			}
			while (float_9 < 1f)
			{
				float_9 *= 10f;
				num /= 10f;
			}
			if ((double)float_9 < 1.5)
			{
				float_9 = 1f;
			}
			else if ((double)float_9 < 3.5)
			{
				float_9 = 2f;
			}
			else if ((double)float_9 < 7.5)
			{
				float_9 = 5f;
			}
			else
			{
				float_9 = 10f;
			}
			float_9 *= num;
		}
	}

	[DllImport("Gdi32.dll")]
	private static extern IntPtr CreatePen(int int_13, int int_14, int int_15);

	protected virtual bool disLg_Valid()
	{
		return disLg.Valid_xy;
	}

	public virtual void DrawFrameAndLabel(bool evrmOK)
	{
		DebugLogAppendLine("FrameDis.draw(bool evrmOK) Begin");
		Pen pen = frmPen;
		Color color = (brush.Color = (frmColor = Color.Black));
		pen.Color = color;
		graphics_0.DrawRectangle(frmPen, frmRC.Left, frmRC.Top, frmRC.Width, frmRC.Height);
		if (evrmOK)
		{
			drawScale();
			Draw_AllString();
			if (showMouseLgValue)
			{
				drawMouseLgValue(graphics_0);
			}
		}
		DebugLogAppendLine("FrameDis..draw(bool evrmOK) End");
	}

	public virtual void DrawFrameAndLabelPrintf(bool evrmOK)
	{
		DebugLogAppendLine("FrameDis.draw(bool evrmOK) Begin");
		Pen pen = frmPen;
		Color color = (brush.Color = (frmColor = Color.Black));
		pen.Color = color;
		graphics_0.DrawRectangle(frmPen, frmRC.Left, frmRC.Top, frmRC.Width, frmRC.Height);
		if (evrmOK)
		{
			drawScale();
			Draw_AllString();
			bool flag = true;
			drawMouseLgValue(graphics_0);
		}
		DebugLogAppendLine("FrameDis..draw(bool evrmOK) End");
	}

	public void Draw(Graphics graphics, bool erase)
	{
		strDebugLog.Clear();
		graphics_0 = graphics;
		if (displayPanel != null)
		{
			dskRC = displayPanel.ClientRectangle;
		}
		evrmOK = drawEvrmPrep();
		if (erase)
		{
			solidBrush_0.Color = Class49.GetColor(0);
			graphics_0.FillRectangle(solidBrush_0, dskRC);
			solidBrush_0.Color = Class49.GetColor(0);
			graphics_0.FillRectangle(solidBrush_0, frmRC);
		}
		DrawFrameAndLabel(evrmOK);
		graphics_0.SetClip(_frmRC);
		if (isPrint)
		{
			graphics_0.SetClip(rcPage);
		}
		else
		{
			graphics_0.SetClip(dskRC);
		}
		if (evrmOK)
		{
			method_5();
		}
		disLg_0 = disLg;
		rectangleF_0 = dskRC;
		rectangleF_1 = frmRC;
		string_0 = txtX;
		string_1 = txtY;
		string_2 = txtY_;
	}

	public void DrawBmp(Graphics graphics, bool erase)
	{
		strDebugLog.Clear();
		graphics_0 = graphics;
		if (displayPanel != null)
		{
			dskRC = displayPanel.ClientRectangle;
		}
		evrmOK = drawEvrmPrep();
		if (erase)
		{
			solidBrush_0.Color = Class49.GetColor(0);
			graphics_0.FillRectangle(solidBrush_0, dskRC);
			solidBrush_0.Color = Class49.GetColor(0);
			graphics_0.FillRectangle(solidBrush_0, frmRC);
		}
		DrawFrameAndLabelPrintf(evrmOK);
		graphics_0.SetClip(_frmRC);
		if (isPrint)
		{
			graphics_0.SetClip(rcPage);
		}
		else
		{
			graphics_0.SetClip(dskRC);
		}
		if (evrmOK)
		{
			printfWord();
		}
		disLg_0 = disLg;
		rectangleF_0 = dskRC;
		rectangleF_1 = frmRC;
		string_0 = txtX;
		string_1 = txtY;
		string_2 = txtY_;
	}

	public virtual bool drawEvrmPrep()
	{
		brush.Color = Color.Black;
		frmRC = dskRC;
		frmRC.X += 20f;
		frmRC.Y += 20f;
		frmRC.Width -= 40f;
		frmRC.Height -= 40f;
		if (frmRC.Width <= 70f || frmRC.Height <= 40f)
		{
			return false;
		}
		if (stDisChain.Count <= 0)
		{
			return false;
		}
		disLg = stDisChain.CurDisLg;
		if (!disLg_Valid())
		{
			return false;
		}
		graphics_0.SmoothingMode = SmoothingMode.AntiAlias;
		if (Relayout_DisLg)
		{
			fXScaleStep = disLg.lgX / (float)(refScaleXNum - 1);
			method_0(ref fXScaleStep);
			fYScaleStep = disLg.lgY / (float)(refScaleYNum - 1);
			method_0(ref fYScaleStep);
			fYScaleStep2 = disLg.lgY_ / (float)(refScaleY_Num - 1);
			method_0(ref fYScaleStep2);
			if (!(this is CmpdDisplay))
			{
				if (disLg.lgY < 1f)
				{
					fmtY = "0.000";
				}
				else if (disLg.lgY < 10f)
				{
					fmtY = "0.00";
				}
				else if (disLg.lgY < 100f)
				{
					fmtY = "0.0";
				}
				else
				{
					fmtY = "0";
				}
			}
			SizeF sizeF = graphics_0.MeasureString((disLg.lgYBeg + disLg.lgY).ToString(fmtY), font);
			float_2 = Convert.ToInt32(sizeF.Width + sizeF.Height + 5f);
			sizeF = graphics_0.MeasureString(disLg.lgX.ToString(fmtX), font);
			float_8 = Convert.ToInt32(sizeF.Height + sizeF.Height + 5f);
			WinStyle winStyle = this.winStyle;
			if (winStyle == WinStyle.CaliGnl || winStyle == WinStyle.CaliGpc)
			{
				float_7 = 10f;
				float_3 = Convert.ToInt32(sizeF.Height + sizeF.Height + 5f);
			}
			else
			{
				float_7 = 10f;
				if (instruStyle == InstruStyle.LC && options.lcDisAuxYStyle != LcDisAuxYStyle.None)
				{
					sizeF = graphics_0.MeasureString(disLg.lgY_.ToString(fmtY_), font);
					float_7 = Convert.ToInt32(sizeF.Width + sizeF.Height + 5f);
				}
				if (options == null)
				{
					options = new Options();
				}
				if (instruStyle == InstruStyle.GC && options.gcDisAuxYStyle != GcDisAuxYStyle.None)
				{
					sizeF = graphics_0.MeasureString(disLg.lgY_.ToString(fmtY_), font);
					float_7 = Convert.ToInt32(sizeF.Width + sizeF.Height + 5f);
				}
				float_3 = Convert.ToInt32(sizeF.Height + 5f);
			}
		}
		frmRC = dskRC;
		frmRC.X += float_2;
		frmRC.Y += float_3;
		frmRC.Width -= float_2 + float_7;
		frmRC.Height -= float_3 + float_8;
		if (ShowAll)
		{
			int num = 0;
			for (int i = 0; i < signalfactors.Length - 1; i++)
			{
				if (signalfactors[i] != -1f)
				{
					num++;
				}
			}
			frmRC.X += (float)(num * 25);
			frmRC.Width -= (float)(num * 25);
		}
		if (frmRC.Width > 70f && frmRC.Height > 40f)
		{
			_frmRC = frmRC;
			_frmRC.Inflate(-1f, -1f);
			rcMouseLgValue.X = frmRC.Right - rcMouseLgValue.Width;
			rcMouseLgValue.Y = frmRC.Top - rcMouseLgValue.Height;
			fX = frmRC.Width / disLg.lgX;
			fY = frmRC.Height / disLg.lgY;
			fY_ = frmRC.Height / disLg.lgY_;
			return true;
		}
		return false;
	}

	private void DrawDeletePeakLine(int x1, int y1, int x2, int y2, bool bool_0)
	{
		if (bool_0 && (rectangle_1.Top > y1 || y1 > rectangle_1.Bottom || rectangle_1.Top > y2 || y2 > rectangle_1.Bottom) && x1 != x2)
		{
			if (x1 > x2)
			{
				int num = x1;
				x1 = x2;
				x2 = num;
				num = y1;
				y1 = y2;
				y2 = num;
			}
			float num2 = (float)(y2 - y1) / (float)(x2 - x1);
			if (num2 == 0f)
			{
				num2 = 1E-05f;
			}
			if (y1 < rectangle_1.Top)
			{
				x1 += Convert.ToInt32((frmRC.Top - (float)y1) / num2);
				y1 = rectangle_1.Top;
			}
			else if (y1 > rectangle_1.Bottom)
			{
				x1 += Convert.ToInt32((frmRC.Bottom - (float)y1) / num2);
				y1 = rectangle_1.Bottom;
			}
			if (y2 < rectangle_1.Top)
			{
				x2 += Convert.ToInt32((frmRC.Top - (float)y2) / num2);
				y2 = rectangle_1.Top;
			}
			else if (y2 > rectangle_1.Bottom)
			{
				x2 += Convert.ToInt32((frmRC.Bottom - (float)y2) / num2);
				y2 = rectangle_1.Bottom;
			}
		}
		if (bool_0)
		{
			myX1 = x1;
			myY1 = y1;
			myX2 = x2;
			myY2 = y2;
		}
		MoveToEx(hdc, x1, y1, IntPtr.Zero);
		LineTo(hdc, x2, y2);
		DebugLogAppendLine("FrameDis.method_1 End");
	}

	public float DrawL(int int_13, int state)
	{
		DebugLogAppendLine("FrameDis.DrawL Begin");
		int_13 = Math.Max(int_13, rectangle_1.Left);
		int_13 = Math.Min(int_13, rectangle_1.Right);
		switch (state)
		{
		case 1:
			DrawDeletePeakLine(int_7, rectangle_1.Top, int_7, rectangle_1.Bottom, bool_0: false);
			int_7 = int_13;
			break;
		case 2:
			DrawDeletePeakLine(int_8, rectangle_1.Top, int_8, rectangle_1.Bottom, bool_0: false);
			int_8 = int_13;
			break;
		default:
			DrawDeletePeakLine(int_13, rectangle_1.Top, int_13, rectangle_1.Bottom, bool_0: false);
			break;
		}
		DebugLogAppendLine("FrameDis.DrawL End");
		return disLg.lgXBeg + ((float)int_13 - frmRC.Left) / fX;
	}

	public void DrawL_add()
	{
		DrawDeletePeakLine(int_7, rectangle_1.Top, int_7, rectangle_1.Bottom, bool_0: false);
	}

	public void DrawL_begin()
	{
		rectangle_1.X = Convert.ToInt32(frmRC.X);
		rectangle_1.Y = Convert.ToInt32(frmRC.Y);
		rectangle_1.Width = Convert.ToInt32(frmRC.Width);
		rectangle_1.Height = Convert.ToInt32(frmRC.Height);
	}

	public void DrawL_end()
	{
		DrawDeletePeakLine(int_7, rectangle_1.Top, int_7, rectangle_1.Bottom, bool_0: false);
		DrawDeletePeakLine(int_8, rectangle_1.Top, int_8, rectangle_1.Bottom, bool_0: false);
		if (drawDynamicL && myX1 >= 0)
		{
			DrawDeletePeakLine(myX1, myY1, myX2, myY2, bool_0: false);
		}
		int_12 = -1;
		int_10 = -1;
		int_11 = -1;
		int_9 = -1;
		int_8 = -1;
		int_7 = -1;
		myX1 = -1;
	}

	public void DrawL_end2()
	{
		drawL2(lineBEs);
		Array.Resize(ref lineBEs, 0);
	}

	public void DrawL_VtrBl(PointF pointF_10, int state)
	{
		DebugLogAppendLine("FrameDis.DrawL_VtrBl Begin");
		pointF_10 = lgToScr(pointF_10, bool_0: true);
		int num = Convert.ToInt32(pointF_10.X);
		int num2 = Convert.ToInt32(pointF_10.Y);
		if (state == 1)
		{
			int_9 = num;
			int_11 = num2;
			myX1 = -1;
			return;
		}
		if (drawDynamicL && state == 2)
		{
			int_10 = num;
			int_12 = num2;
			DrawDeletePeakLine(int_9, int_11, int_10, int_12, bool_0: true);
		}
		DebugLogAppendLine("FrameDis.DrawL_VtrBl End");
	}

	protected void drawL2(LineBE[] lineBEs)
	{
		DebugLogAppendLine("FrameDis.drawL2 Begin");
		for (int i = 0; i < lineBEs.Length; i++)
		{
			int val = Math.Min(lineBEs[i].begin, lineBEs[i].int_0);
			int val2 = Math.Max(lineBEs[i].begin, lineBEs[i].int_0);
			val = Math.Max(val, rectangle_1.Left);
			val2 = Math.Min(val2, rectangle_1.Right);
			for (int j = 3; j < 6; j++)
			{
				MoveToEx(hdc2, val, rectangle_1.Bottom + j, IntPtr.Zero);
				LineTo(hdc2, val2, rectangle_1.Bottom + j);
			}
		}
		DebugLogAppendLine("FrameDis.drawL2 End");
	}

	public void DrawL2(LineBE[] lineBEs)
	{
		drawL2(this.lineBEs);
		drawL2(lineBEs);
		this.lineBEs = (LineBE[])lineBEs.Clone();
	}

	private void method_2()
	{
		DebugLogAppendLine("FrameDis.method_2 Begin");
		if (point_0.X != 0 || point_0.Y != 0)
		{
			MoveToEx(hdc, ptScaleBegin.X, ptScaleBegin.Y, IntPtr.Zero);
			LineTo(hdc, point_0.X, point_0.Y);
		}
		DebugLogAppendLine("FrameDis.method_2 End");
	}

	public void DrawLbLine_end()
	{
		method_2();
		point_0.Y = 0;
		point_0.X = 0;
	}

	public void DrawLbLine_moving()
	{
		DebugLogAppendLine("FrameDis.DrawLbLine_moving Begin");
		method_2();
		point_0 = mouseLocation;
		method_2();
		DebugLogAppendLine("FrameDis.DrawLbLine_moving End");
	}

	public void DrawLine(PointF pointF_10, PointF pointF_11)
	{
		DebugLogAppendLine("FrameDis.DrawLine Begin");
		graphics_0.DrawLine(disPen, pointF_10, pointF_11);
		DebugLogAppendLine("FrameDis.DrawLine End");
	}

	public void DrawLines(PointF[] pointF_10)
	{
		DebugLogAppendLine("FrameDis.DrawLines(PointF[] pointF_10) Begin");
		if (Class49.bool_3)
		{
			Pen pen = new Pen(Class49.color_0[1]);
			pen.DashStyle = disPen.DashStyle;
			graphics_0.DrawLines(pen, pointF_10);
			pen.Dispose();
		}
		else
		{
			graphics_0.DrawLines(disPen, pointF_10);
			DebugLogAppendLine("FrameDis.DrawLines(PointF[] pointF_10) End");
		}
	}

	protected void drawMouseLgValue(Graphics graphics_1)
	{
	}

	public virtual void DrawMouseLgValue()
	{
		DebugLogAppendLine("FrameDis.DrawMouseLgValue Begin");
		if (evrmOK)
		{
			Graphics graphics = Graphics.FromHdc(hdc);
			solidBrush_0.Color = options.BackClrBorder;
			graphics.FillRectangle(solidBrush_0, rcMouseLgValue);
			drawMouseLgValue(graphics);
			graphics.Dispose();
		}
		DebugLogAppendLine("FrameDis.DrawMouseLgValue End");
	}

	private void drawScale()
	{
		DebugLogAppendLine("FrameDis.method_3 Begin");
		if (disLg.lgX >= 0f && disLg.lgY >= 0f && refScaleXNum > 1 && refScaleYNum > 1 && refScaleY_Num > 1)
		{
			if (fXScaleStep > 0f)
			{
				float num;
				for (num = 0f; num < disLg.lgXBeg; num += fXScaleStep)
				{
				}
				while (num > disLg.lgXBeg + disLg.lgX)
				{
					num -= fXScaleStep;
				}
				float num2 = num;
				while (drawScaleX(num2))
				{
					num2 -= fXScaleStep;
				}
				for (num2 = num + fXScaleStep; drawScaleX(num2); num2 += fXScaleStep)
				{
				}
			}
			if (fYScaleStep > 0f)
			{
				if (fYScaleStep < 0.001f)
				{
					fYScaleStep = 0.001f;
				}
				float num3;
				for (num3 = 0f; num3 < disLg.lgYBeg; num3 += fYScaleStep)
				{
				}
				while (num3 > disLg.lgYBeg + disLg.lgY)
				{
					num3 -= fYScaleStep;
				}
				float num4 = num3;
				while (drawScaleY(num4))
				{
					num4 -= fYScaleStep;
				}
				for (num4 = num3 + fYScaleStep; drawScaleY(num4); num4 += fYScaleStep)
				{
				}
			}
			if (fYScaleStep2 >= 0f)
			{
				if (fYScaleStep2 < 0.001f)
				{
					fYScaleStep2 = 0.001f;
				}
				float num5;
				for (num5 = 0f; num5 < disLg.lgY_Beg; num5 += fYScaleStep2)
				{
				}
				while (num5 > disLg.lgY_Beg + disLg.lgY_)
				{
					num5 -= fYScaleStep2;
				}
				if (Class49.bool_2)
				{
					float num6 = num5;
					while (drawScaleTempUpgrateLine(num6))
					{
						num6 -= fYScaleStep2;
					}
					for (num6 = num5 + fYScaleStep2; drawScaleTempUpgrateLine(num6); num6 += fYScaleStep2)
					{
					}
				}
			}
			DebugLogAppendLine("FrameDis.method_3 End");
			return;
		}
		throw new Exception("显示范围错误！");
	}

	public void DrawScale_end()
	{
		DebugLogAppendLine("FrameDis.DrawScale_end Begin");
		method_4();
		rectangle_0.Height = 0;
		rectangle_0.Width = 0;
		rectangle_0.Y = 0;
		rectangle_0.X = 0;
		DebugLogAppendLine("FrameDis.DrawScale_end End");
	}

	public void DrawScale_moving()
	{
		DebugLogAppendLine("FrameDis.DrawScale_moving Begin");
		method_4();
		rectangle_0.X = Math.Min(ptScaleBegin.X, mouseLocation.X);
		rectangle_0.Y = Math.Min(ptScaleBegin.Y, mouseLocation.Y);
		rectangle_0.Width = Math.Abs(mouseLocation.X - ptScaleBegin.X);
		rectangle_0.Height = Math.Abs(mouseLocation.Y - ptScaleBegin.Y);
		method_4();
		DebugLogAppendLine("FrameDis.DrawScale_moving End");
	}

	private void method_4()
	{
		DebugLogAppendLine("FrameDis.method_4 Begin");
		MoveToEx(hdc, rectangle_0.X, rectangle_0.Y, IntPtr.Zero);
		LineTo(hdc, rectangle_0.X + rectangle_0.Width, rectangle_0.Y);
		LineTo(hdc, rectangle_0.X + rectangle_0.Width, rectangle_0.Y + rectangle_0.Height);
		LineTo(hdc, rectangle_0.X, rectangle_0.Y + rectangle_0.Height);
		LineTo(hdc, rectangle_0.X, rectangle_0.Y);
		DebugLogAppendLine("FrameDis.method_4 End");
	}

	private void method_5()
	{
		if (scaling)
		{
			frmPen.DashStyle = DashStyle.Dot;
			graphics_0.DrawRectangle(frmPen, Math.Min(ptScaleBegin.X, mouseLocation.X), Math.Min(ptScaleBegin.Y, mouseLocation.Y), Math.Abs(mouseLocation.X - ptScaleBegin.X), Math.Abs(mouseLocation.Y - ptScaleBegin.Y));
		}
	}

	private void printfWord()
	{
		if (scaling)
		{
			frmPen.DashStyle = DashStyle.Dot;
			graphics_0.DrawRectangle(frmPen, Math.Min(ptScaleBegin.X, mouseLocation.X), Math.Min(ptScaleBegin.Y, mouseLocation.Y), Math.Abs(mouseLocation.X - ptScaleBegin.X), Math.Abs(mouseLocation.Y - ptScaleBegin.Y));
		}
	}

	public virtual bool drawScaleX(float value)
	{
		DebugLogAppendLine("FrameDis.drawScaleX Begin");
		if (value >= disLg.lgXBeg && disLg.lgXBeg + disLg.lgX >= value)
		{
			PointF lgValue = new PointF(value, 0f);
			PointF pot = lgToScr(lgValue, bool_0: true);
			pot.Y = frmRC.Bottom;
			PointF pot2 = pot;
			pot2.Y += 5f;
			CheckPointF(ref pot);
			CheckPointF(ref pot2);
			graphics_0.DrawLine(frmPen, pot, pot2);
			PointF point = pot2;
			point.Y += 1f;
			string text = (value * refDisScaleX).ToString(fmtX);
			SizeF sizeF = graphics_0.MeasureString(text, font);
			point.X -= sizeF.Width / 2f;
			graphics_0.DrawString(text, font, brush, point);
			if (Class49.bool_1)
			{
				pen_0.Color = Class49.GetColor(3);
				pen_0.DashStyle = dashStyle_0;
				pot2.Y = frmRC.Bottom;
				if (pot2.X != frmRC.Left && pot2.X != frmRC.Right)
				{
					graphics_0.DrawLine(pen_0, pot2, new PointF(pot2.X, frmRC.Top));
				}
			}
			DebugLogAppendLine("FrameDis.drawScaleX End");
			return true;
		}
		DebugLogAppendLine("FrameDis.drawScaleX End");
		return false;
	}

	private void CheckPointF(ref PointF pot)
	{
		if (float.IsInfinity(pot.X) || float.IsNaN(pot.X))
		{
			pot.X = 0f;
			LogMgr.Instance.Write2RunLog("FrameDis:CheckPointF发现异常值，pot.X =" + pot.X);
		}
		if (float.IsInfinity(pot.Y) || float.IsNaN(pot.Y))
		{
			pot.Y = 0f;
			LogMgr.Instance.Write2RunLog("FrameDis:CheckPointF发现异常值，pot.Y =" + pot.Y);
		}
	}

	public bool drawScaleY(float float_9)
	{
		DebugLogAppendLine("FrameDis.method_6 Begin");
		if (float_9 >= disLg.lgYBeg && disLg.lgYBeg + disLg.lgY >= float_9)
		{
			PointF lgValue = new PointF(0f, float_9);
			PointF pointF = lgToScr(lgValue, bool_0: true);
			pointF.X = frmRC.Left;
			PointF pointF2 = pointF;
			pointF2.X -= 5f;
			graphics_0.DrawLine(frmPen, pointF, pointF2);
			PointF point = pointF2;
			string text = (float_9 * refDisScaleY).ToString(fmtY);
			(float_9 * refDisScaleY).ToString(fmtY);
			(float_9 * refDisScaleY).ToString(fmtY);
			(float_9 * refDisScaleY).ToString(fmtY);
			SizeF sizeF = graphics_0.MeasureString(text, font);
			if (unitY == "" || (unitY != "" && point.Y > frmRC.Top + sizeF.Height * 0.7f))
			{
				if (ShowAll)
				{
					int num = 0;
					float num2 = point.X - sizeF.Width - 1f;
					for (int i = 0; i < signalfactors.Length - 1; i++)
					{
						if (signalfactors[i] != -1f)
						{
							num++;
						}
					}
					for (int j = 0; j < signalfactors.Length - 1; j++)
					{
						if (signalfactors[j] != -1f)
						{
							if (signalfactors[j] == 1f)
							{
								point.X = num2 - (float)(num * 30);
								graphics_0.DrawString(text, font, brush, point);
								num--;
							}
							else
							{
								point.X = num2 - (float)(num * 30);
								text = ((signal上限[j] - signal下限[j]) / (signal上限[0] - signal下限[0]) * (float_9 - signal下限[0]) + signal下限[j]).ToString(fmtY);
								graphics_0.DrawString(text, font, brush, point);
								num--;
							}
						}
					}
				}
				else
				{
					point.X -= sizeF.Width - 1f;
					point.Y -= sizeF.Height / 2f;
					graphics_0.DrawString(text, font, brush, point);
				}
			}
			if (Class49.bool_1)
			{
				pen_0.Color = Class49.GetColor(3);
				pen_0.DashStyle = dashStyle_0;
				pointF2.X = frmRC.Left;
				if ((double)Math.Abs(pointF2.Y - frmRC.Top) >= 0.01 && pointF2.Y != frmRC.Bottom)
				{
					graphics_0.DrawLine(pen_0, pointF2, new PointF(pointF2.X + frmRC.Width, pointF2.Y));
				}
			}
			DebugLogAppendLine("FrameDis.method_6 End");
			return true;
		}
		DebugLogAppendLine("FrameDis.method_6 End");
		return false;
	}

	public bool drawScaleTempUpgrateLine(float float_9)
	{
		if (float_9 < disLg.lgY_Beg || disLg.lgY_Beg + disLg.lgY_ < float_9)
		{
			return false;
		}
		if (float_9 == 0f && disLg.lgY_Beg == 0f && disLg.lgY_ == 0f)
		{
			return false;
		}
		DebugLogAppendLine("FrameDis.method_7 Begin");
		PointF lgValue = new PointF(0f, float_9);
		PointF pointF = lgToScr(lgValue, bool_0: false);
		pointF.X = frmRC.Right;
		PointF pointF2 = pointF;
		pointF2.X += 5f;
		graphics_0.DrawLine(frmPen, pointF, pointF2);
		PointF point = pointF2;
		string text = (float_9 * refDisScaleY_).ToString(fmtY_);
		SizeF sizeF = graphics_0.MeasureString(text, font);
		if (unitY_ == "" || (unitY_ != "" && point.Y > frmRC.Top + sizeF.Height * 0.7f))
		{
			point.X += 1f;
			point.Y -= sizeF.Height / 2f;
			graphics_0.DrawString(text, font, brush, point);
		}
		DebugLogAppendLine("FrameDis.method_7 End");
		return true;
	}

	private void method_8()
	{
	}

	public void Draw_AllString()
	{
		DebugLogAppendLine("FrameDis.Draw_AllString Begin");
		bool flag = Relayout_DskFrm || Relayout_Unit;
		if (pointF_4.X < 1f || pointF_5.Y < 1f)
		{
			flag = true;
		}
		if (txtX != "")
		{
			if (flag)
			{
				SizeF sizeF = graphics_0.MeasureString(txtX, font);
				pointF_4 = new PointF(frmRC.Left + frmRC.Width / 2f - sizeF.Width / 2f, dskRC.Bottom - sizeF.Height - 1f);
			}
			graphics_0.DrawString(txtX, font, brush, pointF_4);
		}
		if (unitX != "")
		{
			if (flag)
			{
				string text = "[" + unitX + "]";
				SizeF sizeF2 = graphics_0.MeasureString(text, font);
				float val = frmRC.Right - sizeF2.Width / 2f;
				val = Math.Min(val, dskRC.Right - sizeF2.Width);
				pointF_7 = new PointF(val, dskRC.Bottom - sizeF2.Height - 1f);
			}
			graphics_0.DrawString("[" + unitX + "]", font, brush, pointF_7);
		}
		if (txtY != "")
		{
			if (flag)
			{
				SizeF sizeF3 = graphics_0.MeasureString(txtY, font);
				pointF_5.X = dskRC.Left + 1f;
				pointF_5.Y = frmRC.Top + frmRC.Height / 2f + sizeF3.Width / 2f;
			}
			graphics_0.TranslateTransform(pointF_5.X, pointF_5.Y);
			graphics_0.RotateTransform(-90f);
			PointF point = default(PointF);
			if (unitY.Trim() == "")
			{
				graphics_0.DrawString(txtY + "[mV]", font, brush, point);
			}
			else
			{
				graphics_0.DrawString(txtY + "[" + unitY + "]", font, brush, point);
			}
			graphics_0.ResetTransform();
		}
		if (Class49.bool_2)
		{
			if (unitY != "")
			{
				if (flag)
				{
					string text2 = "[" + unitY + "]";
					SizeF sizeF4 = graphics_0.MeasureString(text2, font);
					float val2 = frmRC.Left - 5f - sizeF4.Width - 1f;
					pointF_8.X = Math.Max(val2, 1f);
					float val3 = frmRC.Top - sizeF4.Height * (2f / 3f);
					pointF_8.Y = Math.Max(val3, 1f);
				}
				graphics_0.DrawString("[" + unitY + "]", font, brush, pointF_8);
			}
			if (txtY_ != "")
			{
				if (flag)
				{
					SizeF sizeF5 = graphics_0.MeasureString(txtY_, font);
					pointF_6.X = dskRC.Right - sizeF5.Height - 1f;
					pointF_6.Y = frmRC.Top + frmRC.Height / 2f + sizeF5.Width / 2f;
				}
				graphics_0.TranslateTransform(pointF_6.X, pointF_6.Y);
				graphics_0.RotateTransform(-90f);
				graphics_0.DrawString(txtY_ + "°C", font, brush, default(PointF));
				graphics_0.ResetTransform();
			}
			if (unitY_ != "")
			{
				if (flag)
				{
					SizeF sizeF6 = graphics_0.MeasureString(unitY_, font);
					float val4 = frmRC.Right + 5f + 1f;
					pointF_9.X = Math.Min(val4, dskRC.Right - sizeF6.Width - 1f);
					float val5 = frmRC.Top - sizeF6.Height * (2f / 3f);
					pointF_9.Y = Math.Max(val5, 1f);
				}
				graphics_0.DrawString("[" + unitY_ + "]", font, brush, pointF_9);
			}
		}
		DebugLogAppendLine("FrameDis.Draw_AllString End");
	}

	public void ExtDraw_begin()
	{
		DebugLogAppendLine("FrameDis.ExtDraw_begin Begin");
		if (intptr_0 != IntPtr.Zero)
		{
			ReleaseDC(intptr_0, hdc);
		}
		intptr_0 = displayPanel.Handle;
		hdc = GetDC(intptr_0);
		IntPtr hPen = CreatePen(2, 1, ColorTranslator.ToWin32(Color.Black));
		SetROP2(hdc, 10);
		SelectObject(hdc, hPen);
		DebugLogAppendLine("FrameDis.ExtDraw_begin End");
	}

	public void ExtDraw_begin2()
	{
		DebugLogAppendLine("FrameDis.ExtDraw_begin2 Begin");
		if (intptr_1 != IntPtr.Zero)
		{
			ReleaseDC(intptr_1, hdc2);
		}
		intptr_1 = displayPanel.Handle;
		hdc2 = GetDC(intptr_1);
		IntPtr hPen = CreatePen(0, 1, ColorTranslator.ToWin32(frmPen.Color));
		SetROP2(hdc2, 10);
		SelectObject(hdc2, hPen);
		DebugLogAppendLine("FrameDis.ExtDraw_begin2 End");
	}

	public void FillPolygon(PointF[] points, Color foreColor)
	{
		HatchBrush hatchBrush = new HatchBrush(HatchStyle.OutlinedDiamond, foreColor, options.BackClrChart);
		graphics_0.FillPolygon(hatchBrush, points);
	}

	[DllImport("USER32.DLL")]
	public static extern IntPtr GetDC(IntPtr hwnd);

	public PointF lgToScr(PointF lgValue, bool bool_0)
	{
		PointF result = default(PointF);
		float num = 0f;
		float x = frmRC.Left + fX * (lgValue.X - disLg.lgXBeg);
		result.X = x;
		if (bool_0)
		{
			num = frmRC.Bottom - fY * (lgValue.Y - disLg.lgYBeg);
			result.Y = (float.IsInfinity(num) ? 0f : num);
			return result;
		}
		num = frmRC.Bottom - fY_ * (lgValue.Y - disLg.lgY_Beg);
		result.Y = (float.IsInfinity(num) ? 0f : num);
		return result;
	}

	[DllImport("GDI32.DLL")]
	public static extern IntPtr LineTo(IntPtr hdc, int x, int y);

	public virtual void LinkOptions(Options options)
	{
		this.options = options;
	}

	[DllImport("GDI32.DLL")]
	public static extern IntPtr MoveToEx(IntPtr hdc, int x, int y, IntPtr lpPoint);

	[DllImport("USER32.DLL")]
	public static extern IntPtr ReleaseDC(IntPtr hwnd, IntPtr hdc);

	public PointF scrToLg(Point ptScr, bool bool_0)
	{
		PointF result = new PointF
		{
			X = disLg.lgXBeg + ((float)ptScr.X - frmRC.Left) / fX
		};
		if (bool_0)
		{
			result.Y = disLg.lgYBeg + (frmRC.Bottom - (float)ptScr.Y) / fY;
			return result;
		}
		result.Y = disLg.lgY_Beg + (frmRC.Bottom - (float)ptScr.Y) / fY_;
		return result;
	}

	public SizeF scrToLg(Size szScr, bool bool_0)
	{
		SizeF result = new SizeF
		{
			Width = (float)szScr.Width / fX
		};
		if (bool_0)
		{
			result.Height = (float)szScr.Height / fY;
			return result;
		}
		result.Height = (float)szScr.Height / fY_;
		return result;
	}

	[DllImport("Gdi32.dll")]
	private static extern IntPtr SelectObject(IntPtr hdc, IntPtr hPen);

	[DllImport("GDI32.DLL")]
	public static extern IntPtr SetROP2(IntPtr hdc, int fnDrawMode);
}
