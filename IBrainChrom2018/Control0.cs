using System;
using System.Windows.Forms;

namespace IBrainChrom2018;

internal class Control0 : SysCfgControl
{
	public const string string_0 = "赛智新液相";

	private const int int_0 = 5;

	public Control0()
	{
		controlModule = ControlModule.Set;
		hwStyle = HwStyle.SZ;
		scnName = "赛智新液相";
		senName = "SZ M.C. Card";
		setupForm = new SZ_MC2SetupForm(scnName, senName);
		aboutForm = new SZ_MC2AboutForm(scnName, senName);
	}

	public override object Clone()
	{
		Control0 control = new Control0();
		control.hardString = hardString;
		control.hardWare = method_1();
		Array.Resize(ref control.bsCtrls, bsCtrls.Length);
		for (int i = 0; i < control.bsCtrls.Length; i++)
		{
			if (bsCtrls[i] is ASC_LC2)
			{
				control.bsCtrls[i] = new ASC_LC2(control);
			}
			else if (bsCtrls[i] is LCC_LC2)
			{
				control.bsCtrls[i] = new LCC_LC2(control);
			}
			else if (bsCtrls[i] is LCG_LC2)
			{
				control.bsCtrls[i] = new LCG_LC2(control);
			}
			else if (bsCtrls[i] is DtC_LC2)
			{
				control.bsCtrls[i] = new DtC_LC2(control);
			}
			else if (bsCtrls[i] is Ovn_LC2)
			{
				control.bsCtrls[i] = new Ovn_LC2(control);
			}
			control.bsCtrls[i].LoadFromObject(bsCtrls[i]);
		}
		return control;
	}

	private void method_0(ref LcCmd lcCmd_0)
	{
		if (!lcCmd_0.OK)
		{
			return;
		}
		for (int i = 0; i < bsCtrls.Length; i++)
		{
			if (bsCtrls[i].byte_0 == lcCmd_0.device)
			{
				bsCtrls[i].Raise(ref lcCmd_0);
				if (i != 5)
				{
					bsCtrls[5].Raise(ref lcCmd_0);
				}
				break;
			}
		}
	}

	public override void InitCreate()
	{
		Array.Resize(ref bsCtrls, 6);
		bsCtrls[0] = new ASC_LC2(this);
		bsCtrls[0].name = Lang.PS("自动进样", "ASC");
		bsCtrls[0].byte_0 = 4;
		bsCtrls[1] = new LCC_LC2(this);
		bsCtrls[1].name = Lang.PS("泵 1 a", "Pump 1 a");
		bsCtrls[1].byte_0 = 2;
		bsCtrls[2] = new LCC_LC2(this);
		bsCtrls[2].name = Lang.PS("泵 2 b", "Pump 2 b");
		bsCtrls[2].byte_0 = 3;
		bsCtrls[3] = new LCG_LC2(this);
		bsCtrls[3].name = Lang.PS("低压梯度", "Gradient");
		bsCtrls[3].byte_0 = 5;
		bsCtrls[4] = new Ovn_LC2(this);
		bsCtrls[4].name = Lang.PS("柱温箱", "Oven");
		bsCtrls[4].byte_0 = 6;
		bsCtrls[5] = new DtC_LC2(this);
		bsCtrls[5].name = Lang.PS("检测器", "Detector");
		bsCtrls[5].byte_0 = 1;
	}

	public override DialogResult ShowDialog()
	{
		SZ_MC2SetupForm sZ_MC2SetupForm = setupForm as SZ_MC2SetupForm;
		sZ_MC2SetupForm.LoadControl(this);
		DialogResult dialogResult = base.ShowDialog();
		if (dialogResult == DialogResult.OK)
		{
			sZ_MC2SetupForm.WriteControl(this);
			tnProduct.Text = base.ShowName;
		}
		return dialogResult;
	}

	public object method_1()
	{
		return hardWare;
	}

	public void method_2(object object_0)
	{
		if (hardWare != null && hardWare is Class10)
		{
			(hardWare as Class10).method_3(controlModule, bool_1: false);
		}
		hardWare = object_0;
		hardString = base.HardStr;
		if (tnProduct != null)
		{
			tnProduct.Text = base.ShowName;
		}
		bool flag = hardWare != null;
		if (hardWare != null && hardWare is Class10 && (hardWare as Class10).method_3(controlModule, flag) && flag)
		{
			(hardWare as Class10).method_6(method_0);
		}
	}
}
