using System;
using System.IO;
using System.Runtime.InteropServices;

namespace IBrainChrom2018;

[Serializable]
[StructLayout(LayoutKind.Sequential)]
public class ChromInfoR
{
	public string mtdFileName = "";

	private bool acqAutoStop = true;

	private float acqRunTime = 45f;

	private Acquisition dtcAcquisition = new Acquisition();

	private bool ecExternalControl = true;

	private ExtCtrlSignal extCtrlSignal = ExtCtrlSignal.Down;

	private ExtCtrlStart extCtrlStart = ExtCtrlStart.StartStop;

	private GcProgTemp gcProgTemp = new GcProgTemp();

	private LcGradient lcGradient = new LcGradient();

	public ProgWaveRow[] uvProgWaves = new ProgWaveRow[0];

	private string uvRange = "0.10";

	private string uvRistTime = "1.0";

	private bool uvUseProgWaves;

	private int uvWave = 460;

	private bool uvWaveScan;

	private int uvwsFrom = 200;

	private int uvwsTo = 500;

	private int uvwsStep = 1;

	private float uvwsStartT = 0.5f;

	private int uvwsStepFreq = 1000;

	public string MtdFileName
	{
		get
		{
			return mtdFileName;
		}
		set
		{
			mtdFileName = value;
		}
	}

	public bool AcqAutoStop
	{
		get
		{
			return acqAutoStop;
		}
		set
		{
			acqAutoStop = value;
		}
	}

	public float AcqRunTime
	{
		get
		{
			return acqRunTime;
		}
		set
		{
			acqRunTime = value;
		}
	}

	public Acquisition DtcAcquisition
	{
		get
		{
			return dtcAcquisition;
		}
		set
		{
			dtcAcquisition = value;
		}
	}

	public bool EcExternalControl
	{
		get
		{
			return ecExternalControl;
		}
		set
		{
			ecExternalControl = value;
		}
	}

	public ExtCtrlSignal ExtCtrlSignal
	{
		get
		{
			return extCtrlSignal;
		}
		set
		{
			extCtrlSignal = value;
		}
	}

	public ExtCtrlStart ExtCtrlStart
	{
		get
		{
			return extCtrlStart;
		}
		set
		{
			extCtrlStart = value;
		}
	}

	public GcProgTemp GcProgTemp
	{
		get
		{
			return gcProgTemp;
		}
		set
		{
			gcProgTemp = value;
		}
	}

	public LcGradient LcGradient
	{
		get
		{
			return lcGradient;
		}
		set
		{
			lcGradient = value;
		}
	}

	public ProgWaveRow[] UvProgWaves
	{
		get
		{
			return uvProgWaves;
		}
		set
		{
			uvProgWaves = value;
		}
	}

	public string UvRange
	{
		get
		{
			return uvRange;
		}
		set
		{
			uvRange = value;
		}
	}

	public string UvRistTime
	{
		get
		{
			return uvRistTime;
		}
		set
		{
			uvRistTime = value;
		}
	}

	public bool UvUseProgWaves
	{
		get
		{
			return uvUseProgWaves;
		}
		set
		{
			uvUseProgWaves = value;
		}
	}

	public int UvWave
	{
		get
		{
			return uvWave;
		}
		set
		{
			uvWave = value;
		}
	}

	public bool UvWaveScan
	{
		get
		{
			return uvWaveScan;
		}
		set
		{
			uvWaveScan = value;
		}
	}

	public int UvwsFrom
	{
		get
		{
			return uvwsFrom;
		}
		set
		{
			uvwsFrom = value;
		}
	}

	public float UvwsStartT
	{
		get
		{
			return uvwsStartT;
		}
		set
		{
			uvwsStartT = value;
		}
	}

	public int UvwsStep
	{
		get
		{
			return uvwsStep;
		}
		set
		{
			uvwsStep = value;
		}
	}

	public int UvwsStepFreq
	{
		get
		{
			return uvwsStepFreq;
		}
		set
		{
			uvwsStepFreq = value;
		}
	}

	public int UvwsTo
	{
		get
		{
			return uvwsTo;
		}
		set
		{
			uvwsTo = value;
		}
	}

	public ChromInfoR Copy()
	{
		ChromInfoR chromInfoR = new ChromInfoR();
		chromInfoR.mtdFileName = mtdFileName;
		chromInfoR.acqAutoStop = acqAutoStop;
		chromInfoR.acqRunTime = acqRunTime;
		chromInfoR.dtcAcquisition = dtcAcquisition.Copy();
		chromInfoR.ecExternalControl = ecExternalControl;
		chromInfoR.extCtrlSignal = extCtrlSignal;
		chromInfoR.extCtrlStart = extCtrlStart;
		chromInfoR.gcProgTemp = gcProgTemp.Copy();
		chromInfoR.lcGradient = lcGradient.Copy();
		chromInfoR.uvProgWaves = uvProgWaves;
		chromInfoR.uvProgWaves = ProgWaveRow.NewArray(uvProgWaves.Length);
		for (int i = 0; i < uvProgWaves.Length; i++)
		{
			chromInfoR.uvProgWaves[i] = uvProgWaves[i].Copy();
		}
		chromInfoR.uvRange = uvRange;
		chromInfoR.uvRistTime = uvRistTime;
		chromInfoR.uvUseProgWaves = uvUseProgWaves;
		chromInfoR.uvWave = uvWave;
		chromInfoR.uvWaveScan = uvWaveScan;
		chromInfoR.uvwsFrom = uvwsFrom;
		chromInfoR.uvwsTo = uvwsTo;
		chromInfoR.uvwsStep = uvwsStep;
		chromInfoR.uvwsStartT = uvwsStartT;
		chromInfoR.uvwsStepFreq = uvwsStepFreq;
		return chromInfoR;
	}

	public void LoadFromFile(BinaryReader binaryReader_0)
	{
		LcGradient.LoadFromFile(binaryReader_0);
		GcProgTemp.LoadFromFile(binaryReader_0);
		UvWave = binaryReader_0.ReadInt32();
		UvRange = binaryReader_0.ReadString();
		UvRistTime = binaryReader_0.ReadString();
		UvUseProgWaves = binaryReader_0.ReadBoolean();
		Array.Resize(ref uvProgWaves, binaryReader_0.ReadInt32());
		for (int i = 0; i < UvProgWaves.Length; i++)
		{
			UvProgWaves[i].LoadFromFile(binaryReader_0);
		}
		UvWaveScan = binaryReader_0.ReadBoolean();
		UvwsStartT = binaryReader_0.ReadSingle();
		UvwsStepFreq = binaryReader_0.ReadInt32();
		UvwsFrom = binaryReader_0.ReadInt32();
		UvwsTo = binaryReader_0.ReadInt32();
		UvwsStep = binaryReader_0.ReadInt32();
		AcqAutoStop = binaryReader_0.ReadBoolean();
		AcqRunTime = binaryReader_0.ReadSingle();
		EcExternalControl = binaryReader_0.ReadBoolean();
		ExtCtrlStart = (ExtCtrlStart)binaryReader_0.ReadByte();
		ExtCtrlSignal = (ExtCtrlSignal)binaryReader_0.ReadByte();
		DtcAcquisition.LoadFromFile(binaryReader_0);
		MtdFileName = binaryReader_0.ReadString();
	}

	public void LoadFromObject(ChromInfoR chromInfoR)
	{
		LcGradient.LoadFromObject(chromInfoR.LcGradient);
		GcProgTemp.LoadFromObject(chromInfoR.GcProgTemp);
		UvWave = chromInfoR.UvWave;
		UvRange = chromInfoR.UvRange;
		UvRistTime = chromInfoR.UvRistTime;
		UvUseProgWaves = chromInfoR.UvUseProgWaves;
		Array.Resize(ref uvProgWaves, chromInfoR.UvProgWaves.Length);
		for (int i = 0; i < UvProgWaves.Length; i++)
		{
			UvProgWaves[i] = chromInfoR.UvProgWaves[i];
		}
		UvWaveScan = chromInfoR.UvWaveScan;
		UvwsStartT = chromInfoR.UvwsStartT;
		UvwsStepFreq = chromInfoR.UvwsStepFreq;
		UvwsFrom = chromInfoR.UvwsFrom;
		UvwsTo = chromInfoR.UvwsTo;
		UvwsStep = chromInfoR.UvwsStep;
		AcqAutoStop = chromInfoR.AcqAutoStop;
		AcqRunTime = chromInfoR.AcqRunTime;
		EcExternalControl = chromInfoR.EcExternalControl;
		ExtCtrlStart = chromInfoR.ExtCtrlStart;
		ExtCtrlSignal = chromInfoR.ExtCtrlSignal;
		DtcAcquisition = chromInfoR.DtcAcquisition;
		MtdFileName = chromInfoR.MtdFileName;
	}

	public void SaveToFile(BinaryWriter binaryWriter_0)
	{
		LcGradient.SaveToFile(binaryWriter_0);
		GcProgTemp.SaveToFile(binaryWriter_0);
		binaryWriter_0.Write(UvWave);
		binaryWriter_0.Write(UvRange);
		binaryWriter_0.Write(UvRistTime);
		binaryWriter_0.Write(UvUseProgWaves);
		binaryWriter_0.Write(UvProgWaves.Length);
		for (int i = 0; i < UvProgWaves.Length; i++)
		{
			UvProgWaves[i].SaveToFile(binaryWriter_0);
		}
		binaryWriter_0.Write(UvWaveScan);
		binaryWriter_0.Write(UvwsStartT);
		binaryWriter_0.Write(UvwsStepFreq);
		binaryWriter_0.Write(UvwsFrom);
		binaryWriter_0.Write(UvwsTo);
		binaryWriter_0.Write(UvwsStep);
		binaryWriter_0.Write(AcqAutoStop);
		binaryWriter_0.Write(AcqRunTime);
		binaryWriter_0.Write(EcExternalControl);
		binaryWriter_0.Write((byte)ExtCtrlStart);
		binaryWriter_0.Write((byte)ExtCtrlSignal);
		DtcAcquisition.SaveToFile(binaryWriter_0);
		binaryWriter_0.Write(MtdFileName);
	}
}
