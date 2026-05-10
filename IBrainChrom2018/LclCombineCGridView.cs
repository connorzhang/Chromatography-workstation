using System;
using System.Drawing;
using System.Windows.Forms;

namespace IBrainChrom2018;

public class LclCombineCGridView : LclGridView
{
	protected CombineC[] combineCs = new CombineC[0];

	public int combineH = 10;

	public int AddCombineC(CombineC combineC)
	{
		int num = combineCs.Length;
		Array.Resize(ref combineCs, num + 1);
		combineCs[num] = combineC;
		return num;
	}

	public void AdjustCombineDisInfo(bool read_refresh)
	{
		if (read_refresh)
		{
			Refresh_ShowHideColumns(AccStyle.Read);
		}
		int i;
		for (i = 0; i < combineCs.Length; i++)
		{
			CombineC combineC = combineCs[i];
			combineC.numDisplayIndices = 0;
			int num = -1;
			for (int j = 0; j < showColumns.Length; j++)
			{
				if (!combineC.Contains(showColumns[j].Index))
				{
					continue;
				}
				num = ((num >= 0) ? (num + 1) : j);
				if (j > num)
				{
					DataGridViewColumn dataGridViewColumn = showColumns[j];
					for (int num2 = j; num2 > num; num2--)
					{
						showColumns[num2] = showColumns[num2 - 1];
					}
					showColumns[num] = dataGridViewColumn;
				}
				combineC.numDisplayIndices++;
			}
		}
		i = 0;
		while (i < combineCs.Length)
		{
			CombineC combineC2 = combineCs[i];
			for (int k = 0; k < showColumns.Length; k++)
			{
				if (combineC2.Contains(showColumns[k].Index))
				{
					combineC2.begDisplayIndex = frozenNum + k;
					i++;
					break;
				}
			}
		}
		Refresh_ShowHideColumns(AccStyle.Write);
	}

	public void ClearCombineCs()
	{
		Array.Resize(ref combineCs, 0);
	}

	private Rectangle method_3(Graphics graphics_0, Rectangle rectangle_0)
	{
		rectangle_0.Offset(-1, -1);
		rectangle_0.Y += combineH;
		rectangle_0.Height -= combineH;
		graphics_0.FillRectangle(Brushes.Gainsboro, rectangle_0);
		graphics_0.DrawRectangle(Pens.Gray, rectangle_0);
		return rectangle_0;
	}

	protected override void OnCellPainting(DataGridViewCellPaintingEventArgs dataGridViewCellPaintingEventArgs_0)
	{
		Graphics graphics = dataGridViewCellPaintingEventArgs_0.Graphics;
		Rectangle cellBounds = dataGridViewCellPaintingEventArgs_0.CellBounds;
		if (dataGridViewCellPaintingEventArgs_0.RowIndex != -1)
		{
			base.OnCellPainting(dataGridViewCellPaintingEventArgs_0);
			return;
		}
		int num = method_4(dataGridViewCellPaintingEventArgs_0.ColumnIndex);
		if (num != -1)
		{
			cellBounds = method_3(graphics, cellBounds);
			LclGridView.drawValue(graphics, dataGridViewCellPaintingEventArgs_0.Value, cellBounds, 0, 2, 0, 0, Font, retHeaderColor(dataGridViewCellPaintingEventArgs_0.ColumnIndex), headerFormat);
			cellBounds = dataGridViewCellPaintingEventArgs_0.CellBounds;
			cellBounds.X -= combineCs[num].GetOffset(this, dataGridViewCellPaintingEventArgs_0.ColumnIndex);
			cellBounds.Width = combineCs[num].GetWholeWidth(this) - 1;
			cellBounds.Height = combineH;
			graphics.FillRectangle(Brushes.Gainsboro, cellBounds);
			LclGridView.DrawCellEdge(graphics, Pens.Gray, cellBounds, offset: false);
			LclGridView.drawValue(graphics, combineCs[num].text, cellBounds, 0, 1, 0, 0, Font, Color.Black, headerFormat);
			dataGridViewCellPaintingEventArgs_0.Handled = true;
		}
		else
		{
			base.OnCellPainting(dataGridViewCellPaintingEventArgs_0);
		}
	}

	private int method_4(int int_2)
	{
		for (int i = 0; i < combineCs.Length; i++)
		{
			if (combineCs[i].Contains(int_2))
			{
				return i;
			}
		}
		return -1;
	}

	public void SetCombineCText(int combineNo, string combineText)
	{
		if (combineNo < combineCs.Length)
		{
			combineCs[combineNo].text = combineText;
			for (int i = 0; i < combineCs[combineNo].indices.Length; i++)
			{
				InvalidateCell(combineCs[combineNo].indices[i], -1);
			}
		}
	}
}
