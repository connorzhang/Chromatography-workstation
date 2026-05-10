using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Windows.Forms;

namespace IBrainChrom2018;

public class StationAdtTrlForm : LclGnlForm
{
	public StatAdtTrlRow[] adtTrls = new StatAdtTrlRow[0];

	private GvPrtInfos gvPrtInfos_0 = new GvPrtInfos();

	private int int_9;

	private int int_10;

	private int int_11 = 30;

	private int int_12;

	private ColumnsSetupDlg columnsSetupDlg_0 = new ColumnsSetupDlg("日志设置列", "Trail Setup Columns");

	private RptSetupDlg rptSetupDlg_0;

	private Font font_0;

	private Font font_1;

	private GvInfos gvInfos_0 = new GvInfos();

	public bool[] instruRecords = new bool[0];

	private int int_13 = 5;

	private int int_14 = 20;

	private float float_0;

	private StatAdtTrlRow[] statAdtTrlRow_0;

	private int int_15;

	private StatAdtTrlRow[] statAdtTrlRow_1;

	private int int_16 = 20;

	private SolidBrush solidBrush_0 = new SolidBrush(Color.Black);

	private StringFormat stringFormat_0 = new StringFormat();

	private bool bool_0;

	public string svFName = "";

	public bool sysRecords;

	private int int_17 = 30;

	private float float_1;

	private bool bool_1;

	private ToolStripButton btnOpen;

	private ToolStripButton btnPreview;

	private ToolStripButton btnPrint;

	private ToolStripButton btnProperties;

	private ToolStripButton btnShowInstru0;

	private ToolStripButton btnShowInstru1;

	private ToolStripButton btnShowInstru2;

	private ToolStripButton btnShowInstru3;

	private ToolStripButton btnShowSystem;

	private ContextMenuStrip cmsAdtTrl;

	private IContainer icontainer_2;

	private LclGridView gvAdtTrl;

	private ToolStripMenuItem toolStripMenuItem_0 = new ToolStripMenuItem();

	private ToolStripMenuItem miEdit;

	private ToolStripMenuItem miEdtColumnsSetup;

	private ToolStripMenuItem miEdtRestoreDftColumns;

	private ToolStripMenuItem miFiExit;

	private ToolStripMenuItem miFiExport;

	private ToolStripMenuItem miFile;

	private ToolStripMenuItem miFilter;

	private ToolStripMenuItem miFiOpen;

	private ToolStripMenuItem miFltShowAll;

	private ToolStripMenuItem miFltShowInstru0;

	private ToolStripMenuItem miFltShowInstru1;

	private ToolStripMenuItem miFltShowInstru2;

	private ToolStripMenuItem miFltShowInstru3;

	private ToolStripMenuItem miFltShowSystem;

	private ToolStripMenuItem miPrtHisRpts;

	private MenuStrip msAdtTrl;

	private OpenFileDialog openFileDialog_0;

	private Pen pen_0 = new Pen(Color.Black, 1f);

	private PrintDialog printDialog_0;

	private PrintDocument printDocument_0;

	private PrintPreviewDialog prtPrvDlg;

	private RectangleF rectangleF_0;

	private Rectangle rectangle_0;

	private RptSetup rptSetup_0;

	private StatusStrip ssAdtTrl;

	private ToolStripStatusLabel sslb1;

	private LclTabControl tcAdtTrl;

	private ToolStripSeparator toolStripSeparator2;

	private ToolStripSeparator toolStripSeparator3;

	private ToolStripSeparator toolStripSeparator4;

	private ToolStripSeparator toolStripSeparator5;

	private ToolStripSeparator toolStripSeparator6;

	private ToolStripSeparator toolStripSeparator7;

	private TabPage tpCurrent;

	private TabPage tpFile;

	private ToolStrip tsAdtTrl;

	private IContainer components;

	private string saraFile => Lang.PS("文件", "File");

	private string saraInstru => Lang.PS("仪器", "Instru.");

	private string saraSys => Lang.PS("系统", "System");

	private string srltFail => Lang.PS("失败", "Fail");

	private string srltOk => Lang.PS("成功", "Ok");

	public StationAdtTrlForm()
	{
		InitializeComponent();
		tpCurrent.Text = Lang.PS("当前日志", "Current Audit Trail");
		tpFile.Text = Lang.PS("文件", "File");
		sslb1.Text = Lang.PS("日志", "Station Audit Trail");
		btnOpen.Text = Lang.PS("打开日志", "Open Log");
		btnShowSystem.Text = Lang.PS("系统信息", "System Information");
		btnShowInstru0.Text = Lang.PS("仪器1", "Instrument1");
		btnShowInstru1.Text = Lang.PS("仪器2", "Instrument2");
		btnShowInstru2.Text = Lang.PS("仪器3", "Instrument3");
		btnShowInstru3.Text = Lang.PS("仪器4", "Instrument4");
		tcAdtTrl.Dock = DockStyle.Fill;
		tcAdtTrl.tabStyle = TabStyle.Special;
		toolStripMenuItem_0.Enabled = false;
		toolStripMenuItem_0.Click += btnProperties_Click;
		btnShowInstru0.Tag = miFltShowInstru0;
		btnShowInstru1.Tag = miFltShowInstru1;
		btnShowInstru2.Tag = miFltShowInstru2;
		btnShowInstru3.Tag = miFltShowInstru3;
		method_14();
		method_12(gvAdtTrl);
		miEdtRestoreDftColumns_Click(null, null);
		miFltShowAll_Click(null, null);
		btnShowSystem_Click(null, null);
	}

	private int method_0(int int_18)
	{
		return PrinterUnitConvert.Convert(int_18, PrinterUnit.Display, PrinterUnit.TenthsOfAMillimeter);
	}

	public StatAdtTrlRow AddTail(int pgNo, ATResult atResult, ATType atType, string analyst, string instruName, ATArea atArea, string descript)
	{
		StatAdtTrlRow statAdtTrlRow = new StatAdtTrlRow();
		statAdtTrlRow.pgNo = pgNo;
		statAdtTrlRow.atResult = atResult;
		statAdtTrlRow.atType = atType;
		statAdtTrlRow.analyst = analyst;
		statAdtTrlRow.instruName = instruName;
		statAdtTrlRow.atArea = atArea;
		statAdtTrlRow.descript = descript;
		Array.Resize(ref adtTrls, adtTrls.Length + 1);
		adtTrls[adtTrls.Length - 1] = statAdtTrlRow;
		StationAdtTrlForm_VisibleChanged(null, null);
		return statAdtTrlRow;
	}

	private void method_1()
	{
		btnShowInstru0.Checked = miFltShowInstru0.Checked;
		btnShowInstru1.Checked = miFltShowInstru1.Checked;
		btnShowInstru2.Checked = miFltShowInstru2.Checked;
		btnShowInstru3.Checked = miFltShowInstru3.Checked;
		StationAdtTrlForm_VisibleChanged(null, null);
	}

	private void btnPreview_Click(object sender, EventArgs e)
	{
		try
		{
			prtPrvDlg.ShowDialog();
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message);
			prtPrvDlg.Close();
		}
	}

	private void btnPrint_Click(object sender, EventArgs e)
	{
		if (printDialog_0.ShowDialog() != DialogResult.OK)
		{
			return;
		}
		if (!printDocument_0.PrinterSettings.IsValid)
		{
			MessageBox.Show(Lang.PS("打印机无效！", "Printer is not valid!"));
			return;
		}
		try
		{
			printDocument_0.Print();
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message);
		}
	}

	private bool method_2(Graphics graphics_0, string string_38, float float_2, out float float_3)
	{
		float_3 = graphics_0.MeasureString(string_38, font_1, rectangle_0.Width).Height;
		return float_2 + float_3 < (float)rectangle_0.Bottom;
	}

	public void CreateAndInit()
	{
		DirectoryInfo directoryInfo = new DirectoryInfo(Instrus_ATDirsDialog.instrus_ATDirs.dirAuditTrail);
		if (!directoryInfo.Exists)
		{
			directoryInfo.Create();
		}
		DateTime now = DateTime.Now;
		svFName = directoryInfo.FullName + "\\" + now.Year.ToString("0_") + now.Month.ToString("0_") + now.Day + ".log";
	}

	private bool method_3(Graphics graphics_0, ref float float_2)
	{
		if (gvPrtInfos_0.DimCount != 0 && statAdtTrlRow_1.Length != 0)
		{
			int num = statAdtTrlRow_1.Length;
			while (int_9 < gvPrtInfos_0.PartsNum && int_10 < num)
			{
				string[] array = gvPrtInfos_0.colNames[int_9];
				string[] array2 = new string[array.Length];
				StatAdtTrlRow statAdtTrlRow = statAdtTrlRow_1[int_10];
				Color color = Class49.GetColor(statAdtTrlRow.pgNo);
				for (int i = 0; i < array.Length; i++)
				{
					if (array[i] == "")
					{
						array2[i] = (int_10 + 1).ToString();
					}
					else if (array[i] == "Type")
					{
						array2[i] = statAdtTrlRow.pgNo + "," + statAdtTrlRow.atType;
					}
					else
					{
						array2[i] = gvValue(gvUse: false, statAdtTrlRow, array[i]).ToString();
					}
				}
				if (!method_4(graphics_0, gvPrtInfos_0, array2, num, ref int_9, ref int_10, ref float_2, color))
				{
					return false;
				}
			}
		}
		return true;
	}

	private bool method_4(Graphics graphics_0, GvPrtInfos gvPrtInfos_1, string[] string_38, int int_18, ref int int_19, ref int int_20, ref float float_2, Color color_0)
	{
		int partsNum = gvPrtInfos_1.PartsNum;
		bool flag = int_20 == int_18 - 1;
		string[] array = gvPrtInfos_1.colHdrTxts[int_19];
		float[] array2 = gvPrtInfos_1.colWidths[int_19];
		float num = gvPrtInfos_1.float_0[int_19];
		float float_3 = gvPrtInfos_1.float_1[int_19];
		if (int_20 == 0)
		{
			string text = "";
			if (partsNum != 1)
			{
				string[] array3 = new string[5]
				{
					" <",
					(int_19 + 1).ToString(),
					" / ",
					partsNum.ToString(),
					">"
				};
				text = string.Concat(array3);
			}
			float float_4 = 0f;
			if (text != "")
			{
				method_2(graphics_0, text, float_2, out float_4);
			}
			float num2 = float_2 + float_4 + (float)int_13 + 4f + float_0 + 4f + float_1;
			if (flag)
			{
				num2 += 2f;
			}
			if (num2 > (float)rectangle_0.Bottom)
			{
				return false;
			}
			if (text != "")
			{
				method_7(graphics_0, text, ref float_2);
			}
			method_5(graphics_0, bool_2: true, num, float_3, ref float_2);
			float num3 = num;
			for (int i = 0; i < array.Length; i++)
			{
				method_6(graphics_0, array[i], num3, float_2, array2[i], float_0);
				num3 += array2[i];
			}
			float_2 += float_0;
			method_5(graphics_0, bool_2: false, num, float_3, ref float_2);
			method_9(graphics_0, gvPrtInfos_1, string_38, int_19, num, ref float_2, color_0);
		}
		else
		{
			if (float_2 == (float)rectangle_0.Top)
			{
				method_5(graphics_0, bool_2: true, num, float_3, ref float_2);
				float num4 = num;
				for (int j = 0; j < array.Length; j++)
				{
					method_6(graphics_0, array[j], num4, float_2, array2[j], float_0);
					num4 += array2[j];
				}
				float_2 += float_0;
				method_5(graphics_0, bool_2: false, num, float_3, ref float_2);
			}
			float num5 = float_2 + float_1;
			if (flag)
			{
				num5 += 4f;
			}
			if (num5 > (float)rectangle_0.Bottom)
			{
				return false;
			}
			method_9(graphics_0, gvPrtInfos_1, string_38, int_19, num, ref float_2, color_0);
		}
		if (flag)
		{
			method_5(graphics_0, bool_2: true, num, float_3, ref float_2);
			if (int_19 < partsNum - 1)
			{
				float_2 += int_13;
			}
			int_19++;
			int_20 = 0;
		}
		else
		{
			int_20++;
		}
		return true;
	}

	private void method_5(Graphics graphics_0, bool bool_2, float float_2, float float_3, ref float float_4)
	{
		float_4 += 2f;
		int int_ = ((!bool_2) ? 1 : 2);
		method_8(graphics_0, float_2, float_4, float_3, int_);
		float_4 += 2f;
	}

	private void method_6(Graphics graphics_0, string string_38, float float_2, float float_3, float float_4, float float_5)
	{
		if (!bool_1)
		{
			rectangleF_0.X = float_2;
			rectangleF_0.Y = float_3;
			rectangleF_0.Width = float_4;
			rectangleF_0.Height = float_5;
			stringFormat_0.Alignment = StringAlignment.Center;
			graphics_0.DrawString(string_38, font_0, Brushes.Black, rectangleF_0, stringFormat_0);
		}
	}

	private bool method_7(Graphics graphics_0, string string_38, ref float float_2)
	{
		float float_3 = 0f;
		if (!method_2(graphics_0, string_38, float_2, out float_3))
		{
			return false;
		}
		method_10(graphics_0, string_38, rectangle_0.Left, float_2);
		float_2 += float_3;
		return true;
	}

	private void method_8(Graphics graphics_0, float float_2, float float_3, float float_4, int int_18)
	{
		if (!bool_1)
		{
			pen_0.Color = Color.Black;
			pen_0.Width = int_18;
			graphics_0.DrawLine(pen_0, float_2, float_3, float_2 + float_4, float_3);
			pen_0.Width = 1f;
		}
	}

	private void method_9(Graphics graphics_0, GvPrtInfos gvPrtInfos_1, string[] string_38, int int_18, float float_2, ref float float_3, Color color_0)
	{
		string[] array = gvPrtInfos_1.colNames[int_18];
		StringAlignment[] array2 = gvPrtInfos_1.colAligns[int_18];
		float[] array3 = gvPrtInfos_1.colWidths[int_18];
		float num = float_2;
		for (int i = 0; i < array.Length; i++)
		{
			StringAlignment stringAlignment_ = array2[i];
			if (array[i] != "Type")
			{
				if (!(array[i] == "") && !(array[i] == "Result") && !(array[i] == "Time"))
				{
					method_11(graphics_0, string_38[i], num, float_3, array3[i], stringAlignment_, color_0);
				}
				else
				{
					method_11(graphics_0, string_38[i], num, float_3, array3[i], stringAlignment_, Color.Black);
				}
			}
			else
			{
				string[] array4 = string_38[i].Split(',');
				int num2 = int.Parse(array4[0]);
				Bitmap bitmap = null;
				string text = array4[1];
				if (text != null && text != "StartSys" && text != "CloseSys")
				{
					switch (text)
					{
					default:
						if (text == "Print")
						{
							bitmap = SystemIconResource.smethod_35();
						}
						break;
					case "CloseInstru":
						bitmap = num2 switch
						{
							2 => SystemBitmapResource7.smethod_2(), 
							1 => SystemBitmapResource7.smethod_1(), 
							0 => SystemBitmapResource7.smethod_0(), 
							_ => SystemBitmapResource7.smethod_3(), 
						};
						break;
					case "OpenInstru":
						bitmap = SystemIconResource.smethod_31();
						break;
					case "OpenFile":
						break;
					}
				}
				if (bitmap != null)
				{
					float num3 = (array3[i] - (float)bitmap.Width) / 2f;
					graphics_0.DrawImage(bitmap, num + num3, float_3);
				}
			}
			num += array3[i];
		}
		float_3 += float_1;
	}

	private void method_10(Graphics graphics_0, string string_38, float float_2, float float_3)
	{
		if (!bool_1)
		{
			graphics_0.DrawString(string_38, font_1, Brushes.Black, float_2, float_3);
		}
	}

	private void method_11(Graphics graphics_0, string string_38, float float_2, float float_3, float float_4, StringAlignment stringAlignment_0, Color color_0)
	{
		if (!bool_1)
		{
			rectangleF_0.X = float_2;
			rectangleF_0.Y = float_3;
			rectangleF_0.Width = float_4;
			rectangleF_0.Height = float_1;
			stringFormat_0.Alignment = stringAlignment_0;
			solidBrush_0.Color = color_0;
			graphics_0.DrawString(string_38, font_1, solidBrush_0, rectangleF_0, stringFormat_0);
		}
	}

	private void method_12(LclGridView lclGridView_0)
	{
		for (int i = 0; i < lclGridView_0.ColumnCount; i++)
		{
			string name;
			switch (name = lclGridView_0.Columns[i].Name)
			{
			case "Result":
				lclGridView_0.Columns[i].HeaderText = Lang.PS("结果", "Result");
				break;
			case "Time":
				lclGridView_0.Columns[i].HeaderText = Lang.PS("时间", "Result");
				break;
			case "Type":
				lclGridView_0.Columns[i].HeaderText = Lang.PS("类", "Result");
				break;
			case "Analyst":
				lclGridView_0.Columns[i].HeaderText = Lang.PS("分析人", "Result");
				break;
			case "Instru":
				lclGridView_0.Columns[i].HeaderText = Lang.PS("仪器", "Result");
				break;
			case "Area":
				lclGridView_0.Columns[i].HeaderText = Lang.PS("对像", "Result");
				break;
			case "Descript":
				lclGridView_0.Columns[i].HeaderText = Lang.PS("描述", "Result");
				break;
			case "Version":
				lclGridView_0.Columns[i].HeaderText = Lang.PS("版本", "Result");
				break;
			}
		}
	}

	public object gvValue(bool gvUse, StatAdtTrlRow statAdtTrlRow_2, string columnName)
	{
		object obj = null;
		gvAdtTrl.ConvertValFmt(columnName);
		switch (columnName)
		{
		case "Result":
			obj = ((statAdtTrlRow_2.atResult == ATResult.Ok) ? srltOk : srltFail);
			break;
		case "Time":
			obj = statAdtTrlRow_2.atTime.ToLongDateString() + " " + statAdtTrlRow_2.atTime.ToLongTimeString();
			break;
		case "Type":
			if (gvUse)
			{
				obj = null;
			}
			break;
		case "Analyst":
			obj = statAdtTrlRow_2.analyst;
			break;
		case "Instru":
			obj = statAdtTrlRow_2.instruName;
			break;
		case "Area":
			switch (statAdtTrlRow_2.atArea)
			{
			case ATArea.Sys:
				obj = saraSys;
				break;
			case ATArea.Instru:
				obj = saraInstru;
				break;
			case ATArea.File:
				obj = saraFile;
				break;
			}
			break;
		case "Descript":
			obj = statAdtTrlRow_2.descript;
			break;
		case "Version":
			obj = statAdtTrlRow_2.version;
			break;
		}
		if (!gvUse && obj == null)
		{
			obj = "";
		}
		return obj;
	}

	private void method_13()
	{
		Class49.SetGridViewInfo(gvAdtTrl, ref gvInfos_0, null);
		for (int i = 0; i < gvInfos_0.colHdrTxts.Length; i++)
		{
			int num = 30;
			string text;
			switch (text = gvInfos_0.colNames[i])
			{
			case "Result":
				num = 40;
				break;
			case "Time":
				num = 155;
				break;
			case "Type":
				num = 20;
				break;
			case "Analyst":
				num = 90;
				break;
			case "Instru":
				num = 60;
				break;
			case "Area":
				num = 50;
				break;
			case "Descript":
				num = 200;
				break;
			case "Version":
				num = 60;
				break;
			}
			gvInfos_0.colWidths[i] = num;
		}
		method_15(ref gvPrtInfos_0);
	}

	private void method_14()
	{
		gvAdtTrl.textBox_dftAligement = StringAlignment.Near;
		gvAdtTrl.AddLclTextBoxColumn("Result", 40);
		gvAdtTrl.AddLclTextBoxColumn("Time", 155);
		gvAdtTrl.AddLclgvIconColumn("Type", 20);
		gvAdtTrl.AddLclTextBoxColumn("Analyst", 90);
		gvAdtTrl.AddLclTextBoxColumn("Instru", 60);
		gvAdtTrl.AddLclTextBoxColumn("Area", 50);
		gvAdtTrl.AddLclTextBoxColumn("Descript", 200);
		gvAdtTrl.AddLclTextBoxColumn("Version", 60);
	}

	private void method_15(ref GvPrtInfos gvPrtInfos_1)
	{
		if (gvInfos_0.ColCount == 0)
		{
			Array.Resize(ref gvPrtInfos_1.colNames, 0);
			Array.Resize(ref gvPrtInfos_1.colHdrTxts, 0);
			Array.Resize(ref gvPrtInfos_1.colAligns, 0);
			Array.Resize(ref gvPrtInfos_1.colWidths, 0);
			Array.Resize(ref gvPrtInfos_1.float_0, 0);
			Array.Resize(ref gvPrtInfos_1.float_1, 0);
			return;
		}
		float[] array = new float[gvInfos_0.ColCount];
		float num = 0f;
		for (int i = 0; i < gvInfos_0.ColCount; i++)
		{
			array[i] = gvInfos_0.colWidths[i];
			num += array[i];
		}
		float num2 = num / (float)(rectangle_0.Width - 45);
		int num3 = (int)Math.Floor(num2) + 1;
		float num4 = num / (float)num3;
		float num5 = ((float)(rectangle_0.Width - 45) - num4) / 2f;
		num4 += num5;
		int num6 = 0;
		int num7 = -1;
		for (int j = 0; j < gvInfos_0.ColCount; j++)
		{
			float num8 = array[j];
			if (num7 == -1 || num + num8 > 45f + num4)
			{
				num7++;
				if (num7 != 0)
				{
					gvPrtInfos_1.float_0[num7 - 1] = (float)rectangle_0.Left + ((float)rectangle_0.Width - num) / 2f;
					gvPrtInfos_1.float_1[num7 - 1] = num;
				}
				Array.Resize(ref gvPrtInfos_1.colNames, num7 + 1);
				Array.Resize(ref gvPrtInfos_1.colHdrTxts, num7 + 1);
				Array.Resize(ref gvPrtInfos_1.colAligns, num7 + 1);
				Array.Resize(ref gvPrtInfos_1.colWidths, num7 + 1);
				Array.Resize(ref gvPrtInfos_1.float_0, num7 + 1);
				Array.Resize(ref gvPrtInfos_1.float_1, num7 + 1);
				Array.Resize(ref gvPrtInfos_1.colNames[num7], 1);
				gvPrtInfos_1.colNames[num7][0] = "";
				Array.Resize(ref gvPrtInfos_1.colHdrTxts[num7], 1);
				gvPrtInfos_1.colHdrTxts[num7][0] = "";
				Array.Resize(ref gvPrtInfos_1.colAligns[num7], 1);
				gvPrtInfos_1.colAligns[num7][0] = StringAlignment.Center;
				Array.Resize(ref gvPrtInfos_1.colWidths[num7], 1);
				float[] array2 = gvPrtInfos_1.colWidths[num7];
				int num9 = 0;
				num6 = num9 + 1;
				num = (array2[num9] = 45f);
			}
			Array.Resize(ref gvPrtInfos_1.colNames[num7], num6 + 1);
			gvPrtInfos_1.colNames[num7][num6] = gvInfos_0.colNames[j];
			Array.Resize(ref gvPrtInfos_1.colHdrTxts[num7], num6 + 1);
			gvPrtInfos_1.colHdrTxts[num7][num6] = gvInfos_0.colHdrTxts[j];
			Array.Resize(ref gvPrtInfos_1.colAligns[num7], num6 + 1);
			gvPrtInfos_1.colAligns[num7][num6] = gvInfos_0.colAligns[j];
			Array.Resize(ref gvPrtInfos_1.colWidths[num7], num6 + 1);
			gvPrtInfos_1.colWidths[num7][num6++] = num8;
			num += num8;
		}
		gvPrtInfos_1.float_0[num7] = (float)rectangle_0.Left + ((float)rectangle_0.Width - num) / 2f;
		gvPrtInfos_1.float_1[num7] = num;
	}

	private void method_16(BinaryReader binaryReader_0)
	{
		while (binaryReader_0.BaseStream.Position < binaryReader_0.BaseStream.Length)
		{
			int num = statAdtTrlRow_0.Length;
			Array.Resize(ref statAdtTrlRow_0, num + binaryReader_0.ReadInt32());
			for (int i = num; i < statAdtTrlRow_0.Length; i++)
			{
				if (statAdtTrlRow_0[i] == null)
				{
					statAdtTrlRow_0[i] = new StatAdtTrlRow();
				}
				statAdtTrlRow_0[i].LoadFromFile(binaryReader_0);
			}
		}
	}

	public override void LoadLanguage()
	{
		base.LoadLanguage();
		Text = Lang.PS("工作站日志", "Station Audit Trail");
		miFile.Text = Lang.PS("文件", "File");
		miFiOpen.Text = Lang.PS("打开...", "Open...");
		miFiExit.Text = Lang.PS("退出", "Exit");
		miEdit.Text = Lang.PS("编辑", "Edit");
		miEdtColumnsSetup.Text = Lang.PS("列设置...", "Columns Setup...");
		miEdtRestoreDftColumns.Text = Lang.PS("恢复默认列设置", "Restore Default Columns");
		miFilter.Text = Lang.PS("筛选", "Filter");
		miFltShowAll.Text = Lang.PS("显示全部", "Show All");
		miFltShowInstru0.Text = Lang.PS("显示仪器 1", "Show Instru 1");
		miFltShowInstru1.Text = Lang.PS("显示仪器 2", "Show Instru 2");
		miFltShowInstru2.Text = Lang.PS("显示仪器 3", "Show Instru 3");
		miFltShowInstru3.Text = Lang.PS("显示仪器 4", "Show Instru 4");
		miFltShowSystem.Text = Lang.PS("系统信息", "Show System");
		toolStripMenuItem_0.Text = Lang.PS("属性", "Properties");
		btnPrint.Text = Lang.PS("打印", "Print");
		btnPreview.Text = Lang.PS("预览", "Preview");
	}

	private void btnProperties_Click(object sender, EventArgs e)
	{
	}

	private void miEdtColumnsSetup_Click(object sender, EventArgs e)
	{
		columnsSetupDlg_0.ShowDialog(gvAdtTrl);
	}

	private void miEdtRestoreDftColumns_Click(object sender, EventArgs e)
	{
		gvAdtTrl.ini_SetFirstVisibleColumn("Result");
		gvAdtTrl.ini_SetNextVisibleColumn("Time");
		gvAdtTrl.ini_SetNextVisibleColumn("Type");
		gvAdtTrl.ini_SetNextVisibleColumn("Analyst");
		gvAdtTrl.ini_SetNextVisibleColumn("Instru");
		gvAdtTrl.ini_SetNextVisibleColumn("Descript");
		gvAdtTrl.ini_FinishVisibleColumn();
	}

	private void btnOpen_Click(object sender, EventArgs e)
	{
		if (openFileDialog_0 == null)
		{
			openFileDialog_0 = new OpenFileDialog();
			openFileDialog_0.Title = Lang.PS("打开日志文件", "Open Audit Trail File");
			openFileDialog_0.Filter = Class49.MakeFileFilter(".log");
			openFileDialog_0.InitialDirectory = Instrus_ATDirsDialog.instrus_ATDirs.dirAuditTrail;
		}
		if (openFileDialog_0.ShowDialog() != DialogResult.OK)
		{
			return;
		}
		FileInfo fileInfo_ = null;
		FileStream fileStream_ = null;
		BinaryReader binaryReader_ = null;
		try
		{
			Class49.OpenBinaryReader(openFileDialog_0.FileName, out fileInfo_, out fileStream_, out binaryReader_);
			method_16(binaryReader_);
			if (tcAdtTrl.SelectedTab != tpFile)
			{
				tcAdtTrl.SelectedTab = tpFile;
			}
			else
			{
				StationAdtTrlForm_VisibleChanged(null, null);
			}
			string text = fileInfo_.Name.Remove(fileInfo_.Name.Length - fileInfo_.Extension.Length);
			tpFile.Text = tpFile.Text + ((tpFile.Text == Lang.PS("文件", "File")) ? ":" : ",") + text;
		}
		finally
		{
			Class49.FileStreamClose(ref fileStream_, ref binaryReader_);
		}
	}

	private void miFltShowAll_Click(object sender, EventArgs e)
	{
		miFltShowInstru3.Checked = true;
		miFltShowInstru2.Checked = true;
		miFltShowInstru1.Checked = true;
		miFltShowInstru0.Checked = true;
		method_1();
	}

	private void btnShowInstru3_Click(object sender, EventArgs e)
	{
		ToolStripMenuItem toolStripMenuItem = null;
		if (sender is ToolStripMenuItem)
		{
			toolStripMenuItem = sender as ToolStripMenuItem;
		}
		if (sender is ToolStripButton)
		{
			toolStripMenuItem = (sender as ToolStripButton).Tag as ToolStripMenuItem;
		}
		toolStripMenuItem.Checked = !toolStripMenuItem.Checked;
		method_1();
	}

	private void btnShowSystem_Click(object sender, EventArgs e)
	{
		miFltShowSystem.Checked = !miFltShowSystem.Checked;
		btnShowSystem.Checked = miFltShowSystem.Checked;
		StationAdtTrlForm_VisibleChanged(null, null);
	}

	protected override void miHpHelp_Click(object sender, EventArgs e)
	{
		Class49.smethod_32("日志");
	}

	private void miPrtHisRpts_Click(object sender, EventArgs e)
	{
		bool flag = false;
		for (int i = 0; i < gvAdtTrl.SelectedRows.Count; i++)
		{
			StatAdtTrlRow statAdtTrlRow = gvAdtTrl.SelectedRows[i].Tag as StatAdtTrlRow;
			if (statAdtTrlRow.atType != ATType.Print || statAdtTrlRow.atResult != ATResult.Ok)
			{
				continue;
			}
			if (1 == 0)
			{
				break;
			}
			if (rptSetupDlg_0 == null)
			{
				rptSetupDlg_0 = new RptSetupDlg(null);
				rptSetupDlg_0.cbmtdUse.Enabled = false;
				rptSetup_0 = new RptSetup();
				rptSetup_0.mtdUse = false;
			}
			if (rptSetupDlg_0.JustShow2(rptSetup_0) != DialogResult.OK)
			{
				break;
			}
			for (i = 0; i < gvAdtTrl.SelectedRows.Count; i++)
			{
				statAdtTrlRow = gvAdtTrl.SelectedRows[i].Tag as StatAdtTrlRow;
				if (statAdtTrlRow.atType == ATType.Print && statAdtTrlRow.atResult == ATResult.Ok)
				{
					InstruStyle instruStyle = SysCfgDlg.sysConfig.pageInstrus[statAdtTrlRow.pgNo].instruStyle;
					rptSetupDlg_0.Print(statAdtTrlRow.sTag.Split('*'), instruStyle);
				}
			}
			break;
		}
	}

	private void printDocument_0_BeginPrint(object sender, PrintEventArgs e)
	{
		rectangle_0.X = method_0(int_14);
		rectangle_0.Y = method_0(int_17);
		rectangle_0.Width = printDocument_0.DefaultPageSettings.Bounds.Width - method_0(int_16) - rectangle_0.Left;
		rectangle_0.Height = printDocument_0.DefaultPageSettings.Bounds.Height - method_0(int_11) - rectangle_0.Top;
		method_13();
		bool_1 = true;
		int_15 = 0;
		int_12 = 1;
		bool_0 = false;
		statAdtTrlRow_1 = new StatAdtTrlRow[gvAdtTrl.RowCount];
		for (int i = 0; i < statAdtTrlRow_1.Length; i++)
		{
			statAdtTrlRow_1[i] = gvAdtTrl.Rows[i].Tag as StatAdtTrlRow;
		}
		font_0 = (font_1 = Font);
	}

	private void printDocument_0_PrintPage(object sender, PrintPageEventArgs e)
	{
		while (true)
		{
			Graphics graphics = e.Graphics;
			if (!bool_0)
			{
				bool_0 = true;
				int_10 = 0;
				int_9 = 0;
				float_0 = (float_1 = graphics.MeasureString("中国", font_1).Height);
			}
			bool flag = false;
			float float_ = rectangle_0.Top;
			method_10(graphics, "日志", rectangle_0.Left, rectangle_0.Bottom);
			string string_ = int_12 + " / " + int_15;
			SizeF sizeF = graphics.MeasureString(string_, font_1);
			method_10(graphics, string_, (float)rectangle_0.Right - sizeF.Width, rectangle_0.Bottom);
			if (method_3(graphics, ref float_))
			{
				flag = true;
			}
			if (!bool_1)
			{
				break;
			}
			int_15++;
			if (flag)
			{
				bool_1 = false;
				int_12 = 1;
				bool_0 = false;
			}
		}
		e.HasMorePages = int_12++ < int_15;
	}

	public void RefreshMeanus(int instrusNum)
	{
		ToolStripMenuItem[] array = new ToolStripMenuItem[4] { miFltShowInstru0, miFltShowInstru1, miFltShowInstru2, miFltShowInstru3 };
		ToolStripButton[] array2 = new ToolStripButton[4] { btnShowInstru0, btnShowInstru1, btnShowInstru2, btnShowInstru3 };
		for (int i = 0; i < array.Length; i++)
		{
			ToolStripMenuItem obj = array[i];
			bool visible = (array2[i].Visible = i < instrusNum);
			obj.Visible = visible;
		}
	}

	private void method_17(BinaryWriter binaryWriter_0)
	{
		binaryWriter_0.Write(adtTrls.Length);
		for (int i = 0; i < adtTrls.Length; i++)
		{
			adtTrls[i].SaveToFile(binaryWriter_0);
		}
	}

	public void SaveToLog()
	{
		FileInfo fileInfo_ = null;
		FileStream fileStream_ = null;
		BinaryWriter binaryWriter_ = null;
		try
		{
			Class49.OpenBinaryWriter(svFName, out fileInfo_, out fileStream_, out binaryWriter_);
			binaryWriter_.Seek(0, SeekOrigin.End);
			method_17(binaryWriter_);
		}
		finally
		{
			Class49.FileStreamClose(ref fileStream_, ref binaryWriter_);
		}
	}

	public void Show(int pgInstruNo)
	{
		statAdtTrlRow_0 = new StatAdtTrlRow[0];
		tcAdtTrl.SelectedTab = tpCurrent;
		tpFile.Text = Lang.PS("文件", "File");
		Show();
		BringToFront();
	}

	private void StationAdtTrlForm_Load(object sender, EventArgs e)
	{
		gvAdtTrl.Location = tcAdtTrl.Location;
		gvAdtTrl.Size = tpCurrent.Size;
		gvAdtTrl.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
		gvAdtTrl.BorderStyle = BorderStyle.None;
		miFiExit.Click += base.miFiExit_Click;
		msAdtTrl.Items.Add(miView);
		miView.DropDownItems.Add(new ToolStripSeparator());
		miView.DropDownItems.Add(toolStripMenuItem_0);
		msAdtTrl.Items.Add(miHelp);
		base.Icon = SystemIconResource.smethod_5();
		ResourceImageLoad.SetCtrlBitmap(btnOpen, SystemIconResource.smethod_31());
		ResourceImageLoad.SetCtrlBitmap(btnPrint, SystemIconResource.smethod_35());
		ResourceImageLoad.SetCtrlBitmap(btnPreview, SystemIconResource.smethod_33());
		ResourceImageLoad.SetCtrlBitmap(btnShowSystem, SystemBitmapResource7.smethod_8());
		ResourceImageLoad.SetCtrlBitmap(btnShowInstru0, SystemBitmapResource7.smethod_4());
		ResourceImageLoad.SetCtrlBitmap(btnShowInstru1, SystemBitmapResource7.smethod_5());
		ResourceImageLoad.SetCtrlBitmap(btnShowInstru2, SystemBitmapResource7.smethod_6());
		ResourceImageLoad.SetCtrlBitmap(btnShowInstru3, SystemBitmapResource7.smethod_7());
		ResourceImageLoad.SetCtrlBitmap(btnProperties, SystemIconResource.smethod_57());
	}

	private void StationAdtTrlForm_VisibleChanged(object sender, EventArgs e)
	{
		if (!base.Visible)
		{
			return;
		}
		gvAdtTrl.RowCount = 0;
		StatAdtTrlRow[] array = adtTrls;
		if (tcAdtTrl.SelectedTab == tpFile)
		{
			array = statAdtTrlRow_0;
		}
		for (int num = array.Length - 1; num >= 0; num--)
		{
			if ((miFltShowSystem.Checked && array[num].atArea == ATArea.Sys) || (miFltShowInstru0.Checked && array[num].pgNo == 0) || (miFltShowInstru1.Checked && array[num].pgNo == 1) || (miFltShowInstru2.Checked && array[num].pgNo == 2) || (miFltShowInstru3.Checked && array[num].pgNo == 3))
			{
				int rowCount;
				gvAdtTrl.RowCount = (rowCount = gvAdtTrl.RowCount) + 1;
				int index = rowCount;
				StatAdtTrlRow statAdtTrlRow = array[num];
				gvAdtTrl.Rows[index].Tag = statAdtTrlRow;
				for (int i = 0; i < gvAdtTrl.ColumnCount; i++)
				{
					if (!gvAdtTrl.Columns[i].Visible)
					{
						continue;
					}
					string name = gvAdtTrl.Columns[i].Name;
					gvAdtTrl.Rows[index].Cells[i].Value = gvValue(gvUse: true, statAdtTrlRow, name);
					if (name == "Type")
					{
						if (statAdtTrlRow.atType == ATType.OpenInstru)
						{
							(gvAdtTrl.Rows[index].Cells[i] as LclgvIconCell).Img = SystemIconResource.smethod_31();
						}
						if (statAdtTrlRow.atType == ATType.CloseInstru)
						{
							(gvAdtTrl.Rows[index].Cells[i] as LclgvIconCell).Img = ((statAdtTrlRow.pgNo == 0) ? SystemBitmapResource7.smethod_0() : ((statAdtTrlRow.pgNo == 1) ? SystemBitmapResource7.smethod_1() : ((statAdtTrlRow.pgNo == 2) ? SystemBitmapResource7.smethod_2() : ((statAdtTrlRow.pgNo == 3) ? SystemBitmapResource7.smethod_3() : null))));
						}
						if (statAdtTrlRow.atType == ATType.Print)
						{
							(gvAdtTrl.Rows[index].Cells[i] as LclgvIconCell).Img = SystemIconResource.smethod_35();
						}
					}
					gvAdtTrl.Rows[index].Cells[i].Style.ForeColor = Color.Black;
					if ((statAdtTrlRow.atType == ATType.OpenInstru || statAdtTrlRow.atType == ATType.CloseInstru || statAdtTrlRow.atType == ATType.Print || statAdtTrlRow.atType == ATType.OpenFile) && name != "Result" && name != "Time" && name != "Type")
					{
						gvAdtTrl.Rows[index].Cells[i].Style.ForeColor = Class49.GetColor(array[num].pgNo);
					}
				}
			}
		}
	}

	protected override void Dispose(bool disposing)
	{
		if (disposing && icontainer_2 != null)
		{
			icontainer_2.Dispose();
		}
		base.Dispose(disposing);
	}

	private void InitializeComponent()
	{
		this.components = new System.ComponentModel.Container();
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(IBrainChrom2018.StationAdtTrlForm));
		this.msAdtTrl = new System.Windows.Forms.MenuStrip();
		this.miFile = new System.Windows.Forms.ToolStripMenuItem();
		this.miFiOpen = new System.Windows.Forms.ToolStripMenuItem();
		this.miFiExport = new System.Windows.Forms.ToolStripMenuItem();
		this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
		this.miFiExit = new System.Windows.Forms.ToolStripMenuItem();
		this.miEdit = new System.Windows.Forms.ToolStripMenuItem();
		this.miEdtColumnsSetup = new System.Windows.Forms.ToolStripMenuItem();
		this.miEdtRestoreDftColumns = new System.Windows.Forms.ToolStripMenuItem();
		this.miFilter = new System.Windows.Forms.ToolStripMenuItem();
		this.miFltShowAll = new System.Windows.Forms.ToolStripMenuItem();
		this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
		this.miFltShowInstru0 = new System.Windows.Forms.ToolStripMenuItem();
		this.miFltShowInstru1 = new System.Windows.Forms.ToolStripMenuItem();
		this.miFltShowInstru2 = new System.Windows.Forms.ToolStripMenuItem();
		this.miFltShowInstru3 = new System.Windows.Forms.ToolStripMenuItem();
		this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
		this.miFltShowSystem = new System.Windows.Forms.ToolStripMenuItem();
		this.tsAdtTrl = new System.Windows.Forms.ToolStrip();
		this.btnOpen = new System.Windows.Forms.ToolStripButton();
		this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
		this.btnPrint = new System.Windows.Forms.ToolStripButton();
		this.btnPreview = new System.Windows.Forms.ToolStripButton();
		this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
		this.btnShowSystem = new System.Windows.Forms.ToolStripButton();
		this.btnShowInstru0 = new System.Windows.Forms.ToolStripButton();
		this.btnShowInstru1 = new System.Windows.Forms.ToolStripButton();
		this.btnShowInstru2 = new System.Windows.Forms.ToolStripButton();
		this.btnShowInstru3 = new System.Windows.Forms.ToolStripButton();
		this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
		this.btnProperties = new System.Windows.Forms.ToolStripButton();
		this.ssAdtTrl = new System.Windows.Forms.StatusStrip();
		this.sslb1 = new System.Windows.Forms.ToolStripStatusLabel();
		this.tcAdtTrl = new IBrainChrom2018.LclTabControl();
		this.tpCurrent = new System.Windows.Forms.TabPage();
		this.tpFile = new System.Windows.Forms.TabPage();
		this.gvAdtTrl = new IBrainChrom2018.LclGridView();
		this.cmsAdtTrl = new System.Windows.Forms.ContextMenuStrip(this.components);
		this.miPrtHisRpts = new System.Windows.Forms.ToolStripMenuItem();
		this.printDialog_0 = new System.Windows.Forms.PrintDialog();
		this.printDocument_0 = new System.Drawing.Printing.PrintDocument();
		this.prtPrvDlg = new System.Windows.Forms.PrintPreviewDialog();
		this.msAdtTrl.SuspendLayout();
		this.tsAdtTrl.SuspendLayout();
		this.ssAdtTrl.SuspendLayout();
		this.tcAdtTrl.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.gvAdtTrl).BeginInit();
		this.cmsAdtTrl.SuspendLayout();
		base.SuspendLayout();
		this.msAdtTrl.Items.AddRange(new System.Windows.Forms.ToolStripItem[3] { this.miFile, this.miEdit, this.miFilter });
		this.msAdtTrl.Location = new System.Drawing.Point(0, 0);
		this.msAdtTrl.Name = "msAdtTrl";
		this.msAdtTrl.Size = new System.Drawing.Size(764, 25);
		this.msAdtTrl.TabIndex = 0;
		this.msAdtTrl.Text = "menuStrip1";
		this.miFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[4] { this.miFiOpen, this.miFiExport, this.toolStripSeparator2, this.miFiExit });
		this.miFile.Name = "miFile";
		this.miFile.Size = new System.Drawing.Size(53, 21);
		this.miFile.Text = "文件";
		this.miFiOpen.Name = "miFiOpen";
		this.miFiOpen.Size = new System.Drawing.Size(131, 22);
		this.miFiOpen.Text = "打开...";
		this.miFiOpen.Click += new System.EventHandler(btnOpen_Click);
		this.miFiExport.Enabled = false;
		this.miFiExport.Name = "miFiExport";
		this.miFiExport.Size = new System.Drawing.Size(131, 22);
		this.miFiExport.Text = "导出";
		this.miFiExport.Visible = false;
		this.toolStripSeparator2.Name = "toolStripSeparator2";
		this.toolStripSeparator2.Size = new System.Drawing.Size(128, 6);
		this.miFiExit.Name = "miFiExit";
		this.miFiExit.Size = new System.Drawing.Size(131, 22);
		this.miFiExit.Text = "退出";
		this.miEdit.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[2] { this.miEdtColumnsSetup, this.miEdtRestoreDftColumns });
		this.miEdit.Name = "miEdit";
		this.miEdit.Size = new System.Drawing.Size(56, 21);
		this.miEdit.Text = "编辑";
		this.miEdtColumnsSetup.Name = "miEdtColumnsSetup";
		this.miEdtColumnsSetup.Size = new System.Drawing.Size(221, 22);
		this.miEdtColumnsSetup.Text = "列设置...";
		this.miEdtColumnsSetup.Click += new System.EventHandler(miEdtColumnsSetup_Click);
		this.miEdtRestoreDftColumns.Name = "miEdtRestoreDftColumns";
		this.miEdtRestoreDftColumns.Size = new System.Drawing.Size(221, 22);
		this.miEdtRestoreDftColumns.Text = "恢复默认列设置";
		this.miEdtRestoreDftColumns.Click += new System.EventHandler(miEdtRestoreDftColumns_Click);
		this.miFilter.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[8] { this.miFltShowAll, this.toolStripSeparator3, this.miFltShowInstru0, this.miFltShowInstru1, this.miFltShowInstru2, this.miFltShowInstru3, this.toolStripSeparator4, this.miFltShowSystem });
		this.miFilter.Name = "miFilter";
		this.miFilter.Size = new System.Drawing.Size(62, 21);
		this.miFilter.Text = "筛选";
		this.miFltShowAll.Name = "miFltShowAll";
		this.miFltShowAll.Size = new System.Drawing.Size(175, 22);
		this.miFltShowAll.Text = "显示全部";
		this.miFltShowAll.Click += new System.EventHandler(miFltShowAll_Click);
		this.toolStripSeparator3.Name = "toolStripSeparator3";
		this.toolStripSeparator3.Size = new System.Drawing.Size(172, 6);
		this.miFltShowInstru0.Name = "miFltShowInstru0";
		this.miFltShowInstru0.Size = new System.Drawing.Size(175, 22);
		this.miFltShowInstru0.Text = "显示仪器 1";
		this.miFltShowInstru0.Click += new System.EventHandler(btnShowInstru3_Click);
		this.miFltShowInstru1.Name = "miFltShowInstru1";
		this.miFltShowInstru1.Size = new System.Drawing.Size(175, 22);
		this.miFltShowInstru1.Text = "显示仪器 2";
		this.miFltShowInstru1.Click += new System.EventHandler(btnShowInstru3_Click);
		this.miFltShowInstru2.Name = "miFltShowInstru2";
		this.miFltShowInstru2.Size = new System.Drawing.Size(175, 22);
		this.miFltShowInstru2.Text = "显示仪器 3";
		this.miFltShowInstru2.Click += new System.EventHandler(btnShowInstru3_Click);
		this.miFltShowInstru3.Name = "miFltShowInstru3";
		this.miFltShowInstru3.Size = new System.Drawing.Size(175, 22);
		this.miFltShowInstru3.Text = "显示仪器 4";
		this.miFltShowInstru3.Click += new System.EventHandler(btnShowInstru3_Click);
		this.toolStripSeparator4.Name = "toolStripSeparator4";
		this.toolStripSeparator4.Size = new System.Drawing.Size(172, 6);
		this.miFltShowSystem.Name = "miFltShowSystem";
		this.miFltShowSystem.Size = new System.Drawing.Size(175, 22);
		this.miFltShowSystem.Text = "系统信息";
		this.miFltShowSystem.Click += new System.EventHandler(btnShowSystem_Click);
		this.tsAdtTrl.Items.AddRange(new System.Windows.Forms.ToolStripItem[12]
		{
			this.btnOpen, this.toolStripSeparator5, this.btnPrint, this.btnPreview, this.toolStripSeparator6, this.btnShowSystem, this.btnShowInstru0, this.btnShowInstru1, this.btnShowInstru2, this.btnShowInstru3,
			this.toolStripSeparator7, this.btnProperties
		});
		this.tsAdtTrl.Location = new System.Drawing.Point(0, 25);
		this.tsAdtTrl.Name = "tsAdtTrl";
		this.tsAdtTrl.Size = new System.Drawing.Size(764, 25);
		this.tsAdtTrl.TabIndex = 1;
		this.tsAdtTrl.Text = "toolStrip1";
		this.btnOpen.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
		this.btnOpen.ImageTransparentColor = System.Drawing.Color.Magenta;
		this.btnOpen.Name = "btnOpen";
		this.btnOpen.Size = new System.Drawing.Size(23, 22);
		this.btnOpen.Text = "打开日志";
		this.btnOpen.Click += new System.EventHandler(btnOpen_Click);
		this.toolStripSeparator5.Name = "toolStripSeparator5";
		this.toolStripSeparator5.Size = new System.Drawing.Size(6, 25);
		this.btnPrint.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
		this.btnPrint.ImageTransparentColor = System.Drawing.Color.Magenta;
		this.btnPrint.Name = "btnPrint";
		this.btnPrint.Size = new System.Drawing.Size(23, 22);
		this.btnPrint.Text = "打印";
		this.btnPrint.Click += new System.EventHandler(btnPrint_Click);
		this.btnPreview.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
		this.btnPreview.ImageTransparentColor = System.Drawing.Color.Magenta;
		this.btnPreview.Name = "btnPreview";
		this.btnPreview.Size = new System.Drawing.Size(23, 22);
		this.btnPreview.Text = "预览";
		this.btnPreview.Click += new System.EventHandler(btnPreview_Click);
		this.toolStripSeparator6.Name = "toolStripSeparator6";
		this.toolStripSeparator6.Size = new System.Drawing.Size(6, 25);
		this.btnShowSystem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
		this.btnShowSystem.ImageTransparentColor = System.Drawing.Color.Magenta;
		this.btnShowSystem.Name = "btnShowSystem";
		this.btnShowSystem.Size = new System.Drawing.Size(23, 22);
		this.btnShowSystem.Text = "系统信息";
		this.btnShowSystem.Click += new System.EventHandler(btnShowSystem_Click);
		this.btnShowInstru0.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
		this.btnShowInstru0.ImageTransparentColor = System.Drawing.Color.Magenta;
		this.btnShowInstru0.Name = "btnShowInstru0";
		this.btnShowInstru0.Size = new System.Drawing.Size(23, 22);
		this.btnShowInstru0.Text = "仪器1";
		this.btnShowInstru0.Click += new System.EventHandler(btnShowInstru3_Click);
		this.btnShowInstru1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
		this.btnShowInstru1.ImageTransparentColor = System.Drawing.Color.Magenta;
		this.btnShowInstru1.Name = "btnShowInstru1";
		this.btnShowInstru1.Size = new System.Drawing.Size(23, 22);
		this.btnShowInstru1.Text = "仪器2";
		this.btnShowInstru1.Click += new System.EventHandler(btnShowInstru3_Click);
		this.btnShowInstru2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
		this.btnShowInstru2.ImageTransparentColor = System.Drawing.Color.Magenta;
		this.btnShowInstru2.Name = "btnShowInstru2";
		this.btnShowInstru2.Size = new System.Drawing.Size(23, 22);
		this.btnShowInstru2.Text = "仪器3";
		this.btnShowInstru2.Click += new System.EventHandler(btnShowInstru3_Click);
		this.btnShowInstru3.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
		this.btnShowInstru3.ImageTransparentColor = System.Drawing.Color.Magenta;
		this.btnShowInstru3.Name = "btnShowInstru3";
		this.btnShowInstru3.Size = new System.Drawing.Size(23, 22);
		this.btnShowInstru3.Text = "仪器4";
		this.btnShowInstru3.Click += new System.EventHandler(btnShowInstru3_Click);
		this.toolStripSeparator7.Name = "toolStripSeparator7";
		this.toolStripSeparator7.Size = new System.Drawing.Size(6, 25);
		this.btnProperties.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
		this.btnProperties.Enabled = false;
		this.btnProperties.ImageTransparentColor = System.Drawing.Color.Magenta;
		this.btnProperties.Name = "btnProperties";
		this.btnProperties.Size = new System.Drawing.Size(23, 22);
		this.btnProperties.Text = "属性";
		this.btnProperties.Click += new System.EventHandler(btnProperties_Click);
		this.ssAdtTrl.Items.AddRange(new System.Windows.Forms.ToolStripItem[1] { this.sslb1 });
		this.ssAdtTrl.Location = new System.Drawing.Point(0, 443);
		this.ssAdtTrl.Name = "ssAdtTrl";
		this.ssAdtTrl.Size = new System.Drawing.Size(764, 22);
		this.ssAdtTrl.TabIndex = 8;
		this.ssAdtTrl.Text = "statusStrip1";
		this.sslb1.Name = "sslb1";
		this.sslb1.Size = new System.Drawing.Size(32, 17);
		this.sslb1.Text = "日志";
		this.tcAdtTrl.Alignment = System.Windows.Forms.TabAlignment.Bottom;
		this.tcAdtTrl.Controls.Add(this.tpCurrent);
		this.tcAdtTrl.Controls.Add(this.tpFile);
		this.tcAdtTrl.ItemSize = new System.Drawing.Size(90, 19);
		this.tcAdtTrl.Location = new System.Drawing.Point(39, 73);
		this.tcAdtTrl.Name = "tcAdtTrl";
		this.tcAdtTrl.SelectedIndex = 0;
		this.tcAdtTrl.Size = new System.Drawing.Size(189, 140);
		this.tcAdtTrl.TabIndex = 10;
		this.tcAdtTrl.SelectedIndexChanged += new System.EventHandler(StationAdtTrlForm_VisibleChanged);
		this.tpCurrent.Location = new System.Drawing.Point(4, 4);
		this.tpCurrent.Name = "tpCurrent";
		this.tpCurrent.Size = new System.Drawing.Size(181, 113);
		this.tpCurrent.TabIndex = 0;
		this.tpCurrent.Text = "当前日志";
		this.tpCurrent.UseVisualStyleBackColor = true;
		this.tpFile.Location = new System.Drawing.Point(4, 4);
		this.tpFile.Name = "tpFile";
		this.tpFile.Size = new System.Drawing.Size(181, 113);
		this.tpFile.TabIndex = 1;
		this.tpFile.Text = "文件";
		this.tpFile.UseVisualStyleBackColor = true;
		this.gvAdtTrl.AllowUserToAddRows = false;
		this.gvAdtTrl.AllowUserToDeleteRows = false;
		this.gvAdtTrl.AllowUserToResizeRows = false;
		this.gvAdtTrl.BackgroundColor = System.Drawing.Color.AliceBlue;
		this.gvAdtTrl.CharacterHeaderColor = System.Drawing.Color.Black;
		this.gvAdtTrl.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
		this.gvAdtTrl.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
		this.gvAdtTrl.Location = new System.Drawing.Point(277, 77);
		this.gvAdtTrl.Name = "gvAdtTrl";
		this.gvAdtTrl.ReadOnly = true;
		this.gvAdtTrl.RowHeadersWidth = 25;
		this.gvAdtTrl.RowTemplate.Height = 16;
		this.gvAdtTrl.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
		this.gvAdtTrl.ShowCellToolTips = false;
		this.gvAdtTrl.Size = new System.Drawing.Size(197, 117);
		this.gvAdtTrl.TabIndex = 9;
		this.cmsAdtTrl.Enabled = false;
		this.cmsAdtTrl.Items.AddRange(new System.Windows.Forms.ToolStripItem[1] { this.miPrtHisRpts });
		this.cmsAdtTrl.Name = "cmsAdtTrl";
		this.cmsAdtTrl.Size = new System.Drawing.Size(149, 26);
		this.miPrtHisRpts.Enabled = false;
		this.miPrtHisRpts.Name = "miPrtHisRpts";
		this.miPrtHisRpts.Size = new System.Drawing.Size(148, 22);
		this.miPrtHisRpts.Text = "打印历史报告";
		this.miPrtHisRpts.Click += new System.EventHandler(miPrtHisRpts_Click);
		this.printDialog_0.Document = this.printDocument_0;
		this.printDialog_0.UseEXDialog = true;
		this.printDocument_0.BeginPrint += new System.Drawing.Printing.PrintEventHandler(printDocument_0_BeginPrint);
		this.printDocument_0.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(printDocument_0_PrintPage);
		this.prtPrvDlg.AutoScrollMargin = new System.Drawing.Size(0, 0);
		this.prtPrvDlg.AutoScrollMinSize = new System.Drawing.Size(0, 0);
		this.prtPrvDlg.ClientSize = new System.Drawing.Size(400, 300);
		this.prtPrvDlg.Document = this.printDocument_0;
		this.prtPrvDlg.Enabled = true;
		this.prtPrvDlg.Icon = (System.Drawing.Icon)resources.GetObject("prtPrvDlg.Icon");
		this.prtPrvDlg.Name = "prtPrvDlg";
		this.prtPrvDlg.Visible = false;
		base.ClientSize = new System.Drawing.Size(764, 465);
		base.Controls.Add(this.gvAdtTrl);
		base.Controls.Add(this.tcAdtTrl);
		base.Controls.Add(this.ssAdtTrl);
		base.Controls.Add(this.tsAdtTrl);
		base.Controls.Add(this.msAdtTrl);
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
		base.MainMenuStrip = this.msAdtTrl;
		base.Name = "StationAdtTrlForm";
		this.Text = "工作站日志";
		base.Load += new System.EventHandler(StationAdtTrlForm_Load);
		base.VisibleChanged += new System.EventHandler(StationAdtTrlForm_VisibleChanged);
		this.msAdtTrl.ResumeLayout(false);
		this.msAdtTrl.PerformLayout();
		this.tsAdtTrl.ResumeLayout(false);
		this.tsAdtTrl.PerformLayout();
		this.ssAdtTrl.ResumeLayout(false);
		this.ssAdtTrl.PerformLayout();
		this.tcAdtTrl.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.gvAdtTrl).EndInit();
		this.cmsAdtTrl.ResumeLayout(false);
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
