using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using IBrainChrom2018.Unit;

namespace IBrainChrom2018;

public class tabRltCtrl : UserControl
{
	public static tabRltCtrl selfCtrl;

	private FormMainParam frmParam = FormMainParam.Create();

	public bool bLoading = true;

	private IContainer components = null;

	private Label label1;

	private Label label2;

	private Label label3;

	public TextBox tbTHC;

	public TextBox tbCH4;

	public TextBox tbBen1;

	public TextBox tbBen2;

	public TextBox tbBen3;

	public TextBox tbBen4;

	public TextBox tbNMHC;

	private Label label8;

	public TextBox tbBen5;

	public TextBox tbTimeCycle2;

	private Label label10;

	private Label label11;

	public TextBox tbTimeCycle1;

	private Label label12;

	private Label label13;

	private CheckBox chbCycle;

	private CheckBox chbCycle2;

	private Label label14;

	private Label label15;

	public TextBox tbPowerOnDelay;

	public Panel panBen;

	public Panel panChannel;

	public Label label4;

	public Label label5;

	public Label label6;

	public Label label7;

	public Label label9;

	public tabRltCtrl()
	{
		selfCtrl = this;
		InitializeComponent();
		initForm();
		bLoading = false;
	}

	public void initForm()
	{
		tbTimeCycle1.Text = frmParam.fTabChannel1.ToString("F" + Class49.int_8);
		tbTimeCycle2.Text = frmParam.fTabChannel2.ToString("F" + Class49.int_8);
		chbCycle.Checked = frmParam.bCycle;
		chbCycle2.Checked = frmParam.bCycle2;
		tbPowerOnDelay.Text = frmParam.fPowerOnDealy.ToString("F" + Class49.int_8);
		panBen.Visible = true;
	}

	private void tbTimeCycle1_TextChanged(object sender, EventArgs e)
	{
		if (!bLoading)
		{
			float.TryParse(tbTimeCycle1.Text, out frmParam.fTabChannel1);
			frmParam.SaveParam();
		}
	}

	private void tbTimeCycle2_TextChanged(object sender, EventArgs e)
	{
		if (!bLoading)
		{
			float.TryParse(tbTimeCycle2.Text, out frmParam.fTabChannel2);
			frmParam.SaveParam();
		}
	}

	private void chbCycle_CheckedChanged(object sender, EventArgs e)
	{
		if (!bLoading)
		{
			frmParam.bCycle = chbCycle.Checked;
			frmParam.SaveParam();
		}
	}

	private void chbCycle2_CheckedChanged(object sender, EventArgs e)
	{
		if (!bLoading)
		{
			frmParam.bCycle2 = chbCycle2.Checked;
			frmParam.SaveParam();
		}
	}

	private void tbPowerOnDelay_TextChanged(object sender, EventArgs e)
	{
		if (!bLoading)
		{
			float.TryParse(tbPowerOnDelay.Text, out frmParam.fPowerOnDealy);
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
		this.label2 = new System.Windows.Forms.Label();
		this.label3 = new System.Windows.Forms.Label();
		this.label4 = new System.Windows.Forms.Label();
		this.label5 = new System.Windows.Forms.Label();
		this.label6 = new System.Windows.Forms.Label();
		this.label7 = new System.Windows.Forms.Label();
		this.tbTHC = new System.Windows.Forms.TextBox();
		this.tbCH4 = new System.Windows.Forms.TextBox();
		this.tbBen1 = new System.Windows.Forms.TextBox();
		this.tbBen2 = new System.Windows.Forms.TextBox();
		this.tbBen3 = new System.Windows.Forms.TextBox();
		this.tbBen4 = new System.Windows.Forms.TextBox();
		this.tbNMHC = new System.Windows.Forms.TextBox();
		this.label8 = new System.Windows.Forms.Label();
		this.tbBen5 = new System.Windows.Forms.TextBox();
		this.label9 = new System.Windows.Forms.Label();
		this.tbTimeCycle2 = new System.Windows.Forms.TextBox();
		this.label10 = new System.Windows.Forms.Label();
		this.label11 = new System.Windows.Forms.Label();
		this.tbTimeCycle1 = new System.Windows.Forms.TextBox();
		this.label12 = new System.Windows.Forms.Label();
		this.label13 = new System.Windows.Forms.Label();
		this.chbCycle = new System.Windows.Forms.CheckBox();
		this.chbCycle2 = new System.Windows.Forms.CheckBox();
		this.label14 = new System.Windows.Forms.Label();
		this.label15 = new System.Windows.Forms.Label();
		this.tbPowerOnDelay = new System.Windows.Forms.TextBox();
		this.panBen = new System.Windows.Forms.Panel();
		this.panChannel = new System.Windows.Forms.Panel();
		this.panBen.SuspendLayout();
		this.panChannel.SuspendLayout();
		base.SuspendLayout();
		this.label1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
		this.label1.Font = new System.Drawing.Font("宋体", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		this.label1.Location = new System.Drawing.Point(3, 24);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(364, 24);
		this.label1.TabIndex = 0;
		this.label1.Text = "浓度(mgc/m³)";
		this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
		this.label2.AutoSize = true;
		this.label2.Font = new System.Drawing.Font("宋体", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		this.label2.Location = new System.Drawing.Point(36, 104);
		this.label2.Name = "label2";
		this.label2.Size = new System.Drawing.Size(40, 16);
		this.label2.TabIndex = 1;
		this.label2.Text = "总烃";
		this.label3.AutoSize = true;
		this.label3.Font = new System.Drawing.Font("宋体", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		this.label3.Location = new System.Drawing.Point(36, 134);
		this.label3.Name = "label3";
		this.label3.Size = new System.Drawing.Size(40, 16);
		this.label3.TabIndex = 2;
		this.label3.Text = "甲烷";
		this.label4.AutoSize = true;
		this.label4.Font = new System.Drawing.Font("宋体", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		this.label4.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.label4.Location = new System.Drawing.Point(18, 14);
		this.label4.Name = "label4";
		this.label4.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.label4.Size = new System.Drawing.Size(24, 16);
		this.label4.TabIndex = 3;
		this.label4.Text = "苯";
		this.label5.AutoSize = true;
		this.label5.Font = new System.Drawing.Font("宋体", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		this.label5.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.label5.Location = new System.Drawing.Point(18, 104);
		this.label5.Name = "label5";
		this.label5.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.label5.Size = new System.Drawing.Size(72, 16);
		this.label5.TabIndex = 6;
		this.label5.Text = "邻二甲苯";
		this.label6.AutoSize = true;
		this.label6.Font = new System.Drawing.Font("宋体", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		this.label6.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.label6.Location = new System.Drawing.Point(18, 74);
		this.label6.Name = "label6";
		this.label6.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.label6.Size = new System.Drawing.Size(88, 16);
		this.label6.TabIndex = 5;
		this.label6.Text = "间对二甲苯";
		this.label6.TextAlign = System.Drawing.ContentAlignment.TopRight;
		this.label7.AutoSize = true;
		this.label7.Font = new System.Drawing.Font("宋体", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		this.label7.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.label7.Location = new System.Drawing.Point(18, 44);
		this.label7.Name = "label7";
		this.label7.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.label7.Size = new System.Drawing.Size(40, 16);
		this.label7.TabIndex = 4;
		this.label7.Text = "甲苯";
		this.tbTHC.BackColor = System.Drawing.SystemColors.MenuText;
		this.tbTHC.Font = new System.Drawing.Font("宋体", 10.5f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		this.tbTHC.ForeColor = System.Drawing.Color.MediumSpringGreen;
		this.tbTHC.Location = new System.Drawing.Point(163, 99);
		this.tbTHC.Name = "tbTHC";
		this.tbTHC.Size = new System.Drawing.Size(111, 23);
		this.tbTHC.TabIndex = 7;
		this.tbTHC.Text = "0.00";
		this.tbCH4.BackColor = System.Drawing.SystemColors.MenuText;
		this.tbCH4.Font = new System.Drawing.Font("宋体", 10.5f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		this.tbCH4.ForeColor = System.Drawing.Color.MediumSpringGreen;
		this.tbCH4.Location = new System.Drawing.Point(163, 128);
		this.tbCH4.Name = "tbCH4";
		this.tbCH4.Size = new System.Drawing.Size(111, 23);
		this.tbCH4.TabIndex = 8;
		this.tbCH4.Text = "0.00";
		this.tbBen1.BackColor = System.Drawing.SystemColors.MenuText;
		this.tbBen1.Font = new System.Drawing.Font("宋体", 10.5f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		this.tbBen1.ForeColor = System.Drawing.Color.MediumSpringGreen;
		this.tbBen1.Location = new System.Drawing.Point(142, 7);
		this.tbBen1.Name = "tbBen1";
		this.tbBen1.Size = new System.Drawing.Size(111, 23);
		this.tbBen1.TabIndex = 9;
		this.tbBen1.Text = "0.00";
		this.tbBen2.BackColor = System.Drawing.SystemColors.MenuText;
		this.tbBen2.Font = new System.Drawing.Font("宋体", 10.5f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		this.tbBen2.ForeColor = System.Drawing.Color.MediumSpringGreen;
		this.tbBen2.Location = new System.Drawing.Point(142, 37);
		this.tbBen2.Name = "tbBen2";
		this.tbBen2.Size = new System.Drawing.Size(111, 23);
		this.tbBen2.TabIndex = 10;
		this.tbBen2.Text = "0.00";
		this.tbBen3.BackColor = System.Drawing.SystemColors.MenuText;
		this.tbBen3.Font = new System.Drawing.Font("宋体", 10.5f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		this.tbBen3.ForeColor = System.Drawing.Color.MediumSpringGreen;
		this.tbBen3.Location = new System.Drawing.Point(142, 67);
		this.tbBen3.Name = "tbBen3";
		this.tbBen3.Size = new System.Drawing.Size(111, 23);
		this.tbBen3.TabIndex = 11;
		this.tbBen3.Text = "0.00";
		this.tbBen4.BackColor = System.Drawing.SystemColors.MenuText;
		this.tbBen4.Font = new System.Drawing.Font("宋体", 10.5f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		this.tbBen4.ForeColor = System.Drawing.Color.MediumSpringGreen;
		this.tbBen4.Location = new System.Drawing.Point(142, 97);
		this.tbBen4.Name = "tbBen4";
		this.tbBen4.Size = new System.Drawing.Size(111, 23);
		this.tbBen4.TabIndex = 12;
		this.tbBen4.Text = "0.00";
		this.tbNMHC.BackColor = System.Drawing.SystemColors.MenuText;
		this.tbNMHC.Font = new System.Drawing.Font("宋体", 10.5f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		this.tbNMHC.ForeColor = System.Drawing.Color.MediumSpringGreen;
		this.tbNMHC.Location = new System.Drawing.Point(163, 159);
		this.tbNMHC.Name = "tbNMHC";
		this.tbNMHC.Size = new System.Drawing.Size(111, 23);
		this.tbNMHC.TabIndex = 14;
		this.tbNMHC.Text = "0.00";
		this.label8.AutoSize = true;
		this.label8.Font = new System.Drawing.Font("宋体", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		this.label8.Location = new System.Drawing.Point(36, 160);
		this.label8.Name = "label8";
		this.label8.Size = new System.Drawing.Size(88, 16);
		this.label8.TabIndex = 13;
		this.label8.Text = "非甲烷总烃";
		this.tbBen5.BackColor = System.Drawing.SystemColors.MenuText;
		this.tbBen5.Font = new System.Drawing.Font("宋体", 10.5f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		this.tbBen5.ForeColor = System.Drawing.Color.MediumSpringGreen;
		this.tbBen5.Location = new System.Drawing.Point(142, 126);
		this.tbBen5.Name = "tbBen5";
		this.tbBen5.Size = new System.Drawing.Size(111, 23);
		this.tbBen5.TabIndex = 16;
		this.tbBen5.Text = "0.00";
		this.label9.AutoSize = true;
		this.label9.Font = new System.Drawing.Font("宋体", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		this.label9.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.label9.Location = new System.Drawing.Point(18, 133);
		this.label9.Name = "label9";
		this.label9.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.label9.Size = new System.Drawing.Size(56, 16);
		this.label9.TabIndex = 15;
		this.label9.Text = "苯乙烯";
		this.tbTimeCycle2.BackColor = System.Drawing.SystemColors.MenuText;
		this.tbTimeCycle2.Font = new System.Drawing.Font("宋体", 10.5f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		this.tbTimeCycle2.ForeColor = System.Drawing.Color.MediumSpringGreen;
		this.tbTimeCycle2.Location = new System.Drawing.Point(145, 65);
		this.tbTimeCycle2.Name = "tbTimeCycle2";
		this.tbTimeCycle2.Size = new System.Drawing.Size(111, 23);
		this.tbTimeCycle2.TabIndex = 20;
		this.tbTimeCycle2.Text = "0.00";
		this.tbTimeCycle2.TextChanged += new System.EventHandler(tbTimeCycle2_TextChanged);
		this.label10.AutoSize = true;
		this.label10.Font = new System.Drawing.Font("宋体", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		this.label10.Location = new System.Drawing.Point(19, 68);
		this.label10.Name = "label10";
		this.label10.Size = new System.Drawing.Size(112, 16);
		this.label10.TabIndex = 19;
		this.label10.Text = "通道2循环时间";
		this.label11.AutoSize = true;
		this.label11.Font = new System.Drawing.Font("宋体", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		this.label11.Location = new System.Drawing.Point(19, 43);
		this.label11.Name = "label11";
		this.label11.Size = new System.Drawing.Size(112, 16);
		this.label11.TabIndex = 17;
		this.label11.Text = "通道1循环时间";
		this.tbTimeCycle1.BackColor = System.Drawing.SystemColors.MenuText;
		this.tbTimeCycle1.Font = new System.Drawing.Font("宋体", 10.5f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		this.tbTimeCycle1.ForeColor = System.Drawing.Color.MediumSpringGreen;
		this.tbTimeCycle1.Location = new System.Drawing.Point(145, 40);
		this.tbTimeCycle1.Name = "tbTimeCycle1";
		this.tbTimeCycle1.Size = new System.Drawing.Size(111, 23);
		this.tbTimeCycle1.TabIndex = 18;
		this.tbTimeCycle1.Text = "0.00";
		this.tbTimeCycle1.TextChanged += new System.EventHandler(tbTimeCycle1_TextChanged);
		this.label12.AutoSize = true;
		this.label12.Font = new System.Drawing.Font("宋体", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		this.label12.Location = new System.Drawing.Point(262, 68);
		this.label12.Name = "label12";
		this.label12.Size = new System.Drawing.Size(32, 16);
		this.label12.TabIndex = 22;
		this.label12.Text = "min";
		this.label13.AutoSize = true;
		this.label13.Font = new System.Drawing.Font("宋体", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		this.label13.Location = new System.Drawing.Point(262, 43);
		this.label13.Name = "label13";
		this.label13.Size = new System.Drawing.Size(32, 16);
		this.label13.TabIndex = 21;
		this.label13.Text = "min";
		this.chbCycle.AutoSize = true;
		this.chbCycle.Location = new System.Drawing.Point(145, 132);
		this.chbCycle.Name = "chbCycle";
		this.chbCycle.Size = new System.Drawing.Size(102, 16);
		this.chbCycle.TabIndex = 23;
		this.chbCycle.Text = "通道1自动循环";
		this.chbCycle.UseVisualStyleBackColor = true;
		this.chbCycle.CheckedChanged += new System.EventHandler(chbCycle_CheckedChanged);
		this.chbCycle2.AutoSize = true;
		this.chbCycle2.Location = new System.Drawing.Point(145, 154);
		this.chbCycle2.Name = "chbCycle2";
		this.chbCycle2.Size = new System.Drawing.Size(102, 16);
		this.chbCycle2.TabIndex = 24;
		this.chbCycle2.Text = "通道2自动循环";
		this.chbCycle2.UseVisualStyleBackColor = true;
		this.chbCycle2.CheckedChanged += new System.EventHandler(chbCycle2_CheckedChanged);
		this.label14.AutoSize = true;
		this.label14.Font = new System.Drawing.Font("宋体", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		this.label14.Location = new System.Drawing.Point(262, 97);
		this.label14.Name = "label14";
		this.label14.Size = new System.Drawing.Size(32, 16);
		this.label14.TabIndex = 27;
		this.label14.Text = "min";
		this.label15.AutoSize = true;
		this.label15.Font = new System.Drawing.Font("宋体", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		this.label15.Location = new System.Drawing.Point(19, 97);
		this.label15.Name = "label15";
		this.label15.Size = new System.Drawing.Size(104, 16);
		this.label15.TabIndex = 25;
		this.label15.Text = "开机延迟时间";
		this.tbPowerOnDelay.BackColor = System.Drawing.SystemColors.MenuText;
		this.tbPowerOnDelay.Font = new System.Drawing.Font("宋体", 10.5f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		this.tbPowerOnDelay.ForeColor = System.Drawing.Color.MediumSpringGreen;
		this.tbPowerOnDelay.Location = new System.Drawing.Point(145, 94);
		this.tbPowerOnDelay.Name = "tbPowerOnDelay";
		this.tbPowerOnDelay.Size = new System.Drawing.Size(111, 23);
		this.tbPowerOnDelay.TabIndex = 26;
		this.tbPowerOnDelay.Text = "0.00";
		this.tbPowerOnDelay.TextChanged += new System.EventHandler(tbPowerOnDelay_TextChanged);
		this.panBen.Controls.Add(this.label4);
		this.panBen.Controls.Add(this.tbBen3);
		this.panBen.Controls.Add(this.tbBen2);
		this.panBen.Controls.Add(this.tbBen4);
		this.panBen.Controls.Add(this.tbBen1);
		this.panBen.Controls.Add(this.label5);
		this.panBen.Controls.Add(this.label6);
		this.panBen.Controls.Add(this.label9);
		this.panBen.Controls.Add(this.label7);
		this.panBen.Controls.Add(this.tbBen5);
		this.panBen.Location = new System.Drawing.Point(21, 188);
		this.panBen.Name = "panBen";
		this.panBen.Size = new System.Drawing.Size(302, 164);
		this.panBen.TabIndex = 28;
		this.panChannel.Controls.Add(this.label11);
		this.panChannel.Controls.Add(this.tbTimeCycle1);
		this.panChannel.Controls.Add(this.label14);
		this.panChannel.Controls.Add(this.label10);
		this.panChannel.Controls.Add(this.label15);
		this.panChannel.Controls.Add(this.tbTimeCycle2);
		this.panChannel.Controls.Add(this.tbPowerOnDelay);
		this.panChannel.Controls.Add(this.label13);
		this.panChannel.Controls.Add(this.chbCycle2);
		this.panChannel.Controls.Add(this.label12);
		this.panChannel.Controls.Add(this.chbCycle);
		this.panChannel.Location = new System.Drawing.Point(21, 379);
		this.panChannel.Name = "panChannel";
		this.panChannel.Size = new System.Drawing.Size(302, 182);
		this.panChannel.TabIndex = 29;
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.Controls.Add(this.panChannel);
		base.Controls.Add(this.panBen);
		base.Controls.Add(this.tbNMHC);
		base.Controls.Add(this.label8);
		base.Controls.Add(this.tbCH4);
		base.Controls.Add(this.tbTHC);
		base.Controls.Add(this.label3);
		base.Controls.Add(this.label2);
		base.Controls.Add(this.label1);
		this.Font = new System.Drawing.Font("宋体", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		base.Name = "tabRltCtrl";
		base.Size = new System.Drawing.Size(370, 561);
		this.panBen.ResumeLayout(false);
		this.panBen.PerformLayout();
		this.panChannel.ResumeLayout(false);
		this.panChannel.PerformLayout();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
