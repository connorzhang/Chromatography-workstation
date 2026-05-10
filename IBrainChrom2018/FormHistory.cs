using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using IBrainChrom2018.Unit;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;

namespace IBrainChrom2018;

public class FormHistory : Form
{
	public static FormHistory selfCtrl;

	private DataTable dataSource1 = new DataTable();

	private FormMainParam frmParam = FormMainParam.Create();

	private List<string> strFileName1 = new List<string>();

	private List<string> strSerialName = new List<string>();

	private List<Color> strSerialColor = new List<Color>();

	private List<string> strSerialTime = new List<string>();

	private List<string> strSerialTime2 = new List<string>();

	public Series[] serieLines = new Series[11];

	private bool loading = true;

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

	private TextBox tbLowerLimit;

	private TextBox tbUpperLimit;

	private Button btnExplorer;

	private CheckBox ch11;

	private Button btnOpenChrom;

	private Button btnOpenChrom3;

	private Button btnOpenChrom2;

	private DataGridView dataGridView3;

	private DataGridView dataGridView2;

	private SplitContainer splitContainer3;

	private Button btnExplorer2;

	public FormHistory()
	{
		selfCtrl = this;
		InitializeComponent();
		initForm();
	}

	public void initForm()
	{
		if (frmParam.kindMachine == 4)
		{
			btnExplorer.Text = "数据导出";
			btnExplorer2.Visible = false;
		}
		else
		{
			btnExplorer.Text = "非甲烷总烃数据导出";
			btnExplorer2.Text = "苯系物数据导出";
		}
		dataGridView1.ReadOnly = true;
		dataGridView2.ReadOnly = true;
		dataGridView3.ReadOnly = true;
		strSerialName.Clear();
		strSerialName.Add("总烃");
		strSerialName.Add("甲烷");
		strSerialName.Add("非甲烷总烃");
		strSerialName.Add("苯");
		strSerialName.Add("甲苯");
		strSerialName.Add("间对二甲苯");
		strSerialName.Add("邻二甲苯");
		strSerialName.Add("乙苯");
		strSerialName.Add("异丙苯");
		strSerialName.Add("苯乙烯");
		strSerialName.Add("苯系物");
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
		tbUpperLimit.Text = frmParam.UpperLimit.ToString();
		tbLowerLimit.Text = frmParam.LowerLimit.ToString();
		InitChart();
		loading = false;
	}

	public void loadData()
	{
		if (frmParam.kindMachine == 4)
		{
			splitContainer3.Visible = false;
			dataGridView1.Dock = DockStyle.Fill;
			DataTable dataTableMINE = Class49.GetDataTableMINE(0, "vocTable", dateTimePicker1.Value, dateTimePicker2.Value);
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
			if (frmParam.kindMachine == 4)
			{
				relodColumn();
				try
				{
					dataGridView1.Columns.Remove("FileName");
					dataGridView1.Columns.Remove("备用2");
				}
				catch
				{
				}
			}
			else
			{
				relodColumn2();
				try
				{
					if (dataGridView2.Columns.Contains("FileName"))
					{
						dataGridView2.Columns.Remove("FileName");
					}
					if (dataGridView3.Columns.Contains("FileName"))
					{
						dataGridView3.Columns.Remove("FileName");
					}
					if (dataGridView3.Columns.Contains("备用2"))
					{
						dataGridView3.Columns.Remove("备用2");
					}
				}
				catch
				{
				}
			}
			loadSeriesDate();
			return;
		}
		dataGridView1.Visible = false;
		splitContainer3.Dock = DockStyle.Fill;
		dataGridView2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
		dataGridView3.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
		DataTable dataTableMINE2 = Class49.GetDataTableMINE(0, "RNNMHC", dateTimePicker1.Value, dateTimePicker2.Value);
		if (dataTableMINE2 == null)
		{
			return;
		}
		dataGridView2.DataSource = dataTableMINE2;
		DataTable dataTableMINE3 = Class49.GetDataTableMINE(0, "RNBTEX", dateTimePicker1.Value, dateTimePicker2.Value);
		if (dataTableMINE3 == null)
		{
			return;
		}
		dataGridView3.DataSource = dataTableMINE3;
		relodColumn2();
		try
		{
			if (dataGridView2.Columns.Contains("FileName"))
			{
				dataGridView2.Columns.Remove("FileName");
			}
			if (dataGridView3.Columns.Contains("FileName"))
			{
				dataGridView3.Columns.Remove("FileName");
			}
			if (dataGridView3.Columns.Contains("备用2"))
			{
				dataGridView3.Columns.Remove("备用2");
			}
		}
		catch
		{
		}
		loadSeriesDate2();
	}

	public void relodColumn()
	{
		try
		{
			if (!frmParam.bSum[0])
			{
				dataGridView1.Columns.Remove("总烃");
			}
		}
		catch
		{
		}
		try
		{
			if (!frmParam.bSum[1])
			{
				dataGridView1.Columns.Remove("甲烷");
			}
		}
		catch
		{
		}
		try
		{
			if (!frmParam.bSum[2])
			{
				dataGridView1.Columns.Remove("非甲烷总烃");
			}
		}
		catch
		{
		}
		try
		{
			if (!frmParam.bSum[3])
			{
				dataGridView1.Columns.Remove("苯");
			}
		}
		catch
		{
		}
		try
		{
			if (!frmParam.bSum[4])
			{
				dataGridView1.Columns.Remove("甲苯");
			}
		}
		catch
		{
		}
		try
		{
			if (!frmParam.bSum[5])
			{
				dataGridView1.Columns.Remove("间对二甲苯");
			}
		}
		catch
		{
		}
		try
		{
			if (!frmParam.bSum[6])
			{
				dataGridView1.Columns.Remove("邻二甲苯");
			}
		}
		catch
		{
		}
		try
		{
			if (!frmParam.bSum[7])
			{
				dataGridView1.Columns.Remove("乙苯");
			}
		}
		catch
		{
		}
		try
		{
			if (!frmParam.bSum[8])
			{
				dataGridView1.Columns.Remove("异丙苯");
			}
		}
		catch
		{
		}
		try
		{
			if (!frmParam.bSum[9])
			{
				dataGridView1.Columns.Remove("苯乙烯");
			}
		}
		catch
		{
		}
		try
		{
			if (!frmParam.bSum[10])
			{
				dataGridView1.Columns.Remove("苯系物");
			}
		}
		catch
		{
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
		chartPlots.ChartAreas[0].AxisY.Title = "含量百分比";
		chartPlots.ChartAreas[0].AxisY.TitleForeColor = Color.Crimson;
		chartPlots.ChartAreas[0].AxisY.Maximum = float.Parse(tbUpperLimit.Text);
		chartPlots.ChartAreas[0].AxisY.Minimum = float.Parse(tbLowerLimit.Text);
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
		for (int i = 0; i < 11; i++)
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
			if (dataGridView1.Columns["总烃"] != null && dataGridView1.Rows[k].Cells["总烃"].Value != null)
			{
				serieLines[0].Points.AddXY(strSerialTime[k], dataGridView1.Rows[k].Cells["总烃"].Value.ToString());
			}
			if (dataGridView1.Columns["甲烷"] != null && dataGridView1.Rows[k].Cells["甲烷"].Value != null)
			{
				serieLines[1].Points.AddXY(strSerialTime[k], dataGridView1.Rows[k].Cells["甲烷"].Value.ToString());
			}
			if (dataGridView1.Columns["非甲烷总烃"] != null && dataGridView1.Rows[k].Cells["非甲烷总烃"].Value != null)
			{
				serieLines[2].Points.AddXY(strSerialTime[k], dataGridView1.Rows[k].Cells["非甲烷总烃"].Value.ToString());
			}
			if (dataGridView1.Columns["苯"] != null && dataGridView1.Rows[k].Cells["苯"].Value != null)
			{
				serieLines[3].Points.AddXY(strSerialTime[k], dataGridView1.Rows[k].Cells["苯"].Value.ToString());
			}
			if (dataGridView1.Columns["甲苯"] != null && dataGridView1.Rows[k].Cells["甲苯"].Value != null)
			{
				serieLines[4].Points.AddXY(strSerialTime[k], dataGridView1.Rows[k].Cells["甲苯"].Value.ToString());
			}
			if (dataGridView1.Columns["间对二甲苯"] != null && dataGridView1.Rows[k].Cells["间对二甲苯"].Value != null)
			{
				serieLines[5].Points.AddXY(strSerialTime[k], dataGridView1.Rows[k].Cells["间对二甲苯"].Value.ToString());
			}
			if (dataGridView1.Columns["邻二甲苯"] != null && dataGridView1.Rows[k].Cells["邻二甲苯"].Value != null)
			{
				serieLines[6].Points.AddXY(strSerialTime[k], dataGridView1.Rows[k].Cells["邻二甲苯"].Value.ToString());
			}
			if (dataGridView1.Columns["乙苯"] != null && dataGridView1.Rows[k].Cells["乙苯"].Value != null)
			{
				serieLines[7].Points.AddXY(strSerialTime[k], dataGridView1.Rows[k].Cells["乙苯"].Value.ToString());
			}
			if (dataGridView1.Columns["异丙苯"] != null && dataGridView1.Rows[k].Cells["异丙苯"].Value != null)
			{
				serieLines[8].Points.AddXY(strSerialTime[k], dataGridView1.Rows[k].Cells["异丙苯"].Value.ToString());
			}
			if (dataGridView1.Columns["苯乙烯"] != null && dataGridView1.Rows[k].Cells["苯乙烯"].Value != null)
			{
				serieLines[9].Points.AddXY(strSerialTime[k], dataGridView1.Rows[k].Cells["苯乙烯"].Value.ToString());
			}
			if (dataGridView1.Columns["苯系物"] != null && dataGridView1.Rows[k].Cells["苯系物"].Value != null)
			{
				serieLines[10].Points.AddXY(strSerialTime[k], dataGridView1.Rows[k].Cells["苯系物"].Value.ToString());
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
		for (int i = 0; i < 11; i++)
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
			if (dataGridView3.Columns["苯"] != null && dataGridView3.Rows[k].Cells["苯"].Value != null)
			{
				serieLines[3].Points.AddXY(strSerialTime2[k], dataGridView3.Rows[k].Cells["苯"].Value.ToString());
			}
			if (dataGridView3.Columns["甲苯"] != null && dataGridView3.Rows[k].Cells["甲苯"].Value != null)
			{
				serieLines[4].Points.AddXY(strSerialTime2[k], dataGridView3.Rows[k].Cells["甲苯"].Value.ToString());
			}
			if (dataGridView3.Columns["间对二甲苯"] != null && dataGridView3.Rows[k].Cells["间对二甲苯"].Value != null)
			{
				serieLines[5].Points.AddXY(strSerialTime2[k], dataGridView3.Rows[k].Cells["间对二甲苯"].Value.ToString());
			}
			if (dataGridView3.Columns["邻二甲苯"] != null && dataGridView3.Rows[k].Cells["邻二甲苯"].Value != null)
			{
				serieLines[6].Points.AddXY(strSerialTime2[k], dataGridView3.Rows[k].Cells["邻二甲苯"].Value.ToString());
			}
			if (dataGridView3.Columns["乙苯"] != null && dataGridView3.Rows[k].Cells["乙苯"].Value != null)
			{
				serieLines[7].Points.AddXY(strSerialTime2[k], dataGridView3.Rows[k].Cells["乙苯"].Value.ToString());
			}
			if (dataGridView3.Columns["异丙苯"] != null && dataGridView3.Rows[k].Cells["异丙苯"].Value != null)
			{
				serieLines[8].Points.AddXY(strSerialTime2[k], dataGridView3.Rows[k].Cells["异丙苯"].Value.ToString());
			}
			if (dataGridView3.Columns["苯乙烯"] != null && dataGridView3.Rows[k].Cells["苯乙烯"].Value != null)
			{
				serieLines[9].Points.AddXY(strSerialTime2[k], dataGridView3.Rows[k].Cells["苯乙烯"].Value.ToString());
			}
			if (dataGridView3.Columns["苯系物"] != null && dataGridView3.Rows[k].Cells["苯系物"].Value != null)
			{
				serieLines[10].Points.AddXY(strSerialTime2[k], dataGridView3.Rows[k].Cells["苯系物"].Value.ToString());
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
			float.TryParse(tbUpperLimit.Text, out frmParam.UpperLimit);
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
			float.TryParse(tbLowerLimit.Text, out frmParam.LowerLimit);
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
		if (frmParam.kindMachine == 4)
		{
			SaveFileDialog saveFileDialog = new SaveFileDialog();
			saveFileDialog.FileName = Application.StartupPath + "\\VOCs数据" + DateTime.Now.ToString("yyyyMMddHHmm") + ".xls";
			saveFileDialog.Filter = " xls files(*.xls)|*.xls|All files(*.*)|*.*";
			saveFileDialog.FilterIndex = 2;
			saveFileDialog.RestoreDirectory = true;
			if (saveFileDialog.ShowDialog() == DialogResult.OK)
			{
				int num = 0;
				num = dataGridView1.Columns.Count - 1;
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
				if (dataGridView1.SelectedRows.Count <= 0)
				{
					return;
				}
				for (int i = 0; i < dataGridView1.ColumnCount; i++)
				{
					dataGridView.Columns.Add(dataGridView1.Columns[i].Name, dataGridView1.Columns[i].HeaderText);
				}
				foreach (DataGridViewRow selectedRow in dataGridView1.SelectedRows)
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
				dataToExcel(dataGridView, saveFileDialog.FileName);
			}
			if (File.Exists(saveFileDialog.FileName))
			{
				Process.Start(saveFileDialog.FileName);
			}
			return;
		}
		SaveFileDialog saveFileDialog2 = new SaveFileDialog();
		saveFileDialog2.FileName = Application.StartupPath + "\\VOCs数据" + DateTime.Now.ToString("yyyyMMddHHmm") + ".xls";
		saveFileDialog2.Filter = " xls files(*.xls)|*.xls|All files(*.*)|*.*";
		saveFileDialog2.FilterIndex = 2;
		saveFileDialog2.RestoreDirectory = true;
		if (saveFileDialog2.ShowDialog() == DialogResult.OK)
		{
			int num2 = 0;
			num2 = this.dataGridView2.Columns.Count - 1;
			if (num2 < 1)
			{
				num2 = 1;
			}
			FileStream fileStream3 = new FileStream(Application.StartupPath + "\\VOCs数据" + num2 + ".xls", FileMode.Open, FileAccess.Read);
			HSSFWorkbook hSSFWorkbook2 = new HSSFWorkbook(fileStream3);
			ISheet sheetAt2 = hSSFWorkbook2.GetSheetAt(0);
			sheetAt2.ForceFormulaRecalculation = true;
			FileStream fileStream4 = new FileStream(saveFileDialog2.FileName, FileMode.Create);
			hSSFWorkbook2.Write(fileStream4);
			fileStream3.Close();
			fileStream4.Close();
			DataGridView dataGridView2 = new DataGridView();
			if (this.dataGridView2.SelectedRows.Count <= 0)
			{
				return;
			}
			for (int l = 0; l < this.dataGridView2.ColumnCount; l++)
			{
				dataGridView2.Columns.Add(this.dataGridView2.Columns[l].Name, this.dataGridView2.Columns[l].HeaderText);
			}
			foreach (DataGridViewRow selectedRow2 in this.dataGridView2.SelectedRows)
			{
				string[] array2 = new string[selectedRow2.Cells.Count];
				for (int m = 0; m < selectedRow2.Cells.Count; m++)
				{
					array2[m] = selectedRow2.Cells[m].Value.ToString();
				}
				try
				{
					dataGridView2.Rows.Add();
					for (int n = 0; n < selectedRow2.Cells.Count; n++)
					{
						dataGridView2.Rows[dataGridView2.Rows.Count - 1].Cells[n].Value = selectedRow2.Cells[n].Value;
					}
				}
				catch (Exception)
				{
				}
			}
			dataToExcel2(dataGridView2, saveFileDialog2.FileName);
		}
		if (File.Exists(saveFileDialog2.FileName))
		{
			Process.Start(saveFileDialog2.FileName);
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
							if (dataGridView1.Rows[count + 2 - j].Cells[columnName].Value != null)
							{
								cell.SetCellValue(dataGridView1.Rows[count + 2 - j].Cells[columnName].Value.ToString());
							}
						}
						else
						{
							double.TryParse(dataGridView1.Rows[count + 2 - j].Cells[columnName].Value.ToString(), out result2);
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
			DateTime dateTime2 = (DateTime)dataGridView2.CurrentRow.Cells["时间"].Value;
			DataTable dataTableRow2 = Class49.GetDataTableRow(0, "RNNMHC", dateTimePicker1.Value, dateTime2.ToString("yyyy-MM-dd HH:mm:ss"));
			string chromName2 = dataTableRow2.Rows[0]["FileName"].ToString();
			if (ChromForm.form == null)
			{
				ChromForm.form = new ChromForm();
			}
			ChromForm.form.Show();
			ChromForm.form.OpenChrom(chromName2, sampling: true, useCurrent: true);
		}
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(IBrainChrom2018.FormHistory));
		this.splitContainer1 = new System.Windows.Forms.SplitContainer();
		this.splitContainer2 = new System.Windows.Forms.SplitContainer();
		this.splitContainer3 = new System.Windows.Forms.SplitContainer();
		this.dataGridView2 = new System.Windows.Forms.DataGridView();
		this.dataGridView3 = new System.Windows.Forms.DataGridView();
		this.dataGridView1 = new System.Windows.Forms.DataGridView();
		this.chartPlots = new System.Windows.Forms.DataVisualization.Charting.Chart();
		this.btnExplorer2 = new System.Windows.Forms.Button();
		this.btnOpenChrom3 = new System.Windows.Forms.Button();
		this.btnOpenChrom2 = new System.Windows.Forms.Button();
		this.btnOpenChrom = new System.Windows.Forms.Button();
		this.btnExplorer = new System.Windows.Forms.Button();
		this.label2 = new System.Windows.Forms.Label();
		this.label6 = new System.Windows.Forms.Label();
		this.label1 = new System.Windows.Forms.Label();
		this.label7 = new System.Windows.Forms.Label();
		this.tbLowerLimit = new System.Windows.Forms.TextBox();
		this.tbUpperLimit = new System.Windows.Forms.TextBox();
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
		this.splitContainer1.Panel2.Controls.Add(this.btnExplorer2);
		this.splitContainer1.Panel2.Controls.Add(this.btnOpenChrom3);
		this.splitContainer1.Panel2.Controls.Add(this.btnOpenChrom2);
		this.splitContainer1.Panel2.Controls.Add(this.btnOpenChrom);
		this.splitContainer1.Panel2.Controls.Add(this.btnExplorer);
		this.splitContainer1.Panel2.Controls.Add(this.label2);
		this.splitContainer1.Panel2.Controls.Add(this.label6);
		this.splitContainer1.Panel2.Controls.Add(this.label1);
		this.splitContainer1.Panel2.Controls.Add(this.label7);
		this.splitContainer1.Panel2.Controls.Add(this.tbLowerLimit);
		this.splitContainer1.Panel2.Controls.Add(this.tbUpperLimit);
		this.splitContainer1.Panel2.Controls.Add(this.dateTimePicker2);
		this.splitContainer1.Panel2.Controls.Add(this.dateTimePicker1);
		this.splitContainer1.Panel2.Controls.Add(this.groupBox1);
		this.splitContainer1.Size = new System.Drawing.Size(1113, 670);
		this.splitContainer1.SplitterDistance = 885;
		this.splitContainer1.TabIndex = 0;
		this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
		this.splitContainer2.Location = new System.Drawing.Point(0, 0);
		this.splitContainer2.Name = "splitContainer2";
		this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
		this.splitContainer2.Panel1.Controls.Add(this.splitContainer3);
		this.splitContainer2.Panel1.Controls.Add(this.dataGridView1);
		this.splitContainer2.Panel2.Controls.Add(this.chartPlots);
		this.splitContainer2.Size = new System.Drawing.Size(885, 670);
		this.splitContainer2.SplitterDistance = 450;
		this.splitContainer2.TabIndex = 0;
		this.splitContainer3.Location = new System.Drawing.Point(325, 0);
		this.splitContainer3.Name = "splitContainer3";
		this.splitContainer3.Panel1.Controls.Add(this.dataGridView2);
		this.splitContainer3.Panel2.Controls.Add(this.dataGridView3);
		this.splitContainer3.Size = new System.Drawing.Size(557, 300);
		this.splitContainer3.SplitterDistance = 210;
		this.splitContainer3.TabIndex = 4;
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
		this.dataGridView1.Location = new System.Drawing.Point(3, 3);
		this.dataGridView1.Name = "dataGridView1";
		this.dataGridView1.RowTemplate.Height = 23;
		this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
		this.dataGridView1.Size = new System.Drawing.Size(282, 289);
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
		this.chartPlots.Size = new System.Drawing.Size(885, 216);
		this.chartPlots.TabIndex = 1;
		this.chartPlots.Text = "chart1";
		this.btnExplorer2.Location = new System.Drawing.Point(7, 414);
		this.btnExplorer2.Name = "btnExplorer2";
		this.btnExplorer2.Size = new System.Drawing.Size(192, 38);
		this.btnExplorer2.TabIndex = 33;
		this.btnExplorer2.Text = "苯系物数据导出";
		this.btnExplorer2.UseVisualStyleBackColor = true;
		this.btnExplorer2.Click += new System.EventHandler(btnExplorer2_Click);
		this.btnOpenChrom3.Location = new System.Drawing.Point(192, 627);
		this.btnOpenChrom3.Name = "btnOpenChrom3";
		this.btnOpenChrom3.Size = new System.Drawing.Size(192, 38);
		this.btnOpenChrom3.TabIndex = 32;
		this.btnOpenChrom3.Text = "打开TCD谱图";
		this.btnOpenChrom3.UseVisualStyleBackColor = true;
		this.btnOpenChrom3.Visible = false;
		this.btnOpenChrom3.Click += new System.EventHandler(btnOpenChrom3_Click);
		this.btnOpenChrom2.Location = new System.Drawing.Point(7, 506);
		this.btnOpenChrom2.Name = "btnOpenChrom2";
		this.btnOpenChrom2.Size = new System.Drawing.Size(192, 38);
		this.btnOpenChrom2.TabIndex = 31;
		this.btnOpenChrom2.Text = "打开FID2谱图";
		this.btnOpenChrom2.UseVisualStyleBackColor = true;
		this.btnOpenChrom2.Click += new System.EventHandler(btnOpenChrom2_Click);
		this.btnOpenChrom.Location = new System.Drawing.Point(7, 460);
		this.btnOpenChrom.Name = "btnOpenChrom";
		this.btnOpenChrom.Size = new System.Drawing.Size(192, 38);
		this.btnOpenChrom.TabIndex = 30;
		this.btnOpenChrom.Text = "打开FID1谱图";
		this.btnOpenChrom.UseVisualStyleBackColor = true;
		this.btnOpenChrom.Click += new System.EventHandler(btnOpenChrom_Click);
		this.btnExplorer.Location = new System.Drawing.Point(7, 368);
		this.btnExplorer.Name = "btnExplorer";
		this.btnExplorer.Size = new System.Drawing.Size(192, 38);
		this.btnExplorer.TabIndex = 29;
		this.btnExplorer.Text = "数据导出";
		this.btnExplorer.UseVisualStyleBackColor = true;
		this.btnExplorer.Click += new System.EventHandler(btnExplorer_Click);
		this.label2.AutoSize = true;
		this.label2.Location = new System.Drawing.Point(17, 643);
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
		this.label1.Location = new System.Drawing.Point(17, 605);
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
		this.tbLowerLimit.Location = new System.Drawing.Point(79, 637);
		this.tbLowerLimit.Name = "tbLowerLimit";
		this.tbLowerLimit.Size = new System.Drawing.Size(107, 21);
		this.tbLowerLimit.TabIndex = 9;
		this.tbLowerLimit.Text = "99.8";
		this.tbLowerLimit.TextChanged += new System.EventHandler(tbLowerLimit_TextChanged);
		this.tbUpperLimit.Location = new System.Drawing.Point(79, 599);
		this.tbUpperLimit.Name = "tbUpperLimit";
		this.tbUpperLimit.Size = new System.Drawing.Size(107, 21);
		this.tbUpperLimit.TabIndex = 8;
		this.tbUpperLimit.Text = "100";
		this.tbUpperLimit.TextChanged += new System.EventHandler(tbUpperLimit_TextChanged);
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
		this.groupBox1.Location = new System.Drawing.Point(7, 66);
		this.groupBox1.Name = "groupBox1";
		this.groupBox1.Size = new System.Drawing.Size(192, 274);
		this.groupBox1.TabIndex = 24;
		this.groupBox1.TabStop = false;
		this.groupBox1.Text = "显示组份";
		this.ch11.AutoSize = true;
		this.ch11.Location = new System.Drawing.Point(17, 240);
		this.ch11.Name = "ch11";
		this.ch11.Size = new System.Drawing.Size(60, 16);
		this.ch11.TabIndex = 11;
		this.ch11.Text = "苯系物";
		this.ch11.UseVisualStyleBackColor = true;
		this.ch10.AutoSize = true;
		this.ch10.Location = new System.Drawing.Point(17, 218);
		this.ch10.Name = "ch10";
		this.ch10.Size = new System.Drawing.Size(60, 16);
		this.ch10.TabIndex = 10;
		this.ch10.Text = "苯乙烯";
		this.ch10.UseVisualStyleBackColor = true;
		this.btnSaveLoadData.Location = new System.Drawing.Point(95, 245);
		this.btnSaveLoadData.Name = "btnSaveLoadData";
		this.btnSaveLoadData.Size = new System.Drawing.Size(75, 23);
		this.btnSaveLoadData.TabIndex = 9;
		this.btnSaveLoadData.Text = "确定";
		this.btnSaveLoadData.UseVisualStyleBackColor = true;
		this.btnSaveLoadData.Click += new System.EventHandler(btnSaveLoadData_Click);
		this.ch9.AutoSize = true;
		this.ch9.Location = new System.Drawing.Point(17, 196);
		this.ch9.Name = "ch9";
		this.ch9.Size = new System.Drawing.Size(60, 16);
		this.ch9.TabIndex = 8;
		this.ch9.Text = "异丙苯";
		this.ch9.UseVisualStyleBackColor = true;
		this.ch8.AutoSize = true;
		this.ch8.Location = new System.Drawing.Point(17, 174);
		this.ch8.Name = "ch8";
		this.ch8.Size = new System.Drawing.Size(48, 16);
		this.ch8.TabIndex = 7;
		this.ch8.Text = "乙苯";
		this.ch8.UseVisualStyleBackColor = true;
		this.ch7.AutoSize = true;
		this.ch7.Location = new System.Drawing.Point(17, 152);
		this.ch7.Name = "ch7";
		this.ch7.Size = new System.Drawing.Size(72, 16);
		this.ch7.TabIndex = 6;
		this.ch7.Text = "邻二甲苯";
		this.ch7.UseVisualStyleBackColor = true;
		this.ch6.AutoSize = true;
		this.ch6.Location = new System.Drawing.Point(17, 130);
		this.ch6.Name = "ch6";
		this.ch6.Size = new System.Drawing.Size(84, 16);
		this.ch6.TabIndex = 5;
		this.ch6.Text = "间对二甲苯";
		this.ch6.UseVisualStyleBackColor = true;
		this.ch5.AutoSize = true;
		this.ch5.Location = new System.Drawing.Point(17, 108);
		this.ch5.Name = "ch5";
		this.ch5.Size = new System.Drawing.Size(48, 16);
		this.ch5.TabIndex = 4;
		this.ch5.Text = "甲苯";
		this.ch5.UseVisualStyleBackColor = true;
		this.ch4.AutoSize = true;
		this.ch4.Location = new System.Drawing.Point(17, 86);
		this.ch4.Name = "ch4";
		this.ch4.Size = new System.Drawing.Size(36, 16);
		this.ch4.TabIndex = 3;
		this.ch4.Text = "苯";
		this.ch4.UseVisualStyleBackColor = true;
		this.ch3.AutoSize = true;
		this.ch3.Location = new System.Drawing.Point(17, 64);
		this.ch3.Name = "ch3";
		this.ch3.Size = new System.Drawing.Size(84, 16);
		this.ch3.TabIndex = 2;
		this.ch3.Text = "非甲烷总烃";
		this.ch3.UseVisualStyleBackColor = true;
		this.ch2.AutoSize = true;
		this.ch2.Location = new System.Drawing.Point(17, 42);
		this.ch2.Name = "ch2";
		this.ch2.Size = new System.Drawing.Size(48, 16);
		this.ch2.TabIndex = 1;
		this.ch2.Text = "甲烷";
		this.ch2.UseVisualStyleBackColor = true;
		this.ch1.AutoSize = true;
		this.ch1.Location = new System.Drawing.Point(17, 20);
		this.ch1.Name = "ch1";
		this.ch1.Size = new System.Drawing.Size(48, 16);
		this.ch1.TabIndex = 0;
		this.ch1.Text = "总烃";
		this.ch1.UseVisualStyleBackColor = true;
		this.contextMenuStrip1.Name = "contextMenuStrip1";
		this.contextMenuStrip1.Size = new System.Drawing.Size(61, 4);
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(1113, 670);
		base.Controls.Add(this.splitContainer1);
		base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
		base.Name = "FormHistory";
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
