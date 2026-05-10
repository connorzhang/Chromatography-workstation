using System;

namespace IBrainChrom2018;

public class LclgvRespStyleCell : LclgvComboBoxCell
{
	public class LclgvCpmdNameStyleCell : LclgvComboBoxCell
	{
	}

	public override Type EditType => typeof(LclgvRespStyleEditingControl);

	public override Type ValueType => typeof(RespStyle);

	protected override string cellString(object value)
	{
		if (value == null)
		{
			return "";
		}
		return LclgvRespStyleEditingControl.ShowString((RespStyle)value);
	}
}
