using System;
using System.Windows.Forms;

namespace IBrainChrom2018;

public class LiquidPump1 : LiquidPump
{
	public const string brandName = "浙大智达液相";

	public COM_Port com_port;

	public LiquidPump1()
	{
		scnName = "浙大智达液相";
		senName = "ZJU ZhiDa LC";
		setupForm = new LiquidChromControl1SetupForm(scnName, senName);
		aboutForm = new LiquidChromControl1AboutForm(scnName, senName);
	}

	public override object Clone()
	{
		LiquidPump1 liquidPump = new LiquidPump1();
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
		Array.Resize(ref bsCtrls, 1);
		bsCtrls[0] = new LCC_Pump(this);
		bsCtrls[0].name = "Pump";
		bsCtrls[0].channel = 0;
	}

	public override DialogResult ShowDialog()
	{
		LiquidChromControl1SetupForm liquidChromControl1SetupForm = setupForm as LiquidChromControl1SetupForm;
		liquidChromControl1SetupForm.LoadControl(this);
		DialogResult dialogResult = base.ShowDialog();
		if (dialogResult == DialogResult.OK)
		{
			liquidChromControl1SetupForm.WriteControl(this);
		}
		return dialogResult;
	}
}
