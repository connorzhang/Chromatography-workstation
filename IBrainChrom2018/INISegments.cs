using System.Collections;

namespace IBrainChrom2018;

public class INISegments : DictionaryBase
{
	private INIFile inifile_0;

	public INIFile Owner => inifile_0;

	public ICollection Keys => base.Dictionary.Keys;

	public ICollection Values => base.Dictionary.Values;

	public INISegment this[string vName]
	{
		get
		{
			if (!base.Dictionary.Contains(vName))
			{
				return Add(vName);
			}
			return (INISegment)base.Dictionary[vName];
		}
	}

	public INISegments(INIFile inifile_1)
	{
		inifile_0 = inifile_1;
	}

	public void Add(INISegment inisegment_0)
	{
		if (!base.Dictionary.Contains(inisegment_0.Name))
		{
			base.Dictionary.Add(inisegment_0.Name, inisegment_0);
		}
	}

	public INISegment Add(string vName)
	{
		if (base.Dictionary.Contains(vName))
		{
			return (INISegment)base.Dictionary[vName];
		}
		INISegment iNISegment = new INISegment(this, vName);
		base.Dictionary.Add(vName, iNISegment);
		return iNISegment;
	}

	public bool Contains(string vName)
	{
		return base.Dictionary.Contains(vName);
	}
}
