using System;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace IBrainChrom2018;

public class LclIntegGridView : LclCombineCGridView
{
	public delegate void AfterEdit();

	public const string igtcolGroup = "Group";

	public const string igtcolOprtStyle = "ChromOprt";

	public const string igtcolTimeA = "TimeA";

	public const string igtcolTimeB = "TimeB";

	public const string igtcolUnit = "Unit";

	public const string igtcolValue = "Value";

	private const string string_0 = "谱图处理";

	private const string string_1 = "组\n-";

	private const string string_2 = "开始时间\n[min]";

	private const string string_3 = "结束时间\n[min]";

	private const string string_4 = "值";

	private const string string_5 = "合并残留";

	private const string string_6 = "保持残留";

	private const string string_7 = "Chromatogram\nOperation";

	private const string string_8 = "Group\n-";

	private const string string_9 = "Time A\n[min]";

	private const string string_10 = "Time B\n[min]";

	private const string string_11 = "Value";

	private const string string_12 = "Merge Residual";

	private const string string_13 = "Remain Residual";

	private LclgvComboBoxColumn lclgvComboBoxColumn_0;

	private LclgvItgOprtColumn lclgvItgOprtColumn_0;

	private LclgvTextBoxColumn lclgvTextBoxColumn_0;

	private LclgvTextBoxColumn lclgvTextBoxColumn_1;

	private LclgvTextBoxColumn lclgvTextBoxColumn_2;

	private LclgvTextBoxColumn lclgvTextBoxColumn_3;

	private int int_2;

	private static Pen pen_0 = new Pen(Color.Black);

	private IntegRow integRow_0;

	private AfterEdit afterEdit_0;

	public string ColValueFmt => ConvertValFmt(lclgvTextBoxColumn_3.Index);

	public static string sChromOprt => Class49.sysLanguage_0 switch
	{
		SysLanguage.CN => "谱图处理", 
		SysLanguage.EN => "Chromatogram\nOperation", 
		_ => "", 
	};

	public static string sGroup => Class49.sysLanguage_0 switch
	{
		SysLanguage.CN => "组\n-", 
		SysLanguage.EN => "Group\n-", 
		_ => "", 
	};

	private string sPeakV_MergeResidual => Class49.sysLanguage_0 switch
	{
		SysLanguage.CN => "合并残留", 
		SysLanguage.EN => "Merge Residual", 
		_ => "", 
	};

	private string sPeakV_RemainResidual => Class49.sysLanguage_0 switch
	{
		SysLanguage.CN => "保持残留", 
		SysLanguage.EN => "Remain Residual", 
		_ => "", 
	};

	public static string sTimeA => Class49.sysLanguage_0 switch
	{
		SysLanguage.CN => "开始时间[min]", 
		SysLanguage.EN => "Time A\n[min]", 
		_ => "", 
	};

	public static string sTimeB => Class49.sysLanguage_0 switch
	{
		SysLanguage.CN => "结束时间[min]", 
		SysLanguage.EN => "Time B\n[min]", 
		_ => "", 
	};

	public static string sValue => Class49.sysLanguage_0 switch
	{
		SysLanguage.CN => "值", 
		SysLanguage.EN => "Value", 
		_ => "", 
	};

	public event AfterEdit OnAfterEdit
	{
		add
		{
			AfterEdit afterEdit = afterEdit_0;
			AfterEdit afterEdit2;
			do
			{
				afterEdit2 = afterEdit;
				AfterEdit value2 = (AfterEdit)Delegate.Combine(afterEdit2, value);
				afterEdit = Interlocked.CompareExchange(ref afterEdit_0, value2, afterEdit2);
			}
			while (afterEdit != afterEdit2);
		}
		remove
		{
			AfterEdit afterEdit = afterEdit_0;
			AfterEdit afterEdit2;
			do
			{
				afterEdit2 = afterEdit;
				AfterEdit value2 = (AfterEdit)Delegate.Remove(afterEdit2, value);
				afterEdit = Interlocked.CompareExchange(ref afterEdit_0, value2, afterEdit2);
			}
			while (afterEdit != afterEdit2);
		}
	}

	public object gvValue(bool gvUse, IntegRow integRow, string columnName)
	{
		object obj = null;
		if (columnName != null)
		{
			switch (columnName)
			{
			case "Group":
				if ((integRow.oprtStyle == IntegOprtStyle.GroupAdd || integRow.oprtStyle == IntegOprtStyle.GroupDelete) && 'A' <= integRow.group && integRow.group <= 'Z')
				{
					obj = integRow.group;
				}
				break;
			default:
				if (!(columnName == "TimeB"))
				{
					if (columnName == "Value")
					{
						if (integRow.oprtStyle == IntegOprtStyle.DtecDelay || integRow.oprtStyle == IntegOprtStyle.PeakWidth || integRow.oprtStyle == IntegOprtStyle.Threshold || integRow.oprtStyle == IntegOprtStyle.VtVSlope || integRow.oprtStyle == IntegOprtStyle.PkWidth || integRow.oprtStyle == IntegOprtStyle.PkThreshold || integRow.oprtStyle == IntegOprtStyle.PkHalfWidth || integRow.oprtStyle == IntegOprtStyle.PkArea || integRow.oprtStyle == IntegOprtStyle.BsVtV)
						{
							obj = ((!gvUse) ? integRow.value.ToString(ConvertValFmt(lclgvTextBoxColumn_3.Index)) : ((object)integRow.value));
						}
						if (integRow.oprtStyle == IntegOprtStyle.BsTgnt)
						{
							obj = integRow.value.ToString(ConvertValFmt(lclgvTextBoxColumn_3.Index)) + "," + integRow.value2.ToString(ConvertValFmt(lclgvTextBoxColumn_3.Index)) + "," + integRow.value3.ToString(ConvertValFmt(lclgvTextBoxColumn_3.Index)) + "," + integRow.value4.ToString(ConvertValFmt(lclgvTextBoxColumn_3.Index));
						}
					}
					else if (columnName == "Unit")
					{
						obj = ValueUnit(integRow.oprtStyle);
					}
					break;
				}
				goto case "TimeA";
			case "TimeA":
				if (integRow.oprtStyle != IntegOprtStyle.ResetDtecNeg && integRow.oprtStyle != IntegOprtStyle.ClampNeg && integRow.oprtStyle != IntegOprtStyle.PkWidth && integRow.oprtStyle != IntegOprtStyle.PkThreshold && integRow.oprtStyle != IntegOprtStyle.PkAddPosi && integRow.oprtStyle != IntegOprtStyle.PkAddNeg && integRow.oprtStyle != IntegOprtStyle.PkCut && integRow.oprtStyle != IntegOprtStyle.PkHalfWidth && integRow.oprtStyle != IntegOprtStyle.PkArea && integRow.oprtStyle != IntegOprtStyle.PkVale && integRow.oprtStyle != IntegOprtStyle.SolventPeak && integRow.oprtStyle != IntegOprtStyle.FlowMarker && integRow.oprtStyle != IntegOprtStyle.GroupAdd && integRow.oprtStyle != IntegOprtStyle.GroupDelete && integRow.oprtStyle != IntegOprtStyle.BsTgnt && integRow.oprtStyle != IntegOprtStyle.BsVtV && integRow.oprtStyle != IntegOprtStyle.BsValley && integRow.oprtStyle != IntegOprtStyle.BsTogether && integRow.oprtStyle != IntegOprtStyle.BsForwHorz && integRow.oprtStyle != IntegOprtStyle.BsBackHorz && integRow.oprtStyle != IntegOprtStyle.BsFrontTgnt && integRow.oprtStyle != IntegOprtStyle.BsTailTgnt && integRow.oprtStyle != IntegOprtStyle.Noise && integRow.oprtStyle != IntegOprtStyle.Drift)
				{
					break;
				}
				if (gvUse)
				{
					if (columnName == "TimeA")
					{
						obj = integRow.timeA;
					}
					else if (columnName == "TimeB")
					{
						obj = integRow.timeB;
					}
				}
				else if (columnName == "TimeA")
				{
					obj = integRow.timeA.ToString(ConvertValFmt(lclgvTextBoxColumn_0.Index));
				}
				else if (columnName == "TimeB")
				{
					obj = integRow.timeB.ToString(ConvertValFmt(lclgvTextBoxColumn_1.Index));
				}
				break;
			case "ChromOprt":
				obj = ((!gvUse) ? LclgvItgOprtEditingControl.ShowString(integRow.oprtStyle) : ((object)integRow.oprtStyle));
				break;
			}
		}
		if (!gvUse && obj == null)
		{
			obj = "";
		}
		return obj;
	}

	public void InitColumns()
	{
		base.ColumnCount = 0;
		lclgvItgOprtColumn_0 = AddLclItgOprtColumn("ChromOprt", 110);
		lclgvItgOprtColumn_0.Frozen = true;
		lclgvComboBoxColumn_0 = AddLclComboBoxColumn("Group", 110);
		for (char c = 'A'; c <= 'Z'; c = (char)(c + 1))
		{
			lclgvComboBoxColumn_0.Items.Add(c.ToString());
		}
		lclgvTextBoxColumn_0 = AddLclTextBoxColumn("TimeA", 60, StringAlignment.Far);
		lclgvTextBoxColumn_1 = AddLclTextBoxColumn("TimeB", 60, StringAlignment.Far);
		DataGridViewCellStyle dataGridViewCellStyle = lclgvTextBoxColumn_0.DefaultCellStyle;
		string format = (lclgvTextBoxColumn_1.DefaultCellStyle.Format = "F2");
		dataGridViewCellStyle.Format = format;
		lclgvTextBoxColumn_3 = AddLclTextBoxColumn("Value", 50, 4, StringAlignment.Far, readOnly: false);
		lclgvTextBoxColumn_2 = AddLclTextBoxColumn("Unit", 50, StringAlignment.Near);
		lclgvTextBoxColumn_2.ReadOnly = true;
		int_2 = AddCombineC(new CombineC
		{
			indices = new int[2] { lclgvTextBoxColumn_3.Index, lclgvTextBoxColumn_2.Index }
		});
		combineH = base.ColumnHeadersHeight - 1;
		ini_SetFirstVisibleColumn("ChromOprt");
		ini_SetNextVisibleColumn("Group");
		ini_SetNextVisibleColumn("TimeA");
		ini_SetNextVisibleColumn("TimeB");
		ini_SetNextVisibleColumn("Value");
		ini_SetNextVisibleColumn("Unit");
		ini_FinishVisibleColumn();
		AdjustCombineDisInfo(read_refresh: false);
		lclgvItgOprtColumn_0.Items.Clear();
		lclgvItgOprtColumn_0.Items.Add(IntegOprtStyle.ResetDtecNeg);
		lclgvItgOprtColumn_0.Items.Add(IntegOprtStyle.ClampNeg);
		lclgvItgOprtColumn_0.Items.Add(IntegOprtStyle.PkWidth);
		lclgvItgOprtColumn_0.Items.Add(IntegOprtStyle.PkThreshold);
		lclgvItgOprtColumn_0.Items.Add(IntegOprtStyle.PkAddPosi);
		lclgvItgOprtColumn_0.Items.Add(IntegOprtStyle.PkAddNeg);
		lclgvItgOprtColumn_0.Items.Add(IntegOprtStyle.PkCut);
		lclgvItgOprtColumn_0.Items.Add(IntegOprtStyle.PkHalfWidth);
		lclgvItgOprtColumn_0.Items.Add(IntegOprtStyle.PkArea);
		lclgvItgOprtColumn_0.Items.Add(IntegOprtStyle.PkVale);
		lclgvItgOprtColumn_0.Items.Add(IntegOprtStyle.SolventPeak);
		lclgvItgOprtColumn_0.Items.Add(IntegOprtStyle.FlowMarker);
		lclgvItgOprtColumn_0.Items.Add(IntegOprtStyle.GroupAdd);
		lclgvItgOprtColumn_0.Items.Add(IntegOprtStyle.GroupDelete);
		lclgvItgOprtColumn_0.Items.Add(IntegOprtStyle.BsTgnt);
		lclgvItgOprtColumn_0.Items.Add(IntegOprtStyle.BsVtV);
		lclgvItgOprtColumn_0.Items.Add(IntegOprtStyle.BsValley);
		lclgvItgOprtColumn_0.Items.Add(IntegOprtStyle.BsTogether);
		lclgvItgOprtColumn_0.Items.Add(IntegOprtStyle.BsForwHorz);
		lclgvItgOprtColumn_0.Items.Add(IntegOprtStyle.BsBackHorz);
		lclgvItgOprtColumn_0.Items.Add(IntegOprtStyle.BsFrontTgnt);
		lclgvItgOprtColumn_0.Items.Add(IntegOprtStyle.BsTailTgnt);
		lclgvItgOprtColumn_0.Items.Add(IntegOprtStyle.Noise);
		lclgvItgOprtColumn_0.Items.Add(IntegOprtStyle.Drift);
		lclgvItgOprtColumn_0.Items.Add(IntegOprtStyle.DtecDelay);
		lclgvComboBoxColumn_0.Visible = false;
	}

	private void method_5(int int_3, int int_4)
	{
		if (int_3 < 0)
		{
			return;
		}
		if (base.Rows[int_3].Tag == null)
		{
			base.Rows[int_3].Tag = default(IntegRow);
		}
		IntegRow integRow = (IntegRow)base.Rows[int_3].Tag;
		base.Rows[int_3].DefaultCellStyle.ForeColor = (integRow.success ? Color.Black : Color.Red);
		DataGridViewCell dataGridViewCell = base.Rows[int_3].Cells[lclgvItgOprtColumn_0.Index];
		DataGridViewCell dataGridViewCell2 = base.Rows[int_3].Cells[lclgvComboBoxColumn_0.Index];
		DataGridViewCell dataGridViewCell3 = base.Rows[int_3].Cells[lclgvTextBoxColumn_0.Index];
		DataGridViewCell dataGridViewCell4 = base.Rows[int_3].Cells[lclgvTextBoxColumn_1.Index];
		DataGridViewCell dataGridViewCell5 = base.Rows[int_3].Cells[lclgvTextBoxColumn_3.Index];
		DataGridViewCell dataGridViewCell6 = base.Rows[int_3].Cells[lclgvTextBoxColumn_2.Index];
		if (int_4 == lclgvItgOprtColumn_0.Index)
		{
			dataGridViewCell.ReadOnly = false;
			dataGridViewCell2.ReadOnly = true;
			dataGridViewCell3.ReadOnly = true;
			dataGridViewCell4.ReadOnly = true;
			dataGridViewCell5.ReadOnly = true;
			switch (integRow.oprtStyle)
			{
			case IntegOprtStyle.DtecDelay:
			case IntegOprtStyle.PeakWidth:
			case IntegOprtStyle.Threshold:
			case IntegOprtStyle.VtVSlope:
				dataGridViewCell.ReadOnly = true;
				dataGridViewCell5.ReadOnly = false;
				break;
			case IntegOprtStyle.ResetDtecNeg:
			case IntegOprtStyle.ClampNeg:
			case IntegOprtStyle.PkAddPosi:
			case IntegOprtStyle.PkAddNeg:
			case IntegOprtStyle.PkCut:
			case IntegOprtStyle.PkVale:
			case IntegOprtStyle.SolventPeak:
			case IntegOprtStyle.FlowMarker:
			case IntegOprtStyle.BsValley:
			case IntegOprtStyle.BsTogether:
			case IntegOprtStyle.BsForwHorz:
			case IntegOprtStyle.BsBackHorz:
			case IntegOprtStyle.BsFrontTgnt:
			case IntegOprtStyle.BsTailTgnt:
			case IntegOprtStyle.Noise:
			case IntegOprtStyle.Drift:
				dataGridViewCell3.ReadOnly = false;
				dataGridViewCell4.ReadOnly = false;
				break;
			case IntegOprtStyle.PkWidth:
			case IntegOprtStyle.PkThreshold:
			case IntegOprtStyle.PkHalfWidth:
			case IntegOprtStyle.PkArea:
			case IntegOprtStyle.BsTgnt:
			case IntegOprtStyle.BsVtV:
				dataGridViewCell3.ReadOnly = false;
				dataGridViewCell4.ReadOnly = false;
				dataGridViewCell5.ReadOnly = false;
				break;
			case IntegOprtStyle.GroupAdd:
			case IntegOprtStyle.GroupDelete:
				dataGridViewCell2.ReadOnly = false;
				dataGridViewCell3.ReadOnly = false;
				dataGridViewCell4.ReadOnly = false;
				dataGridViewCell5.ReadOnly = false;
				break;
			}
			for (int i = lclgvComboBoxColumn_0.Index; i <= lclgvTextBoxColumn_3.Index; i++)
			{
				InvalidateCell(i, int_3);
			}
		}
		dataGridViewCell.Value = gvValue(gvUse: true, integRow, "ChromOprt");
		object obj = gvValue(gvUse: true, integRow, "Group");
		dataGridViewCell2.Value = ((obj != null) ? lclgvComboBoxColumn_0.Items[(char)obj - 65] : null);
		dataGridViewCell3.Value = gvValue(gvUse: true, integRow, "TimeA");
		dataGridViewCell4.Value = gvValue(gvUse: true, integRow, "TimeB");
		dataGridViewCell5.Value = gvValue(gvUse: true, integRow, "Value");
		dataGridViewCell6.Value = gvValue(gvUse: true, integRow, "Unit");
	}

	public void LoadLanguage()
	{
		switch (Class49.sysLanguage_0)
		{
		case SysLanguage.CN:
			base.Columns["ChromOprt"].HeaderText = "谱图处理";
			base.Columns["Group"].HeaderText = "分组";
			base.Columns["TimeA"].HeaderText = "开始时间\n[min]";
			base.Columns["TimeB"].HeaderText = "结束时间\n[min]";
			base.Columns["Value"].HeaderText = "阈值";
			SetCombineCText(int_2, "值");
			break;
		case SysLanguage.EN:
			base.Columns["ChromOprt"].HeaderText = "Chromatogram\nOperation";
			base.Columns["Group"].HeaderText = "Group\n-";
			base.Columns["TimeA"].HeaderText = "Time A\n[min]";
			base.Columns["TimeB"].HeaderText = "Time B\n[min]";
			base.Columns["Value"].HeaderText = "Value";
			SetCombineCText(int_2, "Value");
			break;
		}
	}

	protected override void OnCellBeginEdit(DataGridViewCellCancelEventArgs dataGridViewCellCancelEventArgs_0)
	{
		base.OnCellBeginEdit(dataGridViewCellCancelEventArgs_0);
		if (base.Rows[dataGridViewCellCancelEventArgs_0.RowIndex].Tag == null)
		{
			base.Rows[dataGridViewCellCancelEventArgs_0.RowIndex].Tag = default(IntegRow);
		}
		integRow_0 = (IntegRow)base.Rows[dataGridViewCellCancelEventArgs_0.RowIndex].Tag;
	}

	protected override void OnCellClick(DataGridViewCellEventArgs dataGridViewCellEventArgs_0)
	{
		base.OnCellClick(dataGridViewCellEventArgs_0);
		if (dataGridViewCellEventArgs_0.RowIndex >= 0 && dataGridViewCellEventArgs_0.ColumnIndex == lclgvTextBoxColumn_3.Index && base.Rows[dataGridViewCellEventArgs_0.RowIndex].Cells[lclgvItgOprtColumn_0.Index].Value is IntegOprtStyle)
		{
			IntegOprtStyle integOprtStyle = (IntegOprtStyle)base.Rows[dataGridViewCellEventArgs_0.RowIndex].Cells[lclgvItgOprtColumn_0.Index].Value;
		}
	}

	protected override void OnCellEndEdit(DataGridViewCellEventArgs dataGridViewCellEventArgs_0)
	{
		base.OnCellEndEdit(dataGridViewCellEventArgs_0);
		IntegRow integRow = RefreshTagFromCells(dataGridViewCellEventArgs_0.RowIndex);
		method_5(dataGridViewCellEventArgs_0.RowIndex, dataGridViewCellEventArgs_0.ColumnIndex);
		if (afterEdit_0 != null && !integRow.Equals(integRow_0))
		{
			integRow.success = true;
			base.Rows[dataGridViewCellEventArgs_0.RowIndex].Tag = integRow;
			afterEdit_0();
		}
	}

	protected override void OnCellPainting(DataGridViewCellPaintingEventArgs dataGridViewCellPaintingEventArgs_0)
	{
		base.OnCellPainting(dataGridViewCellPaintingEventArgs_0);
		if (dataGridViewCellPaintingEventArgs_0.RowIndex >= 0 && dataGridViewCellPaintingEventArgs_0.RowIndex < 3 && lclgvItgOprtColumn_0.Index <= dataGridViewCellPaintingEventArgs_0.ColumnIndex && dataGridViewCellPaintingEventArgs_0.ColumnIndex <= lclgvTextBoxColumn_1.Index && base.Rows[dataGridViewCellPaintingEventArgs_0.RowIndex].Cells[lclgvItgOprtColumn_0.Index].Value is IntegOprtStyle)
		{
			IntegOprtStyle value = (IntegOprtStyle)base.Rows[dataGridViewCellPaintingEventArgs_0.RowIndex].Cells[lclgvItgOprtColumn_0.Index].Value;
			Graphics graphics = dataGridViewCellPaintingEventArgs_0.Graphics;
			Rectangle cellBounds = dataGridViewCellPaintingEventArgs_0.CellBounds;
			LclGridView.sbCell.Color = Color.White;
			graphics.FillRectangle(LclGridView.sbCell, cellBounds);
			Color gray = Color.Gray;
			if ((dataGridViewCellPaintingEventArgs_0.State & DataGridViewElementStates.Selected) == DataGridViewElementStates.Selected)
			{
				cellBounds.Height--;
				pen_0.Color = dataGridViewCellPaintingEventArgs_0.CellStyle.SelectionBackColor;
				if (dataGridViewCellPaintingEventArgs_0.RowIndex != 0)
				{
					graphics.DrawLine(pen_0, cellBounds.Left, cellBounds.Top, cellBounds.Right, cellBounds.Top);
				}
				graphics.DrawLine(pen_0, cellBounds.Left, cellBounds.Bottom - 1, cellBounds.Right, cellBounds.Bottom - 1);
			}
			cellBounds = dataGridViewCellPaintingEventArgs_0.CellBounds;
			graphics.DrawLine(LclGridView.penCellEdge, new Point(cellBounds.Left, cellBounds.Bottom - 1), new Point(cellBounds.Right, cellBounds.Bottom - 1));
			if (dataGridViewCellPaintingEventArgs_0.ColumnIndex == lclgvTextBoxColumn_1.Index)
			{
				graphics.DrawLine(LclGridView.penCellEdge, new Point(cellBounds.Right - 1, cellBounds.Top), new Point(cellBounds.Right - 1, cellBounds.Bottom - 1));
			}
			valueFormat.Alignment = StringAlignment.Near;
			cellBounds.X = base.RowHeadersWidth;
			cellBounds.Width = getColumnsWholeWidth(lclgvItgOprtColumn_0.Index, lclgvTextBoxColumn_1.Index);
			string value2 = LclgvItgOprtEditingControl.ShowString(value);
			LclGridView.drawValue(graphics, value2, cellBounds, 0, 1, 0, 0, Font, gray, valueFormat);
			dataGridViewCellPaintingEventArgs_0.Handled = true;
		}
		if (dataGridViewCellPaintingEventArgs_0.RowIndex >= 0 && dataGridViewCellPaintingEventArgs_0.ColumnIndex == lclgvTextBoxColumn_3.Index && base.Rows[dataGridViewCellPaintingEventArgs_0.RowIndex].Cells[lclgvItgOprtColumn_0.Index].Value is IntegOprtStyle)
		{
			IntegOprtStyle integOprtStyle = (IntegOprtStyle)base.Rows[dataGridViewCellPaintingEventArgs_0.RowIndex].Cells[lclgvItgOprtColumn_0.Index].Value;
		}
	}

	protected override void OnRowsAdded(DataGridViewRowsAddedEventArgs dataGridViewRowsAddedEventArgs_0)
	{
		base.OnRowsAdded(dataGridViewRowsAddedEventArgs_0);
		if (dataGridViewRowsAddedEventArgs_0.RowIndex >= 0 && lclgvItgOprtColumn_0 != null)
		{
			method_5(dataGridViewRowsAddedEventArgs_0.RowIndex, lclgvItgOprtColumn_0.Index);
		}
	}

	public void Refresh(AccStyle accStyle, Integration integration)
	{
		switch (accStyle)
		{
		case AccStyle.Clear:
			base.RowCount = 0;
			break;
		case AccStyle.Read:
		{
			base.RowCount = integration.IntegRows.Length;
			for (int j = 0; j < integration.IntegRows.Length; j++)
			{
				base.Rows[j].Tag = integration.IntegRows[j];
			}
			for (int k = 0; k < base.RowCount; k++)
			{
				if (lclgvItgOprtColumn_0 != null)
				{
					method_5(k, lclgvItgOprtColumn_0.Index);
				}
			}
			break;
		}
		case AccStyle.Write:
		{
			int num = Math.Max(0, base.RowCount);
			integration.IntegRows = new IntegRow[num];
			for (int i = 0; i < integration.IntegRows.Length; i++)
			{
				integration.IntegRows[i] = (IntegRow)base.Rows[i].Tag;
			}
			break;
		}
		}
	}

	protected override void refresh_row(int index)
	{
		base.refresh_row(index);
		method_5(index, lclgvItgOprtColumn_0.Index);
	}

	public IntegRow RefreshTagFromCells(int rowIndex)
	{
		IntegRow integRow = (IntegRow)base.Rows[rowIndex].Tag;
		if (base.Rows[rowIndex].Cells[lclgvItgOprtColumn_0.Index].Value is IntegOprtStyle)
		{
			integRow.oprtStyle = (IntegOprtStyle)base.Rows[rowIndex].Cells[lclgvItgOprtColumn_0.Index].Value;
		}
		if (base.Rows[rowIndex].Cells[lclgvComboBoxColumn_0.Index].Value != null)
		{
			char c = Convert.ToChar(base.Rows[rowIndex].Cells[lclgvComboBoxColumn_0.Index].Value);
			if ('A' <= c && c <= 'Z')
			{
				integRow.group = c;
			}
		}
		integRow.timeA = Class49.String2Float(base.Rows[rowIndex].Cells[lclgvTextBoxColumn_0.Index].Value, integRow.timeA);
		integRow.timeB = Class49.String2Float(base.Rows[rowIndex].Cells[lclgvTextBoxColumn_1.Index].Value, integRow.timeB);
		string text = "";
		if (base.Rows[rowIndex].Cells[lclgvTextBoxColumn_3.Index].Value != null)
		{
			text = base.Rows[rowIndex].Cells[lclgvTextBoxColumn_3.Index].Value.ToString();
		}
		if (text.Contains(","))
		{
			string[] array = text.Split(',');
			integRow.value = Class49.String2Float(array[0], integRow.value);
			integRow.value2 = Class49.String2Float(array[1], integRow.value);
			integRow.value3 = Class49.String2Float(array[2], integRow.value);
			integRow.value4 = Class49.String2Float(array[3], integRow.value);
		}
		else
		{
			integRow.value = Class49.String2Float(text, integRow.value);
		}
		base.Rows[rowIndex].Tag = integRow;
		return integRow;
	}

	public static string ValueUnit(IntegOprtStyle integOprtStyle)
	{
		switch (integOprtStyle)
		{
		default:
			if (integOprtStyle != IntegOprtStyle.PkHalfWidth)
			{
				if (integOprtStyle == IntegOprtStyle.PkArea)
				{
					return Class49.string_15;
				}
				if (integOprtStyle != IntegOprtStyle.Noise)
				{
					if (integOprtStyle != IntegOprtStyle.Drift)
					{
						goto case IntegOprtStyle.PkSlope;
					}
					return Class49.MesureUnit() + "/h";
				}
				break;
			}
			goto case IntegOprtStyle.DtecDelay;
		case IntegOprtStyle.PkSlope:
		case IntegOprtStyle.VtVSlope:
		case IntegOprtStyle.ResetDtecNeg:
		case IntegOprtStyle.ClampNeg:
			return "";
		case IntegOprtStyle.DtecDelay:
		case IntegOprtStyle.PeakWidth:
		case IntegOprtStyle.PkWidth:
			return "min";
		case IntegOprtStyle.Threshold:
		case IntegOprtStyle.PkThreshold:
			break;
		}
		return Class49.string_16;
	}
}
