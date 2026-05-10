using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Threading;
using System.Windows.Forms;
using IBrainChrom2018.Unit;

namespace IBrainChrom2018;

public class LclGridView : DataGridView
{
	public delegate void ChangeColor(int rowIndex);

	private SystemParam sysParam = SystemParam.Create();

	public const uint MSG_CHANGECOLOR = 2048u;

	public const int rowHeadersWidth = 25;

	protected bool cellMouseDown;

	private Color color_0 = Color.Black;

	private int[] int_0 = new int[0];

	private int int_1;

	public int frozenNum;

	public GvColumnsManager gvColumnsManager = new GvColumnsManager();

	protected StringFormat headerFormat = new StringFormat();

	public DataGridViewColumn[] hideColumns;

	public int hideRowIndex = -1;

	public static Bitmap imgContextButton;

	public static Bitmap imgUnContextButton;

	public static int imgWholeWidth = -1;

	public static Pen penCellEdge = new Pen(Color.Gray);

	protected StringFormat rowHeaderFormat = new StringFormat();

	protected static SolidBrush sbCell = new SolidBrush(Color.Black);

	public bool shieldBeginEdit;

	public DataGridViewColumn[] showColumns;

	public StringAlignment textBox_dftAligement = StringAlignment.Far;

	public int textBox_dftDecimalPlaces = Class49.int_8;

	public bool textBox_dftReadOnly;

	protected StringFormat valueFormat = new StringFormat();

	private ChangeColor changeColor_0;

	public Color CharacterHeaderColor
	{
		get
		{
			return color_0;
		}
		set
		{
			color_0 = value;
			for (int i = 0; i < int_0.Length; i++)
			{
				InvalidateCell(int_0[i], -1);
			}
		}
	}

	public event ChangeColor OnChangeColor
	{
		add
		{
			ChangeColor changeColor = changeColor_0;
			ChangeColor changeColor2;
			do
			{
				changeColor2 = changeColor;
				ChangeColor value2 = (ChangeColor)Delegate.Combine(changeColor2, value);
				changeColor = Interlocked.CompareExchange(ref changeColor_0, value2, changeColor2);
			}
			while (changeColor != changeColor2);
		}
		remove
		{
			ChangeColor changeColor = changeColor_0;
			ChangeColor changeColor2;
			do
			{
				changeColor2 = changeColor;
				ChangeColor value2 = (ChangeColor)Delegate.Remove(changeColor2, value);
				changeColor = Interlocked.CompareExchange(ref changeColor_0, value2, changeColor2);
			}
			while (changeColor != changeColor2);
		}
	}

	public void SetimgContextButton(Bitmap imgCBtn)
	{
		imgContextButton = imgCBtn;
	}

	public void SetimgUnContextButton(Bitmap UnimgCBtn)
	{
		imgUnContextButton = UnimgCBtn;
	}

	public LclGridView()
	{
		textBox_dftDecimalPlaces = Class49.int_8;
		DoubleBuffered = true;
		SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint, value: true);
		base.BackgroundColor = Color.AliceBlue;
		base.BorderStyle = BorderStyle.FixedSingle;
		base.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
		base.ShowCellToolTips = false;
		base.AllowUserToAddRows = false;
		base.AllowUserToDeleteRows = false;
		base.EditMode = DataGridViewEditMode.EditProgrammatically;
		base.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
		base.RowHeadersWidth = 25;
		base.RowTemplate.Height = 16;
		base.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
		base.AllowUserToResizeRows = false;
		headerFormat.Alignment = StringAlignment.Center;
		headerFormat.LineAlignment = StringAlignment.Center;
		rowHeaderFormat.Alignment = StringAlignment.Center;
		penCellEdge.DashStyle = DashStyle.Dot;
		valueFormat.LineAlignment = StringAlignment.Center;
		valueFormat.FormatFlags = StringFormatFlags.NoWrap;
		if (imgWholeWidth == -1)
		{
			imgContextButton = SystemBitmapResource9.smethod_5();
			imgUnContextButton = SystemBitmapResource9.smethod_18();
			if (imgContextButton == null)
			{
				imgWholeWidth = 15;
			}
			else
			{
				imgWholeWidth = imgContextButton.Width + 2;
			}
		}
		base.DataError += LclGridView_DataError;
	}

	public void AddColorHeader(int columnIndex)
	{
		Array.Resize(ref int_0, int_0.Length + 1);
		int_0[int_0.Length - 1] = columnIndex;
	}

	private DataGridViewColumn method_0(DataGridViewColumn dataGridViewColumn_0, string string_0, int Width, StringAlignment stringAlignment_0)
	{
		dataGridViewColumn_0.Name = string_0;
		int index = base.Columns.Add(dataGridViewColumn_0);
		base.Columns[index].Width = Width;
		switch (stringAlignment_0)
		{
		case StringAlignment.Near:
			base.Columns[index].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
			return dataGridViewColumn_0;
		case StringAlignment.Center:
			base.Columns[index].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
			return dataGridViewColumn_0;
		default:
			base.Columns[index].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
			return dataGridViewColumn_0;
		}
	}

	public LclgvCheckBoxColumn AddLclCheckBoxColumn(string columnName, int width)
	{
		DataGridViewColumn dataGridViewColumn_ = new LclgvCheckBoxColumn();
		return (LclgvCheckBoxColumn)method_0(dataGridViewColumn_, columnName, width, StringAlignment.Center);
	}

	public LclgvColorColumn AddLclColorColumn(string columnName, int width)
	{
		return (LclgvColorColumn)method_0(new LclgvColorColumn
		{
			ReadOnly = true
		}, columnName, width, StringAlignment.Center);
	}

	public LclgvComboBoxColumn AddLclComboBoxColumn(string columnName, int width)
	{
		DataGridViewColumn dataGridViewColumn_ = new LclgvComboBoxColumn();
		return (LclgvComboBoxColumn)method_0(dataGridViewColumn_, columnName, width, StringAlignment.Near);
	}

	public LclgvIconColumn AddLclgvIconColumn(string columnName, int width)
	{
		return (LclgvIconColumn)method_0(new LclgvIconColumn
		{
			ReadOnly = true,
			Resizable = DataGridViewTriState.False
		}, columnName, width, StringAlignment.Center);
	}

	public LclgvSeqStatusColumn AddLclgvSeqStatusColumn(string columnName, int width)
	{
		return (LclgvSeqStatusColumn)method_0(new LclgvSeqStatusColumn
		{
			ReadOnly = true
		}, columnName, width, StringAlignment.Center);
	}

	public LclgvItgOprtColumn AddLclItgOprtColumn(string columnName, int width)
	{
		DataGridViewColumn dataGridViewColumn_ = new LclgvItgOprtColumn();
		return (LclgvItgOprtColumn)method_0(dataGridViewColumn_, columnName, width, StringAlignment.Near);
	}

	public LclgvRespStyleColumn AddLclRespStyleColumn(string columnName, int width)
	{
		DataGridViewColumn dataGridViewColumn_ = new LclgvRespStyleColumn();
		return (LclgvRespStyleColumn)method_0(dataGridViewColumn_, columnName, width, StringAlignment.Near);
	}

	public DataGridViewComboBoxColumn AddLclCpmdNameColumn(string columnName, int width)
	{
		DataGridViewComboBoxColumn dataGridViewColumn_ = new LclgvCpmdNameColumn();
		return (DataGridViewComboBoxColumn)method_0(dataGridViewColumn_, columnName, width, StringAlignment.Near);
	}

	public LclgvTextBoxColumn AddLclTextBoxColumn(string columnName, int width)
	{
		return AddLclTextBoxColumn(columnName, width, textBox_dftDecimalPlaces, textBox_dftAligement, textBox_dftReadOnly);
	}

	public LclgvTextBoxColumn AddLclTextBoxColumn(string columnName, int width, StringAlignment alignment)
	{
		return AddLclTextBoxColumn(columnName, width, textBox_dftDecimalPlaces, alignment, textBox_dftReadOnly);
	}

	public LclgvTextBoxColumn AddLclTextBoxColumn(string columnName, int width, int decimalPlaces)
	{
		return AddLclTextBoxColumn(columnName, width, decimalPlaces, textBox_dftAligement, textBox_dftReadOnly);
	}

	public LclgvTextBoxColumn AddLclTextBoxColumn(string columnName, int width, int decimalPlaces, bool readOnly)
	{
		return AddLclTextBoxColumn(columnName, width, decimalPlaces, textBox_dftAligement, readOnly);
	}

	public LclgvTextBoxColumn AddLclTextBoxColumn(string columnName, int width, int decimalPlaces, StringAlignment alignment)
	{
		return AddLclTextBoxColumn(columnName, width, decimalPlaces, alignment, textBox_dftReadOnly);
	}

	public LclgvTextBoxColumn AddLclTextBoxColumn(string columnName, int width, int decimalPlaces, StringAlignment alignment, bool readOnly)
	{
		return (LclgvTextBoxColumn)method_0(new LclgvTextBoxColumn
		{
			DefaultCellStyle = 
			{
				Format = "F" + decimalPlaces
			},
			ReadOnly = readOnly
		}, columnName, width, alignment);
	}

	public LclgvTextBoxCtxBtnColumn AddLclTextBoxCtxBtnColumn(string columnName, int width, object linkObject)
	{
		return AddLclTextBoxCtxBtnColumn(columnName, width, StringAlignment.Near, linkObject);
	}

	public LclgvTextBoxCtxBtnColumn AddLclTextBoxCtxBtnColumn(string columnName, int width, StringAlignment alignment, object linkObject)
	{
		DataGridViewColumn dataGridViewColumn_ = new LclgvTextBoxCtxBtnColumn(linkObject);
		return (LclgvTextBoxCtxBtnColumn)method_0(dataGridViewColumn_, columnName, width, alignment);
	}

	public int[] AdjustSelectedRows(AdjustUpDown adjustUpDown)
	{
		int[] int_ = new int[0];
		if (adjustUpDown == AdjustUpDown.Up)
		{
			for (int i = 1; i < base.Rows.Count; i++)
			{
				if (base.Rows[i].Selected && !base.Rows[i - 1].Selected)
				{
					method_2(i, i - 1);
					method_1(ref int_, i);
					method_1(ref int_, i - 1);
				}
			}
		}
		if (adjustUpDown == AdjustUpDown.Down)
		{
			for (int num = base.Rows.Count - 2; num >= 0; num--)
			{
				if (base.Rows[num].Selected && !base.Rows[num + 1].Selected)
				{
					method_2(num, num + 1);
					method_1(ref int_, num);
					method_1(ref int_, num + 1);
				}
			}
		}
		return int_;
	}

	private void method_1(ref int[] int_2, int int_3)
	{
		for (int i = 0; i < int_2.Length; i++)
		{
			if (int_2[i] == int_3)
			{
				return;
			}
		}
		Array.Resize(ref int_2, int_2.Length + 1);
		int_2[int_2.Length - 1] = int_3;
	}

	public void ArrayShowHideColumns(bool show, int int_2)
	{
		if (show)
		{
			Array.Resize(ref showColumns, int_2);
		}
		else
		{
			Array.Resize(ref hideColumns, int_2);
		}
	}

	public StringAlignment ConvertColAlign(int idxColumn)
	{
		return base.Columns[idxColumn].DefaultCellStyle.Alignment switch
		{
			DataGridViewContentAlignment.MiddleLeft => StringAlignment.Near, 
			DataGridViewContentAlignment.MiddleCenter => StringAlignment.Center, 
			DataGridViewContentAlignment.MiddleRight => StringAlignment.Far, 
			_ => StringAlignment.Center, 
		};
	}

	public StringAlignment ConvertColAlign(string columnName)
	{
		return ConvertColAlign(base.Columns[columnName].Index);
	}

	public string ConvertValFmt(int idxColumn)
	{
		string format = base.Columns[idxColumn].DefaultCellStyle.Format;
		string text = "";
		if (format.StartsWith("F") || format.StartsWith("f"))
		{
			int num = int.Parse(format.Remove(0, 1));
			text = "0";
			if (num != 0)
			{
				text += ".";
			}
			for (int i = 0; i < num; i++)
			{
				text += "0";
			}
		}
		return text;
	}

	public string ConvertValFmt(string columnName)
	{
		return ConvertValFmt(base.Columns[columnName].Index);
	}

	public void DeleteSelectedRows()
	{
		if (base.SelectedRows == null || base.SelectedRows.Count <= 0)
		{
			return;
		}
		int[] array = new int[base.SelectedRows.Count];
		int num = 0;
		for (int num2 = base.Rows.Count - 1; num2 >= 0; num2--)
		{
			if (base.Rows[num2].Selected)
			{
				array[num++] = num2;
			}
		}
		for (int i = 0; i < array.Length; i++)
		{
			base.Rows.RemoveAt(array[i]);
		}
		for (int num3 = base.Rows.Count - 1; num3 >= 0; num3--)
		{
			base.Rows[num3].Selected = false;
		}
	}

	public static void DrawCellEdge(Graphics graphics_0, Pen pen_0, Rectangle bounds, bool offset)
	{
		if (offset)
		{
			bounds.Offset(-1, -1);
		}
		Point[] points = new Point[3]
		{
			new Point(bounds.Left, bounds.Bottom),
			new Point(bounds.Right, bounds.Bottom),
			new Point(bounds.Right, bounds.Top)
		};
		graphics_0.DrawLines(pen_0, points);
	}

	protected void drawHeaderBackground(Graphics graphics_0, Rectangle rectangle_0, int offLeft, int offTop, int offWidth, int offHeight)
	{
		graphics_0.FillRectangle(Brushes.Gainsboro, rectangle_0);
		rectangle_0.X += offLeft;
		rectangle_0.Y += offTop;
		rectangle_0.Width += offWidth;
		rectangle_0.Height += offHeight;
		DrawCellEdge(graphics_0, Pens.Gray, rectangle_0, offset: false);
	}

	public static void drawValue(Graphics graphics_0, object value, Rectangle rectangle_0, int offLeft, int offTop, int offWidth, int offHeight, Font font, Color color, StringFormat strFormat)
	{
		if (value != null)
		{
			rectangle_0.X += offLeft;
			rectangle_0.Y += offTop;
			rectangle_0.Width += offWidth;
			rectangle_0.Height += offHeight;
			sbCell.Color = color;
			graphics_0.DrawString(value.ToString(), font, sbCell, rectangle_0, strFormat);
		}
	}

	private void method_2(int int_2, int int_3)
	{
		object tag = base.Rows[int_2].Tag;
		base.Rows[int_2].Tag = base.Rows[int_3].Tag;
		base.Rows[int_3].Tag = tag;
		bool selected = base.Rows[int_2].Selected;
		base.Rows[int_2].Selected = base.Rows[int_3].Selected;
		base.Rows[int_3].Selected = selected;
		refresh_row(int_2);
		refresh_row(int_3);
	}

	protected int getColumnsWholeWidth(int begColumn, int endColumn)
	{
		int num = 0;
		for (int i = begColumn; i <= endColumn; i++)
		{
			num += base.Columns[i].Width;
		}
		return num;
	}

	public int getVisibleIndex(int displayIndex)
	{
		for (int i = 0; i < base.ColumnCount; i++)
		{
			if (base.Columns[i].Visible && base.Columns[i].DisplayIndex == displayIndex)
			{
				return base.Columns[i].Index;
			}
		}
		return -1;
	}

	public void ini_FinishVisibleColumn()
	{
		ResumeLayout();
		Refresh_ShowHideColumns(AccStyle.Read);
	}

	public void ini_SetFirstVisibleColumn(string name)
	{
		SuspendLayout();
		int_1 = 0;
		for (int i = 0; i < base.ColumnCount; i++)
		{
			if (base.Columns[i].Frozen)
			{
				int_1++;
			}
			else
			{
				base.Columns[i].Visible = false;
			}
		}
		frozenNum = int_1;
		ini_SetNextVisibleColumn(name);
	}

	public void ini_SetNextVisibleColumn(string name)
	{
		if (base.Columns.Contains(name) && !base.Columns[name].Frozen)
		{
			base.Columns[name].Visible = true;
			int num = int_1++;
			if (num < base.Columns.Count)
			{
				base.Columns[name].DisplayIndex = num;
			}
		}
	}

	public void ini_SetNextUnVisibleColumn(string name)
	{
		if (base.Columns.Contains(name) && !base.Columns[name].Frozen)
		{
			base.Columns[name].Visible = false;
			int num = int_1++;
			if (num < base.Columns.Count)
			{
				base.Columns[name].DisplayIndex = num;
			}
		}
	}

	private void LclGridView_DataError(object sender, DataGridViewDataErrorEventArgs e)
	{
	}

	public virtual bool LoadFromManager()
	{
		if (gvColumnsManager.ShowColsCount != 0 && gvColumnsManager.colFormats.Length == base.ColumnCount)
		{
			for (int i = 0; i < base.ColumnCount; i++)
			{
				base.Columns[i].DefaultCellStyle.Format = gvColumnsManager.colFormats[i];
			}
			for (int j = 0; j < gvColumnsManager.ShowColsCount; j++)
			{
				if (j == 0)
				{
					ini_SetFirstVisibleColumn(gvColumnsManager.showCols[j]);
				}
				else
				{
					ini_SetNextVisibleColumn(gvColumnsManager.showCols[j]);
				}
			}
			ini_FinishVisibleColumn();
			return true;
		}
		return false;
	}

	protected override void OnCellMouseDown(DataGridViewCellMouseEventArgs dataGridViewCellMouseEventArgs_0)
	{
		base.OnCellMouseDown(dataGridViewCellMouseEventArgs_0);
		cellMouseDown = true;
		if (!shieldBeginEdit && dataGridViewCellMouseEventArgs_0.RowIndex >= 0 && dataGridViewCellMouseEventArgs_0.ColumnIndex >= 0 && base.CurrentCell != null && !(base.Columns[dataGridViewCellMouseEventArgs_0.ColumnIndex] is DataGridViewComboBoxColumn))
		{
			BeginEdit(selectAll: true);
		}
	}

	protected override void OnCellPainting(DataGridViewCellPaintingEventArgs dataGridViewCellPaintingEventArgs_0)
	{
		Graphics graphics = dataGridViewCellPaintingEventArgs_0.Graphics;
		Rectangle cellBounds = dataGridViewCellPaintingEventArgs_0.CellBounds;
		if (dataGridViewCellPaintingEventArgs_0.RowIndex == -1)
		{
			drawHeaderBackground(graphics, cellBounds, -1, 0, 0, -1);
			drawValue(graphics, dataGridViewCellPaintingEventArgs_0.Value, cellBounds, 0, 2, 0, -2, Font, retHeaderColor(dataGridViewCellPaintingEventArgs_0.ColumnIndex), headerFormat);
			dataGridViewCellPaintingEventArgs_0.Handled = true;
		}
		if (dataGridViewCellPaintingEventArgs_0.ColumnIndex == -1 && dataGridViewCellPaintingEventArgs_0.RowIndex >= 0)
		{
			drawHeaderBackground(graphics, cellBounds, -1, -1, 0, 0);
			cellBounds.Y += 2;
			string s = (dataGridViewCellPaintingEventArgs_0.RowIndex + 1).ToString();
			if (hideRowIndex >= 0 && dataGridViewCellPaintingEventArgs_0.RowIndex >= hideRowIndex)
			{
				s = "";
			}
			graphics.DrawString(s, Font, Brushes.Black, cellBounds, rowHeaderFormat);
			dataGridViewCellPaintingEventArgs_0.Handled = true;
		}
		base.OnCellPainting(dataGridViewCellPaintingEventArgs_0);
	}

	protected override void OnClick(EventArgs eventArgs_0)
	{
		base.OnClick(eventArgs_0);
		if (!cellMouseDown)
		{
			base.CurrentCell = null;
		}
	}

	protected override void OnCurrentCellDirtyStateChanged(EventArgs eventArgs_0)
	{
		base.OnCurrentCellDirtyStateChanged(eventArgs_0);
		if (base.CurrentCell != null)
		{
			CommitEdit(DataGridViewDataErrorContexts.Commit);
		}
	}

	protected override void OnMouseDown(MouseEventArgs mouseEventArgs_0)
	{
		cellMouseDown = false;
		base.OnMouseDown(mouseEventArgs_0);
	}

	protected virtual void refresh_row(int index)
	{
	}

	public void Refresh_ShowHideColumns(AccStyle accStyle)
	{
		switch (accStyle)
		{
		case AccStyle.Read:
		{
			int newSize = 0;
			Array.Resize(ref showColumns, base.ColumnCount);
			for (DataGridViewColumn dataGridViewColumn = base.Columns.GetFirstColumn(DataGridViewElementStates.Visible, DataGridViewElementStates.Frozen); dataGridViewColumn != null; dataGridViewColumn = base.Columns.GetNextColumn(dataGridViewColumn, DataGridViewElementStates.Visible, DataGridViewElementStates.Frozen))
			{
				showColumns[newSize++] = dataGridViewColumn;
			}
			Array.Resize(ref showColumns, newSize);
			newSize = 0;
			Array.Resize(ref hideColumns, base.ColumnCount);
			for (int k = 0; k < base.ColumnCount; k++)
			{
				if (!base.Columns[k].Frozen && !base.Columns[k].Visible)
				{
					hideColumns[newSize++] = base.Columns[k];
				}
			}
			Array.Resize(ref hideColumns, newSize);
			break;
		}
		case AccStyle.Write:
		{
			SuspendLayout();
			for (int i = 0; i < showColumns.Length; i++)
			{
				showColumns[i].Visible = true;
				showColumns[i].DisplayIndex = frozenNum + i;
			}
			for (int j = 0; j < hideColumns.Length; j++)
			{
				hideColumns[j].Visible = false;
				hideColumns[j].DisplayIndex = frozenNum + showColumns.Length + j;
			}
			ResumeLayout();
			break;
		}
		}
	}

	public void ResetColorHeaders()
	{
		Array.Resize(ref int_0, 0);
	}

	protected Color retHeaderColor(int columnIndex)
	{
		if (int_0.Length != 0)
		{
			for (int i = 0; i < int_0.Length; i++)
			{
				if (columnIndex == int_0[i])
				{
					return color_0;
				}
			}
		}
		return Color.Black;
	}

	public virtual void SaveToManager()
	{
		if (showColumns != null)
		{
			Array.Resize(ref gvColumnsManager.colFormats, base.ColumnCount);
			for (int i = 0; i < base.ColumnCount; i++)
			{
				gvColumnsManager.colFormats[i] = base.Columns[i].DefaultCellStyle.Format;
			}
			Array.Resize(ref gvColumnsManager.showCols, showColumns.Length);
			for (int j = 0; j < showColumns.Length; j++)
			{
				gvColumnsManager.showCols[j] = showColumns[j].Name;
			}
		}
	}

	protected override void WndProc(ref Message message_0)
	{
		if ((long)message_0.Msg == 2048)
		{
			if (changeColor_0 != null)
			{
				changeColor_0((int)message_0.WParam);
			}
		}
		else
		{
			base.WndProc(ref message_0);
		}
	}
}
