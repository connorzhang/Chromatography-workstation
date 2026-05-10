using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using IBrainChrom2018.Properties;
using IBrainChrom2018.Unit;

namespace IBrainChrom2018;

public class OnlineCtrl : UserControl
{
	public static OnlineCtrl selfCtrl;

	public bool bLoading = true;

	public bool bCombin1 = false;

	public bool bCombin2 = false;

	public bool bCombin3 = false;

	public string fileName1;

	public string fileName2;

	public string fileName3;

	public int cntFire = 0;

	public MyModbus mComModbusMaster = new MyModbus();

	private string strWarter = "";

	private float fWarter;

	private int iCMD = 0;

	private ChromDeviceListMgr cdlMgr = ChromDeviceListMgr.Create();

	private FormMainParam frmParam = FormMainParam.Create();

	private SystemParam sysParam = SystemParam.Create();

	private OnLineCtrlParam onLineCtrlParam = OnLineCtrlParam.Create();

	public SerialPortBase serialPoartBase = new SerialPortBase();

	public SerialPortBase serialPoartWS = new SerialPortBase();

	public SerialPortBaseOil serialPoartOil = new SerialPortBaseOil();

	private FormOnline formOnline = new FormOnline();

	private ChannelTCPServer ChannelTCPserver = new ChannelTCPServer(2000);

	private string[] amountStr1 = new string[50];

	public bool bStartAnalyze = false;

	public int iChannelIndex = 0;

	public bool bSendAnalyze = false;

	public int cntCycle = 0;

	public int cntAanlyzeTime = 0;

	public int currentFlowPath = 0;

	public int memoryFlowPath = 0;

	public int anysisChannel = 0;

	public byte channelEnabelState = 0;

	public bool AutoTempCtr = true;

	public int CountTempCtr = 0;

	public int CountState = 0;

	public ushort StateInstrument = 0;

	public int CountAnalyse = 0;

	public int StateYiqi = 0;

	public string UnitName1;

	public string UnitName2;

	public string UnitName3;

	public string UnitName11;

	public string peakName1;

	public string peakName2;

	public bool bAnalyse = false;

	public int cntAnalyseTime = 0;

	public int cntAnalyseTimes = 0;

	public int indexChannel = 0;

	public int indexChannelStart = 1;

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

	private Label label1;

	private Button MethodOpen3;

	public TextBox tbMethName3;

	private Label label3;

	private Button MethodOpen2;

	public TextBox tbMethName2;

	private Label label2;

	private Button MethodOpen1;

	public TextBox tbMethName1;

	private CheckBox chbChannel3;

	private CheckBox chbChannel2;

	private CheckBox chbChannel1;

	private Label label6;

	private TextBox tbInjecTime3;

	private Label label5;

	private TextBox tbInjecTime2;

	private Label label4;

	private TextBox tbInjecTime1;

	private Button btnSave;

	private Label label7;

	private TextBox tbComTimes;

	private Label label8;

	private TextBox tbCycleTimes;

	private Button btnStart;

	private Label label14;

	private Label label15;

	private Label label16;

	private Label label17;

	private Label label13;

	private Label label12;

	private Label label11;

	private Label label10;

	private Label label29;

	private TextBox tbCycle;

	private Label label26;

	private Label label27;

	private TextBox tbStartTime;

	private Button btnStartCycle;

	private ComboBox cbTimes1;

	private ComboBox cbTimes4;

	private ComboBox cbTimes3;

	private ComboBox cbTimes2;

	private ComboBox cbTimes8;

	private ComboBox cbTimes7;

	private ComboBox cbTimes6;

	private ComboBox cbTimes5;

	private ComboBox cbTimes12;

	private ComboBox cbTimes11;

	private ComboBox cbTimes10;

	private ComboBox cbTimes9;

	private Label label18;

	private Label label19;

	private Label label20;

	private Label label21;

	private Label label24;

	private Label label23;

	private TextBox tbCycleTime3;

	private ComboBox cbCoerce;

	private Label label22;

	private Button btnSave1;

	private Timer timer1;

	private Label labCntTime;

	private Label labCntTimes;

	private TabControl tabControl1;

	private TabPage tabPage1;

	private TabPage tabPage2;

	private Panel panel3;

	private TabPage tabPage3;

	private Panel panel2;

	private Label label28;

	private TabPage tabPage4;

	private Label label30;

	private Button btnModbusSave;

	private TextBox tbModbusAddress;

	private Label label31;

	private TextBox tbDelay;

	private Label label9;

	private TabPage tabPage5;

	private Label label87;

	private Label label88;

	private Label label84;

	private Label label83;

	private TextBox tbTimeOFF4;

	private TextBox tbTimeON4;

	private TextBox tbTimeOFF3;

	private TextBox tbTimeON3;

	private Label label86;

	private Label label89;

	private Label label85;

	private Label label82;

	private TextBox tbTimeOFF2;

	private TextBox tbTimeON2;

	private TextBox tbTimeOFF1;

	private TextBox tbTimeON1;

	private Button btnSavePeakTime;

	private TextBox tbName3;

	private TextBox tbName2;

	private TextBox tbName1;

	private Label label25;

	private TextBox tbTimeInterval;

	private TabPage tabPage6;

	private Label label32;

	private Label label39;

	private Label labPHH2;

	private Label label37;

	private Label label36;

	private Label label35;

	private Label label34;

	private Label label33;

	public TextBox tbPINJ1;

	public TextBox tbPINJ3;

	public TextBox tbPINJ2;

	public TextBox tbPAir2;

	public TextBox tbPHH2;

	public TextBox tbPAir1;

	public TextBox tbPHH1;

	public TextBox tbPINJ4;

	private ComboBox cbDectorNumber;

	private Button btnMonit;

	private Timer timer2;

	private ComboBox cb_checkBit;

	private Label label43;

	private ComboBox cb_stopBit;

	private Label label42;

	private ComboBox cb_dataBit;

	private Label label41;

	private ComboBox cb_baudRate;

	private Label label40;

	private ComboBox cb_portNameSend;

	private Label label38;

	private TextBox tbWarterTemp4;

	private Label label81;

	private TextBox tbWarterTemp3;

	private Label label44;

	private TextBox tbWarterTemp2;

	private Label label45;

	private TextBox tbWarterTemp;

	private TabPage tabPage7;

	private Button btnEPCSet;

	private Label label78;

	private Label label47;

	private Label label48;

	private Label label49;

	private Label label50;

	private Label label51;

	private Label label52;

	public TextBox tbWeiChuiCur1;

	public TextBox tbWeiChuiSet1;

	private Label label53;

	public TextBox tbColPreCur3;

	public TextBox tbColPreSet3;

	private Label label54;

	public TextBox tbColPreCur2;

	public TextBox tbColPreSet2;

	private Label label55;

	private Label label56;

	private Label label57;

	private Label label58;

	public TextBox tbAirCur1;

	public TextBox tbAirSet1;

	private Label label59;

	public TextBox tbHHCur1;

	public TextBox tbHHSet1;

	private Label label60;

	public TextBox tbColPreCur1;

	public TextBox tbColPreSet1;

	private Label label61;

	public CheckBox chbCalibra;

	private GroupBox groupBox1;

	private TextBox tbWarterPpmv;

	private Button btnConver;

	private Label labWS;

	public CheckBox chbCombinDector;

	[DllImport("System.Linq.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
	public static extern bool showDialog(IntPtr parent);

	[DllImport("System.Linq.dll", CallingConvention = CallingConvention.Cdecl)]
	public static extern void InitialDll();

	public static bool IsDesignMode()
	{
		return false;
	}

	private void LoadLanguage()
	{
		tabPage4.Text = Lang.PS("Modbus", "Modbus");
		tabPage7.Text = Lang.PS("EPC", "EPC");
		label48.Text = Lang.PS("实测值", "Measured");
		label78.Text = Lang.PS("实测值", "Measured");
		label61.Text = Lang.PS("载气1", "Cur1");
		label55.Text = Lang.PS("载气2", "Cur2");
		label54.Text = Lang.PS("载气3", "Cur3");
		label60.Text = Lang.PS("氢气1", "HH1");
		label59.Text = Lang.PS("空气1", "Air1");
		label53.Text = Lang.PS("尾吹1", "MakeUp1");
		btnEPCSet.Text = Lang.PS("流量设定", "EPC Set");
		label80.Text = Lang.PS("点火门限", "Ignition");
		label31.Text = Lang.PS("上传站号", "Stand no.");
		btnModbusSave.Text = Lang.PS("保存并应用", "Save&&apply");
		label30.Text = Lang.PS("注：改变串口设置重启生效", "Note: the change of serial port Settings is effective");
		label38.Text = Lang.PS("串口号：", "Serial no.:");
		label40.Text = Lang.PS("波特率：", "Baud rate:");
		label41.Text = Lang.PS("数据位：", "Data bits:");
		label42.Text = Lang.PS("停止位：", "Stop bit:");
		label43.Text = Lang.PS("校验位：", "Parity bit:");
		btnMonit.Text = Lang.PS("回到监控界面：", "Back monit");
	}

	public OnlineCtrl()
	{
		selfCtrl = this;
		formOnline.Show();
		InitializeComponent();
		LoadLanguage();
		if (IsDesignMode())
		{
			return;
		}
		AreaPlotParamMgr areaPlotParamMgr = AreaPlotParamMgr.Create();
		AreaPlotParam areaPlotParam = null;
		tbFireOn.Text = frmParam.fFireOn.ToString();
		tbFireOn2.Text = frmParam.fFireOn2.ToString();
		cbKindMachine.DropDownStyle = ComboBoxStyle.DropDownList;
		cbKindMachine.SelectedIndex = frmParam.kindMachine;
		tbTimeON1.Text = frmParam.fTimeOn1.ToString();
		tbTimeON2.Text = frmParam.fTimeOn2.ToString();
		tbTimeON3.Text = frmParam.fTimeOn3.ToString();
		tbTimeON4.Text = frmParam.fTimeOn4.ToString();
		tbTimeOFF1.Text = frmParam.fTimeOff1.ToString();
		tbTimeOFF2.Text = frmParam.fTimeOff2.ToString();
		tbTimeOFF3.Text = frmParam.fTimeOff3.ToString();
		tbTimeOFF4.Text = frmParam.fTimeOff4.ToString();
		tbTimeInterval.Text = frmParam.fTimeInterval.ToString();
		areaPlotParam = areaPlotParamMgr.GetAreaPlotParam(1);
		UnitName1 = areaPlotParam.UintName;
		areaPlotParam = areaPlotParamMgr.GetAreaPlotParam(1);
		UnitName2 = areaPlotParam.UintName;
		lbBTEX1T.Text = areaPlotParam.PeakName;
		areaPlotParam = areaPlotParamMgr.GetAreaPlotParam(9);
		lbBTEX9T.Text = areaPlotParam.PeakName;
		areaPlotParam = areaPlotParamMgr.GetAreaPlotParam(8);
		lbBTEX8T.Text = areaPlotParam.PeakName;
		areaPlotParam = areaPlotParamMgr.GetAreaPlotParam(7);
		lbBTEX7T.Text = areaPlotParam.PeakName;
		areaPlotParam = areaPlotParamMgr.GetAreaPlotParam(6);
		lbBTEX6T.Text = areaPlotParam.PeakName;
		areaPlotParam = areaPlotParamMgr.GetAreaPlotParam(5);
		lbBTEX5T.Text = areaPlotParam.PeakName;
		areaPlotParam = areaPlotParamMgr.GetAreaPlotParam(4);
		lbBTEX4T.Text = areaPlotParam.PeakName;
		areaPlotParam = areaPlotParamMgr.GetAreaPlotParam(3);
		lbBTEX3T.Text = areaPlotParam.PeakName;
		areaPlotParam = areaPlotParamMgr.GetAreaPlotParam(2);
		lbBTEX2T.Text = areaPlotParam.PeakName;
		frmParam.kindMachine = 3;
		tbModbusAddress.Text = frmParam.iModbusAddress.ToString();
		cb_portNameSend.Text = frmParam.strDCSCom;
		cb_baudRate.Text = frmParam.iBaudRate.ToString();
		cb_dataBit.Text = frmParam.iDataBit.ToString();
		cb_stopBit.Text = frmParam.iStopBit.ToString();
		cdlMgr.tcpServerMgr.mComModbus.DevAdd = frmParam.iModbusAddress;
		cdlMgr.tcpServerMgr.mComModbus.OpenMyCom(frmParam.strDCSCom, frmParam.iBaudRate, frmParam.iDataBit, Parity.None, (StopBits)frmParam.iStopBit);
		tabPage6.Parent = null;
		if (frmParam.iOnlineMode != 1)
		{
			tabPage7.Parent = null;
		}
		if (frmParam.kindMachine == 0)
		{
			gbNHMC1.Visible = true;
			gbBenXW.Visible = true;
		}
		else if (frmParam.kindMachine == 1)
		{
			gbNHMC1.Visible = true;
			gbBenXW.Visible = false;
		}
		else if (frmParam.kindMachine == 2)
		{
			gbNHMC1.Visible = true;
			gbBenXW.Visible = true;
			gbNHMC1.Text = "非甲烷总烃1";
			gbBenXW.Text = "非甲烷总烃2";
			lbBTEX1T.Text = "总烃:";
			lbBTEX2T.Text = "甲烷:";
			lbBTEXt.Text = "非甲烷总烃:";
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
		}
		else if (frmParam.kindMachine == 3)
		{
			gbNHMC1.Visible = false;
			gbBenXW.Visible = true;
			gbBenXW.Location = gbNHMC1.Location;
		}
		frmParam.iOnlineMode = 2;
		if (frmParam.iOnlineMode == 1)
		{
			btnCali.Visible = false;
			tabPage1.Parent = null;
			tabPage2.Parent = null;
			tabPage5.Parent = null;
			gbNHMC1.Visible = false;
			gbBenXW.Visible = false;
			tbMethName1.Text = frmParam.strMethodFilePath1;
			tbMethName2.Text = frmParam.strMethodFilePath2;
			tbMethName3.Text = frmParam.strMethodFilePath3;
			tbDelay.Text = frmParam.fInjecDelay.ToString();
			tbInjecTime1.Text = frmParam.fInjecTime1.ToString();
			tbInjecTime2.Text = frmParam.fInjecTime2.ToString();
			tbInjecTime3.Text = frmParam.fInjecTime3.ToString();
			tbName1.Text = frmParam.strName1;
			tbName2.Text = frmParam.strName2;
			tbName3.Text = frmParam.strName3;
			chbChannel1.Checked = frmParam.bChbChannel1;
			chbChannel2.Checked = frmParam.bChbChannel2;
			chbChannel3.Checked = frmParam.bChbChannel3;
			tbCycleTimes.Text = frmParam.iTbCycleTimes.ToString();
		}
		else if (frmParam.iOnlineMode == 2)
		{
			cbTimes1.SelectedIndex = onLineCtrlParam.iTimes1;
			cbTimes2.SelectedIndex = onLineCtrlParam.iTimes2;
			cbTimes3.SelectedIndex = onLineCtrlParam.iTimes3;
			cbTimes4.SelectedIndex = onLineCtrlParam.iTimes4;
			cbTimes5.SelectedIndex = onLineCtrlParam.iTimes5;
			cbTimes6.SelectedIndex = onLineCtrlParam.iTimes6;
			cbTimes7.SelectedIndex = onLineCtrlParam.iTimes7;
			cbTimes8.SelectedIndex = onLineCtrlParam.iTimes8;
			cbTimes9.SelectedIndex = onLineCtrlParam.iTimes9;
			cbTimes10.SelectedIndex = onLineCtrlParam.iTimes10;
			cbTimes11.SelectedIndex = onLineCtrlParam.iTimes11;
			cbTimes12.SelectedIndex = onLineCtrlParam.iTimes12;
			cbCoerce.SelectedIndex = onLineCtrlParam.iCoerce;
			tbCycle.Text = onLineCtrlParam.iCycles.ToString();
			tbCycleTime3.Text = onLineCtrlParam.fCycleTime.ToString();
			tbStartTime.Text = onLineCtrlParam.fStartTime.ToString();
			try
			{
				serialPoartBase.openPort();
			}
			catch (Exception ex)
			{
				LogMgr.Instance.LogError($"流路串口 COM{ex.Message}");
			}
			groupBox1.Visible = false;
			tabPage3.Parent = null;
		}
	}

	public void spectraCombined(string file1, string file2, string file3)
	{
		Chromatogram chromatogram = Chromatogram.LoadFromFile2(file1, DetectorStyle.General);
		Chromatogram chromatogram2 = Chromatogram.LoadFromFile2(file2, DetectorStyle.General);
		if (chromatogram != null && chromatogram2 != null)
		{
			int dotsNum = chromatogram.signal.DotsNum;
			chromatogram.signal.DotsNum = chromatogram.signal.DotsNum + chromatogram2.signal.DotsNum;
			Array.Resize(ref chromatogram.signal.oriDots, chromatogram.signal.DotsNum);
			for (int i = dotsNum; i < chromatogram.signal.oriDots.Length; i++)
			{
				chromatogram.signal.oriDots[i].X = chromatogram2.signal.oriDots[i - dotsNum].X + chromatogram.signal.oriDots[dotsNum - 1].X;
				chromatogram.signal.oriDots[i].Y = chromatogram2.signal.oriDots[i - dotsNum].Y;
			}
			Array.Resize(ref chromatogram.signal.dots, chromatogram.signal.DotsNum);
			chromatogram.signal.Smooth(16);
			chromatogram.SaveToFile(file3);
		}
		else
		{
			MessageBox.Show("请先选择谱图文件!");
		}
	}

	private void spectraCombined(string file1, string file2, string file4, string file3)
	{
		Chromatogram chromatogram = Chromatogram.LoadFromFile2(file1, DetectorStyle.General);
		Chromatogram chromatogram2 = Chromatogram.LoadFromFile2(file2, DetectorStyle.General);
		Chromatogram chromatogram3 = Chromatogram.LoadFromFile2(file4, DetectorStyle.General);
		if (chromatogram != null && chromatogram2 != null && chromatogram3 != null)
		{
			int dotsNum = chromatogram.signal.DotsNum;
			chromatogram.signal.DotsNum = chromatogram.signal.DotsNum + chromatogram2.signal.DotsNum;
			Array.Resize(ref chromatogram.signal.oriDots, chromatogram.signal.DotsNum);
			for (int i = dotsNum; i < chromatogram.signal.oriDots.Length; i++)
			{
				chromatogram.signal.oriDots[i].X = chromatogram2.signal.oriDots[i - dotsNum].X + chromatogram.signal.oriDots[dotsNum - 1].X;
				chromatogram.signal.oriDots[i].Y = chromatogram2.signal.oriDots[i - dotsNum].Y;
			}
			Array.Resize(ref chromatogram.signal.dots, chromatogram.signal.DotsNum);
			dotsNum = chromatogram.signal.DotsNum;
			chromatogram.signal.DotsNum = chromatogram.signal.DotsNum + chromatogram3.signal.DotsNum;
			Array.Resize(ref chromatogram.signal.oriDots, chromatogram.signal.DotsNum);
			for (int j = dotsNum; j < chromatogram.signal.oriDots.Length; j++)
			{
				chromatogram.signal.oriDots[j].X = chromatogram3.signal.oriDots[j - dotsNum].X + chromatogram.signal.oriDots[dotsNum - 1].X;
				chromatogram.signal.oriDots[j].Y = chromatogram3.signal.oriDots[j - dotsNum].Y;
			}
			Array.Resize(ref chromatogram.signal.dots, chromatogram.signal.DotsNum);
			chromatogram.signal.Smooth(16);
			chromatogram.SaveToFile(file3);
		}
		else
		{
			MessageBox.Show("请先选择谱图文件!");
		}
	}

	public void calorificValue(int selectedIndex, string fileName, string strID, string strSampleIndex)
	{
		if (!File.Exists(fileName))
		{
			return;
		}
		float[] array = new float[1];
		ushort[] array2 = new ushort[2];
		if (ChromForm.form == null)
		{
			ChromForm.form = new ChromForm();
		}
		ChromForm.form.OpenChrom(fileName, sampling: true, useCurrent: true);
		ChromForm.form.chromDataGrid.mstSetChromForm.bUseSet_Click(null, null);
		ChromForm.form.chromDataGrid.saveFile();
		Peak[] peakAllCompound = ChromForm.form.CurChrom.GetPeakAllCompound();
		Peak[] rltPeaks = ChromForm.form.CurChrom.RltPeaks;
		float num = 0f;
		short num2 = 0;
		if (selectedIndex == 100)
		{
			byte b = 0;
			float num3 = 0f;
			int num4 = 0;
			float[] array3 = new float[100];
			CaliGnl caliGnl = new CaliGnl();
			caliGnl = cdlMgr.ChartParaOperaList[0].mtdMgr.caliGnl;
			for (b = 0; b < caliGnl.cmpds.Count(); b++)
			{
				num4 = 0;
				while (1 <= rltPeaks.Count() && num4 < rltPeaks.Count())
				{
					if (rltPeaks[num4].pkRT >= caliGnl.cmpds[b].cmpdInfo.retainTime - caliGnl.cmpds[b].cmpdInfo.leftWindow && rltPeaks[num4].pkRT <= caliGnl.cmpds[b].cmpdInfo.retainTime + caliGnl.cmpds[b].cmpdInfo.rightWindow && !(rltPeaks[num4].name != caliGnl.cmpds[b].cmpdInfo.name) && rltPeaks[num4].height >= 0f)
					{
						if (cdlMgr.formMain.iChannel == 1)
						{
							if (FormKR.selfCtrl != null && b < FormKR.selfCtrl.fComponet.Length)
							{
								FormKR.selfCtrl.fComponet[b] = rltPeaks[num4].amount;
							}
							array = new float[1] { rltPeaks[num4].amount };
							array[0] = rltPeaks[num4].area;
							Buffer.BlockCopy(array, 0, array2, 0, 4);
							cdlMgr.tcpServerMgr.mComModbus.WordVaue[100 + b * 10] = array2[0];
							cdlMgr.tcpServerMgr.mComModbus.WordVaue[101 + b * 10] = array2[1];
							array[0] = rltPeaks[num4].areaPer;
							Buffer.BlockCopy(array, 0, array2, 0, 4);
							cdlMgr.tcpServerMgr.mComModbus.WordVaue[102 + b * 10] = array2[0];
							cdlMgr.tcpServerMgr.mComModbus.WordVaue[103 + b * 10] = array2[1];
							array[0] = rltPeaks[num4].amount;
							num += rltPeaks[num4].amount;
							Buffer.BlockCopy(array, 0, array2, 0, 4);
							cdlMgr.tcpServerMgr.mComModbus.WordVaue[104 + b * 10] = array2[0];
							cdlMgr.tcpServerMgr.mComModbus.WordVaue[105 + b * 10] = array2[1];
							array[0] = rltPeaks[num4].amountPer;
							Buffer.BlockCopy(array, 0, array2, 0, 4);
							cdlMgr.tcpServerMgr.mComModbus.WordVaue[106 + b * 10] = array2[0];
							cdlMgr.tcpServerMgr.mComModbus.WordVaue[107 + b * 10] = array2[1];
							Class49.InsertIntoVoc(1 + num4, 0, rltPeaks[num4].name, fileName.ToLower(), rltPeaks[num4].amount);
						}
						else if (cdlMgr.formMain.iChannel == 2)
						{
							if (FormKR.selfCtrl != null && b < FormKR.selfCtrl.fComponet2.Length)
							{
								FormKR.selfCtrl.fComponet2[b] = rltPeaks[num4].amount;
							}
							array = new float[1] { rltPeaks[num4].amount };
							array[0] = rltPeaks[num4].area;
							Buffer.BlockCopy(array, 0, array2, 0, 4);
							cdlMgr.tcpServerMgr.mComModbus.WordVaue[500 + b * 10] = array2[0];
							cdlMgr.tcpServerMgr.mComModbus.WordVaue[501 + b * 10] = array2[1];
							array[0] = rltPeaks[num4].areaPer;
							Buffer.BlockCopy(array, 0, array2, 0, 4);
							cdlMgr.tcpServerMgr.mComModbus.WordVaue[502 + b * 10] = array2[0];
							cdlMgr.tcpServerMgr.mComModbus.WordVaue[503 + b * 10] = array2[1];
							array[0] = rltPeaks[num4].amount;
							num += rltPeaks[num4].amount;
							Buffer.BlockCopy(array, 0, array2, 0, 4);
							cdlMgr.tcpServerMgr.mComModbus.WordVaue[504 + b * 10] = array2[0];
							cdlMgr.tcpServerMgr.mComModbus.WordVaue[505 + b * 10] = array2[1];
							array[0] = rltPeaks[num4].amountPer;
							Buffer.BlockCopy(array, 0, array2, 0, 4);
							cdlMgr.tcpServerMgr.mComModbus.WordVaue[506 + b * 10] = array2[0];
							cdlMgr.tcpServerMgr.mComModbus.WordVaue[507 + b * 10] = array2[1];
							Class49.InsertIntoVoc(51 + num4, 0, rltPeaks[num4].name, fileName.ToLower(), rltPeaks[num4].amount);
						}
						else if (cdlMgr.formMain.iChannel == 3)
						{
							if (FormKR.selfCtrl != null && b < FormKR.selfCtrl.fComponet3.Length)
							{
								FormKR.selfCtrl.fComponet3[b] = rltPeaks[num4].amount;
							}
							array = new float[1] { rltPeaks[num4].amount };
							array[0] = rltPeaks[num4].area;
							Buffer.BlockCopy(array, 0, array2, 0, 4);
							cdlMgr.tcpServerMgr.mComModbus.WordVaue[1000 + b * 10] = array2[0];
							cdlMgr.tcpServerMgr.mComModbus.WordVaue[1001 + b * 10] = array2[1];
							array[0] = rltPeaks[num4].areaPer;
							Buffer.BlockCopy(array, 0, array2, 0, 4);
							cdlMgr.tcpServerMgr.mComModbus.WordVaue[1002 + b * 10] = array2[0];
							cdlMgr.tcpServerMgr.mComModbus.WordVaue[1003 + b * 10] = array2[1];
							array[0] = rltPeaks[num4].amount;
							num += rltPeaks[num4].amount;
							Buffer.BlockCopy(array, 0, array2, 0, 4);
							cdlMgr.tcpServerMgr.mComModbus.WordVaue[1004 + b * 10] = array2[0];
							cdlMgr.tcpServerMgr.mComModbus.WordVaue[1005 + b * 10] = array2[1];
							array[0] = rltPeaks[num4].amountPer;
							Buffer.BlockCopy(array, 0, array2, 0, 4);
							cdlMgr.tcpServerMgr.mComModbus.WordVaue[1006 + b * 10] = array2[0];
							cdlMgr.tcpServerMgr.mComModbus.WordVaue[1007 + b * 10] = array2[1];
							Class49.InsertIntoVoc(102 + num4, 0, rltPeaks[num4].name, fileName.ToLower(), rltPeaks[num4].amount);
						}
						else if (cdlMgr.formMain.iChannel == 4)
						{
							if (FormKR.selfCtrl != null && b < FormKR.selfCtrl.fComponet4.Length)
							{
								FormKR.selfCtrl.fComponet4[b] = rltPeaks[num4].amount;
							}
							array = new float[1] { rltPeaks[num4].amount };
							array[0] = rltPeaks[num4].area;
							Buffer.BlockCopy(array, 0, array2, 0, 4);
							cdlMgr.tcpServerMgr.mComModbus.WordVaue[1500 + b * 10] = array2[0];
							cdlMgr.tcpServerMgr.mComModbus.WordVaue[1501 + b * 10] = array2[1];
							array[0] = rltPeaks[num4].areaPer;
							Buffer.BlockCopy(array, 0, array2, 0, 4);
							cdlMgr.tcpServerMgr.mComModbus.WordVaue[1502 + b * 10] = array2[0];
							cdlMgr.tcpServerMgr.mComModbus.WordVaue[1503 + b * 10] = array2[1];
							array[0] = rltPeaks[num4].amount;
							num += rltPeaks[num4].amount;
							Buffer.BlockCopy(array, 0, array2, 0, 4);
							cdlMgr.tcpServerMgr.mComModbus.WordVaue[1504 + b * 10] = array2[0];
							cdlMgr.tcpServerMgr.mComModbus.WordVaue[1505 + b * 10] = array2[1];
							array[0] = rltPeaks[num4].amountPer;
							Buffer.BlockCopy(array, 0, array2, 0, 4);
							cdlMgr.tcpServerMgr.mComModbus.WordVaue[1506 + b * 10] = array2[0];
							cdlMgr.tcpServerMgr.mComModbus.WordVaue[1507 + b * 10] = array2[1];
							Class49.InsertIntoVoc(151 + num4, 0, rltPeaks[num4].name, fileName.ToLower(), rltPeaks[num4].amount);
						}
					}
					num4++;
				}
			}
		}
		for (int i = 0; i < peakAllCompound.Length; i++)
		{
			if (cdlMgr.formMain.iChannel == 1)
			{
				if (FormKR.selfCtrl != null && i < FormKR.selfCtrl.fComponet.Length)
				{
					FormKR.selfCtrl.fComponet[i] = peakAllCompound[i].amount;
				}
				array = new float[1] { peakAllCompound[i].amount };
				array[0] = peakAllCompound[i].area;
				Buffer.BlockCopy(array, 0, array2, 0, 4);
				cdlMgr.tcpServerMgr.mComModbus.WordVaue[100 + i * 10] = array2[0];
				cdlMgr.tcpServerMgr.mComModbus.WordVaue[101 + i * 10] = array2[1];
				array[0] = peakAllCompound[i].areaPer;
				Buffer.BlockCopy(array, 0, array2, 0, 4);
				cdlMgr.tcpServerMgr.mComModbus.WordVaue[102 + i * 10] = array2[0];
				cdlMgr.tcpServerMgr.mComModbus.WordVaue[103 + i * 10] = array2[1];
				array[0] = peakAllCompound[i].amount;
				num += peakAllCompound[i].amount;
				Buffer.BlockCopy(array, 0, array2, 0, 4);
				cdlMgr.tcpServerMgr.mComModbus.WordVaue[104 + i * 10] = array2[0];
				cdlMgr.tcpServerMgr.mComModbus.WordVaue[105 + i * 10] = array2[1];
				array[0] = peakAllCompound[i].amountPer;
				Buffer.BlockCopy(array, 0, array2, 0, 4);
				cdlMgr.tcpServerMgr.mComModbus.WordVaue[106 + i * 10] = array2[0];
				cdlMgr.tcpServerMgr.mComModbus.WordVaue[107 + i * 10] = array2[1];
				Class49.InsertIntoVoc(1 + i, 0, peakAllCompound[i].name, fileName.ToLower(), peakAllCompound[i].amount);
			}
			else if (cdlMgr.formMain.iChannel == 2)
			{
				if (FormKR.selfCtrl != null && i < FormKR.selfCtrl.fComponet2.Length)
				{
					FormKR.selfCtrl.fComponet2[i] = peakAllCompound[i].amount;
				}
				array = new float[1] { peakAllCompound[i].amount };
				array[0] = peakAllCompound[i].area;
				Buffer.BlockCopy(array, 0, array2, 0, 4);
				cdlMgr.tcpServerMgr.mComModbus.WordVaue[500 + i * 10] = array2[0];
				cdlMgr.tcpServerMgr.mComModbus.WordVaue[501 + i * 10] = array2[1];
				array[0] = peakAllCompound[i].areaPer;
				Buffer.BlockCopy(array, 0, array2, 0, 4);
				cdlMgr.tcpServerMgr.mComModbus.WordVaue[502 + i * 10] = array2[0];
				cdlMgr.tcpServerMgr.mComModbus.WordVaue[503 + i * 10] = array2[1];
				array[0] = peakAllCompound[i].amount;
				num += peakAllCompound[i].amount;
				Buffer.BlockCopy(array, 0, array2, 0, 4);
				cdlMgr.tcpServerMgr.mComModbus.WordVaue[504 + i * 10] = array2[0];
				cdlMgr.tcpServerMgr.mComModbus.WordVaue[505 + i * 10] = array2[1];
				array[0] = peakAllCompound[i].amountPer;
				Buffer.BlockCopy(array, 0, array2, 0, 4);
				cdlMgr.tcpServerMgr.mComModbus.WordVaue[506 + i * 10] = array2[0];
				cdlMgr.tcpServerMgr.mComModbus.WordVaue[507 + i * 10] = array2[1];
				Class49.InsertIntoVoc(51 + i, 0, peakAllCompound[i].name, fileName.ToLower(), peakAllCompound[i].amount);
			}
			else if (cdlMgr.formMain.iChannel == 3)
			{
				if (FormKR.selfCtrl != null && i < FormKR.selfCtrl.fComponet3.Length)
				{
					FormKR.selfCtrl.fComponet3[i] = peakAllCompound[i].amount;
				}
				array = new float[1] { peakAllCompound[i].amount };
				array[0] = peakAllCompound[i].area;
				Buffer.BlockCopy(array, 0, array2, 0, 4);
				cdlMgr.tcpServerMgr.mComModbus.WordVaue[1000 + i * 10] = array2[0];
				cdlMgr.tcpServerMgr.mComModbus.WordVaue[1001 + i * 10] = array2[1];
				array[0] = peakAllCompound[i].areaPer;
				Buffer.BlockCopy(array, 0, array2, 0, 4);
				cdlMgr.tcpServerMgr.mComModbus.WordVaue[1002 + i * 10] = array2[0];
				cdlMgr.tcpServerMgr.mComModbus.WordVaue[1003 + i * 10] = array2[1];
				array[0] = peakAllCompound[i].amount;
				num += peakAllCompound[i].amount;
				Buffer.BlockCopy(array, 0, array2, 0, 4);
				cdlMgr.tcpServerMgr.mComModbus.WordVaue[1004 + i * 10] = array2[0];
				cdlMgr.tcpServerMgr.mComModbus.WordVaue[1005 + i * 10] = array2[1];
				array[0] = peakAllCompound[i].amountPer;
				Buffer.BlockCopy(array, 0, array2, 0, 4);
				cdlMgr.tcpServerMgr.mComModbus.WordVaue[1006 + i * 10] = array2[0];
				cdlMgr.tcpServerMgr.mComModbus.WordVaue[1007 + i * 10] = array2[1];
				Class49.InsertIntoVoc(102 + i, 0, peakAllCompound[i].name, fileName.ToLower(), peakAllCompound[i].amount);
			}
			else if (cdlMgr.formMain.iChannel == 4)
			{
				if (FormKR.selfCtrl != null && i < FormKR.selfCtrl.fComponet4.Length)
				{
					FormKR.selfCtrl.fComponet4[i] = peakAllCompound[i].amount;
				}
				array = new float[1] { peakAllCompound[i].amount };
				array[0] = peakAllCompound[i].area;
				Buffer.BlockCopy(array, 0, array2, 0, 4);
				cdlMgr.tcpServerMgr.mComModbus.WordVaue[1500 + i * 10] = array2[0];
				cdlMgr.tcpServerMgr.mComModbus.WordVaue[1501 + i * 10] = array2[1];
				array[0] = peakAllCompound[i].areaPer;
				Buffer.BlockCopy(array, 0, array2, 0, 4);
				cdlMgr.tcpServerMgr.mComModbus.WordVaue[1502 + i * 10] = array2[0];
				cdlMgr.tcpServerMgr.mComModbus.WordVaue[1503 + i * 10] = array2[1];
				array[0] = peakAllCompound[i].amount;
				num += rltPeaks[i].amount;
				Buffer.BlockCopy(array, 0, array2, 0, 4);
				cdlMgr.tcpServerMgr.mComModbus.WordVaue[1504 + i * 10] = array2[0];
				cdlMgr.tcpServerMgr.mComModbus.WordVaue[1505 + i * 10] = array2[1];
				array[0] = peakAllCompound[i].amountPer;
				Buffer.BlockCopy(array, 0, array2, 0, 4);
				cdlMgr.tcpServerMgr.mComModbus.WordVaue[1506 + i * 10] = array2[0];
				cdlMgr.tcpServerMgr.mComModbus.WordVaue[1507 + i * 10] = array2[1];
				Class49.InsertIntoVoc(151 + i, 0, peakAllCompound[i].name, fileName.ToLower(), peakAllCompound[i].amount);
			}
			if (7 + i * 2 < frmParam.iChannelACnt * 2 + 7)
			{
				switch (i)
				{
				case -1:
					num2 = (short)((peakAllCompound[i].amount - frmParam.fmount41 - frmParam.fCompen1) / (frmParam.fmount201 - frmParam.fmount41) * 4095f);
					break;
				case 0:
					num2 = (short)((peakAllCompound[i].amount - frmParam.fmount42 - frmParam.fCompen2) / (frmParam.fmount202 - frmParam.fmount42) * 4095f);
					break;
				case 1:
					num2 = (short)((peakAllCompound[i].amount - frmParam.fmount43 - frmParam.fCompen3) / (frmParam.fmount203 - frmParam.fmount43) * 4095f);
					break;
				case 2:
					num2 = (short)((peakAllCompound[i].amount - frmParam.fmount44 - frmParam.fCompen4) / (frmParam.fmount204 - frmParam.fmount44) * 4095f);
					break;
				case 3:
					num2 = (short)((peakAllCompound[i].amount - frmParam.fmount45 - frmParam.fCompen5) / (frmParam.fmount205 - frmParam.fmount45) * 4095f);
					break;
				case 4:
					num2 = (short)((peakAllCompound[i].amount - frmParam.fmount46 - frmParam.fCompen6) / (frmParam.fmount206 - frmParam.fmount46) * 4095f);
					break;
				case 5:
					num2 = (short)((peakAllCompound[i].amount - frmParam.fmount47 - frmParam.fCompen7) / (frmParam.fmount207 - frmParam.fmount47) * 4095f);
					break;
				case 6:
					num2 = (short)((peakAllCompound[i].amount - frmParam.fmount48 - frmParam.fCompen8) / (frmParam.fmount208 - frmParam.fmount48) * 4095f);
					break;
				case 7:
					num2 = (short)((peakAllCompound[i].amount - frmParam.fmount49 - frmParam.fCompen9) / (frmParam.fmount209 - frmParam.fmount49) * 4095f);
					break;
				case 8:
					num2 = (short)((peakAllCompound[i].amount - frmParam.fmount410 - frmParam.fCompen10) / (frmParam.fmount2010 - frmParam.fmount410) * 4095f);
					break;
				case 9:
					num2 = (short)((peakAllCompound[i].amount - frmParam.fmount411 - frmParam.fCompen11) / (frmParam.fmount2011 - frmParam.fmount411) * 4095f);
					break;
				case 10:
					num2 = (short)((peakAllCompound[i].amount - frmParam.fmount412 - frmParam.fCompen12) / (frmParam.fmount2012 - frmParam.fmount412) * 4095f);
					break;
				}
				if (num2 < 0)
				{
					num2 = 0;
				}
				if (num2 > 4095)
				{
					num2 = 4095;
				}
				serialPoartBase.Data2[7 + (i + 1) * 2] = (byte)(num2 >> 8);
				serialPoartBase.Data2[8 + (i + 1) * 2] = (byte)num2;
			}
		}
		num2 = (short)((num - frmParam.fmount41 - frmParam.fCompen1) / (frmParam.fmount201 - frmParam.fmount41) * 4095f);
		if (num2 < 0)
		{
			num2 = 0;
		}
		if (num2 > 4095)
		{
			num2 = 4095;
		}
		serialPoartBase.Data2[7] = (byte)(num2 >> 8);
		serialPoartBase.Data2[8] = (byte)num2;
	}

	public void disposePeaks(int selectedIndex, string fileName, string strID, string strSampleIndex, Chromatogram chromatogram)
	{
		float[] array = new float[1];
		ushort[] array2 = new ushort[2];
		if (frmParam.iOnlineMode == 1)
		{
			byte b = 0;
			int num = 0;
			if (selectedIndex == 0)
			{
				bCombin1 = true;
				fileName1 = fileName;
			}
			if (selectedIndex == 1)
			{
				bCombin2 = true;
				this.fileName2 = fileName;
			}
			if (!bCombin1 || !bCombin2)
			{
				return;
			}
			bCombin1 = false;
			bCombin2 = false;
			string text = fileName1.Substring(0, fileName1.LastIndexOf("."));
			text += "合并.sda";
			spectraCombined(fileName1, this.fileName2, text);
			cdlMgr.formMain.tabControl.SelectedIndex = 2;
			cdlMgr.formMain.chromFormCtrl.OpenChrom(text, sampling: true, useCurrent: true);
			cdlMgr.formMain.chromFormCtrl.mstSetChromForm.bUseSet_Click(null, null);
			cdlMgr.formMain.chromFormCtrl.saveFile();
			array = new float[1];
			Buffer.BlockCopy(array, 0, array2, 0, 4);
			Peak[] rltPeaks = cdlMgr.formMain.chromFormCtrl.CurChrom.RltPeaks;
			if (cdlMgr.CurrentChartParaOpera.mtdMgr.caliGnl == null)
			{
				cdlMgr.CurrentChartParaOpera.mtdMgr.caliGnl = new CaliGnl();
			}
			CaliGnl caliGnl = cdlMgr.CurrentChartParaOpera.mtdMgr.caliGnl;
			CaliGnl caliGnl2 = new CaliGnl();
			caliGnl2 = cdlMgr.ChartParaOperaList[0].mtdMgr.caliGnl;
			Peak[] array3 = new Peak[caliGnl2.cmpds.Count()];
			for (b = 0; b < caliGnl2.cmpds.Count(); b++)
			{
				num = 0;
				while (1 <= rltPeaks.Count() && num < rltPeaks.Count())
				{
					if (rltPeaks[num].pkRT >= caliGnl2.cmpds[b].cmpdInfo.retainTime - caliGnl2.cmpds[b].cmpdInfo.leftWindow && rltPeaks[num].pkRT <= caliGnl2.cmpds[b].cmpdInfo.retainTime + caliGnl2.cmpds[b].cmpdInfo.rightWindow && !(rltPeaks[num].name != caliGnl2.cmpds[b].cmpdInfo.name))
					{
						array3[b] = rltPeaks[num];
						array = new float[1] { rltPeaks[num].area };
						Buffer.BlockCopy(array, 0, array2, 0, 4);
						cdlMgr.tcpServerMgr.mComModbus.WordVaue[100 + b * 10] = array2[0];
						cdlMgr.tcpServerMgr.mComModbus.WordVaue[101 + b * 10] = array2[1];
						array[0] = rltPeaks[num].areaPer;
						Buffer.BlockCopy(array, 0, array2, 0, 4);
						cdlMgr.tcpServerMgr.mComModbus.WordVaue[102 + b * 10] = array2[0];
						cdlMgr.tcpServerMgr.mComModbus.WordVaue[103 + b * 10] = array2[1];
						array[0] = rltPeaks[num].amount;
						Buffer.BlockCopy(array, 0, array2, 0, 4);
						cdlMgr.tcpServerMgr.mComModbus.WordVaue[104 + b * 10] = array2[0];
						cdlMgr.tcpServerMgr.mComModbus.WordVaue[105 + b * 10] = array2[1];
						array[0] = rltPeaks[num].amountPer;
						Buffer.BlockCopy(array, 0, array2, 0, 4);
						cdlMgr.tcpServerMgr.mComModbus.WordVaue[106 + b * 10] = array2[0];
						cdlMgr.tcpServerMgr.mComModbus.WordVaue[107 + b * 10] = array2[1];
						break;
					}
					num++;
				}
			}
			FormRltReminder formRltReminder = new FormRltReminder(array3, indexChannelStart);
			formRltReminder.StartPosition = FormStartPosition.CenterScreen;
			formRltReminder.TopMost = true;
			formRltReminder.Show();
			return;
		}
		AreaPlotParamMgr areaPlotParamMgr = AreaPlotParamMgr.Create();
		AreaPlotParam areaPlotParam = null;
		int index = 0;
		byte indexGnl = 0;
		byte b2 = 0;
		float num2 = 0f;
		float fTHC = 0f;
		float fCH4 = 0f;
		float num3 = 0f;
		cdlMgr.tcpServerMgr.mComModbus.WordVaue[7] = (ushort)memoryFlowPath;
		if (cdlMgr.CurrentChartParaOpera.mtdMgr.caliGnl == null)
		{
			cdlMgr.CurrentChartParaOpera.mtdMgr.caliGnl = new CaliGnl();
		}
		CaliGnl caliGnl3 = cdlMgr.CurrentChartParaOpera.mtdMgr.caliGnl;
		Peak[] peak = chromatogram.RltPeaks;
		switch (selectedIndex)
		{
		case 0:
		{
			for (int num11 = 0; num11 < 50; num11++)
			{
				amountStr1[num11] = 0.ToString("F" + Class49.int_8);
			}
			CaliGnl caliGnl5 = new CaliGnl();
			caliGnl5 = cdlMgr.ChartParaOperaList[0].mtdMgr.caliGnl;
			float[] array5 = new float[100];
			float num12 = 0f;
			areaPlotParam = areaPlotParamMgr.GetAreaPlotParam(11);
			UnitName2 = areaPlotParam.UintName;
			float num13 = 0f;
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
			if (FormOnline.selfCtrl != null)
			{
				if (currentFlowPath == 1)
				{
					for (int num14 = 0; num14 < FormOnline.selfCtrl.lstSource1.Count; num14++)
					{
						FormOnline.selfCtrl.lstSource1[num14].setV = "0";
					}
				}
				else if (currentFlowPath == 2)
				{
					for (int num15 = 0; num15 < FormOnline.selfCtrl.lstSource2.Count; num15++)
					{
						FormOnline.selfCtrl.lstSource2[num15].setV = "0";
					}
				}
				else if (currentFlowPath == 3)
				{
					for (int num16 = 0; num16 < FormOnline.selfCtrl.lstSource3.Count; num16++)
					{
						FormOnline.selfCtrl.lstSource3[num16].setV = "0";
					}
				}
				else if (currentFlowPath == 4)
				{
					for (int num17 = 0; num17 < FormOnline.selfCtrl.lstSource4.Count; num17++)
					{
						FormOnline.selfCtrl.lstSource4[num17].setV = "0";
					}
				}
			}
			array = new float[1];
			Buffer.BlockCopy(array, 0, array2, 0, 4);
			for (int num18 = 0; num18 < 15; num18++)
			{
				cdlMgr.tcpServerMgr.mComModbus.WordVaue[100 + num18 * 10] = array2[0];
				cdlMgr.tcpServerMgr.mComModbus.WordVaue[101 + num18 * 10] = array2[1];
				cdlMgr.tcpServerMgr.mComModbus.WordVaue[102 + num18 * 10] = array2[0];
				cdlMgr.tcpServerMgr.mComModbus.WordVaue[103 + num18 * 10] = array2[1];
				cdlMgr.tcpServerMgr.mComModbus.WordVaue[104 + num18 * 10] = array2[0];
				cdlMgr.tcpServerMgr.mComModbus.WordVaue[105 + num18 * 10] = array2[1];
				cdlMgr.tcpServerMgr.mComModbus.WordVaue[106 + num18 * 10] = array2[0];
				cdlMgr.tcpServerMgr.mComModbus.WordVaue[107 + num18 * 10] = array2[1];
				num13 = 0f;
				if (FormOnline.selfCtrl != null)
				{
					if (currentFlowPath == 1)
					{
						FormOnline.selfCtrl.lstSource1[num18].setV = num13.ToString("F" + Class49.int_8);
					}
					else if (currentFlowPath == 2)
					{
						FormOnline.selfCtrl.lstSource2[num18].setV = num13.ToString("F" + Class49.int_8);
					}
					else if (currentFlowPath == 3)
					{
						FormOnline.selfCtrl.lstSource3[num18].setV = num13.ToString("F" + Class49.int_8);
					}
					else if (currentFlowPath == 4)
					{
						FormOnline.selfCtrl.lstSource4[num18].setV = num13.ToString("F" + Class49.int_8);
					}
				}
			}
			for (indexGnl = 0; indexGnl < caliGnl5.cmpds.Count(); indexGnl++)
			{
				index = 0;
				while (1 <= peak.Count() && index < peak.Count())
				{
					if (peak[index].pkRT >= caliGnl5.cmpds[indexGnl].cmpdInfo.retainTime - caliGnl5.cmpds[indexGnl].cmpdInfo.leftWindow && peak[index].pkRT <= caliGnl5.cmpds[indexGnl].cmpdInfo.retainTime + caliGnl5.cmpds[indexGnl].cmpdInfo.rightWindow && !(peak[index].name != caliGnl5.cmpds[indexGnl].cmpdInfo.name) && peak[index].height >= num2)
					{
						if (cdlMgr.formMain.IsAutoCalibra == 1)
						{
							b2++;
							caliGnl5.cmpds[indexGnl].levels[0].responseA = peak[index].area;
							caliGnl5.CalculateFunc(appendLink: false);
							caliGnl5.cmpds[indexGnl].levels[0].respFactor = peak[index].GasAmount / peak[index].area;
							cdlMgr.ChartParaOperaList[0].mtdMgr.SaveToFile();
							cdlMgr.formMain.MainmstSet.ReadFromMtdMgr();
							array5[indexGnl] = (peak[index].amount = peak[index].area * caliGnl5.cmpds[indexGnl].levels[0].respFactor);
							cdlMgr.ChartParaOperaList[1].mtdMgr.SaveToFile();
							cdlMgr.formMain.MainmstSet.UsePara();
							cdlMgr.formMain.MainmstSet.ReadFromMtdMgr();
							cdlMgr.formMain.MainmstSet.bUseSet_Click(null, null);
						}
						else
						{
							array5[indexGnl] = peak[index].amount;
							array = new float[1] { array5[indexGnl] };
							array[0] = peak[index].area;
							Buffer.BlockCopy(array, 0, array2, 0, 4);
							cdlMgr.tcpServerMgr.mComModbus.WordVaue[100 + indexGnl * 10] = array2[0];
							cdlMgr.tcpServerMgr.mComModbus.WordVaue[101 + indexGnl * 10] = array2[1];
							array[0] = peak[index].areaPer;
							Buffer.BlockCopy(array, 0, array2, 0, 4);
							cdlMgr.tcpServerMgr.mComModbus.WordVaue[102 + indexGnl * 10] = array2[0];
							cdlMgr.tcpServerMgr.mComModbus.WordVaue[103 + indexGnl * 10] = array2[1];
							array[0] = peak[index].amountPer;
							Buffer.BlockCopy(array, 0, array2, 0, 4);
							cdlMgr.tcpServerMgr.mComModbus.WordVaue[106 + indexGnl * 10] = array2[0];
							cdlMgr.tcpServerMgr.mComModbus.WordVaue[107 + indexGnl * 10] = array2[1];
							num13 = ((caliGnl5.cmpds[indexGnl].eFunc.curveFit != CurveFit.Free) ? peak[index].amount : (peak[index].amountPer * 100f));
							if (num13 < 0f)
							{
								num13 = 0f;
							}
							amountStr1[indexGnl] = num13.ToString("F" + Class49.int_8);
							if (tabRltCtrl.selfCtrl != null)
							{
								MethodInvoker method3 = delegate
								{
									if (indexGnl == 0)
									{
										fTHC = peak[index].amount;
										tabRltCtrl.selfCtrl.tbTHC.Text = peak[index].amount.ToString("0.00");
									}
									else if (indexGnl == 1)
									{
										fCH4 = peak[index].amount;
										tabRltCtrl.selfCtrl.tbCH4.Text = peak[index].amount.ToString("0.00");
									}
								};
								Invoke(method3);
							}
							if (FormOnline.selfCtrl != null)
							{
								if (currentFlowPath == 1)
								{
									FormOnline.selfCtrl.lstSource1[indexGnl].setV = num13.ToString("F" + Class49.int_8);
								}
								else if (currentFlowPath == 2)
								{
									FormOnline.selfCtrl.lstSource2[indexGnl].setV = num13.ToString("F" + Class49.int_8);
								}
								else if (currentFlowPath == 3)
								{
									FormOnline.selfCtrl.lstSource3[indexGnl].setV = num13.ToString("F" + Class49.int_8);
								}
								else if (currentFlowPath == 4)
								{
									FormOnline.selfCtrl.lstSource4[indexGnl].setV = num13.ToString("F" + Class49.int_8);
								}
							}
							try
							{
								Class49.InsertIntoVoc(1 + indexGnl, 0, peak[index].name, fileName.ToLower(), peak[index].amount);
							}
							catch (Exception ex2)
							{
								LogMgr.Instance.LogWarning(ex2.Message);
							}
						}
					}
					index++;
				}
			}
			if (FormOnline.selfCtrl != null)
			{
				FormOnline.selfCtrl.reloadData();
			}
			lbBTEX9.Text = array5[8].ToString("F" + Class49.int_8);
			lbBTEX8.Text = array5[7].ToString("F" + Class49.int_8);
			lbBTEX7.Text = array5[6].ToString("F" + Class49.int_8);
			lbBTEX6.Text = array5[5].ToString("F" + Class49.int_8);
			lbBTEX5.Text = array5[4].ToString("F" + Class49.int_8);
			lbBTEX4.Text = array5[3].ToString("F" + Class49.int_8);
			lbBTEX3.Text = array5[2].ToString("F" + Class49.int_8);
			lbBTEX2.Text = array5[1].ToString("F" + Class49.int_8);
			lbBTEX1.Text = array5[0].ToString("F" + Class49.int_8);
			for (int num19 = 0; num19 < 50; num19++)
			{
				short num20 = 0;
				array = new float[1] { array5[num19] };
				Buffer.BlockCopy(array, 0, array2, 0, 4);
				cdlMgr.tcpServerMgr.mComModbus.WordVaue[104 + num19 * 10] = array2[0];
				cdlMgr.tcpServerMgr.mComModbus.WordVaue[105 + num19 * 10] = array2[1];
				if (7 + num19 * 2 < frmParam.iChannelACnt * 2 + 7)
				{
					switch (num19)
					{
					case 0:
						num20 = (short)((array5[num19] - frmParam.fmount41) / (frmParam.fmount201 - frmParam.fmount41) * 4095f);
						break;
					case 1:
						num20 = (short)((array5[num19] - frmParam.fmount42) / (frmParam.fmount202 - frmParam.fmount42) * 4095f);
						break;
					case 2:
						num20 = (short)((array5[num19] - frmParam.fmount43) / (frmParam.fmount203 - frmParam.fmount43) * 4095f);
						break;
					case 3:
						num20 = (short)((array5[num19] - frmParam.fmount44) / (frmParam.fmount204 - frmParam.fmount44) * 4095f);
						break;
					case 4:
						num20 = (short)((array5[num19] - frmParam.fmount45) / (frmParam.fmount205 - frmParam.fmount45) * 4095f);
						break;
					case 5:
						num20 = (short)((array5[num19] - frmParam.fmount46) / (frmParam.fmount206 - frmParam.fmount46) * 4095f);
						break;
					case 6:
						num20 = (short)((array5[num19] - frmParam.fmount47) / (frmParam.fmount207 - frmParam.fmount47) * 4095f);
						break;
					case 7:
						num20 = (short)((array5[num19] - frmParam.fmount48) / (frmParam.fmount208 - frmParam.fmount48) * 4095f);
						break;
					case 8:
						num20 = (short)((array5[num19] - frmParam.fmount49) / (frmParam.fmount209 - frmParam.fmount49) * 4095f);
						break;
					case 9:
						num20 = (short)((array5[num19] - frmParam.fmount410) / (frmParam.fmount2010 - frmParam.fmount410) * 4095f);
						break;
					case 10:
						num20 = (short)((array5[num19] - frmParam.fmount411) / (frmParam.fmount2011 - frmParam.fmount411) * 4095f);
						break;
					case 11:
						num20 = (short)((array5[num19] - frmParam.fmount412) / (frmParam.fmount2012 - frmParam.fmount412) * 4095f);
						break;
					}
					if (num20 < 0)
					{
						num20 = 0;
					}
					if (num20 > 4095)
					{
						num20 = 4095;
					}
					serialPoartBase.Data2[7 + num19 * 2] = (byte)(num20 >> 8);
					serialPoartBase.Data2[8 + num19 * 2] = (byte)num20;
				}
			}
			Class49.InsertIntoVoc(20, 0, null, fileName.ToLower(), array5[0] + array5[1] + array5[2] + array5[3] + array5[4] + array5[5] + array5[6] + array5[7] + array5[8]);
			string fileName3 = Path.GetFileName(cdlMgr.ChartParaOperaList[0].mtdMgr.chromInfo.cclCalibration);
			amountStr1[20] = currentFlowPath.ToString();
			try
			{
				Class49.InsertIntoHISDATASql(2, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "A流路" + currentFlowPath, fileName3, "A", fileName, amountStr1);
			}
			catch
			{
			}
			if (cdlMgr.formMain.IsAutoCalibra == 1 && indexGnl >= caliGnl5.cmpds.Count())
			{
				cdlMgr.formMain.IsAutoCalibra = 0;
				if (b2 >= caliGnl5.cmpds.Count())
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
		case 1:
		{
			float num4 = 0f;
			CaliGnl caliGnl4 = new CaliGnl();
			caliGnl4 = cdlMgr.ChartParaOperaList[1].mtdMgr.caliGnl;
			float[] array4 = new float[100];
			if (tabRltCtrl.selfCtrl != null)
			{
				MethodInvoker method = delegate
				{
					tabRltCtrl.selfCtrl.tbBen1.Text = "0.00";
					tabRltCtrl.selfCtrl.tbBen2.Text = "0.00";
					tabRltCtrl.selfCtrl.tbBen3.Text = "0.00";
					tabRltCtrl.selfCtrl.tbBen4.Text = "0.00";
					tabRltCtrl.selfCtrl.tbBen5.Text = "0.00";
				};
				Invoke(method);
			}
			array = new float[1];
			Buffer.BlockCopy(array, 0, array2, 0, 4);
			for (int num5 = 0; num5 < 80; num5++)
			{
				cdlMgr.tcpServerMgr.mComModbus.WordVaue[2000 + num5 * 10] = array2[0];
				cdlMgr.tcpServerMgr.mComModbus.WordVaue[2001 + num5 * 10] = array2[1];
				cdlMgr.tcpServerMgr.mComModbus.WordVaue[2002 + num5 * 10] = array2[0];
				cdlMgr.tcpServerMgr.mComModbus.WordVaue[2003 + num5 * 10] = array2[1];
				cdlMgr.tcpServerMgr.mComModbus.WordVaue[2004 + num5 * 10] = array2[0];
				cdlMgr.tcpServerMgr.mComModbus.WordVaue[2005 + num5 * 10] = array2[1];
				cdlMgr.tcpServerMgr.mComModbus.WordVaue[2006 + num5 * 10] = array2[0];
				cdlMgr.tcpServerMgr.mComModbus.WordVaue[2007 + num5 * 10] = array2[1];
				if (num5 < 15 && FormOnline.selfCtrl != null)
				{
					if (currentFlowPath == 1)
					{
						FormOnline.selfCtrl.lstSource1[num5].setV = num4.ToString("F" + Class49.int_8);
					}
					else if (currentFlowPath == 2)
					{
						FormOnline.selfCtrl.lstSource2[num5].setV = num4.ToString("F" + Class49.int_8);
					}
					else if (currentFlowPath == 3)
					{
						FormOnline.selfCtrl.lstSource3[num5].setV = num4.ToString("F" + Class49.int_8);
					}
					else if (currentFlowPath == 4)
					{
						FormOnline.selfCtrl.lstSource4[num5].setV = num4.ToString("F" + Class49.int_8);
					}
				}
			}
			if (FormOnline.selfCtrl != null)
			{
				if (currentFlowPath == 1)
				{
					for (int num6 = 0; num6 < FormOnline.selfCtrl.lstBSource1.Count; num6++)
					{
						FormOnline.selfCtrl.lstBSource1[num6].setV = "0";
					}
				}
				else if (currentFlowPath == 2)
				{
					for (int num7 = 0; num7 < FormOnline.selfCtrl.lstBSource2.Count; num7++)
					{
						FormOnline.selfCtrl.lstBSource2[num7].setV = "0";
					}
				}
				else if (currentFlowPath == 3)
				{
					for (int num8 = 0; num8 < FormOnline.selfCtrl.lstBSource3.Count; num8++)
					{
						FormOnline.selfCtrl.lstBSource3[num8].setV = "0";
					}
				}
				else if (currentFlowPath == 4)
				{
					for (int num9 = 0; num9 < FormOnline.selfCtrl.lstBSource4.Count; num9++)
					{
						FormOnline.selfCtrl.lstBSource4[num9].setV = "0";
					}
				}
			}
			for (indexGnl = 0; indexGnl < caliGnl4.cmpds.Count(); indexGnl++)
			{
				index = 0;
				while (1 <= peak.Count() && index < peak.Count())
				{
					if (peak[index].pkRT >= caliGnl4.cmpds[indexGnl].cmpdInfo.retainTime - caliGnl4.cmpds[indexGnl].cmpdInfo.leftWindow && peak[index].pkRT <= caliGnl4.cmpds[indexGnl].cmpdInfo.retainTime + caliGnl4.cmpds[indexGnl].cmpdInfo.rightWindow && !(peak[index].name != caliGnl4.cmpds[indexGnl].cmpdInfo.name) && peak[index].height >= num2)
					{
						if (cdlMgr.formMain.IsAutoCalibra == 2)
						{
							b2++;
							caliGnl4.cmpds[indexGnl].levels[0].responseA = peak[index].area;
							caliGnl4.CalculateFunc(appendLink: false);
							caliGnl4.cmpds[indexGnl].levels[0].respFactor = peak[index].GasAmount / peak[index].area;
							cdlMgr.ChartParaOperaList[0].mtdMgr.SaveToFile();
							cdlMgr.formMain.MainmstSet.ReadFromMtdMgr();
							array4[indexGnl] = (peak[index].amount = peak[index].area * caliGnl4.cmpds[indexGnl].levels[0].respFactor);
							cdlMgr.ChartParaOperaList[1].mtdMgr.SaveToFile();
							cdlMgr.formMain.MainmstSet.UsePara();
							cdlMgr.formMain.MainmstSet.ReadFromMtdMgr();
							cdlMgr.formMain.MainmstSet.bUseSet_Click(null, null);
						}
						else
						{
							array4[indexGnl] = peak[index].amount;
							array = new float[1] { array4[indexGnl] };
							Buffer.BlockCopy(array, 0, array2, 0, 4);
							cdlMgr.tcpServerMgr.mComModbus.WordVaue[2000 + indexGnl * 10] = array2[0];
							cdlMgr.tcpServerMgr.mComModbus.WordVaue[2001 + indexGnl * 10] = array2[1];
							array[0] = peak[index].areaPer;
							Buffer.BlockCopy(array, 0, array2, 0, 4);
							cdlMgr.tcpServerMgr.mComModbus.WordVaue[2002 + indexGnl * 10] = array2[0];
							cdlMgr.tcpServerMgr.mComModbus.WordVaue[2003 + indexGnl * 10] = array2[1];
							array[0] = peak[index].amount;
							Buffer.BlockCopy(array, 0, array2, 0, 4);
							cdlMgr.tcpServerMgr.mComModbus.WordVaue[2004 + indexGnl * 10] = array2[0];
							cdlMgr.tcpServerMgr.mComModbus.WordVaue[2005 + indexGnl * 10] = array2[1];
							array[0] = peak[index].amountPer;
							Buffer.BlockCopy(array, 0, array2, 0, 4);
							cdlMgr.tcpServerMgr.mComModbus.WordVaue[2006 + indexGnl * 10] = array2[0];
							cdlMgr.tcpServerMgr.mComModbus.WordVaue[2007 + indexGnl * 10] = array2[1];
							if (tabRltCtrl.selfCtrl != null)
							{
								MethodInvoker method2 = delegate
								{
									switch (indexGnl)
									{
									case 0:
										tabRltCtrl.selfCtrl.tbBen1.Text = peak[index].amount.ToString("0.00");
										break;
									case 1:
										tabRltCtrl.selfCtrl.tbBen2.Text = peak[index].amount.ToString("0.00");
										break;
									case 2:
										tabRltCtrl.selfCtrl.tbBen3.Text = peak[index].amount.ToString("0.00");
										break;
									case 3:
										tabRltCtrl.selfCtrl.tbBen4.Text = peak[index].amount.ToString("0.00");
										break;
									case 4:
										tabRltCtrl.selfCtrl.tbBen5.Text = peak[index].amount.ToString("0.00");
										break;
									}
								};
								Invoke(method2);
							}
							ushort num10 = (frmParam.iChannelACnt + indexGnl) switch
							{
								0 => (ushort)((peak[index].amount - frmParam.fmount41) / (frmParam.fmount201 - frmParam.fmount41) * 4095f), 
								1 => (ushort)((peak[index].amount - frmParam.fmount42) / (frmParam.fmount202 - frmParam.fmount42) * 4095f), 
								2 => (ushort)((peak[index].amount - frmParam.fmount43) / (frmParam.fmount203 - frmParam.fmount43) * 4095f), 
								3 => (ushort)((peak[index].amount - frmParam.fmount44) / (frmParam.fmount204 - frmParam.fmount44) * 4095f), 
								4 => (ushort)((peak[index].amount - frmParam.fmount45) / (frmParam.fmount205 - frmParam.fmount45) * 4095f), 
								5 => (ushort)((peak[index].amount - frmParam.fmount46) / (frmParam.fmount206 - frmParam.fmount46) * 4095f), 
								6 => (ushort)((peak[index].amount - frmParam.fmount47) / (frmParam.fmount207 - frmParam.fmount47) * 4095f), 
								7 => (ushort)((peak[index].amount - frmParam.fmount48) / (frmParam.fmount208 - frmParam.fmount48) * 4095f), 
								8 => (ushort)((peak[index].amount - frmParam.fmount49) / (frmParam.fmount209 - frmParam.fmount49) * 4095f), 
								9 => (ushort)((peak[index].amount - frmParam.fmount410) / (frmParam.fmount2010 - frmParam.fmount410) * 4095f), 
								10 => (ushort)((peak[index].amount - frmParam.fmount411) / (frmParam.fmount2011 - frmParam.fmount411) * 4095f), 
								11 => (ushort)((peak[index].amount - frmParam.fmount412) / (frmParam.fmount2012 - frmParam.fmount412) * 4095f), 
								_ => 0, 
							};
							if (num10 > 4095)
							{
								num10 = 4095;
							}
							if (num10 < 0)
							{
								num10 = 0;
							}
							try
							{
								serialPoartBase.Data2[7 + frmParam.iChannelACnt * 2 + indexGnl * 2] = (byte)(num10 >> 8);
								serialPoartBase.Data2[8 + frmParam.iChannelACnt * 2 + indexGnl * 2] = (byte)num10;
							}
							catch (Exception)
							{
							}
							num4 = ((caliGnl4.cmpds[indexGnl].eFunc.curveFit != CurveFit.Free) ? peak[index].amount : (peak[index].amountPer * 100f));
							if (num4 < 0f)
							{
								num4 = 0f;
							}
							amountStr1[indexGnl] = num4.ToString("F" + Class49.int_8);
							if (FormOnline.selfCtrl != null)
							{
								if (currentFlowPath == 1)
								{
									FormOnline.selfCtrl.lstBSource1[indexGnl].setV = num4.ToString("F" + Class49.int_8);
								}
								else if (currentFlowPath == 2)
								{
									FormOnline.selfCtrl.lstBSource2[indexGnl].setV = num4.ToString("F" + Class49.int_8);
								}
								else if (currentFlowPath == 3)
								{
									FormOnline.selfCtrl.lstSource3[indexGnl].setV = num4.ToString("F" + Class49.int_8);
								}
								else if (currentFlowPath == 4)
								{
									FormOnline.selfCtrl.lstBSource4[indexGnl].setV = num4.ToString("F" + Class49.int_8);
								}
							}
						}
					}
					index++;
				}
			}
			string fileName2 = Path.GetFileName(cdlMgr.ChartParaOperaList[1].mtdMgr.chromInfo.cclCalibration);
			amountStr1[20] = currentFlowPath.ToString();
			Class49.InsertIntoHISDATASql(2, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "B流路" + currentFlowPath, fileName2, "B", fileName, amountStr1);
			break;
		}
		}
		int num21 = 0;
		int num22 = 0;
		for (; num21 < 4000; num21++)
		{
			cdlMgr.tcpServerMgr.modBusData_0.ModBusBytes[num22++] = (byte)(cdlMgr.tcpServerMgr.mComModbus.WordVaue[num21] / 256);
			cdlMgr.tcpServerMgr.modBusData_0.ModBusBytes[num22++] = (byte)(cdlMgr.tcpServerMgr.mComModbus.WordVaue[num21] % 256);
		}
		if (StateYiqi != 6)
		{
			StateYiqi = 5;
		}
		ushort[] dst = new ushort[2];
		if (chbCalibra.Checked)
		{
			StateYiqi = 9;
		}
		else
		{
			StateYiqi = 0;
		}
		long[] src = new long[1] { CountAnalyse * 10 + StateYiqi };
		Buffer.BlockCopy(src, 0, dst, 0, 4);
		cdlMgr.CurrentChartParaOpera.mtdMgr.caliGnl = caliGnl3;
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
					string value = num2.ToString("F" + Class49.int_8) + text;
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
		}
		if (FormKR.selfCtrl != null)
		{
			FormKR.selfCtrl.Show();
			FormKR.selfCtrl.Activate();
		}
		if (FormOnline.selfCtrl != null)
		{
			FormOnline.selfCtrl.Show();
			FormOnline.selfCtrl.Activate();
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
				MessageBox.Show("样品正在采集，请等待样品结束！");
				return;
			}
		}
		else if (cdlMgr.formMain.tabChannel.TabCount == 1 && cdlMgr.CurrentTcpServerSocket != null && cdlMgr.CurrentTcpServerSocket.sglsSampling[0].simple)
		{
			MessageBox.Show("样品正在采集，请等待样品结束！");
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
		FormAreaPlot formAreaPlot = new FormAreaPlot(1);
		formAreaPlot.StartPosition = FormStartPosition.CenterScreen;
		formAreaPlot.Show();
		formAreaPlot.loadData();
	}

	private void lbBTEX2_Click(object sender, EventArgs e)
	{
		FormAreaPlot formAreaPlot = new FormAreaPlot(2);
		formAreaPlot.StartPosition = FormStartPosition.CenterScreen;
		formAreaPlot.Show();
		formAreaPlot.loadData();
	}

	private void lbBTEX3_Click(object sender, EventArgs e)
	{
		FormAreaPlot formAreaPlot = new FormAreaPlot(3);
		formAreaPlot.StartPosition = FormStartPosition.CenterScreen;
		formAreaPlot.Show();
		formAreaPlot.loadData();
	}

	private void lbBTEX4_Click(object sender, EventArgs e)
	{
		FormAreaPlot formAreaPlot = new FormAreaPlot(4);
		formAreaPlot.StartPosition = FormStartPosition.CenterScreen;
		formAreaPlot.Show();
		formAreaPlot.loadData();
	}

	private void lbBTEX5_Click(object sender, EventArgs e)
	{
		FormAreaPlot formAreaPlot = new FormAreaPlot(5);
		formAreaPlot.StartPosition = FormStartPosition.CenterScreen;
		formAreaPlot.Show();
		formAreaPlot.loadData();
	}

	private void lbBTEX6_Click(object sender, EventArgs e)
	{
		FormAreaPlot formAreaPlot = new FormAreaPlot(6);
		formAreaPlot.StartPosition = FormStartPosition.CenterScreen;
		formAreaPlot.Show();
		formAreaPlot.loadData();
	}

	private void lbBTEX7_Click(object sender, EventArgs e)
	{
		FormAreaPlot formAreaPlot = new FormAreaPlot(7);
		formAreaPlot.StartPosition = FormStartPosition.CenterScreen;
		formAreaPlot.Show();
		formAreaPlot.loadData();
	}

	private void lbBTEX8_Click(object sender, EventArgs e)
	{
		FormAreaPlot formAreaPlot = new FormAreaPlot(8);
		formAreaPlot.StartPosition = FormStartPosition.CenterScreen;
		formAreaPlot.Show();
		formAreaPlot.loadData();
	}

	private void lbBTEX9_Click(object sender, EventArgs e)
	{
		FormAreaPlot formAreaPlot = new FormAreaPlot(9);
		formAreaPlot.StartPosition = FormStartPosition.CenterScreen;
		formAreaPlot.Show();
		formAreaPlot.loadData();
	}

	private void lbBTEX_Click(object sender, EventArgs e)
	{
		FormAreaPlot formAreaPlot = new FormAreaPlot(10);
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
		}
		else if (frmParam.kindMachine == 1)
		{
			gbNHMC1.Visible = true;
			gbBenXW.Visible = false;
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

	public void channelUpdate()
	{
		if (cdlMgr.CurrentTcpServerSocket == null)
		{
			return;
		}
		if (cdlMgr.CurrentTcpServerSocket.Ready)
		{
			byte[] data = new byte[3] { 192, 168, 1 };
			foreach (ChannelTCPClientState client in ChannelTCPserver._clients)
			{
				ChannelTCPserver.Send(client, data);
			}
		}
		else
		{
			byte[] data2 = new byte[3] { 192, 168, 0 };
			foreach (ChannelTCPClientState client2 in ChannelTCPserver._clients)
			{
				ChannelTCPserver.Send(client2, data2);
			}
		}
		if (ChannelTCPserver.dataBuff[0] != 192)
		{
			return;
		}
		switch (ChannelTCPserver.dataBuff[1])
		{
		case 160:
			switch (ChannelTCPserver.dataBuff[2])
			{
			case 1:
				anysisChannel = 1;
				break;
			case 2:
				anysisChannel = 2;
				break;
			case 4:
				anysisChannel = 3;
				break;
			case 8:
				anysisChannel = 4;
				break;
			case 16:
				anysisChannel = 5;
				break;
			case 32:
				anysisChannel = 6;
				break;
			case 64:
				anysisChannel = 7;
				break;
			case 128:
				anysisChannel = 8;
				break;
			case 250:
				ChannelTCPserver.dataBuff[2] = 0;
				if (Class49.user_0.ULevel != User.Level.访问员)
				{
					cdlMgr.CurrentTcpServerSocket.SendCmd(18);
					return;
				}
				MessageBox.Show(Lang.PS("没有启动权限！", "Without permission!"));
				break;
			}
			break;
		case 161:
			channelEnabelState = ChannelTCPserver.dataBuff[2];
			break;
		case 162:
			if (ChannelTCPserver.dataBuff[2] != 1)
			{
			}
			break;
		case 163:
			if (Class49.user_0.ULevel != User.Level.访问员)
			{
				if (ChannelTCPserver.dataBuff[2] == 1)
				{
					cdlMgr.CurrentTcpServerSocket.SendCmd(18);
				}
			}
			else
			{
				MessageBox.Show(Lang.PS("没有启动权限！", "Without permission!"));
			}
			break;
		case 164:
		{
			byte[] array = new byte[4];
			byte[] array2 = new byte[6]
			{
				192,
				164,
				ChannelTCPserver.dataBuff[2],
				ChannelTCPserver.dataBuff[3],
				ChannelTCPserver.dataBuff[4],
				ChannelTCPserver.dataBuff[5]
			};
			float num2 = 0f;
			float[] dst = new float[1];
			Buffer.BlockCopy(new ushort[2]
			{
				(ushort)((ChannelTCPserver.dataBuff[4] << 8) | ChannelTCPserver.dataBuff[5]),
				(ushort)((ChannelTCPserver.dataBuff[2] << 8) | ChannelTCPserver.dataBuff[3])
			}, 0, dst, 0, 4);
			break;
		}
		case 165:
		{
			float[] dst = new float[1];
			Buffer.BlockCopy(new ushort[2]
			{
				(ushort)((ChannelTCPserver.dataBuff[4] << 8) | ChannelTCPserver.dataBuff[5]),
				(ushort)((ChannelTCPserver.dataBuff[2] << 8) | ChannelTCPserver.dataBuff[3])
			}, 0, dst, 0, 4);
			break;
		}
		case 166:
		{
			int num = 0;
			num = (ChannelTCPserver.dataBuff[2] << 8) | ChannelTCPserver.dataBuff[3];
			break;
		}
		case 167:
		{
			int num = (ChannelTCPserver.dataBuff[2] << 8) | ChannelTCPserver.dataBuff[3];
			break;
		}
		case 9:
			if (Class49.user_0.ULevel != User.Level.访问员 && ChannelTCPserver.dataBuff[2] == 1)
			{
				cdlMgr.CurrentTcpServerSocket.SendCmd(19);
			}
			break;
		case 170:
			if (Class49.user_0.ULevel != User.Level.访问员 && ChannelTCPserver.dataBuff[2] == 1)
			{
				cdlMgr.CurrentTcpServerSocket.SendCmd(20);
			}
			break;
		}
		ChannelTCPserver.dataBuff[0] = 0;
		ChannelTCPserver.dataBuff[1] = 0;
		ChannelTCPserver.dataBuff[2] = 0;
		ChannelTCPserver.dataBuff[3] = 0;
	}

	private void MethodOpen1_Click(object sender, EventArgs e)
	{
		OpenFileDialog openFileDialog = new OpenFileDialog();
		openFileDialog.Title = Lang.PS("打开方法", "open method");
		openFileDialog.InitialDirectory = sysParam.strCalDataFileDir;
		openFileDialog.Filter = Lang.PS("方法文件") + "(*.mtd)|*.mtd";
		if (!openFileDialog.CheckPathExists)
		{
			openFileDialog.InitialDirectory = Application.StartupPath;
		}
		if (openFileDialog.ShowDialog() == DialogResult.OK && openFileDialog.FileName != "")
		{
			tbMethName1.Text = openFileDialog.FileName;
			frmParam.strMethodFilePath1 = openFileDialog.FileName;
			frmParam.SaveParam();
		}
	}

	public void AutoReloadMethod(int indexChanel, string methodPath)
	{
		cdlMgr.formMain.tabChannel.SelectedIndex = indexChanel;
		cdlMgr.formMain.MainmstSet.AutoMethodLoad(indexChanel, methodPath);
	}

	private void MethodOpen2_Click(object sender, EventArgs e)
	{
		OpenFileDialog openFileDialog = new OpenFileDialog();
		openFileDialog.Title = Lang.PS("打开方法", "open method");
		openFileDialog.InitialDirectory = sysParam.strCalDataFileDir;
		openFileDialog.Filter = Lang.PS("方法文件") + "(*.mtd)|*.mtd";
		if (!openFileDialog.CheckPathExists)
		{
			openFileDialog.InitialDirectory = Application.StartupPath;
		}
		if (openFileDialog.ShowDialog() == DialogResult.OK && openFileDialog.FileName != "")
		{
			tbMethName2.Text = openFileDialog.FileName;
			frmParam.strMethodFilePath2 = openFileDialog.FileName;
			frmParam.SaveParam();
		}
	}

	private void MethodOpen3_Click(object sender, EventArgs e)
	{
		OpenFileDialog openFileDialog = new OpenFileDialog();
		openFileDialog.Title = Lang.PS("打开方法", "open method");
		openFileDialog.InitialDirectory = sysParam.strCalDataFileDir;
		openFileDialog.Filter = Lang.PS("方法文件") + "(*.mtd)|*.mtd";
		if (!openFileDialog.CheckPathExists)
		{
			openFileDialog.InitialDirectory = Application.StartupPath;
		}
		if (openFileDialog.ShowDialog() == DialogResult.OK && openFileDialog.FileName != "")
		{
			tbMethName3.Text = openFileDialog.FileName;
			frmParam.strMethodFilePath3 = tbMethName3.Text;
			frmParam.SaveParam();
		}
	}

	private void BtnSave_Click(object sender, EventArgs e)
	{
		frmParam.strMethodFilePath1 = tbMethName1.Text;
		frmParam.strMethodFilePath2 = tbMethName2.Text;
		frmParam.strMethodFilePath3 = tbMethName3.Text;
		frmParam.fInjecTime1 = float.Parse(tbInjecTime1.Text);
		frmParam.fInjecTime2 = float.Parse(tbInjecTime2.Text);
		frmParam.fInjecTime3 = float.Parse(tbInjecTime3.Text);
		frmParam.fInjecDelay = float.Parse(tbDelay.Text);
		frmParam.bChbChannel1 = chbChannel1.Checked;
		frmParam.bChbChannel2 = chbChannel2.Checked;
		frmParam.bChbChannel3 = chbChannel3.Checked;
		frmParam.iTbCycleTimes = int.Parse(tbCycleTimes.Text);
		frmParam.strName1 = tbName1.Text;
		frmParam.strName2 = tbName2.Text;
		frmParam.strName3 = tbName3.Text;
		frmParam.fTimeInterval = float.Parse(tbTimeInterval.Text);
		frmParam.SaveParam();
	}

	public void changeChannel(int index)
	{
		switch (index)
		{
		case 1:
			AutoReloadMethod(0, tbMethName1.Text);
			cdlMgr.CurrentTcpServerSocket.dtc_Channels[0].chromInfoR.AcqRunTime = frmParam.fInjecTime1;
			cdlMgr.CurrentTcpServerSocket.dtc_Channels[1].chromInfoR.AcqRunTime = frmParam.fInjecTime1;
			cdlMgr.ChannelChartParaList[0].stopTime = frmParam.fInjecTime1;
			cdlMgr.ChannelChartParaList[1].stopTime = frmParam.fInjecTime1;
			break;
		case 2:
			AutoReloadMethod(0, tbMethName2.Text);
			cdlMgr.CurrentTcpServerSocket.dtc_Channels[0].chromInfoR.AcqRunTime = frmParam.fInjecTime2;
			cdlMgr.CurrentTcpServerSocket.dtc_Channels[1].chromInfoR.AcqRunTime = frmParam.fInjecTime2;
			cdlMgr.ChannelChartParaList[0].stopTime = frmParam.fInjecTime1;
			cdlMgr.ChannelChartParaList[1].stopTime = frmParam.fInjecTime1;
			break;
		case 3:
			AutoReloadMethod(0, tbMethName3.Text);
			cdlMgr.CurrentTcpServerSocket.dtc_Channels[0].chromInfoR.AcqRunTime = frmParam.fInjecTime3;
			cdlMgr.CurrentTcpServerSocket.dtc_Channels[1].chromInfoR.AcqRunTime = frmParam.fInjecTime3;
			cdlMgr.ChannelChartParaList[0].stopTime = frmParam.fInjecTime1;
			cdlMgr.ChannelChartParaList[1].stopTime = frmParam.fInjecTime1;
			break;
		}
	}

	private void BtnStart_Click(object sender, EventArgs e)
	{
		if (!chbChannel1.Checked && !chbChannel2.Checked && !chbChannel3.Checked)
		{
			MessageBox.Show(Lang.PS("请至少选中一个流路参与循环", "Please select at least one flow to participate in the cycle"));
			return;
		}
		if (btnStart.Text == "开始循环")
		{
			cntAnalyseTime = 0;
			btnStart.Text = "结束循环";
			timer1.Start();
			bAnalyse = true;
		}
		else if (btnStart.Text == "结束循环")
		{
			cntAnalyseTime = 0;
			btnStart.Text = "开始循环";
			timer1.Stop();
			cdlMgr.currentTcpServerMgrSendCmd(19);
			bAnalyse = false;
			tbInjecTime1.BackColor = SystemColors.Window;
			tbInjecTime2.BackColor = SystemColors.Window;
			tbInjecTime3.BackColor = SystemColors.Window;
		}
		if (chbChannel1.Checked)
		{
			indexChannel = 1;
			changeChannel(1);
		}
		else if (chbChannel2.Checked)
		{
			indexChannel = 2;
			changeChannel(2);
		}
		else if (chbChannel3.Checked)
		{
			indexChannel = 3;
			changeChannel(3);
		}
	}

	public void onlineCtrl()
	{
		if (bAnalyse)
		{
			cntAnalyseTime++;
			if (indexChannel == 1)
			{
				if (cntAnalyseTime == 1)
				{
					cdlMgr.currentTcpServerMgrSendEPCCmd(84, 1);
				}
				if (cntAnalyseTime == (int)(frmParam.fInjecDelay * 60f))
				{
					cdlMgr.currentTcpServerMgrSendCmd(18);
					indexChannelStart = 1;
				}
				tbInjecTime1.BackColor = Color.Red;
				tbInjecTime2.BackColor = SystemColors.Window;
				tbInjecTime3.BackColor = SystemColors.Window;
				if ((float)cntAnalyseTime > frmParam.fInjecTime1 * 60f + frmParam.fInjecDelay * 60f + 60f)
				{
					tbInjecTime1.BackColor = SystemColors.Window;
					if (chbChannel2.Checked)
					{
						cntAnalyseTime = 0;
						indexChannel = 2;
						changeChannel(2);
					}
					else if (chbChannel3.Checked)
					{
						cntAnalyseTime = 0;
						indexChannel = 3;
						changeChannel(3);
					}
					else if (chbChannel1.Checked && (float)cntAnalyseTime > frmParam.fInjecTime1 * 60f + frmParam.fInjecDelay * 60f + frmParam.fTimeInterval * 60f + 60f)
					{
						cntAnalyseTime = 0;
						indexChannel = 1;
						changeChannel(1);
					}
				}
			}
			else if (indexChannel == 2)
			{
				if (cntAnalyseTime == 1)
				{
					cdlMgr.currentTcpServerMgrSendEPCCmd(84, 2);
				}
				if (cntAnalyseTime == (int)(frmParam.fInjecDelay * 60f))
				{
					cdlMgr.currentTcpServerMgrSendCmd(18);
					indexChannelStart = 2;
				}
				tbInjecTime2.BackColor = Color.Red;
				tbInjecTime1.BackColor = SystemColors.Window;
				tbInjecTime3.BackColor = SystemColors.Window;
				if (!((float)cntAnalyseTime > frmParam.fInjecTime2 * 60f + frmParam.fInjecDelay * 60f + 60f))
				{
					return;
				}
				tbInjecTime2.BackColor = SystemColors.Window;
				if (chbChannel3.Checked)
				{
					cntAnalyseTime = 0;
					indexChannel = 3;
					changeChannel(3);
				}
				else if (chbChannel1.Checked)
				{
					if ((float)cntAnalyseTime > frmParam.fInjecTime1 * 60f + frmParam.fInjecDelay * 60f + frmParam.fTimeInterval * 60f + 60f)
					{
						cntAnalyseTime = 0;
						indexChannel = 1;
						changeChannel(1);
					}
				}
				else if (chbChannel2.Checked && (float)cntAnalyseTime > frmParam.fInjecTime1 * 60f + frmParam.fInjecDelay * 60f + frmParam.fTimeInterval * 60f + 60f)
				{
					cntAnalyseTime = 0;
					indexChannel = 2;
					changeChannel(2);
				}
			}
			else
			{
				if (indexChannel != 3)
				{
					return;
				}
				if (cntAnalyseTime == 1)
				{
					cdlMgr.currentTcpServerMgrSendEPCCmd(84, 4);
				}
				if (cntAnalyseTime == (int)(frmParam.fInjecDelay * 60f))
				{
					cdlMgr.currentTcpServerMgrSendCmd(18);
					indexChannelStart = 3;
				}
				tbInjecTime3.BackColor = Color.Red;
				tbInjecTime1.BackColor = SystemColors.Window;
				tbInjecTime2.BackColor = SystemColors.Window;
				if (!((float)cntAnalyseTime > frmParam.fInjecTime3 * 60f + frmParam.fInjecDelay * 60f + 60f))
				{
					return;
				}
				tbInjecTime3.BackColor = SystemColors.Window;
				if (chbChannel1.Checked)
				{
					if ((float)cntAnalyseTime > frmParam.fInjecTime1 * 60f + frmParam.fInjecDelay * 60f + frmParam.fTimeInterval * 60f + 60f)
					{
						cntAnalyseTime = 0;
						indexChannel = 1;
						changeChannel(1);
					}
				}
				else if (chbChannel2.Checked)
				{
					if ((float)cntAnalyseTime > frmParam.fInjecTime1 * 60f + frmParam.fInjecDelay * 60f + frmParam.fTimeInterval * 60f + 60f)
					{
						cntAnalyseTime = 0;
						indexChannel = 2;
						changeChannel(2);
					}
				}
				else if (chbChannel3.Checked && (float)cntAnalyseTime > frmParam.fInjecTime1 * 60f + frmParam.fInjecDelay * 60f + frmParam.fTimeInterval * 60f + 60f)
				{
					cntAnalyseTime = 0;
					indexChannel = 3;
					changeChannel(3);
				}
			}
		}
		else
		{
			cntAnalyseTime = 0;
			cntAnalyseTimes = 0;
		}
	}

	private void BtnStartCycle_Click(object sender, EventArgs e)
	{
		if (btnStartCycle.Text == "开始循环")
		{
			cntCycle = 1;
			bStartAnalyze = true;
			btnStartCycle.Text = "停止循环";
			iChannelIndex = 1;
			cntAanlyzeTime = 0;
			bSendAnalyze = false;
			timer1.Start();
		}
		else
		{
			timer1.Stop();
			cntCycle = 0;
			bStartAnalyze = false;
			btnStartCycle.Text = "开始循环";
			iChannelIndex = 0;
			bSendAnalyze = false;
			cntAanlyzeTime = 0;
			updateBackColor(0);
			updateSerialPortDate(0, 0);
		}
	}

	private void BtnSave1_Click(object sender, EventArgs e)
	{
		onLineCtrlParam.iTimes1 = cbTimes1.SelectedIndex;
		onLineCtrlParam.iTimes2 = cbTimes2.SelectedIndex;
		onLineCtrlParam.iTimes3 = cbTimes3.SelectedIndex;
		onLineCtrlParam.iTimes4 = cbTimes4.SelectedIndex;
		onLineCtrlParam.iTimes5 = cbTimes5.SelectedIndex;
		onLineCtrlParam.iTimes6 = cbTimes6.SelectedIndex;
		onLineCtrlParam.iTimes7 = cbTimes7.SelectedIndex;
		onLineCtrlParam.iTimes8 = cbTimes8.SelectedIndex;
		onLineCtrlParam.iTimes9 = cbTimes9.SelectedIndex;
		onLineCtrlParam.iTimes10 = cbTimes10.SelectedIndex;
		onLineCtrlParam.iTimes11 = cbTimes11.SelectedIndex;
		onLineCtrlParam.iTimes12 = cbTimes12.SelectedIndex;
		onLineCtrlParam.iCoerce = cbCoerce.SelectedIndex;
		onLineCtrlParam.iCycles = int.Parse(tbCycle.Text);
		onLineCtrlParam.fCycleTime = float.Parse(tbCycleTime3.Text);
		onLineCtrlParam.fStartTime = float.Parse(tbStartTime.Text);
		onLineCtrlParam.SaveParam();
	}

	public void updateSerialPortDate(byte index, byte value)
	{
		serialPoartBase.Data[12] = 0;
		index = (byte)(11 - index);
		for (int i = 4; i < 12; i++)
		{
			if (i == index)
			{
				serialPoartBase.Data[index] = value;
			}
			else
			{
				serialPoartBase.Data[i] = 0;
			}
		}
	}

	public void updateBackColor(int index)
	{
		switch (index)
		{
		case 0:
			cbTimes1.BackColor = SystemColors.Window;
			cbTimes2.BackColor = SystemColors.Window;
			cbTimes3.BackColor = SystemColors.Window;
			cbTimes4.BackColor = SystemColors.Window;
			cbTimes5.BackColor = SystemColors.Window;
			cbTimes6.BackColor = SystemColors.Window;
			cbTimes7.BackColor = SystemColors.Window;
			cbTimes8.BackColor = SystemColors.Window;
			cbTimes9.BackColor = SystemColors.Window;
			cbTimes10.BackColor = SystemColors.Window;
			cbTimes11.BackColor = SystemColors.Window;
			cbTimes12.BackColor = SystemColors.Window;
			cbCoerce.BackColor = SystemColors.Window;
			break;
		case 1:
			cbTimes1.BackColor = Color.Red;
			cbTimes2.BackColor = SystemColors.Window;
			cbTimes3.BackColor = SystemColors.Window;
			cbTimes4.BackColor = SystemColors.Window;
			cbTimes5.BackColor = SystemColors.Window;
			cbTimes6.BackColor = SystemColors.Window;
			cbTimes7.BackColor = SystemColors.Window;
			cbTimes8.BackColor = SystemColors.Window;
			cbTimes9.BackColor = SystemColors.Window;
			cbTimes10.BackColor = SystemColors.Window;
			cbTimes11.BackColor = SystemColors.Window;
			cbTimes12.BackColor = SystemColors.Window;
			cbCoerce.BackColor = SystemColors.Window;
			break;
		case 2:
			cbTimes1.BackColor = SystemColors.Window;
			cbTimes2.BackColor = Color.Red;
			cbTimes3.BackColor = SystemColors.Window;
			cbTimes4.BackColor = SystemColors.Window;
			cbTimes5.BackColor = SystemColors.Window;
			cbTimes6.BackColor = SystemColors.Window;
			cbTimes7.BackColor = SystemColors.Window;
			cbTimes8.BackColor = SystemColors.Window;
			cbTimes9.BackColor = SystemColors.Window;
			cbTimes10.BackColor = SystemColors.Window;
			cbTimes11.BackColor = SystemColors.Window;
			cbTimes12.BackColor = SystemColors.Window;
			cbCoerce.BackColor = SystemColors.Window;
			break;
		case 3:
			cbTimes1.BackColor = SystemColors.Window;
			cbTimes2.BackColor = SystemColors.Window;
			cbTimes3.BackColor = Color.Red;
			cbTimes4.BackColor = SystemColors.Window;
			cbTimes5.BackColor = SystemColors.Window;
			cbTimes6.BackColor = SystemColors.Window;
			cbTimes7.BackColor = SystemColors.Window;
			cbTimes8.BackColor = SystemColors.Window;
			cbTimes9.BackColor = SystemColors.Window;
			cbTimes10.BackColor = SystemColors.Window;
			cbTimes11.BackColor = SystemColors.Window;
			cbTimes12.BackColor = SystemColors.Window;
			cbCoerce.BackColor = SystemColors.Window;
			break;
		case 4:
			cbTimes1.BackColor = SystemColors.Window;
			cbTimes2.BackColor = SystemColors.Window;
			cbTimes3.BackColor = SystemColors.Window;
			cbTimes4.BackColor = Color.Red;
			cbTimes5.BackColor = SystemColors.Window;
			cbTimes6.BackColor = SystemColors.Window;
			cbTimes7.BackColor = SystemColors.Window;
			cbTimes8.BackColor = SystemColors.Window;
			cbTimes9.BackColor = SystemColors.Window;
			cbTimes10.BackColor = SystemColors.Window;
			cbTimes11.BackColor = SystemColors.Window;
			cbTimes12.BackColor = SystemColors.Window;
			cbCoerce.BackColor = SystemColors.Window;
			break;
		case 5:
			cbTimes1.BackColor = SystemColors.Window;
			cbTimes2.BackColor = SystemColors.Window;
			cbTimes3.BackColor = SystemColors.Window;
			cbTimes4.BackColor = SystemColors.Window;
			cbTimes5.BackColor = Color.Red;
			cbTimes6.BackColor = SystemColors.Window;
			cbTimes7.BackColor = SystemColors.Window;
			cbTimes8.BackColor = SystemColors.Window;
			cbTimes9.BackColor = SystemColors.Window;
			cbTimes10.BackColor = SystemColors.Window;
			cbTimes11.BackColor = SystemColors.Window;
			cbTimes12.BackColor = SystemColors.Window;
			cbCoerce.BackColor = SystemColors.Window;
			break;
		case 6:
			cbTimes1.BackColor = SystemColors.Window;
			cbTimes2.BackColor = SystemColors.Window;
			cbTimes3.BackColor = SystemColors.Window;
			cbTimes4.BackColor = SystemColors.Window;
			cbTimes5.BackColor = SystemColors.Window;
			cbTimes6.BackColor = Color.Red;
			cbTimes7.BackColor = SystemColors.Window;
			cbTimes8.BackColor = SystemColors.Window;
			cbTimes9.BackColor = SystemColors.Window;
			cbTimes10.BackColor = SystemColors.Window;
			cbTimes11.BackColor = SystemColors.Window;
			cbTimes12.BackColor = SystemColors.Window;
			cbCoerce.BackColor = SystemColors.Window;
			break;
		case 7:
			cbTimes1.BackColor = SystemColors.Window;
			cbTimes2.BackColor = SystemColors.Window;
			cbTimes3.BackColor = SystemColors.Window;
			cbTimes4.BackColor = SystemColors.Window;
			cbTimes5.BackColor = SystemColors.Window;
			cbTimes6.BackColor = SystemColors.Window;
			cbTimes7.BackColor = Color.Red;
			cbTimes8.BackColor = SystemColors.Window;
			cbTimes9.BackColor = SystemColors.Window;
			cbTimes10.BackColor = SystemColors.Window;
			cbTimes11.BackColor = SystemColors.Window;
			cbTimes12.BackColor = SystemColors.Window;
			cbCoerce.BackColor = SystemColors.Window;
			break;
		case 8:
			cbTimes1.BackColor = SystemColors.Window;
			cbTimes2.BackColor = SystemColors.Window;
			cbTimes3.BackColor = SystemColors.Window;
			cbTimes4.BackColor = SystemColors.Window;
			cbTimes5.BackColor = SystemColors.Window;
			cbTimes6.BackColor = SystemColors.Window;
			cbTimes7.BackColor = SystemColors.Window;
			cbTimes8.BackColor = Color.Red;
			cbTimes9.BackColor = SystemColors.Window;
			cbTimes10.BackColor = SystemColors.Window;
			cbTimes11.BackColor = SystemColors.Window;
			cbTimes12.BackColor = SystemColors.Window;
			cbCoerce.BackColor = SystemColors.Window;
			break;
		case 9:
			cbTimes1.BackColor = SystemColors.Window;
			cbTimes2.BackColor = SystemColors.Window;
			cbTimes3.BackColor = SystemColors.Window;
			cbTimes4.BackColor = SystemColors.Window;
			cbTimes5.BackColor = SystemColors.Window;
			cbTimes6.BackColor = SystemColors.Window;
			cbTimes7.BackColor = SystemColors.Window;
			cbTimes8.BackColor = SystemColors.Window;
			cbTimes9.BackColor = Color.Red;
			cbTimes10.BackColor = SystemColors.Window;
			cbTimes11.BackColor = SystemColors.Window;
			cbTimes12.BackColor = SystemColors.Window;
			cbCoerce.BackColor = SystemColors.Window;
			break;
		case 10:
			cbTimes1.BackColor = SystemColors.Window;
			cbTimes2.BackColor = SystemColors.Window;
			cbTimes3.BackColor = SystemColors.Window;
			cbTimes4.BackColor = SystemColors.Window;
			cbTimes5.BackColor = SystemColors.Window;
			cbTimes6.BackColor = SystemColors.Window;
			cbTimes7.BackColor = SystemColors.Window;
			cbTimes8.BackColor = SystemColors.Window;
			cbTimes9.BackColor = SystemColors.Window;
			cbTimes10.BackColor = Color.Red;
			cbTimes11.BackColor = SystemColors.Window;
			cbTimes12.BackColor = SystemColors.Window;
			cbCoerce.BackColor = SystemColors.Window;
			break;
		case 11:
			cbTimes1.BackColor = SystemColors.Window;
			cbTimes2.BackColor = SystemColors.Window;
			cbTimes3.BackColor = SystemColors.Window;
			cbTimes4.BackColor = SystemColors.Window;
			cbTimes5.BackColor = SystemColors.Window;
			cbTimes6.BackColor = SystemColors.Window;
			cbTimes7.BackColor = SystemColors.Window;
			cbTimes8.BackColor = SystemColors.Window;
			cbTimes9.BackColor = SystemColors.Window;
			cbTimes10.BackColor = SystemColors.Window;
			cbTimes11.BackColor = Color.Red;
			cbTimes12.BackColor = SystemColors.Window;
			cbCoerce.BackColor = SystemColors.Window;
			break;
		case 12:
			cbTimes1.BackColor = SystemColors.Window;
			cbTimes2.BackColor = SystemColors.Window;
			cbTimes3.BackColor = SystemColors.Window;
			cbTimes4.BackColor = SystemColors.Window;
			cbTimes5.BackColor = SystemColors.Window;
			cbTimes6.BackColor = SystemColors.Window;
			cbTimes7.BackColor = SystemColors.Window;
			cbTimes8.BackColor = SystemColors.Window;
			cbTimes9.BackColor = SystemColors.Window;
			cbTimes10.BackColor = SystemColors.Window;
			cbTimes11.BackColor = SystemColors.Window;
			cbTimes12.BackColor = Color.Red;
			cbCoerce.BackColor = SystemColors.Window;
			break;
		case 13:
			cbTimes1.BackColor = SystemColors.Window;
			cbTimes2.BackColor = SystemColors.Window;
			cbTimes3.BackColor = SystemColors.Window;
			cbTimes4.BackColor = SystemColors.Window;
			cbTimes5.BackColor = SystemColors.Window;
			cbTimes6.BackColor = SystemColors.Window;
			cbTimes7.BackColor = SystemColors.Window;
			cbTimes8.BackColor = SystemColors.Window;
			cbTimes9.BackColor = SystemColors.Window;
			cbTimes10.BackColor = SystemColors.Window;
			cbTimes11.BackColor = SystemColors.Window;
			cbTimes12.BackColor = SystemColors.Window;
			cbCoerce.BackColor = Color.Red;
			break;
		default:
			cbTimes1.BackColor = SystemColors.Window;
			cbTimes2.BackColor = SystemColors.Window;
			cbTimes3.BackColor = SystemColors.Window;
			cbTimes4.BackColor = SystemColors.Window;
			cbTimes5.BackColor = SystemColors.Window;
			cbTimes6.BackColor = SystemColors.Window;
			cbTimes7.BackColor = SystemColors.Window;
			cbTimes8.BackColor = SystemColors.Window;
			cbTimes9.BackColor = SystemColors.Window;
			cbTimes10.BackColor = SystemColors.Window;
			cbTimes11.BackColor = SystemColors.Window;
			cbTimes12.BackColor = SystemColors.Window;
			cbCoerce.BackColor = SystemColors.Window;
			break;
		}
	}

	public void updateSerialPortDate(int iChannel)
	{
		switch (iChannel)
		{
		case 0:
			updateSerialPortDate(0, 0);
			break;
		case 1:
			updateSerialPortDate(0, 1);
			break;
		case 2:
			updateSerialPortDate(0, 2);
			break;
		case 3:
			updateSerialPortDate(0, 4);
			break;
		case 4:
			updateSerialPortDate(0, 8);
			break;
		case 5:
			updateSerialPortDate(0, 16);
			break;
		case 6:
			updateSerialPortDate(0, 32);
			break;
		case 7:
			updateSerialPortDate(0, 64);
			break;
		case 8:
			updateSerialPortDate(0, 128);
			break;
		case 9:
			updateSerialPortDate(1, 1);
			break;
		case 10:
			updateSerialPortDate(1, 2);
			break;
		case 11:
			updateSerialPortDate(1, 4);
			break;
		case 12:
			updateSerialPortDate(1, 8);
			break;
		}
	}

	public void updateChannelCase(ref int iCountAnalyse, int indexCh)
	{
		if (!bSendAnalyze && (float)iCountAnalyse > onLineCtrlParam.fStartTime * 60f)
		{
			cdlMgr.currentTcpServerMgrSendCmd(18);
			currentFlowPath = indexCh;
			memoryFlowPath = indexCh;
			bSendAnalyze = true;
		}
		if ((float)iCountAnalyse > onLineCtrlParam.fCycleTime * 60f)
		{
			iChannelIndex++;
			if (iChannelIndex > 12)
			{
				iChannelIndex = 1;
			}
			bSendAnalyze = false;
			cntCycle++;
			iCountAnalyse = 0;
			if (cntCycle > onLineCtrlParam.iCycles && cntCycle < 999)
			{
				timer1.Stop();
				bStartAnalyze = false;
				btnStartCycle.Text = "开始循环";
				iChannelIndex = 0;
				bSendAnalyze = false;
				cntAanlyzeTime = 0;
				updateBackColor(0);
				updateSerialPortDate(0, 0);
			}
		}
	}

	private void updateChannel(int indexChannel)
	{
		updateSerialPortDate(indexChannel);
		updateBackColor(iChannelIndex);
		updateChannelCase(ref cntAanlyzeTime, indexChannel);
	}

	private void Timer1_Tick(object sender, EventArgs e)
	{
		labCntTime.Text = cntAanlyzeTime.ToString();
		labCntTimes.Text = cntCycle.ToString();
		onlineCtrl();
		if (cdlMgr.tcpServerMgr.mComModbus.bReciS)
		{
			cdlMgr.tcpServerMgr.mComModbus.bReciS = false;
			onLineCtrlParam.iCoerce = cdlMgr.tcpServerMgr.mComModbus.WordVaue[8];
			cbCoerce.Text = onLineCtrlParam.iCoerce.ToString();
			if (onLineCtrlParam.iCoerce == 0)
			{
				iChannelIndex = 1;
				cntCycle = 1;
				bStartAnalyze = true;
				btnStartCycle.Text = "停止循环";
				iChannelIndex = 1;
				cntAanlyzeTime = 0;
				bSendAnalyze = false;
			}
		}
		if (!bStartAnalyze)
		{
			return;
		}
		cntAanlyzeTime++;
		if (onLineCtrlParam.iCoerce != 0)
		{
			iChannelIndex = 13;
			updateChannel(onLineCtrlParam.iCoerce);
			return;
		}
		if (iChannelIndex == 1)
		{
			updateChannel(onLineCtrlParam.iTimes1);
			if (onLineCtrlParam.iTimes1 == 0)
			{
				iChannelIndex = 2;
			}
		}
		if (iChannelIndex == 2)
		{
			updateChannel(onLineCtrlParam.iTimes2);
			if (onLineCtrlParam.iTimes2 == 0)
			{
				iChannelIndex = 3;
			}
		}
		if (iChannelIndex == 3)
		{
			updateChannel(onLineCtrlParam.iTimes3);
			if (onLineCtrlParam.iTimes3 == 0)
			{
				iChannelIndex = 4;
			}
		}
		if (iChannelIndex == 4)
		{
			updateChannel(onLineCtrlParam.iTimes4);
			if (onLineCtrlParam.iTimes4 == 0)
			{
				iChannelIndex = 5;
			}
		}
		if (iChannelIndex == 5)
		{
			updateChannel(onLineCtrlParam.iTimes5);
			if (onLineCtrlParam.iTimes5 == 0)
			{
				iChannelIndex = 6;
			}
		}
		if (iChannelIndex == 6)
		{
			updateChannel(onLineCtrlParam.iTimes6);
			if (onLineCtrlParam.iTimes6 == 0)
			{
				iChannelIndex = 7;
			}
		}
		if (iChannelIndex == 7)
		{
			updateChannel(onLineCtrlParam.iTimes7);
			if (onLineCtrlParam.iTimes7 == 0)
			{
				iChannelIndex = 8;
			}
		}
		if (iChannelIndex == 8)
		{
			updateChannel(onLineCtrlParam.iTimes8);
			if (onLineCtrlParam.iTimes8 == 0)
			{
				iChannelIndex = 9;
			}
		}
		if (iChannelIndex == 9)
		{
			updateChannel(onLineCtrlParam.iTimes9);
			if (onLineCtrlParam.iTimes9 == 0)
			{
				iChannelIndex = 10;
			}
		}
		if (iChannelIndex == 10)
		{
			updateChannel(onLineCtrlParam.iTimes10);
			if (onLineCtrlParam.iTimes10 == 0)
			{
				iChannelIndex = 11;
			}
		}
		if (iChannelIndex == 11)
		{
			updateChannel(onLineCtrlParam.iTimes11);
			if (onLineCtrlParam.iTimes11 == 0)
			{
				iChannelIndex = 12;
			}
		}
		if (iChannelIndex == 12)
		{
			updateChannel(onLineCtrlParam.iTimes12);
			if (onLineCtrlParam.iTimes12 == 0)
			{
				iChannelIndex = 1;
			}
		}
	}

	private void btnModbusSave_Click(object sender, EventArgs e)
	{
		if (ushort.TryParse(tbModbusAddress.Text, out frmParam.iModbusAddress))
		{
			frmParam.strDCSCom = cb_portNameSend.Text.Trim();
			int.TryParse(cb_baudRate.Text, out frmParam.iBaudRate);
			int.TryParse(cb_dataBit.Text, out frmParam.iDataBit);
			int.TryParse(cb_stopBit.Text, out frmParam.iStopBit);
			frmParam.SaveParam();
			cdlMgr.tcpServerMgr.mComModbus.DevAdd = frmParam.iModbusAddress;
			cdlMgr.tcpServerMgr.mComModbus.OpenMyCom(frmParam.strDCSCom, frmParam.iBaudRate, frmParam.iDataBit, Parity.None, (StopBits)frmParam.iStopBit);
			MessageBox.Show("设置成功！");
		}
	}

	private void btnSavePeakTime_Click(object sender, EventArgs e)
	{
		if (float.TryParse(tbTimeON1.Text, out frmParam.fTimeOn1) && float.TryParse(tbTimeON2.Text, out frmParam.fTimeOn2) && float.TryParse(tbTimeON3.Text, out frmParam.fTimeOn3) && float.TryParse(tbTimeON4.Text, out frmParam.fTimeOn4) && float.TryParse(tbTimeOFF1.Text, out frmParam.fTimeOff1) && float.TryParse(tbTimeOFF2.Text, out frmParam.fTimeOff2) && float.TryParse(tbTimeOFF3.Text, out frmParam.fTimeOff3) && float.TryParse(tbTimeOFF4.Text, out frmParam.fTimeOff4))
		{
			frmParam.SaveParam();
			MessageBox.Show("设置成功！");
		}
		else
		{
			MessageBox.Show("设置不合法，保存失败！");
		}
	}

	private void cbDectorNumber_SelectedIndexChanged(object sender, EventArgs e)
	{
		frmParam.iDectorNumber = cbDectorNumber.SelectedIndex;
		frmParam.SaveParam();
		if (cbDectorNumber.SelectedIndex == 0)
		{
			label34.Text = "样气";
			label39.Visible = false;
			labPHH2.Visible = false;
			tbPAir2.Visible = false;
			tbPHH2.Visible = false;
			cdlMgr.formMain.insDeviceCtrl.groupBox4.Visible = false;
		}
		else if (cbDectorNumber.SelectedIndex == 1)
		{
			label34.Text = "载气4";
			label39.Visible = true;
			labPHH2.Visible = true;
			tbPAir2.Visible = true;
			tbPHH2.Visible = true;
			cdlMgr.formMain.insDeviceCtrl.groupBox4.Visible = false;
		}
		else if (cbDectorNumber.SelectedIndex == 2)
		{
			label34.Text = "载气4";
			label39.Visible = true;
			labPHH2.Visible = true;
			tbPAir2.Visible = true;
			tbPHH2.Visible = true;
			cdlMgr.formMain.insDeviceCtrl.groupBox4.Visible = true;
		}
	}

	private void btnMonit_Click(object sender, EventArgs e)
	{
		if (FormKR.selfCtrl != null)
		{
			FormKR.selfCtrl.Show();
			FormKR.selfCtrl.Activate();
			FormKR.selfCtrl.WindowState = FormWindowState.Maximized;
		}
	}

	private void timer2_Tick(object sender, EventArgs e)
	{
	}

	private void btnEPCSet_Click(object sender, EventArgs e)
	{
		FormEPC formEPC = new FormEPC();
		formEPC.StartPosition = FormStartPosition.CenterScreen;
		formEPC.Show();
	}

	private void tbModbusAddress_TextChanged(object sender, EventArgs e)
	{
	}

	public void initForm()
	{
	}

	private void btnConver_Click(object sender, EventArgs e)
	{
		Process.Start("总烃换算.exe");
	}

	private void OnlineCtrl_Load(object sender, EventArgs e)
	{
		chbCombinDector.Checked = frmParam.bTwoDector;
		bLoading = false;
	}

	private void chbCombinDector_CheckedChanged(object sender, EventArgs e)
	{
		if (!bLoading)
		{
			frmParam.bTwoDector = chbCombinDector.Checked;
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
		this.panel1 = new System.Windows.Forms.Panel();
		this.btnDevice = new System.Windows.Forms.Button();
		this.btnNetConfig = new System.Windows.Forms.Button();
		this.cbKindMachine = new System.Windows.Forms.ComboBox();
		this.btnCali = new System.Windows.Forms.Button();
		this.gbNHMC1 = new System.Windows.Forms.GroupBox();
		this.btnStart = new System.Windows.Forms.Button();
		this.btnSave = new System.Windows.Forms.Button();
		this.label7 = new System.Windows.Forms.Label();
		this.tbComTimes = new System.Windows.Forms.TextBox();
		this.label8 = new System.Windows.Forms.Label();
		this.tbCycleTimes = new System.Windows.Forms.TextBox();
		this.chbChannel3 = new System.Windows.Forms.CheckBox();
		this.chbChannel2 = new System.Windows.Forms.CheckBox();
		this.chbChannel1 = new System.Windows.Forms.CheckBox();
		this.label6 = new System.Windows.Forms.Label();
		this.tbInjecTime3 = new System.Windows.Forms.TextBox();
		this.label5 = new System.Windows.Forms.Label();
		this.tbInjecTime2 = new System.Windows.Forms.TextBox();
		this.label4 = new System.Windows.Forms.Label();
		this.tbInjecTime1 = new System.Windows.Forms.TextBox();
		this.MethodOpen3 = new System.Windows.Forms.Button();
		this.tbMethName3 = new System.Windows.Forms.TextBox();
		this.label3 = new System.Windows.Forms.Label();
		this.MethodOpen2 = new System.Windows.Forms.Button();
		this.tbMethName2 = new System.Windows.Forms.TextBox();
		this.label2 = new System.Windows.Forms.Label();
		this.MethodOpen1 = new System.Windows.Forms.Button();
		this.tbMethName1 = new System.Windows.Forms.TextBox();
		this.label1 = new System.Windows.Forms.Label();
		this.labCntTimes = new System.Windows.Forms.Label();
		this.labCntTime = new System.Windows.Forms.Label();
		this.btnSave1 = new System.Windows.Forms.Button();
		this.label24 = new System.Windows.Forms.Label();
		this.label23 = new System.Windows.Forms.Label();
		this.tbCycleTime3 = new System.Windows.Forms.TextBox();
		this.cbCoerce = new System.Windows.Forms.ComboBox();
		this.label22 = new System.Windows.Forms.Label();
		this.cbTimes12 = new System.Windows.Forms.ComboBox();
		this.cbTimes11 = new System.Windows.Forms.ComboBox();
		this.cbTimes10 = new System.Windows.Forms.ComboBox();
		this.cbTimes9 = new System.Windows.Forms.ComboBox();
		this.label18 = new System.Windows.Forms.Label();
		this.label19 = new System.Windows.Forms.Label();
		this.label20 = new System.Windows.Forms.Label();
		this.label21 = new System.Windows.Forms.Label();
		this.cbTimes8 = new System.Windows.Forms.ComboBox();
		this.cbTimes7 = new System.Windows.Forms.ComboBox();
		this.cbTimes6 = new System.Windows.Forms.ComboBox();
		this.cbTimes5 = new System.Windows.Forms.ComboBox();
		this.cbTimes4 = new System.Windows.Forms.ComboBox();
		this.cbTimes3 = new System.Windows.Forms.ComboBox();
		this.cbTimes2 = new System.Windows.Forms.ComboBox();
		this.cbTimes1 = new System.Windows.Forms.ComboBox();
		this.btnStartCycle = new System.Windows.Forms.Button();
		this.label29 = new System.Windows.Forms.Label();
		this.tbCycle = new System.Windows.Forms.TextBox();
		this.label26 = new System.Windows.Forms.Label();
		this.label27 = new System.Windows.Forms.Label();
		this.tbStartTime = new System.Windows.Forms.TextBox();
		this.label14 = new System.Windows.Forms.Label();
		this.label15 = new System.Windows.Forms.Label();
		this.label16 = new System.Windows.Forms.Label();
		this.label17 = new System.Windows.Forms.Label();
		this.label13 = new System.Windows.Forms.Label();
		this.label12 = new System.Windows.Forms.Label();
		this.label11 = new System.Windows.Forms.Label();
		this.label10 = new System.Windows.Forms.Label();
		this.timer1 = new System.Windows.Forms.Timer(this.components);
		this.tabControl1 = new System.Windows.Forms.TabControl();
		this.tabPage1 = new System.Windows.Forms.TabPage();
		this.groupBox1 = new System.Windows.Forms.GroupBox();
		this.labWS = new System.Windows.Forms.Label();
		this.tbWarterPpmv = new System.Windows.Forms.TextBox();
		this.tbWarterTemp = new System.Windows.Forms.TextBox();
		this.label45 = new System.Windows.Forms.Label();
		this.tbWarterTemp2 = new System.Windows.Forms.TextBox();
		this.tbWarterTemp4 = new System.Windows.Forms.TextBox();
		this.label81 = new System.Windows.Forms.Label();
		this.tbWarterTemp3 = new System.Windows.Forms.TextBox();
		this.label44 = new System.Windows.Forms.Label();
		this.label28 = new System.Windows.Forms.Label();
		this.tabPage2 = new System.Windows.Forms.TabPage();
		this.panel3 = new System.Windows.Forms.Panel();
		this.tabPage3 = new System.Windows.Forms.TabPage();
		this.panel2 = new System.Windows.Forms.Panel();
		this.label25 = new System.Windows.Forms.Label();
		this.tbTimeInterval = new System.Windows.Forms.TextBox();
		this.tbName3 = new System.Windows.Forms.TextBox();
		this.tbName2 = new System.Windows.Forms.TextBox();
		this.tbName1 = new System.Windows.Forms.TextBox();
		this.tbDelay = new System.Windows.Forms.TextBox();
		this.label9 = new System.Windows.Forms.Label();
		this.tabPage4 = new System.Windows.Forms.TabPage();
		this.chbCalibra = new System.Windows.Forms.CheckBox();
		this.cb_checkBit = new System.Windows.Forms.ComboBox();
		this.label43 = new System.Windows.Forms.Label();
		this.cb_stopBit = new System.Windows.Forms.ComboBox();
		this.label42 = new System.Windows.Forms.Label();
		this.cb_dataBit = new System.Windows.Forms.ComboBox();
		this.label41 = new System.Windows.Forms.Label();
		this.cb_baudRate = new System.Windows.Forms.ComboBox();
		this.label40 = new System.Windows.Forms.Label();
		this.cb_portNameSend = new System.Windows.Forms.ComboBox();
		this.label38 = new System.Windows.Forms.Label();
		this.btnMonit = new System.Windows.Forms.Button();
		this.btnModbusSave = new System.Windows.Forms.Button();
		this.tbModbusAddress = new System.Windows.Forms.TextBox();
		this.label31 = new System.Windows.Forms.Label();
		this.label30 = new System.Windows.Forms.Label();
		this.tabPage5 = new System.Windows.Forms.TabPage();
		this.btnSavePeakTime = new System.Windows.Forms.Button();
		this.label87 = new System.Windows.Forms.Label();
		this.label88 = new System.Windows.Forms.Label();
		this.label84 = new System.Windows.Forms.Label();
		this.label83 = new System.Windows.Forms.Label();
		this.tbTimeOFF4 = new System.Windows.Forms.TextBox();
		this.tbTimeON4 = new System.Windows.Forms.TextBox();
		this.tbTimeOFF3 = new System.Windows.Forms.TextBox();
		this.tbTimeON3 = new System.Windows.Forms.TextBox();
		this.label86 = new System.Windows.Forms.Label();
		this.label89 = new System.Windows.Forms.Label();
		this.label85 = new System.Windows.Forms.Label();
		this.label82 = new System.Windows.Forms.Label();
		this.tbTimeOFF2 = new System.Windows.Forms.TextBox();
		this.tbTimeON2 = new System.Windows.Forms.TextBox();
		this.tbTimeOFF1 = new System.Windows.Forms.TextBox();
		this.tbTimeON1 = new System.Windows.Forms.TextBox();
		this.tabPage6 = new System.Windows.Forms.TabPage();
		this.btnConver = new System.Windows.Forms.Button();
		this.cbDectorNumber = new System.Windows.Forms.ComboBox();
		this.tbPAir2 = new System.Windows.Forms.TextBox();
		this.label39 = new System.Windows.Forms.Label();
		this.tbPHH2 = new System.Windows.Forms.TextBox();
		this.labPHH2 = new System.Windows.Forms.Label();
		this.tbPAir1 = new System.Windows.Forms.TextBox();
		this.label37 = new System.Windows.Forms.Label();
		this.tbPHH1 = new System.Windows.Forms.TextBox();
		this.label36 = new System.Windows.Forms.Label();
		this.tbPINJ4 = new System.Windows.Forms.TextBox();
		this.label35 = new System.Windows.Forms.Label();
		this.tbPINJ3 = new System.Windows.Forms.TextBox();
		this.label34 = new System.Windows.Forms.Label();
		this.tbPINJ2 = new System.Windows.Forms.TextBox();
		this.label33 = new System.Windows.Forms.Label();
		this.tbPINJ1 = new System.Windows.Forms.TextBox();
		this.label32 = new System.Windows.Forms.Label();
		this.tabPage7 = new System.Windows.Forms.TabPage();
		this.btnEPCSet = new System.Windows.Forms.Button();
		this.label78 = new System.Windows.Forms.Label();
		this.tbWeiChuiCur1 = new System.Windows.Forms.TextBox();
		this.label47 = new System.Windows.Forms.Label();
		this.label61 = new System.Windows.Forms.Label();
		this.label48 = new System.Windows.Forms.Label();
		this.tbColPreSet1 = new System.Windows.Forms.TextBox();
		this.label49 = new System.Windows.Forms.Label();
		this.tbColPreCur1 = new System.Windows.Forms.TextBox();
		this.label50 = new System.Windows.Forms.Label();
		this.label60 = new System.Windows.Forms.Label();
		this.label51 = new System.Windows.Forms.Label();
		this.tbHHSet1 = new System.Windows.Forms.TextBox();
		this.label52 = new System.Windows.Forms.Label();
		this.tbHHCur1 = new System.Windows.Forms.TextBox();
		this.label59 = new System.Windows.Forms.Label();
		this.tbWeiChuiSet1 = new System.Windows.Forms.TextBox();
		this.tbAirSet1 = new System.Windows.Forms.TextBox();
		this.label53 = new System.Windows.Forms.Label();
		this.tbAirCur1 = new System.Windows.Forms.TextBox();
		this.tbColPreCur3 = new System.Windows.Forms.TextBox();
		this.label58 = new System.Windows.Forms.Label();
		this.tbColPreSet3 = new System.Windows.Forms.TextBox();
		this.label57 = new System.Windows.Forms.Label();
		this.label54 = new System.Windows.Forms.Label();
		this.label56 = new System.Windows.Forms.Label();
		this.tbColPreCur2 = new System.Windows.Forms.TextBox();
		this.label55 = new System.Windows.Forms.Label();
		this.tbColPreSet2 = new System.Windows.Forms.TextBox();
		this.timer2 = new System.Windows.Forms.Timer(this.components);
		this.chbCombinDector = new System.Windows.Forms.CheckBox();
		this.gbBenXW.SuspendLayout();
		this.panel1.SuspendLayout();
		this.gbNHMC1.SuspendLayout();
		this.tabControl1.SuspendLayout();
		this.tabPage1.SuspendLayout();
		this.groupBox1.SuspendLayout();
		this.tabPage2.SuspendLayout();
		this.panel3.SuspendLayout();
		this.tabPage3.SuspendLayout();
		this.panel2.SuspendLayout();
		this.tabPage4.SuspendLayout();
		this.tabPage5.SuspendLayout();
		this.tabPage6.SuspendLayout();
		this.tabPage7.SuspendLayout();
		base.SuspendLayout();
		this.btShowDesktop.Location = new System.Drawing.Point(126, 1);
		this.btShowDesktop.Name = "btShowDesktop";
		this.btShowDesktop.Size = new System.Drawing.Size(63, 23);
		this.btShowDesktop.TabIndex = 85;
		this.btShowDesktop.Text = "显示桌面";
		this.btShowDesktop.UseVisualStyleBackColor = true;
		this.btShowDesktop.Click += new System.EventHandler(btShowDesktop_Click);
		this.button36.Location = new System.Drawing.Point(62, 1);
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
		this.lbBTEX.Visible = false;
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
		this.lbBTEXt.Visible = false;
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
		this.lbTHC.Font = new System.Drawing.Font("宋体", 15f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		this.lbTHC.ForeColor = System.Drawing.Color.DarkGreen;
		this.lbTHC.Location = new System.Drawing.Point(105, 23);
		this.lbTHC.Name = "lbTHC";
		this.lbTHC.Size = new System.Drawing.Size(19, 20);
		this.lbTHC.TabIndex = 59;
		this.lbTHC.Text = "0";
		this.lbTHC.Visible = false;
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
		this.btnFireOnSet.Location = new System.Drawing.Point(339, 1);
		this.btnFireOnSet.Name = "btnFireOnSet";
		this.btnFireOnSet.Size = new System.Drawing.Size(91, 23);
		this.btnFireOnSet.TabIndex = 90;
		this.btnFireOnSet.Text = "点火门限设定";
		this.btnFireOnSet.UseVisualStyleBackColor = true;
		this.btnFireOnSet.Click += new System.EventHandler(btnFireOnSet_Click);
		this.btnFireOnCheck.Location = new System.Drawing.Point(256, 1);
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
		this.gbBenXW.Location = new System.Drawing.Point(8, 3);
		this.gbBenXW.Name = "gbBenXW";
		this.gbBenXW.Size = new System.Drawing.Size(24, 96);
		this.gbBenXW.TabIndex = 92;
		this.gbBenXW.TabStop = false;
		this.gbBenXW.Text = "分析结果";
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
		this.panel1.Location = new System.Drawing.Point(3, 3);
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
		this.btnNetConfig.Location = new System.Drawing.Point(191, 1);
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
		this.btnCali.Location = new System.Drawing.Point(636, -1);
		this.btnCali.Name = "btnCali";
		this.btnCali.Size = new System.Drawing.Size(72, 33);
		this.btnCali.TabIndex = 92;
		this.btnCali.Text = "一键标定";
		this.btnCali.UseVisualStyleBackColor = true;
		this.btnCali.Click += new System.EventHandler(btnCali_Click);
		this.gbNHMC1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.gbNHMC1.Controls.Add(this.label79);
		this.gbNHMC1.Controls.Add(this.gbBenXW);
		this.gbNHMC1.Controls.Add(this.lbCH4);
		this.gbNHMC1.Controls.Add(this.label77);
		this.gbNHMC1.Controls.Add(this.lbNMHCT);
		this.gbNHMC1.Controls.Add(this.lbNMHC);
		this.gbNHMC1.Location = new System.Drawing.Point(79, 61);
		this.gbNHMC1.Name = "gbNHMC1";
		this.gbNHMC1.Size = new System.Drawing.Size(297, 105);
		this.gbNHMC1.TabIndex = 86;
		this.gbNHMC1.TabStop = false;
		this.gbNHMC1.Text = "非甲烷总烃";
		this.gbNHMC1.Visible = false;
		this.btnStart.Location = new System.Drawing.Point(676, 67);
		this.btnStart.Name = "btnStart";
		this.btnStart.Size = new System.Drawing.Size(75, 34);
		this.btnStart.TabIndex = 66;
		this.btnStart.Text = "开始循环";
		this.btnStart.UseVisualStyleBackColor = true;
		this.btnStart.Click += new System.EventHandler(BtnStart_Click);
		this.btnSave.Location = new System.Drawing.Point(676, 30);
		this.btnSave.Name = "btnSave";
		this.btnSave.Size = new System.Drawing.Size(75, 34);
		this.btnSave.TabIndex = 63;
		this.btnSave.Text = "保存并应用";
		this.btnSave.UseVisualStyleBackColor = true;
		this.btnSave.Click += new System.EventHandler(BtnSave_Click);
		this.label7.AutoSize = true;
		this.label7.Location = new System.Drawing.Point(497, 62);
		this.label7.Name = "label7";
		this.label7.Size = new System.Drawing.Size(53, 12);
		this.label7.TabIndex = 59;
		this.label7.Text = "循环次数";
		this.tbComTimes.Location = new System.Drawing.Point(591, 82);
		this.tbComTimes.Name = "tbComTimes";
		this.tbComTimes.ReadOnly = true;
		this.tbComTimes.Size = new System.Drawing.Size(48, 21);
		this.tbComTimes.TabIndex = 62;
		this.label8.AutoSize = true;
		this.label8.Location = new System.Drawing.Point(497, 88);
		this.label8.Name = "label8";
		this.label8.Size = new System.Drawing.Size(53, 12);
		this.label8.TabIndex = 60;
		this.label8.Text = "完成次数";
		this.tbCycleTimes.Location = new System.Drawing.Point(591, 57);
		this.tbCycleTimes.Name = "tbCycleTimes";
		this.tbCycleTimes.Size = new System.Drawing.Size(48, 21);
		this.tbCycleTimes.TabIndex = 61;
		this.chbChannel3.AutoSize = true;
		this.chbChannel3.Location = new System.Drawing.Point(289, 82);
		this.chbChannel3.Name = "chbChannel3";
		this.chbChannel3.Size = new System.Drawing.Size(15, 14);
		this.chbChannel3.TabIndex = 58;
		this.chbChannel3.UseVisualStyleBackColor = true;
		this.chbChannel2.AutoSize = true;
		this.chbChannel2.Location = new System.Drawing.Point(289, 48);
		this.chbChannel2.Name = "chbChannel2";
		this.chbChannel2.Size = new System.Drawing.Size(15, 14);
		this.chbChannel2.TabIndex = 57;
		this.chbChannel2.UseVisualStyleBackColor = true;
		this.chbChannel1.AutoSize = true;
		this.chbChannel1.Location = new System.Drawing.Point(289, 18);
		this.chbChannel1.Name = "chbChannel1";
		this.chbChannel1.Size = new System.Drawing.Size(15, 14);
		this.chbChannel1.TabIndex = 56;
		this.chbChannel1.UseVisualStyleBackColor = true;
		this.label6.AutoSize = true;
		this.label6.Location = new System.Drawing.Point(318, 76);
		this.label6.Name = "label6";
		this.label6.Size = new System.Drawing.Size(53, 12);
		this.label6.TabIndex = 54;
		this.label6.Text = "采样时间";
		this.tbInjecTime3.Location = new System.Drawing.Point(377, 75);
		this.tbInjecTime3.Name = "tbInjecTime3";
		this.tbInjecTime3.Size = new System.Drawing.Size(48, 21);
		this.tbInjecTime3.TabIndex = 55;
		this.label5.AutoSize = true;
		this.label5.Location = new System.Drawing.Point(318, 46);
		this.label5.Name = "label5";
		this.label5.Size = new System.Drawing.Size(53, 12);
		this.label5.TabIndex = 52;
		this.label5.Text = "采样时间";
		this.tbInjecTime2.Location = new System.Drawing.Point(377, 45);
		this.tbInjecTime2.Name = "tbInjecTime2";
		this.tbInjecTime2.Size = new System.Drawing.Size(48, 21);
		this.tbInjecTime2.TabIndex = 53;
		this.label4.AutoSize = true;
		this.label4.Location = new System.Drawing.Point(318, 16);
		this.label4.Name = "label4";
		this.label4.Size = new System.Drawing.Size(53, 12);
		this.label4.TabIndex = 50;
		this.label4.Text = "采样时间";
		this.tbInjecTime1.BackColor = System.Drawing.SystemColors.Window;
		this.tbInjecTime1.Location = new System.Drawing.Point(377, 11);
		this.tbInjecTime1.Name = "tbInjecTime1";
		this.tbInjecTime1.Size = new System.Drawing.Size(48, 21);
		this.tbInjecTime1.TabIndex = 51;
		this.MethodOpen3.Image = IBrainChrom2018.Properties.Resources.FolderOpen;
		this.MethodOpen3.Location = new System.Drawing.Point(252, 72);
		this.MethodOpen3.Name = "MethodOpen3";
		this.MethodOpen3.Size = new System.Drawing.Size(31, 32);
		this.MethodOpen3.TabIndex = 49;
		this.MethodOpen3.UseVisualStyleBackColor = true;
		this.MethodOpen3.Click += new System.EventHandler(MethodOpen3_Click);
		this.tbMethName3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.tbMethName3.Location = new System.Drawing.Point(124, 76);
		this.tbMethName3.Name = "tbMethName3";
		this.tbMethName3.ReadOnly = true;
		this.tbMethName3.Size = new System.Drawing.Size(117, 21);
		this.tbMethName3.TabIndex = 48;
		this.tbMethName3.Text = "默认";
		this.label3.AutoSize = true;
		this.label3.Location = new System.Drawing.Point(3, 80);
		this.label3.Name = "label3";
		this.label3.Size = new System.Drawing.Size(119, 12);
		this.label3.TabIndex = 47;
		this.label3.Text = "3、流路三分析方法：";
		this.MethodOpen2.Image = IBrainChrom2018.Properties.Resources.FolderOpen;
		this.MethodOpen2.Location = new System.Drawing.Point(252, 39);
		this.MethodOpen2.Name = "MethodOpen2";
		this.MethodOpen2.Size = new System.Drawing.Size(31, 32);
		this.MethodOpen2.TabIndex = 46;
		this.MethodOpen2.UseVisualStyleBackColor = true;
		this.MethodOpen2.Click += new System.EventHandler(MethodOpen2_Click);
		this.tbMethName2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.tbMethName2.Location = new System.Drawing.Point(124, 43);
		this.tbMethName2.Name = "tbMethName2";
		this.tbMethName2.ReadOnly = true;
		this.tbMethName2.Size = new System.Drawing.Size(117, 21);
		this.tbMethName2.TabIndex = 45;
		this.tbMethName2.Text = "默认";
		this.label2.AutoSize = true;
		this.label2.Location = new System.Drawing.Point(3, 47);
		this.label2.Name = "label2";
		this.label2.Size = new System.Drawing.Size(119, 12);
		this.label2.TabIndex = 44;
		this.label2.Text = "2、流路二分析方法：";
		this.MethodOpen1.Image = IBrainChrom2018.Properties.Resources.FolderOpen;
		this.MethodOpen1.Location = new System.Drawing.Point(252, 7);
		this.MethodOpen1.Name = "MethodOpen1";
		this.MethodOpen1.Size = new System.Drawing.Size(31, 32);
		this.MethodOpen1.TabIndex = 43;
		this.MethodOpen1.UseVisualStyleBackColor = true;
		this.MethodOpen1.Click += new System.EventHandler(MethodOpen1_Click);
		this.tbMethName1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.tbMethName1.Location = new System.Drawing.Point(124, 11);
		this.tbMethName1.Name = "tbMethName1";
		this.tbMethName1.ReadOnly = true;
		this.tbMethName1.Size = new System.Drawing.Size(117, 21);
		this.tbMethName1.TabIndex = 42;
		this.tbMethName1.Text = "默认";
		this.label1.AutoSize = true;
		this.label1.Location = new System.Drawing.Point(3, 15);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(119, 12);
		this.label1.TabIndex = 0;
		this.label1.Text = "1、流路一分析方法：";
		this.labCntTimes.AutoSize = true;
		this.labCntTimes.Font = new System.Drawing.Font("宋体", 14.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		this.labCntTimes.Location = new System.Drawing.Point(405, 45);
		this.labCntTimes.Name = "labCntTimes";
		this.labCntTimes.Size = new System.Drawing.Size(19, 19);
		this.labCntTimes.TabIndex = 89;
		this.labCntTimes.Text = "0";
		this.labCntTime.AutoSize = true;
		this.labCntTime.Font = new System.Drawing.Font("宋体", 14.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		this.labCntTime.Location = new System.Drawing.Point(454, 45);
		this.labCntTime.Name = "labCntTime";
		this.labCntTime.Size = new System.Drawing.Size(19, 19);
		this.labCntTime.TabIndex = 88;
		this.labCntTime.Text = "0";
		this.btnSave1.Location = new System.Drawing.Point(369, 79);
		this.btnSave1.Name = "btnSave1";
		this.btnSave1.Size = new System.Drawing.Size(111, 23);
		this.btnSave1.TabIndex = 87;
		this.btnSave1.Text = "保存参数";
		this.btnSave1.UseVisualStyleBackColor = true;
		this.btnSave1.Click += new System.EventHandler(BtnSave1_Click);
		this.label24.AutoSize = true;
		this.label24.Location = new System.Drawing.Point(685, 36);
		this.label24.Name = "label24";
		this.label24.Size = new System.Drawing.Size(23, 12);
		this.label24.TabIndex = 86;
		this.label24.Text = "min";
		this.label23.AutoSize = true;
		this.label23.Location = new System.Drawing.Point(508, 34);
		this.label23.Name = "label23";
		this.label23.Size = new System.Drawing.Size(53, 12);
		this.label23.TabIndex = 85;
		this.label23.Text = "循环时间";
		this.tbCycleTime3.Location = new System.Drawing.Point(579, 30);
		this.tbCycleTime3.Name = "tbCycleTime3";
		this.tbCycleTime3.Size = new System.Drawing.Size(100, 21);
		this.tbCycleTime3.TabIndex = 84;
		this.cbCoerce.AutoCompleteCustomSource.AddRange(new string[4] { "无", "流路一", "流路二", "流路三" });
		this.cbCoerce.FormattingEnabled = true;
		this.cbCoerce.Items.AddRange(new object[13]
		{
			"无", "流路一", "流路二", "流路三", "流路四", "流路五", "流路六", "流路七", "流路八", "流路九",
			"流路十", "流路十一", "流路十二"
		});
		this.cbCoerce.Location = new System.Drawing.Point(404, 7);
		this.cbCoerce.Name = "cbCoerce";
		this.cbCoerce.Size = new System.Drawing.Size(76, 20);
		this.cbCoerce.TabIndex = 83;
		this.cbCoerce.Text = "无";
		this.label22.AutoSize = true;
		this.label22.Location = new System.Drawing.Point(367, 9);
		this.label22.Name = "label22";
		this.label22.Size = new System.Drawing.Size(41, 12);
		this.label22.TabIndex = 82;
		this.label22.Text = "强检：";
		this.cbTimes12.AutoCompleteCustomSource.AddRange(new string[4] { "无", "流路一", "流路二", "流路三" });
		this.cbTimes12.FormattingEnabled = true;
		this.cbTimes12.Items.AddRange(new object[13]
		{
			"无", "流路一", "流路二", "流路三", "流路四", "流路五", "流路六", "流路七", "流路八", "流路九",
			"流路十", "流路十一", "流路十二"
		});
		this.cbTimes12.Location = new System.Drawing.Point(285, 80);
		this.cbTimes12.Name = "cbTimes12";
		this.cbTimes12.Size = new System.Drawing.Size(76, 20);
		this.cbTimes12.TabIndex = 81;
		this.cbTimes12.Text = "无";
		this.cbTimes11.AutoCompleteCustomSource.AddRange(new string[4] { "无", "流路一", "流路二", "流路三" });
		this.cbTimes11.FormattingEnabled = true;
		this.cbTimes11.Items.AddRange(new object[13]
		{
			"无", "流路一", "流路二", "流路三", "流路四", "流路五", "流路六", "流路七", "流路八", "流路九",
			"流路十", "流路十一", "流路十二"
		});
		this.cbTimes11.Location = new System.Drawing.Point(285, 57);
		this.cbTimes11.Name = "cbTimes11";
		this.cbTimes11.Size = new System.Drawing.Size(76, 20);
		this.cbTimes11.TabIndex = 80;
		this.cbTimes11.Text = "无";
		this.cbTimes10.AutoCompleteCustomSource.AddRange(new string[4] { "无", "流路一", "流路二", "流路三" });
		this.cbTimes10.FormattingEnabled = true;
		this.cbTimes10.Items.AddRange(new object[13]
		{
			"无", "流路一", "流路二", "流路三", "流路四", "流路五", "流路六", "流路七", "流路八", "流路九",
			"流路十", "流路十一", "流路十二"
		});
		this.cbTimes10.Location = new System.Drawing.Point(285, 34);
		this.cbTimes10.Name = "cbTimes10";
		this.cbTimes10.Size = new System.Drawing.Size(76, 20);
		this.cbTimes10.TabIndex = 79;
		this.cbTimes10.Text = "无";
		this.cbTimes9.AutoCompleteCustomSource.AddRange(new string[4] { "无", "流路一", "流路二", "流路三" });
		this.cbTimes9.FormattingEnabled = true;
		this.cbTimes9.Items.AddRange(new object[13]
		{
			"无", "流路一", "流路二", "流路三", "流路四", "流路五", "流路六", "流路七", "流路八", "流路九",
			"流路十", "流路十一", "流路十二"
		});
		this.cbTimes9.Location = new System.Drawing.Point(286, 9);
		this.cbTimes9.Name = "cbTimes9";
		this.cbTimes9.Size = new System.Drawing.Size(76, 20);
		this.cbTimes9.TabIndex = 78;
		this.cbTimes9.Text = "无";
		this.label18.AutoSize = true;
		this.label18.Location = new System.Drawing.Point(251, 82);
		this.label18.Name = "label18";
		this.label18.Size = new System.Drawing.Size(41, 12);
		this.label18.TabIndex = 77;
		this.label18.Text = "十二：";
		this.label19.AutoSize = true;
		this.label19.Location = new System.Drawing.Point(250, 58);
		this.label19.Name = "label19";
		this.label19.Size = new System.Drawing.Size(41, 12);
		this.label19.TabIndex = 76;
		this.label19.Text = "十一：";
		this.label20.AutoSize = true;
		this.label20.Location = new System.Drawing.Point(250, 33);
		this.label20.Name = "label20";
		this.label20.Size = new System.Drawing.Size(29, 12);
		this.label20.TabIndex = 75;
		this.label20.Text = "十：";
		this.label21.AutoSize = true;
		this.label21.Location = new System.Drawing.Point(251, 11);
		this.label21.Name = "label21";
		this.label21.Size = new System.Drawing.Size(29, 12);
		this.label21.TabIndex = 74;
		this.label21.Text = "九：";
		this.cbTimes8.AutoCompleteCustomSource.AddRange(new string[4] { "无", "流路一", "流路二", "流路三" });
		this.cbTimes8.FormattingEnabled = true;
		this.cbTimes8.Items.AddRange(new object[13]
		{
			"无", "流路一", "流路二", "流路三", "流路四", "流路五", "流路六", "流路七", "流路八", "流路九",
			"流路十", "流路十一", "流路十二"
		});
		this.cbTimes8.Location = new System.Drawing.Point(160, 79);
		this.cbTimes8.Name = "cbTimes8";
		this.cbTimes8.Size = new System.Drawing.Size(76, 20);
		this.cbTimes8.TabIndex = 73;
		this.cbTimes8.Text = "无";
		this.cbTimes7.AutoCompleteCustomSource.AddRange(new string[4] { "无", "流路一", "流路二", "流路三" });
		this.cbTimes7.FormattingEnabled = true;
		this.cbTimes7.Items.AddRange(new object[13]
		{
			"无", "流路一", "流路二", "流路三", "流路四", "流路五", "流路六", "流路七", "流路八", "流路九",
			"流路十", "流路十一", "流路十二"
		});
		this.cbTimes7.Location = new System.Drawing.Point(160, 56);
		this.cbTimes7.Name = "cbTimes7";
		this.cbTimes7.Size = new System.Drawing.Size(76, 20);
		this.cbTimes7.TabIndex = 72;
		this.cbTimes7.Text = "无";
		this.cbTimes6.AutoCompleteCustomSource.AddRange(new string[4] { "无", "流路一", "流路二", "流路三" });
		this.cbTimes6.FormattingEnabled = true;
		this.cbTimes6.Items.AddRange(new object[13]
		{
			"无", "流路一", "流路二", "流路三", "流路四", "流路五", "流路六", "流路七", "流路八", "流路九",
			"流路十", "流路十一", "流路十二"
		});
		this.cbTimes6.Location = new System.Drawing.Point(160, 33);
		this.cbTimes6.Name = "cbTimes6";
		this.cbTimes6.Size = new System.Drawing.Size(76, 20);
		this.cbTimes6.TabIndex = 71;
		this.cbTimes6.Text = "无";
		this.cbTimes5.AutoCompleteCustomSource.AddRange(new string[4] { "无", "流路一", "流路二", "流路三" });
		this.cbTimes5.FormattingEnabled = true;
		this.cbTimes5.Items.AddRange(new object[13]
		{
			"无", "流路一", "流路二", "流路三", "流路四", "流路五", "流路六", "流路七", "流路八", "流路九",
			"流路十", "流路十一", "流路十二"
		});
		this.cbTimes5.Location = new System.Drawing.Point(161, 8);
		this.cbTimes5.Name = "cbTimes5";
		this.cbTimes5.Size = new System.Drawing.Size(76, 20);
		this.cbTimes5.TabIndex = 70;
		this.cbTimes5.Text = "无";
		this.cbTimes4.AutoCompleteCustomSource.AddRange(new string[4] { "无", "流路一", "流路二", "流路三" });
		this.cbTimes4.FormattingEnabled = true;
		this.cbTimes4.Items.AddRange(new object[13]
		{
			"无", "流路一", "流路二", "流路三", "流路四", "流路五", "流路六", "流路七", "流路八", "流路九",
			"流路十", "流路十一", "流路十二"
		});
		this.cbTimes4.Location = new System.Drawing.Point(37, 79);
		this.cbTimes4.Name = "cbTimes4";
		this.cbTimes4.Size = new System.Drawing.Size(76, 20);
		this.cbTimes4.TabIndex = 69;
		this.cbTimes4.Text = "无";
		this.cbTimes3.AutoCompleteCustomSource.AddRange(new string[4] { "无", "流路一", "流路二", "流路三" });
		this.cbTimes3.FormattingEnabled = true;
		this.cbTimes3.Items.AddRange(new object[13]
		{
			"无", "流路一", "流路二", "流路三", "流路四", "流路五", "流路六", "流路七", "流路八", "流路九",
			"流路十", "流路十一", "流路十二"
		});
		this.cbTimes3.Location = new System.Drawing.Point(37, 56);
		this.cbTimes3.Name = "cbTimes3";
		this.cbTimes3.Size = new System.Drawing.Size(76, 20);
		this.cbTimes3.TabIndex = 68;
		this.cbTimes3.Text = "无";
		this.cbTimes2.AutoCompleteCustomSource.AddRange(new string[4] { "无", "流路一", "流路二", "流路三" });
		this.cbTimes2.FormattingEnabled = true;
		this.cbTimes2.Items.AddRange(new object[13]
		{
			"无", "流路一", "流路二", "流路三", "流路四", "流路五", "流路六", "流路七", "流路八", "流路九",
			"流路十", "流路十一", "流路十二"
		});
		this.cbTimes2.Location = new System.Drawing.Point(37, 33);
		this.cbTimes2.Name = "cbTimes2";
		this.cbTimes2.Size = new System.Drawing.Size(76, 20);
		this.cbTimes2.TabIndex = 67;
		this.cbTimes2.Text = "无";
		this.cbTimes1.AutoCompleteCustomSource.AddRange(new string[4] { "无", "流路一", "流路二", "流路三" });
		this.cbTimes1.FormattingEnabled = true;
		this.cbTimes1.Items.AddRange(new object[13]
		{
			"无", "流路一", "流路二", "流路三", "流路四", "流路五", "流路六", "流路七", "流路八", "流路九",
			"流路十", "流路十一", "流路十二"
		});
		this.cbTimes1.Location = new System.Drawing.Point(38, 8);
		this.cbTimes1.Name = "cbTimes1";
		this.cbTimes1.Size = new System.Drawing.Size(76, 20);
		this.cbTimes1.TabIndex = 66;
		this.cbTimes1.Text = "无";
		this.btnStartCycle.Location = new System.Drawing.Point(504, 77);
		this.btnStartCycle.Name = "btnStartCycle";
		this.btnStartCycle.Size = new System.Drawing.Size(244, 25);
		this.btnStartCycle.TabIndex = 49;
		this.btnStartCycle.Text = "开始循环";
		this.btnStartCycle.UseVisualStyleBackColor = true;
		this.btnStartCycle.Click += new System.EventHandler(BtnStartCycle_Click);
		this.label29.AutoSize = true;
		this.label29.Location = new System.Drawing.Point(508, 56);
		this.label29.Name = "label29";
		this.label29.Size = new System.Drawing.Size(53, 12);
		this.label29.TabIndex = 47;
		this.label29.Text = "循环次数";
		this.tbCycle.Location = new System.Drawing.Point(579, 55);
		this.tbCycle.Name = "tbCycle";
		this.tbCycle.Size = new System.Drawing.Size(100, 21);
		this.tbCycle.TabIndex = 46;
		this.label26.AutoSize = true;
		this.label26.Location = new System.Drawing.Point(685, 12);
		this.label26.Name = "label26";
		this.label26.Size = new System.Drawing.Size(23, 12);
		this.label26.TabIndex = 45;
		this.label26.Text = "min";
		this.label27.AutoSize = true;
		this.label27.Location = new System.Drawing.Point(508, 8);
		this.label27.Name = "label27";
		this.label27.Size = new System.Drawing.Size(53, 12);
		this.label27.TabIndex = 44;
		this.label27.Text = "启动采集";
		this.tbStartTime.Location = new System.Drawing.Point(579, 4);
		this.tbStartTime.Name = "tbStartTime";
		this.tbStartTime.Size = new System.Drawing.Size(100, 21);
		this.tbStartTime.TabIndex = 43;
		this.label14.AutoSize = true;
		this.label14.Location = new System.Drawing.Point(126, 81);
		this.label14.Name = "label14";
		this.label14.Size = new System.Drawing.Size(29, 12);
		this.label14.TabIndex = 34;
		this.label14.Text = "八：";
		this.label15.AutoSize = true;
		this.label15.Location = new System.Drawing.Point(125, 57);
		this.label15.Name = "label15";
		this.label15.Size = new System.Drawing.Size(29, 12);
		this.label15.TabIndex = 33;
		this.label15.Text = "七：";
		this.label16.AutoSize = true;
		this.label16.Location = new System.Drawing.Point(125, 32);
		this.label16.Name = "label16";
		this.label16.Size = new System.Drawing.Size(29, 12);
		this.label16.TabIndex = 32;
		this.label16.Text = "六：";
		this.label17.AutoSize = true;
		this.label17.Location = new System.Drawing.Point(126, 10);
		this.label17.Name = "label17";
		this.label17.Size = new System.Drawing.Size(29, 12);
		this.label17.TabIndex = 31;
		this.label17.Text = "五：";
		this.label13.AutoSize = true;
		this.label13.Location = new System.Drawing.Point(3, 80);
		this.label13.Name = "label13";
		this.label13.Size = new System.Drawing.Size(29, 12);
		this.label13.TabIndex = 30;
		this.label13.Text = "四：";
		this.label12.AutoSize = true;
		this.label12.Location = new System.Drawing.Point(2, 56);
		this.label12.Name = "label12";
		this.label12.Size = new System.Drawing.Size(29, 12);
		this.label12.TabIndex = 29;
		this.label12.Text = "三：";
		this.label11.AutoSize = true;
		this.label11.Location = new System.Drawing.Point(2, 31);
		this.label11.Name = "label11";
		this.label11.Size = new System.Drawing.Size(29, 12);
		this.label11.TabIndex = 28;
		this.label11.Text = "二：";
		this.label10.AutoSize = true;
		this.label10.Location = new System.Drawing.Point(3, 9);
		this.label10.Name = "label10";
		this.label10.Size = new System.Drawing.Size(29, 12);
		this.label10.TabIndex = 27;
		this.label10.Text = "一：";
		this.timer1.Interval = 1000;
		this.timer1.Tick += new System.EventHandler(Timer1_Tick);
		this.tabControl1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.tabControl1.Controls.Add(this.tabPage1);
		this.tabControl1.Controls.Add(this.tabPage2);
		this.tabControl1.Controls.Add(this.tabPage3);
		this.tabControl1.Controls.Add(this.tabPage4);
		this.tabControl1.Controls.Add(this.tabPage5);
		this.tabControl1.Controls.Add(this.tabPage6);
		this.tabControl1.Controls.Add(this.tabPage7);
		this.tabControl1.Location = new System.Drawing.Point(0, 33);
		this.tabControl1.Name = "tabControl1";
		this.tabControl1.SelectedIndex = 0;
		this.tabControl1.Size = new System.Drawing.Size(775, 157);
		this.tabControl1.TabIndex = 96;
		this.tabPage1.Controls.Add(this.groupBox1);
		this.tabPage1.Controls.Add(this.tbWarterTemp4);
		this.tabPage1.Controls.Add(this.label81);
		this.tabPage1.Controls.Add(this.tbWarterTemp3);
		this.tabPage1.Controls.Add(this.label44);
		this.tabPage1.Controls.Add(this.label28);
		this.tabPage1.Controls.Add(this.lbTHC);
		this.tabPage1.Location = new System.Drawing.Point(4, 22);
		this.tabPage1.Name = "tabPage1";
		this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
		this.tabPage1.Size = new System.Drawing.Size(767, 131);
		this.tabPage1.TabIndex = 0;
		this.tabPage1.Text = "结果";
		this.tabPage1.UseVisualStyleBackColor = true;
		this.groupBox1.Controls.Add(this.labWS);
		this.groupBox1.Controls.Add(this.tbWarterPpmv);
		this.groupBox1.Controls.Add(this.tbWarterTemp);
		this.groupBox1.Controls.Add(this.label45);
		this.groupBox1.Controls.Add(this.tbWarterTemp2);
		this.groupBox1.Location = new System.Drawing.Point(458, 6);
		this.groupBox1.Name = "groupBox1";
		this.groupBox1.Size = new System.Drawing.Size(295, 117);
		this.groupBox1.TabIndex = 69;
		this.groupBox1.TabStop = false;
		this.groupBox1.Text = "水含量";
		this.labWS.AutoSize = true;
		this.labWS.Location = new System.Drawing.Point(6, 76);
		this.labWS.Name = "labWS";
		this.labWS.Size = new System.Drawing.Size(11, 12);
		this.labWS.TabIndex = 65;
		this.labWS.Text = "-";
		this.tbWarterPpmv.Location = new System.Drawing.Point(6, 18);
		this.tbWarterPpmv.Name = "tbWarterPpmv";
		this.tbWarterPpmv.Size = new System.Drawing.Size(283, 21);
		this.tbWarterPpmv.TabIndex = 64;
		this.tbWarterTemp.Location = new System.Drawing.Point(6, 46);
		this.tbWarterTemp.Name = "tbWarterTemp";
		this.tbWarterTemp.Size = new System.Drawing.Size(283, 21);
		this.tbWarterTemp.TabIndex = 62;
		this.label45.AutoSize = true;
		this.label45.Location = new System.Drawing.Point(6, 99);
		this.label45.Name = "label45";
		this.label45.Size = new System.Drawing.Size(41, 12);
		this.label45.TabIndex = 63;
		this.label45.Text = "露点：";
		this.tbWarterTemp2.Location = new System.Drawing.Point(50, 92);
		this.tbWarterTemp2.Name = "tbWarterTemp2";
		this.tbWarterTemp2.Size = new System.Drawing.Size(239, 21);
		this.tbWarterTemp2.TabIndex = 64;
		this.tbWarterTemp4.Location = new System.Drawing.Point(474, 46);
		this.tbWarterTemp4.Name = "tbWarterTemp4";
		this.tbWarterTemp4.Size = new System.Drawing.Size(100, 21);
		this.tbWarterTemp4.TabIndex = 68;
		this.tbWarterTemp4.Visible = false;
		this.label81.AutoSize = true;
		this.label81.Location = new System.Drawing.Point(435, 49);
		this.label81.Name = "label81";
		this.label81.Size = new System.Drawing.Size(47, 12);
		this.label81.TabIndex = 67;
		this.label81.Text = "露点4：";
		this.label81.Visible = false;
		this.tbWarterTemp3.Location = new System.Drawing.Point(474, 43);
		this.tbWarterTemp3.Name = "tbWarterTemp3";
		this.tbWarterTemp3.Size = new System.Drawing.Size(100, 21);
		this.tbWarterTemp3.TabIndex = 66;
		this.tbWarterTemp3.Visible = false;
		this.label44.AutoSize = true;
		this.label44.Location = new System.Drawing.Point(435, 46);
		this.label44.Name = "label44";
		this.label44.Size = new System.Drawing.Size(47, 12);
		this.label44.TabIndex = 65;
		this.label44.Text = "露点3：";
		this.label44.Visible = false;
		this.label28.AutoSize = true;
		this.label28.Font = new System.Drawing.Font("宋体", 15f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		this.label28.Location = new System.Drawing.Point(14, 23);
		this.label28.Name = "label28";
		this.label28.Size = new System.Drawing.Size(69, 20);
		this.label28.TabIndex = 60;
		this.label28.Text = "总烃：";
		this.label28.Visible = false;
		this.tabPage2.Controls.Add(this.panel3);
		this.tabPage2.Location = new System.Drawing.Point(4, 22);
		this.tabPage2.Name = "tabPage2";
		this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
		this.tabPage2.Size = new System.Drawing.Size(767, 131);
		this.tabPage2.TabIndex = 1;
		this.tabPage2.Text = "采样时序设定";
		this.tabPage2.UseVisualStyleBackColor = true;
		this.panel3.Controls.Add(this.label10);
		this.panel3.Controls.Add(this.labCntTimes);
		this.panel3.Controls.Add(this.cbTimes5);
		this.panel3.Controls.Add(this.labCntTime);
		this.panel3.Controls.Add(this.cbTimes4);
		this.panel3.Controls.Add(this.cbTimes1);
		this.panel3.Controls.Add(this.cbTimes6);
		this.panel3.Controls.Add(this.btnSave1);
		this.panel3.Controls.Add(this.cbTimes3);
		this.panel3.Controls.Add(this.cbTimes7);
		this.panel3.Controls.Add(this.label24);
		this.panel3.Controls.Add(this.cbTimes2);
		this.panel3.Controls.Add(this.label11);
		this.panel3.Controls.Add(this.cbTimes8);
		this.panel3.Controls.Add(this.label23);
		this.panel3.Controls.Add(this.btnStartCycle);
		this.panel3.Controls.Add(this.label12);
		this.panel3.Controls.Add(this.label21);
		this.panel3.Controls.Add(this.tbCycleTime3);
		this.panel3.Controls.Add(this.label29);
		this.panel3.Controls.Add(this.label13);
		this.panel3.Controls.Add(this.label20);
		this.panel3.Controls.Add(this.cbCoerce);
		this.panel3.Controls.Add(this.tbCycle);
		this.panel3.Controls.Add(this.label17);
		this.panel3.Controls.Add(this.label19);
		this.panel3.Controls.Add(this.label22);
		this.panel3.Controls.Add(this.label26);
		this.panel3.Controls.Add(this.label16);
		this.panel3.Controls.Add(this.label18);
		this.panel3.Controls.Add(this.cbTimes12);
		this.panel3.Controls.Add(this.label27);
		this.panel3.Controls.Add(this.label15);
		this.panel3.Controls.Add(this.cbTimes9);
		this.panel3.Controls.Add(this.cbTimes11);
		this.panel3.Controls.Add(this.tbStartTime);
		this.panel3.Controls.Add(this.label14);
		this.panel3.Controls.Add(this.cbTimes10);
		this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
		this.panel3.Location = new System.Drawing.Point(3, 3);
		this.panel3.Name = "panel3";
		this.panel3.Size = new System.Drawing.Size(761, 125);
		this.panel3.TabIndex = 90;
		this.tabPage3.Controls.Add(this.panel2);
		this.tabPage3.Location = new System.Drawing.Point(4, 22);
		this.tabPage3.Name = "tabPage3";
		this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
		this.tabPage3.Size = new System.Drawing.Size(767, 131);
		this.tabPage3.TabIndex = 2;
		this.tabPage3.Text = "流路分析方法设定";
		this.tabPage3.UseVisualStyleBackColor = true;
		this.panel2.Controls.Add(this.label25);
		this.panel2.Controls.Add(this.tbTimeInterval);
		this.panel2.Controls.Add(this.tbName3);
		this.panel2.Controls.Add(this.tbName2);
		this.panel2.Controls.Add(this.tbName1);
		this.panel2.Controls.Add(this.tbDelay);
		this.panel2.Controls.Add(this.label9);
		this.panel2.Controls.Add(this.label1);
		this.panel2.Controls.Add(this.btnStart);
		this.panel2.Controls.Add(this.label5);
		this.panel2.Controls.Add(this.tbInjecTime2);
		this.panel2.Controls.Add(this.tbInjecTime3);
		this.panel2.Controls.Add(this.label4);
		this.panel2.Controls.Add(this.tbMethName1);
		this.panel2.Controls.Add(this.label6);
		this.panel2.Controls.Add(this.btnSave);
		this.panel2.Controls.Add(this.tbInjecTime1);
		this.panel2.Controls.Add(this.MethodOpen1);
		this.panel2.Controls.Add(this.chbChannel1);
		this.panel2.Controls.Add(this.label7);
		this.panel2.Controls.Add(this.MethodOpen3);
		this.panel2.Controls.Add(this.label2);
		this.panel2.Controls.Add(this.chbChannel2);
		this.panel2.Controls.Add(this.tbComTimes);
		this.panel2.Controls.Add(this.tbMethName3);
		this.panel2.Controls.Add(this.tbMethName2);
		this.panel2.Controls.Add(this.chbChannel3);
		this.panel2.Controls.Add(this.label8);
		this.panel2.Controls.Add(this.label3);
		this.panel2.Controls.Add(this.MethodOpen2);
		this.panel2.Controls.Add(this.tbCycleTimes);
		this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
		this.panel2.Location = new System.Drawing.Point(3, 3);
		this.panel2.Name = "panel2";
		this.panel2.Size = new System.Drawing.Size(761, 125);
		this.panel2.TabIndex = 67;
		this.label25.AutoSize = true;
		this.label25.Location = new System.Drawing.Point(499, 40);
		this.label25.Name = "label25";
		this.label25.Size = new System.Drawing.Size(53, 12);
		this.label25.TabIndex = 73;
		this.label25.Text = "循环间隔";
		this.tbTimeInterval.Location = new System.Drawing.Point(591, 31);
		this.tbTimeInterval.Name = "tbTimeInterval";
		this.tbTimeInterval.Size = new System.Drawing.Size(48, 21);
		this.tbTimeInterval.TabIndex = 72;
		this.tbName3.Location = new System.Drawing.Point(431, 75);
		this.tbName3.Name = "tbName3";
		this.tbName3.Size = new System.Drawing.Size(62, 21);
		this.tbName3.TabIndex = 71;
		this.tbName2.Location = new System.Drawing.Point(431, 46);
		this.tbName2.Name = "tbName2";
		this.tbName2.Size = new System.Drawing.Size(62, 21);
		this.tbName2.TabIndex = 70;
		this.tbName1.Location = new System.Drawing.Point(431, 11);
		this.tbName1.Name = "tbName1";
		this.tbName1.Size = new System.Drawing.Size(62, 21);
		this.tbName1.TabIndex = 69;
		this.tbDelay.Location = new System.Drawing.Point(591, 7);
		this.tbDelay.Name = "tbDelay";
		this.tbDelay.Size = new System.Drawing.Size(48, 21);
		this.tbDelay.TabIndex = 68;
		this.label9.AutoSize = true;
		this.label9.Location = new System.Drawing.Point(499, 13);
		this.label9.Name = "label9";
		this.label9.Size = new System.Drawing.Size(53, 12);
		this.label9.TabIndex = 67;
		this.label9.Text = "采样延迟";
		this.tabPage4.Controls.Add(this.chbCalibra);
		this.tabPage4.Controls.Add(this.cb_checkBit);
		this.tabPage4.Controls.Add(this.label43);
		this.tabPage4.Controls.Add(this.cb_stopBit);
		this.tabPage4.Controls.Add(this.label42);
		this.tabPage4.Controls.Add(this.cb_dataBit);
		this.tabPage4.Controls.Add(this.label41);
		this.tabPage4.Controls.Add(this.cb_baudRate);
		this.tabPage4.Controls.Add(this.label40);
		this.tabPage4.Controls.Add(this.cb_portNameSend);
		this.tabPage4.Controls.Add(this.label38);
		this.tabPage4.Controls.Add(this.btnMonit);
		this.tabPage4.Controls.Add(this.btnModbusSave);
		this.tabPage4.Controls.Add(this.tbModbusAddress);
		this.tabPage4.Controls.Add(this.label31);
		this.tabPage4.Controls.Add(this.label30);
		this.tabPage4.Location = new System.Drawing.Point(4, 22);
		this.tabPage4.Name = "tabPage4";
		this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
		this.tabPage4.Size = new System.Drawing.Size(767, 131);
		this.tabPage4.TabIndex = 3;
		this.tabPage4.Text = "Modbus参数";
		this.tabPage4.UseVisualStyleBackColor = true;
		this.chbCalibra.AutoSize = true;
		this.chbCalibra.Location = new System.Drawing.Point(196, 26);
		this.chbCalibra.Name = "chbCalibra";
		this.chbCalibra.Size = new System.Drawing.Size(48, 16);
		this.chbCalibra.TabIndex = 17;
		this.chbCalibra.Text = "标定";
		this.chbCalibra.UseVisualStyleBackColor = true;
		this.chbCalibra.Visible = false;
		this.cb_checkBit.FormattingEnabled = true;
		this.cb_checkBit.Location = new System.Drawing.Point(595, 47);
		this.cb_checkBit.Name = "cb_checkBit";
		this.cb_checkBit.Size = new System.Drawing.Size(111, 20);
		this.cb_checkBit.TabIndex = 16;
		this.cb_checkBit.Visible = false;
		this.label43.AutoSize = true;
		this.label43.Font = new System.Drawing.Font("微软雅黑", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		this.label43.Location = new System.Drawing.Point(538, 50);
		this.label43.Name = "label43";
		this.label43.Size = new System.Drawing.Size(56, 17);
		this.label43.TabIndex = 15;
		this.label43.Text = "校验位：";
		this.label43.Visible = false;
		this.cb_stopBit.FormattingEnabled = true;
		this.cb_stopBit.Location = new System.Drawing.Point(595, 15);
		this.cb_stopBit.Name = "cb_stopBit";
		this.cb_stopBit.Size = new System.Drawing.Size(111, 20);
		this.cb_stopBit.TabIndex = 14;
		this.label42.AutoSize = true;
		this.label42.Font = new System.Drawing.Font("微软雅黑", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		this.label42.Location = new System.Drawing.Point(538, 18);
		this.label42.Name = "label42";
		this.label42.Size = new System.Drawing.Size(56, 17);
		this.label42.TabIndex = 13;
		this.label42.Text = "停止位：";
		this.cb_dataBit.FormattingEnabled = true;
		this.cb_dataBit.Location = new System.Drawing.Point(388, 82);
		this.cb_dataBit.Name = "cb_dataBit";
		this.cb_dataBit.Size = new System.Drawing.Size(111, 20);
		this.cb_dataBit.TabIndex = 12;
		this.label41.AutoSize = true;
		this.label41.Font = new System.Drawing.Font("微软雅黑", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		this.label41.Location = new System.Drawing.Point(318, 85);
		this.label41.Name = "label41";
		this.label41.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.label41.Size = new System.Drawing.Size(56, 17);
		this.label41.TabIndex = 11;
		this.label41.Text = "数据位：";
		this.label41.TextAlign = System.Drawing.ContentAlignment.TopRight;
		this.cb_baudRate.FormattingEnabled = true;
		this.cb_baudRate.Location = new System.Drawing.Point(388, 47);
		this.cb_baudRate.Name = "cb_baudRate";
		this.cb_baudRate.Size = new System.Drawing.Size(111, 20);
		this.cb_baudRate.TabIndex = 10;
		this.label40.AutoSize = true;
		this.label40.Font = new System.Drawing.Font("微软雅黑", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		this.label40.Location = new System.Drawing.Point(318, 50);
		this.label40.Name = "label40";
		this.label40.Size = new System.Drawing.Size(56, 17);
		this.label40.TabIndex = 9;
		this.label40.Text = "波特率：";
		this.label40.TextAlign = System.Drawing.ContentAlignment.TopRight;
		this.cb_portNameSend.FormattingEnabled = true;
		this.cb_portNameSend.Location = new System.Drawing.Point(388, 12);
		this.cb_portNameSend.Name = "cb_portNameSend";
		this.cb_portNameSend.Size = new System.Drawing.Size(111, 20);
		this.cb_portNameSend.TabIndex = 8;
		this.label38.AutoSize = true;
		this.label38.Font = new System.Drawing.Font("微软雅黑", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		this.label38.Location = new System.Drawing.Point(318, 15);
		this.label38.Name = "label38";
		this.label38.Size = new System.Drawing.Size(56, 17);
		this.label38.TabIndex = 7;
		this.label38.Text = "串口号：";
		this.label38.TextAlign = System.Drawing.ContentAlignment.TopRight;
		this.btnMonit.Location = new System.Drawing.Point(612, 85);
		this.btnMonit.Name = "btnMonit";
		this.btnMonit.Size = new System.Drawing.Size(89, 23);
		this.btnMonit.TabIndex = 6;
		this.btnMonit.Text = "回到监控界面";
		this.btnMonit.UseVisualStyleBackColor = true;
		this.btnMonit.Visible = false;
		this.btnMonit.Click += new System.EventHandler(btnMonit_Click);
		this.btnModbusSave.Location = new System.Drawing.Point(95, 58);
		this.btnModbusSave.Name = "btnModbusSave";
		this.btnModbusSave.Size = new System.Drawing.Size(75, 23);
		this.btnModbusSave.TabIndex = 5;
		this.btnModbusSave.Text = "保存并应用";
		this.btnModbusSave.UseVisualStyleBackColor = true;
		this.btnModbusSave.Click += new System.EventHandler(btnModbusSave_Click);
		this.tbModbusAddress.Location = new System.Drawing.Point(95, 21);
		this.tbModbusAddress.Name = "tbModbusAddress";
		this.tbModbusAddress.Size = new System.Drawing.Size(79, 21);
		this.tbModbusAddress.TabIndex = 4;
		this.tbModbusAddress.TextChanged += new System.EventHandler(tbModbusAddress_TextChanged);
		this.label31.AutoSize = true;
		this.label31.Location = new System.Drawing.Point(25, 28);
		this.label31.Name = "label31";
		this.label31.Size = new System.Drawing.Size(65, 12);
		this.label31.TabIndex = 3;
		this.label31.Text = "上传站号：";
		this.label30.AutoSize = true;
		this.label30.Location = new System.Drawing.Point(25, 93);
		this.label30.Name = "label30";
		this.label30.Size = new System.Drawing.Size(149, 12);
		this.label30.TabIndex = 2;
		this.label30.Text = "注：改变串口设置重启生效";
		this.tabPage5.Controls.Add(this.btnSavePeakTime);
		this.tabPage5.Controls.Add(this.label87);
		this.tabPage5.Controls.Add(this.label88);
		this.tabPage5.Controls.Add(this.label84);
		this.tabPage5.Controls.Add(this.label83);
		this.tabPage5.Controls.Add(this.tbTimeOFF4);
		this.tabPage5.Controls.Add(this.tbTimeON4);
		this.tabPage5.Controls.Add(this.tbTimeOFF3);
		this.tabPage5.Controls.Add(this.tbTimeON3);
		this.tabPage5.Controls.Add(this.label86);
		this.tabPage5.Controls.Add(this.label89);
		this.tabPage5.Controls.Add(this.label85);
		this.tabPage5.Controls.Add(this.label82);
		this.tabPage5.Controls.Add(this.tbTimeOFF2);
		this.tabPage5.Controls.Add(this.tbTimeON2);
		this.tabPage5.Controls.Add(this.tbTimeOFF1);
		this.tabPage5.Controls.Add(this.tbTimeON1);
		this.tabPage5.Location = new System.Drawing.Point(4, 22);
		this.tabPage5.Name = "tabPage5";
		this.tabPage5.Padding = new System.Windows.Forms.Padding(3);
		this.tabPage5.Size = new System.Drawing.Size(767, 131);
		this.tabPage5.TabIndex = 4;
		this.tabPage5.Text = "强检峰";
		this.tabPage5.UseVisualStyleBackColor = true;
		this.btnSavePeakTime.Location = new System.Drawing.Point(664, 82);
		this.btnSavePeakTime.Name = "btnSavePeakTime";
		this.btnSavePeakTime.Size = new System.Drawing.Size(75, 23);
		this.btnSavePeakTime.TabIndex = 32;
		this.btnSavePeakTime.Text = "保存";
		this.btnSavePeakTime.UseVisualStyleBackColor = true;
		this.btnSavePeakTime.Click += new System.EventHandler(btnSavePeakTime_Click);
		this.label87.AutoSize = true;
		this.label87.Font = new System.Drawing.Font("宋体", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		this.label87.Location = new System.Drawing.Point(483, 32);
		this.label87.Name = "label87";
		this.label87.Size = new System.Drawing.Size(40, 16);
		this.label87.TabIndex = 31;
		this.label87.Text = "结束";
		this.label88.AutoSize = true;
		this.label88.Font = new System.Drawing.Font("宋体", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		this.label88.Location = new System.Drawing.Point(483, 58);
		this.label88.Name = "label88";
		this.label88.Size = new System.Drawing.Size(40, 16);
		this.label88.TabIndex = 30;
		this.label88.Text = "结束";
		this.label84.AutoSize = true;
		this.label84.Font = new System.Drawing.Font("宋体", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		this.label84.Location = new System.Drawing.Point(337, 32);
		this.label84.Name = "label84";
		this.label84.Size = new System.Drawing.Size(40, 16);
		this.label84.TabIndex = 29;
		this.label84.Text = "开始";
		this.label83.AutoSize = true;
		this.label83.Font = new System.Drawing.Font("宋体", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		this.label83.Location = new System.Drawing.Point(337, 58);
		this.label83.Name = "label83";
		this.label83.Size = new System.Drawing.Size(40, 16);
		this.label83.TabIndex = 28;
		this.label83.Text = "开始";
		this.tbTimeOFF4.Location = new System.Drawing.Point(532, 58);
		this.tbTimeOFF4.Name = "tbTimeOFF4";
		this.tbTimeOFF4.Size = new System.Drawing.Size(80, 21);
		this.tbTimeOFF4.TabIndex = 27;
		this.tbTimeOFF4.Text = "0";
		this.tbTimeON4.Location = new System.Drawing.Point(397, 58);
		this.tbTimeON4.Name = "tbTimeON4";
		this.tbTimeON4.Size = new System.Drawing.Size(80, 21);
		this.tbTimeON4.TabIndex = 26;
		this.tbTimeON4.Text = "0";
		this.tbTimeOFF3.Location = new System.Drawing.Point(532, 27);
		this.tbTimeOFF3.Name = "tbTimeOFF3";
		this.tbTimeOFF3.Size = new System.Drawing.Size(80, 21);
		this.tbTimeOFF3.TabIndex = 25;
		this.tbTimeOFF3.Text = "0";
		this.tbTimeON3.Location = new System.Drawing.Point(397, 27);
		this.tbTimeON3.Name = "tbTimeON3";
		this.tbTimeON3.Size = new System.Drawing.Size(80, 21);
		this.tbTimeON3.TabIndex = 24;
		this.tbTimeON3.Text = "0";
		this.label86.AutoSize = true;
		this.label86.Font = new System.Drawing.Font("宋体", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		this.label86.Location = new System.Drawing.Point(176, 63);
		this.label86.Name = "label86";
		this.label86.Size = new System.Drawing.Size(40, 16);
		this.label86.TabIndex = 23;
		this.label86.Text = "结束";
		this.label89.AutoSize = true;
		this.label89.Font = new System.Drawing.Font("宋体", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		this.label89.Location = new System.Drawing.Point(176, 33);
		this.label89.Name = "label89";
		this.label89.Size = new System.Drawing.Size(40, 16);
		this.label89.TabIndex = 22;
		this.label89.Text = "结束";
		this.label85.AutoSize = true;
		this.label85.Font = new System.Drawing.Font("宋体", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		this.label85.Location = new System.Drawing.Point(30, 63);
		this.label85.Name = "label85";
		this.label85.Size = new System.Drawing.Size(40, 16);
		this.label85.TabIndex = 21;
		this.label85.Text = "开始";
		this.label82.AutoSize = true;
		this.label82.Font = new System.Drawing.Font("宋体", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		this.label82.Location = new System.Drawing.Point(30, 33);
		this.label82.Name = "label82";
		this.label82.Size = new System.Drawing.Size(40, 16);
		this.label82.TabIndex = 20;
		this.label82.Text = "开始";
		this.tbTimeOFF2.Location = new System.Drawing.Point(225, 58);
		this.tbTimeOFF2.Name = "tbTimeOFF2";
		this.tbTimeOFF2.Size = new System.Drawing.Size(80, 21);
		this.tbTimeOFF2.TabIndex = 19;
		this.tbTimeOFF2.Text = "0";
		this.tbTimeON2.Location = new System.Drawing.Point(90, 58);
		this.tbTimeON2.Name = "tbTimeON2";
		this.tbTimeON2.Size = new System.Drawing.Size(80, 21);
		this.tbTimeON2.TabIndex = 18;
		this.tbTimeON2.Text = "0";
		this.tbTimeOFF1.Location = new System.Drawing.Point(225, 28);
		this.tbTimeOFF1.Name = "tbTimeOFF1";
		this.tbTimeOFF1.Size = new System.Drawing.Size(80, 21);
		this.tbTimeOFF1.TabIndex = 17;
		this.tbTimeOFF1.Text = "0";
		this.tbTimeON1.Location = new System.Drawing.Point(90, 28);
		this.tbTimeON1.Name = "tbTimeON1";
		this.tbTimeON1.Size = new System.Drawing.Size(80, 21);
		this.tbTimeON1.TabIndex = 16;
		this.tbTimeON1.Text = "0";
		this.tabPage6.Controls.Add(this.btnConver);
		this.tabPage6.Controls.Add(this.cbDectorNumber);
		this.tabPage6.Controls.Add(this.tbPAir2);
		this.tabPage6.Controls.Add(this.label39);
		this.tabPage6.Controls.Add(this.tbPHH2);
		this.tabPage6.Controls.Add(this.labPHH2);
		this.tabPage6.Controls.Add(this.tbPAir1);
		this.tabPage6.Controls.Add(this.label37);
		this.tabPage6.Controls.Add(this.tbPHH1);
		this.tabPage6.Controls.Add(this.label36);
		this.tabPage6.Controls.Add(this.tbPINJ4);
		this.tabPage6.Controls.Add(this.label35);
		this.tabPage6.Controls.Add(this.tbPINJ3);
		this.tabPage6.Controls.Add(this.label34);
		this.tabPage6.Controls.Add(this.tbPINJ2);
		this.tabPage6.Controls.Add(this.label33);
		this.tabPage6.Controls.Add(this.tbPINJ1);
		this.tabPage6.Controls.Add(this.label32);
		this.tabPage6.Location = new System.Drawing.Point(4, 22);
		this.tabPage6.Name = "tabPage6";
		this.tabPage6.Padding = new System.Windows.Forms.Padding(3);
		this.tabPage6.Size = new System.Drawing.Size(767, 131);
		this.tabPage6.TabIndex = 5;
		this.tabPage6.Text = "压力";
		this.tabPage6.UseVisualStyleBackColor = true;
		this.btnConver.Location = new System.Drawing.Point(660, 71);
		this.btnConver.Name = "btnConver";
		this.btnConver.Size = new System.Drawing.Size(75, 23);
		this.btnConver.TabIndex = 17;
		this.btnConver.Text = "总烃换算";
		this.btnConver.UseVisualStyleBackColor = true;
		this.btnConver.Click += new System.EventHandler(btnConver_Click);
		this.cbDectorNumber.FormattingEnabled = true;
		this.cbDectorNumber.Items.AddRange(new object[3] { "单检测器", "双检测器", "EPC" });
		this.cbDectorNumber.Location = new System.Drawing.Point(536, 81);
		this.cbDectorNumber.Name = "cbDectorNumber";
		this.cbDectorNumber.Size = new System.Drawing.Size(100, 20);
		this.cbDectorNumber.TabIndex = 16;
		this.cbDectorNumber.SelectedIndexChanged += new System.EventHandler(cbDectorNumber_SelectedIndexChanged);
		this.tbPAir2.Location = new System.Drawing.Point(536, 11);
		this.tbPAir2.Name = "tbPAir2";
		this.tbPAir2.Size = new System.Drawing.Size(100, 21);
		this.tbPAir2.TabIndex = 15;
		this.label39.AutoSize = true;
		this.label39.Location = new System.Drawing.Point(486, 15);
		this.label39.Name = "label39";
		this.label39.Size = new System.Drawing.Size(35, 12);
		this.label39.TabIndex = 14;
		this.label39.Text = "氢气2";
		this.tbPHH2.Location = new System.Drawing.Point(536, 44);
		this.tbPHH2.Name = "tbPHH2";
		this.tbPHH2.Size = new System.Drawing.Size(100, 21);
		this.tbPHH2.TabIndex = 13;
		this.labPHH2.AutoSize = true;
		this.labPHH2.Location = new System.Drawing.Point(486, 48);
		this.labPHH2.Name = "labPHH2";
		this.labPHH2.Size = new System.Drawing.Size(41, 12);
		this.labPHH2.TabIndex = 12;
		this.labPHH2.Text = "空气2:";
		this.tbPAir1.Location = new System.Drawing.Point(287, 85);
		this.tbPAir1.Name = "tbPAir1";
		this.tbPAir1.Size = new System.Drawing.Size(100, 21);
		this.tbPAir1.TabIndex = 11;
		this.label37.AutoSize = true;
		this.label37.Location = new System.Drawing.Point(237, 89);
		this.label37.Name = "label37";
		this.label37.Size = new System.Drawing.Size(35, 12);
		this.label37.TabIndex = 10;
		this.label37.Text = "空气1";
		this.tbPHH1.Location = new System.Drawing.Point(287, 49);
		this.tbPHH1.Name = "tbPHH1";
		this.tbPHH1.Size = new System.Drawing.Size(100, 21);
		this.tbPHH1.TabIndex = 9;
		this.label36.AutoSize = true;
		this.label36.Location = new System.Drawing.Point(237, 53);
		this.label36.Name = "label36";
		this.label36.Size = new System.Drawing.Size(35, 12);
		this.label36.TabIndex = 8;
		this.label36.Text = "氢气1";
		this.tbPINJ4.Location = new System.Drawing.Point(58, 49);
		this.tbPINJ4.Name = "tbPINJ4";
		this.tbPINJ4.Size = new System.Drawing.Size(100, 21);
		this.tbPINJ4.TabIndex = 7;
		this.label35.AutoSize = true;
		this.label35.Location = new System.Drawing.Point(8, 53);
		this.label35.Name = "label35";
		this.label35.Size = new System.Drawing.Size(41, 12);
		this.label35.TabIndex = 6;
		this.label35.Text = "载气2:";
		this.tbPINJ3.Location = new System.Drawing.Point(287, 11);
		this.tbPINJ3.Name = "tbPINJ3";
		this.tbPINJ3.Size = new System.Drawing.Size(100, 21);
		this.tbPINJ3.TabIndex = 5;
		this.label34.AutoSize = true;
		this.label34.Location = new System.Drawing.Point(237, 15);
		this.label34.Name = "label34";
		this.label34.Size = new System.Drawing.Size(41, 12);
		this.label34.TabIndex = 4;
		this.label34.Text = "载气4:";
		this.tbPINJ2.Location = new System.Drawing.Point(58, 84);
		this.tbPINJ2.Name = "tbPINJ2";
		this.tbPINJ2.Size = new System.Drawing.Size(100, 21);
		this.tbPINJ2.TabIndex = 3;
		this.label33.AutoSize = true;
		this.label33.Location = new System.Drawing.Point(8, 88);
		this.label33.Name = "label33";
		this.label33.Size = new System.Drawing.Size(41, 12);
		this.label33.TabIndex = 2;
		this.label33.Text = "载气3:";
		this.tbPINJ1.Location = new System.Drawing.Point(58, 11);
		this.tbPINJ1.Name = "tbPINJ1";
		this.tbPINJ1.Size = new System.Drawing.Size(100, 21);
		this.tbPINJ1.TabIndex = 1;
		this.label32.AutoSize = true;
		this.label32.Location = new System.Drawing.Point(8, 15);
		this.label32.Name = "label32";
		this.label32.Size = new System.Drawing.Size(41, 12);
		this.label32.TabIndex = 0;
		this.label32.Text = "载气1:";
		this.tabPage7.Controls.Add(this.btnEPCSet);
		this.tabPage7.Controls.Add(this.label78);
		this.tabPage7.Controls.Add(this.tbWeiChuiCur1);
		this.tabPage7.Controls.Add(this.label47);
		this.tabPage7.Controls.Add(this.label61);
		this.tabPage7.Controls.Add(this.label48);
		this.tabPage7.Controls.Add(this.tbColPreSet1);
		this.tabPage7.Controls.Add(this.label49);
		this.tabPage7.Controls.Add(this.tbColPreCur1);
		this.tabPage7.Controls.Add(this.label50);
		this.tabPage7.Controls.Add(this.label60);
		this.tabPage7.Controls.Add(this.label51);
		this.tabPage7.Controls.Add(this.tbHHSet1);
		this.tabPage7.Controls.Add(this.label52);
		this.tabPage7.Controls.Add(this.tbHHCur1);
		this.tabPage7.Controls.Add(this.label59);
		this.tabPage7.Controls.Add(this.tbWeiChuiSet1);
		this.tabPage7.Controls.Add(this.tbAirSet1);
		this.tabPage7.Controls.Add(this.label53);
		this.tabPage7.Controls.Add(this.tbAirCur1);
		this.tabPage7.Controls.Add(this.tbColPreCur3);
		this.tabPage7.Controls.Add(this.label58);
		this.tabPage7.Controls.Add(this.tbColPreSet3);
		this.tabPage7.Controls.Add(this.label57);
		this.tabPage7.Controls.Add(this.label54);
		this.tabPage7.Controls.Add(this.label56);
		this.tabPage7.Controls.Add(this.tbColPreCur2);
		this.tabPage7.Controls.Add(this.label55);
		this.tabPage7.Controls.Add(this.tbColPreSet2);
		this.tabPage7.Location = new System.Drawing.Point(4, 22);
		this.tabPage7.Name = "tabPage7";
		this.tabPage7.Size = new System.Drawing.Size(767, 131);
		this.tabPage7.TabIndex = 6;
		this.tabPage7.Text = "流量";
		this.tabPage7.UseVisualStyleBackColor = true;
		this.btnEPCSet.ForeColor = System.Drawing.Color.Black;
		this.btnEPCSet.Location = new System.Drawing.Point(419, 14);
		this.btnEPCSet.Name = "btnEPCSet";
		this.btnEPCSet.Size = new System.Drawing.Size(75, 23);
		this.btnEPCSet.TabIndex = 56;
		this.btnEPCSet.Text = "流量设定";
		this.btnEPCSet.UseVisualStyleBackColor = true;
		this.btnEPCSet.Click += new System.EventHandler(btnEPCSet_Click);
		this.label78.AutoSize = true;
		this.label78.ForeColor = System.Drawing.Color.Black;
		this.label78.Location = new System.Drawing.Point(297, 14);
		this.label78.Name = "label78";
		this.label78.Size = new System.Drawing.Size(41, 12);
		this.label78.TabIndex = 37;
		this.label78.Text = "实测值";
		this.tbWeiChuiCur1.Location = new System.Drawing.Point(297, 95);
		this.tbWeiChuiCur1.Name = "tbWeiChuiCur1";
		this.tbWeiChuiCur1.Size = new System.Drawing.Size(41, 21);
		this.tbWeiChuiCur1.TabIndex = 31;
		this.label47.AutoSize = true;
		this.label47.ForeColor = System.Drawing.Color.Black;
		this.label47.Location = new System.Drawing.Point(251, 14);
		this.label47.Name = "label47";
		this.label47.Size = new System.Drawing.Size(41, 12);
		this.label47.TabIndex = 36;
		this.label47.Text = "设定值";
		this.label61.AutoSize = true;
		this.label61.ForeColor = System.Drawing.Color.Black;
		this.label61.Location = new System.Drawing.Point(21, 36);
		this.label61.Name = "label61";
		this.label61.Size = new System.Drawing.Size(41, 12);
		this.label61.TabIndex = 0;
		this.label61.Text = "载气1:";
		this.label48.AutoSize = true;
		this.label48.ForeColor = System.Drawing.Color.Black;
		this.label48.Location = new System.Drawing.Point(107, 14);
		this.label48.Name = "label48";
		this.label48.Size = new System.Drawing.Size(41, 12);
		this.label48.TabIndex = 35;
		this.label48.Text = "实测值";
		this.tbColPreSet1.Location = new System.Drawing.Point(62, 32);
		this.tbColPreSet1.Name = "tbColPreSet1";
		this.tbColPreSet1.Size = new System.Drawing.Size(41, 21);
		this.tbColPreSet1.TabIndex = 1;
		this.label49.AutoSize = true;
		this.label49.ForeColor = System.Drawing.Color.Black;
		this.label49.Location = new System.Drawing.Point(63, 14);
		this.label49.Name = "label49";
		this.label49.Size = new System.Drawing.Size(41, 12);
		this.label49.TabIndex = 34;
		this.label49.Text = "设定值";
		this.tbColPreCur1.Location = new System.Drawing.Point(106, 32);
		this.tbColPreCur1.Name = "tbColPreCur1";
		this.tbColPreCur1.Size = new System.Drawing.Size(41, 21);
		this.tbColPreCur1.TabIndex = 4;
		this.label50.AutoSize = true;
		this.label50.ForeColor = System.Drawing.Color.Black;
		this.label50.Location = new System.Drawing.Point(346, 99);
		this.label50.Name = "label50";
		this.label50.Size = new System.Drawing.Size(41, 12);
		this.label50.TabIndex = 33;
		this.label50.Text = "ml/min";
		this.label60.AutoSize = true;
		this.label60.ForeColor = System.Drawing.Color.Black;
		this.label60.Location = new System.Drawing.Point(211, 36);
		this.label60.Name = "label60";
		this.label60.Size = new System.Drawing.Size(41, 12);
		this.label60.TabIndex = 5;
		this.label60.Text = "氢气1:";
		this.label51.AutoSize = true;
		this.label51.ForeColor = System.Drawing.Color.Black;
		this.label51.Location = new System.Drawing.Point(153, 99);
		this.label51.Name = "label51";
		this.label51.Size = new System.Drawing.Size(41, 12);
		this.label51.TabIndex = 32;
		this.label51.Text = "ml/min";
		this.tbHHSet1.Location = new System.Drawing.Point(252, 32);
		this.tbHHSet1.Name = "tbHHSet1";
		this.tbHHSet1.Size = new System.Drawing.Size(41, 21);
		this.tbHHSet1.TabIndex = 6;
		this.label52.AutoSize = true;
		this.label52.ForeColor = System.Drawing.Color.Black;
		this.label52.Location = new System.Drawing.Point(154, 67);
		this.label52.Name = "label52";
		this.label52.Size = new System.Drawing.Size(41, 12);
		this.label52.TabIndex = 22;
		this.label52.Text = "ml/min";
		this.tbHHCur1.Location = new System.Drawing.Point(297, 32);
		this.tbHHCur1.Name = "tbHHCur1";
		this.tbHHCur1.Size = new System.Drawing.Size(41, 21);
		this.tbHHCur1.TabIndex = 7;
		this.label59.AutoSize = true;
		this.label59.ForeColor = System.Drawing.Color.Black;
		this.label59.Location = new System.Drawing.Point(211, 67);
		this.label59.Name = "label59";
		this.label59.Size = new System.Drawing.Size(41, 12);
		this.label59.TabIndex = 8;
		this.label59.Text = "空气1:";
		this.tbWeiChuiSet1.Location = new System.Drawing.Point(252, 95);
		this.tbWeiChuiSet1.Name = "tbWeiChuiSet1";
		this.tbWeiChuiSet1.Size = new System.Drawing.Size(41, 21);
		this.tbWeiChuiSet1.TabIndex = 30;
		this.tbAirSet1.Location = new System.Drawing.Point(252, 63);
		this.tbAirSet1.Name = "tbAirSet1";
		this.tbAirSet1.Size = new System.Drawing.Size(41, 21);
		this.tbAirSet1.TabIndex = 9;
		this.label53.AutoSize = true;
		this.label53.ForeColor = System.Drawing.Color.Black;
		this.label53.Location = new System.Drawing.Point(211, 99);
		this.label53.Name = "label53";
		this.label53.Size = new System.Drawing.Size(41, 12);
		this.label53.TabIndex = 29;
		this.label53.Text = "尾吹1:";
		this.tbAirCur1.Location = new System.Drawing.Point(297, 63);
		this.tbAirCur1.Name = "tbAirCur1";
		this.tbAirCur1.Size = new System.Drawing.Size(41, 21);
		this.tbAirCur1.TabIndex = 10;
		this.tbColPreCur3.Location = new System.Drawing.Point(106, 95);
		this.tbColPreCur3.Name = "tbColPreCur3";
		this.tbColPreCur3.Size = new System.Drawing.Size(41, 21);
		this.tbColPreCur3.TabIndex = 28;
		this.label58.AutoSize = true;
		this.label58.ForeColor = System.Drawing.Color.Black;
		this.label58.Location = new System.Drawing.Point(154, 36);
		this.label58.Name = "label58";
		this.label58.Size = new System.Drawing.Size(41, 12);
		this.label58.TabIndex = 0;
		this.label58.Text = "ml/min";
		this.tbColPreSet3.Location = new System.Drawing.Point(62, 95);
		this.tbColPreSet3.Name = "tbColPreSet3";
		this.tbColPreSet3.Size = new System.Drawing.Size(41, 21);
		this.tbColPreSet3.TabIndex = 27;
		this.label57.AutoSize = true;
		this.label57.ForeColor = System.Drawing.Color.Black;
		this.label57.Location = new System.Drawing.Point(346, 36);
		this.label57.Name = "label57";
		this.label57.Size = new System.Drawing.Size(41, 12);
		this.label57.TabIndex = 20;
		this.label57.Text = "ml/min";
		this.label54.AutoSize = true;
		this.label54.ForeColor = System.Drawing.Color.Black;
		this.label54.Location = new System.Drawing.Point(21, 99);
		this.label54.Name = "label54";
		this.label54.Size = new System.Drawing.Size(41, 12);
		this.label54.TabIndex = 26;
		this.label54.Text = "载气3:";
		this.label56.AutoSize = true;
		this.label56.ForeColor = System.Drawing.Color.Black;
		this.label56.Location = new System.Drawing.Point(346, 67);
		this.label56.Name = "label56";
		this.label56.Size = new System.Drawing.Size(41, 12);
		this.label56.TabIndex = 21;
		this.label56.Text = "ml/min";
		this.tbColPreCur2.Location = new System.Drawing.Point(106, 63);
		this.tbColPreCur2.Name = "tbColPreCur2";
		this.tbColPreCur2.Size = new System.Drawing.Size(41, 21);
		this.tbColPreCur2.TabIndex = 25;
		this.label55.AutoSize = true;
		this.label55.ForeColor = System.Drawing.Color.Black;
		this.label55.Location = new System.Drawing.Point(21, 67);
		this.label55.Name = "label55";
		this.label55.Size = new System.Drawing.Size(41, 12);
		this.label55.TabIndex = 23;
		this.label55.Text = "载气2:";
		this.tbColPreSet2.Location = new System.Drawing.Point(62, 63);
		this.tbColPreSet2.Name = "tbColPreSet2";
		this.tbColPreSet2.Size = new System.Drawing.Size(41, 21);
		this.tbColPreSet2.TabIndex = 24;
		this.timer2.Interval = 1000;
		this.timer2.Tick += new System.EventHandler(timer2_Tick);
		this.chbCombinDector.AutoSize = true;
		this.chbCombinDector.ForeColor = System.Drawing.Color.Black;
		this.chbCombinDector.Location = new System.Drawing.Point(708, 4);
		this.chbCombinDector.Name = "chbCombinDector";
		this.chbCombinDector.Size = new System.Drawing.Size(72, 16);
		this.chbCombinDector.TabIndex = 106;
		this.chbCombinDector.Text = "合并运算";
		this.chbCombinDector.UseVisualStyleBackColor = true;
		this.chbCombinDector.CheckedChanged += new System.EventHandler(chbCombinDector_CheckedChanged);
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.Controls.Add(this.chbCombinDector);
		base.Controls.Add(this.tabControl1);
		base.Controls.Add(this.gbNHMC1);
		base.Controls.Add(this.btnCali);
		base.Controls.Add(this.panel1);
		base.Name = "OnlineCtrl";
		base.Padding = new System.Windows.Forms.Padding(3);
		base.Size = new System.Drawing.Size(780, 199);
		base.Load += new System.EventHandler(OnlineCtrl_Load);
		this.gbBenXW.ResumeLayout(false);
		this.gbBenXW.PerformLayout();
		this.panel1.ResumeLayout(false);
		this.panel1.PerformLayout();
		this.gbNHMC1.ResumeLayout(false);
		this.gbNHMC1.PerformLayout();
		this.tabControl1.ResumeLayout(false);
		this.tabPage1.ResumeLayout(false);
		this.tabPage1.PerformLayout();
		this.groupBox1.ResumeLayout(false);
		this.groupBox1.PerformLayout();
		this.tabPage2.ResumeLayout(false);
		this.panel3.ResumeLayout(false);
		this.panel3.PerformLayout();
		this.tabPage3.ResumeLayout(false);
		this.panel2.ResumeLayout(false);
		this.panel2.PerformLayout();
		this.tabPage4.ResumeLayout(false);
		this.tabPage4.PerformLayout();
		this.tabPage5.ResumeLayout(false);
		this.tabPage5.PerformLayout();
		this.tabPage6.ResumeLayout(false);
		this.tabPage6.PerformLayout();
		this.tabPage7.ResumeLayout(false);
		this.tabPage7.PerformLayout();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
