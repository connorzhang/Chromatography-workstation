using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace IBrainChrom2018;

[Serializable]
[StructLayout(LayoutKind.Sequential)]
public class CmpdFunc
{
	private const double cubict = 1.0 / 3.0;

	private const double t2PI_3 = Math.PI * 2.0 / 3.0;

	private const double t4PI_3 = 4.1887902047863905;

	public bool IsFinishCalcuAmountF;

	public bool IsValideData;

	private double a;

	private double b;

	private double c;

	private double k;

	private double d;

	private double t2a;

	private double tb2;

	private double t4a;

	private double tb_3a;

	private double t54a3;

	private double t9abc;

	private double t2b3;

	private double p3;

	private double t27a2;

	public float freeRespFactor;

	public double corrFactor;

	public double[] disCoefs;

	public CurveFit curveFit = CurveFit.Linear;

	public DisLg disLg = default(DisLg);

	public Original original = Original.With;

	public double residuum;

	public WeightsType weightsType;

	public FuncPt[] funcPts;

	public int FuncPtsNum
	{
		get
		{
			return funcPts.Length;
		}
		set
		{
			Array.Resize(ref funcPts, value);
		}
	}

	private void method_0()
	{
		FuncPtsNum++;
		for (int num = FuncPtsNum - 1; num > 0; num--)
		{
			funcPts[num] = funcPts[num - 1];
		}
		funcPts[0].responseF = (funcPts[0].amountF = 0f);
	}

	public bool Adjust_funcPts()
	{
		switch (curveFit)
		{
		case CurveFit.PtToPt:
		{
			int num = 0;
			for (int i = 0; i < FuncPtsNum; i++)
			{
				if (funcPts[i].amountF <= 0f || funcPts[i].responseF <= 0f)
				{
					for (int j = i; j < FuncPtsNum - 1; j++)
					{
						funcPts[j] = funcPts[j + 1];
					}
					num++;
				}
			}
			if (num != 0)
			{
				Array.Resize(ref funcPts, FuncPtsNum - num);
			}
			num = 0;
			for (int k = 0; k < FuncPtsNum; k++)
			{
				for (int l = k + 1; l < FuncPtsNum; l++)
				{
					if (funcPts[l].amountF == funcPts[k].amountF || funcPts[l].responseF == funcPts[k].responseF)
					{
						for (int m = l; m < FuncPtsNum - 1; m++)
						{
							funcPts[m] = funcPts[m + 1];
						}
						num++;
					}
				}
			}
			if (num != 0)
			{
				num = Math.Min(FuncPtsNum, num);
				Array.Resize(ref funcPts, FuncPtsNum - num);
			}
			for (int n = 0; n < FuncPtsNum; n++)
			{
				for (int num2 = n + 1; num2 < FuncPtsNum; num2++)
				{
					if (funcPts[num2].amountF == funcPts[n].amountF)
					{
						MessageBox.Show("排序点异常");
						IsValideData = false;
						return false;
					}
					if (funcPts[num2].responseF < funcPts[n].responseF)
					{
						FuncPt funcPt = funcPts[n];
						funcPts[n] = funcPts[num2];
						funcPts[num2] = funcPt;
					}
				}
			}
			if (FuncPtsNum == 0)
			{
				IsValideData = false;
				return false;
			}
			if (original != Original.Ignore)
			{
				method_0();
			}
			break;
		}
		default:
			if ((FuncPtsNum != 0 && original == Original.With) || original == Original.Pass)
			{
				if (original == Original.Pass && FuncPtsNum == 1)
				{
					method_0();
				}
				else if (original == Original.With)
				{
					method_0();
				}
			}
			break;
		case CurveFit.Free:
			break;
		}
		return true;
	}

	public float[] Calcu_amountF(float responseF)
	{
		if (!IsValideData)
		{
			return new float[0];
		}
		double value = -1.0;
		if (!IsFinishCalcuAmountF)
		{
			IsFinishCalcuAmountF = true;
			switch (curveFit)
			{
			case CurveFit.Free:
			case CurveFit.PtToPt:
				this.k = disCoefs[0];
				break;
			case CurveFit.Linear:
				if (original != Original.Pass)
				{
					this.k = disCoefs[1];
					b = disCoefs[0];
				}
				else
				{
					this.k = disCoefs[0];
					b = 0.0;
				}
				break;
			case CurveFit.Quadratic:
				if (original == Original.Pass)
				{
					a = disCoefs[1];
					b = disCoefs[0];
				}
				else
				{
					a = disCoefs[2];
					b = disCoefs[1];
				}
				t2a = 2.0 * a;
				tb2 = b * b;
				t4a = 4.0 * a;
				break;
			case CurveFit.Cubic:
			{
				if (original == Original.Pass)
				{
					a = disCoefs[2];
					b = disCoefs[1];
					c = disCoefs[0];
				}
				else
				{
					a = disCoefs[3];
					b = disCoefs[2];
					c = disCoefs[1];
				}
				tb_3a = b / (3.0 * a);
				double num = a * a;
				t27a2 = 27.0 * num;
				t9abc = 9.0 * a * b * c;
				t2b3 = 2.0 * Math.Pow(b, 3.0);
				t54a3 = 54.0 * num * a;
				double x = (3.0 * a * c - b * b) / (9.0 * num);
				p3 = Math.Pow(x, 3.0);
				break;
			}
			case CurveFit.Exponent:
				if (original != Original.Pass)
				{
					this.k = disCoefs[1];
					b = disCoefs[0];
				}
				else
				{
					this.k = disCoefs[0];
					b = 0.0;
				}
				break;
			}
		}
		float[] array = new float[0];
		switch (curveFit)
		{
		case CurveFit.Free:
			if (this.k != 0.0)
			{
				value = (double)responseF / this.k;
			}
			break;
		case CurveFit.PtToPt:
		{
			bool flag = false;
			int num2 = 0;
			float num3 = 1E+09f;
			for (int j = 1; j < FuncPtsNum; j++)
			{
				float num4 = Math.Abs(funcPts[j].responseF - responseF);
				if (num4 < num3)
				{
					num3 = num4;
					num2 = j;
				}
			}
			if (num2 >= 0)
			{
				this.k = (funcPts[num2].amountF - funcPts[num2 - 1].amountF) / (funcPts[num2].responseF - funcPts[num2 - 1].responseF);
				value = (double)funcPts[num2].amountF + this.k * (double)(responseF - funcPts[num2].responseF);
				if (1 == 0)
				{
					value = (double)funcPts[FuncPtsNum - 1].amountF + this.k * (double)(responseF - funcPts[FuncPtsNum - 1].responseF);
				}
			}
			break;
		}
		case CurveFit.Linear:
			if (this.k != 0.0)
			{
				value = ((double)responseF - b) / this.k;
			}
			break;
		case CurveFit.Quadratic:
		{
			if (disLg.lgYBeg <= responseF)
			{
				bool flag2 = responseF <= disLg.LgYEnd;
			}
			if (original != Original.Pass)
			{
				c = disCoefs[0] - (double)responseF;
			}
			else
			{
				c = 0.0 - (double)responseF;
			}
			double num5 = tb2 - t4a * c;
			if (num5 > 0.0)
			{
				float[] array3 = new float[2];
				double num6 = Math.Sqrt(num5);
				array3[0] = Convert.ToSingle((0.0 - b + num6) / t2a);
				array3[1] = Convert.ToSingle((0.0 - b - num6) / t2a);
				for (int k = 0; k < array3.Length; k++)
				{
					if (disLg.lgXBeg <= array3[k])
					{
						bool flag3 = array3[k] <= disLg.LgXEnd;
					}
					Array.Resize(ref array, array.Length + 1);
					array[array.Length - 1] = array3[k];
				}
				return array;
			}
			if (num5 != 0.0)
			{
				return array;
			}
			value = (0.0 - b) / t2a;
			break;
		}
		case CurveFit.Cubic:
		{
			if (!(disLg.lgYBeg <= responseF) || !(responseF <= disLg.LgYEnd))
			{
				break;
			}
			if (original != Original.Pass)
			{
				d = disCoefs[0] - (double)responseF;
			}
			else
			{
				d = 0.0 - (double)responseF;
			}
			float[] array2 = method_1(d);
			Array.Resize(ref array, 0);
			for (int i = 0; i < array2.Length; i++)
			{
				if (disLg.lgXBeg <= array2[i] && array2[i] <= disLg.LgXEnd)
				{
					Array.Resize(ref array, array.Length + 1);
					array[array.Length - 1] = array2[i];
				}
			}
			return array;
		}
		case CurveFit.Exponent:
			if (this.k != 0.0)
			{
				value = (double)responseF * this.k + b;
			}
			break;
		}
		return new float[1] { Convert.ToSingle(value) };
	}

	public void Calcu_coefs()
	{
		int num = 1;
		switch (curveFit)
		{
		case CurveFit.Free:
			IsValideData = freeRespFactor != 0f;
			break;
		case CurveFit.PtToPt:
			IsValideData = FuncPtsNum >= 2;
			break;
		case CurveFit.Linear:
			IsValideData = FuncPtsNum >= 2;
			break;
		case CurveFit.Quadratic:
			IsValideData = FuncPtsNum >= 1;
			num = 2;
			break;
		case CurveFit.Cubic:
			IsValideData = FuncPtsNum >= 1;
			num = 3;
			break;
		case CurveFit.Exponent:
			IsValideData = FuncPtsNum >= 1;
			break;
		}
		if (IsValideData)
		{
			switch (curveFit)
			{
			case CurveFit.Free:
			case CurveFit.PtToPt:
				Array.Resize(ref disCoefs, 1);
				disCoefs[0] = 1f / freeRespFactor;
				break;
			case CurveFit.Linear:
			case CurveFit.Quadratic:
			case CurveFit.Cubic:
				if (original == Original.Pass)
				{
					double[] array3 = new double[FuncPtsNum];
					double[] array4 = new double[FuncPtsNum];
					Array.Resize(ref disCoefs, 2);
					for (int j = 0; j < FuncPtsNum; j++)
					{
						array4[j] = funcPts[j].responseF;
						array3[j] = funcPts[j].amountF;
					}
					corrFactor = Program.linearFitPass(array3, array4, FuncPtsNum, disCoefs);
				}
				else
				{
					double[] array5 = new double[FuncPtsNum];
					double[] array6 = new double[FuncPtsNum];
					Array.Resize(ref disCoefs, 2);
					for (int k = 0; k < FuncPtsNum; k++)
					{
						array6[k] = funcPts[k].responseF;
						array5[k] = funcPts[k].amountF;
					}
					corrFactor = Program.linearFit(array5, array6, FuncPtsNum, disCoefs);
				}
				break;
			case CurveFit.Exponent:
			{
				double[] array = new double[FuncPtsNum];
				double[] array2 = new double[FuncPtsNum];
				Array.Resize(ref disCoefs, 2);
				for (int i = 0; i < FuncPtsNum; i++)
				{
					array[i] = Math.Log(funcPts[i].responseF * 1000f);
					array2[i] = Math.Log(funcPts[i].amountF);
				}
				if (original == Original.Pass)
				{
					Array.Resize(ref array, FuncPtsNum + 1);
					Array.Resize(ref array2, FuncPtsNum + 1);
					array[FuncPtsNum] = 0.0;
					array2[FuncPtsNum] = 0.0;
					corrFactor = Program.linearFit(array, array2, FuncPtsNum + 1, disCoefs);
				}
				else
				{
					corrFactor = Program.linearFit(array, array2, FuncPtsNum, disCoefs);
				}
				break;
			}
			}
			for (int l = 0; l < disCoefs.Length; l++)
			{
				if (double.IsNaN(disCoefs[l]))
				{
					IsValideData = false;
					IsFinishCalcuAmountF = false;
				}
			}
		}
		if (!IsValideData)
		{
			return;
		}
		double num2 = 0.0;
		for (int m = 0; m < FuncPtsNum; m++)
		{
			num2 += (double)funcPts[m].amountF;
		}
		num2 /= (double)FuncPtsNum;
		double num3 = 0.0;
		for (int n = 0; n < FuncPtsNum; n++)
		{
			num3 += (double)funcPts[n].responseF;
		}
		num3 /= (double)FuncPtsNum;
		switch (curveFit)
		{
		case CurveFit.Free:
		case CurveFit.Quadratic:
		case CurveFit.Cubic:
			try
			{
				double num9 = 0.0;
				for (int num10 = 0; num10 < FuncPtsNum; num10++)
				{
					num9 += Math.Pow((double)funcPts[num10].responseF - num3, 2.0);
				}
				double num11 = 0.0;
				for (int num12 = 0; num12 < FuncPtsNum; num12++)
				{
					double num13 = CalcuValue(funcPts[num12].amountF);
					num11 += Math.Pow((double)funcPts[num12].responseF - num13, 2.0);
				}
				corrFactor = ((num9 > 0.0) ? ((num9 - num11) / num9) : double.NaN);
			}
			catch
			{
			}
			break;
		case CurveFit.PtToPt:
			corrFactor = 1.0;
			break;
		case CurveFit.Linear:
		case CurveFit.Exponent:
			if (curveFit != CurveFit.Exponent && original == Original.Pass)
			{
				double num4 = 0.0;
				for (int num5 = 0; num5 < FuncPtsNum; num5++)
				{
					num4 += ((double)funcPts[num5].amountF - num2) * ((double)funcPts[num5].responseF - num3);
				}
				double num6 = 0.0;
				double num7 = 0.0;
				for (int num8 = 0; num8 < FuncPtsNum; num8++)
				{
					num6 += Math.Pow((double)funcPts[num8].amountF - num2, 2.0);
					num7 += Math.Pow((double)funcPts[num8].responseF - num3, 2.0);
				}
				corrFactor = num4 / Math.Sqrt(num6 * num7);
			}
			break;
		}
		residuum = 0.0;
		for (int num14 = 0; num14 < FuncPtsNum; num14++)
		{
			double double_ = funcPts[num14].amountF;
			double num15 = ((curveFit != CurveFit.PtToPt) ? ((double)CalcuValue(double_)) : ((double)funcPts[num14].responseF));
			double x = Math.Abs(num15 - (double)funcPts[num14].responseF);
			residuum += Math.Pow(x, 2.0);
		}
		residuum = Math.Sqrt(residuum / (double)FuncPtsNum);
	}

	public float CalcuValue(double double_17)
	{
		double value = -1.0;
		switch (curveFit)
		{
		case CurveFit.Free:
		{
			double num7 = disCoefs[0];
			value = num7 * double_17;
			break;
		}
		case CurveFit.Linear:
		{
			double num5;
			double num6;
			if (original != Original.Pass)
			{
				num5 = disCoefs[1];
				num6 = disCoefs[0];
			}
			else
			{
				num5 = disCoefs[0];
				num6 = 0.0;
			}
			value = num5 * double_17 + num6;
			break;
		}
		case CurveFit.Quadratic:
			try
			{
				double num8;
				double num9;
				double num10;
				if (original == Original.Pass)
				{
					num8 = disCoefs[1];
					num9 = disCoefs[0];
					num10 = 0.0;
				}
				else
				{
					num8 = disCoefs[2];
					num9 = disCoefs[1];
					num10 = disCoefs[0];
				}
				value = num8 * Math.Pow(double_17, 2.0) + num9 * double_17 + num10;
			}
			catch
			{
			}
			break;
		case CurveFit.Cubic:
		{
			double num;
			double num2;
			double num3;
			double num4;
			if (original == Original.Pass)
			{
				num = disCoefs[2];
				num2 = disCoefs[1];
				num3 = disCoefs[0];
				num4 = 0.0;
			}
			else
			{
				num = disCoefs[3];
				num2 = disCoefs[2];
				num3 = disCoefs[1];
				num4 = disCoefs[0];
			}
			value = num * Math.Pow(double_17, 3.0) + num2 * Math.Pow(double_17, 2.0) + num3 * double_17 + num4;
			break;
		}
		}
		return Convert.ToSingle(value);
	}

	private float[] method_1(double double_17)
	{
		double num = (t27a2 * double_17 - t9abc + t2b3) / t54a3;
		double num2 = num * num;
		double num3 = num2 + p3;
		float[] array = new float[0];
		if (num3 >= 0.0)
		{
			double num4 = Math.Sqrt(num3);
			double num5 = 0.0 - num + num4;
			int num6;
			if (num5 >= 0.0)
			{
				num6 = 1;
			}
			else
			{
				num6 = -1;
				num5 = 0.0 - num5;
			}
			num5 = (double)num6 * Math.Pow(num5, 1.0 / 3.0);
			double num7 = 0.0 - num - num4;
			if (num7 >= 0.0)
			{
				num6 = 1;
			}
			else
			{
				num6 = -1;
				num7 = 0.0 - num7;
			}
			num7 = (double)num6 * Math.Pow(num7, 1.0 / 3.0);
			Array.Resize(ref array, 1);
			array[0] = Convert.ToSingle(0.0 - tb_3a + num5 + num7);
			if (num3 == 0.0)
			{
				Array.Resize(ref array, 2);
				array[1] = Convert.ToSingle(0.0 - tb_3a - num5);
			}
			return array;
		}
		Array.Resize(ref array, 3);
		num3 = 0.0 - num3;
		double num8 = Math.Sqrt(num2 + num3);
		double num9 = Math.Acos((0.0 - num) / num8) / 3.0;
		double num10 = 2.0 * Math.Pow(num8, 1.0 / 3.0);
		array[0] = Convert.ToSingle(0.0 - tb_3a + num10 * Math.Cos(num9));
		array[1] = Convert.ToSingle(0.0 - tb_3a + num10 * Math.Cos(Math.PI * 2.0 / 3.0 + num9));
		array[2] = Convert.ToSingle(0.0 - tb_3a + num10 * Math.Cos(4.1887902047863905 + num9));
		return array;
	}

	public string GetCorrFactorTxt()
	{
		if (!double.IsNaN(corrFactor))
		{
			return corrFactor.ToString("0.0000000");
		}
		return "-";
	}

	public string GetEquationStr()
	{
		if (!IsValideData)
		{
			return "Y=";
		}
		string text = "";
		switch (curveFit)
		{
		case CurveFit.Free:
		case CurveFit.PtToPt:
			text = sfp(0, 1);
			break;
		case CurveFit.Linear:
			text = ((original == Original.Pass) ? sfp(0, 1) : (sfp(1, 1) + sfp(0, 0)));
			break;
		case CurveFit.Quadratic:
			text = ((original != Original.Pass) ? (sfp(2, 2) + sfp(1, 1) + sfp(0, 0)) : (sfp(1, 2) + sfp(0, 1)));
			break;
		case CurveFit.Cubic:
			text = ((original != Original.Pass) ? (sfp(3, 3) + sfp(2, 2) + sfp(1, 1) + sfp(0, 0)) : (sfp(2, 3) + sfp(1, 2) + sfp(0, 1)));
			break;
		case CurveFit.Exponent:
			k = disCoefs[1];
			b = disCoefs[0];
			text = string.Concat(new string[3]
			{
				k.ToString("F" + Class49.int_8),
				"lnX + ",
				b.ToString("F" + Class49.int_8)
			});
			break;
		}
		if (text.StartsWith(" +"))
		{
			text = text.Remove(0, 2);
		}
		if (curveFit == CurveFit.Exponent)
		{
			return "lnY= " + text;
		}
		return "Y= " + text;
	}

	public string GetResiduumTxt(string unit)
	{
		if (!double.IsNaN(residuum))
		{
			string text = residuum.ToString("0.00000");
			if (unit != "")
			{
				text = text + " [" + unit + "]";
			}
			return text;
		}
		return "-";
	}

	private string sfp(int int_0, int int_1)
	{
		string text = "F05";
		double num = Math.Abs(disCoefs[int_0]);
		if (num < 0.009999999776482582 || num > 100.0)
		{
			text = "0.00000e0";
		}
		string text2 = int_1 switch
		{
			0 => disCoefs[int_0].ToString(text), 
			1 => disCoefs[int_0].ToString(text) + "X", 
			_ => disCoefs[int_0].ToString(text) + "X^" + int_1, 
		};
		if (disCoefs[int_0] < 0.0)
		{
			return " " + text2;
		}
		if (disCoefs[int_0].Equals(0.0))
		{
			return "";
		}
		return " +" + text2;
	}

	private static double smethod_0(double[,] double_17)
	{
		int length = double_17.GetLength(0);
		if (length < 2)
		{
			return double_17[0, 0];
		}
		if (length == 2)
		{
			return double_17[0, 0] * double_17[1, 1] - double_17[0, 1] * double_17[1, 0];
		}
		int num = 0;
		double num2 = 0.0;
		for (int i = 0; i < length; i++)
		{
			num2 = (((num + i) % 2 != 0) ? (num2 - double_17[num, i] * smethod_0(smethod_1(double_17, num, i))) : (num2 + double_17[num, i] * smethod_0(smethod_1(double_17, num, i))));
		}
		return num2;
	}

	private static double[,] smethod_1(double[,] double_17, int int_0, int int_1)
	{
		int length = double_17.GetLength(0);
		double[,] array = new double[length - 1, length - 1];
		byte b = 0;
		byte b2 = 0;
		for (int i = 0; i < length; i++)
		{
			if (i == int_0)
			{
				continue;
			}
			for (int j = 0; j < length; j++)
			{
				if (j != int_1)
				{
					b2++;
					if (b2 < length - 1)
					{
						array[b, b2] = double_17[i, j];
					}
				}
			}
			b++;
			b2 = 0;
		}
		return array;
	}

	private static FuncPt[] PrepareForExponent(FuncPt[] funcPt_0)
	{
		for (int i = 0; i < funcPt_0.Length; i++)
		{
			if (funcPt_0[i].amountF * funcPt_0[i].responseF != 0f)
			{
				float responseF = (float)Math.Log(funcPt_0[i].amountF);
				float amountF = (float)Math.Log(funcPt_0[i].responseF);
				funcPt_0[i].amountF = amountF;
				funcPt_0[i].responseF = responseF;
			}
		}
		return funcPt_0;
	}

	private static double[] CalcDisCoefs_With(FuncPt[] funcPt_0, int int_0)
	{
		if (1 <= int_0 && int_0 <= 4)
		{
			int num = int_0 + 1;
			double[] array = new double[num];
			double[] array2 = new double[num];
			int num2 = int_0 + int_0 + 1;
			double[] array3 = new double[num2];
			for (int i = 0; i < num2; i++)
			{
				array3[i] = smethod_6(funcPt_0, i);
			}
			double[,] array4 = new double[num, num];
			for (int j = 0; j <= int_0; j++)
			{
				for (int k = 0; k <= int_0; k++)
				{
					array4[j, k] = array3[j + k];
				}
			}
			for (int l = 0; l < num; l++)
			{
				array2[l] = smethod_5(ref funcPt_0, l);
			}
			double num3 = smethod_0(array4);
			double[,] array5 = new double[num, num];
			for (int m = 0; m < num; m++)
			{
				for (int n = 0; n < num; n++)
				{
					for (int num4 = 0; num4 < num; num4++)
					{
						array5[n, num4] = array4[n, num4];
					}
				}
				for (int num5 = 0; num5 < num; num5++)
				{
					array5[num5, m] = array2[num5];
				}
				array[m] = smethod_0(array5) / num3;
			}
			return array;
		}
		throw new Exception("期望1-4阶");
	}

	private static double[] CalcDisCoefs_Pass(FuncPt[] funcPt_0, int int_0)
	{
		if (1 <= int_0 && int_0 <= 4)
		{
			double[] array = new double[int_0];
			double[] array2 = new double[int_0];
			int num = int_0 + int_0 - 1;
			double[] array3 = new double[num];
			for (int i = 0; i < num; i++)
			{
				array3[i] = smethod_6(funcPt_0, i + 1);
			}
			double[,] array4 = new double[int_0, int_0];
			for (int j = 0; j < int_0; j++)
			{
				for (int k = 0; k < int_0; k++)
				{
					array4[j, k] = array3[j + k];
				}
			}
			for (int l = 0; l < int_0; l++)
			{
				array2[l] = smethod_5(ref funcPt_0, l);
			}
			double num2 = smethod_0(array4);
			double[,] array5 = new double[int_0, int_0];
			for (int m = 0; m < int_0; m++)
			{
				for (int n = 0; n < int_0; n++)
				{
					for (int num3 = 0; num3 < int_0; num3++)
					{
						array5[n, num3] = array4[n, num3];
					}
				}
				for (int num4 = 0; num4 < int_0; num4++)
				{
					array5[num4, m] = array2[num4];
				}
				array[m] = smethod_0(array5) / num2;
			}
			return array;
		}
		throw new Exception("期望1-4阶");
	}

	private static double smethod_5(ref FuncPt[] funcPt_0, int n)
	{
		double num = 0.0;
		for (int i = 0; i < funcPt_0.Length; i++)
		{
			num += (double)funcPt_0[i].responseF * Math.Pow(funcPt_0[i].amountF, n);
		}
		return num;
	}

	private static double smethod_6(FuncPt[] funcPt_0, int n)
	{
		if (n == 0)
		{
			return funcPt_0.Length;
		}
		if (n <= 0)
		{
			throw new Exception("阶数错误");
		}
		double num = 0.0;
		for (int i = 0; i < funcPt_0.Length; i++)
		{
			num += Math.Pow(funcPt_0[i].amountF, n);
		}
		return num;
	}

	public CmpdFunc Copy()
	{
		CmpdFunc cmpdFunc = new CmpdFunc();
		cmpdFunc.IsFinishCalcuAmountF = IsFinishCalcuAmountF;
		cmpdFunc.IsValideData = IsValideData;
		cmpdFunc.a = a;
		cmpdFunc.b = b;
		cmpdFunc.c = c;
		cmpdFunc.d = d;
		cmpdFunc.k = k;
		cmpdFunc.p3 = p3;
		cmpdFunc.t27a2 = t27a2;
		cmpdFunc.t2a = t2a;
		cmpdFunc.t2b3 = t2b3;
		cmpdFunc.t4a = t4a;
		cmpdFunc.t54a3 = t54a3;
		cmpdFunc.t9abc = t9abc;
		cmpdFunc.tb_3a = tb_3a;
		cmpdFunc.tb2 = tb2;
		cmpdFunc.corrFactor = corrFactor;
		cmpdFunc.curveFit = curveFit;
		cmpdFunc.disLg = disLg;
		cmpdFunc.freeRespFactor = freeRespFactor;
		cmpdFunc.original = original;
		cmpdFunc.residuum = residuum;
		cmpdFunc.weightsType = weightsType;
		if (disCoefs != null)
		{
			cmpdFunc.disCoefs = new double[disCoefs.Length];
			for (int i = 0; i < disCoefs.Length; i++)
			{
				cmpdFunc.disCoefs[i] = disCoefs[i];
			}
		}
		if (funcPts != null)
		{
			cmpdFunc.funcPts = new FuncPt[funcPts.Length];
			for (int j = 0; j < funcPts.Length; j++)
			{
				cmpdFunc.funcPts[j] = funcPts[j];
			}
		}
		return cmpdFunc;
	}

	public void LoadFromObject(CmpdFunc cmpdFunc)
	{
		freeRespFactor = cmpdFunc.freeRespFactor;
		curveFit = cmpdFunc.curveFit;
		original = cmpdFunc.original;
		weightsType = cmpdFunc.weightsType;
		FuncPtsNum = cmpdFunc.FuncPtsNum;
		for (int i = 0; i < FuncPtsNum; i++)
		{
			funcPts[i] = cmpdFunc.funcPts[i];
		}
		IsValideData = cmpdFunc.IsValideData;
		if (IsValideData)
		{
			Array.Resize(ref disCoefs, cmpdFunc.disCoefs.Length);
			for (int j = 0; j < disCoefs.Length; j++)
			{
				disCoefs[j] = cmpdFunc.disCoefs[j];
			}
		}
		IsFinishCalcuAmountF = false;
	}

	public void LoadFromFile(BinaryReader binaryReader_0)
	{
		curveFit = (CurveFit)binaryReader_0.ReadByte();
		original = (Original)binaryReader_0.ReadByte();
		weightsType = (WeightsType)binaryReader_0.ReadByte();
		IsValideData = binaryReader_0.ReadBoolean();
		if (IsValideData)
		{
			FuncPtsNum = binaryReader_0.ReadInt32();
			for (int i = 0; i < FuncPtsNum; i++)
			{
				funcPts[i].LoadFromFile(binaryReader_0);
			}
			Array.Resize(ref disCoefs, binaryReader_0.ReadInt32());
			for (int j = 0; j < disCoefs.Length; j++)
			{
				disCoefs[j] = binaryReader_0.ReadDouble();
			}
		}
		IsFinishCalcuAmountF = false;
	}

	public void SaveToFile(BinaryWriter binaryWriter_0)
	{
		binaryWriter_0.Write((byte)curveFit);
		binaryWriter_0.Write((byte)original);
		binaryWriter_0.Write((byte)weightsType);
		binaryWriter_0.Write(IsValideData);
		if (IsValideData)
		{
			binaryWriter_0.Write(FuncPtsNum);
			for (int i = 0; i < FuncPtsNum; i++)
			{
				funcPts[i].SaveToFile(binaryWriter_0);
			}
			binaryWriter_0.Write(disCoefs.Length);
			for (int j = 0; j < disCoefs.Length; j++)
			{
				binaryWriter_0.Write(disCoefs[j]);
			}
		}
	}
}
