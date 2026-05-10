using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using IBrainChrom2018.Unit;

namespace IBrainChrom2018;

public class FormLYTHCCalibra : Form
{
	private ChromDeviceListMgr cdlMgr = ChromDeviceListMgr.Create();

	private LYTHCPara lythcParamMgr = LYTHCPara.Create();

	private IContainer components = null;

	private GroupBox groupBox1;

	private TextBox tbCH4Amount;

	private TextBox tbTHCAmount;

	private Label label2;

	private Label label1;

	private Button btnStartCalibra;

	private Label label3;

	private GroupBox groupBox2;

	private TextBox tbBYX;

	private TextBox tblejb;

	private Label label8;

	private Label label9;

	private TextBox tbYBB;

	private TextBox tbJeJB;

	private Label labYBB;

	private Label labJeJB;

	private TextBox tbDeJB;

	private TextBox tbCH3CH2C6H6;

	private Label labDeJB;

	private Label labCH3CH2C6H6;

	private TextBox tbCH3C6H6;

	private TextBox tbC6H6;

	private Label labCH3C6H6;

	private Label labC6H6;

	private Label label4;

	private ComboBox cbUnit;

	public FormLYTHCCalibra()
	{
		InitializeComponent();
		tbTHCAmount.Text = lythcParamMgr.THCAmount.ToString("0.000");
		tbCH4Amount.Text = lythcParamMgr.CH4Amount.ToString("0.000");
		cbUnit.Text = lythcParamMgr.strUnit;
	}

	private void btnStartCalibra_Click(object sender, EventArgs e)
	{
		if (LYTHCtrl.selfCtrl != null)
		{
			LYTHCtrl.selfCtrl.bCalibra = true;
			cdlMgr.currentTcpServerMgrSendCmd(18);
		}
		lythcParamMgr.strUnit = cbUnit.Text;
		lythcParamMgr.SaveParam();
		Close();
	}

	private void tbTHCAmount_TextChanged(object sender, EventArgs e)
	{
		lythcParamMgr.THCAmount = float.Parse(tbTHCAmount.Text);
		lythcParamMgr.SaveParam();
	}

	private void tbCH4Amount_TextChanged(object sender, EventArgs e)
	{
		lythcParamMgr.CH4Amount = float.Parse(tbCH4Amount.Text);
		lythcParamMgr.SaveParam();
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(IBrainChrom2018.FormLYTHCCalibra));
		this.groupBox1 = new System.Windows.Forms.GroupBox();
		this.tbCH4Amount = new System.Windows.Forms.TextBox();
		this.tbTHCAmount = new System.Windows.Forms.TextBox();
		this.label2 = new System.Windows.Forms.Label();
		this.label1 = new System.Windows.Forms.Label();
		this.btnStartCalibra = new System.Windows.Forms.Button();
		this.label3 = new System.Windows.Forms.Label();
		this.groupBox2 = new System.Windows.Forms.GroupBox();
		this.tbBYX = new System.Windows.Forms.TextBox();
		this.tblejb = new System.Windows.Forms.TextBox();
		this.label8 = new System.Windows.Forms.Label();
		this.label9 = new System.Windows.Forms.Label();
		this.tbYBB = new System.Windows.Forms.TextBox();
		this.tbJeJB = new System.Windows.Forms.TextBox();
		this.labYBB = new System.Windows.Forms.Label();
		this.labJeJB = new System.Windows.Forms.Label();
		this.tbDeJB = new System.Windows.Forms.TextBox();
		this.tbCH3CH2C6H6 = new System.Windows.Forms.TextBox();
		this.labDeJB = new System.Windows.Forms.Label();
		this.labCH3CH2C6H6 = new System.Windows.Forms.Label();
		this.tbCH3C6H6 = new System.Windows.Forms.TextBox();
		this.tbC6H6 = new System.Windows.Forms.TextBox();
		this.labCH3C6H6 = new System.Windows.Forms.Label();
		this.labC6H6 = new System.Windows.Forms.Label();
		this.label4 = new System.Windows.Forms.Label();
		this.cbUnit = new System.Windows.Forms.ComboBox();
		this.groupBox1.SuspendLayout();
		this.groupBox2.SuspendLayout();
		base.SuspendLayout();
		this.groupBox1.Controls.Add(this.cbUnit);
		this.groupBox1.Controls.Add(this.label4);
		this.groupBox1.Controls.Add(this.tbCH4Amount);
		this.groupBox1.Controls.Add(this.tbTHCAmount);
		this.groupBox1.Controls.Add(this.label2);
		this.groupBox1.Controls.Add(this.label1);
		this.groupBox1.Location = new System.Drawing.Point(12, 26);
		this.groupBox1.Name = "groupBox1";
		this.groupBox1.Size = new System.Drawing.Size(191, 338);
		this.groupBox1.TabIndex = 0;
		this.groupBox1.TabStop = false;
		this.groupBox1.Text = "非甲烷总烃标气";
		this.tbCH4Amount.Location = new System.Drawing.Point(76, 61);
		this.tbCH4Amount.Name = "tbCH4Amount";
		this.tbCH4Amount.Size = new System.Drawing.Size(100, 21);
		this.tbCH4Amount.TabIndex = 3;
		this.tbCH4Amount.TextChanged += new System.EventHandler(tbCH4Amount_TextChanged);
		this.tbTHCAmount.Location = new System.Drawing.Point(76, 28);
		this.tbTHCAmount.Name = "tbTHCAmount";
		this.tbTHCAmount.Size = new System.Drawing.Size(100, 21);
		this.tbTHCAmount.TabIndex = 2;
		this.tbTHCAmount.TextChanged += new System.EventHandler(tbTHCAmount_TextChanged);
		this.label2.AutoSize = true;
		this.label2.Location = new System.Drawing.Point(6, 70);
		this.label2.Name = "label2";
		this.label2.Size = new System.Drawing.Size(53, 12);
		this.label2.TabIndex = 1;
		this.label2.Text = "甲烷浓度";
		this.label1.AutoSize = true;
		this.label1.Location = new System.Drawing.Point(6, 31);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(53, 12);
		this.label1.TabIndex = 0;
		this.label1.Text = "总烃浓度";
		this.btnStartCalibra.Location = new System.Drawing.Point(328, 394);
		this.btnStartCalibra.Name = "btnStartCalibra";
		this.btnStartCalibra.Size = new System.Drawing.Size(75, 23);
		this.btnStartCalibra.TabIndex = 1;
		this.btnStartCalibra.Text = "开始标定";
		this.btnStartCalibra.UseVisualStyleBackColor = true;
		this.btnStartCalibra.Click += new System.EventHandler(btnStartCalibra_Click);
		this.label3.AutoSize = true;
		this.label3.Location = new System.Drawing.Point(15, 399);
		this.label3.Name = "label3";
		this.label3.Size = new System.Drawing.Size(173, 12);
		this.label3.TabIndex = 2;
		this.label3.Text = "注意：开始标定前请先接好标气";
		this.groupBox2.Controls.Add(this.tbBYX);
		this.groupBox2.Controls.Add(this.tblejb);
		this.groupBox2.Controls.Add(this.label8);
		this.groupBox2.Controls.Add(this.label9);
		this.groupBox2.Controls.Add(this.tbYBB);
		this.groupBox2.Controls.Add(this.tbJeJB);
		this.groupBox2.Controls.Add(this.labYBB);
		this.groupBox2.Controls.Add(this.labJeJB);
		this.groupBox2.Controls.Add(this.tbDeJB);
		this.groupBox2.Controls.Add(this.tbCH3CH2C6H6);
		this.groupBox2.Controls.Add(this.labDeJB);
		this.groupBox2.Controls.Add(this.labCH3CH2C6H6);
		this.groupBox2.Controls.Add(this.tbCH3C6H6);
		this.groupBox2.Controls.Add(this.tbC6H6);
		this.groupBox2.Controls.Add(this.labCH3C6H6);
		this.groupBox2.Controls.Add(this.labC6H6);
		this.groupBox2.Location = new System.Drawing.Point(350, 26);
		this.groupBox2.Name = "groupBox2";
		this.groupBox2.Size = new System.Drawing.Size(191, 338);
		this.groupBox2.TabIndex = 3;
		this.groupBox2.TabStop = false;
		this.groupBox2.Text = "苯系物标气";
		this.groupBox2.Visible = false;
		this.tbBYX.Location = new System.Drawing.Point(76, 281);
		this.tbBYX.Name = "tbBYX";
		this.tbBYX.Size = new System.Drawing.Size(100, 21);
		this.tbBYX.TabIndex = 15;
		this.tblejb.Location = new System.Drawing.Point(76, 248);
		this.tblejb.Name = "tblejb";
		this.tblejb.Size = new System.Drawing.Size(100, 21);
		this.tblejb.TabIndex = 14;
		this.label8.AutoSize = true;
		this.label8.Location = new System.Drawing.Point(6, 290);
		this.label8.Name = "label8";
		this.label8.Size = new System.Drawing.Size(41, 12);
		this.label8.TabIndex = 13;
		this.label8.Text = "苯乙烯";
		this.label9.AutoSize = true;
		this.label9.Location = new System.Drawing.Point(6, 251);
		this.label9.Name = "label9";
		this.label9.Size = new System.Drawing.Size(53, 12);
		this.label9.TabIndex = 12;
		this.label9.Text = "邻二甲苯";
		this.tbYBB.Location = new System.Drawing.Point(76, 206);
		this.tbYBB.Name = "tbYBB";
		this.tbYBB.Size = new System.Drawing.Size(100, 21);
		this.tbYBB.TabIndex = 11;
		this.tbJeJB.Location = new System.Drawing.Point(76, 173);
		this.tbJeJB.Name = "tbJeJB";
		this.tbJeJB.Size = new System.Drawing.Size(100, 21);
		this.tbJeJB.TabIndex = 10;
		this.labYBB.AutoSize = true;
		this.labYBB.Location = new System.Drawing.Point(6, 215);
		this.labYBB.Name = "labYBB";
		this.labYBB.Size = new System.Drawing.Size(41, 12);
		this.labYBB.TabIndex = 9;
		this.labYBB.Text = "异丙苯";
		this.labJeJB.AutoSize = true;
		this.labJeJB.Location = new System.Drawing.Point(6, 176);
		this.labJeJB.Name = "labJeJB";
		this.labJeJB.Size = new System.Drawing.Size(53, 12);
		this.labJeJB.TabIndex = 8;
		this.labJeJB.Text = "间二甲苯";
		this.tbDeJB.Location = new System.Drawing.Point(76, 136);
		this.tbDeJB.Name = "tbDeJB";
		this.tbDeJB.Size = new System.Drawing.Size(100, 21);
		this.tbDeJB.TabIndex = 7;
		this.tbCH3CH2C6H6.Location = new System.Drawing.Point(76, 103);
		this.tbCH3CH2C6H6.Name = "tbCH3CH2C6H6";
		this.tbCH3CH2C6H6.Size = new System.Drawing.Size(100, 21);
		this.tbCH3CH2C6H6.TabIndex = 6;
		this.labDeJB.AutoSize = true;
		this.labDeJB.Location = new System.Drawing.Point(6, 145);
		this.labDeJB.Name = "labDeJB";
		this.labDeJB.Size = new System.Drawing.Size(53, 12);
		this.labDeJB.TabIndex = 5;
		this.labDeJB.Text = "对二甲苯";
		this.labCH3CH2C6H6.AutoSize = true;
		this.labCH3CH2C6H6.Location = new System.Drawing.Point(6, 106);
		this.labCH3CH2C6H6.Name = "labCH3CH2C6H6";
		this.labCH3CH2C6H6.Size = new System.Drawing.Size(29, 12);
		this.labCH3CH2C6H6.TabIndex = 4;
		this.labCH3CH2C6H6.Text = "乙苯";
		this.tbCH3C6H6.Location = new System.Drawing.Point(76, 61);
		this.tbCH3C6H6.Name = "tbCH3C6H6";
		this.tbCH3C6H6.Size = new System.Drawing.Size(100, 21);
		this.tbCH3C6H6.TabIndex = 3;
		this.tbC6H6.Location = new System.Drawing.Point(76, 28);
		this.tbC6H6.Name = "tbC6H6";
		this.tbC6H6.Size = new System.Drawing.Size(100, 21);
		this.tbC6H6.TabIndex = 2;
		this.labCH3C6H6.AutoSize = true;
		this.labCH3C6H6.Location = new System.Drawing.Point(6, 70);
		this.labCH3C6H6.Name = "labCH3C6H6";
		this.labCH3C6H6.Size = new System.Drawing.Size(29, 12);
		this.labCH3C6H6.TabIndex = 1;
		this.labCH3C6H6.Text = "甲苯";
		this.labC6H6.AutoSize = true;
		this.labC6H6.Location = new System.Drawing.Point(6, 31);
		this.labC6H6.Name = "labC6H6";
		this.labC6H6.Size = new System.Drawing.Size(17, 12);
		this.labC6H6.TabIndex = 0;
		this.labC6H6.Text = "苯";
		this.label4.AutoSize = true;
		this.label4.Location = new System.Drawing.Point(6, 106);
		this.label4.Name = "label4";
		this.label4.Size = new System.Drawing.Size(53, 12);
		this.label4.TabIndex = 4;
		this.label4.Text = "标气单位";
		this.cbUnit.FormattingEnabled = true;
		this.cbUnit.Items.AddRange(new object[2] { "mg/m³", "umol/mol" });
		this.cbUnit.Location = new System.Drawing.Point(76, 100);
		this.cbUnit.Name = "cbUnit";
		this.cbUnit.Size = new System.Drawing.Size(100, 20);
		this.cbUnit.TabIndex = 4;
		this.cbUnit.Text = "mg/m³";
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(539, 429);
		base.Controls.Add(this.groupBox2);
		base.Controls.Add(this.label3);
		base.Controls.Add(this.btnStartCalibra);
		base.Controls.Add(this.groupBox1);
		base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
		base.Name = "FormLYTHCCalibra";
		this.Text = "FormLYTHCCalibra";
		this.groupBox1.ResumeLayout(false);
		this.groupBox1.PerformLayout();
		this.groupBox2.ResumeLayout(false);
		this.groupBox2.PerformLayout();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
