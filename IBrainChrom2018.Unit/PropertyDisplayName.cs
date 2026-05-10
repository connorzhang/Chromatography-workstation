using System.ComponentModel;

namespace IBrainChrom2018.Unit;

public class PropertyDisplayName : DisplayNameAttribute
{
	private string myDisplayName;

	public override string DisplayName => myDisplayName;

	public PropertyDisplayName(string displayName)
		: base(displayName)
	{
		myDisplayName = Lang.PS(displayName);
	}
}
