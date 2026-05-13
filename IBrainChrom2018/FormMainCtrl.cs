using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;
using IBrainChrom2018.ChromFile;
using IBrainChrom2018.Unit;

namespace IBrainChrom2018;

public class FormMainCtrl : Form, FrmChromatManagerInterface, ChromFormInterface
{
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	public delegate void CallbackFun(int i);

	public const uint WM_SYSCOMMAND = 274u;

	public const uint SC_RESTORE = 61728u;

	private string OnScreenKeyboadApplication = "osk.exe";

	private ChromDeviceListMgr cdlMgr = ChromDeviceListMgr.Create();

	public int iChannel = 0;

	private SystemParam sysParam = SystemParam.Create();

	public bool bStart1 = false;

	public bool bStart2 = false;

	public uint cntChannel1 = 0u;

	public uint cntChannel2 = 0u;

	public uint cntAnalyze1 = 0u;

	public uint cntAnalyze2 = 0u;

	private short cntDelay1 = 0;

	private short cntDelay2 = 0;

	public bool bAlarm = false;

	public string[] strAlarmArray;

	public string strAlarmFile;

	private bool bChangeLanguage = false;

	public bool selfRestart = false;

	public int IsAutoCalibra = 0;

	public int AutoCalibraPoint = 0;

	private float shuaijian;

	private float shuaijian2;

	private float shuaijian3;

	public long CountAnalyse = 0L;

	public int iCntAlarmPress = 0;

	public bool bAlarmMy = false;

	public bool bAutoCycle1 = true;

	public bool bAutoCycle2 = true;

	public string CurrentGCID = "";

	public int FIDCount = 4;

	public int StateYiqi = 0;

	public FrmMsetup fset;

	private bool m_bLoading = true;

	private bool m_bIsClosing = false;

	public static string string_0 = "";

	private Chromatogram chromatogram_0 = new Chromatogram();

	public FrmChromatManager FrmEquip;

	public FrmTempNameSet FTNS;

	public FrmGasNameSet FGNS;

	public Frmmultivalve Fmultivalve;

	public int SetFireLengthValue;

	public byte[] comData = new byte[1000];

	public static FormMainCtrl fromMain = null;

	public ChromFormCtrl chromFormCtrl2;

	private TabControl tabCtrl2;

	public VocCtrl vocCtrl2;

	private FormMainParam frmParam = FormMainParam.Create();

	private FrmOperLog frmOperLog_0;

	private IContainer icontainer_0;

	private SplitContainer splitContainer1;

	private MenuStrip menuStrip1;

	private ToolStripMenuItem 文件FToolStripMenuItem;

	private SplitContainer splitContainer2;

	private ToolStripMenuItem 系统SToolStripMenuItem;

	private ToolStripMenuItem 帮助HToolStripMenuItem;

	private TabPage tabPage11;

	private TabPage tabPage12;

	private TabPage tabPage13;

	private TabPage tabPage14;

	private ToolStripButton toolStripButton2;

	private ToolStripMenuItem 测试ToolStripMenuItem;

	private DataGridViewTextBoxColumn dataGridViewTextBoxColumn17;

	private DataGridViewTextBoxColumn dataGridViewTextBoxColumn20;

	private DataGridViewTextBoxColumn dataGridViewTextBoxColumn21;

	private DataGridViewTextBoxColumn dataGridViewTextBoxColumn22;

	private DataGridViewTextBoxColumn dataGridViewTextBoxColumn23;

	private DataGridViewTextBoxColumn dataGridViewTextBoxColumn24;

	private DataGridViewTextBoxColumn dataGridViewTextBoxColumn25;

	private DataGridViewTextBoxColumn dataGridViewTextBoxColumn26;

	private DataGridViewTextBoxColumn dataGridViewTextBoxColumn27;

	private DataGridViewTextBoxColumn dataGridViewTextBoxColumn28;

	private DataGridViewTextBoxColumn dataGridViewTextBoxColumn29;

	private DataGridViewTextBoxColumn dataGridViewTextBoxColumn30;

	private System.Windows.Forms.Timer timer_0;

	private ToolStripStatusLabel toolStripStatusLabel1;

	private ToolStripStatusLabel toolStripStatusServer;

	private DataGridViewTextBoxColumn dataGridViewTextBoxColumn31;

	private DataGridViewTextBoxColumn dataGridViewTextBoxColumn32;

	private DataGridViewTextBoxColumn dataGridViewTextBoxColumn33;

	private DataGridViewTextBoxColumn dataGridViewTextBoxColumn34;

	private ToolStripMenuItem 测试2ToolStripMenuItem;

	private ToolStripMenuItem 关于ToolStripMenuItem;

	public TabControl tabChannel;

	private ToolStripMenuItem 参数设置ToolStripMenuItem;

	private ToolStripButton toolStripButton3;

	private ToolStripButton toolStripButton8;

	private ToolStripButton toolStripButton9;

	private ToolStripMenuItem tsmiFileMain;

	private ToolStripMenuItem 退出ToolStripMenuItem;

	private ToolStripMenuItem 选项ToolStripMenuItem;

	private ToolStripMenuItem 系统配置ToolStripMenuItem;

	private ToolStripMenuItem tsmiAbout;

	private System.Windows.Forms.Timer timer_1;

	private ToolTip toolTip_0;

	private ToolStripMenuItem tsmiUpgread;

	private ToolStripMenuItem tsmiHelp;

	private ToolStripSeparator toolStripSeparator5;

	private ToolStripMenuItem ToolStripMenuItemClock;

	private ToolStripMenuItem ToolStripMenuItemTempCtrl;

	private ToolStripMenuItem 气路配置ToolStripMenuItem;

	private ContextMenuStrip cmsIntegration;

	private ToolStripMenuItem miIntegAppendRow;

	private ToolStripMenuItem miIntegDeleteRows;

	private ToolStripMenuItem miIntegInsertRow;

	private ToolStripSeparator toolStripSeparator6;

	private ToolStripMenuItem miIntegResetRows;

	private ToolStripMenuItem miIntegRowsDown;

	private ToolStripSeparator toolStripSeparator7;

	private ToolStripMenuItem miIntegRowsUp;

	private ToolStripMenuItem ToolStripMenuItemTime;

	public ImageList imageList1;

	private ToolStripMenuItem toolStripMenuItem2;

	private ToolStripSeparator toolStripSeparator8;

	private ContextMenuStrip cmPeakInfo;

	private ToolStripMenuItem 峰尺寸ToolStripMenuItem;

	private ToolStripSeparator toolStripSeparator9;

	private ToolStripStatusLabel ToolLabelPeakInfo;

	private ToolStripMenuItem toolStripMenuItem3;

	private ToolStripMenuItem ToolStripMenuItemGmp;

	private IContainer components;

	public ToolStripMenuItem 谱图处理ToolStripMenuItem;

	public ToolStripMenuItem toolStripMenuItem1;

	public ToolStripMenuItem tsmiGraphGraft;

	public ToolStripMenuItem tsmiCheckMain;

	public ToolStripMenuItem tsmiHelpMain;

	public ToolStripMenuItem tsmiSystem;

	public ToolStripStatusLabel tsslMsg;

	public StatusStrip statusStrip1;

	public MstSet MainmstSet;

	private InsDeviceCtrl insDeviceCtrl1;

	private ChromDeviceCtrl chromDeviceCtrl1;

	private ChromAcqCtrl chromAcqCtrl1;

	private SplitContainer spcLeftBottom;

	private ToolStripStatusLabel toolStripStatusLabel2;

	private ToolStripStatusLabel tsslMemory;

	private ToolStripStatusLabel tsslCpu;

	private ToolStripMenuItem tmiFactorSetting;

	private ToolStripMenuItem barSubItemLanguage;

	private ToolStripMenuItem barCheckChinese;

	private ToolStripMenuItem barCheckEnglish;

	private AsyncTcpServer mainTcpServer => cdlMgr.MainTcpServer;

	private AsyncTcpServer modus0TcpServer => cdlMgr.Modus0TcpServer;

	private AsyncTcpServer modus1TcpServer => cdlMgr.Modus1TcpServer;

	public InsDeviceCtrl insDeviceCtrl => insDeviceCtrl1;

	public ChromDeviceCtrl chrDeviceCtrl => chromDeviceCtrl1;

	public ChromAcqCtrl chrAcqCtrl => chromAcqCtrl1;

	public FormVOC formVoc => null;

	public VocCtrl vocCtrl => vocCtrl2;

	public bool IsLoaded => !m_bLoading;

	public bool BAlarm
	{
		get
		{
			return bAlarm;
		}
		set
		{
			bAlarm = value;
		}
	}

	public string[] StrAlarmArray
	{
		get
		{
			return strAlarmArray;
		}
		set
		{
			strAlarmArray = value;
		}
	}

	public string StrAlarmFile
	{
		get
		{
			return strAlarmFile;
		}
		set
		{
			strAlarmFile = value;
		}
	}

	public bool IsDisposed2 => base.IsDisposed || m_bIsClosing;

	public float DisRt => chrAcqCtrl.disLg.lgXBeg + chrAcqCtrl.disLg.lgX;

	string FrmChromatManagerInterface.CurrentGCID => CurrentGCID;

	FrmMsetup ChromFormInterface.fset => fset;

	ModbusSlave ChromFormInterface.mComModbus => cdlMgr.tcpServerMgr.mComModbus;

	ModbusSlave ChromFormInterface.mComModbus2 => cdlMgr.tcpServerMgr.mComModbus2;

	FrmChromatManager ChromFormInterface.FrmChromat => chrDeviceCtrl.FrmEquip;

	VocCtrl ChromFormInterface.vocctrl => vocCtrl2;

	string ChromFormInterface.CurrentGCID => CurrentGCID;

	Button ChromFormInterface.button37 => chromAcqCtrl1.button37;

	Button ChromFormInterface.button38 => chromAcqCtrl1.button38;

	int ChromFormInterface.StateYiqi
	{
		get
		{
			return StateYiqi;
		}
		set
		{
			StateYiqi = value;
		}
	}

	Label ChromFormInterface.labFireState => chromAcqCtrl1.labFireState;

	TextBox ChromFormInterface.tbMachineState => insDeviceCtrl.tbMachineState;

	long ChromFormInterface.CountAnalyse => CountAnalyse;

	CheckBox ChromFormInterface.cbEnNMHC => null;

	float ChromFormInterface.shuaijian => shuaijian;

	float ChromFormInterface.shuaijian2 => shuaijian2;

	float ChromFormInterface.shuaijian3 => shuaijian3;

	int ChromFormInterface.IsAutoCalibra
	{
		get
		{
			return IsAutoCalibra;
		}
		set
		{
			IsAutoCalibra = value;
		}
	}

	uint ChromFormInterface.cntAnalyze1
	{
		get
		{
			return cntAnalyze1;
		}
		set
		{
			cntAnalyze1 = value;
		}
	}

	uint ChromFormInterface.cntAnalyze2
	{
		get
		{
			return cntAnalyze2;
		}
		set
		{
			cntAnalyze2 = value;
		}
	}

	bool ChromFormInterface.bAutoCycle1
	{
		get
		{
			return bAutoCycle1;
		}
		set
		{
			bAutoCycle1 = value;
		}
	}

	bool ChromFormInterface.bAutoCycle2
	{
		get
		{
			return bAutoCycle2;
		}
		set
		{
			bAutoCycle2 = value;
		}
	}

	int ChromFormInterface.AutoCalibraPoint
	{
		get
		{
			return AutoCalibraPoint;
		}
		set
		{
			AutoCalibraPoint = value;
		}
	}

	bool ChromFormInterface.bAlarmMy
	{
		get
		{
			return bAlarmMy;
		}
		set
		{
			bAlarmMy = value;
		}
	}

	ChromFormCtrl ChromFormInterface.chromFormCtrl
	{
		get
		{
			return chromFormCtrl2;
		}
		set
		{
			chromFormCtrl2 = value;
		}
	}

	TabControl ChromFormInterface.tabControl => tabCtrl2;

	public int CurrentChannelIndex
	{
		get
		{
			int num = 0;
			Invoke((MethodInvoker)delegate
			{
				num = tabChannel.SelectedIndex;
				if (num < 0)
				{
					num = 0;
				}
				else if (tabChannel.SelectedTab.Text.Trim() == "AUX")
				{
					num = 3;
				}
			});
			return num;
		}
	}

	ToolStripMenuItem ChromFormInterface.tsmiFileMain => tsmiFileMain;

	FrmTempNameSet ChromFormInterface.FTNS => FTNS;

	MbSerialPort ChromFormInterface.ModbusComClient => cdlMgr.tcpServerMgr.ModbusComClient;

	MstSet ChromFormInterface.MainmstSet => MainmstSet;

	FrmChromatManager ChromFormInterface.FrmEquip => chrDeviceCtrl.FrmEquip;

	Frmmultivalve ChromFormInterface.Fmultivalve => Fmultivalve;

	int ChromFormInterface.SetFireLengthValue { get; set; }

	int ChromFormInterface.FIDCount { get; set; }

	SampleDisplay ChromFormInterface.sampleDisplay => chrAcqCtrl.sampleDisplay;

	DisLg ChromFormInterface.disLg => chrAcqCtrl.disLg;

	TabControl ChromFormInterface.tabChannel => tabChannel;

	ToolStripStatusLabel ChromFormInterface.ToolLabelPeakInfo2 => ToolLabelPeakInfo;

	bool ChromFormInterface.StartBtEnable => chrAcqCtrl.tsStart.Enabled;

	bool ChromFormInterface.StopBtEnable => chrAcqCtrl.tsstop.Enabled;

	int ChromFormInterface.iChannel
	{
		get
		{
			return iChannel;
		}
		set
		{
			iChannel = value;
		}
	}

	bool ChromFormInterface.bStart1
	{
		get
		{
			return bStart1;
		}
		set
		{
			bStart1 = value;
		}
	}

	bool ChromFormInterface.bStart2
	{
		get
		{
			return bStart2;
		}
		set
		{
			bStart2 = value;
		}
	}

	uint ChromFormInterface.cntChannel1
	{
		get
		{
			return cntChannel1;
		}
		set
		{
			cntChannel1 = value;
		}
	}

	uint ChromFormInterface.cntChannel2
	{
		get
		{
			return cntChannel2;
		}
		set
		{
			cntChannel2 = value;
		}
	}

	[DllImport("qtdll.dll", CallingConvention = CallingConvention.Cdecl)]
	public static extern int add(int i);

	[DllImport("qtdll.dll", CallingConvention = CallingConvention.Cdecl)]
	public static extern void call(int i);

	[DllImport("qtdll.dll", CallingConvention = CallingConvention.Cdecl)]
	public static extern void SetFunCallBack([MarshalAs(UnmanagedType.FunctionPtr)] CallbackFun pCallbackFun);

	[DllImport("kernel32.dll", SetLastError = true)]
	public static extern bool Wow64DisableWow64FsRedirection(ref IntPtr ptr);

	[DllImport("kernel32.dll", SetLastError = true)]
	public static extern bool Wow64RevertWow64FsRedirection(IntPtr ptr);

	[DllImport("user32.dll", CharSet = CharSet.Auto)]
	public static extern IntPtr SendMessage(IntPtr hWnd, uint Msg, IntPtr wParam, IntPtr lParam);

	protected override void DefWndProc(ref Message message_0)
	{
		int msg = message_0.Msg;
		if (msg == 74)
		{
			Type type = default(COPYDATASTRUCT).GetType();
			COPYDATASTRUCT cOPYDATASTRUCT = (COPYDATASTRUCT)message_0.GetLParam(type);
			ChromForm chromForm = new ChromForm();
			chromForm.InitFm();
			chromForm.Show();
			chromForm.Opensda(cOPYDATASTRUCT.lpData);
			chromForm.BringToFront();
		}
		else
		{
			base.DefWndProc(ref message_0);
		}
	}

	public FormMainCtrl()
	{
		LogMgr.Instance.Write2RunLog("Program Loaded Begin FormMain");
		string_0 = "";
		fromMain = this;
		cdlMgr.formMain = this;
		cdlMgr.formMainEx = this;
		InitializeComponent();
		LogMgr.Instance.Write2RunLog("Program Loaded Finish InitializeComponent");
		InitCheckedLangButton();
		CtrlLangPS.Instance.UpdateLanguageForAllControl(this);
	}

	private TabPage NewTablePage(string strTableName, string strTableCaption)
	{
		TabPage tabPage = new TabPage();
		tabPage.BackColor = Color.FromArgb(255, 192, 192);
		tabPage.Location = new Point(4, 22);
		tabPage.Name = strTableName;
		tabPage.Padding = new Padding(3);
		tabPage.Size = new Size(696, 0);
		tabPage.TabIndex = 0;
		tabPage.Text = strTableCaption;
		tabPage.UseVisualStyleBackColor = true;
		return tabPage;
	}

	private void FormMain_Load(object sender, EventArgs e)
	{
		if (!DogFeturlMgr.LicencedGMP())
		{
			menuStrip1.Items.Remove(tsmiCheckMain);
			tsmiCheckMain.DropDownItems.Remove(ToolStripMenuItemGmp);
		}
		Class49.CreateDbBase();
		Check_reg_sdaFileType();
		MainmstSet.thisFormMf = this;
		fset = new FrmMsetup();
		fset.Init(this);
		FTNS = new FrmTempNameSet();
		FTNS.Init(this);
		FGNS = new FrmGasNameSet();
		FGNS.Init(this);
		Fmultivalve = new Frmmultivalve();
		Fmultivalve.Init(this);
		Application.DoEvents();
		string path = Application.StartupPath + "\\test.sda";
		Chromatogram chromatogram = null;
		chromatogram = ((!File.Exists(path)) ? new Chromatogram() : Chromatogram.LoadFromFile2(Application.StartupPath + "\\test.sda", DetectorStyle.General));
		if (chromatogram != null)
		{
			chromatogram_0 = chromatogram;
		}
		cdlMgr.tcpServerMgr.modBusData_0.InitBytes(chromatogram_0);
		base.KeyPreview = true;
		Application.DoEvents();
		LoadLanguage();
		Application.DoEvents();
		StartTcpServer();
		if (sysParam.bComEnable)
		{
			cdlMgr.tcpServerMgr.ModbusComClient = new MbSerialPort();
			cdlMgr.tcpServerMgr.ModbusComClient.Open("COM1");
		}
		MisMgrAssist misMgrAssist = MisMgrAssist.Create();
		misMgrAssist.SetForm();
		m_bLoading = false;
		shuaijian = frmParam.fShuaijian;
		shuaijian2 = frmParam.fShuaijian2;
		shuaijian3 = frmParam.fShuaijian3;
		chromAcqCtrl1.checkBox5.Checked = false;
		chromAcqCtrl1.checkBox5.Visible = false;
		Class49.InsertIntoTable(Class49.string_9[0], Class49.user_0.u_name, "", Lang.PS("启动系统成功", "Start succeed"), Lang.PS("启动系统完毕", "Start succeed"));
		UIProxy.Instance.SetErrorMsgStaticLabelMenu();
	}

	private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
	{
		if (mainTcpServer == null)
		{
			e.Cancel = true;
		}
		else if (selfRestart)
		{
			TcpServerSocket currentTcpServerSocket = cdlMgr.CurrentTcpServerSocket;
			if (currentTcpServerSocket != null)
			{
				LogMgr.Instance.LogError("oneInstrumID：" + currentTcpServerSocket.ID);
			}
			else
			{
				LogMgr.Instance.LogError("oneInstrum is null");
			}
			LogMgr.Instance.LogError("当前ID：" + cdlMgr.CurrentGCID);
			LogMgr.Instance.LogError("仪器选中状态：" + cdlMgr.formMain.chrDeviceCtrl.InstrumlistView.Items[0].ImageIndex);
			try
			{
				cdlMgr.StopTcpServerMgr();
				Thread.Sleep(50);
				cdlMgr.SaveWorkSunFile();
				Class49.InsertIntoTable(Class49.string_9[0], Class49.user_0.u_name, "", "结束系统", "结束系统");
			}
			catch (Exception ex)
			{
				LogMgr.Instance.LogError(ex.Message);
			}
			LogMgr.Instance.LogError("仪器自动重启");
			Thread.Sleep(200);
			Application.Restart();
			Process.GetCurrentProcess().Kill();
		}
		else if (!m_bIsClosing)
		{
			if (!mainTcpServer.CanClose())
			{
				if (MessageBox.Show(Lang.PS("当前存在有正在采集的通道，是否确认关闭？", "At present there is acquisition channel, to confirm the close ?"), Lang.PS("提示", "Tips"), MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation) != DialogResult.OK)
				{
					e.Cancel = true;
					m_bIsClosing = false;
				}
				else
				{
					m_bIsClosing = true;
				}
			}
			else if (DialogResult.OK != MessageBox.Show(Lang.PS("请确认是否要退出系统！", "Are you sure you want to exit the system?"), Lang.PS("提示", "Tips"), MessageBoxButtons.OKCancel, MessageBoxIcon.Question))
			{
				e.Cancel = true;
				m_bIsClosing = false;
			}
			else
			{
				m_bIsClosing = true;
			}
		}
		else if (!e.Cancel)
		{
			try
			{
				cdlMgr.StopTcpServerMgr();
				Thread.Sleep(50);
				cdlMgr.SaveWorkSunFile();
				Class49.InsertIntoTable(Class49.string_9[0], Class49.user_0.u_name, "", "结束系统", "结束系统");
			}
			catch (Exception ex2)
			{
				LogMgr.Instance.LogError(ex2.Message);
			}
		}
	}

	public void openSoftKey()
	{
	}

	private void timer_1_Tick(object sender, EventArgs e)
	{
		if (mainTcpServer != null)
		{
			mainTcpServer.CheckConnectList();
		}
		if (string_0 != "")
		{
			ChromForm chromForm = new ChromForm();
			chromForm.InitFm();
			chromForm.Show();
			chromForm.Opensda(string_0);
			chromForm.BringToFront();
			string_0 = "";
		}
		tsslMemory.Text = Lang.PS("内存:") + PerfromMgr.MemorySizePrivate + "MB  ";
		tsslCpu.Text = Lang.PS("CPU占用:") + PerfromMgr.CpuPerformance.ToString("0%");
	}

	private void tabCtrl_SelectedIndexChanged(object sender, EventArgs e)
	{
		if (tabCtrl2.SelectedTab.Text.Trim() == Lang.PS("实时谱图"))
		{
			cdlMgr.formMain.SetShowFullScreen(bFull: false);
			if (frmParam.iOnlineMode == 2)
			{
				splitContainer1.Panel2Collapsed = true;
			}
		}
		else
		{
			cdlMgr.formMain.SetShowFullScreen(bFull: true);
		}
	}

	private void method_5(byte[] byte_0, TcpServerEventArgs tcpServerEventArgs_0, int int_3)
	{
		int mID = byte_0[6];
		byte b = byte_0[7];
		TcpServerSocket oneInstrumByMID = mainTcpServer.GetOneInstrumByMID(mID);
		byte[] array = new byte[byte_0.Length];
		Array.Copy(byte_0, 0, array, 0, array.Length);
		int num = array[8] * 256 + array[9];
		int num2 = array[10] * 255 + array[11];
		switch (b)
		{
		case 1:
		{
			if (oneInstrumByMID == null)
			{
				tcpServerEventArgs_0.ServerSocket.SendData(cdlMgr.tcpServerMgr.modBusData_0.ModBusEquipError05(array, 1));
				break;
			}
			if (num2 > 1)
			{
				tcpServerEventArgs_0.ServerSocket.SendData(cdlMgr.tcpServerMgr.modBusData_0.ModBusEquipError05(array, 3));
				break;
			}
			Array.Resize(ref array, 10);
			array[8] = 1;
			byte b2 = 0;
			switch (num)
			{
			case 10000:
				if (oneInstrumByMID.ControlTemp)
				{
					b2 = 1;
				}
				break;
			case 10001:
				if (oneInstrumByMID.AlalyseStatus)
				{
					b2 = 1;
				}
				break;
			case 10002:
				if (oneInstrumByMID.sglsSampling[0].simple)
				{
					b2 = 1;
				}
				break;
			case 10003:
				if (oneInstrumByMID.sglsSampling[1].simple)
				{
					b2 = 1;
				}
				break;
			case 10004:
				if (oneInstrumByMID.sglsSampling[2].simple)
				{
					b2 = 1;
				}
				break;
			default:
				tcpServerEventArgs_0.ServerSocket.SendData(cdlMgr.tcpServerMgr.modBusData_0.ModBusEquipError05(array, 4));
				return;
			}
			array[9] = b2;
			array[5] = (byte)(array.Length - 6);
			tcpServerEventArgs_0.ServerSocket.SendData(array);
			break;
		}
		case 2:
		case 4:
			break;
		case 3:
		{
			if (oneInstrumByMID == null)
			{
				tcpServerEventArgs_0.ServerSocket.SendData(cdlMgr.tcpServerMgr.modBusData_0.ModBusEquipError04(byte_0));
				break;
			}
			bool flag = false;
			bool flag2 = true;
			chromatogram_0 = oneInstrumByMID.bgChrom;
			cdlMgr.tcpServerMgr.modBusData_0.PrintFilePath = oneInstrumByMID.Printpath;
			cdlMgr.tcpServerMgr.modBusData_0.InitBytesVer1(chromatogram_0);
			if (num >= 0 && num < 10000)
			{
				chromatogram_0 = oneInstrumByMID.ModBusbgChrom[0];
				cdlMgr.tcpServerMgr.modBusData_0.PrintFilePath = oneInstrumByMID.Printpath;
				cdlMgr.tcpServerMgr.modBusData_0.InitBytesVer1(chromatogram_0);
			}
			if (num >= 10000 && num < 20000)
			{
				chromatogram_0 = oneInstrumByMID.ModBusbgChrom[1];
				cdlMgr.tcpServerMgr.modBusData_0.PrintFilePath = oneInstrumByMID.Printpath;
				cdlMgr.tcpServerMgr.modBusData_0.InitBytesVer1(chromatogram_0);
			}
			if (num >= 20000 && num < 30000)
			{
				chromatogram_0 = oneInstrumByMID.ModBusbgChrom[2];
				cdlMgr.tcpServerMgr.modBusData_0.PrintFilePath = oneInstrumByMID.Printpath;
				cdlMgr.tcpServerMgr.modBusData_0.InitBytesVer1(chromatogram_0);
			}
			if (num >= 30000 && num < 40000)
			{
				chromatogram_0 = oneInstrumByMID.ModBusbgChrom[3];
				cdlMgr.tcpServerMgr.modBusData_0.PrintFilePath = oneInstrumByMID.Printpath;
				cdlMgr.tcpServerMgr.modBusData_0.InitBytesVer1(chromatogram_0);
			}
			tcpServerEventArgs_0.ServerSocket.SendData(cdlMgr.tcpServerMgr.modBusData_0.ModBusValueVer1(byte_0, oneInstrumByMID.ModBusbgChrom));
			break;
		}
		case 5:
			if (oneInstrumByMID != null)
			{
				switch (num)
				{
				case 10000:
					if (array[10] == byte.MaxValue)
					{
						oneInstrumByMID.SendCmd(16);
					}
					else
					{
						oneInstrumByMID.SendCmd(17);
					}
					break;
				case 10001:
					if (array[10] == byte.MaxValue)
					{
						oneInstrumByMID.SendCmd(18);
					}
					else
					{
						oneInstrumByMID.SendCmd(19);
					}
					break;
				case 10002:
					if (array[10] == byte.MaxValue)
					{
						oneInstrumByMID.SendCmd(22, 0);
					}
					else
					{
						oneInstrumByMID.SendCmd(23, 0);
					}
					break;
				case 10003:
					if (array[10] == byte.MaxValue)
					{
						oneInstrumByMID.SendCmd(22, 1);
					}
					else
					{
						oneInstrumByMID.SendCmd(23, 1);
					}
					break;
				case 10004:
					if (array[10] == byte.MaxValue)
					{
						oneInstrumByMID.SendCmd(22, 2);
					}
					else
					{
						oneInstrumByMID.SendCmd(23, 2);
					}
					break;
				default:
					tcpServerEventArgs_0.ServerSocket.SendData(cdlMgr.tcpServerMgr.modBusData_0.ModBusEquipError85(tcpServerEventArgs_0.ServerSocket.Buffer, 4));
					return;
				}
				tcpServerEventArgs_0.ServerSocket.SendData(array);
			}
			else
			{
				tcpServerEventArgs_0.ServerSocket.SendData(cdlMgr.tcpServerMgr.modBusData_0.ModBusEquipError85(tcpServerEventArgs_0.ServerSocket.Buffer, 1));
			}
			break;
		}
	}

	public void Check_reg_sdaFileType()
	{
		FileTypeRegInfo fileTypeRegInfo = new FileTypeRegInfo(".sda");
		fileTypeRegInfo.Description = Lang.PS("网络色谱工作站谱图文件");
		fileTypeRegInfo.ExePath = Application.StartupPath + "\\IBrainChrom.exe";
		fileTypeRegInfo.ExtendName = ".sda";
		fileTypeRegInfo.IcoPath = Application.StartupPath + "\\cc.ico";
		FileTypeRegister fileTypeRegister = new FileTypeRegister();
		if (!fileTypeRegister.FileTypeRegistered(".sda"))
		{
			try
			{
				fileTypeRegister.RegisterFileType(fileTypeRegInfo);
			}
			catch
			{
			}
		}
	}

	public void LoadLanguage()
	{
		Text = Lang.PS("IBrainChrom", "IBrainChrom") + "  V" + AssemblyInfoCfg.SoftVersion();
		if (DogFeturlMgr.LicencedGMP())
		{
			Text = Lang.PS("IBrainChromGMP", "IBrainChromGMP") + "  V" + AssemblyInfoCfg.SoftVersion();
		}
		tabChannel.TabPages[0].Text = Lang.PS("通道A", "Channel A");
		tabChannel.TabPages[1].Text = Lang.PS("通道B", "Channel B");
		tabChannel.TabPages[2].Text = Lang.PS("通道C", "Channel C");
		tabChannel.TabPages[3].Text = Lang.PS("通道D", "Channel D");
		toolStripStatusLabel1.Text = Lang.PS("欢迎使用色谱仪工作站控制软件", "Welcome to the chromatograph workstation control software");
	}

	public void SetShowMainmstSet(bool bShow)
	{
		insDeviceCtrl.method_14();
		if (bShow)
		{
			insDeviceCtrl.Visible = true;
			insDeviceCtrl.Dock = DockStyle.Fill;
			MainmstSet.Visible = false;
		}
		else
		{
			insDeviceCtrl.Visible = false;
			MainmstSet.Dock = DockStyle.Fill;
			MainmstSet.Visible = true;
		}
	}

	public void SetShowFullScreen(bool bFull)
	{
		splitContainer1.Panel2Collapsed = bFull;
		splitContainer2.Panel2Collapsed = bFull;
	}

	public void SetShowWindow(bool bShow)
	{
		if (bShow)
		{
			base.Visible = true;
		}
		else
		{
			base.Visible = false;
		}
	}

	public void toolStripButton1_Click(object sender, EventArgs e)
	{
		if (Class49.user_0.ULevel != User.Level.访问员)
		{
			cdlMgr.currentTcpServerMgrSendCmd(22);
			return;
		}
		MessageBox.Show(Lang.PS("没有启动分析权限！", "No boot analysis authority "));
	}

	private void toolStripButton2_Click(object sender, EventArgs e)
	{
	}

	public void toolStripButton3_Click(object sender, EventArgs e)
	{
		cdlMgr.currentTcpServerMgrSendCmd(23);
	}

	private void toolStripButton7_Click(object sender, EventArgs e)
	{
		if (chrDeviceCtrl.FrmEquip.SunAquips == null)
		{
			return;
		}
		for (int i = 0; i < chrDeviceCtrl.FrmEquip.SunAquips.Count; i++)
		{
			if (chrDeviceCtrl.FrmEquip.SunAquips[i].info.ID == CurrentGCID)
			{
				FrmDisposePara frmDisposePara = new FrmDisposePara();
				frmDisposePara.Text = Lang.PS("基线扣除及文件命名设置", "Baseline deduction and file naming settings");
				frmDisposePara.Init(this, 0);
				frmDisposePara.Show();
				break;
			}
		}
	}

	public void UpdateSetCurrentTemplate(Chromatogram chromatogram)
	{
		TcpServerSocket currentTcpServerSocket = cdlMgr.CurrentTcpServerSocket;
		if (currentTcpServerSocket != null)
		{
			int num = tabChannel.SelectedIndex;
			if (tabChannel.SelectedTab.Text.Trim() == "AUX")
			{
				num = 3;
			}
			currentTcpServerSocket.sglsSampling[num].RunningInteg = chromatogram.integ;
		}
	}

	public void ChangeDisLg()
	{
		try
		{
			float val = chrAcqCtrl.disLg.lgX / 100f;
			val = Math.Max(val, 0.001f);
			if (sysParam.iChrgOptionShowMethod == 0)
			{
				chrAcqCtrl.method_10(chrAcqCtrl.disLg.lgXBeg + val, chrAcqCtrl.disLg.lgX, chrAcqCtrl.disLg.lgYBeg, chrAcqCtrl.disLg.lgY);
			}
			else
			{
				chrAcqCtrl.method_10(chrAcqCtrl.disLg.lgXBeg, chrAcqCtrl.disLg.lgX + val, chrAcqCtrl.disLg.lgYBeg, chrAcqCtrl.disLg.lgY);
			}
		}
		catch (Exception)
		{
		}
	}

	public void SetBgChrom(bool visible)
	{
		chrAcqCtrl.sampleDisplay.ShowBgChrom = visible;
	}

	public void SetDrawName(string drawName)
	{
		chrAcqCtrl.sampleDisplay.drawName = drawName;
	}

	public void ReloadMisMgr()
	{
		ChannelChartPara currentChannelChartPara = cdlMgr.CurrentChannelChartPara;
		insDeviceCtrl.method_13();
		insDeviceCtrl.ClearEpcCtrl();
		chrAcqCtrl.LoadChannelChartPara(currentChannelChartPara);
		if (minePlot.minePlotSelf != null)
		{
			minePlot.minePlotSelf.LoadChannelChartPara(currentChannelChartPara);
		}
		MainmstSet.ChangeMst();
		insDeviceCtrl.ReadInsDeviceMgr(cdlMgr.CurrentInsDeviceMgr);
	}

	public void ReloadMisMgr2()
	{
		ChannelChartPara currentChannelChartPara = cdlMgr.CurrentChannelChartPara;
		insDeviceCtrl.method_13();
		insDeviceCtrl.ClearEpcCtrl();
		chrAcqCtrl.LoadChannelChartPara(currentChannelChartPara);
		if (minePlot.minePlotSelf != null)
		{
			minePlot.minePlotSelf.LoadChannelChartPara(currentChannelChartPara);
		}
		insDeviceCtrl.ReadInsDeviceMgr(cdlMgr.CurrentInsDeviceMgr);
	}

	public void UpdateMisMgr()
	{
		MainmstSet.WriteToMtdMgr();
		insDeviceCtrl.WriteInsDeviceMgr(cdlMgr.CurrentInsDeviceMgr);
	}

	public void SetCurrentChromDevice()
	{
		if (cdlMgr.Count == 0)
		{
			return;
		}
		TcpServerSocket currentTcpServerSocket = cdlMgr.CurrentTcpServerSocket;
		if (currentTcpServerSocket == null)
		{
			chrAcqCtrl.sampleDisplay.ClearDisSignals();
		}
		else
		{
			if (currentTcpServerSocket.dtc_Channels.Length == 0)
			{
				return;
			}
			m_bLoading = true;
			CurrentGCID = cdlMgr.CurrentGCID;
			cdlMgr.CurrentChannelIdx = 0;
			SetDrawName(cdlMgr.CurrentChromDevice.info.Name);
			insDeviceCtrl.WriteSmsSetiingInfo(cdlMgr.CurrentInsDeviceMgr);
			tabChannel.TabPages.Clear();
			for (int i = 0; i < currentTcpServerSocket.dtc_Channels.Length; i++)
			{
				if (currentTcpServerSocket.dtc_Channels[i].mark != 0)
				{
					tabChannel.TabPages.Add(new TabPage());
					tabChannel.TabPages[tabChannel.TabPages.Count - 1].Text = "    " + currentTcpServerSocket.dtc_Channels[i].name + "    ";
					tabChannel.TabPages[tabChannel.TabPages.Count - 1].Tag = currentTcpServerSocket.dtc_Channels[i].mark;
				}
			}
			chrAcqCtrl.sampleDisplay.ClearDisSignals();
			chrAcqCtrl.sampleDisplay.LinkDisSignals(currentTcpServerSocket.sglsSampling, 0, out var _);
			chrAcqCtrl.sampleDisplay.bgChrom.signal = chrAcqCtrl.sampleDisplay.disSignals[0];
			currentTcpServerSocket.Linkepctypes();
			ReloadMisMgr();
			m_bLoading = false;
			Thread thread = new Thread(RunSendCmdList);
			thread.Name = "RunSendCmdListThread";
			thread.Start();
		}
	}

	public void RunSendCmdList()
	{
		TcpServerSocket currentTcpServerSocket = cdlMgr.CurrentTcpServerSocket;
		if (currentTcpServerSocket != null)
		{
			currentTcpServerSocket.SendCmd(13);
			Thread.Sleep(50);
			Application.DoEvents();
			currentTcpServerSocket.SendCmd(0);
			Thread.Sleep(50);
			Application.DoEvents();
			currentTcpServerSocket.SendCmd(36);
			Thread.Sleep(50);
			Application.DoEvents();
			currentTcpServerSocket.SendCmd(1);
			currentTcpServerSocket.SendCmd(2);
			currentTcpServerSocket.SendCmd(100);
			Thread.Sleep(50);
			Application.DoEvents();
			currentTcpServerSocket.SendCmd(1);
			currentTcpServerSocket.SendCmd(2);
			currentTcpServerSocket.SendCmd(100);
			Thread.Sleep(50);
			Thread.Sleep(50);
			Application.DoEvents();
			currentTcpServerSocket.SendCmd(98);
			Thread.Sleep(50);
			Application.DoEvents();
			currentTcpServerSocket.SendCmd(4);
			Thread.Sleep(50);
			Application.DoEvents();
			currentTcpServerSocket.SendCmd(48);
			Thread.Sleep(50);
			Application.DoEvents();
			currentTcpServerSocket.SendCmd(5);
			Thread.Sleep(50);
			Application.DoEvents();
			currentTcpServerSocket.SendCmd(98);
			Thread.Sleep(50);
			Application.DoEvents();
			currentTcpServerSocket.SendCmd(64);
			Thread.Sleep(50);
			Application.DoEvents();
			currentTcpServerSocket.SendCmd(66);
			Thread.Sleep(50);
			Application.DoEvents();
			currentTcpServerSocket.SendCmd(83);
			Thread.Sleep(50);
			Application.DoEvents();
			for (int i = 48; i <= 62; i++)
			{
				currentTcpServerSocket.SendEPCCmd(36, (byte)i);
				Thread.Sleep(100);
				Application.DoEvents();
			}
			for (int j = 48; j <= 62; j++)
			{
				currentTcpServerSocket.SendEPCCmd(36, (byte)j);
				Thread.Sleep(100);
				Application.DoEvents();
			}
			Thread.Sleep(50);
			Application.DoEvents();
			currentTcpServerSocket.SendCmd(0);
			Thread.Sleep(50);
			Application.DoEvents();
			currentTcpServerSocket.SendCmd(250);
		}
	}

	private void toolStripButton4_Click(object sender, EventArgs e)
	{
		chrAcqCtrl.sampleDisplay.stDisChain.DynNo--;
		chrAcqCtrl.disLg = chrAcqCtrl.sampleDisplay.stDisChain.CurDisLg;
	}

	private void toolStripButton5_Click(object sender, EventArgs e)
	{
		chrAcqCtrl.sampleDisplay.stDisChain.DynNo++;
		chrAcqCtrl.disLg = chrAcqCtrl.sampleDisplay.stDisChain.CurDisLg;
	}

	public void button16_Click(object sender, EventArgs e)
	{
		cdlMgr.currentTcpServerMgrSendCmd(98);
	}

	public void SelectChannelIdx()
	{
		tabChannel_SelectedIndexChanged(null, null);
	}

	public void tabChannel_SelectedIndexChanged(object sender, EventArgs e)
	{
		if (m_bLoading || !chrAcqCtrl.m_bAllowUpdateChromGraphic)
		{
			return;
		}
		int currentChannelIndex = CurrentChannelIndex;
		cdlMgr.CurrentChannelIdx = currentChannelIndex;
		bool flag = chrAcqCtrl.cbDisPlayAll.Checked;
		if (currentChannelIndex > 0)
		{
			chrAcqCtrl.m_bAllowDisplyAll = false;
			chrAcqCtrl.cbDisPlayAll.Visible = false;
			chrAcqCtrl.m_bAllowDisplyAll = true;
			flag = false;
		}
		else
		{
			chrAcqCtrl.cbDisPlayAll.Visible = true;
			flag = true;
		}
		if (flag && currentChannelIndex == 0)
		{
			chrAcqCtrl.sampleDisplay.ClearDisSignals();
		}
		else if (!flag)
		{
			chrAcqCtrl.sampleDisplay.ClearDisSignals();
		}
		TcpServerSocket currentTcpServerSocket = cdlMgr.CurrentTcpServerSocket;
		if (currentTcpServerSocket != null)
		{
			if (flag && currentChannelIndex == 0)
			{
				chrAcqCtrl.sampleDisplay.LinkDisSignals(currentTcpServerSocket.sglsSampling, currentChannelIndex, out var _);
				chrAcqCtrl.sampleDisplay.bgChrom.signal = chrAcqCtrl.sampleDisplay.disSignals[currentChannelIndex];
			}
			else if (!flag)
			{
				chrAcqCtrl.sampleDisplay.LinkDisSignals(currentTcpServerSocket.sglsSampling, currentChannelIndex, out var _);
				chrAcqCtrl.sampleDisplay.bgChrom.signal = chrAcqCtrl.sampleDisplay.disSignals[currentChannelIndex];
			}
			chrDeviceCtrl.SetSelectItemImageIdx();
			ChannelChartPara currentChannelChartPara = cdlMgr.CurrentChannelChartPara;
			chrAcqCtrl.LoadChannelChartPara(currentChannelChartPara);
			if (minePlot.minePlotSelf != null)
			{
				minePlot.minePlotSelf.LoadChannelChartPara(currentChannelChartPara);
			}
			currentTcpServerSocket.dtc_Channels[currentChannelIndex].chromInfoR.AcqRunTime = currentChannelChartPara.stopTime;
			currentTcpServerSocket.sglsSampling[currentChannelIndex].StopAutoAlalyse = currentChannelChartPara.analysisWhenStop;
			MainmstSet.ChangeMst();
		}
		chrAcqCtrl.m_bAllowUpdateChromGraphic = true;
	}

	private void method_17(object sender, EventArgs e)
	{
	}

	private void method_19(object sender, EventArgs e)
	{
		if (mainTcpServer != null && cdlMgr.CurrentTcpServerSocket != null)
		{
			int channelIndex = tabChannel.SelectedIndex;
			if (tabChannel.SelectedTab.Text.Trim() == "AUX")
			{
				channelIndex = 3;
			}
			ChannelChartPara oneEquipPara = chrDeviceCtrl.FrmEquip.GetOneEquipPara(CurrentGCID, channelIndex);
			chrDeviceCtrl.FrmEquip.UpdateOneEquipPara(CurrentGCID, channelIndex, oneEquipPara);
		}
	}

	private void method_20(object sender, EventArgs e)
	{
		if (mainTcpServer != null && cdlMgr.CurrentTcpServerSocket != null)
		{
			int channelIndex = tabChannel.SelectedIndex;
			if (tabChannel.SelectedTab.Text.Trim() == "AUX")
			{
				channelIndex = 3;
			}
			ChannelChartPara oneEquipPara = chrDeviceCtrl.FrmEquip.GetOneEquipPara(CurrentGCID, channelIndex);
			chrDeviceCtrl.FrmEquip.UpdateOneEquipPara(CurrentGCID, channelIndex, oneEquipPara);
		}
	}

	private void toolStripButton1_Click_1(object sender, EventArgs e)
	{
		TcpServerSocket currentTcpServerSocket = cdlMgr.CurrentTcpServerSocket;
		if (currentTcpServerSocket != null)
		{
			int selectedIndex = tabChannel.SelectedIndex;
			if (!(tabChannel.SelectedTab.Text.Trim() == "AUX"))
			{
				currentTcpServerSocket.ShowDtcrForm(selectedIndex, tabChannel.SelectedTab.Text.Trim());
				Thread.Sleep(100);
				currentTcpServerSocket.SendCmd(13);
			}
		}
	}

	public void FID1Fire()
	{
		TcpServerSocket currentTcpServerSocket = cdlMgr.CurrentTcpServerSocket;
		if (currentTcpServerSocket != null)
		{
			currentTcpServerSocket.SendCmd(20);
			Thread.Sleep(500);
			Application.DoEvents();
			currentTcpServerSocket.SendCmd(21);
		}
	}

	public void FID2Fire()
	{
		TcpServerSocket currentTcpServerSocket = cdlMgr.CurrentTcpServerSocket;
		if (currentTcpServerSocket != null)
		{
			currentTcpServerSocket.SendCmd(20);
			Thread.Sleep(500);
			Application.DoEvents();
			currentTcpServerSocket.SendCmd(21);
		}
	}

	public void SetZero()
	{
		TcpServerSocket currentTcpServerSocket = cdlMgr.CurrentTcpServerSocket;
		if (currentTcpServerSocket != null)
		{
			currentTcpServerSocket.SendCmd(241);
			Thread.Sleep(500);
			Application.DoEvents();
			currentTcpServerSocket.SendCmd(21);
		}
	}

	public void DtrSet()
	{
		TcpServerSocket currentTcpServerSocket = cdlMgr.CurrentTcpServerSocket;
		if (currentTcpServerSocket == null)
		{
			return;
		}
		bool flag = true;
		int selectedIndex = tabChannel.SelectedIndex;
		if (!(tabChannel.SelectedTab.Text.Trim() == "AUX"))
		{
			if (currentTcpServerSocket.sglsSampling[selectedIndex].simple)
			{
				flag = false;
			}
			if (flag)
			{
				currentTcpServerSocket.SendCmd(14);
				return;
			}
			MessageBox.Show(Lang.PS("采集状态下不能对检测器进行设置！", "Can't capture state of detector set！"));
		}
	}

	public void DtrSelectFireLength()
	{
		cdlMgr.currentTcpServerMgrSendCmd(53);
	}

	public void DtrSetFireLength()
	{
		cdlMgr.currentTcpServerMgrSendCmd(50);
	}

	public void MultiValveselect()
	{
		cdlMgr.currentTcpServerMgrSendCmd(110);
	}

	public void MultiValveSet()
	{
		cdlMgr.currentTcpServerMgrSendCmd(111);
	}

	public void DtrTempNameSelect()
	{
		cdlMgr.currentTcpServerMgrSendCmd(64);
	}

	public void DtrTempNameEnableSelect()
	{
		cdlMgr.currentTcpServerMgrSendCmd(66);
	}

	public void DtrTempNameSet()
	{
		cdlMgr.currentTcpServerMgrSendCmd(65);
	}

	public void DtrTempNameEnableSt()
	{
		cdlMgr.currentTcpServerMgrSendCmd(67);
	}

	public void UpdateFireLengthValue()
	{
	}

	public void toolStripMenuItem3_Click(object sender, EventArgs e)
	{
		TcpServerSocket currentTcpServerSocket = cdlMgr.CurrentTcpServerSocket;
		if (currentTcpServerSocket != null)
		{
			Fmultivalve.Show();
			Fmultivalve.button1_Click(null, null);
		}
	}

	private void toolStripMenuItem2_Click(object sender, EventArgs e)
	{
		cdlMgr.currentTcpServerMgrSendCmd(104);
	}

	private string smethod_0()
	{
		long num = 1L;
		byte[] array = Guid.NewGuid().ToByteArray();
		foreach (byte b in array)
		{
			num *= b + 1;
		}
		return $"{num - DateTime.Now.Ticks:x}";
	}

	private void FormMain_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode == Keys.Space)
		{
			chrAcqCtrl.tbTime_DoubleClick(null, null);
			return;
		}
		if (e.KeyCode == Keys.F2)
		{
			toolStripButton1_Click(null, null);
		}
		if (e.KeyCode == Keys.F3)
		{
			toolStripButton3_Click(null, null);
		}
		if (e.KeyCode == Keys.F4)
		{
			toolStripButton6_Click(null, null);
		}
		if (e.KeyCode == Keys.F5)
		{
			insDeviceCtrl.button14_Click(null, null);
		}
		if (e.KeyCode == Keys.Left)
		{
			toolStripButton4_Click(null, null);
		}
		if (e.KeyCode == Keys.Right)
		{
			toolStripButton5_Click(null, null);
		}
		if (e.KeyCode == Keys.Up)
		{
			toolStripButton4_Click(null, null);
		}
		if (e.KeyCode == Keys.Down)
		{
			toolStripButton5_Click(null, null);
		}
	}

	private void ToolStripMenuItemClock_Click(object sender, EventArgs e)
	{
		cdlMgr.currentTcpServerMgrSendCmd(11);
	}

	private void ToolStripMenuItemAbout_Click(object sender, EventArgs e)
	{
		NoQYAboutBox noQYAboutBox = new NoQYAboutBox();
		noQYAboutBox.ShowDialog();
	}

	private void ToolStripMenuItemUpgread_Click(object sender, EventArgs e)
	{
	}

	private void ToolStripMenuItemHelp_Click(object sender, EventArgs e)
	{
		Process.Start(Application.StartupPath.ToString() + "\\Help.pdf");
	}

	private void ToolStripMenuItemGmp_Click(object sender, EventArgs e)
	{
		if (frmOperLog_0 == null)
		{
			frmOperLog_0 = new FrmOperLog();
			frmOperLog_0.Show();
		}
		else
		{
			frmOperLog_0.Show();
		}
	}

	private void ToolStripMenuItemExit_Click(object sender, EventArgs e)
	{
		Close();
	}

	private void 控温配置ToolStripMenuItem_Click(object sender, EventArgs e)
	{
		FTNS.Show();
		FTNS.button1_Click(null, null);
	}

	private void 气路配置ToolStripMenuItem_Click(object sender, EventArgs e)
	{
		FGNS.Show();
	}

	private void 参数设置ToolStripMenuItem_Click(object sender, EventArgs e)
	{
		if (fset == null)
		{
			fset = new FrmMsetup();
			fset.Init(this);
			fset.Show();
		}
		else
		{
			fset.Show();
		}
	}

	private void 系统配置ToolStripMenuItem_Click(object sender, EventArgs e)
	{
		StationConfigDlg stationConfigDlg = new StationConfigDlg();
		stationConfigDlg.ShowDialog(this);
	}

	private void 谱图嫁接ToolStripMenuItem_Click(object sender, EventArgs e)
	{
		FrmSingialAdd frmSingialAdd = new FrmSingialAdd();
		frmSingialAdd.Show();
	}

	private void ToolStripMenuItemChromGraph_Click(object sender, EventArgs e)
	{
		ChromForm chromForm = new ChromForm();
		chromForm.InitFm();
		chromForm.Show();
	}

	private void toolStripMenuItem1_Click(object sender, EventArgs e)
	{
		CaliGnlForm caliGnlForm = new CaliGnlForm();
		caliGnlForm.Show();
	}

	private void ToolStripMenuItemTime_Click(object sender, EventArgs e)
	{
		if (chrDeviceCtrl.FrmEquip.SunAquips == null)
		{
			return;
		}
		int selectedIndex = tabChannel.SelectedIndex;
		if (tabChannel.SelectedTab.Text.Trim() == "AUX")
		{
			return;
		}
		for (int i = 0; i < chrDeviceCtrl.FrmEquip.SunAquips.Count; i++)
		{
			if (chrDeviceCtrl.FrmEquip.SunAquips[i].info.ID == CurrentGCID)
			{
				FrmDisposePara frmDisposePara = new FrmDisposePara();
				frmDisposePara.Init(this, 1);
				frmDisposePara.Text = Lang.PS("时间程序", "time Program");
				frmDisposePara.Show();
				break;
			}
		}
	}

	public void toolStripButton6_Click(object sender, EventArgs e)
	{
		TcpServerSocket currentTcpServerSocket = cdlMgr.CurrentTcpServerSocket;
		if (currentTcpServerSocket == null)
		{
			return;
		}
		int currentChannelIndex = CurrentChannelIndex;
		currentTcpServerSocket.sglsSampling[currentChannelIndex].ResetOriDots(createDiskFile: true);
		currentTcpServerSocket.bgChrom.signal.ClearPeak();
		currentTcpServerSocket.sglsSampling[currentChannelIndex].simple = false;
		chrAcqCtrl.tsStart.Enabled = true;
		chrAcqCtrl.tsstop.Enabled = false;
		for (int i = 0; i < currentTcpServerSocket.sglsSampling.Length; i++)
		{
			if (i >= tabChannel.TabPages.Count)
			{
				currentTcpServerSocket.sglsSampling[i].simple = false;
			}
		}
		if (!currentTcpServerSocket.sglsSampling[0].simple && !currentTcpServerSocket.sglsSampling[1].simple && !currentTcpServerSocket.sglsSampling[2].simple && !currentTcpServerSocket.sglsSampling[3].simple && CurrentGCID == currentTcpServerSocket.ID)
		{
			insDeviceCtrl.UpdateControlAnalyzeText(bCtrl: false);
			if (minePlot.minePlotSelf != null)
			{
				minePlot.minePlotSelf.UpdateControlAnalyzeText(bCtrl: false);
			}
		}
		ChannelChartPara oneEquipPara = chrDeviceCtrl.FrmEquip.GetOneEquipPara(CurrentGCID, currentChannelIndex);
		float num = oneEquipPara.fullScreenTime;
		float num2 = oneEquipPara.showLowLimit;
		float num3 = oneEquipPara.showHighLimit;
		if (num < 0.1f || (num2 == 0f && num3 == 0f))
		{
			num = 0.2f;
			num2 = -1f;
			num3 = 10f;
		}
		currentTcpServerSocket.sglsSampling[currentChannelIndex].disLg.lgXBeg = 0f;
		currentTcpServerSocket.sglsSampling[currentChannelIndex].disLg.lgX = num;
		currentTcpServerSocket.sglsSampling[currentChannelIndex].disLg.lgYBeg = num2;
		currentTcpServerSocket.sglsSampling[currentChannelIndex].disLg.lgY = num3 - num2;
		chrAcqCtrl.tbTime_DoubleClick(null, null);
		tabChannel_SelectedIndexChanged(null, null);
	}

	private void tmiFactorSetting_Click(object sender, EventArgs e)
	{
		if (!ValidatePassword.AllowAccess)
		{
			ValidatePassword validatePassword = new ValidatePassword();
			if (validatePassword.ShowDialog(this) != DialogResult.OK)
			{
				return;
			}
		}
		FactorParamDlg factorParamDlg = new FactorParamDlg();
		factorParamDlg.ShowDialog();
	}

	private void StartTcpServer()
	{
		cdlMgr.NewTcpServerMgr(this);
		try
		{
			mainTcpServer.OnReceiveData += OnTcpServer0ReceiveData;
			mainTcpServer.OnClientDisconnected += OnTcpServer0DisConnect;
			mainTcpServer.Start();
			modus0TcpServer.OnReceiveData += OnTcpServer1ReceiveData;
			modus0TcpServer.OnClientDisconnected += OnTcpServer1DisConnect;
			modus0TcpServer.Start();
			modus1TcpServer.OnReceiveData += OnTcpServer2ReceiveData;
			modus1TcpServer.OnClientDisconnected += OnTcpServer2DisConnect;
			modus1TcpServer.Start();
			toolStripStatusServer.Text = Lang.PS("服务启动成功... ", "Service started successfully ... ");
			if (sysParam.bComEnable)
			{
				mainTcpServer.AddComClient();
			}
		}
		catch (Exception ex)
		{
			toolStripStatusServer.Text = Lang.PS("启动AsyncTcpServer时发生异常：", "started AsyncTcpServer Error：") + ex.Message;
			MessageBox.Show(Lang.PS("工作站启用的25001、502或503端口被占用，启动服务失败！", "25001、502 Or 503 Port is occupied,Service failed to start!"), Lang.PS("端口被占用", "Port is occupied"), MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}
	}

	public void OnTcpServer1ReceiveData(object sender, TcpServerEventArgs e)
	{
		if (m_bLoading)
		{
			return;
		}
		Invoke(new MethodInvoker(method_23));
		byte[] array = new byte[e.DataSize];
		Array.Copy(e.ServerSocket.Buffer, array, array.Length);
		int num = 0;
		while (num >= 0)
		{
			byte[] array2 = new byte[12];
			Array.Copy(array, num * 12, array2, 0, array2.Length);
			method_5(array2, e, sysParam.iComModbusType);
			num++;
			if (array.Length - num * 12 < 12)
			{
				num = -1;
			}
		}
	}

	public void OnTcpServer1DisConnect(object sender, TcpServerEventArgs e)
	{
		if (!m_bLoading)
		{
			Invoke(new MethodInvoker(method_24));
		}
	}

	public TcpServerSocket GetCurrentTcpSocket()
	{
		TcpServerSocket currentTcpServerSocket = cdlMgr.CurrentTcpServerSocket;
		if (currentTcpServerSocket != null)
		{
			return currentTcpServerSocket;
		}
		return null;
	}

	public Instrument GetInstrument()
	{
		for (int i = 0; i < SysCfgDlg.sysConfig.pageInstrus.Length; i++)
		{
			if (SysCfgDlg.sysConfig.pageInstrus[i].name == CurrentGCID)
			{
				return SysCfgDlg.sysConfig.pageInstrus[i];
			}
		}
		return null;
	}

	public void LogToMsg(string msg)
	{
		if (this.IsDisposed) return;
		try
		{
			if (this.InvokeRequired)
			{
				this.Invoke((MethodInvoker)delegate { LogToMsg(msg); });
				return;
			}
			if (this.tsslMsg != null)
			{
				this.tsslMsg.Text = "消息: " + msg;
			}
		}
		catch { }
	}

	public void UpdateAutoInjStatus(string status)
	{
		if (this.IsDisposed) return;
		try
		{
			if (this.InvokeRequired)
			{
				this.Invoke((MethodInvoker)delegate { UpdateAutoInjStatus(status); });
				return;
			}
			if (this.statusStrip1 != null)
			{
				var lbl = this.statusStrip1.Items["tsslAutoInjStatus"] as ToolStripStatusLabel;
				if (lbl == null)
				{
					lbl = new ToolStripStatusLabel();
					lbl.Name = "tsslAutoInjStatus";
					lbl.ForeColor = System.Drawing.Color.Blue;
					this.statusStrip1.Items.Add(lbl);
				}
				lbl.Text = string.IsNullOrEmpty(status) ? "" : " | " + status;
			}
		}
		catch { }
	}

	public void OnTcpServer2ReceiveData(object sender, TcpServerEventArgs e)
	{
		if (m_bLoading)
		{
			return;
		}
		byte[] array = new byte[e.DataSize];
		Array.Copy(e.ServerSocket.Buffer, array, array.Length);
		int num = 0;
		while (num >= 0)
		{
			byte[] array2 = new byte[12];
			Array.Copy(array, num * 12, array2, 0, array2.Length);
			method_5(array2, e, 0);
			num++;
			if (array.Length - num * 12 < 12)
			{
				num = -1;
			}
		}
	}

	public void OnTcpServer2DisConnect(object sender, TcpServerEventArgs e)
	{
	}

	private void OnTcpServer0DisConnect(object sender, TcpServerEventArgs e)
	{
		if (!m_bLoading)
		{
			Invoke((MethodInvoker)delegate
			{
				SetChromDeviceImageIndex(e);
			});
		}
	}

	private void OnTcpServer0ReceiveData(object sender, TcpServerEventArgs e)
	{
		if (!m_bLoading)
		{
			Invoke((MethodInvoker)delegate
			{
				UpdateChromDevice(e);
			});
		}
	}

	private void StopTcpServer()
	{
		cdlMgr.StopTcpServerMgr();
	}

	public void CompServer_OnReceiveData(string ID)
	{
		if (!m_bLoading)
		{
			Invoke((MethodInvoker)delegate
			{
				AddNewChromDevice(ID);
			});
		}
	}

	private void method_23()
	{
		chrDeviceCtrl.method_23();
	}

	private void method_24()
	{
		chrDeviceCtrl.method_24();
	}

	public void SetChromDeviceImageIndex(TcpServerEventArgs e)
	{
		if (!(e.ServerSocket.ID.Trim() == ""))
		{
			chrDeviceCtrl.SetChromDeviceImageIndex(e);
		}
	}

	public void AddNewChromDevice(string strGCID)
	{
		if (!(strGCID.Trim() == ""))
		{
			chrDeviceCtrl.AddNewChromDevice(strGCID);
		}
	}

	private bool hasAutoConnected = false;

	public void UpdateChromDevice(TcpServerEventArgs e)
	{
		if (!(e.ServerSocket.ID.Trim() == ""))
		{
			if (e.ServerSocket.ID.Trim() == "709131284A484845")
			{
				e.ServerSocket.ID = cdlMgr.CurrentGCID;
			}
			chrDeviceCtrl.UpdateChromDevice(e);

			// 当底层的仪器回传数据且状态变为已连接（变绿或变蓝，ImageIndex 为 20 或 22）时，自动执行连接和恢复逻辑
			if (!hasAutoConnected && cdlMgr != null && cdlMgr.Count > 0)
			{
				bool isDeviceReady = false;
				for (int i = 0; i < chrDeviceCtrl.InstrumlistView.Items.Count; i++)
				{
					if (chrDeviceCtrl.InstrumlistView.Items[i].Tag.ToString() == e.ServerSocket.ID.Trim() && 
						(chrDeviceCtrl.InstrumlistView.Items[i].ImageIndex == 20 || chrDeviceCtrl.InstrumlistView.Items[i].ImageIndex == 22))
					{
						isDeviceReady = true;
						break;
					}
				}

				if (isDeviceReady)
				{
					hasAutoConnected = true;
					this.Invoke((MethodInvoker)delegate
					{
						try
						{
							string currentGCID = e.ServerSocket.ID.Trim();
							cdlMgr.CurrentGCID = currentGCID;

							SetCurrentChromDevice();

							if (chrDeviceCtrl != null && chrDeviceCtrl.InstrumlistView.Items.Count > 0)
							{
								for (int i = 0; i < chrDeviceCtrl.InstrumlistView.Items.Count; i++)
								{
									if (chrDeviceCtrl.InstrumlistView.Items[i].Tag.ToString() == currentGCID)
									{
										chrDeviceCtrl.InstrumlistView.Items[i].Selected = true;
										break;
									}
								}
							}

							LogToMsg("已自动连接到主板设备。");

							var tcp = GetCurrentTcpSocket();
							Instrument inst = null;
							if (SysCfgDlg.sysConfig.pageInstrus != null && SysCfgDlg.sysConfig.pageInstrus.Length > 0) {
								inst = SysCfgDlg.sysConfig.pageInstrus[0];
							}

							if (tcp != null && inst != null)
							{
								Instrument.LoadAutoInjState(out bool running, out int done, out int max, out float cycleMin);
								if (running && done > 0 && done < max)
								{
									LogToMsg($"检测到未完成的自动进样任务 (已完成 {done} 组)，等待设备稳定后恢复分析...");
									inst.autoInjDone = done;
									inst.autoInjMax = max;
									inst.RestoreCycleMin(cycleMin);
									Instrument.globalAutoInjRunning = true;
									inst.isAutoRestarting = true;

									System.Threading.Tasks.Task.Run(async delegate {
										// 抛弃不可靠的状态轮询，直接等待固定时间（8秒）让硬件完全初始化并稳定
										await System.Threading.Tasks.Task.Delay(8000);
										this.Invoke((MethodInvoker)delegate {
											try {
												// 检查标志位，防止等待期间用户手动操作了仪器
												if (inst.isAutoRestarting) {
													tcp.SendCmd(18);
												}
											} catch {}
										});
									});
								}
							}
						}
						catch { }
					});
				}
			}
		}
	}

	public void StartGather()
	{
		toolStripButton1_Click(null, null);
	}

	public void StopGather()
	{
		toolStripButton3_Click(null, null);
	}

	public void ClearGather()
	{
		toolStripButton6_Click(null, null);
	}

	public void UpdateAutoSamplerState()
	{
		button16_Click(null, null);
	}

	public void ModbusComSendData(byte[] data)
	{
		Invoke((MethodInvoker)delegate
		{
			ModbusComSendData(data);
		});
	}

	private void chromAcqCtrl1_Load(object sender, EventArgs e)
	{
	}

	private void MainmstSet_OnMethodSaveEvent(object sender, EventArgs e)
	{
		cdlMgr.CurrentChartParaOpera.mtdMgr = MainmstSet.CurMtdMgr;
		cdlMgr.SaveWorkSunFile();
		if (frmParam.kindMachine == 4)
		{
			if (cdlMgr.CurrentChartParaOpera != null)
			{
				cdlMgr.CurrentChartParaOpera.mtdMgr = MainmstSet.CurMtdMgr;
				cdlMgr.SaveWorkSunFile();
				if (VocCtrl.vocCtrl != null)
				{
					for (int i = 0; i < VocCtrl.vocCtrl.strCompName.Length; i++)
					{
						VocCtrl.vocCtrl.strCompName[i] = "";
					}
					if (cdlMgr.CurrentChartParaOpera.mtdMgr.caliGnl != null)
					{
						for (int j = 0; j < VocCtrl.vocCtrl.strCompName.Length && j < cdlMgr.CurrentChartParaOpera.mtdMgr.caliGnl.cmpds.Length; j++)
						{
							VocCtrl.vocCtrl.strCompName[j] = cdlMgr.CurrentChartParaOpera.mtdMgr.caliGnl.cmpds[j].cmpdInfo.name;
						}
					}
					if (MicrFPDCtrl.selfCtrl != null)
					{
						for (int k = 0; k < MicrFPDCtrl.selfCtrl.strCompName.Length; k++)
						{
							MicrFPDCtrl.selfCtrl.strCompName[k] = "";
						}
						if (cdlMgr.CurrentChartParaOpera.mtdMgr.caliGnl != null)
						{
							for (int l = 0; l < MicrFPDCtrl.selfCtrl.strCompName.Length && l < cdlMgr.CurrentChartParaOpera.mtdMgr.caliGnl.cmpds.Length; l++)
							{
								MicrFPDCtrl.selfCtrl.strCompName[l] = cdlMgr.CurrentChartParaOpera.mtdMgr.caliGnl.cmpds[l].cmpdInfo.name;
							}
						}
					}
				}
			}
		}
		else if (cdlMgr.GetChartParaOpera(1) != null)
		{
			if (VocCtrl.vocCtrl != null)
			{
				for (int m = 0; m < VocCtrl.vocCtrl.strCompName.Length; m++)
				{
					VocCtrl.vocCtrl.strCompName[m] = "";
				}
				if (cdlMgr.GetChartParaOpera(1).mtdMgr.caliGnl != null)
				{
					for (int n = 0; n < VocCtrl.vocCtrl.strCompName.Length && n < cdlMgr.GetChartParaOpera(1).mtdMgr.caliGnl.cmpds.Length; n++)
					{
						VocCtrl.vocCtrl.strCompName[n] = cdlMgr.GetChartParaOpera(1).mtdMgr.caliGnl.cmpds[n].cmpdInfo.name;
					}
				}
			}
			if (MicrFPDCtrl.selfCtrl != null)
			{
				for (int num = 0; num < MicrFPDCtrl.selfCtrl.strCompName.Length; num++)
				{
					MicrFPDCtrl.selfCtrl.strCompName[num] = "";
				}
				if (cdlMgr.CurrentChartParaOpera.mtdMgr.caliGnl != null)
				{
					for (int num2 = 0; num2 < MicrFPDCtrl.selfCtrl.strCompName.Length && num2 < cdlMgr.CurrentChartParaOpera.mtdMgr.caliGnl.cmpds.Length; num2++)
					{
						MicrFPDCtrl.selfCtrl.strCompName[num2] = cdlMgr.CurrentChartParaOpera.mtdMgr.caliGnl.cmpds[num2].cmpdInfo.name;
					}
				}
			}
		}
		if (cdlMgr.CurrentChartParaOpera == null || FormKR.selfCtrl == null)
		{
			return;
		}
		for (int num3 = 0; num3 < FormKR.selfCtrl.strCompName.Length; num3++)
		{
			FormKR.selfCtrl.strCompName[num3] = "";
		}
		if (cdlMgr.CurrentChartParaOpera.mtdMgr.caliGnl != null)
		{
			for (int num4 = 0; num4 < FormKR.selfCtrl.strCompName.Length && num4 < cdlMgr.CurrentChartParaOpera.mtdMgr.caliGnl.cmpds.Length; num4++)
			{
				FormKR.selfCtrl.strCompName[num4] = cdlMgr.CurrentChartParaOpera.mtdMgr.caliGnl.cmpds[num4].cmpdInfo.name;
			}
		}
	}

	private void timer_0_Tick(object sender, EventArgs e)
	{
		if (bAlarm)
		{
			if (FormAlarm.form == null)
			{
				FormAlarm.form = new FormAlarm();
			}
			FormAlarm.form.StrAlarmArray = strAlarmArray;
			FormAlarm.form.strAlarmFile = StrAlarmFile;
			FormAlarm.form.Show();
			bAlarm = false;
		}
	}

	public void UpdateControlTempText(bool bCtrl)
	{
	}

	private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
	{
	}

	private void barCheckChinese_Click(object sender, EventArgs e)
	{
		if (!m_bLoading)
		{
			barCheckChinese.Checked = !barCheckChinese.Checked;
			if (barCheckChinese.Checked)
			{
				barCheckEnglish.Checked = false;
				SetLang("zh-cn");
			}
		}
	}

	private void barCheckEnglish_Click(object sender, EventArgs e)
	{
		if (!m_bLoading)
		{
			barCheckEnglish.Checked = !barCheckEnglish.Checked;
			if (barCheckEnglish.Checked)
			{
				barCheckChinese.Checked = false;
				SetLang("en");
			}
		}
	}

	private void SetLang(string lang)
	{
		string langCode = Lang.LangCode;
		if (langCode != lang)
		{
			Lang.SetLastLangID(langCode);
			Lang.SetLangID(lang);
			CtrlLangPS.Instance.UpdateLanguageForAllControl(this);
		}
	}

	private void InitCheckedLangButton()
	{
		string language = sysParam.Language;
		if (language.Equals("en"))
		{
			barCheckEnglish.Checked = true;
			barCheckChinese.Checked = false;
		}
		else if (language.Equals("zh-cn"))
		{
			barCheckChinese.Checked = true;
			barCheckEnglish.Checked = false;
		}
		else
		{
			barCheckChinese.Checked = false;
			barCheckEnglish.Checked = false;
		}
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
		this.components = new System.ComponentModel.Container();
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(IBrainChrom2018.FormMainCtrl));
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
		this.splitContainer1 = new System.Windows.Forms.SplitContainer();
		this.MainmstSet = new IBrainChrom2018.MstSet();
		this.splitContainer2 = new System.Windows.Forms.SplitContainer();
		this.insDeviceCtrl1 = new IBrainChrom2018.InsDeviceCtrl();
		this.chromDeviceCtrl1 = new IBrainChrom2018.ChromDeviceCtrl();
		this.spcLeftBottom = new System.Windows.Forms.SplitContainer();
		this.chromAcqCtrl1 = new IBrainChrom2018.ChromAcqCtrl();
		this.tabChannel = new System.Windows.Forms.TabControl();
		this.tabPage11 = new System.Windows.Forms.TabPage();
		this.tabPage12 = new System.Windows.Forms.TabPage();
		this.tabPage13 = new System.Windows.Forms.TabPage();
		this.tabPage14 = new System.Windows.Forms.TabPage();
		this.cmPeakInfo = new System.Windows.Forms.ContextMenuStrip(this.components);
		this.峰尺寸ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.imageList1 = new System.Windows.Forms.ImageList(this.components);
		this.cmsIntegration = new System.Windows.Forms.ContextMenuStrip(this.components);
		this.miIntegAppendRow = new System.Windows.Forms.ToolStripMenuItem();
		this.miIntegDeleteRows = new System.Windows.Forms.ToolStripMenuItem();
		this.miIntegInsertRow = new System.Windows.Forms.ToolStripMenuItem();
		this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
		this.miIntegResetRows = new System.Windows.Forms.ToolStripMenuItem();
		this.miIntegRowsDown = new System.Windows.Forms.ToolStripMenuItem();
		this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
		this.miIntegRowsUp = new System.Windows.Forms.ToolStripMenuItem();
		this.menuStrip1 = new System.Windows.Forms.MenuStrip();
		this.tsmiFileMain = new System.Windows.Forms.ToolStripMenuItem();
		this.谱图处理ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
		this.退出ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.tsmiSystem = new System.Windows.Forms.ToolStripMenuItem();
		this.选项ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.系统配置ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
		this.ToolStripMenuItemClock = new System.Windows.Forms.ToolStripMenuItem();
		this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripMenuItem();
		this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
		this.ToolStripMenuItemTempCtrl = new System.Windows.Forms.ToolStripMenuItem();
		this.toolStripSeparator8 = new System.Windows.Forms.ToolStripSeparator();
		this.气路配置ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.ToolStripMenuItemTime = new System.Windows.Forms.ToolStripMenuItem();
		this.toolStripSeparator9 = new System.Windows.Forms.ToolStripSeparator();
		this.tsmiGraphGraft = new System.Windows.Forms.ToolStripMenuItem();
		this.tmiFactorSetting = new System.Windows.Forms.ToolStripMenuItem();
		this.tsmiCheckMain = new System.Windows.Forms.ToolStripMenuItem();
		this.ToolStripMenuItemGmp = new System.Windows.Forms.ToolStripMenuItem();
		this.tsmiHelpMain = new System.Windows.Forms.ToolStripMenuItem();
		this.tsmiHelp = new System.Windows.Forms.ToolStripMenuItem();
		this.tsmiAbout = new System.Windows.Forms.ToolStripMenuItem();
		this.tsmiUpgread = new System.Windows.Forms.ToolStripMenuItem();
		this.barSubItemLanguage = new System.Windows.Forms.ToolStripMenuItem();
		this.barCheckChinese = new System.Windows.Forms.ToolStripMenuItem();
		this.barCheckEnglish = new System.Windows.Forms.ToolStripMenuItem();
		this.文件FToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.测试ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.测试2ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.系统SToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.参数设置ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.帮助HToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.关于ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.statusStrip1 = new System.Windows.Forms.StatusStrip();
		this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
		this.toolStripStatusServer = new System.Windows.Forms.ToolStripStatusLabel();
		this.ToolLabelPeakInfo = new System.Windows.Forms.ToolStripStatusLabel();
		this.tsslMsg = new System.Windows.Forms.ToolStripStatusLabel();
		this.toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
		this.tsslMemory = new System.Windows.Forms.ToolStripStatusLabel();
		this.tsslCpu = new System.Windows.Forms.ToolStripStatusLabel();
		this.timer_0 = new System.Windows.Forms.Timer(this.components);
		this.toolStripButton2 = new System.Windows.Forms.ToolStripButton();
		this.toolStripButton3 = new System.Windows.Forms.ToolStripButton();
		this.toolStripButton8 = new System.Windows.Forms.ToolStripButton();
		this.toolStripButton9 = new System.Windows.Forms.ToolStripButton();
		this.timer_1 = new System.Windows.Forms.Timer(this.components);
		this.toolTip_0 = new System.Windows.Forms.ToolTip(this.components);
		this.dataGridViewTextBoxColumn17 = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.dataGridViewTextBoxColumn20 = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.dataGridViewTextBoxColumn21 = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.dataGridViewTextBoxColumn22 = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.dataGridViewTextBoxColumn23 = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.dataGridViewTextBoxColumn24 = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.dataGridViewTextBoxColumn25 = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.dataGridViewTextBoxColumn26 = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.dataGridViewTextBoxColumn27 = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.dataGridViewTextBoxColumn28 = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.dataGridViewTextBoxColumn29 = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.dataGridViewTextBoxColumn30 = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.dataGridViewTextBoxColumn31 = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.dataGridViewTextBoxColumn32 = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.dataGridViewTextBoxColumn33 = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.dataGridViewTextBoxColumn34 = new System.Windows.Forms.DataGridViewTextBoxColumn();
		((System.ComponentModel.ISupportInitialize)this.splitContainer1).BeginInit();
		this.splitContainer1.Panel2.SuspendLayout();
		this.splitContainer1.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.splitContainer2).BeginInit();
		this.splitContainer2.Panel1.SuspendLayout();
		this.splitContainer2.Panel2.SuspendLayout();
		this.splitContainer2.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.spcLeftBottom).BeginInit();
		this.spcLeftBottom.SuspendLayout();
		this.tabChannel.SuspendLayout();
		this.cmPeakInfo.SuspendLayout();
		this.cmsIntegration.SuspendLayout();
		this.menuStrip1.SuspendLayout();
		this.statusStrip1.SuspendLayout();
		base.SuspendLayout();
		this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
		this.splitContainer1.Location = new System.Drawing.Point(836, 171);
		this.splitContainer1.Name = "splitContainer1";
		this.splitContainer1.Panel2.Controls.Add(this.MainmstSet);
		this.splitContainer1.Size = new System.Drawing.Size(428, 401);
		this.splitContainer1.SplitterDistance = 392;
		this.splitContainer1.SplitterWidth = 5;
		this.splitContainer1.TabIndex = 0;
		this.MainmstSet.AutoScroll = true;
		this.MainmstSet.devManager = (IBrainChrom2018.InsDeviceManager)resources.GetObject("MainmstSet.devManager");
		this.MainmstSet.Dock = System.Windows.Forms.DockStyle.Fill;
		this.MainmstSet.Location = new System.Drawing.Point(0, 0);
		this.MainmstSet.Margin = new System.Windows.Forms.Padding(4);
		this.MainmstSet.Name = "MainmstSet";
		this.MainmstSet.PrintMethod = (IBrainChrom2018.PrintPara)resources.GetObject("MainmstSet.PrintMethod");
		this.MainmstSet.ShowComponentTable = false;
		this.MainmstSet.ShowMethodNew = true;
		this.MainmstSet.ShowOnlineMethod = false;
		this.MainmstSet.ShowOnlineMethod2 = false;
		this.MainmstSet.Size = new System.Drawing.Size(31, 401);
		this.MainmstSet.TabIndex = 1;
		this.MainmstSet.Visible = false;
		this.MainmstSet.OnMethodSaveEvent += new System.EventHandler(MainmstSet_OnMethodSaveEvent);
		this.splitContainer2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
		this.splitContainer2.Location = new System.Drawing.Point(0, 25);
		this.splitContainer2.Name = "splitContainer2";
		this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
		this.splitContainer2.Panel1.Controls.Add(this.insDeviceCtrl1);
		this.splitContainer2.Panel2.Controls.Add(this.chromDeviceCtrl1);
		this.splitContainer2.Size = new System.Drawing.Size(381, 714);
		this.splitContainer2.SplitterDistance = 603;
		this.splitContainer2.TabIndex = 0;
		this.insDeviceCtrl1.Dock = System.Windows.Forms.DockStyle.Fill;
		this.insDeviceCtrl1.Location = new System.Drawing.Point(0, 0);
		this.insDeviceCtrl1.Margin = new System.Windows.Forms.Padding(4);
		this.insDeviceCtrl1.Name = "insDeviceCtrl1";
		this.insDeviceCtrl1.ShowVOCMode = false;
		this.insDeviceCtrl1.Size = new System.Drawing.Size(379, 601);
		this.insDeviceCtrl1.TabIndex = 2;
		this.chromDeviceCtrl1.Dock = System.Windows.Forms.DockStyle.Fill;
		this.chromDeviceCtrl1.Location = new System.Drawing.Point(0, 0);
		this.chromDeviceCtrl1.Margin = new System.Windows.Forms.Padding(4);
		this.chromDeviceCtrl1.Name = "chromDeviceCtrl1";
		this.chromDeviceCtrl1.Size = new System.Drawing.Size(379, 105);
		this.chromDeviceCtrl1.TabIndex = 0;
		this.spcLeftBottom.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
		this.spcLeftBottom.Location = new System.Drawing.Point(662, 540);
		this.spcLeftBottom.Name = "spcLeftBottom";
		this.spcLeftBottom.Panel1MinSize = 0;
		this.spcLeftBottom.Panel2MinSize = 0;
		this.spcLeftBottom.Size = new System.Drawing.Size(54, 71);
		this.spcLeftBottom.SplitterDistance = 25;
		this.spcLeftBottom.SplitterWidth = 1;
		this.spcLeftBottom.TabIndex = 1;
		this.spcLeftBottom.Visible = false;
		this.chromAcqCtrl1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.chromAcqCtrl1.Location = new System.Drawing.Point(455, 68);
		this.chromAcqCtrl1.Margin = new System.Windows.Forms.Padding(4);
		this.chromAcqCtrl1.Name = "chromAcqCtrl1";
		this.chromAcqCtrl1.ShowLYTHCMethod = false;
		this.chromAcqCtrl1.ShowOnlineMethod = false;
		this.chromAcqCtrl1.ShowRNMode = false;
		this.chromAcqCtrl1.Size = new System.Drawing.Size(0, 253);
		this.chromAcqCtrl1.TabIndex = 1;
		this.chromAcqCtrl1.Load += new System.EventHandler(chromAcqCtrl1_Load);
		this.tabChannel.Controls.Add(this.tabPage11);
		this.tabChannel.Controls.Add(this.tabPage12);
		this.tabChannel.Controls.Add(this.tabPage13);
		this.tabChannel.Controls.Add(this.tabPage14);
		this.tabChannel.Location = new System.Drawing.Point(911, 89);
		this.tabChannel.Name = "tabChannel";
		this.tabChannel.SelectedIndex = 0;
		this.tabChannel.Size = new System.Drawing.Size(272, 22);
		this.tabChannel.TabIndex = 0;
		this.tabChannel.SelectedIndexChanged += new System.EventHandler(tabChannel_SelectedIndexChanged);
		this.tabPage11.BackColor = System.Drawing.Color.FromArgb(255, 192, 192);
		this.tabPage11.Location = new System.Drawing.Point(4, 22);
		this.tabPage11.Name = "tabPage11";
		this.tabPage11.Padding = new System.Windows.Forms.Padding(3);
		this.tabPage11.Size = new System.Drawing.Size(264, 0);
		this.tabPage11.TabIndex = 0;
		this.tabPage11.Text = "通道A";
		this.tabPage11.UseVisualStyleBackColor = true;
		this.tabPage12.BackColor = System.Drawing.Color.FromArgb(255, 224, 192);
		this.tabPage12.Location = new System.Drawing.Point(4, 22);
		this.tabPage12.Name = "tabPage12";
		this.tabPage12.Padding = new System.Windows.Forms.Padding(3);
		this.tabPage12.Size = new System.Drawing.Size(264, 0);
		this.tabPage12.TabIndex = 1;
		this.tabPage12.Text = "通道B";
		this.tabPage12.UseVisualStyleBackColor = true;
		this.tabPage13.BackColor = System.Drawing.Color.FromArgb(255, 255, 192);
		this.tabPage13.Location = new System.Drawing.Point(4, 22);
		this.tabPage13.Name = "tabPage13";
		this.tabPage13.Padding = new System.Windows.Forms.Padding(3);
		this.tabPage13.Size = new System.Drawing.Size(264, 0);
		this.tabPage13.TabIndex = 2;
		this.tabPage13.Text = "通道C";
		this.tabPage13.UseVisualStyleBackColor = true;
		this.tabPage14.BackColor = System.Drawing.Color.FromArgb(192, 255, 192);
		this.tabPage14.Location = new System.Drawing.Point(4, 22);
		this.tabPage14.Name = "tabPage14";
		this.tabPage14.Padding = new System.Windows.Forms.Padding(3);
		this.tabPage14.Size = new System.Drawing.Size(264, 0);
		this.tabPage14.TabIndex = 3;
		this.tabPage14.Text = "通道D";
		this.tabPage14.UseVisualStyleBackColor = true;
		this.cmPeakInfo.ImageScalingSize = new System.Drawing.Size(20, 20);
		this.cmPeakInfo.Items.AddRange(new System.Windows.Forms.ToolStripItem[1] { this.峰尺寸ToolStripMenuItem });
		this.cmPeakInfo.Name = "cmPeakInfo";
		this.cmPeakInfo.Size = new System.Drawing.Size(122, 26);
		this.峰尺寸ToolStripMenuItem.Name = "峰尺寸ToolStripMenuItem";
		this.峰尺寸ToolStripMenuItem.Size = new System.Drawing.Size(121, 22);
		this.峰尺寸ToolStripMenuItem.Text = "峰尺寸...";
		this.imageList1.ImageStream = (System.Windows.Forms.ImageListStreamer)resources.GetObject("imageList1.ImageStream");
		this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
		this.imageList1.Images.SetKeyName(0, "Abort.png");
		this.imageList1.Images.SetKeyName(1, "Check.png");
		this.imageList1.Images.SetKeyName(2, "check_CheckOK.png");
		this.imageList1.Images.SetKeyName(3, "check_HasError.png");
		this.imageList1.Images.SetKeyName(4, "check_NotCheck.png");
		this.imageList1.Images.SetKeyName(5, "ContextButton.png");
		this.imageList1.Images.SetKeyName(6, "CurMethod.png");
		this.imageList1.Images.SetKeyName(7, "InsertLine.png");
		this.imageList1.Images.SetKeyName(8, "KAlpha.png");
		this.imageList1.Images.SetKeyName(9, "Pause.png");
		this.imageList1.Images.SetKeyName(10, "RepeatInj.png");
		this.imageList1.Images.SetKeyName(11, "Resume.png");
		this.imageList1.Images.SetKeyName(12, "RowsDown.png");
		this.imageList1.Images.SetKeyName(13, "RowsUp.png");
		this.imageList1.Images.SetKeyName(14, "RunSequence.png");
		this.imageList1.Images.SetKeyName(15, "SkipVial.png");
		this.imageList1.Images.SetKeyName(16, "Snapshot.png");
		this.imageList1.Images.SetKeyName(17, "Stop.png");
		this.imageList1.Images.SetKeyName(18, "UnContextButton.png");
		this.imageList1.Images.SetKeyName(19, "201071655882501.jpg");
		this.imageList1.Images.SetKeyName(20, "isrun.ico");
		this.imageList1.Images.SetKeyName(21, "iserr.ico");
		this.imageList1.Images.SetKeyName(22, "Check.png");
		this.imageList1.Images.SetKeyName(23, "gif_47_091.gif");
		this.imageList1.Images.SetKeyName(24, "绿灯.gif");
		this.cmsIntegration.ImageScalingSize = new System.Drawing.Size(20, 20);
		this.cmsIntegration.Items.AddRange(new System.Windows.Forms.ToolStripItem[8] { this.miIntegAppendRow, this.miIntegDeleteRows, this.miIntegInsertRow, this.toolStripSeparator6, this.miIntegResetRows, this.miIntegRowsDown, this.toolStripSeparator7, this.miIntegRowsUp });
		this.cmsIntegration.Name = "cmsIntegration";
		this.cmsIntegration.Size = new System.Drawing.Size(113, 148);
		this.miIntegAppendRow.Name = "miIntegAppendRow";
		this.miIntegAppendRow.Size = new System.Drawing.Size(112, 22);
		this.miIntegAppendRow.Text = "添加行";
		this.miIntegDeleteRows.Name = "miIntegDeleteRows";
		this.miIntegDeleteRows.Size = new System.Drawing.Size(112, 22);
		this.miIntegDeleteRows.Text = "插入行";
		this.miIntegInsertRow.Name = "miIntegInsertRow";
		this.miIntegInsertRow.Size = new System.Drawing.Size(112, 22);
		this.miIntegInsertRow.Text = "删除行";
		this.toolStripSeparator6.Name = "toolStripSeparator6";
		this.toolStripSeparator6.Size = new System.Drawing.Size(109, 6);
		this.miIntegResetRows.Name = "miIntegResetRows";
		this.miIntegResetRows.Size = new System.Drawing.Size(112, 22);
		this.miIntegResetRows.Text = "上移";
		this.miIntegRowsDown.Name = "miIntegRowsDown";
		this.miIntegRowsDown.Size = new System.Drawing.Size(112, 22);
		this.miIntegRowsDown.Text = "下移";
		this.toolStripSeparator7.Name = "toolStripSeparator7";
		this.toolStripSeparator7.Size = new System.Drawing.Size(109, 6);
		this.miIntegRowsUp.Name = "miIntegRowsUp";
		this.miIntegRowsUp.Size = new System.Drawing.Size(112, 22);
		this.miIntegRowsUp.Text = "重置";
		this.menuStrip1.ImageScalingSize = new System.Drawing.Size(32, 32);
		this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[4] { this.tsmiFileMain, this.tsmiSystem, this.tsmiCheckMain, this.tsmiHelpMain });
		this.menuStrip1.Location = new System.Drawing.Point(0, 0);
		this.menuStrip1.Name = "menuStrip1";
		this.menuStrip1.Size = new System.Drawing.Size(381, 25);
		this.menuStrip1.TabIndex = 1;
		this.menuStrip1.Text = "menuStrip1";
		this.menuStrip1.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(menuStrip1_ItemClicked);
		this.tsmiFileMain.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[3] { this.谱图处理ToolStripMenuItem, this.toolStripMenuItem1, this.退出ToolStripMenuItem });
		this.tsmiFileMain.Name = "tsmiFileMain";
		this.tsmiFileMain.Size = new System.Drawing.Size(50, 21);
		this.tsmiFileMain.Text = "文件&F";
		this.谱图处理ToolStripMenuItem.Image = (System.Drawing.Image)resources.GetObject("谱图处理ToolStripMenuItem.Image");
		this.谱图处理ToolStripMenuItem.Name = "谱图处理ToolStripMenuItem";
		this.谱图处理ToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
		this.谱图处理ToolStripMenuItem.Text = "谱图处理";
		this.谱图处理ToolStripMenuItem.Click += new System.EventHandler(ToolStripMenuItemChromGraph_Click);
		this.toolStripMenuItem1.Image = (System.Drawing.Image)resources.GetObject("toolStripMenuItem1.Image");
		this.toolStripMenuItem1.Name = "toolStripMenuItem1";
		this.toolStripMenuItem1.Size = new System.Drawing.Size(136, 22);
		this.toolStripMenuItem1.Text = "编辑组份表";
		this.toolStripMenuItem1.Click += new System.EventHandler(toolStripMenuItem1_Click);
		this.退出ToolStripMenuItem.Image = (System.Drawing.Image)resources.GetObject("退出ToolStripMenuItem.Image");
		this.退出ToolStripMenuItem.Name = "退出ToolStripMenuItem";
		this.退出ToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
		this.退出ToolStripMenuItem.Text = "退出";
		this.退出ToolStripMenuItem.Click += new System.EventHandler(ToolStripMenuItemExit_Click);
		this.tsmiSystem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[13]
		{
			this.选项ToolStripMenuItem, this.系统配置ToolStripMenuItem, this.toolStripSeparator5, this.ToolStripMenuItemClock, this.toolStripMenuItem3, this.toolStripMenuItem2, this.ToolStripMenuItemTempCtrl, this.toolStripSeparator8, this.气路配置ToolStripMenuItem, this.ToolStripMenuItemTime, this.toolStripSeparator9,
			this.tsmiGraphGraft, this.tmiFactorSetting
		});
		this.tsmiSystem.Name = "tsmiSystem";
		this.tsmiSystem.Size = new System.Drawing.Size(51, 21);
		this.tsmiSystem.Text = "系统&S";
		this.选项ToolStripMenuItem.Image = (System.Drawing.Image)resources.GetObject("选项ToolStripMenuItem.Image");
		this.选项ToolStripMenuItem.Name = "选项ToolStripMenuItem";
		this.选项ToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
		this.选项ToolStripMenuItem.Text = "选项";
		this.选项ToolStripMenuItem.Click += new System.EventHandler(参数设置ToolStripMenuItem_Click);
		this.系统配置ToolStripMenuItem.Name = "系统配置ToolStripMenuItem";
		this.系统配置ToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
		this.系统配置ToolStripMenuItem.Text = "系统配置";
		this.系统配置ToolStripMenuItem.Click += new System.EventHandler(系统配置ToolStripMenuItem_Click);
		this.toolStripSeparator5.Name = "toolStripSeparator5";
		this.toolStripSeparator5.Size = new System.Drawing.Size(133, 6);
		this.ToolStripMenuItemClock.Image = (System.Drawing.Image)resources.GetObject("ToolStripMenuItemClock.Image");
		this.ToolStripMenuItemClock.Name = "ToolStripMenuItemClock";
		this.ToolStripMenuItemClock.Size = new System.Drawing.Size(136, 22);
		this.ToolStripMenuItemClock.Text = "校正时钟";
		this.ToolStripMenuItemClock.Visible = false;
		this.ToolStripMenuItemClock.Click += new System.EventHandler(ToolStripMenuItemClock_Click);
		this.toolStripMenuItem3.Name = "toolStripMenuItem3";
		this.toolStripMenuItem3.Size = new System.Drawing.Size(136, 22);
		this.toolStripMenuItem3.Text = "多位阀控制";
		this.toolStripMenuItem3.Visible = false;
		this.toolStripMenuItem3.Click += new System.EventHandler(toolStripMenuItem3_Click);
		this.toolStripMenuItem2.Image = (System.Drawing.Image)resources.GetObject("toolStripMenuItem2.Image");
		this.toolStripMenuItem2.Name = "toolStripMenuItem2";
		this.toolStripMenuItem2.Size = new System.Drawing.Size(136, 22);
		this.toolStripMenuItem2.Text = "复位多位阀";
		this.toolStripMenuItem2.Visible = false;
		this.toolStripMenuItem2.Click += new System.EventHandler(toolStripMenuItem2_Click);
		this.ToolStripMenuItemTempCtrl.Image = (System.Drawing.Image)resources.GetObject("ToolStripMenuItemTempCtrl.Image");
		this.ToolStripMenuItemTempCtrl.Name = "ToolStripMenuItemTempCtrl";
		this.ToolStripMenuItemTempCtrl.Size = new System.Drawing.Size(136, 22);
		this.ToolStripMenuItemTempCtrl.Text = "控温配置";
		this.ToolStripMenuItemTempCtrl.Click += new System.EventHandler(控温配置ToolStripMenuItem_Click);
		this.toolStripSeparator8.Name = "toolStripSeparator8";
		this.toolStripSeparator8.Size = new System.Drawing.Size(133, 6);
		this.气路配置ToolStripMenuItem.Image = (System.Drawing.Image)resources.GetObject("气路配置ToolStripMenuItem.Image");
		this.气路配置ToolStripMenuItem.Name = "气路配置ToolStripMenuItem";
		this.气路配置ToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
		this.气路配置ToolStripMenuItem.Text = "气路配置";
		this.气路配置ToolStripMenuItem.Visible = false;
		this.气路配置ToolStripMenuItem.Click += new System.EventHandler(气路配置ToolStripMenuItem_Click);
		this.ToolStripMenuItemTime.Image = (System.Drawing.Image)resources.GetObject("ToolStripMenuItemTime.Image");
		this.ToolStripMenuItemTime.Name = "ToolStripMenuItemTime";
		this.ToolStripMenuItemTime.Size = new System.Drawing.Size(136, 22);
		this.ToolStripMenuItemTime.Text = "时间程序";
		this.ToolStripMenuItemTime.Click += new System.EventHandler(ToolStripMenuItemTime_Click);
		this.toolStripSeparator9.Name = "toolStripSeparator9";
		this.toolStripSeparator9.Size = new System.Drawing.Size(133, 6);
		this.tsmiGraphGraft.Name = "tsmiGraphGraft";
		this.tsmiGraphGraft.Size = new System.Drawing.Size(136, 22);
		this.tsmiGraphGraft.Text = "谱图嫁接";
		this.tsmiGraphGraft.Click += new System.EventHandler(谱图嫁接ToolStripMenuItem_Click);
		this.tmiFactorSetting.Name = "tmiFactorSetting";
		this.tmiFactorSetting.Size = new System.Drawing.Size(136, 22);
		this.tmiFactorSetting.Text = "厂家配置";
		this.tmiFactorSetting.Visible = false;
		this.tmiFactorSetting.Click += new System.EventHandler(tmiFactorSetting_Click);
		this.tsmiCheckMain.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[1] { this.ToolStripMenuItemGmp });
		this.tsmiCheckMain.Name = "tsmiCheckMain";
		this.tsmiCheckMain.Size = new System.Drawing.Size(49, 21);
		this.tsmiCheckMain.Text = "检查&J";
		this.ToolStripMenuItemGmp.Name = "ToolStripMenuItemGmp";
		this.ToolStripMenuItemGmp.Size = new System.Drawing.Size(124, 22);
		this.ToolStripMenuItemGmp.Text = "检查跟踪";
		this.ToolStripMenuItemGmp.Click += new System.EventHandler(ToolStripMenuItemGmp_Click);
		this.tsmiHelpMain.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[4] { this.tsmiHelp, this.tsmiAbout, this.tsmiUpgread, this.barSubItemLanguage });
		this.tsmiHelpMain.Name = "tsmiHelpMain";
		this.tsmiHelpMain.Size = new System.Drawing.Size(50, 21);
		this.tsmiHelpMain.Text = "帮助&F";
		this.tsmiHelp.Image = (System.Drawing.Image)resources.GetObject("tsmiHelp.Image");
		this.tsmiHelp.Name = "tsmiHelp";
		this.tsmiHelp.Size = new System.Drawing.Size(124, 22);
		this.tsmiHelp.Text = "帮助";
		this.tsmiHelp.Click += new System.EventHandler(ToolStripMenuItemHelp_Click);
		this.tsmiAbout.Image = (System.Drawing.Image)resources.GetObject("tsmiAbout.Image");
		this.tsmiAbout.Name = "tsmiAbout";
		this.tsmiAbout.Size = new System.Drawing.Size(124, 22);
		this.tsmiAbout.Text = "关于...";
		this.tsmiAbout.Click += new System.EventHandler(ToolStripMenuItemAbout_Click);
		this.tsmiUpgread.Image = (System.Drawing.Image)resources.GetObject("tsmiUpgread.Image");
		this.tsmiUpgread.Name = "tsmiUpgread";
		this.tsmiUpgread.Size = new System.Drawing.Size(124, 22);
		this.tsmiUpgread.Text = "检查更新";
		this.tsmiUpgread.Visible = false;
		this.tsmiUpgread.Click += new System.EventHandler(ToolStripMenuItemUpgread_Click);
		this.barSubItemLanguage.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[2] { this.barCheckChinese, this.barCheckEnglish });
		this.barSubItemLanguage.Name = "barSubItemLanguage";
		this.barSubItemLanguage.Size = new System.Drawing.Size(124, 22);
		this.barSubItemLanguage.Text = "语言";
		this.barCheckChinese.Checked = true;
		this.barCheckChinese.CheckState = System.Windows.Forms.CheckState.Checked;
		this.barCheckChinese.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
		this.barCheckChinese.Name = "barCheckChinese";
		this.barCheckChinese.Size = new System.Drawing.Size(145, 22);
		this.barCheckChinese.Text = "中文Chinese";
		this.barCheckChinese.Click += new System.EventHandler(barCheckChinese_Click);
		this.barCheckEnglish.Name = "barCheckEnglish";
		this.barCheckEnglish.Size = new System.Drawing.Size(145, 22);
		this.barCheckEnglish.Text = "英文English";
		this.barCheckEnglish.Click += new System.EventHandler(barCheckEnglish_Click);
		this.文件FToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[2] { this.测试ToolStripMenuItem, this.测试2ToolStripMenuItem });
		this.文件FToolStripMenuItem.Name = "文件FToolStripMenuItem";
		this.文件FToolStripMenuItem.Size = new System.Drawing.Size(58, 21);
		this.文件FToolStripMenuItem.Text = "文件(&F)";
		this.测试ToolStripMenuItem.Name = "测试ToolStripMenuItem";
		this.测试ToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
		this.测试ToolStripMenuItem.Text = "谱图处理";
		this.测试2ToolStripMenuItem.Name = "测试2ToolStripMenuItem";
		this.测试2ToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
		this.测试2ToolStripMenuItem.Text = "退出";
		this.系统SToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[1] { this.参数设置ToolStripMenuItem });
		this.系统SToolStripMenuItem.Name = "系统SToolStripMenuItem";
		this.系统SToolStripMenuItem.Size = new System.Drawing.Size(59, 21);
		this.系统SToolStripMenuItem.Text = "系统(&S)";
		this.参数设置ToolStripMenuItem.Name = "参数设置ToolStripMenuItem";
		this.参数设置ToolStripMenuItem.Size = new System.Drawing.Size(100, 22);
		this.参数设置ToolStripMenuItem.Text = "选项";
		this.参数设置ToolStripMenuItem.Click += new System.EventHandler(参数设置ToolStripMenuItem_Click);
		this.帮助HToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[1] { this.关于ToolStripMenuItem });
		this.帮助HToolStripMenuItem.Name = "帮助HToolStripMenuItem";
		this.帮助HToolStripMenuItem.Size = new System.Drawing.Size(61, 21);
		this.帮助HToolStripMenuItem.Text = "帮助(&H)";
		this.关于ToolStripMenuItem.Name = "关于ToolStripMenuItem";
		this.关于ToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
		this.关于ToolStripMenuItem.Text = "关于英泊";
		this.关于ToolStripMenuItem.Click += new System.EventHandler(ToolStripMenuItemAbout_Click);
		this.statusStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
		this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[7] { this.toolStripStatusLabel1, this.toolStripStatusServer, this.ToolLabelPeakInfo, this.tsslMsg, this.toolStripStatusLabel2, this.tsslMemory, this.tsslCpu });
		this.statusStrip1.Location = new System.Drawing.Point(0, 739);
		this.statusStrip1.Name = "statusStrip1";
		this.statusStrip1.Padding = new System.Windows.Forms.Padding(1, 0, 15, 0);
		this.statusStrip1.Size = new System.Drawing.Size(381, 22);
		this.statusStrip1.TabIndex = 2;
		this.statusStrip1.Text = "statusStrip1";
		this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
		this.toolStripStatusLabel1.Size = new System.Drawing.Size(176, 17);
		this.toolStripStatusLabel1.Text = "欢迎使用色谱仪工作站控制软件";
		this.toolStripStatusServer.Name = "toolStripStatusServer";
		this.toolStripStatusServer.Size = new System.Drawing.Size(56, 17);
		this.toolStripStatusServer.Text = "网络服务";
		this.ToolLabelPeakInfo.Name = "ToolLabelPeakInfo";
		this.ToolLabelPeakInfo.Size = new System.Drawing.Size(0, 17);
		this.tsslMsg.ForeColor = System.Drawing.Color.Black;
		this.tsslMsg.Name = "tsslMsg";
		this.tsslMsg.Size = new System.Drawing.Size(44, 17);
		this.tsslMsg.Text = "消息：";
		this.toolStripStatusLabel2.Name = "toolStripStatusLabel2";
		this.toolStripStatusLabel2.Size = new System.Drawing.Size(60, 17);
		this.toolStripStatusLabel2.Text = "             ";
		this.tsslMemory.Name = "tsslMemory";
		this.tsslMemory.Size = new System.Drawing.Size(35, 17);
		this.tsslMemory.Text = "内存:";
		this.tsslCpu.Name = "tsslCpu";
		this.tsslCpu.Size = new System.Drawing.Size(35, 17);
		this.tsslCpu.Text = "CPU:";
		this.timer_0.Enabled = true;
		this.timer_0.Interval = 500;
		this.timer_0.Tick += new System.EventHandler(timer_0_Tick);
		this.toolStripButton2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
		this.toolStripButton2.Image = (System.Drawing.Image)resources.GetObject("toolStripButton2.Image");
		this.toolStripButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
		this.toolStripButton2.Name = "toolStripButton2";
		this.toolStripButton2.Size = new System.Drawing.Size(36, 36);
		this.toolStripButton2.Text = "toolStripButton2";
		this.toolStripButton2.ToolTipText = "暂停采集";
		this.toolStripButton2.Visible = false;
		this.toolStripButton2.Click += new System.EventHandler(toolStripButton2_Click);
		this.toolStripButton3.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
		this.toolStripButton3.Image = (System.Drawing.Image)resources.GetObject("toolStripButton3.Image");
		this.toolStripButton3.ImageTransparentColor = System.Drawing.Color.Magenta;
		this.toolStripButton3.Name = "toolStripButton3";
		this.toolStripButton3.Size = new System.Drawing.Size(36, 36);
		this.toolStripButton3.Text = "toolStripButton3";
		this.toolStripButton8.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
		this.toolStripButton8.Image = (System.Drawing.Image)resources.GetObject("toolStripButton8.Image");
		this.toolStripButton8.ImageTransparentColor = System.Drawing.Color.Magenta;
		this.toolStripButton8.Name = "toolStripButton8";
		this.toolStripButton8.Size = new System.Drawing.Size(36, 36);
		this.toolStripButton8.Text = "toolStripButton8";
		this.toolStripButton9.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
		this.toolStripButton9.Image = (System.Drawing.Image)resources.GetObject("toolStripButton9.Image");
		this.toolStripButton9.ImageTransparentColor = System.Drawing.Color.Magenta;
		this.toolStripButton9.Name = "toolStripButton9";
		this.toolStripButton9.Size = new System.Drawing.Size(36, 36);
		this.toolStripButton9.Text = "toolStripButton9";
		this.timer_1.Enabled = true;
		this.timer_1.Interval = 1000;
		this.timer_1.Tick += new System.EventHandler(timer_1_Tick);
		dataGridViewCellStyle.BackColor = System.Drawing.Color.Blue;
		dataGridViewCellStyle.Font = new System.Drawing.Font("宋体", 9f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 134);
		dataGridViewCellStyle.ForeColor = System.Drawing.Color.White;
		this.dataGridViewTextBoxColumn17.DefaultCellStyle = dataGridViewCellStyle;
		this.dataGridViewTextBoxColumn17.HeaderText = "";
		this.dataGridViewTextBoxColumn17.MaxInputLength = 10;
		this.dataGridViewTextBoxColumn17.MinimumWidth = 20;
		this.dataGridViewTextBoxColumn17.Name = "dataGridViewTextBoxColumn17";
		this.dataGridViewTextBoxColumn17.ReadOnly = true;
		this.dataGridViewTextBoxColumn17.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.dataGridViewTextBoxColumn17.Width = 80;
		dataGridViewCellStyle2.BackColor = System.Drawing.Color.Black;
		dataGridViewCellStyle2.Font = new System.Drawing.Font("宋体", 9f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 134);
		dataGridViewCellStyle2.ForeColor = System.Drawing.Color.Yellow;
		this.dataGridViewTextBoxColumn20.DefaultCellStyle = dataGridViewCellStyle2;
		this.dataGridViewTextBoxColumn20.HeaderText = "保护(°C)";
		this.dataGridViewTextBoxColumn20.MaxInputLength = 10;
		this.dataGridViewTextBoxColumn20.Name = "dataGridViewTextBoxColumn20";
		this.dataGridViewTextBoxColumn20.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.dataGridViewTextBoxColumn20.Width = 80;
		dataGridViewCellStyle3.BackColor = System.Drawing.Color.Black;
		dataGridViewCellStyle3.Font = new System.Drawing.Font("宋体", 9f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 134);
		dataGridViewCellStyle3.ForeColor = System.Drawing.Color.Lime;
		this.dataGridViewTextBoxColumn21.DefaultCellStyle = dataGridViewCellStyle3;
		this.dataGridViewTextBoxColumn21.HeaderText = "模块标识";
		this.dataGridViewTextBoxColumn21.MaxInputLength = 10;
		this.dataGridViewTextBoxColumn21.MinimumWidth = 20;
		this.dataGridViewTextBoxColumn21.Name = "dataGridViewTextBoxColumn21";
		this.dataGridViewTextBoxColumn21.ReadOnly = true;
		this.dataGridViewTextBoxColumn21.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.dataGridViewTextBoxColumn21.Width = 80;
		dataGridViewCellStyle4.BackColor = System.Drawing.Color.Black;
		dataGridViewCellStyle4.Font = new System.Drawing.Font("宋体", 9f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 134);
		dataGridViewCellStyle4.ForeColor = System.Drawing.Color.White;
		this.dataGridViewTextBoxColumn22.DefaultCellStyle = dataGridViewCellStyle4;
		this.dataGridViewTextBoxColumn22.HeaderText = "         版         本";
		this.dataGridViewTextBoxColumn22.MaxInputLength = 100;
		this.dataGridViewTextBoxColumn22.MinimumWidth = 20;
		this.dataGridViewTextBoxColumn22.Name = "dataGridViewTextBoxColumn22";
		this.dataGridViewTextBoxColumn22.ReadOnly = true;
		this.dataGridViewTextBoxColumn22.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.dataGridViewTextBoxColumn22.Width = 250;
		dataGridViewCellStyle5.BackColor = System.Drawing.Color.Blue;
		dataGridViewCellStyle5.Font = new System.Drawing.Font("宋体", 9f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 134);
		dataGridViewCellStyle5.ForeColor = System.Drawing.Color.White;
		this.dataGridViewTextBoxColumn23.DefaultCellStyle = dataGridViewCellStyle5;
		this.dataGridViewTextBoxColumn23.HeaderText = "";
		this.dataGridViewTextBoxColumn23.MaxInputLength = 10;
		this.dataGridViewTextBoxColumn23.MinimumWidth = 20;
		this.dataGridViewTextBoxColumn23.Name = "dataGridViewTextBoxColumn23";
		this.dataGridViewTextBoxColumn23.ReadOnly = true;
		this.dataGridViewTextBoxColumn23.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.dataGridViewTextBoxColumn23.Width = 80;
		dataGridViewCellStyle6.BackColor = System.Drawing.Color.Black;
		dataGridViewCellStyle6.Font = new System.Drawing.Font("宋体", 9f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 134);
		dataGridViewCellStyle6.ForeColor = System.Drawing.Color.Lime;
		this.dataGridViewTextBoxColumn24.DefaultCellStyle = dataGridViewCellStyle6;
		this.dataGridViewTextBoxColumn24.HeaderText = "升压速率(psi/min)";
		this.dataGridViewTextBoxColumn24.MaxInputLength = 10;
		this.dataGridViewTextBoxColumn24.MinimumWidth = 20;
		this.dataGridViewTextBoxColumn24.Name = "dataGridViewTextBoxColumn24";
		this.dataGridViewTextBoxColumn24.ReadOnly = true;
		this.dataGridViewTextBoxColumn24.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.dataGridViewTextBoxColumn24.Width = 80;
		dataGridViewCellStyle7.BackColor = System.Drawing.Color.Black;
		dataGridViewCellStyle7.Font = new System.Drawing.Font("宋体", 9f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 134);
		dataGridViewCellStyle7.ForeColor = System.Drawing.Color.White;
		this.dataGridViewTextBoxColumn25.DefaultCellStyle = dataGridViewCellStyle7;
		this.dataGridViewTextBoxColumn25.HeaderText = "保持压力(psi/min)";
		this.dataGridViewTextBoxColumn25.MaxInputLength = 10;
		this.dataGridViewTextBoxColumn25.MinimumWidth = 20;
		this.dataGridViewTextBoxColumn25.Name = "dataGridViewTextBoxColumn25";
		this.dataGridViewTextBoxColumn25.ReadOnly = true;
		this.dataGridViewTextBoxColumn25.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.dataGridViewTextBoxColumn25.Width = 80;
		dataGridViewCellStyle8.BackColor = System.Drawing.Color.Black;
		dataGridViewCellStyle8.Font = new System.Drawing.Font("宋体", 9f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 134);
		dataGridViewCellStyle8.ForeColor = System.Drawing.Color.Yellow;
		this.dataGridViewTextBoxColumn26.DefaultCellStyle = dataGridViewCellStyle8;
		this.dataGridViewTextBoxColumn26.HeaderText = "保持时间(min)";
		this.dataGridViewTextBoxColumn26.MaxInputLength = 10;
		this.dataGridViewTextBoxColumn26.MinimumWidth = 20;
		this.dataGridViewTextBoxColumn26.Name = "dataGridViewTextBoxColumn26";
		this.dataGridViewTextBoxColumn26.ReadOnly = true;
		this.dataGridViewTextBoxColumn26.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.dataGridViewTextBoxColumn26.Width = 80;
		dataGridViewCellStyle9.BackColor = System.Drawing.Color.Blue;
		dataGridViewCellStyle9.Font = new System.Drawing.Font("宋体", 9f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 134);
		dataGridViewCellStyle9.ForeColor = System.Drawing.Color.White;
		this.dataGridViewTextBoxColumn27.DefaultCellStyle = dataGridViewCellStyle9;
		this.dataGridViewTextBoxColumn27.HeaderText = "";
		this.dataGridViewTextBoxColumn27.MaxInputLength = 10;
		this.dataGridViewTextBoxColumn27.MinimumWidth = 20;
		this.dataGridViewTextBoxColumn27.Name = "dataGridViewTextBoxColumn27";
		this.dataGridViewTextBoxColumn27.ReadOnly = true;
		this.dataGridViewTextBoxColumn27.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.dataGridViewTextBoxColumn27.Width = 80;
		dataGridViewCellStyle10.BackColor = System.Drawing.Color.Black;
		dataGridViewCellStyle10.Font = new System.Drawing.Font("宋体", 9f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 134);
		dataGridViewCellStyle10.ForeColor = System.Drawing.Color.Lime;
		this.dataGridViewTextBoxColumn28.DefaultCellStyle = dataGridViewCellStyle10;
		this.dataGridViewTextBoxColumn28.HeaderText = "升压速率(psi/min)";
		this.dataGridViewTextBoxColumn28.MaxInputLength = 10;
		this.dataGridViewTextBoxColumn28.MinimumWidth = 20;
		this.dataGridViewTextBoxColumn28.Name = "dataGridViewTextBoxColumn28";
		this.dataGridViewTextBoxColumn28.ReadOnly = true;
		this.dataGridViewTextBoxColumn28.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.dataGridViewTextBoxColumn28.Width = 80;
		dataGridViewCellStyle11.BackColor = System.Drawing.Color.Black;
		dataGridViewCellStyle11.Font = new System.Drawing.Font("宋体", 9f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 134);
		dataGridViewCellStyle11.ForeColor = System.Drawing.Color.White;
		this.dataGridViewTextBoxColumn29.DefaultCellStyle = dataGridViewCellStyle11;
		this.dataGridViewTextBoxColumn29.HeaderText = "保持压力(psi/min)";
		this.dataGridViewTextBoxColumn29.MaxInputLength = 10;
		this.dataGridViewTextBoxColumn29.Name = "dataGridViewTextBoxColumn29";
		this.dataGridViewTextBoxColumn29.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.dataGridViewTextBoxColumn29.Width = 80;
		dataGridViewCellStyle12.BackColor = System.Drawing.Color.Black;
		dataGridViewCellStyle12.Font = new System.Drawing.Font("宋体", 9f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 134);
		dataGridViewCellStyle12.ForeColor = System.Drawing.Color.Yellow;
		this.dataGridViewTextBoxColumn30.DefaultCellStyle = dataGridViewCellStyle12;
		this.dataGridViewTextBoxColumn30.HeaderText = "保持时间(min)";
		this.dataGridViewTextBoxColumn30.MaxInputLength = 10;
		this.dataGridViewTextBoxColumn30.MinimumWidth = 20;
		this.dataGridViewTextBoxColumn30.Name = "dataGridViewTextBoxColumn30";
		this.dataGridViewTextBoxColumn30.ReadOnly = true;
		this.dataGridViewTextBoxColumn30.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.dataGridViewTextBoxColumn30.Width = 80;
		dataGridViewCellStyle13.BackColor = System.Drawing.Color.Blue;
		dataGridViewCellStyle13.Font = new System.Drawing.Font("宋体", 9f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 134);
		dataGridViewCellStyle13.ForeColor = System.Drawing.Color.White;
		this.dataGridViewTextBoxColumn31.DefaultCellStyle = dataGridViewCellStyle13;
		this.dataGridViewTextBoxColumn31.HeaderText = "";
		this.dataGridViewTextBoxColumn31.MaxInputLength = 10;
		this.dataGridViewTextBoxColumn31.MinimumWidth = 20;
		this.dataGridViewTextBoxColumn31.Name = "dataGridViewTextBoxColumn31";
		this.dataGridViewTextBoxColumn31.ReadOnly = true;
		this.dataGridViewTextBoxColumn31.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.dataGridViewTextBoxColumn31.Width = 80;
		dataGridViewCellStyle14.BackColor = System.Drawing.Color.Black;
		dataGridViewCellStyle14.Font = new System.Drawing.Font("宋体", 9f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 134);
		dataGridViewCellStyle14.ForeColor = System.Drawing.Color.Lime;
		this.dataGridViewTextBoxColumn32.DefaultCellStyle = dataGridViewCellStyle14;
		this.dataGridViewTextBoxColumn32.HeaderText = "升压速率(psi/min)";
		this.dataGridViewTextBoxColumn32.MaxInputLength = 10;
		this.dataGridViewTextBoxColumn32.MinimumWidth = 20;
		this.dataGridViewTextBoxColumn32.Name = "dataGridViewTextBoxColumn32";
		this.dataGridViewTextBoxColumn32.ReadOnly = true;
		this.dataGridViewTextBoxColumn32.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.dataGridViewTextBoxColumn32.Width = 80;
		dataGridViewCellStyle15.BackColor = System.Drawing.Color.Black;
		dataGridViewCellStyle15.Font = new System.Drawing.Font("宋体", 9f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 134);
		dataGridViewCellStyle15.ForeColor = System.Drawing.Color.White;
		this.dataGridViewTextBoxColumn33.DefaultCellStyle = dataGridViewCellStyle15;
		this.dataGridViewTextBoxColumn33.HeaderText = "保持压力(psi/min)";
		this.dataGridViewTextBoxColumn33.MaxInputLength = 10;
		this.dataGridViewTextBoxColumn33.MinimumWidth = 20;
		this.dataGridViewTextBoxColumn33.Name = "dataGridViewTextBoxColumn33";
		this.dataGridViewTextBoxColumn33.ReadOnly = true;
		this.dataGridViewTextBoxColumn33.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.dataGridViewTextBoxColumn33.Width = 80;
		dataGridViewCellStyle16.BackColor = System.Drawing.Color.Black;
		dataGridViewCellStyle16.Font = new System.Drawing.Font("宋体", 9f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 134);
		dataGridViewCellStyle16.ForeColor = System.Drawing.Color.Yellow;
		this.dataGridViewTextBoxColumn34.DefaultCellStyle = dataGridViewCellStyle16;
		this.dataGridViewTextBoxColumn34.HeaderText = "保持时间(min)";
		this.dataGridViewTextBoxColumn34.MaxInputLength = 10;
		this.dataGridViewTextBoxColumn34.Name = "dataGridViewTextBoxColumn34";
		this.dataGridViewTextBoxColumn34.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.dataGridViewTextBoxColumn34.Width = 80;
		base.AutoScaleDimensions = new System.Drawing.SizeF(96f, 96f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
		base.ClientSize = new System.Drawing.Size(381, 761);
		base.Controls.Add(this.spcLeftBottom);
		base.Controls.Add(this.tabChannel);
		base.Controls.Add(this.splitContainer2);
		base.Controls.Add(this.chromAcqCtrl1);
		base.Controls.Add(this.splitContainer1);
		base.Controls.Add(this.menuStrip1);
		base.Controls.Add(this.statusStrip1);
		base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
		base.MainMenuStrip = this.menuStrip1;
		base.Name = "FormMainCtrl";
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.Text = "IBrainChrom";
		base.TopMost = true;
		base.FormClosing += new System.Windows.Forms.FormClosingEventHandler(FormMain_FormClosing);
		base.Load += new System.EventHandler(FormMain_Load);
		base.KeyDown += new System.Windows.Forms.KeyEventHandler(FormMain_KeyDown);
		this.splitContainer1.Panel2.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.splitContainer1).EndInit();
		this.splitContainer1.ResumeLayout(false);
		this.splitContainer2.Panel1.ResumeLayout(false);
		this.splitContainer2.Panel2.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.splitContainer2).EndInit();
		this.splitContainer2.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.spcLeftBottom).EndInit();
		this.spcLeftBottom.ResumeLayout(false);
		this.tabChannel.ResumeLayout(false);
		this.cmPeakInfo.ResumeLayout(false);
		this.cmsIntegration.ResumeLayout(false);
		this.menuStrip1.ResumeLayout(false);
		this.menuStrip1.PerformLayout();
		this.statusStrip1.ResumeLayout(false);
		this.statusStrip1.PerformLayout();
		base.ResumeLayout(false);
		base.PerformLayout();
	}

	object ChromFormInterface.Invoke(Delegate method)
	{
		return Invoke(method);
	}
}
