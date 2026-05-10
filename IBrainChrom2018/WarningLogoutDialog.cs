using System;
using System.ComponentModel;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace IBrainChrom2018;

public class WarningLogoutDialog : LclDialog
{
	public delegate void LogoutAllInstrus(object sender, EventArgs e);

	private IContainer icontainer_1;

	private LclLabel lbWarning;

	private LogoutAllInstrus logoutAllInstrus_0;

	public event LogoutAllInstrus OnLogoutAllInstrus
	{
		add
		{
			LogoutAllInstrus logoutAllInstrus = logoutAllInstrus_0;
			LogoutAllInstrus logoutAllInstrus2;
			do
			{
				logoutAllInstrus2 = logoutAllInstrus;
				LogoutAllInstrus value2 = (LogoutAllInstrus)Delegate.Combine(logoutAllInstrus2, value);
				logoutAllInstrus = Interlocked.CompareExchange(ref logoutAllInstrus_0, value2, logoutAllInstrus2);
			}
			while (logoutAllInstrus != logoutAllInstrus2);
		}
		remove
		{
			LogoutAllInstrus logoutAllInstrus = logoutAllInstrus_0;
			LogoutAllInstrus logoutAllInstrus2;
			do
			{
				logoutAllInstrus2 = logoutAllInstrus;
				LogoutAllInstrus value2 = (LogoutAllInstrus)Delegate.Remove(logoutAllInstrus2, value);
				logoutAllInstrus = Interlocked.CompareExchange(ref logoutAllInstrus_0, value2, logoutAllInstrus2);
			}
			while (logoutAllInstrus != logoutAllInstrus2);
		}
	}

	public WarningLogoutDialog()
	{
		InitializeComponent();
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
		this.lbWarning = new IBrainChrom2018.LclLabel();
		base.SuspendLayout();
		base.btnOK.Location = new System.Drawing.Point(23, 46);
		base.btnOK.Size = new System.Drawing.Size(134, 23);
		base.btnCancel.Location = new System.Drawing.Point(181, 46);
		base.btnCancel.Size = new System.Drawing.Size(118, 23);
		base.btnHelp.Location = new System.Drawing.Point(237, 3);
		base.btnHelp.Visible = false;
		this.lbWarning.AutoSize = true;
		this.lbWarning.Location = new System.Drawing.Point(21, 14);
		this.lbWarning.Name = "lbWarning";
		this.lbWarning.Size = new System.Drawing.Size(59, 12);
		this.lbWarning.TabIndex = 1;
		this.lbWarning.Text = "lclLabel1";
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
		base.ClientSize = new System.Drawing.Size(338, 85);
		base.Controls.Add(this.lbWarning);
		base.Name = "WarningLogoutDialog";
		base.Controls.SetChildIndex(base.btnCancel, 0);
		base.Controls.SetChildIndex(base.btnOK, 0);
		base.Controls.SetChildIndex(base.btnHelp, 0);
		base.Controls.SetChildIndex(this.lbWarning, 0);
		base.ResumeLayout(false);
		base.PerformLayout();
	}

	public override void LoadLanguage()
	{
		base.LoadLanguage();
		Text = Lang.PS("警告", "Warn");
		lbWarning.Text = Lang.PS("配置前请先退出登录的仪器！", "Logout instrument(s) before configuration!");
		btnOK.Text = Lang.PS("全部注销", "Logout All");
		btnCancel.Text = Lang.PS("取消", "Cancel");
	}

	public bool LogoutAllInstruments()
	{
		if (SysCfgDlg.sysConfig.GetLoggedInstrusNum() == 0)
		{
			return true;
		}
		if (ShowDialog() == DialogResult.OK)
		{
			if (logoutAllInstrus_0 != null)
			{
				logoutAllInstrus_0(this, null);
			}
			return SysCfgDlg.sysConfig.GetLoggedInstrusNum() == 0;
		}
		return false;
	}
}
