using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using IBrainChrom2018.Unit;

namespace IBrainChrom2018;

public class ChromFormDataGrid : UserControl
{
	public delegate Chromatogram[] GetChromatogramListHandler();

	public delegate Chromatogram GetChromatogramHandler();

	public delegate ChromDisplay GetChromDisplayHandler();

	public delegate SmyTabOpt GetSmyTabOptHandler();

	public delegate void SetslbExplainTextHandler(string strText);

	public delegate bool BoolNoneHandler();

	private SystemParam sysParam = SystemParam.Create();

	private FormMainParam frmParam = FormMainParam.Create();

	private ChromDeviceListMgr cdlMgr = ChromDeviceListMgr.Create();

	private bool m_bLoading = true;

	private IntegRow integRow_0;

	private IntegRow integRow_1;

	private ColumnsSetupDlg columnsSetupDlg_0 = new ColumnsSetupDlg("柱效列表列设置", "Performance Columns Setup");

	private ColumnsSetupDlg columnsSetupDlg_1 = new ColumnsSetupDlg("结果列表列设置", "Result List ColumnsSetup");

	private ColumnsSetupDlg columnsSetupDlg_2 = new ColumnsSetupDlg("切片列表列设置", "Slices List ColumnsSetup");

	private ColumnsSetupDlg columnsSetupDlg_3 = new ColumnsSetupDlg("总结列表列设置", "Summary List ColumnsSetup");

	private ColumnsSetupDlg columnsSetupDlg_4 = new ColumnsSetupDlg("SST结果列表列设置", "SST Results Columns Setup");

	private SmyTabOptDlg smyTabOptDlg_0 = new SmyTabOptDlg();

	private CusDlg cusDlg_0;

	private bool m_bGvPraformanceLoading;

	private bool bool_3;

	public Peak[] peakArray;

	private IContainer components = null;

	private DataGridViewTextBoxColumn 浓度;

	private DataGridViewTextBoxColumn Column1;

	private DataGridViewTextBoxColumn Column4;

	private DataGridViewTextBoxColumn Column2;

	private DataGridViewTextBoxColumn Column3;

	private LclSummaryGridView gvSummary;

	private LclGridView gvPerformStatic;

	public LclLabel lbExpress;

	private ContextMenuStrip cmsRltGV;

	private ToolStripMenuItem mirltsColumnsSetup;

	private ToolStripMenuItem mirltsRestoreDftColumns;

	private ToolStripSeparator toolStripSeparator33;

	private ToolStripMenuItem mirltsResetCmpdNames;

	private ContextMenuStrip cmsSummary;

	private ToolStripMenuItem mismyColumnsSetup;

	private ToolStripMenuItem mismyRestoreDftColumns;

	private ToolStripSeparator toolStripSeparator15;

	private ToolStripMenuItem mismySmyOpt;

	private ContextMenuStrip cmsPerformance;

	private ToolStripMenuItem mipfmColumnsSetup;

	private ToolStripMenuItem mipfmRestoreDftColumns;

	public LclTabControl tcChrom;

	public TabPage tpResults;

	public TabPage tpSummary;

	public TabPage tpPerformance;

	public LclGridView gvRltsDad;

	public LclGridView gvRltsGpc;

	public LclGridView gvRltsGnl;

	public DataGridView dgNMHC;

	public LclExpressLabel lbRltExpress;

	private ToolStripMenuItem mirlAddAllComponent;

	public TabPage tpCalorific;

	private GroupBox groupBox9;

	private Label labLJYL;

	private Label labLJWD;

	private Label labXDMD;

	private Label labMD;

	private Label labLHB;

	private Label labHHB;

	private Label labLRZ;

	private Label labHRZ;

	private Label label93;

	private Label label92;

	private Label label91;

	private Label label90;

	private Label label89;

	private Label label88;

	private Label label87;

	private Label label86;

	private Label label85;

	private LclGridView lclGvPerformStatic => gvPerformStatic;

	private LclGridView lclGvRltsGnl => gvRltsGnl;

	public SetslbExplainTextHandler mySetslbExplainText { get; set; }

	public BoolNoneHandler GetHasChrom { get; set; }

	public BoolNoneHandler GetcbNMHCChecked { get; set; }

	public GetChromatogramListHandler GetChromatogramList { get; set; }

	public GetChromatogramHandler GetChromatogram { get; set; }

	public GetChromDisplayHandler GetChromDisplay { get; set; }

	public GetSmyTabOptHandler GetSmyTabOpt { get; set; }

	private bool IsCbNMHCChecked
	{
		get
		{
			if (GetcbNMHCChecked != null)
			{
				return GetcbNMHCChecked();
			}
			return false;
		}
	}

	public bool HasChrom
	{
		get
		{
			if (GetHasChrom != null)
			{
				return GetHasChrom();
			}
			return false;
		}
	}

	private Signal CurSignal
	{
		get
		{
			if (chromDisplay_0() != null)
			{
				return chromDisplay_0().curSignal;
			}
			return null;
		}
	}

	private Chromatogram CurChrom
	{
		get
		{
			if (GetChromatogram != null)
			{
				return GetChromatogram();
			}
			return null;
		}
	}

	private Chromatogram[] chromatogram_0
	{
		get
		{
			if (GetChromatogramList != null)
			{
				return GetChromatogramList();
			}
			return null;
		}
	}

	public event EventHandler OnDisDpRefresh;

	public event EventHandler OnAddAllCompnent;

	private ChromDisplay chromDisplay_0()
	{
		if (GetChromDisplay != null)
		{
			return GetChromDisplay();
		}
		return null;
	}

	private SmyTabOpt smyTabOpt_0()
	{
		if (GetSmyTabOpt != null)
		{
			return GetSmyTabOpt();
		}
		return null;
	}

	public ChromFormDataGrid()
	{
		InitializeComponent();
		if (!IsDesignMode())
		{
		}
	}

	public static bool IsDesignMode()
	{
		return false;
	}

	private void ChromFormDataGrid_Load(object sender, EventArgs e)
	{
		if (!IsDesignMode())
		{
			Loading();
			LoadLanguage();
			tpCalorific.Parent = null;
			m_bLoading = false;
		}
	}

	public void InitFm()
	{
		frmParam.bEnNMHC = false;
		if (frmParam.bEnNMHC)
		{
			LogMgr.Instance.Write2RunLog("ChromFormDataGrid.InitFm() 1");
			gvRltsGnl.Visible = false;
			gvRltsDad.Visible = false;
			gvRltsGpc.Visible = false;
			dgNMHC.Visible = true;
			dgNMHC.Dock = DockStyle.Fill;
			dgNMHC.BringToFront();
			if (dgNMHC.Rows.Count < 3)
			{
				dgNMHC.Rows.Add(Lang.PS("总烃", "Hyd"), "000.0", 0, 0);
				dgNMHC.Rows.Add(Lang.PS("甲烷", "CH4"), "000.0", 0, 0);
				dgNMHC.Rows.Add(Lang.PS("氧气", "02"), "000.0", 0, 0);
				dgNMHC.Rows.Add(Lang.PS("总烃去氧", "02"), "000.0", 0, 0);
				dgNMHC.Rows.Add(Lang.PS("非甲烷总烃(以碳计)", "NMHC"), "000.0", 0, 0);
				dgNMHC.Rows.Add(Lang.PS("非甲烷总烃(以甲烷计)", "NMHC"), "000.0", 0, 0);
			}
		}
		else
		{
			gvRltsGnl.Visible = true;
			dgNMHC.Visible = false;
			LogMgr.Instance.Write2RunLog("ChromFormDataGrid.InitFm() 2");
		}
	}

	public void reLoad()
	{
		if (frmParam.bEnNMHC)
		{
			LogMgr.Instance.Write2RunLog("ChromFormDataGrid.InitFm() 3");
			gvRltsGnl.Visible = false;
			dgNMHC.Visible = true;
			dgNMHC.Dock = DockStyle.Fill;
			if (dgNMHC.Rows.Count < 3)
			{
				dgNMHC.Rows.Add(Lang.PS("总烃", "Hyd"), "000.0", 0, 0);
				dgNMHC.Rows.Add(Lang.PS("甲烷", "CH4"), "000.0", 0, 0);
				dgNMHC.Rows.Add(Lang.PS("氧气", "02"), "000.0", 0, 0);
				dgNMHC.Rows.Add(Lang.PS("总烃去氧", "02"), "000.0", 0, 0);
				dgNMHC.Rows.Add(Lang.PS("非甲烷总烃(以碳计)", "NMHC"), "000.0", 0, 0);
			}
		}
		else
		{
			LogMgr.Instance.Write2RunLog("ChromFormDataGrid.InitFm() 4");
			gvRltsGnl.Visible = true;
			dgNMHC.Visible = false;
		}
	}

	public void InitFmPeak()
	{
		if (Ch4Ctrl.selfCtrl == null || !Ch4Ctrl.selfCtrl.cbEnNMHC.Checked)
		{
			return;
		}
		LogMgr.Instance.Write2RunLog("InitFmPeak");
		float num = 0f;
		float num2 = 0f;
		float num3 = 0f;
		CH4Param cH4Param = CH4Param.Create();
		gvRltsGnl.Visible = false;
		dgNMHC.Visible = true;
		dgNMHC.Dock = DockStyle.Fill;
		if (CurChrom == null)
		{
			return;
		}
		float threshold = CurChrom.userArchives[0].integ.Threshold;
		Peak[] array = CurChrom.GetPeakAllCompound();
		if (array == null)
		{
			array = CurChrom.GetRltPeaks(combine: false);
		}
		if (array.Length == 0)
		{
			array = CurChrom.RltPeaks;
			dgNMHC.Rows[0].Cells[1].Value = 0.ToString("F" + Class49.int_8);
			dgNMHC.Rows[0].Cells[2].Value = 0.ToString("F" + Class49.int_8);
			dgNMHC.Rows[0].Cells[3].Value = 0.ToString("F" + Class49.int_8);
			dgNMHC.Rows[0].Cells[4].Value = 0.ToString("F" + Class49.int_8);
			dgNMHC.Rows[1].Cells[1].Value = 0.ToString("F" + Class49.int_8);
			dgNMHC.Rows[1].Cells[2].Value = 0.ToString("F" + Class49.int_8);
			dgNMHC.Rows[1].Cells[3].Value = 0.ToString("F" + Class49.int_8);
			dgNMHC.Rows[1].Cells[4].Value = 0.ToString("F" + Class49.int_8);
			dgNMHC.Rows[2].Cells[1].Value = 0.ToString("F" + Class49.int_8);
			dgNMHC.Rows[2].Cells[2].Value = 0.ToString("F" + Class49.int_8);
			dgNMHC.Rows[2].Cells[3].Value = 0.ToString("F" + Class49.int_8);
			dgNMHC.Rows[2].Cells[4].Value = 0.ToString("F" + Class49.int_8);
			dgNMHC.Rows[3].Cells[1].Value = 0.ToString("F" + Class49.int_8);
			dgNMHC.Rows[3].Cells[2].Value = 0.ToString("F" + Class49.int_8);
			dgNMHC.Rows[3].Cells[3].Value = 0.ToString("F" + Class49.int_8);
			dgNMHC.Rows[3].Cells[4].Value = 0.ToString("F" + Class49.int_8);
			dgNMHC.Rows[4].Cells[1].Value = 0.ToString("F" + Class49.int_8);
			dgNMHC.Rows[4].Cells[2].Value = 0.ToString("F" + Class49.int_8);
			dgNMHC.Rows[4].Cells[3].Value = 0.ToString("F" + Class49.int_8);
			dgNMHC.Rows[4].Cells[4].Value = 0.ToString("F" + Class49.int_8);
			return;
		}
		if (array.Length != 0)
		{
			num = array[0].amount;
			dgNMHC.Rows[0].Cells[1].Value = array[0].pkRT.ToString("F" + Class49.int_8);
			dgNMHC.Rows[0].Cells[2].Value = array[0].height.ToString("F" + Class49.int_8);
			dgNMHC.Rows[0].Cells[3].Value = array[0].area.ToString("F" + Class49.int_8);
			dgNMHC.Rows[0].Cells[4].Value = array[0].amount.ToString("F" + Class49.int_8);
		}
		if (array.Length > 1)
		{
			num2 = array[1].amount;
			dgNMHC.Rows[1].Cells[1].Value = array[1].pkRT.ToString("F" + Class49.int_8);
			dgNMHC.Rows[1].Cells[2].Value = array[1].height.ToString("F" + Class49.int_8);
			dgNMHC.Rows[1].Cells[3].Value = array[1].area.ToString("F" + Class49.int_8);
			dgNMHC.Rows[1].Cells[4].Value = array[1].amount.ToString("F" + Class49.int_8);
		}
		float[] array2 = array[0].compound.eFunc.Calcu_amountF(CurChrom.mtdSetup.chromInfoR.UvwsStartT);
		float num4 = float.Parse(array[0].amount.ToString("F" + Class49.int_8));
		float num5 = float.Parse(array[1].amount.ToString("F" + Class49.int_8));
		float num6 = float.Parse(array2[0].ToString("F" + Class49.int_8));
		dgNMHC.Rows[2].Cells[1].Value = "--";
		dgNMHC.Rows[2].Cells[2].Value = "--";
		dgNMHC.Rows[2].Cells[3].Value = CurChrom.mtdSetup.chromInfoR.UvwsStartT.ToString("F" + Class49.int_8);
		dgNMHC.Rows[2].Cells[4].Value = array2[0].ToString("F" + Class49.int_8);
		dgNMHC.Rows[3].Cells[1].Value = "--";
		dgNMHC.Rows[3].Cells[2].Value = "--";
		dgNMHC.Rows[3].Cells[3].Value = (array[0].area - CurChrom.mtdSetup.chromInfoR.UvwsStartT).ToString("F" + Class49.int_8);
		dgNMHC.Rows[3].Cells[4].Value = (num4 - array2[0]).ToString("F" + Class49.int_8);
		dgNMHC.Rows[4].Cells[1].Value = "--";
		dgNMHC.Rows[4].Cells[2].Value = "--";
		dgNMHC.Rows[4].Cells[3].Value = "--";
		num3 = num4 - num5 - num6;
		if (num3 < 0f)
		{
			num3 = 0f;
		}
		dgNMHC.Rows[4].Cells[4].Value = (num3 * 0.75f).ToString("F" + Class49.int_8);
		dgNMHC.Rows[5].Cells[1].Value = "--";
		dgNMHC.Rows[5].Cells[2].Value = "--";
		dgNMHC.Rows[5].Cells[3].Value = "--";
		dgNMHC.Rows[5].Cells[4].Value = num3.ToString("F" + Class49.int_8);
	}

	private void Loading()
	{
		gvRltsDad.Dock = DockStyle.Fill;
		gvRltsGpc.Dock = DockStyle.Fill;
		gvRltsGnl.Dock = DockStyle.Fill;
		integRow_1.oprtStyle = IntegOprtStyle.Noise;
		integRow_0.oprtStyle = IntegOprtStyle.Drift;
		if (AddGvRltsGnlColumn())
		{
			SetGvRltsGnlHeaderText(gvRltsGnl);
			SetGvRltsGnlHeaderText(gvRltsGpc);
			SetGvRltsGnlHeaderText(gvRltsDad);
		}
		gvPerformStatic.Dock = DockStyle.Fill;
		AddGvPerformStaticColumn();
		SetGvPerformStaticHeaderText(gvPerformStatic);
		bool flag = gvRltsGnl.LoadFromManager();
		bool flag2 = gvRltsGpc.LoadFromManager();
		bool flag3 = gvRltsDad.LoadFromManager();
		if (!flag && !flag2 && !flag3)
		{
			loadColumnList_gvRltsGnl();
		}
		gvRltsGpc.shieldBeginEdit = true;
		gvRltsGnl.shieldBeginEdit = true;
		gvRltsDad.shieldBeginEdit = true;
		AddGvSummaryColumn();
		AddGvSummaryColumn(InstruStyle.LC);
		AddGvSummaryColumn(InstruStyle.GPC);
		AddGvSummaryColumn(InstruStyle.PDA);
		SetGvSummaryHeaderText(gvSummary.commonColumns);
		SetGvSummaryHeaderText(gvSummary.smyGnlColumns);
		SetGvSummaryHeaderText(gvSummary.smyGpcColumns);
		SetGvSummaryHeaderText(gvSummary.smyDadColumns);
		lbExpress.Left = 1;
		lbExpress.Height = 1;
		lbExpress.Visible = false;
		lbExpress.BringToFront();
		if (!gvSummary.LoadFromManager())
		{
			loadColumnList_gvSummary();
		}
		SetGvSummaryRowValue();
		InitFm();
	}

	private List<string> getColumnStringList(string strColumnList)
	{
		List<string> source = strColumnList.Split(',').ToList();
		return source.Where((string x) => x != "").ToList();
	}

	private void loadColumnList_gvRltsGnl()
	{
		string strShowColumn_GvRltsGnl = sysParam.strShowColumn_GvRltsGnl;
		List<string> columnStringList = getColumnStringList(strShowColumn_GvRltsGnl);
		if (strShowColumn_GvRltsGnl == "" || columnStringList.Count < 1)
		{
			SetGvRltsGnlShowColumn();
		}
		else
		{
			loadColumnList_gvRltsGnl(columnStringList);
		}
	}

	private void loadColumnList_gvRltsGnl(List<string> strlist)
	{
		gvRltsGnl.ini_SetFirstVisibleColumn(strlist[0]);
		for (int i = 1; i < strlist.Count; i++)
		{
			gvRltsGnl.ini_SetNextVisibleColumn(strlist[i]);
		}
		gvRltsGnl.ini_FinishVisibleColumn();
		if (lclGvRltsGnl != null)
		{
			FillGvRltsGnlTable();
		}
	}

	private void saveColumnList_gvRltsGnl()
	{
		string text = "";
		for (int i = 0; i < gvRltsGnl.showColumns.Length; i++)
		{
			if (text != "")
			{
				text += ",";
			}
			text += gvRltsGnl.showColumns[i].Name;
		}
		sysParam.strShowColumn_GvRltsGnl = text;
		sysParam.SaveParam();
	}

	private void loadColumnList_gvSummary()
	{
		string strShowColumn_GvSummary = sysParam.strShowColumn_GvSummary;
		string strShowColumn_GvSummaryGeneral = sysParam.strShowColumn_GvSummaryGeneral;
		List<string> columnStringList = getColumnStringList(strShowColumn_GvSummary);
		List<string> columnStringList2 = getColumnStringList(strShowColumn_GvSummaryGeneral);
		if (strShowColumn_GvSummary == "" || columnStringList.Count < 1 || strShowColumn_GvSummaryGeneral == "" || columnStringList2.Count < 1)
		{
			mismySmyOpt_Click(mismyRestoreDftColumns, null);
		}
		else
		{
			loadColumnList_gvSummary(columnStringList2, columnStringList);
		}
	}

	private void loadColumnList_gvSummary(List<string> strlistGeneral, List<string> strlist)
	{
		int count = strlistGeneral.Count;
		gvSummary.ArrayComSHColumns(show: true, count);
		for (int i = 0; i < count; i++)
		{
			gvSummary.AddComShowLink(i, Lang.PS(strlistGeneral[i]));
		}
		gvSummary.FinishComHideLinks();
		int count2 = strlist.Count;
		InstruStyle instruStyle_ = InstruStyle.LC;
		gvSummary.ArraySmySHColumns(instruStyle_, show: true, count2);
		for (int j = 0; j < count2; j++)
		{
			gvSummary.AddSmyShowLink(instruStyle_, j, strlist[j]);
		}
		smyTabOpt_0().smyHdrPara = SmyHdrPara.Cmpd_Para;
		SetGvSummaryRowValue();
	}

	private void saveColumnList_gvSummary()
	{
		string text = "";
		for (int i = 0; i < gvSummary.showComColumns.Length; i++)
		{
			if (text != "")
			{
				text += ",";
			}
			text += gvSummary.showComColumns[i].Name;
		}
		sysParam.strShowColumn_GvSummaryGeneral = text;
		string text2 = "";
		for (int j = 0; j < gvSummary.showGnlColumns.Length; j++)
		{
			if (text2 != "")
			{
				text2 += ",";
			}
			text2 += gvSummary.showGnlColumns[j].Name;
		}
		sysParam.strShowColumn_GvSummary = text2;
		sysParam.SaveParam();
	}

	public void LoadLanguage()
	{
		mirlAddAllComponent.Text = Lang.PS("全部添加入组份校正表", "Add all components to the calibration list");
		mirltsResetCmpdNames.Text = Lang.PS("清除组份名", "Clear Cmpds Name");
		tpResults.Text = Lang.PS("结果", "Results");
		mirltsColumnsSetup.Text = Lang.PS("列设置...", "Columns Setup...");
		mirltsRestoreDftColumns.Text = Lang.PS("恢复默认列设置", "Restore Default Columns");
		tpSummary.Text = Lang.PS("总结", "Summary");
		mismyColumnsSetup.Text = Lang.PS("列设置...", "Columns Setup...");
		mismyRestoreDftColumns.Text = Lang.PS("恢复默认列设置", "Restore Default Columns");
		mismySmyOpt.Text = Lang.PS("总结选项...", "Summary Options...");
		tpPerformance.Text = Lang.PS("柱效", "Performance");
		mipfmColumnsSetup.Text = Lang.PS("列设置...", "Columns Setup...");
		mipfmRestoreDftColumns.Text = Lang.PS("恢复默认列设置", "Restore Default Columns");
	}

	public void DisDpRefresh()
	{
		if (this.OnDisDpRefresh != null)
		{
			this.OnDisDpRefresh(this, new EventArgs());
		}
	}

	public void GetDisMouseLgFmtPosition(ref string disMouseLgFmtX, ref string disMouseLgFmtY)
	{
		disMouseLgFmtX = chromDisplay_0().disMouseLgFmtX;
		disMouseLgFmtY = chromDisplay_0().disMouseLgFmtY;
	}

	public Color GetCurSingleColor()
	{
		return chromDisplay_0().curSignal.disColor;
	}

	public void SetslbExplainText(string strText)
	{
		if (mySetslbExplainText != null)
		{
			mySetslbExplainText(strText);
		}
	}

	public void refresh_once()
	{
		SuspendLayout();
		tcChrom.TabPages.Clear();
		gvRltsDad.Visible = false;
		gvRltsGpc.Visible = false;
		gvRltsGnl.Visible = false;
		tcChrom.TabPages.Add(tpResults);
		tcChrom.TabPages.Add(tpSummary);
		tcChrom.TabPages.Add(tpPerformance);
		InitFm();
		if (!gvSummary.LoadFromManager())
		{
			mismySmyOpt_Click(mismyRestoreDftColumns, null);
		}
		SetGvSummaryRowValue();
		ResumeLayout();
	}

	public Peak[] getMaxPeakTower()
	{
		return getPeak();
	}

	public Peak[] getPeak()
	{
		if (!HasChrom)
		{
			lbRltExpress.Text = "[]";
			lclGvRltsGnl.RowCount = 0;
			return null;
		}
		lbRltExpress.ForeColor = CurSignal.disColor;
		string disMouseLgFmtX = chromDisplay_0().disMouseLgFmtX;
		string disMouseLgFmtY = chromDisplay_0().disMouseLgFmtY;
		string text = CurChrom.chromInfo.cclCalcu.ToString();
		bool flag;
		if (flag = CurChrom.integ.GetNDRow(ref integRow_1) && integRow_1.success)
		{
			string text2 = text;
			text = text2 + Lang.PS("\n噪音 (", "\nnoise (") + integRow_1.timeA.ToString(disMouseLgFmtX) + " - " + integRow_1.timeB.ToString(disMouseLgFmtX) + "min): " + method_36(integRow_1);
		}
		if (CurChrom.integ.GetNDRow(ref integRow_0) && integRow_0.success)
		{
			string text3 = text;
			text = text3 + (flag ? "  " : "\n") + Lang.PS("飘移 (", "drift (") + integRow_0.timeA.ToString(disMouseLgFmtX) + " - " + integRow_0.timeB.ToString(disMouseLgFmtX) + "min): " + integRow_0.value.ToString(disMouseLgFmtY) + " [" + integRow_0.ValueUnitStr + "]";
		}
		lbRltExpress.Text = text;
		bool_3 = true;
		lclGvRltsGnl.SuspendLayout();
		SetGvRltsGnlRowValue(lclGvRltsGnl, -1, "Amount", Lang.PS("浓度", "Amount") + "\n[" + CurChrom.AmountUnit + "]");
		lclGvRltsGnl.Columns["Cus1"].HeaderText = CurChrom.cus1_name;
		lclGvRltsGnl.Columns["Cus2"].HeaderText = CurChrom.cus2_name;
		return CurChrom.GetRltPeaks(combine: false);
	}

	private string GetPeakStyleName(PeakStyle peakStyle_0)
	{
		return peakStyle_0 switch
		{
			PeakStyle.Single => Lang.PS("单峰", "Single"), 
			PeakStyle.Overlap => Lang.PS("重叠峰", "Overlap"), 
			PeakStyle.Shoulder => Lang.PS("肩峰", "Shoulder"), 
			PeakStyle.SO => Lang.PS("肩叠峰", "Sh.Over"), 
			_ => "", 
		};
	}

	private void SetGvPerformStaticHeaderText(LclGridView lclGridView_2)
	{
		if (lclGridView_2.ColumnCount == 0)
		{
			return;
		}
		for (int i = 0; i < lclGridView_2.ColumnCount; i++)
		{
			string name;
			switch (name = lclGridView_2.Columns[i].Name)
			{
			case "RetenTime":
				lclGridView_2.Columns[i].HeaderText = Lang.PS("保留时间\n[min]", "Reten Time\n[min]");
				break;
			case "Efficiency":
				lclGridView_2.Columns[i].HeaderText = Lang.PS("效率\n[th.pl]", "Efficiency\n[th.pl]");
				break;
			case "Eff_ColL":
				lclGridView_2.Columns[i].HeaderText = Lang.PS("柱效\n[t.p./m]", "Eff/1\n[t.p./m]");
				break;
			case "HETP":
				lclGridView_2.Columns[i].HeaderText = Lang.PS("HETP\n[mm]", "HETP\n[mm]");
				break;
			case "SymTail":
				lclGridView_2.Columns[i].HeaderText = Lang.PS("对称性/\n拖尾[-]", "Symmetry/\nTailing [-]");
				break;
			case "CmpdName":
				lclGridView_2.Columns[i].HeaderText = Lang.PS("组份名", "Compound Name");
				break;
			case "Centroid":
				lclGridView_2.Columns[i].HeaderText = Lang.PS("重心\n[min]", "Centroid\n[min]");
				break;
			case "Variance":
				lclGridView_2.Columns[i].HeaderText = "_Variance";
				break;
			case "Skew":
				lclGridView_2.Columns[i].HeaderText = "_Skew";
				break;
			case "Excess":
				lclGridView_2.Columns[i].HeaderText = "_Excess";
				break;
			case "WO5":
				lclGridView_2.Columns[i].HeaderText = Lang.PS("半峰宽\n[min]", "WO5\n[min]");
				break;
			case "Asymmetry":
				lclGridView_2.Columns[i].HeaderText = Lang.PS("对称性\n[-]", "Asymmetry\n[-]");
				break;
			case "Capacity":
				lclGridView_2.Columns[i].HeaderText = Lang.PS("容量\n[-]", "Capacity\n[-]");
				break;
			case "Resolution":
				lclGridView_2.Columns[i].HeaderText = Lang.PS("分辨率\n[-]", "Resolution\n[-]");
				break;
			}
		}
	}

	public void SetGvRltsGnlHeaderText(LclGridView lclGridView_2)
	{
		for (int i = 0; i < lclGridView_2.ColumnCount; i++)
		{
			string name;
			switch (name = lclGridView_2.Columns[i].Name)
			{
			case "RetenTime":
				lclGridView_2.Columns[i].HeaderText = Lang.PS("保留时间\n[min]", "Reten Time\n[min]");
				break;
			case "StartTime":
				lclGridView_2.Columns[i].HeaderText = Lang.PS("开始时间\n[min]", "Start Time\n[min]");
				break;
			case "EndTime":
				lclGridView_2.Columns[i].HeaderText = Lang.PS("结束时间\n[min]", "End Time\n[min]");
				break;
			case "StartValue":
				lclGridView_2.Columns[i].HeaderText = Lang.PS("开始值") + "\n[" + Class49.MesureUnit() + "]";
				break;
			case "EndValue":
				lclGridView_2.Columns[i].HeaderText = Lang.PS("结束值") + "\n[" + Class49.MesureUnit() + "]";
				break;
			case "PeakStyle":
				lclGridView_2.Columns[i].HeaderText = Lang.PS("峰类别", "Peak Style");
				break;
			case "Area":
				lclGridView_2.Columns[i].HeaderText = Lang.PS("面积") + "\n[" + Class49.MesureUnit() + ".s]";
				break;
			case "Height":
				lclGridView_2.Columns[i].HeaderText = Lang.PS("高度") + "\n[" + Class49.MesureUnit() + "]";
				break;
			case "AreaPer":
				lclGridView_2.Columns[i].HeaderText = Lang.PS("面积\n[%]", "Area\n[%]");
				break;
			case "HeightPer":
				lclGridView_2.Columns[i].HeaderText = Lang.PS("高度\n[%]", "Height\n[%]");
				break;
			case "WO5":
				lclGridView_2.Columns[i].HeaderText = Lang.PS("半峰宽\n[min]", "WO5\n[min]");
				break;
			case "RespBase":
				lclGridView_2.Columns[i].HeaderText = Lang.PS("响应基础", "Resp. Base");
				break;
			case "Amount":
				lclGridView_2.Columns[i].HeaderText = Lang.PS("浓度", "Amount");
				break;
			case "AmountPer":
				lclGridView_2.Columns[i].HeaderText = Lang.PS("浓度\n[%]", "Amount\n[%]");
				break;
			case "PeakType":
				lclGridView_2.Columns[i].HeaderText = Lang.PS("峰类型", "Peak Type");
				break;
			case "CmpdName":
				lclGridView_2.Columns[i].HeaderText = Lang.PS("组份名", "Compound Name");
				lclGridView_2.Columns[i].ReadOnly = false;
				break;
			case "RetenIndex":
				lclGridView_2.Columns[i].HeaderText = Lang.PS("保留索引\n[-]", "Reten. Idx\n[-]");
				break;
			case "PeakPurity":
				lclGridView_2.Columns[i].HeaderText = Lang.PS("峰纯度", "Peak Purity");
				break;
			case "NameMatch":
				lclGridView_2.Columns[i].HeaderText = Lang.PS("名匹配", "Name Match");
				break;
			case "BestMatchName":
				lclGridView_2.Columns[i].HeaderText = Lang.PS("最佳匹配名", "Best Match Name");
				break;
			case "BestMatch":
				lclGridView_2.Columns[i].HeaderText = Lang.PS("最佳匹配", "Best Match");
				break;
			case "MaxRT":
				lclGridView_2.Columns[i].HeaderText = Lang.PS("最大RT\n[min]", "Max RT\n[min]");
				break;
			case "StartRT":
				lclGridView_2.Columns[i].HeaderText = Lang.PS("开始RT\n[min]", "Start RT\n[min]");
				break;
			case "EndRT":
				lclGridView_2.Columns[i].HeaderText = Lang.PS("结束RT\n[min]", "End RT\n[min]");
				break;
			case "FlowRateCorr":
				lclGridView_2.Columns[i].HeaderText = Lang.PS("流速校正", "Flow Rate\nCorrection");
				break;
			case "ResolutionEP":
				lclGridView_2.Columns[i].HeaderText = Lang.PS("峰分离度");
				break;
			case "GasAmount":
				lclGridView_2.Columns[i].HeaderText = Lang.PS("标气浓度");
				break;
			}
		}
	}

	private void SetGvSummaryHeaderText(DataGridViewColumn[] dataGridViewColumn_0)
	{
		for (int i = 0; i < dataGridViewColumn_0.Length; i++)
		{
			dataGridViewColumn_0[i].HeaderText = getSmyHeaderText(dataGridViewColumn_0[i].Name);
		}
	}

	private bool AddGvPerformStaticColumn()
	{
		if (gvPerformStatic.ColumnCount != 0)
		{
			return false;
		}
		gvPerformStatic.AddLclTextBoxColumn("RetenTime", 60);
		gvPerformStatic.AddLclTextBoxColumn("WO5", 60);
		gvPerformStatic.AddLclTextBoxColumn("Asymmetry", 60);
		gvPerformStatic.AddLclTextBoxColumn("Capacity", 60);
		gvPerformStatic.AddLclTextBoxColumn("Efficiency", 70);
		gvPerformStatic.AddLclTextBoxColumn("Eff_ColL", 75);
		gvPerformStatic.AddLclTextBoxColumn("SymTail", 60);
		gvPerformStatic.AddLclTextBoxColumn("Resolution", 60);
		gvPerformStatic.AddLclTextBoxColumn("CmpdName", 130, StringAlignment.Near);
		return true;
	}

	private bool AddGvRltsGnlColumn()
	{
		if (gvRltsGnl.ColumnCount != 0)
		{
			return false;
		}
		sysParam = SystemParam.Create();
		gvRltsGnl.textBox_dftReadOnly = true;
		gvRltsGnl.AddLclTextBoxColumn("RetenTime", 70, 4, StringAlignment.Far, readOnly: false);
		gvRltsGnl.AddLclTextBoxColumn("StartTime", 70);
		gvRltsGnl.AddLclTextBoxColumn("EndTime", 70);
		gvRltsGnl.AddLclTextBoxColumn("StartValue", 70);
		gvRltsGnl.AddLclTextBoxColumn("EndValue", 70);
		gvRltsGnl.AddLclTextBoxColumn("PeakStyle", 70, StringAlignment.Near);
		gvRltsGnl.AddLclTextBoxColumn("Area", 70);
		gvRltsGnl.AddLclTextBoxColumn("Height", 70);
		gvRltsGnl.AddLclTextBoxColumn("WO5", 70);
		gvRltsGnl.AddLclTextBoxColumn("RespBase", 70, StringAlignment.Center);
		gvRltsGnl.AddLclTextBoxColumn("Amount", 70, Class49.int_8, StringAlignment.Far, readOnly: false);
		gvRltsGnl.AddLclTextBoxColumn("PeakType", 70);
		gvRltsGnl.AddLclTextBoxColumn("CmpdName", 110, 0, StringAlignment.Near, readOnly: false);
		gvRltsGnl.AddLclTextBoxColumn("RetenIndex", 70);
		gvRltsGnl.AddLclTextBoxColumn("Cus1", 70);
		gvRltsGnl.AddLclTextBoxColumn("Cus2", 70);
		gvRltsGnl.AddLclTextBoxColumn("GasAmount", 70, Class49.int_8, StringAlignment.Far, readOnly: false);
		gvRltsGnl.AddLclTextBoxColumn("AreaPer", 70, Class49.int_8);
		gvRltsGnl.AddLclTextBoxColumn("HeightPer", 70, Class49.int_8);
		gvRltsGnl.AddLclTextBoxColumn("AmountPer", 70, Class49.int_8, StringAlignment.Far, readOnly: false);
		gvRltsGnl.AddLclTextBoxColumn("ResolutionEP", 70);
		gvRltsDad.textBox_dftReadOnly = true;
		gvRltsDad.AddLclTextBoxColumn("RetenTime", 70, 0, StringAlignment.Far, readOnly: false);
		gvRltsDad.AddLclTextBoxColumn("StartTime", 70);
		gvRltsDad.AddLclTextBoxColumn("EndTime", 70);
		gvRltsDad.AddLclTextBoxColumn("StartValue", 70);
		gvRltsDad.AddLclTextBoxColumn("EndValue", 70);
		gvRltsDad.AddLclTextBoxColumn("PeakPurity", 70);
		gvRltsDad.AddLclTextBoxColumn("NameMatch", 70);
		gvRltsDad.AddLclTextBoxColumn("BestMatchName", 110);
		gvRltsDad.AddLclTextBoxColumn("BestMatch", 70);
		gvRltsDad.AddLclTextBoxColumn("WO5", 70);
		gvRltsDad.AddLclTextBoxColumn("RespBase", 70);
		gvRltsDad.AddLclTextBoxColumn("Amount", 70, 0, StringAlignment.Far, readOnly: false);
		gvRltsDad.AddLclTextBoxColumn("AmountPer", 70, Class49.int_8, StringAlignment.Far, readOnly: false);
		gvRltsDad.AddLclTextBoxColumn("PeakType", 70);
		gvRltsDad.AddLclTextBoxColumn("CmpdName", 110, 0, StringAlignment.Near, readOnly: false);
		gvRltsDad.AddLclTextBoxColumn("RetenIndex", 70);
		gvRltsDad.AddLclTextBoxColumn("Area", 70);
		gvRltsDad.AddLclTextBoxColumn("Height", 70);
		gvRltsDad.AddLclTextBoxColumn("AreaPer", 70, Class49.int_8);
		gvRltsDad.AddLclTextBoxColumn("HeightPer", 70, Class49.int_8);
		gvRltsGpc.textBox_dftReadOnly = true;
		gvRltsGpc.AddLclTextBoxColumn("CmpdName", 110);
		gvRltsGpc.AddLclTextBoxColumn("MaxRT", 70);
		gvRltsGpc.AddLclTextBoxColumn("StartRT", 70);
		gvRltsGpc.AddLclTextBoxColumn("EndRT", 70);
		gvRltsGpc.AddLclTextBoxColumn("Mp", 70).HeaderText = "Mp";
		gvRltsGpc.AddLclTextBoxColumn("Mn", 70).HeaderText = "Mn";
		gvRltsGpc.AddLclTextBoxColumn("Mw", 70).HeaderText = "Mw";
		gvRltsGpc.AddLclTextBoxColumn("Mz", 70).HeaderText = "Mz";
		gvRltsGpc.AddLclTextBoxColumn("Mz1", 70).HeaderText = "Mz1";
		gvRltsGpc.AddLclTextBoxColumn("Mv", 70).HeaderText = "Mv";
		gvRltsGpc.AddLclTextBoxColumn("PD", 70).HeaderText = "PD";
		gvRltsGpc.AddLclTextBoxColumn("Area", 70);
		gvRltsGpc.AddLclTextBoxColumn("Height", 70);
		gvRltsGpc.AddLclTextBoxColumn("AreaPer", 70, Class49.int_8);
		gvRltsGpc.AddLclTextBoxColumn("HeightPer", 70, Class49.int_8);
		gvRltsGpc.AddLclTextBoxColumn("FlowRateCorr", 70);
		return true;
	}

	private bool AddGvSummaryColumn()
	{
		if (gvSummary.ColumnCount != 0)
		{
			return false;
		}
		int decimalPlaces = 3;
		gvSummary.ArrayComColumns(12);
		gvSummary.AddComTB(0, Lang.PS("谱图名称", "ChromName"), 140, 0, StringAlignment.Near);
		gvSummary.AddComTB(1, Lang.PS("样品ID", "SampleID"), 50, 0, StringAlignment.Near);
		gvSummary.AddComTB(2, Lang.PS("样品", "Sample"), 110, 0, StringAlignment.Near);
		gvSummary.AddComTB(3, Lang.PS("样品浓度", "SampleAmount"), 60, decimalPlaces, StringAlignment.Far);
		gvSummary.AddComTB(4, Lang.PS("样品稀释", "SampleDilution"), 60, decimalPlaces, StringAlignment.Far);
		gvSummary.AddComTB(5, Lang.PS("内标浓度", "ISTDAmount"), 60, decimalPlaces, StringAlignment.Far);
		gvSummary.AddComTB(6, Lang.PS("体积", "InjVol"), 60, decimalPlaces, StringAlignment.Far);
		gvSummary.AddComTB(7, Lang.PS("非保留时间", "ColumnUT"), 60, decimalPlaces, StringAlignment.Far);
		gvSummary.AddComTB(8, Lang.PS("柱长", "ColumnLength"), 60, decimalPlaces, StringAlignment.Far);
		gvSummary.AddComTB(9, Lang.PS("噪声", "Noise"), 60, decimalPlaces, StringAlignment.Far);
		gvSummary.AddComTB(10, Lang.PS("漂移", "Drift"), 60, decimalPlaces, StringAlignment.Far);
		gvSummary.AddComTB(11, Lang.PS("峰分离度", "ResolutionEP"), 60, decimalPlaces, StringAlignment.Far);
		gvSummary.ArraySmyColumns(InstruStyle.LC, 17);
		gvSummary.AddSmyTB(InstruStyle.LC, 0, "StartTime", 70, decimalPlaces, StringAlignment.Far);
		gvSummary.AddSmyTB(InstruStyle.LC, 1, "EndTime", 70, decimalPlaces, StringAlignment.Far);
		gvSummary.AddSmyTB(InstruStyle.LC, 2, "StartValue", 70, decimalPlaces, StringAlignment.Far);
		gvSummary.AddSmyTB(InstruStyle.LC, 3, "EndValue", 70, decimalPlaces, StringAlignment.Far);
		gvSummary.AddSmyTB(InstruStyle.LC, 4, "WO5", 70, decimalPlaces, StringAlignment.Far);
		gvSummary.AddSmyTB(InstruStyle.LC, 5, "RespBase", 70, decimalPlaces, StringAlignment.Center);
		gvSummary.AddSmyTB(InstruStyle.LC, 6, "RetenIndex", 70, decimalPlaces, StringAlignment.Far);
		gvSummary.AddSmyTB(InstruStyle.LC, 7, "Area", 70, decimalPlaces, StringAlignment.Far);
		gvSummary.AddSmyTB(InstruStyle.LC, 8, "Height", 70, decimalPlaces, StringAlignment.Far);
		gvSummary.AddSmyTB(InstruStyle.LC, 9, "AreaPer", 70, decimalPlaces, StringAlignment.Far);
		gvSummary.AddSmyTB(InstruStyle.LC, 10, "HeightPer", 70, decimalPlaces, StringAlignment.Far);
		gvSummary.AddSmyTB(InstruStyle.LC, 11, "RetenTime", 70, decimalPlaces, StringAlignment.Far);
		gvSummary.AddSmyTB(InstruStyle.LC, 12, "Amount", 70, decimalPlaces, StringAlignment.Far);
		gvSummary.AddSmyTB(InstruStyle.LC, 13, "AmountPer", 70, decimalPlaces, StringAlignment.Far);
		gvSummary.AddSmyTB(InstruStyle.LC, 14, "PeakType", 70, decimalPlaces, StringAlignment.Far);
		gvSummary.AddSmyTB(InstruStyle.LC, 15, "CmpdName", 110, 0, StringAlignment.Near);
		gvSummary.AddSmyTB(InstruStyle.LC, 16, "ResolutionEP", 110, 0, StringAlignment.Far);
		gvSummary.ArraySmyColumns(InstruStyle.PDA, 20);
		gvSummary.AddSmyTB(InstruStyle.PDA, 0, "StartTime", 70, decimalPlaces, StringAlignment.Far);
		gvSummary.AddSmyTB(InstruStyle.PDA, 1, "EndTime", 70, decimalPlaces, StringAlignment.Far);
		gvSummary.AddSmyTB(InstruStyle.PDA, 2, "StartValue", 70, decimalPlaces, StringAlignment.Far);
		gvSummary.AddSmyTB(InstruStyle.PDA, 3, "EndValue", 70, decimalPlaces, StringAlignment.Far);
		gvSummary.AddSmyTB(InstruStyle.PDA, 4, "WO5", 70, decimalPlaces, StringAlignment.Far);
		gvSummary.AddSmyTB(InstruStyle.PDA, 5, "RespBase", 70, decimalPlaces, StringAlignment.Center);
		gvSummary.AddSmyTB(InstruStyle.PDA, 6, "RetenIndex", 70, decimalPlaces, StringAlignment.Far);
		gvSummary.AddSmyTB(InstruStyle.PDA, 7, "Area", 70, decimalPlaces, StringAlignment.Far);
		gvSummary.AddSmyTB(InstruStyle.PDA, 8, "Height", 70, decimalPlaces, StringAlignment.Far);
		gvSummary.AddSmyTB(InstruStyle.PDA, 9, "AreaPer", 70, decimalPlaces, StringAlignment.Far);
		gvSummary.AddSmyTB(InstruStyle.PDA, 10, "HeightPer", 70, decimalPlaces, StringAlignment.Far);
		gvSummary.AddSmyTB(InstruStyle.PDA, 11, "RetenTime", 70, decimalPlaces, StringAlignment.Far);
		gvSummary.AddSmyTB(InstruStyle.PDA, 12, "Amount", 70, decimalPlaces, StringAlignment.Far);
		gvSummary.AddSmyTB(InstruStyle.PDA, 13, "AmountPer", 70, decimalPlaces, StringAlignment.Far);
		gvSummary.AddSmyTB(InstruStyle.PDA, 14, "PeakType", 70, decimalPlaces, StringAlignment.Far);
		gvSummary.AddSmyTB(InstruStyle.PDA, 15, "CmpdName", 110, 0, StringAlignment.Near);
		gvSummary.AddSmyTB(InstruStyle.PDA, 16, "PeakPurity", 70, decimalPlaces, StringAlignment.Far);
		gvSummary.AddSmyTB(InstruStyle.PDA, 17, "NameMatch", 70, decimalPlaces, StringAlignment.Far);
		gvSummary.AddSmyTB(InstruStyle.PDA, 18, "BestMatchName", 110, 0, StringAlignment.Near);
		gvSummary.AddSmyTB(InstruStyle.PDA, 19, "BestMatch", 70, decimalPlaces, StringAlignment.Far);
		gvSummary.ArraySmyColumns(InstruStyle.GPC, 15);
		gvSummary.AddSmyTB(InstruStyle.GPC, 0, "MaxRT", 70, decimalPlaces, StringAlignment.Far);
		gvSummary.AddSmyTB(InstruStyle.GPC, 1, "StartRT", 70, decimalPlaces, StringAlignment.Far);
		gvSummary.AddSmyTB(InstruStyle.GPC, 2, "EndRT", 70, decimalPlaces, StringAlignment.Far);
		gvSummary.AddSmyTB(InstruStyle.GPC, 3, "FlowRateCorr", 70, decimalPlaces, StringAlignment.Far);
		gvSummary.AddSmyTB(InstruStyle.GPC, 4, "Mp", 70, decimalPlaces, StringAlignment.Far);
		gvSummary.AddSmyTB(InstruStyle.GPC, 5, "Mn", 70, decimalPlaces, StringAlignment.Far);
		gvSummary.AddSmyTB(InstruStyle.GPC, 6, "Mw", 70, decimalPlaces, StringAlignment.Far);
		gvSummary.AddSmyTB(InstruStyle.GPC, 7, "Mz", 70, decimalPlaces, StringAlignment.Far);
		gvSummary.AddSmyTB(InstruStyle.GPC, 8, "Mz1", 70, decimalPlaces, StringAlignment.Far);
		gvSummary.AddSmyTB(InstruStyle.GPC, 9, "Mv", 70, decimalPlaces, StringAlignment.Far);
		gvSummary.AddSmyTB(InstruStyle.GPC, 10, "PD", 70, decimalPlaces, StringAlignment.Far);
		gvSummary.AddSmyTB(InstruStyle.GPC, 11, "Area", 70, decimalPlaces, StringAlignment.Far);
		gvSummary.AddSmyTB(InstruStyle.GPC, 12, "Height", 70, decimalPlaces, StringAlignment.Far);
		gvSummary.AddSmyTB(InstruStyle.GPC, 13, "AreaPer", 70, decimalPlaces, StringAlignment.Far);
		gvSummary.AddSmyTB(InstruStyle.GPC, 14, "HeightPer", 70, decimalPlaces, StringAlignment.Far);
		return true;
	}

	private void FillGvPerformStaticTable()
	{
		if (!HasChrom)
		{
			gvPerformStatic.RowCount = 0;
			return;
		}
		m_bGvPraformanceLoading = true;
		gvPerformStatic.RowCount = 0;
		gvPerformStatic.SuspendLayout();
		gvPerformStatic.RowCount = CurChrom.PeaksNum;
		for (int i = 0; i < CurChrom.PeaksNum; i++)
		{
			Peak peak = CurChrom.RltPeaks[i];
			gvPerformStatic.Rows[i].Tag = peak;
			gvPerformStatic.Rows[i].Selected = peak.selected;
			for (int j = 0; j < gvPerformStatic.ColumnCount; j++)
			{
				if (gvPerformStatic.Columns[j].Visible)
				{
					gvPerformStatic.Rows[i].Cells[j].Value = gvPerformFrom50Value(peak, gvPerformStatic.Columns[j].Name);
				}
			}
		}
		gvPerformStatic.ResumeLayout();
		m_bGvPraformanceLoading = false;
	}

	private void FillGvRltsGnlTable()
	{
		if (!HasChrom)
		{
			lbRltExpress.Text = "[]";
			lclGvRltsGnl.RowCount = 0;
			return;
		}
		lbRltExpress.ForeColor = GetCurSingleColor();
		string disMouseLgFmtX = "";
		string disMouseLgFmtY = "";
		GetDisMouseLgFmtPosition(ref disMouseLgFmtX, ref disMouseLgFmtY);
		string text = CurChrom.chromInfo.cclCalcu.ToString();
		bool flag;
		if (flag = CurChrom.integ.GetNDRow(ref integRow_1) && integRow_1.success)
		{
			string text2 = text;
			text = text2 + Lang.PS("\n噪音 (", "\nnoise (") + integRow_1.timeA.ToString(disMouseLgFmtX) + " - " + integRow_1.timeB.ToString(disMouseLgFmtX) + "min): " + method_36(integRow_1);
		}
		if (CurChrom.integ.GetNDRow(ref integRow_0) && integRow_0.success)
		{
			string text3 = text;
			text = text3 + (flag ? "  " : "\n") + Lang.PS("飘移 (", "drift (") + integRow_0.timeA.ToString(disMouseLgFmtX) + " - " + integRow_0.timeB.ToString(disMouseLgFmtX) + "min): " + integRow_0.value.ToString(disMouseLgFmtY) + " [" + integRow_0.ValueUnitStr + "]";
		}
		lbRltExpress.Text = text;
		bool_3 = true;
		lclGvRltsGnl.SuspendLayout();
		SetGvRltsGnlRowValue(lclGvRltsGnl, -1, "Amount", Lang.PS("浓度", "Amount") + "\n[" + CurChrom.AmountUnit + "]");
		if (lclGvRltsGnl.Columns["Cus1"] != null)
		{
			lclGvRltsGnl.Columns["Cus1"].HeaderText = CurChrom.cus1_name;
		}
		if (lclGvRltsGnl.Columns["Cus2"] != null)
		{
			lclGvRltsGnl.Columns["Cus2"].HeaderText = CurChrom.cus2_name;
		}
		if (CurChrom.RltPeaks == null)
		{
			CurChrom.RltPeaks = CurChrom.GetRltPeaks(combine: false);
		}
		lclGvRltsGnl.RowCount = CurChrom.RltPeaks.Length + 1;
		frmParam.bMinus = false;
		if (frmParam.bMinus)
		{
			lclGvRltsGnl.RowCount = CurChrom.RltPeaks.Length + 2;
		}
		else
		{
			lclGvRltsGnl.RowCount = CurChrom.RltPeaks.Length + 1;
		}
		lclGvRltsGnl.hideRowIndex = CurChrom.RltPeaks.Length;
		for (int i = 0; i < CurChrom.RltPeaks.Length; i++)
		{
			Peak peak = CurChrom.RltPeaks[i];
			lclGvRltsGnl.Rows[i].Tag = peak;
			lclGvRltsGnl.Rows[i].DefaultCellStyle.BackColor = peak._backColor;
			lclGvRltsGnl.Rows[i].Selected = peak.selected;
			for (int j = 0; j < lclGvRltsGnl.ColumnCount; j++)
			{
				if (lclGvRltsGnl.Columns[j].Visible)
				{
					string name = lclGvRltsGnl.Columns[j].Name;
					lclGvRltsGnl.Rows[i].Cells[j].Value = gvRltsValue(peak, name, "", combine: false);
				}
			}
		}
		int num = CurChrom.RltPeaks.Length;
		if (frmParam.bMinus)
		{
		}
		lclGvRltsGnl.Rows[num].Tag = null;
		lclGvRltsGnl.Rows[num].DefaultCellStyle.BackColor = Color.White;
		for (int k = 0; k < lclGvRltsGnl.ColumnCount; k++)
		{
			lclGvRltsGnl.Rows[num].Cells[k].Value = "";
		}
		if (lclGvRltsGnl.showColumns == null)
		{
			return;
		}
		for (int l = 0; l < lclGvRltsGnl.showColumns.Length; l++)
		{
			if (lclGvRltsGnl.showColumns[l].DisplayIndex == 0)
			{
				SetGvRltsGnlRowValue(lclGvRltsGnl, num, lclGvRltsGnl.showColumns[l].Name, Lang.PS("总计", "Total"));
				SetGvRltsGnlRowValue(lclGvRltsGnl, num, "Area", CurChrom.whlArea);
				SetGvRltsGnlRowValue(lclGvRltsGnl, num, "AreaPer", 100f * CurChrom.whlAreaPer);
				SetGvRltsGnlRowValue(lclGvRltsGnl, num, "Height", CurChrom.whlHeight);
				SetGvRltsGnlRowValue(lclGvRltsGnl, num, "HeightPer", 100f * CurChrom.whlHeightPer);
				SetGvRltsGnlRowValue(lclGvRltsGnl, num, "Amount", CurChrom.whlAmount);
				SetGvRltsGnlRowValue(lclGvRltsGnl, num, "AmountPer", 100f * CurChrom.whlAmountPer);
				lclGvRltsGnl.ResumeLayout();
				bool_3 = false;
				break;
			}
		}
	}

	private void SetGvSummaryRowValue()
	{
		string[] array = method_5(chromatogram_0);
		bool flag;
		if (!(flag = array.Length != 0))
		{
			Array.Resize(ref array, 2);
			array[0] = Lang.PS("组分1", "component 1");
			array[1] = Lang.PS("组分2", "component 2");
		}
		gvSummary.W_showComColumns();
		if (smyTabOpt_0().smyHdrPara == SmyHdrPara.Cmpd_Para)
		{
			gvSummary.combineH = 16;
			for (int i = 0; i < array.Length; i++)
			{
				gvSummary.W_cmpd(InstruStyle.GC, array[i]);
			}
		}
		else if (smyTabOpt_0().smyHdrPara == SmyHdrPara.Para_Cmpd)
		{
			gvSummary.combineH = 32;
			gvSummary.W_cmpds(InstruStyle.GC, array);
		}
		if (chromatogram_0.Length == 0)
		{
			gvSummary.RowCount = 0;
			return;
		}
		if (chromatogram_0.Length > 1)
		{
			gvSummary.RowCount = chromatogram_0.Length + 2;
			for (int j = 0; j < chromatogram_0.Length; j++)
			{
				for (int k = 0; k < gvSummary.showComColumns.Length; k++)
				{
					gvSummary.Rows[j].Cells[k].Value = gvSummaryComValue(chromatogram_0[j], gvSummary.showComColumns[k].Name);
				}
				if (flag)
				{
					for (int l = gvSummary.showComColumns.Length; l < gvSummary.ColumnCount; l++)
					{
						string[] array2 = gvSummary.Columns[l].Name.Split(default(char));
						gvSummary.Rows[j].Cells[l].Value = gvSummarySmyValue(chromatogram_0[j], array2[1], array2[0]);
					}
				}
			}
			gvSummary.Rows[gvSummary.RowCount - 2].Cells[0].Value = Lang.PS("平均值", "average");
			gvSummary.Rows[gvSummary.RowCount - 2].DefaultCellStyle.BackColor = Color.GreenYellow;
			for (int m = gvSummary.showComColumns.Length; m < gvSummary.ColumnCount; m++)
			{
				string[] array3 = gvSummary.Columns[m].Name.Split(default(char));
				if (array3[0] == "Amount")
				{
					gvSummary.Rows[gvSummary.RowCount - 2].Cells[m].Value = method_46(m).ToString("0.000");
				}
				if (array3[0] == "AmountPer")
				{
					gvSummary.Rows[gvSummary.RowCount - 2].Cells[m].Value = method_46(m).ToString("0.000");
				}
				if (array3[0] == "RetenTime")
				{
					gvSummary.Rows[gvSummary.RowCount - 2].Cells[m].Value = method_46(m).ToString("0.000");
				}
				if (array3[0] == "Area")
				{
					gvSummary.Rows[gvSummary.RowCount - 2].Cells[m].Value = method_46(m).ToString("0.000");
				}
				if (array3[0] == "Height")
				{
					gvSummary.Rows[gvSummary.RowCount - 2].Cells[m].Value = method_46(m).ToString("0.000");
				}
			}
			gvSummary.Rows[gvSummary.RowCount - 1].Cells[0].Value = "R S D";
			gvSummary.Rows[gvSummary.RowCount - 1].DefaultCellStyle.BackColor = Color.GreenYellow;
			for (int n = gvSummary.showComColumns.Length; n < gvSummary.ColumnCount; n++)
			{
				string[] array4 = gvSummary.Columns[n].Name.Split(default(char));
				if (array4[0] == "Amount")
				{
					float float_ = Class49.String2Float(gvSummary.Rows[gvSummary.RowCount - 2].Cells[n].Value, 1f);
					gvSummary.Rows[gvSummary.RowCount - 1].Cells[n].Value = method_48(float_, method_47(n)).ToString("0.000");
				}
				if (array4[0] == "AmountPer")
				{
					float float_2 = Class49.String2Float(gvSummary.Rows[gvSummary.RowCount - 2].Cells[n].Value, 1f);
					gvSummary.Rows[gvSummary.RowCount - 1].Cells[n].Value = method_48(float_2, method_47(n)).ToString("0.000");
				}
				if (array4[0] == "RetenTime")
				{
					float float_3 = Class49.String2Float(gvSummary.Rows[gvSummary.RowCount - 2].Cells[n].Value, 1f);
					gvSummary.Rows[gvSummary.RowCount - 1].Cells[n].Value = method_48(float_3, method_47(n)).ToString("0.000");
				}
				if (array4[0] == "Area")
				{
					float float_4 = Class49.String2Float(gvSummary.Rows[gvSummary.RowCount - 2].Cells[n].Value, 1f);
					gvSummary.Rows[gvSummary.RowCount - 1].Cells[n].Value = method_48(float_4, method_47(n)).ToString("0.000");
				}
				if (array4[0] == "Height")
				{
					float float_5 = Class49.String2Float(gvSummary.Rows[gvSummary.RowCount - 2].Cells[n].Value, 1f);
					gvSummary.Rows[gvSummary.RowCount - 1].Cells[n].Value = method_48(float_5, method_47(n)).ToString("0.000");
				}
			}
			return;
		}
		gvSummary.RowCount = chromatogram_0.Length;
		for (int num = 0; num < chromatogram_0.Length; num++)
		{
			for (int num2 = 0; num2 < gvSummary.showComColumns.Length; num2++)
			{
				gvSummary.Rows[num].Cells[num2].Value = gvSummaryComValue(chromatogram_0[num], gvSummary.showComColumns[num2].Name);
			}
			if (flag)
			{
				for (int num3 = gvSummary.showComColumns.Length; num3 < gvSummary.ColumnCount; num3++)
				{
					string[] array5 = gvSummary.Columns[num3].Name.Split(default(char));
					gvSummary.Rows[num].Cells[num3].Value = gvSummarySmyValue(chromatogram_0[num], array5[1], array5[0]);
				}
			}
		}
	}

	public bool SetGvRltsGnlRowValue(LclGridView lclGridView_2, int int_16, string string_289, object object_1)
	{
		if (lclGridView_2.Columns.Contains(string_289) && lclGridView_2.Columns[string_289].Visible)
		{
			if (int_16 >= 0)
			{
				lclGridView_2.Rows[int_16].Cells[string_289].Value = object_1;
			}
			else
			{
				lclGridView_2.Columns[string_289].HeaderText = object_1.ToString();
			}
			return true;
		}
		return false;
	}

	private void SetGvRltsGnlShowColumn()
	{
		gvRltsGnl.ini_SetFirstVisibleColumn("CmpdName");
		gvRltsGnl.ini_SetNextVisibleColumn("PeakStyle");
		gvRltsGnl.ini_SetNextVisibleColumn("RetenTime");
		gvRltsGnl.ini_SetNextVisibleColumn("Area");
		gvRltsGnl.ini_SetNextVisibleColumn("AreaPer");
		gvRltsGnl.ini_SetNextVisibleColumn("Height");
		gvRltsGnl.ini_SetNextVisibleColumn("HeightPer");
		gvRltsGnl.ini_SetNextVisibleColumn("StartTime");
		gvRltsGnl.ini_SetNextVisibleColumn("EndTime");
		gvRltsGnl.ini_SetNextVisibleColumn("Amount");
		gvRltsGnl.ini_SetNextVisibleColumn("AmountPer");
		gvRltsGnl.ini_SetNextVisibleColumn("ResolutionEP");
		gvRltsGnl.ini_SetNextVisibleColumn("GasAmount");
		gvRltsGnl.ini_FinishVisibleColumn();
		if (lclGvRltsGnl != null)
		{
			FillGvRltsGnlTable();
		}
	}

	private void AddGvSummaryColumn(InstruStyle instruStyle_0)
	{
		switch (instruStyle_0)
		{
		case InstruStyle.GC:
		case InstruStyle.LC:
			gvSummary.ArraySmySHColumns(instruStyle_0, show: true, 3);
			gvSummary.AddSmyShowLink(instruStyle_0, 0, "RetenTime");
			gvSummary.AddSmyShowLink(instruStyle_0, 1, "WO5");
			gvSummary.AddSmyShowLink(instruStyle_0, 2, "Amount");
			break;
		case InstruStyle.GPC:
			gvSummary.ArraySmySHColumns(instruStyle_0, show: true, 4);
			gvSummary.AddSmyShowLink(instruStyle_0, 0, "Mn");
			gvSummary.AddSmyShowLink(instruStyle_0, 1, "Mw");
			gvSummary.AddSmyShowLink(instruStyle_0, 2, "Mz");
			gvSummary.AddSmyShowLink(instruStyle_0, 3, "Mv");
			break;
		case InstruStyle.PDA:
			gvSummary.ArraySmySHColumns(instruStyle_0, show: true, 3);
			gvSummary.AddSmyShowLink(instruStyle_0, 0, "PeakPurity");
			gvSummary.AddSmyShowLink(instruStyle_0, 1, "NameMatch");
			gvSummary.AddSmyShowLink(instruStyle_0, 2, "BestMatch");
			break;
		}
		gvSummary.FinishSmyHideLinks(instruStyle_0);
	}

	public void GetItgDisColumns(ref GvInfos gvInfos)
	{
		for (int i = 0; i < gvInfos.colHdrTxts.Length; i++)
		{
			int num = 80;
			string text = gvInfos.colNames[i];
			if (text != null)
			{
				switch (text)
				{
				case "Unit":
					num = 45;
					break;
				case "Group":
					num = 45;
					gvInfos.colAligns[i] = StringAlignment.Center;
					break;
				case "ChromOprt":
					num = 115;
					break;
				}
			}
			gvInfos.colWidths[i] = num;
		}
	}

	public void GetPfmDisColumns(ref GvInfos gvInfos)
	{
		mipfmRestoreDftColumns_Click(mipfmRestoreDftColumns, null);
		Class49.SetGridViewInfo(gvPerformStatic, ref gvInfos, null);
		for (int i = 0; i < gvInfos.colHdrTxts.Length; i++)
		{
			int num = 80;
			string text = gvInfos.colNames[i];
			if (text != null && text == "CmpdName")
			{
				num = 115;
			}
			gvInfos.colWidths[i] = num;
		}
	}

	public void GetRltDisColumns(ref GvInfos gvInfos)
	{
		if (AddGvRltsGnlColumn())
		{
			SetGvRltsGnlHeaderText(gvRltsGnl);
			SetGvRltsGnlHeaderText(gvRltsGpc);
			SetGvRltsGnlHeaderText(gvRltsDad);
			bool flag = gvRltsGnl.LoadFromManager();
			bool flag2 = gvRltsGpc.LoadFromManager();
			bool flag3 = gvRltsDad.LoadFromManager();
			if (!flag && !flag2 && !flag3)
			{
				mirltsResetCmpdNames_Click(mirltsRestoreDftColumns, null);
			}
		}
		Class49.SetGridViewInfo(lclGvRltsGnl, ref gvInfos, null);
		for (int i = 0; i < gvInfos.colHdrTxts.Length; i++)
		{
			int num = 80;
			string text = gvInfos.colNames[i];
			if (text != null && (text == "CmpdName" || text == "BestMatchName"))
			{
				num = 115;
			}
			gvInfos.colWidths[i] = num;
		}
	}

	public void GetSmyColumns(Chromatogram[] chroms, ref GvInfos gvInfos, ref SmyHdrPara smyHdrPara)
	{
		SetGvSummaryRowValue();
		Class49.SetGridViewInfo(gvSummary, ref gvInfos, null);
		for (int i = 0; i < gvInfos.colHdrTxts.Length; i++)
		{
			int num = 80;
			string text = gvInfos.colNames[i];
			if (text != null && (text == "谱图名称" || text == "样品"))
			{
				num = 115;
			}
			gvInfos.colWidths[i] = num;
		}
		smyHdrPara = smyTabOpt_0().smyHdrPara;
	}

	public string getSmyHeaderText(string name)
	{
		if (name == Lang.PS("谱图名称", "Name of spectra"))
		{
			return Lang.PS("谱图名称", "Name of spectra");
		}
		if (name == Lang.PS("样品ID", "Sample ID"))
		{
			return Lang.PS("样品ID", "Sample ID");
		}
		if (name == Lang.PS("样品", "Sample"))
		{
			return Lang.PS("样品", "Sample");
		}
		if (name == Lang.PS("样品浓度", "SampleAmount"))
		{
			return Lang.PS("样品\n浓度", "Sample\nAmount");
		}
		if (name == Lang.PS("样品稀释", "SampleDilution"))
		{
			return Lang.PS("样品稀释", "SampleDilution");
		}
		if (name == Lang.PS("内标浓度", "ISTDAmount"))
		{
			return Lang.PS("内标\n浓度", "ISTD\nAmount");
		}
		if (name == Lang.PS("体积", " volume"))
		{
			return Lang.PS("体积", " volume") + "\n[" + VolumnUnits.const_0.ToString() + "]";
		}
		if (name == Lang.PS("非保留时间", "Non-retention time"))
		{
			return Lang.PS("非保留\n时间\n[min]", "Non\nretention\ntime[min]");
		}
		if (name == Lang.PS("柱长", "ColumnLength"))
		{
			return Lang.PS("柱长", "ColumnLength\n[mm]");
		}
		if (name == Lang.PS("噪声", "Noise"))
		{
			return Lang.PS("噪声", "Noise");
		}
		if (name == Lang.PS("漂移", "Drift"))
		{
			return Lang.PS("漂移", "Drift");
		}
		if (name == Lang.PS("峰分离度", "ResolutionEP"))
		{
			return Lang.PS("峰分离度", "ResolutionEP");
		}
		return name switch
		{
			"StartTime" => Lang.PS("开始时间\n[min]"), 
			"EndTime" => Lang.PS("结束时间\n[min]"), 
			"StartValue" => Lang.PS("开始值") + "\n[" + Class49.MesureUnit() + "]", 
			"EndValue" => Lang.PS("结束值") + "\n[" + Class49.MesureUnit() + "]", 
			"WO5" => Lang.PS("半峰宽\n[min]"), 
			"RespBase" => Lang.PS("响应基础", "Response basis"), 
			"ResolutionEP" => Lang.PS("峰分离度", "ResolutionEP"), 
			"RetenIndex" => Lang.PS("保留索引\n[-]"), 
			"Area" => Lang.PS("面积") + "\n[" + Class49.MesureUnit() + ".s]", 
			"Height" => Lang.PS("高度") + "\n[" + Class49.MesureUnit() + "]", 
			"AreaPer" => Lang.PS("面积\n[%]"), 
			"HeightPer" => Lang.PS("高度\n[%]"), 
			"RetenTime" => Lang.PS("保留时间\n[min]"), 
			"Amount" => Lang.PS("浓度"), 
			"AmountPer" => Lang.PS("浓度\n[%]"), 
			"PeakType" => Lang.PS("峰类型"), 
			"CmpdName" => Lang.PS("组份名"), 
			"PeakPurity" => Lang.PS("峰纯度"), 
			"NameMatch" => Lang.PS("名匹配"), 
			"BestMatchName" => Lang.PS("最佳匹配名"), 
			"BestMatch" => Lang.PS("最佳匹配"), 
			"MaxRT" => Lang.PS("最大RT\n[min]"), 
			"StartRT" => Lang.PS("开始RT\n[min]"), 
			"EndRT" => Lang.PS("结束RT\n[min]"), 
			"FlowRateCorr" => Lang.PS("流速校正"), 
			"Mp" => "Mp", 
			"Mn" => "Mn", 
			"Mw" => "Mw", 
			"Mz" => "Mz", 
			"Mz1" => "Mz1", 
			"Mv" => "Mv", 
			"PD" => "PD", 
			_ => name, 
		};
	}

	public void GetSstDisColumns(ref GvInfos gvInfos)
	{
		for (int i = 0; i < gvInfos.colHdrTxts.Length; i++)
		{
			int num = 80;
			string text = gvInfos.colNames[i];
			if (text != null)
			{
				if (!(text == "X"))
				{
					if (text == "Chrom")
					{
						num = 130;
					}
				}
				else
				{
					num = 20;
				}
			}
			gvInfos.colWidths[i] = num;
		}
	}

	private void gvRltsGnl_Enter(object sender, EventArgs e)
	{
		Class49.smethod_40(((Control)sender).Handle);
	}

	private void gvRltsGnl_CellEnter(object sender, DataGridViewCellEventArgs e)
	{
		Class49.smethod_40(((Control)sender).Handle);
	}

	private void gvRltsGnl_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
	{
		if (lclGvRltsGnl.RowCount <= 0)
		{
			return;
		}
		int columnIndex = e.ColumnIndex;
		string text = lclGvRltsGnl.Columns[columnIndex].HeaderText.Replace("\n", "") + "\r\n";
		for (int i = 0; i < lclGvRltsGnl.RowCount; i++)
		{
			text += lclGvRltsGnl.Rows[i].Cells[columnIndex].Value;
			if (i != lclGvRltsGnl.RowCount - 1)
			{
				text += "\r\n";
			}
		}
		Clipboard.Clear();
		Clipboard.SetData(DataFormats.Text, text);
		SetslbExplainText(string.Format("{0}列内容已复制到剪切板.", lclGvRltsGnl.Columns[columnIndex].HeaderText.Replace("\n", "")));
	}

	private void gvPerformStatic_SelectionChanged(object sender, EventArgs e)
	{
		if (HasChrom && !m_bGvPraformanceLoading)
		{
			LclGridView lclGridView = sender as LclGridView;
			for (int i = 0; i < lclGridView.RowCount; i++)
			{
				(lclGridView.Rows[i].Tag as Peak).selected = lclGridView.Rows[i].Selected;
			}
			DisDpRefresh();
		}
	}

	private void gvRltsGnl_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
	{
	}

	private void gvRltsGnl_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
	{
		if (!HasChrom)
		{
			return;
		}
		if (e.RowIndex >= 0 && e.ColumnIndex == -1)
		{
			Peak peak = lclGvRltsGnl.Rows[e.RowIndex].Tag as Peak;
			string text = "峰 " + (e.RowIndex + 1);
			text = text + "\nLfDotNo: " + peak.LfDotNo + "       RtDotNo: " + peak.RtDotNo;
			text = text + "\nbsLfV.DotNo: " + peak.bsLfV.dotNo + "  bsRtV.DotNo: " + peak.bsRtV.dotNo;
			text = text + "\nbsLfV.N: " + peak.bsLfV.N + "      bsRtV.N: " + peak.bsRtV.N;
			MessageBox.Show(text + "\n\nFrom: " + peak.FromNo + "\tTo: " + peak.ToNo + "\n面积 " + peak.area.ToString(""));
		}
		else if (e.RowIndex == -1 && e.ColumnIndex >= 0)
		{
			if (cusDlg_0 == null)
			{
				cusDlg_0 = new CusDlg();
			}
			if ((lclGvRltsGnl.Columns[e.ColumnIndex].Name == "Cus1" && cusDlg_0.ShowDialog(ref CurChrom.cus1_name, ref CurChrom.cus1_formula) == DialogResult.OK) || (lclGvRltsGnl.Columns[e.ColumnIndex].Name == "Cus2" && cusDlg_0.ShowDialog(ref CurChrom.cus2_name, ref CurChrom.cus2_formula) == DialogResult.OK))
			{
				CurChrom.CalcuCus();
				FillGvRltsGnlTable();
			}
		}
		else
		{
			lclGvRltsGnl.Rows[e.RowIndex].ReadOnly = false;
		}
	}

	private void gvRltsGnl_CellEndEdit(object sender, DataGridViewCellEventArgs e)
	{
		int rowIndex = e.RowIndex;
		int columnIndex = e.ColumnIndex;
		if (lclGvRltsGnl.Columns[columnIndex].Name == "CmpdName")
		{
			string text = lclGvRltsGnl.Rows[rowIndex].Cells[columnIndex].Value.ToString().TrimEnd();
			if (text == "")
			{
				return;
			}
			bool flag = false;
			for (int i = 0; i < CurChrom.PeaksNum; i++)
			{
				if (CurChrom.RltPeaks[i] != (Peak)lclGvRltsGnl.Rows[rowIndex].Tag && CurChrom.RltPeaks[i].name == text)
				{
					flag = true;
					MessageBox.Show(Lang.PS("组份名重复！", "name repeated"));
					break;
				}
			}
			if (!flag && rowIndex < CurChrom.RltPeaks.Length)
			{
				CurChrom.RltPeaks[rowIndex].name = text;
			}
		}
		else
		{
			if (lclGvRltsGnl.Rows[rowIndex].Tag == null || CurChrom.RltPeaks == null)
			{
				return;
			}
			string name = lclGvRltsGnl.Columns[columnIndex].Name;
			string text2 = lclGvRltsGnl.Rows[rowIndex].Cells[columnIndex].Value.ToString();
			Peak peak = lclGvRltsGnl.Rows[rowIndex].Tag as Peak;
			switch (name)
			{
			case "Amount":
			{
				float.TryParse(text2, out CurChrom.RltPeaks[rowIndex].amount);
				float num = Class49.String2Float(text2, CurChrom.chromInfo.GetIstdAmount(peak.pkRT));
				if (peak._backColor != Color.White && num > 0f)
				{
					CurChrom.chromInfo.SetIstdAmount(peak.pkRT, num);
					CurChrom.CalcuResults(InstruStyle.GC);
				}
				break;
			}
			case "AmountPer":
			{
				float result = 0f;
				float.TryParse(text2, out result);
				CurChrom.RltPeaks[rowIndex].amountPer = result * 0.01f;
				break;
			}
			case "GasAmount":
				float.TryParse(text2, out CurChrom.RltPeaks[rowIndex].GasAmount);
				break;
			}
		}
	}

	private void gvRltsGnl_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
	{
		int rowIndex = e.RowIndex;
		int columnIndex = e.ColumnIndex;
		if (HasChrom && rowIndex >= 0 && columnIndex >= 0 && lclGvRltsGnl.Rows[rowIndex].Tag != null)
		{
			Peak peak = lclGvRltsGnl.Rows[rowIndex].Tag as Peak;
			if (lclGvRltsGnl.CurrentCell != null && (lclGvRltsGnl.Columns[columnIndex].Name == "Amount" || lclGvRltsGnl.Columns[columnIndex].Name == "AmountPer" || lclGvRltsGnl.Columns[columnIndex].Name == "RetenTime" || lclGvRltsGnl.Columns[columnIndex].Name == "GasAmount" || lclGvRltsGnl.Columns[columnIndex].Name == "CmpdName"))
			{
				lclGvRltsGnl.Rows[e.RowIndex].Selected = true;
				lclGvRltsGnl.BeginEdit(selectAll: true);
			}
		}
	}

	private void gvRltsGnl_SelectionChanged(object sender, EventArgs e)
	{
		if (!HasChrom || bool_3)
		{
			return;
		}
		for (int i = 0; i < lclGvRltsGnl.RowCount; i++)
		{
			if (lclGvRltsGnl.Rows[i].Tag != null)
			{
				(lclGvRltsGnl.Rows[i].Tag as Peak).selected = lclGvRltsGnl.Rows[i].Selected;
			}
		}
		DisDpRefresh();
		method_15();
	}

	public void tcChrom_SelectedIndexChanged(object sender, EventArgs e)
	{
		if (tcChrom.SelectedTab == tpResults)
		{
			lclGvRltsGnl.EndEdit();
			FillGvRltsGnlTable();
			method_16();
		}
		else if (tcChrom.SelectedTab == tpSummary)
		{
			SetGvSummaryRowValue();
		}
		else if (tcChrom.SelectedTab == tpPerformance)
		{
			FillGvPerformStaticTable();
		}
		DisDpRefresh();
	}

	public void calorificValue(Peak[] arrayCom)
	{
		float num = 0f;
		float num2 = 0f;
		float num3 = 0f;
		float num4 = 0f;
		float num5 = 0f;
		float num6 = 0f;
		float num7 = 0f;
		float num8 = 0f;
		float num9 = 0f;
		float num10 = 0f;
		float num11 = 0f;
		float num12 = 0f;
		float num13 = 216.89f;
		float num14 = 4.713f;
		float num15 = 0f;
		float num16 = 0f;
		float num17 = 0f;
		float num18 = 0f;
		if (arrayCom != null)
		{
			int num19 = 0;
			while (1 <= arrayCom.Length && num19 < arrayCom.Length)
			{
				if (arrayCom[num19].compound.eFunc.curveFit == CurveFit.Free)
				{
					num += Program.getCharacteristic(arrayCom[num19].name, arrayCom[num19].amountPer, 1, 15f);
					num4 += Program.getCharacteristic(arrayCom[num19].name, arrayCom[num19].amountPer, 2, 15f);
					num12 += Program.getCharacteristic(arrayCom[num19].name, arrayCom[num19].amountPer, 3, 15f);
					num11 += Program.getCharacteristic(arrayCom[num19].name, arrayCom[num19].amountPer, 4, 15f);
					num2 += Program.getCharacteristic(arrayCom[num19].name, arrayCom[num19].amountPer, 5, 15f);
					num5 += Program.getCharacteristic(arrayCom[num19].name, arrayCom[num19].amountPer, 6, 15f);
					num3 += Program.getCharacteristic(arrayCom[num19].name, arrayCom[num19].amountPer, 7, 15f);
				}
				else
				{
					num += Program.getCharacteristic(arrayCom[num19].name, arrayCom[num19].amountPer, 1, 15f);
					num4 += Program.getCharacteristic(arrayCom[num19].name, arrayCom[num19].amountPer, 2, 15f);
					num12 += Program.getCharacteristic(arrayCom[num19].name, arrayCom[num19].amountPer, 3, 15f);
					num11 += Program.getCharacteristic(arrayCom[num19].name, arrayCom[num19].amountPer, 4, 15f);
					num2 += Program.getCharacteristic(arrayCom[num19].name, arrayCom[num19].amountPer, 5, 15f);
					num5 += Program.getCharacteristic(arrayCom[num19].name, arrayCom[num19].amountPer, 6, 15f);
					num3 += Program.getCharacteristic(arrayCom[num19].name, arrayCom[num19].amountPer, 7, 15f);
				}
				num19++;
			}
		}
		double d = num12;
		float num20 = (float)Math.Sqrt(d);
		num6 = num / num20;
		num7 = num2 / num20;
		num8 = num4 / num20;
		num9 = num5 / num20;
		labHRZ.Text = num.ToString("0.000") + "(MJ / Nm3) = " + num2.ToString("0.000") + "(KCal / Nm3)";
		labLRZ.Text = num4.ToString("0.000") + "(MJ / Nm3) = " + num5.ToString("0.000") + "(KCal / Nm3)";
		labHHB.Text = num6.ToString("0.000") + "(MJ / Nm3) = " + num7.ToString("0.000") + "(KCal / Nm3)";
		labLHB.Text = num8.ToString("0.000") + "(MJ / Nm3) = " + num9.ToString("0.000") + "(KCal / Nm3)";
		labMD.Text = num11.ToString("0.000") + "(kg / m3)";
		labXDMD.Text = num12.ToString("0.000");
		labLJWD.Text = num13.ToString("0.000") + "(K)";
		labLJYL.Text = num14.ToString("0.000") + "(MPa)";
	}

	private string[] method_5(Chromatogram[] chromatogram_2)
	{
		string[] string_ = new string[0];
		if (smyTabOpt_0().smyTabRpt == SmyTabRpt.AllIdentifiedPeaks)
		{
			foreach (Chromatogram chromatogram in chromatogram_2)
			{
				for (int j = 0; j < chromatogram.PeaksNum; j++)
				{
					if (chromatogram.RltPeaks[j].IsIdentified)
					{
						Class49.Append2Array(ref string_, chromatogram.RltPeaks[j].name, bool_5: true);
					}
				}
			}
		}
		if (smyTabOpt_0().smyTabRpt == SmyTabRpt.AllPeaksInCali)
		{
			foreach (Chromatogram chromatogram2 in chromatogram_2)
			{
				if (chromatogram2.caliGnl != null)
				{
					for (int l = 0; l < chromatogram2.caliGnl.cmpds.Length; l++)
					{
						Class49.Append2Array(ref string_, chromatogram2.caliGnl.cmpds[l].cmpdInfo.name, bool_5: true);
					}
				}
			}
		}
		return string_;
	}

	private void method_15()
	{
		if (lclGvRltsGnl.RowCount <= 0 || lclGvRltsGnl.SelectedRows.Count <= 0)
		{
			return;
		}
		string text = "";
		float num = 0f;
		float num2 = 0f;
		float num3 = 0f;
		for (int i = 0; i < lclGvRltsGnl.SelectedRows.Count; i++)
		{
			if (lclGvRltsGnl.SelectedRows[lclGvRltsGnl.SelectedRows.Count - 1].Index != lclGvRltsGnl.RowCount - 1)
			{
				num += Class49.String2Float(lclGvRltsGnl.SelectedRows[i].Cells["Area"].Value, 0f);
				num2 += Class49.String2Float(lclGvRltsGnl.SelectedRows[i].Cells["Height"].Value, 0f);
				num3 += Class49.String2Float(lclGvRltsGnl.SelectedRows[i].Cells["Amount"].Value, 0f);
			}
		}
		float num4 = Class49.String2Float(lclGvRltsGnl.Rows[lclGvRltsGnl.RowCount - 1].Cells["Area"].Value, 1f);
		float num5 = Class49.String2Float(lclGvRltsGnl.Rows[lclGvRltsGnl.RowCount - 1].Cells["Height"].Value, 1f);
		float num6 = Class49.String2Float(lclGvRltsGnl.Rows[lclGvRltsGnl.RowCount - 1].Cells["Amount"].Value, 1f);
		float num7 = num / num4 * 100f;
		float num8 = num2 / num5 * 100f;
		float num9 = ((!(num6 > 0f)) ? 0f : (num3 / num6 * 100f));
		switch (Class49.sysLanguage_0)
		{
		case SysLanguage.CN:
			text = string.Format("选择组份总面积:{0},占总面积{1}%;选择组份总峰高:{2},占总峰高{3}%;选择组份总浓度:{4},占总浓度{5}%.", num.ToString("0.00"), num7.ToString("0.00"), num2.ToString("0.00"), num8.ToString("0.00"), num3.ToString("0.00"), num9.ToString("0.00"));
			break;
		case SysLanguage.EN:
			text = string.Format("Select Peak totalArea:{0},around {1}% of the Area;Select Peak totalHeight:{2},around {3}% of the Height;Select Peak totalAmount:{4},around {5}% of the Amount.", num.ToString("0.00"), num7.ToString("0.00"), num2.ToString("0.00"), num8.ToString("0.00"), num3.ToString("0.00"), num9.ToString("0.00"));
			break;
		}
	}

	private void method_16()
	{
		for (int i = 0; i < lclGvRltsGnl.RowCount; i++)
		{
			lclGvRltsGnl.Rows[i].DefaultCellStyle.ForeColor = Color.Black;
		}
		if (lclGvRltsGnl.SelectedRows.Count > 0)
		{
			lclGvRltsGnl.SelectedRows[0].DefaultCellStyle.ForeColor = Color.Red;
			lclGvRltsGnl.FirstDisplayedScrollingRowIndex = lclGvRltsGnl.SelectedRows[0].Index;
		}
	}

	private void method_35(object sender, EventArgs e)
	{
		if (smyTabOptDlg_0.ShowDialog(smyTabOpt_0()) == DialogResult.OK)
		{
			SetGvSummaryRowValue();
		}
	}

	public string method_36(IntegRow integRow)
	{
		string disMouseLgFmtY = chromDisplay_0().disMouseLgFmtY;
		float value = integRow.value;
		if ((double)value < 0.1)
		{
			return value * 1000f + " [μV]";
		}
		return integRow.value.ToString(disMouseLgFmtY) + " [" + integRow.ValueUnitStr + "]";
	}

	private float method_46(int int_16)
	{
		float num = 0f;
		int num2 = 0;
		for (int i = 0; i < gvSummary.Rows.Count - 1 - 1; i++)
		{
			float num3 = Class49.String2Float(gvSummary.Rows[i].Cells[int_16].Value, 0f);
			num += num3;
			if (num3 != 0f)
			{
				num2++;
			}
		}
		if (num2 == 0)
		{
			return 0f;
		}
		return num / (float)num2;
	}

	private float[] method_47(int int_16)
	{
		int num = 0;
		for (int i = 0; i < gvSummary.Rows.Count - 1 - 1; i++)
		{
			if (Class49.String2Float(gvSummary.Rows[i].Cells[int_16].Value, 0f) != 0f)
			{
				num++;
			}
		}
		float[] array = new float[num];
		int num2 = 0;
		for (int j = 0; j < gvSummary.Rows.Count - 1 - 1; j++)
		{
			float num3 = Class49.String2Float(gvSummary.Rows[j].Cells[int_16].Value, 0f);
			if (num3 != 0f)
			{
				array[num2] = num3;
				num2++;
			}
		}
		return array;
	}

	private double method_48(float float_8, float[] float_9)
	{
		return Program.RSDCalculate(float_8, float_9, float_9.Length);
	}

	public string gvSummaryComValue(Chromatogram chrom, string columnName)
	{
		float num = -1f;
		switch (columnName)
		{
		case "谱图名称":
			return chrom.fName;
		case "样品ID":
			return chrom.injAnalysis.sampleID;
		case "样品":
			return chrom.injAnalysis.sample;
		case "样品浓度":
			num = chrom.injAnalysis.amount;
			break;
		case "样品稀释":
			num = chrom.injAnalysis.dilution;
			break;
		case "内标浓度":
			num = chrom.injAnalysis.ISTD_amount;
			break;
		case "体积":
			num = chrom.injAnalysis.inj_volume;
			break;
		case "非保留时间":
			num = chrom.chromInfo.ccColumnUT;
			break;
		case "柱长":
			num = chrom.chromInfo.ccColumnLength;
			break;
		case "噪声":
			if (chrom.integ.GetNDRow(ref integRow_1) && integRow_1.success)
			{
				num = integRow_1.value;
			}
			break;
		case "漂移":
			if (chrom.integ.GetNDRow(ref integRow_0) && integRow_0.success)
			{
				num = integRow_0.value;
			}
			break;
		}
		if (num > 0f)
		{
			string text = gvSummary.ConvertValFmt(columnName);
			return num.ToString(text);
		}
		return "";
	}

	public string gvSummarySmyValue(Chromatogram chrom, string cmpdName, string columnName)
	{
		Peak peak = null;
		for (int i = 0; i < chrom.PeaksNum; i++)
		{
			if (chrom.RltPeaks[i].name == cmpdName)
			{
				peak = chrom.RltPeaks[i];
				if (peak == null)
				{
					return "";
				}
				string string_ = gvSummary.ConvertValFmt(InstruStyle.GC, columnName);
				return gvRltsValue(peak, columnName, string_, combine: false);
			}
		}
		return "";
	}

	public string gvPerformFrom50Value(Peak peak, string columnName)
	{
		float num = -1f;
		switch (columnName)
		{
		case "RetenTime":
			num = peak.pkRT;
			break;
		case "WO5":
			num = peak.WO5;
			break;
		case "Asymmetry":
			num = peak.Asymmetry;
			break;
		case "SymTail":
			num = peak.SymmetryTailing;
			break;
		case "Capacity":
			num = peak.Capacity;
			break;
		case "Efficiency":
			num = peak.Efficiency_EP;
			break;
		case "Eff_ColL":
			num = peak.Eff_Column_EP;
			break;
		case "Resolution":
			num = peak.Resolution_EP;
			break;
		case "CmpdName":
			return peak.name;
		}
		if (!(columnName == "Capacity") && num <= 0f)
		{
			return "";
		}
		string text = gvPerformStatic.ConvertValFmt(columnName);
		return num.ToString("F4");
	}

	public string gvRltsValue(Peak peak, string columnName, string string_289, bool combine)
	{
		float num = -1f;
		switch (columnName)
		{
		case "RetenTime":
			num = peak.pkRT;
			break;
		case "StartTime":
			num = peak.startT;
			break;
		case "EndTime":
			num = peak.endT;
			break;
		case "StartValue":
			num = peak.startV;
			break;
		case "EndValue":
			num = peak.endV;
			break;
		case "PeakStyle":
			return GetPeakStyleName(peak.pkStyle);
		case "Area":
			num = peak.area;
			break;
		case "AreaPer":
			num = 100f * (combine ? peak._areaPer : peak.areaPer);
			break;
		case "Height":
			num = peak.height;
			break;
		case "HeightPer":
			num = 100f * (combine ? peak._heightPer : peak.heightPer);
			break;
		case "WO5":
			num = peak.WO5;
			break;
		case "RespBase":
			if (peak.respStyle == 0)
			{
				return RespStyle.Area.ToString();
			}
			if (peak.respStyle == 1)
			{
				return RespStyle.Height.ToString();
			}
			if (peak.respStyle == 2)
			{
				return RespStyle.AreaSquare.ToString();
			}
			if (peak.respStyle == 3)
			{
				return RespStyle.PeakHeightSquare.ToString();
			}
			break;
		case "Amount":
			num = peak.amount;
			break;
		case "AmountPer":
			num = 100f * (combine ? peak._amountPer : peak.amountPer);
			break;
		case "PeakType":
			if (peak.compound != null && peak.compound.cmpdInfo.isIstd)
			{
				return "标样";
			}
			break;
		case "CmpdName":
			return peak.name;
		case "ResolutionEP":
			num = peak.Resolution_EP;
			break;
		case "GasAmount":
			num = peak.GasAmount;
			break;
		case "Cus1":
			if (!float.IsNaN(peak.cus1))
			{
				if (string_289 == "")
				{
					string_289 = lclGvRltsGnl.ConvertValFmt(columnName);
				}
				return peak.cus1.ToString(string_289);
			}
			break;
		case "Cus2":
			if (!float.IsNaN(peak.cus2))
			{
				if (string_289 == "")
				{
					string_289 = lclGvRltsGnl.ConvertValFmt(columnName);
				}
				return peak.cus2.ToString(string_289);
			}
			break;
		}
		if (num > 0f)
		{
			if (string_289 == "")
			{
				string_289 = lclGvRltsGnl.ConvertValFmt(columnName);
			}
			return num.ToString(string_289);
		}
		if (num == 0f)
		{
			return "0";
		}
		if (num < 0f)
		{
			return "-0";
		}
		return "";
	}

	private void tbpfmColumnUT_KeyDown(object sender, KeyEventArgs e)
	{
		if (HasChrom && e.KeyCode == Keys.Return)
		{
			CurChrom.CalcuPerformanceAndCus();
		}
	}

	private void mirltsResetCmpdNames_Click(object sender, EventArgs e)
	{
		if (sender == mirltsColumnsSetup)
		{
			if (columnsSetupDlg_1.ShowDialog(lclGvRltsGnl) == DialogResult.OK)
			{
				saveColumnList_gvRltsGnl();
				FillGvRltsGnlTable();
			}
		}
		else if (sender == mirltsRestoreDftColumns)
		{
			SetGvRltsGnlShowColumn();
			saveColumnList_gvRltsGnl();
		}
		else if (sender == mirltsResetCmpdNames)
		{
			if (!HasChrom)
			{
				return;
			}
			for (int i = 0; i < CurChrom.PeaksNum; i++)
			{
				CurChrom.RltPeaks[i].name = "";
			}
			FillGvRltsGnlTable();
		}
		if (lclGvRltsGnl != null)
		{
			chromDisplay_0().fmtPeakRT = lclGvRltsGnl.ConvertValFmt("RetenTime");
		}
		DisDpRefresh();
	}

	private void mismySmyOpt_Click(object sender, EventArgs e)
	{
		if (sender != mismyColumnsSetup)
		{
			if (sender == mismyRestoreDftColumns)
			{
				gvSummary.ArrayComSHColumns(show: true, 3);
				gvSummary.AddComShowLink(0, Lang.PS("谱图名称", "ChromName"));
				gvSummary.AddComShowLink(1, Lang.PS("样品浓度", "SampleAmount"));
				gvSummary.AddComShowLink(2, Lang.PS("体积", "InjVol"));
				gvSummary.FinishComHideLinks();
				AddGvSummaryColumn(InstruStyle.GC);
				smyTabOpt_0().smyHdrPara = SmyHdrPara.Cmpd_Para;
				SetGvSummaryRowValue();
				if (!m_bLoading)
				{
					saveColumnList_gvSummary();
				}
			}
			else if (sender == mismySmyOpt)
			{
				method_35(null, null);
			}
		}
		else
		{
			DialogResult dialogResult = columnsSetupDlg_3.ShowDialog(gvSummary, InstruStyle.GC, Lang.PS("一般列", "Common"), Lang.PS("总结列", "Summary"), gvSummary.commonColumns, gvSummary.showComColumns, gvSummary.hideComColumns, gvSummary.smyGnlColumns, gvSummary.showGnlColumns, gvSummary.hideGnlColumns);
			if (dialogResult == DialogResult.OK)
			{
				saveColumnList_gvSummary();
				SetGvSummaryRowValue();
			}
		}
	}

	private void mipfmRestoreDftColumns_Click(object sender, EventArgs e)
	{
		if (sender == mipfmColumnsSetup)
		{
			columnsSetupDlg_0.ShowDialog(lclGvPerformStatic);
		}
	}

	private void mirlAddAllComponent_Click(object sender, EventArgs e)
	{
		if (this.OnAddAllCompnent != null)
		{
			this.OnAddAllCompnent(null, null);
		}
	}

	private void dgNMHC_CellContentClick(object sender, DataGridViewCellEventArgs e)
	{
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
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle12 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle13 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle14 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle15 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle16 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle17 = new System.Windows.Forms.DataGridViewCellStyle();
		this.cmsRltGV = new System.Windows.Forms.ContextMenuStrip(this.components);
		this.mirltsColumnsSetup = new System.Windows.Forms.ToolStripMenuItem();
		this.mirltsRestoreDftColumns = new System.Windows.Forms.ToolStripMenuItem();
		this.toolStripSeparator33 = new System.Windows.Forms.ToolStripSeparator();
		this.mirltsResetCmpdNames = new System.Windows.Forms.ToolStripMenuItem();
		this.mirlAddAllComponent = new System.Windows.Forms.ToolStripMenuItem();
		this.cmsSummary = new System.Windows.Forms.ContextMenuStrip(this.components);
		this.mismyColumnsSetup = new System.Windows.Forms.ToolStripMenuItem();
		this.mismyRestoreDftColumns = new System.Windows.Forms.ToolStripMenuItem();
		this.toolStripSeparator15 = new System.Windows.Forms.ToolStripSeparator();
		this.mismySmyOpt = new System.Windows.Forms.ToolStripMenuItem();
		this.cmsPerformance = new System.Windows.Forms.ContextMenuStrip(this.components);
		this.mipfmColumnsSetup = new System.Windows.Forms.ToolStripMenuItem();
		this.mipfmRestoreDftColumns = new System.Windows.Forms.ToolStripMenuItem();
		this.tcChrom = new IBrainChrom2018.LclTabControl();
		this.tpResults = new System.Windows.Forms.TabPage();
		this.gvRltsGnl = new IBrainChrom2018.LclGridView();
		this.dgNMHC = new System.Windows.Forms.DataGridView();
		this.浓度 = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.gvRltsDad = new IBrainChrom2018.LclGridView();
		this.gvRltsGpc = new IBrainChrom2018.LclGridView();
		this.lbRltExpress = new IBrainChrom2018.LclExpressLabel();
		this.tpSummary = new System.Windows.Forms.TabPage();
		this.gvSummary = new IBrainChrom2018.LclSummaryGridView();
		this.tpPerformance = new System.Windows.Forms.TabPage();
		this.gvPerformStatic = new IBrainChrom2018.LclGridView();
		this.tpCalorific = new System.Windows.Forms.TabPage();
		this.groupBox9 = new System.Windows.Forms.GroupBox();
		this.labLJYL = new System.Windows.Forms.Label();
		this.labLJWD = new System.Windows.Forms.Label();
		this.labXDMD = new System.Windows.Forms.Label();
		this.labMD = new System.Windows.Forms.Label();
		this.labLHB = new System.Windows.Forms.Label();
		this.labHHB = new System.Windows.Forms.Label();
		this.labLRZ = new System.Windows.Forms.Label();
		this.labHRZ = new System.Windows.Forms.Label();
		this.label93 = new System.Windows.Forms.Label();
		this.label92 = new System.Windows.Forms.Label();
		this.label91 = new System.Windows.Forms.Label();
		this.label90 = new System.Windows.Forms.Label();
		this.label89 = new System.Windows.Forms.Label();
		this.label88 = new System.Windows.Forms.Label();
		this.label87 = new System.Windows.Forms.Label();
		this.label86 = new System.Windows.Forms.Label();
		this.label85 = new System.Windows.Forms.Label();
		this.lbExpress = new IBrainChrom2018.LclLabel();
		this.cmsRltGV.SuspendLayout();
		this.cmsSummary.SuspendLayout();
		this.cmsPerformance.SuspendLayout();
		this.tcChrom.SuspendLayout();
		this.tpResults.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.gvRltsGnl).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.dgNMHC).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.gvRltsDad).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.gvRltsGpc).BeginInit();
		this.tpSummary.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.gvSummary).BeginInit();
		this.tpPerformance.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.gvPerformStatic).BeginInit();
		this.tpCalorific.SuspendLayout();
		this.groupBox9.SuspendLayout();
		base.SuspendLayout();
		this.cmsRltGV.ImageScalingSize = new System.Drawing.Size(20, 20);
		this.cmsRltGV.Items.AddRange(new System.Windows.Forms.ToolStripItem[5] { this.mirltsColumnsSetup, this.mirltsRestoreDftColumns, this.toolStripSeparator33, this.mirltsResetCmpdNames, this.mirlAddAllComponent });
		this.cmsRltGV.Name = "cmsRltGV";
		this.cmsRltGV.ShowImageMargin = false;
		this.cmsRltGV.Size = new System.Drawing.Size(172, 98);
		this.mirltsColumnsSetup.Name = "mirltsColumnsSetup";
		this.mirltsColumnsSetup.Size = new System.Drawing.Size(171, 22);
		this.mirltsColumnsSetup.Text = "列设置";
		this.mirltsColumnsSetup.Click += new System.EventHandler(mirltsResetCmpdNames_Click);
		this.mirltsRestoreDftColumns.Name = "mirltsRestoreDftColumns";
		this.mirltsRestoreDftColumns.Size = new System.Drawing.Size(171, 22);
		this.mirltsRestoreDftColumns.Text = "恢复列设置";
		this.mirltsRestoreDftColumns.Click += new System.EventHandler(mirltsResetCmpdNames_Click);
		this.toolStripSeparator33.Name = "toolStripSeparator33";
		this.toolStripSeparator33.Size = new System.Drawing.Size(168, 6);
		this.mirltsResetCmpdNames.Name = "mirltsResetCmpdNames";
		this.mirltsResetCmpdNames.Size = new System.Drawing.Size(171, 22);
		this.mirltsResetCmpdNames.Text = "清除组份名";
		this.mirltsResetCmpdNames.Click += new System.EventHandler(mirltsResetCmpdNames_Click);
		this.mirlAddAllComponent.Name = "mirlAddAllComponent";
		this.mirlAddAllComponent.Size = new System.Drawing.Size(171, 22);
		this.mirlAddAllComponent.Text = "全部添加入组份校正表";
		this.mirlAddAllComponent.Click += new System.EventHandler(mirlAddAllComponent_Click);
		this.cmsSummary.ImageScalingSize = new System.Drawing.Size(20, 20);
		this.cmsSummary.Items.AddRange(new System.Windows.Forms.ToolStripItem[4] { this.mismyColumnsSetup, this.mismyRestoreDftColumns, this.toolStripSeparator15, this.mismySmyOpt });
		this.cmsSummary.Name = "cmsSummary";
		this.cmsSummary.ShowImageMargin = false;
		this.cmsSummary.Size = new System.Drawing.Size(112, 76);
		this.mismyColumnsSetup.Name = "mismyColumnsSetup";
		this.mismyColumnsSetup.Size = new System.Drawing.Size(111, 22);
		this.mismyColumnsSetup.Text = "列设置";
		this.mismyColumnsSetup.Click += new System.EventHandler(mismySmyOpt_Click);
		this.mismyRestoreDftColumns.Name = "mismyRestoreDftColumns";
		this.mismyRestoreDftColumns.Size = new System.Drawing.Size(111, 22);
		this.mismyRestoreDftColumns.Text = "恢复列设置";
		this.mismyRestoreDftColumns.Click += new System.EventHandler(mismySmyOpt_Click);
		this.toolStripSeparator15.Name = "toolStripSeparator15";
		this.toolStripSeparator15.Size = new System.Drawing.Size(108, 6);
		this.mismySmyOpt.Name = "mismySmyOpt";
		this.mismySmyOpt.Size = new System.Drawing.Size(111, 22);
		this.mismySmyOpt.Text = "总结选项";
		this.mismySmyOpt.Click += new System.EventHandler(mismySmyOpt_Click);
		this.cmsPerformance.ImageScalingSize = new System.Drawing.Size(20, 20);
		this.cmsPerformance.Items.AddRange(new System.Windows.Forms.ToolStripItem[2] { this.mipfmColumnsSetup, this.mipfmRestoreDftColumns });
		this.cmsPerformance.Name = "cmsPerformance";
		this.cmsPerformance.ShowImageMargin = false;
		this.cmsPerformance.Size = new System.Drawing.Size(112, 48);
		this.mipfmColumnsSetup.Name = "mipfmColumnsSetup";
		this.mipfmColumnsSetup.Size = new System.Drawing.Size(111, 22);
		this.mipfmColumnsSetup.Text = "列设置";
		this.mipfmColumnsSetup.Click += new System.EventHandler(mipfmRestoreDftColumns_Click);
		this.mipfmRestoreDftColumns.Name = "mipfmRestoreDftColumns";
		this.mipfmRestoreDftColumns.Size = new System.Drawing.Size(111, 22);
		this.mipfmRestoreDftColumns.Text = "恢复列设置";
		this.mipfmRestoreDftColumns.Click += new System.EventHandler(mipfmRestoreDftColumns_Click);
		this.tcChrom.Controls.Add(this.tpResults);
		this.tcChrom.Controls.Add(this.tpSummary);
		this.tcChrom.Controls.Add(this.tpPerformance);
		this.tcChrom.Controls.Add(this.tpCalorific);
		this.tcChrom.Dock = System.Windows.Forms.DockStyle.Fill;
		this.tcChrom.ItemSize = new System.Drawing.Size(90, 19);
		this.tcChrom.Location = new System.Drawing.Point(0, 12);
		this.tcChrom.Name = "tcChrom";
		this.tcChrom.Padding = new System.Drawing.Point(0, 0);
		this.tcChrom.SelectedIndex = 0;
		this.tcChrom.Size = new System.Drawing.Size(853, 334);
		this.tcChrom.TabIndex = 15;
		this.tcChrom.SelectedIndexChanged += new System.EventHandler(tcChrom_SelectedIndexChanged);
		this.tpResults.BackColor = System.Drawing.Color.Transparent;
		this.tpResults.Controls.Add(this.gvRltsGnl);
		this.tpResults.Controls.Add(this.dgNMHC);
		this.tpResults.Controls.Add(this.gvRltsDad);
		this.tpResults.Controls.Add(this.gvRltsGpc);
		this.tpResults.Controls.Add(this.lbRltExpress);
		this.tpResults.Location = new System.Drawing.Point(4, 23);
		this.tpResults.Name = "tpResults";
		this.tpResults.Size = new System.Drawing.Size(845, 307);
		this.tpResults.TabIndex = 0;
		this.tpResults.Text = "结果";
		this.tpResults.UseVisualStyleBackColor = true;
		this.gvRltsGnl.AllowUserToAddRows = false;
		this.gvRltsGnl.AllowUserToDeleteRows = false;
		this.gvRltsGnl.AllowUserToResizeRows = false;
		this.gvRltsGnl.BackgroundColor = System.Drawing.Color.AliceBlue;
		this.gvRltsGnl.CharacterHeaderColor = System.Drawing.Color.Black;
		dataGridViewCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
		dataGridViewCellStyle.BackColor = System.Drawing.SystemColors.Control;
		dataGridViewCellStyle.Font = new System.Drawing.Font("宋体", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		dataGridViewCellStyle.ForeColor = System.Drawing.SystemColors.WindowText;
		dataGridViewCellStyle.SelectionBackColor = System.Drawing.SystemColors.Highlight;
		dataGridViewCellStyle.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
		dataGridViewCellStyle.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
		this.gvRltsGnl.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle;
		this.gvRltsGnl.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
		this.gvRltsGnl.ContextMenuStrip = this.cmsRltGV;
		dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
		dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
		dataGridViewCellStyle2.Font = new System.Drawing.Font("宋体", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		dataGridViewCellStyle2.ForeColor = System.Drawing.Color.Black;
		dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
		dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
		dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
		this.gvRltsGnl.DefaultCellStyle = dataGridViewCellStyle2;
		this.gvRltsGnl.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
		this.gvRltsGnl.Location = new System.Drawing.Point(21, 17);
		this.gvRltsGnl.Name = "gvRltsGnl";
		dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
		dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
		dataGridViewCellStyle3.Font = new System.Drawing.Font("宋体", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
		dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
		dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
		dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
		this.gvRltsGnl.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
		this.gvRltsGnl.RowHeadersWidth = 25;
		this.gvRltsGnl.RowTemplate.DefaultCellStyle.SelectionForeColor = System.Drawing.Color.Red;
		this.gvRltsGnl.RowTemplate.Height = 16;
		this.gvRltsGnl.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
		this.gvRltsGnl.ShowCellToolTips = false;
		this.gvRltsGnl.Size = new System.Drawing.Size(380, 148);
		this.gvRltsGnl.TabIndex = 4;
		this.gvRltsGnl.CellBeginEdit += new System.Windows.Forms.DataGridViewCellCancelEventHandler(gvRltsGnl_CellBeginEdit);
		this.gvRltsGnl.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(gvRltsGnl_CellDoubleClick);
		this.gvRltsGnl.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(gvRltsGnl_CellEndEdit);
		this.gvRltsGnl.CellEnter += new System.Windows.Forms.DataGridViewCellEventHandler(gvRltsGnl_CellEnter);
		this.gvRltsGnl.CellMouseDown += new System.Windows.Forms.DataGridViewCellMouseEventHandler(gvRltsGnl_CellMouseDown);
		this.gvRltsGnl.ColumnHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(gvRltsGnl_ColumnHeaderMouseClick);
		this.gvRltsGnl.SelectionChanged += new System.EventHandler(gvRltsGnl_SelectionChanged);
		this.gvRltsGnl.Enter += new System.EventHandler(gvRltsGnl_Enter);
		this.dgNMHC.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
		dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
		dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control;
		dataGridViewCellStyle4.Font = new System.Drawing.Font("宋体", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
		dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
		dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
		dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
		this.dgNMHC.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle4;
		this.dgNMHC.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
		this.dgNMHC.Columns.AddRange(this.浓度, this.Column1, this.Column4, this.Column2, this.Column3);
		dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
		dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Window;
		dataGridViewCellStyle5.Font = new System.Drawing.Font("宋体", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.ControlText;
		dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
		dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
		dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
		this.dgNMHC.DefaultCellStyle = dataGridViewCellStyle5;
		this.dgNMHC.Location = new System.Drawing.Point(431, 88);
		this.dgNMHC.Name = "dgNMHC";
		this.dgNMHC.RowTemplate.Height = 23;
		this.dgNMHC.Size = new System.Drawing.Size(597, 67);
		this.dgNMHC.TabIndex = 11;
		this.dgNMHC.Visible = false;
		this.dgNMHC.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(dgNMHC_CellContentClick);
		this.浓度.HeaderText = "组分名";
		this.浓度.Name = "浓度";
		this.浓度.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.Column1.HeaderText = "保留时间";
		this.Column1.Name = "Column1";
		this.Column1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.Column4.HeaderText = "峰高";
		this.Column4.Name = "Column4";
		this.Column4.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.Column2.HeaderText = "峰面积";
		this.Column2.Name = "Column2";
		this.Column2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.Column3.HeaderText = "浓度";
		this.Column3.Name = "Column3";
		this.Column3.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.gvRltsDad.AllowUserToAddRows = false;
		this.gvRltsDad.AllowUserToDeleteRows = false;
		this.gvRltsDad.AllowUserToResizeRows = false;
		this.gvRltsDad.BackgroundColor = System.Drawing.Color.AliceBlue;
		this.gvRltsDad.CharacterHeaderColor = System.Drawing.Color.Black;
		dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
		dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Control;
		dataGridViewCellStyle6.Font = new System.Drawing.Font("宋体", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.WindowText;
		dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
		dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
		dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
		this.gvRltsDad.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle6;
		this.gvRltsDad.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
		this.gvRltsDad.ContextMenuStrip = this.cmsRltGV;
		dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
		dataGridViewCellStyle7.BackColor = System.Drawing.SystemColors.Window;
		dataGridViewCellStyle7.Font = new System.Drawing.Font("宋体", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		dataGridViewCellStyle7.ForeColor = System.Drawing.Color.Black;
		dataGridViewCellStyle7.SelectionBackColor = System.Drawing.SystemColors.Highlight;
		dataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
		dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
		this.gvRltsDad.DefaultCellStyle = dataGridViewCellStyle7;
		this.gvRltsDad.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
		this.gvRltsDad.Location = new System.Drawing.Point(431, 206);
		this.gvRltsDad.Name = "gvRltsDad";
		dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
		dataGridViewCellStyle8.BackColor = System.Drawing.SystemColors.Control;
		dataGridViewCellStyle8.Font = new System.Drawing.Font("宋体", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		dataGridViewCellStyle8.ForeColor = System.Drawing.SystemColors.WindowText;
		dataGridViewCellStyle8.SelectionBackColor = System.Drawing.SystemColors.Highlight;
		dataGridViewCellStyle8.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
		dataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
		this.gvRltsDad.RowHeadersDefaultCellStyle = dataGridViewCellStyle8;
		this.gvRltsDad.RowHeadersWidth = 25;
		this.gvRltsDad.RowTemplate.Height = 16;
		this.gvRltsDad.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
		this.gvRltsDad.ShowCellToolTips = false;
		this.gvRltsDad.Size = new System.Drawing.Size(378, 67);
		this.gvRltsDad.TabIndex = 4;
		this.gvRltsDad.CellBeginEdit += new System.Windows.Forms.DataGridViewCellCancelEventHandler(gvRltsGnl_CellBeginEdit);
		this.gvRltsDad.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(gvRltsGnl_CellEndEdit);
		this.gvRltsDad.CellEnter += new System.Windows.Forms.DataGridViewCellEventHandler(gvRltsGnl_CellEnter);
		this.gvRltsDad.SelectionChanged += new System.EventHandler(gvRltsGnl_SelectionChanged);
		this.gvRltsDad.Enter += new System.EventHandler(gvRltsGnl_Enter);
		this.gvRltsGpc.AllowUserToAddRows = false;
		this.gvRltsGpc.AllowUserToDeleteRows = false;
		this.gvRltsGpc.AllowUserToResizeRows = false;
		this.gvRltsGpc.BackgroundColor = System.Drawing.Color.AliceBlue;
		this.gvRltsGpc.CharacterHeaderColor = System.Drawing.Color.Black;
		dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
		dataGridViewCellStyle9.BackColor = System.Drawing.SystemColors.Control;
		dataGridViewCellStyle9.Font = new System.Drawing.Font("宋体", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		dataGridViewCellStyle9.ForeColor = System.Drawing.SystemColors.WindowText;
		dataGridViewCellStyle9.SelectionBackColor = System.Drawing.SystemColors.Highlight;
		dataGridViewCellStyle9.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
		dataGridViewCellStyle9.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
		this.gvRltsGpc.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle9;
		this.gvRltsGpc.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
		this.gvRltsGpc.ContextMenuStrip = this.cmsRltGV;
		dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
		dataGridViewCellStyle10.BackColor = System.Drawing.SystemColors.Window;
		dataGridViewCellStyle10.Font = new System.Drawing.Font("宋体", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		dataGridViewCellStyle10.ForeColor = System.Drawing.Color.Black;
		dataGridViewCellStyle10.SelectionBackColor = System.Drawing.SystemColors.Highlight;
		dataGridViewCellStyle10.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
		dataGridViewCellStyle10.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
		this.gvRltsGpc.DefaultCellStyle = dataGridViewCellStyle10;
		this.gvRltsGpc.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
		this.gvRltsGpc.Location = new System.Drawing.Point(21, 206);
		this.gvRltsGpc.Name = "gvRltsGpc";
		dataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
		dataGridViewCellStyle11.BackColor = System.Drawing.SystemColors.Control;
		dataGridViewCellStyle11.Font = new System.Drawing.Font("宋体", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		dataGridViewCellStyle11.ForeColor = System.Drawing.SystemColors.WindowText;
		dataGridViewCellStyle11.SelectionBackColor = System.Drawing.SystemColors.Highlight;
		dataGridViewCellStyle11.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
		dataGridViewCellStyle11.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
		this.gvRltsGpc.RowHeadersDefaultCellStyle = dataGridViewCellStyle11;
		this.gvRltsGpc.RowHeadersWidth = 25;
		this.gvRltsGpc.RowTemplate.Height = 16;
		this.gvRltsGpc.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
		this.gvRltsGpc.ShowCellToolTips = false;
		this.gvRltsGpc.Size = new System.Drawing.Size(404, 65);
		this.gvRltsGpc.TabIndex = 4;
		this.gvRltsGpc.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(gvRltsGnl_CellDoubleClick);
		this.gvRltsGpc.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(gvRltsGnl_CellEndEdit);
		this.gvRltsGpc.CellEnter += new System.Windows.Forms.DataGridViewCellEventHandler(gvRltsGnl_CellEnter);
		this.gvRltsGpc.CellMouseDown += new System.Windows.Forms.DataGridViewCellMouseEventHandler(gvRltsGnl_CellMouseDown);
		this.gvRltsGpc.SelectionChanged += new System.EventHandler(gvRltsGnl_SelectionChanged);
		this.gvRltsGpc.Enter += new System.EventHandler(gvRltsGnl_Enter);
		this.lbRltExpress.BackColor = System.Drawing.Color.Transparent;
		this.lbRltExpress.Dock = System.Windows.Forms.DockStyle.Top;
		this.lbRltExpress.Location = new System.Drawing.Point(0, 0);
		this.lbRltExpress.Name = "lbRltExpress";
		this.lbRltExpress.Size = new System.Drawing.Size(845, 0);
		this.lbRltExpress.TabIndex = 5;
		this.lbRltExpress.Text = "[]";
		this.lbRltExpress.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
		this.tpSummary.Controls.Add(this.gvSummary);
		this.tpSummary.Location = new System.Drawing.Point(4, 23);
		this.tpSummary.Name = "tpSummary";
		this.tpSummary.Size = new System.Drawing.Size(845, 307);
		this.tpSummary.TabIndex = 1;
		this.tpSummary.Text = "总结";
		this.tpSummary.UseVisualStyleBackColor = true;
		this.gvSummary.AllowUserToAddRows = false;
		this.gvSummary.AllowUserToDeleteRows = false;
		this.gvSummary.AllowUserToResizeRows = false;
		this.gvSummary.BackgroundColor = System.Drawing.Color.AliceBlue;
		this.gvSummary.BorderStyle = System.Windows.Forms.BorderStyle.None;
		this.gvSummary.CharacterHeaderColor = System.Drawing.Color.Black;
		dataGridViewCellStyle12.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
		dataGridViewCellStyle12.BackColor = System.Drawing.SystemColors.Control;
		dataGridViewCellStyle12.Font = new System.Drawing.Font("宋体", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		dataGridViewCellStyle12.ForeColor = System.Drawing.SystemColors.WindowText;
		dataGridViewCellStyle12.SelectionBackColor = System.Drawing.SystemColors.Highlight;
		dataGridViewCellStyle12.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
		dataGridViewCellStyle12.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
		this.gvSummary.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle12;
		this.gvSummary.ColumnHeadersHeight = 48;
		this.gvSummary.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
		this.gvSummary.ContextMenuStrip = this.cmsSummary;
		dataGridViewCellStyle13.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
		dataGridViewCellStyle13.BackColor = System.Drawing.SystemColors.Window;
		dataGridViewCellStyle13.Font = new System.Drawing.Font("宋体", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		dataGridViewCellStyle13.ForeColor = System.Drawing.SystemColors.ControlText;
		dataGridViewCellStyle13.SelectionBackColor = System.Drawing.SystemColors.Highlight;
		dataGridViewCellStyle13.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
		dataGridViewCellStyle13.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
		this.gvSummary.DefaultCellStyle = dataGridViewCellStyle13;
		this.gvSummary.Dock = System.Windows.Forms.DockStyle.Fill;
		this.gvSummary.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
		this.gvSummary.Location = new System.Drawing.Point(0, 0);
		this.gvSummary.Name = "gvSummary";
		this.gvSummary.ReadOnly = true;
		dataGridViewCellStyle14.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
		dataGridViewCellStyle14.BackColor = System.Drawing.SystemColors.Control;
		dataGridViewCellStyle14.Font = new System.Drawing.Font("宋体", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		dataGridViewCellStyle14.ForeColor = System.Drawing.SystemColors.WindowText;
		dataGridViewCellStyle14.SelectionBackColor = System.Drawing.SystemColors.Highlight;
		dataGridViewCellStyle14.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
		dataGridViewCellStyle14.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
		this.gvSummary.RowHeadersDefaultCellStyle = dataGridViewCellStyle14;
		this.gvSummary.RowHeadersWidth = 25;
		this.gvSummary.RowTemplate.Height = 16;
		this.gvSummary.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
		this.gvSummary.ShowCellToolTips = false;
		this.gvSummary.Size = new System.Drawing.Size(845, 307);
		this.gvSummary.TabIndex = 0;
		this.tpPerformance.Controls.Add(this.gvPerformStatic);
		this.tpPerformance.Location = new System.Drawing.Point(4, 23);
		this.tpPerformance.Name = "tpPerformance";
		this.tpPerformance.Size = new System.Drawing.Size(845, 307);
		this.tpPerformance.TabIndex = 2;
		this.tpPerformance.Text = "柱效";
		this.tpPerformance.UseVisualStyleBackColor = true;
		this.gvPerformStatic.AllowUserToAddRows = false;
		this.gvPerformStatic.AllowUserToDeleteRows = false;
		this.gvPerformStatic.AllowUserToResizeRows = false;
		this.gvPerformStatic.BackgroundColor = System.Drawing.Color.AliceBlue;
		this.gvPerformStatic.BorderStyle = System.Windows.Forms.BorderStyle.None;
		this.gvPerformStatic.CharacterHeaderColor = System.Drawing.Color.Black;
		dataGridViewCellStyle15.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
		dataGridViewCellStyle15.BackColor = System.Drawing.SystemColors.Control;
		dataGridViewCellStyle15.Font = new System.Drawing.Font("宋体", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		dataGridViewCellStyle15.ForeColor = System.Drawing.SystemColors.WindowText;
		dataGridViewCellStyle15.SelectionBackColor = System.Drawing.SystemColors.Highlight;
		dataGridViewCellStyle15.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
		dataGridViewCellStyle15.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
		this.gvPerformStatic.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle15;
		this.gvPerformStatic.ColumnHeadersHeight = 32;
		this.gvPerformStatic.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
		this.gvPerformStatic.ContextMenuStrip = this.cmsPerformance;
		dataGridViewCellStyle16.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
		dataGridViewCellStyle16.BackColor = System.Drawing.SystemColors.Window;
		dataGridViewCellStyle16.Font = new System.Drawing.Font("宋体", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		dataGridViewCellStyle16.ForeColor = System.Drawing.SystemColors.ControlText;
		dataGridViewCellStyle16.SelectionBackColor = System.Drawing.SystemColors.Highlight;
		dataGridViewCellStyle16.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
		dataGridViewCellStyle16.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
		this.gvPerformStatic.DefaultCellStyle = dataGridViewCellStyle16;
		this.gvPerformStatic.Dock = System.Windows.Forms.DockStyle.Fill;
		this.gvPerformStatic.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
		this.gvPerformStatic.Location = new System.Drawing.Point(0, 0);
		this.gvPerformStatic.Name = "gvPerformStatic";
		this.gvPerformStatic.ReadOnly = true;
		dataGridViewCellStyle17.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
		dataGridViewCellStyle17.BackColor = System.Drawing.SystemColors.Control;
		dataGridViewCellStyle17.Font = new System.Drawing.Font("宋体", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		dataGridViewCellStyle17.ForeColor = System.Drawing.SystemColors.WindowText;
		dataGridViewCellStyle17.SelectionBackColor = System.Drawing.SystemColors.Highlight;
		dataGridViewCellStyle17.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
		dataGridViewCellStyle17.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
		this.gvPerformStatic.RowHeadersDefaultCellStyle = dataGridViewCellStyle17;
		this.gvPerformStatic.RowHeadersWidth = 25;
		this.gvPerformStatic.RowTemplate.Height = 16;
		this.gvPerformStatic.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
		this.gvPerformStatic.ShowCellToolTips = false;
		this.gvPerformStatic.Size = new System.Drawing.Size(845, 307);
		this.gvPerformStatic.TabIndex = 2;
		this.gvPerformStatic.SelectionChanged += new System.EventHandler(gvPerformStatic_SelectionChanged);
		this.tpCalorific.Controls.Add(this.groupBox9);
		this.tpCalorific.Location = new System.Drawing.Point(4, 23);
		this.tpCalorific.Name = "tpCalorific";
		this.tpCalorific.Size = new System.Drawing.Size(845, 307);
		this.tpCalorific.TabIndex = 3;
		this.tpCalorific.Text = "热值";
		this.tpCalorific.UseVisualStyleBackColor = true;
		this.groupBox9.Controls.Add(this.labLJYL);
		this.groupBox9.Controls.Add(this.labLJWD);
		this.groupBox9.Controls.Add(this.labXDMD);
		this.groupBox9.Controls.Add(this.labMD);
		this.groupBox9.Controls.Add(this.labLHB);
		this.groupBox9.Controls.Add(this.labHHB);
		this.groupBox9.Controls.Add(this.labLRZ);
		this.groupBox9.Controls.Add(this.labHRZ);
		this.groupBox9.Controls.Add(this.label93);
		this.groupBox9.Controls.Add(this.label92);
		this.groupBox9.Controls.Add(this.label91);
		this.groupBox9.Controls.Add(this.label90);
		this.groupBox9.Controls.Add(this.label89);
		this.groupBox9.Controls.Add(this.label88);
		this.groupBox9.Controls.Add(this.label87);
		this.groupBox9.Controls.Add(this.label86);
		this.groupBox9.Controls.Add(this.label85);
		this.groupBox9.Dock = System.Windows.Forms.DockStyle.Fill;
		this.groupBox9.Location = new System.Drawing.Point(0, 0);
		this.groupBox9.Name = "groupBox9";
		this.groupBox9.Size = new System.Drawing.Size(845, 307);
		this.groupBox9.TabIndex = 45;
		this.groupBox9.TabStop = false;
		this.groupBox9.Text = "15℃ 273.15K、101325Pa";
		this.labLJYL.AutoSize = true;
		this.labLJYL.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
		this.labLJYL.Cursor = System.Windows.Forms.Cursors.Hand;
		this.labLJYL.Location = new System.Drawing.Point(89, 195);
		this.labLJYL.Name = "labLJYL";
		this.labLJYL.Size = new System.Drawing.Size(13, 14);
		this.labLJYL.TabIndex = 16;
		this.labLJYL.Text = "0";
		this.labLJWD.AutoSize = true;
		this.labLJWD.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
		this.labLJWD.Cursor = System.Windows.Forms.Cursors.Hand;
		this.labLJWD.Location = new System.Drawing.Point(89, 174);
		this.labLJWD.Name = "labLJWD";
		this.labLJWD.Size = new System.Drawing.Size(13, 14);
		this.labLJWD.TabIndex = 15;
		this.labLJWD.Text = "0";
		this.labXDMD.AutoSize = true;
		this.labXDMD.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
		this.labXDMD.Cursor = System.Windows.Forms.Cursors.Hand;
		this.labXDMD.Location = new System.Drawing.Point(89, 154);
		this.labXDMD.Name = "labXDMD";
		this.labXDMD.Size = new System.Drawing.Size(13, 14);
		this.labXDMD.TabIndex = 14;
		this.labXDMD.Text = "0";
		this.labMD.AutoSize = true;
		this.labMD.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
		this.labMD.Cursor = System.Windows.Forms.Cursors.Hand;
		this.labMD.Location = new System.Drawing.Point(89, 133);
		this.labMD.Name = "labMD";
		this.labMD.Size = new System.Drawing.Size(13, 14);
		this.labMD.TabIndex = 13;
		this.labMD.Text = "0";
		this.labLHB.AutoSize = true;
		this.labLHB.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
		this.labLHB.Cursor = System.Windows.Forms.Cursors.Hand;
		this.labLHB.Location = new System.Drawing.Point(89, 87);
		this.labLHB.Name = "labLHB";
		this.labLHB.Size = new System.Drawing.Size(13, 14);
		this.labLHB.TabIndex = 12;
		this.labLHB.Text = "0";
		this.labHHB.AutoSize = true;
		this.labHHB.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
		this.labHHB.Cursor = System.Windows.Forms.Cursors.Hand;
		this.labHHB.Location = new System.Drawing.Point(89, 66);
		this.labHHB.Name = "labHHB";
		this.labHHB.Size = new System.Drawing.Size(13, 14);
		this.labHHB.TabIndex = 11;
		this.labHHB.Text = "0";
		this.labLRZ.AutoSize = true;
		this.labLRZ.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
		this.labLRZ.Cursor = System.Windows.Forms.Cursors.Hand;
		this.labLRZ.Location = new System.Drawing.Point(89, 43);
		this.labLRZ.Name = "labLRZ";
		this.labLRZ.Size = new System.Drawing.Size(13, 14);
		this.labLRZ.TabIndex = 10;
		this.labLRZ.Text = "0";
		this.labHRZ.AutoSize = true;
		this.labHRZ.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
		this.labHRZ.Cursor = System.Windows.Forms.Cursors.Hand;
		this.labHRZ.Location = new System.Drawing.Point(89, 27);
		this.labHRZ.Name = "labHRZ";
		this.labHRZ.Size = new System.Drawing.Size(13, 14);
		this.labHRZ.TabIndex = 9;
		this.labHRZ.Text = "0";
		this.label93.AutoSize = true;
		this.label93.Location = new System.Drawing.Point(6, 197);
		this.label93.Name = "label93";
		this.label93.Size = new System.Drawing.Size(65, 12);
		this.label93.TabIndex = 8;
		this.label93.Text = "临界压力：";
		this.label92.AutoSize = true;
		this.label92.Location = new System.Drawing.Point(6, 176);
		this.label92.Name = "label92";
		this.label92.Size = new System.Drawing.Size(65, 12);
		this.label92.TabIndex = 7;
		this.label92.Text = "临界温度：";
		this.label91.AutoSize = true;
		this.label91.Location = new System.Drawing.Point(6, 156);
		this.label91.Name = "label91";
		this.label91.Size = new System.Drawing.Size(65, 12);
		this.label91.TabIndex = 6;
		this.label91.Text = "相对密度：";
		this.label90.AutoSize = true;
		this.label90.Location = new System.Drawing.Point(6, 135);
		this.label90.Name = "label90";
		this.label90.Size = new System.Drawing.Size(41, 12);
		this.label90.TabIndex = 5;
		this.label90.Text = "密度：";
		this.label89.AutoSize = true;
		this.label89.Location = new System.Drawing.Point(6, 111);
		this.label89.Name = "label89";
		this.label89.Size = new System.Drawing.Size(53, 12);
		this.label89.TabIndex = 4;
		this.label89.Text = "燃烧势：";
		this.label89.Visible = false;
		this.label88.AutoSize = true;
		this.label88.Location = new System.Drawing.Point(6, 89);
		this.label88.Name = "label88";
		this.label88.Size = new System.Drawing.Size(89, 12);
		this.label88.TabIndex = 3;
		this.label88.Text = "低热值华白数：";
		this.label87.AutoSize = true;
		this.label87.Location = new System.Drawing.Point(6, 68);
		this.label87.Name = "label87";
		this.label87.Size = new System.Drawing.Size(89, 12);
		this.label87.TabIndex = 2;
		this.label87.Text = "高热值华白数：";
		this.label86.AutoSize = true;
		this.label86.Location = new System.Drawing.Point(6, 48);
		this.label86.Name = "label86";
		this.label86.Size = new System.Drawing.Size(53, 12);
		this.label86.TabIndex = 1;
		this.label86.Text = "低热值：";
		this.label85.AutoSize = true;
		this.label85.Location = new System.Drawing.Point(6, 27);
		this.label85.Name = "label85";
		this.label85.Size = new System.Drawing.Size(53, 12);
		this.label85.TabIndex = 0;
		this.label85.Text = "高热值：";
		this.lbExpress.AutoEllipsis = true;
		this.lbExpress.BackColor = System.Drawing.Color.PowderBlue;
		this.lbExpress.Dock = System.Windows.Forms.DockStyle.Top;
		this.lbExpress.Location = new System.Drawing.Point(0, 0);
		this.lbExpress.Name = "lbExpress";
		this.lbExpress.Size = new System.Drawing.Size(853, 12);
		this.lbExpress.TabIndex = 14;
		this.lbExpress.Text = "中华";
		this.lbExpress.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.Controls.Add(this.tcChrom);
		base.Controls.Add(this.lbExpress);
		base.Name = "ChromFormDataGrid";
		base.Size = new System.Drawing.Size(853, 346);
		base.Load += new System.EventHandler(ChromFormDataGrid_Load);
		this.cmsRltGV.ResumeLayout(false);
		this.cmsSummary.ResumeLayout(false);
		this.cmsPerformance.ResumeLayout(false);
		this.tcChrom.ResumeLayout(false);
		this.tpResults.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.gvRltsGnl).EndInit();
		((System.ComponentModel.ISupportInitialize)this.dgNMHC).EndInit();
		((System.ComponentModel.ISupportInitialize)this.gvRltsDad).EndInit();
		((System.ComponentModel.ISupportInitialize)this.gvRltsGpc).EndInit();
		this.tpSummary.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.gvSummary).EndInit();
		this.tpPerformance.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.gvPerformStatic).EndInit();
		this.tpCalorific.ResumeLayout(false);
		this.groupBox9.ResumeLayout(false);
		this.groupBox9.PerformLayout();
		base.ResumeLayout(false);
	}
}
