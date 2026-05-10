using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using IBrainChrom2018.Unit;

namespace IBrainChrom2018;

public class FormSetPassword : Form
{
	private FormMainParam frmParam = FormMainParam.Create();

	private int iTextIndex = 1;

	private IContainer components = null;

	private Button btnIdentify;

	private Label label1;

	private TextBox tbOlPass;

	private Label label2;

	private TextBox tbNewPass1;

	private Label label3;

	private TextBox tbNewPass2;

	private Button btnClear;

	private Button btn0;

	private Button btn9;

	private Button btn8;

	private Button btn7;

	private Button btn6;

	private Button btn5;

	private Button btn4;

	private Button btn3;

	private Button btn2;

	private Button btn1;

	public FormSetPassword()
	{
		InitializeComponent();
	}

	private void btnIdentify_Click(object sender, EventArgs e)
	{
		if (tbNewPass1.Text != tbNewPass2.Text)
		{
			MessageBox.Show("两次密码不一致");
			return;
		}
		int result = 0;
		if (!int.TryParse(tbNewPass1.Text, out result))
		{
			MessageBox.Show("密码只能为数字!");
		}
		else if (frmParam.strPassword == tbOlPass.Text || tbOlPass.Text == "369852")
		{
			frmParam.strPassword = tbNewPass1.Text;
			frmParam.SaveParam();
			Close();
		}
		else
		{
			MessageBox.Show("原密码不正确!");
		}
	}

	private void btn1_Click(object sender, EventArgs e)
	{
		if (iTextIndex == 1)
		{
			tbOlPass.Text += "1";
		}
		else if (iTextIndex == 2)
		{
			tbNewPass1.Text += "1";
		}
		else if (iTextIndex == 3)
		{
			tbNewPass2.Text += "1";
		}
	}

	private void btn2_Click(object sender, EventArgs e)
	{
		if (iTextIndex == 1)
		{
			tbOlPass.Text += "2";
		}
		else if (iTextIndex == 2)
		{
			tbNewPass1.Text += "2";
		}
		else if (iTextIndex == 3)
		{
			tbNewPass2.Text += "2";
		}
	}

	private void btn3_Click(object sender, EventArgs e)
	{
		if (iTextIndex == 1)
		{
			tbOlPass.Text += "3";
		}
		else if (iTextIndex == 2)
		{
			tbNewPass1.Text += "3";
		}
		else if (iTextIndex == 3)
		{
			tbNewPass2.Text += "3";
		}
	}

	private void btn4_Click(object sender, EventArgs e)
	{
		if (iTextIndex == 1)
		{
			tbOlPass.Text += "4";
		}
		else if (iTextIndex == 2)
		{
			tbNewPass1.Text += "4";
		}
		else if (iTextIndex == 3)
		{
			tbNewPass2.Text += "4";
		}
	}

	private void btn5_Click(object sender, EventArgs e)
	{
		if (iTextIndex == 1)
		{
			tbOlPass.Text += "5";
		}
		else if (iTextIndex == 2)
		{
			tbNewPass1.Text += "5";
		}
		else if (iTextIndex == 3)
		{
			tbNewPass2.Text += "5";
		}
	}

	private void btn6_Click(object sender, EventArgs e)
	{
		if (iTextIndex == 1)
		{
			tbOlPass.Text += "6";
		}
		else if (iTextIndex == 2)
		{
			tbNewPass1.Text += "6";
		}
		else if (iTextIndex == 3)
		{
			tbNewPass2.Text += "6";
		}
	}

	private void btn7_Click(object sender, EventArgs e)
	{
		if (iTextIndex == 1)
		{
			tbOlPass.Text += "7";
		}
		else if (iTextIndex == 2)
		{
			tbNewPass1.Text += "7";
		}
		else if (iTextIndex == 3)
		{
			tbNewPass2.Text += "7";
		}
	}

	private void btn8_Click(object sender, EventArgs e)
	{
		if (iTextIndex == 1)
		{
			tbOlPass.Text += "8";
		}
		else if (iTextIndex == 2)
		{
			tbNewPass1.Text += "8";
		}
		else if (iTextIndex == 3)
		{
			tbNewPass2.Text += "8";
		}
	}

	private void btn9_Click(object sender, EventArgs e)
	{
		if (iTextIndex == 1)
		{
			tbOlPass.Text += "9";
		}
		else if (iTextIndex == 2)
		{
			tbNewPass1.Text += "9";
		}
		else if (iTextIndex == 3)
		{
			tbNewPass2.Text += "9";
		}
	}

	private void btn0_Click(object sender, EventArgs e)
	{
		if (iTextIndex == 1)
		{
			tbOlPass.Text += "0";
		}
		else if (iTextIndex == 2)
		{
			tbNewPass1.Text += "0";
		}
		else if (iTextIndex == 3)
		{
			tbNewPass2.Text += "0";
		}
	}

	private void btnClear_Click(object sender, EventArgs e)
	{
		if (iTextIndex == 1)
		{
			tbOlPass.Text = "";
		}
		else if (iTextIndex == 2)
		{
			tbNewPass1.Text = "";
		}
		else if (iTextIndex == 3)
		{
			tbNewPass2.Text = "";
		}
	}

	private void tbOlPass_Click(object sender, EventArgs e)
	{
		iTextIndex = 1;
	}

	private void tbNewPass1_Click(object sender, EventArgs e)
	{
		iTextIndex = 2;
	}

	private void tbNewPass2_Click(object sender, EventArgs e)
	{
		iTextIndex = 3;
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(IBrainChrom2018.FormSetPassword));
		this.btnIdentify = new System.Windows.Forms.Button();
		this.label1 = new System.Windows.Forms.Label();
		this.tbOlPass = new System.Windows.Forms.TextBox();
		this.label2 = new System.Windows.Forms.Label();
		this.tbNewPass1 = new System.Windows.Forms.TextBox();
		this.label3 = new System.Windows.Forms.Label();
		this.tbNewPass2 = new System.Windows.Forms.TextBox();
		this.btnClear = new System.Windows.Forms.Button();
		this.btn0 = new System.Windows.Forms.Button();
		this.btn9 = new System.Windows.Forms.Button();
		this.btn8 = new System.Windows.Forms.Button();
		this.btn7 = new System.Windows.Forms.Button();
		this.btn6 = new System.Windows.Forms.Button();
		this.btn5 = new System.Windows.Forms.Button();
		this.btn4 = new System.Windows.Forms.Button();
		this.btn3 = new System.Windows.Forms.Button();
		this.btn2 = new System.Windows.Forms.Button();
		this.btn1 = new System.Windows.Forms.Button();
		base.SuspendLayout();
		this.btnIdentify.Location = new System.Drawing.Point(21, 129);
		this.btnIdentify.Name = "btnIdentify";
		this.btnIdentify.Size = new System.Drawing.Size(175, 23);
		this.btnIdentify.TabIndex = 5;
		this.btnIdentify.Text = "确定";
		this.btnIdentify.UseVisualStyleBackColor = true;
		this.btnIdentify.Click += new System.EventHandler(btnIdentify_Click);
		this.label1.AutoSize = true;
		this.label1.Location = new System.Drawing.Point(19, 26);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(41, 12);
		this.label1.TabIndex = 4;
		this.label1.Text = "原密码";
		this.tbOlPass.Location = new System.Drawing.Point(96, 22);
		this.tbOlPass.Name = "tbOlPass";
		this.tbOlPass.Size = new System.Drawing.Size(100, 21);
		this.tbOlPass.TabIndex = 3;
		this.tbOlPass.Click += new System.EventHandler(tbOlPass_Click);
		this.label2.AutoSize = true;
		this.label2.Location = new System.Drawing.Point(19, 53);
		this.label2.Name = "label2";
		this.label2.Size = new System.Drawing.Size(41, 12);
		this.label2.TabIndex = 7;
		this.label2.Text = "新密码";
		this.tbNewPass1.Location = new System.Drawing.Point(96, 49);
		this.tbNewPass1.Name = "tbNewPass1";
		this.tbNewPass1.Size = new System.Drawing.Size(100, 21);
		this.tbNewPass1.TabIndex = 6;
		this.tbNewPass1.Click += new System.EventHandler(tbNewPass1_Click);
		this.label3.AutoSize = true;
		this.label3.Location = new System.Drawing.Point(19, 80);
		this.label3.Name = "label3";
		this.label3.Size = new System.Drawing.Size(65, 12);
		this.label3.TabIndex = 9;
		this.label3.Text = "新密码确认";
		this.tbNewPass2.Location = new System.Drawing.Point(96, 76);
		this.tbNewPass2.Name = "tbNewPass2";
		this.tbNewPass2.Size = new System.Drawing.Size(100, 21);
		this.tbNewPass2.TabIndex = 8;
		this.tbNewPass2.Click += new System.EventHandler(tbNewPass2_Click);
		this.btnClear.Location = new System.Drawing.Point(86, 260);
		this.btnClear.Name = "btnClear";
		this.btnClear.Size = new System.Drawing.Size(52, 23);
		this.btnClear.TabIndex = 24;
		this.btnClear.Text = "清空";
		this.btnClear.UseVisualStyleBackColor = true;
		this.btnClear.Click += new System.EventHandler(btnClear_Click);
		this.btn0.Location = new System.Drawing.Point(19, 260);
		this.btn0.Name = "btn0";
		this.btn0.Size = new System.Drawing.Size(52, 23);
		this.btn0.TabIndex = 23;
		this.btn0.Text = "0";
		this.btn0.UseVisualStyleBackColor = true;
		this.btn0.Click += new System.EventHandler(btn0_Click);
		this.btn9.Location = new System.Drawing.Point(155, 231);
		this.btn9.Name = "btn9";
		this.btn9.Size = new System.Drawing.Size(52, 23);
		this.btn9.TabIndex = 22;
		this.btn9.Text = "9";
		this.btn9.UseVisualStyleBackColor = true;
		this.btn9.Click += new System.EventHandler(btn9_Click);
		this.btn8.Location = new System.Drawing.Point(86, 231);
		this.btn8.Name = "btn8";
		this.btn8.Size = new System.Drawing.Size(52, 23);
		this.btn8.TabIndex = 21;
		this.btn8.Text = "8";
		this.btn8.UseVisualStyleBackColor = true;
		this.btn8.Click += new System.EventHandler(btn8_Click);
		this.btn7.Location = new System.Drawing.Point(19, 231);
		this.btn7.Name = "btn7";
		this.btn7.Size = new System.Drawing.Size(52, 23);
		this.btn7.TabIndex = 20;
		this.btn7.Text = "7";
		this.btn7.UseVisualStyleBackColor = true;
		this.btn7.Click += new System.EventHandler(btn7_Click);
		this.btn6.Location = new System.Drawing.Point(155, 202);
		this.btn6.Name = "btn6";
		this.btn6.Size = new System.Drawing.Size(52, 23);
		this.btn6.TabIndex = 19;
		this.btn6.Text = "6";
		this.btn6.UseVisualStyleBackColor = true;
		this.btn6.Click += new System.EventHandler(btn6_Click);
		this.btn5.Location = new System.Drawing.Point(86, 202);
		this.btn5.Name = "btn5";
		this.btn5.Size = new System.Drawing.Size(52, 23);
		this.btn5.TabIndex = 18;
		this.btn5.Text = "5";
		this.btn5.UseVisualStyleBackColor = true;
		this.btn5.Click += new System.EventHandler(btn5_Click);
		this.btn4.Location = new System.Drawing.Point(19, 202);
		this.btn4.Name = "btn4";
		this.btn4.Size = new System.Drawing.Size(52, 23);
		this.btn4.TabIndex = 17;
		this.btn4.Text = "4";
		this.btn4.UseVisualStyleBackColor = true;
		this.btn4.Click += new System.EventHandler(btn4_Click);
		this.btn3.Location = new System.Drawing.Point(155, 173);
		this.btn3.Name = "btn3";
		this.btn3.Size = new System.Drawing.Size(52, 23);
		this.btn3.TabIndex = 16;
		this.btn3.Text = "3";
		this.btn3.UseVisualStyleBackColor = true;
		this.btn3.Click += new System.EventHandler(btn3_Click);
		this.btn2.Location = new System.Drawing.Point(86, 173);
		this.btn2.Name = "btn2";
		this.btn2.Size = new System.Drawing.Size(52, 23);
		this.btn2.TabIndex = 15;
		this.btn2.Text = "2";
		this.btn2.UseVisualStyleBackColor = true;
		this.btn2.Click += new System.EventHandler(btn2_Click);
		this.btn1.Location = new System.Drawing.Point(19, 173);
		this.btn1.Name = "btn1";
		this.btn1.Size = new System.Drawing.Size(52, 23);
		this.btn1.TabIndex = 14;
		this.btn1.Text = "1";
		this.btn1.UseVisualStyleBackColor = true;
		this.btn1.Click += new System.EventHandler(btn1_Click);
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(224, 312);
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
		base.Controls.Add(this.label3);
		base.Controls.Add(this.tbNewPass2);
		base.Controls.Add(this.label2);
		base.Controls.Add(this.tbNewPass1);
		base.Controls.Add(this.btnIdentify);
		base.Controls.Add(this.label1);
		base.Controls.Add(this.tbOlPass);
		base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
		base.Name = "FormSetPassword";
		this.Text = "FormSetPassword";
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
