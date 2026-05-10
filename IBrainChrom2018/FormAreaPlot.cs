using System;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using IBrainChrom2018.Unit;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;

namespace IBrainChrom2018;

public class FormAreaPlot : Form
{
	private DataRowView drv2;

	private DataRowView drv3;

	public int locationIndex;

	public Series serie2 = new Series();

	private DataPoint SelectDp;

	private AreaPlotParamMgr plotParamMgr = AreaPlotParamMgr.Create();

	private AreaPlotParam plotParam = null;

	private DataTable dataSource1;

	private bool m_bLoading = true;

	public string strPeakName;

	private DataGridView dataGridView1 = new DataGridView();

	private FormMainParam frmParam = FormMainParam.Create();

	private IContainer components = null;

	private Chart chart1;

	private Button BtnOutData;

	private SplitContainer splitContainer1;

	private ContextMenuStrip contextMenuStrip1;

	private ToolStripMenuItem 刷新ToolStripMenuItem;

	private ToolStripMenuItem 编辑ToolStripMenuItem;

	private ToolStripMenuItem 删除ToolStripMenuItem;

	private TextBox tbLowerLimit;

	private TextBox tbUpperLimit;

	private Label label2;

	private Label label1;

	private TextBox tbTowerNumber;

	private Label label3;

	private TextBox tbPeakName;

	private TextBox tbUnitName;

	private Label label5;

	private ComboBox cbUnitName;

	private Label label6;

	private Label label7;

	private DateTimePicker dateTimePicker2;

	private DateTimePicker dateTimePicker1;

	private Button button1;

	public Label label4;

	public ComboBox cbPeakName;

	public Button btnConvert;

	private CheckBox chbSum;

	private Button btnSet;

	public FormAreaPlot(int location)
	{
		InitializeComponent();
		switch (location)
		{
		case 100:
			locationIndex = location;
			plotParam = plotParamMgr.GetAreaPlotParam(locationIndex);
			splitContainer1.Panel1.Controls.Remove(chart1);
			label1.Visible = false;
			label2.Visible = false;
			tbUnitName.Visible = false;
			tbUpperLimit.Visible = false;
			tbLowerLimit.Visible = false;
			btnConvert.Visible = false;
			label4.Visible = false;
			label5.Visible = false;
			cbPeakName.Visible = false;
			cbUnitName.Visible = false;
			break;
		case 101:
			locationIndex = location;
			plotParam = plotParamMgr.GetAreaPlotParam(locationIndex);
			splitContainer1.Panel1.Controls.Remove(chart1);
			label1.Visible = false;
			label2.Visible = false;
			tbUnitName.Visible = false;
			tbUpperLimit.Visible = false;
			tbLowerLimit.Visible = false;
			btnConvert.Visible = false;
			label4.Visible = false;
			label5.Visible = false;
			cbPeakName.Visible = false;
			cbUnitName.Visible = false;
			break;
		default:
			locationIndex = location;
			plotParam = plotParamMgr.GetAreaPlotParam(locationIndex);
			tbUpperLimit.Text = plotParam.UpperLimit.ToString();
			tbLowerLimit.Text = plotParam.LowerLimit.ToString();
			tbTowerNumber.Text = plotParam.TowerNumber.ToString();
			tbPeakName.Text = plotParam.PeakName;
			cbPeakName.Text = plotParam.PeakName;
			cbUnitName.Text = plotParam.UintName;
			tbUnitName.Text = plotParam.UintName;
			CtrlLangPS.Instance.UpdateLanguageForAllControl(this);
			cbPeakName.Text = plotParam.PeakName;
			chbSum.Checked = frmParam.bSum[locationIndex - 1];
			break;
		}
	}

	private void FormAreaPlot_Load(object sender, EventArgs e)
	{
		string text = Application.StartupPath + "\\1.ico";
		if (File.Exists(text))
		{
			base.Icon = new Icon(text);
		}
		InitChart();
		if (locationIndex == 1)
		{
			cbPeakName.Visible = false;
			label4.Visible = false;
		}
		if (locationIndex == 2)
		{
			cbPeakName.Visible = false;
			label4.Visible = false;
		}
		if (locationIndex == 3)
		{
			cbPeakName.Visible = false;
			label4.Visible = false;
			label5.Visible = false;
			cbUnitName.Visible = false;
		}
		if (locationIndex == 20)
		{
			cbPeakName.Visible = false;
			label4.Visible = false;
			label5.Visible = false;
			cbUnitName.Visible = false;
		}
		if (frmParam.kindMachine == 2)
		{
			cbPeakName.Visible = false;
			label4.Visible = false;
		}
		dateTimePicker1.Text = plotParam.dataTimeStart.ToString("yyyy/MM/dd");
		dateTimePicker2.Text = plotParam.dataTimeEnd.ToString("yyyy/MM/dd HH:mm");
		tbUpperLimit.Text = plotParam.UpperLimit.ToString();
		tbLowerLimit.Text = plotParam.LowerLimit.ToString();
		tbTowerNumber.Text = plotParam.TowerNumber.ToString();
		tbPeakName.Text = plotParam.PeakName;
		cbPeakName.Text = plotParam.PeakName;
		cbUnitName.Text = plotParam.UintName;
		tbUnitName.Text = plotParam.UintName;
		LoadLanguage();
		m_bLoading = false;
	}

	public void LoadLanguage()
	{
		Text = Lang.PS("历史数据", "Historical data");
		label1.Text = Lang.PS("含量上限:", "Upper limit");
		label2.Text = Lang.PS("含量下限:", "Lower limit");
		label7.Text = Lang.PS("起始日期", "Start date");
		label6.Text = Lang.PS("结束日期", "End date");
		label4.Text = Lang.PS("组份名称", "Name");
		label5.Text = Lang.PS("单位转换", "Unit conversion");
		button1.Text = Lang.PS("全部删除", "Delete all ");
		BtnOutData.Text = Lang.PS("导出数据", "Export date ");
	}

	private void InitChart()
	{
		chart1.ContextMenuStrip = contextMenuStrip1;
		chart1.Series.Clear();
		serie2.ChartType = SeriesChartType.Line;
		serie2.IsVisibleInLegend = false;
		serie2.IsValueShownAsLabel = false;
		serie2.XValueType = ChartValueType.String;
		serie2.YValueType = ChartValueType.Double;
		serie2.Color = Color.Black;
		serie2.MarkerStyle = MarkerStyle.Circle;
		serie2.MarkerSize = 10;
		chart1.ChartAreas[0].AxisX.MajorGrid.Interval = 0.1;
		chart1.ChartAreas[0].AxisY.MajorGrid.Interval = 0.1;
		chart1.ChartAreas[0].AxisX.MajorGrid.Enabled = false;
		chart1.ChartAreas[0].AxisY.MajorGrid.Enabled = false;
		chart1.ChartAreas[0].AxisX.IsMarginVisible = true;
		chart1.ChartAreas[0].AxisX.Title = "时间";
		chart1.ChartAreas[0].AxisX.TitleForeColor = Color.Crimson;
		chart1.ChartAreas["ChartArea1"].AxisX.LabelStyle.Format = "yyyy/MM/dd/ HH:mm:ss";
		chart1.ChartAreas[0].AxisY.Title = "含量百分比";
		chart1.ChartAreas[0].AxisY.TitleForeColor = Color.Crimson;
		chart1.ChartAreas[0].AxisY.Maximum = float.Parse(tbUpperLimit.Text);
		chart1.ChartAreas[0].AxisY.Minimum = float.Parse(tbLowerLimit.Text);
		chart1.ChartAreas[0].AxisY.LabelStyle.Format = "0.000";
		chart1.ChartAreas[0].CursorX.IsUserEnabled = true;
		chart1.ChartAreas[0].CursorX.IsUserSelectionEnabled = true;
		chart1.ChartAreas[0].AxisX.ScaleView.Zoomable = true;
		chart1.ChartAreas[0].AxisX.ScrollBar.IsPositionedInside = true;
		chart1.Series.Add(serie2);
	}

	public void loadData(int idx)
	{
		switch (idx)
		{
		case 100:
		{
			DataTable dataTableRZHistory = Class49.GetDataTableRZHistory(idx, dateTimePicker1.Value, dateTimePicker2.Value);
			dataGridView1.DataSource = dataTableRZHistory;
			dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
			splitContainer1.Panel1.Controls.Add(dataGridView1);
			dataGridView1.Dock = DockStyle.Fill;
			return;
		}
		case 101:
		{
			DataTable dataTableRLTHistory = Class49.GetDataTableRLTHistory(1, "一氧化碳", dateTimePicker1.Value, dateTimePicker2.Value);
			DataTable dataTableRLTHistory2 = Class49.GetDataTableRLTHistory(2, "六氟化硫", dateTimePicker1.Value, dateTimePicker2.Value);
			DataColumn column = dataTableRLTHistory2.Columns[0];
			dataTableRLTHistory2.Columns.RemoveAt(0);
			dataTableRLTHistory.Columns.Add(column);
			dataTableRLTHistory2 = Class49.GetDataTableRLTHistory(2, "乙炔", dateTimePicker1.Value, dateTimePicker2.Value);
			column = dataTableRLTHistory2.Columns[0];
			dataTableRLTHistory2.Columns.RemoveAt(0);
			dataTableRLTHistory.Columns.Add(column);
			dataTableRLTHistory2 = Class49.GetDataTableRLTHistory(2, "乙烯", dateTimePicker1.Value, dateTimePicker2.Value);
			column = dataTableRLTHistory2.Columns[0];
			dataTableRLTHistory2.Columns.RemoveAt(0);
			dataTableRLTHistory.Columns.Add(column);
			dataTableRLTHistory2 = Class49.GetDataTableRLTHistory(2, "乙烷", dateTimePicker1.Value, dateTimePicker2.Value);
			column = dataTableRLTHistory2.Columns[0];
			dataTableRLTHistory2.Columns.RemoveAt(0);
			dataTableRLTHistory.Columns.Add(column);
			dataTableRLTHistory2 = Class49.GetDataTableRLTHistory(2, "二氧化碳", dateTimePicker1.Value, dateTimePicker2.Value);
			column = dataTableRLTHistory2.Columns[0];
			dataTableRLTHistory2.Columns.RemoveAt(0);
			dataTableRLTHistory.Columns.Add(column);
			DataTable dataTableRLTHistory3 = Class49.GetDataTableRLTHistory(2, "氧气", dateTimePicker1.Value, dateTimePicker2.Value);
			column = dataTableRLTHistory3.Columns[0];
			dataTableRLTHistory3.Columns.RemoveAt(0);
			dataTableRLTHistory.Columns.Add(column);
			dataTableRLTHistory2 = Class49.GetDataTableRLTHistory(2, "氮气", dateTimePicker1.Value, dateTimePicker2.Value);
			column = dataTableRLTHistory2.Columns[0];
			dataTableRLTHistory2.Columns.RemoveAt(0);
			dataTableRLTHistory.Columns.Add(column);
			dataTableRLTHistory2 = Class49.GetDataTableRLTHistory(2, "甲烷", dateTimePicker1.Value, dateTimePicker2.Value);
			column = dataTableRLTHistory2.Columns[0];
			dataTableRLTHistory2.Columns.RemoveAt(0);
			dataTableRLTHistory.Columns.Add(column);
			dataSource1 = dataTableRLTHistory;
			dataGridView1.DataSource = dataTableRLTHistory;
			dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
			splitContainer1.Panel1.Controls.Add(dataGridView1);
			dataGridView1.Dock = DockStyle.Fill;
			return;
		}
		}
		serie2.Points.Clear();
		DataTable dataTableVoc = Class49.GetDataTableVoc(idx, dateTimePicker1.Value, dateTimePicker2.Value);
		if (dataTableVoc == null)
		{
			return;
		}
		DataView defaultView = dataTableVoc.DefaultView;
		foreach (DataRowView item in defaultView)
		{
			DateTime dateTime = (DateTime)item["DateTime"];
			double num = (double)item["AreaPer"];
			serie2.Points.AddXY(dateTime.ToString(), num);
		}
	}

	public void loadData()
	{
		loadData(locationIndex);
	}

	private void chart1_GetToolTipText(object sender, ToolTipEventArgs e)
	{
		if (e.HitTestResult.ChartElementType == ChartElementType.DataPoint)
		{
			int pointIndex = e.HitTestResult.PointIndex;
			DataPoint dataPoint = e.HitTestResult.Series.Points[pointIndex];
			e.Text = $"时间:{dataPoint.AxisLabel};含量:{dataPoint.YValues[0]:F4} ";
		}
	}

	private void button2_Click(object sender, EventArgs e)
	{
	}

	private void chart1_MouseClick(object sender, MouseEventArgs e)
	{
	}

	private void chart1_MouseDoubleClick(object sender, MouseEventArgs e)
	{
		HitTestResult hitTestResult = chart1.HitTest(e.X, e.Y);
		if (hitTestResult.ChartElementType != ChartElementType.DataPoint)
		{
			return;
		}
		int pointIndex = hitTestResult.PointIndex;
		DataPoint dataPoint = hitTestResult.Series.Points[pointIndex];
		double num = dataPoint.YValues[0];
		string text = "123456";
		DataTable dataTableVoc = Class49.GetDataTableVoc(locationIndex, dateTimePicker1.Value, dateTimePicker2.Value);
		if (dataTableVoc == null)
		{
			return;
		}
		DataView defaultView = dataTableVoc.DefaultView;
		foreach (DataRowView item in defaultView)
		{
			string text2 = ((DateTime)item["DateTime"]).ToString();
			if (text2 == dataPoint.AxisLabel)
			{
				text = (string)item["fileName"];
			}
		}
		if (File.Exists(text))
		{
			ChromForm chromForm = new ChromForm();
			chromForm.Show();
			chromForm.OpenChrom(text, sampling: true, useCurrent: true);
		}
		else
		{
			MessageBox.Show("不存在谱图文件" + text);
		}
	}

	private void 刷新ToolStripMenuItem_Click(object sender, EventArgs e)
	{
		loadData();
	}

	private void 编辑ToolStripMenuItem_Click(object sender, EventArgs e)
	{
		string text = "123456";
		DataTable dataTableVoc = Class49.GetDataTableVoc(locationIndex, dateTimePicker1.Value, dateTimePicker2.Value);
		if (dataTableVoc == null)
		{
			return;
		}
		DataView defaultView = dataTableVoc.DefaultView;
		foreach (DataRowView item in defaultView)
		{
			string text2 = ((DateTime)item["DateTime"]).ToString();
			if (text2 == SelectDp.AxisLabel)
			{
				text = (string)item["fileName"];
			}
		}
		if (File.Exists(text))
		{
			ChromForm chromForm = new ChromForm();
			chromForm.Show();
			chromForm.OpenChrom(text, sampling: true, useCurrent: true);
		}
		else
		{
			MessageBox.Show("不存在谱图文件" + text);
		}
	}

	private void 删除ToolStripMenuItem_Click(object sender, EventArgs e)
	{
		if (SelectDp != null)
		{
			DialogResult dialogResult = MessageBox.Show("确定删除点" + SelectDp.AxisLabel + ":" + SelectDp.YValues[0] + "吗?", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
			if (dialogResult == DialogResult.Yes)
			{
				DateTime dateTime = DateTime.Parse(SelectDp.AxisLabel);
				DateTime startTime = dateTime.Subtract(new TimeSpan(1000L));
				DateTime endtTime = dateTime.AddSeconds(1.0);
				bool flag = serie2.Points.Remove(SelectDp);
				int num = Class49.DeleteDataTableVoc(locationIndex, startTime, endtTime);
				loadData();
			}
		}
	}

	private void chart1_MouseDown(object sender, MouseEventArgs e)
	{
		if (e.Button == MouseButtons.Right)
		{
			HitTestResult hitTestResult = chart1.HitTest(e.X, e.Y);
			if (hitTestResult.ChartElementType == ChartElementType.DataPoint)
			{
				int pointIndex = hitTestResult.PointIndex;
				SelectDp = hitTestResult.Series.Points[pointIndex];
			}
		}
	}

	public bool dataToExcel(string Outpath)
	{
		DataTable dataTableVoc = Class49.GetDataTableVoc(1, dateTimePicker1.Value, dateTimePicker2.Value);
		DataTable dataTableVoc2 = Class49.GetDataTableVoc(2, dateTimePicker1.Value, dateTimePicker2.Value);
		DataTable dataTableVoc3 = Class49.GetDataTableVoc(3, dateTimePicker1.Value, dateTimePicker2.Value);
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
			string[] array = new string[3];
			if (dataTableVoc != null && dataTableVoc.Rows.Count > 0)
			{
				workbook = new HSSFWorkbook(fileStream2);
				sheet = workbook.GetSheetAt(0);
				int count = dataTableVoc.Rows.Count;
				int num2 = 3;
				row2 = sheet.GetRow(2);
				cell2 = row2.GetCell(3);
				cell2.SetCellValue("分析结果(" + plotParam.UintName.ToString() + ")");
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
				int num3 = 4;
				DataView defaultView = dataTableVoc.DefaultView;
				DataView defaultView2 = dataTableVoc2.DefaultView;
				DataView defaultView3 = dataTableVoc3.DefaultView;
				for (int j = 0; j < defaultView.Count; j++)
				{
					DataRowView dataRowView = defaultView[j];
					if (defaultView2.Count > j)
					{
						drv2 = defaultView2[j];
					}
					if (defaultView3.Count > j)
					{
						drv3 = defaultView3[j];
					}
					DateTime dateTime = (DateTime)dataRowView["DateTime"];
					double num4 = (double)dataRowView["AreaPer"];
					double num5 = 0.0;
					double num6 = 0.0;
					num5 = ((drv2 == null) ? 0.0 : ((double)drv2["AreaPer"]));
					num6 = ((drv3 == null) ? 0.0 : ((double)drv3["AreaPer"]));
					if (plotParam.UintName == "mg/m³")
					{
						num4 = num4 * 16.0 / 22.399999618530273 / 16.0 * 12.0;
						num5 = num5 * 16.0 / 22.399999618530273 / 16.0 * 12.0;
						num6 = num6 * 16.0 / 22.399999618530273 / 16.0 * 12.0;
					}
					string cellValue = (string)dataRowView["ComponentName"];
					if (num3 > 4)
					{
						IRow row3 = sheet.GetRow(5);
						MyInsertRow(sheet, num3 + 1, 1, row3);
					}
					row = sheet.GetRow(num3);
					if (row == null)
					{
						row = sheet.CreateRow(num3);
					}
					for (int k = 0; k <= 5; k++)
					{
						cell = row.GetCell(k);
						if (k == 0)
						{
							cell.SetCellValue((num3 - 3).ToString());
							continue;
						}
						if (cell == null)
						{
							cell = row.CreateCell(k);
						}
						switch (k)
						{
						case 1:
							cell.SetCellValue(dateTime.ToString());
							break;
						case 2:
							cell.SetCellValue(cellValue);
							break;
						case 3:
							cell.SetCellValue(num4.ToString("0.00000"));
							break;
						case 4:
							cell.SetCellValue(num5.ToString("0.00000"));
							break;
						case 5:
							cell.SetCellValue(Math.Abs(num4 - num5).ToString("0.00000"));
							break;
						}
					}
					num3++;
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

	private void BtnOutData_Click(object sender, EventArgs e)
	{
		SaveFileDialog saveFileDialog = new SaveFileDialog();
		int num = serie2.Points.Count();
		saveFileDialog.FileName = "含量表" + DateTime.Now.ToString("yyyyMMddHHmm") + ".xls";
		saveFileDialog.Filter = " csv files(*.csv)|*.csv|All files(*.*)|*.*";
		saveFileDialog.FilterIndex = 2;
		saveFileDialog.RestoreDirectory = true;
		if (saveFileDialog.ShowDialog() != DialogResult.OK)
		{
			return;
		}
		if (locationIndex == 100)
		{
			DataTable dt = (DataTable)dataGridView1.DataSource;
			SaveCSV(dt, saveFileDialog.FileName.ToString());
		}
		else if (locationIndex == 101)
		{
			string text = saveFileDialog.FileName.Replace(".csv", ".xls");
			FileStream fileStream = new FileStream(Application.StartupPath + "\\矿井数据输出.xls", FileMode.Open, FileAccess.Read);
			HSSFWorkbook hSSFWorkbook = new HSSFWorkbook(fileStream);
			byte[] array = File.ReadAllBytes(Application.StartupPath + "\\a1.Emf");
			ISheet sheetAt = hSSFWorkbook.GetSheetAt(0);
			sheetAt.ForceFormulaRecalculation = true;
			FileStream fileStream2 = new FileStream(text, FileMode.Create);
			hSSFWorkbook.Write(fileStream2);
			fileStream.Close();
			fileStream2.Close();
			DataTableToExcel(dataSource1, text);
			Process.Start(text);
		}
		else
		{
			string path = saveFileDialog.FileName.ToString();
			FileStream fileStream3 = new FileStream(path, FileMode.OpenOrCreate);
			StreamWriter streamWriter = new StreamWriter(fileStream3, Encoding.Default);
			for (int i = 0; i < num; i++)
			{
				streamWriter.Write(serie2.Points[i].AxisLabel + "," + serie2.Points[i].YValues[0].ToString("0.000000") + "\r\n");
			}
			streamWriter.Close();
			fileStream3.Close();
		}
	}

	public static bool DataTableToExcel(DataTable dt, string Outpath)
	{
		bool result = false;
		IWorkbook workbook = null;
		FileStream fileStream = null;
		IRow row = null;
		ISheet sheet = null;
		ICell cell = null;
		bool flag = false;
		double result2 = 0.0;
		FileStream fileStream2 = new FileStream(Outpath, FileMode.Open, FileAccess.ReadWrite);
		try
		{
			if (dt != null && dt.Rows.Count > 0)
			{
				workbook = new HSSFWorkbook(fileStream2);
				sheet = workbook.GetSheetAt(0);
				int count = dt.Rows.Count;
				int count2 = dt.Columns.Count;
				row = sheet.GetRow(5);
				for (int i = 1; i < count2; i++)
				{
					cell = row.GetCell(i);
					if (cell == null)
					{
						cell = row.CreateCell(i);
					}
				}
				for (int j = 4; j < count + 4; j++)
				{
					if (j > 4)
					{
						IRow row2 = sheet.GetRow(5);
						MyInsertRow(sheet, j + 1, 1, row2);
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
						if (cell == null)
						{
							cell = row.CreateCell(k);
						}
						switch (k)
						{
						case 1:
							cell.SetCellValue(dt.Rows[j - 4][k - 1].ToString());
							break;
						case 2:
							cell.SetCellValue(dt.Rows[j - 4][k - 1].ToString());
							break;
						default:
							double.TryParse(dt.Rows[j - 4][k - 1].ToString(), out result2);
							cell.SetCellValue(result2.ToString("0.00000"));
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
			fileStream?.Close();
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

	private void button1_Click_1(object sender, EventArgs e)
	{
	}

	private void tbUpperLimit_TextChanged(object sender, EventArgs e)
	{
		float.TryParse(tbUpperLimit.Text, out plotParam.UpperLimit);
		if ((double)plotParam.UpperLimit > chart1.ChartAreas[0].AxisY.Minimum)
		{
			chart1.ChartAreas[0].AxisY.Maximum = plotParam.UpperLimit;
		}
		plotParam.SaveParam();
	}

	private void tbLowerLimit_TextChanged(object sender, EventArgs e)
	{
		float.TryParse(tbLowerLimit.Text, out plotParam.LowerLimit);
		if ((double)plotParam.LowerLimit < chart1.ChartAreas[0].AxisY.Maximum)
		{
			chart1.ChartAreas[0].AxisY.Minimum = plotParam.LowerLimit;
		}
		plotParam.SaveParam();
	}

	private void tbTowerNumber_TextChanged(object sender, EventArgs e)
	{
		float.TryParse(tbTowerNumber.Text, out plotParam.TowerNumber);
		plotParam.SaveParam();
	}

	private void tbPeakName_TextChanged(object sender, EventArgs e)
	{
		plotParam.PeakName = tbPeakName.Text;
		plotParam.SaveParam();
	}

	private void tbUnitName_TextChanged(object sender, EventArgs e)
	{
		plotParam.UintName = tbUnitName.Text;
		plotParam.SaveParam();
	}

	private void cbPeakName_TextChanged(object sender, EventArgs e)
	{
		plotParam.PeakName = cbPeakName.Text;
		plotParam.SaveParam();
	}

	private void cbUnitName_TextChanged(object sender, EventArgs e)
	{
		plotParam.UintName = cbUnitName.Text;
		plotParam.SaveParam();
	}

	private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
	{
		if (!m_bLoading)
		{
			plotParam.dataTimeStart = dateTimePicker1.Value;
			plotParam.SaveParam();
			loadData();
		}
	}

	private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
	{
		if (!m_bLoading)
		{
			plotParam.dataTimeEnd = dateTimePicker2.Value;
			plotParam.SaveParam();
			loadData();
		}
	}

	private void button1_Click(object sender, EventArgs e)
	{
		if (locationIndex == 100)
		{
			Class49.DeleteDataTableRZHistory(locationIndex, dateTimePicker1.Value, dateTimePicker2.Value);
			loadData();
		}
		else
		{
			serie2.Points.Clear();
			Class49.DeleteDataTableVoc(locationIndex, dateTimePicker1.Value, dateTimePicker2.Value);
		}
	}

	private void cbPeakName_SelectedIndexChanged(object sender, EventArgs e)
	{
		plotParam.PeakName = cbPeakName.Text;
		plotParam.SaveParam();
	}

	private void cbUnitName_SelectedIndexChanged(object sender, EventArgs e)
	{
		plotParam.UintName = cbUnitName.Text;
		plotParam.SaveParam();
	}

	private void BtnConvert_Click(object sender, EventArgs e)
	{
		FormAllHydr formAllHydr = new FormAllHydr();
		formAllHydr.Show();
	}

	public static void SaveCSV(DataTable dt, string fullPath)
	{
		FileInfo fileInfo = new FileInfo(fullPath);
		if (!fileInfo.Directory.Exists)
		{
			fileInfo.Directory.Create();
		}
		FileStream fileStream = new FileStream(fullPath, FileMode.Create, FileAccess.Write);
		StreamWriter streamWriter = new StreamWriter(fileStream, Encoding.UTF8);
		string text = "";
		for (int i = 0; i < dt.Columns.Count; i++)
		{
			text += dt.Columns[i].ColumnName.ToString();
			if (i < dt.Columns.Count - 1)
			{
				text += ",";
			}
		}
		streamWriter.WriteLine(text);
		for (int j = 0; j < dt.Rows.Count; j++)
		{
			text = "";
			for (int k = 0; k < dt.Columns.Count; k++)
			{
				string text2 = dt.Rows[j][k].ToString();
				text2 = text2.Replace("\"", "\"\"");
				if (text2.Contains(',') || text2.Contains('"') || text2.Contains('\r') || text2.Contains('\n'))
				{
					text2 = $"\"{text2}\"";
				}
				text += text2;
				if (k < dt.Columns.Count - 1)
				{
					text += ",";
				}
			}
			streamWriter.WriteLine(text);
		}
		streamWriter.Close();
		fileStream.Close();
	}

	private void chbSum_CheckedChanged(object sender, EventArgs e)
	{
		frmParam.bSum[locationIndex - 1] = chbSum.Checked;
		frmParam.SaveParam();
	}

	private void cbUnitName_Click(object sender, EventArgs e)
	{
	}

	private void btnSet_Click(object sender, EventArgs e)
	{
		if (LYTHCtrl2.selfCtrl != null)
		{
			LYTHCtrl2.selfCtrl.changeUnit(cbUnitName.Text);
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(IBrainChrom2018.FormAreaPlot));
		this.chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
		this.BtnOutData = new System.Windows.Forms.Button();
		this.splitContainer1 = new System.Windows.Forms.SplitContainer();
		this.chbSum = new System.Windows.Forms.CheckBox();
		this.btnConvert = new System.Windows.Forms.Button();
		this.button1 = new System.Windows.Forms.Button();
		this.label6 = new System.Windows.Forms.Label();
		this.label7 = new System.Windows.Forms.Label();
		this.dateTimePicker2 = new System.Windows.Forms.DateTimePicker();
		this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
		this.cbUnitName = new System.Windows.Forms.ComboBox();
		this.cbPeakName = new System.Windows.Forms.ComboBox();
		this.tbUnitName = new System.Windows.Forms.TextBox();
		this.label5 = new System.Windows.Forms.Label();
		this.tbPeakName = new System.Windows.Forms.TextBox();
		this.label4 = new System.Windows.Forms.Label();
		this.tbTowerNumber = new System.Windows.Forms.TextBox();
		this.label3 = new System.Windows.Forms.Label();
		this.label2 = new System.Windows.Forms.Label();
		this.label1 = new System.Windows.Forms.Label();
		this.tbLowerLimit = new System.Windows.Forms.TextBox();
		this.tbUpperLimit = new System.Windows.Forms.TextBox();
		this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
		this.刷新ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.编辑ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.删除ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.btnSet = new System.Windows.Forms.Button();
		((System.ComponentModel.ISupportInitialize)this.chart1).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.splitContainer1).BeginInit();
		this.splitContainer1.Panel1.SuspendLayout();
		this.splitContainer1.Panel2.SuspendLayout();
		this.splitContainer1.SuspendLayout();
		this.contextMenuStrip1.SuspendLayout();
		base.SuspendLayout();
		chartArea.Name = "ChartArea1";
		this.chart1.ChartAreas.Add(chartArea);
		this.chart1.Dock = System.Windows.Forms.DockStyle.Fill;
		legend.Name = "Legend1";
		this.chart1.Legends.Add(legend);
		this.chart1.Location = new System.Drawing.Point(0, 0);
		this.chart1.Name = "chart1";
		series.ChartArea = "ChartArea1";
		series.Legend = "Legend1";
		series.Name = "Series1";
		this.chart1.Series.Add(series);
		this.chart1.Size = new System.Drawing.Size(387, 361);
		this.chart1.TabIndex = 0;
		this.chart1.Text = "chart1";
		this.chart1.GetToolTipText += new System.EventHandler<System.Windows.Forms.DataVisualization.Charting.ToolTipEventArgs>(chart1_GetToolTipText);
		this.chart1.MouseClick += new System.Windows.Forms.MouseEventHandler(chart1_MouseClick);
		this.chart1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(chart1_MouseDoubleClick);
		this.chart1.MouseDown += new System.Windows.Forms.MouseEventHandler(chart1_MouseDown);
		this.BtnOutData.Location = new System.Drawing.Point(17, 291);
		this.BtnOutData.Name = "BtnOutData";
		this.BtnOutData.Size = new System.Drawing.Size(75, 23);
		this.BtnOutData.TabIndex = 3;
		this.BtnOutData.Text = "导出数据";
		this.BtnOutData.UseVisualStyleBackColor = true;
		this.BtnOutData.Click += new System.EventHandler(BtnOutData_Click);
		this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
		this.splitContainer1.Location = new System.Drawing.Point(0, 0);
		this.splitContainer1.Name = "splitContainer1";
		this.splitContainer1.Panel1.Controls.Add(this.chart1);
		this.splitContainer1.Panel2.Controls.Add(this.btnSet);
		this.splitContainer1.Panel2.Controls.Add(this.chbSum);
		this.splitContainer1.Panel2.Controls.Add(this.btnConvert);
		this.splitContainer1.Panel2.Controls.Add(this.button1);
		this.splitContainer1.Panel2.Controls.Add(this.label6);
		this.splitContainer1.Panel2.Controls.Add(this.label7);
		this.splitContainer1.Panel2.Controls.Add(this.dateTimePicker2);
		this.splitContainer1.Panel2.Controls.Add(this.dateTimePicker1);
		this.splitContainer1.Panel2.Controls.Add(this.cbUnitName);
		this.splitContainer1.Panel2.Controls.Add(this.cbPeakName);
		this.splitContainer1.Panel2.Controls.Add(this.tbUnitName);
		this.splitContainer1.Panel2.Controls.Add(this.label5);
		this.splitContainer1.Panel2.Controls.Add(this.tbPeakName);
		this.splitContainer1.Panel2.Controls.Add(this.label4);
		this.splitContainer1.Panel2.Controls.Add(this.tbTowerNumber);
		this.splitContainer1.Panel2.Controls.Add(this.label3);
		this.splitContainer1.Panel2.Controls.Add(this.label2);
		this.splitContainer1.Panel2.Controls.Add(this.label1);
		this.splitContainer1.Panel2.Controls.Add(this.tbLowerLimit);
		this.splitContainer1.Panel2.Controls.Add(this.tbUpperLimit);
		this.splitContainer1.Panel2.Controls.Add(this.BtnOutData);
		this.splitContainer1.Size = new System.Drawing.Size(584, 361);
		this.splitContainer1.SplitterDistance = 387;
		this.splitContainer1.TabIndex = 4;
		this.chbSum.AutoSize = true;
		this.chbSum.Location = new System.Drawing.Point(17, 240);
		this.chbSum.Name = "chbSum";
		this.chbSum.Size = new System.Drawing.Size(72, 16);
		this.chbSum.TabIndex = 23;
		this.chbSum.Text = "参与加和";
		this.chbSum.UseVisualStyleBackColor = true;
		this.chbSum.CheckedChanged += new System.EventHandler(chbSum_CheckedChanged);
		this.btnConvert.Location = new System.Drawing.Point(108, 291);
		this.btnConvert.Name = "btnConvert";
		this.btnConvert.Size = new System.Drawing.Size(75, 23);
		this.btnConvert.TabIndex = 22;
		this.btnConvert.Text = "单位转换";
		this.btnConvert.UseVisualStyleBackColor = true;
		this.btnConvert.Click += new System.EventHandler(BtnConvert_Click);
		this.button1.Location = new System.Drawing.Point(17, 262);
		this.button1.Name = "button1";
		this.button1.Size = new System.Drawing.Size(75, 23);
		this.button1.TabIndex = 21;
		this.button1.Text = "全部删除";
		this.button1.UseVisualStyleBackColor = true;
		this.button1.Click += new System.EventHandler(button1_Click);
		this.label6.AutoSize = true;
		this.label6.Location = new System.Drawing.Point(15, 139);
		this.label6.Name = "label6";
		this.label6.Size = new System.Drawing.Size(65, 12);
		this.label6.TabIndex = 20;
		this.label6.Text = "结束日期：";
		this.label7.AutoSize = true;
		this.label7.Location = new System.Drawing.Point(15, 114);
		this.label7.Name = "label7";
		this.label7.Size = new System.Drawing.Size(65, 12);
		this.label7.TabIndex = 19;
		this.label7.Text = "起始日期：";
		this.dateTimePicker2.Checked = false;
		this.dateTimePicker2.CustomFormat = "yyyy/MM/dd HH:mm";
		this.dateTimePicker2.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
		this.dateTimePicker2.Location = new System.Drawing.Point(77, 136);
		this.dateTimePicker2.Name = "dateTimePicker2";
		this.dateTimePicker2.Size = new System.Drawing.Size(107, 21);
		this.dateTimePicker2.TabIndex = 18;
		this.dateTimePicker2.ValueChanged += new System.EventHandler(dateTimePicker2_ValueChanged);
		this.dateTimePicker1.Checked = false;
		this.dateTimePicker1.CustomFormat = "yyyy/MM/dd HH:mm";
		this.dateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
		this.dateTimePicker1.Location = new System.Drawing.Point(77, 108);
		this.dateTimePicker1.Name = "dateTimePicker1";
		this.dateTimePicker1.Size = new System.Drawing.Size(107, 21);
		this.dateTimePicker1.TabIndex = 17;
		this.dateTimePicker1.Value = new System.DateTime(2018, 7, 12, 0, 0, 0, 0);
		this.dateTimePicker1.ValueChanged += new System.EventHandler(dateTimePicker1_ValueChanged);
		this.cbUnitName.FormattingEnabled = true;
		this.cbUnitName.Items.AddRange(new object[5] { "ppm", "mg/m³", "ppm转mg/m³", "mg/m³转ppm", "ppb" });
		this.cbUnitName.Location = new System.Drawing.Point(77, 206);
		this.cbUnitName.Name = "cbUnitName";
		this.cbUnitName.Size = new System.Drawing.Size(107, 20);
		this.cbUnitName.TabIndex = 15;
		this.cbUnitName.Text = "ppm";
		this.cbUnitName.SelectedIndexChanged += new System.EventHandler(cbUnitName_SelectedIndexChanged);
		this.cbUnitName.TextChanged += new System.EventHandler(cbUnitName_TextChanged);
		this.cbUnitName.Click += new System.EventHandler(cbUnitName_Click);
		this.cbPeakName.FormattingEnabled = true;
		this.cbPeakName.Items.AddRange(new object[8] { "苯", "甲苯", "间对二甲苯", "邻二甲苯", "乙苯", "异丙苯", "苯乙烯", "备用" });
		this.cbPeakName.Location = new System.Drawing.Point(77, 170);
		this.cbPeakName.Name = "cbPeakName";
		this.cbPeakName.Size = new System.Drawing.Size(107, 20);
		this.cbPeakName.TabIndex = 14;
		this.cbPeakName.Text = "苯";
		this.cbPeakName.SelectedIndexChanged += new System.EventHandler(cbPeakName_SelectedIndexChanged);
		this.cbPeakName.TextChanged += new System.EventHandler(cbPeakName_TextChanged);
		this.tbUnitName.Location = new System.Drawing.Point(17, 337);
		this.tbUnitName.Name = "tbUnitName";
		this.tbUnitName.Size = new System.Drawing.Size(100, 21);
		this.tbUnitName.TabIndex = 13;
		this.tbUnitName.Text = "不转换";
		this.tbUnitName.Visible = false;
		this.tbUnitName.TextChanged += new System.EventHandler(tbUnitName_TextChanged);
		this.label5.AutoSize = true;
		this.label5.Location = new System.Drawing.Point(15, 211);
		this.label5.Name = "label5";
		this.label5.Size = new System.Drawing.Size(65, 12);
		this.label5.TabIndex = 12;
		this.label5.Text = "单位转换：";
		this.tbPeakName.Location = new System.Drawing.Point(17, 320);
		this.tbPeakName.Name = "tbPeakName";
		this.tbPeakName.Size = new System.Drawing.Size(100, 21);
		this.tbPeakName.TabIndex = 11;
		this.tbPeakName.Text = "甲烷";
		this.tbPeakName.Visible = false;
		this.tbPeakName.TextChanged += new System.EventHandler(tbPeakName_TextChanged);
		this.label4.AutoSize = true;
		this.label4.Location = new System.Drawing.Point(15, 175);
		this.label4.Name = "label4";
		this.label4.Size = new System.Drawing.Size(65, 12);
		this.label4.TabIndex = 10;
		this.label4.Text = "组分名称：";
		this.tbTowerNumber.Location = new System.Drawing.Point(98, 293);
		this.tbTowerNumber.Name = "tbTowerNumber";
		this.tbTowerNumber.Size = new System.Drawing.Size(100, 21);
		this.tbTowerNumber.TabIndex = 9;
		this.tbTowerNumber.Text = "4号塔";
		this.tbTowerNumber.Visible = false;
		this.tbTowerNumber.TextChanged += new System.EventHandler(tbTowerNumber_TextChanged);
		this.label3.AutoSize = true;
		this.label3.Location = new System.Drawing.Point(115, 317);
		this.label3.Name = "label3";
		this.label3.Size = new System.Drawing.Size(65, 12);
		this.label3.TabIndex = 8;
		this.label3.Text = "塔号设定：";
		this.label3.Visible = false;
		this.label2.AutoSize = true;
		this.label2.Location = new System.Drawing.Point(15, 82);
		this.label2.Name = "label2";
		this.label2.Size = new System.Drawing.Size(65, 12);
		this.label2.TabIndex = 7;
		this.label2.Text = "含量下限：";
		this.label1.AutoSize = true;
		this.label1.Location = new System.Drawing.Point(15, 44);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(59, 12);
		this.label1.TabIndex = 6;
		this.label1.Text = "含量上限:";
		this.tbLowerLimit.Location = new System.Drawing.Point(77, 76);
		this.tbLowerLimit.Name = "tbLowerLimit";
		this.tbLowerLimit.Size = new System.Drawing.Size(107, 21);
		this.tbLowerLimit.TabIndex = 5;
		this.tbLowerLimit.Text = "99.8";
		this.tbLowerLimit.TextChanged += new System.EventHandler(tbLowerLimit_TextChanged);
		this.tbUpperLimit.Location = new System.Drawing.Point(77, 38);
		this.tbUpperLimit.Name = "tbUpperLimit";
		this.tbUpperLimit.Size = new System.Drawing.Size(107, 21);
		this.tbUpperLimit.TabIndex = 4;
		this.tbUpperLimit.Text = "100";
		this.tbUpperLimit.TextChanged += new System.EventHandler(tbUpperLimit_TextChanged);
		this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
		this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[3] { this.刷新ToolStripMenuItem, this.编辑ToolStripMenuItem, this.删除ToolStripMenuItem });
		this.contextMenuStrip1.Name = "contextMenuStrip1";
		this.contextMenuStrip1.Size = new System.Drawing.Size(101, 70);
		this.刷新ToolStripMenuItem.Name = "刷新ToolStripMenuItem";
		this.刷新ToolStripMenuItem.Size = new System.Drawing.Size(100, 22);
		this.刷新ToolStripMenuItem.Text = "刷新";
		this.刷新ToolStripMenuItem.Click += new System.EventHandler(刷新ToolStripMenuItem_Click);
		this.编辑ToolStripMenuItem.Name = "编辑ToolStripMenuItem";
		this.编辑ToolStripMenuItem.Size = new System.Drawing.Size(100, 22);
		this.编辑ToolStripMenuItem.Text = "编辑";
		this.编辑ToolStripMenuItem.Click += new System.EventHandler(编辑ToolStripMenuItem_Click);
		this.删除ToolStripMenuItem.Name = "删除ToolStripMenuItem";
		this.删除ToolStripMenuItem.Size = new System.Drawing.Size(100, 22);
		this.删除ToolStripMenuItem.Text = "删除";
		this.删除ToolStripMenuItem.Click += new System.EventHandler(删除ToolStripMenuItem_Click);
		this.btnSet.Location = new System.Drawing.Point(105, 240);
		this.btnSet.Name = "btnSet";
		this.btnSet.Size = new System.Drawing.Size(75, 23);
		this.btnSet.TabIndex = 24;
		this.btnSet.Text = "确定";
		this.btnSet.UseVisualStyleBackColor = true;
		this.btnSet.Click += new System.EventHandler(btnSet_Click);
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(584, 361);
		base.Controls.Add(this.splitContainer1);
		base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
		base.Name = "FormAreaPlot";
		this.Text = "历史数据";
		base.Load += new System.EventHandler(FormAreaPlot_Load);
		((System.ComponentModel.ISupportInitialize)this.chart1).EndInit();
		this.splitContainer1.Panel1.ResumeLayout(false);
		this.splitContainer1.Panel2.ResumeLayout(false);
		this.splitContainer1.Panel2.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.splitContainer1).EndInit();
		this.splitContainer1.ResumeLayout(false);
		this.contextMenuStrip1.ResumeLayout(false);
		base.ResumeLayout(false);
	}
}
