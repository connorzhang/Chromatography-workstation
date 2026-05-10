using System;
using System.Windows.Forms;

namespace IBrainChrom2018;

public class GasChromControl2 : GasChromControl
{
	public const string brandName = "智达气相";

	public COM_Port com_port;

	public GasChromControl2()
	{
		scnName = "智达气相";
		senName = "Zju ZhiDa GC";
		setupForm = new GasChromControl2SetupForm(scnName, senName);
		aboutForm = new GasChromControl2AboutForm(scnName, senName);
	}

	public override object Clone()
	{
		GasChromControl2 gasChromControl = new GasChromControl2();
		gasChromControl.com_port = com_port;
		Array.Resize(ref gasChromControl.bsCtrls, bsCtrls.Length);
		for (int i = 0; i < gasChromControl.bsCtrls.Length; i++)
		{
			gasChromControl.bsCtrls[i] = new GCC_GCs(gasChromControl);
			gasChromControl.bsCtrls[i].LoadFromObject(bsCtrls[i]);
		}
		return gasChromControl;
	}

	public override void InitCreate()
	{
		Array.Resize(ref bsCtrls, 1);
		bsCtrls[0] = new GCC_GCs(this);
		bsCtrls[0].name = "GC";
		bsCtrls[0].channel = 0;
		bsCtrls[0].from = this;
	}

	public override DialogResult ShowDialog()
	{
		GasChromControl2SetupForm gasChromControl2SetupForm = setupForm as GasChromControl2SetupForm;
		gasChromControl2SetupForm.LoadControl(this);
		DialogResult dialogResult = base.ShowDialog();
		if (dialogResult == DialogResult.OK)
		{
			gasChromControl2SetupForm.WriteControl(this);
		}
		return dialogResult;
	}
}
