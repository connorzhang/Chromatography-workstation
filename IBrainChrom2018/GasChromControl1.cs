using System;
using System.Windows.Forms;

namespace IBrainChrom2018;

public class GasChromControl1 : GasChromControl
{
	public const string brandName = "GC08";

	public COM_Port com_port;

	public int gcSerial;

	public GasChromControl1()
	{
		scnName = "GC08";
		senName = "GC08";
		setupForm = new GasChromControl1SetupForm(scnName, senName);
		aboutForm = new GasChromControl1AboutForm(scnName, senName);
	}

	public override object Clone()
	{
		GasChromControl1 gasChromControl = new GasChromControl1();
		gasChromControl.com_port = com_port;
		gasChromControl.gcSerial = gcSerial;
		Array.Resize(ref gasChromControl.bsCtrls, bsCtrls.Length);
		for (int i = 0; i < gasChromControl.bsCtrls.Length; i++)
		{
			gasChromControl.bsCtrls[i] = new GC08_GCs(gasChromControl);
			gasChromControl.bsCtrls[i].LoadFromObject(bsCtrls[i]);
		}
		return gasChromControl;
	}

	public override void InitCreate()
	{
		Array.Resize(ref bsCtrls, 1);
		bsCtrls[0] = new GC08_GCs(this);
		bsCtrls[0].name = "GCs";
		bsCtrls[0].channel = 0;
	}

	public override DialogResult ShowDialog()
	{
		GasChromControl1SetupForm gasChromControl1SetupForm = setupForm as GasChromControl1SetupForm;
		gasChromControl1SetupForm.LoadControl(this);
		DialogResult dialogResult = base.ShowDialog();
		if (dialogResult == DialogResult.OK)
		{
			gasChromControl1SetupForm.WriteControl(this);
		}
		return dialogResult;
	}
}
