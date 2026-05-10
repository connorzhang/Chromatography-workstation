using System;
using System.Drawing;
using System.Windows.Forms;

namespace IBrainChrom2018;

public class LclgvTextBoxCtxBtnCell : DataGridViewTextBoxCell
{
	private Size size_0;

	private static Pen pen_0 = new Pen(Color.Black);

	private Point point_0;

	private static SolidBrush solidBrush_0 = new SolidBrush(Color.Black);

	private static StringFormat stringFormat_0 = new StringFormat();

	public override Type EditType => typeof(LclgvTextBoxEditingControl);

	public LclgvTextBoxCtxBtnCell()
	{
		stringFormat_0.FormatFlags = StringFormatFlags.NoWrap;
	}

	protected override void OnMouseUp(DataGridViewCellMouseEventArgs dataGridViewCellMouseEventArgs_0)
	{
		if (LclGridView.imgContextButton == null)
		{
			return;
		}
		Rectangle rectangle = new Rectangle(0, 0, size_0.Width, size_0.Height);
		rectangle.Offset(rectangle.Width - LclGridView.imgContextButton.Width, 0);
		if (!rectangle.Contains(dataGridViewCellMouseEventArgs_0.Location) || base.Tag == null || base.DataGridView.CurrentCell == null)
		{
			return;
		}
		base.DataGridView.BeginEdit(selectAll: false);
		if (base.DataGridView.EditingControl == null || !base.DataGridView.EditingControl.Focus())
		{
			return;
		}
		if (base.Tag is CMS_InfoParasFMT)
		{
			(base.Tag as CMS_InfoParasFMT).Show(base.DataGridView.EditingControl, base.DataGridView.EditingControl.Width + 10, 8, base.DataGridView.EditingControl);
		}
		if (base.Tag is OpenFileDialog)
		{
			OpenFileDialog openFileDialog = base.Tag as OpenFileDialog;
			if (openFileDialog.ShowDialog() == DialogResult.OK)
			{
				base.DataGridView.EditingControl.Text = openFileDialog.FileName;
				base.DataGridView.EndEdit();
			}
		}
	}

	protected override void Paint(Graphics graphics, Rectangle clipBounds, Rectangle cellBounds, int rowIndex, DataGridViewElementStates cellState, object value, object formattedValue, string errorText, DataGridViewCellStyle cellStyle, DataGridViewAdvancedBorderStyle advancedBorderStyle, DataGridViewPaintParts paintParts)
	{
		Rectangle rectangle_ = cellBounds;
		size_0 = rectangle_.Size;
		if (cellStyle.Alignment == DataGridViewContentAlignment.MiddleLeft)
		{
			stringFormat_0.Alignment = StringAlignment.Near;
		}
		else
		{
			stringFormat_0.Alignment = StringAlignment.Center;
		}
		graphics.FillRectangle(Brushes.White, cellBounds);
		if ((cellState & DataGridViewElementStates.Selected) == DataGridViewElementStates.Selected)
		{
			rectangle_.Height--;
			pen_0.Color = cellStyle.SelectionBackColor;
			if (base.RowIndex != 0)
			{
				graphics.DrawLine(pen_0, rectangle_.Left, rectangle_.Top, rectangle_.Right, rectangle_.Top);
			}
			graphics.DrawLine(pen_0, rectangle_.Left, rectangle_.Bottom - 1, rectangle_.Right, rectangle_.Bottom - 1);
			rectangle_.Width -= LclGridView.imgWholeWidth - 1;
			point_0.X = rectangle_.Right;
			point_0.Y = rectangle_.Top;
			solidBrush_0.Color = cellStyle.BackColor;
			graphics.FillRectangle(solidBrush_0, point_0.X, point_0.Y, LclGridView.imgWholeWidth, rectangle_.Height);
			ResourceImageLoad.DrawToGraphic(graphics, LclGridView.imgContextButton, point_0);
			LclGridView.drawValue(graphics, value, rectangle_, 0, 2, 0, 0, cellStyle.Font, cellStyle.ForeColor, stringFormat_0);
		}
		else
		{
			Color color = cellStyle.ForeColor;
			if (ReadOnly)
			{
				color = Color.Gray;
			}
			LclGridView.drawValue(graphics, formattedValue, rectangle_, 0, 2, 0, 0, cellStyle.Font, color, stringFormat_0);
		}
		LclGridView.DrawCellEdge(graphics, LclGridView.penCellEdge, cellBounds, offset: true);
	}

	public override Rectangle PositionEditingPanel(Rectangle cellBounds, Rectangle cellClip, DataGridViewCellStyle cellStyle, bool singleVerticalBorderAdded, bool singleHorizontalBorderAdded, bool isFirstDisplayedColumn, bool isFirstDisplayedRow)
	{
		cellBounds.Width -= LclGridView.imgWholeWidth - 2;
		Rectangle result = base.PositionEditingPanel(cellBounds, cellClip, cellStyle, singleVerticalBorderAdded, singleHorizontalBorderAdded, isFirstDisplayedColumn, isFirstDisplayedRow);
		result.Offset(-1, 1);
		return result;
	}
}
