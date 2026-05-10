using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using IBrainChrom2018.Unit;

namespace IBrainChrom2018;

public class FormCalibra : Form
{
	private ChromDeviceListMgr cdlMgr = ChromDeviceListMgr.Create();

	private FormMainParam frmParam = FormMainParam.Create();

	private IContainer components = null;

	private GroupBox groupBox1;

	private Button btnStartCalibra;

	private Label label3;

	private RadioButton rBCalibraChannel2;

	private RadioButton rBCalibraChannel1;

	private GroupBox groupBox2;

	private RadioButton rbPoint5;

	private RadioButton rbPoint4;

	private RadioButton rbPoint3;

	private RadioButton rbPoint2;

	private RadioButton rbPoint1;

	private RadioButton rBCalibraChannelAll;

	public FormCalibra()
	{
		InitializeComponent();
		if (frmParam.kindMachine != 0)
		{
			if (frmParam.kindMachine == 1)
			{
				rBCalibraChannel2.Visible = false;
			}
			else if (frmParam.kindMachine == 2)
			{
				rBCalibraChannel1.Text = Lang.PS("第一通道非甲烷总烃", "The first channel is non-methane total hydrocarbon");
				rBCalibraChannel2.Text = Lang.PS("第二通道非甲烷总烃", "Second channel non-methane total hydrocarbon");
			}
			else if (frmParam.kindMachine == 3)
			{
				rBCalibraChannel1.Text = Lang.PS("苯系物", "BTEX");
				rBCalibraChannel2.Visible = false;
			}
		}
		LoadLanguage();
	}

	private void LoadLanguage()
	{
		groupBox1.Text = Lang.PS("通道选择", "channel selection ");
		groupBox2.Text = Lang.PS("采样次数", "Sampling frequency ");
		rBCalibraChannel1.Text = Lang.PS("非甲烷总烃", "NMHC");
		rBCalibraChannel2.Text = Lang.PS("苯系物", "BTEX");
		rBCalibraChannelAll.Text = Lang.PS("双通道同时标定", "Calibra Two Channel");
		rbPoint1.Text = Lang.PS("一点", "one point");
		rbPoint2.Text = Lang.PS("二点", "two point");
		rbPoint3.Text = Lang.PS("三点", "three point");
		rbPoint4.Text = Lang.PS("四点", "four point");
		rbPoint5.Text = Lang.PS("五点", "five point");
		label3.Text = Lang.PS("注意：开始标定前请先接好标气", "Note: please connect the standard gas before starting calibration");
		btnStartCalibra.Text = Lang.PS("开始标定", "start calibration");
	}

	private void btnStartCalibra_Click(object sender, EventArgs e)
	{
		if (rBCalibraChannel1.Checked)
		{
			cdlMgr.formMain.IsAutoCalibra = 1;
			cdlMgr.formMain.tabChannel.SelectedIndex = 0;
			cdlMgr.formMain.tabChannel.Enabled = false;
		}
		else if (rBCalibraChannel2.Checked)
		{
			cdlMgr.formMain.IsAutoCalibra = 2;
			cdlMgr.formMain.tabChannel.SelectedIndex = 1;
			cdlMgr.formMain.tabChannel.Enabled = false;
		}
		else if (rBCalibraChannelAll.Checked)
		{
			cdlMgr.formMain.IsAutoCalibra = 3;
			cdlMgr.formMain.tabChannel.Enabled = false;
		}
		if (MessageBox.Show(Lang.PS("是否新建方法", "New method or not"), Lang.PS("提示：", "NOTE"), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
		{
			cdlMgr.formMain.MainmstSet.ReSave_();
		}
		if (rbPoint1.Checked)
		{
			cdlMgr.formMain.AutoCalibraPoint = 1;
		}
		else if (rbPoint2.Checked)
		{
			cdlMgr.formMain.AutoCalibraPoint = 2;
		}
		else if (rbPoint3.Checked)
		{
			cdlMgr.formMain.AutoCalibraPoint = 3;
		}
		else if (rbPoint4.Checked)
		{
			cdlMgr.formMain.AutoCalibraPoint = 4;
		}
		else if (rbPoint5.Checked)
		{
			cdlMgr.formMain.AutoCalibraPoint = 5;
		}
		if (Class49.user_0.ULevel == User.Level.管理员)
		{
			cdlMgr.currentTcpServerMgrSendCmd(18);
			Close();
		}
		else
		{
			MessageBox.Show(Lang.PS("没有设置仪器权限！ ", "Without permission!"));
			Close();
		}
	}

	private void tbTHCAmount_TextChanged(object sender, EventArgs e)
	{
	}

	private void tbCH4Amount_TextChanged(object sender, EventArgs e)
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(IBrainChrom2018.FormCalibra));
		this.groupBox1 = new System.Windows.Forms.GroupBox();
		this.rBCalibraChannelAll = new System.Windows.Forms.RadioButton();
		this.rBCalibraChannel2 = new System.Windows.Forms.RadioButton();
		this.rBCalibraChannel1 = new System.Windows.Forms.RadioButton();
		this.btnStartCalibra = new System.Windows.Forms.Button();
		this.label3 = new System.Windows.Forms.Label();
		this.groupBox2 = new System.Windows.Forms.GroupBox();
		this.rbPoint5 = new System.Windows.Forms.RadioButton();
		this.rbPoint4 = new System.Windows.Forms.RadioButton();
		this.rbPoint3 = new System.Windows.Forms.RadioButton();
		this.rbPoint2 = new System.Windows.Forms.RadioButton();
		this.rbPoint1 = new System.Windows.Forms.RadioButton();
		this.groupBox1.SuspendLayout();
		this.groupBox2.SuspendLayout();
		base.SuspendLayout();
		this.groupBox1.Controls.Add(this.rBCalibraChannelAll);
		this.groupBox1.Controls.Add(this.rBCalibraChannel2);
		this.groupBox1.Controls.Add(this.rBCalibraChannel1);
		this.groupBox1.Location = new System.Drawing.Point(12, 26);
		this.groupBox1.Name = "groupBox1";
		this.groupBox1.Size = new System.Drawing.Size(207, 171);
		this.groupBox1.TabIndex = 0;
		this.groupBox1.TabStop = false;
		this.groupBox1.Text = "通道选择";
		this.rBCalibraChannelAll.AutoSize = true;
		this.rBCalibraChannelAll.Location = new System.Drawing.Point(20, 95);
		this.rBCalibraChannelAll.Name = "rBCalibraChannelAll";
		this.rBCalibraChannelAll.Size = new System.Drawing.Size(173, 16);
		this.rBCalibraChannelAll.TabIndex = 3;
		this.rBCalibraChannelAll.TabStop = true;
		this.rBCalibraChannelAll.Text = "非甲烷总烃+苯系物同时标定";
		this.rBCalibraChannelAll.UseVisualStyleBackColor = true;
		this.rBCalibraChannel2.AutoSize = true;
		this.rBCalibraChannel2.Location = new System.Drawing.Point(20, 58);
		this.rBCalibraChannel2.Name = "rBCalibraChannel2";
		this.rBCalibraChannel2.Size = new System.Drawing.Size(59, 16);
		this.rBCalibraChannel2.TabIndex = 2;
		this.rBCalibraChannel2.TabStop = true;
		this.rBCalibraChannel2.Text = "苯系物";
		this.rBCalibraChannel2.UseVisualStyleBackColor = true;
		this.rBCalibraChannel1.AutoSize = true;
		this.rBCalibraChannel1.Location = new System.Drawing.Point(20, 20);
		this.rBCalibraChannel1.Name = "rBCalibraChannel1";
		this.rBCalibraChannel1.Size = new System.Drawing.Size(83, 16);
		this.rBCalibraChannel1.TabIndex = 1;
		this.rBCalibraChannel1.TabStop = true;
		this.rBCalibraChannel1.Text = "非甲烷总烃";
		this.rBCalibraChannel1.UseVisualStyleBackColor = true;
		this.btnStartCalibra.Location = new System.Drawing.Point(343, 228);
		this.btnStartCalibra.Name = "btnStartCalibra";
		this.btnStartCalibra.Size = new System.Drawing.Size(75, 23);
		this.btnStartCalibra.TabIndex = 1;
		this.btnStartCalibra.Text = "开始标定";
		this.btnStartCalibra.UseVisualStyleBackColor = true;
		this.btnStartCalibra.Click += new System.EventHandler(btnStartCalibra_Click);
		this.label3.AutoSize = true;
		this.label3.Location = new System.Drawing.Point(30, 264);
		this.label3.Name = "label3";
		this.label3.Size = new System.Drawing.Size(173, 12);
		this.label3.TabIndex = 2;
		this.label3.Text = "注意：开始标定前请先接好标气";
		this.groupBox2.Controls.Add(this.rbPoint5);
		this.groupBox2.Controls.Add(this.rbPoint4);
		this.groupBox2.Controls.Add(this.rbPoint3);
		this.groupBox2.Controls.Add(this.rbPoint2);
		this.groupBox2.Controls.Add(this.rbPoint1);
		this.groupBox2.Location = new System.Drawing.Point(225, 26);
		this.groupBox2.Name = "groupBox2";
		this.groupBox2.Size = new System.Drawing.Size(211, 171);
		this.groupBox2.TabIndex = 3;
		this.groupBox2.TabStop = false;
		this.groupBox2.Text = "采样次数";
		this.rbPoint5.AutoSize = true;
		this.rbPoint5.Enabled = false;
		this.rbPoint5.Location = new System.Drawing.Point(20, 126);
		this.rbPoint5.Name = "rbPoint5";
		this.rbPoint5.Size = new System.Drawing.Size(47, 16);
		this.rbPoint5.TabIndex = 5;
		this.rbPoint5.TabStop = true;
		this.rbPoint5.Text = "五点";
		this.rbPoint5.UseVisualStyleBackColor = true;
		this.rbPoint4.AutoSize = true;
		this.rbPoint4.Enabled = false;
		this.rbPoint4.Location = new System.Drawing.Point(20, 95);
		this.rbPoint4.Name = "rbPoint4";
		this.rbPoint4.Size = new System.Drawing.Size(47, 16);
		this.rbPoint4.TabIndex = 4;
		this.rbPoint4.TabStop = true;
		this.rbPoint4.Text = "四点";
		this.rbPoint4.UseVisualStyleBackColor = true;
		this.rbPoint3.AutoSize = true;
		this.rbPoint3.Enabled = false;
		this.rbPoint3.Location = new System.Drawing.Point(20, 70);
		this.rbPoint3.Name = "rbPoint3";
		this.rbPoint3.Size = new System.Drawing.Size(47, 16);
		this.rbPoint3.TabIndex = 3;
		this.rbPoint3.TabStop = true;
		this.rbPoint3.Text = "三点";
		this.rbPoint3.UseVisualStyleBackColor = true;
		this.rbPoint2.AutoSize = true;
		this.rbPoint2.Enabled = false;
		this.rbPoint2.Location = new System.Drawing.Point(20, 45);
		this.rbPoint2.Name = "rbPoint2";
		this.rbPoint2.Size = new System.Drawing.Size(47, 16);
		this.rbPoint2.TabIndex = 2;
		this.rbPoint2.TabStop = true;
		this.rbPoint2.Text = "两点";
		this.rbPoint2.UseVisualStyleBackColor = true;
		this.rbPoint1.AutoSize = true;
		this.rbPoint1.Checked = true;
		this.rbPoint1.Location = new System.Drawing.Point(20, 20);
		this.rbPoint1.Name = "rbPoint1";
		this.rbPoint1.Size = new System.Drawing.Size(47, 16);
		this.rbPoint1.TabIndex = 1;
		this.rbPoint1.TabStop = true;
		this.rbPoint1.Text = "一点";
		this.rbPoint1.UseVisualStyleBackColor = true;
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(455, 299);
		base.Controls.Add(this.groupBox2);
		base.Controls.Add(this.label3);
		base.Controls.Add(this.btnStartCalibra);
		base.Controls.Add(this.groupBox1);
		base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
		base.Name = "FormCalibra";
		this.Text = "FormCalibra";
		this.groupBox1.ResumeLayout(false);
		this.groupBox1.PerformLayout();
		this.groupBox2.ResumeLayout(false);
		this.groupBox2.PerformLayout();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
