using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace IBrainChrom2018;

public class OptionsDialog : LclDialog
{
	private LclFontBtn btnaaTitle;

	private LclFontBtn btnaaUnits;

	private LclFontBtn btnaaValue;

	private LclButton btnApply;

	private LclColorBtn btnbdrBackColor;

	private LclColorBtn btnccCalibCurve;

	private LclColorBtn btnchtBackColor;

	private LclColorBtn btnclrA;

	private LclColorBtn btnclrAxes;

	private LclColorBtn btnclrB;

	private LclColorBtn btnclrBaseLine;

	private LclColorBtn btnclrC;

	private LclColorBtn btnclrD;

	private LclButton btnclrInitial;

	private LclButton btnosOriginal;

	private LclButton btnosOriginal2;

	private LclButton btnosOriginal3;

	private LclFontBtn btnptFont;

	private LclButton btnrgGetCurrent;

	private LclButton btnrgGetCurrent2;

	private LclButton btnscInitialColor;

	private LclColorBtn btnsglColor;

	private LclCheckBox cbaaclrAsActiveSignal;

	private LclCheckBox cbbdrWindowsDefault;

	private LclCheckBox cbblclrAsActiveSignal;

	private LclCheckBox cbblLine;

	private LclLineStyleCB cbblLineStyle;

	private LclCheckBox cbblMarks;

	private LclCheckBox cbccAsActiveSignal;

	private LclCheckBox cbchtWindowsDefault;

	private LclCheckBox cbgnlPlayEventsSounds;

	private LclCheckBox cbgnlRequestOldFormatsConfirm;

	private LclCheckBox cbgnlSendUnsuccReports;

	private LclCheckBox cbgnlShowWinsOnTaskbar;

	private LclCheckBox cbgnlWarnMaxZoom;

	private LclCheckBox cbgrpShowEvents;

	private LclCheckBox cbgrpShowGrid;

	private LclCheckBox cbgrpShowLegend;

	private LclCheckBox cbgrpShowWorkplaceLabels;

	private LclCheckBox cbpacPeakTags;

	private LclCheckBox cbpacSetByCalib;

	private LclCheckBox cbptFtClrAsActiveSignal;

	private LclCheckBox cbptGroupID;

	private LclCheckBox cbptName;

	private LclCheckBox cbptPeakNumber;

	private LclCheckBox cbptRetenTime;

	private LclCheckBox cbrgFixed;

	private LclCheckBox cbrgFixed2;

	private LclCheckBox cbsaVisible;

	private LclCheckBox cbsglShow;

	private LclCheckBox cbsglShowLabels;

	private LclComboBox cbsigSignals;

	private LclComboBox cbsymScaleTo;

	private LclCheckBox cbtaVisible;

	private IContainer icontainer_1;

	private LclGroupBox gbaaColor;

	private LclGroupBox gbbcBorder;

	private LclGroupBox gbbcChart;

	private LclGroupBox gbblColor;

	private LclGroupBox gbgaShowYAxisFor;

	private LclGroupBox gbgfColors;

	private LclGroupBox gbgnlTimeAxisData;

	private LclGroupBox gbgrpBackgroundColors;

	private LclGroupBox gbgrpBaseline;

	private LclGroupBox gbgrpPeakTags;

	private LclGroupBox gbptPeakAreaColor;

	private LclGroupBox gbsaOffsetScale;

	private LclGroupBox gbsaRange;

	private LclGroupBox gbscCalibCurve;

	private LclGroupBox gbsglOffsetScale;

	private LclGroupBox gbsigScaleYMode;

	private LclGroupBox gbsigSignals;

	private LclGroupBox gbtaOffsetScale;

	private LclGroupBox gbtaRange;

	private LclDtColorsGV gvscAcquisition;

	public LclSigColorsGV gvscChromatogram;

	private LclLabel lbaaLineWidth;

	private LclLabel lbosOffset;

	private LclLabel lbosOffset2;

	private LclLabel lbosScale;

	private LclLabel lbosScale2;

	private LclLabel lbosX;

	private LclLabel lbosXScale;

	private LclLabel lbosXUnit;

	private LclLabel lbosY;

	private LclLabel lbosYScale;

	private LclLabel lbosYUnit;

	private LclLabel lbrgFrom;

	private LclLabel lbrgFrom2;

	private LclLabel lbrgTo;

	private LclLabel lbrgTo2;

	private LclLabel lbsaDisUnit;

	private LclLabel lbsaTitle;

	private LclLabel lbscAcquisition;

	private LclLabel lbscChromatogram;

	private LclLabel lbscLine;

	private LclLabel lbsglLineWidth;

	private LclLabel lbsymScaleTo;

	private LclLabel lbtaDisUnit;

	private LclLabel lbtaTitle;

	private LclGroupBox lclGroupBox1;

	private Options options_0 = new Options();

	private LclNumericUpDown nudaaLineWidth;

	private LclNumericUpDown nudscLine;

	private LclNumericUpDown nudsglLineWidth;

	private Options options_1;

	private LclPanel pnlsymPreserve;

	private LclRadioButton rbgcDoNotShow;

	private LclRadioButton rbgcTemp;

	private LclRadioButton rblcDoNotShow;

	private LclRadioButton rblcGradient;

	private LclRadioButton rblcTotalFlow;

	private LclRadioButton rbpsrActive;

	private LclRadioButton rbpsrAll;

	private LclRadioButton rbsymPreserve;

	private LclRadioButton rbsymSeperate;

	private LclRadioButton rbtadMinutes;

	private LclRadioButton rbtadSeconds;

	private LclTextBox tbosOffset;

	private LclTextBox tbosOffset2;

	private LclTextBox tbosScale;

	private LclTextBox tbosScale2;

	private LclTextBox tbosX;

	private LclTextBox tbosXScale;

	private LclTextBox tbosY;

	private LclTextBox tbosYScale;

	private LclTextBox tbrgFrom;

	private LclTextBox tbrgFrom2;

	private LclTextBox tbrgTo;

	private LclTextBox tbrgTo2;

	private LclTextBox tbsaTitle;

	private LclTextBox tbsaUnits;

	private LclTextBox tbtaTitle;

	private LclTextBox tbtaUnits;

	private LclTabControl tcDis;

	private LclTabControl tcGraph;

	private LclTabControl tcUserOptions;

	private TabPage tabPage_0;

	private TabPage tabPage_1;

	private TabPage tabPage_2;

	private TabPage tpAuxiliary;

	private TabPage tpAxisAppear;

	private TabPage tpDis;

	private TabPage tpElements;

	private TabPage tpGeneral;

	private TabPage tpGraph;

	private TabPage tpSignalAxis;

	private TabPage tpSignalColor;

	private TabPage tpSignalScale;

	private TabPage tpTimeAxis;

	private WinStyle winStyle_0;

	public OptionsDialog()
	{
		InitializeComponent();
		gbgaShowYAxisFor.Text = Lang.PS("液相", "Liquid");
		lclGroupBox1.Text = Lang.PS("气相", "Gas");
		rbgcTemp.Text = Lang.PS("温度", "Temprature");
		cbblLineStyle.AddItems();
		gvscAcquisition.AddLclColorColumn("color", 70);
		gvscAcquisition.RowCount = 12;
		gvscAcquisition.Refresh_Colors(AccStyle.Read, options_0.dtColors);
		gvscChromatogram.AddLclColorColumn("color", 70);
		gvscChromatogram.RowCount = 12;
		gvscChromatogram.Refresh_Colors(AccStyle.Read, options_0.sgColors);
		cbsigSignals.Width = gbsigSignals.Width - 20;
		gbsigSignals.Location = new Point(cbsigSignals.Left - 7, cbsigSignals.Top);
		cbsigSignals.BringToFront();
	}

	private void btnApply_Click(object sender, EventArgs e)
	{
		method_0(AccStyle.Write, options_0);
		method_0(AccStyle.Read, options_0);
		options_1.LoadFromObject(options_0);
		instrument.SetSignalColor();
		instrument.form.dataAcqForm.LoadOptions();
		WinStyle winStyle = winStyle_0;
		if (winStyle == WinStyle.Chromatogram)
		{
			instrument.form.chromForm.DisDpRefresh();
		}
	}

	private void btnclrInitial_Click(object sender, EventArgs e)
	{
		options_0.InitGradientColors();
		btnclrA.Color = options_0.gradSolvClrA;
		btnclrB.Color = options_0.gradSolvClrB;
		btnclrC.Color = options_0.gradSolvClrC;
		btnclrD.Color = options_0.gradSolvClrD;
	}

	private void btnosOriginal_Click(object sender, EventArgs e)
	{
		tbosOffset.Text = "0";
		tbosScale.Text = "1";
	}

	private void btnosOriginal2_Click(object sender, EventArgs e)
	{
		tbosOffset2.Text = "0";
		tbosScale2.Text = "1";
	}

	private void btnosOriginal3_Click(object sender, EventArgs e)
	{
		LclTextBox lclTextBox = tbosX;
		string text = (tbosY.Text = "0");
		lclTextBox.Text = text;
		LclTextBox lclTextBox2 = tbosXScale;
		text = (tbosYScale.Text = "1");
		lclTextBox2.Text = text;
	}

	private void btnrgGetCurrent_Click(object sender, EventArgs e)
	{
	}

	private void btnrgGetCurrent2_Click(object sender, EventArgs e)
	{
	}

	private void btnscInitialColor_Click(object sender, EventArgs e)
	{
		options_0.InitDtSigColors();
		gvscAcquisition.detector1AsInstru = true;
		gvscAcquisition.Refresh_Colors(AccStyle.Read, options_0.dtColors);
		gvscChromatogram.Refresh_Colors(AccStyle.Read, options_0.sgColors);
	}

	private void cbsigSignals_SelectedIndexChanged(object sender, EventArgs e)
	{
	}

	private void gvscAcquisition_CellClick(object sender, DataGridViewCellEventArgs e)
	{
		if (e.RowIndex == 0 && (gvscAcquisition.Rows[0].Cells[0] as LclgvColorCell).selectResult == DialogResult.OK)
		{
			gvscAcquisition.detector1AsInstru = false;
		}
	}

	public override void LoadLanguage()
	{
		base.LoadLanguage();
		switch (Class49.sysLanguage_0)
		{
		case SysLanguage.CN:
		{
			Text = Lang.PS("用户选项", "User Options");
			tpGeneral.Text = Lang.PS("常规", "General");
			cbgnlShowWinsOnTaskbar.Text = Lang.PS("任务栏显示窗口", "Show windows on the taskbar");
			cbgnlPlayEventsSounds.Text = Lang.PS("播放事件声音", "Play selected events' sounds");
			cbgnlSendUnsuccReports.Text = Lang.PS("发送失败报告", "Send Unsuccessful reports");
			cbgnlRequestOldFormatsConfirm.Text = Lang.PS("旧格式确认", "Old file formats confirmation");
			cbgnlWarnMaxZoom.Text = Lang.PS("最大放大提示", "Warn when maximum zoom reached");
			tpGraph.Text = Lang.PS("图像", "Graph");
			tpElements.Text = Lang.PS("元素", "Elements");
			cbgrpShowWorkplaceLabels.Text = Lang.PS("显示工作区标签", "Show Workplace Labels");
			cbgrpShowGrid.Text = Lang.PS("显示网格", "Show Grid");
			cbgrpShowLegend.Text = Lang.PS("显示图例", "Show Legend");
			cbgrpShowEvents.Text = Lang.PS("显示事件", "Show Events");
			gbgrpBaseline.Text = Lang.PS("基线", "Baseline");
			cbblLine.Text = Lang.PS("显示", "Line");
			cbblMarks.Text = Lang.PS("标识", "Marks");
			gbblColor.Text = Lang.PS("颜色", "Color");
			cbblclrAsActiveSignal.Text = Lang.PS("同当前信号", "As Active Signal");
			btnclrBaseLine.Text = Lang.PS("选择...", "Select...");
			gbgrpPeakTags.Text = Lang.PS("峰标记", "Peak Tags");
			cbptRetenTime.Text = Lang.PS("保留时间", "Retention Time");
			cbptName.Text = Lang.PS("峰名", "Name");
			cbptPeakNumber.Text = Lang.PS("峰标号", "Peak Number");
			cbptGroupID.Text = Lang.PS("组ID", "Group ID");
			btnptFont.Text = Lang.PS("字体...", "Font...");
			cbptFtClrAsActiveSignal.Text = Lang.PS("字体颜色\n同当前信号", "Font Color\nAs Active Signal");
			gbptPeakAreaColor.Text = Lang.PS("峰面着色", "Peak Area Coloring");
			cbpacSetByCalib.Text = Lang.PS("根据校正文件", "Set by Calibration");
			cbpacPeakTags.Text = Lang.PS("表中选择的峰", "Peaks selected\nin the Table");
			gbgrpBackgroundColors.Text = Lang.PS("背景颜色", "Background Colors");
			gbbcChart.Text = Lang.PS("谱图", "Chart");
			cbchtWindowsDefault.Text = Lang.PS("系统默认", "Windows Default");
			btnchtBackColor.Text = Lang.PS("选择...", "Select...");
			gbbcBorder.Text = Lang.PS("边界", "Border");
			cbbdrWindowsDefault.Text = Lang.PS("系统默认", "Windows Default");
			btnbdrBackColor.Text = Lang.PS("选择...", "Select...");
			tpDis.Text = Lang.PS("显示", "Display");
			tpAxisAppear.Text = Lang.PS("轴外观", "AxisAppear.");
			lbaaLineWidth.Text = Lang.PS("线宽", "Line Width");
			gbaaColor.Text = Lang.PS("颜色", "Color");
			cbaaclrAsActiveSignal.Text = Lang.PS("同当前信号", "As Active Signal");
			btnclrAxes.Text = Lang.PS("选择...", "Select...");
			btnaaTitle.Text = Lang.PS("标题字体...", "Title Font...");
			btnaaValue.Text = Lang.PS("值字体...", "Value Font...");
			btnaaUnits.Text = Lang.PS("单位字体...", "Units Font...");
			tpSignalColor.Text = Lang.PS("信号颜色", "Sig.Color");
			lbscLine.Text = Lang.PS("线宽", "Line");
			btnscInitialColor.Text = Lang.PS("设置初始颜色", "Set Initial Color");
			lbscAcquisition.Text = Lang.PS("采集", "Acquisition");
			lbscChromatogram.Text = Lang.PS("谱图", "Chromatogram");
			gbscCalibCurve.Text = Lang.PS("校正曲线", "Calibration Curve");
			cbccAsActiveSignal.Text = Lang.PS("同当前信号", "As Active Signal");
			btnccCalibCurve.Text = Lang.PS("选择...", "Select...");
			tpTimeAxis.Text = Lang.PS("时间轴", "TimeAxis");
			cbtaVisible.Text = Lang.PS("可见", "Visible");
			lbtaTitle.Text = Lang.PS("标题", "Title");
			gbgnlTimeAxisData.Text = Lang.PS("时间轴数据", "Time Axis Data");
			rbtadSeconds.Text = Lang.PS("秒", "Seconds");
			rbtadMinutes.Text = Lang.PS("分", "Minutes");
			lbtaDisUnit.Text = Lang.PS("显示单位", "Display Unit");
			gbtaOffsetScale.Text = Lang.PS("偏移.缩放", "Offset.Scale");
			lbosOffset.Text = Lang.PS("偏移", "Offset");
			lbosScale.Text = Lang.PS("缩放", "Scale");
			btnosOriginal.Text = Lang.PS("复位", "Original");
			gbtaRange.Text = Lang.PS("范围", "Range");
			cbrgFixed.Text = Lang.PS("固定", "Fixed");
			lbrgFrom.Text = Lang.PS("从:", "From:");
			lbrgTo.Text = Lang.PS("到:", "To:");
			btnrgGetCurrent.Text = Lang.PS("提取当前值", "Get Current");
			tpSignalAxis.Text = Lang.PS("信号轴", "Sig.Axis");
			cbsaVisible.Text = Lang.PS("可见", "Visible");
			lbsaTitle.Text = Lang.PS("标题", "Title");
			lbsaDisUnit.Text = Lang.PS("显示单位", "Display Unit");
			gbsaOffsetScale.Text = Lang.PS("偏移.缩放", "Offset.Scale");
			lbosOffset2.Text = Lang.PS("偏移", "Offset");
			lbosScale2.Text = Lang.PS("缩放", "Scale");
			btnosOriginal2.Text = Lang.PS("复位", "Original");
			gbsaRange.Text = Lang.PS("范围", "Range");
			cbrgFixed2.Text = Lang.PS("固定", "Fixed");
			lbrgFrom2.Text = Lang.PS("从:", "From:");
			lbrgTo2.Text = Lang.PS("到:", "To:");
			btnrgGetCurrent2.Text = Lang.PS("提取当前值", "Get Current");
			tpSignalScale.Text = Lang.PS("信号缩放", "Sig.Scale");
			gbsigScaleYMode.Text = Lang.PS("Y轴缩放模式", "Scale Y Mode");
			rbsymPreserve.Text = Lang.PS("保留信号相对性", "Preserve Signal Relation");
			rbpsrAll.Text = Lang.PS("缩放所有信号", "Scale to All Signals");
			rbpsrActive.Text = Lang.PS("缩放当前信号", "Scale to Active Signals");
			rbsymSeperate.Text = Lang.PS("单独缩放信号", "Scale Signals Seperately");
			lbsymScaleTo.Text = Lang.PS("缩放到:", "Scale To:");
			cbsymScaleTo.Items.Clear();
			cbsymScaleTo.Items.Add(Lang.PS("最大值", "Maximum Value"));
			cbsymScaleTo.Items.Add(Lang.PS("最高峰", "Maximum Value"));
			cbsymScaleTo.Items.Add(Lang.PS("第2高峰", "Maximum Value"));
			cbsymScaleTo.Items.Add(Lang.PS("第3高峰", "Maximum Value"));
			cbsglShow.Text = Lang.PS("显示", "Show");
			cbsglShowLabels.Text = Lang.PS("显示标签", "Show Labels");
			lbsglLineWidth.Text = Lang.PS("线宽", "Line Width");
			btnsglColor.Text = Lang.PS("颜色...", "Color...");
			gbsglOffsetScale.Text = Lang.PS("平移.缩放", "Offset.Scale");
			lbosXScale.Text = Lang.PS("X缩放", "X Scale");
			lbosYScale.Text = Lang.PS("Y缩放", "Y Scale");
			btnosOriginal3.Text = Lang.PS("复位", "Original");
			tpAuxiliary.Text = Lang.PS("辅助", "Auxiliary");
			LclRadioButton lclRadioButton2 = rblcDoNotShow;
			string text = (rbgcDoNotShow.Text = Lang.PS("(不显示)", "(do not show)"));
			lclRadioButton2.Text = text;
			rblcGradient.Text = Lang.PS("梯度", "Gradient");
			rblcTotalFlow.Text = Lang.PS("总流速", "Total Flow");
			gbgfColors.Text = Lang.PS("颜色", "Colors");
			btnclrInitial.Text = Lang.PS("颜色初始化", "Set to Initial");
			btnclrA.Text = Lang.PS("溶剂  A", "Solvent A");
			btnclrB.Text = Lang.PS("溶剂  B", "Solvent B");
			btnclrC.Text = Lang.PS("溶剂  C", "Solvent C");
			btnclrD.Text = Lang.PS("溶剂  D", "Solvent D");
			btnApply.Text = Lang.PS("应用", "Apply");
			break;
		}
		case SysLanguage.EN:
		{
			Text = "User Options";
			tpGeneral.Text = "General";
			cbgnlShowWinsOnTaskbar.Text = "Show windows on the taskbar";
			cbgnlPlayEventsSounds.Text = "Play selected events' sounds";
			cbgnlSendUnsuccReports.Text = "Send Unsuccessful reports";
			cbgnlRequestOldFormatsConfirm.Text = "Old file formats confirmation";
			cbgnlWarnMaxZoom.Text = "Warn when maximum zoom reached";
			tpGraph.Text = "Graph";
			tpElements.Text = "Elements";
			cbgrpShowWorkplaceLabels.Text = "Show Workplace Labels";
			cbgrpShowGrid.Text = "Show Grid";
			cbgrpShowLegend.Text = "Show Legend";
			cbgrpShowEvents.Text = "Show Events";
			gbgrpBaseline.Text = "Baseline";
			cbblLine.Text = "Line";
			cbblMarks.Text = "Marks";
			gbblColor.Text = "Color";
			cbblclrAsActiveSignal.Text = "As Active Signal";
			btnclrBaseLine.Text = "Select...";
			gbgrpPeakTags.Text = "Peak Tags";
			cbptRetenTime.Text = "Retention Time";
			cbptName.Text = "Name";
			cbptPeakNumber.Text = "Peak Number";
			cbptGroupID.Text = "Group ID";
			btnptFont.Text = "Font...";
			cbptFtClrAsActiveSignal.Text = "Font Color\nAs Active Signal";
			gbptPeakAreaColor.Text = "Peak Area Coloring";
			cbpacSetByCalib.Text = "Set by Calibration";
			cbpacPeakTags.Text = "Peaks selected\nin the Table";
			gbgrpBackgroundColors.Text = "Background Colors";
			gbbcChart.Text = "Chart";
			cbchtWindowsDefault.Text = "Windows Default";
			btnchtBackColor.Text = "Select...";
			gbbcBorder.Text = "Border";
			cbbdrWindowsDefault.Text = "Windows Default";
			btnbdrBackColor.Text = "Select...";
			tpDis.Text = "Display";
			tpAxisAppear.Text = "AxisAppear.";
			lbaaLineWidth.Text = "Line Width";
			gbaaColor.Text = "Color";
			cbaaclrAsActiveSignal.Text = "As Active Signal";
			btnclrAxes.Text = "Select...";
			btnaaTitle.Text = "Title Font...";
			btnaaValue.Text = "Value Font...";
			btnaaUnits.Text = "Units Font...";
			tpSignalColor.Text = "Sig.Color";
			lbscLine.Text = "Line";
			btnscInitialColor.Text = "Set Initial Color";
			lbscAcquisition.Text = "Acquisition";
			lbscChromatogram.Text = "Chromatogram";
			gbscCalibCurve.Text = "Calibration Curve";
			cbccAsActiveSignal.Text = "As Active Signal";
			btnccCalibCurve.Text = "Select...";
			tpTimeAxis.Text = "TimeAxis";
			cbtaVisible.Text = "Visible";
			lbtaTitle.Text = "Title";
			gbgnlTimeAxisData.Text = "Time Axis Data";
			rbtadSeconds.Text = "Seconds";
			rbtadMinutes.Text = "Minutes";
			lbtaDisUnit.Text = "Display Unit";
			gbtaOffsetScale.Text = "Offset.Scale";
			lbosOffset.Text = "Offset";
			lbosScale.Text = "Scale";
			btnosOriginal.Text = "Original";
			gbtaRange.Text = "Range";
			cbrgFixed.Text = "Fixed";
			lbrgFrom.Text = "From:";
			lbrgTo.Text = "To:";
			btnrgGetCurrent.Text = "Get Current";
			tpSignalAxis.Text = "Sig.Axis";
			cbsaVisible.Text = "Visible";
			lbsaTitle.Text = "Title";
			lbsaDisUnit.Text = "Display Unit";
			gbsaOffsetScale.Text = "Offset.Scale";
			lbosOffset2.Text = "Offset";
			lbosScale2.Text = "Scale";
			btnosOriginal2.Text = "Original";
			gbsaRange.Text = "Range";
			cbrgFixed2.Text = "Fixed";
			lbrgFrom2.Text = "From:";
			lbrgTo2.Text = "To:";
			btnrgGetCurrent2.Text = "Get Current";
			tpSignalScale.Text = "Sig.Scale";
			gbsigScaleYMode.Text = "Scale Y Mode";
			rbsymPreserve.Text = "Preserve Signal Relation";
			rbpsrAll.Text = "Scale to All Signals";
			rbpsrActive.Text = "Scale to Active Signals";
			rbsymSeperate.Text = "Scale Signals Seperately";
			lbsymScaleTo.Text = "Scale To:";
			cbsymScaleTo.Items.Clear();
			cbsymScaleTo.Items.Add("Maximum Value");
			cbsymScaleTo.Items.Add("Highest Peak");
			cbsymScaleTo.Items.Add("2nd Highest Peak");
			cbsymScaleTo.Items.Add("3nd Highest Peak");
			cbsglShow.Text = "Show";
			cbsglShowLabels.Text = "Show Labels";
			lbsglLineWidth.Text = "Line Width";
			btnsglColor.Text = "Color...";
			gbsglOffsetScale.Text = "Offset.Scale";
			lbosXScale.Text = "X Scale";
			lbosYScale.Text = "Y Scale";
			btnosOriginal3.Text = "Original";
			tpAuxiliary.Text = "Auxiliary";
			LclRadioButton lclRadioButton = rblcDoNotShow;
			string text = (rbgcDoNotShow.Text = "(do not show)");
			lclRadioButton.Text = text;
			rblcGradient.Text = "Gradient";
			rblcTotalFlow.Text = "Total Flow";
			gbgfColors.Text = "Colors";
			btnclrInitial.Text = "Set to Initial";
			btnclrA.Text = "Solvent A";
			btnclrB.Text = "Solvent B";
			btnclrC.Text = "Solvent C";
			btnclrD.Text = "Solvent D";
			btnApply.Text = "Apply";
			break;
		}
		}
	}

	private void OptionsDialog_FormClosing(object sender, FormClosingEventArgs e)
	{
		switch (winStyle_0)
		{
		case WinStyle.Instrument:
		case WinStyle.DataAcq:
			tabPage_0 = tcUserOptions.SelectedTab;
			break;
		case WinStyle.Chromatogram:
			tabPage_1 = tcUserOptions.SelectedTab;
			break;
		case WinStyle.CaliGnl:
		case WinStyle.CaliGpc:
			tabPage_2 = tcUserOptions.SelectedTab;
			break;
		}
	}

	private void rbsymPreserve_Click(object sender, EventArgs e)
	{
		(sender as RadioButton).Checked = true;
		pnlsymPreserve.Enabled = sender == rbsymPreserve;
		if (sender == rbsymPreserve)
		{
		}
	}

	private void rbpsrAll_Click(object sender, EventArgs e)
	{
		(sender as RadioButton).Checked = true;
	}

	private void rbtadSeconds_Click(object sender, EventArgs e)
	{
		(sender as RadioButton).Checked = true;
		if (sender == rbtadSeconds)
		{
			tbtaUnits.Text = "sec";
		}
		else if (sender == rbtadMinutes)
		{
			tbtaUnits.Text = "min";
		}
	}

	private void method_0(AccStyle accStyle_0, Options options_2)
	{
		switch (accStyle_0)
		{
		case AccStyle.Read:
			cbgnlShowWinsOnTaskbar.Checked = options_2.gnlShowWinsOnTaskbar;
			cbgnlPlayEventsSounds.Checked = options_2.gnlPlayEventsSounds;
			cbgnlSendUnsuccReports.Checked = options_2.gnlSendUnsuccReports;
			cbgnlRequestOldFormatsConfirm.Checked = options_2.gnlRequestOldFormatsConfirm;
			cbgnlWarnMaxZoom.Checked = options_2.gnlWarnMaxZoom;
			cbgrpShowWorkplaceLabels.Checked = options_2.grpShowWorkplaceLabels;
			cbgrpShowGrid.Checked = options_2.grpShowGrid;
			cbgrpShowLegend.Checked = options_2.grpShowLegend;
			cbgrpShowEvents.Checked = options_2.grpShowEvents;
			cbblLine.Checked = options_2.baselineVisible;
			cbblLineStyle.SelectedIndex = cbblLineStyle.retIndex(options_2.baselineStyle);
			cbblMarks.Checked = options_2.baselineMarks;
			cbblclrAsActiveSignal.Checked = options_2.baselineColorAsActive;
			btnclrBaseLine.Color = options_2.baselineColor;
			cbptRetenTime.Checked = options_2.peakRetenTime;
			cbptName.Checked = options_2.peakName;
			cbptPeakNumber.Checked = options_2.peakNumber;
			cbptGroupID.Checked = options_2.peakGroupID;
			btnptFont.Font = options_2.peakFont;
			cbptFtClrAsActiveSignal.Checked = options_2.peakFtClrAsActiveSignal;
			cbpacSetByCalib.Checked = options_2.peakAreaClrSetByCalib;
			cbpacPeakTags.Checked = options_2.peakAreaClrByTags;
			cbchtWindowsDefault.Checked = options_2.backClrDefaultChart;
			btnchtBackColor.Color = options_2.backClrChart;
			cbbdrWindowsDefault.Checked = options_2.backClrDefaultBorder;
			btnbdrBackColor.Color = options_2.backClrBorder;
			nudaaLineWidth.Value = options_2.axisLineWidth;
			cbaaclrAsActiveSignal.Checked = options_2.axisClrAsActive;
			btnclrAxes.Color = options_2.axisColor;
			btnaaTitle.Font = options_2.titleFont;
			btnaaValue.Font = options_2.valueFont;
			btnaaUnits.Font = options_2.unitFont;
			nudscLine.Value = options_2.sigLineWidth;
			cbccAsActiveSignal.Checked = options_2.caliCurveClrAsActive;
			btnccCalibCurve.Color = options_2.caliCurveColor;
			gvscAcquisition.detector1AsInstru = options_2.dt1cAsInstru;
			gvscAcquisition.Refresh_Colors(accStyle_0, options_2.dtColors);
			gvscChromatogram.Refresh_Colors(accStyle_0, options_2.sgColors);
			cbtaVisible.Checked = options_2.timeAxisVisible;
			tbtaTitle.Text = options_2.timeAxisTitle;
			rbtadSeconds.Checked = options_2.timeAxisData == TimeAxisData.Second;
			rbtadMinutes.Checked = !rbtadSeconds.Checked;
			tbtaUnits.Text = options_2.timeAxisDisUnit;
			tbosOffset.Text = options_2.timeAxisOffset.ToString();
			tbosScale.Text = options_2.timeAxisScale.ToString();
			cbrgFixed.Checked = options_2.timeAxisRangeFixed;
			tbrgFrom.Text = options_2.timeAxisFrom.ToString();
			tbrgTo.Text = options_2.timeAxisTo.ToString();
			cbsaVisible.Checked = options_2.sigAxisVisible;
			tbsaTitle.Text = options_2.sigAxisTitle;
			tbsaUnits.Text = options_2.sigAxisDisUnit;
			tbosOffset2.Text = options_2.sigAxisOffset.ToString();
			tbosScale2.Text = options_2.sigAxisScale.ToString();
			cbrgFixed2.Checked = options_2.sigAxisRangeFixed;
			tbrgFrom2.Text = options_2.sigAxisFrom.ToString();
			tbrgTo2.Text = options_2.sigAxisTo.ToString();
			rbsymPreserve.Checked = options_2.sigScaleYModePreserveRelation;
			rbsymSeperate.Checked = !rbsymPreserve.Checked;
			rbpsrAll.Checked = options_2.sigScalePsrAll;
			rbpsrActive.Checked = !rbpsrAll.Checked;
			cbsymScaleTo.SelectedIndex = (byte)options_2.scaleToStyle;
			rblcDoNotShow.Checked = options_2.lcDisAuxYStyle == LcDisAuxYStyle.None;
			rblcGradient.Checked = options_2.lcDisAuxYStyle == LcDisAuxYStyle.Gradient;
			rblcTotalFlow.Checked = options_2.lcDisAuxYStyle == LcDisAuxYStyle.TotalFlow;
			rbgcDoNotShow.Checked = options_2.gcDisAuxYStyle == GcDisAuxYStyle.None;
			rbgcTemp.Checked = options_2.gcDisAuxYStyle == GcDisAuxYStyle.Temperature;
			btnclrA.Color = options_2.gradSolvClrA;
			btnclrB.Color = options_2.gradSolvClrB;
			btnclrC.Color = options_2.gradSolvClrC;
			btnclrD.Color = options_2.gradSolvClrD;
			break;
		case AccStyle.Write:
			options_2.gnlShowWinsOnTaskbar = cbgnlShowWinsOnTaskbar.Checked;
			options_2.gnlPlayEventsSounds = cbgnlPlayEventsSounds.Checked;
			options_2.gnlSendUnsuccReports = cbgnlSendUnsuccReports.Checked;
			options_2.gnlRequestOldFormatsConfirm = cbgnlRequestOldFormatsConfirm.Checked;
			options_2.gnlWarnMaxZoom = cbgnlWarnMaxZoom.Checked;
			options_2.grpShowWorkplaceLabels = cbgrpShowWorkplaceLabels.Checked;
			options_2.grpShowGrid = cbgrpShowGrid.Checked;
			options_2.grpShowLegend = cbgrpShowLegend.Checked;
			options_2.grpShowEvents = cbgrpShowEvents.Checked;
			options_2.baselineVisible = cbblLine.Checked;
			options_2.baselineStyle = cbblLineStyle.retStyle(cbblLineStyle.SelectedIndex);
			options_2.baselineMarks = cbblMarks.Checked;
			options_2.baselineColorAsActive = cbblclrAsActiveSignal.Checked;
			options_2.baselineColor = btnclrBaseLine.Color;
			options_2.peakRetenTime = cbptRetenTime.Checked;
			options_2.peakName = cbptName.Checked;
			options_2.peakNumber = cbptPeakNumber.Checked;
			options_2.peakGroupID = cbptGroupID.Checked;
			options_2.peakFont = btnptFont.Font;
			options_2.peakFtClrAsActiveSignal = cbptFtClrAsActiveSignal.Checked;
			options_2.peakAreaClrSetByCalib = cbpacSetByCalib.Checked;
			options_2.peakAreaClrByTags = cbpacPeakTags.Checked;
			options_2.backClrDefaultChart = cbchtWindowsDefault.Checked;
			options_2.backClrChart = btnchtBackColor.Color;
			options_2.backClrDefaultBorder = cbbdrWindowsDefault.Checked;
			options_2.backClrBorder = btnbdrBackColor.Color;
			options_2.axisLineWidth = nudaaLineWidth.Value;
			options_2.axisClrAsActive = cbaaclrAsActiveSignal.Checked;
			options_2.axisColor = btnclrAxes.Color;
			options_2.titleFont = btnaaTitle.Font;
			options_2.valueFont = btnaaValue.Font;
			options_2.unitFont = btnaaUnits.Font;
			options_2.sigLineWidth = nudscLine.Value;
			options_2.caliCurveClrAsActive = cbccAsActiveSignal.Checked;
			options_2.caliCurveColor = btnccCalibCurve.Color;
			options_2.dt1cAsInstru = gvscAcquisition.detector1AsInstru;
			gvscAcquisition.Refresh_Colors(accStyle_0, options_2.dtColors);
			gvscChromatogram.Refresh_Colors(accStyle_0, options_2.sgColors);
			options_2.timeAxisVisible = cbtaVisible.Checked;
			options_2.timeAxisTitle = tbtaTitle.Text;
			if (rbtadSeconds.Checked)
			{
				options_2.timeAxisData = TimeAxisData.Second;
			}
			else
			{
				options_2.timeAxisData = TimeAxisData.Minute;
			}
			options_2.timeAxisDisUnit = tbtaUnits.Text;
			options_2.timeAxisOffset = Class49.String2Float(tbosOffset.Text, options_2.timeAxisOffset);
			options_2.timeAxisScale = Class49.String2Float(tbosScale.Text, options_2.timeAxisScale);
			options_2.timeAxisRangeFixed = cbrgFixed.Checked;
			options_2.timeAxisFrom = Class49.String2Float(tbrgFrom.Text, options_2.timeAxisFrom);
			options_2.timeAxisTo = Class49.String2Float(tbrgTo.Text, options_2.timeAxisTo);
			options_2.sigAxisVisible = cbsaVisible.Checked;
			options_2.sigAxisTitle = tbsaTitle.Text;
			options_2.sigAxisDisUnit = tbsaUnits.Text;
			options_2.sigAxisOffset = Class49.String2Float(tbosOffset2.Text, options_2.sigAxisOffset);
			options_2.sigAxisScale = Class49.String2Float(tbosScale2.Text, options_2.sigAxisScale);
			options_2.sigAxisRangeFixed = cbrgFixed2.Checked;
			options_2.sigAxisFrom = Class49.String2Float(tbrgFrom2.Text, options_2.sigAxisFrom);
			options_2.sigAxisTo = Class49.String2Float(tbrgTo2.Text, options_2.sigAxisTo);
			options_2.sigScaleYModePreserveRelation = rbsymPreserve.Checked;
			options_2.sigScalePsrAll = rbpsrAll.Checked;
			options_2.scaleToStyle = (ScaleToStyle)cbsymScaleTo.SelectedIndex;
			if (rblcDoNotShow.Checked)
			{
				options_2.lcDisAuxYStyle = LcDisAuxYStyle.None;
			}
			else if (rblcGradient.Checked)
			{
				options_2.lcDisAuxYStyle = LcDisAuxYStyle.Gradient;
			}
			else if (rblcTotalFlow.Checked)
			{
				options_2.lcDisAuxYStyle = LcDisAuxYStyle.TotalFlow;
			}
			if (rbgcDoNotShow.Checked)
			{
				options_2.gcDisAuxYStyle = GcDisAuxYStyle.None;
			}
			else if (rbgcTemp.Checked)
			{
				options_2.gcDisAuxYStyle = GcDisAuxYStyle.Temperature;
			}
			options_2.gradSolvClrA = btnclrA.Color;
			options_2.gradSolvClrB = btnclrB.Color;
			options_2.gradSolvClrC = btnclrC.Color;
			options_2.gradSolvClrD = btnclrD.Color;
			break;
		}
	}

	public DialogResult ShowDialog(Instrument instrument, WinStyle window, Options options)
	{
		base.instrument = instrument;
		winStyle_0 = window;
		options_1 = options;
		tcUserOptions.TabPages.Clear();
		switch (window)
		{
		case WinStyle.Instrument:
		case WinStyle.DataAcq:
			tcUserOptions.TabPages.Add(tpGeneral);
			tcUserOptions.TabPages.Add(tpGraph);
			tcUserOptions.TabPages.Add(tpAuxiliary);
			if (tabPage_0 != null)
			{
				tcUserOptions.SelectedTab = tabPage_0;
			}
			break;
		case WinStyle.Chromatogram:
			tcUserOptions.TabPages.Add(tpGeneral);
			tcUserOptions.TabPages.Add(tpGraph);
			tcUserOptions.TabPages.Add(tpDis);
			tcUserOptions.TabPages.Add(tpAuxiliary);
			if (tabPage_1 != null)
			{
				tcUserOptions.SelectedTab = tabPage_1;
			}
			break;
		case WinStyle.CaliGnl:
		case WinStyle.CaliGpc:
			tcUserOptions.TabPages.Add(tpGeneral);
			tcUserOptions.TabPages.Add(tpGraph);
			tcUserOptions.TabPages.Add(tpDis);
			if (tabPage_2 != null)
			{
				tcUserOptions.SelectedTab = tabPage_2;
			}
			break;
		}
		DialogResult dialogResult = ShowDialog();
		if (dialogResult == DialogResult.OK)
		{
			btnApply_Click(null, null);
		}
		return dialogResult;
	}

	private void OptionsDialog_Load(object sender, EventArgs e)
	{
		options_0.LoadFromObject(options_1);
		method_0(AccStyle.Read, options_0);
	}

	protected override void Dispose(bool disposing)
	{
		if (disposing && icontainer_1 != null)
		{
			icontainer_1.Dispose();
		}
		base.Dispose(disposing);
	}

	private void InitializeComponent()
	{
		this.tcUserOptions = new IBrainChrom2018.LclTabControl();
		this.tpGeneral = new System.Windows.Forms.TabPage();
		this.cbgnlWarnMaxZoom = new IBrainChrom2018.LclCheckBox();
		this.cbgnlRequestOldFormatsConfirm = new IBrainChrom2018.LclCheckBox();
		this.cbgnlSendUnsuccReports = new IBrainChrom2018.LclCheckBox();
		this.cbgnlPlayEventsSounds = new IBrainChrom2018.LclCheckBox();
		this.cbgnlShowWinsOnTaskbar = new IBrainChrom2018.LclCheckBox();
		this.tpGraph = new System.Windows.Forms.TabPage();
		this.tcGraph = new IBrainChrom2018.LclTabControl();
		this.tpElements = new System.Windows.Forms.TabPage();
		this.gbgrpPeakTags = new IBrainChrom2018.LclGroupBox();
		this.gbptPeakAreaColor = new IBrainChrom2018.LclGroupBox();
		this.cbpacPeakTags = new IBrainChrom2018.LclCheckBox();
		this.cbpacSetByCalib = new IBrainChrom2018.LclCheckBox();
		this.btnptFont = new IBrainChrom2018.LclFontBtn();
		this.cbptFtClrAsActiveSignal = new IBrainChrom2018.LclCheckBox();
		this.cbptGroupID = new IBrainChrom2018.LclCheckBox();
		this.cbptPeakNumber = new IBrainChrom2018.LclCheckBox();
		this.cbptName = new IBrainChrom2018.LclCheckBox();
		this.cbptRetenTime = new IBrainChrom2018.LclCheckBox();
		this.cbgrpShowWorkplaceLabels = new IBrainChrom2018.LclCheckBox();
		this.gbgrpBackgroundColors = new IBrainChrom2018.LclGroupBox();
		this.gbbcChart = new IBrainChrom2018.LclGroupBox();
		this.btnchtBackColor = new IBrainChrom2018.LclColorBtn();
		this.cbchtWindowsDefault = new IBrainChrom2018.LclCheckBox();
		this.gbbcBorder = new IBrainChrom2018.LclGroupBox();
		this.btnbdrBackColor = new IBrainChrom2018.LclColorBtn();
		this.cbbdrWindowsDefault = new IBrainChrom2018.LclCheckBox();
		this.cbgrpShowGrid = new IBrainChrom2018.LclCheckBox();
		this.gbgrpBaseline = new IBrainChrom2018.LclGroupBox();
		this.gbblColor = new IBrainChrom2018.LclGroupBox();
		this.btnclrBaseLine = new IBrainChrom2018.LclColorBtn();
		this.cbblclrAsActiveSignal = new IBrainChrom2018.LclCheckBox();
		this.cbblMarks = new IBrainChrom2018.LclCheckBox();
		this.cbblLineStyle = new IBrainChrom2018.LclLineStyleCB();
		this.cbblLine = new IBrainChrom2018.LclCheckBox();
		this.cbgrpShowLegend = new IBrainChrom2018.LclCheckBox();
		this.cbgrpShowEvents = new IBrainChrom2018.LclCheckBox();
		this.tpAxisAppear = new System.Windows.Forms.TabPage();
		this.btnaaValue = new IBrainChrom2018.LclFontBtn();
		this.btnaaUnits = new IBrainChrom2018.LclFontBtn();
		this.lbaaLineWidth = new IBrainChrom2018.LclLabel();
		this.nudaaLineWidth = new IBrainChrom2018.LclNumericUpDown();
		this.btnaaTitle = new IBrainChrom2018.LclFontBtn();
		this.gbaaColor = new IBrainChrom2018.LclGroupBox();
		this.btnclrAxes = new IBrainChrom2018.LclColorBtn();
		this.cbaaclrAsActiveSignal = new IBrainChrom2018.LclCheckBox();
		this.tpSignalColor = new System.Windows.Forms.TabPage();
		this.gvscAcquisition = new IBrainChrom2018.LclDtColorsGV();
		this.gvscChromatogram = new IBrainChrom2018.LclSigColorsGV();
		this.gbscCalibCurve = new IBrainChrom2018.LclGroupBox();
		this.btnccCalibCurve = new IBrainChrom2018.LclColorBtn();
		this.cbccAsActiveSignal = new IBrainChrom2018.LclCheckBox();
		this.btnscInitialColor = new IBrainChrom2018.LclButton();
		this.nudscLine = new IBrainChrom2018.LclNumericUpDown();
		this.lbscChromatogram = new IBrainChrom2018.LclLabel();
		this.lbscAcquisition = new IBrainChrom2018.LclLabel();
		this.lbscLine = new IBrainChrom2018.LclLabel();
		this.tpDis = new System.Windows.Forms.TabPage();
		this.tcDis = new IBrainChrom2018.LclTabControl();
		this.tpTimeAxis = new System.Windows.Forms.TabPage();
		this.gbgnlTimeAxisData = new IBrainChrom2018.LclGroupBox();
		this.rbtadMinutes = new IBrainChrom2018.LclRadioButton();
		this.rbtadSeconds = new IBrainChrom2018.LclRadioButton();
		this.cbtaVisible = new IBrainChrom2018.LclCheckBox();
		this.gbtaRange = new IBrainChrom2018.LclGroupBox();
		this.cbrgFixed = new IBrainChrom2018.LclCheckBox();
		this.btnrgGetCurrent = new IBrainChrom2018.LclButton();
		this.lbrgTo = new IBrainChrom2018.LclLabel();
		this.lbrgFrom = new IBrainChrom2018.LclLabel();
		this.tbrgTo = new IBrainChrom2018.LclTextBox();
		this.tbrgFrom = new IBrainChrom2018.LclTextBox();
		this.gbtaOffsetScale = new IBrainChrom2018.LclGroupBox();
		this.btnosOriginal = new IBrainChrom2018.LclButton();
		this.lbosScale = new IBrainChrom2018.LclLabel();
		this.lbosOffset = new IBrainChrom2018.LclLabel();
		this.tbosScale = new IBrainChrom2018.LclTextBox();
		this.tbosOffset = new IBrainChrom2018.LclTextBox();
		this.tbtaUnits = new IBrainChrom2018.LclTextBox();
		this.tbtaTitle = new IBrainChrom2018.LclTextBox();
		this.lbtaDisUnit = new IBrainChrom2018.LclLabel();
		this.lbtaTitle = new IBrainChrom2018.LclLabel();
		this.tpSignalAxis = new System.Windows.Forms.TabPage();
		this.cbsaVisible = new IBrainChrom2018.LclCheckBox();
		this.gbsaRange = new IBrainChrom2018.LclGroupBox();
		this.cbrgFixed2 = new IBrainChrom2018.LclCheckBox();
		this.btnrgGetCurrent2 = new IBrainChrom2018.LclButton();
		this.lbrgTo2 = new IBrainChrom2018.LclLabel();
		this.lbrgFrom2 = new IBrainChrom2018.LclLabel();
		this.tbrgTo2 = new IBrainChrom2018.LclTextBox();
		this.tbrgFrom2 = new IBrainChrom2018.LclTextBox();
		this.gbsaOffsetScale = new IBrainChrom2018.LclGroupBox();
		this.btnosOriginal2 = new IBrainChrom2018.LclButton();
		this.lbosScale2 = new IBrainChrom2018.LclLabel();
		this.lbosOffset2 = new IBrainChrom2018.LclLabel();
		this.tbosScale2 = new IBrainChrom2018.LclTextBox();
		this.tbosOffset2 = new IBrainChrom2018.LclTextBox();
		this.tbsaUnits = new IBrainChrom2018.LclTextBox();
		this.tbsaTitle = new IBrainChrom2018.LclTextBox();
		this.lbsaDisUnit = new IBrainChrom2018.LclLabel();
		this.lbsaTitle = new IBrainChrom2018.LclLabel();
		this.tpSignalScale = new System.Windows.Forms.TabPage();
		this.gbsigSignals = new IBrainChrom2018.LclGroupBox();
		this.gbsglOffsetScale = new IBrainChrom2018.LclGroupBox();
		this.tbosYScale = new IBrainChrom2018.LclTextBox();
		this.tbosXScale = new IBrainChrom2018.LclTextBox();
		this.tbosY = new IBrainChrom2018.LclTextBox();
		this.btnosOriginal3 = new IBrainChrom2018.LclButton();
		this.tbosX = new IBrainChrom2018.LclTextBox();
		this.lbosYScale = new IBrainChrom2018.LclLabel();
		this.lbosYUnit = new IBrainChrom2018.LclLabel();
		this.lbosXScale = new IBrainChrom2018.LclLabel();
		this.lbosY = new IBrainChrom2018.LclLabel();
		this.lbosXUnit = new IBrainChrom2018.LclLabel();
		this.lbosX = new IBrainChrom2018.LclLabel();
		this.btnsglColor = new IBrainChrom2018.LclColorBtn();
		this.nudsglLineWidth = new IBrainChrom2018.LclNumericUpDown();
		this.lbsglLineWidth = new IBrainChrom2018.LclLabel();
		this.cbsglShowLabels = new IBrainChrom2018.LclCheckBox();
		this.cbsglShow = new IBrainChrom2018.LclCheckBox();
		this.cbsigSignals = new IBrainChrom2018.LclComboBox();
		this.gbsigScaleYMode = new IBrainChrom2018.LclGroupBox();
		this.cbsymScaleTo = new IBrainChrom2018.LclComboBox();
		this.lbsymScaleTo = new IBrainChrom2018.LclLabel();
		this.pnlsymPreserve = new IBrainChrom2018.LclPanel();
		this.rbpsrActive = new IBrainChrom2018.LclRadioButton();
		this.rbpsrAll = new IBrainChrom2018.LclRadioButton();
		this.rbsymSeperate = new IBrainChrom2018.LclRadioButton();
		this.rbsymPreserve = new IBrainChrom2018.LclRadioButton();
		this.tpAuxiliary = new System.Windows.Forms.TabPage();
		this.gbgfColors = new IBrainChrom2018.LclGroupBox();
		this.btnclrInitial = new IBrainChrom2018.LclButton();
		this.btnclrD = new IBrainChrom2018.LclColorBtn();
		this.btnclrC = new IBrainChrom2018.LclColorBtn();
		this.btnclrB = new IBrainChrom2018.LclColorBtn();
		this.btnclrA = new IBrainChrom2018.LclColorBtn();
		this.lclGroupBox1 = new IBrainChrom2018.LclGroupBox();
		this.rbgcTemp = new IBrainChrom2018.LclRadioButton();
		this.rbgcDoNotShow = new IBrainChrom2018.LclRadioButton();
		this.gbgaShowYAxisFor = new IBrainChrom2018.LclGroupBox();
		this.rblcTotalFlow = new IBrainChrom2018.LclRadioButton();
		this.rblcGradient = new IBrainChrom2018.LclRadioButton();
		this.rblcDoNotShow = new IBrainChrom2018.LclRadioButton();
		this.btnApply = new IBrainChrom2018.LclButton();
		this.tcUserOptions.SuspendLayout();
		this.tpGeneral.SuspendLayout();
		this.tpGraph.SuspendLayout();
		this.tcGraph.SuspendLayout();
		this.tpElements.SuspendLayout();
		this.gbgrpPeakTags.SuspendLayout();
		this.gbptPeakAreaColor.SuspendLayout();
		this.gbgrpBackgroundColors.SuspendLayout();
		this.gbbcChart.SuspendLayout();
		this.gbbcBorder.SuspendLayout();
		this.gbgrpBaseline.SuspendLayout();
		this.gbblColor.SuspendLayout();
		this.tpAxisAppear.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.nudaaLineWidth).BeginInit();
		this.gbaaColor.SuspendLayout();
		this.tpSignalColor.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.gvscAcquisition).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.gvscChromatogram).BeginInit();
		this.gbscCalibCurve.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.nudscLine).BeginInit();
		this.tpDis.SuspendLayout();
		this.tcDis.SuspendLayout();
		this.tpTimeAxis.SuspendLayout();
		this.gbgnlTimeAxisData.SuspendLayout();
		this.gbtaRange.SuspendLayout();
		this.gbtaOffsetScale.SuspendLayout();
		this.tpSignalAxis.SuspendLayout();
		this.gbsaRange.SuspendLayout();
		this.gbsaOffsetScale.SuspendLayout();
		this.tpSignalScale.SuspendLayout();
		this.gbsigSignals.SuspendLayout();
		this.gbsglOffsetScale.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.nudsglLineWidth).BeginInit();
		this.gbsigScaleYMode.SuspendLayout();
		this.pnlsymPreserve.SuspendLayout();
		this.tpAuxiliary.SuspendLayout();
		this.gbgfColors.SuspendLayout();
		this.lclGroupBox1.SuspendLayout();
		this.gbgaShowYAxisFor.SuspendLayout();
		base.SuspendLayout();
		base.btnCancel.Location = new System.Drawing.Point(194, 353);
		base.btnCancel.Text = "取消";
		base.btnHelp.Location = new System.Drawing.Point(387, 353);
		base.btnHelp.Text = "帮助";
		base.btnOK.Location = new System.Drawing.Point(113, 353);
		base.btnOK.Text = "确认";
		this.tcUserOptions.Controls.Add(this.tpGeneral);
		this.tcUserOptions.Controls.Add(this.tpGraph);
		this.tcUserOptions.Controls.Add(this.tpDis);
		this.tcUserOptions.Controls.Add(this.tpAuxiliary);
		this.tcUserOptions.ItemSize = new System.Drawing.Size(90, 19);
		this.tcUserOptions.Location = new System.Drawing.Point(4, 7);
		this.tcUserOptions.Name = "tcUserOptions";
		this.tcUserOptions.SelectedIndex = 0;
		this.tcUserOptions.Size = new System.Drawing.Size(507, 333);
		this.tcUserOptions.TabIndex = 1;
		this.tpGeneral.Controls.Add(this.cbgnlWarnMaxZoom);
		this.tpGeneral.Controls.Add(this.cbgnlRequestOldFormatsConfirm);
		this.tpGeneral.Controls.Add(this.cbgnlSendUnsuccReports);
		this.tpGeneral.Controls.Add(this.cbgnlPlayEventsSounds);
		this.tpGeneral.Controls.Add(this.cbgnlShowWinsOnTaskbar);
		this.tpGeneral.Location = new System.Drawing.Point(4, 23);
		this.tpGeneral.Name = "tpGeneral";
		this.tpGeneral.Size = new System.Drawing.Size(499, 306);
		this.tpGeneral.TabIndex = 0;
		this.tpGeneral.Text = "常规";
		this.tpGeneral.UseVisualStyleBackColor = true;
		this.cbgnlWarnMaxZoom.AutoSize = true;
		this.cbgnlWarnMaxZoom.Location = new System.Drawing.Point(14, 100);
		this.cbgnlWarnMaxZoom.Name = "cbgnlWarnMaxZoom";
		this.cbgnlWarnMaxZoom.Size = new System.Drawing.Size(96, 16);
		this.cbgnlWarnMaxZoom.TabIndex = 0;
		this.cbgnlWarnMaxZoom.Text = "最大放大提示";
		this.cbgnlWarnMaxZoom.UseVisualStyleBackColor = true;
		this.cbgnlRequestOldFormatsConfirm.AutoSize = true;
		this.cbgnlRequestOldFormatsConfirm.Location = new System.Drawing.Point(14, 78);
		this.cbgnlRequestOldFormatsConfirm.Name = "cbgnlRequestOldFormatsConfirm";
		this.cbgnlRequestOldFormatsConfirm.Size = new System.Drawing.Size(96, 16);
		this.cbgnlRequestOldFormatsConfirm.TabIndex = 0;
		this.cbgnlRequestOldFormatsConfirm.Text = "旧格式确认";
		this.cbgnlRequestOldFormatsConfirm.UseVisualStyleBackColor = true;
		this.cbgnlSendUnsuccReports.AutoSize = true;
		this.cbgnlSendUnsuccReports.Location = new System.Drawing.Point(14, 56);
		this.cbgnlSendUnsuccReports.Name = "cbgnlSendUnsuccReports";
		this.cbgnlSendUnsuccReports.Size = new System.Drawing.Size(96, 16);
		this.cbgnlSendUnsuccReports.TabIndex = 0;
		this.cbgnlSendUnsuccReports.Text = "发送失败报告";
		this.cbgnlSendUnsuccReports.UseVisualStyleBackColor = true;
		this.cbgnlPlayEventsSounds.AutoSize = true;
		this.cbgnlPlayEventsSounds.Location = new System.Drawing.Point(14, 34);
		this.cbgnlPlayEventsSounds.Name = "cbgnlPlayEventsSounds";
		this.cbgnlPlayEventsSounds.Size = new System.Drawing.Size(96, 16);
		this.cbgnlPlayEventsSounds.TabIndex = 0;
		this.cbgnlPlayEventsSounds.Text = "播放事件声音";
		this.cbgnlPlayEventsSounds.UseVisualStyleBackColor = true;
		this.cbgnlShowWinsOnTaskbar.AutoSize = true;
		this.cbgnlShowWinsOnTaskbar.Location = new System.Drawing.Point(14, 12);
		this.cbgnlShowWinsOnTaskbar.Name = "cbgnlShowWinsOnTaskbar";
		this.cbgnlShowWinsOnTaskbar.Size = new System.Drawing.Size(96, 16);
		this.cbgnlShowWinsOnTaskbar.TabIndex = 0;
		this.cbgnlShowWinsOnTaskbar.Text = "任务栏显示窗口";
		this.cbgnlShowWinsOnTaskbar.UseVisualStyleBackColor = true;
		this.tpGraph.Controls.Add(this.tcGraph);
		this.tpGraph.Location = new System.Drawing.Point(4, 23);
		this.tpGraph.Name = "tpGraph";
		this.tpGraph.Size = new System.Drawing.Size(499, 306);
		this.tpGraph.TabIndex = 1;
		this.tpGraph.Text = "图像";
		this.tpGraph.UseVisualStyleBackColor = true;
		this.tcGraph.Controls.Add(this.tpElements);
		this.tcGraph.Controls.Add(this.tpAxisAppear);
		this.tcGraph.Controls.Add(this.tpSignalColor);
		this.tcGraph.ItemSize = new System.Drawing.Size(90, 19);
		this.tcGraph.Location = new System.Drawing.Point(3, 3);
		this.tcGraph.Name = "tcGraph";
		this.tcGraph.SelectedIndex = 0;
		this.tcGraph.Size = new System.Drawing.Size(492, 300);
		this.tcGraph.TabIndex = 3;
		this.tpElements.Controls.Add(this.gbgrpPeakTags);
		this.tpElements.Controls.Add(this.cbgrpShowWorkplaceLabels);
		this.tpElements.Controls.Add(this.gbgrpBackgroundColors);
		this.tpElements.Controls.Add(this.cbgrpShowGrid);
		this.tpElements.Controls.Add(this.gbgrpBaseline);
		this.tpElements.Controls.Add(this.cbgrpShowLegend);
		this.tpElements.Controls.Add(this.cbgrpShowEvents);
		this.tpElements.Location = new System.Drawing.Point(4, 23);
		this.tpElements.Name = "tpElements";
		this.tpElements.Size = new System.Drawing.Size(484, 273);
		this.tpElements.TabIndex = 0;
		this.tpElements.Text = "元素";
		this.tpElements.UseVisualStyleBackColor = true;
		this.gbgrpPeakTags.Controls.Add(this.gbptPeakAreaColor);
		this.gbgrpPeakTags.Controls.Add(this.btnptFont);
		this.gbgrpPeakTags.Controls.Add(this.cbptFtClrAsActiveSignal);
		this.gbgrpPeakTags.Controls.Add(this.cbptGroupID);
		this.gbgrpPeakTags.Controls.Add(this.cbptPeakNumber);
		this.gbgrpPeakTags.Controls.Add(this.cbptName);
		this.gbgrpPeakTags.Controls.Add(this.cbptRetenTime);
		this.gbgrpPeakTags.Location = new System.Drawing.Point(177, 10);
		this.gbgrpPeakTags.Name = "gbgrpPeakTags";
		this.gbgrpPeakTags.Size = new System.Drawing.Size(154, 258);
		this.gbgrpPeakTags.TabIndex = 2;
		this.gbgrpPeakTags.TabStop = false;
		this.gbgrpPeakTags.Text = "峰标记";
		this.gbptPeakAreaColor.Controls.Add(this.cbpacPeakTags);
		this.gbptPeakAreaColor.Controls.Add(this.cbpacSetByCalib);
		this.gbptPeakAreaColor.Location = new System.Drawing.Point(6, 188);
		this.gbptPeakAreaColor.Name = "gbptPeakAreaColor";
		this.gbptPeakAreaColor.Size = new System.Drawing.Size(141, 64);
		this.gbptPeakAreaColor.TabIndex = 2;
		this.gbptPeakAreaColor.TabStop = false;
		this.gbptPeakAreaColor.Text = "峰面着色";
		this.cbpacPeakTags.AutoSize = true;
		this.cbpacPeakTags.Location = new System.Drawing.Point(6, 42);
		this.cbpacPeakTags.Name = "cbpacPeakTags";
		this.cbpacPeakTags.Size = new System.Drawing.Size(102, 16);
		this.cbpacPeakTags.TabIndex = 0;
		this.cbpacPeakTags.Text = "表中选择的峰";
		this.cbpacPeakTags.UseVisualStyleBackColor = true;
		this.cbpacSetByCalib.AutoSize = true;
		this.cbpacSetByCalib.Location = new System.Drawing.Point(6, 20);
		this.cbpacSetByCalib.Name = "cbpacSetByCalib";
		this.cbpacSetByCalib.Size = new System.Drawing.Size(102, 16);
		this.cbpacSetByCalib.TabIndex = 0;
		this.cbpacSetByCalib.Text = "根据校正文件";
		this.cbpacSetByCalib.UseVisualStyleBackColor = true;
		this.btnptFont.Location = new System.Drawing.Point(6, 110);
		this.btnptFont.Name = "btnptFont";
		this.btnptFont.Size = new System.Drawing.Size(129, 36);
		this.btnptFont.TabIndex = 1;
		this.btnptFont.Text = "字体...";
		this.btnptFont.UseVisualStyleBackColor = true;
		this.cbptFtClrAsActiveSignal.AutoSize = true;
		this.cbptFtClrAsActiveSignal.Location = new System.Drawing.Point(6, 152);
		this.cbptFtClrAsActiveSignal.Name = "cbptFtClrAsActiveSignal";
		this.cbptFtClrAsActiveSignal.Size = new System.Drawing.Size(96, 16);
		this.cbptFtClrAsActiveSignal.TabIndex = 0;
		this.cbptFtClrAsActiveSignal.Text = "字体颜色\n同当前信号";
		this.cbptFtClrAsActiveSignal.UseVisualStyleBackColor = true;
		this.cbptGroupID.AutoSize = true;
		this.cbptGroupID.Location = new System.Drawing.Point(6, 88);
		this.cbptGroupID.Name = "cbptGroupID";
		this.cbptGroupID.Size = new System.Drawing.Size(96, 16);
		this.cbptGroupID.TabIndex = 0;
		this.cbptGroupID.Text = "组ID";
		this.cbptGroupID.UseVisualStyleBackColor = true;
		this.cbptPeakNumber.AutoSize = true;
		this.cbptPeakNumber.Location = new System.Drawing.Point(6, 66);
		this.cbptPeakNumber.Name = "cbptPeakNumber";
		this.cbptPeakNumber.Size = new System.Drawing.Size(96, 16);
		this.cbptPeakNumber.TabIndex = 0;
		this.cbptPeakNumber.Text = "峰标号";
		this.cbptPeakNumber.UseVisualStyleBackColor = true;
		this.cbptName.AutoSize = true;
		this.cbptName.Location = new System.Drawing.Point(6, 44);
		this.cbptName.Name = "cbptName";
		this.cbptName.Size = new System.Drawing.Size(96, 16);
		this.cbptName.TabIndex = 0;
		this.cbptName.Text = "峰名";
		this.cbptName.UseVisualStyleBackColor = true;
		this.cbptRetenTime.AutoSize = true;
		this.cbptRetenTime.Location = new System.Drawing.Point(6, 22);
		this.cbptRetenTime.Name = "cbptRetenTime";
		this.cbptRetenTime.Size = new System.Drawing.Size(96, 16);
		this.cbptRetenTime.TabIndex = 0;
		this.cbptRetenTime.Text = "保留时间";
		this.cbptRetenTime.UseVisualStyleBackColor = true;
		this.cbgrpShowWorkplaceLabels.AutoSize = true;
		this.cbgrpShowWorkplaceLabels.Location = new System.Drawing.Point(11, 10);
		this.cbgrpShowWorkplaceLabels.Name = "cbgrpShowWorkplaceLabels";
		this.cbgrpShowWorkplaceLabels.Size = new System.Drawing.Size(96, 16);
		this.cbgrpShowWorkplaceLabels.TabIndex = 0;
		this.cbgrpShowWorkplaceLabels.Text = "显示工作区标签";
		this.cbgrpShowWorkplaceLabels.UseVisualStyleBackColor = true;
		this.gbgrpBackgroundColors.Controls.Add(this.gbbcChart);
		this.gbgrpBackgroundColors.Controls.Add(this.gbbcBorder);
		this.gbgrpBackgroundColors.Location = new System.Drawing.Point(337, 10);
		this.gbgrpBackgroundColors.Name = "gbgrpBackgroundColors";
		this.gbgrpBackgroundColors.Size = new System.Drawing.Size(140, 188);
		this.gbgrpBackgroundColors.TabIndex = 1;
		this.gbgrpBackgroundColors.TabStop = false;
		this.gbgrpBackgroundColors.Text = "背景颜色";
		this.gbbcChart.Controls.Add(this.btnchtBackColor);
		this.gbbcChart.Controls.Add(this.cbchtWindowsDefault);
		this.gbbcChart.Location = new System.Drawing.Point(6, 20);
		this.gbbcChart.Name = "gbbcChart";
		this.gbbcChart.Size = new System.Drawing.Size(127, 74);
		this.gbbcChart.TabIndex = 2;
		this.gbbcChart.TabStop = false;
		this.gbbcChart.Text = "谱图";
		this.btnchtBackColor.Color = System.Drawing.Color.Green;
		this.btnchtBackColor.Location = new System.Drawing.Point(8, 42);
		this.btnchtBackColor.Name = "btnchtBackColor";
		this.btnchtBackColor.Size = new System.Drawing.Size(100, 23);
		this.btnchtBackColor.TabIndex = 1;
		this.btnchtBackColor.Text = "选择...";
		this.btnchtBackColor.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.btnchtBackColor.UseVisualStyleBackColor = true;
		this.cbchtWindowsDefault.AutoSize = true;
		this.cbchtWindowsDefault.Location = new System.Drawing.Point(6, 20);
		this.cbchtWindowsDefault.Name = "cbchtWindowsDefault";
		this.cbchtWindowsDefault.Size = new System.Drawing.Size(102, 16);
		this.cbchtWindowsDefault.TabIndex = 0;
		this.cbchtWindowsDefault.Text = "系统默认";
		this.cbchtWindowsDefault.UseVisualStyleBackColor = true;
		this.gbbcBorder.Controls.Add(this.btnbdrBackColor);
		this.gbbcBorder.Controls.Add(this.cbbdrWindowsDefault);
		this.gbbcBorder.Location = new System.Drawing.Point(6, 104);
		this.gbbcBorder.Name = "gbbcBorder";
		this.gbbcBorder.Size = new System.Drawing.Size(127, 74);
		this.gbbcBorder.TabIndex = 2;
		this.gbbcBorder.TabStop = false;
		this.gbbcBorder.Text = "边界";
		this.btnbdrBackColor.Color = System.Drawing.Color.Green;
		this.btnbdrBackColor.Location = new System.Drawing.Point(8, 42);
		this.btnbdrBackColor.Name = "btnbdrBackColor";
		this.btnbdrBackColor.Size = new System.Drawing.Size(100, 23);
		this.btnbdrBackColor.TabIndex = 1;
		this.btnbdrBackColor.Text = "选择...";
		this.btnbdrBackColor.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.btnbdrBackColor.UseVisualStyleBackColor = true;
		this.cbbdrWindowsDefault.AutoSize = true;
		this.cbbdrWindowsDefault.Location = new System.Drawing.Point(6, 20);
		this.cbbdrWindowsDefault.Name = "cbbdrWindowsDefault";
		this.cbbdrWindowsDefault.Size = new System.Drawing.Size(102, 16);
		this.cbbdrWindowsDefault.TabIndex = 0;
		this.cbbdrWindowsDefault.Text = "系统默认";
		this.cbbdrWindowsDefault.UseVisualStyleBackColor = true;
		this.cbgrpShowGrid.AutoSize = true;
		this.cbgrpShowGrid.Location = new System.Drawing.Point(11, 32);
		this.cbgrpShowGrid.Name = "cbgrpShowGrid";
		this.cbgrpShowGrid.Size = new System.Drawing.Size(96, 16);
		this.cbgrpShowGrid.TabIndex = 0;
		this.cbgrpShowGrid.Text = "显示网格";
		this.cbgrpShowGrid.UseVisualStyleBackColor = true;
		this.gbgrpBaseline.Controls.Add(this.gbblColor);
		this.gbgrpBaseline.Controls.Add(this.cbblMarks);
		this.gbgrpBaseline.Controls.Add(this.cbblLineStyle);
		this.gbgrpBaseline.Controls.Add(this.cbblLine);
		this.gbgrpBaseline.Location = new System.Drawing.Point(11, 96);
		this.gbgrpBaseline.Name = "gbgrpBaseline";
		this.gbgrpBaseline.Size = new System.Drawing.Size(160, 172);
		this.gbgrpBaseline.TabIndex = 1;
		this.gbgrpBaseline.TabStop = false;
		this.gbgrpBaseline.Text = "基线";
		this.gbblColor.Controls.Add(this.btnclrBaseLine);
		this.gbblColor.Controls.Add(this.cbblclrAsActiveSignal);
		this.gbblColor.Location = new System.Drawing.Point(6, 92);
		this.gbblColor.Name = "gbblColor";
		this.gbblColor.Size = new System.Drawing.Size(147, 74);
		this.gbblColor.TabIndex = 2;
		this.gbblColor.TabStop = false;
		this.gbblColor.Text = "颜色";
		this.btnclrBaseLine.Color = System.Drawing.Color.Green;
		this.btnclrBaseLine.Location = new System.Drawing.Point(8, 42);
		this.btnclrBaseLine.Name = "btnclrBaseLine";
		this.btnclrBaseLine.Size = new System.Drawing.Size(100, 23);
		this.btnclrBaseLine.TabIndex = 1;
		this.btnclrBaseLine.Text = "选择...";
		this.btnclrBaseLine.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.btnclrBaseLine.UseVisualStyleBackColor = true;
		this.cbblclrAsActiveSignal.AutoSize = true;
		this.cbblclrAsActiveSignal.Location = new System.Drawing.Point(6, 20);
		this.cbblclrAsActiveSignal.Name = "cbblclrAsActiveSignal";
		this.cbblclrAsActiveSignal.Size = new System.Drawing.Size(102, 16);
		this.cbblclrAsActiveSignal.TabIndex = 0;
		this.cbblclrAsActiveSignal.Text = "同当前信号";
		this.cbblclrAsActiveSignal.UseVisualStyleBackColor = true;
		this.cbblMarks.AutoSize = true;
		this.cbblMarks.Location = new System.Drawing.Point(6, 70);
		this.cbblMarks.Name = "cbblMarks";
		this.cbblMarks.Size = new System.Drawing.Size(102, 16);
		this.cbblMarks.TabIndex = 2;
		this.cbblMarks.Text = "标识";
		this.cbblMarks.UseVisualStyleBackColor = true;
		this.cbblLineStyle.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
		this.cbblLineStyle.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.cbblLineStyle.FormattingEnabled = true;
		this.cbblLineStyle.Location = new System.Drawing.Point(6, 42);
		this.cbblLineStyle.Name = "cbblLineStyle";
		this.cbblLineStyle.Size = new System.Drawing.Size(121, 22);
		this.cbblLineStyle.TabIndex = 2;
		this.cbblLine.AutoSize = true;
		this.cbblLine.Location = new System.Drawing.Point(6, 20);
		this.cbblLine.Name = "cbblLine";
		this.cbblLine.Size = new System.Drawing.Size(96, 16);
		this.cbblLine.TabIndex = 0;
		this.cbblLine.Text = "显示";
		this.cbblLine.UseVisualStyleBackColor = true;
		this.cbgrpShowLegend.AutoSize = true;
		this.cbgrpShowLegend.Location = new System.Drawing.Point(11, 54);
		this.cbgrpShowLegend.Name = "cbgrpShowLegend";
		this.cbgrpShowLegend.Size = new System.Drawing.Size(96, 16);
		this.cbgrpShowLegend.TabIndex = 0;
		this.cbgrpShowLegend.Text = "显示图例";
		this.cbgrpShowLegend.UseVisualStyleBackColor = true;
		this.cbgrpShowEvents.AutoSize = true;
		this.cbgrpShowEvents.Location = new System.Drawing.Point(11, 77);
		this.cbgrpShowEvents.Name = "cbgrpShowEvents";
		this.cbgrpShowEvents.Size = new System.Drawing.Size(96, 16);
		this.cbgrpShowEvents.TabIndex = 0;
		this.cbgrpShowEvents.Text = "显示事件";
		this.cbgrpShowEvents.UseVisualStyleBackColor = true;
		this.tpAxisAppear.Controls.Add(this.btnaaValue);
		this.tpAxisAppear.Controls.Add(this.btnaaUnits);
		this.tpAxisAppear.Controls.Add(this.lbaaLineWidth);
		this.tpAxisAppear.Controls.Add(this.nudaaLineWidth);
		this.tpAxisAppear.Controls.Add(this.btnaaTitle);
		this.tpAxisAppear.Controls.Add(this.gbaaColor);
		this.tpAxisAppear.Location = new System.Drawing.Point(4, 23);
		this.tpAxisAppear.Name = "tpAxisAppear";
		this.tpAxisAppear.Size = new System.Drawing.Size(484, 273);
		this.tpAxisAppear.TabIndex = 9;
		this.tpAxisAppear.Text = "轴外观";
		this.tpAxisAppear.UseVisualStyleBackColor = true;
		this.btnaaValue.Location = new System.Drawing.Point(190, 74);
		this.btnaaValue.Name = "btnaaValue";
		this.btnaaValue.Size = new System.Drawing.Size(110, 30);
		this.btnaaValue.TabIndex = 4;
		this.btnaaValue.Text = "值字体...";
		this.btnaaValue.UseVisualStyleBackColor = true;
		this.btnaaUnits.Location = new System.Drawing.Point(190, 110);
		this.btnaaUnits.Name = "btnaaUnits";
		this.btnaaUnits.Size = new System.Drawing.Size(110, 30);
		this.btnaaUnits.TabIndex = 4;
		this.btnaaUnits.Text = "单位字体...";
		this.btnaaUnits.UseVisualStyleBackColor = true;
		this.lbaaLineWidth.AutoSize = true;
		this.lbaaLineWidth.Location = new System.Drawing.Point(16, 27);
		this.lbaaLineWidth.Name = "lbaaLineWidth";
		this.lbaaLineWidth.Size = new System.Drawing.Size(59, 12);
		this.lbaaLineWidth.TabIndex = 0;
		this.lbaaLineWidth.Text = "线宽";
		this.nudaaLineWidth.Location = new System.Drawing.Point(18, 42);
		this.nudaaLineWidth.Name = "nudaaLineWidth";
		this.nudaaLineWidth.Size = new System.Drawing.Size(57, 21);
		this.nudaaLineWidth.TabIndex = 1;
		this.btnaaTitle.Location = new System.Drawing.Point(190, 38);
		this.btnaaTitle.Name = "btnaaTitle";
		this.btnaaTitle.Size = new System.Drawing.Size(110, 30);
		this.btnaaTitle.TabIndex = 4;
		this.btnaaTitle.Text = "标题字体...";
		this.btnaaTitle.UseVisualStyleBackColor = true;
		this.gbaaColor.Controls.Add(this.btnclrAxes);
		this.gbaaColor.Controls.Add(this.cbaaclrAsActiveSignal);
		this.gbaaColor.Location = new System.Drawing.Point(18, 80);
		this.gbaaColor.Name = "gbaaColor";
		this.gbaaColor.Size = new System.Drawing.Size(139, 74);
		this.gbaaColor.TabIndex = 3;
		this.gbaaColor.TabStop = false;
		this.gbaaColor.Text = "颜色";
		this.btnclrAxes.Color = System.Drawing.Color.Green;
		this.btnclrAxes.Location = new System.Drawing.Point(8, 42);
		this.btnclrAxes.Name = "btnclrAxes";
		this.btnclrAxes.Size = new System.Drawing.Size(100, 23);
		this.btnclrAxes.TabIndex = 1;
		this.btnclrAxes.Text = "选择...";
		this.btnclrAxes.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.btnclrAxes.UseVisualStyleBackColor = true;
		this.cbaaclrAsActiveSignal.AutoSize = true;
		this.cbaaclrAsActiveSignal.Location = new System.Drawing.Point(6, 20);
		this.cbaaclrAsActiveSignal.Name = "cbaaclrAsActiveSignal";
		this.cbaaclrAsActiveSignal.Size = new System.Drawing.Size(102, 16);
		this.cbaaclrAsActiveSignal.TabIndex = 0;
		this.cbaaclrAsActiveSignal.Text = "同当前信号";
		this.cbaaclrAsActiveSignal.UseVisualStyleBackColor = true;
		this.tpSignalColor.Controls.Add(this.gvscAcquisition);
		this.tpSignalColor.Controls.Add(this.gvscChromatogram);
		this.tpSignalColor.Controls.Add(this.gbscCalibCurve);
		this.tpSignalColor.Controls.Add(this.btnscInitialColor);
		this.tpSignalColor.Controls.Add(this.nudscLine);
		this.tpSignalColor.Controls.Add(this.lbscChromatogram);
		this.tpSignalColor.Controls.Add(this.lbscAcquisition);
		this.tpSignalColor.Controls.Add(this.lbscLine);
		this.tpSignalColor.Location = new System.Drawing.Point(4, 23);
		this.tpSignalColor.Name = "tpSignalColor";
		this.tpSignalColor.Size = new System.Drawing.Size(484, 273);
		this.tpSignalColor.TabIndex = 10;
		this.tpSignalColor.Text = "信号颜色";
		this.tpSignalColor.UseVisualStyleBackColor = true;
		this.gvscAcquisition.AllowUserToAddRows = false;
		this.gvscAcquisition.AllowUserToDeleteRows = false;
		this.gvscAcquisition.AllowUserToResizeRows = false;
		this.gvscAcquisition.BackgroundColor = System.Drawing.Color.AliceBlue;
		this.gvscAcquisition.CharacterHeaderColor = System.Drawing.Color.Black;
		this.gvscAcquisition.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
		this.gvscAcquisition.ColumnHeadersVisible = false;
		this.gvscAcquisition.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
		this.gvscAcquisition.Location = new System.Drawing.Point(12, 72);
		this.gvscAcquisition.MultiSelect = false;
		this.gvscAcquisition.Name = "gvscAcquisition";
		this.gvscAcquisition.ReadOnly = true;
		this.gvscAcquisition.RowHeadersWidth = 80;
		this.gvscAcquisition.RowTemplate.Height = 16;
		this.gvscAcquisition.ScrollBars = System.Windows.Forms.ScrollBars.None;
		this.gvscAcquisition.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
		this.gvscAcquisition.ShowCellToolTips = false;
		this.gvscAcquisition.Size = new System.Drawing.Size(151, 194);
		this.gvscAcquisition.TabIndex = 7;
		this.gvscAcquisition.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(gvscAcquisition_CellClick);
		this.gvscChromatogram.AllowUserToAddRows = false;
		this.gvscChromatogram.AllowUserToDeleteRows = false;
		this.gvscChromatogram.AllowUserToResizeRows = false;
		this.gvscChromatogram.BackgroundColor = System.Drawing.Color.AliceBlue;
		this.gvscChromatogram.CharacterHeaderColor = System.Drawing.Color.Black;
		this.gvscChromatogram.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
		this.gvscChromatogram.ColumnHeadersVisible = false;
		this.gvscChromatogram.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
		this.gvscChromatogram.Location = new System.Drawing.Point(223, 72);
		this.gvscChromatogram.MultiSelect = false;
		this.gvscChromatogram.Name = "gvscChromatogram";
		this.gvscChromatogram.ReadOnly = true;
		this.gvscChromatogram.RowHeadersWidth = 80;
		this.gvscChromatogram.RowTemplate.Height = 16;
		this.gvscChromatogram.ScrollBars = System.Windows.Forms.ScrollBars.None;
		this.gvscChromatogram.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
		this.gvscChromatogram.ShowCellToolTips = false;
		this.gvscChromatogram.Size = new System.Drawing.Size(151, 194);
		this.gvscChromatogram.TabIndex = 6;
		this.gbscCalibCurve.Controls.Add(this.btnccCalibCurve);
		this.gbscCalibCurve.Controls.Add(this.cbccAsActiveSignal);
		this.gbscCalibCurve.Location = new System.Drawing.Point(223, 6);
		this.gbscCalibCurve.Name = "gbscCalibCurve";
		this.gbscCalibCurve.Size = new System.Drawing.Size(224, 45);
		this.gbscCalibCurve.TabIndex = 5;
		this.gbscCalibCurve.TabStop = false;
		this.gbscCalibCurve.Text = "校正曲线";
		this.btnccCalibCurve.Color = System.Drawing.Color.Green;
		this.btnccCalibCurve.Location = new System.Drawing.Point(114, 16);
		this.btnccCalibCurve.Name = "btnccCalibCurve";
		this.btnccCalibCurve.Size = new System.Drawing.Size(100, 23);
		this.btnccCalibCurve.TabIndex = 1;
		this.btnccCalibCurve.Text = "选择...";
		this.btnccCalibCurve.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.btnccCalibCurve.UseVisualStyleBackColor = true;
		this.cbccAsActiveSignal.AutoSize = true;
		this.cbccAsActiveSignal.Location = new System.Drawing.Point(6, 20);
		this.cbccAsActiveSignal.Name = "cbccAsActiveSignal";
		this.cbccAsActiveSignal.Size = new System.Drawing.Size(102, 16);
		this.cbccAsActiveSignal.TabIndex = 0;
		this.cbccAsActiveSignal.Text = "同当前信号";
		this.cbccAsActiveSignal.UseVisualStyleBackColor = true;
		this.btnscInitialColor.Location = new System.Drawing.Point(75, 29);
		this.btnscInitialColor.Name = "btnscInitialColor";
		this.btnscInitialColor.Size = new System.Drawing.Size(133, 23);
		this.btnscInitialColor.TabIndex = 4;
		this.btnscInitialColor.Text = "设置初始颜色";
		this.btnscInitialColor.UseVisualStyleBackColor = true;
		this.btnscInitialColor.Click += new System.EventHandler(btnscInitialColor_Click);
		this.nudscLine.Location = new System.Drawing.Point(12, 30);
		this.nudscLine.Name = "nudscLine";
		this.nudscLine.Size = new System.Drawing.Size(57, 21);
		this.nudscLine.TabIndex = 3;
		this.lbscChromatogram.AutoSize = true;
		this.lbscChromatogram.Location = new System.Drawing.Point(221, 57);
		this.lbscChromatogram.Name = "lbscChromatogram";
		this.lbscChromatogram.Size = new System.Drawing.Size(59, 12);
		this.lbscChromatogram.TabIndex = 2;
		this.lbscChromatogram.Text = "谱图";
		this.lbscAcquisition.AutoSize = true;
		this.lbscAcquisition.Location = new System.Drawing.Point(10, 57);
		this.lbscAcquisition.Name = "lbscAcquisition";
		this.lbscAcquisition.Size = new System.Drawing.Size(59, 12);
		this.lbscAcquisition.TabIndex = 2;
		this.lbscAcquisition.Text = "采集";
		this.lbscLine.AutoSize = true;
		this.lbscLine.Location = new System.Drawing.Point(10, 15);
		this.lbscLine.Name = "lbscLine";
		this.lbscLine.Size = new System.Drawing.Size(59, 12);
		this.lbscLine.TabIndex = 2;
		this.lbscLine.Text = "线宽";
		this.tpDis.Controls.Add(this.tcDis);
		this.tpDis.Location = new System.Drawing.Point(4, 23);
		this.tpDis.Name = "tpDis";
		this.tpDis.Size = new System.Drawing.Size(499, 306);
		this.tpDis.TabIndex = 2;
		this.tpDis.Text = "显示";
		this.tpDis.UseVisualStyleBackColor = true;
		this.tcDis.Controls.Add(this.tpTimeAxis);
		this.tcDis.Controls.Add(this.tpSignalAxis);
		this.tcDis.Controls.Add(this.tpSignalScale);
		this.tcDis.ItemSize = new System.Drawing.Size(90, 19);
		this.tcDis.Location = new System.Drawing.Point(3, 3);
		this.tcDis.Name = "tcDis";
		this.tcDis.SelectedIndex = 0;
		this.tcDis.Size = new System.Drawing.Size(493, 300);
		this.tcDis.TabIndex = 5;
		this.tpTimeAxis.Controls.Add(this.gbgnlTimeAxisData);
		this.tpTimeAxis.Controls.Add(this.cbtaVisible);
		this.tpTimeAxis.Controls.Add(this.gbtaRange);
		this.tpTimeAxis.Controls.Add(this.gbtaOffsetScale);
		this.tpTimeAxis.Controls.Add(this.tbtaUnits);
		this.tpTimeAxis.Controls.Add(this.tbtaTitle);
		this.tpTimeAxis.Controls.Add(this.lbtaDisUnit);
		this.tpTimeAxis.Controls.Add(this.lbtaTitle);
		this.tpTimeAxis.Location = new System.Drawing.Point(4, 23);
		this.tpTimeAxis.Name = "tpTimeAxis";
		this.tpTimeAxis.Size = new System.Drawing.Size(485, 273);
		this.tpTimeAxis.TabIndex = 1;
		this.tpTimeAxis.Text = "时间轴";
		this.tpTimeAxis.UseVisualStyleBackColor = true;
		this.gbgnlTimeAxisData.Controls.Add(this.rbtadMinutes);
		this.gbgnlTimeAxisData.Controls.Add(this.rbtadSeconds);
		this.gbgnlTimeAxisData.Location = new System.Drawing.Point(12, 88);
		this.gbgnlTimeAxisData.Name = "gbgnlTimeAxisData";
		this.gbgnlTimeAxisData.Size = new System.Drawing.Size(132, 62);
		this.gbgnlTimeAxisData.TabIndex = 15;
		this.gbgnlTimeAxisData.TabStop = false;
		this.gbgnlTimeAxisData.Text = "时间轴数据";
		this.rbtadMinutes.AutoSize = true;
		this.rbtadMinutes.Location = new System.Drawing.Point(6, 42);
		this.rbtadMinutes.Name = "rbtadMinutes";
		this.rbtadMinutes.Size = new System.Drawing.Size(113, 16);
		this.rbtadMinutes.TabIndex = 2;
		this.rbtadMinutes.TabStop = true;
		this.rbtadMinutes.Text = "分";
		this.rbtadMinutes.UseVisualStyleBackColor = true;
		this.rbtadMinutes.Click += new System.EventHandler(rbtadSeconds_Click);
		this.rbtadSeconds.AutoSize = true;
		this.rbtadSeconds.Location = new System.Drawing.Point(6, 20);
		this.rbtadSeconds.Name = "rbtadSeconds";
		this.rbtadSeconds.Size = new System.Drawing.Size(113, 16);
		this.rbtadSeconds.TabIndex = 2;
		this.rbtadSeconds.TabStop = true;
		this.rbtadSeconds.Text = "秒";
		this.rbtadSeconds.UseVisualStyleBackColor = true;
		this.rbtadSeconds.Click += new System.EventHandler(rbtadSeconds_Click);
		this.cbtaVisible.AutoSize = true;
		this.cbtaVisible.Location = new System.Drawing.Point(12, 14);
		this.cbtaVisible.Name = "cbtaVisible";
		this.cbtaVisible.Size = new System.Drawing.Size(96, 16);
		this.cbtaVisible.TabIndex = 10;
		this.cbtaVisible.Text = "可见";
		this.cbtaVisible.UseVisualStyleBackColor = true;
		this.gbtaRange.Controls.Add(this.cbrgFixed);
		this.gbtaRange.Controls.Add(this.btnrgGetCurrent);
		this.gbtaRange.Controls.Add(this.lbrgTo);
		this.gbtaRange.Controls.Add(this.lbrgFrom);
		this.gbtaRange.Controls.Add(this.tbrgTo);
		this.gbtaRange.Controls.Add(this.tbrgFrom);
		this.gbtaRange.Location = new System.Drawing.Point(217, 118);
		this.gbtaRange.Name = "gbtaRange";
		this.gbtaRange.Size = new System.Drawing.Size(192, 123);
		this.gbtaRange.TabIndex = 14;
		this.gbtaRange.TabStop = false;
		this.gbtaRange.Text = "范围";
		this.cbrgFixed.AutoSize = true;
		this.cbrgFixed.Location = new System.Drawing.Point(82, 17);
		this.cbrgFixed.Name = "cbrgFixed";
		this.cbrgFixed.Size = new System.Drawing.Size(96, 16);
		this.cbrgFixed.TabIndex = 2;
		this.cbrgFixed.Text = "固定";
		this.cbrgFixed.UseVisualStyleBackColor = true;
		this.btnrgGetCurrent.Location = new System.Drawing.Point(82, 93);
		this.btnrgGetCurrent.Name = "btnrgGetCurrent";
		this.btnrgGetCurrent.Size = new System.Drawing.Size(104, 23);
		this.btnrgGetCurrent.TabIndex = 0;
		this.btnrgGetCurrent.Text = "提取当前值";
		this.btnrgGetCurrent.UseVisualStyleBackColor = true;
		this.btnrgGetCurrent.Click += new System.EventHandler(btnrgGetCurrent_Click);
		this.lbrgTo.AutoSize = true;
		this.lbrgTo.Location = new System.Drawing.Point(17, 70);
		this.lbrgTo.Name = "lbrgTo";
		this.lbrgTo.Size = new System.Drawing.Size(59, 12);
		this.lbrgTo.TabIndex = 0;
		this.lbrgTo.Text = "到:";
		this.lbrgFrom.AutoSize = true;
		this.lbrgFrom.Location = new System.Drawing.Point(17, 43);
		this.lbrgFrom.Name = "lbrgFrom";
		this.lbrgFrom.Size = new System.Drawing.Size(59, 12);
		this.lbrgFrom.TabIndex = 0;
		this.lbrgFrom.Text = "从:";
		this.tbrgTo.Location = new System.Drawing.Point(82, 66);
		this.tbrgTo.Name = "tbrgTo";
		this.tbrgTo.Size = new System.Drawing.Size(72, 21);
		this.tbrgTo.TabIndex = 1;
		this.tbrgFrom.Location = new System.Drawing.Point(82, 39);
		this.tbrgFrom.Name = "tbrgFrom";
		this.tbrgFrom.Size = new System.Drawing.Size(72, 21);
		this.tbrgFrom.TabIndex = 1;
		this.gbtaOffsetScale.Controls.Add(this.btnosOriginal);
		this.gbtaOffsetScale.Controls.Add(this.lbosScale);
		this.gbtaOffsetScale.Controls.Add(this.lbosOffset);
		this.gbtaOffsetScale.Controls.Add(this.tbosScale);
		this.gbtaOffsetScale.Controls.Add(this.tbosOffset);
		this.gbtaOffsetScale.Location = new System.Drawing.Point(217, 7);
		this.gbtaOffsetScale.Name = "gbtaOffsetScale";
		this.gbtaOffsetScale.Size = new System.Drawing.Size(192, 105);
		this.gbtaOffsetScale.TabIndex = 13;
		this.gbtaOffsetScale.TabStop = false;
		this.gbtaOffsetScale.Text = "偏移.缩放";
		this.btnosOriginal.Location = new System.Drawing.Point(82, 74);
		this.btnosOriginal.Name = "btnosOriginal";
		this.btnosOriginal.Size = new System.Drawing.Size(104, 23);
		this.btnosOriginal.TabIndex = 0;
		this.btnosOriginal.Text = "复位";
		this.btnosOriginal.UseVisualStyleBackColor = true;
		this.btnosOriginal.Click += new System.EventHandler(btnosOriginal_Click);
		this.lbosScale.AutoSize = true;
		this.lbosScale.Location = new System.Drawing.Point(17, 51);
		this.lbosScale.Name = "lbosScale";
		this.lbosScale.Size = new System.Drawing.Size(59, 12);
		this.lbosScale.TabIndex = 0;
		this.lbosScale.Text = "缩放";
		this.lbosOffset.AutoSize = true;
		this.lbosOffset.Location = new System.Drawing.Point(17, 24);
		this.lbosOffset.Name = "lbosOffset";
		this.lbosOffset.Size = new System.Drawing.Size(59, 12);
		this.lbosOffset.TabIndex = 0;
		this.lbosOffset.Text = "偏移";
		this.tbosScale.Location = new System.Drawing.Point(82, 47);
		this.tbosScale.Name = "tbosScale";
		this.tbosScale.Size = new System.Drawing.Size(72, 21);
		this.tbosScale.TabIndex = 1;
		this.tbosOffset.Location = new System.Drawing.Point(82, 20);
		this.tbosOffset.Name = "tbosOffset";
		this.tbosOffset.Size = new System.Drawing.Size(72, 21);
		this.tbosOffset.TabIndex = 1;
		this.tbtaUnits.Location = new System.Drawing.Point(12, 188);
		this.tbtaUnits.Name = "tbtaUnits";
		this.tbtaUnits.Size = new System.Drawing.Size(50, 21);
		this.tbtaUnits.TabIndex = 9;
		this.tbtaUnits.Text = "min.";
		this.tbtaTitle.Location = new System.Drawing.Point(15, 60);
		this.tbtaTitle.Name = "tbtaTitle";
		this.tbtaTitle.Size = new System.Drawing.Size(184, 21);
		this.tbtaTitle.TabIndex = 8;
		this.tbtaTitle.Text = "Time";
		this.lbtaDisUnit.AutoSize = true;
		this.lbtaDisUnit.Location = new System.Drawing.Point(10, 169);
		this.lbtaDisUnit.Name = "lbtaDisUnit";
		this.lbtaDisUnit.Size = new System.Drawing.Size(59, 12);
		this.lbtaDisUnit.TabIndex = 4;
		this.lbtaDisUnit.Text = "显示单位";
		this.lbtaTitle.AutoSize = true;
		this.lbtaTitle.Location = new System.Drawing.Point(10, 42);
		this.lbtaTitle.Name = "lbtaTitle";
		this.lbtaTitle.Size = new System.Drawing.Size(59, 12);
		this.lbtaTitle.TabIndex = 6;
		this.lbtaTitle.Text = "标题";
		this.tpSignalAxis.Controls.Add(this.cbsaVisible);
		this.tpSignalAxis.Controls.Add(this.gbsaRange);
		this.tpSignalAxis.Controls.Add(this.gbsaOffsetScale);
		this.tpSignalAxis.Controls.Add(this.tbsaUnits);
		this.tpSignalAxis.Controls.Add(this.tbsaTitle);
		this.tpSignalAxis.Controls.Add(this.lbsaDisUnit);
		this.tpSignalAxis.Controls.Add(this.lbsaTitle);
		this.tpSignalAxis.Location = new System.Drawing.Point(4, 23);
		this.tpSignalAxis.Name = "tpSignalAxis";
		this.tpSignalAxis.Size = new System.Drawing.Size(485, 273);
		this.tpSignalAxis.TabIndex = 2;
		this.tpSignalAxis.Text = "信号轴";
		this.tpSignalAxis.UseVisualStyleBackColor = true;
		this.cbsaVisible.AutoSize = true;
		this.cbsaVisible.Location = new System.Drawing.Point(12, 14);
		this.cbsaVisible.Name = "cbsaVisible";
		this.cbsaVisible.Size = new System.Drawing.Size(96, 16);
		this.cbsaVisible.TabIndex = 25;
		this.cbsaVisible.Text = "可见";
		this.cbsaVisible.UseVisualStyleBackColor = true;
		this.gbsaRange.Controls.Add(this.cbrgFixed2);
		this.gbsaRange.Controls.Add(this.btnrgGetCurrent2);
		this.gbsaRange.Controls.Add(this.lbrgTo2);
		this.gbsaRange.Controls.Add(this.lbrgFrom2);
		this.gbsaRange.Controls.Add(this.tbrgTo2);
		this.gbsaRange.Controls.Add(this.tbrgFrom2);
		this.gbsaRange.Location = new System.Drawing.Point(217, 118);
		this.gbsaRange.Name = "gbsaRange";
		this.gbsaRange.Size = new System.Drawing.Size(192, 123);
		this.gbsaRange.TabIndex = 23;
		this.gbsaRange.TabStop = false;
		this.gbsaRange.Text = "范围";
		this.cbrgFixed2.AutoSize = true;
		this.cbrgFixed2.Location = new System.Drawing.Point(82, 17);
		this.cbrgFixed2.Name = "cbrgFixed2";
		this.cbrgFixed2.Size = new System.Drawing.Size(96, 16);
		this.cbrgFixed2.TabIndex = 2;
		this.cbrgFixed2.Text = "固定";
		this.cbrgFixed2.UseVisualStyleBackColor = true;
		this.btnrgGetCurrent2.Location = new System.Drawing.Point(82, 93);
		this.btnrgGetCurrent2.Name = "btnrgGetCurrent2";
		this.btnrgGetCurrent2.Size = new System.Drawing.Size(104, 23);
		this.btnrgGetCurrent2.TabIndex = 0;
		this.btnrgGetCurrent2.Text = "提取当前值";
		this.btnrgGetCurrent2.UseVisualStyleBackColor = true;
		this.btnrgGetCurrent2.Click += new System.EventHandler(btnrgGetCurrent2_Click);
		this.lbrgTo2.AutoSize = true;
		this.lbrgTo2.Location = new System.Drawing.Point(17, 70);
		this.lbrgTo2.Name = "lbrgTo2";
		this.lbrgTo2.Size = new System.Drawing.Size(59, 12);
		this.lbrgTo2.TabIndex = 0;
		this.lbrgTo2.Text = "到:";
		this.lbrgFrom2.AutoSize = true;
		this.lbrgFrom2.Location = new System.Drawing.Point(17, 43);
		this.lbrgFrom2.Name = "lbrgFrom2";
		this.lbrgFrom2.Size = new System.Drawing.Size(59, 12);
		this.lbrgFrom2.TabIndex = 0;
		this.lbrgFrom2.Text = "从:";
		this.tbrgTo2.Location = new System.Drawing.Point(82, 66);
		this.tbrgTo2.Name = "tbrgTo2";
		this.tbrgTo2.Size = new System.Drawing.Size(72, 21);
		this.tbrgTo2.TabIndex = 1;
		this.tbrgFrom2.Location = new System.Drawing.Point(82, 39);
		this.tbrgFrom2.Name = "tbrgFrom2";
		this.tbrgFrom2.Size = new System.Drawing.Size(72, 21);
		this.tbrgFrom2.TabIndex = 1;
		this.gbsaOffsetScale.Controls.Add(this.btnosOriginal2);
		this.gbsaOffsetScale.Controls.Add(this.lbosScale2);
		this.gbsaOffsetScale.Controls.Add(this.lbosOffset2);
		this.gbsaOffsetScale.Controls.Add(this.tbosScale2);
		this.gbsaOffsetScale.Controls.Add(this.tbosOffset2);
		this.gbsaOffsetScale.Location = new System.Drawing.Point(217, 7);
		this.gbsaOffsetScale.Name = "gbsaOffsetScale";
		this.gbsaOffsetScale.Size = new System.Drawing.Size(192, 105);
		this.gbsaOffsetScale.TabIndex = 24;
		this.gbsaOffsetScale.TabStop = false;
		this.gbsaOffsetScale.Text = "偏移.缩放";
		this.btnosOriginal2.Location = new System.Drawing.Point(82, 74);
		this.btnosOriginal2.Name = "btnosOriginal2";
		this.btnosOriginal2.Size = new System.Drawing.Size(104, 23);
		this.btnosOriginal2.TabIndex = 0;
		this.btnosOriginal2.Text = "复位";
		this.btnosOriginal2.UseVisualStyleBackColor = true;
		this.btnosOriginal2.Click += new System.EventHandler(btnosOriginal2_Click);
		this.lbosScale2.AutoSize = true;
		this.lbosScale2.Location = new System.Drawing.Point(17, 51);
		this.lbosScale2.Name = "lbosScale2";
		this.lbosScale2.Size = new System.Drawing.Size(59, 12);
		this.lbosScale2.TabIndex = 0;
		this.lbosScale2.Text = "缩放";
		this.lbosOffset2.AutoSize = true;
		this.lbosOffset2.Location = new System.Drawing.Point(17, 24);
		this.lbosOffset2.Name = "lbosOffset2";
		this.lbosOffset2.Size = new System.Drawing.Size(59, 12);
		this.lbosOffset2.TabIndex = 0;
		this.lbosOffset2.Text = "偏移";
		this.tbosScale2.Location = new System.Drawing.Point(82, 47);
		this.tbosScale2.Name = "tbosScale2";
		this.tbosScale2.Size = new System.Drawing.Size(72, 21);
		this.tbosScale2.TabIndex = 1;
		this.tbosOffset2.Location = new System.Drawing.Point(82, 20);
		this.tbosOffset2.Name = "tbosOffset2";
		this.tbosOffset2.Size = new System.Drawing.Size(72, 21);
		this.tbosOffset2.TabIndex = 1;
		this.tbsaUnits.Location = new System.Drawing.Point(12, 188);
		this.tbsaUnits.Name = "tbsaUnits";
		this.tbsaUnits.Size = new System.Drawing.Size(50, 21);
		this.tbsaUnits.TabIndex = 20;
		this.tbsaUnits.Text = "V";
		this.tbsaTitle.Location = new System.Drawing.Point(15, 60);
		this.tbsaTitle.Name = "tbsaTitle";
		this.tbsaTitle.Size = new System.Drawing.Size(184, 21);
		this.tbsaTitle.TabIndex = 19;
		this.tbsaTitle.Text = "Voltage";
		this.lbsaDisUnit.AutoSize = true;
		this.lbsaDisUnit.Location = new System.Drawing.Point(10, 169);
		this.lbsaDisUnit.Name = "lbsaDisUnit";
		this.lbsaDisUnit.Size = new System.Drawing.Size(59, 12);
		this.lbsaDisUnit.TabIndex = 15;
		this.lbsaDisUnit.Text = "显示单位";
		this.lbsaTitle.AutoSize = true;
		this.lbsaTitle.Location = new System.Drawing.Point(10, 42);
		this.lbsaTitle.Name = "lbsaTitle";
		this.lbsaTitle.Size = new System.Drawing.Size(65, 12);
		this.lbsaTitle.TabIndex = 17;
		this.lbsaTitle.Text = "标题";
		this.tpSignalScale.Controls.Add(this.gbsigSignals);
		this.tpSignalScale.Controls.Add(this.cbsigSignals);
		this.tpSignalScale.Controls.Add(this.gbsigScaleYMode);
		this.tpSignalScale.Location = new System.Drawing.Point(4, 23);
		this.tpSignalScale.Name = "tpSignalScale";
		this.tpSignalScale.Size = new System.Drawing.Size(485, 273);
		this.tpSignalScale.TabIndex = 8;
		this.tpSignalScale.Text = "信号缩放";
		this.tpSignalScale.UseVisualStyleBackColor = true;
		this.gbsigSignals.Controls.Add(this.gbsglOffsetScale);
		this.gbsigSignals.Controls.Add(this.btnsglColor);
		this.gbsigSignals.Controls.Add(this.nudsglLineWidth);
		this.gbsigSignals.Controls.Add(this.lbsglLineWidth);
		this.gbsigSignals.Controls.Add(this.cbsglShowLabels);
		this.gbsigSignals.Controls.Add(this.cbsglShow);
		this.gbsigSignals.Location = new System.Drawing.Point(187, 44);
		this.gbsigSignals.Name = "gbsigSignals";
		this.gbsigSignals.Size = new System.Drawing.Size(282, 196);
		this.gbsigSignals.TabIndex = 4;
		this.gbsigSignals.TabStop = false;
		this.gbsigSignals.Text = "lclGroupBox6";
		this.gbsglOffsetScale.Controls.Add(this.tbosYScale);
		this.gbsglOffsetScale.Controls.Add(this.tbosXScale);
		this.gbsglOffsetScale.Controls.Add(this.tbosY);
		this.gbsglOffsetScale.Controls.Add(this.btnosOriginal3);
		this.gbsglOffsetScale.Controls.Add(this.tbosX);
		this.gbsglOffsetScale.Controls.Add(this.lbosYScale);
		this.gbsglOffsetScale.Controls.Add(this.lbosYUnit);
		this.gbsglOffsetScale.Controls.Add(this.lbosXScale);
		this.gbsglOffsetScale.Controls.Add(this.lbosY);
		this.gbsglOffsetScale.Controls.Add(this.lbosXUnit);
		this.gbsglOffsetScale.Controls.Add(this.lbosX);
		this.gbsglOffsetScale.Location = new System.Drawing.Point(120, 33);
		this.gbsglOffsetScale.Name = "gbsglOffsetScale";
		this.gbsglOffsetScale.Size = new System.Drawing.Size(157, 155);
		this.gbsglOffsetScale.TabIndex = 5;
		this.gbsglOffsetScale.TabStop = false;
		this.gbsglOffsetScale.Text = "平移.缩放";
		this.tbosYScale.Location = new System.Drawing.Point(60, 98);
		this.tbosYScale.Name = "tbosYScale";
		this.tbosYScale.Size = new System.Drawing.Size(53, 21);
		this.tbosYScale.TabIndex = 1;
		this.tbosXScale.Location = new System.Drawing.Point(60, 46);
		this.tbosXScale.Name = "tbosXScale";
		this.tbosXScale.Size = new System.Drawing.Size(53, 21);
		this.tbosXScale.TabIndex = 1;
		this.tbosY.Location = new System.Drawing.Point(60, 72);
		this.tbosY.Name = "tbosY";
		this.tbosY.Size = new System.Drawing.Size(53, 21);
		this.tbosY.TabIndex = 1;
		this.btnosOriginal3.Location = new System.Drawing.Point(60, 125);
		this.btnosOriginal3.Name = "btnosOriginal3";
		this.btnosOriginal3.Size = new System.Drawing.Size(72, 23);
		this.btnosOriginal3.TabIndex = 0;
		this.btnosOriginal3.Text = "复位";
		this.btnosOriginal3.UseVisualStyleBackColor = true;
		this.btnosOriginal3.Click += new System.EventHandler(btnosOriginal3_Click);
		this.tbosX.Location = new System.Drawing.Point(60, 20);
		this.tbosX.Name = "tbosX";
		this.tbosX.Size = new System.Drawing.Size(53, 21);
		this.tbosX.TabIndex = 1;
		this.lbosYScale.AutoSize = true;
		this.lbosYScale.Location = new System.Drawing.Point(6, 104);
		this.lbosYScale.Name = "lbosYScale";
		this.lbosYScale.Size = new System.Drawing.Size(65, 12);
		this.lbosYScale.TabIndex = 0;
		this.lbosYScale.Text = "Y缩放";
		this.lbosYUnit.AutoSize = true;
		this.lbosYUnit.Location = new System.Drawing.Point(115, 78);
		this.lbosYUnit.Name = "lbosYUnit";
		this.lbosYUnit.Size = new System.Drawing.Size(29, 12);
		this.lbosYUnit.TabIndex = 0;
		this.lbosYUnit.Text = "[mV]";
		this.lbosXScale.AutoSize = true;
		this.lbosXScale.Location = new System.Drawing.Point(6, 52);
		this.lbosXScale.Name = "lbosXScale";
		this.lbosXScale.Size = new System.Drawing.Size(65, 12);
		this.lbosXScale.TabIndex = 0;
		this.lbosXScale.Text = "X缩放";
		this.lbosY.AutoSize = true;
		this.lbosY.Location = new System.Drawing.Point(6, 78);
		this.lbosY.Name = "lbosY";
		this.lbosY.Size = new System.Drawing.Size(11, 12);
		this.lbosY.TabIndex = 0;
		this.lbosY.Text = "Y";
		this.lbosXUnit.AutoSize = true;
		this.lbosXUnit.Location = new System.Drawing.Point(115, 26);
		this.lbosXUnit.Name = "lbosXUnit";
		this.lbosXUnit.Size = new System.Drawing.Size(41, 12);
		this.lbosXUnit.TabIndex = 0;
		this.lbosXUnit.Text = "[min.]";
		this.lbosX.AutoSize = true;
		this.lbosX.Location = new System.Drawing.Point(6, 26);
		this.lbosX.Name = "lbosX";
		this.lbosX.Size = new System.Drawing.Size(11, 12);
		this.lbosX.TabIndex = 0;
		this.lbosX.Text = "X";
		this.btnsglColor.Color = System.Drawing.Color.Green;
		this.btnsglColor.Location = new System.Drawing.Point(11, 152);
		this.btnsglColor.Name = "btnsglColor";
		this.btnsglColor.Size = new System.Drawing.Size(100, 23);
		this.btnsglColor.TabIndex = 4;
		this.btnsglColor.Text = "颜色...";
		this.btnsglColor.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.btnsglColor.UseVisualStyleBackColor = true;
		this.nudsglLineWidth.Location = new System.Drawing.Point(11, 104);
		this.nudsglLineWidth.Name = "nudsglLineWidth";
		this.nudsglLineWidth.Size = new System.Drawing.Size(52, 21);
		this.nudsglLineWidth.TabIndex = 3;
		this.lbsglLineWidth.AutoSize = true;
		this.lbsglLineWidth.Location = new System.Drawing.Point(8, 85);
		this.lbsglLineWidth.Name = "lbsglLineWidth";
		this.lbsglLineWidth.Size = new System.Drawing.Size(65, 12);
		this.lbsglLineWidth.TabIndex = 2;
		this.lbsglLineWidth.Text = "线宽";
		this.cbsglShowLabels.AutoSize = true;
		this.cbsglShowLabels.Location = new System.Drawing.Point(8, 55);
		this.cbsglShowLabels.Name = "cbsglShowLabels";
		this.cbsglShowLabels.Size = new System.Drawing.Size(96, 16);
		this.cbsglShowLabels.TabIndex = 0;
		this.cbsglShowLabels.Text = "显示标签";
		this.cbsglShowLabels.UseVisualStyleBackColor = true;
		this.cbsglShow.AutoSize = true;
		this.cbsglShow.Location = new System.Drawing.Point(8, 33);
		this.cbsglShow.Name = "cbsglShow";
		this.cbsglShow.Size = new System.Drawing.Size(96, 16);
		this.cbsglShow.TabIndex = 0;
		this.cbsglShow.Text = "显示";
		this.cbsglShow.UseVisualStyleBackColor = true;
		this.cbsigSignals.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.cbsigSignals.FormattingEnabled = true;
		this.cbsigSignals.ItemExtString = "";
		this.cbsigSignals.Location = new System.Drawing.Point(195, 18);
		this.cbsigSignals.Name = "cbsigSignals";
		this.cbsigSignals.Size = new System.Drawing.Size(123, 20);
		this.cbsigSignals.TabIndex = 3;
		this.cbsigSignals.SelectedIndexChanged += new System.EventHandler(cbsigSignals_SelectedIndexChanged);
		this.gbsigScaleYMode.Controls.Add(this.cbsymScaleTo);
		this.gbsigScaleYMode.Controls.Add(this.lbsymScaleTo);
		this.gbsigScaleYMode.Controls.Add(this.pnlsymPreserve);
		this.gbsigScaleYMode.Controls.Add(this.rbsymSeperate);
		this.gbsigScaleYMode.Controls.Add(this.rbsymPreserve);
		this.gbsigScaleYMode.Location = new System.Drawing.Point(6, 38);
		this.gbsigScaleYMode.Name = "gbsigScaleYMode";
		this.gbsigScaleYMode.Size = new System.Drawing.Size(175, 168);
		this.gbsigScaleYMode.TabIndex = 1;
		this.gbsigScaleYMode.TabStop = false;
		this.gbsigScaleYMode.Text = "Y轴缩放模式";
		this.cbsymScaleTo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.cbsymScaleTo.FormattingEnabled = true;
		this.cbsymScaleTo.ItemExtString = "";
		this.cbsymScaleTo.Location = new System.Drawing.Point(6, 138);
		this.cbsymScaleTo.Name = "cbsymScaleTo";
		this.cbsymScaleTo.Size = new System.Drawing.Size(123, 20);
		this.cbsymScaleTo.TabIndex = 3;
		this.lbsymScaleTo.AutoSize = true;
		this.lbsymScaleTo.Location = new System.Drawing.Point(6, 123);
		this.lbsymScaleTo.Name = "lbsymScaleTo";
		this.lbsymScaleTo.Size = new System.Drawing.Size(65, 12);
		this.lbsymScaleTo.TabIndex = 2;
		this.lbsymScaleTo.Text = "缩放到:";
		this.pnlsymPreserve.Controls.Add(this.rbpsrActive);
		this.pnlsymPreserve.Controls.Add(this.rbpsrAll);
		this.pnlsymPreserve.Location = new System.Drawing.Point(6, 39);
		this.pnlsymPreserve.Name = "pnlsymPreserve";
		this.pnlsymPreserve.Size = new System.Drawing.Size(169, 44);
		this.pnlsymPreserve.TabIndex = 1;
		this.rbpsrActive.AutoSize = true;
		this.rbpsrActive.Location = new System.Drawing.Point(28, 25);
		this.rbpsrActive.Name = "rbpsrActive";
		this.rbpsrActive.Size = new System.Drawing.Size(113, 16);
		this.rbpsrActive.TabIndex = 0;
		this.rbpsrActive.TabStop = true;
		this.rbpsrActive.Text = "缩放当前信号";
		this.rbpsrActive.UseVisualStyleBackColor = true;
		this.rbpsrActive.Click += new System.EventHandler(rbpsrAll_Click);
		this.rbpsrAll.AutoSize = true;
		this.rbpsrAll.Location = new System.Drawing.Point(28, 3);
		this.rbpsrAll.Name = "rbpsrAll";
		this.rbpsrAll.Size = new System.Drawing.Size(113, 16);
		this.rbpsrAll.TabIndex = 0;
		this.rbpsrAll.TabStop = true;
		this.rbpsrAll.Text = "缩放所有信号";
		this.rbpsrAll.UseVisualStyleBackColor = true;
		this.rbpsrAll.Click += new System.EventHandler(rbpsrAll_Click);
		this.rbsymSeperate.AutoSize = true;
		this.rbsymSeperate.Location = new System.Drawing.Point(6, 86);
		this.rbsymSeperate.Name = "rbsymSeperate";
		this.rbsymSeperate.Size = new System.Drawing.Size(113, 16);
		this.rbsymSeperate.TabIndex = 0;
		this.rbsymSeperate.TabStop = true;
		this.rbsymSeperate.Text = "单独缩放信号";
		this.rbsymSeperate.UseVisualStyleBackColor = true;
		this.rbsymSeperate.Click += new System.EventHandler(rbsymPreserve_Click);
		this.rbsymPreserve.AutoSize = true;
		this.rbsymPreserve.Location = new System.Drawing.Point(8, 20);
		this.rbsymPreserve.Name = "rbsymPreserve";
		this.rbsymPreserve.Size = new System.Drawing.Size(113, 16);
		this.rbsymPreserve.TabIndex = 0;
		this.rbsymPreserve.TabStop = true;
		this.rbsymPreserve.Text = "保留信号相对性";
		this.rbsymPreserve.UseVisualStyleBackColor = true;
		this.rbsymPreserve.Click += new System.EventHandler(rbsymPreserve_Click);
		this.tpAuxiliary.Controls.Add(this.gbgfColors);
		this.tpAuxiliary.Controls.Add(this.lclGroupBox1);
		this.tpAuxiliary.Controls.Add(this.gbgaShowYAxisFor);
		this.tpAuxiliary.Location = new System.Drawing.Point(4, 23);
		this.tpAuxiliary.Name = "tpAuxiliary";
		this.tpAuxiliary.Size = new System.Drawing.Size(499, 306);
		this.tpAuxiliary.TabIndex = 4;
		this.tpAuxiliary.Text = "辅助";
		this.tpAuxiliary.UseVisualStyleBackColor = true;
		this.gbgfColors.Controls.Add(this.btnclrInitial);
		this.gbgfColors.Controls.Add(this.btnclrD);
		this.gbgfColors.Controls.Add(this.btnclrC);
		this.gbgfColors.Controls.Add(this.btnclrB);
		this.gbgfColors.Controls.Add(this.btnclrA);
		this.gbgfColors.Location = new System.Drawing.Point(141, 18);
		this.gbgfColors.Name = "gbgfColors";
		this.gbgfColors.Size = new System.Drawing.Size(142, 155);
		this.gbgfColors.TabIndex = 2;
		this.gbgfColors.TabStop = false;
		this.gbgfColors.Text = "颜色";
		this.btnclrInitial.Location = new System.Drawing.Point(8, 20);
		this.btnclrInitial.Name = "btnclrInitial";
		this.btnclrInitial.Size = new System.Drawing.Size(113, 23);
		this.btnclrInitial.TabIndex = 2;
		this.btnclrInitial.Text = "颜色初始化";
		this.btnclrInitial.UseVisualStyleBackColor = true;
		this.btnclrInitial.Click += new System.EventHandler(btnclrInitial_Click);
		this.btnclrD.Color = System.Drawing.Color.Green;
		this.btnclrD.Location = new System.Drawing.Point(8, 124);
		this.btnclrD.Name = "btnclrD";
		this.btnclrD.Size = new System.Drawing.Size(100, 23);
		this.btnclrD.TabIndex = 1;
		this.btnclrD.Text = "溶剂  D";
		this.btnclrD.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.btnclrD.UseVisualStyleBackColor = true;
		this.btnclrC.Color = System.Drawing.Color.Green;
		this.btnclrC.Location = new System.Drawing.Point(8, 99);
		this.btnclrC.Name = "btnclrC";
		this.btnclrC.Size = new System.Drawing.Size(100, 23);
		this.btnclrC.TabIndex = 1;
		this.btnclrC.Text = "溶剂  C";
		this.btnclrC.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.btnclrC.UseVisualStyleBackColor = true;
		this.btnclrB.Color = System.Drawing.Color.Green;
		this.btnclrB.Location = new System.Drawing.Point(8, 74);
		this.btnclrB.Name = "btnclrB";
		this.btnclrB.Size = new System.Drawing.Size(100, 23);
		this.btnclrB.TabIndex = 1;
		this.btnclrB.Text = "溶剂  B";
		this.btnclrB.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.btnclrB.UseVisualStyleBackColor = true;
		this.btnclrA.Color = System.Drawing.Color.Green;
		this.btnclrA.Location = new System.Drawing.Point(8, 49);
		this.btnclrA.Name = "btnclrA";
		this.btnclrA.Size = new System.Drawing.Size(100, 23);
		this.btnclrA.TabIndex = 1;
		this.btnclrA.Text = "溶剂  A";
		this.btnclrA.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.btnclrA.UseVisualStyleBackColor = true;
		this.lclGroupBox1.Controls.Add(this.rbgcTemp);
		this.lclGroupBox1.Controls.Add(this.rbgcDoNotShow);
		this.lclGroupBox1.Location = new System.Drawing.Point(9, 140);
		this.lclGroupBox1.Name = "lclGroupBox1";
		this.lclGroupBox1.Size = new System.Drawing.Size(126, 68);
		this.lclGroupBox1.TabIndex = 0;
		this.lclGroupBox1.TabStop = false;
		this.lclGroupBox1.Text = "气相辅助";
		this.rbgcTemp.AutoSize = true;
		this.rbgcTemp.Location = new System.Drawing.Point(6, 42);
		this.rbgcTemp.Name = "rbgcTemp";
		this.rbgcTemp.Size = new System.Drawing.Size(47, 16);
		this.rbgcTemp.TabIndex = 1;
		this.rbgcTemp.TabStop = true;
		this.rbgcTemp.Text = "温度";
		this.rbgcTemp.UseVisualStyleBackColor = true;
		this.rbgcDoNotShow.AutoSize = true;
		this.rbgcDoNotShow.Location = new System.Drawing.Point(6, 20);
		this.rbgcDoNotShow.Name = "rbgcDoNotShow";
		this.rbgcDoNotShow.Size = new System.Drawing.Size(71, 16);
		this.rbgcDoNotShow.TabIndex = 1;
		this.rbgcDoNotShow.TabStop = true;
		this.rbgcDoNotShow.Text = "(不显示)";
		this.rbgcDoNotShow.UseVisualStyleBackColor = true;
		this.gbgaShowYAxisFor.Controls.Add(this.rblcTotalFlow);
		this.gbgaShowYAxisFor.Controls.Add(this.rblcGradient);
		this.gbgaShowYAxisFor.Controls.Add(this.rblcDoNotShow);
		this.gbgaShowYAxisFor.Location = new System.Drawing.Point(9, 44);
		this.gbgaShowYAxisFor.Name = "gbgaShowYAxisFor";
		this.gbgaShowYAxisFor.Size = new System.Drawing.Size(126, 90);
		this.gbgaShowYAxisFor.TabIndex = 0;
		this.gbgaShowYAxisFor.TabStop = false;
		this.gbgaShowYAxisFor.Text = "液相辅助";
		this.rblcTotalFlow.AutoSize = true;
		this.rblcTotalFlow.Location = new System.Drawing.Point(6, 64);
		this.rblcTotalFlow.Name = "rblcTotalFlow";
		this.rblcTotalFlow.Size = new System.Drawing.Size(113, 16);
		this.rblcTotalFlow.TabIndex = 1;
		this.rblcTotalFlow.TabStop = true;
		this.rblcTotalFlow.Text = "总流速";
		this.rblcTotalFlow.UseVisualStyleBackColor = true;
		this.rblcGradient.AutoSize = true;
		this.rblcGradient.Location = new System.Drawing.Point(6, 42);
		this.rblcGradient.Name = "rblcGradient";
		this.rblcGradient.Size = new System.Drawing.Size(113, 16);
		this.rblcGradient.TabIndex = 1;
		this.rblcGradient.TabStop = true;
		this.rblcGradient.Text = "梯度";
		this.rblcGradient.UseVisualStyleBackColor = true;
		this.rblcDoNotShow.AutoSize = true;
		this.rblcDoNotShow.Location = new System.Drawing.Point(6, 20);
		this.rblcDoNotShow.Name = "rblcDoNotShow";
		this.rblcDoNotShow.Size = new System.Drawing.Size(113, 16);
		this.rblcDoNotShow.TabIndex = 1;
		this.rblcDoNotShow.TabStop = true;
		this.rblcDoNotShow.Text = "(不显示)";
		this.rblcDoNotShow.UseVisualStyleBackColor = true;
		this.btnApply.Location = new System.Drawing.Point(306, 353);
		this.btnApply.Name = "btnApply";
		this.btnApply.Size = new System.Drawing.Size(75, 23);
		this.btnApply.TabIndex = 2;
		this.btnApply.Text = "应用";
		this.btnApply.UseVisualStyleBackColor = true;
		this.btnApply.Click += new System.EventHandler(btnApply_Click);
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
		base.ClientSize = new System.Drawing.Size(516, 383);
		base.Controls.Add(this.btnApply);
		base.Controls.Add(this.tcUserOptions);
		base.Name = "OptionsDialog";
		this.Text = "谱图用户选项";
		base.FormClosing += new System.Windows.Forms.FormClosingEventHandler(OptionsDialog_FormClosing);
		base.Load += new System.EventHandler(OptionsDialog_Load);
		base.Controls.SetChildIndex(base.btnHelp, 0);
		base.Controls.SetChildIndex(base.btnOK, 0);
		base.Controls.SetChildIndex(base.btnCancel, 0);
		base.Controls.SetChildIndex(this.tcUserOptions, 0);
		base.Controls.SetChildIndex(this.btnApply, 0);
		this.tcUserOptions.ResumeLayout(false);
		this.tpGeneral.ResumeLayout(false);
		this.tpGeneral.PerformLayout();
		this.tpGraph.ResumeLayout(false);
		this.tcGraph.ResumeLayout(false);
		this.tpElements.ResumeLayout(false);
		this.tpElements.PerformLayout();
		this.gbgrpPeakTags.ResumeLayout(false);
		this.gbgrpPeakTags.PerformLayout();
		this.gbptPeakAreaColor.ResumeLayout(false);
		this.gbptPeakAreaColor.PerformLayout();
		this.gbgrpBackgroundColors.ResumeLayout(false);
		this.gbbcChart.ResumeLayout(false);
		this.gbbcChart.PerformLayout();
		this.gbbcBorder.ResumeLayout(false);
		this.gbbcBorder.PerformLayout();
		this.gbgrpBaseline.ResumeLayout(false);
		this.gbgrpBaseline.PerformLayout();
		this.gbblColor.ResumeLayout(false);
		this.gbblColor.PerformLayout();
		this.tpAxisAppear.ResumeLayout(false);
		this.tpAxisAppear.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.nudaaLineWidth).EndInit();
		this.gbaaColor.ResumeLayout(false);
		this.gbaaColor.PerformLayout();
		this.tpSignalColor.ResumeLayout(false);
		this.tpSignalColor.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.gvscAcquisition).EndInit();
		((System.ComponentModel.ISupportInitialize)this.gvscChromatogram).EndInit();
		this.gbscCalibCurve.ResumeLayout(false);
		this.gbscCalibCurve.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.nudscLine).EndInit();
		this.tpDis.ResumeLayout(false);
		this.tcDis.ResumeLayout(false);
		this.tpTimeAxis.ResumeLayout(false);
		this.tpTimeAxis.PerformLayout();
		this.gbgnlTimeAxisData.ResumeLayout(false);
		this.gbgnlTimeAxisData.PerformLayout();
		this.gbtaRange.ResumeLayout(false);
		this.gbtaRange.PerformLayout();
		this.gbtaOffsetScale.ResumeLayout(false);
		this.gbtaOffsetScale.PerformLayout();
		this.tpSignalAxis.ResumeLayout(false);
		this.tpSignalAxis.PerformLayout();
		this.gbsaRange.ResumeLayout(false);
		this.gbsaRange.PerformLayout();
		this.gbsaOffsetScale.ResumeLayout(false);
		this.gbsaOffsetScale.PerformLayout();
		this.tpSignalScale.ResumeLayout(false);
		this.gbsigSignals.ResumeLayout(false);
		this.gbsigSignals.PerformLayout();
		this.gbsglOffsetScale.ResumeLayout(false);
		this.gbsglOffsetScale.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.nudsglLineWidth).EndInit();
		this.gbsigScaleYMode.ResumeLayout(false);
		this.gbsigScaleYMode.PerformLayout();
		this.pnlsymPreserve.ResumeLayout(false);
		this.pnlsymPreserve.PerformLayout();
		this.tpAuxiliary.ResumeLayout(false);
		this.gbgfColors.ResumeLayout(false);
		this.lclGroupBox1.ResumeLayout(false);
		this.lclGroupBox1.PerformLayout();
		this.gbgaShowYAxisFor.ResumeLayout(false);
		this.gbgaShowYAxisFor.PerformLayout();
		base.ResumeLayout(false);
	}
}
