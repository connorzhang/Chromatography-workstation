using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace IBrainChrom2018;

public class AvaiDirversDlg : LclDialog
{
	public delegate void AddControlModule(SysCfgControl sysCfgControl);

	private const string string_0 = "可用驱动（控制模块）";

	private const string string_1 = "Available Drivers(Control Modules)";

	public static AvaiDirvers avaiDirvers = new AvaiDirvers();

	private IContainer icontainer_1;

	public AddControlModule OnAddControlModule;

	public SysCfgControl sysCfgControl;

	private TreeNode treeNode_0;

	private TreeNode treeNode_1;

	private TreeNode treeNode_2;

	private TreeNode treeNode_3;

	private TreeNode treeNode_4;

	private LclTreeView tvCMs;

	public AvaiDirversDlg()
	{
		InitializeComponent_1();
		tvCMs.ImageList = SystemImageListResource2.smethod_1();
		avaiDirvers.SysAvaiDirvers();
		method_1();
	}

	private void method_0(object sender, EventArgs e)
	{
		if (sysCfgControl != null)
		{
			sysCfgControl.InitCreate();
			if (sysCfgControl.ShowDialog() == DialogResult.OK && OnAddControlModule != null)
			{
				OnAddControlModule(sysCfgControl);
			}
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

	private void method_1()
	{
		treeNode_0 = tvCMs.Nodes.Add(ControlModule.AutoSampler.ToString());
		TreeNode treeNode = treeNode_0;
		int imageIndex = (treeNode_0.SelectedImageIndex = SystemImageListResource2.int_0);
		treeNode.ImageIndex = imageIndex;
		treeNode_3 = tvCMs.Nodes.Add(ControlModule.Pump.ToString());
		TreeNode treeNode2 = treeNode_3;
		imageIndex = (treeNode_3.SelectedImageIndex = SystemImageListResource2.int_10);
		treeNode2.ImageIndex = imageIndex;
		treeNode_2 = tvCMs.Nodes.Add(ControlModule.GasControl.ToString());
		TreeNode treeNode3 = treeNode_2;
		imageIndex = (treeNode_2.SelectedImageIndex = SystemImageListResource2.int_7);
		treeNode3.ImageIndex = imageIndex;
		treeNode_1 = tvCMs.Nodes.Add(ControlModule.Detector.ToString());
		TreeNode treeNode4 = treeNode_1;
		imageIndex = (treeNode_1.SelectedImageIndex = SystemImageListResource2.int_3);
		treeNode4.ImageIndex = imageIndex;
		treeNode_4 = tvCMs.Nodes.Add(ControlModule.Set.ToString());
		TreeNode treeNode5 = treeNode_4;
		imageIndex = (treeNode_4.SelectedImageIndex = SystemImageListResource2.int_16);
		treeNode5.ImageIndex = imageIndex;
		SysCfgDlg.RefreshRootModuleNode(treeNode_0, avaiDirvers.controlModules.autoSamplers, SystemImageListResource2.int_15, refreshBaseCtrls: false, lcl_proc: false);
		SysCfgDlg.RefreshRootModuleNode(treeNode_3, avaiDirvers.controlModules.liquidControls, SystemImageListResource2.int_15, refreshBaseCtrls: false, lcl_proc: false);
		SysCfgDlg.RefreshRootModuleNode(treeNode_2, avaiDirvers.controlModules.gasControls, SystemImageListResource2.int_15, refreshBaseCtrls: false, lcl_proc: false);
		SysCfgDlg.RefreshRootModuleNode(treeNode_1, avaiDirvers.controlModules.detectors, SystemImageListResource2.int_15, refreshBaseCtrls: false, lcl_proc: false);
		SysCfgDlg.RefreshRootModuleNode(treeNode_4, avaiDirvers.controlModules.sets, SystemImageListResource2.int_15, refreshBaseCtrls: false, lcl_proc: false);
	}

	private void InitializeComponent_1()
	{
		tvCMs = new LclTreeView();
		SuspendLayout();
		btnOK.Location = new Point(35, 407);
		btnOK.Click += method_0;
		btnCancel.Location = new Point(125, 407);
		btnHelp.Location = new Point(217, 407);
		tvCMs.Location = new Point(12, 12);
		tvCMs.Name = "tvCMs";
		tvCMs.Size = new Size(321, 389);
		tvCMs.TabIndex = 1;
		tvCMs.NodeMouseDoubleClick += tvCMs_NodeMouseDoubleClick;
		tvCMs.AfterSelect += tvCMs_AfterSelect;
		tvCMs.MouseDown += tvCMs_MouseDown;
		base.AutoScaleDimensions = new SizeF(6f, 12f);
		base.ClientSize = new Size(345, 442);
		base.Controls.Add(tvCMs);
		base.Name = "AvaiDirversDlg";
		base.Controls.SetChildIndex(btnCancel, 0);
		base.Controls.SetChildIndex(btnOK, 0);
		base.Controls.SetChildIndex(btnHelp, 0);
		base.Controls.SetChildIndex(tvCMs, 0);
		ResumeLayout(performLayout: false);
	}

	public override void LoadLanguage()
	{
		base.LoadLanguage();
		switch (Class49.sysLanguage_0)
		{
		case SysLanguage.CN:
			Text = "可用驱动（控制模块）";
			btnOK.Text = "添加";
			btnCancel.Text = "关闭";
			treeNode_0.Text = "自动进样器";
			treeNode_3.Text = "液相控制";
			treeNode_2.Text = "气相控制";
			treeNode_1.Text = "检测器";
			treeNode_4.Text = "套件";
			break;
		case SysLanguage.EN:
			Text = "Available Drivers(Control Modules)";
			btnOK.Text = "Add";
			btnCancel.Text = "Close";
			treeNode_0.Text = "AutoSampler";
			treeNode_3.Text = "LiquidControl";
			treeNode_2.Text = "GasControl";
			treeNode_1.Text = "Detector";
			break;
		}
	}

	private void tvCMs_AfterSelect(object sender, TreeViewEventArgs e)
	{
		btnOK.Enabled = e.Node.Level == 1;
		sysCfgControl = (SysCfgControl)e.Node.Tag;
	}

	private void tvCMs_MouseDown(object sender, MouseEventArgs e)
	{
		tvCMs.SelectedNode = tvCMs.GetNodeAt(e.X, e.Y);
	}

	private void tvCMs_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
	{
		method_0(null, null);
	}
}
