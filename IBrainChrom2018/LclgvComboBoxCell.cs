using System;
using System.Drawing;
using System.Windows.Forms;

namespace IBrainChrom2018;

public class LclgvComboBoxCell : DataGridViewComboBoxCell
{
	protected Rectangle cellBounds;

	private static Pen pen_0 = new Pen(Color.Black);

	private Point point_0;

	private Rectangle rectangle_0;

	private SolidBrush solidBrush_0 = new SolidBrush(Color.Black);

	private StringFormat stringFormat_0 = new StringFormat();

	public override Type EditType => typeof(LclgvComboBoxEditingControl);

	public LclgvComboBoxCell()
	{
		stringFormat_0.FormatFlags = StringFormatFlags.NoWrap;
	}

	protected virtual string cellString(object value)
	{
		if (value == null)
		{
			return "";
		}
		return value.ToString();
	}

	protected override void OnMouseDown(DataGridViewCellMouseEventArgs dataGridViewCellMouseEventArgs_0)
	{
		if (dataGridViewCellMouseEventArgs_0.Button == MouseButtons.Left)
		{
			base.OnMouseDown(dataGridViewCellMouseEventArgs_0);
			Rectangle rectangle = new Rectangle(0, 0, cellBounds.Width, cellBounds.Height);
			if (LclGridView.imgContextButton == null)
			{
				rectangle.Offset(rectangle.Width - 16, 0);
			}
			else
			{
				rectangle.Offset(rectangle.Width - LclGridView.imgContextButton.Width, 0);
			}
			if (rectangle.Contains(dataGridViewCellMouseEventArgs_0.Location))
			{
				base.DataGridView.CurrentCell = this;
				if (base.DataGridView.BeginEdit(selectAll: true))
				{
					(base.DataGridView.EditingControl as LclgvComboBoxEditingControl).DroppedDown = true;
				}
			}
		}
		else
		{
			MouseButtons button = dataGridViewCellMouseEventArgs_0.Button;
		}
	}

	protected override void Paint(Graphics graphics, Rectangle clipBounds, Rectangle cellBounds, int rowIndex, DataGridViewElementStates cellState, object value, object formattedValue, string errorText, DataGridViewCellStyle cellStyle, DataGridViewAdvancedBorderStyle advancedBorderStyle, DataGridViewPaintParts paintParts)
	{
		rectangle_0 = cellBounds;
		this.cellBounds = cellBounds;
		if (cellStyle.Alignment == DataGridViewContentAlignment.MiddleLeft)
		{
			stringFormat_0.Alignment = StringAlignment.Near;
		}
		else
		{
			stringFormat_0.Alignment = StringAlignment.Center;
		}
		solidBrush_0.Color = cellStyle.BackColor;
		graphics.FillRectangle(solidBrush_0, rectangle_0);
		if ((cellState & DataGridViewElementStates.Selected) == DataGridViewElementStates.Selected)
		{
			rectangle_0.Height -= 1;
			pen_0.Color = cellStyle.SelectionBackColor;
			if (base.RowIndex != 0)
			{
				graphics.DrawLine(pen_0, rectangle_0.Left, rectangle_0.Top, rectangle_0.Right, rectangle_0.Top);
			}
			graphics.DrawLine(pen_0, rectangle_0.Left, rectangle_0.Bottom - 1, rectangle_0.Right, rectangle_0.Bottom - 1);
			rectangle_0 = cellBounds;
			rectangle_0.Width -= LclGridView.imgWholeWidth - 1;
			point_0.X = rectangle_0.Right;
			point_0.Y = rectangle_0.Top;
			solidBrush_0.Color = cellStyle.BackColor;
			graphics.FillRectangle(solidBrush_0, point_0.X, point_0.Y, LclGridView.imgWholeWidth, rectangle_0.Height);
			if (ReadOnly)
			{
				ResourceImageLoad.DrawToGraphic(graphics, LclGridView.imgUnContextButton, point_0);
			}
			else
			{
				ResourceImageLoad.DrawToGraphic(graphics, LclGridView.imgContextButton, point_0);
			}
			Color color = cellStyle.ForeColor;
			if (cellStyle.ForeColor == Color.Red)
			{
				color = Color.Red;
			}
			LclGridView.drawValue(graphics, cellString(value), rectangle_0, 0, 2, 0, 0, cellStyle.Font, color, stringFormat_0);
		}
		else
		{
			LclGridView.drawValue(graphics, cellString(value), rectangle_0, 0, 2, 0, 0, cellStyle.Font, cellStyle.ForeColor, stringFormat_0);
		}
		LclGridView.DrawCellEdge(graphics, LclGridView.penCellEdge, cellBounds, offset: true);
	}
}
