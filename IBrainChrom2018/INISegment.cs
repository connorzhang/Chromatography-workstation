namespace IBrainChrom2018;

public class INISegment
{
	private string string_0;

	private INISegments inisegments_0;

	public INIItems Items;

	public string Name => string_0;

	public INISegments Owner => inisegments_0;

	public INISegment(INISegments inisegments_1, string vName)
	{
		inisegments_0 = inisegments_1;
		string_0 = vName;
		Items = new INIItems(this);
		inisegments_1.Owner.GetSegment(this);
	}

	public void Clear()
	{
		inisegments_0.Owner.WriteSegment(string_0, "\0\0");
	}
}
