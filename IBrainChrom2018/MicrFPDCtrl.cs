using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using HZH_Controls.Controls;
using IBrainChrom2018.Properties;
using IBrainChrom2018.Unit;

namespace IBrainChrom2018;

public class MicrFPDCtrl : UserControl
{
	public static MicrFPDCtrl selfCtrl;

	public bool bCalibration = false;

	public bool bCalibration2 = false;

	public bool AutoTempCtr = true;

	public int CountTempCtr = 0;

	public int CountAnalyse = 0;

	public int StateYiqi = 0;

	public bool bAutoFire1 = true;

	public bool bAutoFire2 = true;

	public int iChn = 0;

	private IniParam iniParam = new IniParam(Application.StartupPath + "\\iniParam.dll");

	public List<ResultGridModel> lstSource1 = new List<ResultGridModel>();

	public List<ResultGridModel> lstSource2 = new List<ResultGridModel>();

	private ChromDeviceListMgr cdlMgr = ChromDeviceListMgr.Create();

	private FormMainParam frmParam = FormMainParam.Create();

	private ChannelTCPServer ChannelTCPserver = new ChannelTCPServer(2000);

	public SerialPortBase serialPoartBase = new SerialPortBase();

	public string[] strCompName;

	public int anysisChannel = 0;

	public int anysisChannelOld = 0;

	public long anysisStart = 0L;

	private byte channelEnabelState = 0;

	private CalibraParam calibraParam = CalibraParam.Create();

	private DateTime dtLastTimeCalibra;

	public int cntCalibra = 0;

	public double subTimeAutoCalibra;

	public int iSampleDelay;

	public int iCollectTimes;

	public int iLevel;

	public bool bAutoCalibra = false;

	public float[] fCompAmountLevel = new float[20];

	public ClassAutoCalibraComp[] autoCalibraComp = new ClassAutoCalibraComp[30];

	public ClassAutoCalibraComp[] autoCalibraComp2 = new ClassAutoCalibraComp[30];

	public ClassAutoCalibraComp[] autoCalibraComp3 = new ClassAutoCalibraComp[30];

	public ClassAutoCalibraComp[] autoCalibraComp4 = new ClassAutoCalibraComp[30];

	public ClassAutoCalibraPeak[] autoCalibraPeak1 = new ClassAutoCalibraPeak[0];

	public ClassAutoCalibraPeak[] autoCalibraPeak2 = new ClassAutoCalibraPeak[0];

	public ClassAutoCalibraPeak[] autoCalibraPeak3 = new ClassAutoCalibraPeak[0];

	public ClassAutoCalibraPeak[] autoCalibraPeak4 = new ClassAutoCalibraPeak[0];

	public int iState = 0;

	private IContainer components = null;

	public PictureBox pibChannel3;

	private CheckBox chbChannel1;

	private CheckBox chbChannel2;

	private CheckBox chbChannel3;

	private Button btnStart;

	private Button btnSet;

	public PictureBox pibChannel8;

	private Label label84;

	public PictureBox pibChannel7;

	private CheckBox chbChannel8;

	public PictureBox pibChannel6;

	private CheckBox chbChannel7;

	private CheckBox chbChannel4;

	public PictureBox pibChannel5;

	private Label label83;

	private Label label82;

	private Label label94;

	private Label label95;

	private Label label96;

	private CheckBox chbChannel6;

	private Label label97;

	private CheckBox chbChannel5;

	private TextBox tbComTimes;

	private TextBox tbInjecTime;

	private Label label98;

	private TextBox tbAnyTime;

	private TextBox tbCycleTimes;

	public PictureBox pibChannel1;

	public PictureBox pibChannel4;

	public PictureBox pibChannel2;

	private Button button39;

	private Label labStateIns;

	private Button btnHighValueSet;

	private Button btnHighValueCheck;

	private Label label78;

	public TextBox tbHighV;

	public CheckBox chbFPDHighV;

	private Label label2;

	private Button btShowDesktop;

	private Button button36;

	private Button NetConfig;

	private Button button2;

	public Label LabHHS;

	public Label labName1;

	private Button btnFireOnCheck;

	public TextBox tbFireOn2;

	public TextBox tbFireOn;

	private Button btnFireOnSet;

	public Label labFPDhighV;

	private Button btnStartAutoCalibra;

	private Label label1;

	private TextBox tbCalibraCountDown;

	private TextBox tbLastCalibra;

	private Label label3;

	private System.Windows.Forms.Timer timer1;

	private Label labState;

	private Label labLevel;

	private TextBox tbTotalSAlarm;

	private Label label4;

	private TextBox tbHSAlarm;

	private Label label5;

	private GroupBox groupBox2;

	private Button btnModbusSave;

	private TextBox tbModbusAddress;

	private Label label31;

	private Button btnCali;

	private TextBox tbModbusRegister;

	private Label label6;

	private TabControl tabControl1;

	private TabPage tabPage1;

	private TabPage tabPage2;

	private TabPage tabPage3;

	private GroupBox groupBox1;

	private GroupBox groupBox3;

	public CheckBox chbFPDHighV2;

	public Label LabHHS2;

	private Label label8;

	public Label labFPDhighV2;

	public TextBox tbHighV2;

	public Label label10;

	private Button button1;

	private Button btnHighValueSet2;

	private UCBtnExt btnHistory;

	public Label labData2;

	public Label labName2;

	private UCDataGridView dgFpd1;

	private UCDataGridView dgFpd2;

	private CheckBox chbSul;

	public Label labThermTemp;

	public static bool IsDesignMode()
	{
		return false;
	}

	public MicrFPDCtrl()
	{
		InitializeComponent();
		selfCtrl = this;
		if (!IsDesignMode())
		{
			tbLastCalibra.Text = calibraParam.strLastTimeCalibra;
			if (!DateTime.TryParse(calibraParam.strLastTimeCalibra, out dtLastTimeCalibra))
			{
				dtLastTimeCalibra = new DateTime(2000, 1, 1, 21, 21, 21);
			}
			tbTotalSAlarm.Text = frmParam.fTotalSAlarm.ToString();
			tbHSAlarm.Text = frmParam.fHSAlarm.ToString();
			tbModbusAddress.Text = frmParam.iModbusAddress.ToString();
			tbModbusRegister.Text = frmParam.iModbusRegister.ToString();
			cdlMgr.tcpServerMgr.mComModbus.DevAdd = frmParam.iModbusAddress;
			ChannelTCPserver.Start();
			strCompName = new string[15];
			for (int i = 0; i < 15; i++)
			{
				strCompName[i] = "";
			}
			List<DataGridViewColumnEntity> columns = new List<DataGridViewColumnEntity>
			{
				new DataGridViewColumnEntity
				{
					DataField = "name",
					HeadText = "组份名",
					Width = 80,
					WidthType = SizeType.Absolute
				},
				new DataGridViewColumnEntity
				{
					DataField = "curV",
					HeadText = "含量",
					Width = 40,
					WidthType = SizeType.Percent
				}
			};
			dgFpd1.Columns = columns;
			dgFpd1.RowFont = new Font("微软雅黑", 6f);
			dgFpd1.IsShowCheckBox = false;
			for (int j = 0; j < 50; j++)
			{
				ResultGridModel item = new ResultGridModel
				{
					name = " ",
					curV = " "
				};
				lstSource1.Add(item);
			}
			dgFpd1.DataSource = lstSource1;
			dgFpd1.ReloadSource();
			List<DataGridViewColumnEntity> list = new List<DataGridViewColumnEntity>
			{
				new DataGridViewColumnEntity
				{
					DataField = "name",
					HeadText = "组份名",
					Width = 80,
					WidthType = SizeType.Absolute
				},
				new DataGridViewColumnEntity
				{
					DataField = "curV",
					HeadText = "含量",
					Width = 40,
					WidthType = SizeType.Percent
				}
			};
			dgFpd2.Columns = columns;
			dgFpd2.RowFont = new Font("微软雅黑", 6f);
			dgFpd2.IsShowCheckBox = false;
			for (int k = 0; k < 50; k++)
			{
				ResultGridModel item2 = new ResultGridModel
				{
					name = " ",
					curV = " "
				};
				lstSource2.Add(item2);
			}
			dgFpd2.DataSource = lstSource2;
			dgFpd2.ReloadSource();
			try
			{
				serialPoartBase.openPort();
			}
			catch
			{
			}
			iniParam.LoadParam();
			chbSul.Checked = iniParam.bSul;
		}
	}

	private void btnHighValueCheck_Click(object sender, EventArgs e)
	{
		if (Class49.user_0.ULevel == User.Level.管理员)
		{
			cdlMgr.currentTcpServerMgrSendCmd(253);
			return;
		}
		MessageBox.Show(Lang.PS("没有设置仪器权限！", "Without permission!"));
	}

	private void btnHighValueSet_Click(object sender, EventArgs e)
	{
		if (int.Parse(tbHighV.Text.Trim()) > 800)
		{
			MessageBox.Show("高压值设定不能高于800V");
		}
		else if (Class49.user_0.ULevel == User.Level.管理员)
		{
			if (cdlMgr.CurrentTcpServerSocket != null)
			{
				cdlMgr.currentTcpServerMgrSendCmd(252);
			}
		}
		else
		{
			MessageBox.Show(Lang.PS("没有设置仪器权限！", "Without permission!"));
		}
	}

	private void button39_Click(object sender, EventArgs e)
	{
		FormAUTOCalibra formAUTOCalibra = new FormAUTOCalibra();
		formAUTOCalibra.StartPosition = FormStartPosition.CenterScreen;
		formAUTOCalibra.Show();
	}

	private void FPDHighV_CheckedChanged(object sender, EventArgs e)
	{
		cdlMgr.currentTcpServerMgrSendCmd(252);
	}

	private void btnSet_Click(object sender, EventArgs e)
	{
		byte[] array = new byte[4];
		byte[] array2 = new byte[6] { 192, 164, 0, 0, 0, 0 };
		float result = 0f;
		float.TryParse(tbInjecTime.Text, out result);
		if (result <= 0f)
		{
			MessageBox.Show("采样时间须大于0！");
			return;
		}
		float[] array3 = new float[1];
		ushort[] array4 = new ushort[2];
		array3[0] = result;
		Buffer.BlockCopy(array3, 0, array4, 0, 4);
		Buffer.BlockCopy(array4, 0, array, 0, 4);
		array2[2] = array[3];
		array2[3] = array[2];
		array2[4] = array[1];
		array2[5] = array[0];
		foreach (ChannelTCPClientState client in ChannelTCPserver._clients)
		{
			ChannelTCPserver.Send(client, array2);
		}
		Thread.Sleep(50);
		float.TryParse(tbAnyTime.Text, out result);
		if (result <= 0f)
		{
			MessageBox.Show("分析时间须大于0！");
			return;
		}
		array3[0] = result;
		Buffer.BlockCopy(array3, 0, array4, 0, 4);
		Buffer.BlockCopy(array4, 0, array, 0, 4);
		array2[0] = 192;
		array2[1] = 165;
		array2[2] = array[3];
		array2[3] = array[2];
		array2[4] = array[1];
		array2[5] = array[0];
		foreach (ChannelTCPClientState client2 in ChannelTCPserver._clients)
		{
			ChannelTCPserver.Send(client2, array2);
		}
		Thread.Sleep(50);
		int result2 = 0;
		int.TryParse(tbCycleTimes.Text, out result2);
		if (result2 < 0)
		{
			MessageBox.Show("循环次数须大于等于0！");
			return;
		}
		byte[] data = new byte[4]
		{
			192,
			166,
			(byte)(result2 >> 8),
			(byte)result2
		};
		foreach (ChannelTCPClientState client3 in ChannelTCPserver._clients)
		{
			ChannelTCPserver.Send(client3, data);
		}
	}

	private void btnStart_Click(object sender, EventArgs e)
	{
		byte[] array = new byte[3] { 192, 162, 0 };
		if (btnStart.Text == Lang.PS("循环采集", "StartAll"))
		{
			if (channelEnabelState == 0)
			{
				MessageBox.Show("至少选择一个流路");
				return;
			}
			chbChannel1.Enabled = false;
			chbChannel2.Enabled = false;
			chbChannel3.Enabled = false;
			chbChannel4.Enabled = false;
			chbChannel5.Enabled = false;
			chbChannel6.Enabled = false;
			chbChannel7.Enabled = false;
			chbChannel8.Enabled = false;
			tbInjecTime.ReadOnly = true;
			tbCycleTimes.ReadOnly = true;
			tbAnyTime.ReadOnly = true;
			array[2] = 1;
			btnStart.Text = Lang.PS("停止采集", "StartAll");
		}
		else
		{
			btnStart.Text = Lang.PS("循环采集", "StartAll");
			chbChannel1.Enabled = true;
			chbChannel2.Enabled = true;
			chbChannel3.Enabled = true;
			chbChannel4.Enabled = true;
			chbChannel5.Enabled = true;
			chbChannel6.Enabled = true;
			chbChannel7.Enabled = true;
			chbChannel8.Enabled = true;
			tbInjecTime.ReadOnly = false;
			tbCycleTimes.ReadOnly = false;
			tbAnyTime.ReadOnly = false;
			array[2] = 0;
		}
		foreach (ChannelTCPClientState client in ChannelTCPserver._clients)
		{
			ChannelTCPserver.Send(client, array);
		}
	}

	private void chbChannel1_CheckedChanged(object sender, EventArgs e)
	{
		byte[] array = new byte[3] { 192, 161, 0 };
		if (chbChannel1.Checked)
		{
			channelEnabelState |= 1;
		}
		else
		{
			channelEnabelState &= 254;
		}
		array[2] = channelEnabelState;
		foreach (ChannelTCPClientState client in ChannelTCPserver._clients)
		{
			ChannelTCPserver.Send(client, array);
		}
	}

	private void chbChannel2_CheckedChanged(object sender, EventArgs e)
	{
		byte[] array = new byte[3] { 192, 161, 0 };
		if (chbChannel2.Checked)
		{
			channelEnabelState |= 2;
		}
		else
		{
			channelEnabelState &= 253;
		}
		array[2] = channelEnabelState;
		foreach (ChannelTCPClientState client in ChannelTCPserver._clients)
		{
			ChannelTCPserver.Send(client, array);
		}
	}

	private void chbChannel3_CheckedChanged(object sender, EventArgs e)
	{
		byte[] array = new byte[3] { 192, 161, 0 };
		if (chbChannel3.Checked)
		{
			channelEnabelState |= 4;
		}
		else
		{
			channelEnabelState &= 251;
		}
		array[2] = channelEnabelState;
		foreach (ChannelTCPClientState client in ChannelTCPserver._clients)
		{
			ChannelTCPserver.Send(client, array);
		}
	}

	private void chbChannel4_CheckedChanged(object sender, EventArgs e)
	{
		byte[] array = new byte[3] { 192, 161, 0 };
		if (chbChannel4.Checked)
		{
			channelEnabelState |= 8;
		}
		else
		{
			channelEnabelState &= 247;
		}
		array[2] = channelEnabelState;
		foreach (ChannelTCPClientState client in ChannelTCPserver._clients)
		{
			ChannelTCPserver.Send(client, array);
		}
	}

	private void chbChannel5_CheckedChanged(object sender, EventArgs e)
	{
		byte[] array = new byte[3] { 192, 161, 0 };
		if (chbChannel5.Checked)
		{
			channelEnabelState |= 16;
		}
		else
		{
			channelEnabelState &= 239;
		}
		array[2] = channelEnabelState;
		foreach (ChannelTCPClientState client in ChannelTCPserver._clients)
		{
			ChannelTCPserver.Send(client, array);
		}
	}

	private void chbChannel6_CheckedChanged(object sender, EventArgs e)
	{
		byte[] array = new byte[3] { 192, 161, 0 };
		if (chbChannel6.Checked)
		{
			channelEnabelState |= 32;
		}
		else
		{
			channelEnabelState &= 223;
		}
		array[2] = channelEnabelState;
		foreach (ChannelTCPClientState client in ChannelTCPserver._clients)
		{
			ChannelTCPserver.Send(client, array);
		}
	}

	private void chbChannel7_CheckedChanged(object sender, EventArgs e)
	{
		byte[] array = new byte[3] { 192, 161, 0 };
		if (chbChannel7.Checked)
		{
			channelEnabelState |= 64;
		}
		else
		{
			channelEnabelState &= 191;
		}
		array[2] = channelEnabelState;
		foreach (ChannelTCPClientState client in ChannelTCPserver._clients)
		{
			ChannelTCPserver.Send(client, array);
		}
	}

	private void chbChannel8_CheckedChanged(object sender, EventArgs e)
	{
		byte[] array = new byte[3] { 192, 161, 0 };
		if (chbChannel8.Checked)
		{
			channelEnabelState |= 128;
		}
		else
		{
			channelEnabelState &= 127;
		}
		array[2] = channelEnabelState;
		foreach (ChannelTCPClientState client in ChannelTCPserver._clients)
		{
			ChannelTCPserver.Send(client, array);
		}
	}

	private void button2_Click(object sender, EventArgs e)
	{
		FrmChromatManager frmChromatManager = new FrmChromatManager();
		frmChromatManager.Show();
	}

	private void NetConfig_Click(object sender, EventArgs e)
	{
		NetSetForm netSetForm = new NetSetForm();
		netSetForm.StartPosition = FormStartPosition.CenterScreen;
		netSetForm.TopMost = true;
		netSetForm.Show();
	}

	private void btShowDesktop_Click(object sender, EventArgs e)
	{
		if (FormVOC.fromVoc != null)
		{
			FormVOC.fromVoc.WindowState = FormWindowState.Minimized;
		}
		base.ParentForm.WindowState = FormWindowState.Minimized;
	}

	public void disposeFPDPeaks(int selectedIndex, string fileName, string strID, string strSampleIndex, Chromatogram chromatogram)
	{
		AreaPlotParamMgr areaPlotParamMgr = AreaPlotParamMgr.Create();
		AreaPlotParam areaPlotParam = null;
		if (cdlMgr.tcpServerMgr.mComModbus.WordVaue.Length <= 50)
		{
			LogMgr.Instance.Write2RunLog("VocCtrl.disposeVOCPeaks  Error:mComModbus.WordVaue.Length<50");
			return;
		}
		float[] array = new float[50];
		string[] array2 = new string[50];
		int num = 0;
		byte b = 0;
		byte b2 = 0;
		float num2 = 0f;
		float num3 = 0f;
		float num4 = 0f;
		int num5 = 0;
		float[] array3 = new float[10];
		if (cdlMgr.CurrentChartParaOpera.mtdMgr.caliGnl == null)
		{
			cdlMgr.CurrentChartParaOpera.mtdMgr.caliGnl = new CaliGnl();
		}
		CaliGnl caliGnl = cdlMgr.CurrentChartParaOpera.mtdMgr.caliGnl;
		if (cdlMgr.CurrentTcpServerSocket.bAutoCalibra)
		{
			calibraDispose(selectedIndex, chromatogram);
			return;
		}
		Peak[] rltPeaks = chromatogram.RltPeaks;
		float[] array4 = new float[1];
		ushort[] array5 = new ushort[2];
		int iModbusRegister = frmParam.iModbusRegister;
		switch (selectedIndex)
		{
		case 0:
		{
			CaliGnl caliGnl3 = cdlMgr.ChartParaOperaList[0].mtdMgr.caliGnl;
			CountAnalyse++;
			float num6 = 0f;
			float num7 = 0f;
			array4 = new float[1];
			Buffer.BlockCopy(array4, 0, array5, 0, 4);
			int num8 = 0;
			for (int j = 0; j < 50; j++)
			{
				array[j] = 0f;
				array2[j] = 0.ToString("F" + Class49.int_8);
				lstSource1[j].name = " ";
				lstSource1[j].curV = " ";
				cdlMgr.tcpServerMgr.mComModbus.WordVaue[iModbusRegister + num8 + iChn * 100] = array5[0];
				cdlMgr.tcpServerMgr.mComModbus.WordVaue[iModbusRegister + 1 + num8 + iChn * 100] = array5[1];
				num8 += 2;
			}
			cdlMgr.tcpServerMgr.mComModbus.WordVaue[14] = array5[0];
			cdlMgr.tcpServerMgr.mComModbus.WordVaue[15] = array5[1];
			cdlMgr.tcpServerMgr.mComModbus.WordVaue[16] = array5[0];
			cdlMgr.tcpServerMgr.mComModbus.WordVaue[17] = array5[1];
			for (b = 0; b < cdlMgr.ChartParaOperaList[0].mtdMgr.caliGnl.cmpds.Count(); b++)
			{
				lstSource1[b].name = cdlMgr.ChartParaOperaList[0].mtdMgr.caliGnl.cmpds[b].cmpdInfo.name;
				lstSource1[b].curV = 0.ToString("F" + Class49.int_8);
				num = 0;
				while (1 <= rltPeaks.Count() && num < rltPeaks.Count())
				{
					if (rltPeaks[num].pkRT >= cdlMgr.ChartParaOperaList[0].mtdMgr.caliGnl.cmpds[b].cmpdInfo.retainTime - cdlMgr.ChartParaOperaList[0].mtdMgr.caliGnl.cmpds[b].cmpdInfo.leftWindow && rltPeaks[num].pkRT <= cdlMgr.ChartParaOperaList[0].mtdMgr.caliGnl.cmpds[b].cmpdInfo.retainTime + cdlMgr.ChartParaOperaList[0].mtdMgr.caliGnl.cmpds[b].cmpdInfo.rightWindow && !(rltPeaks[num].name != cdlMgr.ChartParaOperaList[0].mtdMgr.caliGnl.cmpds[b].cmpdInfo.name) && rltPeaks[num].height >= num2)
					{
						if (cdlMgr.formMain.IsAutoCalibra == 1)
						{
							b2++;
							caliGnl3.cmpds[b].levels[0].responseA = rltPeaks[num].area;
							caliGnl3.CalculateFunc(appendLink: false);
							caliGnl3.cmpds[b].levels[0].respFactor = rltPeaks[num].GasAmount / rltPeaks[num].area;
							cdlMgr.ChartParaOperaList[0].mtdMgr.SaveToFile();
							cdlMgr.formMain.MainmstSet.ReadFromMtdMgr();
							switch (b)
							{
							case 0:
								num6 = rltPeaks[num].area * caliGnl3.cmpds[b].levels[0].respFactor;
								break;
							case 1:
								num7 = (rltPeaks[num].amount = rltPeaks[num].area * caliGnl3.cmpds[b].levels[0].respFactor);
								break;
							}
							cdlMgr.ChartParaOperaList[0].mtdMgr.SaveToFile();
							cdlMgr.formMain.MainmstSet.UsePara();
							cdlMgr.formMain.MainmstSet.ReadFromMtdMgr();
							cdlMgr.formMain.MainmstSet.bUseSet_Click(null, null);
							break;
						}
						if (bCalibration)
						{
							num5 = cdlMgr.ChartParaOperaList[0].mtdMgr.caliGnl.cmpds.Count();
							b2++;
							cdlMgr.ChartParaOperaList[0].mtdMgr.caliGnl.cmpds[b].levels[0].responseA = rltPeaks[num].area;
							caliGnl3.cmpds[b].levels[0].respFactor = rltPeaks[num].GasAmount / rltPeaks[num].area;
							rltPeaks[num].amount = rltPeaks[num].area * caliGnl3.cmpds[b].levels[0].respFactor;
						}
						array4 = new float[1] { rltPeaks[num].amount };
						array[b] = rltPeaks[num].amount;
						lstSource1[b].curV = (array2[b] = rltPeaks[num].amount.ToString("F" + Class49.int_8));
						Buffer.BlockCopy(array4, 0, array5, 0, 4);
						num3 += rltPeaks[num].amount;
						cdlMgr.tcpServerMgr.mComModbus.WordVaue[100 + iModbusRegister++] = array5[0];
						cdlMgr.tcpServerMgr.mComModbus.WordVaue[100 + iModbusRegister++] = array5[1];
						array4[0] = rltPeaks[num].areaPer;
						Buffer.BlockCopy(array4, 0, array5, 0, 4);
						cdlMgr.tcpServerMgr.mComModbus.WordVaue[200 + iModbusRegister++] = array5[0];
						cdlMgr.tcpServerMgr.mComModbus.WordVaue[200 + iModbusRegister++] = array5[1];
						if (rltPeaks[num].name.Contains("硫化氢"))
						{
							num4 = rltPeaks[num].amount;
						}
						break;
					}
					num++;
				}
				if (num >= rltPeaks.Count())
				{
					Buffer.BlockCopy(new float[1] { 0f }, 0, array5, 0, 4);
					cdlMgr.tcpServerMgr.mComModbus.WordVaue[iChn * 100 + iModbusRegister++] = array5[0];
					cdlMgr.tcpServerMgr.mComModbus.WordVaue[iChn * 100 + iModbusRegister++] = array5[1];
					Class49.InsertIntoVoc(11 + b, 0, cdlMgr.ChartParaOperaList[0].mtdMgr.caliGnl.cmpds[b].cmpdInfo.name, fileName.ToLower(), 0f);
				}
			}
			if (bCalibration)
			{
				bCalibration = false;
				cdlMgr.ChartParaOperaList[0].mtdMgr.caliGnl.CalculateFunc(appendLink: false);
				cdlMgr.ChartParaOperaList[0].mtdMgr.SaveToFile();
				cdlMgr.formMain.MainmstSet.ReadFromMtdMgr();
				cdlMgr.formMain.MainmstSet.bUseSet_Click(null, null);
			}
			cdlMgr.tcpServerMgr.mComModbus.WordVaue[999] = (ushort)StateYiqi;
			if (num3 >= frmParam.fTotalSAlarm)
			{
				cdlMgr.tcpServerMgr.mComModbus.WordVaue[1000] = 1;
			}
			else
			{
				cdlMgr.tcpServerMgr.mComModbus.WordVaue[1000] = 0;
			}
			if (num4 >= frmParam.fHSAlarm)
			{
				cdlMgr.tcpServerMgr.mComModbus.WordVaue[1001] = 1;
			}
			else
			{
				cdlMgr.tcpServerMgr.mComModbus.WordVaue[1001] = 0;
			}
			if (cdlMgr.CurrentTcpServerSocket.bError)
			{
				cdlMgr.tcpServerMgr.mComModbus.WordVaue[1002] = 1;
			}
			else
			{
				cdlMgr.tcpServerMgr.mComModbus.WordVaue[1002] = 0;
			}
			Buffer.BlockCopy(new float[1] { num3 }, 0, array5, 0, 4);
			cdlMgr.tcpServerMgr.mComModbus.WordVaue[100] = array5[0];
			cdlMgr.tcpServerMgr.mComModbus.WordVaue[101] = array5[1];
			ushort num9 = (ushort)((!(num3 - frmParam.fmount41 < 0f)) ? ((ushort)((num3 - frmParam.fmount41) / (frmParam.fmount201 - frmParam.fmount41) * 4095f)) : 0);
			if (num9 > 4095)
			{
				num9 = 4095;
			}
			if (num9 < 0)
			{
				num9 = 0;
			}
			cdlMgr.tcpServerMgr.mComModbus.WordVaue[102] = num9;
			num9 = (ushort)((!(array[0] - frmParam.fmount42 < 0f)) ? ((ushort)((array[0] - frmParam.fmount42) / (frmParam.fmount202 - frmParam.fmount42) * 4095f)) : 0);
			if (num9 > 4095)
			{
				num9 = 4095;
			}
			if (num9 < 0)
			{
				num9 = 0;
			}
			cdlMgr.tcpServerMgr.mComModbus.WordVaue[103] = num9;
			num9 = (ushort)((!(array[1] - frmParam.fmount43 < 0f)) ? ((ushort)((array[1] - frmParam.fmount43) / (frmParam.fmount203 - frmParam.fmount43) * 4095f)) : 0);
			if (num9 > 4095)
			{
				num9 = 4095;
			}
			if (num9 < 0)
			{
				num9 = 0;
			}
			cdlMgr.tcpServerMgr.mComModbus.WordVaue[104] = num9;
			num9 = (ushort)((!(array[2] - frmParam.fmount44 < 0f)) ? ((ushort)((array[2] - frmParam.fmount44) / (frmParam.fmount204 - frmParam.fmount44) * 4095f)) : 0);
			cdlMgr.tcpServerMgr.mComModbus.WordVaue[105] = num9;
			num9 = (ushort)((!(array[3] - frmParam.fmount45 < 0f)) ? ((ushort)((array[3] - frmParam.fmount45) / (frmParam.fmount205 - frmParam.fmount45) * 4095f)) : 0);
			if (num9 > 4095)
			{
				num9 = 4095;
			}
			if (num9 < 0)
			{
				num9 = 0;
			}
			cdlMgr.tcpServerMgr.mComModbus.WordVaue[106] = num9;
			num9 = (ushort)((!(array[4] - frmParam.fmount46 < 0f)) ? ((ushort)((array[4] - frmParam.fmount46) / (frmParam.fmount206 - frmParam.fmount46) * 4095f)) : 0);
			if (num9 > 4095)
			{
				num9 = 4095;
			}
			if (num9 < 0)
			{
				num9 = 0;
			}
			cdlMgr.tcpServerMgr.mComModbus.WordVaue[107] = num9;
			num9 = (ushort)((!(array[5] - frmParam.fmount47 < 0f)) ? ((ushort)((array[5] - frmParam.fmount47) / (frmParam.fmount207 - frmParam.fmount47) * 4095f)) : 0);
			if (num9 > 4095)
			{
				num9 = 4095;
			}
			if (num9 < 0)
			{
				num9 = 0;
			}
			cdlMgr.tcpServerMgr.mComModbus.WordVaue[108] = num9;
			num9 = (ushort)((!(array[6] - frmParam.fmount48 < 0f)) ? ((ushort)((array[6] - frmParam.fmount48) / (frmParam.fmount208 - frmParam.fmount48) * 4095f)) : 0);
			if (num9 > 4095)
			{
				num9 = 4095;
			}
			if (num9 < 0)
			{
				num9 = 0;
			}
			cdlMgr.tcpServerMgr.mComModbus.WordVaue[109] = num9;
			num9 = (ushort)((!(array[7] - frmParam.fmount49 < 0f)) ? ((ushort)((array[7] - frmParam.fmount49) / (frmParam.fmount209 - frmParam.fmount49) * 4095f)) : 0);
			if (num9 > 4095)
			{
				num9 = 4095;
			}
			if (num9 < 0)
			{
				num9 = 0;
			}
			cdlMgr.tcpServerMgr.mComModbus.WordVaue[110] = num9;
			num9 = (ushort)((!(array[8] - frmParam.fmount410 < 0f)) ? ((ushort)((array[8] - frmParam.fmount410) / (frmParam.fmount2010 - frmParam.fmount410) * 4095f)) : 0);
			if (num9 > 4095)
			{
				num9 = 4095;
			}
			if (num9 < 0)
			{
				num9 = 0;
			}
			cdlMgr.tcpServerMgr.mComModbus.WordVaue[111] = num9;
			num9 = (ushort)((!(num4 - frmParam.fmount411 < 0f)) ? ((ushort)((num4 - frmParam.fmount411) / (frmParam.fmount2011 - frmParam.fmount411) * 4095f)) : 0);
			if (num9 > 4095)
			{
				num9 = 4095;
			}
			if (num9 < 0)
			{
				num9 = 0;
			}
			cdlMgr.tcpServerMgr.mComModbus.WordVaue[112] = num9;
			for (int k = 0; k < 2000; k++)
			{
				cdlMgr.tcpServerMgr.mComModbus2.WordVaue[k] = cdlMgr.tcpServerMgr.mComModbus.WordVaue[k];
			}
			if (cdlMgr.formMain.IsAutoCalibra == 1 && b >= caliGnl3.cmpds.Count())
			{
				cdlMgr.formMain.IsAutoCalibra = 0;
				if (b2 >= caliGnl3.cmpds.Count())
				{
					MessageBox.Show("标定成功！");
				}
				else
				{
					MessageBox.Show("标定失败！");
				}
				cdlMgr.formMain.tabControl.SelectedIndex = 1;
				cdlMgr.formMain.chromFormCtrl.OpenChrom(fileName, sampling: false, useCurrent: true);
				cdlMgr.currentTcpServerMgrSendCmd(19);
				cdlMgr.formMain.tabChannel.Enabled = true;
				return;
			}
			dgFpd1.ReloadSource();
			break;
		}
		case 1:
		{
			CaliGnl caliGnl2 = cdlMgr.ChartParaOperaList[1].mtdMgr.caliGnl;
			float[] array6 = new float[10];
			areaPlotParam = areaPlotParamMgr.GetAreaPlotParam(11);
			CountAnalyse++;
			array4 = new float[1];
			Buffer.BlockCopy(array4, 0, array5, 0, 4);
			cdlMgr.tcpServerMgr.mComModbus.WordVaue[50] = array5[0];
			cdlMgr.tcpServerMgr.mComModbus.WordVaue[51] = array5[1];
			cdlMgr.tcpServerMgr.mComModbus.WordVaue[48] = array5[0];
			cdlMgr.tcpServerMgr.mComModbus.WordVaue[49] = array5[1];
			cdlMgr.tcpServerMgr.mComModbus.WordVaue[46] = array5[0];
			cdlMgr.tcpServerMgr.mComModbus.WordVaue[47] = array5[1];
			cdlMgr.tcpServerMgr.mComModbus.WordVaue[44] = array5[0];
			cdlMgr.tcpServerMgr.mComModbus.WordVaue[45] = array5[1];
			cdlMgr.tcpServerMgr.mComModbus.WordVaue[42] = array5[0];
			cdlMgr.tcpServerMgr.mComModbus.WordVaue[43] = array5[1];
			cdlMgr.tcpServerMgr.mComModbus.WordVaue[40] = array5[0];
			cdlMgr.tcpServerMgr.mComModbus.WordVaue[41] = array5[1];
			cdlMgr.tcpServerMgr.mComModbus.WordVaue[38] = array5[0];
			cdlMgr.tcpServerMgr.mComModbus.WordVaue[39] = array5[1];
			cdlMgr.tcpServerMgr.mComModbus.WordVaue[36] = array5[0];
			cdlMgr.tcpServerMgr.mComModbus.WordVaue[37] = array5[1];
			cdlMgr.tcpServerMgr.mComModbus.WordVaue[34] = array5[0];
			cdlMgr.tcpServerMgr.mComModbus.WordVaue[35] = array5[1];
			for (int i = 0; i < 50; i++)
			{
				lstSource2[i].name = " ";
				lstSource2[i].curV = " ";
			}
			for (b = 0; b < caliGnl2.cmpds.Count(); b++)
			{
				lstSource2[b].name = caliGnl2.cmpds[b].cmpdInfo.name;
				lstSource2[b].curV = 0.ToString("F" + Class49.int_8);
				num = 0;
				while (1 <= rltPeaks.Count() && num < rltPeaks.Count())
				{
					if (rltPeaks[num].pkRT >= caliGnl2.cmpds[b].cmpdInfo.retainTime - caliGnl2.cmpds[b].cmpdInfo.leftWindow && rltPeaks[num].pkRT <= caliGnl2.cmpds[b].cmpdInfo.retainTime + caliGnl2.cmpds[b].cmpdInfo.rightWindow && !(rltPeaks[num].name != caliGnl2.cmpds[b].cmpdInfo.name) && rltPeaks[num].height >= num2)
					{
						if (cdlMgr.formMain.IsAutoCalibra == 2)
						{
							b2++;
							caliGnl2.cmpds[b].levels[0].responseA = rltPeaks[num].area;
							caliGnl2.CalculateFunc(appendLink: false);
							caliGnl2.cmpds[b].levels[0].respFactor = rltPeaks[num].GasAmount / rltPeaks[num].area;
							cdlMgr.ChartParaOperaList[1].mtdMgr.SaveToFile();
							cdlMgr.formMain.MainmstSet.ReadFromMtdMgr();
							array6[b] = (rltPeaks[num].amount = rltPeaks[num].area * caliGnl2.cmpds[b].levels[0].respFactor);
							cdlMgr.ChartParaOperaList[1].mtdMgr.SaveToFile();
							cdlMgr.formMain.MainmstSet.UsePara();
							cdlMgr.formMain.MainmstSet.ReadFromMtdMgr();
							cdlMgr.formMain.MainmstSet.bUseSet_Click(null, null);
						}
						else
						{
							if (!bCalibration2)
							{
								array6[b] = rltPeaks[num].amount;
								lstSource2[b].curV = rltPeaks[num].amount.ToString("F" + Class49.int_8);
								array4 = new float[1] { array6[b] };
								Buffer.BlockCopy(array4, 0, array5, 0, 4);
								Class49.InsertIntoVoc(1 + b, 0, rltPeaks[num].name, fileName.ToLower(), rltPeaks[num].amount);
								break;
							}
							b2++;
							cdlMgr.ChartParaOperaList[1].mtdMgr.caliGnl.cmpds[b].levels[0].responseA = rltPeaks[num].area;
						}
					}
					num++;
				}
				if (num >= rltPeaks.Count())
				{
					Class49.InsertIntoVoc(1 + b, 0, caliGnl2.cmpds[b].cmpdInfo.name, fileName.ToLower(), 0f);
				}
			}
			if (bCalibration2)
			{
				bCalibration2 = false;
				cdlMgr.ChartParaOperaList[1].mtdMgr.caliGnl.CalculateFunc(appendLink: false);
				cdlMgr.ChartParaOperaList[1].mtdMgr.SaveToFile();
				cdlMgr.formMain.MainmstSet.ReadFromMtdMgr();
				cdlMgr.formMain.MainmstSet.bUseSet_Click(null, null);
			}
			Class49.InsertIntoVoc(20, 0, null, fileName.ToLower(), array6[0] + array6[1] + array6[2] + array6[3] + array6[4] + array6[5] + array6[6] + array6[7] + array6[8]);
			if (cdlMgr.formMain.IsAutoCalibra == 2 && b >= caliGnl2.cmpds.Count())
			{
				cdlMgr.formMain.IsAutoCalibra = 0;
				if (b2 >= caliGnl2.cmpds.Count())
				{
					MessageBox.Show("标定成功！");
				}
				else
				{
					MessageBox.Show("标定失败！");
				}
				cdlMgr.formMain.tabControl.SelectedIndex = 2;
				cdlMgr.formMain.chromFormCtrl.OpenChrom(fileName, sampling: false, useCurrent: true);
				cdlMgr.currentTcpServerMgrSendCmd(19);
				cdlMgr.formMain.tabChannel.Enabled = true;
				return;
			}
			break;
		}
		}
		int l = 0;
		int num10 = 0;
		for (; l < 2000; l++)
		{
			cdlMgr.tcpServerMgr.modBusData_0.ModBusBytes[num10++] = (byte)(cdlMgr.tcpServerMgr.mComModbus.WordVaue[l] / 256);
			cdlMgr.tcpServerMgr.modBusData_0.ModBusBytes[num10++] = (byte)(cdlMgr.tcpServerMgr.mComModbus.WordVaue[l] % 256);
		}
		if (StateYiqi != 6)
		{
			StateYiqi = 5;
		}
		ushort[] array7 = new ushort[2];
		long[] src = new long[1] { CountAnalyse * 10 + StateYiqi };
		Buffer.BlockCopy(src, 0, array7, 0, 4);
		cdlMgr.tcpServerMgr.mComModbus.WordVaue[8] = array7[0];
		cdlMgr.tcpServerMgr.mComModbus.WordVaue[9] = array7[1];
		cdlMgr.CurrentChartParaOpera.mtdMgr.caliGnl = caliGnl;
		dgFpd2.ReloadSource();
	}

	private void chbFPDHighV_CheckedChanged(object sender, EventArgs e)
	{
	}

	private void BtnFireOnCheck_Click(object sender, EventArgs e)
	{
		if (Class49.user_0.ULevel == User.Level.管理员)
		{
			cdlMgr.currentTcpServerMgrSendCmd(250);
			return;
		}
		MessageBox.Show(Lang.PS("没有设置仪器权限！", "Without permission!"));
	}

	private float ToFloat(string str)
	{
		float result = 0f;
		float.TryParse(str, out result);
		return result;
	}

	private void BtnFireOnSet_Click(object sender, EventArgs e)
	{
		if (Class49.user_0.ULevel == User.Level.管理员)
		{
			cdlMgr.currentTcpServerMgrSendCmd(249);
			frmParam.fFireOn = ToFloat(tbFireOn.Text);
			frmParam.fFireOn2 = ToFloat(tbFireOn2.Text);
			frmParam.SaveParam();
		}
		else
		{
			MessageBox.Show(Lang.PS("没有设置仪器权限！", "Without permission!"));
		}
	}

	public void channelUpdate()
	{
		if (cdlMgr.CurrentTcpServerSocket == null)
		{
			return;
		}
		if (cdlMgr.CurrentTcpServerSocket.Ready)
		{
		}
		if (ChannelTCPserver.dataBuff[0] != 192)
		{
			return;
		}
		switch (ChannelTCPserver.dataBuff[1])
		{
		case 160:
			switch (ChannelTCPserver.dataBuff[2])
			{
			case 1:
				anysisChannel = 1;
				break;
			case 2:
				anysisChannel = 2;
				break;
			case 4:
				anysisChannel = 3;
				break;
			case 8:
				anysisChannel = 4;
				break;
			case 16:
				anysisChannel = 5;
				break;
			case 32:
				anysisChannel = 6;
				break;
			case 64:
				anysisChannel = 7;
				break;
			case 128:
				anysisChannel = 8;
				break;
			case 250:
				ChannelTCPserver.dataBuff[2] = 0;
				if (Class49.user_0.ULevel != User.Level.访问员)
				{
					cdlMgr.CurrentTcpServerSocket.SendCmd(18);
					return;
				}
				MessageBox.Show(Lang.PS("没有启动权限！", "Without permission!"));
				break;
			}
			break;
		case 161:
			channelEnabelState = ChannelTCPserver.dataBuff[2];
			break;
		case 162:
			if (ChannelTCPserver.dataBuff[2] != 1)
			{
			}
			break;
		case 163:
			if (Class49.user_0.ULevel != User.Level.访问员)
			{
				if (ChannelTCPserver.dataBuff[2] == 1)
				{
					cdlMgr.CurrentTcpServerSocket.SendCmd(18);
				}
			}
			else
			{
				MessageBox.Show(Lang.PS("没有启动权限！", "Without permission!"));
			}
			break;
		case 164:
		{
			byte[] array = new byte[4];
			byte[] array2 = new byte[6]
			{
				192,
				164,
				ChannelTCPserver.dataBuff[2],
				ChannelTCPserver.dataBuff[3],
				ChannelTCPserver.dataBuff[4],
				ChannelTCPserver.dataBuff[5]
			};
			float num2 = 0f;
			float[] dst = new float[1];
			Buffer.BlockCopy(new ushort[2]
			{
				(ushort)((ChannelTCPserver.dataBuff[4] << 8) | ChannelTCPserver.dataBuff[5]),
				(ushort)((ChannelTCPserver.dataBuff[2] << 8) | ChannelTCPserver.dataBuff[3])
			}, 0, dst, 0, 4);
			break;
		}
		case 165:
		{
			float[] dst = new float[1];
			Buffer.BlockCopy(new ushort[2]
			{
				(ushort)((ChannelTCPserver.dataBuff[4] << 8) | ChannelTCPserver.dataBuff[5]),
				(ushort)((ChannelTCPserver.dataBuff[2] << 8) | ChannelTCPserver.dataBuff[3])
			}, 0, dst, 0, 4);
			break;
		}
		case 166:
		{
			int num = 0;
			num = (ChannelTCPserver.dataBuff[2] << 8) | ChannelTCPserver.dataBuff[3];
			break;
		}
		case 167:
		{
			int num = (ChannelTCPserver.dataBuff[2] << 8) | ChannelTCPserver.dataBuff[3];
			break;
		}
		case 169:
			if (Class49.user_0.ULevel != User.Level.访问员 && ChannelTCPserver.dataBuff[2] == 1)
			{
				cdlMgr.CurrentTcpServerSocket.SendCmd(19);
			}
			break;
		case 170:
			if (Class49.user_0.ULevel != User.Level.访问员 && ChannelTCPserver.dataBuff[2] == 1)
			{
				cdlMgr.CurrentTcpServerSocket.SendCmd(20);
			}
			break;
		case 179:
			if (ChannelTCPserver.dataBuff[2] == 1)
			{
				iChn = 0;
				cdlMgr.CurrentTcpServerSocket.SendCmd(18);
			}
			else if (ChannelTCPserver.dataBuff[2] == 2)
			{
				iChn = 1;
				cdlMgr.CurrentTcpServerSocket.SendCmd(18);
			}
			break;
		}
		ChannelTCPserver.dataBuff[0] = 0;
		ChannelTCPserver.dataBuff[1] = 0;
		ChannelTCPserver.dataBuff[2] = 0;
		ChannelTCPserver.dataBuff[3] = 0;
	}

	private void LabHHS_Click(object sender, EventArgs e)
	{
		FormAreaPlot formAreaPlot = new FormAreaPlot(11);
		formAreaPlot.StartPosition = FormStartPosition.CenterScreen;
		formAreaPlot.label4.Visible = false;
		formAreaPlot.cbPeakName.Visible = false;
		formAreaPlot.btnConvert.Visible = false;
		formAreaPlot.Show();
		formAreaPlot.loadData();
	}

	private void BtnFireOnCheck_Click_1(object sender, EventArgs e)
	{
		cdlMgr.currentTcpServerMgrSendCmd(250);
	}

	private void ChbFPDHighV_MouseClick(object sender, MouseEventArgs e)
	{
		if (!chbFPDHighV.Checked)
		{
			bAutoFire1 = false;
		}
		else
		{
			bAutoFire1 = true;
		}
		cdlMgr.currentTcpServerMgrSendCmd(251);
	}

	private void btnStartAutoCalibra_Click(object sender, EventArgs e)
	{
		if (cdlMgr.ChartParaOperaList[0].mtdMgr.IsNull)
		{
			MessageBox.Show("请先手动标定并加载组份表!");
			return;
		}
		if (btnStartAutoCalibra.Text == "启用自动标定")
		{
			btnStartAutoCalibra.Text = "停用自动标定";
			Array.Resize(ref autoCalibraPeak1, 0);
			Array.Resize(ref autoCalibraPeak2, 0);
			Array.Resize(ref autoCalibraPeak3, 0);
			Array.Resize(ref autoCalibraPeak4, 0);
			timer1.Enabled = true;
			updateSerialPortDate(0);
			return;
		}
		btnStartAutoCalibra.Text = "启用自动标定";
		tbCalibraCountDown.Text = " ";
		timer1.Enabled = false;
		labState.Text = "正常运行";
		updateSerialPortDate(0);
		iState = 0;
		cdlMgr.CurrentTcpServerSocket.bAutoCalibra = false;
		cdlMgr.CurrentTcpServerSocket.iLevel = 0;
		cdlMgr.CurrentTcpServerSocket.iCollectTimes = 0;
		cdlMgr.CurrentTcpServerSocket.iLevel = 0;
		Array.Resize(ref autoCalibraPeak1, 0);
		Array.Resize(ref autoCalibraPeak2, 0);
		Array.Resize(ref autoCalibraPeak3, 0);
		Array.Resize(ref autoCalibraPeak4, 0);
	}

	public void calibraDispose(int selectedIndex, Chromatogram chromatogram)
	{
		Peak[] peakFromCompound = chromatogram.GetPeakFromCompound();
		switch (selectedIndex)
		{
		case 0:
			Array.Resize(ref autoCalibraPeak1, autoCalibraPeak1.Length + 1);
			autoCalibraPeak1[autoCalibraPeak1.Length - 1] = new ClassAutoCalibraPeak();
			autoCalibraPeak1[autoCalibraPeak1.Length - 1].peak = chromatogram.GetPeakFromCompound();
			break;
		case 1:
			Array.Resize(ref autoCalibraPeak2, autoCalibraPeak2.Length + 1);
			autoCalibraPeak2[autoCalibraPeak2.Length - 1] = new ClassAutoCalibraPeak();
			autoCalibraPeak2[autoCalibraPeak2.Length - 1].peak = chromatogram.GetPeakFromCompound();
			break;
		case 2:
			Array.Resize(ref autoCalibraPeak3, autoCalibraPeak3.Length + 1);
			autoCalibraPeak3[autoCalibraPeak3.Length - 1] = new ClassAutoCalibraPeak();
			autoCalibraPeak3[autoCalibraPeak3.Length - 1].peak = chromatogram.GetPeakFromCompound();
			break;
		case 3:
			Array.Resize(ref autoCalibraPeak4, autoCalibraPeak4.Length + 1);
			autoCalibraPeak4[autoCalibraPeak4.Length - 1] = new ClassAutoCalibraPeak();
			autoCalibraPeak4[autoCalibraPeak4.Length - 1].peak = chromatogram.GetPeakFromCompound();
			break;
		}
		if (autoCalibraPeak1.Length < calibraParam.iLevel * calibraParam.iCollectTimes)
		{
			return;
		}
		updateSerialPortDate(0);
		CaliGnl caliGnl = cdlMgr.ChartParaOperaList[selectedIndex].mtdMgr.caliGnl;
		if (caliGnl.cmpds.Length != autoCalibraPeak1[0].peak.Length)
		{
			restartCalibra();
			return;
		}
		ClassAutoCalibraPeak[] array = new ClassAutoCalibraPeak[calibraParam.iLevel];
		for (int i = 0; i < calibraParam.iLevel; i++)
		{
			array[i] = new ClassAutoCalibraPeak();
			Array.Resize(ref array[i].peak, autoCalibraPeak1[0].peak.Length);
			for (int j = 0; j < autoCalibraPeak1[0].peak.Length; j++)
			{
				array[i].peak[j] = new Peak();
			}
		}
		switch (selectedIndex)
		{
		case 0:
		{
			for (int k = 0; k < autoCalibraPeak1[0].peak.Length; k++)
			{
				for (int l = 0; l < calibraParam.iLevel; l++)
				{
					for (int m = l * calibraParam.iCollectTimes; m < (l + 1) * calibraParam.iCollectTimes; m++)
					{
						array[l].peak[k].area += autoCalibraPeak1[m].peak[k].area / (float)calibraParam.iCollectTimes;
						array[l].peak[k].height += autoCalibraPeak1[m].peak[k].height / (float)calibraParam.iCollectTimes;
					}
				}
			}
			break;
		}
		}
		btnStartAutoCalibra.Text = "启用自动标定";
		labState.Text = "正常运行";
		iState = 0;
		cdlMgr.CurrentTcpServerSocket.bAutoCalibra = false;
		cdlMgr.CurrentTcpServerSocket.iLevel = 0;
		cdlMgr.CurrentTcpServerSocket.iCollectTimes = 0;
		float[] array2 = new float[calibraParam.iCollectTimes];
		if (autoCalibraPeak1[0].peak == null)
		{
			restartCalibra();
			return;
		}
		for (int n = 0; n < autoCalibraPeak1[0].peak.Length; n++)
		{
			for (int num = 0; num < array2.Length; num++)
			{
				if (autoCalibraPeak1[num].peak == null)
				{
					restartCalibra();
					return;
				}
				if (caliGnl.cmpds[0].cmpdInfo.respStyle == RespStyle.Area)
				{
					array2[num] = autoCalibraPeak1[num].peak[n].area;
				}
				else
				{
					array2[num] = autoCalibraPeak1[num].peak[n].height;
				}
			}
			if (rsdCalculate(array2.Length, array2, array2.Length) > calibraParam.fRSDLimit)
			{
				restartCalibra();
				return;
			}
		}
		for (int num2 = 0; num2 < calibraParam.iLevel; num2++)
		{
			for (int num3 = 0; num3 < caliGnl.cmpds.Length; num3++)
			{
				if (caliGnl.cmpds[num3].cmpdInfo.respStyle == RespStyle.Area)
				{
					caliGnl.cmpds[num3].levels[num2].responseA = array[num2].peak[num3].area;
				}
				else
				{
					caliGnl.cmpds[num3].levels[num2].responseA = array[num2].peak[num3].height;
				}
				caliGnl.cmpds[num3].levels[num2].LastAddresponseA = caliGnl.cmpds[num3].levels[num2].responseA;
				caliGnl.cmpds[num3].levels[num2].amount = 120f;
				caliGnl.cmpds[num3].levels[num2].used = true;
				caliGnl.cmpds[num3].eFunc.curveFit = (CurveFit)calibraParam.iCurveFitSelect;
				caliGnl.cmpds[num3].eFunc.original = (Original)calibraParam.iOriginalSelect;
				caliGnl.CalculateFunc(appendLink: false);
				caliGnl.cmpds[num3].levels[num2].respFactor = caliGnl.cmpds[num3].levels[num2].amount / caliGnl.cmpds[num3].levels[num2].responseA;
				caliGnl.cmpds[num3].cmpdInfo.freeRespFactor = caliGnl.cmpds[num3].levels[num2].respFactor;
			}
		}
		caliGnl.SaveFile(cdlMgr.ChartParaOperaList[0].mtdMgr.chromInfo.cclCalibration);
		cdlMgr.ChartParaOperaList[0].mtdMgr.SaveToFile();
		cdlMgr.formMain.MainmstSet.ReadFromMtdMgr();
		cdlMgr.formMain.MainmstSet.UsePara();
		cdlMgr.formMain.MainmstSet.ReadFromMtdMgr();
		cdlMgr.formMain.MainmstSet.bUseSet_Click(null, null);
		btnStartAutoCalibra.Text = "启用自动标定";
		tbCalibraCountDown.Text = " ";
		timer1.Enabled = false;
		labState.Text = "标定完成";
		iState = 0;
		cdlMgr.CurrentTcpServerSocket.bAutoCalibra = false;
		cdlMgr.CurrentTcpServerSocket.iLevel = 0;
		cdlMgr.CurrentTcpServerSocket.iCollectTimes = 0;
		labState.Text = "自动标定成功";
		cdlMgr.currentTcpServerMgrSendCmd(18);
		calibraParam.strLastTimeCalibra = DateTime.Now.ToString();
		calibraParam.SaveParam();
		tbLastCalibra.Text = calibraParam.strLastTimeCalibra;
	}

	public double rsdCalculate(int peakLen, float[] arrValue, int length)
	{
		float num = 0f;
		for (int i = 0; i < peakLen; i++)
		{
			num += arrValue[i];
		}
		float fAverage = num / (float)peakLen;
		return Program.RSDCalculate(fAverage, arrValue, length);
	}

	public void restartCalibra()
	{
		btnStartAutoCalibra.Text = "启用自动标定";
		tbCalibraCountDown.Text = " ";
		timer1.Enabled = false;
		labState.Text = "标定失败 重新开始标定";
		updateSerialPortDate(0);
		iState = 0;
		cdlMgr.CurrentTcpServerSocket.bAutoCalibra = false;
		cdlMgr.CurrentTcpServerSocket.iLevel = 0;
		cdlMgr.CurrentTcpServerSocket.iCollectTimes = 0;
		Array.Resize(ref autoCalibraPeak1, 0);
		Array.Resize(ref autoCalibraPeak2, 0);
		Array.Resize(ref autoCalibraPeak3, 0);
		Array.Resize(ref autoCalibraPeak4, 0);
		cntCalibra++;
		if (cntCalibra >= 3)
		{
			labState.Text = "自动标定失败";
			cdlMgr.CurrentTcpServerSocket.SendCmd(18);
			calibraParam.strLastTimeCalibra = DateTime.Now.ToString();
			calibraParam.SaveParam();
			tbLastCalibra.Text = calibraParam.strLastTimeCalibra;
		}
		else
		{
			btnStartAutoCalibra.Text = "停用自动标定";
			Array.Resize(ref autoCalibraPeak1, 0);
			Array.Resize(ref autoCalibraPeak2, 0);
			Array.Resize(ref autoCalibraPeak3, 0);
			Array.Resize(ref autoCalibraPeak4, 0);
			timer1.Enabled = true;
		}
	}

	public void updateSerialPortDate(byte index, byte value)
	{
		serialPoartBase.Data[12] = 0;
		index = (byte)(11 - index);
		for (int i = 4; i < 12; i++)
		{
			serialPoartBase.Data[i] = 0;
			if (i == index)
			{
				serialPoartBase.Data[index] = value;
			}
		}
	}

	public void updateSerialPortDate(int iChannel)
	{
		switch (iChannel)
		{
		case 0:
			updateSerialPortDate(0, 0);
			break;
		case 1:
			updateSerialPortDate(0, 1);
			break;
		case 2:
			updateSerialPortDate(0, 2);
			break;
		case 3:
			updateSerialPortDate(0, 4);
			break;
		case 4:
			updateSerialPortDate(0, 8);
			break;
		case 5:
			updateSerialPortDate(0, 16);
			break;
		case 6:
			updateSerialPortDate(0, 32);
			break;
		case 7:
			updateSerialPortDate(0, 64);
			break;
		case 8:
			updateSerialPortDate(0, 128);
			break;
		case 9:
			updateSerialPortDate(1, 1);
			break;
		case 10:
			updateSerialPortDate(1, 2);
			break;
		case 11:
			updateSerialPortDate(1, 4);
			break;
		case 12:
			updateSerialPortDate(1, 8);
			break;
		}
	}

	private void timer1_Tick(object sender, EventArgs e)
	{
		if (cdlMgr.formMain.tabChannel.TabCount < 2)
		{
			groupBox3.Visible = false;
		}
		if (cdlMgr.ChartParaOperaList != null && cdlMgr.ChartParaOperaList[0].mtdMgr != null && cdlMgr.ChartParaOperaList[0].mtdMgr.caliGnl != null)
		{
			if (cdlMgr.ChartParaOperaList[0].mtdMgr.caliGnl.cmpds.Count() > 0)
			{
				labName1.Text = cdlMgr.ChartParaOperaList[0].mtdMgr.caliGnl.cmpds[0].cmpdInfo.name;
			}
			if (cdlMgr.ChartParaOperaList[0].mtdMgr.caliGnl.cmpds.Count() > 1)
			{
				labName2.Text = cdlMgr.ChartParaOperaList[0].mtdMgr.caliGnl.cmpds[1].cmpdInfo.name;
			}
		}
	}

	private void tbTotalSAlarm_TextChanged(object sender, EventArgs e)
	{
		float.TryParse(tbTotalSAlarm.Text.Trim(), out frmParam.fTotalSAlarm);
		frmParam.SaveParam();
	}

	private void tbHSAlarm_TextChanged(object sender, EventArgs e)
	{
		float.TryParse(tbHSAlarm.Text.Trim(), out frmParam.fHSAlarm);
		frmParam.SaveParam();
	}

	private void btnModbusSave_Click(object sender, EventArgs e)
	{
		ushort.TryParse(tbModbusAddress.Text, out frmParam.iModbusAddress);
		ushort.TryParse(tbModbusRegister.Text, out frmParam.iModbusRegister);
		frmParam.SaveParam();
		cdlMgr.tcpServerMgr.mComModbus.DevAdd = frmParam.iModbusAddress;
	}

	private void btnCali_Click(object sender, EventArgs e)
	{
		if (cdlMgr.formMain.tabChannel.TabCount > 1)
		{
			if (cdlMgr.CurrentTcpServerSocket != null && (cdlMgr.CurrentTcpServerSocket.sglsSampling[0].simple || cdlMgr.CurrentTcpServerSocket.sglsSampling[1].simple))
			{
				MessageBox.Show(Lang.PS("样品正在采集，请等待样品结束！", "Samples are being collected, please wait for the end of samples!"));
				return;
			}
		}
		else if (cdlMgr.formMain.tabChannel.TabCount == 1 && cdlMgr.CurrentTcpServerSocket != null && cdlMgr.CurrentTcpServerSocket.sglsSampling[0].simple)
		{
			MessageBox.Show(Lang.PS("样品正在采集，请等待样品结束！", "Samples are being collected, please wait for the end of samples!"));
			return;
		}
		if (Class49.user_0.ULevel == User.Level.管理员)
		{
			cdlMgr.currentTcpServerMgrSendCmd(18);
			bCalibration2 = true;
			bCalibration = true;
		}
	}

	private void chbFPDHighV2_CheckedChanged(object sender, EventArgs e)
	{
	}

	private void chbFPDHighV2_MouseClick(object sender, MouseEventArgs e)
	{
		if (!chbFPDHighV2.Checked)
		{
			bAutoFire2 = false;
		}
		else
		{
			bAutoFire2 = true;
		}
		cdlMgr.currentTcpServerMgrSendCmd(251);
	}

	private void button1_Click(object sender, EventArgs e)
	{
		if (Class49.user_0.ULevel == User.Level.管理员)
		{
			cdlMgr.currentTcpServerMgrSendCmd(253);
			return;
		}
		MessageBox.Show(Lang.PS("没有设置仪器权限！", "Without permission!"));
	}

	private void btnHighValueSet2_Click(object sender, EventArgs e)
	{
		if (int.Parse(tbHighV2.Text.Trim()) > 800)
		{
			MessageBox.Show("高压值设定不能高于800V");
		}
		else if (Class49.user_0.ULevel == User.Level.管理员)
		{
			if (cdlMgr.CurrentTcpServerSocket != null)
			{
				cdlMgr.currentTcpServerMgrSendCmd(252);
			}
		}
		else
		{
			MessageBox.Show(Lang.PS("没有设置仪器权限！", "Without permission!"));
		}
	}

	private void LabHHS2_Click(object sender, EventArgs e)
	{
		FormAreaPlot formAreaPlot = new FormAreaPlot(1);
		formAreaPlot.StartPosition = FormStartPosition.CenterScreen;
		formAreaPlot.label4.Visible = false;
		formAreaPlot.cbPeakName.Visible = false;
		formAreaPlot.btnConvert.Visible = false;
		formAreaPlot.Show();
		formAreaPlot.loadData();
	}

	private void btnHistory_BtnClick(object sender, EventArgs e)
	{
		FormHistoryData formHistoryData = new FormHistoryData();
		formHistoryData.StartPosition = FormStartPosition.CenterScreen;
		formHistoryData.Show();
		formHistoryData.loadData();
	}

	private void labData2_Click(object sender, EventArgs e)
	{
		FormAreaPlot formAreaPlot = new FormAreaPlot(12);
		formAreaPlot.StartPosition = FormStartPosition.CenterScreen;
		formAreaPlot.label4.Visible = false;
		formAreaPlot.cbPeakName.Visible = false;
		formAreaPlot.btnConvert.Visible = false;
		formAreaPlot.Show();
		formAreaPlot.loadData();
	}

	private void chbSul_Click(object sender, EventArgs e)
	{
		iniParam.bSul = chbSul.Checked;
		iniParam.SaveParam();
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(IBrainChrom2018.MicrFPDCtrl));
		this.btnCali = new System.Windows.Forms.Button();
		this.groupBox2 = new System.Windows.Forms.GroupBox();
		this.tbModbusRegister = new System.Windows.Forms.TextBox();
		this.label6 = new System.Windows.Forms.Label();
		this.btnModbusSave = new System.Windows.Forms.Button();
		this.tbModbusAddress = new System.Windows.Forms.TextBox();
		this.label31 = new System.Windows.Forms.Label();
		this.tbTotalSAlarm = new System.Windows.Forms.TextBox();
		this.label4 = new System.Windows.Forms.Label();
		this.tbHSAlarm = new System.Windows.Forms.TextBox();
		this.label5 = new System.Windows.Forms.Label();
		this.labLevel = new System.Windows.Forms.Label();
		this.labState = new System.Windows.Forms.Label();
		this.tbLastCalibra = new System.Windows.Forms.TextBox();
		this.label3 = new System.Windows.Forms.Label();
		this.tbCalibraCountDown = new System.Windows.Forms.TextBox();
		this.label1 = new System.Windows.Forms.Label();
		this.btnStartAutoCalibra = new System.Windows.Forms.Button();
		this.btnFireOnCheck = new System.Windows.Forms.Button();
		this.tbFireOn2 = new System.Windows.Forms.TextBox();
		this.tbFireOn = new System.Windows.Forms.TextBox();
		this.btnFireOnSet = new System.Windows.Forms.Button();
		this.button39 = new System.Windows.Forms.Button();
		this.LabHHS = new System.Windows.Forms.Label();
		this.labName1 = new System.Windows.Forms.Label();
		this.labStateIns = new System.Windows.Forms.Label();
		this.btnHighValueSet = new System.Windows.Forms.Button();
		this.btnHighValueCheck = new System.Windows.Forms.Button();
		this.label78 = new System.Windows.Forms.Label();
		this.tbHighV = new System.Windows.Forms.TextBox();
		this.chbFPDHighV = new System.Windows.Forms.CheckBox();
		this.labFPDhighV = new System.Windows.Forms.Label();
		this.label2 = new System.Windows.Forms.Label();
		this.btShowDesktop = new System.Windows.Forms.Button();
		this.button36 = new System.Windows.Forms.Button();
		this.NetConfig = new System.Windows.Forms.Button();
		this.button2 = new System.Windows.Forms.Button();
		this.pibChannel3 = new System.Windows.Forms.PictureBox();
		this.chbChannel1 = new System.Windows.Forms.CheckBox();
		this.chbChannel2 = new System.Windows.Forms.CheckBox();
		this.chbChannel3 = new System.Windows.Forms.CheckBox();
		this.btnStart = new System.Windows.Forms.Button();
		this.btnSet = new System.Windows.Forms.Button();
		this.pibChannel8 = new System.Windows.Forms.PictureBox();
		this.label84 = new System.Windows.Forms.Label();
		this.pibChannel7 = new System.Windows.Forms.PictureBox();
		this.chbChannel8 = new System.Windows.Forms.CheckBox();
		this.pibChannel6 = new System.Windows.Forms.PictureBox();
		this.chbChannel7 = new System.Windows.Forms.CheckBox();
		this.chbChannel4 = new System.Windows.Forms.CheckBox();
		this.pibChannel5 = new System.Windows.Forms.PictureBox();
		this.label83 = new System.Windows.Forms.Label();
		this.label82 = new System.Windows.Forms.Label();
		this.label94 = new System.Windows.Forms.Label();
		this.label95 = new System.Windows.Forms.Label();
		this.label96 = new System.Windows.Forms.Label();
		this.chbChannel6 = new System.Windows.Forms.CheckBox();
		this.label97 = new System.Windows.Forms.Label();
		this.chbChannel5 = new System.Windows.Forms.CheckBox();
		this.tbComTimes = new System.Windows.Forms.TextBox();
		this.tbInjecTime = new System.Windows.Forms.TextBox();
		this.label98 = new System.Windows.Forms.Label();
		this.tbAnyTime = new System.Windows.Forms.TextBox();
		this.tbCycleTimes = new System.Windows.Forms.TextBox();
		this.pibChannel1 = new System.Windows.Forms.PictureBox();
		this.pibChannel4 = new System.Windows.Forms.PictureBox();
		this.pibChannel2 = new System.Windows.Forms.PictureBox();
		this.timer1 = new System.Windows.Forms.Timer(this.components);
		this.tabControl1 = new System.Windows.Forms.TabControl();
		this.tabPage1 = new System.Windows.Forms.TabPage();
		this.chbSul = new System.Windows.Forms.CheckBox();
		this.btnHistory = new HZH_Controls.Controls.UCBtnExt();
		this.LabHHS2 = new System.Windows.Forms.Label();
		this.labData2 = new System.Windows.Forms.Label();
		this.groupBox3 = new System.Windows.Forms.GroupBox();
		this.dgFpd2 = new HZH_Controls.Controls.UCDataGridView();
		this.chbFPDHighV2 = new System.Windows.Forms.CheckBox();
		this.label8 = new System.Windows.Forms.Label();
		this.labFPDhighV2 = new System.Windows.Forms.Label();
		this.tbHighV2 = new System.Windows.Forms.TextBox();
		this.button1 = new System.Windows.Forms.Button();
		this.btnHighValueSet2 = new System.Windows.Forms.Button();
		this.labName2 = new System.Windows.Forms.Label();
		this.groupBox1 = new System.Windows.Forms.GroupBox();
		this.dgFpd1 = new HZH_Controls.Controls.UCDataGridView();
		this.label10 = new System.Windows.Forms.Label();
		this.tabPage2 = new System.Windows.Forms.TabPage();
		this.tabPage3 = new System.Windows.Forms.TabPage();
		this.labThermTemp = new System.Windows.Forms.Label();
		this.groupBox2.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.pibChannel3).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.pibChannel8).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.pibChannel7).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.pibChannel6).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.pibChannel5).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.pibChannel1).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.pibChannel4).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.pibChannel2).BeginInit();
		this.tabControl1.SuspendLayout();
		this.tabPage1.SuspendLayout();
		this.groupBox3.SuspendLayout();
		this.groupBox1.SuspendLayout();
		this.tabPage2.SuspendLayout();
		this.tabPage3.SuspendLayout();
		base.SuspendLayout();
		this.btnCali.BackColor = System.Drawing.Color.Lime;
		this.btnCali.Location = new System.Drawing.Point(735, 146);
		this.btnCali.Name = "btnCali";
		this.btnCali.Size = new System.Drawing.Size(87, 40);
		this.btnCali.TabIndex = 110;
		this.btnCali.Text = "一键标定";
		this.btnCali.UseVisualStyleBackColor = false;
		this.btnCali.Click += new System.EventHandler(btnCali_Click);
		this.groupBox2.Controls.Add(this.tbModbusRegister);
		this.groupBox2.Controls.Add(this.label6);
		this.groupBox2.Controls.Add(this.btnModbusSave);
		this.groupBox2.Controls.Add(this.tbModbusAddress);
		this.groupBox2.Controls.Add(this.label31);
		this.groupBox2.Location = new System.Drawing.Point(267, 12);
		this.groupBox2.Name = "groupBox2";
		this.groupBox2.Size = new System.Drawing.Size(207, 155);
		this.groupBox2.TabIndex = 109;
		this.groupBox2.TabStop = false;
		this.groupBox2.Text = "modbus站号";
		this.tbModbusRegister.Location = new System.Drawing.Point(86, 49);
		this.tbModbusRegister.Name = "tbModbusRegister";
		this.tbModbusRegister.Size = new System.Drawing.Size(79, 21);
		this.tbModbusRegister.TabIndex = 10;
		this.label6.AutoSize = true;
		this.label6.Location = new System.Drawing.Point(15, 52);
		this.label6.Name = "label6";
		this.label6.Size = new System.Drawing.Size(65, 12);
		this.label6.TabIndex = 9;
		this.label6.Text = "起始地址：";
		this.btnModbusSave.Location = new System.Drawing.Point(86, 80);
		this.btnModbusSave.Name = "btnModbusSave";
		this.btnModbusSave.Size = new System.Drawing.Size(75, 23);
		this.btnModbusSave.TabIndex = 8;
		this.btnModbusSave.Text = "保存并应用";
		this.btnModbusSave.UseVisualStyleBackColor = true;
		this.btnModbusSave.Click += new System.EventHandler(btnModbusSave_Click);
		this.tbModbusAddress.Location = new System.Drawing.Point(86, 22);
		this.tbModbusAddress.Name = "tbModbusAddress";
		this.tbModbusAddress.Size = new System.Drawing.Size(79, 21);
		this.tbModbusAddress.TabIndex = 7;
		this.label31.AutoSize = true;
		this.label31.Location = new System.Drawing.Point(15, 25);
		this.label31.Name = "label31";
		this.label31.Size = new System.Drawing.Size(65, 12);
		this.label31.TabIndex = 6;
		this.label31.Text = "上传站号：";
		this.tbTotalSAlarm.Location = new System.Drawing.Point(118, 9);
		this.tbTotalSAlarm.Name = "tbTotalSAlarm";
		this.tbTotalSAlarm.Size = new System.Drawing.Size(119, 21);
		this.tbTotalSAlarm.TabIndex = 108;
		this.tbTotalSAlarm.TextChanged += new System.EventHandler(tbTotalSAlarm_TextChanged);
		this.label4.AutoSize = true;
		this.label4.Location = new System.Drawing.Point(11, 12);
		this.label4.Name = "label4";
		this.label4.Size = new System.Drawing.Size(65, 12);
		this.label4.TabIndex = 107;
		this.label4.Text = "总硫报警值";
		this.tbHSAlarm.Location = new System.Drawing.Point(118, 34);
		this.tbHSAlarm.Name = "tbHSAlarm";
		this.tbHSAlarm.Size = new System.Drawing.Size(119, 21);
		this.tbHSAlarm.TabIndex = 106;
		this.tbHSAlarm.TextChanged += new System.EventHandler(tbHSAlarm_TextChanged);
		this.label5.AutoSize = true;
		this.label5.Location = new System.Drawing.Point(11, 37);
		this.label5.Name = "label5";
		this.label5.Size = new System.Drawing.Size(77, 12);
		this.label5.TabIndex = 105;
		this.label5.Text = "硫化氢报警值";
		this.labLevel.AutoSize = true;
		this.labLevel.Location = new System.Drawing.Point(12, 15);
		this.labLevel.Name = "labLevel";
		this.labLevel.Size = new System.Drawing.Size(41, 12);
		this.labLevel.TabIndex = 104;
		this.labLevel.Text = "label4";
		this.labLevel.Visible = false;
		this.labState.AutoSize = true;
		this.labState.Location = new System.Drawing.Point(736, 10);
		this.labState.Name = "labState";
		this.labState.Size = new System.Drawing.Size(53, 12);
		this.labState.TabIndex = 103;
		this.labState.Text = "正常运行";
		this.tbLastCalibra.Location = new System.Drawing.Point(267, 110);
		this.tbLastCalibra.Name = "tbLastCalibra";
		this.tbLastCalibra.Size = new System.Drawing.Size(119, 21);
		this.tbLastCalibra.TabIndex = 102;
		this.tbLastCalibra.Visible = false;
		this.label3.AutoSize = true;
		this.label3.Location = new System.Drawing.Point(160, 113);
		this.label3.Name = "label3";
		this.label3.Size = new System.Drawing.Size(77, 12);
		this.label3.TabIndex = 101;
		this.label3.Text = "上次自动标定";
		this.label3.Visible = false;
		this.tbCalibraCountDown.Location = new System.Drawing.Point(267, 143);
		this.tbCalibraCountDown.Name = "tbCalibraCountDown";
		this.tbCalibraCountDown.Size = new System.Drawing.Size(119, 21);
		this.tbCalibraCountDown.TabIndex = 100;
		this.tbCalibraCountDown.Visible = false;
		this.label1.AutoSize = true;
		this.label1.Location = new System.Drawing.Point(160, 146);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(89, 12);
		this.label1.TabIndex = 99;
		this.label1.Text = "自动标定倒计时";
		this.label1.Visible = false;
		this.btnStartAutoCalibra.Location = new System.Drawing.Point(64, 141);
		this.btnStartAutoCalibra.Name = "btnStartAutoCalibra";
		this.btnStartAutoCalibra.Size = new System.Drawing.Size(86, 23);
		this.btnStartAutoCalibra.TabIndex = 98;
		this.btnStartAutoCalibra.Text = "启用自动标定";
		this.btnStartAutoCalibra.UseVisualStyleBackColor = true;
		this.btnStartAutoCalibra.Visible = false;
		this.btnStartAutoCalibra.Click += new System.EventHandler(btnStartAutoCalibra_Click);
		this.btnFireOnCheck.Location = new System.Drawing.Point(272, 4);
		this.btnFireOnCheck.Name = "btnFireOnCheck";
		this.btnFireOnCheck.Size = new System.Drawing.Size(85, 23);
		this.btnFireOnCheck.TabIndex = 94;
		this.btnFireOnCheck.Text = "点火门限查询";
		this.btnFireOnCheck.UseVisualStyleBackColor = true;
		this.btnFireOnCheck.Click += new System.EventHandler(BtnFireOnCheck_Click_1);
		this.tbFireOn2.Location = new System.Drawing.Point(517, 5);
		this.tbFireOn2.Name = "tbFireOn2";
		this.tbFireOn2.Size = new System.Drawing.Size(56, 21);
		this.tbFireOn2.TabIndex = 96;
		this.tbFireOn.Location = new System.Drawing.Point(451, 5);
		this.tbFireOn.Name = "tbFireOn";
		this.tbFireOn.Size = new System.Drawing.Size(58, 21);
		this.tbFireOn.TabIndex = 92;
		this.btnFireOnSet.Location = new System.Drawing.Point(355, 4);
		this.btnFireOnSet.Name = "btnFireOnSet";
		this.btnFireOnSet.Size = new System.Drawing.Size(91, 23);
		this.btnFireOnSet.TabIndex = 95;
		this.btnFireOnSet.Text = "点火门限设定";
		this.btnFireOnSet.UseVisualStyleBackColor = true;
		this.btnFireOnSet.Click += new System.EventHandler(BtnFireOnSet_Click);
		this.button39.Location = new System.Drawing.Point(63, 108);
		this.button39.Name = "button39";
		this.button39.Size = new System.Drawing.Size(86, 23);
		this.button39.TabIndex = 87;
		this.button39.Text = "自动标定设置";
		this.button39.UseVisualStyleBackColor = true;
		this.button39.Visible = false;
		this.button39.Click += new System.EventHandler(button39_Click);
		this.LabHHS.Cursor = System.Windows.Forms.Cursors.Hand;
		this.LabHHS.Location = new System.Drawing.Point(727, 50);
		this.LabHHS.Name = "LabHHS";
		this.LabHHS.Size = new System.Drawing.Size(68, 14);
		this.LabHHS.TabIndex = 86;
		this.LabHHS.Text = "0";
		this.LabHHS.Visible = false;
		this.LabHHS.Click += new System.EventHandler(LabHHS_Click);
		this.labName1.AutoSize = true;
		this.labName1.Location = new System.Drawing.Point(674, 50);
		this.labName1.Name = "labName1";
		this.labName1.Size = new System.Drawing.Size(47, 12);
		this.labName1.TabIndex = 85;
		this.labName1.Text = "组份1：";
		this.labName1.Visible = false;
		this.labStateIns.AutoSize = true;
		this.labStateIns.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
		this.labStateIns.Location = new System.Drawing.Point(699, 133);
		this.labStateIns.Name = "labStateIns";
		this.labStateIns.Size = new System.Drawing.Size(67, 14);
		this.labStateIns.TabIndex = 84;
		this.labStateIns.Text = "非在线循环";
		this.labStateIns.Visible = false;
		this.btnHighValueSet.Location = new System.Drawing.Point(188, 86);
		this.btnHighValueSet.Name = "btnHighValueSet";
		this.btnHighValueSet.Size = new System.Drawing.Size(86, 23);
		this.btnHighValueSet.TabIndex = 83;
		this.btnHighValueSet.Text = "高压设定";
		this.btnHighValueSet.UseVisualStyleBackColor = true;
		this.btnHighValueSet.Click += new System.EventHandler(btnHighValueSet_Click);
		this.btnHighValueCheck.Location = new System.Drawing.Point(188, 60);
		this.btnHighValueCheck.Name = "btnHighValueCheck";
		this.btnHighValueCheck.Size = new System.Drawing.Size(86, 23);
		this.btnHighValueCheck.TabIndex = 82;
		this.btnHighValueCheck.Text = "高压查询";
		this.btnHighValueCheck.UseVisualStyleBackColor = true;
		this.btnHighValueCheck.Click += new System.EventHandler(btnHighValueCheck_Click);
		this.label78.AutoSize = true;
		this.label78.Location = new System.Drawing.Point(188, 37);
		this.label78.Name = "label78";
		this.label78.Size = new System.Drawing.Size(59, 12);
		this.label78.TabIndex = 81;
		this.label78.Text = "高压设定:";
		this.tbHighV.Location = new System.Drawing.Point(188, 34);
		this.tbHighV.Name = "tbHighV";
		this.tbHighV.Size = new System.Drawing.Size(68, 21);
		this.tbHighV.TabIndex = 80;
		this.chbFPDHighV.AutoSize = true;
		this.chbFPDHighV.BackColor = System.Drawing.Color.Red;
		this.chbFPDHighV.Cursor = System.Windows.Forms.Cursors.Hand;
		this.chbFPDHighV.Font = new System.Drawing.Font("宋体", 24f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		this.chbFPDHighV.ForeColor = System.Drawing.Color.Black;
		this.chbFPDHighV.Location = new System.Drawing.Point(188, 119);
		this.chbFPDHighV.Name = "chbFPDHighV";
		this.chbFPDHighV.Size = new System.Drawing.Size(98, 37);
		this.chbFPDHighV.TabIndex = 79;
		this.chbFPDHighV.Text = "高压";
		this.chbFPDHighV.UseVisualStyleBackColor = false;
		this.chbFPDHighV.CheckedChanged += new System.EventHandler(chbFPDHighV_CheckedChanged);
		this.chbFPDHighV.MouseClick += new System.Windows.Forms.MouseEventHandler(ChbFPDHighV_MouseClick);
		this.labFPDhighV.Font = new System.Drawing.Font("宋体", 9f);
		this.labFPDhighV.Location = new System.Drawing.Point(238, 14);
		this.labFPDhighV.Name = "labFPDhighV";
		this.labFPDhighV.Size = new System.Drawing.Size(68, 16);
		this.labFPDhighV.TabIndex = 78;
		this.labFPDhighV.Text = "0";
		this.label2.AutoSize = true;
		this.label2.Font = new System.Drawing.Font("宋体", 9f);
		this.label2.Location = new System.Drawing.Point(186, 14);
		this.label2.Name = "label2";
		this.label2.Size = new System.Drawing.Size(53, 12);
		this.label2.TabIndex = 77;
		this.label2.Text = "高  压：";
		this.btShowDesktop.Location = new System.Drawing.Point(194, 5);
		this.btShowDesktop.Name = "btShowDesktop";
		this.btShowDesktop.Size = new System.Drawing.Size(75, 23);
		this.btShowDesktop.TabIndex = 76;
		this.btShowDesktop.Text = "显示桌面";
		this.btShowDesktop.UseVisualStyleBackColor = true;
		this.btShowDesktop.Click += new System.EventHandler(btShowDesktop_Click);
		this.button36.Location = new System.Drawing.Point(120, 5);
		this.button36.Name = "button36";
		this.button36.Size = new System.Drawing.Size(75, 23);
		this.button36.TabIndex = 75;
		this.button36.Text = "监控界面";
		this.button36.UseVisualStyleBackColor = true;
		this.NetConfig.Location = new System.Drawing.Point(49, 5);
		this.NetConfig.Name = "NetConfig";
		this.NetConfig.Size = new System.Drawing.Size(72, 23);
		this.NetConfig.TabIndex = 74;
		this.NetConfig.Text = "网络配置";
		this.NetConfig.UseVisualStyleBackColor = true;
		this.NetConfig.Click += new System.EventHandler(NetConfig_Click);
		this.button2.Image = (System.Drawing.Image)resources.GetObject("button2.Image");
		this.button2.Location = new System.Drawing.Point(6, 6);
		this.button2.Name = "button2";
		this.button2.Size = new System.Drawing.Size(43, 21);
		this.button2.TabIndex = 73;
		this.button2.UseVisualStyleBackColor = true;
		this.button2.Click += new System.EventHandler(button2_Click);
		this.pibChannel3.Image = IBrainChrom2018.Properties.Resources.x12;
		this.pibChannel3.Location = new System.Drawing.Point(202, 14);
		this.pibChannel3.Name = "pibChannel3";
		this.pibChannel3.Size = new System.Drawing.Size(63, 63);
		this.pibChannel3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
		this.pibChannel3.TabIndex = 32;
		this.pibChannel3.TabStop = false;
		this.chbChannel1.AutoSize = true;
		this.chbChannel1.Location = new System.Drawing.Point(83, 61);
		this.chbChannel1.Name = "chbChannel1";
		this.chbChannel1.Size = new System.Drawing.Size(15, 14);
		this.chbChannel1.TabIndex = 12;
		this.chbChannel1.UseVisualStyleBackColor = true;
		this.chbChannel1.CheckedChanged += new System.EventHandler(chbChannel1_CheckedChanged);
		this.chbChannel2.AutoSize = true;
		this.chbChannel2.Location = new System.Drawing.Point(177, 61);
		this.chbChannel2.Name = "chbChannel2";
		this.chbChannel2.Size = new System.Drawing.Size(15, 14);
		this.chbChannel2.TabIndex = 13;
		this.chbChannel2.UseVisualStyleBackColor = true;
		this.chbChannel2.CheckedChanged += new System.EventHandler(chbChannel2_CheckedChanged);
		this.chbChannel3.AutoSize = true;
		this.chbChannel3.Location = new System.Drawing.Point(283, 61);
		this.chbChannel3.Name = "chbChannel3";
		this.chbChannel3.Size = new System.Drawing.Size(15, 14);
		this.chbChannel3.TabIndex = 14;
		this.chbChannel3.UseVisualStyleBackColor = true;
		this.chbChannel3.CheckedChanged += new System.EventHandler(chbChannel3_CheckedChanged);
		this.btnStart.Location = new System.Drawing.Point(414, 130);
		this.btnStart.Name = "btnStart";
		this.btnStart.Size = new System.Drawing.Size(146, 29);
		this.btnStart.TabIndex = 28;
		this.btnStart.Text = "开始采集";
		this.btnStart.UseVisualStyleBackColor = true;
		this.btnStart.Click += new System.EventHandler(btnStart_Click);
		this.btnSet.Location = new System.Drawing.Point(414, 101);
		this.btnSet.Name = "btnSet";
		this.btnSet.Size = new System.Drawing.Size(146, 29);
		this.btnSet.TabIndex = 29;
		this.btnSet.Text = "参数设定";
		this.btnSet.UseVisualStyleBackColor = true;
		this.btnSet.Click += new System.EventHandler(btnSet_Click);
		this.pibChannel8.Image = (System.Drawing.Image)resources.GetObject("pibChannel8.Image");
		this.pibChannel8.Location = new System.Drawing.Point(309, 84);
		this.pibChannel8.Name = "pibChannel8";
		this.pibChannel8.Size = new System.Drawing.Size(63, 63);
		this.pibChannel8.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
		this.pibChannel8.TabIndex = 37;
		this.pibChannel8.TabStop = false;
		this.label84.AutoSize = true;
		this.label84.Location = new System.Drawing.Point(530, 82);
		this.label84.Name = "label84";
		this.label84.Size = new System.Drawing.Size(17, 12);
		this.label84.TabIndex = 41;
		this.label84.Text = "次";
		this.pibChannel7.Image = (System.Drawing.Image)resources.GetObject("pibChannel7.Image");
		this.pibChannel7.Location = new System.Drawing.Point(202, 83);
		this.pibChannel7.Name = "pibChannel7";
		this.pibChannel7.Size = new System.Drawing.Size(63, 63);
		this.pibChannel7.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
		this.pibChannel7.TabIndex = 36;
		this.pibChannel7.TabStop = false;
		this.chbChannel8.AutoSize = true;
		this.chbChannel8.Location = new System.Drawing.Point(380, 122);
		this.chbChannel8.Name = "chbChannel8";
		this.chbChannel8.Size = new System.Drawing.Size(15, 14);
		this.chbChannel8.TabIndex = 19;
		this.chbChannel8.UseVisualStyleBackColor = true;
		this.chbChannel8.CheckedChanged += new System.EventHandler(chbChannel8_CheckedChanged);
		this.pibChannel6.Image = (System.Drawing.Image)resources.GetObject("pibChannel6.Image");
		this.pibChannel6.Location = new System.Drawing.Point(104, 84);
		this.pibChannel6.Name = "pibChannel6";
		this.pibChannel6.Size = new System.Drawing.Size(63, 63);
		this.pibChannel6.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
		this.pibChannel6.TabIndex = 35;
		this.pibChannel6.TabStop = false;
		this.chbChannel7.AutoSize = true;
		this.chbChannel7.Location = new System.Drawing.Point(280, 122);
		this.chbChannel7.Name = "chbChannel7";
		this.chbChannel7.Size = new System.Drawing.Size(15, 14);
		this.chbChannel7.TabIndex = 18;
		this.chbChannel7.UseVisualStyleBackColor = true;
		this.chbChannel7.CheckedChanged += new System.EventHandler(chbChannel7_CheckedChanged);
		this.chbChannel4.AutoSize = true;
		this.chbChannel4.Location = new System.Drawing.Point(380, 61);
		this.chbChannel4.Name = "chbChannel4";
		this.chbChannel4.Size = new System.Drawing.Size(15, 14);
		this.chbChannel4.TabIndex = 15;
		this.chbChannel4.UseVisualStyleBackColor = true;
		this.chbChannel4.CheckedChanged += new System.EventHandler(chbChannel4_CheckedChanged);
		this.pibChannel5.Image = (System.Drawing.Image)resources.GetObject("pibChannel5.Image");
		this.pibChannel5.Location = new System.Drawing.Point(14, 84);
		this.pibChannel5.Name = "pibChannel5";
		this.pibChannel5.Size = new System.Drawing.Size(63, 63);
		this.pibChannel5.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
		this.pibChannel5.TabIndex = 34;
		this.pibChannel5.TabStop = false;
		this.label83.AutoSize = true;
		this.label83.Location = new System.Drawing.Point(530, 59);
		this.label83.Name = "label83";
		this.label83.Size = new System.Drawing.Size(17, 12);
		this.label83.TabIndex = 40;
		this.label83.Text = "次";
		this.label82.AutoSize = true;
		this.label82.Location = new System.Drawing.Point(415, 11);
		this.label82.Name = "label82";
		this.label82.Size = new System.Drawing.Size(53, 12);
		this.label82.TabIndex = 20;
		this.label82.Text = "进样时间";
		this.label94.AutoSize = true;
		this.label94.Location = new System.Drawing.Point(530, 36);
		this.label94.Name = "label94";
		this.label94.Size = new System.Drawing.Size(29, 12);
		this.label94.TabIndex = 39;
		this.label94.Text = "分钟";
		this.label95.AutoSize = true;
		this.label95.Location = new System.Drawing.Point(415, 36);
		this.label95.Name = "label95";
		this.label95.Size = new System.Drawing.Size(53, 12);
		this.label95.TabIndex = 21;
		this.label95.Text = "分析时间";
		this.label96.AutoSize = true;
		this.label96.Location = new System.Drawing.Point(530, 12);
		this.label96.Name = "label96";
		this.label96.Size = new System.Drawing.Size(29, 12);
		this.label96.TabIndex = 38;
		this.label96.Text = "分钟";
		this.chbChannel6.AutoSize = true;
		this.chbChannel6.Location = new System.Drawing.Point(180, 122);
		this.chbChannel6.Name = "chbChannel6";
		this.chbChannel6.Size = new System.Drawing.Size(15, 14);
		this.chbChannel6.TabIndex = 17;
		this.chbChannel6.UseVisualStyleBackColor = true;
		this.chbChannel6.CheckedChanged += new System.EventHandler(chbChannel6_CheckedChanged);
		this.label97.AutoSize = true;
		this.label97.Location = new System.Drawing.Point(415, 60);
		this.label97.Name = "label97";
		this.label97.Size = new System.Drawing.Size(53, 12);
		this.label97.TabIndex = 22;
		this.label97.Text = "循环次数";
		this.chbChannel5.AutoSize = true;
		this.chbChannel5.Location = new System.Drawing.Point(83, 122);
		this.chbChannel5.Name = "chbChannel5";
		this.chbChannel5.Size = new System.Drawing.Size(15, 14);
		this.chbChannel5.TabIndex = 16;
		this.chbChannel5.UseVisualStyleBackColor = true;
		this.chbChannel5.CheckedChanged += new System.EventHandler(chbChannel5_CheckedChanged);
		this.tbComTimes.Location = new System.Drawing.Point(474, 79);
		this.tbComTimes.Name = "tbComTimes";
		this.tbComTimes.ReadOnly = true;
		this.tbComTimes.Size = new System.Drawing.Size(48, 21);
		this.tbComTimes.TabIndex = 27;
		this.tbInjecTime.Location = new System.Drawing.Point(474, 8);
		this.tbInjecTime.Name = "tbInjecTime";
		this.tbInjecTime.Size = new System.Drawing.Size(48, 21);
		this.tbInjecTime.TabIndex = 24;
		this.label98.AutoSize = true;
		this.label98.Location = new System.Drawing.Point(415, 84);
		this.label98.Name = "label98";
		this.label98.Size = new System.Drawing.Size(53, 12);
		this.label98.TabIndex = 23;
		this.label98.Text = "完成次数";
		this.tbAnyTime.Location = new System.Drawing.Point(474, 31);
		this.tbAnyTime.Name = "tbAnyTime";
		this.tbAnyTime.Size = new System.Drawing.Size(48, 21);
		this.tbAnyTime.TabIndex = 25;
		this.tbCycleTimes.Location = new System.Drawing.Point(474, 55);
		this.tbCycleTimes.Name = "tbCycleTimes";
		this.tbCycleTimes.Size = new System.Drawing.Size(48, 21);
		this.tbCycleTimes.TabIndex = 26;
		this.pibChannel1.Image = IBrainChrom2018.Properties.Resources.x12;
		this.pibChannel1.Location = new System.Drawing.Point(14, 14);
		this.pibChannel1.Name = "pibChannel1";
		this.pibChannel1.Size = new System.Drawing.Size(63, 63);
		this.pibChannel1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
		this.pibChannel1.TabIndex = 30;
		this.pibChannel1.TabStop = false;
		this.pibChannel4.Image = IBrainChrom2018.Properties.Resources.x12;
		this.pibChannel4.Location = new System.Drawing.Point(307, 15);
		this.pibChannel4.Name = "pibChannel4";
		this.pibChannel4.Size = new System.Drawing.Size(63, 63);
		this.pibChannel4.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
		this.pibChannel4.TabIndex = 33;
		this.pibChannel4.TabStop = false;
		this.pibChannel2.Image = IBrainChrom2018.Properties.Resources.x12;
		this.pibChannel2.Location = new System.Drawing.Point(104, 15);
		this.pibChannel2.Name = "pibChannel2";
		this.pibChannel2.Size = new System.Drawing.Size(63, 63);
		this.pibChannel2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
		this.pibChannel2.TabIndex = 31;
		this.pibChannel2.TabStop = false;
		this.timer1.Enabled = true;
		this.timer1.Interval = 1000;
		this.timer1.Tick += new System.EventHandler(timer1_Tick);
		this.tabControl1.Controls.Add(this.tabPage1);
		this.tabControl1.Controls.Add(this.tabPage2);
		this.tabControl1.Controls.Add(this.tabPage3);
		this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
		this.tabControl1.Location = new System.Drawing.Point(3, 3);
		this.tabControl1.Name = "tabControl1";
		this.tabControl1.SelectedIndex = 0;
		this.tabControl1.Size = new System.Drawing.Size(836, 218);
		this.tabControl1.TabIndex = 111;
		this.tabPage1.Controls.Add(this.labThermTemp);
		this.tabPage1.Controls.Add(this.chbSul);
		this.tabPage1.Controls.Add(this.btnHistory);
		this.tabPage1.Controls.Add(this.LabHHS2);
		this.tabPage1.Controls.Add(this.labData2);
		this.tabPage1.Controls.Add(this.groupBox3);
		this.tabPage1.Controls.Add(this.labName2);
		this.tabPage1.Controls.Add(this.groupBox1);
		this.tabPage1.Controls.Add(this.label10);
		this.tabPage1.Controls.Add(this.labState);
		this.tabPage1.Controls.Add(this.LabHHS);
		this.tabPage1.Controls.Add(this.btnFireOnCheck);
		this.tabPage1.Controls.Add(this.btnCali);
		this.tabPage1.Controls.Add(this.tbFireOn2);
		this.tabPage1.Controls.Add(this.labName1);
		this.tabPage1.Controls.Add(this.tbFireOn);
		this.tabPage1.Controls.Add(this.btnFireOnSet);
		this.tabPage1.Controls.Add(this.button2);
		this.tabPage1.Controls.Add(this.NetConfig);
		this.tabPage1.Controls.Add(this.button36);
		this.tabPage1.Controls.Add(this.btShowDesktop);
		this.tabPage1.Location = new System.Drawing.Point(4, 22);
		this.tabPage1.Name = "tabPage1";
		this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
		this.tabPage1.Size = new System.Drawing.Size(828, 192);
		this.tabPage1.TabIndex = 0;
		this.tabPage1.Text = "数据";
		this.tabPage1.UseVisualStyleBackColor = true;
		this.chbSul.AutoSize = true;
		this.chbSul.Location = new System.Drawing.Point(635, 124);
		this.chbSul.Name = "chbSul";
		this.chbSul.Size = new System.Drawing.Size(84, 16);
		this.chbSul.TabIndex = 114;
		this.chbSul.Text = "硫化物方法";
		this.chbSul.UseVisualStyleBackColor = true;
		this.chbSul.Click += new System.EventHandler(chbSul_Click);
		this.btnHistory.BackColor = System.Drawing.Color.White;
		this.btnHistory.BtnBackColor = System.Drawing.Color.White;
		this.btnHistory.BtnFont = new System.Drawing.Font("微软雅黑", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		this.btnHistory.BtnForeColor = System.Drawing.Color.White;
		this.btnHistory.BtnText = "历史数据";
		this.btnHistory.ConerRadius = 5;
		this.btnHistory.Cursor = System.Windows.Forms.Cursors.Hand;
		this.btnHistory.EnabledMouseEffect = false;
		this.btnHistory.FillColor = System.Drawing.Color.FromArgb(255, 77, 59);
		this.btnHistory.Font = new System.Drawing.Font("微软雅黑", 15f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
		this.btnHistory.IsRadius = true;
		this.btnHistory.IsShowRect = true;
		this.btnHistory.IsShowTips = false;
		this.btnHistory.Location = new System.Drawing.Point(625, 146);
		this.btnHistory.Margin = new System.Windows.Forms.Padding(0);
		this.btnHistory.Name = "btnHistory";
		this.btnHistory.RectColor = System.Drawing.Color.FromArgb(255, 77, 58);
		this.btnHistory.RectWidth = 1;
		this.btnHistory.Size = new System.Drawing.Size(96, 40);
		this.btnHistory.TabIndex = 113;
		this.btnHistory.TabStop = false;
		this.btnHistory.TipsColor = System.Drawing.Color.FromArgb(232, 30, 99);
		this.btnHistory.TipsText = "";
		this.btnHistory.BtnClick += new System.EventHandler(btnHistory_BtnClick);
		this.LabHHS2.Cursor = System.Windows.Forms.Cursors.Hand;
		this.LabHHS2.Location = new System.Drawing.Point(721, 94);
		this.LabHHS2.Name = "LabHHS2";
		this.LabHHS2.Size = new System.Drawing.Size(68, 14);
		this.LabHHS2.TabIndex = 86;
		this.LabHHS2.Text = "0";
		this.LabHHS2.Visible = false;
		this.LabHHS2.Click += new System.EventHandler(LabHHS2_Click);
		this.labData2.Cursor = System.Windows.Forms.Cursors.Hand;
		this.labData2.Location = new System.Drawing.Point(727, 70);
		this.labData2.Name = "labData2";
		this.labData2.Size = new System.Drawing.Size(68, 14);
		this.labData2.TabIndex = 88;
		this.labData2.Text = "0";
		this.labData2.Visible = false;
		this.labData2.Click += new System.EventHandler(labData2_Click);
		this.groupBox3.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.groupBox3.Controls.Add(this.dgFpd2);
		this.groupBox3.Controls.Add(this.chbFPDHighV2);
		this.groupBox3.Controls.Add(this.label8);
		this.groupBox3.Controls.Add(this.labFPDhighV2);
		this.groupBox3.Controls.Add(this.tbHighV2);
		this.groupBox3.Controls.Add(this.button1);
		this.groupBox3.Controls.Add(this.btnHighValueSet2);
		this.groupBox3.Location = new System.Drawing.Point(314, 33);
		this.groupBox3.Name = "groupBox3";
		this.groupBox3.Size = new System.Drawing.Size(302, 156);
		this.groupBox3.TabIndex = 112;
		this.groupBox3.TabStop = false;
		this.groupBox3.Text = "检测器2";
		this.dgFpd2.BackColor = System.Drawing.Color.White;
		this.dgFpd2.Columns = null;
		this.dgFpd2.DataSource = null;
		this.dgFpd2.Dock = System.Windows.Forms.DockStyle.Left;
		this.dgFpd2.Font = new System.Drawing.Font("宋体", 6.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		this.dgFpd2.HeadFont = new System.Drawing.Font("微软雅黑", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		this.dgFpd2.HeadHeight = 0;
		this.dgFpd2.HeadPadingLeft = 0;
		this.dgFpd2.HeadTextColor = System.Drawing.Color.Black;
		this.dgFpd2.IsShowCheckBox = false;
		this.dgFpd2.IsShowHead = false;
		this.dgFpd2.Location = new System.Drawing.Point(3, 17);
		this.dgFpd2.Name = "dgFpd2";
		this.dgFpd2.RowFont = new System.Drawing.Font("微软雅黑", 12f);
		this.dgFpd2.RowHeight = 10;
		this.dgFpd2.RowType = typeof(HZH_Controls.Controls.UCDataGridViewRow);
		this.dgFpd2.Size = new System.Drawing.Size(177, 136);
		this.dgFpd2.TabIndex = 115;
		this.chbFPDHighV2.AutoSize = true;
		this.chbFPDHighV2.BackColor = System.Drawing.Color.Red;
		this.chbFPDHighV2.Cursor = System.Windows.Forms.Cursors.Hand;
		this.chbFPDHighV2.Font = new System.Drawing.Font("宋体", 24f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		this.chbFPDHighV2.ForeColor = System.Drawing.Color.Black;
		this.chbFPDHighV2.Location = new System.Drawing.Point(203, 119);
		this.chbFPDHighV2.Name = "chbFPDHighV2";
		this.chbFPDHighV2.Size = new System.Drawing.Size(98, 37);
		this.chbFPDHighV2.TabIndex = 79;
		this.chbFPDHighV2.Text = "高压";
		this.chbFPDHighV2.UseVisualStyleBackColor = false;
		this.chbFPDHighV2.CheckedChanged += new System.EventHandler(chbFPDHighV2_CheckedChanged);
		this.chbFPDHighV2.MouseClick += new System.Windows.Forms.MouseEventHandler(chbFPDHighV2_MouseClick);
		this.label8.AutoSize = true;
		this.label8.Font = new System.Drawing.Font("宋体", 9f);
		this.label8.Location = new System.Drawing.Point(201, 17);
		this.label8.Name = "label8";
		this.label8.Size = new System.Drawing.Size(53, 12);
		this.label8.TabIndex = 77;
		this.label8.Text = "高  压：";
		this.labFPDhighV2.Font = new System.Drawing.Font("宋体", 9f);
		this.labFPDhighV2.Location = new System.Drawing.Point(252, 18);
		this.labFPDhighV2.Name = "labFPDhighV2";
		this.labFPDhighV2.Size = new System.Drawing.Size(68, 16);
		this.labFPDhighV2.TabIndex = 78;
		this.labFPDhighV2.Text = "0";
		this.tbHighV2.Location = new System.Drawing.Point(203, 34);
		this.tbHighV2.Name = "tbHighV2";
		this.tbHighV2.Size = new System.Drawing.Size(68, 21);
		this.tbHighV2.TabIndex = 80;
		this.button1.Location = new System.Drawing.Point(203, 58);
		this.button1.Name = "button1";
		this.button1.Size = new System.Drawing.Size(86, 23);
		this.button1.TabIndex = 82;
		this.button1.Text = "高压查询";
		this.button1.UseVisualStyleBackColor = true;
		this.button1.Click += new System.EventHandler(button1_Click);
		this.btnHighValueSet2.Location = new System.Drawing.Point(203, 84);
		this.btnHighValueSet2.Name = "btnHighValueSet2";
		this.btnHighValueSet2.Size = new System.Drawing.Size(86, 23);
		this.btnHighValueSet2.TabIndex = 83;
		this.btnHighValueSet2.Text = "高压设定";
		this.btnHighValueSet2.UseVisualStyleBackColor = true;
		this.btnHighValueSet2.Click += new System.EventHandler(btnHighValueSet2_Click);
		this.labName2.AutoSize = true;
		this.labName2.Location = new System.Drawing.Point(674, 70);
		this.labName2.Name = "labName2";
		this.labName2.Size = new System.Drawing.Size(47, 12);
		this.labName2.TabIndex = 87;
		this.labName2.Text = "组份2：";
		this.labName2.Visible = false;
		this.groupBox1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.groupBox1.Controls.Add(this.dgFpd1);
		this.groupBox1.Controls.Add(this.chbFPDHighV);
		this.groupBox1.Controls.Add(this.label2);
		this.groupBox1.Controls.Add(this.labFPDhighV);
		this.groupBox1.Controls.Add(this.tbHighV);
		this.groupBox1.Controls.Add(this.label78);
		this.groupBox1.Controls.Add(this.btnHighValueCheck);
		this.groupBox1.Controls.Add(this.btnHighValueSet);
		this.groupBox1.Location = new System.Drawing.Point(6, 33);
		this.groupBox1.Name = "groupBox1";
		this.groupBox1.Size = new System.Drawing.Size(302, 156);
		this.groupBox1.TabIndex = 111;
		this.groupBox1.TabStop = false;
		this.groupBox1.Text = "检测器1";
		this.dgFpd1.BackColor = System.Drawing.Color.White;
		this.dgFpd1.Columns = null;
		this.dgFpd1.DataSource = null;
		this.dgFpd1.Dock = System.Windows.Forms.DockStyle.Left;
		this.dgFpd1.Font = new System.Drawing.Font("宋体", 6.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		this.dgFpd1.HeadFont = new System.Drawing.Font("微软雅黑", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		this.dgFpd1.HeadHeight = 0;
		this.dgFpd1.HeadPadingLeft = 0;
		this.dgFpd1.HeadTextColor = System.Drawing.Color.Black;
		this.dgFpd1.IsShowCheckBox = false;
		this.dgFpd1.IsShowHead = false;
		this.dgFpd1.Location = new System.Drawing.Point(3, 17);
		this.dgFpd1.Name = "dgFpd1";
		this.dgFpd1.RowFont = new System.Drawing.Font("微软雅黑", 12f);
		this.dgFpd1.RowHeight = 10;
		this.dgFpd1.RowType = typeof(HZH_Controls.Controls.UCDataGridViewRow);
		this.dgFpd1.Size = new System.Drawing.Size(177, 136);
		this.dgFpd1.TabIndex = 114;
		this.label10.AutoSize = true;
		this.label10.Location = new System.Drawing.Point(668, 94);
		this.label10.Name = "label10";
		this.label10.Size = new System.Drawing.Size(53, 12);
		this.label10.TabIndex = 85;
		this.label10.Text = "硫化氢：";
		this.label10.Visible = false;
		this.tabPage2.Controls.Add(this.groupBox2);
		this.tabPage2.Controls.Add(this.tbHSAlarm);
		this.tabPage2.Controls.Add(this.label4);
		this.tabPage2.Controls.Add(this.button39);
		this.tabPage2.Controls.Add(this.tbTotalSAlarm);
		this.tabPage2.Controls.Add(this.btnStartAutoCalibra);
		this.tabPage2.Controls.Add(this.label5);
		this.tabPage2.Controls.Add(this.label1);
		this.tabPage2.Controls.Add(this.tbCalibraCountDown);
		this.tabPage2.Controls.Add(this.label3);
		this.tabPage2.Controls.Add(this.tbLastCalibra);
		this.tabPage2.Location = new System.Drawing.Point(4, 22);
		this.tabPage2.Name = "tabPage2";
		this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
		this.tabPage2.Size = new System.Drawing.Size(828, 192);
		this.tabPage2.TabIndex = 1;
		this.tabPage2.Text = "参数设置";
		this.tabPage2.UseVisualStyleBackColor = true;
		this.tabPage3.Controls.Add(this.labStateIns);
		this.tabPage3.Controls.Add(this.pibChannel3);
		this.tabPage3.Controls.Add(this.pibChannel2);
		this.tabPage3.Controls.Add(this.chbChannel1);
		this.tabPage3.Controls.Add(this.pibChannel4);
		this.tabPage3.Controls.Add(this.chbChannel2);
		this.tabPage3.Controls.Add(this.pibChannel1);
		this.tabPage3.Controls.Add(this.chbChannel3);
		this.tabPage3.Controls.Add(this.tbCycleTimes);
		this.tabPage3.Controls.Add(this.btnStart);
		this.tabPage3.Controls.Add(this.tbAnyTime);
		this.tabPage3.Controls.Add(this.btnSet);
		this.tabPage3.Controls.Add(this.label98);
		this.tabPage3.Controls.Add(this.labLevel);
		this.tabPage3.Controls.Add(this.tbInjecTime);
		this.tabPage3.Controls.Add(this.pibChannel8);
		this.tabPage3.Controls.Add(this.tbComTimes);
		this.tabPage3.Controls.Add(this.label84);
		this.tabPage3.Controls.Add(this.chbChannel5);
		this.tabPage3.Controls.Add(this.pibChannel7);
		this.tabPage3.Controls.Add(this.label97);
		this.tabPage3.Controls.Add(this.chbChannel8);
		this.tabPage3.Controls.Add(this.chbChannel6);
		this.tabPage3.Controls.Add(this.pibChannel6);
		this.tabPage3.Controls.Add(this.label96);
		this.tabPage3.Controls.Add(this.chbChannel7);
		this.tabPage3.Controls.Add(this.label95);
		this.tabPage3.Controls.Add(this.chbChannel4);
		this.tabPage3.Controls.Add(this.label94);
		this.tabPage3.Controls.Add(this.pibChannel5);
		this.tabPage3.Controls.Add(this.label82);
		this.tabPage3.Controls.Add(this.label83);
		this.tabPage3.Location = new System.Drawing.Point(4, 22);
		this.tabPage3.Name = "tabPage3";
		this.tabPage3.Size = new System.Drawing.Size(828, 192);
		this.tabPage3.TabIndex = 2;
		this.tabPage3.Text = "循环采集";
		this.tabPage3.UseVisualStyleBackColor = true;
		this.labThermTemp.AutoSize = true;
		this.labThermTemp.Location = new System.Drawing.Point(581, 9);
		this.labThermTemp.Name = "labThermTemp";
		this.labThermTemp.Size = new System.Drawing.Size(77, 12);
		this.labThermTemp.TabIndex = 115;
		this.labThermTemp.Text = "热电偶温度：";
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.Controls.Add(this.tabControl1);
		base.Name = "MicrFPDCtrl";
		base.Padding = new System.Windows.Forms.Padding(3);
		base.Size = new System.Drawing.Size(842, 224);
		this.groupBox2.ResumeLayout(false);
		this.groupBox2.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.pibChannel3).EndInit();
		((System.ComponentModel.ISupportInitialize)this.pibChannel8).EndInit();
		((System.ComponentModel.ISupportInitialize)this.pibChannel7).EndInit();
		((System.ComponentModel.ISupportInitialize)this.pibChannel6).EndInit();
		((System.ComponentModel.ISupportInitialize)this.pibChannel5).EndInit();
		((System.ComponentModel.ISupportInitialize)this.pibChannel1).EndInit();
		((System.ComponentModel.ISupportInitialize)this.pibChannel4).EndInit();
		((System.ComponentModel.ISupportInitialize)this.pibChannel2).EndInit();
		this.tabControl1.ResumeLayout(false);
		this.tabPage1.ResumeLayout(false);
		this.tabPage1.PerformLayout();
		this.groupBox3.ResumeLayout(false);
		this.groupBox3.PerformLayout();
		this.groupBox1.ResumeLayout(false);
		this.groupBox1.PerformLayout();
		this.tabPage2.ResumeLayout(false);
		this.tabPage2.PerformLayout();
		this.tabPage3.ResumeLayout(false);
		this.tabPage3.PerformLayout();
		base.ResumeLayout(false);
	}
}
