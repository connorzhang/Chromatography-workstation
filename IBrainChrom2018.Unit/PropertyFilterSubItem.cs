using System.Collections.Generic;
using System.Linq;

namespace IBrainChrom2018.Unit;

public class PropertyFilterSubItem
{
	public List<int> valueList = new List<int>();

	public List<string> nameList;

	public PropertyFilterSubItem(int val, List<string> namelist)
	{
		valueList.Add(val);
		nameList = namelist;
	}

	public PropertyFilterSubItem(int val, string namelist)
	{
		valueList.Add(val);
		nameList = namelist.Split(',').ToList();
	}

	public PropertyFilterSubItem(string vallist, string namelist)
	{
		List<string> source = vallist.Split(',').ToList();
		valueList = source.Select((string x) => int.Parse(x)).ToList();
		nameList = namelist.Split(',').ToList();
	}

	public bool ValueInList(int val)
	{
		return valueList.Where((int x) => x == val).ToList().Count > 0;
	}
}
