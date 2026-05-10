using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using IBrainChrom2018.Unit;

namespace IBrainChrom2018;

public class FormCollectMode : Form
{
	private LYTHCPara lythcParamMgr = LYTHCPara.Create();

	private IContainer components = null;

	private GroupBox groupBox1;

	private Button btnStartCalibra;

	private RadioButton rad60Min;

	private RadioButton radFive;

	private RadioButton radOne;

	private TextBox tbCollectTime;

	private Label label2;

	private TextBox tbCollectTimes;

	private Label label1;

	private Label label5;

	private Label label4;

	private TextBox tbIntervalTime;

	private Label label3;

	private Label label6;

	private TextBox tbCollectSite;

	private TextBox tbCollectP;

	private Label label7;

	private TextBox tbCollectMC;

	private Label label8;

	private TextBox tbCollectBH;

	private Label label9;

	private TextBox tbCollectSJDW;

	private Label label10;

	private TextBox tbCollectJYDW;

	private Label label11;

	private TextBox tbCollectJCXM;

	private Label label12;

	private TextBox textBox1;

	private Label label13;

	private TextBox textBox2;

	private Label label14;

	private TextBox textBox3;

	private Label label15;

	public FormCollectMode()
	{
		InitializeComponent();
		if (lythcParamMgr.collectMode == 1)
		{
			radOne.Checked = true;
		}
		else if (lythcParamMgr.collectMode == 2)
		{
			radFive.Checked = true;
		}
		else if (lythcParamMgr.collectMode == 3)
		{
			rad60Min.Checked = true;
		}
		tbCollectTimes.Text = lythcParamMgr.collectTimes.ToString();
		tbCollectTime.Text = lythcParamMgr.collectTime.ToString();
		tbIntervalTime.Text = lythcParamMgr.intervalTime.ToString();
		tbCollectSite.Text = lythcParamMgr.strCollectSite;
		tbCollectP.Text = lythcParamMgr.strCollectP;
		tbCollectMC.Text = lythcParamMgr.strCollectMC;
		tbCollectBH.Text = lythcParamMgr.strCollectBH;
		tbCollectSJDW.Text = lythcParamMgr.strCollectSJDW;
		tbCollectJYDW.Text = lythcParamMgr.strCollectJYDW;
		tbCollectJCXM.Text = lythcParamMgr.strCollectJCXM;
		textBox2.Text = lythcParamMgr.strCollectWenDu;
		textBox1.Text = lythcParamMgr.strCollectShiDu;
		textBox3.Text = lythcParamMgr.strCollectDQy;
		base.StartPosition = FormStartPosition.CenterScreen;
	}

	private void btnStartCalibra_Click(object sender, EventArgs e)
	{
		if (radOne.Checked)
		{
			lythcParamMgr.collectMode = 1;
		}
		else if (radFive.Checked)
		{
			lythcParamMgr.collectMode = 2;
			if (tbCollectTimes.Text == "")
			{
				MessageBox.Show("请输入采样次数!");
				return;
			}
			if (tbIntervalTime.Text == "")
			{
				MessageBox.Show("请输入间隔时间!");
				return;
			}
		}
		else if (rad60Min.Checked)
		{
			lythcParamMgr.collectMode = 3;
			if (tbCollectTime.Text == "")
			{
				MessageBox.Show("请输入采样时长!");
				return;
			}
			if (tbIntervalTime.Text == "")
			{
				MessageBox.Show("请输入间隔时间!");
				return;
			}
		}
		lythcParamMgr.collectTimes = int.Parse(tbCollectTimes.Text.ToLower());
		lythcParamMgr.collectTime = float.Parse(tbCollectTime.Text.ToLower());
		lythcParamMgr.intervalTime = float.Parse(tbIntervalTime.Text.ToLower());
		lythcParamMgr.strCollectSite = tbCollectSite.Text;
		lythcParamMgr.strCollectP = tbCollectP.Text;
		lythcParamMgr.strCollectMC = tbCollectMC.Text;
		lythcParamMgr.strCollectBH = tbCollectBH.Text;
		lythcParamMgr.strCollectSJDW = tbCollectSJDW.Text;
		lythcParamMgr.strCollectJYDW = tbCollectJYDW.Text;
		lythcParamMgr.strCollectJCXM = tbCollectJCXM.Text;
		lythcParamMgr.strCollectWenDu = textBox2.Text;
		lythcParamMgr.strCollectShiDu = textBox1.Text;
		lythcParamMgr.strCollectDQy = textBox3.Text;
		lythcParamMgr.SaveParam();
		Close();
	}

	private void tbCollectP_TextChanged(object sender, EventArgs e)
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(IBrainChrom2018.FormCollectMode));
		this.groupBox1 = new System.Windows.Forms.GroupBox();
		this.label5 = new System.Windows.Forms.Label();
		this.label4 = new System.Windows.Forms.Label();
		this.tbIntervalTime = new System.Windows.Forms.TextBox();
		this.label3 = new System.Windows.Forms.Label();
		this.tbCollectTime = new System.Windows.Forms.TextBox();
		this.label2 = new System.Windows.Forms.Label();
		this.tbCollectTimes = new System.Windows.Forms.TextBox();
		this.label1 = new System.Windows.Forms.Label();
		this.rad60Min = new System.Windows.Forms.RadioButton();
		this.radFive = new System.Windows.Forms.RadioButton();
		this.radOne = new System.Windows.Forms.RadioButton();
		this.btnStartCalibra = new System.Windows.Forms.Button();
		this.label6 = new System.Windows.Forms.Label();
		this.tbCollectSite = new System.Windows.Forms.TextBox();
		this.tbCollectP = new System.Windows.Forms.TextBox();
		this.label7 = new System.Windows.Forms.Label();
		this.tbCollectMC = new System.Windows.Forms.TextBox();
		this.label8 = new System.Windows.Forms.Label();
		this.tbCollectBH = new System.Windows.Forms.TextBox();
		this.label9 = new System.Windows.Forms.Label();
		this.tbCollectSJDW = new System.Windows.Forms.TextBox();
		this.label10 = new System.Windows.Forms.Label();
		this.tbCollectJYDW = new System.Windows.Forms.TextBox();
		this.label11 = new System.Windows.Forms.Label();
		this.tbCollectJCXM = new System.Windows.Forms.TextBox();
		this.label12 = new System.Windows.Forms.Label();
		this.textBox1 = new System.Windows.Forms.TextBox();
		this.label13 = new System.Windows.Forms.Label();
		this.textBox2 = new System.Windows.Forms.TextBox();
		this.label14 = new System.Windows.Forms.Label();
		this.textBox3 = new System.Windows.Forms.TextBox();
		this.label15 = new System.Windows.Forms.Label();
		this.groupBox1.SuspendLayout();
		base.SuspendLayout();
		this.groupBox1.Controls.Add(this.label5);
		this.groupBox1.Controls.Add(this.label4);
		this.groupBox1.Controls.Add(this.tbIntervalTime);
		this.groupBox1.Controls.Add(this.label3);
		this.groupBox1.Controls.Add(this.tbCollectTime);
		this.groupBox1.Controls.Add(this.label2);
		this.groupBox1.Controls.Add(this.tbCollectTimes);
		this.groupBox1.Controls.Add(this.label1);
		this.groupBox1.Controls.Add(this.rad60Min);
		this.groupBox1.Controls.Add(this.radFive);
		this.groupBox1.Controls.Add(this.radOne);
		this.groupBox1.Location = new System.Drawing.Point(12, 26);
		this.groupBox1.Name = "groupBox1";
		this.groupBox1.Size = new System.Drawing.Size(431, 117);
		this.groupBox1.TabIndex = 0;
		this.groupBox1.TabStop = false;
		this.groupBox1.Text = "采样模式";
		this.label5.AutoSize = true;
		this.label5.Location = new System.Drawing.Point(404, 88);
		this.label5.Name = "label5";
		this.label5.Size = new System.Drawing.Size(23, 12);
		this.label5.TabIndex = 11;
		this.label5.Text = "min";
		this.label4.AutoSize = true;
		this.label4.Location = new System.Drawing.Point(230, 88);
		this.label4.Name = "label4";
		this.label4.Size = new System.Drawing.Size(23, 12);
		this.label4.TabIndex = 10;
		this.label4.Text = "min";
		this.tbIntervalTime.Location = new System.Drawing.Point(328, 81);
		this.tbIntervalTime.Name = "tbIntervalTime";
		this.tbIntervalTime.Size = new System.Drawing.Size(70, 21);
		this.tbIntervalTime.TabIndex = 9;
		this.label3.AutoSize = true;
		this.label3.Location = new System.Drawing.Point(257, 88);
		this.label3.Name = "label3";
		this.label3.Size = new System.Drawing.Size(65, 12);
		this.label3.TabIndex = 8;
		this.label3.Text = "间隔时间：";
		this.tbCollectTime.Location = new System.Drawing.Point(154, 81);
		this.tbCollectTime.Name = "tbCollectTime";
		this.tbCollectTime.Size = new System.Drawing.Size(70, 21);
		this.tbCollectTime.TabIndex = 7;
		this.label2.AutoSize = true;
		this.label2.Location = new System.Drawing.Point(83, 88);
		this.label2.Name = "label2";
		this.label2.Size = new System.Drawing.Size(65, 12);
		this.label2.TabIndex = 6;
		this.label2.Text = "采样时间：";
		this.tbCollectTimes.Location = new System.Drawing.Point(154, 46);
		this.tbCollectTimes.Name = "tbCollectTimes";
		this.tbCollectTimes.Size = new System.Drawing.Size(70, 21);
		this.tbCollectTimes.TabIndex = 5;
		this.label1.AutoSize = true;
		this.label1.Location = new System.Drawing.Point(83, 53);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(65, 12);
		this.label1.TabIndex = 4;
		this.label1.Text = "采样次数：";
		this.rad60Min.AutoSize = true;
		this.rad60Min.Location = new System.Drawing.Point(6, 86);
		this.rad60Min.Name = "rad60Min";
		this.rad60Min.Size = new System.Drawing.Size(71, 16);
		this.rad60Min.TabIndex = 3;
		this.rad60Min.TabStop = true;
		this.rad60Min.Text = "时间采样";
		this.rad60Min.UseVisualStyleBackColor = true;
		this.radFive.AutoSize = true;
		this.radFive.Location = new System.Drawing.Point(6, 51);
		this.radFive.Name = "radFive";
		this.radFive.Size = new System.Drawing.Size(71, 16);
		this.radFive.TabIndex = 2;
		this.radFive.TabStop = true;
		this.radFive.Text = "多次采样";
		this.radFive.UseVisualStyleBackColor = true;
		this.radOne.AutoSize = true;
		this.radOne.Location = new System.Drawing.Point(6, 20);
		this.radOne.Name = "radOne";
		this.radOne.Size = new System.Drawing.Size(71, 16);
		this.radOne.TabIndex = 1;
		this.radOne.TabStop = true;
		this.radOne.Text = "单次采样";
		this.radOne.UseVisualStyleBackColor = true;
		this.btnStartCalibra.Location = new System.Drawing.Point(368, 326);
		this.btnStartCalibra.Name = "btnStartCalibra";
		this.btnStartCalibra.Size = new System.Drawing.Size(75, 37);
		this.btnStartCalibra.TabIndex = 1;
		this.btnStartCalibra.Text = "设定";
		this.btnStartCalibra.UseVisualStyleBackColor = true;
		this.btnStartCalibra.Click += new System.EventHandler(btnStartCalibra_Click);
		this.label6.AutoSize = true;
		this.label6.Location = new System.Drawing.Point(16, 313);
		this.label6.Name = "label6";
		this.label6.Size = new System.Drawing.Size(65, 12);
		this.label6.TabIndex = 2;
		this.label6.Text = "采样地点：";
		this.tbCollectSite.Location = new System.Drawing.Point(77, 304);
		this.tbCollectSite.Name = "tbCollectSite";
		this.tbCollectSite.Size = new System.Drawing.Size(159, 21);
		this.tbCollectSite.TabIndex = 3;
		this.tbCollectP.Location = new System.Drawing.Point(77, 342);
		this.tbCollectP.Name = "tbCollectP";
		this.tbCollectP.Size = new System.Drawing.Size(159, 21);
		this.tbCollectP.TabIndex = 5;
		this.tbCollectP.TextChanged += new System.EventHandler(tbCollectP_TextChanged);
		this.label7.AutoSize = true;
		this.label7.Location = new System.Drawing.Point(16, 351);
		this.label7.Name = "label7";
		this.label7.Size = new System.Drawing.Size(53, 12);
		this.label7.TabIndex = 4;
		this.label7.Text = "采样人：";
		this.tbCollectMC.Location = new System.Drawing.Point(77, 158);
		this.tbCollectMC.Name = "tbCollectMC";
		this.tbCollectMC.Size = new System.Drawing.Size(159, 21);
		this.tbCollectMC.TabIndex = 7;
		this.label8.AutoSize = true;
		this.label8.Location = new System.Drawing.Point(16, 167);
		this.label8.Name = "label8";
		this.label8.Size = new System.Drawing.Size(65, 12);
		this.label8.TabIndex = 6;
		this.label8.Text = "样品名称：";
		this.tbCollectBH.Location = new System.Drawing.Point(77, 185);
		this.tbCollectBH.Name = "tbCollectBH";
		this.tbCollectBH.Size = new System.Drawing.Size(159, 21);
		this.tbCollectBH.TabIndex = 9;
		this.label9.AutoSize = true;
		this.label9.Location = new System.Drawing.Point(16, 194);
		this.label9.Name = "label9";
		this.label9.Size = new System.Drawing.Size(65, 12);
		this.label9.TabIndex = 8;
		this.label9.Text = "样品编号：";
		this.tbCollectSJDW.Location = new System.Drawing.Point(77, 212);
		this.tbCollectSJDW.Name = "tbCollectSJDW";
		this.tbCollectSJDW.Size = new System.Drawing.Size(159, 21);
		this.tbCollectSJDW.TabIndex = 11;
		this.label10.AutoSize = true;
		this.label10.Location = new System.Drawing.Point(16, 221);
		this.label10.Name = "label10";
		this.label10.Size = new System.Drawing.Size(65, 12);
		this.label10.TabIndex = 10;
		this.label10.Text = "受检单位：";
		this.tbCollectJYDW.Location = new System.Drawing.Point(77, 239);
		this.tbCollectJYDW.Name = "tbCollectJYDW";
		this.tbCollectJYDW.Size = new System.Drawing.Size(159, 21);
		this.tbCollectJYDW.TabIndex = 13;
		this.label11.AutoSize = true;
		this.label11.Location = new System.Drawing.Point(16, 248);
		this.label11.Name = "label11";
		this.label11.Size = new System.Drawing.Size(65, 12);
		this.label11.TabIndex = 12;
		this.label11.Text = "检验单位：";
		this.tbCollectJCXM.Location = new System.Drawing.Point(77, 266);
		this.tbCollectJCXM.Name = "tbCollectJCXM";
		this.tbCollectJCXM.Size = new System.Drawing.Size(159, 21);
		this.tbCollectJCXM.TabIndex = 15;
		this.label12.AutoSize = true;
		this.label12.Location = new System.Drawing.Point(16, 275);
		this.label12.Name = "label12";
		this.label12.Size = new System.Drawing.Size(65, 12);
		this.label12.TabIndex = 14;
		this.label12.Text = "检测项目：";
		this.textBox1.Location = new System.Drawing.Point(298, 185);
		this.textBox1.Name = "textBox1";
		this.textBox1.Size = new System.Drawing.Size(146, 21);
		this.textBox1.TabIndex = 19;
		this.label13.AutoSize = true;
		this.label13.Location = new System.Drawing.Point(237, 194);
		this.label13.Name = "label13";
		this.label13.Size = new System.Drawing.Size(65, 12);
		this.label13.TabIndex = 18;
		this.label13.Text = "环境湿度：";
		this.textBox2.Location = new System.Drawing.Point(298, 158);
		this.textBox2.Name = "textBox2";
		this.textBox2.Size = new System.Drawing.Size(146, 21);
		this.textBox2.TabIndex = 17;
		this.label14.AutoSize = true;
		this.label14.Location = new System.Drawing.Point(237, 167);
		this.label14.Name = "label14";
		this.label14.Size = new System.Drawing.Size(65, 12);
		this.label14.TabIndex = 16;
		this.label14.Text = "环境温度：";
		this.textBox3.Location = new System.Drawing.Point(298, 212);
		this.textBox3.Name = "textBox3";
		this.textBox3.Size = new System.Drawing.Size(146, 21);
		this.textBox3.TabIndex = 21;
		this.label15.AutoSize = true;
		this.label15.Location = new System.Drawing.Point(237, 221);
		this.label15.Name = "label15";
		this.label15.Size = new System.Drawing.Size(65, 12);
		this.label15.TabIndex = 20;
		this.label15.Text = "大气压力：";
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(456, 380);
		base.Controls.Add(this.textBox3);
		base.Controls.Add(this.label15);
		base.Controls.Add(this.textBox1);
		base.Controls.Add(this.label13);
		base.Controls.Add(this.textBox2);
		base.Controls.Add(this.label14);
		base.Controls.Add(this.tbCollectJCXM);
		base.Controls.Add(this.label12);
		base.Controls.Add(this.tbCollectJYDW);
		base.Controls.Add(this.label11);
		base.Controls.Add(this.tbCollectSJDW);
		base.Controls.Add(this.label10);
		base.Controls.Add(this.tbCollectBH);
		base.Controls.Add(this.label9);
		base.Controls.Add(this.tbCollectMC);
		base.Controls.Add(this.label8);
		base.Controls.Add(this.tbCollectP);
		base.Controls.Add(this.label7);
		base.Controls.Add(this.tbCollectSite);
		base.Controls.Add(this.label6);
		base.Controls.Add(this.btnStartCalibra);
		base.Controls.Add(this.groupBox1);
		base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
		base.Name = "FormCollectMode";
		this.Text = "采样模式设定";
		this.groupBox1.ResumeLayout(false);
		this.groupBox1.PerformLayout();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
