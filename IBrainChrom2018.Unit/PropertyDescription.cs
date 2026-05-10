using System.ComponentModel;

namespace IBrainChrom2018.Unit;

public class PropertyDescription : DescriptionAttribute
{
	private string myDescription;

	public override string Description => myDescription;

	public PropertyDescription(string mydescription)
		: base(mydescription)
	{
		myDescription = Lang.PS(mydescription);
	}
}
