using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Web.Script.Serialization;
using System.Xml.Serialization;

namespace IBrainChrom2018;

[Serializable]
[StructLayout(LayoutKind.Sequential)]
[XmlInclude(typeof(IntegRow))]
public class Integration : IArrayBase
{
	[NonSerialized]
	private int int_0;

	[NonSerialized]
	private int int_1 = -1;

	private float iniPeakWidth = 0.015f;

	private float iniPkSlope = 1f;

	private float iniTgntAreaF = 3f;

	private float iniTgntLfF = 4f;

	private float iniTgntRtF = 2f;

	private float iniTgntSlopeF = 2.5f;

	private float iniThreshold = 0.2f;

	private float iniVtVSlope;

	private IntegRow[] integRows = new IntegRow[0];

	private IntegStyle integStyle;

	[NonSerialized]
	private IntegRowListSnap[] integRowsListSnap = new IntegRowListSnap[0];

	public int Count => IntegRows.Length;

	[XmlIgnore]
	[ScriptIgnore]
	public float DtecDelay
	{
		get
		{
			for (int i = 3; i < Count; i++)
			{
				if (IntegRows[i].oprtStyle == IntegOprtStyle.DtecDelay)
				{
					return IntegRows[i].value;
				}
			}
			return 0f;
		}
	}

	[XmlIgnore]
	[ScriptIgnore]
	public float PeakWidth
	{
		get
		{
			if (IntegRows.Length == 0)
			{
				return 0f;
			}
			return IntegRows[0].value;
		}
		set
		{
			if (IntegRows.Length != 0)
			{
				IntegRows[0].value = value;
			}
		}
	}

	[XmlIgnore]
	[ScriptIgnore]
	public float PkSlope
	{
		get
		{
			if (IntegRows.Length < 3)
			{
				return 0f;
			}
			return IntegRows[2].value;
		}
		set
		{
			if (IntegRows.Length > 2)
			{
				IntegRows[2].value = value;
			}
		}
	}

	[XmlIgnore]
	[ScriptIgnore]
	public float Threshold
	{
		get
		{
			if (IntegRows.Length < 2)
			{
				return 0f;
			}
			return IntegRows[1].value;
		}
		set
		{
			if (IntegRows.Length > 1)
			{
				IntegRows[1].value = value;
			}
		}
	}

	public float IniPeakWidth
	{
		get
		{
			return iniPeakWidth;
		}
		set
		{
			iniPeakWidth = value;
		}
	}

	public float IniPkSlope
	{
		get
		{
			return iniPkSlope;
		}
		set
		{
			iniPkSlope = value;
		}
	}

	public float IniTgntAreaF
	{
		get
		{
			return iniTgntAreaF;
		}
		set
		{
			iniTgntAreaF = value;
		}
	}

	public float IniTgntLfF
	{
		get
		{
			return iniTgntLfF;
		}
		set
		{
			iniTgntLfF = value;
		}
	}

	public float IniTgntRtF
	{
		get
		{
			return iniTgntRtF;
		}
		set
		{
			iniTgntRtF = value;
		}
	}

	public float IniTgntSlopeF
	{
		get
		{
			return iniTgntSlopeF;
		}
		set
		{
			iniTgntSlopeF = value;
		}
	}

	public float IniThreshold
	{
		get
		{
			return iniThreshold;
		}
		set
		{
			iniThreshold = value;
		}
	}

	public float IniVtVSlope
	{
		get
		{
			return iniVtVSlope;
		}
		set
		{
			iniVtVSlope = value;
		}
	}

	public IntegRow[] IntegRows
	{
		get
		{
			return integRows;
		}
		set
		{
			integRows = value;
		}
	}

	public IntegStyle IntegStyle
	{
		get
		{
			return integStyle;
		}
		set
		{
			integStyle = value;
		}
	}

	public void ResetDeletTime(IntegRow integRow)
	{
		IntegRows[3] = integRow;
		int_1 = -1;
	}

	public void AppendRow(IntegRow integRow)
	{
		if (integRow.oprtStyle == IntegOprtStyle.DtecDelay || integRow.oprtStyle == IntegOprtStyle.Noise || integRow.oprtStyle == IntegOprtStyle.Drift)
		{
			for (int i = 3; i < Count; i++)
			{
				if (IntegRows[i].oprtStyle == integRow.oprtStyle)
				{
					IntegRows[i] = integRow;
					int_1 = -1;
					return;
				}
			}
		}
		int count = Count;
		Array.Resize(ref integRows, count + 1);
		IntegRows[count] = integRow;
		int_1 = -1;
	}

	public void AppendRows(IntegRow[] rows)
	{
		for (int i = 0; i < rows.Length; i++)
		{
			IntegRow integRow = rows[i];
			if (integRow.oprtStyle == IntegOprtStyle.PeakWidth)
			{
				IntegRows[0].value = integRow.value;
			}
			else if (integRow.oprtStyle == IntegOprtStyle.Threshold)
			{
				IntegRows[1].value = integRow.value;
			}
			else if (integRow.oprtStyle == IntegOprtStyle.VtVSlope)
			{
				IntegRows[2].value = integRow.value;
			}
			else
			{
				AppendRow(integRow);
			}
		}
		int_1 = -1;
	}

	public void DeleteRows(int[] indices)
	{
		IntegRow[] array = new IntegRow[Count - indices.Length];
		int num = 0;
		for (int i = 0; i < Count; i++)
		{
			if (!Class49.ValueInArray(indices, i))
			{
				array[num++] = IntegRows[i];
			}
		}
		IntegRows = array;
		int_1 = -1;
	}

	public void InsertNullRow(int int_2)
	{
		Array.Resize(ref integRows, Count + 1);
		for (int num = Count - 1; num > int_2; num--)
		{
			IntegRows[num] = IntegRows[num - 1];
		}
		IntegRows[int_2] = default(IntegRow);
		int_1 = -1;
	}

	public bool Equals(Integration integration_0)
	{
		bool result;
		if (result = integration_0.IntegStyle == IntegStyle && integration_0.IniPeakWidth == IniPeakWidth && integration_0.IniThreshold == IniThreshold && integration_0.IniTgntAreaF == IniTgntAreaF && integration_0.IniTgntSlopeF == IniTgntSlopeF && integration_0.IniVtVSlope == IniVtVSlope)
		{
			if (IntegRows.Length != integration_0.IntegRows.Length)
			{
				return false;
			}
			for (int i = 0; i < IntegRows.Length; i++)
			{
				if (!IntegRows[i].Equals(integration_0.IntegRows[i]))
				{
					return false;
				}
			}
		}
		return result;
	}

	public bool GetNDRow(ref IntegRow integRow)
	{
		for (int i = 3; i < Count; i++)
		{
			if (IntegRows[i].oprtStyle == integRow.oprtStyle)
			{
				integRow = IntegRows[i];
				return true;
			}
		}
		return false;
	}

	public void LoadFromObject(Integration integration)
	{
		IntegStyle = integration.IntegStyle;
		Array.Resize(ref integRows, integration.IntegRows.Length);
		for (int i = 0; i < IntegRows.Length; i++)
		{
			IntegRows[i] = integration.IntegRows[i];
		}
	}

	public Integration Copy()
	{
		Integration integration = new Integration();
		integration.IntegStyle = IntegStyle;
		Array.Resize(ref integration.integRows, IntegRows.Length);
		for (int i = 0; i < integration.IntegRows.Length; i++)
		{
			integration.IntegRows[i] = IntegRows[i];
		}
		integration.iniPeakWidth = iniPeakWidth;
		integration.iniPkSlope = iniPkSlope;
		integration.iniTgntAreaF = iniTgntAreaF;
		integration.iniTgntLfF = iniTgntLfF;
		integration.iniTgntRtF = iniTgntRtF;
		integration.iniTgntSlopeF = iniTgntSlopeF;
		integration.iniThreshold = iniThreshold;
		integration.iniVtVSlope = iniVtVSlope;
		return integration;
	}

	public void LoadFromFile(BinaryReader binaryReader_0)
	{
		IntegStyle = (IntegStyle)binaryReader_0.ReadByte();
		Array.Resize(ref integRows, 0);
		IntegRow integRow = default(IntegRow);
		int num = binaryReader_0.ReadInt32();
		for (int i = 0; i < num; i++)
		{
			integRow.LoadFromFile(binaryReader_0);
			AppendRow(integRow);
		}
	}

	public void SaveToFile(BinaryWriter binaryWriter_0)
	{
		binaryWriter_0.Write((byte)IntegStyle);
		binaryWriter_0.Write(IntegRows.Length);
		for (int i = 0; i < IntegRows.Length; i++)
		{
			IntegRows[i].SaveToFile(binaryWriter_0);
		}
	}

	public void RecordSnap()
	{
		if (integRowsListSnap != null)
		{
			int num = integRowsListSnap.Length;
			Array.Resize(ref integRowsListSnap, num + 1);
			(integRowsListSnap[num] = new IntegRowListSnap()).CopyFrom(IntegRows);
		}
	}

	public bool Redo()
	{
		if (integRowsListSnap == null)
		{
			return false;
		}
		if (int_1 != -1 && int_1 < int_0)
		{
			int_1++;
			integRowsListSnap[int_1].CloneTo(ref integRows);
			return true;
		}
		return false;
	}

	public void Reset()
	{
		Array.Resize(ref integRows, 3);
		IntegRows[0].oprtStyle = IntegOprtStyle.PeakWidth;
		IntegRows[1].oprtStyle = IntegOprtStyle.Threshold;
		IntegRows[2].oprtStyle = IntegOprtStyle.VtVSlope;
		for (int i = 0; i < 3; i++)
		{
			IntegRows[i].success = true;
		}
		PeakWidth = IniPeakWidth;
		Threshold = IniThreshold;
		PkSlope = IniPkSlope;
		int_1 = -1;
	}

	public void ResetUndoIndex()
	{
		int_1 = -1;
	}

	public bool Undo()
	{
		if (integRowsListSnap.Length <= 1)
		{
			return false;
		}
		if (int_1 == -1)
		{
			int_1 = integRowsListSnap.Length - 2;
			int_0 = integRowsListSnap.Length - 1;
		}
		else
		{
			if (int_1 == 0)
			{
				return false;
			}
			int_1--;
		}
		integRowsListSnap[int_1].CloneTo(ref integRows);
		return true;
	}
}
