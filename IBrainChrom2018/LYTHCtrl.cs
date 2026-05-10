using System;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using IBrainChrom2018.Unit;

namespace IBrainChrom2018;

public class LYTHCtrl : UserControl
{
	public static LYTHCtrl selfCtrl;

	private ChromDeviceListMgr cdlMgr = ChromDeviceListMgr.Create();

	private LYTHCPara lythcParamMgr = LYTHCPara.Create();

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

	private IContainer components = null;

	private Label label79;

	private Label label77;

	private GroupBox groupBox1;

	private GroupBox groupBox2;

	private Label label1;

	private Label label2;

	private Label label3;

	private GroupBox groupBox3;

	private Label label8;

	private Label label9;

	private Label label12;

	private GroupBox groupBox5;

	private GroupBox groupBox6;

	private Label label32;

	private Label label31;

	private Label label33;

	private Label label39;

	private GroupBox groupBox4;

	private Label label17;

	private Label label40;

	private Label label41;

	private Label label42;

	private Label label43;

	private Label label44;

	public TextBox tbTimeHuoHua;

	public Label label22;

	public Label label23;

	public TextBox tbTempHuoHua;

	public Button btnHuoHua;

	public Label label19;

	public Label label20;

	public Label labHumiEnvirCur;

	public Label labLatitudeCur;

	public Label labLongitudeCur;

	public Label labCH4Rlt;

	public Label labTHCRlt;

	public Label lbNMHCT;

	public Label labNMHCRlt;

	public Label labFaXiangSet;

	public Label labCHSSet;

	public Label labDecSet;

	public Label label7;

	public Label labTempEnvirCur;

	public Label labBaroEnvirCur;

	public Label label21;

	public Label label24;

	public Label labCYGXCur;

	public Label labFaXiangCur;

	public Label labCHSCur;

	public Label labDecCur;

	public Label labCYGXSet;

	public Label labSampleCur;

	public Label labHHCur;

	public Label labAirCur;

	public Label labZQCur;

	public Label labSampleSet;

	public Label labHHSet;

	public Label labAirSet;

	public Label labZQSet;

	public Button btnChuiSao;

	public TextBox tbTimeChuiSao;

	public LYTHCtrl()
	{
		selfCtrl = this;
		InitializeComponent();
	}

	public void stateSwitch()
	{
		LYTHCPara lYTHCPara = LYTHCPara.Create();
		if (collectMode == 1)
		{
			if (bAnalyse)
			{
				bAnalyse = false;
				bStateAnalyze = false;
				lYTHCPara.strCollectTime = DateTime.Now.ToString();
				lYTHCPara.SaveParam();
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
				if ((float)cntIntivalTIme > intervalTime * 600f)
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
			bStateAnalyze = false;
			bAnalyse = false;
			cntIntivalTIme = 0uL;
			for (int i = 0; i < collectTimes; i++)
			{
				Class49.InsertIntoLYTHCRLT(0, 1, fthcAmount[i], fch4Amount[i], fnmhcAmount[i]);
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
			bStateAnalyze = false;
			bAnalyse = false;
			cntIntivalTIme = 0uL;
			for (int k = 0; k < collectTimes; k++)
			{
				Class49.InsertIntoLYTHCRLT(0, 1, fthcAmount[k], fch4Amount[k], fnmhcAmount[k]);
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
			formCollectResult3.TopLevel = true;
			formCollectResult3.Show();
		}
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
			CountAnalyse++;
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
					if (!(rltPeaks[num].pkRT >= cdlMgr.ChartParaOperaList[0].mtdMgr.caliGnl.cmpds[b].cmpdInfo.retainTime - cdlMgr.ChartParaOperaList[0].mtdMgr.caliGnl.cmpds[b].cmpdInfo.leftWindow) || !(rltPeaks[num].pkRT <= cdlMgr.ChartParaOperaList[0].mtdMgr.caliGnl.cmpds[b].cmpdInfo.retainTime + cdlMgr.ChartParaOperaList[0].mtdMgr.caliGnl.cmpds[b].cmpdInfo.rightWindow) || !(rltPeaks[num].height >= num2))
					{
						continue;
					}
					if (cdlMgr.formMain.IsAutoCalibra == 1)
					{
						b2++;
						caliGnl2.cmpds[b].levels[0].responseA = rltPeaks[num].area;
						caliGnl2.CalculateFunc(appendLink: false);
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
						num5 = rltPeaks[num].area * caliGnl2.cmpds[b].levels[0].respFactor;
						labTHCRlt.Text = num5.ToString("0.00") + " " + lythcParamMgr.strUnit;
						array = new float[1] { num5 };
						Buffer.BlockCopy(array, 0, array2, 0, 4);
						cdlMgr.tcpServerMgr.mComModbus.WordVaue[14] = array2[0];
						cdlMgr.tcpServerMgr.mComModbus.WordVaue[15] = array2[1];
						Class49.InsertIntoVoc(1, 0, rltPeaks[num].name, fileName.ToLower(), num5);
						continue;
					case 1:
						break;
					default:
						continue;
					}
					num6 = (rltPeaks[num].amount = rltPeaks[num].area * caliGnl2.cmpds[b].levels[0].respFactor);
					num4 = num;
					labCH4Rlt.Text = rltPeaks[num].amount.ToString("0.00") + " " + lythcParamMgr.strUnit;
					array = new float[1] { rltPeaks[num].amount };
					Buffer.BlockCopy(array, 0, array2, 0, 4);
					cdlMgr.tcpServerMgr.mComModbus.WordVaue[16] = array2[0];
					cdlMgr.tcpServerMgr.mComModbus.WordVaue[17] = array2[1];
					Class49.InsertIntoVoc(2, 0, rltPeaks[num].name, fileName.ToLower(), rltPeaks[num].amount);
					break;
				}
			}
			if (num5 > num6)
			{
				labNMHCRlt.Text = (num5 - num6).ToString("0.00") + " " + lythcParamMgr.strUnit;
				Class49.InsertIntoVoc(3, 0, "非甲烷总烃", fileName.ToLower(), num5 - num6);
			}
			else
			{
				labNMHCRlt.Text = "0.00 " + lythcParamMgr.strUnit;
				Class49.InsertIntoVoc(3, 0, "非甲烷总烃", fileName.ToLower(), 0f);
			}
		}
		cdlMgr.CurrentChartParaOpera.mtdMgr.caliGnl = caliGnl;
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
		this.label79 = new System.Windows.Forms.Label();
		this.labCH4Rlt = new System.Windows.Forms.Label();
		this.labTHCRlt = new System.Windows.Forms.Label();
		this.label77 = new System.Windows.Forms.Label();
		this.lbNMHCT = new System.Windows.Forms.Label();
		this.labNMHCRlt = new System.Windows.Forms.Label();
		this.groupBox1 = new System.Windows.Forms.GroupBox();
		this.groupBox2 = new System.Windows.Forms.GroupBox();
		this.labCYGXCur = new System.Windows.Forms.Label();
		this.labFaXiangCur = new System.Windows.Forms.Label();
		this.labCHSCur = new System.Windows.Forms.Label();
		this.labDecCur = new System.Windows.Forms.Label();
		this.label33 = new System.Windows.Forms.Label();
		this.labCYGXSet = new System.Windows.Forms.Label();
		this.label32 = new System.Windows.Forms.Label();
		this.label31 = new System.Windows.Forms.Label();
		this.label1 = new System.Windows.Forms.Label();
		this.label2 = new System.Windows.Forms.Label();
		this.label3 = new System.Windows.Forms.Label();
		this.labFaXiangSet = new System.Windows.Forms.Label();
		this.labCHSSet = new System.Windows.Forms.Label();
		this.labDecSet = new System.Windows.Forms.Label();
		this.groupBox3 = new System.Windows.Forms.GroupBox();
		this.label39 = new System.Windows.Forms.Label();
		this.label7 = new System.Windows.Forms.Label();
		this.label8 = new System.Windows.Forms.Label();
		this.label9 = new System.Windows.Forms.Label();
		this.labTempEnvirCur = new System.Windows.Forms.Label();
		this.labBaroEnvirCur = new System.Windows.Forms.Label();
		this.label12 = new System.Windows.Forms.Label();
		this.groupBox5 = new System.Windows.Forms.GroupBox();
		this.tbTimeChuiSao = new System.Windows.Forms.TextBox();
		this.btnChuiSao = new System.Windows.Forms.Button();
		this.label21 = new System.Windows.Forms.Label();
		this.label24 = new System.Windows.Forms.Label();
		this.groupBox6 = new System.Windows.Forms.GroupBox();
		this.groupBox4 = new System.Windows.Forms.GroupBox();
		this.labSampleCur = new System.Windows.Forms.Label();
		this.labHHCur = new System.Windows.Forms.Label();
		this.labAirCur = new System.Windows.Forms.Label();
		this.labZQCur = new System.Windows.Forms.Label();
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
		this.tbTempHuoHua = new System.Windows.Forms.TextBox();
		this.btnHuoHua = new System.Windows.Forms.Button();
		this.label19 = new System.Windows.Forms.Label();
		this.label20 = new System.Windows.Forms.Label();
		this.tbTimeHuoHua = new System.Windows.Forms.TextBox();
		this.label22 = new System.Windows.Forms.Label();
		this.label23 = new System.Windows.Forms.Label();
		this.labLongitudeCur = new System.Windows.Forms.Label();
		this.labLatitudeCur = new System.Windows.Forms.Label();
		this.labHumiEnvirCur = new System.Windows.Forms.Label();
		this.groupBox1.SuspendLayout();
		this.groupBox2.SuspendLayout();
		this.groupBox3.SuspendLayout();
		this.groupBox5.SuspendLayout();
		this.groupBox6.SuspendLayout();
		this.groupBox4.SuspendLayout();
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
		this.groupBox1.Location = new System.Drawing.Point(3, 532);
		this.groupBox1.Name = "groupBox1";
		this.groupBox1.Size = new System.Drawing.Size(300, 187);
		this.groupBox1.TabIndex = 80;
		this.groupBox1.TabStop = false;
		this.groupBox1.Text = "测量结果";
		this.groupBox2.Controls.Add(this.labCYGXCur);
		this.groupBox2.Controls.Add(this.labFaXiangCur);
		this.groupBox2.Controls.Add(this.labCHSCur);
		this.groupBox2.Controls.Add(this.labDecCur);
		this.groupBox2.Controls.Add(this.label33);
		this.groupBox2.Controls.Add(this.labCYGXSet);
		this.groupBox2.Controls.Add(this.label32);
		this.groupBox2.Controls.Add(this.label31);
		this.groupBox2.Controls.Add(this.label1);
		this.groupBox2.Controls.Add(this.label2);
		this.groupBox2.Controls.Add(this.label3);
		this.groupBox2.Controls.Add(this.labFaXiangSet);
		this.groupBox2.Controls.Add(this.labCHSSet);
		this.groupBox2.Controls.Add(this.labDecSet);
		this.groupBox2.Location = new System.Drawing.Point(3, 3);
		this.groupBox2.Name = "groupBox2";
		this.groupBox2.Size = new System.Drawing.Size(300, 247);
		this.groupBox2.TabIndex = 81;
		this.groupBox2.TabStop = false;
		this.groupBox2.Text = "温度参数";
		this.labCYGXCur.AutoSize = true;
		this.labCYGXCur.Cursor = System.Windows.Forms.Cursors.Hand;
		this.labCYGXCur.Location = new System.Drawing.Point(200, 195);
		this.labCYGXCur.Name = "labCYGXCur";
		this.labCYGXCur.Size = new System.Drawing.Size(11, 12);
		this.labCYGXCur.TabIndex = 87;
		this.labCYGXCur.Text = "0";
		this.labFaXiangCur.AutoSize = true;
		this.labFaXiangCur.Cursor = System.Windows.Forms.Cursors.Hand;
		this.labFaXiangCur.Location = new System.Drawing.Point(200, 103);
		this.labFaXiangCur.Name = "labFaXiangCur";
		this.labFaXiangCur.Size = new System.Drawing.Size(11, 12);
		this.labFaXiangCur.TabIndex = 85;
		this.labFaXiangCur.Text = "0";
		this.labCHSCur.AutoSize = true;
		this.labCHSCur.Cursor = System.Windows.Forms.Cursors.Hand;
		this.labCHSCur.Location = new System.Drawing.Point(200, 147);
		this.labCHSCur.Name = "labCHSCur";
		this.labCHSCur.Size = new System.Drawing.Size(11, 12);
		this.labCHSCur.TabIndex = 86;
		this.labCHSCur.Text = "0";
		this.labDecCur.AutoSize = true;
		this.labDecCur.Cursor = System.Windows.Forms.Cursors.Hand;
		this.labDecCur.Location = new System.Drawing.Point(200, 54);
		this.labDecCur.Name = "labDecCur";
		this.labDecCur.Size = new System.Drawing.Size(11, 12);
		this.labDecCur.TabIndex = 84;
		this.labDecCur.Text = "0";
		this.label33.AutoSize = true;
		this.label33.Location = new System.Drawing.Point(15, 193);
		this.label33.Name = "label33";
		this.label33.Size = new System.Drawing.Size(65, 12);
		this.label33.TabIndex = 82;
		this.label33.Text = "采样管线：";
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
		this.groupBox3.Location = new System.Drawing.Point(3, 297);
		this.groupBox3.Name = "groupBox3";
		this.groupBox3.Size = new System.Drawing.Size(300, 187);
		this.groupBox3.TabIndex = 84;
		this.groupBox3.TabStop = false;
		this.groupBox3.Text = "测量结果";
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
		this.groupBox5.Controls.Add(this.tbTimeChuiSao);
		this.groupBox5.Controls.Add(this.btnChuiSao);
		this.groupBox5.Controls.Add(this.label21);
		this.groupBox5.Controls.Add(this.label24);
		this.groupBox5.Location = new System.Drawing.Point(506, 297);
		this.groupBox5.Name = "groupBox5";
		this.groupBox5.Size = new System.Drawing.Size(300, 187);
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
		this.groupBox6.Location = new System.Drawing.Point(506, 532);
		this.groupBox6.Name = "groupBox6";
		this.groupBox6.Size = new System.Drawing.Size(300, 187);
		this.groupBox6.TabIndex = 86;
		this.groupBox6.TabStop = false;
		this.groupBox6.Text = "测量结果";
		this.groupBox4.Controls.Add(this.labSampleCur);
		this.groupBox4.Controls.Add(this.labHHCur);
		this.groupBox4.Controls.Add(this.labAirCur);
		this.groupBox4.Controls.Add(this.labZQCur);
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
		this.groupBox4.Location = new System.Drawing.Point(506, 3);
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
		this.labHHCur.AutoSize = true;
		this.labHHCur.Cursor = System.Windows.Forms.Cursors.Hand;
		this.labHHCur.Location = new System.Drawing.Point(199, 103);
		this.labHHCur.Name = "labHHCur";
		this.labHHCur.Size = new System.Drawing.Size(11, 12);
		this.labHHCur.TabIndex = 85;
		this.labHHCur.Text = "0";
		this.labAirCur.AutoSize = true;
		this.labAirCur.Cursor = System.Windows.Forms.Cursors.Hand;
		this.labAirCur.Location = new System.Drawing.Point(199, 147);
		this.labAirCur.Name = "labAirCur";
		this.labAirCur.Size = new System.Drawing.Size(11, 12);
		this.labAirCur.TabIndex = 86;
		this.labAirCur.Text = "0";
		this.labZQCur.AutoSize = true;
		this.labZQCur.Cursor = System.Windows.Forms.Cursors.Hand;
		this.labZQCur.Location = new System.Drawing.Point(199, 54);
		this.labZQCur.Name = "labZQCur";
		this.labZQCur.Size = new System.Drawing.Size(11, 12);
		this.labZQCur.TabIndex = 84;
		this.labZQCur.Text = "0";
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
		this.labLongitudeCur.AutoSize = true;
		this.labLongitudeCur.Cursor = System.Windows.Forms.Cursors.Hand;
		this.labLongitudeCur.Location = new System.Drawing.Point(69, 33);
		this.labLongitudeCur.Name = "labLongitudeCur";
		this.labLongitudeCur.Size = new System.Drawing.Size(11, 12);
		this.labLongitudeCur.TabIndex = 81;
		this.labLongitudeCur.Text = "0";
		this.labLatitudeCur.AutoSize = true;
		this.labLatitudeCur.Cursor = System.Windows.Forms.Cursors.Hand;
		this.labLatitudeCur.Location = new System.Drawing.Point(167, 35);
		this.labLatitudeCur.Name = "labLatitudeCur";
		this.labLatitudeCur.Size = new System.Drawing.Size(11, 12);
		this.labLatitudeCur.TabIndex = 82;
		this.labLatitudeCur.Text = "0";
		this.labHumiEnvirCur.AutoSize = true;
		this.labHumiEnvirCur.Cursor = System.Windows.Forms.Cursors.Hand;
		this.labHumiEnvirCur.Location = new System.Drawing.Point(167, 80);
		this.labHumiEnvirCur.Name = "labHumiEnvirCur";
		this.labHumiEnvirCur.Size = new System.Drawing.Size(11, 12);
		this.labHumiEnvirCur.TabIndex = 83;
		this.labHumiEnvirCur.Text = "0";
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.Controls.Add(this.groupBox4);
		base.Controls.Add(this.groupBox6);
		base.Controls.Add(this.groupBox5);
		base.Controls.Add(this.groupBox3);
		base.Controls.Add(this.groupBox2);
		base.Controls.Add(this.groupBox1);
		base.Name = "LYTHCtrl";
		base.Size = new System.Drawing.Size(1055, 755);
		this.groupBox1.ResumeLayout(false);
		this.groupBox1.PerformLayout();
		this.groupBox2.ResumeLayout(false);
		this.groupBox2.PerformLayout();
		this.groupBox3.ResumeLayout(false);
		this.groupBox3.PerformLayout();
		this.groupBox5.ResumeLayout(false);
		this.groupBox5.PerformLayout();
		this.groupBox6.ResumeLayout(false);
		this.groupBox6.PerformLayout();
		this.groupBox4.ResumeLayout(false);
		this.groupBox4.PerformLayout();
		base.ResumeLayout(false);
	}
}
