using System;

namespace IBrainChrom2018;

public class LclCusComboBox : LclComboBox
{
	private string[] string_1 = new string[0];

	private string[] string_2 = new string[0];

	public void InitItems(object[] items)
	{
		base.Items.Clear();
		for (int i = 0; i < items.Length; i++)
		{
			base.Items.Add(items[i]);
		}
		Array.Resize(ref string_1, items.Length);
		Array.Resize(ref string_2, items.Length);
		base.ItemExtString = " ";
	}

	public void InitShowText(string[] texts)
	{
		for (int i = 0; i < string_1.Length; i++)
		{
			if (i < texts.Length)
			{
				string_1[i] = texts[i];
			}
		}
	}

	protected override string itemString(int itemIndex)
	{
		return string_1[itemIndex];
	}
}
