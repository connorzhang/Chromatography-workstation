using System.Drawing;
using System.Windows.Forms;

namespace IBrainChrom2018;

public class LclgvIconCell : DataGridViewTextBoxCell
{
	private Bitmap bitmap_0;

	public Bitmap Img
	{
		set
		{
			bitmap_0 = value;
		}
	}

	protected override void Paint(Graphics graphics, Rectangle clipBounds, Rectangle cellBounds, int rowIndex, DataGridViewElementStates cellState, object value, object formattedValue, string errorText, DataGridViewCellStyle cellStyle, DataGridViewAdvancedBorderStyle advancedBorderStyle, DataGridViewPaintParts paintParts)
	{
		Rectangle rectangle = cellBounds;
		graphics.FillRectangle(Brushes.White, cellBounds);
		if (bitmap_0 != null)
		{
			rectangle.X += (rectangle.Width - bitmap_0.Width) / 2;
			rectangle.Y--;
			ResourceImageLoad.DrawToGraphic(graphics, bitmap_0, rectangle.Location);
		}
		LclGridView.DrawCellEdge(graphics, LclGridView.penCellEdge, cellBounds, offset: true);
	}
}
