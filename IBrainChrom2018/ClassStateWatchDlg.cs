using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.Utils;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraVerticalGrid;
using DevExpress.XtraVerticalGrid.Rows;
using IBrainChrom2018.Unit;

namespace IBrainChrom2018;

public class ClassStateWatchDlg : Form
{
	private AsyncTcpServer m_asyncTcpServer;

	private SystemParam sysParam = SystemParam.Create();

	private SystemParamProperty sysPropertyParam = SystemParamProperty.Create();

	private bool bLoading = true;

	private IContainer components = null;

	private Button btnApply;

	private Panel panel1;

	private TabControl tablectrl;

	private TabPage tabPage1;

	private RepositoryItemColorEdit repositoryItemColorEdit5;

	private RepositoryItemCheckEdit repositoryItemCheckEdit5;

	private RepositoryItemPictureEdit repositoryItemPictureEdit5;

	private RepositoryItemTrackBar repositoryItemTrackBar1;

	private RepositoryItemSpinEdit riseNumic;

	private RepositoryItemPopupContainerEdit ripceEdit;

	private RepositoryItemMRUEdit rimeApprox;

	private RepositoryItemImageComboBox ricbColor;

	private RepositoryItemButtonEdit ribeMorph;

	private RepositoryItemMRUEdit rimeDetect;

	private RepositoryItemImageComboBox ricbeApprox;

	private RepositoryItemSpinEdit riseColor;

	public PropertyGridControl propGrid;

	private RepositoryItemColorEdit repositoryItemColorEdit1;

	private RepositoryItemCheckEdit repositoryItemCheckEdit1;

	private RepositoryItemPictureEdit repositoryItemPictureEdit1;

	private RepositoryItemSpinEdit repositoryItemSpinEdit1;

	private Button btInitSocket;

	private RepositoryItemColorEdit repositoryItemColorEdit3;

	private RepositoryItemCheckEdit repositoryItemCheckEdit3;

	private RepositoryItemPictureEdit repositoryItemPictureEdit3;

	private RepositoryItemSpinEdit repositoryItemSpinEdit3;

	private CheckBox cbEnableRestartListener;

	public ClassStateWatchDlg()
	{
		InitializeComponent();
	}

	private void SystemParamDlgNew_Load(object sender, EventArgs e)
	{
		cbEnableRestartListener.Checked = sysParam.bAllowAutoRestartListenerWhenCloseSocket;
		bLoading = false;
	}

	public void LoadWatchData(AsyncTcpServer asyncTcpServer)
	{
		m_asyncTcpServer = asyncTcpServer;
		propGrid.SelectedObject = asyncTcpServer;
	}

	private void propGrid_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
	{
	}

	private void btnApply_Click(object sender, EventArgs e)
	{
		Close();
	}

	private void btInitSocket_Click(object sender, EventArgs e)
	{
		if (m_asyncTcpServer != null)
		{
			m_asyncTcpServer.Stop();
			m_asyncTcpServer.InitAsyncTcpServer();
			m_asyncTcpServer.Start();
		}
	}

	private void cbEnableRestartListener_CheckedChanged(object sender, EventArgs e)
	{
		if (!bLoading)
		{
			sysParam.bAllowAutoRestartListenerWhenCloseSocket = cbEnableRestartListener.Checked;
			sysParam.SaveParam();
		}
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
		DevExpress.Utils.SerializableAppearanceObject appearance = new DevExpress.Utils.SerializableAppearanceObject();
		DevExpress.Utils.SerializableAppearanceObject appearance2 = new DevExpress.Utils.SerializableAppearanceObject();
		this.repositoryItemColorEdit3 = new DevExpress.XtraEditors.Repository.RepositoryItemColorEdit();
		this.repositoryItemCheckEdit3 = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
		this.repositoryItemPictureEdit3 = new DevExpress.XtraEditors.Repository.RepositoryItemPictureEdit();
		this.repositoryItemSpinEdit3 = new DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit();
		this.btnApply = new System.Windows.Forms.Button();
		this.panel1 = new System.Windows.Forms.Panel();
		this.cbEnableRestartListener = new System.Windows.Forms.CheckBox();
		this.btInitSocket = new System.Windows.Forms.Button();
		this.tablectrl = new System.Windows.Forms.TabControl();
		this.tabPage1 = new System.Windows.Forms.TabPage();
		this.propGrid = new DevExpress.XtraVerticalGrid.PropertyGridControl();
		this.repositoryItemColorEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemColorEdit();
		this.repositoryItemCheckEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
		this.repositoryItemPictureEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemPictureEdit();
		this.repositoryItemSpinEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit();
		this.repositoryItemColorEdit5 = new DevExpress.XtraEditors.Repository.RepositoryItemColorEdit();
		this.repositoryItemCheckEdit5 = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
		this.repositoryItemPictureEdit5 = new DevExpress.XtraEditors.Repository.RepositoryItemPictureEdit();
		this.repositoryItemTrackBar1 = new DevExpress.XtraEditors.Repository.RepositoryItemTrackBar();
		this.riseNumic = new DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit();
		this.ripceEdit = new DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit();
		this.rimeApprox = new DevExpress.XtraEditors.Repository.RepositoryItemMRUEdit();
		this.ricbColor = new DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox();
		this.ribeMorph = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
		this.rimeDetect = new DevExpress.XtraEditors.Repository.RepositoryItemMRUEdit();
		this.ricbeApprox = new DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox();
		this.riseColor = new DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemColorEdit3).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemCheckEdit3).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemPictureEdit3).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemSpinEdit3).BeginInit();
		this.panel1.SuspendLayout();
		this.tablectrl.SuspendLayout();
		this.tabPage1.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.propGrid).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemColorEdit1).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemCheckEdit1).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemPictureEdit1).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemSpinEdit1).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemColorEdit5).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemCheckEdit5).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemPictureEdit5).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemTrackBar1).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.riseNumic).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.ripceEdit).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.rimeApprox).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.ricbColor).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.ribeMorph).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.rimeDetect).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.ricbeApprox).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.riseColor).BeginInit();
		base.SuspendLayout();
		this.repositoryItemColorEdit3.AutoHeight = false;
		this.repositoryItemColorEdit3.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)
		});
		this.repositoryItemColorEdit3.Name = "repositoryItemColorEdit3";
		this.repositoryItemColorEdit3.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
		this.repositoryItemCheckEdit3.AutoHeight = false;
		this.repositoryItemCheckEdit3.Caption = "Check";
		this.repositoryItemCheckEdit3.Name = "repositoryItemCheckEdit3";
		this.repositoryItemPictureEdit3.Name = "repositoryItemPictureEdit3";
		this.repositoryItemPictureEdit3.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Squeeze;
		this.repositoryItemSpinEdit3.AutoHeight = false;
		this.repositoryItemSpinEdit3.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)
		});
		this.repositoryItemSpinEdit3.EditValueChangedFiringMode = DevExpress.XtraEditors.Controls.EditValueChangedFiringMode.Default;
		this.repositoryItemSpinEdit3.Name = "repositoryItemSpinEdit3";
		this.btnApply.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.btnApply.Location = new System.Drawing.Point(445, 16);
		this.btnApply.Name = "btnApply";
		this.btnApply.Size = new System.Drawing.Size(64, 20);
		this.btnApply.TabIndex = 2;
		this.btnApply.Text = "关闭";
		this.btnApply.UseVisualStyleBackColor = true;
		this.btnApply.Click += new System.EventHandler(btnApply_Click);
		this.panel1.Controls.Add(this.cbEnableRestartListener);
		this.panel1.Controls.Add(this.btInitSocket);
		this.panel1.Controls.Add(this.btnApply);
		this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.panel1.Location = new System.Drawing.Point(0, 550);
		this.panel1.Name = "panel1";
		this.panel1.Size = new System.Drawing.Size(521, 44);
		this.panel1.TabIndex = 4;
		this.cbEnableRestartListener.AutoSize = true;
		this.cbEnableRestartListener.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.cbEnableRestartListener.Location = new System.Drawing.Point(12, 19);
		this.cbEnableRestartListener.Name = "cbEnableRestartListener";
		this.cbEnableRestartListener.Size = new System.Drawing.Size(204, 16);
		this.cbEnableRestartListener.TabIndex = 14;
		this.cbEnableRestartListener.Text = "主机断开时，自动检查并重启监听";
		this.cbEnableRestartListener.UseVisualStyleBackColor = true;
		this.cbEnableRestartListener.CheckedChanged += new System.EventHandler(cbEnableRestartListener_CheckedChanged);
		this.btInitSocket.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.btInitSocket.Location = new System.Drawing.Point(334, 16);
		this.btInitSocket.Name = "btInitSocket";
		this.btInitSocket.Size = new System.Drawing.Size(105, 20);
		this.btInitSocket.TabIndex = 3;
		this.btInitSocket.Text = "重新初始化监听";
		this.btInitSocket.UseVisualStyleBackColor = true;
		this.btInitSocket.Click += new System.EventHandler(btInitSocket_Click);
		this.tablectrl.Controls.Add(this.tabPage1);
		this.tablectrl.Dock = System.Windows.Forms.DockStyle.Fill;
		this.tablectrl.Location = new System.Drawing.Point(0, 0);
		this.tablectrl.Name = "tablectrl";
		this.tablectrl.SelectedIndex = 0;
		this.tablectrl.Size = new System.Drawing.Size(521, 550);
		this.tablectrl.TabIndex = 5;
		this.tabPage1.Controls.Add(this.propGrid);
		this.tabPage1.Location = new System.Drawing.Point(4, 22);
		this.tabPage1.Name = "tabPage1";
		this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
		this.tabPage1.Size = new System.Drawing.Size(513, 524);
		this.tabPage1.TabIndex = 0;
		this.tabPage1.Text = "AsyncTcpServer";
		this.tabPage1.UseVisualStyleBackColor = true;
		this.propGrid.AutoGenerateRows = true;
		this.propGrid.DefaultEditors.AddRange(new DevExpress.XtraVerticalGrid.Rows.DefaultEditor[4]
		{
			new DevExpress.XtraVerticalGrid.Rows.DefaultEditor(typeof(System.Drawing.Color), this.repositoryItemColorEdit3),
			new DevExpress.XtraVerticalGrid.Rows.DefaultEditor(typeof(bool), this.repositoryItemCheckEdit3),
			new DevExpress.XtraVerticalGrid.Rows.DefaultEditor(typeof(System.Drawing.Image), this.repositoryItemPictureEdit3),
			new DevExpress.XtraVerticalGrid.Rows.DefaultEditor(typeof(float), this.repositoryItemSpinEdit3)
		});
		this.propGrid.Dock = System.Windows.Forms.DockStyle.Fill;
		this.propGrid.Location = new System.Drawing.Point(3, 3);
		this.propGrid.Name = "propGrid";
		this.propGrid.OptionsBehavior.PropertySort = DevExpress.XtraVerticalGrid.PropertySort.NoSort;
		this.propGrid.OptionsBehavior.ResizeRowHeaders = false;
		this.propGrid.OptionsMenu.EnableContextMenu = true;
		this.propGrid.OptionsView.MaxRowAutoHeight = 100;
		this.propGrid.OptionsView.MinRowAutoHeight = 19;
		this.propGrid.Size = new System.Drawing.Size(507, 518);
		this.propGrid.TabIndex = 223;
		this.propGrid.TreeButtonStyle = DevExpress.XtraVerticalGrid.TreeButtonStyle.TreeView;
		this.repositoryItemColorEdit1.AutoHeight = false;
		this.repositoryItemColorEdit1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)
		});
		this.repositoryItemColorEdit1.Name = "repositoryItemColorEdit1";
		this.repositoryItemColorEdit1.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
		this.repositoryItemCheckEdit1.AutoHeight = false;
		this.repositoryItemCheckEdit1.Caption = "Check";
		this.repositoryItemCheckEdit1.Name = "repositoryItemCheckEdit1";
		this.repositoryItemPictureEdit1.Name = "repositoryItemPictureEdit1";
		this.repositoryItemPictureEdit1.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Squeeze;
		this.repositoryItemSpinEdit1.AutoHeight = false;
		this.repositoryItemSpinEdit1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)
		});
		this.repositoryItemSpinEdit1.EditValueChangedFiringMode = DevExpress.XtraEditors.Controls.EditValueChangedFiringMode.Default;
		this.repositoryItemSpinEdit1.Name = "repositoryItemSpinEdit1";
		this.repositoryItemColorEdit5.AutoHeight = false;
		this.repositoryItemColorEdit5.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)
		});
		this.repositoryItemColorEdit5.Name = "repositoryItemColorEdit5";
		this.repositoryItemColorEdit5.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
		this.repositoryItemCheckEdit5.AutoHeight = false;
		this.repositoryItemCheckEdit5.Caption = "Check";
		this.repositoryItemCheckEdit5.Name = "repositoryItemCheckEdit5";
		this.repositoryItemPictureEdit5.Name = "repositoryItemPictureEdit5";
		this.repositoryItemPictureEdit5.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Squeeze;
		this.repositoryItemTrackBar1.LabelAppearance.Options.UseTextOptions = true;
		this.repositoryItemTrackBar1.LabelAppearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
		this.repositoryItemTrackBar1.Name = "repositoryItemTrackBar1";
		this.repositoryItemTrackBar1.ShowLabels = true;
		this.repositoryItemTrackBar1.ShowLabelsForHiddenTicks = true;
		this.repositoryItemTrackBar1.ShowValueToolTip = true;
		this.riseNumic.AutoHeight = false;
		this.riseNumic.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)
		});
		this.riseNumic.EditValueChangedFiringMode = DevExpress.XtraEditors.Controls.EditValueChangedFiringMode.Default;
		this.riseNumic.Name = "riseNumic";
		this.ripceEdit.AutoHeight = false;
		this.ripceEdit.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)
		});
		this.ripceEdit.Name = "ripceEdit";
		this.rimeApprox.AutoHeight = false;
		this.rimeApprox.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)
		});
		this.rimeApprox.Items.AddRange(new object[7] { "无", "一级", "二级", "三级", "四级", "五级", "六级" });
		this.rimeApprox.Name = "rimeApprox";
		this.ricbColor.AllowNullInput = DevExpress.Utils.DefaultBoolean.False;
		this.ricbColor.AutoHeight = false;
		this.ricbColor.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[2]
		{
			new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo),
			new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Ellipsis, "", -1, true, true, false, DevExpress.XtraEditors.ImageLocation.MiddleCenter, null, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), appearance, "设置索引色", null, null, true)
		});
		this.ricbColor.Items.AddRange(new DevExpress.XtraEditors.Controls.ImageComboBoxItem[9]
		{
			new DevExpress.XtraEditors.Controls.ImageComboBoxItem("红", 0, 9),
			new DevExpress.XtraEditors.Controls.ImageComboBoxItem("蓝", 1, 10),
			new DevExpress.XtraEditors.Controls.ImageComboBoxItem("灰", 2, 11),
			new DevExpress.XtraEditors.Controls.ImageComboBoxItem("青", 3, 12),
			new DevExpress.XtraEditors.Controls.ImageComboBoxItem("紫", 4, 13),
			new DevExpress.XtraEditors.Controls.ImageComboBoxItem("黄", 5, 14),
			new DevExpress.XtraEditors.Controls.ImageComboBoxItem("绿", 6, 15),
			new DevExpress.XtraEditors.Controls.ImageComboBoxItem("棕", 7, 16),
			new DevExpress.XtraEditors.Controls.ImageComboBoxItem("靛", 8, 17)
		});
		this.ricbColor.Name = "ricbColor";
		this.ribeMorph.AutoHeight = false;
		this.ribeMorph.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton()
		});
		this.ribeMorph.Name = "ribeMorph";
		this.rimeDetect.AutoHeight = false;
		this.rimeDetect.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)
		});
		this.rimeDetect.Items.AddRange(new object[6] { "轮廓检测法", "边缘检测法", "区域检测法", "钻石检测", "缎带检测", "方巾检测" });
		this.rimeDetect.Name = "rimeDetect";
		this.ricbeApprox.AutoHeight = false;
		this.ricbeApprox.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[2]
		{
			new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo),
			new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Ellipsis, "", -1, true, true, false, DevExpress.XtraEditors.ImageLocation.MiddleCenter, null, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), appearance2, "设置索引色", null, null, true)
		});
		this.ricbeApprox.Items.AddRange(new DevExpress.XtraEditors.Controls.ImageComboBoxItem[7]
		{
			new DevExpress.XtraEditors.Controls.ImageComboBoxItem("无", 0, -1),
			new DevExpress.XtraEditors.Controls.ImageComboBoxItem("一级", 1, -1),
			new DevExpress.XtraEditors.Controls.ImageComboBoxItem("二级", 2, -1),
			new DevExpress.XtraEditors.Controls.ImageComboBoxItem("三级", 3, -1),
			new DevExpress.XtraEditors.Controls.ImageComboBoxItem("四级", 4, -1),
			new DevExpress.XtraEditors.Controls.ImageComboBoxItem("五级", 5, -1),
			new DevExpress.XtraEditors.Controls.ImageComboBoxItem("六级", 6, -1)
		});
		this.ricbeApprox.Name = "ricbeApprox";
		this.riseColor.AutoHeight = false;
		this.riseColor.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
		{
			new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)
		});
		this.riseColor.MaxValue = new decimal(new int[4] { 255, 0, 0, 0 });
		this.riseColor.Name = "riseColor";
		base.AcceptButton = this.btnApply;
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(521, 594);
		base.Controls.Add(this.tablectrl);
		base.Controls.Add(this.panel1);
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
		base.Name = "ClassStateWatchDlg";
		base.ShowInTaskbar = false;
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
		this.Text = "类属性查看";
		base.Load += new System.EventHandler(SystemParamDlgNew_Load);
		((System.ComponentModel.ISupportInitialize)this.repositoryItemColorEdit3).EndInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemCheckEdit3).EndInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemPictureEdit3).EndInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemSpinEdit3).EndInit();
		this.panel1.ResumeLayout(false);
		this.panel1.PerformLayout();
		this.tablectrl.ResumeLayout(false);
		this.tabPage1.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.propGrid).EndInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemColorEdit1).EndInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemCheckEdit1).EndInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemPictureEdit1).EndInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemSpinEdit1).EndInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemColorEdit5).EndInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemCheckEdit5).EndInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemPictureEdit5).EndInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemTrackBar1).EndInit();
		((System.ComponentModel.ISupportInitialize)this.riseNumic).EndInit();
		((System.ComponentModel.ISupportInitialize)this.ripceEdit).EndInit();
		((System.ComponentModel.ISupportInitialize)this.rimeApprox).EndInit();
		((System.ComponentModel.ISupportInitialize)this.ricbColor).EndInit();
		((System.ComponentModel.ISupportInitialize)this.ribeMorph).EndInit();
		((System.ComponentModel.ISupportInitialize)this.rimeDetect).EndInit();
		((System.ComponentModel.ISupportInitialize)this.ricbeApprox).EndInit();
		((System.ComponentModel.ISupportInitialize)this.riseColor).EndInit();
		base.ResumeLayout(false);
	}
}
