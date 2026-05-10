using System;
using System.Collections;
using System.ComponentModel;
using System.Globalization;

namespace IBrainChrom2018.Unit;

public class BoolComboBoxItem : TypeConverter
{
	public Hashtable _hash = null;

	public BoolComboBoxItem()
	{
		_hash = new Hashtable();
		_hash.Add(true, Lang.PS("是", "True"));
		_hash.Add(false, Lang.PS("否", "False"));
	}

	public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
	{
		return true;
	}

	public override StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
	{
		bool[] array = new bool[_hash.Values.Count];
		int num = 0;
		foreach (DictionaryEntry item in _hash)
		{
			array[num++] = (bool)item.Key;
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
			foreach (DictionaryEntry item in _hash)
			{
				if (item.Value.Equals(v.ToString()))
				{
					return item.Key;
				}
			}
		}
		return base.ConvertFrom(context, culture, v);
	}

	public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object v, Type destinationType)
	{
		if (destinationType == typeof(string))
		{
			foreach (DictionaryEntry item in _hash)
			{
				if (item.Key.Equals(v))
				{
					return item.Value.ToString();
				}
			}
			return "";
		}
		return base.ConvertTo(context, culture, v, destinationType);
	}

	public override bool GetStandardValuesExclusive(ITypeDescriptorContext context)
	{
		return true;
	}
}
