using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace IBrainChrom2018;

public class SysConfig
{
	private const string string_0 = "该模块已配置到仪器\n请先从相关仪器移除！";

	private const string string_1 = "错误配置！";

	private const string string_2 = "仪器已配置：";

	private const string string_3 = "Current control has equiped to instrument\nPlease remove from it,first!";

	private const string string_4 = "Error Equip!";

	private const string string_5 = "Instrument has eqiuped:";

	public bool hasInstruForm;

	public Instrument[] instruments = new Instrument[0];

	public Instrument[] pageInstrus = new Instrument[0];

	public CtrlModules setupModules = new CtrlModules();

	private string sCtrlHasEquiped => Class49.sysLanguage_0 switch
	{
		SysLanguage.CN => "该模块已配置到仪器\n请先从相关仪器移除！", 
		SysLanguage.EN => "Current control has equiped to instrument\nPlease remove from it,first!", 
		_ => "", 
	};

	private string sErrorEquip => Class49.sysLanguage_0 switch
	{
		SysLanguage.CN => "错误配置！", 
		SysLanguage.EN => "Error Equip!", 
		_ => "", 
	};

	private string sInstruEquiped => Class49.sysLanguage_0 switch
	{
		SysLanguage.CN => "仪器已配置：", 
		SysLanguage.EN => "Instrument has eqiuped:", 
		_ => "", 
	};

	public SysConfig()
	{
		pageInstrus = new Instrument[4];
		for (int i = 0; i < pageInstrus.Length; i++)
		{
			pageInstrus[i] = new Instrument();
		}
	}

	public SysCfgControl AddControlModule(SysCfgControl sysCfgControl)
	{
		SysCfgControl sysCfgControl2 = (SysCfgControl)sysCfgControl.Clone();
		setupModules.AddControlModule(sysCfgControl2);
		return sysCfgControl2;
	}

	public void CreateInstruForm()
	{
		for (int i = 0; i < pageInstrus.Length; i++)
		{
			pageInstrus[i].CreateForm();
		}
		pageInstrus[0].FormLocation = new Point(10, 10);
		pageInstrus[1].FormLocation = new Point(20 + pageInstrus[0].FormSize.Width, 10);
		pageInstrus[2].FormLocation = new Point(10, 20 + pageInstrus[0].FormSize.Height);
		pageInstrus[3].FormLocation = new Point(pageInstrus[1].FormLocation.X, pageInstrus[2].FormLocation.Y);
		hasInstruForm = true;
	}

	private void method_0()
	{
		pageInstrus[0].SetInstrumentStyle(InstruStyle.GC, 0);
		pageInstrus[1].SetInstrumentStyle(InstruStyle.LC, 1);
		pageInstrus[2].SetInstrumentStyle(InstruStyle.LC, 2);
		pageInstrus[3].SetInstrumentStyle(InstruStyle.LC, 3);
		if (Class49.edition_0 == Edition.VI2010G)
		{
			pageInstrus[1].SetInstrumentStyle(InstruStyle.GC, 1);
		}
		if (Class49.edition_0 == Edition.VI2010L || Class49.edition_0 == Edition.VI2010L2)
		{
			pageInstrus[0].SetInstrumentStyle(InstruStyle.LC, 0);
		}
		for (int i = 0; i < pageInstrus.Length; i++)
		{
			pageInstrus[i].DefaultSettings();
		}
		pageInstrus[2].name = pageInstrus[1].name + "2";
		pageInstrus[3].name = pageInstrus[1].name + "3";
		if (Class49.edition_0 == Edition.Clarify)
		{
			pageInstrus[0].setuped = true;
			pageInstrus[1].setuped = true;
			pageInstrus[2].setuped = true;
			pageInstrus[3].setuped = true;
		}
		else
		{
			pageInstrus[0].setuped = true;
			pageInstrus[1].setuped = true;
			pageInstrus[2].setuped = false;
			pageInstrus[3].setuped = false;
		}
		LinkInstrusFromPageInstruments();
	}

	private void method_1()
	{
		if (Class49.edition_0 == Edition.VI2010)
		{
			AddControlModule(AvaiDirversDlg.avaiDirvers.controlModules.liquidControls[1]).InitCreate();
			AddControlModule(AvaiDirversDlg.avaiDirvers.controlModules.sets[0]).InitCreate();
			AddControlModule(AvaiDirversDlg.avaiDirvers.controlModules.sets[0]).InitCreate();
		}
		if (Class49.edition_0 == Edition.VI2010G)
		{
			AddControlModule(AvaiDirversDlg.avaiDirvers.controlModules.gasControls[0]).InitCreate();
			AddControlModule(AvaiDirversDlg.avaiDirvers.controlModules.gasControls[0]).InitCreate();
			AddControlModule(AvaiDirversDlg.avaiDirvers.controlModules.detectors[0]).InitCreate();
			AddControlModule(AvaiDirversDlg.avaiDirvers.controlModules.detectors[0]).InitCreate();
		}
		if (Class49.edition_0 == Edition.VI2010L)
		{
			AddControlModule(AvaiDirversDlg.avaiDirvers.controlModules.sets[1]).InitCreate();
			AddControlModule(AvaiDirversDlg.avaiDirvers.controlModules.sets[1]).InitCreate();
		}
		if (Class49.edition_0 == Edition.VI2010L2)
		{
			AddControlModule(AvaiDirversDlg.avaiDirvers.controlModules.sets[2]).InitCreate();
			AddControlModule(AvaiDirversDlg.avaiDirvers.controlModules.sets[2]).InitCreate();
		}
		if (Class49.edition_0 == Edition.Clarify)
		{
			AddControlModule(AvaiDirversDlg.avaiDirvers.controlModules.gasControls[0]).InitCreate();
			AddControlModule(AvaiDirversDlg.avaiDirvers.controlModules.gasControls[0]).InitCreate();
			AddControlModule(AvaiDirversDlg.avaiDirvers.controlModules.gasControls[0]).InitCreate();
			AddControlModule(AvaiDirversDlg.avaiDirvers.controlModules.gasControls[0]).InitCreate();
			AddControlModule(AvaiDirversDlg.avaiDirvers.controlModules.liquidControls[1]).InitCreate();
			AddControlModule(AvaiDirversDlg.avaiDirvers.controlModules.detectors[0]).InitCreate();
			AddControlModule(AvaiDirversDlg.avaiDirvers.controlModules.detectors[0]).InitCreate();
			AddControlModule(AvaiDirversDlg.avaiDirvers.controlModules.detectors[0]).InitCreate();
			AddControlModule(AvaiDirversDlg.avaiDirvers.controlModules.detectors[0]).InitCreate();
			AddControlModule(AvaiDirversDlg.avaiDirvers.controlModules.sets[1]).InitCreate();
		}
	}

	public void DefaultSystemConfig()
	{
		method_1();
		method_0();
		try
		{
			if (Class49.edition_0 == Edition.VI2010)
			{
				EquipCMtoInstrument(setupModules.sets[0].bsCtrls[0], 0);
				EquipCMtoInstrument(setupModules.liquidControls[0].bsCtrls[0], 1);
				EquipCMtoInstrument(setupModules.sets[0].bsCtrls[1], 1);
			}
			if (Class49.edition_0 == Edition.VI2010G)
			{
				EquipCMtoInstrument(setupModules.gasControls[0].bsCtrls[0], 0);
				EquipCMtoInstrument(setupModules.detectors[0].bsCtrls[0], 0);
				EquipCMtoInstrument(setupModules.detectors[0].bsCtrls[1], 0);
				EquipCMtoInstrument(setupModules.detectors[0].bsCtrls[2], 0);
				EquipCMtoInstrument(setupModules.detectors[0].bsCtrls[3], 0);
				EquipCMtoInstrument(setupModules.gasControls[1].bsCtrls[0], 1);
				EquipCMtoInstrument(setupModules.detectors[1].bsCtrls[0], 1);
				EquipCMtoInstrument(setupModules.detectors[1].bsCtrls[1], 1);
				EquipCMtoInstrument(setupModules.detectors[1].bsCtrls[2], 1);
				EquipCMtoInstrument(setupModules.detectors[1].bsCtrls[3], 1);
			}
			if (Class49.edition_0 == Edition.VI2010L)
			{
				EquipCMtoInstrument(setupModules.sets[0].bsCtrls[0], 0);
				EquipCMtoInstrument(setupModules.sets[0].bsCtrls[2], 0);
				EquipCMtoInstrument(setupModules.sets[1].bsCtrls[0], 1);
				EquipCMtoInstrument(setupModules.sets[1].bsCtrls[2], 1);
			}
			if (Class49.edition_0 == Edition.VI2010L2)
			{
				EquipCMtoInstrument(setupModules.sets[0].bsCtrls[0], 0);
				EquipCMtoInstrument(setupModules.sets[0].bsCtrls[1], 0);
				EquipCMtoInstrument(setupModules.sets[0].bsCtrls[2], 0);
				EquipCMtoInstrument(setupModules.sets[0].bsCtrls[5], 0);
				EquipCMtoInstrument(setupModules.sets[1].bsCtrls[0], 1);
				EquipCMtoInstrument(setupModules.sets[1].bsCtrls[2], 1);
			}
			if (Class49.edition_0 == Edition.Clarify)
			{
				EquipCMtoInstrument(setupModules.gasControls[0].bsCtrls[0], 0);
				EquipCMtoInstrument(setupModules.detectors[0].bsCtrls[0], 0);
				EquipCMtoInstrument(setupModules.detectors[0].bsCtrls[1], 0);
				EquipCMtoInstrument(setupModules.detectors[0].bsCtrls[2], 0);
				EquipCMtoInstrument(setupModules.detectors[0].bsCtrls[3], 0);
				EquipCMtoInstrument(setupModules.sets[0].bsCtrls[0], 1);
				EquipCMtoInstrument(setupModules.sets[0].bsCtrls[2], 1);
			}
		}
		catch
		{
		}
	}

	public bool DeleteControlModule(SysCfgControl sysCfgControl)
	{
		return setupModules.DeleteControlModule(sysCfgControl);
	}

	private void method_2(Instrument instrument_0, SysCfgControl sysCfgControl_0, int int_0)
	{
		for (int i = 0; i < sysCfgControl_0.bsCtrls.Length; i++)
		{
			BaseControl baseControl;
			if ((baseControl = sysCfgControl_0.bsCtrls[i]).equipedInstruNo == int_0)
			{
				if (baseControl is ASC_Sampler)
				{
					int num = instrument_0.asc_Samplers.Length;
					Array.Resize(ref instrument_0.asc_Samplers, num + 1);
					instrument_0.asc_Samplers[num] = baseControl as ASC_Sampler;
				}
				else if (baseControl is GCC_GCs)
				{
					int num2 = instrument_0.gcc_GCss.Length;
					Array.Resize(ref instrument_0.gcc_GCss, num2 + 1);
					instrument_0.gcc_GCss[num2] = baseControl as GCC_GCs;
				}
				else if (baseControl is LCC_Pump)
				{
					int num3 = instrument_0.lcc_Pumps.Length;
					Array.Resize(ref instrument_0.lcc_Pumps, num3 + 1);
					instrument_0.lcc_Pumps[num3] = baseControl as LCC_Pump;
				}
				else if (baseControl is DtC_Channel)
				{
					int num4 = instrument_0.dtc_Channels.Length;
					Array.Resize(ref instrument_0.dtc_Channels, num4 + 1);
					instrument_0.dtc_Channels[num4] = baseControl as DtC_Channel;
				}
				else if (!(baseControl is LCG_LC2))
				{
				}
			}
		}
	}

	private void method_3(ref Instrument instrument_0, SysCfgControl sysCfgControl_0, int int_0)
	{
		for (int i = 0; i < sysCfgControl_0.bsCtrls.Length; i++)
		{
			BaseControl baseControl;
			if ((baseControl = sysCfgControl_0.bsCtrls[i]).equipedInstruNo == int_0)
			{
				if (baseControl is ASC_Sampler)
				{
					int num = instrument_0.asc_Samplers.Length;
					Array.Resize(ref instrument_0.asc_Samplers, num + 1);
					instrument_0.asc_Samplers[num] = baseControl as ASC_Sampler;
				}
				else if (baseControl is GCC_GCs)
				{
					int num2 = instrument_0.gcc_GCss.Length;
					Array.Resize(ref instrument_0.gcc_GCss, num2 + 1);
					instrument_0.gcc_GCss[num2] = baseControl as GCC_GCs;
				}
				else if (baseControl is LCC_Pump)
				{
					int num3 = instrument_0.lcc_Pumps.Length;
					Array.Resize(ref instrument_0.lcc_Pumps, num3 + 1);
					instrument_0.lcc_Pumps[num3] = baseControl as LCC_Pump;
				}
				else if (baseControl is DtC_Channel)
				{
					int num4 = instrument_0.dtc_Channels.Length;
					Array.Resize(ref instrument_0.dtc_Channels, num4 + 1);
					instrument_0.dtc_Channels[num4] = baseControl as DtC_Channel;
				}
				else if (!(baseControl is LCG_LC2))
				{
				}
			}
		}
	}

	public bool EquipCMtoInstrument(BaseControl baseControl, int instruNo)
	{
		if (baseControl != null)
		{
			if (baseControl.equipedInstruNo != -1)
			{
				if (baseControl.equipedInstruNo != instruNo)
				{
					MessageBox.Show(sCtrlHasEquiped, Lang.PS("错误", "Error"), MessageBoxButtons.OK, MessageBoxIcon.Hand);
				}
			}
			else
			{
				ControlModule cmStyle = baseControl.cmStyle;
				InstruStyle instruStyle = pageInstrus[instruNo].instruStyle;
				bool flag = true;
				switch (instruStyle)
				{
				case InstruStyle.GC:
					if (cmStyle == ControlModule.GasControl && pageInstrus[instruNo].gcc_GCss.Length != 0)
					{
						MessageBox.Show(sInstruEquiped + setupModules.gasControls[0].Name);
						return false;
					}
					if (cmStyle == ControlModule.Pump)
					{
						flag = false;
					}
					break;
				case InstruStyle.LC:
					if (cmStyle == ControlModule.GasControl)
					{
						flag = false;
					}
					break;
				}
				if (flag)
				{
					baseControl.equipedInstruNo = instruNo;
					return true;
				}
				MessageBox.Show(sErrorEquip, Lang.PS("错误", "Error"), MessageBoxButtons.OK, MessageBoxIcon.Hand);
			}
		}
		return false;
	}

	public int GetInstrumentsNum()
	{
		return instruments.Length;
	}

	public int GetLoggedInstrusNum()
	{
		int num = 0;
		for (int i = 0; i < instruments.Length; i++)
		{
			if (instruments[i].logged)
			{
				num++;
			}
		}
		return num;
	}

	public void LinkInstrusFromPageInstruments()
	{
		int num = 0;
		for (int i = 0; i < pageInstrus.Length; i++)
		{
			if (pageInstrus[i].setuped)
			{
				num++;
			}
		}
		Array.Resize(ref instruments, num);
		num = 0;
		for (int j = 0; j < pageInstrus.Length; j++)
		{
			if (pageInstrus[j].setuped)
			{
				instruments[num++] = pageInstrus[j];
			}
		}
	}

	public void LoadFromFile(BinaryReader binaryReader_0)
	{
		setupModules.LoadFromFile(binaryReader_0);
		int num = binaryReader_0.ReadInt32();
		Array.Resize(ref pageInstrus, num);
		for (int i = 0; i < num; i++)
		{
			if (pageInstrus[i] == null)
			{
				pageInstrus[i] = new Instrument();
			}
			pageInstrus[i].LoadFromFile(binaryReader_0);
			pageInstrus[i].LoadClosedOpenedImage();
		}
		LinkInstrusFromPageInstruments();
	}

	public void LoadFromObject(SysConfig sysConfig)
	{
		setupModules.LoadFromObject(sysConfig.setupModules);
		for (int i = 0; i < pageInstrus.Length; i++)
		{
			pageInstrus[i].LoadFromObject(sysConfig.pageInstrus[i]);
		}
		LinkInstrusFromPageInstruments();
	}

	public void PageInstruRefreshCtrls(int pgInstruNo)
	{
		Instrument instrument = pageInstrus[pgInstruNo];
		Array.Resize(ref instrument.asc_Samplers, 0);
		Array.Resize(ref instrument.gcc_GCss, 0);
		Array.Resize(ref instrument.lcc_Pumps, 0);
		Array.Resize(ref instrument.dtc_Channels, 0);
		for (int i = 0; i < setupModules.autoSamplers.Length; i++)
		{
			method_2(instrument, setupModules.autoSamplers[i], pgInstruNo);
		}
		for (int j = 0; j < setupModules.gasControls.Length; j++)
		{
			method_2(instrument, setupModules.gasControls[j], pgInstruNo);
		}
		for (int k = 0; k < setupModules.liquidControls.Length; k++)
		{
			method_2(instrument, setupModules.liquidControls[k], pgInstruNo);
		}
		for (int l = 0; l < setupModules.detectors.Length; l++)
		{
			method_2(instrument, setupModules.detectors[l], pgInstruNo);
		}
		for (int m = 0; m < setupModules.sets.Length; m++)
		{
			method_2(instrument, setupModules.sets[m], pgInstruNo);
		}
	}

	public Instrument PageInstruRefreshCtrls(ref Instrument pgInstru, int pgInstruNo)
	{
		Array.Resize(ref pgInstru.asc_Samplers, 0);
		Array.Resize(ref pgInstru.gcc_GCss, 0);
		Array.Resize(ref pgInstru.lcc_Pumps, 0);
		Array.Resize(ref pgInstru.dtc_Channels, 0);
		for (int i = 0; i < setupModules.autoSamplers.Length; i++)
		{
			method_3(ref pgInstru, setupModules.autoSamplers[i], pgInstruNo);
		}
		for (int j = 0; j < setupModules.gasControls.Length; j++)
		{
			method_3(ref pgInstru, setupModules.gasControls[j], pgInstruNo);
		}
		for (int k = 0; k < setupModules.liquidControls.Length; k++)
		{
			method_3(ref pgInstru, setupModules.liquidControls[k], pgInstruNo);
		}
		for (int l = 0; l < setupModules.detectors.Length; l++)
		{
			method_3(ref pgInstru, setupModules.detectors[l], pgInstruNo);
		}
		for (int m = 0; m < setupModules.sets.Length; m++)
		{
			method_3(ref pgInstru, setupModules.sets[m], pgInstruNo);
		}
		return pgInstru;
	}

	public void RemoveCMfromInstrument(BaseControl baseControl)
	{
		baseControl.equipedInstruNo = -1;
	}

	public Instrument RetInstrument(int instrumentNo)
	{
		if (instrumentNo >= instruments.Length)
		{
			return null;
		}
		return instruments[instrumentNo];
	}

	public void SaveToFile(BinaryWriter binaryWriter_0)
	{
		setupModules.SaveToFile(binaryWriter_0);
		binaryWriter_0.Write(pageInstrus.Length);
		for (int i = 0; i < pageInstrus.Length; i++)
		{
			pageInstrus[i].SaveToFile(binaryWriter_0);
		}
	}
}
