using System;
using System.ComponentModel;
using System.Globalization;

namespace IBrainChrom2018.Unit;

public class IntConverter : Int32Converter
{
	private int m_fMin = int.MinValue;

	private int m_fMax = int.MaxValue;

	public IntConverter()
	{
	}

	public IntConverter(int min, int max)
	{
		m_fMin = min;
		m_fMax = max;
	}

	public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
	{
		int num = (int)base.ConvertFrom(context, culture, value);
		if (num < m_fMin || num > m_fMax)
		{
			throw new Exception("输入值要在1至32之间。");
		}
		return num;
	}
}
