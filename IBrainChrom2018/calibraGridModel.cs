using System.Collections.Generic;

namespace IBrainChrom2018;

public class calibraGridModel
{
	public string ID { get; set; }

	public float RT1 { get; set; }

	public float Area1 { get; set; }

	public float Height1 { get; set; }

	public float RT2 { get; set; }

	public float Area2 { get; set; }

	public float Height2 { get; set; }

	public List<PortableGridModel> Childrens { get; set; }
}
