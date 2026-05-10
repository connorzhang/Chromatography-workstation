using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace IBrainChrom2018;

public class SSAlyForm : LclGnlForm
{
	private const string string_0 = ".ss";

	private CMS_InfoParasFMT cms_InfoParasFMT_0 = new CMS_InfoParasFMT();

	public int counter;

	private ColumnsSetupDlg columnsSetupDlg_0 = new ColumnsSetupDlg("单针序列 列设置", "Single Seq. Clm.Setup");

	public SSOptDlg dlgSsOpt = new SSOptDlg();

	private int int_0;

	private SSAly ssaly_0 = new SSAly();

	private OpenFileDialog openFileDialog_0 = new OpenFileDialog();

	private OpenFileDialog openFileDialog_1 = new OpenFileDialog();

	private OpenFileDialog openFileDialog_2 = new OpenFileDialog();

	private SaveFileDialog saveFileDialog_0 = new SaveFileDialog();

	private string string_1;

	private ImageList imageList_0;

	private ChromFormInterface formMain_0;

	private ToolStripButton btnAbortAcq;

	private ToolStripButton btnInsertLine;

	private ToolStripButton btnNew;

	private ToolStripButton btnOpen;

	private ToolStripButton btnOptions;

	private ToolStripButton btnRowMethod;

	private ToolStripButton btnRowReportSetup;

	private ToolStripButton btnRowsDown;

	private ToolStripButton btnRowsUp;

	private ToolStripButton btnRunCurRow;

	private ToolStripButton btnSave;

	private ToolStripButton btnSnapshot;

	private ToolStripButton btnStopAcq;

	private DataGridViewColumn dataGridViewColumn_0;

	private DataGridViewColumn dataGridViewColumn_1;

	private DataGridViewColumn dataGridViewColumn_2;

	private IContainer icontainer_2;

	private MtdSetupDlg mtdSetupDlg_0;

	private RptSetupDlg rptSetupDlg_0;

	private LclGridView gvGnlSSAly;

	private ToolStripMenuItem miAlyAbortAcquisition;

	private ToolStripMenuItem miAlyRunSingle;

	private ToolStripMenuItem miAlySnapshot;

	private ToolStripMenuItem miAlyStopAcquisition;

	private ToolStripMenuItem miAnalysis;

	private ToolStripMenuItem miEdit;

	private ToolStripMenuItem miEditClmSetup;

	private ToolStripMenuItem miEdtDftClms;

	private ToolStripMenuItem miEdtInsertLine;

	private ToolStripMenuItem miEdtResetStatus;

	private ToolStripMenuItem miEdtRowsDown;

	private ToolStripMenuItem miEdtRowsUp;

	private ToolStripMenuItem miFiExit;

	private ToolStripMenuItem miFile;

	private ToolStripMenuItem miFiNew;

	private ToolStripMenuItem miFiOpen;

	private ToolStripMenuItem miFiSave;

	private ToolStripMenuItem miFiSaveAs;

	private MenuStrip msSSAly;

	private ToolStripStatusLabel slbExplain;

	private StatusStrip ssSequAly;

	private ToolStripSeparator toolStripSeparator1;

	private ToolStripSeparator toolStripSeparator2;

	private ToolStripSeparator toolStripSeparator3;

	private ToolStripSeparator toolStripSeparator4;

	private ToolStripSeparator toolStripSeparator5;

	private ToolStripSeparator toolStripSeparator6;

	private ToolStripSeparator toolStripSeparator7;

	private ToolStripLabel tslbCounter;

	private ToolStrip tsSSAly;

	private ToolStripTextBox tstbCounter;

	private IContainer components;

	public SSAlyForm()
	{
		InitializeComponent();
		gvGnlSSAly.AllowUserToDeleteRows = true;
		miEdtResetStatus.Text = Lang.PS("重置状态", "Reset Status");
		slbExplain.Text = Lang.PS("单针序列", "Single List");
		tslbCounter.Text = Lang.PS("计数", "Counter");
		gvGnlSSAly.Dock = DockStyle.Fill;
		gvGnlSSAly.BorderStyle = BorderStyle.None;
		method_1();
		SeqAlyForm._gvRefreshHeaders(gvGnlSSAly);
		method_3();
		btnNew_Click(null, null);
	}

	public void Init(ChromFormInterface F)
	{
		formMain_0 = F;
	}

	private void method_0()
	{
		formMain_0.ClearGather();
		method_6(int_0, InjStatusMeasure.MeasuredBySkip);
		counter++;
		tstbCounter.Text = counter.ToString();
		Injection injection = gvGnlSSAly.Rows[int_0].Tag as Injection;
		injection.injNo++;
		if (injection.injNo < injection.vialInjs)
		{
			return;
		}
		gvGnlSSAly.Rows[int_0].Selected = false;
		if (int_0 >= gvGnlSSAly.RowCount - 1)
		{
			return;
		}
		if (gvGnlSSAly.Columns["Run"].Visible)
		{
			for (int i = int_0 + 1; i < gvGnlSSAly.RowCount; i++)
			{
				if ((bool)gvGnlSSAly.Rows[i].Cells["Run"].Value)
				{
					gvGnlSSAly.Rows[i].Selected = true;
					break;
				}
			}
		}
		else
		{
			gvGnlSSAly.Rows[int_0 + 1].Selected = true;
		}
	}

	private void btnOptions_Click(object sender, EventArgs e)
	{
		if (dlgSsOpt.ShowDialog(ssaly_0.ssOpt) == DialogResult.OK)
		{
			method_3();
		}
	}

	private void btnRowMethod_Click(object sender, EventArgs e)
	{
		gvGnlSSAly.EndEdit();
		if (gvGnlSSAly.SelectedRows.Count != 1)
		{
			return;
		}
		int index = gvGnlSSAly.SelectedRows[0].Index;
		Injection injection = gvGnlSSAly.Rows[index].Tag as Injection;
		if (injection.methodFileName != "" && File.Exists(injection.methodFileName))
		{
			if (mtdSetupDlg_0 == null)
			{
				mtdSetupDlg_0 = new MtdSetupDlg(instrument);
				mtdSetupDlg_0.refresh_once();
			}
			mtdSetupDlg_0.Text = Lang.PS("方法:") + " [" + injection.methodFileName + "]";
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
		gvGnlSSAly.EndEdit();
		if (gvGnlSSAly.SelectedRows.Count != 1)
		{
			return;
		}
		int index = gvGnlSSAly.SelectedRows[0].Index;
		Injection injection = gvGnlSSAly.Rows[index].Tag as Injection;
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

	private void gvGnlSSAly_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
	{
		if (e.RowIndex == -1 && e.ColumnIndex != -1 && gvGnlSSAly.RowCount > 1)
		{
			gvGnlSSAly.EndEdit();
			string name = gvGnlSSAly.Columns[e.ColumnIndex].Name;
			object value = gvValue(gvUse: true, gvGnlSSAly.Rows[0].Tag as Injection, name);
			for (int i = 1; i < gvGnlSSAly.RowCount; i++)
			{
				gvGnlSSAly.Rows[i].Cells[name].Value = value;
			}
			method_2(AccStyle.Write);
			method_2(AccStyle.Read);
		}
	}

	private void gvGnlSSAly_CellEndEdit(object sender, DataGridViewCellEventArgs e)
	{
		method_4(e.RowIndex, AccStyle.Write);
		method_4(e.RowIndex, AccStyle.Read);
	}

	public object gvValue(bool gvUse, Injection injection_0, string columnName)
	{
		object obj = null;
		string text = gvGnlSSAly.ConvertValFmt(columnName);
		switch (columnName)
		{
		case "Status":
			obj = null;
			break;
		case "Run":
			obj = (gvUse ? ((object)injection_0.bool_0) : (injection_0.bool_0 ? "√" : ""));
			break;
		case "VI":
			obj = (gvUse ? ((object)injection_0.vialInjs) : injection_0.vialInjs.ToString(text));
			break;
		case "SampleID":
			obj = injection_0.sampleID;
			break;
		case "Sample":
			obj = injection_0.sample;
			break;
		case "Amount":
			obj = (gvUse ? ((object)injection_0.amount) : injection_0.amount.ToString(text));
			break;
		case "ISTDAmount":
			obj = (gvUse ? ((object)injection_0.ISTD_amount) : injection_0.ISTD_amount.ToString(text));
			break;
		case "Dilution":
			obj = (gvUse ? ((object)injection_0.dilution) : injection_0.dilution.ToString(text));
			break;
		case "InjVol":
			obj = (gvUse ? ((object)injection_0.inj_volume) : injection_0.inj_volume.ToString(text));
			break;
		case "FileNameFMT":
			obj = injection_0.fileNameFMT;
			break;
		case "CaliStand":
			obj = (gvUse ? ((object)injection_0.cali_stand) : (injection_0.cali_stand ? "√" : ""));
			break;
		case "MethodName":
			obj = injection_0.methodFileName;
			break;
		case "ReportStyle":
			obj = injection_0.reportStyleFileName;
			break;
		case "OpenChrom":
			obj = (gvUse ? ((object)injection_0.openChromWin) : (injection_0.openChromWin ? "√" : ""));
			break;
		case "OpenCali":
			obj = (gvUse ? ((object)injection_0.openCaliWin) : (injection_0.openCaliWin ? "√" : ""));
			break;
		case "Print":
			obj = (gvUse ? ((object)injection_0.openPrintWin) : (injection_0.openPrintWin ? "√" : ""));
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
		gvGnlSSAly.AddLclgvSeqStatusColumn("Status", 35).Frozen = true;
		gvGnlSSAly.AddLclCheckBoxColumn("Run", 30);
		gvGnlSSAly.AddLclTextBoxColumn("VI", 30, 0, StringAlignment.Center);
		gvGnlSSAly.AddLclTextBoxCtxBtnColumn("SampleID", 80, cms_InfoParasFMT_0);
		gvGnlSSAly.AddLclTextBoxCtxBtnColumn("Sample", 90, cms_InfoParasFMT_0);
		gvGnlSSAly.AddLclTextBoxColumn("Amount", 50);
		gvGnlSSAly.AddLclTextBoxColumn("ISTDAmount", 50);
		gvGnlSSAly.AddLclTextBoxColumn("Dilution", 50);
		gvGnlSSAly.AddLclTextBoxColumn("InjVol", 50);
		gvGnlSSAly.AddLclTextBoxCtxBtnColumn("FileNameFMT", 110, cms_InfoParasFMT_0);
		gvGnlSSAly.AddLclCheckBoxColumn("CaliStand", 40);
		gvGnlSSAly.AddLclTextBoxCtxBtnColumn("MethodName", 90, openFileDialog_0);
		gvGnlSSAly.AddLclTextBoxCtxBtnColumn("ReportStyle", 90, openFileDialog_1);
		dataGridViewColumn_1 = gvGnlSSAly.AddLclCheckBoxColumn("OpenChrom", 35);
		dataGridViewColumn_0 = gvGnlSSAly.AddLclCheckBoxColumn("OpenCali", 35);
		dataGridViewColumn_2 = gvGnlSSAly.AddLclCheckBoxColumn("Print", 35);
		miEdtDftClms_Click(null, null);
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
		miAnalysis.Text = Lang.PS("分析", "Analysis");
		miAlyRunSingle.Text = Lang.PS("运行单针", "Run Single");
		miAlySnapshot.Text = Lang.PS("快照", "Snapshot");
		miAlyStopAcquisition.Text = Lang.PS("停止采集", "Stop Acquisition");
		miAlyAbortAcquisition.Text = Lang.PS("放弃采集", "Abort Acquisition");
		method_3();
		cms_InfoParasFMT_0.LoadLanguage();
	}

	public void miAlyAbortAcquisition_Click(object sender, EventArgs e)
	{
		if (instrument.sampling && int_0 >= 0 && MessageBox.Show("确定放弃采集吗?", "警告", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
		{
			method_0();
		}
	}

	public void miAlyRunSingle_Click(object sender, EventArgs e)
	{
		if (!formMain_0.StartBtEnable)
		{
			return;
		}
		gvGnlSSAly.EndEdit();
		int_0 = -1;
		if (gvGnlSSAly.RowCount == 1)
		{
			int_0 = 0;
		}
		else
		{
			if (gvGnlSSAly.SelectedRows.Count != 1)
			{
				MessageBox.Show(Lang.PS("请在[单针序列]中选择进样行！", "Please choose inj. row in single sequence, first!"));
				return;
			}
			int_0 = gvGnlSSAly.SelectedRows[0].Index;
		}
		method_6(int_0, InjStatusMeasure.NoAnalysis);
		method_4(int_0, AccStyle.Write);
		method_4(int_0, AccStyle.Read);
		Injection injection = gvGnlSSAly.Rows[int_0].Tag as Injection;
		injection.dtAcquire = DateTime.Now;
		injection.analyst = instrument.user.u_name;
		injection.counter = counter;
		TcpServerSocket currentTcpSocket = formMain_0.GetCurrentTcpSocket();
		if (currentTcpSocket == null)
		{
			return;
		}
		int currentChannelIndex = formMain_0.CurrentChannelIndex;
		currentTcpSocket.sglsSampling[currentChannelIndex].runningInjInfo.LoadFromObject(injection, dataGridViewColumn_1.Visible, Cali: false, Print: false);
		ChromDevice chromDevice = new ChromDevice();
		for (int i = 0; i < formMain_0.FrmChromat.SunAquips.Count; i++)
		{
			if (formMain_0.FrmChromat.SunAquips[i].info.ID == formMain_0.CurrentGCID)
			{
				chromDevice = formMain_0.FrmChromat.SunAquips[i];
			}
		}
		int num = currentChannelIndex;
		if (currentTcpSocket.sglsSampling[currentChannelIndex].runningInjInfo.methodFileName != "")
		{
			chromDevice.misMgr.ChartParaOperaS[num].mtdMgr.LoadFromFile(currentTcpSocket.sglsSampling[currentChannelIndex].runningInjInfo.methodFileName);
		}
		if (currentTcpSocket.dtc_Channels[num] is DtC_Detector dtC_Detector)
		{
			chromDevice.misMgr.ChartParaOperaS[num].mtdMgr.chromInfoR.UvWave = dtC_Detector.wave;
			chromDevice.misMgr.ChartParaOperaS[num].mtdMgr.chromInfoR.UvRange = dtC_Detector.range.ToString("0.00");
			chromDevice.misMgr.ChartParaOperaS[num].mtdMgr.chromInfoR.UvRistTime = dtC_Detector.ristTime.ToString("0.0");
		}
		method_6(int_0, InjStatusMeasure.BeingMeasured);
		if (formMain_0.StartBtEnable)
		{
			formMain_0.StartGather();
		}
	}

	public void miAlySnapshot_Click(object sender, EventArgs e)
	{
		TcpServerSocket currentTcpSocket = formMain_0.GetCurrentTcpSocket();
		if (currentTcpSocket == null)
		{
			return;
		}
		int currentChannelIndex = formMain_0.CurrentChannelIndex;
		if (gvGnlSSAly.SelectedRows.Count == 1)
		{
			int index = gvGnlSSAly.SelectedRows[0].Index;
			Injection injection = gvGnlSSAly.Rows[index].Tag as Injection;
			currentTcpSocket.sglsSampling[currentChannelIndex].runningInjInfo.LoadFromObject(injection, dataGridViewColumn_1.Visible, Cali: false, Print: false);
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

	public void miAlyStopAcquisition_Click(object sender, EventArgs e)
	{
		if (formMain_0.StopBtEnable)
		{
			formMain_0.StopGather();
		}
	}

	private void miEditClmSetup_Click(object sender, EventArgs e)
	{
		columnsSetupDlg_0.ShowDialog(gvGnlSSAly);
	}

	private void miEdtDftClms_Click(object sender, EventArgs e)
	{
		gvGnlSSAly.ini_SetFirstVisibleColumn("Status");
		gvGnlSSAly.ini_SetNextVisibleColumn("Run");
		gvGnlSSAly.ini_SetNextVisibleColumn("VI");
		gvGnlSSAly.ini_SetNextVisibleColumn("SampleID");
		gvGnlSSAly.ini_SetNextVisibleColumn("Sample");
		gvGnlSSAly.ini_SetNextVisibleColumn("Amount");
		gvGnlSSAly.ini_SetNextVisibleColumn("ISTDAmount");
		gvGnlSSAly.ini_SetNextVisibleColumn("Dilution");
		gvGnlSSAly.ini_SetNextVisibleColumn("InjVol");
		gvGnlSSAly.ini_SetNextVisibleColumn("FileNameFMT");
		gvGnlSSAly.ini_SetNextVisibleColumn("CaliStand");
		gvGnlSSAly.ini_SetNextVisibleColumn("MethodName");
		gvGnlSSAly.ini_SetNextVisibleColumn("ReportStyle");
		gvGnlSSAly.ini_FinishVisibleColumn();
	}

	private void btnInsertLine_Click(object sender, EventArgs e)
	{
		int num;
		if (gvGnlSSAly.SelectedRows != null && gvGnlSSAly.SelectedRows.Count != 0)
		{
			num = gvGnlSSAly.SelectedRows[0].Index;
			Injection injection = new Injection();
			injection.LoadFromObject(gvGnlSSAly.Rows[num].Tag as Injection);
			gvGnlSSAly.Rows.Insert(num, 1);
			gvGnlSSAly.Rows[num].Tag = injection;
		}
		else
		{
			num = gvGnlSSAly.Rows.Add();
			gvGnlSSAly.Rows[num].Tag = new Injection();
		}
		method_4(num, AccStyle.Read);
	}

	private void miEdtResetStatus_Click(object sender, EventArgs e)
	{
		for (int i = 0; i < gvGnlSSAly.RowCount; i++)
		{
			method_6(i, InjStatusMeasure.NoAnalysis);
			(gvGnlSSAly.Rows[i].Tag as Injection).injNo = 0;
		}
		if (gvGnlSSAly.RowCount != 0)
		{
			gvGnlSSAly.Rows[0].Selected = true;
		}
	}

	private void btnRowsDown_Click(object sender, EventArgs e)
	{
		int[] array = gvGnlSSAly.AdjustSelectedRows(AdjustUpDown.Down);
		for (int i = 0; i < array.Length; i++)
		{
			method_4(array[i], AccStyle.Read);
		}
	}

	private void btnRowsUp_Click(object sender, EventArgs e)
	{
		int[] array = gvGnlSSAly.AdjustSelectedRows(AdjustUpDown.Up);
		for (int i = 0; i < array.Length; i++)
		{
			method_4(array[i], AccStyle.Read);
		}
	}

	private void btnNew_Click(object sender, EventArgs e)
	{
		ssaly_0 = new SSAly();
		method_2(AccStyle.Read);
		btnInsertLine_Click(null, null);
		method_6(0, InjStatusMeasure.NoAnalysis);
		string_1 = null;
		method_5();
	}

	private void btnOpen_Click(object sender, EventArgs e)
	{
		if (openFileDialog_2.ShowDialog() == DialogResult.OK)
		{
			string_1 = openFileDialog_2.FileName;
			ssaly_0.LoadFromFile(string_1);
			method_5();
			method_2(AccStyle.Read);
			for (int i = 0; i < gvGnlSSAly.RowCount; i++)
			{
				method_6(i, InjStatusMeasure.NoAnalysis);
			}
		}
	}

	private void btnSave_Click(object sender, EventArgs e)
	{
		if (gvGnlSSAly.RowCount != 0)
		{
			method_2(AccStyle.Write);
			method_2(AccStyle.Read);
			if (string_1 == null)
			{
				miFiSaveAs_Click(null, null);
			}
			else
			{
				ssaly_0.SaveToFile(string_1);
			}
		}
	}

	private void miFiSaveAs_Click(object sender, EventArgs e)
	{
		if (saveFileDialog_0.ShowDialog() == DialogResult.OK)
		{
			string_1 = saveFileDialog_0.FileName;
			method_5();
			btnSave_Click(null, null);
		}
	}

	protected override void miHpHelp_Click(object sender, EventArgs e)
	{
	}

	public override void ReadWinInfo(WinInfo winInfo)
	{
		base.ReadWinInfo(winInfo);
		int gvNo = 0;
		winInfo.gvCF_w(gvGnlSSAly, ref gvNo);
		counter = winInfo.para1;
		tstbCounter.Text = counter.ToString();
		if (winInfo.dftInj != null && gvGnlSSAly.RowCount == 1)
		{
			gvGnlSSAly.Rows[0].Tag = winInfo.dftInj;
			method_4(0, AccStyle.Read);
		}
	}

	private void method_2(AccStyle accStyle_0)
	{
		switch (accStyle_0)
		{
		case AccStyle.Read:
		{
			gvGnlSSAly.SuspendLayout();
			gvGnlSSAly.RowCount = ssaly_0.ssInjs.Length;
			for (int j = 0; j < gvGnlSSAly.RowCount; j++)
			{
				if (gvGnlSSAly.Rows[j].Tag == null)
				{
					gvGnlSSAly.Rows[j].Tag = new Injection();
				}
				(gvGnlSSAly.Rows[j].Tag as Injection).LoadFromObject(ssaly_0.ssInjs[j]);
				method_4(j, AccStyle.Read);
			}
			gvGnlSSAly.ResumeLayout();
			method_3();
			break;
		}
		case AccStyle.Write:
		{
			ssaly_0.SetSeqInjsNum(gvGnlSSAly.RowCount);
			for (int i = 0; i < gvGnlSSAly.RowCount; i++)
			{
				method_4(i, AccStyle.Write);
				ssaly_0.ssInjs[i].LoadFromObject((Injection)gvGnlSSAly.Rows[i].Tag);
			}
			break;
		}
		}
	}

	private void method_3()
	{
		if (gvGnlSSAly.Columns.Contains("InjVol"))
		{
			gvGnlSSAly.Columns["InjVol"].HeaderText = Lang.PS("体积\n", "Inj.Vol\n") + "[" + ssaly_0.ssOpt.injVolumnUnit.ToString() + "]";
		}
	}

	private void method_4(int int_1, AccStyle accStyle_0)
	{
		Injection injection = gvGnlSSAly.Rows[int_1].Tag as Injection;
		switch (accStyle_0)
		{
		case AccStyle.Read:
		{
			for (int j = 0; j < gvGnlSSAly.ColumnCount; j++)
			{
				object obj = gvValue(gvUse: true, injection, gvGnlSSAly.Columns[j].Name);
				if (obj == null)
				{
					gvGnlSSAly.InvalidateCell(j, int_1);
				}
				else
				{
					gvGnlSSAly.Rows[int_1].Cells[j].Value = obj;
				}
			}
			break;
		}
		case AccStyle.Write:
		{
			for (int i = 0; i < gvGnlSSAly.ColumnCount; i++)
			{
				object value = gvGnlSSAly.Rows[int_1].Cells[i].Value;
				string name;
				switch (name = gvGnlSSAly.Columns[i].Name)
				{
				case "Run":
					injection.bool_0 = (bool)value;
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
		method_5();
		tstbCounter.Text = counter.ToString();
	}

	private void method_5()
	{
		Text = Lang.PS("单针序列[", "Single Injection[") + instrument.name + "] " + string_1;
	}

	private void method_6(int int_1, InjStatusMeasure injStatusMeasure_0)
	{
		if (int_1 >= 0 && int_1 < gvGnlSSAly.RowCount)
		{
			(gvGnlSSAly.Rows[int_1].Cells[0] as LclgvSeqStatusCell).injStatusMeasure = injStatusMeasure_0;
			gvGnlSSAly.InvalidateCell(0, int_1);
		}
	}

	public void Set3Buttons(bool enabled)
	{
		ToolStripButton toolStripButton = btnRunCurRow;
		bool enabled2 = (miAlyRunSingle.Enabled = !enabled);
		toolStripButton.Enabled = enabled2;
		ToolStripItem toolStripItem = btnSnapshot;
		ToolStripItem toolStripItem2 = btnStopAcq;
		btnAbortAcq.Enabled = enabled;
		toolStripItem2.Enabled = enabled;
		toolStripItem.Enabled = enabled;
		ToolStripItem toolStripItem3 = miAlySnapshot;
		ToolStripItem toolStripItem4 = miAlyStopAcquisition;
		miAlyAbortAcquisition.Enabled = enabled;
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

	private void SSAlyForm_Load(object sender, EventArgs e)
	{
		LclGridView.imgContextButton = (Bitmap)imageList_0.Images[0];
		LclGridView.imgUnContextButton = (Bitmap)imageList_0.Images[1];
		OpenFileDialog openFileDialog = openFileDialog_2;
		string filter = (saveFileDialog_0.Filter = Class49.MakeFileFilter(".ss"));
		openFileDialog.Filter = filter;
		openFileDialog_0.Title = "打开方法文件";
		openFileDialog_0.InitialDirectory = instrument.PrjPath;
		openFileDialog_0.Filter = Class49.MakeFileFilter(".mtd");
		openFileDialog_1.Title = "打开样式文件";
		openFileDialog_1.InitialDirectory = instrument.PrjPath;
		openFileDialog_1.Filter = Class49.MakeFileFilter(".sty");
	}

	private void tsSSAly_Paint(object sender, PaintEventArgs e)
	{
		ToolStrip toolStrip = sender as ToolStrip;
		for (int i = 0; i < toolStrip.Items.Count; i++)
		{
			if (toolStrip.Items[i] is ToolStripTextBox)
			{
				ToolStripTextBox toolStripTextBox = toolStrip.Items[i] as ToolStripTextBox;
				if (toolStripTextBox.Visible)
				{
					toolStripTextBox.Height = 17;
					toolStripTextBox.AutoSize = false;
					Rectangle bounds = toolStripTextBox.Bounds;
					bounds.Offset(-1, -1);
					bounds.Width += 3;
					e.Graphics.DrawRectangle(Pens.Gray, bounds);
				}
			}
		}
	}

	private void tstbCounter_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode == Keys.Return)
		{
			int result = 0;
			if (int.TryParse(tstbCounter.Text.Trim(), out result))
			{
				counter = result;
			}
		}
	}

	public override void WriteWinInfo(WinInfo winInfo)
	{
		base.WriteWinInfo(winInfo);
		winInfo.para1 = counter;
		winInfo.gvCF_r(gvGnlSSAly);
		winInfo.dftInj = ((gvGnlSSAly.RowCount == 1) ? (gvGnlSSAly.Rows[0].Tag as Injection) : null);
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(IBrainChrom2018.SSAlyForm));
		this.msSSAly = new System.Windows.Forms.MenuStrip();
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
		this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
		this.miEdtResetStatus = new System.Windows.Forms.ToolStripMenuItem();
		this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
		this.miEditClmSetup = new System.Windows.Forms.ToolStripMenuItem();
		this.miEdtDftClms = new System.Windows.Forms.ToolStripMenuItem();
		this.miAnalysis = new System.Windows.Forms.ToolStripMenuItem();
		this.miAlyRunSingle = new System.Windows.Forms.ToolStripMenuItem();
		this.miAlySnapshot = new System.Windows.Forms.ToolStripMenuItem();
		this.miAlyStopAcquisition = new System.Windows.Forms.ToolStripMenuItem();
		this.miAlyAbortAcquisition = new System.Windows.Forms.ToolStripMenuItem();
		this.tsSSAly = new System.Windows.Forms.ToolStrip();
		this.btnNew = new System.Windows.Forms.ToolStripButton();
		this.btnOpen = new System.Windows.Forms.ToolStripButton();
		this.btnSave = new System.Windows.Forms.ToolStripButton();
		this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
		this.btnInsertLine = new System.Windows.Forms.ToolStripButton();
		this.btnRowsUp = new System.Windows.Forms.ToolStripButton();
		this.btnRowsDown = new System.Windows.Forms.ToolStripButton();
		this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
		this.btnRunCurRow = new System.Windows.Forms.ToolStripButton();
		this.btnSnapshot = new System.Windows.Forms.ToolStripButton();
		this.btnStopAcq = new System.Windows.Forms.ToolStripButton();
		this.btnAbortAcq = new System.Windows.Forms.ToolStripButton();
		this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
		this.btnOptions = new System.Windows.Forms.ToolStripButton();
		this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
		this.btnRowMethod = new System.Windows.Forms.ToolStripButton();
		this.btnRowReportSetup = new System.Windows.Forms.ToolStripButton();
		this.tslbCounter = new System.Windows.Forms.ToolStripLabel();
		this.tstbCounter = new System.Windows.Forms.ToolStripTextBox();
		this.ssSequAly = new System.Windows.Forms.StatusStrip();
		this.slbExplain = new System.Windows.Forms.ToolStripStatusLabel();
		this.gvGnlSSAly = new IBrainChrom2018.LclGridView();
		this.imageList_0 = new System.Windows.Forms.ImageList(this.components);
		this.msSSAly.SuspendLayout();
		this.tsSSAly.SuspendLayout();
		this.ssSequAly.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.gvGnlSSAly).BeginInit();
		base.SuspendLayout();
		this.msSSAly.Items.AddRange(new System.Windows.Forms.ToolStripItem[3] { this.miFile, this.miEdit, this.miAnalysis });
		this.msSSAly.Location = new System.Drawing.Point(0, 0);
		this.msSSAly.Name = "msSSAly";
		this.msSSAly.ShowItemToolTips = true;
		this.msSSAly.Size = new System.Drawing.Size(944, 25);
		this.msSSAly.TabIndex = 1;
		this.msSSAly.Text = "menuStrip1";
		this.miFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[6] { this.miFiNew, this.miFiOpen, this.miFiSave, this.miFiSaveAs, this.toolStripSeparator1, this.miFiExit });
		this.miFile.Name = "miFile";
		this.miFile.Size = new System.Drawing.Size(44, 21);
		this.miFile.Text = "文件";
		this.miFiNew.Name = "miFiNew";
		this.miFiNew.Size = new System.Drawing.Size(152, 22);
		this.miFiNew.Text = "新建";
		this.miFiNew.Click += new System.EventHandler(btnNew_Click);
		this.miFiOpen.Name = "miFiOpen";
		this.miFiOpen.Size = new System.Drawing.Size(152, 22);
		this.miFiOpen.Text = "打开...";
		this.miFiOpen.Click += new System.EventHandler(btnOpen_Click);
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
		this.miEdit.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[8] { this.miEdtInsertLine, this.miEdtRowsUp, this.miEdtRowsDown, this.toolStripSeparator5, this.miEdtResetStatus, this.toolStripSeparator7, this.miEditClmSetup, this.miEdtDftClms });
		this.miEdit.Name = "miEdit";
		this.miEdit.Size = new System.Drawing.Size(44, 21);
		this.miEdit.Text = "编辑";
		this.miEdtInsertLine.Name = "miEdtInsertLine";
		this.miEdtInsertLine.Size = new System.Drawing.Size(152, 22);
		this.miEdtInsertLine.Text = "插入行";
		this.miEdtInsertLine.Click += new System.EventHandler(btnInsertLine_Click);
		this.miEdtRowsUp.Name = "miEdtRowsUp";
		this.miEdtRowsUp.Size = new System.Drawing.Size(152, 22);
		this.miEdtRowsUp.Text = "提前行";
		this.miEdtRowsUp.Click += new System.EventHandler(btnRowsUp_Click);
		this.miEdtRowsDown.Name = "miEdtRowsDown";
		this.miEdtRowsDown.Size = new System.Drawing.Size(152, 22);
		this.miEdtRowsDown.Text = "后退行";
		this.miEdtRowsDown.Click += new System.EventHandler(btnRowsDown_Click);
		this.toolStripSeparator5.Name = "toolStripSeparator5";
		this.toolStripSeparator5.Size = new System.Drawing.Size(149, 6);
		this.miEdtResetStatus.Name = "miEdtResetStatus";
		this.miEdtResetStatus.Size = new System.Drawing.Size(152, 22);
		this.miEdtResetStatus.Click += new System.EventHandler(miEdtResetStatus_Click);
		this.toolStripSeparator7.Name = "toolStripSeparator7";
		this.toolStripSeparator7.Size = new System.Drawing.Size(149, 6);
		this.miEditClmSetup.Name = "miEditClmSetup";
		this.miEditClmSetup.Size = new System.Drawing.Size(152, 22);
		this.miEditClmSetup.Click += new System.EventHandler(miEditClmSetup_Click);
		this.miEdtDftClms.Name = "miEdtDftClms";
		this.miEdtDftClms.Size = new System.Drawing.Size(152, 22);
		this.miEdtDftClms.Click += new System.EventHandler(miEdtDftClms_Click);
		this.miAnalysis.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[4] { this.miAlyRunSingle, this.miAlySnapshot, this.miAlyStopAcquisition, this.miAlyAbortAcquisition });
		this.miAnalysis.Name = "miAnalysis";
		this.miAnalysis.Size = new System.Drawing.Size(44, 21);
		this.miAnalysis.Text = "分析";
		this.miAlyRunSingle.Name = "miAlyRunSingle";
		this.miAlyRunSingle.ShortcutKeys = System.Windows.Forms.Keys.F4;
		this.miAlyRunSingle.Size = new System.Drawing.Size(152, 22);
		this.miAlyRunSingle.Text = "运行单针";
		this.miAlyRunSingle.Click += new System.EventHandler(miAlyRunSingle_Click);
		this.miAlySnapshot.Name = "miAlySnapshot";
		this.miAlySnapshot.ShortcutKeys = System.Windows.Forms.Keys.F5;
		this.miAlySnapshot.Size = new System.Drawing.Size(152, 22);
		this.miAlySnapshot.Text = "快照";
		this.miAlySnapshot.Click += new System.EventHandler(miAlySnapshot_Click);
		this.miAlyStopAcquisition.Name = "miAlyStopAcquisition";
		this.miAlyStopAcquisition.ShortcutKeys = System.Windows.Forms.Keys.F8;
		this.miAlyStopAcquisition.Size = new System.Drawing.Size(152, 22);
		this.miAlyStopAcquisition.Text = "停止采集";
		this.miAlyStopAcquisition.Click += new System.EventHandler(miAlyStopAcquisition_Click);
		this.miAlyAbortAcquisition.Name = "miAlyAbortAcquisition";
		this.miAlyAbortAcquisition.ShortcutKeys = System.Windows.Forms.Keys.F9;
		this.miAlyAbortAcquisition.Size = new System.Drawing.Size(152, 22);
		this.miAlyAbortAcquisition.Text = "放弃采集";
		this.miAlyAbortAcquisition.Click += new System.EventHandler(miAlyAbortAcquisition_Click);
		this.tsSSAly.Items.AddRange(new System.Windows.Forms.ToolStripItem[19]
		{
			this.btnNew, this.btnOpen, this.btnSave, this.toolStripSeparator2, this.btnInsertLine, this.btnRowsUp, this.btnRowsDown, this.toolStripSeparator4, this.btnRunCurRow, this.btnSnapshot,
			this.btnStopAcq, this.btnAbortAcq, this.toolStripSeparator6, this.btnOptions, this.toolStripSeparator3, this.btnRowMethod, this.btnRowReportSetup, this.tslbCounter, this.tstbCounter
		});
		this.tsSSAly.Location = new System.Drawing.Point(0, 25);
		this.tsSSAly.Name = "tsSSAly";
		this.tsSSAly.ShowItemToolTips = false;
		this.tsSSAly.Size = new System.Drawing.Size(944, 25);
		this.tsSSAly.TabIndex = 6;
		this.tsSSAly.Text = "toolStrip1";
		this.tsSSAly.Paint += new System.Windows.Forms.PaintEventHandler(tsSSAly_Paint);
		this.btnNew.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
		this.btnNew.Image = (System.Drawing.Image)resources.GetObject("btnNew.Image");
		this.btnNew.ImageTransparentColor = System.Drawing.Color.Magenta;
		this.btnNew.Name = "btnNew";
		this.btnNew.Size = new System.Drawing.Size(23, 22);
		this.btnNew.Text = "toolStripButton1";
		this.btnNew.Click += new System.EventHandler(btnNew_Click);
		this.btnOpen.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
		this.btnOpen.Image = (System.Drawing.Image)resources.GetObject("btnOpen.Image");
		this.btnOpen.ImageTransparentColor = System.Drawing.Color.Magenta;
		this.btnOpen.Name = "btnOpen";
		this.btnOpen.Size = new System.Drawing.Size(23, 22);
		this.btnOpen.Text = "toolStripButton2";
		this.btnOpen.Click += new System.EventHandler(btnOpen_Click);
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
		this.btnRunCurRow.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
		this.btnRunCurRow.Image = (System.Drawing.Image)resources.GetObject("btnRunCurRow.Image");
		this.btnRunCurRow.ImageTransparentColor = System.Drawing.Color.Magenta;
		this.btnRunCurRow.Name = "btnRunCurRow";
		this.btnRunCurRow.Size = new System.Drawing.Size(23, 22);
		this.btnRunCurRow.Text = "toolStripButton7";
		this.btnRunCurRow.Click += new System.EventHandler(miAlyRunSingle_Click);
		this.btnSnapshot.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
		this.btnSnapshot.Image = (System.Drawing.Image)resources.GetObject("btnSnapshot.Image");
		this.btnSnapshot.ImageTransparentColor = System.Drawing.Color.Magenta;
		this.btnSnapshot.Name = "btnSnapshot";
		this.btnSnapshot.Size = new System.Drawing.Size(23, 22);
		this.btnSnapshot.Text = "toolStripButton16";
		this.btnSnapshot.Click += new System.EventHandler(miAlySnapshot_Click);
		this.btnStopAcq.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
		this.btnStopAcq.Image = (System.Drawing.Image)resources.GetObject("btnStopAcq.Image");
		this.btnStopAcq.ImageTransparentColor = System.Drawing.Color.Magenta;
		this.btnStopAcq.Name = "btnStopAcq";
		this.btnStopAcq.Size = new System.Drawing.Size(23, 22);
		this.btnStopAcq.Text = "toolStripButton14";
		this.btnStopAcq.Click += new System.EventHandler(miAlyStopAcquisition_Click);
		this.btnAbortAcq.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
		this.btnAbortAcq.Image = (System.Drawing.Image)resources.GetObject("btnAbortAcq.Image");
		this.btnAbortAcq.ImageTransparentColor = System.Drawing.Color.Magenta;
		this.btnAbortAcq.Name = "btnAbortAcq";
		this.btnAbortAcq.Size = new System.Drawing.Size(23, 22);
		this.btnAbortAcq.Text = "toolStripButton15";
		this.btnAbortAcq.Click += new System.EventHandler(miAlyAbortAcquisition_Click);
		this.toolStripSeparator6.Name = "toolStripSeparator6";
		this.toolStripSeparator6.Size = new System.Drawing.Size(6, 25);
		this.btnOptions.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
		this.btnOptions.Image = (System.Drawing.Image)resources.GetObject("btnOptions.Image");
		this.btnOptions.ImageTransparentColor = System.Drawing.Color.Magenta;
		this.btnOptions.Name = "btnOptions";
		this.btnOptions.Size = new System.Drawing.Size(23, 22);
		this.btnOptions.Text = "toolStripButton17";
		this.btnOptions.Click += new System.EventHandler(btnOptions_Click);
		this.toolStripSeparator3.Name = "toolStripSeparator3";
		this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
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
		this.tslbCounter.Margin = new System.Windows.Forms.Padding(5, 1, 0, 2);
		this.tslbCounter.Name = "tslbCounter";
		this.tslbCounter.Size = new System.Drawing.Size(0, 22);
		this.tstbCounter.BorderStyle = System.Windows.Forms.BorderStyle.None;
		this.tstbCounter.Name = "tstbCounter";
		this.tstbCounter.Size = new System.Drawing.Size(50, 25);
		this.tstbCounter.TextBoxTextAlign = System.Windows.Forms.HorizontalAlignment.Center;
		this.tstbCounter.KeyDown += new System.Windows.Forms.KeyEventHandler(tstbCounter_KeyDown);
		this.ssSequAly.Items.AddRange(new System.Windows.Forms.ToolStripItem[1] { this.slbExplain });
		this.ssSequAly.Location = new System.Drawing.Point(0, 267);
		this.ssSequAly.Name = "ssSequAly";
		this.ssSequAly.Size = new System.Drawing.Size(944, 22);
		this.ssSequAly.TabIndex = 12;
		this.ssSequAly.Text = "statusStrip1";
		this.slbExplain.Name = "slbExplain";
		this.slbExplain.Size = new System.Drawing.Size(0, 17);
		this.gvGnlSSAly.AllowUserToAddRows = false;
		this.gvGnlSSAly.AllowUserToDeleteRows = false;
		this.gvGnlSSAly.AllowUserToResizeRows = false;
		this.gvGnlSSAly.BackgroundColor = System.Drawing.Color.AliceBlue;
		this.gvGnlSSAly.CharacterHeaderColor = System.Drawing.Color.Black;
		this.gvGnlSSAly.ColumnHeadersHeight = 32;
		this.gvGnlSSAly.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
		this.gvGnlSSAly.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
		this.gvGnlSSAly.Location = new System.Drawing.Point(70, 82);
		this.gvGnlSSAly.MultiSelect = false;
		this.gvGnlSSAly.Name = "gvGnlSSAly";
		this.gvGnlSSAly.RowHeadersWidth = 25;
		this.gvGnlSSAly.RowTemplate.Height = 16;
		this.gvGnlSSAly.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
		this.gvGnlSSAly.ShowCellToolTips = false;
		this.gvGnlSSAly.Size = new System.Drawing.Size(240, 150);
		this.gvGnlSSAly.TabIndex = 13;
		this.gvGnlSSAly.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(gvGnlSSAly_CellDoubleClick);
		this.gvGnlSSAly.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(gvGnlSSAly_CellEndEdit);
		this.imageList_0.ImageStream = (System.Windows.Forms.ImageListStreamer)resources.GetObject("imageList_0.ImageStream");
		this.imageList_0.TransparentColor = System.Drawing.Color.Transparent;
		this.imageList_0.Images.SetKeyName(0, "ContextButton.png");
		this.imageList_0.Images.SetKeyName(1, "UnContextButton.png");
		base.ClientSize = new System.Drawing.Size(944, 289);
		base.Controls.Add(this.gvGnlSSAly);
		base.Controls.Add(this.ssSequAly);
		base.Controls.Add(this.tsSSAly);
		base.Controls.Add(this.msSSAly);
		base.Name = "SSAlyForm";
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.Text = "单针序列";
		base.Load += new System.EventHandler(SSAlyForm_Load);
		this.msSSAly.ResumeLayout(false);
		this.msSSAly.PerformLayout();
		this.tsSSAly.ResumeLayout(false);
		this.tsSSAly.PerformLayout();
		this.ssSequAly.ResumeLayout(false);
		this.ssSequAly.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.gvGnlSSAly).EndInit();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
