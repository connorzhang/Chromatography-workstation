using System.Collections.Generic;

namespace IBrainChrom2018.Unit;

public class PropertyFilterItem
{
	public string strName;

	public List<PropertyFilterSubItem> sublist;

	public PropertyFilterItem(string name, List<PropertyFilterSubItem> list)
	{
		strName = name;
		sublist = list;
	}

	public PropertyFilterItem(List<PropertyFilterSubItem> list)
	{
		sublist = list;
	}
}
