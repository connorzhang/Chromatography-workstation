using System;
using System.Windows.Forms;

namespace IBrainChrom2018;

public class DetectorControl1 : DetectorControl
{
	public const string brandName = "浙大智达检测器";

	public DetectorControl1()
	{
		scnName = "浙大智达检测器";
		senName = "浙大智达Detector";
		setupForm = new DetectorControl1SetupForm(scnName, senName);
		aboutForm = new DetectorControl1AboutForm(scnName, senName);
	}

	public override object Clone()
	{
		DetectorControl1 detectorControl = new DetectorControl1();
		Array.Resize(ref detectorControl.bsCtrls, bsCtrls.Length);
		for (int i = 0; i < detectorControl.bsCtrls.Length; i++)
		{
			detectorControl.bsCtrls[i] = new DtC_Channel(detectorControl);
			detectorControl.bsCtrls[i].LoadFromObject(bsCtrls[i]);
		}
		return detectorControl;
	}

	public override void InitCreate()
	{
		Array.Resize(ref bsCtrls, 4);
		for (int i = 0; i < bsCtrls.Length; i++)
		{
			bsCtrls[i] = new DtC_Channel(this);
			bsCtrls[i].name = i + 1 + " 通道";
			bsCtrls[i].channel = (byte)i;
		}
	}

	public override DialogResult ShowDialog()
	{
		DetectorControl1SetupForm detectorControl1SetupForm = setupForm as DetectorControl1SetupForm;
		detectorControl1SetupForm.LoadControl(this);
		DialogResult dialogResult = base.ShowDialog();
		if (dialogResult == DialogResult.OK)
		{
			detectorControl1SetupForm.WriteControl(this);
		}
		return dialogResult;
	}
}
