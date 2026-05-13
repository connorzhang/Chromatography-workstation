using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using IBrainChrom2018.Unit;

namespace IBrainChrom2018;

public class TcpServerSocket
{
	private struct Struct0
	{
		public int int_0;

		public float float_0;
	}

	[CompilerGenerated]
	private sealed class Class12
	{
		public int int_0;

		public AccStyle accStyle_0;

		public byte[] byte_0;

		public string string_0;

		public int int_1;

		public TcpServerSocket tcpServerSocket_0;

		public void method_0()
		{
			int_1 = tcpServerSocket_0.mForm.CurrentChannelIndex;
		}
	}

	public CH4Param cH4Param = CH4Param.Create();

	public int cntSeq1 = 1;

	public int cntSeq2 = 1;

	public int cntSeq3 = 1;

	public int cntSeq4 = 1;

	public byte eventState = 0;

	public bool channel1Ready = false;

	public bool channel2Ready = false;

	public bool channel3Ready = false;

	public string channel1File = null;

	public string channel2File = null;

	public string channel3File = null;

	public bool bError = false;

	public bool bHighVed1 = false;

	public bool bHighVed2 = false;

	public double fPress;

	public string[] strEPCCru = new string[5];

	public byte[] bArrCheck = new byte[100]
	{
		239, 50, 86, 7, 7, 136, 3, 5, 6, 7,
		52, 9, 52, 35, 69, 71, 147, 17, 105, 69,
		50, 86, 35, 50, 17, 120, 9, 152, 68, 51,
		70, 40, 25, 67, 173, 228, 147, 17, 35, 69,
		52, 34, 52, 1, 69, 71, 147, 17, 105, 69,
		52, 22, 78, 35, 23, 71, 147, 17, 105, 69,
		59, 9, 52, 35, 244, 71, 147, 17, 105, 69,
		9, 11, 52, 35, 222, 71, 147, 56, 105, 69,
		52, 9, 52, 35, 69, 71, 147, 17, 105, 69,
		52, 9, 52, 35, 64, 223, 16, 253, 1, 222
	};

	public float[] fTemp1 = new float[5];

	private int iTempS1 = 0;

	public float[] fTemp2 = new float[5];

	private int iTempS2 = 0;

	public float[] fTemp3 = new float[5];

	private int iTempS3 = 0;

	public float[] fTemp4 = new float[5];

	private int iTempS4 = 0;

	public float[] fTemp5 = new float[5];

	private int iTempS5 = 0;

	public float[] fTemp6 = new float[5];

	private int iTempS6 = 0;

	public float[] fTemp7 = new float[5];

	private int iTempS7 = 0;

	public float[] fTemp8 = new float[5];

	private int iTempS8 = 0;

	public uint iVBat = 0u;

	private const int int_0 = 20480;

	private const int int_1 = 20480;

	public string fpdHighValue = "0";

	public byte indexFPDHIGHV = 1;

	public bool Bfire1 = false;

	public bool Bfire2 = true;

	public bool Ready = false;

	public int class30Chn;

	public bool DisConnect;

	public float fFireOn = 10000f;

	public float fFireOn2 = 10000f;

	public bool PhoneHelp;

	public int FPDhighV;

	public Chromatogram bgChrom;

	public Chromatogram bgChromModbus;

	public Chromatogram[] ModBusbgChrom = new Chromatogram[4];

	public string Printpath = "";

	public bool IsComClient;

	private SerialPortClient serialPortClient_0;

	public Socket _socket;

	private string string_0 = "";

	private int int_2 = 100;

	private string string_1 = "";

	private string string_2 = "";

	private int int_3;

	private VikiDataWindowMate vikiDataWindowMate_0;

	private byte[] byte_0;

	public DtC_Channel[] dtc_Channels;

	private DetectorParse[] detectorParseList;

	public Signal[] sglsSampling = new Signal[4];

	public int beginIdleTC;

	public float idle_time;

	public float sample_time;

	public bool sampling;

	public DateTime StartReceiveTime = DateTime.Now;

	public int TempProgram = 8;

	public FIDSet gcHardFrm;

	public bool ControlTemp;

	public bool BControlTemp;

	public bool AlalyseStatus;

	private static int int_4 = 0;

	public static List<CmdItem> lsItems = new List<CmdItem>();

	private short short_0;

	private Class44 class44_0;

	private DateTime dateTime_0 = DateTime.Now;

	private int int_5 = 6;

	private int int_6 = -1;

	private float float_0;

	private Struct0 struct0_0 = default(Struct0);

	public static bool m_bStopFlag = false;

	public ChromFormInterface mForm;

	private SystemParam sysParam = SystemParam.Create();

	private ChromDeviceListMgr cdlMgr = ChromDeviceListMgr.Create();

	private FormMainParam frmParam = FormMainParam.Create();

	public bool bAutoCalibra = false;

	private CalibraParam calibraParam = CalibraParam.Create();

	public int iCollectTimes;

	public int iLevel;

	public int cntCalibra = 0;

	public string strCalibraDir;

	public string strCaliChannel1;

	public string strCaliChannel2;

	public StringBuilder strDebugLog = new StringBuilder();

	public List<Signal> SignalList => sglsSampling.ToList();

	public List<DtC_Channel> Dtc_Channels => dtc_Channels.ToList();

	[TypeConverter(typeof(ExpandableObjectConverter))]
	[EditorBrowsable(EditorBrowsableState.Always)]
	public VikiDataWindowMate VikiDataWindowMate => vikiDataWindowMate_0;

	public List<CmdItem> CmdListItems => lsItems;

	[TypeConverter(typeof(ExpandableObjectConverter))]
	[EditorBrowsable(EditorBrowsableState.Always)]
	public InsDeviceManager devManager0 => cdlMgr.CurrentInsDeviceMgr;

	public InsDeviceManager devManager1 => cdlMgr.CurrentInsDeviceMgr;

	public string DebugLog => strDebugLog.ToString();

	public byte[] Buffer => byte_0;

	public float StartTime => devManager1.tempHoldTime;

	public bool AutoQ => devManager1.injectSpendTime == 16f;

	public string ID
	{
		get
		{
			return string_0;
		}
		set
		{
			string_0 = value;
		}
	}

	public int DID
	{
		get
		{
			return int_2;
		}
		set
		{
			int_2 = value;
		}
	}

	[TypeConverter(typeof(ExpandableObjectConverter))]
	[EditorBrowsable(EditorBrowsableState.Always)]
	public Socket Socket => _socket;

	public string ClientIP => string_2;

	public void refreshSocket(Socket Ns)
	{
		_socket = Ns;
	}

	public TcpServerSocket(Socket socket, ChromFormInterface form)
		: this(socket, 20480)
	{
		if (socket != null)
		{
			string_2 = ((IPEndPoint)socket.RemoteEndPoint).Address.ToString();
		}
		mForm = form;
		vikiDataWindowMate_0 = new VikiDataWindowMate(30720);
		short_0 = 0;
		class44_0 = new Class44();
		bgChrom = new Chromatogram();
		struct0_0.int_0 = -1;
	}

	public void LogIn(ChromFormInterface form, DtC_Channel[] dtc_Channels)
	{
		LogMgr.Instance.Write2RunLog("TcpServerSocket.LogIn");
		mForm = form;
		this.dtc_Channels = dtc_Channels;
		int_4++;
		Initializesgls();
	}

	public void Initializesgls()
	{
		Array.Resize(ref sglsSampling, dtc_Channels.Length);
		float num = 0f;
		float num2 = 0.1f;
		for (int i = 0; i < dtc_Channels.Length; i++)
		{
			if (dtc_Channels[i] is DtC_Detector)
			{
				num += 0.23f;
				num2 += 0.1f;
			}
			dtc_Channels[i].Foo(i);
			if (sglsSampling[i] == null)
			{
				sglsSampling[i] = new Signal();
			}
			sglsSampling[i].detectorStyle = dtc_Channels[i].detectorStyle;
			sglsSampling[i].detector_name = dtc_Channels[i].name;
			sglsSampling[i].sname = "TTTTTTT";
			sglsSampling[i].simple = false;
			GcProgTemp gcProgTemp = new GcProgTemp();
			LcGradient lcGradient = new LcGradient();
			gcProgTemp.Init();
			lcGradient.Init();
			sglsSampling[i].linkLcGradient = lcGradient;
			sglsSampling[i].linkGcProgTemp = gcProgTemp;
		}
		for (int j = 0; j < sglsSampling.Length; j++)
		{
			sglsSampling[j].instruMark = j;
			sglsSampling[j].detectorMark = j;
		}
		beginIdleTC = Environment.TickCount;
	}

	public void LogOut()
	{
		int_4--;
		int_4 = Math.Max(0, int_4);
		LogMgr.Instance.Write2RunLog("TcpServerSocket.LogOut");
	}

	public void StoptAllGather()
	{
		int iChannel;
		for (iChannel = 0; iChannel < sglsSampling.Length; iChannel++)
		{
			string saveFilePath = "";
			try
			{
				saveFilePath = GetChromFileName(iChannel);
			}
			catch (Exception ex)
			{
				LogMgr.Instance.LogError(ex.Message);
			}
			if (bgChrom.userArchives.Length == 0)
			{
				Array.Resize(ref bgChrom.userArchives, 1);
				bgChrom.userArchives[0] = new UserArchive();
			}
			bgChrom.signal = sglsSampling[iChannel];
			string name = dtc_Channels[iChannel].name;
			if (bgChrom.signal.simple)
			{
				Save(sglsSampling[iChannel], saveFilePath, cdlMgr.GetChartParaOpera(ID, iChannel), iChannel, cdlMgr.GetChromDevice(ID).info.Name);
				sglsSampling[iChannel].ResetOriDots(createDiskFile: true);
				sglsSampling[iChannel].ClearPeak();
			}
			InitSignalDilg(iChannel, AllChannels: true);
			mForm.Invoke((MethodInvoker)delegate
			{
				if (mForm.CurrentGCID == ID && iChannel == mForm.CurrentChannelIndex)
				{
					mForm.chrAcqCtrl.tsStart.Enabled = true;
					mForm.chrAcqCtrl.tsstop.Enabled = false;
				}
			});
		}
	}

	public void OpenCom()
	{
		if (IsComClient)
		{
			serialPortClient_0 = new SerialPortClient();
			serialPortClient_0.Open(sysParam.ChromComNumberString);
			serialPortClient_0.OnDataRecivedFromCom += method_1;
		}
	}

	public void CloseCom()
	{
		if (IsComClient)
		{
			serialPortClient_0.Close();
		}
	}

	private void method_1(byte[] byte_1)
	{
		try
		{
			OneDataReceive(byte_1, byte_1.Length);
			mForm.CompServer_OnReceiveData(ID);
		}
		catch (Exception)
		{
		}
	}

	private short CheckDataBuffStartFlag(byte[] byte_1, short short_1)
	{
		if (byte_1 == null)
		{
			return -1;
		}
		for (int i = short_1; i < byte_1.Length; i++)
		{
			if (byte_1[i] == 71 && byte_1[i + 1] == 67 && byte_1[i + 2] == 75 && byte_1[i + 3] == 67)
			{
				return (short)i;
			}
		}
		return -1;
	}

	public TcpServerSocket(Socket socket, int buffersize)
	{
		if (IsComClient)
		{
		}
		_socket = socket;
		byte_0 = new byte[buffersize];
		fFireOn = frmParam.fFireOn;
		fFireOn2 = frmParam.fFireOn2;
	}

	public void OneDataReceive(byte[] Buffer, int length)
	{
		if (m_bStopFlag)
		{
			return;
		}
		try
		{
			StartReceiveTime = DateTime.Now;
			vikiDataWindowMate_0.AppendBlock(Buffer, length);
			short num = CheckDataBuffStartFlag(vikiDataWindowMate_0.DataBuff, 0);
			while (num >= 0)
			{
				short num2;
				try
				{
					num2 = (short)(vikiDataWindowMate_0.DataBuff[num + 4] * 256 + vikiDataWindowMate_0.DataBuff[num + 5] + 7);
					if (num + num2 > vikiDataWindowMate_0.DataSize)
					{
						num2 = -1;
					}
				}
				catch (Exception)
				{
					num2 = -1;
				}
				if (num2 > 0)
				{
					try
					{
						byte[] array = new byte[num2];
						Array.Copy(vikiDataWindowMate_0.DataBuff, num, array, 0, num2);
						byte b = IBrainConvert.BitByBitNo(array, 6, array.Length - 6 - 1);
						string text = "0x";
						if (array.Length >= 24)
						{
							text += BitConverter.ToString(new byte[1] { array[24] });
						}
						if (b != array[array.Length - 1])
						{
							Console.WriteLine("检验失败");
						}
						else
						{
							AnalyseReceivedData(array);
						}
					}
					catch (Exception ex2)
					{
						Console.WriteLine(ex2.Message + ex2.StackTrace);
					}
					num = CheckDataBuffStartFlag(vikiDataWindowMate_0.DataBuff, (short)(num + num2));
					if (num < 0)
					{
						vikiDataWindowMate_0.Clean();
					}
				}
				else
				{
					vikiDataWindowMate_0.MoveData(num);
					num = -1;
				}
			}
		}
		catch
		{
		}
	}

	private byte[] AnalyseReceivedData(byte[] byte_1)
	{
		if (m_bStopFlag)
		{
			return new byte[1];
		}
		Class12 @class = new Class12();
		@class.tcpServerSocket_0 = this;
		string text = Encoding.ASCII.GetString(byte_1, 6, 16);
		if (byte_1[6] == 0)
		{
			return null;
		}
		if (text.Trim() == "")
		{
			return null;
		}
		if (text.Trim() == "709131284A484845")
		{
			text = string_0;
		}
		if (string_0.Trim() == "")
		{
			string_0 = Encoding.ASCII.GetString(byte_1, 6, 16);
			Class49.InsertIntoTable(Class49.string_9[0], Class49.user_0.u_name, ID, "新色谱仪连接", "新色谱仪连接,IP" + ClientIP);
			LogMgr.Instance.Write2RunLog("TcpServerSocket.AnalyseReceivedData: 新色谱仪连接:" + ClientIP);
		}
		else if (string_0 != text)
		{
			vikiDataWindowMate_0 = new VikiDataWindowMate(30720);
			short_0 = 0;
			class44_0 = new Class44();
			bgChrom = new Chromatogram();
			struct0_0.int_0 = -1;
			DtC_Channel[] array = new DtC_Channel[4]
			{
				new DtC_Channel(new SysCfgControl()),
				new DtC_Channel(new SysCfgControl()),
				new DtC_Channel(new SysCfgControl()),
				new DtC_Channel(new SysCfgControl())
			};
			sglsSampling = new Signal[0];
			int_2 = -1;
			LogIn(mForm, array);
			string_0 = text;
			Class49.InsertIntoTable(Class49.string_9[0], Class49.user_0.u_name, ID, "新色谱仪连接", "新色谱仪连接,IP" + ClientIP);
			LogMgr.Instance.Write2RunLog("TcpServerSocket.AnalyseReceivedData: 新色谱仪连接:" + ClientIP);
		}
		if (int_2 == -1)
		{
			int_2 = mForm.FrmEquip.GetModBusDeviceIDByEquipID(string_0);
			bgChrom.chromInfo.cclDescription = mForm.FrmEquip.GetNameByID(string_0);
		}
		string nameByID = mForm.FrmEquip.GetNameByID(string_0);
		byte b = byte_1[24];
		@class.accStyle_0 = AccStyle.Read;
		@class.byte_0 = IBrainConvert.ArrayCopy(byte_1, 25, byte_1.Length - 26);
		@class.string_0 = "";
		@class.int_1 = 0;
		mForm.Invoke((MethodInvoker)delegate
		{
			@class.int_1 = @class.tcpServerSocket_0.mForm.CurrentChannelIndex;
		});
		byte b2 = b;
		if (b2 == 250)
		{
			fFireOn = (int)byte_1[25];
			fFireOn /= 10f;
			fFireOn2 = (int)byte_1[26];
			fFireOn2 /= 10f;
			mForm.Invoke((MethodInvoker)delegate
			{
				ChromDeviceCtrl.selfCtrl.tbFireOn.Text = fFireOn.ToString();
				ChromDeviceCtrl.selfCtrl.tbFireOn2.Text = fFireOn2.ToString();
			});
			@class.string_0 = Lang.PS("点火门限查询应答", "Fire threshold parameter query and response ");
		}
		if (b2 == 251)
		{
			mForm.Invoke((MethodInvoker)delegate
			{
				if (byte_1[26] != 0 && byte_1[25] == 49)
				{
					FormMain.fromMain.tsslMsg.Text = "报警码：" + byte_1[25];
				}
				if (byte_1[25] == 1)
				{
					mForm.StateYiqi = 6;
					bError = true;
					cdlMgr.tcpServerMgr.mComModbus2.WordVaue[1002] = 1;
				}
				if (byte_1[26] != 0)
				{
					mForm.StateYiqi = 2;
					bError = true;
					cdlMgr.tcpServerMgr.mComModbus2.WordVaue[1002] = 1;
				}
				else if (mForm.StateYiqi != 6)
				{
					mForm.StateYiqi = 3;
				}
				if (byte_1[26] == 0 && byte_1[25] != 1)
				{
					bError = false;
					cdlMgr.tcpServerMgr.mComModbus2.WordVaue[1002] = 0;
				}
			});
			@class.string_0 = Lang.PS("色谱仪主动发送异常报警码", "Temperature control parameter query and response ");
		}
		if (b2 == 253)
		{
			FPDhighV = (byte_1[25] << 8) | byte_1[26];
			mForm.Invoke((MethodInvoker)delegate
			{
				if (MicrFPDCtrl.selfCtrl != null)
				{
					if (indexFPDHIGHV == 1)
					{
						MicrFPDCtrl.selfCtrl.tbHighV.Text = FPDhighV.ToString();
					}
					else if (indexFPDHIGHV == 2)
					{
						MicrFPDCtrl.selfCtrl.tbHighV2.Text = FPDhighV.ToString();
					}
				}
				else
				{
					if (gcHardFrm == null)
					{
						gcHardFrm = new FIDSet();
					}
					gcHardFrm.tbHighV.Text = FPDhighV.ToString();
				}
			});
			@class.string_0 = Lang.PS("高压设定值查询应答", "High V parameter query and response ");
			LogMgr.Instance.Write2RunLog("TcpServerSocket.AnalyseReceivedData: 高压设定值查询应答:");
		}
		if (b2 == 128 || b2 == 136)
		{
			devManager1.SetTempSetedList(@class.byte_0);
			Answer128(@class.accStyle_0, devManager1);
			@class.string_0 = Lang.PS("控温参数查询及设置应答", "Temperature control parameter query and response ");
		}
		if (b == 234 || b == 236)
		{
			devManager1.SetTempProtectList(@class.byte_0);
			Answer234(@class.accStyle_0, devManager1);
			@class.string_0 = Lang.PS("控温参数查询及设置应答", "Temperature control parameter query and response");
		}
		if (b == 238 || b == 239)
		{
			devManager1.multivalveEnable = @class.byte_0;
			Answer238();
			@class.string_0 = Lang.PS("多位阀使能查询设置应答", "MultiValve control parameter query and response ");
			LogMgr.Instance.Write2RunLog("TcpServerSocket.AnalyseReceivedData: 多位阀使能查询设置应答:");
		}
		if (b == 129 || b == 137)
		{
			devManager1.SetTempSettingList(@class.byte_0);
			if (@class.byte_0.Length > 50)
			{
				TempProgram = 16;
			}
			else
			{
				TempProgram = 8;
			}
			Answer129(@class.accStyle_0, devManager1);
			@class.string_0 = Lang.PS("程序升温查询设置应答", "Temperature-programmed query set answer  ");
			LogMgr.Instance.Write2RunLog("TcpServerSocket.AnalyseReceivedData: 程序升温查询设置应答:");
		}
		if (b == 130 || b == 138)
		{
			devManager1.SetEventTable0(@class.byte_0);
			Answer130(@class.accStyle_0, devManager1);
			@class.string_0 = Lang.PS("外部事件查询设置应答", "External event query set answer ");
			LogMgr.Instance.Write2RunLog("TcpServerSocket.AnalyseReceivedData: 外部事件查询设置应答:");
		}
		if (b == 228 || b == 229)
		{
			devManager1.SetEventTable1(@class.byte_0);
			Answer228(@class.accStyle_0, devManager1);
			@class.string_0 = Lang.PS("外部事件2查询设置应答", "External event2 query set answer ");
			LogMgr.Instance.Write2RunLog("TcpServerSocket.AnalyseReceivedData: 外部事件2查询设置应答:");
		}
		if (b == 134 || b == 131)
		{
			devManager1.SetExeFileNumber(@class.byte_0);
			@class.string_0 = Lang.PS("执行文件号查询设置应答", "The implementation of document query set answer ");
			LogMgr.Instance.Write2RunLog("TcpServerSocket.AnalyseReceivedData: 执行文件号查询设置应答:");
		}
		if (b == 132 || b == 140)
		{
			devManager1.injectInterval = IBrainConvert.ByteArray2Float(@class.byte_0, 0, 1);
			devManager1.injectNTimes = IBrainConvert.Byte2ToInt(@class.byte_0, 2);
			devManager1.injectSpendTime = (int)@class.byte_0[4];
			devManager1.injectLightTime = (int)@class.byte_0[5];
			Answer132();
			@class.string_0 = Lang.PS("自动进样时间查询设置应答", "Automatic sampling time query set answer ");
			LogMgr.Instance.Write2RunLog("TcpServerSocket.AnalyseReceivedData: 自动进样时间查询设置应答:");
		}
		if (b == 133)
		{
			devManager1.SetNetDevList(@class.byte_0);
			Answer133();
			@class.string_0 = Lang.PS("硬件版本号查询设置应答", "Hardware version number query set answer");
			LogMgr.Instance.Write2RunLog("TcpServerSocket.AnalyseReceivedData: 硬件版本号查询设置应答:");
		}
		if (b == 135)
		{
			@class.string_0 = Lang.PS("设置仪器序列号应答", "To set the instrument sequence number of the response ");
			LogMgr.Instance.Write2RunLog("TcpServerSocket.AnalyseReceivedData: 设置仪器序列号应答:");
		}
		if (b == 232)
		{
			@class.string_0 = Lang.PS("复位多位阀返回", "Reset multiposition valve return ");
			LogMgr.Instance.Write2RunLog("TcpServerSocket.AnalyseReceivedData: 复位多位阀返回:");
		}
		if (b == 139)
		{
			@class.string_0 = Lang.PS("设置时钟应答", "Set the clock response ");
			LogMgr.Instance.Write2RunLog("TcpServerSocket.AnalyseReceivedData: 设置时钟应答:");
		}
		if (b == 141 || b == 142)
		{
			devManager1.SetDetectorSettingList(@class.byte_0);
			Answer141(@class.accStyle_0, devManager1, @class.int_1);
			@class.string_0 = Lang.PS("检测器参数查询设置应答", "The detector parameters query set answer ");
			LogMgr.Instance.Write2RunLog("TcpServerSocket.AnalyseReceivedData: 检测器参数查询设置应答:");
		}
		switch (b)
		{
		case 144:
			@class.string_0 = Lang.PS("开始控温应答", "Start temperature response ");
			LogMgr.Instance.Write2RunLog("TcpServerSocket.AnalyseReceivedData: 开始控温应答:");
			ControlTemp = true;
			return null;
		case 145:
			@class.string_0 = Lang.PS("关闭控温应答", "Close the control response ");
			LogMgr.Instance.Write2RunLog("TcpServerSocket.AnalyseReceivedData: 关闭控温应答:");
			ControlTemp = false;
			return null;
		case 146:
			@class.string_0 = Lang.PS("启动全部样品分析应答", "Start all samples analyzed response ");
			mForm.chrDeviceCtrl.bAutoCycle1 = true;
			mForm.chrDeviceCtrl.bAutoCycle2 = true;
			mForm.bAutoCycle1 = true;
			mForm.bAutoCycle2 = true;
			channel1Ready = false;
			channel2Ready = false;
			channel3Ready = false;
			if (OnlineCtrl.selfCtrl != null)
			{
				OnlineCtrl.selfCtrl.bCombin1 = false;
				OnlineCtrl.selfCtrl.bCombin2 = false;
				OnlineCtrl.selfCtrl.bCombin3 = false;
			}
			if (cdlMgr.formMain.StateYiqi == 3)
			{
				cdlMgr.formMain.StateYiqi = 5;
			}
			Answer146(@class.int_1);
			return null;
		case 147:
			@class.string_0 = Lang.PS("停止全部样品分析应答", "Stop analyzing response for all the samples ");
			mForm.chrDeviceCtrl.bAutoCycle1 = false;
			mForm.chrDeviceCtrl.bAutoCycle2 = false;
			mForm.bAutoCycle1 = false;
			mForm.bAutoCycle2 = false;
			Answer147(@class);
			break;
		}
		if (b == 150)
		{
			LogMgr.Instance.Write2RunLog("TcpServerSocket.AnalyseReceivedData Info:启动指定通道分析应答： b=150.@class.int_1=" + @class.int_1);
			if (cdlMgr.formMain.StateYiqi == 3)
			{
				cdlMgr.formMain.StateYiqi = 5;
			}
			Answer150(@class.byte_0);
			@class.string_0 = Lang.PS("启动指定通道分析应答", "Start the specified channel analysis response ");
			if (@class.byte_0[0] == 0)
			{
				cdlMgr.formMain.bStart1 = true;
				cdlMgr.formMain.cntChannel1 = (uint)(frmParam.fTabChannel1 * 60f);
				mForm.bAutoCycle1 = true;
				channel1Ready = false;
			}
			else if (@class.byte_0[0] == 1)
			{
				cdlMgr.formMain.bStart2 = true;
				cdlMgr.formMain.cntChannel2 = (uint)(frmParam.fTabChannel2 * 60f);
				mForm.bAutoCycle2 = true;
				channel2Ready = false;
			}
			else
			{
				channel3Ready = false;
			}
		}
		if (b == 151)
		{
			devManager0.SetCurSglNumberEnd(@class.byte_0);
			@class.string_0 = Lang.PS("指定通道分析停止应答", "The specified channel stop response analysis ");
			LogMgr.Instance.Write2RunLog("TcpServerSocket.AnalyseReceivedData: 指定通道分析停止应答:");
			if (@class.byte_0[0] == 0)
			{
				mForm.chrDeviceCtrl.bAutoCycle1 = false;
				mForm.bAutoCycle1 = false;
			}
			else if (@class.byte_0[0] == 1)
			{
				mForm.chrDeviceCtrl.bAutoCycle2 = false;
				mForm.bAutoCycle2 = false;
			}
			Answer151();
		}
		if (b != 161 && b != 162)
		{
			if (b == 163)
			{
				devManager1.SetInjectNumList(@class.byte_0);
				Answer163();
				@class.string_0 = Lang.PS("查询气路配置应答", "Query gas path configuration response ");
			}
			if (b == 164 || b == 165)
			{
				Answer164(@class.byte_0);
				@class.string_0 = Lang.PS("查询设置EPC模块工作参数应答", "The query set EPC module parameters response ");
			}
			if (b == 166 || b == 167)
			{
				devManager1.SetEpcNameList(@class.byte_0);
				@class.string_0 = Lang.PS("查询设置EPC名称应答", "Set the EPC name query response ");
			}
			if (b == 168 || b == 169)
			{
				if (@class.byte_0.Length != 1)
				{
					throw new Exception("EPC控制");
				}
				devManager1.epcGasType = @class.byte_0[0];
				@class.string_0 = Lang.PS("EPC气体种类查询应答", "EPCGas type query response ");
			}
			if (b == 172)
			{
				@class.string_0 = Lang.PS("EPS用于何处查询应答", "EPSUsed where query response ");
			}
			if (b == 176 || b == 177)
			{
				devManager1.SetIpAddressList(@class.byte_0);
				Answer176();
				@class.string_0 = Lang.PS("查询网络参数应答", "Query the network parameter response");
			}
			if (b == 181 || b == 178)
			{
				Answer181(byte_1[25]);
				@class.string_0 = Lang.PS("设置查询点火时长应答", "Set the query ignition time response ");
			}
			if (b == 188 || b == 189)
			{
				mForm.Invoke((MethodInvoker)delegate
				{
					if (mForm.CurrentGCID == string_0)
					{
						mForm.insDeviceCtrl.comboBox4.Text = "±" + byte_0[0];
					}
				});
				@class.string_0 = Lang.PS("查询设置工作站量程", "Set the workstation range query ");
			}
			if (b == 190 || b == 191)
			{
				mForm.Invoke((MethodInvoker)delegate
				{
					if (mForm.CurrentGCID == string_0)
					{
						mForm.insDeviceCtrl.SetSmsAlarm(@class.byte_0[0] == 0);
					}
				});
				@class.string_0 = Lang.PS("设置查询指令是否鸣叫", "Set the query whether instruction calls ");
			}
			if (b == 192 || b == 193)
			{
				@class.string_0 = Lang.PS("查询设置控区名称应答", "The query set control zone name reply ");
				if (@class.byte_0.Length == 72)
				{
					int_5 = 6;
				}
				else if (@class.byte_0.Length == 96)
				{
					int_5 = 8;
				}
				devManager1.tempCtrlAreaTable.Byte2Name(@class.byte_0);
				mForm.Invoke((MethodInvoker)delegate
				{
					Update_FTNS_Name();
				});
			}
			if (b == 194 || b == 195)
			{
				@class.string_0 = Lang.PS("控温使能查询设置应答", "Temperature control enable query set answer ");
				devManager1.SetInsDevEnable(@class.byte_0);
				mForm.Invoke((MethodInvoker)delegate
				{
					Update_FTNS_Value();
				});
			}
			if (b == 208)
			{
				@class.string_0 = Lang.PS("鸣叫及文字提示应答", "Call and text prompt response ");
			}
			if (b == 212)
			{
				@class.string_0 = Lang.PS("外部事件开关设置应答", "event switch response");
				Answer211(byte_1);
			}
			if (b == 211)
			{
				@class.string_0 = Lang.PS("外部事件开关状态", "event switch status");
				Answer211(byte_1);
			}
			if (b == 216)
			{
				@class.string_0 = Lang.PS("液相泵开关应答", "Liquid pump Open Or Close");
				mForm.Invoke((MethodInvoker)delegate
				{
					switch (byte_0[0])
					{
					}
				});
			}
			if (b == 217)
			{
				@class.string_0 = Lang.PS("液相泵状态查寻应答", "Liquid pump Status");
				Answer217();
			}
			if (b == 218)
			{
				@class.string_0 = Lang.PS("液相泵控制状态设置应答", "Liquid pump ControlStatus set reply");
				Answer218();
			}
			if (b == 224 || b == 225)
			{
				@class.string_0 = Lang.PS("进样器型号查询设置应答", "Injector type query set answer");
				devManager1.injectType = @class.byte_0[0];
				mForm.Invoke((MethodInvoker)delegate
				{
					if (mForm.CurrentGCID == string_0)
					{
						mForm.insDeviceCtrl.comboBox1.SelectedIndex = @class.byte_0[0];
					}
				});
			}
			if (b == 226 || b == 227)
			{
				devManager1.injectSet.ReadFromByte(@class.byte_0);
				if (mForm.CurrentGCID == string_0)
				{
					AutoSampleQuery(@class.accStyle_0, devManager1);
				}
				@class.string_0 = Lang.PS("进样器参数查询设置应答", "Injector parameters query set answer ");
			}
			if (b == 143)
			{
				Answer143(@class.byte_0);
				try
				{
					AnalyseReceivedData_ChromSend(@class.byte_0, string_0);
				}
				catch (Exception ex)
				{
					LogMgr.Instance.LogError($"AnalyseReceivedData_ChromSend{ex.Message}");
					LogMgr.Instance.LogError($"AnalyseReceivedData_ChromSend dots.Length{sglsSampling[0].dots.Length}");
					LogMgr.Instance.LogError($"AnalyseReceivedData_ChromSend oriDots.Length{sglsSampling[0].oriDots.Length}");
					LogMgr.Instance.LogError($"AnalyseReceivedData_ChromSend DotsNum{sglsSampling[0].DotsNum}");
					LogMgr.Instance.LogError($"AnalyseReceivedData_ChromSend dots.Length{sglsSampling[1].dots.Length}");
					LogMgr.Instance.LogError($"AnalyseReceivedData_ChromSend oriDots.Length{sglsSampling[1].oriDots.Length}");
					LogMgr.Instance.LogError($"AnalyseReceivedData_ChromSend DotsNum{sglsSampling[1].DotsNum}");
					LogMgr.Instance.LogError($"AnalyseReceivedData_ChromSend{ex.StackTrace}");
				}
				@class.string_0 = Lang.PS("色谱仪主动送出数据", "Chromatograph the initiative to send data ");
			}
			if (b == 159)
			{
				Answer159(@class.byte_0);
			}
			if (b == 175)
			{
				@class.string_0 = Lang.PS("色谱仪主动送出自动进样器数据", "Chromatograph the initiative to send autosampler data ");
				if (mForm.CurrentGCID == string_0)
				{
					devManager1.injectConnState = Encoding.Default.GetString(@class.byte_0, 0, 3);
					devManager1.injectWorkState = Encoding.Default.GetString(@class.byte_0, 3, 3);
					devManager1.injectConnState = "000";
					devManager1.injectWorkState = "001";
					devManager1.injectBotNum = Encoding.Default.GetString(@class.byte_0, 6, 3);
					devManager1.injectNeedleNum = Encoding.Default.GetString(@class.byte_0, 9, 3);
					Answer175(@class.byte_0);
				}
			}
			if (b != 143 && b != 159 && b != 175)
			{
				mForm.Invoke((MethodInvoker)delegate
				{
					mForm.insDeviceCtrl.AddMessList(string_0);
				});
			}
			return @class.byte_0;
		}
		@class.string_0 = Lang.PS("EPC设定参数查询设置返回", "EPCSet the parameters of the query set the return ");
		return null;
	}

	private string AnalyseReceivedData_ChromSend(byte[] byte_1, string string_3)
	{
		int chn = 0;
		float fuzhu = 0f;
		float zhulu = 0f;
		if (byte_1[19] == 64)
		{
			chn = 0;
		}
		else if (byte_1[19] == 65)
		{
			chn = 1;
		}
		else if (byte_1[19] == 80)
		{
			chn = 2;
		}
		else if (byte_1[19] == 81)
		{
			chn = 3;
		}
		int numout = 0;
		byte b = SetModbusWordValue(byte_1, out numout);
		if (b == 0)
		{
			if (byte_1.Length > 19)
			{
				int num = byte_1.Length - 4;
				fuzhu = (float)(byte_1[num] * 256 + byte_1[num + 1] - 10000) / 100f;
				num += 2;
				zhulu = (float)(byte_1[num] * 256 + byte_1[num + 1] - 10000) / 100f;
			}
			LinkCtrl(string_3, fuzhu, zhulu, chn);
			return "";
		}
		Array.Resize(ref detectorParseList, b);
		for (int i = 0; i < b; i++)
		{
			if (detectorParseList[i] == null)
			{
				detectorParseList[i] = new DetectorParse(i);
			}
			bool mybool = detectorParseList[i].ParseData(byte_1, ref numout, mForm.shuaijian, mForm.shuaijian2, mForm.shuaijian3);
			if (mForm.CurrentGCID == ID)
			{
				mForm.Invoke((MethodInvoker)delegate
				{
					mForm.insDeviceCtrl.SetFlowState(mybool);
				});
			}
		}
		if (byte_1.Length - numout == 16)
		{
			int num2 = byte_1.Length - 4;
			fuzhu = (float)(byte_1[num2] * 256 + byte_1[num2 + 1] - 10000) / 100f;
			num2 += 2;
			zhulu = (float)(byte_1[num2] * 256 + byte_1[num2 + 1] - 10000) / 100f;
		}
		if (Environment.TickCount - beginIdleTC < 3000)
		{
			int i = 0;
			while (i < detectorParseList.Length)
			{
				for (int num3 = 0; num3 < dtc_Channels.Length; num3++)
				{
					if (!dtc_Channels[num3].IsGC08)
					{
						continue;
					}
					if (dtc_Channels[num3].mark != 0)
					{
						if (detectorParseList[i].byte_2 != dtc_Channels[num3].mark)
						{
							continue;
						}
						for (int num4 = 0; num4 < dtc_Channels.Length - 1; num4++)
						{
							for (int num5 = 0; num5 < dtc_Channels.Length - num4 - 1 && dtc_Channels[num5 + 1].mark != 0; num5++)
							{
								if (dtc_Channels[num5].mark > dtc_Channels[num5 + 1].mark)
								{
									DtC_Channel dtC_Channel = dtc_Channels[num5];
									dtc_Channels[num5] = dtc_Channels[num5 + 1];
									dtc_Channels[num5 + 1] = dtC_Channel;
								}
							}
						}
					}
					else
					{
						dtc_Channels[num3].mark = detectorParseList[i].byte_2;
						dtc_Channels[num3].name = DetectorSettingRow.GetDeviceTypeNameByIdx(detectorParseList[i].byte_2, frmParam.iDetector);
						dtc_Channels[num3].Gc08Values(detectorParseList[i].float_0);
						ChromInfoR chromInfoR = dtc_Channels[num3].chromInfoR;
						int byte_2 = detectorParseList[i].byte_1;
						chromInfoR.UvRange = byte_2.ToString();
						dtc_Channels[num3].chromInfoR.UvwsStepFreq = detectorParseList[i].byte_0;
						dtc_Channels[num3].unitStr = detectorParseList[i].string_0;
					}
					i++;
					break;
				}
			}
			for (int num6 = 0; num6 < dtc_Channels.Length - 1; num6++)
			{
				sglsSampling[num6].detector_name = dtc_Channels[num6].name;
			}
		}
		class44_0.class78_0 = detectorParseList;
		for (int i = 0; i < detectorParseList.Length; i++)
		{
			if (!CheckDtcChannels(detectorParseList[i], string_3, chn))
			{
				return "";
			}
		}
		LinkCtrl(string_3, fuzhu, zhulu, chn);
		return "";
	}

	public void Smooth(Class44 class44_0, float fuzhu3, float zhulu2)
	{
		Random random = new Random();
		double num = random.NextDouble();
		num /= 20.0;
		if ((double)(class44_0.float_0[0] - (float)(int)class44_0.float_0[0]) > 0.8)
		{
			class44_0.float_0[0] += 0.09f;
		}
		fTemp1[iTempS1++] = class44_0.float_0[0];
		for (int i = 0; i < 5; i++)
		{
			if (fTemp1[i] == 0f)
			{
				fTemp1[i] = class44_0.float_0[0];
			}
		}
		class44_0.float_0[0] = (fTemp1[0] + fTemp1[1] + fTemp1[2] + fTemp1[3] + fTemp1[4] + (float)num) / 5f;
		if (iTempS1 > 4)
		{
			iTempS1 = 0;
		}
		num = random.NextDouble();
		num /= 20.0;
		if ((double)(class44_0.float_0[1] - (float)(int)class44_0.float_0[1]) > 0.8)
		{
			class44_0.float_0[1] += 0.09f;
		}
		fTemp2[iTempS2++] = class44_0.float_0[1];
		for (int j = 0; j < 5; j++)
		{
			if (fTemp2[j] == 0f)
			{
				fTemp2[j] = class44_0.float_0[1];
			}
		}
		class44_0.float_0[1] = (fTemp2[0] + fTemp2[1] + fTemp2[2] + fTemp2[3] + fTemp2[4] + (float)num) / 5f;
		if (iTempS2 > 4)
		{
			iTempS2 = 0;
		}
		num = random.NextDouble();
		num /= 20.0;
		if ((double)(class44_0.float_0[2] - (float)(int)class44_0.float_0[2]) > 0.8)
		{
			class44_0.float_0[2] += 0.09f;
		}
		fTemp3[iTempS3++] = class44_0.float_0[2];
		for (int k = 0; k < 5; k++)
		{
			if (fTemp3[k] == 0f)
			{
				fTemp3[k] = class44_0.float_0[2];
			}
		}
		class44_0.float_0[2] = (fTemp3[0] + fTemp3[1] + fTemp3[2] + fTemp3[3] + fTemp3[4] + (float)num) / 5f;
		if (iTempS3 > 4)
		{
			iTempS3 = 0;
		}
		num = random.NextDouble();
		num /= 20.0;
		if ((double)(class44_0.float_0[3] - (float)(int)class44_0.float_0[3]) > 0.8)
		{
			class44_0.float_0[3] += 0.09f;
		}
		fTemp4[iTempS4++] = class44_0.float_0[3];
		for (int l = 0; l < 5; l++)
		{
			if (fTemp4[l] == 0f)
			{
				fTemp4[l] = class44_0.float_0[3];
			}
		}
		class44_0.float_0[3] = (fTemp4[0] + fTemp4[1] + fTemp4[2] + fTemp4[3] + fTemp4[4] + (float)num) / 5f;
		if (iTempS4 > 4)
		{
			iTempS4 = 0;
		}
		num = random.NextDouble();
		num /= 20.0;
		if ((double)(class44_0.float_0[4] - (float)(int)class44_0.float_0[4]) > 0.8)
		{
			class44_0.float_0[4] += 0.09f;
		}
		fTemp5[iTempS5++] = class44_0.float_0[4];
		for (int m = 0; m < 5; m++)
		{
			if (fTemp5[m] == 0f)
			{
				fTemp1[m] = class44_0.float_0[4];
			}
		}
		class44_0.float_0[4] = (fTemp5[0] + fTemp5[1] + fTemp5[2] + fTemp5[3] + fTemp5[4] + (float)num) / 5f;
		if (iTempS5 > 4)
		{
			iTempS5 = 0;
		}
		num = random.NextDouble();
		num /= 20.0;
		if ((double)(class44_0.float_0[5] - (float)(int)class44_0.float_0[5]) > 0.8)
		{
			class44_0.float_0[5] += 0.09f;
		}
		fTemp6[iTempS6++] = class44_0.float_0[5];
		for (int n = 0; n < 5; n++)
		{
			if (fTemp6[n] == 0f)
			{
				fTemp6[n] = class44_0.float_0[5];
			}
		}
		class44_0.float_0[5] = (fTemp6[0] + fTemp6[1] + fTemp6[2] + fTemp6[3] + fTemp6[4] + (float)num) / 5f;
		if (iTempS6 > 4)
		{
			iTempS6 = 0;
		}
		num = random.NextDouble();
		num /= 20.0;
		if ((double)(fuzhu3 - (float)(int)fuzhu3) > 0.8)
		{
			fuzhu3 += 0.09f;
		}
		fTemp7[iTempS7++] = fuzhu3;
		for (int num2 = 0; num2 < 5; num2++)
		{
			if (fTemp7[num2] == 0f)
			{
				fTemp7[num2] = fuzhu3;
			}
		}
		fuzhu3 = (fTemp7[0] + fTemp7[1] + fTemp7[2] + fTemp7[3] + fTemp7[4] + (float)num) / 5f;
		if (iTempS7 > 4)
		{
			iTempS7 = 0;
		}
		num = random.NextDouble();
		num /= 20.0;
		if ((double)(zhulu2 - (float)(int)zhulu2) > 0.8)
		{
			zhulu2 += 0.09f;
		}
		fTemp8[iTempS8++] = zhulu2;
		for (int num3 = 0; num3 < 5; num3++)
		{
			if (fTemp8[num3] == 0f)
			{
				fTemp8[num3] = zhulu2;
			}
		}
		zhulu2 = (fTemp8[0] + fTemp8[1] + fTemp8[2] + fTemp8[3] + fTemp8[4] + (float)num) / 5f;
		if (iTempS8 > 4)
		{
			iTempS8 = 0;
		}
	}

	public void LinkCtrl(string CurrentID, float fuzhu3, float zhulu2, int chn)
	{
		float num = fuzhu3;
		float num2 = zhulu2;
		if (mForm.IsDisposed2 || mForm.CurrentGCID != CurrentID)
		{
			return;
		}
		mForm.Invoke((MethodInvoker)delegate
		{
			mForm.insDeviceCtrl.ReadTempratureTable(class44_0, fuzhu3, zhulu2);
			mForm.insDeviceCtrl.SetFromPictureBoxImage(class44_0);
		});
		int currentChannelIndex = mForm.CurrentChannelIndex;
		if (sglsSampling.Length <= currentChannelIndex || sglsSampling.Length <= chn)
		{
			LogMgr.Instance.Write2RunLog("TcpServerSocket.LinkCtrl Error: this.sglsSampling.Length:" + sglsSampling.Length + " num:" + currentChannelIndex + " chn:" + chn);
			return;
		}
		if (dtc_Channels.Length <= currentChannelIndex)
		{
			LogMgr.Instance.Write2RunLog("TcpServerSocket.LinkCtrl Error: this.dtc_Channels.Length:" + dtc_Channels.Length + " num:" + currentChannelIndex);
			return;
		}
		Signal signal = sglsSampling[currentChannelIndex];
		DtC_Channel channel = dtc_Channels[currentChannelIndex];
		int num3 = signal.dots.Length;
		int num4 = signal.DotsNum - 1;
		if (num4 <= 0)
		{
			mForm.Invoke((MethodInvoker)delegate
			{
				SetFormTextBoxSampleValue();
				mForm.insDeviceCtrl.SetFormTextBoxOnLineState(devManager1);
			});
			LogMgr.Instance.Write2RunLog("TcpServerSocket.LinkCtrl Error: signal.dots.Length::" + num3 + " ndotSignal:" + num4 + " current channel:" + currentChannelIndex);
			return;
		}
		if (num3 <= num4)
		{
			LogMgr.Instance.Write2RunLog("TcpServerSocket.LinkCtrl Error: sglsSampling[num].dots.Length:" + num3 + " ndotSignal:" + num4 + " current channel:" + currentChannelIndex);
			return;
		}
		PointF pointF = signal.dots[num4];
		mForm.Invoke((MethodInvoker)delegate
		{
			mForm.chrAcqCtrl.maskedTextBox6.Text = pointF.Y.ToString("0.000");
		});
		mForm.Invoke((MethodInvoker)delegate
		{
			mForm.chrAcqCtrl.maskedTextBox7.Text = pointF.X.ToString("0.000");
			if (minePlot.minePlotSelf != null)
			{
				minePlot.minePlotSelf.maskedTextBox7.Text = pointF.X.ToString("0.000");
			}
			mForm.chrDeviceCtrl.SetUvInfo(channel);
		});
		mForm.Invoke((MethodInvoker)delegate
		{
			mForm.insDeviceCtrl.SetFormTextBoxOnLineState(devManager1);
		});
	}

	private void Answer228(AccStyle accStyle0, InsDeviceManager class56_2)
	{
		RefrushEventTable1(accStyle0, class56_2);
	}

	private void Answer129(AccStyle accStyle0, InsDeviceManager class56_2)
	{
		RefrushTempratureControl(accStyle0, class56_2);
	}

	private void Answer130(AccStyle accStyle0, InsDeviceManager class56_2)
	{
		RefrushEventTable0(accStyle0, class56_2);
	}

	private void Answer128(AccStyle accStyle0, InsDeviceManager class56_2)
	{
		RefrushTempratureTable(accStyle0, class56_2);
	}

	private void Answer141(AccStyle accStyle0, InsDeviceManager class56_2, int myint0)
	{
		method_9(accStyle0, class56_2, myint0);
	}

	private void Answer218(byte[] byte1)
	{
		if (LYTHCtrl2.selfCtrl != null)
		{
			LYTHCtrl2.selfCtrl.disposeDates(byte1);
		}
	}

	private void Answer234(AccStyle accStyle_0, InsDeviceManager class56_2)
	{
		method_7(accStyle_0, class56_2);
	}

	private void Answer238()
	{
		if (mForm.CurrentGCID == string_0)
		{
			mForm.Invoke((MethodInvoker)delegate
			{
				mForm.Fmultivalve.setCheckBox(devManager1.multivalveEnable);
			});
		}
	}

	private void Answer132()
	{
		if (!(string_0 != mForm.CurrentGCID))
		{
			mForm.Invoke((MethodInvoker)delegate
			{
				mForm.insDeviceCtrl.ReadAutoInjectorSetting(devManager1);
			});
		}
	}

	private void Answer133()
	{
		if (mForm.CurrentGCID == string_0)
		{
			mForm.Invoke((MethodInvoker)delegate
			{
				mForm.insDeviceCtrl.ReadHardVersion(devManager1);
			});
		}
	}

	private void Answer143(byte[] mybyte)
	{
	}

	public void Answer146(int curIdx)
	{
		try {
			string logPath = System.IO.Path.Combine(System.Windows.Forms.Application.StartupPath, "AutoInjDebug.txt");
			System.IO.File.AppendAllText(logPath, $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] TcpServerSocket.Answer146 (Command 18 response) received. ID={ID}\r\n");
		} catch {}

		int myint0;
		for (myint0 = 0; myint0 < sglsSampling.Length; myint0++)
		{
			sglsSampling[myint0].ResetOriDots(createDiskFile: true);
			sglsSampling[myint0].simple = true;
			sglsSampling[myint0].EvenUY = 0f;
			sglsSampling[myint0].deltaEvenUY = 0f;
			sglsSampling[myint0].EvenTimeIndex = -1f;
			mForm.Invoke((MethodInvoker)delegate
			{
				if (mForm.CurrentGCID == ID && myint0 == curIdx)
				{
					mForm.chrAcqCtrl.tsStart.Enabled = false;
					mForm.chrAcqCtrl.tsstop.Enabled = true;
					if (cdlMgr.formMain.IsAutoCalibra != 0)
					{
						mForm.vocctrl.btnCali.BackColor = Color.Green;
					}
				}
			});
			if (myint0 == 0)
			{
				InitSignalDilg(myint0, AllChannels: false);
			}
			else
			{
				InitSignalDilg(myint0, AllChannels: true);
			}
		}
		AlalyseStatus = true;
		int_2 = mForm.FrmEquip.GetModBusDeviceIDByEquipID(string_0);
		bgChrom.chromInfo.cclDescription = mForm.FrmEquip.GetNameByID(string_0);
		
		// 为了防止多个无效仪器导致定时器被启动多次（4次并发），
		// 我们只让第一个 Instrument 实例去接管全局的自动进样倒计时。
		if (SysCfgDlg.sysConfig.pageInstrus != null && SysCfgDlg.sysConfig.pageInstrus.Length > 0)
		{
			var inst = SysCfgDlg.sysConfig.pageInstrus[0];
			if (inst != null)
			{
				try {
					string logPath = System.IO.Path.Combine(System.Windows.Forms.Application.StartupPath, "AutoInjDebug.txt");
					System.IO.File.AppendAllText(logPath, $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] TcpServerSocket.Answer146 routing OnInstrumentStarted to FIRST instrument to prevent duplicates.\r\n");
				} catch {}
				inst.OnInstrumentStarted();
			}
		}

		mForm.Invoke((MethodInvoker)delegate
		{
			if (mForm.CurrentGCID == ID)
			{
				mForm.insDeviceCtrl.UpdateControlAnalyzeText(bCtrl: true);
				mForm.chrAcqCtrl.tbTime_DoubleClick(null, null);
				if (minePlot.minePlotSelf != null)
				{
					minePlot.minePlotSelf.UpdateControlAnalyzeText(bCtrl: true);
				}
			}
		});
	}

	private void Answer147(Class12 @class)
	{
		StoptAllGather();
		for (int i = 0; i < sglsSampling.Length; i++)
		{
			sglsSampling[i].simple = false;
		}
		AlalyseStatus = false;
		mForm.Invoke((MethodInvoker)delegate
		{
			if (mForm.CurrentGCID == ID)
			{
				mForm.insDeviceCtrl.UpdateControlAnalyzeText(bCtrl: false);
				mForm.chrAcqCtrl.tbTime_DoubleClick(null, null);
				if (minePlot.minePlotSelf != null)
				{
					minePlot.minePlotSelf.UpdateControlAnalyzeText(bCtrl: false);
				}
			}
		});
	}

	private void Answer150(byte[] mybyte)
	{
		try {
			string logPath = System.IO.Path.Combine(System.Windows.Forms.Application.StartupPath, "AutoInjDebug.txt");
			System.IO.File.AppendAllText(logPath, $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] TcpServerSocket.Answer150 (Command 22 response) received. ID={ID}\r\n");
		} catch {}

		int myint0 = 0;
		devManager1.SetCurSglNumberStart(mybyte);
		if (devManager1.sglNumberStart > 3)
		{
			for (int i = 0; i < dtc_Channels.Length; i++)
			{
				if (dtc_Channels[i].mark == devManager1.sglNumberStart)
				{
					myint0 = i;
					break;
				}
			}
		}
		else
		{
			myint0 = devManager1.sglNumberStart;
		}
		sglsSampling[myint0].ResetOriDots(createDiskFile: true);
		sglsSampling[myint0].simple = true;
		sglsSampling[myint0].EvenUY = 0f;
		sglsSampling[myint0].deltaEvenUY = 0f;
		sglsSampling[myint0].EvenTimeIndex = -1f;
		mForm.Invoke((MethodInvoker)delegate
		{
			if (mForm.CurrentGCID == ID)
			{
				if (mybyte[0] == 0)
				{
					mForm.chrDeviceCtrl.bAutoCycle1 = true;
				}
				else if (mybyte[0] == 1)
				{
					mForm.chrDeviceCtrl.bAutoCycle2 = true;
				}
				mForm.chrAcqCtrl.tsStart.Enabled = false;
				mForm.chrAcqCtrl.tsstop.Enabled = true;
				if (dtc_Channels[myint0].mark == 136)
				{
					mForm.chrAcqCtrl.tbTime_DoubleClick(null, null);
				}
			}
		});
		int_2 = mForm.FrmEquip.GetModBusDeviceIDByEquipID(string_0);
		bgChrom.chromInfo.cclDescription = mForm.FrmEquip.GetNameByID(string_0);
		
		mForm.Invoke((MethodInvoker)delegate
		{
			if (mForm.CurrentGCID == ID)
			{
				mForm.insDeviceCtrl.UpdateControlAnalyzeText(bCtrl: true);
				if (minePlot.minePlotSelf != null)
				{
					minePlot.minePlotSelf.UpdateControlAnalyzeText(bCtrl: true);
				}
			}
		});
		InitSignalDilg(myint0, AllChannels: false);
	}

	private void Answer151()
	{
		mForm.Invoke((MethodInvoker)delegate
		{
			Update_Peak();
		});
	}

	private void Answer159(byte[] mybyte)
	{
		int num = mybyte[0];
		int num2 = 1;
		int myint0 = 0;
		while (myint0 < num)
		{
			num2++;
			double num3 = (double)(mybyte[num2] * 256 + mybyte[num2 + 1]) / 100.0;
			num2 += 2;
			double num4 = (double)(mybyte[num2] * 256 + mybyte[num2 + 1]) / 100.0;
			num2 += 2;
			double mydouble = (double)(mybyte[num2] * 256 + mybyte[num2 + 1]) / 100.0;
			num2 += 2;
			IBrainConvert.Byte2ToInt(mybyte, num2);
			num2++;
			byte myByte = mybyte[num2];
			num2++;
			string str0 = num3.ToString("0.00");
			string str1 = num4.ToString("0.00");
			string str2 = mydouble.ToString("0.00");
			if (myint0 == 0)
			{
				fPress = num4;
			}
			if (str0 == "655.35")
			{
				str0 = "--";
			}
			if (str1 == "655.35")
			{
				str1 = "--";
			}
			if (str2 == "655.35")
			{
				str2 = "--";
			}
			mForm.Invoke((MethodInvoker)delegate
			{
				Answer159_sub(myint0, myByte, str0, str1, str2, mydouble, mybyte);
			});
			int num5 = myint0;
			myint0 = num5 + 1;
		}
	}

	public void Answer159_sub(int idx, byte myByte, string str0, string str1, string str2, double mydouble, byte[] mybyte)
	{
		if (!(ID != mForm.CurrentGCID))
		{
			mForm.insDeviceCtrl.UpdateEpcInfo(idx, myByte, str0, str1, str2, mydouble, mybyte);
		}
	}

	private void Answer163()
	{
		if (!(string_0 != mForm.CurrentGCID))
		{
			mForm.Invoke((MethodInvoker)delegate
			{
				mForm.insDeviceCtrl.ReadInjectNumList(devManager1);
			});
		}
	}

	private void Answer164(byte[] mybyte)
	{
		int num = 0;
		int idx = 0;
		switch (mybyte[0])
		{
		case 48:
			num = 0;
			idx = 0;
			break;
		case 49:
			num = 0;
			idx = 1;
			break;
		case 50:
			num = 0;
			idx = 2;
			break;
		case 51:
			num = 1;
			idx = 3;
			break;
		case 52:
			num = 1;
			idx = 4;
			break;
		case 53:
			num = 1;
			idx = 5;
			break;
		case 54:
			num = 2;
			idx = 6;
			break;
		case 55:
			num = 2;
			idx = 7;
			break;
		case 56:
			num = 2;
			idx = 8;
			break;
		case 57:
			num = 3;
			idx = 9;
			break;
		case 58:
			num = 3;
			idx = 10;
			break;
		case 59:
			num = 3;
			idx = 11;
			break;
		case 60:
			num = 4;
			idx = 12;
			break;
		case 61:
			num = 4;
			idx = 13;
			break;
		case 62:
			num = 4;
			idx = 14;
			break;
		case 63:
			num = 5;
			idx = 15;
			break;
		case 64:
			num = 5;
			idx = 16;
			break;
		case 65:
			num = 5;
			idx = 17;
			break;
		case 80:
			num = 0;
			idx = 0;
			break;
		}
		devManager1.GetEPCDevParam1(idx, mybyte);
		devManager1.GetEPCDevParam(num, mybyte);
		LinkepcParas(num, mybyte[0]);
	}

	private void Answer175(byte[] mybyte)
	{
		mForm.Invoke((MethodInvoker)delegate
		{
			if (MicrFPDCtrl.selfCtrl != null)
			{
				MicrFPDCtrl.selfCtrl.labFPDhighV.Text = ((mybyte[3] << 8) | mybyte[4]).ToString();
				MicrFPDCtrl.selfCtrl.labFPDhighV2.Text = ((mybyte[0] << 8) | mybyte[1]).ToString();
			}
			else if (ChromDeviceCtrl.selfCtrl != null)
			{
				ChromDeviceCtrl.selfCtrl.tbHighV1.Text = ((mybyte[3] << 8) | mybyte[4]).ToString();
				ChromDeviceCtrl.selfCtrl.tbHighV2.Text = ((mybyte[0] << 8) | mybyte[1]).ToString();
			}
		});
	}

	private void Answer176()
	{
		if (!(mForm.CurrentGCID != string_0))
		{
			mForm.Invoke((MethodInvoker)delegate
			{
				mForm.insDeviceCtrl.ReadIpAddress(devManager1);
			});
		}
	}

	private void Answer181(byte fireTime)
	{
		if (!(mForm.CurrentGCID != string_0))
		{
			mForm.Invoke((MethodInvoker)delegate
			{
				gcHardFrm.textBox3.Text = fireTime.ToString();
			});
		}
	}

	private void Answer211(byte[] byte_0)
	{
		if (!(string_0 != mForm.CurrentGCID))
		{
			eventState = byte_0[25];
			mForm.Invoke((MethodInvoker)delegate
			{
				mForm.insDeviceCtrl.SetEventSwitchInfo(byte_0);
			});
		}
	}

	private void Answer212()
	{
		if (!(string_0 != mForm.CurrentGCID))
		{
			mForm.Invoke((MethodInvoker)delegate
			{
				mForm.insDeviceCtrl.SetEventSwitchInfo(byte_0);
			});
		}
	}

	private void Answer217()
	{
		if (!(string_0 != mForm.CurrentGCID))
		{
			mForm.Invoke((MethodInvoker)delegate
			{
				mForm.insDeviceCtrl.SetLiquidState(byte_0);
			});
		}
	}

	private void Answer218()
	{
		if (!(string_0 != mForm.CurrentGCID))
		{
			mForm.Invoke((MethodInvoker)delegate
			{
				mForm.insDeviceCtrl.SetLiquidInfo(byte_0);
			});
		}
	}

	public void InitSignalDilg(int ChannelIndex, bool AllChannels)
	{
		mForm.Invoke((MethodInvoker)delegate
		{
			ChannelChartPara channelChartPara = cdlMgr.GetChannelChartPara(ID, ChannelIndex);
			if (channelChartPara != null)
			{
				float num = channelChartPara.fullScreenTime;
				float num2 = channelChartPara.showLowLimit;
				float num3 = channelChartPara.showHighLimit;
				if (num < 0.1f || (num2 == 0f && num3 == 0f))
				{
					num = 0.2f;
					num2 = -1f;
					num3 = 10f;
				}
				sglsSampling[ChannelIndex].disLg.lgXBeg = 0f;
				sglsSampling[ChannelIndex].disLg.lgX = num;
				sglsSampling[ChannelIndex].disLg.lgYBeg = num2;
				sglsSampling[ChannelIndex].disLg.lgY = num3 - num2;
				if (!AllChannels)
				{
					int currentChannelIndex = mForm.CurrentChannelIndex;
					if (ChannelIndex == currentChannelIndex)
					{
						mForm.chrAcqCtrl.tbTime_DoubleClick(null, null);
					}
					mForm.tabChannel_SelectedIndexChanged(null, null);
				}
			}
		});
	}

	public void Linkepctypes()
	{
		mForm.Invoke(new MethodInvoker(Update_ChannelTableText));
	}

	public int GetLinkepcParasNumber(byte Type)
	{
		int result = 0;
		switch (Type)
		{
		case 48:
			result = 0;
			break;
		case 49:
			result = 1;
			break;
		case 50:
			result = 2;
			break;
		case 51:
			result = 0;
			break;
		case 52:
			result = 1;
			break;
		case 53:
			result = 2;
			break;
		case 54:
			result = 0;
			break;
		case 55:
			result = 1;
			break;
		case 56:
			result = 2;
			break;
		case 57:
			result = 0;
			break;
		case 58:
			result = 1;
			break;
		case 59:
			result = 2;
			break;
		case 60:
			result = 0;
			break;
		case 61:
			result = 1;
			break;
		case 62:
			result = 2;
			break;
		case 63:
			result = 0;
			break;
		case 64:
			result = 1;
			break;
		case 65:
			result = 2;
			break;
		}
		return result;
	}

	public void LinkepcParas(int int_7, byte Type)
	{
		if (string_0 != mForm.CurrentGCID)
		{
			return;
		}
		mForm.Invoke((MethodInvoker)delegate
		{
			if (mForm.insDeviceCtrl.CurrentEpcIdx == int_7)
			{
				mForm.insDeviceCtrl.ReadEpcInfo(devManager1, int_7, Type);
			}
		});
	}

	private void AutoSampleQuery(AccStyle accStyle_0, InsDeviceManager class56_2)
	{
		mForm.Invoke((MethodInvoker)delegate
		{
			if (accStyle_0 == AccStyle.Read)
			{
				mForm.insDeviceCtrl.ReadInjectorBaseSetting(class56_2);
			}
			else if (accStyle_0 == AccStyle.Write)
			{
				mForm.insDeviceCtrl.WriteInjectorBaseSetting(class56_2);
			}
		});
	}

	private void RefrushTempratureTable(AccStyle accStyle0, InsDeviceManager class56_2)
	{
		if (mForm.CurrentGCID != string_0)
		{
			return;
		}
		mForm.Invoke((MethodInvoker)delegate
		{
			int num = 6;
			if (accStyle0 == AccStyle.Read)
			{
				mForm.insDeviceCtrl.ReadTempratureTable(class56_2);
			}
			if (accStyle0 == AccStyle.Write)
			{
				mForm.insDeviceCtrl.WriteTempratureTable(class56_2);
				string text = "";
				for (int i = 0; i < num; i++)
				{
					string text2 = text;
					text = text2 + i + "路,设置:" + devManager0.tempSetedList[i] + ",保护:" + devManager0.tempProtectList[i] + "\r\n";
				}
				Class49.InsertIntoTable(Class49.string_9[4], Class49.user_0.u_name, string_1 + string_0, "反控:设置6路设置温度及6路保护温度", text);
			}
		});
	}

	private void method_7(AccStyle accStyle_0, InsDeviceManager class56_2)
	{
		if (mForm.CurrentGCID != string_0)
		{
			return;
		}
		mForm.Invoke((MethodInvoker)delegate
		{
			if (accStyle_0 == AccStyle.Read)
			{
				mForm.insDeviceCtrl.ReadTempratureTable2(class56_2);
			}
			if (accStyle_0 == AccStyle.Write)
			{
				mForm.insDeviceCtrl.WriteTempratureTable2(class56_2);
				string tempratureTableTextInfo = mForm.insDeviceCtrl.GetTempratureTableTextInfo(class56_2);
				Class49.InsertIntoTable(Class49.string_9[4], Class49.user_0.u_name, string_1 + string_0, "反控:设置7、8路设置温度及保护温度", tempratureTableTextInfo);
			}
		});
	}

	private void method_8(AccStyle accStyle_0, InsDeviceManager class56_2)
	{
		if (mForm.CurrentGCID != string_0)
		{
			return;
		}
		mForm.Invoke((MethodInvoker)delegate
		{
			if (accStyle_0 == AccStyle.Read)
			{
				mForm.insDeviceCtrl.ReadTempratureTable(class56_2);
			}
			if (accStyle_0 == AccStyle.Write)
			{
				mForm.insDeviceCtrl.WriteTempratureTable(class56_2);
			}
		});
	}

	public void ShowDtcrForm(int FIDIndex, string DtrMark)
	{
		mForm.Invoke((MethodInvoker)delegate
		{
			method_9(AccStyle.Read, devManager1, FIDIndex);
			if (gcHardFrm == null)
			{
				gcHardFrm = new FIDSet();
			}
			gcHardFrm.Mark.Text = DtrMark;
			gcHardFrm.Show();
		});
	}

	private void method_9(AccStyle accStyle_0, InsDeviceManager class56_2, int myint0)
	{
		if (mForm.CurrentGCID != string_0)
		{
			return;
		}
		mForm.Invoke((MethodInvoker)delegate
		{
			int channelCount = cdlMgr.ChannelCount;
			int count = class56_2.detectorSettingList.Count;
			while (class56_2.detectorSettingList.Count < dtc_Channels.Length || class56_2.detectorSettingList.Count < myint0)
			{
				DetectorSettingRow item = new DetectorSettingRow
				{
					detectorType = dtc_Channels[count++].mark
				};
				class56_2.detectorSettingList.Add(item);
			}
			DetectorSettingRow detectorSettingRow = class56_2.detectorSettingList[myint0];
			if (gcHardFrm == null)
			{
				gcHardFrm = new FIDSet();
			}
			if (accStyle_0 == AccStyle.Read)
			{
				gcHardFrm.Reload(myint0, dtc_Channels[myint0].name);
				sglsSampling[myint0].detector_name = detectorSettingRow.GetDeviceTypeName();
			}
			if (accStyle_0 == AccStyle.Write)
			{
				string text = "";
				string text2 = text;
				DetectorSettingRow[] array = new DetectorSettingRow[0];
				detectorSettingRow.SetDeviceTypeByName(gcHardFrm.Mark.Text);
				detectorSettingRow.SetPolarity(!gcHardFrm.Positive.Checked);
				detectorSettingRow.range = byte.Parse(gcHardFrm.range.Text);
				detectorSettingRow.SetBaselineDeduction(gcHardFrm.BsDeduct.Checked);
				detectorSettingRow.SetFreq(byte.Parse(Math.Floor((double)(Class49.Object2Int(gcHardFrm.Freq.Text, 0) / 10)).ToString()));
				text = text2 + detectorSettingRow.GetDeviceTypeName() + "极性:" + detectorSettingRow.GetPolarity() + "量程:" + detectorSettingRow.range + "基线扣除:" + detectorSettingRow.GetBaselineDeduction() + "采样频率:" + detectorSettingRow.GetFreq();
				if (!detectorSettingRow.IsVaildDevice())
				{
					throw new Exception("错误");
				}
				int num = array.Length;
				Array.Resize(ref array, num + 1);
				array[num] = detectorSettingRow;
				class56_2.detectorSettingList.Clear();
				for (int i = 0; i < array.Length; i++)
				{
					class56_2.detectorSettingList.Add(array[i]);
				}
				Class49.InsertIntoTable(Class49.string_9[4], Class49.user_0.u_name, string_1 + string_0, "反控:检测器参数设置", "检测器参数设置:" + text);
			}
		});
	}

	private void RefrushEventTable0(AccStyle accStyle_0, InsDeviceManager class56_2)
	{
		if (mForm.CurrentGCID != string_0)
		{
			return;
		}
		mForm.Invoke((MethodInvoker)delegate
		{
			if (accStyle_0 == AccStyle.Read)
			{
				mForm.insDeviceCtrl.ReadEventTable0(class56_2);
			}
			if (accStyle_0 == AccStyle.Write)
			{
				mForm.insDeviceCtrl.WriteEventTable0(class56_2);
				string extEvTPTextInfo = mForm.insDeviceCtrl.GetExtEvTPTextInfo(class56_2);
				Class49.InsertIntoTable(Class49.string_9[4], Class49.user_0.u_name, string_1 + string_0, "反控:外部事件设置", "外部事件设置:" + extEvTPTextInfo);
			}
		});
	}

	private void RefrushEventTable1(AccStyle accStyle_0, InsDeviceManager class56_2)
	{
		if (mForm.CurrentGCID != string_0)
		{
			return;
		}
		mForm.Invoke((MethodInvoker)delegate
		{
			if (accStyle_0 == AccStyle.Read)
			{
				mForm.insDeviceCtrl.ReadEventTable1(class56_2);
			}
			if (accStyle_0 == AccStyle.Write)
			{
				mForm.insDeviceCtrl.WriteEventTable1(class56_2);
				string extEvTPTextInfo = mForm.insDeviceCtrl.GetExtEvTPTextInfo(class56_2);
				Class49.InsertIntoTable(Class49.string_9[4], Class49.user_0.u_name, string_1 + string_0, "反控:外部事件设置", "外部事件设置:" + extEvTPTextInfo);
			}
		});
	}

	private void RefrushTempratureControl(AccStyle accStyle_0, InsDeviceManager class56_2)
	{
		if (mForm.CurrentGCID != string_0)
		{
			return;
		}
		mForm.Invoke((MethodInvoker)delegate
		{
			int currentChannelIndex = mForm.CurrentChannelIndex;
			if (accStyle_0 == AccStyle.Read)
			{
				mForm.insDeviceCtrl.ReadTempratureControl(class56_2);
				GcProgTemp gcProgTemp = new GcProgTemp
				{
					initHoldTime = class56_2.tempHoldTime
				};
				for (int i = 0; i < gcProgTemp.progTempRows.Length; i++)
				{
					gcProgTemp.progTempRows[i].endTemp = class56_2.tempSettingList[i].tempEnd;
					gcProgTemp.progTempRows[i].holdTime = class56_2.tempSettingList[i].tempKeep;
					gcProgTemp.progTempRows[i].upRate = class56_2.tempSettingList[i].tempStart;
				}
				gcProgTemp.SetT6 = class56_2.tempSetedList;
				sglsSampling[0].linkGcProgTemp = gcProgTemp;
				sglsSampling[1].linkGcProgTemp = gcProgTemp;
				sglsSampling[2].linkGcProgTemp = gcProgTemp;
				sglsSampling[3].linkGcProgTemp = gcProgTemp;
				mForm.sampleDisplay.PrepareInfo(gcProgTemp);
			}
			if (accStyle_0 == AccStyle.Write)
			{
				mForm.insDeviceCtrl.WriteTempratureControl(class56_2);
				string tempratureControlTextInfo = mForm.insDeviceCtrl.GetTempratureControlTextInfo(class56_2);
				Class49.InsertIntoTable(Class49.string_9[4], Class49.user_0.u_name, string_1 + string_0, "反控:设置程序升温参数", "设置程序升温:" + tempratureControlTextInfo);
			}
		});
	}

	private float String2Float(object object_0)
	{
		if (object_0 == null)
		{
			throw new Exception("相关字段没有赋值");
		}
		return float.Parse(object_0.ToString());
	}

	internal byte[] CmdNum2Data_AndSend(byte byte_1, InsDeviceManager class56_2, int int_7, bool bool_0)
	{
		byte[] byte_2 = IBrainConvert.String2ByteArray(string_0);
		IBrainConvert.ArrayCopy(ref byte_2, IBrainConvert.Short2ByteArray2(short_0++));
		IBrainConvert.ArrayAdd(ref byte_2, byte_1);
		byte[] byte_3 = CmdNum2Data(byte_1, class56_2, int_7);
		IBrainConvert.ArrayCopy(ref byte_2, byte_3);
		short num = (short)byte_2.Length;
		IBrainConvert.ArrayAdd(ref byte_2, IBrainConvert.BitByBitNo(byte_2, 0, byte_2.Length));
		byte[] byte_4 = Encoding.ASCII.GetBytes("GCKC");
		IBrainConvert.ArrayCopy(ref byte_4, IBrainConvert.Short2ByteArray(num));
		IBrainConvert.ArrayCopy(ref byte_4, byte_2);
		if (bool_0)
		{
			SendData(byte_4);
		}
		return byte_4;
	}

	private byte[] method_15(byte byte_1, byte byte_2)
	{
		switch (byte_1)
		{
		case 22:
			devManager0.sglNumberStart = byte_2;
			return devManager0.GetCurSglNumberStart();
		case 23:
			devManager0.sglNumberEnd = byte_2;
			return devManager0.GetCurSglNumberEnd();
		default:
			return new byte[1] { byte_2 };
		case 0:
			return null;
		}
	}

	private byte[] SendCmd_249()
	{
		int intResult = 0;
		int intResult2 = 0;
		mForm.Invoke((MethodInvoker)delegate
		{
			string s = VocCtrl.vocCtrl.tbFireOn.Text.Trim();
			float result = 0f;
			if (float.TryParse(s, out result))
			{
				fFireOn = result;
				intResult = int.Parse(Math.Round(Convert.ToDouble(result.ToString()) * 10.0, 0).ToString());
			}
			s = VocCtrl.vocCtrl.tbFireOn2.Text.Trim();
			if (float.TryParse(s, out result))
			{
				fFireOn2 = result;
				intResult2 = int.Parse(Math.Round(Convert.ToDouble(result.ToString()) * 10.0, 0).ToString());
			}
		});
		byte[] array = new byte[1] { (byte)intResult };
		byte[] array2 = new byte[1] { (byte)intResult2 };
		int num = array.Length;
		Array.Resize(ref array, num + array2.Length);
		Array.Copy(array2, 0, array, num, array2.Length);
		return array;
	}

	private byte[] SendCmd_11()
	{
		DateTime now = DateTime.Now;
		byte[] result = new byte[6]
		{
			IBrainConvert.Int2Byte(now.Year % 100),
			IBrainConvert.Int2Byte(now.Month),
			IBrainConvert.Int2Byte(now.Day),
			IBrainConvert.Int2Byte(now.Hour),
			IBrainConvert.Int2Byte(now.Minute),
			IBrainConvert.Int2Byte(now.Second)
		};
		Class49.InsertIntoTable(Class49.string_9[4], Class49.user_0.u_name, string_1 + string_0, "反控:校时", "校时:" + now.ToString());
		return result;
	}

	private byte[] SendCmd_12()
	{
		byte[] array = new byte[6];
		mForm.Invoke((MethodInvoker)delegate
		{
			mForm.insDeviceCtrl.WriteAutoInjectorSetting(devManager1);
			byte[] array2 = new byte[2];
			array2 = IBrainConvert.Float2Byte(devManager1.injectInterval, 1);
			array2.CopyTo(array, 0);
			array2 = IBrainConvert.Int2Byte2((int)devManager1.injectNTimes);
			array2.CopyTo(array, 2);
			array[4] = (byte)devManager1.injectSpendTime;
			array[5] = IBrainConvert.Int2Byte((int)devManager1.injectLightTime);
		});
		Class49.InsertIntoTable(Class49.string_9[4], Class49.user_0.u_name, string_1 + string_0, "反控:设置自动进样时间", "设置自动进样时间:" + devManager1.injectLightTime);
		return array;
	}

	private byte[] SendCmd_36()
	{
		byte byteSplitRatio = 0;
		mForm.Invoke((MethodInvoker)delegate
		{
			byteSplitRatio = mForm.insDeviceCtrl.GetEpcSplitRatio();
		});
		return new byte[1] { byteSplitRatio };
	}

	private byte[] SendCmd_37()
	{
		int currentChannelIndex = mForm.CurrentChannelIndex;
		int idxSelTab = mForm.insDeviceCtrl.CurrentEpcIdx;
		byte byteSplitRatio = mForm.insDeviceCtrl.GetEpcSplitRatio();
		byte[] byteEpc = null;
		mForm.Invoke((MethodInvoker)delegate
		{
			mForm.insDeviceCtrl.WriteEpcInfo(devManager0, idxSelTab);
			byteEpc = devManager0.GetEPCDevParam(idxSelTab, byteSplitRatio);
		});
		Class49.InsertIntoTable(Class49.string_9[4], Class49.user_0.u_name, string_1 + string_0, "反控:设定EPC工作参数", "EPC工作参数设定");
		return byteEpc;
	}

	private byte[] SendCmd_65()
	{
		int clmCT6CNIndex = mForm.FTNS.clmCT6CNIndex;
		int clmCT6ENIndex = mForm.FTNS.clmCT6ENIndex;
		string text = "";
		for (int i = 0; i < int_5; i++)
		{
			switch (i)
			{
			case 3:
			{
				object value3 = mForm.FTNS.dgvCT6.Rows[4].Cells[clmCT6CNIndex].Value;
				devManager0.tempCtrlAreaTable.tempList[i].strNameCn = ((value3 != null) ? value3.ToString() : "      ");
				value3 = mForm.FTNS.dgvCT6.Rows[4].Cells[clmCT6ENIndex].Value;
				devManager0.tempCtrlAreaTable.tempList[i].strNameEn = ((value3 != null) ? value3.ToString() : "      ");
				break;
			}
			case 4:
			{
				object value2 = mForm.FTNS.dgvCT6.Rows[3].Cells[clmCT6CNIndex].Value;
				devManager0.tempCtrlAreaTable.tempList[i].strNameCn = ((value2 != null) ? value2.ToString() : "      ");
				value2 = mForm.FTNS.dgvCT6.Rows[3].Cells[clmCT6ENIndex].Value;
				devManager0.tempCtrlAreaTable.tempList[i].strNameEn = ((value2 != null) ? value2.ToString() : "      ");
				break;
			}
			default:
			{
				object value = mForm.FTNS.dgvCT6.Rows[i].Cells[clmCT6CNIndex].Value;
				devManager0.tempCtrlAreaTable.tempList[i].strNameCn = ((value != null) ? value.ToString() : "      ");
				value = mForm.FTNS.dgvCT6.Rows[i].Cells[clmCT6ENIndex].Value;
				devManager0.tempCtrlAreaTable.tempList[i].strNameEn = ((value != null) ? value.ToString() : "      ");
				break;
			}
			}
			text = text + devManager0.tempCtrlAreaTable.tempList[i].strNameCn + devManager0.tempCtrlAreaTable.tempList[i].strNameEn + "\r\n";
		}
		Class49.InsertIntoTable(Class49.string_9[4], Class49.user_0.u_name, string_1 + string_0, "反控:设置控区名称", "设置控区名称" + text);
		return devManager0.tempCtrlAreaTable.Name2Byte(int_5);
	}

	private byte[] SendCmd_67()
	{
		int clmCT6CtrlTIndex = mForm.FTNS.clmCT6CtrlTIndex;
		object value = mForm.FTNS.dgvCT6.Rows[7].Cells[clmCT6CtrlTIndex].Value;
		devManager0.insDevEnable7 = value != null && (bool)value;
		value = mForm.FTNS.dgvCT6.Rows[6].Cells[clmCT6CtrlTIndex].Value;
		devManager0.insDevEnable6 = value != null && (bool)value;
		value = mForm.FTNS.dgvCT6.Rows[5].Cells[clmCT6CtrlTIndex].Value;
		devManager0.insDevEnable5 = value != null && (bool)value;
		value = mForm.FTNS.dgvCT6.Rows[3].Cells[clmCT6CtrlTIndex].Value;
		devManager0.insDevEnable4 = value != null && (bool)value;
		value = mForm.FTNS.dgvCT6.Rows[4].Cells[clmCT6CtrlTIndex].Value;
		devManager0.insDevEnable3 = value != null && (bool)value;
		value = mForm.FTNS.dgvCT6.Rows[2].Cells[clmCT6CtrlTIndex].Value;
		devManager0.insDevEnable2 = value != null && (bool)value;
		value = mForm.FTNS.dgvCT6.Rows[1].Cells[clmCT6CtrlTIndex].Value;
		devManager0.insDevEnable1 = value != null && (bool)value;
		value = mForm.FTNS.dgvCT6.Rows[0].Cells[clmCT6CtrlTIndex].Value;
		devManager0.insDevEnable0 = value != null && (bool)value;
		Class49.InsertIntoTable(Class49.string_9[4], Class49.user_0.u_name, string_1 + string_0, "反控:控温使能设置", "控温使能设置");
		return devManager0.GetInsDevEnable();
	}

	private byte[] AutoInjCtrl()
	{
		byte[] bData = new byte[1];
		bData[0] = byte.MaxValue;
		mForm.Invoke((MethodInvoker)delegate
		{
			bData[0] = mForm.insDeviceCtrl.bData;
		});
		return bData;
	}

	private byte[] SendCmd_80()
	{
		return mForm.insDeviceCtrl.GetSmsSetiingInfo();
	}

	private byte[] SendCmd_Convert(byte byte_1)
	{
		int currentChannelIndex = mForm.CurrentChannelIndex;
		int num = currentChannelIndex;
		AccStyle accStyle = AccStyle.Write;
		switch (byte_1)
		{
		case 0:
			return null;
		case 1:
			return null;
		case 2:
			return null;
		case 100:
			return null;
		case 3:
			return devManager0.GetExeFileNumber();
		case 4:
			return null;
		case 5:
			return null;
		case 6:
			return null;
		case 7:
		{
			string strSerial = "";
			mForm.Invoke((MethodInvoker)delegate
			{
				strSerial = mForm.chrAcqCtrl.tbinsSerial.Text.Trim();
			});
			devManager0.SetInsSerial(strSerial);
			Class49.InsertIntoTable(Class49.string_9[4], Class49.user_0.u_name, string_1 + string_0, "反控:设置设备序列号", "设置设备序列号为:" + strSerial);
			return devManager0.insSerial;
		}
		case 8:
			RefrushTempratureTable(accStyle, devManager0);
			return devManager0.GetTempSetedList();
		case 9:
			RefrushTempratureControl(accStyle, devManager0);
			return devManager0.GetTempSettingList();
		case 10:
			RefrushEventTable0(accStyle, devManager0);
			return devManager0.GetEventTable0();
		case 11:
			return SendCmd_11();
		case 12:
			return SendCmd_12();
		case 13:
			return null;
		case 14:
			method_9(accStyle, devManager0, 0);
			return devManager0.GetDetectorSettingList();
		case 16:
			Class49.InsertIntoTable(Class49.string_9[4], Class49.user_0.u_name, string_1 + string_0, "反控:开始控温", "开始控温");
			return null;
		case 17:
			Class49.InsertIntoTable(Class49.string_9[4], Class49.user_0.u_name, string_1 + string_0, "反控:关闭控温", "关闭控温");
			return null;
		case 18:
			Class49.InsertIntoTable(Class49.string_9[0], Class49.user_0.u_name, string_1 + string_0, "开始分析(全部样品)", "启动全部样品分析");
			return null;
		case 19:
			Class49.InsertIntoTable(Class49.string_9[0], Class49.user_0.u_name, string_1 + string_0, "停止分析(全部样品)", "样品全部分析停止");
			return null;
		case 20:
			Class49.InsertIntoTable(Class49.string_9[4], Class49.user_0.u_name, string_1 + string_0, "反控:启动FID1点火", "启动FID1点火");
			return null;
		case 21:
			Class49.InsertIntoTable(Class49.string_9[4], Class49.user_0.u_name, string_1 + string_0, "反控:启动FID2点火", "启动FID2点火");
			return null;
		case 22:
			devManager0.sglNumberStart = (byte)num;
			Class49.InsertIntoTable(Class49.string_9[0], Class49.user_0.u_name, string_1 + string_0, "开始分析", "启动分析,通道号:" + devManager0.sglNumberStart);
			return devManager0.GetCurSglNumberStart();
		case 23:
			devManager0.sglNumberEnd = (byte)num;
			Class49.InsertIntoTable(Class49.string_9[0], Class49.user_0.u_name, string_1 + string_0, "停止分析", "停止分析,通道号:" + devManager0.sglNumberStart);
			return devManager0.GetCurSglNumberEnd();
		case 24:
			return null;
		case 33:
			return null;
		case 34:
			Class49.InsertIntoTable(Class49.string_9[4], Class49.user_0.u_name, string_1 + string_0, "反控:EPC设定参数设定", "EPC设定参数设定");
			return null;
		case 35:
			return null;
		case 36:
			return SendCmd_36();
		case 37:
			return SendCmd_37();
		case 38:
			return null;
		case 39:
			Class49.InsertIntoTable(Class49.string_9[4], Class49.user_0.u_name, string_1 + string_0, "反控:设置EPC名称", "设置EPC名称:" + Encoding.Default.GetString(devManager0.GetEpcNameList()));
			return devManager0.GetEpcNameList();
		case 40:
			return null;
		case 41:
			devManager0.epcGasType = 0;
			Class49.InsertIntoTable(Class49.string_9[4], Class49.user_0.u_name, string_1 + string_0, "反控:EPC气体种类设置", "设置EPC气体种类:" + devManager0.epcGasType);
			return new byte[1] { devManager0.epcGasType };
		case 48:
			return null;
		case 49:
			IArrayBase.NewArray2(ref devManager0.ipAddressList, 6);
			mForm.Invoke((MethodInvoker)delegate
			{
				Update_From_IpAddress();
			});
			return devManager0.GetIpAddressList();
		case 50:
			Class49.InsertIntoTable(Class49.string_9[4], Class49.user_0.u_name, string_1 + string_0, "反控:设置点火时长", "设置点火时长:" + mForm.SetFireLengthValue);
			return new byte[1] { (byte)mForm.SetFireLengthValue };
		case 53:
			return null;
		case 60:
			return null;
		case 61:
		{
			int idx0 = 0;
			mForm.Invoke((MethodInvoker)delegate
			{
				idx0 = mForm.insDeviceCtrl.comboBox4.SelectedIndex + 1;
			});
			byte b = byte.Parse(idx0.ToString());
			Class49.InsertIntoTable(Class49.string_9[4], Class49.user_0.u_name, string_1 + string_0, "反控:设定工作站量程", "设定工作站量程:" + idx0);
			return new byte[1] { b };
		}
		case 62:
			return null;
		case 63:
			return mForm.insDeviceCtrl.GetSmsAlarm();
		case 64:
			return null;
		case 65:
			return SendCmd_65();
		case 66:
			return null;
		case 69:
			return mForm.insDeviceCtrl.GetFlowState();
		case 67:
			return SendCmd_67();
		case 80:
			return SendCmd_80();
		case 89:
			return null;
		case 97:
			AutoSampleQuery(AccStyle.Write, devManager1);
			Class49.InsertIntoTable(Class49.string_9[4], Class49.user_0.u_name, string_1 + string_0, "反控:自动进样器参数设置", "自动进样器参数设置");
			return devManager1.injectSet.GetByte();
		case 99:
			return AutoInjCtrl();
		default:
			if (byte_1 != 91)
			{
				switch (byte_1)
				{
				case 101:
					RefrushEventTable1(accStyle, devManager0);
					return devManager0.GetEventTable1();
				case 104:
					Class49.InsertIntoTable(Class49.string_9[4], Class49.user_0.u_name, string_1 + string_0, "反控:复位多位阀", "复位多位阀");
					return null;
				case 106:
					method_7(accStyle, devManager0);
					return devManager0.GetTempProtectList();
				case 110:
					return null;
				case 111:
					Class49.InsertIntoTable(Class49.string_9[4], Class49.user_0.u_name, string_1 + string_0, "反控:设置多位阀使能", "设置多位阀使能");
					mForm.Invoke((MethodInvoker)delegate
					{
						devManager0.multivalveEnable = mForm.Fmultivalve.GetCheckBox2Byte();
					});
					return devManager0.multivalveEnable;
				case 249:
				{
					int intResult2 = 0;
					int intResult3 = 0;
					mForm.Invoke((MethodInvoker)delegate
					{
						string s = ChromDeviceCtrl.selfCtrl.tbFireOn.Text.Trim();
						float result = 0f;
						if (float.TryParse(s, out result))
						{
							fFireOn = result;
							intResult2 = int.Parse(Math.Round(Convert.ToDouble(result.ToString()) * 10.0, 0).ToString());
						}
						s = ChromDeviceCtrl.selfCtrl.tbFireOn2.Text.Trim();
						if (float.TryParse(s, out result))
						{
							fFireOn2 = result;
							intResult3 = int.Parse(Math.Round(Convert.ToDouble(result.ToString()) * 10.0, 0).ToString());
						}
					});
					byte[] array2 = new byte[1] { (byte)intResult2 };
					byte[] array3 = new byte[1] { (byte)intResult3 };
					int num2 = array2.Length;
					Array.Resize(ref array2, num2 + array3.Length);
					Array.Copy(array3, 0, array2, num2, array3.Length);
					return array2;
				}
				case 250:
					return null;
				case 252:
				{
					int intResult = 0;
					byte[] array = new byte[3] { 0, 0, 10 };
					intResult = int.Parse(fpdHighValue);
					mForm.Invoke((MethodInvoker)delegate
					{
						if (MicrFPDCtrl.selfCtrl != null)
						{
							if (indexFPDHIGHV == 1)
							{
								intResult = int.Parse(MicrFPDCtrl.selfCtrl.tbHighV.Text.Trim());
							}
							else if (indexFPDHIGHV == 2)
							{
								intResult = int.Parse(MicrFPDCtrl.selfCtrl.tbHighV2.Text.Trim());
							}
						}
					});
					array[0] = (byte)(intResult >> 8);
					array[1] = (byte)intResult;
					array[2] = indexFPDHIGHV;
					return array;
				}
				case 253:
					return new byte[1] { indexFPDHIGHV };
				default:
					return null;
				}
			}
			goto case 90;
		case 90:
			return mForm.insDeviceCtrl.GetLiquidInfo();
		}
	}

	private byte[] CmdNum2Data(byte byte_1, InsDeviceManager class56_2, int int_7)
	{
		return byte_1 switch
		{
			111 => class56_2.multivalveEnable, 
			8 => class56_2.GetTempSetedList(), 
			9 => class56_2.GetTempSettingList(), 
			10 => class56_2.GetEventTable0(), 
			101 => class56_2.GetEventTable1(), 
			37 => class56_2.GetEPCDevParam1(int_7), 
			_ => null, 
		};
	}

	private void method_18(string string_3)
	{
		ChromDevice oneEquip = mForm.FrmEquip.GetOneEquip(mForm.CurrentGCID);
		if (!oneEquip.misMgr.devManager.Msg.AutoSendByStopTime)
		{
			return;
		}
		string mess = oneEquip.misMgr.devManager.Msg.Mess;
		byte[] bytes = Encoding.Default.GetBytes(mess);
		byte[] array = new byte[3];
		if (oneEquip.misMgr.devManager.Msg.sound)
		{
			array[0] = 1;
		}
		else
		{
			array[0] = 0;
		}
		array[1] = (byte)Class49.Object2Int(oneEquip.misMgr.devManager.Msg.soundTimes, 1);
		byte[] array2;
		if (bytes.Length % 24 == 0)
		{
			array[2] = (byte)(int)Math.Ceiling((double)(bytes.Length / 24));
			array2 = bytes;
		}
		else
		{
			array[2] = (byte)(Math.Ceiling((double)(bytes.Length / 24)) + 1.0);
			array2 = new byte[array[2] * 24];
			for (int i = 0; i < array2.Length; i++)
			{
				array2[i] = 32;
			}
			bytes.CopyTo(array2, 0);
		}
		byte[] array3 = new byte[array2.Length + array.Length];
		array.CopyTo(array3, 0);
		array2.CopyTo(array3, 3);
		SendCmd(80, array3);
	}

	private bool CheckDtcChannels(DetectorParse myClass78, string string_3, int chn)
	{
		ArrayList arrayList = new ArrayList();
		byte b = 0;
		for (int int0 = 0; int0 < dtc_Channels.Length; int0++)
		{
			DtC_Channel dtC_Channel = dtc_Channels[int0];
			Signal signal = sglsSampling[int0];
			if (dtC_Channel.mark == 64 || dtC_Channel.mark == 65 || dtC_Channel.mark == 66)
			{
				dtC_Channel.unitStr = "pA";
				dtc_Channels[int0].unitStr = "pA";
				signal.detecter_unit = "pA";
			}
			else
			{
				dtC_Channel.unitStr = "mV";
				dtc_Channels[int0].unitStr = "mV";
				signal.detecter_unit = "mV";
			}
			if (!dtC_Channel.IsGC08)
			{
				continue;
			}
			if (dtC_Channel.mark == 0)
			{
				dtC_Channel.mark = myClass78.byte_2;
				dtC_Channel.name = DetectorSettingRow.GetDeviceTypeNameByIdx(myClass78.byte_2, frmParam.iDetector);
				dtC_Channel.Gc08Values(myClass78.float_0);
				ChromInfoR chromInfoR = dtC_Channel.chromInfoR;
				int byte_ = myClass78.byte_1;
				chromInfoR.UvRange = byte_.ToString();
				dtC_Channel.chromInfoR.UvwsStepFreq = myClass78.byte_0;
				dtC_Channel.unitStr = myClass78.string_0;
				if (myClass78.byte_2 == dtC_Channel.mark)
				{
					for (int i = 0; i < dtc_Channels.Length - 1; i++)
					{
						for (int j = 0; j < dtc_Channels.Length - i - 1 && dtc_Channels[j + 1].mark != 0; j++)
						{
							if (dtc_Channels[j].mark > dtc_Channels[j + 1].mark)
							{
								DtC_Channel dtC_Channel2 = dtc_Channels[j];
								dtc_Channels[j] = dtc_Channels[j + 1];
								dtc_Channels[j + 1] = dtC_Channel2;
							}
						}
					}
				}
				return false;
			}
			b = myClass78.byte_2;
			if (myClass78.byte_2 != dtC_Channel.mark)
			{
				continue;
			}
			dtC_Channel.Gc08Values(myClass78.float_0);
			dtC_Channel.unitStr = myClass78.string_0;
			int_2 = mForm.FrmEquip.GetModBusDeviceIDByEquipID(string_3);
			ChartParaOpera oneEquipSinglePara = mForm.FrmEquip.GetOneEquipSinglePara(string_3, int0);
			ChartParaOpera oneEquipSinglePara2 = mForm.FrmEquip.GetOneEquipSinglePara(string_3, 0);
			if (oneEquipSinglePara.mtdMgr.sigIntegrations.Count > 0)
			{
				Integration integration = oneEquipSinglePara.mtdMgr.sigIntegrations[0];
				if (integration != null)
				{
					signal.RunningInteg = integration;
				}
			}
			int myint_0 = mForm.CurrentChannelIndex;
			for (int k = 0; k < oneEquipSinglePara2.tProgram.Count; k++)
			{
				if (!(oneEquipSinglePara2.tProgram[k].TimeValue >= 0.0) || oneEquipSinglePara2.tProgram[k].TestCard - 1 < 0 || oneEquipSinglePara2.tProgram[k].TestCard - 1 != int0)
				{
					continue;
				}
				if (dtc_Channels[3].mark == 0)
				{
					dtc_Channels[3].mark = 136;
					dtc_Channels[3].name = DetectorSettingRow.GetDeviceTypeNameByIdx(dtc_Channels[3].mark, frmParam.iDetector);
					dtc_Channels[3].Gc08Values(myClass78.float_0);
					int byte_2 = myClass78.byte_1;
					dtc_Channels[3].chromInfoR.UvRange = byte_2.ToString();
					dtc_Channels[3].chromInfoR.UvwsStepFreq = myClass78.byte_0;
					ChartParaOpera oneEquipSinglePara3 = mForm.FrmEquip.GetOneEquipSinglePara(string_3, 3);
					if (oneEquipSinglePara3.mtdMgr != null)
					{
						sglsSampling[3].RunningInteg = oneEquipSinglePara3.mtdMgr.sigIntegrations[0];
					}
				}
				float num = 0f;
				if (sglsSampling[3].dots.Length != 0)
				{
					num = sglsSampling[3].dots[sglsSampling[3].DotsNum - 1].X;
				}
				float num2 = 0f;
				if (k > 0)
				{
					num2 = (float)oneEquipSinglePara2.tProgram[k - 1].TimeValue;
				}
				if (!(num < (float)oneEquipSinglePara2.tProgram[k].TimeValue) || !(num >= num2))
				{
					continue;
				}
				PointF newDot = new PointF(0f, 0f);
				bool mybool_0 = false;
				mForm.Invoke((MethodInvoker)delegate
				{
					if (string_0 == mForm.CurrentGCID && int0 == myint_0)
					{
						mybool_0 = true;
					}
				});
				if (struct0_0.int_0 != int0)
				{
					float_0 = myClass78.float_0[0] - struct0_0.float_0;
					if (struct0_0.int_0 == -1)
					{
						float_0 = 0f;
					}
					struct0_0.int_0 = int0;
				}
				float[] array = (float[])myClass78.float_0.Clone();
				for (int num3 = 0; num3 < array.Length; num3++)
				{
					array[num3] -= float_0;
				}
				sglsSampling[3].AddDots(array, myClass78.byte_0, out newDot, mybool_0, frmParam.iSmooths);
				method_20(newDot);
				struct0_0.float_0 = array[array.Length - 1];
				break;
			}
			if (signal.simple)
			{
				bool flag = false;
				int num4 = oneEquipSinglePara.evenPara.Count - 1;
				while (num4 >= 0 && signal.dots.Length != 0)
				{
					if (!(((double)signal.dots[signal.DotsNum - 1].X >= oneEquipSinglePara.evenPara[num4].TimeStart) & ((double)signal.dots[signal.DotsNum - 1].X < oneEquipSinglePara.evenPara[num4].TimeEnd)))
					{
						num4--;
						continue;
					}
					if (signal.EvenTimeIndex != (float)num4)
					{
						signal.EvenUY = signal.dots[signal.DotsNum - 1].Y;
						signal.EvenTimeIndex = num4;
						signal.deltaEvenUY = 0f;
					}
					for (int num5 = 0; num5 < myClass78.float_0.Length; num5++)
					{
						myClass78.float_0[num5] = signal.EvenUY;
					}
					flag = true;
					break;
				}
				if (!flag && signal.EvenUY != 0f)
				{
					if (signal.deltaEvenUY == 0f)
					{
						signal.deltaEvenUY = myClass78.float_0[0] - signal.EvenUY;
					}
					for (int num6 = 0; num6 < myClass78.float_0.Length; num6++)
					{
						myClass78.float_0[num6] = myClass78.float_0[num6] - signal.deltaEvenUY;
					}
				}
				if (signal.baseLinededuct)
				{
					if (signal.baseLine == null)
					{
						signal.baseLine = Chromatogram.LoadFromFile2(oneEquipSinglePara.TemplatePath, DetectorStyle.General);
					}
					if (!signal.baseLine.fName.Equals(Path.GetFileNameWithoutExtension(oneEquipSinglePara.TemplatePath), StringComparison.OrdinalIgnoreCase))
					{
						signal.baseLine = Chromatogram.LoadFromFile2(oneEquipSinglePara.TemplatePath, DetectorStyle.General);
					}
					for (int num7 = 0; num7 < myClass78.float_0.Length; num7++)
					{
						if (signal.DotsNum != 0 && signal.DotsNum + num7 < signal.baseLine.signal.dots.Length)
						{
							myClass78.float_0[num7] -= signal.baseLine.signal.oriDots[signal.DotsNum + num7].Y;
						}
					}
				}
			}
			bool bAnlyse = false;
			if (string_0 == mForm.CurrentGCID && int0 == myint_0)
			{
				bAnlyse = true;
			}
			PointF newDot2 = default(PointF);
			if (minePlot.minePlotSelf != null)
			{
				arrayList = signal.AddDots(myClass78.float_0, myClass78.byte_0, out newDot2, bAnlyse, b);
				if (arrayList != null)
				{
					minePlot.minePlotSelf.AddDots(myClass78.float_0, b, arrayList);
				}
			}
			else
			{
				signal.AddDots(myClass78.float_0, myClass78.byte_0, out newDot2, bAnlyse, frmParam.iSmooths);
			}
			if (newDot2.X > 20480f)
			{
				signal.ResetOriDots(createDiskFile: true);
			}
			sample_time = newDot2.X;
			if (!sampling)
			{
				idle_time = (float)(Environment.TickCount - beginIdleTC) / 60000f;
			}
			mForm.Invoke((MethodInvoker)delegate
			{
				if (string_3 == mForm.CurrentGCID && myint_0 == int0 && !mForm.chrAcqCtrl.lclCPauseRefresh.Checked && sample_time >= mForm.disLg.LgXEnd)
				{
					mForm.ChangeDisLg();
				}
			});
			if (signal.simple && newDot2.X >= dtC_Channel.chromInfoR.AcqRunTime)
			{
				byte channelMask = (byte)int0;
				SendCmd(245, channelMask);
				Thread.Sleep(100);
				string text = "";
				try
				{
					text = SaveChromFile(int0);
				}
				catch
				{
				}
				method_18(string_0);
				mForm.Invoke((MethodInvoker)delegate
				{
					for (int l = 0; l < sglsSampling.Length; l++)
					{
						if (l >= mForm.tabChannel.TabPages.Count)
						{
							sglsSampling[l].simple = false;
						}
					}
					if (!sglsSampling[0].simple && !sglsSampling[1].simple && !sglsSampling[2].simple && !sglsSampling[3].simple && mForm.CurrentGCID == ID)
					{
						mForm.insDeviceCtrl.UpdateControlAnalyzeText(bCtrl: false);
						if (minePlot.minePlotSelf != null)
						{
							minePlot.minePlotSelf.UpdateControlAnalyzeText(bCtrl: false);
						}
					}
				});
				if (oneEquipSinglePara.mtdMgr != null)
				{
					bgChrom.mtdSetup = oneEquipSinglePara.mtdMgr;
				}
				if (mForm.sampleDisplay.disSignals != null)
				{
					chn = int0;
				}
				else
				{
					LogMgr.Instance.Write2RunLog("TcpServerSocket.disposeVOCPeaks Info:this.mForm.sampleDisplay.disSignals == null");
				}
			}
			if (!signal.simple && newDot2.X > 120f)
			{
				signal.ResetOriDots(createDiskFile: true);
				break;
			}
			return true;
		}
		return true;
	}

	private byte SetModbusWordValue(byte[] byte_1, out int numout)
	{
		for (int i = 0; i < class44_0.float_0.Length; i++)
		{
			bool flag = false;
			if ((byte_1[i * 2] & 0xD0) == 208)
			{
				flag = true;
				byte_1[i * 2] = (byte)(byte_1[i * 2] - 208);
			}
			if (flag)
			{
				class44_0.float_0[i] = 0f - IBrainConvert.ByteArray2Float(byte_1, i * 2, 1);
			}
			else
			{
				class44_0.float_0[i] = IBrainConvert.ByteArray2Float(byte_1, i * 2, 1);
			}
		}
		float[] array = new float[1];
		ushort[] array2 = new ushort[2];
		array = new float[1] { class44_0.float_0[4] };
		System.Buffer.BlockCopy(array, 0, array2, 0, 4);
		mForm.mComModbus.WordVaue[6] = array2[0];
		mForm.mComModbus.WordVaue[7] = array2[1];
		array[0] = class44_0.float_0[2];
		System.Buffer.BlockCopy(array, 0, array2, 0, 4);
		mForm.mComModbus.WordVaue[2] = array2[0];
		mForm.mComModbus.WordVaue[3] = array2[1];
		array[0] = class44_0.float_0[3];
		System.Buffer.BlockCopy(array, 0, array2, 0, 4);
		mForm.mComModbus.WordVaue[4] = array2[0];
		mForm.mComModbus.WordVaue[5] = array2[1];
		array[0] = class44_0.float_0[0];
		System.Buffer.BlockCopy(array, 0, array2, 0, 4);
		mForm.mComModbus.WordVaue[96] = array2[0];
		mForm.mComModbus.WordVaue[97] = array2[1];
		array[0] = class44_0.float_0[1];
		System.Buffer.BlockCopy(array, 0, array2, 0, 4);
		mForm.mComModbus.WordVaue[98] = array2[0];
		mForm.mComModbus.WordVaue[99] = array2[1];
		int j = 0;
		int num = 0;
		for (; j < 2000; j++)
		{
			cdlMgr.tcpServerMgr.modBusData_0.ModBusBytes[num++] = (byte)(cdlMgr.tcpServerMgr.mComModbus.WordVaue[j] / 256);
			cdlMgr.tcpServerMgr.modBusData_0.ModBusBytes[num++] = (byte)(cdlMgr.tcpServerMgr.mComModbus.WordVaue[j] % 256);
		}
		int num2 = 12;
		int num3 = 12;
		num2 = num3 + 1;
		byte byte_2 = byte_1[num3];
		class44_0.bool_1 = IBrainConvert.Byte2Bool(byte_2, 7);
		if (cdlMgr.formMain != null)
		{
			mForm.Invoke(new MethodInvoker(Update_ControlTempButton));
		}
		class44_0.bool_12 = IBrainConvert.Byte2Bool(byte_2, 6);
		Ready = class44_0.bool_12;
		class44_0.bool_14 = IBrainConvert.Byte2Bool(byte_2, 5);
		class44_0.bool_21 = IBrainConvert.Byte2Bool(byte_2, 4);
		class44_0.bool_2 = IBrainConvert.Byte2Bool(byte_2, 3);
		int num4 = 13;
		num2 = num4 + 1;
		byte byte_3 = byte_1[num4];
		class44_0.bool_7 = IBrainConvert.Byte2Bool(byte_3, 7);
		class44_0.bool_5 = IBrainConvert.Byte2Bool(byte_3, 5);
		class44_0.bool_6 = IBrainConvert.Byte2Bool(byte_3, 4);
		class44_0.bool_13 = IBrainConvert.Byte2Bool(byte_3, 3);
		class44_0.bool_3 = IBrainConvert.Byte2Bool(byte_3, 2);
		class44_0.bool_4 = IBrainConvert.Byte2Bool(byte_3, 1);
		class44_0.bool_0 = IBrainConvert.Byte2Bool(byte_3, 0);
		int num5 = 14;
		num2 = num5 + 1;
		byte b = byte_1[num5];
		class44_0.bool_10 = IBrainConvert.Byte2Bool(b, 7);
		class44_0.bool_11 = IBrainConvert.Byte2Bool(b, 6);
		class44_0.bool_9 = IBrainConvert.Byte2Bool(b, 5);
		class44_0.bool_8 = IBrainConvert.Byte2Bool(b, 4);
		class44_0.byte_0 = (byte)(b & 0xE);
		int num6 = 15;
		num2 = num6 + 1;
		byte b2 = byte_1[num6];
		class44_0.bool_16 = IBrainConvert.Byte2Bool(b2, 7);
		class44_0.bool_15 = IBrainConvert.Byte2Bool(b2, 6);
		class44_0.byte_1 = (byte)(b2 & 0xE);
		int num7 = 16;
		num2 = num7 + 1;
		byte b3 = byte_1[num7];
		class44_0.bool_18 = IBrainConvert.Byte2Bool(b3, 7);
		class44_0.bool_17 = IBrainConvert.Byte2Bool(b3, 6);
		class44_0.byte_2 = (byte)(b3 & 0xE);
		int num8 = 17;
		num2 = num8 + 1;
		byte b4 = byte_1[num8];
		class44_0.bool_20 = IBrainConvert.Byte2Bool(b4, 7);
		class44_0.bool_19 = IBrainConvert.Byte2Bool(b4, 6);
		class44_0.byte_3 = (byte)(b4 & 0xE);
		Array.Resize(ref class44_0.class78_0, 0);
		int num9 = 18;
		num2 = num9 + 1;
		numout = num2;
		return byte_1[num9];
	}

	private void method_20(PointF pointF_0)
	{
		int myint0 = 3;
		if (pointF_0.X > 20480f)
		{
			sglsSampling[myint0].ResetOriDots(createDiskFile: true);
		}
		sample_time = pointF_0.X;
		if (!sampling)
		{
			idle_time = (float)(Environment.TickCount - beginIdleTC) / 60000f;
		}
		mForm.Invoke((MethodInvoker)delegate
		{
			int currentChannelIndex = mForm.CurrentChannelIndex;
			if (string_0 == mForm.CurrentGCID && currentChannelIndex == myint0 && !mForm.chrAcqCtrl.lclCPauseRefresh.Checked)
			{
				while (sample_time >= mForm.disLg.LgXEnd)
				{
					mForm.ChangeDisLg();
				}
			}
		});
		if (sglsSampling[myint0].simple && pointF_0.X >= dtc_Channels[myint0].chromInfoR.AcqRunTime)
		{
			if (sglsSampling[myint0].simple)
			{
				mForm.Invoke((MethodInvoker)delegate
				{
					if (mForm.CurrentGCID == ID)
					{
						mForm.chrAcqCtrl.tsStart.Enabled = true;
						mForm.chrAcqCtrl.tsstop.Enabled = false;
					}
					string chromFileName = GetChromFileName(myint0);
					Save(sglsSampling[myint0], chromFileName, mForm.FrmEquip.GetDEVICE(ID, myint0), myint0, mForm.FrmEquip.GetNameByID(ID));
				});
				sglsSampling[myint0].simple = false;
				mForm.Invoke((MethodInvoker)delegate
				{
					if (mForm.tabChannel != null)
					{
						for (int i = 0; i < sglsSampling.Length; i++)
						{
							if (i >= mForm.tabChannel.TabPages.Count)
							{
								sglsSampling[i].simple = false;
							}
						}
						if (!sglsSampling[0].simple && !sglsSampling[1].simple && !sglsSampling[2].simple && !sglsSampling[3].simple && mForm.CurrentGCID == ID)
						{
							mForm.insDeviceCtrl.UpdateControlAnalyzeText(bCtrl: false);
							if (minePlot.minePlotSelf != null)
							{
								minePlot.minePlotSelf.UpdateControlAnalyzeText(bCtrl: false);
							}
						}
					}
				});
			}
			method_18(string_0);
		}
		if (!sglsSampling[myint0].simple && pointF_0.X > 120f)
		{
			sglsSampling[myint0].ResetOriDots(createDiskFile: true);
		}
	}

	private void method_21(Signal signal_0, float[] float_1, ChartParaOpera chartParaOpera_0, DtC_Channel dtC_Channel_0, int int_7, byte byte_1)
	{
		bool mybool0 = false;
		int myint0 = mForm.CurrentChannelIndex;
		int num = chartParaOpera_0.evenPara.Count - 1;
		while (num >= 0)
		{
			if (signal_0.dots.Length != 0)
			{
				if (!(((double)signal_0.dots[signal_0.DotsNum - 1].X >= chartParaOpera_0.evenPara[num].TimeStart) & ((double)signal_0.dots[signal_0.DotsNum - 1].X < chartParaOpera_0.evenPara[num].TimeEnd)))
				{
					num--;
					continue;
				}
				if (signal_0.EvenTimeIndex != (float)num)
				{
					signal_0.EvenUY = signal_0.dots[signal_0.DotsNum - 1].Y;
					signal_0.EvenTimeIndex = num;
				}
			}
			for (int i = 0; i < float_1.Length; i++)
			{
				float_1[i] -= signal_0.EvenUY;
			}
			mybool0 = false;
			mForm.Invoke((MethodInvoker)delegate
			{
				if (string_0 == mForm.CurrentGCID && int_7 == myint0)
				{
					mybool0 = true;
				}
			});
			signal_0.AddDots(float_1, detectorParseList[int_7].byte_0, out var newDot, mybool0, frmParam.iSmooths);
			if (newDot.X > 20480f)
			{
				signal_0.ResetOriDots(createDiskFile: true);
			}
			sample_time = newDot.X;
			if (!sampling)
			{
				idle_time = (float)(Environment.TickCount - beginIdleTC) / 60000f;
			}
			mForm.Invoke((MethodInvoker)delegate
			{
				if (string_0 == mForm.CurrentGCID && myint0 == int_7 && !mForm.chrAcqCtrl.lclCPauseRefresh.Checked)
				{
					while (sample_time >= mForm.disLg.LgXEnd)
					{
						mForm.ChangeDisLg();
					}
				}
			});
			if (signal_0.simple && newDot.X >= dtC_Channel_0.chromInfoR.AcqRunTime)
			{
				mForm.Invoke((MethodInvoker)delegate
				{
					if (signal_0.simple)
					{
						if (mForm.CurrentGCID == ID)
						{
							mForm.chrAcqCtrl.tsStart.Enabled = true;
							mForm.chrAcqCtrl.tsstop.Enabled = false;
						}
						string chromFileName = GetChromFileName(int_7);
						Save(signal_0, chromFileName, mForm.FrmEquip.GetDEVICE(ID, int_7), int_7, mForm.FrmEquip.GetNameByID(ID));
						signal_0.simple = false;
					}
				});
				method_18(string_0);
			}
			if (!signal_0.simple && newDot.X > 120f)
			{
				signal_0.ResetOriDots(createDiskFile: true);
			}
			break;
		}
	}

	public int SendData(byte[] dataBuff)
	{
		int result = 0;
		try
		{
			if (IsComClient)
			{
				serialPortClient_0.SendData(dataBuff);
				result = dataBuff.Length;
			}
			else
			{
				result = _socket.Send(dataBuff);
			}
		}
		catch (Exception ex)
		{
			DisConnect = true;
			Program.WriteLine("TcpServerSocket，SendData错误{0}", ex.Message);
		}
		return result;
	}

	public void SendCmd(byte byte_1)
	{
		try {
			if (byte_1 == 18 || byte_1 == 22) {
				string logPath = System.IO.Path.Combine(System.Windows.Forms.Application.StartupPath, "AutoInjDebug.txt");
				System.IO.File.AppendAllText(logPath, $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] TcpServerSocket.SendCmd({byte_1}) called. ID={ID}\r\n");
			}
		} catch {}
		byte[] byte_2 = IBrainConvert.String2ByteArray(string_0);
		IBrainConvert.ArrayCopy(ref byte_2, IBrainConvert.Short2ByteArray2(short_0++));
		IBrainConvert.ArrayAdd(ref byte_2, byte_1);
		byte[] byte_3 = SendCmd_Convert(byte_1);
		IBrainConvert.ArrayCopy(ref byte_2, byte_3);
		short num = (short)byte_2.Length;
		IBrainConvert.ArrayAdd(ref byte_2, IBrainConvert.BitByBitNo(byte_2, 0, byte_2.Length));
		byte[] byte_4 = Encoding.ASCII.GetBytes("GCKC");
		IBrainConvert.ArrayCopy(ref byte_4, IBrainConvert.Short2ByteArray(num));
		IBrainConvert.ArrayCopy(ref byte_4, byte_2);
		SendData(byte_4);
	}

	public void UdpDebug(byte[] DBuffer)
	{
		UdpClient udpClient = new UdpClient(new IPEndPoint(IPAddress.Any, 0));
		IPEndPoint endPoint = new IPEndPoint(IPAddress.Parse("255.255.255.255"), 4800);
		string s = "**##";
		byte[] byte_ = Encoding.Default.GetBytes(s);
		IBrainConvert.ArrayCopy(ref byte_, DBuffer);
		udpClient.Send(byte_, byte_.Length, endPoint);
	}

	public void SendEPCCmd(byte byte_1, byte Type)
	{
		byte[] byte_2 = IBrainConvert.String2ByteArray(string_0);
		IBrainConvert.ArrayCopy(ref byte_2, IBrainConvert.Short2ByteArray2(short_0++));
		IBrainConvert.ArrayAdd(ref byte_2, byte_1);
		byte[] byte_3 = new byte[1] { Type };
		IBrainConvert.ArrayCopy(ref byte_2, byte_3);
		short num = (short)byte_2.Length;
		IBrainConvert.ArrayAdd(ref byte_2, IBrainConvert.BitByBitNo(byte_2, 0, byte_2.Length));
		byte[] byte_4 = Encoding.ASCII.GetBytes("GCKC");
		IBrainConvert.ArrayCopy(ref byte_4, IBrainConvert.Short2ByteArray(num));
		IBrainConvert.ArrayCopy(ref byte_4, byte_2);
		SendData(byte_4);
	}

	public void SendCmd(byte byte_1, byte[] cmdData)
	{
		byte[] byte_2 = IBrainConvert.String2ByteArray(string_0);
		IBrainConvert.ArrayCopy(ref byte_2, IBrainConvert.Short2ByteArray2(short_0++));
		IBrainConvert.ArrayAdd(ref byte_2, byte_1);
		IBrainConvert.ArrayCopy(ref byte_2, cmdData);
		short num = (short)byte_2.Length;
		IBrainConvert.ArrayAdd(ref byte_2, IBrainConvert.BitByBitNo(byte_2, 0, byte_2.Length));
		byte[] byte_3 = Encoding.ASCII.GetBytes("GCKC");
		IBrainConvert.ArrayCopy(ref byte_3, IBrainConvert.Short2ByteArray(num));
		IBrainConvert.ArrayCopy(ref byte_3, byte_2);
		SendData(byte_3);
	}

	public void SendCmd(byte byte_1, byte ChannelMask)
	{
		byte[] byte_2 = IBrainConvert.String2ByteArray(string_0);
		IBrainConvert.ArrayCopy(ref byte_2, IBrainConvert.Short2ByteArray2(short_0++));
		IBrainConvert.ArrayAdd(ref byte_2, byte_1);
		byte[] byte_3 = method_15(byte_1, ChannelMask);
		IBrainConvert.ArrayCopy(ref byte_2, byte_3);
		short num = (short)byte_2.Length;
		IBrainConvert.ArrayAdd(ref byte_2, IBrainConvert.BitByBitNo(byte_2, 0, byte_2.Length));
		byte[] byte_4 = Encoding.ASCII.GetBytes("GCKC");
		IBrainConvert.ArrayCopy(ref byte_4, IBrainConvert.Short2ByteArray(num));
		IBrainConvert.ArrayCopy(ref byte_4, byte_2);
		SendData(byte_4);
	}

	public string GetFileLongName(string EquipName, int ChannelIndex, string ChannelName, string filename, int injNo, int vialNo, int SaveIndex)
	{
		return mForm.fset.GetFileLongName(EquipName, ChannelIndex, ChannelName, filename, injNo, vialNo, SaveIndex);
	}

	public string GetFileName(string path, ChartParaOpera Device, string EquipName, int ChannelIndex, string ChannelName, string filename, int injNo, int vialNo, int SaveIndex)
	{
		string text = "";
		if (Device.FileNameAquipName)
		{
			text = text + EquipName + "_";
		}
		if (Device.FileNameChannelName)
		{
			text = text + ChannelName + "_";
		}
		if (Device.FileNameDateTime)
		{
			text = text + DateTime.Now.ToString("yyyyMMdd-HHmmss") + "_";
		}
		text = ((!(Device.FileUserSet.Trim() != "")) ? (text + Lang.PS("_未定义", "_Undefined")) : (text + Device.FileUserSet.Trim()));
		if (Device.FileNameAutoInject)
		{
			text = text + "_" + vialNo.ToString("000") + "_" + injNo.ToString("000");
		}
		if (Device.InjectIndex)
		{
			SaveIndex = 0;
			if (Directory.Exists(path))
			{
				DirectoryInfo directoryInfo = new DirectoryInfo(path);
				FileInfo[] files = directoryInfo.GetFiles(text + "*.sda");
				if (files.Length == 0)
				{
					SaveIndex = 0;
				}
				else
				{
					string text2 = files[0].Name.Replace(".sda", "");
					string[] array = text2.Split('_');
					if (array.Length != 0)
					{
						SaveIndex = Class49.Object2Int(array[array.Length - 1], 0);
					}
				}
				for (int i = 1; i < files.Length; i++)
				{
					string text3 = files[i].Name.Replace(".sda", "");
					string[] array2 = text3.Split('_');
					if (array2.Length != 0)
					{
						int num = Class49.Object2Int(array2[array2.Length - 1], 0);
						if (SaveIndex < num)
						{
							SaveIndex = num;
						}
					}
				}
			}
			SaveIndex++;
			text = text + "_" + SaveIndex.ToString("0000");
		}
		return text + ".sda";
	}

	public string GetChromFileFolder(int iChannel)
	{
		string text = "Projects\\" + string_0 + "\\" + DateTime.Now.Date.ToString("yyyy-MM-dd") + "\\" + string_0 + "_" + DateTime.Now.ToString("yyyy-MM-dd_HH_mm_ss") + "_" + devManager0.sglNumberEnd + ".sda";
		string name = cdlMgr.GetChromDevice(ID).info.Name;
		int modBusDeviceID = cdlMgr.GetChromDevice(ID).info.ModBusDeviceID;
		ChartParaOpera chartParaOpera = cdlMgr.GetChartParaOpera(ID, iChannel);
		string filename = CMS_InfoParasFMT.FmtStr(2, bgChrom.signal.runningInjInfo, modBusDeviceID, name, "");
		return GetFileLongName(name, iChannel, GetChannelName(iChannel), filename, Class49.Object2Int(devManager1.injectNeedleNum, 0), Class49.Object2Int(devManager1.injectBotNum, 0), sglsSampling[iChannel].SaveIndex++);
	}

	private string snapGetChromFileName(int iChannel)
	{
		string text = Application.StartupPath + "\\snap\\快照_";
		string name = cdlMgr.GetChromDevice(ID).info.Name;
		int modBusDeviceID = cdlMgr.GetChromDevice(ID).info.ModBusDeviceID;
		ChartParaOpera chartParaOpera = cdlMgr.GetChartParaOpera(ID, iChannel);
		string filename = CMS_InfoParasFMT.FmtStr(2, bgChrom.signal.runningInjInfo, modBusDeviceID, name, "");
		return text + GetFileName(text, chartParaOpera, name, iChannel, GetChannelName(iChannel), filename, Class49.Object2Int(devManager1.injectNeedleNum, 0), Class49.Object2Int(devManager1.injectBotNum, 0), sglsSampling[iChannel].SaveIndex++);
	}

	private string GetChromFileName(int iChannel)
	{
		string text = "Projects\\" + string_0 + "\\" + DateTime.Now.Date.ToString("yyyy-MM-dd") + "\\" + string_0 + "_" + DateTime.Now.ToString("yyyy-MM-dd_HH_mm_ss") + "_" + devManager0.sglNumberEnd + ".sda";
		string name = cdlMgr.GetChromDevice(ID).info.Name;
		int modBusDeviceID = cdlMgr.GetChromDevice(ID).info.ModBusDeviceID;
		ChartParaOpera chartParaOpera = cdlMgr.GetChartParaOpera(ID, iChannel);
		string filename = CMS_InfoParasFMT.FmtStr(2, bgChrom.signal.runningInjInfo, modBusDeviceID, name, "");
		text = GetFileLongName(name, iChannel, GetChannelName(iChannel), filename, Class49.Object2Int(devManager1.injectNeedleNum, 0), Class49.Object2Int(devManager1.injectBotNum, 0), sglsSampling[iChannel].SaveIndex++);
		if (OnlineCtrl.selfCtrl != null)
		{
			try
			{
				text = text + "流路" + OnlineCtrl.selfCtrl.currentFlowPath + "_";
			}
			catch (Exception)
			{
				text += "流路x_";
			}
		}
		text += GetFileName(text, chartParaOpera, name, iChannel, GetChannelName(iChannel), filename, Class49.Object2Int(devManager1.injectNeedleNum, 0), Class49.Object2Int(devManager1.injectBotNum, 0), sglsSampling[iChannel].SaveIndex++);
		if (bAutoCalibra)
		{
			text = sysParam.strDirOptionInitDir + "\\" + name + "\\" + strCalibraDir + "\\通道" + iChannel + "\\标气" + iLevel + "\\" + DateTime.Now.ToString("yyyy-MM-dd_HH_mm_ss") + ".sda";
		}
		return text;
	}

	private string GetChannelName(int iChannel)
	{
		string text = "";
		switch (iChannel)
		{
		case 0:
			text = sysParam.strFileNameOptionChannel0Custom;
			break;
		case 1:
			text = sysParam.strFileNameOptionChannel1Custom;
			break;
		case 2:
			text = sysParam.strFileNameOptionChannel2Custom;
			break;
		}
		return dtc_Channels[iChannel].name;
	}

	public void snapChromFile(int iChannel)
	{
		string text = "";
		if (sglsSampling[iChannel].simple)
		{
			text = snapGetChromFileName(iChannel);
			snapSave(sglsSampling[iChannel], text, cdlMgr.GetChartParaOpera(ID, iChannel), iChannel, cdlMgr.GetChromDevice(ID).info.Name);
		}
	}

	public string SaveChromFile(int iChannel)
	{
		string text = "";
		if (sglsSampling[iChannel].simple)
		{
			text = GetChromFileName(iChannel);
			Save(sglsSampling[iChannel], text, cdlMgr.GetChartParaOpera(ID, iChannel), iChannel, cdlMgr.GetChromDevice(ID).info.Name);
			sglsSampling[iChannel].simple = false;
		}
		mForm.Invoke((MethodInvoker)delegate
		{
			for (int i = 0; i < sglsSampling.Length; i++)
			{
				if (i >= mForm.tabChannel.TabPages.Count)
				{
					sglsSampling[i].simple = false;
				}
			}
			if (!sglsSampling[0].simple && !sglsSampling[1].simple && !sglsSampling[2].simple && !sglsSampling[3].simple && mForm.CurrentGCID == ID)
			{
				mForm.insDeviceCtrl.UpdateControlAnalyzeText(bCtrl: false);
				if (minePlot.minePlotSelf != null)
				{
					minePlot.minePlotSelf.UpdateControlAnalyzeText(bCtrl: false);
				}
			}
		});
		return text;
	}

	public void snapSave(Signal OneChannelData, string SaveFilePath, ChartParaOpera Cpara, int ChannelIndex, string Name)
	{
		try
		{
			ChannelChartPara oneEquipPara = mForm.FrmEquip.GetOneEquipPara(ID, ChannelIndex);
			MtdSetup mtdMgr = Cpara.mtdMgr;
			if (OneChannelData.DotsNum < 10)
			{
				return;
			}
			Chromatogram chromatogram = new Chromatogram();
			chromatogram.cus1_name = Cpara.FileUserSet.Trim();
			for (int i = 0; i < mtdMgr.chromInfoR.GcProgTemp.SetT6.Length; i++)
			{
				object obj = devManager1.tempSetedList[i];
				if (obj != null)
				{
					mtdMgr.chromInfoR.GcProgTemp.SetT6[i] = Class49.String2Float(obj, 0f);
				}
			}
			for (int j = 0; j < mtdMgr.chromInfoR.GcProgTemp.progTempRows.Length; j++)
			{
				object object_ = devManager1.tempSettingList[j].tempEnd;
				object object_2 = devManager1.tempSettingList[j].tempStart;
				object object_3 = devManager1.tempSettingList[j].tempKeep;
				mtdMgr.chromInfoR.GcProgTemp.progTempRows[j].endTemp = Class49.String2Float(object_, 0f);
				mtdMgr.chromInfoR.GcProgTemp.progTempRows[j].upRate = Class49.String2Float(object_2, 0f);
				mtdMgr.chromInfoR.GcProgTemp.progTempRows[j].holdTime = Class49.String2Float(object_3, 0f);
			}
			if (devManager1.detectorSettingList.Count > ChannelIndex)
			{
				mtdMgr.chromInfoR.DtcAcquisition.AcqRate = devManager1.detectorSettingList[ChannelIndex].GetFreq() * 10;
			}
			chromatogram.disLg.lgXBeg = 0f;
			chromatogram.disLg.lgX = oneEquipPara.fullScreenTime;
			chromatogram.disLg.lgYBeg = oneEquipPara.showLowLimit;
			chromatogram.disLg.lgY = oneEquipPara.showHighLimit - oneEquipPara.showLowLimit;
			chromatogram.injAnalysis = OneChannelData.runningInjInfo;
			chromatogram.injAnalysis.injNo = Class49.Object2Int(devManager1.injectNeedleNum, 0);
			chromatogram.injAnalysis.vialNo = Class49.Object2Int(devManager1.injectBotNum, 0);
			chromatogram.mtdSetup = mtdMgr;
			Array.Resize(ref chromatogram.userArchives, 1);
			UserArchive userArchive = (chromatogram.userArchives[0] = new UserArchive());
			userArchive.userName = Class49.user_0.u_name;
			userArchive.chromInfo.LoadFromObject(mtdMgr.chromInfo);
			userArchive.openTime = DateTime.Now;
			userArchive.saveTime = DateTime.Now;
			userArchive.chromInfo = mtdMgr.chromInfo;
			if (dtc_Channels[ChannelIndex].unitStr == "pA")
			{
				chromatogram.canSetRs = false;
			}
			else
			{
				chromatogram.canSetRs = true;
			}
			chromatogram.injAnalysis.dtAcquire = DateTime.Now;
			chromatogram.signal = OneChannelData;
			userArchive.integ.LoadFromObject(OneChannelData.RunningInteg);
			if (Cpara.mtdMgr.printPara == null)
			{
				Cpara.mtdMgr.printPara = new PrintPara();
			}
			if (Cpara.mtdMgr.printPara.UseUserZeroTime)
			{
				IntegRow integRow = new IntegRow
				{
					oprtStyle = IntegOprtStyle.DtecDelay,
					value = 0f
				};
				double num = 10000.0;
				for (int k = 0; k < OneChannelData.peaks.Length; k++)
				{
					if ((double)OneChannelData.peaks[k].pkRT > Cpara.mtdMgr.printPara.ZeroTime - Cpara.mtdMgr.printPara.ZeroTimeLeft && (double)OneChannelData.peaks[k].pkRT < Cpara.mtdMgr.printPara.ZeroTime + Cpara.mtdMgr.printPara.ZeroTimeRight && num > Math.Abs((double)OneChannelData.peaks[k].pkRT - Cpara.mtdMgr.printPara.ZeroTime))
					{
						integRow.value = (float)((double)OneChannelData.peaks[k].pkRT - Cpara.mtdMgr.printPara.ZeroTime);
						userArchive.integ.AppendRow(integRow);
						num = Math.Abs((double)OneChannelData.peaks[k].pkRT - Cpara.mtdMgr.printPara.ZeroTime);
					}
				}
			}
			devManager1.insDevEnable0 = mForm.MainmstSet.checkBox4.Checked;
			devManager1.insDevEnable1 = mForm.MainmstSet.checkBox5.Checked;
			devManager1.insDevEnable2 = mForm.MainmstSet.checkBox7.Checked;
			devManager1.insDevEnable3 = mForm.MainmstSet.checkBox6.Checked;
			chromatogram.Process(InstruStyle.GC);
			chromatogram.bEnNMHC = false;
			chromatogram.injAnalysis.dtAcquire = DateTime.Now;
			chromatogram.chromInfo.cclDescription = Name;
			chromatogram.injAnalysis = OneChannelData.runningInjInfo;
			chromatogram.injAnalysis.injNo = Class49.Object2Int(devManager1.injectNeedleNum, 0);
			chromatogram.injAnalysis.vialNo = Class49.Object2Int(devManager1.injectBotNum, 0);
			chromatogram.ChromPPara = mtdMgr.printPara;
			chromatogram.chromInfoR.mtdFileName = mtdMgr.strMtdShowName;
			if (cH4Param.bSeq)
			{
				string[] array = SaveFilePath.Split('\\');
				switch (ChannelIndex)
				{
				case 0:
					if (cntSeq1 <= cH4Param.strSeqName1.Length)
					{
						array[array.Length - 1] = cH4Param.strSeqName1[cntSeq1 - 1] + ".sda";
						cntSeq1++;
					}
					break;
				case 1:
					if (cntSeq2 <= cH4Param.strSeqName2.Length)
					{
						array[array.Length - 1] = cH4Param.strSeqName2[cntSeq2 - 1] + ".sda";
						cntSeq2++;
					}
					break;
				case 2:
					if (cntSeq3 <= cH4Param.strSeqName3.Length)
					{
						array[array.Length - 1] = cH4Param.strSeqName3[cntSeq2 - 1] + ".sda";
						cntSeq3++;
					}
					break;
				case 4:
					if (cntSeq4 <= cH4Param.strSeqName4.Length)
					{
						array[array.Length - 1] = cH4Param.strSeqName4[cntSeq4 - 1] + ".sda";
						cntSeq4++;
					}
					break;
				}
				SaveFilePath = "";
				for (int l = 0; l < array.Length; l++)
				{
					SaveFilePath += array[l];
					if (l != array.Length - 1)
					{
						SaveFilePath += "\\";
					}
				}
			}
			chromatogram.SaveToFileOld(SaveFilePath);
			mForm.Invoke((MethodInvoker)delegate
			{
				try
				{
					if (ChromForm.form == null)
					{
						ChromForm.form = new ChromForm();
					}
					ChromForm.form.Show();
					ChromForm.form.TopMost = true;
					ChromForm.form.OpenChrom(SaveFilePath, sampling: false, useCurrent: true);
					ChromForm.form.TopMost = false;
				}
				catch (Exception ex2)
				{
					LogMgr.Instance.LogError($"ChromForm.form.OpenChrom{SaveFilePath}");
					LogMgr.Instance.LogError($"ChromForm.form.OpenChrom{ex2.Message}");
					LogMgr.Instance.LogError($"ChromForm.form.OpenChrom{ex2.StackTrace}");
				}
			});
			for (int num2 = 0; num2 < chromatogram.RltPeaks.Length; num2++)
			{
			}
			Class49.InsertIntoTable(Class49.string_9[2], Class49.user_0.u_name, string_0 + string_1, "存储快照谱图", "通道:" + ChannelIndex + " 分析结束生成新谱图:" + SaveFilePath);
		}
		catch (Exception ex)
		{
			LogMgr.Instance.LogError(ex.Message);
		}
	}

	public void Save(Signal OneChannelData, string SaveFilePath, ChartParaOpera Cpara, int ChannelIndex, string Name)
	{
		try
		{
			ChannelChartPara oneEquipPara = mForm.FrmEquip.GetOneEquipPara(ID, ChannelIndex);
			MtdSetup mtdMgr = Cpara.mtdMgr;
			if (OneChannelData.DotsNum < 10)
			{
				return;
			}
			Chromatogram chromatogram = new Chromatogram();
			chromatogram.cus1_name = Cpara.FileUserSet.Trim();
			for (int i = 0; i < mtdMgr.chromInfoR.GcProgTemp.SetT6.Length; i++)
			{
				object obj = devManager1.tempSetedList[i];
				if (obj != null)
				{
					mtdMgr.chromInfoR.GcProgTemp.SetT6[i] = Class49.String2Float(obj, 0f);
				}
			}
			for (int j = 0; j < mtdMgr.chromInfoR.GcProgTemp.progTempRows.Length; j++)
			{
				object object_ = devManager1.tempSettingList[j].tempEnd;
				object object_2 = devManager1.tempSettingList[j].tempStart;
				object object_3 = devManager1.tempSettingList[j].tempKeep;
				mtdMgr.chromInfoR.GcProgTemp.progTempRows[j].endTemp = Class49.String2Float(object_, 0f);
				mtdMgr.chromInfoR.GcProgTemp.progTempRows[j].upRate = Class49.String2Float(object_2, 0f);
				mtdMgr.chromInfoR.GcProgTemp.progTempRows[j].holdTime = Class49.String2Float(object_3, 0f);
			}
			if (devManager1.detectorSettingList.Count > ChannelIndex)
			{
				mtdMgr.chromInfoR.DtcAcquisition.AcqRate = devManager1.detectorSettingList[ChannelIndex].GetFreq() * 10;
			}
			chromatogram.disLg.lgXBeg = 0f;
			chromatogram.disLg.lgX = oneEquipPara.fullScreenTime;
			chromatogram.disLg.lgYBeg = oneEquipPara.showLowLimit;
			chromatogram.disLg.lgY = oneEquipPara.showHighLimit - oneEquipPara.showLowLimit;
			chromatogram.injAnalysis = OneChannelData.runningInjInfo;
			chromatogram.injAnalysis.injNo = Class49.Object2Int(devManager1.injectNeedleNum, 0);
			chromatogram.injAnalysis.vialNo = Class49.Object2Int(devManager1.injectBotNum, 0);
			chromatogram.mtdSetup = mtdMgr;
			Array.Resize(ref chromatogram.userArchives, 1);
			UserArchive userArchive = (chromatogram.userArchives[0] = new UserArchive());
			userArchive.userName = Class49.user_0.u_name;
			userArchive.chromInfo.LoadFromObject(mtdMgr.chromInfo);
			userArchive.openTime = DateTime.Now;
			userArchive.saveTime = DateTime.Now;
			userArchive.chromInfo = mtdMgr.chromInfo;
			if (dtc_Channels[ChannelIndex].unitStr == "pA")
			{
				chromatogram.canSetRs = false;
			}
			else
			{
				chromatogram.canSetRs = true;
			}
			chromatogram.injAnalysis.dtAcquire = DateTime.Now;
			chromatogram.signal = OneChannelData;
			userArchive.integ.LoadFromObject(OneChannelData.RunningInteg);
			if (Cpara.mtdMgr.printPara == null)
			{
				Cpara.mtdMgr.printPara = new PrintPara();
			}
			if (Cpara.mtdMgr.printPara.UseUserZeroTime)
			{
				IntegRow integRow = new IntegRow
				{
					oprtStyle = IntegOprtStyle.DtecDelay,
					value = 0f
				};
				double num = 10000.0;
				for (int k = 0; k < OneChannelData.peaks.Length; k++)
				{
					if ((double)OneChannelData.peaks[k].pkRT > Cpara.mtdMgr.printPara.ZeroTime - Cpara.mtdMgr.printPara.ZeroTimeLeft && (double)OneChannelData.peaks[k].pkRT < Cpara.mtdMgr.printPara.ZeroTime + Cpara.mtdMgr.printPara.ZeroTimeRight && num > Math.Abs((double)OneChannelData.peaks[k].pkRT - Cpara.mtdMgr.printPara.ZeroTime))
					{
						integRow.value = (float)((double)OneChannelData.peaks[k].pkRT - Cpara.mtdMgr.printPara.ZeroTime);
						userArchive.integ.AppendRow(integRow);
						num = Math.Abs((double)OneChannelData.peaks[k].pkRT - Cpara.mtdMgr.printPara.ZeroTime);
					}
				}
			}
			devManager1.insDevEnable0 = mForm.MainmstSet.checkBox4.Checked;
			devManager1.insDevEnable1 = mForm.MainmstSet.checkBox5.Checked;
			devManager1.insDevEnable2 = mForm.MainmstSet.checkBox7.Checked;
			devManager1.insDevEnable3 = mForm.MainmstSet.checkBox6.Checked;
			chromatogram.Process(InstruStyle.GC);
			chromatogram.bEnNMHC = false;
			chromatogram.injAnalysis.dtAcquire = DateTime.Now;
			chromatogram.chromInfo.cclDescription = Name;
			chromatogram.injAnalysis = OneChannelData.runningInjInfo;
			chromatogram.injAnalysis.injNo = Class49.Object2Int(devManager1.injectNeedleNum, 0);
			chromatogram.injAnalysis.vialNo = Class49.Object2Int(devManager1.injectBotNum, 0);
			chromatogram.ChromPPara = mtdMgr.printPara;
			chromatogram.chromInfoR.mtdFileName = mtdMgr.strMtdShowName;
			if (cH4Param.bSeq)
			{
				string[] array = SaveFilePath.Split('\\');
				if (ChannelIndex == 0)
				{
					if (cntSeq1 <= cH4Param.strSeqName1.Length)
					{
						array[array.Length - 1] = cH4Param.strSeqName1[cntSeq1 - 1] + ".sda";
						cntSeq1++;
					}
				}
				else if (ChannelIndex == 1)
				{
					if (cntSeq2 <= cH4Param.strSeqName2.Length)
					{
						array[array.Length - 1] = cH4Param.strSeqName2[cntSeq2 - 1] + ".sda";
						cntSeq2++;
					}
				}
				else if (ChannelIndex == 2)
				{
					if (cntSeq3 <= cH4Param.strSeqName3.Length)
					{
						array[array.Length - 1] = cH4Param.strSeqName3[cntSeq2 - 1] + ".sda";
						cntSeq3++;
					}
				}
				else if (ChannelIndex == 4 && cntSeq4 <= cH4Param.strSeqName4.Length)
				{
					array[array.Length - 1] = cH4Param.strSeqName4[cntSeq4 - 1] + ".sda";
					cntSeq4++;
				}
				SaveFilePath = "";
				for (int l = 0; l < array.Length; l++)
				{
					SaveFilePath += array[l];
					if (l != array.Length - 1)
					{
						SaveFilePath += "\\";
					}
				}
			}
			chromatogram.SaveToFileOld(SaveFilePath);
			bgChromModbus = (bgChrom = chromatogram);
			Printpath = SaveFilePath;
			Printpath = Printpath.Replace(".sda", ".xls");
			mForm.Invoke((MethodInvoker)delegate
			{
				if (mForm.CurrentGCID == ID)
				{
					mForm.chrAcqCtrl.tsStart.Enabled = true;
					mForm.chrAcqCtrl.tsstop.Enabled = false;
				}
			});
			if (OneChannelData.StopAutoAlalyse)
			{
				mForm.Invoke((MethodInvoker)delegate
				{
					try
					{
						if (ChromForm.form == null)
						{
							ChromForm.form = new ChromForm();
						}
						ChromForm.form.Show();
						ChromForm.form.OpenChrom(SaveFilePath, sampling: false, useCurrent: true);
					}
					catch (Exception ex2)
					{
						LogMgr.Instance.LogError($"ChromForm.form.OpenChrom{SaveFilePath}");
						LogMgr.Instance.LogError($"ChromForm.form.OpenChrom{ex2.Message}");
						LogMgr.Instance.LogError($"ChromForm.form.OpenChrom{ex2.StackTrace}");
					}
				});
			}
			for (int num2 = 0; num2 < chromatogram.RltPeaks.Length; num2++)
			{
				if (chromatogram.caliGnl == null || num2 != chromatogram.caliGnl.UpDataPeakIndex)
				{
					continue;
				}
				float areaPer = chromatogram.RltPeaks[num2].areaPer;
				float num3 = 0.16f * (areaPer * 100f) + 4f;
				if (num3 < 4f)
				{
					num3 = 4f;
				}
				if (num3 > 20f)
				{
					num3 = 20f;
				}
				string s = "#010+" + num3.ToString("00.000") + ".";
				byte[] mybyte0 = Encoding.Default.GetBytes(s);
				mybyte0[mybyte0.Length - 1] = 13;
				mForm.Invoke((MethodInvoker)delegate
				{
					if (mForm.ModbusComClient != null)
					{
						mForm.ModbusComClient.SendData(mybyte0);
					}
				});
				areaPer = chromatogram.RltPeaks[num2].amount;
				num3 = 16f / (sysParam.fDcsMaxValue - sysParam.fDcsMinValue) * (areaPer - sysParam.fDcsMinValue) + 4f;
				if (num3 < 4f)
				{
					num3 = 4f;
				}
				if (num3 > 20f)
				{
					num3 = 20f;
				}
				s = "#011+" + num3.ToString("00.000") + ".";
				byte[] mybyte1 = Encoding.Default.GetBytes(s);
				mybyte1[mybyte1.Length - 1] = 13;
				mForm.Invoke((MethodInvoker)delegate
				{
					if (mForm.ModbusComClient != null)
					{
						mForm.ModbusComClient.SendData(mybyte1);
					}
				});
			}
			ModBusbgChrom[ChannelIndex] = chromatogram;
			Class49.bUpdateDataCtrl = true;
			Class49.strSaveFilePath = SaveFilePath;
			Class49.ChannelIndex = ChannelIndex;
			mForm.Invoke((MethodInvoker)delegate
			{
				if (frmParam.bTwoDector)
				{
					int count = cdlMgr.formMain.tabChannel.TabPages.Count;
					if (ChannelIndex == 0)
					{
						channel1Ready = true;
						channel1File = SaveFilePath;
					}
					if (ChannelIndex == 1)
					{
						channel2Ready = true;
						channel2File = SaveFilePath;
					}
					if (ChannelIndex == 2)
					{
						channel3Ready = true;
						channel3File = SaveFilePath;
					}
					switch (count)
					{
					case 3:
						if (channel1Ready && channel2Ready && channel3Ready)
						{
							channel1Ready = false;
							channel2Ready = false;
							channel3Ready = false;
							string text = channel1File.Substring(0, channel1File.LastIndexOf("."));
							text += "合并.sda";
							IBrainCommon.spectraCombined(channel1File, channel2File, channel3File, text);
							try
							{
								if (ChromForm.form == null)
								{
									ChromForm.form = new ChromForm();
								}
								ChromForm.form.Show();
								ChromForm.form.OpenChrom(text, sampling: true, useCurrent: true);
								ChromForm.form.chromDataGrid.mstSetChromForm.bUseSet_Click(null, null);
								ChromForm.form.chromDataGrid.saveFile();
								break;
							}
							catch (Exception)
							{
								break;
							}
						}
						break;
					case 2:
						if (channel1Ready && channel2Ready)
						{
							channel1Ready = false;
							channel2Ready = false;
							string name = channel1File.Substring(0, channel1File.LastIndexOf("."));
							name += "合并.sda";
							IBrainCommon.spectraCombined(channel1File, channel2File, name);
							try
							{
								mForm.Invoke((MethodInvoker)delegate
								{
									if (ChromForm.form == null)
									{
										ChromForm.form = new ChromForm();
									}
									ChromForm.form.Show();
									ChromForm.form.OpenChrom(name, sampling: true, useCurrent: true);
									ChromForm.form.chromDataGrid.mstSetChromForm.bUseSet_Click(null, null);
									ChromForm.form.chromDataGrid.saveFile();
								});
								break;
							}
							catch (Exception)
							{
								break;
							}
						}
						break;
					}
				}
			});
			Class49.InsertIntoTable(Class49.string_9[2], Class49.user_0.u_name, string_0 + string_1, "存储新谱图", "通道:" + ChannelIndex + " 分析结束生成新谱图:" + SaveFilePath);
		}
		catch (Exception ex)
		{
			LogMgr.Instance.LogError(ex.Message);
		}
	}

	public void RefreshForm()
	{
		Update_pA_mV();
	}

	private void SetVocFireState(int chn)
	{
		Signal signal = sglsSampling[0];
		Signal signal2 = sglsSampling[1];
		int num = signal.DotsNum - 1;
		int num2 = signal2.DotsNum - 1;
		if (signal.dots.Length <= num)
		{
			LogMgr.Instance.Write2RunLog("TcpServerSocket.SetVocFireState Error: sglsSampling[chn].dots.Length:" + sglsSampling[0].dots.Length + " ndotSignalChn:" + num);
			return;
		}
		if (signal2.dots.Length <= num2)
		{
			LogMgr.Instance.Write2RunLog("TcpServerSocket.SetVocFireState Error: sglsSampling[chn].dots.Length:" + sglsSampling[1].dots.Length + " ndotSignalChn:" + num2);
			return;
		}
		if (num > 0)
		{
			PointF pointF = signal.dots[num];
			mForm.chrAcqCtrl.maskedTextBox6.Text = pointF.Y.ToString("0.000");
			if (FormOnline.selfCtrl != null)
			{
				FormOnline.selfCtrl.labSignal.Text = pointF.Y.ToString("0.000") + "pA";
			}
			if (pointF.Y >= fFireOn)
			{
				mForm.button37.Enabled = true;
				Bfire1 = true;
				if (Bfire2 && mForm.StateYiqi != 5 && mForm.StateYiqi != 2)
				{
					mForm.StateYiqi = 3;
				}
				if (FormOnline.selfCtrl != null)
				{
					FormOnline.selfCtrl.picBoxFire.Image = FormOnline.selfCtrl.imageList1.Images[2];
				}
				if (FormMainPortable.fromMain != null)
				{
					FormMainPortable.fromMain.picBoxFire.Image = FormMainPortable.fromMain.imageList2.Images[2];
				}
			}
			else
			{
				mForm.button37.Enabled = false;
				float[] array = new float[1];
				ushort[] array2 = new ushort[2];
				array = new float[1];
				System.Buffer.BlockCopy(array, 0, array2, 0, 4);
				mForm.mComModbus.WordVaue[0] = array2[0];
				mForm.mComModbus.WordVaue[1] = array2[1];
				Bfire1 = false;
				if (cdlMgr.formMain.StateYiqi == 3 || cdlMgr.formMain.StateYiqi == 5)
				{
					cdlMgr.formMain.StateYiqi = 1;
				}
				if (FormOnline.selfCtrl != null)
				{
					FormOnline.selfCtrl.picBoxFire.Image = FormOnline.selfCtrl.imageList1.Images[3];
				}
				if (FormMainPortable.fromMain != null)
				{
					FormMainPortable.fromMain.picBoxFire.Image = FormMainPortable.fromMain.imageList2.Images[3];
				}
			}
			SetFromLabFireState();
		}
		if (num2 > 0)
		{
			PointF pointF2 = signal2.dots[num2];
			mForm.chrAcqCtrl.maskedTextBox12.Text = pointF2.Y.ToString("0.000");
			if (pointF2.Y >= fFireOn2)
			{
				mForm.button38.Enabled = true;
				Bfire2 = true;
				return;
			}
			Bfire2 = false;
			if (cdlMgr.formMain.StateYiqi == 3 || cdlMgr.formMain.StateYiqi == 5)
			{
				cdlMgr.formMain.StateYiqi = 1;
			}
		}
		else
		{
			mForm.button38.Enabled = false;
		}
	}

	private void SetFormTextBoxSampleValue()
	{
		mForm.chrAcqCtrl.maskedTextBox6.Text = "";
		mForm.chrAcqCtrl.maskedTextBox7.Text = "";
		mForm.chrDeviceCtrl.ClearUvInfo();
	}

	private void SetFromLabFireState()
	{
		switch (mForm.StateYiqi)
		{
		case 0:
			mForm.labFireState.Text = Lang.PS("开机准备", "Boot to prepare");
			if (mForm.tbMachineState != null)
			{
				mForm.tbMachineState.Text = Lang.PS("开机准备", "Boot to prepare");
			}
			break;
		case 1:
			mForm.labFireState.Text = Lang.PS("异常熄火", "Abnormal flameout");
			if (mForm.tbMachineState != null)
			{
				mForm.tbMachineState.Text = Lang.PS("异常熄火", "Abnormal flameout");
			}
			break;
		case 2:
			mForm.labFireState.Text = Lang.PS("温度异常", "Temperature anomaly");
			if (mForm.tbMachineState != null)
			{
				mForm.tbMachineState.Text = Lang.PS("温度异常", "Temperature anomaly");
			}
			break;
		case 3:
			mForm.labFireState.Text = Lang.PS("仪器稳定", "Instrument stability");
			if (mForm.tbMachineState != null)
			{
				mForm.tbMachineState.Text = Lang.PS("仪器稳定", "Instrument stability");
			}
			break;
		case 4:
			mForm.labFireState.Text = Lang.PS("等待点火", "Instrument stability");
			break;
		case 5:
			mForm.labFireState.Text = Lang.PS("正常分析", "Normal analysis");
			if (mForm.tbMachineState != null)
			{
				mForm.tbMachineState.Text = Lang.PS("正常分析", "Normal analysis");
			}
			break;
		case 6:
			mForm.labFireState.Text = Lang.PS("点火失败", "Failure to  ignition");
			if (mForm.tbMachineState != null)
			{
				mForm.tbMachineState.Text = Lang.PS("点火失败", "Failure to  ignition");
			}
			break;
		default:
			mForm.labFireState.Text = "";
			if (mForm.tbMachineState != null)
			{
				mForm.tbMachineState.Text = "";
			}
			break;
		}
	}

	private void Update_pA_mV()
	{
		RefrushTempratureTable(AccStyle.Read, devManager1);
		RefrushTempratureControl(AccStyle.Read, devManager1);
		RefrushEventTable0(AccStyle.Read, devManager1);
		int currentChannelIndex = mForm.CurrentChannelIndex;
		mForm.insDeviceCtrl.ReadHardVersion(devManager1);
		if (currentChannelIndex >= 0)
		{
			method_9(AccStyle.Read, devManager1, currentChannelIndex);
		}
		if (currentChannelIndex >= dtc_Channels.Length)
		{
			LogMgr.Instance.Write2RunLog("TcpServerSocket.Update_pA_mV Wrong: num >= this.dtc_Channels.Length. num:" + currentChannelIndex + " dtc_Channels.Length:" + dtc_Channels.Length);
			return;
		}
		if (dtc_Channels[currentChannelIndex].unitStr == "pA")
		{
			Class49.bool_4 = true;
			mForm.chrAcqCtrl.SetDisplayUnit(0);
			if (mForm.sampleDisplay != null)
			{
				mForm.sampleDisplay.unitY = "pA";
			}
			if (mForm.sampleDisplay != null)
			{
				mForm.sampleDisplay.txtY = "电流";
			}
		}
		else
		{
			Class49.bool_4 = false;
			mForm.chrAcqCtrl.SetDisplayUnit(1);
			if (mForm.sampleDisplay != null)
			{
				mForm.sampleDisplay.unitY = "mV";
			}
			if (mForm.sampleDisplay != null)
			{
				mForm.sampleDisplay.txtY = "电压";
			}
		}
		mForm.insDeviceCtrl.ReadIpAddress(devManager1);
	}

	private void Update_CloseButton()
	{
		if (mForm.CurrentGCID == ID)
		{
			mForm.insDeviceCtrl.UpdateControlAnalyzeText(bCtrl: true);
			mForm.chrAcqCtrl.tbTime_DoubleClick(null, null);
			if (minePlot.minePlotSelf != null)
			{
				minePlot.minePlotSelf.UpdateControlAnalyzeText(bCtrl: true);
			}
		}
	}

	private void Update_Peak()
	{
		if (mForm.CurrentGCID == ID)
		{
			mForm.chrAcqCtrl.tsStart.Enabled = true;
			mForm.chrAcqCtrl.tsstop.Enabled = false;
		}
		string text = "Projects\\" + string_0 + "\\" + DateTime.Now.Date.ToString("yyyy-MM-dd") + "\\" + string_0 + "_" + DateTime.Now.ToString("yyyy-MM-dd_HH_mm_ss") + "_" + devManager0.sglNumberEnd + ".sda";
		if (bgChrom.userArchives.Length == 0)
		{
			Array.Resize(ref bgChrom.userArchives, 1);
			bgChrom.userArchives[0] = new UserArchive();
		}
		string channelName = "";
		int num = 0;
		if (devManager0.sglNumberEnd > 3)
		{
			for (int i = 0; i < sglsSampling.Length; i++)
			{
				if (dtc_Channels[i].mark == devManager0.sglNumberEnd)
				{
					bgChrom.signal = sglsSampling[i];
					channelName = dtc_Channels[i].name;
					num = i;
					break;
				}
			}
		}
		else
		{
			bgChrom.signal = sglsSampling[devManager0.sglNumberEnd];
			channelName = dtc_Channels[devManager0.sglNumberEnd].name;
			num = devManager0.sglNumberEnd;
		}
		string name = cdlMgr.GetChromDevice(ID).info.Name;
		int modBusDeviceID = cdlMgr.GetChromDevice(ID).info.ModBusDeviceID;
		ChartParaOpera chartParaOpera = cdlMgr.GetChartParaOpera(ID, num);
		string filename = CMS_InfoParasFMT.FmtStr(2, bgChrom.signal.runningInjInfo, modBusDeviceID, name, "");
		if (bgChrom.signal.simple)
		{
			text = GetFileLongName(name, num, channelName, filename, Class49.Object2Int(devManager1.injectNeedleNum, 0), Class49.Object2Int(devManager1.injectBotNum, 0), bgChrom.signal.SaveIndex++);
			if (OnlineCtrl.selfCtrl != null)
			{
				try
				{
					text = text + "流路" + OnlineCtrl.selfCtrl.currentFlowPath + "_";
				}
				catch (Exception)
				{
					text += "流路x_";
				}
			}
			text += GetFileName(text, chartParaOpera, name, num, channelName, filename, Class49.Object2Int(devManager1.injectNeedleNum, 0), Class49.Object2Int(devManager1.injectBotNum, 0), bgChrom.signal.SaveIndex++);
			SaveChromFile(num);
		}
		bgChrom.signal.ResetOriDots(createDiskFile: true);
		bgChrom.signal.ClearPeak();
	}

	private void Update_FTNS_Name()
	{
		if (mForm.CurrentGCID == string_0)
		{
			int clmCT6CNIndex = mForm.FTNS.clmCT6CNIndex;
			int clmCT6ENIndex = mForm.FTNS.clmCT6ENIndex;
			mForm.FTNS.dgvCT6.Rows[0].Cells[clmCT6CNIndex].Value = devManager1.tempCtrlAreaTable.tempList[0].strNameCn;
			mForm.FTNS.dgvCT6.Rows[0].Cells[clmCT6ENIndex].Value = devManager1.tempCtrlAreaTable.tempList[0].strNameEn;
			mForm.FTNS.dgvCT6.Rows[1].Cells[clmCT6CNIndex].Value = devManager1.tempCtrlAreaTable.tempList[1].strNameCn;
			mForm.FTNS.dgvCT6.Rows[1].Cells[clmCT6ENIndex].Value = devManager1.tempCtrlAreaTable.tempList[1].strNameEn;
			mForm.FTNS.dgvCT6.Rows[2].Cells[clmCT6CNIndex].Value = devManager1.tempCtrlAreaTable.tempList[2].strNameCn;
			mForm.FTNS.dgvCT6.Rows[2].Cells[clmCT6ENIndex].Value = devManager1.tempCtrlAreaTable.tempList[2].strNameEn;
			mForm.FTNS.dgvCT6.Rows[4].Cells[clmCT6CNIndex].Value = devManager1.tempCtrlAreaTable.tempList[3].strNameCn;
			mForm.FTNS.dgvCT6.Rows[4].Cells[clmCT6ENIndex].Value = devManager1.tempCtrlAreaTable.tempList[3].strNameEn;
			mForm.FTNS.dgvCT6.Rows[3].Cells[clmCT6CNIndex].Value = devManager1.tempCtrlAreaTable.tempList[4].strNameCn;
			mForm.FTNS.dgvCT6.Rows[3].Cells[clmCT6ENIndex].Value = devManager1.tempCtrlAreaTable.tempList[4].strNameEn;
			mForm.FTNS.dgvCT6.Rows[5].Cells[clmCT6CNIndex].Value = devManager1.tempCtrlAreaTable.tempList[5].strNameCn;
			mForm.FTNS.dgvCT6.Rows[5].Cells[clmCT6ENIndex].Value = devManager1.tempCtrlAreaTable.tempList[5].strNameEn;
			mForm.FTNS.dgvCT6.Rows[6].Cells[clmCT6CNIndex].Value = devManager1.tempCtrlAreaTable.tempList[6].strNameCn;
			mForm.FTNS.dgvCT6.Rows[6].Cells[clmCT6ENIndex].Value = devManager1.tempCtrlAreaTable.tempList[6].strNameEn;
			mForm.FTNS.dgvCT6.Rows[7].Cells[clmCT6CNIndex].Value = devManager1.tempCtrlAreaTable.tempList[7].strNameCn;
			mForm.FTNS.dgvCT6.Rows[7].Cells[clmCT6ENIndex].Value = devManager1.tempCtrlAreaTable.tempList[7].strNameEn;
			mForm.insDeviceCtrl.ReadTempratureTableName(devManager1);
		}
	}

	private void Update_FTNS_Value()
	{
		if (mForm.CurrentGCID == string_0)
		{
			int clmCT6CtrlTIndex = mForm.FTNS.clmCT6CtrlTIndex;
			mForm.FTNS.dgvCT6.Rows[7].Cells[clmCT6CtrlTIndex].Value = devManager1.insDevEnable7;
			mForm.FTNS.dgvCT6.Rows[6].Cells[clmCT6CtrlTIndex].Value = devManager1.insDevEnable6;
			mForm.FTNS.dgvCT6.Rows[5].Cells[clmCT6CtrlTIndex].Value = devManager1.insDevEnable5;
			mForm.FTNS.dgvCT6.Rows[3].Cells[clmCT6CtrlTIndex].Value = devManager1.insDevEnable4;
			mForm.FTNS.dgvCT6.Rows[4].Cells[clmCT6CtrlTIndex].Value = devManager1.insDevEnable3;
			mForm.FTNS.dgvCT6.Rows[2].Cells[clmCT6CtrlTIndex].Value = devManager1.insDevEnable2;
			mForm.FTNS.dgvCT6.Rows[1].Cells[clmCT6CtrlTIndex].Value = devManager1.insDevEnable1;
			mForm.FTNS.dgvCT6.Rows[0].Cells[clmCT6CtrlTIndex].Value = devManager1.insDevEnable0;
			mForm.insDeviceCtrl.ReadTempratureTableColor(devManager1);
		}
	}

	private void Update_ChannelTableText()
	{
		mForm.insDeviceCtrl.ReadInjectNumList(devManager1);
	}

	private void Update_From_IpAddress()
	{
		mForm.insDeviceCtrl.WriteIpAddress(devManager0);
	}

	private void Update_ControlTempButton()
	{
		if (!(mForm.CurrentGCID != ID))
		{
			BControlTemp = class44_0.bool_1;
			mForm.insDeviceCtrl.UpdateControlTempText(class44_0.bool_1);
			mForm.chrAcqCtrl.UpdateControlTempText(class44_0.bool_1);
			mForm.UpdateControlTempText(class44_0.bool_1);
			if (LYTHCtrl2.selfCtrl != null)
			{
				LYTHCtrl2.selfCtrl.UpdateControlTempText(class44_0.bool_1);
			}
			if (minePlot.minePlotSelf != null)
			{
				minePlot.minePlotSelf.UpdateControlTempText(class44_0.bool_1);
			}
		}
	}
}
