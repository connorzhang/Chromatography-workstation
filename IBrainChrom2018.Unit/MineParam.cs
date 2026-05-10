using System;

namespace IBrainChrom2018.Unit;

public class MineParam
{
	private static MineParam myparam = null;

	private ParamBase dbBase = ParamBase.Create();

	public int iCurrentSamplingSiteCount;

	public string strCurrentSamplingSite;

	public float fShuanjian;

	public float fShuanjian2;

	public bool bAutoheight = true;

	public bool bcombexChannel = true;

	public bool[] benSG = new bool[60];

	public ulong enSG = 0uL;

	public double tbTimeStat = 0.0;

	public double tbTimeStop = 15.0;

	public double tbYhigh1 = 0.0;

	public double tbYlow1 = 0.0;

	public double tbYhigh2 = 0.0;

	public double tbYlow2 = 0.0;

	public double tbYhigh3 = 0.0;

	public double tbYlow3 = 0.0;

	public string[] zufenName = new string[15];

	public string[] tbChannelName = new string[60];

	public int tbCycles = 0;

	public float tbInjQTime = 0f;

	public float tbCycleQtime = 0f;

	public float tbAnalyzeTime = 0f;

	public string tbChannelName1;

	public string tbChannelName2;

	public string tbChannelName3;

	public string tbChannelName4;

	public string tbChannelName5;

	public string tbChannelName6;

	public string tbChannelName7;

	public string tbChannelName8;

	public string tbChannelName9;

	public string tbChannelName10;

	public string tbChannelName11;

	public string tbChannelName12;

	public string tbChannelName13;

	public string tbChannelName14;

	public string tbChannelName15;

	public string tbChannelName16;

	public string tbChannelName17;

	public string tbChannelName18;

	public string tbChannelName19;

	public string tbChannelName20;

	public string tbChannelName21;

	public string tbChannelName22;

	public string tbChannelName23;

	public string tbChannelName24;

	public string tbChannelName25;

	public string tbChannelName26;

	public string tbChannelName27;

	public string tbChannelName28;

	public string tbChannelName29;

	public string tbChannelName30;

	public string tbChannelName31;

	public string tbChannelName32;

	public string tbChannelName33;

	public string tbChannelName34;

	public string tbChannelName35;

	public string tbChannelName36;

	public string tbChannelName37;

	public string tbChannelName38;

	public string tbChannelName39;

	public string tbChannelName40;

	public string tbChannelName41;

	public string tbChannelName42;

	public string tbChannelName43;

	public string tbChannelName44;

	public string tbChannelName45;

	public string tbChannelName46;

	public string tbChannelName47;

	public string tbChannelName48;

	public string tbChannelName49;

	public string tbChannelName50;

	public string tbChannelName51;

	public string tbChannelName52;

	public string tbChannelName53;

	public string tbChannelName54;

	public string tbChannelName55;

	public string tbChannelName56;

	public string tbChannelName57;

	public string tbChannelName58;

	public string tbChannelName59;

	public string tbChannelName60;

	public DateTime dataTimeStart;

	public DateTime dataTimeEnd;

	public string strCompanyNameMine = "矿井在线监测";

	public static MineParam Create()
	{
		if (myparam == null)
		{
			myparam = new MineParam();
		}
		return myparam;
	}

	private MineParam()
	{
		LoadParam();
		if (!dbBase.ConfigFileExist)
		{
			SaveParam();
		}
	}

	public void ResetParam()
	{
		dbBase.ClearSection("MineParam");
		LoadParam();
	}

	public void LoadParam()
	{
		for (int i = 0; i < 60; i++)
		{
			benSG[i] = bool.Parse(dbBase.GetValue("MineParam", "benSG" + i, "false"));
		}
		for (int j = 0; j < 15; j++)
		{
			zufenName[j] = dbBase.GetValue("MineParam", "zufenName" + j, j.ToString());
		}
		fShuanjian = float.Parse(dbBase.GetValue("MineParam", "fShuanjian", "1"));
		fShuanjian2 = float.Parse(dbBase.GetValue("MineParam", "fShuanjian2", "1"));
		bAutoheight = bool.Parse(dbBase.GetValue("MineParam", "bAutoheight", "false"));
		bcombexChannel = bool.Parse(dbBase.GetValue("MineParam", "bcombexChannel", "true"));
		strCompanyNameMine = dbBase.GetValue("MineParam", "CompanyNameVOC", "1");
		enSG = ulong.Parse(dbBase.GetValue("MineParam", "enSG", "1"));
		tbCycles = int.Parse(dbBase.GetValue("MineParam", "tbCycles", "1"));
		tbInjQTime = float.Parse(dbBase.GetValue("MineParam", "tbInjQTime", "1"));
		tbCycleQtime = float.Parse(dbBase.GetValue("MineParam", "tbCycleQtime", "1"));
		tbAnalyzeTime = float.Parse(dbBase.GetValue("MineParam", "tbAnalyzeTime", "1"));
		tbTimeStat = double.Parse(dbBase.GetValue("MineParam", "tbTimeStat", "0"));
		tbTimeStop = double.Parse(dbBase.GetValue("MineParam", "tbTimeStop", "15"));
		tbYhigh1 = double.Parse(dbBase.GetValue("MineParam", "tbYhigh1", "220"));
		tbYlow1 = double.Parse(dbBase.GetValue("MineParam", "tbYlow1", "0"));
		tbYhigh2 = double.Parse(dbBase.GetValue("MineParam", "tbYhigh2", "220"));
		tbYlow2 = double.Parse(dbBase.GetValue("MineParam", "tbYlow2", "0"));
		tbYhigh3 = double.Parse(dbBase.GetValue("MineParam", "tbYhigh3", "220"));
		tbYlow3 = double.Parse(dbBase.GetValue("MineParam", "tbYlow3", "0"));
		iCurrentSamplingSiteCount = int.Parse(dbBase.GetValue("MineParam", "iCurrentSamplingSiteCount", "0"));
		tbChannelName1 = dbBase.GetValue("MineParam", "tbChannelName1", "");
		tbChannelName[0] = tbChannelName1;
		tbChannelName2 = dbBase.GetValue("MineParam", "tbChannelName2", "");
		tbChannelName[1] = tbChannelName2;
		tbChannelName3 = dbBase.GetValue("MineParam", "tbChannelName3", "");
		tbChannelName[2] = tbChannelName3;
		tbChannelName4 = dbBase.GetValue("MineParam", "tbChannelName4", "");
		tbChannelName[3] = tbChannelName4;
		tbChannelName5 = dbBase.GetValue("MineParam", "tbChannelName5", "");
		tbChannelName[4] = tbChannelName5;
		tbChannelName6 = dbBase.GetValue("MineParam", "tbChannelName6", "");
		tbChannelName[5] = tbChannelName6;
		tbChannelName7 = dbBase.GetValue("MineParam", "tbChannelName7", "");
		tbChannelName[6] = tbChannelName7;
		tbChannelName8 = dbBase.GetValue("MineParam", "tbChannelName8", "");
		tbChannelName[7] = tbChannelName8;
		tbChannelName9 = dbBase.GetValue("MineParam", "tbChannelName9", "");
		tbChannelName[8] = tbChannelName9;
		tbChannelName10 = dbBase.GetValue("MineParam", "tbChannelName10", "");
		tbChannelName[9] = tbChannelName10;
		tbChannelName11 = dbBase.GetValue("MineParam", "tbChannelName11", "");
		tbChannelName[10] = tbChannelName11;
		tbChannelName12 = dbBase.GetValue("MineParam", "tbChannelName12", "");
		tbChannelName[11] = tbChannelName12;
		tbChannelName13 = dbBase.GetValue("MineParam", "tbChannelName13", "");
		tbChannelName[12] = tbChannelName13;
		tbChannelName14 = dbBase.GetValue("MineParam", "tbChannelName14", "");
		tbChannelName[13] = tbChannelName14;
		tbChannelName15 = dbBase.GetValue("MineParam", "tbChannelName15", "");
		tbChannelName[14] = tbChannelName15;
		tbChannelName16 = dbBase.GetValue("MineParam", "tbChannelName16", "");
		tbChannelName[15] = tbChannelName16;
		tbChannelName17 = dbBase.GetValue("MineParam", "tbChannelName17", "");
		tbChannelName[16] = tbChannelName17;
		tbChannelName18 = dbBase.GetValue("MineParam", "tbChannelName18", "");
		tbChannelName[17] = tbChannelName18;
		tbChannelName19 = dbBase.GetValue("MineParam", "tbChannelName19", "");
		tbChannelName[18] = tbChannelName19;
		tbChannelName20 = dbBase.GetValue("MineParam", "tbChannelName20", "");
		tbChannelName[19] = tbChannelName20;
		tbChannelName21 = dbBase.GetValue("MineParam", "tbChannelName21", "");
		tbChannelName[20] = tbChannelName21;
		tbChannelName22 = dbBase.GetValue("MineParam", "tbChannelName22", "");
		tbChannelName[21] = tbChannelName22;
		tbChannelName23 = dbBase.GetValue("MineParam", "tbChannelName23", "");
		tbChannelName[22] = tbChannelName23;
		tbChannelName24 = dbBase.GetValue("MineParam", "tbChannelName24", "");
		tbChannelName[23] = tbChannelName24;
		tbChannelName25 = dbBase.GetValue("MineParam", "tbChannelName25", "");
		tbChannelName[24] = tbChannelName25;
		tbChannelName26 = dbBase.GetValue("MineParam", "tbChannelName26", "");
		tbChannelName[25] = tbChannelName26;
		tbChannelName27 = dbBase.GetValue("MineParam", "tbChannelName27", "");
		tbChannelName[26] = tbChannelName27;
		tbChannelName28 = dbBase.GetValue("MineParam", "tbChannelName28", "");
		tbChannelName[27] = tbChannelName28;
		tbChannelName29 = dbBase.GetValue("MineParam", "tbChannelName29", "");
		tbChannelName[28] = tbChannelName29;
		tbChannelName30 = dbBase.GetValue("MineParam", "tbChannelName30", "");
		tbChannelName[29] = tbChannelName30;
		tbChannelName31 = dbBase.GetValue("MineParam", "tbChannelName31", "");
		tbChannelName[30] = tbChannelName31;
		tbChannelName32 = dbBase.GetValue("MineParam", "tbChannelName32", "");
		tbChannelName[31] = tbChannelName32;
		tbChannelName33 = dbBase.GetValue("MineParam", "tbChannelName33", "");
		tbChannelName[32] = tbChannelName33;
		tbChannelName34 = dbBase.GetValue("MineParam", "tbChannelName34", "");
		tbChannelName[33] = tbChannelName34;
		tbChannelName35 = dbBase.GetValue("MineParam", "tbChannelName35", "");
		tbChannelName[34] = tbChannelName35;
		tbChannelName36 = dbBase.GetValue("MineParam", "tbChannelName36", "");
		tbChannelName[35] = tbChannelName36;
		tbChannelName37 = dbBase.GetValue("MineParam", "tbChannelName37", "");
		tbChannelName[36] = tbChannelName37;
		tbChannelName38 = dbBase.GetValue("MineParam", "tbChannelName38", "");
		tbChannelName[37] = tbChannelName38;
		tbChannelName39 = dbBase.GetValue("MineParam", "tbChannelName39", "");
		tbChannelName[38] = tbChannelName39;
		tbChannelName40 = dbBase.GetValue("MineParam", "tbChannelName40", "");
		tbChannelName[39] = tbChannelName40;
		tbChannelName41 = dbBase.GetValue("MineParam", "tbChannelName41", "");
		tbChannelName[40] = tbChannelName41;
		tbChannelName42 = dbBase.GetValue("MineParam", "tbChannelName42", "");
		tbChannelName[41] = tbChannelName42;
		tbChannelName43 = dbBase.GetValue("MineParam", "tbChannelName43", "");
		tbChannelName[42] = tbChannelName43;
		tbChannelName44 = dbBase.GetValue("MineParam", "tbChannelName44", "");
		tbChannelName[43] = tbChannelName44;
		tbChannelName45 = dbBase.GetValue("MineParam", "tbChannelName45", "");
		tbChannelName[44] = tbChannelName45;
		tbChannelName46 = dbBase.GetValue("MineParam", "tbChannelName46", "");
		tbChannelName[45] = tbChannelName46;
		tbChannelName47 = dbBase.GetValue("MineParam", "tbChannelName47", "");
		tbChannelName[46] = tbChannelName47;
		tbChannelName48 = dbBase.GetValue("MineParam", "tbChannelName48", "");
		tbChannelName[47] = tbChannelName48;
		tbChannelName49 = dbBase.GetValue("MineParam", "tbChannelName49", "");
		tbChannelName[48] = tbChannelName49;
		tbChannelName50 = dbBase.GetValue("MineParam", "tbChannelName50", "");
		tbChannelName[49] = tbChannelName50;
		tbChannelName51 = dbBase.GetValue("MineParam", "tbChannelName51", "");
		tbChannelName[50] = tbChannelName51;
		tbChannelName52 = dbBase.GetValue("MineParam", "tbChannelName52", "");
		tbChannelName[51] = tbChannelName52;
		tbChannelName[52] = tbChannelName53;
		tbChannelName53 = dbBase.GetValue("MineParam", "tbChannelName53", "");
		tbChannelName[53] = tbChannelName54;
		tbChannelName54 = dbBase.GetValue("MineParam", "tbChannelName54", "");
		tbChannelName[54] = tbChannelName55;
		tbChannelName55 = dbBase.GetValue("MineParam", "tbChannelName55", "");
		tbChannelName[55] = tbChannelName56;
		tbChannelName56 = dbBase.GetValue("MineParam", "tbChannelName56", "");
		tbChannelName[56] = tbChannelName57;
		tbChannelName57 = dbBase.GetValue("MineParam", "tbChannelName57", "");
		tbChannelName[57] = tbChannelName58;
		tbChannelName58 = dbBase.GetValue("MineParam", "tbChannelName58", "");
		tbChannelName[58] = tbChannelName59;
		tbChannelName59 = dbBase.GetValue("MineParam", "tbChannelName59", "");
		tbChannelName[59] = tbChannelName60;
		tbChannelName60 = dbBase.GetValue("MineParam", "tbChannelName60", "");
		strCurrentSamplingSite = dbBase.GetValue("MineParam", "strCurrentSamplingSite", "手动进样");
		dataTimeStart = DateTime.Parse(dbBase.GetValue("MineParam", "dataTimeStart", DateTime.Now.ToString("yyyy/MM/dd 00:00:01")));
		dataTimeEnd = DateTime.Parse(dbBase.GetValue("MineParam", "dataTimeEnd", DateTime.Now.ToString("yyyy/MM/dd") + " 23:59:59"));
	}

	public void SaveParam()
	{
		for (int i = 0; i < 60; i++)
		{
			dbBase.SetValue("MineParam", "benSG" + i, benSG[i].ToString());
		}
		for (int j = 0; j < 15; j++)
		{
			dbBase.SetValue("MineParam", "zufenName" + j, zufenName[j]);
		}
		dbBase.SetValue("MineParam", "fShuanjian", fShuanjian.ToString());
		dbBase.SetValue("MineParam", "fShuanjian2", fShuanjian2.ToString());
		dbBase.SetValue("MineParam", "bAutoheight", bAutoheight.ToString());
		dbBase.SetValue("MineParam", "bcombexChannel", bcombexChannel.ToString());
		dbBase.SetValue("MineParam", "enSG", enSG.ToString());
		dbBase.SetValue("MineParam", "tbCycles", tbCycles.ToString());
		dbBase.SetValue("MineParam", "tbInjQTime", tbInjQTime.ToString());
		dbBase.SetValue("MineParam", "tbCycleQtime", tbCycleQtime.ToString());
		dbBase.SetValue("MineParam", "tbAnalyzeTime", tbAnalyzeTime.ToString());
		dbBase.SetValue("MineParam", "tbTimeStat", tbTimeStat.ToString());
		dbBase.SetValue("MineParam", "tbTimeStop", tbTimeStop.ToString());
		dbBase.SetValue("MineParam", "tbYhigh1", tbYhigh1.ToString());
		dbBase.SetValue("MineParam", "tbYlow1", tbYlow1.ToString());
		dbBase.SetValue("MineParam", "tbYhigh2", tbYhigh2.ToString());
		dbBase.SetValue("MineParam", "tbYlow2", tbYlow2.ToString());
		dbBase.SetValue("MineParam", "tbYhigh3", tbYhigh3.ToString());
		dbBase.SetValue("MineParam", "tbYlow3", tbYlow3.ToString());
		dbBase.SetValue("MineParam", "iCurrentSamplingSiteCount", iCurrentSamplingSiteCount.ToString());
		dbBase.SetValue("MineParam", "tbChannelName1", tbChannelName1);
		dbBase.SetValue("MineParam", "tbChannelName2", tbChannelName2);
		dbBase.SetValue("MineParam", "tbChannelName3", tbChannelName3);
		dbBase.SetValue("MineParam", "tbChannelName4", tbChannelName4);
		dbBase.SetValue("MineParam", "tbChannelName5", tbChannelName5);
		dbBase.SetValue("MineParam", "tbChannelName6", tbChannelName6);
		dbBase.SetValue("MineParam", "tbChannelName7", tbChannelName7);
		dbBase.SetValue("MineParam", "tbChannelName8", tbChannelName8);
		dbBase.SetValue("MineParam", "tbChannelName9", tbChannelName9);
		dbBase.SetValue("MineParam", "tbChannelName10", tbChannelName10);
		dbBase.SetValue("MineParam", "tbChannelName11", tbChannelName11);
		dbBase.SetValue("MineParam", "tbChannelName12", tbChannelName12);
		dbBase.SetValue("MineParam", "tbChannelName13", tbChannelName13);
		dbBase.SetValue("MineParam", "tbChannelName14", tbChannelName14);
		dbBase.SetValue("MineParam", "tbChannelName15", tbChannelName15);
		dbBase.SetValue("MineParam", "tbChannelName16", tbChannelName16);
		dbBase.SetValue("MineParam", "tbChannelName17", tbChannelName17);
		dbBase.SetValue("MineParam", "tbChannelName18", tbChannelName18);
		dbBase.SetValue("MineParam", "tbChannelName19", tbChannelName19);
		dbBase.SetValue("MineParam", "tbChannelName20", tbChannelName20);
		dbBase.SetValue("MineParam", "tbChannelName21", tbChannelName21);
		dbBase.SetValue("MineParam", "tbChannelName22", tbChannelName22);
		dbBase.SetValue("MineParam", "tbChannelName23", tbChannelName23);
		dbBase.SetValue("MineParam", "tbChannelName24", tbChannelName24);
		dbBase.SetValue("MineParam", "tbChannelName25", tbChannelName25);
		dbBase.SetValue("MineParam", "tbChannelName26", tbChannelName26);
		dbBase.SetValue("MineParam", "tbChannelName27", tbChannelName27);
		dbBase.SetValue("MineParam", "tbChannelName28", tbChannelName28);
		dbBase.SetValue("MineParam", "tbChannelName29", tbChannelName29);
		dbBase.SetValue("MineParam", "tbChannelName30", tbChannelName30);
		dbBase.SetValue("MineParam", "tbChannelName31", tbChannelName31);
		dbBase.SetValue("MineParam", "tbChannelName32", tbChannelName32);
		dbBase.SetValue("MineParam", "tbChannelName33", tbChannelName33);
		dbBase.SetValue("MineParam", "tbChannelName34", tbChannelName34);
		dbBase.SetValue("MineParam", "tbChannelName35", tbChannelName35);
		dbBase.SetValue("MineParam", "tbChannelName36", tbChannelName36);
		dbBase.SetValue("MineParam", "tbChannelName37", tbChannelName37);
		dbBase.SetValue("MineParam", "tbChannelName38", tbChannelName38);
		dbBase.SetValue("MineParam", "tbChannelName39", tbChannelName39);
		dbBase.SetValue("MineParam", "tbChannelName40", tbChannelName40);
		dbBase.SetValue("MineParam", "tbChannelName41", tbChannelName41);
		dbBase.SetValue("MineParam", "tbChannelName42", tbChannelName42);
		dbBase.SetValue("MineParam", "tbChannelName43", tbChannelName43);
		dbBase.SetValue("MineParam", "tbChannelName44", tbChannelName44);
		dbBase.SetValue("MineParam", "tbChannelName45", tbChannelName45);
		dbBase.SetValue("MineParam", "tbChannelName46", tbChannelName46);
		dbBase.SetValue("MineParam", "tbChannelName47", tbChannelName47);
		dbBase.SetValue("MineParam", "tbChannelName48", tbChannelName48);
		dbBase.SetValue("MineParam", "tbChannelName49", tbChannelName49);
		dbBase.SetValue("MineParam", "tbChannelName50", tbChannelName50);
		dbBase.SetValue("MineParam", "tbChannelName51", tbChannelName51);
		dbBase.SetValue("MineParam", "tbChannelName52", tbChannelName52);
		dbBase.SetValue("MineParam", "tbChannelName53", tbChannelName53);
		dbBase.SetValue("MineParam", "tbChannelName54", tbChannelName54);
		dbBase.SetValue("MineParam", "tbChannelName55", tbChannelName55);
		dbBase.SetValue("MineParam", "tbChannelName56", tbChannelName56);
		dbBase.SetValue("MineParam", "tbChannelName57", tbChannelName57);
		dbBase.SetValue("MineParam", "tbChannelName58", tbChannelName58);
		dbBase.SetValue("MineParam", "tbChannelName59", tbChannelName59);
		dbBase.SetValue("MineParam", "tbChannelName60", tbChannelName60);
		dbBase.SetValue("MineParam", "strCurrentSamplingSite", strCurrentSamplingSite);
		dbBase.SetValue("MineParam", "dataTimeStart", dataTimeStart.ToString("yyyy/MM/dd") + " 00:00:01");
		dbBase.SetValue("MineParam", "dataTimeEnd", dataTimeEnd.ToString("yyyy/MM/dd") + " 23:59:59");
		dbBase.Save();
	}
}
