using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;
using IBrainChrom2018.Properties;
using IBrainChrom2018.Unit;

namespace IBrainChrom2018;

public class VocCtrl : UserControl
{
	private Process softKey;

	public static VocCtrl vocCtrl;

	public SerialPortBase serialPoartBase = new SerialPortBase();

	public string[] arrayData = new string[15];

	public float[] fArrayData = new float[15];

	public string strFileName = "";

	public string strFileName2 = "";

	private SystemParam sysParam = SystemParam.Create();

	public bool flagChannelOver1 = false;

	public bool flagChannelOver2 = false;

	private ChromDeviceListMgr cdlMgr = ChromDeviceListMgr.Create();

	private FormMainParam frmParam = FormMainParam.Create();

	private bool m_bLoading = true;

	public bool AutoTempCtr = true;

	public int CountTempCtr = 0;

	public int CountState = 0;

	public ushort StateInstrument = 0;

	public int CountAnalyse = 0;

	public int StateYiqi = 0;

	public string[] strCompName;

	public string UnitName1;

	public string UnitName2;

	public string UnitName3;

	public string UnitName11;

	public string peakName1;

	public string peakName2;

	public FormVOC formVoc = new FormVOC();

	public bool bCalibration = false;

	public bool bCalibration2 = false;

	public bool bAutoCycle1 = true;

	public bool bAutoCycle2 = true;

	private IContainer components = null;

	private Button btShowDesktop;

	private Button button36;

	private Label lbNMHC;

	private Label lbNMHCT;

	private Label lbCH4;

	private Label label79;

	private Label lbTHC;

	private Label label77;

	public TextBox tbFireOn2;

	private Button btnFireOnSet;

	private Button btnFireOnCheck;

	private Label label80;

	public TextBox tbFireOn;

	private GroupBox gbBenXW;

	private Panel panel1;

	private Button button1;

	private Button button2;

	private ComboBox cbKindMachine;

	private GroupBox gbNHMC1;

	private Button btnNetConfig;

	public Button btnCali;

	private Button btnDevice;

	public Label lbBTEX9;

	public Label lbBTEX9T;

	public Label lbBTEX8;

	public Label lbBTEX8T;

	public Label lbBTEX;

	public Label lbBTEXt;

	public Label lbBTEX7;

	public Label lbBTEX7T;

	public Label lbBTEX5;

	public Label lbBTEX6;

	public Label lbBTEX4;

	public Label lbBTEX5T;

	public Label lbBTEX6T;

	public Label lbBTEX4T;

	public Label lbBTEX2;

	public Label lbBTEX3;

	public Label lbBTEX1;

	public Label lbBTEX2T;

	public Label lbBTEX3T;

	public Label lbBTEX1T;

	private Button btnSoftKey;

	private ComboBox cbCOM2;

	private Label label1;

	private System.Windows.Forms.Timer timer1;

	private Button btnHistory;

	private Button btnSetPassword;

	private TabControl tabControl1;

	private TabPage tabPage1;

	private TabPage tabPage2;

	private Label label2;

	private CheckBox chbO2;

	private TextBox tbSdaFileName;

	private Label labO;

	private Label label3;

	private Button sdaOpen;

	private TabPage tabPage3;

	private Label label11;

	public TextBox tbTimeCycle1;

	private Label label14;

	private Label label10;

	private Label label15;

	public TextBox tbTimeCycle2;

	public TextBox tbPowerOnDelay;

	private Label label13;

	private Label label12;

	private CheckBox chbCycle2;

	private CheckBox chbCycle;

	private Label label4;

	public TextBox tbTimesCycle1;

	private Label label5;

	public TextBox tbTimesCycle2;

	private Label label6;

	private Label label7;

	private Label labTimes2;

	private Label labTimes1;

	public Panel panel2;

	[DllImport("User32.dll")]
	public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

	[DllImport("user32.dll")]
	public static extern bool MoveWindow(IntPtr hWnd, int X, int Y, int nWidth, int nHeight, bool bRepaint);

	[DllImport("user32.dll")]
	private static extern bool SetForegroundWindow(IntPtr hWnd);

	public static bool IsDesignMode()
	{
		return false;
	}

	public VocCtrl()
	{
		vocCtrl = this;
		InitializeComponent();
		LoadLanguage();
		if (!IsDesignMode())
		{
			AreaPlotParamMgr areaPlotParamMgr = AreaPlotParamMgr.Create();
			AreaPlotParam areaPlotParam = null;
			strCompName = new string[15];
			for (int i = 0; i < 15; i++)
			{
				arrayData[i] = "";
			}
			tbFireOn.Text = frmParam.fFireOn.ToString();
			tbFireOn2.Text = frmParam.fFireOn2.ToString();
			cbKindMachine.DropDownStyle = ComboBoxStyle.DropDownList;
			cbKindMachine.SelectedIndex = frmParam.kindMachine;
			areaPlotParam = areaPlotParamMgr.GetAreaPlotParam(1);
			UnitName1 = areaPlotParam.UintName;
			areaPlotParam = areaPlotParamMgr.GetAreaPlotParam(11);
			UnitName2 = areaPlotParam.UintName;
			formVoc.lbNMHCDanWei.Text = UnitName1;
			formVoc.lbBXWDanWei.Text = UnitName2;
			areaPlotParam = areaPlotParamMgr.GetAreaPlotParam(19);
			formVoc.lbBTEX9T.Text = (lbBTEX9T.Text = areaPlotParam.PeakName);
			areaPlotParam = areaPlotParamMgr.GetAreaPlotParam(18);
			formVoc.lbBTEX8T.Text = (lbBTEX8T.Text = areaPlotParam.PeakName);
			areaPlotParam = areaPlotParamMgr.GetAreaPlotParam(17);
			formVoc.lbBTEX7T.Text = (lbBTEX7T.Text = areaPlotParam.PeakName);
			areaPlotParam = areaPlotParamMgr.GetAreaPlotParam(16);
			formVoc.lbBTEX6T.Text = (lbBTEX6T.Text = areaPlotParam.PeakName);
			areaPlotParam = areaPlotParamMgr.GetAreaPlotParam(15);
			formVoc.lbBTEX5T.Text = (lbBTEX5T.Text = areaPlotParam.PeakName);
			areaPlotParam = areaPlotParamMgr.GetAreaPlotParam(14);
			formVoc.lbBTEX4T.Text = (lbBTEX4T.Text = areaPlotParam.PeakName);
			areaPlotParam = areaPlotParamMgr.GetAreaPlotParam(13);
			formVoc.lbBTEX3T.Text = (lbBTEX3T.Text = areaPlotParam.PeakName);
			areaPlotParam = areaPlotParamMgr.GetAreaPlotParam(12);
			formVoc.lbBTEX2T.Text = (lbBTEX2T.Text = areaPlotParam.PeakName);
			areaPlotParam = areaPlotParamMgr.GetAreaPlotParam(11);
			formVoc.lbBTEX1T.Text = (lbBTEX1T.Text = areaPlotParam.PeakName);
			formVoc.lbBTEXt.Text = lbBTEXt.Text;
			if (frmParam.kindMachine == 4)
			{
				formVoc.labAnaTimes.Text = cdlMgr.CurrentInsDeviceMgr.injectBotNum;
			}
			if (frmParam.kindMachine == 0)
			{
				gbNHMC1.Visible = true;
				gbBenXW.Visible = true;
				formVoc.gbNHMC1.Visible = true;
				formVoc.gbBenXW.Visible = true;
				formVoc.sctSetting.Panel2Collapsed = false;
			}
			else if (frmParam.kindMachine == 1)
			{
				gbNHMC1.Visible = true;
				gbBenXW.Visible = false;
				formVoc.gbNHMC1.Visible = true;
				formVoc.gbBenXW.Visible = false;
				formVoc.sctSetting.Panel2Collapsed = true;
				formVoc.label77.Location = new Point(500, 41);
				formVoc.lbTHC.Location = new Point(630, 41);
				formVoc.label79.Location = new Point(500, 113);
				formVoc.lbCH4.Location = new Point(630, 113);
				formVoc.lbNMHCT.Location = new Point(500, 340);
				formVoc.lbNMHC.Location = new Point(630, 340);
				formVoc.gbNHMC1.Controls.Add(formVoc.btnData);
				formVoc.btnData.Location = new Point(984, 496);
			}
			else if (frmParam.kindMachine == 2)
			{
				gbNHMC1.Visible = true;
				gbBenXW.Visible = true;
				gbNHMC1.Text = Lang.PS("非甲烷总烃1", "NMHC1");
				gbBenXW.Text = Lang.PS("非甲烷总烃2", "NMHC2");
				lbBTEX1T.Text = Lang.PS("总烃", "THC");
				lbBTEX2T.Text = Lang.PS("甲烷", "CH4");
				lbBTEXt.Text = Lang.PS("非甲烷总烃", "NMHC");
				lbBTEXt.Location = new Point(8, 50);
				lbBTEX.Location = new Point(75, 50);
				lbBTEX3.Visible = false;
				lbBTEX3T.Visible = false;
				lbBTEX4.Visible = false;
				lbBTEX4T.Visible = false;
				lbBTEX5.Visible = false;
				lbBTEX5T.Visible = false;
				lbBTEX6.Visible = false;
				lbBTEX6T.Visible = false;
				lbBTEX7.Visible = false;
				lbBTEX7T.Visible = false;
				lbBTEX8.Visible = false;
				lbBTEX8T.Visible = false;
				lbBTEX9.Visible = false;
				lbBTEX9T.Visible = false;
				formVoc.gbNHMC1.Visible = true;
				formVoc.gbBenXW.Visible = true;
				formVoc.sctSetting.Panel2Collapsed = false;
				formVoc.lbBTEX2T.Text = Lang.PS("甲      烷：", "CH4");
				formVoc.lbBTEX1T.Text = Lang.PS("总      烃：", "THC");
				formVoc.lbBTEX2T.Location = new Point(16, 124);
				formVoc.lbBTEX2.Location = new Point(formVoc.lbBTEX1.Location.X, 124);
				formVoc.lbBTEX3T.Visible = false;
				formVoc.lbBTEX3.Visible = false;
				formVoc.lbBTEX4T.Visible = false;
				formVoc.lbBTEX4.Visible = false;
				formVoc.lbBTEX5T.Visible = false;
				formVoc.lbBTEX5.Visible = false;
				formVoc.lbBTEX6T.Visible = false;
				formVoc.lbBTEX6.Visible = false;
				formVoc.lbBTEX7T.Visible = false;
				formVoc.lbBTEX7.Visible = false;
				formVoc.lbBTEX8T.Visible = false;
				formVoc.lbBTEX8.Visible = false;
				formVoc.lbBTEX9T.Visible = false;
				formVoc.lbBTEX9.Visible = false;
				formVoc.lbBTEXt.Text = lbBTEXt.Text;
			}
			else if (frmParam.kindMachine == 3)
			{
				gbNHMC1.Visible = false;
				gbBenXW.Visible = true;
				gbBenXW.Location = gbNHMC1.Location;
				formVoc.gbNHMC1.Visible = false;
				formVoc.gbBenXW.Visible = true;
				formVoc.sctSetting.Panel1Collapsed = true;
			}
			else if (frmParam.kindMachine == 4)
			{
				gbNHMC1.Visible = true;
				gbBenXW.Visible = true;
				formVoc.gbNHMC1.Visible = true;
				formVoc.gbBenXW.Visible = true;
				formVoc.sctSetting.Panel2Collapsed = false;
			}
			chbO2.Checked = frmParam.bO;
			labO.Text = frmParam.fValueO.ToString("F" + Class49.int_8);
			try
			{
				tbSdaFileName.Text = Path.GetFileName(frmParam.strSdaFile);
			}
			catch
			{
			}
			tbTimeCycle1.Text = frmParam.fTabChannel1.ToString("F" + Class49.int_8);
			tbTimeCycle2.Text = frmParam.fTabChannel2.ToString("F" + Class49.int_8);
			chbCycle.Checked = frmParam.bCycle;
			chbCycle2.Checked = frmParam.bCycle2;
			tbPowerOnDelay.Text = frmParam.fPowerOnDealy.ToString("F" + Class49.int_8);
			tbTimesCycle1.Text = frmParam.iTimesCycle1.ToString();
			tbTimesCycle2.Text = frmParam.iTimesCycle2.ToString();
			tabPage3.Parent = null;
			formVoc.Show();
			selectCom2Fun();
			m_bLoading = false;
		}
	}

	private void LoadLanguage()
	{
		btnFireOnCheck.Text = Lang.PS("点火门限查询", "Igquery");
		btnFireOnSet.Text = Lang.PS("点火门限设定", "Igset");
		btnDevice.Text = Lang.PS("色谱机", "Instrument");
		button36.Text = Lang.PS("数据界面", "Data interface");
		btShowDesktop.Text = Lang.PS("显示桌面", "ShowDesktop");
		label1.Text = Lang.PS("COM2功能", "COM2 setting");
		label80.Text = Lang.PS("点火门限", "Ignition");
		btnCali.Text = Lang.PS("一键标定", "Calibration");
		gbNHMC1.Text = Lang.PS("非甲烷总烃", "NMHC");
		gbBenXW.Text = Lang.PS("苯系物", "MACHs");
		lbBTEXt.Text = Lang.PS("苯系物", "MACHs");
		label77.Text = Lang.PS("总烃", "THC");
		label79.Text = Lang.PS("甲烷", "CH4");
		lbNMHCT.Text = Lang.PS("非甲烷总烃", "NMHC");
		tabPage1.Text = Lang.PS("数据", "data");
		tabPage2.Text = Lang.PS("氧干扰", "Oxygen interference");
		tabPage3.Text = Lang.PS("通道循环数据", "Channel cycle data");
		chbO2.Text = Lang.PS("使用氧干扰扣除", "Use oxygen interference deduction");
		label3.Text = Lang.PS("氧干扰值", "Oxygen");
		label2.Text = Lang.PS("氧干扰谱图", "spectrogram");
		label11.Text = Lang.PS("通道1循环时间", "cycle time 1");
		label10.Text = Lang.PS("通道2循环时间", "cycle time 2");
		label15.Text = Lang.PS("启动后采集延时", "delay startup");
		label4.Text = Lang.PS("通道1循环次数", "loops 1");
		label5.Text = Lang.PS("通道2循环次数", "loops 2");
		chbCycle.Text = Lang.PS("通道1自动循环", "Channel 1 loops automatically");
		chbCycle2.Text = Lang.PS("通道2自动循环", "Channel 2 loops automatically");
	}

	public void disposeVOCPeaks(int selectedIndex, string fileName, string strID, string strSampleIndex, Chromatogram chromatogram)
	{
		AreaPlotParamMgr areaPlotParamMgr = AreaPlotParamMgr.Create();
		AreaPlotParam areaPlotParam = null;
		int num = 0;
		byte b = 0;
		byte b2 = 0;
		float num2 = 0f;
		float amount = 0f;
		int num3 = 0;
		if (cdlMgr.CurrentChartParaOpera.mtdMgr.caliGnl == null)
		{
			cdlMgr.CurrentChartParaOpera.mtdMgr.caliGnl = new CaliGnl();
		}
		CaliGnl caliGnl = cdlMgr.CurrentChartParaOpera.mtdMgr.caliGnl;
		Peak[] rltPeaks = chromatogram.RltPeaks;
		float[] array = new float[1];
		ushort[] array2 = new ushort[2];
		float[] array3 = new float[50];
		if (frmParam.kindMachine == 3)
		{
			selectedIndex = 1;
		}
		switch (selectedIndex)
		{
		case 0:
		{
			strFileName = fileName;
			array = new float[1] { array3[b] };
			array[0] = 0f;
			Buffer.BlockCopy(array, 0, array2, 0, 4);
			if (frmParam.kindMachine == 4)
			{
				cdlMgr.tcpServerMgr.mComModbus.WordVaue[14] = array2[0];
				cdlMgr.tcpServerMgr.mComModbus.WordVaue[15] = array2[1];
				cdlMgr.tcpServerMgr.mComModbus.WordVaue[16] = array2[0];
				cdlMgr.tcpServerMgr.mComModbus.WordVaue[17] = array2[1];
				cdlMgr.tcpServerMgr.mComModbus.WordVaue[34] = array2[0];
				cdlMgr.tcpServerMgr.mComModbus.WordVaue[35] = array2[1];
				cdlMgr.tcpServerMgr.mComModbus.WordVaue[36] = array2[0];
				cdlMgr.tcpServerMgr.mComModbus.WordVaue[37] = array2[1];
				cdlMgr.tcpServerMgr.mComModbus.WordVaue[38] = array2[0];
				cdlMgr.tcpServerMgr.mComModbus.WordVaue[39] = array2[1];
				cdlMgr.tcpServerMgr.mComModbus.WordVaue[40] = array2[0];
				cdlMgr.tcpServerMgr.mComModbus.WordVaue[41] = array2[1];
				cdlMgr.tcpServerMgr.mComModbus.WordVaue[42] = array2[0];
				cdlMgr.tcpServerMgr.mComModbus.WordVaue[43] = array2[1];
				cdlMgr.tcpServerMgr.mComModbus.WordVaue[44] = array2[0];
				cdlMgr.tcpServerMgr.mComModbus.WordVaue[45] = array2[1];
				cdlMgr.tcpServerMgr.mComModbus.WordVaue[46] = array2[0];
				cdlMgr.tcpServerMgr.mComModbus.WordVaue[47] = array2[1];
				cdlMgr.tcpServerMgr.mComModbus.WordVaue[48] = array2[0];
				cdlMgr.tcpServerMgr.mComModbus.WordVaue[49] = array2[1];
				cdlMgr.tcpServerMgr.mComModbus.WordVaue[50] = array2[0];
				cdlMgr.tcpServerMgr.mComModbus.WordVaue[51] = array2[1];
				cdlMgr.tcpServerMgr.mComModbus.WordVaue[64] = array2[0];
				cdlMgr.tcpServerMgr.mComModbus.WordVaue[65] = array2[1];
				cdlMgr.tcpServerMgr.mComModbus.WordVaue[66] = array2[0];
				cdlMgr.tcpServerMgr.mComModbus.WordVaue[67] = array2[1];
				cdlMgr.tcpServerMgr.mComModbus.WordVaue[68] = array2[0];
				cdlMgr.tcpServerMgr.mComModbus.WordVaue[69] = array2[1];
				cdlMgr.tcpServerMgr.mComModbus.WordVaue[70] = array2[0];
				cdlMgr.tcpServerMgr.mComModbus.WordVaue[71] = array2[1];
				cdlMgr.tcpServerMgr.mComModbus.WordVaue[72] = array2[0];
				cdlMgr.tcpServerMgr.mComModbus.WordVaue[73] = array2[1];
				cdlMgr.tcpServerMgr.mComModbus.WordVaue[74] = array2[0];
				cdlMgr.tcpServerMgr.mComModbus.WordVaue[75] = array2[1];
				cdlMgr.tcpServerMgr.mComModbus.WordVaue[76] = array2[0];
				cdlMgr.tcpServerMgr.mComModbus.WordVaue[77] = array2[1];
				cdlMgr.tcpServerMgr.mComModbus.WordVaue[78] = array2[0];
				cdlMgr.tcpServerMgr.mComModbus.WordVaue[79] = array2[1];
			}
			else
			{
				cdlMgr.tcpServerMgr.mComModbus.WordVaue[14] = array2[0];
				cdlMgr.tcpServerMgr.mComModbus.WordVaue[15] = array2[1];
				cdlMgr.tcpServerMgr.mComModbus.WordVaue[16] = array2[0];
				cdlMgr.tcpServerMgr.mComModbus.WordVaue[17] = array2[1];
				cdlMgr.tcpServerMgr.mComModbus.WordVaue[64] = array2[0];
				cdlMgr.tcpServerMgr.mComModbus.WordVaue[65] = array2[1];
				cdlMgr.tcpServerMgr.mComModbus.WordVaue[66] = array2[0];
				cdlMgr.tcpServerMgr.mComModbus.WordVaue[67] = array2[1];
			}
			CaliGnl caliGnl3 = cdlMgr.ChartParaOperaList[0].mtdMgr.caliGnl;
			CountAnalyse++;
			int num6 = 255;
			int num7 = 255;
			float num8 = 0f;
			float num9 = 0f;
			lbTHC.Text = "0" + UnitName1;
			lbCH4.Text = "0" + UnitName2;
			lbNMHC.Text = "0" + UnitName3;
			flagChannelOver1 = true;
			areaPlotParam = areaPlotParamMgr.GetAreaPlotParam(1);
			UnitName1 = areaPlotParam.UintName;
			array = new float[1];
			Buffer.BlockCopy(array, 0, array2, 0, 4);
			cdlMgr.tcpServerMgr.mComModbus.WordVaue[14] = array2[0];
			cdlMgr.tcpServerMgr.mComModbus.WordVaue[15] = array2[1];
			cdlMgr.tcpServerMgr.mComModbus.WordVaue[16] = array2[0];
			cdlMgr.tcpServerMgr.mComModbus.WordVaue[17] = array2[1];
			for (b = 0; b < cdlMgr.ChartParaOperaList[0].mtdMgr.caliGnl.cmpds.Count(); b++)
			{
				for (num = 0; 1 <= rltPeaks.Count() && num < rltPeaks.Count(); num++)
				{
					if (!(rltPeaks[num].pkRT >= caliGnl3.cmpds[b].cmpdInfo.retainTime - caliGnl3.cmpds[b].cmpdInfo.leftWindow) || !(rltPeaks[num].pkRT <= caliGnl3.cmpds[b].cmpdInfo.retainTime + caliGnl3.cmpds[b].cmpdInfo.rightWindow) || !(rltPeaks[num].height >= num2))
					{
						continue;
					}
					if (cdlMgr.formMain.IsAutoCalibra == 1)
					{
						num3 = 2;
						b2++;
						if (caliGnl3.cmpds[b].cmpdInfo.respStyle == RespStyle.Area)
						{
							caliGnl3.cmpds[b].levels[0].responseA = rltPeaks[num].area;
							caliGnl3.cmpds[b].levels[0].responseH = rltPeaks[num].height;
							caliGnl3.CalculateFunc(appendLink: false);
							caliGnl3.cmpds[b].levels[0].respFactor = rltPeaks[num].GasAmount / caliGnl3.cmpds[b].levels[0].responseA;
						}
						else if (caliGnl3.cmpds[b].cmpdInfo.respStyle == RespStyle.Height)
						{
							caliGnl3.cmpds[b].levels[0].responseA = rltPeaks[num].area;
							caliGnl3.cmpds[b].levels[0].responseH = rltPeaks[num].height;
							caliGnl3.CalculateFunc(appendLink: false);
							caliGnl3.cmpds[b].levels[0].respFactor = rltPeaks[num].GasAmount / caliGnl3.cmpds[b].levels[0].responseH;
						}
						cdlMgr.ChartParaOperaList[0].mtdMgr.SaveToFile();
						cdlMgr.formMain.MainmstSet.ReadFromMtdMgr();
						switch (b)
						{
						case 0:
							num8 = rltPeaks[num].area * caliGnl3.cmpds[b].levels[0].respFactor;
							lbTHC.Text = num8.ToString("0.00") + UnitName1;
							break;
						case 1:
							num9 = (rltPeaks[num].amount = rltPeaks[num].area * caliGnl3.cmpds[b].levels[0].respFactor);
							lbCH4.Text = rltPeaks[num].amount.ToString("0.00") + UnitName1;
							break;
						}
						cdlMgr.ChartParaOperaList[0].mtdMgr.SaveToFile();
						cdlMgr.formMain.MainmstSet.UsePara();
						cdlMgr.formMain.MainmstSet.ReadFromMtdMgr();
						cdlMgr.formMain.MainmstSet.bUseSet_Click(null, null);
						LogMgr.Instance.Write2RunLog("VocCtrl.disposeVOCPeaks  index:" + num + " peak.Count():" + rltPeaks.Count() + " amount2:" + num9 + "respFactor" + caliGnl3.cmpds[b].levels[0].respFactor + "GadAmount:" + rltPeaks[num].GasAmount + "area:" + rltPeaks[num].area);
						continue;
					}
					if (cdlMgr.formMain.IsAutoCalibra == 2)
					{
						num3 = cdlMgr.ChartParaOperaList[0].mtdMgr.caliGnl.cmpds.Count() - 2;
						b2++;
						if (b >= 2)
						{
							byte b3 = (byte)(b - 2);
							if (caliGnl3.cmpds[b].cmpdInfo.respStyle == RespStyle.Area)
							{
								caliGnl3.cmpds[b].levels[0].responseA = rltPeaks[num].area;
								caliGnl3.cmpds[b].levels[0].responseH = rltPeaks[num].height;
								caliGnl3.CalculateFunc(appendLink: false);
								caliGnl3.cmpds[b].levels[0].respFactor = rltPeaks[num].GasAmount / caliGnl3.cmpds[b].levels[0].responseA;
							}
							else if (caliGnl3.cmpds[b].cmpdInfo.respStyle == RespStyle.Height)
							{
								caliGnl3.cmpds[b].levels[0].responseA = rltPeaks[num].area;
								caliGnl3.cmpds[b].levels[0].responseH = rltPeaks[num].height;
								caliGnl3.CalculateFunc(appendLink: false);
								caliGnl3.cmpds[b].levels[0].respFactor = rltPeaks[num].GasAmount / caliGnl3.cmpds[b].levels[0].responseH;
							}
							cdlMgr.ChartParaOperaList[0].mtdMgr.SaveToFile();
							cdlMgr.formMain.MainmstSet.ReadFromMtdMgr();
							array3[b3] = (rltPeaks[num].amount = rltPeaks[num].area * caliGnl3.cmpds[b].levels[0].respFactor);
							cdlMgr.ChartParaOperaList[0].mtdMgr.SaveToFile();
							cdlMgr.formMain.MainmstSet.UsePara();
							cdlMgr.formMain.MainmstSet.ReadFromMtdMgr();
							cdlMgr.formMain.MainmstSet.bUseSet_Click(null, null);
						}
						continue;
					}
					if (cdlMgr.formMain.IsAutoCalibra == 3)
					{
						num3 = cdlMgr.ChartParaOperaList[0].mtdMgr.caliGnl.cmpds.Count();
						b2++;
						if (caliGnl3.cmpds[b].cmpdInfo.respStyle == RespStyle.Area)
						{
							caliGnl3.cmpds[b].levels[0].responseA = rltPeaks[num].area;
							caliGnl3.cmpds[b].levels[0].responseH = rltPeaks[num].height;
							caliGnl3.CalculateFunc(appendLink: false);
							caliGnl3.cmpds[b].levels[0].respFactor = rltPeaks[num].GasAmount / caliGnl3.cmpds[b].levels[0].responseA;
						}
						else if (caliGnl3.cmpds[b].cmpdInfo.respStyle == RespStyle.Height)
						{
							caliGnl3.cmpds[b].levels[0].responseA = rltPeaks[num].area;
							caliGnl3.cmpds[b].levels[0].responseH = rltPeaks[num].height;
							caliGnl3.CalculateFunc(appendLink: false);
							caliGnl3.cmpds[b].levels[0].respFactor = rltPeaks[num].GasAmount / caliGnl3.cmpds[b].levels[0].responseH;
						}
						cdlMgr.ChartParaOperaList[0].mtdMgr.SaveToFile();
						cdlMgr.formMain.MainmstSet.ReadFromMtdMgr();
						switch (b)
						{
						case 0:
							if (caliGnl3.cmpds[b].cmpdInfo.respStyle == RespStyle.Area)
							{
								num8 = rltPeaks[num].area * caliGnl3.cmpds[b].levels[0].respFactor;
							}
							else if (caliGnl3.cmpds[b].cmpdInfo.respStyle == RespStyle.Height)
							{
								num8 = rltPeaks[num].height * caliGnl3.cmpds[b].levels[0].respFactor;
							}
							lbTHC.Text = num8.ToString("0.00") + UnitName1;
							break;
						case 1:
							num9 = (rltPeaks[num].amount = rltPeaks[num].area * caliGnl3.cmpds[b].levels[0].respFactor);
							lbCH4.Text = rltPeaks[num].amount.ToString("0.00") + UnitName1;
							break;
						default:
							array3[b - 2] = (rltPeaks[num].amount = rltPeaks[num].area * caliGnl3.cmpds[b].levels[0].respFactor);
							break;
						}
						cdlMgr.ChartParaOperaList[0].mtdMgr.SaveToFile();
						cdlMgr.formMain.MainmstSet.UsePara();
						cdlMgr.formMain.MainmstSet.ReadFromMtdMgr();
						cdlMgr.formMain.MainmstSet.bUseSet_Click(null, null);
						continue;
					}
					if (bCalibration)
					{
						num3 = cdlMgr.ChartParaOperaList[0].mtdMgr.caliGnl.cmpds.Count();
						b2++;
						if (caliGnl3.cmpds[b].cmpdInfo.respStyle == RespStyle.Area)
						{
							cdlMgr.ChartParaOperaList[0].mtdMgr.caliGnl.cmpds[b].levels[0].responseA = rltPeaks[num].area;
							cdlMgr.ChartParaOperaList[0].mtdMgr.caliGnl.cmpds[b].levels[0].responseH = rltPeaks[num].height;
						}
						else if (caliGnl3.cmpds[b].cmpdInfo.respStyle == RespStyle.Height)
						{
							cdlMgr.ChartParaOperaList[0].mtdMgr.caliGnl.cmpds[b].levels[0].responseA = rltPeaks[num].area;
							cdlMgr.ChartParaOperaList[0].mtdMgr.caliGnl.cmpds[b].levels[0].responseH = rltPeaks[num].height;
						}
						continue;
					}
					if (rltPeaks[num].amount < 0f)
					{
						rltPeaks[num].amount = 0f;
					}
					data2Array(rltPeaks[num].name, rltPeaks[num].amount);
					switch (b)
					{
					case 0:
						num6 = num;
						if (frmParam.bO)
						{
							rltPeaks[num].amount -= frmParam.fValueO;
							if (rltPeaks[num].amount < 0f)
							{
								rltPeaks[num].amount = 0f;
							}
						}
						num8 = rltPeaks[num].amount;
						lbTHC.Text = num8.ToString("0.00");
						array = new float[1] { num8 };
						Buffer.BlockCopy(array, 0, array2, 0, 4);
						cdlMgr.tcpServerMgr.mComModbus.WordVaue[14] = array2[0];
						cdlMgr.tcpServerMgr.mComModbus.WordVaue[15] = array2[1];
						array = new float[1] { rltPeaks[num].height };
						Buffer.BlockCopy(array, 0, array2, 0, 4);
						cdlMgr.tcpServerMgr.mComModbus.WordVaue[64] = array2[0];
						cdlMgr.tcpServerMgr.mComModbus.WordVaue[65] = array2[1];
						Class49.InsertIntoVoc(1, 0, rltPeaks[num].name, fileName.ToLower(), num8);
						break;
					case 1:
						if (caliGnl3.cmpds[b].cmpdInfo.respStyle == RespStyle.Area)
						{
							num9 = (rltPeaks[num].amount = rltPeaks[num].area * caliGnl3.cmpds[b].levels[0].respFactor);
						}
						else if (caliGnl3.cmpds[b].cmpdInfo.respStyle == RespStyle.Height)
						{
							num9 = (rltPeaks[num].amount = rltPeaks[num].height * caliGnl3.cmpds[b].levels[0].respFactor);
						}
						num7 = num;
						lbCH4.Text = rltPeaks[num].amount.ToString("0.00");
						array = new float[1] { rltPeaks[num].amount };
						Buffer.BlockCopy(array, 0, array2, 0, 4);
						cdlMgr.tcpServerMgr.mComModbus.WordVaue[16] = array2[0];
						cdlMgr.tcpServerMgr.mComModbus.WordVaue[17] = array2[1];
						array = new float[1] { rltPeaks[num].height };
						Buffer.BlockCopy(array, 0, array2, 0, 4);
						cdlMgr.tcpServerMgr.mComModbus.WordVaue[66] = array2[0];
						cdlMgr.tcpServerMgr.mComModbus.WordVaue[67] = array2[1];
						Class49.InsertIntoVoc(2, 0, rltPeaks[num].name, fileName.ToLower(), rltPeaks[num].amount);
						break;
					default:
					{
						if (frmParam.kindMachine != 4)
						{
							continue;
						}
						byte b4 = (byte)(b - 2);
						array3[b4] = rltPeaks[num].amount;
						array = new float[1] { array3[b4] };
						Buffer.BlockCopy(array, 0, array2, 0, 4);
						Class49.InsertIntoVoc(11 + b4, 0, rltPeaks[num].name, fileName.ToLower(), rltPeaks[num].amount);
						if (b4 < 0)
						{
							continue;
						}
						lbBTEX9.Text = array3[8].ToString("0.00");
						lbBTEX8.Text = array3[7].ToString("0.00");
						lbBTEX7.Text = array3[6].ToString("0.00");
						lbBTEX6.Text = array3[5].ToString("0.00");
						lbBTEX5.Text = array3[4].ToString("0.00");
						lbBTEX4.Text = array3[3].ToString("0.00");
						lbBTEX3.Text = array3[2].ToString("0.00");
						lbBTEX2.Text = array3[1].ToString("0.00");
						lbBTEX1.Text = array3[0].ToString("0.00");
						if (frmParam.kindMachine == 2)
						{
							if (array3[0] > array3[1])
							{
								lbBTEX.Text = (array3[0] - array3[1]).ToString("0.00");
							}
							else
							{
								lbBTEX.Text = "0.00";
							}
						}
						else
						{
							amount = array3[0] + array3[1] + array3[2] + array3[3] + array3[4] + array3[5] + array3[6] + array3[7] + array3[8];
							lbBTEX.Text = amount.ToString("0.00");
						}
						array = new float[1] { array3[b] };
						array[0] = array3[0];
						Buffer.BlockCopy(array, 0, array2, 0, 4);
						cdlMgr.tcpServerMgr.mComModbus.WordVaue[34] = array2[0];
						cdlMgr.tcpServerMgr.mComModbus.WordVaue[35] = array2[1];
						array[0] = array3[1];
						Buffer.BlockCopy(array, 0, array2, 0, 4);
						cdlMgr.tcpServerMgr.mComModbus.WordVaue[36] = array2[0];
						cdlMgr.tcpServerMgr.mComModbus.WordVaue[37] = array2[1];
						array[0] = array3[2];
						Buffer.BlockCopy(array, 0, array2, 0, 4);
						cdlMgr.tcpServerMgr.mComModbus.WordVaue[38] = array2[0];
						cdlMgr.tcpServerMgr.mComModbus.WordVaue[39] = array2[1];
						array[0] = array3[3];
						Buffer.BlockCopy(array, 0, array2, 0, 4);
						cdlMgr.tcpServerMgr.mComModbus.WordVaue[40] = array2[0];
						cdlMgr.tcpServerMgr.mComModbus.WordVaue[41] = array2[1];
						array[0] = array3[4];
						Buffer.BlockCopy(array, 0, array2, 0, 4);
						cdlMgr.tcpServerMgr.mComModbus.WordVaue[42] = array2[0];
						cdlMgr.tcpServerMgr.mComModbus.WordVaue[43] = array2[1];
						array[0] = array3[5];
						Buffer.BlockCopy(array, 0, array2, 0, 4);
						cdlMgr.tcpServerMgr.mComModbus.WordVaue[44] = array2[0];
						cdlMgr.tcpServerMgr.mComModbus.WordVaue[45] = array2[1];
						array[0] = array3[6];
						Buffer.BlockCopy(array, 0, array2, 0, 4);
						cdlMgr.tcpServerMgr.mComModbus.WordVaue[46] = array2[0];
						cdlMgr.tcpServerMgr.mComModbus.WordVaue[47] = array2[1];
						array[0] = array3[7];
						Buffer.BlockCopy(array, 0, array2, 0, 4);
						cdlMgr.tcpServerMgr.mComModbus.WordVaue[48] = array2[0];
						cdlMgr.tcpServerMgr.mComModbus.WordVaue[49] = array2[1];
						array[0] = array3[8];
						Buffer.BlockCopy(array, 0, array2, 0, 4);
						cdlMgr.tcpServerMgr.mComModbus.WordVaue[50] = array2[0];
						cdlMgr.tcpServerMgr.mComModbus.WordVaue[51] = array2[1];
						array = new float[1] { rltPeaks[num].height };
						Buffer.BlockCopy(array, 0, array2, 0, 4);
						cdlMgr.tcpServerMgr.mComModbus.WordVaue[68 + (num - 2) * 2] = array2[0];
						cdlMgr.tcpServerMgr.mComModbus.WordVaue[69 + (num - 2) * 2] = array2[1];
						Class49.InsertIntoVoc(20, 0, null, fileName.ToLower(), array3[0] + array3[1] + array3[2] + array3[3] + array3[4] + array3[5] + array3[6] + array3[7] + array3[8]);
						break;
					}
					}
					break;
				}
			}
			if (bCalibration)
			{
				bCalibration = false;
				cdlMgr.ChartParaOperaList[0].mtdMgr.caliGnl.CalculateFunc(appendLink: false);
				cdlMgr.formMain.MainmstSet.CalcuFreeRespFactor(cdlMgr.ChartParaOperaList[0].mtdMgr.caliGnl);
				cdlMgr.ChartParaOperaList[0].mtdMgr.SaveToFile();
				cdlMgr.formMain.MainmstSet.ReadFromMtdMgr();
				cdlMgr.formMain.MainmstSet.bUseSet_Click(null, null);
			}
			if (cdlMgr.formMain.IsAutoCalibra != 0)
			{
				lbBTEX9.Text = array3[8].ToString("0.00");
				lbBTEX8.Text = array3[7].ToString("0.00");
				lbBTEX7.Text = array3[6].ToString("0.00");
				lbBTEX6.Text = array3[5].ToString("0.00");
				lbBTEX5.Text = array3[4].ToString("0.00");
				lbBTEX4.Text = array3[3].ToString("0.00");
				lbBTEX3.Text = array3[2].ToString("0.00");
				lbBTEX2.Text = array3[1].ToString("0.00");
				lbBTEX1.Text = array3[0].ToString("0.00");
				if (b >= caliGnl3.cmpds.Count())
				{
					cdlMgr.formMain.IsAutoCalibra = 0;
					if (b2 >= num3)
					{
						MessageBox.Show("标定成功！");
					}
					else
					{
						MessageBox.Show("标定失败！");
					}
					btnCali.BackColor = btnFireOnSet.BackColor;
					cdlMgr.formMain.tabControl.SelectedIndex = 2;
					cdlMgr.formMain.chromFormCtrl.OpenChrom(fileName, sampling: false, useCurrent: true);
					cdlMgr.currentTcpServerMgrSendCmd(19);
					cdlMgr.formMain.tabChannel.Enabled = true;
					return;
				}
			}
			float num10;
			if (num8 > num9)
			{
				num10 = num8 - num9;
				lbNMHC.Text = (num8 - num9).ToString("0.00");
				Class49.InsertIntoVoc(3, 0, "非甲烷总烃", fileName.ToLower(), num8 - num9);
			}
			else
			{
				num10 = 0f;
				lbNMHC.Text = "0.00";
				Class49.InsertIntoVoc(3, 0, "非甲烷总烃", fileName.ToLower(), 0f);
			}
			array = new float[1] { num10 };
			Buffer.BlockCopy(array, 0, array2, 0, 4);
			cdlMgr.tcpServerMgr.mComModbus.WordVaue[18] = array2[0];
			cdlMgr.tcpServerMgr.mComModbus.WordVaue[19] = array2[1];
			data2Array("非甲烷总烃", num10);
			data2Array("苯系物", amount);
			ushort num11 = 0;
			num11 = (ushort)((num8 - frmParam.fmount41) / (frmParam.fmount201 - frmParam.fmount41) * 4095f);
			if (num11 > 4095)
			{
				num11 = 4095;
			}
			serialPoartBase.Data2[7] = (byte)(num11 >> 8);
			serialPoartBase.Data2[8] = (byte)num11;
			num11 = (ushort)((num9 - frmParam.fmount42) / (frmParam.fmount202 - frmParam.fmount42) * 4095f);
			if (num11 > 4095)
			{
				num11 = 4095;
			}
			serialPoartBase.Data2[9] = (byte)(num11 >> 8);
			serialPoartBase.Data2[10] = (byte)num11;
			num11 = (ushort)((num10 - frmParam.fmount43) / (frmParam.fmount203 - frmParam.fmount43) * 4095f);
			if (num11 > 4095)
			{
				num11 = 4095;
			}
			serialPoartBase.Data2[11] = (byte)(num11 >> 8);
			serialPoartBase.Data2[12] = (byte)num11;
			if (frmParam.kindMachine == 4)
			{
				num11 = (ushort)((array3[0] - frmParam.fmount44) / (frmParam.fmount204 - frmParam.fmount44) * 4095f);
				if (num11 > 4095)
				{
					num11 = 4095;
				}
				serialPoartBase.Data2[13] = (byte)(num11 >> 8);
				serialPoartBase.Data2[14] = (byte)num11;
				num11 = (ushort)((array3[1] - frmParam.fmount45) / (frmParam.fmount205 - frmParam.fmount45) * 4095f);
				if (num11 > 4095)
				{
					num11 = 4095;
				}
				serialPoartBase.Data2[15] = (byte)(num11 >> 8);
				serialPoartBase.Data2[16] = (byte)num11;
				num11 = (ushort)((array3[2] - frmParam.fmount46) / (frmParam.fmount206 - frmParam.fmount46) * 4095f);
				if (num11 > 4095)
				{
					num11 = 4095;
				}
				serialPoartBase.Data2[17] = (byte)(num11 >> 8);
				serialPoartBase.Data2[18] = (byte)num11;
				num11 = (ushort)((array3[3] - frmParam.fmount47) / (frmParam.fmount207 - frmParam.fmount47) * 4095f);
				if (num11 > 4095)
				{
					num11 = 4095;
				}
				serialPoartBase.Data2[19] = (byte)(num11 >> 8);
				serialPoartBase.Data2[20] = (byte)num11;
				num11 = (ushort)((array3[4] - frmParam.fmount48) / (frmParam.fmount208 - frmParam.fmount48) * 4095f);
				if (num11 > 4095)
				{
					num11 = 4095;
				}
				serialPoartBase.Data2[21] = (byte)(num11 >> 8);
				serialPoartBase.Data2[22] = (byte)num11;
				num11 = (ushort)((array3[5] - frmParam.fmount49) / (frmParam.fmount209 - frmParam.fmount49) * 4095f);
				if (num11 > 4095)
				{
					num11 = 4095;
				}
				serialPoartBase.Data2[23] = (byte)(num11 >> 8);
				serialPoartBase.Data2[24] = (byte)num11;
				num11 = (ushort)((array3[6] - frmParam.fmount410) / (frmParam.fmount2010 - frmParam.fmount410) * 4095f);
				if (num11 > 4095)
				{
					num11 = 4095;
				}
				serialPoartBase.Data2[25] = (byte)(num11 >> 8);
				serialPoartBase.Data2[26] = (byte)num11;
				num11 = (ushort)((array3[7] - frmParam.fmount411) / (frmParam.fmount2011 - frmParam.fmount411) * 4095f);
				if (num11 > 4095)
				{
					num11 = 4095;
				}
				serialPoartBase.Data2[27] = (byte)(num11 >> 8);
				serialPoartBase.Data2[28] = (byte)num11;
				num11 = (ushort)((array3[8] - frmParam.fmount412) / (frmParam.fmount2012 - frmParam.fmount412) * 4095f);
				if (num11 > 4095)
				{
					num11 = 4095;
				}
				serialPoartBase.Data2[29] = (byte)(num11 >> 8);
				serialPoartBase.Data2[30] = (byte)num11;
			}
			break;
		}
		case 1:
		{
			float num4 = 0f;
			strFileName2 = fileName;
			array = new float[1] { array3[b] };
			array[0] = 0f;
			Buffer.BlockCopy(array, 0, array2, 0, 4);
			cdlMgr.tcpServerMgr.mComModbus.WordVaue[34] = array2[0];
			cdlMgr.tcpServerMgr.mComModbus.WordVaue[35] = array2[1];
			cdlMgr.tcpServerMgr.mComModbus.WordVaue[36] = array2[0];
			cdlMgr.tcpServerMgr.mComModbus.WordVaue[37] = array2[1];
			cdlMgr.tcpServerMgr.mComModbus.WordVaue[38] = array2[0];
			cdlMgr.tcpServerMgr.mComModbus.WordVaue[39] = array2[1];
			cdlMgr.tcpServerMgr.mComModbus.WordVaue[40] = array2[0];
			cdlMgr.tcpServerMgr.mComModbus.WordVaue[41] = array2[1];
			cdlMgr.tcpServerMgr.mComModbus.WordVaue[42] = array2[0];
			cdlMgr.tcpServerMgr.mComModbus.WordVaue[43] = array2[1];
			cdlMgr.tcpServerMgr.mComModbus.WordVaue[44] = array2[0];
			cdlMgr.tcpServerMgr.mComModbus.WordVaue[45] = array2[1];
			cdlMgr.tcpServerMgr.mComModbus.WordVaue[46] = array2[0];
			cdlMgr.tcpServerMgr.mComModbus.WordVaue[47] = array2[1];
			cdlMgr.tcpServerMgr.mComModbus.WordVaue[48] = array2[0];
			cdlMgr.tcpServerMgr.mComModbus.WordVaue[49] = array2[1];
			cdlMgr.tcpServerMgr.mComModbus.WordVaue[50] = array2[0];
			cdlMgr.tcpServerMgr.mComModbus.WordVaue[51] = array2[1];
			cdlMgr.tcpServerMgr.mComModbus.WordVaue[68] = array2[0];
			cdlMgr.tcpServerMgr.mComModbus.WordVaue[69] = array2[1];
			cdlMgr.tcpServerMgr.mComModbus.WordVaue[70] = array2[0];
			cdlMgr.tcpServerMgr.mComModbus.WordVaue[71] = array2[1];
			cdlMgr.tcpServerMgr.mComModbus.WordVaue[72] = array2[0];
			cdlMgr.tcpServerMgr.mComModbus.WordVaue[73] = array2[1];
			cdlMgr.tcpServerMgr.mComModbus.WordVaue[74] = array2[0];
			cdlMgr.tcpServerMgr.mComModbus.WordVaue[75] = array2[1];
			cdlMgr.tcpServerMgr.mComModbus.WordVaue[76] = array2[0];
			cdlMgr.tcpServerMgr.mComModbus.WordVaue[77] = array2[1];
			cdlMgr.tcpServerMgr.mComModbus.WordVaue[78] = array2[0];
			cdlMgr.tcpServerMgr.mComModbus.WordVaue[79] = array2[1];
			CaliGnl caliGnl2 = new CaliGnl();
			caliGnl2 = ((frmParam.kindMachine != 3) ? cdlMgr.ChartParaOperaList[1].mtdMgr.caliGnl : cdlMgr.ChartParaOperaList[0].mtdMgr.caliGnl);
			flagChannelOver2 = true;
			areaPlotParam = areaPlotParamMgr.GetAreaPlotParam(11);
			UnitName2 = areaPlotParam.UintName;
			CountAnalyse++;
			lbBTEX9.Text = "0";
			lbBTEX8.Text = "0";
			lbBTEX7.Text = "0";
			lbBTEX6.Text = "0";
			lbBTEX5.Text = "0";
			lbBTEX4.Text = "0";
			lbBTEX3.Text = "0";
			lbBTEX2.Text = "0";
			lbBTEX1.Text = "0";
			lbBTEX.Text = "0";
			array = new float[1];
			Buffer.BlockCopy(array, 0, array2, 0, 4);
			cdlMgr.tcpServerMgr.mComModbus.WordVaue[50] = array2[0];
			cdlMgr.tcpServerMgr.mComModbus.WordVaue[51] = array2[1];
			cdlMgr.tcpServerMgr.mComModbus.WordVaue[48] = array2[0];
			cdlMgr.tcpServerMgr.mComModbus.WordVaue[49] = array2[1];
			cdlMgr.tcpServerMgr.mComModbus.WordVaue[46] = array2[0];
			cdlMgr.tcpServerMgr.mComModbus.WordVaue[47] = array2[1];
			cdlMgr.tcpServerMgr.mComModbus.WordVaue[44] = array2[0];
			cdlMgr.tcpServerMgr.mComModbus.WordVaue[45] = array2[1];
			cdlMgr.tcpServerMgr.mComModbus.WordVaue[42] = array2[0];
			cdlMgr.tcpServerMgr.mComModbus.WordVaue[43] = array2[1];
			cdlMgr.tcpServerMgr.mComModbus.WordVaue[40] = array2[0];
			cdlMgr.tcpServerMgr.mComModbus.WordVaue[41] = array2[1];
			cdlMgr.tcpServerMgr.mComModbus.WordVaue[38] = array2[0];
			cdlMgr.tcpServerMgr.mComModbus.WordVaue[39] = array2[1];
			cdlMgr.tcpServerMgr.mComModbus.WordVaue[36] = array2[0];
			cdlMgr.tcpServerMgr.mComModbus.WordVaue[37] = array2[1];
			cdlMgr.tcpServerMgr.mComModbus.WordVaue[34] = array2[0];
			cdlMgr.tcpServerMgr.mComModbus.WordVaue[35] = array2[1];
			for (b = 0; b < caliGnl2.cmpds.Count(); b++)
			{
				num = 0;
				while (1 <= rltPeaks.Count() && num < rltPeaks.Count())
				{
					if (rltPeaks[num].pkRT >= caliGnl2.cmpds[b].cmpdInfo.retainTime - caliGnl2.cmpds[b].cmpdInfo.leftWindow && rltPeaks[num].pkRT <= caliGnl2.cmpds[b].cmpdInfo.retainTime + caliGnl2.cmpds[b].cmpdInfo.rightWindow && rltPeaks[num].height >= num2)
					{
						if (cdlMgr.formMain.IsAutoCalibra == 2)
						{
							b2++;
							caliGnl2.cmpds[b].levels[0].responseA = rltPeaks[num].area;
							caliGnl2.cmpds[b].levels[0].responseH = rltPeaks[num].height;
							caliGnl2.CalculateFunc(appendLink: false);
							if (caliGnl2.cmpds[b].cmpdInfo.respStyle == RespStyle.Area)
							{
								caliGnl2.cmpds[b].levels[0].respFactor = rltPeaks[num].GasAmount / rltPeaks[num].area;
							}
							else if (caliGnl2.cmpds[b].cmpdInfo.respStyle == RespStyle.Height)
							{
								caliGnl2.cmpds[b].levels[0].respFactor = rltPeaks[num].GasAmount / rltPeaks[num].height;
							}
							cdlMgr.ChartParaOperaList[1].mtdMgr.SaveToFile();
							cdlMgr.formMain.MainmstSet.ReadFromMtdMgr();
							array3[b] = (rltPeaks[num].amount = caliGnl2.cmpds[b].levels[0].respFactor * caliGnl2.cmpds[b].levels[0].respFactor);
							cdlMgr.ChartParaOperaList[1].mtdMgr.SaveToFile();
							cdlMgr.formMain.MainmstSet.UsePara();
							cdlMgr.formMain.MainmstSet.ReadFromMtdMgr();
							cdlMgr.formMain.MainmstSet.bUseSet_Click(null, null);
						}
						else if (bCalibration2)
						{
							b2++;
							cdlMgr.ChartParaOperaList[1].mtdMgr.caliGnl.cmpds[b].levels[0].responseA = rltPeaks[num].area;
							cdlMgr.ChartParaOperaList[1].mtdMgr.caliGnl.cmpds[b].levels[0].responseH = rltPeaks[num].height;
						}
						else
						{
							num4 += rltPeaks[num].amount;
							data2Array("苯系物", num4);
							data2Array(rltPeaks[num].name, rltPeaks[num].amount);
							array3[b] = rltPeaks[num].amount;
							array = new float[1] { array3[b] };
							Buffer.BlockCopy(array, 0, array2, 0, 4);
							array = new float[1] { rltPeaks[num].height };
							Buffer.BlockCopy(array, 0, array2, 0, 4);
							cdlMgr.tcpServerMgr.mComModbus.WordVaue[68 + num * 2] = array2[0];
							cdlMgr.tcpServerMgr.mComModbus.WordVaue[69 + num * 2] = array2[1];
							Class49.InsertIntoVoc(11 + b, 0, rltPeaks[num].name, fileName.ToLower(), rltPeaks[num].amount);
						}
					}
					num++;
				}
			}
			if (bCalibration2)
			{
				bCalibration2 = false;
				cdlMgr.ChartParaOperaList[1].mtdMgr.caliGnl.CalculateFunc(appendLink: false);
				cdlMgr.formMain.MainmstSet.CalcuFreeRespFactor(cdlMgr.ChartParaOperaList[1].mtdMgr.caliGnl);
				cdlMgr.ChartParaOperaList[1].mtdMgr.SaveToFile();
				cdlMgr.formMain.MainmstSet.ReadFromMtdMgr();
				cdlMgr.formMain.MainmstSet.bUseSet_Click(null, null);
			}
			lbBTEX9.Text = array3[8].ToString("0.00");
			lbBTEX8.Text = array3[7].ToString("0.00");
			lbBTEX7.Text = array3[6].ToString("0.00");
			lbBTEX6.Text = array3[5].ToString("0.00");
			lbBTEX5.Text = array3[4].ToString("0.00");
			lbBTEX4.Text = array3[3].ToString("0.00");
			lbBTEX3.Text = array3[2].ToString("0.00");
			lbBTEX2.Text = array3[1].ToString("0.00");
			lbBTEX1.Text = array3[0].ToString("0.00");
			if (frmParam.kindMachine == 2)
			{
				if (array3[0] > array3[1])
				{
					lbBTEX.Text = (array3[0] - array3[1]).ToString("0.00");
				}
				else
				{
					lbBTEX.Text = "0.00";
				}
			}
			else
			{
				lbBTEX.Text = (array3[0] + array3[1] + array3[2] + array3[3] + array3[4] + array3[5] + array3[6] + array3[7] + array3[8]).ToString("0.00");
			}
			array = new float[1] { array3[b] };
			array[0] = array3[0];
			Buffer.BlockCopy(array, 0, array2, 0, 4);
			cdlMgr.tcpServerMgr.mComModbus.WordVaue[34] = array2[0];
			cdlMgr.tcpServerMgr.mComModbus.WordVaue[35] = array2[1];
			array[0] = array3[1];
			Buffer.BlockCopy(array, 0, array2, 0, 4);
			cdlMgr.tcpServerMgr.mComModbus.WordVaue[36] = array2[0];
			cdlMgr.tcpServerMgr.mComModbus.WordVaue[37] = array2[1];
			array[0] = array3[2];
			Buffer.BlockCopy(array, 0, array2, 0, 4);
			cdlMgr.tcpServerMgr.mComModbus.WordVaue[38] = array2[0];
			cdlMgr.tcpServerMgr.mComModbus.WordVaue[39] = array2[1];
			array[0] = array3[3];
			Buffer.BlockCopy(array, 0, array2, 0, 4);
			cdlMgr.tcpServerMgr.mComModbus.WordVaue[40] = array2[0];
			cdlMgr.tcpServerMgr.mComModbus.WordVaue[41] = array2[1];
			array[0] = array3[4];
			Buffer.BlockCopy(array, 0, array2, 0, 4);
			cdlMgr.tcpServerMgr.mComModbus.WordVaue[42] = array2[0];
			cdlMgr.tcpServerMgr.mComModbus.WordVaue[43] = array2[1];
			array[0] = array3[5];
			Buffer.BlockCopy(array, 0, array2, 0, 4);
			cdlMgr.tcpServerMgr.mComModbus.WordVaue[44] = array2[0];
			cdlMgr.tcpServerMgr.mComModbus.WordVaue[45] = array2[1];
			array[0] = array3[6];
			Buffer.BlockCopy(array, 0, array2, 0, 4);
			cdlMgr.tcpServerMgr.mComModbus.WordVaue[46] = array2[0];
			cdlMgr.tcpServerMgr.mComModbus.WordVaue[47] = array2[1];
			array[0] = array3[7];
			Buffer.BlockCopy(array, 0, array2, 0, 4);
			cdlMgr.tcpServerMgr.mComModbus.WordVaue[48] = array2[0];
			cdlMgr.tcpServerMgr.mComModbus.WordVaue[49] = array2[1];
			array[0] = array3[8];
			Buffer.BlockCopy(array, 0, array2, 0, 4);
			cdlMgr.tcpServerMgr.mComModbus.WordVaue[50] = array2[0];
			cdlMgr.tcpServerMgr.mComModbus.WordVaue[51] = array2[1];
			ushort num5 = 0;
			num5 = (ushort)((array3[0] - frmParam.fmount44) / (frmParam.fmount204 - frmParam.fmount44) * 4095f);
			if (num5 > 4095)
			{
				num5 = 4095;
			}
			serialPoartBase.Data2[13] = (byte)(num5 >> 8);
			serialPoartBase.Data2[14] = (byte)num5;
			num5 = (ushort)((array3[1] - frmParam.fmount45) / (frmParam.fmount205 - frmParam.fmount45) * 4095f);
			if (num5 > 4095)
			{
				num5 = 4095;
			}
			serialPoartBase.Data2[15] = (byte)(num5 >> 8);
			serialPoartBase.Data2[16] = (byte)num5;
			num5 = (ushort)((array3[2] - frmParam.fmount46) / (frmParam.fmount206 - frmParam.fmount46) * 4095f);
			if (num5 > 4095)
			{
				num5 = 4095;
			}
			serialPoartBase.Data2[17] = (byte)(num5 >> 8);
			serialPoartBase.Data2[18] = (byte)num5;
			num5 = (ushort)((array3[3] - frmParam.fmount47) / (frmParam.fmount207 - frmParam.fmount47) * 4095f);
			if (num5 > 4095)
			{
				num5 = 4095;
			}
			serialPoartBase.Data2[19] = (byte)(num5 >> 8);
			serialPoartBase.Data2[20] = (byte)num5;
			num5 = (ushort)((array3[4] - frmParam.fmount48) / (frmParam.fmount208 - frmParam.fmount48) * 4095f);
			if (num5 > 4095)
			{
				num5 = 4095;
			}
			serialPoartBase.Data2[21] = (byte)(num5 >> 8);
			serialPoartBase.Data2[22] = (byte)num5;
			num5 = (ushort)((array3[5] - frmParam.fmount49) / (frmParam.fmount209 - frmParam.fmount49) * 4095f);
			if (num5 > 4095)
			{
				num5 = 4095;
			}
			serialPoartBase.Data2[23] = (byte)(num5 >> 8);
			serialPoartBase.Data2[24] = (byte)num5;
			num5 = (ushort)((array3[6] - frmParam.fmount410) / (frmParam.fmount2010 - frmParam.fmount410) * 4095f);
			if (num5 > 4095)
			{
				num5 = 4095;
			}
			serialPoartBase.Data2[25] = (byte)(num5 >> 8);
			serialPoartBase.Data2[26] = (byte)num5;
			num5 = (ushort)((array3[7] - frmParam.fmount411) / (frmParam.fmount2011 - frmParam.fmount411) * 4095f);
			if (num5 > 4095)
			{
				num5 = 4095;
			}
			serialPoartBase.Data2[27] = (byte)(num5 >> 8);
			serialPoartBase.Data2[28] = (byte)num5;
			num5 = (ushort)((array3[8] - frmParam.fmount412) / (frmParam.fmount2012 - frmParam.fmount412) * 4095f);
			if (num5 > 4095)
			{
				num5 = 4095;
			}
			serialPoartBase.Data2[29] = (byte)(num5 >> 8);
			serialPoartBase.Data2[30] = (byte)num5;
			Class49.InsertIntoVoc(20, 0, null, fileName.ToLower(), array3[0] + array3[1] + array3[2] + array3[3] + array3[4] + array3[5] + array3[6] + array3[7] + array3[8]);
			if (cdlMgr.formMain.IsAutoCalibra == 2 && b >= caliGnl2.cmpds.Count())
			{
				cdlMgr.formMain.IsAutoCalibra = 0;
				if (b2 >= caliGnl2.cmpds.Count())
				{
					MessageBox.Show("标定成功！");
				}
				else
				{
					MessageBox.Show("标定失败！");
				}
				btnCali.BackColor = btnFireOnSet.BackColor;
				cdlMgr.formMain.tabControl.SelectedIndex = 2;
				cdlMgr.formMain.chromFormCtrl.OpenChrom(fileName, sampling: false, useCurrent: true);
				cdlMgr.currentTcpServerMgrSendCmd(19);
				cdlMgr.formMain.tabChannel.Enabled = true;
				return;
			}
			break;
		}
		}
		if (flagChannelOver1)
		{
			flagChannelOver1 = false;
			Class49.InsertIntoVocTable(2, 0, strFileName, arrayData);
			for (int i = 0; i < 15; i++)
			{
				arrayData[i] = "0";
			}
		}
		if (flagChannelOver2)
		{
			flagChannelOver2 = false;
			Class49.InsertIntoVocTable(1, 0, strFileName2, arrayData);
			for (int j = 0; j < 15; j++)
			{
				arrayData[j] = "0";
			}
		}
		MethodInvoker method = delegate
		{
			FormVOC.fromVoc.lbTHC.Text = lbTHC.Text;
			FormVOC.fromVoc.lbCH4.Text = lbCH4.Text;
			FormVOC.fromVoc.lbNMHC.Text = lbNMHC.Text;
			FormVOC.fromVoc.lbBTEX.Text = lbBTEX.Text;
			FormVOC.fromVoc.lbNMHCT.Text = lbNMHCT.Text;
			FormVOC.fromVoc.lbBTEX9T.Text = lbBTEX9T.Text;
			FormVOC.fromVoc.lbBTEX8T.Text = lbBTEX8T.Text;
			FormVOC.fromVoc.lbBTEX7T.Text = lbBTEX7T.Text;
			FormVOC.fromVoc.lbBTEX6T.Text = lbBTEX6T.Text;
			FormVOC.fromVoc.lbBTEX5T.Text = lbBTEX5T.Text;
			FormVOC.fromVoc.lbBTEX4T.Text = lbBTEX4T.Text;
			FormVOC.fromVoc.lbBTEX3T.Text = lbBTEX3T.Text;
			FormVOC.fromVoc.lbBTEX2T.Text = lbBTEX2T.Text;
			FormVOC.fromVoc.lbBTEX1T.Text = lbBTEX1T.Text;
			FormVOC.fromVoc.lbBTEXt.Text = lbBTEXt.Text;
			if (frmParam.kindMachine == 4)
			{
				FormVOC.fromVoc.labAnaTimes.Text = cdlMgr.CurrentInsDeviceMgr.injectBotNum;
			}
		};
		Invoke(method);
		if (StateYiqi != 6)
		{
			StateYiqi = 5;
		}
		ushort[] array4 = new ushort[2];
		long[] src = new long[1] { CountAnalyse * 10 + StateYiqi };
		Buffer.BlockCopy(src, 0, array4, 0, 4);
		cdlMgr.tcpServerMgr.mComModbus.WordVaue[8] = array4[0];
		cdlMgr.tcpServerMgr.mComModbus.WordVaue[9] = array4[1];
		int num12 = 0;
		int num13 = 0;
		for (; num12 < 2000; num12++)
		{
			cdlMgr.tcpServerMgr.modBusData_0.ModBusBytes[num13++] = (byte)(cdlMgr.tcpServerMgr.mComModbus.WordVaue[num12] / 256);
			cdlMgr.tcpServerMgr.modBusData_0.ModBusBytes[num13++] = (byte)(cdlMgr.tcpServerMgr.mComModbus.WordVaue[num12] % 256);
			cdlMgr.tcpServerMgr.mComModbus2.WordVaue[num12] = cdlMgr.tcpServerMgr.mComModbus.WordVaue[num12];
		}
		cdlMgr.CurrentChartParaOpera.mtdMgr.caliGnl = caliGnl;
	}

	public void data2Array(string name, float amount)
	{
		switch (name)
		{
		case "总烃":
			arrayData[0] = amount.ToString("0.00000");
			fArrayData[0] = amount;
			break;
		case "甲烷":
			arrayData[1] = amount.ToString("0.00000");
			fArrayData[1] = amount;
			break;
		case "非甲烷总烃":
			arrayData[2] = amount.ToString("0.00000");
			fArrayData[2] = amount;
			break;
		case "苯":
			arrayData[3] = amount.ToString("0.00000");
			fArrayData[3] = amount;
			break;
		case "甲苯":
			arrayData[4] = amount.ToString("0.00000");
			fArrayData[4] = amount;
			break;
		case "间对二甲苯":
			arrayData[5] = amount.ToString("0.00000");
			fArrayData[5] = amount;
			break;
		case "邻二甲苯":
			arrayData[6] = amount.ToString("0.00000");
			fArrayData[6] = amount;
			break;
		case "乙苯":
			arrayData[7] = amount.ToString("0.00000");
			fArrayData[7] = amount;
			break;
		case "异丙苯":
			arrayData[8] = amount.ToString("0.00000");
			fArrayData[8] = amount;
			break;
		case "苯乙烯":
			arrayData[9] = amount.ToString("0.00000");
			fArrayData[9] = amount;
			break;
		case "苯系物":
			arrayData[10] = amount.ToString("0.00000");
			fArrayData[10] = amount;
			break;
		case "异丁烷":
			arrayData[11] = amount.ToString("0.00000");
			fArrayData[11] = amount;
			break;
		case "正丁烷":
			arrayData[12] = amount.ToString("0.00000");
			fArrayData[12] = amount;
			break;
		case "二氧化硫":
			arrayData[13] = amount.ToString("0.00000");
			fArrayData[13] = amount;
			break;
		case "氢气":
			arrayData[14] = amount.ToString("0.00000");
			fArrayData[14] = amount;
			break;
		}
	}

	private void SetVocPeakUnitName(int i, int typdID, float Threshold, Peak[] peak, CaliGnl caliGnl, string fileName)
	{
		AreaPlotParamMgr areaPlotParamMgr = AreaPlotParamMgr.Create();
		AreaPlotParam areaPlotParam = areaPlotParamMgr.GetAreaPlotParam(typdID);
		LogMgr.Instance.Write2RunLog("FormMainVOC.SetVocPeakUnitName begin");
		Type type = GetType();
		int num = 0;
		float num2 = 0f;
		float[] array = new float[1];
		ushort[] array2 = new ushort[2];
		num = 0;
		while (1 <= peak.Count() && num < peak.Count())
		{
			if (!(peak[num].height < Threshold))
			{
				if (caliGnl.cmpds.Length < i + 1)
				{
					LogMgr.Instance.Write2RunLog("FormMainVOC.disposeVOCPeaks  Error:caliGnl.cmpds.Length=" + caliGnl.cmpds.Length + " i:" + i);
					break;
				}
				if (peak[num].pkRT >= caliGnl.cmpds[i].cmpdInfo.retainTime - caliGnl.cmpds[i].cmpdInfo.leftWindow && peak[num].pkRT <= caliGnl.cmpds[i].cmpdInfo.retainTime + caliGnl.cmpds[i].cmpdInfo.rightWindow)
				{
					num2 = peak[num].amount;
					string uintName = areaPlotParam.UintName;
					string peakName = areaPlotParam.PeakName;
					FieldInfo field = type.GetField("UnitName" + typdID);
					FieldInfo field2 = type.GetField("peakName" + typdID);
					string text = "ppm";
					switch (peakName)
					{
					case "苯":
						switch (uintName)
						{
						case "ppm":
							text = "ppm";
							break;
						case "mg/m³":
							text = "mg/m³";
							break;
						case "ppm转mg/m³":
							num2 = 78.11f * peak[num].amount / 22.4f;
							text = "mg/m³";
							break;
						case "mg/m³转ppm":
							num2 = peak[num].amount * 22.4f / 78.11f;
							text = "ppm";
							break;
						}
						break;
					case "甲苯":
						switch (uintName)
						{
						case "ppm":
							text = "ppm";
							break;
						case "mg/m³":
							text = "mg/m³";
							break;
						case "ppm转mg/m³":
							num2 = 92.14f * peak[num].amount / 22.4f;
							text = "mg/m³";
							break;
						case "mg/m³转ppm":
							num2 = peak[num].amount * 22.4f / 92.14f;
							text = "ppm";
							break;
						}
						break;
					case "间对二甲苯":
						switch (uintName)
						{
						case "ppm":
							text = "ppm";
							break;
						case "mg/m³":
							text = "mg/m³";
							break;
						case "ppm转mg/m³":
							num2 = 106.17f * peak[num].amount / 22.4f;
							text = "mg/m³";
							break;
						case "mg/m³转ppm":
							num2 = peak[num].amount * 22.4f / 106.17f;
							text = "ppm";
							break;
						}
						break;
					case "邻二甲苯":
						switch (uintName)
						{
						case "ppm":
							text = "ppm";
							break;
						case "mg/m³":
							text = "mg/m³";
							break;
						case "ppm转mg/m³":
							num2 = 106.17f * peak[num].amount / 22.4f;
							text = "mg/m³";
							break;
						case "mg/m³转ppm":
							num2 = peak[num].amount * 22.4f / 106.17f;
							text = "ppm";
							break;
						}
						break;
					case "乙苯":
						switch (uintName)
						{
						case "ppm":
							text = "ppm";
							break;
						case "mg/m³":
							text = "mg/m³";
							break;
						case "ppm转mg/m³":
							num2 = 106.16f * peak[num].amount / 22.4f;
							text = "mg/m³";
							break;
						case "mg/m³转ppm":
							num2 = peak[num].amount * 22.4f / 106.16f;
							text = "ppm";
							break;
						}
						break;
					case "异丙苯":
						switch (uintName)
						{
						case "ppm":
							text = "ppm";
							break;
						case "mg/m³":
							text = "mg/m³";
							break;
						case "ppm转mg/m³":
							num2 = 120.19f * peak[num].amount / 22.4f;
							text = "mg/m³";
							break;
						case "mg/m³转ppm":
							num2 = peak[num].amount * 22.4f / 120.19f;
							text = "ppm";
							break;
						}
						break;
					case "苯乙烯":
						switch (uintName)
						{
						case "ppm":
							text = "ppm";
							break;
						case "mg/m³":
							text = "mg/m³";
							break;
						case "ppm转mg/m³":
							num2 = 104.15f * peak[num].amount / 22.4f;
							text = "mg/m³";
							break;
						case "mg/m³转ppm":
							num2 = peak[num].amount * 22.4f / 104.15f;
							text = "ppm";
							break;
						}
						break;
					default:
						switch (uintName)
						{
						case "ppm":
							text = "ppm";
							break;
						case "mg/m³":
							text = "mg/m³";
							break;
						case "ppm转mg/m³":
							text = "mg/m³";
							break;
						case "mg/m³转ppm":
							text = "ppm";
							break;
						}
						break;
					}
					string value = num2.ToString("0.000") + text;
					FieldInfo field3 = type.GetField("lbBTEX" + (i + 1));
					field3.SetValue(this, value);
					array = new float[1] { num2 };
					Buffer.BlockCopy(array, 0, array2, 0, 4);
					cdlMgr.tcpServerMgr.mComModbus.WordVaue[34 + i] = array2[0];
					cdlMgr.tcpServerMgr.mComModbus.WordVaue[35 + i] = array2[1];
					Class49.InsertIntoVoc(typdID, 0, peak[num].name, fileName.ToLower(), num2);
					break;
				}
			}
			num++;
		}
		if (num >= peak.Count())
		{
			array = new float[1];
			Buffer.BlockCopy(array, 0, array2, 0, 4);
			cdlMgr.tcpServerMgr.mComModbus.WordVaue[34 + i] = array2[0];
			cdlMgr.tcpServerMgr.mComModbus.WordVaue[35 + i] = array2[1];
			LogMgr.Instance.Write2RunLog("FormMainVOC.SetVocPeakUnitName   没有找到组分值对应的峰");
			if (peak.Length != 0)
			{
				Class49.InsertIntoVoc(typdID, 0, peak[peak.Length - 1].name, fileName.ToLower(), 0f);
			}
		}
		LogMgr.Instance.Write2RunLog("FormMainVOC.SetVocPeakUnitName   end");
	}

	private void button36_Click(object sender, EventArgs e)
	{
		if (FormVOC.fromVoc != null)
		{
			FormVOC.fromVoc.Show();
			FormVOC.fromVoc.Activate();
			FormVOC.fromVoc.WindowState = FormWindowState.Maximized;
			base.ParentForm.Hide();
		}
	}

	private void btShowDesktop_Click(object sender, EventArgs e)
	{
		if (FormVOC.fromVoc != null)
		{
			FormVOC.fromVoc.WindowState = FormWindowState.Minimized;
		}
		base.ParentForm.WindowState = FormWindowState.Minimized;
	}

	private void btnFireOnCheck_Click(object sender, EventArgs e)
	{
		if (Class49.user_0.ULevel == User.Level.管理员)
		{
			cdlMgr.currentTcpServerMgrSendCmd(250);
			return;
		}
		MessageBox.Show(Lang.PS("没有设置仪器权限！", "Without permission!"));
	}

	private void btnFireOnSet_Click(object sender, EventArgs e)
	{
		if (Class49.user_0.ULevel == User.Level.管理员)
		{
			cdlMgr.currentTcpServerMgrSendCmd(249);
			frmParam.fFireOn = ToFloat(tbFireOn.Text);
			frmParam.fFireOn2 = ToFloat(tbFireOn2.Text);
			frmParam.SaveParam();
		}
		else
		{
			MessageBox.Show(Lang.PS("没有设置仪器权限！", "Without permission!"));
		}
	}

	private int ToInt(string str)
	{
		int result = 0;
		int.TryParse(str, out result);
		return result;
	}

	private float ToFloat(string str)
	{
		float result = 0f;
		float.TryParse(str, out result);
		return result;
	}

	public void openSoftKey()
	{
		try
		{
			if (!File.Exists("C:\\Windows\\System32\\osk.exe"))
			{
				MessageBox.Show("软键盘可执行文件不存在！");
				return;
			}
			softKey = Process.Start("C:\\Windows\\System32\\osk.exe");
			IntPtr intPtr = IntPtr.Zero;
			while (IntPtr.Zero == intPtr)
			{
				Thread.Sleep(100);
				intPtr = FindWindow(null, "屏幕键盘");
			}
			int num = Screen.PrimaryScreen.Bounds.Width;
			int num2 = Screen.PrimaryScreen.Bounds.Height;
			int num3 = (num - 1000) / 2;
			int num4 = num2 - 300;
			MoveWindow(intPtr, num3, num4, 1000, 300, bRepaint: true);
			SetForegroundWindow(intPtr);
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message);
		}
	}

	private void btnCali_Click(object sender, EventArgs e)
	{
		if (cdlMgr.formMain.IsAutoCalibra != 0)
		{
			return;
		}
		Form owner = FormMain.fromMain;
		if (FIDSet.myself != null && FIDSet.myself.Visible)
		{
			owner = FIDSet.myself;
		}
		if (MessageBox.Show(owner, Lang.PS("请确认是否接入了标气", "Please confirm whether the standard gas is connected"), Lang.PS("提示：", "reminder:"), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
		{
			return;
		}
		if (cdlMgr.formMain.tabChannel.TabCount > 1)
		{
			if (cdlMgr.CurrentTcpServerSocket != null && (cdlMgr.CurrentTcpServerSocket.sglsSampling[0].simple || cdlMgr.CurrentTcpServerSocket.sglsSampling[1].simple))
			{
				MessageBox.Show(Lang.PS("样品正在采集，请等待样品结束！", "Samples are being collected, please wait for the end of samples!"));
				return;
			}
		}
		else if (cdlMgr.formMain.tabChannel.TabCount == 1 && cdlMgr.CurrentTcpServerSocket != null && cdlMgr.CurrentTcpServerSocket.sglsSampling[0].simple)
		{
			MessageBox.Show(Lang.PS("样品正在采集，请等待样品结束！", "Samples are being collected, please wait for the end of samples!"));
			return;
		}
		FormCalibra formCalibra = new FormCalibra();
		formCalibra.StartPosition = FormStartPosition.CenterScreen;
		formCalibra.Show();
		formCalibra.TopMost = true;
		formCalibra.Activate();
		formCalibra.BringToFront();
		formCalibra.Focus();
	}

	private void button1_Click(object sender, EventArgs e)
	{
		float responseA = cdlMgr.ChartParaOperaList[0].mtdMgr.caliGnl.cmpds[0].levels[0].responseA;
		float responseA2 = cdlMgr.ChartParaOperaList[1].mtdMgr.caliGnl.cmpds[0].levels[0].responseA;
		MessageBox.Show(responseA.ToString() + responseA2);
		cdlMgr.ChartParaOperaList[0].mtdMgr.caliGnl.cmpds[0].levels[0].responseA = 10f;
		cdlMgr.ChartParaOperaList[1].mtdMgr.caliGnl.cmpds[0].levels[0].responseA = 11f;
		cdlMgr.ChartParaOperaList[0].mtdMgr.SaveToFile();
		cdlMgr.ChartParaOperaList[1].mtdMgr.SaveToFile();
	}

	private void button2_Click(object sender, EventArgs e)
	{
		float responseA = cdlMgr.ChartParaOperaList[0].mtdMgr.caliGnl.cmpds[0].levels[0].responseA;
		MessageBox.Show(responseA.ToString());
	}

	private void lbTHC_Click(object sender, EventArgs e)
	{
		FormAreaPlot formAreaPlot = new FormAreaPlot(1);
		formAreaPlot.StartPosition = FormStartPosition.CenterScreen;
		formAreaPlot.Show();
		formAreaPlot.loadData();
	}

	private void lbCH4_Click(object sender, EventArgs e)
	{
		FormAreaPlot formAreaPlot = new FormAreaPlot(2);
		formAreaPlot.StartPosition = FormStartPosition.CenterScreen;
		formAreaPlot.Show();
		formAreaPlot.loadData();
	}

	private void lbNMHC_Click(object sender, EventArgs e)
	{
		FormAreaPlot formAreaPlot = new FormAreaPlot(3);
		formAreaPlot.StartPosition = FormStartPosition.CenterScreen;
		formAreaPlot.Show();
		formAreaPlot.loadData();
	}

	private void lbBTEX1_Click(object sender, EventArgs e)
	{
		FormAreaPlot formAreaPlot = new FormAreaPlot(11);
		formAreaPlot.StartPosition = FormStartPosition.CenterScreen;
		formAreaPlot.Show();
		formAreaPlot.loadData();
	}

	private void lbBTEX2_Click(object sender, EventArgs e)
	{
		FormAreaPlot formAreaPlot = new FormAreaPlot(12);
		formAreaPlot.StartPosition = FormStartPosition.CenterScreen;
		formAreaPlot.Show();
		formAreaPlot.loadData();
	}

	private void lbBTEX3_Click(object sender, EventArgs e)
	{
		FormAreaPlot formAreaPlot = new FormAreaPlot(13);
		formAreaPlot.StartPosition = FormStartPosition.CenterScreen;
		formAreaPlot.Show();
		formAreaPlot.loadData();
	}

	private void lbBTEX4_Click(object sender, EventArgs e)
	{
		FormAreaPlot formAreaPlot = new FormAreaPlot(14);
		formAreaPlot.StartPosition = FormStartPosition.CenterScreen;
		formAreaPlot.Show();
		formAreaPlot.loadData();
	}

	private void lbBTEX5_Click(object sender, EventArgs e)
	{
		FormAreaPlot formAreaPlot = new FormAreaPlot(15);
		formAreaPlot.StartPosition = FormStartPosition.CenterScreen;
		formAreaPlot.Show();
		formAreaPlot.loadData();
	}

	private void lbBTEX6_Click(object sender, EventArgs e)
	{
		FormAreaPlot formAreaPlot = new FormAreaPlot(16);
		formAreaPlot.StartPosition = FormStartPosition.CenterScreen;
		formAreaPlot.Show();
		formAreaPlot.loadData();
	}

	private void lbBTEX7_Click(object sender, EventArgs e)
	{
		FormAreaPlot formAreaPlot = new FormAreaPlot(17);
		formAreaPlot.StartPosition = FormStartPosition.CenterScreen;
		formAreaPlot.Show();
		formAreaPlot.loadData();
	}

	private void lbBTEX8_Click(object sender, EventArgs e)
	{
		FormAreaPlot formAreaPlot = new FormAreaPlot(18);
		formAreaPlot.StartPosition = FormStartPosition.CenterScreen;
		formAreaPlot.Show();
		formAreaPlot.loadData();
	}

	private void lbBTEX9_Click(object sender, EventArgs e)
	{
		FormAreaPlot formAreaPlot = new FormAreaPlot(19);
		formAreaPlot.StartPosition = FormStartPosition.CenterScreen;
		formAreaPlot.Show();
		formAreaPlot.loadData();
	}

	private void lbBTEX_Click(object sender, EventArgs e)
	{
		FormAreaPlot formAreaPlot = new FormAreaPlot(20);
		formAreaPlot.StartPosition = FormStartPosition.CenterScreen;
		formAreaPlot.Show();
		formAreaPlot.loadData();
	}

	private void cbKindMachine_SelectedIndexChanged(object sender, EventArgs e)
	{
		frmParam.kindMachine = cbKindMachine.SelectedIndex;
		frmParam.SaveParam();
		if (frmParam.kindMachine == 0)
		{
			gbNHMC1.Visible = true;
			gbBenXW.Visible = true;
			formVoc.gbNHMC1.Visible = true;
			formVoc.gbBenXW.Visible = true;
			formVoc.sctSetting.Panel2Collapsed = false;
		}
		else if (frmParam.kindMachine == 1)
		{
			gbNHMC1.Visible = true;
			gbBenXW.Visible = false;
			formVoc.gbNHMC1.Visible = true;
			formVoc.gbBenXW.Visible = false;
			formVoc.sctSetting.Panel2Collapsed = true;
		}
	}

	private void btnNetConfig_Click(object sender, EventArgs e)
	{
		NetSetForm netSetForm = new NetSetForm();
		netSetForm.StartPosition = FormStartPosition.CenterScreen;
		netSetForm.TopMost = true;
		netSetForm.Show();
	}

	private void lbBTEX9T_Click(object sender, EventArgs e)
	{
	}

	private void lbBTEX8T_Click(object sender, EventArgs e)
	{
	}

	private void lbBTEX7T_Click(object sender, EventArgs e)
	{
	}

	private void lbBTEX6T_Click(object sender, EventArgs e)
	{
	}

	private void lbBTEX5T_Click(object sender, EventArgs e)
	{
	}

	public void updateBxw()
	{
	}

	private void btnDevice_Click(object sender, EventArgs e)
	{
		FrmChromatManager frmChromatManager = new FrmChromatManager();
		frmChromatManager.Show();
	}

	private void BtnSoftKey_Click(object sender, EventArgs e)
	{
		Random random = new Random();
		for (int i = 0; i < 15; i++)
		{
			arrayData[i] = random.Next(0, 100).ToString();
		}
		Class49.InsertIntoVocTable(0, 0, "sds", arrayData);
	}

	private void cbCOM2_SelectedIndexChanged(object sender, EventArgs e)
	{
		if (!m_bLoading)
		{
			frmParam.iCom2 = cbCOM2.SelectedIndex;
			frmParam.SaveParam();
			MessageBox.Show("更改重启后生效！");
		}
	}

	public void selectCom2Fun()
	{
		cbCOM2.SelectedIndex = frmParam.iCom2;
		if (frmParam.iCom2 == 0)
		{
			try
			{
				cdlMgr.tcpServerMgr.mComModbus2.CloseMyCom();
				serialPoartBase.openPort();
				return;
			}
			catch
			{
				return;
			}
		}
		if (frmParam.iCom2 == 0)
		{
			serialPoartBase.closePort();
			cdlMgr.tcpServerMgr.mComModbus2.OpenMyCom("COM2", 9600, 8, Parity.None, StopBits.One);
		}
	}

	private void timer1_Tick(object sender, EventArgs e)
	{
		if (frmParam.kindMachine == 4)
		{
			Label label = formVoc.lbBTEX1T;
			string text = (lbBTEX1T.Text = strCompName[2]);
			label.Text = text;
			Label label2 = formVoc.lbBTEX2T;
			text = (lbBTEX2T.Text = strCompName[3]);
			label2.Text = text;
			Label label3 = formVoc.lbBTEX3T;
			text = (lbBTEX3T.Text = strCompName[4]);
			label3.Text = text;
			Label label4 = formVoc.lbBTEX4T;
			text = (lbBTEX4T.Text = strCompName[5]);
			label4.Text = text;
			Label label5 = formVoc.lbBTEX5T;
			text = (lbBTEX5T.Text = strCompName[6]);
			label5.Text = text;
			Label label6 = formVoc.lbBTEX6T;
			text = (lbBTEX6T.Text = strCompName[7]);
			label6.Text = text;
			Label label7 = formVoc.lbBTEX7T;
			text = (lbBTEX7T.Text = strCompName[8]);
			label7.Text = text;
			Label label8 = formVoc.lbBTEX8T;
			text = (lbBTEX8T.Text = strCompName[9]);
			label8.Text = text;
			Label label9 = formVoc.lbBTEX9T;
			text = (lbBTEX9T.Text = strCompName[10]);
			label9.Text = text;
		}
		else
		{
			Label label10 = formVoc.lbBTEX1T;
			string text = (lbBTEX1T.Text = strCompName[0]);
			label10.Text = text;
			Label label11 = formVoc.lbBTEX2T;
			text = (lbBTEX2T.Text = strCompName[1]);
			label11.Text = text;
			Label label12 = formVoc.lbBTEX3T;
			text = (lbBTEX3T.Text = strCompName[2]);
			label12.Text = text;
			Label label13 = formVoc.lbBTEX4T;
			text = (lbBTEX4T.Text = strCompName[3]);
			label13.Text = text;
			Label label14 = formVoc.lbBTEX5T;
			text = (lbBTEX5T.Text = strCompName[4]);
			label14.Text = text;
			Label label15 = formVoc.lbBTEX6T;
			text = (lbBTEX6T.Text = strCompName[5]);
			label15.Text = text;
			Label label16 = formVoc.lbBTEX7T;
			text = (lbBTEX7T.Text = strCompName[6]);
			label16.Text = text;
			Label label17 = formVoc.lbBTEX8T;
			text = (lbBTEX8T.Text = strCompName[7]);
			label17.Text = text;
			Label label18 = formVoc.lbBTEX9T;
			text = (lbBTEX9T.Text = strCompName[8]);
			label18.Text = text;
			labTimes1.Text = Lang.PS("通道1循环 ", "loops 1 ") + cdlMgr.formMain.cntAnalyze1 + Lang.PS(" 次 ", " times ") + cdlMgr.formMain.cntChannel1 + " s";
			labTimes2.Text = Lang.PS("通道2循环 ", "loops 2 ") + cdlMgr.formMain.cntAnalyze2 + Lang.PS(" 次 ", " times ") + cdlMgr.formMain.cntChannel2 + " s";
			formVoc.labAnaTimes.Text = cdlMgr.formMain.cntAnalyze1 + " " + cdlMgr.formMain.cntAnalyze2;
		}
		if (lbBTEX1T.Text == "")
		{
			Label label19 = formVoc.lbBTEX1;
			string text = (lbBTEX1.Text = "");
			label19.Text = text;
		}
		else
		{
			formVoc.lbBTEX1.Text = lbBTEX1.Text;
		}
		if (lbBTEX2T.Text == "")
		{
			Label label20 = formVoc.lbBTEX2;
			string text = (lbBTEX2.Text = "");
			label20.Text = text;
		}
		else
		{
			formVoc.lbBTEX2.Text = lbBTEX2.Text;
		}
		if (lbBTEX3T.Text == "")
		{
			Label label21 = formVoc.lbBTEX3;
			string text = (lbBTEX3.Text = "");
			label21.Text = text;
		}
		else
		{
			formVoc.lbBTEX3.Text = lbBTEX3.Text;
		}
		if (lbBTEX4T.Text == "")
		{
			Label label22 = formVoc.lbBTEX4;
			string text = (lbBTEX4.Text = "");
			label22.Text = text;
		}
		else
		{
			formVoc.lbBTEX4.Text = lbBTEX4.Text;
		}
		if (lbBTEX5T.Text == "")
		{
			Label label23 = formVoc.lbBTEX5;
			string text = (lbBTEX5.Text = "");
			label23.Text = text;
		}
		else
		{
			formVoc.lbBTEX5.Text = lbBTEX5.Text;
		}
		if (lbBTEX6T.Text == "")
		{
			Label label24 = formVoc.lbBTEX6;
			string text = (lbBTEX6.Text = "");
			label24.Text = text;
		}
		else
		{
			formVoc.lbBTEX6.Text = lbBTEX6.Text;
		}
		if (lbBTEX7T.Text == "")
		{
			Label label25 = formVoc.lbBTEX7;
			string text = (lbBTEX7.Text = "");
			label25.Text = text;
		}
		else
		{
			formVoc.lbBTEX7.Text = lbBTEX7.Text;
		}
		if (lbBTEX8T.Text == "")
		{
			Label label26 = formVoc.lbBTEX8;
			string text = (lbBTEX8.Text = "");
			label26.Text = text;
		}
		else
		{
			formVoc.lbBTEX8.Text = lbBTEX8.Text;
		}
		if (lbBTEX9T.Text == "")
		{
			Label label27 = formVoc.lbBTEX9;
			string text = (lbBTEX9.Text = "");
			label27.Text = text;
		}
		else
		{
			formVoc.lbBTEX9.Text = lbBTEX9.Text;
		}
	}

	private void btnHistory_Click(object sender, EventArgs e)
	{
		FormHistory formHistory = new FormHistory();
		formHistory.StartPosition = FormStartPosition.CenterScreen;
		formHistory.Show();
		formHistory.loadData();
	}

	private void btnSetPassword_Click(object sender, EventArgs e)
	{
		FormSetPassword formSetPassword = new FormSetPassword();
		formSetPassword.StartPosition = FormStartPosition.CenterScreen;
		formSetPassword.Show();
	}

	private void sdaOpen_Click(object sender, EventArgs e)
	{
		OpenFileDialog openFileDialog = new OpenFileDialog();
		openFileDialog.Title = Lang.PS("打开谱图", "open sdafile");
		openFileDialog.InitialDirectory = sysParam.strSdaDataFileDir;
		openFileDialog.Filter = Lang.PS("谱图文件") + "(*.sda)|*.sda";
		if (openFileDialog.ShowDialog() != DialogResult.OK)
		{
			return;
		}
		string fileName = openFileDialog.FileName;
		tbSdaFileName.Text = Path.GetFileName(fileName);
		sysParam.strSdaDataFileDir = Path.GetDirectoryName(fileName);
		sysParam.SaveParam();
		frmParam.strSdaFile = fileName;
		Chromatogram chromatogram = Chromatogram.LoadFromFile2(fileName, DetectorStyle.General);
		if (chromatogram != null)
		{
			Peak[] peakAllCompound = chromatogram.GetPeakAllCompound();
			if (peakAllCompound != null && peakAllCompound.Length != 0)
			{
				frmParam.fValueO = peakAllCompound[0].amount;
				labO.Text = frmParam.fValueO.ToString("F" + Class49.int_8);
			}
			frmParam.SaveParam();
		}
	}

	private void chbO2_CheckedChanged(object sender, EventArgs e)
	{
		if (!m_bLoading)
		{
			frmParam.bO = chbO2.Checked;
			frmParam.SaveParam();
		}
	}

	private void tbTimeCycle1_TextChanged(object sender, EventArgs e)
	{
		if (!m_bLoading)
		{
			float.TryParse(tbTimeCycle1.Text, out frmParam.fTabChannel1);
			frmParam.SaveParam();
		}
	}

	private void tbTimeCycle2_TextChanged(object sender, EventArgs e)
	{
		if (!m_bLoading)
		{
			float.TryParse(tbTimeCycle2.Text, out frmParam.fTabChannel2);
			frmParam.SaveParam();
		}
	}

	private void chbCycle_CheckedChanged(object sender, EventArgs e)
	{
		if (!m_bLoading)
		{
			frmParam.bCycle = chbCycle.Checked;
			frmParam.SaveParam();
		}
	}

	private void chbCycle2_CheckedChanged(object sender, EventArgs e)
	{
		if (!m_bLoading)
		{
			frmParam.bCycle2 = chbCycle2.Checked;
			frmParam.SaveParam();
		}
	}

	private void tbPowerOnDelay_TextChanged(object sender, EventArgs e)
	{
		if (!m_bLoading)
		{
			float.TryParse(tbPowerOnDelay.Text, out frmParam.fPowerOnDealy);
			frmParam.SaveParam();
		}
	}

	private void tbTimesCycle1_TextChanged(object sender, EventArgs e)
	{
		if (!m_bLoading)
		{
			int.TryParse(tbTimesCycle1.Text, out frmParam.iTimesCycle1);
			frmParam.SaveParam();
		}
	}

	private void tbTimesCycle2_TextChanged(object sender, EventArgs e)
	{
		if (!m_bLoading)
		{
			int.TryParse(tbTimesCycle2.Text, out frmParam.iTimesCycle2);
			frmParam.SaveParam();
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
		this.btShowDesktop = new System.Windows.Forms.Button();
		this.button36 = new System.Windows.Forms.Button();
		this.lbBTEX9 = new System.Windows.Forms.Label();
		this.lbBTEX9T = new System.Windows.Forms.Label();
		this.lbBTEX8 = new System.Windows.Forms.Label();
		this.lbBTEX8T = new System.Windows.Forms.Label();
		this.lbNMHC = new System.Windows.Forms.Label();
		this.lbBTEX = new System.Windows.Forms.Label();
		this.lbNMHCT = new System.Windows.Forms.Label();
		this.lbBTEXt = new System.Windows.Forms.Label();
		this.lbBTEX7 = new System.Windows.Forms.Label();
		this.lbBTEX7T = new System.Windows.Forms.Label();
		this.lbBTEX5 = new System.Windows.Forms.Label();
		this.lbBTEX6 = new System.Windows.Forms.Label();
		this.lbBTEX4 = new System.Windows.Forms.Label();
		this.lbBTEX5T = new System.Windows.Forms.Label();
		this.lbBTEX6T = new System.Windows.Forms.Label();
		this.lbBTEX4T = new System.Windows.Forms.Label();
		this.lbBTEX2 = new System.Windows.Forms.Label();
		this.lbBTEX3 = new System.Windows.Forms.Label();
		this.lbBTEX1 = new System.Windows.Forms.Label();
		this.lbBTEX2T = new System.Windows.Forms.Label();
		this.lbBTEX3T = new System.Windows.Forms.Label();
		this.lbBTEX1T = new System.Windows.Forms.Label();
		this.lbCH4 = new System.Windows.Forms.Label();
		this.label79 = new System.Windows.Forms.Label();
		this.lbTHC = new System.Windows.Forms.Label();
		this.label77 = new System.Windows.Forms.Label();
		this.tbFireOn2 = new System.Windows.Forms.TextBox();
		this.btnFireOnSet = new System.Windows.Forms.Button();
		this.btnFireOnCheck = new System.Windows.Forms.Button();
		this.label80 = new System.Windows.Forms.Label();
		this.tbFireOn = new System.Windows.Forms.TextBox();
		this.gbBenXW = new System.Windows.Forms.GroupBox();
		this.button2 = new System.Windows.Forms.Button();
		this.button1 = new System.Windows.Forms.Button();
		this.btnSoftKey = new System.Windows.Forms.Button();
		this.panel1 = new System.Windows.Forms.Panel();
		this.btnDevice = new System.Windows.Forms.Button();
		this.btnNetConfig = new System.Windows.Forms.Button();
		this.cbKindMachine = new System.Windows.Forms.ComboBox();
		this.btnCali = new System.Windows.Forms.Button();
		this.gbNHMC1 = new System.Windows.Forms.GroupBox();
		this.cbCOM2 = new System.Windows.Forms.ComboBox();
		this.label1 = new System.Windows.Forms.Label();
		this.timer1 = new System.Windows.Forms.Timer(this.components);
		this.btnHistory = new System.Windows.Forms.Button();
		this.btnSetPassword = new System.Windows.Forms.Button();
		this.tabControl1 = new System.Windows.Forms.TabControl();
		this.tabPage1 = new System.Windows.Forms.TabPage();
		this.tabPage2 = new System.Windows.Forms.TabPage();
		this.sdaOpen = new System.Windows.Forms.Button();
		this.tbSdaFileName = new System.Windows.Forms.TextBox();
		this.labO = new System.Windows.Forms.Label();
		this.label3 = new System.Windows.Forms.Label();
		this.label2 = new System.Windows.Forms.Label();
		this.chbO2 = new System.Windows.Forms.CheckBox();
		this.tabPage3 = new System.Windows.Forms.TabPage();
		this.panel2 = new System.Windows.Forms.Panel();
		this.chbCycle = new System.Windows.Forms.CheckBox();
		this.label11 = new System.Windows.Forms.Label();
		this.label10 = new System.Windows.Forms.Label();
		this.label4 = new System.Windows.Forms.Label();
		this.label15 = new System.Windows.Forms.Label();
		this.tbTimeCycle1 = new System.Windows.Forms.TextBox();
		this.label14 = new System.Windows.Forms.Label();
		this.label5 = new System.Windows.Forms.Label();
		this.labTimes2 = new System.Windows.Forms.Label();
		this.chbCycle2 = new System.Windows.Forms.CheckBox();
		this.tbTimeCycle2 = new System.Windows.Forms.TextBox();
		this.labTimes1 = new System.Windows.Forms.Label();
		this.tbPowerOnDelay = new System.Windows.Forms.TextBox();
		this.tbTimesCycle1 = new System.Windows.Forms.TextBox();
		this.label13 = new System.Windows.Forms.Label();
		this.label7 = new System.Windows.Forms.Label();
		this.label12 = new System.Windows.Forms.Label();
		this.label6 = new System.Windows.Forms.Label();
		this.tbTimesCycle2 = new System.Windows.Forms.TextBox();
		this.gbBenXW.SuspendLayout();
		this.panel1.SuspendLayout();
		this.gbNHMC1.SuspendLayout();
		this.tabControl1.SuspendLayout();
		this.tabPage1.SuspendLayout();
		this.tabPage2.SuspendLayout();
		this.tabPage3.SuspendLayout();
		this.panel2.SuspendLayout();
		base.SuspendLayout();
		this.btShowDesktop.Location = new System.Drawing.Point(126, 2);
		this.btShowDesktop.Name = "btShowDesktop";
		this.btShowDesktop.Size = new System.Drawing.Size(63, 23);
		this.btShowDesktop.TabIndex = 85;
		this.btShowDesktop.Text = "显示桌面";
		this.btShowDesktop.UseVisualStyleBackColor = true;
		this.btShowDesktop.Click += new System.EventHandler(btShowDesktop_Click);
		this.button36.Location = new System.Drawing.Point(62, 2);
		this.button36.Name = "button36";
		this.button36.Size = new System.Drawing.Size(63, 23);
		this.button36.TabIndex = 84;
		this.button36.Text = "监控界面";
		this.button36.UseVisualStyleBackColor = true;
		this.button36.Click += new System.EventHandler(button36_Click);
		this.lbBTEX9.AutoSize = true;
		this.lbBTEX9.Cursor = System.Windows.Forms.Cursors.Hand;
		this.lbBTEX9.Location = new System.Drawing.Point(357, 52);
		this.lbBTEX9.Name = "lbBTEX9";
		this.lbBTEX9.Size = new System.Drawing.Size(11, 12);
		this.lbBTEX9.TabIndex = 83;
		this.lbBTEX9.Text = "0";
		this.lbBTEX9.Click += new System.EventHandler(lbBTEX9_Click);
		this.lbBTEX9T.AutoSize = true;
		this.lbBTEX9T.Location = new System.Drawing.Point(294, 52);
		this.lbBTEX9T.Name = "lbBTEX9T";
		this.lbBTEX9T.Size = new System.Drawing.Size(29, 12);
		this.lbBTEX9T.TabIndex = 82;
		this.lbBTEX9T.Text = "苯：";
		this.lbBTEX9T.Click += new System.EventHandler(lbBTEX9T_Click);
		this.lbBTEX8.AutoSize = true;
		this.lbBTEX8.Cursor = System.Windows.Forms.Cursors.Hand;
		this.lbBTEX8.Location = new System.Drawing.Point(357, 34);
		this.lbBTEX8.Name = "lbBTEX8";
		this.lbBTEX8.Size = new System.Drawing.Size(11, 12);
		this.lbBTEX8.TabIndex = 81;
		this.lbBTEX8.Text = "0";
		this.lbBTEX8.Click += new System.EventHandler(lbBTEX8_Click);
		this.lbBTEX8T.AutoSize = true;
		this.lbBTEX8T.Location = new System.Drawing.Point(294, 34);
		this.lbBTEX8T.Name = "lbBTEX8T";
		this.lbBTEX8T.Size = new System.Drawing.Size(29, 12);
		this.lbBTEX8T.TabIndex = 80;
		this.lbBTEX8T.Text = "苯：";
		this.lbBTEX8T.Click += new System.EventHandler(lbBTEX8T_Click);
		this.lbNMHC.AutoSize = true;
		this.lbNMHC.Cursor = System.Windows.Forms.Cursors.Hand;
		this.lbNMHC.Location = new System.Drawing.Point(82, 51);
		this.lbNMHC.Name = "lbNMHC";
		this.lbNMHC.Size = new System.Drawing.Size(11, 12);
		this.lbNMHC.TabIndex = 79;
		this.lbNMHC.Text = "0";
		this.lbNMHC.Click += new System.EventHandler(lbNMHC_Click);
		this.lbBTEX.AutoSize = true;
		this.lbBTEX.Cursor = System.Windows.Forms.Cursors.Hand;
		this.lbBTEX.Location = new System.Drawing.Point(479, 52);
		this.lbBTEX.Name = "lbBTEX";
		this.lbBTEX.Size = new System.Drawing.Size(11, 12);
		this.lbBTEX.TabIndex = 78;
		this.lbBTEX.Text = "0";
		this.lbBTEX.Click += new System.EventHandler(lbBTEX_Click);
		this.lbNMHCT.AutoSize = true;
		this.lbNMHCT.Location = new System.Drawing.Point(6, 51);
		this.lbNMHCT.Name = "lbNMHCT";
		this.lbNMHCT.Size = new System.Drawing.Size(77, 12);
		this.lbNMHCT.TabIndex = 77;
		this.lbNMHCT.Text = "非甲烷总烃：";
		this.lbBTEXt.AutoSize = true;
		this.lbBTEXt.Location = new System.Drawing.Point(407, 52);
		this.lbBTEXt.Name = "lbBTEXt";
		this.lbBTEXt.Size = new System.Drawing.Size(53, 12);
		this.lbBTEXt.TabIndex = 76;
		this.lbBTEXt.Text = "苯系物：";
		this.lbBTEX7.AutoSize = true;
		this.lbBTEX7.Cursor = System.Windows.Forms.Cursors.Hand;
		this.lbBTEX7.Location = new System.Drawing.Point(358, 16);
		this.lbBTEX7.Name = "lbBTEX7";
		this.lbBTEX7.Size = new System.Drawing.Size(11, 12);
		this.lbBTEX7.TabIndex = 75;
		this.lbBTEX7.Text = "0";
		this.lbBTEX7.Click += new System.EventHandler(lbBTEX7_Click);
		this.lbBTEX7T.AutoSize = true;
		this.lbBTEX7T.Location = new System.Drawing.Point(294, 16);
		this.lbBTEX7T.Name = "lbBTEX7T";
		this.lbBTEX7T.Size = new System.Drawing.Size(29, 12);
		this.lbBTEX7T.TabIndex = 74;
		this.lbBTEX7T.Text = "苯：";
		this.lbBTEX7T.Click += new System.EventHandler(lbBTEX7T_Click);
		this.lbBTEX5.AutoSize = true;
		this.lbBTEX5.Cursor = System.Windows.Forms.Cursors.Hand;
		this.lbBTEX5.Location = new System.Drawing.Point(224, 34);
		this.lbBTEX5.Name = "lbBTEX5";
		this.lbBTEX5.Size = new System.Drawing.Size(11, 12);
		this.lbBTEX5.TabIndex = 73;
		this.lbBTEX5.Text = "0";
		this.lbBTEX5.Click += new System.EventHandler(lbBTEX5_Click);
		this.lbBTEX6.AutoSize = true;
		this.lbBTEX6.Cursor = System.Windows.Forms.Cursors.Hand;
		this.lbBTEX6.Location = new System.Drawing.Point(224, 52);
		this.lbBTEX6.Name = "lbBTEX6";
		this.lbBTEX6.Size = new System.Drawing.Size(11, 12);
		this.lbBTEX6.TabIndex = 72;
		this.lbBTEX6.Text = "0";
		this.lbBTEX6.Click += new System.EventHandler(lbBTEX6_Click);
		this.lbBTEX4.AutoSize = true;
		this.lbBTEX4.Cursor = System.Windows.Forms.Cursors.Hand;
		this.lbBTEX4.Location = new System.Drawing.Point(224, 16);
		this.lbBTEX4.Name = "lbBTEX4";
		this.lbBTEX4.Size = new System.Drawing.Size(11, 12);
		this.lbBTEX4.TabIndex = 71;
		this.lbBTEX4.Text = "0";
		this.lbBTEX4.Click += new System.EventHandler(lbBTEX4_Click);
		this.lbBTEX5T.AutoSize = true;
		this.lbBTEX5T.Location = new System.Drawing.Point(157, 34);
		this.lbBTEX5T.Name = "lbBTEX5T";
		this.lbBTEX5T.Size = new System.Drawing.Size(41, 12);
		this.lbBTEX5T.TabIndex = 70;
		this.lbBTEX5T.Text = "甲苯：";
		this.lbBTEX5T.Click += new System.EventHandler(lbBTEX5T_Click);
		this.lbBTEX6T.AutoSize = true;
		this.lbBTEX6T.Location = new System.Drawing.Point(157, 52);
		this.lbBTEX6T.Name = "lbBTEX6T";
		this.lbBTEX6T.Size = new System.Drawing.Size(41, 12);
		this.lbBTEX6T.TabIndex = 69;
		this.lbBTEX6T.Text = "乙苯：";
		this.lbBTEX6T.Click += new System.EventHandler(lbBTEX6T_Click);
		this.lbBTEX4T.AutoSize = true;
		this.lbBTEX4T.Location = new System.Drawing.Point(157, 16);
		this.lbBTEX4T.Name = "lbBTEX4T";
		this.lbBTEX4T.Size = new System.Drawing.Size(29, 12);
		this.lbBTEX4T.TabIndex = 68;
		this.lbBTEX4T.Text = "苯：";
		this.lbBTEX2.AutoSize = true;
		this.lbBTEX2.Cursor = System.Windows.Forms.Cursors.Hand;
		this.lbBTEX2.Location = new System.Drawing.Point(71, 34);
		this.lbBTEX2.Name = "lbBTEX2";
		this.lbBTEX2.Size = new System.Drawing.Size(11, 12);
		this.lbBTEX2.TabIndex = 67;
		this.lbBTEX2.Text = "0";
		this.lbBTEX2.Click += new System.EventHandler(lbBTEX2_Click);
		this.lbBTEX3.AutoSize = true;
		this.lbBTEX3.Cursor = System.Windows.Forms.Cursors.Hand;
		this.lbBTEX3.Location = new System.Drawing.Point(71, 52);
		this.lbBTEX3.Name = "lbBTEX3";
		this.lbBTEX3.Size = new System.Drawing.Size(11, 12);
		this.lbBTEX3.TabIndex = 66;
		this.lbBTEX3.Text = "0";
		this.lbBTEX3.Click += new System.EventHandler(lbBTEX3_Click);
		this.lbBTEX1.AutoSize = true;
		this.lbBTEX1.Cursor = System.Windows.Forms.Cursors.Hand;
		this.lbBTEX1.Location = new System.Drawing.Point(71, 16);
		this.lbBTEX1.Name = "lbBTEX1";
		this.lbBTEX1.Size = new System.Drawing.Size(11, 12);
		this.lbBTEX1.TabIndex = 65;
		this.lbBTEX1.Text = "0";
		this.lbBTEX1.Click += new System.EventHandler(lbBTEX1_Click);
		this.lbBTEX2T.AutoSize = true;
		this.lbBTEX2T.Location = new System.Drawing.Point(6, 34);
		this.lbBTEX2T.Name = "lbBTEX2T";
		this.lbBTEX2T.Size = new System.Drawing.Size(41, 12);
		this.lbBTEX2T.TabIndex = 64;
		this.lbBTEX2T.Text = "甲苯：";
		this.lbBTEX3T.AutoSize = true;
		this.lbBTEX3T.Location = new System.Drawing.Point(6, 52);
		this.lbBTEX3T.Name = "lbBTEX3T";
		this.lbBTEX3T.Size = new System.Drawing.Size(41, 12);
		this.lbBTEX3T.TabIndex = 63;
		this.lbBTEX3T.Text = "乙苯：";
		this.lbBTEX1T.AutoSize = true;
		this.lbBTEX1T.Location = new System.Drawing.Point(6, 16);
		this.lbBTEX1T.Name = "lbBTEX1T";
		this.lbBTEX1T.Size = new System.Drawing.Size(29, 12);
		this.lbBTEX1T.TabIndex = 62;
		this.lbBTEX1T.Text = "苯：";
		this.lbCH4.AutoSize = true;
		this.lbCH4.Cursor = System.Windows.Forms.Cursors.Hand;
		this.lbCH4.Location = new System.Drawing.Point(82, 35);
		this.lbCH4.Name = "lbCH4";
		this.lbCH4.Size = new System.Drawing.Size(11, 12);
		this.lbCH4.TabIndex = 61;
		this.lbCH4.Text = "0";
		this.lbCH4.Click += new System.EventHandler(lbCH4_Click);
		this.label79.AutoSize = true;
		this.label79.Location = new System.Drawing.Point(6, 35);
		this.label79.Name = "label79";
		this.label79.Size = new System.Drawing.Size(41, 12);
		this.label79.TabIndex = 60;
		this.label79.Text = "甲烷：";
		this.lbTHC.AutoSize = true;
		this.lbTHC.Cursor = System.Windows.Forms.Cursors.Hand;
		this.lbTHC.Location = new System.Drawing.Point(82, 15);
		this.lbTHC.Name = "lbTHC";
		this.lbTHC.Size = new System.Drawing.Size(11, 12);
		this.lbTHC.TabIndex = 59;
		this.lbTHC.Text = "0";
		this.lbTHC.Click += new System.EventHandler(lbTHC_Click);
		this.label77.AutoSize = true;
		this.label77.Location = new System.Drawing.Point(6, 17);
		this.label77.Name = "label77";
		this.label77.Size = new System.Drawing.Size(41, 12);
		this.label77.TabIndex = 58;
		this.label77.Text = "总烃：";
		this.tbFireOn2.Location = new System.Drawing.Point(567, 2);
		this.tbFireOn2.Name = "tbFireOn2";
		this.tbFireOn2.Size = new System.Drawing.Size(56, 21);
		this.tbFireOn2.TabIndex = 91;
		this.btnFireOnSet.Location = new System.Drawing.Point(339, 2);
		this.btnFireOnSet.Name = "btnFireOnSet";
		this.btnFireOnSet.Size = new System.Drawing.Size(91, 23);
		this.btnFireOnSet.TabIndex = 90;
		this.btnFireOnSet.Text = "点火门限设定";
		this.btnFireOnSet.UseVisualStyleBackColor = true;
		this.btnFireOnSet.Click += new System.EventHandler(btnFireOnSet_Click);
		this.btnFireOnCheck.Location = new System.Drawing.Point(256, 2);
		this.btnFireOnCheck.Name = "btnFireOnCheck";
		this.btnFireOnCheck.Size = new System.Drawing.Size(85, 23);
		this.btnFireOnCheck.TabIndex = 89;
		this.btnFireOnCheck.Text = "点火门限查询";
		this.btnFireOnCheck.UseVisualStyleBackColor = true;
		this.btnFireOnCheck.Click += new System.EventHandler(btnFireOnCheck_Click);
		this.label80.AutoSize = true;
		this.label80.Location = new System.Drawing.Point(433, 6);
		this.label80.Name = "label80";
		this.label80.Size = new System.Drawing.Size(65, 12);
		this.label80.TabIndex = 88;
		this.label80.Text = "点火门限：";
		this.tbFireOn.Location = new System.Drawing.Point(501, 2);
		this.tbFireOn.Name = "tbFireOn";
		this.tbFireOn.Size = new System.Drawing.Size(58, 21);
		this.tbFireOn.TabIndex = 87;
		this.gbBenXW.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.gbBenXW.Controls.Add(this.button2);
		this.gbBenXW.Controls.Add(this.button1);
		this.gbBenXW.Controls.Add(this.lbBTEX1T);
		this.gbBenXW.Controls.Add(this.btnSoftKey);
		this.gbBenXW.Controls.Add(this.lbBTEX3T);
		this.gbBenXW.Controls.Add(this.lbBTEX2T);
		this.gbBenXW.Controls.Add(this.lbBTEX1);
		this.gbBenXW.Controls.Add(this.lbBTEX3);
		this.gbBenXW.Controls.Add(this.lbBTEX9);
		this.gbBenXW.Controls.Add(this.lbBTEX2);
		this.gbBenXW.Controls.Add(this.lbBTEX9T);
		this.gbBenXW.Controls.Add(this.lbBTEX4T);
		this.gbBenXW.Controls.Add(this.lbBTEX8);
		this.gbBenXW.Controls.Add(this.lbBTEX6T);
		this.gbBenXW.Controls.Add(this.lbBTEX8T);
		this.gbBenXW.Controls.Add(this.lbBTEX5T);
		this.gbBenXW.Controls.Add(this.lbBTEX4);
		this.gbBenXW.Controls.Add(this.lbBTEX);
		this.gbBenXW.Controls.Add(this.lbBTEX6);
		this.gbBenXW.Controls.Add(this.lbBTEX5);
		this.gbBenXW.Controls.Add(this.lbBTEXt);
		this.gbBenXW.Controls.Add(this.lbBTEX7T);
		this.gbBenXW.Controls.Add(this.lbBTEX7);
		this.gbBenXW.Location = new System.Drawing.Point(170, 35);
		this.gbBenXW.Name = "gbBenXW";
		this.gbBenXW.Size = new System.Drawing.Size(597, 86);
		this.gbBenXW.TabIndex = 92;
		this.gbBenXW.TabStop = false;
		this.gbBenXW.Text = "苯系物";
		this.button2.Location = new System.Drawing.Point(604, 41);
		this.button2.Name = "button2";
		this.button2.Size = new System.Drawing.Size(75, 23);
		this.button2.TabIndex = 85;
		this.button2.Text = "button2";
		this.button2.UseVisualStyleBackColor = true;
		this.button2.Visible = false;
		this.button2.Click += new System.EventHandler(button2_Click);
		this.button1.Location = new System.Drawing.Point(604, 16);
		this.button1.Name = "button1";
		this.button1.Size = new System.Drawing.Size(75, 23);
		this.button1.TabIndex = 84;
		this.button1.Text = "button1";
		this.button1.UseVisualStyleBackColor = true;
		this.button1.Visible = false;
		this.button1.Click += new System.EventHandler(button1_Click);
		this.btnSoftKey.Font = new System.Drawing.Font("宋体", 9f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 134);
		this.btnSoftKey.Location = new System.Drawing.Point(521, 29);
		this.btnSoftKey.Name = "btnSoftKey";
		this.btnSoftKey.Size = new System.Drawing.Size(70, 32);
		this.btnSoftKey.TabIndex = 95;
		this.btnSoftKey.Text = "软键盘";
		this.btnSoftKey.UseVisualStyleBackColor = true;
		this.btnSoftKey.Visible = false;
		this.btnSoftKey.Click += new System.EventHandler(BtnSoftKey_Click);
		this.panel1.Controls.Add(this.btnDevice);
		this.panel1.Controls.Add(this.btnNetConfig);
		this.panel1.Controls.Add(this.cbKindMachine);
		this.panel1.Controls.Add(this.button36);
		this.panel1.Controls.Add(this.btShowDesktop);
		this.panel1.Controls.Add(this.btnFireOnCheck);
		this.panel1.Controls.Add(this.tbFireOn2);
		this.panel1.Controls.Add(this.label80);
		this.panel1.Controls.Add(this.tbFireOn);
		this.panel1.Controls.Add(this.btnFireOnSet);
		this.panel1.Location = new System.Drawing.Point(3, 6);
		this.panel1.Name = "panel1";
		this.panel1.Size = new System.Drawing.Size(627, 26);
		this.panel1.TabIndex = 94;
		this.btnDevice.Location = new System.Drawing.Point(0, 2);
		this.btnDevice.Name = "btnDevice";
		this.btnDevice.Size = new System.Drawing.Size(59, 23);
		this.btnDevice.TabIndex = 95;
		this.btnDevice.Text = "色谱机";
		this.btnDevice.UseVisualStyleBackColor = true;
		this.btnDevice.Click += new System.EventHandler(btnDevice_Click);
		this.btnNetConfig.Location = new System.Drawing.Point(191, 2);
		this.btnNetConfig.Name = "btnNetConfig";
		this.btnNetConfig.Size = new System.Drawing.Size(63, 23);
		this.btnNetConfig.TabIndex = 94;
		this.btnNetConfig.Text = "网络配置";
		this.btnNetConfig.UseVisualStyleBackColor = true;
		this.btnNetConfig.Click += new System.EventHandler(btnNetConfig_Click);
		this.cbKindMachine.FormattingEnabled = true;
		this.cbKindMachine.Items.AddRange(new object[5] { "非甲烷总烃+苯系物", "单非甲烷总烃", "双非甲烷总烃", "单苯系物", "B型" });
		this.cbKindMachine.Location = new System.Drawing.Point(721, 4);
		this.cbKindMachine.Name = "cbKindMachine";
		this.cbKindMachine.Size = new System.Drawing.Size(125, 20);
		this.cbKindMachine.TabIndex = 93;
		this.cbKindMachine.Text = "非甲烷总烃+苯系物";
		this.cbKindMachine.Visible = false;
		this.cbKindMachine.SelectedIndexChanged += new System.EventHandler(cbKindMachine_SelectedIndexChanged);
		this.btnCali.Font = new System.Drawing.Font("宋体", 9f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 134);
		this.btnCali.Location = new System.Drawing.Point(633, 2);
		this.btnCali.Name = "btnCali";
		this.btnCali.Size = new System.Drawing.Size(73, 33);
		this.btnCali.TabIndex = 92;
		this.btnCali.Text = "一键标定";
		this.btnCali.UseVisualStyleBackColor = true;
		this.btnCali.Click += new System.EventHandler(btnCali_Click);
		this.gbNHMC1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.gbNHMC1.Controls.Add(this.label79);
		this.gbNHMC1.Controls.Add(this.lbCH4);
		this.gbNHMC1.Controls.Add(this.lbTHC);
		this.gbNHMC1.Controls.Add(this.label77);
		this.gbNHMC1.Controls.Add(this.lbNMHCT);
		this.gbNHMC1.Controls.Add(this.lbNMHC);
		this.gbNHMC1.Location = new System.Drawing.Point(6, 36);
		this.gbNHMC1.Name = "gbNHMC1";
		this.gbNHMC1.Size = new System.Drawing.Size(158, 85);
		this.gbNHMC1.TabIndex = 86;
		this.gbNHMC1.TabStop = false;
		this.gbNHMC1.Text = "非甲烷总烃";
		this.cbCOM2.FormattingEnabled = true;
		this.cbCOM2.Items.AddRange(new object[2] { "4-20mA", "Modbus RTU" });
		this.cbCOM2.Location = new System.Drawing.Point(784, 43);
		this.cbCOM2.Name = "cbCOM2";
		this.cbCOM2.Size = new System.Drawing.Size(65, 20);
		this.cbCOM2.TabIndex = 96;
		this.cbCOM2.SelectedIndexChanged += new System.EventHandler(cbCOM2_SelectedIndexChanged);
		this.label1.AutoSize = true;
		this.label1.Location = new System.Drawing.Point(787, 20);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(65, 12);
		this.label1.TabIndex = 97;
		this.label1.Text = "COM2功能：";
		this.timer1.Enabled = true;
		this.timer1.Interval = 1000;
		this.timer1.Tick += new System.EventHandler(timer1_Tick);
		this.btnHistory.Location = new System.Drawing.Point(777, 69);
		this.btnHistory.Name = "btnHistory";
		this.btnHistory.Size = new System.Drawing.Size(75, 23);
		this.btnHistory.TabIndex = 98;
		this.btnHistory.Text = "数据查询";
		this.btnHistory.UseVisualStyleBackColor = true;
		this.btnHistory.Click += new System.EventHandler(btnHistory_Click);
		this.btnSetPassword.Location = new System.Drawing.Point(712, 3);
		this.btnSetPassword.Name = "btnSetPassword";
		this.btnSetPassword.Size = new System.Drawing.Size(69, 32);
		this.btnSetPassword.TabIndex = 99;
		this.btnSetPassword.Text = "修改密码";
		this.btnSetPassword.UseVisualStyleBackColor = true;
		this.btnSetPassword.Click += new System.EventHandler(btnSetPassword_Click);
		this.tabControl1.Controls.Add(this.tabPage1);
		this.tabControl1.Controls.Add(this.tabPage2);
		this.tabControl1.Controls.Add(this.tabPage3);
		this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
		this.tabControl1.Location = new System.Drawing.Point(3, 3);
		this.tabControl1.Name = "tabControl1";
		this.tabControl1.SelectedIndex = 0;
		this.tabControl1.Size = new System.Drawing.Size(906, 150);
		this.tabControl1.TabIndex = 100;
		this.tabPage1.Controls.Add(this.panel1);
		this.tabPage1.Controls.Add(this.btnSetPassword);
		this.tabPage1.Controls.Add(this.btnCali);
		this.tabPage1.Controls.Add(this.btnHistory);
		this.tabPage1.Controls.Add(this.gbBenXW);
		this.tabPage1.Controls.Add(this.label1);
		this.tabPage1.Controls.Add(this.gbNHMC1);
		this.tabPage1.Controls.Add(this.cbCOM2);
		this.tabPage1.Location = new System.Drawing.Point(4, 22);
		this.tabPage1.Name = "tabPage1";
		this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
		this.tabPage1.Size = new System.Drawing.Size(898, 124);
		this.tabPage1.TabIndex = 0;
		this.tabPage1.Text = "数据";
		this.tabPage1.UseVisualStyleBackColor = true;
		this.tabPage2.Controls.Add(this.sdaOpen);
		this.tabPage2.Controls.Add(this.tbSdaFileName);
		this.tabPage2.Controls.Add(this.labO);
		this.tabPage2.Controls.Add(this.label3);
		this.tabPage2.Controls.Add(this.label2);
		this.tabPage2.Controls.Add(this.chbO2);
		this.tabPage2.Location = new System.Drawing.Point(4, 22);
		this.tabPage2.Name = "tabPage2";
		this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
		this.tabPage2.Size = new System.Drawing.Size(898, 124);
		this.tabPage2.TabIndex = 1;
		this.tabPage2.Text = "氧干扰";
		this.tabPage2.UseVisualStyleBackColor = true;
		this.sdaOpen.Image = IBrainChrom2018.Properties.Resources.FolderOpen;
		this.sdaOpen.Location = new System.Drawing.Point(539, 52);
		this.sdaOpen.Name = "sdaOpen";
		this.sdaOpen.Size = new System.Drawing.Size(84, 32);
		this.sdaOpen.TabIndex = 30;
		this.sdaOpen.UseVisualStyleBackColor = true;
		this.sdaOpen.Click += new System.EventHandler(sdaOpen_Click);
		this.tbSdaFileName.Location = new System.Drawing.Point(97, 59);
		this.tbSdaFileName.Name = "tbSdaFileName";
		this.tbSdaFileName.Size = new System.Drawing.Size(436, 21);
		this.tbSdaFileName.TabIndex = 4;
		this.labO.AutoSize = true;
		this.labO.Location = new System.Drawing.Point(97, 35);
		this.labO.Name = "labO";
		this.labO.Size = new System.Drawing.Size(11, 12);
		this.labO.TabIndex = 3;
		this.labO.Text = "0";
		this.label3.AutoSize = true;
		this.label3.Location = new System.Drawing.Point(20, 35);
		this.label3.Name = "label3";
		this.label3.Size = new System.Drawing.Size(59, 12);
		this.label3.TabIndex = 2;
		this.label3.Text = "氧含量值:";
		this.label2.AutoSize = true;
		this.label2.Location = new System.Drawing.Point(20, 62);
		this.label2.Name = "label2";
		this.label2.Size = new System.Drawing.Size(71, 12);
		this.label2.TabIndex = 1;
		this.label2.Text = "氧干扰谱图:";
		this.chbO2.AutoSize = true;
		this.chbO2.Location = new System.Drawing.Point(22, 6);
		this.chbO2.Name = "chbO2";
		this.chbO2.Size = new System.Drawing.Size(108, 16);
		this.chbO2.TabIndex = 0;
		this.chbO2.Text = "使用氧干扰扣除";
		this.chbO2.UseVisualStyleBackColor = true;
		this.chbO2.CheckedChanged += new System.EventHandler(chbO2_CheckedChanged);
		this.tabPage3.Controls.Add(this.panel2);
		this.tabPage3.Location = new System.Drawing.Point(4, 22);
		this.tabPage3.Name = "tabPage3";
		this.tabPage3.Size = new System.Drawing.Size(898, 124);
		this.tabPage3.TabIndex = 2;
		this.tabPage3.Text = "通道循环数据";
		this.tabPage3.UseVisualStyleBackColor = true;
		this.panel2.Controls.Add(this.chbCycle);
		this.panel2.Controls.Add(this.label11);
		this.panel2.Controls.Add(this.label10);
		this.panel2.Controls.Add(this.label4);
		this.panel2.Controls.Add(this.label15);
		this.panel2.Controls.Add(this.tbTimeCycle1);
		this.panel2.Controls.Add(this.label14);
		this.panel2.Controls.Add(this.label5);
		this.panel2.Controls.Add(this.labTimes2);
		this.panel2.Controls.Add(this.chbCycle2);
		this.panel2.Controls.Add(this.tbTimeCycle2);
		this.panel2.Controls.Add(this.labTimes1);
		this.panel2.Controls.Add(this.tbPowerOnDelay);
		this.panel2.Controls.Add(this.tbTimesCycle1);
		this.panel2.Controls.Add(this.label13);
		this.panel2.Controls.Add(this.label7);
		this.panel2.Controls.Add(this.label12);
		this.panel2.Controls.Add(this.label6);
		this.panel2.Controls.Add(this.tbTimesCycle2);
		this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
		this.panel2.Location = new System.Drawing.Point(0, 0);
		this.panel2.Name = "panel2";
		this.panel2.Size = new System.Drawing.Size(898, 124);
		this.panel2.TabIndex = 47;
		this.chbCycle.AutoSize = true;
		this.chbCycle.Location = new System.Drawing.Point(764, 79);
		this.chbCycle.Name = "chbCycle";
		this.chbCycle.Size = new System.Drawing.Size(102, 16);
		this.chbCycle.TabIndex = 37;
		this.chbCycle.Text = "通道1自动循环";
		this.chbCycle.UseVisualStyleBackColor = true;
		this.chbCycle.CheckedChanged += new System.EventHandler(chbCycle_CheckedChanged);
		this.label11.AutoSize = true;
		this.label11.Font = new System.Drawing.Font("宋体", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		this.label11.Location = new System.Drawing.Point(3, 20);
		this.label11.Name = "label11";
		this.label11.Size = new System.Drawing.Size(112, 16);
		this.label11.TabIndex = 28;
		this.label11.Text = "通道1循环时间";
		this.label10.AutoSize = true;
		this.label10.Font = new System.Drawing.Font("宋体", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		this.label10.Location = new System.Drawing.Point(3, 48);
		this.label10.Name = "label10";
		this.label10.Size = new System.Drawing.Size(112, 16);
		this.label10.TabIndex = 30;
		this.label10.Text = "通道2循环时间";
		this.label4.AutoSize = true;
		this.label4.Font = new System.Drawing.Font("宋体", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		this.label4.Location = new System.Drawing.Point(340, 26);
		this.label4.Name = "label4";
		this.label4.Size = new System.Drawing.Size(112, 16);
		this.label4.TabIndex = 39;
		this.label4.Text = "通道1循环次数";
		this.label15.AutoSize = true;
		this.label15.Font = new System.Drawing.Font("宋体", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		this.label15.Location = new System.Drawing.Point(3, 79);
		this.label15.Name = "label15";
		this.label15.Size = new System.Drawing.Size(120, 16);
		this.label15.TabIndex = 34;
		this.label15.Text = "启动后采集延时";
		this.tbTimeCycle1.BackColor = System.Drawing.SystemColors.HighlightText;
		this.tbTimeCycle1.Font = new System.Drawing.Font("宋体", 10.5f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		this.tbTimeCycle1.ForeColor = System.Drawing.Color.Black;
		this.tbTimeCycle1.Location = new System.Drawing.Point(147, 20);
		this.tbTimeCycle1.Name = "tbTimeCycle1";
		this.tbTimeCycle1.Size = new System.Drawing.Size(111, 23);
		this.tbTimeCycle1.TabIndex = 29;
		this.tbTimeCycle1.Text = "0.00";
		this.tbTimeCycle1.TextChanged += new System.EventHandler(tbTimeCycle1_TextChanged);
		this.label14.AutoSize = true;
		this.label14.Font = new System.Drawing.Font("宋体", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		this.label14.Location = new System.Drawing.Point(264, 79);
		this.label14.Name = "label14";
		this.label14.Size = new System.Drawing.Size(32, 16);
		this.label14.TabIndex = 36;
		this.label14.Text = "min";
		this.label5.AutoSize = true;
		this.label5.Font = new System.Drawing.Font("宋体", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		this.label5.Location = new System.Drawing.Point(340, 54);
		this.label5.Name = "label5";
		this.label5.Size = new System.Drawing.Size(112, 16);
		this.label5.TabIndex = 41;
		this.label5.Text = "通道2循环次数";
		this.labTimes2.AutoSize = true;
		this.labTimes2.Location = new System.Drawing.Point(644, 58);
		this.labTimes2.Name = "labTimes2";
		this.labTimes2.Size = new System.Drawing.Size(11, 12);
		this.labTimes2.TabIndex = 46;
		this.labTimes2.Text = "0";
		this.chbCycle2.AutoSize = true;
		this.chbCycle2.Location = new System.Drawing.Point(764, 101);
		this.chbCycle2.Name = "chbCycle2";
		this.chbCycle2.Size = new System.Drawing.Size(102, 16);
		this.chbCycle2.TabIndex = 38;
		this.chbCycle2.Text = "通道2自动循环";
		this.chbCycle2.UseVisualStyleBackColor = true;
		this.chbCycle2.CheckedChanged += new System.EventHandler(chbCycle2_CheckedChanged);
		this.tbTimeCycle2.BackColor = System.Drawing.SystemColors.HighlightText;
		this.tbTimeCycle2.Font = new System.Drawing.Font("宋体", 10.5f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		this.tbTimeCycle2.ForeColor = System.Drawing.Color.Black;
		this.tbTimeCycle2.Location = new System.Drawing.Point(147, 48);
		this.tbTimeCycle2.Name = "tbTimeCycle2";
		this.tbTimeCycle2.Size = new System.Drawing.Size(111, 23);
		this.tbTimeCycle2.TabIndex = 31;
		this.tbTimeCycle2.Text = "0.00";
		this.tbTimeCycle2.TextChanged += new System.EventHandler(tbTimeCycle2_TextChanged);
		this.labTimes1.AutoSize = true;
		this.labTimes1.Location = new System.Drawing.Point(644, 31);
		this.labTimes1.Name = "labTimes1";
		this.labTimes1.Size = new System.Drawing.Size(11, 12);
		this.labTimes1.TabIndex = 45;
		this.labTimes1.Text = "0";
		this.tbPowerOnDelay.BackColor = System.Drawing.SystemColors.HighlightText;
		this.tbPowerOnDelay.Font = new System.Drawing.Font("宋体", 10.5f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		this.tbPowerOnDelay.ForeColor = System.Drawing.Color.Black;
		this.tbPowerOnDelay.Location = new System.Drawing.Point(147, 76);
		this.tbPowerOnDelay.Name = "tbPowerOnDelay";
		this.tbPowerOnDelay.Size = new System.Drawing.Size(111, 23);
		this.tbPowerOnDelay.TabIndex = 35;
		this.tbPowerOnDelay.Text = "0.00";
		this.tbPowerOnDelay.TextChanged += new System.EventHandler(tbPowerOnDelay_TextChanged);
		this.tbTimesCycle1.BackColor = System.Drawing.SystemColors.HighlightText;
		this.tbTimesCycle1.Font = new System.Drawing.Font("宋体", 10.5f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		this.tbTimesCycle1.ForeColor = System.Drawing.Color.Black;
		this.tbTimesCycle1.Location = new System.Drawing.Point(473, 26);
		this.tbTimesCycle1.Name = "tbTimesCycle1";
		this.tbTimesCycle1.Size = new System.Drawing.Size(111, 23);
		this.tbTimesCycle1.TabIndex = 40;
		this.tbTimesCycle1.Text = "0";
		this.tbTimesCycle1.TextChanged += new System.EventHandler(tbTimesCycle1_TextChanged);
		this.label13.AutoSize = true;
		this.label13.Font = new System.Drawing.Font("宋体", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		this.label13.Location = new System.Drawing.Point(264, 20);
		this.label13.Name = "label13";
		this.label13.Size = new System.Drawing.Size(32, 16);
		this.label13.TabIndex = 32;
		this.label13.Text = "min";
		this.label7.AutoSize = true;
		this.label7.Font = new System.Drawing.Font("宋体", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		this.label7.Location = new System.Drawing.Point(590, 54);
		this.label7.Name = "label7";
		this.label7.Size = new System.Drawing.Size(24, 16);
		this.label7.TabIndex = 44;
		this.label7.Text = "次";
		this.label12.AutoSize = true;
		this.label12.Font = new System.Drawing.Font("宋体", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		this.label12.Location = new System.Drawing.Point(264, 48);
		this.label12.Name = "label12";
		this.label12.Size = new System.Drawing.Size(32, 16);
		this.label12.TabIndex = 33;
		this.label12.Text = "min";
		this.label6.AutoSize = true;
		this.label6.Font = new System.Drawing.Font("宋体", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		this.label6.Location = new System.Drawing.Point(590, 26);
		this.label6.Name = "label6";
		this.label6.Size = new System.Drawing.Size(24, 16);
		this.label6.TabIndex = 43;
		this.label6.Text = "次";
		this.tbTimesCycle2.BackColor = System.Drawing.SystemColors.HighlightText;
		this.tbTimesCycle2.Font = new System.Drawing.Font("宋体", 10.5f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		this.tbTimesCycle2.ForeColor = System.Drawing.Color.Black;
		this.tbTimesCycle2.Location = new System.Drawing.Point(473, 54);
		this.tbTimesCycle2.Name = "tbTimesCycle2";
		this.tbTimesCycle2.Size = new System.Drawing.Size(111, 23);
		this.tbTimesCycle2.TabIndex = 42;
		this.tbTimesCycle2.Text = "0";
		this.tbTimesCycle2.TextChanged += new System.EventHandler(tbTimesCycle2_TextChanged);
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.Controls.Add(this.tabControl1);
		base.Name = "VocCtrl";
		base.Padding = new System.Windows.Forms.Padding(3);
		base.Size = new System.Drawing.Size(912, 156);
		this.gbBenXW.ResumeLayout(false);
		this.gbBenXW.PerformLayout();
		this.panel1.ResumeLayout(false);
		this.panel1.PerformLayout();
		this.gbNHMC1.ResumeLayout(false);
		this.gbNHMC1.PerformLayout();
		this.tabControl1.ResumeLayout(false);
		this.tabPage1.ResumeLayout(false);
		this.tabPage1.PerformLayout();
		this.tabPage2.ResumeLayout(false);
		this.tabPage2.PerformLayout();
		this.tabPage3.ResumeLayout(false);
		this.panel2.ResumeLayout(false);
		this.panel2.PerformLayout();
		base.ResumeLayout(false);
	}
}
