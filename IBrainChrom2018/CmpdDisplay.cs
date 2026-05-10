using System;
using System.Drawing;

namespace IBrainChrom2018;

internal class CmpdDisplay : FrameDis
{
	private const int int_13 = 300;

	private const int int_14 = 5;

	private Compound compound_0;

	private CmpdFunc cmpdFunc_0;

	private PointF[][] pointF_10;

	private int int_15;

	private Struct4[] struct4_0;

	private PointF[] pointF_11;

	private int int_16;

	private PointF[] pointF_12;

	private PointF[][] pointF_13;

	private int int_17;

	public CmpdDisplay(WinStyle winStyle_0, LclDisplayPanel lclDisplayPanel_0)
		: base(winStyle_0, lclDisplayPanel_0)
	{
		pointF_12 = new PointF[0];
		pointF_11 = new PointF[0];
		struct4_0 = new Struct4[0];
		refScaleYNum = 6;
		if (options == null)
		{
			options = new Options();
		}
	}

	private void method_10(Enum8 enum8_0)
	{
		switch (enum8_0)
		{
		case Enum8.const_1:
			method_12();
			break;
		case Enum8.const_2:
			method_11();
			break;
		}
	}

	private void method_11()
	{
		if (int_15 >= 0 && struct4_0[int_15].method_0() < disLg.lgX / 1000f)
		{
			Array.Resize(ref struct4_0, int_15);
			int_15--;
		}
	}

	private void method_12()
	{
		if (int_17 >= 0)
		{
			if (int_16 < 4)
			{
				Array.Resize(ref pointF_13, int_17);
				int_17--;
			}
			else
			{
				Array.Resize(ref pointF_13[int_17], int_16);
			}
		}
	}

	public override void DrawFrameAndLabel(bool evrmOK)
	{
		if (options == null)
		{
			options = new Options();
		}
		base.DrawFrameAndLabel(evrmOK);
		if (compound_0 != null)
		{
			string text = compound_0.cmpdInfo.name + " - " + compound_0.cmpdInfo.retainTime.ToString(disMouseLgFmtX) + " min";
			SizeF sizeF = graphics_0.MeasureString(text, FrameDis.font);
			graphics_0.DrawString(text, FrameDis.font, brush, frmRC.Left + (frmRC.Width - sizeF.Width) / 2f, frmRC.Top - sizeF.Height - 5f);
			graphics_0.SetClip(frmRC);
			method_13();
			method_14();
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

	private void method_13()
	{
		if (!cmpdFunc_0.IsValideData)
		{
			return;
		}
		Enum8 @enum = Enum8.const_0;
		int_15 = -1;
		int_17 = -1;
		PointF lgValue = default(PointF);
		Array.Resize(ref pointF_13, 0);
		Array.Resize(ref struct4_0, 0);
		float num = disLg.lgY / 299f;
		for (int i = 1; i < 300; i++)
		{
			lgValue.Y = disLg.lgYBeg + (float)i * num;
			float[] array = cmpdFunc_0.Calcu_amountF(lgValue.Y);
			if (array.Length == 0)
			{
				method_10(@enum);
				@enum = Enum8.const_0;
			}
			else if (array.Length == 1)
			{
				lgValue.X = array[0];
				switch (@enum)
				{
				case Enum8.const_0:
				case Enum8.const_2:
					method_10(@enum);
					int_17++;
					Array.Resize(ref pointF_13, int_17 + 1);
					Array.Resize(ref pointF_13[int_17], 300);
					int_16 = 0;
					pointF_13[int_17][int_16++] = lgToScr(lgValue, bool_0: true);
					break;
				case Enum8.const_1:
					pointF_13[int_17][int_16++] = lgToScr(lgValue, bool_0: true);
					break;
				}
				@enum = Enum8.const_1;
			}
			else
			{
				switch (@enum)
				{
				case Enum8.const_0:
				case Enum8.const_1:
					method_10(@enum);
					int_15++;
					Array.Resize(ref struct4_0, int_15 + 1);
					struct4_0[int_15].float_0 = Class49.MinValue(array);
					struct4_0[int_15].float_1 = Class49.MaxValue(array);
					break;
				case Enum8.const_2:
					struct4_0[int_15].float_0 = Math.Min(struct4_0[int_15].float_0, Class49.MinValue(array));
					struct4_0[int_15].float_1 = Math.Max(struct4_0[int_15].float_1, Class49.MaxValue(array));
					break;
				}
				@enum = Enum8.const_2;
			}
		}
		method_10(@enum);
		disPen.Color = frmColor;
		for (int j = 0; j < pointF_13.Length; j++)
		{
			graphics_0.DrawLines(disPen, pointF_13[j]);
		}
		Array.Resize(ref pointF_10, struct4_0.Length);
		num = disLg.lgX / 299f;
		for (int k = 0; k < pointF_10.Length; k++)
		{
			int num2 = Convert.ToInt32((struct4_0[k].float_1 - struct4_0[k].float_0) / num) + 1;
			Array.Resize(ref pointF_10[k], num2);
			for (int l = 0; l < num2; l++)
			{
				lgValue.X = Convert.ToSingle(struct4_0[k].float_0 + (float)l * num);
				lgValue.Y = cmpdFunc_0.CalcuValue(lgValue.X);
				pointF_10[k][l] = lgToScr(lgValue, bool_0: true);
			}
		}
		disPen.Color = Color.Blue;
		for (int m = 0; m < pointF_10.Length; m++)
		{
			graphics_0.DrawLines(disPen, pointF_10[m]);
		}
	}

	private void method_14()
	{
		PointF[] array = new PointF[pointF_12.Length];
		PointF[] array2 = new PointF[pointF_11.Length];
		for (int i = 0; i < pointF_12.Length; i++)
		{
			array[i] = lgToScr(pointF_12[i], bool_0: true);
		}
		for (int j = 0; j < pointF_11.Length; j++)
		{
			array2[j] = lgToScr(pointF_11[j], bool_0: true);
		}
		disPen.Color = ((!isPrint) ? Color.Red : (psColors ? Color.Red : Color.Black));
		for (int k = 0; k < array.Length; k++)
		{
			graphics_0.DrawLine(disPen, array[k].X - 5f, array[k].Y, array[k].X + 5f, array[k].Y);
			graphics_0.DrawLine(disPen, array[k].X, array[k].Y - 5f, array[k].X, array[k].Y + 5f);
		}
		for (int l = 0; l < array2.Length; l++)
		{
			graphics_0.DrawEllipse(disPen, array2[l].X - 5f, array2[l].Y - 5f, 10f, 10f);
		}
	}

	public void method_15(Compound compound_1, bool bool_0, string string_3, ref string string_4)
	{
		compound_0 = compound_1;
		if (bool_0)
		{
			cmpdFunc_0 = compound_1.iFunc;
		}
		else
		{
			cmpdFunc_0 = compound_1.eFunc;
		}
		if (bool_0)
		{
			if (compound_1.cmpdInfo.istdCmpd != null)
			{
				txtY = "Response/ISTD Response";
				unitY = "";
				fmtY = "0.0";
				txtX = "Amount/ISTD Amount";
				unitX = "";
				fmtX = "0.0";
			}
			else
			{
				txtY = "Response";
				unitY = string_4;
				txtX = "Amount";
				unitX = string_3;
				fmtX = "";
				fmtY = "0";
			}
		}
		else
		{
			txtY = "Response";
			unitY = string_4;
			txtX = "Amount";
			unitX = string_3;
			fmtX = "";
			fmtY = "0";
		}
		string_4 = unitY;
		if (cmpdFunc_0 == null)
		{
			disLg.lgXBeg = 0f;
			disLg.lgX = 100f;
			disLg.lgYBeg = 0f;
			disLg.lgY = 100f;
			Array.Resize(ref pointF_12, 0);
			Array.Resize(ref pointF_11, 0);
		}
		else
		{
			disLg = cmpdFunc_0.disLg;
			Array.Resize(ref pointF_12, compound_1.levels.Length);
			Array.Resize(ref pointF_11, compound_1.levels.Length);
			int newSize = 0;
			int newSize2 = 0;
			for (int i = 0; i < compound_1.levels.Length; i++)
			{
				if (bool_0)
				{
					if (compound_1.levels[i].iFuncPt.IsValid)
					{
						if (compound_1.levels[i].used)
						{
							pointF_12[newSize++] = compound_1.levels[i].iFuncPt.AsPointF;
						}
						else
						{
							pointF_11[newSize2++] = compound_1.levels[i].iFuncPt.AsPointF;
						}
					}
				}
				else if (compound_1.levels[i].eFuncPt.IsValid)
				{
					if (compound_1.levels[i].used)
					{
						pointF_12[newSize++] = compound_1.levels[i].eFuncPt.AsPointF;
					}
					else
					{
						pointF_11[newSize2++] = compound_1.levels[i].eFuncPt.AsPointF;
					}
				}
			}
			Array.Resize(ref pointF_12, newSize);
			Array.Resize(ref pointF_11, newSize2);
		}
		stDisChain.ReplaceCurFrameLg(disLg);
	}

	public override void DrawMouseLgValue()
	{
		graphics_0 = Graphics.FromHwnd(displayPanel.Handle);
		solidBrush_0.Color = options.BackClrBorder;
		graphics_0.FillRectangle(solidBrush_0, rcMouseLgValue);
		drawMouseLgValue(graphics_0);
	}

	public void SetCompound(Compound compound_1, bool bool_0, string string_3, ref string string_4)
	{
		double[] array = new double[2];
		compound_0 = compound_1;
		if (bool_0)
		{
			cmpdFunc_0 = compound_1.iFunc;
		}
		else
		{
			cmpdFunc_0 = compound_1.eFunc;
		}
		if (bool_0)
		{
			if (compound_1.cmpdInfo.istdCmpd != "")
			{
				txtY = "Response/ISTD Response";
				unitY = "";
				fmtY = "0.0";
				txtX = "Amount/ISTD Amount";
				unitX = "";
				fmtX = "0.0";
			}
			else
			{
				txtY = "Response";
				unitY = string_4;
				txtX = "Amount";
				unitX = string_3;
				fmtX = "";
				fmtY = "0";
			}
		}
		else
		{
			txtY = "Response";
			unitY = string_4;
			txtX = "Amount";
			unitX = string_3;
			fmtX = "";
			fmtY = "0";
		}
		string_4 = unitY;
		if (cmpdFunc_0 == null)
		{
			disLg.lgXBeg = 0f;
			disLg.lgX = 100f;
			disLg.lgYBeg = 0f;
			disLg.lgY = 100f;
			Array.Resize(ref pointF_12, 0);
			Array.Resize(ref pointF_11, 0);
		}
		else
		{
			disLg = cmpdFunc_0.disLg;
			Array.Resize(ref pointF_12, compound_1.levels.Length);
			Array.Resize(ref pointF_11, compound_1.levels.Length);
			int num = 0;
			int newSize = 0;
			for (int i = 0; i < compound_1.levels.Length; i++)
			{
				if (bool_0)
				{
					if (compound_1.levels[i].iFuncPt.IsValid)
					{
						if (compound_1.levels[i].used)
						{
							pointF_12[num++] = compound_1.levels[i].iFuncPt.AsPointF;
						}
						else
						{
							pointF_11[newSize++] = compound_1.levels[i].iFuncPt.AsPointF;
						}
					}
				}
				else if (compound_1.levels[i].eFuncPt.IsValid)
				{
					if (compound_1.levels[i].used)
					{
						pointF_12[num++] = compound_1.levels[i].eFuncPt.AsPointF;
					}
					else
					{
						pointF_11[newSize++] = compound_1.levels[i].eFuncPt.AsPointF;
					}
				}
			}
			Array.Resize(ref pointF_12, num);
			Array.Resize(ref pointF_11, newSize);
			if (cmpdFunc_0.curveFit == CurveFit.Exponent)
			{
				for (int j = 0; j < num; j++)
				{
					pointF_12[j].X = Convert.ToSingle(Math.Log(pointF_12[j].X));
					pointF_12[j].Y = Convert.ToSingle(Math.Log(pointF_12[j].Y * 1000f));
				}
				if (disLg.lgXBeg < 0f)
				{
					disLg.lgX -= disLg.lgXBeg;
				}
				if (disLg.lgYBeg < 0f)
				{
					disLg.lgY = 0f;
				}
			}
		}
		stDisChain.ReplaceCurFrameLg(disLg);
	}

	public void SetCompound2(Compound compound_1, bool bool_0, string string_3, ref string string_4)
	{
		double[] array = new double[2];
		compound_0 = compound_1;
		if (bool_0)
		{
			cmpdFunc_0 = compound_1.iFunc;
		}
		else
		{
			cmpdFunc_0 = compound_1.eFunc;
		}
		if (bool_0)
		{
			if (compound_1.cmpdInfo.istdCmpd != "")
			{
				txtY = "Response/ISTD Response";
				unitY = "";
				fmtY = "0.0";
				txtX = "Amount/ISTD Amount";
				unitX = "";
				fmtX = "0.0";
			}
			else
			{
				txtY = "Response";
				unitY = string_4;
				txtX = "Amount";
				unitX = string_3;
				fmtX = "";
				fmtY = "0";
			}
		}
		else
		{
			txtY = "Response";
			unitY = string_4;
			txtX = "Amount";
			unitX = string_3;
			fmtX = "";
			fmtY = "0";
		}
		string_4 = unitY;
		if (cmpdFunc_0 == null)
		{
			disLg.lgXBeg = 0f;
			disLg.lgX = 100f;
			disLg.lgYBeg = 0f;
			disLg.lgY = 100f;
			Array.Resize(ref pointF_12, 0);
			Array.Resize(ref pointF_11, 0);
		}
		else
		{
			disLg = cmpdFunc_0.disLg;
			Array.Resize(ref pointF_12, compound_1.levels.Length);
			Array.Resize(ref pointF_11, compound_1.levels.Length);
			int num = 0;
			int newSize = 0;
			for (int i = 0; i < compound_1.levels.Length; i++)
			{
				if (bool_0)
				{
					if (compound_1.levels[i].iFuncPt.IsValid)
					{
						if (compound_1.levels[i].used)
						{
							pointF_12[num++] = compound_1.levels[i].iFuncPt.AsPointF;
						}
						else
						{
							pointF_11[newSize++] = compound_1.levels[i].iFuncPt.AsPointF;
						}
					}
				}
				else if (compound_1.levels[i].eFuncPt.IsValid)
				{
					if (compound_1.levels[i].used)
					{
						pointF_12[num++] = compound_1.levels[i].eFuncPt.AsPointF;
					}
					else
					{
						pointF_11[newSize++] = compound_1.levels[i].eFuncPt.AsPointF;
					}
				}
			}
			Array.Resize(ref pointF_12, num);
			Array.Resize(ref pointF_11, newSize);
			if (cmpdFunc_0.curveFit == CurveFit.Exponent)
			{
				for (int j = 0; j < num; j++)
				{
					pointF_12[j].X = Convert.ToSingle(Math.Log(pointF_12[j].X));
					pointF_12[j].Y = Convert.ToSingle(Math.Log(pointF_12[j].Y * 1000f));
				}
			}
		}
		stDisChain.ReplaceCurFrameLg(disLg);
	}
}
