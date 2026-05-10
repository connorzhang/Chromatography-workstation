using System;
using System.IO;
using System.Runtime.InteropServices;

namespace IBrainChrom2018;

[Serializable]
[StructLayout(LayoutKind.Sequential)]
public class Level
{
	public float amount;

	public FuncPt eFuncPt;

	public FuncPt iFuncPt;

	public float respFactor;

	public float response;

	public float responseA;

	public float responseH;

	private SplLevel[] splLevel_0 = new SplLevel[0];

	public bool used;

	public float LastAddresponseA;

	public float LastAddresponseH;

	public int SecsNum => splLevel_0.Length;

	public void AddRec(float responseA, float responseH)
	{
		Array.Resize(ref splLevel_0, splLevel_0.Length + 1);
		splLevel_0[splLevel_0.Length - 1].responseA = responseA;
		splLevel_0[splLevel_0.Length - 1].responseH = responseH;
		LastAddresponseA = responseA;
		LastAddresponseH = responseH;
	}

	public void ClearRecs()
	{
		responseA = (responseH = (response = (amount = (respFactor = 0f))));
		Array.Resize(ref splLevel_0, 0);
	}

	public void LoadFromObject(Level level)
	{
		responseA = level.responseA;
		responseH = level.responseH;
		response = level.response;
		amount = level.amount;
		respFactor = level.respFactor;
		used = level.used;
		eFuncPt = level.eFuncPt;
		iFuncPt = level.iFuncPt;
		Array.Resize(ref splLevel_0, level.splLevel_0.Length);
		for (int i = 0; i < SecsNum; i++)
		{
			splLevel_0[i] = level.splLevel_0[i];
		}
	}

	public void SetRecaliMode(RecaliMode recaliMode)
	{
		if (SecsNum == 0)
		{
			return;
		}
		switch (recaliMode)
		{
		case RecaliMode.Average:
		{
			responseA = 0f;
			responseH = 0f;
			for (int i = 0; i < SecsNum; i++)
			{
				responseA += splLevel_0[i].responseA;
				responseH += splLevel_0[i].responseH;
			}
			responseA /= SecsNum;
			responseH /= SecsNum;
			break;
		}
		case RecaliMode.Replace:
			responseA = splLevel_0[SecsNum - 1].responseA;
			responseH = splLevel_0[SecsNum - 1].responseH;
			break;
		}
	}

	public Level Copy()
	{
		Level level = new Level();
		level.responseA = responseA;
		level.responseH = responseH;
		level.response = response;
		level.amount = amount;
		level.respFactor = respFactor;
		level.used = used;
		level.eFuncPt = eFuncPt;
		level.iFuncPt = iFuncPt;
		Array.Resize(ref level.splLevel_0, splLevel_0.Length);
		for (int i = 0; i < SecsNum; i++)
		{
			level.splLevel_0[i] = splLevel_0[i];
		}
		return level;
	}

	public void LoadFromFile(BinaryReader binaryReader_0)
	{
		responseA = binaryReader_0.ReadSingle();
		responseH = binaryReader_0.ReadSingle();
		response = binaryReader_0.ReadSingle();
		amount = binaryReader_0.ReadSingle();
		respFactor = binaryReader_0.ReadSingle();
		used = binaryReader_0.ReadBoolean();
		Array.Resize(ref splLevel_0, binaryReader_0.ReadInt32());
		for (int i = 0; i < SecsNum; i++)
		{
			splLevel_0[i].LoadFromFile(binaryReader_0);
		}
	}

	public void SaveToFile(BinaryWriter binaryWriter_0)
	{
		binaryWriter_0.Write(responseA);
		binaryWriter_0.Write(responseH);
		binaryWriter_0.Write(response);
		binaryWriter_0.Write(amount);
		binaryWriter_0.Write(respFactor);
		binaryWriter_0.Write(used);
		binaryWriter_0.Write(SecsNum);
		for (int i = 0; i < SecsNum; i++)
		{
			splLevel_0[i].SaveToFile(binaryWriter_0);
		}
	}
}
