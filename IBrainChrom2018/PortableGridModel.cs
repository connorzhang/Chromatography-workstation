using System.Collections.Generic;

namespace IBrainChrom2018;

public class PortableGridModel
{
	public string name { get; set; }

	public string setV { get; set; }

	public string curV { get; set; }

	public List<PortableGridModel> Childrens { get; set; }
}
