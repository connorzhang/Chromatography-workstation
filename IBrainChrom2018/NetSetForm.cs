using System;
using System.ComponentModel;
using System.Drawing;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace IBrainChrom2018;

public class NetSetForm : Form
{
	private IContainer components = null;

	public MaskedTextBox IP4;

	private Label label1;

	private Button btnNetSet;

	private Label label2;

	public NetSetForm()
	{
		InitializeComponent();
		IPHostEntry hostEntry = Dns.GetHostEntry(Dns.GetHostName());
		Regex regex = new Regex("^[1-2]\\d+\\.(\\d+.){2}(([1-2](\\d){2})|((\\d){2})|\\d)");
		if (hostEntry.AddressList == null || hostEntry.AddressList.Length == 0)
		{
			return;
		}
		for (int i = 0; i < hostEntry.AddressList.Length; i++)
		{
			Match match = regex.Match(hostEntry.AddressList[i].ToString());
			if (match.Success)
			{
				IP4.Text = match.Groups[0].Value;
				break;
			}
		}
	}

	private string ipAddressFormat(string ipAddress)
	{
		Regex regex = new Regex("^[1-2]\\d+\\.(\\d+.){2}(([1-2](\\d){2})|((\\d){2})|\\d)");
		string text = "000000000000";
		Match match = regex.Match(ipAddress);
		if (match.Success)
		{
			int num = 0;
			string text2 = "";
			while (num >= 0)
			{
				num = ipAddress.IndexOf(".");
				if (num >= 0)
				{
					text2 = ipAddress.Substring(0, num);
					ipAddress = ipAddress.Substring(num + 1);
					if (!string.IsNullOrEmpty(text2))
					{
						text2 = "00" + text2;
					}
					text += text2.Substring(text2.Length - 3, 3);
				}
			}
			if (!string.IsNullOrEmpty(ipAddress))
			{
				text2 = "00" + ipAddress;
				text += text2.Substring(text2.Length - 3, 3);
			}
		}
		return text.Substring(text.Length - 12, 12);
	}

	private void btnNetSet_Click(object sender, EventArgs e)
	{
		try
		{
			string ipAddress = IP4.Text;
			string ipAddress2 = "127.0.0.1";
			string ipAddress3 = "127.0.0.1";
			string text = ipAddressFormat(ipAddress);
			text += ipAddressFormat(ipAddress2);
			text += ipAddressFormat(ipAddress3);
			UdpClient udpClient = new UdpClient(new IPEndPoint(IPAddress.Any, 0));
			IPEndPoint endPoint = new IPEndPoint(IPAddress.Parse("255.255.255.255"), 4800);
			string text2 = "*#*#";
			byte[] bytes = Encoding.Default.GetBytes(text2 + text);
			udpClient.Send(bytes, bytes.Length, endPoint);
		}
		catch (Exception)
		{
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(IBrainChrom2018.NetSetForm));
		this.IP4 = new System.Windows.Forms.MaskedTextBox();
		this.label1 = new System.Windows.Forms.Label();
		this.btnNetSet = new System.Windows.Forms.Button();
		this.label2 = new System.Windows.Forms.Label();
		base.SuspendLayout();
		this.IP4.Font = new System.Drawing.Font("宋体", 9f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 134);
		this.IP4.ForeColor = System.Drawing.Color.FromArgb(192, 0, 0);
		this.IP4.Location = new System.Drawing.Point(152, 78);
		this.IP4.Name = "IP4";
		this.IP4.Size = new System.Drawing.Size(131, 21);
		this.IP4.TabIndex = 2;
		this.IP4.Text = "127.0.0.1";
		this.label1.AutoSize = true;
		this.label1.Location = new System.Drawing.Point(69, 81);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(77, 12);
		this.label1.TabIndex = 3;
		this.label1.Text = IBrainChrom2018.Lang.PS("本机IP地址", "Native IP");
		this.btnNetSet.Location = new System.Drawing.Point(282, 159);
		this.btnNetSet.Name = "btnNetSet";
		this.btnNetSet.Size = new System.Drawing.Size(66, 28);
		this.btnNetSet.TabIndex = 4;
		this.btnNetSet.Text = IBrainChrom2018.Lang.PS("搜索", "search");
		this.btnNetSet.UseVisualStyleBackColor = true;
		this.btnNetSet.Click += new System.EventHandler(btnNetSet_Click);
		this.label2.AutoSize = true;
		this.label2.Location = new System.Drawing.Point(69, 117);
		this.label2.Name = "label2";
		this.label2.Size = new System.Drawing.Size(329, 12);
		this.label2.TabIndex = 5;
		this.label2.Text = IBrainChrom2018.Lang.PS("注：请检查此IP地址是否和电脑接色谱仪的网卡的IP地址一致", "Note: please check if this IP address is consistent with the IP address of the computer network card connected to the chromatograph");
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(443, 263);
		base.Controls.Add(this.label2);
		base.Controls.Add(this.btnNetSet);
		base.Controls.Add(this.label1);
		base.Controls.Add(this.IP4);
		base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
		base.Name = "NetSetForm";
		this.Text = "NetSetForm";
		base.TopMost = true;
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
