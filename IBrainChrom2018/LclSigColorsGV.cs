using System.Drawing;
using System.Windows.Forms;

namespace IBrainChrom2018;

public class LclSigColorsGV : LclGridView
{
	private const string string_0 = "信号";

	private const string string_1 = "Signal";

	private string sSignal => Class49.sysLanguage_0 switch
	{
		SysLanguage.CN => "信号", 
		SysLanguage.EN => "Signal", 
		_ => "", 
	};

	public LclSigColorsGV()
	{
		base.ScrollBars = ScrollBars.None;
		base.ColumnHeadersVisible = false;
		base.MultiSelect = false;
		base.RowHeadersWidth = 80;
		rowHeaderFormat.Alignment = StringAlignment.Near;
		base.ReadOnly = true;
	}

	protected override void OnCellPainting(DataGridViewCellPaintingEventArgs dataGridViewCellPaintingEventArgs_0)
	{
		Graphics graphics = dataGridViewCellPaintingEventArgs_0.Graphics;
		Rectangle cellBounds = dataGridViewCellPaintingEventArgs_0.CellBounds;
		if (dataGridViewCellPaintingEventArgs_0.ColumnIndex == -1 && dataGridViewCellPaintingEventArgs_0.RowIndex >= 0)
		{
			drawHeaderBackground(graphics, cellBounds, -1, -1, 0, 0);
			cellBounds.Y += 2;
			graphics.DrawString(sSignal + " " + (dataGridViewCellPaintingEventArgs_0.RowIndex + 1), Font, Brushes.Black, cellBounds, rowHeaderFormat);
			dataGridViewCellPaintingEventArgs_0.Handled = true;
		}
		else
		{
			base.OnCellPainting(dataGridViewCellPaintingEventArgs_0);
		}
	}

	public void Refresh_Colors(AccStyle accStyle, Color[] colors)
	{
		switch (accStyle)
		{
		case AccStyle.Read:
		{
			for (int j = 0; j < base.RowCount; j++)
			{
				(base.Rows[j].Cells[0] as LclgvColorCell).Color = colors[j];
			}
			Refresh();
			break;
		}
		case AccStyle.Write:
		{
			for (int i = 0; i < base.RowCount; i++)
			{
				colors[i] = (base.Rows[i].Cells[0] as LclgvColorCell).Color;
			}
			break;
		}
		}
	}
}
