using System;
using System.IO;
using System.Windows.Forms;

namespace IBrainChrom2018;

public class SysCfgControl
{
	protected CtrlAboutDlg aboutForm;

	public BaseControl[] bsCtrls = new BaseControl[0];

	public ControlModule controlModule;

	public string hardString = "";

	protected object hardWare;

	public HwStyle hwStyle;

	private static bool bool_0;

	public int No;

	public string scnName = "";

	protected string senName = "";

	protected CtrlSetupDlg setupForm;

	public TreeNode tnProduct;

	public string HardStr
	{
		get
		{
			if (hardWare != null)
			{
				if (hardWare is UsbSZ)
				{
					return (hardWare as UsbSZ).productString;
				}
				if (hardWare is Class10)
				{
					return (hardWare as Class10).string_0;
				}
			}
			return "";
		}
	}

	public object HardWare
	{
		get
		{
			return hardWare;
		}
		set
		{
			if (hardWare != null && hardWare is UsbSZ)
			{
				(hardWare as UsbSZ).Install(controlModule, installed: false);
			}
			hardWare = value;
			hardString = HardStr;
			if (tnProduct != null)
			{
				tnProduct.Text = ShowName;
			}
			if (hardWare != null && hardWare is UsbSZ)
			{
				(hardWare as UsbSZ).Install(controlModule, hardWare != null);
			}
		}
	}

	public string Name => Class49.sysLanguage_0 switch
	{
		SysLanguage.CN => scnName, 
		SysLanguage.EN => senName, 
		_ => throw new Exception("未指定语言"), 
	};

	public string ShowName
	{
		get
		{
			string text = ((HardWare != null) ? "†" : " ");
			return text + Name;
		}
	}

	public virtual object Clone()
	{
		return null;
	}

	public virtual void InitCreate()
	{
	}

	private static SysCfgControl smethod_0(BinaryReader binaryReader_0, SysCfgControl sysCfgControl_0)
	{
		bool flag = sysCfgControl_0 is SZ_Dt || sysCfgControl_0 is Control1;
		bool flag2 = sysCfgControl_0 is Control0;
		Array.Resize(ref sysCfgControl_0.bsCtrls, binaryReader_0.ReadInt32());
		for (int i = 0; i < sysCfgControl_0.bsCtrls.Length; i++)
		{
			switch (binaryReader_0.ReadByte())
			{
			case 0:
				if (flag2)
				{
					sysCfgControl_0.bsCtrls[i] = new ASC_LC2(sysCfgControl_0);
				}
				else
				{
					sysCfgControl_0.bsCtrls[i] = new ASC_Sampler(sysCfgControl_0);
				}
				break;
			case 1:
				if (flag2)
				{
					sysCfgControl_0.bsCtrls[i] = new LCC_LC2(sysCfgControl_0);
				}
				else
				{
					sysCfgControl_0.bsCtrls[i] = new LCC_Pump(sysCfgControl_0);
				}
				break;
			case 2:
				sysCfgControl_0.bsCtrls[i] = new LCG_LC2(sysCfgControl_0);
				break;
			case 3:
				sysCfgControl_0.bsCtrls[i] = new Ovn_LC2(sysCfgControl_0);
				break;
			case 4:
				sysCfgControl_0.bsCtrls[i] = (bool_0 ? new GC08_GCs(sysCfgControl_0) : new GCC_GCs(sysCfgControl_0));
				break;
			case 5:
				if (flag2)
				{
					sysCfgControl_0.bsCtrls[i] = new DtC_LC2(sysCfgControl_0);
				}
				else if (flag)
				{
					sysCfgControl_0.bsCtrls[i] = new DtC_Detector(sysCfgControl_0);
				}
				else
				{
					sysCfgControl_0.bsCtrls[i] = new DtC_Channel(sysCfgControl_0);
				}
				break;
			case 6:
				throw new Exception("检错组件");
			}
			sysCfgControl_0.bsCtrls[i].LoadFromFile(binaryReader_0);
		}
		return sysCfgControl_0;
	}

	public static void LoadFromFile(BinaryReader binaryReader_0, out SysCfgControl control)
	{
		bool_0 = false;
		string text;
		switch (text = binaryReader_0.ReadString())
		{
		case "赛尔泰自动进样器":
			control = smethod_0(binaryReader_0, new AutoSamplerControl1
			{
				hardString = binaryReader_0.ReadString()
			});
			break;
		case "浙大智达液相":
			control = smethod_0(binaryReader_0, new LiquidPump1
			{
				hardString = binaryReader_0.ReadString()
			});
			break;
		case "赛尔泰液相":
			control = smethod_0(binaryReader_0, new LiquidPump2
			{
				hardString = binaryReader_0.ReadString()
			});
			break;
		case "GC08":
		{
			GasChromControl1 gasChromControl = new GasChromControl1();
			gasChromControl.hardString = binaryReader_0.ReadString();
			bool_0 = true;
			control = smethod_0(binaryReader_0, gasChromControl);
			break;
		}
		case "智达气相":
			control = smethod_0(binaryReader_0, new GasChromControl2
			{
				hardString = binaryReader_0.ReadString()
			});
			break;
		case "浙大智达检测器":
			control = smethod_0(binaryReader_0, new DetectorControl1
			{
				hardString = binaryReader_0.ReadString()
			});
			break;
		case "赛智采集卡":
			control = smethod_0(binaryReader_0, new SZ_Dt
			{
				hardString = binaryReader_0.ReadString()
			});
			break;
		case "赛智液相控制器":
			control = smethod_0(binaryReader_0, new Control1
			{
				hardString = binaryReader_0.ReadString()
			});
			break;
		case "赛智新液相":
			control = smethod_0(binaryReader_0, new Control0
			{
				hardString = binaryReader_0.ReadString()
			});
			break;
		default:
			control = null;
			MessageBox.Show("未知");
			break;
		}
	}

	public virtual void SaveToFile(BinaryWriter binaryWriter_0)
	{
		binaryWriter_0.Write(scnName);
		if (hardWare == null)
		{
			hardString = "";
		}
		binaryWriter_0.Write(hardString);
		binaryWriter_0.Write(bsCtrls.Length);
		for (int i = 0; i < bsCtrls.Length; i++)
		{
			bsCtrls[i].SaveToFile(binaryWriter_0);
		}
	}

	public virtual void ShowAboutDialog()
	{
		if (aboutForm != null)
		{
			aboutForm.ShowDialog();
		}
	}

	public virtual DialogResult ShowDialog()
	{
		if (setupForm == null)
		{
			return DialogResult.Cancel;
		}
		return setupForm.ShowDialog();
	}
}
