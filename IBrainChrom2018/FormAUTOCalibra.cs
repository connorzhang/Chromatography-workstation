using System;
using System.ComponentModel;
using System.Windows.Forms;
using IBrainChrom2018.Unit;

namespace IBrainChrom2018;

public class FormAUTOCalibra : Form
{
	private ChromDeviceListMgr cdlMgr = ChromDeviceListMgr.Create();

	private CalibraParam calibraParam = CalibraParam.Create();

	private IContainer components = null;

	private Button btnSaveCalibra;

	private TabControl tabControl1;

	private TabPage tabPage1;

	private TabPage tabPage2;

	private TabPage tabPage3;

	private ListBox lbComp1;

	private GroupBox groupBox3;

	private DataGridView dgLevelAmount;

	private DataGridViewTextBoxColumn Level;

	private DataGridViewTextBoxColumn 浓度;

	private DataGridViewTextBoxColumn 单位;

	private TextBox tbCalibraPoint;

	private DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;

	private DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;

	private DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;

	private Label label3;

	private LclLabel lbCurveFit;

	private LclCusComboBox cbCurveFit;

	private LclLabel lbOriginal;

	private LclCusComboBox cbOriginal;

	private GroupBox gbFunc;

	private Label label5;

	private TextBox tbUnit;

	private Label 组份名;

	private TextBox tbCompName;

	private GroupBox groupBox1;

	private DataGridView dgLevelAmount2;

	private DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;

	private DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;

	private DataGridViewTextBoxColumn dataGridViewTextBoxColumn6;

	private ListBox lbComp2;

	private TabPage tabPage4;

	private GroupBox groupBox2;

	private DataGridView dgLevelAmount3;

	private DataGridViewTextBoxColumn dataGridViewTextBoxColumn7;

	private DataGridViewTextBoxColumn dataGridViewTextBoxColumn8;

	private DataGridViewTextBoxColumn dataGridViewTextBoxColumn9;

	private ListBox lbComp3;

	private TabPage tabPage5;

	private GroupBox groupBox4;

	private DataGridView dgLevelAmount4;

	private DataGridViewTextBoxColumn dataGridViewTextBoxColumn10;

	private DataGridViewTextBoxColumn dataGridViewTextBoxColumn11;

	private DataGridViewTextBoxColumn dataGridViewTextBoxColumn12;

	private ListBox lbComp4;

	private Label label2;

	private Label label1;

	private Label label4;

	private CheckBox cbUsedAutoCalibra;

	private TextBox tbRSDLimit;

	private TextBox tbIntervalTime;

	private TextBox tbCollectTimes;

	private Label label6;

	private Label label7;

	private Label label8;

	private Label label9;

	private TextBox tbSampleDelay;

	private Label label10;

	private Button btnInitCalibraTime;

	public FormAUTOCalibra()
	{
		InitializeComponent();
		cbOriginal.InitItems(new object[3]
		{
			Original.Ignore,
			Original.With,
			Original.Pass
		});
		cbOriginal.InitShowText(new string[3]
		{
			Lang.PS("忽略", "Ignore"),
			Lang.PS("考虑", "Compute With"),
			Lang.PS("经过", "Pass Through")
		});
		cbCurveFit.InitItems(new object[6]
		{
			CurveFit.Free,
			CurveFit.PtToPt,
			CurveFit.Linear,
			CurveFit.Quadratic,
			CurveFit.Cubic,
			CurveFit.Exponent
		});
		cbCurveFit.InitShowText(new string[6]
		{
			Lang.PS("校正归一(基于校正因子)", "Free"),
			Lang.PS("单点校正点到点(基于线性直线)", "Pt to Pt"),
			Lang.PS("单点校正线性(基于线性直线)", "Linear"),
			Lang.PS("多点校正二次(基于工作曲线)", "Quadratic2"),
			Lang.PS("多点校正三次(基于工作曲线)", "Quadratic3"),
			Lang.PS("指数", "Cubic")
		});
		cbUsedAutoCalibra.Checked = calibraParam.bAutoCalibra;
		tbCollectTimes.Text = calibraParam.iCollectTimes.ToString();
		tbIntervalTime.Text = calibraParam.fIntervalTime.ToString();
		tbSampleDelay.Text = calibraParam.iSampleDelay.ToString();
		tbRSDLimit.Text = calibraParam.fRSDLimit.ToString();
		tbUnit.Text = calibraParam.strUnit;
		tbCalibraPoint.Text = calibraParam.iLevel.ToString();
		cbOriginal.SelectedIndex = calibraParam.iOriginalSelect;
		cbCurveFit.SelectedIndex = calibraParam.iCurveFitSelect;
		for (int i = 0; i < calibraParam.autoCalibraComp.Length; i++)
		{
			lbComp1.Items.Add(calibraParam.autoCalibraComp[i].strName);
		}
		for (int j = 0; j < calibraParam.autoCalibraComp2.Length; j++)
		{
			lbComp2.Items.Add(calibraParam.autoCalibraComp2[j].strName);
		}
		for (int k = 0; k < calibraParam.autoCalibraComp3.Length; k++)
		{
			lbComp3.Items.Add(calibraParam.autoCalibraComp3[k].strName);
		}
		for (int l = 0; l < calibraParam.autoCalibraComp4.Length; l++)
		{
			lbComp4.Items.Add(calibraParam.autoCalibraComp4[l].strName);
		}
	}

	private void lbComp1_Click(object sender, EventArgs e)
	{
		dgLevelAmount.Rows.Clear();
		tbCompName.Text = lbComp1.SelectedItem.ToString();
		for (int i = 0; i < calibraParam.iLevel; i++)
		{
			dgLevelAmount.Rows.Add(i + 1, calibraParam.autoCalibraComp[lbComp1.SelectedIndex].fCompAmountLevel[i].ToString("0.0"), calibraParam.strUnit);
		}
	}

	public void reLoad(int index)
	{
		switch (index)
		{
		case 1:
			if (lbComp1.SelectedItem != null)
			{
				dgLevelAmount.Rows.Clear();
				for (int k = 0; k < calibraParam.iLevel; k++)
				{
					dgLevelAmount.Rows.Add(k + 1, calibraParam.autoCalibraComp[lbComp1.SelectedIndex].fCompAmountLevel[k].ToString("0.0"), calibraParam.strUnit);
				}
			}
			break;
		case 2:
			if (lbComp2.SelectedItem != null)
			{
				dgLevelAmount2.Rows.Clear();
				for (int j = 0; j < calibraParam.iLevel; j++)
				{
					dgLevelAmount2.Rows.Add(j + 1, calibraParam.autoCalibraComp2[lbComp2.SelectedIndex].fCompAmountLevel[j].ToString("0.0"), calibraParam.strUnit);
				}
			}
			break;
		case 3:
			if (lbComp3.SelectedItem != null)
			{
				dgLevelAmount3.Rows.Clear();
				for (int l = 0; l < calibraParam.iLevel; l++)
				{
					dgLevelAmount3.Rows.Add(l + 1, calibraParam.autoCalibraComp3[lbComp3.SelectedIndex].fCompAmountLevel[l].ToString("0.0"), calibraParam.strUnit);
				}
			}
			break;
		case 4:
			if (lbComp4.SelectedItem != null)
			{
				dgLevelAmount4.Rows.Clear();
				for (int i = 0; i < calibraParam.iLevel; i++)
				{
					dgLevelAmount4.Rows.Add(i + 1, calibraParam.autoCalibraComp4[lbComp4.SelectedIndex].fCompAmountLevel[i].ToString("0.0"), calibraParam.strUnit);
				}
			}
			break;
		}
	}

	private void btnSaveCalibra_Click(object sender, EventArgs e)
	{
		calibraParam.bAutoCalibra = cbUsedAutoCalibra.Checked;
		int.TryParse(tbCollectTimes.Text, out calibraParam.iCollectTimes);
		float.TryParse(tbIntervalTime.Text, out calibraParam.fIntervalTime);
		int.TryParse(tbSampleDelay.Text, out calibraParam.iSampleDelay);
		double.TryParse(tbRSDLimit.Text, out calibraParam.fRSDLimit);
		int.TryParse(tbCalibraPoint.Text, out calibraParam.iLevel);
		calibraParam.strUnit = tbUnit.Text;
		calibraParam.iOriginalSelect = cbOriginal.SelectedIndex;
		calibraParam.iCurveFitSelect = cbCurveFit.SelectedIndex;
		int selectedIndex = tabControl1.SelectedIndex;
		if (lbComp1.SelectedItem != null)
		{
			if (selectedIndex == 1)
			{
				calibraParam.autoCalibraComp[lbComp1.SelectedIndex].strName = tbCompName.Text;
			}
			if (dgLevelAmount.Rows.Count < calibraParam.iLevel)
			{
				reLoad(1);
			}
			for (int i = 0; i < calibraParam.iLevel; i++)
			{
				calibraParam.autoCalibraComp[lbComp1.SelectedIndex].fCompAmountLevel[i] = float.Parse(dgLevelAmount.Rows[i].Cells[1].Value.ToString());
			}
		}
		if (lbComp2.SelectedItem != null)
		{
			if (selectedIndex == 2)
			{
				calibraParam.autoCalibraComp2[lbComp2.SelectedIndex].strName = tbCompName.Text;
			}
			if (dgLevelAmount2.Rows.Count < calibraParam.iLevel)
			{
				reLoad(2);
			}
			for (int j = 0; j < calibraParam.iLevel; j++)
			{
				calibraParam.autoCalibraComp2[lbComp2.SelectedIndex].fCompAmountLevel[j] = float.Parse(dgLevelAmount2.Rows[j].Cells[1].Value.ToString());
			}
		}
		if (lbComp3.SelectedItem != null)
		{
			if (selectedIndex == 3)
			{
				calibraParam.autoCalibraComp3[lbComp3.SelectedIndex].strName = tbCompName.Text;
			}
			if (dgLevelAmount3.Rows.Count < calibraParam.iLevel)
			{
				reLoad(3);
			}
			for (int k = 0; k < calibraParam.iLevel; k++)
			{
				calibraParam.autoCalibraComp3[lbComp3.SelectedIndex].fCompAmountLevel[k] = float.Parse(dgLevelAmount3.Rows[k].Cells[1].Value.ToString());
			}
		}
		if (lbComp4.SelectedItem != null)
		{
			if (selectedIndex == 4)
			{
				calibraParam.autoCalibraComp4[lbComp4.SelectedIndex].strName = tbCompName.Text;
			}
			if (dgLevelAmount4.Rows.Count < calibraParam.iLevel)
			{
				reLoad(4);
			}
			for (int l = 0; l < calibraParam.iLevel; l++)
			{
				calibraParam.autoCalibraComp4[lbComp4.SelectedIndex].fCompAmountLevel[l] = float.Parse(dgLevelAmount4.Rows[l].Cells[1].Value.ToString());
			}
		}
		calibraParam.SaveParam();
	}

	private void lbComp2_Click(object sender, EventArgs e)
	{
		dgLevelAmount2.Rows.Clear();
		tbCompName.Text = lbComp2.SelectedItem.ToString();
		for (int i = 0; i < calibraParam.iLevel; i++)
		{
			dgLevelAmount2.Rows.Add(i + 1, calibraParam.autoCalibraComp2[lbComp2.SelectedIndex].fCompAmountLevel[i].ToString("0.0"), calibraParam.strUnit);
		}
	}

	private void lbComp3_Click(object sender, EventArgs e)
	{
		dgLevelAmount3.Rows.Clear();
		tbCompName.Text = lbComp3.SelectedItem.ToString();
		for (int i = 0; i < calibraParam.iLevel; i++)
		{
			dgLevelAmount3.Rows.Add(i + 1, calibraParam.autoCalibraComp3[lbComp3.SelectedIndex].fCompAmountLevel[i].ToString("0.0"), calibraParam.strUnit);
		}
	}

	private void lbComp4_Click(object sender, EventArgs e)
	{
		dgLevelAmount4.Rows.Clear();
		tbCompName.Text = lbComp4.SelectedItem.ToString();
		for (int i = 0; i < calibraParam.iLevel; i++)
		{
			dgLevelAmount4.Rows.Add(i + 1, calibraParam.autoCalibraComp4[lbComp4.SelectedIndex].fCompAmountLevel[i].ToString("0.0"), calibraParam.strUnit);
		}
	}

	private void btnInitCalibraTime_Click(object sender, EventArgs e)
	{
		calibraParam.strLastTimeCalibra = DateTime.Now.ToString();
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(IBrainChrom2018.FormAUTOCalibra));
		this.btnSaveCalibra = new System.Windows.Forms.Button();
		this.tabControl1 = new System.Windows.Forms.TabControl();
		this.tabPage1 = new System.Windows.Forms.TabPage();
		this.label9 = new System.Windows.Forms.Label();
		this.tbSampleDelay = new System.Windows.Forms.TextBox();
		this.label10 = new System.Windows.Forms.Label();
		this.label6 = new System.Windows.Forms.Label();
		this.label7 = new System.Windows.Forms.Label();
		this.label8 = new System.Windows.Forms.Label();
		this.tbRSDLimit = new System.Windows.Forms.TextBox();
		this.tbIntervalTime = new System.Windows.Forms.TextBox();
		this.tbCollectTimes = new System.Windows.Forms.TextBox();
		this.cbUsedAutoCalibra = new System.Windows.Forms.CheckBox();
		this.label4 = new System.Windows.Forms.Label();
		this.label2 = new System.Windows.Forms.Label();
		this.label1 = new System.Windows.Forms.Label();
		this.gbFunc = new System.Windows.Forms.GroupBox();
		this.tbUnit = new System.Windows.Forms.TextBox();
		this.label5 = new System.Windows.Forms.Label();
		this.tbCalibraPoint = new System.Windows.Forms.TextBox();
		this.lbCurveFit = new IBrainChrom2018.LclLabel();
		this.cbOriginal = new IBrainChrom2018.LclCusComboBox();
		this.cbCurveFit = new IBrainChrom2018.LclCusComboBox();
		this.lbOriginal = new IBrainChrom2018.LclLabel();
		this.label3 = new System.Windows.Forms.Label();
		this.tabPage2 = new System.Windows.Forms.TabPage();
		this.groupBox3 = new System.Windows.Forms.GroupBox();
		this.dgLevelAmount = new System.Windows.Forms.DataGridView();
		this.Level = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.浓度 = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.单位 = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.lbComp1 = new System.Windows.Forms.ListBox();
		this.tabPage3 = new System.Windows.Forms.TabPage();
		this.groupBox1 = new System.Windows.Forms.GroupBox();
		this.dgLevelAmount2 = new System.Windows.Forms.DataGridView();
		this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.dataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.dataGridViewTextBoxColumn6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.lbComp2 = new System.Windows.Forms.ListBox();
		this.tabPage4 = new System.Windows.Forms.TabPage();
		this.groupBox2 = new System.Windows.Forms.GroupBox();
		this.dgLevelAmount3 = new System.Windows.Forms.DataGridView();
		this.dataGridViewTextBoxColumn7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.dataGridViewTextBoxColumn8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.dataGridViewTextBoxColumn9 = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.lbComp3 = new System.Windows.Forms.ListBox();
		this.tabPage5 = new System.Windows.Forms.TabPage();
		this.groupBox4 = new System.Windows.Forms.GroupBox();
		this.dgLevelAmount4 = new System.Windows.Forms.DataGridView();
		this.dataGridViewTextBoxColumn10 = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.dataGridViewTextBoxColumn11 = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.dataGridViewTextBoxColumn12 = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.lbComp4 = new System.Windows.Forms.ListBox();
		this.组份名 = new System.Windows.Forms.Label();
		this.tbCompName = new System.Windows.Forms.TextBox();
		this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.btnInitCalibraTime = new System.Windows.Forms.Button();
		this.tabControl1.SuspendLayout();
		this.tabPage1.SuspendLayout();
		this.gbFunc.SuspendLayout();
		this.tabPage2.SuspendLayout();
		this.groupBox3.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.dgLevelAmount).BeginInit();
		this.tabPage3.SuspendLayout();
		this.groupBox1.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.dgLevelAmount2).BeginInit();
		this.tabPage4.SuspendLayout();
		this.groupBox2.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.dgLevelAmount3).BeginInit();
		this.tabPage5.SuspendLayout();
		this.groupBox4.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.dgLevelAmount4).BeginInit();
		base.SuspendLayout();
		resources.ApplyResources(this.btnSaveCalibra, "btnSaveCalibra");
		this.btnSaveCalibra.Name = "btnSaveCalibra";
		this.btnSaveCalibra.UseVisualStyleBackColor = true;
		this.btnSaveCalibra.Click += new System.EventHandler(btnSaveCalibra_Click);
		this.tabControl1.Controls.Add(this.tabPage1);
		this.tabControl1.Controls.Add(this.tabPage2);
		this.tabControl1.Controls.Add(this.tabPage3);
		this.tabControl1.Controls.Add(this.tabPage4);
		this.tabControl1.Controls.Add(this.tabPage5);
		resources.ApplyResources(this.tabControl1, "tabControl1");
		this.tabControl1.Name = "tabControl1";
		this.tabControl1.SelectedIndex = 0;
		this.tabPage1.Controls.Add(this.btnInitCalibraTime);
		this.tabPage1.Controls.Add(this.label9);
		this.tabPage1.Controls.Add(this.tbSampleDelay);
		this.tabPage1.Controls.Add(this.label10);
		this.tabPage1.Controls.Add(this.label6);
		this.tabPage1.Controls.Add(this.label7);
		this.tabPage1.Controls.Add(this.label8);
		this.tabPage1.Controls.Add(this.tbRSDLimit);
		this.tabPage1.Controls.Add(this.tbIntervalTime);
		this.tabPage1.Controls.Add(this.tbCollectTimes);
		this.tabPage1.Controls.Add(this.cbUsedAutoCalibra);
		this.tabPage1.Controls.Add(this.label4);
		this.tabPage1.Controls.Add(this.label2);
		this.tabPage1.Controls.Add(this.label1);
		this.tabPage1.Controls.Add(this.gbFunc);
		resources.ApplyResources(this.tabPage1, "tabPage1");
		this.tabPage1.Name = "tabPage1";
		this.tabPage1.UseVisualStyleBackColor = true;
		resources.ApplyResources(this.label9, "label9");
		this.label9.Name = "label9";
		resources.ApplyResources(this.tbSampleDelay, "tbSampleDelay");
		this.tbSampleDelay.Name = "tbSampleDelay";
		resources.ApplyResources(this.label10, "label10");
		this.label10.Name = "label10";
		resources.ApplyResources(this.label6, "label6");
		this.label6.Name = "label6";
		resources.ApplyResources(this.label7, "label7");
		this.label7.Name = "label7";
		resources.ApplyResources(this.label8, "label8");
		this.label8.Name = "label8";
		resources.ApplyResources(this.tbRSDLimit, "tbRSDLimit");
		this.tbRSDLimit.Name = "tbRSDLimit";
		resources.ApplyResources(this.tbIntervalTime, "tbIntervalTime");
		this.tbIntervalTime.Name = "tbIntervalTime";
		resources.ApplyResources(this.tbCollectTimes, "tbCollectTimes");
		this.tbCollectTimes.Name = "tbCollectTimes";
		resources.ApplyResources(this.cbUsedAutoCalibra, "cbUsedAutoCalibra");
		this.cbUsedAutoCalibra.Name = "cbUsedAutoCalibra";
		this.cbUsedAutoCalibra.UseVisualStyleBackColor = true;
		resources.ApplyResources(this.label4, "label4");
		this.label4.Name = "label4";
		resources.ApplyResources(this.label2, "label2");
		this.label2.Name = "label2";
		resources.ApplyResources(this.label1, "label1");
		this.label1.Name = "label1";
		this.gbFunc.Controls.Add(this.tbUnit);
		this.gbFunc.Controls.Add(this.label5);
		this.gbFunc.Controls.Add(this.tbCalibraPoint);
		this.gbFunc.Controls.Add(this.lbCurveFit);
		this.gbFunc.Controls.Add(this.cbOriginal);
		this.gbFunc.Controls.Add(this.cbCurveFit);
		this.gbFunc.Controls.Add(this.lbOriginal);
		this.gbFunc.Controls.Add(this.label3);
		resources.ApplyResources(this.gbFunc, "gbFunc");
		this.gbFunc.Name = "gbFunc";
		this.gbFunc.TabStop = false;
		resources.ApplyResources(this.tbUnit, "tbUnit");
		this.tbUnit.Name = "tbUnit";
		resources.ApplyResources(this.label5, "label5");
		this.label5.Name = "label5";
		resources.ApplyResources(this.tbCalibraPoint, "tbCalibraPoint");
		this.tbCalibraPoint.Name = "tbCalibraPoint";
		resources.ApplyResources(this.lbCurveFit, "lbCurveFit");
		this.lbCurveFit.Name = "lbCurveFit";
		this.cbOriginal.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.cbOriginal.FormattingEnabled = true;
		this.cbOriginal.ItemExtString = "";
		resources.ApplyResources(this.cbOriginal, "cbOriginal");
		this.cbOriginal.Name = "cbOriginal";
		this.cbCurveFit.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.cbCurveFit.FormattingEnabled = true;
		this.cbCurveFit.ItemExtString = "";
		resources.ApplyResources(this.cbCurveFit, "cbCurveFit");
		this.cbCurveFit.Name = "cbCurveFit";
		resources.ApplyResources(this.lbOriginal, "lbOriginal");
		this.lbOriginal.Name = "lbOriginal";
		resources.ApplyResources(this.label3, "label3");
		this.label3.Name = "label3";
		this.tabPage2.Controls.Add(this.groupBox3);
		this.tabPage2.Controls.Add(this.lbComp1);
		resources.ApplyResources(this.tabPage2, "tabPage2");
		this.tabPage2.Name = "tabPage2";
		this.tabPage2.UseVisualStyleBackColor = true;
		this.groupBox3.Controls.Add(this.dgLevelAmount);
		resources.ApplyResources(this.groupBox3, "groupBox3");
		this.groupBox3.Name = "groupBox3";
		this.groupBox3.TabStop = false;
		this.dgLevelAmount.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
		this.dgLevelAmount.Columns.AddRange(this.Level, this.浓度, this.单位);
		resources.ApplyResources(this.dgLevelAmount, "dgLevelAmount");
		this.dgLevelAmount.Name = "dgLevelAmount";
		this.dgLevelAmount.RowTemplate.Height = 23;
		resources.ApplyResources(this.Level, "Level");
		this.Level.Name = "Level";
		this.Level.ReadOnly = true;
		resources.ApplyResources(this.浓度, "浓度");
		this.浓度.Name = "浓度";
		resources.ApplyResources(this.单位, "单位");
		this.单位.Name = "单位";
		this.lbComp1.FormattingEnabled = true;
		resources.ApplyResources(this.lbComp1, "lbComp1");
		this.lbComp1.Name = "lbComp1";
		this.lbComp1.Click += new System.EventHandler(lbComp1_Click);
		this.tabPage3.Controls.Add(this.groupBox1);
		this.tabPage3.Controls.Add(this.lbComp2);
		resources.ApplyResources(this.tabPage3, "tabPage3");
		this.tabPage3.Name = "tabPage3";
		this.tabPage3.UseVisualStyleBackColor = true;
		this.groupBox1.Controls.Add(this.dgLevelAmount2);
		resources.ApplyResources(this.groupBox1, "groupBox1");
		this.groupBox1.Name = "groupBox1";
		this.groupBox1.TabStop = false;
		this.dgLevelAmount2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
		this.dgLevelAmount2.Columns.AddRange(this.dataGridViewTextBoxColumn4, this.dataGridViewTextBoxColumn5, this.dataGridViewTextBoxColumn6);
		resources.ApplyResources(this.dgLevelAmount2, "dgLevelAmount2");
		this.dgLevelAmount2.Name = "dgLevelAmount2";
		this.dgLevelAmount2.RowTemplate.Height = 23;
		resources.ApplyResources(this.dataGridViewTextBoxColumn4, "dataGridViewTextBoxColumn4");
		this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
		this.dataGridViewTextBoxColumn4.ReadOnly = true;
		resources.ApplyResources(this.dataGridViewTextBoxColumn5, "dataGridViewTextBoxColumn5");
		this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
		resources.ApplyResources(this.dataGridViewTextBoxColumn6, "dataGridViewTextBoxColumn6");
		this.dataGridViewTextBoxColumn6.Name = "dataGridViewTextBoxColumn6";
		this.lbComp2.FormattingEnabled = true;
		resources.ApplyResources(this.lbComp2, "lbComp2");
		this.lbComp2.Name = "lbComp2";
		this.lbComp2.Click += new System.EventHandler(lbComp2_Click);
		this.tabPage4.Controls.Add(this.groupBox2);
		this.tabPage4.Controls.Add(this.lbComp3);
		resources.ApplyResources(this.tabPage4, "tabPage4");
		this.tabPage4.Name = "tabPage4";
		this.tabPage4.UseVisualStyleBackColor = true;
		this.groupBox2.Controls.Add(this.dgLevelAmount3);
		resources.ApplyResources(this.groupBox2, "groupBox2");
		this.groupBox2.Name = "groupBox2";
		this.groupBox2.TabStop = false;
		this.dgLevelAmount3.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
		this.dgLevelAmount3.Columns.AddRange(this.dataGridViewTextBoxColumn7, this.dataGridViewTextBoxColumn8, this.dataGridViewTextBoxColumn9);
		resources.ApplyResources(this.dgLevelAmount3, "dgLevelAmount3");
		this.dgLevelAmount3.Name = "dgLevelAmount3";
		this.dgLevelAmount3.RowTemplate.Height = 23;
		resources.ApplyResources(this.dataGridViewTextBoxColumn7, "dataGridViewTextBoxColumn7");
		this.dataGridViewTextBoxColumn7.Name = "dataGridViewTextBoxColumn7";
		this.dataGridViewTextBoxColumn7.ReadOnly = true;
		resources.ApplyResources(this.dataGridViewTextBoxColumn8, "dataGridViewTextBoxColumn8");
		this.dataGridViewTextBoxColumn8.Name = "dataGridViewTextBoxColumn8";
		resources.ApplyResources(this.dataGridViewTextBoxColumn9, "dataGridViewTextBoxColumn9");
		this.dataGridViewTextBoxColumn9.Name = "dataGridViewTextBoxColumn9";
		this.lbComp3.FormattingEnabled = true;
		resources.ApplyResources(this.lbComp3, "lbComp3");
		this.lbComp3.Name = "lbComp3";
		this.lbComp3.Click += new System.EventHandler(lbComp3_Click);
		this.tabPage5.Controls.Add(this.groupBox4);
		this.tabPage5.Controls.Add(this.lbComp4);
		resources.ApplyResources(this.tabPage5, "tabPage5");
		this.tabPage5.Name = "tabPage5";
		this.tabPage5.UseVisualStyleBackColor = true;
		this.groupBox4.Controls.Add(this.dgLevelAmount4);
		resources.ApplyResources(this.groupBox4, "groupBox4");
		this.groupBox4.Name = "groupBox4";
		this.groupBox4.TabStop = false;
		this.dgLevelAmount4.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
		this.dgLevelAmount4.Columns.AddRange(this.dataGridViewTextBoxColumn10, this.dataGridViewTextBoxColumn11, this.dataGridViewTextBoxColumn12);
		resources.ApplyResources(this.dgLevelAmount4, "dgLevelAmount4");
		this.dgLevelAmount4.Name = "dgLevelAmount4";
		this.dgLevelAmount4.RowTemplate.Height = 23;
		resources.ApplyResources(this.dataGridViewTextBoxColumn10, "dataGridViewTextBoxColumn10");
		this.dataGridViewTextBoxColumn10.Name = "dataGridViewTextBoxColumn10";
		this.dataGridViewTextBoxColumn10.ReadOnly = true;
		resources.ApplyResources(this.dataGridViewTextBoxColumn11, "dataGridViewTextBoxColumn11");
		this.dataGridViewTextBoxColumn11.Name = "dataGridViewTextBoxColumn11";
		resources.ApplyResources(this.dataGridViewTextBoxColumn12, "dataGridViewTextBoxColumn12");
		this.dataGridViewTextBoxColumn12.Name = "dataGridViewTextBoxColumn12";
		this.lbComp4.FormattingEnabled = true;
		resources.ApplyResources(this.lbComp4, "lbComp4");
		this.lbComp4.Name = "lbComp4";
		this.lbComp4.Click += new System.EventHandler(lbComp4_Click);
		resources.ApplyResources(this.组份名, "组份名");
		this.组份名.Name = "组份名";
		resources.ApplyResources(this.tbCompName, "tbCompName");
		this.tbCompName.Name = "tbCompName";
		resources.ApplyResources(this.dataGridViewTextBoxColumn1, "dataGridViewTextBoxColumn1");
		this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
		this.dataGridViewTextBoxColumn1.ReadOnly = true;
		resources.ApplyResources(this.dataGridViewTextBoxColumn2, "dataGridViewTextBoxColumn2");
		this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
		resources.ApplyResources(this.dataGridViewTextBoxColumn3, "dataGridViewTextBoxColumn3");
		this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
		resources.ApplyResources(this.btnInitCalibraTime, "btnInitCalibraTime");
		this.btnInitCalibraTime.Name = "btnInitCalibraTime";
		this.btnInitCalibraTime.UseVisualStyleBackColor = true;
		this.btnInitCalibraTime.Click += new System.EventHandler(btnInitCalibraTime_Click);
		resources.ApplyResources(this, "$this");
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.Controls.Add(this.tbCompName);
		base.Controls.Add(this.组份名);
		base.Controls.Add(this.btnSaveCalibra);
		base.Controls.Add(this.tabControl1);
		base.Name = "FormAUTOCalibra";
		this.tabControl1.ResumeLayout(false);
		this.tabPage1.ResumeLayout(false);
		this.tabPage1.PerformLayout();
		this.gbFunc.ResumeLayout(false);
		this.gbFunc.PerformLayout();
		this.tabPage2.ResumeLayout(false);
		this.groupBox3.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.dgLevelAmount).EndInit();
		this.tabPage3.ResumeLayout(false);
		this.groupBox1.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.dgLevelAmount2).EndInit();
		this.tabPage4.ResumeLayout(false);
		this.groupBox2.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.dgLevelAmount3).EndInit();
		this.tabPage5.ResumeLayout(false);
		this.groupBox4.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.dgLevelAmount4).EndInit();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
