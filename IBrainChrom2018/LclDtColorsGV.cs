using System;
using System.Drawing;
using System.Windows.Forms;

namespace IBrainChrom2018;

public class LclDtColorsGV : LclSigColorsGV
{
	private const string string_2 = "同仪器";

	private const string string_3 = "检测器";

	private const string string_4 = "As Instru";

	private const string string_5 = "Detector";

	public bool detector1AsInstru = true;

	private string sAsInstru => Class49.sysLanguage_0 switch
	{
		SysLanguage.CN => "同仪器", 
		SysLanguage.EN => "As Instru", 
		_ => "", 
	};

	private string sDetector => Class49.sysLanguage_0 switch
	{
		SysLanguage.CN => "检测器", 
		SysLanguage.EN => "Detector", 
		_ => "", 
	};

	protected override void OnCellPainting(DataGridViewCellPaintingEventArgs dataGridViewCellPaintingEventArgs_0)
	{
		Graphics graphics = dataGridViewCellPaintingEventArgs_0.Graphics;
		Rectangle cellBounds = dataGridViewCellPaintingEventArgs_0.CellBounds;
		if (dataGridViewCellPaintingEventArgs_0.ColumnIndex == -1 && dataGridViewCellPaintingEventArgs_0.RowIndex >= 0)
		{
			drawHeaderBackground(graphics, cellBounds, -1, -1, 0, 0);
			cellBounds.Y += 2;
			graphics.DrawString(sDetector + " " + (dataGridViewCellPaintingEventArgs_0.RowIndex + 1), Font, Brushes.Black, cellBounds, rowHeaderFormat);
			dataGridViewCellPaintingEventArgs_0.Handled = true;
		}
		else if (detector1AsInstru && dataGridViewCellPaintingEventArgs_0.RowIndex == 0)
		{
			graphics.FillRectangle(Brushes.White, cellBounds);
			LclGridView.DrawCellEdge(graphics, LclGridView.penCellEdge, cellBounds, offset: true);
			cellBounds.Y += 2;
			graphics.DrawString(sAsInstru, Font, Brushes.Black, cellBounds, rowHeaderFormat);
			dataGridViewCellPaintingEventArgs_0.Handled = true;
		}
		else
		{
			base.OnCellPainting(dataGridViewCellPaintingEventArgs_0);
		}
	}

	protected override void OnClick(EventArgs eventArgs_0)
	{
		base.OnClick(eventArgs_0);
	}
}
