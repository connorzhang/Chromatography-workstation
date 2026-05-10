using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Printing;
using System.Windows.Forms;

namespace IBrainChrom2018;

public class DataGridViewPrinter
{
	private DataGridView dataGridView_0;

	private PrintDocument printDocument_0;

	private bool bool_0;

	private bool bool_1;

	private string string_0;

	private Font font_0;

	private Color color_0;

	private bool bool_2;

	private static int int_0;

	private static int int_1;

	private static int int_2;

	private static int int_3;

	private int int_4;

	private int int_5;

	private int int_6;

	private int int_7;

	private int int_8;

	private int int_9;

	private float float_0;

	private float float_1;

	private List<float> list_0;

	private List<float> list_1;

	private float float_2;

	private List<int[]> list_2;

	private List<float> list_3;

	private int int_10;

	public static int flag;

	public static bool hasmorepages;

	public DataGridViewPrinter(DataGridView aDataGridView, PrintDocument aPrintDocument, bool CenterOnPage, bool WithTitle, string aTitleText, Font aTitleFont, Color aTitleColor, bool WithPaging)
	{
		dataGridView_0 = aDataGridView;
		printDocument_0 = aPrintDocument;
		bool_0 = CenterOnPage;
		bool_1 = WithTitle;
		string_0 = aTitleText;
		font_0 = aTitleFont;
		color_0 = aTitleColor;
		bool_2 = WithPaging;
		int_1 = 0;
		int_2 = 0;
		list_0 = new List<float>();
		list_1 = new List<float>();
		list_2 = new List<int[]>();
		list_3 = new List<float>();
		if (!printDocument_0.DefaultPageSettings.Landscape)
		{
			int_4 = printDocument_0.DefaultPageSettings.PaperSize.Width;
			int_5 = printDocument_0.DefaultPageSettings.PaperSize.Height;
		}
		else
		{
			int_5 = printDocument_0.DefaultPageSettings.PaperSize.Width;
			int_4 = printDocument_0.DefaultPageSettings.PaperSize.Height;
		}
		int_6 = printDocument_0.DefaultPageSettings.Margins.Left;
		int_7 = printDocument_0.DefaultPageSettings.Margins.Top;
		int_8 = printDocument_0.DefaultPageSettings.Margins.Right;
		int_9 = printDocument_0.DefaultPageSettings.Margins.Bottom;
		int_0 = 0;
		flag = 0;
		hasmorepages = false;
		int_3 = 0;
	}

	private void method_0(Graphics graphics_0)
	{
		if (int_1 != 0)
		{
			return;
		}
		SizeF sizeF = default(SizeF);
		float_2 = 0f;
		for (int i = 0; i < dataGridView_0.Columns.Count; i++)
		{
			Font font = dataGridView_0.ColumnHeadersDefaultCellStyle.Font;
			if (font == null)
			{
				font = dataGridView_0.DefaultCellStyle.Font;
			}
			sizeF = graphics_0.MeasureString(dataGridView_0.Columns[i].HeaderText, font);
			float width = sizeF.Width;
			float_1 = sizeF.Height;
			for (int j = 0; j < dataGridView_0.Rows.Count; j++)
			{
				font = dataGridView_0.Rows[j].DefaultCellStyle.Font;
				if (font == null)
				{
					font = dataGridView_0.DefaultCellStyle.Font;
				}
				sizeF = graphics_0.MeasureString("Anything", font);
				list_0.Add(sizeF.Height);
				sizeF = graphics_0.MeasureString(dataGridView_0.Rows[j].Cells[i].EditedFormattedValue.ToString(), font);
				if (sizeF.Width > width)
				{
					width = sizeF.Width;
				}
			}
			if (dataGridView_0.Columns[i].Visible)
			{
				float_2 += width;
			}
			list_1.Add(width);
		}
		int num = 0;
		for (int k = 0; k < dataGridView_0.Columns.Count; k++)
		{
			if (!dataGridView_0.Columns[k].Visible)
			{
				continue;
			}
			num = k;
			int count = dataGridView_0.Columns.Count;
			for (k = dataGridView_0.Columns.Count - 1; k >= 0; k--)
			{
				if (dataGridView_0.Columns[k].Visible)
				{
					count = k + 1;
					float num2 = float_2;
					float num3 = (float)int_4 - (float)int_6 - (float)int_8;
					if (float_2 > num3)
					{
						num2 = 0f;
						for (k = 0; k < dataGridView_0.Columns.Count; k++)
						{
							if (dataGridView_0.Columns[k].Visible)
							{
								num2 += list_1[k];
								if (num2 > num3)
								{
									num2 -= list_1[k];
									list_2.Add(new int[2] { num, count });
									list_3.Add(num2);
									num = k;
									num2 = list_1[k];
								}
							}
							count = k + 1;
						}
					}
					list_2.Add(new int[2] { num, count });
					list_3.Add(num2);
					int_10 = 0;
					return;
				}
			}
		}
	}

	private void method_1(Graphics graphics_0)
	{
		float_0 = int_7;
		if (bool_2)
		{
			if (flag > 2 && !hasmorepages)
			{
				int_1 = 0;
				int_3 = 0;
			}
			if (int_3 == int_10)
			{
				int_1++;
			}
			else
			{
				int_1 = 1;
				int_3 = int_10;
			}
			int_2 = int_10 + 1;
			string text = "Page " + int_1 + "-" + int_2;
			StringFormat stringFormat = new StringFormat();
			stringFormat.Trimming = StringTrimming.Word;
			stringFormat.FormatFlags = StringFormatFlags.NoWrap | StringFormatFlags.LineLimit | StringFormatFlags.NoClip;
			stringFormat.Alignment = StringAlignment.Far;
			Font font = new Font("Tahoma", 8f, FontStyle.Regular, GraphicsUnit.Point);
			RectangleF layoutRectangle = new RectangleF(int_6, float_0, (float)int_4 - (float)int_8 - (float)int_6, graphics_0.MeasureString(text, font).Height);
			graphics_0.DrawString(text, font, new SolidBrush(Color.Black), layoutRectangle, stringFormat);
			float_0 += graphics_0.MeasureString(text, font).Height;
		}
		if (bool_1)
		{
			StringFormat stringFormat2 = new StringFormat();
			stringFormat2.Trimming = StringTrimming.Word;
			stringFormat2.FormatFlags = StringFormatFlags.NoWrap | StringFormatFlags.LineLimit | StringFormatFlags.NoClip;
			if (bool_0)
			{
				stringFormat2.Alignment = StringAlignment.Center;
			}
			else
			{
				stringFormat2.Alignment = StringAlignment.Near;
			}
			RectangleF layoutRectangle2 = new RectangleF(int_6, float_0, (float)int_4 - (float)int_8 - (float)int_6, graphics_0.MeasureString(string_0, font_0).Height);
			graphics_0.DrawString(string_0, font_0, new SolidBrush(color_0), layoutRectangle2, stringFormat2);
			float_0 += graphics_0.MeasureString(string_0, font_0).Height;
		}
		float num = int_6;
		if (bool_0)
		{
			num += ((float)int_4 - (float)int_8 - (float)int_6 - list_3[int_10]) / 2f;
		}
		Color foreColor = dataGridView_0.ColumnHeadersDefaultCellStyle.ForeColor;
		if (foreColor.IsEmpty)
		{
			foreColor = dataGridView_0.DefaultCellStyle.ForeColor;
		}
		SolidBrush brush = new SolidBrush(foreColor);
		Color backColor = dataGridView_0.ColumnHeadersDefaultCellStyle.BackColor;
		if (backColor.IsEmpty)
		{
			backColor = dataGridView_0.DefaultCellStyle.BackColor;
		}
		SolidBrush brush2 = new SolidBrush(backColor);
		Pen pen = new Pen(dataGridView_0.GridColor, 1f);
		Font font2 = dataGridView_0.ColumnHeadersDefaultCellStyle.Font;
		if (font2 == null)
		{
			font2 = dataGridView_0.DefaultCellStyle.Font;
		}
		RectangleF rect = new RectangleF(num, float_0, list_3[int_10], float_1);
		graphics_0.FillRectangle(brush2, rect);
		StringFormat stringFormat3 = new StringFormat();
		stringFormat3.Trimming = StringTrimming.Word;
		stringFormat3.FormatFlags = StringFormatFlags.NoWrap | StringFormatFlags.LineLimit | StringFormatFlags.NoClip;
		for (int i = (int)list_2[int_10].GetValue(0); i < (int)list_2[int_10].GetValue(1); i++)
		{
			if (dataGridView_0.Columns[i].Visible)
			{
				float num2 = list_1[i];
				if (dataGridView_0.ColumnHeadersDefaultCellStyle.Alignment.ToString().Contains("Right"))
				{
					stringFormat3.Alignment = StringAlignment.Far;
				}
				else if (dataGridView_0.ColumnHeadersDefaultCellStyle.Alignment.ToString().Contains("Center"))
				{
					stringFormat3.Alignment = StringAlignment.Center;
				}
				else
				{
					stringFormat3.Alignment = StringAlignment.Near;
				}
				graphics_0.DrawString(layoutRectangle: new RectangleF(num, float_0, num2, float_1), s: dataGridView_0.Columns[i].HeaderText, font: font2, brush: brush, format: stringFormat3);
				if (dataGridView_0.RowHeadersBorderStyle != DataGridViewHeaderBorderStyle.None)
				{
					graphics_0.DrawRectangle(pen, num, float_0, num2, float_1);
				}
				num += num2;
			}
		}
		float_0 += float_1;
	}

	private bool method_2(Graphics graphics_0)
	{
		Pen pen = new Pen(dataGridView_0.GridColor, 1f);
		StringFormat stringFormat = new StringFormat();
		stringFormat.Trimming = StringTrimming.Word;
		stringFormat.FormatFlags = StringFormatFlags.NoWrap | StringFormatFlags.LineLimit;
		while (int_0 < dataGridView_0.Rows.Count)
		{
			if (dataGridView_0.Rows[int_0].Visible)
			{
				Font font = dataGridView_0.Rows[int_0].DefaultCellStyle.Font;
				if (font == null)
				{
					font = dataGridView_0.DefaultCellStyle.Font;
				}
				Color foreColor = dataGridView_0.Rows[int_0].DefaultCellStyle.ForeColor;
				if (foreColor.IsEmpty)
				{
					foreColor = dataGridView_0.DefaultCellStyle.ForeColor;
				}
				SolidBrush brush = new SolidBrush(foreColor);
				Color backColor = dataGridView_0.Rows[int_0].DefaultCellStyle.BackColor;
				SolidBrush brush2;
				SolidBrush brush3;
				if (backColor.IsEmpty)
				{
					brush2 = new SolidBrush(dataGridView_0.DefaultCellStyle.BackColor);
					brush3 = new SolidBrush(dataGridView_0.AlternatingRowsDefaultCellStyle.BackColor);
				}
				else
				{
					brush2 = new SolidBrush(backColor);
					brush3 = new SolidBrush(backColor);
				}
				float num = int_6;
				if (bool_0)
				{
					num += ((float)int_4 - (float)int_8 - (float)int_6 - list_3[int_10]) / 2f;
				}
				RectangleF rect = new RectangleF(num, float_0, list_3[int_10], list_0[int_0]);
				if (int_0 % 2 == 0)
				{
					graphics_0.FillRectangle(brush2, rect);
				}
				else
				{
					graphics_0.FillRectangle(brush3, rect);
				}
				for (int i = (int)list_2[int_10].GetValue(0); i < (int)list_2[int_10].GetValue(1); i++)
				{
					if (dataGridView_0.Columns[i].Visible)
					{
						if (dataGridView_0.Columns[i].DefaultCellStyle.Alignment.ToString().Contains("Right"))
						{
							stringFormat.Alignment = StringAlignment.Far;
						}
						else if (dataGridView_0.Columns[i].DefaultCellStyle.Alignment.ToString().Contains("Center"))
						{
							stringFormat.Alignment = StringAlignment.Center;
						}
						else
						{
							stringFormat.Alignment = StringAlignment.Near;
						}
						float num2 = list_1[i];
						graphics_0.DrawString(layoutRectangle: new RectangleF(num, float_0, num2, list_0[int_0]), s: dataGridView_0.Rows[int_0].Cells[i].EditedFormattedValue.ToString(), font: font, brush: brush, format: stringFormat);
						if (dataGridView_0.CellBorderStyle != DataGridViewCellBorderStyle.None)
						{
							graphics_0.DrawRectangle(pen, num, float_0, num2, list_0[int_0]);
						}
						num += num2;
					}
				}
				float_0 += list_0[int_0];
				if ((int)float_0 > int_5 - int_7 - int_9)
				{
					int_0++;
					return true;
				}
			}
			int_0++;
		}
		int_0 = 0;
		int_10++;
		if (int_10 == list_2.Count)
		{
			int_10 = 0;
			return false;
		}
		return true;
	}

	public bool DrawDataGridView(Graphics graphics_0)
	{
		try
		{
			method_0(graphics_0);
			method_1(graphics_0);
			return method_2(graphics_0);
		}
		catch (Exception ex)
		{
			MessageBox.Show("Operation failed: " + ex.Message.ToString(), Application.ProductName + " - Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
			return false;
		}
	}
}
