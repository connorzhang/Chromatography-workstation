using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace IBrainChrom2018;

public class UserAccountsDlg : LclDialog
{
	private const string string_0 = "修改密码";

	private const string string_1 = "仪器1";

	private const string string_2 = "仪器2";

	private const string string_3 = "仪器3";

	private const string string_4 = "仪器4";

	private const string string_5 = "提前警告";

	private const string string_6 = "有效期";

	private const string string_7 = "最小长度";

	private const string string_8 = "编辑校正";

	private const string string_9 = "编辑谱图";

	private const string string_10 = "编辑方法";

	private const string string_11 = "编辑报告规则";

	private const string string_12 = "编辑队列";

	private const string string_13 = "打开日志设置";

	private const string string_14 = "打开配置";

	private const string string_15 = "打开用户帐户";

	private const string string_16 = "选择方法";

	private const string string_17 = "密码规则 - [全体有效]";

	private const string string_18 = "用户";

	private const string string_19 = "访问权限";

	private const string string_20 = "用户信息";

	private const string string_21 = "用户列表";

	private const string string_22 = "[天]";

	private const string string_23 = "[天]";

	private const string string_24 = "[字符]";

	private const string string_25 = "最后登录：";

	private const string string_26 = "最后修改：";

	private const string string_27 = "密码状态：";

	private const string string_28 = "描述";

	private const string string_29 = "用户名";

	private const string string_30 = "用户明细";

	private const string string_31 = "将没有用户有权限访问本帐户管理窗口！";

	private const string string_32 = "[空]";

	private const string string_33 = "提交";

	private const string string_34 = "用户帐户";

	private const string string_35 = "Change Password";

	private const string string_36 = "Instrument1";

	private const string string_37 = "Instrument2";

	private const string string_38 = "Instrument3";

	private const string string_39 = "Instrument4";

	private const string string_40 = "Expiration Warning";

	private const string string_41 = "Life Time";

	private const string string_42 = "Min Length";

	private const string string_43 = "Edit Calibration";

	private const string string_44 = "Edit Chromatogram";

	private const string string_45 = "Edit Method";

	private const string string_46 = "Edit ReportStyle";

	private const string string_47 = "Edit Sequence";

	private const string string_48 = "Open Audit Trail Settings";

	private const string string_49 = "Open Configuration";

	private const string string_50 = "Open User Accounts";

	private const string string_51 = "Select Method";

	private const string string_52 = "Password Restrictions - Common for all";

	private const string string_53 = "User";

	private const string string_54 = "User Access Rights";

	private const string string_55 = "User Info";

	private const string string_56 = "User List";

	private const string string_57 = "[days]";

	private const string string_58 = "[days]";

	private const string string_59 = "[chars]";

	private const string string_60 = "LastLogin:";

	private const string string_61 = "Set Time:";

	private const string string_62 = "Pwd State:";

	private const string string_63 = "Description";

	private const string string_64 = "User Name";

	private const string string_65 = "User Details";

	private const string string_66 = "No User Can Open AccountsForm!";

	private const string string_67 = "[Blank]";

	private const string string_68 = "Submitted";

	private const string string_69 = "User Accounts";

	private LclButton btnSgSizeMode;

	private LclButton btnuiChangePassword;

	private LclButton btnulDeleteUser;

	private LclButton btnulNewUser;

	private LclCheckBox cbatInstru1;

	private LclCheckBox cbatInstru2;

	private LclCheckBox cbatInstru3;

	private LclCheckBox cbatInstru4;

	private LclCheckBox cbprExpirWarning;

	private LclCheckBox cbprLifeTime;

	private LclCheckBox cbprMinLength;

	private LclCheckBox cbuarEditCalibration;

	private LclCheckBox cbuarEditChromatogram;

	private LclCheckBox cbuarEditMethod;

	private LclCheckBox cbuarEditReportStyle;

	private LclCheckBox cbuarEditSequence;

	private LclCheckBox cbuarOpenATSettings;

	private LclCheckBox cbuarOpenConfiguration;

	private LclCheckBox cbuarOpenUserAccounts;

	private LclCheckBox cbuarSelectMethod;

	private IContainer icontainer_1;

	private User user_0;

	public static Size defaultSignGraph = new Size(159, 35);

	private ChgPswdDlg chgPswdDlg_0;

	private LclGroupBox gbPasswordRestricts;

	private LclGroupBox gbPerInfo;

	private LclGroupBox gbUser;

	private LclGroupBox gbUserAccessRights;

	private LclGroupBox gbUserInfo;

	private LclGroupBox gbUserList;

	private LclGridView gvulNames;

	private LclLabel lbprExpirWarning;

	private LclLabel lbprLifeTime;

	private LclLabel lbprMinLength;

	private LclLabel lbSign;

	private LclLabel lbuiCreateDT;

	private LclLabel lbuiDateCT;

	private LclLabel lbuiLastLogin;

	private LclLabel lbuiLastLoginV;

	private LclLabel lbuiPasswordSetTime;

	private LclLabel lbuiPasswordSetTimeV;

	private LclLabel lbuiPasswordState;

	private LclLabel lbuiPasswordStateV;

	private LclLabel lburDescription;

	private LclLabel lburUserName;

	private LclLabel lbUserDetails;

	private UserAccounts userAccounts_0 = new UserAccounts();

	private LclNumericUpDown nudprExpirWarning;

	private LclNumericUpDown nudprLifeTime;

	private LclNumericUpDown nudprMinLength;

	private OpenFileDialog openFileDialog_0;

	private LclPictureBox pbSignGraph;

	private TextBox tbPersonInfo;

	private TextBox tburDescription;

	private TextBox tburUserName;

	public static UserAccounts userAccounts = new UserAccounts();

	private string sNoUserCanOpenAccountsForm => Class49.sysLanguage_0 switch
	{
		SysLanguage.CN => "将没有用户有权限访问本帐户管理窗口！", 
		SysLanguage.EN => "No User Can Open AccountsForm!", 
		_ => "", 
	};

	private string spsBlank => Class49.sysLanguage_0 switch
	{
		SysLanguage.CN => "[空]", 
		SysLanguage.EN => "[Blank]", 
		_ => "", 
	};

	private string spsSubmitted => Class49.sysLanguage_0 switch
	{
		SysLanguage.CN => "提交", 
		SysLanguage.EN => "Submitted", 
		_ => "", 
	};

	public UserAccountsDlg()
	{
		InitializeComponent_1();
		gbPerInfo.Text = Lang.PS("个人资料", "Personal Information");
		lbSign.Text = Lang.PS("签名", "Sign");
		btnSgSizeMode.Text = Lang.PS("图像模式", "pic. Mode");
		lbuiDateCT.Text = Lang.PS("创建日期", "Create Date");
		gvulNames.AddLclTextBoxColumn("", gvulNames.Width - 25, StringAlignment.Near);
	}

	private void btnSgSizeMode_Click(object sender, EventArgs e)
	{
		switch (user_0.sgSizeMode)
		{
		case PictureBoxSizeMode.Normal:
			user_0.sgSizeMode = PictureBoxSizeMode.StretchImage;
			break;
		case PictureBoxSizeMode.StretchImage:
			user_0.sgSizeMode = PictureBoxSizeMode.Zoom;
			break;
		case PictureBoxSizeMode.AutoSize:
			user_0.sgSizeMode = PictureBoxSizeMode.CenterImage;
			pbSignGraph.Size = defaultSignGraph;
			break;
		case PictureBoxSizeMode.CenterImage:
			user_0.sgSizeMode = PictureBoxSizeMode.Normal;
			break;
		case PictureBoxSizeMode.Zoom:
			user_0.sgSizeMode = PictureBoxSizeMode.AutoSize;
			break;
		}
		pbSignGraph.SizeMode = user_0.sgSizeMode;
	}

	private void method_0(object sender, EventArgs e)
	{
		if (sender == btnOK)
		{
			bool flag = false;
			for (int i = 0; i < userAccounts_0.users.Length; i++)
			{
				if (userAccounts_0.users[i].uar_OpenUserAccounts)
				{
					if (1 == 0)
					{
						MessageBox.Show(sNoUserCanOpenAccountsForm, Lang.PS("错误", "Error"), MessageBoxButtons.OK, MessageBoxIcon.Hand);
					}
					else
					{
						userAccounts.LoadFromObject(userAccounts_0);
						Class49.loginDlg_0.RefreshUserList();
					}
					return;
				}
			}
		}
		if (sender != btnCancel && sender != btnHelp)
		{
		}
	}

	private void btnuiChangePassword_Click(object sender, EventArgs e)
	{
		if (user_0 != null && chgPswdDlg_0.ShowDialog() == DialogResult.OK)
		{
			user_0.ui_ModifyPassword(chgPswdDlg_0.newPassword);
			lbuiPasswordStateV.Text = ((user_0.ui_passwordState == PasswordState.Blank) ? spsBlank : spsSubmitted);
			lbuiPasswordSetTimeV.Text = user_0.ui_lastPasswordSet.ToLongDateString();
		}
	}

	private void btnulNewUser_Click(object sender, EventArgs e)
	{
		if (sender == btnulNewUser)
		{
			User user = new User();
			if (userAccounts_0.AddUser(user))
			{
				gvulNames.RowCount++;
			}
			gvulNames.Rows[gvulNames.RowCount - 1].Selected = true;
			gvulNames_SelectionChanged(null, null);
		}
		else if (sender == btnulDeleteUser)
		{
			if (gvulNames.SelectedRows.Count == 1)
			{
				string userName = gvulNames.SelectedRows[0].Cells[0].Value.ToString();
				userAccounts_0.DeleteUser(userName);
			}
			method_1();
		}
	}

	private void cbatInstru1_Click(object sender, EventArgs e)
	{
		if (user_0 != null)
		{
			CheckBox checkBox = sender as CheckBox;
			if (checkBox == cbatInstru1)
			{
				user_0.at_Instru1 = checkBox.Checked;
			}
			else if (checkBox == cbatInstru2)
			{
				user_0.at_Instru2 = checkBox.Checked;
			}
			else if (checkBox == cbatInstru3)
			{
				user_0.at_Instru3 = checkBox.Checked;
			}
			else if (checkBox == cbatInstru4)
			{
				user_0.at_Instru4 = checkBox.Checked;
			}
		}
	}

	private void cbprMinLength_Click(object sender, EventArgs e)
	{
		CheckBox checkBox = sender as CheckBox;
		if (checkBox == cbprMinLength)
		{
			userAccounts_0.useMinLength = checkBox.Checked;
		}
		else if (checkBox == cbprLifeTime)
		{
			userAccounts_0.useLifeTime = checkBox.Checked;
		}
		else if (checkBox == cbprExpirWarning)
		{
			userAccounts_0.useExpirWarning = checkBox.Checked;
		}
	}

	private void cbuarOpenUserAccounts_Click(object sender, EventArgs e)
	{
		if (user_0 != null)
		{
			CheckBox checkBox = sender as CheckBox;
			if (checkBox == cbuarOpenUserAccounts)
			{
				user_0.uar_OpenUserAccounts = checkBox.Checked;
			}
			else if (checkBox == cbuarOpenConfiguration)
			{
				user_0.uar_OpenConfiguration = checkBox.Checked;
			}
			else if (checkBox == cbuarEditMethod)
			{
				user_0.uar_EditMethod = checkBox.Checked;
			}
			else if (checkBox == cbuarEditChromatogram)
			{
				user_0.uar_EditChromatogram = checkBox.Checked;
			}
			else if (checkBox == cbuarEditCalibration)
			{
				user_0.uar_EditCalibration = checkBox.Checked;
			}
			else if (checkBox == cbuarEditSequence)
			{
				user_0.uar_EditSequence = checkBox.Checked;
			}
			else if (checkBox == cbuarEditReportStyle)
			{
				user_0.uar_EditReportStyle = checkBox.Checked;
			}
			else if (checkBox == cbuarSelectMethod)
			{
				user_0.uar_SelectMethod = checkBox.Checked;
			}
			else if (checkBox == cbuarOpenATSettings)
			{
				user_0.uar_OpenAuditTrailSettings = checkBox.Checked;
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

	private void gvulNames_SelectionChanged(object sender, EventArgs e)
	{
		if (gvulNames.SelectedRows.Count != 1)
		{
			user_0 = null;
			tburUserName.Text = "";
			tburDescription.Text = "";
			cbuarOpenUserAccounts.Checked = false;
			cbuarOpenConfiguration.Checked = false;
			cbuarEditMethod.Checked = false;
			cbuarEditChromatogram.Checked = false;
			cbuarEditCalibration.Checked = false;
			cbuarEditSequence.Checked = false;
			cbuarEditReportStyle.Checked = false;
			cbuarSelectMethod.Checked = false;
			cbuarOpenATSettings.Checked = false;
			lbuiPasswordStateV.Text = "";
			lbuiPasswordSetTimeV.Text = "";
			lbuiLastLoginV.Text = "";
			cbatInstru1.Checked = false;
			cbatInstru2.Checked = false;
			cbatInstru3.Checked = false;
			cbatInstru4.Checked = false;
			tbPersonInfo.Text = "";
			pbSignGraph.Image = null;
			return;
		}
		user_0 = userAccounts_0.users[gvulNames.SelectedRows[0].Index];
		tburUserName.Text = user_0.u_name;
		tburDescription.Text = user_0.u_description;
		cbuarOpenUserAccounts.Checked = user_0.uar_OpenUserAccounts;
		cbuarOpenConfiguration.Checked = user_0.uar_OpenConfiguration;
		cbuarEditMethod.Checked = user_0.uar_EditMethod;
		cbuarEditChromatogram.Checked = user_0.uar_EditChromatogram;
		cbuarEditCalibration.Checked = user_0.uar_EditCalibration;
		cbuarEditSequence.Checked = user_0.uar_EditSequence;
		cbuarEditReportStyle.Checked = user_0.uar_EditReportStyle;
		cbuarSelectMethod.Checked = user_0.uar_SelectMethod;
		cbuarOpenATSettings.Checked = user_0.uar_OpenAuditTrailSettings;
		lbuiCreateDT.Text = user_0.ui_createDT.ToLongDateString();
		lbuiPasswordStateV.Text = ((user_0.ui_passwordState == PasswordState.Blank) ? spsBlank : spsSubmitted);
		if (user_0.ui_passwordState == PasswordState.Submitted)
		{
			lbuiPasswordSetTimeV.Text = user_0.ui_lastPasswordSet.ToLongDateString();
		}
		else if (user_0.ui_passwordState == PasswordState.Blank)
		{
			lbuiPasswordSetTimeV.Text = "";
		}
		lbuiPasswordSetTimeV.Text = user_0.ui_lastPasswordSet.ToLongDateString();
		lbuiLastLoginV.Text = user_0.ui_lastLogin.ToLongDateString();
		cbatInstru1.Checked = user_0.at_Instru1;
		cbatInstru2.Checked = user_0.at_Instru2;
		cbatInstru3.Checked = user_0.at_Instru3;
		cbatInstru4.Checked = user_0.at_Instru4;
		tbPersonInfo.Text = user_0.personInfo;
		if (user_0.signGraph != "" && File.Exists(user_0.signGraph))
		{
			pbSignGraph.Image = Image.FromFile(user_0.signGraph);
			pbSignGraph.SizeMode = user_0.sgSizeMode;
		}
		else
		{
			pbSignGraph.Image = null;
		}
	}

	private void InitializeComponent_1()
	{
		lbuiPasswordSetTimeV = new LclLabel();
		lbuiPasswordStateV = new LclLabel();
		lbuiPasswordSetTime = new LclLabel();
		lbuiPasswordState = new LclLabel();
		btnuiChangePassword = new LclButton();
		lburDescription = new LclLabel();
		lburUserName = new LclLabel();
		tburDescription = new TextBox();
		tburUserName = new TextBox();
		btnulDeleteUser = new LclButton();
		btnulNewUser = new LclButton();
		gvulNames = new LclGridView();
		lbUserDetails = new LclLabel();
		gbUserList = new LclGroupBox();
		gbPasswordRestricts = new LclGroupBox();
		nudprExpirWarning = new LclNumericUpDown();
		nudprLifeTime = new LclNumericUpDown();
		lbprExpirWarning = new LclLabel();
		lbprLifeTime = new LclLabel();
		lbprMinLength = new LclLabel();
		nudprMinLength = new LclNumericUpDown();
		cbprExpirWarning = new LclCheckBox();
		cbprLifeTime = new LclCheckBox();
		cbprMinLength = new LclCheckBox();
		gbUser = new LclGroupBox();
		gbUserInfo = new LclGroupBox();
		lbuiDateCT = new LclLabel();
		lbuiLastLoginV = new LclLabel();
		lbuiLastLogin = new LclLabel();
		lbuiCreateDT = new LclLabel();
		gbUserAccessRights = new LclGroupBox();
		cbatInstru4 = new LclCheckBox();
		cbuarEditCalibration = new LclCheckBox();
		cbatInstru3 = new LclCheckBox();
		cbuarOpenATSettings = new LclCheckBox();
		cbatInstru2 = new LclCheckBox();
		cbuarEditChromatogram = new LclCheckBox();
		cbatInstru1 = new LclCheckBox();
		cbuarSelectMethod = new LclCheckBox();
		cbuarEditReportStyle = new LclCheckBox();
		cbuarEditMethod = new LclCheckBox();
		cbuarEditSequence = new LclCheckBox();
		cbuarOpenConfiguration = new LclCheckBox();
		cbuarOpenUserAccounts = new LclCheckBox();
		gbPerInfo = new LclGroupBox();
		pbSignGraph = new LclPictureBox();
		lbSign = new LclLabel();
		btnSgSizeMode = new LclButton();
		tbPersonInfo = new TextBox();
		((ISupportInitialize)gvulNames).BeginInit();
		gbUserList.SuspendLayout();
		gbPasswordRestricts.SuspendLayout();
		nudprExpirWarning.BeginInit();
		nudprLifeTime.BeginInit();
		nudprMinLength.BeginInit();
		gbUser.SuspendLayout();
		gbUserInfo.SuspendLayout();
		gbUserAccessRights.SuspendLayout();
		gbPerInfo.SuspendLayout();
		((ISupportInitialize)pbSignGraph).BeginInit();
		SuspendLayout();
		btnOK.Location = new Point(258, 380);
		btnOK.Text = "确认";
		btnOK.Click += method_0;
		btnCancel.Location = new Point(352, 380);
		btnCancel.Text = "取消";
		btnCancel.Click += method_0;
		btnHelp.Location = new Point(444, 380);
		btnHelp.Text = "帮助";
		btnHelp.Click += method_0;
		lbuiPasswordSetTimeV.AutoSize = true;
		lbuiPasswordSetTimeV.Location = new Point(92, 55);
		lbuiPasswordSetTimeV.Name = "lbuiPasswordSetTimeV";
		lbuiPasswordSetTimeV.Size = new Size(59, 12);
		lbuiPasswordSetTimeV.TabIndex = 12;
		lbuiPasswordSetTimeV.Text = "lclLabel1";
		lbuiPasswordStateV.AutoSize = true;
		lbuiPasswordStateV.Location = new Point(92, 37);
		lbuiPasswordStateV.Name = "lbuiPasswordStateV";
		lbuiPasswordStateV.Size = new Size(59, 12);
		lbuiPasswordStateV.TabIndex = 12;
		lbuiPasswordStateV.Text = "lclLabel1";
		lbuiPasswordSetTime.AutoSize = true;
		lbuiPasswordSetTime.Location = new Point(14, 55);
		lbuiPasswordSetTime.Name = "lbuiPasswordSetTime";
		lbuiPasswordSetTime.Size = new Size(59, 12);
		lbuiPasswordSetTime.TabIndex = 12;
		lbuiPasswordSetTime.Text = "lclLabel1";
		lbuiPasswordState.AutoSize = true;
		lbuiPasswordState.Location = new Point(14, 37);
		lbuiPasswordState.Name = "lbuiPasswordState";
		lbuiPasswordState.Size = new Size(59, 12);
		lbuiPasswordState.TabIndex = 12;
		lbuiPasswordState.Text = "lclLabel1";
		btnuiChangePassword.Location = new Point(209, 33);
		btnuiChangePassword.Name = "btnuiChangePassword";
		btnuiChangePassword.Size = new Size(70, 20);
		btnuiChangePassword.TabIndex = 12;
		btnuiChangePassword.Text = "lclButton1";
		btnuiChangePassword.UseVisualStyleBackColor = true;
		btnuiChangePassword.Click += btnuiChangePassword_Click;
		lburDescription.AutoSize = true;
		lburDescription.Location = new Point(14, 45);
		lburDescription.Name = "lburDescription";
		lburDescription.Size = new Size(59, 12);
		lburDescription.TabIndex = 12;
		lburDescription.Text = "lclLabel1";
		lburUserName.AutoSize = true;
		lburUserName.Location = new Point(14, 23);
		lburUserName.Name = "lburUserName";
		lburUserName.Size = new Size(59, 12);
		lburUserName.TabIndex = 12;
		lburUserName.Text = "lclLabel1";
		tburDescription.Location = new Point(94, 42);
		tburDescription.Name = "tburDescription";
		tburDescription.Size = new Size(174, 21);
		tburDescription.TabIndex = 3;
		tburDescription.TextChanged += tbPersonInfo_TextChanged;
		tburUserName.Location = new Point(94, 18);
		tburUserName.Name = "tburUserName";
		tburUserName.Size = new Size(174, 21);
		tburUserName.TabIndex = 3;
		tburUserName.TextChanged += tbPersonInfo_TextChanged;
		btnulDeleteUser.Location = new Point(10, 69);
		btnulDeleteUser.Name = "btnulDeleteUser";
		btnulDeleteUser.Size = new Size(70, 20);
		btnulDeleteUser.TabIndex = 12;
		btnulDeleteUser.Text = "lclButton1";
		btnulDeleteUser.UseVisualStyleBackColor = true;
		btnulDeleteUser.Click += btnulNewUser_Click;
		btnulNewUser.Location = new Point(10, 48);
		btnulNewUser.Name = "btnulNewUser";
		btnulNewUser.Size = new Size(70, 20);
		btnulNewUser.TabIndex = 12;
		btnulNewUser.Text = "lclButton1";
		btnulNewUser.UseVisualStyleBackColor = true;
		btnulNewUser.Click += btnulNewUser_Click;
		gvulNames.AllowUserToAddRows = false;
		gvulNames.AllowUserToDeleteRows = false;
		gvulNames.AllowUserToResizeRows = false;
		gvulNames.BackgroundColor = Color.AliceBlue;
		gvulNames.CharacterHeaderColor = Color.Black;
		gvulNames.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
		gvulNames.ColumnHeadersVisible = false;
		gvulNames.EditMode = DataGridViewEditMode.EditProgrammatically;
		gvulNames.Location = new Point(86, 17);
		gvulNames.MultiSelect = false;
		gvulNames.Name = "gvulNames";
		gvulNames.ReadOnly = true;
		gvulNames.RowHeadersVisible = false;
		gvulNames.RowHeadersWidth = 25;
		gvulNames.RowTemplate.Height = 16;
		gvulNames.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
		gvulNames.ShowCellToolTips = false;
		gvulNames.Size = new Size(197, 72);
		gvulNames.TabIndex = 2;
		gvulNames.SelectionChanged += gvulNames_SelectionChanged;
		lbUserDetails.AutoSize = true;
		lbUserDetails.Location = new Point(251, 107);
		lbUserDetails.Name = "lbUserDetails";
		lbUserDetails.Size = new Size(59, 12);
		lbUserDetails.TabIndex = 12;
		lbUserDetails.Text = "lclLabel1";
		gbUserList.Controls.Add(btnulDeleteUser);
		gbUserList.Controls.Add(gvulNames);
		gbUserList.Controls.Add(btnulNewUser);
		gbUserList.Location = new Point(7, 8);
		gbUserList.Name = "gbUserList";
		gbUserList.Size = new Size(291, 96);
		gbUserList.TabIndex = 13;
		gbUserList.TabStop = false;
		gbUserList.Text = "lclGroupBox1";
		gbPasswordRestricts.Controls.Add(nudprExpirWarning);
		gbPasswordRestricts.Controls.Add(nudprLifeTime);
		gbPasswordRestricts.Controls.Add(lbprExpirWarning);
		gbPasswordRestricts.Controls.Add(lbprLifeTime);
		gbPasswordRestricts.Controls.Add(lbprMinLength);
		gbPasswordRestricts.Controls.Add(nudprMinLength);
		gbPasswordRestricts.Controls.Add(cbprExpirWarning);
		gbPasswordRestricts.Controls.Add(cbprLifeTime);
		gbPasswordRestricts.Controls.Add(cbprMinLength);
		gbPasswordRestricts.Location = new Point(304, 8);
		gbPasswordRestricts.Name = "gbPasswordRestricts";
		gbPasswordRestricts.Size = new Size(275, 96);
		gbPasswordRestricts.TabIndex = 14;
		gbPasswordRestricts.TabStop = false;
		gbPasswordRestricts.Text = "lclGroupBox1";
		nudprExpirWarning.Location = new Point(145, 67);
		nudprExpirWarning.Name = "nudprExpirWarning";
		nudprExpirWarning.Size = new Size(55, 21);
		nudprExpirWarning.TabIndex = 1;
		nudprExpirWarning.ValueChanged += nudprMinLength_ValueChanged;
		nudprLifeTime.Location = new Point(145, 42);
		nudprLifeTime.Name = "nudprLifeTime";
		nudprLifeTime.Size = new Size(55, 21);
		nudprLifeTime.TabIndex = 1;
		nudprLifeTime.ValueChanged += nudprMinLength_ValueChanged;
		lbprExpirWarning.AutoSize = true;
		lbprExpirWarning.Location = new Point(206, 72);
		lbprExpirWarning.Name = "lbprExpirWarning";
		lbprExpirWarning.Size = new Size(59, 12);
		lbprExpirWarning.TabIndex = 12;
		lbprExpirWarning.Text = "lclLabel1";
		lbprLifeTime.AutoSize = true;
		lbprLifeTime.Location = new Point(206, 47);
		lbprLifeTime.Name = "lbprLifeTime";
		lbprLifeTime.Size = new Size(59, 12);
		lbprLifeTime.TabIndex = 12;
		lbprLifeTime.Text = "lclLabel1";
		lbprMinLength.AutoSize = true;
		lbprMinLength.Location = new Point(206, 22);
		lbprMinLength.Name = "lbprMinLength";
		lbprMinLength.Size = new Size(59, 12);
		lbprMinLength.TabIndex = 12;
		lbprMinLength.Text = "lclLabel1";
		nudprMinLength.Location = new Point(145, 17);
		nudprMinLength.Name = "nudprMinLength";
		nudprMinLength.Size = new Size(55, 21);
		nudprMinLength.TabIndex = 1;
		nudprMinLength.ValueChanged += nudprMinLength_ValueChanged;
		cbprExpirWarning.AutoSize = true;
		cbprExpirWarning.Location = new Point(10, 69);
		cbprExpirWarning.Name = "cbprExpirWarning";
		cbprExpirWarning.Size = new Size(96, 16);
		cbprExpirWarning.TabIndex = 0;
		cbprExpirWarning.Text = "lclCheckBox1";
		cbprExpirWarning.UseVisualStyleBackColor = true;
		cbprExpirWarning.Click += cbprMinLength_Click;
		cbprLifeTime.AutoSize = true;
		cbprLifeTime.Location = new Point(10, 45);
		cbprLifeTime.Name = "cbprLifeTime";
		cbprLifeTime.Size = new Size(96, 16);
		cbprLifeTime.TabIndex = 0;
		cbprLifeTime.Text = "lclCheckBox1";
		cbprLifeTime.UseVisualStyleBackColor = true;
		cbprLifeTime.Click += cbprMinLength_Click;
		cbprMinLength.AutoSize = true;
		cbprMinLength.Location = new Point(10, 21);
		cbprMinLength.Name = "cbprMinLength";
		cbprMinLength.Size = new Size(96, 16);
		cbprMinLength.TabIndex = 0;
		cbprMinLength.Text = "lclCheckBox1";
		cbprMinLength.UseVisualStyleBackColor = true;
		cbprMinLength.Click += cbprMinLength_Click;
		gbUser.Controls.Add(lburDescription);
		gbUser.Controls.Add(tburUserName);
		gbUser.Controls.Add(lburUserName);
		gbUser.Controls.Add(tburDescription);
		gbUser.Location = new Point(304, 128);
		gbUser.Name = "gbUser";
		gbUser.Size = new Size(275, 69);
		gbUser.TabIndex = 14;
		gbUser.TabStop = false;
		gbUser.Text = "lclGroupBox1";
		gbUserInfo.Controls.Add(btnuiChangePassword);
		gbUserInfo.Controls.Add(lbuiDateCT);
		gbUserInfo.Controls.Add(lbuiPasswordState);
		gbUserInfo.Controls.Add(lbuiLastLoginV);
		gbUserInfo.Controls.Add(lbuiLastLogin);
		gbUserInfo.Controls.Add(lbuiPasswordSetTimeV);
		gbUserInfo.Controls.Add(lbuiPasswordSetTime);
		gbUserInfo.Controls.Add(lbuiCreateDT);
		gbUserInfo.Controls.Add(lbuiPasswordStateV);
		gbUserInfo.Location = new Point(7, 128);
		gbUserInfo.Name = "gbUserInfo";
		gbUserInfo.Size = new Size(285, 91);
		gbUserInfo.TabIndex = 14;
		gbUserInfo.TabStop = false;
		gbUserInfo.Text = "lclGroupBox1";
		lbuiDateCT.AutoSize = true;
		lbuiDateCT.Location = new Point(14, 19);
		lbuiDateCT.Name = "lbuiDateCT";
		lbuiDateCT.Size = new Size(53, 12);
		lbuiDateCT.TabIndex = 12;
		lbuiDateCT.Text = "创建日期";
		lbuiLastLoginV.AutoSize = true;
		lbuiLastLoginV.Location = new Point(92, 72);
		lbuiLastLoginV.Name = "lbuiLastLoginV";
		lbuiLastLoginV.Size = new Size(59, 12);
		lbuiLastLoginV.TabIndex = 12;
		lbuiLastLoginV.Text = "lclLabel1";
		lbuiLastLogin.AutoSize = true;
		lbuiLastLogin.Location = new Point(14, 72);
		lbuiLastLogin.Name = "lbuiLastLogin";
		lbuiLastLogin.Size = new Size(59, 12);
		lbuiLastLogin.TabIndex = 12;
		lbuiLastLogin.Text = "lclLabel1";
		lbuiCreateDT.AutoSize = true;
		lbuiCreateDT.Location = new Point(92, 19);
		lbuiCreateDT.Name = "lbuiCreateDT";
		lbuiCreateDT.Size = new Size(59, 12);
		lbuiCreateDT.TabIndex = 12;
		lbuiCreateDT.Text = "lclLabel1";
		gbUserAccessRights.Controls.Add(cbatInstru4);
		gbUserAccessRights.Controls.Add(cbuarEditCalibration);
		gbUserAccessRights.Controls.Add(cbatInstru3);
		gbUserAccessRights.Controls.Add(cbuarOpenATSettings);
		gbUserAccessRights.Controls.Add(cbatInstru2);
		gbUserAccessRights.Controls.Add(cbuarEditChromatogram);
		gbUserAccessRights.Controls.Add(cbatInstru1);
		gbUserAccessRights.Controls.Add(cbuarSelectMethod);
		gbUserAccessRights.Controls.Add(cbuarEditReportStyle);
		gbUserAccessRights.Controls.Add(cbuarEditMethod);
		gbUserAccessRights.Controls.Add(cbuarEditSequence);
		gbUserAccessRights.Controls.Add(cbuarOpenConfiguration);
		gbUserAccessRights.Controls.Add(cbuarOpenUserAccounts);
		gbUserAccessRights.Location = new Point(304, 203);
		gbUserAccessRights.Name = "gbUserAccessRights";
		gbUserAccessRights.Size = new Size(275, 167);
		gbUserAccessRights.TabIndex = 14;
		gbUserAccessRights.TabStop = false;
		gbUserAccessRights.Text = "lclGroupBox1";
		cbatInstru4.AutoSize = true;
		cbatInstru4.Location = new Point(145, 146);
		cbatInstru4.Name = "cbatInstru4";
		cbatInstru4.Size = new Size(96, 16);
		cbatInstru4.TabIndex = 0;
		cbatInstru4.Text = "lclCheckBox1";
		cbatInstru4.UseVisualStyleBackColor = true;
		cbatInstru4.Click += cbatInstru1_Click;
		cbuarEditCalibration.AutoSize = true;
		cbuarEditCalibration.Location = new Point(12, 104);
		cbuarEditCalibration.Name = "cbuarEditCalibration";
		cbuarEditCalibration.Size = new Size(96, 16);
		cbuarEditCalibration.TabIndex = 0;
		cbuarEditCalibration.Text = "lclCheckBox1";
		cbuarEditCalibration.UseVisualStyleBackColor = true;
		cbuarEditCalibration.Click += cbuarOpenUserAccounts_Click;
		cbatInstru3.AutoSize = true;
		cbatInstru3.Location = new Point(12, 146);
		cbatInstru3.Name = "cbatInstru3";
		cbatInstru3.Size = new Size(96, 16);
		cbatInstru3.TabIndex = 0;
		cbatInstru3.Text = "lclCheckBox1";
		cbatInstru3.UseVisualStyleBackColor = true;
		cbatInstru3.Click += cbatInstru1_Click;
		cbuarOpenATSettings.AutoSize = true;
		cbuarOpenATSettings.Location = new Point(147, 83);
		cbuarOpenATSettings.Name = "cbuarOpenATSettings";
		cbuarOpenATSettings.Size = new Size(96, 16);
		cbuarOpenATSettings.TabIndex = 0;
		cbuarOpenATSettings.Text = "lclCheckBox1";
		cbuarOpenATSettings.UseVisualStyleBackColor = true;
		cbuarOpenATSettings.Click += cbuarOpenUserAccounts_Click;
		cbatInstru2.AutoSize = true;
		cbatInstru2.Location = new Point(145, 125);
		cbatInstru2.Name = "cbatInstru2";
		cbatInstru2.Size = new Size(96, 16);
		cbatInstru2.TabIndex = 0;
		cbatInstru2.Text = "lclCheckBox1";
		cbatInstru2.UseVisualStyleBackColor = true;
		cbatInstru2.Click += cbatInstru1_Click;
		cbuarEditChromatogram.AutoSize = true;
		cbuarEditChromatogram.Location = new Point(12, 83);
		cbuarEditChromatogram.Name = "cbuarEditChromatogram";
		cbuarEditChromatogram.Size = new Size(96, 16);
		cbuarEditChromatogram.TabIndex = 0;
		cbuarEditChromatogram.Text = "lclCheckBox1";
		cbuarEditChromatogram.UseVisualStyleBackColor = true;
		cbuarEditChromatogram.Click += cbuarOpenUserAccounts_Click;
		cbatInstru1.AutoSize = true;
		cbatInstru1.Location = new Point(12, 125);
		cbatInstru1.Name = "cbatInstru1";
		cbatInstru1.Size = new Size(96, 16);
		cbatInstru1.TabIndex = 0;
		cbatInstru1.Text = "lclCheckBox1";
		cbatInstru1.UseVisualStyleBackColor = true;
		cbatInstru1.Click += cbatInstru1_Click;
		cbuarSelectMethod.AutoSize = true;
		cbuarSelectMethod.Enabled = false;
		cbuarSelectMethod.Location = new Point(147, 62);
		cbuarSelectMethod.Name = "cbuarSelectMethod";
		cbuarSelectMethod.Size = new Size(96, 16);
		cbuarSelectMethod.TabIndex = 0;
		cbuarSelectMethod.Text = "lclCheckBox1";
		cbuarSelectMethod.UseVisualStyleBackColor = true;
		cbuarSelectMethod.Click += cbuarOpenUserAccounts_Click;
		cbuarEditReportStyle.AutoSize = true;
		cbuarEditReportStyle.Location = new Point(147, 41);
		cbuarEditReportStyle.Name = "cbuarEditReportStyle";
		cbuarEditReportStyle.Size = new Size(96, 16);
		cbuarEditReportStyle.TabIndex = 0;
		cbuarEditReportStyle.Text = "lclCheckBox1";
		cbuarEditReportStyle.UseVisualStyleBackColor = true;
		cbuarEditReportStyle.Click += cbuarOpenUserAccounts_Click;
		cbuarEditMethod.AutoSize = true;
		cbuarEditMethod.Location = new Point(12, 62);
		cbuarEditMethod.Name = "cbuarEditMethod";
		cbuarEditMethod.Size = new Size(96, 16);
		cbuarEditMethod.TabIndex = 0;
		cbuarEditMethod.Text = "lclCheckBox1";
		cbuarEditMethod.UseVisualStyleBackColor = true;
		cbuarEditMethod.Click += cbuarOpenUserAccounts_Click;
		cbuarEditSequence.AutoSize = true;
		cbuarEditSequence.Location = new Point(147, 20);
		cbuarEditSequence.Name = "cbuarEditSequence";
		cbuarEditSequence.Size = new Size(96, 16);
		cbuarEditSequence.TabIndex = 0;
		cbuarEditSequence.Text = "lclCheckBox1";
		cbuarEditSequence.UseVisualStyleBackColor = true;
		cbuarEditSequence.Click += cbuarOpenUserAccounts_Click;
		cbuarOpenConfiguration.AutoSize = true;
		cbuarOpenConfiguration.Location = new Point(12, 41);
		cbuarOpenConfiguration.Name = "cbuarOpenConfiguration";
		cbuarOpenConfiguration.Size = new Size(96, 16);
		cbuarOpenConfiguration.TabIndex = 0;
		cbuarOpenConfiguration.Text = "lclCheckBox1";
		cbuarOpenConfiguration.UseVisualStyleBackColor = true;
		cbuarOpenConfiguration.Click += cbuarOpenUserAccounts_Click;
		cbuarOpenUserAccounts.AutoSize = true;
		cbuarOpenUserAccounts.Location = new Point(12, 20);
		cbuarOpenUserAccounts.Name = "cbuarOpenUserAccounts";
		cbuarOpenUserAccounts.Size = new Size(96, 16);
		cbuarOpenUserAccounts.TabIndex = 0;
		cbuarOpenUserAccounts.Text = "lclCheckBox1";
		cbuarOpenUserAccounts.UseVisualStyleBackColor = true;
		cbuarOpenUserAccounts.Click += cbuarOpenUserAccounts_Click;
		gbPerInfo.Controls.Add(pbSignGraph);
		gbPerInfo.Controls.Add(lbSign);
		gbPerInfo.Controls.Add(btnSgSizeMode);
		gbPerInfo.Controls.Add(tbPersonInfo);
		gbPerInfo.Location = new Point(7, 225);
		gbPerInfo.Name = "gbPerInfo";
		gbPerInfo.Size = new Size(291, 145);
		gbPerInfo.TabIndex = 14;
		gbPerInfo.TabStop = false;
		gbPerInfo.Text = "个人资料";
		pbSignGraph.BorderStyle = BorderStyle.FixedSingle;
		pbSignGraph.Location = new Point(45, 104);
		pbSignGraph.Name = "pbSignGraph";
		pbSignGraph.Size = new Size(159, 35);
		pbSignGraph.SizeMode = PictureBoxSizeMode.StretchImage;
		pbSignGraph.TabIndex = 13;
		pbSignGraph.TabStop = false;
		pbSignGraph.Click += pbSignGraph_Click;
		lbSign.AutoSize = true;
		lbSign.Location = new Point(6, 106);
		lbSign.Name = "lbSign";
		lbSign.Size = new Size(29, 12);
		lbSign.TabIndex = 12;
		lbSign.Text = "签名";
		btnSgSizeMode.Location = new Point(210, 103);
		btnSgSizeMode.Name = "btnSgSizeMode";
		btnSgSizeMode.Size = new Size(70, 20);
		btnSgSizeMode.TabIndex = 12;
		btnSgSizeMode.Text = "图像模式";
		btnSgSizeMode.UseVisualStyleBackColor = true;
		btnSgSizeMode.Click += btnSgSizeMode_Click;
		tbPersonInfo.Location = new Point(6, 14);
		tbPersonInfo.Multiline = true;
		tbPersonInfo.Name = "tbPersonInfo";
		tbPersonInfo.ScrollBars = ScrollBars.Both;
		tbPersonInfo.Size = new Size(281, 86);
		tbPersonInfo.TabIndex = 3;
		tbPersonInfo.TextChanged += tbPersonInfo_TextChanged;
		base.AutoScaleDimensions = new SizeF(6f, 12f);
		base.ClientSize = new Size(585, 410);
		base.Controls.Add(gbPasswordRestricts);
		base.Controls.Add(gbUser);
		base.Controls.Add(gbUserInfo);
		base.Controls.Add(gbPerInfo);
		base.Controls.Add(gbUserAccessRights);
		base.Controls.Add(gbUserList);
		base.Controls.Add(lbUserDetails);
		base.Name = "UserAccountsDlg";
		base.Load += UserAccountsDlg_Load;
		base.KeyDown += UserAccountsDlg_KeyDown;
		base.Controls.SetChildIndex(btnCancel, 0);
		base.Controls.SetChildIndex(btnOK, 0);
		base.Controls.SetChildIndex(btnHelp, 0);
		base.Controls.SetChildIndex(lbUserDetails, 0);
		base.Controls.SetChildIndex(gbUserList, 0);
		base.Controls.SetChildIndex(gbUserAccessRights, 0);
		base.Controls.SetChildIndex(gbPerInfo, 0);
		base.Controls.SetChildIndex(gbUserInfo, 0);
		base.Controls.SetChildIndex(gbUser, 0);
		base.Controls.SetChildIndex(gbPasswordRestricts, 0);
		((ISupportInitialize)gvulNames).EndInit();
		gbUserList.ResumeLayout(performLayout: false);
		gbPasswordRestricts.ResumeLayout(performLayout: false);
		gbPasswordRestricts.PerformLayout();
		nudprExpirWarning.EndInit();
		nudprLifeTime.EndInit();
		nudprMinLength.EndInit();
		gbUser.ResumeLayout(performLayout: false);
		gbUser.PerformLayout();
		gbUserInfo.ResumeLayout(performLayout: false);
		gbUserInfo.PerformLayout();
		gbUserAccessRights.ResumeLayout(performLayout: false);
		gbUserAccessRights.PerformLayout();
		gbPerInfo.ResumeLayout(performLayout: false);
		gbPerInfo.PerformLayout();
		((ISupportInitialize)pbSignGraph).EndInit();
		ResumeLayout(performLayout: false);
		PerformLayout();
	}

	public override void LoadLanguage()
	{
		base.LoadLanguage();
		switch (Class49.sysLanguage_0)
		{
		case SysLanguage.CN:
			Text = "用户帐户";
			gbUserList.Text = "用户列表";
			btnulNewUser.Text = "新建";
			btnulDeleteUser.Text = "删除";
			gbPasswordRestricts.Text = "密码规则 - [全体有效]";
			cbprMinLength.Text = "最小长度";
			lbprMinLength.Text = "[字符]";
			cbprLifeTime.Text = "有效期";
			lbprLifeTime.Text = "[天]";
			cbprExpirWarning.Text = "提前警告";
			lbprExpirWarning.Text = "[天]";
			lbUserDetails.Text = "用户明细";
			gbUser.Text = "用户";
			lburUserName.Text = "用户名";
			lburDescription.Text = "描述";
			gbUserInfo.Text = "用户信息";
			lbuiPasswordState.Text = "密码状态：";
			lbuiPasswordSetTime.Text = "最后修改：";
			lbuiLastLogin.Text = "最后登录：";
			btnuiChangePassword.Text = "修改密码";
			cbatInstru1.Text = "仪器1";
			cbatInstru2.Text = "仪器2";
			cbatInstru3.Text = "仪器3";
			cbatInstru4.Text = "仪器4";
			gbUserAccessRights.Text = "访问权限";
			cbuarOpenUserAccounts.Text = "打开用户帐户";
			cbuarOpenConfiguration.Text = "打开配置";
			cbuarEditMethod.Text = "编辑方法";
			cbuarEditChromatogram.Text = "编辑谱图";
			cbuarEditCalibration.Text = "编辑校正";
			cbuarEditSequence.Text = "编辑队列";
			cbuarEditReportStyle.Text = "编辑报告规则";
			cbuarSelectMethod.Text = "选择方法";
			cbuarOpenATSettings.Text = "打开日志设置";
			break;
		case SysLanguage.EN:
			Text = "User Accounts";
			gbUserList.Text = "User List";
			btnulNewUser.Text = "New";
			btnulDeleteUser.Text = "Delete";
			gbPasswordRestricts.Text = "Password Restrictions - Common for all";
			cbprMinLength.Text = "Min Length";
			lbprMinLength.Text = "[chars]";
			cbprLifeTime.Text = "Life Time";
			lbprLifeTime.Text = "[days]";
			cbprExpirWarning.Text = "Expiration Warning";
			lbprExpirWarning.Text = "[days]";
			lbUserDetails.Text = "User Details";
			gbUser.Text = "User";
			lburUserName.Text = "User Name";
			lburDescription.Text = "Description";
			gbUserInfo.Text = "User Info";
			lbuiPasswordState.Text = "Pwd State:";
			lbuiPasswordSetTime.Text = "Set Time:";
			lbuiLastLogin.Text = "LastLogin:";
			btnuiChangePassword.Text = "Change Password";
			cbatInstru1.Text = "Instrument1";
			cbatInstru2.Text = "Instrument2";
			cbatInstru3.Text = "Instrument3";
			cbatInstru4.Text = "Instrument4";
			gbUserAccessRights.Text = "User Access Rights";
			cbuarOpenUserAccounts.Text = "Open User Accounts";
			cbuarOpenConfiguration.Text = "Open Configuration";
			cbuarEditMethod.Text = "Edit Method";
			cbuarEditChromatogram.Text = "Edit Chromatogram";
			cbuarEditCalibration.Text = "Edit Calibration";
			cbuarEditSequence.Text = "Edit Sequence";
			cbuarEditReportStyle.Text = "Edit ReportStyle";
			cbuarSelectMethod.Text = "Select Method";
			cbuarOpenATSettings.Text = "Open Audit Trail Settings";
			break;
		}
	}

	private void nudprMinLength_ValueChanged(object sender, EventArgs e)
	{
		NumericUpDown numericUpDown = sender as NumericUpDown;
		if (numericUpDown == nudprMinLength)
		{
			userAccounts_0.minLength = (int)numericUpDown.Value;
		}
		else if (numericUpDown == nudprLifeTime)
		{
			userAccounts_0.lifeTime = (int)numericUpDown.Value;
		}
		else if (numericUpDown == nudprExpirWarning)
		{
			userAccounts_0.expirationWarning = (int)numericUpDown.Value;
		}
	}

	private void pbSignGraph_Click(object sender, EventArgs e)
	{
		if (openFileDialog_0 == null)
		{
			openFileDialog_0 = new OpenFileDialog();
			openFileDialog_0.Filter = Class49.MakeFileFilter(".jpg");
			openFileDialog_0.Multiselect = false;
		}
		if (openFileDialog_0.ShowDialog() == DialogResult.OK)
		{
			user_0.signGraph = openFileDialog_0.FileName;
			pbSignGraph.Image = Image.FromFile(user_0.signGraph);
			pbSignGraph.SizeMode = user_0.sgSizeMode;
		}
	}

	private void method_1()
	{
		gvulNames.RowCount = 0;
		for (int i = 0; i < userAccounts_0.users.Length; i++)
		{
			int index = gvulNames.Rows.Add();
			gvulNames.Rows[index].Cells[0].Value = userAccounts_0.users[i].u_name;
		}
		gvulNames_SelectionChanged(null, null);
	}

	public new void ShowDialog()
	{
		if (Class49.loginDlg_0.ShowDialog(AccessType.OpenUserAccounts))
		{
			base.ShowDialog();
		}
	}

	private void tbPersonInfo_TextChanged(object sender, EventArgs e)
	{
		if (user_0 != null)
		{
			TextBox textBox = sender as TextBox;
			if (textBox == tburUserName)
			{
				user_0.u_name = textBox.Text;
				gvulNames.SelectedRows[0].Cells[0].Value = textBox.Text;
			}
			else if (textBox == tburDescription)
			{
				user_0.u_description = textBox.Text;
			}
			else if (textBox == tbPersonInfo)
			{
				user_0.personInfo = textBox.Text;
			}
		}
	}

	private void UserAccountsDlg_Load(object sender, EventArgs e)
	{
		userAccounts_0.LoadFromObject(userAccounts);
		method_1();
		chgPswdDlg_0 = new ChgPswdDlg();
		cbprMinLength.Checked = userAccounts_0.useMinLength;
		cbprLifeTime.Checked = userAccounts_0.useLifeTime;
		cbprExpirWarning.Checked = userAccounts_0.useExpirWarning;
		nudprMinLength.Value = userAccounts_0.minLength;
		nudprLifeTime.Value = userAccounts_0.lifeTime;
		nudprExpirWarning.Value = userAccounts_0.expirationWarning;
	}

	private void UserAccountsDlg_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode == Keys.F1)
		{
			Class49.smethod_32("用户账户");
		}
	}
}
