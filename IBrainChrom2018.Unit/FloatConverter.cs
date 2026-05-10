using System;
using System.ComponentModel;
using System.Globalization;

namespace IBrainChrom2018.Unit;

public class FloatConverter : DoubleConverter
{
	private float m_fMin = float.MinValue;

	private float m_fMax = float.MaxValue;

	public FloatConverter()
	{
	}

	public FloatConverter(float min, float max)
	{
		m_fMin = min;
		m_fMax = max;
	}

	public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
	{
		int num = (int)base.ConvertFrom(context, culture, value);
		if ((float)num < m_fMin || (float)num > m_fMax)
		{
			throw new Exception("输入值要在1至32之间。");
		}
		return num;
	}
}
