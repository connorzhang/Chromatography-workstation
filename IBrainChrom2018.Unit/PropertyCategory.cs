using System.ComponentModel;

namespace IBrainChrom2018.Unit;

public class PropertyCategory : CategoryAttribute
{
	public PropertyCategory(string mycate)
		: base(mycate)
	{
	}

	protected override string GetLocalizedString(string value)
	{
		return Lang.PS(value);
	}
}
