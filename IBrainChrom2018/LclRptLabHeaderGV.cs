using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace IBrainChrom2018;

public class LclRptLabHeaderGV : DataGridView
{
	protected bool cellMouseDown;

	public bool grayBkgnd = true;

	private Pen pen_0 = new Pen(Color.Gray, 1f);

	private SolidBrush solidBrush_0 = new SolidBrush(Color.Red);

	private StringFormat stringFormat_0 = new StringFormat();

	public LclRptLabHeaderGV()
	{
		pen_0.DashStyle = DashStyle.Dot;
		stringFormat_0.LineAlignment = StringAlignment.Center;
	}

	protected override void OnCellMouseDown(DataGridViewCellMouseEventArgs dataGridViewCellMouseEventArgs_0)
	{
		base.OnCellMouseDown(dataGridViewCellMouseEventArgs_0);
		cellMouseDown = true;
		BeginEdit(selectAll: false);
	}

	protected override void OnCellPainting(DataGridViewCellPaintingEventArgs dataGridViewCellPaintingEventArgs_0)
	{
		Graphics graphics = dataGridViewCellPaintingEventArgs_0.Graphics;
		Rectangle cellBounds = dataGridViewCellPaintingEventArgs_0.CellBounds;
		int rowIndex = dataGridViewCellPaintingEventArgs_0.RowIndex;
		if (dataGridViewCellPaintingEventArgs_0.Value != null && base.Rows[rowIndex].Tag != null)
		{
			string s = dataGridViewCellPaintingEventArgs_0.Value.ToString();
			LabHdrTag labHdrTag = (LabHdrTag)base.Rows[rowIndex].Tag;
			SizeF sizeF = graphics.MeasureString(s, labHdrTag.font);
			base.Rows[rowIndex].Height = Convert.ToInt32(sizeF.Height + 2f);
			solidBrush_0.Color = (grayBkgnd ? Color.LightGray : Color.White);
			base.Rows[rowIndex].Cells[0].Style.Font = labHdrTag.font;
			base.Rows[rowIndex].Cells[0].Style.ForeColor = labHdrTag.color;
			base.Rows[rowIndex].Cells[0].Style.BackColor = solidBrush_0.Color;
			DataGridViewContentAlignment alignment = DataGridViewContentAlignment.MiddleCenter;
			if (labHdrTag.align == StringAlignment.Near)
			{
				alignment = DataGridViewContentAlignment.MiddleLeft;
			}
			if (labHdrTag.align == StringAlignment.Far)
			{
				alignment = DataGridViewContentAlignment.MiddleRight;
			}
			base.Rows[rowIndex].Cells[0].Style.Alignment = alignment;
			graphics.FillRectangle(solidBrush_0, cellBounds);
			Rectangle cellBounds2 = dataGridViewCellPaintingEventArgs_0.CellBounds;
			cellBounds2.Offset(-1, -1);
			Point[] points = new Point[3]
			{
				new Point(cellBounds2.Left, cellBounds2.Bottom),
				new Point(cellBounds2.Right, cellBounds2.Bottom),
				new Point(cellBounds2.Right, cellBounds2.Top)
			};
			graphics.DrawLines(pen_0, points);
			solidBrush_0.Color = labHdrTag.color;
			stringFormat_0.Alignment = labHdrTag.align;
			graphics.DrawString(s, labHdrTag.font, solidBrush_0, cellBounds, stringFormat_0);
			dataGridViewCellPaintingEventArgs_0.Handled = true;
			base.OnCellPainting(dataGridViewCellPaintingEventArgs_0);
		}
		else
		{
			base.OnCellPainting(dataGridViewCellPaintingEventArgs_0);
		}
	}

	protected override void OnClick(EventArgs eventArgs_0)
	{
		base.OnClick(eventArgs_0);
		if (!cellMouseDown)
		{
			base.CurrentCell = null;
		}
	}

	protected override void OnMouseDown(MouseEventArgs mouseEventArgs_0)
	{
		cellMouseDown = false;
		base.OnMouseDown(mouseEventArgs_0);
	}
}
