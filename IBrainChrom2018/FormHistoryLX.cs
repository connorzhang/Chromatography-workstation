using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using HZH_Controls;
using HZH_Controls.Controls;
using IBrainChrom2018.Unit;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;

namespace IBrainChrom2018;

public class FormHistoryLX : Form
{
	public static FormHistoryLX selfCtrl;

	private ChromDeviceListMgr cdlMgr = ChromDeviceListMgr.Create();

	private DataTable dataSource1 = new DataTable();

	private FormMainParam frmParam = FormMainParam.Create();

	private List<string> strFileName1 = new List<string>();

	private List<string> strSerialName = new List<string>();

	private List<Color> strSerialColor = new List<Color>();

	private List<string> strSerialTime = new List<string>();

	private List<string> strSerialTime2 = new List<string>();

	public Series[] serieLines = new Series[11];

	private bool loading = true;

	private ChartParaOpera cpoaChannel = new ChartParaOpera();

	private IContainer components = null;

	private SplitContainer splitContainer1;

	private SplitContainer splitContainer2;

	private DataGridView dataGridView1;

	private Chart chartPlots;

	private GroupBox groupBox1;

	private CheckBox ch10;

	private Button btnSaveLoadData;

	private CheckBox ch9;

	private CheckBox ch8;

	private CheckBox ch7;

	private CheckBox ch6;

	private CheckBox ch5;

	private CheckBox ch4;

	private CheckBox ch3;

	private CheckBox ch2;

	private CheckBox ch1;

	private Label label6;

	private Label label7;

	private DateTimePicker dateTimePicker2;

	private DateTimePicker dateTimePicker1;

	private ContextMenuStrip contextMenuStrip1;

	private Label label2;

	private Label label1;

	private Button btnExplorer;

	private CheckBox ch11;

	private Button btnOpenChrom;

	private Button btnOpenChrom3;

	private Button btnOpenChrom2;

	private DataGridView dataGridView3;

	private DataGridView dataGridView2;

	private SplitContainer splitContainer3;

	private Button btnExplorer2;

	private UCCombox ucCBSites;

	private UCTextBoxEx tbUpperLimit;

	private UCTextBoxEx tbLowerLimit;

	public FormHistoryLX()
	{
		selfCtrl = this;
		InitializeComponent();
		initForm();
	}

	public void initForm()
	{
		splitContainer3.Panel2Collapsed = true;
		bool flag = true;
		btnExplorer.Text = "数据导出";
		btnExplorer2.Visible = false;
		dataGridView1.ReadOnly = true;
		dataGridView2.ReadOnly = true;
		dataGridView3.ReadOnly = true;
		strSerialName.Clear();
		strSerialName.Add("1");
		strSerialName.Add("2");
		strSerialName.Add("3");
		strSerialName.Add("4");
		strSerialName.Add("5");
		strSerialName.Add("6");
		strSerialName.Add("7");
		strSerialName.Add("8");
		strSerialName.Add("9");
		strSerialName.Add("10");
		strSerialName.Add("11");
		strSerialColor.Clear();
		strSerialColor.Add(Color.Blue);
		strSerialColor.Add(Color.Black);
		strSerialColor.Add(Color.Red);
		strSerialColor.Add(Color.Green);
		strSerialColor.Add(Color.Aqua);
		strSerialColor.Add(Color.Brown);
		strSerialColor.Add(Color.Coral);
		strSerialColor.Add(Color.Orange);
		strSerialColor.Add(Color.Crimson);
		strSerialColor.Add(Color.BurlyWood);
		strSerialColor.Add(Color.DarkKhaki);
		ch1.Checked = frmParam.bSum[0];
		ch2.Checked = frmParam.bSum[1];
		ch3.Checked = frmParam.bSum[2];
		ch4.Checked = frmParam.bSum[3];
		ch5.Checked = frmParam.bSum[4];
		ch6.Checked = frmParam.bSum[5];
		ch7.Checked = frmParam.bSum[6];
		ch8.Checked = frmParam.bSum[7];
		ch9.Checked = frmParam.bSum[8];
		ch10.Checked = frmParam.bSum[9];
		ch11.Checked = frmParam.bSum[10];
		tbUpperLimit.InputText = frmParam.UpperLimit.ToString();
		tbLowerLimit.InputText = frmParam.LowerLimit.ToString();
		InitChart();
		List<KeyValuePair<string, string>> list = new List<KeyValuePair<string, string>>();
		list.Add(new KeyValuePair<string, string>(0.ToString(), "A流路1"));
		list.Add(new KeyValuePair<string, string>(1.ToString(), "A流路2"));
		list.Add(new KeyValuePair<string, string>(2.ToString(), "A流路3"));
		list.Add(new KeyValuePair<string, string>(3.ToString(), "A流路4"));
		list.Add(new KeyValuePair<string, string>(4.ToString(), "A流路5"));
		list.Add(new KeyValuePair<string, string>(5.ToString(), "A流路6"));
		list.Add(new KeyValuePair<string, string>(6.ToString(), "A流路7"));
		list.Add(new KeyValuePair<string, string>(7.ToString(), "A流路8"));
		list.Add(new KeyValuePair<string, string>(8.ToString(), "B流路1"));
		list.Add(new KeyValuePair<string, string>(9.ToString(), "B流路2"));
		list.Add(new KeyValuePair<string, string>(10.ToString(), "B流路3"));
		list.Add(new KeyValuePair<string, string>(11.ToString(), "B流路4"));
		ucCBSites.Source = list;
		tbUpperLimit.KeyBoardType = KeyBoardType.UCKeyBorderNum;
		tbUpperLimit.IsShowKeyboard = true;
		tbUpperLimit.txtInput.TextChanged += tbUpperLimit_TextChanged;
		tbLowerLimit.KeyBoardType = KeyBoardType.UCKeyBorderNum;
		tbLowerLimit.IsShowKeyboard = true;
		tbLowerLimit.txtInput.TextChanged += tbLowerLimit_TextChanged;
		loading = false;
	}

	public void loadData()
	{
		bool flag = true;
		splitContainer3.Visible = false;
		dataGridView1.Dock = DockStyle.Fill;
		DataTable dataTableMINE = Class49.GetDataTableMINE(0, ucCBSites.TextValue, dateTimePicker1.Value, dateTimePicker2.Value);
		if (dataTableMINE == null)
		{
			return;
		}
		dataGridView1.DataSource = dataTableMINE;
		dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
		for (int i = 0; i < dataGridView1.Rows.Count; i++)
		{
			if (dataGridView1.Columns["FileName"] != null && dataGridView1.Rows[i].Cells["FileName"].Value != null)
			{
				strFileName1.Add(dataGridView1.Rows[i].Cells["FileName"].Value.ToString());
			}
		}
		try
		{
			if (ucCBSites.SelectedIndex < 4)
			{
				cpoaChannel = cdlMgr.ChartParaOperaList[0];
			}
			else
			{
				cpoaChannel = cdlMgr.ChartParaOperaList[1];
			}
			relodColumn();
		}
		catch
		{
		}
		loadSeriesDate();
	}

	public void relodColumn()
	{
		if (dataGridView1.Columns.Contains("Code"))
		{
			dataGridView1.Columns.Remove("Code");
		}
		if (ucCBSites.SelectedIndex < 4)
		{
			if (cdlMgr.ChartParaOperaList == null || cpoaChannel.mtdMgr == null)
			{
				return;
			}
			for (int i = 0; i < cpoaChannel.mtdMgr.caliGnl.cmpds.Count(); i++)
			{
				dataGridView1.Columns[i + 3].HeaderText = cpoaChannel.mtdMgr.caliGnl.cmpds[i].cmpdInfo.name;
			}
			for (int j = cpoaChannel.mtdMgr.caliGnl.cmpds.Count(); j < 20; j++)
			{
				if (dataGridView1.Columns.Contains((j + 1).ToString()))
				{
					dataGridView1.Columns.Remove((j + 1).ToString());
				}
			}
		}
		else
		{
			if (cdlMgr.ChartParaOperaList == null || cdlMgr.ChartParaOperaList.Count <= 1 || cdlMgr.ChartParaOperaList[1].mtdMgr == null)
			{
				return;
			}
			for (int k = 0; k < cdlMgr.ChartParaOperaList[1].mtdMgr.caliGnl.cmpds.Count(); k++)
			{
				dataGridView1.Columns[k + 3].HeaderText = cdlMgr.ChartParaOperaList[1].mtdMgr.caliGnl.cmpds[k].cmpdInfo.name;
			}
			for (int l = cdlMgr.ChartParaOperaList[0].mtdMgr.caliGnl.cmpds.Count(); l < 20; l++)
			{
				if (dataGridView1.Columns.Contains((l + 1).ToString()))
				{
					dataGridView1.Columns.Remove((l + 1).ToString());
				}
			}
		}
	}

	public void relodColumn2()
	{
		try
		{
			if (!frmParam.bSum[0])
			{
				dataGridView2.Columns.Remove("总烃");
			}
		}
		catch
		{
		}
		try
		{
			if (!frmParam.bSum[1])
			{
				dataGridView2.Columns.Remove("甲烷");
			}
		}
		catch
		{
		}
		try
		{
			if (!frmParam.bSum[2])
			{
				dataGridView2.Columns.Remove("非甲烷总烃");
			}
		}
		catch
		{
		}
		try
		{
			if (!frmParam.bSum[3])
			{
				dataGridView3.Columns.Remove("苯");
			}
		}
		catch
		{
		}
		try
		{
			if (!frmParam.bSum[4])
			{
				dataGridView3.Columns.Remove("甲苯");
			}
		}
		catch
		{
		}
		try
		{
			if (!frmParam.bSum[5])
			{
				dataGridView3.Columns.Remove("间对二甲苯");
			}
		}
		catch
		{
		}
		try
		{
			if (!frmParam.bSum[6])
			{
				dataGridView3.Columns.Remove("邻二甲苯");
			}
		}
		catch
		{
		}
		try
		{
			if (!frmParam.bSum[7])
			{
				dataGridView3.Columns.Remove("乙苯");
			}
		}
		catch
		{
		}
		try
		{
			if (!frmParam.bSum[8])
			{
				dataGridView3.Columns.Remove("异丙苯");
			}
		}
		catch
		{
		}
		try
		{
			if (!frmParam.bSum[9])
			{
				dataGridView3.Columns.Remove("苯乙烯");
			}
		}
		catch
		{
		}
		try
		{
			if (!frmParam.bSum[10])
			{
				dataGridView3.Columns.Remove("苯系物");
			}
		}
		catch
		{
		}
	}

	private void InitChart()
	{
		chartPlots.ContextMenuStrip = contextMenuStrip1;
		chartPlots.Series.Clear();
		chartPlots.ChartAreas[0].AxisX.MajorGrid.Interval = 0.1;
		chartPlots.ChartAreas[0].AxisY.MajorGrid.Interval = 0.1;
		chartPlots.ChartAreas[0].AxisX.MajorGrid.Enabled = false;
		chartPlots.ChartAreas[0].AxisY.MajorGrid.Enabled = false;
		chartPlots.ChartAreas[0].AxisX.IsMarginVisible = true;
		chartPlots.ChartAreas[0].AxisX.Title = "时间";
		chartPlots.ChartAreas[0].AxisX.TitleForeColor = Color.Crimson;
		chartPlots.ChartAreas["ChartArea1"].AxisX.LabelStyle.Format = "yyyy/MM/dd/ HH:mm:ss";
		chartPlots.ChartAreas[0].AxisY.Title = "含量";
		chartPlots.ChartAreas[0].AxisY.TitleForeColor = Color.Crimson;
		chartPlots.ChartAreas[0].AxisY.Maximum = float.Parse(tbUpperLimit.InputText);
		chartPlots.ChartAreas[0].AxisY.Minimum = float.Parse(tbLowerLimit.InputText);
		chartPlots.ChartAreas[0].AxisY.LabelStyle.Format = "0.000";
		chartPlots.ChartAreas[0].CursorX.IsUserEnabled = true;
		chartPlots.ChartAreas[0].CursorX.IsUserSelectionEnabled = true;
		chartPlots.ChartAreas[0].AxisX.ScaleView.Zoomable = true;
		chartPlots.ChartAreas[0].AxisX.ScrollBar.IsPositionedInside = true;
		for (int i = 0; i < 11; i++)
		{
			if (serieLines[i] == null)
			{
				serieLines[i] = new Series();
			}
			serieLines[i].ChartType = SeriesChartType.Line;
			serieLines[i].IsVisibleInLegend = true;
			serieLines[i].IsValueShownAsLabel = false;
			serieLines[i].XValueType = ChartValueType.String;
			serieLines[i].YValueType = ChartValueType.Double;
			serieLines[i].Color = strSerialColor[i];
			serieLines[i].MarkerStyle = MarkerStyle.Circle;
			serieLines[i].MarkerSize = 2;
			serieLines[i].LegendText = strSerialName[i];
			chartPlots.Series.Add(serieLines[i]);
		}
	}

	public void loadSeriesDate()
	{
		for (int i = 0; i < 10; i++)
		{
			serieLines[i].Points.Clear();
		}
		for (int j = 0; j < dataGridView1.Rows.Count; j++)
		{
			if (dataGridView1.Rows[j].Cells["时间"].Value != null)
			{
				strSerialTime.Add(dataGridView1.Rows[j].Cells["时间"].Value.ToString());
			}
		}
		for (int k = 0; k < dataGridView1.Rows.Count; k++)
		{
			if (dataGridView1.Columns["1"] != null && dataGridView1.Rows[k].Cells["1"].Value != null)
			{
				serieLines[0].Points.AddXY(strSerialTime[k], dataGridView1.Rows[k].Cells["1"].Value.ToString());
			}
			if (dataGridView1.Columns["2"] != null && dataGridView1.Rows[k].Cells["2"].Value != null)
			{
				serieLines[1].Points.AddXY(strSerialTime[k], dataGridView1.Rows[k].Cells["2"].Value.ToString());
			}
			if (dataGridView1.Columns["3"] != null && dataGridView1.Rows[k].Cells["3"].Value != null)
			{
				serieLines[2].Points.AddXY(strSerialTime[k], dataGridView1.Rows[k].Cells["3"].Value.ToString());
			}
			if (dataGridView1.Columns["4"] != null && dataGridView1.Rows[k].Cells["4"].Value != null)
			{
				serieLines[3].Points.AddXY(strSerialTime[k], dataGridView1.Rows[k].Cells["4"].Value.ToString());
			}
			if (dataGridView1.Columns["5"] != null && dataGridView1.Rows[k].Cells["5"].Value != null)
			{
				serieLines[4].Points.AddXY(strSerialTime[k], dataGridView1.Rows[k].Cells["5"].Value.ToString());
			}
			if (dataGridView1.Columns["6"] != null && dataGridView1.Rows[k].Cells["6"].Value != null)
			{
				serieLines[5].Points.AddXY(strSerialTime[k], dataGridView1.Rows[k].Cells["6"].Value.ToString());
			}
			if (dataGridView1.Columns["7"] != null && dataGridView1.Rows[k].Cells["7"].Value != null)
			{
				serieLines[6].Points.AddXY(strSerialTime[k], dataGridView1.Rows[k].Cells["7"].Value.ToString());
			}
			if (dataGridView1.Columns["8"] != null && dataGridView1.Rows[k].Cells["8"].Value != null)
			{
				serieLines[7].Points.AddXY(strSerialTime[k], dataGridView1.Rows[k].Cells["8"].Value.ToString());
			}
			if (dataGridView1.Columns["9"] != null && dataGridView1.Rows[k].Cells["9"].Value != null)
			{
				serieLines[8].Points.AddXY(strSerialTime[k], dataGridView1.Rows[k].Cells["9"].Value.ToString());
			}
			if (dataGridView1.Columns["10"] != null && dataGridView1.Rows[k].Cells["10"].Value != null)
			{
				serieLines[9].Points.AddXY(strSerialTime[k], dataGridView1.Rows[k].Cells["10"].Value.ToString());
			}
			if (dataGridView1.Columns["11"] != null && dataGridView1.Rows[k].Cells["11"].Value != null)
			{
				serieLines[10].Points.AddXY(strSerialTime[k], dataGridView1.Rows[k].Cells["11"].Value.ToString());
			}
		}
		for (int l = 0; l < 11; l++)
		{
			serieLines[l].IsVisibleInLegend = frmParam.bSum[l];
			if (!frmParam.bSum[l])
			{
				serieLines[l].Points.Clear();
			}
		}
	}

	public void loadSeriesDate2()
	{
		for (int i = 0; i < 10; i++)
		{
			serieLines[i].Points.Clear();
		}
		for (int j = 0; j < dataGridView2.Rows.Count; j++)
		{
			if (dataGridView2.Rows[j].Cells["时间"].Value != null)
			{
				strSerialTime.Add(dataGridView2.Rows[j].Cells["时间"].Value.ToString());
			}
		}
		for (int k = 0; k < dataGridView3.Rows.Count; k++)
		{
			if (dataGridView3.Rows[k].Cells["时间"].Value != null)
			{
				strSerialTime2.Add(dataGridView3.Rows[k].Cells["时间"].Value.ToString());
			}
		}
		for (int l = 0; l < dataGridView2.Rows.Count; l++)
		{
			if (dataGridView2.Columns["总烃"] != null && dataGridView2.Rows[l].Cells["总烃"].Value != null)
			{
				serieLines[0].Points.AddXY(strSerialTime[l], dataGridView2.Rows[l].Cells["总烃"].Value.ToString());
			}
			if (dataGridView2.Columns["甲烷"] != null && dataGridView2.Rows[l].Cells["甲烷"].Value != null)
			{
				serieLines[1].Points.AddXY(strSerialTime[l], dataGridView2.Rows[l].Cells["甲烷"].Value.ToString());
			}
			if (dataGridView2.Columns["非甲烷总烃"] != null && dataGridView2.Rows[l].Cells["非甲烷总烃"].Value != null)
			{
				serieLines[2].Points.AddXY(strSerialTime[l], dataGridView2.Rows[l].Cells["非甲烷总烃"].Value.ToString());
			}
			if (dataGridView3.Columns["苯"] != null && dataGridView3.Rows[l].Cells["苯"].Value != null)
			{
				serieLines[3].Points.AddXY(strSerialTime2[l], dataGridView3.Rows[l].Cells["苯"].Value.ToString());
			}
			if (dataGridView3.Columns["甲苯"] != null && dataGridView3.Rows[l].Cells["甲苯"].Value != null)
			{
				serieLines[4].Points.AddXY(strSerialTime2[l], dataGridView3.Rows[l].Cells["甲苯"].Value.ToString());
			}
			if (dataGridView3.Columns["间对二甲苯"] != null && dataGridView3.Rows[l].Cells["间对二甲苯"].Value != null)
			{
				serieLines[5].Points.AddXY(strSerialTime[l], dataGridView3.Rows[l].Cells["间对二甲苯"].Value.ToString());
			}
			if (dataGridView3.Columns["邻二甲苯"] != null && dataGridView3.Rows[l].Cells["邻二甲苯"].Value != null)
			{
				serieLines[6].Points.AddXY(strSerialTime2[l], dataGridView3.Rows[l].Cells["邻二甲苯"].Value.ToString());
			}
			if (dataGridView3.Columns["乙苯"] != null && dataGridView3.Rows[l].Cells["乙苯"].Value != null)
			{
				serieLines[7].Points.AddXY(strSerialTime2[l], dataGridView3.Rows[l].Cells["乙苯"].Value.ToString());
			}
			if (dataGridView3.Columns["异丙苯"] != null && dataGridView3.Rows[l].Cells["异丙苯"].Value != null)
			{
				serieLines[8].Points.AddXY(strSerialTime2[l], dataGridView3.Rows[l].Cells["异丙苯"].Value.ToString());
			}
			if (dataGridView3.Columns["苯乙烯"] != null && dataGridView3.Rows[l].Cells["苯乙烯"].Value != null)
			{
				serieLines[9].Points.AddXY(strSerialTime2[l], dataGridView3.Rows[l].Cells["苯乙烯"].Value.ToString());
			}
			if (dataGridView3.Columns["苯系物"] != null && dataGridView3.Rows[l].Cells["苯系物"].Value != null)
			{
				serieLines[10].Points.AddXY(strSerialTime2[l], dataGridView3.Rows[l].Cells["苯系物"].Value.ToString());
			}
		}
		for (int m = 0; m < 11; m++)
		{
			serieLines[m].IsVisibleInLegend = frmParam.bSum[m];
			if (!frmParam.bSum[m])
			{
				serieLines[m].Points.Clear();
			}
		}
	}

	private void btnSaveLoadData_Click(object sender, EventArgs e)
	{
		frmParam.bSum[0] = ch1.Checked;
		frmParam.bSum[1] = ch2.Checked;
		frmParam.bSum[2] = ch3.Checked;
		frmParam.bSum[3] = ch4.Checked;
		frmParam.bSum[4] = ch5.Checked;
		frmParam.bSum[5] = ch6.Checked;
		frmParam.bSum[6] = ch7.Checked;
		frmParam.bSum[7] = ch8.Checked;
		frmParam.bSum[8] = ch9.Checked;
		frmParam.bSum[9] = ch10.Checked;
		frmParam.bSum[10] = ch11.Checked;
		loadData();
		frmParam.SaveParam();
	}

	private void tbUpperLimit_TextChanged(object sender, EventArgs e)
	{
		if (!loading)
		{
			float.TryParse(tbUpperLimit.InputText, out frmParam.UpperLimit);
			if ((double)frmParam.UpperLimit > chartPlots.ChartAreas[0].AxisY.Minimum)
			{
				chartPlots.ChartAreas[0].AxisY.Maximum = frmParam.UpperLimit;
			}
			frmParam.SaveParam();
		}
	}

	private void tbLowerLimit_TextChanged(object sender, EventArgs e)
	{
		if (!loading)
		{
			float.TryParse(tbLowerLimit.InputText, out frmParam.LowerLimit);
			if ((double)frmParam.LowerLimit < chartPlots.ChartAreas[0].AxisY.Maximum)
			{
				chartPlots.ChartAreas[0].AxisY.Minimum = frmParam.LowerLimit;
			}
			frmParam.SaveParam();
		}
	}

	private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
	{
		if (!loading)
		{
			frmParam.dataTimeStart = dateTimePicker1.Value;
			frmParam.SaveParam();
			loadData();
		}
	}

	private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
	{
		if (!loading)
		{
			frmParam.dataTimeEnd = dateTimePicker2.Value;
			frmParam.SaveParam();
			loadData();
		}
	}

	private void btnExplorer_Click(object sender, EventArgs e)
	{
		bool flag = true;
		SaveFileDialog saveFileDialog = new SaveFileDialog();
		saveFileDialog.FileName = Application.StartupPath + "\\历史数据" + DateTime.Now.ToString("yyyyMMddHHmm") + ".xls";
		saveFileDialog.Filter = " xls files(*.xls)|*.xls|All files(*.*)|*.*";
		saveFileDialog.FilterIndex = 2;
		saveFileDialog.RestoreDirectory = true;
		if (saveFileDialog.ShowDialog() == DialogResult.OK)
		{
			int num = 0;
			num = dataGridView1.Columns.Count - 2;
			if (num < 1)
			{
				num = 1;
			}
			FileStream fileStream = new FileStream(Application.StartupPath + "\\微量硫数据" + num + ".xls", FileMode.Open, FileAccess.Read);
			HSSFWorkbook hSSFWorkbook = new HSSFWorkbook(fileStream);
			ISheet sheetAt = hSSFWorkbook.GetSheetAt(0);
			sheetAt.ForceFormulaRecalculation = true;
			FileStream fileStream2 = new FileStream(saveFileDialog.FileName, FileMode.Create);
			hSSFWorkbook.Write(fileStream2);
			fileStream.Close();
			fileStream2.Close();
			DataGridView dataGridView = new DataGridView();
			if (dataGridView1.Rows.Count <= 0)
			{
				return;
			}
			for (int i = 0; i < dataGridView1.ColumnCount; i++)
			{
				dataGridView.Columns.Add(dataGridView1.Columns[i].Name, dataGridView1.Columns[i].HeaderText);
			}
			foreach (DataGridViewRow item in (IEnumerable)dataGridView1.Rows)
			{
				if (item.Cells[0].Value == null)
				{
					continue;
				}
				string[] array = new string[item.Cells.Count];
				for (int j = 0; j < item.Cells.Count; j++)
				{
					array[j] = item.Cells[j].Value.ToString();
				}
				try
				{
					dataGridView.Rows.Add();
					for (int k = 0; k < item.Cells.Count; k++)
					{
						dataGridView.Rows[dataGridView.Rows.Count - 1].Cells[k].Value = item.Cells[k].Value;
					}
				}
				catch (Exception)
				{
				}
			}
			dataToExcel(dataGridView, saveFileDialog.FileName);
		}
		if (File.Exists(saveFileDialog.FileName))
		{
			Process.Start(saveFileDialog.FileName);
		}
	}

	public bool dataToExcel(DataGridView ordgv, string Outpath)
	{
		bool result = false;
		IWorkbook workbook = null;
		FileStream fileStream = null;
		IRow row = null;
		IRow row2 = null;
		ISheet sheet = null;
		ICell cell = null;
		ICell cell2 = null;
		bool flag = false;
		double num = 0.0;
		FileStream fileStream2 = new FileStream(Outpath, FileMode.Open, FileAccess.ReadWrite);
		try
		{
			string[] array = new string[ordgv.Columns.Count];
			foreach (DataGridViewColumn column in ordgv.Columns)
			{
				if (column.Visible)
				{
					array[column.DisplayIndex] = column.HeaderText;
				}
			}
			DataGridView dataGridView = new DataGridView();
			dataGridView = ordgv;
			if (dataGridView != null && dataGridView.Rows.Count > 0)
			{
				workbook = new HSSFWorkbook(fileStream2);
				sheet = workbook.GetSheetAt(0);
				int count = dataGridView.Rows.Count;
				int num2 = dataGridView.Columns.Count - 1;
				int num3 = 0;
				int num4 = 1;
				row2 = sheet.GetRow(0);
				cell2 = row2.GetCell(0);
				row2 = sheet.GetRow(1);
				cell2 = row2.GetCell(0);
				cell2.SetCellValue("制表时间:" + DateTime.Now.ToString());
				row = sheet.GetRow(5);
				for (int i = 1; i < num2; i++)
				{
					cell = row.GetCell(i);
					if (cell == null)
					{
						cell = row.CreateCell(i);
					}
				}
				for (int j = 4; j < dataGridView1.Rows.Count + 3; j++)
				{
					if (j > 4)
					{
						IRow row3 = sheet.GetRow(5);
						MyInsertRow(sheet, j + 1, 1, row3);
					}
					row = sheet.GetRow(j);
					if (row == null)
					{
						row = sheet.CreateRow(j);
					}
					for (int k = 0; k <= num2; k++)
					{
						cell = row.GetCell(k);
						if (k == 0)
						{
							cell.SetCellValue((j - 3).ToString());
							continue;
						}
						string columnName;
						try
						{
							columnName = array.GetValue(k - 1).ToString();
						}
						catch
						{
							continue;
						}
						if (cell == null)
						{
							cell = row.CreateCell(k);
						}
						if (k == 1)
						{
							if (dataGridView1.Rows[count + 2 - j].Cells[columnName].Value != null)
							{
								cell.SetCellValue(dataGridView1.Rows[count + 2 - j].Cells[columnName].Value.ToString());
							}
						}
						else
						{
							cell.SetCellValue(dataGridView1.Rows[count + 2 - j].Cells[k - 1].Value.ToString());
						}
					}
					for (int l = 2; l <= num2; l++)
					{
						row = sheet.GetRow(3);
						cell = row.GetCell(l);
						cell.SetCellValue(array.GetValue(l - 1).ToString());
					}
				}
				using (fileStream = File.OpenWrite(Outpath))
				{
					workbook.Write(fileStream);
					result = true;
				}
				fileStream2.Close();
			}
			return result;
		}
		catch (Exception)
		{
			using (fileStream = File.OpenWrite(Outpath))
			{
				workbook.Write(fileStream);
				result = true;
			}
			fileStream.Close();
			return false;
		}
	}

	public bool dataToExcel2(DataGridView ordgv, string Outpath)
	{
		bool result = false;
		IWorkbook workbook = null;
		FileStream fileStream = null;
		IRow row = null;
		IRow row2 = null;
		ISheet sheet = null;
		ICell cell = null;
		ICell cell2 = null;
		bool flag = false;
		double result2 = 0.0;
		FileStream fileStream2 = new FileStream(Outpath, FileMode.Open, FileAccess.ReadWrite);
		try
		{
			string[] array = new string[ordgv.Columns.Count];
			foreach (DataGridViewColumn column in ordgv.Columns)
			{
				if (column.Visible)
				{
					array[column.DisplayIndex] = column.HeaderText;
				}
			}
			DataGridView dataGridView = new DataGridView();
			dataGridView = ordgv;
			if (dataGridView != null && dataGridView.Rows.Count > 0)
			{
				workbook = new HSSFWorkbook(fileStream2);
				sheet = workbook.GetSheetAt(0);
				int count = dataGridView.Rows.Count;
				int count2 = dataGridView.Columns.Count;
				int num = 0;
				int num2 = 1;
				row2 = sheet.GetRow(0);
				cell2 = row2.GetCell(0);
				row2 = sheet.GetRow(1);
				cell2 = row2.GetCell(0);
				cell2.SetCellValue("制表时间:" + DateTime.Now.ToString());
				row = sheet.GetRow(5);
				for (int i = 1; i < count2; i++)
				{
					cell = row.GetCell(i);
					if (cell == null)
					{
						cell = row.CreateCell(i);
					}
				}
				for (int j = 4; j < dataGridView2.Rows.Count + 3; j++)
				{
					if (j > 4)
					{
						IRow row3 = sheet.GetRow(5);
						MyInsertRow(sheet, j + 1, 1, row3);
					}
					row = sheet.GetRow(j);
					if (row == null)
					{
						row = sheet.CreateRow(j);
					}
					for (int k = 0; k <= count2; k++)
					{
						cell = row.GetCell(k);
						if (k == 0)
						{
							cell.SetCellValue((j - 3).ToString());
							continue;
						}
						string columnName;
						try
						{
							columnName = array.GetValue(k - 1).ToString();
						}
						catch
						{
							continue;
						}
						if (cell == null)
						{
							cell = row.CreateCell(k);
						}
						if (k == 1)
						{
							if (dataGridView2.Rows[count + 2 - j].Cells[columnName].Value != null)
							{
								cell.SetCellValue(dataGridView2.Rows[count + 2 - j].Cells[columnName].Value.ToString());
							}
						}
						else
						{
							double.TryParse(dataGridView2.Rows[count + 2 - j].Cells[columnName].Value.ToString(), out result2);
							cell.SetCellValue(result2.ToString("0.00000"));
						}
					}
					for (int l = 2; l <= count2; l++)
					{
						row = sheet.GetRow(3);
						cell = row.GetCell(l);
						cell.SetCellValue(array.GetValue(l - 1).ToString());
					}
				}
				using (fileStream = File.OpenWrite(Outpath))
				{
					workbook.Write(fileStream);
					result = true;
				}
				fileStream2.Close();
			}
			return result;
		}
		catch (Exception)
		{
			using (fileStream = File.OpenWrite(Outpath))
			{
				workbook.Write(fileStream);
				result = true;
			}
			fileStream.Close();
			return false;
		}
	}

	public bool dataToExcel3(DataGridView ordgv, string Outpath)
	{
		bool result = false;
		IWorkbook workbook = null;
		FileStream fileStream = null;
		IRow row = null;
		IRow row2 = null;
		ISheet sheet = null;
		ICell cell = null;
		ICell cell2 = null;
		bool flag = false;
		double result2 = 0.0;
		FileStream fileStream2 = new FileStream(Outpath, FileMode.Open, FileAccess.ReadWrite);
		try
		{
			string[] array = new string[ordgv.Columns.Count];
			foreach (DataGridViewColumn column in ordgv.Columns)
			{
				if (column.Visible)
				{
					array[column.DisplayIndex] = column.HeaderText;
				}
			}
			DataGridView dataGridView = new DataGridView();
			dataGridView = ordgv;
			if (dataGridView != null && dataGridView.Rows.Count > 0)
			{
				workbook = new HSSFWorkbook(fileStream2);
				sheet = workbook.GetSheetAt(0);
				int count = dataGridView.Rows.Count;
				int count2 = dataGridView.Columns.Count;
				int num = 0;
				int num2 = 1;
				row2 = sheet.GetRow(0);
				cell2 = row2.GetCell(0);
				row2 = sheet.GetRow(1);
				cell2 = row2.GetCell(0);
				cell2.SetCellValue("制表时间:" + DateTime.Now.ToString());
				row = sheet.GetRow(5);
				for (int i = 1; i < count2; i++)
				{
					cell = row.GetCell(i);
					if (cell == null)
					{
						cell = row.CreateCell(i);
					}
				}
				for (int j = 4; j < dataGridView3.Rows.Count + 3; j++)
				{
					if (j > 4)
					{
						IRow row3 = sheet.GetRow(5);
						MyInsertRow(sheet, j + 1, 1, row3);
					}
					row = sheet.GetRow(j);
					if (row == null)
					{
						row = sheet.CreateRow(j);
					}
					for (int k = 0; k <= count2; k++)
					{
						cell = row.GetCell(k);
						if (k == 0)
						{
							cell.SetCellValue((j - 3).ToString());
							continue;
						}
						string columnName;
						try
						{
							columnName = array.GetValue(k - 1).ToString();
						}
						catch
						{
							continue;
						}
						if (cell == null)
						{
							cell = row.CreateCell(k);
						}
						if (k == 1)
						{
							if (dataGridView3.Rows[count + 2 - j].Cells[columnName].Value != null)
							{
								cell.SetCellValue(dataGridView3.Rows[count + 2 - j].Cells[columnName].Value.ToString());
							}
						}
						else
						{
							double.TryParse(dataGridView3.Rows[count + 2 - j].Cells[columnName].Value.ToString(), out result2);
							cell.SetCellValue(result2.ToString("0.00000"));
						}
					}
					for (int l = 2; l <= count2; l++)
					{
						row = sheet.GetRow(3);
						cell = row.GetCell(l);
						cell.SetCellValue(array.GetValue(l - 1).ToString());
					}
				}
				using (fileStream = File.OpenWrite(Outpath))
				{
					workbook.Write(fileStream);
					result = true;
				}
				fileStream2.Close();
			}
			return result;
		}
		catch (Exception)
		{
			using (fileStream = File.OpenWrite(Outpath))
			{
				workbook.Write(fileStream);
				result = true;
			}
			fileStream.Close();
			return false;
		}
	}

	public static void MyInsertRow(ISheet sheet, int 插入行, int 插入行总数, IRow 源格式行)
	{
		sheet.ShiftRows(插入行, sheet.LastRowNum, 插入行总数, copyRowHeight: true, resetOriginalRowHeight: false);
		for (int i = 插入行; i < 插入行 + 插入行总数 - 1; i++)
		{
			IRow row = null;
			ICell cell = null;
			ICell cell2 = null;
			row = sheet.CreateRow(i + 1);
			for (int j = 源格式行.FirstCellNum; j < 源格式行.LastCellNum; j++)
			{
				cell = 源格式行.GetCell(j);
				if (cell != null)
				{
					cell2 = row.CreateCell(j);
					cell2.CellStyle = cell.CellStyle;
					cell2.SetCellType(cell.CellType);
				}
			}
		}
		IRow row2 = sheet.GetRow(插入行);
		ICell cell3 = null;
		ICell cell4 = null;
		for (int k = 源格式行.FirstCellNum; k < 源格式行.LastCellNum; k++)
		{
			cell3 = 源格式行.GetCell(k);
			if (cell3 != null)
			{
				cell4 = row2.CreateCell(k);
				cell4.CellStyle = cell3.CellStyle;
				cell4.SetCellType(cell3.CellType);
			}
		}
	}

	private void btnOpenChrom_Click(object sender, EventArgs e)
	{
		bool flag = true;
		DateTime dateTime = (DateTime)dataGridView1.CurrentRow.Cells["时间"].Value;
		DataTable dataTableRow = Class49.GetDataTableRow(0, ucCBSites.TextValue, dateTimePicker1.Value, dateTime.ToString("yyyy-MM-dd HH:mm:ss"));
		string chromName = dataTableRow.Rows[0]["谱图"].ToString();
		if (ChromForm.form == null)
		{
			ChromForm.form = new ChromForm();
		}
		ChromForm.form.Show();
		ChromForm.form.OpenChrom(chromName, sampling: true, useCurrent: true);
	}

	private void btnOpenChrom2_Click(object sender, EventArgs e)
	{
		if (frmParam.kindMachine == 4)
		{
			DateTime dateTime = (DateTime)dataGridView1.CurrentRow.Cells["时间"].Value;
			DataTable dataTableRow = Class49.GetDataTableRow(0, "vocTable", dateTimePicker1.Value, dateTime.ToString("yyyy-MM-dd HH:mm:ss"));
			string chromName = dataTableRow.Rows[0]["FileName"].ToString();
			if (ChromForm.form == null)
			{
				ChromForm.form = new ChromForm();
			}
			ChromForm.form.Show();
			ChromForm.form.OpenChrom(chromName, sampling: true, useCurrent: true);
		}
		else
		{
			DateTime dateTime2 = (DateTime)dataGridView3.CurrentRow.Cells["时间"].Value;
			DataTable dataTableRow2 = Class49.GetDataTableRow(0, "RNBTEX", dateTimePicker1.Value, dateTime2.ToString("yyyy-MM-dd HH:mm:ss"));
			string chromName2 = dataTableRow2.Rows[0]["FileName"].ToString();
			if (ChromForm.form == null)
			{
				ChromForm.form = new ChromForm();
			}
			ChromForm.form.Show();
			ChromForm.form.OpenChrom(chromName2, sampling: true, useCurrent: true);
		}
	}

	private void btnOpenChrom3_Click(object sender, EventArgs e)
	{
	}

	private void btnExplorer2_Click(object sender, EventArgs e)
	{
		SaveFileDialog saveFileDialog = new SaveFileDialog();
		saveFileDialog.FileName = Application.StartupPath + "\\VOCs数据" + DateTime.Now.ToString("yyyyMMddHHmm") + ".xls";
		saveFileDialog.Filter = " xls files(*.xls)|*.xls|All files(*.*)|*.*";
		saveFileDialog.FilterIndex = 2;
		saveFileDialog.RestoreDirectory = true;
		if (saveFileDialog.ShowDialog() == DialogResult.OK)
		{
			int num = 0;
			num = dataGridView3.Columns.Count - 1;
			if (num < 1)
			{
				num = 1;
			}
			FileStream fileStream = new FileStream(Application.StartupPath + "\\VOCs数据" + num + ".xls", FileMode.Open, FileAccess.Read);
			HSSFWorkbook hSSFWorkbook = new HSSFWorkbook(fileStream);
			ISheet sheetAt = hSSFWorkbook.GetSheetAt(0);
			sheetAt.ForceFormulaRecalculation = true;
			FileStream fileStream2 = new FileStream(saveFileDialog.FileName, FileMode.Create);
			hSSFWorkbook.Write(fileStream2);
			fileStream.Close();
			fileStream2.Close();
			DataGridView dataGridView = new DataGridView();
			if (dataGridView3.SelectedRows.Count <= 0)
			{
				return;
			}
			for (int i = 0; i < dataGridView3.ColumnCount; i++)
			{
				dataGridView.Columns.Add(dataGridView3.Columns[i].Name, dataGridView3.Columns[i].HeaderText);
			}
			foreach (DataGridViewRow selectedRow in dataGridView3.SelectedRows)
			{
				string[] array = new string[selectedRow.Cells.Count];
				for (int j = 0; j < selectedRow.Cells.Count; j++)
				{
					array[j] = selectedRow.Cells[j].Value.ToString();
				}
				try
				{
					dataGridView.Rows.Add();
					for (int k = 0; k < selectedRow.Cells.Count; k++)
					{
						dataGridView.Rows[dataGridView.Rows.Count - 1].Cells[k].Value = selectedRow.Cells[k].Value;
					}
				}
				catch (Exception)
				{
				}
			}
			dataToExcel3(dataGridView, saveFileDialog.FileName);
		}
		if (File.Exists(saveFileDialog.FileName))
		{
			Process.Start(saveFileDialog.FileName);
		}
	}

	private void ucCBSites_SelectedChangedEvent(object sender, EventArgs e)
	{
		if (!loading)
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
		this.components = new System.ComponentModel.Container();
		System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
		System.Windows.Forms.DataVisualization.Charting.Legend legend = new System.Windows.Forms.DataVisualization.Charting.Legend();
		System.Windows.Forms.DataVisualization.Charting.Series series = new System.Windows.Forms.DataVisualization.Charting.Series();
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(IBrainChrom2018.FormHistoryLX));
		this.splitContainer1 = new System.Windows.Forms.SplitContainer();
		this.splitContainer2 = new System.Windows.Forms.SplitContainer();
		this.splitContainer3 = new System.Windows.Forms.SplitContainer();
		this.dataGridView2 = new System.Windows.Forms.DataGridView();
		this.dataGridView3 = new System.Windows.Forms.DataGridView();
		this.dataGridView1 = new System.Windows.Forms.DataGridView();
		this.chartPlots = new System.Windows.Forms.DataVisualization.Charting.Chart();
		this.tbLowerLimit = new HZH_Controls.Controls.UCTextBoxEx();
		this.tbUpperLimit = new HZH_Controls.Controls.UCTextBoxEx();
		this.ucCBSites = new HZH_Controls.Controls.UCCombox();
		this.btnExplorer2 = new System.Windows.Forms.Button();
		this.btnOpenChrom3 = new System.Windows.Forms.Button();
		this.btnOpenChrom2 = new System.Windows.Forms.Button();
		this.btnOpenChrom = new System.Windows.Forms.Button();
		this.btnExplorer = new System.Windows.Forms.Button();
		this.label2 = new System.Windows.Forms.Label();
		this.label6 = new System.Windows.Forms.Label();
		this.label1 = new System.Windows.Forms.Label();
		this.label7 = new System.Windows.Forms.Label();
		this.dateTimePicker2 = new System.Windows.Forms.DateTimePicker();
		this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
		this.groupBox1 = new System.Windows.Forms.GroupBox();
		this.ch11 = new System.Windows.Forms.CheckBox();
		this.ch10 = new System.Windows.Forms.CheckBox();
		this.btnSaveLoadData = new System.Windows.Forms.Button();
		this.ch9 = new System.Windows.Forms.CheckBox();
		this.ch8 = new System.Windows.Forms.CheckBox();
		this.ch7 = new System.Windows.Forms.CheckBox();
		this.ch6 = new System.Windows.Forms.CheckBox();
		this.ch5 = new System.Windows.Forms.CheckBox();
		this.ch4 = new System.Windows.Forms.CheckBox();
		this.ch3 = new System.Windows.Forms.CheckBox();
		this.ch2 = new System.Windows.Forms.CheckBox();
		this.ch1 = new System.Windows.Forms.CheckBox();
		this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
		((System.ComponentModel.ISupportInitialize)this.splitContainer1).BeginInit();
		this.splitContainer1.Panel1.SuspendLayout();
		this.splitContainer1.Panel2.SuspendLayout();
		this.splitContainer1.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.splitContainer2).BeginInit();
		this.splitContainer2.Panel1.SuspendLayout();
		this.splitContainer2.Panel2.SuspendLayout();
		this.splitContainer2.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.splitContainer3).BeginInit();
		this.splitContainer3.Panel1.SuspendLayout();
		this.splitContainer3.Panel2.SuspendLayout();
		this.splitContainer3.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.dataGridView2).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.dataGridView3).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.dataGridView1).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.chartPlots).BeginInit();
		this.groupBox1.SuspendLayout();
		base.SuspendLayout();
		this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
		this.splitContainer1.Location = new System.Drawing.Point(0, 0);
		this.splitContainer1.Name = "splitContainer1";
		this.splitContainer1.Panel1.Controls.Add(this.splitContainer2);
		this.splitContainer1.Panel2.Controls.Add(this.tbLowerLimit);
		this.splitContainer1.Panel2.Controls.Add(this.tbUpperLimit);
		this.splitContainer1.Panel2.Controls.Add(this.ucCBSites);
		this.splitContainer1.Panel2.Controls.Add(this.btnExplorer2);
		this.splitContainer1.Panel2.Controls.Add(this.btnOpenChrom3);
		this.splitContainer1.Panel2.Controls.Add(this.btnOpenChrom2);
		this.splitContainer1.Panel2.Controls.Add(this.btnOpenChrom);
		this.splitContainer1.Panel2.Controls.Add(this.btnExplorer);
		this.splitContainer1.Panel2.Controls.Add(this.label2);
		this.splitContainer1.Panel2.Controls.Add(this.label6);
		this.splitContainer1.Panel2.Controls.Add(this.label1);
		this.splitContainer1.Panel2.Controls.Add(this.label7);
		this.splitContainer1.Panel2.Controls.Add(this.dateTimePicker2);
		this.splitContainer1.Panel2.Controls.Add(this.dateTimePicker1);
		this.splitContainer1.Panel2.Controls.Add(this.groupBox1);
		this.splitContainer1.Size = new System.Drawing.Size(784, 561);
		this.splitContainer1.SplitterDistance = 590;
		this.splitContainer1.TabIndex = 0;
		this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
		this.splitContainer2.Location = new System.Drawing.Point(0, 0);
		this.splitContainer2.Name = "splitContainer2";
		this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
		this.splitContainer2.Panel1.Controls.Add(this.splitContainer3);
		this.splitContainer2.Panel1.Controls.Add(this.dataGridView1);
		this.splitContainer2.Panel2.Controls.Add(this.chartPlots);
		this.splitContainer2.Size = new System.Drawing.Size(590, 561);
		this.splitContainer2.SplitterDistance = 319;
		this.splitContainer2.TabIndex = 0;
		this.splitContainer3.Location = new System.Drawing.Point(325, 0);
		this.splitContainer3.Name = "splitContainer3";
		this.splitContainer3.Panel1.Controls.Add(this.dataGridView2);
		this.splitContainer3.Panel2.Controls.Add(this.dataGridView3);
		this.splitContainer3.Size = new System.Drawing.Size(557, 300);
		this.splitContainer3.SplitterDistance = 210;
		this.splitContainer3.TabIndex = 4;
		this.splitContainer3.Visible = false;
		this.dataGridView2.AllowUserToOrderColumns = true;
		this.dataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
		this.dataGridView2.Dock = System.Windows.Forms.DockStyle.Fill;
		this.dataGridView2.Location = new System.Drawing.Point(0, 0);
		this.dataGridView2.Name = "dataGridView2";
		this.dataGridView2.RowTemplate.Height = 23;
		this.dataGridView2.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
		this.dataGridView2.Size = new System.Drawing.Size(210, 300);
		this.dataGridView2.TabIndex = 2;
		this.dataGridView3.AllowUserToOrderColumns = true;
		this.dataGridView3.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
		this.dataGridView3.Dock = System.Windows.Forms.DockStyle.Fill;
		this.dataGridView3.Location = new System.Drawing.Point(0, 0);
		this.dataGridView3.Name = "dataGridView3";
		this.dataGridView3.RowTemplate.Height = 23;
		this.dataGridView3.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
		this.dataGridView3.Size = new System.Drawing.Size(343, 300);
		this.dataGridView3.TabIndex = 3;
		this.dataGridView1.AllowUserToOrderColumns = true;
		this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
		this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
		this.dataGridView1.Location = new System.Drawing.Point(0, 0);
		this.dataGridView1.Name = "dataGridView1";
		this.dataGridView1.RowTemplate.Height = 23;
		this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
		this.dataGridView1.Size = new System.Drawing.Size(590, 319);
		this.dataGridView1.TabIndex = 1;
		chartArea.Name = "ChartArea1";
		this.chartPlots.ChartAreas.Add(chartArea);
		this.chartPlots.Dock = System.Windows.Forms.DockStyle.Fill;
		legend.Name = "Legend1";
		this.chartPlots.Legends.Add(legend);
		this.chartPlots.Location = new System.Drawing.Point(0, 0);
		this.chartPlots.Name = "chartPlots";
		series.ChartArea = "ChartArea1";
		series.Legend = "Legend1";
		series.Name = "Series1";
		this.chartPlots.Series.Add(series);
		this.chartPlots.Size = new System.Drawing.Size(590, 238);
		this.chartPlots.TabIndex = 1;
		this.chartPlots.Text = "chart1";
		this.tbLowerLimit.BackColor = System.Drawing.Color.Transparent;
		this.tbLowerLimit.ConerRadius = 5;
		this.tbLowerLimit.Cursor = System.Windows.Forms.Cursors.IBeam;
		this.tbLowerLimit.DecLength = 2;
		this.tbLowerLimit.FillColor = System.Drawing.Color.Empty;
		this.tbLowerLimit.FocusBorderColor = System.Drawing.Color.FromArgb(255, 77, 59);
		this.tbLowerLimit.Font = new System.Drawing.Font("微软雅黑", 18f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
		this.tbLowerLimit.InputText = "";
		this.tbLowerLimit.InputType = HZH_Controls.TextInputType.NotControl;
		this.tbLowerLimit.IsFocusColor = true;
		this.tbLowerLimit.IsRadius = true;
		this.tbLowerLimit.IsShowClearBtn = true;
		this.tbLowerLimit.IsShowKeyboard = true;
		this.tbLowerLimit.IsShowRect = true;
		this.tbLowerLimit.IsShowSearchBtn = false;
		this.tbLowerLimit.KeyBoardType = HZH_Controls.Controls.KeyBoardType.UCKeyBorderNum;
		this.tbLowerLimit.Location = new System.Drawing.Point(69, 524);
		this.tbLowerLimit.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
		this.tbLowerLimit.MaxValue = new decimal(new int[4] { 1000000, 0, 0, 0 });
		this.tbLowerLimit.MinValue = new decimal(new int[4] { 1000000, 0, 0, -2147483648 });
		this.tbLowerLimit.Name = "tbLowerLimit";
		this.tbLowerLimit.Padding = new System.Windows.Forms.Padding(5);
		this.tbLowerLimit.PasswordChar = '\0';
		this.tbLowerLimit.PromptColor = System.Drawing.Color.Gray;
		this.tbLowerLimit.PromptFont = new System.Drawing.Font("微软雅黑", 15f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
		this.tbLowerLimit.PromptText = "";
		this.tbLowerLimit.RectColor = System.Drawing.Color.FromArgb(220, 220, 220);
		this.tbLowerLimit.RectWidth = 1;
		this.tbLowerLimit.RegexPattern = "";
		this.tbLowerLimit.Size = new System.Drawing.Size(108, 42);
		this.tbLowerLimit.TabIndex = 35;
		this.tbUpperLimit.BackColor = System.Drawing.Color.Transparent;
		this.tbUpperLimit.ConerRadius = 5;
		this.tbUpperLimit.Cursor = System.Windows.Forms.Cursors.IBeam;
		this.tbUpperLimit.DecLength = 2;
		this.tbUpperLimit.FillColor = System.Drawing.Color.Empty;
		this.tbUpperLimit.FocusBorderColor = System.Drawing.Color.FromArgb(255, 77, 59);
		this.tbUpperLimit.Font = new System.Drawing.Font("微软雅黑", 18f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
		this.tbUpperLimit.InputText = "";
		this.tbUpperLimit.InputType = HZH_Controls.TextInputType.NotControl;
		this.tbUpperLimit.IsFocusColor = true;
		this.tbUpperLimit.IsRadius = true;
		this.tbUpperLimit.IsShowClearBtn = true;
		this.tbUpperLimit.IsShowKeyboard = true;
		this.tbUpperLimit.IsShowRect = true;
		this.tbUpperLimit.IsShowSearchBtn = false;
		this.tbUpperLimit.KeyBoardType = HZH_Controls.Controls.KeyBoardType.UCKeyBorderNum;
		this.tbUpperLimit.Location = new System.Drawing.Point(69, 472);
		this.tbUpperLimit.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
		this.tbUpperLimit.MaxValue = new decimal(new int[4] { 1000000, 0, 0, 0 });
		this.tbUpperLimit.MinValue = new decimal(new int[4] { 1000000, 0, 0, -2147483648 });
		this.tbUpperLimit.Name = "tbUpperLimit";
		this.tbUpperLimit.Padding = new System.Windows.Forms.Padding(5);
		this.tbUpperLimit.PasswordChar = '\0';
		this.tbUpperLimit.PromptColor = System.Drawing.Color.Gray;
		this.tbUpperLimit.PromptFont = new System.Drawing.Font("微软雅黑", 15f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
		this.tbUpperLimit.PromptText = "";
		this.tbUpperLimit.RectColor = System.Drawing.Color.FromArgb(220, 220, 220);
		this.tbUpperLimit.RectWidth = 1;
		this.tbUpperLimit.RegexPattern = "";
		this.tbUpperLimit.Size = new System.Drawing.Size(108, 42);
		this.tbUpperLimit.TabIndex = 12;
		this.ucCBSites.BackColor = System.Drawing.Color.Transparent;
		this.ucCBSites.BackColorExt = System.Drawing.Color.FromArgb(240, 240, 240);
		this.ucCBSites.BoxStyle = System.Windows.Forms.ComboBoxStyle.DropDown;
		this.ucCBSites.ConerRadius = 5;
		this.ucCBSites.DropPanelHeight = -1;
		this.ucCBSites.FillColor = System.Drawing.Color.White;
		this.ucCBSites.Font = new System.Drawing.Font("微软雅黑", 12f);
		this.ucCBSites.IsRadius = true;
		this.ucCBSites.IsShowRect = true;
		this.ucCBSites.ItemWidth = 70;
		this.ucCBSites.Location = new System.Drawing.Point(14, 68);
		this.ucCBSites.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
		this.ucCBSites.Name = "ucCBSites";
		this.ucCBSites.RectColor = System.Drawing.Color.FromArgb(220, 220, 220);
		this.ucCBSites.RectWidth = 1;
		this.ucCBSites.SelectedIndex = -1;
		this.ucCBSites.SelectedValue = "";
		this.ucCBSites.Size = new System.Drawing.Size(143, 32);
		this.ucCBSites.Source = null;
		this.ucCBSites.TabIndex = 34;
		this.ucCBSites.TextValue = null;
		this.ucCBSites.TriangleColor = System.Drawing.Color.FromArgb(255, 77, 59);
		this.ucCBSites.SelectedChangedEvent += new System.EventHandler(ucCBSites_SelectedChangedEvent);
		this.btnExplorer2.Location = new System.Drawing.Point(-2, 620);
		this.btnExplorer2.Name = "btnExplorer2";
		this.btnExplorer2.Size = new System.Drawing.Size(179, 38);
		this.btnExplorer2.TabIndex = 33;
		this.btnExplorer2.Text = "苯系物数据导出";
		this.btnExplorer2.UseVisualStyleBackColor = true;
		this.btnExplorer2.Visible = false;
		this.btnExplorer2.Click += new System.EventHandler(btnExplorer2_Click);
		this.btnOpenChrom3.Location = new System.Drawing.Point(192, 627);
		this.btnOpenChrom3.Name = "btnOpenChrom3";
		this.btnOpenChrom3.Size = new System.Drawing.Size(192, 38);
		this.btnOpenChrom3.TabIndex = 32;
		this.btnOpenChrom3.Text = "打开TCD谱图";
		this.btnOpenChrom3.UseVisualStyleBackColor = true;
		this.btnOpenChrom3.Visible = false;
		this.btnOpenChrom3.Click += new System.EventHandler(btnOpenChrom3_Click);
		this.btnOpenChrom2.Location = new System.Drawing.Point(7, 473);
		this.btnOpenChrom2.Name = "btnOpenChrom2";
		this.btnOpenChrom2.Size = new System.Drawing.Size(180, 38);
		this.btnOpenChrom2.TabIndex = 31;
		this.btnOpenChrom2.Text = "打开FID2谱图";
		this.btnOpenChrom2.UseVisualStyleBackColor = true;
		this.btnOpenChrom2.Visible = false;
		this.btnOpenChrom2.Click += new System.EventHandler(btnOpenChrom2_Click);
		this.btnOpenChrom.Location = new System.Drawing.Point(7, 429);
		this.btnOpenChrom.Name = "btnOpenChrom";
		this.btnOpenChrom.Size = new System.Drawing.Size(179, 38);
		this.btnOpenChrom.TabIndex = 30;
		this.btnOpenChrom.Text = "打开谱图";
		this.btnOpenChrom.UseVisualStyleBackColor = true;
		this.btnOpenChrom.Click += new System.EventHandler(btnOpenChrom_Click);
		this.btnExplorer.Location = new System.Drawing.Point(7, 383);
		this.btnExplorer.Name = "btnExplorer";
		this.btnExplorer.Size = new System.Drawing.Size(180, 38);
		this.btnExplorer.TabIndex = 29;
		this.btnExplorer.Text = "数据导出";
		this.btnExplorer.UseVisualStyleBackColor = true;
		this.btnExplorer.Click += new System.EventHandler(btnExplorer_Click);
		this.label2.AutoSize = true;
		this.label2.Location = new System.Drawing.Point(8, 539);
		this.label2.Name = "label2";
		this.label2.Size = new System.Drawing.Size(59, 12);
		this.label2.TabIndex = 11;
		this.label2.Text = "含量下限:";
		this.label6.AutoSize = true;
		this.label6.Location = new System.Drawing.Point(12, 43);
		this.label6.Name = "label6";
		this.label6.Size = new System.Drawing.Size(65, 12);
		this.label6.TabIndex = 28;
		this.label6.Text = "结束日期：";
		this.label1.AutoSize = true;
		this.label1.Location = new System.Drawing.Point(8, 490);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(59, 12);
		this.label1.TabIndex = 10;
		this.label1.Text = "含量上限:";
		this.label7.AutoSize = true;
		this.label7.Location = new System.Drawing.Point(12, 18);
		this.label7.Name = "label7";
		this.label7.Size = new System.Drawing.Size(65, 12);
		this.label7.TabIndex = 27;
		this.label7.Text = "起始日期：";
		this.dateTimePicker2.CustomFormat = "yyyy/MM/dd";
		this.dateTimePicker2.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
		this.dateTimePicker2.Location = new System.Drawing.Point(79, 39);
		this.dateTimePicker2.Name = "dateTimePicker2";
		this.dateTimePicker2.Size = new System.Drawing.Size(107, 21);
		this.dateTimePicker2.TabIndex = 26;
		this.dateTimePicker2.ValueChanged += new System.EventHandler(dateTimePicker2_ValueChanged);
		this.dateTimePicker1.CustomFormat = "yyyy/MM/dd";
		this.dateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
		this.dateTimePicker1.Location = new System.Drawing.Point(79, 12);
		this.dateTimePicker1.Name = "dateTimePicker1";
		this.dateTimePicker1.Size = new System.Drawing.Size(107, 21);
		this.dateTimePicker1.TabIndex = 25;
		this.dateTimePicker1.Value = new System.DateTime(2018, 7, 12, 0, 0, 0, 0);
		this.dateTimePicker1.ValueChanged += new System.EventHandler(dateTimePicker1_ValueChanged);
		this.groupBox1.Controls.Add(this.ch11);
		this.groupBox1.Controls.Add(this.ch10);
		this.groupBox1.Controls.Add(this.btnSaveLoadData);
		this.groupBox1.Controls.Add(this.ch9);
		this.groupBox1.Controls.Add(this.ch8);
		this.groupBox1.Controls.Add(this.ch7);
		this.groupBox1.Controls.Add(this.ch6);
		this.groupBox1.Controls.Add(this.ch5);
		this.groupBox1.Controls.Add(this.ch4);
		this.groupBox1.Controls.Add(this.ch3);
		this.groupBox1.Controls.Add(this.ch2);
		this.groupBox1.Controls.Add(this.ch1);
		this.groupBox1.Location = new System.Drawing.Point(14, 108);
		this.groupBox1.Name = "groupBox1";
		this.groupBox1.Size = new System.Drawing.Size(140, 268);
		this.groupBox1.TabIndex = 24;
		this.groupBox1.TabStop = false;
		this.groupBox1.Text = "显示组份";
		this.ch11.AutoSize = true;
		this.ch11.Location = new System.Drawing.Point(17, 240);
		this.ch11.Name = "ch11";
		this.ch11.Size = new System.Drawing.Size(36, 16);
		this.ch11.TabIndex = 11;
		this.ch11.Text = "11";
		this.ch11.UseVisualStyleBackColor = true;
		this.ch10.AutoSize = true;
		this.ch10.Location = new System.Drawing.Point(17, 218);
		this.ch10.Name = "ch10";
		this.ch10.Size = new System.Drawing.Size(36, 16);
		this.ch10.TabIndex = 10;
		this.ch10.Text = "10";
		this.ch10.UseVisualStyleBackColor = true;
		this.btnSaveLoadData.Location = new System.Drawing.Point(59, 233);
		this.btnSaveLoadData.Name = "btnSaveLoadData";
		this.btnSaveLoadData.Size = new System.Drawing.Size(75, 23);
		this.btnSaveLoadData.TabIndex = 9;
		this.btnSaveLoadData.Text = "确定";
		this.btnSaveLoadData.UseVisualStyleBackColor = true;
		this.btnSaveLoadData.Click += new System.EventHandler(btnSaveLoadData_Click);
		this.ch9.AutoSize = true;
		this.ch9.Location = new System.Drawing.Point(17, 196);
		this.ch9.Name = "ch9";
		this.ch9.Size = new System.Drawing.Size(30, 16);
		this.ch9.TabIndex = 8;
		this.ch9.Text = "9";
		this.ch9.UseVisualStyleBackColor = true;
		this.ch8.AutoSize = true;
		this.ch8.Location = new System.Drawing.Point(17, 174);
		this.ch8.Name = "ch8";
		this.ch8.Size = new System.Drawing.Size(30, 16);
		this.ch8.TabIndex = 7;
		this.ch8.Text = "8";
		this.ch8.UseVisualStyleBackColor = true;
		this.ch7.AutoSize = true;
		this.ch7.Location = new System.Drawing.Point(17, 152);
		this.ch7.Name = "ch7";
		this.ch7.Size = new System.Drawing.Size(30, 16);
		this.ch7.TabIndex = 6;
		this.ch7.Text = "7";
		this.ch7.UseVisualStyleBackColor = true;
		this.ch6.AutoSize = true;
		this.ch6.Location = new System.Drawing.Point(17, 130);
		this.ch6.Name = "ch6";
		this.ch6.Size = new System.Drawing.Size(30, 16);
		this.ch6.TabIndex = 5;
		this.ch6.Text = "6";
		this.ch6.UseVisualStyleBackColor = true;
		this.ch5.AutoSize = true;
		this.ch5.Location = new System.Drawing.Point(17, 108);
		this.ch5.Name = "ch5";
		this.ch5.Size = new System.Drawing.Size(30, 16);
		this.ch5.TabIndex = 4;
		this.ch5.Text = "5";
		this.ch5.UseVisualStyleBackColor = true;
		this.ch4.AutoSize = true;
		this.ch4.Location = new System.Drawing.Point(17, 86);
		this.ch4.Name = "ch4";
		this.ch4.Size = new System.Drawing.Size(30, 16);
		this.ch4.TabIndex = 3;
		this.ch4.Text = "4";
		this.ch4.UseVisualStyleBackColor = true;
		this.ch3.AutoSize = true;
		this.ch3.Location = new System.Drawing.Point(17, 64);
		this.ch3.Name = "ch3";
		this.ch3.Size = new System.Drawing.Size(30, 16);
		this.ch3.TabIndex = 2;
		this.ch3.Text = "3";
		this.ch3.UseVisualStyleBackColor = true;
		this.ch2.AutoSize = true;
		this.ch2.Location = new System.Drawing.Point(17, 42);
		this.ch2.Name = "ch2";
		this.ch2.Size = new System.Drawing.Size(30, 16);
		this.ch2.TabIndex = 1;
		this.ch2.Text = "2";
		this.ch2.UseVisualStyleBackColor = true;
		this.ch1.AutoSize = true;
		this.ch1.Location = new System.Drawing.Point(17, 20);
		this.ch1.Name = "ch1";
		this.ch1.Size = new System.Drawing.Size(30, 16);
		this.ch1.TabIndex = 0;
		this.ch1.Text = "1";
		this.ch1.UseVisualStyleBackColor = true;
		this.contextMenuStrip1.Name = "contextMenuStrip1";
		this.contextMenuStrip1.Size = new System.Drawing.Size(61, 4);
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(784, 561);
		base.Controls.Add(this.splitContainer1);
		base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
		base.Name = "FormHistoryLX";
		this.Text = "FormHistory";
		this.splitContainer1.Panel1.ResumeLayout(false);
		this.splitContainer1.Panel2.ResumeLayout(false);
		this.splitContainer1.Panel2.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.splitContainer1).EndInit();
		this.splitContainer1.ResumeLayout(false);
		this.splitContainer2.Panel1.ResumeLayout(false);
		this.splitContainer2.Panel2.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.splitContainer2).EndInit();
		this.splitContainer2.ResumeLayout(false);
		this.splitContainer3.Panel1.ResumeLayout(false);
		this.splitContainer3.Panel2.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.splitContainer3).EndInit();
		this.splitContainer3.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.dataGridView2).EndInit();
		((System.ComponentModel.ISupportInitialize)this.dataGridView3).EndInit();
		((System.ComponentModel.ISupportInitialize)this.dataGridView1).EndInit();
		((System.ComponentModel.ISupportInitialize)this.chartPlots).EndInit();
		this.groupBox1.ResumeLayout(false);
		this.groupBox1.PerformLayout();
		base.ResumeLayout(false);
	}
}
