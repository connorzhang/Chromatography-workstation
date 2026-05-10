using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using IBrainChrom2018.Unit;

namespace IBrainChrom2018;

public class GradientDisplay : FrameDis
{
	private SystemParam sysParam = SystemParam.Create();

	private PointF[] pointF_10;

	private GrdtOpt grdtOpt_0;

	protected GradientRow[] gradientRows;

	private Class62[] class62_0;

	private RectangleF rectangleF_2;

	protected float progTempInitHoldT;

	private float float_9;

	protected ProgTRow[] progTempRows;

	public bool showFlowLine;

	public bool showGradientLabels;

	public bool showGrdtBelt;

	public bool showProgTemp;

	private Pen penProgTemp = new Pen(Color.Blue);

	public GradientDisplay(WinStyle winStyle, LclDisplayPanel displayPanel)
		: base(winStyle, displayPanel)
	{
		gradientRows = new GradientRow[0];
		float_9 = 100f;
		progTempRows = new ProgTRow[0];
		grdtOpt_0 = new GrdtOpt();
		pointF_10 = new PointF[0];
		showGradientLabels = true;
		rectangleF_2 = new Rectangle(0, 0, 14, 9);
		class62_0 = new Class62[0];
		showFlowLine = true;
		showGrdtBelt = true;
		showProgTemp = false;
		txtX = "Time";
		unitX = "min.";
		fmtX = "0.00";
		refScaleXNum = 4;
		refScaleYNum = 4;
		refScaleY_Num = 4;
		frmPen.Color = (brush.Color = Color.Black);
		showMouseLgValue = true;
		stDisChain.AppendFrameLg(default(DisLg));
		penProgTemp.DashStyle = DashStyle.DashDot;
	}

	private void method_10()
	{
		for (int i = 0; i < class62_0.Length; i++)
		{
			Array.Resize(ref class62_0[i].struct7_1, class62_0[i].struct7_0.Length);
			for (int j = 0; j < class62_0[i].struct7_1.Length; j++)
			{
				class62_0[i].struct7_1[j].pointF_0 = lgToScr(class62_0[i].struct7_0[j].pointF_0, bool_0: false);
				class62_0[i].struct7_1[j].pointF_1 = lgToScr(class62_0[i].struct7_0[j].pointF_1, bool_0: false);
			}
		}
	}

	public override void DrawFrameAndLabel(bool evrmOK)
	{
		base.DrawFrameAndLabel(evrmOK);
		if (!evrmOK)
		{
			return;
		}
		if (instruStyle == InstruStyle.LC)
		{
			if (showGrdtBelt)
			{
				graphics_0.SetClip(_frmRC);
				if (base.Relayout_DskFrm || base.Relayout_DisLg)
				{
					method_10();
				}
				for (int i = 0; i < class62_0.Length; i++)
				{
					brush.Color = (isPrint ? RptSetupDlg._clr(psColors, class62_0[i].color_0) : class62_0[i].color_0);
					for (int j = 0; j < class62_0[i].struct7_1.Length - 1; j++)
					{
						PointF[] points = new PointF[4]
						{
							class62_0[i].struct7_1[j].pointF_0,
							class62_0[i].struct7_1[j].pointF_1,
							class62_0[i].struct7_1[j + 1].pointF_1,
							class62_0[i].struct7_1[j + 1].pointF_0
						};
						graphics_0.FillPolygon(brush, points);
					}
				}
				if (isPrint)
				{
					graphics_0.SetClip(rcPage);
				}
				else
				{
					graphics_0.SetClip(dskRC);
				}
			}
			if (showGradientLabels)
			{
				int num = 0;
				float num2 = frmRC.Width / 4f;
				rectangleF_2.Y = frmRC.Top - rectangleF_2.Height - 4f;
				frmPen.Color = Color.Black;
				if (grdtOpt_0.hasSolvent1)
				{
					rectangleF_2.X = frmRC.Left + (float)num * num2;
					brush.Color = (isPrint ? RptSetupDlg._clr(psColors, options.gradSolvClrA) : options.gradSolvClrA);
					graphics_0.FillRectangle(brush, rectangleF_2);
					brush.Color = Color.Black;
					graphics_0.DrawRectangle(frmPen, rectangleF_2.Left, rectangleF_2.Top, rectangleF_2.Width, rectangleF_2.Height);
					graphics_0.DrawString(grdtOpt_0.solvent1Name, FrameDis.font, brush, rectangleF_2.Right + 3f, rectangleF_2.Top - 2f);
					num++;
				}
				if (grdtOpt_0.hasSolvent2)
				{
					rectangleF_2.X = frmRC.Left + (float)num * num2;
					brush.Color = (isPrint ? RptSetupDlg._clr(psColors, options.gradSolvClrB) : options.gradSolvClrB);
					graphics_0.FillRectangle(brush, rectangleF_2);
					brush.Color = Color.Black;
					graphics_0.DrawRectangle(frmPen, rectangleF_2.Left, rectangleF_2.Top, rectangleF_2.Width, rectangleF_2.Height);
					graphics_0.DrawString(grdtOpt_0.solvent2Name, FrameDis.font, brush, rectangleF_2.Right + 3f, rectangleF_2.Top - 2f);
					num++;
				}
				if (grdtOpt_0.hasSolvent3)
				{
					rectangleF_2.X = frmRC.Left + (float)num * num2;
					brush.Color = (isPrint ? RptSetupDlg._clr(psColors, options.gradSolvClrC) : options.gradSolvClrC);
					graphics_0.FillRectangle(brush, rectangleF_2);
					brush.Color = Color.Black;
					graphics_0.DrawRectangle(frmPen, rectangleF_2.Left, rectangleF_2.Top, rectangleF_2.Width, rectangleF_2.Height);
					graphics_0.DrawString(grdtOpt_0.solvent3Name, FrameDis.font, brush, rectangleF_2.Right + 3f, rectangleF_2.Top - 2f);
					num++;
				}
				if (grdtOpt_0.hasSolvent4)
				{
					rectangleF_2.X = frmRC.Left + (float)num * num2;
					brush.Color = (isPrint ? RptSetupDlg._clr(psColors, options.gradSolvClrD) : options.gradSolvClrD);
					graphics_0.FillRectangle(brush, rectangleF_2);
					brush.Color = Color.Black;
					graphics_0.DrawRectangle(frmPen, rectangleF_2.Left, rectangleF_2.Top, rectangleF_2.Width, rectangleF_2.Height);
					graphics_0.DrawString(grdtOpt_0.solvent4Name, FrameDis.font, brush, rectangleF_2.Right + 3f, rectangleF_2.Top - 2f);
					num++;
				}
			}
			if (showFlowLine && gradientRows.Length > 1)
			{
				graphics_0.SetClip(_frmRC);
				PointF[] array = new PointF[gradientRows.Length];
				for (int k = 0; k < array.Length; k++)
				{
					array[k].X = gradientRows[k].time;
					array[k].Y = gradientRows[k].flow;
					array[k] = ptFlowProc(array[k]);
				}
				frmPen.Color = Color.Black;
				graphics_0.DrawLines(frmPen, array);
				if (isPrint)
				{
					graphics_0.SetClip(rcPage);
				}
				else
				{
					graphics_0.SetClip(dskRC);
				}
			}
		}
		if (instruStyle != InstruStyle.GC || !showProgTemp || progTempRows.Length <= 1)
		{
			return;
		}
		Array.Resize(ref pointF_10, 2);
		pointF_10[0].X = 0f;
		pointF_10[0].Y = float_9;
		pointF_10[1].X = progTempInitHoldT;
		pointF_10[1].Y = pointF_10[0].Y;
		float num3 = pointF_10[1].X;
		float num4 = pointF_10[1].Y;
		for (int l = 0; l < progTempRows.Length; l++)
		{
			if (progTempRows[l].Valid)
			{
				float num5 = (progTempRows[l].endTemp - num4) / progTempRows[l].upRate;
				int num6 = pointF_10.Length;
				Array.Resize(ref pointF_10, num6 + 2);
				num3 += num5;
				pointF_10[num6].X = num3;
				float num7 = (pointF_10[num6].Y = progTempRows[l].endTemp);
				num4 = num7;
				num3 += progTempRows[l].holdTime;
				pointF_10[num6 + 1].X = num3;
				pointF_10[num6 + 1].Y = num4;
			}
		}
		graphics_0.SetClip(_frmRC);
		for (int m = 0; m < pointF_10.Length; m++)
		{
			pointF_10[m] = ptFlowProc(pointF_10[m]);
		}
		frmPen.Color = Color.Black;
		if (Class49.bool_2 && showProgTemp)
		{
			graphics_0.DrawLines(penProgTemp, pointF_10);
		}
		if (isPrint)
		{
			graphics_0.SetClip(rcPage);
		}
		else
		{
			graphics_0.SetClip(dskRC);
		}
	}

	private bool method_11(float float_10)
	{
		for (int i = 0; i < progTempRows.Length; i++)
		{
			if (progTempRows[i].endTemp == float_10)
			{
				return true;
			}
		}
		return false;
	}

	private bool method_12(float float_10)
	{
		for (int i = 0; i < gradientRows.Length; i++)
		{
			if (gradientRows[i].time == float_10)
			{
				return true;
			}
		}
		return false;
	}

	protected virtual void prepareDisLg()
	{
		DisLg disLg = default(DisLg);
		if (instruStyle == InstruStyle.LC)
		{
			float num = 0f;
			for (int i = 0; i < gradientRows.Length; i++)
			{
				num = Math.Max(num, gradientRows[i].flow);
			}
			num += num / 4f;
			num = Math.Max(1f, num);
			float val = 0f;
			if (gradientRows.Length != 0)
			{
				GradientRow gradientRow = gradientRows[gradientRows.Length - 1];
				val = gradientRow.time;
				val += val / 20f;
			}
			disLg.lgXBeg = 0f;
			val = Math.Max(1f, val);
			disLg.lgX = val;
			disLg.lgYBeg = 0f;
			disLg.lgY = num;
			disLg.lgY_Beg = 0f;
			disLg.lgY_ = 101f;
		}
		if (instruStyle == InstruStyle.GC)
		{
			float num2 = 0f;
			for (int j = 0; j < progTempRows.Length; j++)
			{
				num2 = Math.Max(num2, progTempRows[j].endTemp);
			}
			num2 += num2 / 4f;
			num2 = Math.Max(1f, num2);
			float val2 = 0f;
			if (showProgTemp && progTempRows.Length > 1)
			{
				Array.Resize(ref pointF_10, 2);
				pointF_10[0].X = 0f;
				pointF_10[0].Y = float_9;
				pointF_10[1].X = progTempInitHoldT;
				pointF_10[1].Y = pointF_10[0].Y;
				float num3 = pointF_10[1].X;
				float num4 = pointF_10[1].Y;
				for (int k = 0; k < progTempRows.Length; k++)
				{
					if (progTempRows[k].Valid)
					{
						float num5 = (progTempRows[k].endTemp - num4) / progTempRows[k].upRate;
						int num6 = pointF_10.Length;
						Array.Resize(ref pointF_10, num6 + 2);
						num3 += num5;
						pointF_10[num6].X = num3;
						float num7 = (pointF_10[num6].Y = progTempRows[k].endTemp);
						num4 = num7;
						num3 += progTempRows[k].holdTime;
						pointF_10[num6 + 1].X = num3;
						pointF_10[num6 + 1].Y = num4;
					}
				}
				val2 = num3 * 1.05f;
			}
			disLg.lgXBeg = 0f;
			val2 = Math.Max(1f, val2);
			disLg.lgX = val2;
			disLg.lgYBeg = 0f;
			disLg.lgY = num2;
		}
		stDisChain.ReplaceCurFrameLg(disLg);
	}

	public void PrepareInfo(GcProgTemp gcProgTemp)
	{
		if (gcProgTemp == null)
		{
			gcProgTemp = new GcProgTemp();
		}
		float_9 = gcProgTemp.SetT6[1];
		progTempInitHoldT = gcProgTemp.initHoldTime;
		ProgTRow[] array = gcProgTemp.progTempRows;
		Array.Resize(ref progTempRows, 0);
		for (int i = 0; i < array.Length; i++)
		{
			if (array[i].upRate >= 0f && array[i].holdTime >= 0f && !method_11(array[i].endTemp))
			{
				int num = progTempRows.Length;
				Array.Resize(ref progTempRows, num + 1);
				progTempRows[num] = array[i];
			}
		}
		for (int j = 0; j < progTempRows.Length; j++)
		{
			for (int k = j + 1; k < progTempRows.Length; k++)
			{
				if (progTempRows[k].endTemp < progTempRows[j].endTemp)
				{
					ProgTRow progTRow = progTempRows[j];
					progTempRows[j] = progTempRows[k];
					progTempRows[k] = progTRow;
				}
			}
		}
		prepareDisLg();
	}

	public void PrepareInfo(LcGradient lcGradient)
	{
		GradientRow[] array = lcGradient.gradientRows;
		grdtOpt_0.LoadFromObject(lcGradient.gradientOption);
		Array.Resize(ref gradientRows, 0);
		for (int i = 0; i < array.Length; i++)
		{
			if (array[i].time >= 0f && !method_12(array[i].time))
			{
				Array.Resize(ref gradientRows, gradientRows.Length + 1);
				gradientRows[gradientRows.Length - 1] = array[i];
			}
		}
		for (int j = 0; j < gradientRows.Length; j++)
		{
			for (int k = j + 1; k < gradientRows.Length; k++)
			{
				if (gradientRows[k].time < gradientRows[j].time)
				{
					GradientRow gradientRow = gradientRows[j];
					gradientRows[j] = gradientRows[k];
					gradientRows[k] = gradientRow;
				}
			}
		}
		prepareDisLg();
		Array.Resize(ref class62_0, 0);
		float[] array2 = new float[gradientRows.Length];
		for (int l = 0; l < array2.Length; l++)
		{
			array2[l] = 0f;
		}
		if (grdtOpt_0.hasSolvent1)
		{
			Array.Resize(ref class62_0, class62_0.Length + 1);
			class62_0[class62_0.Length - 1] = new Class62();
			class62_0[class62_0.Length - 1].color_0 = options.gradSolvClrA;
			class62_0[class62_0.Length - 1].struct7_0 = new Struct7[gradientRows.Length];
			for (int m = 0; m < gradientRows.Length; m++)
			{
				class62_0[class62_0.Length - 1].struct7_0[m].pointF_0.Y = array2[m];
				array2[m] += gradientRows[m].float_0 * 100f;
				class62_0[class62_0.Length - 1].struct7_0[m].pointF_1.Y = array2[m];
			}
		}
		if (grdtOpt_0.hasSolvent2)
		{
			Array.Resize(ref class62_0, class62_0.Length + 1);
			class62_0[class62_0.Length - 1] = new Class62();
			class62_0[class62_0.Length - 1].color_0 = options.gradSolvClrB;
			class62_0[class62_0.Length - 1].struct7_0 = new Struct7[gradientRows.Length];
			for (int n = 0; n < gradientRows.Length; n++)
			{
				class62_0[class62_0.Length - 1].struct7_0[n].pointF_0.Y = array2[n];
				array2[n] += gradientRows[n].float_1 * 100f;
				class62_0[class62_0.Length - 1].struct7_0[n].pointF_1.Y = array2[n];
			}
		}
		if (grdtOpt_0.hasSolvent3)
		{
			Array.Resize(ref class62_0, class62_0.Length + 1);
			class62_0[class62_0.Length - 1] = new Class62();
			class62_0[class62_0.Length - 1].color_0 = options.gradSolvClrC;
			class62_0[class62_0.Length - 1].struct7_0 = new Struct7[gradientRows.Length];
			for (int num = 0; num < gradientRows.Length; num++)
			{
				class62_0[class62_0.Length - 1].struct7_0[num].pointF_0.Y = array2[num];
				array2[num] += gradientRows[num].float_2 * 100f;
				class62_0[class62_0.Length - 1].struct7_0[num].pointF_1.Y = array2[num];
			}
		}
		if (grdtOpt_0.hasSolvent4)
		{
			Array.Resize(ref class62_0, class62_0.Length + 1);
			class62_0[class62_0.Length - 1] = new Class62();
			class62_0[class62_0.Length - 1].color_0 = options.gradSolvClrD;
			class62_0[class62_0.Length - 1].struct7_0 = new Struct7[gradientRows.Length];
			for (int num2 = 0; num2 < gradientRows.Length; num2++)
			{
				class62_0[class62_0.Length - 1].struct7_0[num2].pointF_0.Y = array2[num2];
				array2[num2] += gradientRows[num2].float_3 * 100f;
				class62_0[class62_0.Length - 1].struct7_0[num2].pointF_1.Y = array2[num2];
			}
		}
		for (int num3 = 0; num3 < class62_0.Length; num3++)
		{
			for (int num4 = 0; num4 < gradientRows.Length; num4++)
			{
				ref PointF reference = ref class62_0[num3].struct7_0[num4].pointF_0;
				float x = (class62_0[num3].struct7_0[num4].pointF_1.X = gradientRows[num4].time);
				reference.X = x;
			}
		}
		method_10();
	}

	protected virtual PointF ptFlowProc(PointF ptSrc)
	{
		return lgToScr(ptSrc, bool_0: true);
	}
}
