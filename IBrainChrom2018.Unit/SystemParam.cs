using System;
using System.Drawing;
using System.Runtime.InteropServices;

namespace IBrainChrom2018.Unit;

[Serializable]
[StructLayout(LayoutKind.Sequential)]
public class SystemParam
{
	private static SystemParam myparam = null;

	private ParamBase dbBase = ParamBase.Create();

	public int iDbConnectType;

	public string strDbName;

	public int iFileSerializeType;

	public string strMisDataFilePath;

	public string strMtdDataFileDir;

	public string strSdaDataFileDir;

	public string strCalDataFileDir;

	public string strProgramVersion = "";

	public string strCaliGnlOptUnit;

	public int iCaliGnlOptReCali;

	public int iDispMinValue;

	public bool bAllowAutoRestartListenerWhenCloseSocket;

	public bool bTestSocketError;

	public Color corChrgColorBackGround;

	public Color corChrgColoGrid;

	public Color corChrgColoAcq;

	public Color corChrgColoCurve1;

	public Color corChrgColoCurve2;

	public Color corChrgColoCurve3;

	public Color corChrgColoCurve4;

	public Color corChrgColoCurve5;

	public Color corChrgColoCurve6;

	public Color corChrgColoCurve7;

	public Color corChrgColoCurve8;

	public bool bChrgOptionShowGrid;

	public bool bChrgOptionShowPeakSplitLine;

	public bool bChrgOptionShowKeepTime;

	public bool bChrgOptionShowTempUpgrateLine;

	public int iChrgOptionShowMethod;

	public int iChrgOptionDotNumberDensity;

	public float iChrgOptionFullYOffset;

	public string strDirOptionInitDir;

	public bool bDirOptionAddChromDir;

	public bool bDirOptionAddChannelDir;

	public bool bDirOptionAddDateDir;

	public bool bFileNameOptionChrom;

	public bool bFileNameOptionChannel;

	public bool bFileNameOptionDate;

	public string strFileNameOptionChannel0Custom;

	public string strFileNameOptionChannel1Custom;

	public string strFileNameOptionChannel2Custom;

	public string strReportOptionTitle;

	public bool bReportOptionPrintTime;

	public bool bReportOptionInjectTime;

	public bool bReportOptionFileName;

	public bool bReportOptionResultData;

	public bool bReportOptionResultOrgCurve;

	public bool bReportOptionResultChromGraphic;

	public bool bReportOptionChromLineBold;

	public int bReportOptionChromFontSize;

	public int iDcsComNumber;

	public bool bDcsComEnable;

	public float fDcsMinValue;

	public float fDcsMaxValue;

	public string strIpLocal;

	public string strIpMask;

	public string strIpGateway;

	public int iComNumber;

	public bool bComEnable;

	public int iComModbusType;

	public string strPasswordAdmin;

	public string strPasswordAns;

	public string strPasswordGuest;

	public string strPasswordNew;

	public string strPasswordOld;

	public string strShowColumn_GvRltsGnl;

	public string strShowColumn_GvSummary;

	public string strShowColumn_GvSummaryGeneral;

	public string strShowColumn_GvPerformStatic;

	public string strStationId;

	public bool bMqttEnable;

	public string strMqttHost;

	public int iMqttPort;

	public bool bMqttTls;

	public bool bMqttTlsAllowUntrusted;

	public string strMqttUser;

	public string strMqttPassword;

	public string strMqttClientId;

	public string strMqttTopicPrefix;

	public int iMqttHeartbeatSec;

	public string Language
	{
		get
		{
			return Lang.LangCode;
		}
		set
		{
			Lang.SetLangID(value);
		}
	}

	public string ChromComNumberString => GetComStr(iComNumber);

	public string DcsComNumberString => GetComStr(iDcsComNumber);

	public static SystemParam Create()
	{
		if (myparam == null)
		{
			myparam = new SystemParam();
		}
		return myparam;
	}

	private SystemParam()
	{
		LoadParam();
		if (!dbBase.ConfigFileExist)
		{
			SaveParam();
		}
	}

	public void ResetParam()
	{
		dbBase.ClearSection("SystemParam");
		LoadParam();
	}

	public void LoadParam()
	{
		iDbConnectType = int.Parse(dbBase.GetValue("SystemParam", "iDbConnectType", "1"));
		strDbName = dbBase.GetValue("SystemParam", "strDbName", "dbbase.mdb");
		iFileSerializeType = int.Parse(dbBase.GetValue("SystemParam", "iFileSerializeType", "0"));
		strMisDataFilePath = dbBase.GetValue("SystemParam", "strMisDataFilePath", "");
		strMtdDataFileDir = dbBase.GetValue("SystemParam", "strMtdDataFileDir", "");
		strSdaDataFileDir = dbBase.GetValue("SystemParam", "strSdaDataFileDir", "");
		strCalDataFileDir = dbBase.GetValue("SystemParam", "strCalDataFileDir", "");
		strProgramVersion = dbBase.GetValue("SystemParam", "strProgramVersion", "");
		iDispMinValue = dbBase.GetValue("SystemParam", "iDispMinValue", "10", iDispMinValue);
		iCaliGnlOptReCali = int.Parse(dbBase.GetValue("SystemParam", "iCaliGnlOptReCali", "0"));
		strCaliGnlOptUnit = dbBase.GetValue("SystemParam", "strCaliGnlOptUnit", "10-6V/V");
		bAllowAutoRestartListenerWhenCloseSocket = dbBase.GetValue("SystemParam", "bAllowAutoRestartListenerWhenCloseSocket", "1", bAllowAutoRestartListenerWhenCloseSocket);
		if (Language == "zh-cn")
		{
			Class49.sysLanguage_0 = SysLanguage.CN;
		}
		else if (Language == "en")
		{
			Class49.sysLanguage_0 = SysLanguage.EN;
		}
		else
		{
			Class49.sysLanguage_0 = SysLanguage.CN;
		}
		corChrgColorBackGround = Color.FromArgb(int.Parse(dbBase.GetValue("SystemParam", "corChrgColorBackGround", "-1")));
		corChrgColoGrid = Color.FromArgb(int.Parse(dbBase.GetValue("SystemParam", "corChrgColoGrid", "-2039584")));
		corChrgColoAcq = Color.FromArgb(int.Parse(dbBase.GetValue("SystemParam", "corChrgColoAcq", "-16777216")));
		corChrgColoCurve1 = Color.FromArgb(int.Parse(dbBase.GetValue("SystemParam", "corChrgColoCurve1", "-16776961")));
		corChrgColoCurve2 = Color.FromArgb(int.Parse(dbBase.GetValue("SystemParam", "corChrgColoCurve2", "-65536")));
		corChrgColoCurve3 = Color.FromArgb(int.Parse(dbBase.GetValue("SystemParam", "corChrgColoCurve3", "-16711936")));
		corChrgColoCurve4 = Color.FromArgb(int.Parse(dbBase.GetValue("SystemParam", "corChrgColoCurve4", "-16711681")));
		corChrgColoCurve5 = Color.FromArgb(int.Parse(dbBase.GetValue("SystemParam", "corChrgColoCurve5", "-65281")));
		corChrgColoCurve6 = Color.FromArgb(int.Parse(dbBase.GetValue("SystemParam", "corChrgColoCurve6", "-256")));
		corChrgColoCurve7 = Color.FromArgb(int.Parse(dbBase.GetValue("SystemParam", "corChrgColoCurve7", "-32768")));
		corChrgColoCurve8 = Color.FromArgb(int.Parse(dbBase.GetValue("SystemParam", "corChrgColoCurve8", "-4144897")));
		corChrgColorBackGround = Class49.color_0[0];
		corChrgColoCurve1 = Class49.color_0[1];
		corChrgColoCurve2 = Class49.color_0[1];
		corChrgColoCurve3 = Class49.color_0[1];
		corChrgColoCurve4 = Class49.color_0[1];
		corChrgColoCurve5 = Class49.color_0[1];
		corChrgColoCurve6 = Class49.color_0[1];
		corChrgColoCurve7 = Class49.color_0[1];
		corChrgColoCurve8 = Class49.color_0[1];
		bChrgOptionShowGrid = dbBase.GetValue("SystemParam", "bChrgOptionShowGrid", "1") == "1";
		bChrgOptionShowPeakSplitLine = dbBase.GetValue("SystemParam", "bChrgOptionShowPeakSplitLine", "1") == "1";
		bChrgOptionShowKeepTime = dbBase.GetValue("SystemParam", "bChrgOptionShowKeepTime", "1") == "1";
		bChrgOptionShowTempUpgrateLine = dbBase.GetValue("SystemParam", "bChrgOptionShowTempUpgrateLine", "0") == "1";
		iChrgOptionShowMethod = int.Parse(dbBase.GetValue("SystemParam", "iChrgOptionShowMethod", "0"));
		iChrgOptionDotNumberDensity = int.Parse(dbBase.GetValue("SystemParam", "iChrgOptionDotNumberDensity", "4"));
		iChrgOptionFullYOffset = int.Parse(dbBase.GetValue("SystemParam", "iChrgOptionFullYOffset", "0"));
		strDirOptionInitDir = dbBase.GetValue("SystemParam", "strDirOptionInitDir", "D:\\IBrainChrom\\");
		bDirOptionAddChromDir = dbBase.GetValue("SystemParam", "bDirOptionAddChromDir", "1") == "1";
		bDirOptionAddChannelDir = dbBase.GetValue("SystemParam", "bDirOptionAddChannelDir", "1") == "1";
		bDirOptionAddDateDir = dbBase.GetValue("SystemParam", "bDirOptionAddDateDir", "1") == "1";
		bFileNameOptionChrom = dbBase.GetValue("SystemParam", "bFileNameOptionChrom", "1") == "1";
		bFileNameOptionChannel = dbBase.GetValue("SystemParam", "bFileNameOptionChannel", "1") == "1";
		bFileNameOptionDate = dbBase.GetValue("SystemParam", "bFileNameOptionDate", "1") == "1";
		strFileNameOptionChannel0Custom = dbBase.GetValue("SystemParam", "strFileNameOptionChannel0Custom", "");
		strFileNameOptionChannel1Custom = dbBase.GetValue("SystemParam", "strFileNameOptionChannel1Custom", "");
		strFileNameOptionChannel2Custom = dbBase.GetValue("SystemParam", "strFileNameOptionChannel2Custom", "");
		strReportOptionTitle = dbBase.GetValue("SystemParam", "strReportOptionTitle", "");
		bReportOptionPrintTime = dbBase.GetValue("SystemParam", "bReportOptionPrintTime", "1") == "1";
		bReportOptionInjectTime = dbBase.GetValue("SystemParam", "bReportOptionInjectTime", "1") == "1";
		bReportOptionFileName = dbBase.GetValue("SystemParam", "bReportOptionFileName", "1") == "1";
		bReportOptionResultData = dbBase.GetValue("SystemParam", "bReportOptionResultData", "1") == "1";
		bReportOptionResultOrgCurve = dbBase.GetValue("SystemParam", "bReportOptionResultOrgCurve", "1") == "1";
		bReportOptionResultChromGraphic = dbBase.GetValue("SystemParam", "bReportOptionResultChromGraphic", "1") == "1";
		bReportOptionChromLineBold = dbBase.GetValue("SystemParam", "bReportOptionChromLineBold", "1") == "1";
		bReportOptionChromFontSize = int.Parse(dbBase.GetValue("SystemParam", "bReportOptionChromFontSize", "4"));
		iDcsComNumber = int.Parse(dbBase.GetValue("SystemParam", "iDcsComNumber", "0"));
		bDcsComEnable = dbBase.GetValue("SystemParam", "bDcsComEnable", "0") == "1";
		fDcsMinValue = float.Parse(dbBase.GetValue("SystemParam", "fDcsMinValue", "0"));
		fDcsMaxValue = float.Parse(dbBase.GetValue("SystemParam", "fDcsMaxValue", "100"));
		strIpLocal = dbBase.GetValue("SystemParam", "strIpLocal", "192.168.1.100");
		strIpMask = dbBase.GetValue("SystemParam", "strIpMask", "255.255.255.0");
		strIpGateway = dbBase.GetValue("SystemParam", "strIpGateway", "192.168.1.1");
		iComNumber = int.Parse(dbBase.GetValue("SystemParam", "iComNumber", "0"));
		bComEnable = dbBase.GetValue("SystemParam", "bComEnable", "0") == "1";
		iComModbusType = int.Parse(dbBase.GetValue("SystemParam", "iComModbusType", "0"));
		strPasswordAdmin = dbBase.GetValue("SystemParam", "strPasswordAdmin", "");
		strPasswordAns = dbBase.GetValue("SystemParam", "strPasswordAns", "");
		strPasswordGuest = dbBase.GetValue("SystemParam", "strPasswordGuest", "");
		strShowColumn_GvRltsGnl = dbBase.GetValue("SystemParam", "strShowColumn_GvRltsGnl", "", strShowColumn_GvRltsGnl);
		strShowColumn_GvSummary = dbBase.GetValue("SystemParam", "strShowColumn_GvSummary", "", strShowColumn_GvSummary);
		strShowColumn_GvSummaryGeneral = dbBase.GetValue("SystemParam", "strShowColumn_GvSummaryGeneral", "", strShowColumn_GvSummaryGeneral);
		strShowColumn_GvPerformStatic = dbBase.GetValue("SystemParam", "strShowColumn_GvPerformStatic", "", strShowColumn_GvPerformStatic);
		strStationId = NormalizeStationId24Ascii(dbBase.GetValue("SystemParam", "strStationId", "69000000001ABCDEFG123456"));
		bMqttEnable = dbBase.GetValue("SystemParam", "bMqttEnable", "0") == "1";
		strMqttHost = dbBase.GetValue("SystemParam", "strMqttHost", "");
		iMqttPort = int.Parse(dbBase.GetValue("SystemParam", "iMqttPort", "1883"));
		bMqttTls = dbBase.GetValue("SystemParam", "bMqttTls", "0") == "1";
		bMqttTlsAllowUntrusted = dbBase.GetValue("SystemParam", "bMqttTlsAllowUntrusted", "0") == "1";
		strMqttUser = dbBase.GetValue("SystemParam", "strMqttUser", "");
		strMqttPassword = dbBase.GetValue("SystemParam", "strMqttPassword", "");
		strMqttClientId = dbBase.GetValue("SystemParam", "strMqttClientId", "");
		strMqttTopicPrefix = dbBase.GetValue("SystemParam", "strMqttTopicPrefix", "chrom/v1/default/default/{stationId}");
		iMqttHeartbeatSec = int.Parse(dbBase.GetValue("SystemParam", "iMqttHeartbeatSec", "60"));
		if (dbBase.NeedConfigUpgrade)
		{
			corChrgColoAcq = Color.FromArgb(int.Parse("-16777216"));
			corChrgColoCurve1 = Color.FromArgb(int.Parse("-16776961"));
			dbBase.SetValue("SystemParam", "corChrgColoAcq", corChrgColoAcq);
			dbBase.SetValue("SystemParam", "corChrgColoCurve1", corChrgColoCurve1);
			dbBase.Save();
		}
	}

	public static string NormalizeStationId24Ascii(string value)
	{
		string text = value ?? "";
		if (text.Length > 24)
		{
			text = text.Substring(0, 24);
		}
		if (text.Length < 24)
		{
			text = text.PadRight(24, ' ');
		}
		char[] array = text.ToCharArray();
		for (int i = 0; i < array.Length; i++)
		{
			char c = array[i];
			if (c < ' ' || c > '~')
			{
				array[i] = '?';
			}
		}
		return new string(array);
	}

	public void SaveParam()
	{
		dbBase.SetValue("SystemParam", "strDbName", strDbName);
		dbBase.SetValue("SystemParam", "iDbConnectType", iDbConnectType.ToString());
		dbBase.SetValue("SystemParam", "iFileSerializeType", iFileSerializeType.ToString());
		dbBase.SetValue("SystemParam", "strMisDataFilePath", strMisDataFilePath);
		dbBase.SetValue("SystemParam", "strMtdDataFileDir", strMtdDataFileDir);
		dbBase.SetValue("SystemParam", "strSdaDataFileDir", strSdaDataFileDir);
		dbBase.SetValue("SystemParam", "strCalDataFileDir", strCalDataFileDir);
		dbBase.SetValue("SystemParam", "strProgramVersion", strProgramVersion);
		dbBase.SetValue("SystemParam", "strCaliGnlOptUnit", strCaliGnlOptUnit);
		dbBase.SetValue("SystemParam", "iCaliGnlOptReCali", iCaliGnlOptReCali.ToString());
		dbBase.SetValue("SystemParam", "iDispMinValue", iDispMinValue);
		dbBase.SetValue("SystemParam", "bAllowAutoRestartListenerWhenCloseSocket", bAllowAutoRestartListenerWhenCloseSocket);
		dbBase.SetValue("SystemParam", "corChrgColorBackGround", corChrgColorBackGround);
		dbBase.SetValue("SystemParam", "corChrgColoGrid", corChrgColoGrid);
		dbBase.SetValue("SystemParam", "corChrgColoAcq", corChrgColoAcq);
		dbBase.SetValue("SystemParam", "corChrgColoCurve1", corChrgColoCurve1);
		dbBase.SetValue("SystemParam", "corChrgColoCurve2", corChrgColoCurve2);
		dbBase.SetValue("SystemParam", "corChrgColoCurve3", corChrgColoCurve3);
		dbBase.SetValue("SystemParam", "corChrgColoCurve4", corChrgColoCurve4);
		dbBase.SetValue("SystemParam", "corChrgColoCurve5", corChrgColoCurve5);
		dbBase.SetValue("SystemParam", "corChrgColoCurve6", corChrgColoCurve6);
		dbBase.SetValue("SystemParam", "corChrgColoCurve7", corChrgColoCurve7);
		dbBase.SetValue("SystemParam", "corChrgColoCurve8", corChrgColoCurve8);
		dbBase.SetValue("SystemParam", "bChrgOptionShowGrid", bChrgOptionShowGrid);
		dbBase.SetValue("SystemParam", "bChrgOptionShowPeakSplitLine", bChrgOptionShowPeakSplitLine);
		dbBase.SetValue("SystemParam", "bChrgOptionShowKeepTime", bChrgOptionShowKeepTime);
		dbBase.SetValue("SystemParam", "bChrgOptionShowTempUpgrateLine", bChrgOptionShowTempUpgrateLine);
		dbBase.SetValue("SystemParam", "iChrgOptionShowMethod", iChrgOptionShowMethod);
		dbBase.SetValue("SystemParam", "iChrgOptionDotNumberDensity", iChrgOptionDotNumberDensity);
		dbBase.SetValue("SystemParam", "iChrgOptionFullYOffset", iChrgOptionFullYOffset);
		dbBase.SetValue("SystemParam", "strDirOptionInitDir", strDirOptionInitDir);
		dbBase.SetValue("SystemParam", "bDirOptionAddChromDir", bDirOptionAddChromDir);
		dbBase.SetValue("SystemParam", "bDirOptionAddChannelDir", bDirOptionAddChannelDir);
		dbBase.SetValue("SystemParam", "bDirOptionAddDateDir", bDirOptionAddDateDir);
		dbBase.SetValue("SystemParam", "bFileNameOptionChrom", bFileNameOptionChrom);
		dbBase.SetValue("SystemParam", "bFileNameOptionChannel", bFileNameOptionChannel);
		dbBase.SetValue("SystemParam", "bFileNameOptionDate", bFileNameOptionDate);
		dbBase.SetValue("SystemParam", "strFileNameOptionChannel0Custom", strFileNameOptionChannel0Custom);
		dbBase.SetValue("SystemParam", "strFileNameOptionChannel1Custom", strFileNameOptionChannel1Custom);
		dbBase.SetValue("SystemParam", "strFileNameOptionChannel2Custom", strFileNameOptionChannel2Custom);
		dbBase.SetValue("SystemParam", "strReportOptionTitle", strReportOptionTitle);
		dbBase.SetValue("SystemParam", "bReportOptionPrintTime", bReportOptionPrintTime);
		dbBase.SetValue("SystemParam", "bReportOptionInjectTime", bReportOptionInjectTime);
		dbBase.SetValue("SystemParam", "bReportOptionFileName", bReportOptionFileName);
		dbBase.SetValue("SystemParam", "bReportOptionResultData", bReportOptionResultData);
		dbBase.SetValue("SystemParam", "bReportOptionResultOrgCurve", bReportOptionResultOrgCurve);
		dbBase.SetValue("SystemParam", "bReportOptionResultChromGraphic", bReportOptionResultChromGraphic);
		dbBase.SetValue("SystemParam", "bReportOptionChromLineBold", bReportOptionChromLineBold);
		dbBase.SetValue("SystemParam", "bReportOptionChromFontSize", bReportOptionChromFontSize);
		dbBase.SetValue("SystemParam", "iDcsComNumber", iDcsComNumber);
		dbBase.SetValue("SystemParam", "bDcsComEnable", bDcsComEnable);
		dbBase.SetValue("SystemParam", "fDcsMinValue", fDcsMinValue);
		dbBase.SetValue("SystemParam", "fDcsMaxValue", fDcsMaxValue);
		dbBase.SetValue("SystemParam", "strIpLocal", strIpLocal);
		dbBase.SetValue("SystemParam", "strIpMask", strIpMask);
		dbBase.SetValue("SystemParam", "strIpGateway", strIpGateway);
		dbBase.SetValue("SystemParam", "iComNumber", iComNumber);
		dbBase.SetValue("SystemParam", "bComEnable", bComEnable);
		dbBase.SetValue("SystemParam", "iComModbusType", iComModbusType);
		dbBase.SetValue("SystemParam", "strPasswordAdmin", strPasswordAdmin);
		dbBase.SetValue("SystemParam", "strPasswordAns", strPasswordAns);
		dbBase.SetValue("SystemParam", "strPasswordGuest", strPasswordGuest);
		dbBase.SetValue("SystemParam", "strShowColumn_GvRltsGnl", strShowColumn_GvRltsGnl);
		dbBase.SetValue("SystemParam", "strShowColumn_GvSummary", strShowColumn_GvSummary);
		dbBase.SetValue("SystemParam", "strShowColumn_GvSummaryGeneral", strShowColumn_GvSummaryGeneral);
		dbBase.SetValue("SystemParam", "strShowColumn_GvPerformStatic", strShowColumn_GvPerformStatic);
		dbBase.SetValue("SystemParam", "strStationId", NormalizeStationId24Ascii(strStationId));
		dbBase.SetValue("SystemParam", "bMqttEnable", bMqttEnable);
		dbBase.SetValue("SystemParam", "strMqttHost", strMqttHost);
		dbBase.SetValue("SystemParam", "iMqttPort", iMqttPort.ToString());
		dbBase.SetValue("SystemParam", "bMqttTls", bMqttTls);
		dbBase.SetValue("SystemParam", "bMqttTlsAllowUntrusted", bMqttTlsAllowUntrusted);
		dbBase.SetValue("SystemParam", "strMqttUser", strMqttUser);
		dbBase.SetValue("SystemParam", "strMqttPassword", strMqttPassword);
		dbBase.SetValue("SystemParam", "strMqttClientId", strMqttClientId);
		dbBase.SetValue("SystemParam", "strMqttTopicPrefix", strMqttTopicPrefix);
		dbBase.SetValue("SystemParam", "iMqttHeartbeatSec", iMqttHeartbeatSec.ToString());
		dbBase.Save();
	}

	public Color GetChannelColor(int iChannel)
	{
		return iChannel switch
		{
			0 => corChrgColoCurve1, 
			1 => corChrgColoCurve2, 
			2 => corChrgColoCurve3, 
			3 => corChrgColoCurve4, 
			4 => corChrgColoCurve5, 
			5 => corChrgColoCurve6, 
			6 => corChrgColoCurve7, 
			7 => corChrgColoCurve8, 
			_ => corChrgColoCurve1, 
		};
	}

	public string GetComStr(int iCom)
	{
		return "COM" + iCom;
	}
}
