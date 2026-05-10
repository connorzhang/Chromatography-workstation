using System;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace IBrainChrom2018;

public class LclComboBox : ComboBox
{
	public delegate void SelectedIndexChanging(int justSelectedIndex);

	private string string_0 = "";

	private int int_0 = -1;

	private SolidBrush solidBrush_0 = new SolidBrush(Color.Black);

	private SelectedIndexChanging selectedIndexChanging_0;

	public string ItemExtString
	{
		get
		{
			return string_0;
		}
		set
		{
			string_0 = value;
			if (value != null && !(value == ""))
			{
				base.DrawMode = DrawMode.OwnerDrawVariable;
				Class49.SendMessage(base.Handle, 339u, -1, 13);
			}
			else
			{
				base.DrawMode = DrawMode.Normal;
			}
		}
	}

	public event SelectedIndexChanging OnSelectedIndexChanging
	{
		add
		{
			SelectedIndexChanging selectedIndexChanging = selectedIndexChanging_0;
			SelectedIndexChanging selectedIndexChanging2;
			do
			{
				selectedIndexChanging2 = selectedIndexChanging;
				SelectedIndexChanging value2 = (SelectedIndexChanging)Delegate.Combine(selectedIndexChanging2, value);
				selectedIndexChanging = Interlocked.CompareExchange(ref selectedIndexChanging_0, value2, selectedIndexChanging2);
			}
			while (selectedIndexChanging != selectedIndexChanging2);
		}
		remove
		{
			SelectedIndexChanging selectedIndexChanging = selectedIndexChanging_0;
			SelectedIndexChanging selectedIndexChanging2;
			do
			{
				selectedIndexChanging2 = selectedIndexChanging;
				SelectedIndexChanging value2 = (SelectedIndexChanging)Delegate.Remove(selectedIndexChanging2, value);
				selectedIndexChanging = Interlocked.CompareExchange(ref selectedIndexChanging_0, value2, selectedIndexChanging2);
			}
			while (selectedIndexChanging != selectedIndexChanging2);
		}
	}

	public LclComboBox()
	{
		base.DropDownStyle = ComboBoxStyle.DropDownList;
	}

	public void ClearItems()
	{
		base.Items.Clear();
		int_0 = -1;
	}

	protected virtual string itemString(int itemIndex)
	{
		return base.Items[itemIndex].ToString();
	}

	protected override void OnDrawItem(DrawItemEventArgs drawItemEventArgs_0)
	{
		if (drawItemEventArgs_0.Index != -1)
		{
			Rectangle bounds = drawItemEventArgs_0.Bounds;
			drawItemEventArgs_0.DrawBackground();
			string s = itemString(drawItemEventArgs_0.Index) + string_0;
			solidBrush_0.Color = Color.Black;
			if ((drawItemEventArgs_0.State & DrawItemState.Selected) == DrawItemState.Selected)
			{
				solidBrush_0.Color = Color.White;
			}
			drawItemEventArgs_0.Graphics.DrawString(s, Font, solidBrush_0, bounds);
		}
	}

	protected override void OnMeasureItem(MeasureItemEventArgs measureItemEventArgs_0)
	{
		measureItemEventArgs_0.ItemHeight = 14;
	}

	protected override void OnSelectedIndexChanged(EventArgs eventArgs_0)
	{
		if (selectedIndexChanging_0 != null)
		{
			selectedIndexChanging_0(int_0);
		}
		base.OnSelectedIndexChanged(eventArgs_0);
		int_0 = SelectedIndex;
	}

	public bool SetValue(ref float value)
	{
		if (base.SelectedItem == null)
		{
			return false;
		}
		value = Convert.ToSingle(base.SelectedItem);
		return true;
	}

	public bool ShowValue(object value)
	{
		for (int i = 0; i < base.Items.Count; i++)
		{
			if (base.Items[i].Equals(value))
			{
				base.SelectedItem = base.Items[i];
				return true;
			}
		}
		base.SelectedItem = null;
		return false;
	}
}
