using System;
using System.Drawing;
using System.Windows.Forms;

namespace IBrainChrom2018;

public class LclSummaryGridView : LclCombineCGridView
{
	public const char SpltChar = '\0';

	public const string SpltStr = "\0";

	public DataGridViewColumn[] commonColumns;

	public GvColumnsManager gvComManager = new GvColumnsManager();

	public GvColumnsManager gvDadManager = new GvColumnsManager();

	public GvColumnsManager gvGnlManager = new GvColumnsManager();

	public GvColumnsManager gvGpcManager = new GvColumnsManager();

	public DataGridViewColumn[] hideComColumns;

	public DataGridViewColumn[] hideDadColumns;

	public DataGridViewColumn[] hideGnlColumns;

	public DataGridViewColumn[] hideGpcColumns;

	public DataGridViewColumn[] showComColumns;

	public DataGridViewColumn[] showDadColumns;

	public DataGridViewColumn[] showGnlColumns;

	public DataGridViewColumn[] showGpcColumns;

	public DataGridViewColumn[] smyDadColumns;

	public DataGridViewColumn[] smyGnlColumns;

	public DataGridViewColumn[] smyGpcColumns;

	public void AddComShowLink(int index, string name)
	{
		showComColumns[index] = method_7(commonColumns, name);
	}

	public void AddComTB(int index, string name, int width, int decimalPlaces, StringAlignment alignment)
	{
		method_5(bool_0: true, InstruStyle.LC, index, name, width, decimalPlaces, alignment);
	}

	public void AddSmyShowLink(InstruStyle instruStyle_0, int index, string name)
	{
		switch (instruStyle_0)
		{
		case InstruStyle.GC:
		case InstruStyle.LC:
			showGnlColumns[index] = method_7(smyGnlColumns, name);
			break;
		case InstruStyle.GPC:
			showGpcColumns[index] = method_7(smyGpcColumns, name);
			break;
		case InstruStyle.PDA:
			showDadColumns[index] = method_7(smyDadColumns, name);
			break;
		}
	}

	public void AddSmyTB(InstruStyle instruStyle_0, int index, string name, int width, int decimalPlaces, StringAlignment alignment)
	{
		method_5(bool_0: false, instruStyle_0, index, name, width, decimalPlaces, alignment);
	}

	private void method_5(bool bool_0, InstruStyle instruStyle_0, int int_2, string string_0, int int_3, int int_4, StringAlignment stringAlignment_0)
	{
		DataGridViewColumn dataGridViewColumn = new LclgvTextBoxColumn();
		dataGridViewColumn.Name = string_0;
		dataGridViewColumn.Width = int_3;
		dataGridViewColumn.DefaultCellStyle.Format = "F" + int_4;
		switch (stringAlignment_0)
		{
		case StringAlignment.Near:
			dataGridViewColumn.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
			break;
		case StringAlignment.Center:
			dataGridViewColumn.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
			break;
		default:
			dataGridViewColumn.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
			break;
		}
		if (bool_0)
		{
			commonColumns[int_2] = dataGridViewColumn;
			return;
		}
		switch (instruStyle_0)
		{
		case InstruStyle.GC:
		case InstruStyle.LC:
			smyGnlColumns[int_2] = dataGridViewColumn;
			break;
		case InstruStyle.GPC:
			smyGpcColumns[int_2] = dataGridViewColumn;
			break;
		case InstruStyle.PDA:
			smyDadColumns[int_2] = dataGridViewColumn;
			break;
		}
	}

	public void ArrayComColumns(int int_2)
	{
		Array.Resize(ref commonColumns, int_2);
	}

	public void ArrayComSHColumns(bool show, int int_2)
	{
		if (show)
		{
			Array.Resize(ref showComColumns, int_2);
		}
		else
		{
			Array.Resize(ref hideComColumns, int_2);
		}
	}

	public void ArraySmyColumns(InstruStyle instruStyle_0, int int_2)
	{
		switch (instruStyle_0)
		{
		case InstruStyle.GC:
		case InstruStyle.LC:
			Array.Resize(ref smyGnlColumns, int_2);
			break;
		case InstruStyle.GPC:
			Array.Resize(ref smyGpcColumns, int_2);
			break;
		case InstruStyle.PDA:
			Array.Resize(ref smyDadColumns, int_2);
			break;
		}
	}

	public void ArraySmySHColumns(InstruStyle instruStyle_0, bool show, int int_2)
	{
		switch (instruStyle_0)
		{
		case InstruStyle.GC:
		case InstruStyle.LC:
			if (!show)
			{
				Array.Resize(ref hideGnlColumns, int_2);
			}
			else
			{
				Array.Resize(ref showGnlColumns, int_2);
			}
			break;
		case InstruStyle.GPC:
			if (!show)
			{
				Array.Resize(ref hideGpcColumns, int_2);
			}
			else
			{
				Array.Resize(ref showGpcColumns, int_2);
			}
			break;
		case InstruStyle.PDA:
			if (!show)
			{
				Array.Resize(ref hideDadColumns, int_2);
			}
			else
			{
				Array.Resize(ref showDadColumns, int_2);
			}
			break;
		}
	}

	public string ConvertValFmt(InstruStyle instruStyle_0, string columnName)
	{
		string text = "F0";
		if (instruStyle_0 == InstruStyle.LC || instruStyle_0 == InstruStyle.GC)
		{
			for (int i = 0; i < smyGnlColumns.Length; i++)
			{
				if (smyGnlColumns[i].Name == columnName)
				{
					text = smyGnlColumns[i].DefaultCellStyle.Format;
				}
			}
		}
		if (instruStyle_0 == InstruStyle.GPC)
		{
			for (int j = 0; j < smyGpcColumns.Length; j++)
			{
				if (smyGpcColumns[j].Name == columnName)
				{
					text = smyGpcColumns[j].DefaultCellStyle.Format;
				}
			}
		}
		if (instruStyle_0 == InstruStyle.PDA)
		{
			for (int k = 0; k < smyDadColumns.Length; k++)
			{
				if (smyDadColumns[k].Name == columnName)
				{
					text = smyDadColumns[k].DefaultCellStyle.Format;
				}
			}
		}
		string text2 = "";
		if (text.StartsWith("F") || text.StartsWith("f"))
		{
			int num = int.Parse(text.Remove(0, 1));
			text2 = "0";
			if (num != 0)
			{
				text2 += ".";
			}
			for (int l = 0; l < num; l++)
			{
				text2 += "0";
			}
		}
		return text2;
	}

	private DataGridViewColumn[] method_6(ref DataGridViewColumn[] dataGridViewColumn_0)
	{
		for (int i = 0; i < dataGridViewColumn_0.Length; i++)
		{
			if (dataGridViewColumn_0[i] == null)
			{
				dataGridViewColumn_0[i--] = dataGridViewColumn_0[dataGridViewColumn_0.Length - 1];
				Array.Resize(ref dataGridViewColumn_0, dataGridViewColumn_0.Length - 1);
			}
		}
		return dataGridViewColumn_0;
	}

	public void FinishComHideLinks()
	{
		method_6(ref commonColumns);
		ArrayComSHColumns(show: false, commonColumns.Length - showComColumns.Length);
		int num = 0;
		for (int i = 0; i < commonColumns.Length; i++)
		{
			if (method_7(showComColumns, commonColumns[i].Name) == null)
			{
				hideComColumns[num++] = commonColumns[i];
			}
		}
	}

	public void FinishSmyHideLinks(InstruStyle instruStyle_0)
	{
		DataGridViewColumn[] array = null;
		DataGridViewColumn[] array2 = null;
		DataGridViewColumn[] array3 = null;
		switch (instruStyle_0)
		{
		case InstruStyle.GC:
		case InstruStyle.LC:
			array = smyGnlColumns;
			array2 = method_6(ref showGnlColumns);
			break;
		case InstruStyle.GPC:
			array = smyGpcColumns;
			array2 = method_6(ref showGpcColumns);
			break;
		case InstruStyle.PDA:
			array = smyDadColumns;
			array2 = method_6(ref showDadColumns);
			break;
		}
		ArraySmySHColumns(instruStyle_0, show: false, array.Length - array2.Length);
		switch (instruStyle_0)
		{
		case InstruStyle.GC:
		case InstruStyle.LC:
			array3 = hideGnlColumns;
			break;
		case InstruStyle.GPC:
			array3 = hideGpcColumns;
			break;
		case InstruStyle.PDA:
			array3 = hideDadColumns;
			break;
		}
		int num = 0;
		for (int i = 0; i < array.Length; i++)
		{
			if (method_7(array2, array[i].Name) == null)
			{
				array3[num++] = array[i];
			}
		}
	}

	public override bool LoadFromManager()
	{
		bool flag = gvComManager.ShowColsCount != 0 && gvComManager.colFormats.Length == commonColumns.Length;
		bool flag2 = gvGnlManager.ShowColsCount != 0 && gvGnlManager.colFormats.Length == smyGnlColumns.Length;
		bool flag3 = gvGpcManager.ShowColsCount != 0 && gvGpcManager.colFormats.Length == smyGpcColumns.Length;
		bool flag4 = gvDadManager.ShowColsCount != 0 && gvDadManager.colFormats.Length == smyDadColumns.Length;
		if (flag)
		{
			for (int i = 0; i < commonColumns.Length; i++)
			{
				commonColumns[i].DefaultCellStyle.Format = gvComManager.colFormats[i];
			}
			ArrayComSHColumns(show: true, gvComManager.ShowColsCount);
			for (int j = 0; j < gvComManager.ShowColsCount; j++)
			{
				AddComShowLink(j, gvComManager.showCols[j]);
			}
			FinishComHideLinks();
		}
		if (flag2)
		{
			for (int k = 0; k < smyGnlColumns.Length; k++)
			{
				smyGnlColumns[k].DefaultCellStyle.Format = gvGnlManager.colFormats[k];
			}
			InstruStyle instruStyle_ = InstruStyle.LC;
			ArraySmySHColumns(InstruStyle.LC, show: true, gvGnlManager.ShowColsCount);
			for (int l = 0; l < gvGnlManager.ShowColsCount; l++)
			{
				AddSmyShowLink(instruStyle_, l, gvGnlManager.showCols[l]);
			}
			FinishSmyHideLinks(instruStyle_);
		}
		if (flag3)
		{
			for (int m = 0; m < smyGpcColumns.Length; m++)
			{
				smyGpcColumns[m].DefaultCellStyle.Format = gvGpcManager.colFormats[m];
			}
			InstruStyle instruStyle_2 = InstruStyle.GPC;
			ArraySmySHColumns(InstruStyle.GPC, show: true, gvGpcManager.ShowColsCount);
			for (int n = 0; n < gvGpcManager.ShowColsCount; n++)
			{
				AddSmyShowLink(instruStyle_2, n, gvGpcManager.showCols[n]);
			}
			FinishSmyHideLinks(instruStyle_2);
		}
		if (flag4)
		{
			for (int num = 0; num < smyDadColumns.Length; num++)
			{
				smyDadColumns[num].DefaultCellStyle.Format = gvDadManager.colFormats[num];
			}
			InstruStyle instruStyle_3 = InstruStyle.PDA;
			ArraySmySHColumns(InstruStyle.PDA, show: true, gvDadManager.ShowColsCount);
			for (int num2 = 0; num2 < gvDadManager.ShowColsCount; num2++)
			{
				AddSmyShowLink(instruStyle_3, num2, gvDadManager.showCols[num2]);
			}
			FinishSmyHideLinks(instruStyle_3);
		}
		return flag || flag2 || flag3 || flag4;
	}

	private DataGridViewColumn method_7(DataGridViewColumn[] dataGridViewColumn_0, string string_0)
	{
		for (int i = 0; i < dataGridViewColumn_0.Length; i++)
		{
			if (dataGridViewColumn_0[i] != null && dataGridViewColumn_0[i].Name.Equals(string_0))
			{
				return dataGridViewColumn_0[i];
			}
		}
		return null;
	}

	public override void SaveToManager()
	{
		if (showComColumns != null)
		{
			Array.Resize(ref gvComManager.colFormats, commonColumns.Length);
			for (int i = 0; i < commonColumns.Length; i++)
			{
				gvComManager.colFormats[i] = commonColumns[i].DefaultCellStyle.Format;
			}
			Array.Resize(ref gvComManager.showCols, showComColumns.Length);
			for (int j = 0; j < showComColumns.Length; j++)
			{
				gvComManager.showCols[j] = showComColumns[j].Name;
			}
		}
		if (showGnlColumns != null)
		{
			Array.Resize(ref gvGnlManager.colFormats, smyGnlColumns.Length);
			for (int k = 0; k < smyGnlColumns.Length; k++)
			{
				gvGnlManager.colFormats[k] = smyGnlColumns[k].DefaultCellStyle.Format;
			}
			Array.Resize(ref gvGnlManager.showCols, showGnlColumns.Length);
			for (int l = 0; l < showGnlColumns.Length; l++)
			{
				gvGnlManager.showCols[l] = showGnlColumns[l].Name;
			}
		}
		if (showGpcColumns != null)
		{
			Array.Resize(ref gvGpcManager.colFormats, smyGpcColumns.Length);
			for (int m = 0; m < smyGpcColumns.Length; m++)
			{
				gvGpcManager.colFormats[m] = smyGpcColumns[m].DefaultCellStyle.Format;
			}
			Array.Resize(ref gvGpcManager.showCols, showGpcColumns.Length);
			for (int n = 0; n < showGpcColumns.Length; n++)
			{
				gvGpcManager.showCols[n] = showGpcColumns[n].Name;
			}
		}
		if (showDadColumns != null)
		{
			Array.Resize(ref gvDadManager.colFormats, smyDadColumns.Length);
			for (int num = 0; num < smyDadColumns.Length; num++)
			{
				gvDadManager.colFormats[num] = smyDadColumns[num].DefaultCellStyle.Format;
			}
			Array.Resize(ref gvDadManager.showCols, showDadColumns.Length);
			for (int num2 = 0; num2 < showDadColumns.Length; num2++)
			{
				gvDadManager.showCols[num2] = showDadColumns[num2].Name;
			}
		}
	}

	public void W_cmpd(InstruStyle instruStyle_0, string cmpdName)
	{
		DataGridViewColumn[] array = null;
		switch (instruStyle_0)
		{
		case InstruStyle.GC:
		case InstruStyle.LC:
			array = showGnlColumns;
			break;
		case InstruStyle.GPC:
			array = showGpcColumns;
			break;
		case InstruStyle.PDA:
			array = showDadColumns;
			break;
		}
		CombineC combineC = new CombineC();
		combineC.text = cmpdName;
		combineC.indices = new int[array.Length];
		combineC.begDisplayIndex = base.ColumnCount;
		combineC.numDisplayIndices = combineC.indices.Length;
		for (int i = 0; i < array.Length; i++)
		{
			string columnName = array[i].Name + "\0" + cmpdName;
			DataGridViewColumn dataGridViewColumn = AddLclTextBoxColumn(columnName, array[i].Width);
			dataGridViewColumn.HeaderText = Lang.PS(array[i].HeaderText);
			dataGridViewColumn.DefaultCellStyle = array[i].DefaultCellStyle.Clone();
			combineC.indices[i] = dataGridViewColumn.Index;
		}
		AddCombineC(combineC);
	}

	public void W_cmpds(InstruStyle instruStyle_0, string[] cmpdNames)
	{
		DataGridViewColumn[] array = null;
		switch (instruStyle_0)
		{
		case InstruStyle.GC:
		case InstruStyle.LC:
			array = showGnlColumns;
			break;
		case InstruStyle.GPC:
			array = showGpcColumns;
			break;
		case InstruStyle.PDA:
			array = showDadColumns;
			break;
		}
		for (int i = 0; i < array.Length; i++)
		{
			CombineC combineC = new CombineC();
			combineC.text = array[i].HeaderText;
			combineC.indices = new int[cmpdNames.Length];
			combineC.begDisplayIndex = base.ColumnCount;
			combineC.numDisplayIndices = cmpdNames.Length;
			for (int j = 0; j < cmpdNames.Length; j++)
			{
				string columnName = array[i].Name + "\0" + cmpdNames[j];
				DataGridViewColumn dataGridViewColumn = AddLclTextBoxColumn(columnName, array[i].Width);
				dataGridViewColumn.HeaderText = cmpdNames[j];
				dataGridViewColumn.DefaultCellStyle = array[i].DefaultCellStyle.Clone();
				combineC.indices[j] = dataGridViewColumn.Index;
			}
			AddCombineC(combineC);
		}
	}

	public void W_showComColumns()
	{
		base.ColumnCount = 0;
		for (int i = 0; i < showComColumns.Length; i++)
		{
			base.Columns.Add(showComColumns[i]);
		}
		for (int j = 0; j < showComColumns.Length; j++)
		{
			showComColumns[j].DisplayIndex = j;
		}
		ClearCombineCs();
	}
}
