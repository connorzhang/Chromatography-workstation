using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using IBrainChrom2018.Unit;

namespace IBrainChrom2018;

public class RegisterForm : Form
{
	private FormMainParam frmParam = FormMainParam.Create();

	private IContainer components = null;

	private TextBox tbCode;

	private TextBox tbRegister;

	private Button btnRegister;

	private Button btnCheck;

	private Button button1;

	private Button btnDeRegister;

	public RegisterForm()
	{
		InitializeComponent();
		initForm();
	}

	public void initForm()
	{
		tbCode.Text = RegisterClass.CreateCode();
		frmParam.strCPU = RegisterClass.getCpu() + RegisterClass.GetDiskVolumeSerialNumber();
		frmParam.SaveParam();
		RegisterClass.setIntCodeF();
	}

	private void btnRegister_Click(object sender, EventArgs e)
	{
		string code = RegisterClass.GetCode(tbCode.Text.Trim());
		if (tbRegister.Text.Trim() == code)
		{
			if (RegisterClass.RegistIt(tbRegister.Text.Trim(), code))
			{
				MessageBox.Show("激活成功!");
				frmParam.bBen = true;
				if (tabRltCtrl.selfCtrl != null)
				{
					tabRltCtrl.selfCtrl.panBen.Visible = true;
				}
			}
			else
			{
				MessageBox.Show("激活失败!");
				frmParam.bBen = false;
			}
		}
		else
		{
			MessageBox.Show("激活失败!");
		}
	}

	private void btnCheck_Click(object sender, EventArgs e)
	{
		if (RegisterClass.BoolRegist(frmParam.strCPU))
		{
			MessageBox.Show("已经激活成功!");
		}
		else
		{
			MessageBox.Show("没有激活!");
		}
	}

	private void button1_Click(object sender, EventArgs e)
	{
		tbRegister.Text = RegisterClass.GetCode(tbCode.Text.Trim());
	}

	private void btnDeRegister_Click(object sender, EventArgs e)
	{
		if (RegisterClass.DeRegistIt())
		{
			frmParam.bBen = false;
			if (tabRltCtrl.selfCtrl != null)
			{
				tabRltCtrl.selfCtrl.panBen.Visible = false;
			}
			MessageBox.Show("反激活完成!");
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(IBrainChrom2018.RegisterForm));
		this.tbCode = new System.Windows.Forms.TextBox();
		this.tbRegister = new System.Windows.Forms.TextBox();
		this.btnRegister = new System.Windows.Forms.Button();
		this.btnCheck = new System.Windows.Forms.Button();
		this.button1 = new System.Windows.Forms.Button();
		this.btnDeRegister = new System.Windows.Forms.Button();
		base.SuspendLayout();
		this.tbCode.Location = new System.Drawing.Point(84, 61);
		this.tbCode.Name = "tbCode";
		this.tbCode.Size = new System.Drawing.Size(296, 21);
		this.tbCode.TabIndex = 0;
		this.tbRegister.Location = new System.Drawing.Point(84, 110);
		this.tbRegister.Name = "tbRegister";
		this.tbRegister.Size = new System.Drawing.Size(296, 21);
		this.tbRegister.TabIndex = 1;
		this.btnRegister.Location = new System.Drawing.Point(93, 178);
		this.btnRegister.Name = "btnRegister";
		this.btnRegister.Size = new System.Drawing.Size(75, 23);
		this.btnRegister.TabIndex = 2;
		this.btnRegister.Text = "激活";
		this.btnRegister.UseVisualStyleBackColor = true;
		this.btnRegister.Click += new System.EventHandler(btnRegister_Click);
		this.btnCheck.Location = new System.Drawing.Point(305, 178);
		this.btnCheck.Name = "btnCheck";
		this.btnCheck.Size = new System.Drawing.Size(75, 23);
		this.btnCheck.TabIndex = 3;
		this.btnCheck.Text = "查看状态";
		this.btnCheck.UseVisualStyleBackColor = true;
		this.btnCheck.Click += new System.EventHandler(btnCheck_Click);
		this.button1.Location = new System.Drawing.Point(198, 178);
		this.button1.Name = "button1";
		this.button1.Size = new System.Drawing.Size(75, 23);
		this.button1.TabIndex = 4;
		this.button1.Text = "button1";
		this.button1.UseVisualStyleBackColor = true;
		this.button1.Visible = false;
		this.button1.Click += new System.EventHandler(button1_Click);
		this.btnDeRegister.Location = new System.Drawing.Point(93, 221);
		this.btnDeRegister.Name = "btnDeRegister";
		this.btnDeRegister.Size = new System.Drawing.Size(75, 23);
		this.btnDeRegister.TabIndex = 5;
		this.btnDeRegister.Text = "反激活";
		this.btnDeRegister.UseVisualStyleBackColor = true;
		this.btnDeRegister.Click += new System.EventHandler(btnDeRegister_Click);
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(509, 284);
		base.Controls.Add(this.btnDeRegister);
		base.Controls.Add(this.button1);
		base.Controls.Add(this.btnCheck);
		base.Controls.Add(this.btnRegister);
		base.Controls.Add(this.tbRegister);
		base.Controls.Add(this.tbCode);
		base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
		base.Name = "RegisterForm";
		this.Text = "RegisterForm";
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
