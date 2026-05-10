using System;
using System.Windows.Forms;

namespace IBrainChrom2018;

internal class Control1 : SysCfgControl
{
	public const string string_0 = "赛智液相控制器";

	public Control1()
	{
		controlModule = ControlModule.Set;
		hwStyle = HwStyle.SZ;
		scnName = "赛智液相控制器";
		senName = "Clarify M.C. Card";
		setupForm = new SZ_MCSetupForm(scnName, senName);
		aboutForm = new SZ_MCAboutForm(scnName, senName);
	}

	public override object Clone()
	{
		Control1 control = new Control1();
		control.hardString = hardString;
		control.hardWare = base.HardWare;
		Array.Resize(ref control.bsCtrls, bsCtrls.Length);
		for (int i = 0; i < control.bsCtrls.Length; i++)
		{
			if (bsCtrls[i] is ASC_Sampler)
			{
				control.bsCtrls[i] = new ASC_Sampler(control);
			}
			else if (bsCtrls[i] is GCC_GCs)
			{
				control.bsCtrls[i] = new GCC_GCs(control);
			}
			else if (bsCtrls[i] is LCC_Pump)
			{
				control.bsCtrls[i] = new LCC_Pump(control);
			}
			else if (bsCtrls[i] is DtC_Detector)
			{
				control.bsCtrls[i] = new DtC_Detector(control);
			}
			control.bsCtrls[i].LoadFromObject(bsCtrls[i]);
		}
		return control;
	}

	public override void InitCreate()
	{
		Array.Resize(ref bsCtrls, 3);
		bsCtrls[0] = new LCC_Pump(this);
		bsCtrls[0].name = Lang.PS("泵 1 a", "Pump 1 a");
		bsCtrls[0].byte_0 = 2;
		bsCtrls[1] = new LCC_Pump(this);
		bsCtrls[1].name = Lang.PS("泵 2 b", "Pump 2 b");
		bsCtrls[1].byte_0 = 3;
		bsCtrls[2] = new DtC_Detector(this);
		bsCtrls[2].name = Lang.PS("检测器 UV", "Dtc UV");
		bsCtrls[2].byte_0 = 1;
	}

	public override DialogResult ShowDialog()
	{
		SZ_MCSetupForm sZ_MCSetupForm = setupForm as SZ_MCSetupForm;
		sZ_MCSetupForm.LoadControl(this);
		DialogResult dialogResult = base.ShowDialog();
		if (dialogResult == DialogResult.OK)
		{
			sZ_MCSetupForm.WriteControl(this);
			tnProduct.Text = base.ShowName;
		}
		return dialogResult;
	}
}
