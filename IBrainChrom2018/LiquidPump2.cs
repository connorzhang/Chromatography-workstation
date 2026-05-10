using System;
using System.Windows.Forms;

namespace IBrainChrom2018;

public class LiquidPump2 : LiquidPump
{
	public const string brandName = "赛尔泰液相";

	public COM_Port com_port;

	public LiquidPump2()
	{
		scnName = "赛尔泰液相";
		senName = "SaiErTai LC";
		setupForm = new LiquidPump2SetupForm(scnName, senName);
		aboutForm = new LiquidPump2AboutForm(scnName, senName);
	}

	public override object Clone()
	{
		LiquidPump2 liquidPump = new LiquidPump2();
		liquidPump.com_port = com_port;
		Array.Resize(ref liquidPump.bsCtrls, bsCtrls.Length);
		for (int i = 0; i < liquidPump.bsCtrls.Length; i++)
		{
			liquidPump.bsCtrls[i] = new LCC_Pump(liquidPump);
			liquidPump.bsCtrls[i].LoadFromObject(bsCtrls[i]);
		}
		return liquidPump;
	}

	public override void InitCreate()
	{
		Array.Resize(ref bsCtrls, 4);
		for (int i = 0; i < bsCtrls.Length; i++)
		{
			bsCtrls[i] = new LCC_Pump(this);
			bsCtrls[i].name = Lang.PS("泵 ", "Pump ") + (i + 1);
			bsCtrls[i].channel = (byte)i;
		}
	}

	public override DialogResult ShowDialog()
	{
		LiquidPump2SetupForm liquidPump2SetupForm = setupForm as LiquidPump2SetupForm;
		liquidPump2SetupForm.LoadControl(this);
		DialogResult dialogResult = base.ShowDialog();
		if (dialogResult == DialogResult.OK)
		{
			liquidPump2SetupForm.WriteControl(this);
		}
		return dialogResult;
	}
}
