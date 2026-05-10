using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace IBrainChrom2018;

public class ChgPswdDlg : LclDialog
{
	private const string string_0 = "密码与确认不一致，请重新输入！";

	private const string string_1 = "确认新密码";

	private const string string_2 = "输入新密码";

	private const string string_3 = "新设密码";

	private const string string_4 = "Password and confirmation do not match,try again!";

	private const string string_5 = "Confirm(retype)";

	private const string string_6 = "Enter New";

	private const string string_7 = "New Password";

	private IContainer icontainer_1;

	private LclLabel lbConfirm;

	private LclLabel lbEnterNew;

	public string newPassword = "";

	private LclTextBox tbConfirm;

	private LclTextBox tbEnterNew;

	public ChgPswdDlg()
	{
		InitializeComponent_1();
	}

	private void method_0(object sender, EventArgs e)
	{
		if (tbEnterNew.Text.Equals(tbConfirm.Text))
		{
			if (UserAccountsDlg.userAccounts.useMinLength && tbConfirm.Text.Length < UserAccountsDlg.userAccounts.minLength)
			{
				MessageBox.Show("密码长度不足!");
				return;
			}
			newPassword = tbEnterNew.Text;
			base.DialogResult = DialogResult.OK;
			return;
		}
		switch (Class49.sysLanguage_0)
		{
		case SysLanguage.CN:
			MessageBox.Show("密码与确认不一致，请重新输入！");
			break;
		case SysLanguage.EN:
			MessageBox.Show("Password and confirmation do not match,try again!");
			break;
		}
	}

	private void ChgPswdDlg_Load(object sender, EventArgs e)
	{
		tbEnterNew.Text = "";
		tbConfirm.Text = "";
	}

	protected override void Dispose(bool disposing)
	{
		if (disposing && icontainer_1 != null)
		{
			icontainer_1.Dispose();
		}
		base.Dispose(disposing);
	}

	private void InitializeComponent_1()
	{
		tbEnterNew = new LclTextBox();
		tbConfirm = new LclTextBox();
		lbEnterNew = new LclLabel();
		lbConfirm = new LclLabel();
		SuspendLayout();
		btnOK.Location = new Point(29, 111);
		btnOK.Text = "确认";
		btnOK.Click += method_0;
		btnCancel.Location = new Point(151, 111);
		btnCancel.Text = "取消";
		btnHelp.Location = new Point(151, 4);
		btnHelp.Text = "帮助";
		btnHelp.Visible = false;
		tbEnterNew.Name = "tbEnterNew";
		tbEnterNew.Size = new Size(229, 21);
		tbEnterNew.TabIndex = 2;
		tbConfirm.Location = new Point(14, 72);
		tbConfirm.Name = "tbConfirm";
		tbConfirm.Size = new Size(229, 21);
		tbConfirm.TabIndex = 2;
		lbEnterNew.AutoSize = true;
		lbEnterNew.Location = new Point(12, 18);
		lbEnterNew.Name = "lbEnterNew";
		lbEnterNew.Size = new Size(59, 12);
		lbEnterNew.TabIndex = 3;
		lbEnterNew.Text = "lclLabel1";
		lbConfirm.AutoSize = true;
		lbConfirm.Location = new Point(12, 57);
		lbConfirm.Name = "lbConfirm";
		lbConfirm.Size = new Size(59, 12);
		lbConfirm.TabIndex = 3;
		lbConfirm.Text = "lclLabel1";
		base.AutoScaleDimensions = new SizeF(6f, 12f);
		base.ClientSize = new Size(254, 142);
		base.Controls.Add(tbEnterNew);
		base.Controls.Add(lbEnterNew);
		base.Controls.Add(lbConfirm);
		base.Controls.Add(tbConfirm);
		base.Name = "ChgPswdDlg";
		base.Load += ChgPswdDlg_Load;
		base.Controls.SetChildIndex(tbConfirm, 0);
		base.Controls.SetChildIndex(btnOK, 0);
		base.Controls.SetChildIndex(btnCancel, 0);
		base.Controls.SetChildIndex(lbConfirm, 0);
		base.Controls.SetChildIndex(lbEnterNew, 0);
		base.Controls.SetChildIndex(btnHelp, 0);
		base.Controls.SetChildIndex(tbEnterNew, 0);
		ResumeLayout(performLayout: false);
		PerformLayout();
	}

	public override void LoadLanguage()
	{
		base.LoadLanguage();
		switch (Class49.sysLanguage_0)
		{
		case SysLanguage.CN:
			Text = "新设密码";
			lbEnterNew.Text = "输入新密码";
			lbConfirm.Text = "确认新密码";
			break;
		case SysLanguage.EN:
			Text = "New Password";
			lbEnterNew.Text = "Enter New";
			lbConfirm.Text = "Confirm(retype)";
			break;
		}
	}
}
