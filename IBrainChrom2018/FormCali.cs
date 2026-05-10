using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace IBrainChrom2018;

public class FormCali : Form
{
	private ChromDeviceListMgr cdlMgr = ChromDeviceListMgr.Create();

	private IContainer components = null;

	private Button btnCali1;

	private Button btnCali2;

	private Button btnCaliAll;

	public FormCali()
	{
		InitializeComponent();
	}

	private void btnCali1_Click(object sender, EventArgs e)
	{
		if (cdlMgr.CurrentTcpServerSocket != null)
		{
			if (cdlMgr.CurrentTcpServerSocket.sglsSampling[0].simple)
			{
				MessageBox.Show(Lang.PS("样品正在采集，请等待样品结束！", "Samples are being collected, please wait for the end of samples!"));
				return;
			}
			if (Class49.user_0.ULevel == User.Level.管理员)
			{
				byte[] dataBuff = new byte[26]
				{
					71, 67, 75, 67, 0, 0, 0, 0, 0, 0,
					0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
					0, 0, 0, 0, 22, 0
				};
				if (cdlMgr.CurrentTcpServerSocket != null && !cdlMgr.CurrentTcpServerSocket.sglsSampling[0].simple)
				{
					cdlMgr.CurrentTcpServerSocket.SendData(dataBuff);
				}
				if (VocCtrl.vocCtrl != null)
				{
					VocCtrl.vocCtrl.bCalibration = true;
				}
			}
		}
		Close();
	}

	private void btnCali2_Click(object sender, EventArgs e)
	{
		if (cdlMgr.CurrentTcpServerSocket != null)
		{
			if (cdlMgr.CurrentTcpServerSocket.sglsSampling[1].simple)
			{
				MessageBox.Show(Lang.PS("样品正在采集，请等待样品结束！", "Samples are being collected, please wait for the end of samples!"));
				return;
			}
			if (Class49.user_0.ULevel == User.Level.管理员)
			{
				byte[] dataBuff = new byte[26]
				{
					71, 67, 75, 67, 0, 0, 0, 0, 0, 0,
					0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
					0, 0, 0, 0, 22, 1
				};
				cdlMgr.CurrentTcpServerSocket.SendData(dataBuff);
				if (VocCtrl.vocCtrl != null)
				{
					VocCtrl.vocCtrl.bCalibration2 = true;
				}
			}
		}
		Close();
	}

	private void btnCaliAll_Click(object sender, EventArgs e)
	{
		if (cdlMgr.CurrentTcpServerSocket != null)
		{
			if (cdlMgr.CurrentTcpServerSocket.sglsSampling[1].simple || cdlMgr.CurrentTcpServerSocket.sglsSampling[1].simple)
			{
				MessageBox.Show(Lang.PS("样品正在采集，请等待样品结束！", "Samples are being collected, please wait for the end of samples!"));
				return;
			}
			if (Class49.user_0.ULevel == User.Level.管理员)
			{
				cdlMgr.currentTcpServerMgrSendCmd(18);
				if (VocCtrl.vocCtrl != null)
				{
					VocCtrl.vocCtrl.bCalibration = true;
					VocCtrl.vocCtrl.bCalibration2 = true;
				}
			}
		}
		Close();
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(IBrainChrom2018.FormCali));
		this.btnCali1 = new System.Windows.Forms.Button();
		this.btnCali2 = new System.Windows.Forms.Button();
		this.btnCaliAll = new System.Windows.Forms.Button();
		base.SuspendLayout();
		this.btnCali1.Location = new System.Drawing.Point(61, 38);
		this.btnCali1.Name = "btnCali1";
		this.btnCali1.Size = new System.Drawing.Size(138, 47);
		this.btnCali1.TabIndex = 0;
		this.btnCali1.Text = "标定通道1";
		this.btnCali1.UseVisualStyleBackColor = true;
		this.btnCali1.Click += new System.EventHandler(btnCali1_Click);
		this.btnCali2.Location = new System.Drawing.Point(61, 91);
		this.btnCali2.Name = "btnCali2";
		this.btnCali2.Size = new System.Drawing.Size(138, 47);
		this.btnCali2.TabIndex = 1;
		this.btnCali2.Text = "标定通道2";
		this.btnCali2.UseVisualStyleBackColor = true;
		this.btnCali2.Click += new System.EventHandler(btnCali2_Click);
		this.btnCaliAll.Location = new System.Drawing.Point(61, 144);
		this.btnCaliAll.Name = "btnCaliAll";
		this.btnCaliAll.Size = new System.Drawing.Size(138, 47);
		this.btnCaliAll.TabIndex = 2;
		this.btnCaliAll.Text = "通道1和通道2同时标定";
		this.btnCaliAll.UseVisualStyleBackColor = true;
		this.btnCaliAll.Click += new System.EventHandler(btnCaliAll_Click);
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(264, 235);
		base.Controls.Add(this.btnCaliAll);
		base.Controls.Add(this.btnCali2);
		base.Controls.Add(this.btnCali1);
		base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
		base.Name = "FormCali";
		this.Text = "FormCali";
		base.ResumeLayout(false);
	}
}
