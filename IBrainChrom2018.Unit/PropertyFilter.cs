using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace IBrainChrom2018.Unit;

public class PropertyFilter
{
	public delegate void SetFilterMethold(object component, List<PropertyDescriptor> psList);

	public class PropertyFilterMethold
	{
		public string strComonentName;

		public SetFilterMethold funFilter;

		public PropertyFilterMethold(string strName, SetFilterMethold fun)
		{
			strComonentName = strName;
			funFilter = fun;
		}
	}

	private static List<PropertyFilterMethold> m_FilterList = new List<PropertyFilterMethold>();

	public static void AddFilter(string strName, SetFilterMethold fun)
	{
		List<PropertyFilterMethold> list = m_FilterList.Where((PropertyFilterMethold x) => x.strComonentName == strName).ToList();
		if (list.Count > 0)
		{
			list[0].funFilter = fun;
		}
		else
		{
			m_FilterList.Add(new PropertyFilterMethold(strName, fun));
		}
	}

	public static SetFilterMethold GetFilter(string strName)
	{
		List<SetFilterMethold> list = (from x in m_FilterList
			where x.strComonentName == strName
			select x.funFilter).ToList();
		if (list.Count > 0)
		{
			return list[0];
		}
		return null;
	}

	public static void RemoveFilter(string strName)
	{
		List<PropertyFilterMethold> list = m_FilterList.Where((PropertyFilterMethold x) => x.strComonentName == strName).ToList();
		while (list.Count > 0)
		{
			m_FilterList.Remove(list[0]);
			list.RemoveAt(0);
		}
	}

	public static void RemovePropertyItem(List<PropertyDescriptor> psList, List<string> strNameList)
	{
		foreach (string strName in strNameList)
		{
			List<PropertyDescriptor> list = psList.Where((PropertyDescriptor x) => x.Name == strName).ToList();
			while (list.Count > 0)
			{
				psList.Remove(list[0]);
				list.RemoveAt(0);
			}
		}
	}

	public static void SetDetectFilter(object component, List<PropertyDescriptor> psList, List<PropertyFilterItem> filterList)
	{
		if (filterList.Count == 0)
		{
			return;
		}
		Type type = component.GetType();
		foreach (PropertyFilterItem filter in filterList)
		{
			PropertyInfo property = type.GetProperty(filter.strName);
			if (property == null)
			{
				continue;
			}
			int num = 0;
			if (property.PropertyType == Type.GetType("System.Int32"))
			{
				num = (int)property.GetValue(component, null);
			}
			else
			{
				if (!(property.PropertyType == Type.GetType("System.Boolean")))
				{
					continue;
				}
				num = (((bool)property.GetValue(component, null)) ? 1 : 0);
			}
			foreach (PropertyFilterSubItem item in filter.sublist)
			{
				if (!item.ValueInList(num))
				{
					RemovePropertyItem(psList, item.nameList);
				}
			}
		}
	}
}
