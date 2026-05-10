using System;

namespace IBrainChrom2018;

public class DisChain
{
	private const int int_0 = 20;

	private DisLg[] disLg_0 = new DisLg[0];

	private int dispIdx = -1;

	public int Count => disLg_0.Length;

	public DisLg CurDisLg => disLg_0[dispIdx];

	public int DynNo
	{
		get
		{
			return dispIdx;
		}
		set
		{
			Class49.SafeValueCheck(ref value, 0, Count - 1);
			dispIdx = value;
		}
	}

	public bool HasNext => dispIdx >= 0 && dispIdx < Count - 1;

	public bool HasPrevious => dispIdx > 0;

	public void AppendFrameLg(DisLg disLg)
	{
		if (dispIdx < 0 || disLg.lgX != CurDisLg.lgX || disLg.lgY != CurDisLg.lgY || disLg.lgXBeg != CurDisLg.lgXBeg || disLg.lgYBeg != CurDisLg.lgYBeg)
		{
			MustAppendFrameLg(disLg);
		}
	}

	public void Clear()
	{
		Array.Resize(ref disLg_0, 0);
		dispIdx = -1;
	}

	public void LoadFromObject(DisChain disChain)
	{
		Clear();
		for (int i = 0; i < disChain.Count; i++)
		{
			AppendFrameLg(disChain.disLg_0[i]);
		}
	}

	public void MustAppendFrameLg(DisLg disLg)
	{
		if (dispIdx >= 0)
		{
			Array.Resize(ref disLg_0, dispIdx + 1);
		}
		if (Count >= 20)
		{
			for (int i = 0; i < Count - 1; i++)
			{
				disLg_0[i] = disLg_0[i + 1];
			}
		}
		else
		{
			Array.Resize(ref disLg_0, Count + 1);
			dispIdx++;
		}
		disLg_0[dispIdx] = disLg;
	}

	public void ReplaceCurFrameLg(DisLg disLg)
	{
		if (dispIdx >= 0)
		{
			disLg_0[dispIdx] = disLg;
		}
		else
		{
			AppendFrameLg(disLg);
		}
	}
}
