using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace IBrainChrom2018;

public class MainForm : LclDialog
{
	[StructLayout(LayoutKind.Sequential)]
	private class Class2
	{
		public int int_0;

		public int int_1;

		public int int_2;

		public Guid guid_0;

		public short short_0;
	}

	[StructLayout(LayoutKind.Sequential)]
	private class Class3
	{
		public int int_0;

		public int int_1;

		public int int_2;
	}

	private enum Enum2
	{
		const_0,
		const_1,
		const_2
	}

	private const string string_0 = "sys.cfg";

	private const int int_0 = 32768;

	private const int int_1 = 32772;

	private const int int_2 = 5;

	private const int int_3 = 0;

	private const int int_4 = 150;

	public const int picHeight = 80;

	public const int picWidth = 110;

	public const string scnmiAuditTrail = "日志";

	private const string string_1 = "配置...";

	private const string string_2 = "目录...";

	private const string string_3 = "退出";

	private const string string_4 = "帮助";

	private const string string_5 = "锁定";

	private const string string_6 = "登录";

	private const string string_7 = "注销";

	public const string scnmiLogoutAll = "全部注销";

	private const string string_8 = "系统";

	private const string string_9 = "用户帐户...";

	public const string scnslbExplain = "帮助，按F1";

	private const string string_10 = "About...";

	public const string senmiAuditTrail = "Audit Trail";

	private const string string_11 = "Configuration...";

	private const string string_12 = "Directories...";

	private const string string_13 = "Exit";

	private const string string_14 = "Help";

	private const string string_15 = "Lock";

	private const string string_16 = "Login";

	private const string string_17 = "Logout";

	public const string senmiLogoutAll = "Logout All";

	private const string string_18 = "System";

	private const string string_19 = "UserAccounts...";

	public const string senslbExplain = "For Help,press F1";

	private const string string_20 = "uos.cfg";

	private const int int_5 = 537;

	private ToolStripButton btnAbout;

	private ToolStripButton btnAuditTrail;

	private ToolStripButton btnConfig;

	private ToolStripButton btnDirs;

	private LclButton[] lclButton_0;

	private ToolStripButton btnUserAccounts;

	private IContainer icontainer_1;

	private AboutBox aboutBox_0;

	private Instrus_ATDirsDialog instrus_ATDirsDialog_0;

	private DlgIP dlgIP_0;

	private SysCfgDlg sysCfgDlg_0;

	private UserAccountsDlg userAccountsDlg_0;

	private WarningLogoutDialog warningLogoutDialog_0;

	private Guid guid_0;

	private IntPtr intptr_0;

	public static LclLabel[] lbNames = new LclLabel[0];

	private LclLabel[] lclLabel_0;

	private ToolStripMenuItem miAuditTrail;

	private ToolStripMenuItem miConfig;

	private ToolStripMenuItem miDirs;

	private ToolStripMenuItem miExit;

	private ToolStripMenuItem miHelp;

	private ToolStripMenuItem miHpAbout;

	private ToolStripMenuItem miHpHelp;

	private ToolStripMenuItem miIP;

	private ToolStripMenuItem miLock;

	private ToolStripMenuItem[] toolStripMenuItem_0;

	private ToolStripMenuItem miLogin;

	private ToolStripMenuItem[] toolStripMenuItem_1;

	private ToolStripMenuItem miLogout;

	private ToolStripMenuItem miLogoutAll;

	private ToolStripMenuItem[] toolStripMenuItem_2;

	private ToolStripMenuItem miSystem;

	private ToolStripMenuItem miUseAu;

	private ToolStripMenuItem miUserAccounts;

	private MenuStrip msMain;

	private LclPictureBox[] lclPictureBox_0;

	private LclPanel pnlInstruImages;

	private LclPanel pnlLoginUsers;

	private LclPanel pnlNames;

	private ToolStripStatusLabel slbExplain;

	private StatusStrip ssMain;

	public static StationAdtTrlForm stationAdtTrlForm = new StationAdtTrlForm();

	private ToolStripSeparator toolStripSeparator1;

	private ToolStripSeparator toolStripSeparator2;

	private ToolStripSeparator toolStripSeparator3;

	private ToolStripSeparator toolStripSeparator4;

	private ToolStrip tsMain;

	public MainForm()
	{
		sysCfgDlg_0 = new SysCfgDlg();
		warningLogoutDialog_0 = new WarningLogoutDialog();
		lclPictureBox_0 = new LclPictureBox[0];
		lclButton_0 = new LclButton[0];
		lclLabel_0 = new LclLabel[0];
		toolStripMenuItem_1 = new ToolStripMenuItem[0];
		toolStripMenuItem_2 = new ToolStripMenuItem[0];
		toolStripMenuItem_0 = new ToolStripMenuItem[0];
		guid_0 = new Guid(1012798562, 22165, 19992, 135, 107, 243, 243, 208, 138, 175, 24);
		icontainer_1 = null;
		InitializeComponent();
		miIP.Text = Lang.PS("IP查询", "IP Query");
		pnlLoginUsers.Dock = DockStyle.Fill;
		try
		{
			FrameDis.font = new Font("Tahoma", 8f);
		}
		catch
		{
			FrameDis.font = (Font)Font.Clone();
		}
		Class49.bool_4 = miUseAu.Checked;
		base.Icon = SystemIconResource.smethod_15();
		ResourceImageLoad.SetCtrlBitmap(btnUserAccounts, SystemBitmapResource3.smethod_3());
		ResourceImageLoad.SetCtrlBitmap(btnConfig, SystemBitmapResource3.smethod_0());
		ResourceImageLoad.SetCtrlBitmap(btnDirs, SystemBitmapResource3.smethod_1());
		ResourceImageLoad.SetCtrlBitmap(btnAuditTrail, SystemIconResource.smethod_45());
		ResourceImageLoad.SetCtrlBitmap(btnAbout, SystemIconResource.smethod_36());
		int num = SysCfgDlg.sysConfig.pageInstrus.Length;
		Array.Resize(ref toolStripMenuItem_1, num);
		Array.Resize(ref toolStripMenuItem_2, num);
		Array.Resize(ref toolStripMenuItem_0, num);
		for (int i = 0; i < num; i++)
		{
			toolStripMenuItem_1[i] = new ToolStripMenuItem();
			toolStripMenuItem_1[i].Tag = i;
			miLogin.DropDownItems.Add(toolStripMenuItem_1[i]);
			toolStripMenuItem_1[i].Click += method_4;
			toolStripMenuItem_2[i] = new ToolStripMenuItem();
			toolStripMenuItem_2[i].Enabled = false;
			toolStripMenuItem_2[i].Tag = i;
			miLogout.DropDownItems.Add(toolStripMenuItem_2[i]);
			toolStripMenuItem_2[i].Click += method_5;
			toolStripMenuItem_0[i] = new ToolStripMenuItem();
			toolStripMenuItem_0[i].Enabled = false;
			toolStripMenuItem_0[i].Tag = i;
			miLock.DropDownItems.Add(toolStripMenuItem_0[i]);
			toolStripMenuItem_0[i].Click += method_3;
		}
		int num2 = SysCfgDlg.sysConfig.pageInstrus.Length;
		Array.Resize(ref lbNames, num2);
		Array.Resize(ref lclPictureBox_0, num2);
		Array.Resize(ref lclButton_0, num2);
		Array.Resize(ref lclLabel_0, num2);
		for (int j = 0; j < num2; j++)
		{
			lbNames[j] = new LclLabel();
			pnlNames.Controls.Add(lbNames[j]);
			lbNames[j].AutoSize = false;
			lbNames[j].TextAlign = ContentAlignment.MiddleCenter;
			lbNames[j].Location = new Point(j * 150, 0);
			lbNames[j].Width = 150;
			lclPictureBox_0[j] = new LclPictureBox();
			pnlInstruImages.Controls.Add(lclPictureBox_0[j]);
			lclPictureBox_0[j].SizeMode = PictureBoxSizeMode.CenterImage;
			lclPictureBox_0[j].Cursor = Cursors.Hand;
			lclPictureBox_0[j].Location = new Point(j * 150 + 20, (pnlInstruImages.Height - 80) / 2);
			lclPictureBox_0[j].Size = new Size(110, 80);
			lclPictureBox_0[j].Tag = j;
			lclPictureBox_0[j].Click += method_0;
			lclButton_0[j] = new LclButton();
			pnlLoginUsers.Controls.Add(lclButton_0[j]);
			lclButton_0[j].Cursor = Cursors.Hand;
			lclButton_0[j].Location = new Point(j * 150 + (150 - lclButton_0[j].Width) / 2, (pnlLoginUsers.Height - lclButton_0[j].Height) / 2);
			lclButton_0[j].Tag = j;
			lclButton_0[j].Click += method_0;
			lclLabel_0[j] = new LclLabel();
			pnlLoginUsers.Controls.Add(lclLabel_0[j]);
			lclLabel_0[j].TextAlign = ContentAlignment.MiddleCenter;
			lclLabel_0[j].Location = new Point(j * 150, lclButton_0[j].Location.Y);
			lclLabel_0[j].Width = 150;
			lclLabel_0[j].ImageAlign = ContentAlignment.MiddleLeft;
			SysCfgDlg.sysConfig.pageInstrus[j].OnCloseInstrument += method_8;
		}
		warningLogoutDialog_0.OnLogoutAllInstrus += miLogoutAll_Click;
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
		this.msMain = new System.Windows.Forms.MenuStrip();
		this.miSystem = new System.Windows.Forms.ToolStripMenuItem();
		this.miUserAccounts = new System.Windows.Forms.ToolStripMenuItem();
		this.miConfig = new System.Windows.Forms.ToolStripMenuItem();
		this.miDirs = new System.Windows.Forms.ToolStripMenuItem();
		this.miUseAu = new System.Windows.Forms.ToolStripMenuItem();
		this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
		this.miAuditTrail = new System.Windows.Forms.ToolStripMenuItem();
		this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
		this.miExit = new System.Windows.Forms.ToolStripMenuItem();
		this.miLogin = new System.Windows.Forms.ToolStripMenuItem();
		this.miLogout = new System.Windows.Forms.ToolStripMenuItem();
		this.miLogoutAll = new System.Windows.Forms.ToolStripMenuItem();
		this.miLock = new System.Windows.Forms.ToolStripMenuItem();
		this.miHelp = new System.Windows.Forms.ToolStripMenuItem();
		this.miHpHelp = new System.Windows.Forms.ToolStripMenuItem();
		this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
		this.miIP = new System.Windows.Forms.ToolStripMenuItem();
		this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
		this.miHpAbout = new System.Windows.Forms.ToolStripMenuItem();
		this.tsMain = new System.Windows.Forms.ToolStrip();
		this.btnUserAccounts = new System.Windows.Forms.ToolStripButton();
		this.btnConfig = new System.Windows.Forms.ToolStripButton();
		this.btnDirs = new System.Windows.Forms.ToolStripButton();
		this.btnAuditTrail = new System.Windows.Forms.ToolStripButton();
		this.btnAbout = new System.Windows.Forms.ToolStripButton();
		this.ssMain = new System.Windows.Forms.StatusStrip();
		this.slbExplain = new System.Windows.Forms.ToolStripStatusLabel();
		this.pnlLoginUsers = new IBrainChrom2018.LclPanel();
		this.pnlInstruImages = new IBrainChrom2018.LclPanel();
		this.pnlNames = new IBrainChrom2018.LclPanel();
		this.msMain.SuspendLayout();
		this.tsMain.SuspendLayout();
		this.ssMain.SuspendLayout();
		this.pnlInstruImages.SuspendLayout();
		base.SuspendLayout();
		base.btnCancel.Text = "取消";
		base.btnHelp.Text = "帮助";
		base.btnOK.Location = new System.Drawing.Point(47, 151);
		base.btnOK.Text = "确认";
		this.msMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[5] { this.miSystem, this.miLogin, this.miLogout, this.miLock, this.miHelp });
		this.msMain.Location = new System.Drawing.Point(0, 0);
		this.msMain.Name = "msMain";
		this.msMain.Size = new System.Drawing.Size(541, 25);
		this.msMain.TabIndex = 1;
		this.msMain.Text = "menuStrip1";
		this.miSystem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[8] { this.miUserAccounts, this.miConfig, this.miDirs, this.miUseAu, this.toolStripSeparator1, this.miAuditTrail, this.toolStripSeparator2, this.miExit });
		this.miSystem.Name = "miSystem";
		this.miSystem.Size = new System.Drawing.Size(44, 21);
		this.miSystem.Text = "系统";
		this.miUserAccounts.Name = "miUserAccounts";
		this.miUserAccounts.Size = new System.Drawing.Size(152, 22);
		this.miUserAccounts.Text = "用户帐户...";
		this.miUserAccounts.Click += new System.EventHandler(btnUserAccounts_Click);
		this.miConfig.Name = "miConfig";
		this.miConfig.Size = new System.Drawing.Size(152, 22);
		this.miConfig.Text = "配置...";
		this.miConfig.Click += new System.EventHandler(btnConfig_Click);
		this.miDirs.Name = "miDirs";
		this.miDirs.Size = new System.Drawing.Size(152, 22);
		this.miDirs.Text = "目录...";
		this.miDirs.Click += new System.EventHandler(btnDirs_Click);
		this.miUseAu.Name = "miUseAu";
		this.miUseAu.Size = new System.Drawing.Size(164, 22);
		this.miUseAu.Text = "使用Au单位";
		this.miUseAu.Click += new System.EventHandler(miUseAu_Click);
		this.toolStripSeparator1.Name = "toolStripSeparator1";
		this.toolStripSeparator1.Size = new System.Drawing.Size(161, 6);
		this.miAuditTrail.Name = "miAuditTrail";
		this.miAuditTrail.Size = new System.Drawing.Size(152, 22);
		this.miAuditTrail.Text = "日志";
		this.miAuditTrail.Click += new System.EventHandler(btnAuditTrail_Click);
		this.toolStripSeparator2.Name = "toolStripSeparator2";
		this.toolStripSeparator2.Size = new System.Drawing.Size(161, 6);
		this.miExit.Name = "miExit";
		this.miExit.Size = new System.Drawing.Size(152, 22);
		this.miExit.Text = "退出";
		this.miExit.Click += new System.EventHandler(miExit_Click);
		this.miLogin.Name = "miLogin";
		this.miLogin.Size = new System.Drawing.Size(44, 21);
		this.miLogin.Text = "登录";
		this.miLogout.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[1] { this.miLogoutAll });
		this.miLogout.Name = "miLogout";
		this.miLogout.Size = new System.Drawing.Size(44, 21);
		this.miLogout.Text = "注销";
		this.miLogoutAll.Name = "miLogoutAll";
		this.miLogoutAll.Size = new System.Drawing.Size(152, 22);
		this.miLogoutAll.Text = "全部注销";
		this.miLogoutAll.Click += new System.EventHandler(miLogoutAll_Click);
		this.miLock.Name = "miLock";
		this.miLock.Size = new System.Drawing.Size(44, 21);
		this.miLock.Text = "锁定";
		this.miHelp.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[5] { this.miHpHelp, this.toolStripSeparator3, this.miIP, this.toolStripSeparator4, this.miHpAbout });
		this.miHelp.Name = "miHelp";
		this.miHelp.Size = new System.Drawing.Size(44, 21);
		this.miHelp.Text = "帮助";
		this.miHpHelp.Name = "miHpHelp";
		this.miHpHelp.ShortcutKeys = System.Windows.Forms.Keys.F1;
		this.miHpHelp.Size = new System.Drawing.Size(192, 22);
		this.miHpHelp.Text = "帮助";
		this.miHpHelp.Click += new System.EventHandler(miHpHelp_Click);
		this.toolStripSeparator3.Name = "toolStripSeparator3";
		this.toolStripSeparator3.Size = new System.Drawing.Size(189, 6);
		this.miIP.Name = "miIP";
		this.miIP.Size = new System.Drawing.Size(192, 22);
		this.miIP.Text = "toolStripMenuItem1";
		this.miIP.Click += new System.EventHandler(miIP_Click);
		this.toolStripSeparator4.Name = "toolStripSeparator4";
		this.toolStripSeparator4.Size = new System.Drawing.Size(189, 6);
		this.miHpAbout.Name = "miHpAbout";
		this.miHpAbout.Size = new System.Drawing.Size(192, 22);
		this.miHpAbout.Text = "关于...";
		this.miHpAbout.Click += new System.EventHandler(btnAbout_Click);
		this.tsMain.Dock = System.Windows.Forms.DockStyle.Left;
		this.tsMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[5] { this.btnUserAccounts, this.btnConfig, this.btnDirs, this.btnAuditTrail, this.btnAbout });
		this.tsMain.Location = new System.Drawing.Point(0, 25);
		this.tsMain.Name = "tsMain";
		this.tsMain.Size = new System.Drawing.Size(24, 191);
		this.tsMain.TabIndex = 2;
		this.tsMain.Text = "toolStrip1";
		this.btnUserAccounts.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
		this.btnUserAccounts.ImageTransparentColor = System.Drawing.Color.Magenta;
		this.btnUserAccounts.Name = "btnUserAccounts";
		this.btnUserAccounts.Size = new System.Drawing.Size(21, 4);
		this.btnUserAccounts.Text = "User Accounts";
		this.btnUserAccounts.Click += new System.EventHandler(btnUserAccounts_Click);
		this.btnConfig.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
		this.btnConfig.ImageTransparentColor = System.Drawing.Color.Magenta;
		this.btnConfig.Name = "btnConfig";
		this.btnConfig.Size = new System.Drawing.Size(21, 4);
		this.btnConfig.Text = "Configuration";
		this.btnConfig.Click += new System.EventHandler(btnConfig_Click);
		this.btnDirs.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
		this.btnDirs.ImageTransparentColor = System.Drawing.Color.Magenta;
		this.btnDirs.Name = "btnDirs";
		this.btnDirs.Size = new System.Drawing.Size(21, 4);
		this.btnDirs.Text = "Directories";
		this.btnDirs.Click += new System.EventHandler(btnDirs_Click);
		this.btnAuditTrail.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
		this.btnAuditTrail.ImageTransparentColor = System.Drawing.Color.Magenta;
		this.btnAuditTrail.Name = "btnAuditTrail";
		this.btnAuditTrail.Size = new System.Drawing.Size(21, 4);
		this.btnAuditTrail.Text = "Audit Trail";
		this.btnAuditTrail.Click += new System.EventHandler(btnAuditTrail_Click);
		this.btnAbout.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
		this.btnAbout.ImageTransparentColor = System.Drawing.Color.Magenta;
		this.btnAbout.Name = "btnAbout";
		this.btnAbout.Size = new System.Drawing.Size(21, 4);
		this.btnAbout.Text = "About";
		this.btnAbout.Click += new System.EventHandler(btnAbout_Click);
		this.ssMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[1] { this.slbExplain });
		this.ssMain.Location = new System.Drawing.Point(24, 194);
		this.ssMain.Name = "ssMain";
		this.ssMain.Size = new System.Drawing.Size(517, 22);
		this.ssMain.SizingGrip = false;
		this.ssMain.TabIndex = 10;
		this.ssMain.Text = "statusStrip1";
		this.slbExplain.Name = "slbExplain";
		this.slbExplain.Size = new System.Drawing.Size(32, 17);
		this.slbExplain.Text = "帮助";
		this.pnlLoginUsers.Location = new System.Drawing.Point(23, 77);
		this.pnlLoginUsers.Name = "pnlLoginUsers";
		this.pnlLoginUsers.Size = new System.Drawing.Size(200, 20);
		this.pnlLoginUsers.TabIndex = 13;
		this.pnlInstruImages.Controls.Add(this.pnlLoginUsers);
		this.pnlInstruImages.Dock = System.Windows.Forms.DockStyle.Top;
		this.pnlInstruImages.Location = new System.Drawing.Point(24, 48);
		this.pnlInstruImages.Name = "pnlInstruImages";
		this.pnlInstruImages.Size = new System.Drawing.Size(517, 87);
		this.pnlInstruImages.TabIndex = 12;
		this.pnlNames.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
		this.pnlNames.Dock = System.Windows.Forms.DockStyle.Top;
		this.pnlNames.Location = new System.Drawing.Point(24, 25);
		this.pnlNames.Name = "pnlNames";
		this.pnlNames.Size = new System.Drawing.Size(517, 23);
		this.pnlNames.TabIndex = 12;
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
		base.ClientSize = new System.Drawing.Size(541, 216);
		base.Controls.Add(this.pnlInstruImages);
		base.Controls.Add(this.pnlNames);
		base.Controls.Add(this.ssMain);
		base.Controls.Add(this.tsMain);
		base.Controls.Add(this.msMain);
		base.Name = "MainForm";
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.Text = "色谱工作站";
		base.FormClosing += new System.Windows.Forms.FormClosingEventHandler(MainForm_FormClosing);
		base.Load += new System.EventHandler(MainForm_Load);
		base.Shown += new System.EventHandler(MainForm_Shown);
		base.KeyDown += new System.Windows.Forms.KeyEventHandler(MainForm_KeyDown);
		base.Controls.SetChildIndex(this.msMain, 0);
		base.Controls.SetChildIndex(base.btnOK, 0);
		base.Controls.SetChildIndex(this.tsMain, 0);
		base.Controls.SetChildIndex(this.ssMain, 0);
		base.Controls.SetChildIndex(this.pnlNames, 0);
		base.Controls.SetChildIndex(this.pnlInstruImages, 0);
		base.Controls.SetChildIndex(base.btnCancel, 0);
		base.Controls.SetChildIndex(base.btnHelp, 0);
		this.msMain.ResumeLayout(false);
		this.msMain.PerformLayout();
		this.tsMain.ResumeLayout(false);
		this.tsMain.PerformLayout();
		this.ssMain.ResumeLayout(false);
		this.ssMain.PerformLayout();
		this.pnlInstruImages.ResumeLayout(false);
		base.ResumeLayout(false);
		base.PerformLayout();
	}

	private void method_0(object sender, EventArgs e)
	{
		int int_ = 0;
		if (sender is PictureBox)
		{
			int_ = (int)(sender as PictureBox).Tag;
		}
		if (sender is Button)
		{
			int_ = (int)(sender as Button).Tag;
		}
		method_6(int_, Enum2.const_0);
	}

	private void method_1()
	{
		Text = Class49.smethod_13();
		miSystem.Text = Lang.PS("系统", "System");
		miLogin.Text = Lang.PS("登录", "Login");
		miLogout.Text = Lang.PS("注销", "Logout");
		miLock.Text = Lang.PS("锁定", "Lock");
		miHelp.Text = Lang.PS("帮助", "Help");
		miUserAccounts.Text = Lang.PS("用户帐户...", "UserAccounts...");
		miConfig.Text = Lang.PS("配置...", "Configuration...");
		miDirs.Text = Lang.PS("目录...", "Directories...");
		miAuditTrail.Text = Lang.PS("日志", "Audit Trail");
		miExit.Text = Lang.PS("退出", "Exit");
		miLogoutAll.Text = Lang.PS("全部注销", "Logout All");
		miHpHelp.Text = Lang.PS("帮助", "Help");
		miHpAbout.Text = Lang.PS("关于...", "About...");
		btnUserAccounts.Text = Lang.PS("用户帐户...", "UserAccounts...");
		btnConfig.Text = Lang.PS("配置...", "Configuration...");
		btnDirs.Text = Lang.PS("目录...", "Directories...");
		btnAuditTrail.Text = Lang.PS("日志", "Audit Trail");
		btnAbout.Text = Lang.PS("关于", "About");
		slbExplain.Text = Lang.PS("帮助，按F1", "For Help,press F1");
		for (int i = 0; i < lclButton_0.Length; i++)
		{
			lclButton_0[i].Text = Lang.PS("登录", "Login");
		}
		for (int j = 0; j < miLogin.DropDownItems.Count; j++)
		{
			toolStripMenuItem_1[j].Text = Lang.PS("登录 ", "Login ") + SysCfgDlg.sysConfig.pageInstrus[j].name;
			toolStripMenuItem_2[j].Text = Lang.PS("注销 ", "Logout ") + SysCfgDlg.sysConfig.pageInstrus[j].name;
			toolStripMenuItem_0[j].Text = Lang.PS("锁定 ", "Lock ") + SysCfgDlg.sysConfig.pageInstrus[j].name;
		}
	}

	private bool method_2()
	{
		for (int i = 0; i < SysCfgDlg.sysConfig.GetInstrumentsNum(); i++)
		{
			if (!SysCfgDlg.sysConfig.RetInstrument(i).CloseInstru())
			{
				return false;
			}
		}
		return true;
	}

	private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
	{
		if (!method_2())
		{
			e.Cancel = true;
			return;
		}
		for (int i = 0; i < SysCfgDlg.sysConfig.GetInstrumentsNum(); i++)
		{
			SysCfgDlg.sysConfig.RetInstrument(i).Detector_stop(onlyVirtual: false);
		}
		method_15();
		UnregisterDeviceNotification(intptr_0);
		sysCfgDlg_0.EndDevice(null);
	}

	private void MainForm_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode == Keys.Escape)
		{
			Close();
		}
	}

	private void MainForm_Load(object sender, EventArgs e)
	{
		method_1();
		method_14();
		ShowInstruments();
		DirectoryInfo directoryInfo = new DirectoryInfo("Station\\VSG\\");
		if (!directoryInfo.Exists)
		{
			directoryInfo.Create();
		}
		directoryInfo = new DirectoryInfo("Station\\TMP\\");
		if (!directoryInfo.Exists)
		{
			directoryInfo.Create();
		}
		directoryInfo = new DirectoryInfo(ResourceImageLoad.ExePath() + "Common\\");
		if (!directoryInfo.Exists)
		{
			directoryInfo.Create();
		}
		string instruName = Lang.PS("系统", "System");
		string descript = Lang.PS("打开系统", "Open System");
		stationAdtTrlForm.AddTail(-1, ATResult.Ok, ATType.StartSys, "", instruName, ATArea.Sys, descript);
		Class2 @class = new Class2();
		int cb = (@class.int_0 = Marshal.SizeOf((object)@class));
		@class.int_1 = 5;
		@class.guid_0 = guid_0;
		IntPtr intPtr = Marshal.AllocHGlobal(cb);
		Marshal.StructureToPtr((object)@class, intPtr, fDeleteOld: true);
		intptr_0 = RegisterDeviceNotification(base.Handle, intPtr, 0u);
		sysCfgDlg_0.ScanHardWares();
	}

	private void MainForm_Shown(object sender, EventArgs e)
	{
	}

	private void btnAuditTrail_Click(object sender, EventArgs e)
	{
		stationAdtTrlForm.Show(-1);
	}

	private void btnConfig_Click(object sender, EventArgs e)
	{
		if (warningLogoutDialog_0.LogoutAllInstruments() && sysCfgDlg_0.ShowDialog() == DialogResult.OK)
		{
			ShowInstruments();
		}
	}

	private void btnDirs_Click(object sender, EventArgs e)
	{
		if (warningLogoutDialog_0.LogoutAllInstruments())
		{
			if (instrus_ATDirsDialog_0 == null)
			{
				instrus_ATDirsDialog_0 = new Instrus_ATDirsDialog();
			}
			instrus_ATDirsDialog_0.ShowDialog();
		}
	}

	private void miExit_Click(object sender, EventArgs e)
	{
		Close();
	}

	private void btnAbout_Click(object sender, EventArgs e)
	{
		if (aboutBox_0 == null)
		{
			aboutBox_0 = new AboutBox();
		}
		aboutBox_0.ShowDialog();
	}

	private void miHpHelp_Click(object sender, EventArgs e)
	{
		Class49.smethod_32("概述");
	}

	private void miIP_Click(object sender, EventArgs e)
	{
		if (dlgIP_0 == null)
		{
			dlgIP_0 = new DlgIP();
		}
		dlgIP_0.ShowDialog();
	}

	private void method_3(object sender, EventArgs e)
	{
		ToolStripMenuItem toolStripMenuItem = sender as ToolStripMenuItem;
		int int_ = (int)toolStripMenuItem.Tag;
		method_6(int_, Enum2.const_2);
	}

	private void method_4(object sender, EventArgs e)
	{
		ToolStripMenuItem toolStripMenuItem = sender as ToolStripMenuItem;
		int int_ = (int)toolStripMenuItem.Tag;
		method_6(int_, Enum2.const_0);
	}

	private void miLogoutAll_Click(object sender, EventArgs e)
	{
		method_2();
	}

	private void method_5(object sender, EventArgs e)
	{
		ToolStripMenuItem toolStripMenuItem = sender as ToolStripMenuItem;
		int int_ = (int)toolStripMenuItem.Tag;
		method_6(int_, Enum2.const_1);
	}

	private void miUseAu_Click(object sender, EventArgs e)
	{
		miUseAu.Checked = !miUseAu.Checked;
		Class49.bool_4 = miUseAu.Checked;
	}

	private void btnUserAccounts_Click(object sender, EventArgs e)
	{
		if (warningLogoutDialog_0.LogoutAllInstruments())
		{
			if (userAccountsDlg_0 == null)
			{
				userAccountsDlg_0 = new UserAccountsDlg();
			}
			userAccountsDlg_0.ShowDialog();
		}
	}

	private void method_6(int int_6, Enum2 enum2_0)
	{
		if (!SysCfgDlg.sysConfig.hasInstruForm)
		{
			SysCfgDlg.sysConfig.CreateInstruForm();
			for (int i = 0; i < SysCfgDlg.sysConfig.pageInstrus.Length; i++)
			{
				SysCfgDlg.sysConfig.pageInstrus[i].form.mubtnMainFormHandler = method_13;
				SysCfgDlg.sysConfig.pageInstrus[i].form.pipe_color = Class49.GetColor(i);
			}
		}
		Instrument instrument = SysCfgDlg.sysConfig.RetInstrument(int_6);
		switch (enum2_0)
		{
		case Enum2.const_0:
			if (!instrument.locked)
			{
				if (!instrument.logged)
				{
					Class49.loginDlg_0.openInstruNo = int_6;
					if (!Class49.loginDlg_0.ShowDialog(AccessType.OpenInstrus))
					{
						return;
					}
					for (int j = 0; j < Class49.loginDlg_0.openInstruNos.Length; j++)
					{
						int_6 = Class49.loginDlg_0.openInstruNos[j];
						instrument = SysCfgDlg.sysConfig.RetInstrument(int_6);
						if (instrument != null && !instrument.logged)
						{
							instrument.OpenInstru(Class49.loginDlg_0.user);
							method_8(int_6, instrument);
						}
					}
					return;
				}
				instrument.OpenInstru(null);
				break;
			}
			goto case Enum2.const_2;
		case Enum2.const_1:
			instrument.CloseInstru();
			break;
		case Enum2.const_2:
			instrument.LockUnlockInstru();
			break;
		}
		method_9(int_6, instrument);
	}

	private void method_7(int int_6, Instrument instrument_0)
	{
		if (instrument_0.logged)
		{
			lclPictureBox_0[int_6].Image = instrument_0.openedImage;
			lclButton_0[int_6].Visible = false;
			lclLabel_0[int_6].Text = instrument_0.user.u_name;
		}
		else
		{
			lclPictureBox_0[int_6].Image = instrument_0.closedImage;
			lclButton_0[int_6].Visible = true;
			lclLabel_0[int_6].Text = "";
		}
	}

	private void method_8(int int_6, Instrument instrument_0)
	{
		method_7(int_6, instrument_0);
		method_9(int_6, instrument_0);
	}

	private void method_9(int int_6, Instrument instrument_0)
	{
		toolStripMenuItem_1[int_6].Checked = instrument_0.logged;
		toolStripMenuItem_2[int_6].Enabled = instrument_0.logged;
		toolStripMenuItem_0[int_6].Enabled = instrument_0.logged;
		toolStripMenuItem_0[int_6].Checked = instrument_0.locked;
		if (instrument_0.locked)
		{
			lclLabel_0[int_6].Image = SystemBitmapResource3.smethod_2();
		}
		else
		{
			lclLabel_0[int_6].Image = null;
		}
	}

	private void method_10()
	{
		for (int i = 0; i < miLogin.DropDownItems.Count; i++)
		{
			toolStripMenuItem_1[i].Text = Lang.PS("登录 ", "Login ") + SysCfgDlg.sysConfig.pageInstrus[i].name;
			toolStripMenuItem_2[i].Text = Lang.PS("注销 ", "Logout ") + SysCfgDlg.sysConfig.pageInstrus[i].name;
			toolStripMenuItem_0[i].Text = Lang.PS("锁定 ", "Lock ") + SysCfgDlg.sysConfig.pageInstrus[i].name;
		}
	}

	private void method_11()
	{
		for (int i = 0; i < lclButton_0.Length; i++)
		{
			lclButton_0[i].Text = Lang.PS("登录", "Login");
		}
	}

	[DllImport("user32.dll", CharSet = CharSet.Auto)]
	private static extern IntPtr RegisterDeviceNotification(IntPtr intptr_1, IntPtr intptr_2, uint uint_0);

	private void method_12(int int_6, Instrument instrument_0)
	{
		lbNames[int_6].Text = instrument_0.name;
		method_7(int_6, instrument_0);
		method_11();
	}

	public void ShowInstruments()
	{
		int instrumentsNum = SysCfgDlg.sysConfig.GetInstrumentsNum();
		int num = 150 * instrumentsNum;
		base.Width = num + tsMain.Width + 10;
		for (int i = 0; i < toolStripMenuItem_1.Length; i++)
		{
			ToolStripMenuItem obj = toolStripMenuItem_1[i];
			ToolStripMenuItem obj2 = toolStripMenuItem_2[i];
			bool flag = (toolStripMenuItem_0[i].Visible = i < instrumentsNum);
			bool visible = (obj2.Visible = flag);
			obj.Visible = visible;
		}
		for (int j = 0; j < instrumentsNum; j++)
		{
			method_12(j, SysCfgDlg.sysConfig.RetInstrument(j));
		}
		method_10();
	}

	private void method_13(object sender, EventArgs e)
	{
		base.WindowState = FormWindowState.Normal;
		BringToFront();
	}

	private void method_14()
	{
		string text = ResourceImageLoad.ExePath() + "Common\\sys.cfg";
		if (File.Exists(text))
		{
			FileInfo fileInfo = new FileInfo(text);
			FileStream fileStream = fileInfo.Open(FileMode.Open);
			BinaryReader binaryReader = new BinaryReader(fileStream);
			try
			{
				SysCfgDlg.sysConfig.LoadFromFile(binaryReader);
				Instrus_ATDirsDialog.instrus_ATDirs.LoadFromFile(binaryReader);
			}
			finally
			{
				binaryReader.Close();
				fileStream.Close();
			}
		}
		else
		{
			Instrus_ATDirsDialog.instrus_ATDirs.DefaultDirections();
			SysCfgDlg.sysConfig.DefaultSystemConfig();
			Class49.kalphaDlg_0.DefaultFile();
		}
		text = ResourceImageLoad.ExePath() + "Common\\uos.cfg";
		if (File.Exists(text))
		{
			FileStream fileStream2 = new FileInfo(text).Open(FileMode.Open);
			BinaryReader binaryReader2 = new BinaryReader(fileStream2);
			try
			{
				UserAccountsDlg.userAccounts.LoadFromFile(binaryReader2);
			}
			finally
			{
				binaryReader2.Close();
				fileStream2.Close();
			}
		}
		else
		{
			UserAccountsDlg.userAccounts.DefaultUser();
		}
		Instrus_ATDirsDialog.instrus_ATDirs.CreateDirectories();
		Class49.loginDlg_0.RefreshUserList();
		sysCfgDlg_0.PageInstruRefreshCtrls();
		stationAdtTrlForm.RefreshMeanus(SysCfgDlg.sysConfig.instruments.Length);
		stationAdtTrlForm.CreateAndInit();
	}

	private void method_15()
	{
		FileStream fileStream = new FileInfo(ResourceImageLoad.ExePath() + "Common\\sys.cfg").Open(FileMode.OpenOrCreate);
		BinaryWriter binaryWriter = new BinaryWriter(fileStream);
		try
		{
			SysCfgDlg.sysConfig.SaveToFile(binaryWriter);
			Instrus_ATDirsDialog.instrus_ATDirs.SaveToFile(binaryWriter);
		}
		finally
		{
			binaryWriter.Close();
			fileStream.Close();
		}
		fileStream = new FileInfo(ResourceImageLoad.ExePath() + "Common\\uos.cfg").Open(FileMode.OpenOrCreate);
		binaryWriter = new BinaryWriter(fileStream);
		try
		{
			UserAccountsDlg.userAccounts.SaveToFile(binaryWriter);
		}
		finally
		{
			binaryWriter.Close();
			fileStream.Close();
		}
		string instruName = Lang.PS("系统", "System");
		string descript = Lang.PS("关闭系统", "Close System");
		stationAdtTrlForm.AddTail(-1, ATResult.Ok, ATType.CloseSys, "", instruName, ATArea.Sys, descript);
		stationAdtTrlForm.SaveToLog();
	}

	[DllImport("user32.dll", CharSet = CharSet.Auto)]
	private static extern uint UnregisterDeviceNotification(IntPtr intptr_1);

	protected override void WndProc(ref Message message_0)
	{
		if (message_0.Msg == 537)
		{
			int num = message_0.WParam.ToInt32();
			int num2 = num;
			if (num2 == 32768 || num2 == 32772)
			{
				Class3 @class = new Class3();
				Marshal.PtrToStructure(message_0.LParam, (object)@class);
				if (@class.int_1 == 5)
				{
					Class2 class2 = new Class2();
					Marshal.PtrToStructure(message_0.LParam, (object)class2);
					if (class2.guid_0 == guid_0)
					{
						sysCfgDlg_0.ScanHardWares();
					}
				}
			}
		}
		base.WndProc(ref message_0);
	}
}
