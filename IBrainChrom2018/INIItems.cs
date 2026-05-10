using System.Collections;

namespace IBrainChrom2018;

public class INIItems : DictionaryBase
{
	private INISegment inisegment_0;

	public INISegment Owner => inisegment_0;

	public ICollection Keys => base.Dictionary.Keys;

	public ICollection Values => base.Dictionary.Values;

	public INIItem this[string vName]
	{
		get
		{
			if (base.Dictionary.Contains(vName))
			{
				return (INIItem)base.Dictionary[vName];
			}
			return Add(vName, "");
		}
	}

	public INIItems(INISegment inisegment_1)
	{
		inisegment_0 = inisegment_1;
	}

	public void Add(INIItem iniitem_0)
	{
		if (!base.Dictionary.Contains(iniitem_0.Name))
		{
			base.Dictionary.Add(iniitem_0.Name, iniitem_0);
		}
	}

	public bool Contains(string vName)
	{
		return base.Dictionary.Contains(vName);
	}

	public INIItem Add(string vName, string vValue)
	{
		if (base.Dictionary.Contains(vName))
		{
			return (INIItem)base.Dictionary[vName];
		}
		INIItem iNIItem = new INIItem(this, vName, vValue);
		Add(iNIItem);
		return iNIItem;
	}
}
