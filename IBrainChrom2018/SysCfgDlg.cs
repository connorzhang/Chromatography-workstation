using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace IBrainChrom2018;

public class SysCfgDlg : LclDialog
{
	public struct e__FixedBuffer8
	{
		public byte byte_0;
	}

	public static AvaiDirversDlg dlgAvaiDirvers = new AvaiDirversDlg();

	public static object[] hardWares = new object[0];

	private LclComboBox cbType;

	private ContextMenuStrip cmsControls;

	private ContextMenuStrip cmsCtrls;

	private IContainer icontainer_1;

	private Instrument instrument_0;

	private LclLabel lbClosedImage;

	private LclLabel lbInstrusNum;

	private LclLabel lbName;

	private LclLabel lbOpenedImage;

	private LclLabel lbType;

	private SysConfig sysConfig_0 = new SysConfig();

	private ToolStripMenuItem miCtrlsAbout;

	private ToolStripMenuItem miCtrlsAdd;

	private ToolStripMenuItem miCtrlsAddToInstru;

	private ToolStripMenuItem miCtrlsFindInstru;

	private ToolStripMenuItem miCtrlsProperty;

	private ToolStripMenuItem miCtrlsRemove;

	private ToolStripMenuItem miICRemove;

	private ToolStripMenuItem miICRemoveAll;

	private LclNumericUpDown nudInstrusNum;

	private LclPictureBox pbClosed;

	private LclPictureBox pbOpened;

	private LclPanel pnlInstruSet;

	private StringFormat stringFormat_0 = new StringFormat();

	private StringFormat stringFormat_1 = new StringFormat();

	private StringFormat stringFormat_2 = new StringFormat();

	public static SysConfig sysConfig = new SysConfig();

	private TabPage tabPage1;

	private TabPage tabPage2;

	private LclTextBox tbName;

	private TabControl tcInstruments;

	private TreeNode treeNode_0;

	private TreeNode treeNode_1;

	private TreeNode treeNode_2;

	private TreeNode treeNode_3;

	private TreeNode treeNode_4;

	private TreeNode treeNode_5;

	private TreeNode treeNode_6;

	private TreeNode treeNode_7;

	private TreeNode treeNode_8;

	private ToolStripSeparator toolStripSeparator1;

	public TabPage tpInstru0 = new TabPage();

	public TabPage tpInstru1 = new TabPage();

	public TabPage tpInstru2 = new TabPage();

	public TabPage tpInstru3 = new TabPage();

	private LclTV tvControls;

	private IContainer components;

	private DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;

	private DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;

	private DataGridViewTextBoxColumn dataGridViewTextBoxColumn6;

	private DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;

	private DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;

	private DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;

	private LclTV tvCtrls;

	private string sPgName => Lang.PS("仪器", "Instru");

	public SysCfgDlg()
	{
		InitializeComponent();
		tvControls.ImageList = SystemImageListResource2.smethod_1();
		tvControls.HeaderWidth1 = 40;
		tvControls.HeaderWidth2 = 80;
		tvControls.HeaderWidth0 = tvControls.Width - tvControls.HeaderWidth1 - tvControls.HeaderWidth2 - 2;
		stringFormat_2.Alignment = StringAlignment.Center;
		stringFormat_0.Alignment = StringAlignment.Near;
		treeNode_1 = tvControls.Nodes.Add(ControlModule.AutoSampler.ToString());
		treeNode_7 = tvControls.Nodes.Add(ControlModule.Pump.ToString());
		treeNode_4 = tvControls.Nodes.Add(ControlModule.GasControl.ToString());
		treeNode_2 = tvControls.Nodes.Add(ControlModule.Detector.ToString());
		treeNode_8 = tvControls.Nodes.Add("套件");
		treeNode_1.ImageIndex = (treeNode_7.ImageIndex = (treeNode_4.ImageIndex = (treeNode_2.ImageIndex = (treeNode_8.ImageIndex = SystemImageListResource2.int_5))));
		treeNode_1.SelectedImageIndex = (treeNode_7.SelectedImageIndex = (treeNode_4.SelectedImageIndex = (treeNode_2.SelectedImageIndex = (treeNode_8.SelectedImageIndex = SystemImageListResource2.int_6))));
		dlgAvaiDirvers.OnAddControlModule = (AvaiDirversDlg.AddControlModule)Delegate.Combine(dlgAvaiDirvers.OnAddControlModule, new AvaiDirversDlg.AddControlModule(method_0));
		tcInstruments.ImageList = SystemImageListResource2.smethod_1();
		tcInstruments.TabPages.Clear();
		tcInstruments.TabPages.Add(tpInstru0);
		tcInstruments.TabPages.Add(tpInstru1);
		tcInstruments.TabPages.Add(tpInstru2);
		tcInstruments.TabPages.Add(tpInstru3);
		tcInstruments.Height = 22;
		pnlInstruSet.Location = new Point(tcInstruments.Left, tcInstruments.Bottom - 1);
		pnlInstruSet.Size = new Size(tcInstruments.Width - 2, tvControls.Bottom - pnlInstruSet.Top);
		cbType.Items.Add(InstruStyle.GC);
		cbType.Items.Add(InstruStyle.LC);
		tvCtrls.ImageList = SystemImageListResource2.smethod_1();
		tvCtrls.Location = new Point(-1, pbClosed.Bottom + 15);
		tvCtrls.Size = new Size(pnlInstruSet.Width, pnlInstruSet.Height - tvCtrls.Top - 1);
		tvCtrls.HeaderWidth1 = 120;
		tvCtrls.HeaderWidth0 = tvCtrls.Width - tvCtrls.HeaderWidth1 - 2;
		stringFormat_1.Alignment = StringAlignment.Near;
		treeNode_0 = tvCtrls.Nodes.Add(ControlModule.AutoSampler.ToString());
		treeNode_6 = tvCtrls.Nodes.Add(ControlModule.Pump.ToString());
		treeNode_5 = tvCtrls.Nodes.Add(ControlModule.GasControl.ToString());
		treeNode_3 = tvCtrls.Nodes.Add(ControlModule.Detector.ToString());
		treeNode_0.ImageIndex = (treeNode_6.ImageIndex = (treeNode_5.ImageIndex = (treeNode_3.ImageIndex = SystemImageListResource2.int_5)));
		treeNode_0.SelectedImageIndex = (treeNode_6.SelectedImageIndex = (treeNode_5.SelectedImageIndex = (treeNode_3.SelectedImageIndex = SystemImageListResource2.int_6)));
		if (Class49.edition_0 == Edition.Clarify)
		{
			nudInstrusNum.Value = 4m;
			nudInstrusNum.Enabled = true;
			Control control = cmsControls;
			cmsCtrls.Enabled = true;
			control.Enabled = true;
		}
		else
		{
			nudInstrusNum.Value = 2m;
			nudInstrusNum.Enabled = false;
			ToolStripItem toolStripItem = miCtrlsAdd;
			miCtrlsRemove.Enabled = false;
			toolStripItem.Enabled = false;
		}
	}

	private void method_0(SysCfgControl sysCfgControl_0)
	{
		SysCfgControl sysCfgControl = sysConfig_0.AddControlModule(sysCfgControl_0);
		tvControls.DrawMode = TreeViewDrawMode.Normal;
		TreeNode treeNode = null;
		switch (sysCfgControl_0.controlModule)
		{
		case ControlModule.AutoSampler:
		{
			treeNode = treeNode_1.Nodes.Add(sysCfgControl.Name);
			TreeNode treeNode6 = treeNode;
			int imageIndex = (treeNode.SelectedImageIndex = SystemImageListResource2.int_0);
			treeNode6.ImageIndex = imageIndex;
			break;
		}
		case ControlModule.Pump:
		{
			treeNode = treeNode_7.Nodes.Add(sysCfgControl.Name);
			TreeNode treeNode5 = treeNode;
			int imageIndex = (treeNode.SelectedImageIndex = SystemImageListResource2.int_10);
			treeNode5.ImageIndex = imageIndex;
			break;
		}
		case ControlModule.GasControl:
		{
			treeNode = treeNode_4.Nodes.Add(sysCfgControl.Name);
			TreeNode treeNode4 = treeNode;
			int imageIndex = (treeNode.SelectedImageIndex = SystemImageListResource2.int_7);
			treeNode4.ImageIndex = imageIndex;
			break;
		}
		case ControlModule.Detector:
		{
			treeNode = treeNode_2.Nodes.Add(sysCfgControl.Name);
			TreeNode treeNode3 = treeNode;
			int imageIndex = (treeNode.SelectedImageIndex = SystemImageListResource2.int_3);
			treeNode3.ImageIndex = imageIndex;
			break;
		}
		case ControlModule.Set:
		{
			treeNode = treeNode_8.Nodes.Add(sysCfgControl.Name);
			TreeNode treeNode2 = treeNode;
			int imageIndex = (treeNode.SelectedImageIndex = SystemImageListResource2.int_16);
			treeNode2.ImageIndex = imageIndex;
			break;
		}
		}
		treeNode.Text = " " + sysCfgControl.Name;
		sysCfgControl.tnProduct = treeNode;
		treeNode.Tag = sysCfgControl;
		tvControls.DrawMode = TreeViewDrawMode.OwnerDrawText;
		smethod_2(treeNode);
	}

	private void method_1(object sender, EventArgs e)
	{
		sysConfig.LoadFromObject(sysConfig_0);
		PageInstruRefreshCtrls();
		MainForm.stationAdtTrlForm.RefreshMeanus(sysConfig.instruments.Length);
	}

	private void cbType_SelectedIndexChanged(object sender, EventArgs e)
	{
		instrument_0.instruStyle = (InstruStyle)cbType.SelectedIndex;
		tvCtrls.Nodes.Clear();
		tvCtrls.Nodes.Add(treeNode_0);
		switch (instrument_0.instruStyle)
		{
		case InstruStyle.GC:
		{
			tvCtrls.Nodes.Add(treeNode_5);
			for (int j = 0; j < instrument_0.lcc_Pumps.Length; j++)
			{
				instrument_0.lcc_Pumps[j].equipedInstruNo = -1;
			}
			treeNode_6.Nodes.Clear();
			for (int k = 0; k < instrument_0.dtc_Channels.Length; k++)
			{
				if (instrument_0.dtc_Channels[k].detectorStyle == DetectorStyle.DAD)
				{
					instrument_0.dtc_Channels[k].equipedInstruNo = -1;
				}
			}
			if (treeNode_3.Nodes.Count == 0)
			{
				break;
			}
			for (int num = treeNode_3.Nodes.Count - 1; num >= 0; num--)
			{
				if (treeNode_3.Nodes[num].ImageIndex == SystemImageListResource2.int_4)
				{
					treeNode_3.Nodes[num].Remove();
				}
			}
			break;
		}
		case InstruStyle.LC:
		{
			tvCtrls.Nodes.Add(treeNode_6);
			for (int i = 0; i < instrument_0.gcc_GCss.Length; i++)
			{
				instrument_0.gcc_GCss[i].equipedInstruNo = -1;
			}
			treeNode_5.Nodes.Clear();
			break;
		}
		}
		tvCtrls.Nodes.Add(treeNode_3);
		tvControls.Refresh();
	}

	private static void smethod_0(TreeNode treeNode_9)
	{
		if (treeNode_9.Nodes.Count != 0)
		{
			for (int num = treeNode_9.Nodes.Count - 1; num >= 0; num--)
			{
				smethod_3(treeNode_9.Nodes[num]);
			}
			treeNode_9.Nodes.Clear();
		}
	}

	private void cmsControls_Opening(object sender, CancelEventArgs e)
	{
		if (tvControls.SelectedNode != null)
		{
			if (Class49.edition_0 == Edition.Clarify)
			{
				miCtrlsRemove.Enabled = tvControls.SelectedNode.Level == 1;
			}
			miCtrlsProperty.Enabled = tvControls.SelectedNode.Level == 1;
			miCtrlsAbout.Enabled = tvControls.SelectedNode.Level == 1;
			miCtrlsAddToInstru.Enabled = tvControls.SelectedNode.Level == 2;
			miCtrlsFindInstru.Enabled = tvControls.SelectedNode.Level == 2;
		}
		else
		{
			miCtrlsRemove.Enabled = false;
			miCtrlsProperty.Enabled = false;
			miCtrlsAbout.Enabled = false;
			miCtrlsAddToInstru.Enabled = false;
			miCtrlsFindInstru.Enabled = false;
		}
	}

	private void cmsCtrls_Opening(object sender, CancelEventArgs e)
	{
		if (tvCtrls.SelectedNode != null)
		{
			miICRemove.Enabled = tvCtrls.SelectedNode.Level == 1;
		}
		else
		{
			miICRemove.Enabled = false;
		}
	}

	public void EndDevice(string productString)
	{
		for (int i = 0; i < hardWares.Length; i++)
		{
			if (hardWares[i] is UsbSZ)
			{
				UsbSZ usbSZ = hardWares[i] as UsbSZ;
				if (productString == null || usbSZ.productString == productString)
				{
					method_2(usbSZ, bool_1: false);
					usbSZ.EndDevice();
					int num = hardWares.Length - 1;
					hardWares[i] = hardWares[num];
					Array.Resize(ref hardWares, num);
				}
			}
			else if (hardWares[i] is Class10)
			{
				Class10 @class = hardWares[i] as Class10;
				if (productString == null || @class.string_0 == productString)
				{
					method_3(@class, bool_1: false);
					@class.method_2();
					int num2 = hardWares.Length - 1;
					hardWares[i] = hardWares[num2];
					Array.Resize(ref hardWares, num2);
				}
			}
		}
	}

	private void method_2(UsbSZ usbSZ_0, bool bool_1)
	{
		if ((bool_1 && usbSZ_0.installed) || (!bool_1 && !usbSZ_0.installed))
		{
			return;
		}
		SysConfig sysConfig = (base.Visible ? sysConfig_0 : SysCfgDlg.sysConfig);
		for (int i = 0; i < usbSZ_0.applyCMs.Length; i++)
		{
			SysCfgControl[] array = null;
			switch (usbSZ_0.applyCMs[i])
			{
			case ControlModule.AutoSampler:
				array = sysConfig.setupModules.autoSamplers;
				break;
			case ControlModule.Pump:
				array = sysConfig.setupModules.liquidControls;
				break;
			case ControlModule.GasControl:
				array = sysConfig.setupModules.gasControls;
				break;
			case ControlModule.Detector:
				array = sysConfig.setupModules.detectors;
				break;
			case ControlModule.Set:
				array = sysConfig.setupModules.sets;
				break;
			}
			if (array == null)
			{
				continue;
			}
			if (Class49.edition_0 == Edition.VI2010)
			{
				for (int j = 0; j < array.Length; j++)
				{
					if (bool_1 && array[j].HardWare == null && array[j].hwStyle == usbSZ_0.hwStyle)
					{
						array[j].HardWare = usbSZ_0;
						break;
					}
					if (!bool_1 && array[j].HardWare != null && array[j].hwStyle == usbSZ_0.hwStyle && array[j].HardStr == usbSZ_0.productString)
					{
						array[j].HardWare = null;
					}
				}
				continue;
			}
			for (int k = 0; k < array.Length; k++)
			{
				if (bool_1 && array[k].HardWare == null && array[k].hwStyle == usbSZ_0.hwStyle && array[k].hardString == usbSZ_0.productString)
				{
					array[k].HardWare = usbSZ_0;
					break;
				}
				if (!bool_1 && array[k].HardWare != null && array[k].hwStyle == usbSZ_0.hwStyle && array[k].HardStr == usbSZ_0.productString)
				{
					array[k].HardWare = null;
				}
			}
		}
	}

	private void method_3(Class10 class10_0, bool bool_1)
	{
		if ((bool_1 && class10_0.bool_0) || (!bool_1 && !class10_0.bool_0))
		{
			return;
		}
		SysConfig sysConfig = (base.Visible ? sysConfig_0 : SysCfgDlg.sysConfig);
		for (int i = 0; i < class10_0.controlModule_0.Length; i++)
		{
			SysCfgControl[] array = null;
			switch (class10_0.controlModule_0[i])
			{
			case ControlModule.AutoSampler:
				array = sysConfig.setupModules.autoSamplers;
				break;
			case ControlModule.Pump:
				array = sysConfig.setupModules.liquidControls;
				break;
			case ControlModule.GasControl:
				array = sysConfig.setupModules.gasControls;
				break;
			case ControlModule.Detector:
				array = sysConfig.setupModules.detectors;
				break;
			case ControlModule.Set:
				array = sysConfig.setupModules.sets;
				break;
			}
			if (array == null)
			{
				continue;
			}
			for (int j = 0; j < array.Length; j++)
			{
				if (bool_1 && array[j].HardWare == null && array[j].hwStyle == class10_0.hwStyle_0 && array[j].hardString == class10_0.string_0)
				{
					array[j].HardWare = class10_0;
					break;
				}
				if (!bool_1 && array[j].HardWare != null && array[j].hwStyle == class10_0.hwStyle_0 && array[j].HardStr == class10_0.string_0)
				{
					array[j].HardWare = null;
				}
			}
		}
	}

	public override void LoadLanguage()
	{
		base.LoadLanguage();
		Text = Lang.PS("系统配置", "System Configuration");
		tvControls.HeaderText0 = Lang.PS("系统已安装模块", "System Setup Controls");
		tvControls.HeaderText1 = Lang.PS("标号", "No");
		tvControls.HeaderText2 = Lang.PS("配置", "Equiped");
		TreeNode treeNode = treeNode_1;
		string text = (treeNode_0.Text = Lang.PS("自动进样器", "AutoSampler"));
		treeNode.Text = text;
		TreeNode treeNode2 = treeNode_7;
		text = (treeNode_6.Text = Lang.PS("液相控制", "LiquidControl"));
		treeNode2.Text = text;
		TreeNode treeNode3 = treeNode_4;
		text = (treeNode_5.Text = Lang.PS("气相控制", "GasControl"));
		treeNode3.Text = text;
		TreeNode treeNode4 = treeNode_2;
		text = (treeNode_3.Text = Lang.PS("检测器", "Detector"));
		treeNode4.Text = text;
		treeNode_8.Text = Lang.PS("套件", "Set");
		miCtrlsAdd.Text = Lang.PS("添加", "Add");
		miCtrlsRemove.Text = Lang.PS("移除", "Remove");
		miCtrlsProperty.Text = Lang.PS("属性...", "Property...");
		miCtrlsAbout.Text = Lang.PS("关于...", "About...");
		miCtrlsAddToInstru.Text = Lang.PS("配置到仪器", "Equip To Instrument");
		miCtrlsFindInstru.Text = Lang.PS("定位仪器", "Find Instrument");
		miICRemove.Text = Lang.PS("移除", "Remove");
		miICRemoveAll.Text = Lang.PS("移除全部", "RemoveAll");
		lbInstrusNum.Text = Lang.PS("仪器数", "Instrus Num");
		tpInstru0.Text = Lang.PS("仪器1", "Instru1");
		tpInstru1.Text = Lang.PS("仪器2", "Instru2");
		tpInstru2.Text = Lang.PS("仪器3", "Instru3");
		tpInstru3.Text = Lang.PS("仪器4", "Instru4");
		lbType.Text = Lang.PS("类型", "Type");
		lbName.Text = Lang.PS("名称", "Name");
		lbClosedImage.Text = Lang.PS("仪器关闭图片", "Image for Closed Instru");
		lbOpenedImage.Text = Lang.PS("仪器运行图片", "Image for Opened Instru");
		tvCtrls.HeaderText0 = Lang.PS("安装模块", "Setup Controls");
		tvCtrls.HeaderText1 = Lang.PS("来自", "From");
	}

	private void miCtrlsAbout_Click(object sender, EventArgs e)
	{
		(tvControls.SelectedNode.Tag as SysCfgControl).ShowAboutDialog();
	}

	private void miCtrlsAdd_Click(object sender, EventArgs e)
	{
		dlgAvaiDirvers.ShowDialog();
	}

	private void miCtrlsAddToInstru_Click(object sender, EventArgs e)
	{
		int selectedIndex = tcInstruments.SelectedIndex;
		if (sysConfig_0.EquipCMtoInstrument(tvControls.SelectedNode.Tag as BaseControl, selectedIndex))
		{
			tvControls.Refresh();
			method_4();
		}
	}

	private void miCtrlsFindInstru_Click(object sender, EventArgs e)
	{
		tcInstruments.SelectedIndex = (tvControls.SelectedNode.Tag as BaseControl).equipedInstruNo;
	}

	private void miCtrlsProperty_Click(object sender, EventArgs e)
	{
		if ((tvControls.SelectedNode.Tag as SysCfgControl).ShowDialog() == DialogResult.OK)
		{
			smethod_2(tvControls.SelectedNode);
			method_4();
		}
	}

	private void miCtrlsRemove_Click(object sender, EventArgs e)
	{
		TreeNode selectedNode = tvControls.SelectedNode;
		if (sysConfig_0.DeleteControlModule(selectedNode.Tag as SysCfgControl))
		{
			selectedNode.Nodes.Clear();
			selectedNode.Remove();
			method_4();
		}
	}

	private void miICRemove_Click(object sender, EventArgs e)
	{
		sysConfig_0.RemoveCMfromInstrument(tvCtrls.SelectedNode.Tag as BaseControl);
		tvControls.Refresh();
		method_4();
	}

	private void miICRemoveAll_Click(object sender, EventArgs e)
	{
		for (int i = 0; i < treeNode_0.Nodes.Count; i++)
		{
			sysConfig_0.RemoveCMfromInstrument(treeNode_0.Nodes[i].Tag as BaseControl);
		}
		for (int j = 0; j < treeNode_6.Nodes.Count; j++)
		{
			sysConfig_0.RemoveCMfromInstrument(treeNode_6.Nodes[j].Tag as BaseControl);
		}
		for (int k = 0; k < treeNode_5.Nodes.Count; k++)
		{
			sysConfig_0.RemoveCMfromInstrument(treeNode_5.Nodes[k].Tag as BaseControl);
		}
		for (int l = 0; l < treeNode_3.Nodes.Count; l++)
		{
			sysConfig_0.RemoveCMfromInstrument(treeNode_3.Nodes[l].Tag as BaseControl);
		}
		tvControls.Refresh();
		method_4();
	}

	private static void smethod_1(TreeNode treeNode_9, ControlModule controlModule_0, BaseControl[] baseControl_0)
	{
		smethod_0(treeNode_9);
		if (controlModule_0 == ControlModule.None || baseControl_0 == null)
		{
			return;
		}
		int num = 0;
		for (int i = 0; i < baseControl_0.Length; i++)
		{
			BaseControl baseControl = baseControl_0[i];
			TreeNode treeNode = treeNode_9.Nodes.Add(baseControl.name);
			if (baseControl is ASC_Sampler || baseControl is ASC_LC2)
			{
				num = SystemImageListResource2.int_1;
			}
			if (baseControl is GCC_GCs)
			{
				num = SystemImageListResource2.int_8;
			}
			if (baseControl is LCC_Pump || baseControl is LCC_LC2)
			{
				num = SystemImageListResource2.int_13;
			}
			if (baseControl is DtC_Channel)
			{
				num = SystemImageListResource2.int_2;
			}
			if (baseControl is LCG_LC2)
			{
				num = SystemImageListResource2.int_11;
			}
			if (baseControl is Ovn_LC2)
			{
				num = SystemImageListResource2.int_12;
			}
			int imageIndex = (treeNode.SelectedImageIndex = num);
			treeNode.ImageIndex = imageIndex;
			treeNode.Tag = baseControl_0[i];
		}
	}

	private void nudInstrusNum_ValueChanged(object sender, EventArgs e)
	{
		tpInstru0.ImageIndex = SystemImageListResource2.int_9;
		tpInstru1.ImageIndex = SystemImageListResource2.int_9;
		tpInstru2.ImageIndex = SystemImageListResource2.int_9;
		tpInstru3.ImageIndex = SystemImageListResource2.int_9;
		if (nudInstrusNum.Value == 1m)
		{
			tpInstru1.ImageIndex = SystemImageListResource2.int_14;
			tpInstru2.ImageIndex = SystemImageListResource2.int_14;
			tpInstru3.ImageIndex = SystemImageListResource2.int_14;
		}
		else if (nudInstrusNum.Value == 2m)
		{
			tpInstru2.ImageIndex = SystemImageListResource2.int_14;
			tpInstru3.ImageIndex = SystemImageListResource2.int_14;
		}
		else if (nudInstrusNum.Value == 3m)
		{
			tpInstru3.ImageIndex = SystemImageListResource2.int_14;
		}
		else
		{
			nudInstrusNum.Value = 4m;
		}
		sysConfig_0.pageInstrus[0].setuped = tpInstru0.ImageIndex == SystemImageListResource2.int_9;
		sysConfig_0.pageInstrus[1].setuped = tpInstru1.ImageIndex == SystemImageListResource2.int_9;
		sysConfig_0.pageInstrus[2].setuped = tpInstru2.ImageIndex == SystemImageListResource2.int_9;
		sysConfig_0.pageInstrus[3].setuped = tpInstru3.ImageIndex == SystemImageListResource2.int_9;
	}

	public void PageInstruRefreshCtrls()
	{
		sysConfig.PageInstruRefreshCtrls(0);
		sysConfig.PageInstruRefreshCtrls(1);
		sysConfig.PageInstruRefreshCtrls(2);
		sysConfig.PageInstruRefreshCtrls(3);
	}

	private void pbClosed_Click(object sender, EventArgs e)
	{
		bool flag = sender == pbOpened;
		if (instrument_0.SetImageDialog(flag))
		{
			if (flag)
			{
				(sender as PictureBox).Image = instrument_0.openedImage;
			}
			else
			{
				(sender as PictureBox).Image = instrument_0.closedImage;
			}
		}
	}

	public static void RefreshRootModuleNode(TreeNode root, SysCfgControl[] sysCfgControls, int imgIndex, bool refreshBaseCtrls, bool lcl_proc)
	{
		smethod_0(root);
		for (int i = 0; i < sysCfgControls.Length; i++)
		{
			if (lcl_proc)
			{
				root.TreeView.DrawMode = TreeViewDrawMode.Normal;
			}
			TreeNode treeNode = root.Nodes.Add(sysCfgControls[i].ShowName);
			if (lcl_proc)
			{
				root.TreeView.DrawMode = TreeViewDrawMode.OwnerDrawText;
			}
			TreeNode treeNode2 = treeNode;
			treeNode.SelectedImageIndex = imgIndex;
			treeNode2.ImageIndex = imgIndex;
			treeNode.Tag = sysCfgControls[i];
			sysCfgControls[i].tnProduct = treeNode;
			if (refreshBaseCtrls)
			{
				smethod_2(treeNode);
			}
		}
	}

	private static void smethod_2(TreeNode treeNode_9)
	{
		if (treeNode_9.Tag != null)
		{
			SysCfgControl sysCfgControl = treeNode_9.Tag as SysCfgControl;
			smethod_1(treeNode_9, sysCfgControl.controlModule, sysCfgControl.bsCtrls);
		}
	}

	private void method_4()
	{
		sysConfig_0.PageInstruRefreshCtrls(tcInstruments.SelectedIndex);
		smethod_1(treeNode_0, ControlModule.AutoSampler, instrument_0.asc_Samplers);
		smethod_1(treeNode_5, ControlModule.GasControl, instrument_0.gcc_GCss);
		smethod_1(treeNode_6, ControlModule.Pump, instrument_0.lcc_Pumps);
		smethod_1(treeNode_3, ControlModule.Detector, instrument_0.dtc_Channels);
	}

	private static void smethod_3(TreeNode treeNode_9)
	{
		if (treeNode_9.Nodes.Count != 0)
		{
			for (int num = treeNode_9.Nodes.Count - 1; num >= 0; num--)
			{
				smethod_3(treeNode_9.Nodes[num]);
			}
		}
		else
		{
			treeNode_9.Remove();
		}
	}

	public void ScanHardWares()
	{
	}

	public new DialogResult ShowDialog()
	{
		if (Class49.loginDlg_0.ShowDialog(AccessType.OpenConfiguration))
		{
			return base.ShowDialog();
		}
		return DialogResult.Cancel;
	}

	private void SysCfgDlg_FormClosing(object sender, FormClosingEventArgs e)
	{
		SysCfgControl[] array = null;
		for (int i = 0; i < hardWares.Length; i++)
		{
			if (hardWares[i] is UsbSZ)
			{
				UsbSZ usbSZ = hardWares[i] as UsbSZ;
				for (int j = 0; j < usbSZ.applyCMs.Length; j++)
				{
					switch (usbSZ.applyCMs[j])
					{
					case ControlModule.AutoSampler:
						array = sysConfig.setupModules.autoSamplers;
						break;
					case ControlModule.Pump:
						array = sysConfig.setupModules.liquidControls;
						break;
					case ControlModule.GasControl:
						array = sysConfig.setupModules.gasControls;
						break;
					case ControlModule.Detector:
						array = sysConfig.setupModules.detectors;
						break;
					case ControlModule.Set:
						array = sysConfig.setupModules.sets;
						break;
					}
					usbSZ.installed = false;
					if (array == null)
					{
						continue;
					}
					for (int k = 0; k < array.Length; k++)
					{
						if (array[k].HardWare != null && array[k].hwStyle == usbSZ.hwStyle && array[k].hardString == usbSZ.productString)
						{
							usbSZ.installed = true;
							break;
						}
					}
				}
				continue;
			}
			Class10 @class = hardWares[i] as Class10;
			for (int l = 0; l < @class.controlModule_0.Length; l++)
			{
				switch (@class.controlModule_0[l])
				{
				case ControlModule.AutoSampler:
					array = sysConfig.setupModules.autoSamplers;
					break;
				case ControlModule.Pump:
					array = sysConfig.setupModules.liquidControls;
					break;
				case ControlModule.GasControl:
					array = sysConfig.setupModules.gasControls;
					break;
				case ControlModule.Detector:
					array = sysConfig.setupModules.detectors;
					break;
				case ControlModule.Set:
					array = sysConfig.setupModules.sets;
					break;
				}
				@class.bool_0 = false;
				if (array == null)
				{
					continue;
				}
				for (int m = 0; m < array.Length; m++)
				{
					if (array[m].HardWare != null && array[m].hwStyle == @class.hwStyle_0 && array[m].hardString == @class.string_0)
					{
						@class.bool_0 = true;
						break;
					}
				}
			}
		}
	}

	private void SysCfgDlg_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode == Keys.F1)
		{
			if (tvControls.SelectedNode != null && tvControls.SelectedNode.Text.Contains("赛智采集卡"))
			{
				Class49.smethod_32("赛智采集卡");
			}
			else if (tvControls.SelectedNode != null && tvControls.SelectedNode.Text.Contains("赛智液相控制器"))
			{
				Class49.smethod_32("赛智液相控制器");
			}
			else
			{
				Class49.smethod_32("模块");
			}
		}
	}

	private void SysCfgDlg_Load(object sender, EventArgs e)
	{
		sysConfig_0.LoadFromObject(sysConfig);
		RefreshRootModuleNode(treeNode_1, sysConfig_0.setupModules.autoSamplers, SystemImageListResource2.int_0, refreshBaseCtrls: true, lcl_proc: true);
		RefreshRootModuleNode(treeNode_7, sysConfig_0.setupModules.liquidControls, SystemImageListResource2.int_10, refreshBaseCtrls: true, lcl_proc: true);
		RefreshRootModuleNode(treeNode_4, sysConfig_0.setupModules.gasControls, SystemImageListResource2.int_7, refreshBaseCtrls: true, lcl_proc: true);
		RefreshRootModuleNode(treeNode_2, sysConfig_0.setupModules.detectors, SystemImageListResource2.int_3, refreshBaseCtrls: true, lcl_proc: true);
		RefreshRootModuleNode(treeNode_8, sysConfig_0.setupModules.sets, SystemImageListResource2.int_16, refreshBaseCtrls: true, lcl_proc: true);
		nudInstrusNum.Value = sysConfig_0.GetInstrumentsNum();
		tcInstruments_SelectedIndexChanged(null, null);
	}

	private void tbName_TextChanged(object sender, EventArgs e)
	{
		instrument_0.name = tbName.Text;
	}

	private void tcInstruments_SelectedIndexChanged(object sender, EventArgs e)
	{
		int selectedIndex = tcInstruments.SelectedIndex;
		if (selectedIndex >= 0)
		{
			instrument_0 = sysConfig_0.pageInstrus[selectedIndex];
			cbType.SelectedIndex = (int)instrument_0.instruStyle;
			tbName.Text = instrument_0.name;
			pbClosed.Image = instrument_0.closedImage;
			pbOpened.Image = instrument_0.openedImage;
			treeNode_0.Tag = instrument_0.asc_Samplers;
			treeNode_5.Tag = instrument_0.gcc_GCss;
			treeNode_6.Tag = instrument_0.lcc_Pumps;
			treeNode_3.Tag = instrument_0.dtc_Channels;
			method_4();
		}
	}

	private void tvControls_DrawNode(object sender, DrawTreeNodeEventArgs e)
	{
		if (e.Node.Level == 1 && e.Node.Tag != null)
		{
			SysCfgControl sysCfgControl = e.Node.Tag as SysCfgControl;
			Rectangle rectangle = new Rectangle(tvControls.HeaderWidth0, e.Bounds.Top, tvControls.HeaderWidth1, e.Bounds.Height + 1);
			int num = sysCfgControl.No + 1;
			e.Graphics.DrawString(num.ToString(), tvControls.Font, Brushes.Black, rectangle, stringFormat_2);
		}
		if (e.Node.Level == 2 && e.Node.Tag != null)
		{
			BaseControl baseControl = e.Node.Tag as BaseControl;
			if (baseControl.equipedInstruNo != -1)
			{
				Rectangle rectangle2 = new Rectangle(tvControls.HeaderWidth0 + tvControls.HeaderWidth1, e.Bounds.Top, tvControls.HeaderWidth2, e.Bounds.Height + 1);
				int num2 = baseControl.equipedInstruNo + 1;
				e.Graphics.DrawString(sPgName + num2, tvControls.Font, Brushes.Black, rectangle2, stringFormat_0);
			}
		}
	}

	private void tvControls_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
	{
		if (e.Node.Level == 1)
		{
			miCtrlsProperty_Click(null, null);
		}
	}

	private void tvCtrls_DrawNode(object sender, DrawTreeNodeEventArgs e)
	{
		if (e.Node.Level == 1 && e.Node.Tag != null)
		{
			BaseControl baseControl = e.Node.Tag as BaseControl;
			Rectangle rectangle = new Rectangle(tvCtrls.HeaderWidth0, e.Bounds.Top, tvCtrls.HeaderWidth1, e.Bounds.Height + 1);
			e.Graphics.DrawString(baseControl.from.Name, tvCtrls.Font, Brushes.Black, rectangle, stringFormat_1);
		}
	}

	protected override void Dispose(bool disposing)
	{
		if (disposing && icontainer_1 != null)
		{
			icontainer_1.Dispose();
		}
		base.Dispose(disposing);
	}

	private void InitializeComponent()
	{
		this.components = new System.ComponentModel.Container();
		this.cmsControls = new System.Windows.Forms.ContextMenuStrip(this.components);
		this.miCtrlsAdd = new System.Windows.Forms.ToolStripMenuItem();
		this.miCtrlsRemove = new System.Windows.Forms.ToolStripMenuItem();
		this.miCtrlsProperty = new System.Windows.Forms.ToolStripMenuItem();
		this.miCtrlsAbout = new System.Windows.Forms.ToolStripMenuItem();
		this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
		this.miCtrlsAddToInstru = new System.Windows.Forms.ToolStripMenuItem();
		this.miCtrlsFindInstru = new System.Windows.Forms.ToolStripMenuItem();
		this.lbInstrusNum = new IBrainChrom2018.LclLabel();
		this.nudInstrusNum = new IBrainChrom2018.LclNumericUpDown();
		this.pnlInstruSet = new IBrainChrom2018.LclPanel();
		this.tvCtrls = new IBrainChrom2018.LclTV();
		this.cmsCtrls = new System.Windows.Forms.ContextMenuStrip(this.components);
		this.miICRemove = new System.Windows.Forms.ToolStripMenuItem();
		this.miICRemoveAll = new System.Windows.Forms.ToolStripMenuItem();
		this.pbOpened = new IBrainChrom2018.LclPictureBox();
		this.pbClosed = new IBrainChrom2018.LclPictureBox();
		this.tbName = new IBrainChrom2018.LclTextBox();
		this.cbType = new IBrainChrom2018.LclComboBox();
		this.lbOpenedImage = new IBrainChrom2018.LclLabel();
		this.lbClosedImage = new IBrainChrom2018.LclLabel();
		this.lbName = new IBrainChrom2018.LclLabel();
		this.lbType = new IBrainChrom2018.LclLabel();
		this.tvControls = new IBrainChrom2018.LclTV();
		this.tcInstruments = new System.Windows.Forms.TabControl();
		this.tabPage1 = new System.Windows.Forms.TabPage();
		this.tabPage2 = new System.Windows.Forms.TabPage();
		this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.dataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.dataGridViewTextBoxColumn6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.cmsControls.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.nudInstrusNum).BeginInit();
		this.pnlInstruSet.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.tvCtrls).BeginInit();
		this.cmsCtrls.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.pbOpened).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.pbClosed).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.tvControls).BeginInit();
		this.tcInstruments.SuspendLayout();
		base.SuspendLayout();
		base.btnCancel.Location = new System.Drawing.Point(361, 367);
		base.btnCancel.Text = "取消";
		base.btnHelp.Location = new System.Drawing.Point(453, 367);
		base.btnHelp.Text = "帮助";
		base.btnOK.Location = new System.Drawing.Point(271, 367);
		base.btnOK.Text = "确认";
		base.btnOK.Click += new System.EventHandler(method_1);
		this.cmsControls.Items.AddRange(new System.Windows.Forms.ToolStripItem[7] { this.miCtrlsAdd, this.miCtrlsRemove, this.miCtrlsProperty, this.miCtrlsAbout, this.toolStripSeparator1, this.miCtrlsAddToInstru, this.miCtrlsFindInstru });
		this.cmsControls.Name = "cmsControls";
		this.cmsControls.Size = new System.Drawing.Size(193, 142);
		this.cmsControls.Opening += new System.ComponentModel.CancelEventHandler(cmsControls_Opening);
		this.miCtrlsAdd.Name = "miCtrlsAdd";
		this.miCtrlsAdd.Size = new System.Drawing.Size(192, 22);
		this.miCtrlsAdd.Text = "添加";
		this.miCtrlsAdd.Click += new System.EventHandler(miCtrlsAdd_Click);
		this.miCtrlsRemove.Name = "miCtrlsRemove";
		this.miCtrlsRemove.Size = new System.Drawing.Size(192, 22);
		this.miCtrlsRemove.Text = "移除";
		this.miCtrlsRemove.Click += new System.EventHandler(miCtrlsRemove_Click);
		this.miCtrlsProperty.Name = "miCtrlsProperty";
		this.miCtrlsProperty.Size = new System.Drawing.Size(192, 22);
		this.miCtrlsProperty.Text = "属性...";
		this.miCtrlsProperty.Click += new System.EventHandler(miCtrlsProperty_Click);
		this.miCtrlsAbout.Name = "miCtrlsAbout";
		this.miCtrlsAbout.Size = new System.Drawing.Size(192, 22);
		this.miCtrlsAbout.Text = "关于...";
		this.miCtrlsAbout.Click += new System.EventHandler(miCtrlsAbout_Click);
		this.toolStripSeparator1.Name = "toolStripSeparator1";
		this.toolStripSeparator1.Size = new System.Drawing.Size(189, 6);
		this.miCtrlsAddToInstru.Name = "miCtrlsAddToInstru";
		this.miCtrlsAddToInstru.Size = new System.Drawing.Size(192, 22);
		this.miCtrlsAddToInstru.Text = "配置到仪器";
		this.miCtrlsAddToInstru.Click += new System.EventHandler(miCtrlsAddToInstru_Click);
		this.miCtrlsFindInstru.Name = "miCtrlsFindInstru";
		this.miCtrlsFindInstru.Size = new System.Drawing.Size(192, 22);
		this.miCtrlsFindInstru.Text = "定位仪器";
		this.miCtrlsFindInstru.Click += new System.EventHandler(miCtrlsFindInstru_Click);
		this.lbInstrusNum.Location = new System.Drawing.Point(315, 12);
		this.lbInstrusNum.Name = "lbInstrusNum";
		this.lbInstrusNum.Size = new System.Drawing.Size(105, 12);
		this.lbInstrusNum.TabIndex = 3;
		this.lbInstrusNum.Text = "仪器数";
		this.lbInstrusNum.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.nudInstrusNum.Location = new System.Drawing.Point(426, 7);
		this.nudInstrusNum.Maximum = new decimal(new int[4] { 1, 0, 0, 0 });
		this.nudInstrusNum.Minimum = new decimal(new int[4] { 1, 0, 0, 0 });
		this.nudInstrusNum.Name = "nudInstrusNum";
		this.nudInstrusNum.Size = new System.Drawing.Size(51, 21);
		this.nudInstrusNum.TabIndex = 4;
		this.nudInstrusNum.Value = new decimal(new int[4] { 1, 0, 0, 0 });
		this.nudInstrusNum.ValueChanged += new System.EventHandler(nudInstrusNum_ValueChanged);
		this.pnlInstruSet.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.pnlInstruSet.Controls.Add(this.tvCtrls);
		this.pnlInstruSet.Controls.Add(this.pbOpened);
		this.pnlInstruSet.Controls.Add(this.pbClosed);
		this.pnlInstruSet.Controls.Add(this.tbName);
		this.pnlInstruSet.Controls.Add(this.cbType);
		this.pnlInstruSet.Controls.Add(this.lbOpenedImage);
		this.pnlInstruSet.Controls.Add(this.lbClosedImage);
		this.pnlInstruSet.Controls.Add(this.lbName);
		this.pnlInstruSet.Controls.Add(this.lbType);
		this.pnlInstruSet.Location = new System.Drawing.Point(284, 78);
		this.pnlInstruSet.Name = "pnlInstruSet";
		this.pnlInstruSet.Size = new System.Drawing.Size(277, 283);
		this.pnlInstruSet.TabIndex = 5;
		this.tvCtrls.AllowUserToResizeColumns = false;
		this.tvCtrls.BorderStyle = System.Windows.Forms.BorderStyle.None;
		this.tvCtrls.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
		this.tvCtrls.Columns.AddRange(this.dataGridViewTextBoxColumn2, this.dataGridViewTextBoxColumn3, this.dataGridViewTextBoxColumn4);
		this.tvCtrls.ContextMenuStrip = this.cmsCtrls;
		this.tvCtrls.DrawMode = System.Windows.Forms.TreeViewDrawMode.OwnerDrawText;
		this.tvCtrls.HeaderWidth0 = 150;
		this.tvCtrls.HeaderWidth1 = 5;
		this.tvCtrls.HeaderWidth2 = 5;
		this.tvCtrls.ImageList = null;
		this.tvCtrls.Location = new System.Drawing.Point(20, 167);
		this.tvCtrls.Name = "tvCtrls";
		this.tvCtrls.RowHeadersVisible = false;
		this.tvCtrls.RowTemplate.Height = 23;
		this.tvCtrls.ScrollBars = System.Windows.Forms.ScrollBars.None;
		this.tvCtrls.Size = new System.Drawing.Size(240, 61);
		this.tvCtrls.TabIndex = 6;
		this.tvCtrls.DrawNode += new System.Windows.Forms.DrawTreeNodeEventHandler(tvCtrls_DrawNode);
		this.cmsCtrls.Items.AddRange(new System.Windows.Forms.ToolStripItem[2] { this.miICRemove, this.miICRemoveAll });
		this.cmsCtrls.Name = "cmsCtrls";
		this.cmsCtrls.Size = new System.Drawing.Size(193, 48);
		this.cmsCtrls.Opening += new System.ComponentModel.CancelEventHandler(cmsCtrls_Opening);
		this.miICRemove.Name = "miICRemove";
		this.miICRemove.Size = new System.Drawing.Size(192, 22);
		this.miICRemove.Text = "移除";
		this.miICRemove.Click += new System.EventHandler(miICRemove_Click);
		this.miICRemoveAll.Name = "miICRemoveAll";
		this.miICRemoveAll.Size = new System.Drawing.Size(192, 22);
		this.miICRemoveAll.Text = "移除全部";
		this.miICRemoveAll.Click += new System.EventHandler(miICRemoveAll_Click);
		this.pbOpened.Location = new System.Drawing.Point(140, 49);
		this.pbOpened.Name = "pbOpened";
		this.pbOpened.Size = new System.Drawing.Size(130, 100);
		this.pbOpened.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
		this.pbOpened.TabIndex = 3;
		this.pbOpened.TabStop = false;
		this.pbOpened.Click += new System.EventHandler(pbClosed_Click);
		this.pbClosed.Location = new System.Drawing.Point(4, 49);
		this.pbClosed.Name = "pbClosed";
		this.pbClosed.Size = new System.Drawing.Size(130, 100);
		this.pbClosed.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
		this.pbClosed.TabIndex = 3;
		this.pbClosed.TabStop = false;
		this.pbClosed.Click += new System.EventHandler(pbClosed_Click);
		this.tbName.Location = new System.Drawing.Point(156, 6);
		this.tbName.Name = "tbName";
		this.tbName.Size = new System.Drawing.Size(114, 21);
		this.tbName.TabIndex = 2;
		this.tbName.TextChanged += new System.EventHandler(tbName_TextChanged);
		this.cbType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.cbType.FormattingEnabled = true;
		this.cbType.ItemExtString = "";
		this.cbType.Location = new System.Drawing.Point(50, 6);
		this.cbType.Name = "cbType";
		this.cbType.Size = new System.Drawing.Size(55, 20);
		this.cbType.TabIndex = 1;
		this.cbType.SelectedIndexChanged += new System.EventHandler(cbType_SelectedIndexChanged);
		this.lbOpenedImage.Location = new System.Drawing.Point(140, 34);
		this.lbOpenedImage.Name = "lbOpenedImage";
		this.lbOpenedImage.Size = new System.Drawing.Size(130, 12);
		this.lbOpenedImage.TabIndex = 0;
		this.lbOpenedImage.Text = "仪器运行图片";
		this.lbOpenedImage.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
		this.lbClosedImage.Location = new System.Drawing.Point(1, 34);
		this.lbClosedImage.Name = "lbClosedImage";
		this.lbClosedImage.Size = new System.Drawing.Size(133, 12);
		this.lbClosedImage.TabIndex = 0;
		this.lbClosedImage.Text = "仪器关闭图片";
		this.lbClosedImage.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
		this.lbName.AutoSize = true;
		this.lbName.Location = new System.Drawing.Point(117, 9);
		this.lbName.Name = "lbName";
		this.lbName.Size = new System.Drawing.Size(29, 12);
		this.lbName.TabIndex = 0;
		this.lbName.Text = "名称";
		this.lbType.AutoSize = true;
		this.lbType.Location = new System.Drawing.Point(6, 9);
		this.lbType.Name = "lbType";
		this.lbType.Size = new System.Drawing.Size(29, 12);
		this.lbType.TabIndex = 0;
		this.lbType.Text = "类型";
		this.tvControls.AllowUserToResizeColumns = false;
		this.tvControls.BorderStyle = System.Windows.Forms.BorderStyle.None;
		this.tvControls.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
		this.tvControls.Columns.AddRange(this.dataGridViewTextBoxColumn1, this.dataGridViewTextBoxColumn5, this.dataGridViewTextBoxColumn6);
		this.tvControls.ContextMenuStrip = this.cmsControls;
		this.tvControls.DrawMode = System.Windows.Forms.TreeViewDrawMode.OwnerDrawText;
		this.tvControls.HeaderWidth0 = 100;
		this.tvControls.HeaderWidth1 = 5;
		this.tvControls.HeaderWidth2 = 5;
		this.tvControls.ImageList = null;
		this.tvControls.Location = new System.Drawing.Point(3, 7);
		this.tvControls.Name = "tvControls";
		this.tvControls.RowHeadersVisible = false;
		this.tvControls.RowTemplate.Height = 23;
		this.tvControls.ScrollBars = System.Windows.Forms.ScrollBars.None;
		this.tvControls.Size = new System.Drawing.Size(277, 354);
		this.tvControls.TabIndex = 6;
		this.tvControls.DrawNode += new System.Windows.Forms.DrawTreeNodeEventHandler(tvControls_DrawNode);
		this.tvControls.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(tvControls_NodeMouseDoubleClick);
		this.tcInstruments.Controls.Add(this.tabPage1);
		this.tcInstruments.Controls.Add(this.tabPage2);
		this.tcInstruments.Location = new System.Drawing.Point(284, 34);
		this.tcInstruments.Name = "tcInstruments";
		this.tcInstruments.SelectedIndex = 0;
		this.tcInstruments.Size = new System.Drawing.Size(277, 38);
		this.tcInstruments.TabIndex = 7;
		this.tcInstruments.SelectedIndexChanged += new System.EventHandler(tcInstruments_SelectedIndexChanged);
		this.tabPage1.Location = new System.Drawing.Point(4, 22);
		this.tabPage1.Name = "tabPage1";
		this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
		this.tabPage1.Size = new System.Drawing.Size(269, 12);
		this.tabPage1.TabIndex = 0;
		this.tabPage1.Text = "tabPage1";
		this.tabPage1.UseVisualStyleBackColor = true;
		this.tabPage2.Location = new System.Drawing.Point(4, 22);
		this.tabPage2.Name = "tabPage2";
		this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
		this.tabPage2.Size = new System.Drawing.Size(269, 12);
		this.tabPage2.TabIndex = 1;
		this.tabPage2.Text = "tabPage2";
		this.tabPage2.UseVisualStyleBackColor = true;
		this.dataGridViewTextBoxColumn1.HeaderText = "系统已安装模块";
		this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
		this.dataGridViewTextBoxColumn5.HeaderText = "标号";
		this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
		this.dataGridViewTextBoxColumn5.Width = 5;
		this.dataGridViewTextBoxColumn6.HeaderText = "配置";
		this.dataGridViewTextBoxColumn6.Name = "dataGridViewTextBoxColumn6";
		this.dataGridViewTextBoxColumn6.Width = 5;
		this.dataGridViewTextBoxColumn2.HeaderText = "安装模块";
		this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
		this.dataGridViewTextBoxColumn2.Width = 150;
		this.dataGridViewTextBoxColumn3.HeaderText = "来自";
		this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
		this.dataGridViewTextBoxColumn3.Width = 5;
		this.dataGridViewTextBoxColumn4.HeaderText = "";
		this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
		this.dataGridViewTextBoxColumn4.Width = 5;
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
		base.ClientSize = new System.Drawing.Size(564, 397);
		base.Controls.Add(this.tcInstruments);
		base.Controls.Add(this.tvControls);
		base.Controls.Add(this.nudInstrusNum);
		base.Controls.Add(this.pnlInstruSet);
		base.Controls.Add(this.lbInstrusNum);
		base.Name = "SysCfgDlg";
		this.Text = "系统配置";
		base.FormClosing += new System.Windows.Forms.FormClosingEventHandler(SysCfgDlg_FormClosing);
		base.Load += new System.EventHandler(SysCfgDlg_Load);
		base.KeyDown += new System.Windows.Forms.KeyEventHandler(SysCfgDlg_KeyDown);
		base.Controls.SetChildIndex(this.lbInstrusNum, 0);
		base.Controls.SetChildIndex(this.pnlInstruSet, 0);
		base.Controls.SetChildIndex(this.nudInstrusNum, 0);
		base.Controls.SetChildIndex(base.btnCancel, 0);
		base.Controls.SetChildIndex(base.btnOK, 0);
		base.Controls.SetChildIndex(base.btnHelp, 0);
		base.Controls.SetChildIndex(this.tvControls, 0);
		base.Controls.SetChildIndex(this.tcInstruments, 0);
		this.cmsControls.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.nudInstrusNum).EndInit();
		this.pnlInstruSet.ResumeLayout(false);
		this.pnlInstruSet.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.tvCtrls).EndInit();
		this.cmsCtrls.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.pbOpened).EndInit();
		((System.ComponentModel.ISupportInitialize)this.pbClosed).EndInit();
		((System.ComponentModel.ISupportInitialize)this.tvControls).EndInit();
		this.tcInstruments.ResumeLayout(false);
		base.ResumeLayout(false);
	}
}
