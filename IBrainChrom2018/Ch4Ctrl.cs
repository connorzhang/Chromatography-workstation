using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using IBrainChrom2018.Unit;

namespace IBrainChrom2018;

public class Ch4Ctrl : UserControl
{
	public static Ch4Ctrl selfCtrl;

	private FormMainParam frmParam = FormMainParam.Create();

	private ChromDeviceListMgr cdlMgr = ChromDeviceListMgr.Create();

	public bool bLoading = true;

	public float fO2;

	private IContainer components = null;

	private CheckBox chbO2;

	private Button btnO2;

	private TextBox tbO2;

	private TextBox tbNMHC;

	private TextBox tbCH4;

	private TextBox tbHyd;

	private Label label79;

	private Label label78;

	private Label label77;

	private Label label76;

	private GroupBox groupBox1;

	public Label labO;

	public CheckBox cbEnNMHC;

	private Label label1;

	private TextBox tbTHCO;

	public static bool IsDesignMode()
	{
		return false;
	}

	public Ch4Ctrl()
	{
		InitializeComponent();
		if (!IsDesignMode())
		{
			selfCtrl = this;
			tbHyd.Text = frmParam.fHyd.ToString();
			tbCH4.Text = frmParam.fCh4.ToString();
			tbNMHC.Text = frmParam.fNMHC.ToString();
			tbO2.Text = frmParam.fO2.ToString();
			cbEnNMHC.Checked = frmParam.bEnNMHC;
			chbO2.Checked = frmParam.bEnO2;
			bLoading = false;
		}
	}

	private void btnO2_Click(object sender, EventArgs e)
	{
		frmParam.fHyd = (float)Convert.ToDouble(tbHyd.Text.Trim());
		frmParam.fCh4 = (float)Convert.ToDouble(tbCH4.Text.Trim());
		frmParam.fNMHC = (float)Convert.ToDouble(tbNMHC.Text.Trim());
		frmParam.fO2 = (float)Convert.ToDouble(tbO2.Text.Trim());
		frmParam.SaveParam();
	}

	public void disposeVOCPeaks(int selectedIndex, string fileName, string strID, string strSampleIndex, Chromatogram chromatogram)
	{
		if (!cbEnNMHC.Checked)
		{
			return;
		}
		int num = 0;
		float num2 = 0f;
		float num3 = 0f;
		float num4 = 0f;
		float num5 = 0f;
		float num6 = 0f;
		Peak[] peakAllCompound = chromatogram.GetPeakAllCompound();
		for (int i = 0; i < peakAllCompound.Length; i++)
		{
			switch (i)
			{
			case 0:
				num4 = peakAllCompound[i].amount;
				tbHyd.Text = num4.ToString("F" + Class49.int_8);
				break;
			case 1:
				num5 = peakAllCompound[i].amount;
				tbCH4.Text = num5.ToString("F" + Class49.int_8);
				break;
			}
		}
		if (peakAllCompound.Length != 0)
		{
			float[] array = peakAllCompound[0].compound.eFunc.Calcu_amountF(cdlMgr.CurrentChartParaOpera.mtdMgr.chromInfoR.UvwsStartT);
			float num7 = float.Parse(peakAllCompound[0].amount.ToString("F" + Class49.int_8));
			float num8 = float.Parse(peakAllCompound[1].amount.ToString("F" + Class49.int_8));
			float num9 = float.Parse(array[0].ToString("F" + Class49.int_8));
			tbTHCO.Text = (peakAllCompound[0].amount - array[0]).ToString("F" + Class49.int_8);
			num6 = num7 - num8 - num9;
			if (num6 < 0f)
			{
				num6 = 0f;
			}
			tbNMHC.Text = "以碳计:" + (num6 * 0.75f).ToString("F" + Class49.int_8) + "  以甲烷计:" + num6.ToString("F" + Class49.int_8);
		}
		else
		{
			tbHyd.Text = 0.ToString("F" + Class49.int_8);
			tbCH4.Text = 0.ToString("F" + Class49.int_8);
			tbTHCO.Text = 0.ToString("F" + Class49.int_8);
			tbNMHC.Text = 0.ToString("F" + Class49.int_8);
		}
	}

	private void CbEnNMHC_CheckedChanged(object sender, EventArgs e)
	{
		if (!bLoading)
		{
			frmParam.bEnNMHC = cbEnNMHC.Checked;
			frmParam.SaveParam();
			cdlMgr.formMain.chromFormCtrl.Reload();
		}
	}

	private void ChbO2_CheckedChanged(object sender, EventArgs e)
	{
		if (!bLoading)
		{
			frmParam.bEnO2 = chbO2.Checked;
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
		this.cbEnNMHC = new System.Windows.Forms.CheckBox();
		this.chbO2 = new System.Windows.Forms.CheckBox();
		this.btnO2 = new System.Windows.Forms.Button();
		this.tbO2 = new System.Windows.Forms.TextBox();
		this.tbNMHC = new System.Windows.Forms.TextBox();
		this.tbCH4 = new System.Windows.Forms.TextBox();
		this.tbHyd = new System.Windows.Forms.TextBox();
		this.label79 = new System.Windows.Forms.Label();
		this.label78 = new System.Windows.Forms.Label();
		this.label77 = new System.Windows.Forms.Label();
		this.label76 = new System.Windows.Forms.Label();
		this.groupBox1 = new System.Windows.Forms.GroupBox();
		this.label1 = new System.Windows.Forms.Label();
		this.tbTHCO = new System.Windows.Forms.TextBox();
		this.labO = new System.Windows.Forms.Label();
		this.groupBox1.SuspendLayout();
		base.SuspendLayout();
		this.cbEnNMHC.AutoSize = true;
		this.cbEnNMHC.Location = new System.Drawing.Point(189, 15);
		this.cbEnNMHC.Name = "cbEnNMHC";
		this.cbEnNMHC.Size = new System.Drawing.Size(84, 16);
		this.cbEnNMHC.TabIndex = 22;
		this.cbEnNMHC.Text = "非甲烷总烃";
		this.cbEnNMHC.UseVisualStyleBackColor = true;
		this.cbEnNMHC.CheckedChanged += new System.EventHandler(CbEnNMHC_CheckedChanged);
		this.chbO2.AutoSize = true;
		this.chbO2.Location = new System.Drawing.Point(473, 30);
		this.chbO2.Name = "chbO2";
		this.chbO2.Size = new System.Drawing.Size(84, 16);
		this.chbO2.TabIndex = 21;
		this.chbO2.Text = "氧含量标定";
		this.chbO2.UseVisualStyleBackColor = true;
		this.chbO2.Visible = false;
		this.chbO2.CheckedChanged += new System.EventHandler(ChbO2_CheckedChanged);
		this.btnO2.Location = new System.Drawing.Point(187, 70);
		this.btnO2.Name = "btnO2";
		this.btnO2.Size = new System.Drawing.Size(75, 23);
		this.btnO2.TabIndex = 20;
		this.btnO2.Text = "保存氧含量";
		this.btnO2.UseVisualStyleBackColor = true;
		this.btnO2.Visible = false;
		this.btnO2.Click += new System.EventHandler(btnO2_Click);
		this.tbO2.Location = new System.Drawing.Point(400, 72);
		this.tbO2.Name = "tbO2";
		this.tbO2.Size = new System.Drawing.Size(67, 21);
		this.tbO2.TabIndex = 19;
		this.tbO2.Visible = false;
		this.tbNMHC.Location = new System.Drawing.Point(84, 96);
		this.tbNMHC.Name = "tbNMHC";
		this.tbNMHC.Size = new System.Drawing.Size(316, 21);
		this.tbNMHC.TabIndex = 18;
		this.tbCH4.Location = new System.Drawing.Point(84, 45);
		this.tbCH4.Name = "tbCH4";
		this.tbCH4.Size = new System.Drawing.Size(67, 21);
		this.tbCH4.TabIndex = 17;
		this.tbHyd.Location = new System.Drawing.Point(84, 15);
		this.tbHyd.Name = "tbHyd";
		this.tbHyd.Size = new System.Drawing.Size(67, 21);
		this.tbHyd.TabIndex = 16;
		this.label79.AutoSize = true;
		this.label79.Location = new System.Drawing.Point(187, 48);
		this.label79.Name = "label79";
		this.label79.Size = new System.Drawing.Size(53, 12);
		this.label79.TabIndex = 15;
		this.label79.Text = "氧含量：";
		this.label78.AutoSize = true;
		this.label78.Location = new System.Drawing.Point(7, 102);
		this.label78.Name = "label78";
		this.label78.Size = new System.Drawing.Size(77, 12);
		this.label78.TabIndex = 14;
		this.label78.Text = "非甲烷总烃：";
		this.label77.AutoSize = true;
		this.label77.Location = new System.Drawing.Point(7, 48);
		this.label77.Name = "label77";
		this.label77.Size = new System.Drawing.Size(77, 12);
		this.label77.TabIndex = 13;
		this.label77.Text = "甲      烷：";
		this.label76.AutoSize = true;
		this.label76.Location = new System.Drawing.Point(7, 20);
		this.label76.Name = "label76";
		this.label76.Size = new System.Drawing.Size(77, 12);
		this.label76.TabIndex = 12;
		this.label76.Text = "总      烃：";
		this.groupBox1.Controls.Add(this.label1);
		this.groupBox1.Controls.Add(this.tbTHCO);
		this.groupBox1.Controls.Add(this.labO);
		this.groupBox1.Controls.Add(this.label76);
		this.groupBox1.Controls.Add(this.cbEnNMHC);
		this.groupBox1.Controls.Add(this.label77);
		this.groupBox1.Controls.Add(this.chbO2);
		this.groupBox1.Controls.Add(this.label78);
		this.groupBox1.Controls.Add(this.btnO2);
		this.groupBox1.Controls.Add(this.label79);
		this.groupBox1.Controls.Add(this.tbO2);
		this.groupBox1.Controls.Add(this.tbHyd);
		this.groupBox1.Controls.Add(this.tbNMHC);
		this.groupBox1.Controls.Add(this.tbCH4);
		this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
		this.groupBox1.Location = new System.Drawing.Point(3, 3);
		this.groupBox1.Name = "groupBox1";
		this.groupBox1.Size = new System.Drawing.Size(852, 130);
		this.groupBox1.TabIndex = 23;
		this.groupBox1.TabStop = false;
		this.groupBox1.Text = "非甲烷总烃";
		this.label1.AutoSize = true;
		this.label1.Location = new System.Drawing.Point(19, 75);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(65, 12);
		this.label1.TabIndex = 24;
		this.label1.Text = "总烃去氧：";
		this.tbTHCO.Location = new System.Drawing.Point(84, 69);
		this.tbTHCO.Name = "tbTHCO";
		this.tbTHCO.Size = new System.Drawing.Size(67, 21);
		this.tbTHCO.TabIndex = 25;
		this.labO.AutoSize = true;
		this.labO.Location = new System.Drawing.Point(246, 48);
		this.labO.Name = "labO";
		this.labO.Size = new System.Drawing.Size(11, 12);
		this.labO.TabIndex = 23;
		this.labO.Text = "0";
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.Controls.Add(this.groupBox1);
		base.Name = "Ch4Ctrl";
		base.Padding = new System.Windows.Forms.Padding(3);
		base.Size = new System.Drawing.Size(858, 136);
		this.groupBox1.ResumeLayout(false);
		this.groupBox1.PerformLayout();
		base.ResumeLayout(false);
	}
}
