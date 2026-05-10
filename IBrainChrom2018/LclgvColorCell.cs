using System.Drawing;
using System.Windows.Forms;

namespace IBrainChrom2018;

public class LclgvColorCell : DataGridViewTextBoxCell
{
	private ColorDialog colorDialog_0 = new ColorDialog();

	private Color color_0 = Color.Azure;

	private SolidBrush solidBrush_0 = new SolidBrush(Color.Red);

	public DialogResult selectResult;

	public Color Color
	{
		get
		{
			return color_0;
		}
		set
		{
			color_0 = value;
			base.DataGridView.InvalidateCell(this);
		}
	}

	protected override void OnDoubleClick(DataGridViewCellEventArgs dataGridViewCellEventArgs_0)
	{
		if ((selectResult = colorDialog_0.ShowDialog()) == DialogResult.OK)
		{
			bool flag = false;
			if (!colorDialog_0.Color.Equals(Color))
			{
				flag = true;
			}
			Color = colorDialog_0.Color;
			if (flag)
			{
				Class49.SendMessage(base.DataGridView.Handle, 2048u, dataGridViewCellEventArgs_0.RowIndex, 0);
			}
		}
		base.OnDoubleClick(dataGridViewCellEventArgs_0);
	}

	protected override void Paint(Graphics graphics, Rectangle clipBounds, Rectangle cellBounds, int rowIndex, DataGridViewElementStates cellState, object value, object formattedValue, string errorText, DataGridViewCellStyle cellStyle, DataGridViewAdvancedBorderStyle advancedBorderStyle, DataGridViewPaintParts paintParts)
	{
		graphics.FillRectangle(Brushes.White, cellBounds);
		solidBrush_0.Color = Color;
		graphics.FillRectangle(solidBrush_0, cellBounds);
		LclGridView.DrawCellEdge(graphics, LclGridView.penCellEdge, cellBounds, offset: true);
	}
}
