using System;
using IBrainChrom2018.Unit;

namespace IBrainChrom2018;

public class DetectorParse
{
	public byte byte_0;

	public bool bool_0;

	public byte byte_1;

	public byte byte_2;

	private double[] double_0;

	public float[] float_0;

	public string string_0 = "mV";

	private FormMainParam formmainParam = FormMainParam.Create();

	public DetectorParse(int int_0)
	{
	}

	public bool method_0(byte[] byte_3, ref int int_0)
	{
		return true;
	}

	public double VoltageLogarithmicTransformation(double ValueIn)
	{
		double result = 0.0;
		try
		{
			double num = 340000.0;
			double num2 = 1000000.0;
			double num3 = 0.00010416666666666667;
			double double_ = 0.56;
			double double_2 = 0.0;
			double num4 = smethod_1(num3, double_, double_2);
			double num5 = (ValueIn - num4 * num) / num * ((ValueIn - num4 * num) / num);
			double num6 = num5 / num3;
			result = num6 * num2;
			return result;
		}
		catch
		{
			return result;
		}
	}

	private double smethod_1(double double_0, double double_1, double double_2)
	{
		return double_0 / (Math.Exp(double_1 / double_2) - 1.0);
	}

	public bool ParseData(byte[] byte_3, ref int int_0, float shuaijian1, float shuaijian2, float shuaijian3)
	{
		DetectorParam detectorParam = DetectorParam.Create();
		bool result = false;
		byte_2 = byte_3[int_0++];
		bool_0 = byte_3[int_0++] == 0;
		byte_1 = byte_3[int_0++];
		byte_0 = byte_3[int_0++];
		int num = byte_0 * 10;
		int num2 = 0;
		Array.Resize(ref double_0, num);
		for (int i = 0; i < num; i++)
		{
			if (byte_3.Length > int_0 + 4)
			{
				byte b = byte_3[int_0++];
				byte b2 = byte_3[int_0++];
				byte b3 = byte_3[int_0++];
				byte b4 = byte_3[int_0++];
				if (byte_1 == 250)
				{
					b = Program.TestFoumularEncrypt(num2++, b);
					b2 = Program.TestFoumularEncrypt(num2++, b2);
					b3 = Program.TestFoumularEncrypt(num2++, b3);
					b4 = Program.TestFoumularEncrypt(num2++, b4);
				}
				bool flag = IBrainConvert.Byte2Bool(b, 4);
				byte b5 = (byte)(b & 0xF);
				byte b6 = (byte)((b2 & 0xF0) >> 4);
				byte b7 = (byte)(b2 & 0xF);
				byte b8 = (byte)((b3 & 0xF0) >> 4);
				byte b9 = (byte)(b3 & 0xF);
				byte b10 = (byte)((b4 & 0xF0) >> 4);
				byte b11 = (byte)(b4 & 0xF);
				double_0[i] = (float)(int)b5 + (float)(int)b6 * 0.1f + (float)(int)b7 * 0.01f + (float)(int)b8 * 0.001f + (float)(int)b9 * 0.0001f + (float)(int)b10 * 1E-05f + (float)(int)b11 * 1E-06f;
				if (byte_1 == 250)
				{
					double num3 = double_0[i];
					double_0[i] = num3;
				}
				else
				{
					try
					{
						double num4 = double_0[i];
						switch (byte_2)
						{
						case 64:
							double_0[i] *= 1000.0;
							double_0[i] *= double_0[i];
							double_0[i] /= 1500.0;
							num4 = double_0[i];
							break;
						case 65:
							double_0[i] *= 1000.0;
							double_0[i] *= double_0[i];
							double_0[i] /= 1500.0;
							num4 = double_0[i];
							break;
						case 66:
							double_0[i] *= 1000.0;
							double_0[i] *= double_0[i];
							double_0[i] /= 1500.0;
							num4 = double_0[i];
							break;
						case 67:
							double_0[i] *= 1000.0;
							double_0[i] *= double_0[i];
							double_0[i] /= 1500.0;
							num4 = double_0[i];
							break;
						case 160:
							double_0[i] *= 1000.0;
							double_0[i] *= double_0[i];
							double_0[i] /= 1500.0;
							num4 = double_0[i];
							break;
						case 161:
							double_0[i] *= 1000.0;
							double_0[i] *= double_0[i];
							double_0[i] /= 1500.0;
							num4 = double_0[i];
							break;
						case 162:
							double_0[i] *= 1000.0;
							double_0[i] *= double_0[i];
							double_0[i] /= 1500.0;
							num4 = double_0[i];
							break;
						case 163:
							double_0[i] *= 1000.0;
							double_0[i] *= double_0[i];
							double_0[i] /= 1500.0;
							num4 = double_0[i];
							break;
						}
						double_0[i] = num4;
					}
					catch (Exception)
					{
					}
				}
				if (byte_2 == 64)
				{
					double_0[i] /= Math.Max(formmainParam.fShuaijian, 1f);
				}
				else if (byte_2 == 65)
				{
					double_0[i] /= Math.Max(formmainParam.fShuaijian2, 1f);
				}
				else if (byte_2 == 112)
				{
					double_0[i] /= Math.Max(formmainParam.fShuaijian3, 1f);
				}
				else if (byte_2 == 160)
				{
					double_0[i] /= Math.Max(formmainParam.fShuaijian, 1f);
				}
				else if (byte_2 == 161)
				{
					double_0[i] /= Math.Max(formmainParam.fShuaijian2, 1f);
				}
				else if (byte_2 != 96)
				{
				}
				if (flag)
				{
					double_0[i] = 0.0 - double_0[i];
				}
			}
			else
			{
				double_0[i] = double_0[i - 1];
				result = true;
			}
		}
		Array.Resize(ref float_0, double_0.Length);
		for (int i = 0; i < float_0.Length; i++)
		{
			float_0[i] = Convert.ToSingle(double_0[i] * 1000.0);
		}
		return result;
	}

	public override string ToString()
	{
		string text = DetectorSettingRow.GetDeviceTypeNameByIdx(byte_2, formmainParam.iDetector) + ", 极性:" + (bool_0 ? "正" : "负") + ", 量程:0x" + BitConverter.ToString(new byte[1] { byte_1 }) + ", 频率:" + BitConverter.ToString(new byte[1] { byte_0 }) + ", 伏特:";
		for (int i = 0; i < float_0.Length; i++)
		{
			text = text + float_0[i] + ((i < float_0.Length - 1) ? ", " : "");
		}
		return text;
	}
}
