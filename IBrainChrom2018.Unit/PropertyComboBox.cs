using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;

namespace IBrainChrom2018.Unit;

public abstract class PropertyComboBox : TypeConverter
{
	public List<string> _hash = null;

	public PropertyComboBox()
	{
		_hash = new List<string>();
		GetConvertHash();
	}

	public abstract void GetConvertHash();

	public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
	{
		return true;
	}

	public override StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
	{
		int[] array = new int[_hash.Count];
		for (int i = 0; i < _hash.Count; i++)
		{
			array[i] = i;
		}
		return new StandardValuesCollection(array);
	}

	public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
	{
		if (sourceType == typeof(string))
		{
			return true;
		}
		return base.CanConvertFrom(context, sourceType);
	}

	public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object v)
	{
		if (v is string)
		{
			for (int i = 0; i < _hash.Count; i++)
			{
				string text = _hash[i];
				if (text == v.ToString())
				{
					return i;
				}
			}
			return 0;
		}
		return base.ConvertFrom(context, culture, v);
	}

	public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object v, Type destinationType)
	{
		if (destinationType == typeof(string))
		{
			int num = int.Parse(v.ToString());
			if (_hash.Count > num)
			{
				return _hash[int.Parse(v.ToString())];
			}
			return _hash[_hash.Count - 1];
		}
		return base.ConvertTo(context, culture, v, destinationType);
	}

	public override bool GetStandardValuesExclusive(ITypeDescriptorContext context)
	{
		return true;
	}
}
