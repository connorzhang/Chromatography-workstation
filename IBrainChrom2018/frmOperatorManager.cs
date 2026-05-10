using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace IBrainChrom2018;

public class frmOperatorManager : Form
{
	private IContainer components;

	private Button btnSubmit;

	private Button btnCancel;

	private ToolTip toolTip;

	private GroupBox gbOperatorInfo;

	private Label lblValidatePwd;

	private Label lblOperatorPwd;

	private Label lblOperatorName;

	private Label label1;

	private Label label2;

	private Label label3;

	public TextBox txtValidatePwd;

	public TextBox txtOperatorPwd;

	public TextBox txtOperatorName;

	public ComboBox cright;

	public DateTimePicker dateTimePicker1;

	private bool _isModify;

	public frmRightsManager.User AddUser;

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
		this.components = new System.ComponentModel.Container();
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(IBrainChrom2018.frmOperatorManager));
		this.btnSubmit = new System.Windows.Forms.Button();
		this.btnCancel = new System.Windows.Forms.Button();
		this.toolTip = new System.Windows.Forms.ToolTip(this.components);
		this.gbOperatorInfo = new System.Windows.Forms.GroupBox();
		this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
		this.label2 = new System.Windows.Forms.Label();
		this.cright = new System.Windows.Forms.ComboBox();
		this.label3 = new System.Windows.Forms.Label();
		this.label1 = new System.Windows.Forms.Label();
		this.txtValidatePwd = new System.Windows.Forms.TextBox();
		this.txtOperatorPwd = new System.Windows.Forms.TextBox();
		this.lblValidatePwd = new System.Windows.Forms.Label();
		this.txtOperatorName = new System.Windows.Forms.TextBox();
		this.lblOperatorPwd = new System.Windows.Forms.Label();
		this.lblOperatorName = new System.Windows.Forms.Label();
		this.gbOperatorInfo.SuspendLayout();
		base.SuspendLayout();
		this.btnSubmit.Location = new System.Drawing.Point(155, 269);
		this.btnSubmit.Name = "btnSubmit";
		this.btnSubmit.Size = new System.Drawing.Size(60, 21);
		this.btnSubmit.TabIndex = 2;
		this.btnSubmit.Text = "确认";
		this.btnSubmit.UseVisualStyleBackColor = true;
		this.btnSubmit.Click += new System.EventHandler(btnSubmit_Click);
		this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
		this.btnCancel.Location = new System.Drawing.Point(236, 269);
		this.btnCancel.Name = "btnCancel";
		this.btnCancel.Size = new System.Drawing.Size(60, 21);
		this.btnCancel.TabIndex = 3;
		this.btnCancel.Text = "取消";
		this.btnCancel.UseVisualStyleBackColor = true;
		this.btnCancel.Click += new System.EventHandler(btnCancel_Click);
		this.toolTip.ShowAlways = true;
		this.gbOperatorInfo.Controls.Add(this.dateTimePicker1);
		this.gbOperatorInfo.Controls.Add(this.label2);
		this.gbOperatorInfo.Controls.Add(this.cright);
		this.gbOperatorInfo.Controls.Add(this.label3);
		this.gbOperatorInfo.Controls.Add(this.label1);
		this.gbOperatorInfo.Controls.Add(this.txtValidatePwd);
		this.gbOperatorInfo.Controls.Add(this.txtOperatorPwd);
		this.gbOperatorInfo.Controls.Add(this.lblValidatePwd);
		this.gbOperatorInfo.Controls.Add(this.txtOperatorName);
		this.gbOperatorInfo.Controls.Add(this.lblOperatorPwd);
		this.gbOperatorInfo.Controls.Add(this.lblOperatorName);
		this.gbOperatorInfo.Location = new System.Drawing.Point(12, 12);
		this.gbOperatorInfo.Name = "gbOperatorInfo";
		this.gbOperatorInfo.Size = new System.Drawing.Size(294, 251);
		this.gbOperatorInfo.TabIndex = 0;
		this.gbOperatorInfo.TabStop = false;
		this.gbOperatorInfo.Text = "用户信息";
		this.dateTimePicker1.Location = new System.Drawing.Point(108, 111);
		this.dateTimePicker1.Name = "dateTimePicker1";
		this.dateTimePicker1.Size = new System.Drawing.Size(135, 21);
		this.dateTimePicker1.TabIndex = 9;
		this.label2.AutoSize = true;
		this.label2.ForeColor = System.Drawing.Color.Blue;
		this.label2.Location = new System.Drawing.Point(25, 165);
		this.label2.Name = "label2";
		this.label2.Size = new System.Drawing.Size(29, 12);
		this.label2.TabIndex = 8;
		this.label2.Text = "****";
		this.cright.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.cright.FormattingEnabled = true;
		this.cright.Items.AddRange(new object[4] { "管理员", "分析员", "检验员", "访问员" });
		this.cright.Location = new System.Drawing.Point(108, 138);
		this.cright.Name = "cright";
		this.cright.Size = new System.Drawing.Size(135, 20);
		this.cright.TabIndex = 7;
		this.cright.SelectedIndexChanged += new System.EventHandler(cright_SelectedIndexChanged);
		this.label3.AutoSize = true;
		this.label3.Location = new System.Drawing.Point(25, 116);
		this.label3.Name = "label3";
		this.label3.Size = new System.Drawing.Size(77, 12);
		this.label3.TabIndex = 6;
		this.label3.Text = "有 效 期(&D):";
		this.label1.AutoSize = true;
		this.label1.Location = new System.Drawing.Point(25, 141);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(77, 12);
		this.label1.TabIndex = 6;
		this.label1.Text = "用户级别(&L):";
		this.txtValidatePwd.Location = new System.Drawing.Point(108, 81);
		this.txtValidatePwd.Name = "txtValidatePwd";
		this.txtValidatePwd.PasswordChar = '*';
		this.txtValidatePwd.Size = new System.Drawing.Size(135, 21);
		this.txtValidatePwd.TabIndex = 5;
		this.txtOperatorPwd.Location = new System.Drawing.Point(108, 51);
		this.txtOperatorPwd.Name = "txtOperatorPwd";
		this.txtOperatorPwd.PasswordChar = '*';
		this.txtOperatorPwd.Size = new System.Drawing.Size(135, 21);
		this.txtOperatorPwd.TabIndex = 3;
		this.lblValidatePwd.AutoSize = true;
		this.lblValidatePwd.Location = new System.Drawing.Point(25, 84);
		this.lblValidatePwd.Name = "lblValidatePwd";
		this.lblValidatePwd.Size = new System.Drawing.Size(77, 12);
		this.lblValidatePwd.TabIndex = 4;
		this.lblValidatePwd.Text = "确认密码(&P):";
		this.txtOperatorName.Location = new System.Drawing.Point(108, 21);
		this.txtOperatorName.Name = "txtOperatorName";
		this.txtOperatorName.Size = new System.Drawing.Size(135, 21);
		this.txtOperatorName.TabIndex = 1;
		this.lblOperatorPwd.AutoSize = true;
		this.lblOperatorPwd.Location = new System.Drawing.Point(25, 54);
		this.lblOperatorPwd.Name = "lblOperatorPwd";
		this.lblOperatorPwd.Size = new System.Drawing.Size(77, 12);
		this.lblOperatorPwd.TabIndex = 2;
		this.lblOperatorPwd.Text = "用户密码(&P):";
		this.lblOperatorName.AutoSize = true;
		this.lblOperatorName.Location = new System.Drawing.Point(25, 24);
		this.lblOperatorName.Name = "lblOperatorName";
		this.lblOperatorName.Size = new System.Drawing.Size(77, 12);
		this.lblOperatorName.TabIndex = 0;
		this.lblOperatorName.Text = "登录名称(&N):";
		base.AcceptButton = this.btnSubmit;
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.CancelButton = this.btnCancel;
		base.ClientSize = new System.Drawing.Size(308, 302);
		base.Controls.Add(this.btnCancel);
		base.Controls.Add(this.btnSubmit);
		base.Controls.Add(this.gbOperatorInfo);
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
		base.HelpButton = true;
		base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
		base.MaximizeBox = false;
		base.MinimizeBox = false;
		this.MinimumSize = new System.Drawing.Size(300, 200);
		base.Name = "frmOperatorManager";
		base.ShowInTaskbar = false;
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.Text = "添加用户";
		base.Load += new System.EventHandler(frmOperatorManager_Load);
		this.gbOperatorInfo.ResumeLayout(false);
		this.gbOperatorInfo.PerformLayout();
		base.ResumeLayout(false);
	}

	public frmOperatorManager()
	{
		InitializeComponent();
	}

	private bool UserInputCheck()
	{
		string value = txtOperatorName.Text.Trim();
		string text = txtOperatorPwd.Text.Trim();
		string text2 = txtValidatePwd.Text.Trim();
		if (string.IsNullOrEmpty(value))
		{
			toolTip.ToolTipIcon = ToolTipIcon.Info;
			toolTip.ToolTipTitle = ((!_isModify) ? "添加提示" : "修改提示");
			Point point = new Point(txtOperatorName.Location.X + txtOperatorName.Width, txtOperatorName.Location.Y);
			toolTip.Show((!_isModify) ? "请输入登录名称！" : "请输入原始密码！", this, point, 5000);
			txtOperatorName.Focus();
			return false;
		}
		if (text.Length < 6)
		{
			toolTip.ToolTipIcon = ToolTipIcon.Warning;
			toolTip.ToolTipTitle = ((!_isModify) ? "添加警告" : "修改警告");
			Point point2 = new Point(txtOperatorPwd.Location.X + txtOperatorPwd.Width, txtOperatorPwd.Location.Y);
			toolTip.Show("用户密码长度不能小于六位！", this, point2, 5000);
			txtOperatorPwd.Focus();
			return false;
		}
		if (text2.Length < 6)
		{
			toolTip.ToolTipIcon = ToolTipIcon.Warning;
			toolTip.ToolTipTitle = ((!_isModify) ? "添加警告" : "修改警告");
			Point point3 = new Point(txtValidatePwd.Location.X + txtValidatePwd.Width, txtValidatePwd.Location.Y);
			toolTip.Show("确认密码长度不能小于六位！", this, point3, 5000);
			txtValidatePwd.Focus();
			return false;
		}
		if (text != text2)
		{
			toolTip.ToolTipIcon = ToolTipIcon.Warning;
			toolTip.ToolTipTitle = ((!_isModify) ? "添加警告" : "修改警告");
			Point point4 = new Point(txtValidatePwd.Location.X + txtValidatePwd.Width, txtValidatePwd.Location.Y);
			toolTip.Show("两次输入的密码必须一致！", this, point4, 5000);
			txtValidatePwd.Focus();
			return false;
		}
		return true;
	}

	private void btnCancel_Click(object sender, EventArgs e)
	{
		Close();
	}

	private void frmOperatorManager_Load(object sender, EventArgs e)
	{
		cright.SelectedIndex = 1;
		AddUser.UName = "";
		AddUser.Pwd = "";
		AddUser.UserLevel = (frmRightsManager.User.Level)cright.SelectedIndex;
	}

	private void cright_SelectedIndexChanged(object sender, EventArgs e)
	{
		if (cright.SelectedIndex == 0)
		{
			label2.Text = "管理员：新建、删除账号，给账号分别权限，修改仪器配置";
		}
		if (cright.SelectedIndex == 1)
		{
			label2.Text = "分析员：建立方法、修改方法、修改谱图\r\n";
		}
		if (cright.SelectedIndex == 2)
		{
			label2.Text = "检验员：运行仪器，查看、调用方法\r\n";
		}
		if (cright.SelectedIndex == 3)
		{
			label2.Text = "访问员：访问权限\r\n";
		}
	}

	private void btnSubmit_Click(object sender, EventArgs e)
	{
		if (UserInputCheck())
		{
			AddUser.UName = txtOperatorName.Text.Trim();
			AddUser.Pwd = txtOperatorPwd.Text.Trim();
			AddUser.TooltipTime = dateTimePicker1.Value;
			if (AddUser.UName == "admin")
			{
				AddUser.UserLevel = frmRightsManager.User.Level.管理员;
			}
			else
			{
				AddUser.UserLevel = (frmRightsManager.User.Level)cright.SelectedIndex;
			}
			Common.AddLog2Db("系统", Common.LoginUserName, "", "系统用户及权限管理", "添加用户:用户名:" + AddUser.UName + ",角色:" + AddUser.UserLevel);
			Close();
		}
	}
}
