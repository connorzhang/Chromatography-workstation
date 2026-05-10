using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using IBrainChrom2018.Properties;
using IBrainChrom2018.Unit;

namespace IBrainChrom2018;

public class TVOCCtrl : UserControl
{
	public bool bLoading = true;

	private FormMainParam frmParam = FormMainParam.Create();

	private IContainer components = null;

	private Label label1;

	private TextBox tbTemp;

	private Label label2;

	private Label label3;

	private TextBox tbAtm;

	private Label label4;

	private TextBox tbCoef;

	private Label label5;

	private TextBox tbInjectionVolume;

	private Label label7;

	private Label label6;

	private GroupBox gbO;

	private Button sdaOpen;

	private TextBox tbSdaFileName;

	public TVOCCtrl()
	{
		InitializeComponent();
		initForm();
		bLoading = false;
	}

	public void initForm()
	{
		Class49.fAtm = frmParam.fAtm;
		Class49.fTemp = frmParam.fTemp;
		Class49.fInjectionVolume = frmParam.fInjectionVolume;
		tbAtm.Text = frmParam.fAtm.ToString();
		tbTemp.Text = frmParam.fTemp.ToString();
		tbInjectionVolume.Text = frmParam.fInjectionVolume.ToString();
		tbCoef.Text = (101.3f * (frmParam.fTemp + 273f) / (frmParam.fAtm * 273f)).ToString();
	}

	private void tbTemp_TextChanged(object sender, EventArgs e)
	{
		if (!bLoading)
		{
			float.TryParse(tbTemp.Text.Trim(), out frmParam.fTemp);
			Class49.fTemp = frmParam.fTemp;
			frmParam.SaveParam();
			float num = 101.3f * (frmParam.fTemp + 273f);
			float num2 = frmParam.fAtm * 273f;
			tbCoef.Text = (num / num2).ToString();
		}
	}

	private void tbAtm_TextChanged(object sender, EventArgs e)
	{
		if (!bLoading)
		{
			float.TryParse(tbAtm.Text.Trim(), out frmParam.fAtm);
			Class49.fAtm = frmParam.fAtm;
			frmParam.SaveParam();
			tbCoef.Text = (101.3f * (frmParam.fTemp + 273f) / (frmParam.fAtm * 273f)).ToString();
		}
	}

	private void tbInjectionVolume_TextChanged(object sender, EventArgs e)
	{
		if (!bLoading)
		{
			float.TryParse(tbInjectionVolume.Text.Trim(), out frmParam.fInjectionVolume);
			Class49.fInjectionVolume = frmParam.fInjectionVolume;
			frmParam.SaveParam();
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
		this.label1 = new System.Windows.Forms.Label();
		this.tbTemp = new System.Windows.Forms.TextBox();
		this.label2 = new System.Windows.Forms.Label();
		this.label3 = new System.Windows.Forms.Label();
		this.tbAtm = new System.Windows.Forms.TextBox();
		this.label4 = new System.Windows.Forms.Label();
		this.tbCoef = new System.Windows.Forms.TextBox();
		this.label5 = new System.Windows.Forms.Label();
		this.tbInjectionVolume = new System.Windows.Forms.TextBox();
		this.label7 = new System.Windows.Forms.Label();
		this.label6 = new System.Windows.Forms.Label();
		this.gbO = new System.Windows.Forms.GroupBox();
		this.sdaOpen = new System.Windows.Forms.Button();
		this.tbSdaFileName = new System.Windows.Forms.TextBox();
		this.gbO.SuspendLayout();
		base.SuspendLayout();
		this.label1.AutoSize = true;
		this.label1.Location = new System.Drawing.Point(101, 30);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(71, 12);
		this.label1.TabIndex = 0;
		this.label1.Text = "采样点温度:";
		this.tbTemp.Location = new System.Drawing.Point(176, 26);
		this.tbTemp.Name = "tbTemp";
		this.tbTemp.Size = new System.Drawing.Size(100, 21);
		this.tbTemp.TabIndex = 1;
		this.tbTemp.Text = "0";
		this.tbTemp.TextChanged += new System.EventHandler(tbTemp_TextChanged);
		this.label2.AutoSize = true;
		this.label2.Location = new System.Drawing.Point(299, 30);
		this.label2.Name = "label2";
		this.label2.Size = new System.Drawing.Size(17, 12);
		this.label2.TabIndex = 2;
		this.label2.Text = "℃";
		this.label3.AutoSize = true;
		this.label3.Location = new System.Drawing.Point(299, 69);
		this.label3.Name = "label3";
		this.label3.Size = new System.Drawing.Size(23, 12);
		this.label3.TabIndex = 5;
		this.label3.Text = "KPa";
		this.tbAtm.Location = new System.Drawing.Point(176, 65);
		this.tbAtm.Name = "tbAtm";
		this.tbAtm.Size = new System.Drawing.Size(100, 21);
		this.tbAtm.TabIndex = 4;
		this.tbAtm.Text = "101.3";
		this.tbAtm.TextChanged += new System.EventHandler(tbAtm_TextChanged);
		this.label4.AutoSize = true;
		this.label4.Location = new System.Drawing.Point(89, 69);
		this.label4.Name = "label4";
		this.label4.Size = new System.Drawing.Size(83, 12);
		this.label4.TabIndex = 3;
		this.label4.Text = "采样点大气压:";
		this.tbCoef.Location = new System.Drawing.Point(176, 102);
		this.tbCoef.Name = "tbCoef";
		this.tbCoef.ReadOnly = true;
		this.tbCoef.Size = new System.Drawing.Size(100, 21);
		this.tbCoef.TabIndex = 7;
		this.tbCoef.Text = "1";
		this.label5.AutoSize = true;
		this.label5.Location = new System.Drawing.Point(65, 106);
		this.label5.Name = "label5";
		this.label5.Size = new System.Drawing.Size(107, 12);
		this.label5.TabIndex = 6;
		this.label5.Text = "标准状态换算系数:";
		this.tbInjectionVolume.Location = new System.Drawing.Point(433, 26);
		this.tbInjectionVolume.Name = "tbInjectionVolume";
		this.tbInjectionVolume.Size = new System.Drawing.Size(100, 21);
		this.tbInjectionVolume.TabIndex = 9;
		this.tbInjectionVolume.Text = "0";
		this.tbInjectionVolume.TextChanged += new System.EventHandler(tbInjectionVolume_TextChanged);
		this.label7.AutoSize = true;
		this.label7.Location = new System.Drawing.Point(368, 30);
		this.label7.Name = "label7";
		this.label7.Size = new System.Drawing.Size(59, 12);
		this.label7.TabIndex = 8;
		this.label7.Text = "采样体积:";
		this.label6.AutoSize = true;
		this.label6.Location = new System.Drawing.Point(549, 30);
		this.label6.Name = "label6";
		this.label6.Size = new System.Drawing.Size(11, 12);
		this.label6.TabIndex = 10;
		this.label6.Text = "L";
		this.gbO.Controls.Add(this.sdaOpen);
		this.gbO.Controls.Add(this.tbSdaFileName);
		this.gbO.Location = new System.Drawing.Point(370, 77);
		this.gbO.Name = "gbO";
		this.gbO.Size = new System.Drawing.Size(317, 46);
		this.gbO.TabIndex = 14;
		this.gbO.TabStop = false;
		this.gbO.Text = "空白谱图";
		this.gbO.Visible = false;
		this.sdaOpen.Image = IBrainChrom2018.Properties.Resources.FolderOpen;
		this.sdaOpen.Location = new System.Drawing.Point(266, 19);
		this.sdaOpen.Name = "sdaOpen";
		this.sdaOpen.Size = new System.Drawing.Size(45, 22);
		this.sdaOpen.TabIndex = 31;
		this.sdaOpen.UseVisualStyleBackColor = true;
		this.tbSdaFileName.Location = new System.Drawing.Point(4, 19);
		this.tbSdaFileName.Name = "tbSdaFileName";
		this.tbSdaFileName.Size = new System.Drawing.Size(248, 21);
		this.tbSdaFileName.TabIndex = 5;
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.Controls.Add(this.gbO);
		base.Controls.Add(this.label6);
		base.Controls.Add(this.tbInjectionVolume);
		base.Controls.Add(this.label7);
		base.Controls.Add(this.tbCoef);
		base.Controls.Add(this.label5);
		base.Controls.Add(this.label3);
		base.Controls.Add(this.tbAtm);
		base.Controls.Add(this.label4);
		base.Controls.Add(this.label2);
		base.Controls.Add(this.tbTemp);
		base.Controls.Add(this.label1);
		base.Name = "TVOCCtrl";
		base.Size = new System.Drawing.Size(980, 158);
		this.gbO.ResumeLayout(false);
		this.gbO.PerformLayout();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
