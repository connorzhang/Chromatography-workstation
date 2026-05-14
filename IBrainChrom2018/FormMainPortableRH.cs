using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using HZH_Controls;
using HZH_Controls.Controls;
using IBrainChrom2018.ChromFile;
using IBrainChrom2018.Properties;
using IBrainChrom2018.Unit;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;

namespace IBrainChrom2018;

public class FormMainPortableRH : Form, FrmChromatManagerInterface, ChromFormInterface
{
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	public delegate void CallbackFun(int i);

	public const uint WM_SYSCOMMAND = 274u;

	public const uint SC_RESTORE = 61728u;

	private string OnScreenKeyboadApplication = "osk.exe";

	private ChromDeviceListMgr cdlMgr = ChromDeviceListMgr.Create();

	public int iChannel = 0;

	private SystemParam sysParam = SystemParam.Create();

	public bool AutoTempCtr = true;

	public List<PortableGridModel> lstSource = new List<PortableGridModel>();

	public bool bUpdateCheck = false;

	private List<statisticsGridModel> statisSource = new List<statisticsGridModel>();

	private LYTHCPara lythcParamMgr = LYTHCPara.Create();

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

	public string strCollectSJDW = "";

	public string strCollectJYDW = "";

	public string strCollectSite = "";

	public string strCollectP = "";

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

	private byte[] data_len61 = new byte[61]
	{
		71, 67, 75, 67, 0, 54, 55, 48, 57, 49,
		51, 49, 50, 56, 52, 65, 52, 56, 52, 56,
		52, 53, 3, 167, 37, 48, 0, 1, 50, 1,
		80, 0, 16, 1, 18, 52, 18, 52, 0, 0,
		16, 0, 0, 0, 0, 0, 0, 0, 16, 0,
		0, 0, 0, 0, 0, 0, 0, 16, 0, 0,
		162
	};

	public static FormMainPortableRH fromMain = null;

	public ChromAcqCtrl chromAcqCtrl3;

	public ChromFormCtrl chromFormCtrl2;

	private TabControl tabCtrl2;

	public VocCtrl vocCtrl2;

	private FormMainParam frmParam = FormMainParam.Create();

	private MstSet MainmstSet1;

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

	private InsDeviceCtrl insDeviceCtrl1;

	private ChromDeviceCtrl chromDeviceCtrl1;

	private SplitContainer spcLeftBottom;

	private ToolStripStatusLabel toolStripStatusLabel2;

	private ToolStripStatusLabel tsslMemory;

	private ToolStripStatusLabel tsslCpu;

	private ToolStripMenuItem tmiFactorSetting;

	private ToolStripMenuItem barSubItemLanguage;

	private ToolStripMenuItem barCheckChinese;

	private ToolStripMenuItem barCheckEnglish;

	private Label labNMHCT;

	private Label labNMHC;

	private Label labCH4;

	private Label label4;

	private Label labTHC;

	private Label label2;

	private Label labTime;

	private Label label6;

	private ToolStrip toolStrip1;

	private TabControlExt tabControlExt1;

	private TabPage tabPage3;

	private TabPage tabPage4;

	private ChromAcqCtrlPortable chromAcqCtrl1;

	private UCBtnExt btnStartTe;

	private UCBtnExt btnStartAn;

	private UCTextBoxEx tbTimeCycle1;

	private Label label7;

	private Label 分析次数;

	private UCTextBoxEx tbTimesCycle1;

	private UCBtnExt btnNetConfig;

	private TabPage tabPage1;

	private TabControl tabControl1;

	private TabPage tabPage6;

	private TabPage tabPage7;

	private MstSetPortable MainmstSet;

	private Label label24;

	private Label label23;

	private Label label22;

	private UCBtnExt btShowDesktop;

	private UCBtnExt btnFire;

	private Label labSignal;

	public UCDataGridView dGPara1;

	public UCTextBoxEx tbColueAUX;

	public UCTextBoxEx tbColuenDetec;

	public UCTextBoxEx tbColuenTemp;

	public UCSwitch uSwColu;

	public UCSwitch uSwDec;

	public UCSwitch uSwAUX;

	private Label label3;

	private Label label5;

	private Label label8;

	public UCTextBoxEx tbHHSet2;

	public UCTextBoxEx tbColPreSet2;

	public UCTextBoxEx tbAirSet1;

	public UCTextBoxEx tbHHSet1;

	public UCTextBoxEx tbColPreSet1;

	private Label label9;

	private Label label1;

	private TabPage tabPage2;

	private UCDataGridView ucDGHistory;

	private UCDatePickerExt ucDP1;

	private UCCombox ucCBSites;

	private UCDatePickerExt ucDP2;

	private TabControlExt tabControlExt2;

	private TabPage tabPage5;

	private TabPage tabPage8;

	private SplitContainer splitContainer3;

	private UCBtnExt ucBtnStatic;

	private UCDataGridView ucDGStatistics;

	private UCBtnExt ucBtnExport;

	private UCBtnExt ucBtnPrintf;

	private PrintDocument printDocument1;

	private UCBtnExt ucBtnDelete;

	private UCBtnExt ucBtnSet;

	private UCPanelTitle ucPTSys;

	public UCTextBoxEx ucTBshuaijian;

	public UCTextBoxEx ucTBValve2;

	public UCTextBoxEx ucTBValve1;

	public UCTextBoxEx ucTBJump2;

	public UCTextBoxEx ucTBJump1;

	private Label label12;

	private Label label11;

	private UCBtnExt ucBtnOpenSda;

	private Label labBat;

	private UCBtnExt ucBtnClose;

	public UCTextBoxEx ucTBShowSJ;

	private UCBtnExt btnFireOnCheck;

	private UCBtnExt btnFireOnSet;

	public UCTextBoxEx tbFireOn;

	public PictureBox picBoxFire;

	public ImageList imageList2;

	private AsyncTcpServer mainTcpServer => cdlMgr.MainTcpServer;

	private AsyncTcpServer modus0TcpServer => cdlMgr.Modus0TcpServer;

	private AsyncTcpServer modus1TcpServer => cdlMgr.Modus1TcpServer;

	public InsDeviceCtrl insDeviceCtrl => insDeviceCtrl1;

	public ChromDeviceCtrl chrDeviceCtrl => chromDeviceCtrl1;

	public ChromAcqCtrl chrAcqCtrl => chromAcqCtrl3;

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

	MstSet ChromFormInterface.MainmstSet => MainmstSet1;

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

	public FormMainPortableRH()
	{
		LogMgr.Instance.Write2RunLog("Program Loaded Begin FormMain");
		string_0 = "";
		fromMain = this;
		cdlMgr.formMain = this;
		cdlMgr.formMainEx = this;
		InitializeComponent();
		LogMgr.Instance.Write2RunLog("Program Loaded Finish InitializeComponent");
		base.Controls.Remove(splitContainer1);
		ChromFormCtrl chromFormCtrl = new ChromFormCtrl
		{
			ShowManuAndStateBar = false,
			Dock = DockStyle.Fill,
			BackColor = SystemColors.Control,
			ShowOnlineMethod = false
		};
		chromFormCtrl2 = chromFormCtrl;
		TabControl tabControl = (tabCtrl2 = new TabControl());
		TabPage tabPage = NewTablePage("tabPageSingle", Lang.PS("实时谱图"));
		TabPage tabPage2 = NewTablePage("tabPageCaliGnl", Lang.PS("谱图处理"));
		tabControl.Controls.Add(tabPage);
		tabControl.Controls.Add(tabPage2);
		tabControl.Dock = DockStyle.Fill;
		tabControl.Location = new Point(0, 0);
		tabControl.Name = "tabCtrl";
		tabControl.SelectedIndex = 0;
		tabControl.Size = new Size(704, 100);
		tabPage.Controls.Add(splitContainer1);
		tabPage2.Controls.Add(chromFormCtrl);
		chromFormCtrl.ShowOnlineMethod = false;
		chromFormCtrl.mstSetChromForm.ShowOnlineMethod = false;
		chromFormCtrl.mstSetChromForm.ShowOnlineMethod2 = false;
		tabPage2.BackColor = (tabPage.BackColor = SystemColors.Control);
		base.Controls.Add(tabControl);
		tabControl.BringToFront();
		MainmstSet.ShowOnlineMethod = false;
		MainmstSet.ShowOnlineMethod2 = false;
		spcLeftBottom.Panel2Collapsed = true;
		splitContainer2.Panel2MinSize = 100;
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
		shuaijian = frmParam.fShuaijian;
		shuaijian2 = frmParam.fShuaijian2;
		shuaijian3 = frmParam.fShuaijian3;
		chromAcqCtrl1.checkBox5.Checked = false;
		chromAcqCtrl1.checkBox5.Visible = false;
		Class49.InsertIntoTable(Class49.string_9[0], Class49.user_0.u_name, "", Lang.PS("启动系统成功", "Start succeed"), Lang.PS("启动系统完毕", "Start succeed"));
		UIProxy.Instance.SetErrorMsgStaticLabelMenu();
		m_bLoading = false;
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

	public void UpdateBat(uint ibat)
	{
		if (ibat >= 2856)
		{
			labBat.Text = "电池电压：" + ((float)cdlMgr.CurrentTcpServerSocket.iVBat / 100f).ToString("0.0") + " 电量:100%";
		}
		else if (ibat >= 2800)
		{
			labBat.Text = "电池电压：" + ((float)cdlMgr.CurrentTcpServerSocket.iVBat / 100f).ToString("0.0") + " 电量:90%";
		}
		else if (ibat >= 2751)
		{
			labBat.Text = "电池电压：" + ((float)cdlMgr.CurrentTcpServerSocket.iVBat / 100f).ToString("0.0") + " 电量:80%";
		}
		else if (ibat >= 2709)
		{
			labBat.Text = "电池电压：" + ((float)cdlMgr.CurrentTcpServerSocket.iVBat / 100f).ToString("0.0") + " 电量:70%";
		}
		else if (ibat >= 2674)
		{
			labBat.Text = "电池电压：" + ((float)cdlMgr.CurrentTcpServerSocket.iVBat / 100f).ToString("0.0") + " 电量:60%";
		}
		else if (ibat >= 2653)
		{
			labBat.Text = "电池电压：" + ((float)cdlMgr.CurrentTcpServerSocket.iVBat / 100f).ToString("0.0") + " 电量:50%";
		}
		else if (ibat >= 2639)
		{
			labBat.Text = "电池电压：" + ((float)cdlMgr.CurrentTcpServerSocket.iVBat / 100f).ToString("0.0") + " 电量:40%";
		}
		else if (ibat >= 2611)
		{
			labBat.Text = "电池电压：" + ((float)cdlMgr.CurrentTcpServerSocket.iVBat / 100f).ToString("0.0") + " 电量:30%";
		}
		else if (ibat >= 2590)
		{
			labBat.Text = "电池电压：" + ((float)cdlMgr.CurrentTcpServerSocket.iVBat / 100f).ToString("0.0") + " 电量:20%";
		}
		else if (ibat >= 2576)
		{
			labBat.Text = "电池电压：" + ((float)cdlMgr.CurrentTcpServerSocket.iVBat / 100f).ToString("0.0") + " 电量:10%";
		}
		else if (ibat >= 2450)
		{
			labBat.Text = "电池电压：" + ((float)cdlMgr.CurrentTcpServerSocket.iVBat / 100f).ToString("0.0") + " 电量:5%";
		}
		else
		{
			labBat.Text = "电量低请充电";
		}
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
			fileTypeRegister.RegisterFileType(fileTypeRegInfo);
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
			bool flag = false;
			chrAcqCtrl.method_10(chrAcqCtrl.disLg.lgXBeg, chrAcqCtrl.disLg.lgX + val, chrAcqCtrl.disLg.lgYBeg, chrAcqCtrl.disLg.lgY);
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
			SetDrawName("FID谱图");
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
			chrAcqCtrl.tbTime.Text = frmParam.fTabChannel1.ToString("F" + Class49.int_8);
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
			MessageBox.Show(Lang.PS("工作站启用的25001、8000、502或503端口被占用，启动服务失败！", "25001, 8000, 502 Or 503 Port is occupied,Service failed to start!"), Lang.PS("端口被占用", "Port is occupied"), MessageBoxButtons.OK, MessageBoxIcon.Hand);
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
		if (cdlMgr.CurrentChartParaOpera == null)
		{
			return;
		}
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

	private void btnStartAn_BtnClick(object sender, EventArgs e)
	{
		if (btnStartAn.BtnText == "开始分析")
		{
			cdlMgr.formMain.bStart1 = true;
			TcpServerSocket currentTcpServerSocket = cdlMgr.CurrentTcpServerSocket;
			if (currentTcpServerSocket != null)
			{
				btnStartAn.BtnText = "结束分析";
				currentTcpServerSocket.SendCmd(18);
			}
		}
		else
		{
			cdlMgr.formMain.bStart1 = false;
			TcpServerSocket currentTcpServerSocket2 = cdlMgr.CurrentTcpServerSocket;
			if (currentTcpServerSocket2 != null)
			{
				btnStartAn.BtnText = "开始分析";
				currentTcpServerSocket2.SendCmd(19);
			}
		}
	}

	public void UpdateControlTempText(bool bCtrl)
	{
		if (bCtrl)
		{
			btnStartTe.BtnText = Lang.PS("关闭控温", "Stop Temp");
		}
		else
		{
			btnStartTe.BtnText = Lang.PS("开始控温", "Start Temp");
		}
	}

	private void btnStartTe_BtnClick(object sender, EventArgs e)
	{
		TcpServerSocket currentTcpServerSocket = cdlMgr.CurrentTcpServerSocket;
		if (currentTcpServerSocket != null)
		{
			if (btnStartTe.BtnText == Lang.PS("关闭控温", "Stop Temp"))
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

	private void tbTimeCycle1_TextChanged(object sender, EventArgs e)
	{
		if (!m_bLoading)
		{
			float.TryParse(tbTimeCycle1.InputText, out frmParam.fTabChannel1);
			chromAcqCtrl1.tbTime.Text = frmParam.fTabChannel1.ToString();
			chromAcqCtrl1.tbTime_DoubleClick(null, null);
			frmParam.SaveParam();
		}
	}

	private void tbTimesCycle1_TextChanged(object sender, EventArgs e)
	{
		if (!m_bLoading)
		{
			int.TryParse(tbTimesCycle1.InputText, out frmParam.iTimesCycle1);
			frmParam.SaveParam();
		}
	}

	private void btnNetConfig_BtnClick(object sender, EventArgs e)
	{
		NetSetForm netSetForm = new NetSetForm();
		netSetForm.StartPosition = FormStartPosition.CenterScreen;
		netSetForm.Show();
	}

	private void btShowDesktop_BtnClick(object sender, EventArgs e)
	{
		base.WindowState = FormWindowState.Minimized;
	}

	private void btnFire_BtnClick(object sender, EventArgs e)
	{
		FID1Fire();
	}

	private void btnSetTemp_BtnClick(object sender, EventArgs e)
	{
		if (Class49.user_0.ULevel == User.Level.管理员)
		{
			cdlMgr.CurrentTcpServerSocket?.SendCmd(8);
			return;
		}
		MessageBox.Show(Lang.PS("没有设置仪器权限！", "Without permission!"));
	}

	private void tabPage6_Click(object sender, EventArgs e)
	{
	}

	public void upSetTemp(object sender, KeyEventArgs e)
	{
		if (e.KeyCode != Keys.Return)
		{
			return;
		}
		if (Class49.user_0.ULevel == User.Level.管理员)
		{
			TcpServerSocket currentTcpServerSocket = cdlMgr.CurrentTcpServerSocket;
			if (currentTcpServerSocket != null)
			{
				currentTcpServerSocket.SendCmd(8);
				Thread.Sleep(100);
				Application.DoEvents();
				currentTcpServerSocket.SendCmd(106);
				Thread.Sleep(100);
				Application.DoEvents();
				currentTcpServerSocket.SendCmd(0);
			}
		}
		else
		{
			MessageBox.Show(Lang.PS("没有设置仪器权限！", "Without permission!"));
		}
	}

	private void tbColPreSet1_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode == Keys.Return)
		{
			TcpServerSocket currentTcpServerSocket = cdlMgr.CurrentTcpServerSocket;
			if (currentTcpServerSocket != null)
			{
				float num = 0f;
				byte[] array = new byte[2];
				data_len61[25] = 48;
				data_len61[27] = 0;
				num = Class49.String2Float(tbColPreSet1.InputText, 0f);
				array = IBrainConvert.Float2Byte(num, 1);
				data_len61[32] = array[0];
				data_len61[33] = array[1];
				currentTcpServerSocket.SendData(data_len61);
			}
		}
	}

	private void ucEvent_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode != Keys.Return)
		{
			return;
		}
		if (Class49.user_0.ULevel == User.Level.管理员)
		{
			TcpServerSocket currentTcpServerSocket = cdlMgr.CurrentTcpServerSocket;
			if (currentTcpServerSocket != null)
			{
				currentTcpServerSocket.SendCmd(10);
				Thread.Sleep(500);
				Thread.Sleep(500);
				currentTcpServerSocket.SendCmd(101);
			}
		}
		else
		{
			MessageBox.Show(Lang.PS("没有设置仪器权限！", "Without permission!"));
		}
	}

	private void ucShowSJ_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode != Keys.Return)
		{
			return;
		}
		if (Class49.user_0.ULevel == User.Level.管理员)
		{
			if (ucTBShowSJ.InputText == "0632")
			{
				ucTBshuaijian.Visible = true;
			}
			else
			{
				ucTBshuaijian.Visible = false;
			}
		}
		else
		{
			MessageBox.Show(Lang.PS("没有设置仪器权限！", "Without permission!"));
		}
	}

	private void ucTBshuaijian_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode == Keys.Return)
		{
			float num = 1f;
			num = Class49.String2Float(ucTBshuaijian.InputText, 0f);
			frmParam.fShuaijian = num;
			frmParam.SaveParam();
		}
	}

	private void tbHHSet1_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode == Keys.Return)
		{
			TcpServerSocket currentTcpServerSocket = cdlMgr.CurrentTcpServerSocket;
			if (currentTcpServerSocket != null)
			{
				float num = 0f;
				byte[] array = new byte[2];
				data_len61[25] = 49;
				data_len61[26] = 1;
				data_len61[27] = 1;
				num = Class49.String2Float(tbHHSet1.InputText, 0f);
				array = IBrainConvert.Float2Byte(num, 1);
				data_len61[32] = array[0];
				data_len61[33] = array[1];
				currentTcpServerSocket.SendData(data_len61);
			}
		}
	}

	private void tbAirSet1_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode == Keys.Return)
		{
			TcpServerSocket currentTcpServerSocket = cdlMgr.CurrentTcpServerSocket;
			if (currentTcpServerSocket != null)
			{
				float num = 0f;
				byte[] array = new byte[2];
				data_len61[25] = 50;
				data_len61[26] = 2;
				data_len61[27] = 1;
				num = Class49.String2Float(tbAirSet1.InputText, 0f);
				array = IBrainConvert.Float2Byte(num, 1);
				data_len61[32] = array[0];
				data_len61[33] = array[1];
				currentTcpServerSocket.SendData(data_len61);
			}
		}
	}

	private void tbColPreSet2_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode == Keys.Return)
		{
			TcpServerSocket currentTcpServerSocket = cdlMgr.CurrentTcpServerSocket;
			if (currentTcpServerSocket != null)
			{
				float num = 0f;
				byte[] array = new byte[2];
				data_len61[25] = 51;
				data_len61[27] = 0;
				num = Class49.String2Float(tbColPreSet2.InputText, 0f);
				array = IBrainConvert.Float2Byte(num, 1);
				data_len61[32] = array[0];
				data_len61[33] = array[1];
				currentTcpServerSocket.SendData(data_len61);
			}
		}
	}

	private void tbHHSet2_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode == Keys.Return)
		{
			TcpServerSocket currentTcpServerSocket = cdlMgr.CurrentTcpServerSocket;
			if (currentTcpServerSocket != null)
			{
				float num = 0f;
				byte[] array = new byte[2];
				data_len61[25] = 52;
				data_len61[26] = 1;
				data_len61[27] = 1;
				num = Class49.String2Float(tbHHSet2.InputText, 0f);
				array = IBrainConvert.Float2Byte(num, 1);
				data_len61[32] = array[0];
				data_len61[33] = array[1];
				currentTcpServerSocket.SendData(data_len61);
			}
		}
	}

	private void btnEPCSet_BtnClick(object sender, EventArgs e)
	{
		Thread thread = new Thread(sendData);
		thread.IsBackground = true;
		thread.Start();
	}

	public void sendData()
	{
		float num = 0f;
		TcpServerSocket currentTcpServerSocket = cdlMgr.CurrentTcpServerSocket;
		byte[] array = new byte[2];
		currentTcpServerSocket = cdlMgr.CurrentTcpServerSocket;
		if (currentTcpServerSocket != null)
		{
			bool flag = true;
			data_len61[25] = 48;
			data_len61[27] = 0;
			num = Class49.String2Float(tbColPreSet1.InputText, 0f);
			array = IBrainConvert.Float2Byte(num, 1);
			data_len61[32] = array[0];
			data_len61[33] = array[1];
			currentTcpServerSocket.SendData(data_len61);
			Thread.Sleep(1500);
			data_len61[25] = 49;
			data_len61[26] = 1;
			data_len61[27] = 1;
			num = Class49.String2Float(tbHHSet1.InputText, 0f);
			array = IBrainConvert.Float2Byte(num, 1);
			data_len61[32] = array[0];
			data_len61[33] = array[1];
			currentTcpServerSocket.SendData(data_len61);
			Thread.Sleep(1500);
			data_len61[25] = 50;
			data_len61[26] = 2;
			data_len61[27] = 1;
			num = Class49.String2Float(tbAirSet1.InputText, 0f);
			array = IBrainConvert.Float2Byte(num, 1);
			data_len61[32] = array[0];
			data_len61[33] = array[1];
			currentTcpServerSocket.SendData(data_len61);
			Thread.Sleep(1500);
			data_len61[25] = 51;
			data_len61[27] = 0;
			num = Class49.String2Float(tbColPreSet2.InputText, 0f);
			array = IBrainConvert.Float2Byte(num, 1);
			data_len61[32] = array[0];
			data_len61[33] = array[1];
			currentTcpServerSocket.SendData(data_len61);
			Thread.Sleep(1500);
			data_len61[25] = 52;
			data_len61[26] = 1;
			data_len61[27] = 1;
			num = Class49.String2Float(tbHHSet2.InputText, 0f);
			array = IBrainConvert.Float2Byte(num, 1);
			data_len61[32] = array[0];
			data_len61[33] = array[1];
			currentTcpServerSocket.SendData(data_len61);
			Thread.Sleep(4500);
		}
	}

	private void tabControlExt1_TabIndexChanged(object sender, EventArgs e)
	{
	}

	private void tabControlExt1_SelectedIndexChanged(object sender, EventArgs e)
	{
		if (tabControlExt1.SelectedTab.Text == "方法")
		{
			cdlMgr.currentTcpServerMgrSendCmd(0);
			tbColPreSet1.InputText = InsDeviceCtrl.self.tbColPreSet1.Text;
			tbColPreSet2.InputText = InsDeviceCtrl.self.tbColPreSet2.Text;
			tbHHSet1.InputText = InsDeviceCtrl.self.tbHHSet1.Text;
			tbHHSet2.InputText = InsDeviceCtrl.self.tbHHSet2.Text;
			tbAirSet1.InputText = InsDeviceCtrl.self.tbAirSet1.Text;
			Thread.Sleep(100);
			Application.DoEvents();
			cdlMgr.currentTcpServerMgrSendCmd(66);
		}
		else
		{
			if (!(tabControlExt1.SelectedTab.Text == "历史数据"))
			{
				return;
			}
			string textValue = ucCBSites.TextValue;
			string connectionString = "Data Source='" + Application.StartupPath + "\\ngmpolPortable.dll';Version=3;";
			string text = "";
			List<KeyValuePair<string, string>> list = new List<KeyValuePair<string, string>>();
			int num = 0;
			StringBuilder stringBuilder = new StringBuilder();
			using (SQLiteConnection sQLiteConnection = new SQLiteConnection(connectionString))
			{
				sQLiteConnection.Open();
				string commandText = "select name from sqlite_master where type='table' order by name;";
				SQLiteCommand sQLiteCommand = new SQLiteCommand(commandText, sQLiteConnection);
				using SQLiteDataReader sQLiteDataReader = sQLiteCommand.ExecuteReader();
				while (sQLiteDataReader.Read())
				{
					text = sQLiteDataReader["Name"].ToString();
					list.Add(new KeyValuePair<string, string>(num++.ToString(), text));
				}
			}
			ucCBSites.Source = list;
			ucCBSites.TextValue = textValue;
		}
	}

	private void ucCBSites_SelectedChangedEvent(object sender, EventArgs e)
	{
		DataTable dataTableMINE = Class49.GetDataTableMINE(1, ucCBSites.TextValue, ucDP1.CurrentTime, ucDP2.CurrentTime);
		if (dataTableMINE == null)
		{
			ucDGHistory.DataSource = null;
		}
		else
		{
			ucDGHistory.DataSource = dataTableMINE;
		}
	}

	private void ucBtnStatic_BtnClick(object sender, EventArgs e)
	{
		float num = 0f;
		int num2 = 0;
		float num3 = 0f;
		float num4 = 0f;
		float num5 = 0f;
		float num6 = 0f;
		float num7 = 0f;
		float num8 = 0f;
		float[] array = new float[0];
		float[] array2 = new float[0];
		float[] array3 = new float[0];
		float[] array4 = new float[0];
		float[] array5 = new float[0];
		List<IDataGridViewRow> selectRows = ucDGHistory.SelectRows;
		statisSource.Clear();
		for (int i = 0; i < selectRows.Count; i++)
		{
			DataRow dataRow = (DataRow)selectRows[i].DataSource;
			statisticsGridModel statisticsGridModel2 = new statisticsGridModel
			{
				DataTime = dataRow.ItemArray[0].ToString(),
				AmountThc = float.Parse(dataRow.ItemArray[1].ToString()),
				AmountCh4 = float.Parse(dataRow.ItemArray[2].ToString()),
				AmountNmhc = float.Parse(dataRow.ItemArray[3].ToString()),
				RtThc = float.Parse(dataRow.ItemArray[4].ToString()),
				RtCh4 = float.Parse(dataRow.ItemArray[5].ToString()),
				unit = dataRow.ItemArray[10].ToString()
			};
			statisSource.Add(statisticsGridModel2);
			if (i == 0)
			{
				strCollectSJDW = dataRow.ItemArray[6].ToString();
				strCollectSite = dataRow.ItemArray[7].ToString();
				strCollectJYDW = dataRow.ItemArray[8].ToString();
				strCollectP = dataRow.ItemArray[9].ToString();
			}
			num2++;
			Array.Resize(ref array, num2);
			Array.Resize(ref array2, num2);
			Array.Resize(ref array3, num2);
			Array.Resize(ref array4, num2);
			Array.Resize(ref array5, num2);
			array[num2 - 1] = statisticsGridModel2.RtThc;
			array2[num2 - 1] = statisticsGridModel2.RtCh4;
			array3[num2 - 1] = statisticsGridModel2.AmountThc;
			array4[num2 - 1] = statisticsGridModel2.AmountCh4;
			array5[num2 - 1] = statisticsGridModel2.AmountNmhc;
			num4 += statisticsGridModel2.RtThc;
			num5 += statisticsGridModel2.RtCh4;
			num6 += statisticsGridModel2.AmountThc;
			num7 += statisticsGridModel2.AmountCh4;
			num8 += statisticsGridModel2.AmountNmhc;
		}
		num3 = num4 / (float)num2;
		num = (float)Program.RSDCalculate(num3, array, num2);
		statisticsGridModel statisticsGridModel3 = new statisticsGridModel
		{
			DataTime = "RSD",
			RtThc = num
		};
		statisticsGridModel statisticsGridModel4 = new statisticsGridModel
		{
			DataTime = "AVR",
			RtThc = num3
		};
		num3 = num5 / (float)num2;
		num = (float)Program.RSDCalculate(num3, array2, num2);
		statisticsGridModel3.RtCh4 = num;
		statisticsGridModel4.RtCh4 = num3;
		num3 = num6 / (float)num2;
		num = (float)Program.RSDCalculate(num3, array3, num2);
		statisticsGridModel3.AmountThc = num;
		statisticsGridModel4.AmountThc = num3;
		num3 = num7 / (float)num2;
		num = (float)Program.RSDCalculate(num3, array4, num2);
		statisticsGridModel3.AmountCh4 = num;
		statisticsGridModel4.AmountCh4 = num3;
		num3 = num8 / (float)num2;
		num = (float)Program.RSDCalculate(num3, array5, num2);
		if (float.IsNaN(num))
		{
			num = 0f;
		}
		statisticsGridModel3.AmountNmhc = num;
		statisticsGridModel4.AmountNmhc = num3;
		statisSource.Add(statisticsGridModel3);
		statisSource.Add(statisticsGridModel4);
		ucDGStatistics.DataSource = statisSource;
		ucDGStatistics.ReloadSource();
	}

	public void printfHis()
	{
		RichTextBox richTextBox = new RichTextBox();
		richTextBox.Text = "";
		richTextBox.Text += "                  便携式总烃、甲烷";
		richTextBox.Text += "\r\n";
		richTextBox.Text += "                  和非甲烷总烃工况";
		richTextBox.Text += "\r\n";
		richTextBox.Text += "                     数据报表";
		richTextBox.Text += "\r\n";
		richTextBox.Text += "..................................................................\r\n\r\n\r\n";
		string text = "";
		text = text + "被测单位:" + strCollectSJDW + "\r\n";
		text = text + "采样地点:" + strCollectSite + "\r\n";
		text = text + "检测单位:" + strCollectJYDW + "\r\n";
		text = text + "检测员:" + strCollectP + "\r\n\r\n\r\n";
		richTextBox.Text += text;
		text = "     分析结果（ " + statisSource[0].unit + " ）  \r\n\r\n\r\n";
		richTextBox.Text += text;
		richTextBox.Text += "   THC    CH4    NMHC  ";
		richTextBox.Text += "\r\n\r\n\r\n";
		text = "   ";
		for (int i = 0; i < statisSource.Count - 2; i++)
		{
			text += statisSource[i].AmountThc.ToString("F" + Class49.int_8);
			text += "、";
			text += statisSource[i].AmountCh4.ToString("F" + Class49.int_8);
			text += "、";
			text += statisSource[i].AmountNmhc.ToString("F" + Class49.int_8);
			text += "\r\n   ";
		}
		richTextBox.Text += text;
		text = "\r\n";
		text += "RSD";
		text += " ";
		text += statisSource[statisSource.Count - 2].AmountThc.ToString("F" + Class49.int_8);
		text += "、";
		text += statisSource[statisSource.Count - 2].AmountCh4.ToString("F" + Class49.int_8);
		text += "、";
		text += statisSource[statisSource.Count - 2].AmountNmhc.ToString("F" + Class49.int_8);
		text += "\r\n";
		richTextBox.Text += text;
		text = "AVR";
		text += " ";
		text += statisSource[statisSource.Count - 1].AmountThc.ToString("F" + Class49.int_8);
		text += "、";
		text += statisSource[statisSource.Count - 1].AmountCh4.ToString("F" + Class49.int_8);
		text += "、";
		text += statisSource[statisSource.Count - 1].AmountNmhc.ToString("F" + Class49.int_8);
		text += "\r\n\r\n  ";
		text += "\r\n\r\n  ";
		text += "\r\n\r\n  ";
		text += "\r\n\r\n  ";
		text += "\r\n\r\n  ";
		text += "\r\n\r\n  ";
		text += "-----------------------------------------------------------------\r\n\r\n";
		richTextBox.Text += text;
		FormPrintf formPrintf = new FormPrintf();
		formPrintf.rtprtb.Text = richTextBox.Text;
		formPrintf.Show();
	}

	private void ucBtnPrintf_BtnClick(object sender, EventArgs e)
	{
		printfHis();
	}

	private void printDocument1_PrintPage(object sender, PrintPageEventArgs e)
	{
		RichTextBox richTextBox = new RichTextBox();
		Clipboard.Clear();
		Clipboard.SetText("便携式总烃、甲烷");
		richTextBox.Paste();
		Clipboard.Clear();
		Clipboard.SetText("\r\n\r\n");
		richTextBox.Paste();
		Clipboard.Clear();
		Clipboard.SetText("和非甲烷总烃工况");
		richTextBox.Paste();
		Clipboard.Clear();
		Clipboard.SetText("\r\n\r\n");
		richTextBox.Paste();
		Clipboard.Clear();
		Clipboard.SetText("数据报表");
		richTextBox.Paste();
		Clipboard.Clear();
		Clipboard.SetText("\r\n\r\n");
		richTextBox.Paste();
		Clipboard.Clear();
		Clipboard.SetText("..........................\r\n\r\n");
		richTextBox.Paste();
		Clipboard.Clear();
		string text = "";
		text = text + "被测单位:" + lythcParamMgr.strCollectSJDW + "\r\n";
		text = text + "采样地点:" + lythcParamMgr.strCollectSite + "\r\n";
		text = text + "检测单位:" + lythcParamMgr.strCollectJYDW + "\r\n";
		text = text + "检测员:" + lythcParamMgr.strCollectP + "\r\n\r\n\r\n";
		Clipboard.SetText(text);
		richTextBox.Paste();
		Clipboard.Clear();
		text = "     分析结果    \r\n\r\n";
		Clipboard.SetText(text);
		richTextBox.Paste();
		Clipboard.Clear();
		text = "   THC CH4 NMHC  \r\n\r\n";
		Clipboard.SetText(text);
		richTextBox.Paste();
		Clipboard.Clear();
		text = "   ";
		for (int i = 0; i < statisSource.Count - 2; i++)
		{
			text += statisSource[i].AmountThc.ToString("F" + Class49.int_8);
			text += "、";
			text += statisSource[i].AmountCh4.ToString("F" + Class49.int_8);
			text += "、";
			text += statisSource[i].AmountNmhc.ToString("F" + Class49.int_8);
			text += " ";
			text += statisSource[i].unit;
			text += "\r\n\r\n   ";
		}
		Clipboard.SetText(text);
		richTextBox.Paste();
		Clipboard.Clear();
		text = "\r\n";
		text += "RSD";
		text += " ";
		text += statisSource[statisSource.Count - 2].AmountThc.ToString("F" + Class49.int_8);
		text += "、";
		text += statisSource[statisSource.Count - 2].AmountCh4.ToString("F" + Class49.int_8);
		text += "、";
		text += statisSource[statisSource.Count - 2].AmountNmhc.ToString("F" + Class49.int_8);
		text += "\r\n\r\n";
		Clipboard.SetText(text);
		richTextBox.Paste();
		Clipboard.Clear();
		text = "AVR";
		text += " ";
		text += statisSource[statisSource.Count - 1].AmountThc.ToString("F" + Class49.int_8);
		text += "、";
		text += statisSource[statisSource.Count - 1].AmountCh4.ToString("F" + Class49.int_8);
		text += "、";
		text += statisSource[statisSource.Count - 1].AmountNmhc.ToString("F" + Class49.int_8);
		text += "\r\n\r\n";
		Clipboard.SetText(text);
		richTextBox.Paste();
		Clipboard.Clear();
		Graphics graphics = e.Graphics;
		using Font font = new Font("Lucda Console", 20f);
		graphics.DrawString(richTextBox.Text, font, Brushes.Black, 0f, 0f);
	}

	private void ucBtnExport_BtnClick(object sender, EventArgs e)
	{
		SaveFileDialog saveFileDialog = new SaveFileDialog();
		saveFileDialog.FileName = DateTime.Now.ToString("yyyyMMddHHmm") + ".xls";
		saveFileDialog.Filter = " xls files(*.xls)|*.xls|All files(*.*)|*.*";
		saveFileDialog.FilterIndex = 2;
		saveFileDialog.RestoreDirectory = true;
		if (saveFileDialog.ShowDialog() == DialogResult.OK)
		{
			int num = 3;
			FileStream fileStream = new FileStream(Application.StartupPath + "\\VOCs数据" + 3 + ".xls", FileMode.Open, FileAccess.Read);
			HSSFWorkbook hSSFWorkbook = new HSSFWorkbook(fileStream);
			ISheet sheetAt = hSSFWorkbook.GetSheetAt(0);
			sheetAt.ForceFormulaRecalculation = true;
			FileStream fileStream2 = new FileStream(saveFileDialog.FileName, FileMode.Create);
			hSSFWorkbook.Write(fileStream2);
			fileStream.Close();
			fileStream2.Close();
			dataToExcel(saveFileDialog.FileName);
		}
	}

	public bool dataToExcel(string Outpath)
	{
		bool result = false;
		IWorkbook workbook = null;
		FileStream fileStream = null;
		IRow row = null;
		IRow row2 = null;
		ISheet sheet = null;
		ICell cell = null;
		ICell cell2 = null;
		bool flag = false;
		double num = 0.0;
		FileStream fileStream2 = new FileStream(Outpath, FileMode.Open, FileAccess.ReadWrite);
		try
		{
			string[] array = new string[3];
			if (statisSource.Count > 0)
			{
				workbook = new HSSFWorkbook(fileStream2);
				sheet = workbook.GetSheetAt(0);
				int count = statisSource.Count;
				int num2 = 3;
				int num3 = 0;
				int num4 = 1;
				row2 = sheet.GetRow(0);
				cell2 = row2.GetCell(0);
				row2 = sheet.GetRow(1);
				cell2 = row2.GetCell(0);
				cell2.SetCellValue("制表时间:" + DateTime.Now.ToString());
				row = sheet.GetRow(5);
				for (int i = 1; i < num2; i++)
				{
					cell = row.GetCell(i);
					if (cell == null)
					{
						cell = row.CreateCell(i);
					}
				}
				for (int j = 4; j < statisSource.Count + 4; j++)
				{
					if (j > 4)
					{
						IRow row3 = sheet.GetRow(5);
						MyInsertRow(sheet, j + 1, 1, row3);
					}
					row = sheet.GetRow(j);
					if (row == null)
					{
						row = sheet.CreateRow(j);
					}
					for (int k = 0; k < 6; k++)
					{
						cell = row.GetCell(k);
						if (k == 0)
						{
							cell.SetCellValue((j - 3).ToString());
							continue;
						}
						if (cell == null)
						{
							cell = row.CreateCell(k);
						}
						switch (k)
						{
						case 1:
							cell.SetCellValue(statisSource[j - 4].DataTime.ToString());
							break;
						case 2:
							cell.SetCellValue(statisSource[j - 4].AmountThc.ToString("F" + Class49.int_8));
							break;
						case 3:
							cell.SetCellValue(statisSource[j - 4].AmountCh4.ToString("F" + Class49.int_8));
							break;
						case 4:
							cell.SetCellValue(statisSource[j - 4].AmountNmhc.ToString("F" + Class49.int_8));
							break;
						case 5:
							if (j < statisSource.Count + 2)
							{
								cell.SetCellValue(statisSource[j - 4].unit.ToString());
							}
							break;
						}
					}
				}
				using (fileStream = File.OpenWrite(Outpath))
				{
					workbook.Write(fileStream);
					result = true;
				}
				fileStream2.Close();
			}
			return result;
		}
		catch (Exception)
		{
			using (fileStream = File.OpenWrite(Outpath))
			{
				workbook.Write(fileStream);
				result = true;
			}
			fileStream.Close();
			return false;
		}
	}

	public static void MyInsertRow(ISheet sheet, int 插入行, int 插入行总数, IRow 源格式行)
	{
		sheet.ShiftRows(插入行, sheet.LastRowNum, 插入行总数, copyRowHeight: true, resetOriginalRowHeight: false);
		for (int i = 插入行; i < 插入行 + 插入行总数 - 1; i++)
		{
			IRow row = null;
			ICell cell = null;
			ICell cell2 = null;
			row = sheet.CreateRow(i + 1);
			for (int j = 源格式行.FirstCellNum; j < 源格式行.LastCellNum; j++)
			{
				cell = 源格式行.GetCell(j);
				if (cell != null)
				{
					cell2 = row.CreateCell(j);
					cell2.CellStyle = cell.CellStyle;
					cell2.SetCellType(cell.CellType);
				}
			}
		}
		IRow row2 = sheet.GetRow(插入行);
		ICell cell3 = null;
		ICell cell4 = null;
		for (int k = 源格式行.FirstCellNum; k < 源格式行.LastCellNum; k++)
		{
			cell3 = 源格式行.GetCell(k);
			if (cell3 != null)
			{
				cell4 = row2.CreateCell(k);
				cell4.CellStyle = cell3.CellStyle;
				cell4.SetCellType(cell3.CellType);
			}
		}
	}

	private void ucBtnDelete_BtnClick(object sender, EventArgs e)
	{
		DialogResult dialogResult = MessageBox.Show("确认删除采样点: " + ucCBSites.TextValue + " 的所有数据？", "删除数据", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
		if (dialogResult == DialogResult.OK)
		{
			Class49.DeleteDataTable(0, ucCBSites.TextValue);
			dataRolad();
		}
	}

	public void dataRolad()
	{
		string connectionString = "Data Source='" + Application.StartupPath + "\\ngmpolPortable.dll';Version=3;";
		string text = "";
		List<KeyValuePair<string, string>> list = new List<KeyValuePair<string, string>>();
		int num = 0;
		StringBuilder stringBuilder = new StringBuilder();
		using (SQLiteConnection sQLiteConnection = new SQLiteConnection(connectionString))
		{
			sQLiteConnection.Open();
			string commandText = "select name from sqlite_master where type='table' order by name;";
			SQLiteCommand sQLiteCommand = new SQLiteCommand(commandText, sQLiteConnection);
			using SQLiteDataReader sQLiteDataReader = sQLiteCommand.ExecuteReader();
			while (sQLiteDataReader.Read())
			{
				text = sQLiteDataReader["Name"].ToString();
				list.Add(new KeyValuePair<string, string>(num++.ToString(), text));
			}
		}
		ucCBSites.Source = list;
	}

	private void ucBtnSet_BtnClick(object sender, EventArgs e)
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

	private void ucBtnOpenSda_BtnClick(object sender, EventArgs e)
	{
		List<IDataGridViewRow> selectRows = ucDGHistory.SelectRows;
		if (selectRows.Count > 0)
		{
			if (ChromForm.form == null)
			{
				ChromForm.form = new ChromForm();
			}
			ChromForm.form.Show();
			ChromForm.form.chromDataGrid.setOverlayMode();
		}
		else
		{
			MessageBox.Show(Lang.PS("请先选中数据！", "Please select the data first!"), Lang.PS("提示", "Tips"), MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation);
		}
		for (int i = 0; i < selectRows.Count; i++)
		{
			DataRow dataRow = (DataRow)selectRows[i].DataSource;
			ChromForm.form.OpenChrom(dataRow.ItemArray[11].ToString(), sampling: true, useCurrent: true);
		}
	}

	private void ucBtnClose_BtnClick(object sender, EventArgs e)
	{
		Close();
	}

	private void uSwAUX_Click(object sender, EventArgs e)
	{
		DtrTempNameEnableSt();
	}

	private void uSwDec_Click(object sender, EventArgs e)
	{
		DtrTempNameEnableSt();
	}

	private void uSwColu_Click(object sender, EventArgs e)
	{
		DtrTempNameEnableSt();
	}

	private void btnFireOnCheck_BtnClick(object sender, EventArgs e)
	{
		if (Class49.user_0.ULevel == User.Level.管理员)
		{
			cdlMgr.currentTcpServerMgrSendCmd(250);
		}
	}

	private void btnFireOnSet_BtnClick(object sender, EventArgs e)
	{
		if (Class49.user_0.ULevel == User.Level.管理员)
		{
			cdlMgr.currentTcpServerMgrSendCmd(249);
			frmParam.fFireOn = ToFloat(tbFireOn.InputText);
			frmParam.fFireOn2 = 0f;
			frmParam.SaveParam();
		}
	}

	private float ToFloat(string str)
	{
		float result = 0f;
		float.TryParse(str, out result);
		return result;
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(IBrainChrom2018.FormMainPortable));
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
		this.splitContainer2 = new System.Windows.Forms.SplitContainer();
		this.tabChannel = new System.Windows.Forms.TabControl();
		this.tabPage11 = new System.Windows.Forms.TabPage();
		this.tabPage12 = new System.Windows.Forms.TabPage();
		this.tabPage13 = new System.Windows.Forms.TabPage();
		this.tabPage14 = new System.Windows.Forms.TabPage();
		this.spcLeftBottom = new System.Windows.Forms.SplitContainer();
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
		this.labTime = new System.Windows.Forms.Label();
		this.label6 = new System.Windows.Forms.Label();
		this.labCH4 = new System.Windows.Forms.Label();
		this.label4 = new System.Windows.Forms.Label();
		this.labTHC = new System.Windows.Forms.Label();
		this.label2 = new System.Windows.Forms.Label();
		this.labNMHC = new System.Windows.Forms.Label();
		this.labNMHCT = new System.Windows.Forms.Label();
		this.toolStrip1 = new System.Windows.Forms.ToolStrip();
		this.tabControlExt1 = new HZH_Controls.Controls.TabControlExt();
		this.tabPage3 = new System.Windows.Forms.TabPage();
		this.ucBtnClose = new HZH_Controls.Controls.UCBtnExt();
		this.dGPara1 = new HZH_Controls.Controls.UCDataGridView();
		this.labSignal = new System.Windows.Forms.Label();
		this.btnFire = new HZH_Controls.Controls.UCBtnExt();
		this.btShowDesktop = new HZH_Controls.Controls.UCBtnExt();
		this.分析次数 = new System.Windows.Forms.Label();
		this.tbTimesCycle1 = new HZH_Controls.Controls.UCTextBoxEx();
		this.label7 = new System.Windows.Forms.Label();
		this.tbTimeCycle1 = new HZH_Controls.Controls.UCTextBoxEx();
		this.chromDeviceCtrl1 = new IBrainChrom2018.ChromDeviceCtrl();
		this.btnStartTe = new HZH_Controls.Controls.UCBtnExt();
		this.btnStartAn = new HZH_Controls.Controls.UCBtnExt();
		this.chromAcqCtrl1 = new IBrainChrom2018.ChromAcqCtrlPortable();
		this.tabPage4 = new System.Windows.Forms.TabPage();
		this.tabControl1 = new System.Windows.Forms.TabControl();
		this.tabPage6 = new System.Windows.Forms.TabPage();
		this.tbFireOn = new HZH_Controls.Controls.UCTextBoxEx();
		this.btnFireOnSet = new HZH_Controls.Controls.UCBtnExt();
		this.btnFireOnCheck = new HZH_Controls.Controls.UCBtnExt();
		this.label12 = new System.Windows.Forms.Label();
		this.label11 = new System.Windows.Forms.Label();
		this.ucTBValve2 = new HZH_Controls.Controls.UCTextBoxEx();
		this.ucTBValve1 = new HZH_Controls.Controls.UCTextBoxEx();
		this.ucTBJump2 = new HZH_Controls.Controls.UCTextBoxEx();
		this.ucTBJump1 = new HZH_Controls.Controls.UCTextBoxEx();
		this.ucPTSys = new HZH_Controls.Controls.UCPanelTitle();
		this.ucTBShowSJ = new HZH_Controls.Controls.UCTextBoxEx();
		this.btnNetConfig = new HZH_Controls.Controls.UCBtnExt();
		this.ucTBshuaijian = new HZH_Controls.Controls.UCTextBoxEx();
		this.ucBtnSet = new HZH_Controls.Controls.UCBtnExt();
		this.label9 = new System.Windows.Forms.Label();
		this.label1 = new System.Windows.Forms.Label();
		this.label3 = new System.Windows.Forms.Label();
		this.label5 = new System.Windows.Forms.Label();
		this.label8 = new System.Windows.Forms.Label();
		this.tbHHSet2 = new HZH_Controls.Controls.UCTextBoxEx();
		this.tbColPreSet2 = new HZH_Controls.Controls.UCTextBoxEx();
		this.tbAirSet1 = new HZH_Controls.Controls.UCTextBoxEx();
		this.tbHHSet1 = new HZH_Controls.Controls.UCTextBoxEx();
		this.tbColPreSet1 = new HZH_Controls.Controls.UCTextBoxEx();
		this.uSwDec = new HZH_Controls.Controls.UCSwitch();
		this.uSwAUX = new HZH_Controls.Controls.UCSwitch();
		this.uSwColu = new HZH_Controls.Controls.UCSwitch();
		this.tbColueAUX = new HZH_Controls.Controls.UCTextBoxEx();
		this.tbColuenDetec = new HZH_Controls.Controls.UCTextBoxEx();
		this.tbColuenTemp = new HZH_Controls.Controls.UCTextBoxEx();
		this.label24 = new System.Windows.Forms.Label();
		this.label23 = new System.Windows.Forms.Label();
		this.label22 = new System.Windows.Forms.Label();
		this.tabPage7 = new System.Windows.Forms.TabPage();
		this.MainmstSet = new IBrainChrom2018.MstSetPortable();
		this.tabPage2 = new System.Windows.Forms.TabPage();
		this.splitContainer3 = new System.Windows.Forms.SplitContainer();
		this.ucBtnOpenSda = new HZH_Controls.Controls.UCBtnExt();
		this.ucBtnDelete = new HZH_Controls.Controls.UCBtnExt();
		this.ucBtnExport = new HZH_Controls.Controls.UCBtnExt();
		this.ucBtnPrintf = new HZH_Controls.Controls.UCBtnExt();
		this.ucBtnStatic = new HZH_Controls.Controls.UCBtnExt();
		this.ucDP1 = new HZH_Controls.Controls.UCDatePickerExt();
		this.ucCBSites = new HZH_Controls.Controls.UCCombox();
		this.ucDP2 = new HZH_Controls.Controls.UCDatePickerExt();
		this.tabControlExt2 = new HZH_Controls.Controls.TabControlExt();
		this.tabPage5 = new System.Windows.Forms.TabPage();
		this.ucDGHistory = new HZH_Controls.Controls.UCDataGridView();
		this.tabPage8 = new System.Windows.Forms.TabPage();
		this.ucDGStatistics = new HZH_Controls.Controls.UCDataGridView();
		this.tabPage1 = new System.Windows.Forms.TabPage();
		this.insDeviceCtrl1 = new IBrainChrom2018.InsDeviceCtrl();
		this.labBat = new System.Windows.Forms.Label();
		this.printDocument1 = new System.Drawing.Printing.PrintDocument();
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
		this.picBoxFire = new System.Windows.Forms.PictureBox();
		this.imageList2 = new System.Windows.Forms.ImageList(this.components);
		((System.ComponentModel.ISupportInitialize)this.splitContainer1).BeginInit();
		this.splitContainer1.Panel1.SuspendLayout();
		this.splitContainer1.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.splitContainer2).BeginInit();
		this.splitContainer2.Panel1.SuspendLayout();
		this.splitContainer2.Panel2.SuspendLayout();
		this.splitContainer2.SuspendLayout();
		this.tabChannel.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.spcLeftBottom).BeginInit();
		this.spcLeftBottom.SuspendLayout();
		this.cmPeakInfo.SuspendLayout();
		this.cmsIntegration.SuspendLayout();
		this.menuStrip1.SuspendLayout();
		this.statusStrip1.SuspendLayout();
		this.tabControlExt1.SuspendLayout();
		this.tabPage3.SuspendLayout();
		this.tabPage4.SuspendLayout();
		this.tabControl1.SuspendLayout();
		this.tabPage6.SuspendLayout();
		this.ucPTSys.SuspendLayout();
		this.tabPage7.SuspendLayout();
		this.tabPage2.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.splitContainer3).BeginInit();
		this.splitContainer3.Panel1.SuspendLayout();
		this.splitContainer3.Panel2.SuspendLayout();
		this.splitContainer3.SuspendLayout();
		this.tabControlExt2.SuspendLayout();
		this.tabPage5.SuspendLayout();
		this.tabPage8.SuspendLayout();
		this.tabPage1.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.picBoxFire).BeginInit();
		base.SuspendLayout();
		this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
		this.splitContainer1.Location = new System.Drawing.Point(0, 213);
		this.splitContainer1.Name = "splitContainer1";
		this.splitContainer1.Panel1.Controls.Add(this.splitContainer2);
		this.splitContainer1.Size = new System.Drawing.Size(644, 375);
		this.splitContainer1.SplitterDistance = 172;
		this.splitContainer1.SplitterWidth = 5;
		this.splitContainer1.TabIndex = 0;
		this.splitContainer1.Visible = false;
		this.splitContainer2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.splitContainer2.Location = new System.Drawing.Point(148, 107);
		this.splitContainer2.Name = "splitContainer2";
		this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
		this.splitContainer2.Panel1.Controls.Add(this.tabChannel);
		this.splitContainer2.Panel2.Controls.Add(this.spcLeftBottom);
		this.splitContainer2.Size = new System.Drawing.Size(351, 260);
		this.splitContainer2.SplitterDistance = 106;
		this.splitContainer2.TabIndex = 0;
		this.tabChannel.Controls.Add(this.tabPage11);
		this.tabChannel.Controls.Add(this.tabPage12);
		this.tabChannel.Controls.Add(this.tabPage13);
		this.tabChannel.Controls.Add(this.tabPage14);
		this.tabChannel.Location = new System.Drawing.Point(508, 121);
		this.tabChannel.Name = "tabChannel";
		this.tabChannel.SelectedIndex = 0;
		this.tabChannel.Size = new System.Drawing.Size(371, 22);
		this.tabChannel.TabIndex = 0;
		this.tabChannel.SelectedIndexChanged += new System.EventHandler(tabChannel_SelectedIndexChanged);
		this.tabPage11.BackColor = System.Drawing.Color.FromArgb(255, 192, 192);
		this.tabPage11.Location = new System.Drawing.Point(4, 22);
		this.tabPage11.Name = "tabPage11";
		this.tabPage11.Padding = new System.Windows.Forms.Padding(3);
		this.tabPage11.Size = new System.Drawing.Size(363, 0);
		this.tabPage11.TabIndex = 0;
		this.tabPage11.Text = "通道A";
		this.tabPage11.UseVisualStyleBackColor = true;
		this.tabPage12.BackColor = System.Drawing.Color.FromArgb(255, 224, 192);
		this.tabPage12.Location = new System.Drawing.Point(4, 22);
		this.tabPage12.Name = "tabPage12";
		this.tabPage12.Padding = new System.Windows.Forms.Padding(3);
		this.tabPage12.Size = new System.Drawing.Size(363, 0);
		this.tabPage12.TabIndex = 1;
		this.tabPage12.Text = "通道B";
		this.tabPage12.UseVisualStyleBackColor = true;
		this.tabPage13.BackColor = System.Drawing.Color.FromArgb(255, 255, 192);
		this.tabPage13.Location = new System.Drawing.Point(4, 22);
		this.tabPage13.Name = "tabPage13";
		this.tabPage13.Padding = new System.Windows.Forms.Padding(3);
		this.tabPage13.Size = new System.Drawing.Size(363, 0);
		this.tabPage13.TabIndex = 2;
		this.tabPage13.Text = "通道C";
		this.tabPage13.UseVisualStyleBackColor = true;
		this.tabPage14.BackColor = System.Drawing.Color.FromArgb(192, 255, 192);
		this.tabPage14.Location = new System.Drawing.Point(4, 22);
		this.tabPage14.Name = "tabPage14";
		this.tabPage14.Padding = new System.Windows.Forms.Padding(3);
		this.tabPage14.Size = new System.Drawing.Size(363, 0);
		this.tabPage14.TabIndex = 3;
		this.tabPage14.Text = "通道D";
		this.tabPage14.UseVisualStyleBackColor = true;
		this.spcLeftBottom.Dock = System.Windows.Forms.DockStyle.Fill;
		this.spcLeftBottom.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
		this.spcLeftBottom.Location = new System.Drawing.Point(0, 0);
		this.spcLeftBottom.Name = "spcLeftBottom";
		this.spcLeftBottom.Panel1MinSize = 0;
		this.spcLeftBottom.Panel2MinSize = 0;
		this.spcLeftBottom.Size = new System.Drawing.Size(349, 148);
		this.spcLeftBottom.SplitterDistance = 300;
		this.spcLeftBottom.SplitterWidth = 1;
		this.spcLeftBottom.TabIndex = 1;
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
		this.menuStrip1.Size = new System.Drawing.Size(800, 25);
		this.menuStrip1.TabIndex = 1;
		this.menuStrip1.Text = "menuStrip1";
		this.menuStrip1.Visible = false;
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
		this.statusStrip1.Location = new System.Drawing.Point(0, 654);
		this.statusStrip1.Name = "statusStrip1";
		this.statusStrip1.Padding = new System.Windows.Forms.Padding(1, 0, 15, 0);
		this.statusStrip1.Size = new System.Drawing.Size(800, 22);
		this.statusStrip1.TabIndex = 2;
		this.statusStrip1.Text = "statusStrip1";
		this.statusStrip1.Visible = false;
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
		this.labTime.AutoSize = true;
		this.labTime.Font = new System.Drawing.Font("宋体", 10.5f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		this.labTime.Location = new System.Drawing.Point(626, 62);
		this.labTime.Name = "labTime";
		this.labTime.Size = new System.Drawing.Size(14, 14);
		this.labTime.TabIndex = 15;
		this.labTime.Text = "0";
		this.label6.AutoSize = true;
		this.label6.Font = new System.Drawing.Font("宋体", 10.5f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		this.label6.Location = new System.Drawing.Point(577, 32);
		this.label6.Name = "label6";
		this.label6.Size = new System.Drawing.Size(63, 14);
		this.label6.TabIndex = 14;
		this.label6.Text = "采集时间";
		this.labCH4.AutoSize = true;
		this.labCH4.Font = new System.Drawing.Font("微软雅黑", 15f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 134);
		this.labCH4.ForeColor = System.Drawing.Color.OrangeRed;
		this.labCH4.Location = new System.Drawing.Point(139, 73);
		this.labCH4.Name = "labCH4";
		this.labCH4.Size = new System.Drawing.Size(24, 27);
		this.labCH4.TabIndex = 13;
		this.labCH4.Text = "0";
		this.label4.AutoSize = true;
		this.label4.Font = new System.Drawing.Font("微软雅黑", 15f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 134);
		this.label4.ForeColor = System.Drawing.Color.OrangeRed;
		this.label4.Location = new System.Drawing.Point(68, 73);
		this.label4.Name = "label4";
		this.label4.Size = new System.Drawing.Size(52, 27);
		this.label4.TabIndex = 12;
		this.label4.Text = "甲烷";
		this.labTHC.AutoSize = true;
		this.labTHC.Font = new System.Drawing.Font("微软雅黑", 15f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 134);
		this.labTHC.ForeColor = System.Drawing.Color.OrangeRed;
		this.labTHC.Location = new System.Drawing.Point(139, 32);
		this.labTHC.Name = "labTHC";
		this.labTHC.Size = new System.Drawing.Size(24, 27);
		this.labTHC.TabIndex = 11;
		this.labTHC.Text = "0";
		this.label2.AutoSize = true;
		this.label2.Font = new System.Drawing.Font("微软雅黑", 15f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 134);
		this.label2.ForeColor = System.Drawing.Color.OrangeRed;
		this.label2.Location = new System.Drawing.Point(68, 32);
		this.label2.Name = "label2";
		this.label2.Size = new System.Drawing.Size(52, 27);
		this.label2.TabIndex = 10;
		this.label2.Text = "总烃";
		this.labNMHC.AutoSize = true;
		this.labNMHC.Font = new System.Drawing.Font("微软雅黑", 15f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 134);
		this.labNMHC.ForeColor = System.Drawing.Color.OrangeRed;
		this.labNMHC.Location = new System.Drawing.Point(139, 116);
		this.labNMHC.Name = "labNMHC";
		this.labNMHC.Size = new System.Drawing.Size(24, 27);
		this.labNMHC.TabIndex = 9;
		this.labNMHC.Text = "0";
		this.labNMHCT.AutoSize = true;
		this.labNMHCT.Font = new System.Drawing.Font("微软雅黑", 15f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 134);
		this.labNMHCT.ForeColor = System.Drawing.Color.OrangeRed;
		this.labNMHCT.Location = new System.Drawing.Point(8, 116);
		this.labNMHCT.Name = "labNMHCT";
		this.labNMHCT.Size = new System.Drawing.Size(112, 27);
		this.labNMHCT.TabIndex = 8;
		this.labNMHCT.Text = "非甲烷总烃";
		this.toolStrip1.BackColor = System.Drawing.Color.OrangeRed;
		this.toolStrip1.Location = new System.Drawing.Point(0, 0);
		this.toolStrip1.Name = "toolStrip1";
		this.toolStrip1.Size = new System.Drawing.Size(800, 25);
		this.toolStrip1.TabIndex = 6;
		this.toolStrip1.Text = "toolStrip1";
		this.tabControlExt1.Alignment = System.Windows.Forms.TabAlignment.Bottom;
		this.tabControlExt1.CloseBtnColor = System.Drawing.Color.OrangeRed;
		this.tabControlExt1.Controls.Add(this.tabPage3);
		this.tabControlExt1.Controls.Add(this.tabPage4);
		this.tabControlExt1.Controls.Add(this.tabPage2);
		this.tabControlExt1.Controls.Add(this.tabPage1);
		this.tabControlExt1.Dock = System.Windows.Forms.DockStyle.Fill;
		this.tabControlExt1.IsShowCloseBtn = false;
		this.tabControlExt1.ItemSize = new System.Drawing.Size(0, 50);
		this.tabControlExt1.Location = new System.Drawing.Point(0, 25);
		this.tabControlExt1.Name = "tabControlExt1";
		this.tabControlExt1.SelectedIndex = 0;
		this.tabControlExt1.Size = new System.Drawing.Size(800, 575);
		this.tabControlExt1.TabIndex = 1;
		this.tabControlExt1.UncloseTabIndexs = null;
		this.tabControlExt1.SelectedIndexChanged += new System.EventHandler(tabControlExt1_SelectedIndexChanged);
		this.tabControlExt1.TabIndexChanged += new System.EventHandler(tabControlExt1_TabIndexChanged);
		this.tabPage3.Controls.Add(this.picBoxFire);
		this.tabPage3.Controls.Add(this.ucBtnClose);
		this.tabPage3.Controls.Add(this.dGPara1);
		this.tabPage3.Controls.Add(this.labSignal);
		this.tabPage3.Controls.Add(this.btnFire);
		this.tabPage3.Controls.Add(this.btShowDesktop);
		this.tabPage3.Controls.Add(this.分析次数);
		this.tabPage3.Controls.Add(this.tbTimesCycle1);
		this.tabPage3.Controls.Add(this.label7);
		this.tabPage3.Controls.Add(this.tbTimeCycle1);
		this.tabPage3.Controls.Add(this.chromDeviceCtrl1);
		this.tabPage3.Controls.Add(this.btnStartTe);
		this.tabPage3.Controls.Add(this.btnStartAn);
		this.tabPage3.Controls.Add(this.chromAcqCtrl1);
		this.tabPage3.Controls.Add(this.labNMHCT);
		this.tabPage3.Controls.Add(this.labTime);
		this.tabPage3.Controls.Add(this.label6);
		this.tabPage3.Controls.Add(this.labNMHC);
		this.tabPage3.Controls.Add(this.label4);
		this.tabPage3.Controls.Add(this.label2);
		this.tabPage3.Controls.Add(this.labCH4);
		this.tabPage3.Controls.Add(this.labTHC);
		this.tabPage3.Location = new System.Drawing.Point(4, 4);
		this.tabPage3.Name = "tabPage3";
		this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
		this.tabPage3.Size = new System.Drawing.Size(792, 517);
		this.tabPage3.TabIndex = 0;
		this.tabPage3.Text = "主页";
		this.tabPage3.UseVisualStyleBackColor = true;
		this.ucBtnClose.BackColor = System.Drawing.Color.White;
		this.ucBtnClose.BtnBackColor = System.Drawing.Color.White;
		this.ucBtnClose.BtnFont = new System.Drawing.Font("微软雅黑", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		this.ucBtnClose.BtnForeColor = System.Drawing.Color.White;
		this.ucBtnClose.BtnText = "退出";
		this.ucBtnClose.ConerRadius = 20;
		this.ucBtnClose.Cursor = System.Windows.Forms.Cursors.Hand;
		this.ucBtnClose.EnabledMouseEffect = true;
		this.ucBtnClose.FillColor = System.Drawing.Color.OrangeRed;
		this.ucBtnClose.Font = new System.Drawing.Font("微软雅黑", 15f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
		this.ucBtnClose.IsRadius = true;
		this.ucBtnClose.IsShowRect = true;
		this.ucBtnClose.IsShowTips = false;
		this.ucBtnClose.Location = new System.Drawing.Point(457, 207);
		this.ucBtnClose.Margin = new System.Windows.Forms.Padding(0);
		this.ucBtnClose.Name = "ucBtnClose";
		this.ucBtnClose.RectColor = System.Drawing.Color.FromArgb(255, 77, 58);
		this.ucBtnClose.RectWidth = 1;
		this.ucBtnClose.Size = new System.Drawing.Size(115, 31);
		this.ucBtnClose.TabIndex = 32;
		this.ucBtnClose.TabStop = false;
		this.ucBtnClose.TipsColor = System.Drawing.Color.FromArgb(232, 30, 99);
		this.ucBtnClose.TipsText = "";
		this.ucBtnClose.BtnClick += new System.EventHandler(ucBtnClose_BtnClick);
		this.dGPara1.BackColor = System.Drawing.Color.White;
		this.dGPara1.Columns = null;
		this.dGPara1.DataSource = null;
		this.dGPara1.Font = new System.Drawing.Font("宋体", 6.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		this.dGPara1.HeadFont = new System.Drawing.Font("微软雅黑", 10.5f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		this.dGPara1.HeadHeight = 30;
		this.dGPara1.HeadPadingLeft = 0;
		this.dGPara1.HeadTextColor = System.Drawing.Color.Black;
		this.dGPara1.IsShowCheckBox = false;
		this.dGPara1.IsShowHead = true;
		this.dGPara1.Location = new System.Drawing.Point(421, 255);
		this.dGPara1.Name = "dGPara1";
		this.dGPara1.Padding = new System.Windows.Forms.Padding(0, 30, 0, 0);
		this.dGPara1.RowHeight = 20;
		this.dGPara1.RowType = typeof(HZH_Controls.Controls.UCDataGridViewRow);
		this.dGPara1.Size = new System.Drawing.Size(368, 256);
		this.dGPara1.TabIndex = 31;
		this.labSignal.AutoSize = true;
		this.labSignal.Font = new System.Drawing.Font("微软雅黑", 15f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 134);
		this.labSignal.ForeColor = System.Drawing.Color.OrangeRed;
		this.labSignal.Location = new System.Drawing.Point(6, 265);
		this.labSignal.Name = "labSignal";
		this.labSignal.Size = new System.Drawing.Size(58, 27);
		this.labSignal.TabIndex = 30;
		this.labSignal.Text = "信号:";
		this.btnFire.BackColor = System.Drawing.Color.White;
		this.btnFire.BtnBackColor = System.Drawing.Color.White;
		this.btnFire.BtnFont = new System.Drawing.Font("微软雅黑", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		this.btnFire.BtnForeColor = System.Drawing.Color.White;
		this.btnFire.BtnText = "点火";
		this.btnFire.ConerRadius = 20;
		this.btnFire.Cursor = System.Windows.Forms.Cursors.Hand;
		this.btnFire.EnabledMouseEffect = true;
		this.btnFire.FillColor = System.Drawing.Color.OrangeRed;
		this.btnFire.Font = new System.Drawing.Font("微软雅黑", 15f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
		this.btnFire.IsRadius = true;
		this.btnFire.IsShowRect = true;
		this.btnFire.IsShowTips = false;
		this.btnFire.Location = new System.Drawing.Point(457, 122);
		this.btnFire.Margin = new System.Windows.Forms.Padding(0);
		this.btnFire.Name = "btnFire";
		this.btnFire.RectColor = System.Drawing.Color.FromArgb(255, 77, 58);
		this.btnFire.RectWidth = 1;
		this.btnFire.Size = new System.Drawing.Size(115, 31);
		this.btnFire.TabIndex = 29;
		this.btnFire.TabStop = false;
		this.btnFire.TipsColor = System.Drawing.Color.FromArgb(232, 30, 99);
		this.btnFire.TipsText = "";
		this.btnFire.BtnClick += new System.EventHandler(btnFire_BtnClick);
		this.btShowDesktop.BackColor = System.Drawing.Color.White;
		this.btShowDesktop.BtnBackColor = System.Drawing.Color.White;
		this.btShowDesktop.BtnFont = new System.Drawing.Font("微软雅黑", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		this.btShowDesktop.BtnForeColor = System.Drawing.Color.White;
		this.btShowDesktop.BtnText = "显示桌面";
		this.btShowDesktop.ConerRadius = 20;
		this.btShowDesktop.Cursor = System.Windows.Forms.Cursors.Hand;
		this.btShowDesktop.EnabledMouseEffect = true;
		this.btShowDesktop.FillColor = System.Drawing.Color.OrangeRed;
		this.btShowDesktop.Font = new System.Drawing.Font("微软雅黑", 15f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
		this.btShowDesktop.IsRadius = true;
		this.btShowDesktop.IsShowRect = true;
		this.btShowDesktop.IsShowTips = false;
		this.btShowDesktop.Location = new System.Drawing.Point(457, 167);
		this.btShowDesktop.Margin = new System.Windows.Forms.Padding(0);
		this.btShowDesktop.Name = "btShowDesktop";
		this.btShowDesktop.RectColor = System.Drawing.Color.FromArgb(255, 77, 58);
		this.btShowDesktop.RectWidth = 1;
		this.btShowDesktop.Size = new System.Drawing.Size(115, 31);
		this.btShowDesktop.TabIndex = 28;
		this.btShowDesktop.TabStop = false;
		this.btShowDesktop.TipsColor = System.Drawing.Color.FromArgb(232, 30, 99);
		this.btShowDesktop.TipsText = "";
		this.btShowDesktop.BtnClick += new System.EventHandler(btShowDesktop_BtnClick);
		this.分析次数.AutoSize = true;
		this.分析次数.Location = new System.Drawing.Point(581, 144);
		this.分析次数.Name = "分析次数";
		this.分析次数.Size = new System.Drawing.Size(53, 12);
		this.分析次数.TabIndex = 26;
		this.分析次数.Text = "分析次数";
		this.tbTimesCycle1.BackColor = System.Drawing.Color.Transparent;
		this.tbTimesCycle1.ConerRadius = 5;
		this.tbTimesCycle1.Cursor = System.Windows.Forms.Cursors.IBeam;
		this.tbTimesCycle1.DecLength = 2;
		this.tbTimesCycle1.FillColor = System.Drawing.Color.Empty;
		this.tbTimesCycle1.FocusBorderColor = System.Drawing.Color.FromArgb(255, 77, 59);
		this.tbTimesCycle1.Font = new System.Drawing.Font("微软雅黑", 18f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
		this.tbTimesCycle1.InputText = "";
		this.tbTimesCycle1.InputType = HZH_Controls.TextInputType.NotControl;
		this.tbTimesCycle1.IsFocusColor = true;
		this.tbTimesCycle1.IsRadius = true;
		this.tbTimesCycle1.IsShowClearBtn = true;
		this.tbTimesCycle1.IsShowKeyboard = false;
		this.tbTimesCycle1.IsShowRect = true;
		this.tbTimesCycle1.IsShowSearchBtn = false;
		this.tbTimesCycle1.KeyBoardType = HZH_Controls.Controls.KeyBoardType.UCKeyBorderNum;
		this.tbTimesCycle1.Location = new System.Drawing.Point(650, 132);
		this.tbTimesCycle1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
		this.tbTimesCycle1.MaxValue = new decimal(new int[4] { 1000000, 0, 0, 0 });
		this.tbTimesCycle1.MinValue = new decimal(new int[4] { 1000000, 0, 0, -2147483648 });
		this.tbTimesCycle1.Name = "tbTimesCycle1";
		this.tbTimesCycle1.Padding = new System.Windows.Forms.Padding(5);
		this.tbTimesCycle1.PasswordChar = '\0';
		this.tbTimesCycle1.PromptColor = System.Drawing.Color.Gray;
		this.tbTimesCycle1.PromptFont = new System.Drawing.Font("微软雅黑", 15f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
		this.tbTimesCycle1.PromptText = "";
		this.tbTimesCycle1.RectColor = System.Drawing.Color.FromArgb(220, 220, 220);
		this.tbTimesCycle1.RectWidth = 1;
		this.tbTimesCycle1.RegexPattern = "";
		this.tbTimesCycle1.Size = new System.Drawing.Size(115, 33);
		this.tbTimesCycle1.TabIndex = 25;
		this.tbTimesCycle1.TextChanged += new System.EventHandler(tbTimesCycle1_TextChanged);
		this.label7.AutoSize = true;
		this.label7.Location = new System.Drawing.Point(581, 101);
		this.label7.Name = "label7";
		this.label7.Size = new System.Drawing.Size(53, 12);
		this.label7.TabIndex = 24;
		this.label7.Text = "分析时间";
		this.tbTimeCycle1.BackColor = System.Drawing.Color.Transparent;
		this.tbTimeCycle1.ConerRadius = 5;
		this.tbTimeCycle1.Cursor = System.Windows.Forms.Cursors.IBeam;
		this.tbTimeCycle1.DecLength = 2;
		this.tbTimeCycle1.FillColor = System.Drawing.Color.Empty;
		this.tbTimeCycle1.FocusBorderColor = System.Drawing.Color.FromArgb(255, 77, 59);
		this.tbTimeCycle1.Font = new System.Drawing.Font("微软雅黑", 18f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
		this.tbTimeCycle1.InputText = "";
		this.tbTimeCycle1.InputType = HZH_Controls.TextInputType.NotControl;
		this.tbTimeCycle1.IsFocusColor = true;
		this.tbTimeCycle1.IsRadius = true;
		this.tbTimeCycle1.IsShowClearBtn = true;
		this.tbTimeCycle1.IsShowKeyboard = false;
		this.tbTimeCycle1.IsShowRect = true;
		this.tbTimeCycle1.IsShowSearchBtn = false;
		this.tbTimeCycle1.KeyBoardType = HZH_Controls.Controls.KeyBoardType.UCKeyBorderNum;
		this.tbTimeCycle1.Location = new System.Drawing.Point(650, 89);
		this.tbTimeCycle1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
		this.tbTimeCycle1.MaxValue = new decimal(new int[4] { 1000000, 0, 0, 0 });
		this.tbTimeCycle1.MinValue = new decimal(new int[4] { 1000000, 0, 0, -2147483648 });
		this.tbTimeCycle1.Name = "tbTimeCycle1";
		this.tbTimeCycle1.Padding = new System.Windows.Forms.Padding(5);
		this.tbTimeCycle1.PasswordChar = '\0';
		this.tbTimeCycle1.PromptColor = System.Drawing.Color.Gray;
		this.tbTimeCycle1.PromptFont = new System.Drawing.Font("微软雅黑", 15f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
		this.tbTimeCycle1.PromptText = "";
		this.tbTimeCycle1.RectColor = System.Drawing.Color.FromArgb(220, 220, 220);
		this.tbTimeCycle1.RectWidth = 1;
		this.tbTimeCycle1.RegexPattern = "";
		this.tbTimeCycle1.Size = new System.Drawing.Size(115, 33);
		this.tbTimeCycle1.TabIndex = 23;
		this.tbTimeCycle1.TextChanged += new System.EventHandler(tbTimeCycle1_TextChanged);
		this.chromDeviceCtrl1.BackColor = System.Drawing.Color.Transparent;
		this.chromDeviceCtrl1.Location = new System.Drawing.Point(600, 476);
		this.chromDeviceCtrl1.Margin = new System.Windows.Forms.Padding(4);
		this.chromDeviceCtrl1.Name = "chromDeviceCtrl1";
		this.chromDeviceCtrl1.Size = new System.Drawing.Size(10, 10);
		this.chromDeviceCtrl1.TabIndex = 0;
		this.btnStartTe.BackColor = System.Drawing.Color.White;
		this.btnStartTe.BtnBackColor = System.Drawing.Color.White;
		this.btnStartTe.BtnFont = new System.Drawing.Font("微软雅黑", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		this.btnStartTe.BtnForeColor = System.Drawing.Color.White;
		this.btnStartTe.BtnText = "开始控温";
		this.btnStartTe.ConerRadius = 20;
		this.btnStartTe.Cursor = System.Windows.Forms.Cursors.Hand;
		this.btnStartTe.EnabledMouseEffect = true;
		this.btnStartTe.FillColor = System.Drawing.Color.OrangeRed;
		this.btnStartTe.Font = new System.Drawing.Font("微软雅黑", 15f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
		this.btnStartTe.IsRadius = true;
		this.btnStartTe.IsShowRect = true;
		this.btnStartTe.IsShowTips = false;
		this.btnStartTe.Location = new System.Drawing.Point(457, 77);
		this.btnStartTe.Margin = new System.Windows.Forms.Padding(0);
		this.btnStartTe.Name = "btnStartTe";
		this.btnStartTe.RectColor = System.Drawing.Color.FromArgb(255, 77, 58);
		this.btnStartTe.RectWidth = 1;
		this.btnStartTe.Size = new System.Drawing.Size(115, 31);
		this.btnStartTe.TabIndex = 21;
		this.btnStartTe.TabStop = false;
		this.btnStartTe.TipsColor = System.Drawing.Color.FromArgb(232, 30, 99);
		this.btnStartTe.TipsText = "";
		this.btnStartTe.BtnClick += new System.EventHandler(btnStartTe_BtnClick);
		this.btnStartAn.BackColor = System.Drawing.Color.White;
		this.btnStartAn.BtnBackColor = System.Drawing.Color.White;
		this.btnStartAn.BtnFont = new System.Drawing.Font("微软雅黑", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		this.btnStartAn.BtnForeColor = System.Drawing.Color.White;
		this.btnStartAn.BtnText = "开始分析";
		this.btnStartAn.ConerRadius = 20;
		this.btnStartAn.Cursor = System.Windows.Forms.Cursors.Hand;
		this.btnStartAn.EnabledMouseEffect = true;
		this.btnStartAn.FillColor = System.Drawing.Color.OrangeRed;
		this.btnStartAn.Font = new System.Drawing.Font("微软雅黑", 15f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
		this.btnStartAn.IsRadius = true;
		this.btnStartAn.IsShowRect = true;
		this.btnStartAn.IsShowTips = false;
		this.btnStartAn.Location = new System.Drawing.Point(457, 32);
		this.btnStartAn.Margin = new System.Windows.Forms.Padding(0);
		this.btnStartAn.Name = "btnStartAn";
		this.btnStartAn.RectColor = System.Drawing.Color.FromArgb(255, 77, 58);
		this.btnStartAn.RectWidth = 1;
		this.btnStartAn.Size = new System.Drawing.Size(115, 31);
		this.btnStartAn.TabIndex = 20;
		this.btnStartAn.TabStop = false;
		this.btnStartAn.TipsColor = System.Drawing.Color.FromArgb(232, 30, 99);
		this.btnStartAn.TipsText = "";
		this.btnStartAn.BtnClick += new System.EventHandler(btnStartAn_BtnClick);
		this.chromAcqCtrl1.Location = new System.Drawing.Point(3, 295);
		this.chromAcqCtrl1.Name = "chromAcqCtrl1";
		this.chromAcqCtrl1.ShowLYTHCMethod = false;
		this.chromAcqCtrl1.ShowOnlineMethod = false;
		this.chromAcqCtrl1.ShowRNMode = false;
		this.chromAcqCtrl1.Size = new System.Drawing.Size(412, 222);
		this.chromAcqCtrl1.TabIndex = 19;
		this.tabPage4.Controls.Add(this.tabControl1);
		this.tabPage4.Location = new System.Drawing.Point(4, 4);
		this.tabPage4.Name = "tabPage4";
		this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
		this.tabPage4.Size = new System.Drawing.Size(792, 517);
		this.tabPage4.TabIndex = 1;
		this.tabPage4.Text = "方法";
		this.tabPage4.UseVisualStyleBackColor = true;
		this.tabControl1.Alignment = System.Windows.Forms.TabAlignment.Left;
		this.tabControl1.Controls.Add(this.tabPage6);
		this.tabControl1.Controls.Add(this.tabPage7);
		this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
		this.tabControl1.Location = new System.Drawing.Point(3, 3);
		this.tabControl1.Multiline = true;
		this.tabControl1.Name = "tabControl1";
		this.tabControl1.SelectedIndex = 0;
		this.tabControl1.Size = new System.Drawing.Size(786, 511);
		this.tabControl1.TabIndex = 3;
		this.tabPage6.Controls.Add(this.tbFireOn);
		this.tabPage6.Controls.Add(this.btnFireOnSet);
		this.tabPage6.Controls.Add(this.btnFireOnCheck);
		this.tabPage6.Controls.Add(this.label12);
		this.tabPage6.Controls.Add(this.label11);
		this.tabPage6.Controls.Add(this.ucTBValve2);
		this.tabPage6.Controls.Add(this.ucTBValve1);
		this.tabPage6.Controls.Add(this.ucTBJump2);
		this.tabPage6.Controls.Add(this.ucTBJump1);
		this.tabPage6.Controls.Add(this.ucPTSys);
		this.tabPage6.Controls.Add(this.label9);
		this.tabPage6.Controls.Add(this.label1);
		this.tabPage6.Controls.Add(this.label3);
		this.tabPage6.Controls.Add(this.label5);
		this.tabPage6.Controls.Add(this.label8);
		this.tabPage6.Controls.Add(this.tbHHSet2);
		this.tabPage6.Controls.Add(this.tbColPreSet2);
		this.tabPage6.Controls.Add(this.tbAirSet1);
		this.tabPage6.Controls.Add(this.tbHHSet1);
		this.tabPage6.Controls.Add(this.tbColPreSet1);
		this.tabPage6.Controls.Add(this.uSwDec);
		this.tabPage6.Controls.Add(this.uSwAUX);
		this.tabPage6.Controls.Add(this.uSwColu);
		this.tabPage6.Controls.Add(this.tbColueAUX);
		this.tabPage6.Controls.Add(this.tbColuenDetec);
		this.tabPage6.Controls.Add(this.tbColuenTemp);
		this.tabPage6.Controls.Add(this.label24);
		this.tabPage6.Controls.Add(this.label23);
		this.tabPage6.Controls.Add(this.label22);
		this.tabPage6.Location = new System.Drawing.Point(22, 4);
		this.tabPage6.Name = "tabPage6";
		this.tabPage6.Padding = new System.Windows.Forms.Padding(3);
		this.tabPage6.Size = new System.Drawing.Size(760, 503);
		this.tabPage6.TabIndex = 0;
		this.tabPage6.Text = "仪器方法";
		this.tabPage6.UseVisualStyleBackColor = true;
		this.tabPage6.Click += new System.EventHandler(tabPage6_Click);
		this.tbFireOn.BackColor = System.Drawing.Color.Transparent;
		this.tbFireOn.ConerRadius = 5;
		this.tbFireOn.Cursor = System.Windows.Forms.Cursors.IBeam;
		this.tbFireOn.DecLength = 2;
		this.tbFireOn.FillColor = System.Drawing.Color.Empty;
		this.tbFireOn.FocusBorderColor = System.Drawing.Color.FromArgb(255, 77, 59);
		this.tbFireOn.Font = new System.Drawing.Font("微软雅黑", 18f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
		this.tbFireOn.InputText = "";
		this.tbFireOn.InputType = HZH_Controls.TextInputType.NotControl;
		this.tbFireOn.IsFocusColor = true;
		this.tbFireOn.IsRadius = true;
		this.tbFireOn.IsShowClearBtn = true;
		this.tbFireOn.IsShowKeyboard = false;
		this.tbFireOn.IsShowRect = true;
		this.tbFireOn.IsShowSearchBtn = false;
		this.tbFireOn.KeyBoardType = HZH_Controls.Controls.KeyBoardType.UCKeyBorderNum;
		this.tbFireOn.Location = new System.Drawing.Point(367, 186);
		this.tbFireOn.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
		this.tbFireOn.MaxValue = new decimal(new int[4] { 1000000, 0, 0, 0 });
		this.tbFireOn.MinValue = new decimal(new int[4] { 1000000, 0, 0, -2147483648 });
		this.tbFireOn.Name = "tbFireOn";
		this.tbFireOn.Padding = new System.Windows.Forms.Padding(5);
		this.tbFireOn.PasswordChar = '\0';
		this.tbFireOn.PromptColor = System.Drawing.Color.Gray;
		this.tbFireOn.PromptFont = new System.Drawing.Font("微软雅黑", 15f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
		this.tbFireOn.PromptText = "";
		this.tbFireOn.RectColor = System.Drawing.Color.FromArgb(220, 220, 220);
		this.tbFireOn.RectWidth = 1;
		this.tbFireOn.RegexPattern = "";
		this.tbFireOn.Size = new System.Drawing.Size(136, 33);
		this.tbFireOn.TabIndex = 78;
		this.btnFireOnSet.BackColor = System.Drawing.Color.White;
		this.btnFireOnSet.BtnBackColor = System.Drawing.Color.White;
		this.btnFireOnSet.BtnFont = new System.Drawing.Font("微软雅黑", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		this.btnFireOnSet.BtnForeColor = System.Drawing.Color.White;
		this.btnFireOnSet.BtnText = "点火门限设定";
		this.btnFireOnSet.ConerRadius = 30;
		this.btnFireOnSet.Cursor = System.Windows.Forms.Cursors.Hand;
		this.btnFireOnSet.EnabledMouseEffect = true;
		this.btnFireOnSet.FillColor = System.Drawing.Color.FromArgb(255, 77, 59);
		this.btnFireOnSet.Font = new System.Drawing.Font("微软雅黑", 15f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
		this.btnFireOnSet.IsRadius = true;
		this.btnFireOnSet.IsShowRect = true;
		this.btnFireOnSet.IsShowTips = false;
		this.btnFireOnSet.Location = new System.Drawing.Point(198, 180);
		this.btnFireOnSet.Margin = new System.Windows.Forms.Padding(0);
		this.btnFireOnSet.Name = "btnFireOnSet";
		this.btnFireOnSet.RectColor = System.Drawing.Color.FromArgb(255, 77, 58);
		this.btnFireOnSet.RectWidth = 1;
		this.btnFireOnSet.Size = new System.Drawing.Size(141, 39);
		this.btnFireOnSet.TabIndex = 77;
		this.btnFireOnSet.TabStop = false;
		this.btnFireOnSet.TipsColor = System.Drawing.Color.FromArgb(232, 30, 99);
		this.btnFireOnSet.TipsText = "";
		this.btnFireOnSet.BtnClick += new System.EventHandler(btnFireOnSet_BtnClick);
		this.btnFireOnCheck.BackColor = System.Drawing.Color.White;
		this.btnFireOnCheck.BtnBackColor = System.Drawing.Color.White;
		this.btnFireOnCheck.BtnFont = new System.Drawing.Font("微软雅黑", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		this.btnFireOnCheck.BtnForeColor = System.Drawing.Color.White;
		this.btnFireOnCheck.BtnText = "点火门限查询";
		this.btnFireOnCheck.ConerRadius = 30;
		this.btnFireOnCheck.Cursor = System.Windows.Forms.Cursors.Hand;
		this.btnFireOnCheck.EnabledMouseEffect = true;
		this.btnFireOnCheck.FillColor = System.Drawing.Color.FromArgb(255, 77, 59);
		this.btnFireOnCheck.Font = new System.Drawing.Font("微软雅黑", 15f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
		this.btnFireOnCheck.IsRadius = true;
		this.btnFireOnCheck.IsShowRect = true;
		this.btnFireOnCheck.IsShowTips = false;
		this.btnFireOnCheck.Location = new System.Drawing.Point(42, 180);
		this.btnFireOnCheck.Margin = new System.Windows.Forms.Padding(0);
		this.btnFireOnCheck.Name = "btnFireOnCheck";
		this.btnFireOnCheck.RectColor = System.Drawing.Color.FromArgb(255, 77, 58);
		this.btnFireOnCheck.RectWidth = 1;
		this.btnFireOnCheck.Size = new System.Drawing.Size(141, 39);
		this.btnFireOnCheck.TabIndex = 76;
		this.btnFireOnCheck.TabStop = false;
		this.btnFireOnCheck.TipsColor = System.Drawing.Color.FromArgb(232, 30, 99);
		this.btnFireOnCheck.TipsText = "";
		this.btnFireOnCheck.BtnClick += new System.EventHandler(btnFireOnCheck_BtnClick);
		this.label12.AutoSize = true;
		this.label12.Font = new System.Drawing.Font("微软雅黑", 15f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		this.label12.ForeColor = System.Drawing.Color.OrangeRed;
		this.label12.Location = new System.Drawing.Point(647, 15);
		this.label12.Name = "label12";
		this.label12.Size = new System.Drawing.Size(64, 27);
		this.label12.TabIndex = 75;
		this.label12.Text = "事件2";
		this.label11.AutoSize = true;
		this.label11.Font = new System.Drawing.Font("微软雅黑", 15f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		this.label11.ForeColor = System.Drawing.Color.OrangeRed;
		this.label11.Location = new System.Drawing.Point(503, 15);
		this.label11.Name = "label11";
		this.label11.Size = new System.Drawing.Size(64, 27);
		this.label11.TabIndex = 74;
		this.label11.Text = "事件1";
		this.ucTBValve2.BackColor = System.Drawing.Color.Transparent;
		this.ucTBValve2.ConerRadius = 5;
		this.ucTBValve2.Cursor = System.Windows.Forms.Cursors.IBeam;
		this.ucTBValve2.DecLength = 2;
		this.ucTBValve2.FillColor = System.Drawing.Color.Empty;
		this.ucTBValve2.FocusBorderColor = System.Drawing.Color.FromArgb(255, 77, 59);
		this.ucTBValve2.Font = new System.Drawing.Font("微软雅黑", 18f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
		this.ucTBValve2.InputText = "";
		this.ucTBValve2.InputType = HZH_Controls.TextInputType.NotControl;
		this.ucTBValve2.IsFocusColor = true;
		this.ucTBValve2.IsRadius = true;
		this.ucTBValve2.IsShowClearBtn = true;
		this.ucTBValve2.IsShowKeyboard = false;
		this.ucTBValve2.IsShowRect = true;
		this.ucTBValve2.IsShowSearchBtn = false;
		this.ucTBValve2.KeyBoardType = HZH_Controls.Controls.KeyBoardType.UCKeyBorderNum;
		this.ucTBValve2.Location = new System.Drawing.Point(622, 99);
		this.ucTBValve2.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
		this.ucTBValve2.MaxValue = new decimal(new int[4] { 1000000, 0, 0, 0 });
		this.ucTBValve2.MinValue = new decimal(new int[4] { 1000000, 0, 0, -2147483648 });
		this.ucTBValve2.Name = "ucTBValve2";
		this.ucTBValve2.Padding = new System.Windows.Forms.Padding(5);
		this.ucTBValve2.PasswordChar = '\0';
		this.ucTBValve2.PromptColor = System.Drawing.Color.Gray;
		this.ucTBValve2.PromptFont = new System.Drawing.Font("微软雅黑", 15f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
		this.ucTBValve2.PromptText = "";
		this.ucTBValve2.RectColor = System.Drawing.Color.FromArgb(220, 220, 220);
		this.ucTBValve2.RectWidth = 1;
		this.ucTBValve2.RegexPattern = "";
		this.ucTBValve2.Size = new System.Drawing.Size(131, 33);
		this.ucTBValve2.TabIndex = 73;
		this.ucTBValve1.BackColor = System.Drawing.Color.Transparent;
		this.ucTBValve1.ConerRadius = 5;
		this.ucTBValve1.Cursor = System.Windows.Forms.Cursors.IBeam;
		this.ucTBValve1.DecLength = 2;
		this.ucTBValve1.FillColor = System.Drawing.Color.Empty;
		this.ucTBValve1.FocusBorderColor = System.Drawing.Color.FromArgb(255, 77, 59);
		this.ucTBValve1.Font = new System.Drawing.Font("微软雅黑", 18f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
		this.ucTBValve1.InputText = "";
		this.ucTBValve1.InputType = HZH_Controls.TextInputType.NotControl;
		this.ucTBValve1.IsFocusColor = true;
		this.ucTBValve1.IsRadius = true;
		this.ucTBValve1.IsShowClearBtn = true;
		this.ucTBValve1.IsShowKeyboard = false;
		this.ucTBValve1.IsShowRect = true;
		this.ucTBValve1.IsShowSearchBtn = false;
		this.ucTBValve1.KeyBoardType = HZH_Controls.Controls.KeyBoardType.UCKeyBorderNum;
		this.ucTBValve1.Location = new System.Drawing.Point(622, 53);
		this.ucTBValve1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
		this.ucTBValve1.MaxValue = new decimal(new int[4] { 1000000, 0, 0, 0 });
		this.ucTBValve1.MinValue = new decimal(new int[4] { 1000000, 0, 0, -2147483648 });
		this.ucTBValve1.Name = "ucTBValve1";
		this.ucTBValve1.Padding = new System.Windows.Forms.Padding(5);
		this.ucTBValve1.PasswordChar = '\0';
		this.ucTBValve1.PromptColor = System.Drawing.Color.Gray;
		this.ucTBValve1.PromptFont = new System.Drawing.Font("微软雅黑", 15f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
		this.ucTBValve1.PromptText = "";
		this.ucTBValve1.RectColor = System.Drawing.Color.FromArgb(220, 220, 220);
		this.ucTBValve1.RectWidth = 1;
		this.ucTBValve1.RegexPattern = "";
		this.ucTBValve1.Size = new System.Drawing.Size(131, 33);
		this.ucTBValve1.TabIndex = 72;
		this.ucTBJump2.BackColor = System.Drawing.Color.Transparent;
		this.ucTBJump2.ConerRadius = 5;
		this.ucTBJump2.Cursor = System.Windows.Forms.Cursors.IBeam;
		this.ucTBJump2.DecLength = 2;
		this.ucTBJump2.FillColor = System.Drawing.Color.Empty;
		this.ucTBJump2.FocusBorderColor = System.Drawing.Color.FromArgb(255, 77, 59);
		this.ucTBJump2.Font = new System.Drawing.Font("微软雅黑", 18f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
		this.ucTBJump2.InputText = "";
		this.ucTBJump2.InputType = HZH_Controls.TextInputType.NotControl;
		this.ucTBJump2.IsFocusColor = true;
		this.ucTBJump2.IsRadius = true;
		this.ucTBJump2.IsShowClearBtn = true;
		this.ucTBJump2.IsShowKeyboard = false;
		this.ucTBJump2.IsShowRect = true;
		this.ucTBJump2.IsShowSearchBtn = false;
		this.ucTBJump2.KeyBoardType = HZH_Controls.Controls.KeyBoardType.UCKeyBorderNum;
		this.ucTBJump2.Location = new System.Drawing.Point(462, 99);
		this.ucTBJump2.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
		this.ucTBJump2.MaxValue = new decimal(new int[4] { 1000000, 0, 0, 0 });
		this.ucTBJump2.MinValue = new decimal(new int[4] { 1000000, 0, 0, -2147483648 });
		this.ucTBJump2.Name = "ucTBJump2";
		this.ucTBJump2.Padding = new System.Windows.Forms.Padding(5);
		this.ucTBJump2.PasswordChar = '\0';
		this.ucTBJump2.PromptColor = System.Drawing.Color.Gray;
		this.ucTBJump2.PromptFont = new System.Drawing.Font("微软雅黑", 15f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
		this.ucTBJump2.PromptText = "";
		this.ucTBJump2.RectColor = System.Drawing.Color.FromArgb(220, 220, 220);
		this.ucTBJump2.RectWidth = 1;
		this.ucTBJump2.RegexPattern = "";
		this.ucTBJump2.Size = new System.Drawing.Size(133, 33);
		this.ucTBJump2.TabIndex = 71;
		this.ucTBJump1.BackColor = System.Drawing.Color.Transparent;
		this.ucTBJump1.ConerRadius = 5;
		this.ucTBJump1.Cursor = System.Windows.Forms.Cursors.IBeam;
		this.ucTBJump1.DecLength = 2;
		this.ucTBJump1.FillColor = System.Drawing.Color.Empty;
		this.ucTBJump1.FocusBorderColor = System.Drawing.Color.FromArgb(255, 77, 59);
		this.ucTBJump1.Font = new System.Drawing.Font("微软雅黑", 18f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
		this.ucTBJump1.InputText = "";
		this.ucTBJump1.InputType = HZH_Controls.TextInputType.NotControl;
		this.ucTBJump1.IsFocusColor = true;
		this.ucTBJump1.IsRadius = true;
		this.ucTBJump1.IsShowClearBtn = true;
		this.ucTBJump1.IsShowKeyboard = false;
		this.ucTBJump1.IsShowRect = true;
		this.ucTBJump1.IsShowSearchBtn = false;
		this.ucTBJump1.KeyBoardType = HZH_Controls.Controls.KeyBoardType.UCKeyBorderNum;
		this.ucTBJump1.Location = new System.Drawing.Point(462, 53);
		this.ucTBJump1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
		this.ucTBJump1.MaxValue = new decimal(new int[4] { 1000000, 0, 0, 0 });
		this.ucTBJump1.MinValue = new decimal(new int[4] { 1000000, 0, 0, -2147483648 });
		this.ucTBJump1.Name = "ucTBJump1";
		this.ucTBJump1.Padding = new System.Windows.Forms.Padding(5);
		this.ucTBJump1.PasswordChar = '\0';
		this.ucTBJump1.PromptColor = System.Drawing.Color.Gray;
		this.ucTBJump1.PromptFont = new System.Drawing.Font("微软雅黑", 15f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
		this.ucTBJump1.PromptText = "";
		this.ucTBJump1.RectColor = System.Drawing.Color.FromArgb(220, 220, 220);
		this.ucTBJump1.RectWidth = 1;
		this.ucTBJump1.RegexPattern = "";
		this.ucTBJump1.Size = new System.Drawing.Size(133, 33);
		this.ucTBJump1.TabIndex = 70;
		this.ucPTSys.BackColor = System.Drawing.Color.Transparent;
		this.ucPTSys.BorderColor = System.Drawing.Color.FromArgb(255, 77, 59);
		this.ucPTSys.ConerRadius = 10;
		this.ucPTSys.Controls.Add(this.ucTBShowSJ);
		this.ucPTSys.Controls.Add(this.btnNetConfig);
		this.ucPTSys.Controls.Add(this.ucTBshuaijian);
		this.ucPTSys.Controls.Add(this.ucBtnSet);
		this.ucPTSys.FillColor = System.Drawing.Color.White;
		this.ucPTSys.Font = new System.Drawing.Font("微软雅黑", 15f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
		this.ucPTSys.IsCanExpand = true;
		this.ucPTSys.IsExpand = false;
		this.ucPTSys.IsRadius = true;
		this.ucPTSys.IsShowRect = true;
		this.ucPTSys.Location = new System.Drawing.Point(342, 244);
		this.ucPTSys.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
		this.ucPTSys.Name = "ucPTSys";
		this.ucPTSys.Padding = new System.Windows.Forms.Padding(1);
		this.ucPTSys.RectColor = System.Drawing.Color.FromArgb(255, 77, 59);
		this.ucPTSys.RectWidth = 1;
		this.ucPTSys.Size = new System.Drawing.Size(394, 199);
		this.ucPTSys.TabIndex = 69;
		this.ucPTSys.Title = "系统参数";
		this.ucTBShowSJ.BackColor = System.Drawing.Color.Transparent;
		this.ucTBShowSJ.ConerRadius = 5;
		this.ucTBShowSJ.Cursor = System.Windows.Forms.Cursors.IBeam;
		this.ucTBShowSJ.DecLength = 2;
		this.ucTBShowSJ.FillColor = System.Drawing.Color.Empty;
		this.ucTBShowSJ.FocusBorderColor = System.Drawing.Color.FromArgb(255, 77, 59);
		this.ucTBShowSJ.Font = new System.Drawing.Font("微软雅黑", 18f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
		this.ucTBShowSJ.InputText = "";
		this.ucTBShowSJ.InputType = HZH_Controls.TextInputType.NotControl;
		this.ucTBShowSJ.IsFocusColor = true;
		this.ucTBShowSJ.IsRadius = true;
		this.ucTBShowSJ.IsShowClearBtn = true;
		this.ucTBShowSJ.IsShowKeyboard = false;
		this.ucTBShowSJ.IsShowRect = true;
		this.ucTBShowSJ.IsShowSearchBtn = false;
		this.ucTBShowSJ.KeyBoardType = HZH_Controls.Controls.KeyBoardType.UCKeyBorderNum;
		this.ucTBShowSJ.Location = new System.Drawing.Point(25, 59);
		this.ucTBShowSJ.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
		this.ucTBShowSJ.MaxValue = new decimal(new int[4] { 1000000, 0, 0, 0 });
		this.ucTBShowSJ.MinValue = new decimal(new int[4] { 1000000, 0, 0, -2147483648 });
		this.ucTBShowSJ.Name = "ucTBShowSJ";
		this.ucTBShowSJ.Padding = new System.Windows.Forms.Padding(5);
		this.ucTBShowSJ.PasswordChar = '\0';
		this.ucTBShowSJ.PromptColor = System.Drawing.Color.Gray;
		this.ucTBShowSJ.PromptFont = new System.Drawing.Font("微软雅黑", 15f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
		this.ucTBShowSJ.PromptText = "";
		this.ucTBShowSJ.RectColor = System.Drawing.Color.FromArgb(220, 220, 220);
		this.ucTBShowSJ.RectWidth = 1;
		this.ucTBShowSJ.RegexPattern = "";
		this.ucTBShowSJ.Size = new System.Drawing.Size(136, 33);
		this.ucTBShowSJ.TabIndex = 70;
		this.btnNetConfig.BackColor = System.Drawing.Color.White;
		this.btnNetConfig.BtnBackColor = System.Drawing.Color.White;
		this.btnNetConfig.BtnFont = new System.Drawing.Font("微软雅黑", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		this.btnNetConfig.BtnForeColor = System.Drawing.Color.White;
		this.btnNetConfig.BtnText = "网络配置";
		this.btnNetConfig.ConerRadius = 20;
		this.btnNetConfig.Cursor = System.Windows.Forms.Cursors.Hand;
		this.btnNetConfig.EnabledMouseEffect = true;
		this.btnNetConfig.FillColor = System.Drawing.Color.OrangeRed;
		this.btnNetConfig.Font = new System.Drawing.Font("微软雅黑", 15f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
		this.btnNetConfig.IsRadius = true;
		this.btnNetConfig.IsShowRect = true;
		this.btnNetConfig.IsShowTips = false;
		this.btnNetConfig.Location = new System.Drawing.Point(227, 135);
		this.btnNetConfig.Margin = new System.Windows.Forms.Padding(0);
		this.btnNetConfig.Name = "btnNetConfig";
		this.btnNetConfig.RectColor = System.Drawing.Color.FromArgb(255, 77, 58);
		this.btnNetConfig.RectWidth = 1;
		this.btnNetConfig.Size = new System.Drawing.Size(153, 60);
		this.btnNetConfig.TabIndex = 27;
		this.btnNetConfig.TabStop = false;
		this.btnNetConfig.TipsColor = System.Drawing.Color.FromArgb(232, 30, 99);
		this.btnNetConfig.TipsText = "";
		this.btnNetConfig.BtnClick += new System.EventHandler(btnNetConfig_BtnClick);
		this.ucTBshuaijian.BackColor = System.Drawing.Color.Transparent;
		this.ucTBshuaijian.ConerRadius = 5;
		this.ucTBshuaijian.Cursor = System.Windows.Forms.Cursors.IBeam;
		this.ucTBshuaijian.DecLength = 2;
		this.ucTBshuaijian.FillColor = System.Drawing.Color.Empty;
		this.ucTBshuaijian.FocusBorderColor = System.Drawing.Color.FromArgb(255, 77, 59);
		this.ucTBshuaijian.Font = new System.Drawing.Font("微软雅黑", 18f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
		this.ucTBshuaijian.InputText = "";
		this.ucTBshuaijian.InputType = HZH_Controls.TextInputType.NotControl;
		this.ucTBshuaijian.IsFocusColor = true;
		this.ucTBshuaijian.IsRadius = true;
		this.ucTBshuaijian.IsShowClearBtn = true;
		this.ucTBshuaijian.IsShowKeyboard = false;
		this.ucTBshuaijian.IsShowRect = true;
		this.ucTBshuaijian.IsShowSearchBtn = false;
		this.ucTBshuaijian.KeyBoardType = HZH_Controls.Controls.KeyBoardType.UCKeyBorderNum;
		this.ucTBshuaijian.Location = new System.Drawing.Point(170, 59);
		this.ucTBshuaijian.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
		this.ucTBshuaijian.MaxValue = new decimal(new int[4] { 1000000, 0, 0, 0 });
		this.ucTBshuaijian.MinValue = new decimal(new int[4] { 1000000, 0, 0, -2147483648 });
		this.ucTBshuaijian.Name = "ucTBshuaijian";
		this.ucTBshuaijian.Padding = new System.Windows.Forms.Padding(5);
		this.ucTBshuaijian.PasswordChar = '\0';
		this.ucTBshuaijian.PromptColor = System.Drawing.Color.Gray;
		this.ucTBshuaijian.PromptFont = new System.Drawing.Font("微软雅黑", 15f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
		this.ucTBshuaijian.PromptText = "";
		this.ucTBshuaijian.RectColor = System.Drawing.Color.FromArgb(220, 220, 220);
		this.ucTBshuaijian.RectWidth = 1;
		this.ucTBshuaijian.RegexPattern = "";
		this.ucTBshuaijian.Size = new System.Drawing.Size(136, 33);
		this.ucTBshuaijian.TabIndex = 69;
		this.ucTBshuaijian.Visible = false;
		this.ucBtnSet.BackColor = System.Drawing.Color.White;
		this.ucBtnSet.BtnBackColor = System.Drawing.Color.White;
		this.ucBtnSet.BtnFont = new System.Drawing.Font("微软雅黑", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		this.ucBtnSet.BtnForeColor = System.Drawing.Color.White;
		this.ucBtnSet.BtnText = "选项";
		this.ucBtnSet.ConerRadius = 30;
		this.ucBtnSet.Cursor = System.Windows.Forms.Cursors.Hand;
		this.ucBtnSet.EnabledMouseEffect = true;
		this.ucBtnSet.FillColor = System.Drawing.Color.FromArgb(255, 77, 59);
		this.ucBtnSet.Font = new System.Drawing.Font("微软雅黑", 15f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
		this.ucBtnSet.IsRadius = true;
		this.ucBtnSet.IsShowRect = true;
		this.ucBtnSet.IsShowTips = false;
		this.ucBtnSet.Location = new System.Drawing.Point(11, 135);
		this.ucBtnSet.Margin = new System.Windows.Forms.Padding(0);
		this.ucBtnSet.Name = "ucBtnSet";
		this.ucBtnSet.RectColor = System.Drawing.Color.FromArgb(255, 77, 58);
		this.ucBtnSet.RectWidth = 1;
		this.ucBtnSet.Size = new System.Drawing.Size(153, 60);
		this.ucBtnSet.TabIndex = 68;
		this.ucBtnSet.TabStop = false;
		this.ucBtnSet.TipsColor = System.Drawing.Color.FromArgb(232, 30, 99);
		this.ucBtnSet.TipsText = "";
		this.ucBtnSet.BtnClick += new System.EventHandler(ucBtnSet_BtnClick);
		this.label9.AutoSize = true;
		this.label9.Font = new System.Drawing.Font("微软雅黑", 15f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		this.label9.Location = new System.Drawing.Point(23, 334);
		this.label9.Name = "label9";
		this.label9.Size = new System.Drawing.Size(105, 27);
		this.label9.TabIndex = 67;
		this.label9.Text = "载气3(psi)";
		this.label1.AutoSize = true;
		this.label1.Font = new System.Drawing.Font("微软雅黑", 15f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		this.label1.Location = new System.Drawing.Point(23, 289);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(105, 27);
		this.label1.TabIndex = 66;
		this.label1.Text = "载气2(psi)";
		this.label3.AutoSize = true;
		this.label3.Font = new System.Drawing.Font("微软雅黑", 15f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		this.label3.Location = new System.Drawing.Point(23, 424);
		this.label3.Name = "label3";
		this.label3.Size = new System.Drawing.Size(93, 27);
		this.label3.TabIndex = 65;
		this.label3.Text = "空气(psi)";
		this.label5.AutoSize = true;
		this.label5.Font = new System.Drawing.Font("微软雅黑", 15f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		this.label5.Location = new System.Drawing.Point(23, 379);
		this.label5.Name = "label5";
		this.label5.Size = new System.Drawing.Size(93, 27);
		this.label5.TabIndex = 64;
		this.label5.Text = "氢气(psi)";
		this.label8.AutoSize = true;
		this.label8.Font = new System.Drawing.Font("微软雅黑", 15f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		this.label8.Location = new System.Drawing.Point(23, 244);
		this.label8.Name = "label8";
		this.label8.Size = new System.Drawing.Size(93, 27);
		this.label8.TabIndex = 63;
		this.label8.Text = "载气(psi)";
		this.tbHHSet2.BackColor = System.Drawing.Color.Transparent;
		this.tbHHSet2.ConerRadius = 5;
		this.tbHHSet2.Cursor = System.Windows.Forms.Cursors.IBeam;
		this.tbHHSet2.DecLength = 2;
		this.tbHHSet2.FillColor = System.Drawing.Color.Empty;
		this.tbHHSet2.FocusBorderColor = System.Drawing.Color.FromArgb(255, 77, 59);
		this.tbHHSet2.Font = new System.Drawing.Font("微软雅黑", 18f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
		this.tbHHSet2.InputText = "";
		this.tbHHSet2.InputType = HZH_Controls.TextInputType.NotControl;
		this.tbHHSet2.IsFocusColor = true;
		this.tbHHSet2.IsRadius = true;
		this.tbHHSet2.IsShowClearBtn = true;
		this.tbHHSet2.IsShowKeyboard = false;
		this.tbHHSet2.IsShowRect = true;
		this.tbHHSet2.IsShowSearchBtn = false;
		this.tbHHSet2.KeyBoardType = HZH_Controls.Controls.KeyBoardType.UCKeyBorderNum;
		this.tbHHSet2.Location = new System.Drawing.Point(198, 331);
		this.tbHHSet2.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
		this.tbHHSet2.MaxValue = new decimal(new int[4] { 1000000, 0, 0, 0 });
		this.tbHHSet2.MinValue = new decimal(new int[4] { 1000000, 0, 0, -2147483648 });
		this.tbHHSet2.Name = "tbHHSet2";
		this.tbHHSet2.Padding = new System.Windows.Forms.Padding(5);
		this.tbHHSet2.PasswordChar = '\0';
		this.tbHHSet2.PromptColor = System.Drawing.Color.Gray;
		this.tbHHSet2.PromptFont = new System.Drawing.Font("微软雅黑", 15f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
		this.tbHHSet2.PromptText = "";
		this.tbHHSet2.RectColor = System.Drawing.Color.FromArgb(220, 220, 220);
		this.tbHHSet2.RectWidth = 1;
		this.tbHHSet2.RegexPattern = "";
		this.tbHHSet2.Size = new System.Drawing.Size(136, 33);
		this.tbHHSet2.TabIndex = 62;
		this.tbColPreSet2.BackColor = System.Drawing.Color.Transparent;
		this.tbColPreSet2.ConerRadius = 5;
		this.tbColPreSet2.Cursor = System.Windows.Forms.Cursors.IBeam;
		this.tbColPreSet2.DecLength = 2;
		this.tbColPreSet2.FillColor = System.Drawing.Color.Empty;
		this.tbColPreSet2.FocusBorderColor = System.Drawing.Color.FromArgb(255, 77, 59);
		this.tbColPreSet2.Font = new System.Drawing.Font("微软雅黑", 18f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
		this.tbColPreSet2.InputText = "";
		this.tbColPreSet2.InputType = HZH_Controls.TextInputType.NotControl;
		this.tbColPreSet2.IsFocusColor = true;
		this.tbColPreSet2.IsRadius = true;
		this.tbColPreSet2.IsShowClearBtn = true;
		this.tbColPreSet2.IsShowKeyboard = false;
		this.tbColPreSet2.IsShowRect = true;
		this.tbColPreSet2.IsShowSearchBtn = false;
		this.tbColPreSet2.KeyBoardType = HZH_Controls.Controls.KeyBoardType.UCKeyBorderNum;
		this.tbColPreSet2.Location = new System.Drawing.Point(198, 286);
		this.tbColPreSet2.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
		this.tbColPreSet2.MaxValue = new decimal(new int[4] { 1000000, 0, 0, 0 });
		this.tbColPreSet2.MinValue = new decimal(new int[4] { 1000000, 0, 0, -2147483648 });
		this.tbColPreSet2.Name = "tbColPreSet2";
		this.tbColPreSet2.Padding = new System.Windows.Forms.Padding(5);
		this.tbColPreSet2.PasswordChar = '\0';
		this.tbColPreSet2.PromptColor = System.Drawing.Color.Gray;
		this.tbColPreSet2.PromptFont = new System.Drawing.Font("微软雅黑", 15f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
		this.tbColPreSet2.PromptText = "";
		this.tbColPreSet2.RectColor = System.Drawing.Color.FromArgb(220, 220, 220);
		this.tbColPreSet2.RectWidth = 1;
		this.tbColPreSet2.RegexPattern = "";
		this.tbColPreSet2.Size = new System.Drawing.Size(136, 33);
		this.tbColPreSet2.TabIndex = 61;
		this.tbAirSet1.BackColor = System.Drawing.Color.Transparent;
		this.tbAirSet1.ConerRadius = 5;
		this.tbAirSet1.Cursor = System.Windows.Forms.Cursors.IBeam;
		this.tbAirSet1.DecLength = 2;
		this.tbAirSet1.FillColor = System.Drawing.Color.Empty;
		this.tbAirSet1.FocusBorderColor = System.Drawing.Color.FromArgb(255, 77, 59);
		this.tbAirSet1.Font = new System.Drawing.Font("微软雅黑", 18f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
		this.tbAirSet1.InputText = "";
		this.tbAirSet1.InputType = HZH_Controls.TextInputType.NotControl;
		this.tbAirSet1.IsFocusColor = true;
		this.tbAirSet1.IsRadius = true;
		this.tbAirSet1.IsShowClearBtn = true;
		this.tbAirSet1.IsShowKeyboard = false;
		this.tbAirSet1.IsShowRect = true;
		this.tbAirSet1.IsShowSearchBtn = false;
		this.tbAirSet1.KeyBoardType = HZH_Controls.Controls.KeyBoardType.UCKeyBorderNum;
		this.tbAirSet1.Location = new System.Drawing.Point(198, 421);
		this.tbAirSet1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
		this.tbAirSet1.MaxValue = new decimal(new int[4] { 1000000, 0, 0, 0 });
		this.tbAirSet1.MinValue = new decimal(new int[4] { 1000000, 0, 0, -2147483648 });
		this.tbAirSet1.Name = "tbAirSet1";
		this.tbAirSet1.Padding = new System.Windows.Forms.Padding(5);
		this.tbAirSet1.PasswordChar = '\0';
		this.tbAirSet1.PromptColor = System.Drawing.Color.Gray;
		this.tbAirSet1.PromptFont = new System.Drawing.Font("微软雅黑", 15f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
		this.tbAirSet1.PromptText = "";
		this.tbAirSet1.RectColor = System.Drawing.Color.FromArgb(220, 220, 220);
		this.tbAirSet1.RectWidth = 1;
		this.tbAirSet1.RegexPattern = "";
		this.tbAirSet1.Size = new System.Drawing.Size(136, 33);
		this.tbAirSet1.TabIndex = 60;
		this.tbHHSet1.BackColor = System.Drawing.Color.Transparent;
		this.tbHHSet1.ConerRadius = 5;
		this.tbHHSet1.Cursor = System.Windows.Forms.Cursors.IBeam;
		this.tbHHSet1.DecLength = 2;
		this.tbHHSet1.FillColor = System.Drawing.Color.Empty;
		this.tbHHSet1.FocusBorderColor = System.Drawing.Color.FromArgb(255, 77, 59);
		this.tbHHSet1.Font = new System.Drawing.Font("微软雅黑", 18f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
		this.tbHHSet1.InputText = "";
		this.tbHHSet1.InputType = HZH_Controls.TextInputType.NotControl;
		this.tbHHSet1.IsFocusColor = true;
		this.tbHHSet1.IsRadius = true;
		this.tbHHSet1.IsShowClearBtn = true;
		this.tbHHSet1.IsShowKeyboard = false;
		this.tbHHSet1.IsShowRect = true;
		this.tbHHSet1.IsShowSearchBtn = false;
		this.tbHHSet1.KeyBoardType = HZH_Controls.Controls.KeyBoardType.UCKeyBorderNum;
		this.tbHHSet1.Location = new System.Drawing.Point(198, 376);
		this.tbHHSet1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
		this.tbHHSet1.MaxValue = new decimal(new int[4] { 1000000, 0, 0, 0 });
		this.tbHHSet1.MinValue = new decimal(new int[4] { 1000000, 0, 0, -2147483648 });
		this.tbHHSet1.Name = "tbHHSet1";
		this.tbHHSet1.Padding = new System.Windows.Forms.Padding(5);
		this.tbHHSet1.PasswordChar = '\0';
		this.tbHHSet1.PromptColor = System.Drawing.Color.Gray;
		this.tbHHSet1.PromptFont = new System.Drawing.Font("微软雅黑", 15f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
		this.tbHHSet1.PromptText = "";
		this.tbHHSet1.RectColor = System.Drawing.Color.FromArgb(220, 220, 220);
		this.tbHHSet1.RectWidth = 1;
		this.tbHHSet1.RegexPattern = "";
		this.tbHHSet1.Size = new System.Drawing.Size(136, 33);
		this.tbHHSet1.TabIndex = 59;
		this.tbColPreSet1.BackColor = System.Drawing.Color.Transparent;
		this.tbColPreSet1.ConerRadius = 5;
		this.tbColPreSet1.Cursor = System.Windows.Forms.Cursors.IBeam;
		this.tbColPreSet1.DecLength = 2;
		this.tbColPreSet1.FillColor = System.Drawing.Color.Empty;
		this.tbColPreSet1.FocusBorderColor = System.Drawing.Color.FromArgb(255, 77, 59);
		this.tbColPreSet1.Font = new System.Drawing.Font("微软雅黑", 18f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
		this.tbColPreSet1.InputText = "";
		this.tbColPreSet1.InputType = HZH_Controls.TextInputType.NotControl;
		this.tbColPreSet1.IsFocusColor = true;
		this.tbColPreSet1.IsRadius = true;
		this.tbColPreSet1.IsShowClearBtn = true;
		this.tbColPreSet1.IsShowKeyboard = false;
		this.tbColPreSet1.IsShowRect = true;
		this.tbColPreSet1.IsShowSearchBtn = false;
		this.tbColPreSet1.KeyBoardType = HZH_Controls.Controls.KeyBoardType.UCKeyBorderNum;
		this.tbColPreSet1.Location = new System.Drawing.Point(198, 241);
		this.tbColPreSet1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
		this.tbColPreSet1.MaxValue = new decimal(new int[4] { 1000000, 0, 0, 0 });
		this.tbColPreSet1.MinValue = new decimal(new int[4] { 1000000, 0, 0, -2147483648 });
		this.tbColPreSet1.Name = "tbColPreSet1";
		this.tbColPreSet1.Padding = new System.Windows.Forms.Padding(5);
		this.tbColPreSet1.PasswordChar = '\0';
		this.tbColPreSet1.PromptColor = System.Drawing.Color.Gray;
		this.tbColPreSet1.PromptFont = new System.Drawing.Font("微软雅黑", 15f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
		this.tbColPreSet1.PromptText = "";
		this.tbColPreSet1.RectColor = System.Drawing.Color.FromArgb(220, 220, 220);
		this.tbColPreSet1.RectWidth = 1;
		this.tbColPreSet1.RegexPattern = "";
		this.tbColPreSet1.Size = new System.Drawing.Size(136, 33);
		this.tbColPreSet1.TabIndex = 58;
		this.uSwDec.BackColor = System.Drawing.Color.Transparent;
		this.uSwDec.Checked = true;
		this.uSwDec.FalseColor = System.Drawing.Color.FromArgb(189, 189, 189);
		this.uSwDec.FalseTextColr = System.Drawing.Color.Green;
		this.uSwDec.Location = new System.Drawing.Point(341, 100);
		this.uSwDec.Name = "uSwDec";
		this.uSwDec.Size = new System.Drawing.Size(86, 34);
		this.uSwDec.SwitchType = HZH_Controls.Controls.SwitchType.Ellipse;
		this.uSwDec.TabIndex = 52;
		this.uSwDec.Texts = new string[2] { "开", "关" };
		this.uSwDec.TrueColor = System.Drawing.Color.FromArgb(255, 77, 59);
		this.uSwDec.TrueTextColr = System.Drawing.Color.Black;
		this.uSwDec.Click += new System.EventHandler(uSwDec_Click);
		this.uSwAUX.BackColor = System.Drawing.Color.Transparent;
		this.uSwAUX.Checked = true;
		this.uSwAUX.FalseColor = System.Drawing.Color.FromArgb(189, 189, 189);
		this.uSwAUX.FalseTextColr = System.Drawing.Color.Green;
		this.uSwAUX.Location = new System.Drawing.Point(341, 143);
		this.uSwAUX.Name = "uSwAUX";
		this.uSwAUX.Size = new System.Drawing.Size(86, 34);
		this.uSwAUX.SwitchType = HZH_Controls.Controls.SwitchType.Ellipse;
		this.uSwAUX.TabIndex = 51;
		this.uSwAUX.Texts = new string[2] { "开", "关" };
		this.uSwAUX.TrueColor = System.Drawing.Color.FromArgb(255, 77, 59);
		this.uSwAUX.TrueTextColr = System.Drawing.Color.Black;
		this.uSwAUX.Click += new System.EventHandler(uSwAUX_Click);
		this.uSwColu.BackColor = System.Drawing.Color.Transparent;
		this.uSwColu.Checked = true;
		this.uSwColu.FalseColor = System.Drawing.Color.FromArgb(189, 189, 189);
		this.uSwColu.FalseTextColr = System.Drawing.Color.Green;
		this.uSwColu.Location = new System.Drawing.Point(341, 55);
		this.uSwColu.Name = "uSwColu";
		this.uSwColu.Size = new System.Drawing.Size(86, 34);
		this.uSwColu.SwitchType = HZH_Controls.Controls.SwitchType.Ellipse;
		this.uSwColu.TabIndex = 50;
		this.uSwColu.Texts = new string[2] { "开", "关" };
		this.uSwColu.TrueColor = System.Drawing.Color.FromArgb(255, 77, 59);
		this.uSwColu.TrueTextColr = System.Drawing.Color.Black;
		this.uSwColu.Click += new System.EventHandler(uSwColu_Click);
		this.tbColueAUX.BackColor = System.Drawing.Color.Transparent;
		this.tbColueAUX.ConerRadius = 5;
		this.tbColueAUX.Cursor = System.Windows.Forms.Cursors.IBeam;
		this.tbColueAUX.DecLength = 2;
		this.tbColueAUX.FillColor = System.Drawing.Color.Empty;
		this.tbColueAUX.FocusBorderColor = System.Drawing.Color.FromArgb(255, 77, 59);
		this.tbColueAUX.Font = new System.Drawing.Font("微软雅黑", 18f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
		this.tbColueAUX.InputText = "";
		this.tbColueAUX.InputType = HZH_Controls.TextInputType.NotControl;
		this.tbColueAUX.IsFocusColor = true;
		this.tbColueAUX.IsRadius = true;
		this.tbColueAUX.IsShowClearBtn = true;
		this.tbColueAUX.IsShowKeyboard = false;
		this.tbColueAUX.IsShowRect = true;
		this.tbColueAUX.IsShowSearchBtn = false;
		this.tbColueAUX.KeyBoardType = HZH_Controls.Controls.KeyBoardType.UCKeyBorderNum;
		this.tbColueAUX.Location = new System.Drawing.Point(198, 142);
		this.tbColueAUX.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
		this.tbColueAUX.MaxValue = new decimal(new int[4] { 1000000, 0, 0, 0 });
		this.tbColueAUX.MinValue = new decimal(new int[4] { 1000000, 0, 0, -2147483648 });
		this.tbColueAUX.Name = "tbColueAUX";
		this.tbColueAUX.Padding = new System.Windows.Forms.Padding(5);
		this.tbColueAUX.PasswordChar = '\0';
		this.tbColueAUX.PromptColor = System.Drawing.Color.Gray;
		this.tbColueAUX.PromptFont = new System.Drawing.Font("微软雅黑", 15f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
		this.tbColueAUX.PromptText = "";
		this.tbColueAUX.RectColor = System.Drawing.Color.FromArgb(220, 220, 220);
		this.tbColueAUX.RectWidth = 1;
		this.tbColueAUX.RegexPattern = "";
		this.tbColueAUX.Size = new System.Drawing.Size(136, 33);
		this.tbColueAUX.TabIndex = 49;
		this.tbColuenDetec.BackColor = System.Drawing.Color.Transparent;
		this.tbColuenDetec.ConerRadius = 5;
		this.tbColuenDetec.Cursor = System.Windows.Forms.Cursors.IBeam;
		this.tbColuenDetec.DecLength = 2;
		this.tbColuenDetec.FillColor = System.Drawing.Color.Empty;
		this.tbColuenDetec.FocusBorderColor = System.Drawing.Color.FromArgb(255, 77, 59);
		this.tbColuenDetec.Font = new System.Drawing.Font("微软雅黑", 18f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
		this.tbColuenDetec.InputText = "";
		this.tbColuenDetec.InputType = HZH_Controls.TextInputType.NotControl;
		this.tbColuenDetec.IsFocusColor = true;
		this.tbColuenDetec.IsRadius = true;
		this.tbColuenDetec.IsShowClearBtn = true;
		this.tbColuenDetec.IsShowKeyboard = false;
		this.tbColuenDetec.IsShowRect = true;
		this.tbColuenDetec.IsShowSearchBtn = false;
		this.tbColuenDetec.KeyBoardType = HZH_Controls.Controls.KeyBoardType.UCKeyBorderNum;
		this.tbColuenDetec.Location = new System.Drawing.Point(198, 99);
		this.tbColuenDetec.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
		this.tbColuenDetec.MaxValue = new decimal(new int[4] { 1000000, 0, 0, 0 });
		this.tbColuenDetec.MinValue = new decimal(new int[4] { 1000000, 0, 0, -2147483648 });
		this.tbColuenDetec.Name = "tbColuenDetec";
		this.tbColuenDetec.Padding = new System.Windows.Forms.Padding(5);
		this.tbColuenDetec.PasswordChar = '\0';
		this.tbColuenDetec.PromptColor = System.Drawing.Color.Gray;
		this.tbColuenDetec.PromptFont = new System.Drawing.Font("微软雅黑", 15f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
		this.tbColuenDetec.PromptText = "";
		this.tbColuenDetec.RectColor = System.Drawing.Color.FromArgb(220, 220, 220);
		this.tbColuenDetec.RectWidth = 1;
		this.tbColuenDetec.RegexPattern = "";
		this.tbColuenDetec.Size = new System.Drawing.Size(136, 33);
		this.tbColuenDetec.TabIndex = 48;
		this.tbColuenTemp.BackColor = System.Drawing.Color.Transparent;
		this.tbColuenTemp.ConerRadius = 5;
		this.tbColuenTemp.Cursor = System.Windows.Forms.Cursors.IBeam;
		this.tbColuenTemp.DecLength = 2;
		this.tbColuenTemp.FillColor = System.Drawing.Color.Empty;
		this.tbColuenTemp.FocusBorderColor = System.Drawing.Color.FromArgb(255, 77, 59);
		this.tbColuenTemp.Font = new System.Drawing.Font("微软雅黑", 18f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
		this.tbColuenTemp.InputText = "";
		this.tbColuenTemp.InputType = HZH_Controls.TextInputType.NotControl;
		this.tbColuenTemp.IsFocusColor = true;
		this.tbColuenTemp.IsRadius = true;
		this.tbColuenTemp.IsShowClearBtn = true;
		this.tbColuenTemp.IsShowKeyboard = false;
		this.tbColuenTemp.IsShowRect = true;
		this.tbColuenTemp.IsShowSearchBtn = false;
		this.tbColuenTemp.KeyBoardType = HZH_Controls.Controls.KeyBoardType.UCKeyBorderNum;
		this.tbColuenTemp.Location = new System.Drawing.Point(198, 56);
		this.tbColuenTemp.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
		this.tbColuenTemp.MaxValue = new decimal(new int[4] { 1000000, 0, 0, 0 });
		this.tbColuenTemp.MinValue = new decimal(new int[4] { 1000000, 0, 0, -2147483648 });
		this.tbColuenTemp.Name = "tbColuenTemp";
		this.tbColuenTemp.Padding = new System.Windows.Forms.Padding(5);
		this.tbColuenTemp.PasswordChar = '\0';
		this.tbColuenTemp.PromptColor = System.Drawing.Color.Gray;
		this.tbColuenTemp.PromptFont = new System.Drawing.Font("微软雅黑", 15f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
		this.tbColuenTemp.PromptText = "";
		this.tbColuenTemp.RectColor = System.Drawing.Color.FromArgb(220, 220, 220);
		this.tbColuenTemp.RectWidth = 1;
		this.tbColuenTemp.RegexPattern = "";
		this.tbColuenTemp.Size = new System.Drawing.Size(136, 33);
		this.tbColuenTemp.TabIndex = 47;
		this.label24.AutoSize = true;
		this.label24.Font = new System.Drawing.Font("微软雅黑", 15f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		this.label24.Location = new System.Drawing.Point(23, 138);
		this.label24.Name = "label24";
		this.label24.Size = new System.Drawing.Size(146, 27);
		this.label24.TabIndex = 46;
		this.label24.Text = "伴热管温度(℃)";
		this.label23.AutoSize = true;
		this.label23.Font = new System.Drawing.Font("微软雅黑", 15f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		this.label23.Location = new System.Drawing.Point(23, 100);
		this.label23.Name = "label23";
		this.label23.Size = new System.Drawing.Size(146, 27);
		this.label23.TabIndex = 45;
		this.label23.Text = "检测器温度(℃)";
		this.label22.AutoSize = true;
		this.label22.Font = new System.Drawing.Font("微软雅黑", 15f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		this.label22.Location = new System.Drawing.Point(23, 61);
		this.label22.Name = "label22";
		this.label22.Size = new System.Drawing.Size(126, 27);
		this.label22.TabIndex = 44;
		this.label22.Text = "柱箱温度(℃)";
		this.tabPage7.Controls.Add(this.MainmstSet);
		this.tabPage7.Location = new System.Drawing.Point(40, 4);
		this.tabPage7.Name = "tabPage7";
		this.tabPage7.Padding = new System.Windows.Forms.Padding(3);
		this.tabPage7.Size = new System.Drawing.Size(142, 28);
		this.tabPage7.TabIndex = 1;
		this.tabPage7.Text = "单点校正";
		this.tabPage7.UseVisualStyleBackColor = true;
		this.MainmstSet.devManager = (IBrainChrom2018.InsDeviceManager)resources.GetObject("MainmstSet.devManager");
		this.MainmstSet.Dock = System.Windows.Forms.DockStyle.Fill;
		this.MainmstSet.Location = new System.Drawing.Point(3, 3);
		this.MainmstSet.Name = "MainmstSet";
		this.MainmstSet.PrintMethod = (IBrainChrom2018.PrintPara)resources.GetObject("MainmstSet.PrintMethod");
		this.MainmstSet.ShowComponentTable = false;
		this.MainmstSet.ShowMethodNew = false;
		this.MainmstSet.ShowOnlineMethod = false;
		this.MainmstSet.ShowOnlineMethod2 = false;
		this.MainmstSet.Size = new System.Drawing.Size(136, 22);
		this.MainmstSet.TabIndex = 1;
		this.MainmstSet.OnMethodSaveEvent += new System.EventHandler(MainmstSet_OnMethodSaveEvent);
		this.tabPage2.Controls.Add(this.splitContainer3);
		this.tabPage2.Location = new System.Drawing.Point(4, 4);
		this.tabPage2.Name = "tabPage2";
		this.tabPage2.Size = new System.Drawing.Size(792, 517);
		this.tabPage2.TabIndex = 3;
		this.tabPage2.Text = "历史数据";
		this.tabPage2.UseVisualStyleBackColor = true;
		this.splitContainer3.Dock = System.Windows.Forms.DockStyle.Fill;
		this.splitContainer3.Location = new System.Drawing.Point(0, 0);
		this.splitContainer3.Name = "splitContainer3";
		this.splitContainer3.Orientation = System.Windows.Forms.Orientation.Horizontal;
		this.splitContainer3.Panel1.Controls.Add(this.ucBtnOpenSda);
		this.splitContainer3.Panel1.Controls.Add(this.ucBtnDelete);
		this.splitContainer3.Panel1.Controls.Add(this.ucBtnExport);
		this.splitContainer3.Panel1.Controls.Add(this.ucBtnPrintf);
		this.splitContainer3.Panel1.Controls.Add(this.ucBtnStatic);
		this.splitContainer3.Panel1.Controls.Add(this.ucDP1);
		this.splitContainer3.Panel1.Controls.Add(this.ucCBSites);
		this.splitContainer3.Panel1.Controls.Add(this.ucDP2);
		this.splitContainer3.Panel2.Controls.Add(this.tabControlExt2);
		this.splitContainer3.Size = new System.Drawing.Size(792, 517);
		this.splitContainer3.SplitterDistance = 132;
		this.splitContainer3.TabIndex = 5;
		this.ucBtnOpenSda.BackColor = System.Drawing.Color.White;
		this.ucBtnOpenSda.BtnBackColor = System.Drawing.Color.White;
		this.ucBtnOpenSda.BtnFont = new System.Drawing.Font("微软雅黑", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		this.ucBtnOpenSda.BtnForeColor = System.Drawing.Color.White;
		this.ucBtnOpenSda.BtnText = "查看谱图";
		this.ucBtnOpenSda.ConerRadius = 30;
		this.ucBtnOpenSda.Cursor = System.Windows.Forms.Cursors.Hand;
		this.ucBtnOpenSda.EnabledMouseEffect = true;
		this.ucBtnOpenSda.FillColor = System.Drawing.Color.FromArgb(255, 77, 59);
		this.ucBtnOpenSda.Font = new System.Drawing.Font("微软雅黑", 15f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
		this.ucBtnOpenSda.IsRadius = true;
		this.ucBtnOpenSda.IsShowRect = true;
		this.ucBtnOpenSda.IsShowTips = false;
		this.ucBtnOpenSda.Location = new System.Drawing.Point(673, 66);
		this.ucBtnOpenSda.Margin = new System.Windows.Forms.Padding(0);
		this.ucBtnOpenSda.Name = "ucBtnOpenSda";
		this.ucBtnOpenSda.RectColor = System.Drawing.Color.FromArgb(255, 77, 58);
		this.ucBtnOpenSda.RectWidth = 1;
		this.ucBtnOpenSda.Size = new System.Drawing.Size(86, 32);
		this.ucBtnOpenSda.TabIndex = 8;
		this.ucBtnOpenSda.TabStop = false;
		this.ucBtnOpenSda.TipsColor = System.Drawing.Color.FromArgb(232, 30, 99);
		this.ucBtnOpenSda.TipsText = "";
		this.ucBtnOpenSda.BtnClick += new System.EventHandler(ucBtnOpenSda_BtnClick);
		this.ucBtnDelete.BackColor = System.Drawing.Color.White;
		this.ucBtnDelete.BtnBackColor = System.Drawing.Color.White;
		this.ucBtnDelete.BtnFont = new System.Drawing.Font("微软雅黑", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		this.ucBtnDelete.BtnForeColor = System.Drawing.Color.White;
		this.ucBtnDelete.BtnText = "删除采样点数据";
		this.ucBtnDelete.ConerRadius = 30;
		this.ucBtnDelete.Cursor = System.Windows.Forms.Cursors.Hand;
		this.ucBtnDelete.EnabledMouseEffect = true;
		this.ucBtnDelete.FillColor = System.Drawing.Color.FromArgb(255, 77, 59);
		this.ucBtnDelete.Font = new System.Drawing.Font("微软雅黑", 15f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
		this.ucBtnDelete.IsRadius = true;
		this.ucBtnDelete.IsShowRect = true;
		this.ucBtnDelete.IsShowTips = false;
		this.ucBtnDelete.Location = new System.Drawing.Point(408, 66);
		this.ucBtnDelete.Margin = new System.Windows.Forms.Padding(0);
		this.ucBtnDelete.Name = "ucBtnDelete";
		this.ucBtnDelete.RectColor = System.Drawing.Color.FromArgb(255, 77, 58);
		this.ucBtnDelete.RectWidth = 1;
		this.ucBtnDelete.Size = new System.Drawing.Size(138, 32);
		this.ucBtnDelete.TabIndex = 7;
		this.ucBtnDelete.TabStop = false;
		this.ucBtnDelete.TipsColor = System.Drawing.Color.FromArgb(232, 30, 99);
		this.ucBtnDelete.TipsText = "";
		this.ucBtnDelete.BtnClick += new System.EventHandler(ucBtnDelete_BtnClick);
		this.ucBtnExport.BackColor = System.Drawing.Color.White;
		this.ucBtnExport.BtnBackColor = System.Drawing.Color.White;
		this.ucBtnExport.BtnFont = new System.Drawing.Font("微软雅黑", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		this.ucBtnExport.BtnForeColor = System.Drawing.Color.White;
		this.ucBtnExport.BtnText = "导出数据";
		this.ucBtnExport.ConerRadius = 30;
		this.ucBtnExport.Cursor = System.Windows.Forms.Cursors.Hand;
		this.ucBtnExport.EnabledMouseEffect = true;
		this.ucBtnExport.FillColor = System.Drawing.Color.FromArgb(255, 77, 59);
		this.ucBtnExport.Font = new System.Drawing.Font("微软雅黑", 15f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
		this.ucBtnExport.IsRadius = true;
		this.ucBtnExport.IsShowRect = true;
		this.ucBtnExport.IsShowTips = false;
		this.ucBtnExport.Location = new System.Drawing.Point(570, 66);
		this.ucBtnExport.Margin = new System.Windows.Forms.Padding(0);
		this.ucBtnExport.Name = "ucBtnExport";
		this.ucBtnExport.RectColor = System.Drawing.Color.FromArgb(255, 77, 58);
		this.ucBtnExport.RectWidth = 1;
		this.ucBtnExport.Size = new System.Drawing.Size(86, 32);
		this.ucBtnExport.TabIndex = 6;
		this.ucBtnExport.TabStop = false;
		this.ucBtnExport.TipsColor = System.Drawing.Color.FromArgb(232, 30, 99);
		this.ucBtnExport.TipsText = "";
		this.ucBtnExport.BtnClick += new System.EventHandler(ucBtnExport_BtnClick);
		this.ucBtnPrintf.BackColor = System.Drawing.Color.White;
		this.ucBtnPrintf.BtnBackColor = System.Drawing.Color.White;
		this.ucBtnPrintf.BtnFont = new System.Drawing.Font("微软雅黑", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		this.ucBtnPrintf.BtnForeColor = System.Drawing.Color.White;
		this.ucBtnPrintf.BtnText = "打印数据";
		this.ucBtnPrintf.ConerRadius = 30;
		this.ucBtnPrintf.Cursor = System.Windows.Forms.Cursors.Hand;
		this.ucBtnPrintf.EnabledMouseEffect = true;
		this.ucBtnPrintf.FillColor = System.Drawing.Color.FromArgb(255, 77, 59);
		this.ucBtnPrintf.Font = new System.Drawing.Font("微软雅黑", 15f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
		this.ucBtnPrintf.IsRadius = true;
		this.ucBtnPrintf.IsShowRect = true;
		this.ucBtnPrintf.IsShowTips = false;
		this.ucBtnPrintf.Location = new System.Drawing.Point(673, 17);
		this.ucBtnPrintf.Margin = new System.Windows.Forms.Padding(0);
		this.ucBtnPrintf.Name = "ucBtnPrintf";
		this.ucBtnPrintf.RectColor = System.Drawing.Color.FromArgb(255, 77, 58);
		this.ucBtnPrintf.RectWidth = 1;
		this.ucBtnPrintf.Size = new System.Drawing.Size(86, 32);
		this.ucBtnPrintf.TabIndex = 5;
		this.ucBtnPrintf.TabStop = false;
		this.ucBtnPrintf.TipsColor = System.Drawing.Color.FromArgb(232, 30, 99);
		this.ucBtnPrintf.TipsText = "";
		this.ucBtnPrintf.BtnClick += new System.EventHandler(ucBtnPrintf_BtnClick);
		this.ucBtnStatic.BackColor = System.Drawing.Color.White;
		this.ucBtnStatic.BtnBackColor = System.Drawing.Color.White;
		this.ucBtnStatic.BtnFont = new System.Drawing.Font("微软雅黑", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		this.ucBtnStatic.BtnForeColor = System.Drawing.Color.White;
		this.ucBtnStatic.BtnText = "统计数据";
		this.ucBtnStatic.ConerRadius = 30;
		this.ucBtnStatic.Cursor = System.Windows.Forms.Cursors.Hand;
		this.ucBtnStatic.EnabledMouseEffect = true;
		this.ucBtnStatic.FillColor = System.Drawing.Color.FromArgb(255, 77, 59);
		this.ucBtnStatic.Font = new System.Drawing.Font("微软雅黑", 15f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
		this.ucBtnStatic.IsRadius = true;
		this.ucBtnStatic.IsShowRect = true;
		this.ucBtnStatic.IsShowTips = false;
		this.ucBtnStatic.Location = new System.Drawing.Point(570, 17);
		this.ucBtnStatic.Margin = new System.Windows.Forms.Padding(0);
		this.ucBtnStatic.Name = "ucBtnStatic";
		this.ucBtnStatic.RectColor = System.Drawing.Color.FromArgb(255, 77, 58);
		this.ucBtnStatic.RectWidth = 1;
		this.ucBtnStatic.Size = new System.Drawing.Size(86, 32);
		this.ucBtnStatic.TabIndex = 4;
		this.ucBtnStatic.TabStop = false;
		this.ucBtnStatic.TipsColor = System.Drawing.Color.FromArgb(232, 30, 99);
		this.ucBtnStatic.TipsText = "";
		this.ucBtnStatic.BtnClick += new System.EventHandler(ucBtnStatic_BtnClick);
		this.ucDP1.BackColor = System.Drawing.Color.White;
		this.ucDP1.ConerRadius = 5;
		this.ucDP1.CurrentTime = new System.DateTime(2020, 10, 3, 10, 0, 41, 0);
		this.ucDP1.FillColor = System.Drawing.Color.Transparent;
		this.ucDP1.Font = new System.Drawing.Font("微软雅黑", 15f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
		this.ucDP1.IsRadius = true;
		this.ucDP1.IsShowRect = true;
		this.ucDP1.Location = new System.Drawing.Point(19, 17);
		this.ucDP1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
		this.ucDP1.Name = "ucDP1";
		this.ucDP1.Padding = new System.Windows.Forms.Padding(0, 10, 0, 0);
		this.ucDP1.RectColor = System.Drawing.Color.FromArgb(220, 220, 220);
		this.ucDP1.RectWidth = 1;
		this.ucDP1.Size = new System.Drawing.Size(336, 39);
		this.ucDP1.TabIndex = 2;
		this.ucDP1.TimeFontSize = 20;
		this.ucDP1.TimeType = HZH_Controls.Controls.DateTimePickerType.DateTime;
		this.ucCBSites.BackColor = System.Drawing.Color.Transparent;
		this.ucCBSites.BackColorExt = System.Drawing.Color.FromArgb(240, 240, 240);
		this.ucCBSites.BoxStyle = System.Windows.Forms.ComboBoxStyle.DropDown;
		this.ucCBSites.ConerRadius = 5;
		this.ucCBSites.DropPanelHeight = -1;
		this.ucCBSites.FillColor = System.Drawing.Color.White;
		this.ucCBSites.Font = new System.Drawing.Font("微软雅黑", 12f);
		this.ucCBSites.IsRadius = true;
		this.ucCBSites.IsShowRect = true;
		this.ucCBSites.ItemWidth = 70;
		this.ucCBSites.Location = new System.Drawing.Point(373, 17);
		this.ucCBSites.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
		this.ucCBSites.Name = "ucCBSites";
		this.ucCBSites.RectColor = System.Drawing.Color.FromArgb(220, 220, 220);
		this.ucCBSites.RectWidth = 1;
		this.ucCBSites.SelectedIndex = -1;
		this.ucCBSites.SelectedValue = "";
		this.ucCBSites.Size = new System.Drawing.Size(173, 32);
		this.ucCBSites.Source = null;
		this.ucCBSites.TabIndex = 1;
		this.ucCBSites.TextValue = null;
		this.ucCBSites.TriangleColor = System.Drawing.Color.FromArgb(255, 77, 59);
		this.ucCBSites.SelectedChangedEvent += new System.EventHandler(ucCBSites_SelectedChangedEvent);
		this.ucDP2.BackColor = System.Drawing.Color.White;
		this.ucDP2.ConerRadius = 5;
		this.ucDP2.CurrentTime = new System.DateTime(2020, 10, 3, 10, 0, 41, 0);
		this.ucDP2.FillColor = System.Drawing.Color.Transparent;
		this.ucDP2.Font = new System.Drawing.Font("微软雅黑", 15f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
		this.ucDP2.IsRadius = true;
		this.ucDP2.IsShowRect = true;
		this.ucDP2.Location = new System.Drawing.Point(18, 66);
		this.ucDP2.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
		this.ucDP2.Name = "ucDP2";
		this.ucDP2.Padding = new System.Windows.Forms.Padding(0, 10, 0, 0);
		this.ucDP2.RectColor = System.Drawing.Color.FromArgb(220, 220, 220);
		this.ucDP2.RectWidth = 1;
		this.ucDP2.Size = new System.Drawing.Size(336, 39);
		this.ucDP2.TabIndex = 3;
		this.ucDP2.TimeFontSize = 20;
		this.ucDP2.TimeType = HZH_Controls.Controls.DateTimePickerType.DateTime;
		this.tabControlExt2.Alignment = System.Windows.Forms.TabAlignment.Left;
		this.tabControlExt2.CloseBtnColor = System.Drawing.Color.FromArgb(255, 85, 51);
		this.tabControlExt2.Controls.Add(this.tabPage5);
		this.tabControlExt2.Controls.Add(this.tabPage8);
		this.tabControlExt2.Dock = System.Windows.Forms.DockStyle.Fill;
		this.tabControlExt2.ImageList = this.imageList1;
		this.tabControlExt2.IsShowCloseBtn = false;
		this.tabControlExt2.ItemSize = new System.Drawing.Size(0, 50);
		this.tabControlExt2.Location = new System.Drawing.Point(0, 0);
		this.tabControlExt2.Multiline = true;
		this.tabControlExt2.Name = "tabControlExt2";
		this.tabControlExt2.SelectedIndex = 0;
		this.tabControlExt2.Size = new System.Drawing.Size(792, 381);
		this.tabControlExt2.TabIndex = 4;
		this.tabControlExt2.UncloseTabIndexs = null;
		this.tabPage5.Controls.Add(this.ucDGHistory);
		this.tabPage5.Location = new System.Drawing.Point(54, 4);
		this.tabPage5.Name = "tabPage5";
		this.tabPage5.Padding = new System.Windows.Forms.Padding(3);
		this.tabPage5.Size = new System.Drawing.Size(734, 373);
		this.tabPage5.TabIndex = 0;
		this.tabPage5.Text = "历史数据";
		this.tabPage5.UseVisualStyleBackColor = true;
		this.ucDGHistory.BackColor = System.Drawing.Color.White;
		this.ucDGHistory.Columns = null;
		this.ucDGHistory.DataSource = null;
		this.ucDGHistory.Dock = System.Windows.Forms.DockStyle.Fill;
		this.ucDGHistory.HeadFont = new System.Drawing.Font("微软雅黑", 12f);
		this.ucDGHistory.HeadHeight = 40;
		this.ucDGHistory.HeadPadingLeft = 0;
		this.ucDGHistory.HeadTextColor = System.Drawing.Color.Black;
		this.ucDGHistory.IsShowCheckBox = false;
		this.ucDGHistory.IsShowHead = true;
		this.ucDGHistory.Location = new System.Drawing.Point(3, 3);
		this.ucDGHistory.Name = "ucDGHistory";
		this.ucDGHistory.Padding = new System.Windows.Forms.Padding(0, 40, 0, 0);
		this.ucDGHistory.RowHeight = 40;
		this.ucDGHistory.RowType = typeof(HZH_Controls.Controls.UCDataGridViewRow);
		this.ucDGHistory.Size = new System.Drawing.Size(728, 367);
		this.ucDGHistory.TabIndex = 0;
		this.tabPage8.Controls.Add(this.ucDGStatistics);
		this.tabPage8.Location = new System.Drawing.Point(54, 4);
		this.tabPage8.Name = "tabPage8";
		this.tabPage8.Padding = new System.Windows.Forms.Padding(3);
		this.tabPage8.Size = new System.Drawing.Size(734, 373);
		this.tabPage8.TabIndex = 1;
		this.tabPage8.Text = "统计结果";
		this.tabPage8.UseVisualStyleBackColor = true;
		this.ucDGStatistics.BackColor = System.Drawing.Color.White;
		this.ucDGStatistics.Columns = null;
		this.ucDGStatistics.DataSource = null;
		this.ucDGStatistics.Dock = System.Windows.Forms.DockStyle.Fill;
		this.ucDGStatistics.HeadFont = new System.Drawing.Font("微软雅黑", 10f);
		this.ucDGStatistics.HeadHeight = 40;
		this.ucDGStatistics.HeadPadingLeft = 0;
		this.ucDGStatistics.HeadTextColor = System.Drawing.Color.Black;
		this.ucDGStatistics.IsShowCheckBox = false;
		this.ucDGStatistics.IsShowHead = true;
		this.ucDGStatistics.Location = new System.Drawing.Point(3, 3);
		this.ucDGStatistics.Name = "ucDGStatistics";
		this.ucDGStatistics.Padding = new System.Windows.Forms.Padding(0, 40, 0, 0);
		this.ucDGStatistics.RowHeight = 40;
		this.ucDGStatistics.RowType = typeof(HZH_Controls.Controls.UCDataGridViewRow);
		this.ucDGStatistics.Size = new System.Drawing.Size(728, 367);
		this.ucDGStatistics.TabIndex = 0;
		this.tabPage1.Controls.Add(this.insDeviceCtrl1);
		this.tabPage1.Location = new System.Drawing.Point(4, 4);
		this.tabPage1.Name = "tabPage1";
		this.tabPage1.Size = new System.Drawing.Size(792, 517);
		this.tabPage1.TabIndex = 2;
		this.tabPage1.Text = "仪器参数";
		this.tabPage1.UseVisualStyleBackColor = true;
		this.insDeviceCtrl1.Dock = System.Windows.Forms.DockStyle.Fill;
		this.insDeviceCtrl1.Location = new System.Drawing.Point(0, 0);
		this.insDeviceCtrl1.Margin = new System.Windows.Forms.Padding(4);
		this.insDeviceCtrl1.Name = "insDeviceCtrl1";
		this.insDeviceCtrl1.ShowVOCMode = false;
		this.insDeviceCtrl1.Size = new System.Drawing.Size(792, 517);
		this.insDeviceCtrl1.TabIndex = 2;
		this.labBat.AutoSize = true;
		this.labBat.ForeColor = System.Drawing.Color.White;
		this.labBat.Location = new System.Drawing.Point(581, 6);
		this.labBat.Name = "labBat";
		this.labBat.Size = new System.Drawing.Size(29, 12);
		this.labBat.TabIndex = 32;
		this.labBat.Text = "电池";
		this.printDocument1.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(printDocument1_PrintPage);
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
		this.picBoxFire.BackColor = System.Drawing.Color.Transparent;
		this.picBoxFire.Cursor = System.Windows.Forms.Cursors.Hand;
		this.picBoxFire.Image = IBrainChrom2018.Properties.Resources.gas_50px;
		this.picBoxFire.Location = new System.Drawing.Point(345, 238);
		this.picBoxFire.Name = "picBoxFire";
		this.picBoxFire.Size = new System.Drawing.Size(57, 51);
		this.picBoxFire.TabIndex = 97;
		this.picBoxFire.TabStop = false;
		this.imageList2.ImageStream = (System.Windows.Forms.ImageListStreamer)resources.GetObject("imageList2.ImageStream");
		this.imageList2.TransparentColor = System.Drawing.Color.Transparent;
		this.imageList2.Images.SetKeyName(0, "folder_52px.png");
		this.imageList2.Images.SetKeyName(1, "opened_folder_52px.png");
		this.imageList2.Images.SetKeyName(2, "gas_50px.png");
		this.imageList2.Images.SetKeyName(3, "gas_50px灰.png");
		base.AutoScaleDimensions = new System.Drawing.SizeF(96f, 96f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
		this.BackColor = System.Drawing.Color.OrangeRed;
		base.ClientSize = new System.Drawing.Size(800, 600);
		base.Controls.Add(this.labBat);
		base.Controls.Add(this.tabControlExt1);
		base.Controls.Add(this.toolStrip1);
		base.Controls.Add(this.splitContainer1);
		base.Controls.Add(this.menuStrip1);
		base.Controls.Add(this.statusStrip1);
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
		base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
		base.MainMenuStrip = this.menuStrip1;
		base.Name = "FormMainPortable";
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.Text = "IBrainChrom";
		base.FormClosing += new System.Windows.Forms.FormClosingEventHandler(FormMain_FormClosing);
		base.Load += new System.EventHandler(FormMain_Load);
		base.KeyDown += new System.Windows.Forms.KeyEventHandler(FormMain_KeyDown);
		this.splitContainer1.Panel1.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.splitContainer1).EndInit();
		this.splitContainer1.ResumeLayout(false);
		this.splitContainer2.Panel1.ResumeLayout(false);
		this.splitContainer2.Panel2.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.splitContainer2).EndInit();
		this.splitContainer2.ResumeLayout(false);
		this.tabChannel.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.spcLeftBottom).EndInit();
		this.spcLeftBottom.ResumeLayout(false);
		this.cmPeakInfo.ResumeLayout(false);
		this.cmsIntegration.ResumeLayout(false);
		this.menuStrip1.ResumeLayout(false);
		this.menuStrip1.PerformLayout();
		this.statusStrip1.ResumeLayout(false);
		this.statusStrip1.PerformLayout();
		this.tabControlExt1.ResumeLayout(false);
		this.tabPage3.ResumeLayout(false);
		this.tabPage3.PerformLayout();
		this.tabPage4.ResumeLayout(false);
		this.tabControl1.ResumeLayout(false);
		this.tabPage6.ResumeLayout(false);
		this.tabPage6.PerformLayout();
		this.ucPTSys.ResumeLayout(false);
		this.tabPage7.ResumeLayout(false);
		this.tabPage2.ResumeLayout(false);
		this.splitContainer3.Panel1.ResumeLayout(false);
		this.splitContainer3.Panel2.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.splitContainer3).EndInit();
		this.splitContainer3.ResumeLayout(false);
		this.tabControlExt2.ResumeLayout(false);
		this.tabPage5.ResumeLayout(false);
		this.tabPage8.ResumeLayout(false);
		this.tabPage1.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.picBoxFire).EndInit();
		base.ResumeLayout(false);
		base.PerformLayout();
	}

	object ChromFormInterface.Invoke(Delegate method)
	{
		return Invoke(method);
	}
}
