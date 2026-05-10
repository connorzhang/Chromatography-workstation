using System;
using System.Runtime.InteropServices;

namespace IBrainChrom2018;

[Serializable]
[StructLayout(LayoutKind.Sequential)]
public class EventTable
{
	public EventTableRow[] fRowList;

	public EventTableRow this[int index]
	{
		get
		{
			return fRowList[index];
		}
		set
		{
			fRowList[index] = value;
		}
	}

	public float this[int iRow, int iCol]
	{
		get
		{
			return fRowList[iRow][iCol];
		}
		set
		{
			fRowList[iRow][iCol] = value;
		}
	}

	public EventTable(int nrow)
	{
		fRowList = new EventTableRow[nrow];
		for (int i = 0; i < nrow; i++)
		{
			fRowList[i] = new EventTableRow();
		}
	}

	public EventTable()
	{
		fRowList = new EventTableRow[0];
	}

	public int GetLength(int index)
	{
		if (fRowList == null)
		{
			return 0;
		}
		if (index == 0)
		{
			return fRowList.Length;
		}
		if (fRowList.Length == 0)
		{
			return 0;
		}
		return fRowList[0].fRowList.Length;
	}
}
