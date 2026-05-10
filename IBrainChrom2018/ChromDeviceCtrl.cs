using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using IBrainChrom2018.Unit;

namespace IBrainChrom2018;

public class ChromDeviceCtrl : UserControl
{
	private ChromDeviceListMgr cdlMgr = ChromDeviceListMgr.Create();

	public static ChromDeviceCtrl selfCtrl;

	public bool bLoading = true;

	private Color color_0;

	public SerialPortBase serialPoartBase = new SerialPortBase();

	public SerialPortBase serialPoartOut = new SerialPortBase();

	private int iSelect = 0;

	public FrmChromatManager FrmEquip;

	private int CountAnalyse = 0;

	private bool m_bLoading = true;

	public int cntAnalysis = 0;

	private FormMainParam frmParam = FormMainParam.Create();

	public string[] arrayData = new string[15];

	public float[] fArrayData = new float[15];

	public bool bAutoCycle1 = true;

	public bool bAutoCycle2 = true;

	public bool flagChannelOver1 = false;

	public bool flagChannelOver2 = false;

	private IContainer components = null;

	private GroupBox groupBox2;

	private Panel panel4;

	private Button NetConfig;

	private Button button12;

	private RadioButton rdcs;

	private Button button32;

	private Button button23;

	private CheckBox checkBox4;

	private Label label24;

	public TextBox textBox3;

	private Button button5;

	private Button button1;

	private Label label25;

	private Button button2;

	public TextBox textBox2;

	private Label label26;

	public TextBox textBox1;

	public ListView InstrumlistView;

	private ContextMenuStrip equipEdit;

	private ToolStripMenuItem 编辑ToolStripMenuItem;

	public ImageList imageList1;

	private Button btnFireOnCheck;

	public TextBox tbFireOn2;

	public TextBox tbFireOn;

	private Button btnFireOnSet;

	public TextBox tbHighV1;

	public TextBox tbHighV2;

	private Label label2;

	public CheckBox chbCombinDector;

	public CheckBox chbSeq;

	private SimpleButton btnSetSeq;

	private Label label1;

	private Label label3;

	public TextBox tbSeq4;

	public TextBox tbSeq3;

	private Label label5;

	private Label label4;

	private Button btnSeqNum;

	public TextBox tbSeq2;

	public TextBox tbSeq1;

	private Label label6;

	private Label label7;

	public TextBox tbSeqCur4;

	public TextBox tbSeqCur3;

	private Label label8;

	private Label label9;

	public TextBox tbSeqCur2;

	public TextBox tbSeqCur1;

	private GroupBox groupBox1;

	private TextBox txtMinus;

	private Label label62;

	private CheckBox chbMinus;

	public static bool IsDesignMode()
	{
		return false;
	}

	public ChromDeviceCtrl()
	{
		InitializeComponent();
		if (!IsDesignMode())
		{
			RefrushChromList();
			FrmEquip = new FrmChromatManager();
			chbCombinDector.Checked = frmParam.bTwoDector;
			chbMinus.Checked = frmParam.bMinus;
			try
			{
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
			LoadLanguage();
			m_bLoading = false;
			selfCtrl = this;
		}
	}

	private void LoadLanguage()
	{
		groupBox2.Text = Lang.PS("设备管理", "AquipManager");
		button32.Text = Lang.PS("软键盘输入", "Soft keyboard");
		rdcs.Text = Lang.PS("DCS空闲", "DCS Free");
		chbCombinDector.Text = Lang.PS("合并计算", "Normalizing");
		btnFireOnCheck.Text = Lang.PS("点火门限查询", "Igquery");
		btnFireOnSet.Text = Lang.PS("点火门限设定", "Igset");
	}

	private void button23_Click(object sender, EventArgs e)
	{
		cdlMgr.currentTcpServerMgrSendCmd(7);
	}

	private void NetConfig_Click(object sender, EventArgs e)
	{
		NetSetForm netSetForm = new NetSetForm();
		netSetForm.Show();
	}

	private void button12_Click(object sender, EventArgs e)
	{
		cdlMgr.currentTcpServerMgrSendCmd(41);
	}

	private void button32_Click(object sender, EventArgs e)
	{
		string fileName = "C:\\Program Files\\Common Files\\microsoft shared\\ink\\TabTip.exe";
		Process.Start(fileName);
	}

	private void checkBox4_CheckedChanged(object sender, EventArgs e)
	{
		TcpServerSocket currentTcpServerSocket = cdlMgr.CurrentTcpServerSocket;
		if (currentTcpServerSocket != null)
		{
			int currentChannelIdx = cdlMgr.CurrentChannelIdx;
			if (checkBox4.Checked)
			{
				currentTcpServerSocket.sglsSampling[currentChannelIdx].StopAutoPutright = true;
			}
			else
			{
				currentTcpServerSocket.sglsSampling[currentChannelIdx].StopAutoPutright = false;
			}
		}
	}

	private void button5_Click(object sender, EventArgs e)
	{
		cdlMgr.currentTcpServerMgrSendCmd(35);
	}

	public void button1_Click(object sender, EventArgs e)
	{
		int num = -1;
		if (InstrumlistView.SelectedItems.Count > 0)
		{
			num = InstrumlistView.SelectedItems[0].Index;
		}
		cdlMgr.SaveWorkSunFile();
		if (num >= 0)
		{
			InstrumlistView.Items[num].Selected = true;
			InstrumlistView_DoubleClick(null, null);
		}
	}

	private void button2_Click(object sender, EventArgs e)
	{
		FrmEquip.Show();
	}

	public void InstrumlistView_DoubleClick(object sender, EventArgs e)
	{
		if (cdlMgr.Count != 0)
		{
			ListViewItem listViewItem = null;
			if (InstrumlistView.SelectedItems.Count > 0)
			{
				listViewItem = InstrumlistView.SelectedItems[0];
			}
			if (listViewItem == null && InstrumlistView.Items.Count > 0)
			{
				listViewItem = InstrumlistView.Items[0];
			}
			if (listViewItem != null)
			{
				string currentGCID = listViewItem.Tag.ToString();
				cdlMgr.CurrentGCID = currentGCID;
				cdlMgr.formMain.SetCurrentChromDevice();
			}
		}
	}

	public void method_23()
	{
		rdcs.Checked = true;
		rdcs.Text = Lang.PS("DCS已连接", "DCS Connected");
	}

	public void method_24()
	{
		rdcs.Checked = false;
		rdcs.Text = Lang.PS("DCS空闲", "DCS Free");
	}

	public void RefrushChromList()
	{
		InstrumlistView.Items.Clear();
		for (int i = 0; i < cdlMgr.Count && i <= 300; i++)
		{
			ListViewItem listViewItem = new ListViewItem(cdlMgr[i].info.Name);
			listViewItem.Tag = cdlMgr[i].info.ID.Trim();
			listViewItem.ImageIndex = 19;
			InstrumlistView.Items.Add(listViewItem);
			InstrumlistView.Refresh();
		}
	}

	public void SetChromDeviceImageIndex(TcpServerEventArgs e)
	{
		if (e.ServerSocket.ID.Trim() == "")
		{
			return;
		}
		for (int i = 0; i < InstrumlistView.Items.Count; i++)
		{
			ListViewItem listViewItem = InstrumlistView.Items[i];
			if (listViewItem.Tag.ToString() == e.ServerSocket.ID)
			{
				listViewItem.ImageIndex = 19;
				break;
			}
		}
	}

	public void AddNewChromDevice(string strGCID)
	{
		if (!(strGCID.Trim() == ""))
		{
			cdlMgr.Add(strGCID);
			RefrushChromList();
		}
	}

	public void SetUvInfo(DtC_Channel channel)
	{
		textBox1.Text = DetectorSettingRow.GetDeviceTypeNameByIdx(channel.mark, frmParam.iDetector);
		textBox2.Text = channel.chromInfoR.UvRange.ToString();
		textBox3.Text = channel.chromInfoR.UvwsStepFreq.ToString();
	}

	public void ClearUvInfo()
	{
		textBox1.Text = "";
		textBox2.Text = "";
		textBox3.Text = "";
	}

	public void UpdateChromDevice(TcpServerEventArgs e)
	{
		if (e.ServerSocket.ID.Trim() == "")
		{
			return;
		}
		bool flag = false;
		for (int i = 0; i < InstrumlistView.Items.Count; i++)
		{
			ListViewItem listViewItem = InstrumlistView.Items[i];
			if (listViewItem.Tag.ToString().CompareTo(e.ServerSocket.ID) == 0 || listViewItem.Text.Trim().CompareTo(e.ServerSocket.ID) == 0)
			{
				if (e.ServerSocket.ID == cdlMgr.CurrentGCID)
				{
					listViewItem.ImageIndex = 22;
				}
				else if (e.ServerSocket.ID != "709131284A484845")
				{
					listViewItem.ImageIndex = 20;
				}
				flag = true;
			}
		}
		if (!flag && !(e.ServerSocket.ID.Trim() == ""))
		{
			if (cdlMgr.GetChromDevice(e.ServerSocket.ID) == null)
			{
				cdlMgr.Add(e.ServerSocket.ID);
			}
			ListViewItem listViewItem2 = new ListViewItem(e.ServerSocket.ID);
			listViewItem2.ImageIndex = 19;
			listViewItem2.Tag = e.ServerSocket.ID;
			InstrumlistView.Items.Add(listViewItem2);
			InstrumlistView.Refresh();
		}
	}

	public void SetSelectItemImageIdx()
	{
		if (InstrumlistView.SelectedItems.Count != 0)
		{
			InstrumlistView.SelectedItems[0].ImageIndex = 22;
		}
	}

	private void ToolStripMenuItemEdit_Click(object sender, EventArgs e)
	{
		if (InstrumlistView.SelectedItems.Count > 0)
		{
			FrmEquip.SelectNodeByID((string)InstrumlistView.SelectedItems[InstrumlistView.SelectedItems.Count - 1].Tag);
			FrmEquip.Show();
		}
		else
		{
			MessageBox.Show(Lang.PS("请选中后编辑。", "please Select Edit!"));
		}
	}

	private void ChromDeviceCtrl_SizeChanged(object sender, EventArgs e)
	{
		if (!m_bLoading)
		{
			if (base.Size.Width < 200)
			{
				NetConfig.Visible = false;
				rdcs.Visible = false;
			}
			else
			{
				NetConfig.Visible = true;
				rdcs.Visible = true;
			}
		}
	}

	private void BtnFireOnCheck_Click(object sender, EventArgs e)
	{
		Class49.InsertIntoRNVocTable(0, 0, arrayData);
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

	public void data2Array(string name, float amount)
	{
		switch (name)
		{
		case "总烃":
			arrayData[0] = amount.ToString("0.00000");
			fArrayData[0] = amount;
			break;
		case "甲烷":
			arrayData[1] = amount.ToString("0.00000");
			fArrayData[1] = amount;
			break;
		case "非甲烷总烃":
			arrayData[2] = amount.ToString("0.00000");
			fArrayData[2] = amount;
			break;
		case "苯":
			arrayData[3] = amount.ToString("0.00000");
			fArrayData[3] = amount;
			break;
		case "甲苯":
			arrayData[4] = amount.ToString("0.00000");
			fArrayData[4] = amount;
			break;
		case "间对二甲苯":
			arrayData[5] = amount.ToString("0.00000");
			fArrayData[5] = amount;
			break;
		case "邻二甲苯":
			arrayData[6] = amount.ToString("0.00000");
			fArrayData[6] = amount;
			break;
		case "苯乙烯":
			arrayData[7] = amount.ToString("0.00000");
			fArrayData[7] = amount;
			break;
		}
	}

	public void disposePeaks(int selectedIndex, string fileName, string strID, string strSampleIndex, Chromatogram chromatogram)
	{
		if (frmParam.bTestModbus)
		{
			cntAnalysis++;
			float[] array = new float[1];
			ushort[] array2 = new ushort[2];
			array = new float[1] { cntAnalysis };
			Buffer.BlockCopy(array, 0, array2, 0, 4);
			for (int i = 0; i < 10; i++)
			{
				cdlMgr.tcpServerMgr.mComModbus.WordVaue[105 + i * 10] = array2[0];
				cdlMgr.tcpServerMgr.mComModbus.WordVaue[104 + i * 10] = array2[1];
				cdlMgr.tcpServerMgr.mComModbus.WordVaue[2005 + i * 10] = array2[0];
				cdlMgr.tcpServerMgr.mComModbus.WordVaue[2004 + i * 10] = array2[1];
			}
			array[0] = 7.853f;
			Buffer.BlockCopy(array, 0, array2, 0, 4);
			cdlMgr.tcpServerMgr.mComModbus.WordVaue[0] = array2[0];
			cdlMgr.tcpServerMgr.mComModbus.WordVaue[1] = array2[1];
			return;
		}
		AreaPlotParamMgr areaPlotParamMgr = AreaPlotParamMgr.Create();
		AreaPlotParam areaPlotParam = null;
		float fTHC = 0f;
		float fCH4 = 0f;
		float fNMHC = 0f;
		if (cdlMgr.tcpServerMgr.mComModbus.WordVaue.Length <= 50)
		{
			LogMgr.Instance.Write2RunLog("VocCtrl.disposeVOCPeaks  Error:mComModbus.WordVaue.Length<50");
			return;
		}
		int index = 0;
		byte indexGnl = 0;
		byte b = 0;
		float num = 0f;
		if (cdlMgr.CurrentChartParaOpera.mtdMgr.caliGnl == null)
		{
			cdlMgr.CurrentChartParaOpera.mtdMgr.caliGnl = new CaliGnl();
		}
		CaliGnl caliGnl = cdlMgr.CurrentChartParaOpera.mtdMgr.caliGnl;
		Peak[] peak = chromatogram.RltPeaks;
		float[] floatData = new float[1];
		ushort[] uintData1 = new ushort[2];
		int num2 = 14;
		int[] array3 = new int[1];
		string s = DateTime.Now.ToString("yyyyMMdd");
		string s2 = DateTime.Now.ToString("HHmmss");
		int.TryParse(s, out array3[0]);
		Buffer.BlockCopy(array3, 0, uintData1, 0, 4);
		cdlMgr.tcpServerMgr.mComModbus.WordVaue[96] = uintData1[1];
		cdlMgr.tcpServerMgr.mComModbus.WordVaue[97] = uintData1[0];
		int.TryParse(s2, out array3[0]);
		Buffer.BlockCopy(array3, 0, uintData1, 0, 4);
		cdlMgr.tcpServerMgr.mComModbus.WordVaue[98] = uintData1[1];
		cdlMgr.tcpServerMgr.mComModbus.WordVaue[99] = uintData1[0];
		switch (selectedIndex)
		{
		case 0:
		{
			flagChannelOver1 = true;
			CaliGnl caliGnl3 = cdlMgr.ChartParaOperaList[0].mtdMgr.caliGnl;
			CountAnalyse++;
			int num4 = 255;
			int num5 = 255;
			float num6 = 0f;
			float num7 = 0f;
			floatData = new float[1];
			float[] array5 = new float[50];
			Buffer.BlockCopy(floatData, 0, uintData1, 0, 4);
			cdlMgr.tcpServerMgr.mComModbus.WordVaue[14] = uintData1[0];
			cdlMgr.tcpServerMgr.mComModbus.WordVaue[15] = uintData1[1];
			cdlMgr.tcpServerMgr.mComModbus.WordVaue[16] = uintData1[0];
			cdlMgr.tcpServerMgr.mComModbus.WordVaue[17] = uintData1[1];
			cdlMgr.tcpServerMgr.mComModbus.WordVaue[18] = uintData1[0];
			cdlMgr.tcpServerMgr.mComModbus.WordVaue[19] = uintData1[1];
			for (int num8 = 0; num8 < 4; num8++)
			{
				cdlMgr.tcpServerMgr.mComModbus.WordVaue[105 + num8 * 10] = uintData1[0];
				cdlMgr.tcpServerMgr.mComModbus.WordVaue[104 + num8 * 10] = uintData1[1];
			}
			MethodInvoker method3 = delegate
			{
				if (tabRltCtrl.selfCtrl != null)
				{
					tabRltCtrl.selfCtrl.tbTHC.Text = "0.00";
					tabRltCtrl.selfCtrl.tbCH4.Text = "0.00";
				}
			};
			Invoke(method3);
			for (indexGnl = 0; indexGnl < caliGnl3.cmpds.Count(); indexGnl++)
			{
				index = 0;
				while (1 <= peak.Count() && index < peak.Count())
				{
					if (peak[index].pkRT >= caliGnl3.cmpds[indexGnl].cmpdInfo.retainTime - caliGnl3.cmpds[indexGnl].cmpdInfo.leftWindow && peak[index].pkRT <= caliGnl3.cmpds[indexGnl].cmpdInfo.retainTime + caliGnl3.cmpds[indexGnl].cmpdInfo.rightWindow && !(caliGnl3.cmpds[indexGnl].cmpdInfo.name != peak[index].name) && peak[index].height >= num)
					{
						if (indexGnl < 50)
						{
							array5[indexGnl] = peak[index].amount;
						}
						data2Array(peak[index].name, peak[index].amount);
						floatData = new float[1];
						floatData[0] = peak[index].area;
						Buffer.BlockCopy(floatData, 0, uintData1, 0, 4);
						floatData = new float[1] { 2.4f };
						floatData[0] = peak[index].area;
						Buffer.BlockCopy(floatData, 0, uintData1, 0, 4);
						if (tabRltCtrl.selfCtrl != null)
						{
							MethodInvoker method4 = delegate
							{
								if (indexGnl == 0)
								{
									fTHC = peak[index].amount;
									tabRltCtrl.selfCtrl.tbTHC.Text = peak[index].amount.ToString("0.00");
								}
								else if (indexGnl == 1)
								{
									fCH4 = peak[index].amount;
									tabRltCtrl.selfCtrl.tbCH4.Text = peak[index].amount.ToString("0.00");
								}
							};
							Invoke(method4);
						}
						cdlMgr.tcpServerMgr.mComModbus.WordVaue[101 + indexGnl * 10] = uintData1[0];
						cdlMgr.tcpServerMgr.mComModbus.WordVaue[100 + indexGnl * 10] = uintData1[1];
						floatData[0] = peak[index].areaPer;
						Buffer.BlockCopy(floatData, 0, uintData1, 0, 4);
						cdlMgr.tcpServerMgr.mComModbus.WordVaue[103 + indexGnl * 10] = uintData1[0];
						cdlMgr.tcpServerMgr.mComModbus.WordVaue[102 + indexGnl * 10] = uintData1[1];
						floatData[0] = peak[index].amount;
						Buffer.BlockCopy(floatData, 0, uintData1, 0, 4);
						cdlMgr.tcpServerMgr.mComModbus.WordVaue[105 + indexGnl * 10] = uintData1[0];
						cdlMgr.tcpServerMgr.mComModbus.WordVaue[104 + indexGnl * 10] = uintData1[1];
						floatData[0] = peak[index].amountPer;
						Buffer.BlockCopy(floatData, 0, uintData1, 0, 4);
						cdlMgr.tcpServerMgr.mComModbus.WordVaue[107 + indexGnl * 10] = uintData1[0];
						cdlMgr.tcpServerMgr.mComModbus.WordVaue[106 + indexGnl * 10] = uintData1[1];
						ushort num9 = indexGnl switch
						{
							0 => (ushort)((peak[index].amount - frmParam.fmount41) / (frmParam.fmount201 - frmParam.fmount41) * 4095f), 
							1 => (ushort)((peak[index].amount - frmParam.fmount42) / (frmParam.fmount202 - frmParam.fmount42) * 4095f), 
							2 => (ushort)((peak[index].amount - frmParam.fmount43) / (frmParam.fmount203 - frmParam.fmount43) * 4095f), 
							3 => (ushort)((peak[index].amount - frmParam.fmount44) / (frmParam.fmount204 - frmParam.fmount44) * 4095f), 
							4 => (ushort)((peak[index].amount - frmParam.fmount45) / (frmParam.fmount205 - frmParam.fmount45) * 4095f), 
							5 => (ushort)((peak[index].amount - frmParam.fmount46) / (frmParam.fmount206 - frmParam.fmount46) * 4095f), 
							6 => (ushort)((peak[index].amount - frmParam.fmount47) / (frmParam.fmount207 - frmParam.fmount47) * 4095f), 
							7 => (ushort)((peak[index].amount - frmParam.fmount48) / (frmParam.fmount208 - frmParam.fmount48) * 4095f), 
							8 => (ushort)((peak[index].amount - frmParam.fmount49) / (frmParam.fmount209 - frmParam.fmount49) * 4095f), 
							9 => (ushort)((peak[index].amount - frmParam.fmount410) / (frmParam.fmount2010 - frmParam.fmount410) * 4095f), 
							10 => (ushort)((peak[index].amount - frmParam.fmount411) / (frmParam.fmount2011 - frmParam.fmount411) * 4095f), 
							11 => (ushort)((peak[index].amount - frmParam.fmount412) / (frmParam.fmount2012 - frmParam.fmount412) * 4095f), 
							_ => 0, 
						};
						if (num9 > 4095)
						{
							num9 = 4095;
						}
						if (num9 < 0)
						{
							num9 = 0;
						}
						try
						{
							serialPoartBase.Data2[7 + indexGnl * 2] = (byte)(num9 >> 8);
							serialPoartBase.Data2[8 + indexGnl * 2] = (byte)num9;
						}
						catch (Exception)
						{
						}
						break;
					}
					index++;
				}
				if (index >= peak.Count())
				{
					floatData = new float[1];
					floatData[0] = 0f;
					Buffer.BlockCopy(floatData, 0, uintData1, 0, 4);
					cdlMgr.tcpServerMgr.mComModbus.WordVaue[101 + indexGnl * 10] = uintData1[0];
					cdlMgr.tcpServerMgr.mComModbus.WordVaue[100 + indexGnl * 10] = uintData1[1];
					cdlMgr.tcpServerMgr.mComModbus.WordVaue[103 + indexGnl * 10] = uintData1[0];
					cdlMgr.tcpServerMgr.mComModbus.WordVaue[102 + indexGnl * 10] = uintData1[1];
					cdlMgr.tcpServerMgr.mComModbus.WordVaue[105 + indexGnl * 10] = uintData1[0];
					cdlMgr.tcpServerMgr.mComModbus.WordVaue[104 + indexGnl * 10] = uintData1[1];
					cdlMgr.tcpServerMgr.mComModbus.WordVaue[107 + indexGnl * 10] = uintData1[0];
					cdlMgr.tcpServerMgr.mComModbus.WordVaue[106 + indexGnl * 10] = uintData1[1];
				}
			}
			MethodInvoker method5 = delegate
			{
				if (fTHC > fCH4)
				{
					fNMHC = fTHC - fCH4;
					if (tabRltCtrl.selfCtrl != null)
					{
						tabRltCtrl.selfCtrl.tbNMHC.Text = fNMHC.ToString("0.00");
					}
				}
				else
				{
					fNMHC = 0f;
					if (tabRltCtrl.selfCtrl != null)
					{
						tabRltCtrl.selfCtrl.tbNMHC.Text = 0.ToString("0.00");
					}
				}
				floatData[0] = fNMHC;
				Buffer.BlockCopy(floatData, 0, uintData1, 0, 4);
				cdlMgr.tcpServerMgr.mComModbus.WordVaue[125 + indexGnl * 10] = uintData1[0];
				cdlMgr.tcpServerMgr.mComModbus.WordVaue[124 + indexGnl * 10] = uintData1[1];
			};
			Invoke(method5);
			data2Array("非甲烷总烃", fNMHC);
			break;
		}
		case 1:
		{
			flagChannelOver2 = true;
			CaliGnl caliGnl2 = cdlMgr.ChartParaOperaList[1].mtdMgr.caliGnl;
			int num3 = Math.Min(caliGnl2.cmpds.Count(), 10);
			float[] array4 = new float[num3];
			areaPlotParam = areaPlotParamMgr.GetAreaPlotParam(11);
			CountAnalyse++;
			if (caliGnl2.cmpds.Count() >= 10)
			{
				LogMgr.Instance.Write2RunLog("VocCtrl.disposeVOCPeaks  Warring:苯系物组份表数量大于9,是:" + caliGnl2.cmpds.Count() + "个");
			}
			floatData = new float[1];
			Buffer.BlockCopy(floatData, 0, uintData1, 0, 4);
			if (tabRltCtrl.selfCtrl != null)
			{
				MethodInvoker method = delegate
				{
					tabRltCtrl.selfCtrl.tbBen1.Text = 0.ToString("0.00");
					tabRltCtrl.selfCtrl.tbBen2.Text = 0.ToString("0.00");
					tabRltCtrl.selfCtrl.tbBen3.Text = 0.ToString("0.00");
					tabRltCtrl.selfCtrl.tbBen4.Text = 0.ToString("0.00");
					tabRltCtrl.selfCtrl.tbBen5.Text = 0.ToString("0.00");
				};
				Invoke(method);
			}
			for (indexGnl = 0; indexGnl < num3; indexGnl++)
			{
				index = 0;
				while (1 <= peak.Count() && index < peak.Count())
				{
					if (peak[index].pkRT >= caliGnl2.cmpds[indexGnl].cmpdInfo.retainTime - caliGnl2.cmpds[indexGnl].cmpdInfo.leftWindow && peak[index].pkRT <= caliGnl2.cmpds[indexGnl].cmpdInfo.retainTime + caliGnl2.cmpds[indexGnl].cmpdInfo.rightWindow && peak[index].height >= num)
					{
						data2Array(peak[index].name, peak[index].amount);
						array4[indexGnl] = peak[index].amount;
						floatData = new float[1] { array4[indexGnl] };
						Buffer.BlockCopy(floatData, 0, uintData1, 0, 4);
						floatData = new float[1];
						floatData[0] = peak[index].area;
						Buffer.BlockCopy(floatData, 0, uintData1, 0, 4);
						floatData[0] = peak[index].areaPer;
						Buffer.BlockCopy(floatData, 0, uintData1, 0, 4);
						cdlMgr.tcpServerMgr.mComModbus.WordVaue[153 + indexGnl * 10] = uintData1[0];
						cdlMgr.tcpServerMgr.mComModbus.WordVaue[152 + indexGnl * 10] = uintData1[1];
						floatData[0] = peak[index].amount;
						Buffer.BlockCopy(floatData, 0, uintData1, 0, 4);
						cdlMgr.tcpServerMgr.mComModbus.WordVaue[155 + indexGnl * 10] = uintData1[0];
						cdlMgr.tcpServerMgr.mComModbus.WordVaue[154 + indexGnl * 10] = uintData1[1];
						floatData[0] = peak[index].amountPer;
						Buffer.BlockCopy(floatData, 0, uintData1, 0, 4);
						cdlMgr.tcpServerMgr.mComModbus.WordVaue[157 + indexGnl * 10] = uintData1[0];
						cdlMgr.tcpServerMgr.mComModbus.WordVaue[156 + indexGnl * 10] = uintData1[1];
						cdlMgr.tcpServerMgr.mComModbus.WordVaue[2001 + indexGnl * 10] = uintData1[0];
						cdlMgr.tcpServerMgr.mComModbus.WordVaue[2000 + indexGnl * 10] = uintData1[1];
						floatData[0] = peak[index].areaPer;
						Buffer.BlockCopy(floatData, 0, uintData1, 0, 4);
						cdlMgr.tcpServerMgr.mComModbus.WordVaue[2003 + indexGnl * 10] = uintData1[0];
						cdlMgr.tcpServerMgr.mComModbus.WordVaue[2002 + indexGnl * 10] = uintData1[1];
						floatData[0] = peak[index].amount;
						Buffer.BlockCopy(floatData, 0, uintData1, 0, 4);
						cdlMgr.tcpServerMgr.mComModbus.WordVaue[2005 + indexGnl * 10] = uintData1[0];
						cdlMgr.tcpServerMgr.mComModbus.WordVaue[2004 + indexGnl * 10] = uintData1[1];
						floatData[0] = peak[index].amountPer;
						Buffer.BlockCopy(floatData, 0, uintData1, 0, 4);
						cdlMgr.tcpServerMgr.mComModbus.WordVaue[2007 + indexGnl * 10] = uintData1[0];
						cdlMgr.tcpServerMgr.mComModbus.WordVaue[2006 + indexGnl * 10] = uintData1[1];
						if (tabRltCtrl.selfCtrl == null)
						{
							break;
						}
						MethodInvoker method2 = delegate
						{
							switch (indexGnl)
							{
							case 0:
								tabRltCtrl.selfCtrl.tbBen1.Text = peak[index].amount.ToString("0.00");
								break;
							case 1:
								tabRltCtrl.selfCtrl.tbBen2.Text = peak[index].amount.ToString("0.00");
								break;
							case 2:
								tabRltCtrl.selfCtrl.tbBen3.Text = peak[index].amount.ToString("0.00");
								break;
							case 3:
								tabRltCtrl.selfCtrl.tbBen4.Text = peak[index].amount.ToString("0.00");
								break;
							case 4:
								tabRltCtrl.selfCtrl.tbBen5.Text = peak[index].amount.ToString("0.00");
								break;
							}
						};
						Invoke(method2);
						break;
					}
					index++;
				}
				if (index >= peak.Count())
				{
					floatData = new float[1];
					floatData[0] = 0f;
					Buffer.BlockCopy(floatData, 0, uintData1, 0, 4);
					cdlMgr.tcpServerMgr.mComModbus.WordVaue[2000 + indexGnl * 10] = uintData1[0];
					cdlMgr.tcpServerMgr.mComModbus.WordVaue[2001 + indexGnl * 10] = uintData1[1];
					Buffer.BlockCopy(floatData, 0, uintData1, 0, 4);
					cdlMgr.tcpServerMgr.mComModbus.WordVaue[2002 + indexGnl * 10] = uintData1[0];
					cdlMgr.tcpServerMgr.mComModbus.WordVaue[2003 + indexGnl * 10] = uintData1[1];
					Buffer.BlockCopy(floatData, 0, uintData1, 0, 4);
					cdlMgr.tcpServerMgr.mComModbus.WordVaue[2004 + indexGnl * 10] = uintData1[0];
					cdlMgr.tcpServerMgr.mComModbus.WordVaue[2005 + indexGnl * 10] = uintData1[1];
					Buffer.BlockCopy(floatData, 0, uintData1, 0, 4);
					cdlMgr.tcpServerMgr.mComModbus.WordVaue[2006 + indexGnl * 10] = uintData1[0];
					cdlMgr.tcpServerMgr.mComModbus.WordVaue[2007 + indexGnl * 10] = uintData1[1];
				}
			}
			break;
		}
		}
		int num10 = 0;
		int num11 = 0;
		for (; num10 < 4000; num10++)
		{
			cdlMgr.tcpServerMgr.modBusData_0.ModBusBytes[num11++] = (byte)(cdlMgr.tcpServerMgr.mComModbus.WordVaue[num10] / 256);
			cdlMgr.tcpServerMgr.modBusData_0.ModBusBytes[num11++] = (byte)(cdlMgr.tcpServerMgr.mComModbus.WordVaue[num10] % 256);
		}
		if (!bAutoCycle1 || selectedIndex == 0)
		{
		}
		if (!bAutoCycle2 || selectedIndex == 1)
		{
		}
		if (flagChannelOver1)
		{
			flagChannelOver1 = false;
			Class49.InsertIntoRNNMHC(0, 0, arrayData);
			for (int num12 = 0; num12 < 15; num12++)
			{
				arrayData[num12] = "0";
			}
			MethodInvoker method6 = delegate
			{
				if (HistoryCtrl.selfCtrl != null)
				{
					HistoryCtrl.selfCtrl.updataGridView();
				}
			};
			Invoke(method6);
		}
		if (flagChannelOver2)
		{
			flagChannelOver2 = false;
			Class49.InsertIntoRNBTEX(0, 0, arrayData);
			for (int num13 = 0; num13 < 15; num13++)
			{
				arrayData[num13] = "0";
			}
			MethodInvoker method7 = delegate
			{
				if (HistoryCtrl.selfCtrl != null)
				{
					HistoryCtrl.selfCtrl.updataGridView();
				}
			};
			Invoke(method7);
		}
		ushort[] array6 = new ushort[2];
		long[] src = new long[1] { CountAnalyse };
		Buffer.BlockCopy(src, 0, array6, 0, 4);
		cdlMgr.tcpServerMgr.mComModbus.WordVaue[8] = array6[0];
		cdlMgr.tcpServerMgr.mComModbus.WordVaue[9] = array6[1];
		cdlMgr.CurrentChartParaOpera.mtdMgr.caliGnl = caliGnl;
	}

	private void ChbCombinDector_CheckedChanged(object sender, EventArgs e)
	{
		frmParam.bTwoDector = chbCombinDector.Checked;
		frmParam.SaveParam();
	}

	private void btnSetSeq_Click(object sender, EventArgs e)
	{
		FormSeq formSeq = new FormSeq();
		formSeq.StartPosition = FormStartPosition.CenterScreen;
		formSeq.Show();
	}

	private void chbSeq_CheckedChanged(object sender, EventArgs e)
	{
		if (!m_bLoading && cdlMgr.CurrentTcpServerSocket != null)
		{
			cdlMgr.CurrentTcpServerSocket.cH4Param.bSeq = chbSeq.Checked;
			cdlMgr.CurrentTcpServerSocket.cH4Param.SaveParam();
		}
	}

	private void btnSeqNum_Click(object sender, EventArgs e)
	{
		if (cdlMgr.CurrentTcpServerSocket != null)
		{
			int.TryParse(tbSeq1.Text, out var result);
			int.TryParse(tbSeq2.Text, out var result2);
			int.TryParse(tbSeq3.Text, out var result3);
			int.TryParse(tbSeq4.Text, out var result4);
			cdlMgr.CurrentTcpServerSocket.cntSeq1 = result;
			cdlMgr.CurrentTcpServerSocket.cntSeq2 = result2;
			cdlMgr.CurrentTcpServerSocket.cntSeq3 = result3;
			cdlMgr.CurrentTcpServerSocket.cntSeq4 = result4;
		}
	}

	private void chbMinus_CheckedChanged(object sender, EventArgs e)
	{
		if (!m_bLoading)
		{
			frmParam.bMinus = chbMinus.Checked;
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
		this.components = new System.ComponentModel.Container();
		System.Windows.Forms.ListViewItem listViewItem = new System.Windows.Forms.ListViewItem("色谱1", 19);
		System.Windows.Forms.ListViewItem listViewItem2 = new System.Windows.Forms.ListViewItem("色谱2", 19);
		System.Windows.Forms.ListViewItem listViewItem3 = new System.Windows.Forms.ListViewItem("色谱3", 6);
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(IBrainChrom2018.ChromDeviceCtrl));
		this.groupBox2 = new System.Windows.Forms.GroupBox();
		this.label6 = new System.Windows.Forms.Label();
		this.label7 = new System.Windows.Forms.Label();
		this.tbSeqCur4 = new System.Windows.Forms.TextBox();
		this.tbSeqCur3 = new System.Windows.Forms.TextBox();
		this.label8 = new System.Windows.Forms.Label();
		this.label9 = new System.Windows.Forms.Label();
		this.tbSeqCur2 = new System.Windows.Forms.TextBox();
		this.tbSeqCur1 = new System.Windows.Forms.TextBox();
		this.label1 = new System.Windows.Forms.Label();
		this.label3 = new System.Windows.Forms.Label();
		this.tbSeq4 = new System.Windows.Forms.TextBox();
		this.tbSeq3 = new System.Windows.Forms.TextBox();
		this.label5 = new System.Windows.Forms.Label();
		this.label4 = new System.Windows.Forms.Label();
		this.btnSeqNum = new System.Windows.Forms.Button();
		this.tbSeq2 = new System.Windows.Forms.TextBox();
		this.tbSeq1 = new System.Windows.Forms.TextBox();
		this.btnSetSeq = new DevExpress.XtraEditors.SimpleButton();
		this.InstrumlistView = new System.Windows.Forms.ListView();
		this.equipEdit = new System.Windows.Forms.ContextMenuStrip(this.components);
		this.编辑ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.imageList1 = new System.Windows.Forms.ImageList(this.components);
		this.chbSeq = new System.Windows.Forms.CheckBox();
		this.panel4 = new System.Windows.Forms.Panel();
		this.chbCombinDector = new System.Windows.Forms.CheckBox();
		this.tbHighV2 = new System.Windows.Forms.TextBox();
		this.label2 = new System.Windows.Forms.Label();
		this.tbHighV1 = new System.Windows.Forms.TextBox();
		this.btnFireOnCheck = new System.Windows.Forms.Button();
		this.tbFireOn2 = new System.Windows.Forms.TextBox();
		this.tbFireOn = new System.Windows.Forms.TextBox();
		this.btnFireOnSet = new System.Windows.Forms.Button();
		this.button23 = new System.Windows.Forms.Button();
		this.NetConfig = new System.Windows.Forms.Button();
		this.button12 = new System.Windows.Forms.Button();
		this.checkBox4 = new System.Windows.Forms.CheckBox();
		this.label24 = new System.Windows.Forms.Label();
		this.textBox3 = new System.Windows.Forms.TextBox();
		this.button5 = new System.Windows.Forms.Button();
		this.button1 = new System.Windows.Forms.Button();
		this.label25 = new System.Windows.Forms.Label();
		this.button2 = new System.Windows.Forms.Button();
		this.textBox2 = new System.Windows.Forms.TextBox();
		this.label26 = new System.Windows.Forms.Label();
		this.textBox1 = new System.Windows.Forms.TextBox();
		this.button32 = new System.Windows.Forms.Button();
		this.rdcs = new System.Windows.Forms.RadioButton();
		this.groupBox1 = new System.Windows.Forms.GroupBox();
		this.txtMinus = new System.Windows.Forms.TextBox();
		this.label62 = new System.Windows.Forms.Label();
		this.chbMinus = new System.Windows.Forms.CheckBox();
		this.groupBox2.SuspendLayout();
		this.equipEdit.SuspendLayout();
		this.panel4.SuspendLayout();
		this.groupBox1.SuspendLayout();
		base.SuspendLayout();
		this.groupBox2.BackColor = System.Drawing.Color.FromArgb(224, 224, 224);
		this.groupBox2.Controls.Add(this.groupBox1);
		this.groupBox2.Controls.Add(this.label6);
		this.groupBox2.Controls.Add(this.label7);
		this.groupBox2.Controls.Add(this.tbSeqCur4);
		this.groupBox2.Controls.Add(this.tbSeqCur3);
		this.groupBox2.Controls.Add(this.label8);
		this.groupBox2.Controls.Add(this.label9);
		this.groupBox2.Controls.Add(this.tbSeqCur2);
		this.groupBox2.Controls.Add(this.tbSeqCur1);
		this.groupBox2.Controls.Add(this.label1);
		this.groupBox2.Controls.Add(this.label3);
		this.groupBox2.Controls.Add(this.tbSeq4);
		this.groupBox2.Controls.Add(this.tbSeq3);
		this.groupBox2.Controls.Add(this.label5);
		this.groupBox2.Controls.Add(this.label4);
		this.groupBox2.Controls.Add(this.btnSeqNum);
		this.groupBox2.Controls.Add(this.tbSeq2);
		this.groupBox2.Controls.Add(this.tbSeq1);
		this.groupBox2.Controls.Add(this.btnSetSeq);
		this.groupBox2.Controls.Add(this.InstrumlistView);
		this.groupBox2.Controls.Add(this.chbSeq);
		this.groupBox2.Controls.Add(this.panel4);
		this.groupBox2.Controls.Add(this.button32);
		this.groupBox2.Controls.Add(this.rdcs);
		this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
		this.groupBox2.ForeColor = System.Drawing.Color.Blue;
		this.groupBox2.Location = new System.Drawing.Point(0, 0);
		this.groupBox2.Name = "groupBox2";
		this.groupBox2.Size = new System.Drawing.Size(1025, 204);
		this.groupBox2.TabIndex = 1;
		this.groupBox2.TabStop = false;
		this.groupBox2.Text = "设备管理";
		this.label6.AutoSize = true;
		this.label6.Location = new System.Drawing.Point(526, 133);
		this.label6.Name = "label6";
		this.label6.Size = new System.Drawing.Size(89, 12);
		this.label6.TabIndex = 124;
		this.label6.Text = "通道4当前序列:";
		this.label7.AutoSize = true;
		this.label7.Location = new System.Drawing.Point(526, 107);
		this.label7.Name = "label7";
		this.label7.Size = new System.Drawing.Size(89, 12);
		this.label7.TabIndex = 123;
		this.label7.Text = "通道3当前序列:";
		this.tbSeqCur4.Location = new System.Drawing.Point(619, 129);
		this.tbSeqCur4.Name = "tbSeqCur4";
		this.tbSeqCur4.Size = new System.Drawing.Size(67, 21);
		this.tbSeqCur4.TabIndex = 122;
		this.tbSeqCur4.Text = "1";
		this.tbSeqCur3.Location = new System.Drawing.Point(619, 103);
		this.tbSeqCur3.Name = "tbSeqCur3";
		this.tbSeqCur3.Size = new System.Drawing.Size(67, 21);
		this.tbSeqCur3.TabIndex = 121;
		this.tbSeqCur3.Text = "1";
		this.label8.AutoSize = true;
		this.label8.Location = new System.Drawing.Point(526, 81);
		this.label8.Name = "label8";
		this.label8.Size = new System.Drawing.Size(89, 12);
		this.label8.TabIndex = 120;
		this.label8.Text = "通道2当前序列:";
		this.label9.AutoSize = true;
		this.label9.Location = new System.Drawing.Point(526, 55);
		this.label9.Name = "label9";
		this.label9.Size = new System.Drawing.Size(89, 12);
		this.label9.TabIndex = 119;
		this.label9.Text = "通道1当前序列:";
		this.tbSeqCur2.Location = new System.Drawing.Point(619, 77);
		this.tbSeqCur2.Name = "tbSeqCur2";
		this.tbSeqCur2.Size = new System.Drawing.Size(67, 21);
		this.tbSeqCur2.TabIndex = 118;
		this.tbSeqCur2.Text = "1";
		this.tbSeqCur1.Location = new System.Drawing.Point(619, 51);
		this.tbSeqCur1.Name = "tbSeqCur1";
		this.tbSeqCur1.Size = new System.Drawing.Size(67, 21);
		this.tbSeqCur1.TabIndex = 117;
		this.tbSeqCur1.Text = "1";
		this.label1.AutoSize = true;
		this.label1.Location = new System.Drawing.Point(385, 133);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(65, 12);
		this.label1.TabIndex = 116;
		this.label1.Text = "通道4序列:";
		this.label3.AutoSize = true;
		this.label3.Location = new System.Drawing.Point(385, 107);
		this.label3.Name = "label3";
		this.label3.Size = new System.Drawing.Size(65, 12);
		this.label3.TabIndex = 115;
		this.label3.Text = "通道3序列:";
		this.tbSeq4.Location = new System.Drawing.Point(453, 129);
		this.tbSeq4.Name = "tbSeq4";
		this.tbSeq4.Size = new System.Drawing.Size(67, 21);
		this.tbSeq4.TabIndex = 114;
		this.tbSeq4.Text = "1";
		this.tbSeq3.Location = new System.Drawing.Point(453, 103);
		this.tbSeq3.Name = "tbSeq3";
		this.tbSeq3.Size = new System.Drawing.Size(67, 21);
		this.tbSeq3.TabIndex = 113;
		this.tbSeq3.Text = "1";
		this.label5.AutoSize = true;
		this.label5.Location = new System.Drawing.Point(385, 81);
		this.label5.Name = "label5";
		this.label5.Size = new System.Drawing.Size(65, 12);
		this.label5.TabIndex = 112;
		this.label5.Text = "通道2序列:";
		this.label4.AutoSize = true;
		this.label4.Location = new System.Drawing.Point(385, 55);
		this.label4.Name = "label4";
		this.label4.Size = new System.Drawing.Size(65, 12);
		this.label4.TabIndex = 111;
		this.label4.Text = "通道1序列:";
		this.btnSeqNum.Location = new System.Drawing.Point(387, 160);
		this.btnSeqNum.Name = "btnSeqNum";
		this.btnSeqNum.Size = new System.Drawing.Size(133, 23);
		this.btnSeqNum.TabIndex = 110;
		this.btnSeqNum.Text = "设定起始序列号";
		this.btnSeqNum.UseVisualStyleBackColor = true;
		this.btnSeqNum.Click += new System.EventHandler(btnSeqNum_Click);
		this.tbSeq2.Location = new System.Drawing.Point(453, 77);
		this.tbSeq2.Name = "tbSeq2";
		this.tbSeq2.Size = new System.Drawing.Size(67, 21);
		this.tbSeq2.TabIndex = 109;
		this.tbSeq2.Text = "1";
		this.tbSeq1.Location = new System.Drawing.Point(453, 51);
		this.tbSeq1.Name = "tbSeq1";
		this.tbSeq1.Size = new System.Drawing.Size(67, 21);
		this.tbSeq1.TabIndex = 108;
		this.tbSeq1.Text = "1";
		this.btnSetSeq.Location = new System.Drawing.Point(705, 74);
		this.btnSetSeq.Name = "btnSetSeq";
		this.btnSetSeq.Size = new System.Drawing.Size(93, 23);
		this.btnSetSeq.TabIndex = 107;
		this.btnSetSeq.Text = "设定序列进样";
		this.btnSetSeq.Click += new System.EventHandler(btnSetSeq_Click);
		this.InstrumlistView.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.InstrumlistView.ContextMenuStrip = this.equipEdit;
		this.InstrumlistView.Dock = System.Windows.Forms.DockStyle.Left;
		this.InstrumlistView.HideSelection = false;
		this.InstrumlistView.Items.AddRange(new System.Windows.Forms.ListViewItem[3] { listViewItem, listViewItem2, listViewItem3 });
		this.InstrumlistView.LargeImageList = this.imageList1;
		this.InstrumlistView.Location = new System.Drawing.Point(3, 45);
		this.InstrumlistView.MultiSelect = false;
		this.InstrumlistView.Name = "InstrumlistView";
		this.InstrumlistView.Size = new System.Drawing.Size(360, 156);
		this.InstrumlistView.TabIndex = 0;
		this.InstrumlistView.UseCompatibleStateImageBehavior = false;
		this.InstrumlistView.DoubleClick += new System.EventHandler(InstrumlistView_DoubleClick);
		this.equipEdit.ImageScalingSize = new System.Drawing.Size(20, 20);
		this.equipEdit.Items.AddRange(new System.Windows.Forms.ToolStripItem[1] { this.编辑ToolStripMenuItem });
		this.equipEdit.Name = "equipEdit";
		this.equipEdit.Size = new System.Drawing.Size(105, 30);
		this.编辑ToolStripMenuItem.Image = (System.Drawing.Image)resources.GetObject("编辑ToolStripMenuItem.Image");
		this.编辑ToolStripMenuItem.Name = "编辑ToolStripMenuItem";
		this.编辑ToolStripMenuItem.Size = new System.Drawing.Size(104, 26);
		this.编辑ToolStripMenuItem.Text = "编辑";
		this.编辑ToolStripMenuItem.Click += new System.EventHandler(ToolStripMenuItemEdit_Click);
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
		this.chbSeq.AutoSize = true;
		this.chbSeq.ForeColor = System.Drawing.Color.Black;
		this.chbSeq.Location = new System.Drawing.Point(705, 53);
		this.chbSeq.Name = "chbSeq";
		this.chbSeq.Size = new System.Drawing.Size(96, 16);
		this.chbSeq.TabIndex = 106;
		this.chbSeq.Text = "启用序列进样";
		this.chbSeq.UseVisualStyleBackColor = true;
		this.chbSeq.CheckedChanged += new System.EventHandler(chbSeq_CheckedChanged);
		this.panel4.BackColor = System.Drawing.Color.FromArgb(128, 128, 255);
		this.panel4.Controls.Add(this.chbCombinDector);
		this.panel4.Controls.Add(this.tbHighV2);
		this.panel4.Controls.Add(this.label2);
		this.panel4.Controls.Add(this.tbHighV1);
		this.panel4.Controls.Add(this.btnFireOnCheck);
		this.panel4.Controls.Add(this.tbFireOn2);
		this.panel4.Controls.Add(this.tbFireOn);
		this.panel4.Controls.Add(this.btnFireOnSet);
		this.panel4.Controls.Add(this.button23);
		this.panel4.Controls.Add(this.NetConfig);
		this.panel4.Controls.Add(this.button12);
		this.panel4.Controls.Add(this.checkBox4);
		this.panel4.Controls.Add(this.label24);
		this.panel4.Controls.Add(this.textBox3);
		this.panel4.Controls.Add(this.button5);
		this.panel4.Controls.Add(this.button1);
		this.panel4.Controls.Add(this.label25);
		this.panel4.Controls.Add(this.button2);
		this.panel4.Controls.Add(this.textBox2);
		this.panel4.Controls.Add(this.label26);
		this.panel4.Controls.Add(this.textBox1);
		this.panel4.Dock = System.Windows.Forms.DockStyle.Top;
		this.panel4.Location = new System.Drawing.Point(3, 17);
		this.panel4.Name = "panel4";
		this.panel4.Size = new System.Drawing.Size(1019, 28);
		this.panel4.TabIndex = 2;
		this.chbCombinDector.AutoSize = true;
		this.chbCombinDector.ForeColor = System.Drawing.Color.Black;
		this.chbCombinDector.Location = new System.Drawing.Point(621, 7);
		this.chbCombinDector.Name = "chbCombinDector";
		this.chbCombinDector.Size = new System.Drawing.Size(72, 16);
		this.chbCombinDector.TabIndex = 105;
		this.chbCombinDector.Text = "合并运算";
		this.chbCombinDector.UseVisualStyleBackColor = true;
		this.chbCombinDector.CheckedChanged += new System.EventHandler(ChbCombinDector_CheckedChanged);
		this.tbHighV2.Location = new System.Drawing.Point(546, 5);
		this.tbHighV2.Name = "tbHighV2";
		this.tbHighV2.Size = new System.Drawing.Size(68, 21);
		this.tbHighV2.TabIndex = 104;
		this.label2.AutoSize = true;
		this.label2.Font = new System.Drawing.Font("宋体", 9f);
		this.label2.ForeColor = System.Drawing.Color.Black;
		this.label2.Location = new System.Drawing.Point(429, 7);
		this.label2.Name = "label2";
		this.label2.Size = new System.Drawing.Size(41, 12);
		this.label2.TabIndex = 103;
		this.label2.Text = "高压：";
		this.tbHighV1.Location = new System.Drawing.Point(474, 5);
		this.tbHighV1.Name = "tbHighV1";
		this.tbHighV1.Size = new System.Drawing.Size(68, 21);
		this.tbHighV1.TabIndex = 102;
		this.btnFireOnCheck.Location = new System.Drawing.Point(119, 2);
		this.btnFireOnCheck.Name = "btnFireOnCheck";
		this.btnFireOnCheck.Size = new System.Drawing.Size(85, 23);
		this.btnFireOnCheck.TabIndex = 99;
		this.btnFireOnCheck.Text = "点火门限查询";
		this.btnFireOnCheck.UseVisualStyleBackColor = true;
		this.btnFireOnCheck.Click += new System.EventHandler(BtnFireOnCheck_Click);
		this.tbFireOn2.Location = new System.Drawing.Point(360, 5);
		this.tbFireOn2.Name = "tbFireOn2";
		this.tbFireOn2.Size = new System.Drawing.Size(51, 21);
		this.tbFireOn2.TabIndex = 101;
		this.tbFireOn.Location = new System.Drawing.Point(306, 5);
		this.tbFireOn.Name = "tbFireOn";
		this.tbFireOn.Size = new System.Drawing.Size(54, 21);
		this.tbFireOn.TabIndex = 97;
		this.btnFireOnSet.Location = new System.Drawing.Point(210, 2);
		this.btnFireOnSet.Name = "btnFireOnSet";
		this.btnFireOnSet.Size = new System.Drawing.Size(91, 23);
		this.btnFireOnSet.TabIndex = 100;
		this.btnFireOnSet.Text = "点火门限设定";
		this.btnFireOnSet.UseVisualStyleBackColor = true;
		this.btnFireOnSet.Click += new System.EventHandler(BtnFireOnSet_Click);
		this.button23.Location = new System.Drawing.Point(560, 3);
		this.button23.Name = "button23";
		this.button23.Size = new System.Drawing.Size(64, 23);
		this.button23.TabIndex = 2;
		this.button23.Text = "设置";
		this.button23.UseVisualStyleBackColor = true;
		this.button23.Visible = false;
		this.button23.Click += new System.EventHandler(button23_Click);
		this.NetConfig.Location = new System.Drawing.Point(44, 3);
		this.NetConfig.Name = "NetConfig";
		this.NetConfig.Size = new System.Drawing.Size(72, 23);
		this.NetConfig.TabIndex = 7;
		this.NetConfig.Text = "网络配置";
		this.NetConfig.UseVisualStyleBackColor = true;
		this.NetConfig.Click += new System.EventHandler(NetConfig_Click);
		this.button12.Location = new System.Drawing.Point(560, 3);
		this.button12.Name = "button12";
		this.button12.Size = new System.Drawing.Size(81, 23);
		this.button12.TabIndex = 1;
		this.button12.Text = "EPS查询";
		this.button12.UseVisualStyleBackColor = true;
		this.button12.Visible = false;
		this.button12.Click += new System.EventHandler(button12_Click);
		this.checkBox4.AutoSize = true;
		this.checkBox4.Font = new System.Drawing.Font("宋体", 9f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 134);
		this.checkBox4.Location = new System.Drawing.Point(504, 6);
		this.checkBox4.Name = "checkBox4";
		this.checkBox4.Size = new System.Drawing.Size(115, 16);
		this.checkBox4.TabIndex = 5;
		this.checkBox4.Text = "停止后谱图校正";
		this.checkBox4.UseVisualStyleBackColor = true;
		this.checkBox4.Visible = false;
		this.checkBox4.CheckedChanged += new System.EventHandler(checkBox4_CheckedChanged);
		this.label24.AutoSize = true;
		this.label24.Font = new System.Drawing.Font("宋体", 9f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 134);
		this.label24.ForeColor = System.Drawing.Color.Black;
		this.label24.Location = new System.Drawing.Point(619, 7);
		this.label24.Name = "label24";
		this.label24.Size = new System.Drawing.Size(51, 12);
		this.label24.TabIndex = 0;
		this.label24.Text = "检测器:";
		this.label24.Visible = false;
		this.textBox3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.textBox3.Location = new System.Drawing.Point(943, 4);
		this.textBox3.Name = "textBox3";
		this.textBox3.ReadOnly = true;
		this.textBox3.Size = new System.Drawing.Size(47, 21);
		this.textBox3.TabIndex = 4;
		this.textBox3.Visible = false;
		this.button5.Location = new System.Drawing.Point(571, 2);
		this.button5.Name = "button5";
		this.button5.Size = new System.Drawing.Size(70, 23);
		this.button5.TabIndex = 1;
		this.button5.Text = "查询气路";
		this.button5.UseVisualStyleBackColor = true;
		this.button5.Visible = false;
		this.button5.Click += new System.EventHandler(button5_Click);
		this.button1.Image = (System.Drawing.Image)resources.GetObject("button1.Image");
		this.button1.Location = new System.Drawing.Point(54, 0);
		this.button1.Name = "button1";
		this.button1.Size = new System.Drawing.Size(43, 21);
		this.button1.TabIndex = 1;
		this.button1.UseVisualStyleBackColor = true;
		this.button1.Visible = false;
		this.button1.Click += new System.EventHandler(button1_Click);
		this.label25.AutoSize = true;
		this.label25.Font = new System.Drawing.Font("宋体", 9f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 134);
		this.label25.ForeColor = System.Drawing.Color.Black;
		this.label25.Location = new System.Drawing.Point(1015, 10);
		this.label25.Name = "label25";
		this.label25.Size = new System.Drawing.Size(38, 12);
		this.label25.TabIndex = 0;
		this.label25.Text = "量程:";
		this.label25.Visible = false;
		this.button2.Image = (System.Drawing.Image)resources.GetObject("button2.Image");
		this.button2.Location = new System.Drawing.Point(1, 3);
		this.button2.Name = "button2";
		this.button2.Size = new System.Drawing.Size(43, 23);
		this.button2.TabIndex = 1;
		this.button2.UseVisualStyleBackColor = true;
		this.button2.Click += new System.EventHandler(button2_Click);
		this.textBox2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.textBox2.Location = new System.Drawing.Point(1017, 3);
		this.textBox2.Name = "textBox2";
		this.textBox2.ReadOnly = true;
		this.textBox2.Size = new System.Drawing.Size(47, 21);
		this.textBox2.TabIndex = 4;
		this.textBox2.Visible = false;
		this.label26.AutoSize = true;
		this.label26.Font = new System.Drawing.Font("宋体", 9f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 134);
		this.label26.ForeColor = System.Drawing.Color.Black;
		this.label26.Location = new System.Drawing.Point(996, 6);
		this.label26.Name = "label26";
		this.label26.Size = new System.Drawing.Size(38, 12);
		this.label26.TabIndex = 0;
		this.label26.Text = "采样:";
		this.label26.Visible = false;
		this.textBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.textBox1.Location = new System.Drawing.Point(674, 2);
		this.textBox1.Name = "textBox1";
		this.textBox1.ReadOnly = true;
		this.textBox1.Size = new System.Drawing.Size(47, 21);
		this.textBox1.TabIndex = 4;
		this.textBox1.Visible = false;
		this.button32.Location = new System.Drawing.Point(1022, 19);
		this.button32.Name = "button32";
		this.button32.Size = new System.Drawing.Size(62, 23);
		this.button32.TabIndex = 1;
		this.button32.Text = "软键盘";
		this.button32.UseVisualStyleBackColor = true;
		this.button32.Visible = false;
		this.button32.Click += new System.EventHandler(button32_Click);
		this.rdcs.AutoSize = true;
		this.rdcs.Enabled = false;
		this.rdcs.Location = new System.Drawing.Point(947, 4);
		this.rdcs.Name = "rdcs";
		this.rdcs.Size = new System.Drawing.Size(65, 16);
		this.rdcs.TabIndex = 3;
		this.rdcs.TabStop = true;
		this.rdcs.Text = "DCS空闲";
		this.rdcs.UseVisualStyleBackColor = true;
		this.rdcs.Visible = false;
		this.groupBox1.Controls.Add(this.txtMinus);
		this.groupBox1.Controls.Add(this.label62);
		this.groupBox1.Controls.Add(this.chbMinus);
		this.groupBox1.Location = new System.Drawing.Point(705, 107);
		this.groupBox1.Name = "groupBox1";
		this.groupBox1.Size = new System.Drawing.Size(158, 56);
		this.groupBox1.TabIndex = 125;
		this.groupBox1.TabStop = false;
		this.groupBox1.Text = "差减法(H2)";
		this.txtMinus.Location = new System.Drawing.Point(41, 64);
		this.txtMinus.Name = "txtMinus";
		this.txtMinus.Size = new System.Drawing.Size(100, 21);
		this.txtMinus.TabIndex = 72;
		this.txtMinus.Visible = false;
		this.label62.AutoSize = true;
		this.label62.Location = new System.Drawing.Point(6, 68);
		this.label62.Name = "label62";
		this.label62.Size = new System.Drawing.Size(29, 12);
		this.label62.TabIndex = 73;
		this.label62.Text = "名称";
		this.label62.Visible = false;
		this.chbMinus.AutoSize = true;
		this.chbMinus.Location = new System.Drawing.Point(6, 29);
		this.chbMinus.Name = "chbMinus";
		this.chbMinus.Size = new System.Drawing.Size(48, 16);
		this.chbMinus.TabIndex = 71;
		this.chbMinus.Text = "使能";
		this.chbMinus.UseVisualStyleBackColor = true;
		this.chbMinus.CheckedChanged += new System.EventHandler(chbMinus_CheckedChanged);
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.Controls.Add(this.groupBox2);
		base.Name = "ChromDeviceCtrl";
		base.Size = new System.Drawing.Size(1025, 204);
		base.SizeChanged += new System.EventHandler(ChromDeviceCtrl_SizeChanged);
		this.groupBox2.ResumeLayout(false);
		this.groupBox2.PerformLayout();
		this.equipEdit.ResumeLayout(false);
		this.panel4.ResumeLayout(false);
		this.panel4.PerformLayout();
		this.groupBox1.ResumeLayout(false);
		this.groupBox1.PerformLayout();
		base.ResumeLayout(false);
	}
}
