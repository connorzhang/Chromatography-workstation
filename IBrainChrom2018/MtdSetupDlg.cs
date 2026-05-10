using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace IBrainChrom2018;

public class MtdSetupDlg : LclDialog
{
	private GcProgTemp gcProgTemp_0 = new GcProgTemp();

	private LcGradient lcGradient_0 = new LcGradient();

	private MtdSetup mtdSetup_0 = new MtdSetup();

	private MtdSetup mtdSetup_1;

	private LclButton btnasNoneChrom;

	private LclButton btnasSetChrom;

	private LclButton btncclNone;

	private LclButton btncclSet;

	private LclButton btncclView;

	private Button btnExtEvTPQry;

	private Button btnExtEvTPSet;

	private LclButton btnmtdApply;

	private Button btnPTQry;

	private Button btnPTSet;

	private LclCheckBox cbacqAutoStop;

	private LclComboBox cbacqRange;

	private LclComboBox cbacqRate;

	private LclCusComboBox cbasMatching;

	private LclComboBox cbASsSelect;

	private LclCusComboBox cbcclCalcu;

	private LclComboBox cbDtsSelect;

	private LclCheckBox cbecExternalControl;

	private LclCheckBox cblcUse;

	private LclCheckBox cblgoptSolvent1;

	private LclCheckBox cblgoptSolvent2;

	private LclCheckBox cblgoptSolvent3;

	private LclCheckBox cblgoptSolvent4;

	private LclCheckBox cblsoForAllDetectedPeaks;

	private LclCusComboBox cblsoMatchCriteria;

	private LclCheckBox cblsoRestrictRT;

	private LclCheckBox cblsoRestrictWaveLength;

	private LclCheckBox cblsoUseBackCorr;

	private LclCheckBox cbppoRestrictWaveLength;

	private LclCheckBox cbppoUseBackCorr;

	private LclCusComboBox cbprsUncalBase;

	private LclCheckBox cbprsUseScaleFactor;

	private LclCheckBox cbrtrHideISTDPeak;

	private CheckBox cbUseProgWave;

	private CheckBox cbWaveScan;

	private DataGridViewTextBoxColumn clmExtEvTP0;

	private DataGridViewTextBoxColumn clmExtEvTP1;

	private DataGridViewTextBoxColumn clmExtEvTP2;

	private DataGridViewTextBoxColumn clmExtEvTP3;

	private ContextMenuStrip cmsIntegration;

	private ContextMenuStrip cmsLcGradient;

	private ContextMenuStrip cmsLibs;

	private DataGridViewTextBoxColumn colTime;

	private DataGridViewTextBoxColumn colWave;

	private IContainer components;

	public LclGridView dgvPT;

	private LclDisplayPanel dpgcProgTemp;

	private LclDisplayPanel dplcGradient;

	private LclGroupBox gbadvAddSub;

	private LclGroupBox gbadvColumnCalcu;

	private LclGroupBox gbcclParas;

	private LclGroupBox gbcclRltTableReport;

	private LclGroupBox gblcIdleState;

	private LclGroupBox gblcStandBy;

	private LclGroupBox gbmsmAcquisition;

	private LclGroupBox gbmsmExternalControl;

	private LclGroupBox gbpdaLibSearchOptions;

	private LclGroupBox gbpdaPeakPurityOptions;

	private LclGroupBox gbppoUsedPoints;

	private GradientDisplay gradientDisplay_0;

	public DataGridView gvExtEvTP;

	private LclGridView gvgrMw;

	private LclGridView gvgrPercent;

	public LclIntegGridView gvInteg;

	private LclGridView gvlcGradient;

	private LclGridView gvpdaLibs;

	private LclGridView gvProgWave;

	private Label label1;

	private Label label14;

	private Label label15;

	private Label label16;

	private Label label17;

	private Label label2;

	private Label label27;

	private LclLabel lbacqDetector;

	private LclLabel lbacqRange;

	private LclLabel lbacqRate;

	private LclLabel lbacqRunTime;

	private LclLabel lbasChrom;

	private LclLabel lbasMatching;

	private LclLabel lbccColumnLength;

	public LclLabel lbccColumnLengthU;

	private LclLabel lbcclAuthor;

	private LclLabel lbcclAuthorV;

	private LclLabel lbcclCalcu;

	private LclLabel lbcclCalibration;

	private LclLabel lbcclCreateTime;

	private LclLabel lbcclCreateTimeV;

	private LclLabel lbcclDescription;

	private LclLabel lbcclDescriptionV;

	private LclLabel lbcclModifiedTime;

	private LclLabel lbcclModifiedTimeV;

	private LclLabel lbccUnretainedPeak;

	public LclLabel lbccUnretainedPeakU;

	private LclLabel lbExpress;

	private LclLabel lbgrMw;

	private LclLabel lbgrPercent;

	private LclLabel lblcPumpNum;

	private LclLabel lblsoFrom;

	private LclLabel lblsoMatchCriteria;

	private LclLabel lblsoMatchFactorThreshold;

	private LclLabel lblsoMaxNumHits;

	private LclLabel lblsoTo;

	private LclLabel lbmsmColumn;

	private LclLabel lbmsmDetection;

	private LclLabel lbmsmFlowRate;

	private LclLabel lbmsmMobilePhase;

	private LclLabel lbmsmMtdDspt;

	private LclLabel lbmsmNote;

	private LclLabel lbmsmPressure;

	private LclLabel lbmsmTemperature;

	private LclLabel lbppoAbsorbanceThreshold;

	private LclLabel lbppoFrom;

	private LclLabel lbppoPurityThreshold;

	private LclLabel lbppoTo;

	private LclLabel lbprsScaleFactor;

	private LclLabel lbprsUncalAmtRespF;

	public LclLabel lbprsUncalAmtRespFU;

	private LclLabel lbprsUncalBase;

	private LclLabel lbprsUnitAfterScale;

	public Label lbptInitT;

	private LclLabel lbsbFlowRate;

	private LclLabel lbsbPersist;

	private LclLabel lbsbTimeTo;

	private LclLabel lclLabel17;

	private LclLabel lclLabel42;

	private LclLabel lclLabel43;

	private LclLabel lclLabel44;

	private LclLabel lclLabel45;

	private LclLabel lclLabel46;

	private LclLabel lclLabel47;

	private LclLabel lclLabel5;

	private LclLabel lclLabel6;

	private LclLabel lclLabel7;

	private ToolStripMenuItem miAddRow;

	private ToolStripMenuItem miDeleteRow;

	private ToolStripMenuItem miIntegAppendRow;

	private ToolStripMenuItem miIntegDeleteRows;

	private ToolStripMenuItem miIntegInsertRow;

	private ToolStripMenuItem miIntegResetRows;

	private ToolStripMenuItem miIntegRowsDown;

	private ToolStripMenuItem miIntegRowsUp;

	private ToolStripMenuItem milgAddRow;

	private ToolStripMenuItem milgDeleteRow;

	private OpenFileDialog openFileDialog_0;

	private OpenFileDialog openFileDialog_1;

	private OpenFileDialog openFileDialog_2;

	private LclPictureBox pbecDown;

	private LclPictureBox pbecUp;

	private LclPanel pnlecES;

	private GradientDisplay gradientDisplay_1;

	private LclRadioButton rbasAdd;

	private LclRadioButton rbasSub;

	private LclRadioButton rbccFrom50per;

	private LclRadioButton rbccStatistical;

	private LclRadioButton rbecDown;

	private LclRadioButton rbecStartOnly;

	private LclRadioButton rbecStartRestart;

	private LclRadioButton rbecStartStop;

	private LclRadioButton rbecUp;

	private LclRadioButton rbisInitial;

	private LclRadioButton rbisMonitorSet;

	private LclRadioButton rbisPumpOff;

	private LclRadioButton rbrtrAllDetectedPeaks;

	private LclRadioButton rbrtrCaliPeaks;

	private LclRadioButton rbrtrIdentifiedPeaks;

	private LclRadioButton rbupAll;

	private LclRadioButton rbupFive;

	private LclTextBox tbacqRunTime;

	private LclTextBox tbasChrom;

	private LclTextBox tbccColumnLength;

	private LclTextBox tbcclCalibration;

	private LclTextBox tbccUnretainedPeak;

	private LclTextBox tblgoptSolvent1;

	private LclTextBox tblgoptSolvent2;

	private LclTextBox tblgoptSolvent3;

	private LclTextBox tblgoptSolvent4;

	private LclTextBox tblsoFrom;

	private LclTextBox tblsoMatchFactorThreshold;

	private LclTextBox tblsoMaxNumHits;

	private LclTextBox tblsoRestrictRT;

	private LclTextBox tblsoTo;

	private LclTextBox tbmsmColumn;

	private LclTextBox tbmsmDetection;

	private LclTextBox tbmsmFlowRate;

	private LclTextBox tbmsmMobilePhase;

	private LclTextBox tbmsmMtdDspt;

	private LclTextBox tbmsmNote;

	private LclTextBox tbmsmPressure;

	private LclTextBox tbmsmTemperature;

	private LclTextBox tbppoAbsorbanceThreshold;

	private LclTextBox tbppoFrom;

	private LclTextBox tbppoPurityThreshold;

	private LclTextBox tbppoTo;

	private LclTextBox tbprsScaleFactor;

	private LclTextBox tbprsUncalAmtRespF;

	private LclTextBox tbprsUnitAfterScale;

	public TextBox tbptIniTempHoldT;

	private LclTextBox tbsbFlowRate;

	private LclTextBox tbsbPersist;

	private LclTextBox tbsbTimeTo;

	private TextBox tbwsFrom;

	private TextBox tbwsStartT;

	private TextBox tbwsStep;

	private TextBox tbwsStepFreq;

	private TextBox tbwsTo;

	public LclTabControl tcMethod;

	private ToolStripSeparator toolStripSeparator1;

	private ToolStripSeparator toolStripSeparator2;

	public TabPage tpAcquisition;

	private TabPage tpAdvanced;

	private TabPage tpAS;

	public TabPage tpCaculation;

	private TabPage tpGC;

	public TabPage tpGradient;

	public TabPage tpIntegration;

	private TabPage tpLC;

	public TabPage tpMeasurement;

	private TabPage tpPDA;

	private TabPage tpRangesGPC;

	public TabPage tpTempProg;

	private TabPage tpUV;

	private LclGroupBox gblcOption;

	public void LoadInstrument(Instrument instrument)
	{
		base.instrument = instrument;
	}

	private void btnasNoneChrom_Click(object sender, EventArgs e)
	{
		ChromInfo chromInfo = mtdSetup_0.chromInfo;
		if (sender == btnasSetChrom)
		{
			if (openFileDialog_0 == null)
			{
				openFileDialog_0 = new OpenFileDialog();
				openFileDialog_0.Title = "设置加/减谱图";
				openFileDialog_0.Filter = Class49.MakeFileFilter(".sda") + "|" + Class49.MakeFileFilter(".dat");
				openFileDialog_0.FilterIndex = 2;
			}
			openFileDialog_0.InitialDirectory = ((chromInfo.asDirectory != "") ? chromInfo.asDirectory : instrument.PrjPath);
			if (openFileDialog_0.ShowDialog() != DialogResult.OK)
			{
				return;
			}
			chromInfo.asChrom = openFileDialog_0.FileName;
		}
		if (sender == btnasNoneChrom)
		{
			chromInfo.asChrom = "";
		}
		chromInfo.RefreshAsInfo();
		tbasChrom.Text = chromInfo.asShowName;
	}

	private void btncclView_Click(object sender, EventArgs e)
	{
		ChromInfo chromInfo = mtdSetup_0.chromInfo;
		if (sender == btncclSet)
		{
			if (openFileDialog_2 == null)
			{
				openFileDialog_2 = new OpenFileDialog();
				openFileDialog_2.Title = btncclSet.Text;
			}
			openFileDialog_2.InitialDirectory = ((chromInfo.cclDirectory != "") ? chromInfo.cclDirectory : instrument.PrjPath);
			if (instrument.instruStyle != InstruStyle.GPC)
			{
				openFileDialog_2.Filter = CaliGnlUserCtrl.Filter;
			}
			else
			{
				openFileDialog_2.Filter = CaliGpcForm.Filter;
			}
			if (openFileDialog_2.ShowDialog() != DialogResult.OK)
			{
				return;
			}
			chromInfo.cclCalibration = openFileDialog_2.FileName;
			chromInfo.LoadFromFile();
			tbcclCalibration.Text = chromInfo.cclShowName;
		}
		if (sender == btncclNone)
		{
			tbcclCalibration.Text = (chromInfo.cclCalibration = (chromInfo.cclDirectory = (chromInfo.cclShowName = "")));
			chromInfo.LoadFromFile();
		}
		if (sender == btncclView)
		{
			string cclCalibration = chromInfo.cclCalibration;
			if (cclCalibration != "")
			{
				if (File.Exists(cclCalibration))
				{
					instrument.form.btnCaliWindow_Click(null, null);
					instrument.form.LoadCaliFile(cclCalibration);
				}
				else
				{
					MessageBox.Show("文件无效");
				}
			}
		}
		else
		{
			method_15();
		}
	}

	private void btnPTSet_Click(object sender, EventArgs e)
	{
		byte byte_ = byte.MaxValue;
		if (sender == btnPTQry)
		{
			byte_ = 1;
		}
		if (sender == btnExtEvTPQry)
		{
			byte_ = 2;
		}
		if (sender == btnPTSet)
		{
			byte_ = 9;
		}
		if (sender == btnExtEvTPSet)
		{
			byte_ = 10;
		}
		for (int i = 0; i < instrument.gcc_GCss.Length; i++)
		{
			if (instrument.gcc_GCss[i] is GC08_GCs)
			{
				(instrument.gcc_GCss[i] as GC08_GCs).Send(byte_);
			}
		}
	}

	private void method_0(object sender, EventArgs e)
	{
	}

	private void btnmtdApply_Click(object sender, EventArgs e)
	{
		if (instrument.user.uar_EditMethod)
		{
			method_9(AccStyle.Write);
		}
		else
		{
			MessageBox.Show(Lang.PS("受限！", "No Right！"));
		}
		Array.Sort(mtdSetup_0.chromInfoR.LcGradient.gradientRows);
		method_9(AccStyle.Read);
		mtdSetup_1.Copy(mtdSetup_0);
		instrument.ApplyMethod();
		method_3();
	}

	private void method_1(int int_0)
	{
		if (tcMethod.SelectedTab == tpAS)
		{
			method_7(int_0, AccStyle.Write);
		}
	}

	private void cbASsSelect_SelectedIndexChanged(object sender, EventArgs e)
	{
		if (tcMethod.SelectedTab == tpAS)
		{
			method_7(cbASsSelect.SelectedIndex, AccStyle.Read);
		}
	}

	private void method_2(int int_0)
	{
		if (tcMethod.SelectedTab == tpAcquisition)
		{
			method_6(int_0, AccStyle.Write);
		}
		else if (tcMethod.SelectedTab == tpIntegration)
		{
			method_12(int_0, AccStyle.Write);
		}
	}

	private void cbDtsSelect_SelectedIndexChanged(object sender, EventArgs e)
	{
		if (tcMethod.SelectedTab == tpAcquisition)
		{
			method_6(cbDtsSelect.SelectedIndex, AccStyle.Read);
		}
		else if (tcMethod.SelectedTab == tpIntegration)
		{
			method_12(cbDtsSelect.SelectedIndex, AccStyle.Read);
		}
	}

	private void cblgoptSolvent1_Click(object sender, EventArgs e)
	{
		gvlcGradient_CellEndEdit(null, null);
	}

	private void cbprsUseScaleFactor_Click(object sender, EventArgs e)
	{
		LclTextBox lclTextBox = tbprsScaleFactor;
		bool enabled = (tbprsUnitAfterScale.Enabled = cbprsUseScaleFactor.Checked);
		lclTextBox.Enabled = enabled;
	}

	private bool method_3()
	{
		if (instrument.lcc_Pumps.Length == 2 && mtdSetup_0.chromInfoR.LcGradient.gradientOption.SolventNum != instrument.lcc_Pumps.Length)
		{
			MessageBox.Show(Lang.PS("双泵应设置两元！", "Two pumps, should set two units!"), Lang.PS("方法", "Method"), MessageBoxButtons.OK, MessageBoxIcon.Hand);
			return false;
		}
		return true;
	}

	private void cmsIntegration_Opening(object sender, CancelEventArgs e)
	{
		ToolStripMenuItem toolStripMenuItem = miIntegDeleteRows;
		ToolStripMenuItem toolStripMenuItem2 = miIntegRowsUp;
		bool flag = (miIntegRowsDown.Enabled = gvInteg.SelectedRows != null && gvInteg.SelectedRows.Count != 0);
		bool enabled = (toolStripMenuItem2.Enabled = flag);
		toolStripMenuItem.Enabled = enabled;
	}

	private void dgvPT_DataError(object sender, DataGridViewDataErrorEventArgs e)
	{
		DataGridView dataGridView = sender as DataGridView;
		int rowIndex = e.RowIndex;
		int columnIndex = e.ColumnIndex;
		MessageBox.Show(e.Exception.Message + rowIndex.ToString("\n行: 0") + columnIndex.ToString(" 列: 0\n值: ") + dataGridView.Rows[rowIndex].Cells[columnIndex].Value, "输入错误", MessageBoxButtons.OK, MessageBoxIcon.Hand);
		e.ThrowException = false;
	}

	public void dgvPT_CellEndEdit(object sender, DataGridViewCellEventArgs e)
	{
		method_8(AccStyle.Write, gcProgTemp_0);
		method_8(AccStyle.Read, gcProgTemp_0);
		refresh_dpgcProgTemp();
	}

	private void dpgcProgTemp_Paint(object sender, PaintEventArgs e)
	{
		gradientDisplay_1.Draw(e.Graphics, erase: true);
	}

	private void dplcGradient_Paint(object sender, PaintEventArgs e)
	{
		gradientDisplay_0.Draw(e.Graphics, erase: true);
	}

	private void gbcclParas_Paint(object sender, PaintEventArgs e)
	{
		Point point = new Point(5, tbprsUnitAfterScale.Bottom + 5);
		Point pt = point;
		pt.X = gbcclParas.Width - point.X;
		e.Graphics.DrawLine(Pens.LightGray, point, pt);
	}

	public void GetGrdtDisColumns(ref GvInfos gvInfos, MtdSetup mtdSetup)
	{
		ReadLcGradient(gvlcGradient, mtdSetup.chromInfoR.LcGradient);
		Class49.SetGridViewInfo(gvlcGradient, ref gvInfos, null);
		for (int i = 0; i < gvInfos.colHdrTxts.Length; i++)
		{
			string text = "";
			string text2 = gvInfos.colNames[i];
			if (text2 != null)
			{
				switch (text2)
				{
				case "D":
					text = mtdSetup.chromInfoR.LcGradient.gradientOption.solvent4Name + "\n[%]";
					break;
				case "C":
					text = mtdSetup.chromInfoR.LcGradient.gradientOption.solvent3Name + "\n[%]";
					break;
				case "B":
					text = mtdSetup.chromInfoR.LcGradient.gradientOption.solvent2Name + "\n[%]";
					break;
				case "A":
					text = mtdSetup.chromInfoR.LcGradient.gradientOption.solvent1Name + "\n[%]";
					break;
				}
			}
			gvInfos.colWidths[i] = 80;
			if (text != "")
			{
				gvInfos.colHdrTxts[i] = text;
			}
		}
	}

	public void GetItgDisColumns(ref GvInfos gvInfos)
	{
		Class49.SetGridViewInfo(gvInteg, ref gvInfos, null);
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

	public void GetProgTempDisColumns(ref GvInfos gvInfos, MtdSetup mtdSetup)
	{
		ReadGcProgTemp(dgvPT, mtdSetup.chromInfoR.GcProgTemp);
		Class49.SetGridViewInfo(dgvPT, ref gvInfos, null);
		for (int i = 0; i < gvInfos.colHdrTxts.Length; i++)
		{
			gvInfos.colWidths[i] = 80;
		}
	}

	private void method_4()
	{
		gvgrPercent.Columns["LowPercent"].HeaderText = Lang.PS("低百分比", "Low Percent");
		gvgrPercent.Columns["HightPercent"].HeaderText = Lang.PS("高百分比", "Hight Percent");
		gvgrMw.Columns["HighMw"].HeaderText = Lang.PS("高分子量", "High Mw");
		gvgrMw.Columns["LowMw"].HeaderText = Lang.PS("低分子量", "Low Mw");
	}

	public string gvGrdtValue(GradientRow gradientRow_0, string columnName)
	{
		float num = 0f;
		string text = gvlcGradient.ConvertValFmt(columnName);
		if (columnName != null)
		{
			switch (columnName)
			{
			case "A":
				num = gradientRow_0.float_0 * 100f;
				break;
			case "B":
				num = gradientRow_0.float_1 * 100f;
				break;
			case "C":
				num = gradientRow_0.float_2 * 100f;
				break;
			case "D":
				num = gradientRow_0.float_3 * 100f;
				break;
			case "Flow":
				num = gradientRow_0.flow;
				break;
			case "Time":
				num = gradientRow_0.time;
				break;
			}
		}
		return num.ToString(text);
	}

	private void gvlcGradient_CellEndEdit(object sender, DataGridViewCellEventArgs e)
	{
		method_11(AccStyle.Write, lcGradient_0);
		method_11(AccStyle.Read, lcGradient_0);
		method_10();
	}

	private void method_5()
	{
		gvpdaLibs.Columns["Used"].HeaderText = Lang.PS("使用", "Used");
		gvpdaLibs.Columns["Library"].HeaderText = Lang.PS("匹配库", "Match Library");
	}

	public string gvProgTempValue(ProgTRow progTempRow, string columnName)
	{
		float num = 0f;
		string text = dgvPT.ConvertValFmt(columnName);
		if (columnName != null)
		{
			switch (columnName)
			{
			case "EndTemp":
				num = progTempRow.endTemp;
				break;
			case "HoldTime":
				num = progTempRow.holdTime;
				break;
			case "UpRate":
				num = progTempRow.upRate;
				break;
			}
		}
		return num.ToString(text);
	}

	public static void Init_gvgcProgTemp(LclGridView dgvPT)
	{
		LclgvTextBoxColumn lclgvTextBoxColumn = dgvPT.AddLclTextBoxColumn("UpRate", 100, 3, StringAlignment.Far, readOnly: false);
		lclgvTextBoxColumn.HeaderText = Lang.PS("升温\n[℃/min]", "UpRate\n[℃/min]");
		lclgvTextBoxColumn.ValueType = typeof(float);
		lclgvTextBoxColumn.Width = 60;
		lclgvTextBoxColumn.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
		lclgvTextBoxColumn.DefaultCellStyle.Format = "f1";
		lclgvTextBoxColumn = dgvPT.AddLclTextBoxColumn("EndTemp", 100, 3, StringAlignment.Far, readOnly: false);
		lclgvTextBoxColumn.HeaderText = Lang.PS("终温\n[℃]", "End Temp\n[℃]");
		lclgvTextBoxColumn.ValueType = typeof(float);
		lclgvTextBoxColumn.Width = 60;
		lclgvTextBoxColumn.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
		lclgvTextBoxColumn.DefaultCellStyle.Format = "f1";
		lclgvTextBoxColumn = dgvPT.AddLclTextBoxColumn("HoldTime", 100, 3, StringAlignment.Far, readOnly: false);
		lclgvTextBoxColumn.HeaderText = Lang.PS("保持\n[min]", "Hold T\n[min]");
		lclgvTextBoxColumn.ValueType = typeof(float);
		lclgvTextBoxColumn.Width = 60;
		lclgvTextBoxColumn.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
		lclgvTextBoxColumn.DefaultCellStyle.Format = "f1";
		dgvPT.RowCount = 16;
	}

	public static void Init_gvGradient(LclGridView gvLcGradient)
	{
		gvLcGradient.AddLclTextBoxColumn("Time", 50, StringAlignment.Far).DefaultCellStyle.Format = "f2";
		gvLcGradient.AddLclTextBoxColumn("A", 50, StringAlignment.Far).DefaultCellStyle.Format = "f2";
		gvLcGradient.AddLclTextBoxColumn("B", 50, StringAlignment.Far).DefaultCellStyle.Format = "f2";
		gvLcGradient.AddLclTextBoxColumn("C", 50, StringAlignment.Far).DefaultCellStyle.Format = "f2";
		gvLcGradient.AddLclTextBoxColumn("D", 50, StringAlignment.Far).DefaultCellStyle.Format = "f2";
		gvLcGradient.AddLclTextBoxColumn("Flow", 70, StringAlignment.Far).DefaultCellStyle.Format = "f3";
		gvLcGradient.Rows.Add();
		gvLcGradient.Rows[0].Cells[0].ReadOnly = true;
	}

	public DialogResult JustShow(MtdSetup methodSetup, MtdDlgInitStyle mtdDlgInitStyle)
	{
		mtdSetup_1 = methodSetup;
		btnmtdApply.Visible = false;
		cbASsSelect.ClearItems();
		cbDtsSelect.ClearItems();
		for (int i = 0; i < methodSetup.dtcAcquisitions.Count; i++)
		{
			cbDtsSelect.Items.Add("检测器 " + i);
		}
		method_14(mtdDlgInitStyle);
		DialogResult dialogResult = ShowDialog();
		if (dialogResult == DialogResult.OK)
		{
			method_9(AccStyle.Write);
			methodSetup.Copy(mtdSetup_0);
		}
		return dialogResult;
	}

	public override void LoadLanguage()
	{
		base.LoadLanguage();
		gvInteg.EndEdit();
		tpAS.Text = Lang.PS("自动进样器", "AS Control");
		tpGC.Text = Lang.PS("气相", "GC Control");
		tpLC.Text = Lang.PS("液相", "LC Control");
		tpGradient.Text = Lang.PS("液相梯度", "LC Gradient");
		tpMeasurement.Text = Lang.PS("测量", "Measurement");
		tpAcquisition.Text = Lang.PS("采集", "Acquisition");
		tpIntegration.Text = Lang.PS("积分", "Integration");
		tpCaculation.Text = Lang.PS("计算", "Caculation");
		tpAdvanced.Text = Lang.PS("高级", "Advanced");
		tpPDA.Text = Lang.PS("PDA 方法", "PDA Method");
		tpRangesGPC.Text = Lang.PS("分段", "Ranges");
		cblcUse.Text = Lang.PS("使用梯度表", "Use Gradient Table");
		gblcStandBy.Text = Lang.PS("监控设置", "Monitor Set");
		lbsbFlowRate.Text = Lang.PS("流速", "Flow Rate");
		lbsbTimeTo.Text = Lang.PS("进入", "Time To");
		lbsbPersist.Text = Lang.PS("持续", "Persist");
		gblcIdleState.Text = Lang.PS("空闲状态", "Idle State");
		rbisPumpOff.Text = Lang.PS("关泵", "Pump Off");
		rbisInitial.Text = Lang.PS("初始", "Initial");
		rbisMonitorSet.Text = Lang.PS("监控设置", "Monitor Set");
		gblcOption.Text = Lang.PS("梯度选项", "Gradient Options");
		cblgoptSolvent1.Text = Lang.PS("溶剂 1", "Solvent 1");
		cblgoptSolvent2.Text = Lang.PS("溶剂 2", "Solvent 2");
		cblgoptSolvent3.Text = Lang.PS("溶剂 3", "Solvent 3");
		cblgoptSolvent4.Text = Lang.PS("溶剂 4", "Solvent 4");
		lbmsmMtdDspt.Text = Lang.PS("方法描述", "Method Description");
		lbmsmColumn.Text = ((instrument.instruStyle != InstruStyle.GC) ? Lang.PS("色谱柱", "Column") : Lang.PS("色谱柱", "Carrier Flow"));
		lbmsmMobilePhase.Text = ((instrument.instruStyle != InstruStyle.GC) ? Lang.PS("流动相", "Mobile Phase") : Lang.PS("柱温", "Air Flow"));
		lbmsmFlowRate.Text = ((instrument.instruStyle != InstruStyle.GC) ? Lang.PS("流速", "Flow Rate") : Lang.PS("载气", "H2 Flow"));
		lbmsmPressure.Text = ((instrument.instruStyle != InstruStyle.GC) ? Lang.PS("压力", "Pressure") : Lang.PS("气体1", "Inj. Flow"));
		lbmsmDetection.Text = ((instrument.instruStyle != InstruStyle.GC) ? Lang.PS("检测", "Detection") : Lang.PS("气体2", "Column Temp."));
		lbmsmTemperature.Text = ((instrument.instruStyle != InstruStyle.GC) ? Lang.PS("温度", "Temperature") : Lang.PS("检测器", "Detector Temp."));
		lbmsmNote.Text = Lang.PS("备注", "Note");
		gbmsmAcquisition.Text = Lang.PS("采集", "Acquisition");
		cbacqAutoStop.Text = Lang.PS("自动结束", "Enable Autostop");
		lbacqRunTime.Text = Lang.PS("运行时间", "Run Time");
		gbmsmExternalControl.Text = Lang.PS("外部控制", "External Control");
		cbecExternalControl.Text = Lang.PS("外部开始/结束", "External Start/Stop");
		rbecStartOnly.Text = Lang.PS("仅开始", "Start Only");
		rbecStartRestart.Text = Lang.PS("开始-开始", "Start - Restart");
		rbecStartStop.Text = Lang.PS("开始-结束", "Start - Stop");
		rbecUp.Text = Lang.PS("上升沿", "Up");
		rbecDown.Text = Lang.PS("下降沿", "Down");
		lbacqDetector.Text = Lang.PS("***检测器", "***Detector");
		lbacqRange.Text = Lang.PS("范围 [", "Range [") + Class49.MesureUnit() + "]";
		lbacqRate.Text = Lang.PS("采样频率[Hz]", "Sample Rate[Hz]");
		miIntegAppendRow.Text = Lang.PS("添加行", "Append Row");
		miIntegInsertRow.Text = Lang.PS("插入行", "Insert Row");
		miIntegDeleteRows.Text = Lang.PS("删除行", "Delete Row(s)");
		miIntegRowsUp.Text = Lang.PS("上移", "Rows Up");
		miIntegRowsDown.Text = Lang.PS("下移", "Rows Down");
		miIntegResetRows.Text = Lang.PS("重置", "Reset");
		lbcclCalibration.Text = Lang.PS("校正文件[峰表]", "Calibration\n[Peak Table]");
		btncclView.Text = Lang.PS("查看", "View");
		btncclSet.Text = Lang.PS("设置...", "Set...");
		btncclNone.Text = Lang.PS("置空", "None");
		lbcclCalcu.Text = Lang.PS("计算", "Calculation");
		lbcclAuthor.Text = Lang.PS("作者", "Author");
		lbcclDescription.Text = Lang.PS("描述", "Description");
		lbcclCreateTime.Text = Lang.PS("创建时间", "Create Time");
		lbcclModifiedTime.Text = Lang.PS("修改时间", "Modified Time");
		gbcclRltTableReport.Text = Lang.PS("结果表报告", "Report in Result Table");
		cbrtrHideISTDPeak.Text = Lang.PS("隐藏内标峰", "Hide ISTD Peak");
		rbrtrAllDetectedPeaks.Text = Lang.PS("所有检测峰", "All Detected Peaks");
		rbrtrIdentifiedPeaks.Text = Lang.PS("所有识别峰", "All Identified Peaks");
		rbrtrCaliPeaks.Text = Lang.PS("所有校正峰", "All Peaks in Calibration");
		gbcclParas.Text = Lang.PS("参数", "Parameters");
		cbprsUseScaleFactor.Text = Lang.PS("使用缩放因子", "Use Scale Factor");
		lbprsScaleFactor.Text = Lang.PS("缩放因子", "Scale Factor");
		lbprsUnitAfterScale.Text = Lang.PS("缩放后单位", "Use Unit");
		lbprsUncalBase.Text = Lang.PS("未识别响应", "Uncal. Base");
		lbprsUncalAmtRespF.Text = Lang.PS("未识别因子", "Uncal. Factor");
		gbadvAddSub.Text = Lang.PS("加减谱图", "Add Subtraction");
		lbasChrom.Text = Lang.PS("谱图", "Chromatogram");
		lbasMatching.Text = Lang.PS("匹配方式", "Matching");
		btnasSetChrom.Text = Lang.PS("设置...", "Set...");
		btnasNoneChrom.Text = Lang.PS("置空", "None");
		gbadvColumnCalcu.Text = Lang.PS("柱效计算", "Column Caculation");
		lbccUnretainedPeak.Text = Lang.PS("非保留峰时间", "Unretained Peak");
		lbccColumnLength.Text = Lang.PS("柱长", "Column Length");
		rbccStatistical.Text = Lang.PS("静态时间", "Statistical Moments");
		rbccFrom50per.Text = Lang.PS("50%宽起始", "From Width at 50%");
		gbpdaPeakPurityOptions.Text = Lang.PS("峰纯度选项", "Peak Purity Options");
		cbppoRestrictWaveLength.Text = Lang.PS("限制波长范围", "Restrict Wavelength Range");
		LclLabel lclLabel = lbppoFrom;
		string text = (lblsoFrom.Text = Lang.PS("从:", "from:"));
		lclLabel.Text = text;
		LclLabel lclLabel2 = lbppoTo;
		text = (lblsoTo.Text = Lang.PS("到:", "to:"));
		lclLabel2.Text = text;
		lbppoPurityThreshold.Text = Lang.PS("纯度极限", "Purity Threshold");
		lbppoAbsorbanceThreshold.Text = Lang.PS("吸收极限", "Absorbance Threshold");
		gbppoUsedPoints.Text = Lang.PS("使用点数", "Used Points");
		rbupAll.Text = Lang.PS("全部", "All");
		rbupFive.Text = Lang.PS("五点", "Five");
		cbppoUseBackCorr.Text = Lang.PS("使用背景修正", "Use Background Correction");
		gbpdaLibSearchOptions.Text = Lang.PS("库分析选项", "Library Search Options");
		lblsoMatchCriteria.Text = Lang.PS("匹配规则", "Match Criteria");
		lblsoMatchFactorThreshold.Text = Lang.PS("匹配因子极限", "Match Factor Threshold");
		lblsoMaxNumHits.Text = Lang.PS("最大显示波数", "Max. Number of Hits");
		cblsoRestrictWaveLength.Text = Lang.PS("限制波长范围", "Restrict Wavelength Range");
		cblsoRestrictRT.Text = Lang.PS("限制保留时间", "Restrict Reten. Time");
		cblsoForAllDetectedPeaks.Text = Lang.PS("所有检测峰", "For All Detected Peaks");
		cblsoUseBackCorr.Text = Lang.PS("使用背景修正", "Use Background Correction");
		miAddRow.Text = Lang.PS("添加库", "Add Library");
		miDeleteRow.Text = Lang.PS("删除库", "Delete Library");
		lbgrPercent.Text = Lang.PS("百分比类型GPC表", "Percent Type GPC Ranges Table");
		lbgrMw.Text = Lang.PS("分子量类型GPC表", "Mw Type GPC Ranges Table");
		btnmtdApply.Text = Lang.PS("应用", "Apply");
	}

	private void MtdSetupDlg_Load(object sender, EventArgs e)
	{
		cbUseProgWave.Text = Lang.PS("使用程序波长", "Use Program Wave");
		gvProgWave.Columns[0].HeaderText = Lang.PS("时间[min]", "Time[min]");
		gvProgWave.Columns[1].HeaderText = Lang.PS("波长[nm]", "Wave[nm]");
		rbasAdd.Text = Lang.PS("加", "Add");
		rbasSub.Text = Lang.PS("减", "Subtract");
		tpTempProg.Text = Lang.PS("程序升温", "Prog. Temp.");
		cbASsSelect.Location = cbDtsSelect.Location;
		cbacqRange.ItemExtString = " " + Class49.MesureUnit();
		cbacqRange.Items.Add(2500f);
		cbacqRate.ItemExtString = " Hz";
		cbacqRate.Items.Add(15f);
		cbacqRate.Items.Add(30f);
		cbacqRate.Items.Add(60f);
		cbacqRate.Items.Add(120f);
		Init_gvGradient(gvlcGradient);
		RefreshHeaders_gvGradient(gvlcGradient);
		Init_gvgcProgTemp(dgvPT);
		pbecUp.Image = SystemBitmapResource6.smethod_1();
		pbecDown.Image = SystemBitmapResource6.smethod_0();
		gvInteg.Dock = DockStyle.Fill;
		gvInteg.BorderStyle = BorderStyle.None;
		gvInteg.InitColumns();
		gvInteg.LoadLanguage();
		cbcclCalcu.InitItems(new object[3]
		{
			CalcuStyle.Uncal,
			CalcuStyle.ESTD,
			CalcuStyle.ISTD
		});
		cbcclCalcu.InitShowText(new string[3]
		{
			Lang.PS("无校正", "Uncal"),
			Lang.PS("外标法", "ESTD"),
			Lang.PS("内标法", "ISTD")
		});
		cbprsUncalBase.InitItems(new object[4]
		{
			RespStyle.Area,
			RespStyle.Height,
			RespStyle.AreaSquare,
			RespStyle.PeakHeightSquare
		});
		cbprsUncalBase.InitShowText(new string[4]
		{
			Lang.PS("面积", "Area"),
			Lang.PS("高度", "Height"),
			Lang.PS("面积平方根", "AreaSquare"),
			Lang.PS("高度平方根", "PeakHeightSquare")
		});
		cbasMatching.InitItems(new object[3]
		{
			ASMatchStyle.NoChange,
			ASMatchStyle.OffsetChrom,
			ASMatchStyle.ScaleChrom
		});
		cbasMatching.InitShowText(new string[3]
		{
			Lang.PS("无变化", "No Change"),
			Lang.PS("偏移谱图", "Offset Chrom"),
			Lang.PS("缩放谱图", "Scale Chrom")
		});
		cblsoMatchCriteria.InitItems(new object[3]
		{
			LSO_MatchCriteria.LeastSquare,
			LSO_MatchCriteria.WeightedLeastSquare,
			LSO_MatchCriteria.Correlation
		});
		cblsoMatchCriteria.InitShowText(new string[3]
		{
			Lang.PS("最小平方", "Least Square"),
			Lang.PS("重量最小平方", "Weighted Least Square"),
			Lang.PS("修正", "Correlation")
		});
		openFileDialog_1.Tag = "PDA";
		openFileDialog_1.Filter = Class49.MakeFileFilter(".lib");
		gvpdaLibs.AddLclCheckBoxColumn("Used", 35);
		gvpdaLibs.AddLclTextBoxCtxBtnColumn("Library", 270, StringAlignment.Near, openFileDialog_1);
		method_5();
		gvpdaLibs.RowCount = 7;
		gvgrPercent.AddLclTextBoxColumn("LowPercent", 110, StringAlignment.Center);
		gvgrPercent.AddLclTextBoxColumn("HightPercent", 110, StringAlignment.Center);
		gvgrMw.AddLclTextBoxColumn("HighMw", 110, StringAlignment.Center);
		gvgrMw.AddLclTextBoxColumn("LowMw", 110, StringAlignment.Center);
		DataGridView dataGridView = gvgrPercent;
		gvgrPercent.AllowUserToDeleteRows = true;
		dataGridView.AllowUserToAddRows = true;
		DataGridView dataGridView2 = gvgrMw;
		gvgrMw.AllowUserToDeleteRows = true;
		dataGridView2.AllowUserToAddRows = true;
		gradientDisplay_0 = new GradientDisplay(WinStyle.Method, dplcGradient);
		gradientDisplay_0.instruStyle = InstruStyle.LC;
		gradientDisplay_0.txtY = "Flow";
		gradientDisplay_0.unitY = "mL/min";
		gradientDisplay_0.fmtY = "0.0";
		gradientDisplay_0.txtY_ = "Gradient";
		gradientDisplay_0.unitY_ = "%";
		gradientDisplay_0.refScaleY_Num = 4;
		gradientDisplay_1 = new GradientDisplay(WinStyle.Method, dpgcProgTemp);
		gradientDisplay_1.instruStyle = InstruStyle.GC;
		gradientDisplay_1.txtY = "Temp.";
		gradientDisplay_1.unitY = "℃";
		gradientDisplay_1.fmtY = "0.0";
		gvExtEvTP.RowCount = 8;
		for (int i = 0; i < gvExtEvTP.RowCount; i++)
		{
			gvExtEvTP.Rows[i].HeaderCell.Value = (i + 1).ToString();
		}
		DataGridViewTextBoxColumn dataGridViewTextBoxColumn = clmExtEvTP0;
		DataGridViewTextBoxColumn dataGridViewTextBoxColumn2 = clmExtEvTP1;
		DataGridViewTextBoxColumn dataGridViewTextBoxColumn3 = clmExtEvTP2;
		Type type = (clmExtEvTP3.ValueType = typeof(float));
		Type type2 = (dataGridViewTextBoxColumn3.ValueType = type);
		Type valueType = (dataGridViewTextBoxColumn2.ValueType = type2);
		dataGridViewTextBoxColumn.ValueType = valueType;
		gradientDisplay_0.LinkOptions(instrument.user.options);
		gradientDisplay_1.LinkOptions(instrument.user.options);
		mtdSetup_0.Copy(mtdSetup_1);
		method_9(AccStyle.Read);
		if (cbDtsSelect.SelectedIndex == -1 && cbDtsSelect.Items.Count != 0)
		{
			cbDtsSelect.SelectedIndex = 0;
		}
		method_3();
	}

	private void miIntegAppendRow_Click(object sender, EventArgs e)
	{
		gvInteg.RowCount++;
	}

	private void miIntegDeleteRows_Click(object sender, EventArgs e)
	{
		gvInteg.DeleteSelectedRows();
	}

	private void miIntegInsertRow_Click(object sender, EventArgs e)
	{
		int num = gvInteg.Rows.Count - 1;
		for (int num2 = num; num2 >= 0; num2--)
		{
			if (gvInteg.Rows[num2].Selected)
			{
				num = num2;
			}
		}
		gvInteg.Rows.Insert(num, 1);
	}

	private void miIntegResetRows_Click(object sender, EventArgs e)
	{
		gvInteg.Refresh(AccStyle.Clear, null);
	}

	private void miIntegRowsDown_Click(object sender, EventArgs e)
	{
		gvInteg.AdjustSelectedRows(AdjustUpDown.Down);
	}

	private void miIntegRowsUp_Click(object sender, EventArgs e)
	{
		gvInteg.AdjustSelectedRows(AdjustUpDown.Up);
	}

	private void milgDeleteRow_Click(object sender, EventArgs e)
	{
		if (tcMethod.SelectedTab == tpGradient)
		{
			if (sender == milgAddRow)
			{
				gvlcGradient.RowCount++;
			}
			else if (sender == milgDeleteRow)
			{
				DataGridViewRow[] array = new DataGridViewRow[gvlcGradient.SelectedRows.Count];
				for (int i = 0; i < array.Length; i++)
				{
					array[i] = gvlcGradient.SelectedRows[i];
				}
				for (int j = 0; j < array.Length; j++)
				{
					gvlcGradient.Rows.Remove(array[j]);
				}
			}
			gvlcGradient_CellEndEdit(null, null);
		}
		if (tcMethod.SelectedTab == tpTempProg)
		{
			if (sender == milgAddRow)
			{
				dgvPT.RowCount++;
			}
			else if (sender == milgDeleteRow)
			{
				DataGridViewRow[] array2 = new DataGridViewRow[dgvPT.SelectedRows.Count];
				for (int k = 0; k < array2.Length; k++)
				{
					array2[k] = dgvPT.SelectedRows[k];
				}
				for (int l = 0; l < array2.Length; l++)
				{
					dgvPT.Rows.Remove(array2[l]);
				}
			}
			dgvPT_CellEndEdit(null, null);
		}
		if (tcMethod.SelectedTab != tpUV)
		{
			return;
		}
		if (sender == milgAddRow)
		{
			gvProgWave.RowCount++;
		}
		else if (sender == milgDeleteRow)
		{
			DataGridViewRow[] array3 = new DataGridViewRow[gvProgWave.SelectedRows.Count];
			for (int m = 0; m < array3.Length; m++)
			{
				array3[m] = gvProgWave.SelectedRows[m];
			}
			for (int n = 0; n < array3.Length; n++)
			{
				gvProgWave.Rows.Remove(array3[n]);
			}
		}
	}

	private void MtdSetupDlg_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode == Keys.F1)
		{
			Class49.smethod_32("方法");
		}
	}

	public void OpenInstruInit()
	{
		cbASsSelect.ClearItems();
		for (int i = 0; i < instrument.asc_Samplers.Length; i++)
		{
			cbASsSelect.Items.Add(instrument.asc_Samplers[i].name);
		}
		cbDtsSelect.ClearItems();
		for (int j = 0; j < instrument.dtc_Channels.Length; j++)
		{
			cbDtsSelect.Items.Add(instrument.dtc_Channels[j].name);
		}
	}

	private void pnlecES_Paint(object sender, PaintEventArgs e)
	{
		Panel panel = sender as Panel;
		Point pt = new Point(0, 1);
		Point pt2 = new Point(panel.Width, pt.Y);
		e.Graphics.DrawLine(Pens.LightGray, pt, pt2);
		int num = (pt2.Y = panel.Height - 1);
		pt.Y = num;
		e.Graphics.DrawLine(Pens.LightGray, pt, pt2);
	}

	public void ReadGcProgTemp(LclGridView lclGridView_0, GcProgTemp gcProgTemp)
	{
		lbptInitT.Text = gcProgTemp.SetT6[1].ToString("0.0");
		tbptIniTempHoldT.Text = gcProgTemp.initHoldTime.ToString("0.0");
		ProgTRow[] progTempRows = gcProgTemp.progTempRows;
		for (int i = 0; i < lclGridView_0.RowCount; i++)
		{
			lclGridView_0.Rows[i].Cells[0].Value = gvProgTempValue(progTempRows[i], "UpRate");
			lclGridView_0.Rows[i].Cells[1].Value = gvProgTempValue(progTempRows[i], "EndTemp");
			lclGridView_0.Rows[i].Cells[2].Value = gvProgTempValue(progTempRows[i], "HoldTime");
		}
	}

	public void ReadLcGradient(LclGridView gvLcGradient, LcGradient lcGradient)
	{
		GrdtOpt gradientOption = lcGradient.gradientOption;
		GradientRow[] gradientRows = lcGradient.gradientRows;
		gvLcGradient.Columns["A"].Visible = gradientOption.hasSolvent1;
		gvLcGradient.Columns["A"].HeaderText = gradientOption.solvent1Name + "\n[%]";
		gvLcGradient.Columns["B"].Visible = gradientOption.hasSolvent2;
		gvLcGradient.Columns["B"].HeaderText = gradientOption.solvent2Name + "\n[%]";
		gvLcGradient.Columns["C"].Visible = gradientOption.hasSolvent3;
		gvLcGradient.Columns["C"].HeaderText = gradientOption.solvent3Name + "\n[%]";
		gvLcGradient.Columns["D"].Visible = gradientOption.hasSolvent4;
		gvLcGradient.Columns["D"].HeaderText = gradientOption.solvent4Name + "\n[%]";
		gvLcGradient.RowCount = gradientRows.Length;
		for (int i = 0; i < gvLcGradient.RowCount; i++)
		{
			if (i == 0)
			{
				gvLcGradient.Rows[i].Cells["Time"].Value = Lang.PS("初始", "Initial");
				gvLcGradient.Rows[i].Cells["Time"].ReadOnly = true;
			}
			else
			{
				gvLcGradient.Rows[i].Cells["Time"].Value = gvGrdtValue(gradientRows[i], "Time");
			}
			if (gvLcGradient.Columns["A"].Visible)
			{
				gvLcGradient.Rows[i].Cells["A"].Value = gvGrdtValue(gradientRows[i], "A");
			}
			if (gvLcGradient.Columns["B"].Visible)
			{
				gvLcGradient.Rows[i].Cells["B"].Value = gvGrdtValue(gradientRows[i], "B");
			}
			if (gvLcGradient.Columns["C"].Visible)
			{
				gvLcGradient.Rows[i].Cells["C"].Value = gvGrdtValue(gradientRows[i], "C");
			}
			if (gvLcGradient.Columns["D"].Visible)
			{
				gvLcGradient.Rows[i].Cells["D"].Value = gvGrdtValue(gradientRows[i], "D");
			}
			gvLcGradient.Rows[i].Cells["Flow"].Value = gvGrdtValue(gradientRows[i], "Flow");
		}
	}

	private void method_6(int int_0, AccStyle accStyle_0)
	{
		if (int_0 >= 0)
		{
			switch (accStyle_0)
			{
			case AccStyle.Read:
				cbacqRange.ShowValue(mtdSetup_0.dtcAcquisitions[int_0].AcqRange);
				cbacqRate.ShowValue(mtdSetup_0.dtcAcquisitions[int_0].AcqRate);
				return;
			case AccStyle.Write:
				cbacqRange.SetValue(ref mtdSetup_0.dtcAcquisitions[int_0].acqRange);
				cbacqRate.SetValue(ref mtdSetup_0.dtcAcquisitions[int_0].acqRate);
				return;
			default:
				return;
			case AccStyle.Clear:
				break;
			}
		}
		cbacqRange.SelectedIndex = -1;
		cbacqRate.SelectedIndex = -1;
	}

	private void method_7(int int_0, AccStyle accStyle_0)
	{
		if (int_0 >= 0 && accStyle_0 != AccStyle.Read)
		{
		}
	}

	private void method_8(AccStyle accStyle_0, GcProgTemp gcProgTemp_1)
	{
		dgvPT.EndEdit();
		switch (accStyle_0)
		{
		case AccStyle.Read:
			ReadGcProgTemp(dgvPT, gcProgTemp_1);
			break;
		case AccStyle.Write:
		{
			gcProgTemp_1.SetT6[1] = float.Parse(lbptInitT.Text);
			float.TryParse(tbptIniTempHoldT.Text, out gcProgTemp_1.initHoldTime);
			ProgTRow[] array = gcProgTemp_1.progTempRows;
			Array.Resize(ref array, dgvPT.RowCount);
			for (int i = 0; i < dgvPT.RowCount; i++)
			{
				array[i].upRate = Class49.String2Float(dgvPT.Rows[i].Cells[0].Value, 0f);
				array[i].endTemp = Class49.String2Float(dgvPT.Rows[i].Cells[1].Value, 0f);
				array[i].holdTime = Class49.String2Float(dgvPT.Rows[i].Cells[2].Value, 0f);
			}
			gcProgTemp_1.progTempRows = array;
			break;
		}
		}
	}

	private void method_9(AccStyle accStyle_0)
	{
		gvInteg.EndEdit();
		switch (accStyle_0)
		{
		case AccStyle.Write:
		{
			tcMethod_Deselecting(null, null);
			method_8(accStyle_0, mtdSetup_0.chromInfoR.GcProgTemp);
			method_11(AccStyle.Write, mtdSetup_0.chromInfoR.LcGradient);
			mtdSetup_0.chromInfoR.LcGradient.sbFlowRate = Class49.String2Float(tbsbFlowRate.Text, mtdSetup_0.chromInfoR.LcGradient.sbFlowRate);
			mtdSetup_0.chromInfoR.LcGradient.sbTimeTo = Class49.String2Float(tbsbTimeTo.Text, mtdSetup_0.chromInfoR.LcGradient.sbTimeTo);
			mtdSetup_0.chromInfoR.LcGradient.sbPersist = Class49.String2Float(tbsbPersist.Text, mtdSetup_0.chromInfoR.LcGradient.sbPersist);
			if (rbisPumpOff.Checked)
			{
				mtdSetup_0.chromInfoR.LcGradient.idleStateProc = IdleStateProc.PumpOff;
			}
			else if (rbisInitial.Checked)
			{
				mtdSetup_0.chromInfoR.LcGradient.idleStateProc = IdleStateProc.Initial;
			}
			else
			{
				mtdSetup_0.chromInfoR.LcGradient.idleStateProc = IdleStateProc.MonitorSet;
			}
			ChromInfoR chromInfoR2 = mtdSetup_0.chromInfoR;
			chromInfoR2.UvUseProgWaves = cbUseProgWave.Checked;
			ProgWaveRow progWaveRow = new ProgWaveRow();
			ProgWaveRow.NewArray(ref chromInfoR2.uvProgWaves, 0);
			for (int m = 0; m < gvProgWave.RowCount; m++)
			{
				if (gvProgWave.Rows[m].Cells[0].Value == null || gvProgWave.Rows[m].Cells[1].Value == null)
				{
					continue;
				}
				progWaveRow.Time = Class49.String2Float(gvProgWave.Rows[m].Cells[0].Value, 0f);
				progWaveRow.Wave = Class49.Object2Int(gvProgWave.Rows[m].Cells[1].Value, 440);
				if (!(progWaveRow.Time >= 0f) || progWaveRow.Wave < 190 || progWaveRow.Wave > 720)
				{
					continue;
				}
				bool flag = false;
				int num = chromInfoR2.UvProgWaves.Length;
				for (int n = 0; n < num; n++)
				{
					if (chromInfoR2.UvProgWaves[n].Time == progWaveRow.Time)
					{
						if (1 == 0)
						{
							ProgWaveRow.NewArray(ref chromInfoR2.uvProgWaves, num + 1);
							chromInfoR2.UvProgWaves[num] = progWaveRow;
						}
						break;
					}
				}
			}
			Array.Sort(chromInfoR2.UvProgWaves);
			chromInfoR2.UvWaveScan = cbWaveScan.Checked;
			chromInfoR2.UvwsStartT = Class49.String2Float(tbwsStartT.Text, chromInfoR2.UvwsStartT);
			chromInfoR2.UvwsStepFreq = Class49.Object2Int(tbwsStepFreq.Text, chromInfoR2.UvwsStepFreq);
			chromInfoR2.UvwsFrom = Class49.Object2Int(tbwsFrom.Text, chromInfoR2.UvwsFrom);
			chromInfoR2.UvwsTo = Class49.Object2Int(tbwsTo.Text, chromInfoR2.UvwsTo);
			chromInfoR2.UvwsStep = Class49.Object2Int(tbwsStep.Text, chromInfoR2.UvwsStep);
			mtdSetup_0.chromInfo.msmMtdDspt = tbmsmMtdDspt.Text;
			mtdSetup_0.chromInfo.msmColumn = tbmsmColumn.Text;
			mtdSetup_0.chromInfo.msmMobilePhase = tbmsmMobilePhase.Text;
			mtdSetup_0.chromInfo.msmFlowRate = tbmsmFlowRate.Text;
			mtdSetup_0.chromInfo.msmPressure = tbmsmPressure.Text;
			mtdSetup_0.chromInfo.msmDetection = tbmsmDetection.Text;
			mtdSetup_0.chromInfo.msmTemperature = tbmsmTemperature.Text;
			mtdSetup_0.chromInfo.msmNote = tbmsmNote.Text;
			mtdSetup_0.chromInfoR.AcqAutoStop = cbacqAutoStop.Checked;
			mtdSetup_0.chromInfoR.AcqRunTime = Class49.String2Float(tbacqRunTime.Text, mtdSetup_0.chromInfoR.AcqRunTime);
			mtdSetup_0.chromInfoR.EcExternalControl = cbecExternalControl.Checked;
			if (rbecStartOnly.Checked)
			{
				mtdSetup_0.chromInfoR.ExtCtrlStart = ExtCtrlStart.StartOnly;
			}
			else if (rbecStartRestart.Checked)
			{
				mtdSetup_0.chromInfoR.ExtCtrlStart = ExtCtrlStart.StartRestart;
			}
			else if (rbecStartStop.Checked)
			{
				mtdSetup_0.chromInfoR.ExtCtrlStart = ExtCtrlStart.StartStop;
			}
			if (rbecUp.Checked)
			{
				mtdSetup_0.chromInfoR.ExtCtrlSignal = ExtCtrlSignal.Up;
			}
			else if (rbecDown.Checked)
			{
				mtdSetup_0.chromInfoR.ExtCtrlSignal = ExtCtrlSignal.Down;
			}
			mtdSetup_0.chromInfo.cclCalcu = (CalcuStyle)cbcclCalcu.SelectedIndex;
			mtdSetup_0.chromInfo.rtrHideISTDPeak = cbrtrHideISTDPeak.Checked;
			if (rbrtrAllDetectedPeaks.Checked)
			{
				mtdSetup_0.chromInfo.rtrRltReportPeaks = RltReportPeaks.AllDetectedPeaks;
			}
			else if (rbrtrIdentifiedPeaks.Checked)
			{
				mtdSetup_0.chromInfo.rtrRltReportPeaks = RltReportPeaks.IdentifiedPeaks;
			}
			else if (rbrtrCaliPeaks.Checked)
			{
				mtdSetup_0.chromInfo.rtrRltReportPeaks = RltReportPeaks.CaliPeaks;
			}
			mtdSetup_0.chromInfo.prsUseScaleFactor = cbprsUseScaleFactor.Checked;
			mtdSetup_0.chromInfo.prsScaleFactor = Class49.String2Float(tbprsScaleFactor.Text, mtdSetup_0.chromInfo.prsScaleFactor);
			mtdSetup_0.chromInfo.prsUnitAfterScale = tbprsUnitAfterScale.Text;
			mtdSetup_0.chromInfo.prsUncalBase = (RespStyle)cbprsUncalBase.SelectedIndex;
			mtdSetup_0.chromInfo.prsUncalAmtRespF = Class49.String2Float(tbprsUncalAmtRespF.Text, mtdSetup_0.chromInfo.prsUncalAmtRespF);
			mtdSetup_0.chromInfo.addChrom = rbasAdd.Checked;
			mtdSetup_0.chromInfo.asMatching = (ASMatchStyle)cbasMatching.SelectedIndex;
			mtdSetup_0.chromInfo.ccColumnUT = Class49.String2Float(tbccUnretainedPeak.Text, mtdSetup_0.chromInfo.ccColumnUT);
			mtdSetup_0.chromInfo.ccColumnLength = Class49.String2Float(tbccColumnLength.Text, mtdSetup_0.chromInfo.ccColumnLength);
			if (rbccStatistical.Checked)
			{
				mtdSetup_0.chromInfo.ccStyle = ColumnCalcuStyle.Statistical;
			}
			else if (rbccFrom50per.Checked)
			{
				mtdSetup_0.chromInfo.ccStyle = ColumnCalcuStyle.From50per;
			}
			mtdSetup_0.chromInfo.ppoRestrictWaveLength = cbppoRestrictWaveLength.Checked;
			mtdSetup_0.chromInfo.ppoFrom = Class49.Object2Int(tbppoFrom.Text, mtdSetup_0.chromInfo.ppoFrom);
			mtdSetup_0.chromInfo.ppoTo = Class49.Object2Int(tbppoTo.Text, mtdSetup_0.chromInfo.ppoTo);
			mtdSetup_0.chromInfo.ppoPurityThreshold = Class49.Object2Int(tbppoPurityThreshold.Text, mtdSetup_0.chromInfo.ppoPurityThreshold);
			mtdSetup_0.chromInfo.ppoAbsorbanceThreshold = Class49.String2Float(tbppoAbsorbanceThreshold.Text, mtdSetup_0.chromInfo.ppoAbsorbanceThreshold);
			if (rbupAll.Checked)
			{
				mtdSetup_0.chromInfo.ppoUsedPoints = PPO_UsedPoints.All;
			}
			else if (rbupFive.Checked)
			{
				mtdSetup_0.chromInfo.ppoUsedPoints = PPO_UsedPoints.Five;
			}
			mtdSetup_0.chromInfo.ppoUseBackCorr = cbppoUseBackCorr.Checked;
			mtdSetup_0.chromInfo.lsoMatchCriteria = (LSO_MatchCriteria)cblsoMatchCriteria.SelectedIndex;
			mtdSetup_0.chromInfo.lsoMatchFactorThreshold = Class49.Object2Int(tblsoMatchFactorThreshold.Text, mtdSetup_0.chromInfo.lsoMatchFactorThreshold);
			mtdSetup_0.chromInfo.lsoMaxNumHits = Class49.Object2Int(tblsoMaxNumHits.Text, mtdSetup_0.chromInfo.lsoMaxNumHits);
			mtdSetup_0.chromInfo.lsoRestrictWaveLength = cblsoRestrictWaveLength.Checked;
			mtdSetup_0.chromInfo.lsoFrom = Class49.Object2Int(tblsoFrom.Text, mtdSetup_0.chromInfo.lsoFrom);
			mtdSetup_0.chromInfo.lsoTo = Class49.Object2Int(tblsoTo.Text, mtdSetup_0.chromInfo.lsoTo);
			mtdSetup_0.chromInfo.lsoRestrictRT = cblsoRestrictRT.Checked;
			mtdSetup_0.chromInfo.lsoRestrictRTV = Class49.String2Float(tblsoRestrictRT.Text, mtdSetup_0.chromInfo.lsoRestrictRTV);
			mtdSetup_0.chromInfo.lsoUseBackCorr = cblsoUseBackCorr.Checked;
			mtdSetup_0.chromInfo.lsoForAllDetectedPeaks = cblsoForAllDetectedPeaks.Checked;
			Array.Resize(ref mtdSetup_0.chromInfo.pdaRows, gvpdaLibs.RowCount);
			for (int num2 = 0; num2 < gvpdaLibs.RowCount; num2++)
			{
				mtdSetup_0.chromInfo.pdaRows[num2].used = (bool)gvpdaLibs.Rows[num2].Cells[0].Value;
				mtdSetup_0.chromInfo.pdaRows[num2].name = gvpdaLibs.Rows[num2].Cells[1].Value.ToString();
			}
			Array.Resize(ref mtdSetup_0.chromInfo.percents, gvgrPercent.RowCount - 1);
			for (int num3 = 0; num3 < mtdSetup_0.chromInfo.percents.Length; num3++)
			{
				mtdSetup_0.chromInfo.percents[num3].float_0 = Class49.String2Float(gvgrPercent.Rows[num3].Cells[0].Value, mtdSetup_0.chromInfo.percents[num3].float_0);
				mtdSetup_0.chromInfo.percents[num3].high = Class49.String2Float(gvgrPercent.Rows[num3].Cells[1].Value, mtdSetup_0.chromInfo.percents[num3].high);
			}
			Array.Resize(ref mtdSetup_0.chromInfo.gpc_RangeRow_0, gvgrMw.RowCount - 1);
			for (int num4 = 0; num4 < mtdSetup_0.chromInfo.gpc_RangeRow_0.Length; num4++)
			{
				mtdSetup_0.chromInfo.gpc_RangeRow_0[num4].high = Class49.String2Float(gvgrMw.Rows[num4].Cells[0].Value, mtdSetup_0.chromInfo.gpc_RangeRow_0[num4].high);
				mtdSetup_0.chromInfo.gpc_RangeRow_0[num4].float_0 = Class49.String2Float(gvgrMw.Rows[num4].Cells[1].Value, mtdSetup_0.chromInfo.gpc_RangeRow_0[num4].float_0);
			}
			break;
		}
		case AccStyle.Read:
		{
			tcMethod_SelectedIndexChanged(null, null);
			method_8(accStyle_0, mtdSetup_0.chromInfoR.GcProgTemp);
			gcProgTemp_0.LoadFromObject(mtdSetup_0.chromInfoR.GcProgTemp);
			refresh_dpgcProgTemp();
			method_11(AccStyle.Read, mtdSetup_0.chromInfoR.LcGradient);
			lcGradient_0.LoadFromObject(mtdSetup_0.chromInfoR.LcGradient);
			method_10();
			tbsbFlowRate.Text = mtdSetup_0.chromInfoR.LcGradient.sbFlowRate.ToString();
			tbsbTimeTo.Text = mtdSetup_0.chromInfoR.LcGradient.sbTimeTo.ToString();
			tbsbPersist.Text = mtdSetup_0.chromInfoR.LcGradient.sbPersist.ToString();
			switch (mtdSetup_0.chromInfoR.LcGradient.idleStateProc)
			{
			case IdleStateProc.PumpOff:
				rbisPumpOff.Checked = true;
				break;
			case IdleStateProc.Initial:
				rbisInitial.Checked = true;
				break;
			case IdleStateProc.MonitorSet:
				rbisMonitorSet.Checked = true;
				break;
			}
			ChromInfoR chromInfoR = mtdSetup_0.chromInfoR;
			cbUseProgWave.Checked = chromInfoR.UvUseProgWaves;
			gvProgWave.RowCount = chromInfoR.UvProgWaves.Length;
			for (int i = 0; i < chromInfoR.UvProgWaves.Length; i++)
			{
				gvProgWave.Rows[i].Cells[0].Value = chromInfoR.UvProgWaves[i].Time;
				gvProgWave.Rows[i].Cells[1].Value = chromInfoR.UvProgWaves[i].Wave;
			}
			cbWaveScan.Checked = chromInfoR.UvWaveScan;
			tbwsStartT.Text = chromInfoR.UvwsStartT.ToString();
			tbwsStepFreq.Text = chromInfoR.UvwsStepFreq.ToString();
			tbwsFrom.Text = chromInfoR.UvwsFrom.ToString();
			tbwsTo.Text = chromInfoR.UvwsTo.ToString();
			tbwsStep.Text = chromInfoR.UvwsStep.ToString();
			tbmsmMtdDspt.Text = mtdSetup_0.chromInfo.msmMtdDspt;
			tbmsmColumn.Text = mtdSetup_0.chromInfo.msmColumn;
			tbmsmMobilePhase.Text = mtdSetup_0.chromInfo.msmMobilePhase;
			tbmsmFlowRate.Text = mtdSetup_0.chromInfo.msmFlowRate;
			tbmsmPressure.Text = mtdSetup_0.chromInfo.msmPressure;
			tbmsmDetection.Text = mtdSetup_0.chromInfo.msmDetection;
			tbmsmTemperature.Text = mtdSetup_0.chromInfo.msmTemperature;
			tbmsmNote.Text = mtdSetup_0.chromInfo.msmNote;
			cbacqAutoStop.Checked = mtdSetup_0.chromInfoR.AcqAutoStop;
			tbacqRunTime.Text = mtdSetup_0.chromInfoR.AcqRunTime.ToString();
			cbecExternalControl.Checked = mtdSetup_0.chromInfoR.EcExternalControl;
			switch (mtdSetup_0.chromInfoR.ExtCtrlStart)
			{
			case ExtCtrlStart.StartOnly:
				rbecStartOnly.Checked = true;
				break;
			case ExtCtrlStart.StartRestart:
				rbecStartRestart.Checked = true;
				break;
			case ExtCtrlStart.StartStop:
				rbecStartStop.Checked = true;
				break;
			}
			switch (mtdSetup_0.chromInfoR.ExtCtrlSignal)
			{
			case ExtCtrlSignal.Up:
				rbecUp.Checked = true;
				break;
			case ExtCtrlSignal.Down:
				rbecDown.Checked = true;
				break;
			}
			tbcclCalibration.Text = mtdSetup_0.chromInfo.cclShowName;
			cbcclCalcu.SelectedIndex = (int)mtdSetup_0.chromInfo.cclCalcu;
			method_15();
			cbrtrHideISTDPeak.Checked = mtdSetup_0.chromInfo.rtrHideISTDPeak;
			switch (mtdSetup_0.chromInfo.rtrRltReportPeaks)
			{
			case RltReportPeaks.AllDetectedPeaks:
				rbrtrAllDetectedPeaks.Checked = true;
				break;
			case RltReportPeaks.IdentifiedPeaks:
				rbrtrIdentifiedPeaks.Checked = true;
				break;
			case RltReportPeaks.CaliPeaks:
				rbrtrCaliPeaks.Checked = true;
				break;
			}
			cbprsUseScaleFactor.Checked = mtdSetup_0.chromInfo.prsUseScaleFactor;
			tbprsScaleFactor.Text = mtdSetup_0.chromInfo.prsScaleFactor.ToString();
			tbprsUnitAfterScale.Text = mtdSetup_0.chromInfo.prsUnitAfterScale;
			cbprsUncalBase.SelectedIndex = (int)mtdSetup_0.chromInfo.prsUncalBase;
			tbprsUncalAmtRespF.Text = mtdSetup_0.chromInfo.prsUncalAmtRespF.ToString();
			tbasChrom.Text = mtdSetup_0.chromInfo.asShowName;
			rbasAdd.Checked = mtdSetup_0.chromInfo.addChrom;
			rbasSub.Checked = !mtdSetup_0.chromInfo.addChrom;
			cbasMatching.SelectedIndex = (int)mtdSetup_0.chromInfo.asMatching;
			tbccUnretainedPeak.Text = mtdSetup_0.chromInfo.ccColumnUT.ToString();
			tbccColumnLength.Text = mtdSetup_0.chromInfo.ccColumnLength.ToString();
			switch (mtdSetup_0.chromInfo.ccStyle)
			{
			case ColumnCalcuStyle.Statistical:
				rbccStatistical.Checked = true;
				break;
			case ColumnCalcuStyle.From50per:
				rbccFrom50per.Checked = true;
				break;
			}
			cbppoRestrictWaveLength.Checked = mtdSetup_0.chromInfo.ppoRestrictWaveLength;
			tbppoFrom.Text = mtdSetup_0.chromInfo.ppoFrom.ToString();
			tbppoTo.Text = mtdSetup_0.chromInfo.ppoTo.ToString();
			tbppoPurityThreshold.Text = mtdSetup_0.chromInfo.ppoPurityThreshold.ToString();
			tbppoAbsorbanceThreshold.Text = mtdSetup_0.chromInfo.ppoAbsorbanceThreshold.ToString();
			switch (mtdSetup_0.chromInfo.ppoUsedPoints)
			{
			case PPO_UsedPoints.All:
				rbupAll.Checked = true;
				break;
			case PPO_UsedPoints.Five:
				rbupFive.Checked = true;
				break;
			}
			cbppoUseBackCorr.Checked = mtdSetup_0.chromInfo.ppoUseBackCorr;
			cblsoMatchCriteria.SelectedIndex = (int)mtdSetup_0.chromInfo.lsoMatchCriteria;
			tblsoMatchFactorThreshold.Text = mtdSetup_0.chromInfo.lsoMatchFactorThreshold.ToString();
			tblsoMaxNumHits.Text = mtdSetup_0.chromInfo.lsoMaxNumHits.ToString();
			cblsoRestrictWaveLength.Checked = mtdSetup_0.chromInfo.lsoRestrictWaveLength;
			tblsoFrom.Text = mtdSetup_0.chromInfo.lsoFrom.ToString();
			tblsoTo.Text = mtdSetup_0.chromInfo.lsoTo.ToString();
			cblsoRestrictRT.Checked = mtdSetup_0.chromInfo.lsoRestrictRT;
			tblsoRestrictRT.Text = mtdSetup_0.chromInfo.lsoRestrictRTV.ToString();
			cblsoUseBackCorr.Checked = mtdSetup_0.chromInfo.lsoUseBackCorr;
			cblsoForAllDetectedPeaks.Checked = mtdSetup_0.chromInfo.lsoForAllDetectedPeaks;
			gvpdaLibs.RowCount = mtdSetup_0.chromInfo.pdaRows.Length;
			for (int j = 0; j < gvpdaLibs.RowCount; j++)
			{
				gvpdaLibs.Rows[j].Cells[0].Value = mtdSetup_0.chromInfo.pdaRows[j].used;
				gvpdaLibs.Rows[j].Cells[1].Value = mtdSetup_0.chromInfo.pdaRows[j].name;
			}
			gvgrPercent.RowCount = mtdSetup_0.chromInfo.percents.Length + 1;
			for (int k = 0; k < mtdSetup_0.chromInfo.percents.Length; k++)
			{
				gvgrPercent.Rows[k].Cells[0].Value = mtdSetup_0.chromInfo.percents[k].float_0;
				gvgrPercent.Rows[k].Cells[1].Value = mtdSetup_0.chromInfo.percents[k].high;
			}
			gvgrMw.RowCount = mtdSetup_0.chromInfo.gpc_RangeRow_0.Length + 1;
			for (int l = 0; l < mtdSetup_0.chromInfo.gpc_RangeRow_0.Length; l++)
			{
				gvgrMw.Rows[l].Cells[0].Value = mtdSetup_0.chromInfo.gpc_RangeRow_0[l].high;
				gvgrMw.Rows[l].Cells[1].Value = mtdSetup_0.chromInfo.gpc_RangeRow_0[l].float_0;
			}
			break;
		}
		}
	}

	public void refresh_dpgcProgTemp()
	{
		if (gradientDisplay_1 != null)
		{
			gradientDisplay_1.PrepareInfo(gcProgTemp_0);
		}
		dpgcProgTemp.Refresh();
	}

	private void method_10()
	{
		if (gradientDisplay_0 != null)
		{
			gradientDisplay_0.PrepareInfo(lcGradient_0);
		}
		dplcGradient.Refresh();
	}

	private void method_11(AccStyle accStyle_0, LcGradient lcGradient_1)
	{
		GrdtOpt gradientOption = lcGradient_1.gradientOption;
		GradientRow[] array = lcGradient_1.gradientRows;
		gvlcGradient.EndEdit();
		switch (accStyle_0)
		{
		case AccStyle.Read:
			cblcUse.Checked = lcGradient_1.lcUse;
			cblgoptSolvent1.Checked = gradientOption.hasSolvent1;
			cblgoptSolvent2.Checked = gradientOption.hasSolvent2;
			cblgoptSolvent3.Checked = gradientOption.hasSolvent3;
			cblgoptSolvent4.Checked = gradientOption.hasSolvent4;
			tblgoptSolvent1.Text = gradientOption.solvent1Name;
			tblgoptSolvent2.Text = gradientOption.solvent2Name;
			tblgoptSolvent3.Text = gradientOption.solvent3Name;
			tblgoptSolvent4.Text = gradientOption.solvent4Name;
			ReadLcGradient(gvlcGradient, lcGradient_1);
			break;
		case AccStyle.Write:
		{
			lcGradient_1.lcUse = cblcUse.Checked;
			gradientOption.hasSolvent1 = cblgoptSolvent1.Checked;
			gradientOption.hasSolvent2 = cblgoptSolvent2.Checked;
			gradientOption.hasSolvent3 = cblgoptSolvent3.Checked;
			gradientOption.hasSolvent4 = cblgoptSolvent4.Checked;
			gradientOption.solvent1Name = tblgoptSolvent1.Text;
			gradientOption.solvent2Name = tblgoptSolvent2.Text;
			gradientOption.solvent3Name = tblgoptSolvent3.Text;
			gradientOption.solvent4Name = tblgoptSolvent4.Text;
			Array.Resize(ref array, gvlcGradient.RowCount);
			for (int i = 0; i < gvlcGradient.RowCount; i++)
			{
				if (i != 0)
				{
					array[i].time = Class49.String2Float(gvlcGradient.Rows[i].Cells["Time"].Value, array[i].time);
				}
				if (gvlcGradient.Columns["A"].Visible)
				{
					array[i].float_0 = Class49.String2Float(gvlcGradient.Rows[i].Cells["A"].Value, array[i].float_0 * 100f) / 100f;
				}
				if (gvlcGradient.Columns["B"].Visible)
				{
					array[i].float_1 = Class49.String2Float(gvlcGradient.Rows[i].Cells["B"].Value, array[i].float_1 * 100f) / 100f;
				}
				if (gvlcGradient.Columns["C"].Visible)
				{
					array[i].float_2 = Class49.String2Float(gvlcGradient.Rows[i].Cells["C"].Value, array[i].float_2 * 100f) / 100f;
				}
				if (gvlcGradient.Columns["D"].Visible)
				{
					array[i].float_3 = Class49.String2Float(gvlcGradient.Rows[i].Cells["D"].Value, array[i].float_3 * 100f) / 100f;
				}
				array[i].flow = Class49.String2Float(gvlcGradient.Rows[i].Cells["Flow"].Value, array[i].flow);
			}
			lcGradient_1.gradientRows = array;
			break;
		}
		}
	}

	private void method_12(int int_0, AccStyle accStyle_0)
	{
		if (int_0 >= 0 && accStyle_0 != AccStyle.Clear)
		{
			gvInteg.Refresh(accStyle_0, mtdSetup_0.sigIntegrations[int_0]);
		}
		else
		{
			gvInteg.Refresh(AccStyle.Clear, null);
		}
	}

	public void refresh_once()
	{
		Text = Lang.PS("方法设置", "Method Setup") + "[" + instrument.name + "]";
		tcMethod.TabPages.Clear();
		switch (instrument.instruStyle)
		{
		case InstruStyle.GC:
			tcMethod.TabPages.Add(tpGC);
			tcMethod.TabPages.Add(tpTempProg);
			tcMethod.TabPages.Add(tpAcquisition);
			tcMethod.TabPages.Add(tpMeasurement);
			tcMethod.TabPages.Add(tpIntegration);
			tcMethod.TabPages.Add(tpCaculation);
			tcMethod.TabPages.Add(tpAdvanced);
			break;
		case InstruStyle.LC:
			tcMethod.TabPages.Add(tpGradient);
			tcMethod.TabPages.Add(tpAcquisition);
			tcMethod.TabPages.Add(tpUV);
			tcMethod.TabPages.Add(tpMeasurement);
			tcMethod.TabPages.Add(tpIntegration);
			tcMethod.TabPages.Add(tpCaculation);
			tcMethod.TabPages.Add(tpAdvanced);
			break;
		}
		lblcPumpNum.Text = Lang.PS("泵数:", "Pumps:") + instrument.lcc_Pumps.Length;
		tcMethod_SelectedIndexChanged(null, null);
	}

	public static void RefreshHeaders_gvGradient(LclGridView gvLcGradient)
	{
		gvLcGradient.Rows[0].Cells[0].Value = Lang.PS("初始", "Initial");
		gvLcGradient.Columns["Time"].HeaderText = Lang.PS("时间\n[min]", "Time\n[min]");
		gvLcGradient.Columns["Flow"].HeaderText = Lang.PS("流速\n[mL/min]", "Flow\n[mL/min]");
	}

	private void method_14(MtdDlgInitStyle mtdDlgInitStyle_0)
	{
		if (mtdDlgInitStyle_0 == MtdDlgInitStyle.Control)
		{
			if (instrument.instruStyle == InstruStyle.LC)
			{
				tcMethod.SelectedTab = tpGradient;
			}
			else if (instrument.instruStyle == InstruStyle.GC)
			{
				tcMethod.SelectedTab = tpTempProg;
			}
		}
		if (mtdDlgInitStyle_0 == MtdDlgInitStyle.Acquisition)
		{
			tcMethod.SelectedTab = tpAcquisition;
		}
		if (mtdDlgInitStyle_0 == MtdDlgInitStyle.Measurment)
		{
			tcMethod.SelectedTab = tpMeasurement;
		}
		if (mtdDlgInitStyle_0 == MtdDlgInitStyle.Integration)
		{
			tcMethod.SelectedTab = tpIntegration;
		}
		if (mtdDlgInitStyle_0 == MtdDlgInitStyle.Calculation)
		{
			tcMethod.SelectedTab = tpCaculation;
		}
	}

	private void method_15()
	{
		lbcclAuthorV.Text = mtdSetup_0.chromInfo.cclAuthor;
		lbcclDescriptionV.Text = mtdSetup_0.chromInfo.cclDescription;
		lbcclCreateTimeV.Text = mtdSetup_0.chromInfo.cclCreateTime.ToShortDateString();
		lbcclModifiedTimeV.Text = mtdSetup_0.chromInfo.cclModifiedTime.ToShortDateString();
	}

	public DialogResult ShowDialog(MtdSetup methodSetup, MtdDlgInitStyle mtdDlgInitStyle)
	{
		mtdSetup_1 = methodSetup;
		btnmtdApply.Visible = true;
		method_14(mtdDlgInitStyle);
		DialogResult dialogResult = ShowDialog();
		if (dialogResult == DialogResult.OK)
		{
			btnmtdApply_Click(null, null);
		}
		return dialogResult;
	}

	private void tbptIniTempHoldT_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode == Keys.Return)
		{
			dgvPT_CellEndEdit(null, null);
		}
	}

	private void tcMethod_Deselecting(object sender, TabControlCancelEventArgs e)
	{
		method_1(cbASsSelect.SelectedIndex);
		method_2(cbDtsSelect.SelectedIndex);
	}

	private void tcMethod_SelectedIndexChanged(object sender, EventArgs e)
	{
		cbASsSelect.Visible = false;
		cbDtsSelect.Visible = false;
		if (tcMethod.SelectedTab == tpAS)
		{
			cbASsSelect.Visible = true;
			lbExpress.Text = Lang.PS("选择AS", "Select AS");
			cbASsSelect_SelectedIndexChanged(null, null);
		}
		else if (tcMethod.SelectedTab == tpGC)
		{
			lbExpress.Text = Lang.PS("选择GC", "Select GC");
		}
		else if (tcMethod.SelectedTab == tpLC)
		{
			lbExpress.Text = Lang.PS("选择LC", "Select LC");
		}
		else if (tcMethod.SelectedTab == tpGradient)
		{
			lbExpress.Text = "";
		}
		else if (tcMethod.SelectedTab == tpMeasurement)
		{
			lbExpress.Text = Lang.PS("所有检测器", "Common for all detectors");
		}
		else if (tcMethod.SelectedTab == tpAcquisition)
		{
			cbDtsSelect.Visible = true;
			lbExpress.Text = Lang.PS("选择检测器", "Select Detector");
			cbDtsSelect_SelectedIndexChanged(null, null);
		}
		else if (tcMethod.SelectedTab == tpUV)
		{
			lbExpress.Text = "紫外灯设置";
		}
		else if (tcMethod.SelectedTab == tpIntegration)
		{
			cbDtsSelect.Visible = true;
			lbExpress.Text = Lang.PS("选择检测器", "Select Detector");
			cbDtsSelect_SelectedIndexChanged(null, null);
		}
		else if (tcMethod.SelectedTab == tpCaculation)
		{
			lbExpress.Text = Lang.PS("所有检测器", "Common for all detectors");
		}
		else if (tcMethod.SelectedTab == tpAdvanced)
		{
			lbExpress.Text = Lang.PS("所有检测器", "Common for all detectors");
		}
		else if (tcMethod.SelectedTab == tpPDA)
		{
			lbExpress.Text = Lang.PS("所有检测器", "Common for all detectors");
		}
		else if (tcMethod.SelectedTab == tpRangesGPC)
		{
			lbExpress.Text = Lang.PS("所有检测器", "Common for all detectors");
		}
	}

	private void method_16(object sender, EventArgs e)
	{
	}

	private void tpRangesGPC_Click(object sender, EventArgs e)
	{
	}

	public MtdSetupDlg(Instrument instrument)
	{
		InitializeComponent();
		base.instrument = instrument;
	}

	public MtdSetupDlg()
	{
		InitializeComponent();
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
		this.tcMethod = new IBrainChrom2018.LclTabControl();
		this.tpAS = new System.Windows.Forms.TabPage();
		this.tpGC = new System.Windows.Forms.TabPage();
		this.btnExtEvTPQry = new System.Windows.Forms.Button();
		this.btnExtEvTPSet = new System.Windows.Forms.Button();
		this.gvExtEvTP = new System.Windows.Forms.DataGridView();
		this.clmExtEvTP0 = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.clmExtEvTP1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.clmExtEvTP2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.clmExtEvTP3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.tpTempProg = new System.Windows.Forms.TabPage();
		this.btnPTQry = new System.Windows.Forms.Button();
		this.btnPTSet = new System.Windows.Forms.Button();
		this.lbptInitT = new System.Windows.Forms.Label();
		this.label2 = new System.Windows.Forms.Label();
		this.label27 = new System.Windows.Forms.Label();
		this.tbptIniTempHoldT = new System.Windows.Forms.TextBox();
		this.dpgcProgTemp = new IBrainChrom2018.LclDisplayPanel();
		this.dgvPT = new IBrainChrom2018.LclGridView();
		this.tpLC = new System.Windows.Forms.TabPage();
		this.tpGradient = new System.Windows.Forms.TabPage();
		this.cblcUse = new IBrainChrom2018.LclCheckBox();
		this.gblcOption = new IBrainChrom2018.LclGroupBox();
		this.tblgoptSolvent4 = new IBrainChrom2018.LclTextBox();
		this.tblgoptSolvent3 = new IBrainChrom2018.LclTextBox();
		this.tblgoptSolvent2 = new IBrainChrom2018.LclTextBox();
		this.tblgoptSolvent1 = new IBrainChrom2018.LclTextBox();
		this.cblgoptSolvent4 = new IBrainChrom2018.LclCheckBox();
		this.cblgoptSolvent3 = new IBrainChrom2018.LclCheckBox();
		this.cblgoptSolvent2 = new IBrainChrom2018.LclCheckBox();
		this.lblcPumpNum = new IBrainChrom2018.LclLabel();
		this.cblgoptSolvent1 = new IBrainChrom2018.LclCheckBox();
		this.gblcIdleState = new IBrainChrom2018.LclGroupBox();
		this.rbisMonitorSet = new IBrainChrom2018.LclRadioButton();
		this.rbisInitial = new IBrainChrom2018.LclRadioButton();
		this.rbisPumpOff = new IBrainChrom2018.LclRadioButton();
		this.gblcStandBy = new IBrainChrom2018.LclGroupBox();
		this.tbsbPersist = new IBrainChrom2018.LclTextBox();
		this.tbsbTimeTo = new IBrainChrom2018.LclTextBox();
		this.tbsbFlowRate = new IBrainChrom2018.LclTextBox();
		this.lclLabel7 = new IBrainChrom2018.LclLabel();
		this.lbsbPersist = new IBrainChrom2018.LclLabel();
		this.lclLabel6 = new IBrainChrom2018.LclLabel();
		this.lclLabel5 = new IBrainChrom2018.LclLabel();
		this.lbsbTimeTo = new IBrainChrom2018.LclLabel();
		this.lbsbFlowRate = new IBrainChrom2018.LclLabel();
		this.dplcGradient = new IBrainChrom2018.LclDisplayPanel();
		this.gvlcGradient = new IBrainChrom2018.LclGridView();
		this.cmsLcGradient = new System.Windows.Forms.ContextMenuStrip(this.components);
		this.milgAddRow = new System.Windows.Forms.ToolStripMenuItem();
		this.milgDeleteRow = new System.Windows.Forms.ToolStripMenuItem();
		this.tpUV = new System.Windows.Forms.TabPage();
		this.gvProgWave = new IBrainChrom2018.LclGridView();
		this.colTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.colWave = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.cbWaveScan = new System.Windows.Forms.CheckBox();
		this.cbUseProgWave = new System.Windows.Forms.CheckBox();
		this.tbwsStep = new System.Windows.Forms.TextBox();
		this.tbwsTo = new System.Windows.Forms.TextBox();
		this.tbwsFrom = new System.Windows.Forms.TextBox();
		this.tbwsStepFreq = new System.Windows.Forms.TextBox();
		this.tbwsStartT = new System.Windows.Forms.TextBox();
		this.label17 = new System.Windows.Forms.Label();
		this.label16 = new System.Windows.Forms.Label();
		this.label1 = new System.Windows.Forms.Label();
		this.label15 = new System.Windows.Forms.Label();
		this.label14 = new System.Windows.Forms.Label();
		this.tpMeasurement = new System.Windows.Forms.TabPage();
		this.gbmsmExternalControl = new IBrainChrom2018.LclGroupBox();
		this.pbecDown = new IBrainChrom2018.LclPictureBox();
		this.pbecUp = new IBrainChrom2018.LclPictureBox();
		this.rbecDown = new IBrainChrom2018.LclRadioButton();
		this.rbecUp = new IBrainChrom2018.LclRadioButton();
		this.pnlecES = new IBrainChrom2018.LclPanel();
		this.rbecStartStop = new IBrainChrom2018.LclRadioButton();
		this.rbecStartRestart = new IBrainChrom2018.LclRadioButton();
		this.rbecStartOnly = new IBrainChrom2018.LclRadioButton();
		this.cbecExternalControl = new IBrainChrom2018.LclCheckBox();
		this.gbmsmAcquisition = new IBrainChrom2018.LclGroupBox();
		this.cbacqAutoStop = new IBrainChrom2018.LclCheckBox();
		this.lclLabel17 = new IBrainChrom2018.LclLabel();
		this.lbacqRunTime = new IBrainChrom2018.LclLabel();
		this.tbacqRunTime = new IBrainChrom2018.LclTextBox();
		this.tbmsmNote = new IBrainChrom2018.LclTextBox();
		this.tbmsmTemperature = new IBrainChrom2018.LclTextBox();
		this.tbmsmDetection = new IBrainChrom2018.LclTextBox();
		this.tbmsmPressure = new IBrainChrom2018.LclTextBox();
		this.tbmsmFlowRate = new IBrainChrom2018.LclTextBox();
		this.tbmsmMobilePhase = new IBrainChrom2018.LclTextBox();
		this.tbmsmColumn = new IBrainChrom2018.LclTextBox();
		this.tbmsmMtdDspt = new IBrainChrom2018.LclTextBox();
		this.lbmsmNote = new IBrainChrom2018.LclLabel();
		this.lbmsmTemperature = new IBrainChrom2018.LclLabel();
		this.lbmsmDetection = new IBrainChrom2018.LclLabel();
		this.lbmsmPressure = new IBrainChrom2018.LclLabel();
		this.lbmsmFlowRate = new IBrainChrom2018.LclLabel();
		this.lbmsmMobilePhase = new IBrainChrom2018.LclLabel();
		this.lbmsmColumn = new IBrainChrom2018.LclLabel();
		this.lbmsmMtdDspt = new IBrainChrom2018.LclLabel();
		this.tpAcquisition = new System.Windows.Forms.TabPage();
		this.lbacqRate = new IBrainChrom2018.LclLabel();
		this.lbacqRange = new IBrainChrom2018.LclLabel();
		this.lbacqDetector = new IBrainChrom2018.LclLabel();
		this.cbacqRate = new IBrainChrom2018.LclComboBox();
		this.cbacqRange = new IBrainChrom2018.LclComboBox();
		this.tpIntegration = new System.Windows.Forms.TabPage();
		this.gvInteg = new IBrainChrom2018.LclIntegGridView();
		this.cmsIntegration = new System.Windows.Forms.ContextMenuStrip(this.components);
		this.miIntegAppendRow = new System.Windows.Forms.ToolStripMenuItem();
		this.miIntegInsertRow = new System.Windows.Forms.ToolStripMenuItem();
		this.miIntegDeleteRows = new System.Windows.Forms.ToolStripMenuItem();
		this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
		this.miIntegRowsUp = new System.Windows.Forms.ToolStripMenuItem();
		this.miIntegRowsDown = new System.Windows.Forms.ToolStripMenuItem();
		this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
		this.miIntegResetRows = new System.Windows.Forms.ToolStripMenuItem();
		this.tpCaculation = new System.Windows.Forms.TabPage();
		this.cbcclCalcu = new IBrainChrom2018.LclCusComboBox();
		this.gbcclRltTableReport = new IBrainChrom2018.LclGroupBox();
		this.rbrtrCaliPeaks = new IBrainChrom2018.LclRadioButton();
		this.rbrtrIdentifiedPeaks = new IBrainChrom2018.LclRadioButton();
		this.rbrtrAllDetectedPeaks = new IBrainChrom2018.LclRadioButton();
		this.cbrtrHideISTDPeak = new IBrainChrom2018.LclCheckBox();
		this.gbcclParas = new IBrainChrom2018.LclGroupBox();
		this.cbprsUncalBase = new IBrainChrom2018.LclCusComboBox();
		this.cbprsUseScaleFactor = new IBrainChrom2018.LclCheckBox();
		this.lbprsUncalBase = new IBrainChrom2018.LclLabel();
		this.lbprsUncalAmtRespFU = new IBrainChrom2018.LclLabel();
		this.lbprsUncalAmtRespF = new IBrainChrom2018.LclLabel();
		this.lbprsUnitAfterScale = new IBrainChrom2018.LclLabel();
		this.lbprsScaleFactor = new IBrainChrom2018.LclLabel();
		this.tbprsUncalAmtRespF = new IBrainChrom2018.LclTextBox();
		this.tbprsUnitAfterScale = new IBrainChrom2018.LclTextBox();
		this.tbprsScaleFactor = new IBrainChrom2018.LclTextBox();
		this.btncclSet = new IBrainChrom2018.LclButton();
		this.btncclNone = new IBrainChrom2018.LclButton();
		this.btncclView = new IBrainChrom2018.LclButton();
		this.tbcclCalibration = new IBrainChrom2018.LclTextBox();
		this.lbcclModifiedTimeV = new IBrainChrom2018.LclLabel();
		this.lbcclModifiedTime = new IBrainChrom2018.LclLabel();
		this.lbcclCreateTimeV = new IBrainChrom2018.LclLabel();
		this.lbcclCreateTime = new IBrainChrom2018.LclLabel();
		this.lbcclDescriptionV = new IBrainChrom2018.LclLabel();
		this.lbcclDescription = new IBrainChrom2018.LclLabel();
		this.lbcclAuthorV = new IBrainChrom2018.LclLabel();
		this.lbcclAuthor = new IBrainChrom2018.LclLabel();
		this.lbcclCalcu = new IBrainChrom2018.LclLabel();
		this.lbcclCalibration = new IBrainChrom2018.LclLabel();
		this.tpAdvanced = new System.Windows.Forms.TabPage();
		this.gbadvColumnCalcu = new IBrainChrom2018.LclGroupBox();
		this.rbccFrom50per = new IBrainChrom2018.LclRadioButton();
		this.rbccStatistical = new IBrainChrom2018.LclRadioButton();
		this.tbccColumnLength = new IBrainChrom2018.LclTextBox();
		this.lbccColumnLengthU = new IBrainChrom2018.LclLabel();
		this.tbccUnretainedPeak = new IBrainChrom2018.LclTextBox();
		this.lbccColumnLength = new IBrainChrom2018.LclLabel();
		this.lbccUnretainedPeakU = new IBrainChrom2018.LclLabel();
		this.lbccUnretainedPeak = new IBrainChrom2018.LclLabel();
		this.gbadvAddSub = new IBrainChrom2018.LclGroupBox();
		this.rbasSub = new IBrainChrom2018.LclRadioButton();
		this.rbasAdd = new IBrainChrom2018.LclRadioButton();
		this.cbasMatching = new IBrainChrom2018.LclCusComboBox();
		this.btnasSetChrom = new IBrainChrom2018.LclButton();
		this.btnasNoneChrom = new IBrainChrom2018.LclButton();
		this.tbasChrom = new IBrainChrom2018.LclTextBox();
		this.lbasMatching = new IBrainChrom2018.LclLabel();
		this.lbasChrom = new IBrainChrom2018.LclLabel();
		this.tpPDA = new System.Windows.Forms.TabPage();
		this.gvpdaLibs = new IBrainChrom2018.LclGridView();
		this.cmsLibs = new System.Windows.Forms.ContextMenuStrip(this.components);
		this.miAddRow = new System.Windows.Forms.ToolStripMenuItem();
		this.miDeleteRow = new System.Windows.Forms.ToolStripMenuItem();
		this.gbpdaLibSearchOptions = new IBrainChrom2018.LclGroupBox();
		this.cblsoMatchCriteria = new IBrainChrom2018.LclCusComboBox();
		this.tblsoRestrictRT = new IBrainChrom2018.LclTextBox();
		this.tblsoMaxNumHits = new IBrainChrom2018.LclTextBox();
		this.tblsoMatchFactorThreshold = new IBrainChrom2018.LclTextBox();
		this.tblsoFrom = new IBrainChrom2018.LclTextBox();
		this.lclLabel42 = new IBrainChrom2018.LclLabel();
		this.tblsoTo = new IBrainChrom2018.LclTextBox();
		this.lclLabel43 = new IBrainChrom2018.LclLabel();
		this.lclLabel44 = new IBrainChrom2018.LclLabel();
		this.lblsoMaxNumHits = new IBrainChrom2018.LclLabel();
		this.lblsoTo = new IBrainChrom2018.LclLabel();
		this.lblsoMatchCriteria = new IBrainChrom2018.LclLabel();
		this.lblsoMatchFactorThreshold = new IBrainChrom2018.LclLabel();
		this.lblsoFrom = new IBrainChrom2018.LclLabel();
		this.cblsoForAllDetectedPeaks = new IBrainChrom2018.LclCheckBox();
		this.cblsoUseBackCorr = new IBrainChrom2018.LclCheckBox();
		this.cblsoRestrictRT = new IBrainChrom2018.LclCheckBox();
		this.cblsoRestrictWaveLength = new IBrainChrom2018.LclCheckBox();
		this.gbpdaPeakPurityOptions = new IBrainChrom2018.LclGroupBox();
		this.gbppoUsedPoints = new IBrainChrom2018.LclGroupBox();
		this.rbupFive = new IBrainChrom2018.LclRadioButton();
		this.rbupAll = new IBrainChrom2018.LclRadioButton();
		this.tbppoAbsorbanceThreshold = new IBrainChrom2018.LclTextBox();
		this.tbppoPurityThreshold = new IBrainChrom2018.LclTextBox();
		this.tbppoFrom = new IBrainChrom2018.LclTextBox();
		this.tbppoTo = new IBrainChrom2018.LclTextBox();
		this.lclLabel45 = new IBrainChrom2018.LclLabel();
		this.lclLabel46 = new IBrainChrom2018.LclLabel();
		this.lclLabel47 = new IBrainChrom2018.LclLabel();
		this.lbppoAbsorbanceThreshold = new IBrainChrom2018.LclLabel();
		this.lbppoTo = new IBrainChrom2018.LclLabel();
		this.lbppoPurityThreshold = new IBrainChrom2018.LclLabel();
		this.lbppoFrom = new IBrainChrom2018.LclLabel();
		this.cbppoUseBackCorr = new IBrainChrom2018.LclCheckBox();
		this.cbppoRestrictWaveLength = new IBrainChrom2018.LclCheckBox();
		this.tpRangesGPC = new System.Windows.Forms.TabPage();
		this.gvgrMw = new IBrainChrom2018.LclGridView();
		this.gvgrPercent = new IBrainChrom2018.LclGridView();
		this.lbgrMw = new IBrainChrom2018.LclLabel();
		this.lbgrPercent = new IBrainChrom2018.LclLabel();
		this.lbExpress = new IBrainChrom2018.LclLabel();
		this.cbDtsSelect = new IBrainChrom2018.LclComboBox();
		this.openFileDialog_1 = new System.Windows.Forms.OpenFileDialog();
		this.btnmtdApply = new IBrainChrom2018.LclButton();
		this.cbASsSelect = new IBrainChrom2018.LclComboBox();
		this.tcMethod.SuspendLayout();
		this.tpGC.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.gvExtEvTP).BeginInit();
		this.tpTempProg.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.dgvPT).BeginInit();
		this.tpGradient.SuspendLayout();
		this.gblcOption.SuspendLayout();
		this.gblcIdleState.SuspendLayout();
		this.gblcStandBy.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.gvlcGradient).BeginInit();
		this.cmsLcGradient.SuspendLayout();
		this.tpUV.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.gvProgWave).BeginInit();
		this.tpMeasurement.SuspendLayout();
		this.gbmsmExternalControl.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.pbecDown).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.pbecUp).BeginInit();
		this.pnlecES.SuspendLayout();
		this.gbmsmAcquisition.SuspendLayout();
		this.tpAcquisition.SuspendLayout();
		this.tpIntegration.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.gvInteg).BeginInit();
		this.cmsIntegration.SuspendLayout();
		this.tpCaculation.SuspendLayout();
		this.gbcclRltTableReport.SuspendLayout();
		this.gbcclParas.SuspendLayout();
		this.tpAdvanced.SuspendLayout();
		this.gbadvColumnCalcu.SuspendLayout();
		this.gbadvAddSub.SuspendLayout();
		this.tpPDA.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.gvpdaLibs).BeginInit();
		this.cmsLibs.SuspendLayout();
		this.gbpdaLibSearchOptions.SuspendLayout();
		this.gbpdaPeakPurityOptions.SuspendLayout();
		this.gbppoUsedPoints.SuspendLayout();
		this.tpRangesGPC.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.gvgrMw).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.gvgrPercent).BeginInit();
		base.SuspendLayout();
		base.btnCancel.Location = new System.Drawing.Point(230, 393);
		base.btnCancel.Text = "取消";
		base.btnHelp.Location = new System.Drawing.Point(452, 393);
		base.btnHelp.Text = "帮助";
		base.btnHelp.Click += new System.EventHandler(method_0);
		base.btnOK.Location = new System.Drawing.Point(149, 393);
		base.btnOK.Text = "确认";
		base.btnOK.Click += new System.EventHandler(method_16);
		this.tcMethod.Alignment = System.Windows.Forms.TabAlignment.Bottom;
		this.tcMethod.Controls.Add(this.tpAS);
		this.tcMethod.Controls.Add(this.tpGC);
		this.tcMethod.Controls.Add(this.tpTempProg);
		this.tcMethod.Controls.Add(this.tpLC);
		this.tcMethod.Controls.Add(this.tpGradient);
		this.tcMethod.Controls.Add(this.tpUV);
		this.tcMethod.Controls.Add(this.tpMeasurement);
		this.tcMethod.Controls.Add(this.tpAcquisition);
		this.tcMethod.Controls.Add(this.tpIntegration);
		this.tcMethod.Controls.Add(this.tpCaculation);
		this.tcMethod.Controls.Add(this.tpAdvanced);
		this.tcMethod.Controls.Add(this.tpPDA);
		this.tcMethod.Controls.Add(this.tpRangesGPC);
		this.tcMethod.ItemSize = new System.Drawing.Size(90, 19);
		this.tcMethod.Location = new System.Drawing.Point(3, 29);
		this.tcMethod.Name = "tcMethod";
		this.tcMethod.SelectedIndex = 0;
		this.tcMethod.Size = new System.Drawing.Size(538, 344);
		this.tcMethod.TabIndex = 5;
		this.tcMethod.SelectedIndexChanged += new System.EventHandler(tcMethod_SelectedIndexChanged);
		this.tcMethod.Deselecting += new System.Windows.Forms.TabControlCancelEventHandler(tcMethod_Deselecting);
		this.tpAS.Location = new System.Drawing.Point(4, 4);
		this.tpAS.Name = "tpAS";
		this.tpAS.Size = new System.Drawing.Size(530, 249);
		this.tpAS.TabIndex = 0;
		this.tpAS.Text = "自动进样器";
		this.tpAS.UseVisualStyleBackColor = true;
		this.tpGC.Controls.Add(this.btnExtEvTPQry);
		this.tpGC.Controls.Add(this.btnExtEvTPSet);
		this.tpGC.Controls.Add(this.gvExtEvTP);
		this.tpGC.Location = new System.Drawing.Point(4, 4);
		this.tpGC.Name = "tpGC";
		this.tpGC.Size = new System.Drawing.Size(530, 249);
		this.tpGC.TabIndex = 1;
		this.tpGC.Text = "气相";
		this.tpGC.UseVisualStyleBackColor = true;
		this.btnExtEvTPQry.Location = new System.Drawing.Point(314, 209);
		this.btnExtEvTPQry.Name = "btnExtEvTPQry";
		this.btnExtEvTPQry.Size = new System.Drawing.Size(65, 23);
		this.btnExtEvTPQry.TabIndex = 6;
		this.btnExtEvTPQry.Text = "查询硬件";
		this.btnExtEvTPQry.UseVisualStyleBackColor = true;
		this.btnExtEvTPQry.Click += new System.EventHandler(btnPTSet_Click);
		this.btnExtEvTPSet.Location = new System.Drawing.Point(385, 209);
		this.btnExtEvTPSet.Name = "btnExtEvTPSet";
		this.btnExtEvTPSet.Size = new System.Drawing.Size(65, 23);
		this.btnExtEvTPSet.TabIndex = 5;
		this.btnExtEvTPSet.Text = "写入硬件";
		this.btnExtEvTPSet.UseVisualStyleBackColor = true;
		this.btnExtEvTPSet.Click += new System.EventHandler(btnPTSet_Click);
		this.gvExtEvTP.AllowUserToAddRows = false;
		this.gvExtEvTP.AllowUserToDeleteRows = false;
		this.gvExtEvTP.BackgroundColor = System.Drawing.Color.White;
		this.gvExtEvTP.BorderStyle = System.Windows.Forms.BorderStyle.None;
		dataGridViewCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
		dataGridViewCellStyle.BackColor = System.Drawing.SystemColors.Control;
		dataGridViewCellStyle.Font = new System.Drawing.Font("宋体", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		dataGridViewCellStyle.ForeColor = System.Drawing.SystemColors.WindowText;
		dataGridViewCellStyle.SelectionBackColor = System.Drawing.SystemColors.Highlight;
		dataGridViewCellStyle.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
		dataGridViewCellStyle.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
		this.gvExtEvTP.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle;
		this.gvExtEvTP.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
		this.gvExtEvTP.Columns.AddRange(this.clmExtEvTP0, this.clmExtEvTP1, this.clmExtEvTP2, this.clmExtEvTP3);
		this.gvExtEvTP.EnableHeadersVisualStyles = false;
		this.gvExtEvTP.Location = new System.Drawing.Point(0, 0);
		this.gvExtEvTP.MultiSelect = false;
		this.gvExtEvTP.Name = "gvExtEvTP";
		this.gvExtEvTP.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
		this.gvExtEvTP.RowTemplate.Height = 18;
		this.gvExtEvTP.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.gvExtEvTP.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
		this.gvExtEvTP.ShowEditingIcon = false;
		this.gvExtEvTP.Size = new System.Drawing.Size(299, 168);
		this.gvExtEvTP.TabIndex = 4;
		this.gvExtEvTP.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(dgvPT_DataError);
		dataGridViewCellStyle2.Format = "0.00";
		this.clmExtEvTP0.DefaultCellStyle = dataGridViewCellStyle2;
		this.clmExtEvTP0.HeaderText = "事件1";
		this.clmExtEvTP0.Name = "clmExtEvTP0";
		this.clmExtEvTP0.Width = 60;
		dataGridViewCellStyle3.Format = "0.00";
		this.clmExtEvTP1.DefaultCellStyle = dataGridViewCellStyle3;
		this.clmExtEvTP1.HeaderText = "事件2";
		this.clmExtEvTP1.Name = "clmExtEvTP1";
		this.clmExtEvTP1.Width = 60;
		dataGridViewCellStyle4.Format = "0.00";
		this.clmExtEvTP2.DefaultCellStyle = dataGridViewCellStyle4;
		this.clmExtEvTP2.HeaderText = "事件3";
		this.clmExtEvTP2.Name = "clmExtEvTP2";
		this.clmExtEvTP2.Width = 60;
		dataGridViewCellStyle5.Format = "0.00";
		this.clmExtEvTP3.DefaultCellStyle = dataGridViewCellStyle5;
		this.clmExtEvTP3.HeaderText = "事件4";
		this.clmExtEvTP3.Name = "clmExtEvTP3";
		this.clmExtEvTP3.Width = 60;
		this.tpTempProg.Controls.Add(this.btnPTQry);
		this.tpTempProg.Controls.Add(this.btnPTSet);
		this.tpTempProg.Controls.Add(this.lbptInitT);
		this.tpTempProg.Controls.Add(this.label2);
		this.tpTempProg.Controls.Add(this.label27);
		this.tpTempProg.Controls.Add(this.tbptIniTempHoldT);
		this.tpTempProg.Controls.Add(this.dpgcProgTemp);
		this.tpTempProg.Controls.Add(this.dgvPT);
		this.tpTempProg.Location = new System.Drawing.Point(4, 4);
		this.tpTempProg.Name = "tpTempProg";
		this.tpTempProg.Size = new System.Drawing.Size(530, 317);
		this.tpTempProg.TabIndex = 13;
		this.tpTempProg.Text = "程序升温";
		this.tpTempProg.UseVisualStyleBackColor = true;
		this.btnPTQry.Location = new System.Drawing.Point(379, 91);
		this.btnPTQry.Name = "btnPTQry";
		this.btnPTQry.Size = new System.Drawing.Size(66, 23);
		this.btnPTQry.TabIndex = 11;
		this.btnPTQry.Text = "查询硬件";
		this.btnPTQry.UseVisualStyleBackColor = true;
		this.btnPTQry.Click += new System.EventHandler(btnPTSet_Click);
		this.btnPTSet.Location = new System.Drawing.Point(451, 91);
		this.btnPTSet.Name = "btnPTSet";
		this.btnPTSet.Size = new System.Drawing.Size(61, 23);
		this.btnPTSet.TabIndex = 12;
		this.btnPTSet.Text = "写入硬件";
		this.btnPTSet.UseVisualStyleBackColor = true;
		this.btnPTSet.Click += new System.EventHandler(btnPTSet_Click);
		this.lbptInitT.AutoSize = true;
		this.lbptInitT.Location = new System.Drawing.Point(304, 72);
		this.lbptInitT.Name = "lbptInitT";
		this.lbptInitT.Size = new System.Drawing.Size(23, 12);
		this.lbptInitT.TabIndex = 9;
		this.lbptInitT.Text = "100";
		this.label2.AutoSize = true;
		this.label2.Location = new System.Drawing.Point(222, 72);
		this.label2.Name = "label2";
		this.label2.Size = new System.Drawing.Size(53, 12);
		this.label2.TabIndex = 9;
		this.label2.Text = "初温[℃]";
		this.label27.AutoSize = true;
		this.label27.Location = new System.Drawing.Point(222, 97);
		this.label27.Name = "label27";
		this.label27.Size = new System.Drawing.Size(83, 12);
		this.label27.TabIndex = 9;
		this.label27.Text = "初温保持[min]";
		this.tbptIniTempHoldT.Location = new System.Drawing.Point(306, 92);
		this.tbptIniTempHoldT.Name = "tbptIniTempHoldT";
		this.tbptIniTempHoldT.Size = new System.Drawing.Size(44, 21);
		this.tbptIniTempHoldT.TabIndex = 10;
		this.tbptIniTempHoldT.Text = "?";
		this.tbptIniTempHoldT.KeyDown += new System.Windows.Forms.KeyEventHandler(tbptIniTempHoldT_KeyDown);
		this.dpgcProgTemp.BackColor = System.Drawing.Color.BlanchedAlmond;
		this.dpgcProgTemp.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.dpgcProgTemp.Location = new System.Drawing.Point(219, 185);
		this.dpgcProgTemp.Name = "dpgcProgTemp";
		this.dpgcProgTemp.Size = new System.Drawing.Size(311, 132);
		this.dpgcProgTemp.TabIndex = 4;
		this.dpgcProgTemp.Paint += new System.Windows.Forms.PaintEventHandler(dpgcProgTemp_Paint);
		this.dgvPT.AllowUserToAddRows = false;
		this.dgvPT.AllowUserToDeleteRows = false;
		this.dgvPT.AllowUserToResizeRows = false;
		this.dgvPT.BackgroundColor = System.Drawing.Color.White;
		this.dgvPT.BorderStyle = System.Windows.Forms.BorderStyle.None;
		this.dgvPT.CharacterHeaderColor = System.Drawing.Color.Black;
		dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
		dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Control;
		dataGridViewCellStyle6.Font = new System.Drawing.Font("宋体", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.WindowText;
		dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
		dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
		dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
		this.dgvPT.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle6;
		this.dgvPT.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
		this.dgvPT.Dock = System.Windows.Forms.DockStyle.Left;
		this.dgvPT.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
		this.dgvPT.EnableHeadersVisualStyles = false;
		this.dgvPT.Location = new System.Drawing.Point(0, 0);
		this.dgvPT.MultiSelect = false;
		this.dgvPT.Name = "dgvPT";
		this.dgvPT.RowHeadersWidth = 21;
		this.dgvPT.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
		this.dgvPT.RowTemplate.Height = 18;
		this.dgvPT.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.dgvPT.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
		this.dgvPT.ShowCellToolTips = false;
		this.dgvPT.ShowEditingIcon = false;
		this.dgvPT.Size = new System.Drawing.Size(219, 317);
		this.dgvPT.TabIndex = 8;
		this.dgvPT.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(dgvPT_CellEndEdit);
		this.dgvPT.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(dgvPT_DataError);
		this.tpLC.Location = new System.Drawing.Point(4, 4);
		this.tpLC.Name = "tpLC";
		this.tpLC.Size = new System.Drawing.Size(530, 317);
		this.tpLC.TabIndex = 2;
		this.tpLC.Text = "液相";
		this.tpLC.UseVisualStyleBackColor = true;
		this.tpGradient.Controls.Add(this.cblcUse);
		this.tpGradient.Controls.Add(this.gblcOption);
		this.tpGradient.Controls.Add(this.gblcIdleState);
		this.tpGradient.Controls.Add(this.gblcStandBy);
		this.tpGradient.Controls.Add(this.dplcGradient);
		this.tpGradient.Controls.Add(this.gvlcGradient);
		this.tpGradient.Location = new System.Drawing.Point(4, 4);
		this.tpGradient.Name = "tpGradient";
		this.tpGradient.Size = new System.Drawing.Size(530, 317);
		this.tpGradient.TabIndex = 3;
		this.tpGradient.Text = "液相梯度";
		this.tpGradient.UseVisualStyleBackColor = true;
		this.cblcUse.AutoSize = true;
		this.cblcUse.Location = new System.Drawing.Point(378, 3);
		this.cblcUse.Name = "cblcUse";
		this.cblcUse.Size = new System.Drawing.Size(84, 16);
		this.cblcUse.TabIndex = 7;
		this.cblcUse.Text = "使用梯度表";
		this.cblcUse.UseVisualStyleBackColor = true;
		this.gblcOption.Controls.Add(this.tblgoptSolvent4);
		this.gblcOption.Controls.Add(this.tblgoptSolvent3);
		this.gblcOption.Controls.Add(this.tblgoptSolvent2);
		this.gblcOption.Controls.Add(this.tblgoptSolvent1);
		this.gblcOption.Controls.Add(this.cblgoptSolvent4);
		this.gblcOption.Controls.Add(this.cblgoptSolvent3);
		this.gblcOption.Controls.Add(this.cblgoptSolvent2);
		this.gblcOption.Controls.Add(this.lblcPumpNum);
		this.gblcOption.Controls.Add(this.cblgoptSolvent1);
		this.gblcOption.Location = new System.Drawing.Point(376, 109);
		this.gblcOption.Name = "gblcOption";
		this.gblcOption.Size = new System.Drawing.Size(150, 137);
		this.gblcOption.TabIndex = 6;
		this.gblcOption.TabStop = false;
		this.gblcOption.Text = "梯度选项";
		this.tblgoptSolvent4.Location = new System.Drawing.Point(70, 110);
		this.tblgoptSolvent4.Name = "tblgoptSolvent4";
		this.tblgoptSolvent4.Size = new System.Drawing.Size(74, 21);
		this.tblgoptSolvent4.TabIndex = 10;
		this.tblgoptSolvent4.TextChanged += new System.EventHandler(cblgoptSolvent1_Click);
		this.tblgoptSolvent3.Location = new System.Drawing.Point(70, 86);
		this.tblgoptSolvent3.Name = "tblgoptSolvent3";
		this.tblgoptSolvent3.Size = new System.Drawing.Size(74, 21);
		this.tblgoptSolvent3.TabIndex = 9;
		this.tblgoptSolvent3.TextChanged += new System.EventHandler(cblgoptSolvent1_Click);
		this.tblgoptSolvent2.Location = new System.Drawing.Point(70, 62);
		this.tblgoptSolvent2.Name = "tblgoptSolvent2";
		this.tblgoptSolvent2.Size = new System.Drawing.Size(74, 21);
		this.tblgoptSolvent2.TabIndex = 11;
		this.tblgoptSolvent2.TextChanged += new System.EventHandler(cblgoptSolvent1_Click);
		this.tblgoptSolvent1.Location = new System.Drawing.Point(70, 38);
		this.tblgoptSolvent1.Name = "tblgoptSolvent1";
		this.tblgoptSolvent1.Size = new System.Drawing.Size(74, 21);
		this.tblgoptSolvent1.TabIndex = 13;
		this.tblgoptSolvent1.TextChanged += new System.EventHandler(cblgoptSolvent1_Click);
		this.cblgoptSolvent4.AutoSize = true;
		this.cblgoptSolvent4.Location = new System.Drawing.Point(6, 112);
		this.cblgoptSolvent4.Name = "cblgoptSolvent4";
		this.cblgoptSolvent4.Size = new System.Drawing.Size(60, 16);
		this.cblgoptSolvent4.TabIndex = 15;
		this.cblgoptSolvent4.Text = "溶剂 4";
		this.cblgoptSolvent4.UseVisualStyleBackColor = true;
		this.cblgoptSolvent4.Click += new System.EventHandler(cblgoptSolvent1_Click);
		this.cblgoptSolvent3.AutoSize = true;
		this.cblgoptSolvent3.Location = new System.Drawing.Point(6, 88);
		this.cblgoptSolvent3.Name = "cblgoptSolvent3";
		this.cblgoptSolvent3.Size = new System.Drawing.Size(60, 16);
		this.cblgoptSolvent3.TabIndex = 17;
		this.cblgoptSolvent3.Text = "溶剂 3";
		this.cblgoptSolvent3.UseVisualStyleBackColor = true;
		this.cblgoptSolvent3.Click += new System.EventHandler(cblgoptSolvent1_Click);
		this.cblgoptSolvent2.AutoSize = true;
		this.cblgoptSolvent2.Location = new System.Drawing.Point(6, 64);
		this.cblgoptSolvent2.Name = "cblgoptSolvent2";
		this.cblgoptSolvent2.Size = new System.Drawing.Size(60, 16);
		this.cblgoptSolvent2.TabIndex = 16;
		this.cblgoptSolvent2.Text = "溶剂 2";
		this.cblgoptSolvent2.UseVisualStyleBackColor = true;
		this.cblgoptSolvent2.Click += new System.EventHandler(cblgoptSolvent1_Click);
		this.lblcPumpNum.AutoSize = true;
		this.lblcPumpNum.Location = new System.Drawing.Point(6, 20);
		this.lblcPumpNum.Name = "lblcPumpNum";
		this.lblcPumpNum.Size = new System.Drawing.Size(35, 12);
		this.lblcPumpNum.TabIndex = 0;
		this.lblcPumpNum.Text = "泵数:";
		this.cblgoptSolvent1.AutoSize = true;
		this.cblgoptSolvent1.Location = new System.Drawing.Point(6, 40);
		this.cblgoptSolvent1.Name = "cblgoptSolvent1";
		this.cblgoptSolvent1.Size = new System.Drawing.Size(60, 16);
		this.cblgoptSolvent1.TabIndex = 14;
		this.cblgoptSolvent1.Text = "溶剂 1";
		this.cblgoptSolvent1.UseVisualStyleBackColor = true;
		this.cblgoptSolvent1.Click += new System.EventHandler(cblgoptSolvent1_Click);
		this.gblcIdleState.Controls.Add(this.rbisMonitorSet);
		this.gblcIdleState.Controls.Add(this.rbisInitial);
		this.gblcIdleState.Controls.Add(this.rbisPumpOff);
		this.gblcIdleState.Location = new System.Drawing.Point(376, 22);
		this.gblcIdleState.Name = "gblcIdleState";
		this.gblcIdleState.Size = new System.Drawing.Size(123, 81);
		this.gblcIdleState.TabIndex = 4;
		this.gblcIdleState.TabStop = false;
		this.gblcIdleState.Text = "空闲状态";
		this.rbisMonitorSet.AutoSize = true;
		this.rbisMonitorSet.Location = new System.Drawing.Point(6, 60);
		this.rbisMonitorSet.Name = "rbisMonitorSet";
		this.rbisMonitorSet.Size = new System.Drawing.Size(71, 16);
		this.rbisMonitorSet.TabIndex = 0;
		this.rbisMonitorSet.TabStop = true;
		this.rbisMonitorSet.Text = "监控设置";
		this.rbisMonitorSet.UseVisualStyleBackColor = true;
		this.rbisInitial.AutoSize = true;
		this.rbisInitial.Location = new System.Drawing.Point(6, 40);
		this.rbisInitial.Name = "rbisInitial";
		this.rbisInitial.Size = new System.Drawing.Size(47, 16);
		this.rbisInitial.TabIndex = 0;
		this.rbisInitial.TabStop = true;
		this.rbisInitial.Text = "初始";
		this.rbisInitial.UseVisualStyleBackColor = true;
		this.rbisPumpOff.AutoSize = true;
		this.rbisPumpOff.Location = new System.Drawing.Point(6, 20);
		this.rbisPumpOff.Name = "rbisPumpOff";
		this.rbisPumpOff.Size = new System.Drawing.Size(47, 16);
		this.rbisPumpOff.TabIndex = 0;
		this.rbisPumpOff.TabStop = true;
		this.rbisPumpOff.Text = "关泵";
		this.rbisPumpOff.UseVisualStyleBackColor = true;
		this.gblcStandBy.Controls.Add(this.tbsbPersist);
		this.gblcStandBy.Controls.Add(this.tbsbTimeTo);
		this.gblcStandBy.Controls.Add(this.tbsbFlowRate);
		this.gblcStandBy.Controls.Add(this.lclLabel7);
		this.gblcStandBy.Controls.Add(this.lbsbPersist);
		this.gblcStandBy.Controls.Add(this.lclLabel6);
		this.gblcStandBy.Controls.Add(this.lclLabel5);
		this.gblcStandBy.Controls.Add(this.lbsbTimeTo);
		this.gblcStandBy.Controls.Add(this.lbsbFlowRate);
		this.gblcStandBy.Location = new System.Drawing.Point(481, 3);
		this.gblcStandBy.Name = "gblcStandBy";
		this.gblcStandBy.Size = new System.Drawing.Size(167, 53);
		this.gblcStandBy.TabIndex = 3;
		this.gblcStandBy.TabStop = false;
		this.gblcStandBy.Text = "监控设置";
		this.gblcStandBy.Visible = false;
		this.tbsbPersist.Location = new System.Drawing.Point(60, 66);
		this.tbsbPersist.Name = "tbsbPersist";
		this.tbsbPersist.Size = new System.Drawing.Size(53, 21);
		this.tbsbPersist.TabIndex = 1;
		this.tbsbTimeTo.Location = new System.Drawing.Point(60, 43);
		this.tbsbTimeTo.Name = "tbsbTimeTo";
		this.tbsbTimeTo.Size = new System.Drawing.Size(53, 21);
		this.tbsbTimeTo.TabIndex = 1;
		this.tbsbFlowRate.Location = new System.Drawing.Point(60, 20);
		this.tbsbFlowRate.Name = "tbsbFlowRate";
		this.tbsbFlowRate.Size = new System.Drawing.Size(53, 21);
		this.tbsbFlowRate.TabIndex = 1;
		this.lclLabel7.AutoSize = true;
		this.lclLabel7.Location = new System.Drawing.Point(121, 71);
		this.lclLabel7.Name = "lclLabel7";
		this.lclLabel7.Size = new System.Drawing.Size(35, 12);
		this.lclLabel7.TabIndex = 0;
		this.lclLabel7.Text = "[min]";
		this.lbsbPersist.AutoSize = true;
		this.lbsbPersist.Location = new System.Drawing.Point(6, 70);
		this.lbsbPersist.Name = "lbsbPersist";
		this.lbsbPersist.Size = new System.Drawing.Size(59, 12);
		this.lbsbPersist.TabIndex = 0;
		this.lbsbPersist.Text = "lclLabel1";
		this.lclLabel6.AutoSize = true;
		this.lclLabel6.Location = new System.Drawing.Point(121, 48);
		this.lclLabel6.Name = "lclLabel6";
		this.lclLabel6.Size = new System.Drawing.Size(35, 12);
		this.lclLabel6.TabIndex = 0;
		this.lclLabel6.Text = "[min]";
		this.lclLabel5.AutoSize = true;
		this.lclLabel5.Location = new System.Drawing.Point(121, 25);
		this.lclLabel5.Name = "lclLabel5";
		this.lclLabel5.Size = new System.Drawing.Size(41, 12);
		this.lclLabel5.TabIndex = 0;
		this.lclLabel5.Text = "mL/min";
		this.lbsbTimeTo.AutoSize = true;
		this.lbsbTimeTo.Location = new System.Drawing.Point(6, 47);
		this.lbsbTimeTo.Name = "lbsbTimeTo";
		this.lbsbTimeTo.Size = new System.Drawing.Size(59, 12);
		this.lbsbTimeTo.TabIndex = 0;
		this.lbsbTimeTo.Text = "lclLabel1";
		this.lbsbFlowRate.AutoSize = true;
		this.lbsbFlowRate.Location = new System.Drawing.Point(6, 24);
		this.lbsbFlowRate.Name = "lbsbFlowRate";
		this.lbsbFlowRate.Size = new System.Drawing.Size(29, 12);
		this.lbsbFlowRate.TabIndex = 0;
		this.lbsbFlowRate.Text = "流速";
		this.dplcGradient.BackColor = System.Drawing.Color.BlanchedAlmond;
		this.dplcGradient.Location = new System.Drawing.Point(4, 115);
		this.dplcGradient.Name = "dplcGradient";
		this.dplcGradient.Size = new System.Drawing.Size(367, 131);
		this.dplcGradient.TabIndex = 2;
		this.dplcGradient.Paint += new System.Windows.Forms.PaintEventHandler(dplcGradient_Paint);
		this.gvlcGradient.AllowUserToAddRows = false;
		this.gvlcGradient.AllowUserToDeleteRows = false;
		this.gvlcGradient.AllowUserToResizeRows = false;
		this.gvlcGradient.BackgroundColor = System.Drawing.Color.AliceBlue;
		this.gvlcGradient.CharacterHeaderColor = System.Drawing.Color.Black;
		this.gvlcGradient.ColumnHeadersHeight = 32;
		this.gvlcGradient.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
		this.gvlcGradient.ContextMenuStrip = this.cmsLcGradient;
		this.gvlcGradient.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
		this.gvlcGradient.Location = new System.Drawing.Point(4, 3);
		this.gvlcGradient.Name = "gvlcGradient";
		this.gvlcGradient.RowHeadersWidth = 25;
		this.gvlcGradient.RowTemplate.Height = 16;
		this.gvlcGradient.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
		this.gvlcGradient.ShowCellToolTips = false;
		this.gvlcGradient.Size = new System.Drawing.Size(367, 109);
		this.gvlcGradient.TabIndex = 1;
		this.gvlcGradient.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(gvlcGradient_CellEndEdit);
		this.cmsLcGradient.Items.AddRange(new System.Windows.Forms.ToolStripItem[2] { this.milgAddRow, this.milgDeleteRow });
		this.cmsLcGradient.Name = "cmsLcGradient";
		this.cmsLcGradient.Size = new System.Drawing.Size(113, 48);
		this.milgAddRow.Name = "milgAddRow";
		this.milgAddRow.Size = new System.Drawing.Size(112, 22);
		this.milgAddRow.Text = "添加行";
		this.milgAddRow.Click += new System.EventHandler(milgDeleteRow_Click);
		this.milgDeleteRow.Name = "milgDeleteRow";
		this.milgDeleteRow.Size = new System.Drawing.Size(112, 22);
		this.milgDeleteRow.Text = "删除行";
		this.milgDeleteRow.Click += new System.EventHandler(milgDeleteRow_Click);
		this.tpUV.Controls.Add(this.gvProgWave);
		this.tpUV.Controls.Add(this.cbWaveScan);
		this.tpUV.Controls.Add(this.cbUseProgWave);
		this.tpUV.Controls.Add(this.tbwsStep);
		this.tpUV.Controls.Add(this.tbwsTo);
		this.tpUV.Controls.Add(this.tbwsFrom);
		this.tpUV.Controls.Add(this.tbwsStepFreq);
		this.tpUV.Controls.Add(this.tbwsStartT);
		this.tpUV.Controls.Add(this.label17);
		this.tpUV.Controls.Add(this.label16);
		this.tpUV.Controls.Add(this.label1);
		this.tpUV.Controls.Add(this.label15);
		this.tpUV.Controls.Add(this.label14);
		this.tpUV.Location = new System.Drawing.Point(4, 4);
		this.tpUV.Name = "tpUV";
		this.tpUV.Size = new System.Drawing.Size(530, 317);
		this.tpUV.TabIndex = 14;
		this.tpUV.Text = "UV";
		this.tpUV.UseVisualStyleBackColor = true;
		this.gvProgWave.AllowUserToAddRows = false;
		this.gvProgWave.AllowUserToDeleteRows = false;
		this.gvProgWave.AllowUserToResizeRows = false;
		this.gvProgWave.BackgroundColor = System.Drawing.Color.AliceBlue;
		this.gvProgWave.CharacterHeaderColor = System.Drawing.Color.Black;
		this.gvProgWave.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
		this.gvProgWave.Columns.AddRange(this.colTime, this.colWave);
		this.gvProgWave.ContextMenuStrip = this.cmsLcGradient;
		this.gvProgWave.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
		this.gvProgWave.Location = new System.Drawing.Point(4, 3);
		this.gvProgWave.Name = "gvProgWave";
		this.gvProgWave.RowHeadersWidth = 25;
		this.gvProgWave.RowTemplate.Height = 16;
		this.gvProgWave.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
		this.gvProgWave.ShowCellToolTips = false;
		this.gvProgWave.Size = new System.Drawing.Size(236, 136);
		this.gvProgWave.TabIndex = 28;
		this.colTime.HeaderText = "时间(min)";
		this.colTime.Name = "colTime";
		this.colTime.Width = 80;
		this.colWave.HeaderText = "波长(nm)";
		this.colWave.Name = "colWave";
		this.colWave.Width = 80;
		this.cbWaveScan.AutoSize = true;
		this.cbWaveScan.Location = new System.Drawing.Point(189, 157);
		this.cbWaveScan.Name = "cbWaveScan";
		this.cbWaveScan.Size = new System.Drawing.Size(72, 16);
		this.cbWaveScan.TabIndex = 27;
		this.cbWaveScan.Text = "波长扫描";
		this.cbWaveScan.UseVisualStyleBackColor = true;
		this.cbWaveScan.Visible = false;
		this.cbUseProgWave.AutoSize = true;
		this.cbUseProgWave.Location = new System.Drawing.Point(267, 3);
		this.cbUseProgWave.Name = "cbUseProgWave";
		this.cbUseProgWave.Size = new System.Drawing.Size(96, 16);
		this.cbUseProgWave.TabIndex = 21;
		this.cbUseProgWave.Text = "使用程序波长";
		this.cbUseProgWave.UseVisualStyleBackColor = true;
		this.tbwsStep.Location = new System.Drawing.Point(410, 190);
		this.tbwsStep.Name = "tbwsStep";
		this.tbwsStep.Size = new System.Drawing.Size(39, 21);
		this.tbwsStep.TabIndex = 24;
		this.tbwsStep.Text = "1";
		this.tbwsStep.Visible = false;
		this.tbwsTo.Location = new System.Drawing.Point(358, 190);
		this.tbwsTo.Name = "tbwsTo";
		this.tbwsTo.Size = new System.Drawing.Size(39, 21);
		this.tbwsTo.TabIndex = 25;
		this.tbwsTo.Text = "500";
		this.tbwsTo.Visible = false;
		this.tbwsFrom.Location = new System.Drawing.Point(306, 190);
		this.tbwsFrom.Name = "tbwsFrom";
		this.tbwsFrom.Size = new System.Drawing.Size(39, 21);
		this.tbwsFrom.TabIndex = 26;
		this.tbwsFrom.Text = "200";
		this.tbwsFrom.Visible = false;
		this.tbwsStepFreq.Location = new System.Drawing.Point(239, 190);
		this.tbwsStepFreq.Name = "tbwsStepFreq";
		this.tbwsStepFreq.Size = new System.Drawing.Size(39, 21);
		this.tbwsStepFreq.TabIndex = 23;
		this.tbwsStepFreq.Text = "5";
		this.tbwsStepFreq.Visible = false;
		this.tbwsStartT.Location = new System.Drawing.Point(189, 190);
		this.tbwsStartT.Name = "tbwsStartT";
		this.tbwsStartT.Size = new System.Drawing.Size(39, 21);
		this.tbwsStartT.TabIndex = 23;
		this.tbwsStartT.Text = "5";
		this.tbwsStartT.Visible = false;
		this.label17.AutoSize = true;
		this.label17.Location = new System.Drawing.Point(407, 175);
		this.label17.Name = "label17";
		this.label17.Size = new System.Drawing.Size(53, 12);
		this.label17.TabIndex = 19;
		this.label17.Text = "步长(nm)";
		this.label17.Visible = false;
		this.label16.AutoSize = true;
		this.label16.Location = new System.Drawing.Point(355, 175);
		this.label16.Name = "label16";
		this.label16.Size = new System.Drawing.Size(41, 12);
		this.label16.TabIndex = 18;
		this.label16.Text = "到(nm)";
		this.label16.Visible = false;
		this.label1.AutoSize = true;
		this.label1.Location = new System.Drawing.Point(241, 175);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(53, 12);
		this.label1.TabIndex = 20;
		this.label1.Text = "步频(ms)";
		this.label1.Visible = false;
		this.label15.AutoSize = true;
		this.label15.Location = new System.Drawing.Point(303, 175);
		this.label15.Name = "label15";
		this.label15.Size = new System.Drawing.Size(41, 12);
		this.label15.TabIndex = 17;
		this.label15.Text = "从(nm)";
		this.label15.Visible = false;
		this.label14.AutoSize = true;
		this.label14.Location = new System.Drawing.Point(191, 175);
		this.label14.Name = "label14";
		this.label14.Size = new System.Drawing.Size(59, 12);
		this.label14.TabIndex = 20;
		this.label14.Text = "开始(min)";
		this.label14.Visible = false;
		this.tpMeasurement.Controls.Add(this.gbmsmExternalControl);
		this.tpMeasurement.Controls.Add(this.gbmsmAcquisition);
		this.tpMeasurement.Controls.Add(this.tbmsmNote);
		this.tpMeasurement.Controls.Add(this.tbmsmTemperature);
		this.tpMeasurement.Controls.Add(this.tbmsmDetection);
		this.tpMeasurement.Controls.Add(this.tbmsmPressure);
		this.tpMeasurement.Controls.Add(this.tbmsmFlowRate);
		this.tpMeasurement.Controls.Add(this.tbmsmMobilePhase);
		this.tpMeasurement.Controls.Add(this.tbmsmColumn);
		this.tpMeasurement.Controls.Add(this.tbmsmMtdDspt);
		this.tpMeasurement.Controls.Add(this.lbmsmNote);
		this.tpMeasurement.Controls.Add(this.lbmsmTemperature);
		this.tpMeasurement.Controls.Add(this.lbmsmDetection);
		this.tpMeasurement.Controls.Add(this.lbmsmPressure);
		this.tpMeasurement.Controls.Add(this.lbmsmFlowRate);
		this.tpMeasurement.Controls.Add(this.lbmsmMobilePhase);
		this.tpMeasurement.Controls.Add(this.lbmsmColumn);
		this.tpMeasurement.Controls.Add(this.lbmsmMtdDspt);
		this.tpMeasurement.Location = new System.Drawing.Point(4, 4);
		this.tpMeasurement.Name = "tpMeasurement";
		this.tpMeasurement.Size = new System.Drawing.Size(530, 317);
		this.tpMeasurement.TabIndex = 4;
		this.tpMeasurement.Text = "测量";
		this.tpMeasurement.UseVisualStyleBackColor = true;
		this.gbmsmExternalControl.Controls.Add(this.pbecDown);
		this.gbmsmExternalControl.Controls.Add(this.pbecUp);
		this.gbmsmExternalControl.Controls.Add(this.rbecDown);
		this.gbmsmExternalControl.Controls.Add(this.rbecUp);
		this.gbmsmExternalControl.Controls.Add(this.pnlecES);
		this.gbmsmExternalControl.Controls.Add(this.cbecExternalControl);
		this.gbmsmExternalControl.Location = new System.Drawing.Point(374, 74);
		this.gbmsmExternalControl.Name = "gbmsmExternalControl";
		this.gbmsmExternalControl.Size = new System.Drawing.Size(152, 155);
		this.gbmsmExternalControl.TabIndex = 2;
		this.gbmsmExternalControl.TabStop = false;
		this.gbmsmExternalControl.Text = "外部控制";
		this.pbecDown.Location = new System.Drawing.Point(126, 133);
		this.pbecDown.Name = "pbecDown";
		this.pbecDown.Size = new System.Drawing.Size(20, 15);
		this.pbecDown.TabIndex = 5;
		this.pbecDown.TabStop = false;
		this.pbecUp.Location = new System.Drawing.Point(126, 113);
		this.pbecUp.Name = "pbecUp";
		this.pbecUp.Size = new System.Drawing.Size(20, 15);
		this.pbecUp.TabIndex = 5;
		this.pbecUp.TabStop = false;
		this.rbecDown.AutoSize = true;
		this.rbecDown.Enabled = false;
		this.rbecDown.Location = new System.Drawing.Point(7, 134);
		this.rbecDown.Name = "rbecDown";
		this.rbecDown.Size = new System.Drawing.Size(59, 16);
		this.rbecDown.TabIndex = 0;
		this.rbecDown.TabStop = true;
		this.rbecDown.Text = "下降沿";
		this.rbecDown.UseVisualStyleBackColor = true;
		this.rbecUp.AutoSize = true;
		this.rbecUp.Enabled = false;
		this.rbecUp.Location = new System.Drawing.Point(7, 113);
		this.rbecUp.Name = "rbecUp";
		this.rbecUp.Size = new System.Drawing.Size(59, 16);
		this.rbecUp.TabIndex = 0;
		this.rbecUp.TabStop = true;
		this.rbecUp.Text = "上升沿";
		this.rbecUp.UseVisualStyleBackColor = true;
		this.pnlecES.Controls.Add(this.rbecStartStop);
		this.pnlecES.Controls.Add(this.rbecStartRestart);
		this.pnlecES.Controls.Add(this.rbecStartOnly);
		this.pnlecES.Location = new System.Drawing.Point(4, 39);
		this.pnlecES.Name = "pnlecES";
		this.pnlecES.Size = new System.Drawing.Size(143, 69);
		this.pnlecES.TabIndex = 4;
		this.pnlecES.Paint += new System.Windows.Forms.PaintEventHandler(pnlecES_Paint);
		this.rbecStartStop.AutoSize = true;
		this.rbecStartStop.Location = new System.Drawing.Point(3, 48);
		this.rbecStartStop.Name = "rbecStartStop";
		this.rbecStartStop.Size = new System.Drawing.Size(77, 16);
		this.rbecStartStop.TabIndex = 0;
		this.rbecStartStop.TabStop = true;
		this.rbecStartStop.Text = "开始-结束";
		this.rbecStartStop.UseVisualStyleBackColor = true;
		this.rbecStartRestart.AutoSize = true;
		this.rbecStartRestart.Location = new System.Drawing.Point(2, 27);
		this.rbecStartRestart.Name = "rbecStartRestart";
		this.rbecStartRestart.Size = new System.Drawing.Size(77, 16);
		this.rbecStartRestart.TabIndex = 0;
		this.rbecStartRestart.TabStop = true;
		this.rbecStartRestart.Text = "开始-开始";
		this.rbecStartRestart.UseVisualStyleBackColor = true;
		this.rbecStartOnly.AutoSize = true;
		this.rbecStartOnly.Location = new System.Drawing.Point(2, 6);
		this.rbecStartOnly.Name = "rbecStartOnly";
		this.rbecStartOnly.Size = new System.Drawing.Size(59, 16);
		this.rbecStartOnly.TabIndex = 0;
		this.rbecStartOnly.TabStop = true;
		this.rbecStartOnly.Text = "仅开始";
		this.rbecStartOnly.UseVisualStyleBackColor = true;
		this.cbecExternalControl.AutoSize = true;
		this.cbecExternalControl.Location = new System.Drawing.Point(6, 19);
		this.cbecExternalControl.Name = "cbecExternalControl";
		this.cbecExternalControl.Size = new System.Drawing.Size(102, 16);
		this.cbecExternalControl.TabIndex = 3;
		this.cbecExternalControl.Text = "外部开始/结束";
		this.cbecExternalControl.UseVisualStyleBackColor = true;
		this.gbmsmAcquisition.Controls.Add(this.cbacqAutoStop);
		this.gbmsmAcquisition.Controls.Add(this.lclLabel17);
		this.gbmsmAcquisition.Controls.Add(this.lbacqRunTime);
		this.gbmsmAcquisition.Controls.Add(this.tbacqRunTime);
		this.gbmsmAcquisition.Location = new System.Drawing.Point(374, 5);
		this.gbmsmAcquisition.Name = "gbmsmAcquisition";
		this.gbmsmAcquisition.Size = new System.Drawing.Size(152, 63);
		this.gbmsmAcquisition.TabIndex = 2;
		this.gbmsmAcquisition.TabStop = false;
		this.gbmsmAcquisition.Text = "采集";
		this.cbacqAutoStop.AutoSize = true;
		this.cbacqAutoStop.Location = new System.Drawing.Point(8, 17);
		this.cbacqAutoStop.Name = "cbacqAutoStop";
		this.cbacqAutoStop.Size = new System.Drawing.Size(72, 16);
		this.cbacqAutoStop.TabIndex = 3;
		this.cbacqAutoStop.Text = "自动结束";
		this.cbacqAutoStop.UseVisualStyleBackColor = true;
		this.lclLabel17.AutoSize = true;
		this.lclLabel17.Location = new System.Drawing.Point(111, 40);
		this.lclLabel17.Name = "lclLabel17";
		this.lclLabel17.Size = new System.Drawing.Size(35, 12);
		this.lclLabel17.TabIndex = 0;
		this.lclLabel17.Text = "[min]";
		this.lbacqRunTime.AutoSize = true;
		this.lbacqRunTime.Location = new System.Drawing.Point(6, 40);
		this.lbacqRunTime.Name = "lbacqRunTime";
		this.lbacqRunTime.Size = new System.Drawing.Size(53, 12);
		this.lbacqRunTime.TabIndex = 0;
		this.lbacqRunTime.Text = "运行时间";
		this.tbacqRunTime.Location = new System.Drawing.Point(69, 36);
		this.tbacqRunTime.Name = "tbacqRunTime";
		this.tbacqRunTime.Size = new System.Drawing.Size(39, 21);
		this.tbacqRunTime.TabIndex = 1;
		this.tbmsmNote.Location = new System.Drawing.Point(72, 173);
		this.tbmsmNote.Multiline = true;
		this.tbmsmNote.Name = "tbmsmNote";
		this.tbmsmNote.Size = new System.Drawing.Size(296, 70);
		this.tbmsmNote.TabIndex = 1;
		this.tbmsmTemperature.Location = new System.Drawing.Point(72, 149);
		this.tbmsmTemperature.Name = "tbmsmTemperature";
		this.tbmsmTemperature.Size = new System.Drawing.Size(296, 21);
		this.tbmsmTemperature.TabIndex = 1;
		this.tbmsmDetection.Location = new System.Drawing.Point(72, 125);
		this.tbmsmDetection.Name = "tbmsmDetection";
		this.tbmsmDetection.Size = new System.Drawing.Size(296, 21);
		this.tbmsmDetection.TabIndex = 1;
		this.tbmsmPressure.Location = new System.Drawing.Point(72, 101);
		this.tbmsmPressure.Name = "tbmsmPressure";
		this.tbmsmPressure.Size = new System.Drawing.Size(296, 21);
		this.tbmsmPressure.TabIndex = 1;
		this.tbmsmFlowRate.Location = new System.Drawing.Point(72, 77);
		this.tbmsmFlowRate.Name = "tbmsmFlowRate";
		this.tbmsmFlowRate.Size = new System.Drawing.Size(296, 21);
		this.tbmsmFlowRate.TabIndex = 1;
		this.tbmsmMobilePhase.Location = new System.Drawing.Point(72, 53);
		this.tbmsmMobilePhase.Name = "tbmsmMobilePhase";
		this.tbmsmMobilePhase.Size = new System.Drawing.Size(296, 21);
		this.tbmsmMobilePhase.TabIndex = 1;
		this.tbmsmColumn.Location = new System.Drawing.Point(72, 29);
		this.tbmsmColumn.Name = "tbmsmColumn";
		this.tbmsmColumn.Size = new System.Drawing.Size(296, 21);
		this.tbmsmColumn.TabIndex = 1;
		this.tbmsmMtdDspt.Location = new System.Drawing.Point(72, 5);
		this.tbmsmMtdDspt.Name = "tbmsmMtdDspt";
		this.tbmsmMtdDspt.Size = new System.Drawing.Size(296, 21);
		this.tbmsmMtdDspt.TabIndex = 1;
		this.lbmsmNote.AutoSize = true;
		this.lbmsmNote.Location = new System.Drawing.Point(9, 177);
		this.lbmsmNote.Name = "lbmsmNote";
		this.lbmsmNote.Size = new System.Drawing.Size(29, 12);
		this.lbmsmNote.TabIndex = 0;
		this.lbmsmNote.Text = "备注";
		this.lbmsmTemperature.AutoSize = true;
		this.lbmsmTemperature.Location = new System.Drawing.Point(9, 153);
		this.lbmsmTemperature.Name = "lbmsmTemperature";
		this.lbmsmTemperature.Size = new System.Drawing.Size(29, 12);
		this.lbmsmTemperature.TabIndex = 0;
		this.lbmsmTemperature.Text = "温度";
		this.lbmsmDetection.AutoSize = true;
		this.lbmsmDetection.Location = new System.Drawing.Point(9, 129);
		this.lbmsmDetection.Name = "lbmsmDetection";
		this.lbmsmDetection.Size = new System.Drawing.Size(29, 12);
		this.lbmsmDetection.TabIndex = 0;
		this.lbmsmDetection.Text = "检测";
		this.lbmsmPressure.AutoSize = true;
		this.lbmsmPressure.Location = new System.Drawing.Point(9, 105);
		this.lbmsmPressure.Name = "lbmsmPressure";
		this.lbmsmPressure.Size = new System.Drawing.Size(29, 12);
		this.lbmsmPressure.TabIndex = 0;
		this.lbmsmPressure.Text = "压力";
		this.lbmsmFlowRate.AutoSize = true;
		this.lbmsmFlowRate.Location = new System.Drawing.Point(9, 81);
		this.lbmsmFlowRate.Name = "lbmsmFlowRate";
		this.lbmsmFlowRate.Size = new System.Drawing.Size(29, 12);
		this.lbmsmFlowRate.TabIndex = 0;
		this.lbmsmFlowRate.Text = "流速";
		this.lbmsmMobilePhase.AutoSize = true;
		this.lbmsmMobilePhase.Location = new System.Drawing.Point(9, 57);
		this.lbmsmMobilePhase.Name = "lbmsmMobilePhase";
		this.lbmsmMobilePhase.Size = new System.Drawing.Size(41, 12);
		this.lbmsmMobilePhase.TabIndex = 0;
		this.lbmsmMobilePhase.Text = "流动相";
		this.lbmsmColumn.AutoSize = true;
		this.lbmsmColumn.Location = new System.Drawing.Point(9, 33);
		this.lbmsmColumn.Name = "lbmsmColumn";
		this.lbmsmColumn.Size = new System.Drawing.Size(41, 12);
		this.lbmsmColumn.TabIndex = 0;
		this.lbmsmColumn.Text = "色谱柱";
		this.lbmsmMtdDspt.AutoSize = true;
		this.lbmsmMtdDspt.Location = new System.Drawing.Point(9, 9);
		this.lbmsmMtdDspt.Name = "lbmsmMtdDspt";
		this.lbmsmMtdDspt.Size = new System.Drawing.Size(53, 12);
		this.lbmsmMtdDspt.TabIndex = 0;
		this.lbmsmMtdDspt.Text = "方法描述";
		this.tpAcquisition.Controls.Add(this.lbacqRate);
		this.tpAcquisition.Controls.Add(this.lbacqRange);
		this.tpAcquisition.Controls.Add(this.lbacqDetector);
		this.tpAcquisition.Controls.Add(this.cbacqRate);
		this.tpAcquisition.Controls.Add(this.cbacqRange);
		this.tpAcquisition.Location = new System.Drawing.Point(4, 4);
		this.tpAcquisition.Name = "tpAcquisition";
		this.tpAcquisition.Size = new System.Drawing.Size(530, 317);
		this.tpAcquisition.TabIndex = 5;
		this.tpAcquisition.Text = "采集";
		this.tpAcquisition.UseVisualStyleBackColor = true;
		this.lbacqRate.AutoSize = true;
		this.lbacqRate.Location = new System.Drawing.Point(35, 122);
		this.lbacqRate.Name = "lbacqRate";
		this.lbacqRate.Size = new System.Drawing.Size(77, 12);
		this.lbacqRate.TabIndex = 1;
		this.lbacqRate.Text = "采样频率[Hz]";
		this.lbacqRange.AutoSize = true;
		this.lbacqRange.Location = new System.Drawing.Point(35, 69);
		this.lbacqRange.Name = "lbacqRange";
		this.lbacqRange.Size = new System.Drawing.Size(29, 12);
		this.lbacqRange.TabIndex = 1;
		this.lbacqRange.Text = "范围";
		this.lbacqDetector.AutoSize = true;
		this.lbacqDetector.Location = new System.Drawing.Point(120, 28);
		this.lbacqDetector.Name = "lbacqDetector";
		this.lbacqDetector.Size = new System.Drawing.Size(59, 12);
		this.lbacqDetector.TabIndex = 1;
		this.lbacqDetector.Text = "***检测器";
		this.cbacqRate.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.cbacqRate.FormattingEnabled = true;
		this.cbacqRate.ItemExtString = "";
		this.cbacqRate.Location = new System.Drawing.Point(40, 140);
		this.cbacqRate.Name = "cbacqRate";
		this.cbacqRate.Size = new System.Drawing.Size(121, 20);
		this.cbacqRate.TabIndex = 2;
		this.cbacqRange.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.cbacqRange.FormattingEnabled = true;
		this.cbacqRange.ItemExtString = "";
		this.cbacqRange.Location = new System.Drawing.Point(40, 87);
		this.cbacqRange.Name = "cbacqRange";
		this.cbacqRange.Size = new System.Drawing.Size(121, 20);
		this.cbacqRange.TabIndex = 2;
		this.tpIntegration.Controls.Add(this.gvInteg);
		this.tpIntegration.Location = new System.Drawing.Point(4, 4);
		this.tpIntegration.Name = "tpIntegration";
		this.tpIntegration.Size = new System.Drawing.Size(530, 317);
		this.tpIntegration.TabIndex = 6;
		this.tpIntegration.Text = "积分";
		this.tpIntegration.UseVisualStyleBackColor = true;
		this.gvInteg.AllowUserToAddRows = false;
		this.gvInteg.AllowUserToDeleteRows = false;
		this.gvInteg.AllowUserToResizeRows = false;
		this.gvInteg.BackgroundColor = System.Drawing.Color.AliceBlue;
		this.gvInteg.CharacterHeaderColor = System.Drawing.Color.Black;
		this.gvInteg.ColumnHeadersHeight = 32;
		this.gvInteg.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
		this.gvInteg.ContextMenuStrip = this.cmsIntegration;
		this.gvInteg.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
		this.gvInteg.Location = new System.Drawing.Point(142, 69);
		this.gvInteg.Name = "gvInteg";
		this.gvInteg.RowHeadersWidth = 25;
		this.gvInteg.RowTemplate.Height = 16;
		this.gvInteg.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
		this.gvInteg.ShowCellToolTips = false;
		this.gvInteg.Size = new System.Drawing.Size(240, 150);
		this.gvInteg.TabIndex = 1;
		this.cmsIntegration.Items.AddRange(new System.Windows.Forms.ToolStripItem[8] { this.miIntegAppendRow, this.miIntegInsertRow, this.miIntegDeleteRows, this.toolStripSeparator1, this.miIntegRowsUp, this.miIntegRowsDown, this.toolStripSeparator2, this.miIntegResetRows });
		this.cmsIntegration.Name = "cmsIntegration";
		this.cmsIntegration.Size = new System.Drawing.Size(193, 148);
		this.cmsIntegration.Opening += new System.ComponentModel.CancelEventHandler(cmsIntegration_Opening);
		this.miIntegAppendRow.Name = "miIntegAppendRow";
		this.miIntegAppendRow.Size = new System.Drawing.Size(192, 22);
		this.miIntegAppendRow.Text = "toolStripMenuItem1";
		this.miIntegAppendRow.Click += new System.EventHandler(miIntegAppendRow_Click);
		this.miIntegInsertRow.Name = "miIntegInsertRow";
		this.miIntegInsertRow.Size = new System.Drawing.Size(192, 22);
		this.miIntegInsertRow.Text = "toolStripMenuItem1";
		this.miIntegInsertRow.Click += new System.EventHandler(miIntegInsertRow_Click);
		this.miIntegDeleteRows.Name = "miIntegDeleteRows";
		this.miIntegDeleteRows.Size = new System.Drawing.Size(192, 22);
		this.miIntegDeleteRows.Text = "toolStripMenuItem2";
		this.miIntegDeleteRows.Click += new System.EventHandler(miIntegDeleteRows_Click);
		this.toolStripSeparator1.Name = "toolStripSeparator1";
		this.toolStripSeparator1.Size = new System.Drawing.Size(189, 6);
		this.miIntegRowsUp.Name = "miIntegRowsUp";
		this.miIntegRowsUp.Size = new System.Drawing.Size(192, 22);
		this.miIntegRowsUp.Text = "toolStripMenuItem3";
		this.miIntegRowsUp.Click += new System.EventHandler(miIntegRowsUp_Click);
		this.miIntegRowsDown.Name = "miIntegRowsDown";
		this.miIntegRowsDown.Size = new System.Drawing.Size(192, 22);
		this.miIntegRowsDown.Text = "toolStripMenuItem4";
		this.miIntegRowsDown.Click += new System.EventHandler(miIntegRowsDown_Click);
		this.toolStripSeparator2.Name = "toolStripSeparator2";
		this.toolStripSeparator2.Size = new System.Drawing.Size(189, 6);
		this.miIntegResetRows.Name = "miIntegResetRows";
		this.miIntegResetRows.Size = new System.Drawing.Size(192, 22);
		this.miIntegResetRows.Text = "toolStripMenuItem5";
		this.miIntegResetRows.Click += new System.EventHandler(miIntegResetRows_Click);
		this.tpCaculation.Controls.Add(this.cbcclCalcu);
		this.tpCaculation.Controls.Add(this.gbcclRltTableReport);
		this.tpCaculation.Controls.Add(this.gbcclParas);
		this.tpCaculation.Controls.Add(this.btncclSet);
		this.tpCaculation.Controls.Add(this.btncclNone);
		this.tpCaculation.Controls.Add(this.btncclView);
		this.tpCaculation.Controls.Add(this.tbcclCalibration);
		this.tpCaculation.Controls.Add(this.lbcclModifiedTimeV);
		this.tpCaculation.Controls.Add(this.lbcclModifiedTime);
		this.tpCaculation.Controls.Add(this.lbcclCreateTimeV);
		this.tpCaculation.Controls.Add(this.lbcclCreateTime);
		this.tpCaculation.Controls.Add(this.lbcclDescriptionV);
		this.tpCaculation.Controls.Add(this.lbcclDescription);
		this.tpCaculation.Controls.Add(this.lbcclAuthorV);
		this.tpCaculation.Controls.Add(this.lbcclAuthor);
		this.tpCaculation.Controls.Add(this.lbcclCalcu);
		this.tpCaculation.Controls.Add(this.lbcclCalibration);
		this.tpCaculation.Location = new System.Drawing.Point(4, 4);
		this.tpCaculation.Name = "tpCaculation";
		this.tpCaculation.Size = new System.Drawing.Size(530, 317);
		this.tpCaculation.TabIndex = 7;
		this.tpCaculation.Text = "计算";
		this.tpCaculation.UseVisualStyleBackColor = true;
		this.cbcclCalcu.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.cbcclCalcu.FormattingEnabled = true;
		this.cbcclCalcu.ItemExtString = "";
		this.cbcclCalcu.Location = new System.Drawing.Point(72, 84);
		this.cbcclCalcu.Name = "cbcclCalcu";
		this.cbcclCalcu.Size = new System.Drawing.Size(85, 20);
		this.cbcclCalcu.TabIndex = 6;
		this.gbcclRltTableReport.Controls.Add(this.rbrtrCaliPeaks);
		this.gbcclRltTableReport.Controls.Add(this.rbrtrIdentifiedPeaks);
		this.gbcclRltTableReport.Controls.Add(this.rbrtrAllDetectedPeaks);
		this.gbcclRltTableReport.Controls.Add(this.cbrtrHideISTDPeak);
		this.gbcclRltTableReport.Location = new System.Drawing.Point(325, 155);
		this.gbcclRltTableReport.Name = "gbcclRltTableReport";
		this.gbcclRltTableReport.Size = new System.Drawing.Size(201, 76);
		this.gbcclRltTableReport.TabIndex = 5;
		this.gbcclRltTableReport.TabStop = false;
		this.gbcclRltTableReport.Text = "结果表报告";
		this.rbrtrCaliPeaks.AutoSize = true;
		this.rbrtrCaliPeaks.Location = new System.Drawing.Point(103, 55);
		this.rbrtrCaliPeaks.Name = "rbrtrCaliPeaks";
		this.rbrtrCaliPeaks.Size = new System.Drawing.Size(83, 16);
		this.rbrtrCaliPeaks.TabIndex = 0;
		this.rbrtrCaliPeaks.TabStop = true;
		this.rbrtrCaliPeaks.Text = "所有校正峰";
		this.rbrtrCaliPeaks.UseVisualStyleBackColor = true;
		this.rbrtrIdentifiedPeaks.AutoSize = true;
		this.rbrtrIdentifiedPeaks.Location = new System.Drawing.Point(103, 35);
		this.rbrtrIdentifiedPeaks.Name = "rbrtrIdentifiedPeaks";
		this.rbrtrIdentifiedPeaks.Size = new System.Drawing.Size(83, 16);
		this.rbrtrIdentifiedPeaks.TabIndex = 0;
		this.rbrtrIdentifiedPeaks.TabStop = true;
		this.rbrtrIdentifiedPeaks.Text = "所有识别峰";
		this.rbrtrIdentifiedPeaks.UseVisualStyleBackColor = true;
		this.rbrtrAllDetectedPeaks.AutoSize = true;
		this.rbrtrAllDetectedPeaks.Location = new System.Drawing.Point(103, 15);
		this.rbrtrAllDetectedPeaks.Name = "rbrtrAllDetectedPeaks";
		this.rbrtrAllDetectedPeaks.Size = new System.Drawing.Size(83, 16);
		this.rbrtrAllDetectedPeaks.TabIndex = 0;
		this.rbrtrAllDetectedPeaks.TabStop = true;
		this.rbrtrAllDetectedPeaks.Text = "所有检测峰";
		this.rbrtrAllDetectedPeaks.UseVisualStyleBackColor = true;
		this.cbrtrHideISTDPeak.AutoSize = true;
		this.cbrtrHideISTDPeak.Location = new System.Drawing.Point(6, 16);
		this.cbrtrHideISTDPeak.Name = "cbrtrHideISTDPeak";
		this.cbrtrHideISTDPeak.Size = new System.Drawing.Size(84, 16);
		this.cbrtrHideISTDPeak.TabIndex = 3;
		this.cbrtrHideISTDPeak.Text = "隐藏内标峰";
		this.cbrtrHideISTDPeak.UseVisualStyleBackColor = true;
		this.gbcclParas.Controls.Add(this.cbprsUncalBase);
		this.gbcclParas.Controls.Add(this.cbprsUseScaleFactor);
		this.gbcclParas.Controls.Add(this.lbprsUncalBase);
		this.gbcclParas.Controls.Add(this.lbprsUncalAmtRespFU);
		this.gbcclParas.Controls.Add(this.lbprsUncalAmtRespF);
		this.gbcclParas.Controls.Add(this.lbprsUnitAfterScale);
		this.gbcclParas.Controls.Add(this.lbprsScaleFactor);
		this.gbcclParas.Controls.Add(this.tbprsUncalAmtRespF);
		this.gbcclParas.Controls.Add(this.tbprsUnitAfterScale);
		this.gbcclParas.Controls.Add(this.tbprsScaleFactor);
		this.gbcclParas.Location = new System.Drawing.Point(325, 7);
		this.gbcclParas.Name = "gbcclParas";
		this.gbcclParas.Size = new System.Drawing.Size(201, 142);
		this.gbcclParas.TabIndex = 4;
		this.gbcclParas.TabStop = false;
		this.gbcclParas.Text = "参数";
		this.gbcclParas.Paint += new System.Windows.Forms.PaintEventHandler(gbcclParas_Paint);
		this.cbprsUncalBase.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.cbprsUncalBase.FormattingEnabled = true;
		this.cbprsUncalBase.ItemExtString = "";
		this.cbprsUncalBase.Location = new System.Drawing.Point(74, 92);
		this.cbprsUncalBase.Name = "cbprsUncalBase";
		this.cbprsUncalBase.Size = new System.Drawing.Size(60, 20);
		this.cbprsUncalBase.TabIndex = 4;
		this.cbprsUseScaleFactor.AutoSize = true;
		this.cbprsUseScaleFactor.Location = new System.Drawing.Point(6, 16);
		this.cbprsUseScaleFactor.Name = "cbprsUseScaleFactor";
		this.cbprsUseScaleFactor.Size = new System.Drawing.Size(96, 16);
		this.cbprsUseScaleFactor.TabIndex = 3;
		this.cbprsUseScaleFactor.Text = "使用缩放因子";
		this.cbprsUseScaleFactor.UseVisualStyleBackColor = true;
		this.cbprsUseScaleFactor.Click += new System.EventHandler(cbprsUseScaleFactor_Click);
		this.lbprsUncalBase.AutoSize = true;
		this.lbprsUncalBase.Location = new System.Drawing.Point(6, 96);
		this.lbprsUncalBase.Name = "lbprsUncalBase";
		this.lbprsUncalBase.Size = new System.Drawing.Size(65, 12);
		this.lbprsUncalBase.TabIndex = 0;
		this.lbprsUncalBase.Text = "未识别响应";
		this.lbprsUncalAmtRespFU.AutoSize = true;
		this.lbprsUncalAmtRespFU.Location = new System.Drawing.Point(135, 119);
		this.lbprsUncalAmtRespFU.Name = "lbprsUncalAmtRespFU";
		this.lbprsUncalAmtRespFU.Size = new System.Drawing.Size(65, 12);
		this.lbprsUncalAmtRespFU.TabIndex = 0;
		this.lbprsUncalAmtRespFU.Text = "[Amt/Resp]";
		this.lbprsUncalAmtRespF.AutoSize = true;
		this.lbprsUncalAmtRespF.Location = new System.Drawing.Point(6, 119);
		this.lbprsUncalAmtRespF.Name = "lbprsUncalAmtRespF";
		this.lbprsUncalAmtRespF.Size = new System.Drawing.Size(65, 12);
		this.lbprsUncalAmtRespF.TabIndex = 0;
		this.lbprsUncalAmtRespF.Text = "未识别因子";
		this.lbprsUnitAfterScale.AutoSize = true;
		this.lbprsUnitAfterScale.Location = new System.Drawing.Point(6, 64);
		this.lbprsUnitAfterScale.Name = "lbprsUnitAfterScale";
		this.lbprsUnitAfterScale.Size = new System.Drawing.Size(65, 12);
		this.lbprsUnitAfterScale.TabIndex = 0;
		this.lbprsUnitAfterScale.Text = "缩放后单位";
		this.lbprsScaleFactor.AutoSize = true;
		this.lbprsScaleFactor.Location = new System.Drawing.Point(6, 39);
		this.lbprsScaleFactor.Name = "lbprsScaleFactor";
		this.lbprsScaleFactor.Size = new System.Drawing.Size(53, 12);
		this.lbprsScaleFactor.TabIndex = 0;
		this.lbprsScaleFactor.Text = "缩放因子";
		this.tbprsUncalAmtRespF.Location = new System.Drawing.Point(75, 115);
		this.tbprsUncalAmtRespF.Name = "tbprsUncalAmtRespF";
		this.tbprsUncalAmtRespF.Size = new System.Drawing.Size(59, 21);
		this.tbprsUncalAmtRespF.TabIndex = 1;
		this.tbprsUnitAfterScale.Location = new System.Drawing.Point(75, 60);
		this.tbprsUnitAfterScale.Name = "tbprsUnitAfterScale";
		this.tbprsUnitAfterScale.Size = new System.Drawing.Size(59, 21);
		this.tbprsUnitAfterScale.TabIndex = 1;
		this.tbprsScaleFactor.Location = new System.Drawing.Point(75, 36);
		this.tbprsScaleFactor.Name = "tbprsScaleFactor";
		this.tbprsScaleFactor.Size = new System.Drawing.Size(59, 21);
		this.tbprsScaleFactor.TabIndex = 1;
		this.btncclSet.Location = new System.Drawing.Point(163, 46);
		this.btncclSet.Name = "btncclSet";
		this.btncclSet.Size = new System.Drawing.Size(75, 23);
		this.btncclSet.TabIndex = 3;
		this.btncclSet.Text = "设置...";
		this.btncclSet.UseVisualStyleBackColor = true;
		this.btncclSet.Click += new System.EventHandler(btncclView_Click);
		this.btncclNone.Location = new System.Drawing.Point(244, 46);
		this.btncclNone.Name = "btncclNone";
		this.btncclNone.Size = new System.Drawing.Size(75, 23);
		this.btncclNone.TabIndex = 3;
		this.btncclNone.Text = "置空";
		this.btncclNone.UseVisualStyleBackColor = true;
		this.btncclNone.Click += new System.EventHandler(btncclView_Click);
		this.btncclView.Location = new System.Drawing.Point(244, 21);
		this.btncclView.Name = "btncclView";
		this.btncclView.Size = new System.Drawing.Size(75, 23);
		this.btncclView.TabIndex = 3;
		this.btncclView.Text = "查看";
		this.btncclView.UseVisualStyleBackColor = true;
		this.btncclView.Click += new System.EventHandler(btncclView_Click);
		this.tbcclCalibration.Location = new System.Drawing.Point(7, 22);
		this.tbcclCalibration.Name = "tbcclCalibration";
		this.tbcclCalibration.ReadOnly = true;
		this.tbcclCalibration.Size = new System.Drawing.Size(231, 21);
		this.tbcclCalibration.TabIndex = 2;
		this.lbcclModifiedTimeV.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
		this.lbcclModifiedTimeV.Location = new System.Drawing.Point(72, 225);
		this.lbcclModifiedTimeV.Name = "lbcclModifiedTimeV";
		this.lbcclModifiedTimeV.Size = new System.Drawing.Size(247, 19);
		this.lbcclModifiedTimeV.TabIndex = 1;
		this.lbcclModifiedTimeV.Text = "lclLabel1";
		this.lbcclModifiedTimeV.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.lbcclModifiedTime.AutoSize = true;
		this.lbcclModifiedTime.Location = new System.Drawing.Point(3, 228);
		this.lbcclModifiedTime.Name = "lbcclModifiedTime";
		this.lbcclModifiedTime.Size = new System.Drawing.Size(53, 12);
		this.lbcclModifiedTime.TabIndex = 1;
		this.lbcclModifiedTime.Text = "修改时间";
		this.lbcclCreateTimeV.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
		this.lbcclCreateTimeV.Location = new System.Drawing.Point(72, 202);
		this.lbcclCreateTimeV.Name = "lbcclCreateTimeV";
		this.lbcclCreateTimeV.Size = new System.Drawing.Size(247, 19);
		this.lbcclCreateTimeV.TabIndex = 1;
		this.lbcclCreateTimeV.Text = "lclLabel1";
		this.lbcclCreateTimeV.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.lbcclCreateTime.AutoSize = true;
		this.lbcclCreateTime.Location = new System.Drawing.Point(3, 206);
		this.lbcclCreateTime.Name = "lbcclCreateTime";
		this.lbcclCreateTime.Size = new System.Drawing.Size(53, 12);
		this.lbcclCreateTime.TabIndex = 1;
		this.lbcclCreateTime.Text = "创建时间";
		this.lbcclDescriptionV.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
		this.lbcclDescriptionV.Location = new System.Drawing.Point(72, 131);
		this.lbcclDescriptionV.Name = "lbcclDescriptionV";
		this.lbcclDescriptionV.Size = new System.Drawing.Size(247, 67);
		this.lbcclDescriptionV.TabIndex = 1;
		this.lbcclDescriptionV.Text = "lclLabel1";
		this.lbcclDescription.AutoSize = true;
		this.lbcclDescription.Location = new System.Drawing.Point(3, 135);
		this.lbcclDescription.Name = "lbcclDescription";
		this.lbcclDescription.Size = new System.Drawing.Size(29, 12);
		this.lbcclDescription.TabIndex = 1;
		this.lbcclDescription.Text = "描述";
		this.lbcclAuthorV.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
		this.lbcclAuthorV.Location = new System.Drawing.Point(72, 108);
		this.lbcclAuthorV.Name = "lbcclAuthorV";
		this.lbcclAuthorV.Size = new System.Drawing.Size(247, 19);
		this.lbcclAuthorV.TabIndex = 1;
		this.lbcclAuthorV.Text = "lclLabel1";
		this.lbcclAuthorV.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.lbcclAuthor.AutoSize = true;
		this.lbcclAuthor.Location = new System.Drawing.Point(3, 111);
		this.lbcclAuthor.Name = "lbcclAuthor";
		this.lbcclAuthor.Size = new System.Drawing.Size(29, 12);
		this.lbcclAuthor.TabIndex = 1;
		this.lbcclAuthor.Text = "作者";
		this.lbcclCalcu.AutoSize = true;
		this.lbcclCalcu.Location = new System.Drawing.Point(5, 87);
		this.lbcclCalcu.Name = "lbcclCalcu";
		this.lbcclCalcu.Size = new System.Drawing.Size(29, 12);
		this.lbcclCalcu.TabIndex = 1;
		this.lbcclCalcu.Text = "计算";
		this.lbcclCalibration.AutoSize = true;
		this.lbcclCalibration.Location = new System.Drawing.Point(5, 7);
		this.lbcclCalibration.Name = "lbcclCalibration";
		this.lbcclCalibration.Size = new System.Drawing.Size(89, 12);
		this.lbcclCalibration.TabIndex = 1;
		this.lbcclCalibration.Text = "校正文件[峰表]";
		this.tpAdvanced.Controls.Add(this.gbadvColumnCalcu);
		this.tpAdvanced.Controls.Add(this.gbadvAddSub);
		this.tpAdvanced.Location = new System.Drawing.Point(4, 4);
		this.tpAdvanced.Name = "tpAdvanced";
		this.tpAdvanced.Size = new System.Drawing.Size(530, 317);
		this.tpAdvanced.TabIndex = 10;
		this.tpAdvanced.Text = "高级";
		this.tpAdvanced.UseVisualStyleBackColor = true;
		this.gbadvColumnCalcu.Controls.Add(this.rbccFrom50per);
		this.gbadvColumnCalcu.Controls.Add(this.rbccStatistical);
		this.gbadvColumnCalcu.Controls.Add(this.tbccColumnLength);
		this.gbadvColumnCalcu.Controls.Add(this.lbccColumnLengthU);
		this.gbadvColumnCalcu.Controls.Add(this.tbccUnretainedPeak);
		this.gbadvColumnCalcu.Controls.Add(this.lbccColumnLength);
		this.gbadvColumnCalcu.Controls.Add(this.lbccUnretainedPeakU);
		this.gbadvColumnCalcu.Controls.Add(this.lbccUnretainedPeak);
		this.gbadvColumnCalcu.Location = new System.Drawing.Point(5, 126);
		this.gbadvColumnCalcu.Name = "gbadvColumnCalcu";
		this.gbadvColumnCalcu.Size = new System.Drawing.Size(238, 108);
		this.gbadvColumnCalcu.TabIndex = 0;
		this.gbadvColumnCalcu.TabStop = false;
		this.gbadvColumnCalcu.Text = "柱效计算";
		this.rbccFrom50per.AutoSize = true;
		this.rbccFrom50per.Checked = true;
		this.rbccFrom50per.Location = new System.Drawing.Point(32, 87);
		this.rbccFrom50per.Name = "rbccFrom50per";
		this.rbccFrom50per.Size = new System.Drawing.Size(77, 16);
		this.rbccFrom50per.TabIndex = 1;
		this.rbccFrom50per.TabStop = true;
		this.rbccFrom50per.Text = "50%宽起始";
		this.rbccFrom50per.UseVisualStyleBackColor = true;
		this.rbccStatistical.AutoSize = true;
		this.rbccStatistical.Enabled = false;
		this.rbccStatistical.Location = new System.Drawing.Point(32, 67);
		this.rbccStatistical.Name = "rbccStatistical";
		this.rbccStatistical.Size = new System.Drawing.Size(71, 16);
		this.rbccStatistical.TabIndex = 1;
		this.rbccStatistical.Text = "静态时间";
		this.rbccStatistical.UseVisualStyleBackColor = true;
		this.tbccColumnLength.Location = new System.Drawing.Point(122, 42);
		this.tbccColumnLength.Name = "tbccColumnLength";
		this.tbccColumnLength.Size = new System.Drawing.Size(59, 21);
		this.tbccColumnLength.TabIndex = 1;
		this.lbccColumnLengthU.AutoSize = true;
		this.lbccColumnLengthU.Location = new System.Drawing.Point(187, 46);
		this.lbccColumnLengthU.Name = "lbccColumnLengthU";
		this.lbccColumnLengthU.Size = new System.Drawing.Size(29, 12);
		this.lbccColumnLengthU.TabIndex = 1;
		this.lbccColumnLengthU.Text = "[mm]";
		this.tbccUnretainedPeak.Location = new System.Drawing.Point(122, 18);
		this.tbccUnretainedPeak.Name = "tbccUnretainedPeak";
		this.tbccUnretainedPeak.Size = new System.Drawing.Size(59, 21);
		this.tbccUnretainedPeak.TabIndex = 1;
		this.lbccColumnLength.AutoSize = true;
		this.lbccColumnLength.Location = new System.Drawing.Point(6, 46);
		this.lbccColumnLength.Name = "lbccColumnLength";
		this.lbccColumnLength.Size = new System.Drawing.Size(29, 12);
		this.lbccColumnLength.TabIndex = 1;
		this.lbccColumnLength.Text = "柱长";
		this.lbccUnretainedPeakU.AutoSize = true;
		this.lbccUnretainedPeakU.Location = new System.Drawing.Point(187, 22);
		this.lbccUnretainedPeakU.Name = "lbccUnretainedPeakU";
		this.lbccUnretainedPeakU.Size = new System.Drawing.Size(35, 12);
		this.lbccUnretainedPeakU.TabIndex = 1;
		this.lbccUnretainedPeakU.Text = "[min]";
		this.lbccUnretainedPeak.AutoSize = true;
		this.lbccUnretainedPeak.Location = new System.Drawing.Point(6, 22);
		this.lbccUnretainedPeak.Name = "lbccUnretainedPeak";
		this.lbccUnretainedPeak.Size = new System.Drawing.Size(77, 12);
		this.lbccUnretainedPeak.TabIndex = 1;
		this.lbccUnretainedPeak.Text = "非保留峰时间";
		this.gbadvAddSub.Controls.Add(this.rbasSub);
		this.gbadvAddSub.Controls.Add(this.rbasAdd);
		this.gbadvAddSub.Controls.Add(this.cbasMatching);
		this.gbadvAddSub.Controls.Add(this.btnasSetChrom);
		this.gbadvAddSub.Controls.Add(this.btnasNoneChrom);
		this.gbadvAddSub.Controls.Add(this.tbasChrom);
		this.gbadvAddSub.Controls.Add(this.lbasMatching);
		this.gbadvAddSub.Controls.Add(this.lbasChrom);
		this.gbadvAddSub.Location = new System.Drawing.Point(5, 6);
		this.gbadvAddSub.Name = "gbadvAddSub";
		this.gbadvAddSub.Size = new System.Drawing.Size(277, 114);
		this.gbadvAddSub.TabIndex = 0;
		this.gbadvAddSub.TabStop = false;
		this.gbadvAddSub.Text = "加减谱图";
		this.rbasSub.AutoSize = true;
		this.rbasSub.Location = new System.Drawing.Point(138, 17);
		this.rbasSub.Name = "rbasSub";
		this.rbasSub.Size = new System.Drawing.Size(35, 16);
		this.rbasSub.TabIndex = 5;
		this.rbasSub.TabStop = true;
		this.rbasSub.Text = "减";
		this.rbasSub.UseVisualStyleBackColor = true;
		this.rbasAdd.AutoSize = true;
		this.rbasAdd.Location = new System.Drawing.Point(70, 17);
		this.rbasAdd.Name = "rbasAdd";
		this.rbasAdd.Size = new System.Drawing.Size(35, 16);
		this.rbasAdd.TabIndex = 5;
		this.rbasAdd.TabStop = true;
		this.rbasAdd.Text = "加";
		this.rbasAdd.UseVisualStyleBackColor = true;
		this.cbasMatching.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.cbasMatching.FormattingEnabled = true;
		this.cbasMatching.ItemExtString = "";
		this.cbasMatching.Location = new System.Drawing.Point(70, 63);
		this.cbasMatching.Name = "cbasMatching";
		this.cbasMatching.Size = new System.Drawing.Size(202, 20);
		this.cbasMatching.TabIndex = 4;
		this.btnasSetChrom.Location = new System.Drawing.Point(116, 86);
		this.btnasSetChrom.Name = "btnasSetChrom";
		this.btnasSetChrom.Size = new System.Drawing.Size(75, 23);
		this.btnasSetChrom.TabIndex = 3;
		this.btnasSetChrom.Text = "设置...";
		this.btnasSetChrom.UseVisualStyleBackColor = true;
		this.btnasSetChrom.Click += new System.EventHandler(btnasNoneChrom_Click);
		this.btnasNoneChrom.Location = new System.Drawing.Point(197, 86);
		this.btnasNoneChrom.Name = "btnasNoneChrom";
		this.btnasNoneChrom.Size = new System.Drawing.Size(75, 23);
		this.btnasNoneChrom.TabIndex = 3;
		this.btnasNoneChrom.Text = "置空";
		this.btnasNoneChrom.UseVisualStyleBackColor = true;
		this.btnasNoneChrom.Click += new System.EventHandler(btnasNoneChrom_Click);
		this.tbasChrom.Location = new System.Drawing.Point(70, 38);
		this.tbasChrom.Name = "tbasChrom";
		this.tbasChrom.ReadOnly = true;
		this.tbasChrom.Size = new System.Drawing.Size(202, 21);
		this.tbasChrom.TabIndex = 1;
		this.lbasMatching.AutoSize = true;
		this.lbasMatching.Location = new System.Drawing.Point(6, 67);
		this.lbasMatching.Name = "lbasMatching";
		this.lbasMatching.Size = new System.Drawing.Size(53, 12);
		this.lbasMatching.TabIndex = 1;
		this.lbasMatching.Text = "匹配方式";
		this.lbasChrom.AutoSize = true;
		this.lbasChrom.Location = new System.Drawing.Point(6, 42);
		this.lbasChrom.Name = "lbasChrom";
		this.lbasChrom.Size = new System.Drawing.Size(29, 12);
		this.lbasChrom.TabIndex = 1;
		this.lbasChrom.Text = "谱图";
		this.tpPDA.Controls.Add(this.gvpdaLibs);
		this.tpPDA.Controls.Add(this.gbpdaLibSearchOptions);
		this.tpPDA.Controls.Add(this.gbpdaPeakPurityOptions);
		this.tpPDA.Location = new System.Drawing.Point(4, 4);
		this.tpPDA.Name = "tpPDA";
		this.tpPDA.Size = new System.Drawing.Size(530, 317);
		this.tpPDA.TabIndex = 11;
		this.tpPDA.Text = "PDA";
		this.tpPDA.UseVisualStyleBackColor = true;
		this.gvpdaLibs.AllowUserToAddRows = false;
		this.gvpdaLibs.AllowUserToDeleteRows = false;
		this.gvpdaLibs.AllowUserToResizeRows = false;
		this.gvpdaLibs.BackgroundColor = System.Drawing.Color.AliceBlue;
		this.gvpdaLibs.CharacterHeaderColor = System.Drawing.Color.Black;
		this.gvpdaLibs.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
		this.gvpdaLibs.ContextMenuStrip = this.cmsLibs;
		this.gvpdaLibs.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
		this.gvpdaLibs.Location = new System.Drawing.Point(7, 229);
		this.gvpdaLibs.Name = "gvpdaLibs";
		this.gvpdaLibs.RowHeadersWidth = 25;
		this.gvpdaLibs.RowTemplate.Height = 16;
		this.gvpdaLibs.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
		this.gvpdaLibs.ShowCellToolTips = false;
		this.gvpdaLibs.Size = new System.Drawing.Size(352, 71);
		this.gvpdaLibs.TabIndex = 0;
		this.cmsLibs.Items.AddRange(new System.Windows.Forms.ToolStripItem[2] { this.miAddRow, this.miDeleteRow });
		this.cmsLibs.Name = "cmsLibs";
		this.cmsLibs.ShowImageMargin = false;
		this.cmsLibs.Size = new System.Drawing.Size(168, 48);
		this.miAddRow.Name = "miAddRow";
		this.miAddRow.Size = new System.Drawing.Size(167, 22);
		this.miAddRow.Text = "toolStripMenuItem1";
		this.miDeleteRow.Name = "miDeleteRow";
		this.miDeleteRow.Size = new System.Drawing.Size(167, 22);
		this.miDeleteRow.Text = "toolStripMenuItem2";
		this.gbpdaLibSearchOptions.Controls.Add(this.cblsoMatchCriteria);
		this.gbpdaLibSearchOptions.Controls.Add(this.tblsoRestrictRT);
		this.gbpdaLibSearchOptions.Controls.Add(this.tblsoMaxNumHits);
		this.gbpdaLibSearchOptions.Controls.Add(this.tblsoMatchFactorThreshold);
		this.gbpdaLibSearchOptions.Controls.Add(this.tblsoFrom);
		this.gbpdaLibSearchOptions.Controls.Add(this.lclLabel42);
		this.gbpdaLibSearchOptions.Controls.Add(this.tblsoTo);
		this.gbpdaLibSearchOptions.Controls.Add(this.lclLabel43);
		this.gbpdaLibSearchOptions.Controls.Add(this.lclLabel44);
		this.gbpdaLibSearchOptions.Controls.Add(this.lblsoMaxNumHits);
		this.gbpdaLibSearchOptions.Controls.Add(this.lblsoTo);
		this.gbpdaLibSearchOptions.Controls.Add(this.lblsoMatchCriteria);
		this.gbpdaLibSearchOptions.Controls.Add(this.lblsoMatchFactorThreshold);
		this.gbpdaLibSearchOptions.Controls.Add(this.lblsoFrom);
		this.gbpdaLibSearchOptions.Controls.Add(this.cblsoForAllDetectedPeaks);
		this.gbpdaLibSearchOptions.Controls.Add(this.cblsoUseBackCorr);
		this.gbpdaLibSearchOptions.Controls.Add(this.cblsoRestrictRT);
		this.gbpdaLibSearchOptions.Controls.Add(this.cblsoRestrictWaveLength);
		this.gbpdaLibSearchOptions.Location = new System.Drawing.Point(278, 4);
		this.gbpdaLibSearchOptions.Name = "gbpdaLibSearchOptions";
		this.gbpdaLibSearchOptions.Size = new System.Drawing.Size(264, 219);
		this.gbpdaLibSearchOptions.TabIndex = 3;
		this.gbpdaLibSearchOptions.TabStop = false;
		this.gbpdaLibSearchOptions.Text = "库分析选项";
		this.cblsoMatchCriteria.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.cblsoMatchCriteria.FormattingEnabled = true;
		this.cblsoMatchCriteria.ItemExtString = "";
		this.cblsoMatchCriteria.Location = new System.Drawing.Point(134, 19);
		this.cblsoMatchCriteria.Name = "cblsoMatchCriteria";
		this.cblsoMatchCriteria.Size = new System.Drawing.Size(121, 20);
		this.cblsoMatchCriteria.TabIndex = 3;
		this.tblsoRestrictRT.Location = new System.Drawing.Point(135, 143);
		this.tblsoRestrictRT.Name = "tblsoRestrictRT";
		this.tblsoRestrictRT.Size = new System.Drawing.Size(57, 21);
		this.tblsoRestrictRT.TabIndex = 2;
		this.tblsoMaxNumHits.Location = new System.Drawing.Point(135, 72);
		this.tblsoMaxNumHits.Name = "tblsoMaxNumHits";
		this.tblsoMaxNumHits.Size = new System.Drawing.Size(57, 21);
		this.tblsoMaxNumHits.TabIndex = 2;
		this.tblsoMatchFactorThreshold.Location = new System.Drawing.Point(135, 45);
		this.tblsoMatchFactorThreshold.Name = "tblsoMatchFactorThreshold";
		this.tblsoMatchFactorThreshold.Size = new System.Drawing.Size(57, 21);
		this.tblsoMatchFactorThreshold.TabIndex = 2;
		this.tblsoFrom.Location = new System.Drawing.Point(90, 118);
		this.tblsoFrom.Name = "tblsoFrom";
		this.tblsoFrom.Size = new System.Drawing.Size(52, 21);
		this.tblsoFrom.TabIndex = 2;
		this.lclLabel42.AutoSize = true;
		this.lclLabel42.Location = new System.Drawing.Point(198, 146);
		this.lclLabel42.Name = "lclLabel42";
		this.lclLabel42.Size = new System.Drawing.Size(11, 12);
		this.lclLabel42.TabIndex = 1;
		this.lclLabel42.Text = "%";
		this.tblsoTo.Location = new System.Drawing.Point(180, 118);
		this.tblsoTo.Name = "tblsoTo";
		this.tblsoTo.Size = new System.Drawing.Size(52, 21);
		this.tblsoTo.TabIndex = 2;
		this.lclLabel43.AutoSize = true;
		this.lclLabel43.Location = new System.Drawing.Point(238, 121);
		this.lclLabel43.Name = "lclLabel43";
		this.lclLabel43.Size = new System.Drawing.Size(17, 12);
		this.lclLabel43.TabIndex = 1;
		this.lclLabel43.Text = "nm";
		this.lclLabel44.AutoSize = true;
		this.lclLabel44.Location = new System.Drawing.Point(198, 48);
		this.lclLabel44.Name = "lclLabel44";
		this.lclLabel44.Size = new System.Drawing.Size(59, 12);
		this.lclLabel44.TabIndex = 1;
		this.lclLabel44.Text = "(0..1000)";
		this.lblsoMaxNumHits.AutoSize = true;
		this.lblsoMaxNumHits.Location = new System.Drawing.Point(6, 75);
		this.lblsoMaxNumHits.Name = "lblsoMaxNumHits";
		this.lblsoMaxNumHits.Size = new System.Drawing.Size(77, 12);
		this.lblsoMaxNumHits.TabIndex = 1;
		this.lblsoMaxNumHits.Text = "最大显示波数";
		this.lblsoTo.AutoSize = true;
		this.lblsoTo.Location = new System.Drawing.Point(148, 121);
		this.lblsoTo.Name = "lblsoTo";
		this.lblsoTo.Size = new System.Drawing.Size(23, 12);
		this.lblsoTo.TabIndex = 1;
		this.lblsoTo.Text = "到:";
		this.lblsoMatchCriteria.AutoSize = true;
		this.lblsoMatchCriteria.Location = new System.Drawing.Point(6, 21);
		this.lblsoMatchCriteria.Name = "lblsoMatchCriteria";
		this.lblsoMatchCriteria.Size = new System.Drawing.Size(53, 12);
		this.lblsoMatchCriteria.TabIndex = 1;
		this.lblsoMatchCriteria.Text = "匹配规则";
		this.lblsoMatchFactorThreshold.AutoSize = true;
		this.lblsoMatchFactorThreshold.Location = new System.Drawing.Point(6, 48);
		this.lblsoMatchFactorThreshold.Name = "lblsoMatchFactorThreshold";
		this.lblsoMatchFactorThreshold.Size = new System.Drawing.Size(77, 12);
		this.lblsoMatchFactorThreshold.TabIndex = 1;
		this.lblsoMatchFactorThreshold.Text = "匹配因子极限";
		this.lblsoFrom.AutoSize = true;
		this.lblsoFrom.Location = new System.Drawing.Point(50, 121);
		this.lblsoFrom.Name = "lblsoFrom";
		this.lblsoFrom.Size = new System.Drawing.Size(23, 12);
		this.lblsoFrom.TabIndex = 1;
		this.lblsoFrom.Text = "从:";
		this.cblsoForAllDetectedPeaks.AutoSize = true;
		this.cblsoForAllDetectedPeaks.Location = new System.Drawing.Point(6, 189);
		this.cblsoForAllDetectedPeaks.Name = "cblsoForAllDetectedPeaks";
		this.cblsoForAllDetectedPeaks.Size = new System.Drawing.Size(84, 16);
		this.cblsoForAllDetectedPeaks.TabIndex = 0;
		this.cblsoForAllDetectedPeaks.Text = "所有检测峰";
		this.cblsoForAllDetectedPeaks.UseVisualStyleBackColor = true;
		this.cblsoUseBackCorr.AutoSize = true;
		this.cblsoUseBackCorr.Location = new System.Drawing.Point(6, 167);
		this.cblsoUseBackCorr.Name = "cblsoUseBackCorr";
		this.cblsoUseBackCorr.Size = new System.Drawing.Size(96, 16);
		this.cblsoUseBackCorr.TabIndex = 0;
		this.cblsoUseBackCorr.Text = "使用背景修正";
		this.cblsoUseBackCorr.UseVisualStyleBackColor = true;
		this.cblsoRestrictRT.AutoSize = true;
		this.cblsoRestrictRT.Location = new System.Drawing.Point(6, 145);
		this.cblsoRestrictRT.Name = "cblsoRestrictRT";
		this.cblsoRestrictRT.Size = new System.Drawing.Size(96, 16);
		this.cblsoRestrictRT.TabIndex = 0;
		this.cblsoRestrictRT.Text = "限制保留时间";
		this.cblsoRestrictRT.UseVisualStyleBackColor = true;
		this.cblsoRestrictWaveLength.AutoSize = true;
		this.cblsoRestrictWaveLength.Location = new System.Drawing.Point(6, 99);
		this.cblsoRestrictWaveLength.Name = "cblsoRestrictWaveLength";
		this.cblsoRestrictWaveLength.Size = new System.Drawing.Size(96, 16);
		this.cblsoRestrictWaveLength.TabIndex = 0;
		this.cblsoRestrictWaveLength.Text = "限制波长范围";
		this.cblsoRestrictWaveLength.UseVisualStyleBackColor = true;
		this.gbpdaPeakPurityOptions.Controls.Add(this.gbppoUsedPoints);
		this.gbpdaPeakPurityOptions.Controls.Add(this.tbppoAbsorbanceThreshold);
		this.gbpdaPeakPurityOptions.Controls.Add(this.tbppoPurityThreshold);
		this.gbpdaPeakPurityOptions.Controls.Add(this.tbppoFrom);
		this.gbpdaPeakPurityOptions.Controls.Add(this.tbppoTo);
		this.gbpdaPeakPurityOptions.Controls.Add(this.lclLabel45);
		this.gbpdaPeakPurityOptions.Controls.Add(this.lclLabel46);
		this.gbpdaPeakPurityOptions.Controls.Add(this.lclLabel47);
		this.gbpdaPeakPurityOptions.Controls.Add(this.lbppoAbsorbanceThreshold);
		this.gbpdaPeakPurityOptions.Controls.Add(this.lbppoTo);
		this.gbpdaPeakPurityOptions.Controls.Add(this.lbppoPurityThreshold);
		this.gbpdaPeakPurityOptions.Controls.Add(this.lbppoFrom);
		this.gbpdaPeakPurityOptions.Controls.Add(this.cbppoUseBackCorr);
		this.gbpdaPeakPurityOptions.Controls.Add(this.cbppoRestrictWaveLength);
		this.gbpdaPeakPurityOptions.Location = new System.Drawing.Point(7, 4);
		this.gbpdaPeakPurityOptions.Name = "gbpdaPeakPurityOptions";
		this.gbpdaPeakPurityOptions.Size = new System.Drawing.Size(266, 219);
		this.gbpdaPeakPurityOptions.TabIndex = 2;
		this.gbpdaPeakPurityOptions.TabStop = false;
		this.gbpdaPeakPurityOptions.Text = "峰纯度选项";
		this.gbppoUsedPoints.Controls.Add(this.rbupFive);
		this.gbppoUsedPoints.Controls.Add(this.rbupAll);
		this.gbppoUsedPoints.Location = new System.Drawing.Point(6, 126);
		this.gbppoUsedPoints.Name = "gbppoUsedPoints";
		this.gbppoUsedPoints.Size = new System.Drawing.Size(103, 64);
		this.gbppoUsedPoints.TabIndex = 3;
		this.gbppoUsedPoints.TabStop = false;
		this.gbppoUsedPoints.Text = "使用点数";
		this.rbupFive.AutoSize = true;
		this.rbupFive.Location = new System.Drawing.Point(6, 42);
		this.rbupFive.Name = "rbupFive";
		this.rbupFive.Size = new System.Drawing.Size(47, 16);
		this.rbupFive.TabIndex = 4;
		this.rbupFive.TabStop = true;
		this.rbupFive.Text = "五点";
		this.rbupFive.UseVisualStyleBackColor = true;
		this.rbupAll.AutoSize = true;
		this.rbupAll.Location = new System.Drawing.Point(6, 20);
		this.rbupAll.Name = "rbupAll";
		this.rbupAll.Size = new System.Drawing.Size(47, 16);
		this.rbupAll.TabIndex = 4;
		this.rbupAll.TabStop = true;
		this.rbupAll.Text = "全部";
		this.rbupAll.UseVisualStyleBackColor = true;
		this.tbppoAbsorbanceThreshold.Location = new System.Drawing.Point(139, 99);
		this.tbppoAbsorbanceThreshold.Name = "tbppoAbsorbanceThreshold";
		this.tbppoAbsorbanceThreshold.Size = new System.Drawing.Size(57, 21);
		this.tbppoAbsorbanceThreshold.TabIndex = 2;
		this.tbppoPurityThreshold.Location = new System.Drawing.Point(139, 72);
		this.tbppoPurityThreshold.Name = "tbppoPurityThreshold";
		this.tbppoPurityThreshold.Size = new System.Drawing.Size(57, 21);
		this.tbppoPurityThreshold.TabIndex = 2;
		this.tbppoFrom.Location = new System.Drawing.Point(96, 45);
		this.tbppoFrom.Name = "tbppoFrom";
		this.tbppoFrom.Size = new System.Drawing.Size(52, 21);
		this.tbppoFrom.TabIndex = 2;
		this.tbppoTo.Location = new System.Drawing.Point(186, 45);
		this.tbppoTo.Name = "tbppoTo";
		this.tbppoTo.Size = new System.Drawing.Size(52, 21);
		this.tbppoTo.TabIndex = 2;
		this.lclLabel45.AutoSize = true;
		this.lclLabel45.Location = new System.Drawing.Point(202, 102);
		this.lclLabel45.Name = "lclLabel45";
		this.lclLabel45.Size = new System.Drawing.Size(11, 12);
		this.lclLabel45.TabIndex = 1;
		this.lclLabel45.Text = "%";
		this.lclLabel46.AutoSize = true;
		this.lclLabel46.Location = new System.Drawing.Point(244, 48);
		this.lclLabel46.Name = "lclLabel46";
		this.lclLabel46.Size = new System.Drawing.Size(17, 12);
		this.lclLabel46.TabIndex = 1;
		this.lclLabel46.Text = "nm";
		this.lclLabel47.AutoSize = true;
		this.lclLabel47.Location = new System.Drawing.Point(202, 75);
		this.lclLabel47.Name = "lclLabel47";
		this.lclLabel47.Size = new System.Drawing.Size(59, 12);
		this.lclLabel47.TabIndex = 1;
		this.lclLabel47.Text = "(0..1000)";
		this.lbppoAbsorbanceThreshold.AutoSize = true;
		this.lbppoAbsorbanceThreshold.Location = new System.Drawing.Point(8, 102);
		this.lbppoAbsorbanceThreshold.Name = "lbppoAbsorbanceThreshold";
		this.lbppoAbsorbanceThreshold.Size = new System.Drawing.Size(53, 12);
		this.lbppoAbsorbanceThreshold.TabIndex = 1;
		this.lbppoAbsorbanceThreshold.Text = "吸收极限";
		this.lbppoTo.AutoSize = true;
		this.lbppoTo.Location = new System.Drawing.Point(154, 48);
		this.lbppoTo.Name = "lbppoTo";
		this.lbppoTo.Size = new System.Drawing.Size(23, 12);
		this.lbppoTo.TabIndex = 1;
		this.lbppoTo.Text = "到:";
		this.lbppoPurityThreshold.AutoSize = true;
		this.lbppoPurityThreshold.Location = new System.Drawing.Point(8, 75);
		this.lbppoPurityThreshold.Name = "lbppoPurityThreshold";
		this.lbppoPurityThreshold.Size = new System.Drawing.Size(53, 12);
		this.lbppoPurityThreshold.TabIndex = 1;
		this.lbppoPurityThreshold.Text = "纯度极限";
		this.lbppoFrom.AutoSize = true;
		this.lbppoFrom.Location = new System.Drawing.Point(50, 48);
		this.lbppoFrom.Name = "lbppoFrom";
		this.lbppoFrom.Size = new System.Drawing.Size(23, 12);
		this.lbppoFrom.TabIndex = 1;
		this.lbppoFrom.Text = "从:";
		this.cbppoUseBackCorr.AutoSize = true;
		this.cbppoUseBackCorr.Location = new System.Drawing.Point(6, 196);
		this.cbppoUseBackCorr.Name = "cbppoUseBackCorr";
		this.cbppoUseBackCorr.Size = new System.Drawing.Size(96, 16);
		this.cbppoUseBackCorr.TabIndex = 0;
		this.cbppoUseBackCorr.Text = "使用背景修正";
		this.cbppoUseBackCorr.UseVisualStyleBackColor = true;
		this.cbppoRestrictWaveLength.AutoSize = true;
		this.cbppoRestrictWaveLength.Location = new System.Drawing.Point(10, 20);
		this.cbppoRestrictWaveLength.Name = "cbppoRestrictWaveLength";
		this.cbppoRestrictWaveLength.Size = new System.Drawing.Size(96, 16);
		this.cbppoRestrictWaveLength.TabIndex = 0;
		this.cbppoRestrictWaveLength.Text = "限制波长范围";
		this.cbppoRestrictWaveLength.UseVisualStyleBackColor = true;
		this.tpRangesGPC.Controls.Add(this.gvgrMw);
		this.tpRangesGPC.Controls.Add(this.gvgrPercent);
		this.tpRangesGPC.Controls.Add(this.lbgrMw);
		this.tpRangesGPC.Controls.Add(this.lbgrPercent);
		this.tpRangesGPC.Location = new System.Drawing.Point(4, 4);
		this.tpRangesGPC.Name = "tpRangesGPC";
		this.tpRangesGPC.Size = new System.Drawing.Size(530, 317);
		this.tpRangesGPC.TabIndex = 12;
		this.tpRangesGPC.Text = "分段";
		this.tpRangesGPC.UseVisualStyleBackColor = true;
		this.tpRangesGPC.Click += new System.EventHandler(tpRangesGPC_Click);
		this.gvgrMw.AllowUserToAddRows = false;
		this.gvgrMw.AllowUserToDeleteRows = false;
		this.gvgrMw.AllowUserToResizeRows = false;
		this.gvgrMw.BackgroundColor = System.Drawing.Color.AliceBlue;
		this.gvgrMw.CharacterHeaderColor = System.Drawing.Color.Black;
		this.gvgrMw.ColumnHeadersHeight = 16;
		this.gvgrMw.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
		this.gvgrMw.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
		this.gvgrMw.Location = new System.Drawing.Point(314, 21);
		this.gvgrMw.Name = "gvgrMw";
		this.gvgrMw.RowHeadersWidth = 25;
		this.gvgrMw.RowTemplate.Height = 16;
		this.gvgrMw.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
		this.gvgrMw.ShowCellToolTips = false;
		this.gvgrMw.Size = new System.Drawing.Size(302, 281);
		this.gvgrMw.TabIndex = 4;
		this.gvgrPercent.AllowUserToAddRows = false;
		this.gvgrPercent.AllowUserToDeleteRows = false;
		this.gvgrPercent.AllowUserToResizeRows = false;
		this.gvgrPercent.BackgroundColor = System.Drawing.Color.AliceBlue;
		this.gvgrPercent.CharacterHeaderColor = System.Drawing.Color.Black;
		this.gvgrPercent.ColumnHeadersHeight = 16;
		this.gvgrPercent.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
		this.gvgrPercent.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
		this.gvgrPercent.Location = new System.Drawing.Point(8, 21);
		this.gvgrPercent.Name = "gvgrPercent";
		this.gvgrPercent.RowHeadersWidth = 25;
		this.gvgrPercent.RowTemplate.Height = 16;
		this.gvgrPercent.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
		this.gvgrPercent.ShowCellToolTips = false;
		this.gvgrPercent.Size = new System.Drawing.Size(300, 281);
		this.gvgrPercent.TabIndex = 5;
		this.lbgrMw.AutoSize = true;
		this.lbgrMw.Location = new System.Drawing.Point(312, 3);
		this.lbgrMw.Name = "lbgrMw";
		this.lbgrMw.Size = new System.Drawing.Size(95, 12);
		this.lbgrMw.TabIndex = 2;
		this.lbgrMw.Text = "分子量类型GPC表";
		this.lbgrPercent.AutoSize = true;
		this.lbgrPercent.Location = new System.Drawing.Point(3, 3);
		this.lbgrPercent.Name = "lbgrPercent";
		this.lbgrPercent.Size = new System.Drawing.Size(95, 12);
		this.lbgrPercent.TabIndex = 3;
		this.lbgrPercent.Text = "百分比类型GPC表";
		this.lbExpress.AutoSize = true;
		this.lbExpress.Location = new System.Drawing.Point(10, 9);
		this.lbExpress.Name = "lbExpress";
		this.lbExpress.Size = new System.Drawing.Size(41, 12);
		this.lbExpress.TabIndex = 6;
		this.lbExpress.Text = "选择AS";
		this.cbDtsSelect.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.cbDtsSelect.FormattingEnabled = true;
		this.cbDtsSelect.ItemExtString = "";
		this.cbDtsSelect.Location = new System.Drawing.Point(174, 6);
		this.cbDtsSelect.Name = "cbDtsSelect";
		this.cbDtsSelect.Size = new System.Drawing.Size(121, 20);
		this.cbDtsSelect.TabIndex = 7;
		this.cbDtsSelect.OnSelectedIndexChanging += new IBrainChrom2018.LclComboBox.SelectedIndexChanging(method_2);
		this.cbDtsSelect.SelectedIndexChanged += new System.EventHandler(cbDtsSelect_SelectedIndexChanged);
		this.btnmtdApply.Location = new System.Drawing.Point(322, 393);
		this.btnmtdApply.Name = "btnmtdApply";
		this.btnmtdApply.Size = new System.Drawing.Size(75, 23);
		this.btnmtdApply.TabIndex = 3;
		this.btnmtdApply.Text = "lclButton5";
		this.btnmtdApply.UseVisualStyleBackColor = true;
		this.btnmtdApply.Click += new System.EventHandler(btnmtdApply_Click);
		this.cbASsSelect.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.cbASsSelect.FormattingEnabled = true;
		this.cbASsSelect.ItemExtString = "";
		this.cbASsSelect.Location = new System.Drawing.Point(365, 6);
		this.cbASsSelect.Name = "cbASsSelect";
		this.cbASsSelect.Size = new System.Drawing.Size(121, 20);
		this.cbASsSelect.TabIndex = 7;
		this.cbASsSelect.OnSelectedIndexChanging += new IBrainChrom2018.LclComboBox.SelectedIndexChanging(method_1);
		this.cbASsSelect.SelectedIndexChanged += new System.EventHandler(cbASsSelect_SelectedIndexChanged);
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
		base.ClientSize = new System.Drawing.Size(544, 420);
		base.Controls.Add(this.lbExpress);
		base.Controls.Add(this.btnmtdApply);
		base.Controls.Add(this.cbASsSelect);
		base.Controls.Add(this.cbDtsSelect);
		base.Controls.Add(this.tcMethod);
		base.Name = "MtdSetupDlg";
		this.Text = "方法设置";
		base.Load += new System.EventHandler(MtdSetupDlg_Load);
		base.KeyDown += new System.Windows.Forms.KeyEventHandler(MtdSetupDlg_KeyDown);
		base.Controls.SetChildIndex(base.btnCancel, 0);
		base.Controls.SetChildIndex(base.btnOK, 0);
		base.Controls.SetChildIndex(base.btnHelp, 0);
		base.Controls.SetChildIndex(this.tcMethod, 0);
		base.Controls.SetChildIndex(this.cbDtsSelect, 0);
		base.Controls.SetChildIndex(this.cbASsSelect, 0);
		base.Controls.SetChildIndex(this.btnmtdApply, 0);
		base.Controls.SetChildIndex(this.lbExpress, 0);
		this.tcMethod.ResumeLayout(false);
		this.tpGC.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.gvExtEvTP).EndInit();
		this.tpTempProg.ResumeLayout(false);
		this.tpTempProg.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.dgvPT).EndInit();
		this.tpGradient.ResumeLayout(false);
		this.tpGradient.PerformLayout();
		this.gblcOption.ResumeLayout(false);
		this.gblcOption.PerformLayout();
		this.gblcIdleState.ResumeLayout(false);
		this.gblcIdleState.PerformLayout();
		this.gblcStandBy.ResumeLayout(false);
		this.gblcStandBy.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.gvlcGradient).EndInit();
		this.cmsLcGradient.ResumeLayout(false);
		this.tpUV.ResumeLayout(false);
		this.tpUV.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.gvProgWave).EndInit();
		this.tpMeasurement.ResumeLayout(false);
		this.tpMeasurement.PerformLayout();
		this.gbmsmExternalControl.ResumeLayout(false);
		this.gbmsmExternalControl.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.pbecDown).EndInit();
		((System.ComponentModel.ISupportInitialize)this.pbecUp).EndInit();
		this.pnlecES.ResumeLayout(false);
		this.pnlecES.PerformLayout();
		this.gbmsmAcquisition.ResumeLayout(false);
		this.gbmsmAcquisition.PerformLayout();
		this.tpAcquisition.ResumeLayout(false);
		this.tpAcquisition.PerformLayout();
		this.tpIntegration.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.gvInteg).EndInit();
		this.cmsIntegration.ResumeLayout(false);
		this.tpCaculation.ResumeLayout(false);
		this.tpCaculation.PerformLayout();
		this.gbcclRltTableReport.ResumeLayout(false);
		this.gbcclRltTableReport.PerformLayout();
		this.gbcclParas.ResumeLayout(false);
		this.gbcclParas.PerformLayout();
		this.tpAdvanced.ResumeLayout(false);
		this.gbadvColumnCalcu.ResumeLayout(false);
		this.gbadvColumnCalcu.PerformLayout();
		this.gbadvAddSub.ResumeLayout(false);
		this.gbadvAddSub.PerformLayout();
		this.tpPDA.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.gvpdaLibs).EndInit();
		this.cmsLibs.ResumeLayout(false);
		this.gbpdaLibSearchOptions.ResumeLayout(false);
		this.gbpdaLibSearchOptions.PerformLayout();
		this.gbpdaPeakPurityOptions.ResumeLayout(false);
		this.gbpdaPeakPurityOptions.PerformLayout();
		this.gbppoUsedPoints.ResumeLayout(false);
		this.gbppoUsedPoints.PerformLayout();
		this.tpRangesGPC.ResumeLayout(false);
		this.tpRangesGPC.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.gvgrMw).EndInit();
		((System.ComponentModel.ISupportInitialize)this.gvgrPercent).EndInit();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
