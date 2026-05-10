using System;
using System.Drawing;
using System.Windows.Forms;

namespace IBrainChrom2018;

public class LclgvComboBoxEditingControl : DataGridViewComboBoxEditingControl
{
	private const int int_0 = 7;

	private int int_1;

	private Point point_0;

	private SolidBrush solidBrush_0 = new SolidBrush(Color.Black);

	private StringFormat stringFormat_0 = new StringFormat();

	public LclgvComboBoxEditingControl()
	{
		base.DrawMode = DrawMode.OwnerDrawVariable;
		SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint, value: true);
		DoubleBuffered = true;
		base.ResizeRedraw = true;
		base.DropDownStyle = ComboBoxStyle.DropDownList;
		stringFormat_0.FormatFlags = StringFormatFlags.NoWrap;
		int_1 = base.Bounds.Top - 7;
	}

	public override void ApplyCellStyleToEditingControl(DataGridViewCellStyle dataGridViewCellStyle)
	{
		BackColor = dataGridViewCellStyle.BackColor;
		ForeColor = dataGridViewCellStyle.ForeColor;
		if (dataGridViewCellStyle.Alignment == DataGridViewContentAlignment.MiddleLeft)
		{
			stringFormat_0.Alignment = StringAlignment.Near;
		}
		else
		{
			stringFormat_0.Alignment = StringAlignment.Center;
		}
	}

	protected virtual string itemString(int itemIndex)
	{
		return base.Items[itemIndex].ToString();
	}

	protected override void OnDrawItem(DrawItemEventArgs drawItemEventArgs_0)
	{
		base.Top = int_1;
		if (drawItemEventArgs_0.Index != -1 && (drawItemEventArgs_0.State & DrawItemState.ComboBoxEdit) != DrawItemState.ComboBoxEdit)
		{
			drawItemEventArgs_0.DrawBackground();
			Graphics graphics = drawItemEventArgs_0.Graphics;
			Rectangle bounds = drawItemEventArgs_0.Bounds;
			solidBrush_0.Color = Color.Black;
			if ((drawItemEventArgs_0.State & DrawItemState.Selected) == DrawItemState.Selected)
			{
				solidBrush_0.Color = Color.White;
			}
			bounds.Y++;
			graphics.DrawString(itemString(drawItemEventArgs_0.Index), Font, solidBrush_0, bounds);
		}
	}

	protected override void OnDropDownClosed(EventArgs eventArgs_0)
	{
		base.OnDropDownClosed(eventArgs_0);
		if (SelectedIndex >= 0)
		{
			EditingControlDataGridView.CurrentCell.Value = base.Items[SelectedIndex];
		}
		EditingControlDataGridView.EndEdit();
		Invalidate();
	}

	protected override void OnMeasureItem(MeasureItemEventArgs measureItemEventArgs_0)
	{
		measureItemEventArgs_0.ItemHeight = 14;
	}

	protected override void OnPaint(PaintEventArgs pevent)
	{
		Graphics graphics = pevent.Graphics;
		Rectangle bounds = base.Bounds;
		bounds.Y += 14;
		solidBrush_0.Color = BackColor;
		graphics.FillRectangle(solidBrush_0, bounds);
		bounds.Width -= LclGridView.imgWholeWidth - 2;
		point_0.X = bounds.Right;
		point_0.Y = bounds.Top;
		ResourceImageLoad.DrawToGraphic(graphics, LclGridView.imgContextButton, point_0);
		if (SelectedIndex != -1)
		{
			LclGridView.drawValue(graphics, itemString(SelectedIndex), bounds, 0, 2, 0, 0, Font, ForeColor, stringFormat_0);
		}
	}
}
