using System.Collections.Generic;

namespace IBrainChrom2018;

public class ResultGridModel
{
	public string name { get; set; }

	public string curV { get; set; }

	public List<ResultGridModel> Childrens { get; set; }
}
