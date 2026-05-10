using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using IBrainChrom2018.Unit;

namespace IBrainChrom2018;

public class ChromAcqCtrlPortable : UserControl
{
	private SystemParam sysParam = SystemParam.Create();

	private FormMainParam frmParam = FormMainParam.Create();

	private ChromDeviceListMgr cdlMgr;

	public string oldMaskedTextBox7 = "";

	public int cntFreeze = 0;

	public bool bFreeze = true;

	public SampleDisplay sampleDisplay;

	public bool AutoTempCtr = true;

	public int CountTempCtr = 0;

	public int CountState = 0;

	public ushort StateInstrument = 1;

	public DisLg disLg;

	private RectangleF rectangleF_0;

	public bool m_bAllowUpdateChromGraphic = true;

	public bool m_bAllowDisplyAll = true;

	private bool m_bLoading = true;

	private bool bOnlineMethod;

	private int m_iTimeNumber = 0;

	private int m_iTimeCount = 5;

	private bool bLYTHCMethod = false;

	private bool bRNMode;

	private IContainer components = null;

	private CheckBox cbFullScreem;

	private Button button26;

	public MaskedTextBox maskedTextBox6;

	private Button button25;

	private ToolStrip toolStrip1;

	public ToolStripButton tsStart;

	private ToolStripSeparator toolStripSeparator1;

	public ToolStripButton tsstop;

	private ToolStripButton toolStripButton6;

	private ToolStripSeparator toolStripSeparator2;

	private ToolStripButton toolStripButton4;

	private ToolStripButton toolStripButton5;

	private ToolStripSeparator toolStripSeparator3;

	private ToolStripButton toolStripButton7;

	private ToolStripButton toolStripButton1;

	private ToolStripSeparator toolStripSeparator4;

	private ToolStripButton toolStripButton10;

	private Label label23;

	private Label label20;

	public MaskedTextBox maskedTextBox7;

	private Label label22;

	public Label label21;

	private Button btnTimeFull;

	public CheckBox cbDisPlayAll;

	public LclCheckBox lclCPauseRefresh;

	private CheckBox checkBox2;

	private CheckBox checkBox3;

	public MaskedTextBox maskedTextBox11;

	private Label label34;

	private Label label32;

	private MaskedTextBox tbSigYEnd;

	private Label label33;

	private Label label116;

	private MaskedTextBox maskedTextBox13;

	private Label label31;

	private MaskedTextBox tbSigYBeg;

	private Label label115;

	public Label label29;

	private Label label119;

	private Label label27;

	private LclDisplayPanel dpDatAcq;

	public TextBox tbinsSerial;

	private System.Windows.Forms.Timer timer_0;

	private ContextMenuStrip cmPeakInfo;

	private ToolStripMenuItem 峰尺寸ToolStripMenuItem;

	private DropDownButton ddbXYFull;

	private BarManager barManager1;

	private BarDockControl barDockControlTop;

	private BarDockControl barDockControlBottom;

	private BarDockControl barDockControlLeft;

	private BarDockControl barDockControlRight;

	private PopupMenu pomCaptureAndCutting;

	private CheckBox checkBox8;

	private BarCheckItem bcYAuto;

	public Button button37;

	public Button button38;

	public Button bControlTemp;

	public Label labFireState;

	public Button bFunctionSelect;

	public SplitContainer splitContainer3;

	public MaskedTextBox maskedTextBox12;

	public CheckBox checkBox5;

	public MaskedTextBox tbTime;

	public bool ShowLYTHCMethod
	{
		get
		{
			return bLYTHCMethod;
		}
		set
		{
			bLYTHCMethod = value;
			if (bLYTHCMethod)
			{
				toolStrip1.Visible = false;
				bFunctionSelect.Visible = false;
				button25.Visible = false;
				button26.Visible = false;
				splitContainer3.Panel1Collapsed = true;
				splitContainer3.Height = 20;
				ddbXYFull.Location = new Point(2, 8);
			}
		}
	}

	public bool ShowRNMode
	{
		get
		{
			return bRNMode;
		}
		set
		{
			bRNMode = value;
			if (bRNMode)
			{
				toolStripButton4.Visible = false;
				toolStripButton5.Visible = false;
				toolStripButton10.Visible = false;
				button37.Location = new Point(210, 0);
				button38.Location = new Point(260, 0);
			}
		}
	}

	public bool ShowOnlineMethod
	{
		get
		{
			return bOnlineMethod;
		}
		set
		{
			bOnlineMethod = value;
			if (bOnlineMethod)
			{
				toolStripButton4.Visible = false;
				toolStripButton5.Visible = false;
				toolStripButton10.Visible = false;
				button25.Visible = false;
				button26.Visible = false;
				checkBox2.Visible = false;
				checkBox8.Visible = false;
				label20.Location = new Point(220, 13);
				maskedTextBox6.Location = new Point(260, 8);
				maskedTextBox6.Size = new Size(60, 21);
				maskedTextBox12.Location = new Point(320, 8);
				maskedTextBox12.Size = new Size(60, 21);
				label21.Location = new Point(350, 13);
				label23.Location = new Point(380, 13);
				maskedTextBox7.Location = new Point(410, 8);
				label22.Location = new Point(475, 13);
				button37.Location = new Point(500, 2);
				button38.Location = new Point(540, 2);
				bControlTemp.Location = new Point(590, 1);
				labFireState.Location = new Point(590, 23);
			}
		}
	}

	public static bool IsDesignMode()
	{
		return false;
	}

	public ChromAcqCtrlPortable()
	{
		InitializeComponent();
		if (!IsDesignMode())
		{
			disLg = default(DisLg);
			cdlMgr = ChromDeviceListMgr.Create();
			LoadLanguage();
			try
			{
				FrameDis.font = new Font("Tahoma", 8f);
			}
			catch
			{
				FrameDis.font = (Font)Font.Clone();
			}
			User user = new User();
			user.options.InitGradientColors();
			user.LoadUserOptions();
			sampleDisplay = new SampleDisplay(WinStyle.DataAcq, dpDatAcq);
			sampleDisplay.IsDataAcq = true;
			sampleDisplay.showMouseLgValue = false;
			sampleDisplay.showProgTemp = false;
			sampleDisplay.LinkOptions(user.options);
			sampleDisplay.ShowBgChrom = true;
			sampleDisplay.setShowGrid = true;
			sampleDisplay.OnSignalDoubleClick += method_11;
			method_9();
			tbTime.Text = frmParam.fTabChannel1.ToString();
			m_bLoading = false;
		}
	}

	public void LoadLanguage()
	{
		峰尺寸ToolStripMenuItem.Text = Lang.PS("峰尺寸...", "PeakInfo...");
		tsStart.ToolTipText = Lang.PS("开始采集", "start collecting ");
		tsstop.ToolTipText = Lang.PS("停止采集", "stop collecting");
		toolStripButton6.ToolTipText = Lang.PS("放弃采集", "Give up collecting");
		toolStripButton4.ToolTipText = Lang.PS("上一视图", "last view");
		toolStripButton5.ToolTipText = Lang.PS("下一视图", "next view");
		toolStripButton7.ToolTipText = Lang.PS("谱图文件命名", "file name ");
		toolStripButton1.ToolTipText = Lang.PS("检测器设置", "detector set ");
		toolStripButton10.ToolTipText = Lang.PS("基线扣除", "baseline");
		label20.Text = Lang.PS("信号:", "CurrentV:");
		label23.Text = Lang.PS("时间:", "Time:");
		bFunctionSelect.Text = Lang.PS("仪器设置", "Instrument Management");
		checkBox2.Text = Lang.PS("调零", "ZeroSet");
		lclCPauseRefresh.Text = Lang.PS("暂停刷新", "PauseRefreshing");
		label119.Text = Lang.PS("下限:", "lower:");
		label27.Text = Lang.PS("上限:", "upper:");
		label32.Text = Lang.PS("满屏时间:", "FScrTime:");
		label34.Text = Lang.PS("停止时间:", "StopTime:");
		checkBox3.Text = Lang.PS("结束后显示", "Shown end");
		checkBox5.Text = Lang.PS("结束后打印", "Print end");
		cbDisPlayAll.Text = Lang.PS("全显", "DisAll");
		ddbXYFull.Text = Lang.PS("Y满屏", "YFull");
		btnTimeFull.Text = Lang.PS("X满屏", "XFull");
		cbFullScreem.Text = Lang.PS("全屏", "FullScreen");
	}

	public void RefrushDisplayUnit()
	{
		if (cdlMgr.formMain.tabChannel.SelectedTab != null)
		{
			if (cdlMgr.formMain.tabChannel.SelectedTab.Text.Trim().StartsWith("FID"))
			{
				sampleDisplay.unitY = "pA";
				label21.Text = "pA";
				label29.Text = "pA";
			}
			else if (cdlMgr.formMain.tabChannel.SelectedTab.Text.Trim().StartsWith("PDD"))
			{
				sampleDisplay.unitY = "pA";
				label21.Text = "pA";
				label29.Text = "pA";
			}
			else
			{
				sampleDisplay.unitY = "mv";
				label21.Text = "mv";
				label29.Text = "mv";
			}
		}
	}

	public void SetDisplayUnit(int iUnitType)
	{
		if (iUnitType == 0)
		{
			sampleDisplay.unitY = "pA";
			label21.Text = "pA";
			label29.Text = "pA";
		}
		if (!cdlMgr.formMain.tabChannel.SelectedTab.Text.Trim().StartsWith("FID") && !cdlMgr.formMain.tabChannel.SelectedTab.Text.Trim().StartsWith("PDD"))
		{
			sampleDisplay.unitY = "mV";
			label21.Text = "mV";
			label29.Text = "mV";
		}
	}

	public void UpdateControlTempText(bool bCtrl)
	{
		if (bCtrl)
		{
			bControlTemp.Text = Lang.PS("关闭控温", "Stop Temp");
		}
		else
		{
			bControlTemp.Text = Lang.PS("开始控温", "Start Temp");
		}
	}

	public void LoadChannelChartPara(ChannelChartPara oneEquipPara)
	{
		m_bLoading = true;
		TcpServerSocket currentTcpServerSocket = cdlMgr.CurrentTcpServerSocket;
		int currentChannelIdx = cdlMgr.CurrentChannelIdx;
		if (currentTcpServerSocket == null)
		{
			return;
		}
		if (currentTcpServerSocket.sglsSampling[currentChannelIdx].simple)
		{
			tsStart.Enabled = false;
			tsstop.Enabled = true;
		}
		else
		{
			tsStart.Enabled = true;
			tsstop.Enabled = false;
		}
		if (!currentTcpServerSocket.sglsSampling[0].simple && !currentTcpServerSocket.sglsSampling[1].simple && !currentTcpServerSocket.sglsSampling[2].simple && !currentTcpServerSocket.sglsSampling[3].simple)
		{
			currentTcpServerSocket.mForm.insDeviceCtrl.UpdateControlAnalyzeText(bCtrl: false);
		}
		else
		{
			currentTcpServerSocket.mForm.insDeviceCtrl.UpdateControlAnalyzeText(bCtrl: true);
		}
		bcYAuto.Checked = oneEquipPara.bAutoFullY;
		checkBox2.Checked = oneEquipPara.bClearZero;
		m_bAllowDisplyAll = false;
		cbDisPlayAll.Checked = oneEquipPara.bFullScreen;
		m_bAllowDisplyAll = true;
		checkBox3.Checked = oneEquipPara.analysisWhenStop;
		checkBox5.Checked = oneEquipPara.printWhenStop;
		checkBox8.Checked = oneEquipPara.bBaselineDeduction;
		tbSigYBeg.Text = oneEquipPara.showLowLimit.ToString();
		tbSigYEnd.Text = oneEquipPara.showHighLimit.ToString();
		tbTime.Text = oneEquipPara.fullScreenTime.ToString();
		maskedTextBox11.Text = oneEquipPara.stopTime.ToString();
		for (int i = 0; i < currentTcpServerSocket.dtc_Channels.Length; i++)
		{
			oneEquipPara = cdlMgr.CurrentChromDevice.misMgr.GetChannelChartPara(i);
			currentTcpServerSocket.dtc_Channels[i].chromInfoR.AcqRunTime = oneEquipPara.stopTime;
			currentTcpServerSocket.sglsSampling[i].StopAutoAlalyse = oneEquipPara.analysisWhenStop;
			currentTcpServerSocket.sglsSampling[i].StopAutoPrint = oneEquipPara.printWhenStop;
		}
		maskedTextBox6.Text = currentTcpServerSocket.sglsSampling[currentChannelIdx].ZeroUY.ToString();
		m_bAllowUpdateChromGraphic = true;
		RefrushDisplayUnit();
		disLg = GetDisLg();
		if (sampleDisplay.disSignals[currentChannelIdx].disLg.lgXBeg + sampleDisplay.disSignals[currentChannelIdx].disLg.LgXEnd == 0f)
		{
			if (cbDisPlayAll.Checked)
			{
				sampleDisplay.stDisChain.AppendFrameLg(disLg);
				DisDpRefresh();
			}
			else if (!cbDisPlayAll.Checked)
			{
				sampleDisplay.stDisChain.AppendFrameLg(disLg);
				DisDpRefresh();
			}
			sampleDisplay.displayPanel.Refresh();
		}
		else
		{
			disLg = sampleDisplay.disSignals[currentChannelIdx].disLg;
			sampleDisplay.stDisChain.AppendFrameLg(disLg);
			sampleDisplay.disLg = disLg;
		}
		method_12();
		button26.Enabled = currentTcpServerSocket.AutoQ;
		m_bLoading = false;
	}

	public DisLg GetDisLg()
	{
		if (tbTime.Text.Trim() == "" || tbSigYBeg.Text.Trim() == "" || tbSigYEnd.Text.Trim() == "")
		{
			return disLg;
		}
		float num = Class49.String2Float(tbTime.Text, disLg.lgX);
		if (num <= 0f)
		{
			num = 0.001f;
		}
		if (num > 600f)
		{
			num = 600f;
		}
		float num2 = Class49.String2Float(tbSigYBeg.Text, disLg.lgYBeg);
		if ((double)num2 < -9999.999)
		{
			num2 = -9999.999f;
		}
		float num3 = Class49.String2Float(tbSigYEnd.Text, disLg.lgYBeg + disLg.lgY);
		if ((double)num3 < -9999.999)
		{
			num3 = -9999.999f;
		}
		if (num < 0.1f || (num2 == 0f && num3 == 0f))
		{
			disLg.lgXBeg = 0f;
			num = 0.2f;
			num2 = -1f;
			num3 = 10f;
		}
		disLg.lgXBeg = 0f;
		disLg.lgX = num;
		disLg.lgYBeg = num2;
		disLg.lgY = num3 - num2;
		return disLg;
	}

	private void timer_0_Tick(object sender, EventArgs e)
	{
		if (IsDesignMode())
		{
			return;
		}
		dpDatAcq.Refresh();
		if (cdlMgr.formMain.tabChannel.TabCount == 0)
		{
			TcpServerSocket currentTcpServerSocket = cdlMgr.CurrentTcpServerSocket;
			if (currentTcpServerSocket != null && Environment.TickCount - currentTcpServerSocket.beginIdleTC < 3000)
			{
				cdlMgr.formMain.SetCurrentChromDevice();
			}
		}
		else
		{
			if (cdlMgr.CurrentChannelChartPara == null)
			{
				return;
			}
			TcpServerSocket currentTcpServerSocket2 = cdlMgr.CurrentTcpServerSocket;
			if (currentTcpServerSocket2 == null)
			{
				return;
			}
			if (m_iTimeNumber > m_iTimeCount)
			{
				int currentChannelIdx = cdlMgr.CurrentChannelIdx;
				currentTcpServerSocket2.sglsSampling[currentChannelIdx].refresh_TimeValue();
				float num = currentTcpServerSocket2.sglsSampling[currentChannelIdx].yMaxValue - currentTcpServerSocket2.sglsSampling[currentChannelIdx].yMinValue;
				if (num < (float)sysParam.iDispMinValue)
				{
					currentTcpServerSocket2.sglsSampling[currentChannelIdx].yMaxValue = currentTcpServerSocket2.sglsSampling[currentChannelIdx].yMaxValue + (float)sysParam.iDispMinValue * 0.75f;
					currentTcpServerSocket2.sglsSampling[currentChannelIdx].yMinValue = currentTcpServerSocket2.sglsSampling[currentChannelIdx].yMinValue - (float)sysParam.iDispMinValue * 0.15f;
				}
				num *= 0.02f;
				tbSigYBeg.Text = (currentTcpServerSocket2.sglsSampling[currentChannelIdx].yMinValue - num).ToString("0.00");
				tbSigYEnd.Text = (currentTcpServerSocket2.sglsSampling[currentChannelIdx].yMaxValue + num).ToString("0.00");
				m_iTimeNumber = 0;
			}
			else
			{
				m_iTimeNumber++;
			}
		}
	}

	private void dpDatAcq_Paint(object sender, PaintEventArgs e)
	{
		if (sampleDisplay == null)
		{
			return;
		}
		try
		{
			sampleDisplay.Draw(e.Graphics, erase: true);
		}
		catch
		{
		}
	}

	private void dpDatAcq_MouseDown(object sender, MouseEventArgs e)
	{
		if (sampleDisplay != null)
		{
			if (e.Button == MouseButtons.Left)
			{
				sampleDisplay.ptScaleBegin = e.Location;
			}
			if (e.Button == MouseButtons.Right)
			{
				sampleDisplay.ptMouseRight = e.Location;
			}
		}
	}

	private void dpDatAcq_MouseMove(object sender, MouseEventArgs e)
	{
		if (sampleDisplay == null)
		{
			return;
		}
		if (e.Button == MouseButtons.Left)
		{
			sampleDisplay.scaling = true;
			dpDatAcq.Refresh();
		}
		if (e.Button == MouseButtons.Right)
		{
			dpDatAcq.Cursor = Cursors.SizeAll;
			if (!sampleDisplay.moving)
			{
				sampleDisplay.stDisChain.MustAppendFrameLg(disLg);
			}
			Size szScr = new Size(e.X - sampleDisplay.mouseLocation.X, e.Y - sampleDisplay.mouseLocation.Y);
			SizeF sizeF = sampleDisplay.scrToLg(szScr, bool_0: true);
			disLg.lgXBeg -= sizeF.Width;
			disLg.lgYBeg += sizeF.Height;
			sampleDisplay.moving = true;
			sampleDisplay.stDisChain.ReplaceCurFrameLg(disLg);
			dpDatAcq.Refresh();
		}
		if (sampleDisplay != null)
		{
			sampleDisplay.mouseLocation = e.Location;
		}
	}

	private void dpDatAcq_MouseUp(object sender, MouseEventArgs e)
	{
		dpDatAcq.Cursor = Cursors.Default;
		if (sampleDisplay != null)
		{
			if (sampleDisplay.scaling && Math.Abs(sampleDisplay.ptScaleBegin.X - sampleDisplay.mouseLocation.X) > 10 && Math.Abs(sampleDisplay.ptScaleBegin.Y - sampleDisplay.mouseLocation.Y) > 10)
			{
				PointF pointF = sampleDisplay.scrToLg(sampleDisplay.ptScaleBegin, bool_0: true);
				PointF pointF2 = sampleDisplay.scrToLg(sampleDisplay.mouseLocation, bool_0: true);
				rectangleF_0.X = Math.Min(pointF.X, pointF2.X);
				rectangleF_0.Y = Math.Min(pointF.Y, pointF2.Y);
				rectangleF_0.Width = Math.Max(pointF.X, pointF2.X) - rectangleF_0.X;
				rectangleF_0.Height = Math.Max(pointF.Y, pointF2.Y) - rectangleF_0.Y;
				method_10(rectangleF_0.X, rectangleF_0.Width, rectangleF_0.Y, rectangleF_0.Height);
			}
			sampleDisplay.moving = false;
			sampleDisplay.scaling = false;
			dpDatAcq.Refresh();
		}
	}

	private void comboBFun_SelectedIndexChanged(object sender, EventArgs e)
	{
		if (!m_bLoading)
		{
		}
	}

	private void cbFullScreem_CheckedChanged(object sender, EventArgs e)
	{
		cdlMgr.formMain.SetShowFullScreen(cbFullScreem.Checked);
	}

	private void bFunctionSelect_Click(object sender, EventArgs e)
	{
		if (bFunctionSelect.Text == Lang.PS("方法设置", "MethodSet"))
		{
			cdlMgr.formMain.SetShowMainmstSet(bShow: true);
			bFunctionSelect.Text = Lang.PS("仪器设置", "Instrument Management");
		}
		else
		{
			cdlMgr.formMain.SetShowMainmstSet(bShow: false);
			bFunctionSelect.Text = Lang.PS("方法设置", "MethodSet");
		}
	}

	private void button26_KeyDown(object sender, KeyEventArgs e)
	{
	}

	private void button25_KeyDown(object sender, KeyEventArgs e)
	{
	}

	public void toolStripButton1_Click(object sender, EventArgs e)
	{
		if (Class49.user_0.ULevel != User.Level.访问员)
		{
			cdlMgr.CurrentTcpServerSocket?.SendCmd(22);
			return;
		}
		MessageBox.Show(Lang.PS("没有启动分析权限！", "No boot analysis authority "));
	}

	public void toolStripButton3_Click(object sender, EventArgs e)
	{
		if (cdlMgr.formMain.IsAutoCalibra != 0)
		{
			MessageBox.Show("标定未结束，不能停止！");
		}
		else
		{
			cdlMgr.CurrentTcpServerSocket?.SendCmd(23);
		}
	}

	public void toolStripButton6_Click(object sender, EventArgs e)
	{
		if (cdlMgr.formMain.IsAutoCalibra != 0)
		{
			MessageBox.Show("标定未结束，不能放弃！");
			return;
		}
		TcpServerSocket oneInstrum = cdlMgr.CurrentTcpServerSocket;
		if (oneInstrum == null)
		{
			return;
		}
		oneInstrum.SendCmd(244);
		int currentChannelIdx = cdlMgr.CurrentChannelIdx;
		oneInstrum.sglsSampling[currentChannelIdx].ResetOriDots(createDiskFile: true);
		oneInstrum.bgChrom.signal.ClearPeak();
		oneInstrum.sglsSampling[currentChannelIdx].simple = false;
		tsStart.Enabled = true;
		tsstop.Enabled = false;
		for (int i = 0; i < oneInstrum.sglsSampling.Length; i++)
		{
			if (i >= cdlMgr.ChannelCount)
			{
				oneInstrum.sglsSampling[i].simple = false;
			}
		}
		oneInstrum.mForm.Invoke((MethodInvoker)delegate
		{
			for (int j = 0; j < oneInstrum.sglsSampling.Length; j++)
			{
				if (j >= oneInstrum.mForm.tabChannel.TabPages.Count)
				{
					oneInstrum.sglsSampling[j].simple = false;
				}
			}
			if (!oneInstrum.sglsSampling[0].simple && !oneInstrum.sglsSampling[1].simple && !oneInstrum.sglsSampling[2].simple && !oneInstrum.sglsSampling[3].simple && oneInstrum.mForm.CurrentGCID == oneInstrum.ID)
			{
				oneInstrum.mForm.insDeviceCtrl.UpdateControlAnalyzeText(bCtrl: false);
				if (minePlot.minePlotSelf != null)
				{
					minePlot.minePlotSelf.UpdateControlAnalyzeText(bCtrl: false);
				}
			}
		});
		tbTime_DoubleClick(null, null);
	}

	private void toolStripButton4_Click(object sender, EventArgs e)
	{
		sampleDisplay.stDisChain.DynNo--;
		disLg = sampleDisplay.stDisChain.CurDisLg;
	}

	private void toolStripButton5_Click(object sender, EventArgs e)
	{
		sampleDisplay.stDisChain.DynNo++;
		disLg = sampleDisplay.stDisChain.CurDisLg;
	}

	private void toolStripButton7_Click(object sender, EventArgs e)
	{
		FrmDisposePara frmDisposePara = new FrmDisposePara();
		frmDisposePara.Text = Lang.PS("基线扣除及文件命名设置", "Baseline deduction and file naming settings");
		frmDisposePara.Init(cdlMgr.formMain, 0);
		frmDisposePara.Show();
	}

	private void toolStripButton1_Click_1(object sender, EventArgs e)
	{
		TcpServerSocket currentTcpServerSocket = cdlMgr.CurrentTcpServerSocket;
		if (currentTcpServerSocket != null)
		{
			int currentChannelIdx = cdlMgr.CurrentChannelIdx;
			if (currentChannelIdx != 3)
			{
				currentTcpServerSocket.ShowDtcrForm(currentChannelIdx, cdlMgr.formMain.tabChannel.SelectedTab.Text.Trim());
				Thread.Sleep(100);
				currentTcpServerSocket.SendCmd(13);
			}
		}
	}

	private void toolStripButton10_Click(object sender, EventArgs e)
	{
		FrmDisposePara frmDisposePara = new FrmDisposePara();
		frmDisposePara.Text = Lang.PS("基线扣除及文件命名设置", "Baseline deduction and file naming settings");
		frmDisposePara.Init(cdlMgr.formMain, 10);
		frmDisposePara.Show();
	}

	private void btnTimeFull_Click(object sender, EventArgs e)
	{
		TcpServerSocket currentTcpServerSocket = cdlMgr.CurrentTcpServerSocket;
		if (currentTcpServerSocket != null)
		{
			int currentChannelIdx = cdlMgr.CurrentChannelIdx;
			currentTcpServerSocket.sglsSampling[currentChannelIdx].refresh_TimeValue();
			tbTime.Text = currentTcpServerSocket.sglsSampling[currentChannelIdx].xMaxTime.ToString("0.0");
		}
	}

	private void cbDisPlayAll_CheckedChanged(object sender, EventArgs e)
	{
		if (m_bLoading)
		{
			return;
		}
		sampleDisplay.ShowAll = cbDisPlayAll.Checked;
		if (!m_bAllowDisplyAll)
		{
			return;
		}
		method_12();
		if (cdlMgr.CurrentTcpServerSocket != null)
		{
			ChannelChartPara currentChannelChartPara = cdlMgr.CurrentChannelChartPara;
			if (cbDisPlayAll.Checked)
			{
				currentChannelChartPara.bFullScreen = true;
			}
			else
			{
				currentChannelChartPara.bFullScreen = false;
			}
		}
	}

	private void checkBox5_CheckedChanged(object sender, EventArgs e)
	{
		if (m_bLoading)
		{
			return;
		}
		TcpServerSocket currentTcpServerSocket = cdlMgr.CurrentTcpServerSocket;
		if (currentTcpServerSocket != null)
		{
			int currentChannelIdx = cdlMgr.CurrentChannelIdx;
			ChannelChartPara currentChannelChartPara = cdlMgr.CurrentChannelChartPara;
			if (checkBox5.Checked)
			{
				currentTcpServerSocket.sglsSampling[currentChannelIdx].StopAutoPrint = true;
				currentChannelChartPara.printWhenStop = true;
			}
			else
			{
				currentTcpServerSocket.sglsSampling[currentChannelIdx].StopAutoPrint = false;
				currentChannelChartPara.printWhenStop = false;
			}
		}
	}

	private void checkBox3_CheckedChanged(object sender, EventArgs e)
	{
		if (m_bLoading)
		{
			return;
		}
		TcpServerSocket currentTcpServerSocket = cdlMgr.CurrentTcpServerSocket;
		if (currentTcpServerSocket != null)
		{
			int currentChannelIdx = cdlMgr.CurrentChannelIdx;
			ChannelChartPara currentChannelChartPara = cdlMgr.CurrentChannelChartPara;
			if (checkBox3.Checked)
			{
				currentTcpServerSocket.sglsSampling[currentChannelIdx].StopAutoAlalyse = true;
				currentChannelChartPara.analysisWhenStop = true;
			}
			else
			{
				currentTcpServerSocket.sglsSampling[currentChannelIdx].StopAutoAlalyse = false;
				currentChannelChartPara.analysisWhenStop = false;
			}
		}
	}

	private void checkBox8_CheckedChanged(object sender, EventArgs e)
	{
		if (m_bLoading)
		{
			return;
		}
		TcpServerSocket currentTcpServerSocket = cdlMgr.CurrentTcpServerSocket;
		if (currentTcpServerSocket == null)
		{
			return;
		}
		int currentChannelIdx = cdlMgr.CurrentChannelIdx;
		ChannelChartPara currentChannelChartPara = cdlMgr.CurrentChannelChartPara;
		if (checkBox8.Checked)
		{
			if (!(cdlMgr.CurrentChartParaOpera.TemplatePath.Trim() == ""))
			{
				currentTcpServerSocket.sglsSampling[currentChannelIdx].baseLinededuct = true;
				currentChannelChartPara.bBaselineDeduction = true;
			}
			else
			{
				MessageBox.Show(Lang.PS("请先设置基线文件！", "Please set the baseline file !"));
			}
		}
		else
		{
			currentTcpServerSocket.sglsSampling[currentChannelIdx].baseLinededuct = false;
			currentChannelChartPara.bBaselineDeduction = false;
		}
	}

	private void checkBox2_CheckedChanged(object sender, EventArgs e)
	{
		if (m_bLoading)
		{
			return;
		}
		TcpServerSocket currentTcpServerSocket = cdlMgr.CurrentTcpServerSocket;
		if (currentTcpServerSocket == null)
		{
			return;
		}
		int currentChannelIdx = cdlMgr.CurrentChannelIdx;
		ChannelChartPara currentChannelChartPara = cdlMgr.CurrentChannelChartPara;
		if (checkBox2.Checked)
		{
			currentTcpServerSocket.sglsSampling[currentChannelIdx].ZeroUYCan = true;
			currentTcpServerSocket.sglsSampling[currentChannelIdx].ZeroUY = Class49.String2Float(maskedTextBox6.Text, 0f);
			currentChannelChartPara.bClearZero = true;
		}
		else
		{
			currentTcpServerSocket.sglsSampling[currentChannelIdx].ZeroUYCan = false;
			currentTcpServerSocket.sglsSampling[currentChannelIdx].ZeroUY = 0f;
			currentChannelChartPara.bClearZero = false;
		}
		if (checkBox8.Checked)
		{
			if (!(cdlMgr.CurrentChartParaOpera.TemplatePath.Trim() == ""))
			{
				currentTcpServerSocket.sglsSampling[currentChannelIdx].baseLinededuct = true;
				currentChannelChartPara.bBaselineDeduction = true;
			}
			else
			{
				MessageBox.Show(Lang.PS("请先设置基线文件！", "Please set the baseline file !"));
			}
		}
		else
		{
			currentTcpServerSocket.sglsSampling[currentChannelIdx].baseLinededuct = false;
			currentChannelChartPara.bBaselineDeduction = false;
		}
	}

	private void checkBox2_KeyDown(object sender, KeyEventArgs e)
	{
	}

	private void maskedTextBox11_TextChanged(object sender, EventArgs e)
	{
		if (!m_bLoading)
		{
			double num = Class49.String2Float(maskedTextBox11.Text.Trim(), -10000f);
			if (num == -10000.0)
			{
				maskedTextBox11.Text = "600";
			}
			else
			{
				maskedTextBox11_DoubleClick(null, null);
			}
		}
	}

	private void maskedTextBox11_DoubleClick(object sender, EventArgs e)
	{
		if (m_bAllowUpdateChromGraphic && !(maskedTextBox11.Text.Trim() == ""))
		{
			float num = Class49.String2Float(maskedTextBox11.Text.Trim(), 600f);
			if (num > 9999f)
			{
				num = 9999f;
			}
			if ((double)num < 0.1)
			{
				num = 0.1f;
			}
			TcpServerSocket currentTcpServerSocket = cdlMgr.CurrentTcpServerSocket;
			int currentChannelIdx = cdlMgr.CurrentChannelIdx;
			if (currentTcpServerSocket != null)
			{
				currentTcpServerSocket.dtc_Channels[currentChannelIdx].chromInfoR.AcqRunTime = num;
			}
			ChannelChartPara currentChannelChartPara = cdlMgr.CurrentChannelChartPara;
			if (currentChannelChartPara != null)
			{
				currentChannelChartPara.stopTime = num;
			}
		}
	}

	private void IP4_Enter(object sender, EventArgs e)
	{
		Class49.smethod_40(((Control)sender).Handle);
	}

	private void tbTime_TextChanged(object sender, EventArgs e)
	{
		if (!m_bLoading)
		{
			double num = Class49.String2Float(tbTime.Text.Trim(), -10000f);
			if (num == -10000.0)
			{
				tbTime.Text = "";
			}
			else
			{
				tbTime_DoubleClick(null, null);
			}
		}
	}

	public void tbTime_DoubleClick(object sender, EventArgs e)
	{
		if (sampleDisplay == null || cdlMgr.MainTcpServer == null || !m_bAllowUpdateChromGraphic)
		{
			return;
		}
		TcpServerSocket currentTcpServerSocket = cdlMgr.CurrentTcpServerSocket;
		if (currentTcpServerSocket != null && !(tbTime.Text.Trim() == "") && !(tbSigYBeg.Text.Trim() == "") && !(tbSigYEnd.Text.Trim() == ""))
		{
			disLg = GetDisLg();
			int currentChannelIdx = cdlMgr.CurrentChannelIdx;
			if (cbDisPlayAll.Checked && currentChannelIdx == 0)
			{
				sampleDisplay.stDisChain.AppendFrameLg(disLg);
				DisDpRefresh();
			}
			else if (!cbDisPlayAll.Checked)
			{
				sampleDisplay.stDisChain.AppendFrameLg(disLg);
				DisDpRefresh();
			}
			currentTcpServerSocket.sglsSampling[currentChannelIdx].MaxBlowV = Class49.String2Float(tbSigYBeg.Text, -10f);
			currentTcpServerSocket.sglsSampling[currentChannelIdx].MaxUpV = Class49.String2Float(tbSigYEnd.Text, 500f);
			currentTcpServerSocket.sglsSampling[currentChannelIdx].HoleT = Class49.String2Float(tbTime.Text, 30f);
			ChannelChartPara currentChannelChartPara = cdlMgr.CurrentChannelChartPara;
			currentChannelChartPara.showHighLimit = Class49.String2Float(tbSigYEnd.Text, 500f);
			currentChannelChartPara.showLowLimit = Class49.String2Float(tbSigYBeg.Text, -10f);
			currentChannelChartPara.fullScreenTime = Class49.String2Float(tbTime.Text, 30f);
			currentChannelChartPara.bBaselineDeduction = checkBox8.Checked;
			method_12();
			sampleDisplay.displayPanel.Refresh();
		}
	}

	private void tbSigYBeg_KeyDown(object sender, KeyEventArgs e)
	{
		if (!m_bLoading && e.KeyCode == Keys.Return)
		{
			tbTime_DoubleClick(null, null);
		}
	}

	private void tbSigYEnd_TextChanged(object sender, EventArgs e)
	{
		if (m_bLoading || tbSigYEnd.Text.Trim() == "-")
		{
			return;
		}
		double num = Class49.String2Float(tbSigYEnd.Text.Trim(), -10000f);
		if (!(tbSigYEnd.Text.Trim() == "-"))
		{
			if (num == -10000.0)
			{
				tbSigYEnd.Text = "";
			}
			else
			{
				tbTime_DoubleClick(null, null);
			}
		}
	}

	private void tbSigYBeg_TextChanged(object sender, EventArgs e)
	{
		if (m_bLoading || tbSigYBeg.Text.Trim() == "-")
		{
			return;
		}
		double num = Class49.String2Float(tbSigYBeg.Text.Trim(), -10000f);
		if (!(tbSigYBeg.Text.Trim() == "-"))
		{
			if (num == -10000.0)
			{
				tbSigYBeg.Text = "";
			}
			else
			{
				tbTime_DoubleClick(null, null);
			}
		}
	}

	private void 峰尺寸ToolStripMenuItem_Click(object sender, EventArgs e)
	{
		int currentChannelIdx = cdlMgr.CurrentChannelIdx;
		ChartParaOpera currentChartParaOpera = cdlMgr.CurrentChartParaOpera;
		if (currentChartParaOpera != null)
		{
			MtdSetup mtdMgr = currentChartParaOpera.mtdMgr;
			ChromInfo chromInfo = mtdMgr.chromInfo;
			string cclCalibration = chromInfo.cclCalibration;
			CaliGnl caliGnl = new CaliGnl();
			if (cclCalibration != "" && File.Exists(cclCalibration))
			{
				caliGnl = CaliGnl.LoadFromFile(cclCalibration);
				caliGnl.CalculateFunc(appendLink: false);
			}
			Peak peak = sampleDisplay.getPeak(currentChannelIdx, caliGnl);
			if (peak != null)
			{
				FrmPeakInfo frmPeakInfo = new FrmPeakInfo();
				frmPeakInfo.LabelPeakInfo.Text = "";
				string text = "";
				frmPeakInfo.Text = Lang.PS("峰尺寸", "PeakInfo");
				cdlMgr.formMain.ToolLabelPeakInfo2.Text = Lang.PS("组份名称:", "PeakName:") + peak.name + Lang.PS(" 保留时间:", " PeakRt:") + peak.pkRT.ToString("0.00") + Lang.PS(" 面积:", " PeakArea:") + peak.area.ToString("F" + Class49.int_8) + Lang.PS(" 峰高:", " PeakHeight:") + peak.height.ToString("F" + Class49.int_8);
				text = Lang.PS("组份名称:", "PeakName:") + peak.name + "\r\n\r\n";
				text = text + Lang.PS("保留时间:", "PeakRt:") + peak.pkRT.ToString("0.00") + "\r";
				text = text + Lang.PS("开始时间:", "startT:") + peak.startT.ToString("0.00") + "\r";
				text = text + Lang.PS("结束时间:", "endT:") + peak.endT.ToString("0.00") + "\r\n\r\n";
				text = text + Lang.PS("面积:", "area:") + peak.area.ToString("F" + Class49.int_8) + "\r";
				text = text + Lang.PS("面积百分比:", "areaPer:") + peak.areaPer.ToString("0.000000") + "\r\n\r\n";
				text = text + Lang.PS("峰高:", "height:") + peak.height.ToString("0.00") + "\r";
				text = text + Lang.PS("峰高百分比:", "heightPer:") + peak.heightPer.ToString("0.00") + "\r\n\r\n";
				frmPeakInfo.LabelPeakInfo.Text = text;
				frmPeakInfo.Show();
			}
			else
			{
				cdlMgr.formMain.ToolLabelPeakInfo2.Text = "";
				MessageBox.Show(Lang.PS("此处没有检测到峰", "There were no peak was detected."), Lang.PS("失败", "fail"));
			}
		}
	}

	public void DisDpRefresh()
	{
		sampleDisplay.displayPanel.Refresh();
		if (sampleDisplay.stDisChain.Count == 0)
		{
			return;
		}
		disLg = sampleDisplay.stDisChain.CurDisLg;
		if (cdlMgr.MainTcpServer != null)
		{
			TcpServerSocket currentTcpServerSocket = cdlMgr.CurrentTcpServerSocket;
			int currentChannelIdx = cdlMgr.CurrentChannelIdx;
			if (currentTcpServerSocket != null && currentChannelIdx >= 0 && currentChannelIdx < currentTcpServerSocket.sglsSampling.Length)
			{
				currentTcpServerSocket.sglsSampling[currentChannelIdx].disLg = disLg;
			}
		}
	}

	private void method_12()
	{
		ChannelChartPara currentChannelChartPara = cdlMgr.CurrentChannelChartPara;
		if (currentChannelChartPara != null)
		{
			sampleDisplay.signalfactors[0] = 1f;
			sampleDisplay.signal上限[0] = 500f;
			sampleDisplay.signal下限[0] = 0f;
			for (int i = 0; i < sampleDisplay.signalfactors.Length; i++)
			{
				ChannelChartPara oneEquipPara = cdlMgr.GetOneEquipPara(i);
				sampleDisplay.signalfactors[i] = (currentChannelChartPara.showHighLimit - currentChannelChartPara.showLowLimit) / (oneEquipPara.showHighLimit - oneEquipPara.showLowLimit);
				sampleDisplay.signal上限[i] = oneEquipPara.showHighLimit;
				sampleDisplay.signal下限[i] = oneEquipPara.showLowLimit;
			}
		}
	}

	private void method_9()
	{
		float num = 30f;
		float num2 = -10f;
		float num3 = 500f;
		if (num < 0.1f || (num2 == 0f && num3 == 0f))
		{
			disLg.lgXBeg = 0f;
			num = 0.2f;
			num2 = -1f;
			num3 = 10f;
		}
		method_10(disLg.lgXBeg, num, num2, num3 - num2);
	}

	private void method_11(Signal signal_0)
	{
		signal_0.refresh_TimeValue();
		sampleDisplay.SetFullDisLg(ref disLg, signal_0, second: false);
		method_10(disLg.lgXBeg, disLg.lgX, disLg.lgYBeg, disLg.lgY);
	}

	public void method_10(float float_0, float float_1, float float_2, float float_3)
	{
		float_1 = Math.Max(float_1, 0.01f);
		float_3 = Math.Max(float_3, 0.001f);
		disLg.lgXBeg = float_0;
		disLg.lgX = float_1;
		disLg.lgYBeg = float_2;
		disLg.lgY = float_3;
		sampleDisplay.stDisChain.AppendFrameLg(disLg);
		sampleDisplay.disLg = disLg;
		if (cdlMgr.MainTcpServer != null)
		{
			TcpServerSocket currentTcpServerSocket = cdlMgr.CurrentTcpServerSocket;
			if (currentTcpServerSocket != null)
			{
				int currentChannelIdx = cdlMgr.CurrentChannelIdx;
				currentTcpServerSocket.sglsSampling[currentChannelIdx].disLg = disLg;
			}
		}
	}

	private void ddbXYFull_Click(object sender, EventArgs e)
	{
		TcpServerSocket currentTcpServerSocket = cdlMgr.CurrentTcpServerSocket;
		if (currentTcpServerSocket != null)
		{
			int currentChannelIdx = cdlMgr.CurrentChannelIdx;
			currentTcpServerSocket.sglsSampling[currentChannelIdx].refresh_TimeValue();
			float num = currentTcpServerSocket.sglsSampling[currentChannelIdx].yMaxValue - currentTcpServerSocket.sglsSampling[currentChannelIdx].yMinValue;
			if (num < (float)sysParam.iDispMinValue)
			{
				currentTcpServerSocket.sglsSampling[currentChannelIdx].yMaxValue = currentTcpServerSocket.sglsSampling[currentChannelIdx].yMaxValue + (float)sysParam.iDispMinValue / 2f;
				currentTcpServerSocket.sglsSampling[currentChannelIdx].yMinValue = currentTcpServerSocket.sglsSampling[currentChannelIdx].yMinValue - (float)sysParam.iDispMinValue / 2f;
			}
			num *= 0.02f;
			tbSigYBeg.Text = (currentTcpServerSocket.sglsSampling[currentChannelIdx].yMinValue - num).ToString("0.00");
			tbSigYEnd.Text = (currentTcpServerSocket.sglsSampling[currentChannelIdx].yMaxValue + num).ToString("0.00");
		}
	}

	private void bcYAuto_CheckedChanged(object sender, ItemClickEventArgs e)
	{
		if (m_bLoading)
		{
			return;
		}
		TcpServerSocket currentTcpServerSocket = cdlMgr.CurrentTcpServerSocket;
		if (currentTcpServerSocket != null)
		{
			cdlMgr.CurrentChannelChartPara.bAutoFullY = bcYAuto.Checked;
			if (bcYAuto.Checked)
			{
				ddbXYFull_Click(null, null);
			}
		}
	}

	private void bControlTemp_Click(object sender, EventArgs e)
	{
		cdlMgr.formMain.insDeviceCtrl.method_14();
		TcpServerSocket currentTcpServerSocket = cdlMgr.CurrentTcpServerSocket;
		if (currentTcpServerSocket != null)
		{
			if (bControlTemp.Text == Lang.PS("关闭控温", "Stop Temp"))
			{
				AutoTempCtr = false;
			}
			else
			{
				AutoTempCtr = true;
			}
			if (!currentTcpServerSocket.ControlTemp)
			{
				currentTcpServerSocket.SendCmd(16);
			}
			else
			{
				currentTcpServerSocket.SendCmd(17);
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
		this.components = new System.ComponentModel.Container();
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(IBrainChrom2018.ChromAcqCtrlPortable));
		this.dpDatAcq = new IBrainChrom2018.LclDisplayPanel();
		this.cmPeakInfo = new System.Windows.Forms.ContextMenuStrip(this.components);
		this.峰尺寸ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.tbinsSerial = new System.Windows.Forms.TextBox();
		this.checkBox5 = new System.Windows.Forms.CheckBox();
		this.splitContainer3 = new System.Windows.Forms.SplitContainer();
		this.maskedTextBox12 = new System.Windows.Forms.MaskedTextBox();
		this.maskedTextBox7 = new System.Windows.Forms.MaskedTextBox();
		this.label23 = new System.Windows.Forms.Label();
		this.bControlTemp = new System.Windows.Forms.Button();
		this.labFireState = new System.Windows.Forms.Label();
		this.button37 = new System.Windows.Forms.Button();
		this.button38 = new System.Windows.Forms.Button();
		this.bFunctionSelect = new System.Windows.Forms.Button();
		this.button26 = new System.Windows.Forms.Button();
		this.maskedTextBox6 = new System.Windows.Forms.MaskedTextBox();
		this.button25 = new System.Windows.Forms.Button();
		this.toolStrip1 = new System.Windows.Forms.ToolStrip();
		this.tsStart = new System.Windows.Forms.ToolStripButton();
		this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
		this.tsstop = new System.Windows.Forms.ToolStripButton();
		this.toolStripButton6 = new System.Windows.Forms.ToolStripButton();
		this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
		this.toolStripButton4 = new System.Windows.Forms.ToolStripButton();
		this.toolStripButton5 = new System.Windows.Forms.ToolStripButton();
		this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
		this.toolStripButton7 = new System.Windows.Forms.ToolStripButton();
		this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
		this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
		this.toolStripButton10 = new System.Windows.Forms.ToolStripButton();
		this.label20 = new System.Windows.Forms.Label();
		this.label22 = new System.Windows.Forms.Label();
		this.label21 = new System.Windows.Forms.Label();
		this.checkBox3 = new System.Windows.Forms.CheckBox();
		this.checkBox8 = new System.Windows.Forms.CheckBox();
		this.ddbXYFull = new DevExpress.XtraEditors.DropDownButton();
		this.pomCaptureAndCutting = new DevExpress.XtraBars.PopupMenu(this.components);
		this.bcYAuto = new DevExpress.XtraBars.BarCheckItem();
		this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
		this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
		this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
		this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
		this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
		this.cbFullScreem = new System.Windows.Forms.CheckBox();
		this.btnTimeFull = new System.Windows.Forms.Button();
		this.cbDisPlayAll = new System.Windows.Forms.CheckBox();
		this.lclCPauseRefresh = new IBrainChrom2018.LclCheckBox();
		this.checkBox2 = new System.Windows.Forms.CheckBox();
		this.maskedTextBox11 = new System.Windows.Forms.MaskedTextBox();
		this.label34 = new System.Windows.Forms.Label();
		this.tbTime = new System.Windows.Forms.MaskedTextBox();
		this.label32 = new System.Windows.Forms.Label();
		this.tbSigYEnd = new System.Windows.Forms.MaskedTextBox();
		this.label33 = new System.Windows.Forms.Label();
		this.label116 = new System.Windows.Forms.Label();
		this.maskedTextBox13 = new System.Windows.Forms.MaskedTextBox();
		this.label31 = new System.Windows.Forms.Label();
		this.tbSigYBeg = new System.Windows.Forms.MaskedTextBox();
		this.label115 = new System.Windows.Forms.Label();
		this.label29 = new System.Windows.Forms.Label();
		this.label119 = new System.Windows.Forms.Label();
		this.label27 = new System.Windows.Forms.Label();
		this.timer_0 = new System.Windows.Forms.Timer(this.components);
		this.dpDatAcq.SuspendLayout();
		this.cmPeakInfo.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.splitContainer3).BeginInit();
		this.splitContainer3.Panel1.SuspendLayout();
		this.splitContainer3.Panel2.SuspendLayout();
		this.splitContainer3.SuspendLayout();
		this.toolStrip1.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.pomCaptureAndCutting).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.barManager1).BeginInit();
		base.SuspendLayout();
		this.dpDatAcq.BackColor = System.Drawing.Color.LightSalmon;
		this.dpDatAcq.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.dpDatAcq.ContextMenuStrip = this.cmPeakInfo;
		this.dpDatAcq.Controls.Add(this.tbinsSerial);
		this.dpDatAcq.Dock = System.Windows.Forms.DockStyle.Fill;
		this.dpDatAcq.Location = new System.Drawing.Point(0, 0);
		this.dpDatAcq.Name = "dpDatAcq";
		this.dpDatAcq.Size = new System.Drawing.Size(481, 202);
		this.dpDatAcq.TabIndex = 4;
		this.dpDatAcq.Paint += new System.Windows.Forms.PaintEventHandler(dpDatAcq_Paint);
		this.dpDatAcq.DoubleClick += new System.EventHandler(tbTime_DoubleClick);
		this.dpDatAcq.MouseDown += new System.Windows.Forms.MouseEventHandler(dpDatAcq_MouseDown);
		this.dpDatAcq.MouseMove += new System.Windows.Forms.MouseEventHandler(dpDatAcq_MouseMove);
		this.dpDatAcq.MouseUp += new System.Windows.Forms.MouseEventHandler(dpDatAcq_MouseUp);
		this.cmPeakInfo.ImageScalingSize = new System.Drawing.Size(20, 20);
		this.cmPeakInfo.Items.AddRange(new System.Windows.Forms.ToolStripItem[1] { this.峰尺寸ToolStripMenuItem });
		this.cmPeakInfo.Name = "cmPeakInfo";
		this.cmPeakInfo.Size = new System.Drawing.Size(122, 26);
		this.峰尺寸ToolStripMenuItem.Name = "峰尺寸ToolStripMenuItem";
		this.峰尺寸ToolStripMenuItem.Size = new System.Drawing.Size(121, 22);
		this.峰尺寸ToolStripMenuItem.Text = "峰尺寸...";
		this.峰尺寸ToolStripMenuItem.Click += new System.EventHandler(峰尺寸ToolStripMenuItem_Click);
		this.tbinsSerial.Location = new System.Drawing.Point(411, 229);
		this.tbinsSerial.Name = "tbinsSerial";
		this.tbinsSerial.Size = new System.Drawing.Size(230, 21);
		this.tbinsSerial.TabIndex = 4;
		this.tbinsSerial.Text = "设备序列号：60789EC04B484858";
		this.tbinsSerial.Visible = false;
		this.checkBox5.AutoSize = true;
		this.checkBox5.Font = new System.Drawing.Font("宋体", 9f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 134);
		this.checkBox5.Location = new System.Drawing.Point(860, 7);
		this.checkBox5.Name = "checkBox5";
		this.checkBox5.Size = new System.Drawing.Size(89, 16);
		this.checkBox5.TabIndex = 5;
		this.checkBox5.Text = "结束后打印";
		this.checkBox5.UseVisualStyleBackColor = true;
		this.checkBox5.CheckedChanged += new System.EventHandler(checkBox5_CheckedChanged);
		this.splitContainer3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.splitContainer3.Location = new System.Drawing.Point(501, 182);
		this.splitContainer3.Name = "splitContainer3";
		this.splitContainer3.Orientation = System.Windows.Forms.Orientation.Horizontal;
		this.splitContainer3.Panel1.Controls.Add(this.maskedTextBox12);
		this.splitContainer3.Panel1.Controls.Add(this.maskedTextBox7);
		this.splitContainer3.Panel1.Controls.Add(this.label23);
		this.splitContainer3.Panel1.Controls.Add(this.bControlTemp);
		this.splitContainer3.Panel1.Controls.Add(this.labFireState);
		this.splitContainer3.Panel1.Controls.Add(this.button37);
		this.splitContainer3.Panel1.Controls.Add(this.button38);
		this.splitContainer3.Panel1.Controls.Add(this.bFunctionSelect);
		this.splitContainer3.Panel1.Controls.Add(this.button26);
		this.splitContainer3.Panel1.Controls.Add(this.maskedTextBox6);
		this.splitContainer3.Panel1.Controls.Add(this.button25);
		this.splitContainer3.Panel1.Controls.Add(this.toolStrip1);
		this.splitContainer3.Panel1.Controls.Add(this.label20);
		this.splitContainer3.Panel1.Controls.Add(this.label22);
		this.splitContainer3.Panel1.Controls.Add(this.label21);
		this.splitContainer3.Panel2.Controls.Add(this.checkBox5);
		this.splitContainer3.Panel2.Controls.Add(this.checkBox3);
		this.splitContainer3.Panel2.Controls.Add(this.checkBox8);
		this.splitContainer3.Panel2.Controls.Add(this.ddbXYFull);
		this.splitContainer3.Panel2.Controls.Add(this.cbFullScreem);
		this.splitContainer3.Panel2.Controls.Add(this.btnTimeFull);
		this.splitContainer3.Panel2.Controls.Add(this.cbDisPlayAll);
		this.splitContainer3.Panel2.Controls.Add(this.lclCPauseRefresh);
		this.splitContainer3.Panel2.Controls.Add(this.checkBox2);
		this.splitContainer3.Panel2.Controls.Add(this.maskedTextBox11);
		this.splitContainer3.Panel2.Controls.Add(this.label34);
		this.splitContainer3.Panel2.Controls.Add(this.tbTime);
		this.splitContainer3.Panel2.Controls.Add(this.label32);
		this.splitContainer3.Panel2.Controls.Add(this.tbSigYEnd);
		this.splitContainer3.Panel2.Controls.Add(this.label33);
		this.splitContainer3.Panel2.Controls.Add(this.label116);
		this.splitContainer3.Panel2.Controls.Add(this.maskedTextBox13);
		this.splitContainer3.Panel2.Controls.Add(this.label31);
		this.splitContainer3.Panel2.Controls.Add(this.tbSigYBeg);
		this.splitContainer3.Panel2.Controls.Add(this.label115);
		this.splitContainer3.Panel2.Controls.Add(this.label29);
		this.splitContainer3.Panel2.Controls.Add(this.label119);
		this.splitContainer3.Panel2.Controls.Add(this.label27);
		this.splitContainer3.Size = new System.Drawing.Size(149, 76);
		this.splitContainer3.SplitterDistance = 40;
		this.splitContainer3.TabIndex = 1;
		this.maskedTextBox12.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.maskedTextBox12.Font = new System.Drawing.Font("宋体", 9f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 134);
		this.maskedTextBox12.ForeColor = System.Drawing.Color.Blue;
		this.maskedTextBox12.Location = new System.Drawing.Point(425, 9);
		this.maskedTextBox12.Name = "maskedTextBox12";
		this.maskedTextBox12.ReadOnly = true;
		this.maskedTextBox12.Size = new System.Drawing.Size(52, 21);
		this.maskedTextBox12.TabIndex = 15;
		this.maskedTextBox12.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
		this.maskedTextBox12.Visible = false;
		this.maskedTextBox7.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.maskedTextBox7.Font = new System.Drawing.Font("宋体", 9f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 134);
		this.maskedTextBox7.ForeColor = System.Drawing.Color.Blue;
		this.maskedTextBox7.Location = new System.Drawing.Point(471, 9);
		this.maskedTextBox7.Name = "maskedTextBox7";
		this.maskedTextBox7.ReadOnly = true;
		this.maskedTextBox7.Size = new System.Drawing.Size(52, 21);
		this.maskedTextBox7.TabIndex = 2;
		this.maskedTextBox7.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
		this.label23.AutoSize = true;
		this.label23.Font = new System.Drawing.Font("宋体", 9f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 134);
		this.label23.ForeColor = System.Drawing.Color.Black;
		this.label23.Location = new System.Drawing.Point(436, 13);
		this.label23.Name = "label23";
		this.label23.Size = new System.Drawing.Size(38, 12);
		this.label23.TabIndex = 0;
		this.label23.Text = "时间:";
		this.bControlTemp.BackColor = System.Drawing.SystemColors.Control;
		this.bControlTemp.Font = new System.Drawing.Font("宋体", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		this.bControlTemp.Location = new System.Drawing.Point(729, -1);
		this.bControlTemp.Name = "bControlTemp";
		this.bControlTemp.Size = new System.Drawing.Size(66, 38);
		this.bControlTemp.TabIndex = 13;
		this.bControlTemp.Text = "开始控温";
		this.bControlTemp.UseVisualStyleBackColor = false;
		this.bControlTemp.Visible = false;
		this.bControlTemp.Click += new System.EventHandler(bControlTemp_Click);
		this.labFireState.AutoSize = true;
		this.labFireState.Location = new System.Drawing.Point(734, 23);
		this.labFireState.Name = "labFireState";
		this.labFireState.Size = new System.Drawing.Size(53, 12);
		this.labFireState.TabIndex = 14;
		this.labFireState.Text = "开机运行";
		this.labFireState.Visible = false;
		this.button37.Image = (System.Drawing.Image)resources.GetObject("button37.Image");
		this.button37.Location = new System.Drawing.Point(638, -3);
		this.button37.Name = "button37";
		this.button37.Size = new System.Drawing.Size(45, 44);
		this.button37.TabIndex = 12;
		this.button37.UseVisualStyleBackColor = true;
		this.button37.Visible = false;
		this.button38.Image = (System.Drawing.Image)resources.GetObject("button38.Image");
		this.button38.Location = new System.Drawing.Point(681, -2);
		this.button38.Name = "button38";
		this.button38.Size = new System.Drawing.Size(45, 44);
		this.button38.TabIndex = 11;
		this.button38.UseVisualStyleBackColor = true;
		this.button38.Visible = false;
		this.bFunctionSelect.Location = new System.Drawing.Point(548, 0);
		this.bFunctionSelect.Name = "bFunctionSelect";
		this.bFunctionSelect.Size = new System.Drawing.Size(93, 39);
		this.bFunctionSelect.TabIndex = 6;
		this.bFunctionSelect.Text = "仪器设置";
		this.bFunctionSelect.UseVisualStyleBackColor = true;
		this.bFunctionSelect.Click += new System.EventHandler(bFunctionSelect_Click);
		this.button26.Enabled = false;
		this.button26.Image = (System.Drawing.Image)resources.GetObject("button26.Image");
		this.button26.Location = new System.Drawing.Point(593, -3);
		this.button26.Name = "button26";
		this.button26.Size = new System.Drawing.Size(45, 44);
		this.button26.TabIndex = 4;
		this.button26.UseVisualStyleBackColor = true;
		this.button26.KeyDown += new System.Windows.Forms.KeyEventHandler(button26_KeyDown);
		this.maskedTextBox6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.maskedTextBox6.Font = new System.Drawing.Font("宋体", 9f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 134);
		this.maskedTextBox6.ForeColor = System.Drawing.Color.Blue;
		this.maskedTextBox6.Location = new System.Drawing.Point(359, 9);
		this.maskedTextBox6.Name = "maskedTextBox6";
		this.maskedTextBox6.ReadOnly = true;
		this.maskedTextBox6.Size = new System.Drawing.Size(63, 21);
		this.maskedTextBox6.TabIndex = 2;
		this.maskedTextBox6.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
		this.button25.Enabled = false;
		this.button25.Image = (System.Drawing.Image)resources.GetObject("button25.Image");
		this.button25.Location = new System.Drawing.Point(548, -3);
		this.button25.Name = "button25";
		this.button25.Size = new System.Drawing.Size(45, 44);
		this.button25.TabIndex = 4;
		this.button25.UseVisualStyleBackColor = true;
		this.button25.KeyDown += new System.Windows.Forms.KeyEventHandler(button25_KeyDown);
		this.toolStrip1.Dock = System.Windows.Forms.DockStyle.None;
		this.toolStrip1.ImageScalingSize = new System.Drawing.Size(32, 32);
		this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[12]
		{
			this.tsStart, this.toolStripSeparator1, this.tsstop, this.toolStripButton6, this.toolStripSeparator2, this.toolStripButton4, this.toolStripButton5, this.toolStripSeparator3, this.toolStripButton7, this.toolStripButton1,
			this.toolStripSeparator4, this.toolStripButton10
		});
		this.toolStrip1.Location = new System.Drawing.Point(3, 0);
		this.toolStrip1.Name = "toolStrip1";
		this.toolStrip1.Size = new System.Drawing.Size(324, 39);
		this.toolStrip1.TabIndex = 0;
		this.toolStrip1.Text = "toolStrip1";
		this.tsStart.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
		this.tsStart.Image = (System.Drawing.Image)resources.GetObject("tsStart.Image");
		this.tsStart.ImageTransparentColor = System.Drawing.Color.Magenta;
		this.tsStart.Name = "tsStart";
		this.tsStart.Size = new System.Drawing.Size(36, 36);
		this.tsStart.Text = "开始采集";
		this.tsStart.ToolTipText = "开始采集";
		this.tsStart.Click += new System.EventHandler(toolStripButton1_Click);
		this.toolStripSeparator1.Name = "toolStripSeparator1";
		this.toolStripSeparator1.Size = new System.Drawing.Size(6, 39);
		this.tsstop.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
		this.tsstop.Image = (System.Drawing.Image)resources.GetObject("tsstop.Image");
		this.tsstop.ImageTransparentColor = System.Drawing.Color.Magenta;
		this.tsstop.Name = "tsstop";
		this.tsstop.Size = new System.Drawing.Size(36, 36);
		this.tsstop.Text = "toolStripButton3";
		this.tsstop.ToolTipText = "停止采集";
		this.tsstop.Click += new System.EventHandler(toolStripButton3_Click);
		this.toolStripButton6.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
		this.toolStripButton6.Image = (System.Drawing.Image)resources.GetObject("toolStripButton6.Image");
		this.toolStripButton6.ImageTransparentColor = System.Drawing.Color.Magenta;
		this.toolStripButton6.Name = "toolStripButton6";
		this.toolStripButton6.Size = new System.Drawing.Size(36, 36);
		this.toolStripButton6.Text = "toolStripButton6";
		this.toolStripButton6.ToolTipText = "放弃采集";
		this.toolStripButton6.Click += new System.EventHandler(toolStripButton6_Click);
		this.toolStripSeparator2.Name = "toolStripSeparator2";
		this.toolStripSeparator2.Size = new System.Drawing.Size(6, 39);
		this.toolStripButton4.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
		this.toolStripButton4.Image = (System.Drawing.Image)resources.GetObject("toolStripButton4.Image");
		this.toolStripButton4.ImageTransparentColor = System.Drawing.Color.Magenta;
		this.toolStripButton4.Name = "toolStripButton4";
		this.toolStripButton4.Size = new System.Drawing.Size(36, 36);
		this.toolStripButton4.Text = "toolStripButton4";
		this.toolStripButton4.ToolTipText = "上一视图";
		this.toolStripButton4.Click += new System.EventHandler(toolStripButton4_Click);
		this.toolStripButton5.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
		this.toolStripButton5.Image = (System.Drawing.Image)resources.GetObject("toolStripButton5.Image");
		this.toolStripButton5.ImageTransparentColor = System.Drawing.Color.Magenta;
		this.toolStripButton5.Name = "toolStripButton5";
		this.toolStripButton5.Size = new System.Drawing.Size(36, 36);
		this.toolStripButton5.Text = "toolStripButton5";
		this.toolStripButton5.ToolTipText = "下一视图";
		this.toolStripButton5.Click += new System.EventHandler(toolStripButton5_Click);
		this.toolStripSeparator3.Name = "toolStripSeparator3";
		this.toolStripSeparator3.Size = new System.Drawing.Size(6, 39);
		this.toolStripButton7.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
		this.toolStripButton7.Image = (System.Drawing.Image)resources.GetObject("toolStripButton7.Image");
		this.toolStripButton7.ImageTransparentColor = System.Drawing.Color.Magenta;
		this.toolStripButton7.Name = "toolStripButton7";
		this.toolStripButton7.Size = new System.Drawing.Size(36, 36);
		this.toolStripButton7.ToolTipText = "文件命名";
		this.toolStripButton7.Click += new System.EventHandler(toolStripButton7_Click);
		this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
		this.toolStripButton1.Image = (System.Drawing.Image)resources.GetObject("toolStripButton1.Image");
		this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
		this.toolStripButton1.Name = "toolStripButton1";
		this.toolStripButton1.Size = new System.Drawing.Size(36, 36);
		this.toolStripButton1.Text = "toolStripButton1";
		this.toolStripButton1.ToolTipText = "检测器设置";
		this.toolStripButton1.Click += new System.EventHandler(toolStripButton1_Click_1);
		this.toolStripSeparator4.Name = "toolStripSeparator4";
		this.toolStripSeparator4.Size = new System.Drawing.Size(6, 39);
		this.toolStripButton10.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
		this.toolStripButton10.Image = (System.Drawing.Image)resources.GetObject("toolStripButton10.Image");
		this.toolStripButton10.ImageTransparentColor = System.Drawing.Color.Magenta;
		this.toolStripButton10.Name = "toolStripButton10";
		this.toolStripButton10.Size = new System.Drawing.Size(36, 36);
		this.toolStripButton10.Text = "toolStripButton10";
		this.toolStripButton10.ToolTipText = "基线扣除";
		this.toolStripButton10.Click += new System.EventHandler(toolStripButton10_Click);
		this.label20.AutoSize = true;
		this.label20.Font = new System.Drawing.Font("宋体", 9f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 134);
		this.label20.ForeColor = System.Drawing.Color.Black;
		this.label20.Location = new System.Drawing.Point(325, 13);
		this.label20.Name = "label20";
		this.label20.Size = new System.Drawing.Size(38, 12);
		this.label20.TabIndex = 0;
		this.label20.Text = "信号:";
		this.label22.AutoSize = true;
		this.label22.Location = new System.Drawing.Point(524, 13);
		this.label22.Name = "label22";
		this.label22.Size = new System.Drawing.Size(23, 12);
		this.label22.TabIndex = 3;
		this.label22.Text = "min";
		this.label21.AutoSize = true;
		this.label21.Location = new System.Drawing.Point(423, 13);
		this.label21.Name = "label21";
		this.label21.Size = new System.Drawing.Size(17, 12);
		this.label21.TabIndex = 3;
		this.label21.Text = "mV";
		this.checkBox3.AutoSize = true;
		this.checkBox3.Font = new System.Drawing.Font("宋体", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		this.checkBox3.ForeColor = System.Drawing.Color.Green;
		this.checkBox3.Location = new System.Drawing.Point(71, 9);
		this.checkBox3.Name = "checkBox3";
		this.checkBox3.Size = new System.Drawing.Size(84, 16);
		this.checkBox3.TabIndex = 5;
		this.checkBox3.Text = "结束后显示";
		this.checkBox3.UseVisualStyleBackColor = true;
		this.checkBox3.CheckedChanged += new System.EventHandler(checkBox3_CheckedChanged);
		this.checkBox8.AutoSize = true;
		this.checkBox8.Font = new System.Drawing.Font("宋体", 9f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 134);
		this.checkBox8.Location = new System.Drawing.Point(913, 8);
		this.checkBox8.Name = "checkBox8";
		this.checkBox8.Size = new System.Drawing.Size(76, 16);
		this.checkBox8.TabIndex = 198;
		this.checkBox8.Text = "基线扣除";
		this.checkBox8.UseVisualStyleBackColor = true;
		this.checkBox8.Visible = false;
		this.checkBox8.CheckedChanged += new System.EventHandler(checkBox8_CheckedChanged);
		this.ddbXYFull.DropDownControl = this.pomCaptureAndCutting;
		this.ddbXYFull.Location = new System.Drawing.Point(397, 5);
		this.ddbXYFull.Name = "ddbXYFull";
		this.ddbXYFull.Size = new System.Drawing.Size(58, 23);
		this.ddbXYFull.TabIndex = 197;
		this.ddbXYFull.Text = "Y满屏";
		this.ddbXYFull.Click += new System.EventHandler(ddbXYFull_Click);
		this.pomCaptureAndCutting.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[1]
		{
			new DevExpress.XtraBars.LinkPersistInfo(this.bcYAuto)
		});
		this.pomCaptureAndCutting.Manager = this.barManager1;
		this.pomCaptureAndCutting.Name = "pomCaptureAndCutting";
		this.bcYAuto.Caption = "自适应";
		this.bcYAuto.Id = 4;
		this.bcYAuto.Name = "bcYAuto";
		this.bcYAuto.CheckedChanged += new DevExpress.XtraBars.ItemClickEventHandler(bcYAuto_CheckedChanged);
		this.barManager1.DockControls.Add(this.barDockControlTop);
		this.barManager1.DockControls.Add(this.barDockControlBottom);
		this.barManager1.DockControls.Add(this.barDockControlLeft);
		this.barManager1.DockControls.Add(this.barDockControlRight);
		this.barManager1.Form = this;
		this.barManager1.Items.AddRange(new DevExpress.XtraBars.BarItem[1] { this.bcYAuto });
		this.barManager1.MaxItemId = 5;
		this.barDockControlTop.CausesValidation = false;
		this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
		this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
		this.barDockControlTop.Size = new System.Drawing.Size(481, 0);
		this.barDockControlBottom.CausesValidation = false;
		this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.barDockControlBottom.Location = new System.Drawing.Point(0, 202);
		this.barDockControlBottom.Size = new System.Drawing.Size(481, 0);
		this.barDockControlLeft.CausesValidation = false;
		this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
		this.barDockControlLeft.Location = new System.Drawing.Point(0, 0);
		this.barDockControlLeft.Size = new System.Drawing.Size(0, 202);
		this.barDockControlRight.CausesValidation = false;
		this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
		this.barDockControlRight.Location = new System.Drawing.Point(481, 0);
		this.barDockControlRight.Size = new System.Drawing.Size(0, 202);
		this.cbFullScreem.AutoSize = true;
		this.cbFullScreem.Location = new System.Drawing.Point(837, 8);
		this.cbFullScreem.Name = "cbFullScreem";
		this.cbFullScreem.Size = new System.Drawing.Size(48, 16);
		this.cbFullScreem.TabIndex = 7;
		this.cbFullScreem.Text = "全屏";
		this.cbFullScreem.UseVisualStyleBackColor = true;
		this.cbFullScreem.CheckedChanged += new System.EventHandler(cbFullScreem_CheckedChanged);
		this.btnTimeFull.Location = new System.Drawing.Point(577, 3);
		this.btnTimeFull.Name = "btnTimeFull";
		this.btnTimeFull.Size = new System.Drawing.Size(47, 26);
		this.btnTimeFull.TabIndex = 8;
		this.btnTimeFull.Text = "X满屏";
		this.btnTimeFull.UseVisualStyleBackColor = true;
		this.btnTimeFull.Click += new System.EventHandler(btnTimeFull_Click);
		this.cbDisPlayAll.AutoSize = true;
		this.cbDisPlayAll.Location = new System.Drawing.Point(793, 8);
		this.cbDisPlayAll.Name = "cbDisPlayAll";
		this.cbDisPlayAll.Size = new System.Drawing.Size(48, 16);
		this.cbDisPlayAll.TabIndex = 7;
		this.cbDisPlayAll.Text = "全显";
		this.cbDisPlayAll.UseVisualStyleBackColor = true;
		this.cbDisPlayAll.CheckedChanged += new System.EventHandler(cbDisPlayAll_CheckedChanged);
		this.lclCPauseRefresh.AutoSize = true;
		this.lclCPauseRefresh.Location = new System.Drawing.Point(2, 8);
		this.lclCPauseRefresh.Name = "lclCPauseRefresh";
		this.lclCPauseRefresh.Size = new System.Drawing.Size(72, 16);
		this.lclCPauseRefresh.TabIndex = 6;
		this.lclCPauseRefresh.Text = "暂停刷新";
		this.lclCPauseRefresh.UseVisualStyleBackColor = true;
		this.checkBox2.Font = new System.Drawing.Font("宋体", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		this.checkBox2.ForeColor = System.Drawing.Color.Green;
		this.checkBox2.Location = new System.Drawing.Point(749, 7);
		this.checkBox2.Name = "checkBox2";
		this.checkBox2.Size = new System.Drawing.Size(48, 20);
		this.checkBox2.TabIndex = 4;
		this.checkBox2.Text = "调零";
		this.checkBox2.UseVisualStyleBackColor = true;
		this.checkBox2.CheckedChanged += new System.EventHandler(checkBox2_CheckedChanged);
		this.checkBox2.KeyDown += new System.Windows.Forms.KeyEventHandler(checkBox2_KeyDown);
		this.maskedTextBox11.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.maskedTextBox11.Font = new System.Drawing.Font("宋体", 9f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 134);
		this.maskedTextBox11.ForeColor = System.Drawing.Color.Red;
		this.maskedTextBox11.Location = new System.Drawing.Point(683, 8);
		this.maskedTextBox11.Name = "maskedTextBox11";
		this.maskedTextBox11.PromptChar = ' ';
		this.maskedTextBox11.Size = new System.Drawing.Size(39, 21);
		this.maskedTextBox11.TabIndex = 2;
		this.maskedTextBox11.Text = "45";
		this.maskedTextBox11.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
		this.maskedTextBox11.TextChanged += new System.EventHandler(maskedTextBox11_TextChanged);
		this.maskedTextBox11.DoubleClick += new System.EventHandler(maskedTextBox11_DoubleClick);
		this.maskedTextBox11.Enter += new System.EventHandler(IP4_Enter);
		this.label34.AutoSize = true;
		this.label34.Font = new System.Drawing.Font("宋体", 9f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 134);
		this.label34.ForeColor = System.Drawing.Color.Black;
		this.label34.Location = new System.Drawing.Point(623, 11);
		this.label34.Name = "label34";
		this.label34.Size = new System.Drawing.Size(64, 12);
		this.label34.TabIndex = 0;
		this.label34.Text = "停止时间:";
		this.tbTime.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.tbTime.Font = new System.Drawing.Font("宋体", 9f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 134);
		this.tbTime.ForeColor = System.Drawing.Color.Lime;
		this.tbTime.Location = new System.Drawing.Point(515, 8);
		this.tbTime.Name = "tbTime";
		this.tbTime.PromptChar = ' ';
		this.tbTime.Size = new System.Drawing.Size(40, 21);
		this.tbTime.TabIndex = 2;
		this.tbTime.Text = "30";
		this.tbTime.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
		this.tbTime.TextChanged += new System.EventHandler(tbTime_TextChanged);
		this.tbTime.DoubleClick += new System.EventHandler(tbTime_DoubleClick);
		this.tbTime.Enter += new System.EventHandler(IP4_Enter);
		this.tbTime.KeyDown += new System.Windows.Forms.KeyEventHandler(tbSigYBeg_KeyDown);
		this.label32.AutoSize = true;
		this.label32.Font = new System.Drawing.Font("宋体", 9f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 134);
		this.label32.ForeColor = System.Drawing.Color.Black;
		this.label32.Location = new System.Drawing.Point(455, 11);
		this.label32.Name = "label32";
		this.label32.Size = new System.Drawing.Size(64, 12);
		this.label32.TabIndex = 0;
		this.label32.Text = "满屏时间:";
		this.tbSigYEnd.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.tbSigYEnd.Font = new System.Drawing.Font("宋体", 9f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 134);
		this.tbSigYEnd.ForeColor = System.Drawing.Color.Lime;
		this.tbSigYEnd.Location = new System.Drawing.Point(328, 8);
		this.tbSigYEnd.Name = "tbSigYEnd";
		this.tbSigYEnd.PromptChar = ' ';
		this.tbSigYEnd.Size = new System.Drawing.Size(52, 21);
		this.tbSigYEnd.TabIndex = 2;
		this.tbSigYEnd.Text = "500";
		this.tbSigYEnd.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
		this.tbSigYEnd.TextChanged += new System.EventHandler(tbSigYEnd_TextChanged);
		this.tbSigYEnd.DoubleClick += new System.EventHandler(tbTime_DoubleClick);
		this.tbSigYEnd.Enter += new System.EventHandler(IP4_Enter);
		this.tbSigYEnd.KeyDown += new System.Windows.Forms.KeyEventHandler(tbSigYBeg_KeyDown);
		this.label33.AutoSize = true;
		this.label33.Location = new System.Drawing.Point(724, 11);
		this.label33.Name = "label33";
		this.label33.Size = new System.Drawing.Size(23, 12);
		this.label33.TabIndex = 3;
		this.label33.Text = "min";
		this.label116.AutoSize = true;
		this.label116.Font = new System.Drawing.Font("宋体", 9f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 134);
		this.label116.ForeColor = System.Drawing.Color.Black;
		this.label116.Location = new System.Drawing.Point(168, -27);
		this.label116.Name = "label116";
		this.label116.Size = new System.Drawing.Size(38, 12);
		this.label116.TabIndex = 0;
		this.label116.Text = "信号:";
		this.maskedTextBox13.Font = new System.Drawing.Font("宋体", 9f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 134);
		this.maskedTextBox13.ForeColor = System.Drawing.Color.Lime;
		this.maskedTextBox13.Location = new System.Drawing.Point(233, -30);
		this.maskedTextBox13.Mask = "000.00";
		this.maskedTextBox13.Name = "maskedTextBox13";
		this.maskedTextBox13.Size = new System.Drawing.Size(52, 21);
		this.maskedTextBox13.TabIndex = 2;
		this.maskedTextBox13.Text = "00000";
		this.label31.AutoSize = true;
		this.label31.Location = new System.Drawing.Point(556, 11);
		this.label31.Name = "label31";
		this.label31.Size = new System.Drawing.Size(23, 12);
		this.label31.TabIndex = 3;
		this.label31.Text = "min";
		this.tbSigYBeg.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.tbSigYBeg.Font = new System.Drawing.Font("宋体", 9f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 134);
		this.tbSigYBeg.ForeColor = System.Drawing.Color.Lime;
		this.tbSigYBeg.Location = new System.Drawing.Point(244, 8);
		this.tbSigYBeg.Name = "tbSigYBeg";
		this.tbSigYBeg.PromptChar = ' ';
		this.tbSigYBeg.Size = new System.Drawing.Size(52, 21);
		this.tbSigYBeg.TabIndex = 2;
		this.tbSigYBeg.Text = "-10";
		this.tbSigYBeg.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
		this.tbSigYBeg.TextChanged += new System.EventHandler(tbSigYBeg_TextChanged);
		this.tbSigYBeg.DoubleClick += new System.EventHandler(tbTime_DoubleClick);
		this.tbSigYBeg.Enter += new System.EventHandler(IP4_Enter);
		this.tbSigYBeg.KeyDown += new System.Windows.Forms.KeyEventHandler(tbSigYBeg_KeyDown);
		this.label115.AutoSize = true;
		this.label115.Location = new System.Drawing.Point(283, -27);
		this.label115.Name = "label115";
		this.label115.Size = new System.Drawing.Size(17, 12);
		this.label115.TabIndex = 3;
		this.label115.Text = "mv";
		this.label29.AutoSize = true;
		this.label29.Location = new System.Drawing.Point(379, 11);
		this.label29.Name = "label29";
		this.label29.Size = new System.Drawing.Size(17, 12);
		this.label29.TabIndex = 3;
		this.label29.Text = "mV";
		this.label119.AutoSize = true;
		this.label119.Font = new System.Drawing.Font("宋体", 9f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 134);
		this.label119.ForeColor = System.Drawing.Color.Black;
		this.label119.Location = new System.Drawing.Point(209, 10);
		this.label119.Name = "label119";
		this.label119.Size = new System.Drawing.Size(38, 12);
		this.label119.TabIndex = 0;
		this.label119.Text = "下限:";
		this.label27.AutoSize = true;
		this.label27.Font = new System.Drawing.Font("宋体", 9f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 134);
		this.label27.ForeColor = System.Drawing.Color.Black;
		this.label27.Location = new System.Drawing.Point(294, 11);
		this.label27.Name = "label27";
		this.label27.Size = new System.Drawing.Size(38, 12);
		this.label27.TabIndex = 0;
		this.label27.Text = "上限:";
		this.timer_0.Enabled = true;
		this.timer_0.Interval = 500;
		this.timer_0.Tick += new System.EventHandler(timer_0_Tick);
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.Controls.Add(this.dpDatAcq);
		base.Controls.Add(this.splitContainer3);
		base.Controls.Add(this.barDockControlLeft);
		base.Controls.Add(this.barDockControlRight);
		base.Controls.Add(this.barDockControlBottom);
		base.Controls.Add(this.barDockControlTop);
		base.Name = "ChromAcqCtrlPortable";
		base.Size = new System.Drawing.Size(481, 202);
		this.dpDatAcq.ResumeLayout(false);
		this.dpDatAcq.PerformLayout();
		this.cmPeakInfo.ResumeLayout(false);
		this.splitContainer3.Panel1.ResumeLayout(false);
		this.splitContainer3.Panel1.PerformLayout();
		this.splitContainer3.Panel2.ResumeLayout(false);
		this.splitContainer3.Panel2.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.splitContainer3).EndInit();
		this.splitContainer3.ResumeLayout(false);
		this.toolStrip1.ResumeLayout(false);
		this.toolStrip1.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.pomCaptureAndCutting).EndInit();
		((System.ComponentModel.ISupportInitialize)this.barManager1).EndInit();
		base.ResumeLayout(false);
	}
}
