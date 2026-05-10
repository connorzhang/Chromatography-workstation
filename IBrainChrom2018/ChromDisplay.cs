using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace IBrainChrom2018;

public class ChromDisplay : SampleDisplay
{
	public string fmtPeakRT;

	public Pen lbLinePen;

	public Pen lbSelPen;

	private RectangleF rectangleF_3;

	public Pen FrmPen
	{
		get
		{
			return frmPen;
		}
		set
		{
			frmPen = value;
		}
	}

	public Pen DisPen
	{
		get
		{
			return disPen;
		}
		set
		{
			disPen = value;
		}
	}

	public ChromDisplay(WinStyle winStyle, LclDisplayPanel displayPanel)
		: base(winStyle, displayPanel)
	{
		fmtPeakRT = "0.000";
		rectangleF_3 = new RectangleF(0f, 0f, 150f, 15f);
		lbSelPen = new Pen(Color.Black);
		lbSelPen.DashStyle = DashStyle.Dot;
		lbLinePen = new Pen(Color.Black);
	}

	public override void DrawSingle(bool evrmOK)
	{
		if (evrmOK && disSignals != null && disSignals.Length != 0)
		{
			for (int i = 0; i < disSignals.Length; i++)
			{
				signal_proc(disSignals[i]);
			}
			DrawSelectPeak();
			for (int j = 0; j < disSignals.Length; j++)
			{
				signal_show(disSignals[j], Color.Transparent, j);
			}
			DrawSignalAssist();
			drawL2(lineBEs);
		}
	}

	public PointF ClickLgV()
	{
		return scrToLg(ptScaleBegin, bool_0: true);
	}

	private void DrawSignalAssist()
	{
		if (curSignal == null)
		{
			return;
		}
		for (int i = 0; i < curSignal.peaks.Length; i++)
		{
			Peak peak = curSignal.peaks[i];
			if (peak.disNo < 0 || !(peak.area > 0f) || peak.pkN >= curSignal.dots.Length)
			{
				continue;
			}
			if (disLg.lgXBeg < curSignal.dots[peak.pkN].X && curSignal.dots[peak.pkN].X < disLg.lgXBeg + disLg.lgX)
			{
				disPen.DashStyle = DashStyle.Solid;
				disPen.Color = curSignal.disColor;
				PointF pointF = lgToScr(curSignal.dots[peak.pkN], bool_0: true);
				PointF pt = pointF;
				if (peak.positive)
				{
					pt.Y -= 5f;
				}
				else
				{
					pt.Y += 5f;
				}
				graphics_0.DrawLine(disPen, pointF, pt);
				float y = pt.Y;
				brush.Color = Color.Black;
				if (options.peakFtClrAsActiveSignal)
				{
					brush.Color = curSignal.disColor;
				}
				graphics_0.TranslateTransform(pt.X, pt.Y);
				graphics_0.RotateTransform(-90f);
				for (int j = 0; j <= 1; j++)
				{
					if (j != 1)
					{
						float x = (pt.Y = 0f);
						pt.X = x;
						continue;
					}
					float x2 = pt.X;
					pt.X = (peak.positive ? 0f : (0f - x2));
					pt.Y = 0f;
					float num2 = (peak.positive ? (y - x2) : y);
					float num3 = (peak.positive ? y : (y + x2));
					if (num2 < frmRC.Top)
					{
						pt.X -= frmRC.Top - num2;
					}
					if (num3 > frmRC.Bottom)
					{
						pt.X += num3 - frmRC.Bottom;
					}
				}
				graphics_0.ResetTransform();
			}
			disPen.Color = options.baselineColor;
			if (options.baselineColorAsActive)
			{
				disPen.Color = curSignal.disColor;
			}
			bool flag = false;
			if (peak.RtDotNo < curSignal.dots.Length)
			{
				flag = curSignal.dots[peak.RtDotNo].X <= disLg.lgXBeg || disLg.lgXBeg + disLg.lgX <= curSignal.dots[peak.LfDotNo].X;
			}
			if (!flag && options.baselineMarks)
			{
				DrawPeakAndValeMark(peak.FromNo, bool_3: true);
				DrawPeakAndValeMark(peak.ToNo, bool_3: false);
			}
			if (!flag && peak.selected)
			{
				DrawSelectPeakMark(peak.bsLfV.dotNo);
				DrawSelectPeakMark(peak.bsRtV.dotNo);
			}
			if (peak.RtDotNo < curSignal.dots.Length && curSignal.dots[peak.RtDotNo].X > disLg.lgXBeg && disLg.lgXBeg + disLg.lgX > curSignal.dots[peak.LfDotNo].X && options.baselineVisible)
			{
				disPen.DashStyle = DashStyle.Dot;
				peak.DrawBaseLine(curSignal.dots, this);
			}
		}
		if (base.Relayout_DisLg || base.Relayout_DskFrm || curSignal.ReCalcuDis())
		{
			for (int k = 0; k < curSignal.lbTexts.Length; k++)
			{
				LbText lbText = curSignal.lbTexts[k];
				lbText.disPt = lgToScr(lbText.pointF_0, bool_0: true);
				lbText.szText = graphics_0.MeasureString(lbText.text, lbText.font);
				float num4 = lbText.szText.Width / 2f;
				float num5 = lbText.szText.Height / 2f;
				lbText.pointF_1[0].X = lbText.disPt.X - num4;
				lbText.pointF_1[0].Y = lbText.disPt.Y - num5;
				lbText.pointF_1[1].X = lbText.disPt.X + num4;
				lbText.pointF_1[1].Y = lbText.pointF_1[0].Y;
				lbText.pointF_1[2].X = lbText.pointF_1[1].X;
				lbText.pointF_1[2].Y = lbText.disPt.Y + num5;
				lbText.pointF_1[3].X = lbText.pointF_1[0].X;
				lbText.pointF_1[3].Y = lbText.pointF_1[2].Y;
				lbText.pointF_1[4] = lbText.pointF_1[0];
			}
			for (int l = 0; l < curSignal.lbLines.Length; l++)
			{
				curSignal.lbLines[l].disPt = lgToScr(curSignal.lbLines[l].pointF_0, bool_0: true);
				curSignal.lbLines[l].disPt2 = lgToScr(curSignal.lbLines[l].pointF_2, bool_0: true);
			}
		}
		for (int m = 0; m < curSignal.lbTexts.Length; m++)
		{
			LbText lbText2 = curSignal.lbTexts[m];
			if (!float.IsNaN(lbText2.disPt.X))
			{
				graphics_0.TranslateTransform(lbText2.disPt.X, lbText2.disPt.Y);
				graphics_0.RotateTransform(0f - (float)lbText2.int_0);
				float x3 = (0f - lbText2.szText.Width) / 2f;
				float y2 = (0f - lbText2.szText.Height) / 2f;
				graphics_0.DrawString(lbText2.text, lbText2.font, new SolidBrush(isPrint ? RptSetupDlg._clr(psColors, lbText2.color_0) : lbText2.color_0), x3, y2);
				graphics_0.ResetTransform();
				if (lbText2.selected)
				{
					graphics_0.DrawLines(lbSelPen, lbText2.pointF_1);
				}
			}
		}
		for (int n = 0; n < curSignal.lbLines.Length; n++)
		{
			LbLine lbLine = curSignal.lbLines[n];
			if (!float.IsNaN(lbLine.disPt.X))
			{
				lbLinePen.Width = lbLine.int_0;
				lbLinePen.Color = (isPrint ? RptSetupDlg._clr(psColors, lbLine.color_0) : lbLine.color_0);
				lbLinePen.EndCap = LineCap.ArrowAnchor;
				lbLinePen.DashStyle = lbLine.style;
				graphics_0.DrawLine(lbLinePen, lbLine.disPt, lbLine.disPt2);
				if (lbLine.selected)
				{
					graphics_0.FillRectangle(Brushes.Black, lbLine.disPt.X - (float)lbLine.int_0, lbLine.disPt.Y - (float)lbLine.int_0, lbLine.int_0 + lbLine.int_0, lbLine.int_0 + lbLine.int_0);
					graphics_0.FillRectangle(Brushes.Black, lbLine.disPt2.X - (float)lbLine.int_0, lbLine.disPt2.Y - (float)lbLine.int_0, lbLine.int_0 + lbLine.int_0, lbLine.int_0 + lbLine.int_0);
				}
			}
		}
	}

	private void DrawSelectPeak()
	{
		if (curSignal == null || !options.peakAreaClrByTags)
		{
			return;
		}
		brush.Color = curSignal.disColor;
		for (int i = 0; i < curSignal.peaks.Length; i++)
		{
			Peak peak = curSignal.peaks[i];
			if (peak.disNo >= 0)
			{
				bool flag = false;
				if (peak.area > 0f && peak.RtDotNo < curSignal.dots.Length)
				{
					flag = curSignal.dots[peak.RtDotNo].X <= disLg.lgXBeg || disLg.lgXBeg + disLg.lgX <= curSignal.dots[peak.LfDotNo].X;
				}
				if (options.peakAreaClrByTags && !flag && peak.selected)
				{
					peak.DrawArea(curSignal.dots, this, curSignal.disColor);
				}
			}
		}
	}

	public override bool drawEvrmPrep()
	{
		bool result = base.drawEvrmPrep();
		rectangleF_3.X = frmRC.Left;
		return result;
	}

	private void DrawSelectPeakMark(int int_14)
	{
		PointF pointF = lgToScr(curSignal.dots[int_14], bool_0: true);
		float num = 5f;
		pointF.Y -= num;
		PointF pointF2 = pointF;
		pointF2.Y += num + num;
		Color color = disPen.Color;
		disPen.Color = ((color == Color.Red) ? Color.Black : Color.Red);
		graphics_0.DrawLine(disPen, pointF.X - num, pointF.Y, pointF2.X + num, pointF2.Y);
		graphics_0.DrawLine(disPen, pointF.X + num, pointF.Y, pointF2.X - num, pointF2.Y);
		disPen.Color = color;
	}

	private void DrawPeakAndValeMark(int int_14, bool bool_3)
	{
		PointF pointF = lgToScr(curSignal.dots[int_14], bool_0: true);
		pointF.Y -= 2.5f;
		PointF pt = pointF;
		pt.Y += 5f;
		if (bool_3)
		{
			disPen.Color = Color.Green;
		}
		else
		{
			disPen.Color = Color.Red;
		}
		graphics_0.DrawLine(disPen, pointF, pt);
	}

	public override void DrawMouseLgValue()
	{
		if (evrmOK)
		{
			base.DrawMouseLgValue();
		}
	}

	public override void DrawName()
	{
	}

	public int GraphClick()
	{
		if (curSignal.dots.Length == 0)
		{
			return -1;
		}
		PointF pointF = scrToLg(ptScaleBegin, bool_0: true);
		float x = curSignal.dots[0].X;
		float x2 = curSignal.dots[curSignal.DotsNum - 1].X;
		int dotNo = curSignal.getDotNo(pointF.X);
		for (int i = 0; i < curSignal.PeaksNum; i++)
		{
			Peak peak = curSignal.peaks[i];
			if (peak.lfDotNo < dotNo && dotNo < peak.rtDotNo)
			{
				float y = curSignal.dots[dotNo].Y;
				int num = dotNo - peak.FromNo;
				if (num >= peak.bsYs.Length || num < 0)
				{
					return -1;
				}
				if ((peak.positive && y > pointF.Y && pointF.Y > peak.bsYs[dotNo - peak.FromNo]) || (!peak.positive && peak.bsYs[dotNo - peak.FromNo] > pointF.Y && pointF.Y > y))
				{
					return peak.pkN;
				}
			}
		}
		return -1;
	}

	protected override string labelName(Signal signal)
	{
		return signal.sample_name;
	}

	public Chromatogram LinkDisChroms(Chromatogram[] chroms, ref int setChromNo)
	{
		Chromatogram chromatogram;
		if (chroms.Length != 0)
		{
			setChromNo = Math.Max(0, setChromNo);
			if (setChromNo <= chroms.Length)
			{
				setChromNo = chroms.Length - 1;
			}
			chromatogram = chroms[setChromNo];
			curSignal = chromatogram.signal;
			Array.Resize(ref disSignals, chroms.Length);
			for (int i = 0; i < disSignals.Length; i++)
			{
				disSignals[i] = chroms[i].signal;
			}
			createSignalLabels();
			if (options == null)
			{
				options = new Options();
			}
			if (stDisChain.Count > 0)
			{
				if (!stDisChain.CurDisLg.Valid_xy)
				{
					if (chromatogram.disLg.Valid_xy)
					{
						stDisChain.ReplaceCurFrameLg(chromatogram.disLg);
					}
					else
					{
						disLg.lgXBeg = 0f;
						disLg.lgYBeg = -10f;
						disLg.lgX = 2f;
						disLg.lgY = 100f;
						stDisChain.ReplaceCurFrameLg(disLg);
					}
				}
			}
			else
			{
				stDisChain.Clear();
				SetFullDisLg(ref disLg, chromatogram.signal, second: true);
			}
			return chromatogram;
		}
		setChromNo = -1;
		chromatogram = null;
		curSignal = null;
		return null;
	}

	private void DrawString(bool bool_3, string string_3, ref PointF pointF_11)
	{
		SizeF sizeF = graphics_0.MeasureString(string_3, options.peakFont);
		PointF point = pointF_11;
		point.Y -= sizeF.Height * 0.9f / 2f;
		if (bool_3)
		{
			graphics_0.DrawString(string_3, options.peakFont, brush, point);
		}
		pointF_11.X += sizeF.Width;
	}

	protected override void signalLabel_DoubleClick(object sender, EventArgs e)
	{
		base.signalLabel_DoubleClick(sender, e);
	}

	protected override void signalLabel_MouseClick(object sender, MouseEventArgs e)
	{
		base.signalLabel_MouseClick(sender, e);
		displayPanel.Refresh();
	}
}
