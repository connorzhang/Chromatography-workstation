using System;
using System.ComponentModel;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace IBrainChrom2018;

public class FormEPC : Form
{
	public static FormEPC formAdvancedP;

	private ChromDeviceListMgr cdlMgr = ChromDeviceListMgr.Create();

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

	private GroupBox groupBox15;

	private Button btnEPCSet;

	private Label label47;

	private Label label61;

	public TextBox tbColPreSet1;

	private Label label49;

	private Label label50;

	private Label label60;

	private Label label51;

	public TextBox tbHHSet1;

	private Label label52;

	private Label label59;

	public TextBox tbWeiChuiSet1;

	public TextBox tbAirSet1;

	private Label label53;

	private Label label58;

	public TextBox tbColPreSet3;

	private Label label57;

	private Label label54;

	private Label label56;

	private Label label55;

	public TextBox tbColPreSet2;

	public FormEPC()
	{
		formAdvancedP = this;
		InitializeComponent();
		groupBox15.Visible = true;
		tbColPreSet1.Text = OnlineCtrl.selfCtrl.tbColPreSet1.Text;
		tbColPreSet2.Text = OnlineCtrl.selfCtrl.tbColPreSet2.Text;
		tbColPreSet3.Text = OnlineCtrl.selfCtrl.tbColPreSet3.Text;
		tbHHSet1.Text = OnlineCtrl.selfCtrl.tbHHSet1.Text;
		tbAirSet1.Text = OnlineCtrl.selfCtrl.tbAirSet1.Text;
		tbWeiChuiSet1.Text = OnlineCtrl.selfCtrl.tbWeiChuiSet1.Text;
		LoadLanguage();
	}

	private void LoadLanguage()
	{
		label49.Text = Lang.PS("设定值", "Set");
		label47.Text = Lang.PS("设定值", "Set");
		label61.Text = Lang.PS("载气1", "Cur1");
		label55.Text = Lang.PS("载气2", "Cur2");
		label54.Text = Lang.PS("载气3", "Cur3");
		label60.Text = Lang.PS("氢气1", "HH1");
		label59.Text = Lang.PS("空气1", "Air1");
		label53.Text = Lang.PS("尾吹1", "MakeUp1");
		btnEPCSet.Text = Lang.PS("设定", "Set");
	}

	private void BtnSet_Click(object sender, EventArgs e)
	{
		string text = "0632";
		text = text.Trim();
		if (text.Equals("0632"))
		{
			groupBox15.Visible = true;
			tbColPreSet1.Text = OnlineCtrl.selfCtrl.tbColPreSet1.Text;
			tbColPreSet2.Text = OnlineCtrl.selfCtrl.tbColPreSet2.Text;
			tbColPreSet3.Text = OnlineCtrl.selfCtrl.tbColPreSet3.Text;
			tbHHSet1.Text = OnlineCtrl.selfCtrl.tbHHSet1.Text;
			tbAirSet1.Text = OnlineCtrl.selfCtrl.tbAirSet1.Text;
			tbWeiChuiSet1.Text = OnlineCtrl.selfCtrl.tbWeiChuiSet1.Text;
		}
		else
		{
			MessageBox.Show("密码错误！");
		}
	}

	private void BtnEPCSet_Click(object sender, EventArgs e)
	{
		TcpServerSocket currentTcpServerSocket = cdlMgr.CurrentTcpServerSocket;
		byte[] array = new byte[2];
		if (currentTcpServerSocket != null)
		{
			data_len61[25] = 49;
			data_len61[26] = 1;
			data_len61[27] = 1;
			fCol1 = Class49.String2Float(tbColPreSet1.Text, 0f);
			array = IBrainConvert.Float2Byte(fCol1, 1);
			data_len61[32] = array[0];
			data_len61[33] = array[1];
			currentTcpServerSocket.SendData(data_len61);
			Thread.Sleep(1500);
			data_len61[25] = 50;
			data_len61[26] = 1;
			data_len61[27] = 1;
			fCol1 = Class49.String2Float(tbColPreSet2.Text, 0f);
			array = IBrainConvert.Float2Byte(fCol1, 1);
			data_len61[32] = array[0];
			data_len61[33] = array[1];
			currentTcpServerSocket.SendData(data_len61);
			Thread.Sleep(1500);
			data_len61[25] = 51;
			data_len61[26] = 1;
			data_len61[27] = 1;
			fCol1 = Class49.String2Float(tbColPreSet3.Text, 0f);
			array = IBrainConvert.Float2Byte(fCol1, 1);
			data_len61[32] = array[0];
			data_len61[33] = array[1];
			currentTcpServerSocket.SendData(data_len61);
			Thread.Sleep(1500);
			data_len61[25] = 57;
			data_len61[27] = 0;
			fCol1 = Class49.String2Float(tbHHSet1.Text, 0f);
			array = IBrainConvert.Float2Byte(fCol1, 1);
			data_len61[32] = array[0];
			data_len61[33] = array[1];
			currentTcpServerSocket.SendData(data_len61);
			Thread.Sleep(1500);
			data_len61[25] = 58;
			data_len61[26] = 1;
			data_len61[27] = 1;
			fCol1 = Class49.String2Float(tbAirSet1.Text, 0f);
			array = IBrainConvert.Float2Byte(fCol1, 1);
			data_len61[32] = array[0];
			data_len61[33] = array[1];
			currentTcpServerSocket.SendData(data_len61);
			Thread.Sleep(1500);
			data_len61[25] = 59;
			data_len61[26] = 2;
			data_len61[27] = 1;
			fCol1 = Class49.String2Float(tbWeiChuiSet1.Text, 0f);
			array = IBrainConvert.Float2Byte(fCol1, 1);
			data_len61[32] = array[0];
			data_len61[33] = array[1];
			currentTcpServerSocket.SendData(data_len61);
			Thread.Sleep(500);
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(IBrainChrom2018.FormEPC));
		this.groupBox15 = new System.Windows.Forms.GroupBox();
		this.btnEPCSet = new System.Windows.Forms.Button();
		this.label47 = new System.Windows.Forms.Label();
		this.label61 = new System.Windows.Forms.Label();
		this.tbColPreSet1 = new System.Windows.Forms.TextBox();
		this.label49 = new System.Windows.Forms.Label();
		this.label50 = new System.Windows.Forms.Label();
		this.label60 = new System.Windows.Forms.Label();
		this.label51 = new System.Windows.Forms.Label();
		this.tbHHSet1 = new System.Windows.Forms.TextBox();
		this.label52 = new System.Windows.Forms.Label();
		this.label59 = new System.Windows.Forms.Label();
		this.tbWeiChuiSet1 = new System.Windows.Forms.TextBox();
		this.tbAirSet1 = new System.Windows.Forms.TextBox();
		this.label53 = new System.Windows.Forms.Label();
		this.label58 = new System.Windows.Forms.Label();
		this.tbColPreSet3 = new System.Windows.Forms.TextBox();
		this.label57 = new System.Windows.Forms.Label();
		this.label54 = new System.Windows.Forms.Label();
		this.label56 = new System.Windows.Forms.Label();
		this.label55 = new System.Windows.Forms.Label();
		this.tbColPreSet2 = new System.Windows.Forms.TextBox();
		this.groupBox15.SuspendLayout();
		base.SuspendLayout();
		this.groupBox15.Controls.Add(this.label47);
		this.groupBox15.Controls.Add(this.label61);
		this.groupBox15.Controls.Add(this.tbColPreSet1);
		this.groupBox15.Controls.Add(this.label49);
		this.groupBox15.Controls.Add(this.label50);
		this.groupBox15.Controls.Add(this.label60);
		this.groupBox15.Controls.Add(this.label51);
		this.groupBox15.Controls.Add(this.tbHHSet1);
		this.groupBox15.Controls.Add(this.label52);
		this.groupBox15.Controls.Add(this.label59);
		this.groupBox15.Controls.Add(this.tbWeiChuiSet1);
		this.groupBox15.Controls.Add(this.tbAirSet1);
		this.groupBox15.Controls.Add(this.label53);
		this.groupBox15.Controls.Add(this.label58);
		this.groupBox15.Controls.Add(this.tbColPreSet3);
		this.groupBox15.Controls.Add(this.label57);
		this.groupBox15.Controls.Add(this.label54);
		this.groupBox15.Controls.Add(this.label56);
		this.groupBox15.Controls.Add(this.label55);
		this.groupBox15.Controls.Add(this.tbColPreSet2);
		this.groupBox15.Controls.Add(this.btnEPCSet);
		this.groupBox15.Location = new System.Drawing.Point(11, 12);
		this.groupBox15.Name = "groupBox15";
		this.groupBox15.Size = new System.Drawing.Size(307, 195);
		this.groupBox15.TabIndex = 55;
		this.groupBox15.TabStop = false;
		this.groupBox15.Text = "EPC";
		this.btnEPCSet.ForeColor = System.Drawing.Color.Black;
		this.btnEPCSet.Location = new System.Drawing.Point(6, 138);
		this.btnEPCSet.Name = "btnEPCSet";
		this.btnEPCSet.Size = new System.Drawing.Size(291, 40);
		this.btnEPCSet.TabIndex = 56;
		this.btnEPCSet.Text = "设定";
		this.btnEPCSet.UseVisualStyleBackColor = true;
		this.btnEPCSet.Click += new System.EventHandler(BtnEPCSet_Click);
		this.label47.AutoSize = true;
		this.label47.ForeColor = System.Drawing.Color.Black;
		this.label47.Location = new System.Drawing.Point(196, 15);
		this.label47.Name = "label47";
		this.label47.Size = new System.Drawing.Size(41, 12);
		this.label47.TabIndex = 83;
		this.label47.Text = "设定值";
		this.label61.AutoSize = true;
		this.label61.ForeColor = System.Drawing.Color.Black;
		this.label61.Location = new System.Drawing.Point(9, 37);
		this.label61.Name = "label61";
		this.label61.Size = new System.Drawing.Size(41, 12);
		this.label61.TabIndex = 57;
		this.label61.Text = "载气1:";
		this.tbColPreSet1.Location = new System.Drawing.Point(50, 33);
		this.tbColPreSet1.Name = "tbColPreSet1";
		this.tbColPreSet1.Size = new System.Drawing.Size(41, 21);
		this.tbColPreSet1.TabIndex = 59;
		this.label49.AutoSize = true;
		this.label49.ForeColor = System.Drawing.Color.Black;
		this.label49.Location = new System.Drawing.Point(51, 15);
		this.label49.Name = "label49";
		this.label49.Size = new System.Drawing.Size(41, 12);
		this.label49.TabIndex = 81;
		this.label49.Text = "设定值";
		this.label50.AutoSize = true;
		this.label50.ForeColor = System.Drawing.Color.Black;
		this.label50.Location = new System.Drawing.Point(256, 100);
		this.label50.Name = "label50";
		this.label50.Size = new System.Drawing.Size(41, 12);
		this.label50.TabIndex = 80;
		this.label50.Text = "ml/min";
		this.label60.AutoSize = true;
		this.label60.ForeColor = System.Drawing.Color.Black;
		this.label60.Location = new System.Drawing.Point(156, 37);
		this.label60.Name = "label60";
		this.label60.Size = new System.Drawing.Size(41, 12);
		this.label60.TabIndex = 61;
		this.label60.Text = "氢气1:";
		this.label51.AutoSize = true;
		this.label51.ForeColor = System.Drawing.Color.Black;
		this.label51.Location = new System.Drawing.Point(102, 100);
		this.label51.Name = "label51";
		this.label51.Size = new System.Drawing.Size(41, 12);
		this.label51.TabIndex = 79;
		this.label51.Text = "ml/min";
		this.tbHHSet1.Location = new System.Drawing.Point(197, 33);
		this.tbHHSet1.Name = "tbHHSet1";
		this.tbHHSet1.Size = new System.Drawing.Size(41, 21);
		this.tbHHSet1.TabIndex = 62;
		this.label52.AutoSize = true;
		this.label52.ForeColor = System.Drawing.Color.Black;
		this.label52.Location = new System.Drawing.Point(103, 68);
		this.label52.Name = "label52";
		this.label52.Size = new System.Drawing.Size(41, 12);
		this.label52.TabIndex = 69;
		this.label52.Text = "ml/min";
		this.label59.AutoSize = true;
		this.label59.ForeColor = System.Drawing.Color.Black;
		this.label59.Location = new System.Drawing.Point(156, 68);
		this.label59.Name = "label59";
		this.label59.Size = new System.Drawing.Size(41, 12);
		this.label59.TabIndex = 64;
		this.label59.Text = "空气1:";
		this.tbWeiChuiSet1.Location = new System.Drawing.Point(197, 96);
		this.tbWeiChuiSet1.Name = "tbWeiChuiSet1";
		this.tbWeiChuiSet1.Size = new System.Drawing.Size(41, 21);
		this.tbWeiChuiSet1.TabIndex = 77;
		this.tbAirSet1.Location = new System.Drawing.Point(197, 64);
		this.tbAirSet1.Name = "tbAirSet1";
		this.tbAirSet1.Size = new System.Drawing.Size(41, 21);
		this.tbAirSet1.TabIndex = 65;
		this.label53.AutoSize = true;
		this.label53.ForeColor = System.Drawing.Color.Black;
		this.label53.Location = new System.Drawing.Point(156, 100);
		this.label53.Name = "label53";
		this.label53.Size = new System.Drawing.Size(41, 12);
		this.label53.TabIndex = 76;
		this.label53.Text = "尾吹1:";
		this.label58.AutoSize = true;
		this.label58.ForeColor = System.Drawing.Color.Black;
		this.label58.Location = new System.Drawing.Point(103, 37);
		this.label58.Name = "label58";
		this.label58.Size = new System.Drawing.Size(41, 12);
		this.label58.TabIndex = 58;
		this.label58.Text = "ml/min";
		this.tbColPreSet3.Location = new System.Drawing.Point(50, 96);
		this.tbColPreSet3.Name = "tbColPreSet3";
		this.tbColPreSet3.Size = new System.Drawing.Size(41, 21);
		this.tbColPreSet3.TabIndex = 74;
		this.label57.AutoSize = true;
		this.label57.ForeColor = System.Drawing.Color.Black;
		this.label57.Location = new System.Drawing.Point(256, 37);
		this.label57.Name = "label57";
		this.label57.Size = new System.Drawing.Size(41, 12);
		this.label57.TabIndex = 67;
		this.label57.Text = "ml/min";
		this.label54.AutoSize = true;
		this.label54.ForeColor = System.Drawing.Color.Black;
		this.label54.Location = new System.Drawing.Point(9, 100);
		this.label54.Name = "label54";
		this.label54.Size = new System.Drawing.Size(41, 12);
		this.label54.TabIndex = 73;
		this.label54.Text = "载气3:";
		this.label56.AutoSize = true;
		this.label56.ForeColor = System.Drawing.Color.Black;
		this.label56.Location = new System.Drawing.Point(256, 68);
		this.label56.Name = "label56";
		this.label56.Size = new System.Drawing.Size(41, 12);
		this.label56.TabIndex = 68;
		this.label56.Text = "ml/min";
		this.label55.AutoSize = true;
		this.label55.ForeColor = System.Drawing.Color.Black;
		this.label55.Location = new System.Drawing.Point(9, 68);
		this.label55.Name = "label55";
		this.label55.Size = new System.Drawing.Size(41, 12);
		this.label55.TabIndex = 70;
		this.label55.Text = "载气2:";
		this.tbColPreSet2.Location = new System.Drawing.Point(50, 64);
		this.tbColPreSet2.Name = "tbColPreSet2";
		this.tbColPreSet2.Size = new System.Drawing.Size(41, 21);
		this.tbColPreSet2.TabIndex = 71;
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(327, 215);
		base.Controls.Add(this.groupBox15);
		base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
		base.Name = "FormEPC";
		this.Text = "FormEPC";
		this.groupBox15.ResumeLayout(false);
		this.groupBox15.PerformLayout();
		base.ResumeLayout(false);
	}
}
