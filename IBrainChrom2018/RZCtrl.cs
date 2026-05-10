using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using IBrainChrom2018.Unit;

namespace IBrainChrom2018;

public class RZCtrl : UserControl
{
	public static RZCtrl selfCtrl;

	private FormMainParam frmParam = FormMainParam.Create();

	private ChromDeviceListMgr cdlMgr = ChromDeviceListMgr.Create();

	public SerialPortBase serialPoartBase = new SerialPortBase();

	private bool m_bLoading = true;

	public bool analysisReady1 = false;

	public bool analysisReady2 = false;

	public bool analysisReady3 = false;

	public string channel1File = null;

	public string channel2File = null;

	public string channel3File = null;

	public bool channel1Ready = false;

	public bool channel2Ready = false;

	public bool channel3Ready = false;

	private IContainer components = null;

	private GroupBox groupBox9;

	private Label labLJYL;

	private Label labLJWD;

	private Label labXDMD;

	private Label labMD;

	private Label labLHB;

	private Label labHHB;

	private Label labLRZ;

	private Label labHRZ;

	private Label label93;

	private Label label92;

	private Label label91;

	private Label label90;

	private Label label89;

	private Label label88;

	private Label label87;

	private Label label86;

	private Label label85;

	private CheckBox chbCombinDector;

	private GroupBox gbBenXW;

	private Button button2;

	private Button button1;

	public Label lbBTEX1T;

	public Label lbBTEX3T;

	public Label lbBTEX2T;

	public Label lbBTEX1;

	public Label lbBTEX3;

	public Label lbBTEX9;

	public Label lbBTEX2;

	public Label lbBTEX9T;

	public Label lbBTEX4T;

	public Label lbBTEX8;

	public Label lbBTEX6T;

	public Label lbBTEX8T;

	public Label lbBTEX5T;

	public Label lbBTEX4;

	public Label lbBTEX;

	public Label lbBTEX6;

	public Label lbBTEX5;

	public Label lbBTEXt;

	public Label lbBTEX7T;

	public Label lbBTEX7;

	private Button btnExport;

	private Label labQ;

	private Label label2;

	private Label label15;

	private Label label14;

	public TextBox tbPowerOnDelay;

	private Label labRatio;

	private Label label3;

	public RZCtrl()
	{
		selfCtrl = this;
		InitializeComponent();
		chbCombinDector.Checked = frmParam.bTwoDector;
		initForm();
		m_bLoading = false;
	}

	public void initForm()
	{
		LoadLanguage();
		AreaPlotParamMgr areaPlotParamMgr = AreaPlotParamMgr.Create();
		AreaPlotParam areaPlotParam = null;
		areaPlotParam = areaPlotParamMgr.GetAreaPlotParam(20);
		Label label = lbBTEXt;
		string text = (lbBTEX9T.Text = areaPlotParam.PeakName);
		label.Text = text;
		areaPlotParam = areaPlotParamMgr.GetAreaPlotParam(19);
		Label label2 = lbBTEX9T;
		text = (lbBTEX9T.Text = areaPlotParam.PeakName);
		label2.Text = text;
		areaPlotParam = areaPlotParamMgr.GetAreaPlotParam(18);
		Label label3 = lbBTEX8T;
		text = (lbBTEX8T.Text = areaPlotParam.PeakName);
		label3.Text = text;
		areaPlotParam = areaPlotParamMgr.GetAreaPlotParam(17);
		Label label4 = lbBTEX7T;
		text = (lbBTEX7T.Text = areaPlotParam.PeakName);
		label4.Text = text;
		areaPlotParam = areaPlotParamMgr.GetAreaPlotParam(16);
		Label label5 = lbBTEX6T;
		text = (lbBTEX6T.Text = areaPlotParam.PeakName);
		label5.Text = text;
		areaPlotParam = areaPlotParamMgr.GetAreaPlotParam(15);
		Label label6 = lbBTEX5T;
		text = (lbBTEX5T.Text = areaPlotParam.PeakName);
		label6.Text = text;
		areaPlotParam = areaPlotParamMgr.GetAreaPlotParam(14);
		Label label7 = lbBTEX4T;
		text = (lbBTEX4T.Text = areaPlotParam.PeakName);
		label7.Text = text;
		areaPlotParam = areaPlotParamMgr.GetAreaPlotParam(13);
		Label label8 = lbBTEX3T;
		text = (lbBTEX3T.Text = areaPlotParam.PeakName);
		label8.Text = text;
		areaPlotParam = areaPlotParamMgr.GetAreaPlotParam(12);
		Label label9 = lbBTEX2T;
		text = (lbBTEX2T.Text = areaPlotParam.PeakName);
		label9.Text = text;
		areaPlotParam = areaPlotParamMgr.GetAreaPlotParam(11);
		Label label10 = lbBTEX1T;
		text = (lbBTEX1T.Text = areaPlotParam.PeakName);
		label10.Text = text;
		tbPowerOnDelay.Text = frmParam.fPowerOnDealy.ToString("F" + Class49.int_8);
		try
		{
			serialPoartBase.openPort();
		}
		catch
		{
		}
	}

	private void LoadLanguage()
	{
		chbCombinDector.Text = Lang.PS("合并计算", "Merge");
		gbBenXW.Text = Lang.PS("含量", "Content");
		label85.Text = Lang.PS("高热值:", "High calorific:");
		label86.Text = Lang.PS("低热值:", "Low calorific:");
		label87.Text = Lang.PS("高热值华白数:", "High hua bai no.:");
		label88.Text = Lang.PS("低热值华白数:", "Low hua bai no.:");
		label89.Text = Lang.PS("燃烧势:", "Content");
		label90.Text = Lang.PS("密度:", "Density");
		label91.Text = Lang.PS("相对密度:", "Relative density");
		label92.Text = Lang.PS("临界温度:", "Critical TEMP");
		label93.Text = Lang.PS("临界压力:", "Critical pressure");
	}

	public void disposePeaks(int selectedIndex, string fileName, string strID, string strSampleIndex, Chromatogram chromatogram)
	{
		int count = cdlMgr.formMain.tabChannel.TabPages.Count;
		if (frmParam.bTwoDector)
		{
			if (selectedIndex == 0)
			{
				channel1Ready = true;
				channel1File = fileName;
				analysisReady1 = false;
			}
			if (selectedIndex == 1)
			{
				channel2Ready = true;
				channel2File = fileName;
				analysisReady2 = false;
			}
			if (selectedIndex == 2)
			{
				channel3Ready = true;
				channel3File = fileName;
				analysisReady3 = false;
			}
			switch (count)
			{
			case 3:
				if (channel1Ready && channel2Ready && channel3Ready)
				{
					channel1Ready = false;
					channel2Ready = false;
					channel3Ready = false;
					string text2 = channel1File.Substring(0, channel1File.LastIndexOf("."));
					text2 += "合并.sda";
					spectraCombined(channel1File, channel2File, channel3File, text2);
					calorificValue(0, text2, strID, "1");
				}
				break;
			case 2:
				if (channel1Ready && channel2Ready)
				{
					channel1Ready = false;
					channel2Ready = false;
					string text = channel1File.Substring(0, channel1File.LastIndexOf("."));
					text += "合并.sda";
					spectraCombined(channel1File, channel2File, text);
					calorificValue(0, text, strID, "1");
				}
				break;
			}
		}
		else
		{
			calorificValue(0, fileName, strID, "1");
		}
	}

	public void calorificValue(int selectedIndex, string fileName, string strID, string strSampleIndex)
	{
		int num = 0;
		float num2 = 0f;
		float num3 = 0f;
		float num4 = 0f;
		float num5 = 0f;
		float num6 = 0f;
		float num7 = 0f;
		float num8 = 0f;
		float num9 = 0f;
		float num10 = 0f;
		float num11 = 0f;
		float num12 = 0f;
		float num13 = 0f;
		float num14 = 0f;
		float num15 = 216.89f;
		float num16 = 4.713f;
		float num17 = 0f;
		float num18 = 0f;
		float num19 = 0f;
		float num20 = 0f;
		float num21 = 0f;
		float num22 = 0f;
		float[] array = new float[1];
		ushort[] array2 = new ushort[2];
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
		cdlMgr.tcpServerMgr.mComModbus.WordVaue[52] = array2[0];
		cdlMgr.tcpServerMgr.mComModbus.WordVaue[53] = array2[1];
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
		labRatio.Text = "";
		if (!File.Exists(fileName))
		{
			return;
		}
		float[] array3 = new float[50];
		if (ChromForm.form == null)
		{
			ChromForm.form = new ChromForm();
		}
		ChromForm.form.OpenChrom(fileName, sampling: true, useCurrent: true);
		ChromForm.form.chromDataGrid.mstSetChromForm.bUseSet_Click(null, null);
		ChromForm.form.chromDataGrid.saveFile();
		Peak[] rltPeaks = ChromForm.form.CurChrom.RltPeaks;
		Peak[] array4 = new Peak[0];
		if (cdlMgr.CurrentChartParaOpera.mtdMgr.caliGnl == null)
		{
			cdlMgr.CurrentChartParaOpera.mtdMgr.caliGnl = new CaliGnl();
		}
		CaliGnl caliGnl = cdlMgr.CurrentChartParaOpera.mtdMgr.caliGnl;
		CaliGnl caliGnl2 = new CaliGnl();
		if (rltPeaks.Length == 0 || selectedIndex != 0)
		{
			return;
		}
		caliGnl2 = cdlMgr.ChartParaOperaList[0].mtdMgr.caliGnl;
		int num23 = 255;
		float num24 = 0f;
		float num25 = 0f;
		array = new float[1];
		Buffer.BlockCopy(array, 0, array2, 0, 4);
		cdlMgr.tcpServerMgr.mComModbus.WordVaue[14] = array2[0];
		cdlMgr.tcpServerMgr.mComModbus.WordVaue[15] = array2[1];
		cdlMgr.tcpServerMgr.mComModbus.WordVaue[16] = array2[0];
		cdlMgr.tcpServerMgr.mComModbus.WordVaue[17] = array2[1];
		LogMgr.Instance.Write2RunLog("FormMainVOC.disposeVOCPeaks  selectedIndex==0,  peak.Count():" + rltPeaks.Count() + " caliGnl.cmpds.Length:" + caliGnl.cmpds.Length);
		int num26 = 0;
		while (1 <= caliGnl2.cmpds.Length && num26 < caliGnl2.cmpds.Length)
		{
			num = 0;
			while (1 <= rltPeaks.Count() && num < rltPeaks.Count())
			{
				if (rltPeaks[num].pkRT >= caliGnl2.cmpds[num26].cmpdInfo.retainTime - caliGnl2.cmpds[num26].cmpdInfo.leftWindow && rltPeaks[num].pkRT <= caliGnl2.cmpds[num26].cmpdInfo.retainTime + caliGnl2.cmpds[num26].cmpdInfo.rightWindow && !(caliGnl2.cmpds[num26].cmpdInfo.name != rltPeaks[num].name))
				{
					if (caliGnl2.cmpds[num26].eFunc.curveFit == CurveFit.Free)
					{
						if (rltPeaks[num].amountPer < 0f)
						{
							rltPeaks[num].amountPer = 0f;
						}
						if (num26 < 50)
						{
							array3[num26] = rltPeaks[num].amountPer * 100f;
						}
					}
					else
					{
						if (rltPeaks[num].amount < 0f)
						{
							rltPeaks[num].amount = 0f;
						}
						if (num26 < 50)
						{
							array3[num26] = rltPeaks[num].amount;
						}
					}
					if (rltPeaks[num].name == "氢" || rltPeaks[num].name == "H2")
					{
						num5 += rltPeaks[num].amountPer * 108f * 100f;
					}
					else if (rltPeaks[num].name == "一氧化碳" || rltPeaks[num].name == "CO")
					{
						num5 += rltPeaks[num].amountPer * 127f * 100f;
						num22 = rltPeaks[num].amountPer;
					}
					else if (rltPeaks[num].name == "甲烷" || rltPeaks[num].name == "CH4")
					{
						num5 += rltPeaks[num].amountPer * 358.3f * 100f;
					}
					if (rltPeaks[num].name == "二氧化碳" || rltPeaks[num].name == "CO2")
					{
						num21 = rltPeaks[num].amountPer;
					}
					num3 += Program.getCharacteristic(rltPeaks[num].name, rltPeaks[num].amountPer, 1);
					num6 += Program.getCharacteristic(rltPeaks[num].name, rltPeaks[num].amountPer, 2);
					num14 += Program.getCharacteristic(rltPeaks[num].name, rltPeaks[num].amountPer, 3);
					num13 += Program.getCharacteristic(rltPeaks[num].name, rltPeaks[num].amountPer, 4);
					num4 += Program.getCharacteristic(rltPeaks[num].name, rltPeaks[num].amountPer, 5);
					num7 += Program.getCharacteristic(rltPeaks[num].name, rltPeaks[num].amountPer, 6);
					Class49.InsertIntoVoc(11 + num26, 0, rltPeaks[num].name, fileName.ToLower(), array3[num26]);
					break;
				}
				num++;
			}
			num26++;
		}
		double d = num14;
		float num27 = (float)Math.Sqrt(d);
		num8 = num3 / num27;
		num9 = num4 / num27;
		num10 = num6 / num27;
		num11 = num7 / num27;
		labQ.Text = num5.ToString("0.000") + "(KJ / m3)";
		labHRZ.Text = num3.ToString("0.000") + "(MJ / Nm3)";
		labLRZ.Text = num6.ToString("0.000") + "(MJ / Nm3)";
		labHHB.Text = num8.ToString("0.000") + "(MJ / Nm3)";
		labLHB.Text = num10.ToString("0.000") + "(MJ / Nm3)";
		labMD.Text = num13.ToString("0.000") + "(kg / m3)";
		labXDMD.Text = num14.ToString("0.000");
		labLJWD.Text = num15.ToString("0.000") + "(K)";
		labLJYL.Text = num16.ToString("0.000") + "(MPa)";
		labRatio.Text = num21 / (num21 + num22) * 100f + "%";
		array3[10] = num5;
		array3[11] = num6;
		array3[12] = num8;
		array3[13] = num10;
		array3[14] = num13;
		array3[15] = num14;
		array3[16] = num15;
		array3[17] = num16;
		for (int i = 0; i < 50; i++)
		{
			if (float.IsNaN(array3[i]))
			{
				array3[i] = 0f;
			}
		}
		lbBTEX.Text = array3[9].ToString("0.00");
		lbBTEX9.Text = array3[8].ToString("0.00");
		lbBTEX8.Text = array3[7].ToString("0.00");
		lbBTEX7.Text = array3[6].ToString("0.00");
		lbBTEX6.Text = array3[5].ToString("0.00");
		lbBTEX5.Text = array3[4].ToString("0.00");
		lbBTEX4.Text = array3[3].ToString("0.00");
		lbBTEX3.Text = array3[2].ToString("0.00");
		lbBTEX2.Text = array3[1].ToString("0.00");
		lbBTEX1.Text = array3[0].ToString("0.00");
		Class49.InsertIntoRZHistory(1, 1, "0", array3);
		HistoryCtrl.selfCtrl.updataGridView();
		array = new float[1] { num5 };
		Buffer.BlockCopy(array, 0, array2, 0, 4);
		cdlMgr.tcpServerMgr.mComModbus.WordVaue[14] = array2[0];
		cdlMgr.tcpServerMgr.mComModbus.WordVaue[15] = array2[1];
		array = new float[1] { num6 };
		Buffer.BlockCopy(array, 0, array2, 0, 4);
		cdlMgr.tcpServerMgr.mComModbus.WordVaue[16] = array2[0];
		cdlMgr.tcpServerMgr.mComModbus.WordVaue[17] = array2[1];
		array = new float[1] { num8 };
		Buffer.BlockCopy(array, 0, array2, 0, 4);
		cdlMgr.tcpServerMgr.mComModbus.WordVaue[18] = array2[0];
		cdlMgr.tcpServerMgr.mComModbus.WordVaue[19] = array2[1];
		array = new float[1] { num10 };
		Buffer.BlockCopy(array, 0, array2, 0, 4);
		cdlMgr.tcpServerMgr.mComModbus.WordVaue[20] = array2[0];
		cdlMgr.tcpServerMgr.mComModbus.WordVaue[21] = array2[1];
		array = new float[1] { num13 };
		Buffer.BlockCopy(array, 0, array2, 0, 4);
		cdlMgr.tcpServerMgr.mComModbus.WordVaue[22] = array2[0];
		cdlMgr.tcpServerMgr.mComModbus.WordVaue[23] = array2[1];
		array = new float[1] { num14 };
		Buffer.BlockCopy(array, 0, array2, 0, 4);
		cdlMgr.tcpServerMgr.mComModbus.WordVaue[24] = array2[0];
		cdlMgr.tcpServerMgr.mComModbus.WordVaue[25] = array2[1];
		array = new float[1] { array3[0] };
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
		array[0] = array3[9];
		Buffer.BlockCopy(array, 0, array2, 0, 4);
		cdlMgr.tcpServerMgr.mComModbus.WordVaue[52] = array2[0];
		cdlMgr.tcpServerMgr.mComModbus.WordVaue[53] = array2[1];
		ushort num28 = 0;
		float num29 = num3;
		if (num29 < frmParam.fmount41)
		{
			num29 = frmParam.fmount41;
		}
		if (num29 > frmParam.fmount201)
		{
			num29 = frmParam.fmount201;
		}
		num28 = (ushort)((num29 - frmParam.fmount41) / (frmParam.fmount201 - frmParam.fmount41) * 4095f);
		if (num28 > 4095)
		{
			num28 = 4095;
		}
		serialPoartBase.Data2[7] = (byte)(num28 >> 8);
		serialPoartBase.Data2[8] = (byte)num28;
		num29 = num6;
		if (num29 < frmParam.fmount42)
		{
			num29 = frmParam.fmount42;
		}
		if (num29 > frmParam.fmount202)
		{
			num29 = frmParam.fmount202;
		}
		num28 = (ushort)((num29 - frmParam.fmount42) / (frmParam.fmount202 - frmParam.fmount42) * 4095f);
		if (num28 > 4095)
		{
			num28 = 4095;
		}
		serialPoartBase.Data2[9] = (byte)(num28 >> 8);
		serialPoartBase.Data2[10] = (byte)num28;
		num29 = num8;
		if (num29 < frmParam.fmount43)
		{
			num29 = frmParam.fmount43;
		}
		if (num29 > frmParam.fmount203)
		{
			num29 = frmParam.fmount203;
		}
		num28 = (ushort)((num29 - frmParam.fmount43) / (frmParam.fmount203 - frmParam.fmount43) * 4095f);
		if (num28 > 4095)
		{
			num28 = 4095;
		}
		serialPoartBase.Data2[11] = (byte)(num28 >> 8);
		serialPoartBase.Data2[12] = (byte)num28;
		num29 = num10;
		if (num29 < frmParam.fmount44)
		{
			num29 = frmParam.fmount44;
		}
		if (num29 > frmParam.fmount204)
		{
			num29 = frmParam.fmount204;
		}
		num28 = (ushort)((num29 - frmParam.fmount44) / (frmParam.fmount204 - frmParam.fmount44) * 4095f);
		if (num28 > 4095)
		{
			num28 = 4095;
		}
		serialPoartBase.Data2[13] = (byte)(num28 >> 8);
		serialPoartBase.Data2[14] = (byte)num28;
		num29 = array3[0];
		if (num29 < frmParam.fmount45)
		{
			num29 = frmParam.fmount45;
		}
		if (num29 > frmParam.fmount205)
		{
			num29 = frmParam.fmount205;
		}
		num28 = (ushort)((num29 - frmParam.fmount45) / (frmParam.fmount205 - frmParam.fmount45) * 4095f);
		if (num28 > 4095)
		{
			num28 = 4095;
		}
		serialPoartBase.Data2[15] = (byte)(num28 >> 8);
		serialPoartBase.Data2[16] = (byte)num28;
		num29 = array3[1];
		if (num29 < frmParam.fmount46)
		{
			num29 = frmParam.fmount46;
		}
		if (num29 > frmParam.fmount206)
		{
			num29 = frmParam.fmount206;
		}
		num28 = (ushort)((num29 - frmParam.fmount46) / (frmParam.fmount206 - frmParam.fmount46) * 4095f);
		if (num28 > 4095)
		{
			num28 = 4095;
		}
		serialPoartBase.Data2[17] = (byte)(num28 >> 8);
		serialPoartBase.Data2[18] = (byte)num28;
		num29 = array3[2];
		if (num29 < frmParam.fmount47)
		{
			num29 = frmParam.fmount47;
		}
		if (num29 > frmParam.fmount207)
		{
			num29 = frmParam.fmount207;
		}
		num28 = (ushort)((num29 - frmParam.fmount47) / (frmParam.fmount207 - frmParam.fmount47) * 4095f);
		if (num28 > 4095)
		{
			num28 = 4095;
		}
		serialPoartBase.Data2[19] = (byte)(num28 >> 8);
		serialPoartBase.Data2[20] = (byte)num28;
		num29 = array3[3];
		if (num29 < frmParam.fmount48)
		{
			num29 = frmParam.fmount48;
		}
		if (num29 > frmParam.fmount208)
		{
			num29 = frmParam.fmount208;
		}
		num28 = (ushort)((num29 - frmParam.fmount48) / (frmParam.fmount208 - frmParam.fmount48) * 4095f);
		if (num28 > 4095)
		{
			num28 = 4095;
		}
		serialPoartBase.Data2[21] = (byte)(num28 >> 8);
		serialPoartBase.Data2[22] = (byte)num28;
		num29 = array3[4];
		if (num29 < frmParam.fmount49)
		{
			num29 = frmParam.fmount49;
		}
		if (num29 > frmParam.fmount209)
		{
			num29 = frmParam.fmount209;
		}
		num28 = (ushort)((num29 - frmParam.fmount49) / (frmParam.fmount209 - frmParam.fmount49) * 4095f);
		if (num28 > 4095)
		{
			num28 = 4095;
		}
		serialPoartBase.Data2[23] = (byte)(num28 >> 8);
		serialPoartBase.Data2[24] = (byte)num28;
		num29 = array3[5];
		if (num29 < frmParam.fmount410)
		{
			num29 = frmParam.fmount410;
		}
		if (num29 > frmParam.fmount2010)
		{
			num29 = frmParam.fmount2010;
		}
		num28 = (ushort)((array3[5] - frmParam.fmount410) / (frmParam.fmount2010 - frmParam.fmount410) * 4095f);
		if (num28 > 4095)
		{
			num28 = 4095;
		}
		serialPoartBase.Data2[25] = (byte)(num28 >> 8);
		serialPoartBase.Data2[26] = (byte)num28;
		num29 = array3[6];
		if (num29 < frmParam.fmount411)
		{
			num29 = frmParam.fmount411;
		}
		if (num29 > frmParam.fmount2011)
		{
			num29 = frmParam.fmount2011;
		}
		num28 = (ushort)((num29 - frmParam.fmount411) / (frmParam.fmount2011 - frmParam.fmount411) * 4095f);
		if (num28 > 4095)
		{
			num28 = 4095;
		}
		serialPoartBase.Data2[27] = (byte)(num28 >> 8);
		serialPoartBase.Data2[28] = (byte)num28;
	}

	private void spectraCombined(string file1, string file2, string file3)
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

	private void ChbCombinDector_CheckedChanged(object sender, EventArgs e)
	{
		frmParam.bTwoDector = chbCombinDector.Checked;
		frmParam.SaveParam();
	}

	private void LbBTEX1_Click(object sender, EventArgs e)
	{
		FormAreaPlot formAreaPlot = new FormAreaPlot(11);
		formAreaPlot.StartPosition = FormStartPosition.CenterScreen;
		formAreaPlot.Show();
		formAreaPlot.loadData();
	}

	private void LbBTEX2_Click(object sender, EventArgs e)
	{
		FormAreaPlot formAreaPlot = new FormAreaPlot(12);
		formAreaPlot.StartPosition = FormStartPosition.CenterScreen;
		formAreaPlot.Show();
		formAreaPlot.loadData();
	}

	private void LbBTEX3_Click(object sender, EventArgs e)
	{
		FormAreaPlot formAreaPlot = new FormAreaPlot(13);
		formAreaPlot.StartPosition = FormStartPosition.CenterScreen;
		formAreaPlot.Show();
		formAreaPlot.loadData();
	}

	private void LbBTEX4_Click(object sender, EventArgs e)
	{
		FormAreaPlot formAreaPlot = new FormAreaPlot(14);
		formAreaPlot.StartPosition = FormStartPosition.CenterScreen;
		formAreaPlot.Show();
		formAreaPlot.loadData();
	}

	private void LbBTEX5_Click(object sender, EventArgs e)
	{
		FormAreaPlot formAreaPlot = new FormAreaPlot(15);
		formAreaPlot.StartPosition = FormStartPosition.CenterScreen;
		formAreaPlot.Show();
		formAreaPlot.loadData();
	}

	private void LbBTEX6_Click(object sender, EventArgs e)
	{
		FormAreaPlot formAreaPlot = new FormAreaPlot(16);
		formAreaPlot.StartPosition = FormStartPosition.CenterScreen;
		formAreaPlot.Show();
		formAreaPlot.loadData();
	}

	private void LbBTEX7_Click(object sender, EventArgs e)
	{
		FormAreaPlot formAreaPlot = new FormAreaPlot(17);
		formAreaPlot.StartPosition = FormStartPosition.CenterScreen;
		formAreaPlot.Show();
		formAreaPlot.loadData();
	}

	private void LbBTEX8_Click(object sender, EventArgs e)
	{
		FormAreaPlot formAreaPlot = new FormAreaPlot(18);
		formAreaPlot.StartPosition = FormStartPosition.CenterScreen;
		formAreaPlot.Show();
		formAreaPlot.loadData();
	}

	private void LbBTEX9_Click(object sender, EventArgs e)
	{
		FormAreaPlot formAreaPlot = new FormAreaPlot(19);
		formAreaPlot.StartPosition = FormStartPosition.CenterScreen;
		formAreaPlot.Show();
		formAreaPlot.loadData();
	}

	private void LbBTEX_Click(object sender, EventArgs e)
	{
		FormAreaPlot formAreaPlot = new FormAreaPlot(20);
		formAreaPlot.StartPosition = FormStartPosition.CenterScreen;
		formAreaPlot.Show();
		formAreaPlot.loadData();
	}

	private void labHRZ_Click(object sender, EventArgs e)
	{
		FormAreaPlot formAreaPlot = new FormAreaPlot(1);
		formAreaPlot.StartPosition = FormStartPosition.CenterScreen;
		formAreaPlot.Show();
		formAreaPlot.loadData();
	}

	private void labLRZ_Click(object sender, EventArgs e)
	{
		FormAreaPlot formAreaPlot = new FormAreaPlot(2);
		formAreaPlot.StartPosition = FormStartPosition.CenterScreen;
		formAreaPlot.Show();
		formAreaPlot.loadData();
	}

	private void labHHB_Click(object sender, EventArgs e)
	{
		FormAreaPlot formAreaPlot = new FormAreaPlot(3);
		formAreaPlot.StartPosition = FormStartPosition.CenterScreen;
		formAreaPlot.Show();
		formAreaPlot.loadData();
	}

	private void labLHB_Click(object sender, EventArgs e)
	{
		FormAreaPlot formAreaPlot = new FormAreaPlot(4);
		formAreaPlot.StartPosition = FormStartPosition.CenterScreen;
		formAreaPlot.Show();
		formAreaPlot.loadData();
	}

	private void labMD_Click(object sender, EventArgs e)
	{
		FormAreaPlot formAreaPlot = new FormAreaPlot(5);
		formAreaPlot.StartPosition = FormStartPosition.CenterScreen;
		formAreaPlot.Show();
		formAreaPlot.loadData();
	}

	private void labXDMD_Click(object sender, EventArgs e)
	{
		FormAreaPlot formAreaPlot = new FormAreaPlot(6);
		formAreaPlot.StartPosition = FormStartPosition.CenterScreen;
		formAreaPlot.Show();
		formAreaPlot.loadData();
	}

	private void btnExport_Click(object sender, EventArgs e)
	{
		FormAreaPlot formAreaPlot = new FormAreaPlot(100);
		formAreaPlot.StartPosition = FormStartPosition.CenterScreen;
		formAreaPlot.Show();
		formAreaPlot.loadData(100);
	}

	private void tbPowerOnDelay_TextChanged(object sender, EventArgs e)
	{
		if (!m_bLoading)
		{
			float.TryParse(tbPowerOnDelay.Text, out frmParam.fPowerOnDealy);
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
		this.groupBox9 = new System.Windows.Forms.GroupBox();
		this.labQ = new System.Windows.Forms.Label();
		this.label2 = new System.Windows.Forms.Label();
		this.labLJYL = new System.Windows.Forms.Label();
		this.labLJWD = new System.Windows.Forms.Label();
		this.labXDMD = new System.Windows.Forms.Label();
		this.labMD = new System.Windows.Forms.Label();
		this.labLHB = new System.Windows.Forms.Label();
		this.labHHB = new System.Windows.Forms.Label();
		this.labLRZ = new System.Windows.Forms.Label();
		this.labHRZ = new System.Windows.Forms.Label();
		this.label93 = new System.Windows.Forms.Label();
		this.label92 = new System.Windows.Forms.Label();
		this.label91 = new System.Windows.Forms.Label();
		this.label90 = new System.Windows.Forms.Label();
		this.label89 = new System.Windows.Forms.Label();
		this.label88 = new System.Windows.Forms.Label();
		this.label87 = new System.Windows.Forms.Label();
		this.label86 = new System.Windows.Forms.Label();
		this.label85 = new System.Windows.Forms.Label();
		this.chbCombinDector = new System.Windows.Forms.CheckBox();
		this.gbBenXW = new System.Windows.Forms.GroupBox();
		this.button2 = new System.Windows.Forms.Button();
		this.button1 = new System.Windows.Forms.Button();
		this.lbBTEX1T = new System.Windows.Forms.Label();
		this.lbBTEX3T = new System.Windows.Forms.Label();
		this.lbBTEX2T = new System.Windows.Forms.Label();
		this.lbBTEX1 = new System.Windows.Forms.Label();
		this.lbBTEX3 = new System.Windows.Forms.Label();
		this.lbBTEX9 = new System.Windows.Forms.Label();
		this.lbBTEX2 = new System.Windows.Forms.Label();
		this.lbBTEX9T = new System.Windows.Forms.Label();
		this.lbBTEX4T = new System.Windows.Forms.Label();
		this.lbBTEX8 = new System.Windows.Forms.Label();
		this.lbBTEX6T = new System.Windows.Forms.Label();
		this.lbBTEX8T = new System.Windows.Forms.Label();
		this.lbBTEX5T = new System.Windows.Forms.Label();
		this.lbBTEX4 = new System.Windows.Forms.Label();
		this.lbBTEX = new System.Windows.Forms.Label();
		this.lbBTEX6 = new System.Windows.Forms.Label();
		this.lbBTEX5 = new System.Windows.Forms.Label();
		this.lbBTEXt = new System.Windows.Forms.Label();
		this.lbBTEX7T = new System.Windows.Forms.Label();
		this.lbBTEX7 = new System.Windows.Forms.Label();
		this.btnExport = new System.Windows.Forms.Button();
		this.label15 = new System.Windows.Forms.Label();
		this.label14 = new System.Windows.Forms.Label();
		this.tbPowerOnDelay = new System.Windows.Forms.TextBox();
		this.labRatio = new System.Windows.Forms.Label();
		this.label3 = new System.Windows.Forms.Label();
		this.groupBox9.SuspendLayout();
		this.gbBenXW.SuspendLayout();
		base.SuspendLayout();
		this.groupBox9.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.groupBox9.Controls.Add(this.labRatio);
		this.groupBox9.Controls.Add(this.label3);
		this.groupBox9.Controls.Add(this.labQ);
		this.groupBox9.Controls.Add(this.label2);
		this.groupBox9.Controls.Add(this.labLJYL);
		this.groupBox9.Controls.Add(this.labLJWD);
		this.groupBox9.Controls.Add(this.labXDMD);
		this.groupBox9.Controls.Add(this.labMD);
		this.groupBox9.Controls.Add(this.labLHB);
		this.groupBox9.Controls.Add(this.labHHB);
		this.groupBox9.Controls.Add(this.labLRZ);
		this.groupBox9.Controls.Add(this.labHRZ);
		this.groupBox9.Controls.Add(this.label93);
		this.groupBox9.Controls.Add(this.label92);
		this.groupBox9.Controls.Add(this.label91);
		this.groupBox9.Controls.Add(this.label90);
		this.groupBox9.Controls.Add(this.label89);
		this.groupBox9.Controls.Add(this.label88);
		this.groupBox9.Controls.Add(this.label87);
		this.groupBox9.Controls.Add(this.label86);
		this.groupBox9.Controls.Add(this.label85);
		this.groupBox9.Location = new System.Drawing.Point(535, 10);
		this.groupBox9.Name = "groupBox9";
		this.groupBox9.Size = new System.Drawing.Size(418, 150);
		this.groupBox9.TabIndex = 44;
		this.groupBox9.TabStop = false;
		this.groupBox9.Text = "273.15K、101325Pa";
		this.labQ.AutoSize = true;
		this.labQ.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
		this.labQ.Cursor = System.Windows.Forms.Cursors.Hand;
		this.labQ.Location = new System.Drawing.Point(89, 20);
		this.labQ.Name = "labQ";
		this.labQ.Size = new System.Drawing.Size(13, 14);
		this.labQ.TabIndex = 18;
		this.labQ.Text = "0";
		this.label2.AutoSize = true;
		this.label2.Location = new System.Drawing.Point(6, 21);
		this.label2.Name = "label2";
		this.label2.Size = new System.Drawing.Size(47, 12);
		this.label2.TabIndex = 17;
		this.label2.Text = "Q热值：";
		this.labLJYL.AutoSize = true;
		this.labLJYL.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
		this.labLJYL.Cursor = System.Windows.Forms.Cursors.Hand;
		this.labLJYL.Location = new System.Drawing.Point(303, 87);
		this.labLJYL.Name = "labLJYL";
		this.labLJYL.Size = new System.Drawing.Size(13, 14);
		this.labLJYL.TabIndex = 16;
		this.labLJYL.Text = "0";
		this.labLJWD.AutoSize = true;
		this.labLJWD.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
		this.labLJWD.Cursor = System.Windows.Forms.Cursors.Hand;
		this.labLJWD.Location = new System.Drawing.Point(303, 66);
		this.labLJWD.Name = "labLJWD";
		this.labLJWD.Size = new System.Drawing.Size(13, 14);
		this.labLJWD.TabIndex = 15;
		this.labLJWD.Text = "0";
		this.labXDMD.AutoSize = true;
		this.labXDMD.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
		this.labXDMD.Cursor = System.Windows.Forms.Cursors.Hand;
		this.labXDMD.Location = new System.Drawing.Point(303, 46);
		this.labXDMD.Name = "labXDMD";
		this.labXDMD.Size = new System.Drawing.Size(13, 14);
		this.labXDMD.TabIndex = 14;
		this.labXDMD.Text = "0";
		this.labXDMD.Click += new System.EventHandler(labXDMD_Click);
		this.labMD.AutoSize = true;
		this.labMD.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
		this.labMD.Cursor = System.Windows.Forms.Cursors.Hand;
		this.labMD.Location = new System.Drawing.Point(303, 25);
		this.labMD.Name = "labMD";
		this.labMD.Size = new System.Drawing.Size(13, 14);
		this.labMD.TabIndex = 13;
		this.labMD.Text = "0";
		this.labMD.Click += new System.EventHandler(labMD_Click);
		this.labLHB.AutoSize = true;
		this.labLHB.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
		this.labLHB.Cursor = System.Windows.Forms.Cursors.Hand;
		this.labLHB.Location = new System.Drawing.Point(89, 106);
		this.labLHB.Name = "labLHB";
		this.labLHB.Size = new System.Drawing.Size(13, 14);
		this.labLHB.TabIndex = 12;
		this.labLHB.Text = "0";
		this.labLHB.Click += new System.EventHandler(labLHB_Click);
		this.labHHB.AutoSize = true;
		this.labHHB.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
		this.labHHB.Cursor = System.Windows.Forms.Cursors.Hand;
		this.labHHB.Location = new System.Drawing.Point(89, 85);
		this.labHHB.Name = "labHHB";
		this.labHHB.Size = new System.Drawing.Size(13, 14);
		this.labHHB.TabIndex = 11;
		this.labHHB.Text = "0";
		this.labHHB.Click += new System.EventHandler(labHHB_Click);
		this.labLRZ.AutoSize = true;
		this.labLRZ.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
		this.labLRZ.Cursor = System.Windows.Forms.Cursors.Hand;
		this.labLRZ.Location = new System.Drawing.Point(89, 57);
		this.labLRZ.Name = "labLRZ";
		this.labLRZ.Size = new System.Drawing.Size(13, 14);
		this.labLRZ.TabIndex = 10;
		this.labLRZ.Text = "0";
		this.labLRZ.Click += new System.EventHandler(labLRZ_Click);
		this.labHRZ.AutoSize = true;
		this.labHRZ.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
		this.labHRZ.Cursor = System.Windows.Forms.Cursors.Hand;
		this.labHRZ.Location = new System.Drawing.Point(89, 41);
		this.labHRZ.Name = "labHRZ";
		this.labHRZ.Size = new System.Drawing.Size(13, 14);
		this.labHRZ.TabIndex = 9;
		this.labHRZ.Text = "0";
		this.labHRZ.Click += new System.EventHandler(labHRZ_Click);
		this.label93.AutoSize = true;
		this.label93.Location = new System.Drawing.Point(202, 89);
		this.label93.Name = "label93";
		this.label93.Size = new System.Drawing.Size(65, 12);
		this.label93.TabIndex = 8;
		this.label93.Text = "临界压力：";
		this.label92.AutoSize = true;
		this.label92.Location = new System.Drawing.Point(202, 68);
		this.label92.Name = "label92";
		this.label92.Size = new System.Drawing.Size(65, 12);
		this.label92.TabIndex = 7;
		this.label92.Text = "临界温度：";
		this.label91.AutoSize = true;
		this.label91.Location = new System.Drawing.Point(202, 48);
		this.label91.Name = "label91";
		this.label91.Size = new System.Drawing.Size(65, 12);
		this.label91.TabIndex = 6;
		this.label91.Text = "相对密度：";
		this.label90.AutoSize = true;
		this.label90.Location = new System.Drawing.Point(202, 27);
		this.label90.Name = "label90";
		this.label90.Size = new System.Drawing.Size(41, 12);
		this.label90.TabIndex = 5;
		this.label90.Text = "密度：";
		this.label89.AutoSize = true;
		this.label89.Location = new System.Drawing.Point(344, 117);
		this.label89.Name = "label89";
		this.label89.Size = new System.Drawing.Size(53, 12);
		this.label89.TabIndex = 4;
		this.label89.Text = "燃烧势：";
		this.label89.Visible = false;
		this.label88.AutoSize = true;
		this.label88.Location = new System.Drawing.Point(6, 108);
		this.label88.Name = "label88";
		this.label88.Size = new System.Drawing.Size(89, 12);
		this.label88.TabIndex = 3;
		this.label88.Text = "低热值华白数：";
		this.label87.AutoSize = true;
		this.label87.Location = new System.Drawing.Point(6, 87);
		this.label87.Name = "label87";
		this.label87.Size = new System.Drawing.Size(89, 12);
		this.label87.TabIndex = 2;
		this.label87.Text = "高热值华白数：";
		this.label86.AutoSize = true;
		this.label86.Location = new System.Drawing.Point(6, 62);
		this.label86.Name = "label86";
		this.label86.Size = new System.Drawing.Size(53, 12);
		this.label86.TabIndex = 1;
		this.label86.Text = "低热值：";
		this.label85.AutoSize = true;
		this.label85.Location = new System.Drawing.Point(6, 41);
		this.label85.Name = "label85";
		this.label85.Size = new System.Drawing.Size(53, 12);
		this.label85.TabIndex = 0;
		this.label85.Text = "高热值：";
		this.chbCombinDector.AutoSize = true;
		this.chbCombinDector.ForeColor = System.Drawing.Color.Black;
		this.chbCombinDector.Location = new System.Drawing.Point(11, 6);
		this.chbCombinDector.Name = "chbCombinDector";
		this.chbCombinDector.Size = new System.Drawing.Size(72, 16);
		this.chbCombinDector.TabIndex = 106;
		this.chbCombinDector.Text = "合并运算";
		this.chbCombinDector.UseVisualStyleBackColor = true;
		this.chbCombinDector.CheckedChanged += new System.EventHandler(ChbCombinDector_CheckedChanged);
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
		this.gbBenXW.Location = new System.Drawing.Point(3, 28);
		this.gbBenXW.Name = "gbBenXW";
		this.gbBenXW.Size = new System.Drawing.Size(526, 132);
		this.gbBenXW.TabIndex = 107;
		this.gbBenXW.TabStop = false;
		this.gbBenXW.Text = "含量";
		this.button2.Location = new System.Drawing.Point(604, 41);
		this.button2.Name = "button2";
		this.button2.Size = new System.Drawing.Size(75, 23);
		this.button2.TabIndex = 85;
		this.button2.Text = "button2";
		this.button2.UseVisualStyleBackColor = true;
		this.button2.Visible = false;
		this.button1.Location = new System.Drawing.Point(604, 16);
		this.button1.Name = "button1";
		this.button1.Size = new System.Drawing.Size(75, 23);
		this.button1.TabIndex = 84;
		this.button1.Text = "button1";
		this.button1.UseVisualStyleBackColor = true;
		this.button1.Visible = false;
		this.lbBTEX1T.AutoSize = true;
		this.lbBTEX1T.Location = new System.Drawing.Point(6, 16);
		this.lbBTEX1T.Name = "lbBTEX1T";
		this.lbBTEX1T.Size = new System.Drawing.Size(29, 12);
		this.lbBTEX1T.TabIndex = 62;
		this.lbBTEX1T.Text = "苯：";
		this.lbBTEX3T.AutoSize = true;
		this.lbBTEX3T.Location = new System.Drawing.Point(6, 52);
		this.lbBTEX3T.Name = "lbBTEX3T";
		this.lbBTEX3T.Size = new System.Drawing.Size(41, 12);
		this.lbBTEX3T.TabIndex = 63;
		this.lbBTEX3T.Text = "乙苯：";
		this.lbBTEX2T.AutoSize = true;
		this.lbBTEX2T.Location = new System.Drawing.Point(6, 34);
		this.lbBTEX2T.Name = "lbBTEX2T";
		this.lbBTEX2T.Size = new System.Drawing.Size(41, 12);
		this.lbBTEX2T.TabIndex = 64;
		this.lbBTEX2T.Text = "甲苯：";
		this.lbBTEX1.AutoSize = true;
		this.lbBTEX1.Cursor = System.Windows.Forms.Cursors.Hand;
		this.lbBTEX1.Location = new System.Drawing.Point(71, 16);
		this.lbBTEX1.Name = "lbBTEX1";
		this.lbBTEX1.Size = new System.Drawing.Size(11, 12);
		this.lbBTEX1.TabIndex = 65;
		this.lbBTEX1.Text = "0";
		this.lbBTEX1.Click += new System.EventHandler(LbBTEX1_Click);
		this.lbBTEX3.AutoSize = true;
		this.lbBTEX3.Cursor = System.Windows.Forms.Cursors.Hand;
		this.lbBTEX3.Location = new System.Drawing.Point(71, 52);
		this.lbBTEX3.Name = "lbBTEX3";
		this.lbBTEX3.Size = new System.Drawing.Size(11, 12);
		this.lbBTEX3.TabIndex = 66;
		this.lbBTEX3.Text = "0";
		this.lbBTEX3.Click += new System.EventHandler(LbBTEX3_Click);
		this.lbBTEX9.AutoSize = true;
		this.lbBTEX9.Cursor = System.Windows.Forms.Cursors.Hand;
		this.lbBTEX9.Location = new System.Drawing.Point(357, 52);
		this.lbBTEX9.Name = "lbBTEX9";
		this.lbBTEX9.Size = new System.Drawing.Size(11, 12);
		this.lbBTEX9.TabIndex = 83;
		this.lbBTEX9.Text = "0";
		this.lbBTEX9.Click += new System.EventHandler(LbBTEX9_Click);
		this.lbBTEX2.AutoSize = true;
		this.lbBTEX2.Cursor = System.Windows.Forms.Cursors.Hand;
		this.lbBTEX2.Location = new System.Drawing.Point(71, 34);
		this.lbBTEX2.Name = "lbBTEX2";
		this.lbBTEX2.Size = new System.Drawing.Size(11, 12);
		this.lbBTEX2.TabIndex = 67;
		this.lbBTEX2.Text = "0";
		this.lbBTEX2.Click += new System.EventHandler(LbBTEX2_Click);
		this.lbBTEX9T.AutoSize = true;
		this.lbBTEX9T.Location = new System.Drawing.Point(294, 52);
		this.lbBTEX9T.Name = "lbBTEX9T";
		this.lbBTEX9T.Size = new System.Drawing.Size(29, 12);
		this.lbBTEX9T.TabIndex = 82;
		this.lbBTEX9T.Text = "苯：";
		this.lbBTEX4T.AutoSize = true;
		this.lbBTEX4T.Location = new System.Drawing.Point(157, 16);
		this.lbBTEX4T.Name = "lbBTEX4T";
		this.lbBTEX4T.Size = new System.Drawing.Size(29, 12);
		this.lbBTEX4T.TabIndex = 68;
		this.lbBTEX4T.Text = "苯：";
		this.lbBTEX8.AutoSize = true;
		this.lbBTEX8.Cursor = System.Windows.Forms.Cursors.Hand;
		this.lbBTEX8.Location = new System.Drawing.Point(357, 34);
		this.lbBTEX8.Name = "lbBTEX8";
		this.lbBTEX8.Size = new System.Drawing.Size(11, 12);
		this.lbBTEX8.TabIndex = 81;
		this.lbBTEX8.Text = "0";
		this.lbBTEX8.Click += new System.EventHandler(LbBTEX8_Click);
		this.lbBTEX6T.AutoSize = true;
		this.lbBTEX6T.Location = new System.Drawing.Point(157, 52);
		this.lbBTEX6T.Name = "lbBTEX6T";
		this.lbBTEX6T.Size = new System.Drawing.Size(41, 12);
		this.lbBTEX6T.TabIndex = 69;
		this.lbBTEX6T.Text = "乙苯：";
		this.lbBTEX8T.AutoSize = true;
		this.lbBTEX8T.Location = new System.Drawing.Point(294, 34);
		this.lbBTEX8T.Name = "lbBTEX8T";
		this.lbBTEX8T.Size = new System.Drawing.Size(29, 12);
		this.lbBTEX8T.TabIndex = 80;
		this.lbBTEX8T.Text = "苯：";
		this.lbBTEX5T.AutoSize = true;
		this.lbBTEX5T.Location = new System.Drawing.Point(157, 34);
		this.lbBTEX5T.Name = "lbBTEX5T";
		this.lbBTEX5T.Size = new System.Drawing.Size(41, 12);
		this.lbBTEX5T.TabIndex = 70;
		this.lbBTEX5T.Text = "甲苯：";
		this.lbBTEX4.AutoSize = true;
		this.lbBTEX4.Cursor = System.Windows.Forms.Cursors.Hand;
		this.lbBTEX4.Location = new System.Drawing.Point(224, 16);
		this.lbBTEX4.Name = "lbBTEX4";
		this.lbBTEX4.Size = new System.Drawing.Size(11, 12);
		this.lbBTEX4.TabIndex = 71;
		this.lbBTEX4.Text = "0";
		this.lbBTEX4.Click += new System.EventHandler(LbBTEX4_Click);
		this.lbBTEX.AutoSize = true;
		this.lbBTEX.Cursor = System.Windows.Forms.Cursors.Hand;
		this.lbBTEX.Location = new System.Drawing.Point(479, 52);
		this.lbBTEX.Name = "lbBTEX";
		this.lbBTEX.Size = new System.Drawing.Size(11, 12);
		this.lbBTEX.TabIndex = 78;
		this.lbBTEX.Text = "0";
		this.lbBTEX.Click += new System.EventHandler(LbBTEX_Click);
		this.lbBTEX6.AutoSize = true;
		this.lbBTEX6.Cursor = System.Windows.Forms.Cursors.Hand;
		this.lbBTEX6.Location = new System.Drawing.Point(224, 52);
		this.lbBTEX6.Name = "lbBTEX6";
		this.lbBTEX6.Size = new System.Drawing.Size(11, 12);
		this.lbBTEX6.TabIndex = 72;
		this.lbBTEX6.Text = "0";
		this.lbBTEX6.Click += new System.EventHandler(LbBTEX6_Click);
		this.lbBTEX5.AutoSize = true;
		this.lbBTEX5.Cursor = System.Windows.Forms.Cursors.Hand;
		this.lbBTEX5.Location = new System.Drawing.Point(224, 34);
		this.lbBTEX5.Name = "lbBTEX5";
		this.lbBTEX5.Size = new System.Drawing.Size(11, 12);
		this.lbBTEX5.TabIndex = 73;
		this.lbBTEX5.Text = "0";
		this.lbBTEX5.Click += new System.EventHandler(LbBTEX5_Click);
		this.lbBTEXt.AutoSize = true;
		this.lbBTEXt.Location = new System.Drawing.Point(407, 52);
		this.lbBTEXt.Name = "lbBTEXt";
		this.lbBTEXt.Size = new System.Drawing.Size(53, 12);
		this.lbBTEXt.TabIndex = 76;
		this.lbBTEXt.Text = "苯系物：";
		this.lbBTEX7T.AutoSize = true;
		this.lbBTEX7T.Location = new System.Drawing.Point(294, 16);
		this.lbBTEX7T.Name = "lbBTEX7T";
		this.lbBTEX7T.Size = new System.Drawing.Size(29, 12);
		this.lbBTEX7T.TabIndex = 74;
		this.lbBTEX7T.Text = "苯：";
		this.lbBTEX7.AutoSize = true;
		this.lbBTEX7.Cursor = System.Windows.Forms.Cursors.Hand;
		this.lbBTEX7.Location = new System.Drawing.Point(358, 16);
		this.lbBTEX7.Name = "lbBTEX7";
		this.lbBTEX7.Size = new System.Drawing.Size(11, 12);
		this.lbBTEX7.TabIndex = 75;
		this.lbBTEX7.Text = "0";
		this.lbBTEX7.Click += new System.EventHandler(LbBTEX7_Click);
		this.btnExport.Location = new System.Drawing.Point(89, 3);
		this.btnExport.Name = "btnExport";
		this.btnExport.Size = new System.Drawing.Size(75, 23);
		this.btnExport.TabIndex = 108;
		this.btnExport.Text = "导出数据";
		this.btnExport.UseVisualStyleBackColor = true;
		this.btnExport.Click += new System.EventHandler(btnExport_Click);
		this.label15.AutoSize = true;
		this.label15.Font = new System.Drawing.Font("宋体", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		this.label15.Location = new System.Drawing.Point(170, 10);
		this.label15.Name = "label15";
		this.label15.Size = new System.Drawing.Size(120, 16);
		this.label15.TabIndex = 109;
		this.label15.Text = "启动后采集延时";
		this.label14.AutoSize = true;
		this.label14.Font = new System.Drawing.Font("宋体", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		this.label14.Location = new System.Drawing.Point(431, 10);
		this.label14.Name = "label14";
		this.label14.Size = new System.Drawing.Size(32, 16);
		this.label14.TabIndex = 111;
		this.label14.Text = "min";
		this.tbPowerOnDelay.BackColor = System.Drawing.SystemColors.HighlightText;
		this.tbPowerOnDelay.Font = new System.Drawing.Font("宋体", 10.5f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		this.tbPowerOnDelay.ForeColor = System.Drawing.Color.Black;
		this.tbPowerOnDelay.Location = new System.Drawing.Point(314, 7);
		this.tbPowerOnDelay.Name = "tbPowerOnDelay";
		this.tbPowerOnDelay.Size = new System.Drawing.Size(111, 23);
		this.tbPowerOnDelay.TabIndex = 110;
		this.tbPowerOnDelay.Text = "0.00";
		this.tbPowerOnDelay.TextChanged += new System.EventHandler(tbPowerOnDelay_TextChanged);
		this.labRatio.AutoSize = true;
		this.labRatio.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
		this.labRatio.Cursor = System.Windows.Forms.Cursors.Hand;
		this.labRatio.Location = new System.Drawing.Point(89, 127);
		this.labRatio.Name = "labRatio";
		this.labRatio.Size = new System.Drawing.Size(13, 14);
		this.labRatio.TabIndex = 20;
		this.labRatio.Text = "0";
		this.label3.AutoSize = true;
		this.label3.Location = new System.Drawing.Point(6, 129);
		this.label3.Name = "label3";
		this.label3.Size = new System.Drawing.Size(53, 12);
		this.label3.TabIndex = 19;
		this.label3.Text = "利用率：";
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.Controls.Add(this.label15);
		base.Controls.Add(this.label14);
		base.Controls.Add(this.tbPowerOnDelay);
		base.Controls.Add(this.btnExport);
		base.Controls.Add(this.gbBenXW);
		base.Controls.Add(this.chbCombinDector);
		base.Controls.Add(this.groupBox9);
		base.Name = "RZCtrl";
		base.Size = new System.Drawing.Size(967, 170);
		this.groupBox9.ResumeLayout(false);
		this.groupBox9.PerformLayout();
		this.gbBenXW.ResumeLayout(false);
		this.gbBenXW.PerformLayout();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
