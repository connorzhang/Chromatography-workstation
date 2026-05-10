using System;
using System.Windows.Forms;

namespace IBrainChrom2018;

public class SZ_Dt : SysCfgControl
{
	public const string brandName = "赛智采集卡";

	public SZ_Dt()
	{
		controlModule = ControlModule.Set;
		hwStyle = HwStyle.SZ;
		scnName = "赛智采集卡";
		senName = "Clarify Data Card";
		setupForm = new SZ_DtSetupForm(scnName, senName);
		aboutForm = new SZ_DtAboutForm(scnName, senName);
	}

	public override object Clone()
	{
		SZ_Dt sZ_Dt = new SZ_Dt();
		sZ_Dt.hardString = hardString;
		sZ_Dt.hardWare = base.HardWare;
		Array.Resize(ref sZ_Dt.bsCtrls, bsCtrls.Length);
		for (int i = 0; i < sZ_Dt.bsCtrls.Length; i++)
		{
			sZ_Dt.bsCtrls[i] = new DtC_Detector(sZ_Dt);
			sZ_Dt.bsCtrls[i].LoadFromObject(bsCtrls[i]);
		}
		return sZ_Dt;
	}

	public override void InitCreate()
	{
		Array.Resize(ref bsCtrls, 2);
		bsCtrls[0] = new DtC_Detector(this);
		bsCtrls[0].name = Lang.PS("通道 1 a", "Channel 1 a");
		bsCtrls[0].channel = 0;
		bsCtrls[1] = new DtC_Detector(this);
		bsCtrls[1].name = Lang.PS("通道 2 x", "Channel 2 x");
		bsCtrls[1].channel = 1;
	}

	public override DialogResult ShowDialog()
	{
		SZ_DtSetupForm sZ_DtSetupForm = setupForm as SZ_DtSetupForm;
		sZ_DtSetupForm.LoadControl(this);
		DialogResult dialogResult = base.ShowDialog();
		if (dialogResult == DialogResult.OK)
		{
			sZ_DtSetupForm.WriteControl(this);
		}
		return dialogResult;
	}
}
