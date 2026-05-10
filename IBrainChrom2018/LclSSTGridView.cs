using System.Drawing;
using System.Windows.Forms;

namespace IBrainChrom2018;

public class LclSSTGridView : LclGridView
{
	public const int ParasRowsNum = 6;

	public override bool BeginEdit(bool selectAll)
	{
		return 0 <= base.CurrentCell.RowIndex && base.CurrentCell.RowIndex <= 2 && base.CurrentCell.ColumnIndex >= frozenNum && base.BeginEdit(selectAll);
	}

	protected override void OnCellPainting(DataGridViewCellPaintingEventArgs dataGridViewCellPaintingEventArgs_0)
	{
		Graphics graphics = dataGridViewCellPaintingEventArgs_0.Graphics;
		Rectangle cellBounds = dataGridViewCellPaintingEventArgs_0.CellBounds;
		if (0 <= dataGridViewCellPaintingEventArgs_0.RowIndex && dataGridViewCellPaintingEventArgs_0.RowIndex < 5)
		{
			if (0 <= dataGridViewCellPaintingEventArgs_0.ColumnIndex && dataGridViewCellPaintingEventArgs_0.ColumnIndex <= 1)
			{
				LclGridView.sbCell.Color = Color.White;
				graphics.FillRectangle(LclGridView.sbCell, cellBounds);
				cellBounds = dataGridViewCellPaintingEventArgs_0.CellBounds;
				graphics.DrawLine(LclGridView.penCellEdge, new Point(cellBounds.Left, cellBounds.Bottom - 1), new Point(cellBounds.Right, cellBounds.Bottom - 1));
				if (dataGridViewCellPaintingEventArgs_0.ColumnIndex == 1)
				{
					graphics.DrawLine(LclGridView.penCellEdge, new Point(cellBounds.Right - 1, cellBounds.Top), new Point(cellBounds.Right - 1, cellBounds.Bottom));
				}
				cellBounds.X -= getColumnsWholeWidth(0, dataGridViewCellPaintingEventArgs_0.ColumnIndex - 1);
				cellBounds.Width = getColumnsWholeWidth(0, 1);
				valueFormat.Alignment = StringAlignment.Far;
				LclGridView.drawValue(graphics, dataGridViewCellPaintingEventArgs_0.Value, cellBounds, 0, 1, 0, 0, Font, Color.Black, valueFormat);
				dataGridViewCellPaintingEventArgs_0.Handled = true;
			}
			else
			{
				base.OnCellPainting(dataGridViewCellPaintingEventArgs_0);
			}
		}
		else if (dataGridViewCellPaintingEventArgs_0.RowIndex == 5)
		{
			LclGridView.sbCell.Color = Color.White;
			graphics.FillRectangle(LclGridView.sbCell, cellBounds);
			cellBounds = dataGridViewCellPaintingEventArgs_0.CellBounds;
			graphics.DrawLine(Pens.Black, new Point(cellBounds.Left, cellBounds.Bottom - 1), new Point(cellBounds.Right, cellBounds.Bottom - 1));
			if (dataGridViewCellPaintingEventArgs_0.ColumnIndex >= 1)
			{
				graphics.DrawLine(LclGridView.penCellEdge, new Point(cellBounds.Right - 1, cellBounds.Top), new Point(cellBounds.Right - 1, cellBounds.Bottom));
			}
			if (0 <= dataGridViewCellPaintingEventArgs_0.ColumnIndex && dataGridViewCellPaintingEventArgs_0.ColumnIndex < frozenNum)
			{
				cellBounds.X -= getColumnsWholeWidth(0, dataGridViewCellPaintingEventArgs_0.ColumnIndex - 1);
				cellBounds.Width = getColumnsWholeWidth(0, 1);
				valueFormat.Alignment = StringAlignment.Far;
				LclGridView.drawValue(graphics, dataGridViewCellPaintingEventArgs_0.Value, cellBounds, 0, 1, 0, 0, Font, Color.Black, valueFormat);
			}
			else if (dataGridViewCellPaintingEventArgs_0.Value != null)
			{
				Bitmap bitmap = (SSTResult)dataGridViewCellPaintingEventArgs_0.Value switch
				{
					SSTResult.Unknown => SystemIconResource.smethod_26(), 
					SSTResult.Fail => SystemIconResource.smethod_24(), 
					SSTResult.Success => SystemIconResource.smethod_25(), 
					_ => null, 
				};
				if (bitmap != null)
				{
					cellBounds.X += (cellBounds.Width - bitmap.Width) / 2;
					cellBounds.Y--;
					ResourceImageLoad.DrawToGraphic(graphics, bitmap, cellBounds.Location);
				}
			}
			dataGridViewCellPaintingEventArgs_0.Handled = true;
		}
		else
		{
			base.OnCellPainting(dataGridViewCellPaintingEventArgs_0);
		}
	}

	public void SetChromCount(int chromCount)
	{
		base.RowCount = 6 + chromCount;
	}
}
