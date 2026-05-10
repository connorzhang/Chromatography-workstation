using System.Collections.Generic;

namespace IBrainChrom2018;

public class statisticsGridModel
{
	public string DataTime { get; set; }

	public float RtNmhc { get; set; }

	public float AreaNmhc { get; set; }

	public float AmountNmhc { get; set; }

	public float RtThc { get; set; }

	public float AreaThc { get; set; }

	public float AmountThc { get; set; }

	public float RtCh4 { get; set; }

	public float AreaCh4 { get; set; }

	public float AmountCh4 { get; set; }

	public string unit { get; set; }

	public List<statisticsGridModel> Childrens { get; set; }
}
