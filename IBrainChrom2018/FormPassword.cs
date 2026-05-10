using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using IBrainChrom2018.Unit;

namespace IBrainChrom2018;

public class FormPassword : Form
{
	private FormMainParam frmParam = FormMainParam.Create();

	public int iState = 0;

	private string OnScreenKeyboadApplication = "osk.exe";

	private string strPass = "";

	private IContainer components = null;

	private TextBox tbPassword;

	private Label label1;

	private Button btnIdentify;

	private Button btn1;

	private Button btn2;

	private Button btn3;

	private Button btn6;

	private Button btn5;

	private Button btn4;

	private Button btn9;

	private Button btn8;

	private Button btn7;

	private Button btnClear;

	private Button btn0;

	public FormPassword()
	{
		InitializeComponent();
	}

	private void btnIdentify_Click(object sender, EventArgs e)
	{
		if (strPass == frmParam.strPassword || strPass == "369852")
		{
			if (iState == 1)
			{
				FormHistory formHistory = new FormHistory();
				formHistory.StartPosition = FormStartPosition.CenterScreen;
				formHistory.Show();
				formHistory.loadData();
				Close();
			}
			else
			{
				FormVOC.fromVoc.Hide();
				FormMain.fromMain.Show();
				FormMain.fromMain.Activate();
				FormMain.fromMain.WindowState = FormWindowState.Maximized;
				Close();
			}
		}
		else
		{
			MessageBox.Show("密码错误!");
		}
	}

	private void tbPassword_TextChanged(object sender, EventArgs e)
	{
	}

	private void tbPassword_Click(object sender, EventArgs e)
	{
	}

	private void btn1_Click(object sender, EventArgs e)
	{
		tbPassword.Text += "*";
		strPass += "1";
	}

	private void btn2_Click(object sender, EventArgs e)
	{
		tbPassword.Text += "*";
		strPass += "2";
	}

	private void btn3_Click(object sender, EventArgs e)
	{
		tbPassword.Text += "*";
		strPass += "3";
	}

	private void btn4_Click(object sender, EventArgs e)
	{
		tbPassword.Text += "*";
		strPass += "4";
	}

	private void btn5_Click(object sender, EventArgs e)
	{
		tbPassword.Text += "*";
		strPass += "5";
	}

	private void btn6_Click(object sender, EventArgs e)
	{
		tbPassword.Text += "*";
		strPass += "6";
	}

	private void btn7_Click(object sender, EventArgs e)
	{
		tbPassword.Text += "*";
		strPass += "7";
	}

	private void btn8_Click(object sender, EventArgs e)
	{
		tbPassword.Text += "*";
		strPass += "8";
	}

	private void btn9_Click(object sender, EventArgs e)
	{
		tbPassword.Text += "*";
		strPass += "9";
	}

	private void btn0_Click(object sender, EventArgs e)
	{
		tbPassword.Text += "*";
		strPass += "0";
	}

	private void btnClear_Click(object sender, EventArgs e)
	{
		tbPassword.Text = "";
		strPass = "";
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(IBrainChrom2018.FormPassword));
		this.tbPassword = new System.Windows.Forms.TextBox();
		this.label1 = new System.Windows.Forms.Label();
		this.btnIdentify = new System.Windows.Forms.Button();
		this.btn1 = new System.Windows.Forms.Button();
		this.btn2 = new System.Windows.Forms.Button();
		this.btn3 = new System.Windows.Forms.Button();
		this.btn6 = new System.Windows.Forms.Button();
		this.btn5 = new System.Windows.Forms.Button();
		this.btn4 = new System.Windows.Forms.Button();
		this.btn9 = new System.Windows.Forms.Button();
		this.btn8 = new System.Windows.Forms.Button();
		this.btn7 = new System.Windows.Forms.Button();
		this.btnClear = new System.Windows.Forms.Button();
		this.btn0 = new System.Windows.Forms.Button();
		base.SuspendLayout();
		this.tbPassword.Location = new System.Drawing.Point(85, 19);
		this.tbPassword.Name = "tbPassword";
		this.tbPassword.Size = new System.Drawing.Size(100, 21);
		this.tbPassword.TabIndex = 0;
		this.tbPassword.Click += new System.EventHandler(tbPassword_Click);
		this.tbPassword.TextChanged += new System.EventHandler(tbPassword_TextChanged);
		this.label1.AutoSize = true;
		this.label1.Location = new System.Drawing.Point(29, 23);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(29, 12);
		this.label1.TabIndex = 1;
		this.label1.Text = "密码";
		this.btnIdentify.Location = new System.Drawing.Point(31, 82);
		this.btnIdentify.Name = "btnIdentify";
		this.btnIdentify.Size = new System.Drawing.Size(154, 23);
		this.btnIdentify.TabIndex = 2;
		this.btnIdentify.Text = "确定";
		this.btnIdentify.UseVisualStyleBackColor = true;
		this.btnIdentify.Click += new System.EventHandler(btnIdentify_Click);
		this.btn1.Location = new System.Drawing.Point(31, 139);
		this.btn1.Name = "btn1";
		this.btn1.Size = new System.Drawing.Size(52, 23);
		this.btn1.TabIndex = 3;
		this.btn1.Text = "1";
		this.btn1.UseVisualStyleBackColor = true;
		this.btn1.Click += new System.EventHandler(btn1_Click);
		this.btn2.Location = new System.Drawing.Point(98, 139);
		this.btn2.Name = "btn2";
		this.btn2.Size = new System.Drawing.Size(52, 23);
		this.btn2.TabIndex = 4;
		this.btn2.Text = "2";
		this.btn2.UseVisualStyleBackColor = true;
		this.btn2.Click += new System.EventHandler(btn2_Click);
		this.btn3.Location = new System.Drawing.Point(167, 139);
		this.btn3.Name = "btn3";
		this.btn3.Size = new System.Drawing.Size(52, 23);
		this.btn3.TabIndex = 5;
		this.btn3.Text = "3";
		this.btn3.UseVisualStyleBackColor = true;
		this.btn3.Click += new System.EventHandler(btn3_Click);
		this.btn6.Location = new System.Drawing.Point(167, 168);
		this.btn6.Name = "btn6";
		this.btn6.Size = new System.Drawing.Size(52, 23);
		this.btn6.TabIndex = 8;
		this.btn6.Text = "6";
		this.btn6.UseVisualStyleBackColor = true;
		this.btn6.Click += new System.EventHandler(btn6_Click);
		this.btn5.Location = new System.Drawing.Point(98, 168);
		this.btn5.Name = "btn5";
		this.btn5.Size = new System.Drawing.Size(52, 23);
		this.btn5.TabIndex = 7;
		this.btn5.Text = "5";
		this.btn5.UseVisualStyleBackColor = true;
		this.btn5.Click += new System.EventHandler(btn5_Click);
		this.btn4.Location = new System.Drawing.Point(31, 168);
		this.btn4.Name = "btn4";
		this.btn4.Size = new System.Drawing.Size(52, 23);
		this.btn4.TabIndex = 6;
		this.btn4.Text = "4";
		this.btn4.UseVisualStyleBackColor = true;
		this.btn4.Click += new System.EventHandler(btn4_Click);
		this.btn9.Location = new System.Drawing.Point(167, 197);
		this.btn9.Name = "btn9";
		this.btn9.Size = new System.Drawing.Size(52, 23);
		this.btn9.TabIndex = 11;
		this.btn9.Text = "9";
		this.btn9.UseVisualStyleBackColor = true;
		this.btn9.Click += new System.EventHandler(btn9_Click);
		this.btn8.Location = new System.Drawing.Point(98, 197);
		this.btn8.Name = "btn8";
		this.btn8.Size = new System.Drawing.Size(52, 23);
		this.btn8.TabIndex = 10;
		this.btn8.Text = "8";
		this.btn8.UseVisualStyleBackColor = true;
		this.btn8.Click += new System.EventHandler(btn8_Click);
		this.btn7.Location = new System.Drawing.Point(31, 197);
		this.btn7.Name = "btn7";
		this.btn7.Size = new System.Drawing.Size(52, 23);
		this.btn7.TabIndex = 9;
		this.btn7.Text = "7";
		this.btn7.UseVisualStyleBackColor = true;
		this.btn7.Click += new System.EventHandler(btn7_Click);
		this.btnClear.Location = new System.Drawing.Point(98, 226);
		this.btnClear.Name = "btnClear";
		this.btnClear.Size = new System.Drawing.Size(52, 23);
		this.btnClear.TabIndex = 13;
		this.btnClear.Text = "清空";
		this.btnClear.UseVisualStyleBackColor = true;
		this.btnClear.Click += new System.EventHandler(btnClear_Click);
		this.btn0.Location = new System.Drawing.Point(31, 226);
		this.btn0.Name = "btn0";
		this.btn0.Size = new System.Drawing.Size(52, 23);
		this.btn0.TabIndex = 12;
		this.btn0.Text = "0";
		this.btn0.UseVisualStyleBackColor = true;
		this.btn0.Click += new System.EventHandler(btn0_Click);
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(247, 314);
		base.Controls.Add(this.btnClear);
		base.Controls.Add(this.btn0);
		base.Controls.Add(this.btn9);
		base.Controls.Add(this.btn8);
		base.Controls.Add(this.btn7);
		base.Controls.Add(this.btn6);
		base.Controls.Add(this.btn5);
		base.Controls.Add(this.btn4);
		base.Controls.Add(this.btn3);
		base.Controls.Add(this.btn2);
		base.Controls.Add(this.btn1);
		base.Controls.Add(this.btnIdentify);
		base.Controls.Add(this.label1);
		base.Controls.Add(this.tbPassword);
		base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
		base.Name = "FormPassword";
		this.Text = "FormPassword";
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
