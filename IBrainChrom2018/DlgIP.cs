using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Management;
using System.Windows.Forms;

namespace IBrainChrom2018;

public class DlgIP : LclDialog
{
	private LclButton btnDetect;

	private IContainer icontainer_1;

	private GroupBox groupBox1;

	private Label ipExt;

	private Label ipLcl;

	private Label label1;

	private Label label3;

	private LinkLabel llbGate;

	private TextBox textBox_0;

	public DlgIP()
	{
		InitializeComponent_1();
		Text = Lang.PS("IP查询", "IP Query");
		btnDetect.Text = Lang.PS("检测", "Detect");
		ipExt.Text = (ipLcl.Text = "[未检测]");
		string text2 = "工作站方作为服务器，仪器方作为客户，服务器需配置路由：" + '\r' + '\n' + "1，点击上面的超链接，配置路由器：" + '\r' + '\n' + "     <一般默认用户名 admin 密码 admin>" + '\r' + '\n' + "2，在[转发规则]－[虚拟服务器]项目添加一条记录：" + '\r' + '\n' + "     本机IP(局域IP)、端口(25001和8000)、协议(TCP)" + '\r' + '\n' + "3，启动工作站登录气相，防火墙若询问请“允许”" + '\r' + '\n' + "4，[将自己的“广域IP”告知仪器，仪器主动连接]";
		textBox_0.Text = text2;
	}

	private void btnDetect_Click(object sender, EventArgs e)
	{
		btnDetect.Text = Lang.PS("检测中", "Detect...");
		ipLcl.Text = IPAddressParse.GetLocalIPAddress();
		ipExt.Text = IPAddressParse.smethod_1("http://www.skyiv.com/info");
		btnDetect.Text = Lang.PS("检测", "Detect");
	}

	protected override void Dispose(bool disposing)
	{
		if (disposing && icontainer_1 != null)
		{
			icontainer_1.Dispose();
		}
		base.Dispose(disposing);
	}

	private void DlgIP_Load(object sender, EventArgs e)
	{
		ManagementObjectCollection instances = new ManagementClass("Win32_NetworkAdapterConfiguration").GetInstances();
		string text = "0.0.0.0";
		foreach (ManagementObject item in instances)
		{
			if (Convert.ToBoolean(item["IPEnabled"]) && (item["DefaultIPGateway"] as string[]).Length != 0 && text == "0.0.0.0")
			{
				text = (item["DefaultIPGateway"] as string[])[0];
				break;
			}
		}
		llbGate.Text = "http://" + text;
	}

	private void InitializeComponent_1()
	{
		groupBox1 = new GroupBox();
		ipLcl = new Label();
		ipExt = new Label();
		label3 = new Label();
		label1 = new Label();
		btnDetect = new LclButton();
		llbGate = new LinkLabel();
		textBox_0 = new TextBox();
		groupBox1.SuspendLayout();
		SuspendLayout();
		btnCancel.Location = new Point(81, 208);
		btnCancel.Text = "取消";
		btnCancel.Visible = false;
		btnHelp.Location = new Point(0, 208);
		btnHelp.Text = "帮助";
		btnHelp.Visible = false;
		btnOK.Location = new Point(260, 208);
		btnOK.Text = "确认";
		btnOK.Click += method_0;
		groupBox1.Controls.Add(ipLcl);
		groupBox1.Controls.Add(ipExt);
		groupBox1.Controls.Add(label3);
		groupBox1.Controls.Add(label1);
		groupBox1.Location = new Point(12, 12);
		groupBox1.Name = "groupBox1";
		groupBox1.Size = new Size(177, 50);
		groupBox1.TabIndex = 4;
		groupBox1.TabStop = false;
		groupBox1.Text = "IP地址 <告知仪器 IP>";
		ipLcl.AutoSize = true;
		ipLcl.Location = new Point(57, 33);
		ipLcl.Name = "ipLcl";
		ipLcl.Size = new Size(41, 12);
		ipLcl.TabIndex = 2;
		ipLcl.Text = "label1";
		ipExt.AutoSize = true;
		ipExt.Location = new Point(57, 17);
		ipExt.Name = "ipExt";
		ipExt.Size = new Size(41, 12);
		ipExt.TabIndex = 2;
		ipExt.Text = "label1";
		label3.AutoSize = true;
		label3.Location = new Point(10, 33);
		label3.Name = "label3";
		label3.Size = new Size(53, 12);
		label3.TabIndex = 2;
		label3.Text = "局域IP：";
		label1.AutoSize = true;
		label1.Location = new Point(10, 17);
		label1.Name = "label1";
		label1.Size = new Size(53, 12);
		label1.TabIndex = 2;
		label1.Text = "广域IP：";
		btnDetect.Location = new Point(260, 24);
		btnDetect.Name = "btnDetect";
		btnDetect.Size = new Size(75, 23);
		btnDetect.TabIndex = 5;
		btnDetect.Text = "lclButton1";
		btnDetect.UseVisualStyleBackColor = true;
		btnDetect.Click += btnDetect_Click;
		llbGate.AutoSize = true;
		llbGate.Location = new Point(10, 69);
		llbGate.Name = "llbGate";
		llbGate.Size = new Size(65, 12);
		llbGate.TabIndex = 9;
		llbGate.TabStop = true;
		llbGate.Text = "linkLabel1";
		llbGate.TextAlign = ContentAlignment.MiddleRight;
		llbGate.LinkClicked += llbGate_LinkClicked;
		textBox_0.Location = new Point(0, 84);
		textBox_0.Multiline = true;
		textBox_0.Name = "tb1";
		textBox_0.ReadOnly = true;
		textBox_0.Size = new Size(346, 96);
		textBox_0.TabIndex = 10;
		base.ClientSize = new Size(347, 245);
		base.Controls.Add(textBox_0);
		base.Controls.Add(llbGate);
		base.Controls.Add(btnDetect);
		base.Controls.Add(groupBox1);
		base.Name = "DlgIP";
		Text = "IP";
		base.Load += DlgIP_Load;
		base.Controls.SetChildIndex(btnOK, 0);
		base.Controls.SetChildIndex(btnHelp, 0);
		base.Controls.SetChildIndex(btnCancel, 0);
		base.Controls.SetChildIndex(groupBox1, 0);
		base.Controls.SetChildIndex(btnDetect, 0);
		base.Controls.SetChildIndex(llbGate, 0);
		base.Controls.SetChildIndex(textBox_0, 0);
		groupBox1.ResumeLayout(performLayout: false);
		groupBox1.PerformLayout();
		ResumeLayout(performLayout: false);
		PerformLayout();
	}

	private void llbGate_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
	{
		Process.Start("IEXPLORE.EXE", (sender as LinkLabel).Text);
	}

	private void method_0(object sender, EventArgs e)
	{
	}
}
