using System;
using System.Windows.Forms;

namespace IBrainChrom2018;

public class AutoSamplerControl1 : AutoSamplerControl
{
	public const string brandName = "赛尔泰自动进样器";

	public AutoSamplerControl1_ID autoSamplerControl1_ID;

	public COM_Port com_port;

	public AutoSamplerControl1()
	{
		scnName = "赛尔泰自动进样器";
		senName = "赛尔泰AS";
		setupForm = new AutoSamplerControl1SetupForm(scnName, senName);
		aboutForm = new AutoSamplerControl1AboutForm(scnName, senName);
	}

	public override object Clone()
	{
		AutoSamplerControl1 autoSamplerControl = new AutoSamplerControl1();
		autoSamplerControl.com_port = com_port;
		autoSamplerControl.autoSamplerControl1_ID = autoSamplerControl1_ID;
		Array.Resize(ref autoSamplerControl.bsCtrls, bsCtrls.Length);
		for (int i = 0; i < autoSamplerControl.bsCtrls.Length; i++)
		{
			autoSamplerControl.bsCtrls[i] = new ASC_Sampler(autoSamplerControl);
			autoSamplerControl.bsCtrls[i].LoadFromObject(bsCtrls[i]);
		}
		return autoSamplerControl;
	}

	public override void InitCreate()
	{
		Array.Resize(ref bsCtrls, 1);
		bsCtrls[0] = new ASC_Sampler(this);
		bsCtrls[0].name = "Sampler";
		bsCtrls[0].channel = 0;
	}

	public override DialogResult ShowDialog()
	{
		AutoSamplerControl1SetupForm autoSamplerControl1SetupForm = setupForm as AutoSamplerControl1SetupForm;
		autoSamplerControl1SetupForm.LoadControl(this);
		DialogResult dialogResult = base.ShowDialog();
		if (dialogResult == DialogResult.OK)
		{
			autoSamplerControl1SetupForm.WriteControl(this);
		}
		return dialogResult;
	}
}
