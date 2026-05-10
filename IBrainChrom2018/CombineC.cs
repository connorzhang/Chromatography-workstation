namespace IBrainChrom2018;

public class CombineC
{
	public int begDisplayIndex;

	public int[] indices = new int[0];

	public int numDisplayIndices;

	public string text = "";

	public bool Contains(int index)
	{
		for (int i = 0; i < indices.Length; i++)
		{
			if (indices[i] == index)
			{
				return true;
			}
		}
		return false;
	}

	public int GetOffset(LclGridView lclGridView_0, int index)
	{
		int num = 0;
		for (int i = 0; i < numDisplayIndices; i++)
		{
			int visibleIndex = lclGridView_0.getVisibleIndex(begDisplayIndex + i);
			if (visibleIndex == index)
			{
				return num;
			}
			num += lclGridView_0.Columns[visibleIndex].Width;
		}
		return num;
	}

	public int GetWholeWidth(LclGridView lclGridView_0)
	{
		int num = 0;
		for (int i = 0; i < numDisplayIndices; i++)
		{
			int visibleIndex = lclGridView_0.getVisibleIndex(begDisplayIndex + i);
			num += lclGridView_0.Columns[visibleIndex].Width;
		}
		return num;
	}
}
