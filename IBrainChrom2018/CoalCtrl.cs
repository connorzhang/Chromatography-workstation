using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using IBrainChrom2018.Unit;

namespace IBrainChrom2018;

public class CoalCtrl : UserControl
{
	public bool flagChannelOver1 = false;

	public bool flagChannelOver2 = false;

	public bool flagChannelOver3 = false;

	public string strFileName = "";

	public string strFileName2 = "";

	public string strFileName3 = "";

	public float fTempRlt1;

	public float fTempCol1;

	public float fTempRlt2;

	public float fTempCol2;

	public bool bLoading = true;

	public string[] arrayData = new string[15];

	public float[] fArrayData = new float[15];

	public bool bStart = false;

	public static CoalCtrl selfCtrl;

	private FormMainParam frmParam = FormMainParam.Create();

	private ChromDeviceListMgr cdlMgr = ChromDeviceListMgr.Create();

	private IContainer components = null;

	private Button btnHistory;

	private Button btnTest;

	private TextBox tbInterTemp;

	private Label label1;

	private Label label2;

	public CoalCtrl()
	{
		selfCtrl = this;
		InitializeComponent();
		initForm();
		bLoading = false;
	}

	public void initForm()
	{
		tbInterTemp.Text = frmParam.fInterTemp.ToString("F" + Class49.int_8);
		for (int i = 0; i < 15; i++)
		{
			arrayData[i] = "0.00000";
			fArrayData[i] = 0f;
		}
	}

	public void disposePeaks(int selectedIndex, string fileName, string strID, string strSampleIndex, Chromatogram chromatogram)
	{
		if (selectedIndex == 0)
		{
			strFileName = fileName;
			flagChannelOver1 = true;
		}
		if (selectedIndex == 1)
		{
			strFileName2 = fileName;
			flagChannelOver2 = true;
		}
		if (selectedIndex == 2)
		{
			strFileName3 = fileName;
			flagChannelOver3 = true;
		}
		Peak[] peakAllCompound = chromatogram.GetPeakAllCompound();
		for (int i = 0; i < peakAllCompound.Length; i++)
		{
			if (peakAllCompound[i].amount < 0f)
			{
				peakAllCompound[i].amount = 0f;
			}
			data2Array(peakAllCompound[i].name, peakAllCompound[i].amount);
		}
		if (cdlMgr.formMain.tabChannel.TabCount == 1)
		{
			if (flagChannelOver1)
			{
				flagChannelOver1 = false;
				flagChannelOver2 = false;
				flagChannelOver3 = false;
				arrayData[0] = fTempCol1.ToString();
				arrayData[1] = fTempCol2.ToString();
				Class49.InsertIntoCoalTable(0, 0, arrayData, strFileName, strFileName2, strFileName3);
			}
		}
		else if (cdlMgr.formMain.tabChannel.TabCount == 2)
		{
			if (flagChannelOver1 && flagChannelOver2)
			{
				flagChannelOver1 = false;
				flagChannelOver2 = false;
				flagChannelOver3 = false;
				arrayData[0] = fTempCol1.ToString();
				arrayData[1] = fTempCol2.ToString();
				Class49.InsertIntoCoalTable(0, 0, arrayData, strFileName, strFileName2, strFileName3);
			}
		}
		else if (cdlMgr.formMain.tabChannel.TabCount == 3 && flagChannelOver1 && flagChannelOver2 && flagChannelOver3)
		{
			flagChannelOver1 = false;
			flagChannelOver2 = false;
			flagChannelOver3 = false;
			arrayData[0] = fTempCol1.ToString();
			arrayData[1] = fTempCol2.ToString();
			Class49.InsertIntoCoalTable(0, 0, arrayData, strFileName, strFileName2, strFileName3);
		}
	}

	public void data2Array(string name, float amount)
	{
		switch (name)
		{
		case "丙烯":
			arrayData[8] = amount.ToString("F" + Class49.int_8);
			fArrayData[8] = amount;
			break;
		case "氮气":
			arrayData[11] = amount.ToString("F" + Class49.int_8);
			fArrayData[11] = amount;
			break;
		case "甲烷":
			arrayData[3] = amount.ToString("F" + Class49.int_8);
			fArrayData[3] = amount;
			break;
		case "乙炔":
			arrayData[9] = amount.ToString("F" + Class49.int_8);
			fArrayData[9] = amount;
			break;
		case "氧气":
			arrayData[10] = amount.ToString("F" + Class49.int_8);
			fArrayData[10] = amount;
			break;
		case "乙烷":
			arrayData[6] = amount.ToString("F" + Class49.int_8);
			fArrayData[6] = amount;
			break;
		case "一氧化碳":
			arrayData[2] = amount.ToString("F" + Class49.int_8);
			fArrayData[2] = amount;
			break;
		case "乙烯":
			arrayData[5] = amount.ToString("F" + Class49.int_8);
			fArrayData[5] = amount;
			break;
		case "二氧化碳":
			arrayData[4] = amount.ToString("F" + Class49.int_8);
			fArrayData[4] = amount;
			break;
		case "丙烷":
			arrayData[7] = amount.ToString("F" + Class49.int_8);
			fArrayData[7] = amount;
			break;
		}
	}

	private void tbTemp_TextChanged(object sender, EventArgs e)
	{
		if (!bLoading)
		{
		}
	}

	private void tbAtm_TextChanged(object sender, EventArgs e)
	{
		if (!bLoading)
		{
		}
	}

	private void tbInjectionVolume_TextChanged(object sender, EventArgs e)
	{
		if (!bLoading)
		{
		}
	}

	private void btnHistory_Click(object sender, EventArgs e)
	{
		if (FormCoalHistory.selfCtrl == null)
		{
			FormCoalHistory formCoalHistory = new FormCoalHistory();
			formCoalHistory.StartPosition = FormStartPosition.CenterScreen;
			formCoalHistory.Show();
			formCoalHistory.loadData();
		}
		else
		{
			FormCoalHistory.selfCtrl.BringToFront();
		}
	}

	private void btnTest_Click(object sender, EventArgs e)
	{
		Class49.InsertIntoCoalTable(0, 0, arrayData, strFileName, strFileName2, strFileName3);
	}

	private void tbInterTemp_TextChanged(object sender, EventArgs e)
	{
		if (!bLoading)
		{
			float.TryParse(tbInterTemp.Text.Trim(), out frmParam.fInterTemp);
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
		this.btnHistory = new System.Windows.Forms.Button();
		this.btnTest = new System.Windows.Forms.Button();
		this.tbInterTemp = new System.Windows.Forms.TextBox();
		this.label1 = new System.Windows.Forms.Label();
		this.label2 = new System.Windows.Forms.Label();
		base.SuspendLayout();
		this.btnHistory.Location = new System.Drawing.Point(133, 22);
		this.btnHistory.Name = "btnHistory";
		this.btnHistory.Size = new System.Drawing.Size(87, 36);
		this.btnHistory.TabIndex = 99;
		this.btnHistory.Text = "数据查询";
		this.btnHistory.UseVisualStyleBackColor = true;
		this.btnHistory.Click += new System.EventHandler(btnHistory_Click);
		this.btnTest.Location = new System.Drawing.Point(713, 119);
		this.btnTest.Name = "btnTest";
		this.btnTest.Size = new System.Drawing.Size(75, 23);
		this.btnTest.TabIndex = 100;
		this.btnTest.Text = "测试";
		this.btnTest.UseVisualStyleBackColor = true;
		this.btnTest.Visible = false;
		this.btnTest.Click += new System.EventHandler(btnTest_Click);
		this.tbInterTemp.Location = new System.Drawing.Point(229, 98);
		this.tbInterTemp.Name = "tbInterTemp";
		this.tbInterTemp.Size = new System.Drawing.Size(100, 21);
		this.tbInterTemp.TabIndex = 101;
		this.tbInterTemp.TextChanged += new System.EventHandler(tbInterTemp_TextChanged);
		this.label1.AutoSize = true;
		this.label1.Location = new System.Drawing.Point(131, 102);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(59, 12);
		this.label1.TabIndex = 102;
		this.label1.Text = "间隔温度:";
		this.label2.AutoSize = true;
		this.label2.Location = new System.Drawing.Point(348, 102);
		this.label2.Name = "label2";
		this.label2.Size = new System.Drawing.Size(17, 12);
		this.label2.TabIndex = 103;
		this.label2.Text = "℃";
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.Controls.Add(this.label2);
		base.Controls.Add(this.label1);
		base.Controls.Add(this.tbInterTemp);
		base.Controls.Add(this.btnTest);
		base.Controls.Add(this.btnHistory);
		base.Name = "CoalCtrl";
		base.Size = new System.Drawing.Size(980, 158);
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
