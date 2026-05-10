using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace IBrainChrom2018;

public class LclgvCheckBoxCell : DataGridViewCheckBoxCell
{
	private static Pen pen_0 = new Pen(Color.Black);

	private Rectangle rectangle_0 = new Rectangle(0, 0, 12, 12);

	private static SolidBrush solidBrush_0 = new SolidBrush(Color.Black);

	private void method_0(Graphics graphics_0, Rectangle rectangle_1)
	{
		LinearGradientBrush brush = new LinearGradientBrush(rectangle_1, Color.DarkGreen, Color.Green, LinearGradientMode.ForwardDiagonal);
		rectangle_1.Offset(2, 2);
		rectangle_1.Width -= 3;
		rectangle_1.Height -= 3;
		graphics_0.FillRectangle(brush, rectangle_1);
	}

	private void method_1(Graphics graphics_0, Rectangle rectangle_1)
	{
		rectangle_1.Inflate(-1, -1);
		graphics_0.DrawRectangle(Pens.LightGray, rectangle_1);
	}

	protected override void OnMouseUp(DataGridViewCellMouseEventArgs dataGridViewCellMouseEventArgs_0)
	{
		if (rectangle_0.Contains(dataGridViewCellMouseEventArgs_0.Location) && base.DataGridView.BeginEdit(selectAll: true))
		{
			if (base.Value == null)
			{
				base.Value = true;
			}
			else
			{
				base.Value = !(bool)base.Value;
			}
			base.DataGridView.EndEdit();
		}
	}

	protected override void Paint(Graphics graphics, Rectangle clipBounds, Rectangle cellBounds, int rowIndex, DataGridViewElementStates elementState, object value, object formattedValue, string errorText, DataGridViewCellStyle cellStyle, DataGridViewAdvancedBorderStyle advancedBorderStyle, DataGridViewPaintParts paintParts)
	{
		Rectangle rect = cellBounds;
		graphics.FillRectangle(Brushes.White, cellBounds);
		solidBrush_0.Color = cellStyle.BackColor;
		rect.Height--;
		graphics.FillRectangle(solidBrush_0, rect);
		if ((elementState & DataGridViewElementStates.Selected) == DataGridViewElementStates.Selected)
		{
			pen_0.Color = cellStyle.SelectionBackColor;
			if (base.RowIndex != 0)
			{
				graphics.DrawLine(pen_0, rect.Left, rect.Top, rect.Right, rect.Top);
			}
			graphics.DrawLine(pen_0, rect.Left, rect.Bottom - 1, rect.Right, rect.Bottom - 1);
		}
		rectangle_0.X = rect.Left + (rect.Width - rectangle_0.Width) / 2;
		rectangle_0.Y = rect.Top + (rect.Height - rectangle_0.Height) / 2;
		graphics.FillRectangle(Brushes.White, rectangle_0);
		graphics.DrawRectangle(Pens.Black, rectangle_0);
		method_1(graphics, rectangle_0);
		if (value != null && (bool)value)
		{
			method_0(graphics, rectangle_0);
		}
		else
		{
			value = false;
		}
		LclGridView.DrawCellEdge(graphics, LclGridView.penCellEdge, cellBounds, offset: true);
		rectangle_0.X -= rect.Left;
		rectangle_0.Y -= rect.Top;
	}
}
