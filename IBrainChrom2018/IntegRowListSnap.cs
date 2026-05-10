using System;

namespace IBrainChrom2018;

public class IntegRowListSnap
{
	public IntegRow[] integRow_0 = new IntegRow[0];

	public void CopyFrom(IntegRow[] integRow_1)
	{
		Array.Resize(ref integRow_0, integRow_1.Length);
		for (int i = 0; i < integRow_0.Length; i++)
		{
			integRow_0[i] = integRow_1[i];
		}
	}

	public void CloneTo(ref IntegRow[] integRow_1)
	{
		Array.Resize(ref integRow_1, integRow_0.Length);
		for (int i = 0; i < integRow_1.Length; i++)
		{
			integRow_1[i] = integRow_0[i];
		}
	}
}
