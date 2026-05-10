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

public class FormCoalHistory : Form
{
	public static FormCoalHistory selfCtrl;

	private DataTable dataSource1 = new DataTable();

	private FormMainParam frmParam = FormMainParam.Create();

	private List<string> strFileName1 = new List<string>();

	private List<string> strSerialName = new List<string>();

	private List<Color> strSerialColor = new List<Color>();

	private List<string> strSerialTime = new List<string>();

	public Series[] serieLines = new Series[12];

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

	private Button btnOpenChrom;

	private Button btnOpenChrom3;

	private Button btnOpenChrom2;

	private CheckBox ch12;

	private CheckBox ch11;

	private CheckBox chBON;

	public FormCoalHistory()
	{
		selfCtrl = this;
		InitializeComponent();
		initForm();
	}

	public void initForm()
	{
		dataGridView1.ReadOnly = true;
		strSerialName.Clear();
		strSerialName.Add("一氧化碳");
		strSerialName.Add("甲烷");
		strSerialName.Add("二氧化碳");
		strSerialName.Add("乙烯");
		strSerialName.Add("乙烷");
		strSerialName.Add("丙烷");
		strSerialName.Add("丙烯");
		strSerialName.Add("乙炔");
		strSerialName.Add("氧气");
		strSerialName.Add("氮气");
		strSerialName.Add("柱箱");
		strSerialName.Add("氧化室");
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
		strSerialColor.Add(Color.DarkBlue);
		strSerialColor.Add(Color.DarkCyan);
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
		ch12.Checked = frmParam.bSum[11];
		tbUpperLimit.Text = frmParam.UpperLimit.ToString();
		tbLowerLimit.Text = frmParam.LowerLimit.ToString();
		dateTimePicker1.Value = frmParam.dataTimeStart;
		dateTimePicker2.Value = frmParam.dataTimeEnd;
		InitChart();
		loading = false;
	}

	public void loadData()
	{
		DataTable dataTableMINE = Class49.GetDataTableMINE(0, "coalTable", dateTimePicker1.Value, dateTimePicker2.Value);
		if (dataTableMINE != null)
		{
			dataGridView1.DataSource = dataTableMINE;
			dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
			relodColumn();
			try
			{
				dataGridView1.Columns.Remove("FileName");
				dataGridView1.Columns.Remove("备用2");
			}
			catch
			{
			}
			loadSeriesDate();
		}
	}

	public void relodColumn()
	{
		try
		{
			if (!frmParam.bSum[0])
			{
				dataGridView1.Columns.Remove("丙烯");
			}
		}
		catch
		{
		}
		try
		{
			if (!frmParam.bSum[1])
			{
				dataGridView1.Columns.Remove("氮气");
			}
		}
		catch
		{
		}
		try
		{
			if (!frmParam.bSum[2])
			{
				dataGridView1.Columns.Remove("甲烷");
			}
		}
		catch
		{
		}
		try
		{
			if (!frmParam.bSum[3])
			{
				dataGridView1.Columns.Remove("乙炔");
			}
		}
		catch
		{
		}
		try
		{
			if (!frmParam.bSum[4])
			{
				dataGridView1.Columns.Remove("氧气");
			}
		}
		catch
		{
		}
		try
		{
			if (!frmParam.bSum[5])
			{
				dataGridView1.Columns.Remove("乙烷");
			}
		}
		catch
		{
		}
		try
		{
			if (!frmParam.bSum[6])
			{
				dataGridView1.Columns.Remove("一氧化碳");
			}
		}
		catch
		{
		}
		try
		{
			if (!frmParam.bSum[7])
			{
				dataGridView1.Columns.Remove("乙烯");
			}
		}
		catch
		{
		}
		try
		{
			if (!frmParam.bSum[8])
			{
				dataGridView1.Columns.Remove("二氧化碳");
			}
		}
		catch
		{
		}
		try
		{
			if (!frmParam.bSum[9])
			{
				dataGridView1.Columns.Remove("丙烷");
			}
		}
		catch
		{
		}
		try
		{
			if (!frmParam.bSum[10])
			{
				dataGridView1.Columns.Remove("柱箱");
			}
		}
		catch
		{
		}
		try
		{
			if (!frmParam.bSum[11])
			{
				dataGridView1.Columns.Remove("氧化室");
			}
		}
		catch
		{
		}
		try
		{
			dataGridView1.Columns.Remove("FID1");
		}
		catch
		{
		}
		try
		{
			dataGridView1.Columns.Remove("FID2");
		}
		catch
		{
		}
		try
		{
			dataGridView1.Columns.Remove("TCD");
		}
		catch
		{
		}
		if (dataGridView1.Columns.Contains("丙烯"))
		{
			dataGridView1.Columns["丙烯"].DefaultCellStyle.Format = "F" + Class49.int_8;
		}
		if (dataGridView1.Columns.Contains("氮气"))
		{
			dataGridView1.Columns["氮气"].DefaultCellStyle.Format = "F" + Class49.int_8;
		}
		if (dataGridView1.Columns.Contains("甲烷"))
		{
			dataGridView1.Columns["甲烷"].DefaultCellStyle.Format = "F" + Class49.int_8;
		}
		if (dataGridView1.Columns.Contains("乙炔"))
		{
			dataGridView1.Columns["乙炔"].DefaultCellStyle.Format = "F" + Class49.int_8;
		}
		if (dataGridView1.Columns.Contains("氧气"))
		{
			dataGridView1.Columns["氧气"].DefaultCellStyle.Format = "F" + Class49.int_8;
		}
		if (dataGridView1.Columns.Contains("乙烷"))
		{
			dataGridView1.Columns["乙烷"].DefaultCellStyle.Format = "F" + Class49.int_8;
		}
		if (dataGridView1.Columns.Contains("一氧化碳"))
		{
			dataGridView1.Columns["一氧化碳"].DefaultCellStyle.Format = "F" + Class49.int_8;
		}
		if (dataGridView1.Columns.Contains("乙烯"))
		{
			dataGridView1.Columns["乙烯"].DefaultCellStyle.Format = "F" + Class49.int_8;
		}
		if (dataGridView1.Columns.Contains("二氧化碳"))
		{
			dataGridView1.Columns["二氧化碳"].DefaultCellStyle.Format = "F" + Class49.int_8;
		}
		if (dataGridView1.Columns.Contains("丙烷"))
		{
			dataGridView1.Columns["丙烷"].DefaultCellStyle.Format = "F" + Class49.int_8;
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
		for (int i = 0; i < strSerialName.Count; i++)
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
		for (int i = 0; i < 12; i++)
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
			if (dataGridView1.Columns["一氧化碳"] != null && dataGridView1.Rows[k].Cells["一氧化碳"].Value != null)
			{
				serieLines[0].Points.AddXY(strSerialTime[k], dataGridView1.Rows[k].Cells["一氧化碳"].Value.ToString());
			}
			if (dataGridView1.Columns["甲烷"] != null && dataGridView1.Rows[k].Cells["甲烷"].Value != null)
			{
				serieLines[1].Points.AddXY(strSerialTime[k], dataGridView1.Rows[k].Cells["甲烷"].Value.ToString());
			}
			if (dataGridView1.Columns["二氧化碳"] != null && dataGridView1.Rows[k].Cells["二氧化碳"].Value != null)
			{
				serieLines[2].Points.AddXY(strSerialTime[k], dataGridView1.Rows[k].Cells["二氧化碳"].Value.ToString());
			}
			if (dataGridView1.Columns["乙烯"] != null && dataGridView1.Rows[k].Cells["乙烯"].Value != null)
			{
				serieLines[3].Points.AddXY(strSerialTime[k], dataGridView1.Rows[k].Cells["乙烯"].Value.ToString());
			}
			if (dataGridView1.Columns["乙烷"] != null && dataGridView1.Rows[k].Cells["乙烷"].Value != null)
			{
				serieLines[4].Points.AddXY(strSerialTime[k], dataGridView1.Rows[k].Cells["乙烷"].Value.ToString());
			}
			if (dataGridView1.Columns["丙烷"] != null && dataGridView1.Rows[k].Cells["丙烷"].Value != null)
			{
				serieLines[5].Points.AddXY(strSerialTime[k], dataGridView1.Rows[k].Cells["丙烷"].Value.ToString());
			}
			if (dataGridView1.Columns["丙烯"] != null && dataGridView1.Rows[k].Cells["丙烯"].Value != null)
			{
				serieLines[6].Points.AddXY(strSerialTime[k], dataGridView1.Rows[k].Cells["丙烯"].Value.ToString());
			}
			if (dataGridView1.Columns["乙炔"] != null && dataGridView1.Rows[k].Cells["乙炔"].Value != null)
			{
				serieLines[7].Points.AddXY(strSerialTime[k], dataGridView1.Rows[k].Cells["乙炔"].Value.ToString());
			}
			if (dataGridView1.Columns["氧气"] != null && dataGridView1.Rows[k].Cells["氧气"].Value != null)
			{
				serieLines[8].Points.AddXY(strSerialTime[k], dataGridView1.Rows[k].Cells["氧气"].Value.ToString());
			}
			if (dataGridView1.Columns["氮气"] != null && dataGridView1.Rows[k].Cells["氮气"].Value != null)
			{
				serieLines[9].Points.AddXY(strSerialTime[k], dataGridView1.Rows[k].Cells["氮气"].Value.ToString());
			}
			if (dataGridView1.Columns["柱箱"] != null && dataGridView1.Rows[k].Cells["柱箱"].Value != null)
			{
				serieLines[10].Points.AddXY(strSerialTime[k], dataGridView1.Rows[k].Cells["柱箱"].Value.ToString());
			}
			if (dataGridView1.Columns["氧化室"] != null && dataGridView1.Rows[k].Cells["氧化室"].Value != null)
			{
				serieLines[11].Points.AddXY(strSerialTime[k], dataGridView1.Rows[k].Cells["氧化室"].Value.ToString());
			}
		}
		for (int l = 0; l < 12; l++)
		{
			serieLines[l].IsVisibleInLegend = frmParam.bSum[l];
			if (!frmParam.bSum[l])
			{
				serieLines[l].Points.Clear();
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
		frmParam.bSum[11] = ch12.Checked;
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
		SaveFileDialog saveFileDialog = new SaveFileDialog();
		saveFileDialog.FileName = Application.StartupPath + "\\" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xls";
		saveFileDialog.Filter = " csv files(*.csv)|*.csv|All files(*.*)|*.*";
		saveFileDialog.FilterIndex = 2;
		saveFileDialog.RestoreDirectory = true;
		if (saveFileDialog.ShowDialog() != DialogResult.OK)
		{
			return;
		}
		int num = 0;
		num = dataGridView1.Columns.Count - 1;
		if (num < 1)
		{
			num = 1;
		}
		FileStream fileStream = (chBON.Checked ? new FileStream(Application.StartupPath + "\\coal14.xls", FileMode.Open, FileAccess.Read) : new FileStream(Application.StartupPath + "\\coal12.xls", FileMode.Open, FileAccess.Read));
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
			if (!chBON.Checked)
			{
				if (dataGridView1.Columns[i].Name != "时间" && dataGridView1.Columns[i].Name != "柱箱" && dataGridView1.Columns[i].Name != "氧气" && dataGridView1.Columns[i].Name != "氮气")
				{
					dataGridView.Columns.Add(dataGridView1.Columns[i].Name, dataGridView1.Columns[i].HeaderText);
				}
			}
			else if (dataGridView1.Columns[i].Name != "时间" && dataGridView1.Columns[i].Name != "柱箱")
			{
				dataGridView.Columns.Add(dataGridView1.Columns[i].Name, dataGridView1.Columns[i].HeaderText);
			}
		}
		dataToExcel(dataGridView, saveFileDialog.FileName);
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
			if (dataGridView1.SelectedRows != null && dataGridView1.SelectedRows.Count > 0)
			{
				workbook = new HSSFWorkbook(fileStream2);
				sheet = workbook.GetSheetAt(0);
				int count = dataGridView1.SelectedRows.Count;
				int count2 = dataGridView.Columns.Count;
				int num2 = 0;
				int num3 = 1;
				row2 = sheet.GetRow(0);
				cell2 = row2.GetCell(0);
				row = sheet.GetRow(5);
				for (int i = 1; i < count2; i++)
				{
					cell = row.GetCell(i);
					if (cell == null)
					{
						cell = row.CreateCell(i);
					}
				}
				for (int j = 11; j < dataGridView1.SelectedRows.Count + 11; j++)
				{
					if (j > 4)
					{
					}
					row = sheet.GetRow(j);
					if (row == null)
					{
						row = sheet.CreateRow(j);
					}
					for (int k = 0; k <= count2; k++)
					{
						string text = "";
						try
						{
							text = array.GetValue(k - 1).ToString();
						}
						catch
						{
							continue;
						}
						switch (text)
						{
						case "一氧化碳":
						{
							cell = row.GetCell(1);
							if (cell == null)
							{
								cell = row.CreateCell(1);
							}
							float result7 = 0f;
							float.TryParse(dataGridView1.SelectedRows[count + 10 - j].Cells[text].Value.ToString(), out result7);
							cell.SetCellValue(result7.ToString("F" + Class49.int_8));
							break;
						}
						case "二氧化碳":
						{
							cell = row.GetCell(2);
							float result4 = 0f;
							float.TryParse(dataGridView1.SelectedRows[count + 10 - j].Cells[text].Value.ToString(), out result4);
							cell.SetCellValue(result4.ToString("F" + Class49.int_8));
							break;
						}
						case "甲烷":
						{
							cell = row.GetCell(3);
							float result8 = 0f;
							float.TryParse(dataGridView1.SelectedRows[count + 10 - j].Cells[text].Value.ToString(), out result8);
							cell.SetCellValue(result8.ToString("F" + Class49.int_8));
							break;
						}
						case "乙烷":
						{
							cell = row.GetCell(4);
							float result3 = 0f;
							float.TryParse(dataGridView1.SelectedRows[count + 10 - j].Cells[text].Value.ToString(), out result3);
							cell.SetCellValue(result3.ToString("F" + Class49.int_8));
							break;
						}
						case "乙烯":
						{
							cell = row.GetCell(5);
							if (cell == null)
							{
								cell = row.CreateCell(5);
							}
							float result11 = 0f;
							float.TryParse(dataGridView1.SelectedRows[count + 10 - j].Cells[text].Value.ToString(), out result11);
							cell.SetCellValue(result11.ToString("F" + Class49.int_8));
							break;
						}
						case "丙烷":
						{
							cell = row.GetCell(6);
							if (cell == null)
							{
								cell = row.CreateCell(6);
							}
							float result9 = 0f;
							float.TryParse(dataGridView1.SelectedRows[count + 10 - j].Cells[text].Value.ToString(), out result9);
							cell.SetCellValue(result9.ToString("F" + Class49.int_8));
							break;
						}
						case "丙烯":
						{
							cell = row.GetCell(7);
							if (cell == null)
							{
								cell = row.CreateCell(7);
							}
							float result5 = 0f;
							float.TryParse(dataGridView1.SelectedRows[count + 10 - j].Cells[text].Value.ToString(), out result5);
							cell.SetCellValue(result5.ToString("F" + Class49.int_8));
							break;
						}
						case "乙炔":
						{
							cell = row.GetCell(8);
							if (cell == null)
							{
								cell = row.CreateCell(8);
							}
							float result10 = 0f;
							float.TryParse(dataGridView1.SelectedRows[count + 10 - j].Cells[text].Value.ToString(), out result10);
							cell.SetCellValue(result10.ToString("F" + Class49.int_8));
							break;
						}
						case "氧气":
							if (chBON.Checked)
							{
								cell = row.GetCell(9);
								if (cell == null)
								{
									cell = row.CreateCell(9);
								}
								float result6 = 0f;
								float.TryParse(dataGridView1.SelectedRows[count + 10 - j].Cells[text].Value.ToString(), out result6);
								cell.SetCellValue(result6.ToString("F" + Class49.int_8));
							}
							break;
						case "氮气":
							if (chBON.Checked)
							{
								cell = row.GetCell(10);
								if (cell == null)
								{
									cell = row.CreateCell(10);
								}
								float result2 = 0f;
								float.TryParse(dataGridView1.SelectedRows[count + 10 - j].Cells[text].Value.ToString(), out result2);
								cell.SetCellValue(result2.ToString("F" + Class49.int_8));
							}
							break;
						}
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
		DateTime dateTime = (DateTime)dataGridView1.CurrentRow.Cells["时间"].Value;
		DataTable dataTableRow = Class49.GetDataTableRow(0, "coalTable", dateTimePicker1.Value, dateTime.ToString("yyyy-MM-dd HH:mm:ss"));
		string chromName = dataTableRow.Rows[0]["FID1"].ToString();
		if (ChromForm.form == null)
		{
			ChromForm.form = new ChromForm();
		}
		ChromForm.form.chromDataGrid.strDetec = "FID1";
		ChromForm.form.Show();
		ChromForm.form.OpenChrom(chromName, sampling: true, useCurrent: true);
	}

	private void btnOpenChrom2_Click(object sender, EventArgs e)
	{
		DateTime dateTime = (DateTime)dataGridView1.CurrentRow.Cells["时间"].Value;
		DataTable dataTableRow = Class49.GetDataTableRow(0, "coalTable", dateTimePicker1.Value, dateTime.ToString("yyyy-MM-dd HH:mm:ss"));
		string chromName = dataTableRow.Rows[0]["FID2"].ToString();
		if (ChromForm.form == null)
		{
			ChromForm.form = new ChromForm();
		}
		ChromForm.form.chromDataGrid.strDetec = "FID2";
		ChromForm.form.Show();
		ChromForm.form.OpenChrom(chromName, sampling: true, useCurrent: true);
	}

	private void btnOpenChrom3_Click(object sender, EventArgs e)
	{
		DateTime dateTime = (DateTime)dataGridView1.CurrentRow.Cells["时间"].Value;
		DataTable dataTableRow = Class49.GetDataTableRow(0, "coalTable", dateTimePicker1.Value, dateTime.ToString("yyyy-MM-dd HH:mm:ss"));
		string chromName = dataTableRow.Rows[0]["TCD"].ToString();
		if (ChromForm.form == null)
		{
			ChromForm.form = new ChromForm();
		}
		ChromForm.form.chromDataGrid.strDetec = "TCD";
		ChromForm.form.Show();
		ChromForm.form.OpenChrom(chromName, sampling: true, useCurrent: true);
	}

	private void FormCoalHistory_FormClosed(object sender, FormClosedEventArgs e)
	{
		selfCtrl = null;
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(IBrainChrom2018.FormCoalHistory));
		this.splitContainer1 = new System.Windows.Forms.SplitContainer();
		this.splitContainer2 = new System.Windows.Forms.SplitContainer();
		this.dataGridView1 = new System.Windows.Forms.DataGridView();
		this.chartPlots = new System.Windows.Forms.DataVisualization.Charting.Chart();
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
		this.chBON = new System.Windows.Forms.CheckBox();
		this.ch12 = new System.Windows.Forms.CheckBox();
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
		((System.ComponentModel.ISupportInitialize)this.dataGridView1).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.chartPlots).BeginInit();
		this.groupBox1.SuspendLayout();
		base.SuspendLayout();
		this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
		this.splitContainer1.Location = new System.Drawing.Point(0, 0);
		this.splitContainer1.Name = "splitContainer1";
		this.splitContainer1.Panel1.Controls.Add(this.splitContainer2);
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
		this.splitContainer1.Size = new System.Drawing.Size(1067, 670);
		this.splitContainer1.SplitterDistance = 848;
		this.splitContainer1.TabIndex = 0;
		this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
		this.splitContainer2.Location = new System.Drawing.Point(0, 0);
		this.splitContainer2.Name = "splitContainer2";
		this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
		this.splitContainer2.Panel1.Controls.Add(this.dataGridView1);
		this.splitContainer2.Panel2.Controls.Add(this.chartPlots);
		this.splitContainer2.Size = new System.Drawing.Size(848, 670);
		this.splitContainer2.SplitterDistance = 450;
		this.splitContainer2.TabIndex = 0;
		this.dataGridView1.AllowUserToOrderColumns = true;
		this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
		this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
		this.dataGridView1.Location = new System.Drawing.Point(0, 0);
		this.dataGridView1.Name = "dataGridView1";
		this.dataGridView1.RowTemplate.Height = 23;
		this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
		this.dataGridView1.Size = new System.Drawing.Size(848, 450);
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
		this.chartPlots.Size = new System.Drawing.Size(848, 216);
		this.chartPlots.TabIndex = 1;
		this.chartPlots.Text = "chart1";
		this.btnOpenChrom3.Location = new System.Drawing.Point(7, 506);
		this.btnOpenChrom3.Name = "btnOpenChrom3";
		this.btnOpenChrom3.Size = new System.Drawing.Size(192, 38);
		this.btnOpenChrom3.TabIndex = 34;
		this.btnOpenChrom3.Text = "打开TCD谱图";
		this.btnOpenChrom3.UseVisualStyleBackColor = true;
		this.btnOpenChrom3.Click += new System.EventHandler(btnOpenChrom3_Click);
		this.btnOpenChrom2.Location = new System.Drawing.Point(7, 459);
		this.btnOpenChrom2.Name = "btnOpenChrom2";
		this.btnOpenChrom2.Size = new System.Drawing.Size(192, 38);
		this.btnOpenChrom2.TabIndex = 33;
		this.btnOpenChrom2.Text = "打开FID2谱图";
		this.btnOpenChrom2.UseVisualStyleBackColor = true;
		this.btnOpenChrom2.Click += new System.EventHandler(btnOpenChrom2_Click);
		this.btnOpenChrom.Location = new System.Drawing.Point(7, 412);
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
		this.groupBox1.Controls.Add(this.chBON);
		this.groupBox1.Controls.Add(this.ch12);
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
		this.groupBox1.Size = new System.Drawing.Size(192, 296);
		this.groupBox1.TabIndex = 24;
		this.groupBox1.TabStop = false;
		this.groupBox1.Text = "显示组份";
		this.chBON.AutoSize = true;
		this.chBON.Location = new System.Drawing.Point(17, 274);
		this.chBON.Name = "chBON";
		this.chBON.Size = new System.Drawing.Size(15, 14);
		this.chBON.TabIndex = 13;
		this.chBON.UseVisualStyleBackColor = true;
		this.ch12.AutoSize = true;
		this.ch12.Location = new System.Drawing.Point(17, 251);
		this.ch12.Name = "ch12";
		this.ch12.Size = new System.Drawing.Size(60, 16);
		this.ch12.TabIndex = 12;
		this.ch12.Text = "氧化室";
		this.ch12.UseVisualStyleBackColor = true;
		this.ch11.AutoSize = true;
		this.ch11.Location = new System.Drawing.Point(17, 230);
		this.ch11.Name = "ch11";
		this.ch11.Size = new System.Drawing.Size(48, 16);
		this.ch11.TabIndex = 11;
		this.ch11.Text = "柱箱";
		this.ch11.UseVisualStyleBackColor = true;
		this.ch10.AutoSize = true;
		this.ch10.Location = new System.Drawing.Point(17, 209);
		this.ch10.Name = "ch10";
		this.ch10.Size = new System.Drawing.Size(48, 16);
		this.ch10.TabIndex = 10;
		this.ch10.Text = "丙烷";
		this.ch10.UseVisualStyleBackColor = true;
		this.btnSaveLoadData.Location = new System.Drawing.Point(104, 267);
		this.btnSaveLoadData.Name = "btnSaveLoadData";
		this.btnSaveLoadData.Size = new System.Drawing.Size(75, 23);
		this.btnSaveLoadData.TabIndex = 9;
		this.btnSaveLoadData.Text = "确定";
		this.btnSaveLoadData.UseVisualStyleBackColor = true;
		this.btnSaveLoadData.Click += new System.EventHandler(btnSaveLoadData_Click);
		this.ch9.AutoSize = true;
		this.ch9.Location = new System.Drawing.Point(17, 188);
		this.ch9.Name = "ch9";
		this.ch9.Size = new System.Drawing.Size(72, 16);
		this.ch9.TabIndex = 8;
		this.ch9.Text = "二氧化碳";
		this.ch9.UseVisualStyleBackColor = true;
		this.ch8.AutoSize = true;
		this.ch8.Location = new System.Drawing.Point(17, 167);
		this.ch8.Name = "ch8";
		this.ch8.Size = new System.Drawing.Size(48, 16);
		this.ch8.TabIndex = 7;
		this.ch8.Text = "乙烯";
		this.ch8.UseVisualStyleBackColor = true;
		this.ch7.AutoSize = true;
		this.ch7.Location = new System.Drawing.Point(17, 146);
		this.ch7.Name = "ch7";
		this.ch7.Size = new System.Drawing.Size(72, 16);
		this.ch7.TabIndex = 6;
		this.ch7.Text = "一氧化碳";
		this.ch7.UseVisualStyleBackColor = true;
		this.ch6.AutoSize = true;
		this.ch6.Location = new System.Drawing.Point(17, 125);
		this.ch6.Name = "ch6";
		this.ch6.Size = new System.Drawing.Size(48, 16);
		this.ch6.TabIndex = 5;
		this.ch6.Text = "乙烷";
		this.ch6.UseVisualStyleBackColor = true;
		this.ch5.AutoSize = true;
		this.ch5.Location = new System.Drawing.Point(17, 104);
		this.ch5.Name = "ch5";
		this.ch5.Size = new System.Drawing.Size(48, 16);
		this.ch5.TabIndex = 4;
		this.ch5.Text = "氧气";
		this.ch5.UseVisualStyleBackColor = true;
		this.ch4.AutoSize = true;
		this.ch4.Location = new System.Drawing.Point(17, 83);
		this.ch4.Name = "ch4";
		this.ch4.Size = new System.Drawing.Size(48, 16);
		this.ch4.TabIndex = 3;
		this.ch4.Text = "乙炔";
		this.ch4.UseVisualStyleBackColor = true;
		this.ch3.AutoSize = true;
		this.ch3.Location = new System.Drawing.Point(17, 62);
		this.ch3.Name = "ch3";
		this.ch3.Size = new System.Drawing.Size(48, 16);
		this.ch3.TabIndex = 2;
		this.ch3.Text = "甲烷";
		this.ch3.UseVisualStyleBackColor = true;
		this.ch2.AutoSize = true;
		this.ch2.Location = new System.Drawing.Point(17, 41);
		this.ch2.Name = "ch2";
		this.ch2.Size = new System.Drawing.Size(48, 16);
		this.ch2.TabIndex = 1;
		this.ch2.Text = "氮气";
		this.ch2.UseVisualStyleBackColor = true;
		this.ch1.AutoSize = true;
		this.ch1.Location = new System.Drawing.Point(17, 20);
		this.ch1.Name = "ch1";
		this.ch1.Size = new System.Drawing.Size(48, 16);
		this.ch1.TabIndex = 0;
		this.ch1.Text = "丙烯";
		this.ch1.UseVisualStyleBackColor = true;
		this.contextMenuStrip1.Name = "contextMenuStrip1";
		this.contextMenuStrip1.Size = new System.Drawing.Size(61, 4);
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(1067, 670);
		base.Controls.Add(this.splitContainer1);
		base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
		base.Name = "FormCoalHistory";
		this.Text = "FormHistory";
		base.FormClosed += new System.Windows.Forms.FormClosedEventHandler(FormCoalHistory_FormClosed);
		this.splitContainer1.Panel1.ResumeLayout(false);
		this.splitContainer1.Panel2.ResumeLayout(false);
		this.splitContainer1.Panel2.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.splitContainer1).EndInit();
		this.splitContainer1.ResumeLayout(false);
		this.splitContainer2.Panel1.ResumeLayout(false);
		this.splitContainer2.Panel2.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.splitContainer2).EndInit();
		this.splitContainer2.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.dataGridView1).EndInit();
		((System.ComponentModel.ISupportInitialize)this.chartPlots).EndInit();
		this.groupBox1.ResumeLayout(false);
		this.groupBox1.PerformLayout();
		base.ResumeLayout(false);
	}
}
