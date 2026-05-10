using System.IO;

namespace IBrainChrom2018;

public class InstruWinsInfo
{
	public const int idxCaliGnlForm = 5;

	public const int idxCaliGpcForm = 6;

	public const int idxChromForm = 4;

	public const int idxDataAcqForm = 3;

	public const int idxDevMonitorForm = 2;

	public const int idxInstruForm = 0;

	public const int idxSeqAlyForm = 1;

	public const int idxSSAlyForm = 7;

	public string curPrjName = "";

	public InstrumentForm instruForm;

	public bool valid;

	public WinInfo[] winInfos = new WinInfo[8];

	public InstruWinsInfo()
	{
		for (int i = 0; i < winInfos.Length; i++)
		{
			winInfos[i] = new WinInfo();
		}
	}

	public void Load(BinaryReader binaryReader_0)
	{
		valid = binaryReader_0.ReadBoolean();
		for (int i = 0; i < winInfos.Length; i++)
		{
			winInfos[i].Load(binaryReader_0);
		}
		curPrjName = binaryReader_0.ReadString();
	}

	public void ReadFromForm()
	{
		winInfos[0].ReadFromForm(instruForm);
		if (instruForm.instrument.pjtDir != null)
		{
			curPrjName = instruForm.instrument.pjtDir.projectName;
		}
		else
		{
			curPrjName = "";
		}
		winInfos[1].ReadFromForm(instruForm.seqAlyForm);
		winInfos[2].ReadFromForm(instruForm.devMonitorForm);
		winInfos[3].ReadFromForm(instruForm.dataAcqForm);
		winInfos[4].ReadFromForm(instruForm.chromForm);
		winInfos[5].ReadFromForm(instruForm.caliGnlForm);
		winInfos[6].ReadFromForm(instruForm.caliGpcForm);
		winInfos[7].ReadFromForm(instruForm.ssAlyForm);
	}

	public void Save(BinaryWriter binaryWriter_0)
	{
		binaryWriter_0.Write(valid);
		for (int i = 0; i < winInfos.Length; i++)
		{
			winInfos[i].Save(binaryWriter_0);
		}
		binaryWriter_0.Write(curPrjName);
	}

	public void WriteToForm()
	{
		instruForm.ReadWinInfo(winInfos[0]);
		DirectoryInfo directoryInfo = new DirectoryInfo(instruForm.instrument.InstruDir + curPrjName);
		if (curPrjName != "" && directoryInfo.Exists)
		{
			instruForm.instrument.pjtDir = new PjtDir(instruForm.instrument.InstruDir, curPrjName);
			instruForm.instrument.pjtDir.CreateDirectories();
			instruForm.SetProjectDir();
		}
		instruForm.seqAlyForm.ReadWinInfo(winInfos[1]);
		instruForm.devMonitorForm.ReadWinInfo(winInfos[2]);
		instruForm.dataAcqForm.ReadWinInfo(winInfos[3]);
		instruForm.chromForm.ReadWinInfo(winInfos[4]);
		instruForm.caliGpcForm.ReadWinInfo(winInfos[6]);
		instruForm.ssAlyForm.ReadWinInfo(winInfos[7]);
		if (winInfos[1].visible)
		{
			instruForm.btnSequence_Click(null, null);
		}
		if (winInfos[2].visible)
		{
			instruForm.btnDeviceMonitor_Click(null, null);
		}
		if (winInfos[3].visible)
		{
			instruForm.btnDataAcquisition_Click(null, null);
		}
		if (winInfos[4].visible)
		{
			instruForm.btnChromWindow_Click(null, null);
		}
		if (winInfos[5].visible || winInfos[6].visible)
		{
			instruForm.btnCaliWindow_Click(null, null);
		}
		if (winInfos[7].visible)
		{
			instruForm.btnSingle_Click(null, null);
		}
	}
}
