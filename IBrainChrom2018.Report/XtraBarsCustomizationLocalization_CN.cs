using System.ComponentModel;
using System.Drawing;
using DevExpress.XtraBars.Customization;

namespace IBrainChrom2018.Report;

public class XtraBarsCustomizationLocalization_CN : CustomizationControl
{
	private Container components = null;

	public XtraBarsCustomizationLocalization_CN()
	{
		InitializeComponent();
	}

	protected override void Dispose(bool disposing)
	{
		if (disposing && components != null)
		{
			components.Dispose();
		}
		base.Dispose(disposing);
	}

	private void InitializeComponent()
	{
		base.tpOptions.SuspendLayout();
		base.tpCommands.SuspendLayout();
		base.tpToolbars.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)base.toolBarsList).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.lbCommands).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.lbCategories).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.cbOptionsShowFullMenus.Properties).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.cbOptions_showFullMenusAfterDelay.Properties).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.cbOptions_showTips.Properties).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.cbOptions_ShowShortcutInTips.Properties).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.tabControl).BeginInit();
		base.tabControl.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)base.tbNBDlgName.Properties).BeginInit();
		base.pnlNBDlg.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)base.cbOptions_largeIcons.Properties).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.cbOptions_MenuAnimation.Properties).BeginInit();
		base.SuspendLayout();
		base.btClose.Text = "关闭";
		base.btResetBar.Text = "重新设置(&R)...";
		base.btRenameBar.Text = "重命名(&E)...";
		base.btNewBar.Text = "新建(&N)...";
		base.btDeleteBar.Text = "删除(&D)";
		base.btOptions_Reset.Text = "重置惯用数据(&R)";
		base.btNBDlgCancel.Text = "取消";
		base.btNBDlgOk.Text = "确定";
		base.tpOptions.Size = new System.Drawing.Size(354, 246);
		base.tpOptions.Text = "选项(&O)";
		base.tpCommands.Text = "命令(&C)";
		base.tpToolbars.Text = "工具栏(&B)";
		base.cbOptionsShowFullMenus.Properties.Caption = "始终显示整个菜单(&N)";
		base.cbOptions_showFullMenusAfterDelay.Properties.Caption = "鼠标指针短暂停留后显示完整菜单(&U)";
		base.cbOptions_largeIcons.Properties.Caption = "大图标(&L)";
		base.cbOptions_showTips.Properties.Caption = "显示关于工具栏屏幕提示(&T)";
		base.cbOptions_ShowShortcutInTips.Properties.Caption = "在屏幕提示中显示快捷键(&H)";
		base.lbDescCaption.Text = "详细说明";
		base.lbOptions_Other.Text = "其它";
		base.lbOptions_PCaption.Text = "个性化菜单和工具栏";
		base.lbCategoriesCaption.Text = "类别:";
		base.lbCommandsCaption.Text = "命令:";
		base.lbToolbarCaption.Text = "工具栏:";
		base.lbOptions_MenuAnimation.Text = "菜单动画设置(&M):";
		base.lbNBDlgCaption.Text = "工具栏名称(&T)";
		base.lbCommands.Appearance.BackColor = System.Drawing.SystemColors.Window;
		base.lbCommands.Appearance.Options.UseBackColor = true;
		base.Name = "XtraBarsCustomizationLocalization_CN";
		base.tpOptions.ResumeLayout(false);
		base.tpCommands.ResumeLayout(false);
		base.tpToolbars.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)base.toolBarsList).EndInit();
		((System.ComponentModel.ISupportInitialize)base.lbCommands).EndInit();
		((System.ComponentModel.ISupportInitialize)base.lbCategories).EndInit();
		((System.ComponentModel.ISupportInitialize)base.cbOptionsShowFullMenus.Properties).EndInit();
		((System.ComponentModel.ISupportInitialize)base.cbOptions_showFullMenusAfterDelay.Properties).EndInit();
		((System.ComponentModel.ISupportInitialize)base.cbOptions_showTips.Properties).EndInit();
		((System.ComponentModel.ISupportInitialize)base.cbOptions_ShowShortcutInTips.Properties).EndInit();
		((System.ComponentModel.ISupportInitialize)base.tabControl).EndInit();
		base.tabControl.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)base.tbNBDlgName.Properties).EndInit();
		base.pnlNBDlg.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)base.cbOptions_largeIcons.Properties).EndInit();
		((System.ComponentModel.ISupportInitialize)base.cbOptions_MenuAnimation.Properties).EndInit();
		base.ResumeLayout(false);
	}
}
