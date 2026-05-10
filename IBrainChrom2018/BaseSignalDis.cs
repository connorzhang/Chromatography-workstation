using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Threading;
using System.Windows.Forms;

namespace IBrainChrom2018;

public class BaseSignalDis : GradientDisplay
{
	public delegate void SignalClick(int disSignalNo, Signal curSignal);

	public delegate void SignalDoubleClick(Signal curSignal);

	private const byte byte_2 = 3;

	private const int int_13 = 3;

	protected const int markLength = 5;

	protected const int shtMkLen = 3;

	public const int signalLabelHeight = 12;

	private bool bool_0;

	public Signal curSignal;

	public Signal[] disSignals;

	public bool IsDataAcq;

	public bool RefreshSignalLabels;

	private LclSignalLabel[] lclSignalLabel_0;

	private SignalClick signalClick_0;

	private SignalDoubleClick signalDoubleClick_0;

	public event SignalClick OnSignalClick
	{
		add
		{
			SignalClick signalClick = signalClick_0;
			SignalClick signalClick2;
			do
			{
				signalClick2 = signalClick;
				SignalClick value2 = (SignalClick)Delegate.Combine(signalClick2, value);
				signalClick = Interlocked.CompareExchange(ref signalClick_0, value2, signalClick2);
			}
			while (signalClick != signalClick2);
		}
		remove
		{
			SignalClick signalClick = signalClick_0;
			SignalClick signalClick2;
			do
			{
				signalClick2 = signalClick;
				SignalClick value2 = (SignalClick)Delegate.Remove(signalClick2, value);
				signalClick = Interlocked.CompareExchange(ref signalClick_0, value2, signalClick2);
			}
			while (signalClick != signalClick2);
		}
	}

	public event SignalDoubleClick OnSignalDoubleClick
	{
		add
		{
			SignalDoubleClick signalDoubleClick = signalDoubleClick_0;
			SignalDoubleClick signalDoubleClick2;
			do
			{
				signalDoubleClick2 = signalDoubleClick;
				SignalDoubleClick value2 = (SignalDoubleClick)Delegate.Combine(signalDoubleClick2, value);
				signalDoubleClick = Interlocked.CompareExchange(ref signalDoubleClick_0, value2, signalDoubleClick2);
			}
			while (signalDoubleClick != signalDoubleClick2);
		}
		remove
		{
			SignalDoubleClick signalDoubleClick = signalDoubleClick_0;
			SignalDoubleClick signalDoubleClick2;
			do
			{
				signalDoubleClick2 = signalDoubleClick;
				SignalDoubleClick value2 = (SignalDoubleClick)Delegate.Remove(signalDoubleClick2, value);
				signalDoubleClick = Interlocked.CompareExchange(ref signalDoubleClick_0, value2, signalDoubleClick2);
			}
			while (signalDoubleClick != signalDoubleClick2);
		}
	}

	public BaseSignalDis(WinStyle winStyle, LclDisplayPanel displayPanel)
		: base(winStyle, displayPanel)
	{
		IsDataAcq = false;
		bool_0 = false;
		disSignals = new Signal[0];
		lclSignalLabel_0 = new LclSignalLabel[0];
		signalfactors = new float[4];
		signalfactors[0] = -1f;
		signalfactors[1] = -1f;
		signalfactors[2] = -1f;
		signalfactors[3] = -1f;
		signal下限 = new float[4];
		signal上限 = new float[4];
		signal上限[0] = 0f;
		signal上限[1] = 0f;
		signal上限[2] = 0f;
		signal上限[3] = 0f;
		signal下限[0] = 0f;
		signal下限[1] = 0f;
		signal下限[2] = 0f;
		signal下限[3] = 0f;
		RefreshSignalLabels = false;
		showGradientLabels = false;
		refScaleXNum = 10;
		refScaleYNum = 7;
		refScaleY_Num = 7;
	}

	public void CalcuFullDisLg(ref DisLg disLg, float xMinTime, float xMaxTime, float yMinValue, float yMaxValue)
	{
		disLg.lgXBeg = xMinTime;
		disLg.lgX = (xMaxTime - xMinTime) * 1.05f;
		disLg.lgYBeg = yMinValue;
		disLg.lgY = yMaxValue - yMinValue;
		float num = disLg.lgY * 0.05f;
		disLg.lgYBeg -= num;
		disLg.lgY += num + num + num;
	}

	public void ClearDisSignals()
	{
		Array.Resize(ref disSignals, 0);
		Array.Resize(ref lclSignalLabel_0, 0);
		displayPanel.Controls.Clear();
		curSignal = null;
		stDisChain.Clear();
		stDisChain.AppendFrameLg(default(DisLg));
	}

	protected void createSignalLabels()
	{
		if (displayPanel != null)
		{
			displayPanel.Controls.Clear();
			Array.Resize(ref lclSignalLabel_0, 0);
			for (int i = 0; i < disSignals.Length; i++)
			{
				Signal tag = disSignals[i];
				Array.Resize(ref lclSignalLabel_0, lclSignalLabel_0.Length + 1);
				LclSignalLabel lclSignalLabel = (lclSignalLabel_0[lclSignalLabel_0.Length - 1] = new LclSignalLabel());
				displayPanel.Controls.Add(lclSignalLabel);
				lclSignalLabel.Tag = tag;
				lclSignalLabel.disNo = i;
				lclSignalLabel.MouseClick += signalLabel_MouseClick;
				lclSignalLabel.DoubleClick += signalLabel_DoubleClick;
			}
			RefreshSignalLabels = true;
		}
	}

	public override void DrawFrameAndLabel(bool evrmOK)
	{
		base.DrawFrameAndLabel(evrmOK);
		method_16();
	}

	public override bool drawEvrmPrep()
	{
		frmColor = Color.Black;
		if (curSignal != null)
		{
			frmColor = curSignal.disColor;
			if (instruStyle == InstruStyle.LC)
			{
				PrepareInfo(curSignal.linkLcGradient);
			}
			if (instruStyle == InstruStyle.GC)
			{
				PrepareInfo(curSignal.linkGcProgTemp);
			}
		}
		return base.drawEvrmPrep();
	}

	private void method_13(Signal signal_0, int int_14)
	{
		float x = signal_0.dots[int_14].X;
		if (disLg.lgXBeg < x && x < disLg.lgXBeg + disLg.lgX)
		{
			PointF pointF = method_14(signal_0, int_14, bool_1: false);
			PointF pt = pointF;
			pt.X -= 3f;
			pt.Y -= 3f;
			PointF pt2 = pointF;
			pt2.X += 3f;
			pt2.Y -= 3f;
			graphics_0.DrawLine(disPen, pointF, pt);
			graphics_0.DrawLine(disPen, pointF, pt2);
		}
	}

	private PointF method_14(Signal signal_0, int int_14, bool bool_1)
	{
		PointF pointF = lgToScr(signal_0.dots[int_14], bool_0: true);
		PointF pointF2 = pointF;
		pointF.Y -= 5f;
		pointF2.Y += 5f;
		graphics_0.DrawLine(disPen, pointF, pointF2);
		if (!bool_1)
		{
			return pointF2;
		}
		return pointF;
	}

	private void method_15(Signal signal_0, int int_14)
	{
		float x = signal_0.dots[int_14].X;
		if (disLg.lgXBeg < x && x < disLg.lgXBeg + disLg.lgX)
		{
			PointF pointF = method_14(signal_0, int_14, bool_1: true);
			PointF pt = pointF;
			pt.X -= 3f;
			pt.Y += 3f;
			PointF pt2 = pointF;
			pt2.X += 3f;
			pt2.Y += 3f;
			graphics_0.DrawLine(disPen, pointF, pt);
			graphics_0.DrawLine(disPen, pointF, pt2);
		}
	}

	protected virtual string labelName(Signal signal)
	{
		if (IsDataAcq && (!IsDataAcq || !signal.SampleV))
		{
			return "(" + signal.detector_name + ")";
		}
		return signal.detector_name;
	}

	public Signal LinkDisSignals(Signal[] signals, int setSignalNo, out int curSignalNo)
	{
		disSignals = signals;
		if (disSignals.Length != 0)
		{
			setSignalNo = Math.Max(0, setSignalNo);
			if (setSignalNo >= signals.Length)
			{
				curSignalNo = signals.Length - 1;
			}
			else
			{
				curSignalNo = setSignalNo;
			}
			curSignal = signals[curSignalNo];
		}
		else
		{
			curSignalNo = -1;
			curSignal = null;
		}
		createSignalLabels();
		return curSignal;
	}

	public override void LinkOptions(Options options)
	{
		base.LinkOptions(options);
		RefreshSignalLabels = true;
		showGrdtBelt = options.lcDisAuxYStyle == LcDisAuxYStyle.Gradient;
		showFlowLine = options.lcDisAuxYStyle == LcDisAuxYStyle.TotalFlow;
		showProgTemp = options.gcDisAuxYStyle == GcDisAuxYStyle.Temperature;
		bool_0 = true;
		prepareDisLg();
	}

	protected override void prepareDisLg()
	{
		if (stDisChain.Count == 0)
		{
			return;
		}
		DisLg curDisLg = stDisChain.CurDisLg;
		if (instruStyle == InstruStyle.LC)
		{
			switch (options.lcDisAuxYStyle)
			{
			case LcDisAuxYStyle.None:
				curDisLg.lgY_ = 0f;
				txtY_ = (unitY_ = "");
				break;
			case LcDisAuxYStyle.Gradient:
				curDisLg.lgY_Beg = 0f;
				curDisLg.lgY_ = 101f;
				txtY_ = "Gradient";
				unitY_ = "%";
				fmtY_ = "0";
				refScaleY_Num = 4;
				break;
			case LcDisAuxYStyle.TotalFlow:
			{
				float num = 0f;
				for (int i = 0; i < gradientRows.Length; i++)
				{
					num = Math.Max(num, gradientRows[i].flow);
				}
				num += num / 4f;
				num = Math.Max(1f, num);
				curDisLg.lgY_Beg = 0f;
				curDisLg.lgY_ = num;
				txtY_ = "Total Flow";
				unitY_ = "mL/min";
				fmtY_ = "0.0";
				refScaleY_Num = 4;
				break;
			}
			}
		}
		if (instruStyle == InstruStyle.GC)
		{
			switch (options.gcDisAuxYStyle)
			{
			case GcDisAuxYStyle.None:
				curDisLg.lgY_ = 0f;
				txtY_ = (unitY_ = "");
				break;
			case GcDisAuxYStyle.Temperature:
			{
				float num2 = 0f;
				for (int j = 0; j < progTempRows.Length; j++)
				{
					num2 = Math.Max(num2, progTempRows[j].endTemp);
				}
				num2 += num2 / 4f;
				num2 = Math.Max(1f, num2);
				curDisLg.lgY_Beg = 0f;
				curDisLg.lgY_ = num2;
				txtY_ = "温度";
				unitY_ = "℃";
				fmtY_ = "0.0";
				break;
			}
			}
		}
		if (bool_0)
		{
			stDisChain.Clear();
			stDisChain.AppendFrameLg(curDisLg);
			bool_0 = false;
		}
		else
		{
			stDisChain.ReplaceCurFrameLg(curDisLg);
		}
	}

	protected override PointF ptFlowProc(PointF ptSrc)
	{
		return lgToScr(ptSrc, bool_0: false);
	}

	private void method_16()
	{
		if (!ShowAll)
		{
			for (int i = 0; i < lclSignalLabel_0.Length; i++)
			{
				if (lclSignalLabel_0[i] != null)
				{
					lclSignalLabel_0[i].Text = "";
				}
			}
			return;
		}
		if (base.Relayout_DskFrm)
		{
			RefreshSignalLabels = true;
		}
		RefreshSignalLabels = true;
		if (!RefreshSignalLabels)
		{
			return;
		}
		RefreshSignalLabels = false;
		int num = Math.Min(200, Convert.ToInt32(frmRC.Width / 5f));
		int left = Convert.ToInt32(frmRC.Right - (float)num);
		for (int j = 0; j < lclSignalLabel_0.Length; j++)
		{
			LclSignalLabel lclSignalLabel = lclSignalLabel_0[j];
			Signal signal = lclSignalLabel.Tag as Signal;
			lclSignalLabel.Left = left;
			if (j == 0)
			{
				lclSignalLabel.Top = Convert.ToInt32(frmRC.Top + 3f);
			}
			else
			{
				lclSignalLabel.Top = lclSignalLabel_0[j - 1].Bottom + 3;
			}
			lclSignalLabel.ForeColor = signal.disColor;
			string text = "  ";
			if (signal == curSignal)
			{
				text = "> ";
			}
			string text2 = text + labelName(signal);
			lclSignalLabel.Set(text2, num);
		}
	}

	public bool SetFullDisLg(ref DisLg disLg, Signal signal, bool second)
	{
		if (signal != null && signal.xMaxTime > signal.xMinTime)
		{
			CalcuFullDisLg(ref disLg, signal.xMinTime, signal.xMaxTime, signal.yMinValue, second ? signal.SecondY : signal.yMaxValue);
			stDisChain.AppendFrameLg(disLg);
			return true;
		}
		return false;
	}

	protected virtual void signal_proc(Signal signal)
	{
		if (signal.DotsNum != 0 && signal.JudgeBegEnd(this))
		{
			for (int i = signal.disBeg; i <= signal.disEnd; i++)
			{
				signal.WriteDisplay(i);
			}
		}
	}

	protected virtual void signal_proc(Signal signal, float YPara, float float_10)
	{
		if (signal.DotsNum != 0 && signal.JudgeBegEnd(this))
		{
			for (int i = signal.disBeg; i <= signal.disEnd; i++)
			{
				signal.WriteDisplay(i, YPara, float_10);
			}
		}
	}

	private void WriteDebugLog2PointArray(PointF[] array)
	{
		base.DebugLog2 = "";
		for (int i = 0; i < array.Length; i++)
		{
			base.DebugLog2 = base.DebugLog2 + array[i].X + ",";
			base.DebugLog2 = base.DebugLog2 + array[i].Y + "\r\n";
		}
	}

	private bool FloatIsInfinite(float fval)
	{
		return float.IsNaN(fval) || (double)fval >= 2867300000.0 || (double)fval <= -2867305000.0;
	}

	protected void signal_show(Signal signal, Color cusColor, int Index)
	{
		if ((IsDataAcq && !signal.SampleV) || signal.disPts.Length < 2)
		{
			return;
		}
		disPen.DashStyle = DashStyle.Solid;
		if (cusColor != Color.Transparent)
		{
			if (!signal.simple)
			{
				disPen.Color = Class49.GetColor(1);
			}
			else
			{
				disPen.Color = Class49.GetColor(2);
			}
		}
		else
		{
			disPen.Color = signal.disColor;
		}
		if (signal.disPts.Length > 8000)
		{
			PointF[] array = new PointF[8000];
			for (int i = 0; i <= signal.disPts.Length / 8000; i++)
			{
				int num = i * 8000;
				int j;
				for (j = 0; j < 8000 && num + j < signal.disPts.Length; j++)
				{
					PointF pointF = signal.disPts[num + j];
					ref PointF reference = ref array[j];
					float x = (array[j].Y = 0f);
					reference.X = x;
					if (!FloatIsInfinite(pointF.X))
					{
						array[j].X = pointF.X;
					}
					if (!FloatIsInfinite(pointF.Y))
					{
						array[j].Y = pointF.Y;
					}
				}
				if (j < 4)
				{
					break;
				}
				Array.Resize(ref array, j);
				array[0] = array[1];
				graphics_0.DrawLines(disPen, array);
			}
		}
		else if (signal.disPts.Length > 2)
		{
			int num3 = signal.disPts.Length;
			PointF[] array2 = new PointF[num3];
			for (int k = 0; k < num3; k++)
			{
				ref PointF reference2 = ref array2[k];
				float x = (array2[k].Y = 0f);
				reference2.X = x;
				PointF pointF2 = signal.disPts[k];
				if (!FloatIsInfinite(pointF2.X))
				{
					array2[k].X = pointF2.X;
				}
				if (!FloatIsInfinite(pointF2.Y))
				{
					array2[k].Y = pointF2.Y;
				}
			}
			array2[0] = array2[1];
			graphics_0.DrawLines(disPen, array2);
		}
		if (signal.startNs != null)
		{
			for (int l = 0; l < signal.startNs.Length; l++)
			{
				method_15(signal, signal.startNs[l]);
			}
		}
		if (signal.endNs != null)
		{
			for (int m = 0; m < signal.endNs.Length; m++)
			{
				method_13(signal, signal.endNs[m]);
			}
		}
	}

	protected virtual void signalLabel_DoubleClick(object sender, EventArgs e)
	{
		SetFullDisLg(ref disLg, curSignal, second: false);
		if (signalDoubleClick_0 != null)
		{
			signalDoubleClick_0(curSignal);
		}
	}

	protected virtual void signalLabel_MouseClick(object sender, MouseEventArgs e)
	{
		LclSignalLabel lclSignalLabel = sender as LclSignalLabel;
		Signal signal = lclSignalLabel.Tag as Signal;
		if (IsDataAcq && e.Button == MouseButtons.Right)
		{
			signal.SampleV = !signal.SampleV;
			if (!signal.SampleV)
			{
				RefreshSignalLabels = true;
				method_16();
				return;
			}
		}
		curSignal = signal;
		curSignal.needReCalcuDis = true;
		RefreshSignalLabels = true;
		method_16();
		if (signalClick_0 != null)
		{
			signalClick_0(lclSignalLabel.disNo, curSignal);
		}
	}
}
