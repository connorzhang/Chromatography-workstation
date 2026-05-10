using System;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using IBrainChrom2018.Unit;

namespace IBrainChrom2018;

public class FormCollectHistory : Form
{
	private ChromDeviceListMgr cdlMgr = ChromDeviceListMgr.Create();

	private LYTHCPara lythcParamMgr = LYTHCPara.Create();

	public bool bLoading = true;

	private IContainer components = null;

	private DataGridView dataGridView1;

	private Label label1;

	private Label label2;

	private Label label3;

	private Label label4;

	private Label label5;

	private Label label6;

	private Button btnPrintf;

	private Label label7;

	private Label labCollectSite;

	private DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;

	private DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;

	private DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;

	private Label labCollectTimes;

	private Label label9;

	private Label labThcAver;

	private Label labCH4Avr;

	private Label labCH4Max;

	private Label labThcMax;

	private Label labCH4Min;

	private Label labThcMin;

	private Label labNMHCMin;

	private Label labNMHCMax;

	private Label labNMHCAvr;

	private Label labCollectP;

	private Label label10;

	private DataGridView dataGridView2;

	private GroupBox groupBox1;

	private GroupBox groupBox2;

	private GroupBox groupBox3;

	private Label lbBTEX8T;

	private Label lbBTEXt;

	private Label lbBTEX7T;

	private Label lbBTEX5T;

	private Label lbBTEX6T;

	private Label lbBTEX4T;

	private Label lbBTEX2T;

	private Label lbBTEX1T;

	private Label lbBTEX3T;

	private Label label34;

	private Label label35;

	private Label label36;

	private Label label31;

	private Label label32;

	private Label label33;

	private Label label28;

	private Label label29;

	private Label label30;

	private Label label25;

	private Label label26;

	private Label label27;

	private Label label22;

	private Label label23;

	private Label label24;

	private Label label19;

	private Label label20;

	private Label label21;

	private Label label16;

	private Label label17;

	private Label label18;

	private Label label13;

	private Label label14;

	private Label label15;

	private Label label8;

	private Label label11;

	private Label label12;

	public ComboBox cbPeakName;

	public Label label37;

	public FormCollectHistory()
	{
		InitializeComponent();
		labCollectSite.Text = lythcParamMgr.strCollectSite;
		labCollectP.Text = lythcParamMgr.strCollectP;
		initCbPeak();
		if (lythcParamMgr.detectorMode == 0)
		{
			groupBox1.Visible = false;
			dataGridView2.Visible = false;
		}
		bLoading = false;
	}

	private void btnPrintf_Click(object sender, EventArgs e)
	{
	}

	public void initCbPeak()
	{
		string connectionString = "Data Source='" + Application.StartupPath + "\\ngmpolHis.dll';Version=3;";
		string text = "";
		StringBuilder stringBuilder = new StringBuilder();
		using SQLiteConnection sQLiteConnection = new SQLiteConnection(connectionString);
		sQLiteConnection.Open();
		string commandText = "select name from sqlite_master where type='table' order by name;";
		SQLiteCommand sQLiteCommand = new SQLiteCommand(commandText, sQLiteConnection);
		using SQLiteDataReader sQLiteDataReader = sQLiteCommand.ExecuteReader();
		while (sQLiteDataReader.Read())
		{
			text = sQLiteDataReader["Name"].ToString();
			if (text != "VOC" && text != "OLog")
			{
				cbPeakName.Items.Add(text);
			}
		}
	}

	public void loadData()
	{
		try
		{
			float num = 0f;
			float num2 = 0f;
			float num3 = 0f;
			float num4 = 0f;
			float num5 = 0f;
			float num6 = 0f;
			float num7 = 0f;
			float num8 = 0f;
			float num9 = 0f;
			string strSql = "select * from '" + cbPeakName.Text.Trim() + "'";
			DataTable dataTable = Class49.GetDataTable(strSql, "ngmpolHis.dll");
			dataGridView1.DataSource = dataTable;
			dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
			labCollectTimes.Text = (dataGridView1.Rows.Count - 1).ToString();
			for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
			{
				float num10 = Class49.String2Float(dataGridView1.Rows[i].Cells[0].Value, 0f);
				if (num2 < num10)
				{
					num2 = num10;
				}
				if (i == 0)
				{
					num3 = num10;
				}
				if (num3 > num10)
				{
					num3 = num10;
				}
				num += num10;
				num10 = Class49.String2Float(dataGridView1.Rows[i].Cells[1].Value, 0f);
				if (num5 < num10)
				{
					num5 = num10;
				}
				if (i == 0)
				{
					num6 = num10;
				}
				if (num6 > num10)
				{
					num6 = num10;
				}
				num4 += num10;
				num10 = Class49.String2Float(dataGridView1.Rows[i].Cells[2].Value, 0f);
				if (num8 < num10)
				{
					num8 = num10;
				}
				if (i == 0)
				{
					num9 = num10;
				}
				if (num9 > num10)
				{
					num9 = num10;
				}
				num7 += num10;
			}
			if (LYTHCtrl2.selfCtrl != null)
			{
				labThcAver.Text = (num / (float)(dataGridView1.Rows.Count - 1)).ToString("0.00");
				labThcMax.Text = num2.ToString();
				labThcMin.Text = num3.ToString();
				labCH4Avr.Text = (num4 / (float)(dataGridView1.Rows.Count - 1)).ToString("0.00");
				labCH4Max.Text = num5.ToString();
				labCH4Min.Text = num6.ToString();
				labNMHCAvr.Text = (num7 / (float)(dataGridView1.Rows.Count - 1)).ToString("0.00");
				labNMHCMax.Text = num8.ToString();
				labNMHCMin.Text = num9.ToString();
			}
			if (lythcParamMgr.detectorMode == 1)
			{
				DataTable dataTable2 = Class49.GetDataTable("select * from [" + cbPeakName.Text + "]", "ngmpolHis.dll");
				dataGridView2.DataSource = dataTable2;
				dataGridView2.Columns[0].Width = 50;
				dataGridView2.Columns[1].Width = 50;
				dataGridView2.Columns[2].Width = 80;
			}
		}
		catch (Exception)
		{
		}
	}

	private void cbPeakName_SelectedIndexChanged(object sender, EventArgs e)
	{
		if (!bLoading)
		{
			loadData();
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(IBrainChrom2018.FormCollectHistory));
		this.dataGridView1 = new System.Windows.Forms.DataGridView();
		this.label1 = new System.Windows.Forms.Label();
		this.label2 = new System.Windows.Forms.Label();
		this.label3 = new System.Windows.Forms.Label();
		this.label4 = new System.Windows.Forms.Label();
		this.label5 = new System.Windows.Forms.Label();
		this.label6 = new System.Windows.Forms.Label();
		this.btnPrintf = new System.Windows.Forms.Button();
		this.label7 = new System.Windows.Forms.Label();
		this.labCollectSite = new System.Windows.Forms.Label();
		this.labCollectTimes = new System.Windows.Forms.Label();
		this.label9 = new System.Windows.Forms.Label();
		this.labThcAver = new System.Windows.Forms.Label();
		this.labCH4Avr = new System.Windows.Forms.Label();
		this.labCH4Max = new System.Windows.Forms.Label();
		this.labThcMax = new System.Windows.Forms.Label();
		this.labCH4Min = new System.Windows.Forms.Label();
		this.labThcMin = new System.Windows.Forms.Label();
		this.labNMHCMin = new System.Windows.Forms.Label();
		this.labNMHCMax = new System.Windows.Forms.Label();
		this.labNMHCAvr = new System.Windows.Forms.Label();
		this.labCollectP = new System.Windows.Forms.Label();
		this.label10 = new System.Windows.Forms.Label();
		this.dataGridView2 = new System.Windows.Forms.DataGridView();
		this.groupBox1 = new System.Windows.Forms.GroupBox();
		this.label34 = new System.Windows.Forms.Label();
		this.label35 = new System.Windows.Forms.Label();
		this.label36 = new System.Windows.Forms.Label();
		this.label31 = new System.Windows.Forms.Label();
		this.label32 = new System.Windows.Forms.Label();
		this.label33 = new System.Windows.Forms.Label();
		this.label28 = new System.Windows.Forms.Label();
		this.label29 = new System.Windows.Forms.Label();
		this.label30 = new System.Windows.Forms.Label();
		this.label25 = new System.Windows.Forms.Label();
		this.label26 = new System.Windows.Forms.Label();
		this.label27 = new System.Windows.Forms.Label();
		this.label22 = new System.Windows.Forms.Label();
		this.label23 = new System.Windows.Forms.Label();
		this.label24 = new System.Windows.Forms.Label();
		this.label19 = new System.Windows.Forms.Label();
		this.label20 = new System.Windows.Forms.Label();
		this.label21 = new System.Windows.Forms.Label();
		this.label16 = new System.Windows.Forms.Label();
		this.label17 = new System.Windows.Forms.Label();
		this.label18 = new System.Windows.Forms.Label();
		this.label13 = new System.Windows.Forms.Label();
		this.label14 = new System.Windows.Forms.Label();
		this.label15 = new System.Windows.Forms.Label();
		this.label8 = new System.Windows.Forms.Label();
		this.label11 = new System.Windows.Forms.Label();
		this.label12 = new System.Windows.Forms.Label();
		this.lbBTEX8T = new System.Windows.Forms.Label();
		this.lbBTEXt = new System.Windows.Forms.Label();
		this.lbBTEX7T = new System.Windows.Forms.Label();
		this.lbBTEX5T = new System.Windows.Forms.Label();
		this.lbBTEX6T = new System.Windows.Forms.Label();
		this.lbBTEX4T = new System.Windows.Forms.Label();
		this.lbBTEX2T = new System.Windows.Forms.Label();
		this.lbBTEX1T = new System.Windows.Forms.Label();
		this.lbBTEX3T = new System.Windows.Forms.Label();
		this.groupBox2 = new System.Windows.Forms.GroupBox();
		this.groupBox3 = new System.Windows.Forms.GroupBox();
		this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.cbPeakName = new System.Windows.Forms.ComboBox();
		this.label37 = new System.Windows.Forms.Label();
		((System.ComponentModel.ISupportInitialize)this.dataGridView1).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.dataGridView2).BeginInit();
		this.groupBox1.SuspendLayout();
		this.groupBox2.SuspendLayout();
		this.groupBox3.SuspendLayout();
		base.SuspendLayout();
		this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
		this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Left;
		this.dataGridView1.Location = new System.Drawing.Point(0, 0);
		this.dataGridView1.Name = "dataGridView1";
		this.dataGridView1.RowTemplate.Height = 23;
		this.dataGridView1.Size = new System.Drawing.Size(454, 374);
		this.dataGridView1.TabIndex = 0;
		this.label1.AutoSize = true;
		this.label1.Location = new System.Drawing.Point(12, 22);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(65, 12);
		this.label1.TabIndex = 1;
		this.label1.Text = "总      烃";
		this.label2.AutoSize = true;
		this.label2.Location = new System.Drawing.Point(12, 51);
		this.label2.Name = "label2";
		this.label2.Size = new System.Drawing.Size(65, 12);
		this.label2.TabIndex = 2;
		this.label2.Text = "甲      烷";
		this.label3.AutoSize = true;
		this.label3.Location = new System.Drawing.Point(12, 85);
		this.label3.Name = "label3";
		this.label3.Size = new System.Drawing.Size(65, 12);
		this.label3.TabIndex = 3;
		this.label3.Text = "非甲烷总烃";
		this.label4.AutoSize = true;
		this.label4.Location = new System.Drawing.Point(547, 5);
		this.label4.Name = "label4";
		this.label4.Size = new System.Drawing.Size(41, 12);
		this.label4.TabIndex = 4;
		this.label4.Text = "平均值";
		this.label5.AutoSize = true;
		this.label5.Location = new System.Drawing.Point(602, 5);
		this.label5.Name = "label5";
		this.label5.Size = new System.Drawing.Size(41, 12);
		this.label5.TabIndex = 5;
		this.label5.Text = "最大值";
		this.label6.AutoSize = true;
		this.label6.Location = new System.Drawing.Point(661, 5);
		this.label6.Name = "label6";
		this.label6.Size = new System.Drawing.Size(41, 12);
		this.label6.TabIndex = 6;
		this.label6.Text = "最小值";
		this.btnPrintf.Location = new System.Drawing.Point(471, 321);
		this.btnPrintf.Name = "btnPrintf";
		this.btnPrintf.Size = new System.Drawing.Size(182, 38);
		this.btnPrintf.TabIndex = 7;
		this.btnPrintf.Text = "打印结果";
		this.btnPrintf.UseVisualStyleBackColor = true;
		this.btnPrintf.Click += new System.EventHandler(btnPrintf_Click);
		this.label7.AutoSize = true;
		this.label7.Location = new System.Drawing.Point(8, 55);
		this.label7.Name = "label7";
		this.label7.Size = new System.Drawing.Size(65, 12);
		this.label7.TabIndex = 8;
		this.label7.Text = "采样地点：";
		this.labCollectSite.AutoSize = true;
		this.labCollectSite.Location = new System.Drawing.Point(79, 55);
		this.labCollectSite.Name = "labCollectSite";
		this.labCollectSite.Size = new System.Drawing.Size(29, 12);
		this.labCollectSite.TabIndex = 9;
		this.labCollectSite.Text = "济南";
		this.labCollectTimes.AutoSize = true;
		this.labCollectTimes.Location = new System.Drawing.Point(88, 28);
		this.labCollectTimes.Name = "labCollectTimes";
		this.labCollectTimes.Size = new System.Drawing.Size(11, 12);
		this.labCollectTimes.TabIndex = 11;
		this.labCollectTimes.Text = "0";
		this.label9.AutoSize = true;
		this.label9.Location = new System.Drawing.Point(8, 28);
		this.label9.Name = "label9";
		this.label9.Size = new System.Drawing.Size(65, 12);
		this.label9.TabIndex = 10;
		this.label9.Text = "采样次数：";
		this.labThcAver.AutoSize = true;
		this.labThcAver.Location = new System.Drawing.Point(92, 22);
		this.labThcAver.Name = "labThcAver";
		this.labThcAver.Size = new System.Drawing.Size(11, 12);
		this.labThcAver.TabIndex = 12;
		this.labThcAver.Text = "0";
		this.labCH4Avr.AutoSize = true;
		this.labCH4Avr.Location = new System.Drawing.Point(92, 51);
		this.labCH4Avr.Name = "labCH4Avr";
		this.labCH4Avr.Size = new System.Drawing.Size(11, 12);
		this.labCH4Avr.TabIndex = 13;
		this.labCH4Avr.Text = "0";
		this.labCH4Max.AutoSize = true;
		this.labCH4Max.Location = new System.Drawing.Point(146, 51);
		this.labCH4Max.Name = "labCH4Max";
		this.labCH4Max.Size = new System.Drawing.Size(11, 12);
		this.labCH4Max.TabIndex = 15;
		this.labCH4Max.Text = "0";
		this.labThcMax.AutoSize = true;
		this.labThcMax.Location = new System.Drawing.Point(146, 22);
		this.labThcMax.Name = "labThcMax";
		this.labThcMax.Size = new System.Drawing.Size(11, 12);
		this.labThcMax.TabIndex = 14;
		this.labThcMax.Text = "0";
		this.labCH4Min.AutoSize = true;
		this.labCH4Min.Location = new System.Drawing.Point(200, 51);
		this.labCH4Min.Name = "labCH4Min";
		this.labCH4Min.Size = new System.Drawing.Size(11, 12);
		this.labCH4Min.TabIndex = 17;
		this.labCH4Min.Text = "0";
		this.labThcMin.AutoSize = true;
		this.labThcMin.Location = new System.Drawing.Point(200, 22);
		this.labThcMin.Name = "labThcMin";
		this.labThcMin.Size = new System.Drawing.Size(11, 12);
		this.labThcMin.TabIndex = 16;
		this.labThcMin.Text = "0";
		this.labNMHCMin.AutoSize = true;
		this.labNMHCMin.Location = new System.Drawing.Point(200, 85);
		this.labNMHCMin.Name = "labNMHCMin";
		this.labNMHCMin.Size = new System.Drawing.Size(11, 12);
		this.labNMHCMin.TabIndex = 20;
		this.labNMHCMin.Text = "0";
		this.labNMHCMax.AutoSize = true;
		this.labNMHCMax.Location = new System.Drawing.Point(146, 85);
		this.labNMHCMax.Name = "labNMHCMax";
		this.labNMHCMax.Size = new System.Drawing.Size(11, 12);
		this.labNMHCMax.TabIndex = 19;
		this.labNMHCMax.Text = "0";
		this.labNMHCAvr.AutoSize = true;
		this.labNMHCAvr.Location = new System.Drawing.Point(92, 85);
		this.labNMHCAvr.Name = "labNMHCAvr";
		this.labNMHCAvr.Size = new System.Drawing.Size(11, 12);
		this.labNMHCAvr.TabIndex = 18;
		this.labNMHCAvr.Text = "0";
		this.labCollectP.AutoSize = true;
		this.labCollectP.Location = new System.Drawing.Point(79, 84);
		this.labCollectP.Name = "labCollectP";
		this.labCollectP.Size = new System.Drawing.Size(41, 12);
		this.labCollectP.TabIndex = 22;
		this.labCollectP.Text = "负责人";
		this.label10.AutoSize = true;
		this.label10.Location = new System.Drawing.Point(8, 84);
		this.label10.Name = "label10";
		this.label10.Size = new System.Drawing.Size(53, 12);
		this.label10.TabIndex = 21;
		this.label10.Text = "采样人：";
		this.dataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
		this.dataGridView2.Location = new System.Drawing.Point(3, 555);
		this.dataGridView2.Name = "dataGridView2";
		this.dataGridView2.RowTemplate.Height = 23;
		this.dataGridView2.Size = new System.Drawing.Size(678, 24);
		this.dataGridView2.TabIndex = 23;
		this.groupBox1.Controls.Add(this.label34);
		this.groupBox1.Controls.Add(this.label35);
		this.groupBox1.Controls.Add(this.label36);
		this.groupBox1.Controls.Add(this.label31);
		this.groupBox1.Controls.Add(this.label32);
		this.groupBox1.Controls.Add(this.label33);
		this.groupBox1.Controls.Add(this.label28);
		this.groupBox1.Controls.Add(this.label29);
		this.groupBox1.Controls.Add(this.label30);
		this.groupBox1.Controls.Add(this.label25);
		this.groupBox1.Controls.Add(this.label26);
		this.groupBox1.Controls.Add(this.label27);
		this.groupBox1.Controls.Add(this.label22);
		this.groupBox1.Controls.Add(this.label23);
		this.groupBox1.Controls.Add(this.label24);
		this.groupBox1.Controls.Add(this.label19);
		this.groupBox1.Controls.Add(this.label20);
		this.groupBox1.Controls.Add(this.label21);
		this.groupBox1.Controls.Add(this.label16);
		this.groupBox1.Controls.Add(this.label17);
		this.groupBox1.Controls.Add(this.label18);
		this.groupBox1.Controls.Add(this.label13);
		this.groupBox1.Controls.Add(this.label14);
		this.groupBox1.Controls.Add(this.label15);
		this.groupBox1.Controls.Add(this.label8);
		this.groupBox1.Controls.Add(this.label11);
		this.groupBox1.Controls.Add(this.label12);
		this.groupBox1.Controls.Add(this.lbBTEX8T);
		this.groupBox1.Controls.Add(this.lbBTEXt);
		this.groupBox1.Controls.Add(this.lbBTEX7T);
		this.groupBox1.Controls.Add(this.lbBTEX5T);
		this.groupBox1.Controls.Add(this.lbBTEX6T);
		this.groupBox1.Controls.Add(this.lbBTEX4T);
		this.groupBox1.Controls.Add(this.lbBTEX2T);
		this.groupBox1.Controls.Add(this.lbBTEX1T);
		this.groupBox1.Controls.Add(this.lbBTEX3T);
		this.groupBox1.Location = new System.Drawing.Point(647, 339);
		this.groupBox1.Name = "groupBox1";
		this.groupBox1.Size = new System.Drawing.Size(45, 10);
		this.groupBox1.TabIndex = 24;
		this.groupBox1.TabStop = false;
		this.groupBox1.Text = "苯系物";
		this.label34.AutoSize = true;
		this.label34.Location = new System.Drawing.Point(200, 193);
		this.label34.Name = "label34";
		this.label34.Size = new System.Drawing.Size(11, 12);
		this.label34.TabIndex = 109;
		this.label34.Text = "0";
		this.label35.AutoSize = true;
		this.label35.Location = new System.Drawing.Point(146, 193);
		this.label35.Name = "label35";
		this.label35.Size = new System.Drawing.Size(11, 12);
		this.label35.TabIndex = 108;
		this.label35.Text = "0";
		this.label36.AutoSize = true;
		this.label36.Location = new System.Drawing.Point(92, 193);
		this.label36.Name = "label36";
		this.label36.Size = new System.Drawing.Size(11, 12);
		this.label36.TabIndex = 107;
		this.label36.Text = "0";
		this.label31.AutoSize = true;
		this.label31.Location = new System.Drawing.Point(200, 166);
		this.label31.Name = "label31";
		this.label31.Size = new System.Drawing.Size(11, 12);
		this.label31.TabIndex = 106;
		this.label31.Text = "0";
		this.label32.AutoSize = true;
		this.label32.Location = new System.Drawing.Point(146, 166);
		this.label32.Name = "label32";
		this.label32.Size = new System.Drawing.Size(11, 12);
		this.label32.TabIndex = 105;
		this.label32.Text = "0";
		this.label33.AutoSize = true;
		this.label33.Location = new System.Drawing.Point(92, 166);
		this.label33.Name = "label33";
		this.label33.Size = new System.Drawing.Size(11, 12);
		this.label33.TabIndex = 104;
		this.label33.Text = "0";
		this.label28.AutoSize = true;
		this.label28.Location = new System.Drawing.Point(200, 144);
		this.label28.Name = "label28";
		this.label28.Size = new System.Drawing.Size(11, 12);
		this.label28.TabIndex = 103;
		this.label28.Text = "0";
		this.label29.AutoSize = true;
		this.label29.Location = new System.Drawing.Point(146, 144);
		this.label29.Name = "label29";
		this.label29.Size = new System.Drawing.Size(11, 12);
		this.label29.TabIndex = 102;
		this.label29.Text = "0";
		this.label30.AutoSize = true;
		this.label30.Location = new System.Drawing.Point(92, 144);
		this.label30.Name = "label30";
		this.label30.Size = new System.Drawing.Size(11, 12);
		this.label30.TabIndex = 101;
		this.label30.Text = "0";
		this.label25.AutoSize = true;
		this.label25.Location = new System.Drawing.Point(200, 122);
		this.label25.Name = "label25";
		this.label25.Size = new System.Drawing.Size(11, 12);
		this.label25.TabIndex = 100;
		this.label25.Text = "0";
		this.label26.AutoSize = true;
		this.label26.Location = new System.Drawing.Point(146, 122);
		this.label26.Name = "label26";
		this.label26.Size = new System.Drawing.Size(11, 12);
		this.label26.TabIndex = 99;
		this.label26.Text = "0";
		this.label27.AutoSize = true;
		this.label27.Location = new System.Drawing.Point(92, 122);
		this.label27.Name = "label27";
		this.label27.Size = new System.Drawing.Size(11, 12);
		this.label27.TabIndex = 98;
		this.label27.Text = "0";
		this.label22.AutoSize = true;
		this.label22.Location = new System.Drawing.Point(200, 100);
		this.label22.Name = "label22";
		this.label22.Size = new System.Drawing.Size(11, 12);
		this.label22.TabIndex = 97;
		this.label22.Text = "0";
		this.label23.AutoSize = true;
		this.label23.Location = new System.Drawing.Point(146, 100);
		this.label23.Name = "label23";
		this.label23.Size = new System.Drawing.Size(11, 12);
		this.label23.TabIndex = 96;
		this.label23.Text = "0";
		this.label24.AutoSize = true;
		this.label24.Location = new System.Drawing.Point(92, 100);
		this.label24.Name = "label24";
		this.label24.Size = new System.Drawing.Size(11, 12);
		this.label24.TabIndex = 95;
		this.label24.Text = "0";
		this.label19.AutoSize = true;
		this.label19.Location = new System.Drawing.Point(200, 77);
		this.label19.Name = "label19";
		this.label19.Size = new System.Drawing.Size(11, 12);
		this.label19.TabIndex = 94;
		this.label19.Text = "0";
		this.label20.AutoSize = true;
		this.label20.Location = new System.Drawing.Point(146, 77);
		this.label20.Name = "label20";
		this.label20.Size = new System.Drawing.Size(11, 12);
		this.label20.TabIndex = 93;
		this.label20.Text = "0";
		this.label21.AutoSize = true;
		this.label21.Location = new System.Drawing.Point(92, 77);
		this.label21.Name = "label21";
		this.label21.Size = new System.Drawing.Size(11, 12);
		this.label21.TabIndex = 92;
		this.label21.Text = "0";
		this.label16.AutoSize = true;
		this.label16.Location = new System.Drawing.Point(200, 56);
		this.label16.Name = "label16";
		this.label16.Size = new System.Drawing.Size(11, 12);
		this.label16.TabIndex = 91;
		this.label16.Text = "0";
		this.label17.AutoSize = true;
		this.label17.Location = new System.Drawing.Point(146, 56);
		this.label17.Name = "label17";
		this.label17.Size = new System.Drawing.Size(11, 12);
		this.label17.TabIndex = 90;
		this.label17.Text = "0";
		this.label18.AutoSize = true;
		this.label18.Location = new System.Drawing.Point(92, 56);
		this.label18.Name = "label18";
		this.label18.Size = new System.Drawing.Size(11, 12);
		this.label18.TabIndex = 89;
		this.label18.Text = "0";
		this.label13.AutoSize = true;
		this.label13.Location = new System.Drawing.Point(200, 36);
		this.label13.Name = "label13";
		this.label13.Size = new System.Drawing.Size(11, 12);
		this.label13.TabIndex = 88;
		this.label13.Text = "0";
		this.label14.AutoSize = true;
		this.label14.Location = new System.Drawing.Point(146, 36);
		this.label14.Name = "label14";
		this.label14.Size = new System.Drawing.Size(11, 12);
		this.label14.TabIndex = 87;
		this.label14.Text = "0";
		this.label15.AutoSize = true;
		this.label15.Location = new System.Drawing.Point(92, 36);
		this.label15.Name = "label15";
		this.label15.Size = new System.Drawing.Size(11, 12);
		this.label15.TabIndex = 86;
		this.label15.Text = "0";
		this.label8.AutoSize = true;
		this.label8.Location = new System.Drawing.Point(200, 18);
		this.label8.Name = "label8";
		this.label8.Size = new System.Drawing.Size(11, 12);
		this.label8.TabIndex = 85;
		this.label8.Text = "0";
		this.label11.AutoSize = true;
		this.label11.Location = new System.Drawing.Point(146, 18);
		this.label11.Name = "label11";
		this.label11.Size = new System.Drawing.Size(11, 12);
		this.label11.TabIndex = 84;
		this.label11.Text = "0";
		this.label12.AutoSize = true;
		this.label12.Location = new System.Drawing.Point(92, 18);
		this.label12.Name = "label12";
		this.label12.Size = new System.Drawing.Size(11, 12);
		this.label12.TabIndex = 83;
		this.label12.Text = "0";
		this.lbBTEX8T.AutoSize = true;
		this.lbBTEX8T.Font = new System.Drawing.Font("SimSun", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		this.lbBTEX8T.Location = new System.Drawing.Point(18, 169);
		this.lbBTEX8T.Name = "lbBTEX8T";
		this.lbBTEX8T.Size = new System.Drawing.Size(47, 12);
		this.lbBTEX8T.TabIndex = 82;
		this.lbBTEX8T.Text = "苯乙烯:";
		this.lbBTEXt.AutoSize = true;
		this.lbBTEXt.Font = new System.Drawing.Font("SimSun", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		this.lbBTEXt.Location = new System.Drawing.Point(18, 193);
		this.lbBTEXt.Name = "lbBTEXt";
		this.lbBTEXt.Size = new System.Drawing.Size(47, 12);
		this.lbBTEXt.TabIndex = 80;
		this.lbBTEXt.Text = "苯系物:";
		this.lbBTEX7T.AutoSize = true;
		this.lbBTEX7T.Font = new System.Drawing.Font("SimSun", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		this.lbBTEX7T.Location = new System.Drawing.Point(6, 144);
		this.lbBTEX7T.Name = "lbBTEX7T";
		this.lbBTEX7T.Size = new System.Drawing.Size(59, 12);
		this.lbBTEX7T.TabIndex = 78;
		this.lbBTEX7T.Text = "邻二甲苯:";
		this.lbBTEX5T.AutoSize = true;
		this.lbBTEX5T.Font = new System.Drawing.Font("SimSun", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		this.lbBTEX5T.Location = new System.Drawing.Point(6, 100);
		this.lbBTEX5T.Name = "lbBTEX5T";
		this.lbBTEX5T.Size = new System.Drawing.Size(59, 12);
		this.lbBTEX5T.TabIndex = 74;
		this.lbBTEX5T.Text = "间二甲苯:";
		this.lbBTEX6T.AutoSize = true;
		this.lbBTEX6T.Font = new System.Drawing.Font("SimSun", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		this.lbBTEX6T.Location = new System.Drawing.Point(18, 122);
		this.lbBTEX6T.Name = "lbBTEX6T";
		this.lbBTEX6T.Size = new System.Drawing.Size(47, 12);
		this.lbBTEX6T.TabIndex = 73;
		this.lbBTEX6T.Text = "异丙苯:";
		this.lbBTEX4T.AutoSize = true;
		this.lbBTEX4T.Font = new System.Drawing.Font("SimSun", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		this.lbBTEX4T.Location = new System.Drawing.Point(6, 77);
		this.lbBTEX4T.Name = "lbBTEX4T";
		this.lbBTEX4T.Size = new System.Drawing.Size(59, 12);
		this.lbBTEX4T.TabIndex = 72;
		this.lbBTEX4T.Text = "对二甲苯:";
		this.lbBTEX2T.AutoSize = true;
		this.lbBTEX2T.Font = new System.Drawing.Font("SimSun", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		this.lbBTEX2T.Location = new System.Drawing.Point(30, 36);
		this.lbBTEX2T.Name = "lbBTEX2T";
		this.lbBTEX2T.Size = new System.Drawing.Size(35, 12);
		this.lbBTEX2T.TabIndex = 68;
		this.lbBTEX2T.Text = "甲苯:";
		this.lbBTEX1T.AutoSize = true;
		this.lbBTEX1T.Font = new System.Drawing.Font("SimSun", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		this.lbBTEX1T.Location = new System.Drawing.Point(42, 18);
		this.lbBTEX1T.Name = "lbBTEX1T";
		this.lbBTEX1T.Size = new System.Drawing.Size(23, 12);
		this.lbBTEX1T.TabIndex = 67;
		this.lbBTEX1T.Text = "苯:";
		this.lbBTEX3T.AutoSize = true;
		this.lbBTEX3T.Font = new System.Drawing.Font("SimSun", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		this.lbBTEX3T.Location = new System.Drawing.Point(30, 56);
		this.lbBTEX3T.Name = "lbBTEX3T";
		this.lbBTEX3T.Size = new System.Drawing.Size(35, 12);
		this.lbBTEX3T.TabIndex = 66;
		this.lbBTEX3T.Text = "乙苯:";
		this.groupBox2.Controls.Add(this.label3);
		this.groupBox2.Controls.Add(this.label1);
		this.groupBox2.Controls.Add(this.label2);
		this.groupBox2.Controls.Add(this.labThcAver);
		this.groupBox2.Controls.Add(this.labCH4Avr);
		this.groupBox2.Controls.Add(this.labNMHCMin);
		this.groupBox2.Controls.Add(this.labThcMax);
		this.groupBox2.Controls.Add(this.labNMHCMax);
		this.groupBox2.Controls.Add(this.labCH4Max);
		this.groupBox2.Controls.Add(this.labNMHCAvr);
		this.groupBox2.Controls.Add(this.labThcMin);
		this.groupBox2.Controls.Add(this.labCH4Min);
		this.groupBox2.Location = new System.Drawing.Point(471, 20);
		this.groupBox2.Name = "groupBox2";
		this.groupBox2.Size = new System.Drawing.Size(264, 111);
		this.groupBox2.TabIndex = 25;
		this.groupBox2.TabStop = false;
		this.groupBox2.Text = "非甲烷总烃";
		this.groupBox3.Controls.Add(this.labCollectP);
		this.groupBox3.Controls.Add(this.label7);
		this.groupBox3.Controls.Add(this.labCollectSite);
		this.groupBox3.Controls.Add(this.label9);
		this.groupBox3.Controls.Add(this.labCollectTimes);
		this.groupBox3.Controls.Add(this.label10);
		this.groupBox3.Location = new System.Drawing.Point(471, 137);
		this.groupBox3.Name = "groupBox3";
		this.groupBox3.Size = new System.Drawing.Size(182, 178);
		this.groupBox3.TabIndex = 26;
		this.groupBox3.TabStop = false;
		this.groupBox3.Text = "采集信息";
		this.dataGridViewTextBoxColumn1.HeaderText = "THC";
		this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
		this.dataGridViewTextBoxColumn1.Width = 60;
		this.dataGridViewTextBoxColumn2.HeaderText = "CH4";
		this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
		this.dataGridViewTextBoxColumn2.Width = 60;
		this.dataGridViewTextBoxColumn3.HeaderText = "NMHC";
		this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
		this.dataGridViewTextBoxColumn3.Width = 60;
		this.cbPeakName.FormattingEnabled = true;
		this.cbPeakName.Location = new System.Drawing.Point(812, 22);
		this.cbPeakName.Name = "cbPeakName";
		this.cbPeakName.Size = new System.Drawing.Size(197, 20);
		this.cbPeakName.TabIndex = 28;
		this.cbPeakName.SelectedIndexChanged += new System.EventHandler(cbPeakName_SelectedIndexChanged);
		this.label37.AutoSize = true;
		this.label37.Location = new System.Drawing.Point(753, 22);
		this.label37.Name = "label37";
		this.label37.Size = new System.Drawing.Size(53, 12);
		this.label37.TabIndex = 27;
		this.label37.Text = "历史数据";
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(1052, 374);
		base.Controls.Add(this.cbPeakName);
		base.Controls.Add(this.label37);
		base.Controls.Add(this.groupBox3);
		base.Controls.Add(this.groupBox2);
		base.Controls.Add(this.groupBox1);
		base.Controls.Add(this.dataGridView2);
		base.Controls.Add(this.btnPrintf);
		base.Controls.Add(this.label6);
		base.Controls.Add(this.label5);
		base.Controls.Add(this.label4);
		base.Controls.Add(this.dataGridView1);
		base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
		base.Name = "FormCollectHistory";
		this.Text = "采样结果";
		((System.ComponentModel.ISupportInitialize)this.dataGridView1).EndInit();
		((System.ComponentModel.ISupportInitialize)this.dataGridView2).EndInit();
		this.groupBox1.ResumeLayout(false);
		this.groupBox1.PerformLayout();
		this.groupBox2.ResumeLayout(false);
		this.groupBox2.PerformLayout();
		this.groupBox3.ResumeLayout(false);
		this.groupBox3.PerformLayout();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
