using System.IO;

namespace IBrainChrom2018;

public class SeqAlyOpt
{
	public bool activeSequence = true;

	public int counter_current = 1;

	public CounterResetStyle counter_resetStyle;

	public int counter_start = 1;

	public string description = "";

	public FormatStyle formatStyle;

	public bool idleBeforeFirstInj;

	public float idleTime = 0.1f;

	public VolumnUnits injVolumnUnit;

	public void LoadFromFile(BinaryReader binaryReader_0)
	{
		byte b = binaryReader_0.ReadByte();
		if (b == 1)
		{
			activeSequence = binaryReader_0.ReadBoolean();
			idleBeforeFirstInj = binaryReader_0.ReadBoolean();
			idleTime = binaryReader_0.ReadSingle();
			counter_start = binaryReader_0.ReadInt32();
			counter_resetStyle = (CounterResetStyle)binaryReader_0.ReadByte();
			counter_current = binaryReader_0.ReadInt32();
			formatStyle = (FormatStyle)binaryReader_0.ReadByte();
			injVolumnUnit = (VolumnUnits)binaryReader_0.ReadByte();
			description = binaryReader_0.ReadString();
		}
		else
		{
			Class49.smethod_33(b);
		}
	}

	public void LoadFromObject(SeqAlyOpt sequenceAnalysisOptions)
	{
		activeSequence = sequenceAnalysisOptions.activeSequence;
		idleBeforeFirstInj = sequenceAnalysisOptions.idleBeforeFirstInj;
		idleTime = sequenceAnalysisOptions.idleTime;
		counter_start = sequenceAnalysisOptions.counter_start;
		counter_resetStyle = sequenceAnalysisOptions.counter_resetStyle;
		counter_current = sequenceAnalysisOptions.counter_current;
		formatStyle = sequenceAnalysisOptions.formatStyle;
		injVolumnUnit = sequenceAnalysisOptions.injVolumnUnit;
		description = sequenceAnalysisOptions.description;
	}

	public void SaveToFile(BinaryWriter binaryWriter_0)
	{
		binaryWriter_0.Write(Class49.smethod_36());
		binaryWriter_0.Write(activeSequence);
		binaryWriter_0.Write(idleBeforeFirstInj);
		binaryWriter_0.Write(idleTime);
		binaryWriter_0.Write(counter_start);
		binaryWriter_0.Write((byte)counter_resetStyle);
		binaryWriter_0.Write(counter_current);
		binaryWriter_0.Write((byte)formatStyle);
		binaryWriter_0.Write((byte)injVolumnUnit);
		binaryWriter_0.Write(description);
	}
}
