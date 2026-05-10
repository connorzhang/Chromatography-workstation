using System;
using System.ComponentModel;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace IBrainChrom2018;

public class FormAdvancedP : Form
{
	public static FormAdvancedP formAdvancedP;

	private ChromDeviceListMgr cdlMgr = ChromDeviceListMgr.Create();

	private bool _keepWriting = false;

	private Thread _sendCmd;

	public float fCol1 = 0f;

	private byte[] data_len61 = new byte[61]
	{
		71, 67, 75, 67, 0, 54, 55, 48, 57, 49,
		51, 49, 50, 56, 52, 65, 52, 56, 52, 56,
		52, 53, 3, 167, 37, 48, 0, 1, 50, 1,
		80, 0, 16, 1, 18, 52, 18, 52, 0, 0,
		16, 0, 0, 0, 0, 0, 0, 0, 16, 0,
		0, 0, 0, 0, 0, 0, 0, 16, 0, 0,
		162
	};

	private IContainer components = null;

	private Label label1;

	private TextBox tbPassword;

	private GroupBox groupBox15;

	private Button btnEPCSet;

	private Label label79;

	private Label label34;

	private Label label21;

	private Label label24;

	private Label label25;

	private TextBox tbAirSet2;

	private Label label31;

	private TextBox tbHHSet2;

	private Label label32;

	private TextBox tbColPreSet2;

	private Label label33;

	private Label label29;

	private Label label27;

	private Label label26;

	private TextBox tbAirSet1;

	private Label label23;

	private TextBox tbHHSet1;

	private Label label22;

	private TextBox tbColPreSet1;

	private Label label20;

	private GroupBox groupBox1;

	private Button btnSet;

	public FormAdvancedP()
	{
		formAdvancedP = this;
		InitializeComponent();
		_sendCmd = new Thread(sendData);
		_sendCmd.IsBackground = true;
		_sendCmd.Start();
	}

	private void BtnSet_Click(object sender, EventArgs e)
	{
		string text = tbPassword.Text;
		text = text.Trim();
		if (text.Equals("0632"))
		{
			groupBox15.Visible = true;
			tbColPreSet1.Text = InsDeviceCtrl.self.tbColPreSet1.Text;
			tbColPreSet2.Text = InsDeviceCtrl.self.tbColPreSet2.Text;
			tbHHSet1.Text = InsDeviceCtrl.self.tbHHSet1.Text;
			tbHHSet2.Text = InsDeviceCtrl.self.tbHHSet2.Text;
			tbAirSet1.Text = InsDeviceCtrl.self.tbAirSet1.Text;
			tbAirSet2.Text = InsDeviceCtrl.self.tbAirSet2.Text;
		}
		else
		{
			MessageBox.Show("密码错误！");
		}
	}

	public void sendData()
	{
		TcpServerSocket currentTcpServerSocket = cdlMgr.CurrentTcpServerSocket;
		byte[] array = new byte[2];
		while (true)
		{
			currentTcpServerSocket = cdlMgr.CurrentTcpServerSocket;
			if (currentTcpServerSocket != null && _keepWriting)
			{
				data_len61[25] = 48;
				data_len61[27] = 0;
				fCol1 = Class49.String2Float(tbColPreSet1.Text, 0f);
				array = IBrainConvert.Float2Byte(fCol1, 1);
				data_len61[32] = array[0];
				data_len61[33] = array[1];
				currentTcpServerSocket.SendData(data_len61);
				Thread.Sleep(1500);
				data_len61[25] = 49;
				data_len61[26] = 1;
				data_len61[27] = 1;
				fCol1 = Class49.String2Float(tbHHSet1.Text, 0f);
				array = IBrainConvert.Float2Byte(fCol1, 1);
				data_len61[32] = array[0];
				data_len61[33] = array[1];
				currentTcpServerSocket.SendData(data_len61);
				Thread.Sleep(1500);
				data_len61[25] = 50;
				data_len61[26] = 2;
				data_len61[27] = 1;
				fCol1 = Class49.String2Float(tbAirSet1.Text, 0f);
				array = IBrainConvert.Float2Byte(fCol1, 1);
				data_len61[32] = array[0];
				data_len61[33] = array[1];
				currentTcpServerSocket.SendData(data_len61);
				Thread.Sleep(1500);
				data_len61[25] = 51;
				data_len61[27] = 0;
				fCol1 = Class49.String2Float(tbColPreSet2.Text, 0f);
				array = IBrainConvert.Float2Byte(fCol1, 1);
				data_len61[32] = array[0];
				data_len61[33] = array[1];
				currentTcpServerSocket.SendData(data_len61);
				Thread.Sleep(1500);
				data_len61[25] = 52;
				data_len61[26] = 1;
				data_len61[27] = 1;
				fCol1 = Class49.String2Float(tbHHSet2.Text, 0f);
				array = IBrainConvert.Float2Byte(fCol1, 1);
				data_len61[32] = array[0];
				data_len61[33] = array[1];
				currentTcpServerSocket.SendData(data_len61);
				Thread.Sleep(4500);
				data_len61[25] = 53;
				data_len61[26] = 2;
				data_len61[27] = 1;
				fCol1 = Class49.String2Float(tbAirSet2.Text, 0f);
				array = IBrainConvert.Float2Byte(fCol1, 1);
				data_len61[32] = array[0];
				data_len61[33] = array[1];
				currentTcpServerSocket.SendData(data_len61);
				Thread.Sleep(500);
				_keepWriting = false;
			}
		}
	}

	private void BtnEPCSet_Click(object sender, EventArgs e)
	{
		_keepWriting = true;
	}

	private void tbPassword_TextChanged(object sender, EventArgs e)
	{
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(IBrainChrom2018.FormAdvancedP));
		this.label1 = new System.Windows.Forms.Label();
		this.tbPassword = new System.Windows.Forms.TextBox();
		this.groupBox15 = new System.Windows.Forms.GroupBox();
		this.btnEPCSet = new System.Windows.Forms.Button();
		this.label79 = new System.Windows.Forms.Label();
		this.label34 = new System.Windows.Forms.Label();
		this.label21 = new System.Windows.Forms.Label();
		this.label24 = new System.Windows.Forms.Label();
		this.label25 = new System.Windows.Forms.Label();
		this.tbAirSet2 = new System.Windows.Forms.TextBox();
		this.label31 = new System.Windows.Forms.Label();
		this.tbHHSet2 = new System.Windows.Forms.TextBox();
		this.label32 = new System.Windows.Forms.Label();
		this.tbColPreSet2 = new System.Windows.Forms.TextBox();
		this.label33 = new System.Windows.Forms.Label();
		this.label29 = new System.Windows.Forms.Label();
		this.label27 = new System.Windows.Forms.Label();
		this.label26 = new System.Windows.Forms.Label();
		this.tbAirSet1 = new System.Windows.Forms.TextBox();
		this.label23 = new System.Windows.Forms.Label();
		this.tbHHSet1 = new System.Windows.Forms.TextBox();
		this.label22 = new System.Windows.Forms.Label();
		this.tbColPreSet1 = new System.Windows.Forms.TextBox();
		this.label20 = new System.Windows.Forms.Label();
		this.groupBox1 = new System.Windows.Forms.GroupBox();
		this.btnSet = new System.Windows.Forms.Button();
		this.groupBox15.SuspendLayout();
		this.groupBox1.SuspendLayout();
		base.SuspendLayout();
		this.label1.AutoSize = true;
		this.label1.Location = new System.Drawing.Point(11, 23);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(41, 12);
		this.label1.TabIndex = 0;
		this.label1.Text = "密码：";
		this.tbPassword.Location = new System.Drawing.Point(48, 20);
		this.tbPassword.Name = "tbPassword";
		this.tbPassword.Size = new System.Drawing.Size(100, 21);
		this.tbPassword.TabIndex = 1;
		this.tbPassword.TextChanged += new System.EventHandler(tbPassword_TextChanged);
		this.groupBox15.Controls.Add(this.btnEPCSet);
		this.groupBox15.Controls.Add(this.label79);
		this.groupBox15.Controls.Add(this.label34);
		this.groupBox15.Controls.Add(this.label21);
		this.groupBox15.Controls.Add(this.label24);
		this.groupBox15.Controls.Add(this.label25);
		this.groupBox15.Controls.Add(this.tbAirSet2);
		this.groupBox15.Controls.Add(this.label31);
		this.groupBox15.Controls.Add(this.tbHHSet2);
		this.groupBox15.Controls.Add(this.label32);
		this.groupBox15.Controls.Add(this.tbColPreSet2);
		this.groupBox15.Controls.Add(this.label33);
		this.groupBox15.Controls.Add(this.label29);
		this.groupBox15.Controls.Add(this.label27);
		this.groupBox15.Controls.Add(this.label26);
		this.groupBox15.Controls.Add(this.tbAirSet1);
		this.groupBox15.Controls.Add(this.label23);
		this.groupBox15.Controls.Add(this.tbHHSet1);
		this.groupBox15.Controls.Add(this.label22);
		this.groupBox15.Controls.Add(this.tbColPreSet1);
		this.groupBox15.Controls.Add(this.label20);
		this.groupBox15.Location = new System.Drawing.Point(12, 154);
		this.groupBox15.Name = "groupBox15";
		this.groupBox15.Size = new System.Drawing.Size(333, 150);
		this.groupBox15.TabIndex = 55;
		this.groupBox15.TabStop = false;
		this.groupBox15.Text = "EPC";
		this.groupBox15.Visible = false;
		this.btnEPCSet.ForeColor = System.Drawing.Color.Black;
		this.btnEPCSet.Location = new System.Drawing.Point(212, 121);
		this.btnEPCSet.Name = "btnEPCSet";
		this.btnEPCSet.Size = new System.Drawing.Size(75, 23);
		this.btnEPCSet.TabIndex = 56;
		this.btnEPCSet.Text = "设定";
		this.btnEPCSet.UseVisualStyleBackColor = true;
		this.btnEPCSet.Click += new System.EventHandler(BtnEPCSet_Click);
		this.label79.AutoSize = true;
		this.label79.ForeColor = System.Drawing.Color.Black;
		this.label79.Location = new System.Drawing.Point(218, 14);
		this.label79.Name = "label79";
		this.label79.Size = new System.Drawing.Size(29, 12);
		this.label79.TabIndex = 36;
		this.label79.Text = "设定";
		this.label34.AutoSize = true;
		this.label34.ForeColor = System.Drawing.Color.Black;
		this.label34.Location = new System.Drawing.Point(51, 14);
		this.label34.Name = "label34";
		this.label34.Size = new System.Drawing.Size(29, 12);
		this.label34.TabIndex = 34;
		this.label34.Text = "设定";
		this.label21.AutoSize = true;
		this.label21.ForeColor = System.Drawing.Color.Black;
		this.label21.Location = new System.Drawing.Point(266, 86);
		this.label21.Name = "label21";
		this.label21.Size = new System.Drawing.Size(23, 12);
		this.label21.TabIndex = 33;
		this.label21.Text = "psi";
		this.label24.AutoSize = true;
		this.label24.ForeColor = System.Drawing.Color.Black;
		this.label24.Location = new System.Drawing.Point(266, 59);
		this.label24.Name = "label24";
		this.label24.Size = new System.Drawing.Size(23, 12);
		this.label24.TabIndex = 32;
		this.label24.Text = "psi";
		this.label25.AutoSize = true;
		this.label25.ForeColor = System.Drawing.Color.Black;
		this.label25.Location = new System.Drawing.Point(266, 32);
		this.label25.Name = "label25";
		this.label25.Size = new System.Drawing.Size(23, 12);
		this.label25.TabIndex = 22;
		this.label25.Text = "psi";
		this.tbAirSet2.Location = new System.Drawing.Point(212, 83);
		this.tbAirSet2.Name = "tbAirSet2";
		this.tbAirSet2.Size = new System.Drawing.Size(41, 21);
		this.tbAirSet2.TabIndex = 30;
		this.label31.AutoSize = true;
		this.label31.ForeColor = System.Drawing.Color.Black;
		this.label31.Location = new System.Drawing.Point(172, 86);
		this.label31.Name = "label31";
		this.label31.Size = new System.Drawing.Size(41, 12);
		this.label31.TabIndex = 29;
		this.label31.Text = "载气4:";
		this.tbHHSet2.Location = new System.Drawing.Point(212, 56);
		this.tbHHSet2.Name = "tbHHSet2";
		this.tbHHSet2.Size = new System.Drawing.Size(41, 21);
		this.tbHHSet2.TabIndex = 27;
		this.label32.AutoSize = true;
		this.label32.ForeColor = System.Drawing.Color.Black;
		this.label32.Location = new System.Drawing.Point(172, 59);
		this.label32.Name = "label32";
		this.label32.Size = new System.Drawing.Size(41, 12);
		this.label32.TabIndex = 26;
		this.label32.Text = "载气3:";
		this.tbColPreSet2.Location = new System.Drawing.Point(212, 29);
		this.tbColPreSet2.Name = "tbColPreSet2";
		this.tbColPreSet2.Size = new System.Drawing.Size(41, 21);
		this.tbColPreSet2.TabIndex = 24;
		this.label33.AutoSize = true;
		this.label33.ForeColor = System.Drawing.Color.Black;
		this.label33.Location = new System.Drawing.Point(172, 32);
		this.label33.Name = "label33";
		this.label33.Size = new System.Drawing.Size(41, 12);
		this.label33.TabIndex = 23;
		this.label33.Text = "载气2:";
		this.label29.AutoSize = true;
		this.label29.ForeColor = System.Drawing.Color.Black;
		this.label29.Location = new System.Drawing.Point(98, 86);
		this.label29.Name = "label29";
		this.label29.Size = new System.Drawing.Size(41, 12);
		this.label29.TabIndex = 21;
		this.label29.Text = "ml/min";
		this.label27.AutoSize = true;
		this.label27.ForeColor = System.Drawing.Color.Black;
		this.label27.Location = new System.Drawing.Point(98, 59);
		this.label27.Name = "label27";
		this.label27.Size = new System.Drawing.Size(41, 12);
		this.label27.TabIndex = 20;
		this.label27.Text = "ml/min";
		this.label26.AutoSize = true;
		this.label26.ForeColor = System.Drawing.Color.Black;
		this.label26.Location = new System.Drawing.Point(98, 32);
		this.label26.Name = "label26";
		this.label26.Size = new System.Drawing.Size(23, 12);
		this.label26.TabIndex = 0;
		this.label26.Text = "psi";
		this.tbAirSet1.Location = new System.Drawing.Point(46, 83);
		this.tbAirSet1.Name = "tbAirSet1";
		this.tbAirSet1.Size = new System.Drawing.Size(41, 21);
		this.tbAirSet1.TabIndex = 9;
		this.label23.AutoSize = true;
		this.label23.ForeColor = System.Drawing.Color.Black;
		this.label23.Location = new System.Drawing.Point(6, 86);
		this.label23.Name = "label23";
		this.label23.Size = new System.Drawing.Size(41, 12);
		this.label23.TabIndex = 8;
		this.label23.Text = "空气1:";
		this.tbHHSet1.Location = new System.Drawing.Point(46, 56);
		this.tbHHSet1.Name = "tbHHSet1";
		this.tbHHSet1.Size = new System.Drawing.Size(41, 21);
		this.tbHHSet1.TabIndex = 6;
		this.label22.AutoSize = true;
		this.label22.ForeColor = System.Drawing.Color.Black;
		this.label22.Location = new System.Drawing.Point(6, 59);
		this.label22.Name = "label22";
		this.label22.Size = new System.Drawing.Size(41, 12);
		this.label22.TabIndex = 5;
		this.label22.Text = "氢气1:";
		this.tbColPreSet1.Location = new System.Drawing.Point(46, 29);
		this.tbColPreSet1.Name = "tbColPreSet1";
		this.tbColPreSet1.Size = new System.Drawing.Size(41, 21);
		this.tbColPreSet1.TabIndex = 1;
		this.label20.AutoSize = true;
		this.label20.ForeColor = System.Drawing.Color.Black;
		this.label20.Location = new System.Drawing.Point(6, 32);
		this.label20.Name = "label20";
		this.label20.Size = new System.Drawing.Size(41, 12);
		this.label20.TabIndex = 0;
		this.label20.Text = "载气1:";
		this.groupBox1.Controls.Add(this.btnSet);
		this.groupBox1.Controls.Add(this.tbPassword);
		this.groupBox1.Controls.Add(this.label1);
		this.groupBox1.Location = new System.Drawing.Point(12, 12);
		this.groupBox1.Name = "groupBox1";
		this.groupBox1.Size = new System.Drawing.Size(333, 136);
		this.groupBox1.TabIndex = 56;
		this.groupBox1.TabStop = false;
		this.groupBox1.Text = "权限管理";
		this.btnSet.Location = new System.Drawing.Point(13, 65);
		this.btnSet.Name = "btnSet";
		this.btnSet.Size = new System.Drawing.Size(135, 35);
		this.btnSet.TabIndex = 2;
		this.btnSet.Text = "确定";
		this.btnSet.UseVisualStyleBackColor = true;
		this.btnSet.Click += new System.EventHandler(BtnSet_Click);
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(356, 313);
		base.Controls.Add(this.groupBox1);
		base.Controls.Add(this.groupBox15);
		base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
		base.Name = "FormAdvancedP";
		this.Text = "FormAdvancedP";
		this.groupBox15.ResumeLayout(false);
		this.groupBox15.PerformLayout();
		this.groupBox1.ResumeLayout(false);
		this.groupBox1.PerformLayout();
		base.ResumeLayout(false);
	}
}
