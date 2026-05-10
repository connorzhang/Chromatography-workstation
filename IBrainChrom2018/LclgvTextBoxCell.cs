using System;
using System.Drawing;
using System.Windows.Forms;

namespace IBrainChrom2018;

public class LclgvTextBoxCell : DataGridViewTextBoxCell
{
	private static Pen pen_0 = new Pen(Color.Black);

	private static SolidBrush solidBrush_0 = new SolidBrush(Color.Black);

	private static StringFormat stringFormat_0 = new StringFormat();

	public override Type EditType => typeof(LclgvTextBoxEditingControl);

	protected override void Paint(Graphics graphics, Rectangle clipBounds, Rectangle cellBounds, int rowIndex, DataGridViewElementStates cellState, object value, object formattedValue, string errorText, DataGridViewCellStyle cellStyle, DataGridViewAdvancedBorderStyle advancedBorderStyle, DataGridViewPaintParts paintParts)
	{
		Rectangle rectangle_ = cellBounds;
		if (cellStyle.Alignment == DataGridViewContentAlignment.MiddleLeft)
		{
			stringFormat_0.Alignment = StringAlignment.Near;
		}
		else if (cellStyle.Alignment == DataGridViewContentAlignment.MiddleCenter)
		{
			stringFormat_0.Alignment = StringAlignment.Center;
		}
		else
		{
			stringFormat_0.Alignment = StringAlignment.Far;
		}
		solidBrush_0.Color = cellStyle.BackColor;
		graphics.FillRectangle(solidBrush_0, cellBounds);
		if ((cellState & DataGridViewElementStates.Selected) == DataGridViewElementStates.Selected)
		{
			rectangle_.Height--;
			pen_0.Color = cellStyle.SelectionBackColor;
			if (base.RowIndex != 0)
			{
				graphics.DrawLine(pen_0, rectangle_.Left, rectangle_.Top, rectangle_.Right, rectangle_.Top);
			}
			graphics.DrawLine(pen_0, rectangle_.Left, rectangle_.Bottom - 1, rectangle_.Right, rectangle_.Bottom - 1);
			Color color = cellStyle.ForeColor;
			if (cellStyle.ForeColor == Color.Red)
			{
				color = Color.Red;
			}
			LclGridView.drawValue(graphics, formattedValue, rectangle_, 0, 2, 0, 0, cellStyle.Font, color, stringFormat_0);
		}
		else
		{
			Color foreColor = cellStyle.ForeColor;
			LclGridView.drawValue(graphics, formattedValue, rectangle_, 0, 2, 0, 0, cellStyle.Font, foreColor, stringFormat_0);
		}
		LclGridView.DrawCellEdge(graphics, LclGridView.penCellEdge, cellBounds, offset: true);
	}

	public override Rectangle PositionEditingPanel(Rectangle cellBounds, Rectangle cellClip, DataGridViewCellStyle cellStyle, bool singleVerticalBorderAdded, bool singleHorizontalBorderAdded, bool isFirstDisplayedColumn, bool isFirstDisplayedRow)
	{
		Rectangle result = base.PositionEditingPanel(cellBounds, cellClip, cellStyle, singleVerticalBorderAdded, singleHorizontalBorderAdded, isFirstDisplayedColumn, isFirstDisplayedRow);
		if (cellStyle.Alignment == DataGridViewContentAlignment.MiddleLeft)
		{
			result.X--;
		}
		else if (cellStyle.Alignment == DataGridViewContentAlignment.MiddleCenter)
		{
			result.X++;
		}
		result.Y++;
		return result;
	}
}
