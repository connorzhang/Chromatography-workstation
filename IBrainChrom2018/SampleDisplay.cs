using System.Drawing;
using System.Drawing.Drawing2D;
using System.Text;
using IBrainChrom2018.Unit;

namespace IBrainChrom2018;

public class SampleDisplay : BaseSignalDis
{
	private SystemParam sysParam = SystemParam.Create();

	private ChromDeviceListMgr cdl = ChromDeviceListMgr.Create();

	public Chromatogram bgChrom;

	private Color color_1;

	public string drawName;

	private bool bool_1;

	private bool bool_2;

	public bool showPeakArea;

	public new StringBuilder strDebugLog = new StringBuilder();

	public bool ShowBgChrom
	{
		get
		{
			return bool_1;
		}
		set
		{
			bool_1 = value;
			if (value)
			{
				bool_2 = true;
			}
		}
	}

	public SampleDisplay(WinStyle winStyle, LclDisplayPanel displayPanel)
		: base(winStyle, displayPanel)
	{
		color_1 = Color.Black;
		bgChrom = new Chromatogram();
		bool_1 = false;
		bool_2 = false;
		showPeakArea = false;
		drawName = "";
		txtX = Lang.PS("时间", "Time");
		txtY = Lang.PS("信号", "Voltage");
		unitX = "min.";
		unitY = Class49.MesureUnit();
		fmtX = "0.00";
		fmtY = "0.0";
		txtY_ = "Integration";
		unitY_ = "..";
		showMouseLgValue = true;
	}

	private Color GetChannelColor()
	{
		if (!bgChrom.signal.simple)
		{
			return sysParam.corChrgColoAcq;
		}
		return sysParam.GetChannelColor(cdl.CurrentChannelIdx);
	}

	public virtual void DrawSingle(bool evrmOK)
	{
		bool bool_ = base.Relayout_DisLg || base.Relayout_DskFrm || base.Relayout_Unit;
		if (bool_1)
		{
			if (bool_2)
			{
				method_22(bgChrom.signal, bool_3: true);
				bool_2 = false;
			}
			else
			{
				method_22(bgChrom.signal, bool_);
			}
			if (!ShowAll)
			{
				signal_show(bgChrom.signal, color_1, -1);
			}
		}
		if (ShowAll && disSignals != null)
		{
			for (int i = 0; i < disSignals.Length; i++)
			{
				float float_ = signal下限[i] * signalfactors[i] - signal下限[0];
				method_23(disSignals[i], bool_, signalfactors[i], float_);
				signal_show(disSignals[i], Class49.color_0[i], i);
			}
		}
	}

	public override void DrawFrameAndLabel(bool evrmOK)
	{
		base.DrawFrameAndLabel(evrmOK);
		graphics_0.SetClip(_frmRC);
		DrawSingle(evrmOK);
		if (isPrint)
		{
			graphics_0.SetClip(rcPage);
		}
		else
		{
			graphics_0.SetClip(dskRC);
		}
		DrawName();
		DrawPeakName();
	}

	public override void DrawFrameAndLabelPrintf(bool evrmOK)
	{
		base.DrawFrameAndLabelPrintf(evrmOK);
		graphics_0.SetClip(_frmRC);
		DrawSingle(evrmOK);
		graphics_0.SetClip(rcPage);
	}

	public virtual void DrawName()
	{
		SizeF sizeF = graphics_0.MeasureString(drawName, FrameDis.font);
		float y = frmRC.Top - sizeF.Height - 1f;
		brush.Color = Color.DarkGreen;
		graphics_0.DrawString(drawName, FrameDis.font, brush, frmRC.Left, y);
	}

	private void DrawPeakName()
	{
		if (curSignal == null)
		{
			return;
		}
		for (int i = 0; i < curSignal.peaks.Length; i++)
		{
			Peak peak = curSignal.peaks[i];
			if (peak.disNo < 0 || !(peak.area > 0f) || peak.pkN < 0 || peak.pkN >= curSignal.dots.Length)
			{
				continue;
			}
			if (disLg.lgXBeg < curSignal.dots[peak.pkN].X && curSignal.dots[peak.pkN].X < disLg.lgXBeg + disLg.lgX)
			{
				disPen.DashStyle = DashStyle.Solid;
				PointF pointF = lgToScr(curSignal.dots[peak.pkN], bool_0: true);
				PointF pointF_ = pointF;
				if (peak.positive)
				{
					pointF_.Y -= 5f;
				}
				else
				{
					pointF_.Y += 5f;
				}
				graphics_0.DrawLine(disPen, pointF, pointF_);
				float y = pointF_.Y;
				brush.Color = Color.Black;
				curSignal.disColor = ((curSignal.disColor == Color.Empty) ? Color.Black : curSignal.disColor);
				if (options.peakFtClrAsActiveSignal)
				{
					brush.Color = curSignal.disColor;
				}
				graphics_0.TranslateTransform(pointF_.X, pointF_.Y);
				graphics_0.RotateTransform(-90f);
				for (int j = 0; j <= 1; j++)
				{
					bool bool_;
					if (!(bool_ = j == 1))
					{
						float x = (pointF_.Y = 0f);
						pointF_.X = x;
					}
					else
					{
						float x2 = pointF_.X;
						pointF_.X = (peak.positive ? 0f : (0f - x2));
						pointF_.Y = 0f;
						float num2 = (peak.positive ? (y - x2) : y);
						float num3 = (peak.positive ? y : (y + x2));
						if (num2 < frmRC.Top)
						{
							pointF_.X -= frmRC.Top - num2;
						}
						if (num3 > frmRC.Bottom)
						{
							pointF_.X += num3 - frmRC.Bottom;
						}
					}
					if (options.peakRetenTime)
					{
						string string_ = peak.pkRT.ToString("0.000");
						DrawPeakNameLabel(bool_, string_, ref pointF_);
					}
					if (options.peakNumber)
					{
						string text = peak.disNo.ToString(" 0");
						if (text != " 0")
						{
							DrawPeakValueLabel(Color.Blue, bool_, text, ref pointF_);
						}
					}
					if (options.peakName)
					{
						string text2 = peak.name.Trim();
						if (text2 != "")
						{
							DrawPeakNameLabel(bool_, text2, ref pointF_);
						}
					}
					if (showPeakArea)
					{
						PointF pointF2 = pointF_;
						string text3 = peak.area.ToString();
						if (text3 != "")
						{
							pointF2.Y += 10f;
							DrawPeakNameLabel(bool_, text3, ref pointF_);
						}
					}
					if (options.peakGroupID && peak.groupID != 0)
					{
						string string_2 = peak.groupID.ToString();
						DrawPeakNameLabel(bool_, string_2, ref pointF_);
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
				DrawPeakMarkStartEndLine(peak.FromNo, bool_3: true);
				DrawPeakMarkStartEndLine(peak.ToNo, bool_3: false);
			}
			if (!flag && peak.selected)
			{
				method_20(peak.bsLfV.dotNo);
				method_20(peak.bsRtV.dotNo);
			}
			if (peak.RtDotNo < curSignal.dots.Length && curSignal.dots[peak.RtDotNo].X > disLg.lgXBeg && disLg.lgXBeg + disLg.lgX > curSignal.dots[peak.LfDotNo].X && options.baselineVisible)
			{
				disPen.DashStyle = DashStyle.Dot;
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
	}

	private void DrawPeakNameLabel(bool bool_3, string string_3, ref PointF pointF_11)
	{
		SizeF sizeF = graphics_0.MeasureString(string_3, options.peakFont);
		PointF point = pointF_11;
		point.Y -= sizeF.Height * 0.9f / 2f;
		if (bool_3)
		{
			brush.Color = ((brush.Color == Color.Empty) ? Color.Red : brush.Color);
			graphics_0.DrawString(string_3, options.peakFont, brush, point);
		}
		pointF_11.X += sizeF.Width;
	}

	private void DrawPeakValueLabel(Color color_2, bool bool_3, string string_3, ref PointF pointF_11)
	{
		Font font = new Font("Tahoma", 10f, FontStyle.Bold);
		SizeF sizeF = graphics_0.MeasureString(string_3, font);
		PointF point = pointF_11;
		point.Y -= sizeF.Height * 0.9f / 2f;
		if (bool_3)
		{
			brush.Color = color_2;
			graphics_0.DrawString(string_3, options.peakFont, brush, point);
		}
		pointF_11.X += sizeF.Width;
	}

	private void method_20(int int_14)
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

	private void DrawPeakMarkStartEndLine(int int_14, bool bool_3)
	{
		if (int_14 >= 0 && int_14 < curSignal.dots.Length)
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
	}

	private void method_22(Signal signal_0, bool bool_3)
	{
		if (bool_3)
		{
			base.signal_proc(signal_0);
		}
		else
		{
			if (signal_0.DotsNum <= 5)
			{
				return;
			}
			int disEnd = signal_0.disEnd;
			if (signal_0.JudgeBegEnd(this))
			{
				for (int i = disEnd + 1; i <= signal_0.disEnd; i++)
				{
					signal_0.WriteDisplay(i);
				}
			}
		}
	}

	private void method_23(Signal signal_0, bool bool_3, float float_10, float float_11)
	{
		if (bool_3)
		{
			base.signal_proc(signal_0, float_10, float_11);
		}
		else
		{
			if (signal_0.DotsNum <= 5)
			{
				return;
			}
			int num = 0;
			if (signal_0.JudgeBegEnd(this))
			{
				for (int i = num + 1; i <= signal_0.disEnd; i++)
				{
					signal_0.WriteDisplay(i, float_10, float_11);
				}
			}
		}
	}

	public Peak getPeak(int ChannelIndex, CaliGnl caliGnl)
	{
		PointF mouseLgValue = base.MouseLgValue;
		int i = 0;
		Signal signal = null;
		Peak result = null;
		if (disSignals != null)
		{
			signal = disSignals[ChannelIndex];
		}
		if (signal != null)
		{
			int numIndex = -1;
			for (int j = 0; j < signal.PeaksNum; j++)
			{
				numIndex = caliGnl.SetCompound(signal.peaks[j], signal.peaks, numIndex);
			}
			for (; i < signal.PeaksNum; i++)
			{
				float num = signal.peaks[i].Get_lf(signal.dots);
				float num2 = signal.peaks[i].Get_rt(signal.dots);
				if (num < mouseLgValue.X && mouseLgValue.X < num2)
				{
					result = signal.peaks[i];
					break;
				}
			}
		}
		return result;
	}
}
