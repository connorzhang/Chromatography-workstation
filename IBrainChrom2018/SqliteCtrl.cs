using System;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using IBrainChrom2018.Unit;

namespace IBrainChrom2018;

public class SqliteCtrl : UserControl
{
	public static SqliteCtrl selfCtrl;

	public bool flagChannelOver1 = false;

	public bool flagChannelOver2 = false;

	public bool flagChannelOver3 = false;

	public string channel1File = null;

	public string channel2File = null;

	public string channel3File = null;

	public string[] arrayData = new string[14];

	public int iCntChanl;

	private FormMainParam frmParam = FormMainParam.Create();

	private ChromDeviceListMgr cdlMgr = ChromDeviceListMgr.Create();

	private IContainer components = null;

	private SplitContainer splitContainer1;

	private Button btnExport;

	private Label label1;

	private Button btnSaveSite;

	private Label label2;

	private ComboBox cbSite;

	private GroupBox groupBox1;

	private CheckBox chChannel3;

	private CheckBox chChannel2;

	private CheckBox chChannel1;

	private Button btnChnSave;

	public SqliteCtrl()
	{
		selfCtrl = this;
		InitializeComponent();
		initForm();
		for (int i = 0; i < 14; i++)
		{
			arrayData[i] = "0.00000";
		}
	}

	private void btnExport_Click(object sender, EventArgs e)
	{
		ExportReport exportReport = new ExportReport();
		exportReport.StartPosition = FormStartPosition.CenterScreen;
		exportReport.Show();
	}

	public void initForm()
	{
		try
		{
			cbSite.Text = frmParam.strSampleSite.Substring(2);
			chChannel1.Checked = frmParam.bChannel;
			chChannel2.Checked = frmParam.bChanne2;
			chChannel3.Checked = frmParam.bChanne3;
			findAllTableName();
		}
		catch (Exception)
		{
		}
	}

	public void findAllTableName()
	{
		string connectionString = "Data Source='" + Application.StartupPath + "\\ngmpol.dll';Version=3;";
		string text = "";
		StringBuilder stringBuilder = new StringBuilder();
		cbSite.Items.Clear();
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
				cbSite.Items.Add(text.Substring(2));
			}
		}
	}

	public void updataGridView()
	{
		try
		{
		}
		catch (Exception)
		{
		}
	}

	public void disposePeaks(int selectedIndex, string fileName, string strID, string strSampleIndex, Chromatogram chromatogram)
	{
		int num = 0;
		byte b = 0;
		byte b2 = 0;
		float num2 = 0f;
		int num3 = 0;
		if (cdlMgr.CurrentChartParaOpera.mtdMgr.caliGnl == null)
		{
			cdlMgr.CurrentChartParaOpera.mtdMgr.caliGnl = new CaliGnl();
		}
		CaliGnl caliGnl = cdlMgr.CurrentChartParaOpera.mtdMgr.caliGnl;
		Peak[] rltPeaks = chromatogram.RltPeaks;
		float[] array = new float[1];
		ushort[] dst = new ushort[2];
		float[] array2 = new float[50];
		if (!frmParam.bChannel)
		{
			flagChannelOver1 = true;
		}
		if (!frmParam.bChanne2)
		{
			flagChannelOver2 = true;
		}
		if (!frmParam.bChanne3)
		{
			flagChannelOver3 = true;
		}
		switch (selectedIndex)
		{
		case 0:
		{
			flagChannelOver1 = true;
			channel1File = fileName;
			CaliGnl caliGnl4 = cdlMgr.ChartParaOperaList[0].mtdMgr.caliGnl;
			float num4 = 0f;
			float num5 = 0f;
			array = new float[1];
			Buffer.BlockCopy(array, 0, dst, 0, 4);
			for (b = 0; b < cdlMgr.ChartParaOperaList[0].mtdMgr.caliGnl.cmpds.Count(); b++)
			{
				num = 0;
				while (1 <= rltPeaks.Count() && num < rltPeaks.Count())
				{
					if (rltPeaks[num].pkRT >= caliGnl4.cmpds[b].cmpdInfo.retainTime - caliGnl4.cmpds[b].cmpdInfo.leftWindow && rltPeaks[num].pkRT <= caliGnl4.cmpds[b].cmpdInfo.retainTime + caliGnl4.cmpds[b].cmpdInfo.rightWindow && rltPeaks[num].height >= num2 && !(rltPeaks[num].name != caliGnl4.cmpds[b].cmpdInfo.name))
					{
						data2Array(rltPeaks[num].name, rltPeaks[num].amount);
					}
					num++;
				}
			}
			break;
		}
		case 1:
		{
			flagChannelOver2 = true;
			channel2File = fileName;
			CaliGnl caliGnl3 = new CaliGnl();
			caliGnl3 = cdlMgr.ChartParaOperaList[1].mtdMgr.caliGnl;
			for (b = 0; b < caliGnl3.cmpds.Count(); b++)
			{
				num = 0;
				while (1 <= rltPeaks.Count() && num < rltPeaks.Count())
				{
					if (rltPeaks[num].pkRT >= caliGnl3.cmpds[b].cmpdInfo.retainTime - caliGnl3.cmpds[b].cmpdInfo.leftWindow && rltPeaks[num].pkRT <= caliGnl3.cmpds[b].cmpdInfo.retainTime + caliGnl3.cmpds[b].cmpdInfo.rightWindow && rltPeaks[num].height >= num2 && !(rltPeaks[num].name != caliGnl3.cmpds[b].cmpdInfo.name))
					{
						data2Array(rltPeaks[num].name, rltPeaks[num].amount);
					}
					num++;
				}
			}
			break;
		}
		case 2:
		{
			flagChannelOver3 = true;
			channel3File = fileName;
			CaliGnl caliGnl2 = new CaliGnl();
			caliGnl2 = cdlMgr.ChartParaOperaList[2].mtdMgr.caliGnl;
			if (caliGnl2 == null)
			{
				break;
			}
			for (b = 0; b < caliGnl2.cmpds.Count(); b++)
			{
				num = 0;
				while (1 <= rltPeaks.Count() && num < rltPeaks.Count())
				{
					if (rltPeaks[num].pkRT >= caliGnl2.cmpds[b].cmpdInfo.retainTime - caliGnl2.cmpds[b].cmpdInfo.leftWindow && rltPeaks[num].pkRT <= caliGnl2.cmpds[b].cmpdInfo.retainTime + caliGnl2.cmpds[b].cmpdInfo.rightWindow && rltPeaks[num].height >= num2 && !(rltPeaks[num].name != caliGnl2.cmpds[b].cmpdInfo.name))
					{
						data2Array(rltPeaks[num].name, rltPeaks[num].amount);
					}
					num++;
				}
			}
			break;
		}
		}
		if (!flagChannelOver1 || !flagChannelOver2 || !flagChannelOver3)
		{
			return;
		}
		flagChannelOver1 = false;
		flagChannelOver2 = false;
		flagChannelOver3 = false;
		iCntChanl = 0;
		if (frmParam.bChannel)
		{
			iCntChanl++;
		}
		if (frmParam.bChanne2)
		{
			iCntChanl++;
		}
		if (frmParam.bChanne3)
		{
			iCntChanl++;
		}
		if (frmParam.bTwoDector)
		{
			if (channel1File == null)
			{
				channel1File = DateTime.Now.ToString("yyyy-MM-dd hh-mm-ss");
			}
			if (iCntChanl == 3)
			{
				string text = channel1File.Substring(0, channel1File.LastIndexOf("."));
				text += "合并.sda";
				spectraCombined(channel1File, channel2File, channel3File, text);
				calorificValue(text);
			}
			else if (iCntChanl == 2)
			{
				if (!frmParam.bChannel)
				{
					string text2 = channel2File.Substring(0, channel2File.LastIndexOf("."));
					text2 += "合并.sda";
					spectraCombined(channel2File, channel3File, text2);
					calorificValue(text2);
				}
				else if (!frmParam.bChanne2)
				{
					string text3 = channel1File.Substring(0, channel1File.LastIndexOf("."));
					text3 += "合并.sda";
					spectraCombined(channel1File, channel3File, text3);
					calorificValue(text3);
				}
				else if (!frmParam.bChanne3)
				{
					string text4 = channel1File.Substring(0, channel1File.LastIndexOf("."));
					text4 += "合并.sda";
					spectraCombined(channel1File, channel2File, text4);
					calorificValue(text4);
				}
			}
			try
			{
				Class49.InsertIntoMine(1, "YB" + cbSite.Text, arrayData);
			}
			catch (Exception)
			{
			}
		}
		else
		{
			try
			{
				Class49.InsertIntoMine(1, "YB" + cbSite.Text, arrayData);
			}
			catch (Exception)
			{
			}
		}
		for (int i = 0; i < 14; i++)
		{
			arrayData[i] = "0.00000";
		}
	}

	public void calorificValue(string fileName)
	{
		int num = 0;
		ushort[] array = new ushort[2];
		if (!File.Exists(fileName))
		{
			return;
		}
		float[] array2 = new float[50];
		if (ChromForm.form == null)
		{
			ChromForm.form = new ChromForm();
		}
		ChromForm.form.OpenChrom(fileName, sampling: true, useCurrent: true);
		ChromForm.form.chromDataGrid.mstSetChromForm.bUseSet_Click(null, null);
		ChromForm.form.chromDataGrid.saveFile();
		Peak[] rltPeaks = ChromForm.form.CurChrom.RltPeaks;
		if (cdlMgr.CurrentChartParaOpera.mtdMgr.caliGnl == null)
		{
			cdlMgr.CurrentChartParaOpera.mtdMgr.caliGnl = new CaliGnl();
		}
		CaliGnl caliGnl = cdlMgr.CurrentChartParaOpera.mtdMgr.caliGnl;
		CaliGnl caliGnl2 = new CaliGnl();
		if (rltPeaks.Length == 0)
		{
			return;
		}
		caliGnl2 = cdlMgr.ChartParaOperaList[0].mtdMgr.caliGnl;
		int num2 = 0;
		while (1 <= caliGnl2.cmpds.Length && num2 < caliGnl2.cmpds.Length)
		{
			num = 0;
			while (1 <= rltPeaks.Count() && num < rltPeaks.Count())
			{
				if (rltPeaks[num].pkRT >= caliGnl2.cmpds[num2].cmpdInfo.retainTime - caliGnl2.cmpds[num2].cmpdInfo.leftWindow && rltPeaks[num].pkRT <= caliGnl2.cmpds[num2].cmpdInfo.retainTime + caliGnl2.cmpds[num2].cmpdInfo.rightWindow && !(caliGnl2.cmpds[num2].cmpdInfo.name != rltPeaks[num].name))
				{
					if (caliGnl2.cmpds[num2].eFunc.curveFit == CurveFit.Free)
					{
						data2Array(rltPeaks[num].name, rltPeaks[num].amountPer * 100f);
						break;
					}
					if (num2 < 50)
					{
						array2[num2] = rltPeaks[num].amount;
					}
					data2Array(rltPeaks[num].name, rltPeaks[num].amount);
					break;
				}
				num++;
			}
			num2++;
		}
	}

	public void data2Array(string name, float amount)
	{
		switch (name)
		{
		case "六氟化硫":
			arrayData[0] = amount.ToString("0.00000");
			break;
		case "氮气":
			arrayData[1] = amount.ToString("0.00000");
			break;
		case "甲烷":
			arrayData[2] = amount.ToString("0.00000");
			break;
		case "乙炔":
			arrayData[3] = amount.ToString("0.00000");
			break;
		case "氧气":
			arrayData[4] = amount.ToString("0.00000");
			break;
		case "乙烷":
			arrayData[5] = amount.ToString("0.00000");
			break;
		case "一氧化碳":
			arrayData[6] = amount.ToString("0.00000");
			break;
		case "乙烯":
			arrayData[7] = amount.ToString("0.00000");
			break;
		case "二氧化碳":
			arrayData[8] = amount.ToString("0.00000");
			break;
		case "硫化氢":
			arrayData[9] = amount.ToString("0.00000");
			break;
		case "丙烷":
			arrayData[10] = amount.ToString("0.00000");
			break;
		case "异丁烷":
			arrayData[11] = amount.ToString("0.00000");
			break;
		case "正丁烷":
			arrayData[12] = amount.ToString("0.00000");
			break;
		case "二氧化硫":
			arrayData[13] = amount.ToString("0.00000");
			break;
		}
	}

	private void btnSaveSite_Click(object sender, EventArgs e)
	{
		string text = "YB" + cbSite.Text;
		string text2 = " ";
		text2 = text2 + " CREATE TABLE [" + text + "] ( ";
		text2 += "[时间] DATETIME,  ";
		text2 += "[地点] CHAR,  ";
		text2 += "[六氟化硫] FLOAT ,  ";
		text2 += "[氮气] FLOAT ,  ";
		text2 += "[甲烷] FLOAT ,  ";
		text2 += "[乙炔] FLOAT ,  ";
		text2 += "[氧气] FLOAT ,  ";
		text2 += "[乙烷] FLOAT ,  ";
		text2 += "[一氧化碳] FLOAT ,  ";
		text2 += "[乙烯] FLOAT,  ";
		text2 += "[二氧化碳] FLOAT,  ";
		text2 += "[硫化氢] FLOAT,  ";
		text2 += "[丙烷] FLOAT,  ";
		text2 += "[异丁烷] FLOAT,  ";
		text2 += "[正丁烷] FLOAT,  ";
		text2 += "[二氧化硫] FLOAT ); ";
		string connectionString = "Data Source='" + Application.StartupPath + "\\ngmpol.dll';Version=3;";
		StringBuilder stringBuilder = new StringBuilder();
		string text3 = "";
		using (SQLiteConnection sQLiteConnection = new SQLiteConnection(connectionString))
		{
			sQLiteConnection.Open();
			string text4 = "select name from sqlite_master where type='table' order by name;";
			string commandText = "SELECT COUNT(*) FROM sqlite_master where type='table' and name='" + text + "';";
			SQLiteCommand sQLiteCommand = new SQLiteCommand(commandText, sQLiteConnection);
			if (Convert.ToInt32(sQLiteCommand.ExecuteScalar()) == 0)
			{
				string text5 = Application.StartupPath + "\\ngmpol.dll";
				SQLiteConnection sQLiteConnection2 = new SQLiteConnection("Data Source=" + text5);
				sQLiteConnection2.Open();
				SQLiteCommand sQLiteCommand2 = new SQLiteCommand(text2, sQLiteConnection2);
				sQLiteCommand2.ExecuteNonQuery();
				sQLiteConnection2.Close();
				frmParam.strSampleSite = text;
				frmParam.SaveParam();
				string text6 = "select * from " + frmParam.strSampleSite;
			}
			else
			{
				frmParam.strSampleSite = text;
				frmParam.SaveParam();
			}
		}
		try
		{
			DateTime dateTime = default(DateTime);
			DateTime dateTime2 = default(DateTime);
			dateTime = DateTime.Now;
			dateTime2 = DateTime.Now;
			DataTable dataTableMINE = Class49.GetDataTableMINE(0, text, dateTime, dateTime2);
			if (dataTableMINE == null)
			{
				MessageBox.Show("名称不合法！");
				Class49.DeleteDataTable(text);
			}
		}
		catch (Exception)
		{
		}
		findAllTableName();
	}

	private void btnChnSave_Click(object sender, EventArgs e)
	{
		frmParam.bChannel = chChannel1.Checked;
		frmParam.bChanne2 = chChannel2.Checked;
		frmParam.bChanne3 = chChannel3.Checked;
		frmParam.SaveParam();
	}

	private void SqliteCtrl_Load(object sender, EventArgs e)
	{
	}

	private void button1_Click_1(object sender, EventArgs e)
	{
		Class49.InsertIntoMine(1, "YB" + cbSite.Text, arrayData);
	}

	private void spectraCombined(string file1, string file2, string file3)
	{
		Chromatogram chromatogram = Chromatogram.LoadFromFile2(file1, DetectorStyle.General);
		Chromatogram chromatogram2 = Chromatogram.LoadFromFile2(file2, DetectorStyle.General);
		if (chromatogram != null && chromatogram2 != null)
		{
			int dotsNum = chromatogram.signal.DotsNum;
			chromatogram.signal.DotsNum = chromatogram.signal.DotsNum + chromatogram2.signal.DotsNum;
			Array.Resize(ref chromatogram.signal.oriDots, chromatogram.signal.DotsNum);
			for (int i = dotsNum; i < chromatogram.signal.oriDots.Length; i++)
			{
				chromatogram.signal.oriDots[i].X = chromatogram2.signal.oriDots[i - dotsNum].X + chromatogram.signal.oriDots[dotsNum - 1].X;
				chromatogram.signal.oriDots[i].Y = chromatogram2.signal.oriDots[i - dotsNum].Y;
			}
			Array.Resize(ref chromatogram.signal.dots, chromatogram.signal.DotsNum);
			chromatogram.signal.Smooth(16);
			chromatogram.SaveToFile(file3);
		}
		else
		{
			MessageBox.Show("请先选择谱图文件!");
		}
	}

	private void spectraCombined(string file1, string file2, string file4, string file3)
	{
		Chromatogram chromatogram = Chromatogram.LoadFromFile2(file1, DetectorStyle.General);
		Chromatogram chromatogram2 = Chromatogram.LoadFromFile2(file2, DetectorStyle.General);
		Chromatogram chromatogram3 = Chromatogram.LoadFromFile2(file4, DetectorStyle.General);
		if (chromatogram != null && chromatogram2 != null && chromatogram3 != null)
		{
			int dotsNum = chromatogram.signal.DotsNum;
			chromatogram.signal.DotsNum = chromatogram.signal.DotsNum + chromatogram2.signal.DotsNum;
			Array.Resize(ref chromatogram.signal.oriDots, chromatogram.signal.DotsNum);
			for (int i = dotsNum; i < chromatogram.signal.oriDots.Length; i++)
			{
				chromatogram.signal.oriDots[i].X = chromatogram2.signal.oriDots[i - dotsNum].X + chromatogram.signal.oriDots[dotsNum - 1].X;
				chromatogram.signal.oriDots[i].Y = chromatogram2.signal.oriDots[i - dotsNum].Y;
			}
			Array.Resize(ref chromatogram.signal.dots, chromatogram.signal.DotsNum);
			dotsNum = chromatogram.signal.DotsNum;
			chromatogram.signal.DotsNum = chromatogram.signal.DotsNum + chromatogram3.signal.DotsNum;
			Array.Resize(ref chromatogram.signal.oriDots, chromatogram.signal.DotsNum);
			for (int j = dotsNum; j < chromatogram.signal.oriDots.Length; j++)
			{
				chromatogram.signal.oriDots[j].X = chromatogram3.signal.oriDots[j - dotsNum].X + chromatogram.signal.oriDots[dotsNum - 1].X;
				chromatogram.signal.oriDots[j].Y = chromatogram3.signal.oriDots[j - dotsNum].Y;
			}
			Array.Resize(ref chromatogram.signal.dots, chromatogram.signal.DotsNum);
			chromatogram.signal.Smooth(16);
			chromatogram.SaveToFile(file3);
		}
		else
		{
			MessageBox.Show("请先选择谱图文件!");
		}
	}

	private void button1_Click(object sender, EventArgs e)
	{
		Class49.InsertIntoMine(1, "YB" + cbSite.Text, arrayData);
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
		this.splitContainer1 = new System.Windows.Forms.SplitContainer();
		this.groupBox1 = new System.Windows.Forms.GroupBox();
		this.btnChnSave = new System.Windows.Forms.Button();
		this.chChannel3 = new System.Windows.Forms.CheckBox();
		this.chChannel2 = new System.Windows.Forms.CheckBox();
		this.chChannel1 = new System.Windows.Forms.CheckBox();
		this.cbSite = new System.Windows.Forms.ComboBox();
		this.label2 = new System.Windows.Forms.Label();
		this.label1 = new System.Windows.Forms.Label();
		this.btnSaveSite = new System.Windows.Forms.Button();
		this.btnExport = new System.Windows.Forms.Button();
		((System.ComponentModel.ISupportInitialize)this.splitContainer1).BeginInit();
		this.splitContainer1.Panel1.SuspendLayout();
		this.splitContainer1.SuspendLayout();
		this.groupBox1.SuspendLayout();
		base.SuspendLayout();
		this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
		this.splitContainer1.Location = new System.Drawing.Point(0, 0);
		this.splitContainer1.Name = "splitContainer1";
		this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
		this.splitContainer1.Panel1.Controls.Add(this.groupBox1);
		this.splitContainer1.Panel1.Controls.Add(this.cbSite);
		this.splitContainer1.Panel1.Controls.Add(this.label2);
		this.splitContainer1.Panel1.Controls.Add(this.label1);
		this.splitContainer1.Panel1.Controls.Add(this.btnSaveSite);
		this.splitContainer1.Panel1.Controls.Add(this.btnExport);
		this.splitContainer1.Size = new System.Drawing.Size(382, 373);
		this.splitContainer1.SplitterDistance = 195;
		this.splitContainer1.TabIndex = 63;
		this.groupBox1.Controls.Add(this.btnChnSave);
		this.groupBox1.Controls.Add(this.chChannel3);
		this.groupBox1.Controls.Add(this.chChannel2);
		this.groupBox1.Controls.Add(this.chChannel1);
		this.groupBox1.Location = new System.Drawing.Point(18, 126);
		this.groupBox1.Name = "groupBox1";
		this.groupBox1.Size = new System.Drawing.Size(340, 53);
		this.groupBox1.TabIndex = 6;
		this.groupBox1.TabStop = false;
		this.groupBox1.Text = "通道使用";
		this.btnChnSave.Location = new System.Drawing.Point(234, 16);
		this.btnChnSave.Name = "btnChnSave";
		this.btnChnSave.Size = new System.Drawing.Size(75, 23);
		this.btnChnSave.TabIndex = 3;
		this.btnChnSave.Text = "保存并应用";
		this.btnChnSave.UseVisualStyleBackColor = true;
		this.btnChnSave.Click += new System.EventHandler(btnChnSave_Click);
		this.chChannel3.AutoSize = true;
		this.chChannel3.Location = new System.Drawing.Point(174, 20);
		this.chChannel3.Name = "chChannel3";
		this.chChannel3.Size = new System.Drawing.Size(54, 16);
		this.chChannel3.TabIndex = 2;
		this.chChannel3.Text = "通道3";
		this.chChannel3.UseVisualStyleBackColor = true;
		this.chChannel2.AutoSize = true;
		this.chChannel2.Location = new System.Drawing.Point(90, 20);
		this.chChannel2.Name = "chChannel2";
		this.chChannel2.Size = new System.Drawing.Size(54, 16);
		this.chChannel2.TabIndex = 1;
		this.chChannel2.Text = "通道2";
		this.chChannel2.UseVisualStyleBackColor = true;
		this.chChannel1.AutoSize = true;
		this.chChannel1.Location = new System.Drawing.Point(6, 20);
		this.chChannel1.Name = "chChannel1";
		this.chChannel1.Size = new System.Drawing.Size(54, 16);
		this.chChannel1.TabIndex = 0;
		this.chChannel1.Text = "通道1";
		this.chChannel1.UseVisualStyleBackColor = true;
		this.cbSite.FormattingEnabled = true;
		this.cbSite.Location = new System.Drawing.Point(87, 74);
		this.cbSite.Name = "cbSite";
		this.cbSite.Size = new System.Drawing.Size(121, 20);
		this.cbSite.TabIndex = 5;
		this.label2.AutoSize = true;
		this.label2.Location = new System.Drawing.Point(16, 111);
		this.label2.Name = "label2";
		this.label2.Size = new System.Drawing.Size(365, 12);
		this.label2.TabIndex = 4;
		this.label2.Text = "注：采样地点名字只可以包含汉字、字母、数字、不能用特殊字符！";
		this.label1.AutoSize = true;
		this.label1.Location = new System.Drawing.Point(16, 77);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(65, 12);
		this.label1.TabIndex = 3;
		this.label1.Text = "采样地点：";
		this.btnSaveSite.Location = new System.Drawing.Point(221, 72);
		this.btnSaveSite.Name = "btnSaveSite";
		this.btnSaveSite.Size = new System.Drawing.Size(136, 23);
		this.btnSaveSite.TabIndex = 2;
		this.btnSaveSite.Text = "保存采样地点";
		this.btnSaveSite.UseVisualStyleBackColor = true;
		this.btnSaveSite.Click += new System.EventHandler(btnSaveSite_Click);
		this.btnExport.Location = new System.Drawing.Point(18, 12);
		this.btnExport.Name = "btnExport";
		this.btnExport.Size = new System.Drawing.Size(340, 43);
		this.btnExport.TabIndex = 0;
		this.btnExport.Text = "数据导出";
		this.btnExport.UseVisualStyleBackColor = true;
		this.btnExport.Click += new System.EventHandler(btnExport_Click);
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.Controls.Add(this.splitContainer1);
		base.Name = "SqliteCtrl";
		base.Size = new System.Drawing.Size(382, 373);
		base.Load += new System.EventHandler(SqliteCtrl_Load);
		this.splitContainer1.Panel1.ResumeLayout(false);
		this.splitContainer1.Panel1.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.splitContainer1).EndInit();
		this.splitContainer1.ResumeLayout(false);
		this.groupBox1.ResumeLayout(false);
		this.groupBox1.PerformLayout();
		base.ResumeLayout(false);
	}
}
