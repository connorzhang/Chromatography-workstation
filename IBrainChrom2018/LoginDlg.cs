using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace IBrainChrom2018;

public class LoginDlg : LclDialog
{
	private const string string_0 = "所有可用仪器";

	private const string string_1 = "无效密码！";

	private const string string_2 = "选择用户及输入密码：";

	private const string string_3 = "登录";

	private const string string_4 = "All Possible Instruments";

	private const string string_5 = "Invalid Password!";

	private const string string_6 = "Choose User and Enter Password:";

	private const string string_7 = "Login";

	private LclCheckBox cbAllInstruments;

	private LclComboBox cbUsers;

	private IContainer icontainer_1;

	private LclLabel lbExpress;

	public int openInstruNo;

	public int[] openInstruNos = new int[0];

	private LclTextBox tbKey;

	public User user;

	private string sInvalidPassword => Class49.sysLanguage_0 switch
	{
		SysLanguage.CN => "无效密码！", 
		SysLanguage.EN => "Invalid Password!", 
		_ => "", 
	};

	public LoginDlg()
	{
		InitializeComponent();
	}

	private void method_0(object sender, EventArgs e)
	{
		user = UserAccountsDlg.userAccounts.UserLogin(cbUsers.Text, tbKey.Text);
		if (user != null)
		{
			base.DialogResult = DialogResult.OK;
		}
		else
		{
			MessageBox.Show(sInvalidPassword);
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
		this.lbExpress = new IBrainChrom2018.LclLabel();
		this.cbUsers = new IBrainChrom2018.LclComboBox();
		this.tbKey = new IBrainChrom2018.LclTextBox();
		this.cbAllInstruments = new IBrainChrom2018.LclCheckBox();
		base.SuspendLayout();
		base.btnOK.Location = new System.Drawing.Point(16, 102);
		base.btnOK.Size = new System.Drawing.Size(65, 23);
		base.btnOK.Text = "确认";
		base.btnOK.Click += new System.EventHandler(method_0);
		base.btnCancel.Location = new System.Drawing.Point(97, 102);
		base.btnCancel.Size = new System.Drawing.Size(65, 23);
		base.btnCancel.Text = "取消";
		base.btnHelp.Location = new System.Drawing.Point(178, 102);
		base.btnHelp.Size = new System.Drawing.Size(65, 23);
		base.btnHelp.Text = "帮助";
		this.lbExpress.AutoSize = true;
		this.lbExpress.Location = new System.Drawing.Point(12, 9);
		this.lbExpress.Name = "lbExpress";
		this.lbExpress.Size = new System.Drawing.Size(59, 12);
		this.lbExpress.TabIndex = 1;
		this.lbExpress.Text = "lclLabel1";
		this.cbUsers.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.cbUsers.FormattingEnabled = true;
		this.cbUsers.ItemExtString = "";
		this.cbUsers.Location = new System.Drawing.Point(14, 24);
		this.cbUsers.Name = "cbUsers";
		this.cbUsers.Size = new System.Drawing.Size(233, 20);
		this.cbUsers.TabIndex = 2;
		this.tbKey.Location = new System.Drawing.Point(14, 50);
		this.tbKey.Name = "tbKey";
		this.tbKey.PasswordChar = '*';
		this.tbKey.Size = new System.Drawing.Size(233, 21);
		this.tbKey.TabIndex = 3;
		this.cbAllInstruments.AutoSize = true;
		this.cbAllInstruments.Location = new System.Drawing.Point(14, 77);
		this.cbAllInstruments.Name = "cbAllInstruments";
		this.cbAllInstruments.Size = new System.Drawing.Size(96, 16);
		this.cbAllInstruments.TabIndex = 4;
		this.cbAllInstruments.Text = "lclCheckBox1";
		this.cbAllInstruments.UseVisualStyleBackColor = true;
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
		base.ClientSize = new System.Drawing.Size(262, 137);
		base.Controls.Add(this.lbExpress);
		base.Controls.Add(this.cbUsers);
		base.Controls.Add(this.tbKey);
		base.Controls.Add(this.cbAllInstruments);
		base.Name = "LoginDlg";
		base.Load += new System.EventHandler(LoginDlg_Load);
		base.Controls.SetChildIndex(this.cbAllInstruments, 0);
		base.Controls.SetChildIndex(base.btnOK, 0);
		base.Controls.SetChildIndex(base.btnCancel, 0);
		base.Controls.SetChildIndex(this.tbKey, 0);
		base.Controls.SetChildIndex(this.cbUsers, 0);
		base.Controls.SetChildIndex(base.btnHelp, 0);
		base.Controls.SetChildIndex(this.lbExpress, 0);
		base.ResumeLayout(false);
		base.PerformLayout();
	}

	private bool method_1(string string_8)
	{
		for (int i = 0; i < SysCfgDlg.sysConfig.pageInstrus.Length; i++)
		{
			if (SysCfgDlg.sysConfig.pageInstrus[i].logged && SysCfgDlg.sysConfig.pageInstrus[i].user.u_name == string_8)
			{
				return false;
			}
		}
		return true;
	}

	public override void LoadLanguage()
	{
		base.LoadLanguage();
		switch (Class49.sysLanguage_0)
		{
		case SysLanguage.CN:
			Text = "登录";
			lbExpress.Text = "选择用户及输入密码：";
			cbAllInstruments.Text = "所有可用仪器";
			break;
		case SysLanguage.EN:
			Text = "Login";
			lbExpress.Text = "Choose User and Enter Password:";
			cbAllInstruments.Text = "All Possible Instruments";
			break;
		}
	}

	private void LoginDlg_Load(object sender, EventArgs e)
	{
		tbKey.Text = "";
	}

	public void RefreshUserList()
	{
		cbUsers.Items.Clear();
		for (int i = 0; i < UserAccountsDlg.userAccounts.users.Length; i++)
		{
			cbUsers.Items.Add(UserAccountsDlg.userAccounts.users[i].u_name);
		}
		if (cbUsers.Items.Count != 0)
		{
			cbUsers.SelectedIndex = 0;
		}
	}

	public bool ShowDialog(AccessType accessType)
	{
		cbAllInstruments.Visible = accessType == AccessType.OpenInstrus;
		if (ShowDialog() == DialogResult.OK)
		{
			if (user == null)
			{
				return false;
			}
			bool flag = false;
			switch (accessType)
			{
			case AccessType.Unlock:
				return true;
			case AccessType.OpenUserAccounts:
				flag = user.uar_OpenUserAccounts;
				break;
			case AccessType.OpenConfiguration:
				flag = user.uar_OpenConfiguration;
				break;
			case AccessType.OpenAuditTrailSettings:
				flag = user.uar_OpenAuditTrailSettings;
				break;
			case AccessType.OpenInstrus:
			{
				int days = DateTime.Now.Subtract(user.ui_createDT).Days;
				if (UserAccountsDlg.userAccounts.useLifeTime && days > UserAccountsDlg.userAccounts.lifeTime)
				{
					MessageBox.Show(Lang.PS("用户过期", "User Overdue"));
					return false;
				}
				int num = UserAccountsDlg.userAccounts.lifeTime - days;
				if (UserAccountsDlg.userAccounts.useLifeTime && UserAccountsDlg.userAccounts.useExpirWarning && num < UserAccountsDlg.userAccounts.expirationWarning)
				{
					MessageBox.Show(num.ToString(Lang.PS("用户将在0天过期", "User would overdue after 0 days")));
				}
				if (cbAllInstruments.Checked)
				{
					int num2 = 0;
					if (user.at_Instru1)
					{
						num2++;
					}
					if (user.at_Instru2)
					{
						num2++;
					}
					if (user.at_Instru3)
					{
						num2++;
					}
					if (user.at_Instru4)
					{
						num2++;
					}
					Array.Resize(ref openInstruNos, num2);
					num2 = 0;
					if (user.at_Instru1)
					{
						openInstruNos[num2++] = 0;
					}
					if (user.at_Instru2)
					{
						openInstruNos[num2++] = 1;
					}
					if (user.at_Instru3)
					{
						openInstruNos[num2++] = 2;
					}
					if (user.at_Instru4)
					{
						openInstruNos[num2++] = 3;
					}
					flag = num2 != 0;
				}
				else
				{
					switch (openInstruNo)
					{
					case 0:
						flag = user.at_Instru1;
						break;
					case 1:
						flag = user.at_Instru2;
						break;
					case 2:
						flag = user.at_Instru3;
						break;
					case 3:
						flag = user.at_Instru4;
						break;
					}
					if (flag)
					{
						Array.Resize(ref openInstruNos, 1);
						openInstruNos[0] = openInstruNo;
					}
				}
				if (flag && method_1(user.u_name))
				{
					user.LoadUserOptions();
				}
				break;
			}
			}
			if (flag)
			{
				return true;
			}
			User.MessageNoAccessRights();
		}
		return false;
	}
}
