using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Windows.Forms;
using IBrainChrom2018.Unit;
using Microsoft.Office.Interop.Excel;

namespace IBrainChrom2018;

public class FrmOperLog : Form
{
	[CompilerGenerated]
	private static class Class9
	{
		public static CallSite<Func<CallSite, object, Worksheet>> callSite_0;

		public static CallSite<Func<CallSite, object, Range>> callSite_1;
	}

	private bool bool_0;

	private bool bool_1 = true;

	private bool bool_2 = true;

	private bool bool_3 = true;

	private bool bool_4 = true;

	private bool bool_5 = true;

	private DataGridViewPrinter dataGridViewPrinter_0;

	private IContainer icontainer_0;

	private ToolStrip toolStrip1;

	private ToolStripButton toolStripButton1;

	private ToolStripSeparator toolStripSeparator1;

	private ToolStripButton toolStripButton2;

	private ToolStripButton toolStripButton3;

	private ToolStripSeparator toolStripSeparator2;

	private ToolStripButton toolStripButton4;

	private ToolStripButton toolStripButton5;

	private ToolStripButton toolStripButton6;

	private ToolStripButton toolStripButton7;

	private ToolStripButton toolStripButton8;

	private ToolStripSeparator toolStripSeparator3;

	private ToolStripButton toolStripButton9;

	private DataGridView dataGridView1;

	private TabControl tabControl1;

	private TabPage tabPage1;

	private TabPage tabPage2;

	private DataGridView dataGridView2;

	private ImageList imageList_0;

	private ToolStripSeparator toolStripSeparator4;

	private ToolStripButton toolStripButton10;

	private System.Windows.Forms.Timer timer_0;

	private PrintDocument printDocument_0;

	private PrintPreviewDialog prtPrvDlg;

	private PrintDialog printDialog_0;

	private ToolStripButton toolStripButton11;

	private DateTimePicker dateTimePicker1;

	private Button button1;

	public FrmOperLog()
	{
		InitializeComponent();
		CtrlLangPS.Instance.UpdateLanguageForAllControl(this);
	}

	private void FrmOperLog_Load(object sender, EventArgs e)
	{
		SystemParam systemParam = SystemParam.Create();
		string text = "DATE([DateTime]) = DATE(datetime('now','localtime'))";
		string text2 = "([DateTime]>DATE()-1) and ([DateTime]<DATE()+1)";
		string text3 = "SELECT [DateTime] as 记录日期 ,[Type] as 类型,[NetChromUserName] as 操作人员,[IntrumentName] as 机器名称, [Moudle] as 模块 ,  [Describe] as 描述,[ComputerUsername] as 系统用户名, [ComputerName] as 计算机名称, [VerInfo] as 版本号 FROM [OLog] WHERE  ";
		text3 = ((systemParam.iDbConnectType != 1) ? (text3 + text2) : (text3 + text));
		DataTable dataTable = Class49.GetDataTable(text3);
		dataGridView1.DataSource = dataTable;
	}

	private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
	{
		if (tabControl1.SelectedIndex == 1)
		{
			SystemParam systemParam = SystemParam.Create();
			string text = "DATE([DateTime]) = DATE(datetime('now','localtime','-1 day') )";
			string text2 = "[DateTime]<DATE()-1";
			string text3 = "SELECT [DateTime] as 记录日期 ,[Type] as 类型,[NetChromUserName] as 操作人员,[IntrumentName] as 机器名称, [Moudle] as 模块 ,  [Describe] as 描述,[ComputerUsername] as 系统用户名, [ComputerName] as 计算机名称, [VerInfo] as 版本号 FROM [OLog] WHERE  ";
			text3 = ((systemParam.iDbConnectType != 1) ? (text3 + text2) : (text3 + text));
			DataTable dataTable = Class49.GetDataTable(text3);
			dataGridView2.DataSource = dataTable;
		}
	}

	private void FrmOperLog_FormClosing(object sender, FormClosingEventArgs e)
	{
		e.Cancel = true;
		Hide();
	}

	private void timer_0_Tick(object sender, EventArgs e)
	{
		method_5();
	}

	private void toolStripButton1_Click(object sender, EventArgs e)
	{
		bool flag = (timer_0.Enabled = !timer_0.Enabled);
		bool_0 = flag;
		if (bool_0)
		{
			toolStripButton1.Image = imageList_0.Images[10];
		}
		else
		{
			toolStripButton1.Image = imageList_0.Images[11];
		}
	}

	private string method_0()
	{
		string text = "";
		if (bool_1)
		{
			text = " [Type]='系统'";
		}
		if (bool_2)
		{
			text = ((!(text != "")) ? (text + " [Type]='方法'") : (text + " OR [Type]='方法'"));
		}
		if (bool_3)
		{
			text = ((!(text != "")) ? (text + "  [Type]='谱图'") : (text + " OR [Type]='谱图'"));
		}
		if (bool_4)
		{
			text = ((!(text != "")) ? (text + "  [Type]='打印'") : (text + " OR [Type]='打印'"));
		}
		if (bool_5)
		{
			text = ((!(text != "")) ? (text + "   [Type]='反控'") : (text + " OR [Type]='反控'"));
		}
		return text;
	}

	private string SelectSql()
	{
		SystemParam systemParam = SystemParam.Create();
		if (systemParam.iDbConnectType == 1)
		{
			return "SELECT [DateTime] as 记录日期 ,[Type] as 类型,[NetChromUserName] as 操作人员,[IntrumentName] as 机器名称, [Moudle] as 模块 ,  [Describe] as 描述,[ComputerUsername] as 系统用户名, [ComputerName] as 计算机名称, [VerInfo] as 版本号 FROM [OLog] WHERE  DATE([DateTime]) = DATE(datetime('now','localtime')) AND  (" + method_0() + ")";
		}
		return "SELECT [DateTime] as 记录日期 ,[Type] as 类型,[NetChromUserName] as 操作人员,[IntrumentName] as 机器名称, [Moudle] as 模块 ,  [Describe] as 描述,[ComputerUsername] as 系统用户名, [ComputerName] as 计算机名称, [VerInfo] as 版本号 FROM [OLog] WHERE  ([DateTime]>(DATE()-1) AND [DateTime]<(DATE()+1)) AND  (" + method_0() + ")";
	}

	private string Select2Sql()
	{
		string text = dateTimePicker1.Value.ToString("yyyy-MM-dd ");
		text = text.Replace('/', '-');
		SystemParam systemParam = SystemParam.Create();
		if (systemParam.iDbConnectType == 1)
		{
			return "SELECT [DateTime] as 记录日期 ,[Type] as 类型,[NetChromUserName] as 操作人员,[IntrumentName] as 机器名称, [Moudle] as 模块 ,  [Describe] as 描述,[ComputerUsername] as 系统用户名, [ComputerName] as 计算机名称, [VerInfo] as 版本号 FROM [OLog] WHERE  DATE([DateTime]) = DATE('" + text.Trim() + "') AND  (" + method_0() + ")";
		}
		return "SELECT [DateTime] as 记录日期 ,[Type] as 类型,[NetChromUserName] as 操作人员,[IntrumentName] as 机器名称, [Moudle] as 模块 ,  [Describe] as 描述,[ComputerUsername] as 系统用户名, [ComputerName] as 计算机名称, [VerInfo] as 版本号 FROM [OLog] WHERE  [DateTime]<CDATE('" + text.Trim() + "') AND  (" + method_0() + ")";
	}

	private void method_3()
	{
		DataTable dataTable = Class49.GetDataTable(SelectSql());
		dataGridView1.DataSource = dataTable;
	}

	private void method_4()
	{
		DataTable dataTable = Class49.GetDataTable(Select2Sql());
		dataGridView2.DataSource = dataTable;
	}

	private void method_5()
	{
		if (tabControl1.SelectedIndex == 0)
		{
			method_3();
		}
		if (tabControl1.SelectedIndex == 1)
		{
			method_4();
		}
	}

	private void toolStripButton4_Click(object sender, EventArgs e)
	{
		if (bool_1)
		{
			toolStripButton4.Image = imageList_0.Images[7];
		}
		else
		{
			toolStripButton4.Image = imageList_0.Images[2];
		}
		bool_1 = !bool_1;
		method_5();
	}

	private void toolStripButton5_Click(object sender, EventArgs e)
	{
		if (bool_2)
		{
			toolStripButton5.Image = imageList_0.Images[5];
		}
		else
		{
			toolStripButton5.Image = imageList_0.Images[0];
		}
		bool_2 = !bool_2;
		method_5();
	}

	private void toolStripButton6_Click(object sender, EventArgs e)
	{
		if (bool_3)
		{
			toolStripButton6.Image = imageList_0.Images[6];
		}
		else
		{
			toolStripButton6.Image = imageList_0.Images[1];
		}
		bool_3 = !bool_3;
		method_5();
	}

	private void toolStripButton7_Click(object sender, EventArgs e)
	{
		if (bool_4)
		{
			toolStripButton7.Image = imageList_0.Images[8];
		}
		else
		{
			toolStripButton7.Image = imageList_0.Images[3];
		}
		bool_4 = !bool_4;
		method_5();
	}

	private void toolStripButton8_Click(object sender, EventArgs e)
	{
		if (bool_5)
		{
			toolStripButton8.Image = imageList_0.Images[9];
		}
		else
		{
			toolStripButton8.Image = imageList_0.Images[4];
		}
		bool_5 = !bool_5;
		method_5();
	}

	private void toolStripButton9_Click(object sender, EventArgs e)
	{
		method_5();
	}

	private void toolStripButton10_Click(object sender, EventArgs e)
	{
		if (tabControl1.SelectedIndex == 0)
		{
			ExportExcel((DataTable)dataGridView1.DataSource);
		}
		if (tabControl1.SelectedIndex == 1)
		{
			ExportExcel((DataTable)dataGridView2.DataSource);
		}
	}

	protected void ExportExcel(DataTable dataTable_0)
	{
		if (dataTable_0 == null || dataTable_0 == null || dataTable_0.Rows.Count == 0)
		{
			return;
		}
		Microsoft.Office.Interop.Excel.Application application = (Microsoft.Office.Interop.Excel.Application)Activator.CreateInstance(Type.GetTypeFromCLSID(new Guid("00024500-0000-0000-C000-000000000046")));
		if (application == null)
		{
			return;
		}
		CultureInfo currentCulture = Thread.CurrentThread.CurrentCulture;
		Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
		Workbooks workbooks = application.Workbooks;
		Workbook workbook = workbooks.Add(XlWBATemplate.xlWBATWorksheet);
		Worksheet worksheet = (Worksheet)(dynamic)workbook.Worksheets[1];
		long num = dataTable_0.Rows.Count;
		long num2 = 0L;
		for (int i = 0; i < dataTable_0.Columns.Count; i++)
		{
			worksheet.Cells[1, i + 1] = dataTable_0.Columns[i].ColumnName;
			Range range = (Range)(dynamic)worksheet.Cells[1, i + 1];
			range.Interior.ColorIndex = 15;
			range.Font.Bold = true;
		}
		for (int j = 0; j < dataTable_0.Rows.Count; j++)
		{
			for (int k = 0; k < dataTable_0.Columns.Count; k++)
			{
				worksheet.Cells[j + 2, k + 1] = dataTable_0.Rows[j][k].ToString();
			}
			num2++;
		}
		application.Visible = true;
	}

	private void toolStripButton2_Click(object sender, EventArgs e)
	{
		try
		{
			DataGridViewPrinter.flag = 1;
			prtPrvDlg = new PrintPreviewDialog();
			prtPrvDlg.Document = printDocument_0;
			prtPrvDlg.ShowIcon = false;
			prtPrvDlg.TopMost = true;
			prtPrvDlg.Show();
			prtPrvDlg.Activate();
			if (tabControl1.SelectedIndex == 0)
			{
				dataGridViewPrinter_0 = new DataGridViewPrinter(dataGridView1, printDocument_0, CenterOnPage: true, WithTitle: true, "日志打印", new System.Drawing.Font("Tahoma", 18f, FontStyle.Bold, GraphicsUnit.Point), Color.Black, WithPaging: true);
			}
			else
			{
				dataGridViewPrinter_0 = new DataGridViewPrinter(dataGridView2, printDocument_0, CenterOnPage: true, WithTitle: true, "日志打印", new System.Drawing.Font("Tahoma", 18f, FontStyle.Bold, GraphicsUnit.Point), Color.Black, WithPaging: true);
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message);
			prtPrvDlg.Close();
		}
	}

	private bool method_6()
	{
		PageSetupDialog pageSetupDialog = new PageSetupDialog();
		PageSettings pageSettings = new PageSettings();
		pageSetupDialog.PageSettings = pageSettings;
		pageSetupDialog.ShowDialog();
		PrintDialog printDialog = new PrintDialog();
		printDialog.AllowCurrentPage = false;
		printDialog.AllowPrintToFile = false;
		printDialog.AllowSelection = false;
		printDialog.AllowSomePages = false;
		printDialog.PrintToFile = false;
		printDialog.ShowHelp = false;
		printDialog.ShowNetwork = false;
		if (printDialog.ShowDialog() != DialogResult.OK)
		{
			return false;
		}
		printDocument_0.DocumentName = "Log Report";
		printDocument_0.PrinterSettings = printDialog.PrinterSettings;
		printDocument_0.DefaultPageSettings = printDialog.PrinterSettings.DefaultPageSettings;
		printDocument_0.DefaultPageSettings.Margins = new Margins(40, 40, 40, 40);
		printDocument_0.DefaultPageSettings.Landscape = pageSetupDialog.PageSettings.Landscape;
		return true;
	}

	private void toolStripButton3_Click(object sender, EventArgs e)
	{
		method_6();
	}

	private void printDocument_0_PrintPage(object sender, PrintPageEventArgs e)
	{
		if (DataGridViewPrinter.flag != 0 && !DataGridViewPrinter.hasmorepages)
		{
			DataGridViewPrinter.flag++;
		}
		if (dataGridViewPrinter_0.DrawDataGridView(e.Graphics))
		{
			e.HasMorePages = true;
			DataGridViewPrinter.hasmorepages = true;
		}
		else
		{
			DataGridViewPrinter.hasmorepages = false;
		}
	}

	private void toolStripButton11_Click(object sender, EventArgs e)
	{
		Close();
	}

	private void button1_Click(object sender, EventArgs e)
	{
		method_5();
	}

	protected override void Dispose(bool disposing)
	{
		if (disposing && icontainer_0 != null)
		{
			icontainer_0.Dispose();
		}
		base.Dispose(disposing);
	}

	private void InitializeComponent()
	{
		this.icontainer_0 = new System.ComponentModel.Container();
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(IBrainChrom2018.FrmOperLog));
		this.toolStrip1 = new System.Windows.Forms.ToolStrip();
		this.toolStripButton11 = new System.Windows.Forms.ToolStripButton();
		this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
		this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
		this.toolStripButton3 = new System.Windows.Forms.ToolStripButton();
		this.toolStripButton2 = new System.Windows.Forms.ToolStripButton();
		this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
		this.toolStripButton4 = new System.Windows.Forms.ToolStripButton();
		this.toolStripButton5 = new System.Windows.Forms.ToolStripButton();
		this.toolStripButton6 = new System.Windows.Forms.ToolStripButton();
		this.toolStripButton7 = new System.Windows.Forms.ToolStripButton();
		this.toolStripButton8 = new System.Windows.Forms.ToolStripButton();
		this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
		this.toolStripButton9 = new System.Windows.Forms.ToolStripButton();
		this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
		this.toolStripButton10 = new System.Windows.Forms.ToolStripButton();
		this.dataGridView1 = new System.Windows.Forms.DataGridView();
		this.tabControl1 = new System.Windows.Forms.TabControl();
		this.tabPage1 = new System.Windows.Forms.TabPage();
		this.tabPage2 = new System.Windows.Forms.TabPage();
		this.dataGridView2 = new System.Windows.Forms.DataGridView();
		this.imageList_0 = new System.Windows.Forms.ImageList(this.icontainer_0);
		this.timer_0 = new System.Windows.Forms.Timer(this.icontainer_0);
		this.printDocument_0 = new System.Drawing.Printing.PrintDocument();
		this.prtPrvDlg = new System.Windows.Forms.PrintPreviewDialog();
		this.printDialog_0 = new System.Windows.Forms.PrintDialog();
		this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
		this.button1 = new System.Windows.Forms.Button();
		this.toolStrip1.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.dataGridView1).BeginInit();
		this.tabControl1.SuspendLayout();
		this.tabPage1.SuspendLayout();
		this.tabPage2.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.dataGridView2).BeginInit();
		base.SuspendLayout();
		this.toolStrip1.ImageScalingSize = new System.Drawing.Size(32, 32);
		this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[15]
		{
			this.toolStripButton11, this.toolStripButton1, this.toolStripSeparator1, this.toolStripButton3, this.toolStripButton2, this.toolStripSeparator2, this.toolStripButton4, this.toolStripButton5, this.toolStripButton6, this.toolStripButton7,
			this.toolStripButton8, this.toolStripSeparator3, this.toolStripButton9, this.toolStripSeparator4, this.toolStripButton10
		});
		this.toolStrip1.Location = new System.Drawing.Point(0, 0);
		this.toolStrip1.Name = "toolStrip1";
		this.toolStrip1.Size = new System.Drawing.Size(726, 39);
		this.toolStrip1.TabIndex = 0;
		this.toolStrip1.Text = "toolStrip1";
		this.toolStripButton11.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
		this.toolStripButton11.Image = (System.Drawing.Image)resources.GetObject("toolStripButton11.Image");
		this.toolStripButton11.ImageTransparentColor = System.Drawing.Color.Magenta;
		this.toolStripButton11.Name = "toolStripButton11";
		this.toolStripButton11.Size = new System.Drawing.Size(36, 36);
		this.toolStripButton11.Text = "退出";
		this.toolStripButton11.Visible = false;
		this.toolStripButton11.Click += new System.EventHandler(toolStripButton11_Click);
		this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
		this.toolStripButton1.Image = (System.Drawing.Image)resources.GetObject("toolStripButton1.Image");
		this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
		this.toolStripButton1.Name = "toolStripButton1";
		this.toolStripButton1.Size = new System.Drawing.Size(36, 36);
		this.toolStripButton1.Text = "实时刷新";
		this.toolStripButton1.Click += new System.EventHandler(toolStripButton1_Click);
		this.toolStripSeparator1.Name = "toolStripSeparator1";
		this.toolStripSeparator1.Size = new System.Drawing.Size(6, 39);
		this.toolStripButton3.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
		this.toolStripButton3.Image = (System.Drawing.Image)resources.GetObject("toolStripButton3.Image");
		this.toolStripButton3.ImageTransparentColor = System.Drawing.Color.Magenta;
		this.toolStripButton3.Name = "toolStripButton3";
		this.toolStripButton3.Size = new System.Drawing.Size(36, 36);
		this.toolStripButton3.Text = "打印设置";
		this.toolStripButton3.Click += new System.EventHandler(toolStripButton3_Click);
		this.toolStripButton2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
		this.toolStripButton2.Image = (System.Drawing.Image)resources.GetObject("toolStripButton2.Image");
		this.toolStripButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
		this.toolStripButton2.Name = "toolStripButton2";
		this.toolStripButton2.Size = new System.Drawing.Size(36, 36);
		this.toolStripButton2.Text = "打印预览";
		this.toolStripButton2.Click += new System.EventHandler(toolStripButton2_Click);
		this.toolStripSeparator2.Name = "toolStripSeparator2";
		this.toolStripSeparator2.Size = new System.Drawing.Size(6, 39);
		this.toolStripButton4.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
		this.toolStripButton4.Image = (System.Drawing.Image)resources.GetObject("toolStripButton4.Image");
		this.toolStripButton4.ImageTransparentColor = System.Drawing.Color.Magenta;
		this.toolStripButton4.Name = "toolStripButton4";
		this.toolStripButton4.Size = new System.Drawing.Size(36, 36);
		this.toolStripButton4.Text = "系统日志";
		this.toolStripButton4.Click += new System.EventHandler(toolStripButton4_Click);
		this.toolStripButton5.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
		this.toolStripButton5.Image = (System.Drawing.Image)resources.GetObject("toolStripButton5.Image");
		this.toolStripButton5.ImageTransparentColor = System.Drawing.Color.Magenta;
		this.toolStripButton5.Name = "toolStripButton5";
		this.toolStripButton5.Size = new System.Drawing.Size(36, 36);
		this.toolStripButton5.Text = "方法日志";
		this.toolStripButton5.Click += new System.EventHandler(toolStripButton5_Click);
		this.toolStripButton6.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
		this.toolStripButton6.Image = (System.Drawing.Image)resources.GetObject("toolStripButton6.Image");
		this.toolStripButton6.ImageTransparentColor = System.Drawing.Color.Magenta;
		this.toolStripButton6.Name = "toolStripButton6";
		this.toolStripButton6.Size = new System.Drawing.Size(36, 36);
		this.toolStripButton6.Text = "谱图日志";
		this.toolStripButton6.Click += new System.EventHandler(toolStripButton6_Click);
		this.toolStripButton7.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
		this.toolStripButton7.Image = (System.Drawing.Image)resources.GetObject("toolStripButton7.Image");
		this.toolStripButton7.ImageTransparentColor = System.Drawing.Color.Magenta;
		this.toolStripButton7.Name = "toolStripButton7";
		this.toolStripButton7.Size = new System.Drawing.Size(36, 36);
		this.toolStripButton7.Text = "打印日志";
		this.toolStripButton7.Click += new System.EventHandler(toolStripButton7_Click);
		this.toolStripButton8.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
		this.toolStripButton8.Image = (System.Drawing.Image)resources.GetObject("toolStripButton8.Image");
		this.toolStripButton8.ImageTransparentColor = System.Drawing.Color.Magenta;
		this.toolStripButton8.Name = "toolStripButton8";
		this.toolStripButton8.Size = new System.Drawing.Size(36, 36);
		this.toolStripButton8.Text = "反控日志";
		this.toolStripButton8.Click += new System.EventHandler(toolStripButton8_Click);
		this.toolStripSeparator3.Name = "toolStripSeparator3";
		this.toolStripSeparator3.Size = new System.Drawing.Size(6, 39);
		this.toolStripButton9.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
		this.toolStripButton9.Image = (System.Drawing.Image)resources.GetObject("toolStripButton9.Image");
		this.toolStripButton9.ImageTransparentColor = System.Drawing.Color.Magenta;
		this.toolStripButton9.Name = "toolStripButton9";
		this.toolStripButton9.Size = new System.Drawing.Size(36, 36);
		this.toolStripButton9.Text = "搜索";
		this.toolStripButton9.Visible = false;
		this.toolStripButton9.Click += new System.EventHandler(toolStripButton9_Click);
		this.toolStripSeparator4.Name = "toolStripSeparator4";
		this.toolStripSeparator4.Size = new System.Drawing.Size(6, 39);
		this.toolStripSeparator4.Visible = false;
		this.toolStripButton10.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
		this.toolStripButton10.Enabled = false;
		this.toolStripButton10.Image = (System.Drawing.Image)resources.GetObject("toolStripButton10.Image");
		this.toolStripButton10.ImageTransparentColor = System.Drawing.Color.Magenta;
		this.toolStripButton10.Name = "toolStripButton10";
		this.toolStripButton10.Size = new System.Drawing.Size(36, 36);
		this.toolStripButton10.Text = "导出execl";
		this.toolStripButton10.Click += new System.EventHandler(toolStripButton10_Click);
		this.dataGridView1.AllowUserToAddRows = false;
		this.dataGridView1.AllowUserToDeleteRows = false;
		this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
		this.dataGridView1.Location = new System.Drawing.Point(3, 3);
		this.dataGridView1.Name = "dataGridView1";
		this.dataGridView1.ReadOnly = true;
		this.dataGridView1.RowTemplate.Height = 23;
		this.dataGridView1.Size = new System.Drawing.Size(712, 364);
		this.dataGridView1.TabIndex = 1;
		this.tabControl1.Alignment = System.Windows.Forms.TabAlignment.Bottom;
		this.tabControl1.Controls.Add(this.tabPage1);
		this.tabControl1.Controls.Add(this.tabPage2);
		this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
		this.tabControl1.Location = new System.Drawing.Point(0, 39);
		this.tabControl1.Name = "tabControl1";
		this.tabControl1.SelectedIndex = 0;
		this.tabControl1.Size = new System.Drawing.Size(726, 396);
		this.tabControl1.TabIndex = 2;
		this.tabControl1.SelectedIndexChanged += new System.EventHandler(tabControl1_SelectedIndexChanged);
		this.tabPage1.Controls.Add(this.dataGridView1);
		this.tabPage1.Location = new System.Drawing.Point(4, 4);
		this.tabPage1.Name = "tabPage1";
		this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
		this.tabPage1.Size = new System.Drawing.Size(718, 370);
		this.tabPage1.TabIndex = 0;
		this.tabPage1.Text = "当前日志";
		this.tabPage1.UseVisualStyleBackColor = true;
		this.tabPage2.Controls.Add(this.dataGridView2);
		this.tabPage2.Location = new System.Drawing.Point(4, 4);
		this.tabPage2.Name = "tabPage2";
		this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
		this.tabPage2.Size = new System.Drawing.Size(718, 370);
		this.tabPage2.TabIndex = 1;
		this.tabPage2.Text = "历史查询";
		this.tabPage2.UseVisualStyleBackColor = true;
		this.dataGridView2.AllowUserToAddRows = false;
		this.dataGridView2.AllowUserToDeleteRows = false;
		this.dataGridView2.Dock = System.Windows.Forms.DockStyle.Fill;
		this.dataGridView2.Location = new System.Drawing.Point(3, 3);
		this.dataGridView2.Name = "dataGridView2";
		this.dataGridView2.ReadOnly = true;
		this.dataGridView2.RowTemplate.Height = 23;
		this.dataGridView2.Size = new System.Drawing.Size(712, 364);
		this.dataGridView2.TabIndex = 2;
		this.imageList_0.ImageStream = (System.Windows.Forms.ImageListStreamer)resources.GetObject("imageList1.ImageStream");
		this.imageList_0.TransparentColor = System.Drawing.Color.Transparent;
		this.imageList_0.Images.SetKeyName(0, "1.bmp");
		this.imageList_0.Images.SetKeyName(1, "2.png");
		this.imageList_0.Images.SetKeyName(2, "3.bmp");
		this.imageList_0.Images.SetKeyName(3, "4.bmp");
		this.imageList_0.Images.SetKeyName(4, "5.bmp");
		this.imageList_0.Images.SetKeyName(5, "11.ico");
		this.imageList_0.Images.SetKeyName(6, "22.bmp");
		this.imageList_0.Images.SetKeyName(7, "33.ico");
		this.imageList_0.Images.SetKeyName(8, "44.gif");
		this.imageList_0.Images.SetKeyName(9, "55.jpg");
		this.imageList_0.Images.SetKeyName(10, "6.gif");
		this.imageList_0.Images.SetKeyName(11, "66.bmp");
		this.timer_0.Interval = 10000;
		this.timer_0.Tick += new System.EventHandler(timer_0_Tick);
		this.printDocument_0.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(printDocument_0_PrintPage);
		this.prtPrvDlg.AutoScrollMargin = new System.Drawing.Size(0, 0);
		this.prtPrvDlg.AutoScrollMinSize = new System.Drawing.Size(0, 0);
		this.prtPrvDlg.ClientSize = new System.Drawing.Size(400, 300);
		this.prtPrvDlg.Enabled = true;
		this.prtPrvDlg.Icon = (System.Drawing.Icon)resources.GetObject("prtPrvDlg.Icon");
		this.prtPrvDlg.Name = "prtPrvDlg";
		this.prtPrvDlg.Visible = false;
		this.printDialog_0.UseEXDialog = true;
		this.dateTimePicker1.Location = new System.Drawing.Point(440, 12);
		this.dateTimePicker1.Name = "dateTimePicker1";
		this.dateTimePicker1.Size = new System.Drawing.Size(113, 21);
		this.dateTimePicker1.TabIndex = 3;
		this.button1.Location = new System.Drawing.Point(559, 12);
		this.button1.Name = "button1";
		this.button1.Size = new System.Drawing.Size(75, 23);
		this.button1.TabIndex = 4;
		this.button1.Text = "查询";
		this.button1.UseVisualStyleBackColor = true;
		this.button1.Click += new System.EventHandler(button1_Click);
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(726, 435);
		base.Controls.Add(this.button1);
		base.Controls.Add(this.dateTimePicker1);
		base.Controls.Add(this.tabControl1);
		base.Controls.Add(this.toolStrip1);
		base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
		base.Name = "FrmOperLog";
		this.Text = "检查跟踪";
		base.FormClosing += new System.Windows.Forms.FormClosingEventHandler(FrmOperLog_FormClosing);
		base.Load += new System.EventHandler(FrmOperLog_Load);
		this.toolStrip1.ResumeLayout(false);
		this.toolStrip1.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.dataGridView1).EndInit();
		this.tabControl1.ResumeLayout(false);
		this.tabPage1.ResumeLayout(false);
		this.tabPage2.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.dataGridView2).EndInit();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
