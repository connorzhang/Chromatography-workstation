using System;

namespace IBrainChrom2018;

public class LclgvItgOprtCell : LclgvComboBoxCell
{
	public override Type EditType => typeof(LclgvItgOprtEditingControl);

	public override Type ValueType => typeof(IntegOprtStyle);

	protected override string cellString(object value)
	{
		if (value == null)
		{
			return "";
		}
		return LclgvItgOprtEditingControl.ShowString((IntegOprtStyle)value);
	}
}
