using System;
using System.IO;

namespace IBrainChrom2018;

public class CtrlModules
{
	public SysCfgControl[] autoSamplers = new SysCfgControl[0];

	public SysCfgControl[] detectors = new SysCfgControl[0];

	public SysCfgControl[] gasControls = new SysCfgControl[0];

	public SysCfgControl[] liquidControls = new SysCfgControl[0];

	public SysCfgControl[] sets = new SysCfgControl[0];

	private void method_0(ref SysCfgControl[] sysCfgControl_0, SysCfgControl sysCfgControl_1)
	{
		Type type = sysCfgControl_1.GetType();
		int num = 0;
		for (int i = 0; i < sysCfgControl_0.Length; i++)
		{
			if (sysCfgControl_0[i].GetType().Equals(type))
			{
				num++;
			}
		}
		sysCfgControl_1.No = num;
		Array.Resize(ref sysCfgControl_0, sysCfgControl_0.Length + 1);
		sysCfgControl_0[sysCfgControl_0.Length - 1] = sysCfgControl_1;
	}

	public void AddControlModule(SysCfgControl sysCfgControl)
	{
		switch (sysCfgControl.controlModule)
		{
		case ControlModule.AutoSampler:
			method_0(ref autoSamplers, sysCfgControl);
			break;
		case ControlModule.Pump:
			method_0(ref liquidControls, sysCfgControl);
			break;
		case ControlModule.Gradient:
		case ControlModule.Oven:
			break;
		case ControlModule.GasControl:
			method_0(ref gasControls, sysCfgControl);
			break;
		case ControlModule.Detector:
			method_0(ref detectors, sysCfgControl);
			break;
		case ControlModule.Set:
			method_0(ref sets, sysCfgControl);
			break;
		}
	}

	public void ClearAll()
	{
		Array.Resize(ref autoSamplers, 0);
		Array.Resize(ref liquidControls, 0);
		Array.Resize(ref gasControls, 0);
		Array.Resize(ref detectors, 0);
		Array.Resize(ref sets, 0);
	}

	private bool method_1(ref SysCfgControl[] sysCfgControl_0, SysCfgControl sysCfgControl_1)
	{
		Type type = sysCfgControl_1.GetType();
		bool result = false;
		for (int i = 0; i < sysCfgControl_0.Length; i++)
		{
			if (!sysCfgControl_0[i].Equals(sysCfgControl_1))
			{
				continue;
			}
			for (int j = i; j < sysCfgControl_0.Length - 1; j++)
			{
				sysCfgControl_0[j] = sysCfgControl_0[j + 1];
			}
			Array.Resize(ref sysCfgControl_0, sysCfgControl_0.Length - 1);
			result = true;
			if (result)
			{
				int num = 0;
				for (i = 0; i < sysCfgControl_0.Length; i++)
				{
					if (sysCfgControl_0[i].GetType().Equals(type))
					{
						sysCfgControl_0[i].No = num++;
					}
				}
			}
			return result;
		}
		return result;
	}

	public bool DeleteControlModule(SysCfgControl sysCfgControl)
	{
		return sysCfgControl.controlModule switch
		{
			ControlModule.AutoSampler => method_1(ref autoSamplers, sysCfgControl), 
			ControlModule.Pump => method_1(ref liquidControls, sysCfgControl), 
			ControlModule.GasControl => method_1(ref gasControls, sysCfgControl), 
			ControlModule.Detector => method_1(ref detectors, sysCfgControl), 
			ControlModule.Set => method_1(ref sets, sysCfgControl), 
			_ => false, 
		};
	}

	private void method_2(BinaryReader binaryReader_0, ref SysCfgControl[] sysCfgControl_0)
	{
		Array.Resize(ref sysCfgControl_0, binaryReader_0.ReadInt32());
		for (int i = 0; i < sysCfgControl_0.Length; i++)
		{
			SysCfgControl.LoadFromFile(binaryReader_0, out sysCfgControl_0[i]);
		}
	}

	public void LoadFromFile(BinaryReader binaryReader_0)
	{
		method_2(binaryReader_0, ref autoSamplers);
		method_2(binaryReader_0, ref liquidControls);
		method_2(binaryReader_0, ref gasControls);
		method_2(binaryReader_0, ref detectors);
		method_2(binaryReader_0, ref sets);
	}

	public void LoadFromObject(CtrlModules controlModules)
	{
		ClearAll();
		for (int i = 0; i < controlModules.autoSamplers.Length; i++)
		{
			AddControlModule((SysCfgControl)controlModules.autoSamplers[i].Clone());
		}
		for (int j = 0; j < controlModules.liquidControls.Length; j++)
		{
			AddControlModule((SysCfgControl)controlModules.liquidControls[j].Clone());
		}
		for (int k = 0; k < controlModules.gasControls.Length; k++)
		{
			AddControlModule((SysCfgControl)controlModules.gasControls[k].Clone());
		}
		for (int l = 0; l < controlModules.detectors.Length; l++)
		{
			AddControlModule((SysCfgControl)controlModules.detectors[l].Clone());
		}
		for (int m = 0; m < controlModules.sets.Length; m++)
		{
			AddControlModule((SysCfgControl)controlModules.sets[m].Clone());
		}
	}

	private void method_3(BinaryWriter binaryWriter_0, SysCfgControl[] sysCfgControl_0)
	{
		binaryWriter_0.Write(sysCfgControl_0.Length);
		for (int i = 0; i < sysCfgControl_0.Length; i++)
		{
			sysCfgControl_0[i].SaveToFile(binaryWriter_0);
		}
	}

	public void SaveToFile(BinaryWriter binaryWriter_0)
	{
		method_3(binaryWriter_0, autoSamplers);
		method_3(binaryWriter_0, liquidControls);
		method_3(binaryWriter_0, gasControls);
		method_3(binaryWriter_0, detectors);
		method_3(binaryWriter_0, sets);
	}
}
