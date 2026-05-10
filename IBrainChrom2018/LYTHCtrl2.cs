using System;
using System.ComponentModel;
using System.Data.SQLite;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using DevExpress.Utils;
using DevExpress.XtraCharts;
using DevExpress.XtraGauges.Base;
using DevExpress.XtraGauges.Core.Base;
using DevExpress.XtraGauges.Core.Drawing;
using DevExpress.XtraGauges.Core.Model;
using DevExpress.XtraGauges.Win;
using DevExpress.XtraGauges.Win.Base;
using DevExpress.XtraGauges.Win.Gauges.Circular;
using DevExpress.XtraGauges.Win.Gauges.Linear;
using HZH_Controls.Controls;
using IBrainChrom2018.Unit;

namespace IBrainChrom2018
{
	public class LYTHCtrl2 : UserControl
	{
		public static LYTHCtrl2 selfCtrl;

		private ChromDeviceListMgr cdlMgr = ChromDeviceListMgr.Create();

		private LYTHCPara lythcParamMgr = LYTHCPara.Create();

		private FormMainParam frmParam = FormMainParam.Create();

		private AreaPlotParamMgr plotParamMgr = AreaPlotParamMgr.Create();

		private AreaPlotParam plotParam = null;

		public MyModbus mComModbusMaster = new MyModbus();

		public float gCh4 = 0f;

		public float gTHC = 0f;

		public long CountAnalyse = 0L;

		public float fTempEnvir;

		public float fHuEnvir;

		public float fPressEnvir;

		public float fJingDuEnvir;

		public float fWeiDuEnvir;

		public float[] fthcAmount = new float[200];

		public float[] fch4Amount = new float[200];

		public float[] fnmhcAmount = new float[200];

		public float[,] fBtexAmount = new float[9, 200];

		public ulong cntCollect = 0uL;

		public ulong cntIntivalTIme = 0uL;

		public int collectMode = 0;

		public float collectTime = 0f;

		public float intervalTime = 0f;

		public ulong cntTime1 = 0uL;

		public string strJidian = "手动进样";

		public bool bStateAnalyze = false;

		public bool[] benSG = new bool[60];

		public float[] zufenAmount = new float[15];

		public string[] zufenName = new string[15];

		public int[] countZufen = new int[3];

		public ushort stateChannel = 1;

		public ulong cntCycle = 0uL;

		public int cntCycles = 0;

		public bool bAnalyse = false;

		public int collectTimes;

		public bool bCalibra = false;

		private SeriesPoint seriesPointTHC = new SeriesPoint("总烃", default(double));

		private SeriesPoint seriesPointCH4 = new SeriesPoint("甲烷", default(double));

		private SeriesPoint seriesPointNMHC = new SeriesPoint("非甲烷总烃", default(double));

		public string strStartTime = "";

		public string strStopTime = "";

		private string strDB = "";

		private byte[] CCITT_CRC8_DATA1 = new byte[320]
		{
			0, 94, 188, 226, 97, 63, 221, 131, 194, 156,
			126, 32, 163, 253, 31, 65, 157, 195, 33, 127,
			252, 162, 64, 30, 95, 1, 227, 189, 62, 96,
			130, 220, 35, 125, 159, 193, 66, 28, 254, 160,
			225, 191, 93, 3, 128, 222, 60, 98, 190, 224,
			2, 92, 223, 129, 99, 61, 124, 34, 192, 158,
			29, 67, 161, 255, 70, 24, 250, 164, 39, 121,
			155, 197, 132, 218, 56, 102, 229, 187, 89, 7,
			219, 133, 103, 57, 186, 228, 6, 88, 25, 71,
			165, 251, 120, 38, 196, 154, 101, 59, 217, 135,
			4, 90, 184, 230, 167, 249, 27, 69, 198, 152,
			122, 36, 248, 166, 68, 26, 153, 199, 37, 123,
			58, 100, 134, 216, 91, 5, 231, 185, 140, 210,
			48, 110, 237, 179, 81, 15, 78, 16, 242, 172,
			47, 113, 147, 205, 17, 79, 173, 243, 112, 46,
			204, 146, 211, 141, 111, 49, 178, 236, 14, 80,
			175, 241, 19, 77, 206, 144, 114, 44, 109, 51,
			209, 143, 12, 82, 176, 238, 50, 108, 142, 208,
			83, 13, 239, 177, 240, 174, 76, 18, 145, 207,
			45, 115, 202, 148, 118, 40, 171, 245, 23, 73,
			8, 86, 180, 234, 105, 55, 213, 139, 87, 9,
			235, 181, 54, 104, 138, 212, 149, 203, 41, 119,
			244, 170, 72, 22, 233, 183, 85, 11, 136, 214,
			52, 106, 43, 117, 151, 201, 74, 20, 246, 168,
			116, 42, 200, 150, 21, 75, 169, 247, 182, 232,
			10, 84, 215, 137, 107, 53, 175, 241, 19, 77,
			206, 144, 114, 44, 109, 51, 209, 143, 12, 82,
			176, 238, 50, 108, 142, 208, 83, 13, 239, 177,
			240, 174, 76, 18, 145, 207, 45, 115, 202, 148,
			118, 40, 171, 245, 23, 73, 8, 86, 180, 234,
			105, 55, 213, 139, 87, 9, 235, 181, 54, 104,
			138, 212, 149, 203, 41, 119, 244, 170, 72, 22
		};

		private IContainer components = null;

		private System.Windows.Forms.Label label79;

		private System.Windows.Forms.Label label77;

		private GroupBox groupBox1;

		private GroupBox groupBox2;

		private System.Windows.Forms.Label label1;

		private System.Windows.Forms.Label label2;

		private System.Windows.Forms.Label label3;

		private GroupBox groupBox3;

		private System.Windows.Forms.Label label8;

		private System.Windows.Forms.Label label9;

		private System.Windows.Forms.Label label12;

		private GroupBox groupBox5;

		private GroupBox groupBox6;

		private System.Windows.Forms.Label label32;

		private System.Windows.Forms.Label label31;

		private System.Windows.Forms.Label label33;

		private System.Windows.Forms.Label label39;

		private GroupBox groupBox4;

		private System.Windows.Forms.Label label17;

		private System.Windows.Forms.Label label40;

		private System.Windows.Forms.Label label41;

		private System.Windows.Forms.Label label42;

		private System.Windows.Forms.Label label43;

		private System.Windows.Forms.Label label44;

		public TextBox tbTimeHuoHua;

		public System.Windows.Forms.Label label22;

		public System.Windows.Forms.Label label23;

		public TextBox tbTempHuoHua;

		public Button btnHuoHua;

		public System.Windows.Forms.Label label19;

		public System.Windows.Forms.Label label20;

		public System.Windows.Forms.Label labHumiEnvirCur;

		public System.Windows.Forms.Label labLatitudeCur;

		public System.Windows.Forms.Label labLongitudeCur;

		public System.Windows.Forms.Label labCH4Rlt;

		public System.Windows.Forms.Label labTHCRlt;

		public System.Windows.Forms.Label lbNMHCT;

		public System.Windows.Forms.Label labNMHCRlt;

		public System.Windows.Forms.Label labFaXiangSet;

		public System.Windows.Forms.Label labCHSSet;

		public System.Windows.Forms.Label labDecSet;

		public System.Windows.Forms.Label label7;

		public System.Windows.Forms.Label labTempEnvirCur;

		public System.Windows.Forms.Label labBaroEnvirCur;

		public System.Windows.Forms.Label label21;

		public System.Windows.Forms.Label label24;

		public System.Windows.Forms.Label labCYGXCur;

		public System.Windows.Forms.Label labFaXiangCur;

		public System.Windows.Forms.Label labCHSCur;

		public System.Windows.Forms.Label labDecCur;

		public System.Windows.Forms.Label labCYGXSet;

		public System.Windows.Forms.Label labSampleCur;

		public System.Windows.Forms.Label labHHCur;

		public System.Windows.Forms.Label labAirCur;

		public System.Windows.Forms.Label labZQCur;

		public System.Windows.Forms.Label labSampleSet;

		public System.Windows.Forms.Label labHHSet;

		public System.Windows.Forms.Label labAirSet;

		public System.Windows.Forms.Label labZQSet;

		public Button btnChuiSao;

		public TextBox tbTimeChuiSao;

		private ArcScaleBackgroundLayerComponent arcScaleBackgroundLayerComponent2;

		private ArcScaleComponent arcScaleComponent2;

		private ArcScaleNeedleComponent arcScaleNeedleComponent2;

		private ArcScaleSpindleCapComponent arcScaleSpindleCapComponent2;

		private GaugeControl gaugeControl1;

		private CircularGauge circularGauge1;

		private ArcScaleBackgroundLayerComponent arcScaleBackgroundLayerComponent1;

		private ArcScaleComponent ascZeroGasPress;

		private LabelComponent labelComponent2;

		private ArcScaleNeedleComponent arcScaleNeedleComponent1;

		private ArcScaleSpindleCapComponent arcScaleSpindleCapComponent1;

		private GaugeControl gaugeControl2;

		private CircularGauge circularGauge2;

		private ArcScaleBackgroundLayerComponent arcScaleBackgroundLayerComponent3;

		private ArcScaleComponent ascHHPress;

		private LabelComponent labelComponent1;

		private ArcScaleNeedleComponent arcScaleNeedleComponent3;

		private ArcScaleSpindleCapComponent arcScaleSpindleCapComponent3;

		private GaugeControl gaugeControl3;

		private CircularGauge circularGauge3;

		private ArcScaleBackgroundLayerComponent arcScaleBackgroundLayerComponent4;

		private ArcScaleComponent ascStandardGasPress;

		private LabelComponent labelComponent3;

		private ArcScaleNeedleComponent arcScaleNeedleComponent4;

		private ArcScaleSpindleCapComponent arcScaleSpindleCapComponent4;

		private LabelComponent labelComponent9;

		private LabelComponent labelComponent10;

		private LabelComponent labelComponent11;

		private LabelComponent labelComponent12;

		private LabelComponent labelComponent13;

		private LabelComponent labelComponent14;

		private LinearScaleMarkerComponent linearScaleMarkerComponent5;

		private LinearScaleComponent linearScaleComponent10;

		private LinearScaleMarkerComponent linearScaleMarkerComponent6;

		private LinearScaleComponent linearScaleComponent12;

		private LinearScaleMarkerComponent linearScaleMarkerComponent7;

		private LinearScaleComponent linearScaleComponent14;

		private LinearScaleRangeBarComponent linearScaleRangeBarComponent5;

		private LinearScaleComponent lSCTEnvir;

		private LinearScaleRangeBarComponent linearScaleRangeBarComponent6;

		private LinearScaleComponent lSCHUMEnvir;

		private LinearScaleRangeBarComponent linearScaleRangeBarComponent7;

		private LinearScaleComponent lSCPreEnvir;

		private System.Windows.Forms.Label label4;

		private System.Windows.Forms.Label label5;

		private Button bStartAnalyze;

		private Button btnEnvirPollu;

		private Button btnFixPollu;

		private Button btnTemp;

		private GroupBox groupBox8;

		public SplitContainer spChrom;

		public System.Windows.Forms.Label labAnyTimes;

		private System.Windows.Forms.Label label18;

		public System.Windows.Forms.Label label15;

		private System.Windows.Forms.Label label14;

		private System.Windows.Forms.Label label13;

		private System.Windows.Forms.Label label10;

		private System.Windows.Forms.Label label34;

		private System.Windows.Forms.Label label29;

		private System.Windows.Forms.Label label28;

		private System.Windows.Forms.Label label27;

		private System.Windows.Forms.Label label26;

		private System.Windows.Forms.Label label25;

		private GroupBox groupBox9;

		private System.Windows.Forms.Label label35;

		private System.Windows.Forms.Label label36;

		private TextBox tbIntervalTime;

		private System.Windows.Forms.Label label37;

		private TextBox tbCollectTime;

		private System.Windows.Forms.Label label38;

		private TextBox tbCollectTimes;

		private System.Windows.Forms.Label label45;

		private RadioButton rad60Min;

		private RadioButton radFive;

		private RadioButton radOne;

		private Button btnStartCalibra;

		private TextBox tbCollectJCXM;

		private TextBox tbCollectJYDW;

		private TextBox tbCollectSJDW;

		private TextBox tbCollectP;

		private TextBox tbCollectBH;

		private TextBox tbCollectSite;

		private System.Windows.Forms.Timer timer1;

		private System.Windows.Forms.Label label46;

		private System.Windows.Forms.Label label16;

		private System.Windows.Forms.Label label6;

		private System.Windows.Forms.Label label48;

		private System.Windows.Forms.Label label47;

		private System.Windows.Forms.Label label30;

		private System.Windows.Forms.Label label11;

		private Panel panel1;

		private Button button1;

		private Button btnNetConfig;

		private ComboBox cbKindMachine;

		private Button btnFireOnCheck;

		public TextBox tbFireOn2;

		public TextBox tbFireOn;

		private Button btnFireOnSet;

		private Button btnPoweroff;

		private Button btnAdvancedParameters;

		public GaugeControl graph2;

		public TextBox tbJingDu;

		public TextBox tbWeiDu;

		public LinearGauge linearGauge3;

		private Button btShowDesktop;

		private ComboBox cbRunMode;

		private System.Windows.Forms.Label label49;

		public PictureBox pictureBox1;

		public ImageList imageList1;

		private System.Windows.Forms.Label label50;

		public TextBox tbSatellite;

		public ChartControl chartTHC;

		public ChartControl chartCH4;

		public ChartControl chartNMHC;

		private System.Windows.Forms.Timer timer2;

		private UCBtnExt ucBtnHistory;

		public LYTHCtrl2()
		{
			selfCtrl = this;
			InitializeComponent();
			initForm();
			try
			{
			}
			catch
			{
			}
		}

		public void initForm()
		{
			plotParam = plotParamMgr.GetAreaPlotParam(1);
			foreach (Series item in chartTHC.Series)
			{
				SideBySideBarSeriesLabel sideBySideBarSeriesLabel = item.Label as SideBySideBarSeriesLabel;
				item.LabelsVisibility = DefaultBoolean.True;
				sideBySideBarSeriesLabel.Position = BarSeriesLabelPosition.TopInside;
				item.Points.Clear();
				item.Points.AddRange(seriesPointTHC);
				item.Name = plotParam.UintName;
			}
			foreach (Series item2 in chartCH4.Series)
			{
				SideBySideBarSeriesLabel sideBySideBarSeriesLabel2 = item2.Label as SideBySideBarSeriesLabel;
				item2.LabelsVisibility = DefaultBoolean.True;
				sideBySideBarSeriesLabel2.Position = BarSeriesLabelPosition.TopInside;
				item2.Points.Clear();
				item2.Points.AddRange(seriesPointCH4);
				item2.Name = plotParam.UintName;
			}
			foreach (Series item3 in chartNMHC.Series)
			{
				SideBySideBarSeriesLabel sideBySideBarSeriesLabel3 = item3.Label as SideBySideBarSeriesLabel;
				item3.LabelsVisibility = DefaultBoolean.True;
				sideBySideBarSeriesLabel3.Position = BarSeriesLabelPosition.TopInside;
				item3.Points.Clear();
				item3.Points.AddRange(seriesPointNMHC);
				item3.Name = plotParam.UintName;
			}
			ascZeroGasPress.Value = 0f;
			ascHHPress.Value = 0f;
			ascStandardGasPress.Value = 0f;
			lSCTEnvir.Value = 0f;
			lSCHUMEnvir.Value = 0f;
			lSCPreEnvir.Value = 0f;
			if (lythcParamMgr.collectMode == 1)
			{
				radOne.Checked = true;
			}
			else if (lythcParamMgr.collectMode == 2)
			{
				radFive.Checked = true;
			}
			else if (lythcParamMgr.collectMode == 3)
			{
				rad60Min.Checked = true;
			}
			tbCollectTimes.Text = lythcParamMgr.collectTimes.ToString();
			tbCollectTime.Text = lythcParamMgr.collectTime.ToString();
			tbIntervalTime.Text = lythcParamMgr.intervalTime.ToString();
			tbCollectSite.Text = lythcParamMgr.strCollectSite;
			tbCollectP.Text = lythcParamMgr.strCollectP;
			tbCollectBH.Text = lythcParamMgr.strCollectBH;
			tbCollectSJDW.Text = lythcParamMgr.strCollectSJDW;
			tbCollectJYDW.Text = lythcParamMgr.strCollectJYDW;
			tbCollectJCXM.Text = lythcParamMgr.strCollectJCXM;
			tbFireOn.Text = frmParam.fFireOn.ToString();
			tbFireOn2.Text = frmParam.fFireOn2.ToString();
			cbRunMode.SelectedIndex = lythcParamMgr.runMode;
			if (lythcParamMgr.iSample == 1)
			{
				btnFixPollu.BackColor = Color.LawnGreen;
			}
			else if (lythcParamMgr.iSample == 2)
			{
				btnEnvirPollu.BackColor = Color.LawnGreen;
			}
		}

		public void stateSwitch()
		{
			LYTHCPara lYTHCPara = LYTHCPara.Create();
			if (collectMode == 1)
			{
				if (bAnalyse)
				{
					strStopTime = DateTime.Now.ToString();
					bStartAnalyze.Text = "开始运行";
					bAnalyse = false;
					bStateAnalyze = false;
					lYTHCPara.strCollectTime = DateTime.Now.ToString();
					lYTHCPara.SaveParam();
					Class49.InsertIntoLYTHCRLT(0, 1, fthcAmount[0], fch4Amount[0], fnmhcAmount[0]);
					Class49.InsertIntoLYTHCRLT(0, 2, Class49.strDB, fthcAmount[0], fch4Amount[0], fnmhcAmount[0]);
					FormCollectResult formCollectResult = new FormCollectResult();
					formCollectResult.StartPosition = FormStartPosition.CenterScreen;
					formCollectResult.TopMost = true;
					formCollectResult.Show();
				}
			}
			else if (collectMode == 2)
			{
				if (!bAnalyse)
				{
					return;
				}
				if (CountAnalyse < collectTimes)
				{
					cntIntivalTIme++;
					if ((float)cntIntivalTIme >= intervalTime * 600f)
					{
						TcpServerSocket currentTcpServerSocket = cdlMgr.CurrentTcpServerSocket;
						if (currentTcpServerSocket != null)
						{
							currentTcpServerSocket.SendCmd(18);
							bAnalyse = false;
							cntIntivalTIme = 0uL;
						}
					}
					return;
				}
				strStopTime = DateTime.Now.ToString();
				bStateAnalyze = false;
				bAnalyse = false;
				bStartAnalyze.Text = "开始运行";
				cntIntivalTIme = 0uL;
				for (int i = 0; i < collectTimes; i++)
				{
					Class49.InsertIntoLYTHCRLT(0, 1, fthcAmount[i], fch4Amount[i], fnmhcAmount[i]);
					if (i == 0)
					{
						Class49.InsertIntoLYTHCRLT(0, 2, Class49.strDB, fthcAmount[i], fch4Amount[i], fnmhcAmount[i]);
					}
					else if (i == collectTimes - 1)
					{
						Class49.InsertIntoLYTHCRLT(0, 3, Class49.strDB, fthcAmount[i], fch4Amount[i], fnmhcAmount[i]);
					}
					else
					{
						Class49.InsertIntoLYTHCRLT(0, 1, Class49.strDB, fthcAmount[i], fch4Amount[i], fnmhcAmount[i]);
					}
				}
				lYTHCPara.strCollectTime = DateTime.Now.ToString();
				lYTHCPara.SaveParam();
				if (lYTHCPara.detectorMode == 1)
				{
					for (int j = 0; j < collectTimes; j++)
					{
						float[] array = new float[9];
						int num = 0;
						while (j < 8)
						{
							array[num] = fBtexAmount[num, j];
							num++;
						}
						Class49.InsertIntoLYBTENRLT(0, 1, array);
					}
				}
				FormCollectResult formCollectResult2 = new FormCollectResult();
				formCollectResult2.StartPosition = FormStartPosition.CenterScreen;
				formCollectResult2.TopMost = true;
				formCollectResult2.Show();
			}
			else
			{
				if (collectMode != 3)
				{
					return;
				}
				cntCollect++;
				if (!bAnalyse)
				{
					return;
				}
				if ((float)cntCollect < collectTime * 600f)
				{
					cntIntivalTIme++;
					if ((float)cntIntivalTIme > intervalTime * 600f)
					{
						TcpServerSocket currentTcpServerSocket2 = cdlMgr.CurrentTcpServerSocket;
						if (currentTcpServerSocket2 != null)
						{
							currentTcpServerSocket2.SendCmd(18);
							bAnalyse = false;
							cntIntivalTIme = 0uL;
						}
					}
					return;
				}
				strStopTime = DateTime.Now.ToString();
				timer1.Enabled = false;
				bStateAnalyze = false;
				bAnalyse = false;
				bStartAnalyze.Text = "开始运行";
				cntIntivalTIme = 0uL;
				cntCollect = 0uL;
				for (int k = 0; k < CountAnalyse; k++)
				{
					Class49.InsertIntoLYTHCRLT(0, 1, fthcAmount[k], fch4Amount[k], fnmhcAmount[k]);
					if (k == 0)
					{
						Class49.InsertIntoLYTHCRLT(0, 2, Class49.strDB, fthcAmount[k], fch4Amount[k], fnmhcAmount[k]);
					}
					else if (k == collectTimes - 1)
					{
						Class49.InsertIntoLYTHCRLT(0, 3, Class49.strDB, fthcAmount[k], fch4Amount[k], fnmhcAmount[k]);
					}
					else
					{
						Class49.InsertIntoLYTHCRLT(0, 1, Class49.strDB, fthcAmount[k], fch4Amount[k], fnmhcAmount[k]);
					}
				}
				if (lYTHCPara.detectorMode == 1)
				{
					for (int l = 0; l < collectTimes; l++)
					{
						float[] array2 = new float[9];
						int num2 = 0;
						while (l < 8)
						{
							array2[num2] = fBtexAmount[num2, l];
							num2++;
						}
						Class49.InsertIntoLYBTENRLT(0, 1, array2);
					}
				}
				lYTHCPara.strCollectTime = DateTime.Now.ToString();
				lYTHCPara.SaveParam();
				FormCollectResult formCollectResult3 = new FormCollectResult();
				formCollectResult3.StartPosition = FormStartPosition.CenterScreen;
				formCollectResult3.TopMost = true;
				formCollectResult3.Show();
			}
		}

		public void disposeDates(byte[] byte1)
		{
			byte[] array = new byte[320];
			for (int i = 25; i < 320; i++)
			{
				array[i] = (byte)(byte1[i] ^ CCITT_CRC8_DATA1[i]);
			}
			string text = Encoding.ASCII.GetString(array, 25, 200);
			string[] strGPS = text.Split(',');
			float fWeiDu = 0f;
			float fJingDu = 0f;
			if (strGPS.Length > 4 && strGPS[0] == "$GNGGA")
			{
				float.TryParse(strGPS[2], out fWeiDu);
				float.TryParse(strGPS[4], out fJingDu);
				Invoke((MethodInvoker)delegate
				{
					tbWeiDu.Text = (fWeiDu / 100f).ToString("0.000000") + "  " + strGPS[3];
					tbJingDu.Text = (fJingDu / 100f).ToString("0.000000") + "  " + strGPS[5];
					tbSatellite.Text = strGPS[7];
				});
			}
			float num = (lSCTEnvir.Value = (float)(array[305] * 256 + array[306]) / 100f);
			fTempEnvir = num;
			num = (lSCHUMEnvir.Value = (float)(array[307] * 256 + array[308]) / 100f);
			fHuEnvir = num;
			num = (lSCPreEnvir.Value = (float)((array[302] << 16) | (array[303] << 8) | array[304]) / 1000f);
			fPressEnvir = num;
			float num5 = (float)(array[309] * 256 + array[310]) / 100f - 12.9f;
			if (num5 < 0f)
			{
				num5 = 0f;
			}
			ascZeroGasPress.Value = num5;
			num5 = (float)(array[311] * 256 + array[312]) / 100f - 12.9f;
			if (num5 < 0f)
			{
				num5 = 0f;
			}
			ascHHPress.Value = num5;
			num5 = (float)(array[313] * 256 + array[314]) / 100f - 12.9f;
			if (num5 < 0f)
			{
				num5 = 0f;
			}
			ascStandardGasPress.Value = num5;
		}

		public void disposePeaks(int selectedIndex, string fileName, string strID, string strSampleIndex, Chromatogram chromatogram)
		{
			int num = 0;
			byte b = 0;
			byte b2 = 0;
			float num2 = 0f;
			if (cdlMgr.CurrentChartParaOpera.mtdMgr.caliGnl == null)
			{
				cdlMgr.CurrentChartParaOpera.mtdMgr.caliGnl = new CaliGnl();
			}
			CaliGnl caliGnl = cdlMgr.CurrentChartParaOpera.mtdMgr.caliGnl;
			Peak[] rltPeaks = chromatogram.RltPeaks;
			float[] array = new float[1];
			ushort[] array2 = new ushort[2];
			if (selectedIndex == 0)
			{
				CaliGnl caliGnl2 = cdlMgr.ChartParaOperaList[0].mtdMgr.caliGnl;
				int num3 = 255;
				int num4 = 255;
				float num5 = 0f;
				float num6 = 0f;
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
						if (!(rltPeaks[num].pkRT >= cdlMgr.ChartParaOperaList[0].mtdMgr.caliGnl.cmpds[b].cmpdInfo.retainTime - cdlMgr.ChartParaOperaList[0].mtdMgr.caliGnl.cmpds[b].cmpdInfo.leftWindow) || !(rltPeaks[num].pkRT <= cdlMgr.ChartParaOperaList[0].mtdMgr.caliGnl.cmpds[b].cmpdInfo.retainTime + cdlMgr.ChartParaOperaList[0].mtdMgr.caliGnl.cmpds[b].cmpdInfo.rightWindow) || rltPeaks[num].name != cdlMgr.ChartParaOperaList[0].mtdMgr.caliGnl.cmpds[b].cmpdInfo.name || !(rltPeaks[num].height >= num2))
						{
							continue;
						}
						if (cdlMgr.formMain.IsAutoCalibra == 1)
						{
							b2++;
							caliGnl2.cmpds[b].levels[0].responseA = rltPeaks[num].area;
							caliGnl2.CalculateFunc(false);
							caliGnl2.cmpds[b].levels[0].respFactor = rltPeaks[num].GasAmount / rltPeaks[num].area;
							cdlMgr.ChartParaOperaList[0].mtdMgr.SaveToFile();
							cdlMgr.formMain.MainmstSet.ReadFromMtdMgr();
							switch (b)
							{
							case 0:
								num5 = rltPeaks[num].area * caliGnl2.cmpds[b].levels[0].respFactor;
								labTHCRlt.Text = num5.ToString("0.00") + " " + lythcParamMgr.strUnit;
								break;
							case 1:
								num6 = (rltPeaks[num].amount = rltPeaks[num].area * caliGnl2.cmpds[b].levels[0].respFactor);
								labCH4Rlt.Text = rltPeaks[num].amount.ToString("0.00") + " " + lythcParamMgr.strUnit;
								break;
							}
							LogMgr.Instance.Write2RunLog("VocCtrl.disposeVOCPeaks  index:" + num + " peak.Count():" + rltPeaks.Count() + " amount2:" + num6 + "respFactor" + caliGnl2.cmpds[b].levels[0].respFactor + "GadAmount:" + rltPeaks[num].GasAmount + "area:" + rltPeaks[num].area);
							cdlMgr.ChartParaOperaList[0].mtdMgr.SaveToFile();
							cdlMgr.formMain.MainmstSet.UsePara();
							cdlMgr.formMain.MainmstSet.ReadFromMtdMgr();
							cdlMgr.formMain.MainmstSet.bUseSet_Click(null, null);
							continue;
						}
						switch (b)
						{
						case 0:
							num3 = num;
							num5 = (gTHC = rltPeaks[num].amount);
							array = new float[1] { num5 };
							Buffer.BlockCopy(array, 0, array2, 0, 4);
							cdlMgr.tcpServerMgr.mComModbus.WordVaue[14] = array2[0];
							cdlMgr.tcpServerMgr.mComModbus.WordVaue[15] = array2[1];
							Class49.InsertIntoVoc(1, 0, tbCollectSite.Text, fileName.ToLower(), num5);
							continue;
						case 1:
							break;
						default:
							continue;
						}
						num6 = (gCh4 = rltPeaks[num].amount);
						num4 = num;
						array = new float[1] { rltPeaks[num].amount };
						Buffer.BlockCopy(array, 0, array2, 0, 4);
						cdlMgr.tcpServerMgr.mComModbus.WordVaue[16] = array2[0];
						cdlMgr.tcpServerMgr.mComModbus.WordVaue[17] = array2[1];
						Class49.InsertIntoVoc(2, 0, tbCollectSite.Text, fileName.ToLower(), rltPeaks[num].amount);
						break;
					}
				}
				if (plotParam.UintName == "mg/m³")
				{
					num5 = num5 * 16f / 22.4f / 16f * 12f;
					num6 = num6 * 16f / 22.4f / 16f * 12f;
				}
				seriesPointTHC.Values[0] = num5;
				foreach (Series item in chartTHC.Series)
				{
					SideBySideBarSeriesLabel sideBySideBarSeriesLabel = item.Label as SideBySideBarSeriesLabel;
					item.LabelsVisibility = DefaultBoolean.True;
					sideBySideBarSeriesLabel.Position = BarSeriesLabelPosition.TopInside;
					item.Points.Clear();
					item.Points.AddRange(seriesPointTHC);
				}
				seriesPointCH4.Values[0] = num6;
				foreach (Series item2 in chartCH4.Series)
				{
					SideBySideBarSeriesLabel sideBySideBarSeriesLabel2 = item2.Label as SideBySideBarSeriesLabel;
					item2.LabelsVisibility = DefaultBoolean.True;
					sideBySideBarSeriesLabel2.Position = BarSeriesLabelPosition.TopInside;
					item2.Points.Clear();
					item2.Points.AddRange(seriesPointCH4);
				}
				if (num5 > num6)
				{
					seriesPointNMHC.Values[0] = num5 - num6;
					fnmhcAmount[CountAnalyse] = num5 - num6;
					foreach (Series item3 in chartNMHC.Series)
					{
						SideBySideBarSeriesLabel sideBySideBarSeriesLabel3 = item3.Label as SideBySideBarSeriesLabel;
						item3.LabelsVisibility = DefaultBoolean.True;
						sideBySideBarSeriesLabel3.Position = BarSeriesLabelPosition.TopInside;
						item3.Points.Clear();
						item3.Points.AddRange(seriesPointNMHC);
					}
					Class49.InsertIntoVoc(3, 0, "非甲烷总烃", fileName.ToLower(), num5 - num6);
				}
				else
				{
					seriesPointNMHC.Values[0] = 0.0;
					fnmhcAmount[CountAnalyse] = 0f;
					foreach (Series item4 in chartNMHC.Series)
					{
						SideBySideBarSeriesLabel sideBySideBarSeriesLabel4 = item4.Label as SideBySideBarSeriesLabel;
						item4.LabelsVisibility = DefaultBoolean.True;
						sideBySideBarSeriesLabel4.Position = BarSeriesLabelPosition.TopInside;
						item4.Points.Clear();
						item4.Points.AddRange(seriesPointNMHC);
					}
					Class49.InsertIntoVoc(3, 0, "非甲烷总烃", fileName.ToLower(), 0f);
				}
				fthcAmount[CountAnalyse] = num5;
				fch4Amount[CountAnalyse] = num6;
			}
			CountAnalyse++;
			labAnyTimes.Text = CountAnalyse.ToString();
			bAnalyse = true;
			strStopTime = DateTime.Now.ToString();
			cdlMgr.CurrentChartParaOpera.mtdMgr.caliGnl = caliGnl;
		}

		public void changeUnit(string strUnit)
		{
			float num;
			float num2;
			if (strUnit == "mg/m³")
			{
				num = gTHC * 16f / 22.4f / 16f * 12f;
				num2 = gCh4 * 16f / 22.4f / 16f * 12f;
			}
			else
			{
				num = gTHC;
				num2 = gCh4;
			}
			seriesPointTHC.Values[0] = num;
			foreach (Series item in selfCtrl.chartTHC.Series)
			{
				SideBySideBarSeriesLabel sideBySideBarSeriesLabel = item.Label as SideBySideBarSeriesLabel;
				item.Points.Clear();
				item.Points.AddRange(seriesPointTHC);
				item.Name = strUnit;
			}
			seriesPointCH4.Values[0] = num2;
			foreach (Series item2 in selfCtrl.chartCH4.Series)
			{
				SideBySideBarSeriesLabel sideBySideBarSeriesLabel2 = item2.Label as SideBySideBarSeriesLabel;
				item2.Points.Clear();
				item2.Points.AddRange(seriesPointCH4);
				item2.Name = strUnit;
			}
			if (num >= num2)
			{
				seriesPointNMHC.Values[0] = num - num2;
			}
			else
			{
				seriesPointNMHC.Values[0] = 0.0;
			}
			foreach (Series item3 in selfCtrl.chartNMHC.Series)
			{
				SideBySideBarSeriesLabel sideBySideBarSeriesLabel3 = item3.Label as SideBySideBarSeriesLabel;
				item3.Points.Clear();
				item3.Points.AddRange(seriesPointNMHC);
				item3.Name = strUnit;
			}
		}

		public void createSiteDB(string strDB)
		{
			string text = Application.StartupPath + "\\ngmpolHis.dll";
			SQLiteConnection.CreateFile("MyDatabase.sqlite");
			SQLiteConnection sQLiteConnection = new SQLiteConnection("Data Source=" + text);
			sQLiteConnection.Open();
			string text2 = " ";
			text2 = text2 + " CREATE TABLE [" + strDB + "] ( ";
			text2 += "[总烃] FLOAT NOT NULL ON CONFLICT FAIL,  ";
			text2 += "[甲烷] FLOAT NOT NULL ON CONFLICT FAIL,  ";
			text2 += "[非甲烷总烃] FLOAT NOT NULL ON CONFLICT FAIL,  ";
			text2 += "[时间] STRING);  ";
			SQLiteCommand sQLiteCommand = new SQLiteCommand(text2, sQLiteConnection);
			sQLiteCommand.ExecuteNonQuery();
		}

		private void BtnStart_Click(object sender, EventArgs e)
		{
			if (cbRunMode.SelectedIndex == 0)
			{
				LYTHCPara lYTHCPara = LYTHCPara.Create();
				collectMode = lYTHCPara.collectMode;
				collectTime = lYTHCPara.collectTime;
				collectTimes = lYTHCPara.collectTimes;
				intervalTime = lYTHCPara.intervalTime;
				stateChannel = 1;
				cntCycle = 0uL;
				if (bStartAnalyze.Text == Lang.PS("开始运行", "StartAll"))
				{
					strStartTime = DateTime.Now.ToString();
					bStateAnalyze = true;
					bStartAnalyze.Text = Lang.PS("结束运行", "StopAll");
					bStartAnalyze.BackColor = Color.LawnGreen;
					CountAnalyse = 0L;
					bAnalyse = false;
					cntCollect = 0uL;
					labAnyTimes.Text = CountAnalyse.ToString();
					TcpServerSocket currentTcpServerSocket = cdlMgr.CurrentTcpServerSocket;
					if (currentTcpServerSocket != null)
					{
						currentTcpServerSocket.SendCmd(18);
					}
					Class49.strDB = lYTHCPara.strCollectSJDW + "_" + lYTHCPara.strCollectSite + DateTime.Now.ToString("yyyy-MM-dd_HH_mm_ss");
					createSiteDB(Class49.strDB);
					Class49.DeleteDataTableLYThcRLTAll();
					if (lYTHCPara.detectorMode == 1)
					{
						Class49.DeleteDataTableLYThcRLTAll();
					}
					timer1.Enabled = true;
				}
				else
				{
					if (collectMode == 3)
					{
						cntCollect = (ulong)(collectTime * 600f) + 1;
					}
					else if (collectMode == 2)
					{
						collectTimes = (int)CountAnalyse + 1;
					}
					bStartAnalyze.Text = Lang.PS("开始运行", "StartAll");
					bStartAnalyze.BackColor = Color.Transparent;
				}
			}
			else if (cbRunMode.SelectedIndex == 1)
			{
				if (bStartAnalyze.Text == Lang.PS("开始运行", "StartAll"))
				{
					bStateAnalyze = true;
					bStartAnalyze.Text = Lang.PS("结束运行", "StopAll");
					bStartAnalyze.BackColor = Color.LawnGreen;
					cntCycle = 0uL;
					timer1.Enabled = true;
				}
				else
				{
					timer1.Enabled = false;
					bStateAnalyze = false;
					bStartAnalyze.Text = Lang.PS("开始运行", "StartAll");
					bStartAnalyze.BackColor = Color.Transparent;
					CountAnalyse = 0L;
					bAnalyse = false;
					cntCycle = 0uL;
					cdlMgr.currentTcpServerMgrSendEPCCmd(84, 0);
				}
			}
			else if (cbRunMode.SelectedIndex == 2)
			{
				if (bStartAnalyze.Text == Lang.PS("开始运行", "StartAll"))
				{
					bStateAnalyze = true;
					bStartAnalyze.Text = Lang.PS("结束运行", "StopAll");
					bStartAnalyze.BackColor = Color.LawnGreen;
					cntCycle = 0uL;
					timer1.Enabled = true;
				}
				else
				{
					timer1.Enabled = false;
					bStateAnalyze = false;
					bStartAnalyze.Text = Lang.PS("开始运行", "StartAll");
					bStartAnalyze.BackColor = Color.Transparent;
					CountAnalyse = 0L;
					bAnalyse = false;
				}
			}
		}

		private void BtnTemp_Click(object sender, EventArgs e)
		{
			if (Class49.user_0.ULevel == User.Level.访问员)
			{
				return;
			}
			TcpServerSocket currentTcpServerSocket = cdlMgr.CurrentTcpServerSocket;
			if (currentTcpServerSocket != null)
			{
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

		public void UpdateControlTempText(bool bCtrl)
		{
			if (bCtrl)
			{
				btnTemp.Text = Lang.PS("关闭控温", "Stop Temp");
			}
			else
			{
				btnTemp.Text = Lang.PS("开始控温", "Start Temp");
			}
		}

		public void UpdateControlAnalyzeText(bool bCtrl)
		{
		}

		public void AutoReloadMethod(int indexChanel, string methodPath)
		{
			cdlMgr.formMain.tabChannel.SelectedIndex = indexChanel;
			cdlMgr.formMain.MainmstSet.AutoMethodLoad(indexChanel, methodPath);
		}

		private void BtnStartCalibra_Click(object sender, EventArgs e)
		{
			if (radOne.Checked)
			{
				lythcParamMgr.collectMode = 1;
			}
			else if (radFive.Checked)
			{
				lythcParamMgr.collectMode = 2;
				if (tbCollectTimes.Text == "")
				{
					MessageBox.Show("请输入采样次数!");
					return;
				}
				if (tbIntervalTime.Text == "")
				{
					MessageBox.Show("请输入间隔时间!");
					return;
				}
			}
			else if (rad60Min.Checked)
			{
				lythcParamMgr.collectMode = 3;
				if (tbCollectTime.Text == "")
				{
					MessageBox.Show("请输入采样时长!");
					return;
				}
				if (tbIntervalTime.Text == "")
				{
					MessageBox.Show("请输入间隔时间!");
					return;
				}
			}
			lythcParamMgr.collectTimes = int.Parse(tbCollectTimes.Text.ToLower());
			lythcParamMgr.collectTime = float.Parse(tbCollectTime.Text.ToLower());
			lythcParamMgr.intervalTime = float.Parse(tbIntervalTime.Text.ToLower());
			lythcParamMgr.strCollectSite = tbCollectSite.Text;
			lythcParamMgr.strCollectP = tbCollectP.Text;
			lythcParamMgr.strCollectBH = tbCollectBH.Text;
			lythcParamMgr.strCollectSJDW = tbCollectSJDW.Text;
			lythcParamMgr.strCollectJYDW = tbCollectJYDW.Text;
			lythcParamMgr.strCollectJCXM = tbCollectJCXM.Text;
			lythcParamMgr.SaveParam();
			MessageBox.Show("保存成功！");
		}

		private void Timer1_Tick(object sender, EventArgs e)
		{
			cntTime1++;
			if (bStateAnalyze)
			{
				if (cbRunMode.SelectedIndex == 0)
				{
					stateSwitch();
				}
				else if (cbRunMode.SelectedIndex == 1)
				{
					chuiSaoMode();
				}
				else if (cbRunMode.SelectedIndex == 2)
				{
					cuiHua();
				}
			}
			else
			{
				cdlMgr.currentTcpServerMgrSendEPCCmd(84, 0);
				cntCycles = 0;
			}
		}

		public void chuiSaoMode()
		{
			cntCycle++;
			if (cntCycle == 2)
			{
				cdlMgr.currentTcpServerMgrSendEPCCmd(84, 29);
			}
			else if (cntCycle == 600)
			{
				cdlMgr.currentTcpServerMgrSendEPCCmd(84, 21);
			}
			else if (cntCycle == 1200)
			{
				cdlMgr.currentTcpServerMgrSendEPCCmd(84, 29);
			}
			else if (cntCycle == 1800)
			{
				cdlMgr.currentTcpServerMgrSendEPCCmd(84, 21);
			}
			else if (cntCycle == 2400)
			{
				cdlMgr.currentTcpServerMgrSendEPCCmd(84, 29);
			}
			else if (cntCycle == 3000)
			{
				cdlMgr.currentTcpServerMgrSendEPCCmd(84, 0);
				timer1.Enabled = false;
				bStateAnalyze = false;
				bStartAnalyze.Text = Lang.PS("开始运行", "StartAll");
				bStartAnalyze.BackColor = Color.Transparent;
				CountAnalyse = 0L;
				bAnalyse = false;
				cntCycle = 0uL;
			}
		}

		public void cuiHua()
		{
			cntCycle++;
			if (cntCycle == 1)
			{
				MethodInvoker method = delegate
				{
					InsDeviceCtrl.self.dgtempControl.Rows[2].Cells[2].Value = lythcParamMgr.fCatalytic.ToString("0.0");
					cdlMgr.currentTcpServerMgrSendCmd(8);
				};
				Invoke(method);
			}
			else if (cntCycle == 12000)
			{
				MethodInvoker method2 = delegate
				{
					InsDeviceCtrl.self.dgtempControl.Rows[1].Cells[2].Value = lythcParamMgr.fSample.ToString("0.0");
					cdlMgr.currentTcpServerMgrSendCmd(8);
				};
				Invoke(method2);
				timer1.Enabled = false;
				bStateAnalyze = false;
				bStartAnalyze.Text = Lang.PS("开始运行", "StartAll");
				bStartAnalyze.BackColor = Color.Transparent;
				CountAnalyse = 0L;
				bAnalyse = false;
				cntCycle = 0uL;
			}
		}

		private void chartTHC_Click(object sender, EventArgs e)
		{
			FormAreaPlot formAreaPlot = new FormAreaPlot(1);
			formAreaPlot.StartPosition = FormStartPosition.CenterScreen;
			formAreaPlot.Show();
			formAreaPlot.TopMost = true;
			formAreaPlot.loadData();
		}

		private void chartCH4_Click(object sender, EventArgs e)
		{
			FormAreaPlot formAreaPlot = new FormAreaPlot(2);
			formAreaPlot.StartPosition = FormStartPosition.CenterScreen;
			formAreaPlot.Show();
			formAreaPlot.TopMost = true;
			formAreaPlot.loadData();
		}

		private void chartNMHC_Click(object sender, EventArgs e)
		{
			FormAreaPlot formAreaPlot = new FormAreaPlot(3);
			formAreaPlot.StartPosition = FormStartPosition.CenterScreen;
			formAreaPlot.Show();
			formAreaPlot.TopMost = true;
			formAreaPlot.loadData();
		}

		private void btnNetConfig_Click(object sender, EventArgs e)
		{
			NetSetForm netSetForm = new NetSetForm();
			netSetForm.StartPosition = FormStartPosition.CenterScreen;
			netSetForm.TopMost = true;
			netSetForm.Show();
		}

		private void button1_Click(object sender, EventArgs e)
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

		private void btnFireOnCheck_Click(object sender, EventArgs e)
		{
			if (Class49.user_0.ULevel == User.Level.管理员)
			{
				cdlMgr.currentTcpServerMgrSendCmd(250);
			}
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
		}

		private float ToFloat(string str)
		{
			float result = 0f;
			float.TryParse(str, out result);
			return result;
		}

		private void btnPoweroff_Click(object sender, EventArgs e)
		{
			string text = "确定要关机吗？";
			if (MessageBox.Show(text, "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation) == DialogResult.OK)
			{
				Process.Start("shutdown.exe", "-s -f -t 1");
			}
		}

		private void btnAdvancedParameters_Click(object sender, EventArgs e)
		{
			FormLYThcAd formLYThcAd = new FormLYThcAd();
			formLYThcAd.TopMost = true;
			formLYThcAd.StartPosition = FormStartPosition.CenterScreen;
			formLYThcAd.Show();
		}

		private void btShowDesktop_Click(object sender, EventArgs e)
		{
			base.ParentForm.WindowState = FormWindowState.Minimized;
		}

		private void btnFixPollu_Click(object sender, EventArgs e)
		{
			string methodPath = "D:\\LYTHC1.mtd";
			cdlMgr.formMain.MainmstSet.AutoMethodLoad(0, methodPath);
			lythcParamMgr.iSample = 1;
			lythcParamMgr.SaveParam();
			MethodInvoker method = delegate
			{
				InsDeviceCtrl.self.dgtempControl.Rows[1].Cells[2].Value = lythcParamMgr.fSample.ToString("0.0");
				cdlMgr.currentTcpServerMgrSendCmd(8);
			};
			Invoke(method);
			btnFixPollu.BackColor = Color.LawnGreen;
			btnEnvirPollu.BackColor = Color.Transparent;
		}

		private void btnEnvirPollu_Click(object sender, EventArgs e)
		{
			string methodPath = "D:\\LYTHC2.mtd";
			cdlMgr.formMain.MainmstSet.AutoMethodLoad(0, methodPath);
			lythcParamMgr.iSample = 2;
			lythcParamMgr.SaveParam();
			MethodInvoker method = delegate
			{
				InsDeviceCtrl.self.dgtempControl.Rows[1].Cells[2].Value = lythcParamMgr.fSample2.ToString("0.0");
				cdlMgr.currentTcpServerMgrSendCmd(8);
			};
			Invoke(method);
			btnEnvirPollu.BackColor = Color.LawnGreen;
			btnFixPollu.BackColor = Color.Transparent;
		}

		private void cbRunMode_SelectedIndexChanged(object sender, EventArgs e)
		{
			lythcParamMgr.runMode = cbRunMode.SelectedIndex;
			lythcParamMgr.SaveParam();
		}

		public void SetFromPictureBoxImage(Class44 class44_0)
		{
			if (class44_0.bool_12)
			{
				pictureBox1.Image = imageList1.Images[20];
			}
			else
			{
				pictureBox1.Image = imageList1.Images[21];
			}
		}

		private void timer2_Tick(object sender, EventArgs e)
		{
		}

		private void ucBtnHistory_BtnClick(object sender, EventArgs e)
		{
			FormCollectHistory formCollectHistory = new FormCollectHistory();
			formCollectHistory.StartPosition = FormStartPosition.CenterScreen;
			formCollectHistory.TopMost = true;
			formCollectHistory.Show();
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
			DevExpress.XtraCharts.XYDiagram xYDiagram = new DevExpress.XtraCharts.XYDiagram();
			DevExpress.XtraCharts.Series series = new DevExpress.XtraCharts.Series();
			DevExpress.XtraCharts.SideBySideBarSeriesLabel sideBySideBarSeriesLabel = new DevExpress.XtraCharts.SideBySideBarSeriesLabel();
			DevExpress.XtraCharts.PointOptions pointOptions = new DevExpress.XtraCharts.PointOptions();
			DevExpress.XtraCharts.SeriesPoint seriesPoint = new DevExpress.XtraCharts.SeriesPoint("甲烷", new object[1] { 278.0 });
			DevExpress.XtraCharts.XYDiagram xYDiagram2 = new DevExpress.XtraCharts.XYDiagram();
			DevExpress.XtraCharts.Series series2 = new DevExpress.XtraCharts.Series();
			DevExpress.XtraCharts.SideBySideBarSeriesLabel sideBySideBarSeriesLabel2 = new DevExpress.XtraCharts.SideBySideBarSeriesLabel();
			DevExpress.XtraCharts.PointOptions pointOptions2 = new DevExpress.XtraCharts.PointOptions();
			DevExpress.XtraCharts.SeriesPoint seriesPoint2 = new DevExpress.XtraCharts.SeriesPoint("甲烷", new object[1] { 278.0 });
			DevExpress.XtraCharts.XYDiagram xYDiagram3 = new DevExpress.XtraCharts.XYDiagram();
			DevExpress.XtraCharts.Series series3 = new DevExpress.XtraCharts.Series();
			DevExpress.XtraCharts.SideBySideBarSeriesLabel sideBySideBarSeriesLabel3 = new DevExpress.XtraCharts.SideBySideBarSeriesLabel();
			DevExpress.XtraCharts.PointOptions pointOptions3 = new DevExpress.XtraCharts.PointOptions();
			DevExpress.XtraCharts.SeriesPoint seriesPoint3 = new DevExpress.XtraCharts.SeriesPoint("甲烷", new object[1] { 278.0 });
			DevExpress.XtraGauges.Core.Model.ScaleLabel scaleLabel = new DevExpress.XtraGauges.Core.Model.ScaleLabel();
			DevExpress.XtraGauges.Core.Model.LinearScaleRange linearScaleRange = new DevExpress.XtraGauges.Core.Model.LinearScaleRange();
			DevExpress.XtraGauges.Core.Model.LinearScaleRange linearScaleRange2 = new DevExpress.XtraGauges.Core.Model.LinearScaleRange();
			DevExpress.XtraGauges.Core.Model.LinearScaleRange linearScaleRange3 = new DevExpress.XtraGauges.Core.Model.LinearScaleRange();
			DevExpress.XtraGauges.Core.Model.LinearScaleRange linearScaleRange4 = new DevExpress.XtraGauges.Core.Model.LinearScaleRange();
			DevExpress.XtraGauges.Core.Model.LinearScaleRange linearScaleRange5 = new DevExpress.XtraGauges.Core.Model.LinearScaleRange();
			DevExpress.XtraGauges.Core.Model.LinearScaleRange linearScaleRange6 = new DevExpress.XtraGauges.Core.Model.LinearScaleRange();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(IBrainChrom2018.LYTHCtrl2));
			this.label79 = new System.Windows.Forms.Label();
			this.labCH4Rlt = new System.Windows.Forms.Label();
			this.labTHCRlt = new System.Windows.Forms.Label();
			this.label77 = new System.Windows.Forms.Label();
			this.lbNMHCT = new System.Windows.Forms.Label();
			this.labNMHCRlt = new System.Windows.Forms.Label();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.label33 = new System.Windows.Forms.Label();
			this.groupBox4 = new System.Windows.Forms.GroupBox();
			this.labSampleCur = new System.Windows.Forms.Label();
			this.label17 = new System.Windows.Forms.Label();
			this.labSampleSet = new System.Windows.Forms.Label();
			this.label40 = new System.Windows.Forms.Label();
			this.label41 = new System.Windows.Forms.Label();
			this.label42 = new System.Windows.Forms.Label();
			this.label43 = new System.Windows.Forms.Label();
			this.label44 = new System.Windows.Forms.Label();
			this.labHHSet = new System.Windows.Forms.Label();
			this.labAirSet = new System.Windows.Forms.Label();
			this.labZQSet = new System.Windows.Forms.Label();
			this.labCYGXSet = new System.Windows.Forms.Label();
			this.label32 = new System.Windows.Forms.Label();
			this.label31 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.labFaXiangSet = new System.Windows.Forms.Label();
			this.labCHSSet = new System.Windows.Forms.Label();
			this.labDecSet = new System.Windows.Forms.Label();
			this.groupBox5 = new System.Windows.Forms.GroupBox();
			this.tbTimeChuiSao = new System.Windows.Forms.TextBox();
			this.btnChuiSao = new System.Windows.Forms.Button();
			this.label21 = new System.Windows.Forms.Label();
			this.label24 = new System.Windows.Forms.Label();
			this.groupBox6 = new System.Windows.Forms.GroupBox();
			this.tbTimeHuoHua = new System.Windows.Forms.TextBox();
			this.label22 = new System.Windows.Forms.Label();
			this.label23 = new System.Windows.Forms.Label();
			this.tbTempHuoHua = new System.Windows.Forms.TextBox();
			this.btnHuoHua = new System.Windows.Forms.Button();
			this.label19 = new System.Windows.Forms.Label();
			this.label20 = new System.Windows.Forms.Label();
			this.groupBox3 = new System.Windows.Forms.GroupBox();
			this.labHumiEnvirCur = new System.Windows.Forms.Label();
			this.labLatitudeCur = new System.Windows.Forms.Label();
			this.labLongitudeCur = new System.Windows.Forms.Label();
			this.label39 = new System.Windows.Forms.Label();
			this.label7 = new System.Windows.Forms.Label();
			this.label8 = new System.Windows.Forms.Label();
			this.label9 = new System.Windows.Forms.Label();
			this.labTempEnvirCur = new System.Windows.Forms.Label();
			this.labBaroEnvirCur = new System.Windows.Forms.Label();
			this.label12 = new System.Windows.Forms.Label();
			this.labCYGXCur = new System.Windows.Forms.Label();
			this.labFaXiangCur = new System.Windows.Forms.Label();
			this.labCHSCur = new System.Windows.Forms.Label();
			this.labDecCur = new System.Windows.Forms.Label();
			this.labHHCur = new System.Windows.Forms.Label();
			this.labAirCur = new System.Windows.Forms.Label();
			this.labZQCur = new System.Windows.Forms.Label();
			this.arcScaleBackgroundLayerComponent2 = new DevExpress.XtraGauges.Win.Gauges.Circular.ArcScaleBackgroundLayerComponent();
			this.arcScaleComponent2 = new DevExpress.XtraGauges.Win.Gauges.Circular.ArcScaleComponent();
			this.arcScaleNeedleComponent2 = new DevExpress.XtraGauges.Win.Gauges.Circular.ArcScaleNeedleComponent();
			this.arcScaleSpindleCapComponent2 = new DevExpress.XtraGauges.Win.Gauges.Circular.ArcScaleSpindleCapComponent();
			this.gaugeControl1 = new DevExpress.XtraGauges.Win.GaugeControl();
			this.circularGauge1 = new DevExpress.XtraGauges.Win.Gauges.Circular.CircularGauge();
			this.arcScaleBackgroundLayerComponent1 = new DevExpress.XtraGauges.Win.Gauges.Circular.ArcScaleBackgroundLayerComponent();
			this.ascZeroGasPress = new DevExpress.XtraGauges.Win.Gauges.Circular.ArcScaleComponent();
			this.labelComponent2 = new DevExpress.XtraGauges.Win.Base.LabelComponent();
			this.arcScaleNeedleComponent1 = new DevExpress.XtraGauges.Win.Gauges.Circular.ArcScaleNeedleComponent();
			this.arcScaleSpindleCapComponent1 = new DevExpress.XtraGauges.Win.Gauges.Circular.ArcScaleSpindleCapComponent();
			this.gaugeControl2 = new DevExpress.XtraGauges.Win.GaugeControl();
			this.circularGauge2 = new DevExpress.XtraGauges.Win.Gauges.Circular.CircularGauge();
			this.arcScaleBackgroundLayerComponent3 = new DevExpress.XtraGauges.Win.Gauges.Circular.ArcScaleBackgroundLayerComponent();
			this.ascHHPress = new DevExpress.XtraGauges.Win.Gauges.Circular.ArcScaleComponent();
			this.labelComponent1 = new DevExpress.XtraGauges.Win.Base.LabelComponent();
			this.arcScaleNeedleComponent3 = new DevExpress.XtraGauges.Win.Gauges.Circular.ArcScaleNeedleComponent();
			this.arcScaleSpindleCapComponent3 = new DevExpress.XtraGauges.Win.Gauges.Circular.ArcScaleSpindleCapComponent();
			this.gaugeControl3 = new DevExpress.XtraGauges.Win.GaugeControl();
			this.circularGauge3 = new DevExpress.XtraGauges.Win.Gauges.Circular.CircularGauge();
			this.arcScaleBackgroundLayerComponent4 = new DevExpress.XtraGauges.Win.Gauges.Circular.ArcScaleBackgroundLayerComponent();
			this.ascStandardGasPress = new DevExpress.XtraGauges.Win.Gauges.Circular.ArcScaleComponent();
			this.labelComponent3 = new DevExpress.XtraGauges.Win.Base.LabelComponent();
			this.arcScaleNeedleComponent4 = new DevExpress.XtraGauges.Win.Gauges.Circular.ArcScaleNeedleComponent();
			this.arcScaleSpindleCapComponent4 = new DevExpress.XtraGauges.Win.Gauges.Circular.ArcScaleSpindleCapComponent();
			this.chartCH4 = new DevExpress.XtraCharts.ChartControl();
			this.chartTHC = new DevExpress.XtraCharts.ChartControl();
			this.chartNMHC = new DevExpress.XtraCharts.ChartControl();
			this.graph2 = new DevExpress.XtraGauges.Win.GaugeControl();
			this.linearGauge3 = new DevExpress.XtraGauges.Win.Gauges.Linear.LinearGauge();
			this.labelComponent9 = new DevExpress.XtraGauges.Win.Base.LabelComponent();
			this.labelComponent10 = new DevExpress.XtraGauges.Win.Base.LabelComponent();
			this.labelComponent11 = new DevExpress.XtraGauges.Win.Base.LabelComponent();
			this.labelComponent12 = new DevExpress.XtraGauges.Win.Base.LabelComponent();
			this.labelComponent13 = new DevExpress.XtraGauges.Win.Base.LabelComponent();
			this.labelComponent14 = new DevExpress.XtraGauges.Win.Base.LabelComponent();
			this.linearScaleMarkerComponent5 = new DevExpress.XtraGauges.Win.Gauges.Linear.LinearScaleMarkerComponent();
			this.linearScaleComponent10 = new DevExpress.XtraGauges.Win.Gauges.Linear.LinearScaleComponent();
			this.linearScaleMarkerComponent6 = new DevExpress.XtraGauges.Win.Gauges.Linear.LinearScaleMarkerComponent();
			this.linearScaleComponent12 = new DevExpress.XtraGauges.Win.Gauges.Linear.LinearScaleComponent();
			this.linearScaleMarkerComponent7 = new DevExpress.XtraGauges.Win.Gauges.Linear.LinearScaleMarkerComponent();
			this.linearScaleComponent14 = new DevExpress.XtraGauges.Win.Gauges.Linear.LinearScaleComponent();
			this.linearScaleRangeBarComponent5 = new DevExpress.XtraGauges.Win.Gauges.Linear.LinearScaleRangeBarComponent();
			this.lSCTEnvir = new DevExpress.XtraGauges.Win.Gauges.Linear.LinearScaleComponent();
			this.linearScaleRangeBarComponent6 = new DevExpress.XtraGauges.Win.Gauges.Linear.LinearScaleRangeBarComponent();
			this.lSCHUMEnvir = new DevExpress.XtraGauges.Win.Gauges.Linear.LinearScaleComponent();
			this.linearScaleRangeBarComponent7 = new DevExpress.XtraGauges.Win.Gauges.Linear.LinearScaleRangeBarComponent();
			this.lSCPreEnvir = new DevExpress.XtraGauges.Win.Gauges.Linear.LinearScaleComponent();
			this.label4 = new System.Windows.Forms.Label();
			this.tbJingDu = new System.Windows.Forms.TextBox();
			this.tbWeiDu = new System.Windows.Forms.TextBox();
			this.label5 = new System.Windows.Forms.Label();
			this.bStartAnalyze = new System.Windows.Forms.Button();
			this.btnEnvirPollu = new System.Windows.Forms.Button();
			this.btnFixPollu = new System.Windows.Forms.Button();
			this.btnTemp = new System.Windows.Forms.Button();
			this.spChrom = new System.Windows.Forms.SplitContainer();
			this.label49 = new System.Windows.Forms.Label();
			this.pictureBox1 = new System.Windows.Forms.PictureBox();
			this.label48 = new System.Windows.Forms.Label();
			this.label47 = new System.Windows.Forms.Label();
			this.label30 = new System.Windows.Forms.Label();
			this.label11 = new System.Windows.Forms.Label();
			this.label46 = new System.Windows.Forms.Label();
			this.label16 = new System.Windows.Forms.Label();
			this.label6 = new System.Windows.Forms.Label();
			this.labAnyTimes = new System.Windows.Forms.Label();
			this.label18 = new System.Windows.Forms.Label();
			this.label15 = new System.Windows.Forms.Label();
			this.label14 = new System.Windows.Forms.Label();
			this.label13 = new System.Windows.Forms.Label();
			this.label10 = new System.Windows.Forms.Label();
			this.groupBox8 = new System.Windows.Forms.GroupBox();
			this.tbCollectJCXM = new System.Windows.Forms.TextBox();
			this.tbCollectJYDW = new System.Windows.Forms.TextBox();
			this.btnStartCalibra = new System.Windows.Forms.Button();
			this.groupBox9 = new System.Windows.Forms.GroupBox();
			this.label35 = new System.Windows.Forms.Label();
			this.label36 = new System.Windows.Forms.Label();
			this.tbIntervalTime = new System.Windows.Forms.TextBox();
			this.label37 = new System.Windows.Forms.Label();
			this.tbCollectTime = new System.Windows.Forms.TextBox();
			this.label38 = new System.Windows.Forms.Label();
			this.tbCollectTimes = new System.Windows.Forms.TextBox();
			this.label45 = new System.Windows.Forms.Label();
			this.rad60Min = new System.Windows.Forms.RadioButton();
			this.radFive = new System.Windows.Forms.RadioButton();
			this.radOne = new System.Windows.Forms.RadioButton();
			this.tbFireOn2 = new System.Windows.Forms.TextBox();
			this.tbCollectSJDW = new System.Windows.Forms.TextBox();
			this.tbCollectP = new System.Windows.Forms.TextBox();
			this.tbCollectBH = new System.Windows.Forms.TextBox();
			this.label34 = new System.Windows.Forms.Label();
			this.tbCollectSite = new System.Windows.Forms.TextBox();
			this.label29 = new System.Windows.Forms.Label();
			this.label28 = new System.Windows.Forms.Label();
			this.label27 = new System.Windows.Forms.Label();
			this.label26 = new System.Windows.Forms.Label();
			this.label25 = new System.Windows.Forms.Label();
			this.timer1 = new System.Windows.Forms.Timer(this.components);
			this.panel1 = new System.Windows.Forms.Panel();
			this.ucBtnHistory = new HZH_Controls.Controls.UCBtnExt();
			this.btShowDesktop = new System.Windows.Forms.Button();
			this.btnPoweroff = new System.Windows.Forms.Button();
			this.btnNetConfig = new System.Windows.Forms.Button();
			this.cbKindMachine = new System.Windows.Forms.ComboBox();
			this.btnFireOnCheck = new System.Windows.Forms.Button();
			this.tbFireOn = new System.Windows.Forms.TextBox();
			this.btnFireOnSet = new System.Windows.Forms.Button();
			this.button1 = new System.Windows.Forms.Button();
			this.btnAdvancedParameters = new System.Windows.Forms.Button();
			this.cbRunMode = new System.Windows.Forms.ComboBox();
			this.imageList1 = new System.Windows.Forms.ImageList(this.components);
			this.label50 = new System.Windows.Forms.Label();
			this.tbSatellite = new System.Windows.Forms.TextBox();
			this.timer2 = new System.Windows.Forms.Timer(this.components);
			this.groupBox1.SuspendLayout();
			this.groupBox2.SuspendLayout();
			this.groupBox4.SuspendLayout();
			this.groupBox5.SuspendLayout();
			this.groupBox6.SuspendLayout();
			this.groupBox3.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)this.arcScaleBackgroundLayerComponent2).BeginInit();
			((System.ComponentModel.ISupportInitialize)this.arcScaleComponent2).BeginInit();
			((System.ComponentModel.ISupportInitialize)this.arcScaleNeedleComponent2).BeginInit();
			((System.ComponentModel.ISupportInitialize)this.arcScaleSpindleCapComponent2).BeginInit();
			((System.ComponentModel.ISupportInitialize)this.circularGauge1).BeginInit();
			((System.ComponentModel.ISupportInitialize)this.arcScaleBackgroundLayerComponent1).BeginInit();
			((System.ComponentModel.ISupportInitialize)this.ascZeroGasPress).BeginInit();
			((System.ComponentModel.ISupportInitialize)this.labelComponent2).BeginInit();
			((System.ComponentModel.ISupportInitialize)this.arcScaleNeedleComponent1).BeginInit();
			((System.ComponentModel.ISupportInitialize)this.arcScaleSpindleCapComponent1).BeginInit();
			((System.ComponentModel.ISupportInitialize)this.circularGauge2).BeginInit();
			((System.ComponentModel.ISupportInitialize)this.arcScaleBackgroundLayerComponent3).BeginInit();
			((System.ComponentModel.ISupportInitialize)this.ascHHPress).BeginInit();
			((System.ComponentModel.ISupportInitialize)this.labelComponent1).BeginInit();
			((System.ComponentModel.ISupportInitialize)this.arcScaleNeedleComponent3).BeginInit();
			((System.ComponentModel.ISupportInitialize)this.arcScaleSpindleCapComponent3).BeginInit();
			((System.ComponentModel.ISupportInitialize)this.circularGauge3).BeginInit();
			((System.ComponentModel.ISupportInitialize)this.arcScaleBackgroundLayerComponent4).BeginInit();
			((System.ComponentModel.ISupportInitialize)this.ascStandardGasPress).BeginInit();
			((System.ComponentModel.ISupportInitialize)this.labelComponent3).BeginInit();
			((System.ComponentModel.ISupportInitialize)this.arcScaleNeedleComponent4).BeginInit();
			((System.ComponentModel.ISupportInitialize)this.arcScaleSpindleCapComponent4).BeginInit();
			((System.ComponentModel.ISupportInitialize)this.chartCH4).BeginInit();
			((System.ComponentModel.ISupportInitialize)xYDiagram).BeginInit();
			((System.ComponentModel.ISupportInitialize)series).BeginInit();
			((System.ComponentModel.ISupportInitialize)sideBySideBarSeriesLabel).BeginInit();
			((System.ComponentModel.ISupportInitialize)this.chartTHC).BeginInit();
			((System.ComponentModel.ISupportInitialize)xYDiagram2).BeginInit();
			((System.ComponentModel.ISupportInitialize)series2).BeginInit();
			((System.ComponentModel.ISupportInitialize)sideBySideBarSeriesLabel2).BeginInit();
			((System.ComponentModel.ISupportInitialize)this.chartNMHC).BeginInit();
			((System.ComponentModel.ISupportInitialize)xYDiagram3).BeginInit();
			((System.ComponentModel.ISupportInitialize)series3).BeginInit();
			((System.ComponentModel.ISupportInitialize)sideBySideBarSeriesLabel3).BeginInit();
			((System.ComponentModel.ISupportInitialize)this.linearGauge3).BeginInit();
			((System.ComponentModel.ISupportInitialize)this.labelComponent9).BeginInit();
			((System.ComponentModel.ISupportInitialize)this.labelComponent10).BeginInit();
			((System.ComponentModel.ISupportInitialize)this.labelComponent11).BeginInit();
			((System.ComponentModel.ISupportInitialize)this.labelComponent12).BeginInit();
			((System.ComponentModel.ISupportInitialize)this.labelComponent13).BeginInit();
			((System.ComponentModel.ISupportInitialize)this.labelComponent14).BeginInit();
			((System.ComponentModel.ISupportInitialize)this.linearScaleMarkerComponent5).BeginInit();
			((System.ComponentModel.ISupportInitialize)this.linearScaleComponent10).BeginInit();
			((System.ComponentModel.ISupportInitialize)this.linearScaleMarkerComponent6).BeginInit();
			((System.ComponentModel.ISupportInitialize)this.linearScaleComponent12).BeginInit();
			((System.ComponentModel.ISupportInitialize)this.linearScaleMarkerComponent7).BeginInit();
			((System.ComponentModel.ISupportInitialize)this.linearScaleComponent14).BeginInit();
			((System.ComponentModel.ISupportInitialize)this.linearScaleRangeBarComponent5).BeginInit();
			((System.ComponentModel.ISupportInitialize)this.lSCTEnvir).BeginInit();
			((System.ComponentModel.ISupportInitialize)this.linearScaleRangeBarComponent6).BeginInit();
			((System.ComponentModel.ISupportInitialize)this.lSCHUMEnvir).BeginInit();
			((System.ComponentModel.ISupportInitialize)this.linearScaleRangeBarComponent7).BeginInit();
			((System.ComponentModel.ISupportInitialize)this.lSCPreEnvir).BeginInit();
			((System.ComponentModel.ISupportInitialize)this.spChrom).BeginInit();
			this.spChrom.Panel1.SuspendLayout();
			this.spChrom.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)this.pictureBox1).BeginInit();
			this.groupBox8.SuspendLayout();
			this.groupBox9.SuspendLayout();
			this.panel1.SuspendLayout();
			base.SuspendLayout();
			this.label79.AutoSize = true;
			this.label79.Location = new System.Drawing.Point(15, 84);
			this.label79.Name = "label79";
			this.label79.Size = new System.Drawing.Size(41, 12);
			this.label79.TabIndex = 60;
			this.label79.Text = "甲烷：";
			this.labCH4Rlt.AutoSize = true;
			this.labCH4Rlt.Cursor = System.Windows.Forms.Cursors.Hand;
			this.labCH4Rlt.Location = new System.Drawing.Point(91, 84);
			this.labCH4Rlt.Name = "labCH4Rlt";
			this.labCH4Rlt.Size = new System.Drawing.Size(11, 12);
			this.labCH4Rlt.TabIndex = 61;
			this.labCH4Rlt.Text = "0";
			this.labTHCRlt.AutoSize = true;
			this.labTHCRlt.Cursor = System.Windows.Forms.Cursors.Hand;
			this.labTHCRlt.Location = new System.Drawing.Point(91, 35);
			this.labTHCRlt.Name = "labTHCRlt";
			this.labTHCRlt.Size = new System.Drawing.Size(11, 12);
			this.labTHCRlt.TabIndex = 59;
			this.labTHCRlt.Text = "0";
			this.label77.AutoSize = true;
			this.label77.Location = new System.Drawing.Point(15, 35);
			this.label77.Name = "label77";
			this.label77.Size = new System.Drawing.Size(41, 12);
			this.label77.TabIndex = 58;
			this.label77.Text = "总烃：";
			this.lbNMHCT.AutoSize = true;
			this.lbNMHCT.Location = new System.Drawing.Point(15, 132);
			this.lbNMHCT.Name = "lbNMHCT";
			this.lbNMHCT.Size = new System.Drawing.Size(77, 12);
			this.lbNMHCT.TabIndex = 77;
			this.lbNMHCT.Text = "非甲烷总烃：";
			this.labNMHCRlt.AutoSize = true;
			this.labNMHCRlt.Cursor = System.Windows.Forms.Cursors.Hand;
			this.labNMHCRlt.Location = new System.Drawing.Point(91, 132);
			this.labNMHCRlt.Name = "labNMHCRlt";
			this.labNMHCRlt.Size = new System.Drawing.Size(11, 12);
			this.labNMHCRlt.TabIndex = 79;
			this.labNMHCRlt.Text = "0";
			this.groupBox1.Controls.Add(this.lbNMHCT);
			this.groupBox1.Controls.Add(this.label79);
			this.groupBox1.Controls.Add(this.label77);
			this.groupBox1.Controls.Add(this.labCH4Rlt);
			this.groupBox1.Controls.Add(this.labNMHCRlt);
			this.groupBox1.Controls.Add(this.labTHCRlt);
			this.groupBox1.Location = new System.Drawing.Point(6, 54);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(300, 187);
			this.groupBox1.TabIndex = 80;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "测量结果";
			this.groupBox2.Controls.Add(this.label33);
			this.groupBox2.Controls.Add(this.groupBox4);
			this.groupBox2.Controls.Add(this.labCYGXSet);
			this.groupBox2.Controls.Add(this.label32);
			this.groupBox2.Controls.Add(this.label31);
			this.groupBox2.Controls.Add(this.groupBox1);
			this.groupBox2.Controls.Add(this.label1);
			this.groupBox2.Controls.Add(this.label2);
			this.groupBox2.Controls.Add(this.label3);
			this.groupBox2.Controls.Add(this.labFaXiangSet);
			this.groupBox2.Controls.Add(this.labCHSSet);
			this.groupBox2.Controls.Add(this.labDecSet);
			this.groupBox2.Controls.Add(this.groupBox5);
			this.groupBox2.Location = new System.Drawing.Point(497, 418);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(108, 78);
			this.groupBox2.TabIndex = 81;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "温度参数";
			this.groupBox2.Visible = false;
			this.label33.AutoSize = true;
			this.label33.Location = new System.Drawing.Point(15, 193);
			this.label33.Name = "label33";
			this.label33.Size = new System.Drawing.Size(65, 12);
			this.label33.TabIndex = 82;
			this.label33.Text = "采样管线：";
			this.groupBox4.Controls.Add(this.labSampleCur);
			this.groupBox4.Controls.Add(this.label17);
			this.groupBox4.Controls.Add(this.labSampleSet);
			this.groupBox4.Controls.Add(this.label40);
			this.groupBox4.Controls.Add(this.label41);
			this.groupBox4.Controls.Add(this.label42);
			this.groupBox4.Controls.Add(this.label43);
			this.groupBox4.Controls.Add(this.label44);
			this.groupBox4.Controls.Add(this.labHHSet);
			this.groupBox4.Controls.Add(this.labAirSet);
			this.groupBox4.Controls.Add(this.labZQSet);
			this.groupBox4.Location = new System.Drawing.Point(127, 15);
			this.groupBox4.Name = "groupBox4";
			this.groupBox4.Size = new System.Drawing.Size(300, 247);
			this.groupBox4.TabIndex = 87;
			this.groupBox4.TabStop = false;
			this.groupBox4.Text = "压力流量参数";
			this.labSampleCur.AutoSize = true;
			this.labSampleCur.Cursor = System.Windows.Forms.Cursors.Hand;
			this.labSampleCur.ForeColor = System.Drawing.Color.Red;
			this.labSampleCur.Location = new System.Drawing.Point(199, 195);
			this.labSampleCur.Name = "labSampleCur";
			this.labSampleCur.Size = new System.Drawing.Size(11, 12);
			this.labSampleCur.TabIndex = 87;
			this.labSampleCur.Text = "0";
			this.label17.AutoSize = true;
			this.label17.ForeColor = System.Drawing.Color.Red;
			this.label17.Location = new System.Drawing.Point(15, 193);
			this.label17.Name = "label17";
			this.label17.Size = new System.Drawing.Size(65, 12);
			this.label17.TabIndex = 82;
			this.label17.Text = "采样流量：";
			this.labSampleSet.AutoSize = true;
			this.labSampleSet.Cursor = System.Windows.Forms.Cursors.Hand;
			this.labSampleSet.ForeColor = System.Drawing.Color.Red;
			this.labSampleSet.Location = new System.Drawing.Point(109, 195);
			this.labSampleSet.Name = "labSampleSet";
			this.labSampleSet.Size = new System.Drawing.Size(11, 12);
			this.labSampleSet.TabIndex = 83;
			this.labSampleSet.Text = "0";
			this.label40.AutoSize = true;
			this.label40.Location = new System.Drawing.Point(199, 17);
			this.label40.Name = "label40";
			this.label40.Size = new System.Drawing.Size(41, 12);
			this.label40.TabIndex = 81;
			this.label40.Text = "实测值";
			this.label41.AutoSize = true;
			this.label41.Location = new System.Drawing.Point(109, 17);
			this.label41.Name = "label41";
			this.label41.Size = new System.Drawing.Size(41, 12);
			this.label41.TabIndex = 80;
			this.label41.Text = "设定值";
			this.label42.AutoSize = true;
			this.label42.Location = new System.Drawing.Point(15, 145);
			this.label42.Name = "label42";
			this.label42.Size = new System.Drawing.Size(41, 12);
			this.label42.TabIndex = 77;
			this.label42.Text = "空气：";
			this.label43.AutoSize = true;
			this.label43.Location = new System.Drawing.Point(15, 101);
			this.label43.Name = "label43";
			this.label43.Size = new System.Drawing.Size(41, 12);
			this.label43.TabIndex = 60;
			this.label43.Text = "氢气：";
			this.label44.AutoSize = true;
			this.label44.Location = new System.Drawing.Point(15, 54);
			this.label44.Name = "label44";
			this.label44.Size = new System.Drawing.Size(41, 12);
			this.label44.TabIndex = 58;
			this.label44.Text = "载气：";
			this.labHHSet.AutoSize = true;
			this.labHHSet.Cursor = System.Windows.Forms.Cursors.Hand;
			this.labHHSet.Location = new System.Drawing.Point(109, 103);
			this.labHHSet.Name = "labHHSet";
			this.labHHSet.Size = new System.Drawing.Size(11, 12);
			this.labHHSet.TabIndex = 61;
			this.labHHSet.Text = "0";
			this.labAirSet.AutoSize = true;
			this.labAirSet.Cursor = System.Windows.Forms.Cursors.Hand;
			this.labAirSet.Location = new System.Drawing.Point(109, 147);
			this.labAirSet.Name = "labAirSet";
			this.labAirSet.Size = new System.Drawing.Size(11, 12);
			this.labAirSet.TabIndex = 79;
			this.labAirSet.Text = "0";
			this.labZQSet.AutoSize = true;
			this.labZQSet.Cursor = System.Windows.Forms.Cursors.Hand;
			this.labZQSet.Location = new System.Drawing.Point(109, 54);
			this.labZQSet.Name = "labZQSet";
			this.labZQSet.Size = new System.Drawing.Size(11, 12);
			this.labZQSet.TabIndex = 59;
			this.labZQSet.Text = "0";
			this.labCYGXSet.AutoSize = true;
			this.labCYGXSet.Cursor = System.Windows.Forms.Cursors.Hand;
			this.labCYGXSet.Location = new System.Drawing.Point(109, 195);
			this.labCYGXSet.Name = "labCYGXSet";
			this.labCYGXSet.Size = new System.Drawing.Size(11, 12);
			this.labCYGXSet.TabIndex = 83;
			this.labCYGXSet.Text = "0";
			this.label32.AutoSize = true;
			this.label32.Location = new System.Drawing.Point(200, 17);
			this.label32.Name = "label32";
			this.label32.Size = new System.Drawing.Size(41, 12);
			this.label32.TabIndex = 81;
			this.label32.Text = "实测值";
			this.label31.AutoSize = true;
			this.label31.Location = new System.Drawing.Point(109, 17);
			this.label31.Name = "label31";
			this.label31.Size = new System.Drawing.Size(41, 12);
			this.label31.TabIndex = 80;
			this.label31.Text = "设定值";
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(15, 145);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(53, 12);
			this.label1.TabIndex = 77;
			this.label1.Text = "催化室：";
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(15, 101);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(41, 12);
			this.label2.TabIndex = 60;
			this.label2.Text = "阀箱：";
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(15, 54);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(53, 12);
			this.label3.TabIndex = 58;
			this.label3.Text = "检测器：";
			this.labFaXiangSet.AutoSize = true;
			this.labFaXiangSet.Cursor = System.Windows.Forms.Cursors.Hand;
			this.labFaXiangSet.Location = new System.Drawing.Point(109, 103);
			this.labFaXiangSet.Name = "labFaXiangSet";
			this.labFaXiangSet.Size = new System.Drawing.Size(11, 12);
			this.labFaXiangSet.TabIndex = 61;
			this.labFaXiangSet.Text = "0";
			this.labCHSSet.AutoSize = true;
			this.labCHSSet.Cursor = System.Windows.Forms.Cursors.Hand;
			this.labCHSSet.Location = new System.Drawing.Point(109, 147);
			this.labCHSSet.Name = "labCHSSet";
			this.labCHSSet.Size = new System.Drawing.Size(11, 12);
			this.labCHSSet.TabIndex = 79;
			this.labCHSSet.Text = "0";
			this.labDecSet.AutoSize = true;
			this.labDecSet.Cursor = System.Windows.Forms.Cursors.Hand;
			this.labDecSet.Location = new System.Drawing.Point(109, 54);
			this.labDecSet.Name = "labDecSet";
			this.labDecSet.Size = new System.Drawing.Size(11, 12);
			this.labDecSet.TabIndex = 59;
			this.labDecSet.Text = "0";
			this.groupBox5.Controls.Add(this.tbTimeChuiSao);
			this.groupBox5.Controls.Add(this.btnChuiSao);
			this.groupBox5.Controls.Add(this.label21);
			this.groupBox5.Controls.Add(this.label24);
			this.groupBox5.Controls.Add(this.groupBox6);
			this.groupBox5.Location = new System.Drawing.Point(309, 25);
			this.groupBox5.Name = "groupBox5";
			this.groupBox5.Size = new System.Drawing.Size(300, 247);
			this.groupBox5.TabIndex = 85;
			this.groupBox5.TabStop = false;
			this.groupBox5.Text = "吹扫清洗";
			this.tbTimeChuiSao.Location = new System.Drawing.Point(86, 30);
			this.tbTimeChuiSao.Name = "tbTimeChuiSao";
			this.tbTimeChuiSao.Size = new System.Drawing.Size(80, 21);
			this.tbTimeChuiSao.TabIndex = 61;
			this.btnChuiSao.Location = new System.Drawing.Point(16, 121);
			this.btnChuiSao.Name = "btnChuiSao";
			this.btnChuiSao.Size = new System.Drawing.Size(75, 23);
			this.btnChuiSao.TabIndex = 60;
			this.btnChuiSao.Text = "开始";
			this.btnChuiSao.UseVisualStyleBackColor = true;
			this.label21.AutoSize = true;
			this.label21.Location = new System.Drawing.Point(15, 35);
			this.label21.Name = "label21";
			this.label21.Size = new System.Drawing.Size(65, 12);
			this.label21.TabIndex = 58;
			this.label21.Text = "吹扫时间：";
			this.label24.AutoSize = true;
			this.label24.Cursor = System.Windows.Forms.Cursors.Hand;
			this.label24.Location = new System.Drawing.Point(179, 35);
			this.label24.Name = "label24";
			this.label24.Size = new System.Drawing.Size(23, 12);
			this.label24.TabIndex = 59;
			this.label24.Text = "min";
			this.groupBox6.Controls.Add(this.tbTimeHuoHua);
			this.groupBox6.Controls.Add(this.label22);
			this.groupBox6.Controls.Add(this.label23);
			this.groupBox6.Controls.Add(this.tbTempHuoHua);
			this.groupBox6.Controls.Add(this.btnHuoHua);
			this.groupBox6.Controls.Add(this.label19);
			this.groupBox6.Controls.Add(this.label20);
			this.groupBox6.Controls.Add(this.groupBox3);
			this.groupBox6.Location = new System.Drawing.Point(38, 96);
			this.groupBox6.Name = "groupBox6";
			this.groupBox6.Size = new System.Drawing.Size(300, 91);
			this.groupBox6.TabIndex = 86;
			this.groupBox6.TabStop = false;
			this.groupBox6.Text = "测量结果";
			this.tbTimeHuoHua.Location = new System.Drawing.Point(86, 75);
			this.tbTimeHuoHua.Name = "tbTimeHuoHua";
			this.tbTimeHuoHua.Size = new System.Drawing.Size(80, 21);
			this.tbTimeHuoHua.TabIndex = 68;
			this.label22.AutoSize = true;
			this.label22.Location = new System.Drawing.Point(15, 80);
			this.label22.Name = "label22";
			this.label22.Size = new System.Drawing.Size(65, 12);
			this.label22.TabIndex = 66;
			this.label22.Text = "活化时间：";
			this.label23.AutoSize = true;
			this.label23.Cursor = System.Windows.Forms.Cursors.Hand;
			this.label23.Location = new System.Drawing.Point(179, 80);
			this.label23.Name = "label23";
			this.label23.Size = new System.Drawing.Size(23, 12);
			this.label23.TabIndex = 67;
			this.label23.Text = "min";
			this.tbTempHuoHua.Location = new System.Drawing.Point(85, 25);
			this.tbTempHuoHua.Name = "tbTempHuoHua";
			this.tbTempHuoHua.Size = new System.Drawing.Size(80, 21);
			this.tbTempHuoHua.TabIndex = 65;
			this.btnHuoHua.Location = new System.Drawing.Point(17, 121);
			this.btnHuoHua.Name = "btnHuoHua";
			this.btnHuoHua.Size = new System.Drawing.Size(75, 23);
			this.btnHuoHua.TabIndex = 64;
			this.btnHuoHua.Text = "开始";
			this.btnHuoHua.UseVisualStyleBackColor = true;
			this.label19.AutoSize = true;
			this.label19.Location = new System.Drawing.Point(14, 30);
			this.label19.Name = "label19";
			this.label19.Size = new System.Drawing.Size(65, 12);
			this.label19.TabIndex = 62;
			this.label19.Text = "活化温度：";
			this.label20.AutoSize = true;
			this.label20.Cursor = System.Windows.Forms.Cursors.Hand;
			this.label20.Location = new System.Drawing.Point(178, 30);
			this.label20.Name = "label20";
			this.label20.Size = new System.Drawing.Size(17, 12);
			this.label20.TabIndex = 63;
			this.label20.Text = "℃";
			this.groupBox3.Controls.Add(this.labHumiEnvirCur);
			this.groupBox3.Controls.Add(this.labLatitudeCur);
			this.groupBox3.Controls.Add(this.labLongitudeCur);
			this.groupBox3.Controls.Add(this.label39);
			this.groupBox3.Controls.Add(this.label7);
			this.groupBox3.Controls.Add(this.label8);
			this.groupBox3.Controls.Add(this.label9);
			this.groupBox3.Controls.Add(this.labTempEnvirCur);
			this.groupBox3.Controls.Add(this.labBaroEnvirCur);
			this.groupBox3.Controls.Add(this.label12);
			this.groupBox3.Location = new System.Drawing.Point(225, 3);
			this.groupBox3.Name = "groupBox3";
			this.groupBox3.Size = new System.Drawing.Size(300, 187);
			this.groupBox3.TabIndex = 84;
			this.groupBox3.TabStop = false;
			this.groupBox3.Text = "测量结果";
			this.labHumiEnvirCur.AutoSize = true;
			this.labHumiEnvirCur.Cursor = System.Windows.Forms.Cursors.Hand;
			this.labHumiEnvirCur.Location = new System.Drawing.Point(167, 80);
			this.labHumiEnvirCur.Name = "labHumiEnvirCur";
			this.labHumiEnvirCur.Size = new System.Drawing.Size(11, 12);
			this.labHumiEnvirCur.TabIndex = 83;
			this.labHumiEnvirCur.Text = "0";
			this.labLatitudeCur.AutoSize = true;
			this.labLatitudeCur.Cursor = System.Windows.Forms.Cursors.Hand;
			this.labLatitudeCur.Location = new System.Drawing.Point(167, 35);
			this.labLatitudeCur.Name = "labLatitudeCur";
			this.labLatitudeCur.Size = new System.Drawing.Size(11, 12);
			this.labLatitudeCur.TabIndex = 82;
			this.labLatitudeCur.Text = "0";
			this.labLongitudeCur.AutoSize = true;
			this.labLongitudeCur.Cursor = System.Windows.Forms.Cursors.Hand;
			this.labLongitudeCur.Location = new System.Drawing.Point(69, 33);
			this.labLongitudeCur.Name = "labLongitudeCur";
			this.labLongitudeCur.Size = new System.Drawing.Size(11, 12);
			this.labLongitudeCur.TabIndex = 81;
			this.labLongitudeCur.Text = "0";
			this.label39.AutoSize = true;
			this.label39.Location = new System.Drawing.Point(109, 80);
			this.label39.Name = "label39";
			this.label39.Size = new System.Drawing.Size(41, 12);
			this.label39.TabIndex = 80;
			this.label39.Text = "湿度：";
			this.label7.AutoSize = true;
			this.label7.Location = new System.Drawing.Point(15, 126);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(53, 12);
			this.label7.TabIndex = 77;
			this.label7.Text = "大气压：";
			this.label8.AutoSize = true;
			this.label8.Location = new System.Drawing.Point(15, 80);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(41, 12);
			this.label8.TabIndex = 60;
			this.label8.Text = "温度：";
			this.label9.AutoSize = true;
			this.label9.Location = new System.Drawing.Point(15, 35);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(41, 12);
			this.label9.TabIndex = 58;
			this.label9.Text = "经度：";
			this.labTempEnvirCur.AutoSize = true;
			this.labTempEnvirCur.Cursor = System.Windows.Forms.Cursors.Hand;
			this.labTempEnvirCur.Location = new System.Drawing.Point(69, 80);
			this.labTempEnvirCur.Name = "labTempEnvirCur";
			this.labTempEnvirCur.Size = new System.Drawing.Size(11, 12);
			this.labTempEnvirCur.TabIndex = 61;
			this.labTempEnvirCur.Text = "0";
			this.labBaroEnvirCur.AutoSize = true;
			this.labBaroEnvirCur.Cursor = System.Windows.Forms.Cursors.Hand;
			this.labBaroEnvirCur.Location = new System.Drawing.Point(69, 126);
			this.labBaroEnvirCur.Name = "labBaroEnvirCur";
			this.labBaroEnvirCur.Size = new System.Drawing.Size(11, 12);
			this.labBaroEnvirCur.TabIndex = 79;
			this.labBaroEnvirCur.Text = "0";
			this.label12.AutoSize = true;
			this.label12.Cursor = System.Windows.Forms.Cursors.Hand;
			this.label12.Location = new System.Drawing.Point(109, 35);
			this.label12.Name = "label12";
			this.label12.Size = new System.Drawing.Size(41, 12);
			this.label12.TabIndex = 59;
			this.label12.Text = "纬度：";
			this.labCYGXCur.AutoSize = true;
			this.labCYGXCur.Cursor = System.Windows.Forms.Cursors.Hand;
			this.labCYGXCur.Font = new System.Drawing.Font("SimSun", 15f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
			this.labCYGXCur.Location = new System.Drawing.Point(94, 139);
			this.labCYGXCur.Name = "labCYGXCur";
			this.labCYGXCur.Size = new System.Drawing.Size(19, 20);
			this.labCYGXCur.TabIndex = 87;
			this.labCYGXCur.Text = "0";
			this.labFaXiangCur.AutoSize = true;
			this.labFaXiangCur.Cursor = System.Windows.Forms.Cursors.Hand;
			this.labFaXiangCur.Font = new System.Drawing.Font("SimSun", 15f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
			this.labFaXiangCur.Location = new System.Drawing.Point(94, 105);
			this.labFaXiangCur.Name = "labFaXiangCur";
			this.labFaXiangCur.Size = new System.Drawing.Size(19, 20);
			this.labFaXiangCur.TabIndex = 85;
			this.labFaXiangCur.Text = "0";
			this.labCHSCur.AutoSize = true;
			this.labCHSCur.Cursor = System.Windows.Forms.Cursors.Hand;
			this.labCHSCur.Font = new System.Drawing.Font("SimSun", 15f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
			this.labCHSCur.Location = new System.Drawing.Point(94, 71);
			this.labCHSCur.Name = "labCHSCur";
			this.labCHSCur.Size = new System.Drawing.Size(19, 20);
			this.labCHSCur.TabIndex = 86;
			this.labCHSCur.Text = "0";
			this.labDecCur.AutoSize = true;
			this.labDecCur.Cursor = System.Windows.Forms.Cursors.Hand;
			this.labDecCur.Font = new System.Drawing.Font("SimSun", 15f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
			this.labDecCur.Location = new System.Drawing.Point(94, 41);
			this.labDecCur.Name = "labDecCur";
			this.labDecCur.Size = new System.Drawing.Size(19, 20);
			this.labDecCur.TabIndex = 84;
			this.labDecCur.Text = "0";
			this.labHHCur.AutoSize = true;
			this.labHHCur.Cursor = System.Windows.Forms.Cursors.Hand;
			this.labHHCur.Location = new System.Drawing.Point(94, 199);
			this.labHHCur.Name = "labHHCur";
			this.labHHCur.Size = new System.Drawing.Size(11, 12);
			this.labHHCur.TabIndex = 85;
			this.labHHCur.Text = "0";
			this.labAirCur.AutoSize = true;
			this.labAirCur.Cursor = System.Windows.Forms.Cursors.Hand;
			this.labAirCur.Location = new System.Drawing.Point(94, 232);
			this.labAirCur.Name = "labAirCur";
			this.labAirCur.Size = new System.Drawing.Size(11, 12);
			this.labAirCur.TabIndex = 86;
			this.labAirCur.Text = "0";
			this.labZQCur.AutoSize = true;
			this.labZQCur.Cursor = System.Windows.Forms.Cursors.Hand;
			this.labZQCur.Location = new System.Drawing.Point(94, 169);
			this.labZQCur.Name = "labZQCur";
			this.labZQCur.Size = new System.Drawing.Size(11, 12);
			this.labZQCur.TabIndex = 84;
			this.labZQCur.Text = "0";
			this.arcScaleBackgroundLayerComponent2.AcceptOrder = -1000;
			this.arcScaleBackgroundLayerComponent2.Name = "arcScaleBackgroundLayerComponent2";
			this.arcScaleBackgroundLayerComponent2.ScaleCenterPos = new DevExpress.XtraGauges.Core.Base.PointF2D(0.5f, 0.72f);
			this.arcScaleBackgroundLayerComponent2.ShapeType = DevExpress.XtraGauges.Core.Model.BackgroundLayerShapeType.CircularHalf_Style2;
			this.arcScaleBackgroundLayerComponent2.Size = new System.Drawing.SizeF(244f, 170f);
			this.arcScaleBackgroundLayerComponent2.ZOrder = 1000;
			this.arcScaleComponent2.AcceptOrder = 0;
			this.arcScaleComponent2.AppearanceTickmarkText.Font = new System.Drawing.Font("Microsoft Sans Serif", 11f, System.Drawing.FontStyle.Bold);
			this.arcScaleComponent2.AppearanceTickmarkText.TextBrush = new DevExpress.XtraGauges.Core.Drawing.SolidBrushObject("Color:#C0C0FF");
			this.arcScaleComponent2.Center = new DevExpress.XtraGauges.Core.Base.PointF2D(125f, 165f);
			this.arcScaleComponent2.EndAngle = 0f;
			this.arcScaleComponent2.MajorTickCount = 7;
			this.arcScaleComponent2.MajorTickmark.AllowTickOverlap = true;
			this.arcScaleComponent2.MajorTickmark.FormatString = "{0:F0}";
			this.arcScaleComponent2.MajorTickmark.ShapeOffset = -9f;
			this.arcScaleComponent2.MajorTickmark.ShapeType = DevExpress.XtraGauges.Core.Model.TickmarkShapeType.Circular_Style2_2;
			this.arcScaleComponent2.MajorTickmark.TextOffset = -22f;
			this.arcScaleComponent2.MajorTickmark.TextOrientation = DevExpress.XtraGauges.Core.Model.LabelOrientation.LeftToRight;
			this.arcScaleComponent2.MaxValue = 100f;
			this.arcScaleComponent2.MinorTickCount = 4;
			this.arcScaleComponent2.MinorTickmark.ShapeType = DevExpress.XtraGauges.Core.Model.TickmarkShapeType.Circular_Style2_1;
			this.arcScaleComponent2.MinValue = 40f;
			this.arcScaleComponent2.Name = "arcScaleComponent2";
			this.arcScaleComponent2.RadiusX = 91f;
			this.arcScaleComponent2.RadiusY = 91f;
			this.arcScaleComponent2.StartAngle = -180f;
			this.arcScaleComponent2.Value = 40f;
			this.arcScaleNeedleComponent2.AcceptOrder = 50;
			this.arcScaleNeedleComponent2.EndOffset = -6f;
			this.arcScaleNeedleComponent2.Name = "arcScaleNeedleComponent2";
			this.arcScaleNeedleComponent2.ShapeType = DevExpress.XtraGauges.Core.Model.NeedleShapeType.CircularFull_Style2;
			this.arcScaleNeedleComponent2.StartOffset = 9f;
			this.arcScaleNeedleComponent2.ZOrder = -50;
			this.arcScaleSpindleCapComponent2.AcceptOrder = 100;
			this.arcScaleSpindleCapComponent2.Name = "arcScaleSpindleCapComponent2";
			this.arcScaleSpindleCapComponent2.ShapeType = DevExpress.XtraGauges.Core.Model.SpindleCapShapeType.CircularFull_Style2;
			this.arcScaleSpindleCapComponent2.Size = new System.Drawing.SizeF(24f, 24f);
			this.arcScaleSpindleCapComponent2.ZOrder = -100;
			this.gaugeControl1.Gauges.AddRange(new DevExpress.XtraGauges.Base.IGauge[1] { this.circularGauge1 });
			this.gaugeControl1.Location = new System.Drawing.Point(1, 338);
			this.gaugeControl1.Name = "gaugeControl1";
			this.gaugeControl1.Size = new System.Drawing.Size(180, 168);
			this.gaugeControl1.TabIndex = 92;
			this.circularGauge1.BackgroundLayers.AddRange(new DevExpress.XtraGauges.Win.Gauges.Circular.ArcScaleBackgroundLayerComponent[1] { this.arcScaleBackgroundLayerComponent1 });
			this.circularGauge1.Bounds = new System.Drawing.Rectangle(6, 6, 168, 156);
			this.circularGauge1.Labels.AddRange(new DevExpress.XtraGauges.Win.Base.LabelComponent[1] { this.labelComponent2 });
			this.circularGauge1.Name = "circularGauge1";
			this.circularGauge1.Needles.AddRange(new DevExpress.XtraGauges.Win.Gauges.Circular.ArcScaleNeedleComponent[1] { this.arcScaleNeedleComponent1 });
			this.circularGauge1.Scales.AddRange(new DevExpress.XtraGauges.Win.Gauges.Circular.ArcScaleComponent[1] { this.ascZeroGasPress });
			this.circularGauge1.SpindleCaps.AddRange(new DevExpress.XtraGauges.Win.Gauges.Circular.ArcScaleSpindleCapComponent[1] { this.arcScaleSpindleCapComponent1 });
			this.arcScaleBackgroundLayerComponent1.ArcScale = this.ascZeroGasPress;
			this.arcScaleBackgroundLayerComponent1.Name = "arcScaleBackgroundLayerComponent4";
			this.arcScaleBackgroundLayerComponent1.ScaleCenterPos = new DevExpress.XtraGauges.Core.Base.PointF2D(0.5f, 0.6f);
			this.arcScaleBackgroundLayerComponent1.ShapeType = DevExpress.XtraGauges.Core.Model.BackgroundLayerShapeType.CircularThreeFourth_Style2;
			this.arcScaleBackgroundLayerComponent1.Size = new System.Drawing.SizeF(250f, 207f);
			this.arcScaleBackgroundLayerComponent1.ZOrder = 1000;
			this.ascZeroGasPress.AppearanceTickmarkText.Font = new System.Drawing.Font("Microsoft Sans Serif", 11f, System.Drawing.FontStyle.Bold);
			this.ascZeroGasPress.AppearanceTickmarkText.TextBrush = new DevExpress.XtraGauges.Core.Drawing.SolidBrushObject("Color:#C0C0FF");
			this.ascZeroGasPress.Center = new DevExpress.XtraGauges.Core.Base.PointF2D(125f, 140f);
			this.ascZeroGasPress.EndAngle = 30f;
			this.ascZeroGasPress.MajorTickCount = 7;
			this.ascZeroGasPress.MajorTickmark.AllowTickOverlap = true;
			this.ascZeroGasPress.MajorTickmark.FormatString = "{0:F0}";
			this.ascZeroGasPress.MajorTickmark.ShapeOffset = -9f;
			this.ascZeroGasPress.MajorTickmark.ShapeType = DevExpress.XtraGauges.Core.Model.TickmarkShapeType.Circular_Style2_2;
			this.ascZeroGasPress.MajorTickmark.TextOffset = -22f;
			this.ascZeroGasPress.MajorTickmark.TextOrientation = DevExpress.XtraGauges.Core.Model.LabelOrientation.LeftToRight;
			this.ascZeroGasPress.MaxValue = 150f;
			this.ascZeroGasPress.MinorTickCount = 4;
			this.ascZeroGasPress.MinorTickmark.ShapeType = DevExpress.XtraGauges.Core.Model.TickmarkShapeType.Circular_Style2_1;
			this.ascZeroGasPress.Name = "arcScaleComponent4";
			this.ascZeroGasPress.RadiusX = 91f;
			this.ascZeroGasPress.RadiusY = 91f;
			this.ascZeroGasPress.StartAngle = -210f;
			this.ascZeroGasPress.Value = 50f;
			this.labelComponent2.AllowHTMLString = true;
			this.labelComponent2.AppearanceBackground.BorderWidth = 2f;
			this.labelComponent2.AppearanceText.Font = new System.Drawing.Font("SimSun", 20f, System.Drawing.FontStyle.Bold);
			this.labelComponent2.AppearanceText.TextBrush = new DevExpress.XtraGauges.Core.Drawing.SolidBrushObject("Color:Black");
			this.labelComponent2.Name = "circularGauge1_Label1";
			this.labelComponent2.Position = new DevExpress.XtraGauges.Core.Base.PointF2D(125f, 220f);
			this.labelComponent2.Shader = new DevExpress.XtraGauges.Core.Drawing.OpacityShader("");
			this.labelComponent2.Text = "零气";
			this.labelComponent2.ZOrder = -1001;
			this.arcScaleNeedleComponent1.ArcScale = this.ascZeroGasPress;
			this.arcScaleNeedleComponent1.EndOffset = -6f;
			this.arcScaleNeedleComponent1.Name = "arcScaleNeedleComponent4";
			this.arcScaleNeedleComponent1.ShapeType = DevExpress.XtraGauges.Core.Model.NeedleShapeType.CircularFull_Style2;
			this.arcScaleNeedleComponent1.StartOffset = 9f;
			this.arcScaleNeedleComponent1.ZOrder = -50;
			this.arcScaleSpindleCapComponent1.ArcScale = this.ascZeroGasPress;
			this.arcScaleSpindleCapComponent1.Name = "arcScaleSpindleCapComponent4";
			this.arcScaleSpindleCapComponent1.ShapeType = DevExpress.XtraGauges.Core.Model.SpindleCapShapeType.CircularFull_Style2;
			this.arcScaleSpindleCapComponent1.Size = new System.Drawing.SizeF(24f, 24f);
			this.arcScaleSpindleCapComponent1.ZOrder = -100;
			this.gaugeControl2.Gauges.AddRange(new DevExpress.XtraGauges.Base.IGauge[1] { this.circularGauge2 });
			this.gaugeControl2.Location = new System.Drawing.Point(187, 338);
			this.gaugeControl2.Name = "gaugeControl2";
			this.gaugeControl2.Size = new System.Drawing.Size(180, 168);
			this.gaugeControl2.TabIndex = 93;
			this.circularGauge2.BackgroundLayers.AddRange(new DevExpress.XtraGauges.Win.Gauges.Circular.ArcScaleBackgroundLayerComponent[1] { this.arcScaleBackgroundLayerComponent3 });
			this.circularGauge2.Bounds = new System.Drawing.Rectangle(6, 6, 168, 156);
			this.circularGauge2.Labels.AddRange(new DevExpress.XtraGauges.Win.Base.LabelComponent[1] { this.labelComponent1 });
			this.circularGauge2.Name = "circularGauge2";
			this.circularGauge2.Needles.AddRange(new DevExpress.XtraGauges.Win.Gauges.Circular.ArcScaleNeedleComponent[1] { this.arcScaleNeedleComponent3 });
			this.circularGauge2.Scales.AddRange(new DevExpress.XtraGauges.Win.Gauges.Circular.ArcScaleComponent[1] { this.ascHHPress });
			this.circularGauge2.SpindleCaps.AddRange(new DevExpress.XtraGauges.Win.Gauges.Circular.ArcScaleSpindleCapComponent[1] { this.arcScaleSpindleCapComponent3 });
			this.arcScaleBackgroundLayerComponent3.ArcScale = this.ascHHPress;
			this.arcScaleBackgroundLayerComponent3.Name = "arcScaleBackgroundLayerComponent4";
			this.arcScaleBackgroundLayerComponent3.ScaleCenterPos = new DevExpress.XtraGauges.Core.Base.PointF2D(0.5f, 0.6f);
			this.arcScaleBackgroundLayerComponent3.ShapeType = DevExpress.XtraGauges.Core.Model.BackgroundLayerShapeType.CircularThreeFourth_Style2;
			this.arcScaleBackgroundLayerComponent3.Size = new System.Drawing.SizeF(250f, 207f);
			this.arcScaleBackgroundLayerComponent3.ZOrder = 1000;
			this.ascHHPress.AppearanceTickmarkText.Font = new System.Drawing.Font("Microsoft Sans Serif", 11f, System.Drawing.FontStyle.Bold);
			this.ascHHPress.AppearanceTickmarkText.TextBrush = new DevExpress.XtraGauges.Core.Drawing.SolidBrushObject("Color:#C0C0FF");
			this.ascHHPress.Center = new DevExpress.XtraGauges.Core.Base.PointF2D(125f, 140f);
			this.ascHHPress.EndAngle = 30f;
			this.ascHHPress.MajorTickCount = 7;
			this.ascHHPress.MajorTickmark.AllowTickOverlap = true;
			this.ascHHPress.MajorTickmark.FormatString = "{0:F0}";
			this.ascHHPress.MajorTickmark.ShapeOffset = -9f;
			this.ascHHPress.MajorTickmark.ShapeType = DevExpress.XtraGauges.Core.Model.TickmarkShapeType.Circular_Style2_2;
			this.ascHHPress.MajorTickmark.TextOffset = -22f;
			this.ascHHPress.MajorTickmark.TextOrientation = DevExpress.XtraGauges.Core.Model.LabelOrientation.LeftToRight;
			this.ascHHPress.MaxValue = 150f;
			this.ascHHPress.MinorTickCount = 4;
			this.ascHHPress.MinorTickmark.ShapeType = DevExpress.XtraGauges.Core.Model.TickmarkShapeType.Circular_Style2_1;
			this.ascHHPress.Name = "arcScaleComponent4";
			this.ascHHPress.RadiusX = 91f;
			this.ascHHPress.RadiusY = 91f;
			this.ascHHPress.StartAngle = -210f;
			this.ascHHPress.Value = 50f;
			this.labelComponent1.AllowHTMLString = true;
			this.labelComponent1.AppearanceBackground.BorderWidth = 2f;
			this.labelComponent1.AppearanceText.Font = new System.Drawing.Font("SimSun", 20f, System.Drawing.FontStyle.Bold);
			this.labelComponent1.AppearanceText.TextBrush = new DevExpress.XtraGauges.Core.Drawing.SolidBrushObject("Color:Black");
			this.labelComponent1.Name = "circularGauge1_Label1";
			this.labelComponent1.Position = new DevExpress.XtraGauges.Core.Base.PointF2D(125f, 220f);
			this.labelComponent1.Shader = new DevExpress.XtraGauges.Core.Drawing.OpacityShader("");
			this.labelComponent1.Text = "氢气";
			this.labelComponent1.ZOrder = -1001;
			this.arcScaleNeedleComponent3.ArcScale = this.ascHHPress;
			this.arcScaleNeedleComponent3.EndOffset = -6f;
			this.arcScaleNeedleComponent3.Name = "arcScaleNeedleComponent4";
			this.arcScaleNeedleComponent3.ShapeType = DevExpress.XtraGauges.Core.Model.NeedleShapeType.CircularFull_Style2;
			this.arcScaleNeedleComponent3.StartOffset = 9f;
			this.arcScaleNeedleComponent3.ZOrder = -50;
			this.arcScaleSpindleCapComponent3.ArcScale = this.ascHHPress;
			this.arcScaleSpindleCapComponent3.Name = "arcScaleSpindleCapComponent4";
			this.arcScaleSpindleCapComponent3.ShapeType = DevExpress.XtraGauges.Core.Model.SpindleCapShapeType.CircularFull_Style2;
			this.arcScaleSpindleCapComponent3.Size = new System.Drawing.SizeF(24f, 24f);
			this.arcScaleSpindleCapComponent3.ZOrder = -100;
			this.gaugeControl3.Gauges.AddRange(new DevExpress.XtraGauges.Base.IGauge[1] { this.circularGauge3 });
			this.gaugeControl3.Location = new System.Drawing.Point(373, 338);
			this.gaugeControl3.Name = "gaugeControl3";
			this.gaugeControl3.Size = new System.Drawing.Size(180, 168);
			this.gaugeControl3.TabIndex = 94;
			this.circularGauge3.BackgroundLayers.AddRange(new DevExpress.XtraGauges.Win.Gauges.Circular.ArcScaleBackgroundLayerComponent[1] { this.arcScaleBackgroundLayerComponent4 });
			this.circularGauge3.Bounds = new System.Drawing.Rectangle(6, 6, 168, 156);
			this.circularGauge3.Labels.AddRange(new DevExpress.XtraGauges.Win.Base.LabelComponent[1] { this.labelComponent3 });
			this.circularGauge3.Name = "circularGauge3";
			this.circularGauge3.Needles.AddRange(new DevExpress.XtraGauges.Win.Gauges.Circular.ArcScaleNeedleComponent[1] { this.arcScaleNeedleComponent4 });
			this.circularGauge3.Scales.AddRange(new DevExpress.XtraGauges.Win.Gauges.Circular.ArcScaleComponent[1] { this.ascStandardGasPress });
			this.circularGauge3.SpindleCaps.AddRange(new DevExpress.XtraGauges.Win.Gauges.Circular.ArcScaleSpindleCapComponent[1] { this.arcScaleSpindleCapComponent4 });
			this.arcScaleBackgroundLayerComponent4.ArcScale = this.ascStandardGasPress;
			this.arcScaleBackgroundLayerComponent4.Name = "arcScaleBackgroundLayerComponent4";
			this.arcScaleBackgroundLayerComponent4.ScaleCenterPos = new DevExpress.XtraGauges.Core.Base.PointF2D(0.5f, 0.6f);
			this.arcScaleBackgroundLayerComponent4.ShapeType = DevExpress.XtraGauges.Core.Model.BackgroundLayerShapeType.CircularThreeFourth_Style2;
			this.arcScaleBackgroundLayerComponent4.Size = new System.Drawing.SizeF(250f, 207f);
			this.arcScaleBackgroundLayerComponent4.ZOrder = 1000;
			this.ascStandardGasPress.AppearanceTickmarkText.Font = new System.Drawing.Font("Microsoft Sans Serif", 11f, System.Drawing.FontStyle.Bold);
			this.ascStandardGasPress.AppearanceTickmarkText.TextBrush = new DevExpress.XtraGauges.Core.Drawing.SolidBrushObject("Color:#C0C0FF");
			this.ascStandardGasPress.Center = new DevExpress.XtraGauges.Core.Base.PointF2D(125f, 140f);
			this.ascStandardGasPress.EndAngle = 30f;
			this.ascStandardGasPress.MajorTickCount = 7;
			this.ascStandardGasPress.MajorTickmark.AllowTickOverlap = true;
			this.ascStandardGasPress.MajorTickmark.FormatString = "{0:F0}";
			this.ascStandardGasPress.MajorTickmark.ShapeOffset = -9f;
			this.ascStandardGasPress.MajorTickmark.ShapeType = DevExpress.XtraGauges.Core.Model.TickmarkShapeType.Circular_Style2_2;
			this.ascStandardGasPress.MajorTickmark.TextOffset = -22f;
			this.ascStandardGasPress.MajorTickmark.TextOrientation = DevExpress.XtraGauges.Core.Model.LabelOrientation.LeftToRight;
			this.ascStandardGasPress.MaxValue = 150f;
			this.ascStandardGasPress.MinorTickCount = 4;
			this.ascStandardGasPress.MinorTickmark.ShapeType = DevExpress.XtraGauges.Core.Model.TickmarkShapeType.Circular_Style2_1;
			this.ascStandardGasPress.Name = "arcScaleComponent4";
			this.ascStandardGasPress.RadiusX = 91f;
			this.ascStandardGasPress.RadiusY = 91f;
			this.ascStandardGasPress.StartAngle = -210f;
			this.ascStandardGasPress.Value = 50f;
			this.labelComponent3.AllowHTMLString = true;
			this.labelComponent3.AppearanceBackground.BorderWidth = 2f;
			this.labelComponent3.AppearanceText.Font = new System.Drawing.Font("SimSun", 20f, System.Drawing.FontStyle.Bold);
			this.labelComponent3.AppearanceText.TextBrush = new DevExpress.XtraGauges.Core.Drawing.SolidBrushObject("Color:Black");
			this.labelComponent3.Name = "circularGauge1_Label1";
			this.labelComponent3.Position = new DevExpress.XtraGauges.Core.Base.PointF2D(125f, 220f);
			this.labelComponent3.Shader = new DevExpress.XtraGauges.Core.Drawing.OpacityShader("");
			this.labelComponent3.Text = "标气";
			this.labelComponent3.ZOrder = -1001;
			this.arcScaleNeedleComponent4.ArcScale = this.ascStandardGasPress;
			this.arcScaleNeedleComponent4.EndOffset = -6f;
			this.arcScaleNeedleComponent4.Name = "arcScaleNeedleComponent4";
			this.arcScaleNeedleComponent4.ShapeType = DevExpress.XtraGauges.Core.Model.NeedleShapeType.CircularFull_Style2;
			this.arcScaleNeedleComponent4.StartOffset = 9f;
			this.arcScaleNeedleComponent4.ZOrder = -50;
			this.arcScaleSpindleCapComponent4.ArcScale = this.ascStandardGasPress;
			this.arcScaleSpindleCapComponent4.Name = "arcScaleSpindleCapComponent4";
			this.arcScaleSpindleCapComponent4.ShapeType = DevExpress.XtraGauges.Core.Model.SpindleCapShapeType.CircularFull_Style2;
			this.arcScaleSpindleCapComponent4.Size = new System.Drawing.SizeF(24f, 24f);
			this.arcScaleSpindleCapComponent4.ZOrder = -100;
			xYDiagram.AxisX.Title.Text = "States";
			xYDiagram.AxisX.VisibleInPanesSerializable = "-1";
			xYDiagram.AxisY.Interlaced = true;
			xYDiagram.AxisY.NumericScaleOptions.AutoGrid = false;
			xYDiagram.AxisY.NumericScaleOptions.GridSpacing = 75.0;
			xYDiagram.AxisY.Title.Text = "Millions of Dollars";
			xYDiagram.AxisY.VisibleInPanesSerializable = "-1";
			xYDiagram.AxisY.VisualRange.Auto = false;
			xYDiagram.AxisY.VisualRange.AutoSideMargins = false;
			xYDiagram.AxisY.VisualRange.MaxValueSerializable = "466.0931";
			xYDiagram.AxisY.VisualRange.MinValueSerializable = "0";
			xYDiagram.AxisY.VisualRange.SideMarginsValue = 0.0;
			xYDiagram.AxisY.WholeRange.Auto = false;
			xYDiagram.AxisY.WholeRange.MaxValueSerializable = "466.0931";
			xYDiagram.AxisY.WholeRange.MinValueSerializable = "0";
			this.chartCH4.Diagram = xYDiagram;
			this.chartCH4.Location = new System.Drawing.Point(189, 103);
			this.chartCH4.Name = "chartCH4";
			sideBySideBarSeriesLabel.Antialiasing = true;
			sideBySideBarSeriesLabel.LineLength = 20;
			pointOptions.ValueNumericOptions.Format = DevExpress.XtraCharts.NumericFormat.FixedPoint;
			sideBySideBarSeriesLabel.PointOptions = pointOptions;
			sideBySideBarSeriesLabel.Position = DevExpress.XtraCharts.BarSeriesLabelPosition.TopInside;
			sideBySideBarSeriesLabel.ShowForZeroValues = true;
			series.Label = sideBySideBarSeriesLabel;
			series.Name = "mg/m3";
			series.Points.AddRange(seriesPoint);
			this.chartCH4.SeriesSerializable = new DevExpress.XtraCharts.Series[1] { series };
			this.chartCH4.Size = new System.Drawing.Size(178, 232);
			this.chartCH4.TabIndex = 97;
			this.chartCH4.TabStop = false;
			this.chartCH4.Click += new System.EventHandler(chartCH4_Click);
			xYDiagram2.AxisX.Title.Text = "States";
			xYDiagram2.AxisX.VisibleInPanesSerializable = "-1";
			xYDiagram2.AxisY.Interlaced = true;
			xYDiagram2.AxisY.NumericScaleOptions.AutoGrid = false;
			xYDiagram2.AxisY.NumericScaleOptions.GridSpacing = 75.0;
			xYDiagram2.AxisY.Title.Text = "Millions of Dollars";
			xYDiagram2.AxisY.VisibleInPanesSerializable = "-1";
			xYDiagram2.AxisY.VisualRange.Auto = false;
			xYDiagram2.AxisY.VisualRange.AutoSideMargins = false;
			xYDiagram2.AxisY.VisualRange.MaxValueSerializable = "466.0931";
			xYDiagram2.AxisY.VisualRange.MinValueSerializable = "0";
			xYDiagram2.AxisY.VisualRange.SideMarginsValue = 0.0;
			xYDiagram2.AxisY.WholeRange.Auto = false;
			xYDiagram2.AxisY.WholeRange.MaxValueSerializable = "466.0931";
			xYDiagram2.AxisY.WholeRange.MinValueSerializable = "0";
			this.chartTHC.Diagram = xYDiagram2;
			this.chartTHC.Location = new System.Drawing.Point(3, 103);
			this.chartTHC.Name = "chartTHC";
			sideBySideBarSeriesLabel2.Antialiasing = true;
			sideBySideBarSeriesLabel2.LineLength = 20;
			pointOptions2.ValueNumericOptions.Format = DevExpress.XtraCharts.NumericFormat.FixedPoint;
			sideBySideBarSeriesLabel2.PointOptions = pointOptions2;
			sideBySideBarSeriesLabel2.Position = DevExpress.XtraCharts.BarSeriesLabelPosition.TopInside;
			sideBySideBarSeriesLabel2.ShowForZeroValues = true;
			series2.Label = sideBySideBarSeriesLabel2;
			series2.Name = "mg/m³";
			series2.Points.AddRange(seriesPoint2);
			this.chartTHC.SeriesSerializable = new DevExpress.XtraCharts.Series[1] { series2 };
			this.chartTHC.Size = new System.Drawing.Size(178, 232);
			this.chartTHC.TabIndex = 99;
			this.chartTHC.TabStop = false;
			this.chartTHC.Click += new System.EventHandler(chartTHC_Click);
			xYDiagram3.AxisX.Title.Text = "States";
			xYDiagram3.AxisX.VisibleInPanesSerializable = "-1";
			xYDiagram3.AxisY.Interlaced = true;
			xYDiagram3.AxisY.NumericScaleOptions.AutoGrid = false;
			xYDiagram3.AxisY.NumericScaleOptions.GridSpacing = 75.0;
			xYDiagram3.AxisY.Title.Text = "Millions of Dollars";
			xYDiagram3.AxisY.VisibleInPanesSerializable = "-1";
			xYDiagram3.AxisY.VisualRange.Auto = false;
			xYDiagram3.AxisY.VisualRange.AutoSideMargins = false;
			xYDiagram3.AxisY.VisualRange.MaxValueSerializable = "466.0931";
			xYDiagram3.AxisY.VisualRange.MinValueSerializable = "0";
			xYDiagram3.AxisY.VisualRange.SideMarginsValue = 0.0;
			xYDiagram3.AxisY.WholeRange.Auto = false;
			xYDiagram3.AxisY.WholeRange.MaxValueSerializable = "466.0931";
			xYDiagram3.AxisY.WholeRange.MinValueSerializable = "0";
			this.chartNMHC.Diagram = xYDiagram3;
			this.chartNMHC.Location = new System.Drawing.Point(375, 103);
			this.chartNMHC.Name = "chartNMHC";
			sideBySideBarSeriesLabel3.Antialiasing = true;
			sideBySideBarSeriesLabel3.LineLength = 20;
			pointOptions3.ValueNumericOptions.Format = DevExpress.XtraCharts.NumericFormat.FixedPoint;
			sideBySideBarSeriesLabel3.PointOptions = pointOptions3;
			sideBySideBarSeriesLabel3.Position = DevExpress.XtraCharts.BarSeriesLabelPosition.TopInside;
			sideBySideBarSeriesLabel3.ShowForZeroValues = true;
			series3.Label = sideBySideBarSeriesLabel3;
			series3.Name = "mg/m3";
			series3.Points.AddRange(seriesPoint3);
			this.chartNMHC.SeriesSerializable = new DevExpress.XtraCharts.Series[1] { series3 };
			this.chartNMHC.Size = new System.Drawing.Size(178, 232);
			this.chartNMHC.TabIndex = 100;
			this.chartNMHC.TabStop = false;
			this.chartNMHC.Click += new System.EventHandler(chartNMHC_Click);
			this.graph2.AutoLayout = false;
			this.graph2.Gauges.AddRange(new DevExpress.XtraGauges.Base.IGauge[1] { this.linearGauge3 });
			this.graph2.Location = new System.Drawing.Point(10, 509);
			this.graph2.MaximumSize = new System.Drawing.Size(350, 350);
			this.graph2.Name = "graph2";
			this.graph2.Size = new System.Drawing.Size(267, 232);
			this.graph2.TabIndex = 104;
			this.linearGauge3.AutoSize = DevExpress.Utils.DefaultBoolean.False;
			this.linearGauge3.Bounds = new System.Drawing.Rectangle(7, 6, 251, 280);
			this.linearGauge3.Labels.AddRange(new DevExpress.XtraGauges.Win.Base.LabelComponent[6] { this.labelComponent9, this.labelComponent10, this.labelComponent11, this.labelComponent12, this.labelComponent13, this.labelComponent14 });
			this.linearGauge3.Markers.AddRange(new DevExpress.XtraGauges.Win.Gauges.Linear.LinearScaleMarkerComponent[3] { this.linearScaleMarkerComponent5, this.linearScaleMarkerComponent6, this.linearScaleMarkerComponent7 });
			this.linearGauge3.Name = "linearGauge3";
			this.linearGauge3.RangeBars.AddRange(new DevExpress.XtraGauges.Win.Gauges.Linear.LinearScaleRangeBarComponent[3] { this.linearScaleRangeBarComponent5, this.linearScaleRangeBarComponent6, this.linearScaleRangeBarComponent7 });
			this.linearGauge3.Scales.AddRange(new DevExpress.XtraGauges.Win.Gauges.Linear.LinearScaleComponent[6] { this.lSCTEnvir, this.linearScaleComponent10, this.lSCHUMEnvir, this.linearScaleComponent12, this.lSCPreEnvir, this.linearScaleComponent14 });
			this.labelComponent9.AppearanceText.Font = new System.Drawing.Font("Tahoma", 8f, System.Drawing.FontStyle.Bold);
			this.labelComponent9.AppearanceText.TextBrush = new DevExpress.XtraGauges.Core.Drawing.SolidBrushObject("Color:Black");
			this.labelComponent9.Name = "titleRevenue";
			this.labelComponent9.Position = new DevExpress.XtraGauges.Core.Base.PointF2D(0f, 12f);
			this.labelComponent9.Size = new System.Drawing.SizeF(65f, 15f);
			this.labelComponent9.Text = "温度";
			this.labelComponent9.ZOrder = -1001;
			this.labelComponent10.AppearanceText.Font = new System.Drawing.Font("Tahoma", 6f);
			this.labelComponent10.AppearanceText.TextBrush = new DevExpress.XtraGauges.Core.Drawing.SolidBrushObject("Color:Black");
			this.labelComponent10.Name = "unitRevenue";
			this.labelComponent10.Position = new DevExpress.XtraGauges.Core.Base.PointF2D(0f, 23f);
			this.labelComponent10.Size = new System.Drawing.SizeF(65f, 12f);
			this.labelComponent10.Text = "℃";
			this.labelComponent10.ZOrder = -1001;
			this.labelComponent11.AppearanceText.Font = new System.Drawing.Font("Tahoma", 8f, System.Drawing.FontStyle.Bold);
			this.labelComponent11.AppearanceText.TextBrush = new DevExpress.XtraGauges.Core.Drawing.SolidBrushObject("Color:Black");
			this.labelComponent11.Name = "titleProfit";
			this.labelComponent11.Position = new DevExpress.XtraGauges.Core.Base.PointF2D(75f, 12f);
			this.labelComponent11.Size = new System.Drawing.SizeF(65f, 15f);
			this.labelComponent11.Text = "湿度";
			this.labelComponent11.ZOrder = -1001;
			this.labelComponent12.AppearanceText.Font = new System.Drawing.Font("Tahoma", 6f);
			this.labelComponent12.AppearanceText.TextBrush = new DevExpress.XtraGauges.Core.Drawing.SolidBrushObject("Color:Black");
			this.labelComponent12.Name = "unitProfit";
			this.labelComponent12.Position = new DevExpress.XtraGauges.Core.Base.PointF2D(75f, 23f);
			this.labelComponent12.Size = new System.Drawing.SizeF(65f, 12f);
			this.labelComponent12.Text = "%";
			this.labelComponent12.ZOrder = -1001;
			this.labelComponent13.AppearanceText.Font = new System.Drawing.Font("Tahoma", 8f, System.Drawing.FontStyle.Bold);
			this.labelComponent13.AppearanceText.TextBrush = new DevExpress.XtraGauges.Core.Drawing.SolidBrushObject("Color:Black");
			this.labelComponent13.Name = "titleNewCust";
			this.labelComponent13.Position = new DevExpress.XtraGauges.Core.Base.PointF2D(150f, 12f);
			this.labelComponent13.Size = new System.Drawing.SizeF(65f, 15f);
			this.labelComponent13.Text = "大气压";
			this.labelComponent13.ZOrder = -1001;
			this.labelComponent14.AppearanceText.Font = new System.Drawing.Font("Tahoma", 6f);
			this.labelComponent14.AppearanceText.TextBrush = new DevExpress.XtraGauges.Core.Drawing.SolidBrushObject("Color:Black");
			this.labelComponent14.Name = "unitNewCust";
			this.labelComponent14.Position = new DevExpress.XtraGauges.Core.Base.PointF2D(150f, 23f);
			this.labelComponent14.Size = new System.Drawing.SizeF(65f, 12f);
			this.labelComponent14.Text = "kpa";
			this.labelComponent14.ZOrder = -1001;
			this.linearScaleMarkerComponent5.LinearScale = this.linearScaleComponent10;
			this.linearScaleMarkerComponent5.Name = "revenueMarker";
			this.linearScaleMarkerComponent5.Shader = new DevExpress.XtraGauges.Core.Drawing.StyleShader("Colors[Style1:Black;Style2:]");
			this.linearScaleMarkerComponent5.ShapeScale = new DevExpress.XtraGauges.Core.Base.FactorF2D(1.5f, 0.15f);
			this.linearScaleMarkerComponent5.ShapeType = DevExpress.XtraGauges.Core.Model.MarkerPointerShapeType.Box;
			this.linearScaleMarkerComponent5.ZOrder = -150;
			this.linearScaleComponent10.AppearanceTickmarkText.Font = new System.Drawing.Font("Tahoma", 6f);
			this.linearScaleComponent10.AppearanceTickmarkText.TextBrush = new DevExpress.XtraGauges.Core.Drawing.SolidBrushObject("Color:Black");
			this.linearScaleComponent10.CustomLogarithmicBase = 2f;
			this.linearScaleComponent10.EndPoint = new DevExpress.XtraGauges.Core.Base.PointF2D(0f, 35f);
			this.linearScaleComponent10.MajorTickCount = 7;
			this.linearScaleComponent10.MajorTickmark.ShapeOffset = 10f;
			this.linearScaleComponent10.MajorTickmark.ShapeType = DevExpress.XtraGauges.Core.Model.TickmarkShapeType.Circular_Default1;
			this.linearScaleComponent10.MajorTickmark.ShowText = false;
			this.linearScaleComponent10.MaxValue = 300f;
			this.linearScaleComponent10.MinorTickCount = 0;
			this.linearScaleComponent10.Name = "revenueComparativeMeasure";
			this.linearScaleComponent10.StartPoint = new DevExpress.XtraGauges.Core.Base.PointF2D(0f, 215f);
			this.linearScaleComponent10.Value = 250f;
			this.linearScaleMarkerComponent6.LinearScale = this.linearScaleComponent12;
			this.linearScaleMarkerComponent6.Name = "profitMarker";
			this.linearScaleMarkerComponent6.Shader = new DevExpress.XtraGauges.Core.Drawing.StyleShader("Colors[Style1:Black;Style2:]");
			this.linearScaleMarkerComponent6.ShapeScale = new DevExpress.XtraGauges.Core.Base.FactorF2D(1.5f, 0.15f);
			this.linearScaleMarkerComponent6.ShapeType = DevExpress.XtraGauges.Core.Model.MarkerPointerShapeType.Box;
			this.linearScaleMarkerComponent6.ZOrder = -150;
			this.linearScaleComponent12.AppearanceTickmarkText.Font = new System.Drawing.Font("Tahoma", 6f);
			this.linearScaleComponent12.AppearanceTickmarkText.TextBrush = new DevExpress.XtraGauges.Core.Drawing.SolidBrushObject("Color:Black");
			this.linearScaleComponent12.CustomLogarithmicBase = 2f;
			this.linearScaleComponent12.EndPoint = new DevExpress.XtraGauges.Core.Base.PointF2D(75f, 35f);
			this.linearScaleComponent12.MajorTickCount = 7;
			this.linearScaleComponent12.MajorTickmark.ShapeOffset = 10f;
			this.linearScaleComponent12.MajorTickmark.ShapeType = DevExpress.XtraGauges.Core.Model.TickmarkShapeType.Circular_Default1;
			this.linearScaleComponent12.MajorTickmark.ShowText = false;
			this.linearScaleComponent12.MaxValue = 30f;
			this.linearScaleComponent12.MinorTickCount = 0;
			this.linearScaleComponent12.Name = "profitComparativeMeasure";
			this.linearScaleComponent12.StartPoint = new DevExpress.XtraGauges.Core.Base.PointF2D(75f, 215f);
			this.linearScaleComponent12.Value = 26f;
			this.linearScaleMarkerComponent7.LinearScale = this.linearScaleComponent14;
			this.linearScaleMarkerComponent7.Name = "newCustMarker";
			this.linearScaleMarkerComponent7.Shader = new DevExpress.XtraGauges.Core.Drawing.StyleShader("Colors[Style1:Black;Style2:]");
			this.linearScaleMarkerComponent7.ShapeScale = new DevExpress.XtraGauges.Core.Base.FactorF2D(1.5f, 0.15f);
			this.linearScaleMarkerComponent7.ShapeType = DevExpress.XtraGauges.Core.Model.MarkerPointerShapeType.Box;
			this.linearScaleMarkerComponent7.ZOrder = -150;
			this.linearScaleComponent14.AppearanceTickmarkText.Font = new System.Drawing.Font("Tahoma", 6f);
			this.linearScaleComponent14.AppearanceTickmarkText.TextBrush = new DevExpress.XtraGauges.Core.Drawing.SolidBrushObject("Color:Black");
			this.linearScaleComponent14.CustomLogarithmicBase = 2f;
			this.linearScaleComponent14.EndPoint = new DevExpress.XtraGauges.Core.Base.PointF2D(150f, 35f);
			this.linearScaleComponent14.MajorTickCount = 7;
			this.linearScaleComponent14.MajorTickmark.ShapeOffset = 10f;
			this.linearScaleComponent14.MajorTickmark.ShapeType = DevExpress.XtraGauges.Core.Model.TickmarkShapeType.Circular_Default1;
			this.linearScaleComponent14.MajorTickmark.ShowText = false;
			this.linearScaleComponent14.MaxValue = 2500f;
			this.linearScaleComponent14.MinorTickCount = 0;
			this.linearScaleComponent14.Name = "newCustComparativeMeasure";
			this.linearScaleComponent14.StartPoint = new DevExpress.XtraGauges.Core.Base.PointF2D(150f, 215f);
			this.linearScaleComponent14.Value = 2100f;
			this.linearScaleRangeBarComponent5.AppearanceRangeBar.ContentBrush = new DevExpress.XtraGauges.Core.Drawing.SolidBrushObject("Color:Black");
			this.linearScaleRangeBarComponent5.EndOffset = 2f;
			this.linearScaleRangeBarComponent5.LinearScale = this.lSCTEnvir;
			this.linearScaleRangeBarComponent5.Name = "revenueRange";
			this.linearScaleRangeBarComponent5.StartOffset = -2f;
			this.linearScaleRangeBarComponent5.ZOrder = -100;
			this.lSCTEnvir.AppearanceTickmarkText.Font = new System.Drawing.Font("Tahoma", 6f);
			this.lSCTEnvir.AppearanceTickmarkText.TextBrush = new DevExpress.XtraGauges.Core.Drawing.SolidBrushObject("Color:Black");
			this.lSCTEnvir.CustomLogarithmicBase = 2f;
			this.lSCTEnvir.EndPoint = new DevExpress.XtraGauges.Core.Base.PointF2D(0f, 35f);
			scaleLabel.Name = "Label0";
			scaleLabel.TextOrientation = DevExpress.XtraGauges.Core.Model.LabelOrientation.LeftToRight;
			this.lSCTEnvir.Labels.AddRange(new DevExpress.XtraGauges.Core.Model.ILabel[1] { scaleLabel });
			this.lSCTEnvir.MajorTickCount = 7;
			this.lSCTEnvir.MajorTickmark.FormatString = "{0:F0}";
			this.lSCTEnvir.MajorTickmark.ShapeOffset = -15f;
			this.lSCTEnvir.MajorTickmark.ShapeScale = new DevExpress.XtraGauges.Core.Base.FactorF2D(0.65f, 1f);
			this.lSCTEnvir.MajorTickmark.ShapeType = DevExpress.XtraGauges.Core.Model.TickmarkShapeType.Circular_Default3;
			this.lSCTEnvir.MaxValue = 60f;
			this.lSCTEnvir.MinorTickCount = 0;
			this.lSCTEnvir.Name = "revenue";
			linearScaleRange.AppearanceRange.ContentBrush = new DevExpress.XtraGauges.Core.Drawing.SolidBrushObject("Color:#E0E0E0");
			linearScaleRange.EndThickness = 20f;
			linearScaleRange.EndValue = 60f;
			linearScaleRange.Name = "Range0";
			linearScaleRange.ShapeOffset = -10f;
			linearScaleRange.StartThickness = 20f;
			linearScaleRange.StartValue = 60f;
			linearScaleRange2.AppearanceRange.ContentBrush = new DevExpress.XtraGauges.Core.Drawing.SolidBrushObject("Color:Silver");
			linearScaleRange2.EndThickness = 20f;
			linearScaleRange2.EndValue = 62f;
			linearScaleRange2.Name = "Range1";
			linearScaleRange2.ShapeOffset = -10f;
			linearScaleRange2.StartThickness = 20f;
			this.lSCTEnvir.Ranges.AddRange(new DevExpress.XtraGauges.Core.Model.IRange[2] { linearScaleRange, linearScaleRange2 });
			this.lSCTEnvir.StartPoint = new DevExpress.XtraGauges.Core.Base.PointF2D(0f, 193f);
			this.lSCTEnvir.Value = 25.5f;
			this.linearScaleRangeBarComponent6.AppearanceRangeBar.ContentBrush = new DevExpress.XtraGauges.Core.Drawing.SolidBrushObject("Color:Black");
			this.linearScaleRangeBarComponent6.EndOffset = 2f;
			this.linearScaleRangeBarComponent6.LinearScale = this.lSCHUMEnvir;
			this.linearScaleRangeBarComponent6.Name = "profitRange";
			this.linearScaleRangeBarComponent6.StartOffset = -2f;
			this.linearScaleRangeBarComponent6.ZOrder = -100;
			this.lSCHUMEnvir.AppearanceTickmarkText.Font = new System.Drawing.Font("Tahoma", 6f);
			this.lSCHUMEnvir.AppearanceTickmarkText.TextBrush = new DevExpress.XtraGauges.Core.Drawing.SolidBrushObject("Color:Black");
			this.lSCHUMEnvir.CustomLogarithmicBase = 2f;
			this.lSCHUMEnvir.EndPoint = new DevExpress.XtraGauges.Core.Base.PointF2D(75f, 35f);
			this.lSCHUMEnvir.MajorTickCount = 7;
			this.lSCHUMEnvir.MajorTickmark.FormatString = "{0:F0}%";
			this.lSCHUMEnvir.MajorTickmark.ShapeOffset = -15f;
			this.lSCHUMEnvir.MajorTickmark.ShapeScale = new DevExpress.XtraGauges.Core.Base.FactorF2D(0.65f, 1f);
			this.lSCHUMEnvir.MajorTickmark.ShapeType = DevExpress.XtraGauges.Core.Model.TickmarkShapeType.Circular_Default3;
			this.lSCHUMEnvir.MaxValue = 100f;
			this.lSCHUMEnvir.MinorTickCount = 0;
			this.lSCHUMEnvir.Name = "profit";
			linearScaleRange3.AppearanceRange.ContentBrush = new DevExpress.XtraGauges.Core.Drawing.SolidBrushObject("Color:#E0E0E0");
			linearScaleRange3.EndThickness = 20f;
			linearScaleRange3.EndValue = 100f;
			linearScaleRange3.Name = "Range0";
			linearScaleRange3.ShapeOffset = -10f;
			linearScaleRange3.StartThickness = 20f;
			linearScaleRange4.AppearanceRange.ContentBrush = new DevExpress.XtraGauges.Core.Drawing.SolidBrushObject("Color:Silver");
			linearScaleRange4.EndThickness = 20f;
			linearScaleRange4.EndValue = 100f;
			linearScaleRange4.Name = "Range1";
			linearScaleRange4.ShapeOffset = -10f;
			linearScaleRange4.StartThickness = 20f;
			this.lSCHUMEnvir.Ranges.AddRange(new DevExpress.XtraGauges.Core.Model.IRange[2] { linearScaleRange3, linearScaleRange4 });
			this.lSCHUMEnvir.StartPoint = new DevExpress.XtraGauges.Core.Base.PointF2D(75f, 193f);
			this.lSCHUMEnvir.Value = 22f;
			this.linearScaleRangeBarComponent7.AppearanceRangeBar.ContentBrush = new DevExpress.XtraGauges.Core.Drawing.SolidBrushObject("Color:Black");
			this.linearScaleRangeBarComponent7.EndOffset = 2f;
			this.linearScaleRangeBarComponent7.LinearScale = this.lSCPreEnvir;
			this.linearScaleRangeBarComponent7.Name = "newCustRange";
			this.linearScaleRangeBarComponent7.StartOffset = -2f;
			this.linearScaleRangeBarComponent7.ZOrder = -100;
			this.lSCPreEnvir.AppearanceTickmarkText.Font = new System.Drawing.Font("Tahoma", 6f);
			this.lSCPreEnvir.AppearanceTickmarkText.TextBrush = new DevExpress.XtraGauges.Core.Drawing.SolidBrushObject("Color:Black");
			this.lSCPreEnvir.CustomLogarithmicBase = 2f;
			this.lSCPreEnvir.EndPoint = new DevExpress.XtraGauges.Core.Base.PointF2D(150f, 35f);
			this.lSCPreEnvir.MajorTickCount = 6;
			this.lSCPreEnvir.MajorTickmark.FormatString = "{0:F0}";
			this.lSCPreEnvir.MajorTickmark.ShapeOffset = -15f;
			this.lSCPreEnvir.MajorTickmark.ShapeScale = new DevExpress.XtraGauges.Core.Base.FactorF2D(0.65f, 1f);
			this.lSCPreEnvir.MajorTickmark.ShapeType = DevExpress.XtraGauges.Core.Model.TickmarkShapeType.Circular_Default3;
			this.lSCPreEnvir.MaxValue = 200f;
			this.lSCPreEnvir.MinorTickCount = 0;
			this.lSCPreEnvir.Name = "newCust";
			linearScaleRange5.AppearanceRange.ContentBrush = new DevExpress.XtraGauges.Core.Drawing.SolidBrushObject("Color:#E0E0E0");
			linearScaleRange5.EndThickness = 20f;
			linearScaleRange5.EndValue = 200f;
			linearScaleRange5.Name = "Range0";
			linearScaleRange5.ShapeOffset = -10f;
			linearScaleRange5.StartThickness = 20f;
			linearScaleRange6.AppearanceRange.ContentBrush = new DevExpress.XtraGauges.Core.Drawing.SolidBrushObject("Color:Silver");
			linearScaleRange6.EndThickness = 20f;
			linearScaleRange6.EndValue = 200f;
			linearScaleRange6.Name = "Range1";
			linearScaleRange6.ShapeOffset = -10f;
			linearScaleRange6.StartThickness = 20f;
			this.lSCPreEnvir.Ranges.AddRange(new DevExpress.XtraGauges.Core.Model.IRange[2] { linearScaleRange5, linearScaleRange6 });
			this.lSCPreEnvir.StartPoint = new DevExpress.XtraGauges.Core.Base.PointF2D(150f, 193f);
			this.lSCPreEnvir.Value = 101f;
			this.label4.Font = new System.Drawing.Font("SimSun", 15.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
			this.label4.Location = new System.Drawing.Point(283, 527);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(77, 23);
			this.label4.TabIndex = 105;
			this.label4.Text = "经度：";
			this.tbJingDu.Location = new System.Drawing.Point(357, 529);
			this.tbJingDu.Name = "tbJingDu";
			this.tbJingDu.Size = new System.Drawing.Size(204, 21);
			this.tbJingDu.TabIndex = 106;
			this.tbWeiDu.Location = new System.Drawing.Point(357, 560);
			this.tbWeiDu.Name = "tbWeiDu";
			this.tbWeiDu.Size = new System.Drawing.Size(204, 21);
			this.tbWeiDu.TabIndex = 108;
			this.label5.Font = new System.Drawing.Font("SimSun", 15.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
			this.label5.Location = new System.Drawing.Point(283, 558);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(77, 23);
			this.label5.TabIndex = 107;
			this.label5.Text = "纬度：";
			this.bStartAnalyze.Font = new System.Drawing.Font("SimSun", 26.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
			this.bStartAnalyze.Location = new System.Drawing.Point(3, 9);
			this.bStartAnalyze.Name = "bStartAnalyze";
			this.bStartAnalyze.Size = new System.Drawing.Size(178, 91);
			this.bStartAnalyze.TabIndex = 109;
			this.bStartAnalyze.Text = "开始运行";
			this.bStartAnalyze.UseVisualStyleBackColor = true;
			this.bStartAnalyze.Click += new System.EventHandler(BtnStart_Click);
			this.btnEnvirPollu.Location = new System.Drawing.Point(287, 685);
			this.btnEnvirPollu.Name = "btnEnvirPollu";
			this.btnEnvirPollu.Size = new System.Drawing.Size(135, 45);
			this.btnEnvirPollu.TabIndex = 110;
			this.btnEnvirPollu.Text = "环境空气测定";
			this.btnEnvirPollu.UseVisualStyleBackColor = true;
			this.btnEnvirPollu.Click += new System.EventHandler(btnEnvirPollu_Click);
			this.btnFixPollu.Location = new System.Drawing.Point(287, 629);
			this.btnFixPollu.Name = "btnFixPollu";
			this.btnFixPollu.Size = new System.Drawing.Size(135, 45);
			this.btnFixPollu.TabIndex = 112;
			this.btnFixPollu.Text = "固定污染源废气测定";
			this.btnFixPollu.UseVisualStyleBackColor = true;
			this.btnFixPollu.Click += new System.EventHandler(btnFixPollu_Click);
			this.btnTemp.Font = new System.Drawing.Font("SimSun", 26.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
			this.btnTemp.Location = new System.Drawing.Point(244, 9);
			this.btnTemp.Name = "btnTemp";
			this.btnTemp.Size = new System.Drawing.Size(178, 91);
			this.btnTemp.TabIndex = 111;
			this.btnTemp.Text = "开始控温";
			this.btnTemp.UseVisualStyleBackColor = true;
			this.btnTemp.Click += new System.EventHandler(BtnTemp_Click);
			this.spChrom.Location = new System.Drawing.Point(579, 9);
			this.spChrom.Name = "spChrom";
			this.spChrom.Panel1.Controls.Add(this.label49);
			this.spChrom.Panel1.Controls.Add(this.pictureBox1);
			this.spChrom.Panel1.Controls.Add(this.label48);
			this.spChrom.Panel1.Controls.Add(this.label47);
			this.spChrom.Panel1.Controls.Add(this.label30);
			this.spChrom.Panel1.Controls.Add(this.label11);
			this.spChrom.Panel1.Controls.Add(this.label46);
			this.spChrom.Panel1.Controls.Add(this.label16);
			this.spChrom.Panel1.Controls.Add(this.label6);
			this.spChrom.Panel1.Controls.Add(this.labAnyTimes);
			this.spChrom.Panel1.Controls.Add(this.label18);
			this.spChrom.Panel1.Controls.Add(this.label15);
			this.spChrom.Panel1.Controls.Add(this.label14);
			this.spChrom.Panel1.Controls.Add(this.label13);
			this.spChrom.Panel1.Controls.Add(this.labAirCur);
			this.spChrom.Panel1.Controls.Add(this.labHHCur);
			this.spChrom.Panel1.Controls.Add(this.label10);
			this.spChrom.Panel1.Controls.Add(this.labCYGXCur);
			this.spChrom.Panel1.Controls.Add(this.labZQCur);
			this.spChrom.Panel1.Controls.Add(this.labCHSCur);
			this.spChrom.Panel1.Controls.Add(this.labFaXiangCur);
			this.spChrom.Panel1.Controls.Add(this.labDecCur);
			this.spChrom.Size = new System.Drawing.Size(663, 342);
			this.spChrom.SplitterDistance = 234;
			this.spChrom.TabIndex = 113;
			this.label49.AutoSize = true;
			this.label49.Location = new System.Drawing.Point(215, 41);
			this.label49.Name = "label49";
			this.label49.Size = new System.Drawing.Size(29, 12);
			this.label49.TabIndex = 103;
			this.label49.Text = "准备";
			this.label49.Visible = false;
			this.pictureBox1.Image = (System.Drawing.Image)resources.GetObject("pictureBox1.Image");
			this.pictureBox1.Location = new System.Drawing.Point(226, 17);
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.Size = new System.Drawing.Size(20, 20);
			this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
			this.pictureBox1.TabIndex = 102;
			this.pictureBox1.TabStop = false;
			this.pictureBox1.Visible = false;
			this.label48.AutoSize = true;
			this.label48.Location = new System.Drawing.Point(8, 138);
			this.label48.Name = "label48";
			this.label48.Size = new System.Drawing.Size(41, 12);
			this.label48.TabIndex = 101;
			this.label48.Text = "储氢器";
			this.label47.AutoSize = true;
			this.label47.Location = new System.Drawing.Point(8, 104);
			this.label47.Name = "label47";
			this.label47.Size = new System.Drawing.Size(41, 12);
			this.label47.TabIndex = 100;
			this.label47.Text = "取样枪";
			this.label30.AutoSize = true;
			this.label30.Location = new System.Drawing.Point(8, 40);
			this.label30.Name = "label30";
			this.label30.Size = new System.Drawing.Size(41, 12);
			this.label30.TabIndex = 99;
			this.label30.Text = "催化器";
			this.label11.AutoSize = true;
			this.label11.Location = new System.Drawing.Point(8, 70);
			this.label11.Name = "label11";
			this.label11.Size = new System.Drawing.Size(41, 12);
			this.label11.TabIndex = 98;
			this.label11.Text = "检测器";
			this.label46.AutoSize = true;
			this.label46.Location = new System.Drawing.Point(8, 232);
			this.label46.Name = "label46";
			this.label46.Size = new System.Drawing.Size(29, 12);
			this.label46.TabIndex = 97;
			this.label46.Text = "空气";
			this.label16.AutoSize = true;
			this.label16.Location = new System.Drawing.Point(8, 199);
			this.label16.Name = "label16";
			this.label16.Size = new System.Drawing.Size(29, 12);
			this.label16.TabIndex = 96;
			this.label16.Text = "氢气";
			this.label6.AutoSize = true;
			this.label6.Location = new System.Drawing.Point(8, 169);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(29, 12);
			this.label6.TabIndex = 95;
			this.label6.Text = "载气";
			this.labAnyTimes.AutoSize = true;
			this.labAnyTimes.Cursor = System.Windows.Forms.Cursors.Hand;
			this.labAnyTimes.Location = new System.Drawing.Point(136, 303);
			this.labAnyTimes.Name = "labAnyTimes";
			this.labAnyTimes.Size = new System.Drawing.Size(11, 12);
			this.labAnyTimes.TabIndex = 94;
			this.labAnyTimes.Text = "0";
			this.label18.AutoSize = true;
			this.label18.Font = new System.Drawing.Font("SimSun", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
			this.label18.Location = new System.Drawing.Point(90, 306);
			this.label18.Name = "label18";
			this.label18.Size = new System.Drawing.Size(40, 16);
			this.label18.TabIndex = 93;
			this.label18.Text = "样品";
			this.label15.AutoSize = true;
			this.label15.Cursor = System.Windows.Forms.Cursors.Hand;
			this.label15.Location = new System.Drawing.Point(55, 305);
			this.label15.Name = "label15";
			this.label15.Size = new System.Drawing.Size(11, 12);
			this.label15.TabIndex = 92;
			this.label15.Text = "0";
			this.label14.AutoSize = true;
			this.label14.Font = new System.Drawing.Font("SimSun", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
			this.label14.Location = new System.Drawing.Point(8, 308);
			this.label14.Name = "label14";
			this.label14.Size = new System.Drawing.Size(40, 16);
			this.label14.TabIndex = 91;
			this.label14.Text = "校准";
			this.label13.AutoSize = true;
			this.label13.Location = new System.Drawing.Point(8, 268);
			this.label13.Name = "label13";
			this.label13.Size = new System.Drawing.Size(53, 12);
			this.label13.TabIndex = 90;
			this.label13.Text = "分析次数";
			this.label10.AutoSize = true;
			this.label10.Font = new System.Drawing.Font("SimSun", 15.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 134);
			this.label10.Location = new System.Drawing.Point(20, 7);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(98, 21);
			this.label10.TabIndex = 88;
			this.label10.Text = "色谱条件";
			this.groupBox8.Controls.Add(this.tbCollectJCXM);
			this.groupBox8.Controls.Add(this.tbCollectJYDW);
			this.groupBox8.Controls.Add(this.groupBox2);
			this.groupBox8.Controls.Add(this.btnStartCalibra);
			this.groupBox8.Controls.Add(this.groupBox9);
			this.groupBox8.Controls.Add(this.tbFireOn2);
			this.groupBox8.Controls.Add(this.tbCollectSJDW);
			this.groupBox8.Controls.Add(this.tbCollectP);
			this.groupBox8.Controls.Add(this.tbCollectBH);
			this.groupBox8.Controls.Add(this.label34);
			this.groupBox8.Controls.Add(this.tbCollectSite);
			this.groupBox8.Controls.Add(this.label29);
			this.groupBox8.Controls.Add(this.label28);
			this.groupBox8.Controls.Add(this.label27);
			this.groupBox8.Controls.Add(this.label26);
			this.groupBox8.Controls.Add(this.label25);
			this.groupBox8.Location = new System.Drawing.Point(579, 352);
			this.groupBox8.Name = "groupBox8";
			this.groupBox8.Size = new System.Drawing.Size(669, 341);
			this.groupBox8.TabIndex = 115;
			this.groupBox8.TabStop = false;
			this.tbCollectJCXM.Location = new System.Drawing.Point(138, 120);
			this.tbCollectJCXM.Name = "tbCollectJCXM";
			this.tbCollectJCXM.Size = new System.Drawing.Size(467, 21);
			this.tbCollectJCXM.TabIndex = 29;
			this.tbCollectJYDW.Location = new System.Drawing.Point(138, 157);
			this.tbCollectJYDW.Name = "tbCollectJYDW";
			this.tbCollectJYDW.Size = new System.Drawing.Size(467, 21);
			this.tbCollectJYDW.TabIndex = 27;
			this.btnStartCalibra.Location = new System.Drawing.Point(497, 226);
			this.btnStartCalibra.Name = "btnStartCalibra";
			this.btnStartCalibra.Size = new System.Drawing.Size(166, 111);
			this.btnStartCalibra.TabIndex = 106;
			this.btnStartCalibra.Text = "设定";
			this.btnStartCalibra.UseVisualStyleBackColor = true;
			this.btnStartCalibra.Click += new System.EventHandler(BtnStartCalibra_Click);
			this.groupBox9.Controls.Add(this.label35);
			this.groupBox9.Controls.Add(this.label36);
			this.groupBox9.Controls.Add(this.tbIntervalTime);
			this.groupBox9.Controls.Add(this.label37);
			this.groupBox9.Controls.Add(this.tbCollectTime);
			this.groupBox9.Controls.Add(this.label38);
			this.groupBox9.Controls.Add(this.tbCollectTimes);
			this.groupBox9.Controls.Add(this.label45);
			this.groupBox9.Controls.Add(this.rad60Min);
			this.groupBox9.Controls.Add(this.radFive);
			this.groupBox9.Controls.Add(this.radOne);
			this.groupBox9.Location = new System.Drawing.Point(25, 221);
			this.groupBox9.Name = "groupBox9";
			this.groupBox9.Size = new System.Drawing.Size(462, 117);
			this.groupBox9.TabIndex = 105;
			this.groupBox9.TabStop = false;
			this.groupBox9.Text = "采样模式";
			this.label35.AutoSize = true;
			this.label35.Location = new System.Drawing.Point(404, 88);
			this.label35.Name = "label35";
			this.label35.Size = new System.Drawing.Size(23, 12);
			this.label35.TabIndex = 11;
			this.label35.Text = "min";
			this.label36.AutoSize = true;
			this.label36.Location = new System.Drawing.Point(230, 88);
			this.label36.Name = "label36";
			this.label36.Size = new System.Drawing.Size(23, 12);
			this.label36.TabIndex = 10;
			this.label36.Text = "min";
			this.tbIntervalTime.Location = new System.Drawing.Point(328, 81);
			this.tbIntervalTime.Name = "tbIntervalTime";
			this.tbIntervalTime.Size = new System.Drawing.Size(70, 21);
			this.tbIntervalTime.TabIndex = 9;
			this.label37.AutoSize = true;
			this.label37.Location = new System.Drawing.Point(257, 88);
			this.label37.Name = "label37";
			this.label37.Size = new System.Drawing.Size(65, 12);
			this.label37.TabIndex = 8;
			this.label37.Text = "间隔时间：";
			this.tbCollectTime.Location = new System.Drawing.Point(154, 81);
			this.tbCollectTime.Name = "tbCollectTime";
			this.tbCollectTime.Size = new System.Drawing.Size(70, 21);
			this.tbCollectTime.TabIndex = 7;
			this.label38.AutoSize = true;
			this.label38.Location = new System.Drawing.Point(83, 88);
			this.label38.Name = "label38";
			this.label38.Size = new System.Drawing.Size(65, 12);
			this.label38.TabIndex = 6;
			this.label38.Text = "采样时间：";
			this.tbCollectTimes.Location = new System.Drawing.Point(154, 46);
			this.tbCollectTimes.Name = "tbCollectTimes";
			this.tbCollectTimes.Size = new System.Drawing.Size(70, 21);
			this.tbCollectTimes.TabIndex = 5;
			this.label45.AutoSize = true;
			this.label45.Location = new System.Drawing.Point(83, 53);
			this.label45.Name = "label45";
			this.label45.Size = new System.Drawing.Size(65, 12);
			this.label45.TabIndex = 4;
			this.label45.Text = "采样次数：";
			this.rad60Min.AutoSize = true;
			this.rad60Min.Location = new System.Drawing.Point(6, 86);
			this.rad60Min.Name = "rad60Min";
			this.rad60Min.Size = new System.Drawing.Size(71, 16);
			this.rad60Min.TabIndex = 3;
			this.rad60Min.TabStop = true;
			this.rad60Min.Text = "时间采样";
			this.rad60Min.UseVisualStyleBackColor = true;
			this.radFive.AutoSize = true;
			this.radFive.Location = new System.Drawing.Point(6, 51);
			this.radFive.Name = "radFive";
			this.radFive.Size = new System.Drawing.Size(71, 16);
			this.radFive.TabIndex = 2;
			this.radFive.TabStop = true;
			this.radFive.Text = "多次采样";
			this.radFive.UseVisualStyleBackColor = true;
			this.radOne.AutoSize = true;
			this.radOne.Location = new System.Drawing.Point(6, 20);
			this.radOne.Name = "radOne";
			this.radOne.Size = new System.Drawing.Size(71, 16);
			this.radOne.TabIndex = 1;
			this.radOne.TabStop = true;
			this.radOne.Text = "单次采样";
			this.radOne.UseVisualStyleBackColor = true;
			this.tbFireOn2.Location = new System.Drawing.Point(604, 184);
			this.tbFireOn2.Name = "tbFireOn2";
			this.tbFireOn2.Size = new System.Drawing.Size(62, 21);
			this.tbFireOn2.TabIndex = 91;
			this.tbFireOn2.Visible = false;
			this.tbCollectSJDW.Location = new System.Drawing.Point(138, 52);
			this.tbCollectSJDW.Name = "tbCollectSJDW";
			this.tbCollectSJDW.Size = new System.Drawing.Size(467, 21);
			this.tbCollectSJDW.TabIndex = 25;
			this.tbCollectP.Location = new System.Drawing.Point(138, 196);
			this.tbCollectP.Name = "tbCollectP";
			this.tbCollectP.Size = new System.Drawing.Size(467, 21);
			this.tbCollectP.TabIndex = 19;
			this.tbCollectBH.Location = new System.Drawing.Point(138, 20);
			this.tbCollectBH.Name = "tbCollectBH";
			this.tbCollectBH.Size = new System.Drawing.Size(467, 21);
			this.tbCollectBH.TabIndex = 23;
			this.label34.AutoSize = true;
			this.label34.Font = new System.Drawing.Font("SimSun", 15.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
			this.label34.Location = new System.Drawing.Point(21, 157);
			this.label34.Name = "label34";
			this.label34.Size = new System.Drawing.Size(115, 21);
			this.label34.TabIndex = 97;
			this.label34.Text = "检测单位：";
			this.tbCollectSite.Location = new System.Drawing.Point(138, 85);
			this.tbCollectSite.Name = "tbCollectSite";
			this.tbCollectSite.Size = new System.Drawing.Size(467, 21);
			this.tbCollectSite.TabIndex = 17;
			this.label29.AutoSize = true;
			this.label29.Font = new System.Drawing.Font("SimSun", 15.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
			this.label29.Location = new System.Drawing.Point(21, 120);
			this.label29.Name = "label29";
			this.label29.Size = new System.Drawing.Size(115, 21);
			this.label29.TabIndex = 95;
			this.label29.Text = "检测项目：";
			this.label28.AutoSize = true;
			this.label28.Font = new System.Drawing.Font("SimSun", 15.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
			this.label28.Location = new System.Drawing.Point(21, 85);
			this.label28.Name = "label28";
			this.label28.Size = new System.Drawing.Size(115, 21);
			this.label28.TabIndex = 94;
			this.label28.Text = "采样地点：";
			this.label27.AutoSize = true;
			this.label27.Font = new System.Drawing.Font("SimSun", 15.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
			this.label27.Location = new System.Drawing.Point(21, 196);
			this.label27.Name = "label27";
			this.label27.Size = new System.Drawing.Size(115, 21);
			this.label27.TabIndex = 93;
			this.label27.Text = "采样人员：";
			this.label26.AutoSize = true;
			this.label26.Font = new System.Drawing.Font("SimSun", 15.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
			this.label26.Location = new System.Drawing.Point(20, 52);
			this.label26.Name = "label26";
			this.label26.Size = new System.Drawing.Size(115, 21);
			this.label26.TabIndex = 92;
			this.label26.Text = "样品名称：";
			this.label25.AutoSize = true;
			this.label25.Font = new System.Drawing.Font("SimSun", 15.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
			this.label25.Location = new System.Drawing.Point(20, 18);
			this.label25.Name = "label25";
			this.label25.Size = new System.Drawing.Size(115, 21);
			this.label25.TabIndex = 91;
			this.label25.Text = "仪器编号：";
			this.timer1.Tick += new System.EventHandler(Timer1_Tick);
			this.panel1.Controls.Add(this.ucBtnHistory);
			this.panel1.Controls.Add(this.btShowDesktop);
			this.panel1.Controls.Add(this.btnPoweroff);
			this.panel1.Controls.Add(this.btnNetConfig);
			this.panel1.Controls.Add(this.cbKindMachine);
			this.panel1.Controls.Add(this.btnFireOnCheck);
			this.panel1.Controls.Add(this.tbFireOn);
			this.panel1.Controls.Add(this.btnFireOnSet);
			this.panel1.Location = new System.Drawing.Point(579, 696);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(669, 44);
			this.panel1.TabIndex = 116;
			this.ucBtnHistory.BackColor = System.Drawing.Color.White;
			this.ucBtnHistory.BtnBackColor = System.Drawing.Color.White;
			this.ucBtnHistory.BtnFont = new System.Drawing.Font("Microsoft YaHei", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
			this.ucBtnHistory.BtnForeColor = System.Drawing.Color.White;
			this.ucBtnHistory.BtnText = "查看数据";
			this.ucBtnHistory.ConerRadius = 5;
			this.ucBtnHistory.Cursor = System.Windows.Forms.Cursors.Hand;
			this.ucBtnHistory.EnabledMouseEffect = false;
			this.ucBtnHistory.FillColor = System.Drawing.Color.FromArgb(255, 77, 59);
			this.ucBtnHistory.Font = new System.Drawing.Font("Microsoft YaHei", 15f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
			this.ucBtnHistory.IsRadius = true;
			this.ucBtnHistory.IsShowRect = true;
			this.ucBtnHistory.IsShowTips = false;
			this.ucBtnHistory.Location = new System.Drawing.Point(3, 6);
			this.ucBtnHistory.Margin = new System.Windows.Forms.Padding(0);
			this.ucBtnHistory.Name = "ucBtnHistory";
			this.ucBtnHistory.RectColor = System.Drawing.Color.FromArgb(255, 77, 58);
			this.ucBtnHistory.RectWidth = 1;
			this.ucBtnHistory.Size = new System.Drawing.Size(99, 34);
			this.ucBtnHistory.TabIndex = 98;
			this.ucBtnHistory.TabStop = false;
			this.ucBtnHistory.TipsColor = System.Drawing.Color.FromArgb(232, 30, 99);
			this.ucBtnHistory.TipsText = "";
			this.ucBtnHistory.BtnClick += new System.EventHandler(ucBtnHistory_BtnClick);
			this.btShowDesktop.Location = new System.Drawing.Point(109, 4);
			this.btShowDesktop.Name = "btShowDesktop";
			this.btShowDesktop.Size = new System.Drawing.Size(76, 36);
			this.btShowDesktop.TabIndex = 97;
			this.btShowDesktop.Text = "显示桌面";
			this.btShowDesktop.UseVisualStyleBackColor = true;
			this.btShowDesktop.Click += new System.EventHandler(btShowDesktop_Click);
			this.btnPoweroff.Location = new System.Drawing.Point(189, 4);
			this.btnPoweroff.Name = "btnPoweroff";
			this.btnPoweroff.Size = new System.Drawing.Size(106, 36);
			this.btnPoweroff.TabIndex = 96;
			this.btnPoweroff.Text = "关机";
			this.btnPoweroff.UseVisualStyleBackColor = true;
			this.btnPoweroff.Click += new System.EventHandler(btnPoweroff_Click);
			this.btnNetConfig.Location = new System.Drawing.Point(299, 4);
			this.btnNetConfig.Name = "btnNetConfig";
			this.btnNetConfig.Size = new System.Drawing.Size(106, 36);
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
			this.btnFireOnCheck.Location = new System.Drawing.Point(407, 4);
			this.btnFireOnCheck.Name = "btnFireOnCheck";
			this.btnFireOnCheck.Size = new System.Drawing.Size(94, 36);
			this.btnFireOnCheck.TabIndex = 89;
			this.btnFireOnCheck.Text = "点火门限查询";
			this.btnFireOnCheck.UseVisualStyleBackColor = true;
			this.btnFireOnCheck.Click += new System.EventHandler(btnFireOnCheck_Click);
			this.tbFireOn.Location = new System.Drawing.Point(604, 6);
			this.tbFireOn.Name = "tbFireOn";
			this.tbFireOn.Size = new System.Drawing.Size(61, 21);
			this.tbFireOn.TabIndex = 87;
			this.btnFireOnSet.Location = new System.Drawing.Point(507, 4);
			this.btnFireOnSet.Name = "btnFireOnSet";
			this.btnFireOnSet.Size = new System.Drawing.Size(91, 36);
			this.btnFireOnSet.TabIndex = 90;
			this.btnFireOnSet.Text = "点火门限设定";
			this.btnFireOnSet.UseVisualStyleBackColor = true;
			this.btnFireOnSet.Click += new System.EventHandler(btnFireOnSet_Click);
			this.button1.Image = (System.Drawing.Image)resources.GetObject("button1.Image");
			this.button1.Location = new System.Drawing.Point(471, 9);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(90, 91);
			this.button1.TabIndex = 95;
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Click += new System.EventHandler(button1_Click);
			this.btnAdvancedParameters.Location = new System.Drawing.Point(428, 629);
			this.btnAdvancedParameters.Name = "btnAdvancedParameters";
			this.btnAdvancedParameters.Size = new System.Drawing.Size(131, 45);
			this.btnAdvancedParameters.TabIndex = 117;
			this.btnAdvancedParameters.Text = "高级参数";
			this.btnAdvancedParameters.UseVisualStyleBackColor = true;
			this.btnAdvancedParameters.Click += new System.EventHandler(btnAdvancedParameters_Click);
			this.cbRunMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbRunMode.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.cbRunMode.Font = new System.Drawing.Font("SimSun", 15f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
			this.cbRunMode.FormattingEnabled = true;
			this.cbRunMode.Items.AddRange(new object[3] { "正常采样", "气路吹扫", "催化剂活化" });
			this.cbRunMode.Location = new System.Drawing.Point(428, 685);
			this.cbRunMode.Name = "cbRunMode";
			this.cbRunMode.Size = new System.Drawing.Size(131, 28);
			this.cbRunMode.TabIndex = 118;
			this.cbRunMode.SelectedIndexChanged += new System.EventHandler(cbRunMode_SelectedIndexChanged);
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
			this.label50.Font = new System.Drawing.Font("SimSun", 15.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
			this.label50.Location = new System.Drawing.Point(283, 595);
			this.label50.Name = "label50";
			this.label50.Size = new System.Drawing.Size(97, 23);
			this.label50.TabIndex = 119;
			this.label50.Text = "卫星数：";
			this.tbSatellite.Location = new System.Drawing.Point(375, 597);
			this.tbSatellite.Name = "tbSatellite";
			this.tbSatellite.Size = new System.Drawing.Size(184, 21);
			this.tbSatellite.TabIndex = 120;
			this.timer2.Enabled = true;
			this.timer2.Interval = 1000;
			this.timer2.Tick += new System.EventHandler(timer2_Tick);
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.Controls.Add(this.graph2);
			base.Controls.Add(this.spChrom);
			base.Controls.Add(this.tbSatellite);
			base.Controls.Add(this.label50);
			base.Controls.Add(this.cbRunMode);
			base.Controls.Add(this.btnAdvancedParameters);
			base.Controls.Add(this.button1);
			base.Controls.Add(this.panel1);
			base.Controls.Add(this.groupBox8);
			base.Controls.Add(this.btnTemp);
			base.Controls.Add(this.btnEnvirPollu);
			base.Controls.Add(this.btnFixPollu);
			base.Controls.Add(this.bStartAnalyze);
			base.Controls.Add(this.tbWeiDu);
			base.Controls.Add(this.label5);
			base.Controls.Add(this.tbJingDu);
			base.Controls.Add(this.label4);
			base.Controls.Add(this.chartNMHC);
			base.Controls.Add(this.chartTHC);
			base.Controls.Add(this.chartCH4);
			base.Controls.Add(this.gaugeControl3);
			base.Controls.Add(this.gaugeControl2);
			base.Controls.Add(this.gaugeControl1);
			base.Name = "LYTHCtrl2";
			base.Size = new System.Drawing.Size(1280, 741);
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.groupBox2.ResumeLayout(false);
			this.groupBox2.PerformLayout();
			this.groupBox4.ResumeLayout(false);
			this.groupBox4.PerformLayout();
			this.groupBox5.ResumeLayout(false);
			this.groupBox5.PerformLayout();
			this.groupBox6.ResumeLayout(false);
			this.groupBox6.PerformLayout();
			this.groupBox3.ResumeLayout(false);
			this.groupBox3.PerformLayout();
			((System.ComponentModel.ISupportInitialize)this.arcScaleBackgroundLayerComponent2).EndInit();
			((System.ComponentModel.ISupportInitialize)this.arcScaleComponent2).EndInit();
			((System.ComponentModel.ISupportInitialize)this.arcScaleNeedleComponent2).EndInit();
			((System.ComponentModel.ISupportInitialize)this.arcScaleSpindleCapComponent2).EndInit();
			((System.ComponentModel.ISupportInitialize)this.circularGauge1).EndInit();
			((System.ComponentModel.ISupportInitialize)this.arcScaleBackgroundLayerComponent1).EndInit();
			((System.ComponentModel.ISupportInitialize)this.ascZeroGasPress).EndInit();
			((System.ComponentModel.ISupportInitialize)this.labelComponent2).EndInit();
			((System.ComponentModel.ISupportInitialize)this.arcScaleNeedleComponent1).EndInit();
			((System.ComponentModel.ISupportInitialize)this.arcScaleSpindleCapComponent1).EndInit();
			((System.ComponentModel.ISupportInitialize)this.circularGauge2).EndInit();
			((System.ComponentModel.ISupportInitialize)this.arcScaleBackgroundLayerComponent3).EndInit();
			((System.ComponentModel.ISupportInitialize)this.ascHHPress).EndInit();
			((System.ComponentModel.ISupportInitialize)this.labelComponent1).EndInit();
			((System.ComponentModel.ISupportInitialize)this.arcScaleNeedleComponent3).EndInit();
			((System.ComponentModel.ISupportInitialize)this.arcScaleSpindleCapComponent3).EndInit();
			((System.ComponentModel.ISupportInitialize)this.circularGauge3).EndInit();
			((System.ComponentModel.ISupportInitialize)this.arcScaleBackgroundLayerComponent4).EndInit();
			((System.ComponentModel.ISupportInitialize)this.ascStandardGasPress).EndInit();
			((System.ComponentModel.ISupportInitialize)this.labelComponent3).EndInit();
			((System.ComponentModel.ISupportInitialize)this.arcScaleNeedleComponent4).EndInit();
			((System.ComponentModel.ISupportInitialize)this.arcScaleSpindleCapComponent4).EndInit();
			((System.ComponentModel.ISupportInitialize)xYDiagram).EndInit();
			((System.ComponentModel.ISupportInitialize)sideBySideBarSeriesLabel).EndInit();
			((System.ComponentModel.ISupportInitialize)series).EndInit();
			((System.ComponentModel.ISupportInitialize)this.chartCH4).EndInit();
			((System.ComponentModel.ISupportInitialize)xYDiagram2).EndInit();
			((System.ComponentModel.ISupportInitialize)sideBySideBarSeriesLabel2).EndInit();
			((System.ComponentModel.ISupportInitialize)series2).EndInit();
			((System.ComponentModel.ISupportInitialize)this.chartTHC).EndInit();
			((System.ComponentModel.ISupportInitialize)xYDiagram3).EndInit();
			((System.ComponentModel.ISupportInitialize)sideBySideBarSeriesLabel3).EndInit();
			((System.ComponentModel.ISupportInitialize)series3).EndInit();
			((System.ComponentModel.ISupportInitialize)this.chartNMHC).EndInit();
			((System.ComponentModel.ISupportInitialize)this.linearGauge3).EndInit();
			((System.ComponentModel.ISupportInitialize)this.labelComponent9).EndInit();
			((System.ComponentModel.ISupportInitialize)this.labelComponent10).EndInit();
			((System.ComponentModel.ISupportInitialize)this.labelComponent11).EndInit();
			((System.ComponentModel.ISupportInitialize)this.labelComponent12).EndInit();
			((System.ComponentModel.ISupportInitialize)this.labelComponent13).EndInit();
			((System.ComponentModel.ISupportInitialize)this.labelComponent14).EndInit();
			((System.ComponentModel.ISupportInitialize)this.linearScaleMarkerComponent5).EndInit();
			((System.ComponentModel.ISupportInitialize)this.linearScaleComponent10).EndInit();
			((System.ComponentModel.ISupportInitialize)this.linearScaleMarkerComponent6).EndInit();
			((System.ComponentModel.ISupportInitialize)this.linearScaleComponent12).EndInit();
			((System.ComponentModel.ISupportInitialize)this.linearScaleMarkerComponent7).EndInit();
			((System.ComponentModel.ISupportInitialize)this.linearScaleComponent14).EndInit();
			((System.ComponentModel.ISupportInitialize)this.linearScaleRangeBarComponent5).EndInit();
			((System.ComponentModel.ISupportInitialize)this.lSCTEnvir).EndInit();
			((System.ComponentModel.ISupportInitialize)this.linearScaleRangeBarComponent6).EndInit();
			((System.ComponentModel.ISupportInitialize)this.lSCHUMEnvir).EndInit();
			((System.ComponentModel.ISupportInitialize)this.linearScaleRangeBarComponent7).EndInit();
			((System.ComponentModel.ISupportInitialize)this.lSCPreEnvir).EndInit();
			this.spChrom.Panel1.ResumeLayout(false);
			this.spChrom.Panel1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)this.spChrom).EndInit();
			this.spChrom.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)this.pictureBox1).EndInit();
			this.groupBox8.ResumeLayout(false);
			this.groupBox8.PerformLayout();
			this.groupBox9.ResumeLayout(false);
			this.groupBox9.PerformLayout();
			this.panel1.ResumeLayout(false);
			this.panel1.PerformLayout();
			base.ResumeLayout(false);
			base.PerformLayout();
		}
	}
}
