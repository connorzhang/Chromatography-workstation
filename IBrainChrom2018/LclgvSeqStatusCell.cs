using System.Drawing;
using System.Windows.Forms;

namespace IBrainChrom2018;

public class LclgvSeqStatusCell : DataGridViewTextBoxCell
{
	private static Bitmap bitmap_0;

	private static Bitmap bitmap_1;

	private static Bitmap bitmap_2;

	public InjStatusCheck injStatusCheck = InjStatusCheck.NotCheck;

	public InjStatusMeasure injStatusMeasure;

	private static SolidBrush solidBrush_0 = new SolidBrush(Color.Red);

	private static SolidBrush solidBrush_1 = new SolidBrush(Color.LightBlue);

	private static SolidBrush solidBrush_2 = new SolidBrush(Color.Blue);

	private static SolidBrush solidBrush_3 = new SolidBrush(Color.LightGreen);

	public LclgvSeqStatusCell()
	{
		if (bitmap_0 == null)
		{
			bitmap_0 = SystemBitmapResource9.smethod_2();
			bitmap_1 = SystemBitmapResource9.smethod_3();
			bitmap_2 = SystemBitmapResource9.smethod_4();
		}
	}

	public override object Clone()
	{
		LclgvSeqStatusCell lclgvSeqStatusCell = (LclgvSeqStatusCell)base.Clone();
		lclgvSeqStatusCell.injStatusMeasure = injStatusMeasure;
		lclgvSeqStatusCell.injStatusCheck = injStatusCheck;
		return lclgvSeqStatusCell;
	}

	protected override void Paint(Graphics graphics, Rectangle clipBounds, Rectangle cellBounds, int rowIndex, DataGridViewElementStates cellState, object value, object formattedValue, string errorText, DataGridViewCellStyle cellStyle, DataGridViewAdvancedBorderStyle advancedBorderStyle, DataGridViewPaintParts paintParts)
	{
		Rectangle rect = cellBounds;
		graphics.FillRectangle(Brushes.White, cellBounds);
		if (injStatusMeasure == InjStatusMeasure.NoAnalysis)
		{
			graphics.FillRectangle(solidBrush_3, rect);
		}
		else if (injStatusMeasure == InjStatusMeasure.BeingMeasured)
		{
			graphics.FillRectangle(solidBrush_0, rect);
		}
		else if (injStatusMeasure == InjStatusMeasure.MeasuredOut)
		{
			graphics.FillRectangle(solidBrush_2, rect);
		}
		else if (injStatusMeasure == InjStatusMeasure.MeasuredBySkip)
		{
			graphics.FillRectangle(solidBrush_1, rect);
		}
		if (bitmap_0 != null && bitmap_1 != null && bitmap_2 != null)
		{
			rect.X += (rect.Width - bitmap_0.Width) / 2;
			rect.Y--;
			if (injStatusCheck == InjStatusCheck.CheckOK)
			{
				ResourceImageLoad.DrawToGraphic(graphics, bitmap_0, rect.Location);
			}
			else if (injStatusCheck == InjStatusCheck.HasError)
			{
				ResourceImageLoad.DrawToGraphic(graphics, bitmap_1, rect.Location);
			}
			else if (injStatusCheck == InjStatusCheck.NotCheck)
			{
				ResourceImageLoad.DrawToGraphic(graphics, bitmap_2, rect.Location);
			}
		}
		LclGridView.DrawCellEdge(graphics, LclGridView.penCellEdge, cellBounds, offset: true);
	}
}
