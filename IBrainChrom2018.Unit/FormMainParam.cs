using System;
using System.Runtime.InteropServices;

namespace IBrainChrom2018.Unit;

[Serializable]
[StructLayout(LayoutKind.Sequential)]
public class FormMainParam
{
	private static FormMainParam myparam = null;

	private ParamBase dbBase = ParamBase.Create();

	public string strPassword;

	public bool bAutoSelect;

	public bool bTestModbus;

	public float fShuaijian;

	public float fShuaijian2;

	public float fShuaijian3;

	public bool bAutoheight = true;

	public string strCompanyNameVOC = "";

	public int iSmooths = 16;

	public bool bMinus = false;

	public int kindMachine;

	public int epcMode;

	public string strMethod1;

	public string strMethod2;

	public int iCom2;

	public float fValueO;

	public bool bO;

	public string strSdaFile;

	public float fFireOn;

	public float fFireOn2;

	public float UpperLimit;

	public float LowerLimit;

	public float fHyd;

	public float fCh4;

	public float fNMHC;

	public float fO2;

	public bool bEnO2 = true;

	public bool bEnNMHC = false;

	public int iChannelNumber;

	public int iComTimes;

	public int iCycleTimes;

	public float fAnyTime;

	public float fInjecTime;

	public bool bTwoDector;

	public bool bTcdDector;

	public float fmount201;

	public float fmount202;

	public float fmount203;

	public float fmount204;

	public float fmount205;

	public float fmount206;

	public float fmount207;

	public float fmount208;

	public float fmount209;

	public float fmount2010;

	public float fmount2011;

	public float fmount2012;

	public float fmount41;

	public float fmount42;

	public float fmount43;

	public float fmount44;

	public float fmount45;

	public float fmount46;

	public float fmount47;

	public float fmount48;

	public float fmount49;

	public float fmount410;

	public float fmount411;

	public float fmount412;

	public float fCompen1;

	public float fCompen2;

	public float fCompen3;

	public float fCompen4;

	public float fCompen5;

	public float fCompen6;

	public float fCompen7;

	public float fCompen8;

	public float fCompen9;

	public float fCompen10;

	public float fCompen11;

	public float fCompen12;

	public int iChannelACnt;

	public bool bChannel1 = false;

	public bool bChannel2 = false;

	public bool bChannel3 = false;

	public bool bChannel4 = false;

	public bool bChannel5 = false;

	public bool bChannel6 = false;

	public bool bChannel7 = false;

	public bool bChannel8 = false;

	public float fTotalSAlarm;

	public float fHSAlarm;

	public string strName1 = "";

	public string strName2 = "";

	public string strName3 = "";

	public int iOnlineMode = 0;

	public string strMethodFilePath1 = "";

	public string strMethodFilePath2 = "";

	public string strMethodFilePath3 = "";

	public float fInjecTime1 = 0f;

	public float fInjecTime2 = 0f;

	public float fInjecTime3 = 0f;

	public float fInjecDelay = 0f;

	public bool bChbChannel1 = false;

	public bool bChbChannel2 = false;

	public bool bChbChannel3 = false;

	public float fTbCycleTime = 0f;

	public int iTbCycleTimes = 0;

	public int iDetector = 0;

	public string strDCSCom = "COM1";

	public int iBaudRate = 9600;

	public int iDataBit = 8;

	public int iStopBit = 1;

	public int iCheckBit = 0;

	public int iDevAdd = 1;

	public float fTimeInterval = 0f;

	public int iDectorNumber;

	public float fTimeOn1;

	public float fTimeOn2;

	public float fTimeOn3;

	public float fTimeOn4;

	public float fTimeOff1;

	public float fTimeOff2;

	public float fTimeOff3;

	public float fTimeOff4;

	public string strSampleSite;

	public ushort iModbusAddress;

	public ushort iModbusRegister;

	public float fInterTemp;

	public bool bTVOC;

	public bool bChannel;

	public bool bChanne2;

	public bool bChanne3;

	public bool bSF6;

	public bool bN2;

	public bool bCH4;

	public bool bC2H2;

	public bool bO2;

	public bool bC2H6;

	public bool bCO;

	public bool bC2H4;

	public bool bCO2;

	public string strCPU;

	public bool bBen;

	public float fTabChannel1;

	public float fTabChannel2;

	public float fPowerOnDealy;

	public int iTimesCycle1;

	public int iTimesCycle2;

	public bool bCycle;

	public bool bCycle2;

	public bool bHHS;

	public bool bC3H8;

	public bool bC4H10Y;

	public bool bC4H10Z;

	public bool bSO2;

	public bool[] bSum = new bool[200];

	public string strName = "";

	public DateTime dataTimeStart;

	public DateTime dataTimeEnd;

	public float fTemp;

	public float fAtm;

	public float fInjectionVolume;

	public static FormMainParam Create()
	{
		if (myparam == null)
		{
			myparam = new FormMainParam();
		}
		return myparam;
	}

	private FormMainParam()
	{
		LoadParam();
		if (!dbBase.ConfigFileExist)
		{
			SaveParam();
		}
	}

	public void ResetParam()
	{
		dbBase.ClearSection("FormMainParam");
		LoadParam();
	}

	public void LoadParam()
	{
		strPassword = dbBase.GetValue("FormMainParam", "strPassword", "369852");
		fTimeInterval = float.Parse(dbBase.GetValue("FormMainParam", "fTimeInterval", "0"));
		dataTimeStart = DateTime.Parse(dbBase.GetValue("FormMainParam", "dataTimeStart", DateTime.Now.ToString("yyyy-MM-dd 00:00:01")));
		dataTimeEnd = DateTime.Parse(dbBase.GetValue("FormMainParam", "dataTimeEnd", DateTime.Now.ToString("yyyy-MM-dd") + " 23:59:59"));
		for (int i = 0; i < 200; i++)
		{
			bSum[i] = bool.Parse(dbBase.GetValue("FormMainParam", "bSum" + i, "false"));
		}
		fPowerOnDealy = float.Parse(dbBase.GetValue("FormMainParam", "fPowerOnDealy", "45"));
		fTabChannel1 = float.Parse(dbBase.GetValue("FormMainParam", "fTabChannel1", "1.5"));
		fTabChannel2 = float.Parse(dbBase.GetValue("FormMainParam", "fTabChannel2", "5"));
		fTimeOn1 = float.Parse(dbBase.GetValue("FormMainParam", "fTimeOn1", "0"));
		fTimeOn2 = float.Parse(dbBase.GetValue("FormMainParam", "fTimeOn2", "0"));
		fTimeOn3 = float.Parse(dbBase.GetValue("FormMainParam", "fTimeOn3", "0"));
		fTimeOn4 = float.Parse(dbBase.GetValue("FormMainParam", "fTimeOn4", "0"));
		fTimeOff1 = float.Parse(dbBase.GetValue("FormMainParam", "fTimeOff1", "0"));
		fTimeOff2 = float.Parse(dbBase.GetValue("FormMainParam", "fTimeOff2", "0"));
		fTimeOff3 = float.Parse(dbBase.GetValue("FormMainParam", "fTimeOff3", "0"));
		fTimeOff4 = float.Parse(dbBase.GetValue("FormMainParam", "fTimeOff4", "0"));
		fInjectionVolume = float.Parse(dbBase.GetValue("FormMainParam", "fInjectionVolume", "10"));
		bBen = dbBase.GetValue("FormMainParam", "bBen", "0") == "1";
		bCycle = dbBase.GetValue("FormMainParam", "bCycle", "0") == "1";
		bCycle2 = dbBase.GetValue("FormMainParam", "bCycle2", "0") == "1";
		bTwoDector = dbBase.GetValue("FormMainParam", "bTwoDector", "0") == "1";
		bTcdDector = dbBase.GetValue("FormMainParam", "bTcdDector", "0") == "1";
		bChannel = dbBase.GetValue("FormMainParam", "bChannel", "0") == "1";
		bChanne2 = dbBase.GetValue("FormMainParam", "bChanne2", "0") == "1";
		bChanne3 = dbBase.GetValue("FormMainParam", "bChanne3", "0") == "1";
		fInterTemp = float.Parse(dbBase.GetValue("FormMainParam", "fInterTemp", "10"));
		fInjecTime1 = float.Parse(dbBase.GetValue("FormMainParam", "fInjecTime1", "0"));
		fInjecTime2 = float.Parse(dbBase.GetValue("FormMainParam", "fInjecTime2", "0"));
		fInjecTime3 = float.Parse(dbBase.GetValue("FormMainParam", "fInjecTime3", "0"));
		bChbChannel1 = dbBase.GetValue("FormMainParam", "bChbChannel1", "0") == "1";
		bChbChannel2 = dbBase.GetValue("FormMainParam", "bChbChannel2", "0") == "1";
		bChbChannel3 = dbBase.GetValue("FormMainParam", "bChbChannel3", "0") == "1";
		bAutoSelect = dbBase.GetValue("FormMainParam", "bAutoSelect", "0") == "1";
		fTbCycleTime = float.Parse(dbBase.GetValue("FormMainParam", "fTbCycleTime", "0"));
		iTbCycleTimes = int.Parse(dbBase.GetValue("FormMainParam", "iTbCycleTimes", "0"));
		iTimesCycle1 = int.Parse(dbBase.GetValue("FormMainParam", "iTimesCycle1", "0"));
		iTimesCycle2 = int.Parse(dbBase.GetValue("FormMainParam", "iTimesCycle2", "0"));
		iDetector = int.Parse(dbBase.GetValue("FormMainParam", "iDetector", "0"));
		iDevAdd = int.Parse(dbBase.GetValue("FormMainParam", "iDevAdd", "1"));
		iCom2 = int.Parse(dbBase.GetValue("FormMainParam", "iCom2", "0"));
		strDCSCom = dbBase.GetValue("FormMainParam", "strDCSCom", "COM1");
		strCPU = dbBase.GetValue("FormMainParam", "strCPU", "strCPU");
		iDectorNumber = int.Parse(dbBase.GetValue("FormMainParam", "iDectorNumber", "0"));
		iBaudRate = int.Parse(dbBase.GetValue("FormMainParam", "iBaudRate", "9600"));
		iDataBit = int.Parse(dbBase.GetValue("FormMainParam", "iDataBit", "8"));
		iStopBit = int.Parse(dbBase.GetValue("FormMainParam", "iStopBit", "1"));
		fShuaijian = float.Parse(dbBase.GetValue("FormMainParam", "fShuanjian", "1"));
		fShuaijian2 = float.Parse(dbBase.GetValue("FormMainParam", "fShuanjian2", "1"));
		fShuaijian3 = float.Parse(dbBase.GetValue("FormMainParam", "fShuanjian3", "1"));
		fTemp = float.Parse(dbBase.GetValue("FormMainParam", "fTemp", "0"));
		fAtm = float.Parse(dbBase.GetValue("FormMainParam", "fAtm", "101.3"));
		fFireOn = float.Parse(dbBase.GetValue("FormMainParam", "fFireOn", "0"));
		fFireOn2 = float.Parse(dbBase.GetValue("FormMainParam", "fFireOn2", "0"));
		fHyd = float.Parse(dbBase.GetValue("FormMainParam", "fHyd", "0"));
		fCh4 = float.Parse(dbBase.GetValue("FormMainParam", "fCh4", "0"));
		fNMHC = float.Parse(dbBase.GetValue("FormMainParam", "fNMHC", "0"));
		fO2 = float.Parse(dbBase.GetValue("FormMainParam", "fO2", "0"));
		bSF6 = dbBase.GetValue("FormMainParam", "bSF6", "0") == "1";
		bN2 = dbBase.GetValue("FormMainParam", "bN2", "0") == "1";
		bCH4 = dbBase.GetValue("FormMainParam", "bCH4", "0") == "1";
		bC2H2 = dbBase.GetValue("FormMainParam", "bC2H2", "0") == "1";
		bO2 = dbBase.GetValue("FormMainParam", "bO2", "0") == "1";
		bC2H6 = dbBase.GetValue("FormMainParam", "bC2H6", "0") == "1";
		bCO = dbBase.GetValue("FormMainParam", "bCO", "0") == "1";
		bC2H4 = dbBase.GetValue("FormMainParam", "bC2H4", "0") == "1";
		bCO2 = dbBase.GetValue("FormMainParam", "bCO2", "0") == "1";
		bHHS = dbBase.GetValue("FormMainParam", "bHHS", "0") == "1";
		bC3H8 = dbBase.GetValue("FormMainParam", "bC3H8", "0") == "1";
		bC4H10Y = dbBase.GetValue("FormMainParam", "bC4H10Y", "0") == "1";
		bC4H10Z = dbBase.GetValue("FormMainParam", "bC4H10Z", "0") == "1";
		bSO2 = dbBase.GetValue("FormMainParam", "bSO2", "0") == "1";
		bEnO2 = dbBase.GetValue("FormMainParam", "bEnO2", "0") == "1";
		bEnNMHC = dbBase.GetValue("FormMainParam", "bEnNMHC", "0") == "1";
		bTestModbus = dbBase.GetValue("FormMainParam", "bTestModbus", "0") == "1";
		bO = dbBase.GetValue("FormMainParam", "bO", "0") == "1";
		bTVOC = dbBase.GetValue("FormMainParam", "bTVOC", "0") == "1";
		strMethodFilePath1 = dbBase.GetValue("FormMainParam", "strMethodFilePath1", "");
		strMethodFilePath2 = dbBase.GetValue("FormMainParam", "strMethodFilePath2", "");
		strMethodFilePath3 = dbBase.GetValue("FormMainParam", "strMethodFilePath3", "");
		strMethod1 = dbBase.GetValue("FormMainParam", "strMethod1", "");
		strMethod2 = dbBase.GetValue("FormMainParam", "strMethod2", "");
		strName1 = dbBase.GetValue("FormMainParam", "strName1", "");
		strName2 = dbBase.GetValue("FormMainParam", "strName2", "");
		strName3 = dbBase.GetValue("FormMainParam", "strName3", "");
		strSampleSite = dbBase.GetValue("FormMainParam", "strSampleSite", "");
		strName = dbBase.GetValue("FormMainParam", "strName", "空气液氧总烃在线色谱分析系统");
		strSdaFile = dbBase.GetValue("FormMainParam", "strSdaFile", "");
		iOnlineMode = int.Parse(dbBase.GetValue("FormMainParam", "iOnlineMode", "0"));
		fInjecDelay = float.Parse(dbBase.GetValue("FormMainParam", "fInjecDelay", "0"));
		fTotalSAlarm = float.Parse(dbBase.GetValue("FormMainParam", "fTotalSAlarm", "0"));
		fHSAlarm = float.Parse(dbBase.GetValue("FormMainParam", "fHSAlarm", "0"));
		fCompen1 = float.Parse(dbBase.GetValue("FormMainParam", "fCompen1", "0"));
		fCompen2 = float.Parse(dbBase.GetValue("FormMainParam", "fCompen2", "0"));
		fCompen3 = float.Parse(dbBase.GetValue("FormMainParam", "fCompen3", "0"));
		fCompen4 = float.Parse(dbBase.GetValue("FormMainParam", "fCompen4", "0"));
		fCompen5 = float.Parse(dbBase.GetValue("FormMainParam", "fCompen5", "0"));
		fCompen6 = float.Parse(dbBase.GetValue("FormMainParam", "fCompen6", "0"));
		fCompen7 = float.Parse(dbBase.GetValue("FormMainParam", "fCompen7", "0"));
		fCompen8 = float.Parse(dbBase.GetValue("FormMainParam", "fCompen8", "0"));
		fCompen9 = float.Parse(dbBase.GetValue("FormMainParam", "fCompen9", "0"));
		fCompen10 = float.Parse(dbBase.GetValue("FormMainParam", "fCompen10", "0"));
		fCompen11 = float.Parse(dbBase.GetValue("FormMainParam", "fCompen11", "0"));
		fCompen12 = float.Parse(dbBase.GetValue("FormMainParam", "fCompen12", "0"));
		fmount41 = float.Parse(dbBase.GetValue("FormMainParam", "fmount41", "0"));
		fmount42 = float.Parse(dbBase.GetValue("FormMainParam", "fmount42", "0"));
		fmount43 = float.Parse(dbBase.GetValue("FormMainParam", "fmount43", "0"));
		fmount44 = float.Parse(dbBase.GetValue("FormMainParam", "fmount44", "0"));
		fmount45 = float.Parse(dbBase.GetValue("FormMainParam", "fmount45", "0"));
		fmount46 = float.Parse(dbBase.GetValue("FormMainParam", "fmount46", "0"));
		fmount47 = float.Parse(dbBase.GetValue("FormMainParam", "fmount47", "0"));
		fmount48 = float.Parse(dbBase.GetValue("FormMainParam", "fmount48", "0"));
		fmount49 = float.Parse(dbBase.GetValue("FormMainParam", "fmount49", "0"));
		fmount410 = float.Parse(dbBase.GetValue("FormMainParam", "fmount410", "0"));
		fmount411 = float.Parse(dbBase.GetValue("FormMainParam", "fmount411", "0"));
		fmount412 = float.Parse(dbBase.GetValue("FormMainParam", "fmount412", "0"));
		fmount201 = float.Parse(dbBase.GetValue("FormMainParam", "fmount201", "0"));
		fmount202 = float.Parse(dbBase.GetValue("FormMainParam", "fmount202", "0"));
		fmount203 = float.Parse(dbBase.GetValue("FormMainParam", "fmount203", "0"));
		fmount204 = float.Parse(dbBase.GetValue("FormMainParam", "fmount204", "0"));
		fmount205 = float.Parse(dbBase.GetValue("FormMainParam", "fmount205", "0"));
		fmount206 = float.Parse(dbBase.GetValue("FormMainParam", "fmount206", "0"));
		fmount207 = float.Parse(dbBase.GetValue("FormMainParam", "fmount207", "0"));
		fmount208 = float.Parse(dbBase.GetValue("FormMainParam", "fmount208", "0"));
		fmount209 = float.Parse(dbBase.GetValue("FormMainParam", "fmount209", "0"));
		fmount2010 = float.Parse(dbBase.GetValue("FormMainParam", "fmount2010", "0"));
		fmount2011 = float.Parse(dbBase.GetValue("FormMainParam", "fmount2011", "0"));
		fmount2012 = float.Parse(dbBase.GetValue("FormMainParam", "fmount2012", "0"));
		fValueO = float.Parse(dbBase.GetValue("FormMainParam", "fValueO", "0"));
		iChannelACnt = int.Parse(dbBase.GetValue("FormMainParam", "iChannelACnt", "10"));
		iModbusAddress = ushort.Parse(dbBase.GetValue("FormMainParam", "iModbusAddress", "100"));
		iModbusRegister = ushort.Parse(dbBase.GetValue("FormMainParam", "iModbusRegister", "14"));
		iSmooths = int.Parse(dbBase.GetValue("FormMainParam", "iSmooths", "16"));
		bMinus = dbBase.GetValue("OnLineCtrlParam", "bMinus", "0") == "1";
		kindMachine = int.Parse(dbBase.GetValue("FormMainParam", "kindMachine", "1"));
		epcMode = int.Parse(dbBase.GetValue("FormMainParam", "epcMode", "1"));
		LowerLimit = float.Parse(dbBase.GetValue("FormMainParam", "LowerLimit", "0"));
		UpperLimit = float.Parse(dbBase.GetValue("FormMainParam", "UpperLimit", "100"));
		bChannel1 = dbBase.GetValue("FormMainParam", "bChannel1", "0") == "1";
		bChannel2 = dbBase.GetValue("FormMainParam", "bChannel2", "0") == "1";
		bChannel3 = dbBase.GetValue("FormMainParam", "bChannel3", "0") == "1";
		bChannel4 = dbBase.GetValue("FormMainParam", "bChannel4", "0") == "1";
		bChannel5 = dbBase.GetValue("FormMainParam", "bChannel5", "0") == "1";
		bChannel6 = dbBase.GetValue("FormMainParam", "bChannel6", "0") == "1";
		bChannel7 = dbBase.GetValue("FormMainParam", "bChannel7", "0") == "1";
		bChannel8 = dbBase.GetValue("FormMainParam", "bChannel8", "0") == "1";
		fInjecTime = float.Parse(dbBase.GetValue("FormMainParam", "fInjecTime", "0"));
		fAnyTime = float.Parse(dbBase.GetValue("FormMainParam", "fAnyTime", "0"));
		iCycleTimes = int.Parse(dbBase.GetValue("FormMainParam", "iCycleTimes", "0"));
		iComTimes = int.Parse(dbBase.GetValue("FormMainParam", "iComTimes", "0"));
		iChannelNumber = int.Parse(dbBase.GetValue("FormMainParam", "iChannelNumber", "0"));
	}

	public void resetCom()
	{
	}

	public void SaveParam()
	{
		dbBase.SetValue("FormMainParam", "strPassword", strPassword);
		dbBase.SetValue("FormMainParam", "iDectorNumber", iDectorNumber.ToString());
		dbBase.SetValue("FormMainParam", "dataTimeStart", dataTimeStart.ToString("yyyy-MM-dd") + " 00:00:01");
		dbBase.SetValue("FormMainParam", "dataTimeEnd", dataTimeEnd.ToString("yyyy-MM-dd") + " 23:59:59");
		for (int i = 0; i < 200; i++)
		{
			dbBase.SetValue("FormMainParam", "bSum" + i, bSum[i].ToString());
		}
		dbBase.SetValue("FormMainParam", "fInterTemp", fInterTemp.ToString());
		dbBase.SetValue("FormMainParam", "fPowerOnDealy", fPowerOnDealy.ToString());
		dbBase.SetValue("FormMainParam", "fTimeInterval", fTimeInterval.ToString());
		dbBase.SetValue("FormMainParam", "fTabChannel1", fTabChannel1.ToString());
		dbBase.SetValue("FormMainParam", "fTabChannel2", fTabChannel2.ToString());
		dbBase.SetValue("FormMainParam", "fTimeOn1", fTimeOn1.ToString());
		dbBase.SetValue("FormMainParam", "fTimeOn2", fTimeOn2.ToString());
		dbBase.SetValue("FormMainParam", "fTimeOn3", fTimeOn3.ToString());
		dbBase.SetValue("FormMainParam", "fTimeOn4", fTimeOn4.ToString());
		dbBase.SetValue("FormMainParam", "fTimeOff1", fTimeOff1.ToString());
		dbBase.SetValue("FormMainParam", "fTimeOff2", fTimeOff2.ToString());
		dbBase.SetValue("FormMainParam", "fTimeOff3", fTimeOff3.ToString());
		dbBase.SetValue("FormMainParam", "fTimeOff4", fTimeOff4.ToString());
		dbBase.SetValue("FormMainParam", "fShuanjian", fShuaijian.ToString());
		dbBase.SetValue("FormMainParam", "fShuanjian2", fShuaijian2.ToString());
		dbBase.SetValue("FormMainParam", "fShuanjian3", fShuaijian3.ToString());
		dbBase.SetValue("FormMainParam", "fFireOn", fFireOn.ToString());
		dbBase.SetValue("FormMainParam", "fFireOn2", fFireOn2.ToString());
		dbBase.SetValue("FormMainParam", "fHyd", fHyd.ToString());
		dbBase.SetValue("FormMainParam", "fCh4", fCh4.ToString());
		dbBase.SetValue("FormMainParam", "fNMHC", fNMHC.ToString());
		dbBase.SetValue("FormMainParam", "fO2", fO2.ToString());
		dbBase.SetValue("FormMainParam", "fTemp", fTemp.ToString());
		dbBase.SetValue("FormMainParam", "fAtm", fAtm.ToString());
		dbBase.SetValue("FormMainParam", "fInjectionVolume", fInjectionVolume.ToString());
		dbBase.SetValue("FormMainParam", "bBen", bBen ? "1" : "0");
		dbBase.SetValue("FormMainParam", "bCycle", bCycle ? "1" : "0");
		dbBase.SetValue("FormMainParam", "bCycle2", bCycle2 ? "1" : "0");
		dbBase.SetValue("FormMainParam", "bChannel", bChannel ? "1" : "0");
		dbBase.SetValue("FormMainParam", "bChanne2", bChanne2 ? "1" : "0");
		dbBase.SetValue("FormMainParam", "bChanne3", bChanne3 ? "1" : "0");
		dbBase.SetValue("FormMainParam", "bSF6", bSF6 ? "1" : "0");
		dbBase.SetValue("FormMainParam", "bN2", bN2 ? "1" : "0");
		dbBase.SetValue("FormMainParam", "bCH4", bCH4 ? "1" : "0");
		dbBase.SetValue("FormMainParam", "bC2H2", bC2H2 ? "1" : "0");
		dbBase.SetValue("FormMainParam", "bO2", bO2 ? "1" : "0");
		dbBase.SetValue("FormMainParam", "bC2H6", bC2H6 ? "1" : "0");
		dbBase.SetValue("FormMainParam", "bCO", bCO ? "1" : "0");
		dbBase.SetValue("FormMainParam", "bC2H4", bC2H4 ? "1" : "0");
		dbBase.SetValue("FormMainParam", "bCO2", bCO2 ? "1" : "0");
		dbBase.SetValue("FormMainParam", "bHHS", bHHS ? "1" : "0");
		dbBase.SetValue("FormMainParam", "bC3H8", bC3H8 ? "1" : "0");
		dbBase.SetValue("FormMainParam", "bC4H10Y", bC4H10Y ? "1" : "0");
		dbBase.SetValue("FormMainParam", "bC4H10Z", bC4H10Z ? "1" : "0");
		dbBase.SetValue("FormMainParam", "bSO2", bSO2 ? "1" : "0");
		dbBase.SetValue("FormMainParam", "bEnO2", bEnO2 ? "1" : "0");
		dbBase.SetValue("FormMainParam", "bEnNMHC", bEnNMHC ? "1" : "0");
		dbBase.SetValue("FormMainParam", "bTestModbus", bTestModbus ? "1" : "0");
		dbBase.SetValue("FormMainParam", "bAutoSelect", bAutoSelect ? "1" : "0");
		dbBase.SetValue("FormMainParam", "bTVOC", bTVOC ? "1" : "0");
		dbBase.SetValue("FormMainParam", "bO", bO ? "1" : "0");
		dbBase.SetValue("FormMainParam", "iOnlineMode", iOnlineMode.ToString());
		dbBase.SetValue("FormMainParam", "iDevAdd", iDevAdd.ToString());
		dbBase.SetValue("FormMainParam", "iCom2", iCom2.ToString());
		dbBase.SetValue("FormMainParam", "strDCSCom", strDCSCom);
		dbBase.SetValue("FormMainParam", "strSampleSite", strSampleSite);
		dbBase.SetValue("FormMainParam", "strName1", strName1);
		dbBase.SetValue("FormMainParam", "strName2", strName2);
		dbBase.SetValue("FormMainParam", "strName3", strName3);
		dbBase.SetValue("FormMainParam", "strSdaFile", strSdaFile);
		dbBase.SetValue("FormMainParam", "fInjecDelay", fInjecDelay.ToString());
		dbBase.SetValue("FormMainParam", "fmount41", fmount41.ToString());
		dbBase.SetValue("FormMainParam", "fmount42", fmount42.ToString());
		dbBase.SetValue("FormMainParam", "fmount43", fmount43.ToString());
		dbBase.SetValue("FormMainParam", "fmount44", fmount44.ToString());
		dbBase.SetValue("FormMainParam", "fmount45", fmount45.ToString());
		dbBase.SetValue("FormMainParam", "fmount46", fmount46.ToString());
		dbBase.SetValue("FormMainParam", "fmount47", fmount47.ToString());
		dbBase.SetValue("FormMainParam", "fmount48", fmount48.ToString());
		dbBase.SetValue("FormMainParam", "fmount49", fmount49.ToString());
		dbBase.SetValue("FormMainParam", "fmount410", fmount410.ToString());
		dbBase.SetValue("FormMainParam", "fmount411", fmount411.ToString());
		dbBase.SetValue("FormMainParam", "fmount412", fmount412.ToString());
		dbBase.SetValue("FormMainParam", "fCompen1", fCompen1.ToString());
		dbBase.SetValue("FormMainParam", "fCompen2", fCompen2.ToString());
		dbBase.SetValue("FormMainParam", "fCompen3", fCompen3.ToString());
		dbBase.SetValue("FormMainParam", "fCompen4", fCompen4.ToString());
		dbBase.SetValue("FormMainParam", "fCompen5", fCompen5.ToString());
		dbBase.SetValue("FormMainParam", "fCompen6", fCompen6.ToString());
		dbBase.SetValue("FormMainParam", "fCompen7", fCompen7.ToString());
		dbBase.SetValue("FormMainParam", "fCompen8", fCompen8.ToString());
		dbBase.SetValue("FormMainParam", "fCompen9", fCompen9.ToString());
		dbBase.SetValue("FormMainParam", "fCompen10", fCompen10.ToString());
		dbBase.SetValue("FormMainParam", "fCompen11", fCompen11.ToString());
		dbBase.SetValue("FormMainParam", "fCompen12", fCompen12.ToString());
		dbBase.SetValue("FormMainParam", "fmount201", fmount201.ToString());
		dbBase.SetValue("FormMainParam", "fmount202", fmount202.ToString());
		dbBase.SetValue("FormMainParam", "fmount203", fmount203.ToString());
		dbBase.SetValue("FormMainParam", "fmount204", fmount204.ToString());
		dbBase.SetValue("FormMainParam", "fmount205", fmount205.ToString());
		dbBase.SetValue("FormMainParam", "fmount206", fmount206.ToString());
		dbBase.SetValue("FormMainParam", "fmount207", fmount207.ToString());
		dbBase.SetValue("FormMainParam", "fmount208", fmount208.ToString());
		dbBase.SetValue("FormMainParam", "fmount209", fmount209.ToString());
		dbBase.SetValue("FormMainParam", "fmount2010", fmount2010.ToString());
		dbBase.SetValue("FormMainParam", "fmount2011", fmount2011.ToString());
		dbBase.SetValue("FormMainParam", "fmount2012", fmount2012.ToString());
		dbBase.SetValue("FormMainParam", "fValueO", fValueO.ToString());
		dbBase.SetValue("FormMainParam", "iChannelACnt", iChannelACnt.ToString());
		dbBase.SetValue("FormMainParam", "iModbusAddress", iModbusAddress.ToString());
		dbBase.SetValue("FormMainParam", "iModbusRegister", iModbusRegister.ToString());
		dbBase.SetValue("FormMainParam", "strMethodFilePath1", strMethodFilePath1);
		dbBase.SetValue("FormMainParam", "strMethodFilePath2", strMethodFilePath2);
		dbBase.SetValue("FormMainParam", "strMethodFilePath3", strMethodFilePath3);
		dbBase.SetValue("FormMainParam", "strCPU", strCPU);
		dbBase.SetValue("FormMainParam", "strName", strName);
		dbBase.SetValue("FormMainParam", "iSmooths", iSmooths.ToString());
		dbBase.SetValue("FormMainParam", "iBaudRate", iBaudRate.ToString());
		dbBase.SetValue("FormMainParam", "iDataBit", iDataBit.ToString());
		dbBase.SetValue("FormMainParam", "iStopBit", iStopBit.ToString());
		dbBase.SetValue("OnLineCtrlParam", "bMinus", bMinus ? "1" : "0");
		dbBase.SetValue("FormMainParam", "kindMachine", kindMachine.ToString());
		dbBase.SetValue("FormMainParam", "epcMode", epcMode.ToString());
		dbBase.SetValue("FormMainParam", "strMethod1", strMethod1);
		dbBase.SetValue("FormMainParam", "strMethod2", strMethod2);
		dbBase.SetValue("FormMainParam", "LowerLimit", LowerLimit.ToString());
		dbBase.SetValue("FormMainParam", "UpperLimit", UpperLimit.ToString());
		dbBase.SetValue("FormMainParam", "iTimesCycle1", iTimesCycle1.ToString());
		dbBase.SetValue("FormMainParam", "iTimesCycle2", iTimesCycle2.ToString());
		dbBase.SetValue("FormMainParam", "bChannel1", bChannel1 ? "1" : "0");
		dbBase.SetValue("FormMainParam", "bChannel2", bChannel2 ? "1" : "0");
		dbBase.SetValue("FormMainParam", "bChannel3", bChannel3 ? "1" : "0");
		dbBase.SetValue("FormMainParam", "bChannel4", bChannel4 ? "1" : "0");
		dbBase.SetValue("FormMainParam", "bChannel5", bChannel5 ? "1" : "0");
		dbBase.SetValue("FormMainParam", "bChannel6", bChannel6 ? "1" : "0");
		dbBase.SetValue("FormMainParam", "bChannel7", bChannel7 ? "1" : "0");
		dbBase.SetValue("FormMainParam", "bChannel8", bChannel8 ? "1" : "0");
		dbBase.SetValue("FormMainParam", "fInjecTime", fInjecTime.ToString());
		dbBase.SetValue("FormMainParam", "fAnyTime", fAnyTime.ToString());
		dbBase.SetValue("FormMainParam", "iCycleTimes", iCycleTimes.ToString());
		dbBase.SetValue("FormMainParam", "iComTimes", iComTimes.ToString());
		dbBase.SetValue("FormMainParam", "iChannelNumber", iChannelNumber.ToString());
		dbBase.SetValue("FormMainParam", "fTotalSAlarm", fTotalSAlarm.ToString());
		dbBase.SetValue("FormMainParam", "fHSAlarm", fHSAlarm.ToString());
		dbBase.SetValue("FormMainParam", "fInjecTime1", fInjecTime1.ToString());
		dbBase.SetValue("FormMainParam", "fInjecTime2", fInjecTime2.ToString());
		dbBase.SetValue("FormMainParam", "fInjecTime3", fInjecTime3.ToString());
		dbBase.SetValue("FormMainParam", "bChbChannel1", bChbChannel1 ? "1" : "0");
		dbBase.SetValue("FormMainParam", "bChbChannel2", bChbChannel2 ? "1" : "0");
		dbBase.SetValue("FormMainParam", "bChbChannel3", bChbChannel3 ? "1" : "0");
		dbBase.SetValue("FormMainParam", "fTbCycleTime", fTbCycleTime.ToString());
		dbBase.SetValue("FormMainParam", "iTbCycleTimes", iTbCycleTimes.ToString());
		dbBase.SetValue("FormMainParam", "bTwoDector", bTwoDector ? "1" : "0");
		dbBase.SetValue("FormMainParam", "bTcdDector", bTcdDector ? "1" : "0");
		dbBase.Save();
	}
}
