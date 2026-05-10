using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace IBrainChrom2018;

public class SeqAlyForm : LclGnlForm
{
	private CMS_InfoParasFMT cms_InfoParasFMT_0 = new CMS_InfoParasFMT();

	private int int_0;

	private int int_1;

	private Injection injection_0;

	private int int_2;

	private ColumnsSetupDlg columnsSetupDlg_0 = new ColumnsSetupDlg("设置列", "Setup Columns");

	private MtdSetupDlg mtdSetupDlg_0;

	private RptSetupDlg rptSetupDlg_0;

	private SeqAlyAdtTrlDlg seqAlyAdtTrlDlg_0;

	public SeqAlyOptDlg dlgSequAlyOptions = new SeqAlyOptDlg();

	private LclGridView lclGridView_0;

	private LclGridView gvGnlSeqAly;

	private LclGridView gvGpcSeqAly;

	private SeqAly seqAly_0 = new SeqAly();

	private OpenFileDialog openFileDialog_0 = new OpenFileDialog();

	private OpenFileDialog openFileDialog_1 = new OpenFileDialog();

	private OpenFileDialog openFileDialog_2 = new OpenFileDialog();

	private int int_3;

	private bool bool_0;

	private string string_53;

	private SaveFileDialog saveFileDialog_0 = new SaveFileDialog();

	private Class63 class63_0 = new Class63();

	private ChromFormInterface formMain_0;

	private Injection injection_1 = new Injection();

	private IContainer icontainer_2;

	private ToolStripMenuItem miEdit;

	private ToolStripMenuItem miEdtClearRunMarks;

	private ToolStripMenuItem miEdtColumnsSetup;

	private ToolStripMenuItem miEdtInsertLine;

	private ToolStripMenuItem miEdtInvertRunMarks;

	private ToolStripMenuItem miEdtResetStatus;

	private ToolStripMenuItem miEdtRestoreDftColumns;

	private ToolStripMenuItem miEdtRowsDown;

	private ToolStripMenuItem miEdtRowsUp;

	private ToolStripMenuItem miFiExit;

	private ToolStripMenuItem miFile;

	private ToolStripMenuItem miFiNew;

	private ToolStripMenuItem miFiOpen;

	private ToolStripMenuItem miFiSave;

	private ToolStripMenuItem miFiSaveAs;

	private ToolStripMenuItem miSeqAbortAcquisition;

	private ToolStripMenuItem miSeqCheck;

	private ToolStripMenuItem miSeqOptions;

	private ToolStripMenuItem miSeqPauseSequence;

	private ToolStripMenuItem miSeqRepeatInj;

	private ToolStripMenuItem miSeqResumeSequence;

	private ToolStripMenuItem miSeqRowKAlpha;

	private ToolStripMenuItem miSeqRowMethod;

	private ToolStripMenuItem miSeqRowReportSetup;

	private ToolStripMenuItem miSeqRunSequence;

	private ToolStripMenuItem miSeqSkipVial;

	private ToolStripMenuItem miSeqSnapshot;

	private ToolStripMenuItem miSeqStopAcquisition;

	private ToolStripMenuItem miSeqStopSequence;

	private ToolStripMenuItem miSequence;

	private ToolStripMenuItem toolStripMenuItem_0 = new ToolStripMenuItem();

	private MenuStrip msSequAly;

	private ToolStripStatusLabel slbExplain;

	private StatusStrip ssSequAly;

	private ToolStripSeparator toolStripSeparator1;

	private ToolStripSeparator toolStripSeparator10;

	private ToolStripSeparator toolStripSeparator11;

	private ToolStripSeparator toolStripSeparator3;

	private ToolStripSeparator toolStripSeparator8;

	private ToolStripSeparator toolStripSeparator9;

	private ToolStrip tsSequAly;

	private ToolStripButton btnNew;

	private ToolStripButton btnOpen;

	private ToolStripButton btnSave;

	private ToolStripSeparator toolStripSeparator2;

	private ToolStripButton btnInsertLine;

	private ToolStripButton btnRowsUp;

	private ToolStripButton btnRowsDown;

	private ToolStripSeparator toolStripSeparator4;

	private ToolStripButton btnRunSequence;

	private ToolStripButton btnPauseSequence;

	private ToolStripButton btnResumeSequence;

	private ToolStripButton btnStopSequence;

	private ToolStripSeparator toolStripSeparator5;

	private ToolStripButton btnRepeatInj;

	private ToolStripButton btnSkipVial;

	private ToolStripButton btnSnapshot;

	private ToolStripButton btnStopAcquisition;

	private ToolStripButton btnAbortAcquisition;

	private ToolStripSeparator toolStripSeparator6;

	private ToolStripButton btnOptions;

	private ToolStripSeparator toolStripSeparator7;

	private ToolStripButton btnRowMethod;

	private ToolStripButton btnRowReportSetup;

	private ToolStripButton btnKAlpha;

	private ToolStripButton btnCheck;

	private ToolStripButton toolStripButton1;

	public string CurFullName => string_53;

	public SeqAly CurSeqAly => seqAly_0;

	public static string sgvcInjVol => Lang.PS("体积\n", "Inj.Vol\n");

	public SeqAlyForm()
	{
		InitializeComponent();
		seqAlyAdtTrlDlg_0 = new SeqAlyAdtTrlDlg(instrument);
		class63_0.method_0(method_10);
	}

	public void Init(ChromFormInterface F)
	{
		formMain_0 = F;
	}

	public bool SetrunningInjInfo(Injection I)
	{
		if (I != null)
		{
			injection_1 = I;
			return true;
		}
		return false;
	}

	public static void _gvRefreshHeaders(LclGridView lclGridView_1)
	{
		for (int i = 0; i < lclGridView_1.ColumnCount; i++)
		{
			string name;
			switch (name = lclGridView_1.Columns[i].Name)
			{
			case "Status":
				lclGridView_1.Columns[i].HeaderText = Lang.PS("状态", "Sts.");
				break;
			case "Run":
				lclGridView_1.Columns[i].HeaderText = Lang.PS("运行", "Run");
				break;
			case "SV":
				lclGridView_1.Columns[i].HeaderText = Lang.PS("起始瓶", "SV");
				break;
			case "EV":
				lclGridView_1.Columns[i].HeaderText = Lang.PS("结束瓶", "EV");
				break;
			case "VI":
				lclGridView_1.Columns[i].HeaderText = Lang.PS("针数", "I/V");
				break;
			case "SampleID":
				lclGridView_1.Columns[i].HeaderText = Lang.PS("样品ID", "Sample ID");
				break;
			case "Sample":
				lclGridView_1.Columns[i].HeaderText = Lang.PS("样品", "Sample");
				break;
			case "Amount":
				lclGridView_1.Columns[i].HeaderText = Lang.PS("样品\n数量", "Sample\nAmount");
				break;
			case "ISTDAmount":
				lclGridView_1.Columns[i].HeaderText = Lang.PS("内标\n数量", "ISTD\nAmount");
				break;
			case "Dilution":
				lclGridView_1.Columns[i].HeaderText = Lang.PS("稀释", "Sample\nDilut");
				break;
			case "FileNameFMT":
				lclGridView_1.Columns[i].HeaderText = Lang.PS("文件名", "File Name");
				break;
			case "CaliStand":
				lclGridView_1.Columns[i].HeaderText = Lang.PS("校正\n标准", "Cali.\nStand");
				break;
			case "MethodName":
				lclGridView_1.Columns[i].HeaderText = Lang.PS("方法文件", "Method File");
				break;
			case "ReportStyle":
				lclGridView_1.Columns[i].HeaderText = Lang.PS("样式文件", "Style File");
				break;
			case "OpenChrom":
				lclGridView_1.Columns[i].HeaderText = Lang.PS("打开\n谱图", "Open\nChrom.");
				break;
			case "OpenCali":
				lclGridView_1.Columns[i].HeaderText = Lang.PS("打开\n校正", "Open\nCali.");
				break;
			case "Print":
				lclGridView_1.Columns[i].HeaderText = Lang.PS("打印", "Print");
				break;
			}
		}
	}

	public void GetDisColumns(ref GvInfos gvInfos)
	{
		Class49.SetGridViewInfo(lclGridView_0, ref gvInfos, null);
		for (int i = 0; i < gvInfos.colHdrTxts.Length; i++)
		{
			int num = 80;
			string text;
			switch (text = gvInfos.colNames[i])
			{
			case "Status":
			case "Run":
			case "SV":
			case "EV":
			case "VI":
			case "CaliStand":
			case "OpenChrom":
			case "OpenCali":
			case "Print":
				num = 45;
				break;
			case "SampleID":
			case "Sample":
			case "FileNameFMT":
			case "MethodName":
			case "ReportStyle":
				num = 115;
				break;
			}
			gvInfos.colWidths[i] = num;
		}
	}

	private void method_0(LclGridView lclGridView_1)
	{
		_gvRefreshHeaders(lclGridView_1);
		method_6();
	}

	private void gvGpcSeqAly_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
	{
		e.Cancel = method_4(bool_1: false);
	}

	private void gvGpcSeqAly_CellEndEdit(object sender, DataGridViewCellEventArgs e)
	{
		method_7(e.RowIndex, AccStyle.Write);
		method_7(e.RowIndex, AccStyle.Read);
	}

	public object gvValue(bool gvUse, Injection injection_2, string columnName)
	{
		object obj = null;
		string text = lclGridView_0.ConvertValFmt(columnName);
		switch (columnName)
		{
		case "Status":
			obj = null;
			break;
		case "Run":
			obj = (gvUse ? ((object)injection_2.bool_0) : (injection_2.bool_0 ? "√" : ""));
			break;
		case "SV":
			obj = (gvUse ? ((object)injection_2.startVial) : injection_2.startVial.ToString(text));
			break;
		case "EV":
			obj = (gvUse ? ((object)injection_2.endVial) : injection_2.endVial.ToString(text));
			break;
		case "VI":
			obj = (gvUse ? ((object)injection_2.vialInjs) : injection_2.vialInjs.ToString(text));
			break;
		case "SampleID":
			obj = injection_2.sampleID;
			break;
		case "Sample":
			obj = injection_2.sample;
			break;
		case "Amount":
			obj = (gvUse ? ((object)injection_2.amount) : injection_2.amount.ToString(text));
			break;
		case "ISTDAmount":
			obj = (gvUse ? ((object)injection_2.ISTD_amount) : injection_2.ISTD_amount.ToString(text));
			break;
		case "Dilution":
			obj = (gvUse ? ((object)injection_2.dilution) : injection_2.dilution.ToString(text));
			break;
		case "InjVol":
			obj = (gvUse ? ((object)injection_2.inj_volume) : injection_2.inj_volume.ToString(text));
			break;
		case "K":
			obj = (gvUse ? ((object)injection_2.gpc_k) : injection_2.gpc_k.ToString(text));
			break;
		case "Alpha":
			obj = (gvUse ? ((object)injection_2.gpc_alpha) : injection_2.gpc_alpha.ToString(text));
			break;
		case "FileNameFMT":
			obj = injection_2.fileNameFMT;
			break;
		case "CaliStand":
			obj = (gvUse ? ((object)injection_2.cali_stand) : (injection_2.cali_stand ? "√" : ""));
			break;
		case "MethodName":
			obj = injection_2.methodFileName;
			break;
		case "ReportStyle":
			obj = injection_2.reportStyleFileName;
			break;
		case "OpenChrom":
			obj = (gvUse ? ((object)injection_2.openChromWin) : (injection_2.openChromWin ? "√" : ""));
			break;
		case "OpenCali":
			obj = (gvUse ? ((object)injection_2.openCaliWin) : (injection_2.openCaliWin ? "√" : ""));
			break;
		case "Print":
			obj = (gvUse ? ((object)injection_2.openPrintWin) : (injection_2.openPrintWin ? "√" : ""));
			break;
		}
		if (!gvUse && obj == null)
		{
			obj = "";
		}
		return obj;
	}

	private void method_1()
	{
		gvGnlSeqAly.AddLclgvSeqStatusColumn("Status", 35).Frozen = true;
		gvGnlSeqAly.AddLclCheckBoxColumn("Run", 30);
		gvGnlSeqAly.AddLclTextBoxColumn("SV", 30, 0, StringAlignment.Center);
		gvGnlSeqAly.AddLclTextBoxColumn("EV", 30, 0, StringAlignment.Center);
		gvGnlSeqAly.AddLclTextBoxColumn("VI", 30, 0, StringAlignment.Center);
		gvGnlSeqAly.AddLclTextBoxCtxBtnColumn("SampleID", 80, cms_InfoParasFMT_0);
		gvGnlSeqAly.AddLclTextBoxCtxBtnColumn("Sample", 90, cms_InfoParasFMT_0);
		gvGnlSeqAly.AddLclTextBoxColumn("Amount", 50);
		gvGnlSeqAly.AddLclTextBoxColumn("ISTDAmount", 50);
		gvGnlSeqAly.AddLclTextBoxColumn("Dilution", 50);
		gvGnlSeqAly.AddLclTextBoxColumn("InjVol", 50);
		gvGnlSeqAly.AddLclTextBoxCtxBtnColumn("FileNameFMT", 110, cms_InfoParasFMT_0);
		gvGnlSeqAly.AddLclCheckBoxColumn("CaliStand", 40);
		gvGnlSeqAly.AddLclTextBoxCtxBtnColumn("MethodName", 90, openFileDialog_0);
		gvGnlSeqAly.AddLclTextBoxCtxBtnColumn("ReportStyle", 90, openFileDialog_1);
		gvGnlSeqAly.AddLclCheckBoxColumn("OpenChrom", 35);
		gvGnlSeqAly.AddLclCheckBoxColumn("OpenCali", 35);
		gvGnlSeqAly.AddLclCheckBoxColumn("Print", 35);
		gvGpcSeqAly.AddLclgvSeqStatusColumn("Status", 35).Frozen = true;
		gvGpcSeqAly.AddLclCheckBoxColumn("Run", 30);
		gvGpcSeqAly.AddLclTextBoxColumn("SV", 30, 0);
		gvGpcSeqAly.AddLclTextBoxColumn("EV", 30, 0);
		gvGpcSeqAly.AddLclTextBoxColumn("VI", 30, 0);
		gvGpcSeqAly.AddLclTextBoxCtxBtnColumn("SampleID", 80, cms_InfoParasFMT_0);
		gvGpcSeqAly.AddLclTextBoxCtxBtnColumn("Sample", 90, cms_InfoParasFMT_0);
		gvGpcSeqAly.AddLclTextBoxColumn("Amount", 50);
		gvGpcSeqAly.AddLclTextBoxColumn("ISTDAmount", 50);
		gvGpcSeqAly.AddLclTextBoxColumn("Dilution", 50);
		gvGpcSeqAly.AddLclTextBoxColumn("InjVol", 50);
		gvGpcSeqAly.AddLclTextBoxColumn("K", 75, 2).HeaderText = "GPC K\n[dL/g*10^3]";
		gvGpcSeqAly.AddLclTextBoxColumn("Alpha", 60).HeaderText = "GPC\nAlpha";
		gvGpcSeqAly.AddLclTextBoxCtxBtnColumn("FileNameFMT", 110, cms_InfoParasFMT_0);
		gvGpcSeqAly.AddLclCheckBoxColumn("CaliStand", 40);
		gvGpcSeqAly.AddLclTextBoxCtxBtnColumn("MethodName", 90, openFileDialog_0);
		gvGpcSeqAly.AddLclTextBoxCtxBtnColumn("ReportStyle", 90, openFileDialog_1);
		gvGpcSeqAly.AddLclCheckBoxColumn("OpenChrom", 35);
		gvGpcSeqAly.AddLclCheckBoxColumn("OpenCali", 35);
		gvGpcSeqAly.AddLclCheckBoxColumn("Print", 35);
	}

	public override void LoadLanguage()
	{
		base.LoadLanguage();
		miFile.Text = Lang.PS("文件", "File");
		miFiNew.Text = Lang.PS("新建", "New");
		miFiOpen.Text = Lang.PS("打开...", "Open...");
		miFiSave.Text = Lang.PS("保存", "Save");
		miFiSaveAs.Text = Lang.PS("另存...", "Save as...");
		miFiExit.Text = Lang.PS("退出", "Exit");
		miEdit.Text = Lang.PS("编辑", "Edit");
		miEdtInsertLine.Text = Lang.PS("插入行", "Insert Line");
		miEdtRowsUp.Text = Lang.PS("提前行", "Rows Up");
		miEdtRowsDown.Text = Lang.PS("后退行", "Rows Down");
		miEdtClearRunMarks.Text = Lang.PS("清除运行标识", "Clear RunMarks");
		miEdtInvertRunMarks.Text = Lang.PS("置反运行标识", "Invert RunMarks");
		miEdtResetStatus.Text = Lang.PS("重置状态", "Reset Status");
		miEdtColumnsSetup.Text = Lang.PS("列设置...", "Columns Setup...");
		miEdtRestoreDftColumns.Text = Lang.PS("恢复默认列设置", "Restore Default Columns");
		miSequence.Text = Lang.PS("序列", "Sequence");
		miSeqRunSequence.Text = Lang.PS("运行序列", "Run Sequence");
		miSeqPauseSequence.Text = Lang.PS("暂停序列", "Pause Sequence");
		miSeqResumeSequence.Text = Lang.PS("继续序列", "Resume Sequence");
		miSeqStopSequence.Text = Lang.PS("停止序列", "Stop Sequence");
		miSeqRepeatInj.Text = Lang.PS("重复进样", "Repeat Inj.");
		miSeqSkipVial.Text = Lang.PS("跳过瓶", "Skip Vial");
		miSeqStopAcquisition.Text = Lang.PS("停止采集", "Stop Acquisition");
		miSeqAbortAcquisition.Text = Lang.PS("放弃采集", "Abort Acquisition");
		miSeqSnapshot.Text = Lang.PS("快照", "Snapshot");
		miSeqOptions.Text = Lang.PS("选项...", "Options...");
		miSeqRowMethod.Text = Lang.PS("行方法", "Row Method");
		miSeqRowReportSetup.Text = Lang.PS("行报告样式...", "Row ReportStyle...");
		miSeqRowKAlpha.Text = Lang.PS("行K,Alpha", "Row K,Alpha");
		miSeqCheck.Text = Lang.PS("检查", "Check");
		toolStripMenuItem_0.Text = Lang.PS("序列日志", "Sequence Audit Trail");
		btnNew.Text = miFiNew.Text;
		btnOpen.Text = miFiOpen.Text;
		btnSave.Text = miFiSave.Text;
		btnInsertLine.Text = miEdtInsertLine.Text;
		btnRowsUp.Text = miEdtRowsUp.Text;
		btnRowsDown.Text = miEdtRowsDown.Text;
		btnRunSequence.Text = miSeqRunSequence.Text;
		btnPauseSequence.Text = miSeqPauseSequence.Text;
		btnResumeSequence.Text = miSeqResumeSequence.Text;
		btnStopSequence.Text = miSeqStopSequence.Text;
		btnRepeatInj.Text = miSeqRepeatInj.Text;
		btnSkipVial.Text = miSeqSkipVial.Text;
		btnStopAcquisition.Text = miSeqStopAcquisition.Text;
		btnAbortAcquisition.Text = miSeqAbortAcquisition.Text;
		btnSnapshot.Text = miSeqSnapshot.Text;
		btnOptions.Text = miSeqOptions.Text;
		btnRowMethod.Text = miSeqRowMethod.Text;
		btnRowReportSetup.Text = miSeqRowReportSetup.Text;
		btnKAlpha.Text = miSeqRowKAlpha.Text;
		btnCheck.Text = miSeqCheck.Text;
		method_8();
		cms_InfoParasFMT_0.LoadLanguage();
	}

	private void miEdtClearRunMarks_Click(object sender, EventArgs e)
	{
		for (int i = 0; i < lclGridView_0.RowCount; i++)
		{
			Injection injection = lclGridView_0.Rows[i].Tag as Injection;
			injection.bool_0 = false;
			lclGridView_0.Rows[i].Cells["Run"].Value = false;
		}
	}

	private void miEdtColumnsSetup_Click(object sender, EventArgs e)
	{
		columnsSetupDlg_0.ShowDialog(lclGridView_0);
	}

	private void btnInsertLine_Click(object sender, EventArgs e)
	{
		if (lclGridView_0.Rows.Count <= 4 && !method_4(bool_1: true))
		{
			int num;
			if (lclGridView_0.SelectedRows != null && lclGridView_0.SelectedRows.Count != 0)
			{
				num = lclGridView_0.SelectedRows[0].Index;
				Injection injection = new Injection();
				injection.LoadFromObject(lclGridView_0.Rows[num].Tag as Injection);
				lclGridView_0.Rows.Insert(num, 1);
				lclGridView_0.Rows[num].Tag = injection;
			}
			else
			{
				num = lclGridView_0.Rows.Add();
				lclGridView_0.Rows[num].Tag = new Injection();
			}
			method_7(num, AccStyle.Read);
		}
	}

	private void miEdtInvertRunMarks_Click(object sender, EventArgs e)
	{
		for (int i = 0; i < lclGridView_0.RowCount; i++)
		{
			Injection injection = lclGridView_0.Rows[i].Tag as Injection;
			bool flag = (injection.bool_0 = !injection.bool_0);
			lclGridView_0.Rows[i].Cells["Run"].Value = flag;
		}
	}

	private void miEdtResetStatus_Click(object sender, EventArgs e)
	{
		for (int i = 0; i < lclGridView_0.RowCount; i++)
		{
			method_9(i, InjStatusMeasure.NoAnalysis);
		}
	}

	private void miEdtRestoreDftColumns_Click(object sender, EventArgs e)
	{
		if (instrument.instruStyle != InstruStyle.GPC)
		{
			gvGnlSeqAly.ini_SetFirstVisibleColumn("Run");
			gvGnlSeqAly.ini_SetNextVisibleColumn("SV");
			gvGnlSeqAly.ini_SetNextVisibleColumn("EV");
			gvGnlSeqAly.ini_SetNextVisibleColumn("VI");
			gvGnlSeqAly.ini_SetNextVisibleColumn("SampleID");
			gvGnlSeqAly.ini_SetNextVisibleColumn("Sample");
			gvGnlSeqAly.ini_SetNextVisibleColumn("Amount");
			gvGnlSeqAly.ini_SetNextVisibleColumn("ISTDAmount");
			gvGnlSeqAly.ini_SetNextVisibleColumn("Dilution");
			gvGnlSeqAly.ini_SetNextVisibleColumn("InjVol");
			gvGnlSeqAly.ini_SetNextVisibleColumn("CaliStand");
			gvGnlSeqAly.ini_SetNextVisibleColumn("MethodName");
			gvGnlSeqAly.ini_FinishVisibleColumn();
		}
		if (instrument.instruStyle == InstruStyle.GPC)
		{
			gvGpcSeqAly.ini_SetFirstVisibleColumn("Run");
			gvGpcSeqAly.ini_SetNextVisibleColumn("SV");
			gvGpcSeqAly.ini_SetNextVisibleColumn("EV");
			gvGpcSeqAly.ini_SetNextVisibleColumn("VI");
			gvGpcSeqAly.ini_SetNextVisibleColumn("SampleID");
			gvGpcSeqAly.ini_SetNextVisibleColumn("Sample");
			gvGpcSeqAly.ini_SetNextVisibleColumn("Amount");
			gvGpcSeqAly.ini_SetNextVisibleColumn("ISTDAmount");
			gvGpcSeqAly.ini_SetNextVisibleColumn("Dilution");
			gvGpcSeqAly.ini_SetNextVisibleColumn("InjVol");
			gvGpcSeqAly.ini_SetNextVisibleColumn("K");
			gvGpcSeqAly.ini_SetNextVisibleColumn("Alpha");
			gvGpcSeqAly.ini_SetNextVisibleColumn("CaliStand");
			gvGpcSeqAly.ini_SetNextVisibleColumn("MethodName");
			gvGpcSeqAly.ini_FinishVisibleColumn();
		}
	}

	private void btnRowsDown_Click(object sender, EventArgs e)
	{
		if (!method_4(bool_1: true))
		{
			int[] array = lclGridView_0.AdjustSelectedRows(AdjustUpDown.Down);
			for (int i = 0; i < array.Length; i++)
			{
				method_7(array[i], AccStyle.Read);
			}
		}
	}

	private void btnRowsUp_Click(object sender, EventArgs e)
	{
		if (!method_4(bool_1: true))
		{
			int[] array = lclGridView_0.AdjustSelectedRows(AdjustUpDown.Up);
			for (int i = 0; i < array.Length; i++)
			{
				method_7(array[i], AccStyle.Read);
			}
		}
	}

	public void miFiNew_Click(object sender, EventArgs e)
	{
		seqAly_0 = new SeqAly();
		method_5(AccStyle.Read);
		string_53 = null;
		method_8();
	}

	private void btnOpen_DisplayStyleChanged(object sender, EventArgs e)
	{
		if (openFileDialog_2.ShowDialog() == DialogResult.OK)
		{
			string_53 = openFileDialog_2.FileName;
			seqAly_0.LoadFromFile(string_53);
			method_8();
		}
		method_5(AccStyle.Read);
	}

	private void btnSave_Click(object sender, EventArgs e)
	{
		if (!method_4(bool_1: true) && lclGridView_0.RowCount != 0)
		{
			method_5(AccStyle.Write);
			method_5(AccStyle.Read);
			if (string_53 == null)
			{
				miFiSaveAs_Click(null, null);
			}
			else
			{
				seqAly_0.SaveToFile(string_53);
			}
		}
	}

	private void miFiSaveAs_Click(object sender, EventArgs e)
	{
		if (!method_4(bool_1: true) && saveFileDialog_0.ShowDialog() == DialogResult.OK)
		{
			string_53 = saveFileDialog_0.FileName;
			method_8();
			btnSave_Click(null, null);
		}
	}

	protected override void miHpHelp_Click(object sender, EventArgs e)
	{
		base.miHpHelp_Click(sender, e);
	}

	public void miSeqAbortAcquisition_Click(object sender, EventArgs e)
	{
		class63_0.method_2();
		injection_1.injStatus = InjStatusMeasure.MeasuredOut;
		formMain_0.StopGather();
		if (!btnResumeSequence.Visible)
		{
			if (bool_0)
			{
				method_9(int_1, InjStatusMeasure.MeasuredOut);
				return;
			}
			if (method_3())
			{
				method_11(seqAly_0.seqAlyOpt.idleTime);
				return;
			}
			miSeqStopSequence_Click(null, null);
			method_9(int_1, InjStatusMeasure.MeasuredOut);
		}
	}

	private void btnCheck_Click(object sender, EventArgs e)
	{
	}

	private void btnOptions_Click(object sender, EventArgs e)
	{
		if (dlgSequAlyOptions.ShowDialog(seqAly_0.seqAlyOpt, method_4(bool_1: false)) == DialogResult.OK)
		{
			method_6();
		}
	}

	public void miSeqPauseSequence_Click(object sender, EventArgs e)
	{
		ToolStripItem toolStripItem = miSeqPauseSequence;
		btnPauseSequence.Visible = false;
		toolStripItem.Visible = false;
		ToolStripItem toolStripItem2 = miSeqResumeSequence;
		btnResumeSequence.Visible = true;
		toolStripItem2.Visible = true;
		if (!instrument.sampling)
		{
			int_3 = Environment.TickCount;
		}
		else
		{
			int_3 = -1;
		}
		class63_0.method_2();
	}

	public void miSeqRepeatInj_Click(object sender, EventArgs e)
	{
		if (sender is ToolStripMenuItem)
		{
			ToolStripMenuItem toolStripMenuItem = sender as ToolStripMenuItem;
			toolStripMenuItem.Checked = !toolStripMenuItem.Checked;
			btnRepeatInj.Checked = toolStripMenuItem.Checked;
		}
		if (sender is ToolStripButton)
		{
			ToolStripButton toolStripButton = sender as ToolStripButton;
			toolStripButton.Checked = !toolStripButton.Checked;
			miSeqRepeatInj.Checked = toolStripButton.Checked;
		}
	}

	public void miSeqResumeSequence_Click(object sender, EventArgs e)
	{
		ToolStripItem toolStripItem = miSeqPauseSequence;
		btnPauseSequence.Visible = true;
		toolStripItem.Visible = true;
		ToolStripItem toolStripItem2 = miSeqResumeSequence;
		btnResumeSequence.Visible = false;
		toolStripItem2.Visible = false;
		if (instrument.sampling || bool_0)
		{
			return;
		}
		float num = 0f;
		if (int_3 > 0)
		{
			num = (float)(Environment.TickCount - int_3) / 60000f;
		}
		if (injection_1.injStatus == InjStatusMeasure.Prepared)
		{
			class63_0.method_4(seqAly_0.seqAlyOpt.idleTime - num);
		}
		else if (injection_1.injStatus == InjStatusMeasure.MeasuredOut)
		{
			if (method_3())
			{
				method_11(seqAly_0.seqAlyOpt.idleTime - num);
			}
			else
			{
				miSeqStopSequence_Click(null, null);
			}
		}
	}

	private void btnKAlpha_Click(object sender, EventArgs e)
	{
		Class49.kalphaDlg_0.ShowDialog();
	}

	private void btnRowMethod_Click(object sender, EventArgs e)
	{
		lclGridView_0.EndEdit();
		if (lclGridView_0.SelectedRows.Count != 1)
		{
			return;
		}
		int index = lclGridView_0.SelectedRows[0].Index;
		Injection injection = lclGridView_0.Rows[index].Tag as Injection;
		if (injection.methodFileName != "" && File.Exists(injection.methodFileName))
		{
			if (mtdSetupDlg_0 == null)
			{
				mtdSetupDlg_0 = new MtdSetupDlg(instrument);
				mtdSetupDlg_0.refresh_once();
			}
			mtdSetupDlg_0.Text = "方法: [" + injection.methodFileName + "]";
			MtdSetup mtdSetup = new MtdSetup();
			mtdSetup.LoadFromFile(injection.methodFileName);
			if (mtdSetupDlg_0.JustShow(mtdSetup, MtdDlgInitStyle.Calculation) == DialogResult.OK)
			{
				mtdSetup.SaveToFile(injection.methodFileName);
			}
		}
	}

	private void btnRowReportSetup_Click(object sender, EventArgs e)
	{
		lclGridView_0.EndEdit();
		if (lclGridView_0.SelectedRows.Count != 1)
		{
			return;
		}
		int index = lclGridView_0.SelectedRows[0].Index;
		Injection injection = lclGridView_0.Rows[index].Tag as Injection;
		if (injection.reportStyleFileName != "" && File.Exists(injection.reportStyleFileName))
		{
			if (rptSetupDlg_0 == null)
			{
				rptSetupDlg_0 = new RptSetupDlg(null);
			}
			rptSetupDlg_0.Text = Lang.PS("报告样式:") + " [" + injection.reportStyleFileName + "]";
			RptSetup rptSetup = new RptSetup();
			rptSetup.LoadFromFile(injection.reportStyleFileName);
			if (rptSetupDlg_0.JustShow(rptSetup) == DialogResult.OK)
			{
				rptSetup.SaveToFile(injection.reportStyleFileName);
			}
		}
	}

	public void miSeqRunSequence_Click(object sender, EventArgs e)
	{
		if (!formMain_0.StartBtEnable)
		{
			return;
		}
		class63_0.method_2();
		for (int i = 0; i < lclGridView_0.RowCount; i++)
		{
			method_9(i, InjStatusMeasure.NoAnalysis);
		}
		int_1 = -1;
		ToolStripItem toolStripItem = miSeqPauseSequence;
		btnPauseSequence.Enabled = false;
		toolStripItem.Enabled = false;
		btnRepeatInj.Checked = false;
		miSeqRepeatInj.Checked = false;
		bool_0 = false;
		TcpServerSocket currentTcpSocket = formMain_0.GetCurrentTcpSocket();
		if (currentTcpSocket != null)
		{
			int currentChannelIndex = formMain_0.CurrentChannelIndex;
			currentTcpSocket.sglsSampling[currentChannelIndex].runningInjInfo.injStatus = InjStatusMeasure.NoAnalysis;
		}
		if (method_3())
		{
			if (seqAly_0.seqAlyOpt.counter_resetStyle == CounterResetStyle.RunSequence)
			{
				seqAly_0.seqAlyOpt.counter_current = seqAly_0.seqAlyOpt.counter_start;
			}
			float float_ = 0f;
			if (seqAly_0.seqAlyOpt.idleBeforeFirstInj)
			{
				float_ = seqAly_0.seqAlyOpt.idleTime;
			}
			method_11(float_);
			ToolStripItem toolStripItem2 = miSeqRunSequence;
			btnRunSequence.Enabled = false;
			toolStripItem2.Enabled = false;
			ToolStripItem toolStripItem3 = miSeqPauseSequence;
			btnPauseSequence.Enabled = true;
			toolStripItem3.Enabled = true;
			ToolStripItem toolStripItem4 = miSeqStopSequence;
			btnStopSequence.Enabled = true;
			toolStripItem4.Enabled = true;
		}
		formMain_0.StartGather();
	}

	public void miSeqSkipVial_Click(object sender, EventArgs e)
	{
		if (instrument.sampling)
		{
			MessageBox.Show(Lang.PS("正在采集！", " is sampling!"));
			return;
		}
		class63_0.method_2();
		if (int_1 < 0)
		{
			return;
		}
		int_2++;
		if (int_2 <= injection_0.endVial)
		{
			int_0 = 0;
			method_11(seqAly_0.seqAlyOpt.idleTime);
			return;
		}
		method_9(int_1, InjStatusMeasure.MeasuredBySkip);
		for (int i = int_1 + 1; i < lclGridView_0.RowCount; i++)
		{
			if ((injection_0 = lclGridView_0.Rows[i].Tag as Injection).IsValid && injection_0.bool_0)
			{
				int_1 = i;
				int_2 = injection_0.startVial;
				int_0 = 0;
				method_11(seqAly_0.seqAlyOpt.idleTime);
				return;
			}
		}
		miSeqStopSequence_Click(null, null);
	}

	public void miSeqSnapshot_Click(object sender, EventArgs e)
	{
		TcpServerSocket currentTcpSocket = formMain_0.GetCurrentTcpSocket();
		if (currentTcpSocket == null)
		{
			return;
		}
		int currentChannelIndex = formMain_0.CurrentChannelIndex;
		if (gvGnlSeqAly.SelectedRows.Count == 1)
		{
			int index = gvGnlSeqAly.SelectedRows[0].Index;
			Injection injection = gvGnlSeqAly.Rows[index].Tag as Injection;
			currentTcpSocket.sglsSampling[currentChannelIndex].runningInjInfo.LoadFromObject(injection, Chrom: false, Cali: false, Print: false);
			string saveFilePath = "Projects\\Snapshot\\" + DateTime.Now.Date.ToString("yyyy-MM-dd") + "\\" + DateTime.Now.ToString("yyyy-MM-dd_HH_mm_ss") + ".sda";
			MtdSetup mtdSetup = new MtdSetup();
			if (injection.methodFileName != "")
			{
				mtdSetup.LoadFromFile(injection.methodFileName);
			}
			ChartParaOpera chartParaOpera = new ChartParaOpera();
			chartParaOpera.mtdMgr = mtdSetup;
			currentTcpSocket.Save(currentTcpSocket.sglsSampling[currentChannelIndex], saveFilePath, chartParaOpera, currentChannelIndex, "");
		}
	}

	public void miSeqStopAcquisition_Click(object sender, EventArgs e)
	{
		formMain_0.StopGather();
	}

	public void miSeqStopSequence_Click(object sender, EventArgs e)
	{
		class63_0.method_2();
		bool_0 = true;
		if (lclGridView_0 != null)
		{
			for (int i = 0; i < lclGridView_0.RowCount; i++)
			{
				if (i != int_1)
				{
					method_9(i, InjStatusMeasure.NoAnalysis);
				}
			}
		}
		ToolStripItem toolStripItem = miSeqRunSequence;
		btnRunSequence.Enabled = true;
		toolStripItem.Enabled = true;
		ToolStripItem toolStripItem2 = miSeqPauseSequence;
		btnPauseSequence.Enabled = false;
		toolStripItem2.Enabled = false;
		ToolStripItem toolStripItem3 = miSeqStopSequence;
		btnStopSequence.Enabled = false;
		toolStripItem3.Enabled = false;
		ToolStripItem toolStripItem4 = miSeqPauseSequence;
		btnPauseSequence.Visible = true;
		toolStripItem4.Visible = true;
		ToolStripItem toolStripItem5 = miSeqResumeSequence;
		btnResumeSequence.Visible = false;
		toolStripItem5.Visible = false;
		formMain_0.StopGather();
	}

	private void method_2(object sender, EventArgs e)
	{
		seqAlyAdtTrlDlg_0.ShowDialog();
	}

	public void OpenInstrumentResetCounter()
	{
		if (seqAly_0.seqAlyOpt.counter_resetStyle == CounterResetStyle.OpenInstrument)
		{
			seqAly_0.seqAlyOpt.counter_current = seqAly_0.seqAlyOpt.counter_start;
		}
	}

	private bool method_3()
	{
		lclGridView_0.EndEdit();
		if (int_1 < 0)
		{
			for (int i = 0; i < lclGridView_0.RowCount; i++)
			{
				injection_0 = lclGridView_0.Rows[i].Tag as Injection;
				if (injection_0.IsValid && injection_0.bool_0)
				{
					int_1 = i;
					int_2 = injection_0.startVial;
					int_0 = -1;
					if (int_1 < 0)
					{
						return false;
					}
					break;
				}
			}
		}
		if (injection_1.injStatus != InjStatusMeasure.NoAnalysis && btnRepeatInj.Checked)
		{
			return true;
		}
		int_0++;
		if (int_0 < injection_0.vialInjs)
		{
			return true;
		}
		int_2++;
		int_0 = 0;
		if (int_2 <= injection_0.endVial)
		{
			return true;
		}
		method_9(int_1, InjStatusMeasure.MeasuredOut);
		int num = int_1 + 1;
		for (int j = num; j < lclGridView_0.RowCount; j++)
		{
			if ((injection_0 = lclGridView_0.Rows[j].Tag as Injection).IsValid && injection_0.bool_0)
			{
				int_1 = j;
				int_2 = injection_0.startVial;
				return true;
			}
		}
		return false;
	}

	private bool method_4(bool bool_1)
	{
		return false;
	}

	public override void ReadWinInfo(WinInfo winInfo)
	{
		base.ReadWinInfo(winInfo);
		int gvNo = 0;
		winInfo.gvCF_w(gvGnlSeqAly, ref gvNo);
		winInfo.gvCF_w(gvGpcSeqAly, ref gvNo);
	}

	private void method_5(AccStyle accStyle_0)
	{
		switch (accStyle_0)
		{
		case AccStyle.Read:
		{
			lclGridView_0.SuspendLayout();
			lclGridView_0.RowCount = seqAly_0.seqInjs.Length;
			for (int j = 0; j < lclGridView_0.RowCount; j++)
			{
				if (lclGridView_0.Rows[j].Tag == null)
				{
					lclGridView_0.Rows[j].Tag = new Injection();
				}
				(lclGridView_0.Rows[j].Tag as Injection).LoadFromObject(seqAly_0.seqInjs[j]);
				method_7(j, AccStyle.Read);
			}
			lclGridView_0.ResumeLayout();
			break;
		}
		case AccStyle.Write:
		{
			seqAly_0.SetSeqInjsNum(lclGridView_0.RowCount);
			for (int i = 0; i < lclGridView_0.RowCount; i++)
			{
				method_7(i, AccStyle.Write);
				seqAly_0.seqInjs[i].LoadFromObject((Injection)lclGridView_0.Rows[i].Tag);
			}
			break;
		}
		}
	}

	private void method_6()
	{
		if (gvGnlSeqAly.Columns.Contains("InjVol"))
		{
			gvGnlSeqAly.Columns["InjVol"].HeaderText = Lang.PS("体积\n", "Inj.Vol\n") + "[" + seqAly_0.seqAlyOpt.injVolumnUnit.ToString() + "]";
		}
		if (gvGpcSeqAly.Columns.Contains("InjVol"))
		{
			gvGpcSeqAly.Columns["InjVol"].HeaderText = Lang.PS("体积\n", "Inj.Vol\n") + "[" + seqAly_0.seqAlyOpt.injVolumnUnit.ToString() + "]";
		}
	}

	private void method_7(int int_4, AccStyle accStyle_0)
	{
		Injection injection = lclGridView_0.Rows[int_4].Tag as Injection;
		switch (accStyle_0)
		{
		case AccStyle.Read:
		{
			for (int j = 0; j < lclGridView_0.ColumnCount; j++)
			{
				object obj = gvValue(gvUse: true, injection, lclGridView_0.Columns[j].Name);
				if (obj == null)
				{
					lclGridView_0.InvalidateCell(j, int_4);
				}
				else
				{
					lclGridView_0.Rows[int_4].Cells[j].Value = obj;
				}
			}
			break;
		}
		case AccStyle.Write:
		{
			for (int i = 0; i < lclGridView_0.ColumnCount; i++)
			{
				object value = lclGridView_0.Rows[int_4].Cells[i].Value;
				string name;
				switch (name = lclGridView_0.Columns[i].Name)
				{
				case "Run":
					injection.bool_0 = (bool)value;
					break;
				case "SV":
					injection.startVial = Class49.Object2Int(value, 1);
					break;
				case "EV":
					injection.endVial = Class49.Object2Int(value, 1);
					break;
				case "VI":
					injection.vialInjs = Class49.Object2Int(value, 1);
					break;
				case "SampleID":
					injection.sampleID = ((value != null) ? value.ToString() : "");
					break;
				case "Sample":
					injection.sample = ((value != null) ? value.ToString() : "");
					break;
				case "Amount":
					injection.amount = Class49.String2Float(value, 0f);
					break;
				case "ISTDAmount":
					injection.ISTD_amount = Class49.String2Float(value, 0f);
					break;
				case "Dilution":
					injection.dilution = Class49.String2Float(value, 1f);
					break;
				case "InjVol":
					injection.inj_volume = Class49.String2Float(value, 0f);
					break;
				case "K":
					injection.gpc_k = Class49.String2Float(value, 0f);
					break;
				case "Alpha":
					injection.gpc_alpha = Class49.String2Float(value, 0f);
					break;
				case "FileNameFMT":
					injection.fileNameFMT = ((value != null) ? value.ToString() : "");
					break;
				case "CaliStand":
					injection.cali_stand = (bool)value;
					break;
				case "MethodName":
					injection.methodFileName = ((value != null) ? value.ToString() : "");
					break;
				case "ReportStyle":
					injection.reportStyleFileName = ((value != null) ? value.ToString() : "");
					break;
				case "OpenChrom":
					injection.openChromWin = (bool)value;
					break;
				case "OpenCali":
					injection.openCaliWin = (bool)value;
					break;
				case "Print":
					injection.openPrintWin = (bool)value;
					break;
				}
			}
			break;
		}
		}
	}

	public override void refresh_once()
	{
		base.refresh_once();
		method_8();
		switch (instrument.instruStyle)
		{
		case InstruStyle.GC:
		case InstruStyle.LC:
		case InstruStyle.PDA:
		{
			ToolStripItem toolStripItem4 = miWinCaliGnl;
			mubtnCaliGnl.Visible = true;
			toolStripItem4.Visible = true;
			ToolStripItem toolStripItem5 = miWinCaliGpc;
			mubtnCaliGpc.Visible = false;
			toolStripItem5.Visible = false;
			ToolStripItem toolStripItem6 = btnKAlpha;
			miSeqRowKAlpha.Visible = false;
			toolStripItem6.Visible = false;
			gvGnlSeqAly.Visible = true;
			gvGpcSeqAly.Visible = false;
			lclGridView_0 = gvGnlSeqAly;
			break;
		}
		case InstruStyle.GPC:
		{
			ToolStripItem toolStripItem = miWinCaliGnl;
			mubtnCaliGnl.Visible = false;
			toolStripItem.Visible = false;
			ToolStripItem toolStripItem2 = miWinCaliGpc;
			mubtnCaliGpc.Visible = true;
			toolStripItem2.Visible = true;
			ToolStripItem toolStripItem3 = btnKAlpha;
			miSeqRowKAlpha.Visible = true;
			toolStripItem3.Visible = true;
			gvGnlSeqAly.Visible = false;
			gvGpcSeqAly.Visible = true;
			lclGridView_0 = gvGpcSeqAly;
			break;
		}
		}
		method_5(AccStyle.Read);
		if (base.Created && !lclGridView_0.LoadFromManager())
		{
			miEdtRestoreDftColumns_Click(null, null);
		}
	}

	private void method_8()
	{
		Text = Lang.PS("自动进样", "AutoInj. Analysis") + "[" + instrument.name + "] " + string_53;
	}

	private void method_9(int int_4, InjStatusMeasure injStatusMeasure_0)
	{
		if (int_4 >= 0 && int_4 < lclGridView_0.RowCount)
		{
			(lclGridView_0.Rows[int_4].Cells[0] as LclgvSeqStatusCell).injStatusMeasure = injStatusMeasure_0;
			lclGridView_0.InvalidateCell(0, int_4);
		}
	}

	private void SeqAlyForm_Load(object sender, EventArgs e)
	{
		miFiExit.Click += base.miFiExit_Click;
		base.Icon = SystemIconResource.smethod_16();
		Control control = gvGnlSeqAly;
		gvGpcSeqAly.Dock = DockStyle.Fill;
		control.Dock = DockStyle.Fill;
		DataGridView dataGridView = gvGnlSeqAly;
		gvGpcSeqAly.BorderStyle = BorderStyle.None;
		dataGridView.BorderStyle = BorderStyle.None;
		method_1();
		method_0(gvGnlSeqAly);
		method_0(gvGpcSeqAly);
		OpenFileDialog openFileDialog = openFileDialog_2;
		string filter = (saveFileDialog_0.Filter = Class49.MakeFileFilter(".seq"));
		openFileDialog.Filter = filter;
		openFileDialog_0.Title = "打开方法文件";
		openFileDialog_0.Filter = Class49.MakeFileFilter(".mtd");
		openFileDialog_1.Title = "打开样式文件";
		openFileDialog_1.Filter = Class49.MakeFileFilter(".sty");
		InstruWinsInfo instruWinsInfo = new InstruWinsInfo();
		if (instruWinsInfo.valid)
		{
			ReadWinInfo(instruWinsInfo.winInfos[1]);
		}
		for (int i = 0; i < 4; i++)
		{
			btnInsertLine_Click(null, null);
		}
		btnInsertLine.Visible = false;
		btnRowsUp.Visible = false;
		btnRowsDown.Visible = false;
		toolStripSeparator4.Visible = false;
		miEdtInsertLine.Visible = false;
		miEdtRowsUp.Visible = false;
		miEdtRowsDown.Visible = false;
		toolStripSeparator3.Visible = false;
	}

	public void refreshAutoAlyParaFromFM()
	{
		if (formMain_0.insDeviceCtrl.dgGramset.RowCount == 4)
		{
			for (int i = 0; i < 4; i++)
			{
				lclGridView_0.Rows[i].Cells["SV"].Value = formMain_0.insDeviceCtrl.dgGramset.Rows[i].Cells[0].Value;
				lclGridView_0.Rows[i].Cells["EV"].Value = formMain_0.insDeviceCtrl.dgGramset.Rows[i].Cells[1].Value;
				lclGridView_0.Rows[i].Cells["VI"].Value = formMain_0.insDeviceCtrl.dgGramset.Rows[i].Cells[3].Value;
			}
		}
	}

	public void Set3Buttons(bool enabled)
	{
		ToolStripItem toolStripItem = btnSnapshot;
		ToolStripItem toolStripItem2 = btnStopAcquisition;
		btnAbortAcquisition.Enabled = enabled;
		toolStripItem2.Enabled = enabled;
		toolStripItem.Enabled = enabled;
		ToolStripItem toolStripItem3 = miSeqSnapshot;
		ToolStripItem toolStripItem4 = miSeqStopAcquisition;
		miSeqAbortAcquisition.Enabled = enabled;
		toolStripItem4.Enabled = enabled;
		toolStripItem3.Enabled = enabled;
	}

	public override void SetProjectDir(string projectDir)
	{
		base.SetProjectDir(projectDir);
		openFileDialog_2.InitialDirectory = projectDir;
		saveFileDialog_0.InitialDirectory = projectDir;
		openFileDialog_0.InitialDirectory = projectDir;
		openFileDialog_1.InitialDirectory = projectDir;
	}

	private void tsSequAly_MouseEnter(object sender, EventArgs e)
	{
	}

	private void method_10()
	{
		if (!btnPauseSequence.Checked)
		{
			instrument.daf_BeginGather(sample: true, InjectStyle.Sequence);
			method_9(int_1, InjStatusMeasure.BeingMeasured);
			injection_1.injStatus = InjStatusMeasure.BeingMeasured;
			injection_1.dtAcquire = DateTime.Now;
			injection_1.analyst = instrument.user.u_name;
			instrument.form.SeqRefreshInfo();
			seqAly_0.seqAlyOpt.counter_current++;
		}
	}

	private void method_11(float float_0)
	{
		class63_0.method_2();
		instrument.ResetSglsSamplingOriDots(createDiskFile: false);
		if (btnPauseSequence.Checked)
		{
			return;
		}
		injection_1.Reset();
		for (int i = 0; i < lclGridView_0.ColumnCount; i++)
		{
			string name;
			if (lclGridView_0.Columns[i].Visible && (name = lclGridView_0.Columns[i].Name) != null && SystemDictionaryList.dictionary_30.TryGetValue(name, out var value))
			{
				switch (value)
				{
				case 0:
					injection_1.startVial = injection_0.startVial;
					break;
				case 1:
					injection_1.endVial = injection_0.endVial;
					break;
				case 2:
					injection_1.vialInjs = injection_0.vialInjs;
					break;
				case 3:
					injection_1.sampleID = injection_0.sampleID;
					break;
				case 4:
					injection_1.sample = injection_0.sample;
					break;
				case 5:
					injection_1.amount = injection_0.amount;
					break;
				case 6:
					injection_1.ISTD_amount = injection_0.ISTD_amount;
					break;
				case 7:
					injection_1.dilution = injection_0.dilution;
					break;
				case 8:
					injection_1.inj_volume = injection_0.inj_volume;
					break;
				case 9:
					injection_1.gpc_k = injection_0.gpc_k;
					break;
				case 10:
					injection_1.gpc_alpha = injection_0.gpc_alpha;
					break;
				case 11:
					injection_1.fileNameFMT = injection_0.fileNameFMT;
					break;
				case 12:
					injection_1.cali_stand = injection_0.cali_stand;
					break;
				case 13:
					injection_1.methodFileName = injection_0.methodFileName;
					break;
				case 14:
					injection_1.reportStyleFileName = injection_0.reportStyleFileName;
					break;
				case 15:
					injection_1.openChromWin = injection_0.openChromWin;
					break;
				case 16:
					injection_1.openCaliWin = injection_0.openCaliWin;
					break;
				case 17:
					injection_1.openPrintWin = injection_0.openPrintWin;
					break;
				}
			}
		}
		injection_1.vialNo = int_2;
		injection_1.injNo = int_0;
		injection_1.counter = seqAly_0.seqAlyOpt.counter_current;
		injection_1.injStatus = InjStatusMeasure.Prepared;
		if (!btnResumeSequence.Visible)
		{
			class63_0.method_4(float_0);
		}
	}

	public override void WriteWinInfo(WinInfo winInfo)
	{
		base.WriteWinInfo(winInfo);
		winInfo.gvCF_r(gvGnlSeqAly);
		winInfo.gvCF_r(gvGpcSeqAly);
	}

	private void toolStripButton1_Click(object sender, EventArgs e)
	{
		formMain_0.UpdateAutoSamplerState();
		refreshAutoAlyParaFromFM();
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(IBrainChrom2018.SeqAlyForm));
		this.msSequAly = new System.Windows.Forms.MenuStrip();
		this.miFile = new System.Windows.Forms.ToolStripMenuItem();
		this.miFiNew = new System.Windows.Forms.ToolStripMenuItem();
		this.miFiOpen = new System.Windows.Forms.ToolStripMenuItem();
		this.miFiSave = new System.Windows.Forms.ToolStripMenuItem();
		this.miFiSaveAs = new System.Windows.Forms.ToolStripMenuItem();
		this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
		this.miFiExit = new System.Windows.Forms.ToolStripMenuItem();
		this.miEdit = new System.Windows.Forms.ToolStripMenuItem();
		this.miEdtInsertLine = new System.Windows.Forms.ToolStripMenuItem();
		this.miEdtRowsUp = new System.Windows.Forms.ToolStripMenuItem();
		this.miEdtRowsDown = new System.Windows.Forms.ToolStripMenuItem();
		this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
		this.miEdtClearRunMarks = new System.Windows.Forms.ToolStripMenuItem();
		this.miEdtInvertRunMarks = new System.Windows.Forms.ToolStripMenuItem();
		this.miEdtResetStatus = new System.Windows.Forms.ToolStripMenuItem();
		this.toolStripSeparator8 = new System.Windows.Forms.ToolStripSeparator();
		this.miEdtColumnsSetup = new System.Windows.Forms.ToolStripMenuItem();
		this.miEdtRestoreDftColumns = new System.Windows.Forms.ToolStripMenuItem();
		this.miSequence = new System.Windows.Forms.ToolStripMenuItem();
		this.miSeqRunSequence = new System.Windows.Forms.ToolStripMenuItem();
		this.miSeqPauseSequence = new System.Windows.Forms.ToolStripMenuItem();
		this.miSeqResumeSequence = new System.Windows.Forms.ToolStripMenuItem();
		this.miSeqStopSequence = new System.Windows.Forms.ToolStripMenuItem();
		this.toolStripSeparator9 = new System.Windows.Forms.ToolStripSeparator();
		this.miSeqRepeatInj = new System.Windows.Forms.ToolStripMenuItem();
		this.miSeqSkipVial = new System.Windows.Forms.ToolStripMenuItem();
		this.miSeqSnapshot = new System.Windows.Forms.ToolStripMenuItem();
		this.miSeqStopAcquisition = new System.Windows.Forms.ToolStripMenuItem();
		this.miSeqAbortAcquisition = new System.Windows.Forms.ToolStripMenuItem();
		this.toolStripSeparator10 = new System.Windows.Forms.ToolStripSeparator();
		this.miSeqOptions = new System.Windows.Forms.ToolStripMenuItem();
		this.toolStripSeparator11 = new System.Windows.Forms.ToolStripSeparator();
		this.miSeqRowMethod = new System.Windows.Forms.ToolStripMenuItem();
		this.miSeqRowReportSetup = new System.Windows.Forms.ToolStripMenuItem();
		this.miSeqRowKAlpha = new System.Windows.Forms.ToolStripMenuItem();
		this.miSeqCheck = new System.Windows.Forms.ToolStripMenuItem();
		this.ssSequAly = new System.Windows.Forms.StatusStrip();
		this.slbExplain = new System.Windows.Forms.ToolStripStatusLabel();
		this.gvGnlSeqAly = new IBrainChrom2018.LclGridView();
		this.gvGpcSeqAly = new IBrainChrom2018.LclGridView();
		this.tsSequAly = new System.Windows.Forms.ToolStrip();
		this.btnNew = new System.Windows.Forms.ToolStripButton();
		this.btnOpen = new System.Windows.Forms.ToolStripButton();
		this.btnSave = new System.Windows.Forms.ToolStripButton();
		this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
		this.btnInsertLine = new System.Windows.Forms.ToolStripButton();
		this.btnRowsUp = new System.Windows.Forms.ToolStripButton();
		this.btnRowsDown = new System.Windows.Forms.ToolStripButton();
		this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
		this.btnRunSequence = new System.Windows.Forms.ToolStripButton();
		this.btnPauseSequence = new System.Windows.Forms.ToolStripButton();
		this.btnResumeSequence = new System.Windows.Forms.ToolStripButton();
		this.btnStopSequence = new System.Windows.Forms.ToolStripButton();
		this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
		this.btnRepeatInj = new System.Windows.Forms.ToolStripButton();
		this.btnSkipVial = new System.Windows.Forms.ToolStripButton();
		this.btnSnapshot = new System.Windows.Forms.ToolStripButton();
		this.btnStopAcquisition = new System.Windows.Forms.ToolStripButton();
		this.btnAbortAcquisition = new System.Windows.Forms.ToolStripButton();
		this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
		this.btnOptions = new System.Windows.Forms.ToolStripButton();
		this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
		this.btnRowMethod = new System.Windows.Forms.ToolStripButton();
		this.btnRowReportSetup = new System.Windows.Forms.ToolStripButton();
		this.btnKAlpha = new System.Windows.Forms.ToolStripButton();
		this.btnCheck = new System.Windows.Forms.ToolStripButton();
		this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
		this.msSequAly.SuspendLayout();
		this.ssSequAly.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.gvGnlSeqAly).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.gvGpcSeqAly).BeginInit();
		this.tsSequAly.SuspendLayout();
		base.SuspendLayout();
		this.msSequAly.Items.AddRange(new System.Windows.Forms.ToolStripItem[3] { this.miFile, this.miEdit, this.miSequence });
		this.msSequAly.Location = new System.Drawing.Point(0, 0);
		this.msSequAly.Name = "msSequAly";
		this.msSequAly.ShowItemToolTips = true;
		this.msSequAly.Size = new System.Drawing.Size(708, 25);
		this.msSequAly.TabIndex = 0;
		this.msSequAly.Text = "menuStrip1";
		this.miFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[6] { this.miFiNew, this.miFiOpen, this.miFiSave, this.miFiSaveAs, this.toolStripSeparator1, this.miFiExit });
		this.miFile.Name = "miFile";
		this.miFile.Size = new System.Drawing.Size(44, 21);
		this.miFile.Text = "文件";
		this.miFiNew.Name = "miFiNew";
		this.miFiNew.Size = new System.Drawing.Size(152, 22);
		this.miFiNew.Text = "新建";
		this.miFiNew.Click += new System.EventHandler(miFiNew_Click);
		this.miFiOpen.Name = "miFiOpen";
		this.miFiOpen.Size = new System.Drawing.Size(152, 22);
		this.miFiOpen.Text = "打开...";
		this.miFiOpen.Click += new System.EventHandler(btnOpen_DisplayStyleChanged);
		this.miFiSave.Name = "miFiSave";
		this.miFiSave.Size = new System.Drawing.Size(152, 22);
		this.miFiSave.Text = "保存";
		this.miFiSave.Click += new System.EventHandler(btnSave_Click);
		this.miFiSaveAs.Name = "miFiSaveAs";
		this.miFiSaveAs.Size = new System.Drawing.Size(152, 22);
		this.miFiSaveAs.Text = "另存...";
		this.miFiSaveAs.Click += new System.EventHandler(miFiSaveAs_Click);
		this.toolStripSeparator1.Name = "toolStripSeparator1";
		this.toolStripSeparator1.Size = new System.Drawing.Size(149, 6);
		this.miFiExit.Name = "miFiExit";
		this.miFiExit.Size = new System.Drawing.Size(152, 22);
		this.miFiExit.Text = "退出";
		this.miEdit.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[10] { this.miEdtInsertLine, this.miEdtRowsUp, this.miEdtRowsDown, this.toolStripSeparator3, this.miEdtClearRunMarks, this.miEdtInvertRunMarks, this.miEdtResetStatus, this.toolStripSeparator8, this.miEdtColumnsSetup, this.miEdtRestoreDftColumns });
		this.miEdit.Name = "miEdit";
		this.miEdit.Size = new System.Drawing.Size(44, 21);
		this.miEdit.Text = "编辑";
		this.miEdtInsertLine.Name = "miEdtInsertLine";
		this.miEdtInsertLine.Size = new System.Drawing.Size(160, 22);
		this.miEdtInsertLine.Text = "插入行";
		this.miEdtInsertLine.Click += new System.EventHandler(btnInsertLine_Click);
		this.miEdtRowsUp.Name = "miEdtRowsUp";
		this.miEdtRowsUp.Size = new System.Drawing.Size(160, 22);
		this.miEdtRowsUp.Text = "提前行";
		this.miEdtRowsUp.Click += new System.EventHandler(btnRowsUp_Click);
		this.miEdtRowsDown.Name = "miEdtRowsDown";
		this.miEdtRowsDown.Size = new System.Drawing.Size(160, 22);
		this.miEdtRowsDown.Text = "后退行";
		this.miEdtRowsDown.Click += new System.EventHandler(btnRowsDown_Click);
		this.toolStripSeparator3.Name = "toolStripSeparator3";
		this.toolStripSeparator3.Size = new System.Drawing.Size(157, 6);
		this.miEdtClearRunMarks.Name = "miEdtClearRunMarks";
		this.miEdtClearRunMarks.Size = new System.Drawing.Size(160, 22);
		this.miEdtClearRunMarks.Text = "清除运行标识";
		this.miEdtClearRunMarks.Click += new System.EventHandler(miEdtClearRunMarks_Click);
		this.miEdtInvertRunMarks.Name = "miEdtInvertRunMarks";
		this.miEdtInvertRunMarks.Size = new System.Drawing.Size(160, 22);
		this.miEdtInvertRunMarks.Text = "置反运行标识";
		this.miEdtInvertRunMarks.Click += new System.EventHandler(miEdtInvertRunMarks_Click);
		this.miEdtResetStatus.Name = "miEdtResetStatus";
		this.miEdtResetStatus.Size = new System.Drawing.Size(160, 22);
		this.miEdtResetStatus.Text = "重置状态";
		this.miEdtResetStatus.Click += new System.EventHandler(miEdtResetStatus_Click);
		this.toolStripSeparator8.Name = "toolStripSeparator8";
		this.toolStripSeparator8.Size = new System.Drawing.Size(157, 6);
		this.miEdtColumnsSetup.Name = "miEdtColumnsSetup";
		this.miEdtColumnsSetup.Size = new System.Drawing.Size(160, 22);
		this.miEdtColumnsSetup.Text = "列设置...";
		this.miEdtColumnsSetup.Click += new System.EventHandler(miEdtColumnsSetup_Click);
		this.miEdtRestoreDftColumns.Name = "miEdtRestoreDftColumns";
		this.miEdtRestoreDftColumns.Size = new System.Drawing.Size(160, 22);
		this.miEdtRestoreDftColumns.Text = "恢复默认列设置";
		this.miEdtRestoreDftColumns.Click += new System.EventHandler(miEdtRestoreDftColumns_Click);
		this.miSequence.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[17]
		{
			this.miSeqRunSequence, this.miSeqPauseSequence, this.miSeqResumeSequence, this.miSeqStopSequence, this.toolStripSeparator9, this.miSeqRepeatInj, this.miSeqSkipVial, this.miSeqSnapshot, this.miSeqStopAcquisition, this.miSeqAbortAcquisition,
			this.toolStripSeparator10, this.miSeqOptions, this.toolStripSeparator11, this.miSeqRowMethod, this.miSeqRowReportSetup, this.miSeqRowKAlpha, this.miSeqCheck
		});
		this.miSequence.Name = "miSequence";
		this.miSequence.Size = new System.Drawing.Size(44, 21);
		this.miSequence.Text = "序列";
		this.miSeqRunSequence.Name = "miSeqRunSequence";
		this.miSeqRunSequence.Size = new System.Drawing.Size(152, 22);
		this.miSeqRunSequence.Text = "运行序列";
		this.miSeqRunSequence.Click += new System.EventHandler(miSeqRunSequence_Click);
		this.miSeqPauseSequence.Name = "miSeqPauseSequence";
		this.miSeqPauseSequence.Size = new System.Drawing.Size(152, 22);
		this.miSeqPauseSequence.Text = "暂停序列";
		this.miSeqPauseSequence.Click += new System.EventHandler(miSeqPauseSequence_Click);
		this.miSeqResumeSequence.Name = "miSeqResumeSequence";
		this.miSeqResumeSequence.Size = new System.Drawing.Size(152, 22);
		this.miSeqResumeSequence.Text = "继续序列";
		this.miSeqResumeSequence.Click += new System.EventHandler(miSeqResumeSequence_Click);
		this.miSeqStopSequence.Name = "miSeqStopSequence";
		this.miSeqStopSequence.Size = new System.Drawing.Size(152, 22);
		this.miSeqStopSequence.Text = "停止序列";
		this.miSeqStopSequence.Click += new System.EventHandler(miSeqStopSequence_Click);
		this.toolStripSeparator9.Name = "toolStripSeparator9";
		this.toolStripSeparator9.Size = new System.Drawing.Size(149, 6);
		this.miSeqRepeatInj.Name = "miSeqRepeatInj";
		this.miSeqRepeatInj.Size = new System.Drawing.Size(152, 22);
		this.miSeqRepeatInj.Text = "重复进样";
		this.miSeqRepeatInj.Click += new System.EventHandler(miSeqRepeatInj_Click);
		this.miSeqSkipVial.Name = "miSeqSkipVial";
		this.miSeqSkipVial.Size = new System.Drawing.Size(152, 22);
		this.miSeqSkipVial.Text = "跳过瓶";
		this.miSeqSkipVial.Click += new System.EventHandler(miSeqSkipVial_Click);
		this.miSeqSnapshot.Name = "miSeqSnapshot";
		this.miSeqSnapshot.Size = new System.Drawing.Size(152, 22);
		this.miSeqSnapshot.Text = "快照";
		this.miSeqSnapshot.Click += new System.EventHandler(miSeqSnapshot_Click);
		this.miSeqStopAcquisition.Name = "miSeqStopAcquisition";
		this.miSeqStopAcquisition.Size = new System.Drawing.Size(152, 22);
		this.miSeqStopAcquisition.Text = "停止采集";
		this.miSeqStopAcquisition.Click += new System.EventHandler(miSeqStopAcquisition_Click);
		this.miSeqAbortAcquisition.Name = "miSeqAbortAcquisition";
		this.miSeqAbortAcquisition.Size = new System.Drawing.Size(152, 22);
		this.miSeqAbortAcquisition.Text = "放弃采集";
		this.miSeqAbortAcquisition.Click += new System.EventHandler(miSeqAbortAcquisition_Click);
		this.toolStripSeparator10.Name = "toolStripSeparator10";
		this.toolStripSeparator10.Size = new System.Drawing.Size(149, 6);
		this.miSeqOptions.Name = "miSeqOptions";
		this.miSeqOptions.Size = new System.Drawing.Size(152, 22);
		this.miSeqOptions.Text = "选项...";
		this.miSeqOptions.Click += new System.EventHandler(btnOptions_Click);
		this.toolStripSeparator11.Name = "toolStripSeparator11";
		this.toolStripSeparator11.Size = new System.Drawing.Size(149, 6);
		this.miSeqRowMethod.Name = "miSeqRowMethod";
		this.miSeqRowMethod.Size = new System.Drawing.Size(152, 22);
		this.miSeqRowMethod.Text = "行方法";
		this.miSeqRowMethod.Click += new System.EventHandler(btnRowMethod_Click);
		this.miSeqRowReportSetup.Name = "miSeqRowReportSetup";
		this.miSeqRowReportSetup.Size = new System.Drawing.Size(152, 22);
		this.miSeqRowReportSetup.Text = "行报告样式...";
		this.miSeqRowReportSetup.Click += new System.EventHandler(btnRowReportSetup_Click);
		this.miSeqRowKAlpha.Name = "miSeqRowKAlpha";
		this.miSeqRowKAlpha.Size = new System.Drawing.Size(152, 22);
		this.miSeqRowKAlpha.Text = "行K,Alpha";
		this.miSeqRowKAlpha.Click += new System.EventHandler(btnKAlpha_Click);
		this.miSeqCheck.Name = "miSeqCheck";
		this.miSeqCheck.Size = new System.Drawing.Size(152, 22);
		this.miSeqCheck.Text = "检查";
		this.miSeqCheck.Click += new System.EventHandler(btnCheck_Click);
		this.ssSequAly.Items.AddRange(new System.Windows.Forms.ToolStripItem[1] { this.slbExplain });
		this.ssSequAly.Location = new System.Drawing.Point(0, 297);
		this.ssSequAly.Name = "ssSequAly";
		this.ssSequAly.Size = new System.Drawing.Size(708, 22);
		this.ssSequAly.TabIndex = 7;
		this.ssSequAly.Text = "statusStrip1";
		this.slbExplain.Name = "slbExplain";
		this.slbExplain.Size = new System.Drawing.Size(0, 17);
		this.gvGnlSeqAly.AllowUserToAddRows = false;
		this.gvGnlSeqAly.AllowUserToDeleteRows = false;
		this.gvGnlSeqAly.AllowUserToResizeRows = false;
		this.gvGnlSeqAly.BackgroundColor = System.Drawing.Color.AliceBlue;
		this.gvGnlSeqAly.CharacterHeaderColor = System.Drawing.Color.Black;
		this.gvGnlSeqAly.ColumnHeadersHeight = 32;
		this.gvGnlSeqAly.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
		this.gvGnlSeqAly.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
		this.gvGnlSeqAly.Location = new System.Drawing.Point(111, 91);
		this.gvGnlSeqAly.Name = "gvGnlSeqAly";
		this.gvGnlSeqAly.RowHeadersWidth = 25;
		this.gvGnlSeqAly.RowTemplate.Height = 16;
		this.gvGnlSeqAly.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
		this.gvGnlSeqAly.ShowCellToolTips = false;
		this.gvGnlSeqAly.Size = new System.Drawing.Size(240, 150);
		this.gvGnlSeqAly.TabIndex = 8;
		this.gvGnlSeqAly.CellBeginEdit += new System.Windows.Forms.DataGridViewCellCancelEventHandler(gvGpcSeqAly_CellBeginEdit);
		this.gvGnlSeqAly.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(gvGpcSeqAly_CellEndEdit);
		this.gvGpcSeqAly.AllowUserToAddRows = false;
		this.gvGpcSeqAly.AllowUserToDeleteRows = false;
		this.gvGpcSeqAly.AllowUserToResizeRows = false;
		this.gvGpcSeqAly.BackgroundColor = System.Drawing.Color.AliceBlue;
		this.gvGpcSeqAly.CharacterHeaderColor = System.Drawing.Color.Black;
		this.gvGpcSeqAly.ColumnHeadersHeight = 32;
		this.gvGpcSeqAly.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
		this.gvGpcSeqAly.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
		this.gvGpcSeqAly.Location = new System.Drawing.Point(386, 91);
		this.gvGpcSeqAly.Name = "gvGpcSeqAly";
		this.gvGpcSeqAly.RowHeadersWidth = 25;
		this.gvGpcSeqAly.RowTemplate.Height = 16;
		this.gvGpcSeqAly.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
		this.gvGpcSeqAly.ShowCellToolTips = false;
		this.gvGpcSeqAly.Size = new System.Drawing.Size(240, 150);
		this.gvGpcSeqAly.TabIndex = 8;
		this.gvGpcSeqAly.CellBeginEdit += new System.Windows.Forms.DataGridViewCellCancelEventHandler(gvGpcSeqAly_CellBeginEdit);
		this.gvGpcSeqAly.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(gvGpcSeqAly_CellEndEdit);
		this.tsSequAly.Items.AddRange(new System.Windows.Forms.ToolStripItem[26]
		{
			this.btnNew, this.btnOpen, this.btnSave, this.toolStripSeparator2, this.btnInsertLine, this.btnRowsUp, this.btnRowsDown, this.toolStripSeparator4, this.btnRunSequence, this.btnPauseSequence,
			this.btnResumeSequence, this.btnStopSequence, this.toolStripSeparator5, this.btnRepeatInj, this.btnSkipVial, this.btnSnapshot, this.btnStopAcquisition, this.btnAbortAcquisition, this.toolStripSeparator6, this.btnOptions,
			this.toolStripSeparator7, this.btnRowMethod, this.btnRowReportSetup, this.btnKAlpha, this.btnCheck, this.toolStripButton1
		});
		this.tsSequAly.Location = new System.Drawing.Point(0, 25);
		this.tsSequAly.Name = "tsSequAly";
		this.tsSequAly.Size = new System.Drawing.Size(708, 25);
		this.tsSequAly.TabIndex = 9;
		this.tsSequAly.Text = "toolStrip1";
		this.tsSequAly.MouseEnter += new System.EventHandler(tsSequAly_MouseEnter);
		this.btnNew.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
		this.btnNew.Image = (System.Drawing.Image)resources.GetObject("btnNew.Image");
		this.btnNew.ImageTransparentColor = System.Drawing.Color.Magenta;
		this.btnNew.Name = "btnNew";
		this.btnNew.Size = new System.Drawing.Size(23, 22);
		this.btnNew.Text = "toolStripButton1";
		this.btnNew.Click += new System.EventHandler(miFiNew_Click);
		this.btnOpen.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
		this.btnOpen.Image = (System.Drawing.Image)resources.GetObject("btnOpen.Image");
		this.btnOpen.ImageTransparentColor = System.Drawing.Color.Magenta;
		this.btnOpen.Name = "btnOpen";
		this.btnOpen.Size = new System.Drawing.Size(23, 22);
		this.btnOpen.Text = "toolStripButton2";
		this.btnOpen.DisplayStyleChanged += new System.EventHandler(btnOpen_DisplayStyleChanged);
		this.btnSave.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
		this.btnSave.Image = (System.Drawing.Image)resources.GetObject("btnSave.Image");
		this.btnSave.ImageTransparentColor = System.Drawing.Color.Magenta;
		this.btnSave.Name = "btnSave";
		this.btnSave.Size = new System.Drawing.Size(23, 22);
		this.btnSave.Text = "toolStripButton3";
		this.btnSave.Click += new System.EventHandler(btnSave_Click);
		this.toolStripSeparator2.Name = "toolStripSeparator2";
		this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
		this.btnInsertLine.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
		this.btnInsertLine.Image = (System.Drawing.Image)resources.GetObject("btnInsertLine.Image");
		this.btnInsertLine.ImageTransparentColor = System.Drawing.Color.Magenta;
		this.btnInsertLine.Name = "btnInsertLine";
		this.btnInsertLine.Size = new System.Drawing.Size(23, 22);
		this.btnInsertLine.Text = "toolStripButton4";
		this.btnInsertLine.Click += new System.EventHandler(btnInsertLine_Click);
		this.btnRowsUp.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
		this.btnRowsUp.Image = (System.Drawing.Image)resources.GetObject("btnRowsUp.Image");
		this.btnRowsUp.ImageTransparentColor = System.Drawing.Color.Magenta;
		this.btnRowsUp.Name = "btnRowsUp";
		this.btnRowsUp.Size = new System.Drawing.Size(23, 22);
		this.btnRowsUp.Text = "toolStripButton5";
		this.btnRowsUp.Click += new System.EventHandler(btnRowsUp_Click);
		this.btnRowsDown.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
		this.btnRowsDown.Image = (System.Drawing.Image)resources.GetObject("btnRowsDown.Image");
		this.btnRowsDown.ImageTransparentColor = System.Drawing.Color.Magenta;
		this.btnRowsDown.Name = "btnRowsDown";
		this.btnRowsDown.Size = new System.Drawing.Size(23, 22);
		this.btnRowsDown.Text = "toolStripButton6";
		this.btnRowsDown.Click += new System.EventHandler(btnRowsDown_Click);
		this.toolStripSeparator4.Name = "toolStripSeparator4";
		this.toolStripSeparator4.Size = new System.Drawing.Size(6, 25);
		this.btnRunSequence.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
		this.btnRunSequence.Image = (System.Drawing.Image)resources.GetObject("btnRunSequence.Image");
		this.btnRunSequence.ImageTransparentColor = System.Drawing.Color.Magenta;
		this.btnRunSequence.Name = "btnRunSequence";
		this.btnRunSequence.Size = new System.Drawing.Size(23, 22);
		this.btnRunSequence.Text = "toolStripButton7";
		this.btnRunSequence.Click += new System.EventHandler(miSeqRunSequence_Click);
		this.btnPauseSequence.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
		this.btnPauseSequence.Image = (System.Drawing.Image)resources.GetObject("btnPauseSequence.Image");
		this.btnPauseSequence.ImageTransparentColor = System.Drawing.Color.Magenta;
		this.btnPauseSequence.Name = "btnPauseSequence";
		this.btnPauseSequence.Size = new System.Drawing.Size(23, 22);
		this.btnPauseSequence.Text = "toolStripButton10";
		this.btnPauseSequence.Click += new System.EventHandler(miSeqPauseSequence_Click);
		this.btnResumeSequence.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
		this.btnResumeSequence.Image = (System.Drawing.Image)resources.GetObject("btnResumeSequence.Image");
		this.btnResumeSequence.ImageTransparentColor = System.Drawing.Color.Magenta;
		this.btnResumeSequence.Name = "btnResumeSequence";
		this.btnResumeSequence.Size = new System.Drawing.Size(23, 22);
		this.btnResumeSequence.Text = "toolStripButton11";
		this.btnResumeSequence.Click += new System.EventHandler(miSeqResumeSequence_Click);
		this.btnStopSequence.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
		this.btnStopSequence.Image = (System.Drawing.Image)resources.GetObject("btnStopSequence.Image");
		this.btnStopSequence.ImageTransparentColor = System.Drawing.Color.Magenta;
		this.btnStopSequence.Name = "btnStopSequence";
		this.btnStopSequence.Size = new System.Drawing.Size(23, 22);
		this.btnStopSequence.Text = "toolStripButton8";
		this.btnStopSequence.Click += new System.EventHandler(miSeqStopSequence_Click);
		this.toolStripSeparator5.Name = "toolStripSeparator5";
		this.toolStripSeparator5.Size = new System.Drawing.Size(6, 25);
		this.btnRepeatInj.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
		this.btnRepeatInj.Image = (System.Drawing.Image)resources.GetObject("btnRepeatInj.Image");
		this.btnRepeatInj.ImageTransparentColor = System.Drawing.Color.Magenta;
		this.btnRepeatInj.Name = "btnRepeatInj";
		this.btnRepeatInj.Size = new System.Drawing.Size(23, 22);
		this.btnRepeatInj.Text = "toolStripButton12";
		this.btnRepeatInj.Click += new System.EventHandler(miSeqRepeatInj_Click);
		this.btnSkipVial.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
		this.btnSkipVial.Image = (System.Drawing.Image)resources.GetObject("btnSkipVial.Image");
		this.btnSkipVial.ImageTransparentColor = System.Drawing.Color.Magenta;
		this.btnSkipVial.Name = "btnSkipVial";
		this.btnSkipVial.Size = new System.Drawing.Size(23, 22);
		this.btnSkipVial.Text = "toolStripButton13";
		this.btnSkipVial.Click += new System.EventHandler(miSeqSkipVial_Click);
		this.btnSnapshot.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
		this.btnSnapshot.Image = (System.Drawing.Image)resources.GetObject("btnSnapshot.Image");
		this.btnSnapshot.ImageTransparentColor = System.Drawing.Color.Magenta;
		this.btnSnapshot.Name = "btnSnapshot";
		this.btnSnapshot.Size = new System.Drawing.Size(23, 22);
		this.btnSnapshot.Text = "toolStripButton16";
		this.btnSnapshot.Click += new System.EventHandler(miSeqSnapshot_Click);
		this.btnStopAcquisition.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
		this.btnStopAcquisition.Image = (System.Drawing.Image)resources.GetObject("btnStopAcquisition.Image");
		this.btnStopAcquisition.ImageTransparentColor = System.Drawing.Color.Magenta;
		this.btnStopAcquisition.Name = "btnStopAcquisition";
		this.btnStopAcquisition.Size = new System.Drawing.Size(23, 22);
		this.btnStopAcquisition.Text = "toolStripButton14";
		this.btnStopAcquisition.Click += new System.EventHandler(miSeqStopAcquisition_Click);
		this.btnAbortAcquisition.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
		this.btnAbortAcquisition.Image = (System.Drawing.Image)resources.GetObject("btnAbortAcquisition.Image");
		this.btnAbortAcquisition.ImageTransparentColor = System.Drawing.Color.Magenta;
		this.btnAbortAcquisition.Name = "btnAbortAcquisition";
		this.btnAbortAcquisition.Size = new System.Drawing.Size(23, 22);
		this.btnAbortAcquisition.Text = "toolStripButton15";
		this.btnAbortAcquisition.Click += new System.EventHandler(miSeqAbortAcquisition_Click);
		this.toolStripSeparator6.Name = "toolStripSeparator6";
		this.toolStripSeparator6.Size = new System.Drawing.Size(6, 25);
		this.btnOptions.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
		this.btnOptions.Image = (System.Drawing.Image)resources.GetObject("btnOptions.Image");
		this.btnOptions.ImageTransparentColor = System.Drawing.Color.Magenta;
		this.btnOptions.Name = "btnOptions";
		this.btnOptions.Size = new System.Drawing.Size(23, 22);
		this.btnOptions.Text = "toolStripButton17";
		this.btnOptions.Click += new System.EventHandler(btnOptions_Click);
		this.toolStripSeparator7.Name = "toolStripSeparator7";
		this.toolStripSeparator7.Size = new System.Drawing.Size(6, 25);
		this.btnRowMethod.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
		this.btnRowMethod.Image = (System.Drawing.Image)resources.GetObject("btnRowMethod.Image");
		this.btnRowMethod.ImageTransparentColor = System.Drawing.Color.Magenta;
		this.btnRowMethod.Name = "btnRowMethod";
		this.btnRowMethod.Size = new System.Drawing.Size(23, 22);
		this.btnRowMethod.Text = "toolStripButton18";
		this.btnRowMethod.Click += new System.EventHandler(btnRowMethod_Click);
		this.btnRowReportSetup.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
		this.btnRowReportSetup.Image = (System.Drawing.Image)resources.GetObject("btnRowReportSetup.Image");
		this.btnRowReportSetup.ImageTransparentColor = System.Drawing.Color.Magenta;
		this.btnRowReportSetup.Name = "btnRowReportSetup";
		this.btnRowReportSetup.Size = new System.Drawing.Size(23, 22);
		this.btnRowReportSetup.Text = "toolStripButton19";
		this.btnRowReportSetup.Click += new System.EventHandler(btnRowReportSetup_Click);
		this.btnKAlpha.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
		this.btnKAlpha.Image = (System.Drawing.Image)resources.GetObject("btnKAlpha.Image");
		this.btnKAlpha.ImageTransparentColor = System.Drawing.Color.Magenta;
		this.btnKAlpha.Name = "btnKAlpha";
		this.btnKAlpha.Size = new System.Drawing.Size(23, 22);
		this.btnKAlpha.Text = "toolStripButton1";
		this.btnKAlpha.Click += new System.EventHandler(btnKAlpha_Click);
		this.btnCheck.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
		this.btnCheck.Enabled = false;
		this.btnCheck.Image = (System.Drawing.Image)resources.GetObject("btnCheck.Image");
		this.btnCheck.ImageTransparentColor = System.Drawing.Color.Magenta;
		this.btnCheck.Name = "btnCheck";
		this.btnCheck.Size = new System.Drawing.Size(23, 22);
		this.btnCheck.Text = "toolStripButton20";
		this.btnCheck.Click += new System.EventHandler(btnCheck_Click);
		this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
		this.toolStripButton1.Image = (System.Drawing.Image)resources.GetObject("toolStripButton1.Image");
		this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
		this.toolStripButton1.Name = "toolStripButton1";
		this.toolStripButton1.Size = new System.Drawing.Size(23, 22);
		this.toolStripButton1.Text = "toolStripButton1";
		this.toolStripButton1.ToolTipText = "查询自动进样器参数";
		this.toolStripButton1.Click += new System.EventHandler(toolStripButton1_Click);
		base.ClientSize = new System.Drawing.Size(708, 319);
		base.Controls.Add(this.gvGpcSeqAly);
		base.Controls.Add(this.gvGnlSeqAly);
		base.Controls.Add(this.tsSequAly);
		base.Controls.Add(this.ssSequAly);
		base.Controls.Add(this.msSequAly);
		base.MainMenuStrip = this.msSequAly;
		base.Name = "SeqAlyForm";
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.Text = "自动进样序列";
		base.Load += new System.EventHandler(SeqAlyForm_Load);
		this.msSequAly.ResumeLayout(false);
		this.msSequAly.PerformLayout();
		this.ssSequAly.ResumeLayout(false);
		this.ssSequAly.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.gvGnlSeqAly).EndInit();
		((System.ComponentModel.ISupportInitialize)this.gvGpcSeqAly).EndInit();
		this.tsSequAly.ResumeLayout(false);
		this.tsSequAly.PerformLayout();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
