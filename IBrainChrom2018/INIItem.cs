namespace IBrainChrom2018;

public class INIItem
{
	private string string_0;

	private string string_1;

	private INIItems iniitems_0;

	public string Name => string_0;

	public string Value
	{
		get
		{
			return string_1;
		}
		set
		{
			string_1 = value;
			iniitems_0.Owner.Owner.Owner.SetString(iniitems_0.Owner.Name, string_0, value);
		}
	}

	public INIItems Owner => iniitems_0;

	public INIItem(INIItems iniitems_1, string vName, string vValue)
	{
		iniitems_0 = iniitems_1;
		string_0 = vName;
		string_1 = vValue;
		if (!iniitems_1.Contains(vName))
		{
			iniitems_1.Owner.Owner.Owner.SetString(iniitems_1.Owner.Name, vName, vValue);
		}
	}
}
