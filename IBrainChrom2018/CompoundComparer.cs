using System.Collections;

namespace IBrainChrom2018;

public class CompoundComparer : IComparer
{
	int IComparer.Compare(object object_0, object object_1)
	{
		Compound compound = object_0 as Compound;
		Compound compound2 = object_1 as Compound;
		if (compound.cmpdInfo.retainTime < compound2.cmpdInfo.retainTime)
		{
			return -1;
		}
		if (compound.cmpdInfo.retainTime == compound2.cmpdInfo.retainTime)
		{
			return 0;
		}
		return 1;
	}
}
