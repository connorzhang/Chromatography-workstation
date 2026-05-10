using System;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;
using IBrainChrom2018.Unit;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;

namespace IBrainChrom2018;

public class ExportReport : Form
{
	public static ExportReport selfCtrl;

	private ChromDeviceListMgr cdlMgr = ChromDeviceListMgr.Create();

	private MineParam mineParam = MineParam.Create();

	private FormMainParam frmParam = FormMainParam.Create();

	private DataTable dataSource1;

	private DataTable dataSourceALL;

	private bool m_bLoading = true;

	private IContainer components = null;

	private SplitContainer splitContainer1;

	private Button btnDelete;

	private Label label6;

	private Label label7;

	private DateTimePicker dateTimePicker2;

	private DateTimePicker dateTimePicker1;

	public ComboBox cbPeakName;

	public Label label4;

	private Button BtnOutData;

	private DataGridView dataGridView1;

	private DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;

	private DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;

	private Button button2;

	private GroupBox groupBox1;

	private CheckBox chCO2;

	private CheckBox chC2H4;

	private CheckBox chCO;

	private CheckBox chC2H6;

	private CheckBox chO2;

	private CheckBox chC2H2;

	private CheckBox chCH4;

	private CheckBox chN2;

	private CheckBox chSF6;

	private Button btnSaveLoadData;

	private CheckBox chSO2;

	private CheckBox chC4H10Z;

	private CheckBox chC4H10Y;

	private CheckBox chC3H8;

	private CheckBox chHHS;

	public ExportReport()
	{
		InitializeComponent();
		selfCtrl = this;
		chC2H2.Checked = frmParam.bC2H2;
		chC2H4.Checked = frmParam.bC2H4;
		chC2H6.Checked = frmParam.bC2H6;
		chCH4.Checked = frmParam.bCH4;
		chCO.Checked = frmParam.bCO;
		chCO2.Checked = frmParam.bCO2;
		chN2.Checked = frmParam.bN2;
		chO2.Checked = frmParam.bO2;
		chSF6.Checked = frmParam.bSF6;
		chHHS.Checked = frmParam.bHHS;
		chC3H8.Checked = frmParam.bC3H8;
		chC4H10Y.Checked = frmParam.bC4H10Y;
		chC4H10Z.Checked = frmParam.bC4H10Z;
		chSO2.Checked = frmParam.bSO2;
		DateRolad();
		m_bLoading = false;
	}

	public void DateRolad()
	{
		string text = mineParam.dataTimeStart.ToString("yyyy-MM-dd");
		string text2 = mineParam.dataTimeEnd.ToString("yyyy-MM-dd");
		dateTimePicker1.Value = mineParam.dataTimeStart;
		dateTimePicker2.Text = mineParam.dataTimeEnd.ToString("yyyy-MM-dd");
		cbPeakName.Items.Clear();
		cbPeakName.Items.Add("全部");
		string connectionString = "Data Source='" + Application.StartupPath + "\\ngmpol.dll';Version=3;";
		string text3 = "";
		StringBuilder stringBuilder = new StringBuilder();
		using (SQLiteConnection sQLiteConnection = new SQLiteConnection(connectionString))
		{
			sQLiteConnection.Open();
			string commandText = "select name from sqlite_master where type='table' order by name;";
			SQLiteCommand sQLiteCommand = new SQLiteCommand(commandText, sQLiteConnection);
			using SQLiteDataReader sQLiteDataReader = sQLiteCommand.ExecuteReader();
			while (sQLiteDataReader.Read())
			{
				text3 = sQLiteDataReader["Name"].ToString();
				if (text3 != "VOC" && text3 != "OLog")
				{
					cbPeakName.Items.Add(text3.Substring(2));
				}
			}
		}
		cbPeakName.Text = frmParam.strSampleSite.Substring(2);
	}

	public void loadData()
	{
		if (cbPeakName.Text != "全部")
		{
			DataTable dataTableMINE = Class49.GetDataTableMINE(0, "YB" + cbPeakName.Text, dateTimePicker1.Value, dateTimePicker2.Value);
			if (dataTableMINE == null)
			{
				return;
			}
			dataSource1 = dataTableMINE;
			dataGridView1.DataSource = dataTableMINE;
			dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
		}
		else
		{
			string connectionString = "Data Source='" + Application.StartupPath + "\\ngmpol.dll';Version=3;";
			string text = "";
			StringBuilder stringBuilder = new StringBuilder();
			DataTable dataTable = new DataTable();
			dataTable.Columns.Add("时间", typeof(DateTime));
			dataTable.Columns.Add("地点", typeof(string));
			dataTable.Columns.Add("六氟化硫", typeof(double));
			dataTable.Columns.Add("氮气", typeof(double));
			dataTable.Columns.Add("甲烷", typeof(double));
			dataTable.Columns.Add("乙炔", typeof(double));
			dataTable.Columns.Add("氧气", typeof(double));
			dataTable.Columns.Add("乙烷", typeof(double));
			dataTable.Columns.Add("一氧化碳", typeof(double));
			dataTable.Columns.Add("乙烯", typeof(double));
			dataTable.Columns.Add("二氧化碳", typeof(double));
			dataTable.Columns.Add("硫化氢", typeof(double));
			dataTable.Columns.Add("丙烷", typeof(double));
			dataTable.Columns.Add("异丁烷", typeof(double));
			dataTable.Columns.Add("正丁烷", typeof(double));
			dataTable.Columns.Add("二氧化硫", typeof(double));
			using SQLiteConnection sQLiteConnection = new SQLiteConnection(connectionString);
			sQLiteConnection.Open();
			string commandText = "select name from sqlite_master where type='table' order by name;";
			SQLiteCommand sQLiteCommand = new SQLiteCommand(commandText, sQLiteConnection);
			using SQLiteDataReader sQLiteDataReader = sQLiteCommand.ExecuteReader();
			while (sQLiteDataReader.Read())
			{
				text = sQLiteDataReader["Name"].ToString();
				if (!(text != "VOC") || !(text != "OLog"))
				{
					continue;
				}
				DataTable dataTableMINE2 = Class49.GetDataTableMINE(0, text, dateTimePicker1.Value, dateTimePicker2.Value);
				DataView defaultView = dataTableMINE2.DefaultView;
				foreach (DataRowView item in defaultView)
				{
					DateTime dateTime = (DateTime)item["时间"];
					string text2 = (string)item["地点"];
					double num = (double)item.Row.ItemArray[2];
					double num2 = (double)item.Row.ItemArray[3];
					double num3 = (double)item["甲烷"];
					double num4 = (double)item["乙炔"];
					double num5 = (double)item["氧气"];
					double num6 = (double)item["乙烷"];
					double num7 = (double)item["一氧化碳"];
					double num8 = (double)item["乙烯"];
					double num9 = (double)item["二氧化碳"];
					double num10 = (double)item["硫化氢"];
					double num11 = (double)item["丙烷"];
					double num12 = (double)item["异丁烷"];
					double num13 = (double)item["正丁烷"];
					double num14 = (double)item["二氧化硫"];
					dataTable.Rows.Add(dateTime, text2, num, num2, num3, num4, num5, num6, num7, num8, num9, num10, num11, num12, num13, num14);
				}
			}
			dataSourceALL = dataTable;
			dataGridView1.DataSource = dataSourceALL;
		}
		if (!frmParam.bC2H2)
		{
			dataGridView1.Columns.Remove("乙炔");
		}
		if (!frmParam.bC2H4)
		{
			dataGridView1.Columns.Remove("乙烯");
		}
		if (!frmParam.bC2H6)
		{
			dataGridView1.Columns.Remove("乙烷");
		}
		if (!frmParam.bCH4)
		{
			dataGridView1.Columns.Remove("甲烷");
		}
		if (!frmParam.bCO)
		{
			dataGridView1.Columns.Remove("一氧化碳");
		}
		if (!frmParam.bCO2)
		{
			dataGridView1.Columns.Remove("二氧化碳");
		}
		if (!frmParam.bN2)
		{
			dataGridView1.Columns.Remove("氮气");
		}
		if (!frmParam.bO2)
		{
			dataGridView1.Columns.Remove("氧气");
		}
		if (!frmParam.bSF6)
		{
			dataGridView1.Columns.Remove("六氟化硫");
		}
		if (!frmParam.bHHS)
		{
			dataGridView1.Columns.Remove("硫化氢");
		}
		if (!frmParam.bC3H8)
		{
			dataGridView1.Columns.Remove("丙烷");
		}
		if (!frmParam.bC4H10Y)
		{
			dataGridView1.Columns.Remove("异丁烷");
		}
		if (!frmParam.bC4H10Z)
		{
			dataGridView1.Columns.Remove("正丁烷");
		}
		if (!frmParam.bSO2)
		{
			dataGridView1.Columns.Remove("二氧化硫");
		}
	}

	private void DateTimePicker1_ValueChanged(object sender, EventArgs e)
	{
		if (!m_bLoading)
		{
			mineParam.dataTimeStart = dateTimePicker1.Value;
			mineParam.SaveParam();
			loadData();
		}
	}

	private void DateTimePicker2_ValueChanged(object sender, EventArgs e)
	{
		if (!m_bLoading)
		{
			mineParam.dataTimeEnd = dateTimePicker2.Value;
			mineParam.SaveParam();
			loadData();
		}
	}

	private void CbPeakName_SelectedIndexChanged(object sender, EventArgs e)
	{
		if (!m_bLoading)
		{
			loadData();
		}
	}

	private void Button2_Click(object sender, EventArgs e)
	{
		if (cbPeakName.Text == "全部")
		{
			DialogResult dialogResult = MessageBox.Show("确认删除所有采样点数据？", "删除数据", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
			if (dialogResult != DialogResult.OK)
			{
			}
			return;
		}
		DialogResult dialogResult2 = MessageBox.Show("确认删除采样点:" + cbPeakName.Text + "的所有数据？", "删除数据", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
		if (dialogResult2 == DialogResult.OK)
		{
			Class49.DeleteDataTable("YB" + cbPeakName.Text);
			DateRolad();
		}
	}

	private void BtnOutData_Click(object sender, EventArgs e)
	{
		SaveFileDialog saveFileDialog = new SaveFileDialog();
		saveFileDialog.FileName = "含量表" + DateTime.Now.ToString("yyyyMMddHHmm") + ".xls";
		saveFileDialog.Filter = " csv files(*.csv)|*.csv|All files(*.*)|*.*";
		saveFileDialog.FilterIndex = 2;
		saveFileDialog.RestoreDirectory = true;
		if (saveFileDialog.ShowDialog() == DialogResult.OK)
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
			if (cbPeakName.Text == "全部")
			{
				dataToExcel(dataGridView1, text);
			}
			else
			{
				dataToExcel(dataGridView1, text);
			}
			Process.Start(text);
		}
	}

	public bool dataToExcel(DataGridView ordgv, string Outpath)
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
				row = sheet.GetRow(5);
				for (int i = 1; i < count2; i++)
				{
					cell = row.GetCell(i);
					if (cell == null)
					{
						cell = row.CreateCell(i);
					}
				}
				for (int j = 4; j < count + 3; j++)
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
						switch (k)
						{
						case 1:
							cell.SetCellValue(dataGridView.Rows[j - 4].Cells[columnName].Value.ToString());
							break;
						case 2:
							cell.SetCellValue(dataGridView.Rows[j - 4].Cells[columnName].Value.ToString());
							break;
						default:
							double.TryParse(dataGridView.Rows[j - 4].Cells[columnName].Value.ToString(), out result2);
							cell.SetCellValue(result2.ToString("0.00000"));
							break;
						}
					}
					for (int l = 3; l <= count2; l++)
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

	private void ExportReport_Load(object sender, EventArgs e)
	{
		loadData();
	}

	private void btnSaveLoadData_Click(object sender, EventArgs e)
	{
		frmParam.bC2H2 = chC2H2.Checked;
		frmParam.bC2H4 = chC2H4.Checked;
		frmParam.bC2H6 = chC2H6.Checked;
		frmParam.bCH4 = chCH4.Checked;
		frmParam.bCO = chCO.Checked;
		frmParam.bCO2 = chCO2.Checked;
		frmParam.bN2 = chN2.Checked;
		frmParam.bO2 = chO2.Checked;
		frmParam.bSF6 = chSF6.Checked;
		frmParam.bHHS = chHHS.Checked;
		frmParam.bC3H8 = chC3H8.Checked;
		frmParam.bC4H10Y = chC4H10Y.Checked;
		frmParam.bC4H10Z = chC4H10Z.Checked;
		frmParam.bSO2 = chSO2.Checked;
		frmParam.SaveParam();
		loadData();
	}

	private void btnDelete_Click(object sender, EventArgs e)
	{
		if (dataGridView1.CurrentRow != null)
		{
			DialogResult dialogResult = MessageBox.Show(string.Concat("确定删除行", dataGridView1.CurrentRow.Cells[0].Value, "吗?"), "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
			if (dialogResult == DialogResult.Yes)
			{
				DateTime dateTime = DateTime.Parse(dataGridView1.CurrentRow.Cells[0].Value.ToString());
				DateTime startTime = dateTime.Subtract(new TimeSpan(1000L));
				DateTime endtTime = dateTime.AddSeconds(1.0);
				MessageBox.Show("成功删除数据" + Class49.DeleteDataTable("YB" + cbPeakName.Text, startTime, endtTime) + "条");
				DateRolad();
			}
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(IBrainChrom2018.ExportReport));
		this.splitContainer1 = new System.Windows.Forms.SplitContainer();
		this.dataGridView1 = new System.Windows.Forms.DataGridView();
		this.groupBox1 = new System.Windows.Forms.GroupBox();
		this.chSO2 = new System.Windows.Forms.CheckBox();
		this.chC4H10Z = new System.Windows.Forms.CheckBox();
		this.chC4H10Y = new System.Windows.Forms.CheckBox();
		this.chC3H8 = new System.Windows.Forms.CheckBox();
		this.chHHS = new System.Windows.Forms.CheckBox();
		this.btnSaveLoadData = new System.Windows.Forms.Button();
		this.chCO2 = new System.Windows.Forms.CheckBox();
		this.chC2H4 = new System.Windows.Forms.CheckBox();
		this.chCO = new System.Windows.Forms.CheckBox();
		this.chC2H6 = new System.Windows.Forms.CheckBox();
		this.chO2 = new System.Windows.Forms.CheckBox();
		this.chC2H2 = new System.Windows.Forms.CheckBox();
		this.chCH4 = new System.Windows.Forms.CheckBox();
		this.chN2 = new System.Windows.Forms.CheckBox();
		this.chSF6 = new System.Windows.Forms.CheckBox();
		this.button2 = new System.Windows.Forms.Button();
		this.btnDelete = new System.Windows.Forms.Button();
		this.label6 = new System.Windows.Forms.Label();
		this.label7 = new System.Windows.Forms.Label();
		this.dateTimePicker2 = new System.Windows.Forms.DateTimePicker();
		this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
		this.cbPeakName = new System.Windows.Forms.ComboBox();
		this.label4 = new System.Windows.Forms.Label();
		this.BtnOutData = new System.Windows.Forms.Button();
		this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
		((System.ComponentModel.ISupportInitialize)this.splitContainer1).BeginInit();
		this.splitContainer1.Panel1.SuspendLayout();
		this.splitContainer1.Panel2.SuspendLayout();
		this.splitContainer1.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.dataGridView1).BeginInit();
		this.groupBox1.SuspendLayout();
		base.SuspendLayout();
		this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
		this.splitContainer1.Location = new System.Drawing.Point(0, 0);
		this.splitContainer1.Name = "splitContainer1";
		this.splitContainer1.Panel1.Controls.Add(this.dataGridView1);
		this.splitContainer1.Panel2.Controls.Add(this.groupBox1);
		this.splitContainer1.Panel2.Controls.Add(this.button2);
		this.splitContainer1.Panel2.Controls.Add(this.btnDelete);
		this.splitContainer1.Panel2.Controls.Add(this.label6);
		this.splitContainer1.Panel2.Controls.Add(this.label7);
		this.splitContainer1.Panel2.Controls.Add(this.dateTimePicker2);
		this.splitContainer1.Panel2.Controls.Add(this.dateTimePicker1);
		this.splitContainer1.Panel2.Controls.Add(this.cbPeakName);
		this.splitContainer1.Panel2.Controls.Add(this.label4);
		this.splitContainer1.Panel2.Controls.Add(this.BtnOutData);
		this.splitContainer1.Size = new System.Drawing.Size(1035, 553);
		this.splitContainer1.SplitterDistance = 760;
		this.splitContainer1.TabIndex = 5;
		this.dataGridView1.AllowUserToOrderColumns = true;
		this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
		this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
		this.dataGridView1.Location = new System.Drawing.Point(0, 0);
		this.dataGridView1.Name = "dataGridView1";
		this.dataGridView1.RowTemplate.Height = 23;
		this.dataGridView1.Size = new System.Drawing.Size(760, 553);
		this.dataGridView1.TabIndex = 0;
		this.groupBox1.Controls.Add(this.chSO2);
		this.groupBox1.Controls.Add(this.chC4H10Z);
		this.groupBox1.Controls.Add(this.chC4H10Y);
		this.groupBox1.Controls.Add(this.chC3H8);
		this.groupBox1.Controls.Add(this.chHHS);
		this.groupBox1.Controls.Add(this.btnSaveLoadData);
		this.groupBox1.Controls.Add(this.chCO2);
		this.groupBox1.Controls.Add(this.chC2H4);
		this.groupBox1.Controls.Add(this.chCO);
		this.groupBox1.Controls.Add(this.chC2H6);
		this.groupBox1.Controls.Add(this.chO2);
		this.groupBox1.Controls.Add(this.chC2H2);
		this.groupBox1.Controls.Add(this.chCH4);
		this.groupBox1.Controls.Add(this.chN2);
		this.groupBox1.Controls.Add(this.chSF6);
		this.groupBox1.Location = new System.Drawing.Point(21, 114);
		this.groupBox1.Name = "groupBox1";
		this.groupBox1.Size = new System.Drawing.Size(174, 340);
		this.groupBox1.TabIndex = 23;
		this.groupBox1.TabStop = false;
		this.groupBox1.Text = "导出组份";
		this.chSO2.AutoSize = true;
		this.chSO2.Location = new System.Drawing.Point(17, 306);
		this.chSO2.Name = "chSO2";
		this.chSO2.Size = new System.Drawing.Size(72, 16);
		this.chSO2.TabIndex = 14;
		this.chSO2.Text = "二氧化硫";
		this.chSO2.UseVisualStyleBackColor = true;
		this.chC4H10Z.AutoSize = true;
		this.chC4H10Z.Location = new System.Drawing.Point(17, 284);
		this.chC4H10Z.Name = "chC4H10Z";
		this.chC4H10Z.Size = new System.Drawing.Size(60, 16);
		this.chC4H10Z.TabIndex = 13;
		this.chC4H10Z.Text = "正丁烷";
		this.chC4H10Z.UseVisualStyleBackColor = true;
		this.chC4H10Y.AutoSize = true;
		this.chC4H10Y.Location = new System.Drawing.Point(17, 262);
		this.chC4H10Y.Name = "chC4H10Y";
		this.chC4H10Y.Size = new System.Drawing.Size(60, 16);
		this.chC4H10Y.TabIndex = 12;
		this.chC4H10Y.Text = "异丁烷";
		this.chC4H10Y.UseVisualStyleBackColor = true;
		this.chC3H8.AutoSize = true;
		this.chC3H8.Location = new System.Drawing.Point(17, 240);
		this.chC3H8.Name = "chC3H8";
		this.chC3H8.Size = new System.Drawing.Size(48, 16);
		this.chC3H8.TabIndex = 11;
		this.chC3H8.Text = "丙烷";
		this.chC3H8.UseVisualStyleBackColor = true;
		this.chHHS.AutoSize = true;
		this.chHHS.Location = new System.Drawing.Point(17, 218);
		this.chHHS.Name = "chHHS";
		this.chHHS.Size = new System.Drawing.Size(60, 16);
		this.chHHS.TabIndex = 10;
		this.chHHS.Text = "硫化氢";
		this.chHHS.UseVisualStyleBackColor = true;
		this.btnSaveLoadData.Location = new System.Drawing.Point(89, 299);
		this.btnSaveLoadData.Name = "btnSaveLoadData";
		this.btnSaveLoadData.Size = new System.Drawing.Size(75, 23);
		this.btnSaveLoadData.TabIndex = 9;
		this.btnSaveLoadData.Text = "保存";
		this.btnSaveLoadData.UseVisualStyleBackColor = true;
		this.btnSaveLoadData.Click += new System.EventHandler(btnSaveLoadData_Click);
		this.chCO2.AutoSize = true;
		this.chCO2.Location = new System.Drawing.Point(17, 196);
		this.chCO2.Name = "chCO2";
		this.chCO2.Size = new System.Drawing.Size(72, 16);
		this.chCO2.TabIndex = 8;
		this.chCO2.Text = "二氧化碳";
		this.chCO2.UseVisualStyleBackColor = true;
		this.chC2H4.AutoSize = true;
		this.chC2H4.Location = new System.Drawing.Point(17, 174);
		this.chC2H4.Name = "chC2H4";
		this.chC2H4.Size = new System.Drawing.Size(48, 16);
		this.chC2H4.TabIndex = 7;
		this.chC2H4.Text = "乙烯";
		this.chC2H4.UseVisualStyleBackColor = true;
		this.chCO.AutoSize = true;
		this.chCO.Location = new System.Drawing.Point(17, 152);
		this.chCO.Name = "chCO";
		this.chCO.Size = new System.Drawing.Size(72, 16);
		this.chCO.TabIndex = 6;
		this.chCO.Text = "一氧化碳";
		this.chCO.UseVisualStyleBackColor = true;
		this.chC2H6.AutoSize = true;
		this.chC2H6.Location = new System.Drawing.Point(17, 130);
		this.chC2H6.Name = "chC2H6";
		this.chC2H6.Size = new System.Drawing.Size(48, 16);
		this.chC2H6.TabIndex = 5;
		this.chC2H6.Text = "乙烷";
		this.chC2H6.UseVisualStyleBackColor = true;
		this.chO2.AutoSize = true;
		this.chO2.Location = new System.Drawing.Point(17, 108);
		this.chO2.Name = "chO2";
		this.chO2.Size = new System.Drawing.Size(48, 16);
		this.chO2.TabIndex = 4;
		this.chO2.Text = "氧气";
		this.chO2.UseVisualStyleBackColor = true;
		this.chC2H2.AutoSize = true;
		this.chC2H2.Location = new System.Drawing.Point(17, 86);
		this.chC2H2.Name = "chC2H2";
		this.chC2H2.Size = new System.Drawing.Size(48, 16);
		this.chC2H2.TabIndex = 3;
		this.chC2H2.Text = "乙炔";
		this.chC2H2.UseVisualStyleBackColor = true;
		this.chCH4.AutoSize = true;
		this.chCH4.Location = new System.Drawing.Point(17, 64);
		this.chCH4.Name = "chCH4";
		this.chCH4.Size = new System.Drawing.Size(48, 16);
		this.chCH4.TabIndex = 2;
		this.chCH4.Text = "甲烷";
		this.chCH4.UseVisualStyleBackColor = true;
		this.chN2.AutoSize = true;
		this.chN2.Location = new System.Drawing.Point(17, 42);
		this.chN2.Name = "chN2";
		this.chN2.Size = new System.Drawing.Size(48, 16);
		this.chN2.TabIndex = 1;
		this.chN2.Text = "氮气";
		this.chN2.UseVisualStyleBackColor = true;
		this.chSF6.AutoSize = true;
		this.chSF6.Location = new System.Drawing.Point(17, 20);
		this.chSF6.Name = "chSF6";
		this.chSF6.Size = new System.Drawing.Size(72, 16);
		this.chSF6.TabIndex = 0;
		this.chSF6.Text = "六氟化硫";
		this.chSF6.UseVisualStyleBackColor = true;
		this.button2.Location = new System.Drawing.Point(21, 460);
		this.button2.Name = "button2";
		this.button2.Size = new System.Drawing.Size(75, 23);
		this.button2.TabIndex = 22;
		this.button2.Text = "删除采样点";
		this.button2.UseVisualStyleBackColor = true;
		this.button2.Click += new System.EventHandler(Button2_Click);
		this.btnDelete.Location = new System.Drawing.Point(21, 489);
		this.btnDelete.Name = "btnDelete";
		this.btnDelete.Size = new System.Drawing.Size(75, 23);
		this.btnDelete.TabIndex = 21;
		this.btnDelete.Text = "删除选中行";
		this.btnDelete.UseVisualStyleBackColor = true;
		this.btnDelete.Click += new System.EventHandler(btnDelete_Click);
		this.label6.AutoSize = true;
		this.label6.Location = new System.Drawing.Point(21, 43);
		this.label6.Name = "label6";
		this.label6.Size = new System.Drawing.Size(65, 12);
		this.label6.TabIndex = 20;
		this.label6.Text = "结束日期：";
		this.label7.AutoSize = true;
		this.label7.Location = new System.Drawing.Point(21, 18);
		this.label7.Name = "label7";
		this.label7.Size = new System.Drawing.Size(65, 12);
		this.label7.TabIndex = 19;
		this.label7.Text = "起始日期：";
		this.dateTimePicker2.Checked = false;
		this.dateTimePicker2.CustomFormat = "yyyy/MM/dd";
		this.dateTimePicker2.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
		this.dateTimePicker2.Location = new System.Drawing.Point(88, 39);
		this.dateTimePicker2.Name = "dateTimePicker2";
		this.dateTimePicker2.Size = new System.Drawing.Size(107, 21);
		this.dateTimePicker2.TabIndex = 18;
		this.dateTimePicker2.ValueChanged += new System.EventHandler(DateTimePicker2_ValueChanged);
		this.dateTimePicker1.Checked = false;
		this.dateTimePicker1.CustomFormat = "yyyy/MM/dd";
		this.dateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
		this.dateTimePicker1.Location = new System.Drawing.Point(88, 12);
		this.dateTimePicker1.Name = "dateTimePicker1";
		this.dateTimePicker1.Size = new System.Drawing.Size(107, 21);
		this.dateTimePicker1.TabIndex = 17;
		this.dateTimePicker1.Value = new System.DateTime(2018, 7, 12, 0, 0, 0, 0);
		this.dateTimePicker1.ValueChanged += new System.EventHandler(DateTimePicker1_ValueChanged);
		this.cbPeakName.FormattingEnabled = true;
		this.cbPeakName.Location = new System.Drawing.Point(88, 74);
		this.cbPeakName.Name = "cbPeakName";
		this.cbPeakName.Size = new System.Drawing.Size(107, 20);
		this.cbPeakName.TabIndex = 14;
		this.cbPeakName.SelectedIndexChanged += new System.EventHandler(CbPeakName_SelectedIndexChanged);
		this.label4.AutoSize = true;
		this.label4.Location = new System.Drawing.Point(21, 79);
		this.label4.Name = "label4";
		this.label4.Size = new System.Drawing.Size(41, 12);
		this.label4.TabIndex = 10;
		this.label4.Text = "采样点";
		this.BtnOutData.Location = new System.Drawing.Point(21, 518);
		this.BtnOutData.Name = "BtnOutData";
		this.BtnOutData.Size = new System.Drawing.Size(75, 23);
		this.BtnOutData.TabIndex = 3;
		this.BtnOutData.Text = "导出数据";
		this.BtnOutData.UseVisualStyleBackColor = true;
		this.BtnOutData.Click += new System.EventHandler(BtnOutData_Click);
		this.dataGridViewTextBoxColumn1.HeaderText = "采样点";
		this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
		this.dataGridViewTextBoxColumn1.Width = 275;
		this.dataGridViewTextBoxColumn2.HeaderText = "采样时间";
		this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
		this.dataGridViewTextBoxColumn2.Width = 275;
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(1035, 553);
		base.Controls.Add(this.splitContainer1);
		base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
		base.Name = "ExportReport";
		this.Text = "检测报告";
		base.Load += new System.EventHandler(ExportReport_Load);
		this.splitContainer1.Panel1.ResumeLayout(false);
		this.splitContainer1.Panel2.ResumeLayout(false);
		this.splitContainer1.Panel2.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.splitContainer1).EndInit();
		this.splitContainer1.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.dataGridView1).EndInit();
		this.groupBox1.ResumeLayout(false);
		this.groupBox1.PerformLayout();
		base.ResumeLayout(false);
	}
}
