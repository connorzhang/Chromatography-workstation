using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Web.Script.Serialization;
using System.Windows.Forms;
using System.Xml.Serialization;
using IBrainChrom2018.ChromFile;
using IBrainChrom2018.Unit;

namespace IBrainChrom2018;

public class ChromDeviceListMgr
{
	private static ChromDeviceListMgr self = null;

	private ChromDeviceList chromDevList;

	public AsyncTcpServerMgr tcpServerMgr;

	public ChromFormInterface formMain;

	public FrmChromatManagerInterface formMainEx;

	private string strPathWorkSunAquip = Application.StartupPath + "\\saq.cfg";

	private string strPathDefaultSunAquip = Application.StartupPath + "\\default.cfg";

	private string strCurrentGCID = "";

	private int m_iCurrentChannel = 0;

	private FormMainParam frmParam = FormMainParam.Create();

	[XmlIgnore]
	[ScriptIgnore]
	public ChromDevice CurrentChromDevice
	{
		get
		{
			return GetChromDevice(CurrentGCID);
		}
		set
		{
			SetChromDevice(value);
		}
	}

	[XmlIgnore]
	[ScriptIgnore]
	public InsDeviceManager CurrentInsDeviceMgr
	{
		get
		{
			if (CurrentChromDevice == null)
			{
				InsDeviceManager insDeviceManager = new InsDeviceManager();
				insDeviceManager.epcDevReset();
				return insDeviceManager;
			}
			return CurrentChromDevice.misMgr.devManager;
		}
		set
		{
			if (CurrentChromDevice != null && value != null)
			{
				CurrentChromDevice.misMgr.devManager = value;
			}
		}
	}

	public string CurrentGCID
	{
		get
		{
			return strCurrentGCID;
		}
		set
		{
			strCurrentGCID = value;
			m_iCurrentChannel = 0;
		}
	}

	public int CurrentChannelIdx
	{
		get
		{
			return m_iCurrentChannel;
		}
		set
		{
			m_iCurrentChannel = value;
		}
	}

	public int ChannelCount
	{
		get
		{
			if (CurrentChromDevice == null)
			{
				return 0;
			}
			return CurrentChromDevice.misMgr.ChannelChartParaS.Count;
		}
	}

	public ChannelChartPara CurrentChannelChartPara
	{
		get
		{
			return GetChannelChartPara(strCurrentGCID, m_iCurrentChannel);
		}
		set
		{
			ChromDevice chromDevice = GetChromDevice(strCurrentGCID);
			if (chromDevice != null)
			{
				chromDevice.misMgr.ChannelChartParaS[m_iCurrentChannel] = value;
			}
		}
	}

	public List<ChannelChartPara> ChannelChartParaList => GetChromDevice(strCurrentGCID)?.misMgr.ChannelChartParaS;

	public ChartParaOpera CurrentChartParaOpera
	{
		get
		{
			return GetChartParaOpera(strCurrentGCID, m_iCurrentChannel);
		}
		set
		{
			ChromDevice chromDevice = GetChromDevice(strCurrentGCID);
			if (chromDevice != null)
			{
				chromDevice.misMgr.ChartParaOperaS[m_iCurrentChannel] = value;
			}
		}
	}

	public List<ChartParaOpera> ChartParaOperaList => GetChromDevice(strCurrentGCID)?.misMgr.ChartParaOperaS;

	public ChromDevice this[int index]
	{
		get
		{
			if (chromDevList == null)
			{
				return null;
			}
			return chromDevList[index];
		}
		set
		{
			if (chromDevList != null)
			{
				chromDevList[index] = value;
			}
		}
	}

	public int Count
	{
		get
		{
			if (chromDevList == null)
			{
				return 0;
			}
			return chromDevList.Count;
		}
	}

	public TcpServerSocket CurrentTcpServerSocket => tcpServerMgr.GetTcpServerSocket(CurrentGCID);

	public AsyncTcpServer MainTcpServer
	{
		get
		{
			return tcpServerMgr.mainTcpServer;
		}
		set
		{
			tcpServerMgr.mainTcpServer = value;
		}
	}

	public AsyncTcpServer Modus0TcpServer
	{
		get
		{
			return tcpServerMgr.modus0TcpServer;
		}
		set
		{
			tcpServerMgr.modus0TcpServer = value;
		}
	}

	public AsyncTcpServer Modus1TcpServer
	{
		get
		{
			return tcpServerMgr.modus1TcpServer;
		}
		set
		{
			tcpServerMgr.modus1TcpServer = value;
		}
	}

	public static ChromDeviceListMgr Create()
	{
		if (self == null)
		{
			self = new ChromDeviceListMgr();
		}
		return self;
	}

	private ChromDeviceListMgr()
	{
		chromDevList = new ChromDeviceList();
		tcpServerMgr = AsyncTcpServerMgr.Create();
		LoadWorkSunFile();
	}

	public ChannelChartPara GetOneEquipPara(int idxChannel)
	{
		return GetChromDevice(strCurrentGCID)?.misMgr.GetChannelChartPara(idxChannel);
	}

	public ChartParaOpera GetChartParaOpera(int idxChannel)
	{
		return GetChartParaOpera(strCurrentGCID, idxChannel);
	}

	public void Add(ChromDevice device)
	{
		chromDevList.ChromDevList.Add(device);
	}

	public void Add(string strGCID)
	{
		ChromDevice chromDevice = GetChromDevice(strGCID);
		if (chromDevice == null)
		{
			ChromDeviceInfo myinfo = new ChromDeviceInfo(strGCID, strGCID, "", "", 0);
			chromDevice = new ChromDevice(myinfo, 4);
			Add(chromDevice);
			this[Count - 1].misMgr.ChannelChartParaS = IArrayBase.NewArray<ChannelChartPara>(4);
			for (int i = 0; i < this[Count - 1].misMgr.ChannelChartParaS.Count; i++)
			{
				this[Count - 1].misMgr.ChannelChartParaS[i].fullScreenTime = 20f;
				this[Count - 1].misMgr.ChannelChartParaS[i].stopTime = 30f;
				this[Count - 1].misMgr.ChannelChartParaS[i].showHighLimit = 10f;
				this[Count - 1].misMgr.ChannelChartParaS[i].showLowLimit = -1f;
				this[Count - 1].misMgr.ChannelChartParaS[i].analysisWhenStop = false;
			}
			this[Count - 1].misMgr.ChartParaOperaS = IArrayBase.NewArray<ChartParaOpera>(4);
			for (int j = 0; j < this[Count - 1].misMgr.ChartParaOperaS.Count; j++)
			{
				this[Count - 1].misMgr.ChartParaOperaS[j] = new ChartParaOpera();
			}
		}
	}

	public void Remove(ChromDevice device)
	{
		chromDevList.ChromDevList.Remove(device);
	}

	public void Clear()
	{
		chromDevList.ChromDevList.Clear();
	}

	public void Remove(string strGCID)
	{
		ChromDevice chromDevice = GetChromDevice(strGCID);
		if (chromDevice != null)
		{
			Remove(chromDevice);
		}
	}

	public void Resize(int count)
	{
		if (count < 0)
		{
			return;
		}
		if (count > chromDevList.ChromDevList.Count)
		{
			while (count > chromDevList.ChromDevList.Count)
			{
				chromDevList.ChromDevList.Add(new ChromDevice());
			}
		}
		if (count < chromDevList.ChromDevList.Count)
		{
			while (count < chromDevList.ChromDevList.Count)
			{
				chromDevList.ChromDevList.RemoveAt(chromDevList.ChromDevList.Count - 1);
			}
		}
	}

	public ChromDevice GetChromDevice(string strGCID)
	{
		if (chromDevList == null || chromDevList.ChromDevList == null)
		{
			return null;
		}
		for (int i = 0; i < chromDevList.ChromDevList.Count; i++)
		{
			if (chromDevList.ChromDevList[i].info.ID == strGCID)
			{
				return chromDevList.ChromDevList[i];
			}
		}
		return null;
	}

	public void SetChromDevice(ChromDevice chromDev)
	{
		bool flag = false;
		string iD = chromDev.info.ID;
		if (chromDevList == null)
		{
			chromDevList = new ChromDeviceList();
		}
		if (chromDevList.ChromDevList == null)
		{
			chromDevList.ChromDevList = IArrayBase.NewArray<ChromDevice>(0);
		}
		for (int i = 0; i < chromDevList.ChromDevList.Count; i++)
		{
			if (chromDevList.ChromDevList[i].info.ID == iD)
			{
				chromDevList.ChromDevList[i] = chromDev;
				flag = true;
				break;
			}
		}
		if (!flag)
		{
			chromDevList.ChromDevList.Add(chromDev);
		}
	}

	public ChannelChartPara GetChannelChartPara(string strGCID, int iChannel)
	{
		return GetChromDevice(strGCID)?.misMgr.GetChannelChartPara(iChannel);
	}

	public ChartParaOpera GetChartParaOpera(string strGCID, int iChannel)
	{
		return GetChromDevice(strGCID)?.misMgr.GetChartParaOpera(iChannel);
	}

	public void NewTcpServerMgr(ChromFormInterface form)
	{
		if (tcpServerMgr.mainTcpServer == null)
		{
			tcpServerMgr.mainTcpServer = new AsyncTcpServer(new IPEndPoint(IPAddress.Any, AsyncTcpServerMgr.iMainPort), form);
			tcpServerMgr.mainTcpServer.ServerType = AsyncTcpServer.Stype.GCSever;
		}
		if (tcpServerMgr.modus0TcpServer == null)
		{
			tcpServerMgr.modus0TcpServer = new AsyncTcpServer(new IPEndPoint(IPAddress.Any, AsyncTcpServerMgr.modus0Port), form);
			tcpServerMgr.modus0TcpServer.ServerType = AsyncTcpServer.Stype.ModBusServer;
		}
		if (tcpServerMgr.modus1TcpServer == null)
		{
			tcpServerMgr.modus1TcpServer = new AsyncTcpServer(new IPEndPoint(IPAddress.Any, AsyncTcpServerMgr.modus1Port), form);
			tcpServerMgr.modus1TcpServer.ServerType = AsyncTcpServer.Stype.ModBusServer;
		}
	}

	public void StopTcpServerMgr()
	{
		LogMgr.Instance.Write2RunLog($"ChromDeviceListMgr.StopTcpServerMgr");
		tcpServerMgr.mainTcpServer.Stop();
		tcpServerMgr.modus0TcpServer.Stop();
		tcpServerMgr.modus1TcpServer.Stop();
		TcpServerSocket.m_bStopFlag = true;
		if (tcpServerMgr.ModbusComClient != null)
		{
			tcpServerMgr.ModbusComClient.Close();
		}
	}

	public void currentTcpServerMgrSendCmd(byte byte_1)
	{
		CurrentTcpServerSocket?.SendCmd(byte_1);
	}

	public void currentTcpServerMgrSendEPCCmd(byte byte_1, byte Type)
	{
		CurrentTcpServerSocket?.SendEPCCmd(byte_1, Type);
	}

	public static bool InsertIntoMine(int ComponentID, int dexcotID, string[] zufenName, string[] zufenAmount, string strJiedian)
	{
		DBBase dBBase = DBBase.Create();
		SystemParam systemParam = SystemParam.Create();
		if (systemParam.iDbConnectType == 0 || systemParam.iDbConnectType == 2)
		{
			return false;
		}
		return dBBase.InsertIntoMineSqlLite(ComponentID, dexcotID, zufenName, zufenAmount, strJiedian);
	}

	public bool LoadWorkSunFile()
	{
		if (!File.Exists(strPathWorkSunAquip))
		{
			LoadDefaultSunFile();
			return false;
		}
		try
		{
			LoadFromFileOld(strPathWorkSunAquip);
		}
		catch (Exception ex)
		{
			LogMgr.Instance.LogError("logerr 1  LoadWorkSunFile" + ex.Message);
		}
		return true;
	}

	public void SaveWorkSunFile()
	{
		SaveToFileB(strPathWorkSunAquip);
	}

	private void LoadDefaultSunFile()
	{
		if (File.Exists(strPathDefaultSunAquip))
		{
			ChromDeviceList chromDeviceList = (ChromDeviceList)IBaseFileMgr.OpenFile(strPathDefaultSunAquip);
			if (chromDeviceList != null)
			{
				chromDevList = chromDeviceList;
			}
		}
	}

	public bool LoadFromFileOld(string fileName)
	{
		bool result = false;
		if (!File.Exists(fileName))
		{
			return result;
		}
		fileName = fileName.ToLower();
		FileInfo fileInfo_ = null;
		FileStream fileStream_ = null;
		BinaryReader binaryReader_ = null;
		Class49.OpenBinaryReader(fileName, out fileInfo_, out fileStream_, out binaryReader_);
		try
		{
			string text = binaryReader_.ReadString();
			if (text != "IBrainChrom")
			{
				return false;
			}
			string text2 = binaryReader_.ReadString();
			int num = binaryReader_.ReadInt32();
			int num2 = num;
			if (num > 255)
			{
				num = 255;
			}
			Resize(num);
			for (int i = 0; i < Count; i++)
			{
				try
				{
					this[i].misMgr.ChannelChartParaS = IArrayBase.NewArray<ChannelChartPara>(4);
					this[i].misMgr.ChartParaOperaS = IArrayBase.NewArray<ChartParaOpera>(4);
					this[i].info.ID = binaryReader_.ReadString();
					if (this[i].info.ID == "")
					{
					}
					this[i].info.ModBusDeviceID = binaryReader_.ReadInt32();
					this[i].info.DepartMent = binaryReader_.ReadString();
					this[i].info.Name = binaryReader_.ReadString();
					this[i].info.Other = binaryReader_.ReadString();
					this[i].misMgr.devManager.Msg.AutoSendByStopTime = binaryReader_.ReadBoolean();
					this[i].misMgr.devManager.Msg.Mess = binaryReader_.ReadString();
					this[i].misMgr.devManager.Msg.sound = binaryReader_.ReadBoolean();
					this[i].misMgr.devManager.Msg.soundTimes = binaryReader_.ReadInt32();
					for (int j = 0; j < this[i].misMgr.ChannelChartParaS.Count; j++)
					{
						this[i].misMgr.ChannelChartParaS[j].cnlBasisQuantity = (EnumChannelBasisQuantity)binaryReader_.ReadInt32();
						this[i].misMgr.ChannelChartParaS[j].cnlDetectMethod = (EnumChannelDetectionMethod)binaryReader_.ReadInt32();
						this[i].misMgr.ChannelChartParaS[j].fullScreenTime = binaryReader_.ReadSingle();
						this[i].misMgr.ChannelChartParaS[j].bClearZero = binaryReader_.ReadBoolean();
						this[i].misMgr.ChannelChartParaS[j].printWhenStop = binaryReader_.ReadBoolean();
						this[i].misMgr.ChannelChartParaS[j].analysisWhenStop = binaryReader_.ReadBoolean();
						this[i].misMgr.ChannelChartParaS[j].bFullScreen = binaryReader_.ReadBoolean();
						this[i].misMgr.ChannelChartParaS[j].stopTime = binaryReader_.ReadSingle();
						this[i].misMgr.ChannelChartParaS[j].showHighLimit = binaryReader_.ReadSingle();
						this[i].misMgr.ChannelChartParaS[j].showLowLimit = binaryReader_.ReadSingle();
						this[i].misMgr.ChannelChartParaS[j].bBaselineDeduction = binaryReader_.ReadBoolean();
					}
					for (int k = 0; k < this[i].misMgr.ChartParaOperaS.Count; k++)
					{
						this[i].misMgr.ChartParaOperaS[k].mtdMgr.printPara.LoadFromBr(binaryReader_);
						this[i].misMgr.ChartParaOperaS[k].TemplatePath = binaryReader_.ReadString();
						EnumChannelDetectionMethod enumChannelDetectionMethod = (EnumChannelDetectionMethod)binaryReader_.ReadInt32();
						EnumChannelBasisQuantity enumChannelBasisQuantity = (EnumChannelBasisQuantity)binaryReader_.ReadInt32();
						this[i].misMgr.ChartParaOperaS[k].FileNameAquipName = binaryReader_.ReadBoolean();
						this[i].misMgr.ChartParaOperaS[k].FileNameAutoInject = binaryReader_.ReadBoolean();
						this[i].misMgr.ChartParaOperaS[k].FileNameChannelName = binaryReader_.ReadBoolean();
						this[i].misMgr.ChartParaOperaS[k].FileNameDateTime = binaryReader_.ReadBoolean();
						this[i].misMgr.ChartParaOperaS[k].InjectIndex = binaryReader_.ReadBoolean();
						this[i].misMgr.ChartParaOperaS[k].FileUserSet = binaryReader_.ReadString();
						this[i].misMgr.ChartParaOperaS[k].UseUserZeroTime = binaryReader_.ReadBoolean();
						this[i].misMgr.ChartParaOperaS[k].ZeroTime = binaryReader_.ReadDouble();
						this[i].misMgr.ChartParaOperaS[k].ZeroTimeLeft = binaryReader_.ReadDouble();
						this[i].misMgr.ChartParaOperaS[k].ZeroTimeRight = binaryReader_.ReadDouble();
						IArrayBase.NewArray(ref this[i].misMgr.ChartParaOperaS[k].componentList, binaryReader_.ReadInt32());
						for (int l = 0; l < this[i].misMgr.ChartParaOperaS[k].componentList.Count; l++)
						{
							this[i].misMgr.ChartParaOperaS[k].componentList[l].JuseTimeCheck = binaryReader_.ReadBoolean();
							this[i].misMgr.ChartParaOperaS[k].componentList[l].JStdandPeakTime = binaryReader_.ReadDouble();
							this[i].misMgr.ChartParaOperaS[k].componentList[l].JTimePara = binaryReader_.ReadDouble();
							this[i].misMgr.ChartParaOperaS[k].componentList[l].name = binaryReader_.ReadString();
							this[i].misMgr.ChartParaOperaS[k].componentList[l].JPeakAdjustPara = binaryReader_.ReadDouble();
							this[i].misMgr.ChartParaOperaS[k].componentList[l].JModBusAddr = binaryReader_.ReadInt32();
						}
						if (this[i].misMgr.ChartParaOperaS[k].Integ == null)
						{
							this[i].misMgr.ChartParaOperaS[k].Integ = new Integration();
						}
						this[i].misMgr.ChartParaOperaS[k].Integ.Reset();
						this[i].misMgr.ChartParaOperaS[k].Integ.LoadFromFile(binaryReader_);
						if (this[i].misMgr.ChartParaOperaS[k].mtdMgr == null)
						{
							this[i].misMgr.ChartParaOperaS[k].mtdMgr = new MtdSetup();
						}
						string svaddr = "";
						this[i].misMgr.ChartParaOperaS[k].mtdMgr.loadFromFile(binaryReader_, ref svaddr);
						try
						{
							string strMtdFilePath = this[i].misMgr.ChartParaOperaS[k].mtdMgr.strMtdFilePath;
							InsDeviceManager insDeviceManager = new InsDeviceManager();
							this[i].misMgr.ChartParaOperaS[k].mtdMgr.LoadFromFile(strMtdFilePath, insDeviceManager);
						}
						catch (Exception)
						{
						}
						IArrayBase.NewArray(ref this[i].misMgr.ChartParaOperaS[k].tProgram, binaryReader_.ReadInt32());
						for (int m = 0; m < this[i].misMgr.ChartParaOperaS[k].tProgram.Count; m++)
						{
							this[i].misMgr.ChartParaOperaS[k].tProgram[m].TimeValue = binaryReader_.ReadDouble();
							this[i].misMgr.ChartParaOperaS[k].tProgram[m].TestCard = binaryReader_.ReadInt32();
						}
						IArrayBase.NewArray(ref this[i].misMgr.ChartParaOperaS[k].evenPara, binaryReader_.ReadInt32());
						for (int n = 0; n < this[i].misMgr.ChartParaOperaS[k].evenPara.Count; n++)
						{
							this[i].misMgr.ChartParaOperaS[k].evenPara[n].TimeStart = binaryReader_.ReadDouble();
							this[i].misMgr.ChartParaOperaS[k].evenPara[n].TimeEnd = binaryReader_.ReadDouble();
						}
					}
				}
				catch (Exception ex2)
				{
					LogMgr.Instance.LogError(ex2.Message);
				}
			}
		}
		catch (Exception ex3)
		{
			LogMgr.Instance.LogError("ChromDeviceListMge LoadFromFileOld " + ex3.Message);
			result = false;
		}
		finally
		{
			Class49.FileStreamClose(ref fileStream_, ref binaryReader_);
		}
		return true;
	}

	public void SaveToFileB(string fileName)
	{
		Program.WriteLine("重新存储");
		for (int i = 0; i < Count; i++)
		{
		}
		FileInfo fileInfo_ = null;
		FileStream fileStream_ = null;
		BinaryWriter binaryWriter_ = null;
		try
		{
			Class49.OpenBinaryWriter(fileName, out fileInfo_, out fileStream_, out binaryWriter_);
			binaryWriter_.Write("IBrainChrom");
			binaryWriter_.Write(DateTime.Now.ToString());
			binaryWriter_.Write(Count);
			for (int j = 0; j < Count; j++)
			{
				binaryWriter_.Write(this[j].info.ID);
				binaryWriter_.Write(this[j].info.ModBusDeviceID);
				binaryWriter_.Write(this[j].info.DepartMent);
				binaryWriter_.Write(this[j].info.Name);
				binaryWriter_.Write(this[j].info.Other);
				binaryWriter_.Write(this[j].misMgr.devManager.Msg.AutoSendByStopTime);
				if (this[j].misMgr.devManager.Msg.Mess == null)
				{
					this[j].misMgr.devManager.Msg.Mess = "Mess";
				}
				binaryWriter_.Write(this[j].misMgr.devManager.Msg.Mess);
				binaryWriter_.Write(this[j].misMgr.devManager.Msg.sound);
				if (this[j].misMgr.devManager.Msg.soundTimes <= 0)
				{
					this[j].misMgr.devManager.Msg.soundTimes = 0;
				}
				binaryWriter_.Write(this[j].misMgr.devManager.Msg.soundTimes);
				for (int k = 0; k < this[j].misMgr.ChannelChartParaS.Count; k++)
				{
					binaryWriter_.Write((int)this[j].misMgr.ChannelChartParaS[k].cnlBasisQuantity);
					binaryWriter_.Write((int)this[j].misMgr.ChannelChartParaS[k].cnlDetectMethod);
					binaryWriter_.Write(this[j].misMgr.ChannelChartParaS[k].fullScreenTime);
					binaryWriter_.Write(this[j].misMgr.ChannelChartParaS[k].bClearZero);
					binaryWriter_.Write(this[j].misMgr.ChannelChartParaS[k].printWhenStop);
					binaryWriter_.Write(this[j].misMgr.ChannelChartParaS[k].analysisWhenStop);
					binaryWriter_.Write(this[j].misMgr.ChannelChartParaS[k].bFullScreen);
					binaryWriter_.Write(this[j].misMgr.ChannelChartParaS[k].stopTime);
					binaryWriter_.Write(this[j].misMgr.ChannelChartParaS[k].showHighLimit);
					binaryWriter_.Write(this[j].misMgr.ChannelChartParaS[k].showLowLimit);
					binaryWriter_.Write(this[j].misMgr.ChannelChartParaS[k].bBaselineDeduction);
				}
				for (int l = 0; l < this[j].misMgr.ChartParaOperaS.Count; l++)
				{
					this[j].misMgr.ChartParaOperaS[l].mtdMgr.printPara.WriteToFile(binaryWriter_);
					binaryWriter_.Write(this[j].misMgr.ChartParaOperaS[l].TemplatePath);
					binaryWriter_.Write((int)this[j].misMgr.ChartParaOperaS[l].cnlDetectMethod);
					binaryWriter_.Write((int)this[j].misMgr.ChartParaOperaS[l].cnlBasisQuantity);
					binaryWriter_.Write(this[j].misMgr.ChartParaOperaS[l].FileNameAquipName);
					binaryWriter_.Write(this[j].misMgr.ChartParaOperaS[l].FileNameAutoInject);
					binaryWriter_.Write(this[j].misMgr.ChartParaOperaS[l].FileNameChannelName);
					binaryWriter_.Write(this[j].misMgr.ChartParaOperaS[l].FileNameDateTime);
					binaryWriter_.Write(this[j].misMgr.ChartParaOperaS[l].InjectIndex);
					if (this[j].misMgr.ChartParaOperaS[l].FileUserSet == null)
					{
						this[j].misMgr.ChartParaOperaS[l].FileUserSet = "";
					}
					binaryWriter_.Write(this[j].misMgr.ChartParaOperaS[l].FileUserSet);
					binaryWriter_.Write(this[j].misMgr.ChartParaOperaS[l].UseUserZeroTime);
					binaryWriter_.Write(this[j].misMgr.ChartParaOperaS[l].ZeroTime);
					binaryWriter_.Write(this[j].misMgr.ChartParaOperaS[l].ZeroTimeLeft);
					binaryWriter_.Write(this[j].misMgr.ChartParaOperaS[l].ZeroTimeRight);
					binaryWriter_.Write(this[j].misMgr.ChartParaOperaS[l].componentList.Count);
					for (int m = 0; m < this[j].misMgr.ChartParaOperaS[l].componentList.Count; m++)
					{
						binaryWriter_.Write(this[j].misMgr.ChartParaOperaS[l].componentList[m].JuseTimeCheck);
						binaryWriter_.Write(this[j].misMgr.ChartParaOperaS[l].componentList[m].JStdandPeakTime);
						binaryWriter_.Write(this[j].misMgr.ChartParaOperaS[l].componentList[m].JTimePara);
						if (this[j].misMgr.ChartParaOperaS[l].componentList[m].name == null)
						{
							this[j].misMgr.ChartParaOperaS[l].componentList[m].name = "";
						}
						binaryWriter_.Write(this[j].misMgr.ChartParaOperaS[l].componentList[m].name);
						binaryWriter_.Write(this[j].misMgr.ChartParaOperaS[l].componentList[m].JPeakAdjustPara);
						binaryWriter_.Write(this[j].misMgr.ChartParaOperaS[l].componentList[m].JModBusAddr);
					}
					if (this[j].misMgr.ChartParaOperaS[l].Integ == null)
					{
						this[j].misMgr.ChartParaOperaS[l].Integ = new Integration();
					}
					this[j].misMgr.ChartParaOperaS[l].Integ.SaveToFile(binaryWriter_);
					if (this[j].misMgr.ChartParaOperaS[l].mtdMgr == null)
					{
						this[j].misMgr.ChartParaOperaS[l].mtdMgr = new MtdSetup();
					}
					this[j].misMgr.ChartParaOperaS[l].mtdMgr.SaveToFile(binaryWriter_);
					binaryWriter_.Write(this[j].misMgr.ChartParaOperaS[l].tProgram.Count);
					for (int n = 0; n < this[j].misMgr.ChartParaOperaS[l].tProgram.Count; n++)
					{
						binaryWriter_.Write(this[j].misMgr.ChartParaOperaS[l].tProgram[n].TimeValue);
						binaryWriter_.Write(this[j].misMgr.ChartParaOperaS[l].tProgram[n].TestCard);
					}
					binaryWriter_.Write(this[j].misMgr.ChartParaOperaS[l].evenPara.Count);
					for (int num = 0; num < this[j].misMgr.ChartParaOperaS[l].evenPara.Count; num++)
					{
						binaryWriter_.Write(this[j].misMgr.ChartParaOperaS[l].evenPara[num].TimeStart);
						binaryWriter_.Write(this[j].misMgr.ChartParaOperaS[l].evenPara[num].TimeEnd);
					}
				}
			}
			binaryWriter_.Write("--End--");
		}
		catch (Exception ex)
		{
			LogMgr.Instance.LogError("ChromDeviceListMge SaveToFileB " + ex.Message);
		}
		finally
		{
			Class49.FileStreamClose(ref fileStream_, ref binaryWriter_);
		}
	}
}
